# Toleko's Tale : A 2D Bullet Hell Game

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
Detailed Gameplay Features

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

|S/N|Task|Date|Ting Xiao(hrs)|Aloysius(hrs)|Remarks|
|:---:|:---:|:---:|:---:|:---:|:---:|
|1|Proposal Update|12/5/20|2|2|-|
|2|Poster/Video Making|13/5/20|5|5|-|
|3|Learning Git|15/5/20|3|3|-|
|4|Learning Unity|17/5/20|4|4|-|
|5|Programming at home|19/5/20|2|2|Created basic game structure|
|6|Programming at home|20/5/20|2|2|Implemented player and enemy movement|
|7|Programming at home|21/5/20|2|2|Implemented bullets|
|8|Programming at home|22/5/20|2|2|Implemented health bars|
|10|Refactoring code|23/5/20|1|0|-|
|11|Refactoring code|25/5/20|1|0|-|
|12|Programming at home|26/5/20|1|0|Added an EXP system|
|13|Programming at home|27/5/20|1|0|Added an elemental system|
|14|Mentor Meeting|27/5/20|0.5|0.5|Met with Prof Zhao to discuss our game ideas and receive feedback|
|15|Programming at home|28/5/20|1|0|Modified the waves for demo|

