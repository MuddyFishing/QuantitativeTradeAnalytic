﻿using BackTestingPlatform.Core;
using BackTestingPlatform.Model;
using BackTestingPlatform.Model.Common;
using BackTestingPlatform.Model.Stock;
using BackTestingPlatform.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WAPIWrapperCSharp;

namespace BackTestingPlatform.DataAccess
{

    public class StockTickRepository : SequentialDataRepository<StockTickFromMssql>
    {



        protected override List<StockTickFromMssql> readFromWind(string code, DateTime date)
        {
            throw new NotImplementedException();
        }

        protected override List<StockTickFromMssql> readFromDefaultMssql(string code, DateTime date)
        {
            var connName = "corp170";
            var yyyyMM = date.ToString("yyyyMM");
            var yyyyMMdd = date.ToString("yyyyMMdd");
            var codeStr = code.Replace('.', '_');
            var sql = String.Format(@"
            SELECT * FROM [WindFullMarket{0}].[dbo].[MarketData_{1}] where [tdate]={2}
            ", yyyyMM, codeStr, yyyyMMdd);
            var connStr = SqlUtils.GetConnectionString(connName);
            DataTable dt = SqlUtils.GetTable(connStr, sql);

            return dt.AsEnumerable().Select(
                row => new StockTickFromMssql
                {
                    code = (string)row["stkcd"],
                    time = Kit.ToDateTime(row["tdate"], row["ttime"]),

                    lastPrice = Kit.ToDouble(row["cp"]),
                    ask = Position.buildAsk5(row),
                    bid = Position.buildBid5(row),
                    high = Kit.ToDouble(row["hp"]),
                    low = Kit.ToDouble(row["lp"]),
                    preClose = Kit.ToDouble(row["PRECLOSE"]),
                    preSettle = Kit.ToDouble(row["PrevSettle"]),
                    volume = Kit.ToDouble(row["ts"]),
                    amount = Kit.ToDouble(row["tt"])
                }).ToList();
        }


       
    }


}
