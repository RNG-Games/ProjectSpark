using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSpark.actors;
using ProjectSpark.actors.blocks;
using ProjectSpark.actors.enemies;
using ProjectSpark.actors.lines;
using ProjectSpark.util;

namespace ProjectSpark.loading
{
    public static class Loader
    {
        public static bool Load(string filePath, List<IActable> actors)
        {
            try
            {
                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    var data = new byte[fs.Length];
                    for (var i = 0; i < data.Length; i++)
                        data[i] = (byte)fs.ReadByte();
                    var ret = ApplyLoadedData(actors, data);
                    if (ret)
                        Resources.StageData = data;
                    return ret;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in process: {0}", ex);
                return false;
            }
        }

        /***
         * BitConverter.ToSingle uses Little Endian
         * eg: 5.0f: Big Endian: 40 A0 00 00
         *           Little End; 00 00 A0 40
         *           
         *           
         *  minus durch % -> -20 = %20
         */

        public static bool ApplyLoadedData(List<IActable> actors, byte[] data)
        {
            if (data == null)
                return false;
            //Head: FE 34 BD
            var pos = 0;
            if (data[0] != 0xFE || data[1] != 0x34 || data[2] != 0xBD)
            {
                return false;
            }

            while (pos < data.LongLength)
            {
                int id = data[pos++];
                int x;
                int y;
                string texture;
                int borderX, borderY;

                //ID: 1 Byte
                switch (id)
                {
                    //HEX: 01   Trampoline
                    //bis erste 00 X-Pos, bis zweite 00 Y
                    case 1:

                        x = Convert.ToInt32(Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray()), 16);
                        pos += data.Skip(pos).TakeWhile(d => d != 0).ToArray().Length + 1;

                        y = Convert.ToInt32(Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray()), 16);
                        pos += data.Skip(pos).TakeWhile(d => d != 0).ToArray().Length + 1;

                        Console.WriteLine($"Trampoline({x},{y})");
                        actors.Add(new Trampoline(x,y));
                        break;

                    //HEX: 02   Decoration
                    //bis erste 00 X-Pos, bis zweite 00 Y, bis dritte 00 texture
                    case 2:
                        x = Convert.ToInt32(Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray()), 16);
                        pos += data.Skip(pos).TakeWhile(d => d != 0).ToArray().Length + 1;

                        y = Convert.ToInt32(Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray()), 16);
                        pos += data.Skip(pos).TakeWhile(d => d != 0).ToArray().Length + 1;

                        texture = Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray());
                        pos += texture.Length + 1;

                        Console.WriteLine($"Decoration({x},{y},{texture})");
                        actors.Add(new Decoration(x,y,texture));
                        break;

                    //HEX: 03   LargeBlock
                    //type string, lm, rm, tm, bm
                    case 3:
                        var type = Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray());
                        pos += type.Length + 1;

                        var lm = Convert.ToInt32(Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray()), 16);
                        pos += data.Skip(pos).TakeWhile(d => d != 0).ToArray().Length + 1;

                        var rm = Convert.ToInt32(Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray()), 16);
                        pos += data.Skip(pos).TakeWhile(d => d != 0).ToArray().Length + 1;

                        var tm = Convert.ToInt32(Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray()), 16);
                        pos += data.Skip(pos).TakeWhile(d => d != 0).ToArray().Length + 1;

                        var bm = Convert.ToInt32(Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray()), 16);
                        pos += data.Skip(pos).TakeWhile(d => d != 0).ToArray().Length + 1;

                        Console.WriteLine($"LargeBlock({type},{lm},{rm},{tm},{bm})");
                        actors.Add(new LargeBlock(type, lm, rm, tm, bm));
                        break;

                    //HEX: 04   Stationary
                    //x, y
                    case 4:
                        x = Convert.ToInt32(Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray()), 16);
                        pos += data.Skip(pos).TakeWhile(d => d != 0).ToArray().Length + 1;

                        y = Convert.ToInt32(Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray()), 16);
                        pos += data.Skip(pos).TakeWhile(d => d != 0).ToArray().Length + 1;

                        Console.WriteLine($"Stationary({x},{y})");
                        actors.Add(new Stationary(x, y));
                        break;

                    //HEX: 05   Vertical
                    //x, upper, lower
                    case 5:
                        x = Convert.ToInt32(Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray()), 16);
                        pos += data.Skip(pos).TakeWhile(d => d != 0).ToArray().Length + 1;

                        var upper = Convert.ToInt32(Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray()), 16);
                        pos += data.Skip(pos).TakeWhile(d => d != 0).ToArray().Length + 1;

                        var lower = Convert.ToInt32(Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray()), 16);
                        pos += data.Skip(pos).TakeWhile(d => d != 0).ToArray().Length + 1;

                        Console.WriteLine($"Vertical({x},{upper},{lower})");
                        actors.Add(new Vertical(x, upper, lower));
                        break;

                    //HEX: 06   Horizontal
                    //y, left, right
                    case 6:
                        y = Convert.ToInt32(Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray()), 16);
                        pos += data.Skip(pos).TakeWhile(d => d != 0).ToArray().Length + 1;

                        var left = Convert.ToInt32(Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray()), 16);
                        pos += data.Skip(pos).TakeWhile(d => d != 0).ToArray().Length + 1;

                        var right = Convert.ToInt32(Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray()), 16);
                        pos += data.Skip(pos).TakeWhile(d => d != 0).ToArray().Length + 1;

                        Console.WriteLine($"Horizontal({y},{left},{right})");
                        actors.Add(new Horizontal(y, left, right));
                        break;

                    //HEX: 07   Checkpoint
                    //
                    case 7:
                        var start = (data[pos++] == 1);

                        borderX = Convert.ToInt32(Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray()), 16);
                        pos += data.Skip(pos).TakeWhile(d => d != 0).ToArray().Length + 1;

                        borderY = Convert.ToInt32(Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray()), 16);
                        pos += data.Skip(pos).TakeWhile(d => d != 0).ToArray().Length + 1;
                        
                        y = Convert.ToInt32(Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray()), 16);
                        pos += data.Skip(pos).TakeWhile(d => d != 0).ToArray().Length + 1;

                        Console.WriteLine($"Checkpoint({start},{borderX},{borderY},{y})");
                        actors.Add(new Checkpoint(start, new Vector2f(borderX, borderY), y));
                        break;

                    //HEX: 08   Transition
                    //
                    case 8:
                        borderX = Convert.ToInt32(Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray()), 16);
                        pos += data.Skip(pos).TakeWhile(d => d != 0).ToArray().Length + 1;

                        borderY = Convert.ToInt32(Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray()), 16);
                        pos += data.Skip(pos).TakeWhile(d => d != 0).ToArray().Length + 1;

                        y = Convert.ToInt32(Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray()), 16);
                        pos += data.Skip(pos).TakeWhile(d => d != 0).ToArray().Length + 1;

                        Console.WriteLine($"Transition({borderX},{borderY},{y})");
                        actors.Add(new Transition(new Vector2f(borderX, borderY), y));
                        break;

                    //HEX: 09   Npc
                    //x, y, anzahl der messages, text [1,...], texture
                    case 9:
                        x = Convert.ToInt32(Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray()), 16);
                        pos += data.Skip(pos).TakeWhile(d => d != 0).ToArray().Length + 1;

                        y = Convert.ToInt32(Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray()), 16);
                        pos += data.Skip(pos).TakeWhile(d => d != 0).ToArray().Length + 1;

                        var num = Convert.ToInt32(Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray()), 16);
                        pos += data.Skip(pos).TakeWhile(d => d != 0).ToArray().Length + 1;

                        var text = new string[num];
                        for (var i = 0; i < num; i++)
                        {
                            text[i] = Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray());
                            pos += text[i].Length + 1;
                        }

                        texture = Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray());
                        pos += texture.Length + 1;

                        Console.WriteLine($"NPC({x},{y},TEXT,{texture})");
                        actors.Add(new Npc(x,y, text, texture));
                        break;

                    //HEX: 0A   Circular
                    //
                    case 10:
                        x = Convert.ToInt32(Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray()), 16);
                        pos += data.Skip(pos).TakeWhile(d => d != 0).ToArray().Length + 1;

                        y = Convert.ToInt32(Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray()), 16);
                        pos += data.Skip(pos).TakeWhile(d => d != 0).ToArray().Length + 1;

                        var r = Convert.ToInt32(Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray()), 16);
                        pos += data.Skip(pos).TakeWhile(d => d != 0).ToArray().Length + 1;

                        var a = Convert.ToInt32(Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray()), 16);
                        pos += data.Skip(pos).TakeWhile(d => d != 0).ToArray().Length + 1;

                        var speed = Convert.ToInt32(Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray()), 16);
                        pos += data.Skip(pos).TakeWhile(d => d != 0).ToArray().Length + 1;

                        Console.WriteLine($"Circular({x},{y},{r},{a},{speed})");
                        actors.Add(new Circular(x,y,r,a,speed));
                        break;

                    //HEX: 0B   Blockwise
                    //
                    case 11:
                        var count = Convert.ToInt32(Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray()), 16);
                        pos += data.Skip(pos).TakeWhile(d => d != 0).ToArray().Length + 1;

                        var vert = new Vector2f[count];
                        for (var i = 0; i < count; i++)
                        {
                            x = Convert.ToInt32(Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray()), 16);
                            pos += data.Skip(pos).TakeWhile(d => d != 0).ToArray().Length + 1;

                            y = Convert.ToInt32(Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray()), 16);
                            pos += data.Skip(pos).TakeWhile(d => d != 0).ToArray().Length + 1;

                            vert[i] = new Vector2f(x,y);
                        }

                        var wait = Convert.ToInt32(Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray()), 16);
                        pos += data.Skip(pos).TakeWhile(d => d != 0).ToArray().Length + 1;

                        Console.WriteLine($"Blockwise(VERTICES,{wait})");
                        actors.Add(new Blockwise(vert, wait));
                        break;

                    //HEX: 0C   Jumping
                    //
                    case 12:
                        x = Convert.ToInt32(Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray()), 16);
                        pos += data.Skip(pos).TakeWhile(d => d != 0).ToArray().Length + 1;

                        y = Convert.ToInt32(Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray()), 16);
                        pos += data.Skip(pos).TakeWhile(d => d != 0).ToArray().Length + 1;

                        var vel = Convert.ToInt32(Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray()), 16);
                        pos += data.Skip(pos).TakeWhile(d => d != 0).ToArray().Length + 1;

                        Console.WriteLine($"Jumping({x},{y},{vel})");
                        actors.Add(new Jumping(x,y,vel));
                        break;

                    //HEX: 0D   Regular
                    //
                    case 13:
                        x = Convert.ToInt32(Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray()), 16);
                        pos += data.Skip(pos).TakeWhile(d => d != 0).ToArray().Length + 1;

                        y = Convert.ToInt32(Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray()), 16);
                        pos += data.Skip(pos).TakeWhile(d => d != 0).ToArray().Length + 1;

                        Console.WriteLine($"Regular({x},{y})");
                        actors.Add(new Regular(x,y));
                        break;
                    
                    //HEX: 0E   Collectible
                    //
                    case 14:
                        x = Convert.ToInt32(Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray()), 16);
                        pos += data.Skip(pos).TakeWhile(d => d != 0).ToArray().Length + 1;

                        y = Convert.ToInt32(Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray()), 16);
                        pos += data.Skip(pos).TakeWhile(d => d != 0).ToArray().Length + 1;

                        var index = Convert.ToInt32(Encoding.ASCII.GetString(data.Skip(pos).TakeWhile(d => d != 0).ToArray()), 16);
                        pos += data.Skip(pos).TakeWhile(d => d != 0).ToArray().Length + 1;

                        Console.WriteLine($"Collectible({x},{y},{index})");
                        actors.Add(new Collectible(x,y,index));
                        break;
                }
            }

            return true;
        }
    }
}
