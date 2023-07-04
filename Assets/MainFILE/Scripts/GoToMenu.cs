using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

public class GoToMenu : MonoBehaviour
{
    public SteamVR_Action_Boolean MenuAction;  // Reference to the SteamVR Input action for restart

    private void Update()
    {
        if (MenuAction.GetStateDown(SteamVR_Input_Sources.Any))
        {
            Menu();
        }
    }

    private void Menu()
    {
        SceneManager.LoadScene(0);
    }
}
