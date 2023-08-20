using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloLens4Labs.Scripts.Exceptions;
using System.Threading.Tasks;
using HoloLens4Labs.Scripts.DTOs;

namespace HoloLens4Labs.Scripts.Model {

    public class Scientist
    {
        string id = string.Empty;
        string name = string.Empty;

        public string Name {
            get => name;
            set => name = value; 
        }

     
        public string Id { get => id; set => id = value; }

        public Scientist() { }

        public Scientist(string name) { this.Name = name; }

        public Scientist(string id, string name) : this(name) { this.id = id; }

    }
}