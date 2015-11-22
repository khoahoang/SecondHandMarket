using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TraoDoiDoCu.Models
{
    public class UserModel
    {
        TraoDoiDoCuEntities tde = new TraoDoiDoCuEntities();

        public bool SendMessage(int idSender, int idReciver, string content)
        {
           
            Messages mess = new Messages();
            mess.Content = content;
            mess.ID_Sender = idSender;
            mess.ID_Reciver = idReciver;

            tde.Messages.Add(mess);
            tde.SaveChangesAsync();
            return true;
        }

        public List<String> Search(string name)
        {
            return tde.Users.Where(p => p.UserName.Contains(name)).Select(p => p.UserName).ToList();
        }

        public void BanAccount(List<String> username)
        {
            foreach (Users a in tde.Users)
            {
                if (username.Contains(a.UserName))
                {
                    a.Ban = true;
                    a.BanDate = DateTime.Now;
                    a.BanTime = 30;
                }
                    
            }
            tde.SaveChangesAsync();
        }

        public void CheckBanAccount()
        {
            foreach (Users a in tde.Users)
            {
                if (DateTime.Compare(DateTime.Now, (DateTime)a.BanDate) > 30)
                {
                    a.Ban = false;
                    a.BanDate = DateTime.Now;
                    a.BanTime = 0;
                }
            }

            tde.SaveChangesAsync();
        }

        public void DeleteAccount(List<String> username)
        {
            List<Users> users = new List<Users>();
            foreach (Users a in tde.Users)
            {
                if (username.Contains(a.UserName))
                    users.Add(a);
            }

            foreach (Users a in users)
                tde.Users.Remove(a);

            tde.SaveChanges();
        }
    }
}