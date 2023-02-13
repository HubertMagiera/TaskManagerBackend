namespace ToDoBackend.Entities
{
    public class task
    {
        //EF Core will call the parametrised ctor if it is provided
        //if parametrised ctor is not provided, EF Core calls the parameterless ctor
        public task(int task_id, string task_name, string? task_description, DateTime task_creation_date, DateTime? task_close_date, string task_priority, string task_status, task_type task_Type)
        {
            this.task_id = task_id;
            this.task_name = task_name;
            this.task_description = task_description;
            this.task_creation_date = task_creation_date;
            this.task_close_date = task_close_date;
            this.task_priority = task_priority;
            this.task_status = task_status;
            this.task_Type = task_Type;
        }

        public int task_id { get; set; }

        public string task_name { get; set; }

        public string? task_description { get; set; }

        public DateTime task_creation_date { get; set; }

        public DateTime? task_close_date { get; set; }

        public string task_priority { get; set; }

        public string task_status { get; set; }

        public task_type task_Type { get; set; }
    }
}
