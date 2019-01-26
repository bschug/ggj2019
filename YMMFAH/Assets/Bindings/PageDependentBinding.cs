using UnityEngine;
using System.Collections;

public abstract class PageDependentBinding : MonoBehaviour
{
    public LetterDefinition Letter;
    public int PageNr;

    protected LetterPage Page => Letter.Pages.Length > PageNr ? Letter.Pages[PageNr] : null;

}
