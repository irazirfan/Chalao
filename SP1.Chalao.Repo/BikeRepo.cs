using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SP1.Chalao.Entities;
using SP1.Chalao.Framework.Constants;
using SP1.Chalao.Framework.Helper;
using SP1.Chalao.Framework.Objects;

namespace SP1.Chalao.Repo
{
    public class BikeRepo : BaseRepo
    {
        public Result<List<Bike_Details>> GetAll(string key = "")
        {
            var result = new Result<List<Bike_Details>>();
            try
            {
                var list = Context.BikeDetails.ToList();

                if (ValidationHelper.IsValidString(key))
                    list = list.Where(a => a.Bike_ID.ToLower().Contains(key.ToLower())).ToList();

                result.Data = list;

            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;
            }

            return result;
        }

        public Result<Bike_Details> Save(Bike_Details value)
        {
            var result = new Result<Bike_Details>();

            try
            {
                var objToSave = Context.BikeDetails.SingleOrDefault(a => a.ID == value.ID);

                if (objToSave == null)
                {
                    objToSave = new Bike_Details();
                    Context.BikeDetails.Add(objToSave);
                }

                if (!IsValidToSave(value, result))
                    return result;

                objToSave.Bike_ID = value.Bike_ID;
                objToSave.Status = value.Status;
                objToSave.Details = value.Details;

                Context.SaveChanges();

                result.Data = Context.BikeDetails.FirstOrDefault(d => d.ID == objToSave.ID);

            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;
            }

            return result;
        }

        public Result<Bike_Details> GetByID(int id)
        {
            var result = new Result<Bike_Details>();
            try
            {
                result.Data = Context.BikeDetails.FirstOrDefault(b => b.ID == id);
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
                var objToDelete = Context.BikeDetails.FirstOrDefault(d => d.ID == id);

                if (objToDelete == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid Bike ID";
                    return result;
                }

                Context.BikeDetails.Remove(objToDelete);
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

        private bool IsValidToSave(Bike_Details obj, Result<Bike_Details> result)
        {
            if (!ValidationHelper.IsValidString(obj.Bike_ID))
            {
                result.HasError = true;
                result.Message = "Invalid Bike ID";
                return false;
            }

            if (Context.BikeDetails.Any(ui => ui.Bike_ID == obj.Bike_ID && ui.ID != obj.ID))
            {
                result.HasError = true;
                result.Message = "Bike already exists";
                return false;
            }

            return true;
        }
    }
}
