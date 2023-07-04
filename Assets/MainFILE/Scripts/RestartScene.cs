using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

public class RestartScene : MonoBehaviour
{
    public SteamVR_Action_Boolean restartAction;  // Reference to the SteamVR Input action for restart

    private void Update()
    {
        if (restartAction.GetStateDown(SteamVR_Input_Sources.Any))
        {
            Restart();
        }
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}