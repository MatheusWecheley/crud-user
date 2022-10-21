using CRUD_USERS.Repositories;
using CRUD_USERS.Repositories.implementations;
using CRUD_USERS.Services;
using CRUD_USERS.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_USERS
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            IUserView view = new UserForm();
            IUserRepository repository = new SQLImplementation();
            new UserServices(repository, view);
            Application.Run((Form)view);
        }
    }
}
