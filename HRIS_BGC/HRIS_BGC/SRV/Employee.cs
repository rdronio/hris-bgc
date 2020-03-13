using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Net;
using System.IO;
using System.Data.SqlTypes;

namespace SRV
{
    public class Employee
    {
        Common common = new Common();
        SqlConnection con = new SqlConnection();
        SqlDataAdapter sda = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        public void AddEmployee(string birthday, string email, string password, string firstname, string middlename, string lastname, string gender, string civilsatus, string phone, string street, string province, string city, string barangay, string emergency_name, string emergency_no, string date_employed, string department_id, string position_id, string basic_salary, string shift, string employment_status, string termination_date, string tin_no, string philhealth_no, string hdmf_no, string sss_no, string account_no, string drivers_license, string expiration_date, string restriction_code, string bio_number, string emp_number, string schedule_from, string schedule_to)
        {
            try
            {
                con = new SqlConnection(common.sCon);
                con.Open();
                cmd = new SqlCommand("INSERT INTO db_owner.Employee (Emp_bday, Emp_email, Emp_pass, Emp_fname, Emp_mname, Emp_lname, Emp_gender, Emp_civilstatus, Emp_phone, Street, Province, City, barangay, emergency_contactname, emergency_contactno, Date_Employed, Department_ID, Position_ID, Emp_BasicSalary, Employment_status, Shift, Tin_no, Philhealth_no, HDMF_no, SSS_no, Account_no, drivers_license, expiration_date, restriction_code, bio_number, emp_number, schedule_from, schedule_to) VALUES(@Emp_bday, @Emp_email, @Emp_pass, @Emp_fname, @Emp_mname, @Emp_lname, @Emp_gender, @Emp_civilstatus, @Emp_phone, @Street, @Province, @City, @barangay, @emergency_contactname, @emergency_contactno, @Date_Employed, @Department_ID, @Position_ID, @Emp_BasicSalary, @Employment_status, @Shift, @Tin_no, @Philhealth_no, @HDMF_no, @SSS_no, @Account_no, @drivers_license, @expiration_date, @restriction_code, @bio_number, @emp_number, @schedule_from, @schedule_to) ", con);

                cmd.Parameters.AddWithValue("Emp_bday", birthday);
                cmd.Parameters.AddWithValue("Emp_email", email);
                cmd.Parameters.AddWithValue("Emp_pass", password);
                cmd.Parameters.AddWithValue("Emp_fname", firstname);
                cmd.Parameters.AddWithValue("Emp_mname", middlename);
                cmd.Parameters.AddWithValue("Emp_lname", lastname);
                cmd.Parameters.AddWithValue("Emp_gender", gender);
                cmd.Parameters.AddWithValue("Emp_civilstatus", civilsatus);
                cmd.Parameters.AddWithValue("Emp_phone", phone);
                cmd.Parameters.AddWithValue("Street", street);
                cmd.Parameters.AddWithValue("Province", province);
                cmd.Parameters.AddWithValue("City", city);
                cmd.Parameters.AddWithValue("barangay", barangay);
                cmd.Parameters.AddWithValue("emergency_contactname", emergency_name);
                cmd.Parameters.AddWithValue("emergency_contactno", emergency_no);
                try
                {
                    cmd.Parameters.AddWithValue("Date_Employed", DateTime.Parse(date_employed));
                }
                catch
                {
                    cmd.Parameters.AddWithValue("Date_Employed", date_employed);
                }
                cmd.Parameters.AddWithValue("Department_ID", department_id);
                cmd.Parameters.AddWithValue("Position_ID", position_id);
                cmd.Parameters.AddWithValue("Emp_BasicSalary", basic_salary);
                cmd.Parameters.AddWithValue("Employment_status", employment_status);
                cmd.Parameters.AddWithValue("Shift", shift);
                cmd.Parameters.AddWithValue("Tin_no", tin_no);
                cmd.Parameters.AddWithValue("Philhealth_no", philhealth_no);
                cmd.Parameters.AddWithValue("HDMF_no", hdmf_no);
                cmd.Parameters.AddWithValue("SSS_no", sss_no);
                cmd.Parameters.AddWithValue("Account_no", account_no);
                cmd.Parameters.AddWithValue("drivers_license", drivers_license);
                SqlDateTime sqldatenull = SqlDateTime.Null;
                try
                {
                    if (expiration_date != "")
                    {
                        cmd.Parameters.AddWithValue("expiration_date", DateTime.Parse(expiration_date));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("expiration_date", sqldatenull);
                    }
                }
                catch
                {
                    if (expiration_date != "")
                    {
                        cmd.Parameters.AddWithValue("expiration_date", expiration_date);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("expiration_date", SqlDateTime.Null);
                    }
                 
                }

                cmd.Parameters.AddWithValue("restriction_code", restriction_code);
                cmd.Parameters.AddWithValue("bio_number", bio_number);
                cmd.Parameters.AddWithValue("emp_number", emp_number);
                cmd.Parameters.AddWithValue("schedule_from", schedule_from);
                cmd.Parameters.AddWithValue("schedule_to", schedule_to);


                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object LoadCountUsername(string username, DataTable dt)
        {
            dt.Clear();
            try
            {
                string sSQL = "select * from db_owner.Employee where Emp_email ='" + username + "'";
                con = new SqlConnection(common.sCon);
                con.Open();
                cmd = new SqlCommand(sSQL, con);
                sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                con.Close();

                return dt;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void AddHRInfo(string date_employed, string department_id, string position_id, string basic_salary, string shift, string employment_status, string termination_date, string tin_no, string philhealth_no, string hdmf_no, string sss_no, string account_no)
        {
            try
            {
                con = new SqlConnection(common.sCon);
                con.Open();
                cmd = new SqlCommand("INSERT INTO db_owner.Employee () VALUES(@Date_Employed, @Department_ID, @Position_ID, @Emp_BasicSalary, @Employment_status, @Shift, @Tin_no, @Philhealth_no, @HDMF_no, @SSS_no, @Account_no) ", con);

                try
                {
                    cmd.Parameters.AddWithValue("Date_Employed", DateTime.Parse(date_employed));
                }
                catch
                {
                    cmd.Parameters.AddWithValue("Date_Employed",date_employed);
                }
                cmd.Parameters.AddWithValue("Department_ID", department_id);
                cmd.Parameters.AddWithValue("Position_ID", position_id);
                cmd.Parameters.AddWithValue("Emp_BasicSalary", basic_salary);
                cmd.Parameters.AddWithValue("Employment_status", employment_status);
                cmd.Parameters.AddWithValue("Shift", shift);
                cmd.Parameters.AddWithValue("Tin_no", tin_no);
                cmd.Parameters.AddWithValue("Philhealth_no", philhealth_no);
                cmd.Parameters.AddWithValue("HDMF_no", hdmf_no);
                cmd.Parameters.AddWithValue("SSS_no", sss_no);
                cmd.Parameters.AddWithValue("Account_no", account_no);

                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void UpdateProfile(int empid, string birthday, string email, string firstname, string middlename, string lastname, string gender, string civilsatus, string phone, string street, string province, string city, string barangay, string emergency_name, string emergency_no, string date_employed, string department_id, string position_id, string basic_salary, string shift, string employment_status, string termination_date, string tin_no, string philhealth_no, string hdmf_no, string sss_no, string account_no, string drivers_license, string expiration_date, string restriction_code, string bio_number, string emp_number, string schedule_from, string schedule_to)
        {
            try
            {
                SqlConnection con = new SqlConnection(common.sCon);
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE db_owner.Employee SET schedule_from = @schedule_from, schedule_to = @schedule_to, bio_number = @bio_number, emp_number = @emp_number, drivers_license = @drivers_license, expiration_date = @expiration_date, restriction_code = @restriction_code, Emp_bday = @Emp_bday, Emp_email = @Emp_email, Emp_fname = @Emp_fname, Emp_mname = @Emp_mname, Emp_lname = @Emp_lname, Emp_gender = @Emp_gender, Emp_civilstatus = @Emp_civilstatus, Emp_phone = @Emp_phone, Street = @Street, Province = @Province, City = @City, emergency_contactname = @emergency_contactname, emergency_contactno = @emergency_contactno, Date_Employed = @Date_Employed, Department_ID = @Department_ID, Position_ID = @Position_ID, Emp_BasicSalary = @Emp_BasicSalary, Employment_status = @Employment_status, Shift = @Shift, Tin_no = @Tin_no, Philhealth_no = @Philhealth_no, HDMF_no = @HDMF_no, SSS_no = @SSS_no, Account_no = @Account_no WHERE Emp_ID =" + empid, con);

                cmd.Parameters.AddWithValue("@Emp_bday", birthday);
                cmd.Parameters.AddWithValue("@Emp_email", email);
                cmd.Parameters.AddWithValue("@Emp_fname", firstname);
                cmd.Parameters.AddWithValue("@Emp_mname", middlename);
                cmd.Parameters.AddWithValue("@Emp_lname", lastname);
                cmd.Parameters.AddWithValue("@Emp_gender", gender);
                cmd.Parameters.AddWithValue("@Emp_civilstatus", civilsatus);
                cmd.Parameters.AddWithValue("@Emp_phone", phone);
                cmd.Parameters.AddWithValue("@Street", street);
                cmd.Parameters.AddWithValue("@Province", province);
                cmd.Parameters.AddWithValue("@City", city);
                cmd.Parameters.AddWithValue("@emergency_contactname", emergency_name);
                cmd.Parameters.AddWithValue("@emergency_contactno", emergency_no);
                try
                {
                    cmd.Parameters.AddWithValue("@Date_employed", DateTime.Parse(date_employed));
                }
                catch
                {
                    cmd.Parameters.AddWithValue("@Date_employed", date_employed);
                }
                cmd.Parameters.AddWithValue("@Department_ID", department_id);
                cmd.Parameters.AddWithValue("@Position_ID", position_id);
                cmd.Parameters.AddWithValue("@Emp_BasicSalary", basic_salary);
                cmd.Parameters.AddWithValue("@Shift", shift);
                cmd.Parameters.AddWithValue("@Employment_status", employment_status);
                cmd.Parameters.AddWithValue("@termination_date", termination_date);
                cmd.Parameters.AddWithValue("@Tin_no", tin_no);
                cmd.Parameters.AddWithValue("@Philhealth_no", philhealth_no);
                cmd.Parameters.AddWithValue("@HDMF_no", hdmf_no);
                cmd.Parameters.AddWithValue("@SSS_no", sss_no);
                cmd.Parameters.AddWithValue("@Account_no", account_no);
                cmd.Parameters.AddWithValue("@drivers_license", drivers_license);

                SqlDateTime sqldatenull = SqlDateTime.Null;
                try
                {
                    if (expiration_date != "")
                    {
                        cmd.Parameters.AddWithValue("expiration_date", DateTime.Parse(expiration_date));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("expiration_date", sqldatenull);
                    }
                }
                catch
                {
                    if (expiration_date != "")
                    {
                        cmd.Parameters.AddWithValue("expiration_date", expiration_date);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("expiration_date", SqlDateTime.Null);
                    }

                }
                cmd.Parameters.AddWithValue("@restriction_code", restriction_code);
                cmd.Parameters.AddWithValue("@bio_number", bio_number);
                cmd.Parameters.AddWithValue("@emp_number", emp_number);
                try
                {
                    cmd.Parameters.AddWithValue("@schedule_from", DateTime.Parse(schedule_from));
                }
                catch
                {
                    cmd.Parameters.AddWithValue("@schedule_from", schedule_from);
                }

                try
                {
                    cmd.Parameters.AddWithValue("@schedule_to", DateTime.Parse(schedule_to));
                }
                catch
                {
                    cmd.Parameters.AddWithValue("@schedule_to", schedule_to);
                }

                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
