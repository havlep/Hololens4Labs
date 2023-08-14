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




public class TextDataRepositoryTests
{
    public class CloudTableMock : CloudTable
    {
        public CloudTableMock() : base(new Uri("http://sdfs.com")) { }

    }

    [Test]
    public void CreateObjectExistsException()
    {


        var textDataList = new List<TextDataDTO>
        {
            new TextDataDTO {
                TextDataID = "135"
            }
        };


        var constructorInfo = typeof(TableQuerySegment<TextDataDTO>)
                .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
        .FirstOrDefault(c => c.GetParameters().Count() == 1);

        var mockQuerySegment = constructorInfo.Invoke(new object[] {
            textDataList
        }) as TableQuerySegment<TextDataDTO>;

        var mockTable = new Mock<CloudTableMock>();
        mockTable
          .Setup(w => w.ExecuteQuerySegmentedAsync(It.IsAny<TableQuery<TextDataDTO>>(),
            It.IsAny<TableContinuationToken>()))
          .Returns(Task.FromResult(mockQuerySegment));

        var textDataRepository = new TextDataRepository(mockTable.Object, "l");
        var textDataDto = new TextDataDTO()
        {

            TextDataID ="246"

        };

        Assert.Throws<ObjectDataBaseException>(
                          () => UnityTestUtils.RunAsyncMethodSync(() => textDataRepository.Create(textDataDto)));


    }

    [Test]
    public void CreateSuccessfull()
    {

        var constructorInfo = typeof(TableQuerySegment<TextDataDTO>)
                .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
        .FirstOrDefault(c => c.GetParameters().Count() == 1);

        var mockQuerySegment = constructorInfo.Invoke(new object[] {
            new List<TextDataDTO >()
        }) as TableQuerySegment<TextDataDTO>;

        var mockTable = new Mock<CloudTableMock>();
        mockTable
          .Setup(w => w.ExecuteQuerySegmentedAsync(It.IsAny<TableQuery<TextDataDTO>>(),
            It.IsAny<TableContinuationToken>()))
          .Returns(Task.FromResult(mockQuerySegment));


        var textDataDto = new TextDataDTO()
        {
            TextDataID ="2467"

        };

        var operationResult = new TableResult
        {

            Result = textDataDto

        };

        mockTable
         .Setup(w => w.ExecuteAsync(It.IsAny<TableOperation>()))
         .Returns(Task.FromResult(operationResult));

        var textDataRepository = new TextDataRepository(mockTable.Object, "l");

        Assert.DoesNotThrow(
                         () => UnityTestUtils.RunAsyncMethodSync(() => textDataRepository.Create(textDataDto)));

        var result = UnityTestUtils.RunAsyncMethodSync(() => textDataRepository.Create(textDataDto));
        Assert.That(result, Is.EqualTo(textDataDto));

    }

    [Test]
    public void CreateOnSyncException()
    {

        var constructorInfo = typeof(TableQuerySegment<TextDataDTO>)
                .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
        .FirstOrDefault(c => c.GetParameters().Count() == 1);

        var mockQuerySegment = constructorInfo.Invoke(new object[] {
           new List<TextDataDTO>()
         }) as TableQuerySegment<TextDataDTO>;

        var mockTable = new Mock<CloudTableMock>();
        mockTable
          .Setup(w => w.ExecuteQuerySegmentedAsync(It.IsAny<TableQuery<TextDataDTO>>(),
            It.IsAny<TableContinuationToken>()))
          .Returns(Task.FromResult(mockQuerySegment));


        var textDataDto = new TextDataDTO()
        {

            TextDataID ="2467"

        };

        var operationResult = new TableResult
        {

            Result = null

        };


        mockTable
         .Setup(w => w.ExecuteAsync(It.IsAny<TableOperation>()))
         .Returns(Task.FromResult(operationResult));

        var textDataRepository = new TextDataRepository(mockTable.Object, "l");

        Assert.Throws<ObjectDataBaseException>(
                         () => UnityTestUtils.RunAsyncMethodSync(() => textDataRepository.Create(textDataDto)));


    }

    [Test]
    public void DeleteSuccess()
    {

        var textDataDto = new TextDataDTO()
        {

            TextDataID ="2467",
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

        var textDataRepository = new TextDataRepository(mockTable.Object, "l");
        var result = UnityTestUtils.RunAsyncMethodSync(() => textDataRepository.Delete(textDataDto));
        Assert.IsTrue(result);

    }

    [Test]
    public void ReadSuccess()
    {

        var textDataDTO = new TextDataDTO
        {

            TextDataID = "135"
        };

        var textDataList = new List<TextDataDTO>
        {
            textDataDTO
        };

        var constructorInfo = typeof(TableQuerySegment<TextDataDTO>)
                .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
        .FirstOrDefault(c => c.GetParameters().Count() == 1);

        var mockQuerySegment = constructorInfo.Invoke(new object[] {
            textDataList
        }) as TableQuerySegment<TextDataDTO>;

        var mockTable = new Mock<CloudTableMock>();
        mockTable
          .Setup(w => w.ExecuteQuerySegmentedAsync(It.IsAny<TableQuery<TextDataDTO>>(),
            It.IsAny<TableContinuationToken>()))
          .Returns(Task.FromResult(mockQuerySegment));

        var textDataRepository = new TextDataRepository(mockTable.Object, "l");

        var result = UnityTestUtils.RunAsyncMethodSync(() => textDataRepository.Read(textDataDTO.TextDataID));
        Assert.That(result, Is.EqualTo(textDataDTO));


    }
    [Test]
    public void ReadDoesNotExistException()
    {

        var textDataDto = new TextDataDTO()
        {

            TextDataID ="2467"

        };

        var constructorInfo = typeof(TableQuerySegment<TextDataDTO>)
                .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
        .FirstOrDefault(c => c.GetParameters().Count() == 1);

        var mockQuerySegment = constructorInfo.Invoke(new object[] {
           new List<TextDataDTO>()
         }) as TableQuerySegment<TextDataDTO>;

        var mockTable = new Mock<CloudTableMock>();
        mockTable
          .Setup(w => w.ExecuteQuerySegmentedAsync(It.IsAny<TableQuery<TextDataDTO>>(),
            It.IsAny<TableContinuationToken>()))
          .Returns(Task.FromResult(mockQuerySegment));

        var textDataRepository = new TextDataRepository(mockTable.Object, "l");

        Assert.Throws<ObjectDataBaseException>(
                          () => UnityTestUtils.RunAsyncMethodSync(() => textDataRepository.Read(textDataDto.TextDataID)));

    }


    [Test]
    public void UpdateSuccessfull()
    {

        var textDataDto = new TextDataDTO()
        {

            TextDataID ="2467",
            RowKey = "id",
            PartitionKey = "l",
            ETag = "*"

        };

        var operationResult = new TableResult
        {

            Result = textDataDto

        };

        var mockTable = new Mock<CloudTableMock>();

        mockTable
        .Setup(w => w.ExecuteAsync(It.IsAny<TableOperation>()))
        .Returns(Task.FromResult(operationResult));

        var textDataRepository = new TextDataRepository(mockTable.Object, "l");
        var result = UnityTestUtils.RunAsyncMethodSync(() => textDataRepository.Update(textDataDto));
        Assert.IsTrue(result);

    }

    [Test]
    public void UpdateNotSuccessfull()
    {

        var textDataDto = new TextDataDTO()
        {

            TextDataID ="2467",
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

        var textDataRepository = new TextDataRepository(mockTable.Object, "l");
        var result = UnityTestUtils.RunAsyncMethodSync(() => textDataRepository.Update(textDataDto));
        Assert.IsFalse(result);

    }

}



