



namespace GameZone.Controllers
{

    public class GamesController : Controller
    {
        private readonly AppDbContext _context;

        public GamesController(AppDbContext context)
        {
            _context = context;
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
                Categories = _context.Categories
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .OrderBy(c => c.Text)
                .ToList(),

                Devices = _context.Categories
                .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name})
                .OrderBy(d => d.Text)
                .ToList()

                
            };
            return View(viewModel);
        }
    }
}
