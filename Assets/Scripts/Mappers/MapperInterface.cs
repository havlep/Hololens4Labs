
namespace HoloLens4Labs.Scripts.Mappers { 
    public interface MapperInterface<OBJ, DTO, CDTO> 
    {
       
        DTO ToDTO(OBJ obj);
        CDTO CreateDTO(OBJ obj);
        OBJ ToOBJ(DTO dto);
    }
}