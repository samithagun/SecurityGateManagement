using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PassIssueSystem.Controllers;
using PassIssueSystem.Models;

namespace PassIssueSystem.Facades
{
    public class PassRequestFacade
    {
        public static int SavePassRequest(PassRequestHed passReq)
        {
            PassRequestController PRC = new PassRequestController();
            int Ref = PRC.SavePassReqHed(MapModelToHed(passReq));
            if (Ref > 0)
            {
                PRC.SavePassReqDet(MapModelToDet(passReq), Ref);                
            }

            return Ref;
        }

        private static PassRequestHed MapModelToHed(PassRequestHed passReq)
        {
            PassRequestHed Obj = new PassRequestHed();

            Obj.RequiredFrom = passReq.RequiredFrom;
            Obj.RequiredTo = passReq.RequiredTo;
            Obj.Comments = passReq.Comments;
            Obj.CompanyID = "Lumiere";

            return Obj;
        }

        public static List<PassRequestDet> MapModelToDet(PassRequestHed passReq)
        {
            List<PassRequestDet> Obj = new List<PassRequestDet>();
            foreach (PassRequestDet Item in passReq.PassRequestDets)
            {
                Obj.Add(new PassRequestDet()
                {
                    PersonName = Item.PersonName,
                    PersonNIC = Item.PersonNIC,
                    PassCode = Item.PassCode,
                    MobileNo = Item.MobileNo,
                    PassFee = 0
                });
            }

            return Obj;
        }

        public static List<PassReqVehicle> MapModelToVehi(PassRequestHed passReq)
        {
            List<PassReqVehicle> Obj = new List<PassReqVehicle>();
            foreach (PassReqVehicle Item in passReq.PassReqVehicles)
            {
                Obj.Add(new PassReqVehicle()
                {
                    VehicleCode = Item.VehicleCode,
                    VehicleNo = Item.VehicleNo,
                    VehicleFee = Item.VehicleFee
                });
            }

            return Obj;
        }
    }
}