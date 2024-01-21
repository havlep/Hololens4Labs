// Copyright (c) Petr Havel 2023.
// Licensed under the MIT License.

using Microsoft.WindowsAzure.Storage.Table;

namespace HoloLens4Labs.Scripts.DTOs
{
    public class TextLogDTO : TableEntity
    {

        public string LogID { get; set; }
        public string TextID { get; set; }
        public string TextLogID { get; set; }

    }
}
