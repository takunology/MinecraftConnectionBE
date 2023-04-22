namespace MinecraftConnectionBE.JsonProperty
{
    internal class EventSubscribeJson
    {
        public Body body { get; set; }
        public Header header { get; set; }

        public class Body
        {
            public string eventName { get; set; }
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
