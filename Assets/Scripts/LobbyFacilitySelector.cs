using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class LobbyFacilitySelector : MonoBehaviour {
    Camera cam;
    // Start is called before the first frame update
    void Start() {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update() {
        if (Mouse.current.leftButton.wasPressedThisFrame) {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider != null && hit.transform.CompareTag("Facility")) {
                    print("hit " + hit.collider.name);
                }
                else {
                    print("hit Nothing");
                }
            }
        }
    }
}
