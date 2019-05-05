using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SP1.Chalao.Entities;
using SP1.Chalao.Framework.Helper;
using SP1.Chalao.Framework.Objects;

namespace SP1.Chalao.Repo
{
    public class BookRepo : BaseRepo
    {
        public Result<List<Book_Info>> GetAll(string key = "")
        {
            var result = new Result<List<Book_Info>>();
            try
            {
                var list = Context.BookInfos.ToList();

                if (ValidationHelper.IsValidString(key))
                    list = list.Where(a => a.Rider_Email.ToLower().Contains(key.ToLower())).ToList();

                result.Data = list;

            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;
            }

            return result;
        }

        public Result<Book_Info> Save(Book_Info value)
        {
            var result = new Result<Book_Info>();

            try
            {
                var objToSave = Context.BookInfos.SingleOrDefault(a => a.ID == value.ID);

                if (objToSave == null)
                {
                    objToSave = new Book_Info();
                    Context.BookInfos.Add(objToSave);
                }


                if (!IsValidToSave(value, result))
                    return result;

                objToSave.Bike_ID = value.Bike_ID;
                objToSave.Rider_Name = value.Rider_Name;
                objToSave.Rider_Email = value.Rider_Email;
                objToSave.Book_Schedule = DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "-" + DateTime.Now.Hour + ":" + DateTime.Now.Minute + DateTime.Now.Second + "";


                Context.SaveChanges();

                var objToSave2 = Context.BikeDetails.SingleOrDefault(b => b.Bike_ID == value.Bike_ID);

                if (objToSave2 == null)
                {
                    objToSave2 = new Bike_Details();
                    Context.BikeDetails.Add(objToSave2);
                }

                objToSave2.Bike_ID = objToSave.Bike_ID;
                objToSave2.Status = 1;

                Context.SaveChanges();

                result.Data = Context.BookInfos.FirstOrDefault(b => b.ID == objToSave.ID);

            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;
            }

            return result;
        }

        public Result<Book_Info> GetByID(int id)
        {
            var result = new Result<Book_Info>();
            try
            {
                result.Data = Context.BookInfos.FirstOrDefault(b => b.ID == id);
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
                var objToDelete = Context.BookInfos.FirstOrDefault(d => d.ID == id);

                if (objToDelete == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid Bike ID";
                    return result;
                }

                Context.BookInfos.Remove(objToDelete);
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

        private bool IsValidToSave(Book_Info obj, Result<Book_Info> result)
        {
            if (!ValidationHelper.IsValidString(obj.Bike_ID))
            {
                result.HasError = true;
                result.Message = "Invalid Name";
                return false;
            }

            if (Context.BikeDetails.All(ui => ui.Bike_ID == obj.Bike_ID && ui.Status == 1))
            {
                result.HasError = true;
                result.Message = "Bike already booked";
                return false;
            }

            return true;
        }
    }
}
