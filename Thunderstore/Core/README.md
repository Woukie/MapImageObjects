
# MapImageObjects

You can finally add images from the web to your maps! Images can be placed and configured extensively in the editor with the editor mod (MapImageObjectsEditor).

⚠ [WARNING] This mod uses urls to load images while you play the game! ⚠
- If you are a map maker, host the files somewhere where the hosters don't mind you doing this.
- Also, make sure the links you use are permanent!

## Features

- Collision
- Editor Tools
- Shadows
- Lots of object properties!
- Caches images as they load

## Installation

The mods will automatically install in the [Thunderstore](https://thunderstore.io/) mod manager.

There are two mods: One for map-makers and one for users. (If you are a map-maker, you will need both). Both mods can be found in the releases on the [GitHub](https://github.com/Woukie/MapImageObjects).

If you do install manually, make sure to also install the related dependencies depending on which one you go for:

#### MapImageObjects
- unbound
- mapsextended

#### MapImageObjectsEditor
- unbound
- mapimageobjects
- mapsextended
- mapsextended.editor

## FAQ

#### Weird shadows/collision

The shadows are determined by the collision, and Unity generates collision based on the texture. If the texture has a low resolution (like with pixel art), Unity will generate lower-resolution collision. It's a pain, but the solution is to upscale the image to a higher resolution or create the collision manually using invisible objects.

#### How do I share my maps?

[Here's](https://docs.google.com/document/d/1f0bZvolXIGhVRpIURijiVFN2k6p7bZQlzpfVuIE-HFw/edit#heading=h.1r8wfrbpupek) a good place to learn

#### Help there is bug

Submit an issue on GitHub!

## Acknowledgements

 - [willuwontu](https://github.com/willuwontu), I yoinked the color property code from **WillsWackyMapObjects**
 