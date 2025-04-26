# Good Architecture.

A GameManagerPrefab is added automatically, thanks to using "[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]" in InitGameManager.
The GameManager is set to DontDestroyOnLoad and checks are made to not have a duplicate.
GameManagerPrefab brings a PlayerData gameobject with its script, to manage the SaveManager (TODO: rename it as SaveManager).
Other gameobjects to be added to GameManagerPrefab are: AudioManager and BattleManager.

Currently there are only 2 scenes: MainMenu and Battle.