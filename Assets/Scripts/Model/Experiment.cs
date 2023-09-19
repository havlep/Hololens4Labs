namespace HoloLens4Labs.Scripts.Model
{

    /// <summary>
    /// The data model class that holdes information about experiments
    /// </summary>
    public class Experiment
    {
        /// <summary>
        /// The ID of the experiment
        /// <!-- Empty if the experiment has not yet been written to the database -->
        /// </summary>
        string id = string.Empty;

        /// <summary>
        /// The name of the experiment
        /// </summary>
        string name = string.Empty;

        /// <summary>
        /// The ID of the scientist who created the experiment
        /// <!-- Can be empty if the experiment is set below -->
        /// </summary>
        string scientistID = string.Empty;

        /// <summary>
        /// The scientist who created the experiment
        /// <!-- Can be Null if the scientist ID set above -->
        /// </summary>
        public Scientist CreatedBy { get; set; } = null;

        /// <summary>
        /// Property for getting and setting the name of the experiment
        /// </summary>
        public string Name
        {
            get => name;
            set => name = value;
        }

        /// <summary>
        /// Property for getting and setting the id of the experiment
        /// </summary>
        public string Id { get => this.id ; set => this.id = value; }

        /// <summary>
        /// Property for getting and setting the id of the scientist who created the experiment
        /// </summary>
        public string CreatedByID { get => this.CreatedBy == null ? this.scientistID : this.CreatedBy.Id; set => this.scientistID = value; }

        /// <summary>
        /// Constructor for creating a new experiment when name and scientist ID is known
        /// </summary>
        /// <param name="name"> The name of the experiment </param>
        /// <param name="createdByID"> The id of the scientist who created the experiment </param>
        public Experiment(string name, string createdByID) { this.Name = name; CreatedByID = createdByID; }

        /// <summary>
        /// Constructor for creating a new experiment when name and scientist is known
        /// </summary>
        /// <param name="name">The name of the experiment</param>
        /// <param name="createdBy"> The Scientist object representing the user </param>
        public Experiment(string name, Scientist createdBy) { this.Name = name; CreatedBy = createdBy; }

        /// <summary>
        /// Constructor for creating a data model representation of an Experiment that is already present in the database when name, id and scientist is known
        /// </summary>
        /// <param name="id"> The id of the experiment </param>
        /// <param name="name"> The name of the experiment </param>
        /// <param name="createdByID"> The id of the scientist who created the experiment </param>
        public Experiment(string id, string name, string createdByID) : this(name, createdByID)
        {
            this.id = id;
        }

        /// <summary>
        /// Constructor for creating a data model representation of an Experiment that is already present in the database when name, id and scientist is known
        /// </summary>
        /// <param name="id"> The id of the experiment </param>
        /// <param name="name"> The name of the experiment </param>
        /// <param name="createdByID"> The Scientist object representing the user </param>
        public Experiment(string id, string name, Scientist createdBy) : this(name, createdBy)
        {
            this.id = id;
        }

    }
}