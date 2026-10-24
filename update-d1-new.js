const fs = require('fs');
const p = 'E:/Bharath/SuperMarket App/cloudflare/worker/wrangler.toml';
let t = fs.readFileSync(p, 'utf8');
t = t.replace(/database_id\s*=\s*"[^"]+"/, 'database_id = "ed36a9c0-d2e8-497a-8524-9777e66066af"');
fs.writeFileSync(p, t);
console.log('ok');
