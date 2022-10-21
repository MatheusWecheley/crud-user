using CRUD_USERS.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_USERS
{
    public partial class UserForm : Form, IUserView
    {
        private string message;
        private bool isSuccessfully;
        private bool isEdit;

        public UserForm()
        {
            InitializeComponent();
            tabControl1.TabPages.Remove(userDetails);
            ViewEvents();
        }

        private void ViewEvents()
        {
            btnSearch.Click+= delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            txtSearchUser.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    SearchEvent?.Invoke(this, EventArgs.Empty);
            };

            btnAddNew.Click += delegate { 
                AddNewEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(usersList);
                tabControl1.TabPages.Add(userDetails);
                userDetails.Text = "Adicionar Usuario";
            };

            btnUpdate.Click += delegate { 
                EditEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(usersList);
                tabControl1.TabPages.Add(userDetails);
                userDetails.Text = "Editar Usuario";
            };

            btnDelete.Click += delegate { 
               
                var result = MessageBox.Show("Você tem certeza que deseja deletar este usuário?", "Atenção!",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    DeleteEvent?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show(Message);
                }
            };

            btnSave.Click += delegate { 
                SaveEvent?.Invoke(this, EventArgs.Empty);
                if (isSuccessfully)
                {
                    tabControl1.TabPages.Remove(userDetails);
                    tabControl1.TabPages.Add(usersList);
                }
                MessageBox.Show(Message);
            };

            btnCancel.Click += delegate { 
                CancelEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(userDetails);
                tabControl1.TabPages.Add(usersList);
            };
        }

        public string UserId
        {
            get { return txtUserId.Text; }
            set { txtUserId.Text = value; }
        }

        public string UserName
        {
            get { return txtUserName.Text; }
            set { txtUserName.Text = value; }
        }

        public string UserLastName { get { return txtUserLastName.Text; } set { txtUserLastName.Text = value; } }
        public string UserAddress { get { return txtUserAddress.Text; } set { txtUserAddress.Text = value; } }
        public string UserCity { get { return txtUserCity.Text; } set { txtUserCity.Text = value; } }
        public string UserState { get { return txtUserState.Text; } set { txtUserState.Text = value; } }
        public string SearchValue { get { return txtSearchUser.Text; } set { txtSearchUser.Text = value; } }
        public bool IsEdit { get { return isEdit; } set { isEdit = value; } }
        public bool IsSuccessfully { get { return isSuccessfully; } set { isSuccessfully = value; } } 
        public string Message { get { return message; } set { message = value; } }

        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;

        public void SetUserList(BindingSource userList)
        {
            dataGridView1.DataSource = userList;
        }
    }
}
