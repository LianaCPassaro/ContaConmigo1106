//using ContaConmigo.Model;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace ContaConmigo.DataAccess.Managers
//{
//    public class RequestDonorManager
//    {
//        public static void AgregarSolicitud(string Name_Request_Don, string Last_Name_Request_Don, int CityId, DateTime Last_Date_Replacement, int AmountDonor, int InstitutionId, string Comment, string Phone_Number, DateTime Birthday, string Completed, int UserId, List<int> bloodGroupItems, List<int> bloodFactorItems) 
//        {
//            using (var db = new ContaConmigoEntities())
//            {
//                RequestDonor model = new RequestDonor()
//                {
//                    Name_Request_Don = model.Name_Request_Don,
//                    Last_Name_Request_Don = model.Last_Name_Request_Don,
//                    CityId = model.CityId,
//                    Last_Date_Replacement = model.Last_Date_Replacement,
//                    AmountDonor = model.AmountDonor,
//                    InstitutionId = model.InstitutionId,
//                    Comment = model.Comment,
//                    Phone_Number = model.Phone_Number,
//                    Birthday = model.Birthday,
//                    Completed = model.Completed,
//                    UserId = model.UserId
//                };

//                foreach (var factorID in bloodFactorItems)
//                {
//                    var factor = model.BloodFactorItems.Find(factorID);
//                    model.RequestDonorBlood.Add(factor);
//                }
//                db.RequestDonors.Add(model);
//                db.SaveChanges();
//            }
//        }
//    }
//}