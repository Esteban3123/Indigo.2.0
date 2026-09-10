param(
    [string]$SourceBranch = "master",
    [string[]]$TargetBranches = @("v25.47c"),
    [Parameter(Mandatory = $true)]
    [string]$BranchSlug,
    [Parameter(Mandatory = $true)]
    [string]$CommitMessage,
    [Parameter(Mandatory = $true)]
    [string[]]$Paths,
    [string]$Remote = "origin"
)

$ErrorActionPreference = "Stop"

function Invoke-Git {
    param([Parameter(ValueFromRemainingArguments = $true)][string[]]$Args)
    & git @Args
    if ($LASTEXITCODE -ne 0) {
        throw "git $($Args -join ' ') failed with exit code $LASTEXITCODE"
    }
}

function New-BranchName {
    param([string]$BaseBranch)
    $safeBranch = $BaseBranch -replace '[^A-Za-z0-9._-]', '-'
    return "feature/$BranchSlug-$safeBranch"
}

function Resolve-BranchPath {
    param([string]$Path)
    if (Test-Path -LiteralPath $Path) {
        return $Path
    }

    $alternates = @()
    if ($Path.StartsWith("Vie_Erp/")) {
        $alternates += $Path.Replace("Vie_Erp/", "Vie_ERP/")
    }
    if ($Path.StartsWith("Vie_ERP/")) {
        $alternates += $Path.Replace("Vie_ERP/", "Vie_Erp/")
    }

    foreach ($alternate in $alternates) {
        if (Test-Path -LiteralPath $alternate) {
            return $alternate
        }
    }

    return $Path
}

function Convert-PatchForBranch {
    param(
        [string]$SourcePatch,
        [string]$OutputPatch,
        [string[]]$OriginalPaths,
        [string[]]$BranchPaths
    )

    $content = Get-Content -LiteralPath $SourcePatch -Raw
    for ($i = 0; $i -lt $OriginalPaths.Count; $i++) {
        $from = $OriginalPaths[$i]
        $to = $BranchPaths[$i]
        if ($from -ne $to) {
            $content = $content.Replace("a/$from", "a/$to")
            $content = $content.Replace("b/$from", "b/$to")
        }
    }
    Set-Content -Path $OutputPatch -Value $content -Encoding UTF8
}

$repoRoot = (& git rev-parse --show-toplevel).Trim()
if ($LASTEXITCODE -ne 0 -or [string]::IsNullOrWhiteSpace($repoRoot)) {
    throw "Run this script inside a git repository."
}

Set-Location $repoRoot

$patchFile = Join-Path ([System.IO.Path]::GetTempPath()) ("db-commit-{0}.patch" -f ([Guid]::NewGuid().ToString("N")))
$branchPatchFile = Join-Path ([System.IO.Path]::GetTempPath()) ("db-commit-branch-{0}.patch" -f ([Guid]::NewGuid().ToString("N")))
try {
    & git diff -- @Paths | Set-Content -Path $patchFile -Encoding UTF8
    if ((Get-Item $patchFile).Length -eq 0) {
        throw "No local changes found for the selected paths."
    }

    $allBranches = @($SourceBranch) + $TargetBranches
    foreach ($baseBranch in $allBranches) {
        Invoke-Git switch $baseBranch
        Invoke-Git pull $Remote $baseBranch

        $workBranch = New-BranchName $baseBranch
        if ((& git branch --list $workBranch).Trim()) {
            throw "Branch '$workBranch' already exists. Rename BranchSlug or delete the existing branch."
        }

        Invoke-Git switch -c $workBranch
        $branchPaths = @($Paths | ForEach-Object { Resolve-BranchPath $_ })
        Convert-PatchForBranch -SourcePatch $patchFile -OutputPatch $branchPatchFile -OriginalPaths $Paths -BranchPaths $branchPaths

        & git apply --3way $branchPatchFile
        if ($LASTEXITCODE -ne 0) {
            throw "Patch did not apply cleanly on $baseBranch. Resolve manually on branch '$workBranch'."
        }

        Invoke-Git add -- @branchPaths
        Invoke-Git commit -m $CommitMessage
        Invoke-Git push -u $Remote $workBranch
    }
}
finally {
    if (Test-Path $patchFile) {
        Remove-Item -LiteralPath $patchFile -Force
    }
    if (Test-Path $branchPatchFile) {
        Remove-Item -LiteralPath $branchPatchFile -Force
    }
}
