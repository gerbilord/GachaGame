# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Unity Project Details
- **Unity Version**: 2022.3.0f1 LTS
- **Platform**: PC/Mac Standalone
- **Render Pipeline**: Built-in Render Pipeline
- **UI System**: Unity UI (uGUI) with TextMeshPro

## Development Commands

### Unity Editor Operations
Unity development is primarily done through the Unity Editor GUI. Key operations:
- **Open Project**: Launch Unity Hub and open the project folder
- **Play Mode**: Press Play button in Unity Editor or Ctrl+P
- **Build**: File → Build Settings → Build (or Ctrl+Shift+B)

### Code Compilation
Unity automatically compiles C# scripts when:
- Files are saved in the IDE
- Unity Editor window regains focus
- Manually triggered via Assets → Reimport All

### Testing
Unity Test Framework is included in packages. Test files exist at `Assets/Scripts/Tests/TestCardGeneration.cs`

## Architecture Overview

### Scene Management System
The game uses an additive scene loading architecture:
- **Scene_Core**: Always loaded, contains SceneLoader singleton and Core_InitScript
- **Scene_MainMenu**: Main menu with buttons for pack opening and quit
- **Scene_PackOpener**: Pack opening interface (displays 3 packs of 7 cards each)
- **Scene_CardLibrary**: Card library display (not yet implemented)

Scene transitions use LoadingScreenAnimator with slide animations and configurable pause duration.

### Card System Architecture

**Core Components** (`Assets/Scripts/Cards/`):
- `CardData`: Main data container with stats dictionary, creature type, magic/nightmare flags, and rarity level
- `BasicCardData`: 12 predefined card templates with base stats and magic chances
- `CardGenerator`: Static class implementing the card generation algorithm with rarity scaling
- `Rarity`: Encapsulates rarity levels (1-8) with associated extra stat points (0-49)
- `StatUtils`: Helper methods for stat categorization (special vs non-special stats)

**Card Generation Algorithm**:
1. Start at rarity 1, 25% chance to increase each level (up to 8)
2. Assign extra stats based on rarity (7 points per rarity level above 1)
3. Apply special stat decay (25% for Special1, 50% for Special2)
4. 1% chance for nightmare transformation (transfers Armor/Resist to Special3/Special4)
5. Stat distribution weights: Attack/HP (25% each), Special1/2 (12.5% each), Mana/Armor/Resist (~8.3% each)

### UI System Components

**Card Display System**:
- `CardDisplay`: MonoBehaviour that renders card data using TextMeshPro components
- Dynamically sets background color based on rarity
- Shows/hides magic and nightmare indicators
- Updates all stat text fields

**Scene Control**:
- `SceneLoader`: Singleton managing additive scene loading/unloading
- `LoadingScreenAnimator`: Handles loading screen slide animations
- Main menu navigation using Unity Events on Button components
- ESC key returns to main menu from pack opener

**Highlighter Framework** (prepared for future use):
- `IHighlighter`: Interface for unified highlighting behavior
- `Highlighter2D/3D`: Implementations for UI and 3D object highlighting
- Currently not integrated with card display system

### Key Input Controls
- **R Key**: Regenerate all cards (in PackOpener scene)
- **ESC Key**: Return to main menu (from PackOpener scene)
- **Unity Editor**: Ctrl+P for Play Mode, Ctrl+Shift+B for Build

## Build Configuration
Enabled scenes in build order:
1. Scene_Core (always loaded first)
2. Scene_CardLibrary
3. Scene_MainMenu  
4. Scene_PackOpener

Note: Legacy "PackOpener.unity" is disabled in build settings.

## Project Dependencies
- TextMeshPro (for UI text rendering)
- Unity UI package (uGUI system)
- Unity Test Framework (for unit testing)
- IDE support packages (Rider, Visual Studio, VSCode)