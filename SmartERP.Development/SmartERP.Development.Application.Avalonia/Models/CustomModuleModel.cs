namespace SmartERP.Development.Application.Models
{
    public class CustomModuleModel
    {
        #region Fields

        private long _id;

        private string _name = string.Empty;

        private string _displayName = string.Empty;

        private string _description = string.Empty;

        private List<CustomEntityModel> _entities = new();

        private List<CustomViewModel> _views = new();

        private bool _isValid = false;

        #endregion Fields

        #region Properties

        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string DisplayName { get; set; } = String.Empty;

        public string Description { get; set; } = string.Empty;

        public List<CustomEntityModel> Entities { get; set; } = new();

        public List<CustomViewModel> Views = new();

        public bool IsValid { get; set; }

        #endregion Properties

        #region Static

        public static CustomModuleModel Example
        {
            get
            {
                CustomModuleModel model = new CustomModuleModel();
                model.Id = 1;
                model.Name = "Example";
                model.DisplayName = "Example module";
                CustomEntityModel entity = new CustomEntityModel() { Id = 1, Name = "ToDoList", DisplayName = "ToDo List" };

                entity.Fields.Add(new CustomEntityFieldModel()
                {
                    Entity = entity,
                    EntityId = entity.Id,
                    Name = "Name",
                    DisplayName = "Name",
                    Type = "string"
                });

                entity.Fields.Add(new CustomEntityFieldModel()
                {
                    Entity = entity,
                    EntityId = entity.Id,
                    Name = "IsComplete",
                    DisplayName = "Completed",
                    Type = "bool"
                });
                
                model.Entities.Add(entity);

                return model;
            }
        }

        #endregion

        public void Validate()
        {
            if (string.IsNullOrEmpty(Name))
            {
                IsValid = false;
            }
            else
            {
                IsValid = true;
            }
        }
    }
}
