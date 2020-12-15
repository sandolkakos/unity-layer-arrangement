# Layer Arrangement.

Utility to arrange layers similarly to Photoshop and PowerPoint.

## How to use
Just copy the link below and add it to your project via Unity Package Manager: [Installing from a Git URL](https://docs.unity3d.com/Manual/upm-ui-giturl.html)
```
https://github.com/sandolkakos/unity-layer-arrangement.git
```

## RectTransform
A selected RectTransform can be moved up or down inside its Canvas by pressing some shortcuts.

## Shortcuts:
- Bring Forward:   Ctrl + ]
- Bring Backward:  Ctrl + [
- Send to Front:   Ctrl + Shift + ] 
- Send to Back:    Ctrl + Shift + [

## TODO list:
-  Add that behavior to SpriteRenderer, so that we can change the SortingOrder inside its SortingLayer.
- Avoid moving a RectTransform to inside a parent with LayoutGroup.
