using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public interface IComment
    {
        int Id { get;set; }
        int PostId { get;set; }
        string Name { get;set; }
        string Email { get;set; }
        string Body { get;set; }
        DateTime Time { get;set; }
    }
}