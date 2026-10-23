using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Screenly_api.Data;
using Screenly_api.Models;

namespace Screenly_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TitlesController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Внедряем контекст базы данных через конструктор
        public TitlesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/titles?page=1&pageSize=20&search=матрица
        [HttpGet]
        public async Task<ActionResult> GetTitles(
            int page = 1,
            int pageSize = 20,
            string? search = null)
        {
            // Базовый запрос к таблице фильмов
            var query = _context.Titles.AsQueryable();

            // Поиск по названию (если передан параметр search)
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(t => t.Name.Contains(search));
            }

            // Считаем общее количество записей (для пагинации во Flutter)
            var total = await query.CountAsync();

            // Получаем данные с пагинацией и проекцией в DTO
            var items = await query
                .OrderByDescending(t => t.Rating) // Сначала с высоким рейтингом
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(t => new TitleDto
                {
                    TitleId = t.TitleId,
                    Name = t.Name,
                    ReleaseYear = t.ReleaseYear,
                    Rating = t.Rating,
                    PosterUrl = t.PosterUrl
                })
                .ToListAsync();

            // Возвращаем объект, содержащий общее количество и список фильмов
            return Ok(new { total, page, pageSize, items });
        }

        // GET: api/titles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TitleDto>> GetTitle(int id)
        {
            var title = await _context.Titles
                .Where(t => t.TitleId == id)
                .Select(t => new TitleDto
                {
                    TitleId = t.TitleId,
                    Name = t.Name,
                    ReleaseYear = t.ReleaseYear,
                    Rating = t.Rating,
                    PosterUrl = t.PosterUrl
                })
                .FirstOrDefaultAsync();

            if (title == null)
            {
                return NotFound(new { message = "Фильм не найден" });
            }

            return Ok(title);
        }
    }
}