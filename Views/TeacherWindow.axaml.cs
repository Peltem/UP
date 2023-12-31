using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace study_practice;

public partial class TeacherWindow : Window
{
    private string _con = "server=10.10.1.24;database=pro1_11;user=user_01;password=user01pro";
    private List<Teacher> _teachers;
    private MySqlConnection _connection;

    public TeacherWindow()
    {
        InitializeComponent();
        ShowTable();
    }

    public void ShowTable()
    {
        string sql =
                "select Id, Name, Surname, Language from pro1_8.Teacher"
            ;
        _teachers = new List<Teacher>();
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var curTeacher = new Teacher()
            {
                Id = reader.GetInt32("Id"),
                Name = reader.GetString("Name"),
                Surname = reader.GetString("Surname"),
                Language = reader.GetInt32("Language")
            };
            _teachers.Add(curTeacher);
        }
        _connection.Close();
        TeacherDataGrid.ItemsSource = _teachers;
    }

    private void Schedule_Window_OnClick(object? sender, RoutedEventArgs e)
    {
        Schedule_ClientWindow scheduleClientWindowWindow = new Schedule_ClientWindow();
        scheduleClientWindowWindow.Show();
        this.Close();
    }
}