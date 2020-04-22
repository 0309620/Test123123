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

namespace SoundsharpMVCWithDB.Controllers
{
    public class Mp3PlayerController : Controller
    {
        private SoundSharpDBEntities db = new SoundSharpDBEntities();

        // GET: Mp3Player
        public ActionResult Index()
        {
            var mp3players = new List<VmMp3Player>();
            foreach (var mr in db.MemoRecorder)
            {
                // Create a new viewmodel
                var player = new VmMp3Player()
                {
                    Make = mr.AudioDevice.Mp3Player.Make,
                    Model = mr.AudioDevice.Mp3Player.Model,
                    CreationDate = mr.AudioDevice.Mp3Player.CreationDate,
                    PriceExBtw = mr.AudioDevice.Mp3Player.PriceExBtw,
                    BtwPercentage = (double)mr.AudioDevice.Mp3Player.BtwPercentage,
                    SerialId = mr.AudioDevice.Mp3Player.SerialId
                };
                // Add the viewmodel to the list
                mp3players.Add(player);
            }
            return View(mp3players);
        }

        // GET: Mp3Player/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var player = db.MemoRecorder.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            var mr = new VmMemoRecorder()
            {
                Make = player.AudioDevice.Mp3Player.Make,
                Model = player.AudioDevice.Mp3Player.Model,
                CreationDate = player.AudioDevice.Mp3Player.CreationDate,
                PriceExBtw = player.AudioDevice.Mp3Player.PriceExBtw,
                BtwPercentage = (double)player.AudioDevice.Mp3Player.BtwPercentage,
                SerialId = player.AudioDevice.Mp3Player.SerialId
            };

            return View(mr);
        }

        // GET: Mp3Player/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Mp3Player/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SerialId,Make,Model,CreationDate,PriceExBtw,BtwPercentage,MbSize,DisplayWidth,DisplayHeight")] VmMp3Player vmMp3Player)
        {
            if (ModelState.IsValid)
            {
                var device = new AudioDevice()
                {
                    Make = vmMp3Player.Make,
                    Model = vmMp3Player.Model,
                    PriceExBtw = vmMp3Player.PriceExBtw,
                    BtwPercentage = (decimal)vmMp3Player.BtwPercentage,
                    CreationDate = vmMp3Player.CreationDate,
                    SerialId = vmMp3Player.SerialId
                };

                var player = new Mp3Player()
                {
                    AudioDevice = device
                };
                db.AudioDevice.Add(device);
                db.Mp3Player.Add(player);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vmMp3Player);
        }

        // GET: Mp3Player/Edit/5
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
                
            };

            return View(mr);
        }

        // POST: Mp3Player/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SerialId,Make,Model,CreationDate,PriceExBtw,BtwPercentage,MbSize,DisplayWidth,DisplayHeight")] VmMp3Player vmMp3Player)
        {
            if (ModelState.IsValid)
            {
                var device = db.AudioDevice.Find(vmMp3Player.SerialId);
                device.Make = vmMp3Player.Make;
                device.Model = vmMp3Player.Model;
                device.PriceExBtw = vmMp3Player.PriceExBtw;
                device.BtwPercentage = (decimal)vmMp3Player.BtwPercentage;
                device.CreationDate = vmMp3Player.CreationDate;
                device.SerialId = vmMp3Player.SerialId;

                var player = db.Mp3Player.Find(vmMp3Player.SerialId);
                player.AudioDevice = device;

                db.Entry(device).State = EntityState.Modified;
                db.Entry(player).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vmMp3Player);
        }

        // GET: Mp3Player/Delete/5
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
                
            };

            return View(mr);
        }

        // POST: Mp3Player/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var player = db.Mp3Player.Find(id);
            var device = db.AudioDevice.Find(id);

            db.Mp3Player.Remove(player);
            db.AudioDevice.Remove(device);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
