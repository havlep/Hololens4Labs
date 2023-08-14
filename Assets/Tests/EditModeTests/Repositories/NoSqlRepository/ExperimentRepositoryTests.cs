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

public static class UnityTestUtils
{

    public static T RunAsyncMethodSync<T>(Func<Task<T>> asyncFunc)
    {
        return Task.Run(async () => await asyncFunc()).GetAwaiter().GetResult();
    }
    public static void RunAsyncMethodSync(Func<Task> asyncFunc)
    {
        Task.Run(async () => await asyncFunc()).GetAwaiter().GetResult();
    }
}


public class ExperimentRepositoryTests
{
    public  class CloudTableMock : CloudTable
    {


        public CloudTableMock() : base (new Uri("http://sdfs.com")) { }

       /* public CloudTableMock(Uri tabelAddress) : base(new Uri("http://mockuri"))
        {
        }
        public CloudTableMock(StorageUri tableAddress, StorageCredentials credentials) : base(tableAddress, credentials)
        { }

        public CloudTableMock(Uri tableAbsoluteUri, StorageCredentials credentials) : base(tableAbsoluteUri, credentials)
        { }
       */
        public async override Task<TableResult> ExecuteAsync(TableOperation operation)
        {
            return await Task.FromResult(new TableResult
            {
                
            });
        }

        private class ScreenSettingEntity
        {
            public ScreenSettingEntity()
            {
            }

            public string Settings { get; set; }
        }
       
    }



    [Test]
    public void CreateObjectExistsException()
    {


        var experimentList = new List<ExperimentDTO>
        {
            new ExperimentDTO {
                Name = "experiment1",
                ExperimentID = "135"
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

        var experimentRepository = new ExperimentRepository(mockTable.Object, "l");
        var experimentDto = new ExperimentDTO() { 
        
        Name = "Experiment2",
        ExperimentID ="246"
        
        };      

        Assert.Throws<ObjectDataBaseException>(
                          () => UnityTestUtils.RunAsyncMethodSync(() => experimentRepository.Create(experimentDto)));


    }

    [Test]
    public void CreateSuccessfull()
    {


        var experimentList = new List<ExperimentDTO>();


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


        var experimentDto = new ExperimentDTO()
        {

            Name = "Experiment2",
            ExperimentID ="2467"

        };

        var operationResult = new TableResult {

            Result = experimentDto
        
        };
       

        mockTable
         .Setup(w => w.ExecuteAsync(It.IsAny<TableOperation>()))
         .Returns(Task.FromResult(operationResult));

        var ot = mockTable.Object;

        var res = ot.ExecuteAsync(TableOperation.InsertOrMerge(experimentDto));
        
        ExperimentDTO dt = (ExperimentDTO) res.Result.Result;

        Assert.That(dt.Name,Is.EqualTo(experimentDto.Name));

        var experimentRepository = new ExperimentRepository(mockTable.Object, "l");
        

        Assert.DoesNotThrow(
                         () => UnityTestUtils.RunAsyncMethodSync(() => experimentRepository.Create(experimentDto)));

        var result = UnityTestUtils.RunAsyncMethodSync( () => experimentRepository.Create(experimentDto));
        Assert.That(result, Is.EqualTo(experimentDto));


    }

    [Test]
    public void CreateOnSyncException()
    {


        var experimentList = new List<ExperimentDTO>();


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


        var experimentDto = new ExperimentDTO()
        {

            Name = "Experiment2",
            ExperimentID ="2467"

        };

        var operationResult = new TableResult
        {

            Result = null

        };


        mockTable
         .Setup(w => w.ExecuteAsync(It.IsAny<TableOperation>()))
         .Returns(Task.FromResult(operationResult));

        var experimentRepository = new ExperimentRepository(mockTable.Object, "l");

        Assert.Throws<ObjectDataBaseException>(
                         () => UnityTestUtils.RunAsyncMethodSync(() => experimentRepository.Create(experimentDto)));


    }





}


