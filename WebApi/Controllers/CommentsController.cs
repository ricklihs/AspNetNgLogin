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
        private readonly AppSettings _appSettings;

        // 2)
        public CommentsController(
            ICommentService commentService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _commentService = commentService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        // 3) GetAll
        // [HttpGet("{comments}")]
        [HttpGet]
        public IActionResult GetAll()
        {
           IEnumerable<IComment> comments =  _commentService.GetAll();
           
            foreach (IComment item in comments)
            {
             
                item.Name=item.Name.Trim();
                item.Body=item.Body.Trim();
                item.Email=item.Email.Trim();           
            }
            var commentDtos = _mapper.Map<IList<CommentDto>>(comments);
            return Ok(commentDtos);
         
        }
        
        // 6)
        // [HttpGet("comments/{id}")]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var comment =  _commentService.GetById(id);
            var commentDto = _mapper.Map<CommentDto>(comment);
            commentDto.Name=commentDto.Name.Trim();
            commentDto.Body=commentDto.Body.Trim();
            commentDto.Email=commentDto.Email.Trim(); 
            return Ok(commentDto);
        }

        // Comments ?????????
        // [HttpGet("comments/bypost/{postId}")]
        [HttpGet("bypost/{postId}")]
        public IActionResult GetByPostId(int postId)
        {
            var comments =  _commentService.GetByPostId(postId);
            List<CommentDto> commentDtoList=new List<CommentDto>();
            
            // var commentDto = _mapper.Map<UserDto>(comments);
            foreach (Comment comment in comments)
            {
                var commentDto = _mapper.Map<CommentDto>(comment);
                    commentDtoList.Add(commentDto);
            }
            
            return Ok(commentDtoList);
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

        public IComment CommentTrim(IComment item){
            
                item.Name=item.Name.Trim();
                item.Body=item.Body.Trim();
                item.Email=item.Email.Trim();
            return item;
        }
        
    }
}
