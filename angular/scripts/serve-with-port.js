// Runs ng serve with port from PORT env (set by Aspire). No --open so the browser
// is not auto-opened here; open the app from the Aspire dashboard to avoid two tabs.
const { execSync } = require('child_process');
const port = process.env.PORT || '4242';
execSync(`npx ng serve --port ${port}`, { stdio: 'inherit' });
