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
- **Scene Navigation**: File → Open Scene → Assets/Scenes/SampleScene.unity

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
- `CardGenerator.cs`: Fully implemented card generation with rarity-based stat scaling, special stat decay, and rare nightmare transformations
- `StatUtils.cs`: Utility methods for stat validation and categorization

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

**Card Display** (`Assets/Scripts/UiScripts/`):
- `CardDisplay.cs`: MonoBehaviour for displaying card data with TextMeshPro elements, handles nightmare/magic indicators and rarity-based background colors

### Project Structure
```
Assets/
├── Art/              # Card templates (PNG/PSD), UI elements
├── Scenes/           #  SampleScene.unity (main scene),
├── Scripts/
│   ├── Cards/        # Card system implementation
│   ├── Globals/      # Utility classes (AsyncUtils)
│   └── UiScripts/    # UI components and highlighters
├── InitScript.cs     # Scene initialization, generates 100 cards on Start
├── Resources/
│   └── Prefabs/      # CardPrefab.prefab - card UI template
└── Packages/         # Unity Test Framework included
```

## Key Implementation Details

1. **Card Generation Algorithm**:
   - Base rarity starts at 1, increases with 20% chance per level (up to 8)
   - Each rarity level adds 7 stat points distributed randomly
   - Special stats capped at 5 points max
   - 25% chance Special1 decays, 50% chance Special2 decays (redistributed to other stats)
   - 1% chance for nightmare transformation (Armor/Resist → Special3/Special4)

2. **Scene Initialization**: 
   - `InitScript.cs` attached to GameObject in scene
   - Generates 100 cards on Start using `CardGenerator.GenerateCard()`
   - Instantiates `CardPrefab` from Resources/Prefabs/ into libraryGrid

3. **Card Display Colors by Rarity**:
   - Common (1): Black
   - Uncommon (2): Gray  
   - Rare (3): Dark Blue
   - Epic (4): Purple
   - Legendary (5): Red
   - Mythic (6): Orange
   - Divine (7): Gold
   - "Literally hacking" (8): Cyan

4. **IDE Integration**: Project is configured for JetBrains Rider, Visual Studio, and VSCode. Unity automatically generates .csproj files.

## Current Limitations & Missing Features
- Pack opening logic and animations not yet implemented
- No persistent game state or save/load system
- Card collection/inventory management not implemented
- Audio system not configured
- PackOpener.unity scene needs setup