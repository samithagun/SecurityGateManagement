using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PassIssueSystem.Models;

namespace PassIssueSystem.Facades
{
    public class PassIssueFacade
    {
        private static PassIssueHed MapModelToHed(PassRequestHed passReq)
        {
            PassIssueHed Obj = new PassIssueHed();

            Obj.PassReqNo = passReq.PassReqNo;
            Obj.ValidFrom = passReq.RequiredFrom;
            Obj.ValidTo = passReq.RequiredTo;

            return Obj;
        }

        //public static List<PassRequestDet> MapModelToDet(PassRequestHed passReq)
        //{
        //    List<PassRequestDet> Obj = new List<PassRequestDet>();
        //    Entities db = new Entities();

        //    foreach (PassRequestDet Item in passReq.PassRequestDets)
        //    {
        //        if (Item.PersonNIC != null)
        //        {
        //            Obj.Add(new PassRequestDet()
        //            {
        //                PersonName = Item.PersonName,
        //                PersonNIC = Item.PersonNIC,
        //                PassCode = Item.PassCode,
        //                MobileNo = Item.MobileNo,
        //                PassFee = db.PassTypes.Where(p => p.PassCode == Item.PassCode).Select(t => t.PassFee).First(),
        //            });
        //        }
        //    }

        //    return Obj;
        //}

        //public static List<PassReqVehicle> MapModelToVehi(PassRequestHed passReq)
        //{
        //    List<PassReqVehicle> Obj = new List<PassReqVehicle>();
        //    Entities db = new Entities();

        //    foreach (PassReqVehicle Item in passReq.PassReqVehicles)
        //    {
        //        if (Item.VehicleNo != null)
        //        {
        //            Obj.Add(new PassReqVehicle()
        //            {
        //                VehicleCode = Item.VehicleCode,
        //                VehicleNo = Item.VehicleNo,
        //                VehicleFee = db.VehicleTypes.Where(v => v.VehicleCode == Item.VehicleCode).Select(t => t.VehicleFee).First(),
        //            });
        //        }
        //    }

        //    return Obj;
        //}
    }
}