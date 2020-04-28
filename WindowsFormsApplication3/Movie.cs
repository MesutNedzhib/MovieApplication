using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication3
{
    class Movie
    {
        private int int_id;
        private String str_name;
        private String str_actors;
        private String str_genre;
        private String str_date_release;
        private double db_rating;

        public Movie() { }

        public Movie(int id, String name, String actors, String genre, String date, double rating)
        {
            this.int_id = id;
            this.str_name = name;
            this.str_actors = actors;
            this.str_genre = genre;
            this.str_date_release = date;
            this.db_rating = rating;
        }

        public int getID()
        {
            return this.int_id;
        }

        public String getName()
        {
            return this.str_name;
        }

        public String getActors()
        {
            return this.str_actors;
        }

        public String getGenre()
        {
            return this.str_genre;
        }

        public String getDate()
        {
            return this.str_date_release;
        }

        public double getRating()
        {
            return this.db_rating;
        }

        public void setID(int id)
        {
            this.int_id = id;
        }

        public void setName(String name){
            this.str_name=name;
        }

        public void setActors(String actors)
        {
            this.str_actors=actors;
        }

        public void setGenre(String genre)
        {
            this.str_genre=genre;
        }

        public void setDate(String date)
        {
            this.str_date_release=date;
        }

        public void setRting(double rating)
        {
            this.db_rating=rating;
        }





    }
}
