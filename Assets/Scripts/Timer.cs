using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmpText;
    [SerializeField] private float timeToSolve;
    private float timeLeft;
    void Start()
    {
        timeLeft = timeToSolve;
        tmpText.text = "time remaining : " + timeLeft;
        StartCoroutine(UpdateTime());
    }

    // Update is called once per frame
    public IEnumerator UpdateTime()
    {
        while (timeLeft > 0)
        {
            tmpText.text = "time remaining : " + --timeLeft;
            yield return new WaitForSeconds(1);
        }
        //trigger loose
    }
}
