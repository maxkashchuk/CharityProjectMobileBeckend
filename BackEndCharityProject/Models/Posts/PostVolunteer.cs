﻿using BackEndCharityProject.Models.Posts.Additional;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEndCharityProject.Models.Posts
{
    public class PostVolunteer
    {
        public int Id { get; set; }
        public int Goal { get; set; }
        public int Donated { get; set; }
        public string Header { get; set; }
        public ICollection<Image> Images { get; set; }
        public string Description { get; set; }
        public double? Lattitude { get; set; }
        public double? Longtitude { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
