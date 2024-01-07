using HoloLens4Labs.Scripts.Model;
using HoloLens4Labs.Scripts.Model.DataTypes;
using HoloLens4Labs.Scripts.Model.Logs;
using NUnit.Framework;
using System;

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

    // Test constructor used when the ImageData is already in the database and scientist and log don't exist as data model objects
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

    // Test constructor used when the ImageData is not yet in the database and scientist and log exist as data model objects
    [Test]
    public void ImageDataNotYetInDatabase()
    {
        var dateTime = new System.DateTime(2022, 12, 25);
        var imageData = new ImageData(dateTime, scientist, imageLog);

        try
        {

            Assert.That(imageData.DoneWithinLog, Is.EqualTo(imageLog));

            Assert.That(imageData.CreatedBy, Is.EqualTo(scientist));

            Assert.That(imageData.CreatedOn, Is.EqualTo(dateTime));


        }
        catch (Exception)
        {

            Assert.Fail();

        }

    }

    // Test constructor used when the ImageData is already in the database and scientist and log exist as data model objects
    [Test]
    public void ImageDataAlreadyInDatabase()
    {
        var dateTime = new System.DateTime(2022, 12, 25);
        var imageData = new ImageData("101", dateTime, scientist, imageLog);

        try
        {

            Assert.That(imageData.Id, Is.EqualTo("101"));

            Assert.That(imageData.DoneWithinLog, Is.EqualTo(imageLog));

            Assert.That(imageData.CreatedBy, Is.EqualTo(scientist));

            Assert.That(imageData.CreatedOn, Is.EqualTo(dateTime));
        }
        catch (Exception)
        {
            Assert.Fail();
        }
    }
}
