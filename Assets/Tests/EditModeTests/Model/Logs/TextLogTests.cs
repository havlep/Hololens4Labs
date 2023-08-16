using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using HoloLens4Labs.Scripts.Model.DataTypes;
using HoloLens4Labs.Scripts.Model.Logs;
using HoloLens4Labs.Scripts.Model;
using HoloLens4Labs.Scripts.Exceptions;
using System.Security.Cryptography;

public class TextLogTests
{

    DateTime textDateTime;
    Scientist scientist;
    Experiment experiment;

    [SetUp]
    public void Init() 
    {

        textDateTime = new DateTime(2022, 12, 25);
        scientist = new Scientist("Robert Oppenheimer");
        experiment = new Experiment("Trinity", scientist);


    }


    // Test the basic full instatiation of a TextLog object when all variables are available
    [Test]
    public void FullDefinitionConstructor()
    {

        var dateTime = new DateTime(2023, 1, 20);
        var textLog = new TextLog("101", dateTime, scientist, experiment);
        var textData = new TextData("101", textDateTime, scientist, textLog, "Three quarks for master Mark");
        textLog.TextData = textData;

        try
        {

           Assert.That(textLog.Id, Is.EqualTo("101"));
           Assert.That(textLog.TextData, Is.EqualTo(textData));
           Assert.That(textLog.DateTime, Is.EqualTo(dateTime));

        }
        catch (Exception e)
        {

            Assert.Fail(e.ToString());

        }

    }

    // Test the basic full instatiation of a textLog object when all variables are available
    [Test]
    public void OnlyTextInConstructor()
    {
        var textLog = new TextLog(scientist, experiment);
        var textData = new TextData("101", textDateTime, scientist, textLog, "Three quarks for master Mark");
        textLog.TextData = textData;
        var dateTime = DateTime.Now;

        try
        {
            Assert.That(textLog.TextData, Is.EqualTo(textData));
            Assert.That(System.Math.Abs((dateTime - textLog.DateTime).Milliseconds), Is.LessThan(10));

        }
        catch (Exception)
        {

            Assert.Fail();

        }
    }


}
