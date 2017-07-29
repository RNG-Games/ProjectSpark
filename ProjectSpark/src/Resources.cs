using ProjectSpark.actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistedLogik.Ultraviolet;
using TwistedLogik.Ultraviolet.Content;

namespace ProjectSpark
{
    public static class Resources
    {
        public static int Scale => 48;
        public static float deltaTime { get; set; }
        public static ContentManager ContentManager { get; set; }
        public static IUltravioletInput Input { get; set; }
        public static IUltravioletGraphics gfx { get; set; }
        public static bool blocked { get; set; }
        public static List<IActable> actors { get; set; }
        public static List<IActable> actorBuffer { get; set; }
        public static int collectables { get; set; }
        public static int deaths { get; set; }
    }
}
