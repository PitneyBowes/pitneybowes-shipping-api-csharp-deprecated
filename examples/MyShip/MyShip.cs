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
                { "ApiKey", "spg8w5vx2VtaLj0V4rcaYe3BdAZYjGZR" },
                { "ApiSecret", "lYWNnpOn820JBGpV" },
               /// { "RatePlan", "" },
                { "ShipperID", "9028249789" },
            
            };
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder
                .AddInMemoryCollection(configs)
                .AddJsonFile(Path.Combine(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory), "shippingapisettings.json"), optional: true, reloadOnChange: true);



            sandbox.GetConfigItem = (c) => configurationBuilder.Build()[c];
            Model.RegisterSerializationTypes(sandbox.SerializationRegistry);
            Globals.DefaultSession = sandbox;



           
            ICustoms custom = new Customs();
            IReference reference = new Reference();
            reference.Name = "ORDER_NUMBER";
            reference.Value = "111";
            


            ICustomsItems customsItems = new CustomsItems
            {
                Description = "sssss",
              //  HSTariffCode= "test create shipment",
                Quantity = 1,
                OriginCountryCode = "US",
                Url= "www.ebay.com",
                ItemId= "uncommodity"
            };



            custom.AddCustomsItems(customsItems);
            
           

            custom.CustomsInfo = new CustomsInfo() { CurrencyCode = "USD" };

            
         
            var shipment = (Shipment)ShipmentFluent<Shipment>.Create()
                           .Reference<Shipment, Reference>("111" )


                          .ToAddress((Address)AddressFluent<Address>.Create()
                         
                          .Company("Shop")
                          .Name("James Brother")
                          .Phone("620-555-0000")
                          .Email("mary@example.com")
                          .Residential(true)


                             .AddressLines("31st Ave NW")
                              .CityTown("Danbury").StateProvince("NY")
                              .PostalCode("L9P1A5")
                              .CountryCode("CA")
                              )
                         .FromAddress((Address)AddressFluent<Address>.Create()
                              .Company("Supplies")
                               .Name("Manami Mukherjee")
                               .Phone("334-000-0000 ")
                               .Email("kathryn@example.com")
                              .AddressLines("2352 Bent Creek Rd")
                              .CityTown("Danbury").StateProvince("CT").PostalCode("13326")
                              .CountryCode("US")).Customs(custom)
                         .Parcel((Parcel)ParcelFluent<Parcel>.Create()
                           .Weight(0.005M, UnitOfWeight.GM)
                           .CurrencyCode("USD")
                           .Dimension(5, 2, 3, UnitOfDimension.IN))
                         .Rates(RatesArrayFluent<Rates>.Create().
                              Add().Carrier(Carrier.PBI)
                               .Service(Services.PBXPS
                               )
                              .ParcelType(ParcelType.PKG)

                              .SpecialService<SpecialServices>(SpecialServiceCodes.INS, 0M, new Parameter("INPUT_VALUE", "50"))
                             . SpecialService<SpecialServices>(SpecialServiceCodes.ADSIG,0M, new Parameter("INPUT_VALUE","0")))
                              .Documents((List<IDocument>)DocumentsArrayFluent<Document>.Create()
                              .ShippingLabel(ContentType.URL, Size.DOC_8X11, FileFormat.PDF)
                              )

                          
                         .ShipmentOptions(ShipmentOptionsArrayFluent<ShipmentOptions>.Create()

                              
                              .ShipperId(sandbox.GetConfigItem("ShipperID"))
                              .Option(ShipmentOption.CARRIER_FACILITY_ID, "US_ELOVATIONS_KY")
                             .Option(ShipmentOption.CLIENT_FACILITY_ID, "CVG"))


                         .TransactionId(Guid.NewGuid().ToString().Substring(15));



            shipment.IncludeDeliveryCommitment = true;
            var shipmentreq = (Shipment)shipment;
            
            
            shipmentreq.IncludeDeliveryCommitment = true;
          //  shipmentreq.domesticShipmentDetails = "USPS-3085333285";
            //shipmentreq.MinimalAddressValidation = "true";
           // shipmentreq.IntegratorCarrierId = " ";
           // shipmentreq.ParcelTrackingNumber = "USPS-3085333285";

            // shipmentreq.IntegratorCarrierId = "New Carrier ID ";
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