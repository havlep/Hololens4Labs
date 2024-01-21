// Copyright (c) Petr Havel 2023.
// Licensed under the MIT License.

using HoloLens4Labs.Scripts.Model;
using HoloLens4Labs.Scripts.Model.DataTypes;
using HoloLens4Labs.Scripts.Model.Logs;
using NUnit.Framework;
using System;
using UnityEngine;

public class ImageLogTests
{
    DateTime imageDateTime;
    Scientist scientist;
    Experiment experiment;

    [SetUp]
    public void Init()
    {

        imageDateTime = new DateTime(2022, 12, 25);
        scientist = new Scientist("Robert Oppenheimer");
        scientist.Id = "102";
        experiment = new Experiment("Trinity", scientist);
        experiment.Id = "103";

    }

    // Test the consturctor used when the ImageLog is not yet in the database and the Scientist and Experiment exist as data model objects
    [Test]
    public void ImageLogNotInDataBaseExperimentScientistAsModelsConstructor()
    {

        var dateTime = new DateTime(2023, 1, 20);
        var imageLog = new ImageLog(dateTime, scientist, experiment);
        var imageData = new ImageData(imageDateTime, scientist, imageLog, new byte[10], new Texture2D(1,1));
        imageLog.Data = imageData;

        try
        {

            Assert.That(imageLog.Data, Is.EqualTo(imageData));
            Assert.That(imageLog.CreatedOn, Is.EqualTo(dateTime));
            Assert.True(imageLog.CreatedOn == dateTime);
            Assert.True(imageLog.CreatedBy == scientist);
            Assert.True(imageLog.DoneWithin == experiment);

        }
        catch (Exception e)
        {

            Assert.Fail(e.ToString());

        }

    }

    // Test the constructor used when the ImageLog is already in the database and the Scientist and Experiment exist as data model objects
    [Test]
    public void ImageLogInDataBaseExperimentScientistAsModelsConstructor()
    {

        var dateTime = new DateTime(2023, 1, 20);
        var imageLog = new ImageLog("101", dateTime, scientist, experiment);
        var imageData = new ImageData(imageDateTime, scientist, imageLog, new byte[10], new Texture2D(1,1));
        imageLog.Data = imageData;

        try
        {

            Assert.That(imageLog.Id, Is.EqualTo("101"));
            Assert.That(imageLog.Data, Is.EqualTo(imageData));
            Assert.That(imageLog.CreatedOn, Is.EqualTo(dateTime));
            Assert.True(imageLog.CreatedBy == scientist);
            Assert.True(imageLog.DoneWithin == experiment);

        }
        catch (Exception e)
        {

            Assert.Fail(e.ToString());

        }

    }

    // Test the constructor used when the ImageLog is already in the database and the Scientist and Experiment do not exist as data model objects at the moment
    [Test]
    public void ImageLogScientistExperimentInDatabaseConstructor()
    {
        var dateTime = DateTime.Now;
        var imageData = new ImageData("21", imageDateTime, scientist.Id, "101", null);
        var imageLog = new ImageLog("101", dateTime, scientist.Id, experiment.Id, imageData);


        try
        {

            Assert.That(imageLog.Id, Is.EqualTo("101"));
            Assert.That(imageLog.Data, Is.EqualTo(imageData));
            Assert.That(imageLog.CreatedOn, Is.EqualTo(dateTime));
            Assert.True(imageLog.DoneWithinID == experiment.Id);
            Assert.True(imageLog.CreatedByID == scientist.Id);

        }
        catch (Exception e)
        {

            Assert.Fail(e.ToString());

        }

    }

}
