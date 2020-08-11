using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Data_tier
{
    [ServiceContract]
    public interface IDataTier
    {
        [OperationContract(IsOneWay = true)]
        void AddUser(string username, string password);

        [OperationContract]
        bool LoginUser(string username, string password);

        [OperationContract]
        List<string> GetUsers();

        [OperationContract]
        List<List<string>> GetComments();

        [OperationContract]
        void CreateComment(string username, string comment);

        //method to filter comments
        [OperationContract]
        List<List<string>> filterComments(string username);
    }
}
