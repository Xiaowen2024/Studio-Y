using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombie : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject zombieObject;
    private GameObject zombieInitiated;
    void Start()
    {
        initializeZombie();
    }

    // Update is called once per frame
    void Update()
    {
        
        moveZombie();
    }
    
    public void initializeZombie(){
        //zombieInitiated = Instantiate(zombieObject, new Vector3(0,0,8), Quaternion.identity);
        zombieInitiated.transform.LookAt(new Vector3(0,0,0));
        zombieInitiated.SetActive(true);
    }

    public void destroyZombie(){
        Destroy(zombieInitiated);
    }

    public void moveZombie(){
        zombieInitiated.transform.position = Vector3.MoveTowards(zombieInitiated.transform.position, new Vector3(0,0,0), 0.05f);
    }


}
