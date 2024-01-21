// Copyright (c) Petr Havel 2023.
// Licensed under the MIT License.

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

public class ExperimentRepositoryTests
{
    public class CloudTableMock : CloudTable
    {
        public CloudTableMock() : base(new Uri("http://sdfs.com")) { }

    }

    [Test]
    public void CreateObjectExistsException()
    {


        var experimentList = new List<ExperimentDTO>
        {
            new ExperimentDTO {
                Name = "experiment1",
                ExperimentID = "135",
                RowKey = "135"
            }
        };


        var constructorInfo = typeof(TableQuerySegment<ExperimentDTO>)
                .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
        .FirstOrDefault(c => c.GetParameters().Count() == 1);

        var mockQuerySegment = constructorInfo.Invoke(new object[] {
            experimentList
        }) as TableQuerySegment<ExperimentDTO>;

        var mockTable = new Mock<CloudTableMock>();
        mockTable
          .Setup(w => w.ExecuteQuerySegmentedAsync(It.IsAny<TableQuery<ExperimentDTO>>(),
            It.IsAny<TableContinuationToken>()))
          .Returns(Task.FromResult(mockQuerySegment));

        var experimentRepository = new ATExperimentRepository(mockTable.Object, "l");
        var experiment = new Experiment("Experiment", "11", DateTime.Now);

        Assert.Throws<ObjectDataBaseException>(
                          () => UnityTestUtils.RunAsyncMethodSync(() => experimentRepository.Create(experiment)));


    }

    [Test]
    public void CreateSuccessfull()
    {

        var constructorInfo = typeof(TableQuerySegment<ExperimentDTO>)
                .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
        .FirstOrDefault(c => c.GetParameters().Count() == 1);

        var mockQuerySegment = constructorInfo.Invoke(new object[] {
            new List<ExperimentDTO >()
        }) as TableQuerySegment<ExperimentDTO>;

        var mockTable = new Mock<CloudTableMock>();
        mockTable
          .Setup(w => w.ExecuteQuerySegmentedAsync(It.IsAny<TableQuery<ExperimentDTO>>(),
            It.IsAny<TableContinuationToken>()))
          .Returns(Task.FromResult(mockQuerySegment));


        var experiment = new Experiment("Experiment2", "12", DateTime.Now);
        var experimentDTO = new ExperimentDTO()
        {

            Name = experiment.Name,
            ScientistsID = experiment.CreatedByID

        };


        var operationResult = new TableResult
        {

            Result = experimentDTO

        };

        mockTable
         .Setup(w => w.ExecuteAsync(It.IsAny<TableOperation>()))
         .Returns(Task.FromResult(operationResult));

        var experimentRepository = new ATExperimentRepository(mockTable.Object, "l");

        Assert.DoesNotThrow(
                         () => UnityTestUtils.RunAsyncMethodSync(() => experimentRepository.Create(experiment)));

        var result = UnityTestUtils.RunAsyncMethodSync(() => experimentRepository.Create(experiment));
        Assert.That(result.Id, Is.EqualTo(experimentDTO.ExperimentID));
        Assert.That(result.Name, Is.EqualTo(experimentDTO.Name));
        Assert.That(result.CreatedByID, Is.EqualTo(experimentDTO.ScientistsID));



    }

    [Test]
    public void CreateOnSyncException()
    {

        var constructorInfo = typeof(TableQuerySegment<ExperimentDTO>)
                .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
        .FirstOrDefault(c => c.GetParameters().Count() == 1);

        var mockQuerySegment = constructorInfo.Invoke(new object[] {
           new List<ExperimentDTO>()
         }) as TableQuerySegment<ExperimentDTO>;

        var mockTable = new Mock<CloudTableMock>();
        mockTable
          .Setup(w => w.ExecuteQuerySegmentedAsync(It.IsAny<TableQuery<ExperimentDTO>>(),
            It.IsAny<TableContinuationToken>()))
          .Returns(Task.FromResult(mockQuerySegment));


        var experiment = new Experiment("Experiment2", "2467", DateTime.Now);

        var operationResult = new TableResult
        {

            Result = null

        };


        mockTable
         .Setup(w => w.ExecuteAsync(It.IsAny<TableOperation>()))
         .Returns(Task.FromResult(operationResult));

        var experimentRepository = new ATExperimentRepository(mockTable.Object, "l");

        Assert.Throws<ObjectDataBaseException>(
                         () => UnityTestUtils.RunAsyncMethodSync(() => experimentRepository.Create(experiment)));


    }

    [Test]
    public void DeleteSuccess()
    {

        var experiment = new Experiment("2467", "sdfsdf", "21", DateTime.Now);

        var operationResult = new TableResult
        {

            HttpStatusCode = (int)HttpStatusCode.OK

        };

        var mockTable = new Mock<CloudTableMock>();

        mockTable
        .Setup(w => w.ExecuteAsync(It.IsAny<TableOperation>()))
        .Returns(Task.FromResult(operationResult));

        var experimentRepository = new ATExperimentRepository(mockTable.Object, "l");
        var result = UnityTestUtils.RunAsyncMethodSync(() => experimentRepository.Delete(experiment));
        Assert.IsTrue(result);

    }

    [Test]
    public void ReadSuccess()
    {

        var experimentDTO = new ExperimentDTO
        {
            Name = "experiment1",
            ExperimentID = "135",
            ScientistsID = "12",
            RowKey = "135"
        };

        var experimentList = new List<ExperimentDTO>
        {
            experimentDTO
        };

        var constructorInfo = typeof(TableQuerySegment<ExperimentDTO>)
                .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
        .FirstOrDefault(c => c.GetParameters().Count() == 1);

        var mockQuerySegment = constructorInfo.Invoke(new object[] {
            experimentList
        }) as TableQuerySegment<ExperimentDTO>;

        var mockTable = new Mock<CloudTableMock>();
        mockTable
          .Setup(w => w.ExecuteQuerySegmentedAsync(It.IsAny<TableQuery<ExperimentDTO>>(),
            It.IsAny<TableContinuationToken>()))
          .Returns(Task.FromResult(mockQuerySegment));

        var experimentRepository = new ATExperimentRepository(mockTable.Object, "l");

        var result = UnityTestUtils.RunAsyncMethodSync(() => experimentRepository.Read(experimentDTO.ExperimentID));
        Assert.That(result.Id, Is.EqualTo(experimentDTO.ExperimentID));
        Assert.That(result.Name, Is.EqualTo(experimentDTO.Name));
        Assert.That(result.CreatedByID, Is.EqualTo(experimentDTO.ScientistsID));


    }
    [Test]
    public void ReadDoesNotExistException()
    {

        var experimentDto = new ExperimentDTO()
        {

            Name = "Experiment2",
            ExperimentID ="2467"

        };

        var constructorInfo = typeof(TableQuerySegment<ExperimentDTO>)
                .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
        .FirstOrDefault(c => c.GetParameters().Count() == 1);

        var mockQuerySegment = constructorInfo.Invoke(new object[] {
           new List<ExperimentDTO>()
         }) as TableQuerySegment<ExperimentDTO>;

        var mockTable = new Mock<CloudTableMock>();
        mockTable
          .Setup(w => w.ExecuteQuerySegmentedAsync(It.IsAny<TableQuery<ExperimentDTO>>(),
            It.IsAny<TableContinuationToken>()))
          .Returns(Task.FromResult(mockQuerySegment));

        var experimentRepository = new ATExperimentRepository(mockTable.Object, "l");

        Assert.Throws<ObjectDataBaseException>(
                          () => UnityTestUtils.RunAsyncMethodSync(() => experimentRepository.Read(experimentDto.ExperimentID)));

    }


    [Test]
    public void UpdateSuccessfull()
    {

        var experimentDto = new ExperimentDTO()
        {

            Name = "Experiment",
            ExperimentID ="2467",
            RowKey = "id",
            PartitionKey = "l",
            ETag = "*"

        };

        var experiment = new Experiment("12", "testexp", "13", DateTime.Now);

        var operationResult = new TableResult
        {

            Result = experimentDto

        };

        var mockTable = new Mock<CloudTableMock>();

        mockTable
        .Setup(w => w.ExecuteAsync(It.IsAny<TableOperation>()))
        .Returns(Task.FromResult(operationResult));

        var experimentRepository = new ATExperimentRepository(mockTable.Object, "l");
        var result = UnityTestUtils.RunAsyncMethodSync(() => experimentRepository.Update(experiment));
        Assert.IsTrue(result);

    }

    [Test]
    public void UpdateNotSuccessfull()
    {

        var experimentDto = new ExperimentDTO()
        {

            Name = "Experiment",
            ExperimentID ="2467",
            RowKey = "id",
            PartitionKey = "l",
            ETag = "*"

        };

        var experiment = new Experiment("12", "testexp", "13", DateTime.Now);


        var operationResult = new TableResult
        {

            Result = null

        };

        var mockTable = new Mock<CloudTableMock>();

        mockTable
        .Setup(w => w.ExecuteAsync(It.IsAny<TableOperation>()))
        .Returns(Task.FromResult(operationResult));

        var experimentRepository = new ATExperimentRepository(mockTable.Object, "l");
        var result = UnityTestUtils.RunAsyncMethodSync(() => experimentRepository.Update(experiment));
        Assert.IsFalse(result);

    }

}


