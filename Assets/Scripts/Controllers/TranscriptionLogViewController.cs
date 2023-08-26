using HoloLens4Labs.Scripts.Controllers;
using HoloLens4Labs.Scripts.Model.DataTypes;
using HoloLens4Labs.Scripts.Model.Logs;
using UnityEngine;
using UnityEngine.UI;

public class TranscriptionLogViewController : LogViewController
{


    [Header("UI Elements")]
    [SerializeField]
    protected Image imageCanvas = default;

    [SerializeField]
    private Sprite placeHolderImage = default;

    [SerializeField]
    private PhotoCameraController photoCapturePanelPrefab;



    public void InitWithExisting(TranscriptionLog log, GameObject parentObj)
    {

        if (log.Data == null)
            throw new System.Exception("No image in existing image imagelog");

        lastModifiedLabel.SetText(log.Data.CreatedOn.ToShortTimeString());
        imageCanvas.sprite = spriteFromImage(log.Data);

        base.Init(log, parentObj);

    }

    public void InitNew(TranscriptionLog log, GameObject parentObj)
    {

        gameObject.SetActive(false);
        this.log = log;
        var photoCaptureView = Instantiate(photoCapturePanelPrefab, this.transform.position, Quaternion.identity);
        photoCaptureView.Init(log, this);
        base.Init(log, parentObj);

    }

    private Sprite spriteFromImage(TranscriptionData data)
    {
        return Sprite.Create(data.Texture, new Rect(0, 0, data.Texture.width, data.Texture.height), new Vector2(0.5f, 0.5f));
    }


    public override void ImageCaptured(DataType data)
    {
        if(! (data is ImageData) )
            throw new System.Exception("Wrong data type call for transcription log");

        var imageData = data as ImageData;

        // Ugly hack to get the data to the right type until I can refactor the data type to ImageData
        var transData = new TranscriptionData(imageData.Id, imageData.CreatedOn, imageData.CreatedBy, imageData.DoneWithinLog);
        transData.Data = imageData.Data;
        transData.Texture = imageData.Texture;

        var imagelog = log as TranscriptionLog;
        imagelog.Data = transData;
        imageCanvas.sprite = spriteFromImage(imagelog.Data);
        gameObject.SetActive(true);

    }

}