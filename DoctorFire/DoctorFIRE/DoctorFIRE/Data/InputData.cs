using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoctorFIRE.Models;
using DoctorFIRE.Data;
using Microsoft.EntityFrameworkCore;

namespace DoctorFIRE.Data
{
    public static class DbInitializer
    {
        public static void Initialize(DoctorFIREContext context)
        {
            context.Database.EnsureCreated();

            // Look for any Contents
            if (context.CCbyIDs.Any())
            {
                return;   // DB has been seeded
            }

            if (context.Contexts.Any())
            {
                return; //  Contexts needs to be deleted
            }


            var contexts = new Context[]
            {
                new Context{Id=Guid.NewGuid(),Name="Alphabetical", Rank=1},
            };

            foreach (Context s in contexts)
            {
                context.Contexts.Add(s);
            }
            context.SaveChanges();



            var contentspecific = new Content[]
            {
                new Content{Id = Guid.NewGuid(), Text = "STAT 12-lead EKG", Soap = "S"}
            };

            foreach (Content c in contentspecific)
            {
                context.Contents.Add(c);
            }


            context.SaveChanges();


            int ContentMax = 2 + context.Contents.Count();



            CCbyID[] ccs = new CCbyID[ContentMax];
            

            for (int j = 0; j < ContentMax; j++)
            {
                ccs[j] = new CCbyID { CCbyIDID=j, ClickCount = 0, ContentID = j, ContentIDD = j, IdD = 0 };
            }

            
            foreach (CCbyID c in ccs)
            {
                context.CCbyIDs.Add(c);
            }

            
            context.SaveChanges();


        }
    }
}
