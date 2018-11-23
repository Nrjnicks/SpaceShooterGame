
# Vertical Scroller Shooter [Read Me in progress]

Hi! I am Neeraj S. Thakur, Game Programmer. This repository is dedicated to an open Unity3D multiplayer vertical scroller and shooter game project which is very designer friendly with easy configurable win conditions and async asset loads. In this readme, I have explained all the features of the project. 

![alt text](https://raw.githubusercontent.com/Nrjnicks/SpaceShooterGame/master/ReadmeImages/GameplaySpaceMultiplayer.jpg "GameplaySpaceMultiplayer")

Load game from **GameLoader** scene. (Windows Standalone. Recomended Resolution 1920x1080 or aspect ratio of 16:9)

[Disclaimer]Asset Bundles are already built and  since everything is loaded asyn in the game and asset bundles are platform dependent, please build for specific platform in case of any bundle load error. (Also, let me know so i can make the code more flexible)
# Game Mechanics


## Basic Gameplay and overview

A same-screen multiplayer 2D **vertically scrolling** or **vertical scroller** shoot 'em up video game with top-down perspective view. Goal is to survive different waves of enemy. Player(s) controls a plane and shoots at the waves of enemies which include different types of AIs and missiles.

## Platform
**Windows Standalone**.


## Modes

Game has two modes **single player** and (local) **multi player**. 


## Game Flow

### Game Start
Game starts with user selecting **single player** or **multi player** mode and follow with **Game's Core Loop** 


### Game Core Loop

Player(s) starts the level and get approached by enemies in waves. Player has to satisfy win condition (configurable) to proceed to next level, where the cycle repeats.

![alt text](https://raw.githubusercontent.com/Nrjnicks/SpaceShooterGame/master/ReadmeImages/GameplayMedivalMultiplayer.jpg "GameplayMedivalMultiplayer")


### Game End
Game ends if player(s) survive all waves in different levels (Won) or lose all health (Lost)

## Win Conditions
To complete a level, player(s) has to satisfy a win condition to win current level. A game session (with all levels) may or may necessarily has same kind of win conditions. Win condition per level can be decided by the designers for different levels. Game Currently has 3 different (expandable) types of win conditions-

### 1.  Number of Enemies killed
Player has to kill *number_of_enemy_planes*, to proceed to next level
### 2.  Active Time
Player has to survive waves of enemies for *time_in_seconds* without dying, to proceed to next level
### 3.  Score
Player has to gain *score* by either surviving or killing enemy planes, to proceed to next level

## Controls
Player(s) plane is(are) controlled via Keyboard keys. Based on default settings,  
**Player 1** keys: *W A S D* for movement and *Space bar* to fire.
**Player 2** keys: *Arrow Keys* for movement and *Right Ctrl* to fire.

## Enemies
Game has 3 different types of AI Planes (Easy difficulty, Medium difficulty, Hard difficulty) and similar types of Homing Missiles inflicting different damage.
[Add types image here]

# Designers Friendly

Enough about the game. Now, let's talk about this Unity3D project.
Main focus in my mind while making this project was to make it adaptive, but also predictive.  Almost everything in game is configurable using `ScriptableObject` (Later, I'll show you how by changing few values in ScriptableObjects you can change the whole game). Let's talk about stuff mentioned above  again, but in more Level and Game Designer perspective.

## Platform
**Windows Standalone**.

Only dependency of the project with platform are the keyboard controls and Asset Bundles. Asset Bundles can be built to another platform with just change of a line (explained later) and Keyboard controls are easily configurable and the control system is very expandable (explained later). 

## Modes

Game has two modes **single player** and (local) **multi player**. 

Game currently has 2 players multiplayer, but can be theoretically expanded to *int_max*.  Number of players are requested by `GameManager.cs` to `LevelManager.cs` which checks  `MultiPlayerSOData` for Multiplayer data and spawn players. 
`MultiPlayerSOData` contains list of struct with `PlayerPlaneSOData` and `KeyBoardControlsSO`. Create (explained in details later) as many as you like and enjoy.
Current max for this project is 4 just because I've only set 4 player spawn positions. Feel free to add inside PoolManager Transform. 
[Image]

## Win Conditions
`LevelsSOData` contains a `commonWinCondition` and list of `LevelData` which has `WinCondition` and list of `AIWaveData` values. The 3 win condition types mentioned above has exposed variable and can be created as `ScriptableObject` and assigned for these win conditions.
[Inspector image and right click create SO image]

## Controls
KeyBoard Controls for player is read by `KeyBoardControlsSO` which can be configured in editor (or via script for run-time changes!) and assigned individually for each players.
[right click create SO image]

## Plane Data
Configurable Parameters of generic planes:
- Speed
- Max Health
- Attack Cooldown
- Bullet Speed
- Bullet Strength

AI planes has some extra params
- Min Distance To Attack
- FOV To Attack
- Max Active Time On Screen
- Score Bonus On Kill

You can play around with these parameters to create different type of enemies. By just making assigning  *0* to `minDistanceToAttack`, you can make plane, a homing missile.
Having different data and changeable on fly made level design, creating different kind of enemies (dumb to smart) and setting wave sequence very easy.


# Technical Information
Let's now jump to internal working of the code.
## UML Diagram
![alt text](https://raw.githubusercontent.com/Nrjnicks/SpaceShooterGame/master/ReadmeImages/UML.png "UML")
Wow! looks so simple.

Let's simplify it to get the main flow and we'll come back to this diagram again.

Let's Group MVCs together, State Machines together, Win Conditions together, remove some parent classes and let's assume all assets are already loaded so we dont need to worry about asset bundles.

![alt text](https://raw.githubusercontent.com/Nrjnicks/SpaceShooterGame/master/ReadmeImages/SimplifiedUML.png "SimplifiedUML")
much better! Let's start with the technical flow

### Technical Flow
When the game starts, Player chooses mode of the game and calls `GameManager.cs` to start the game. `GameManager.cs` initializes `HUDController.cs` and `LevelManager.cs` params.
 As you can see in the diagram, `LevelManager.cs` is the core of the project. **Main Game Loop** is also handled by this script. 
 
 On initialization, `LevelManager.cs` initializes `PlayerPlaneController.cs`, `AIPlaneController.cs`, `PoolManager.cs`, `ScoreController.cs`, `SoundManager.cs` and starts the level. `PoolManager.cs` calls `PlaneSpawnManager.cs` at this point and spawns number of players based on game mode.
 
**Game Loop flow**

 On level start,  `PoolManager.cs` again calls `PlaneSpawnManager.cs` starts the wave sequence of enemy planes based on `LevelData` and `LevelsDataSO`. `PlaneSpawnManager.cs` creates an `ObjectPool<Plane>` and sets `Plane.cs` data and controller internally. `LevelManager.cs` also subscribe to onDeathEvent of `Plane.cs`. In case of Enemy death, it updates `ScoreController.cs` and in case of Player death, finish the game session and Resets all managers.
 For every `UpdateFrequency`, `LevelManager.cs` checks for `WinCondtion(ScoreManager)`. If condition is satisfied, level get's complete, parameters for new level are set and the cycle repeats.
 

`PlayerPlaneController.cs` and `AIPlaneController.cs` implements `APlaneController.cs` where `Plane.cs` instance is passed as a dependency to update the movements and check condition to fire. In case of Player, keys from `KeyboardControlsSO` are checked for Input and in case of enemy, `AI State Machine System` is used to check those same conditions (more on this in later section).

`APlaneController.cs` handles onFireBulletEvent, which is subscribed to functions of `BulletPool` to spawn bullets and `SoundManager.cs` to play SFX. `Plane.cs` onDeathEvent is also subscribed to function of `BlastPool.cs` to spawn blast at place of death. `Plane.cs` has onHit event which on collision with bullet or other `IHealthable.cs`, coordinates with `Health MVC`(more on this in later section) to display health (and in case of player, start health bar blink if health is below threshold) and manage onHealthChange and onDeath events.

## Project Contents [In progress]
With the basic flow explained, let me go deeper into the code and explain more systems.
### APlaneController (Dependency Injection) and Command pattern
### AIPlaneState (State Machine)
### Model - View - Controller (MVC)
### WorldSpaceGameBoundary (Singleton)
### ObjectPool<**T**>
### Save Load
### Win Condition
### Asset Bundles
### Editor Programming for Levels Data SO
### Shader
