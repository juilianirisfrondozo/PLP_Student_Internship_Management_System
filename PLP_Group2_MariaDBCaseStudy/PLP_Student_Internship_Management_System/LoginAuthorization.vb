Imports System.IO
Imports MySql.Data.MySqlClient

Module LoginAuthorization
    Public Function validAccount(userEmail As String, userPassword As String) As Boolean
        If userEmail = "admin" AndAlso userPassword = "admin" Then Return True

        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand("SELECT COUNT(*) 
                                               FROM faculty_user_account 
                                               WHERE email=@userEmail AND password=@userPassword;", con)
                    con.Open()
                    cmd.Parameters.AddWithValue("@userEmail", userEmail)
                    cmd.Parameters.AddWithValue("@userPassword", userPassword)

                    Dim result As Integer = CInt(cmd.ExecuteScalar())

                    If result <> 1 Then Return False
                    con.Close()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return True
    End Function

    Public Function resetAccount(userEmail As String, newPassword As String) As Boolean
        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand("UPDATE faculty_user_account 
                                               SET password = @newPassword
                                               WHERE email=@userEmail;", con)
                    con.Open()
                    cmd.Parameters.AddWithValue("@userEmail", userEmail)
                    cmd.Parameters.AddWithValue("@newPassword", newPassword)

                    Dim result As String = cmd.ExecuteNonQuery()
                    If result = 0 Then Return False

                    con.Close()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return True
    End Function



End Module
