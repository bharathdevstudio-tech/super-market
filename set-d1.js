const fs = require('fs');
const p = 'E:/Bharath/SuperMarket App/cloudflare/worker/wrangler.toml';
let t = fs.readFileSync(p, 'utf8');
t = t.replace(/database_id = "[^"]+"/, 'database_id = "39048666-f3d2-453e-affa-4fad227c406b"');
fs.writeFileSync(p, t);
console.log('ok');
