using RPHost.Models;
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPHost.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        private string firstName;
        private string lastName;
        private string address;
        private string city;
        private string state;
        private string country;
        private string email;
        private int phone;
        private char gender;

        ICollection<AuthorResearch> AuthorResearches { get; set; }


        public string FirstName {
            get 
            {
                return this.firstName;
            }

            set {

                this.firstName = value;
            }
        }

        public string LastName {
            get {
                return this.lastName;
            }

            set {
                this.lastName = value;
            }
        }

        public string Address {
            get {
                return this.address;
            }

            set {
                this.address = value;
            }
        }

        public string City {
            get {
                return this.city;
            }
            set {
                this.city = value;
            }
        }

        public string State {
            get {
                return this.state;
            }
            set {
                this.state = value;
            }
        }

        public string Country {
            get {
                return this.country;
            }
            set {
                this.country = value;
            }
        }

        public string Email {
            get {
                return this.email;
            }

            set {
                this.email = value;
            }
        }

        public int Phone {
            get {
                return this.phone;
            }

            set {
                this.phone = value;
            }
        }

        public char Gender {
            get {
                return this.gender;
            }

            set {
                this.gender = value;
            }
        }
    }
}