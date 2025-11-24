# Tic-Tac-Toe Unity Game

A complete Tic-Tac-Toe game built in Unity with Human vs Human and Human vs AI modes.

---

## Class Structure

- **GameController.cs**: Handles UI, player input, and updates the game board.
- **CellUI.cs**: Attached to each grid cell prefab, forwards clicks to GameController.
- **SettingsManager.cs**: Singleton storing game settings (sound, AI difficulty, game mode).
- **SettingsUIInitializer.cs**: Updates Settings UI to match saved settings.
- **BoardState.cs**: Handles the core game logic (cell states, checking for winner/draw).
- **TurnManager.cs**: Tracks whose turn it is and switches turns.
- **IAIStrategy.cs**: Interface defining AI move selection.
- **RuleBasedAIStrategy.cs**: Implements AI behavior (win, block, or random move).

---

## AI Behavior

The AI follows a **rule-based algorithm**:

1. **Win:** If the AI can win in the next move, it selects that cell.
2. **Block:** If the opponent can win in their next move, it blocks.
3. **Random:** Otherwise, it chooses a random available cell.

This ensures competitive yet simple AI gameplay.

---

## How to Run

1. Open the Unity project.
2. Open the `MainMenu` scene.
3. Click **Play** to start the game.
4. Click on grid cells to place X or O.
5. In Human vs AI mode, the AI moves automatically.
6. Press **Reload** to reset the board.
7. Use **Settings** to adjust game mode, AI difficulty, or toggle sound.

