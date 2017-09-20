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

        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
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
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    var role = uow.UserRepository.GetUserRole(username);
                    return role == "Admin"
                        ? RedirectToAction("TestList", "TestAdministration")
                        : RedirectToAction("TestList", "UserTests");
                }
                ModelState.AddModelError("", "Incorect username or password");
                return View();
            }
        }
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

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

        public ActionResult ImportUsers(UserCsvModel[] model)
        {
            if (ModelState.IsValid)
            {
                using (var uow = new UnitOfWork())
                {
                    var users = Mapper.Map<List<User>>(model);
                    uow.UserRepository.BulkMergeUsers(users);
                }
            }
            return RedirectToAction("UserList");
        }
    }
}