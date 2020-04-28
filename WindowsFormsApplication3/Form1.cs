using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        String filePath = @"C:\Users\X\Documents\Visual Studio 2013\Projects\WindowsFormsApplication3\test2.txt";
        String filePath2 = @"C:\Users\X\Documents\Visual Studio 2013\Projects\WindowsFormsApplication3\test2.txt";
        DataTable table;
        List<Movie> movies = new List<Movie>();
        int br = 0;

        public Form1()
        {
            InitializeComponent();
            table = new DataTable();
            table.Columns.Add("id", typeof(int));
            table.Columns.Add("Movie", typeof(string));
            table.Columns.Add("Actors", typeof(string));
            table.Columns.Add("Genre", typeof(string));
            table.Columns.Add("Date or Release", typeof(string));
            table.Columns.Add("Rating", typeof(double));         
            dataGridView1.DataSource = table;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                load();
            }
            catch
            {

            }

        }

        void load()
        {
                List<String> lines = File.ReadAllLines(filePath).ToList();
                foreach (String line in lines)
                {
                    String[] entries = line.Split('*');                  
                    movies.Add(new Movie(int.Parse(entries[0]), entries[1], entries[2], entries[3], entries[4], double.Parse(entries[5])));
                    table.Rows.Add(int.Parse(entries[0]), entries[1], entries[2], entries[3], entries[4], double.Parse(entries[5]));      
                }

               idSort();
               displayItems();
        }

        void searchByActors()
        {
            table.Clear();
            String act_name = textBox6.Text;
            String[] entries = act_name.Split(',');
            Boolean flag = false;

            int count = 0;
            int count2 = 0;
            try
            {
                if (act_name.Length <= 14)
                {
                    for (int i = 0; i < movies.Count; i++)
                    {
                        Boolean isPresent = movies[i].getActors().IndexOf(entries[0]) != -1 ? true : false;
                        if (isPresent)
                        {
                            count++;
                            count2++;
                            for (int j = 0; j < count; j++)
                            {
                                table.Rows.Add(count, movies[i].getName(), movies[i].getActors(), movies[i].getGenre(), movies[i].getDate(), movies[i].getRating());
                                if (count == count2)
                                {
                                    break;
                                }
                            }

                            flag = true;
                        }
                    }

                    if (flag == false)
                    {
                        MessageBox.Show("Actor with that is name can't find in the database!", "ERROR", MessageBoxButtons.OK);
                        displayItems();
                    }
                }
                else if (act_name.Length > 14 && act_name.Length<=28)
                {
                    for (int i = 0; i < movies.Count; i++)
                    {
                        Boolean isPresent = movies[i].getActors().IndexOf(entries[0]) != -1 ? true : false;
                        Boolean isPresent2 = movies[i].getActors().IndexOf(entries[1]) != -1 ? true : false;
                        if (isPresent&&isPresent2)
                        {
                            count++;
                            count2++;
                            for (int j = 0; j < count; j++)
                            {
                                table.Rows.Add(count, movies[i].getName(), movies[i].getActors(), movies[i].getGenre(), movies[i].getDate(), movies[i].getRating());
                                if (count == count2)
                                {
                                    break;
                                }
                            }

                            flag = true;
                        }
                    }

                    if (flag == false)
                    {
                        MessageBox.Show("Actor with that is name can't find in the database!", "ERROR", MessageBoxButtons.OK);
                        displayItems();
                    }
                }
                else if (act_name.Length > 28)
                {
                    for (int i = 0; i < movies.Count; i++)
                    {
                        Boolean isPresent = movies[i].getActors().IndexOf(entries[0]) != -1 ? true : false;
                        Boolean isPresent2 = movies[i].getActors().IndexOf(entries[1]) != -1 ? true : false;
                        Boolean isPresent3 = movies[i].getActors().IndexOf(entries[2]) != -1 ? true : false;
                        if (isPresent && isPresent2 && isPresent3)
                        {
                            count++;
                            count2++;
                            for (int j = 0; j < count; j++)
                            {
                                table.Rows.Add(count, movies[i].getName(), movies[i].getActors(), movies[i].getGenre(), movies[i].getDate(), movies[i].getRating());
                                if (count == count2)
                                {
                                    break;
                                }
                            }

                            flag = true;
                        }
                    }

                    if (flag == false)
                    {
                        MessageBox.Show("Actor with that is name can't find in the database!", "ERROR", MessageBoxButtons.OK);
                        displayItems();
                    }
                }
            }
            catch
            {

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.Text=="Movie")
            {
                searchByMovie();
            }else if(comboBox1.Text=="Actors")
            {
                 searchByActors();
            }
            else if (comboBox1.Text == "Genre")
            {
                 //searchByGenre();
                testSearchGenre();
            }
            else if(comboBox1.Text=="Date of Release")
            {
                if (textBox6.Text == "")
                {
                    sortDate();
                    displayItems();
                }
                else
                {
                     searchByDate();  
                }
                                 
            }
            else if (comboBox1.Text == "Rating")
            {
                if(textBox6.Text=="")
                {
                    ratingSort();
                    displayItems();
                }else
                {
                    searchByRating();
                }
                
            }
            
        }

      
        void addMovie()
        {
            String m_name = textBox1.Text;
            String m_actors = textBox2.Text;            
            String m_date = textBox4.Text;
            double m_rating = double.Parse(textBox5.Text);
            String m_genre = "";


                if (movieProvider()&&genreProvider()&&dateProvider(m_date) && ratingProvider())
                {
                    br = movies.Count();
                    br++;
                    
                    if (comboBox2.SelectedIndex != -1 && comboBox3.SelectedIndex != -1 && comboBox4.SelectedIndex != -1)
                    {
                        m_genre = comboBox2.SelectedItem.ToString() + "," + comboBox3.SelectedItem.ToString() + "," + comboBox4.SelectedItem.ToString();
                    }
                    else if (comboBox2.SelectedIndex != -1 && comboBox3.SelectedIndex != -1)
                    {
                        m_genre = comboBox2.SelectedItem.ToString() + "," + comboBox3.SelectedItem.ToString();
                    }
                    else if (comboBox2.SelectedIndex != -1)
                    {
                        m_genre = comboBox2.SelectedItem.ToString();
                    }

                    
                    movies.Add(new Movie(br, m_name, m_actors, m_genre, m_date, m_rating));

                    idSort();

                    displayItems();

                    List<String> output = new List<String>();

                    foreach (Movie a in movies)
                    {
                        output.Add(a.getID() + "*" + a.getName() + "*" + a.getActors() + "*" + a.getGenre() + "*" + a.getDate() + "*" + a.getRating());
                    }

                    File.WriteAllLines(filePath2, output);

                    MessageBox.Show("Successfully added", "", MessageBoxButtons.OK);

                    textBox1.Clear();
                    textBox2.Clear();
                    comboBox2.Text = "";
                    comboBox3.Text = "";
                    comboBox4.Text = "";
                    textBox4.Clear();
                    textBox5.Clear();
                }
            

        }

        void idSort()
        {
            movies.Sort(delegate(Movie a, Movie b)
            {
                int x = b.getID().CompareTo(a.getID());
                return x;
            });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                addMovie();
            }
            catch
            {

            }
        }

        void displayItems()
        {
            table.Rows.Clear();
            textBox6.Clear();
            int br = 0;

            for (int i = 0; i < movies.Count; i++)
            {
                br++;               
                table.Rows.Add(br, movies[i].getName(),movies[i].getActors(),movies[i].getGenre(),movies[i].getDate(),movies[i].getRating());
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                idSort();
                displayItems();
            }
            catch
            {

            }
        }

        void searchByRating()
        {
            table.Clear();
            try
            {
                double m_rating = Double.Parse(textBox6.Text);
                String rating = m_rating.ToString();
                Boolean flag = false;

                ratingSort();

                int count = 0;
                int count2 = 0;

                for (int i = 0; i < movies.Count; i++)
                {
                    Boolean isPresent = movies[i].getRating().ToString().Substring(0, 1).IndexOf(rating) != -1 ? true : false;
                    if (isPresent)
                    {
                        count++;
                        count2++;
                        for (int j = 0; j < count; j++)
                        {
                           
                            table.Rows.Add(count, movies[i].getName(), movies[i].getActors(), movies[i].getGenre(), movies[i].getDate(), movies[i].getRating());
                            if (count == count2)
                            {
                                break;
                            }
                        }
                        flag = true;
                    }

                }

                if (flag == false)
                {
                    MessageBox.Show("Can't finded is movie with such rating!", "", MessageBoxButtons.OK);
                    displayItems();
                }
            }
            catch
            {

            }

        }

        void ratingSort()
        {
            movies.Sort(delegate(Movie a, Movie b)
            {
                int x = b.getRating().CompareTo(a.getRating());
                return x;
            });
        }

        void searchByDate()
        {
            String m_date = textBox6.Text;
            Boolean flag = false;
            table.Clear();
            int count = 0;
            int count2 = 0;
            sortDate();
            for (int i = 0; i < movies.Count; i++)
            {
                Boolean isPresent = movies[i].getDate().IndexOf(m_date) != -1 ? true : false;
                if (isPresent)
                {
                    
                    count++;
                    count2++;
                    for (int j = 0; j < count; j++)
                    {                       
                        table.Rows.Add(count, movies[i].getName(), movies[i].getActors(), movies[i].getGenre(), movies[i].getDate(), movies[i].getRating());
                        if (count == count2)
                        {
                            break;
                        }
                    }

                    flag = true;
                }
            }

            if (flag == false)
            {
                MessageBox.Show("Movie with such date can't find in the database!", "ERROR", MessageBoxButtons.OK);
                displayItems();
            }

        }

        void searchByGenre()
        {
            String m_genre = textBox6.Text;
            Boolean flag = false;
            table.Clear();
            int count = 0;
            int count2 = 0;

            for (int i = 0; i < movies.Count; i++)
            {
                Boolean isPresent = movies[i].getGenre().IndexOf(m_genre) != -1 ? true : false;
                if (isPresent)
                {
                   
                    count++;
                    count2++;
                    for (int j = 0; j < count; j++)
                    {
                        table.Rows.Add(count, movies[i].getName(), movies[i].getActors(), movies[i].getGenre(), movies[i].getDate(), movies[i].getRating());
                        if (count == count2)
                        {
                            break;
                        }
                    }

                    flag = true;
                }
            }

            if (flag == false)
            {
                MessageBox.Show("Movie with such genre not to exist!", "ERROR", MessageBoxButtons.OK);
                displayItems();
            }

        }

        void searchByMovie()
        {
            String m_movie = textBox6.Text;
            Boolean flag = false;
            table.Clear();
            int count = 0;
            int count2 = 0;

            for (int i = 0; i < movies.Count; i++)
            {
                Boolean isPresent = movies[i].getName().IndexOf(m_movie) != -1 ? true : false;
                if (isPresent)
                {

                    count++;
                    count2++;
                    for (int j = 0; j < count; j++)
                    {
                        table.Rows.Add(count, movies[i].getName(), movies[i].getActors(), movies[i].getGenre(), movies[i].getDate(), movies[i].getRating());
                        if (count == count2)
                        {
                            break;
                        }
                    }

                    flag = true;
                }
            }

            if (flag == false)
            {
                MessageBox.Show("Movie with that name not to exist!", "ERROR", MessageBoxButtons.OK);
                displayItems();
            }

        }

        bool ratingProvider()
        {
            bool flag = true;
            double rating = double.Parse(textBox5.Text);
            if (rating > 10 || rating < 1)
            {
                flag = false;
                textBox5.Clear();
                MessageBox.Show("Inccorect rating input", "ERROR", MessageBoxButtons.OK);
            }else
            {
                flag = true;
            }
            return flag;

        }

        bool movieProvider()
        {
            bool flag = true;
            String m_name = textBox1.Text;
            if (textBox1.Text.Length!=0)
            {
                foreach (var search in movies)
                {
                    if (search.getName().Equals(m_name))
                    {
                        flag = false;
                        textBox1.Clear();
                        MessageBox.Show("The movie name which you are to input is already exist!", "ERROR", MessageBoxButtons.OK);
                        break;
                    }
                    else
                    {
                        flag = true;
                    }
                }
            }
            else
            {
                MessageBox.Show("Please insert movie name!", "Warning", MessageBoxButtons.OK);
                flag = false;
            }
            return flag;
        }

        bool dateProvider(String date)
        {
            try
            {
                DateTime dt = DateTime.Parse(date);
                return true;
            }
            catch
            {
                MessageBox.Show("Incorrect format please try again!", "ERROR", MessageBoxButtons.OK);
                textBox4.Clear();
                return false;

            }
        }

        bool genreProvider()
        {
            if (comboBox2.SelectedIndex != -1 && comboBox3.SelectedIndex != -1 && comboBox4.SelectedIndex != -1)
            {
                return true;
            }
            else if (comboBox2.SelectedIndex != -1 && comboBox3.SelectedIndex != -1)
            {
                return true;
            }
            else if (comboBox2.SelectedIndex != -1)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Please select Genre!", "Warning", MessageBoxButtons.OK);
                return false;
            }
           
        }

        private void textBox6_Click(object sender, EventArgs e)
        {
            textBox6.Clear();
        }

        void sortDate()
        {
                movies = movies.OrderByDescending(i => DateTime.Parse(i.getDate())).ToList();
        }

        void testSearchGenre()
        {
            String search = textBox6.Text;
            String[] entries = search.Split(',');

            Boolean flag = false;
            table.Clear();
            int count = 0;
            int count2 = 0;
            try
            {
                if (textBox6.Text.Length != 0)
                {
                    for (int i = 0; i < movies.Count; i++)
                    {
                        Boolean isPresent = movies[i].getGenre().IndexOf(entries[0]) != -1 ? true : false;
                        if (isPresent)
                        {
                           
                            count++;
                            count2++;
                            for (int j = 0; j < count; j++)
                            {
                                ratingSort();
                                table.Rows.Add(count, movies[i].getName(), movies[i].getActors(), movies[i].getGenre(), movies[i].getDate(), movies[i].getRating());
                                if (count == count2)
                                {
                                    break;
                                }
                            }

                            flag = true;
                        }
                    }

                    if (flag == false)
                    {
                        MessageBox.Show("Movie with such genre not to exist!", "ERROR", MessageBoxButtons.OK);
                        displayItems();
                    }
                }
                else  if (textBox6.Text.Length >5 && textBox6.Text.Length <=14)
                {
                    for (int i = 0; i < movies.Count; i++)
                    {
                        Boolean isPresent = movies[i].getGenre().IndexOf(entries[0]) != -1 ? true : false;
                        Boolean isPresent2 = movies[i].getGenre().IndexOf(entries[1]) != -1 ? true : false;
                        if (isPresent&&isPresent2)
                        {

                            count++;
                            count2++;
                            for (int j = 0; j < count; j++)
                            {
                                ratingSort();
                                table.Rows.Add(count, movies[i].getName(), movies[i].getActors(), movies[i].getGenre(), movies[i].getDate(), movies[i].getRating());
                                if (count == count2)
                                {
                                    break;
                                }
                            }

                            flag = true;
                        }
                    }

                    if (flag == false)
                    {
                        MessageBox.Show("Movie with such genre not to exist!", "ERROR", MessageBoxButtons.OK);
                        displayItems();
                    }
                }
                else if (textBox6.Text.Length > 14)
                {
                    for (int i = 0; i < movies.Count; i++)
                    {
                        Boolean isPresent = movies[i].getGenre().IndexOf(entries[0]) != -1 ? true : false;
                        Boolean isPresent2 = movies[i].getGenre().IndexOf(entries[1]) != -1 ? true : false;
                        Boolean isPresent3 = movies[i].getGenre().IndexOf(entries[2]) != -1 ? true : false;
                        if (isPresent && isPresent2 && isPresent3)
                        {

                            count++;
                            count2++;
                            for (int j = 0; j < count; j++)
                            {
                                ratingSort();
                                table.Rows.Add(count, movies[i].getName(), movies[i].getActors(), movies[i].getGenre(), movies[i].getDate(), movies[i].getRating());
                                if (count == count2)
                                {
                                    break;
                                }
                            }

                            flag = true;
                        }
                    }

                    if (flag == false)
                    {
                        MessageBox.Show("Movie with such genre not to exist!", "ERROR", MessageBoxButtons.OK);
                        displayItems();
                    }
                }
            }
            catch
            {

            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

              testSearchGenre();
        }
    }
}
