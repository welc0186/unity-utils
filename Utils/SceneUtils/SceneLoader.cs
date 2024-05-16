using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Alf.Utils.SceneUtils
{
    /// <summary>
    /// Use this basic helper for loading scenes by name, index, etc.
    /// </summary>

    public class SceneLoader : PersistentSingleton<SceneLoader>
    {
        // Default loaded scene that serves as the entry point and does not unload
        private Scene m_BootstrapScene;

        // The previously loaded scene
        private Scene m_LastLoadedScene;

        // Properties
        public Scene BootstrapScene => m_BootstrapScene;

        // MonoBehaviour event functions

        private void Start()
        {
            // Set scene 0 as the Bootloader/Bootstrapscene
            m_BootstrapScene = SceneManager.GetActiveScene();
        }

        // Methods

        // Load a scene non-additively
        public void LoadSceneByPath(string scenePath, bool additive = false)
        {
            if(additive)
            {
                StartCoroutine(LoadAdditiveScene(scenePath));
                return;
            }
            StartCoroutine(LoadScene(scenePath));
        }

        // Coroutine to unload the previous scene and then load a new scene by scene path string
        private IEnumerator LoadScene(string scenePath)
        {
            if (string.IsNullOrEmpty(scenePath))
            {
                Debug.LogWarning("SceneLoader: Invalid scene name");
                yield break;
            }
            Debug.Log("Loading scenePath: " + scenePath);
            yield return UnloadLastScene();
            yield return LoadAdditiveScene(scenePath);
            
        }

        // Load a scene by its index number (non-additively)
        public void LoadScene(int buildIndex)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(buildIndex);

            if (string.IsNullOrEmpty(scenePath))
            {
                Debug.LogWarning("SceneLoader.LoadScene: invalid sceneBuildIndex");
                return;
            }

            SceneManager.LoadScene(scenePath);
        }

        // Method to load a scene by its index number (additively)
        public void LoadSceneAdditively(int buildIndex)
        {
            StartCoroutine(LoadAdditiveScene(buildIndex));
        }

        // Reload the current scene
        public void ReloadScene()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }

        // Load the next scene by index in the Build Settings
        public void LoadNextScene()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex + 1);
        }

        // Unload the last scene (go "back" from a Demo scene)
        public void UnloadLastLoadedScene()
        {
            StartCoroutine(UnloadLastScene());
        }

        // Unload by an explicit path
        private void UnloadSceneByPath(string scenePath)
        {
            Scene sceneToUnload = SceneManager.GetSceneByPath(scenePath);
            if (sceneToUnload.IsValid())
            {
                StartCoroutine(UnloadScene(sceneToUnload));
            }
        }

        // Coroutine to load a scene asynchronously by scene path string in Additive mode,
        // keeps the original scene as the active scene.
        private IEnumerator LoadAdditiveScene(string scenePath)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Additive);

            while (!asyncLoad.isDone)
            {
                float progress = asyncLoad.progress;
                yield return null;
            }

            m_LastLoadedScene = SceneManager.GetSceneByPath(scenePath);
            if(!m_LastLoadedScene.IsValid())
                m_LastLoadedScene = SceneManager.GetSceneByName(scenePath);
            SceneManager.SetActiveScene(m_LastLoadedScene);
        }

        // Coroutine to load a Scene asynchronously by index in Additive mode,
        // keeps the original scene as the active scene.
        private IEnumerator LoadAdditiveScene(int buildIndex)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(buildIndex);
            yield return LoadAdditiveScene(scenePath);
        }


        // Coroutine to unloads the previously loaded scene if it's not the bootstrap scene
        // Note: this only works for one scene and does not create a breadcrumb list backwards. Use UnloadSceneByPath if
        // needed.
        private IEnumerator UnloadLastScene()
        {
            if (!m_LastLoadedScene.IsValid())
                yield break;

            if (m_LastLoadedScene != m_BootstrapScene)
                yield return UnloadScene(m_LastLoadedScene);
        }

        // Coroutine to unload a specific Scene asynchronously
        private IEnumerator UnloadScene(Scene scene)
        {
            // Break if only have one scene loaded
            if (SceneManager.sceneCount <= 1)
            {
                Debug.Log("[SceneLoader: Cannot unload only loaded scene " + scene.name);
                yield break;
            }

            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(scene);

            while (!asyncUnload.isDone)
            {
                yield return null;
            }
        }

        // Logs the scene path for a single loaded scene
        public static void ShowCurrentScenePath()
        {
            string scenePath = SceneManager.GetActiveScene().path;
            Debug.Log("Current scene path: " + scenePath);
        }

        // Logs the scenes paths for all currently loaded scenes
        public static void ShowAllScenePaths()
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                Debug.Log("Scene " + i + " path: " + scene.path);
            }
        }
    }

}

