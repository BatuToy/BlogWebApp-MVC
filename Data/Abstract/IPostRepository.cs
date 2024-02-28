﻿using AppBlog.Data.Concrete.EfCore;
using AppBlog.Entity;
using Microsoft.AspNetCore.Mvc;

namespace AppBlog.Data.Abstract;

public interface IPostRepository 
{
    IQueryable<Post> Posts { get; }
    Task<Post> CreatePost([FromBody]Post post);
    void UpdatePost([FromBody] Post post , int [] tagIds);
    Task<Post> GetById(string url);
    Task<Post> GetById(int id);

    
}
