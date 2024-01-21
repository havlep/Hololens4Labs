// Copyright (c) Petr Havel 2023.
// Licensed under the MIT License.

using HoloLens4Labs.Scripts.DTOs;
using HoloLens4Labs.Scripts.Model.DataTypes;

namespace HoloLens4Labs.Scripts.Mappers
{
    public class TextDataMapper : MapperInterface<TextData, TextDataDTO, TextDataDTO>
    {
        public TextDataDTO CreateDTO(TextData obj)
        {
            return ToDTO(obj);
        }

        public TextDataDTO ToDTO(TextData obj)
        {
            TextDataDTO dto = new TextDataDTO();

            dto.TextDataID = obj.Id;
            dto.TextLogID = obj.DoneWithinLogID;
            dto.ScientistID = obj.CreatedById;
            dto.Created = obj.CreatedOn;
            dto.Text = obj.Text;
            dto.RowKey = obj.Id;

            return dto;

        }

        public TextData ToOBJ(TextDataDTO dto)
        {
            throw new System.NotImplementedException();
        }
    }
}