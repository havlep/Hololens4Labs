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

        public async override Task<TableResult> ExecuteAsync(TableOperation operation)
        {
            return await Task.FromResult(new TableResult
            {
                Result = new ScreenSettingEntity() { Settings = "" },
                HttpStatusCode = 200
            });
        }

        private class ScreenSettingEntity
        {
            public ScreenSettingEntity()
            {
            }

            public string Settings { get; set; }
        }
       */
    }



    [Test]
    public void ObjectExistsException()
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



}


