﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using System.IO;
using MvcBlog.Models;


namespace MvcBlog.Controllers
{

    public class AdminMakaleController : Controller
    {
        mvcblogDB db = new mvcblogDB();
        // GET: AdminMakale
        public ActionResult Index()
        {
            var makale = db.Makales.ToList();
            return View(makale);
        }

        // GET: AdminMakale/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminMakale/Create
        public ActionResult Create()
        {
            ViewBag.KategoriId = new SelectList(db.Kategoris, "KategoriId", "KategoriAdi");
            return View();
        }

        // POST: AdminMakale/Create
        [HttpPost]
        public ActionResult Create(Makale makale, string etiketler, HttpPostedFileBase Foto)
        {
            try
            {
                if (Foto != null)
                {
                    WebImage img = new WebImage(Foto.InputStream);
                    FileInfo fotoinfo= new FileInfo(Foto.FileName);

                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Resize(800, 350);
                    img.Save("~/Uploads/MakaleFoto/" + newfoto);
                    makale.Foto=("/Uploads/MakaleFoto/" + newfoto);

                    db.Makales.Add(makale);
                    db.SaveChanges();

                }

                if (etiketler != null)
                {
                    string[] etiketdizi = etiketler.Split(',');
                    foreach (var i in etiketdizi)
                    {
                        var yenietiket = new Etiket { EtiketAdi = i };
                        db.Etikets.Add(yenietiket);
                        makale.Etikets.Add(yenietiket);
                    }
                }

                db.Makales.Add(makale);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(makale);
            }
        }

        // GET: AdminMakale/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminMakale/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminMakale/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminMakale/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
