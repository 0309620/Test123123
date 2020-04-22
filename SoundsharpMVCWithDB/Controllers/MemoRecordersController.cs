using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AudioDevices;
using SoundsharpMVCWithDB.ViewModels;

namespace SoundsharpMVC.Controllers
{
    /// <summary>
    /// Memorecorder controller
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class MemoRecordersController : Controller
    {
        /// <summary>
        /// The database
        /// </summary>
        private SoundSharpDBEntities db = new SoundSharpDBEntities();

        // GET: MemoRecorders
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var memorecorders = new List<VmMemoRecorder>();
            foreach (var mr in db.MemoRecorder)
            {
                // Create a new viewmodel
                var recorder = new VmMemoRecorder()
                {
                    Make = mr.AudioDevice.Make,
                    Model = mr.AudioDevice.Model,
                    CreationDate = mr.AudioDevice.CreationDate,
                    PriceExBtw = mr.AudioDevice.PriceExBtw,
                    BtwPercentage = (double)mr.AudioDevice.BtwPercentage,
                    SerialId = mr.AudioDevice.SerialId,
                    MaxMemoCartridgeType = mr.MaxCartridgeType
                };
                // Add the viewmodel to the list
                memorecorders.Add(recorder);
            }
            return View(memorecorders);
        }

        // GET: MemoRecorders/Details/5
        /// <summary>
        /// Detailses the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var recorder = db.MemoRecorder.Find(id);
            if (recorder == null)
            {
                return HttpNotFound();
            }
            var mr = new VmMemoRecorder()
            {
                Make = recorder.AudioDevice.Make,
                Model = recorder.AudioDevice.Model,
                CreationDate = recorder.AudioDevice.CreationDate,
                PriceExBtw = recorder.AudioDevice.PriceExBtw,
                BtwPercentage = (double)recorder.AudioDevice.BtwPercentage,
                SerialId = recorder.AudioDevice.SerialId,
                MaxMemoCartridgeType = recorder.MaxCartridgeType
            };

            return View(mr);
        }

        // GET: MemoRecorders/Create
        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        // POST: MemoRecorders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Creates the specified vm memo recorder.
        /// </summary>
        /// <param name="vmMemoRecorder">The vm memo recorder.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SerialdId,Make,Model,CreationDate,PriceExBtw,BtwPercentage,MaxMemoCartridgeType")]
            VmMemoRecorder vmMemoRecorder)
        {
            if (ModelState.IsValid)
            {
                var device = new AudioDevice()
                {
                    Make = vmMemoRecorder.Make,
                    Model = vmMemoRecorder.Model,
                    PriceExBtw = vmMemoRecorder.PriceExBtw,
                    BtwPercentage = (decimal)vmMemoRecorder.BtwPercentage,
                    CreationDate = vmMemoRecorder.CreationDate,
                    SerialId = vmMemoRecorder.SerialId
                };

                var recorder = new MemoRecorder()
                {
                    MaxCartridgeType = vmMemoRecorder.MaxMemoCartridgeType,
                    AudioDevice = device
                };
                db.AudioDevice.Add(device);
                db.MemoRecorder.Add(recorder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vmMemoRecorder);
        }

        // GET: MemoRecorders/Edit/5
        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var recorder = db.MemoRecorder.Find(id);
            if (recorder == null)
            {
                return HttpNotFound();
            }
            var mr = new VmMemoRecorder()
            {
                Make = recorder.AudioDevice.Make,
                Model = recorder.AudioDevice.Model,
                CreationDate = recorder.AudioDevice.CreationDate,
                PriceExBtw = recorder.AudioDevice.PriceExBtw,
                BtwPercentage = (double)recorder.AudioDevice.BtwPercentage,
                SerialId = recorder.AudioDevice.SerialId,
                MaxMemoCartridgeType = recorder.MaxCartridgeType
            };

            return View(mr);
        }

        // POST: MemoRecorders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Edits the specified vm memo recorder.
        /// </summary>
        /// <param name="vmMemoRecorder">The vm memo recorder.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SerialdId,Make,Model,CreationDate,PriceExBtw,BtwPercentage,MaxMemoCartridgeType")]
            VmMemoRecorder vmMemoRecorder)
        {
            if (ModelState.IsValid)
            {
                var device = db.AudioDevice.Find(vmMemoRecorder.SerialId);
                device.Make = vmMemoRecorder.Make;
                device.Model = vmMemoRecorder.Model;
                device.PriceExBtw = vmMemoRecorder.PriceExBtw;
                device.BtwPercentage = (decimal)vmMemoRecorder.BtwPercentage;
                device.CreationDate = vmMemoRecorder.CreationDate;
                device.SerialId = vmMemoRecorder.SerialId;

                var recorder = db.MemoRecorder.Find(vmMemoRecorder.SerialId);
                recorder.MaxCartridgeType = vmMemoRecorder.MaxMemoCartridgeType;
                recorder.AudioDevice = device;

                db.Entry(device).State = EntityState.Modified;
                db.Entry(recorder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vmMemoRecorder);
        }

        // GET: MemoRecorders/Delete/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var recorder = db.MemoRecorder.Find(id);
            if (recorder == null)
            {
                return HttpNotFound();
            }
            var mr = new VmMemoRecorder()
            {
                Make = recorder.AudioDevice.Make,
                Model = recorder.AudioDevice.Model,
                CreationDate = recorder.AudioDevice.CreationDate,
                PriceExBtw = recorder.AudioDevice.PriceExBtw,
                BtwPercentage = (double)recorder.AudioDevice.BtwPercentage,
                SerialId = recorder.AudioDevice.SerialId,
                MaxMemoCartridgeType = recorder.MaxCartridgeType
            };

            return View(mr);
        }

        // POST: MemoRecorders/Delete/5
        /// <summary>
        /// Deletes the confirmed.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var recorder = db.MemoRecorder.Find(id);
            var device = db.AudioDevice.Find(id);

            db.MemoRecorder.Remove(recorder);
            db.AudioDevice.Remove(device);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Releases unmanaged resources and optionally releases managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

