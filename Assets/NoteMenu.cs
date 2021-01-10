using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMenu : MonoBehaviour
{
    public static bool NoteIsActive = false;

    [SerializeField] private GameObject _noteMenuUI = default;
    private AudioSource _audioSource = default;

    void Start() 
    {
        _audioSource = GetComponent<AudioSource>();
    }

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
        _audioSource.Play();
        _noteMenuUI.SetActive(true);
        NoteIsActive = true;
    }

    void CloseNoteMenu()
    {
        _audioSource.Play();
        _noteMenuUI.SetActive(false);
        NoteIsActive = false;
    }
}
