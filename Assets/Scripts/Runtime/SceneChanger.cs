using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneChanger
{
    public static async Awaitable ChangeScene(Scene scene)
    {
        var currentScene = SceneManager.GetActiveScene();
        await SceneManager.LoadSceneAsync((int)Scene.Loadscreen, LoadSceneMode.Additive);

        await Awaitable.NextFrameAsync();
        await SceneManager.UnloadSceneAsync(currentScene);
        await Awaitable.NextFrameAsync();

        var loadScene = SceneManager.LoadSceneAsync((int)scene, LoadSceneMode.Additive);
        loadScene.allowSceneActivation = false;
        while (loadScene.progress < 0.9f)
            await Awaitable.NextFrameAsync();

        loadScene.allowSceneActivation = true;
        while (!loadScene.isDone)
            await Awaitable.NextFrameAsync();

        await Awaitable.NextFrameAsync();
        await SceneManager.UnloadSceneAsync((int)Scene.Loadscreen);
    }

}

public enum Scene
{
    Title = 0,
    Loadscreen = 1,
    GameScene = 2,
}