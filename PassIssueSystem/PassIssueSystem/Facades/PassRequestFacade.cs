using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using PassIssueSystem.Controllers;
using PassIssueSystem.Models;

namespace PassIssueSystem.Facades
{
    public class PassRequestFacade
    {
        /// <summary>
        /// Saves the pass request.
        /// </summary>
        /// <param name="passReq">The pass req.</param>
        /// <returns></returns>
        public static int SavePassRequest(PassRequestHed passReq)
        {
            int Ref;

            PassRequestController PRC = new PassRequestController();
            using (TransactionScope Scope = new TransactionScope(TransactionScopeOption.Required))
            {
                Ref = PRC.SavePassReqHed(MapModelToHed(passReq));
                if (Ref > 0)
                {
                    PRC.SavePassReqDet(MapModelToDet(passReq), Ref);
                    PRC.SavePassReqVehi(MapModelToVehi(passReq), Ref);

                    Scope.Complete();
                }
            }

            return Ref;
        }

        /// <summary>
        /// Maps the model to hed.
        /// </summary>
        /// <param name="passReq">The pass req.</param>
        /// <returns></returns>
        private static PassRequestHed MapModelToHed(PassRequestHed passReq)
        {
            PassRequestHed Obj = new PassRequestHed();

            Obj.RequiredFrom = passReq.RequiredFrom;
            Obj.RequiredTo = passReq.RequiredTo;
            Obj.Comments = passReq.Comments;
            Obj.CompanyID = "Lumiere";

            return Obj;
        }

        /// <summary>
        /// Maps the model to det.
        /// </summary>
        /// <param name="passReq">The pass req.</param>
        /// <returns></returns>
        public static List<PassRequestDet> MapModelToDet(PassRequestHed passReq)
        {
            List<PassRequestDet> Obj = new List<PassRequestDet>();
            foreach (PassRequestDet Item in passReq.PassRequestDets)
            {
                if (Item.PersonNIC != null)
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
            }

            return Obj;
        }

        /// <summary>
        /// Maps the model to vehi.
        /// </summary>
        /// <param name="passReq">The pass req.</param>
        /// <returns></returns>
        public static List<PassReqVehicle> MapModelToVehi(PassRequestHed passReq)
        {
            List<PassReqVehicle> Obj = new List<PassReqVehicle>();
            foreach (PassReqVehicle Item in passReq.PassReqVehicles)
            {
                if (Item.VehicleNo != null)
                {
                    Obj.Add(new PassReqVehicle()
                    {
                        VehicleCode = Item.VehicleCode,
                        VehicleNo = Item.VehicleNo,
                        VehicleFee = Item.VehicleFee
                    });
                }
            }

            return Obj;
        }
    }
}