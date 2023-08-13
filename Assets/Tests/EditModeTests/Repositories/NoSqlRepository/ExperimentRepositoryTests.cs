using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using Microsoft.WindowsAzure.Storage.Table;
using Moq;
using HoloLens4Labs.Scripts.DTOs;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq;

public class ExperimentRepositoryTests
{

    Mock<CloudTableMock> mockTable;
    TableQuerySegment<ExperimentDTO> mockQuerySegment;

    public  class CloudTableMock : CloudTable
    {
        public CloudTableMock() : base(new Uri("http://mockuri"))
        {
        }
    }


    [SetUp]
    public void Init()
    {

  /*      var ctor = typeof(TableQuerySegment<ExperimentDTO>)
                .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
        .FirstOrDefault(c => c.GetParameters().Count() == 1);

        mockQuerySegment = ctor.Invoke(new object[] {
            new ExperimentDTO() {

                Name = "test",
                ExperimentID = "123"

             }
        }) as TableQuerySegment<ExperimentDTO>;

        mockTable = new Mock<CloudTableMock>();
        mockTable
          .Setup(w => w.ExecuteQuerySegmentedAsync(It.IsAny<TableQuery<ExperimentDTO>>(),
            It.IsAny<EntityResolver<ExperimentDTO>>(),
            It.IsAny<TableContinuationToken>()))
          .Returns(Task.FromResult(mockQuerySegment));

        */

    }


    // A Test behaves as an ordinary method
    [Test]
    public void ExperimentRepositoryTestsSimplePasses()
    {
        // Use the Assert class to test conditions
        var ctor = typeof(TableQuerySegment<ExperimentDTO>)
                .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
        .FirstOrDefault(c => c.GetParameters().Count() == 1);
        mockQuerySegment = ctor.Invoke(new object[] {
            new System.Collections.Generic.List<ExperimentDTO> () 
        }) as TableQuerySegment<ExperimentDTO>;

        mockTable = new Mock<CloudTableMock>();
        mockTable
          .Setup(w => w.ExecuteQuerySegmentedAsync(It.IsAny<TableQuery<ExperimentDTO>>(),
            It.IsAny<EntityResolver<ExperimentDTO>>(),
            It.IsAny<TableContinuationToken>()))
          .Returns(Task.FromResult(mockQuerySegment));



    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator ExperimentRepositoryTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}