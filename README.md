# ARVolumeVFX
 A toolkit to create vfx based on the environment and humen 3D data (retrived from lidar sensor on AR enabled devices) in AR. This is a noncommercial tool to explore the possibility of visual effects in AR environment, be prepared to unexpected jittery or unstable results on mobile devices. 
 
## Environment Requirements
   - Unity 2021.2 or later
   - Universal Rendering Pipeline 
   - VFX Graph package
   - AR Foundation + ARKit (ARCore is not tested, but theoretically it will work) 
   - AR enabled devices with Lidar sensor(IPad pro, IPhone 12 Pro etc.)

## How to Install
   1. Install and config AR foundation correctly follow [this instruction](https://docs.unity3d.com/Packages/com.unity.xr.arfoundation@4.2/manual/index.html)
   2. Compile one of the sample scenes from https://github.com/Unity-Technologies/arfoundation-samples and make sure it runs successfully on your device before moving onto the next step.
   3. Pull the **toolkit** branch from this repo. Build the example scene and try it out. 

## Components
   - **LidarDataProcessor** : to process environmental and human data (eg. depth/ stencil), prepared vfx binder ready data. how to use: Add an empty game object to your scene, and add LidarDataProcessor to it, then drag the respective components to the parameter list from your scene. 
  
   ![image](https://user-images.githubusercontent.com/18374192/142716620-bc106738-4e9b-4ee5-be61-48b756db3aa5.png)
   - **VFXLidarDataBinder** : to bind the AR scene data to VFX graph. how to use: Add visual effect and vfx binder components to your vfx gameobject, click the "+" button from vfx binder and select Lidar Data. (in VFXPropertyMenu, select the property you will use in your VFXGraph) 
   
   ![image](https://user-images.githubusercontent.com/18374192/142717317-f017bd17-8054-41e2-a1cc-0c2a3bd4b562.png)

## VFX Subgraph tools
   - **Environment Mesh Position** : read vertices from EnvironmentMesh, set positions for particles 
   
   ![image](https://user-images.githubusercontent.com/18374192/142717504-a08d9ba1-2499-4565-a409-b750381dd80b.png)
   - **Human Froxel** : set particle positions to human (refer to DemoExample VFXGraph to see how to use)

   ![image](https://user-images.githubusercontent.com/18374192/142717673-8e74d049-7883-4e2b-aab5-8cfa01bae463.png)

   - **Kill Nonhuman** : remove particles that are outside of human stencil (refer to DemoExample VFXGraph to see how to use)

   ![image](https://user-images.githubusercontent.com/18374192/142717712-b318b097-5462-4b80-9078-9cc8228b85bb.png)


   
## Feature Demonstration 
  - **Environment**

https://user-images.githubusercontent.com/18374192/142727704-144bfc21-8a9e-492a-a482-7c4a21e82bc7.mp4


https://user-images.githubusercontent.com/18374192/142727748-9d7a7c5a-b8ae-4197-8050-e5d9097aa95c.mp4

  - **Human**

https://user-images.githubusercontent.com/18374192/142727769-b8595802-7458-47a5-8802-a7c10fb85871.mp4




## More examples coming up...
