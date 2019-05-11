﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Models
{

    public enum MovieGenre
    {
        action,
        comedy,
        horror,
        thriller
    }

    public enum MovieWatched
    {
        yes,
        no
    }

    public class Movie
    {
        //[Key()]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [EnumDataType(typeof(MovieGenre))]
        public MovieGenre MovieGenre { get; set; }
        public int Duration { get; set; }
        public int YearOfRelease { get; set; }
        public string Director { get; set; }
        public DateTime DateAdded { get; set; }
        [Range(1, 10)]
        public int Rating { get; set; }
        [EnumDataType(typeof(MovieWatched))]
        public MovieWatched MovieWatched { get; set; }

        public List<Comment> Comments { get; set; }


    }
}
