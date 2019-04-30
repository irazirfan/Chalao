using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SP1.Chalao.Entities;
using SP1.Chalao.Framework.Objects;
using SP1.Chalao.Framework.Constants;
using SP1.Chalao.Framework.Helper;
using SP1.Chalao.Repo;

namespace SP1.Chalao.Repo
{
    public class UserRepo : BaseRepo
    {
        public Result<List<Users>> GetAll(string key = "")
        {
            var result = new Result<List<Users>>();
            try
            {
                var list = Context.Users.Include("Riders").ToList();

                if (ValidationHelper.IsValidString(key))
                    list = list.Where(a => a.Rider.Users.Name.ToLower().Contains(key.ToLower())).ToList();

                result.Data = list;

            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;
            }

            return result;
        }

        public Result<Users> Save(Users value)
        {
            var result = new Result<Users>();

            try
            {
                var objToSave = Context.Users.SingleOrDefault(u => u.ID == value.ID);
                if (objToSave == null)
                {
                    objToSave = new Users();
                    if(value.User_TypeID==(int)EnumCollection.UserTypeEnum.Rider)
                        objToSave.Rider = new Riders();

                    Context.Users.Add(objToSave);
                }

                if (!IsValidToSave(value, result))
                    return result;

                objToSave.Name = value.Name;
                objToSave.Email = value.Email;
                objToSave.Password = value.Password;
                objToSave.Mobile = value.Mobile;
                objToSave.User_TypeID = value.User_TypeID;
                if (value.User_TypeID == (int) EnumCollection.UserTypeEnum.Rider)
                {
                    objToSave.Rider.DOB = value.Rider.DOB;
                }


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

        public Result<Riders> GetByID(int id)
        {
            var result = new Result<Riders>();
            try
            {
                result.Data = Context.Riders.Include("Users").FirstOrDefault(d => d.ID == id);
            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;
            }

            return result;
        }

        public Result<bool> Delete(int id)
        {
            var result = new Result<bool>();

            try
            {
                var objToDelete1 = Context.Riders.FirstOrDefault(d => d.ID == id);
                var objToDelete2 = Context.Users.FirstOrDefault(d => d.ID == id);

                if (objToDelete1 == null || objToDelete2 == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid Rider ID";
                    return result;
                }

                Context.Riders.Remove(objToDelete1);
                Context.Users.Remove(objToDelete2);
                Context.SaveChanges();

                result.Data = true;
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
