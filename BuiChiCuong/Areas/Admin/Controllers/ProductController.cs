using BuiChiCuong.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuiChiCuong.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Admin/Product/
        dbModelDataContext obj = new dbModelDataContext();
        public ActionResult Index(String SearchString)
        {
            var listProduct = obj.Products.ToList();
            return View(listProduct);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product objProduct)
        {
            try
            {
                if (objProduct.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                    string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                    fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                    objProduct.Avatar = fileName;
                    objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images"), fileName));
                }
                obj.Products.InsertOnSubmit(objProduct);
                obj.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var objProduct = obj.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objProduct = obj.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }

        [HttpPost]
        public ActionResult Delete(Product objPro)
        {
            var objProduct = obj.Products.Single(n => n.Id == objPro.Id);
            obj.Products.DeleteOnSubmit(objProduct);
            obj.SubmitChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objProduct = obj.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpPost]
        public ActionResult Edit(int id, Product objProduct)
        {
            try
            {
                if (objProduct.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                    string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                    fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                    objProduct.Avatar = fileName;
                    objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images"), fileName));
                }
                Product objPro = new Product();
                objPro = obj.Products.Where(n => n.Id == objProduct.Id).Single();
                objPro.Name = objProduct.Name;
                objPro.Avatar = objProduct.Name;
                objPro.CategoryId = objProduct.CategoryId;
                objPro.ShortDes = objProduct.ShortDes;
                objPro.FullDescription = objProduct.FullDescription;
                objPro.Price = objProduct.Price;
                objPro.PriceDiscount = objProduct.PriceDiscount;
                objPro.TypeId = objProduct.TypeId;
                objPro.Slug = objProduct.Slug;
                objPro.BrandId = objProduct.BrandId;
                objPro.Deleted = objProduct.Deleted;
                objPro.ShowOnHomePage = objProduct.ShowOnHomePage;
                objPro.DisplayOrder = objProduct.DisplayOrder;
                objPro.CreatedOnUtc = objProduct.CreatedOnUtc;
                objPro.UpdateddOnUtc = objProduct.UpdateddOnUtc;
                obj.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}