const fs = require('fs');
const p = 'E:/Bharath/SuperMarket App/cloudflare/worker/wrangler.toml';
let t = fs.readFileSync(p, 'utf8');
t = t.replace('REPLACE_WITH_D1_DATABASE_ID', 'ab5a2c8b-53b3-432a-962d-b201a074a65d');
fs.writeFileSync(p, t);
console.log('done');
