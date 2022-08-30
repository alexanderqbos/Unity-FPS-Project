using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    private int value = 2;
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();
        if (player != null)
        {
            // apply first-aid and remove the item
            player.FirstAid(value);
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotation = Vector3.up * 180 * Time.deltaTime;
        transform.Rotate(rotation, Space.World);

    }
}
