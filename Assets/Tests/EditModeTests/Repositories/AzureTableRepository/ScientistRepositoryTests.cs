using HoloLens4Labs.Scripts.DTOs;
using HoloLens4Labs.Scripts.Exceptions;
using HoloLens4Labs.Scripts.Model;
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

public class ScientistRepositoryTests
{
    public class CloudTableMock : CloudTable
    {
        public CloudTableMock() : base(new Uri("http://sdfs.com")) { }

    }

    [Test]
    public void CreateObjectExistsException()
    {


        var scientistdto = new ScientistDTO
        {
            Name = "scientist1",
            ScientistID = "135"
        };

        var scientistList = new List<ScientistDTO>
        {
            scientistdto
        };

        var scientist = new Scientist(scientistdto.ScientistID, scientistdto.Name);

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

        var scientistRepository = new ATScientistRepository(mockTable.Object, "l");


        Assert.Throws<ObjectDataBaseException>(
                          () => UnityTestUtils.RunAsyncMethodSync(() => scientistRepository.Create(scientist)));


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
            ScientistID ="2467",
            RowKey = "2467"

        };

        var scientist = new Scientist(scientistDto.Name);


        var operationResult = new TableResult
        {

            Result = scientistDto

        };

        mockTable
         .Setup(w => w.ExecuteAsync(It.IsAny<TableOperation>()))
         .Returns(Task.FromResult(operationResult));

        var scientistRepository = new ATScientistRepository(mockTable.Object, "l");

        Assert.DoesNotThrow(
                         () => UnityTestUtils.RunAsyncMethodSync(() => scientistRepository.Create(scientist)));

        Scientist result = UnityTestUtils.RunAsyncMethodSync(() => scientistRepository.Create(scientist));
        Assert.That(result.Id, Is.EqualTo(scientistDto.RowKey));
        Assert.That(result.Name, Is.EqualTo(scientistDto.Name));


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

        var scientist = new Scientist("2467", "Scientist");


        mockTable
         .Setup(w => w.ExecuteAsync(It.IsAny<TableOperation>()))
         .Returns(Task.FromResult(operationResult));

        var scientistRepository = new ATScientistRepository(mockTable.Object, "l");

        Assert.Throws<ObjectDataBaseException>(
                         () => UnityTestUtils.RunAsyncMethodSync(() => scientistRepository.Create(scientist)));


    }

    [Test]
    public void DeleteSuccess()
    {


        var scientist = new Scientist("2467", "Scientist");


        var operationResult = new TableResult
        {

            HttpStatusCode = (int)HttpStatusCode.OK

        };

        var mockTable = new Mock<CloudTableMock>();

        mockTable
        .Setup(w => w.ExecuteAsync(It.IsAny<TableOperation>()))
        .Returns(Task.FromResult(operationResult));

        var scientistRepository = new ATScientistRepository(mockTable.Object, "l");
        var result = UnityTestUtils.RunAsyncMethodSync(() => scientistRepository.Delete(scientist));
        Assert.IsTrue(result);

    }

    [Test]
    public void ReadSuccess()
    {

        var scientist = new Scientist("2467", "Scientist");

        var scientistDTO = new ScientistDTO
        {
            Name = scientist.Name,
            ScientistID = scientist.Id,
            RowKey = scientist.Id
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

        var scientistRepository = new ATScientistRepository(mockTable.Object, "l");

        var result = UnityTestUtils.RunAsyncMethodSync(() => scientistRepository.Read(scientistDTO.ScientistID));
        Assert.That(result.Id, Is.EqualTo(scientist.Id));
        Assert.That(result.Name, Is.EqualTo(scientist.Name));


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

        var scientistRepository = new ATScientistRepository(mockTable.Object, "l");

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


        var scientist = new Scientist("2467", "Scientist");

        var operationResult = new TableResult
        {

            Result = scientistDto

        };

        var mockTable = new Mock<CloudTableMock>();

        mockTable
        .Setup(w => w.ExecuteAsync(It.IsAny<TableOperation>()))
        .Returns(Task.FromResult(operationResult));

        var scientistRepository = new ATScientistRepository(mockTable.Object, "l");
        var result = UnityTestUtils.RunAsyncMethodSync(() => scientistRepository.Update(scientist));
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

        var scientist = new Scientist("2467", "Scientist");

        var operationResult = new TableResult
        {

            Result = null

        };

        var mockTable = new Mock<CloudTableMock>();

        mockTable
        .Setup(w => w.ExecuteAsync(It.IsAny<TableOperation>()))
        .Returns(Task.FromResult(operationResult));

        var scientistRepository = new ATScientistRepository(mockTable.Object, "l");
        var result = UnityTestUtils.RunAsyncMethodSync(() => scientistRepository.Update(scientist));
        Assert.IsFalse(result);

    }

}


