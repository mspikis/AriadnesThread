using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadGroceryScene()
    {
        LoadingScreen.Instance.Show(SceneManager.LoadSceneAsync("GroceryStore"));
    }
}
