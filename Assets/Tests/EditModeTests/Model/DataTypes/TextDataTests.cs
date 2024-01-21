// Copyright (c) Petr Havel 2023.
// Licensed under the MIT License.

using HoloLens4Labs.Scripts.Model;
using HoloLens4Labs.Scripts.Model.DataTypes;
using HoloLens4Labs.Scripts.Model.Logs;
using NUnit.Framework;
using System;

public class TextDataTests
{

    Log textLog;
    Scientist scientist;
    Experiment experiment;

    [SetUp]
    public void Init()
    {

        scientist = new Scientist("Albert A. Michelson");
        experiment = new Experiment("Michelson-Morley", scientist);

        textLog = new TextLog(scientist, experiment);

    }

    // Test the basic full instatiation of a textData object when all variables are available
    [Test]
    public void FullDefinitionConstructor()
    {
        var dateTime = new DateTime(2022, 12, 25);
        var textData = new TextData("101", dateTime, scientist, textLog, "Three quarks for master Mark");

        try
        {

            Assert.That(textData.Id, Is.EqualTo("101"));

            Assert.That(textData.Text, Is.EqualTo("Three quarks for master Mark"));

            Assert.That(textData.CreatedOn, Is.EqualTo(dateTime));


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
        var dateTime = DateTime.Now;
        var textData = new TextData(dateTime, scientist, textLog, "Three quarks for master Mark");


        try
        {
            Assert.That(textData.Text, Is.EqualTo("Three quarks for master Mark"));
            Assert.That(System.Math.Abs((dateTime - textData.CreatedOn).Milliseconds), Is.LessThan(10));

        }
        catch (Exception)
        {

            Assert.Fail();

        }
    }

}
