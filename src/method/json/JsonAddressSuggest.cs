using System;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    internal class JsonAddressSuggest<T>: JsonWrapper<T>, IAddressSuggest ,IShippingApiRequest where T : IAddressSuggest, new()
    {
        public JsonAddressSuggest() : base() { }

        public JsonAddressSuggest(T t) : base(t) { }

        public string RecordingSuffix => Address.Name;
        public string RecordingFullPath(string resource, ISession session)
        {
            return ShippingApiRequest.RecordingFullPath(this, resource, session);
        }
        public bool ShouldSerializeStatus() => false;
       


        public string ContentType { get => "application/json"; }

        [ShippingApiHeaderAttribute("Bearer")]

        
        public StringBuilder Authorization { get; set; }

        [JsonProperty("address", Order = 1)]
        public IAddress Address
        {
            get => Wrapped.Address;
            set { Wrapped.Address = value; }
        }

        [ShippingApiQuery("returnSuggestions", true)]
        public bool? Suggest { get; set; }

        public string GetUri(string baseUrl)
        {
            StringBuilder uri = new StringBuilder(baseUrl);
            ShippingApiRequest.SubstitueResourceParameters(this, uri);
            ShippingApiRequest.AddRequestQuery(this, uri);
            return uri.ToString();
        }

        public IEnumerable<Tuple<ShippingApiHeaderAttribute, string, string>> GetHeaders()
        {
            return ShippingApiRequest.GetHeaders(this);
        }

        public void SerializeBody(StreamWriter writer, ISession session)
        {
            ShippingApiRequest.SerializeBody(this, writer, session);
        }

    }
}
