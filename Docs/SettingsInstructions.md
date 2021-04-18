## Instructions for setting up and modifying all the allowed variables  

There is a list of files that define the behaviour, visuals and sounds of some aspects of the game. 
They are in the format of ScriptableObjects that all can be duplicated and substitue the current ones in order to test gameplay or game feel.

- MapSettings
- ShipSettings
- ShipWeaponSettings
- AsteroidSettings

`IMPORTANT: while game can be launched from the GameScene and be able to play the level, it is advised to always launch from the MainMenu scene.`

### MapSettings
`Located in Assets/!Data/Map`

`Assignable to GameScene's GameManager GameObject`

MapSettings controls the map difficulty and other values. Also holds a reference for the AsteroidSettings that will use the map.

- Player Lives: The amount of max lives the player has until game over.
- Max/Min Spawn Delay: The spawn delay sets the timer for the spawn of a new asteroid in seconds. The higher, the easier.
- Time Until Max Difficulty: Time in seconds to reach the maximum difficulty according to the SpawnRateCurve.
- Spawn Rate Curve: Defines the SpawnDelay across the TimeUntilMaxDifficulty. This allows for a non-linear difficulty and to change it across time. 
See how the `Map_Hard` example curve is tweaked in a way that causes waves of difficulty increase and decrease.
- Asteroids Config: Reference to AsteroidSettings.
- Round Start Countdown Seconds: The initial delay for each round start.


### AsteroidSettings
`Located in Assets/!Data/Units/Asteroids`

`Assignable to MapSettings ScriptableObject`

AsteroidSettings controls behaviour, visuals, FX and SFX. Also holds a reference to the Asteroid Prefab.

- Min/Max Speed: A random between these two constants will define the speed of each asteroid.
- Number Of Childs: How many asteroid childs will be instantiated after killing a big or medium asteroid.
- Scores: Values of the score for each type of asteroid.
- Sprites: Set of sprites that will use each type of asteroid. On each spawn they will be assigned randomly between each type option.
- Audio: Clip sounds for each event.
- FX: Particles for each event.


### ShipSettings
`Located in Assets/!Data/Units/Ships`

`Assignable to a Ship Prefab (e.g. PlayerShip in Assets/!Prefabs`

ShipSettings controls behaviour, ShipWeaponSettings, SFX and FX of the ships.

- Rotation Speed: Turn speed of the ship.
- Max Velocity: Top speed of the ship.
- Acceleration: Speed increment until Max Velocity.
- Ship Weapon Settings: Reference to ShipWeaponSettings.
- Audio: Clip sounds for each event.
- FX: Particles for each event.


### ShipWeaponSettings
`Located in Assets/!Data/Units/Ships`

`Assignable to ShipSettings in  Assets/!Data/Units/Ships`

ShipWeaponSettings controls behaviour, SFX and FX of the ship's bullets.

- Attack Delay: Minimum time in seconds between each bullet.
- Bullet Speed: Constant speed of the bullet.
- Life Time: Time in seconds for the life of the bullet if it doesn't impact anything.
- Fire Sound: SFX for shooting.
- Bullet: Bullet Prefab reference (`PlayerShip in Assets/!Prefabs`)


### Other Variables
- `GameScene > GameManager: Force Mobile Input`: UI for mobile input will show if this is enabled.
- `PlayerShip Prefab > God Mode Enabled`: Ship can't get hit.
- `GameScene > MapBounds: Bounds Displacement:` The bounds of the map set the spawn points of the asteroids and the teleport edges of the map. 
At value 0, map bounds are equal to the screen size. If incremented, bounds are bigger (far away) than the screen size.
