using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PassIssueSystem.Models;

namespace PassIssueSystem.Facades
{
    public class PaymentFacade
    {

        public static decimal GetPassTotal(int ReqNo)
        {
            decimal Total = 0;
            Entities db = new Entities();

            var DetTotal = db.PassRequestDets.Where(r => r.PassReqNo == ReqNo).Sum(p => p.PassFee);

            // (decimal?) will return null if there are no records
            var VehiTotal = db.PassReqVehicles.Where(r => r.PassReqNo == ReqNo).Sum(p => (decimal?)p.VehicleFee);
            
            Total = DetTotal + Convert.ToDecimal(VehiTotal);
            
            return Total;
        }
    }
}