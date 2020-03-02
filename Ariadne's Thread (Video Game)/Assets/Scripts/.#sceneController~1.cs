using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
        public void LoadScene(int SceneToChangeTo)
        {
           SceneManager.LoadScene(SceneToChangeTo);
        }
}

}
