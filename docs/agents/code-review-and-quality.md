# Code Review And Quality

Use this workflow before merging or signing off on a change.

## Goal

Review code across correctness, readability, architecture, security, and performance so changes improve overall code health.

## Approval Standard

Approve when the change clearly improves the codebase and follows project conventions, even if it is not perfect. Do not block on personal style preferences.

## Review Axes

### 1. Correctness

- Does the change match the task or spec?
- Are edge cases and error paths handled?
- Do tests cover the intended behavior?
- Would the tests catch a regression?

### 2. Readability And Simplicity

- Are names clear and consistent?
- Is control flow easy to follow?
- Is the code simpler than the problem requires, not more abstract?
- Is dead code or no-op scaffolding left behind?

### 3. Architecture

- Does the change fit existing patterns?
- Are boundaries clean?
- Is duplication justified or avoidable?
- Are dependencies flowing in the right direction?

### 4. Security

- Is external or user input treated as untrusted?
- Are secrets absent from code and logs?
- Are validation and authorization handled where needed?
- Are risky outputs or queries properly encoded or parameterized?

### 5. Performance

- Are there obvious N+1 or repeated expensive operations?
- Are loops and data loads bounded?
- Are hot paths free of unnecessary allocations or sync blocking?
- Does the UI avoid unnecessary rerenders or heavy work?

## Review Process

1. Understand intent before judging implementation
2. Read tests first
3. Review implementation against the five axes
4. Categorize findings by severity
5. Verify the claimed verification steps

## Findings Format

- `Critical:` blocks merge
- Unprefixed findings are required changes
- `Consider:` or `Optional:` are non-blocking suggestions
- `Nit:` is minor and optional
- `FYI:` is informational only

## Change Size Guidance

- Around 100 lines changed is ideal
- Around 300 lines is acceptable for one logical change
- Around 1000 lines is usually too large and should be split unless it is largely mechanical

Separate refactoring from feature behavior when possible.

## Dead Code Hygiene

After refactors or structural changes:

1. Identify newly unused code
2. List it explicitly
3. Ask before deleting code you did not create for the current task

## Verification Checklist

- Intent is understood
- Tests exist and meaningfully cover the change
- Required verification was run
- Risks, regressions, and gaps are called out clearly
