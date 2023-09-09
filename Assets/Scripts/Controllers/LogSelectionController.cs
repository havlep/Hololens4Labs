using HoloLens4Labs.Scripts.Controllers;
using HoloLens4Labs.Scripts.Managers;
using HoloLens4Labs.Scripts.Model.DataTypes;
using HoloLens4Labs.Scripts.Model.Logs;
using HoloLens4Labs.Scripts.Utils;
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
    [SerializeField]
    private ScrollableListPopulator scrollableListPopulator = default;

    [Header("Misc Settings")]
    [SerializeField]
    private GameObject mainMenu = default;

   
    // Start is called before the first frame update
    void Start()
    {
        scrollableListPopulator.MakeScrollingList();

        var logs = new List<Log>();

        for (int i = 0; i < 10; i++)
        {
            var log = new TextLog(DateTime.Now, sceneController.CurrentUser, sceneController.CurrentExperiment);
            log.Id = i.ToString();
            var textData = new TextData(DateTime.Now, sceneController.CurrentUser, log, "Testo di prova");
            log.TextData = textData;

            logs.Add(log);
        }

        
        PopulateList(logs.ToArray());
        //PopulateList(dataManager.GetLogsForExperiment(sceneController.CurrentExperiment.Id).Result);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PopulateList(Log[] logs)
    {

        foreach (Log log in logs)
        {
            scrollableListPopulator.AddItem(log);
        }
    }

    public static void PopulateItemAction(GameObject itemInstance, object data)
    {

        var logInfoButtonController = itemInstance.GetComponent<LogInfoButtonController>();

        if (logInfoButtonController == null)
        {

            Debug.LogError("PopulateItemAction: obj is not a LogInfoButtonController");
            throw new NotSupportedException("PopulateItemAction: obj is not a LogInfoButtonController");
        }

        if ((data is not Log))
        {

            Debug.LogError("PopulateItemAction: data is not a Log");
            throw new NotSupportedException("PopulateItemAction: data is not a Log");
        }

        var log = data as Log;

        logInfoButtonController.Init(log);

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
