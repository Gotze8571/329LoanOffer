using LoanOFFER.Data.BusinessLogic;
using LoanOFFER.Data.BusinessObject;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using Newtonsoft.Json;

namespace LoanOFFER.Data.DAL
{
    public class LoanOfferDb
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        // Get Sbu code using Customer Id from finacle..
        public SimbrellaLoanList GetCustomerMoreInfoWithFinacle(string CustId)
        {
            SimbrellaViewModel loanView = new SimbrellaViewModel();
            SimbrellaLoanList otherList = new SimbrellaLoanList();
        
                string com = ConfigurationManager.ConnectionStrings["FinacleCodeContext"].ConnectionString;
                //var query = $"SELECT CUST_NAME, SOL_ID, FREE_CODE_1 SBU_CODE, FREE_CODE_2 BROKER_CODE FROM TBAADM.CMG C, TBAADM.GAM G, TBAADM.FCFTT F " +
                //    $"WHERE G.CIF_ID = C.CIF_ID AND G.ACID = F.ACID AND FORACID = '" + accountNo + "'";

                var query = $"select acct_name, address_line1||' '||address_line2||' '||address_line3 address, free_code_2 BROKER_CODE, SOL_ID, free_code_1 SBU " +
                            $" From tbaadm.gam g, crmuser.accounts a, tbaadm.fcftt f where cif_id = '" + CustId + "' " +
                            $" and g.acid = f.acid " +
                            $" and g.cif_id = a.orgkey";

                using (OracleConnection con = new OracleConnection(com))
                {
                    try
                    {
                        OracleCommand command = new OracleCommand(query, con);
                        con.Open();
                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var model = new SimbrellaViewModel()
                                    {
                                        CustomerName = reader["ACCT_NAME"].ToString(),
                                        CustomerAddress1 = reader["ADDRESS"].ToString(),
                                        BrokerCode = reader["BROKER_CODE"].ToString(),
                                        BranchSolID = reader["SOL_ID"].ToString(),
                                        BranchSBU = reader["SBU"].ToString()
                                       
                                    };
                                    otherList.Add(model);
                                }
                            }
                            logger.Info(" Simbrella Loan Offer other Report data fetched from database successfully");
                            return otherList;
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex);
                    }
                }
      
            return null;
        } 
    
        // GetSimbrellaLoanWithCustomerId 
        public SimbrellaLoanList GetSimbrellaLoanWithCustId(string RequestTime, string LogDate, string CustId)
        {
            SimbrellaLoanList list = new SimbrellaLoanList();
            if ((RequestTime != null) && (LogDate != null))
            {
                var query = $"Select Id, Request, customerid, RequestTime, Response, ResponseTime, LogDate, ResponseCode from vx_FastCashEligibiltyInfo nolock where ltrim(rtrim(customerid))= '{CustId}' and RequestTime BETWEEN '{RequestTime}' AND '{LogDate}'";

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["329LoanOffer"].ToString()))
                {
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        try
                        {
                            con.Open();
                            logger.Info("Connection to Simbrella Loan Offer Report was successful!");
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader != null)
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        var model = new SimbrellaViewModel()
                                        {
                                            Id = reader["Id"].ToString(),
                                            Request = reader["Request"].ToString(),
                                            SimbrellaLoan = JsonConvert.DeserializeObject<SimbrellaLoan>(reader["Request"].ToString()),
                                            customerId = reader["customerid"].ToString(),
                                            RequestTime = reader["RequestTime"].ToString(),
                                            Response = reader["Response"].ToString(),
                                            ResponseTime = reader["ResponseTime"].ToString(),
                                            LogDate = reader["LogDate"].ToString(),
                                            ResponseCode = reader["ResponseCode"].ToString(),
                                            LoanType = "SimbrellaLoan"
                                        };
                                        list.Add(model);
                                    }
                                }
                                logger.Info(" Simbrella Loan Offer Report data fetched from database successfully");
                                return list;
                            }

                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex);

                        }
                    }
                }
            }
            return null;
        }

        public SimbrellaLoanList GetSimbrellaLoanDb(string RequestTime, string LogDate, string CustId)
        {
            SimbrellaLoanList list = new SimbrellaLoanList();
            if ((RequestTime != null) && (LogDate != null))
            {
                var query = $"Select Id, Request, customerid, RequestTime, Response, ResponseTime, LogDate, ResponseCode from vx_FastCashEligibiltyInfo nolock where RequestTime BETWEEN'" + RequestTime + "' and  '" + LogDate + "'";

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["329LoanOffer"].ToString()))
                {
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        try
                        {
                            con.Open();
                            logger.Info("Connection to Simbrella Loan Offer Report was successful!");
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader != null)
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        var model = new SimbrellaViewModel()
                                        {
                                            Id = reader["Id"].ToString(),
                                            Request = reader["Request"].ToString(),
                                            SimbrellaLoan = JsonConvert.DeserializeObject<SimbrellaLoan>(reader["Request"].ToString()),
                                            customerId = reader["customerid"].ToString(),
                                            RequestTime = reader["RequestTime"].ToString(),
                                            Response = reader["Response"].ToString(),
                                            ResponseTime = reader["ResponseTime"].ToString(),
                                            LogDate = reader["LogDate"].ToString(),
                                            ResponseCode = reader["ResponseCode"].ToString(),
                                            LoanType = "SimbrellaLoan"
                                        };
                                        list.Add(model);
                                    }
                                }
                                logger.Info(" Simbrella Loan Offer Report data fetched from database successfully");
                                return list;
                            }

                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex);

                        }
                    }
                }
            }

            else
            {
                logger.Info("Simbrella Loan Report data doesn't contain Customer Id and Request Date.");
            }

            return null;
        }
        // GetRemittaLoan from DB.
        public RemittaLoanList GetRemittaLoanDb(string RequestTime, string LogDate)
        {
            RemittaLoanList Realist = new RemittaLoanList();
            // var query = $"Select Id, Request, PhoneNumber, RequestTime, Response, LogDate FROM [MicroLendingDBNew].[dbo].[vx_RemitaEligibiltyInfo] where ltrim(rtrim(PhoneNumber))= '"+phoneNo+"' and RequestTime BETWEEN '"+RequestTime+"' AND '"+LogDate+"'";

            var query = $"Select Id, Request, PhoneNumber, RequestTime, Response, LogDate FROM [MicroLendingDB].[dbo].[vx_RemitaEligibiltyInfo] nolock where RequestTime BETWEEN '{RequestTime}' AND '{LogDate}'";
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["329LoanOffer"].ToString()))
            {
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    try
                    {
                        con.Open();
                        logger.Info("Connection to database to fetch Remitta Loan Offer Report!");
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader != null)
                        {
                            logger.Info("Reader is not null..");
                            if (reader.HasRows)
                            {
                                logger.Info("Reader is equal to true..");
                                while (reader.Read())
                                {
                                    var model = new RemittaLoanViewModel()
                                    {
                                        Id = reader["Id"].ToString(),
                                        Request = reader["Request"].ToString(),
                                        RemittaLoan = JsonConvert.DeserializeObject<RemittaLoan>(reader["Request"].ToString()),
                                        //PhoneNo = reader["PhoneNumber"].ToString(),
                                        RequestTime = reader["RequestTime"].ToString(),
                                        Response = reader["Response"].ToString(),
                                        RemittaLoanResponse = JsonConvert.DeserializeObject<RemittaLoanResponse>(reader["Response"].ToString()),
                                        LogDate = reader["LogDate"].ToString(),
                                        LoanType = "RemittaLoan"
                                    };
                                    Realist.Add(model);
                                }
                            }
                            logger.Info(" Remitta Loan Offer Report data fetched from database successfully");
                            return Realist;
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex);

                    }
                }
            }
            return null;
        }
        // GetMoreRemittaInfo using phone number
        //public List<RemittaLoanViewModel> GetMoreRemittaInfo()
        //{
        //    List<RemittaLoanViewModel> moreInfoList = new List<RemittaLoanViewModel>();
        //    // GetRemittaPhoneNumber Here.
        //    string phoneNo = "08035528076";
        //    RemittaLoanList list = new RemittaLoanList();
        //    string com = ConfigurationManager.ConnectionStrings["FinacleCodeContext"].ConnectionString;
        //    var query = $"SELECT CUST_NAME, SOL_ID, FREE_CODE_1 SBU_CODE, FREE_CODE_2 BROKER_CODE " +
        //        $"FROM TBAADM.CMG C, TBAADM.GAM G, TBAADM.FCFTT F, CRMUSER.PHONEEMAIL P WHERE G.CIF_ID = C.CIF_ID AND " +
        //        $"G.CIF_ID = P.ORGKEY AND G.ACID = F.ACID AND P.PHONENOLOCALCODE = '" + phoneNo + "' AND P.PHONEOREMAIL = 'PHONE' AND P.PREFERREDFLAG = 'Y' AND ROWNUM = 1";

        //    using (OracleConnection con = new OracleConnection(com))
        //    {
        //        try
        //        {
        //            OracleCommand command = new OracleCommand(query, con);
        //            con.Open();
        //            using (OracleDataReader reader = command.ExecuteReader())
        //            {
        //                if (reader.HasRows)
        //                {
        //                    while (reader.Read())
        //                    {
        //                        var model = new RemittaLoanViewModel()
        //                        {
        //                            CustomerName = reader["CUST_NAME"].ToString(),
        //                            BranchSolId = reader["SOL_ID"].ToString(),
        //                            BranchSbu = reader["FREE_CODE_1 SBU_CODE"].ToString(),
        //                            BrokerCode = reader["FREE_CODE_2 BROKER_CODE"].ToString()
        //                        };
        //                        list.Add(model);
        //                    }
        //                }
        //                logger.Info(" Remitta Loan Offer other Report data fetched from database successfully");
        //                return list;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            logger.Error(ex);
        //        }
        //    }
        //    return null;
        //}
        // Get SPL Loan from Db.
        public SalaryTopUpPlusList GetSalaryTopPlus(string RequestDate, string LogDate)
        {
            SalaryTopUpPlusList salaryList = new SalaryTopUpPlusList();
            var convertLogDate = DateTime.Parse(LogDate);
            var convertRequestDate = DateTime.Parse(RequestDate);

            //var query = $"Select RecID, RequestID, SourcePhone, Channel, RequestType, RequestDate, ResponseDate, Duration, ResponseCode, ResponseDescr, Remark from [USSDBanking].[dbo].[ActivityLog] where requesttype = 'SALARYPLUSTOPUPAMT' and ltrim(rtrim(SourcePhone))= '"+phoneNo+"'  AND RequestDate BETWEEN '"+RequestDate+"' AND '"+LogDate+"'";

            // var query = $"SELECT c.LoanID, c.RequestID, c.SourcePhone, c.LoanType, c.Amount, c.CustType, c.Channel, c.BrokerCode, c.RespCode, c.RespDescr, c.TranDate, p.Address_1,p.[State],p.FirstName, p.LastName,f.Account ,m.network, SUBSTRING(f.account, 1, 7) as CustomerID,CASE WHEN c.RespCode = '00' THEN 'Successful' else 'failed' end as Transaction_Status from USSDBanking.dbo.Loans c join wallet.dbo.WalletCustomer p on c.SourcePhone = p.MobilePhone join wallet.dbo.WalletCustomerAccountsMapping f on c.SourcePhone = f.MobilePhone join [Wallet].[dbo].[MobileNetworkPrefix] m on SUBSTRING(c.SourcePhone, 1, 6) = m.Prefix where LoanType = 'SPLUSTOPUP' and TranDate between '{RequestDate}​​​​​​​' and '{​​​​​​​​​​LogDate}' Order by TranDate desc";

            // var query = $"SELECT c.LoanID, c.RequestID, c.SourcePhone, c.LoanType, c.Amount, c.CustType, c.Channel, c.BrokerCode, c.RespCode, c.RespDescr, c.TranDate, p.Address_1,p.[State],p.FirstName, p.LastName,f.Account ,m.network, SUBSTRING(f.account, 1, 7) as CustomerID,CASE WHEN c.RespCode = '00' THEN 'Successful' else 'failed' end as Transaction_Status from USSDBanking.dbo.Loans c join wallet.dbo.WalletCustomer p on c.SourcePhone = p.MobilePhone join wallet.dbo.WalletCustomerAccountsMapping f on c.SourcePhone = f.MobilePhone join [Wallet].[dbo].[MobileNetworkPrefix] m on SUBSTRING(c.SourcePhone, 1, 6) = m.Prefix where LoanType = 'SPLUSTOPUP' and TranDate between '{RequestDate}' and '{LogDate}' Order by TranDate desc";
            //var query = $"SELECT c.LoanID, c.RequestID, c.SourcePhone, c.LoanType, c.Amount, c.CustType, c.Channel, c.BrokerCode, c.RespCode, c.RespDescr, c.TranDate, p.Address_1,p.[State],p.FirstName, p.LastName,f.Account ,m.network, SUBSTRING(f.account, 1, 7) as CustomerID from USSDBanking.dbo.Loans c join wallet.dbo.WalletCustomer p on c.SourcePhone = p.MobilePhone join wallet.dbo.WalletCustomerAccountsMapping f on c.SourcePhone = f.MobilePhone join [Wallet].[dbo].[MobileNetworkPrefix] m on SUBSTRING(c.SourcePhone, 1, 6) = m.Prefix where LoanType = 'SPLUSTOPUP' and TranDate between '{RequestDate}' and '{LogDate}' Order by TranDate desc";
            // var query = $"SELECT TOP 10000 c.LoanID, c.RequestID, c.SourcePhone, c.LoanType, c.Amount, c.CustType, c.Channel, c.BrokerCode, c.RespCode, c.RespDescr, c.TranDate, p.Address_1,p.[State],p.FirstName, p.LastName,f.Account ,m.network, SUBSTRING(f.account, 1, 7) as CustomerID,CASE WHEN c.RespCode = '00' THEN 'Successful' else 'failed' end as Transaction_Status, ResponseDescr as Loan_Offer from USSDBanking.dbo.Loans c join wallet.dbo.WalletCustomer p on c.SourcePhone = p.MobilePhone join wallet.dbo.WalletCustomerAccountsMapping f on c.SourcePhone = f.MobilePhone join [Wallet].[dbo].[MobileNetworkPrefix] m on SUBSTRING(c.SourcePhone, 1, 6) = m.Prefix join Activitylog_View E on c.SourcePhone = E.SourcePhone where LoanType = 'SPLUSTOPUP' and TranDate between '{RequestDate}' and '{LogDate}' Order by TranDate desc";

            var query = $"SELECT c.LoanID, c.RequestID, c.SourcePhone, c.LoanType, c.Amount, c.CustType, c.Channel, c.BrokerCode, c.RespCode, c.RespDescr, c.TranDate, p.Address_1,p.[State],p.FirstName, p.LastName,f.Account ,m.network, SUBSTRING(f.account, 1, 7) as CustomerID,CASE WHEN c.RespCode = '00' THEN 'Successful' else 'failed' end as Transaction_Status, ResponseDescr as Loan_Offer from USSDBanking.dbo.Loans (nolock) c join wallet.dbo.WalletCustomer (nolock)p on c.SourcePhone = p.MobilePhone join wallet.dbo.WalletCustomerAccountsMapping (nolock) f on c.SourcePhone = f.MobilePhone join [Wallet].[dbo].[MobileNetworkPrefix](nolock) m on SUBSTRING(c.SourcePhone, 1, 6) = m.Prefix join Activitylog_View(nolock) E on c.SourcePhone = E.SourcePhone where LoanType = 'SPLUSTOPUP' and TranDate between'{convertRequestDate}' and '{convertLogDate}' Order by TranDate desc";
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LoanOffer"].ToString()))
            {
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    try
                    {
                        con.Open();
                        logger.Info("Connection to database to fetch Salary Top-Up Plus Loan Offer Report!");
                        SqlDataReader reader = com.ExecuteReader();
                        if (reader != null)
                        {
                            logger.Info("Reader is not null..");
                            if (reader.HasRows)
                            {
                                logger.Info("Reader is equal to true..");
                                while (reader.Read())
                                {
                                    var model = new SalaryTopUpViewModel()
                                    {
                                        LoanID = int.Parse(reader["LoanID"].ToString()),
                                        RequestID = reader["RequestID"].ToString(),
                                        SourcePhone = reader["SourcePhone"].ToString(),
                                        LoanType = reader["LoanType"].ToString(),
                                        Amount = Convert.ToDecimal(reader["Amount"].ToString()),
                                        CustType = reader["CustType"].ToString(),
                                        Channel = reader["Channel"].ToString(),
                                        BrokerCode = reader["BrokerCode"].ToString(),
                                        RespCode = reader["RespCode"].ToString(),
                                        RespDescr = reader["RespDescr"].ToString(),
                                        TranDate = reader["TranDate"].ToString(),
                                        Address_1 = reader["Address_1"].ToString(),
                                        State = reader["State"].ToString(),
                                        FirstName = reader["FirstName"].ToString(),
                                        LastName = reader["LastName"].ToString(),
                                        Account = reader["Account"].ToString(),
                                        Network = reader["network"].ToString(),
                                        CustomerID = reader["CustomerID"].ToString(),
                                        Status = reader["Transaction_Status"].ToString(),
                                        Loan_Offer = reader["Loan_Offer"].ToString()

                                    };
                                    salaryList.Add(model);
                                }
                            }
                            logger.Info(" Salary Top-Up Plus Loan Offer Report data fetched from database successfully");
                            return salaryList;
                        }
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex);
                    }
                }
            }
            return null;
        }
        // Get USSD Fast Cash Loan from Db.
        public FastCashList GetFastCashLoan(string RequestDate, string LogDate)
        {
            FastCashList fastList = new FastCashList();

            var query = $"SELECT c.LoanID, c.RequestID, c.SourcePhone, c.LoanType, c.Amount, c.CustType, c.Channel, c.BrokerCode, c.RespCode, c.RespDescr, c.TranDate, p.Address_1,p.[State],p.FirstName, p.LastName,f.Account ,m.network, SUBSTRING(f.account, 1, 7) as CustomerID from USSDBanking.dbo.Loans c join wallet.dbo.WalletCustomer p on c.SourcePhone = p.MobilePhone join wallet.dbo.WalletCustomerAccountsMapping f on c.SourcePhone = f.MobilePhone join [Wallet].[dbo].[MobileNetworkPrefix] m on SUBSTRING(c.SourcePhone, 1, 6) = m.Prefix where LoanType = 'FASTCASH' and TranDate between '{RequestDate}' and '{LogDate}' Order by TranDate desc";

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LoanOffer"].ToString()))
            {
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    try
                    {
                        con.Open();
                        logger.Info("Connection to database to fetch Salary Top-Up Plus Loan Offer Report!");
                        SqlDataReader reader = com.ExecuteReader();
                        if (reader != null)
                        {
                            logger.Info("Reader is not null..");
                            if (reader.HasRows)
                            {
                                logger.Info("Reader is equal to true..");
                                while (reader.Read())
                                {
                                    var model = new FastCashLoanViewModel()
                                    {
                                        LoanID = int.Parse(reader["LoanID"].ToString()),
                                        RequestID = reader["RequestID"].ToString(),
                                        SourcePhone = reader["SourcePhone"].ToString(),
                                        LoanType = reader["LoanType"].ToString(),
                                        Amount = Convert.ToDecimal(reader["Amount"].ToString()),
                                        CustType = reader["CustType"].ToString(),
                                        Channel = reader["Channel"].ToString(),
                                        BrokerCode = reader["BrokerCode"].ToString(),
                                        RespCode = reader["RespCode"].ToString(),
                                        RespDescr = reader["RespDescr"].ToString(),
                                        TranDate = reader["TranDate"].ToString(),
                                        Address_1 = reader["Address_1"].ToString(),
                                        State = reader["State"].ToString(),
                                        FirstName = reader["FirstName"].ToString(),
                                        LastName = reader["LastName"].ToString(),
                                        Account = reader["Account"].ToString(),
                                        Network = reader["network"].ToString(),
                                        CustomerID = reader["CustomerID"].ToString()

                                    };
                                    fastList.Add(model);
                                }
                            }
                            logger.Info(" Fast Cash Loan Offer Report data fetched from database successfully");
                            return fastList;
                        }
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex);
                    }
                }
            }
            return null;
        }
        // GetFastCashDisbursedLoan from Db.
        public FastCashDisbursedList GetFastCashDisbursed(string RequestDate, string LogDate)
        {
            return null;
        }
        // GetFastCashEligiblity for Loan from Db.
        public FastCashEligibleList GetFastCashEligibleList(string RequestDate, string LogDate)
        {
            FastCashEligibleList fastEligible = new FastCashEligibleList();

            var query = $"select RecID,RequestID,Carrier,SourcePhone,Channel,RequestType,RequestDate,ResponseDate,Duration,ResponseCode,ResponseDescr,LogDate,Remark,AgentCode FROM [USSDBanking].[dbo].[ActivityLog] where RequestType = 'FCACCTELIGIbility' and LogDate between '{RequestDate}' and '{LogDate}' Order by TranDate desc";

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LoanOffer"].ToString()))
            {
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    try
                    {
                        con.Open();
                        logger.Info("Connection to database to fetch Fast Cash Eligiblity Loan Offer Report!");
                        SqlDataReader reader = com.ExecuteReader();
                        if (reader != null)
                        {
                            logger.Info("Reader is not null..");
                            if (reader.HasRows)
                            {
                                logger.Info("Reader is equal to true..");
                                while (reader.Read())
                                {
                                    var model = new FastCashEligibleViewModel()
                                    {
                                        RecID = int.Parse(reader["RecID"].ToString()),
                                        RequestID = reader["RequestID"].ToString(),
                                        Carrier = reader["Carrier"].ToString(),
                                        SourcePhone = reader["SourcePhone"].ToString(),
                                        Channel = reader["Channel"].ToString(),
                                        RequestType = reader["RequestType"].ToString(),
                                        RequestDate = reader["RequestDate"].ToString(),
                                        Duration = reader["Duration"].ToString(),
                                        ResponseCode = reader["ResponseCode"].ToString(),
                                        ResponseDescr = reader["ResponseDescr"].ToString(),
                                        LogDate = reader["LogDate"].ToString(),
                                        Remark = reader["Remark"].ToString(),
                                    };
                                    fastEligible.Add(model);
                                }
                            }
                            logger.Info(" Fast Cash Eligibility Loan Offer Report data fetched from database successfully");
                            return fastEligible;
                        }
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex);
                    }
                }
            }
            return null;
        }
    }
}
