using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PitneyBowes.Developer.Drawing
{
    public class InterfaceDeserializer : JsonConverter
    {
        public static void RegisterDeserializationTypes()
        {
            Register(typeof(IShape), typeof(Line), "line");
            Register(typeof(IShape), typeof(Bitmap), "bitmap");
            Register(typeof(IShape), typeof(Barcode), "barcode");
            Register(typeof(IShape), typeof(Box), "box");
            Register(typeof(IShape), typeof(Container), "group");
            Register(typeof(IShape), typeof(Drawing), "drawing");
            Register(typeof(IShape), typeof(TextBox), "textbox");
            Register(typeof(IText), typeof(StaticText), "staticText");
            Register(typeof(IText), typeof(BoundText), "boundPropertyText");
            Register(typeof(IBarcodeData), typeof(StaticBarcodeData), "staticBarcodeData");
            Register(typeof(IBarcodeData), typeof(BoundBarcodeData), "boundBarcodeData");
            Register(typeof(IBitmapSource), typeof(StreamBitmapSource), "streamBitmapSource");
            Register(typeof(IBitmapSource), typeof(FileBitmapSource), "fileBitmapSource");
        }

        // use the interface and type id string to look up the concrete class
        static Dictionary<Tuple<Type, string>, Type> _deserializationLookup = new Dictionary<Tuple<Type, string>, Type>();
        // interfaces
        static Dictionary<Type, Type> _typeInterfaceMap = new Dictionary<Type, Type>();

        private HashSet<Object> _processedObjects = new HashSet<object>();

        public static void Register(Type interfaceType, Type objectType, string typeId)
        {
            _typeInterfaceMap.Add(objectType, interfaceType);
            _deserializationLookup.Add(new Tuple<Type, string>(interfaceType, typeId), objectType);
        }
        public static bool IsPoymorphicInterface(Type objectType)
        {
            return _typeInterfaceMap.ContainsValue(objectType);
        }
        public override bool CanConvert(Type objectType)
        {
            return _typeInterfaceMap.ContainsValue(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.StartObject) throw new JsonReaderException("Interface deserializer, expected start of object");
            reader.Read();
            if (reader.TokenType != JsonToken.PropertyName )throw new JsonReaderException("Interface deserializer, expected property");
            string typeId = (string)reader.Value;
            reader.Read();
            var key = new Tuple<Type, string>(objectType, typeId);
            if (!_deserializationLookup.ContainsKey(key)) throw new JsonReaderException(string.Format("Interface deserializer, could not find key {0} for interface {1}", typeId, objectType.Name));
            var concreteType = _deserializationLookup[key];
            var rtn =  serializer.Deserialize(reader, concreteType);
            if (reader.TokenType != JsonToken.EndObject) throw new JsonReaderException("Interface deserializer, expected end object");
            reader.Read(); // end of wrapper
            return rtn;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
        }
    }
}
