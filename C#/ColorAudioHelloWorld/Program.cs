using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using TinyCsvParser;
using TinyCsvParser.Mapping;
using TinyCsvParser.TypeConverter;

namespace ColorAudioHelloWorld
{

    class Program
    {
        public static CsvParserOptions tsvParserOpt = new CsvParserOptions(false, '\t');

        public class TimingInfo
        {
            public float startTime { get; set; }
            public float endTime { get; set; }
            public LightInfo info { get; set; }
        }

        public class LightInfo
        {
            public byte section;
            public Vector3 color;
            public IEnumerable<string> tags;

            public override string ToString()
            {
                return $"Section={section}; Color=({color.X},{color.Y},{color.Z}); Tags=\"{String.Join(", ", tags.ToArray())}\"";
            }
        }

        class LightInfoTypeConverter : ITypeConverter<LightInfo>
        {
            public Type TargetType => typeof(LightInfo);

            public bool TryConvert(string value, out LightInfo result)
            {
                if(value.StartsWith("ColorAudio"))
                {
                    result = null;
                    return true;
                }
                var temp = value.TrimEnd(';').Split(';').ToList();

                var colorList = temp[1].Split(',').Select(s => int.Parse(s)).ToList();

                try
                {
                    result = new LightInfo
                    {
                        section = (byte)int.Parse(temp[0].Trim('~')),
                        color = new Vector3(colorList[0], colorList[1], colorList[2]),
                        tags = temp.Skip(2).ToList()
                    };
                } 
                catch
                {
                    result = null;
                    return false;
                }
                
                return true;
            }
        }


        private class TimingMap : CsvMapping<TimingInfo>
        {
            public TimingMap()
                : base()
            {
                MapProperty(0, x => x.startTime);
                MapProperty(1, x => x.endTime);
                MapProperty(2, x => x.info, new LightInfoTypeConverter());
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var tsvMapper = new TimingMap();
            var tsvParser = new CsvParser<TimingInfo>(tsvParserOpt, tsvMapper);
            var result = tsvParser
                .ReadFromFile(@"test.clr", Encoding.ASCII).ToList();

            foreach (var item in result)
            {
                Console.WriteLine(item.Result.info);
            }
        }
    }
}
