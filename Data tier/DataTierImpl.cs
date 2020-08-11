using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Data_tier
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false, IncludeExceptionDetailInFaults = true)]
    public class DataTierImpl : IDataTier
    {
        //static object to refer the dll
        public static ExamLib.CommentDatabase iCommentDB = new ExamLib.CommentDatabase();

        //add user method
        public void AddUser(string username, string password)
        {
            iCommentDB.AddUser(username, password);
        }

        //login user method
        public bool LoginUser(string username, string password)
        {
            //exception handling
            try
            {
                return iCommentDB.LoginUser(username, password);
            }
            catch(Exception e)
            {
                return false;
            }
        }

        //get users method
        public List<string> GetUsers()
        {
            return iCommentDB.GetUsers();
        }

        //getcomments method
        public List<List<string>> GetComments()
        {
            return iCommentDB.GetComments();
        }

        //create comment method
        public void CreateComment(string username, string comment)
        {
            iCommentDB.CreateComment(username, comment);
        }

        //Asynchronous method to filter comments by username
        public List<List<string>> filterComments(string username)
        {
            return asyncFilter(username).Result;
        }

        public async Task<List<List<string>>> asyncFilter(string username)
        {
            List<List<string>> List2 = await taskFilter(username);

            return List2;
        }

        public Task<List<List<string>>> taskFilter(string username)
        {
            List<List<string>> filteredList = new List<List<string>>();

            foreach (List<string> comment in iCommentDB.GetComments())
            {
                foreach (string un in comment)
                {
                    if (un == username)
                    {
                        filteredList.Add(comment);
                    }
                }
            }
            return Task.FromResult<List<List<string>>>(filteredList);
        }
    }
}
