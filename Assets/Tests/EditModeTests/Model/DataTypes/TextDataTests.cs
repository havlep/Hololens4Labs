using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using HoloLens4Labs.Scripts.Model.DataTypes;
using HoloLens4Labs.Scripts.Exceptions;
using HoloLens4Labs.Scripts.Model;
using System.Security.Cryptography;

public class TextDataTests
{

    Experiment experiment;
    Scientist scientist;

    [SetUp]
    public void Init() {

        experiment = new Experiment("Michelson-Morely");
        scientist = new Scientist ("Albert A. Michelson");
    
    }

    // Test the basic full instatiation of a textData object when all variables are available
    [Test]
    public void FullDefinitionConstructor()
    {
        var dateTime = new DateTime(2022,12, 25);
        var textData = new TextData(101, dateTime, scientist, experiment , "Three quarks for master Mark");

        try
        {

            Assert.That(textData.Id, Is.EqualTo(101));

            Assert.That(textData.Text, Is.EqualTo("Three quarks for master Mark"));

            Assert.That(textData.DateTime, Is.EqualTo(dateTime));


        }
        catch (Exception)
        {

            Assert.Fail();

        }

    }

    // Test the basic full instatiation of a textData object when all variables are available
    [Test]
    public void OnlyTextInConstructor()
    {
        var textData = new TextData(scientist, experiment, "Three quarks for master Mark");
        var dateTime = DateTime.Now;

        try
        {
            Assert.That(textData.Text, Is.EqualTo("Three quarks for master Mark"));
            Assert.That(System.Math.Abs((dateTime - textData.DateTime).Milliseconds), Is.LessThan(10));

        }
        catch (Exception)
        {

            Assert.Fail();

        }
    }

    [Test]
    public void OnlyTextNoIdException()
    {

        var textData = new TextData(scientist, experiment, "Three quarks for master Mark");
        try
        {
            var i = textData.Id;
            Assert.Fail();
        }
        catch (ObjectDataBaseException)
        {
            // Exception of correct type raised
        }

    }
}
