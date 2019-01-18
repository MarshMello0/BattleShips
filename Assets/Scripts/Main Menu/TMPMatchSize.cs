using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TMPMatchSize : MonoBehaviour
{
    [SerializeField]
    private float targetSize;

    [SerializeField]
    private TextMeshProUGUI[] texts;

    private void Start()
    {
        foreach (TextMeshProUGUI text in texts)
        {
            text.fontSize = targetSize;
        }
    }
}
