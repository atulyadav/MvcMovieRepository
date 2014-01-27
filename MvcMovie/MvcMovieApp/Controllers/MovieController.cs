using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMovie.DateLayer;
using MvcMovie.DomainClasses;
using System.IO;
using System.Collections.Generic;
using Ionic.Zip;

namespace MvcMovieApp.Controllers
{
    public class MovieController : Controller
    {

        private MovieContext db = new MovieContext();
        //
        // GET: /Movie/

        public ActionResult Index()
        {
            var query = from m in db.Movies select m;
            return View(query.ToList());
        }

        //
        // GET: /Movie/Details/5

        public ActionResult Details(int id = 0)
        {
            Movie movie= db.Movies.Find(id);

            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        //
        // GET: /Movie/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Movie/Create

        [HttpPost]
        public ActionResult Create(Movie movie)//FormCollection collection
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Movies.Add(movie);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(movie);
                }                
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Movie/Edit/5

        public ActionResult Edit(int id)
        {
            Movie movie = db.Movies.Find(id);
            return View(movie);
        }

        //
        // POST: /Movie/Edit/5

        [HttpPost]
        public ActionResult Edit(Movie movie)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(movie).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(movie);
            }
            catch
            {
                return View(movie);
            }
        }

        //
        // GET: /Movie/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Movie/Delete/5

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

        [HttpGet]
        public ActionResult UploadFile(int id=0)
        {
            try
            {
                // TODO: Add delete logic here
                return View();
                //return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file == null)
                    {
                        ModelState.AddModelError("file", "Please UploadFile Your File");
                    }
                    else if (file.ContentLength > 0)
                    {
                        int maxContentLength = 1024 * 1024 * 5;
                        string[] AllowedFileExtensions = new string[] { ".mp3" };
                        if (!AllowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
                        {
                            ModelState.AddModelError("file", "Please file of type: " + string.Join(", ", AllowedFileExtensions));
                        }
                        else if (file.ContentLength > maxContentLength)
                        {
                            ModelState.AddModelError("file", "Your file is too large, maximum allowed size is: " + maxContentLength + " MB");
                        }
                        else
                        {
                            var fileName = Path.GetFileName(file.FileName);
                           // var path = Path.Combine(Server.MapPath("~/Upload"), fileName);
                            String folderPath = "F:/SongCollection";

                            var path = Path.Combine(folderPath,fileName);

                            file.SaveAs(path);
                            ModelState.Clear();
                            ViewBag.Message = "File uploaded successfully";
                        }
                    }
                    return View();
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult UploadZipFile(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        public ActionResult UploadZipFile(int id,HttpPostedFileBase zipFolder)
        {

            IList<String> list = new List<String>();
            try
            {
                if (ModelState.IsValid)
                {
                    if (zipFolder == null)
                    {
                        ModelState.AddModelError("file", "Please UploadFile Your File");
                    }
                    else if (zipFolder.ContentLength > 0)
                    {
                        int maxContentLength = 1024 * 1024 * 5;
                        string[] AllowedFileExtensions = new string[] { ".zip" };
                        if (!AllowedFileExtensions.Contains(zipFolder.FileName.Substring(zipFolder.FileName.LastIndexOf('.'))))
                        {
                            //AllowedFileExtensions.Contains(System.IO.Path.GetExtension(zipFile.FileName).Substring(1));
                            ModelState.AddModelError("file", "Please file of type: " + string.Join(", ", AllowedFileExtensions));
                        }
                        else if (zipFolder.ContentLength > maxContentLength)
                        {
                            ModelState.AddModelError("file", "Your file is too large, maximum allowed size is: " + maxContentLength + " MB");
                        }
                        else
                        {
                            String folderPath = "F:/SongCollection";
                           String fileName = Path.GetFileName(zipFolder.FileName);
                           String fileName1=fileName.Split('.')[0];
                           // var fileName = "ExtractedFile";
                            var path = Path.Combine(folderPath);

                            using (ZipFile zip = ZipFile.Read(zipFolder.InputStream))
                            {
                                zip.ExtractAll(path);
                                foreach (ZipEntry z in zip)
                                {
                                    if (z.FileName.EndsWith(".mp3"))
                                    {
                                        list.Add(z.FileName.Substring(z.FileName.LastIndexOf('/')+1));
                                    }
                                }
                            }
                            Movie movie = db.Movies.Find(id);
                            
                            movie.Song = new Song { Id=movie.Id,FolderPath=fileName1};
                            db.Entry(movie.Song).State = EntityState.Modified;
                           
                            db.SaveChanges();

                            ModelState.Clear();
                            ViewBag.Message = "File uploaded successfully";
                        }
                    }
                    return View("Index",db.Movies.ToList());
                }
                return View("Index", db.Movies.ToList());
            }
            catch(Exception e)
            {
                return View();
            }
        }
    }

    
}
