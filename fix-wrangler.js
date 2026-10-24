const fs = require('fs');
const p = 'E:/Bharath/SuperMarket App/cloudflare/worker/wrangler.toml';
let t = fs.readFileSync(p, 'utf8');
t = t.replace(/database_id\s*=\s*"[^"]*"/, 'database_id = "51c436f8-4b1c-467d-b851-cdc0002b2d31"');
fs.writeFileSync(p, t);
console.log('ok');
