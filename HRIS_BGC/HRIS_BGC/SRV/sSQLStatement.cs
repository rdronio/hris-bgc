using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SRV
{
    public class sSQLStatement
    {
        Common objCommon = new Common();
        //TIME RECORDS
        //Time Records with all employee name biometrics and total hours of 8 hours only, tardiness(min/s)
        public string timerecords(string sql, string from_date, string end_Date, string department_id, string position_id)
        {
            sql = "SELECT C.fullName, C.bio_number, C.DATE, C.TIMEIN, C.TIMEOUT, ";
            sql += "CASE WHEN C.TOTAL >= 5 THEN (C.TOTAL - C.OVERTIME - 1) ELSE (C.TOTAL - C.OVERTIME) END AS TOTAL, ";
            sql += "C.OVERTIME, CASE WHEN C.HALFDAY = '300' THEN (C.TARDINESS - C.HALFDAY) WHEN C.TARDINESS < 0 THEN '0' ELSE C.TARDINESS END AS TARDINESS, ";
            sql += "CASE WHEN C.HALFDAY = '300' THEN cast(round((C.TARDINESS - C.HALFDAY) / 60,2) as numeric (36,2)) WHEN C.TARDINESS < 0 THEN '0' ELSE cast(round(C.TARDINESS / 60,2) as numeric (36,2)) END AS TARDINESSHOUR, C.HALFDAY FROM (SELECT convert(varchar, B.Timelogs_date, 107) AS DATE, ";
            sql += "CONVERT(varchar(15), CAST(B.TIMEIN AS TIME),100) AS TIMEIN, "; 
            sql += "CONVERT(varchar(15),CAST(B.TIMEOUT AS TIME),100) AS TIMEOUT, ";
            sql += "CAST(ROUND(DATEDIFF(MINUTE, B.TIMEIN, B.TIMEOUT)/60.0, 2) AS Numeric(36, 2)) AS TOTAL, ";
            sql += "CASE WHEN B.TIMEOUT > '17:00:00' THEN CAST(ROUND(DATEDIFF(MINUTE, '17:00:00', B.TIMEOUT)/60.0, 2) AS Numeric(36, 2)) else '0.00' END AS OVERTIME, ";
            sql += "CAST(ROUND(DATEDIFF(MINUTE, '8:00:00', B.TIMEIN), 2) AS Numeric(36, 2)) AS TARDINESS, ";
            sql += "CASE WHEN B.TIMEIN >= '13:00:00' THEN '300' ELSE '0' END AS HALFDAY, B.bio_number, B.fullName ";
            sql += "FROM (select (UPPER(LEFT(cast(D.Emp_lname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(D.Emp_lname as nvarchar(max)),2,LEN(cast(D.Emp_lname as nvarchar(max))))) + ', ' + UPPER(LEFT(cast(D.Emp_fname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(D.Emp_fname as nvarchar(max)),2,LEN(cast(D.Emp_fname as nvarchar(max)))))+ ' ' + UPPER(LEFT(cast(D.Emp_mname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(D.Emp_mname as nvarchar(max)),2,LEN(cast(D.Emp_mname as nvarchar(max)))))) AS FullName, A.bio_number, A.Timelogs_date, MIN(A.Timelogs_time) AS TIMEIN, CASE WHEN COUNT(A.Timelogs_time) > 1 THEN MAX(A.Timelogs_time) ELSE NULL END AS TIMEOUT from db_owner.Timelogs A left join db_owner.Employee D on A.bio_number = D.bio_number ";

            if (department_id == "")
            {
                
            }
            else if (department_id == "0")
            {
                
            }
            else
            {
                sql += "WHERE department_id=" + department_id;
            }

            sql += " GROUP BY A.bio_number, A.Timelogs_date, D.Emp_fname, D.Emp_lname, D.Emp_mname) B) C";
            return sql;
        }

        //Time Records in current year
        public string timerecordsTardiness(string sql)
        {
            sql = "SELECT C.fullName, C.bio_number, C.DATE, C.TIMEIN, C.TIMEOUT, ";
            sql += "CASE WHEN C.TOTAL >= 5 THEN (C.TOTAL - C.OVERTIME - 1) ELSE (C.TOTAL - C.OVERTIME) END AS TOTAL, ";
            sql += "C.OVERTIME, CASE WHEN C.HALFDAY = '300' THEN (C.TARDINESS - C.HALFDAY) WHEN C.TARDINESS < 0 THEN '0' ELSE C.TARDINESS END AS TARDINESS, ";
            sql += "CASE WHEN C.HALFDAY = '300' THEN cast(round((C.TARDINESS - C.HALFDAY) / 60,2) as numeric (36,2)) WHEN C.TARDINESS < 0 THEN '0' ELSE cast(round(C.TARDINESS / 60,2) as numeric (36,2)) END AS TARDINESSHOUR, C.HALFDAY FROM (SELECT convert(varchar, B.Timelogs_date, 107) AS DATE, ";
            sql += "CONVERT(varchar(15), CAST(B.TIMEIN AS TIME),100) AS TIMEIN, ";
            sql += "CONVERT(varchar(15),CAST(B.TIMEOUT AS TIME),100) AS TIMEOUT, ";
            sql += "CAST(ROUND(DATEDIFF(MINUTE, B.TIMEIN, B.TIMEOUT)/60.0, 2) AS Numeric(36, 2)) AS TOTAL, ";
            sql += "CASE WHEN B.TIMEOUT > '17:00:00' THEN CAST(ROUND(DATEDIFF(MINUTE, '17:00:00', B.TIMEOUT)/60.0, 2) AS Numeric(36, 2)) else '0.00' END AS OVERTIME, ";
            sql += "CAST(ROUND(DATEDIFF(MINUTE, '8:00:00', B.TIMEIN), 2) AS Numeric(36, 2)) AS TARDINESS, ";
            sql += "CASE WHEN B.TIMEIN >= '13:00:00' THEN '300' ELSE '0' END AS HALFDAY, B.bio_number, B.fullName ";
            sql += "FROM (select (UPPER(LEFT(cast(D.Emp_lname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(D.Emp_lname as nvarchar(max)),2,LEN(cast(D.Emp_lname as nvarchar(max))))) + ', ' + UPPER(LEFT(cast(D.Emp_fname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(D.Emp_fname as nvarchar(max)),2,LEN(cast(D.Emp_fname as nvarchar(max)))))+ ' ' + UPPER(LEFT(cast(D.Emp_mname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(D.Emp_mname as nvarchar(max)),2,LEN(cast(D.Emp_mname as nvarchar(max)))))) AS FullName, ";
            sql += "A.bio_number, A.Timelogs_date, MIN(A.Timelogs_time) AS TIMEIN, CASE WHEN COUNT(A.Timelogs_time) > 1 THEN MAX(A.Timelogs_time) ELSE NULL END AS TIMEOUT from db_owner.Timelogs A left join db_owner.Employee D on A.bio_number = D.bio_number ";
            sql += "WHERE DATENAME(YEAR, timelogs_date) = DATENAME(YEAR, '" + objCommon.pacificdate + "')  GROUP BY A.bio_number, A.Timelogs_date, D.Emp_fname, D.Emp_lname, D.Emp_mname) B) C";
            return sql;
        }

        public string pendingtimealteration(string sql)
        {
            sql = "Select *, convert(varchar, timealteration_date, 107) DATE from db_owner.TimeAlteration where timealteration_status = 0";
            return sql;

        }
        //time records without employee name
        public string timeRecordsNoEmp(string sql)
        {   
            //8:00AM - 5:00PM --- Standard Time
            //Tadiness starts at 8:30AM

            sql = "SELECT C.bio_number, C.DATE, C.TIMEIN, C.TIMEOUT, ";
            sql += "(C.TOTAL - C.OVERTIME) AS TOTAL, C.OVERTIME, ";
            sql += "CASE WHEN C.HALFDAY = '300' THEN (C.TARDINESS - C.HALFDAY) WHEN C.TARDINESS < 0 THEN '0' ELSE C.TARDINESS ";
            sql += "END AS TARDINESS, C.HALFDAY FROM ";
            sql += "(SELECT convert(varchar, B.Timelogs_date, 107) AS DATE, CONVERT(varchar(15), CAST(B.TIMEIN AS TIME),100) AS TIMEIN, ";
            sql += "CONVERT(varchar(15),CAST(B.TIMEOUT AS TIME),100) AS TIMEOUT, ";
            sql += "CAST(ROUND(DATEDIFF(MINUTE, B.TIMEIN, B.TIMEOUT)/60.0, 2) AS Numeric(36, 2)) AS TOTAL, ";
            sql += "CASE WHEN B.TIMEOUT > '17:00:00' THEN CAST(ROUND(DATEDIFF(MINUTE, '17:00:00', B.TIMEOUT)/60.0, 2) AS Numeric(36, 2)) else '0.00' END AS OVERTIME, ";
            sql += "CAST(ROUND(DATEDIFF(MINUTE, '8:30:00', B.TIMEIN), 2) AS Numeric(36, 2)) AS TARDINESS, ";
            sql += "CASE WHEN B.TIMEIN >= '13:00:00' THEN '300' ELSE '0' END AS HALFDAY, B.bio_number FROM ";
            sql += "(select A.bio_number, A.Timelogs_date, MIN(A.Timelogs_time) AS TIMEIN, ";
            sql += "CASE WHEN COUNT(A.Timelogs_time) > 1 THEN MAX(A.Timelogs_time) ELSE NULL END AS TIMEOUT ";
            sql += "from db_owner.Timelogs A GROUP BY bio_number, Timelogs_date) B) C";
            return sql;

        }

        //Query with TimelogsID TimeIN and TimelogsID Timeout if Generate DTR Report remove those id's
        //string sSQLStatement = "SELECT C.Emp_ID, C.DATE, C.TimelogsID_TimeIN, C.TIMEIN, C.TimelogsID_TimeOut, C.TIMEOUT, (C.TOTAL - C.OVERTIME) AS TOTAL, C.OVERTIME, CASE WHEN C.HALFDAY = '300' THEN (C.TARDINESS - C.HALFDAY) ELSE C.TARDINESS END AS TARDINESS, C.HALFDAY FROM (SELECT B.TimelogsID_TimeIN, B.TimelogsID_TimeOut, B.Emp_ID,convert(varchar, B.Timelogs_date, 107) AS DATE, CONVERT(varchar(15), CAST(B.TIMEIN AS TIME),100) AS TIMEIN, CONVERT(varchar(15),CAST(B.TIMEOUT AS TIME),100) AS TIMEOUT, CAST(ROUND(DATEDIFF(MINUTE, B.TIMEIN, B.TIMEOUT)/60.0, 2) AS Numeric(36, 2)) AS TOTAL, CASE WHEN B.TIMEOUT > '17:00:00' THEN CAST(ROUND(DATEDIFF(MINUTE, '17:00:00', B.TIMEOUT)/60.0, 2) AS Numeric(36, 2)) else '0.00' END AS OVERTIME, CAST(ROUND(DATEDIFF(MINUTE, '8:30:00', B.TIMEIN), 2) AS Numeric(36, 2)) AS TARDINESS, CASE WHEN B.TIMEIN >= '13:00:00' THEN '300' ELSE '0' END AS HALFDAY FROM (select (select Timelogs_ID from db_owner.Timelogs where Emp_ID = "+ empid +" AND Timelogs_time = (Select MIN(Timelogs_time) from db_owner.Timelogs)) AS TimelogsID_TimeIN, (select Timelogs_ID from db_owner.Timelogs where Emp_ID = "+ empid +" AND Timelogs_time = (Select MAX(Timelogs_time) from db_owner.Timelogs)) AS TimelogsID_TimeOut, Emp_ID, Timelogs_date, MIN(Timelogs_time) AS TIMEIN, CASE WHEN COUNT(Timelogs_time) > 1 THEN MAX(Timelogs_time) ELSE NULL END AS TIMEOUT from db_owner.Timelogs A WHERE Emp_ID = "+ empid +" GROUP BY EMP_iD, Timelogs_date) B) C";
        //string sSQLStatement = "SELECT C.Emp_ID, C.DATE, C.TimelogsID_TimeIN, C.TIMEIN, C.TimelogsID_TimeOut, C.TIMEOUT, (C.TOTAL - C.OVERTIME) AS TOTAL, C.OVERTIME, CASE WHEN C.HALFDAY = '300' THEN (C.TARDINESS - C.HALFDAY) WHEN C.TARDINESS < 0 THEN '0' ELSE C.TARDINESS END AS TARDINESS, C.HALFDAY FROM (SELECT B.TimelogsID_TimeIN, B.TimelogsID_TimeOut, B.Emp_ID,convert(varchar, B.Timelogs_date, 107) AS DATE, CONVERT(varchar(15), CAST(B.TIMEIN AS TIME),100) AS TIMEIN, CONVERT(varchar(15),CAST(B.TIMEOUT AS TIME),100) AS TIMEOUT, CAST(ROUND(DATEDIFF(MINUTE, B.TIMEIN, B.TIMEOUT)/60.0, 2) AS Numeric(36, 2)) AS TOTAL, CASE WHEN B.TIMEOUT > '17:00:00' THEN CAST(ROUND(DATEDIFF(MINUTE, '17:00:00', B.TIMEOUT)/60.0, 2) AS Numeric(36, 2)) else '0.00' END AS OVERTIME, CAST(ROUND(DATEDIFF(MINUTE, '8:30:00', B.TIMEIN), 2) AS Numeric(36, 2)) AS TARDINESS, CASE WHEN B.TIMEIN >= '13:00:00' THEN '300' ELSE '0' END AS HALFDAY FROM (select (select Timelogs_ID from db_owner.Timelogs where Emp_ID = " + empid + " AND Timelogs_time = (Select MIN(Timelogs_time) from db_owner.Timelogs)) AS TimelogsID_TimeIN, (select Timelogs_ID from db_owner.Timelogs where Emp_ID = " + empid + " AND Timelogs_time = (Select MAX(Timelogs_time) from db_owner.Timelogs)) AS TimelogsID_TimeOut, Emp_ID, Timelogs_date, MIN(Timelogs_time) AS TIMEIN, CASE WHEN COUNT(Timelogs_time) > 1 THEN MAX(Timelogs_time) ELSE NULL END AS TIMEOUT from db_owner.Timelogs A WHERE Emp_ID = " + empid + " GROUP BY EMP_iD, Timelogs_date) B) C";

        //HOLIDAY LIST
        public string holidayList(string sql)
        {
            sql = "SELECT holidaynotice_id, convert(varchar, holiday_startDate, 107) holiday_startDate, ";
            sql += "convert(varchar, holiday_endDate, 107) holiday_endDate, ";
            sql += "holiday_desc, holiday_numberOfDays FROM db_owner.HolidayList";
            return sql;
        }

        //DASHBOARD ADMIN

        public string regularEmployee(string sSearch, string sql)
        {
            sql = "select A.emp_number, A.Employment_status, A.Emp_ID, ";
            sql += "(UPPER(LEFT(cast(A.Emp_lname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(A.Emp_lname as nvarchar(max)),2,LEN(cast(A.Emp_lname as nvarchar(max))))) + ', ' + ";
            sql += "UPPER(LEFT(cast(A.Emp_fname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(A.Emp_fname as nvarchar(max)),2,LEN(cast(A.Emp_fname as nvarchar(max)))))+ ' ' + ";
            sql += "UPPER(LEFT(cast(A.Emp_mname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(A.Emp_mname as nvarchar(max)),2,LEN(cast(A.Emp_mname as nvarchar(max)))))) AS FullName, ";
            sql += "B.Department_name, C.Position_title from db_owner.Employee A left join db_owner.Department B on A.Department_ID = B.Department_ID ";
            sql += "left join db_owner.PositionTitle C on A.Position_ID = C.Position_ID where A.Employment_status = 'regular' ";
            sql += "AND (A.Emp_lname LIKE '" + sSearch + "%' OR A.Emp_fname LIKE '" + sSearch + "%' OR A.Emp_number LIkE '" + sSearch + "%') ";
            sql += "ORDER BY A.Emp_lname ASC";
            return sql;
        }
    
    }
}
