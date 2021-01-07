using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMenu : MonoBehaviour
{
    public static bool NoteIsActive = false;

    [SerializeField] private GameObject noteMenuUI = default;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (NoteIsActive)
            {
                CloseNoteMenu();
            }
            else
            {
                ShowNoteMenu();
            }
        }
    }

    void ShowNoteMenu()
    {
        noteMenuUI.SetActive(true);
        NoteIsActive = true;
    }

    void CloseNoteMenu()
    {
        noteMenuUI.SetActive(false);
        NoteIsActive = false;
    }
}
