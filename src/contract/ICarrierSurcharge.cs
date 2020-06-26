using System;
using System.Collections.Generic;
using System.Text;

namespace PitneyBowes.Developer.ShippingApi
{
    /// <summary>
    /// Response Only, USPS and UPS Only. Additional fees or surcharges for the shipment. 
    /// Each object in this array has two fields, name and fee.
    /// </summary>
    public interface ICarrierSurcharge
    {
        /// <summary>
        /// Name of the surcharge
        /// </summary>
        String Name { get; set; }
        /// <summary>
        /// Amount of the surcharge
        /// </summary>
        decimal Fee { get; set; }
    }
}
