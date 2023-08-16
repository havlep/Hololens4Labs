using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using HoloLens4Labs.Scripts.Exceptions;
using HoloLens4Labs.Scripts.Model;
using System;

public class ScientistTest
{
    // Test the basic full instatiation of a scientist object when all variables are available
    [Test]
    public void FullDefinitionConstructor()
    {
        var scientist = new Scientist("22", "John Glen");

        try
        {

            Assert.That(scientist.Id, Is.EqualTo("22"));
            Assert.That(scientist.Name, Is.EqualTo("John Glen"));
          

        }
        catch (Exception)
        {

            Assert.Fail();

        }

    }

    // Test the basic full instatiation of a scientist object when all variables are available
    [Test]
    public void OnlyNameInConstructor()
    {
        var scientist = new Scientist("John Glen");

        try
        {
            Assert.That(scientist.Name, Is.EqualTo("John Glen"));

        }
        catch (Exception)
        {

            Assert.Fail();

        }
    }

    // Test the basic full instatiation of a scientist object when all variables are available
    [Test]
    public void NameAfterConstructor()
    {
        var scientist = new Scientist();

        try
        {
            scientist.Name = "Neil Armstrong";
            Assert.That(scientist.Name, Is.EqualTo("Neil Armstrong"));

        }
        catch (Exception)
        {

            Assert.Fail();

        }
    }


}