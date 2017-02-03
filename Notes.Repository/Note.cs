namespace Notes.Repository
{
    public class Note : Notes.Interfaces.Note
    {
        
        public string body
        {
            get; set;
        }

        public int? id
        {
            get; set;
        }
    }
}
