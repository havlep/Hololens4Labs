using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using Microsoft.WindowsAzure.Storage.Table;
using Moq;
using HoloLens4Labs.Scripts.DTOs;
using HoloLens4Labs.Scripts.Repositories;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using System.Collections.Generic;
using HoloLens4Labs.Scripts.Exceptions;
using System.Net;
using HoloLens4Labs.Tests;




public class LogRepositoryTests
{
    /*
    public class CloudTableMock : CloudTable
    {
        public CloudTableMock() : base(new Uri("http://sdfs.com")) { }

    }

    [Test]
    public void CreateObjectExistsException()
    {


        var logList = new List<LogDTO>
        {
            new LogDTO {
                LogID = "135"
            }
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

        var logRepository = new LogRepository(mockTable.Object, "l");
        var logDto = new LogDTO()
        {

            LogID ="246"

        };

        Assert.Throws<ObjectDataBaseException>(
                          () => UnityTestUtils.RunAsyncMethodSync(() => logRepository.Create(logDto)));


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


        var logDto = new LogDTO()
        {
            LogID ="2467"

        };

        var operationResult = new TableResult
        {

            Result = logDto

        };

        mockTable
         .Setup(w => w.ExecuteAsync(It.IsAny<TableOperation>()))
         .Returns(Task.FromResult(operationResult));

        var logRepository = new LogRepository(mockTable.Object, "l");

        Assert.DoesNotThrow(
                         () => UnityTestUtils.RunAsyncMethodSync(() => logRepository.Create(logDto)));

        var result = UnityTestUtils.RunAsyncMethodSync(() => logRepository.Create(logDto));
        Assert.That(result, Is.EqualTo(logDto));

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

            LogID ="2467"

        };

        var operationResult = new TableResult
        {

            Result = null

        };


        mockTable
         .Setup(w => w.ExecuteAsync(It.IsAny<TableOperation>()))
         .Returns(Task.FromResult(operationResult));

        var logRepository = new LogRepository(mockTable.Object, "l");

        Assert.Throws<ObjectDataBaseException>(
                         () => UnityTestUtils.RunAsyncMethodSync(() => logRepository.Create(logDto)));


    }

    [Test]
    public void DeleteSuccess()
    {

        var logDto = new LogDTO()
        {

            LogID ="2467",
            RowKey = "id",
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

        var logRepository = new LogRepository(mockTable.Object, "l");
        var result = UnityTestUtils.RunAsyncMethodSync(() => logRepository.Delete(logDto));
        Assert.IsTrue(result);

    }

    [Test]
    public void ReadSuccess()
    {

        var logDTO = new LogDTO
        {
 
            LogID = "135"
        };

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

        var logRepository = new LogRepository(mockTable.Object, "l");

        var result = UnityTestUtils.RunAsyncMethodSync(() => logRepository.Read(logDTO.LogID));
        Assert.That(result, Is.EqualTo(logDTO));


    }
    [Test]
    public void ReadDoesNotExistException()
    {

        var logDto = new LogDTO()
        {

            LogID ="2467"

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

        var logRepository = new LogRepository(mockTable.Object, "l");

        Assert.Throws<ObjectDataBaseException>(
                          () => UnityTestUtils.RunAsyncMethodSync(() => logRepository.Read(logDto.LogID)));

    }


    [Test]
    public void UpdateSuccessfull()
    {

        var logDto = new LogDTO()
        {

            LogID ="2467",
            RowKey = "id",
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

        var logRepository = new LogRepository(mockTable.Object, "l");
        var result = UnityTestUtils.RunAsyncMethodSync(() => logRepository.Update(logDto));
        Assert.IsTrue(result);

    }

    [Test]
    public void UpdateNotSuccessfull()
    {

        var logDto = new LogDTO()
        {

            LogID ="2467",
            RowKey = "id",
            PartitionKey = "l",
            ETag = "*"

        };

        var operationResult = new TableResult
        {

            Result = null

        };

        var mockTable = new Mock<CloudTableMock>();

        mockTable
        .Setup(w => w.ExecuteAsync(It.IsAny<TableOperation>()))
        .Returns(Task.FromResult(operationResult));

        var logRepository = new LogRepository(mockTable.Object, "l");
        var result = UnityTestUtils.RunAsyncMethodSync(() => logRepository.Update(logDto));
        Assert.IsFalse(result);

    }
    */

}


