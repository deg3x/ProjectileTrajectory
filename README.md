# Projectile Trajectory
Projectile trajectory is a common algorithmic concept in gameplay programming. This project is my approach to this
problem, implemented for 3D space usage using the unity 3D engine. It's design is straightforward and can be
easily integrated to any project (at least it can...). Followingly is an explanation of the projects main concepts:

## Project Structure
There are three main components in order for this solution to be effective:
* A gun (or the source of the projectile in general), using the ray camera (explained below) in conjunction
    with the mouse position on screen to aim. A very simple implementation is already provided.
* A line renderer acting as the trajectory. Just create a line renderer and attach the `Trajectory.cs` script
    to it. The rest are handled by the script. You can adjust any option fits your needs from the __LineRenderer__
    component and change the material.
* A camera. We can use the main camera for this but I prefer to create a new one for this purpose, because
    we can modify it separately. Since all we need is its rotation data we can lower its depth to -100 and even move
    it away from the scene so it doesn't render anything and increases our polygon count for no reason.

## Dependencies
* If the projectile is being shot using the unity engine physics system, you need to add force with the 
    velocity change force mode, as in the provided script:
    ```C#
    g.GetComponent<Rigidbody>().AddForce(g.transform.forward * bulletVelocity, ForceMode.VelocityChange);
    ```
* The trajectory works for position coordinates that the `transform.position.y` is above 0. It also works best for coordinates
    not way above 0. This happens because the arc created starts from a given point until it reaches zero height _(y == 0)_.
    If you must have your projectile source positioned in great height and the trajectory starts looking flat you have to
    increase the **resolution** script variable value from the editor until it looks smooth (also increases the overhead of the 
    calculations). In any other case you have to hack your way around.

## Other project info
* Unity version used: 2018.3.3f1

## Resources
* [Wikipedia][1] page of the physics/math behind the calculations.
* A similar implementation for a 2D trajectory by [Board To Bits Games][2] youtube channel.

[1]: https://en.wikipedia.org/wiki/Projectile_motion
[2]: https://www.youtube.com/watch?v=iLlWirdxass