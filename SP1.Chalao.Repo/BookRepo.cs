using System;
using System.Collections.Generic;
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
    }
}
