# Space Warfare

## Quickfire Instructions
- Download the latest verison of Space Warfare from releases page.
- The game is portable, hence, no installation needed, just open and begin playing.
- Click anywhere or press Enter to start.
- Read or skip the initial guide provided.

## Images

<img width="1816" height="1021" alt="Start" src="https://github.com/user-attachments/assets/d20d2e6d-0b91-4836-be70-7fcb2052f2a8" />
<img width="1859" height="1034" alt="Play" src="https://github.com/user-attachments/assets/78049e55-1a0c-48e7-a3bb-78a6bd790615" />
<img width="1853" height="1041" alt="Hover" src="https://github.com/user-attachments/assets/56c3ee0e-628b-490f-b21d-de085e2ebeb5" />

# Game Design Document

## Introduction

### Summary
Tile Breaker is a retro style arcade like game based on Breakout (1976) by Atari with features and graphical improvements.

### Inspiration
**Space Invaders** by Tomohiro Nishikado

### Platform
Windows and Linux

### Genre
Arcade and Retro

### Target Audience
People who want to experience a Space Invaders like game again.

### Development Software
- **Godot 4** for game engine.
- **Visual Studio** for IDE.
- **Inkscape** for vector graphics.
- **LibreSprite** for art.

## Concept

### Gameplay overview
The game consists of a grid of enemies and a player controlled spaceship. The player has to eliminate enemy forces and simultaneously dodge hostile bullets.

Game declares victory if all enemies are eliminated, but, if the player dies, the game loses.

### Navigating UI
To view the menu panel, hover the mouse near left edge of screen. Or simply use keyboard shortcuts for specific actions.

#### Shortcuts

Toggle Fullscreen/Windowed - F
Mute/Unmute game audio - M
Pause/Resume - Escape
View previous scores - G
Information page - F1
Quit game - ALT + F4

## Game Experience

### UI
The games uses similar design language that emits a feel of original retro/acrade themed games.
The game features proximity panels, that appear when cursor is hovered near edges of the window, inspired by Windows 8.

### Controls
The game supports keyboard, mouse, and gamepad. However, mouse is only used for navigating UI.

### Resetting Preferences
Deleting the settings.ini will reset the settting. This goes same for previous scores by deleting scores.json

## Game Mechanics

### Drops
If a enemy is hit it may have a possibility to drop to pickable effect.

#### Drop Types
- Health (H+) - Increases health by one.
- Bunker Repair (B+) - Fixes currently present bunkers by one level.
- Slowdown (Sl) - Enemies move slowly.
- Freeze (Fz) - Completely stop enemies for a few seconds.

### Mothership
Occasionaly a red ship appears above all enemies and travells across the screen in either direction. Shoot it down for extra points.
