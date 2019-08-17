using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    private Camera cam;
    public float initCamSize = 5;
    public float minCamSize = 3;
    private float playerDistanceInit;

    void Awake() {
        cam = GetComponent<Camera>();
        cam.orthographicSize = initCamSize;
    }

    void Start() {
        playerDistanceInit = GameManager.inst.spawnPositions[1].position.x - GameManager.inst.spawnPositions[0].position.x;
    }

    void Update() {
        try {
            Vector3 p1 = GameManager.inst.currentPlayer[0].transform.position, p2 = GameManager.inst.currentPlayer[1].transform.position;
            transform.position = new Vector3((p1.x + p2.x) / 2, (p1.y + p2.y) / 2 + 1, -10);
            float distanceX = Mathf.Abs(p1.x - p2.x), distanceY = Mathf.Abs(p1.y - p2.y);
            float camsize = Mathf.Max(distanceX, distanceY) / playerDistanceInit * initCamSize;
            cam.orthographicSize = Mathf.Max(minCamSize, camsize);
        } catch {
            transform.position = Vector3.zero;
            cam.orthographicSize = initCamSize;
        }
    }
}
