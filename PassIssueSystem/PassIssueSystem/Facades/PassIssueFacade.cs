﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PassIssueSystem.Models;

namespace PassIssueSystem.Facades
{
    public class PassIssueFacade
    {
       public static PassIssueHed MapModelToHed(PassRequestHed passReq)
        {
            PassIssueHed Obj = new PassIssueHed();

            Obj.PassReqNo = passReq.PassReqNo;
            Obj.ValidFrom = passReq.RequiredFrom;
            Obj.ValidTo = passReq.RequiredTo;
            Obj.IssueDate = DateTime.Today.Date;

            return Obj;
        }

        public static List<PassIssueDet> MapModelToDet(PassRequestHed passReq)
        {
            List<PassIssueDet> Obj = new List<PassIssueDet>();
            Entities db = new Entities();

            foreach (PassRequestDet Item in passReq.PassRequestDets)
            {
                if (Item.PersonNIC != null)
                {
                    Obj.Add(new PassIssueDet()
                    {
                        PersonNIC = Item.PersonNIC,
                        MobileNo = Item.MobileNo,
                    });
                }
            }

            return Obj;
        }

        public static List<PassIssueVehicle> MapModelToVehi(PassRequestHed passReq)
        {
            List<PassIssueVehicle> Obj = new List<PassIssueVehicle>();
            Entities db = new Entities();

            foreach (PassReqVehicle Item in passReq.PassReqVehicles)
            {
                if (Item.VehicleNo != null)
                {
                    Obj.Add(new PassIssueVehicle()
                    {
                        VehicleNo = Item.VehicleNo,
                    });
                }
            }

            return Obj;
        }

        /// <summary>
        /// Updates the pass req.
        /// </summary>
        /// <param name="passReq">The pass req.</param>
        /// <returns></returns>
        public static PassRequestHed UpdatePassReq(PassRequestHed passReq)
        {
            passReq.Issued = true;
            passReq.Paid = true;

            return passReq;
        }
    }
}