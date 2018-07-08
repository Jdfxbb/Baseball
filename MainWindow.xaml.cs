using System;
using System.Collections.Generic;
using Xceed.Wpf.Toolkit;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;


namespace Baseball
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DBConnect connect = new DBConnect();
            Application.Current.Properties["SQLConnection"] = connect;

            List<List<Team>> teams = new List<List<Team>>();
            Application.Current.Properties["TeamList"] = teams;

            List<string> teamNames = new List<string>();
            Application.Current.Properties["TeamNames"] = teamNames;
        }

        private void createButton()
        {
            Button button = new Button();
            button.Name = "button";
            layout.RegisterName(button.Name, button);
            button.HorizontalAlignment = HorizontalAlignment.Center;
            button.VerticalAlignment = VerticalAlignment.Center;
            button.Width = 75;
            button.Content = "This";
            button.Click += new RoutedEventHandler(button_click);
            Grid.SetColumn(button, 0);
            Grid.SetRow(button, 1);
            layout.Children.Add(button);
        }

        private void button_click(object sender, RoutedEventArgs e)
        {
        }

        private void NewGameClick(object sender, RoutedEventArgs e)
        {
            using (new WaitCursor())
            {
                layout.Children.Clear();
                DBConnect instance = new DBConnect();

                List<string> names = (List<string>)Application.Current.Properties["TeamNames"];
                List<string> cities = (List<string>)Application.Current.Properties["Cities"];
                List<List<Team>> teams = (List<List<Team>>)Application.Current.Properties["TeamList"];

                names = instance.getTeams();
                teams = instance.generateTeams();
                cities = instance.getCities();

                Application.Current.Properties["TeamNames"] = names;
                Application.Current.Properties["TeamList"] = teams;

                ComboBox nameCombo = new ComboBox();
                nameCombo.Height = 20;
                nameCombo.Width = 120;
                nameCombo.Margin = new Thickness(20);
                nameCombo.IsTextSearchEnabled = true;
                nameCombo.HorizontalAlignment = HorizontalAlignment.Center;
                nameCombo.ItemsSource = names;
                nameCombo.SelectedItem = names[0];
                Application.Current.Properties["nameCombo"] = nameCombo;
                layout.Children.Add(nameCombo);

                ComboBox cityCombo = new ComboBox();
                cityCombo.Height = 20;
                cityCombo.Width = 200;
                cityCombo.Margin = new Thickness(20);
                cityCombo.IsTextSearchEnabled = true;
                cityCombo.HorizontalAlignment = HorizontalAlignment.Left;
                cityCombo.ItemsSource = cities;
                cityCombo.SelectedItem = cities[0];
                Application.Current.Properties["cityCombo"] = cityCombo;
                layout.Children.Add(cityCombo);

                Button submitTeamSelect = new Button();
                submitTeamSelect.Height = 20;
                submitTeamSelect.Width = 100;
                submitTeamSelect.Margin = new Thickness(20);
                submitTeamSelect.HorizontalAlignment = HorizontalAlignment.Right;
                submitTeamSelect.Content = "Submit";
                submitTeamSelect.Click += new RoutedEventHandler(submitTeamClick);
                layout.Children.Add(submitTeamSelect);

                

            }

        }

        private void submitTeamClick(object sender, RoutedEventArgs e)
        {
            
            ComboBox cityCombo = (ComboBox)Application.Current.Properties["cityCombo"];
            ComboBox nameCombo = (ComboBox)Application.Current.Properties["nameCombo"];
            string city = cityCombo.SelectedItem.ToString().Split(',')[0];
            string state = cityCombo.SelectedItem.ToString().Split(',')[1];
            string region = cityCombo.SelectedItem.ToString().Split(',')[2];
            string name = nameCombo.SelectedItem.ToString();
            UserTeam user = new UserTeam(name, city, state, region);
            Application.Current.Properties["user"] = user;

            TextBox txt = new TextBox();
            txt.Height = 20;
            txt.Width = 200;
            txt.HorizontalAlignment = HorizontalAlignment.Center;
            txt.VerticalAlignment = VerticalAlignment.Top;
            txt.Margin = new Thickness(20);
            txt.Text = city + " " + name;
            Grid.SetRow(txt, 0);
            Grid.SetColumn(txt, 1);
            layout.Children.Add(txt);

            IntegerUpDown batting = new IntegerUpDown();
            batting.Height = 20;
            batting.Width = 50;
            batting.DefaultValue = 400;
            batting.Minimum = 400;
            batting.Maximum = 900;
            batting.HorizontalAlignment = HorizontalAlignment.Left;
            batting.VerticalAlignment = VerticalAlignment.Center;
            batting.Margin = new Thickness(20);
            Grid.SetRow(batting, 0);
            Grid.SetColumn(batting, 1);
            layout.Children.Add(batting);

        }

       private void createBoxScore()
        {
            Grid grid = new Grid();
            grid.ShowGridLines = true;
            grid.Height = 75;
            grid.Width = 580;
            grid.HorizontalAlignment = HorizontalAlignment.Center;
            grid.VerticalAlignment = VerticalAlignment.Center;
            grid.Background = new SolidColorBrush(Colors.LightSlateGray);
            

            ColumnDefinition name = new ColumnDefinition();
            ColumnDefinition first = new ColumnDefinition();
            ColumnDefinition second = new ColumnDefinition();
            ColumnDefinition third = new ColumnDefinition();
            ColumnDefinition fourth = new ColumnDefinition();
            ColumnDefinition fifth = new ColumnDefinition();
            ColumnDefinition sixth = new ColumnDefinition();
            ColumnDefinition seventh = new ColumnDefinition();
            ColumnDefinition eighth = new ColumnDefinition();
            ColumnDefinition ninth = new ColumnDefinition();
            ColumnDefinition runs = new ColumnDefinition();
            ColumnDefinition hits = new ColumnDefinition();
            ColumnDefinition errors = new ColumnDefinition();

            name.Width = new GridLength(220);
            first.Width = new GridLength(30);
            second.Width = new GridLength(30);
            third.Width = new GridLength(30);
            fourth.Width = new GridLength(30);
            fifth.Width = new GridLength(30);
            sixth.Width = new GridLength(30);
            seventh.Width = new GridLength(30);
            eighth.Width = new GridLength(30);
            ninth.Width = new GridLength(30);
            runs.Width = new GridLength(30);
            hits.Width = new GridLength(30);
            errors.Width = new GridLength(30);


            grid.ColumnDefinitions.Add(name);
            grid.ColumnDefinitions.Add(first);
            grid.ColumnDefinitions.Add(second);
            grid.ColumnDefinitions.Add(third);
            grid.ColumnDefinitions.Add(fourth);
            grid.ColumnDefinitions.Add(fifth);
            grid.ColumnDefinitions.Add(sixth);
            grid.ColumnDefinitions.Add(seventh);
            grid.ColumnDefinitions.Add(eighth);
            grid.ColumnDefinitions.Add(ninth);
            grid.ColumnDefinitions.Add(runs);
            grid.ColumnDefinitions.Add(hits);
            grid.ColumnDefinitions.Add(errors);

            RowDefinition labels = new RowDefinition();
            RowDefinition away = new RowDefinition();
            RowDefinition home = new RowDefinition();

            grid.RowDefinitions.Add(labels);
            grid.RowDefinitions.Add(away);
            grid.RowDefinitions.Add(home);

            DBConnect connect = new DBConnect();

            TextBlock t00 = new TextBlock();
            t00.HorizontalAlignment = HorizontalAlignment.Center;
            t00.Text = "Game";
            Grid.SetRow(t00, 0);
            Grid.SetColumn(t00, 0);

            TextBlock t01 = new TextBlock();
            t01.HorizontalAlignment = HorizontalAlignment.Center;
            t01.Text = "1";
            Grid.SetRow(t01, 0);
            Grid.SetColumn(t01, 1);

            TextBlock t02 = new TextBlock();
            t02.HorizontalAlignment = HorizontalAlignment.Center;
            t02.Text = "2";
            Grid.SetRow(t02, 0);
            Grid.SetColumn(t02, 2);

            TextBlock t03 = new TextBlock();
            t03.HorizontalAlignment = HorizontalAlignment.Center;
            t03.Text = "3";
            Grid.SetRow(t03, 0);
            Grid.SetColumn(t03, 3);

            TextBlock t04 = new TextBlock();
            t04.HorizontalAlignment = HorizontalAlignment.Center;
            t04.Text = "4";
            Grid.SetRow(t04, 0);
            Grid.SetColumn(t04, 4);

            TextBlock t05 = new TextBlock();
            t05.HorizontalAlignment = HorizontalAlignment.Center;
            t05.Text = "5";
            Grid.SetRow(t05, 0);
            Grid.SetColumn(t05, 5);

            TextBlock t06 = new TextBlock();
            t06.HorizontalAlignment = HorizontalAlignment.Center;
            t06.Text = "6";
            Grid.SetRow(t06, 0);
            Grid.SetColumn(t06, 6);

            TextBlock t07 = new TextBlock();
            t07.HorizontalAlignment = HorizontalAlignment.Center;
            t07.Text = "7";
            Grid.SetRow(t07, 0);
            Grid.SetColumn(t07, 7);

            TextBlock t08 = new TextBlock();
            t08.HorizontalAlignment = HorizontalAlignment.Center;
            t08.Text = "8";
            Grid.SetRow(t08, 0);
            Grid.SetColumn(t08, 8);

            TextBlock t09 = new TextBlock();
            t09.HorizontalAlignment = HorizontalAlignment.Center;
            t09.Text = "9";
            Grid.SetRow(t09, 0);
            Grid.SetColumn(t09, 9);

            TextBlock tR = new TextBlock();
            tR.HorizontalAlignment = HorizontalAlignment.Center;
            tR.Text = "R";
            Grid.SetRow(tR, 0);
            Grid.SetColumn(tR, 10);

            TextBlock tH = new TextBlock();
            tH.HorizontalAlignment = HorizontalAlignment.Center;
            tH.Text = "H";
            Grid.SetRow(tH, 0);
            Grid.SetColumn(tH, 11);

            TextBlock tE = new TextBlock();
            tE.HorizontalAlignment = HorizontalAlignment.Center;
            tE.Text = "E";
            Grid.SetRow(tE, 0);
            Grid.SetColumn(tE, 12);


            TextBlock awayTeam = new TextBlock();
            awayTeam.HorizontalAlignment = HorizontalAlignment.Center;
            awayTeam.Text = connect.getRandomCity() + " " + connect.getRandomTeamName();
            Grid.SetRow(awayTeam, 1);
            Grid.SetColumn(awayTeam, 0);

            TextBlock homeTeam = new TextBlock();
            homeTeam.HorizontalAlignment = HorizontalAlignment.Center;
            homeTeam.Text = connect.getRandomCity() + " " + connect.getRandomTeamName();
            Grid.SetRow(homeTeam, 2);
            Grid.SetColumn(homeTeam, 0);

            TextBlock t11 = new TextBlock();
            t11.Text = "";
            Grid.SetRow(t11, 1);
            Grid.SetColumn(t11, 1);

            TextBlock t12 = new TextBlock();
            t12.Text = "";
            Grid.SetRow(t12, 1);
            Grid.SetColumn(t12, 2);

            TextBlock t13 = new TextBlock();
            t13.Text = "";
            Grid.SetRow(t13, 1);
            Grid.SetColumn(t13, 3);

            TextBlock t14 = new TextBlock();
            t14.Text = "";
            Grid.SetRow(t14, 1);
            Grid.SetColumn(t14, 4);

            TextBlock t15 = new TextBlock();
            t15.Text = "";
            Grid.SetRow(t15, 1);
            Grid.SetColumn(t15, 5);

            TextBlock t16 = new TextBlock();
            t16.Text = "";
            Grid.SetRow(t16, 1);
            Grid.SetColumn(t16, 6);

            TextBlock t17 = new TextBlock();
            t17.Text = "";
            Grid.SetRow(t17, 1);
            Grid.SetColumn(t17, 7);

            TextBlock t18 = new TextBlock();
            t18.Text = "";
            Grid.SetRow(t18, 1);
            Grid.SetColumn(t18, 8);

            TextBlock t19 = new TextBlock();
            t19.Text = "";
            Grid.SetRow(t19, 1);
            Grid.SetColumn(t19, 9);

            grid.Children.Add(t00);
            grid.Children.Add(t01);
            grid.Children.Add(t02);
            grid.Children.Add(t03);
            grid.Children.Add(t04);
            grid.Children.Add(t05);
            grid.Children.Add(t06);
            grid.Children.Add(t07);
            grid.Children.Add(t08);
            grid.Children.Add(t09);
            grid.Children.Add(tR);
            grid.Children.Add(tH);
            grid.Children.Add(tE);
            grid.Children.Add(awayTeam);
            grid.Children.Add(homeTeam);
            grid.Children.Add(t11);
            grid.Children.Add(t12);
            grid.Children.Add(t13);
            grid.Children.Add(t14);
            grid.Children.Add(t15);
            grid.Children.Add(t16);
            grid.Children.Add(t17);
            grid.Children.Add(t18);
            grid.Children.Add(t19);

            Grid.SetColumn(grid, 0);
            Grid.SetRow(grid, 0);
            layout.Children.Add(grid);
            
            //return grid;
        }

        public class WaitCursor : IDisposable
        {
            private Cursor _previousCursor;

            public WaitCursor()
            {
                _previousCursor = Mouse.OverrideCursor;

                Mouse.OverrideCursor = Cursors.Wait;
            }

            #region IDisposable Members

            public void Dispose()
            {
                Mouse.OverrideCursor = _previousCursor;
            }

            #endregion
        }

    }

    class DBConnect
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string username;
        private string password;

        public DBConnect()
        {
            Initialize();
        }

        private void Initialize()
        {
            server = "localhost";
            database = "baseball";
            username = "root";
            password = "Dagge23#";
            string connectionString;
            connectionString = "SERVER=" + server
                                + ";DATABASE=" + database
                                + ";UID=" + username
                                + ";PASSWORD=" + password
                                + ";";

            connection = new MySqlConnection(connectionString);
            test();
        }

        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        System.Windows.MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        System.Windows.MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                return false;
            }
        }

        public void test()
        {
            OpenConnection();
            CloseConnection();
        }

        public string getRandomTeamName() // returns random team name
        {
            string query = "SELECT name FROM team_names ORDER BY RAND() LIMIT 1";
            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                MySqlDataReader datareader = cmd.ExecuteReader();
                datareader.Read();
                var data = datareader["name"];
                CloseConnection();
                return (string)data;
            }
            else return "";
        }

        public string getRandomCity() // returns random team name
        {
            string query = "SELECT * FROM city_data ORDER BY RAND() LIMIT 1";
            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                MySqlDataReader datareader = cmd.ExecuteReader();
                datareader.Read();
                var data = datareader["city"];
                var state = datareader["state"];
                CloseConnection();
                return (string)data + "(" + (string)state + ")";
            }
            else return "";
        }

        public List<string> getTeams() // returns list of team names
        {
            string query = "SELECT * FROM team_names ORDER BY name";
            List<string> list = new List<string>();
            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                MySqlDataReader datareader = cmd.ExecuteReader();
                while (datareader.Read())
                {
                    list.Add((string)datareader["name"]);
                }
                datareader.Close();
                CloseConnection();
                return list;
            }
            else return list;
        }

        public List<string> getCities() // returns list of team names
        {
            string query = "SELECT * FROM city_data ORDER BY city";
            List<string> cities = new List<string>();
            string city;
            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                MySqlDataReader datareader = cmd.ExecuteReader();
                while (datareader.Read())
                {
                    city = (string)datareader["city"] + ", ";
                    city += (string)datareader["state"] + ", ";
                    city += (string)datareader["region"];
                    cities.Add(city);
                }
                datareader.Close();
                CloseConnection();
                return cities;
            }
            else return cities;
        }

        public List<List<Team>> generateTeams()
        {
            List<string> teamNames = new List<string>();

            List<Team> Coastal = new List<Team>();
            List<Team> GreatLakes = new List<Team>();
            List<Team> Midwest = new List<Team>();
            List<Team> North = new List<Team>();
            List<Team> Northeast = new List<Team>();
            List<Team> Southeast = new List<Team>();
            List<Team> Southwest = new List<Team>();
            List<Team> West = new List<Team>();

            List<Team>[] arr = { Coastal, GreatLakes, Midwest, North, Northeast, Southeast, Southwest, West };

            List<List<Team>> teams = new List<List<Team>>();

            teams.AddRange(arr);
 
            string teamQuery,cityQuery,city,state,region,name;
            teamQuery = "SELECT * FROM team_names";
            cityQuery = "SELECT * FROM city_data";
            if (OpenConnection())
            {
                // team names   
                MySqlCommand teamCommand = new MySqlCommand(teamQuery, connection); // team name query
                MySqlDataReader reader = teamCommand.ExecuteReader(); // execute query
                while (reader.Read()) // add team names to list
                {
                    teamNames.Add((string)reader["name"]);
                }
                reader.Close();

                // generate teams
                MySqlCommand cityCommand = new MySqlCommand(cityQuery, connection); // city data query
                reader = cityCommand.ExecuteReader(); // execute query
                var rand = new Random(); // random gen for stats
                while (reader.Read())
                {
                    city = (string)reader["city"]; // get city
                    state = (string)reader["state"]; // get state
                    region = (string)reader["region"]; // get region
                    name = teamNames[rand.Next(teamNames.Count)]; // random team name
                    Team team = new Team(name, city, state, region); // create team
                    team.batting = rand.Next(400, 900); // batting stat
                    team.defense = rand.Next(100, 400); // defense stat
                    for (int i = 0; i < 5; i++) // generate pitching rotation
                    {
                        team.rotation[i] = rand.Next(100, 500);
                    }
                    
                    switch (region) // sort teams by region
                    {
                        case "Coastal": Coastal.Add(team); break;
                        case "Great Lakes": GreatLakes.Add(team); break;
                        case "Midwest": Midwest.Add(team); break;
                        case "North": North.Add(team); break;
                        case "Northeast": Northeast.Add(team); break;
                        case "Southeast": Southeast.Add(team); break;
                        case "Southwest": Southwest.Add(team); break;
                        case "West": West.Add(team); break;
                    }
                }
                CloseConnection();
                reader.Close();
            }
            return teams;
        }
    }

    class Team
    {
        public string name { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string region { get; set; }
        public int batting { get; set; }
        public int defense { get; set; }
        public int next { get; set; }
        public int wins { get; set; }
        public int losses { get; set; }
        public int[] rotation = new int[5];
        public bool hasLeague { get; set; } = false;
        public Team()
        {

        }
        public Team(string name, string city, string state, string region)
        {
            this.name = name;
            this.city = city;
            this.state = state;
            this.region = region;
            wins = losses = 0;
        }
        public Team(string city, string state, string region)
        {
            this.city = city;
            this.state = state;
            this.region = region;
        }
        public void win()
        {
            this.wins++;
        }
        public void lose()
        {
            this.losses++;
        }
    }

    class UserTeam : Team
    {
        public UserTeam()
        {

        }
        public UserTeam(string name, string city, string state, string region)
        {
            this.name = name;
            this.city = city;
            this.state = state;
            this.region = region;
            wins = losses = 0;
        }
        public void modifyBatting(int n)
        {
            this.batting = n;
        }
        public void modifyDefense(int n)
        {
            this.defense = n;
        }
        public void modifyPitching(int n, int p)
        {
            this.rotation[p] = n;
        }
    }

    class Game
    {
        Team homeTeam, awayTeam;
        int homeRuns, awayRuns;
        public Game(ref Team homeTeam, ref Team awayTeam)
        {
            this.homeTeam = homeTeam;
            this.awayTeam = awayTeam;
        }
    }

}
