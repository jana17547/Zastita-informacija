using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here

        //Bifid
        [OperationContract]
         string CryptBifid(string source, string key);

        [OperationContract]
         string DecryptBifid(string source, string key);

        //RC6
        [OperationContract]
        byte[] CryptRC6(string source);

        [OperationContract]
        byte[] DecryptRC6(byte[] source);

        //KnapSack
        [OperationContract]
        string EncryptKS(string source);

        [OperationContract]
        string DecryptKS(string source);

        [OperationContract]
        string EncDecKS(string source, int p);

        //CTR
        [OperationContract]
        byte[] EncryptCTR(byte[] source);

        [OperationContract]
        byte[] DecryptCTR(byte[] source);

        //TigerHash
        [OperationContract]
        byte[] TH(string fInfo1);

    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "WcfServiceLibrary.ContractType".
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
