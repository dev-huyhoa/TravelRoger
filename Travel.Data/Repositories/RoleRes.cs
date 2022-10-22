using Newtonsoft.Json.Linq;
using PrUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Travel.Context.Models;
using Travel.Context.Models.Travel;
using Travel.Data.Interfaces;
using Travel.Shared.Ultilities;
using Travel.Shared.ViewModels;
using Travel.Shared.ViewModels.Travel;

namespace Travel.Data.Repositories
{
    public class RoleRes: IRole
    {
        private readonly TravelContext _db;
        private Notification message;
        private Response res;
        public RoleRes(TravelContext db)
        {
            _db = db;
            message = new Notification();
            res = new Response();
        }

        public string CheckBeforSave(JObject frmData, ref Notification _message, bool isUpdate)
        {
            try
            {
                var idRole = PrCommon.GetString("idRole", frmData);
                if (String.IsNullOrEmpty(idRole))
                {
                }

                var nameRole = PrCommon.GetString("nameRole", frmData);
                if (String.IsNullOrEmpty(nameRole))
                {
                }

                var description = PrCommon.GetString("description", frmData);
                if (String.IsNullOrEmpty(description))
                {
                }
                if (isUpdate)
                {
                    UpdateRoleViewModel objUpdate = new UpdateRoleViewModel();
                    objUpdate.IdRole = int.Parse(idRole);
                    objUpdate.NameRole = nameRole;
                    objUpdate.Description = description;
                    return JsonSerializer.Serialize(objUpdate);
                }

                CreateRoleViewModel objCreate = new CreateRoleViewModel();
                //objCreate.IdRole = int.Parse(idRole);
                objCreate.NameRole = nameRole;
                objCreate.Description = description;
                return JsonSerializer.Serialize(objCreate);
            }
            catch (Exception e) 
            {
                message.DateTime = DateTime.Now;
                message.Description = e.Message;
                message.Messenge = "Có lỗi xảy ra !";
                message.Type = "Error";

                _message = message;
                return string.Empty;
            }
        }

        public Response GetsRole(bool isDelete)
        {
            try
            {
                var listRole= (from x in _db.Roles where x.IsDelete == isDelete select x).ToList();
                var result = Mapper.MapRole(listRole);
                if (result.Count() > 0)
                {
                    res.Content = result;
                }
                else
                {
                    res.Notification.DateTime = DateTime.Now;
                    res.Notification.Messenge = "Không có dữ liệu trả về !";
                    res.Notification.Type = "Warning";
                }
                return res;
            }
            catch (Exception e)
            {
                res.Notification.DateTime = DateTime.Now;
                res.Notification.Description = e.Message;
                res.Notification.Messenge = "Có lỗi xảy ra !";
                res.Notification.Type = "Error";
                return res;
            }
        }

        public Response CreateRole(CreateRoleViewModel input)
        {
            try
            {
                Role role = new Role();
                role = Mapper.MapCreateRole(input);
                _db.Roles.Add(role);
                _db.SaveChanges();
                res.Notification.DateTime = DateTime.Now;
                res.Notification.Messenge = "Thêm thành công !";
                res.Notification.Type = "Success";
                return res;
            }
            catch (Exception e)
            {

                res.Notification.DateTime = DateTime.Now;
                res.Notification.Description = e.Message;
                res.Notification.Messenge = "Có lỗi xảy ra !";
                res.Notification.Type = "Error";
                return res;
            }
        }
        public Response UpdateRole(UpdateRoleViewModel input)
        {
            try
            {
                Role role = new Role();
                role = Mapper.MapCreateRole(input);
                _db.Roles.Update(role);
                _db.SaveChanges();

                res.Notification.DateTime = DateTime.Now;
                res.Notification.Messenge = "Sửa thành công !";
                res.Notification.Type = "Success";
                return res;
            }
            catch (Exception e)
            {
                res.Notification.DateTime = DateTime.Now;
                res.Notification.Description = e.Message;
                res.Notification.Messenge = "Có lỗi xảy ra !";
                res.Notification.Type = "Error";
                return res;
            }

        }

        public Response RestoreRole(int idRole)
        {
            try
            {
                var role = _db.Roles.Find(idRole);
                if (role != null)
                {
                    role.IsDelete = false;
                    _db.SaveChanges();

                    res.Notification.DateTime = DateTime.Now;
                    res.Notification.Messenge = "Khôi phục thành công !";
                    res.Notification.Type = "Success";
                }
                else
                {
                    res.Notification.DateTime = DateTime.Now;
                    res.Notification.Messenge = "Không tìm thấy !";
                    res.Notification.Type = "Warning";
                }     
                return res;
            }
            catch (Exception e)
            {
                res.Notification.DateTime = DateTime.Now;
                res.Notification.Description = e.Message;
                res.Notification.Messenge = "Có lỗi xảy ra !";
                res.Notification.Type = "Error";
                return res;
            }

        }

        public Response DeleteRole(int idRole)
        {
            try
            {
                var role = _db.Roles.Find(idRole);
                if(role != null)
                {
                    role.IsDelete = true;
                    _db.SaveChanges();

                    res.Notification.DateTime = DateTime.Now;
                    res.Notification.Messenge = "Xóa thành công !";
                    res.Notification.Type = "Success";
                }
                else
                {
                    res.Notification.DateTime = DateTime.Now;
                    res.Notification.Messenge = "Không tìm thấy !";
                    res.Notification.Type = "Warning";
                }
                return res;
            }
            catch (Exception e)
            { 
                res.Notification.DateTime = DateTime.Now;
                res.Notification.Description = e.Message;
                res.Notification.Messenge = "Có lỗi xảy ra !";
                res.Notification.Type = "Error";
                return res;
            }
        }

        public Response SearchRole(JObject frmData)
        {
            try
            {
                Keywords keywords = new Keywords();

                var isDelete = PrCommon.GetString("isDelete", frmData);
                if (!String.IsNullOrEmpty(isDelete))
                {
                    keywords.IsDelete = Boolean.Parse(isDelete);
                }

                var idRole = PrCommon.GetString("idRole", frmData);
                if (!String.IsNullOrEmpty(isDelete))
                {
                    keywords.KwId = idRole;
                }

                var kwName = PrCommon.GetString("nameRole", frmData).Trim();
                if (!String.IsNullOrEmpty(kwName))
                {
                    keywords.KwName = kwName.Trim().ToLower();
                }
                else
                {
                    keywords.KwName = "";

                }

                var kwDescription = PrCommon.GetString("description", frmData).Trim();
                if (!String.IsNullOrEmpty(kwDescription))
                {
                    keywords.KwDescription = kwDescription.Trim().ToLower();
                }
                else
                {
                    keywords.KwDescription = "";

                }

                var kwIdRole = PrCommon.GetString("idRole", frmData);
                keywords.KwIdRole = PrCommon.getListInt(kwIdRole, ',', false);



                var listRole = new List<Role>();
                if (keywords.KwIdRole.Count > 0)
                {
                    listRole = (from x in _db.Roles
                               where x.IsDelete == keywords.IsDelete &&
                                               x.IdRole.ToString().Contains(keywords.KwId) &&
                                               x.NameRole.ToLower().Contains(keywords.KwName) &&
                                               x.Description.ToLower().Contains(keywords.KwDescription)
                                             
                               select x).ToList();
                }
                else
                {
                    listRole = (from x in _db.Roles
                               where x.IsDelete == keywords.IsDelete &&
                                               x.IdRole.ToString().Contains(keywords.KwId) &&
                                               x.NameRole.ToLower().Contains(keywords.KwName) &&
                                               x.Description.ToLower().Contains(keywords.KwDescription)
                               select x).ToList();
                }
                var result = Mapper.MapRole(listRole);
                if (listRole.Count() > 0)
                {
                    res.Content = result;
                }
                else
                {
                    res.Notification.DateTime = DateTime.Now;
                    res.Notification.Messenge = "Không có dữ liệu trả về !";
                    res.Notification.Type = "Warning";
                }
                return res;
            }
            catch (Exception e)
            {
                res.Notification.DateTime = DateTime.Now;
                res.Notification.Description = e.Message;
                res.Notification.Messenge = "Có lỗi xảy ra !";
                res.Notification.Type = "Error";
                return res;
            }
        }
    }
}
