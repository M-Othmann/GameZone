



using GameZone.Services;

namespace GameZone.Controllers
{

    public class GamesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ICategoriesService _categoriesService;
        private readonly IDevicesService _devicesService;
        private readonly IGamesService _gamesService;


        public GamesController(AppDbContext context, ICategoriesService categoriesService, IDevicesService devicesService, IGamesService gamesService)
        {
            _context = context;
            _categoriesService = categoriesService;
            _devicesService = devicesService;
            _gamesService = gamesService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {

            CreateGameFromViewModel viewModel = new()
            {
                Categories = _categoriesService.GetSelectListItems(),

                Devices = _devicesService.GetSelectListItems()


            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameFromViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Devices = _devicesService.GetSelectListItems();

                model.Categories = _categoriesService.GetSelectListItems();

                return View(model);
            }

            //save game in DB
            await _gamesService.Create(model);



            //save cover to server



            return RedirectToAction(nameof(Index));
        }
    }
}
