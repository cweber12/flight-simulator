param(
    [string]$Workflow = "Unity Tests",
    [string]$OutputDirectory = "gh-actions-logs",
    [string]$RunId = ""
)

$ErrorActionPreference = "Stop"

if (-not (Get-Command gh -ErrorAction SilentlyContinue)) {
    throw "GitHub CLI is required. Install gh, then run 'gh auth login'."
}

try {
    gh auth status 1>$null
}
catch {
    throw "GitHub CLI is not authenticated. Run 'gh auth login' and try again."
}

if ([string]::IsNullOrWhiteSpace($RunId)) {
    $runsJson = gh run list --workflow $Workflow --limit 20 --json databaseId,status,conclusion,createdAt,displayTitle
    $runs = $runsJson | ConvertFrom-Json
    $run = $runs | Where-Object { $_.conclusion -eq "failure" } | Select-Object -First 1

    if ($null -eq $run) {
        $run = $runs | Select-Object -First 1
    }

    if ($null -eq $run) {
        throw "No workflow runs found for '$Workflow'."
    }

    $RunId = [string]$run.databaseId
}

$runDirectory = Join-Path $OutputDirectory $RunId
New-Item -ItemType Directory -Force -Path $runDirectory | Out-Null

gh run view $RunId --log | Out-File -FilePath (Join-Path $runDirectory "workflow.log") -Encoding utf8
gh run view $RunId --json databaseId,status,conclusion,createdAt,updatedAt,headBranch,headSha,event,name,url | Out-File -FilePath (Join-Path $runDirectory "run.json") -Encoding utf8

try {
    gh run download $RunId --dir (Join-Path $runDirectory "artifacts")
}
catch {
    Write-Warning "No artifacts were downloaded. The run may have failed before artifact upload."
}

Write-Host "Downloaded GitHub Actions logs and artifacts to $runDirectory"
