﻿namespace JokesOnYou.Web.Api.DTOs
{
    public class TagReplyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public string OwnerId { get; set; }
        public int Likes { get; set; }
    }
}
