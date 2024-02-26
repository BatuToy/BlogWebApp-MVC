using AppBlog.Entity;
using Microsoft.AspNetCore.Mvc;

namespace AppBlog.Data.Abstract;

public interface ICommentRepository 
{
    IQueryable<Comment> Comments { get; }    
    
    //Task<Comment> CreateComment([FromBody] Comment comment);

    void CreateComment(Comment comment); 
}