# Unreleased
- Updated the editor script for the config to add undo's to the toggles.
- Changed the default config asset path
- Made editor assembly only compile on Unity Editor platform.

# Version 0.2.6  -  28/02/2023

- Made some object fields read only
- Added automatic detection of collections that are open when no collection has been loaded manually. i.e: Automatically detect what collection was open in the previous editor session.
- fixed the scene manager window not loading when opening the project/swapping layouts. 
- fixed some smaller bugs and improved some code.

# Version 0.2.5  -  ??/??/20??

- Step 1. Look at code
- Step 2. Develop
- Step 3. Forget you did anything
- Step 4. Progress
- Step ?. ???????

# Version 0.2.4  -  18/11/2022

- Fixed issue with config editor script always resetting the paths to default values.

# Version 0.2.4  -  17/11/2022

- Fixed issue with config instance not being set when booting

# Version 0.2.3  -  16/11/2022

- Made OnSceneLoad public and accessible 
- Changed name of OnSceneLoad -> OnSceneCollectionLoaded
- Added another event (OnSceneCollectionLoadDebug), now there is two. one which outputs the scene collection data and one void.

# Version 0.2.2  -  14/11/2022

- Fixed scene selection field in the scene manager window 

# Version 0.2.1  -  14/11/2022

- Re arranged SceneManager window
- Refactored MultiSceneToolsConfig
- Refactored MultiSceneLoader.loadDifference()
- Added optional parameter to keep boot scene when unloading scenes.
- Added MultiSceneLoader.loadAdditive()
- Added MultiSceneLoader.OnSceneLoad
    - use OnSceneLoadAddMethod(UnityAction<SceneCollection, collectionLoadMode> unityAction) to add an action
- Added config setting: allowing cross scene references
- Added config setting: toggle OnSceneLoad logging
- Added config setting: SceneCollection Load path
- Added config setting: Boot scene path

# Version 0.1.0

- Initial Upload
