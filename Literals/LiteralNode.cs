namespace TroyerA2
{
    class LiteralNode
    {
        public string Name;
        public string Value;
        public int Length;
        public int Address;

        public LiteralNode(string name, string value, int length, int address)
        {
            Name = name;
            Value = value;
            Length = length;
            Address = address;
        }
    }
}