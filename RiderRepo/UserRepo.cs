using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SP1.Chalao.Entities;
using SP1.Chalao.Framework.Objects;
using RiderRepo;

namespace ATP2.SMS.Repo
{
    public class UserRepo : BaseRepo
    {
        public Result<Users> Save(Users value)
        {
            var result = new Result<Users>();

            try
            {
                var objToSave = Context.Users.SingleOrDefault(u => u.ID == value.ID);
                if (objToSave == null)
                {
                    objToSave = new Users();
                    Context.Users.Add(objToSave);
                }

                if (!IsValidToSave(value, result))
                    return result;

                objToSave.Name = value.Name;
                objToSave.Email = value.Email;
                objToSave.Password = value.Password;
                objToSave.Mobile = value.Mobile;
                objToSave.User_TypeID = value.User_TypeID;

                Context.SaveChanges();
                result.Data = objToSave;
            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;
            }

            return result;
        }

        public Result<Users> Login(string email, string password)
        {
            var result = new Result<Users>();

            try
            {
                var objToSave = Context.Users.SingleOrDefault(u => u.Email == email && u.Password == password);
                if (objToSave == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid Email or Password";
                }

                result.Data = objToSave;
            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;
            }

            return result;
        }

        private bool IsValidToSave(Users u, Result<Users> result)
        {
            if (u.Password.Length < 6)
            {
                result.HasError = true;
                result.Message = "Password should be 6 characters long";
                return false;
            }

            if (Context.Users.Any(ui => ui.Email == u.Email && ui.ID != u.ID))
            {
                result.HasError = true;
                result.Message = "Email already exists";
                return false;
            }

            return true;
        }
    }
}
