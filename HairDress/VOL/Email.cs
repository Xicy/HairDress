namespace HairDress.VOL
{
    public class Email
    {
        public int ID { set; get; }
        public string EmailData { set; get; }
        public EmailType EmailType { set; get; }
    }

    public enum EmailType : byte
    {
        Home,
        Work
    }
}