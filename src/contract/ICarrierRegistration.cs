using System;
using System.Collections.Generic;
using System.Text;

namespace PitneyBowes.Developer.ShippingApi
{
    /// <summary>
    /// This operation authorizes a merchant to use an additional carrier for use with a merchant’s Pitney Bowes Account. 
    /// The merchant must have an existing account with the carrier. Currently, this API supports integration with UPS.
    /// For step-by-step implementation of the API, please see the Carrier Registration Tutorial.
    /// When you issue this API call, Pitney Bowes verifies the merchant’s account with the carrier using information you 
    /// send in the API request.The carrier sends back to Pitney Bowes a unique set of credentials to use with the merchant’s 
    /// Pitney Bowes account.Pitney Bowes returns the credentials in the response to this API call but also returns a unique 
    /// shipperCarrierAccountId you use instead of the credentials. When the merchant performs an action that uses this carrier, 
    /// you need only pass the shipperCarrierAccountId. Pitney Bowes uses the ID to locate and send the required credentials 
    /// to the carrier.You pass the ID in the X-PB-Shipper-Carrier-AccountId request header of an API request.
    /// </summary>
    public interface ICarrierRegistration
    {
        /// <summary>
        /// Required. A unique identifier for the transaction, up to 25 characters.
        /// Important: You must ensure this is a unique id.
        /// </summary>
        string TransactionId { get; set; }

        /// <summary>
        /// Required.Your Pitney Bowes developer ID. To retrieve your developer ID, log into Developer Hub and click your 
        /// username and select Profile.
        /// </summary>
        string DeveloperId { get; set; }
        /// <summary>
        /// Required. The unique ID used to identify the merchant. To retrieve the merchant’s postalReportingNumber, issue the Get All Merchants API call.
        /// </summary>
        string PostalReportingNumber { get; set; }
        /// <summary>
        /// Required.The carrier to add to the merchant’s account.
        /// Valid value: UPS
        /// </summary>
        Carrier Carrier { get; set; }
        /// <summary>
        /// The merchant’s account number with the carrier.
        /// </summary>
        string AccountNumber { get; set; }
        /// <summary>
        /// Carrier Registration Address Object. The merchant’s “ship from” address as it appears on the carrier account.
        /// For UPS, this address must exactly match the “ship from” address on the merchant’s UPS invoice.
        /// </summary>
        IAddress AccountAddress { get; set; }
        /// <summary>
        /// Carrier Registration Address Object The merchant’s contact address.
        /// </summary>
        IAddress ContactAddress { get; set; }
        /// <summary>
        /// Carrier Registration Parameters.The parameters include information the merchant 
        /// received from the carrier and must provide to the developer. Each object in the array defines a parameter 
        /// in the form of a name-value pair. 
        /// 
        /// </summary>
        IEnumerable<IParameter> InputParameters { get; set; }
        /// <summary>
        /// Add parameter to InputParameters
        /// </summary>
        /// <param name="parameter"></param>
        IParameter AddInputParameter(IParameter parameter);
        /// <summary>
        /// Response Only.When Pitney Bowes verifies the merchant’s account with the carrier, the carrier generates 
        /// and sends back a set of credentials for use when the merchant accesses the carrier through the PB Shipping APIs. 
        /// These are not required for future API operations.Instead, Pitney Bowes provides the shipperCarrierAccountId 
        /// (below) to be passed with API calls.
        /// </summary>
        IEnumerable<IParameter> CarrierAccount { get; set; }
        /// <summary>
        /// Add a carrier account parameter
        /// </summary>
        /// <param name="parameter"></param>
        IParameter AddCarrierAccount(IParameter parameter);
        /// <summary>
        /// The identifier to use when the merchant performs an operation that uses this carrier account. The identifier is 
        /// passed in the X-PB-Shipper-Carrier-AccountId request header of the API request.
        /// </summary>
        string ShipperCarrierAccountId { get; set; }
    }

    /// <summary>
    /// Validation extension methods
    /// </summary>
    public static partial class InterfaceValidators
    {
        /// <summary>
        /// If false, the object underlying the interface is not a valid carrier registration. If true, the object may or 
        /// may not be valid.
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool IsValidCarrierRegistration(this ICarrierRegistration r)
        {
            if (r.Carrier != Carrier.UPS)
            {
                return false;
            }
            return true;
        }
    }
}
