using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloLens4Labs.Scripts.Exceptions;


namespace HoloLens4Labs.Scripts.Model {

    public class Scientist
    {
        int id = -1;
        string name = string.Empty;

        // TODO throw exception if name is empty on get
        public string Name {
            get => string.IsNullOrEmpty(name) ? throw new ObjectNotInitializedException() : name;
            set => name = value; 
        }

        // TODO throw exception for negative value
        public int Id { get => this.id < 0 ? throw new ObjectDataBaseException() : id; }

        public Scientist() { }

        public Scientist(string name) { this.Name = name; }

        public Scientist(int id, string name) : this(name) { this.id = id; }

    }
}