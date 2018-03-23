using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.Processor.Utility
{
    public static class MessageField
    {
        public const int RESPONSE_FIELD = 39;
        public const int ORIGINAL_DATA_ELEMENT_FIELD = 90;
        public const int TRANSACTION_TYPE_FIELD = 3;
        public const int CHANNEL_ID_FIELD = 41;
        public const int CARD_PAN_FIELD = 2;        //Primary Account Number
        public const int STAN_FIELD = 11;
        public const int AMOUNT_FIELD = 4;            
        public const int EXPIRY_DATE_FIELD = 14;
        public const int TRANSACTION_FEE_FIELD = 28;
        public const int PROCESSING_FEE_FIELD = 29;
        public const int FROM_ACCOUNT_ID_FIELD = 102;
        public const int TO_ACCOUNT_ID_FIELD = 103;
    }
}
