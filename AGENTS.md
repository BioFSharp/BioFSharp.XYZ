# BioFSharp extension guide

`BioFSharp.XYZ` extends [BioFSharp](https://github.com/BioFSharp/BioFSharp). After scaffolding, replace this introduction with the package's purpose and scope.

## Architecture

- Reuse core biological types, formats, parsers, and algorithms before adding overlapping implementations.
- Dependencies flow from extensions to core. Use NuGet references so a fresh clone does not require sibling repositories.
- Add distinct modules to `BioFSharp.FileFormats` and `BioFSharp.IO` where appropriate; avoid existing type/module names. Package-specific APIs belong under `BioFSharp.XYZ`.
- Keep reusable models and parsers independent of network access, persistence, and orchestration. Preserve biological semantics and missing values.
- Mature, broadly useful models may be upstreamed. Moving public types between assemblies requires compatibility planning even when namespaces stay unchanged.

## Development

- Follow the SDK, framework, and tool versions in the repository configuration.
- Register F# files in `.fsproj` dependency order; update solution/project references when renaming. Document public APIs with `///` comments.
- Test observable behavior and relevant edge cases. Keep ordinary tests offline and live integration tests opt-in.
- Run checks appropriate to the change; inspect rendered output when changing executable documentation.

Run from the repository root:

| Task | Windows | Linux / macOS |
| --- | --- | --- |
| Build | `.\build.cmd` | `bash ./build.sh` |
| Tests + coverage | `.\build.cmd RunTests` | `bash ./build.sh RunTests` |
| Docs | `.\build.cmd BuildDocs` | `bash ./build.sh BuildDocs` |
| Local packages | `.\build.cmd Pack` | `bash ./build.sh Pack` |

`Pack` builds, tests, and prompts for the version. Release targets publish externally. See [README.md](README.md) for scaffolding and release instructions.
