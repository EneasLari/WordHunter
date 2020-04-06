using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingTrigger : MonoBehaviour
{
    public TilesInitialization TilesInitializationObject;
    private GameObject[] array;
    private void Start() {
        array = TilesInitializationObject.LettersArray;
        int rand1 = Random.Range(0, array.Length);
        int rand2 = Random.Range(0, array.Length);
        Vector3 pos1 = gameObject.transform.GetChild(0).position;
        Vector3 pos2 = gameObject.transform.GetChild(1).position;
        GameObject instance =Instantiate(array[rand1], pos1, Quaternion.identity);
        instance.transform.Rotate(-90, 0, 0); //Rotate the letter So we can see it right
        GameObject instance2 = Instantiate(array[rand2], pos2, Quaternion.identity);
        instance2.transform.Rotate(-90, 0, 0); //Rotate the letter So we can see it right
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag=="Player") {
            GameObject gobject = gameObject.transform.parent.gameObject;
            TilesInitializationObject.TransformGameObject(gobject);
            int rand1 = Random.Range(0, array.Length);
            int rand2 = Random.Range(0, array.Length);
            Vector3 pos1 = gameObject.transform.GetChild(0).position;
            Vector3 pos2 = gameObject.transform.GetChild(1).position;
            GameObject instance = Instantiate(array[rand1], pos1, Quaternion.identity);
            instance.transform.Rotate(-90, 0, 0); //Rotate the letter So we can see it right
            GameObject instance2 = Instantiate(array[rand2], pos2, Quaternion.identity);
            instance2.transform.Rotate(-90, 0, 0); //Rotate the letter So we can see it right
        }
    }
}
