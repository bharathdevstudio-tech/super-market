/**
 * Minimal D1-compatible shim backed by node:sqlite.
 *
 * This lets the real Worker handlers (src/**) run under `node --test` without
 * deploying anything. It implements exactly the surface the Worker uses:
 *
 *   env.DB.prepare(sql).bind(...).first() / .all() / .run()
 *   env.DB.batch([...])            -- atomic
 *
 * Values are passed to SQLite as bound parameters, same as D1.
 */

import { DatabaseSync } from 'node:sqlite';
import fs from 'node:fs';
import path from 'node:path';

function normalise(value) {
  if (value === undefined) return null;
  if (value === true) return 1;
  if (value === false) return 0;
  if (typeof value === 'bigint') return Number(value);
  return value;
}

class Statement {
  constructor(db, sql, params = []) {
    this.db = db;
    this.sql = sql;
    this.params = params;
  }

  bind(...params) {
    return new Statement(this.db, this.sql, params.map(normalise));
  }

  #arguments(params) {
    return params.length ? params.map(normalise) : this.params;
  }

  all(...params) {
    const statement = this.db.prepare(this.sql);
    const rows = statement.all(...this.#arguments(params));

    return {
      results: rows,
      success: true,
      meta: { changes: 0, last_row_id: null }
    };
  }

  first(...params) {
    const statement = this.db.prepare(this.sql);
    const row = statement.get(...this.#arguments(params));

    return row === undefined ? null : row;
  }

  get(...params) {
    return this.first(...params);
  }

  run(...params) {
    const statement = this.db.prepare(this.sql);
    const info = statement.run(...this.#arguments(params));

    return {
      results: [],
      success: true,
      meta: {
        changes: Number(info.changes || 0),
        last_row_id: info.lastInsertRowid === undefined ? null : Number(info.lastInsertRowid)
      }
    };
  }
}

class D1 {
  constructor(file) {
    this.db = new DatabaseSync(file);
    this.db.exec('PRAGMA foreign_keys = ON;');
  }

  prepare(sql) {
    return new Statement(this.db, sql);
  }

  /** D1 batches are atomic; emulate with BEGIN/COMMIT/ROLLBACK. */
  batch(statements) {
    if (!Array.isArray(statements)) throw new Error('batch expects an array');

    this.db.exec('BEGIN');
    try {
      const results = statements.map(s => (s.sql.trim().toUpperCase().includes('RETURNING') ? s.all() : s.run()));
      this.db.exec('COMMIT');
      return results;
    } catch (err) {
      this.db.exec('ROLLBACK');
      throw err;
    }
  }

  exec(sql) {
    this.db.exec(sql);
  }

  close() {
    this.db.close();
  }
}

/** Creates an in-memory D1 with the project's migrations applied. */
export function createTestDb() {
  const db = new D1(':memory:');

  const migrationDir = path.resolve(import.meta.dirname, '../d1/migrations');
  const files = fs.readdirSync(migrationDir).filter(f => f.endsWith('.sql')).sort();

  for (const file of files) {
    db.exec(fs.readFileSync(path.join(migrationDir, file), 'utf8'));
  }

  return db;
}

export { D1 };
