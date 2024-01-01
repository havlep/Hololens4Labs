using System;
using System.Collections;
using System.Collections.Generic;
using HoloLens4Labs.Scripts.Model;
using HoloLens4Labs.Scripts.Model.DataTypes;
using HoloLens4Labs.Scripts.Model.Logs;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ImageDataTests
{

    Log imageLog;
    Scientist scientist;
    Experiment experiment;

    [SetUp]
    public void Init()
    {

        scientist = new Scientist("Albert A. Michelson");
        experiment = new Experiment("Michelson-Morley", scientist);

        imageLog = new TextLog(scientist, experiment);

    }

    // Test the basic full instatiation of a imageData object when all variables are available
    [Test]
    public void FullDefinitionConstructor()
    {
        var dateTime = new System.DateTime(2022, 12, 25);
        var imageData = new ImageData("101", dateTime, "1015", "2023", "Blob name");

        try
        {

            Assert.That(imageData.Id, Is.EqualTo("101"));

            Assert.That(imageData.ThumbnailBlobName, Is.EqualTo("Blob name"));

            Assert.That(imageData.CreatedOn, Is.EqualTo(dateTime));

            Assert.That(imageData.CreatedById, Is.EqualTo("1015"));

            Assert.That(imageData.DoneWithinLogID, Is.EqualTo("2023"));

        }
        catch (Exception)
        {

            Assert.Fail();

        }

    }


    // A Test behaves as an ordinary method
    [Test]
    public void ImageDataTestsSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator ImageDataTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
