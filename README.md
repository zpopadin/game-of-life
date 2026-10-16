# GameOfLife

Conway's Game of Life on a finite grid. Cells outside the board are always dead.

## Run in Visual Studio

Install Visual Studio 2022 17.8+ (or newer) with the **.NET desktop development** workload and .NET 8 SDK. Open `GameOfLife.sln` and press F5. No NuGet packages or downloaded assets are needed. Windows is required.

Command line: `dotnet run --project GameOfLife`

## Controls

Click a cell to toggle it while paused. Start/Pause runs the simulation; Step advances once while paused. Random fills the board and pauses. Clear resets it. Glider inserts a small moving pattern at the center.

## Explore the code

Start with `GameOfLife/GameForm.cs`. The form and its controls are created in code; use the code editor rather than the visual Designer.

This is deliberately a beginner learning project. Every generation checks every cell and allocates a fresh grid. UI events and simulation logic live together. Comments are sparse so you can trace the code yourself. There are no intentional crashes or incomplete TODO features.

Try changing one rule or number, predict what will happen, and then run it. Commit your experiments so you can compare versions.
