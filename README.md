# pacman2

All Documentation: https://drive.google.com/drive/folders/1Zj5EUIQo2ihr41GHv_0HIZzzK_a2FYb7?usp=sharing(https://drive.google.com/drive/folders/1Zj5EUIQo2ihr41GHv_0HIZzzK_a2FYb7?usp=sharing)

## **Game Title:** CP5609 Game - Expanded Pacman Game

# Contents

[**Game Title:** CP5609 Game - Expanded Pacman Game 1](#_Toc136357375)

[**Team Members**** ：** 1](#_Toc136357376)

[**Player Control Scheme** 1](#_Toc136357377)

[**Unique Scripting Implementations** 2](#_Toc136357378)

[**Known Issues / Bugs** 14](#_Toc136357379)

[**Game Assets List** 15](#_Toc136357380)

[Project GitHub link 15](#_Toc136357381)

[**Branches:** 15](#_Toc136357382)

## **Team Members**** ：**

| **Group member** | **Student ID** |
| ---------------- | -------------- |
| Ke Zhang         | 14121550       |
| SiKai Ye         | 13794420       |
| Shannon Sim      | 14329473       |

## **Player Control Scheme**

| **Player Input**                 | **Function**                                                 |
| -------------------------------- | ------------------------------------------------------------ |
| **Input Device:** Keyboard       |                                                              |
| **W** key or **Up Arrow** key    | While playing a level, changes the direction the player character is travelling in to **up**. |
| **A** key or **Left Arrow** key  | While playing a level, changes the direction the player character is travelling in to **left**. |
| **S** key or **Down Arrow** key  | While playing a level, changes the direction the player character is travelling in to **down**. |
| **D** key or **Right Arrow** key | While playing a level, changes the direction the player character is travelling in to **right**. |
| **Escape** key                   | While playing a level, **pauses** the game and opens the **pause menu UI**. |
| **Input Device:** Mouse          |                                                              |
| **Mouse Movement**               | While viewing a menu, moves the **mouse cursor**.            |
| **Left Click**                   | While viewing a menu, used to interact with **menu buttons**. |

## **Unique Scripting Implementations**

**Movement and Pathfinding**

Movement Class

The **Movement** class handles movement and pathfinding for the player character and the ghost NPCs. To achieve this it uses four methods: **SetDirection()**, **Occupied()**, **Update()**, and **FixedUpdate()**.

**Occupied(Vector2 direction)**

![](RackMultipart20230530-1-ibs0z3_html_c1d63073dd21d54.png)

The **Occupied()** function uses Unity's physics engine to create a box raycast in a specified direction to check if that position is occupied by an object assigned to the **obstacleLayer** (level walls). The method then returns a boolean depending on if a collision was detected or not.

**SetDirection(Vector2 direction)**

![](RackMultipart20230530-1-ibs0z3_html_79018659fcdca361.png)

The **SetDirection()** method is called to set the **direction (Vector2)** variable, which determines the _current_ movement vector of the player/ghosts (i.e., up, down, left, or right), and it also sets the **nextDirection (Vector2)** variable which is the _next__queued_ movement vector.

First, the method checks if the direction specified in the **direction** parameter is occupied using the **Occupied()** function and it also checks if the **forced** boolean parameter. If no obstruction is found, or if the **forced** parameter is _true_ then the current travel direction is set to the one in the **direction** parameter, and the **nextDirection** variable is reset.

If an obstruction is found, and the **forced** parameter is _false_, then the current travel direction is unchanged and the **direction** parameter is instead stored in **nextDirection**.

![](RackMultipart20230530-1-ibs0z3_html_f8df201ea43986f8.png)

The **SetDirection()** method is called by the **Update()** method. The **Update()** method is inherited from Unity's **MonoBehaviour** class, and runs every in-game frame. In the **Update()** function, if **nextDirection** is (0,0) (i.e., there is no movement input) then nothing occurs. However, if there is input then the **SetDirection()** method is called and the **nextDirection** variable is passed.

Because player input is used to update **nextDirection** instead of setting the direction of travel directly this allows for the player to "queue up" a desired direction, meaning that they can input a direction they wish to travel in and have the character change direction only when it becomes possible to do so (i.e., there is no obstruction in that direction).

![](RackMultipart20230530-1-ibs0z3_html_aca06daef59673b1.png)

The actual position of characters is updated in the **FixedUpdate()** method. Like the **Update()** function, the **FixedUpdate()** method is inherited from Unity's **MonoBehaviour** class but instead runs at a specific fixed rate. It is mainly used for updates relating to the physics engine. Inside the **FixedUpdate()** method the character's existing position is input, then the desired translation is calculated from the product of the **direction** vector, **speed** float, **speedMultiplier** float, and **Time.fixedDeltaTime**. **Time.fixedDeltaTime** is a Unity method which returns the time passed since the last time **FixedUpdate()** was called, multiplying the **translation** by this value ensures that movement speed is independent of frames per second/computer performance.

Finally, the character's position is updated by the physics engine, based on the **translation** vector.

**Ghost AI Behaviours**

The ghost adversaries in the game have their behaviour controlled by four scripts: **GhostHome** , **GhostScatter** , **GhostChase** , and **GhostFrightened**. Each of these scripts inherits from the **GhostBehaviour** abstract class. Ghosts can only have one behaviour active at any given moment. Ghosts change behaviours based on time elapsed, and player actions, although a ghost's initial behaviour can be specified.

_GhostHome_

GhostHome contains two transform variables: inside and outside, and 4 methods: OnEnable(), OnDisable() OnCollisionEnter2D(), and ExitTransition().

OnEnable() is called when the GhostHome script is enabled, and it stops all coroutines.

![](RackMultipart20230530-1-ibs0z3_html_e0a3dfe416bc8052.png)

OnDisable() Called when the GhostHome script is disabled, it will initiate the exit transition coroutine if the ghost object is still active.

![](RackMultipart20230530-1-ibs0z3_html_11d04c204000d3d.png)

OnCollisionEnter2D() is called when the ghost collides with walls, it takes a collision as input, and will reverse the ghost's direction, thus resulting in a bouncing effect.

![](RackMultipart20230530-1-ibs0z3_html_769a11ceb7934eba.png)

ExitTransition() is a coroutine for the exit transition of the ghost from the home, it will turn off movement while we manually animate the position, then animate the ghost transform from starting point to the exit point outside the ghost home. Follow by picking a available direction to let the ghost start moving.

![](RackMultipart20230530-1-ibs0z3_html_5d027d42b39de4ad.png)

_GhostScatter_

GhostScatter contains two methods: OnDisable() and OnTriggerEnter2D().

OnDisable() Called when the GhostScatter script is disabled, it will then enable the GhostChase behaviour.

![](RackMultipart20230530-1-ibs0z3_html_1e65231c2f6bbed4.png)

OnTriggerEnter2D() is called when the ghost collides with a node, it takes in the node's collider and will check if GhostScatter is enabled and the ghost is not in frightened mode. If both are true, it will randomly select a direction to move on that is not the same direction where it comes from.

![](RackMultipart20230530-1-ibs0z3_html_b37cbf63012ae34f.png)

_GhostChase_

Contain two methods except from inherited methods: OnDisable() and OnTriggerEnter2D().

OnDisable() called when GhostChase behaviour is disabled, it will activate GhostScatter behaviour.

![](RackMultipart20230530-1-ibs0z3_html_9af16b314a32c901.png)

OnTriggerEnter2D() is called when the ghost collides with a node, it takes in the node's collider and will check if GhostScatter is enabled and the ghost is not in frightened mode.

If both are true, it will iterate all available directions of the node and select the direction given closest distance toward the pacman.

![](RackMultipart20230530-1-ibs0z3_html_99e741da3ae7c93e.png)

_GhostFrightened_

The **GhostFrightened** behaviour is activated when the player character eats a power pellet.

It contains eight methods:

**Enable():**

![](RackMultipart20230530-1-ibs0z3_html_44db5760429147d0.png)

The **Enable()** method changes the ghost's sprite (appearance) to its blue frightened appearance for a number of seconds, specified by the **duration**** (float) **parameter. When half the specified duration has elapsed the** Flash()** method is called to change the ghost's appearance to its "flashing" form.

**Disable():**

![](RackMultipart20230530-1-ibs0z3_html_1eb140ee5a11127b.png)

The **Disable()** method changes the ghost's appearance back to its normal appearance/colour.

**Eaten():**

![](RackMultipart20230530-1-ibs0z3_html_9e0b83f634afbfe9.png)

The **Eaten()** method is called when the ghost collides with a player whilst it is in the frightened state. The ghost's sprite is changed to only show its eyes, its position is changed back to its "home" position, and its behaviour is changed to the **Home** behaviour.

**Flash():**

![](RackMultipart20230530-1-ibs0z3_html_147f7131e6db5129.png)

The **Flash()** method changes the ghost's sprite to an animated "flashing" sprite.

**OnEnable():**

![](RackMultipart20230530-1-ibs0z3_html_db4b4aa38f2ba7b8.png)

The **OnEnable()** method is a Unity method that is called when the script it is in is enabled by the game engine. In this case, the **OnEnable()** method will be called whenever the **GhostFrightened** behaviour is activated (i.e., when the player eats a power pellet). The **OnEnable()** method ensures the ghost's frightened/blue animated sprite is playing, halves the ghost's movement speed, and sets the **eaten** boolean to _false_.

**OnDisable():**

![](RackMultipart20230530-1-ibs0z3_html_3a6e7482b98143eb.png)

The **OnDisable()** method is a Unity method that is called whenever the script that it is in is disabled by Unity. In the GhostFrightened behaviour, the **OnDisable()** method is called when the ghost reverts to a "normal" state from its frightened state (when the effects of a power pellet expire). The ghost's movement speed is reset to normal, and the **eaten** boolean is also reset to _false_.

**OnTriggerEnter2D():**

![](RackMultipart20230530-1-ibs0z3_html_ae8120dba7d50d44.png)

The **OnTriggerEnter2D()** is a Unity function that is called when a collision occurs between the trigger collider of a game object with the script and another object with a trigger collider. In this case, the **OnTriggerEnter2D()** is called whenever a ghost collides with a node object (Node objects are invisible game objects used by the AI for navigation). Everytime a collision between a ghost and a node occurs, the method gets all possible movement directions from the node and selects the direction that will take the ghost further from the player (fleeing behaviour). This is achieved by working out which direction has the furthest distance from the player character. Based on the results of this heuristic a new direction is chosen.

**OnCollisionEnter2D():**

![](RackMultipart20230530-1-ibs0z3_html_4c5df249430edeab.png)

The **OnCollisionEnter2D()** is a Unity function that is called when a collision occurs between the collider of a game object with the script and another object with a collider. In this case, the **OnCollisionEnter2D()** is used to detect when a ghost in its "frightened" state collides with the player character. If the collision occurs then the **Eaten()** method is called as the ghost is eaten by the player.

**Powerups**

SpeedyPowerPellet Class

SpeedyPowerPellet Class inherits PowerPellet Class has two methods: Awake() and Eat(). Awake() sets duration to 8 seconds, and Eat() invokes the SpeedyPowerPelletEaten method in GameManager Class.

InvinciblePowerPellet Class

Similar to SpeedyPowerPellet Class, InvinciblePowerPellet Class also inherits PowerPellet Class has two methods: Awake() and Eat() with the same implementation as SpeedyPowerPellet Class. The only difference is Awake() sets duration to 15 seconds instead of 8 seconds.

GameManager Class

SpeedyPowerPelletEaten(SpeedyPowerPellet pellet)

This method uses an instance of the SpeedyPowerPellet class as input, then plays the speedy animation sequence, followed by increases Pacman's speed multiplier for a designated period and invokes the reset state timer to reset pacman state.

![](RackMultipart20230530-1-ibs0z3_html_d35cd9c007ccb632.png)

private boolean pacmanInvincible

This is a boolean variable belonging to GameManager Class to track Pacman's invincibility.

InvinciblePowerPelletEaten()

This method plays the invincible animation sequence, enables invincibility for 15 seconds, processes the score addition as a regular pellet, and invokes the reset state timer to reset pacman state.

![](RackMultipart20230530-1-ibs0z3_html_dbd57789647d6c9a.png)

When enabling invincibility, boolean pacmanInvincible is set to true, this leads to early return (do nothing) when a pacman has been eaten.

![](RackMultipart20230530-1-ibs0z3_html_da874f028a1d3add.png)

**Sound Effects**

GameManager Class

We have 5 different sound effects for 5 different situations.

![](RackMultipart20230530-1-ibs0z3_html_9b7e9f991889f9d8.png)

1. eatpellet sound effect is activated when a Pellet is eaten.

![](RackMultipart20230530-1-ibs0z3_html_4effb23543f9372d.png)

1. eatpowerpellet sound effect is activated when a Power Pellet is eaten, it would also play the soundtrack sound effect at the same time.

![](RackMultipart20230530-1-ibs0z3_html_5495e41fba9b7952.png)

1. lostlife sound effect is activated when the pacman is eaten by ghosts.

![](RackMultipart20230530-1-ibs0z3_html_93e21c46b4eb0b36.png)

1. ghostscream is is activated when any ghost is eaten by the pacman during the power up.

![](RackMultipart20230530-1-ibs0z3_html_5a070cfbcfcedd0a.png)

**New Levels**

Level 1

Level 1 is a simple tutorial for new players, it contains no monsters and includes both text-based and visual-based instructions to guide new players how to control the pacman, as well as exit and complete levels.

![](RackMultipart20230530-1-ibs0z3_html_b38addcae9830942.png)

Level 2

Level 2 is a classic pacman map with moderate difficulty, this level contains the original features such as pacman, ghost, pellets, sound effects, and power pellets. Some of our new features are not implemented at this level.

![](RackMultipart20230530-1-ibs0z3_html_b4bafb7b706e58d8.png)

Level 3

Level 3 provided a harder map that is almost 3 times larger compared to level 2, with new powers pellets such as SpeedyPowerPellet and InvinciblePowerPellet. The player required a longer time in order to consume all pellets to complete the level.

![](RackMultipart20230530-1-ibs0z3_html_7cc22173decf5352.png)

## **Known Issues / Bugs**

| **Bug/Issue Description**                                    | **Cause(s)**                                                 | **Potential Solution(s)**                                    |
| ------------------------------------------------------------ | ------------------------------------------------------------ | ------------------------------------------------------------ |
| The Player/Ghosts can become temporarily stuck on the walls in the third level. | The raycast method used by the _Movement.cs_ script is not able to properly detect collisions for more elaborate level geometry. | The simplest solution is to modify the level geometry in the third level to be simpler. |
| Alternatively, the raycasting method on the _Movement.cs_ script can be modified to increase its ability to handle complex levels. |                                                              |                                                              |
| When a player picks up a power up pellet while another power up is active this can cause unexpected behaviour in terms of the duration of the powerups. | This issue occurs because the methods that control the duration and timing of the powerups override one another. | Rework powerup timers to not conflict.                       |
| Additionally, add a UI or other visual to indicate to the player when an invulnerability or speed powerup is about to end. |                                                              |                                                              |

##


## **Game Assets List**

| **#**                         | **Asset File Name**                                          | **Asset type** | **Source**                                                   | **Licence / other info** |
| ----------------------------- | ------------------------------------------------------------ | -------------- | ------------------------------------------------------------ | ------------------------ |
| 1                             | png files in ./Assets/Sprites Folder (include pacman, monster, wall, pellet and power pellets) | Sprites        | Zigurous's Unity Pacman Tutorial[https://github.com/zigurous/unity-pacman-tutorial/tree/main/Assets/Sprites](https://github.com/zigurous/unity-pacman-tutorial/tree/main/Assets/Sprites) |                          |
| Free for non-commercial uses  |                                                              |                |                                                              |                          |
| 2                             | All basic C# script that not include new features            | Script         | Zigurous's Unity Pacman Tutorial[https://github.com/zigurous/unity-pacman-tutorial/tree/main/Assets/Scripts](https://github.com/zigurous/unity-pacman-tutorial/tree/main/Assets/Scripts) |                          |
| Free for non-commercial uses  |                                                              |                |                                                              |                          |
| 3                             | All audio resources                                          | Audio          | Audio resources provided by Voicemod [https://tuna.voicemod.net/search/sounds?search=pacman](https://tuna.voicemod.net/search/sounds?search=pacman) |                          |
| Free for non- commercial uses |                                                              |                |                                                              |                          |
| 4                             | New scripts for improved features (include completely original scripts in \Created Scripts and modified scripts in \Pacman Tutorial Scripts) | Script         | Project's repository[https://github.com/technical-zebra/pacman2](https://github.com/technical-zebra/pacman2) | Self-created             |

## Project GitHub link

[https://github.com/technical-zebra/pacman2](https://github.com/technical-zebra/pacman2)

### **Branches:**

main - used for major version release

branch\_latest - used for unity game development
