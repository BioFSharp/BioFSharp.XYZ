# Working on a BioFSharp extension

## Purpose and scope

`BioFSharp.XYZ` is a template for an independently packaged extension of
[BioFSharp](https://github.com/BioFSharp/BioFSharp). Its current `BioTalk` library,
test, and documentation example demonstrate the tooling; they do not define the
purpose of a scaffolded package.

When scaffolding, replace the placeholder names and update this section with the
package's biological domain, supported capabilities, and boundaries. Keep this
guide aligned with the implementation. Use `README.md` for user-facing setup and
development instructions.

## Relationship to core BioFSharp

- Core provides general biological types, file-format models, IO, and foundational
  algorithms. Extensions build on that foundation with specialized functionality,
  models, and dependencies, and may have independent release schedules.
- Keep dependencies directed from the extension toward core and other reusable
  packages. Do not introduce a dependency from core back to this extension or a
  circular dependency between extensions.
- Check the referenced core package and relevant extension APIs before adding a
  type, parser, or algorithm. Reuse or compose existing representations instead of
  copying their implementations or creating competing versions of the same model.
- Use NuGet package references as the portable default. A sibling checkout can
  help with inspection, but a fresh clone must not require a particular local
  repository layout. Check that APIs found in another checkout exist in the
  package version this project actually references.

## Models, namespaces, and ownership

- A namespace describes conceptual organization, not repository or assembly
  ownership. An extension may add distinct modules under
  `BioFSharp.FileFormats.<Format>` and `BioFSharp.IO.<Format>`, as appropriate.
  Avoid fully qualified type or module names already exported by core or another
  dependency. Package-specific APIs normally belong under `BioFSharp.XYZ`.
- Keep reusable format models and parsing/writing independent of network access,
  persistence, and workflow orchestration. This is a separation of responsibilities;
  it does not require a separate project or NuGet package for every layer.
- Distinguish a reusable file format from a service-specific response or projection.
  Preserve biological meaning when modelling data, including identifier semantics,
  units, coordinate conventions, evidence, and missing values where relevant.
  Prefer explicit types and conversions over silently dropping information.
- Upstreaming a mature model or parser is optional. Consider it when it is broadly
  useful independently of this extension. Moving public types between assemblies
  needs a compatibility and versioning strategy even if their namespaces stay the
  same; do not treat it as a transparent file move.

## Repository and code conventions

- `src/BioFSharp.XYZ` contains the library; `tests/BioFSharp.XYZ.Tests` contains
  xUnit tests. `docs` contains literate F# examples. `build` contains FAKE targets.
- Treat `global.json`, project files, and the local tool manifest as the sources
  of truth for SDK, target framework, and tool versions. The library's compatibility
  target is independent of the SDK and runtime used for development and tests.
- Register new F# files explicitly in their `.fsproj` in dependency order. Update
  the solution, project references, and `build/ProjectInfo.fs` when adding or
  renaming projects; keep documentation DLL references aligned.
- Follow existing F# conventions and document public APIs with `///` comments.
  Explain important biological assumptions and failure behavior in the API docs.
- Test observable behavior with representative inputs and relevant edge cases.
  Keep ordinary tests independent of live services where possible, using fixtures
  or injected dependencies. Keep any live integration checks separately opt-in.

## Build and validation

Run commands from the repository root. Prefer the FAKE entry points for
solution-wide checks; direct `dotnet` commands are useful for focused iteration.

| Task | Windows PowerShell | Linux / macOS |
| --- | --- | --- |
| Build | `.\build.cmd` | `bash ./build.sh` |
| Tests and coverage | `.\build.cmd RunTests` | `bash ./build.sh RunTests` |
| Build documentation | `.\build.cmd BuildDocs` | `bash ./build.sh BuildDocs` |
| Package locally | `.\build.cmd Pack` | `bash ./build.sh Pack` |

`RunTests` cleans and builds before testing. `Pack` also runs those checks, prompts
for the version from `RELEASE_NOTES.md`, and writes artifacts to `pkg`.
Documentation targets restore the pinned tools automatically. After changing
executable documentation, inspect the rendered examples as well as the command's
exit status.

Choose validation appropriate to the change. Documentation-only guidance edits
need path, link, and diff checks; they do not require rebuilding unchanged code.
Report the checks performed and any limitations.

Release targets can push Git tags, publish NuGet packages, and deploy documentation.
Run them as part of a requested release, not as routine validation. Publishing uses
`NUGET_API_KEY`; keep credentials out of source and logs. See the README for the
release workflow and the distinction between local packaging and publishing.
