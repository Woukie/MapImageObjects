
# MapImageObjects

You can finally add images from the web to your maps! Images can be placed and configured extensively in the editor with the editor mod (MapImageObjectsEditor).

### Place *Any* Image!

![image](https://github.com/Woukie/Image/blob/main/demoimage.gif?raw=true)

### Change image properties

![colour](https://github.com/Woukie/Image/blob/main/democolour.gif?raw=true)

### Watch me get destroyed by a bot

![fight](https://github.com/Woukie/Image/blob/main/demofight.gif?raw=true)

⚠ **[WARNING]** This mod uses *urls* to load images while you play the game! ⚠
- If you are a map maker, host the files somewhere where the hosters don't mind you doing this.
- Also, make sure the links you use are permanent!

## Features

- Collision matches image
- Physics images
- Breakable images
- Editor tools
- Shadows match image
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

#### My transparent image turned into a silhouette!

Loading images with transparency (the 'A' channel) lower than 128 do this. I have no idea why.

#### Weird shadows/collision

The shadows are determined by the collision, and Unity generates collision based on the texture. If the texture has a low resolution (like with pixel art), Unity will generate lower-resolution collision. It's a pain, but the solution is to upscale the image to a higher resolution or create the collision manually using invisible objects.

#### How do I share my maps?

[Here's](https://docs.google.com/document/d/1f0bZvolXIGhVRpIURijiVFN2k6p7bZQlzpfVuIE-HFw/edit#heading=h.1r8wfrbpupek) a good place to learn. But the gist is to zip up all your maps together with a [manifest, readme and an icon](https://rounds.thunderstore.io/package/create/docs/), then upload to [Thunderstore](https://rounds.thunderstore.io/package/create/).

#### Help there is bug

Submit an issue on GitHub!

## Acknowledgements

 - [willuwontu](https://github.com/willuwontu), I yoinked the color property code from **WillsWackyMapObjects**
 
