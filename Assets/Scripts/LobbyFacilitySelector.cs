using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class LobbyFacilitySelector : MonoBehaviour {
    Camera cam;
    LobbyCameraController camController;
    // Start is called before the first frame update
    void Start() {
        cam = GetComponent<Camera>();
        camController = GetComponent<LobbyCameraController>();
    }

    // Update is called once per frame
    void Update() {
        if (Mouse.current.leftButton.wasPressedThisFrame) {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider != null && hit.transform.CompareTag("Facility")) {
                    print("hit " + hit.collider.name);
                    camController.FocusPos = hit.collider.transform.GetComponent<LobbyFacility>().FocusPos;
                }
                else {
                    print("hit Nothing");
                    camController.FocusPos = null;
                }
            }
        }
    }
}
