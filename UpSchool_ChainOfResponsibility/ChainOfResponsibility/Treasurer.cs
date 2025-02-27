﻿using System;
using UpSchool_ChainOfResponsibility.DAL;
using UpSchool_ChainOfResponsibility.DAL.Entities;

namespace UpSchool_ChainOfResponsibility.ChainOfResponsibility
{
    public class Treasurer: Employee
    {
        public override void ProcessRequest(WithdrawViewModel req)
        {
            Context context = new Context();

            if (req.Amount <= 40000)
            {
                BankProcess bankProcess = new BankProcess();           
                    bankProcess.EmployeeName = "Veznedar - Ayşenur Yıldız";
                    bankProcess.Description = "Müşteriye talep olmuş olduğu ödemesi vezne sorumlusu tarafından gerçekleştirildi";
                    bankProcess.Amount = req.Amount;
                    bankProcess.CustomerName = req.CustomerName;
                    context.BankProcesses.Add(bankProcess);
                    context.SaveChanges();
                
                //db ye kaydetme işlemi
                //Console.WriteLine("{0} tarafından para çekme işlemi onaylandı #{1}",
                //    this.GetType().Name, p.Amount);
            }
            else if (NextApprover != null)
            {
                BankProcess bankProcess = new BankProcess();
                bankProcess.EmployeeName = "Veznedar - Ayşenur Yıldız";
                bankProcess.Description = "Müşteriye talep olmuş olduğu ödemesi vezne sorumlusu tarafından gerçekleştirildi";
                bankProcess.Amount = req.Amount;
                bankProcess.CustomerName = req.CustomerName;

                context.BankProcesses.Add(bankProcess);
                context.SaveChanges();
                NextApprover.ProcessRequest(req);
                
            }
        }
    }
}
