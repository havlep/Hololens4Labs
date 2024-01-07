using HoloLens4Labs.Scripts.Mappers;
using HoloLens4Labs.Scripts.Model;
using HoloLens4Labs.Scripts.Model.DataTypes;
using HoloLens4Labs.Scripts.Model.Logs;
using NUnit.Framework;
using System;

public class TextLogMapperTests
{
    Experiment experiment;
    Scientist scientist;
    TextData data;
    TextLog textLog;

    [SetUp]
    public void Init()
    {
        scientist = new Scientist("Albert A. Michelson");
        experiment = new Experiment("Michelson-Morely", scientist);


        textLog = new TextLog("31459", new DateTime(2031, 1, 1), scientist, experiment);
        data = new TextData("299792", new DateTime(1983, 1, 1), scientist, textLog, "To be or not to be");
        textLog.TextData = data;

    }

    [Test]
    public void ToDTOTest()
    {

        TextLogMapper mapper = new TextLogMapper();
        var dto = mapper.ToDTO(textLog);

        Assert.IsNotNull(dto);
        Assert.AreEqual(textLog.Id.ToString(), dto.LogID);
        Assert.AreEqual(textLog.Id.ToString(), dto.TextLogID);
        Assert.AreEqual(textLog.TextData.Id.ToString(), dto.TextID);

    }

    [Test]
    public void CreateDTOTest()
    {

        TextLogMapper mapper = new TextLogMapper();
        var dto = mapper.ToDTO(textLog);
        var cdto = mapper.CreateDTO(textLog);

        Assert.IsNotNull(cdto);
        Assert.IsNotNull(dto);
        Assert.AreEqual(cdto.LogID, dto.LogID);
        Assert.AreEqual(cdto.TextLogID, dto.TextLogID);
        Assert.AreEqual(cdto.TextID, dto.TextID);

    }

}
