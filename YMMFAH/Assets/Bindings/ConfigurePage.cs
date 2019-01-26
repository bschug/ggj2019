using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigurePage : MonoBehaviour
{
    public int PageNr;

    void Start()
    {
        foreach (var target in transform.GetComponentsInChildren<PageDependentBinding>()) {
            target.PageNr = PageNr;
        }

    }

}
