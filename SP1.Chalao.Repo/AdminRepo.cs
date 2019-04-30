using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SP1.Chalao.Entities;
using SP1.Chalao.Framework.Constants;
using SP1.Chalao.Framework.Helper;
using SP1.Chalao.Framework.Objects;
using SP1.Chalao.Repo;

namespace SP1.Chalao.Repo
{
    public class AdminRepo : BaseRepo
    {
        public Result<List<Admins>> GetAll(string key = "")
        {
            var result = new Result<List<Admins>>();
            try
            {
                var list = Context.Admins.Include("Users").ToList();

                if (ValidationHelper.IsValidString(key))
                    list = list.Where(a => a.Users.Name.ToLower().Contains(key.ToLower())).ToList();

                result.Data = list;

            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;
            }

            return result;
        }

        public Result<Admins> GetByID(int id)
        {
            var result = new Result<Admins>();
            try
            {
                result.Data = Context.Admins.Include("Users").FirstOrDefault(d=> d.ID == id);
            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;
            }

            return result;
        }
        public Result<Admins> Save(Admins value)
        {
            var result = new Result<Admins>();

            try
            {
                var objToSave1 = Context.Users.SingleOrDefault(a => a.ID == value.ID);

                if (objToSave1 == null)
                {
                    objToSave1 = new Users();
                    Context.Users.Add(objToSave1);
                }

                if (!IsValidToSave(value, result))
                    return result;

                objToSave1.Name = value.Users.Name;
                objToSave1.Email = value.Users.Email;
                objToSave1.Mobile = value.Users.Mobile;
                objToSave1.Password = value.Users.Password;
                objToSave1.User_TypeID = (int) EnumCollection.UserTypeEnum.Admin;

                Context.SaveChanges();

                var objToSave2 = Context.Admins.SingleOrDefault(a => a.ID == value.ID);

                if (objToSave2 == null)
                {
                    objToSave2 = new Admins();
                    Context.Admins.Add(objToSave2);
                }

                objToSave2.ID = objToSave1.ID;
                objToSave2.JoinDate = value.JoinDate;

                Context.SaveChanges();
                
                result.Data = Context.Admins.Include("Users").FirstOrDefault(d => d.ID == objToSave1.ID);

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
            var result =  new Result<bool>();

            try
            {
                var objToDelete1 = Context.Admins.FirstOrDefault(d => d.ID == id);
                var objToDelete2 = Context.Users.FirstOrDefault(d=> d.ID == id);

                if (objToDelete1 == null || objToDelete2 == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid Admin ID";
                    return result;
                }

                Context.Admins.Remove(objToDelete1);
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

        private bool IsValidToSave(Admins obj, Result<Admins> result)
        {
            if (!ValidationHelper.IsValidString(obj.Users.Name))
            {
                result.HasError = true;
                result.Message = "Invalid Name";
                return false;
            }

            if (Context.Users.Any(ui => ui.Email == obj.Users.Email && ui.ID != obj.ID))
            {
                result.HasError = true;
                result.Message = "Email already exists";
                return false;
            }

            return true;
        }
    }
}
