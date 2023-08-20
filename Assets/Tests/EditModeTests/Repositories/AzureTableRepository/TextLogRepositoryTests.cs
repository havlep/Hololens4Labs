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




public class TextLogRepositoryTests
{

    /*
    public class CloudTableMock : CloudTable
    {
        public CloudTableMock() : base(new Uri("http://sdfs.com")) { }

    }

    [Test]
    public void CreateObjectExistsException()
    {


        var textLogList = new List<TextLogDTO>
        {
            new TextLogDTO {
                TextLogID = "135"
            }
        };


        var constructorInfo = typeof(TableQuerySegment<TextLogDTO>)
                .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
        .FirstOrDefault(c => c.GetParameters().Count() == 1);

        var mockQuerySegment = constructorInfo.Invoke(new object[] {
            textLogList
        }) as TableQuerySegment<TextLogDTO>;

        var mockTable = new Mock<CloudTableMock>();
        mockTable
          .Setup(w => w.ExecuteQuerySegmentedAsync(It.IsAny<TableQuery<TextLogDTO>>(),
            It.IsAny<TableContinuationToken>()))
          .Returns(Task.FromResult(mockQuerySegment));

        var textLogRepository = new TextLogRepository(mockTable.Object, "l");
        var textLogDto = new TextLogDTO()
        {

            TextLogID ="246"

        };

        Assert.Throws<ObjectDataBaseException>(
                          () => UnityTestUtils.RunAsyncMethodSync(() => textLogRepository.Create(textLogDto)));


    }

    [Test]
    public void CreateSuccessfull()
    {

        var constructorInfo = typeof(TableQuerySegment<TextLogDTO>)
                .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
        .FirstOrDefault(c => c.GetParameters().Count() == 1);

        var mockQuerySegment = constructorInfo.Invoke(new object[] {
            new List<TextLogDTO >()
        }) as TableQuerySegment<TextLogDTO>;

        var mockTable = new Mock<CloudTableMock>();
        mockTable
          .Setup(w => w.ExecuteQuerySegmentedAsync(It.IsAny<TableQuery<TextLogDTO>>(),
            It.IsAny<TableContinuationToken>()))
          .Returns(Task.FromResult(mockQuerySegment));


        var textLogDto = new TextLogDTO()
        {
            TextLogID ="2467"

        };

        var operationResult = new TableResult
        {

            Result = textLogDto

        };

        mockTable
         .Setup(w => w.ExecuteAsync(It.IsAny<TableOperation>()))
         .Returns(Task.FromResult(operationResult));

        var textLogRepository = new TextLogRepository(mockTable.Object, "l");

        Assert.DoesNotThrow(
                         () => UnityTestUtils.RunAsyncMethodSync(() => textLogRepository.Create(textLogDto)));

        var result = UnityTestUtils.RunAsyncMethodSync(() => textLogRepository.Create(textLogDto));
        Assert.That(result, Is.EqualTo(textLogDto));

    }

    [Test]
    public void CreateOnSyncException()
    {

        var constructorInfo = typeof(TableQuerySegment<TextLogDTO>)
                .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
        .FirstOrDefault(c => c.GetParameters().Count() == 1);

        var mockQuerySegment = constructorInfo.Invoke(new object[] {
           new List<TextLogDTO>()
         }) as TableQuerySegment<TextLogDTO>;

        var mockTable = new Mock<CloudTableMock>();
        mockTable
          .Setup(w => w.ExecuteQuerySegmentedAsync(It.IsAny<TableQuery<TextLogDTO>>(),
            It.IsAny<TableContinuationToken>()))
          .Returns(Task.FromResult(mockQuerySegment));


        var textLogDto = new TextLogDTO()
        {

            TextLogID ="2467"

        };

        var operationResult = new TableResult
        {

            Result = null

        };


        mockTable
         .Setup(w => w.ExecuteAsync(It.IsAny<TableOperation>()))
         .Returns(Task.FromResult(operationResult));

        var textLogRepository = new TextLogRepository(mockTable.Object, "l");

        Assert.Throws<ObjectDataBaseException>(
                         () => UnityTestUtils.RunAsyncMethodSync(() => textLogRepository.Create(textLogDto)));


    }

    [Test]
    public void DeleteSuccess()
    {

        var textLogDto = new TextLogDTO()
        {

            TextLogID ="2467",
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

        var textLogRepository = new TextLogRepository(mockTable.Object, "l");
        var result = UnityTestUtils.RunAsyncMethodSync(() => textLogRepository.Delete(textLogDto));
        Assert.IsTrue(result);

    }

    [Test]
    public void ReadSuccess()
    {

        var textLogDTO = new TextLogDTO
        {

            TextLogID = "135"
        };

        var textLogList = new List<TextLogDTO>
        {
            textLogDTO
        };

        var constructorInfo = typeof(TableQuerySegment<TextLogDTO>)
                .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
        .FirstOrDefault(c => c.GetParameters().Count() == 1);

        var mockQuerySegment = constructorInfo.Invoke(new object[] {
            textLogList
        }) as TableQuerySegment<TextLogDTO>;

        var mockTable = new Mock<CloudTableMock>();
        mockTable
          .Setup(w => w.ExecuteQuerySegmentedAsync(It.IsAny<TableQuery<TextLogDTO>>(),
            It.IsAny<TableContinuationToken>()))
          .Returns(Task.FromResult(mockQuerySegment));

        var textLogRepository = new TextLogRepository(mockTable.Object, "l");

        var result = UnityTestUtils.RunAsyncMethodSync(() => textLogRepository.Read(textLogDTO.TextLogID));
        Assert.That(result, Is.EqualTo(textLogDTO));


    }
    [Test]
    public void ReadDoesNotExistException()
    {

        var textLogDto = new TextLogDTO()
        {

            TextLogID ="2467"

        };

        var constructorInfo = typeof(TableQuerySegment<TextLogDTO>)
                .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
        .FirstOrDefault(c => c.GetParameters().Count() == 1);

        var mockQuerySegment = constructorInfo.Invoke(new object[] {
           new List<TextLogDTO>()
         }) as TableQuerySegment<TextLogDTO>;

        var mockTable = new Mock<CloudTableMock>();
        mockTable
          .Setup(w => w.ExecuteQuerySegmentedAsync(It.IsAny<TableQuery<TextLogDTO>>(),
            It.IsAny<TableContinuationToken>()))
          .Returns(Task.FromResult(mockQuerySegment));

        var textLogRepository = new TextLogRepository(mockTable.Object, "l");

        Assert.Throws<ObjectDataBaseException>(
                          () => UnityTestUtils.RunAsyncMethodSync(() => textLogRepository.Read(textLogDto.TextLogID)));

    }


    [Test]
    public void UpdateSuccessfull()
    {

        var textLogDto = new TextLogDTO()
        {

            TextLogID ="2467",
            RowKey = "id",
            PartitionKey = "l",
            ETag = "*"

        };

        var operationResult = new TableResult
        {

            Result = textLogDto

        };

        var mockTable = new Mock<CloudTableMock>();

        mockTable
        .Setup(w => w.ExecuteAsync(It.IsAny<TableOperation>()))
        .Returns(Task.FromResult(operationResult));

        var textLogRepository = new TextLogRepository(mockTable.Object, "l");
        var result = UnityTestUtils.RunAsyncMethodSync(() => textLogRepository.Update(textLogDto));
        Assert.IsTrue(result);

    }

    [Test]
    public void UpdateNotSuccessfull()
    {

        var textLogDto = new TextLogDTO()
        {

            TextLogID ="2467",
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

        var textLogRepository = new TextLogRepository(mockTable.Object, "l");
        var result = UnityTestUtils.RunAsyncMethodSync(() => textLogRepository.Update(textLogDto));
        Assert.IsFalse(result);

    }
    */
}


