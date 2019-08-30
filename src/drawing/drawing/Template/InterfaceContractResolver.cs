using System;
using Newtonsoft.Json.Serialization;

namespace PitneyBowes.Developer.Drawing
{

    public class InterfaceContractResolver : DefaultContractResolver
    {

        public static readonly InterfaceContractResolver Instance = new InterfaceContractResolver();

        protected override JsonContract CreateContract(Type objectType)
        {
            // this will only be called once and then cached 
            JsonContract contract = base.CreateContract(objectType);
            if (InterfaceDeserializer.IsPoymorphicInterface(objectType))
                contract.Converter = new InterfaceDeserializer();
            return contract;
        }
    }
}
