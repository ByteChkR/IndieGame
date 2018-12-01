using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CheckForAnyPressedKey : MonoBehaviour
{
    public UnityEvent onKeyPressed;
    public Controller.Interactions interaction;
    public Controller.Interactions i
    {
        get { return interaction; }
        set { interaction = value; }
    }

    public void SetInteraction(int newInt)
    {
        interaction = (Controller.Interactions)newInt;
    }

    // Update is called once per frame
    void Update()
    {
        KeyCode c = Controller.GetPressedKey();
        if (c == KeyCode.None) return;
        Controller.SetKeycodeFor(interaction, c);
        Controller.SaveToFile(Controller.interactions);
        onKeyPressed.Invoke();
        KeycodeDisplay.onRefresh(interaction);
    }
}
