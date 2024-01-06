using System;
using HoloLens4Labs.Scripts.Model;
using HoloLens4Labs.Scripts.Model.DataTypes;
using HoloLens4Labs.Scripts.Model.Logs;
using NUnit.Framework;


public class TranscriptionDataTests
{
    Log transcriptionLog;
    Scientist scientist;
    Experiment experiment;

    [SetUp]
    public void Init()
    {

        scientist = new Scientist("Albert A. Michelson");
        experiment = new Experiment("Michelson-Morley", scientist);

        transcriptionLog = new TranscriptionLog(scientist, experiment);

    }

    // Constructor used when the TranscriptionData is already in the database and the Scientist and Log don't exist as data model objects
    [Test]
    public void InDatabaseScientistAndLogIDConstructor()
    {
        var dateTime = new System.DateTime(2022, 12, 25);
        var transcriptionData = new TranscriptionData("101", dateTime, "1015", "2023", "Blob name", "Text");

        try
        {

            Assert.That(transcriptionData.Id, Is.EqualTo("101"));

            Assert.That(transcriptionData.ThumbnailBlobName, Is.EqualTo("Blob name"));

            Assert.That(transcriptionData.CreatedOn, Is.EqualTo(dateTime));

            Assert.That(transcriptionData.CreatedById, Is.EqualTo("1015"));

            Assert.That(transcriptionData.DoneWithinLogID, Is.EqualTo("2023"));

            Assert.That(transcriptionData.Text, Is.EqualTo("Text"));

        }
        catch (Exception)
        {

            Assert.Fail();

        }

    }

    // Constructor used when the TranscriptionData is not in the database and the Scientist and Log exist as data model objects
    [Test]
    public  void NotInDatabaseScientistAndLogConstructor()
    {
        var dateTime = new System.DateTime(2022, 12, 25);
        var transcriptionData = new TranscriptionData(dateTime, scientist, transcriptionLog);

        try
        {   

            Assert.That(transcriptionData.CreatedOn, Is.EqualTo(dateTime));

            Assert.That(transcriptionData.CreatedBy, Is.EqualTo(scientist));

            Assert.That(transcriptionData.DoneWithinLog, Is.EqualTo(transcriptionLog));

        }
        catch (Exception)
        {

            Assert.Fail();

        }

    }


    // Constructor used when the TranscriptionData is already in the database and the Scientist and Log exist as data model objects
    [Test]
    public void InDatabaseScientistAndLogConstructor()
    {
        var dateTime = new System.DateTime(2022, 12, 25);
        var transcriptionData = new TranscriptionData("1411", dateTime, scientist, transcriptionLog);

        try
        {

            Assert.That(transcriptionData.CreatedOn, Is.EqualTo(dateTime));

            Assert.That(transcriptionData.CreatedBy, Is.EqualTo(scientist));

            Assert.That(transcriptionData.DoneWithinLog, Is.EqualTo(transcriptionLog));
            
            Assert.That(transcriptionData.Id, Is.EqualTo("1411"));

        }
        catch (Exception)
        {

            Assert.Fail();

        }

    }

}
