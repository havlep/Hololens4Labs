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





public class ScientistRepositoryTests
{
    public class CloudTableMock : CloudTable
    {
        public CloudTableMock() : base(new Uri("http://sdfs.com")) { }

    }

    [Test]
    public void CreateObjectExistsException()
    {


        var scientistList = new List<ScientistDTO>
        {
            new ScientistDTO {
                Name = "scientist1",
                ScientistID = "135"
            }
        };


        var constructorInfo = typeof(TableQuerySegment<ScientistDTO>)
                .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
        .FirstOrDefault(c => c.GetParameters().Count() == 1);

        var mockQuerySegment = constructorInfo.Invoke(new object[] {
            scientistList
        }) as TableQuerySegment<ScientistDTO>;

        var mockTable = new Mock<CloudTableMock>();
        mockTable
          .Setup(w => w.ExecuteQuerySegmentedAsync(It.IsAny<TableQuery<ScientistDTO>>(),
            It.IsAny<TableContinuationToken>()))
          .Returns(Task.FromResult(mockQuerySegment));

        var scientistRepository = new ScientistRepository(mockTable.Object, "l");
        var scientistDto = new ScientistDTO()
        {

            Name = "Scientist2",
            ScientistID ="246"

        };

        Assert.Throws<ObjectDataBaseException>(
                          () => UnityTestUtils.RunAsyncMethodSync(() => scientistRepository.Create(scientistDto)));


    }

    [Test]
    public void CreateSuccessfull()
    {

        var constructorInfo = typeof(TableQuerySegment<ScientistDTO>)
                .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
        .FirstOrDefault(c => c.GetParameters().Count() == 1);

        var mockQuerySegment = constructorInfo.Invoke(new object[] {
            new List<ScientistDTO >()
        }) as TableQuerySegment<ScientistDTO>;

        var mockTable = new Mock<CloudTableMock>();
        mockTable
          .Setup(w => w.ExecuteQuerySegmentedAsync(It.IsAny<TableQuery<ScientistDTO>>(),
            It.IsAny<TableContinuationToken>()))
          .Returns(Task.FromResult(mockQuerySegment));


        var scientistDto = new ScientistDTO()
        {

            Name = "Scientist2",
            ScientistID ="2467"

        };

        var operationResult = new TableResult
        {

            Result = scientistDto

        };

        mockTable
         .Setup(w => w.ExecuteAsync(It.IsAny<TableOperation>()))
         .Returns(Task.FromResult(operationResult));

        var scientistRepository = new ScientistRepository(mockTable.Object, "l");

        Assert.DoesNotThrow(
                         () => UnityTestUtils.RunAsyncMethodSync(() => scientistRepository.Create(scientistDto)));

        var result = UnityTestUtils.RunAsyncMethodSync(() => scientistRepository.Create(scientistDto));
        Assert.That(result, Is.EqualTo(scientistDto));

    }

    [Test]
    public void CreateOnSyncException()
    {

        var constructorInfo = typeof(TableQuerySegment<ScientistDTO>)
                .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
        .FirstOrDefault(c => c.GetParameters().Count() == 1);

        var mockQuerySegment = constructorInfo.Invoke(new object[] {
           new List<ScientistDTO>()
         }) as TableQuerySegment<ScientistDTO>;

        var mockTable = new Mock<CloudTableMock>();
        mockTable
          .Setup(w => w.ExecuteQuerySegmentedAsync(It.IsAny<TableQuery<ScientistDTO>>(),
            It.IsAny<TableContinuationToken>()))
          .Returns(Task.FromResult(mockQuerySegment));


        var scientistDto = new ScientistDTO()
        {

            Name = "Scientist2",
            ScientistID ="2467"

        };

        var operationResult = new TableResult
        {

            Result = null

        };


        mockTable
         .Setup(w => w.ExecuteAsync(It.IsAny<TableOperation>()))
         .Returns(Task.FromResult(operationResult));

        var scientistRepository = new ScientistRepository(mockTable.Object, "l");

        Assert.Throws<ObjectDataBaseException>(
                         () => UnityTestUtils.RunAsyncMethodSync(() => scientistRepository.Create(scientistDto)));


    }

    [Test]
    public void DeleteSuccess()
    {

        var scientistDto = new ScientistDTO()
        {

            Name = "Scientist",
            ScientistID ="2467",
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

        var scientistRepository = new ScientistRepository(mockTable.Object, "l");
        var result = UnityTestUtils.RunAsyncMethodSync(() => scientistRepository.Delete(scientistDto));
        Assert.IsTrue(result);

    }

    [Test]
    public void ReadSuccess()
    {

        var scientistDTO = new ScientistDTO
        {
            Name = "scientist1",
            ScientistID = "135"
        };

        var scientistList = new List<ScientistDTO>
        {
            scientistDTO
        };

        var constructorInfo = typeof(TableQuerySegment<ScientistDTO>)
                .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
        .FirstOrDefault(c => c.GetParameters().Count() == 1);

        var mockQuerySegment = constructorInfo.Invoke(new object[] {
            scientistList
        }) as TableQuerySegment<ScientistDTO>;

        var mockTable = new Mock<CloudTableMock>();
        mockTable
          .Setup(w => w.ExecuteQuerySegmentedAsync(It.IsAny<TableQuery<ScientistDTO>>(),
            It.IsAny<TableContinuationToken>()))
          .Returns(Task.FromResult(mockQuerySegment));

        var scientistRepository = new ScientistRepository(mockTable.Object, "l");

        var result = UnityTestUtils.RunAsyncMethodSync(() => scientistRepository.Read(scientistDTO.ScientistID));
        Assert.That(result, Is.EqualTo(scientistDTO));


    }
    [Test]
    public void ReadDoesNotExistException()
    {

        var scientistDto = new ScientistDTO()
        {

            Name = "Scientist2",
            ScientistID ="2467"

        };

        var constructorInfo = typeof(TableQuerySegment<ScientistDTO>)
                .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
        .FirstOrDefault(c => c.GetParameters().Count() == 1);

        var mockQuerySegment = constructorInfo.Invoke(new object[] {
           new List<ScientistDTO>()
         }) as TableQuerySegment<ScientistDTO>;

        var mockTable = new Mock<CloudTableMock>();
        mockTable
          .Setup(w => w.ExecuteQuerySegmentedAsync(It.IsAny<TableQuery<ScientistDTO>>(),
            It.IsAny<TableContinuationToken>()))
          .Returns(Task.FromResult(mockQuerySegment));

        var scientistRepository = new ScientistRepository(mockTable.Object, "l");

        Assert.Throws<ObjectDataBaseException>(
                          () => UnityTestUtils.RunAsyncMethodSync(() => scientistRepository.Read(scientistDto.ScientistID)));

    }


    [Test]
    public void UpdateSuccessfull()
    {

        var scientistDto = new ScientistDTO()
        {

            Name = "Scientist",
            ScientistID ="2467",
            RowKey = "id",
            PartitionKey = "l",
            ETag = "*"

        };

        var operationResult = new TableResult
        {

            Result = scientistDto

        };

        var mockTable = new Mock<CloudTableMock>();

        mockTable
        .Setup(w => w.ExecuteAsync(It.IsAny<TableOperation>()))
        .Returns(Task.FromResult(operationResult));

        var scientistRepository = new ScientistRepository(mockTable.Object, "l");
        var result = UnityTestUtils.RunAsyncMethodSync(() => scientistRepository.Update(scientistDto));
        Assert.IsTrue(result);

    }

    [Test]
    public void UpdateNotSuccessfull()
    {

        var scientistDto = new ScientistDTO()
        {

            Name = "Scientist",
            ScientistID ="2467",
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

        var scientistRepository = new ScientistRepository(mockTable.Object, "l");
        var result = UnityTestUtils.RunAsyncMethodSync(() => scientistRepository.Update(scientistDto));
        Assert.IsFalse(result);

    }

}


