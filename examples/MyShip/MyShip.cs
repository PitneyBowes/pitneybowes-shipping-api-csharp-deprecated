using System;
using System.IO;
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
            var sandbox = new Session() { EndPoint = "https://api-sandbox.pitneybowes.com", Requester = new ShippingApiHttpRequest() };

            sandbox.AddConfigItem("ApiKey", "your api key");
            sandbox.AddConfigItem("ApiSecret", "your api secret");
            sandbox.AddConfigItem("ShipperID", "your shipper id");
            sandbox.AddConfigItem("DeveloperID", "your developer id");

            Model.RegisterSerializationTypes(sandbox.SerializationRegistry);
            Globals.DefaultSession = sandbox;

            var shipment = ShipmentFluent<Shipment>.Create()
                .PBPresortShipment<Shipment>("", "")
                .ToAddress((Address)AddressFluent<Address>.Create()
                    .AddressLines("643 Greenway Rd")
                    .PostalCode("28607")
                    .CountryCode("US")
                    .Verify())
               .FromAddress((Address)AddressFluent<Address>.Create()
                    .Company("Pitney Bowes Inc")
                    .AddressLines("27 Waterview Drive")
                    .CityTown("Shelton").StateProvince("CT").PostalCode("06484")
                    .CountryCode("US")
                    )
               
               .Parcel((Parcel)ParcelFluent<Parcel>.Create()
                    .Dimension(12, 0.25M, 9)
                    .Weight(3m, UnitOfWeight.OZ))
               .Rates(RatesArrayFluent<Rates>.Create()
                    .Carrier(Carrier.PBPRESORT)
                    .Service(Services.BPM)
                    .ParcelType(ParcelType.LGENV)
                    .CurrencyCode("USD")
                    )
               .Documents((List<IDocument>)DocumentsArrayFluent<Document>.Create()
                    .ShippingLabel(ContentType.BASE64, Size.DOC_4X6, FileFormat.PNG))
               .ShipmentOptions(ShipmentOptionsArrayFluent<ShipmentOptions>.Create()
                    .ShipperId("9026169668")    // ******* dont forget this one too *******
                    .PBPresortPermit("123")
                    )
               .TransactionId(Guid.NewGuid().ToString().Substring(15));

            var s = (Shipment)shipment;
            s.AddShipmentOptions(new ShipmentOptions() { ShipmentOption = ShipmentOption.MINIMAL_ADDRESS_VALIDATION, Value = "true" });

            s.IncludeDeliveryCommitment = true;
            var label = Api.CreateShipment(s).GetAwaiter().GetResult();
            if (label.Success)
            {
                var sw = new StreamWriter("label.pdf");
                foreach (var d in label.APIResponse.Documents)
                {
                    Api.WriteToStream(d, sw.BaseStream).GetAwaiter().GetResult();
                }
            }
        }
    }
}

