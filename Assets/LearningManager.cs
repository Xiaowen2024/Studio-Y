using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningManager : MonoBehaviour
{
    public List<GameObject> poses;
    public List<GameObject> poseCards;
    public int poseIndex;
    public UiManager uiManager;
    private float debounce;
    private float timer;


    void Start()
    {
        debounce = 3f;
        timer = 0f;
    }
    void Update()
    {
        timer += Time.deltaTime;
    }
    

    private void OnEnable()
    {
        poseIndex = 0;
        poseCards[0].SetActive(true);
        poses[0].SetActive(true);
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
    
    IEnumerator WaitForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        NextPose();
    }
}
