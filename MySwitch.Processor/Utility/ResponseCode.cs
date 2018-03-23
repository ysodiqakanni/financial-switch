using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.Processor.Utility
{
    public class ResponseCode
    {
        public const string ERROR = "06";
        public const string INVALID_TRANSACTION = "12";
        public const int INVALID_AMOUNT = 13;
        public const int INVALID_RESPONSE = 20;
        public const string UNACCEPTABLE_TRANSACTION_FEE = "23";
        public const string UNABLE_TO_LOCATE_RECORD = "25";
        public const string ISSUER_OR_SWITCH_INOPERATIVE = "91";
        public const int ROUTING_ERROR = 92;
        public const int DUPLICATE_TRANSACTION = 94;
        public const int EXPIRED_CARD = 54;
        public const int TRANSACTION_NOT_PERMITTED_ON_TERMINAL = 58;
        public const string RESPONSE_RECEIVED_TOO_LATE = "68";
    }
}
