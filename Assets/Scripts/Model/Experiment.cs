using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloLens4Labs.Scripts.Exceptions;
using System.Threading.Tasks;
using HoloLens4Labs.Scripts.DTOs;
using HoloLens4Labs.Scripts.Services.DataTransferServices;

namespace HoloLens4Labs.Scripts.Model
{

    public class Experiment
    {
        string id = string.Empty;
        string name = string.Empty;
        string scientistID = string.Empty;
        public Scientist CreatedBy { get; set; } = null;

        // TODO throw exception if name is empty on get
        public string Name
        {
            get => name;
            set => name = value;
        }

        // TODO throw exception for negative value
        public string Id { get => this.id ; set => this.id = value; }

        public string CreatedByID { get => this.CreatedBy == null ? this.scientistID : this.CreatedBy.Id; set => this.scientistID = value; }

        public Experiment() { }

        public Experiment(string name, string createdByID) { this.Name = name; CreatedByID = createdByID; }

        public Experiment(string name, Scientist createdBy) { this.Name = name; CreatedBy = createdBy; }

        public Experiment(string id, string name, string createdByID) : this(name, createdByID)
        {
            this.id = id;
        }

        public Experiment(string id, string name, Scientist createdBy) : this(name, createdBy)
        {
            this.id = id;
        }

    }
}