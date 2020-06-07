using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimLine : MonoBehaviour {
    public GameObject DistanceGO;
    private LineRenderer Line;
    private GameObject playerGO;
    private bool isDrawing;

    private void Start() {
        Line = GetComponent<LineRenderer>();
        playerGO = GameObject.FindGameObjectWithTag("Player");
        isDrawing = false;
    }

    private void Update() {
        if (isDrawing) {
            Line.SetPosition(0, playerGO.transform.position);
            Line.SetPosition(1, DistanceGO.transform.position);
        }
    }

    public void StartDrawing() {
        gameObject.SetActive(true);
        isDrawing = true;
    }
    public void StopDrawing() {
        isDrawing = false;
        Line.SetPosition(0, Vector3.zero);
        Line.SetPosition(1, Vector3.zero);
        gameObject.SetActive(false);
    }
}
