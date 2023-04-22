namespace MinecraftConnectionBE.JsonProperty
{
    internal class CommandRequestJson
    {
        public Body body { get; set; }
        public Header header { get; set; }

        public class Body
        {
            public Origin origin { get; set; }
            public string commandLine { get; set; }
            public int version { get; set; } = 1;

            public class Origin
            {
                public string type { get; set; }
            }
        }

        public class Header
        {
            public string requestId { get; set; }
            public string messagePurpose { get; set; }
            public int version { get; set; } = 1;
            public string messageType { get; set; }
        }
    }
}
