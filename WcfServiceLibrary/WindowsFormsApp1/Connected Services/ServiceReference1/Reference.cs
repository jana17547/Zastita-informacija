﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WindowsFormsApp1.ServiceReference1 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CompositeType", Namespace="http://schemas.datacontract.org/2004/07/WcfServiceLibrary")]
    [System.SerializableAttribute()]
    public partial class CompositeType : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool BoolValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StringValueField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool BoolValue {
            get {
                return this.BoolValueField;
            }
            set {
                if ((this.BoolValueField.Equals(value) != true)) {
                    this.BoolValueField = value;
                    this.RaisePropertyChanged("BoolValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StringValue {
            get {
                return this.StringValueField;
            }
            set {
                if ((object.ReferenceEquals(this.StringValueField, value) != true)) {
                    this.StringValueField = value;
                    this.RaisePropertyChanged("StringValue");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IService1")]
    public interface IService1 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetData", ReplyAction="http://tempuri.org/IService1/GetDataResponse")]
        string GetData(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetData", ReplyAction="http://tempuri.org/IService1/GetDataResponse")]
        System.Threading.Tasks.Task<string> GetDataAsync(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetDataUsingDataContract", ReplyAction="http://tempuri.org/IService1/GetDataUsingDataContractResponse")]
        WindowsFormsApp1.ServiceReference1.CompositeType GetDataUsingDataContract(WindowsFormsApp1.ServiceReference1.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetDataUsingDataContract", ReplyAction="http://tempuri.org/IService1/GetDataUsingDataContractResponse")]
        System.Threading.Tasks.Task<WindowsFormsApp1.ServiceReference1.CompositeType> GetDataUsingDataContractAsync(WindowsFormsApp1.ServiceReference1.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/CryptBifid", ReplyAction="http://tempuri.org/IService1/CryptBifidResponse")]
        string CryptBifid(string source, string key);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/CryptBifid", ReplyAction="http://tempuri.org/IService1/CryptBifidResponse")]
        System.Threading.Tasks.Task<string> CryptBifidAsync(string source, string key);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/DecryptBifid", ReplyAction="http://tempuri.org/IService1/DecryptBifidResponse")]
        string DecryptBifid(string source, string key);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/DecryptBifid", ReplyAction="http://tempuri.org/IService1/DecryptBifidResponse")]
        System.Threading.Tasks.Task<string> DecryptBifidAsync(string source, string key);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/CryptRC6", ReplyAction="http://tempuri.org/IService1/CryptRC6Response")]
        byte[] CryptRC6(string source);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/CryptRC6", ReplyAction="http://tempuri.org/IService1/CryptRC6Response")]
        System.Threading.Tasks.Task<byte[]> CryptRC6Async(string source);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/DecryptRC6", ReplyAction="http://tempuri.org/IService1/DecryptRC6Response")]
        byte[] DecryptRC6(byte[] source);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/DecryptRC6", ReplyAction="http://tempuri.org/IService1/DecryptRC6Response")]
        System.Threading.Tasks.Task<byte[]> DecryptRC6Async(byte[] source);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/EncryptKS", ReplyAction="http://tempuri.org/IService1/EncryptKSResponse")]
        string EncryptKS(string source);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/EncryptKS", ReplyAction="http://tempuri.org/IService1/EncryptKSResponse")]
        System.Threading.Tasks.Task<string> EncryptKSAsync(string source);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/DecryptKS", ReplyAction="http://tempuri.org/IService1/DecryptKSResponse")]
        string DecryptKS(string source);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/DecryptKS", ReplyAction="http://tempuri.org/IService1/DecryptKSResponse")]
        System.Threading.Tasks.Task<string> DecryptKSAsync(string source);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/EncDecKS", ReplyAction="http://tempuri.org/IService1/EncDecKSResponse")]
        string EncDecKS(string source, int p);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/EncDecKS", ReplyAction="http://tempuri.org/IService1/EncDecKSResponse")]
        System.Threading.Tasks.Task<string> EncDecKSAsync(string source, int p);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/EncryptCTR", ReplyAction="http://tempuri.org/IService1/EncryptCTRResponse")]
        byte[] EncryptCTR(byte[] source);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/EncryptCTR", ReplyAction="http://tempuri.org/IService1/EncryptCTRResponse")]
        System.Threading.Tasks.Task<byte[]> EncryptCTRAsync(byte[] source);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/DecryptCTR", ReplyAction="http://tempuri.org/IService1/DecryptCTRResponse")]
        byte[] DecryptCTR(byte[] source);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/DecryptCTR", ReplyAction="http://tempuri.org/IService1/DecryptCTRResponse")]
        System.Threading.Tasks.Task<byte[]> DecryptCTRAsync(byte[] source);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/TH", ReplyAction="http://tempuri.org/IService1/THResponse")]
        byte[] TH(string fInfo1);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/TH", ReplyAction="http://tempuri.org/IService1/THResponse")]
        System.Threading.Tasks.Task<byte[]> THAsync(string fInfo1);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : WindowsFormsApp1.ServiceReference1.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<WindowsFormsApp1.ServiceReference1.IService1>, WindowsFormsApp1.ServiceReference1.IService1 {
        
        public Service1Client() {
        }
        
        public Service1Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Service1Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetData(int value) {
            return base.Channel.GetData(value);
        }
        
        public System.Threading.Tasks.Task<string> GetDataAsync(int value) {
            return base.Channel.GetDataAsync(value);
        }
        
        public WindowsFormsApp1.ServiceReference1.CompositeType GetDataUsingDataContract(WindowsFormsApp1.ServiceReference1.CompositeType composite) {
            return base.Channel.GetDataUsingDataContract(composite);
        }
        
        public System.Threading.Tasks.Task<WindowsFormsApp1.ServiceReference1.CompositeType> GetDataUsingDataContractAsync(WindowsFormsApp1.ServiceReference1.CompositeType composite) {
            return base.Channel.GetDataUsingDataContractAsync(composite);
        }
        
        public string CryptBifid(string source, string key) {
            return base.Channel.CryptBifid(source, key);
        }
        
        public System.Threading.Tasks.Task<string> CryptBifidAsync(string source, string key) {
            return base.Channel.CryptBifidAsync(source, key);
        }
        
        public string DecryptBifid(string source, string key) {
            return base.Channel.DecryptBifid(source, key);
        }
        
        public System.Threading.Tasks.Task<string> DecryptBifidAsync(string source, string key) {
            return base.Channel.DecryptBifidAsync(source, key);
        }
        
        public byte[] CryptRC6(string source) {
            return base.Channel.CryptRC6(source);
        }
        
        public System.Threading.Tasks.Task<byte[]> CryptRC6Async(string source) {
            return base.Channel.CryptRC6Async(source);
        }
        
        public byte[] DecryptRC6(byte[] source) {
            return base.Channel.DecryptRC6(source);
        }
        
        public System.Threading.Tasks.Task<byte[]> DecryptRC6Async(byte[] source) {
            return base.Channel.DecryptRC6Async(source);
        }
        
        public string EncryptKS(string source) {
            return base.Channel.EncryptKS(source);
        }
        
        public System.Threading.Tasks.Task<string> EncryptKSAsync(string source) {
            return base.Channel.EncryptKSAsync(source);
        }
        
        public string DecryptKS(string source) {
            return base.Channel.DecryptKS(source);
        }
        
        public System.Threading.Tasks.Task<string> DecryptKSAsync(string source) {
            return base.Channel.DecryptKSAsync(source);
        }
        
        public string EncDecKS(string source, int p) {
            return base.Channel.EncDecKS(source, p);
        }
        
        public System.Threading.Tasks.Task<string> EncDecKSAsync(string source, int p) {
            return base.Channel.EncDecKSAsync(source, p);
        }
        
        public byte[] EncryptCTR(byte[] source) {
            return base.Channel.EncryptCTR(source);
        }
        
        public System.Threading.Tasks.Task<byte[]> EncryptCTRAsync(byte[] source) {
            return base.Channel.EncryptCTRAsync(source);
        }
        
        public byte[] DecryptCTR(byte[] source) {
            return base.Channel.DecryptCTR(source);
        }
        
        public System.Threading.Tasks.Task<byte[]> DecryptCTRAsync(byte[] source) {
            return base.Channel.DecryptCTRAsync(source);
        }
        
        public byte[] TH(string fInfo1) {
            return base.Channel.TH(fInfo1);
        }
        
        public System.Threading.Tasks.Task<byte[]> THAsync(string fInfo1) {
            return base.Channel.THAsync(fInfo1);
        }
    }
}
