using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MinecraftConnectionBE.JsonProperty
{
    internal class BlockPlacedEventJson
    {
        public Body body { get; set; }
        public Header header { get; set; }
    }
    public class Header
    {
        public string eventName { get; set; }
        public string messagePurpose { get; set; }
        public int version { get; set; }

    }
    public class Body
    {
        public Block block { get; set; }
        public int count { get; set; }
        public bool placedUnderWater { get; set; }
        public int placementMethod { get; set; }
        public Player player { get; set; }
        public Tool tool { get; set; }

    }
    public class Block
    {
        public int aux { get; set; }
        public string id { get; set; }
        [JsonPropertyName("namespace")]
        public string nameSpace { get; set; }
    }
    public class Position
    {
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }
    }
    public class Player
    {
        public string color { get; set; }
        public int dimension { get; set; }
        public Int64 id { get; set; }
        public string name { get; set; }
        public Position position { get; set; }
        public string type { get; set; }
        public int variant { get; set; }
        public double yRot { get; set; }

    }
    public class Tool
    {
        public int aux { get; set; }
        public IList<string> enchantments { get; set; }
        public int freeStackSize { get; set; }
        public string id { get; set; }
        public int maxStackSize { get; set; }
        [JsonPropertyName("namespace")]
        public string nameSpace { get; set; }
        public int stackSize { get; set; }
    }
}
