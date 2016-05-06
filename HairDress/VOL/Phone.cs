namespace HairDress.VOL
{
    public class Phone
    {
        public int ID { set; get; }
        public string PhoneData { set; get; }
        public PhoneType PhoneType { set; get; }
    }

    public enum PhoneType : byte
    {
        Home,
        Work
    }
}