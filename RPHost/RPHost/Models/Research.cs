﻿using System;
using System.Collections.Generic;

namespace RPHost.Models
{
    public class Research
    {
        public int ResearchId { get; set; }
        public ICollection<AuthorResearch> AuthorResearches { get; set; }

        private string title;
        private DateTime reasearchDate;
        private int upvote;
        private int downvote;
        private string review;


        public string Title {
            get {
                return this.title;
            }
            set {
                this.title = value;
            }
        }

        public DateTime ResearchDate {
            get {
                return this.reasearchDate;
            }
            set {
                this.reasearchDate = value;
            }
        }

        public int Upvote
        {
            get
            {
                return this.upvote;
            }
            set
            {
                this.upvote = value;
            }
        }

        public int Downvote
        {
            get
            {
                return this.downvote;
            }
            set
            {
                this.downvote = value;
            }
        }

        public string Review
        {
            get
            {
                return this.review;
            }
            set
            {
                this.review = value;
            }
        }
    }
}