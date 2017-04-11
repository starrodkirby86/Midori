
Midori
------

Midori is a script library extension for the storyboard editor software [storybrew](https://github.com/Damnae/storybrew). Its main intentions are to promote rapid development of elaborate visual effects within storybrew by offering generalized calculation methods, data types, and other features.

Features
--------
The library includes...

 - **Motion**
    - Includes a set of popular sinusoidal-based parametric equations to generate more elaborate movement patterns for particles.
    - The path data type. Use this to generate a set of interpolated points based off a motion trajectory. Allows conjoining of multiple paths as well. The type has a set of points that allows flexible usage for a particle. Transformation of a path is also possible to create more elaborate motions (including translation, rotation, and scale).
 - **Images**
    - Image effects, such as generating the necessary information to create a set of particles for a dissolve effect, or stripping an image into horizontal and vertical chunks to create a wipe transition.
 - **Color**
    - Color adjustment methods inspired by [Sass](http://sass-lang.com/).
    - Color interpolation, similar to using ``Lerp``.
    - Create a ``Color4`` type from an HSB, HSV, or hex code.
    - A palette data type to ease storage of colors. Load from an .ACO file supporting an RGB or HSB colorspace.
    - Coming soon: Gradients.
 - **File**
    - File management and creation methods used to help facilitate local creation of files from storybrew easier.
 - **Unit Testing**
    - Basic unit testing is possible by adding ``void`` parameter-less methods with ``Assert`` cases. Output seen in a log, with error tracing for failed test cases.
