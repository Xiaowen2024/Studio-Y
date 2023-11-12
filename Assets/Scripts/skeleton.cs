using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeleton : MonoBehaviour
{
    // Start is called before the first frame update
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
        
        this.transform.LookAt(new Vector3(0,-3,1));
        
    }
    
    public void moveZombie(){
        this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(0,-3,1), 0.01f);
    }

}
