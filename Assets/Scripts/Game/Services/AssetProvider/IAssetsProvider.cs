using UnityEngine;

public interface IAssetsProvider
{
    public GameObject Instantiate(string path);
}