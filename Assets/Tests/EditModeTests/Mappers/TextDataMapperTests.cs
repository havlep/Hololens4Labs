using NUnit.Framework;
using HoloLens4Labs.Scripts.Mappers;
using HoloLens4Labs.Scripts.Model;
using HoloLens4Labs.Scripts.DTOs;
using HoloLens4Labs.Scripts.Model.DataTypes;
using HoloLens4Labs.Scripts.Model.Logs;
using System;

public class TextDataMapperTests
{
    Experiment experiment;
    Scientist scientist;
    TextData data;
    TextLog textLog;

    [SetUp]
    public void Init()
    {

        experiment = new Experiment(12,"Michelson-Morely");
        scientist = new Scientist(11,"Albert A. Michelson");

        textLog = new TextLog(31459, new DateTime(2031, 1, 1), scientist, experiment);
        data = new TextData(299792, new DateTime(1983, 1, 1), scientist, textLog, "To be or not to be");
        textLog.TextData = data;

    }

    [Test]
    public void ToDTOTest()
    {

        TextDataMapper mapper = new TextDataMapper();
        var dto = mapper.ToDTO(data);

        Assert.IsNotNull(dto);
        Assert.AreEqual(data.Id.ToString(), dto.TextID);
        Assert.AreEqual(data.DoneWithinLogID.ToString(), dto.TextLogID); 
        Assert.AreEqual(data.Text, dto.Text);
        Assert.AreEqual(data.DateTime, dto.Created);
        Assert.AreEqual(data.CreatedById.ToString(), dto.ScientistID);

    }

    [Test]
    public void CreateDTOTest()
    {

        TextDataMapper mapper = new TextDataMapper();
        var dto = mapper.ToDTO(data);
        var cdto = mapper.CreateDTO(data);

        Assert.IsNotNull(cdto);
        Assert.IsNotNull(dto);
        Assert.AreEqual(cdto.TextID, dto.TextID);
        Assert.AreEqual(cdto.TextLogID, dto.TextLogID);
        Assert.AreEqual(cdto.Text, dto.Text);
        Assert.AreEqual(cdto.Created, dto.Created);
        Assert.AreEqual(cdto.ScientistID, dto.ScientistID);

    }
}