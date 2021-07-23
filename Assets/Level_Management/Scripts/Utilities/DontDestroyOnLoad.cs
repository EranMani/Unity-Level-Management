using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private void Awake()
    {
        // We can do this operation only for objects at the root level of the hierarchy, which means one that has no parent transforms
        transform.SetParent(null);
        Object.DontDestroyOnLoad(gameObject);
    }
}
