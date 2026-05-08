# Agent Operating Rules

This repository keeps agent instructions in two layers:

- `AGENTS.md` is the runtime contract: short rules, routing logic, and hard boundaries.
- `docs/agents/*.md` contains the detailed workflow playbooks and rationale.

Use the smallest process that safely fits the task.

## Decision Tree

- Is the request ambiguous, architectural, cross-cutting, or likely to take more than 30 minutes?
  - Yes: use the spec workflow before coding.
- Does the request add logic, change behavior, or fix a bug?
  - Yes: use the TDD workflow within the approved scope.
- Is the change ready to merge or being reviewed?
  - Yes: use the review workflow before sign-off.

## Default Execution Mode

- Small, unambiguous change:
  - Inspect the relevant code.
  - Implement the smallest correct change.
  - Run targeted verification.
  - Report what changed and any remaining risk.
- Behavior change or bug fix:
  - Reproduce with a failing test first.
  - Make the minimal fix.
  - Re-run the relevant tests after each meaningful change.
- Large or unclear work:
  - Write a spec and get human approval before implementation.

## Always / Ask First / Never

### Always

- Preserve existing user changes unless explicitly asked to modify them.
- State important assumptions when requirements are incomplete.
- Run relevant verification after code changes.
- Keep changes scoped to the requested outcome.
- Prefer small, reviewable edits over broad refactors.

### Ask First

- Database or serialized data schema changes.
- New dependencies or package manager changes.
- CI, build, or deployment configuration changes.
- Removing dead code you did not create during the current task.
- Architectural changes with non-obvious tradeoffs.

### Never

- Commit secrets, credentials, or tokens.
- Revert unrelated local changes.
- Remove or disable tests only to make the suite pass.
- Edit generated or vendor-owned content unless the task specifically requires it.

## Verification Expectations

- Run the smallest test set that proves the change.
- If behavior changed, include or update tests that would catch a regression.
- If verification could not be run, say so clearly in the handoff.

## Workflow Triggers

- Spec workflow: [`docs/agents/spec-driven-development.md`](docs/agents/spec-driven-development.md)
- TDD workflow: [`docs/agents/test-driven-development.md`](docs/agents/test-driven-development.md)
- Review workflow: [`docs/agents/code-review-and-quality.md`](docs/agents/code-review-and-quality.md)

## Precedence

When multiple workflows apply, use them in this order:

1. Spec first for large or ambiguous work.
2. TDD during implementation for behavior changes.
3. Review before merge or final sign-off.
