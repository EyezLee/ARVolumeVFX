# ARVolumeVFX
 A tool to bridge VFX Graph with processed depth, color, human silhouette and environment data retrieved from Lidar sensor of an AR enabled device. It enables users to play VFX Graph in AR world to its maximum possibility, in actual volumetrix data rather than screen spaced post processing AR old fashion.
One device solution, no any sort of connection needed! (early access, under construction) 
 
## Environment Requirements
   - Unity 2021.2 beta 16 / Unity 2022.1 alpha 12
   - Universal Rendering Pipeline (to be able to use VFX Graph on AR devices, more info https://portal.productboard.com/unity/1-unity-platform-rendering-visual-effects/c/97-urp-support-compute-capable-devices-only-lit-particles-and-various-features-and-fixes) 
   - VFX Graph package
   - AR Foundation + ARKit
   - AR enabled devices with Lidar sensor(IPad pro, IPhone 12 Pro etc.)

## Components
   - **LidarDataProcessor**: A component to process occlusion, silhouette and env texture received from devices.
   - LidarDataShader: using together with LidarDataProcessor together to help process textures.
   - **LidarDataBinder**: A customized VFX attribute binder derived from Unity built-in VFX attribute binder. To hook processed Lidar data with VFX Graph for vfx to use.

## Installation Instruction
   - Drag LidarDataProcessor prefeb into your scene
   - Add LidarData from VFX attribute binder componenet on your VFX object
   - DONE! 
   
## Example
https://user-images.githubusercontent.com/18374192/137863804-2e6d2db7-0a59-4e6f-944c-f552e1dd8ad5.mov

_(debug example of using human silhouette alone with depth data from ipad in VFXGraph)
_

## Resouce
   https://github.com/keijiro/Rcam2
