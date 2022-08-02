using System.Collections.Generic;
using UnityEngine;

public interface IAssetsProvider
{
    public GameObject Instantiate(string path);
}