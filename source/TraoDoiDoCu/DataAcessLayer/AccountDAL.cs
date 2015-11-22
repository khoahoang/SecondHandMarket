using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
            Users user = db.Users.FirstOrDefault(x => x.UserName == UserName && x.PassWord == HashPassword);
            if (user != null)
                return true;
            return false;
        }
        public bool IsActivedAccount(string UserName)
        {
            Users user = db.Users.FirstOrDefault(x => x.UserName == UserName);
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
            Users user = db.Users.FirstOrDefault(x => x.UserName == UserName || x.Email == Email);
            if (user == null)
                return true;
            return false;
        }
        public string AddAccount(RegisterViewModel model)
        {            
            Users user = new Users();
            user.UserName = model.UserName;
            user.PassWord = model.Password;
            user.Email = model.Email;
            user.Admin = false;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Phone = model.Phone;

            user.Product = null;

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

        #region # phần active account
        public bool ActivateAccount(string UserName, String ActivationCode)
        {
            Users user = db.Users.FirstOrDefault(x => x.UserName == UserName && x.ActiveCode == ActivationCode);
            if (user.UserName == null)
                return false;
            user.ActiveCode = "active";
            db.SaveChanges();
            return true;
        }
        #endregion        
    
        private float TIME_OVERDUE_RESET_PASSWORD = 2;
        #region # phần forgot password
        public bool CheckMailExistence(string Email)
        {
            Users user = db.Users.FirstOrDefault(x => x.Email == Email);
            if (user != null)
                return true;
            return false;
        }
        public string SetUpResetPassword(string Email)
        {
            Users user = db.Users.FirstOrDefault(x => x.Email == Email);
            string reset_pass_code = Guid.NewGuid().ToString();
            user.ResetPassword = reset_pass_code;
            DateTime time = new DateTime();
            time = DateTime.Now;
            user.DateRequest = time;

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return null;
            }
            return user.ResetPassword;
        }
        public bool CheckResetPassword(string Email, string ResetCode)
        {
            Users user = db.Users.FirstOrDefault(x => x.Email == Email && x.ResetPassword == ResetCode);
            if (user != null)
            {
                DateTime now = DateTime.Now;
                DateTime time = user.DateRequest.Value.ToLocalTime();
                TimeSpan span = now.Subtract(time);
                if (span.Hours > TIME_OVERDUE_RESET_PASSWORD)
                    return false;
                else
                    return true;
            }
            return false;
        }
        public bool ResetPassword(ResetPasswordViewModel ressetPassVM)
        {
            Users user = db.Users.FirstOrDefault(x => x.Email == ressetPassVM.Email && x.ResetPassword == ressetPassVM.ResetCode);
            if (user != null)
            {
                try
                {
                    var hash = Encoding.ASCII.GetBytes("san" + ressetPassVM.Password);
                    var sha1 = new SHA1CryptoServiceProvider();
                    byte[] sha1hash = sha1.ComputeHash(hash);
                    var hashedPassword = System.Text.Encoding.ASCII.GetString(sha1hash);

                    user.PassWord = hashedPassword;
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            return false;
        }
        #endregion

        #region phần Get User Infomation
        public Users getUserInfo(int userID)
        {
            Users result = new Users();
            result = db.Users.FirstOrDefault(x => x.ID == userID);
            return result;

        }

        #endregion

        #region phần Update User Info
        public bool updateUserInfo(int userID, Users newUserInfo)
        {
            Users Target = db.Users.FirstOrDefault(x => x.ID == userID);
            if (Target != null)
            {
                if (newUserInfo.PassWord.Count() > 0)
                    Target.PassWord = newUserInfo.PassWord;
                if (newUserInfo.FirstName.Count() > 0)
                    Target.FirstName = newUserInfo.FirstName;
                if (newUserInfo.LastName.Count() > 0)
                    Target.LastName = newUserInfo.LastName;
                if (newUserInfo.Phone.Count() > 0)
                    Target.Phone = newUserInfo.Phone;
            }
            db.Users.Attach(Target);
            var entry = db.Entry(Target);
            entry.Property(x => x.PassWord).IsModified = true;
            entry.Property(x => x.FirstName).IsModified = true;
            entry.Property(x => x.LastName).IsModified = true;
            entry.Property(x => x.Phone).IsModified = true;

            db.SaveChanges();
            return true;
        }
        #endregion

        #region phần DeleteUser
        public bool deleteUserAccount(int userID)
        {
            Users target = db.Users.FirstOrDefault(x => x.ID == userID);
            try
            {
                target.Ban = true;
                db.Users.Attach(target);
                var entry = db.Entry(target);
                entry.Property(x => x.Ban).IsModified = true;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }



        }

        #endregion
        public bool checkInvalidPassword(int userID, string hashedPassword)
        {
            Users user = db.Users.FirstOrDefault(x => x.ID == userID && x.PassWord == hashedPassword);
            if (user != null)
                return true;
            return false;
        }

        public int getIDFromUsername(string username)
        {
            int result = -1;
            Users target = db.Users.FirstOrDefault(x => x.UserName == username);
            if (target != null)
            {
                result = target.ID;
            }
            return result;
        }


        #region phần show followed products

        //get list ID of followed products
        public List<int> getIDProductsFollowed(string userName)
        {
            List<int> result = new List<int>();
            var User = db.Users.FirstOrDefault(x => x.UserName == userName);
            if (User != null)
            {
                var target = db.FollowProduct.Where(x => x.UserID == User.ID);
                if (target != null)
                {
                    foreach (FollowProduct t in target)
                    {
                        result.Add(t.ProductID);
                    }
                }
            }
            return result;
        }

        //get followed product using list of productID
        public List<Product> getProductsFollowed(List<int> lstOfProductIDs)
        {
            List<Product> result = new List<Product>();
            foreach (int id in lstOfProductIDs)
            {
                Product temp = db.Product.FirstOrDefault(x => x.ID == id);
                if (temp != null)
                    result.Add(temp);
            }
            return result;
        }

        //delete a followed product
        public bool deleteAFollowedProduct(int userID, int productID)
        {
            FollowProduct target = new FollowProduct();
            target = db.FollowProduct.FirstOrDefault(x => x.UserID == userID && x.ProductID == productID);
            if (target != null)
            {
                try
                {
                    db.FollowProduct.Remove(target);
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }

            }
            return false;

        }

        public bool deleteAFollowedProduct(int followedProductID)
        {
            FollowProduct target = new FollowProduct();
            target = db.FollowProduct.FirstOrDefault(x => x.ID == followedProductID);
            if (target != null)
            {
                try
                {
                    db.FollowProduct.Remove(target);
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }
        #endregion
    }
}