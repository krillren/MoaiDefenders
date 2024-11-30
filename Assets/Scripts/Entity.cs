using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public bool isGenerating;
    public virtual void StopGenerating()
    {
        isGenerating = false;
    }
}
