Imports MySql.Data.MySqlClient
Module Connectivity
    'clean connection every time you call it
    Public Function GetConnection() As MySqlConnection
        Dim con As New MySqlConnection("server=localhost;username=root;password=;database=ojtdb_plp")
        Return con
    End Function

End Module
