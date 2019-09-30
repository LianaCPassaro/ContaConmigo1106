//using ContaConmigo.Model;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace ContaConmigo.DataAccess.Managers
//{
//    public class RequestDonorManager
//    {
//        public static void AgregarSolicitud(string name_Request_Don, string last_Name_Request_Don, int cityId, 
//            DateTime last_Date_Replacement, int amountDonor, int institutionId, string comment, string phone_Number, 
//            DateTime birthday, string completed, int userId, List<int> bloodGroupFactorItem)
//        {
//            using (ContaConmigoEntities context = new ContaConmigoEntities())
//            {
//                var requestDonor = new RequestDonor()
//                {
//                    Name_Request_Don = name_Request_Don,
//                    Last_Name_Request_Don = last_Name_Request_Don,
//                    CityId = cityId,
//                    Last_Date_Replacement = last_Date_Replacement,
//                    AmountDonor = amountDonor,
//                    InstitutionId = institutionId,
//                    Comment = comment,
//                    Phone_Number = phone_Number,
//                    Birthday = birthday,
//                    Completed = completed,
//                    UserId = userId
//                };

//                foreach (var factorID in bloodGroupFactorItem)
//                {
//                    var factor = context.GroupFactorBloods.Find(factorID);
//                    d,
//                    requestDonor.RequestDonorBloods.Add(factor);
                    
//                }
//                context.RequestDonors.Add(requestDonor);
//                context.SaveChanges();
//            }
//        }
//    }
//}