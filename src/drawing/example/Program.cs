using System;
using System.IO;
using System.Text;

using PitneyBowes.Developer.Drawing;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace example
{
    class Program
    {

        static void Main(string[] args)
        {
            var label = new Drawing();
            label.DefaultText = new TextProperties("Arial", 12, FontStyle.Normal);
            label.DefaultLine = new LineProperties(1);
            var serviceIcon = new Container(0, 0);
            serviceIcon.Name = "Service Icon";
            serviceIcon.Id = new Guid("aff4e5d5-7839-4367-9fe4-b852c9911cc8");
            serviceIcon.AddShape(new Box(0, 0, 1, 1))
                .AddShape(new Line(0, 0, 1, 1))
                .AddShape(new Line(0, 1, 1, 0));
            label.AddShape(serviceIcon);
            var indiciumArea = new Container(1, 0);
            indiciumArea.Name = "Indicium";
            indiciumArea.DefaultText = new TextProperties("Arial", 8, FontStyle.Normal);
            indiciumArea.AddShape(new Box(0, 0, 3, 1))
                        .AddShape(new TextBox(1, 0.25f, 0.6f, 1.5f, new StaticText("PARCEL SELECT\r\nPostage Paid\r\nPB Presort Svcs Inc.\r\neVS"))
                        { DrawBoundingBox = true, Margin = 2, HorizontalAlignment = HorizontalAlignment.Center }
                        );
            label.AddShape(indiciumArea);
            var brandingSegment = new Container(0, 1);
            brandingSegment.Name = "Branding";
            brandingSegment.DefaultText = new TextProperties("Arial", 14, FontStyle.Bold);
            brandingSegment.AddShape(new Box(0, 0, 4, 5.0f / 16.0f))
                .AddShape(new TextBox(0, 0, 0.5f, 4, new StaticText("USPS PARCEL SELECT "))
                { HorizontalAlignment = HorizontalAlignment.Center });
            label.AddShape(brandingSegment);
            var addressArea = new Container(0, 1 + 5.0f / 16.0f);
            addressArea.Id = new Guid("3d4a2453-f996-4c87-9eb6-87f3d21ea621");
            var from = new TextBox(0.1f, 0.1f, 0.5f, 1.5f, new StaticText("From: Patrick Farry\r\n101 Jenkins Pl.\r\nSanta Clara, CA, 95051"));
            from.DefaultText = new TextProperties("Arial", 8, FontStyle.Normal);
            addressArea.AddShape(from);
            addressArea.AddShape(new TextBox(0.5f, 1f, 0.5f, 1.5f, new StaticText("To: Patrick Farry\r\n101 Jenkins Pl.\r\nSanta Clara, CA, 95051")));
            label.AddShape(addressArea);
            var impbArea = new Container(0, 4);
            impbArea.Name = "IMpb";
            impbArea.DefaultLine = new LineProperties(4);
            var impb = Encoding.UTF8.GetBytes("{FNC1}420900073740{FNC1}9274890213902110029375");
            impbArea.AddShape(new Line(0, 0, 4, 0))
                    .AddShape(new Line(0, 1.5f, 4, 1.5f))
                    .AddShape(new TextBox(0, 0, .5f, 4, new StaticText("USPS TRACKING#"))
                    { HorizontalAlignment = HorizontalAlignment.Center })
                    .AddShape(new TextBox(0, 1.22f, .5f, 4, new StaticText("9212 3998 3487 3412 3456 78"))
                    { HorizontalAlignment = HorizontalAlignment.Center })
                    .AddShape(new Barcode(.25f, .3f, .8f, 3.5f, Symbology.Code128C, new StaticBarcodeData(impb)));
            label.AddShape(impbArea);

            string fileName = "image1.png";
            Console.WriteLine(fileName);
            using (var writer = new StreamWriter(fileName, false))
            {
                var page = new Page() { Height = 6, Width = 4, Resolution = 203 };
                var renderer = new SkiaSharpBitmapRenderer();
                renderer.Render(writer.BaseStream, label, page, 0);
            }

            var label2 = new Drawing();
            var imageArea = new Container(0, 0);
            var fileBitmap = new FileBitmapSource() { FileName = "cubeok.png", Filepath = "." };
            imageArea.AddShape(new Bitmap(0, 0, 1, 1) { BitmapSource = fileBitmap, FitToBoundingBox = true });
            imageArea.AddShape(new Line(0, 0.5f, .5f, 1));
            label2.AddShape(imageArea);

            string fileName2 = "image2.png";
            Console.WriteLine(fileName2);
            using (var writer = new StreamWriter(fileName2, false))
            using (var reader = new StreamReader(fileName))
            {
                var page = new Page() { Height = 6, Width = 4, Resolution = 300 };
                var renderer = new SkiaSharpBitmapRenderer();
                renderer.Render(writer.BaseStream, reader.BaseStream, label2, page, 0);
            }


            var child = new ChildLabel();
            var fontChange = new ChangeFontStyle() { Style = FontStyle.BoldItallic };
            child.AddChange(new Guid("3d4a2453-f996-4c87-9eb6-87f3d21ea621"), fontChange);
            child.Shape = label;
            child.ApplyChanges();
            string fileName3 = "image3.png";
            Console.WriteLine(fileName3);
            using (var writer = new StreamWriter(fileName3, false))
            {
                var page = new Page() { Height = 6, Width = 4, Resolution = 300 };
                var renderer = new SkiaSharpBitmapRenderer();
                renderer.Render(writer.BaseStream, label, page, 0);
            }

            InterfaceDeserializer.RegisterDeserializationTypes();

            SerializationVisitor s = new SerializationVisitor();
            s.Visit(label);

            string jsonTypeNameAll = JsonConvert.SerializeObject(label, Formatting.Indented, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver() { NamingStrategy = new CamelCaseNamingStrategy() }
            });

            using (var writer = new StreamWriter("labeltmpl.json", false))
                writer.WriteLine(jsonTypeNameAll);

            var label4 = JsonConvert.DeserializeObject<Drawing>(jsonTypeNameAll, new JsonSerializerSettings
            {
                ContractResolver = new InterfaceContractResolver() { NamingStrategy = new CamelCaseNamingStrategy() }
            });

            string fileName5 = "image4.svg";
            Console.WriteLine(fileName5);
            using (var writer = new StreamWriter(fileName5, false))
            {
                var page = new Page() { Height = 6, Width = 4, Resolution = 300 };
                var renderer = new SkiaSharpSvgRenderer();
                renderer.Render(writer.BaseStream, label, page, 0);
            }

        }

    }
}
