using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigurePage : MonoBehaviour
{
    public int PageNr;
    public PageDependentBinding[] Targets;

    [ContextMenu("Apply Now")]
    void Start()
    {
        foreach (var target in Targets) {
            target.PageNr = PageNr;
        }

    }

}
