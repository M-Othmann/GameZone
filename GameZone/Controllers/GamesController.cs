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
            var games = _gamesService.GetAll();
            return View(games);
        }


        [HttpGet]
        public IActionResult Details(int id)
        {
            var game = _gamesService.GetById(id);

            if (game is null)
                return NotFound();

            return View(game);
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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var game = _gamesService.GetById(id);

            if (game is null)
                return NotFound();

            EditGameFormViewModel viewModel = new()
            {
                Id = id,
                Name = game.Name,
                Description = game.Description,
                CategoryId = game.CategoryId,
                SelectedDevices = game.Devices.Select(d => d.DeviceId).ToList(),
                Categories = _categoriesService.GetSelectListItems(),
                Devices = _devicesService.GetSelectListItems(),
                CurrentCover = game.Cover
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Devices = _devicesService.GetSelectListItems();

                model.Categories = _categoriesService.GetSelectListItems();

                return View(model);
            }

            //save game in DB
            var game = await _gamesService.Edit(model);

            if (game is null)
                return BadRequest();
            return RedirectToAction(nameof(Index));
        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var isDeleted = _gamesService.Delete(id);

            return isDeleted ? Ok() : BadRequest();
        }
    }
}
