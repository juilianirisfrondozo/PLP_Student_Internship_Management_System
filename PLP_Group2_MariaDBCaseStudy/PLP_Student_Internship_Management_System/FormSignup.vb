Imports MySql.Data.MySqlClient
Public Class FormSignup
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        roundCorners(Me)
        pnlTermsAndCondition.Hide()

        'New Location and Size
        pnlTermsAndCondition.Location = New Point(42, 60)
        pnlTermsAndCondition.Size = New Size(948, 528)

        ' Set icon sa form at taskbar
        'Me.Icon = New System.Drawing.Icon("C:\Users\acer\source\repos\sYntax-rm\Student-_Internship_Management_System\Resources\internship_icon.ico")
        'Me.ShowIcon = True

        ' Set icon sa form at taskbar
        Me.Icon = New System.Drawing.Icon("C:\Users\acer\Downloads\buttons or icons\internship_icon.ico")
        Me.ShowIcon = True


    End Sub
    Private Sub roundCorners(obj As Form)

        Dim DGP As New Drawing2D.GraphicsPath
        DGP.StartFigure()
        'top left corner
        DGP.AddArc(New Rectangle(0, 0, 40, 40), 180, 90)
        DGP.AddLine(40, 0, obj.Width - 40, 0)

        'top right corner
        DGP.AddArc(New Rectangle(obj.Width - 40, 0, 40, 40), -90, 90)
        DGP.AddLine(obj.Width, 40, obj.Width, obj.Height - 40)

        'buttom right corner
        DGP.AddArc(New Rectangle(obj.Width - 40, obj.Height - 40, 40, 40), 0, 90)
        DGP.AddLine(obj.Width - 40, obj.Height, 40, obj.Height)

        'buttom left corner
        DGP.AddArc(New Rectangle(0, obj.Height - 40, 40, 40), 90, 90)
        DGP.CloseFigure()

        obj.Region = New Region(DGP)
    End Sub


    Private Sub clearSignUp()
        txtFirstName.Clear()
        txtLastName.Clear()
        txtEmailSignUp.Clear()
        txtPasswordSignUp.Clear()
        cmbDepartmentSignUp.SelectedIndex = -1
        cbTermsAndConditions.Checked = False
    End Sub
    Private Function validatedSignInInput() As Boolean
        If txtFirstName.Text = "" OrElse txtLastName.Text = "" OrElse txtEmailSignUp.Text = "" OrElse
            txtPasswordSignUp.Text = "" OrElse cmbDepartmentSignUp.Text = "" Then

            MessageBox.Show("Please Fill Out The Boxes", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        If txtPasswordSignUp.Text.Length < 8 Then
            MessageBox.Show("Weak Password, Make it above 8 Characters",
                            "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPasswordSignUp.Clear()
            Return False
        End If

        If Not txtEmailSignUp.Text.Contains("@gmail.com") Then
            MessageBox.Show("Email must be a Gmail address", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtEmailSignUp.Clear()
            Return False
        End If

        If Not cbTermsAndConditions.Checked Then
            MessageBox.Show("Check the Terms And Agreement", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Return True
    End Function
    Private Sub lnklblLogIn_LinkClicked_1(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnklblLogIn.LinkClicked
        Me.Hide()
        FormLogin.Show()
    End Sub

    Private Sub pctBoxExit_Click(sender As Object, e As EventArgs) Handles pctBoxExit.Click
        Dim result = MessageBox.Show("Are you sure you want to exit the program?",
                                     "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub

    Private Sub pctBoxHide_Click(sender As Object, e As EventArgs) Handles pctBoxHide.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub lblTermsAndCondition_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblTermsAndCondition.LinkClicked
        pnlTermsAndCondition.Show()
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        pnlTermsAndCondition.Hide()
    End Sub

    'BUTTON HOVER
    'BUTTON BACK  HOVER STUDENTS
    Private Sub btnBack_MouseEnter(sender As Object, e As EventArgs) Handles btnBack.MouseEnter
        btnBack.BackColor = Color.FromArgb(8, 48, 25)

    End Sub

    Private Sub btnBack_MouseLeave(sender As Object, e As EventArgs) Handles btnBack.MouseLeave
        btnBack.BackColor = Color.FromArgb(97, 144, 118)
    End Sub

    Private Sub btnBack_MouseDown(sender As Object, e As MouseEventArgs) Handles btnBack.MouseDown
        btnBack.BackColor = Color.DarkSeaGreen
    End Sub

    Private Sub btnBack_MouseUp(sender As Object, e As MouseEventArgs) Handles btnBack.MouseUp
        btnBack.BackColor = Color.FromArgb(8, 48, 25)
    End Sub

    Private Sub btnSIgnUp_Click(sender As Object, e As EventArgs) Handles btnSIgnUp.Click
        If Not validatedSignInInput() Then
            Exit Sub
        End If

        Try
            Dim facultyID As String = GenerateFacultyID()
            Dim departmentID As String = GenerateDepartmentID(cmbDepartmentSignUp.SelectedItem.ToString())

            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand("INSERT INTO faculty (faculty_id, faculty_first_name, faculty_last_name, department_id) VALUES (@FacultyID, @first_name, @last_name, @departmentID)", con)
                    con.Open()
                    cmd.Parameters.AddWithValue("@FacultyID", facultyID)
                    cmd.Parameters.AddWithValue("@departmentID", departmentID)
                    cmd.Parameters.AddWithValue("@first_name", txtFirstName.Text)
                    cmd.Parameters.AddWithValue("@last_name", txtLastName.Text)

                    cmd.ExecuteNonQuery()
                    con.Close()
                End Using
            End Using

            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand("INSERT INTO faculty_user_account (email, first_name, last_name, password, faculty_id, department_id) 
                        VALUES (@Email, @first_name, @last_name, @password, @faculty_id, @department_id)", con)
                    con.Open()
                    cmd.Parameters.AddWithValue("@Email", txtEmailSignUp.Text)
                    cmd.Parameters.AddWithValue("@first_name", txtFirstName.Text)
                    cmd.Parameters.AddWithValue("@last_name", txtLastName.Text)
                    cmd.Parameters.AddWithValue("@password", txtPasswordSignUp.Text)
                    cmd.Parameters.AddWithValue("@faculty_id", facultyID)
                    cmd.Parameters.AddWithValue("@department_id", departmentID)
                    cmd.ExecuteNonQuery()

                    con.Close()
                    MessageBox.Show("Successfully Created Faculty Account", "INFO", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk)
                    clearSignUp()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    'BUTTON HOVER
    'BUTTON SIGN UP HOVER STUDENTS
    Private Sub btnSIgnUp_MouseEnter(sender As Object, e As EventArgs) Handles btnSIgnUp.MouseEnter
        btnSIgnUp.BackColor = Color.FromArgb(8, 48, 25)

    End Sub

    Private Sub btnSIgnUp_MouseLeave(sender As Object, e As EventArgs) Handles btnSIgnUp.MouseLeave
        btnSIgnUp.BackColor = Color.FromArgb(97, 144, 118)
    End Sub

    Private Sub btnSIgnUp_MouseDown(sender As Object, e As MouseEventArgs) Handles btnSIgnUp.MouseDown
        btnSIgnUp.BackColor = Color.DarkSeaGreen
    End Sub

    Private Sub btnSIgnUp_MouseUp(sender As Object, e As MouseEventArgs) Handles btnSIgnUp.MouseUp
        btnSIgnUp.BackColor = Color.FromArgb(8, 48, 25)
    End Sub
    Private Sub pnlTermsAndCondition_Paint(sender As Object, e As PaintEventArgs) Handles pnlTermsAndCondition.Paint
        roundCorners(Me)
    End Sub
End Class