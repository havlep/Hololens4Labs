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
        var experiment = new Experiment("22", "Trinity","99", DateTime.Now);

        try
        {

            Assert.That(experiment.Id, Is.EqualTo("22"));
            Assert.That(experiment.Name, Is.EqualTo("Trinity"));
            Assert.That(experiment.CreatedByID, Is.EqualTo("99"));
            Assert.That(experiment.CreatedBy, Is.EqualTo(null));

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
        var scientist = new Scientist("21", "Rutherford");
        var experiment = new Experiment("Trinity",scientist);

        try
        {
            Assert.That(experiment.Name, Is.EqualTo("Trinity"));
            Assert.That(experiment.CreatedByID, Is.EqualTo("21"));
            Assert.That(experiment.CreatedBy, Is.EqualTo(scientist));
            Assert.That(experiment.Id, Is.EqualTo(string.Empty));

        }
        catch (Exception)
        {

            Assert.Fail();

        }
    }


    
}
