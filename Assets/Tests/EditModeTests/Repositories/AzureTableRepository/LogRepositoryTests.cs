// Copyright (c) Petr Havel 2023.
// Licensed under the MIT License.

using HoloLens4Labs.Scripts.DTOs;
using HoloLens4Labs.Scripts.Exceptions;
using HoloLens4Labs.Scripts.Mappers;
using HoloLens4Labs.Scripts.Model;
using HoloLens4Labs.Scripts.Model.DataTypes;
using HoloLens4Labs.Scripts.Model.Logs;
using HoloLens4Labs.Scripts.Repositories.AzureTables;
using HoloLens4Labs.Tests;
using Microsoft.WindowsAzure.Storage.Table;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

public class LogRepositoryTests
{


    TextLog log = default;
    TextData data = default;
    Scientist scientist = default;
    Experiment experiment = default;
    LogMapper mapper = default;

    public class CloudTableMock : CloudTable
    {
        public CloudTableMock() : base(new Uri("http://sdfs.com")) { }

    }

    [SetUp]
    public void init()
    {

        scientist = new Scientist("12", "Galileo", "");
        experiment = new Experiment("13", "Gravity", "12", DateTime.Now);
        log = new TextLog(scientist, experiment);
        data = new TextData("14", DateTime.Now, scientist, log, "Feathers and weights have a different acceleration");
        mapper = new LogMapper(null);

    }

    [Test]
    public void CreateObjectExistsException()
    {


        var logDto = new LogDTO()
        {

            RowKey = log.Id

        };

        var logList = new List<LogDTO>
        {
            logDto
        };


        var constructorInfo = typeof(TableQuerySegment<LogDTO>)
                .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
        .FirstOrDefault(c => c.GetParameters().Count() == 1);

        var mockQuerySegment = constructorInfo.Invoke(new object[] {
            logList
        }) as TableQuerySegment<LogDTO>;

        var mockTable = new Mock<CloudTableMock>();
        mockTable
          .Setup(w => w.ExecuteQuerySegmentedAsync(It.IsAny<TableQuery<LogDTO>>(),
            It.IsAny<TableContinuationToken>()))
          .Returns(Task.FromResult(mockQuerySegment));

        var logRepository = new ATLogRepository(mockTable.Object, "l", null);


        Assert.Throws<ObjectDataBaseException>(
                          () => UnityTestUtils.RunAsyncMethodSync(() => logRepository.Create(log)));


    }

    [Test]
    public void CreateSuccessfull()
    {

        var constructorInfo = typeof(TableQuerySegment<LogDTO>)
                .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
        .FirstOrDefault(c => c.GetParameters().Count() == 1);

        var mockQuerySegment = constructorInfo.Invoke(new object[] {
            new List<LogDTO >()
        }) as TableQuerySegment<LogDTO>;

        var mockTable = new Mock<CloudTableMock>();
        mockTable
          .Setup(w => w.ExecuteQuerySegmentedAsync(It.IsAny<TableQuery<LogDTO>>(),
            It.IsAny<TableContinuationToken>()))
          .Returns(Task.FromResult(mockQuerySegment));


        log.Id = "15";
        var logDto = mapper.ToDTO(log);

        var operationResult = new TableResult
        {

            Result = logDto

        };

        mockTable
         .Setup(w => w.ExecuteAsync(It.IsAny<TableOperation>()))
         .Returns(Task.FromResult(operationResult));

        var logRepository = new ATLogRepository(mockTable.Object, "l", null);

        Assert.DoesNotThrow(
                         () => UnityTestUtils.RunAsyncMethodSync(() => logRepository.Create(log)));

        var result = UnityTestUtils.RunAsyncMethodSync(() => logRepository.Create(log));

        // TODO 
        Assert.That(result.Id, Is.EqualTo(logDto.RowKey));
        Assert.That(result.CreatedByID, Is.EqualTo(logDto.ScientistID));
        Assert.That(result.CreatedOn, Is.EqualTo(logDto.DateTime));

    }

    [Test]
    public void CreateOnSyncException()
    {

        var constructorInfo = typeof(TableQuerySegment<LogDTO>)
                .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
        .FirstOrDefault(c => c.GetParameters().Count() == 1);

        var mockQuerySegment = constructorInfo.Invoke(new object[] {
           new List<LogDTO>()
         }) as TableQuerySegment<LogDTO>;

        var mockTable = new Mock<CloudTableMock>();
        mockTable
          .Setup(w => w.ExecuteQuerySegmentedAsync(It.IsAny<TableQuery<LogDTO>>(),
            It.IsAny<TableContinuationToken>()))
          .Returns(Task.FromResult(mockQuerySegment));


        var logDto = new LogDTO()
        {

            RowKey ="2467"

        };

        var operationResult = new TableResult
        {

            Result = null

        };


        mockTable
         .Setup(w => w.ExecuteAsync(It.IsAny<TableOperation>()))
         .Returns(Task.FromResult(operationResult));

        var logRepository = new ATLogRepository(mockTable.Object, "l", null);

        Assert.Throws<ObjectDataBaseException>(
                         () => UnityTestUtils.RunAsyncMethodSync(() => logRepository.Create(log)));


    }

    [Test]
    public void DeleteSuccess()
    {

        var logDto = new LogDTO()
        {

            RowKey ="2467",
            PartitionKey = "l",
            ETag = "*"

        };

        var operationResult = new TableResult
        {

            HttpStatusCode = (int)HttpStatusCode.OK

        };

        var mockTable = new Mock<CloudTableMock>();

        mockTable
        .Setup(w => w.ExecuteAsync(It.IsAny<TableOperation>()))
        .Returns(Task.FromResult(operationResult));

        var logRepository = new ATLogRepository(mockTable.Object, "l", null);
        var result = UnityTestUtils.RunAsyncMethodSync(() => logRepository.Delete(log));
        Assert.IsTrue(result);

    }

    [Test]
    public void ReadSuccess()
    {

        log.Id = "135";
        var logDTO = mapper.ToDTO(log);

        var logList = new List<LogDTO>
        {
            logDTO
        };

        var constructorInfo = typeof(TableQuerySegment<LogDTO>)
                .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
        .FirstOrDefault(c => c.GetParameters().Count() == 1);

        var mockQuerySegment = constructorInfo.Invoke(new object[] {
            logList
        }) as TableQuerySegment<LogDTO>;

        var mockTable = new Mock<CloudTableMock>();
        mockTable
          .Setup(w => w.ExecuteQuerySegmentedAsync(It.IsAny<TableQuery<LogDTO>>(),
            It.IsAny<TableContinuationToken>()))
          .Returns(Task.FromResult(mockQuerySegment));

        var logRepository = new ATLogRepository(mockTable.Object, "l", null);

        var result = UnityTestUtils.RunAsyncMethodSync(() => logRepository.Read(logDTO.RowKey));

        // TODO 
        Assert.That(result.Id, Is.EqualTo(logDTO.RowKey));
        Assert.That(result.CreatedByID, Is.EqualTo(logDTO.ScientistID));
        Assert.That(result.CreatedOn, Is.EqualTo(logDTO.DateTime));

    }
    [Test]
    public void ReadDoesNotExistException()
    {

        var logDto = new LogDTO()
        {

            RowKey ="2467"

        };

        var constructorInfo = typeof(TableQuerySegment<LogDTO>)
                .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
        .FirstOrDefault(c => c.GetParameters().Count() == 1);

        var mockQuerySegment = constructorInfo.Invoke(new object[] {
           new List<LogDTO>()
         }) as TableQuerySegment<LogDTO>;

        var mockTable = new Mock<CloudTableMock>();
        mockTable
          .Setup(w => w.ExecuteQuerySegmentedAsync(It.IsAny<TableQuery<LogDTO>>(),
            It.IsAny<TableContinuationToken>()))
          .Returns(Task.FromResult(mockQuerySegment));

        var logRepository = new ATLogRepository(mockTable.Object, "l", null);

        Assert.Throws<ObjectDataBaseException>(
                          () => UnityTestUtils.RunAsyncMethodSync(() => logRepository.Read(logDto.RowKey)));

    }


    [Test]
    public void UpdateSuccessfull()
    {

        var logDto = new LogDTO()
        {

            RowKey ="2467",
            PartitionKey = "l",
            ETag = "*"

        };

        var operationResult = new TableResult
        {

            Result = logDto

        };

        var mockTable = new Mock<CloudTableMock>();

        mockTable
        .Setup(w => w.ExecuteAsync(It.IsAny<TableOperation>()))
        .Returns(Task.FromResult(operationResult));

        var logRepository = new ATLogRepository(mockTable.Object, "l", null);
        var result = UnityTestUtils.RunAsyncMethodSync(() => logRepository.Update(log));
        Assert.IsTrue(result);

    }

    [Test]
    public void UpdateNotSuccessfull()
    {


        var operationResult = new TableResult
        {

            Result = null

        };

        var mockTable = new Mock<CloudTableMock>();

        mockTable
        .Setup(w => w.ExecuteAsync(It.IsAny<TableOperation>()))
        .Returns(Task.FromResult(operationResult));

        var logRepository = new ATLogRepository(mockTable.Object, "l", null);
        var result = UnityTestUtils.RunAsyncMethodSync(() => logRepository.Update(log));
        Assert.IsFalse(result);

    }


}


