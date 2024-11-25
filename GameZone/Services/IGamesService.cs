namespace GameZone.Services
{
    public interface IGamesService
    {
        Task Create(CreateGameFromViewModel game);

        Task<Game?> Edit(EditGameFormViewModel model);

        Game? GetById(int id);

        IEnumerable<Game> GetAll();

        bool Delete(int id);

    }
}
