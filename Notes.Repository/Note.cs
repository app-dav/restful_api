namespace Notes.Repository
{
    public class Note : Notes.Interfaces.INote
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
