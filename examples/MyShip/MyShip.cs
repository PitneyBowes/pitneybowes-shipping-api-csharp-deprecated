using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using PitneyBowes.Developer.ShippingApi;
using PitneyBowes.Developer.ShippingApi.Model;
using PitneyBowes.Developer.ShippingApi.Fluent;

namespace MyShip
{
    class Program
    {
        static void Main(string[] args)
        {
            var sandbox = new Session()
            {
                EndPoint = "https://api-qa.pitneybowes.com",
                Requester = new ShippingApiHttpRequest()
            };
            var configs = new Dictionary<string, string>
                {
                    { "ApiKey", "your api key" },
                    { "ApiSecret", "your api secret" },
                    { "RatePlan", "YOUR_RATE_PLAN" },
                    { "ShipperID", "your shipper id" },
                                };
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder
                .AddInMemoryCollection(configs)
                .AddJsonFile(Path.Combine(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory), "shippingapisettings.json"), optional: true, reloadOnChange: true);

            sandbox.GetConfigItem = (c) => configurationBuilder.Build()[c];
            Model.RegisterSerializationTypes(sandbox.SerializationRegistry);
            Globals.DefaultSession = sandbox;
            var shipment = (Shipment)ShipmentFluent<Shipment>.Create()
                          .ToAddress((Address)AddressFluent<Address>.Create()
                          .Company("Shop")
                          .Name("Mary Jones")
                          .Phone("620-555-0000")
                          .Email("mary@example.com")

                             .AddressLines("284 W Fulton")
                              .CityTown("Garden City").StateProvince("KS")
                              .PostalCode("67846")
                              .CountryCode("US")
                              )
                         .FromAddress((Address)AddressFluent<Address>.Create()
                              .Company("Supplies")
                               .Name("Kathryn Smith")
                               .Phone("334-000-0000 ")
                               .Email("kathryn@example.com")
                              .AddressLines("2352 Bent Creek Rd")
                              .CityTown("Auburn").StateProvince("AL").PostalCode("36830")
                              .CountryCode("US"))
                              
                         .Parcel((Parcel)ParcelFluent<Parcel>.Create()
                           .Weight(20, UnitOfWeight.OZ))
                         .Rates(RatesArrayFluent<Rates>.Create().
                              Add().Carrier(Carrier.FEDEX)
                               .Service(Services.TwoDA_AM)
                              .ParcelType(ParcelType.PKG))
                              .Documents((List<IDocument>)DocumentsArrayFluent<Document>.Create()
                              .ShippingLabel(ContentType.URL, Size.DOC_4X6, FileFormat.PDF))

                         .ShipmentOptions(ShipmentOptionsArrayFluent<ShipmentOptions>.Create()
                              .ShipperId(sandbox.GetConfigItem("ShipperID"))
                             
                              )
                         .TransactionId(Guid.NewGuid().ToString().Substring(15));

            shipment.IncludeDeliveryCommitment = true;
            var shipmentreq = (Shipment)shipment;
            shipmentreq.IncludeDeliveryCommitment = true;
                var label = Api.CreateShipment(shipment).GetAwaiter().GetResult();
            if (label.Success)
            {
                var sw = new StreamWriter("label.png");
                foreach (var d in label.APIResponse.Documents)
                {
                    Api.WriteToStream(d, sw.BaseStream).GetAwaiter().GetResult();
                }
            }

        }
    }
}

