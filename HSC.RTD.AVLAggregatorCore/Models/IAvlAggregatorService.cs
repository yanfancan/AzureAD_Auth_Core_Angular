﻿using System.ServiceModel;
using System.Threading.Tasks;

namespace HSC.RTD.AVLAggregatorCore.Models
{
    [ServiceContract(Namespace = "http://mohltc.on.ca/xmlns/avl", ConfigurationName = "HSC.RTD.AVLAggregator.IAvlAggregatorService")]
    public interface IAvlAggregatorService
    {
        [OperationContract]
        string LastPosition(string MessageXml);
        [OperationContract]
        string Login(string MessageXml);
        [OperationContract]
        string Logout(string MessageXml);

        [OperationContract]
        string ExportData(string MessageXml);
    }
}
