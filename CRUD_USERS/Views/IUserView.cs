using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_USERS.Views
{
    public interface IUserView
    {
        string UserId { get; set; }
        string UserName { get; set; }
        string UserLastName { get; set; }
        string UserAddress { get; set; }
        string UserCity { get; set; }
        string UserState { get; set; }

        string SearchValue { get; set; }
        bool IsEdit { get; set; }
        bool IsSuccessfully { get; set; }
        string Message { get; set; }


        event EventHandler SearchEvent;
        event EventHandler AddNewEvent;
        event EventHandler EditEvent;
        event EventHandler DeleteEvent;
        event EventHandler SaveEvent;
        event EventHandler CancelEvent;

        void SetUserList(BindingSource userList);
    }

    
}
