using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public interface ICommentService
    {
        // Comment Authenticate(string username, string password);
         IEnumerable<Comment> GetAll();       
         Comment GetById(int Id);
         IEnumerable<Comment> GetByPostId(int PostId);
         Comment Create(int postId,string username,string email,string body);// default time
         void Update(Comment comment);
         void Delete(int id);
    }

    public class CommentService : ICommentService
    {
        private DataContext _context;

        public CommentService(DataContext context){
            _context = context;
        }

        // 0)
        // public Comment Authenticate(string username, string password)
        // {
        //     if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)){
        //         return null;
        //     }

        //     var user = _context.Users.SingleOrDefault(x => x.Username == username);

        //     // check if username exists
        //     if (user == null) return null;

        //     // check if password is correct
        //     if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
        //         return null;

        //     return user;
        // }

        // 1)
        public IEnumerable<Comment> GetAll()
        {
            return _context.Comments;
        }
        // 2)
        public Comment GetById(int id)
        {
            return _context.Comments.Find(id);
        }

        // 3) GetByPostId
        public IEnumerable<Comment> GetByPostId(int postId)
        {
            return _context.Comments.Where(x=>x.PostId==postId);
        }

        // 4) Create
        // public Comment Create(int PostId,string username,string email,string body)
          
        public Comment Create(int postId,string username,string email,string body)
        {
           // validation  maybe use user.Username
           if (string.IsNullOrWhiteSpace(username))
                throw new AppException("Password is required");

            // Do I need check PostId ??
            // if (_context.Comments.Any(x => x.PostId == comment.PostId))
            //     throw new AppException("PostId: " + comment.PostId + " is already taken");
            Comment comment=new Comment();
            comment.Name=username;
            comment.Email=email;
            comment.PostId=postId;
            comment.Body=body;
            
            _context.Comments.Add(comment);
            _context.SaveChanges();

            return comment;
        }

        // 5) Update Comment (int id,int postId,string username,string email,string body)
        public void Update(Comment commentParam)
        {
            var comment = _context.Comments.Find(commentParam.Id);

            if (comment == null)
                throw new AppException("Post not found");        

            // update user properties
            // comment.Name = commentParam.Name; //update should not allow change name

            comment=new Comment{
                Email=commentParam.Email,
                Body = commentParam.Body,
                Time = commentParam.Time
            };

            // comment.Email = commentParam.Email;
            // comment.Body = commentParam.Body;
            // comment.Time = commentParam.Time;

            _context.Comments.Update(comment);
            _context.SaveChanges();
            
        }

        // 6) Delete
        public void Delete(int id)
        {
            var comment = _context.Comments.Find(id);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                _context.SaveChanges();
            }
        }

    }
    
}
