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
      
        public SimbrellaLoanList GetSimbrellaLoanDb(string RequestTime, string LogDate, string CustId)
        {
            SimbrellaLoanList list = new SimbrellaLoanList();
            if ((RequestTime != null) && (LogDate != null))
            {
                var query = $"Select Id, Request, customerid, RequestTime, Response, ResponseTime, LogDate, ResponseCode from vx_FastCashEligibiltyInfo where RequestTime BETWEEN'"+RequestTime+"' and  '"+LogDate+"'";

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

                            //con.Close();
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex);
                            ErrorLoan err = new ErrorLoan()
                            {
                                ErrorName = "Simbrella Report caught an Exception!!",
                                ErrorDate = DateTime.Now
                            };
                        }
                    }
                }
            }
            else if (CustId != null)
            {
                var query = $"Select Id, Request, customerid, RequestTime, Response, ResponseTime, LogDate, ResponseCode from vx_FastCashEligibiltyInfo where ltrim(rtrim(customerid))= '{CustId}' and RequestTime BETWEEN '{RequestTime}' AND '{LogDate}'";

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

                            //con.Close();
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex);
                            ErrorLoan err = new ErrorLoan()
                            {
                                ErrorName = "Simbrella Report caught an Exception!!",
                                ErrorDate = DateTime.Now
                            };
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
       
        public RemittaLoanList GetRemittaLoanDb(string RequestTime, string LogDate)
        {
            RemittaLoanList Realist = new RemittaLoanList();
            // var query = $"Select Id, Request, PhoneNumber, RequestTime, Response, LogDate FROM [MicroLendingDBNew].[dbo].[vx_RemitaEligibiltyInfo] where ltrim(rtrim(PhoneNumber))= '"+phoneNo+"' and RequestTime BETWEEN '"+RequestTime+"' AND '"+LogDate+"'";

            var query = $"Select Id, Request, PhoneNumber, RequestTime, Response, LogDate FROM [MicroLendingDBNew].[dbo].[vx_RemitaEligibiltyInfo] where RequestTime BETWEEN '{RequestTime}' AND '{LogDate}'";
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
                                        PhoneNo = reader["PhoneNumber"].ToString(),
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
        public List<RemittaLoanViewModel> GetMoreRemittaInfo()
        {
            List<RemittaLoanViewModel> moreInfoList = new List<RemittaLoanViewModel>();
            // GetRemittaPhoneNumber Here.
            string phoneNo = "08035528076";
            RemittaLoanList list = new RemittaLoanList();
            string com = ConfigurationManager.ConnectionStrings["FinacleCodeContext"].ConnectionString;
            var query = $"SELECT CUST_NAME, SOL_ID, FREE_CODE_1 SBU_CODE, FREE_CODE_2 BROKER_CODE " +
                $"FROM TBAADM.CMG C, TBAADM.GAM G, TBAADM.FCFTT F, CRMUSER.PHONEEMAIL P WHERE G.CIF_ID = C.CIF_ID AND " +
                $"G.CIF_ID = P.ORGKEY AND G.ACID = F.ACID AND P.PHONENOLOCALCODE = '"+phoneNo+"' AND P.PHONEOREMAIL = 'PHONE' AND P.PREFERREDFLAG = 'Y' AND ROWNUM = 1";

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
                                var model = new RemittaLoanViewModel()
                                {
                                    CustomerName = reader["CUST_NAME"].ToString(),
                                    BranchSolId = reader["SOL_ID"].ToString(),
                                    BranchSbu = reader["FREE_CODE_1 SBU_CODE"].ToString(),
                                    BrokerCode = reader["FREE_CODE_2 BROKER_CODE"].ToString()
                                };
                                list.Add(model);
                            }
                        }
                        logger.Info(" Remitta Loan Offer other Report data fetched from database successfully");
                        return list;
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }
            }
            return null;
        }
        public SalaryTopUpPlusList GetSalaryTopPlus(string RequestDate, string LogDate)
        {
            SalaryTopUpPlusList salaryList = new SalaryTopUpPlusList();

            //var query = $"Select RecID, RequestID, SourcePhone, Channel, RequestType, RequestDate, ResponseDate, Duration, ResponseCode, ResponseDescr, Remark from [USSDBanking].[dbo].[ActivityLog] where requesttype = 'SALARYPLUSTOPUPAMT' and ltrim(rtrim(SourcePhone))= '"+phoneNo+"'  AND RequestDate BETWEEN '"+RequestDate+"' AND '"+LogDate+"'";

            //var query = $"Select c.*,p.Address_1,p.[State],p.FirstName,p.LastName,f.Account ,m.network, SUBSTRING(f.account,1,7) as CustomerID from USSDBanking.dbo.Loans c join wallet.dbo.WalletCustomer p on c.SourcePhone = p.MobilePhone join wallet.dbo.WalletCustomerAccountsMapping f on c.SourcePhone = f.MobilePhone join [Wallet].[dbo].[MobileNetworkPrefix] m on SUBSTRING(c.SourcePhone, 1, 6) = m.Prefix where LoanType = 'SPLUSTOPUP' and TranDate between '{RequestDate}' and '{LogDate}' Order by TranDate desc";

            var query = $"SELECT c.LoanID, c.RequestID, c.SourcePhone, c.LoanType, c.Amount, c.CustType, c.Channel, c.BrokerCode, c.RespCode, c.RespDescr, c.TranDate, p.Address_1,p.[State],p.FirstName, p.LastName,f.Account ,m.network, SUBSTRING(f.account, 1, 7) as CustomerID from USSDBanking.dbo.Loans c join wallet.dbo.WalletCustomer p on c.SourcePhone = p.MobilePhone join wallet.dbo.WalletCustomerAccountsMapping f on c.SourcePhone = f.MobilePhone join [Wallet].[dbo].[MobileNetworkPrefix] m on SUBSTRING(c.SourcePhone, 1, 6) = m.Prefix where LoanType = 'SPLUSTOPUP' and TranDate between '{RequestDate}' and '{LogDate}' Order by TranDate desc";

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LoanOffer"].ToString()))
            {
                using(SqlCommand com = new SqlCommand(query, con))
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
                                        CustomerID = reader["CustomerID"].ToString()
                                        
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
       
        // Get Account number from Simbrella Loan Offer.
        public string GetSimbrellaCustomerNameWithCustomerID()
        {
            var query = $"Select Id, Request, customerid, RequestTime, Response, ResponseTime, LogDate, ResponseCode from vx_FastCashEligibiltyInfo";
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LoanOffer"].ToString()))
            {
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    try
                    {
                        con.Open();
                        logger.Info("Connection to database to fetch Simbrella Loan Offer Report!");
                        SqlDataReader reader = com.ExecuteReader();
                        if (reader != null)
                        {
                            logger.Info("Reader is not null..");
                            if (reader.HasRows)
                            {
                                logger.Info("Reader is equal to true..");
                                while (reader.Read())
                                {
                                    var model = new SimbrellaViewModel()
                                    {
                                        Request = reader["Request"].ToString()
                                    };
                                    return model.Request.ToString();
                                }
                            }
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
        public SimbrellaLoanList GetMoreInfo(string accountNo)
        {
            accountNo = GetSimbrellaCustomerNameWithCustomerID();
            SimbrellaLoanList otherList = new SimbrellaLoanList();
            string com = ConfigurationManager.ConnectionStrings["FinacleCodeContext"].ConnectionString;
            var query = $"SELECT CUST_NAME, SOL_ID, FREE_CODE_1 SBU_CODE, FREE_CODE_2 BROKER_CODE FROM TBAADM.CMG C, TBAADM.GAM G, TBAADM.FCFTT F " +
                $"WHERE G.CIF_ID = C.CIF_ID AND G.ACID = F.ACID AND FORACID = '"+accountNo+"'";

            using (OracleConnection con = new OracleConnection(com))
            {
                try
                {
                    OracleCommand command = new OracleCommand(query, con);
                    con.Open();
                    using(OracleDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var model = new SimbrellaViewModel()
                                {
                                    CustomerName = reader["CUST_NAME"].ToString(),
                                    BranchSolID = reader["SOL_ID"].ToString(),
                                    BranchSBU = reader["FREE_CODE_1 SBU_CODE"].ToString(),
                                    BrokerCode = reader["FREE_CODE_2 BROKER_CODE"].ToString()
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
    }
}
