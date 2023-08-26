using HoloLens4Labs.Scripts.Controllers;
using HoloLens4Labs.Scripts.Managers;
using HoloLens4Labs.Scripts.Model.Logs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogSelectionController : MonoBehaviour
{

    [Header("Managers")]
    [SerializeField]
    public DataManager dataManager = default;
    public SceneController sceneController = default;

          
    [Header("UI Elements")]
    [SerializeField]
    private TextLogViewController textLogViewPrefab = default;
    [SerializeField]
    private ImageLogViewController photoLogViewPrefab = default;
    [SerializeField]
    private TranscriptionLogViewController transcriptionLogViewPrefab = default;

    [Header("Misc Settings")]
    [SerializeField]
    private GameObject mainMenu = default;

   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateNewTextLog() {

        var textLogView =  Instantiate(textLogViewPrefab, this.transform.position, Quaternion.identity);
        var textLog = new TextLog(DateTime.Now, sceneController.CurrentUser, sceneController.CurrentExperiment  );
        textLogView.Init(textLog, gameObject);
        gameObject.SetActive(false);

    }

    public void CreateNewImageLog()
    {

        var imageLogView = Instantiate(photoLogViewPrefab, this.transform.position, Quaternion.identity);       
        var imageLog = new ImageLog(DateTime.Now, sceneController.CurrentUser, sceneController.CurrentExperiment);
        imageLogView.InitNew(imageLog, gameObject);
        gameObject.SetActive(false);

    }

    public void CreateNewTranscriptionLog() {

        var transcriptionLogView = Instantiate(transcriptionLogViewPrefab, this.transform.position, Quaternion.identity);
        TranscriptionLog transcriptionLog = new TranscriptionLog(DateTime.Now, sceneController.CurrentUser, sceneController.CurrentExperiment);
        transcriptionLogView.InitNew(transcriptionLog, gameObject);
        gameObject.SetActive(false);

    }

}
