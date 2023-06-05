using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeDelta = 5f;
    void Start()
    {
        StartCoroutine(CreateEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CreateEnemies()
    {
        var cd = new WaitForSeconds(timeDelta);
        while(true)
        {
            CreateEnemy();
            yield return cd;
        }
    }
    void CreateEnemy()
    {
        EventHandler.CallCreateEnemy(this.transform.position);
    }
}
