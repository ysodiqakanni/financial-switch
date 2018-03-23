using FluentNHibernate.Mapping;
using MySwitch.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.Core.Maps
{
    class TransactionLogMap : ClassMap<Transaction>
    {
        public TransactionLogMap()
        {
            Id(x => x.ID);
            Map(x => x.CardPan);
            Map(x => x.MTI);
            Map(x => x.Amount);
            Map(x => x.TransactionFee);
            Map(x => x.ProcessingFee);
            Map(x => x.Date);
            Map(x => x.STAN);
            Map(x => x.TransactionTypeCode);
            Map(x => x.ChannelCode);
            Map(x => x.OriginalDataElement);
            Map(x => x.Account1);
            Map(x => x.Account2);
            Map(x => x.FeeCode);
            Map(x => x.ResponseCode);
            Map(x => x.ResponseDescription);
            Map(x => x.IsReversed);
            Map(x => x.IsReversePending);

            References(x => x.SourceNode).LazyLoad();
            Table("TransactionLogs");
        }
    }
}
