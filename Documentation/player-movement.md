# Player Movement Outline

### Abstract

The below document outlines a proposal for how to handle player movement in unity

## General Movement and reference

**Instant (Rigidbody.velocity)**

> Overrides player velocity vector to be instant. 

* Left and Right

**Force Based (Rigidbody.AddForce + : Applies a gradual push or instant burst ()**

> Applies a gradual push or a instant burst using impulse

* Jump + double check

Double jump should only reset when the ground is touched.

Proposal: use a raycast under the character to detect when the ground is touched. 

* Dash
Should reset after touching the ground

* Falling

While pressed down, the user has an increased mass that causes them to fall faster. The player should be able to actively swap this on and off instantly.

> [!NOTE]
> This should only really take effect if the player is actively not touching anything (i.e in the air)

* Wall Jumping

> [!WARNING]
> This mechanic can be very easily exploited. Should implement a wall jump consumption strategy. The player can only wall jump after either condition is met. Either they touch the ground or they jump off a different wall. I wonder if unity has a unique id system we can hash to get the ladder done? 

### Visual Indicators

* Dash should keeps its current streak. There should also be a visual icon in the top left that is either white or gray depending on state

* Double jump should also have a visual indicator (even if for testing purposes). 

* When landing on the ground, there should be a particle effect that activates letting the player know they can jump again. 

* When falling heavy, the above particle effect should be greater

* when heavy falling, should have a streak effect going down. Can implement using the particle system.

### Animations

* Idle
* Falling
* Wall sliding
* Wall jumping

### Finite State Representation

...

## Architecture & Design Patterns

### Code Structure

...

### Handling Input

Should use unity's input system to handle input (should work on both controller and keyboard)

