A complete Tic-Tac-Toe game built in Unity, featuring Human vs Human and Human vs AI gameplay modes. This project demonstrates UI programming, turn-based logic, AI implementation, and clean OOP architecture in Unity.

---

## Features

- **Gameplay**
  - 3×3 grid for Tic-Tac-Toe.
  - Two game modes: Human vs Human, Human vs AI.
  - Detects wins, draws, and displays winning strike lines.
  - Reload button to reset the game.

- **AI**
  - Rule-based AI:
    1. Pick a winning move if available.
    2. Block the opponent's immediate winning move.
    3. Pick a random available cell.
  - AI difficulty adjustable via Settings page.

- **UI**
  - Main Menu with Play and Settings.
  - Settings page includes:
    - Dropdown for game mode.
    - Slider for AI difficulty.
    - Toggle for sound.
  - Dynamic updates of UI sprites for X and O.
  - Strike lines highlight winning combinations.

- **Code Architecture**
  - `GameController` manages UI and player interactions.
  - `BoardState` handles game logic independently of Unity.
  - `TurnManager` manages turn switching.
  - `IAIStrategy` interface and `RuleBasedAIStrategy` implement AI logic.
  - `SettingsManager` stores and persists game settings using PlayerPrefs.
  - Follows SOLID principles and uses Strategy Pattern for AI.
