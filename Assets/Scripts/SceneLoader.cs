using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    [SerializeField] private int sceneIndex;

    private int radius;
    private int loaded;

    private readonly HashSet<Transform> transforms = new HashSet<Transform>();

    private void Awake() {
        transforms.AddRange(FindObjectsOfType<Transform>());
        SceneManager.sceneLoaded += SceneLoaded;
    }

    private void Start() {
        SpawnScene();
    }

    private void SceneLoaded(Scene scene, LoadSceneMode mode) {
        var transformsToMove = FindObjectsOfType<Transform>().Where(t => t.parent == null && !transforms.Contains(t))
            .ToList();

        var x = -radius + loaded % (2 * radius + 1);
        var y = -radius + loaded / (2 * radius + 1);

        foreach (var t in transformsToMove) {
            t.Translate(10 * x, 0, 10 * y, Space.World);
        }

        transforms.AddRange(transformsToMove);
        loaded++;
        if (loaded >= (2 * radius + 1) * (2 * radius + 1)) {
            radius++;
            loaded = 0;

            if (radius <= 3)
                SpawnScene();
        }
    }

    private void SpawnScene() {
        for (var i = -radius; i <= radius; i++)
        for (var j = -radius; j <= radius; j++)
            if (j != 0 || i != 0)
                SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
    }
}