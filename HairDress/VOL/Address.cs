namespace HairDress.VOL
{
    public class Address
    {
        public int ID { set; get; }
        public string AddressData { set; get; }
        public AddressType AddressType { set; get; }
    }

    public enum AddressType : byte
    {
        Home,
        Work
    }
}