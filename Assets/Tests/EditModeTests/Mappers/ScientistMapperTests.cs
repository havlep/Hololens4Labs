using HoloLens4Labs.Scripts.Mappers;
using HoloLens4Labs.Scripts.Model;
using NUnit.Framework;

public class ScientistMapperTests
{
    Scientist scientist;


    [SetUp]
    public void Init()
    {

        scientist = new Scientist("12", "Michelson-Morely","");


    }

    [Test]
    public void ToDTOTest()
    {

        ScientistMapper mapper = new ScientistMapper();
        var dto = mapper.ToDTO(scientist);

        Assert.IsNotNull(dto);
        Assert.AreEqual(scientist.Id.ToString(), dto.ScientistID);
        Assert.AreEqual(scientist.Name, dto.Name);

    }

    [Test]
    public void CreateDTOTest()
    {

        ScientistMapper mapper = new ScientistMapper();
        var dto = mapper.ToDTO(scientist);
        var cdto = mapper.CreateDTO(scientist);

        Assert.IsNotNull(cdto);
        Assert.IsNotNull(dto);
        Assert.AreEqual(cdto.ScientistID, dto.ScientistID);
        Assert.AreEqual(cdto.Name, dto.Name);


    }
}
