using KPMGAssignment.Models.Validation;
using KPMGAssignment.Models.ViewModels;
using KPMGAssignment.Services;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace KPMGAssignment.Controllers
{
    public class HomeController : Controller
    {
        private ITransactionService _transactionService;

        public HomeController()
        {
            _transactionService = new TransactionService();
        }

        public HomeController(ITransactionService svc)
        {
            _transactionService = svc;
        }


        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Upload(HttpPostedFileBase files)
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                if (file.ContentLength > 0 && file.FileName.EndsWith(".csv"))
                {
                    return View(await _transactionService.ProcessFile(file.InputStream));
                }
                ModelState.AddModelError("", "File format is not supported.");
            }
            return View();
        }


        public async Task<ActionResult> Transactions()
        {
            return View(await _transactionService.GetAllTransactions());
        }


        [HttpGet]
        public async Task<ActionResult> EditTransaction(int id)
        {
            return View(await _transactionService.GetTransactionByID(id));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditTransaction(TransactionViewModel model)
        {
            TransactionValidation.Validate(model);
            if (model.IsValid)
            {
                await _transactionService.SaveTransaction(model);

                return RedirectToAction("Transactions");
            }

            ModelState.AddModelError("", model.ValidationMessage);
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteTransaction(int id)
        {
            await _transactionService.DeleteTransaction(id);
            return RedirectToAction("Transactions");
        }
    }
}