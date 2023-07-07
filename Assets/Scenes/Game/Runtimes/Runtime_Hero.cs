using UnityEngine;

[System.Serializable]
public struct Runtime_Hero {
    public Transform transform;
    public SpawnPoints spawnPoints;

    [System.Serializable]
    public struct SpawnPoints {
        public Transform dron;
        public Transform mine;
        public Transform tower;
    }
}
