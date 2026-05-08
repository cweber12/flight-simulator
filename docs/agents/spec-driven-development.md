# Spec-Driven Development

Use this workflow when starting a new feature, making a significant change, or working from incomplete requirements.

## Goal

Create a shared source of truth before coding so implementation is driven by explicit success criteria instead of guesses.

## When To Use

- Starting a new project or feature
- Requirements are ambiguous or incomplete
- The change spans multiple files or modules
- The task requires an architectural decision
- The task is likely to take more than 30 minutes

Skip this workflow for typo fixes, one-line changes, and clearly bounded edits with obvious acceptance criteria.

## Gated Phases

Do not advance past a phase until the human has reviewed and approved it.

1. Specify
2. Plan
3. Tasks
4. Implement

## Phase 1: Specify

Start by surfacing assumptions before writing the spec.

Example:

```text
ASSUMPTIONS I'M MAKING:
1. This is a Unity project using existing repository patterns.
2. We are extending current tooling rather than replacing it.
3. Existing test and build commands remain the source of truth.
-> Correct me now or I will proceed with these assumptions.
```

Write a spec that covers:

1. Objective
2. Commands
3. Project Structure
4. Code Style
5. Testing Strategy
6. Boundaries
7. Success Criteria
8. Open Questions

Template:

```md
# Spec: [Project/Feature Name]

## Objective
[What we are building and why.]

## Tech Stack
[Framework, language, key dependencies.]

## Commands
[Build, test, lint, dev.]

## Project Structure
[Relevant directories and responsibilities.]

## Code Style
[Representative snippet and conventions.]

## Testing Strategy
[Frameworks, test locations, required coverage.]

## Boundaries
- Always: [...]
- Ask first: [...]
- Never: [...]

## Success Criteria
[Specific, testable completion conditions.]

## Open Questions
[Anything unresolved that needs human input.]
```

Translate vague requests into measurable success criteria before proceeding.

## Phase 2: Plan

Build an implementation plan from the approved spec:

- Identify major components and dependencies
- Choose implementation order
- Note risks and mitigations
- Separate sequential work from parallelizable work
- Define verification checkpoints

The plan should be reviewable and specific enough for the human to approve or redirect.

## Phase 3: Tasks

Break the plan into discrete tasks:

- Each task should fit in a focused work session
- Each task needs explicit acceptance criteria
- Each task needs a verification step
- Order tasks by dependency
- Aim to keep each task to roughly five files or fewer

Template:

```md
- [ ] Task: [Description]
  - Acceptance: [What must be true when done]
  - Verify: [Command or manual check]
  - Files: [Expected files to change]
```

## Phase 4: Implement

Implement tasks one at a time. Keep the spec updated if scope or design decisions change.

## Living Spec Rules

- Update the spec before implementing changed decisions
- Reflect scope changes in the spec
- Commit the spec alongside the code
- Reference the spec in PRs or handoffs when relevant

## Verification Checklist

- The spec covers all core areas
- The human approved the spec
- Success criteria are specific and testable
- Boundaries are defined
- The spec is saved in the repository
