﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business.Manager;
using Entities.Models;

namespace PetStoreManager
{
    public partial class frmPeoples : Form
    {
        customerManager cusBll;
        List<customer> cusList;

        public frmPeoples()
        {
            InitializeComponent();
            this.DelegateEvent();
        }

        private void DelegateEvent()
        {
            this.Load += new EventHandler(this.Event_Load);
            this.cusDataGridView.CellMouseClick += Event_GetCustomerDetailRecord;
        }

        private void Event_Load(object sender, EventArgs e)
        {
            this.Load -= Event_Load;
            this.Load_Data();
        }

        private void Load_Data()
        {
            this.cusBll = new customerManager();
            this.cusList = cusBll.GetAll().ToList();

            this.cusDataGridView.DataSource = cusList.Select(cus => new
            {
                ID = cus.ID,
                Phone = cus.Phone,
                ContactName = cus.ContactName,
                Address = cus.Address,
                Country = cus.Country
            }).ToList();
            this.GetCustomerDetailRecord(0);

            this.cusCountNumber.Text = cusBll.GetTotalCustomer().ToString();
        }

        private void Event_GetCustomerDetailRecord(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (cusDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    this.GetCustomerDetailRecord(e.RowIndex);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return;
            }
        }

        private void GetCustomerDetailRecord(int _row)
        {
            cusDataGridView.CurrentRow.Selected = true;
            cusIdTextBox.Text = cusDataGridView.Rows[_row].Cells[0].FormattedValue.ToString();
            cusPhoneTextBox.Text = cusDataGridView.Rows[_row].Cells[1].FormattedValue.ToString();
            cusContactNameTextBox.Text = cusDataGridView.Rows[_row].Cells[2].FormattedValue.ToString();
            cusAddressTextBox.Text = cusDataGridView.Rows[_row].Cells[3].FormattedValue.ToString();
            cusCountryTextBox.Text = cusDataGridView.Rows[_row].Cells[4].FormattedValue.ToString();

            this.cusSaleCountNumber.Text = cusBll.GetTotalSaleEachCustomer(this.cusList[_row]).ToString();
            this.cusMoneyNumber.Text = cusBll.GetTotalMoneyEachCustomer(this.cusList[_row]).ToString();
        }
    }
}
