using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawMovement : MonoBehaviour {
    public GameObject linePrefab;
    public GameObject currentLine;

    public LineRenderer lineRenderer;

    public List<Vector2> fingerPositions;

    private GameObject playerGO;
    bool move = false;
    int i;
    Vector2 currentTarget;
    void Start() {
        playerGO = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            GameManager.instance.DoSlowMotion();
            CreateLine();
        }
        if (Input.GetMouseButton(0)) {
            Vector2 tempFingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(tempFingerPos, fingerPositions[fingerPositions.Count - 1]) > .1f) {
                UpdateLine(tempFingerPos);
            }
        }
        if (Input.GetMouseButtonUp(0)) {
            GameManager.instance.StopSlowMotion();
            startMovement();
        }
        if (move) {
            if (Vector2.Distance(playerGO.transform.position, currentTarget) > 0) {
                playerGO.transform.position = Vector2.MoveTowards(playerGO.transform.position, currentTarget, 65 * Time.deltaTime);
            } else {
                i++;
                if (fingerPositions.Count > i) {
                    currentTarget = fingerPositions[i];
                } else {
                    move = false;
                }
            }
        }
    }

    void CreateLine() {
        currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        fingerPositions.Clear();
        fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        lineRenderer.SetPosition(0, fingerPositions[0]);
        lineRenderer.SetPosition(1, fingerPositions[1]);
    }


    void UpdateLine(Vector2 newFingerPos) {
        fingerPositions.Add(newFingerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos);
    }

    void startMovement() {
        i = 0;
        currentTarget = fingerPositions[i];
        move = true;
    }
}
