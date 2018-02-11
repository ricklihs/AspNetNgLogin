using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

using WebApi.Services;
using WebApi.Dtos;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Controllers
{
   // [Authorize]
    [Route("[controller]")]
    public class CommentsController : Controller
    {
        // 1)
        private ICommentService _commentService;
        // private IUserService _userService;
        private IMapper _mapper;
        //private readonly AppSettings _appSettings;

        // 2)
        public CommentsController(
            ICommentService commentService,
            IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        // 3) GetAll
        // [HttpGet("{comments}")]
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<IComment> comments =  _commentService.GetAll();             
            var commentDtos = _mapper.Map<IList<CommentDto>>(comments);
            return Ok(commentDtos);     
        }
        
        // 6)
        // [HttpGet("comments/{id}")]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            IComment comment =  _commentService.GetById(id);
            IComment commentDto = _mapper.Map<CommentDto>(comment);
            return Ok(commentDto);
        }

        // comments/bypost/3
        [HttpGet("bypost/{postId}")]
        public IActionResult GetByPostId(int postId)
        {
            IEnumerable<IComment> comments =  _commentService.GetByPostId(postId);
     
            var commentDtos = _mapper.Map<IList<CommentDto>>(comments);
            return Ok(commentDtos);
        }
        // return all bypostId // comments/bypost
        [HttpGet("bypost")]
        public IActionResult GetAllOrderByPostId()
        {
            IEnumerable<IComment> comments =  _commentService.GetAll().OrderBy(comment=>comment.PostId);    
            var commentDtos = _mapper.Map<IList<CommentDto>>(comments);
            return Ok(commentDtos);
        }



        // 7) Update
        // [HttpPut("comments/{id}")]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]CommentDto commentDto)
        {
            // map dto to entity and set id
            var comment = _mapper.Map<Comment>(commentDto);
            comment.Id = id;

            try
            {
                // save
                _commentService.Update(comment);
                return Ok();
            }
            catch(AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(ex.Message);
            }
        }

        // 8)
        // [HttpDelete("comments/{id}")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _commentService.Delete(id);
            return Ok();
        }

        // 4)
        [AllowAnonymous]
        [HttpPost] 
        // public IComment Create(int postId,string username,string email,string body)
        public IActionResult Create([FromBody]CommentDto commentDto)
        {
            // map dto to entity
            var comment = _mapper.Map<Comment>(commentDto);

            try
            {
                // save
                _commentService.Create(commentDto.PostId, commentDto.Name,commentDto.Email,commentDto.Body);
                return Ok();
            }
            catch(AppException ex)
            {
                // return error message if there was an exception 
                return BadRequest(ex.Message);
            }
        }

    }
}
