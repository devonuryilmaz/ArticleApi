using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Repositories;
using DataAccess.Entities;
using DataAccess.Services;

namespace ArticleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var resultData = await _articleService.GetAll();

            if (resultData == null)
                return NotFound();

            return Ok(resultData);
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get(int id)
        {
            var resultData = await _articleService.Get(id);

            if (resultData == null)
                return NotFound();

            return Ok(resultData);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(Article article)
        {
            var resp = await _articleService.Insert(article);
            if (!resp)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var resp = await _articleService.Delete(id);
            if (!resp)
                return BadRequest();

            return Ok();
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(Article article)
        {
            var resp = await _articleService.Update(article);
            if (!resp)
                return BadRequest();

            return Ok();
        }

    }
}