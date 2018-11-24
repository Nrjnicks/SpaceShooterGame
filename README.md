
# Vertical Scroller Shooter [Read Me: in progress]

Hi! I am Neeraj S. Thakur, Game Programmer. This repository is dedicated to an open Unity3D multiplayer vertical scroller and shooter game project which is very designer friendly with easy configurable win conditions and async asset loads. In this readme, I have explained all the features of the project. 

![alt text](https://raw.githubusercontent.com/Nrjnicks/SpaceShooterGame/master/ReadmeImages/GameplaySpaceSingleplayer.jpg "GameplaySpaceSingleplayer")

Load game from **GameLoader** scene. (Windows Standalone. Recomended Resolution 1920x1080 or aspect ratio of 16:9)

[Disclaimer]Asset Bundles are already built and  since everything is loaded asyn in the game and asset bundles are platform dependent, please build for specific platform in case of any bundle load error. (Also, let me know so i can make the code more flexible)
# Game Mechanics


## Basic Gameplay and overview

A same-screen multiplayer 2D **vertically scrolling** or **vertical scroller** shoot 'em up video game with top-down perspective view. Goal is to survive different waves of enemy. Player(s) controls a plane and shoots at the waves of enemies which include different types of AIs and missiles.

## Platform
**Windows Standalone**.


## Modes

Game has two modes **single player** and (local) **multi player**. 

![alt text](https://raw.githubusercontent.com/Nrjnicks/SpaceShooterGame/master/ReadmeImages/GameplaySpaceMultiplayer.jpg "GameplaySpaceMultiplayer")

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

![alt text](https://raw.githubusercontent.com/Nrjnicks/SpaceShooterGame/master/ReadmeImages/WinConditions.jpg "WinConditions")


![alt text](https://raw.githubusercontent.com/Nrjnicks/SpaceShooterGame/master/ReadmeImages/WinConditionsLevelSO.jpg "WinConditionsLevelSO")

![alt text](https://raw.githubusercontent.com/Nrjnicks/SpaceShooterGame/master/ReadmeImages/WinConditionsDiffLevelSO.jpg "WinConditionsDiffLevelSO")

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
![alt text](https://raw.githubusercontent.com/Nrjnicks/SpaceShooterGame/master/ReadmeImages/GameplaySpaceMultiplayer2.jpg "GameplaySpaceMultiplayer2")
You can play around with these parameters to create different type of enemies. By just making assigning  *0* to `minDistanceToAttack`, you can make plane, a homing missile.
Having different data and changeable on fly made level design, creating different kind of enemies (dumb to smart) and setting wave sequence very easy.


# Technical Information
Let's now jump to internal working of the code.
## UML Diagram
![alt text](https://raw.githubusercontent.com/Nrjnicks/SpaceShooterGame/master/ReadmeImages/UML.png "UML")
Wow! looks so simple.

Let's simplify it to get the main flow and we'll come back to this diagram again.

Let's Group MVCs together, State Machines together, Win Conditions together, combine some abstraction classes and let's assume all assets are pre-loaded so we don't need to worry about asset bundles.

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
### APlaneController (Dependency Injection) 
`APlaneController.cs` is an abstract class which Updates movement direction of the plane, Move it and triggers FireBullet event. 

![alt text](https://raw.githubusercontent.com/Nrjnicks/SpaceShooterGame/master/ReadmeImages/APlaneControllerDI.png "APlaneControllerDI")

This class is implemented by `PlayerPlaneController.cs` and `AIPlaneController.cs` which use conditions statements provided by `KeyboardControllerSO.cs` (Command pattern) and `AIPlaneState.cs` (State Machine) respectively.

### AIPlaneState (State Machine)
We implemented a very simple State Machine.

 - Approach state: Try to follow player (`AIApproachPlaneState.cs`)
 - Attack state: Fire Bullet (`AIAttackPlaneState.cs`)
 - Evade state: Run away from player (`AIEvadePlaneState.cs`)

![alt text](https://raw.githubusercontent.com/Nrjnicks/SpaceShooterGame/master/ReadmeImages/AIStateMachine.png "AIStateMachine")

To explain state transition in words: **Attack**, if in range and not in cooldown. **Evade**, if in cooldown. **Approach**, if not in cooldown and far away.

Internally, more priority is given to Evade state, than Approach, than Attack.
With just these 3 states, and different `PlaneDataSO`, we are able to create various kind of enemies with similar behaviors.

### Asset Bundles

### Model - View - Controller (MVC)
We are using MVC architectural pattern for 3 systems.

 #### Heath System
 ![alt text](https://raw.githubusercontent.com/Nrjnicks/SpaceShooterGame/master/ReadmeImages/HealthMVC.png "HealthMVC")

`Plane.cs` has direct reference to `HealthModel.cs`, which subscribes to onHit(IHealthable). This event callback for every collision trigger, based on **Layer Collision Matrix** set at **Physics2D** settings

![alt text](https://raw.githubusercontent.com/Nrjnicks/SpaceShooterGame/master/ReadmeImages/CollisionMatrix.jpg "CollisionMatrix")

Internally, `HealthController.cs` reduces health from maxHealth by *inflictingDamageAmount* from `IHealthable.cs` interface. 

`HealthModel.cs` sets onHealthChange(currentHealth, maxHealth) event of `HealthController.cs` for onHit(IHealthable). onHealthChange event also gets subscribed by local `HealthView.cs` to display health bar. 

> onHealthChange is also subscribed by `HealthBarBlink.cs` which makes healthbar blink once it reach some threshold value (configurable from prefab)

#### Score System

![alt text](https://raw.githubusercontent.com/Nrjnicks/SpaceShooterGame/master/ReadmeImages/ScoreMVC.png "ScoreMVC")

`LevelManager.cs` initialize `ScoreController.cs` on start of the game session. `ScoreController.cs` updates score based on *updatefrequency* from `ScoreDataSO`. 
`ScoreController.cs` also subscribes to onEnemyKilled and onPlayerKilled from `LevelManager.cs` and updates score based on Enemy Data or Evaluate Final Score to show highscore in `ScoreView.cs`.
List of High Scores are saved using `SaveLoad.cs` class in persistent data folder (for now atleast), and read by `ScoreModel.cs` after game session ends (on player death). `ScoreController.cs` then evaluates and respond to `ScoreView.cs` to show (or not) high score dialog box.

User can type their name in the dialog box to save their high score.

![alt text](https://raw.githubusercontent.com/Nrjnicks/SpaceShooterGame/master/ReadmeImages/HighScore.jpg "HighScore")

[More on Saving in SaveLoad session]

#### Head Up Display (HUD)

![alt text](https://raw.githubusercontent.com/Nrjnicks/SpaceShooterGame/master/ReadmeImages/HUDMVC.png "HUDMVC")

`GameManager.cs` on session start, sets `HUDController.cs` which subscribes itself to different events in `LevelManager.cs` to show and hide UI using `HUDView.cs`. 

### ObjectPool<**T**>
`ObjectPool<T>` is a generic abstract class with type constraint of `Component`. This script can be implemented by any other script to create bool of similar type. Set *objectForPool*, pool size and hit play. On Start(), if the object is set, the script will create a pool which can be managed by the implemented script.

In current project we are using this script for creating pool of **Plane**, **Bullets** and **SpriteRenderer** (Blast). `PoolManager.cs` manages them with level and game events.

![alt text](https://raw.githubusercontent.com/Nrjnicks/SpaceShooterGame/master/ReadmeImages/PoolManager.jpg "PoolManager")

On Start of the game session, `LevelManager.cs` initialize parameters of `PoolManager.cs` which creates pool for these types.
Let's talk about them in detail.
#### Plane Spawn Manager and Plane Pool
`PlaneSpawnManager.cs` has reference to `PlanePool.cs` which creates pool of `(Player)Plane.cs` and `(AI)Plane.cs`. `PlaneSpawnManager.cs` is responsible for spawning waves of enemy for each level. It reads data from `LevelData` starts spawning enemies.

#### Bullet Pool
`BulletPool.cs` is subscribed to `APlaneController.cs`'s onFireBullet(Plane) event and spawns bullet (`Bullet.cs`) at *position*, *speed* and *direction*.

#### Blast Pool
`BlastPool.cs` instantiates pool of SpriteRenderer, whose positions are set at spawn. `BlastPool.cs` is subscribed to onEnemyKilled(Plane) and onPlayerKilled(Plane) event from `LevelManager.cs` and spawns blast on any plane death.

### World Space Game Boundary (Singleton)
`WorldSpaceGameBoundary.cs` is a singleton class which is used by `APlaneController.cs` and `Bullet.cs` to check if it is valid to move to a *position*.

![alt text](https://raw.githubusercontent.com/Nrjnicks/SpaceShooterGame/master/ReadmeImages/WorldSpaceBoundary.jpg "WorldSpaceBoundary")

`WorldSpaceGameBoundary.cs` draws gizmos in Editor mode for bounds (explained below) and is also *[ExecuteInEditMode]* so Level Designers have visual representation of world to work with.

If you'll look at the image above, 
- ![#00FF00](https://placehold.it/15/00FF00/000000?text=+)  **Green**: Points are inside camera view
- ![#FFFF00](https://placehold.it/15/FFFF00/000000?text=+)  **Yellow**: Points outside the camera view but within a range of extra world range.
- ![#FF0000](https://placehold.it/15/FF0000/000000?text=+)  **Red**: Points are inside extra range

`PlayerPlaneController.cs` works inside  ![#00FF00](https://placehold.it/15/00FF00/000000?text=+) green zone, meaning PlayerPlane cannot move beyond camera view.
`AIPlaneController.cs` works inside  ![#FF0000](https://placehold.it/15/FF0000/000000?text=+) red zone, meaning AIPlane can move beyond camera view but not far away.  `AIPlaneController.cs`checks for ![#FFFF00](https://placehold.it/15/FFFF00/000000?text=+) yellow zone when disabling or force killing the AI enemy (or making enemy plane move out of the camera before killing them)

> `WorldSpaceGameBoundary.cs` could have been easily referenced by `GameManager.cs` and used by `APlaneController.cs` instead of Singleton, but my vision was to provide 'A valid world bound' for all objects irrespective of  inter-references for ease of Level Designers. 

### Save Load
`SaveLoad.cs` is used to save a serialized class. It uses binary serialization and save *filename* (default: typeof(Serialized Class) in *path* (default: Application.persistentDataPath folder).
you may create an instance of this class and call `Save<T>()` or `Load<T>` to save or retrieve data.
In current project, this class is used to save score of player.

### Win Condition
As mentioned earlier, my main focus was to make project as designer friendly as possible. 
`LevelManager.cs` has a reference to `AWinCondition.cs` which is abstract class to be implemented to check for win condition. `AWinCondition.cs` inherits from `ScriptableObject` meaning WinConditions can be created as an asset by the designers

![alt text](https://raw.githubusercontent.com/Nrjnicks/SpaceShooterGame/master/ReadmeImages/WinConditionCreate.jpg "WinConditionCreate")


![alt text](https://raw.githubusercontent.com/Nrjnicks/SpaceShooterGame/master/ReadmeImages/WinConditions.jpg "WinConditions")

These win conditions are set in `LevelsDataSO`
for common win condition for all levels:
![alt text](https://raw.githubusercontent.com/Nrjnicks/SpaceShooterGame/master/ReadmeImages/WinConditionsLevelSO.jpg "WinConditionsLevelSO")

for different win condition for all levels:
![alt text](https://raw.githubusercontent.com/Nrjnicks/SpaceShooterGame/master/ReadmeImages/WinConditionsDiffLevelSO.jpg "WinConditionsDiffLevelSO")

to create a totally new type of wincondition, inherit `AWinCondition.cs` and implement `ConditionToWin(ScoreController)` function, create SO and there you go just drag it on `LevelsDataSO`

#### Editor Programming for Levels Data SO
For ease of Designers, I also wrote a custom inspector editor script for LevelsDataSO

![alt text](https://raw.githubusercontent.com/Nrjnicks/SpaceShooterGame/master/ReadmeImages/LevelsSOData.jpg "LevelsSOData")

If designers wants a common win condition, they can toggle the variable and inspector format changes. Designers can have different win conditions for all levels with toggle of a button. Internally, we are checking for the same variable to set level win conditions for individual levels.

Also, Enemy wave information is shown in a single line so it is easier to read. AIPlaneData to spawn, number of this data enemies in the wave and spawn time difference
![alt text](https://raw.githubusercontent.com/Nrjnicks/SpaceShooterGame/master/ReadmeImages/Wave.jpg "Wave")

### Shader
We Custom created 2 shaders. One `ScrollableBackground` for vertical scrolling, which adds an offset to uv data with some speed attached to Background Obj in scene
![alt text](https://raw.githubusercontent.com/Nrjnicks/SpaceShooterGame/master/ReadmeImages/ScrollableBackground.jpg "ScrollableBackground")

and the other is `HealthBarBlink` for blink effect on UI element. This materials gets set by `HealthBarBlink.cs` on the Health Bar Image on player(s) as soon as health becomes less than some threshold
![alt text](https://raw.githubusercontent.com/Nrjnicks/SpaceShooterGame/master/ReadmeImages/HealthBarBlink.jpg "HealthBarBlink")