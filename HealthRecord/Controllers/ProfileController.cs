using HealthRecord.Data;
using HealthRecord.Data.Entities;
using HealthRecord.Helpers;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HealthRecord.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public ProfileController(ApplicationDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public ActionResult Index(string id = null)
        {
            if (id is null)
            {
                var userId = User.Identity.GetUserId();

                string memberId = _context.Creations
                .Include(x => x.Account)
                .Where(x => x.Account.Id == userId)
                .OrderBy(x => x.MemberType)
                .FirstOrDefault()
                .Id ?? string.Empty;

                return RedirectToAction("Index", new { Id = memberId });
            }

            var member = _context.Creations.FirstOrDefault(x => x.Id == id);

            if (member is Animal animal)
            {
                return View(GetAnimal());
            }
            else
            {
                return View(GetCreation());
            }
        }
        
        public ActionResult AddMember()
        {
            ViewBag.Message = "Add new member.";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddMember(Animal model)
        {
            var user = GetUser();

            Creation creation = MemberFactory.CreateMember(model.MemberType);
            creation.Id = Guid.NewGuid().ToString();
            creation.DateOfBirth = DateTime.Now;
            creation.ImageLink = !String.IsNullOrEmpty(model.ImageLink) ? model.ImageLink : @"https://kooledge.com/assets/default_medium_avatar-57d58da4fc778fbd688dcbc4cbc47e14ac79839a9801187e42a796cbd6569847.png";
            creation.Name = !String.IsNullOrEmpty(model.Name) ? model.Name : "New member";
            creation.MemberType = model.MemberType;
            creation.Sex = model.Sex;
            creation.Weight = model.Weight;

            user.Creations.Add(creation);
            await SaveChanges();

            return RedirectToAction("Index", model.Id);
        }

        public ActionResult AddVaccine(string id)
        {
            ViewBag.Message = "Add new vaccine.";
            ViewBag.CreationId = id;

            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddVaccine(Vaccine model, string creationId)
        {
            Vaccine vaccine = new Vaccine();
            vaccine.Id = Guid.NewGuid().ToString();
            vaccine.Name = model.Name;
            vaccine.Serie = model.Serie;
            vaccine.DateOfVaccination = DateTime.Now;

            var creation = GetCreation(creationId);

            creation.Vaccines.Add(vaccine);
            await SaveChanges();

            return RedirectToAction("Index");
        }
        
        public ActionResult EditVaccine(string id, string creationId)
        {
            ViewBag.Message = "Edit vaccine.";
            ViewBag.CreationId = creationId;

            var creation = GetCreation(creationId);

            var vacine = creation.Vaccines
                .Where(x => x.Id == id)
                .FirstOrDefault();

            return View(vacine);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditVaccine(Vaccine model, string creationId)
        {
            var creation = GetCreation(creationId);

            var vaccine = creation.Vaccines
                .Where(x => x.Id == model.Id)
                .FirstOrDefault();

            vaccine.Id = model.Id;
            vaccine.Name = model.Name;
            vaccine.Serie = model.Serie;

            await SaveChanges();

            return RedirectToAction("Index");
        }
        
        public ActionResult AddMedicalRecord(string id)
        {
            ViewBag.Message = "Add new medical record.";
            ViewBag.CreationId = id;

            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddMedicalRecord(MedicalHistoryRecord model, string creationId)
        {
            var creation = GetCreation(creationId);
            var userId = GetUser().Id;

            MedicalHistoryRecord record = new MedicalHistoryRecord();
            record.Id = Guid.NewGuid().ToString();
            record.Name = model.Name;
            record.RecordType = model.RecordType;
            record.DateOfEvent = model.DateOfEvent;
            record.Description = model.Description;

            if (creation is null || !_context.Users.Include(x => x.Creations).First(x => x.Id == userId).Creations.Any(x => x.Id == creation.Id))
                throw new HttpException(404, "Creation Not Found");

            creation.MedicalHistory.Add(record);
            await SaveChanges();

            return RedirectToAction("Index");
        }
        
        public ActionResult EditMedicalRecord(string id, string creationId)
        {
            ViewBag.Message = "Edit medical record.";
            ViewBag.CreationId = creationId;
        
            var creation = GetCreation(creationId);

            var record = creation.MedicalHistory
                .Where(x => x.Id == id)
                .FirstOrDefault();

            return View(record);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditMedicalRecord(MedicalHistoryRecord model, string creationId)
        {
            var creation = GetCreation(creationId);

            var record = creation.MedicalHistory
                .Where(x => x.Id == model.Id)
                .FirstOrDefault();

            record.DateOfEvent = model.DateOfEvent;
            record.Description = model.Description;
            record.Id = model.Id;
            record.Name = model.Name;
            record.RecordType = model.RecordType;

            await SaveChanges();

            return RedirectToAction("Index");
        }
        
        public ActionResult AddChip(string id)
        {
            ViewBag.Message = "Add new medical record.";
            ViewBag.CreationId = id;

            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddChip(Chip model, string creationId)
        {
            Chip chip = new Chip();
            chip.Id = Guid.NewGuid().ToString();
            chip.ChipDate = model.ChipDate;
            chip.Name = model.Name;
            chip.Number = model.Number;

            var creation = GetAnimal(creationId);

            creation.Chips.Add(chip);
            await SaveChanges();

            return RedirectToAction("Index");
        }
        
        public ActionResult EditChip(string id, string creationId)
        {
            ViewBag.Message = "Edit medical record.";
            ViewBag.CreationId = creationId;

            var creation = GetAnimal(creationId);

            var chip = creation.Chips
                .Where(x => x.Id == id)
                .FirstOrDefault();

            return View(chip);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditChip(Chip model, string creationId)
        {
            var creation = GetAnimal(creationId);

            var chip = creation.Chips
                .Where(x => x.Id == model.Id)
                .FirstOrDefault();

            chip.Id = model.Id;
            chip.ChipDate = model.ChipDate;
            chip.Name = model.Name;
            chip.Number = model.Number;

            await SaveChanges();

            return RedirectToAction("Index");
        }
        
        public ActionResult EditMember(string id)
        {
            ViewBag.Message = "Edit member.";

            var model = _context.Creations
                .FirstOrDefault(x => x.Id == id);

            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditMember(Creation model, string id)
        {
            var creation = GetCreation(id);

            creation.DateOfBirth = model.DateOfBirth;
            creation.ImageLink = model.ImageLink;
            creation.MedicalHistory = model.MedicalHistory;
            creation.Name = model.Name;
            creation.Sex = model.Sex;
            creation.Vaccines = model.Vaccines;
            creation.Weight = model.Weight;

            await SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult DisplayMenu()
        {
            var user = GetUser();
            var model = _context.Creations
                .Include(x => x.Account)
                .Where(x => x.Account.Id == user.Id)
                .GroupBy(x => x.MemberType)
                .AsEnumerable();

            return PartialView("_MenuPartial", model);
        }

        private ApplicationUser GetUser()
        {
            var userId = User.Identity.GetUserId();
            var user = _context.Users
                .Include(x => x.Creations)
                .FirstOrDefault(x => x.Id == userId);

            return user;
        }

        private Creation GetCreation(string creationId = "")
        {
            if (String.IsNullOrEmpty(creationId))
            {
                return _context.Creations
                        .Include(x => x.MedicalHistory)
                        .Include(x => x.Vaccines)
                        .FirstOrDefault();
            }

            var creation = _context.Creations
                .Include(x => x.MedicalHistory)
                .Include(x => x.Vaccines)
                .Where(x => x.Id == creationId)
                .FirstOrDefault()
                ?? throw new HttpException(404, "Member not found");

            return creation;
        }

        private Animal GetAnimal(string creationId = "")
        {
            if(String.IsNullOrEmpty(creationId))
            {
                return _context.Creations
                        .OfType<Animal>()
                        .Include(x => x.Chips)
                        .Include(x => x.MedicalHistory)
                        .Include(x => x.Vaccines)
                        .FirstOrDefault();
            }

            var animal = _context.Creations
                        .OfType<Animal>()
                        .Include(x => x.Chips)
                        .Include(x => x.MedicalHistory)
                        .Include(x => x.Vaccines)
                        .Where(x => x.Id == creationId)
                        .FirstOrDefault()
                        ?? throw new HttpException(404, "Animal not found");

            return animal;
        }

        private async Task SaveChanges()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, "Database exception: " + ex.Message);
            }
        }
    }
}