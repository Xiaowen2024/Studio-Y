using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Meta.WitAi;
using Meta.WitAi.Json;

namespace Meta.WitAi
{
public class spellManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject fireObject; 
    public GameObject iceObject;

    public GameObject windObject;

    public GameObject waterObject;

    public GameObject lightObject;

    public GameObject spellManagerObject;

    public GameObject zombiePrefab;
    private GameObject newZombie;

    private bool skeletonInitialized = false;

    [SerializeField] private Wit wit;
    public UiManager uiManager;
    
    public List<GameObject> poses;
    public List<GameObject> poseCards;

    public List<GameObject> poses2;
    public List<GameObject> poseCards2;
    public int poseIndex;
    public int poseIndex2;
    private float debounce = 3f;
    private float timer = 0f;
    public GameObject book;


    private Dictionary<string, GameObject> spellObjectDictionary = new Dictionary<string, GameObject>();
    void OnEnable()
    {
        fireObject.SetActive(true);
        iceObject.SetActive(true);
        windObject.SetActive(true);
        waterObject.SetActive(true);
        lightObject.SetActive(true);
        spellObjectDictionary["fire"] = fireObject;
        spellObjectDictionary["ice"] = iceObject;
        spellObjectDictionary["storm"] = windObject;
        spellObjectDictionary["water"] = waterObject;
        spellObjectDictionary["light"] = lightObject;
        initiateSkeleton();
        book.SetActive(true);
    }

   


    void Update()
    {
        timer += Time.deltaTime;
    }


    public void PoseSuccess()
    {
        if (debounce > timer)
        {
            timer = 0;
            return;
        }
        poseCards[poseIndex].GetComponent<SpriteRenderer>().color = Color.green;
        poseCards[poseIndex].GetComponent<AudioSource>().Play();
        StartCoroutine(WaitForSeconds(2f));
        
    }
    
    public void PoseSuccess2()
    {
        if (debounce > timer)
        {
            timer = 0;
            return;
        }
        poseCards[poseIndex].GetComponent<SpriteRenderer>().color = Color.green;
        poseCards[poseIndex].GetComponent<AudioSource>().Play();
        StartCoroutine(WaitForSeconds(2f));
        
    }
    
    IEnumerator WaitForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        NextPose();
    }
    
    private void NextPose()
    {
        if (poseIndex <= poses.Count - 1)
        {
            poses[poseIndex].SetActive(false);
            poseCards[poseIndex].SetActive(false);
            poseIndex++;
            if (poseIndex != poses.Count)
            {
                poses[poseIndex].SetActive(true);
                poseCards[poseIndex].SetActive(true);
            }
        }

        if (poseIndex == poses.Count)
        {
            poses[poseIndex].SetActive(false);
            poseCards[poseIndex].SetActive(false);
            uiManager.lessonCompleted = true;
        }
    }
    
    private void NextPose2()
    {
        if (poseIndex <= poses.Count - 1)
        {
            poses[poseIndex].SetActive(false);
            poseCards[poseIndex].SetActive(false);
            poseIndex++;
            if (poseIndex != poses.Count)
            {
                poses[poseIndex].SetActive(true);
                poseCards[poseIndex].SetActive(true);
            }
        }

        if (poseIndex == poses.Count)
        {
            poses[poseIndex].SetActive(false);
            poseCards[poseIndex].SetActive(false);
            uiManager.lessonCompleted = true;
        }
    }
    

    // Update is called once per frame

    // void OnDestroy() {
    //     Scene currentScene = SceneManager.GetActiveScene();

    //     // Iterate through all GameObjects in the scene
    //     foreach (GameObject gameObject in currentScene.GetRootGameObjects())
    //     {
    //         // Destroy each GameObject
    //         Destroy(gameObject);
    //     }
    // }

    public void castSpells(WitResponseNode commandResult){
        string[] spellNames = commandResult.GetAllEntityValues("spell:spell");

        initiateSpells(spellNames);
    }

    public void initiateSpells(string[] spellNames){
        
        if (spellNames.Length == 0){
            return;
        }
        Debug.LogError(spellNames[0]);
        foreach (string spellName in spellNames)
        {
            if (spellObjectDictionary.ContainsKey(spellName))
            {
                // Retrieve the corresponding object and instantiate it, for example
                GameObject spellObject = spellObjectDictionary[spellName];
                GameObject spellCopy = Instantiate(spellObject, newZombie.transform.position, spellManagerObject.transform.rotation);
                spellCopy.transform.SetParent(newZombie.transform, true);
                if (spellName == "fire")
                {
                    poseIndex = 0;
                    poseCards[0].SetActive(true);
                    poses[0].SetActive(true);
                }

                if (spellName == "ice")
                {
                    
                }
                //Invoke()
                //newZombie.destroyZombie();
            }
        }
    }

    public void initiateSkeleton(){
        /*if (skeletonInitialized){
            return;
        }*/
        Debug.LogError("in here");
       newZombie = Instantiate(zombiePrefab, new Vector3(0,-2,10), Quaternion.identity);
       skeletonInitialized = true;
    }
    
    public void delayInitializeSkeleton(){
        Invoke("initiateSkeleton", 3f);
    }

    public void destroySkeleton(){
        Destroy(newZombie);
        Debug.Log("Destroy zombie");
        skeletonInitialized = false;
        //delayInitializeSkeleton();
        uiManager.battleCompleted = true;
    }

    private IEnumerator DeactivateAfterDelay(GameObject obj, float delay)
{
    yield return new WaitForSeconds(delay);

    // Deactivate the object
    obj.SetActive(false);
}
}
}

