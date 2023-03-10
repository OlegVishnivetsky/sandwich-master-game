using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoManager : MonoBehaviour
{
    [SerializeField] private Transform pageRoot;
    private int pageCount;
    private int currentIndex = 0;

    private void Awake()
    {
        pageCount = pageRoot.childCount;
    }

    public void NextPage()
    {
        UpdatePage(1);
    }

    public void PreviousPage()
    {
        UpdatePage(-1);
    }

    private void UpdatePage(int nextIndex)
    {
        pageRoot.GetChild(currentIndex).gameObject.SetActive(false);
        currentIndex = mod(currentIndex+nextIndex, pageCount);
        pageRoot.GetChild(currentIndex).gameObject.SetActive(true);
    }

    private int mod(int a, int n) 
    {
        return ((a%n) + n) % n;
    }
}
