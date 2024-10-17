


# Project Primero

*Game Design Document*

## Contents

Purpose

Concept

Mechanics

Character Types

Health System

Rock, Paper, Scissors Event

Modes

Scale

## Purpose

This goal of this project is two-fold: to create my first original game from start to finish, eventually making it public and to act as the framework for a larger adaptation of the same *type* of game. In essence, this is a trial for a more ambitious and full featured game.

## Concept

This game will be a 2D, competitive, multiplayer, brawler. The game will feature 3-character types with different play styles. Each character will have the same standard type of attacks but will play differently according to their respective style (i.e. consider the primary attack of a tank character to the primary attack of a rogue character). Primary gameplay will involve players fighting each other in attempt to knock each other out and/or for a higher objective(s) (still to be determined). The style of the game will be inspired by Rock, Paper, Scissors and use real photographs for the environments and sprites.




### Key Features:

- Game will be controller-centric, but be playable with keyboard as well

- Game will feature custom artwork, sound, and animations

- 3 character types with play styles that reflect their shape and material

- Characters will have both melee and projectile attacks

- Characters will have special offensive and defensive moves

### Mechanics

A 2D physics system will be used for the game and will be semi-realistic (gravity, collisions, exaggerated movements, double jumps, etc.).

### **Character Types**

All characters will have their own playstyle but possess the same fundamental qualities. This makes it easy to fine tune each character according to their physical characteristic. Abilities will build on these traits and further develop each character’s uniqueness.

####  Fundamental Physical Traits:

- Mass

- ~~Volume~~

- Movement Force

- Jump Force

- Double Jump Force

- Max Speed

#### ROCK

*The Rock character is synonymous with the traditional *Tank* archetype.

##### Passive Moves:

- Jump: --

- Double Jump: --

##### Potential Attacks:

- Pebble Gun: shoots small bits of itself from its ‘mouth’

  - Has the potential to ‘charge up’ as well

- Heavy Lunge: a dash attack that knocks the enemy backwards (can be charged)

- Slam: a vertical, airborne attack that does damage depending on the height from the ground

- Crystal Shard: projects a large crystal shard upward

##### Potential Defensives:

- Diamond Deflect: Significantly reduces damage of melee attacks and deflects projectile attacks

#### SCISSORS

*The Scissor character will be agile in movement and possess quick, precision attacks. 

##### Passive Moves:

- Jump: blade flip

- Double jump: helicopter spin

##### Potential Attacks:

- Windmill: performs a violent, windmill-like spin while in place

- Blade Swipe: fast, single strike attack

- Point Drop: a strong, vertical, downward attack performed in air, point down; the result of a miss is the player getting stuck in the ground for a period of time proportional to the height dropped from

- Boomerang Attack: launches one of its blades out at an enemy and returns
  
  - Consider whether character can move while blade is out

- Blade Throw and Strike: launches one of its blades in specified direction and then strikes with the other where the launched one landed (performed in mid-air on single jump)

- Snip: opens blades and closes when hitting enemy or after a predefined duration

##### Potential Defensives:

- Pointed Deflect: deflects melee attacks and inflicts damage to enemies that move/dash into it.

#### Paper

*The Paper character is the most dynamic, taking many shapes when performing actions. It is consequently the weakest.

##### Passive Moves

- Jump: 

- Double Jump: turns into airplane and flies an arced path in specified direction

##### Potential Attacks

- Spit Ball: shoots spit balls at a sustained rate

- Stick: Hurls itself at a target and briefly sticks to them, slowing them down and paralyzing them; can be countered with rapid button pressing from enemy player

- Ninja Star Loop: transforms into a ninja star that quickly loops around and strikes any players in its path

- Sword Slash: transforms into a sword and performs a slash attack

- Spike Roll: transforms into spike ball and has the ability to roll back and forth injuring enemy players it collides with

##### Potential Defensives:

- Evade: sticks to ground and moves beneath other players.

## Health System

I want to avoid a traditional health system (0-100) and implement something that is both more dynamic and possibly even visual. In my personal opinion, Nintento’s *Super Smash Bros.* (SSB) ‘health’ system is the perfect model for a dynamic one. However, I don’t want to completely rip off that model and am looking for possibly a fusion of the two (assuming I can still keep it relatively simple). Some possible systems:

- Percent based with health steal: essentially SSB system with a mechanic that allows for reducing the percentage

- Traditional with health steal: traditional health system with a mechanic that allows for reducing health

- Traditional with shield: traditional health system after a shield is destroyed (ala Halo). The shield concept allows for many different implementations and the ability to be dynamic.

- Other


## **Rock, Paper, Scissors Event**

This will pose as the most unique game mechanic and make the game more than just RPS *themed*. The general idea is to prompt an actual game of RPS (possibly modified) among all players after a particular trigger. Upon completion of the event, the winner will receive some sort of a bonus, or the losers will receive some sort of a negative bonus, or both. Below are some potential options:

### Outcomes

- Losers are ejected and lose a life

- Winner receives power up in the form of:

  - special/ultra move

  - perk (defensive/offensive/both)

  - extra life

  - other

- Losers receive power down in form of:

  - negative perk (defensive/offensive/both) 

  - other

- Roulette: combination of all bonuses that is chosen randomly

- Tiered Bonuses: a combination of all bonuses where different bonuses are classified and ‘unlocked’ when a certain tier is reached. The bonuses are progressed as each round of a single game of RPS is finished. The longer the game, the more ‘severe’ the bonus (my favorite).

### Event Triggers

The more important matter in regards to the RPS event is how it will be triggered. In theory, the event will be the driving force of the game as in, players will either *work toward* this event or *look forward* to it, as it has the ability to change the game drastically to their favor. However, it has to be determined whether this will be environment or player. Below are some potential ideas:

#### ENVIRONMENTAL:

- Predefined times: every X amount of time, a game of RPS is prompted to players

- Random times: random time chosen within a range

#### PLAYER:

- Joint ‘fight’ meter: all players’ actions are summed and when the bar is full, a game is prompted

- Individual ‘fight’ meter: individual player’s actions are summed and when the bar is full, a game is prompted

- An object representing the RPS trigger occasionally floats around the level and is initiated when a player obtains/destroys it (ala SSM orb)

#### Timing

With these potential mechanics in mind, another factor that needs to be considered is when the prompt for the RPS event will occur. Two options:

- Instantly: as soon as a meter reaches its max or an object representing the prompt is obtained/destroyed, the RPS event is triggered and a game begins

- Player controlled: ‘’ ‘’ ‘’, the RPS event is triggered by a player when they want

The final aspect to be considered is how the event is reset. For any universal object or meter, it is reset in a traditional way; meter set to 0, object spawn timer set to X. 

If individual player meters are chosen, then there are 2 possibilities for a reset:

- The player that used their RPS event has *only* their meter reset

- If *any* RPS event occurs, all meters are reset (favorite)

#### Cadence

To make the RPS event more engaging, players must follow the “Rock, Paper, Scissors, says…” cadence using their controllers. This not only mimics the natural cadence of the real game (using the fist and hand), but also can have consequences (TBD) if done in the wrong order with improper timing. An example using the Xbox 360 controller would be:

“Rock, Paper, Scissors, says: SHOOT!”
      LT      RT         Lb        Rb         Y/X/B

where Y/X/B could be the selection for Rock, Paper, or Scissors, respectively.

## Modes

TBD

## Game Scale

Given that the main characters in the game are Rock, Paper, and Scissors, the size of environments and players should reflect a world that is that of handheld objects. It will make sense to create a fundamental unit and define player and environment size based on this unit. This easiest way to define this unit is by making it a fraction of one of the characters sizes. The Paper character is to be the most circular and posses the most symmetric dimensions. This will make for the best reference:

Paper character dimensions = 1 x 1 Fundamental unit = 1





 





 
