# CsvParserApp

A flexible and extensible CSV processing framework for .NET.

The idea is to have a generic CSV parser that could work for different DTOs, where each DTO can have different parsers. The result is converted to a common DTO class.


## Features

- Dynamic parser selection based on a `parserType` string.
- Strongly-typed parsers via `ICsvParser<T>`, each with its own validation and transformation logic.
- Shared transformation and validation logic using `CsvTransformers` and `CsvValidators`.
- Detailed per-row error tracking with messages and raw data.
- Output in unified format via `ProcessorResult`.
- Console output formatting via pluggable presenters (`IResultPresenter`).

## Architecture Overview
Program.cs
│
├── CsvProcessorRouter // Selects the correct parser based on parserType
│
├── ICsvParser<T> // Interface for all specific CSV parsers
│
├── ICsvProcessor<T> // Processor that reads, parses, validates and returns results
│
├── CsvValidators.cs // Shared static validators (email, phone, etc.)
├── CsvTransformers.cs // Shared static transformers (split name, convert decimal, etc.)
│
└── IResultPresenter // Interface to render or export results
└── ConsoleJsonResultPresenter // Prints results as pretty JSON to console

## Example Workflow

1. A CSV file is passed to the app, along with a `parserType` (e.g. `"customerTypeA"`).
2. The `CsvProcessorRouter` selects the appropriate parser (e.g. `CustomerTypeAParser`).
3. The file is read and parsed line by line.
4. Valid and invalid rows are returned in a `ProcessorResult`:
    - `Successes`: list of parsed DTOs.
    - `Failures`: list of (row, error messages).
5. Results are passed to an `IResultPresenter` (e.g. `ConsoleJsonResultPresenter`) for display or output.


## Adding a New Parser

To support a new CSV format, follow these steps:

### 1. Create a DTO (if needed)
### 2. Implement a parser
### 3. Register the parser in Program.cs
### 4. Register the parser in CsvProcessorRouter

## How to run

```bash
dotnet run --parserType=customerTypeA --filePath=path/to/file.csv