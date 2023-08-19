using NUnit.Framework;
using HoloLens4Labs.Scripts.Mappers;
using HoloLens4Labs.Scripts.Model;
using HoloLens4Labs.Scripts.Model.Logs;
using System;

public class LogMapperTests
{
    Experiment experiment;
    Scientist scientist;
    TextLog textLog;

    [SetUp]
    public void Init()
    {

        scientist = new Scientist("1","Albert A. Michelson");
        experiment = new Experiment("12","Michelson-Morely", scientist);
        textLog = new TextLog("31459", new DateTime(2031, 2, 1), scientist, experiment);

    }

    [Test]
    public void ToDTOTest()
    {

        LogMapper mapper = new LogMapper();
        var dto = mapper.ToDTO(textLog);

        Assert.IsNotNull(dto);
        Assert.AreEqual(textLog.Id.ToString(), dto.LogID);
        Assert.AreEqual(textLog.CreatedOn, dto.DateTime);
        Assert.AreEqual(textLog.DoneWithin.Id.ToString(), dto.ExperimentID);
        Assert.AreEqual(textLog.CreatedBy.Id.ToString(), dto.ScientistID);
        Assert.AreEqual(textLog.Id.ToString(), dto.TextLogID);
        Assert.IsNull(dto.TranscriptionLogID);
        Assert.IsNull(dto.WeightLogID);
        Assert.IsNull(dto.PhotoLogID);

    }

    [Test]
    public void CreateDTOTest()
    {

        LogMapper mapper = new LogMapper();
        var dto = mapper.ToDTO(textLog);
        var cdto = mapper.CreateDTO(textLog);

        Assert.IsNotNull(cdto);
        Assert.IsNotNull(dto);
        Assert.AreEqual(cdto.LogID, dto.LogID);
        Assert.AreEqual(cdto.TextLogID, dto.TextLogID);
     

    }
}
