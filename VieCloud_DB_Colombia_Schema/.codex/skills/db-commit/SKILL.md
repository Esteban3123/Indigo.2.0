---
name: db-commit
description: Automate database schema changes that must be committed separately to master, v25.47c, or other branches when cherry-pick is not allowed. Use when the user asks to pull a base branch, create a new branch, commit selected database files, push to origin, then repeat the same manual file changes on additional target branches.
---

# db-commit

Use this skill for database repository workflows where the same file changes must be applied to more than one branch without cherry-picking.

## Workflow

1. Confirm the repository root with `git rev-parse --show-toplevel`.
2. Check `git status --short --branch` and identify the changed files that should be included.
3. Save the local diff for the selected files before changing branches.
4. Update the source branch with `git pull origin <source-branch>`.
5. Create a work branch from the updated source branch.
6. Reapply the saved diff, resolve conflicts if needed, stage only the intended files, commit, and push to `origin`.
7. For each target branch, switch to the target branch, pull it, create a target work branch, reapply the same saved diff, resolve conflicts if needed, stage only the intended files, commit, and push.
8. Keep branch names explicit and branch-specific, for example:
   `feature/<slug>-master`
   `feature/<slug>-v25.47c`

## Script

Prefer the bundled script for repeatable executions:

```powershell
.\.codex\skills\db-commit\scripts\db-commit.ps1 `
  -SourceBranch master `
  -TargetBranches v25.47c `
  -BranchSlug apme-rips `
  -CommitMessage "Ajusta reporte RIPS para APME" `
  -Paths @(
    "Vie_Erp/Billing/Views/ViewGetInfoMedicamentosRIPS.sql",
    "Vie_Erp/Billing/Views/ViewGetInfoOtrosServiciosRIPS.sql"
  )
```

Add more target branches with:

```powershell
-TargetBranches v25.47c,release-x,hotfix-y
```

The script uses a patch, not cherry-pick. If patch application conflicts, stop and resolve the branch manually before committing.

The script handles the common database repo path casing difference between `Vie_Erp/` and `Vie_ERP/` when applying the same change across branches.
