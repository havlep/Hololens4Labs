using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using HoloLens4Labs.Scripts.Exceptions;
using HoloLens4Labs.Scripts.Model;


public class ExperimentTest
{
    // Test the basic full instatiation of a experiment object when all variables are available
    [Test]
    public void FullDefinitionConstructor()
    {
        var experiment = new Experiment(22, "Trinity");

        try
        {

            Assert.That(experiment.Id, Is.EqualTo(22));
            Assert.That(experiment.Name, Is.EqualTo("Trinity"));


        }
        catch (Exception)
        {

            Assert.Fail();

        }

    }

    // Test the basic full instatiation of a experiment object when all variables are available
    [Test]
    public void OnlyNameInConstructor()
    {
        var experiment = new Experiment("Trinity");

        try
        {
            Assert.That(experiment.Name, Is.EqualTo("Trinity"));

        }
        catch (Exception)
        {

            Assert.Fail();

        }
    }


    [Test]
    public void OnlyNameNoIdException()
    {

        var experiment = new Experiment("Trinity");
        try
        {
            var i = experiment.Id;
            Assert.Fail();
        }
        catch (ObjectDataBaseException)
        {
            // Exception of correct type raised
        }

    }

    [Test]
    public void NoIdOrNameExceptions()
    {

        var experiment = new Experiment();
        try
        {
            var i = experiment.Id;
            Assert.Fail();
        }
        catch (ObjectDataBaseException)
        {
            // Exception of correct type raised
        }

        try
        {
            var i = experiment.Name;
            Assert.Fail();
        }
        catch (ObjectNotInitializedException)
        {
            // Exception of correct type raised
        }

    }
}
