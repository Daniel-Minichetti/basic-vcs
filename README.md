# basic-vcs

`basic-vcs` is a lightweight, cross-platform version control system for managing individual file versions via the command line. It's designed for simple versioning use cases where Git would be overkill, or for educational use, prototyping, and file change tracking.

Built in C# using .NET 8, this CLI tool supports versioning, logging, diffing, restoring, and file ignore rules. 

Most Importantly it helped me better understand how version control works.

---

## Features

- Initialize a file-based version control system
- Commit file versions with optional author/email metadata
- View a detailed commit log with timestamps
- Restore a file to a previous version
- Show inline diffs between any two versions
- Ignore files from versioning using `.vcsignore`
- Persist user info using `.vcsconfig`
- Installable as a global .NET tool

---

## Installation

### Build and install locally as a .NET global tool:

```bash
dotnet pack -c Release
dotnet tool install --global --add-source ./bin/Release basic-vcs
