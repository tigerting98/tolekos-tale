# Toleko's Tale : A 2D Bullet Hell Game

## Milestone 2 Update

### Updates/New Features Developed:
* Creation of stages 1 to 4 and new bullet patterns to fit the theme of the stages.
* Boss healthbars and phases.
* Boss “Spellcard” system(term borrowed from Touhou project for ease of communication between the two of us). Essentially the more difficult and more unique boss patterns.
* Dialogue between bosses and the player character.
* Player bombs that deal damage to all enemies on screen and gives a short period of invulnerability.
* Shop menu where the player can buy healing items, extra bomb and bomb upgrades.
* Story scene in between stages that give the player some exposition of the story.
* Balancing of enemy encounters.

**For a better understanding of the updates, please watch our milestone 2 demo video.** 

### The General Architecture of the Code Base:
As of now, we currently have 113 C# scripts and ~7300 lines of code that is written by ourselves apart from the Unity Engine.
The scripts inside the Asset/scripts folders are in charge of the game logic, UI, movement, etc while the scripts inside the waves folders are in charge of generating the wave patterns and the content of the game. We also have a game manager object that is in charge of holding the references to different scripts and prefabs to allow them to interact with each other. The game data object stores the enemy bullets, sprites to be used by the waves scripts. The stats are then set up within the wave scripts. The player data object stores all of the player stats throughout the game.

We have refactored most of the backend scripts to allow for easy extensibility. We have however, not refactored our audio and sound effect system. We will look to improve that by Milestone 3.

### How to install and test the game:
Download the game from: https://drive.google.com/file/d/1tbSc_yT6DndrvMSS59qmOkV1HCFbjpBd/view?usp=sharing
**Or**
https://github.com/tigerting98/tolekos-tale/blob/master/Toleko's Tale.exe

Download and run the .exe file and simply install the game. Play the game!

If you are unable to install the .exe file, you can download the zip file here:
https://drive.google.com/file/d/1BG2fCIZV_zzaaoU2px1Q4n5hXnMo7Fjb/view?usp=sharing
Unzip and run Teloko’s Tale.exe.

Currently, the game takes around 25 minute to play from start to finish of Stage 4.

### Difficulties Encountered:
1. We were originally planning to complete all 6 stages of the game by milestone 2 but due to certain difficulties we were unable to complete this objective.
The first difficulty is the planning stage. A substantial amount of time was spent thinking of fun and unique bullet patterns to use in our stages as we also wanted the patterns to fit the theme of the stage (Water, Earth Fire). The most difficult part of the planning stage was thinking of patterns for the midboss and bosses, as we felt that each of them needed a unique set of patterns to really feel like a boss. 
2. To create the enemy patterns, we first had to model the bullet pattern we had in mind mathematically, and then figure out a way to code it into our game. As a result, as the bullet patterns became more complex throughout the stages, more time was also spent creating them in our game. We also had to think of their movement and their spawn timings. Furthermore, a lot of time was spent balancing encounters to make sure that they are neither too difficult nor too easy.
3. Quite a lot of time was spent refactoring the codebase as well. There were changes that needed to be made in order to accommodate new features such as exploding bullets, lasers, multiple boss health bars, dialogue etc. Also, we had to make our code more efficient due to the large amount of bullets on the screen.
4. Time was also spent on drawing. Most of the sprites are drawn by ourselves with some help from a friend of ours. As both of us are amateur at drawing, it took us quite a while to draw good looking sprites. 
5. We also had to learn other Unity features like animation and particle effects so as to reduce the amount of stuff we had to code out by script.

### Goals for Milestone 3:
1. Completion of stages 5 and 6
  * Stage 5 will take place in the villain’s castle, where the player fights off his 3 apprentices. This stage will feature a boss fight involving 3 bosses that are firing simultaneously
  * Stage 6 is the final confrontation with the villain and concludes the story. 
  * We will be designing some new enemies with new sprites and patterns to populate these stages.
2. Implementation of music throughout the stages
  * For now, our game has no music and it lacks atmosphere, so for the next milestone, we would like to implement music in all our stages.
  * We would most likely be using free music assets found online.
  * Ideally, the music used will be different in each stage and for each boss, however, depending on the resources available, there is a chance of music assets being reused.
3. Implementation of more sound effects
  * So far, there are only minimal sound effects in our game, such as when enemies or the player dies.
  * We would like to include more sound effects for enemy shooting, such as different sounds for different types of bullets.
4. Graphical touch-ups
  * We will be improving the graphics of the game by including more stylized sprites with animations.
  * We will be improving the look and feel of the UI and changing the background images currently used for our menus.
5. Create new difficulty levels
  * We will be making the other 2 difficulty levels by rebalancing enemy stats and modifying some of their bullet patterns.

## Original README
## Proposed Level of Achievement: Artemis

## Motivation 
As avid gamers, we always wanted to make video games. The game we are currently thinking of is a **2D vertical shooter game**, or more commonly known as a **bullet hell game**. Having played games from the franchise **Touhou Project** created by **Team Shanghai Alice**, it inspired us to make a game similar to those found in Touhou Project. The reason we chose a 2D bullet hell game over other genres is that we believe it is relatively easy to develop while still providing a good challenge for us who are new to game development.

This is a video of a stage in Touhou 11. The core mechanics of our game will resemble those found in this game.
https://www.youtube.com/watch?v=pGzWWQvKJOs

## Aim
We hope to make a fun bullet hell game where people of all skill levels can enjoy. The game we intend to design would have differing difficulty levels and provide a good challenge to the more experienced player while also remaining accessible to new players of this genre.

## User Stories
1. As a player, I want to experience a bullet hell game that is challenging and fun.
2. As a player, I want to see different kinds of bullet patterns that require different strategies to overcome.
3. As a player, I want to see my character get stronger throughout the course of the game.
4. As a player, I want to be able to find a difficulty that is suitable for me so that I can enjoy the game regardless of my skill level.

## Project Scope

The project is focused on developing a 2D bullet hell game. A possible extension that we are looking into is to create a website to host the game client as well as scoreboard.

## Detailed Gameplay Features

### 1. Player and Enemy Health
Enemies will have a certain amount of **Hitpoints (HP)** and different projectiles will carry different damage values. The player character will also have HP, which can be upgraded through the course of the game. When the player character is hit by enemy bullets, he will lose some of his HP. **When the player’s HP reaches 0 (zero), the game ends**. 

The player can then choose to quit, or restart from the beginning of the stage. Some enemies, especially the bosses, may have projectiles that instantly kill the player character and thus they are forced to dodge them, instead of simply tanking through it with sheer HP.

The player initially has 100HP and this can be upgraded throughout the game.

### 2. Focus
The player can hold down a key ('Shift' by default) to **Focus**. Focusing slows down the movement of the player character and makes visible the character's hitbox, allowing the player to have more control over their character in order to squeeze through narrow gaps between enemy bullets.

Focusing also changes the shooting pattern of the player character.

### 3. Elemental Magic

There will be a simple **elemental system** where both enemies and the player can shoot projectiles of different elements. Depending on the element that is being used, the characters will take different amounts of damage. There will be 3 elements: Fire, Water, and Earth. 

The damage affinities will be as follows (x->y means x is strong against y):
**Fire -> Earth -> Water -> Fire**

Furthermore, the element currently being used will determine the spread pattern for the player’s bullets.

### 4. Player Upgrades and Special Abilities
In between stages of the campaign, the player would have access to shops where they can upgrade their HP and damage.
There will also be changes to their shooting pattern, such as adding a wider spread or increasing their rate of fire.

### Controls (Keyboard, can be configured in the setting)

Arrow keys - movement

Z- Normal attack

X - Switch element (Fire/Water/Earth)

Shift - Focus, slowing down movement to allow for finer control.

## Website Hosting
We plan to create a website for our game. The website will include the game client, information, and any potential game updates and announcements. It will also be used to host player highscores.

## Technology Stack
1. Unity - to create the actual game.
2. Asperite- to create sprites and backgrounds.
3. PHP - to develop the website.

## Project Log
https://docs.google.com/document/d/1NV8Q0bUViDfeDqzW5LkirybYbUMsip9ERNo5xfECZhc/edit?usp=sharing

## Link to GDD
https://docs.google.com/document/d/1xsbC9BXN0kCata-k_R-Pm64Y5zuUKliexnhEEttQlng/edit?usp=sharing

## Link to Milestone 1 Demo Video
https://www.youtube.com/watch?v=FzrcNzVW3xI&feature=youtu.be
