# Sudoku.Resolver

This project aims to be readable by following the *Clean Code: A Handbook of Agile Software* guidelines.
It tries to achieve a balance between performance and readability.

The second goal is to use the libraries as a basis for various services that can be written around these sets of libraries.

## Content

The project contains the following libraries:
1. **Sudoku.Parser.*:** Parse puzzles from various sources. For example, the internet or from a file.
2. **Sudoku:** Contains the data structure to represent and validate a variable sized sudoku board. And various algorithms to solve puzzles.  Currently only a Brute Force strategy is implemented.
4. **Sudoku.Benchmark:** A benchmark project to test the performance characteristics.
5. ***.Tests:** Unit tests. Realistically a lot more tests need to be written to fully validate the functionality. But various examples have been given.
6. **Sudoku.Puzzles:** A puzzles library containing various puzzles used in unit testing. It also includes 49151 17 clue puzzles used in a performance showdown: [the-fastest-sudoku-solver](https://codegolf.stackexchange.com/questions/190727/the-fastest-sudoku-solver)

## Jenkins

The master branch contains a Jenkins file. With each commit a webhook is triggered from [Gitea](https://gitea.io/en-us/).
This triggers the build procedure containing the following build steps on the Jenkins server.

1. Restore packages
2. Run unit tests
3. Package each of the required libraries and push them to a privately hosted NuGet server. Skip duplicate package versions. 

A package will only be successfully published when the version is unique and all unit tests pass.

### Hosting

#### Jenkins

Privately hosed on [Portainer](https://www.portainer.io/). A custom Docker image has been built to include .NET related technologies.

1. .NET 6 SDK and runtime.
2. The access key to the private NuGet server.

#### Gitea

[Gitea](https://gitea.io/en-us/) is also hosted privately on Portainer.
