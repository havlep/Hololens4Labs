using HoloLens4Labs.Scripts.Controllers;
using HoloLens4Labs.Scripts.Model.DataTypes;
using HoloLens4Labs.Scripts.Model.Logs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ImageLogViewController : LogViewController
{


    [Header("UI Elements")]
    [SerializeField]
    protected Image imageCanvas = default;

    [SerializeField]
    private Sprite placeHolderImage = default;

    [SerializeField]
    private PhotoCameraController photoCapturePanelPrefab;



    public void InitWithExisting(ImageLog log, GameObject parentObj)
    {

        if (log.ImageData == null)
            throw new System.Exception("No image in existing image imagelog");
    
        lastModifiedLabel.SetText(log.ImageData.CreatedOn.ToShortTimeString());
        imageCanvas.sprite = spriteFromImage(log.ImageData);
        
        base.Init(log, parentObj);

    }

    public void InitNew(ImageLog log, GameObject parentObj)
    {

        gameObject.SetActive(false);
        this.log = log;
        var photoCaptureView = Instantiate(photoCapturePanelPrefab, this.transform.position, Quaternion.identity);
        photoCaptureView.Init(log, this);
        base.Init(log, parentObj);

    }

    private  Sprite spriteFromImage(ImageData imageData)
    {
        return Sprite.Create(imageData.Texture, new Rect(0, 0, imageData.Texture.width, imageData.Texture.height), new Vector2(0.5f, 0.5f));
    }


    public void ImageCaptured(ImageData imageData)
    {

        var imagelog = log as ImageLog;
        imagelog.ImageData = imageData;
        imageCanvas.sprite = spriteFromImage(imagelog.ImageData);
        gameObject.SetActive(true);
    
    }

}
