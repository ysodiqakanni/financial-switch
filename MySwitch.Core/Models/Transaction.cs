using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.Core.Models
{
    public class Transaction : Entity
    {
        public virtual string CardPan { get; set; }
        public virtual decimal Amount { get; set; }  //exclusive of the transaction fee
        public virtual decimal TransactionFee { get; set; }  //A fee charged, by the acquirer to the issuer, for transaction activity, in the currency of the amount, transaction.
        public virtual decimal ProcessingFee { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual string MTI { get; set; }     //Message Type Identifier
        public virtual string STAN { get; set; }    //System Trace Audit Number
        public virtual string ChannelCode { get; set; }
        public virtual string OriginalDataElement { get; set; }

        public virtual string TransactionTypeCode { get; set; }
        public virtual string Account1 { get; set; }    //"from" account number (payer)
        public virtual string Account2 { get; set; }    //"To" account number (payee)
        public virtual string FeeCode { get; set; }
        public virtual string ResponseCode { get; set; }
        public virtual string ResponseDescription { get; set; }
        public virtual bool IsReversePending { get; set; }
        public virtual bool IsReversed { get; set; }
        public virtual SourceNode SourceNode { get; set; }
    }
}
