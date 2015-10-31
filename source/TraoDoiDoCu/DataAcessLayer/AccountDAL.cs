using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TraoDoiDoCu.Models;
using TraoDoiDoCu.Models.Account;

namespace TraoDoiDoCu.DataAcessLayer
{
    public class AccountDAL
    {
        TraoDoiDoCuEntities db = new TraoDoiDoCuEntities();

        #region # phần login
        public bool LoginIsValid(string UserName, string HashPassword)
        {
            User user = db.Users.FirstOrDefault(x => x.UserName == UserName && x.PassWord == HashPassword);
            if (user != null)
                return true;
            return false;
        }
        public bool IsActivedAccount(string UserName)
        {
            User user = db.Users.FirstOrDefault(x => x.UserName == UserName);
            if (user == null)
                return false;
            else if (user.ActiveCode == "active")
                return true;
            return false;
        }
        #endregion

        #region # phần register
        public bool CheckExistenceAccount(string Email, string UserName)
        {
            User user = db.Users.FirstOrDefault(x => x.UserName == UserName || x.Email == Email);
            if (user == null)
                return true;
            return false;
        }
        public string AddAccount(RegisterViewModel model)
        {            
            User user = new User();
            user.UserName = model.UserName;
            user.PassWord = model.Password;
            user.Email = model.Email;
            user.Admin = false;

            user.Products = null;

            string activation_code = Guid.NewGuid().ToString();
            user.ActiveCode = activation_code;

            try
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                return null;
            }
            return activation_code;
        }
        #endregion

        #region
        public bool ActivateAccount(string UserName, String ActivationCode)
        {
            User user = db.Users.FirstOrDefault(x => x.UserName == UserName && x.ActiveCode == ActivationCode);
            if (user.UserName == null)
                return false;
            user.ActiveCode = "active";
            db.SaveChanges();
            return true;
        }
        #endregion        
    }
}