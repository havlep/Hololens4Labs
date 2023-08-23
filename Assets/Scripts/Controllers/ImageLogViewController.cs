using HoloLens4Labs.Scripts.Controllers;
using HoloLens4Labs.Scripts.Model.DataTypes;
using HoloLens4Labs.Scripts.Model.Logs;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ImageLogViewController : LogViewController
{


    [Header("Managers")]
    [SerializeField]
    private SceneController SceneController;

    [Header("UI Elements")]
    [SerializeField]
    protected Image imageCanvas = default;

    [SerializeField]
    private Sprite placeHolderImage = default;

    private bool isWaitingForAirtap;
    private GameObject hintTextInstance;
    ImageLog log;


    public void Init(ImageLog log, GameObject parentObj)
    {

        if (log.ImageData != null)
        {
            lastModifiedLabel.SetText(log.ImageData.CreatedOn.ToShortTimeString());
            imageCanvas.sprite = spriteFromImage(log.ImageData);
        }
        else
        {
            lastModifiedLabel.SetText(string.Empty);
            imageCanvas.sprite = placeHolderImage;
        }
        base.Init(log, parentObj);
        sceneController.PhotoCameraController.StartCamera();

    }

    private  Sprite spriteFromImage(ImageData imageData)
    {
        return Sprite.Create(imageData.Texture, new Rect(0, 0, imageData.Texture.width, imageData.Texture.height), new Vector2(0.5f, 0.5f));
    }

    public void HandleOnPointerClick()
    {
        if (isWaitingForAirtap)
        {
            CapturePhoto();
        }
    }

    private async void CapturePhoto()
    {
        isWaitingForAirtap = false;
        hintTextInstance.SetActive(false);

        log.ImageData = await sceneController.PhotoCameraController.TakePhotoWithThumbnail(log);
        imageCanvas.sprite =  spriteFromImage(log.ImageData);


        SetButtonsInteractiveState(true);
    }


}
