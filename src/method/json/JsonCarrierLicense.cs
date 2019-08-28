using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    class JsonCarrierLicense<T> : JsonWrapper<T>, ICarrierLicense, IShippingApiRequest where T : ICarrierLicense, new()
    {
        public JsonCarrierLicense() : base() { }

        public JsonCarrierLicense(T t) : base(t) { }


        [ShippingApiQuery("carrier")]
        public Carrier Carrier { get => Wrapped.Carrier; set => Wrapped.Carrier = value; }
        
        [ShippingApiQuery("originCountryCode")]
        public string OriginCountryCode { get => Wrapped.OriginCountryCode; set => Wrapped.OriginCountryCode = value; }
        
        [JsonProperty("licenseText")]
        public string LicenseText { get => Wrapped.LicenseText; set => Wrapped.LicenseText = value; }

        public string ContentType { get => "application/json"; }

        public string RecordingSuffix => $"{Carrier}-{OriginCountryCode}";

        public IEnumerable<Tuple<ShippingApiHeaderAttribute, string, string>> GetHeaders()
        {
            return ShippingApiRequest.GetHeaders(this);
        }

        [ShippingApiHeader("Bearer", true)]
        public StringBuilder Authorization { get; set; }

        public string GetUri(string baseUrl)
        {
            StringBuilder uri = new StringBuilder(baseUrl);
            ShippingApiRequest.SubstitueResourceParameters(this, uri);
            ShippingApiRequest.AddRequestQuery(this, uri);
            return uri.ToString();
        }

        public string RecordingFullPath(string resource, ISession session)
        {
            return ShippingApiRequest.RecordingFullPath(this, resource, session);
        }

        public void SerializeBody(StreamWriter writer, ISession session)
        {
            ShippingApiRequest.SerializeBody(this, writer, session);
        }
    }
}
