

namespace PitneyBowes.Developer.ShippingApi
{
    /// <summary>
    /// Response Only, USPS and UPS Only. Additional fees or surcharges for the shipment. 
    /// Each object in this array has two fields, name and fee.
    /// </summary>
    public class CarrierSurcharge : ICarrierSurcharge
    {
        /// <summary>
        /// Name of the surcharge
        /// </summary>
        public Surcharges Surcharge { get; set; }
        /// <summary>
        /// Amount of the surcharge
        /// </summary>
        public decimal Fee { get; set; }
    }
}
