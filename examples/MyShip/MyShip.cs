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
                    { "ApiKey", "9DugRoGkf4rvvlMfnlxumj3ggzzWDCWY" },
                    { "ApiSecret", "IY5tGLbnNAV7dYH5" },
                    { "RatePlan", "YOUR_RATE_PLAN" },
                   /// { "ShipperID", "3000107147" },
                 ///   { "DeveloperID", "58172307" }
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
                          .Name("Manager")
                          .Phone("620-555-0000")
                          .Email("shop@example.com")

                             .AddressLines("643 Greenway RD")
                              .CityTown("Boone").StateProvince("NC")
                              .PostalCode("28607")
                              .CountryCode("US")
                              )
                         .FromAddress((Address)AddressFluent<Address>.Create()
                              .Company("Supplies")
                               .Name("Manager")
                               .Phone("415-555-0000")
                               .Email("supplies@example.com")
                              .AddressLines("545 Market St")
                              .CityTown("San Francisco").StateProvince("CA").PostalCode("94105")
                              .CountryCode("US")
                              )
                         .Parcel((Parcel)ParcelFluent<Parcel>.Create().
                              Dimension(6, 4, 1, UnitOfDimension.IN)
                              .Weight(38, UnitOfWeight.OZ))
                         .Rates(RatesArrayFluent<Rates>.Create().
                              Add().Carrier(Carrier.FEDEX)
                              ///.Service(Services.TwoDA)
                              .ParcelType(ParcelType.PKG))


                        ///  .SpecialService<ISpecialServices>(SpecialServiceCodes.INS,0M,new Parameter() { Name = "INPUT_VALUE", Value = "0" })))

                        /// .SpecialService<SpecialServices>(SpecialServiceCodes.INS, 0M, new Parameter("INPUT_VALUE", "50")))

                        ///  .Documents((List<IDocument>)DocumentsArrayFluent<Document>.Create()
                        ///      .ShippingLabel(ContentType.URL, Size.DOC_4X6, FileFormat.PDF))

                        ///  .ShipmentOptions(ShipmentOptionsArrayFluent<ShipmentOptions>.Create()
                        ////      .ShipperId(sandbox.GetConfigItem("ShipperID"))
                        /// .MinimalAddressvalidation().AddOption(ShipmentOption.PERMIT_NUMBER, "SHFL")
                        /// )
                        ///
                        
                        .TransactionId(Guid.NewGuid().ToString().Substring(15));

            shipment.CarrierAccountId = "323832c1-0646-4c99-80f5-7a7fe97ecdd9";

            shipment.IncludeDeliveryCommitment = true;
            var shipmentreq = (Shipment)shipment;
            shipmentreq.IncludeDeliveryCommitment = true;
            ///shipmentreq.CarrierAccountId= "323832c1-0646-4c99-80f5-7a7fe97ecdd9";
            var label = Api.Rates(shipment).GetAwaiter().GetResult(); ;
           

        }
    }
}

