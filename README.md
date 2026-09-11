# MathTrainer

A modular C# console application for practicing arithmetic and number-system conversions.

MathTrainer generates problems dynamically, checks user answers, tracks performance, and stores settings and scores between sessions.

## Features

### Arithmetic Problems

Generates arithmetic expressions using:

* Addition
* Subtraction
* Multiplication
* Division
* Nested expressions with parentheses

Arithmetic problems are represented as **expression trees**, allowing expressions to be generated and evaluated recursively.

The generator validates every expression before presenting it to the user. Invalid expressions, such as divisions with non-integer results, division by zero, negative results, overflows, or results outside the configured range, are discarded and regenerated.

### Number-System Conversions

Supports conversions between:

* Decimal
* Hexadecimal
* Octal
* Binary

All supported conversion directions can be selected manually or generated randomly.

Hexadecimal values are displayed using uppercase letters.

### Configurable Difficulty

The arithmetic generator can be configured through:

* Maximum number of digits
* Maximum number of operations
* Fixed or random digit count
* Fixed or random operation count
* Settings reset

These settings allow the difficulty and structure of generated problems to be adjusted without changing the source code.

### Scores and Persistence

Math and number-system conversion scores are tracked separately.

Application settings and scores are stored in **JSON files**, allowing them to persist between application sessions.

## Architecture

MathTrainer uses a modular architecture with clear separation of responsibilities.

The **controller** manages the application flow and switches between different application states such as the main menu, problem generation, settings, scores, and exit.

The **models** contain the core application logic. Arithmetic problems are built from recursive expression objects, where numbers and binary operations form an expression tree. This allows calculations, validation, and string representation to be handled recursively.

The **services** provide functionality shared across the application, including settings and score management.

The **view** is responsible for console rendering and user input, keeping presentation logic separate from the problem-generation and application-flow logic.

The application state system keeps the main execution loop independent from individual screens and operations, making the program easier to extend and maintain.

## Technical Highlights

* C# / .NET
* Console application
* Expression-tree-based arithmetic generation
* Recursive expression evaluation and validation
* Checked integer arithmetic with overflow handling
* Operator precedence-aware expression formatting
* Modular separation of application responsibilities
* State-based application flow
* JSON-based persistence
* Configurable problem generation

## Example

An arithmetic problem can be generated as:

```text
(12 + 8) * 3 - 6
```

The expression is internally represented as a tree rather than as a plain string. Each operation recursively evaluates its child expressions, allowing the complete expression to be validated and calculated before being shown to the user.

A conversion problem may look like:

```text
Convert 255 from Decimal to Hexadecimal:
> FF
```

## Purpose

MathTrainer was created as a practical C# project for practicing mathematics while experimenting with modular application design, recursive data structures, expression generation, state management, and persistent application data.
