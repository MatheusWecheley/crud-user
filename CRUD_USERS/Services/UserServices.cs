using CRUD_USERS.Models;
using CRUD_USERS.Repositories;
using CRUD_USERS.Repositories.implementations;
using CRUD_USERS.Services.Validation;
using CRUD_USERS.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_USERS.Services
{
    public class UserServices
    {
        private IUserRepository userRepository;
        private IUserView userView;
        private BindingSource userBindingSource;
        private IEnumerable<UserModel> userList;


        public UserServices(IUserRepository userRepository, IUserView userView)
        {
            this.userBindingSource = new BindingSource();
            this.userRepository = userRepository;
            this.userView = userView;
            this.userView.SearchEvent += SearchUser;
            this.userView.AddNewEvent += AddNew;
            this.userView.CancelEvent += CancelNewUser;
            this.userView.EditEvent += EditUser;
            this.userView.DeleteEvent += DeleteUser;
            this.userView.SaveEvent += SaveUser;
            this.userView.SetUserList(userBindingSource);
            LoadAllUsers();
        }

        private void EditUser(object sender, EventArgs e)
        {
            var user = (UserModel)userBindingSource.Current;
            userView.UserId = user.Id.ToString();
            userView.UserName = user.Name;
            userView.UserLastName = user.LastName;
            userView.UserAddress = user.Address;
            userView.UserCity = user.City;
            userView.UserState = user.State;
            userView.IsEdit = true;
        }

        private void SaveUser(object sender, EventArgs e)
        {
            var user = new UserModel();
            user.Id = Convert.ToInt32(userView.UserId);
            user.Name = userView.UserName;
            user.LastName = userView.UserLastName;
            user.Address = userView.UserAddress;
            user.City = userView.UserCity;
            user.State = userView.UserState;

            try
            {
                new UserDataValidation().Validate(user);
                if(userView.IsEdit)
                {
                    userRepository.update(user);
                    userView.Message = "Usuário Alterado!";
                }
                else
                {
                    userRepository.add(user);
                    userView.Message = "Usuário Criado!";
                }
                userView.IsSuccessfully = true;
                LoadAllUsers();
                CleanAllFields();
            }
            catch(Exception ex)
            {
                userView.IsSuccessfully = false;
                userView.Message = ex.Message;
            }
        }

        private void CleanAllFields()
        {
            userView.UserId = "0";
            userView.UserName = "";
            userView.UserLastName = "";
            userView.UserAddress = "";
            userView.UserCity = "";
            userView.UserState = "";
            userView.IsEdit = true;
        }

        private void DeleteUser(object sender, EventArgs e)
        {
            try
            {
                var user = (UserModel)userBindingSource.Current;
                userRepository.delete(user.Id);
                userView.IsSuccessfully = true;
                userView.Message = "Usuário deletado com sucesso!";
                LoadAllUsers();
            }catch(Exception ex)
            {
                userView.IsSuccessfully = false;
                userView.Message = "Algo deu errado, usuário não pode ser deletado!";
            }
        }
        

        private void CancelNewUser(object sender, EventArgs e)
        {
            CleanAllFields();
        }

        private void AddNew(object sender, EventArgs e)
        {
            userView.IsEdit = false;
        }

        private void SearchUser(object sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(this.userView.SearchValue);
            if (emptyValue == false)
                userList = this.userRepository.getUser(this.userView.SearchValue);
            else userList = this.userRepository.GetAllUsers();
            userBindingSource.DataSource = userList;
        }

        private void LoadAllUsers()
        {
            userList = userRepository.GetAllUsers();
            userBindingSource.DataSource = userList;
        }
    }
}
