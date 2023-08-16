using NUnit.Framework;
using HoloLens4Labs.Scripts.Mappers;
using HoloLens4Labs.Scripts.Model;

public class ExperimentMapperTests
{
    Experiment experiment;
    

    [SetUp]
    public void Init()
    {

        experiment = new Experiment("12", "Michelson-Morely","12");


    }

    [Test]
    public void ToDTOTest()
    {

        ExperimentMapper mapper = new ExperimentMapper();
        var dto = mapper.ToDTO(experiment);

        Assert.IsNotNull(dto);
        Assert.AreEqual(experiment.Id.ToString(), dto.ExperimentID);
        Assert.AreEqual(experiment.Name, dto.Name);
      
    }

    [Test]
    public void CreateDTOTest()
    {

        ExperimentMapper mapper = new ExperimentMapper();
        var dto = mapper.ToDTO(experiment);
        var cdto = mapper.CreateDTO(experiment);

        Assert.IsNotNull(cdto);
        Assert.IsNotNull(dto);
        Assert.AreEqual(cdto.ExperimentID, dto.ExperimentID);
        Assert.AreEqual(cdto.Name, dto.Name);


    }
}

