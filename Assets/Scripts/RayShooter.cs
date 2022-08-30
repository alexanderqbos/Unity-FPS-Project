using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
    private Camera cam;
    
    // [SerializeField]
    // private int aimSize = 16;
    
    // Start is called before the first frame update
    void Start()
    {
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;

        cam = GetComponent<Camera>();
    }

    // void OnGUI() {
    //     GUIStyle style = new GUIStyle();
    //     style.fontSize = aimSize;

    //     float posX = cam.pixelWidth / 2;
    //     float posY = cam.pixelHeight / 2;

    //     GUI.Label(new Rect(posX, posY, aimSize, aimSize), "*", style);
    // }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 point = new Vector3(cam.pixelWidth / 2, cam.pixelHeight /2, 0);
            Ray ray = cam.ScreenPointToRay(point);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
                if(target != null)
                {
                    target.ReactToHit();
                }
                else {
                    StartCoroutine(ShpereIndicator(hit.point));
                }
            }
        }
    }

    private IEnumerator ShpereIndicator(Vector3 hitPosition)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        sphere.transform.position = hitPosition;

        yield return new WaitForSeconds(1);

        Destroy(sphere);
    }
}
