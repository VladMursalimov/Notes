
namespace DatabaseModels
{
    public class Note
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public List<Tag> Tags { get; set; } = new List<Tag>();

    }

    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Reminder
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime ReminderTime { get; set; }

        public List<Tag> Tags { get; set; } = new List<Tag>();
    }
}   