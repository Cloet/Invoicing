using AutoMapper;
using Invoicing.Domain.Model;
using Invoicing.EntityFramework.Services;
using Invoicing.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Invoicing.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ArticleController: ControllerBase
    {

        private readonly ILogger<ArticleController> _logger;
        private readonly IArticleService _articleService;
        private readonly IMapper _mapper;

        public ArticleController(IMapper mapper, ILogger<ArticleController> logger, IArticleService articleService)
        {
            _mapper = mapper;
            _logger = logger;
            _articleService = articleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllArticles()
        {
            try
            {
                var articles = await _articleService.GetAllAsync();

                if (!articles.Any())
                    return NoContent();

                var dto = _mapper.Map<IEnumerable<ArticleDTO>>(articles);

                return Ok(dto);
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ValidationError.CreateError(ex.Message));
            }
        }

        [HttpGet("{id}", Name="article")]
        public async Task<IActionResult> GetArticle(int id)
        {
            try
            {
                var article = await _articleService.GetOneAsync(id);

                if (article == null)
                    return NotFound(ValidationError.CreateError($"Article with Id = {id} not found."));

                var dto = _mapper.Map<ArticleDTO>(article);

                return Ok(dto);
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ValidationError.CreateError(ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostArticle(Article article)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var a = await _articleService.FilterAsync(x => x.ArticleCode == article.ArticleCode,1);

                if (a.Count() > 0)
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, ValidationError.CreateError($"An article with code = {article.ArticleCode} already exists."));

                await _articleService.CreateOneAsync(article);
                await _articleService.SaveAsync();

                article = await _articleService.GetOneAsync(article.Id);

                var dto = _mapper.Map<ArticleDTO>(article);

                return CreatedAtRoute("article", new { id = article.Id }, dto);
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ValidationError.CreateError(ex.Message));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticle(int id, Article article)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                article.Id = id;
                var a = await _articleService.GetOneAsync(article.Id);

                if (a == null)
                    return NotFound(ValidationError.CreateError($"No article found with id {article.Id}"));

                await _articleService.UpdateOneAsync(article);
                await _articleService.SaveAsync();

                article = await _articleService.GetOneAsync(article.Id);

                var dto = _mapper.Map<ArticleDTO>(article);

                return CreatedAtRoute("article", new { id = article.Id }, dto);
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ValidationError.CreateError(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            try
            {
                var article = await _articleService.GetOneAsync(id);

                if (article == null)
                    return NotFound(ValidationError.CreateError($"Article with Id = {id} not found."));

                await _articleService.DeleteOneAsync(id);
                await _articleService.SaveAsync();

                return NoContent();
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ValidationError.CreateError(ex.Message));
            }
        }

    }
}
