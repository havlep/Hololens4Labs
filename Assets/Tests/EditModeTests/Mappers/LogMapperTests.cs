using NUnit.Framework;
using HoloLens4Labs.Scripts.Mappers;
using HoloLens4Labs.Scripts.Model;
using HoloLens4Labs.Scripts.Model.Logs;
using System;
using HoloLens4Labs.Scripts.Model.DataTypes;
using HoloLens4Labs.Scripts.DTOs;

public class LogMapperTests
{
    Experiment experiment;
    Scientist scientist;
    TextLog textLog;
    LogDTO logDTO;

    [SetUp]
    public void Init()
    {

        scientist = new Scientist("1","Albert A. Michelson");
        experiment = new Experiment("12","Michelson-Morely", scientist);
        textLog = new TextLog("31459", new DateTime(2031, 2, 1), scientist, experiment);

        logDTO = new LogDTO() {

            RowKey = "1",
            ScientistID = "1",
            ExperimentID = "1",
            DateTime = DateTime.Now,
            TextLogID = "1",
           
   
        };

    }

    [Test]
    public void ToDTONoData()
    {

        LogMapper mapper = new LogMapper();
        var dto = mapper.ToDTO(textLog);

        Assert.IsNotNull(dto);
        Assert.AreEqual(textLog.Id, dto.RowKey);
        Assert.AreEqual(textLog.CreatedOn, dto.DateTime);
        Assert.AreEqual(textLog.DoneWithin.Id, dto.ExperimentID);
        Assert.AreEqual(textLog.CreatedBy.Id, dto.ScientistID);
        Assert.AreEqual(textLog.Id, dto.TextLogID);
        Assert.IsEmpty(dto.DataID);
        Assert.IsEmpty(dto.DataScientistID);
        Assert.AreEqual(dto.DataDateTime, DateTime.MinValue);
        Assert.IsEmpty(dto.Text);
        Assert.IsEmpty(dto.TranscriptionLogID);
        Assert.IsEmpty(dto.WeightLogID);
        Assert.IsEmpty(dto.ImageLogID);

    }

    public void ToDTOWithData()
    {

        var textData = new TextData("1", DateTime.Now, "2", "31459", "This is a test");
        textLog.TextData = textData;

        LogMapper mapper = new LogMapper();
        var dto = mapper.ToDTO(textLog);

        Assert.IsNotNull(dto);
        Assert.AreEqual(textLog.Id, dto.RowKey);
        Assert.AreEqual(textLog.CreatedOn, dto.DateTime);
        Assert.AreEqual(textLog.DoneWithin.Id, dto.ExperimentID);
        Assert.AreEqual(textLog.CreatedBy.Id, dto.ScientistID);
        Assert.AreEqual(textLog.Id, dto.TextLogID);
        Assert.AreEqual(textLog.TextData.Id, dto.DataID);
        Assert.AreEqual(textLog.TextData.CreatedOn, dto.DataDateTime);
        Assert.AreEqual(textLog.TextData.CreatedById, dto.DataScientistID);
        Assert.AreEqual(textLog.TextData.Text, dto.Text);


        Assert.IsEmpty(dto.TranscriptionLogID);
        Assert.IsEmpty(dto.WeightLogID);
        Assert.IsEmpty(dto.ImageLogID);

    }

    [Test]
    public void CreateDTO()
    {

        LogMapper mapper = new LogMapper();
        var dto = mapper.ToDTO(textLog);
        var cdto = mapper.CreateDTO(textLog);

        Assert.IsNotNull(cdto);
        Assert.IsNotNull(dto);
        Assert.AreEqual(cdto.RowKey, dto.RowKey);
        Assert.AreEqual(cdto.TextLogID, dto.TextLogID);
     

    }

    [Test]
    public void ToObjWithData() {


        logDTO.DataID = "1";
        logDTO.DataDateTime = DateTime.Now;
        logDTO.DataScientistID = "1";
        logDTO.Text = "My test data";
        
        LogMapper mapper = new LogMapper();
        var mappedLog = mapper.ToOBJ(logDTO);

        Assert.IsNotNull(mappedLog);
        Assert.IsTrue(mappedLog is TextLog);
        var mappedTextLog = mappedLog as TextLog;

        Assert.AreEqual(mappedTextLog.Id, logDTO.RowKey);
        Assert.AreEqual(mappedTextLog.CreatedOn, logDTO.DateTime);
        Assert.AreEqual(mappedTextLog.DoneWithinID, logDTO.ExperimentID);
        Assert.AreEqual(mappedTextLog.CreatedByID, logDTO.ScientistID);
        Assert.AreEqual(mappedTextLog.Id, logDTO.TextLogID);
        Assert.AreEqual(mappedTextLog.TextData.Id, logDTO.DataID);
        Assert.AreEqual(mappedTextLog.TextData.CreatedOn, logDTO.DataDateTime);
        Assert.AreEqual(mappedTextLog.TextData.CreatedById, logDTO.DataScientistID);
        Assert.AreEqual(mappedTextLog.TextData.Text, logDTO.Text);



    }

    [Test]
    public void ToObjNoData()
    {

        LogMapper mapper = new LogMapper();
        var mappedLog = mapper.ToOBJ(logDTO);

        Assert.IsNotNull(mappedLog);
        Assert.IsTrue(mappedLog is TextLog);
        var mappedTextLog = mappedLog as TextLog;

        Assert.AreEqual(mappedTextLog.Id, logDTO.RowKey);
        Assert.AreEqual(mappedTextLog.CreatedOn, logDTO.DateTime);
        Assert.AreEqual(mappedTextLog.DoneWithinID, logDTO.ExperimentID);
        Assert.AreEqual(mappedTextLog.CreatedByID, logDTO.ScientistID);
        Assert.AreEqual(mappedTextLog.Id, logDTO.TextLogID);
        Assert.IsNull(mappedTextLog.TextData);


    }

}
