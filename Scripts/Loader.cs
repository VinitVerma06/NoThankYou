using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader {

    public enum Scene {
        LoadingScene,
        GameScene,
        MainMenu,
    }

    private static Scene targetScene;

    // Load level with Scene name
    public static void Load(Scene targetScene) {
        Loader.targetScene = targetScene;
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    public static void LoaderCallback() {
        SceneManager.LoadScene(targetScene.ToString());
    }
}
