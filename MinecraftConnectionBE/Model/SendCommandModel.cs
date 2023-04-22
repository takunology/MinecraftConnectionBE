using System;
using System.Collections.Generic;
using System.Text;

namespace MinecraftConnectionBE.Model
{
    public static class SendCommandModel
    {
        public static string? Command { get; set; }
        public static string? Trigger { get; set; }
        public static string? Statement { get; set; }
    }
}
