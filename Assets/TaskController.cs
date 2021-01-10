using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI taskText = default;
    [SerializeField] TextMeshProUGUI saidLinesText = default;
    private bool _mensRoomTaskCompleted = false;
    private bool _ladiesRoomTaskCompleted = false;
    private bool _bathroomTaskCompleted = false;
    private bool _ovenTaskCompleted = false;
    private bool _monitorsTaskCompleted = false;
    private int _monitorVisited = 0;
    private bool _printerTaskCompleted = false;
    private bool _allTasksCompleted = false;
    private float _hidingTime = 3f;

    void Start()
    {
        UpdateTaskText();
        FirstSaidLineAndTimer();
    }

    public void MensRoomTaskCompleted()
    {
        _mensRoomTaskCompleted = true;
        BathroomsTaskCompleted();
    }

    public void LadiesRoomTaskCompleted()
    {
        _ladiesRoomTaskCompleted = true;
        BathroomsTaskCompleted();
    }

    public void BathroomsTaskCompleted()
    {
        if (_mensRoomTaskCompleted && _ladiesRoomTaskCompleted)
        {
            _bathroomTaskCompleted = true;
            UpdateTaskText();
        }
    }

    public void OvenTaskCompleted()
    {
        _ovenTaskCompleted = true;
        UpdateTaskText();
    }

    public void MonitorsTaskCompleted()
    {
        _monitorVisited++;
        if (_monitorVisited == 3)
        {
            _monitorsTaskCompleted = true;
            UpdateTaskText();
        }
    }

    public void PrinterTaskCompleted()
    {
        _printerTaskCompleted = true;
        UpdateTaskText();
    }

    public void PresidentDoorTask()
    {
        string presidentDoorText;
        if (_bathroomTaskCompleted && _monitorsTaskCompleted && _ovenTaskCompleted && _printerTaskCompleted)
        { 
            _allTasksCompleted = true;
            presidentDoorText = UpdatePresidentDoorSaidLinesText();
        }

        presidentDoorText = UpdatePresidentDoorSaidLinesText();
        StartCoroutine(TextPopUp(saidLinesText.gameObject, saidLinesText, presidentDoorText, _hidingTime));
    }

    private void FirstSaidLineAndTimer()
    {
        bool isFirstLine = true;
        _hidingTime = 4f;
        StartCoroutine(TextPopUp(saidLinesText.gameObject, saidLinesText, "Ok, antes de sair, tenho que fazer algumas coisas na empresa.", _hidingTime, isFirstLine));
    }

    private void TutorialTextShowUp()
    {
        string tutorialText = "Aperte 'F' para lanterna.\nAperte 'Tab' para ver suas tarefas.";
        StartCoroutine(TextPopUp(saidLinesText.gameObject, saidLinesText, tutorialText, _hidingTime));
    }

    void UpdateTaskText()
    {
        taskText.text = "Fala corno, como tu tá sendo otário e trabalhando até mais tarde pra gerar mais-valia, tem como fazer umas paradas antes de sair?\n";
        taskText.text += _monitorsTaskCompleted ? "<s>1. Apagar os monitores;</s>\n" : "1. Apagar os monitores;\n";
        taskText.text += _bathroomTaskCompleted ? "<s>2. Conferir as pias dos banheiros e cozinha;</s>\n" : "2. Conferir as pias dos banheiros e cozinha;\n";
        taskText.text += _ovenTaskCompleted ? "<s>3. Conferir o fogão;</s>\n" : "3.  Conferir o fogão;\n";
        taskText.text += "4.  Fechar as janelas;\n"; //Criar as janelas
        taskText.text += _printerTaskCompleted ? "<s>5. Desligar a impressora;</s>\n" : "5. Desligar a impressora;\n";
        taskText.text += "6. Pegue a chave na sala do presidente e abra a porta, mas não se esqueça de trancar ao sair.\n";
        taskText.text += "A sala do presidente vai estar aberta, a chave está na gaveta da mesa dele!\n";
        taskText.text += "\nAbraços, \n";
        taskText.text += "Robson";
    }

    string UpdatePresidentDoorSaidLinesText()
    {
        return _allTasksCompleted ? "A porta está trancada??? Como diabos eu vou pegar a chave?!" : "Preciso terminar as tarefas antes de sair, não posso ser tão irresponsável sendo o vice presidente.";
    }

    IEnumerator TextPopUp(GameObject textToHide, TextMeshProUGUI textToChange, string changedText, float hidingTime, bool isFirstLine = false)
    {
        if (isFirstLine)
        {
            float isFirstLineTimeWait = 2f;
            yield return new WaitForSeconds(isFirstLineTimeWait);
        }

        textToChange.text = changedText;
        textToHide.gameObject.SetActive(true);
        yield return new WaitForSeconds(hidingTime);
        textToHide.gameObject.SetActive(false);

        if (isFirstLine)
        {
            TutorialTextShowUp();
        }
    }
}