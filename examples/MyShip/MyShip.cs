using System;
using System.Text;
using System.Linq;
using System.Security;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections.Generic;
using PitneyBowes.Developer.ShippingApi;
using PitneyBowes.Developer.ShippingApi.Mock;
using PitneyBowes.Developer.ShippingApi.Fluent;
using PitneyBowes.Developer.ShippingApi.Model;
using PitneyBowes.Developer.ShippingApi.Rules;
using Microsoft.Extensions.Configuration;




namespace SampleApplicationNewGisticLabelGeneration
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
                    { "ApiKey", "YOUR_API_KEY" },
                    { "ApiSecret", "YOUR_API_SECRET" },
                    ///{ "ShipperID", "9025678752" },
                   /// { "DeveloperID", "<Developer_Id>" },
                {"RecordAPICalls","true" }
                };
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder
                .AddInMemoryCollection(configs)
                .AddJsonFile(Globals.GetConfigPath("shippingapisettings.json"), optional: true, reloadOnChange: true);

            sandbox.GetConfigItem = (c) => configurationBuilder.Build()[c];
            Model.RegisterSerializationTypes(sandbox.SerializationRegistry);
            Globals.DefaultSession = sandbox;

            var shipment = ShipmentFluent<Shipment>.Create()
                 .ToAddress((Address)AddressFluent<Address>.Create()
                    .AddressLines("704 Hickory Hill Rd")
                    .Name("Recipent_Name")
                    .CityTown("Wyckoff").StateProvince("NJ").PostalCode("07481-1603")
                     .CountryCode("US")
                     .Verify())
                 .FromAddress((Address)AddressFluent<Address>.Create()
                    .Company("Baker's Best Health")
                    .Name("Sender_Name")
                    .AddressLines("P.O. BOX 2099")
                    .CityTown("Wixom").StateProvince("MI").PostalCode("48393-2099")
                    .CountryCode("US"))
                 .Parcel((Parcel)ParcelFluent<Parcel>.Create()
                          .Dimension(12, 12, 10)
                           .Weight(16m, UnitOfWeight.OZ))
                 .Rates(RatesArrayFluent<Rates>.Create().Add().Carrier(Carrier.NEWGISTICS)
                       .NewgisticsRates<Rates>(Services.BPM))
                 .Documents((List<IDocument>)DocumentsArrayFluent<Document>.Create()
                        .ShippingLabel(ContentType.URL, Size.DOC_4X4, FileFormat.PDF))
                 .ShipmentOptions(ShipmentOptionsArrayFluent<ShipmentOptions>.Create()
                        .ShipperId("9025678752").AddOption(ShipmentOption.CLIENT_FACILITY_ID, "0093").AddOption(ShipmentOption.CARRIER_FACILITY_ID, "1585"))
                 .TransactionId(Guid.NewGuid().ToString().Substring(15));

            var label = Api.CreateShipment((Shipment)shipment).GetAwaiter().GetResult();
        }
    }
}
