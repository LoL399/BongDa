using SoccerWorld.Areas.User.Tools;
using SoccerWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoccerWorld.DAO
{
    public class UserDB
    {
        SoccerStoreEntities dbm = new SoccerStoreEntities();

        public void RegistNew(string name, string pass, string email, string diachi)
        {
            UserTB newuser = new UserTB();
            newuser.Username = name;
            newuser.Password = PassUtility.MD5Hash(pass);
            dbm.UserTBs.Add(newuser);
            UserInfo info = new UserInfo();
            info.Username = name;
            info.Email = email;
            info.DiaChi = diachi;
            dbm.UserInfos.Add(info);
            dbm.SaveChanges();
        }


        public int Validate(string pass, string email, string name)
        {
            if (!PassUtility.ValidatePass(pass))
            {
                return 1;
            }
            if (!email.Contains("@"))
            {
                return 2;
            }
            if (dbm.UserTBs.Any(x => x.Username == name))
            {
                return 3;
            }
            return 0;

        }
        public bool checkSignin(string name, string pass)
        {
            if (dbm.UserTBs.Any(x => x.Username == name))
            {
                var check = dbm.UserTBs.Where(x => x.Username == name).FirstOrDefault();
                if (check.Password == PassUtility.MD5Hash(pass))
                {
                    return true;
                }
            }
            return false;
        }
    }
}