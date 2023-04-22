namespace MinecraftConnectionBE.JsonProperty
{
    internal class ResponceJson
    {
        public Body body { get; set; }
        public Header header { get; set; }

        public class Body
        {
            public string message { get; set; }
            public string receiver { get; set; }
            public string sender { get; set; }
            public string type { get; set; }
        }

        public class Header
        {
            public string eventName { get; set; }
            public string messagePurpose { get; set; }
            public int version { get; set; }
        }
    }
}
