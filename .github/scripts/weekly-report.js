// .github/scripts/weekly-report.js
// Used by docs-weekly.yml (J2). Node 20+ (preinstalled on ubuntu-latest), no dependencies.
//
// Writes:
//   docs/worklog.md                  one line per merged PR (appended, no duplicates)
//   docs/reports/<year>-W<week>.md   merged PRs by feature, open PRs, decision changes, Risks & blockers
'use strict';

const { execFileSync } = require('node:child_process');
const fs = require('node:fs');
const path = require('node:path');

// execFileSync passes arguments directly, without a shell, so PR titles can't inject commands.
function run(cmd, args) {
  return execFileSync(cmd, args, { encoding: 'utf8', maxBuffer: 10 * 1024 * 1024 });
}
function gh(args) {
  return JSON.parse(run('gh', args));
}

const DAY = 24 * 60 * 60 * 1000;
const isoDate = (d) => d.toISOString().slice(0, 10);
const isValidDate = (s) => /^\d{4}-\d{2}-\d{2}$/.test(s) && !Number.isNaN(Date.parse(`${s}T00:00:00Z`));

// ISO 8601 week number (weeks start on Monday; week 1 contains the first Thursday)
function isoWeek(dateStr) {
  const d = new Date(`${dateStr}T00:00:00Z`);
  const day = d.getUTCDay() || 7;            // Mon=1 .. Sun=7
  d.setUTCDate(d.getUTCDate() + 4 - day);    // Thursday of this week decides the year
  const year = d.getUTCFullYear();
  const week = Math.ceil(((d - Date.UTC(year, 0, 1)) / DAY + 1) / 7);
  return { year, week: String(week).padStart(2, '0') };
}

// PR titles, branch names and commit messages are written by PR authors: treat them as untrusted.
// Strip characters that could break the markdown or inject HTML/links into the report.
function clean(s) {
  return String(s ?? '')
    .replace(/[\r\n|<>`[\]]/g, ' ')
    .replace(/\s+/g, ' ')
    .trim()
    .slice(0, 120);
}
const login = (pr) => clean(pr.author?.login ?? 'ghost');

// F-ID from the PR title or branch name (convention still to be agreed in DEC-7)
function featureId(pr) {
  const m = `${pr.title} ${pr.headRefName ?? ''}`.match(/\bF([1-7])\b/i);
  return m ? `F${m[1]}` : 'Other';
}

// ---- Period: manual inputs, or the 7 days ending yesterday (Mon..Sun when run on a Monday)
let since = process.env.SINCE || '';
let until = process.env.UNTIL || '';
if (!since || !until) {
  const now = new Date();
  const end = new Date(Date.UTC(now.getUTCFullYear(), now.getUTCMonth(), now.getUTCDate()) - DAY);
  const start = new Date(end.getTime() - 6 * DAY);
  since = isoDate(start);
  until = isoDate(end);
}
if (!isValidDate(since) || !isValidDate(until) || since > until) {
  console.error(`Invalid period: ${since}..${until} (expected YYYY-MM-DD, since <= until)`);
  process.exit(1);
}
const { year, week } = isoWeek(since);
const label = `${year}-W${week}`;

// ---- Data from GitHub
const merged = gh([
  'pr', 'list', '--state', 'merged',
  '--search', `merged:${since}..${until}`,
  '--json', 'number,title,author,labels,headRefName,mergedAt,url',
  '--limit', '200',
]).sort((a, b) => a.mergedAt.localeCompare(b.mergedAt));

const open = gh([
  'pr', 'list', '--state', 'open',
  '--json', 'number,title,author,url,isDraft',
  '--limit', '100',
]);

const decisionLog = run('git', [
  'log', `--since=${since}T00:00:00Z`, `--until=${until}T23:59:59Z`,
  '--date=short', '--pretty=format:%h %ad %s', '--', 'docs/decisions/',
]).trim();

// ---- Worklog: append one line per merged PR, skip PRs already logged (safe to re-run)
const worklogPath = path.join('docs', 'worklog.md');
fs.mkdirSync(path.dirname(worklogPath), { recursive: true });
let worklog = fs.existsSync(worklogPath)
  ? fs.readFileSync(worklogPath, 'utf8')
  : '# Worklog\n\nOne line per merged PR, generated weekly by J2 (docs-weekly.yml).\n\n';
let added = 0;
for (const pr of merged) {
  if (worklog.includes(`(${pr.url})`)) continue;
  if (!worklog.endsWith('\n')) worklog += '\n';
  worklog += `- ${pr.mergedAt.slice(0, 10)} · ${featureId(pr)} · #${pr.number} ${clean(pr.title)} · @${login(pr)} · [PR](${pr.url})\n`;
  added++;
}
fs.writeFileSync(worklogPath, worklog);

// ---- Weekly report
const groups = {};
for (const pr of merged) (groups[featureId(pr)] ||= []).push(pr);

let md = `# Weekly report ${label}\n\n`;
md += `*Period: ${since} to ${until} · generated ${isoDate(new Date())} by J2 (docs-weekly.yml)*\n\n`;

md += '## Merged PRs by feature\n\n';
if (merged.length === 0) md += 'No PRs merged in this period.\n\n';
for (const f of Object.keys(groups).sort()) {
  md += `### ${f}\n\n`;
  for (const pr of groups[f]) {
    md += `- #${pr.number} ${clean(pr.title)} (@${login(pr)}, ${pr.mergedAt.slice(0, 10)}) · [PR](${pr.url})\n`;
  }
  md += '\n';
}

md += '## Open PRs\n\n';
md += open.length
  ? open.map((pr) => `- #${pr.number} ${clean(pr.title)} (@${login(pr)})${pr.isDraft ? ' · draft' : ''} · [PR](${pr.url})`).join('\n') + '\n\n'
  : 'No open PRs.\n\n';

md += '## Decisions added or changed\n\n';
md += decisionLog
  ? decisionLog.split('\n').map((l) => `- ${clean(l)}`).join('\n') + '\n\n'
  : 'No changes in docs/decisions/.\n\n';

md += '## Risks & blockers\n\n<!-- Tom: fill in before merging -->\n\n- \n';

const reportPath = path.join('docs', 'reports', `${label}.md`);
fs.mkdirSync(path.dirname(reportPath), { recursive: true });
fs.writeFileSync(reportPath, md);

// ---- Hand values to the next workflow step
if (process.env.GITHUB_OUTPUT) {
  fs.appendFileSync(process.env.GITHUB_OUTPUT, `label=${label}\nsince=${since}\nuntil=${until}\nmerged=${merged.length}\n`);
}
console.log(`Report ${label}: ${merged.length} merged, ${open.length} open, ${added} new worklog line(s).`);
