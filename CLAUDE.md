# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Unity Project Details
- **Unity Version**: 2022.3.0f1 LTS
- **Primary Scene**: Assets/Scenes/SampleScene.unity (configured in build settings)
- **Platform**: PC/Mac Standalone

## Development Commands

### Unity Editor Operations
Unity development is primarily done through the Unity Editor GUI. Key operations:
- **Open Project**: Launch Unity Hub and open the project folder
- **Play Mode**: Press Play button in Unity Editor or Ctrl+P
- **Build**: File → Build Settings → Build (or Ctrl+Shift+B)
- **Scene Navigation**: File → Open Scene → Assets/Scenes/PackOpener.unity

### Code Compilation
Unity automatically compiles C# scripts when:
- Files are saved in the IDE
- Unity Editor window regains focus
- Manually triggered via Assets → Reimport All

### Testing
No test framework is currently configured. To add tests:
1. Window → General → Test Runner
2. Create test assemblies in Assets/Tests/
3. Use Unity Test Framework (already in packages)

## Architecture Overview

### Card System
The core game revolves around a card generation system with the following structure:

**Data Model** (`Assets/Scripts/Cards/`):
- `CardData.cs`: Base card structure with stats dictionary, creature type, and magic properties
- `BasicCardData.cs`: Predefined card templates (12 types) with base stats
- `CardGenerator.cs`: Card generation logic (currently empty - needs implementation)

**Enumerations**:
- `CreatureType`: 12 types (Goblin, Fairy, Witch, Demon, etc.)
- `Stat`: 9 stat types (Attack, HP, Armor, Resist, Special1-4, Mana)
- `Rarity`: 8 tiers from Common to "Literally hacking"

### UI System
**Highlighter Framework** (`Assets/Scripts/UiScripts/Highlighter/`):
- `IHighlighter`: Interface for consistent highlighting behavior
- `Highlighter2D`: UI element highlighting using overlay images
- `Highlighter3D`: 3D object highlighting using emission materials
- `HighlighterUtils`: Static utility methods for highlighting operations

### Project Structure
```
Assets/
├── Art/              # Card templates (PNG/PSD), UI elements
├── Scenes/           # PackOpener.unity (main), SampleScene.unity
├── Scripts/
│   ├── Cards/        # Card system implementation
│   ├── Globals/      # Utility classes (AsyncUtils)
│   └── UiScripts/    # UI components and highlighters
├── Prefabs/          # (Currently empty)
└── Resources/        # (Currently empty)
```

## Key Implementation Notes

1. **Card Generation**: The `CardGenerator` class is empty and needs implementation. It should use `BasicCardData` templates and apply rarity-based stat modifications.

2. **Scene Setup**: PackOpener.unity is the main scene but likely contains minimal implementation. UI prefabs and card display components need to be created.

3. **Resource Loading**: Resources folder exists but is empty. Card art and prefabs should be placed here for runtime loading.

4. **Stat System**: Uses a dictionary-based approach (`Dictionary<Stat, float>`) for flexible stat management across different card types.

5. **IDE Integration**: Project is configured for JetBrains Rider, Visual Studio, and VSCode. Unity automatically generates .csproj files.

## Missing Components
- Card prefabs and UI elements
- Pack opening logic and animations  
- Game state management
- Save/load system
- Networking (if multiplayer intended)
- Audio system
- Card collection/inventory system