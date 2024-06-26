﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;

namespace AppBlog.Entity;

public class Post
{
 
    public int PostId { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? Image { get; set; }
    public DateTime PublishedOn { get; set; }
    public bool IsActive { get; set; }


    public int UserId { get; set; }
    public User? User { get; set; } 

    public List<Comment> Comments{ get; set; } = new List<Comment>();
    public List<Tag> Tags { get; set; } = new List<Tag>();





}
