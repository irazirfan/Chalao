using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SP1.Chalao.Entities;
using SP1.Chalao.Framework.Constants;
using SP1.Chalao.Framework.Helper;
using SP1.Chalao.Framework.Objects;

namespace SP1.Chalao.Repo
{
    public class RiderRepo : BaseRepo
    {
        public Result<List<Riders>> GetAll(string key = "")
        {
            var result = new Result<List<Riders>>();
            try
            {
                var list = Context.Riders.Include("Users").ToList();

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

        public Result<Riders> GetByID(int id)
        {
            var result = new Result<Riders>();
            try
            {
                result.Data = Context.Riders.Include("Users").FirstOrDefault(d=> d.ID == id);
            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;
            }

            return result;
        }
        public Result<Riders> Save(Riders value)
        {
            var result = new Result<Riders>();

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
                objToSave1.User_TypeID = (int) EnumCollection.UserTypeEnum.Rider;

                Context.SaveChanges();

                var objToSave2 = Context.Riders.SingleOrDefault(a => a.ID == value.ID);

                if (objToSave2 == null)
                {
                    objToSave2 = new Riders();
                    Context.Riders.Add(objToSave2);
                }

                objToSave2.ID = objToSave1.ID;
                objToSave2.DOB = value.DOB;

                Context.SaveChanges();
                
                result.Data = Context.Riders.Include("Users").FirstOrDefault(d => d.ID == objToSave1.ID);

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
                var objToDelete1 = Context.Riders.FirstOrDefault(d => d.ID == id);
                var objToDelete2 = Context.Users.FirstOrDefault(d=> d.ID == id);

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

        private bool IsValidToSave(Riders obj, Result<Riders> result)
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

            if (obj.Users.Password.Length < 6)
            {
                result.HasError = true;
                result.Message = "Password should be 6 characters long";
                return false;
            }

            return true;
        }
    }
}
