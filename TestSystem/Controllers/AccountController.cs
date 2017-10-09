using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.WebPages;
using AutoMapper;
using Data;
using Models;
using TestSystem.Models.Account;
using PagedList;
using TestSystem.Utilities;

namespace TestSystem.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// Login page
        /// </summary>
        /// <returns>If user Admin rederect to Admin test list, if user is User rederect to user test list</returns>
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return HttpContext.User.IsInRole("Admin") 
                    ? RedirectToAction("TestList", "TestAdministration") 
                    : RedirectToAction("TestList", "UserTests");
            }
            ViewBag.Title = "Login";
            return View();
        }

        /// <summary>
        /// Login form post to check if Username and Password is valid
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns>Rederect to specifick page depending on role</returns>
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid) return View();
            using (var uow = new UnitOfWork())
            {
                var username = model.Username;
                var password = model.Password;
                var isValid = uow.UserRepository.CheckIfUserValid(username, password);
                if (isValid)
                {
                    Session.Clear();
                    FormsAuthentication.SetAuthCookie(username, false);
                    var role = uow.UserRepository.GetUserRole(username);
                    return role == "Admin"
                        ? RedirectToAction("TestList", "TestAdministration")
                        : RedirectToAction("TestList", "UserTests");
                }
                ModelState.AddModelError("", "Incorect username or password");
                return View();
            }
        }

        /// <summary>
        /// Logsout user from system
        /// </summary>
        /// <returns>Rederects to login page</returns>
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        /// <summary>
        /// Sortin user list and searching in user list
        /// </summary>
        /// <param name="sortOrder">What sort order is right now</param>
        /// <param name="currentFilter">what filter we selected</param>
        /// <param name="searchString">user name</param>
        /// <param name="page">In which page we are right now</param>
        /// <returns>Sorted list / Searchted item</returns>
        [Authorize(Roles = "Admin")]
        public ActionResult UserList(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.UsernameSortParm = string.IsNullOrEmpty(sortOrder) ? "username_desc" : "";
            ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.LastNameSortParm = sortOrder == "LastName" ? "lastName_desc" : "LastName";
            ViewBag.PositionSortParm = sortOrder == "Position" ? "position_desc" : "Position";
            ViewBag.GroupSortParm = sortOrder == "Group" ? "group_desc" : "Group";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var userModels = new List<UserModel>();
            using (var uow = new UnitOfWork())
            {
                IEnumerable<User> users;
                if (!string.IsNullOrEmpty(searchString))
                {
                    users = uow.UserRepository.SearchByKeyword(searchString);
                }
                else
                {
                    users = uow.UserRepository.GetAll();
                }
                foreach (var user in users)
                {
                    var userModel = Mapper.Map<UserModel>(user);
                    userModels.Add(userModel);
                }
            }
                switch (sortOrder)
            {
                case "username_desc":
                    userModels = userModels.OrderByDescending(s => s.Username).ToList();
                    break;
                case "Name":
                    userModels = userModels.OrderBy(s => s.FirstName).ToList();
                    break;
                case "name_desc":
                    userModels = userModels.OrderByDescending(s => s.FirstName).ToList();
                    break;
                case "LastName":
                    userModels = userModels.OrderBy(s => s.Lastname).ToList();
                    break;
                case "lastName_desc":
                    userModels = userModels.OrderByDescending(s => s.Lastname).ToList();
                    break;
                case "Position":
                    userModels = userModels.OrderBy(s => s.Position).ToList();
                    break;
                case "position_desc":
                    userModels = userModels.OrderByDescending(s => s.Position).ToList();
                    break;
                case "Group":
                    userModels = userModels.OrderBy(s => s.Group).ToList();
                    break;
                case "group_desc":
                    userModels = userModels.OrderByDescending(s => s.Group).ToList();
                    break;
                default:  
                    userModels = userModels.OrderBy(s => s.Username).ToList();
                    break;
            }
            var pageSize = 10;
            var pageNumber = (page ?? 1);
            return View(userModels.ToPagedList(pageNumber,pageSize));
        }

        /// <summary>
        /// Add / Edit user
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Partial view with filled infromation in inputs</returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult AddEditRecord(int? id)
        {
            ViewBag.Roles = new List<SelectListItem>()
            {
                new SelectListItem{ Text="Role", Value="Role"},
                new SelectListItem{ Text="User", Value="User"},
                new SelectListItem{ Text="Admin", Value="Admin"}
            };

            if (id != null)
            {
                ViewBag.IsUpdate = true;
                using (var uow = new UnitOfWork())
                {
                    var user = uow.UserRepository.Get(id);
                    var userModel = Mapper.Map<UserModel>(user);
                    return PartialView("UserDialog", userModel);
                }
            }

            ViewBag.IsUpdate = false;
            return PartialView("UserDialog");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userModel"></param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddEditRecord(UserModel userModel, string cmd)
        {
            ViewBag.IsUpdate = cmd != "Save";
            if (ModelState.IsValid)
            {
                if (userModel.Password != userModel.RepeatPassword)
                {
                    ModelState.AddModelError("Password", "Passwords don't match");
                    return PartialView("UserDialog",userModel);
                }
                using (var uow = new UnitOfWork())
                {   
                    if (cmd == "Save")
                    {
                        if (userModel.Password.IsEmpty())
                        {
                            ModelState.AddModelError("Password","Passwprd can't be empty");
                            return PartialView("UserDialog", userModel);
                        }
                        var usernameExists = uow.UserRepository.CheckIfUsernameExists(userModel.Username);
                        if (usernameExists)
                        {
                            ModelState.AddModelError("", "Username exists");
                            return PartialView("UserDialog", userModel);
                        }
                        var user = Mapper.Map<User>(userModel);
                        uow.UserRepository.Add(user);
                    }
                    else
                    {
                        var user = uow.UserRepository.Get(userModel.UserId);
                        Mapper.Map(userModel,user);
                    }
                    uow.Commit();
                    return JavaScript("location.reload(true)");
                }
            }
            return PartialView("UserDialog", userModel);
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Refresh page</returns>
        public ActionResult DeleteRecord(int id)
        {
            using (var uow=new UnitOfWork())
            {
                var user = uow.UserRepository.Get(id);
                uow.UserRepository.Remove(user);
                uow.Commit();
            }
            return RedirectToAction("UserList","Account");
        }

        /// <summary>
        /// Import user from csv filde
        /// </summary>
        /// <param name="model">csv file</param>
        /// <returns>Refresh page</returns>
        public ActionResult ImportUsers(UserCsvModel[] model)
        {
            if (ModelState.IsValid)
            {
                using (var uow = new UnitOfWork())
                {
                    var users = Mapper.Map<List<User>>(model);
                    uow.UserRepository.AddUsers(users);
                    uow.Commit();
                }
            }
            return RedirectToAction("UserList");
        }
    }
}