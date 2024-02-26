using AppBlog.Data.Abstract;
using AppBlog.Data.Concrete.EfCore;
using AppBlog.Entity;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AppBlog.Data.Concrete;

public class EfCommentRepository : ICommentRepository
{
    private readonly DataContext _context;
    public EfCommentRepository(DataContext context)
    {
        _context = context;
    }
    public IQueryable<Comment> Comments => _context.Comments;

    public void CreateComment(Comment comment)
    {
        _context.Comments.Add(comment);
        _context.SaveChanges();
    }






    //public async Task<Comment> CreateComment([FromBody] Comment comment)
    //{
    //    await _context.Comments.AddAsync(comment);
    //    await _context.SaveChangesAsync();
    //    return (comment);
    //}
}