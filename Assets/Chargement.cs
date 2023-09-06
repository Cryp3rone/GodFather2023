using UnityEngine;
using UnityEngine.SceneManagement;

public class Chargement : MonoBehaviour
{
    public string nomDeLaScene;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space)){
            loadScene();
        }
    }
    
    public void loadScene() {
        SceneManager.LoadScene(nomDeLaScene);
    }
}