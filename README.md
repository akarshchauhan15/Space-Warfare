# Space Warfare

## Quickfire Instructions
- Download the latest verison of Space Warfare from releases page.
- The game is portable, hence, no installation needed, just open and begin playing.
- Click anywhere or press Enter to start.
- Read or skip the initial guide provided.

## Images
![Start](<img width="1823" height="1064" alt="Start" src="https://github.com/user-attachments/assets/caad4cf9-cabc-4ee4-b909-3338888e9c52" />)
![Playing](<img width="1859" height="1034" alt="Play" src="https://github.com/user-attachments/assets/a2dcd748-c76b-4efe-9f47-10b051bd1ce0" />)
![Hovered](<img width="1853" height="1041" alt="Hover" src="https://github.com/user-attachments/assets/db02c4d1-451a-45fc-9050-90fff1e70d13" />)

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
