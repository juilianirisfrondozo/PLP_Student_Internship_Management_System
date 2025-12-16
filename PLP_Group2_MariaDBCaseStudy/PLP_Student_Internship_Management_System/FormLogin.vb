
Imports MySql.Data.MySqlClient
Public Class FormLogin
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        roundCorners(Me)
        pnlForgotPass.Hide()

        'eto edit ko
        ' Set icon sa form at taskbar
        'Me.Icon = New System.Drawing.Icon("D:\vbnet_programs\Finals\Student Internship (OJT) Management System-20251121T034957Z-1-001\Student Internship (OJT) Management System\Resources\internship_icon.ico")
        'Me.ShowIcon = True

        ' Set icon sa form at taskbar
        Me.Icon = New System.Drawing.Icon("C:\Users\acer\Downloads\buttons or icons\internship_icon.ico")
        Me.ShowIcon = True


    End Sub
    Private Sub roundCorners(obj As Form)

        Dim DGP As New Drawing2D.GraphicsPath
        DGP.StartFigure()
        'top left corners
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

    Private Function validNewInfoInput()
        If txtResetEmail.Text = "" OrElse txtNewPassword.Text = "" OrElse txtOldPassword.Text = "" Then
            MessageBox.Show("Please Fill Out The Boxes", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        If Not txtResetEmail.Text.Contains("@gmail.com") Then
            MessageBox.Show("Email must be a Gmail address", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtEmailLogIn.Clear()

            Return False
        End If

        If txtOldPassword.Text.Length < 8 Then
            MessageBox.Show("Invalid Length, Old Password Must be Above 8 Characters",
                            "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtPasswordLogIn.Clear()
            Return False
        End If

        If txtNewPassword.Text.Length < 8 Then
            MessageBox.Show("Weak New Password, Must be above 8 Characters",
                            "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtPasswordLogIn.Clear()
            Return False
        End If

        Return True

    End Function

    Private Function validLogInInput() As Boolean
        If txtEmailLogIn.Text = "admin" And txtPasswordLogIn.Text = "admin" Then
            FormDashboards.lblHomeTitleAs.Text = "ADMIN"
            Return True
        End If

        If txtEmailLogIn.Text = "" OrElse txtPasswordLogIn.Text = "" Then
            MessageBox.Show("Please Fill Out The Boxes", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        If Not txtEmailLogIn.Text.Contains("@gmail.com") Then
            MessageBox.Show("Email must be a Gmail address", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtEmailLogIn.Clear()

            Return False
        End If

        If txtPasswordLogIn.Text.Length < 8 Then
            MessageBox.Show("Invalid Length, Password Must be Above 8 Characters",
                            "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtPasswordLogIn.Clear()
            Return False
        End If

        Return True
    End Function

    Private Sub pctBox3_Click(sender As Object, e As EventArgs) Handles pctBoxExit1.Click
        Dim result = MessageBox.Show("Are you sure you want to exit the program?",
                                     "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub

    Private Sub pctBox4_Click(sender As Object, e As EventArgs) Handles pctBoxHide1.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub lnklblForgotPass_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnklblChangePass.LinkClicked
        pnlForgotPass.Show()
    End Sub

    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        If Not validNewInfoInput() Then Exit Sub

        If Not validAccount(txtResetEmail.Text, txtOldPassword.Text) Then
            MessageBox.Show("Incorrect Email or Old Password", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtResetEmail.Clear()
            txtOldPassword.Clear()
            Exit Sub
        End If

        If Not resetAccount(txtResetEmail.Text, txtNewPassword.Text) Then
            MessageBox.Show("Cannot Found your account", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtResetEmail.Clear()
            txtOldPassword.Clear()
            txtNewPassword.Clear()
            Exit Sub
        End If

        MessageBox.Show("Successfully Updated Password", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information)
        txtResetEmail.Clear()
        txtOldPassword.Clear()
        txtNewPassword.Clear()

    End Sub

    'BUTTON HOVER
    'BUTTON CONFIRM 1 HOVER STUDENTS
    Private Sub btnConfirm_MouseEnter(sender As Object, e As EventArgs) Handles btnConfirm.MouseEnter
        btnConfirm.BackColor = Color.FromArgb(8, 48, 25)

    End Sub

    Private Sub btnConfirm_MouseLeave(sender As Object, e As EventArgs) Handles btnConfirm.MouseLeave
        btnConfirm.BackColor = Color.FromArgb(97, 144, 118)
    End Sub

    Private Sub btnConfirm_MouseDown(sender As Object, e As MouseEventArgs) Handles btnConfirm.MouseDown
        btnConfirm.BackColor = Color.DarkSeaGreen
    End Sub

    Private Sub btnConfirm_MouseUp(sender As Object, e As MouseEventArgs) Handles btnConfirm.MouseUp
        btnConfirm.BackColor = Color.FromArgb(8, 48, 25)
    End Sub

    Private Sub keyEnterbtnConfirm_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnConfirm.PerformClick()
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub lnklblLogIn_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnklblLogIn.LinkClicked
        pnlForgotPass.Hide()
    End Sub

    Private Sub lnklblSignUp_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnklblSignUp.LinkClicked
        Me.Hide()
        FormSignup.Show()
    End Sub

    Private Sub pctBox7_Click(sender As Object, e As EventArgs) Handles pctBoxExit.Click
        Dim result = MessageBox.Show("Are you sure you want to exit the program?",
                                     "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub

    Private Sub pctBox6_Click(sender As Object, e As EventArgs) Handles pctBoxHide.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub




    'Put this inside your Login Form
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click

        Dim query As String = "
        SELECT faculty_id, first_name, last_name 
        FROM faculty_user_account
        WHERE email=@email AND password=@password
    "

        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand(query, con)

                    con.Open()
                    cmd.Parameters.AddWithValue("@email", txtEmailLogIn.Text)
                    cmd.Parameters.AddWithValue("@password", txtPasswordLogIn.Text)

                    Using rd As MySqlDataReader = cmd.ExecuteReader()
                        If rd.Read() Then

                            'Store logged-in info (use SessionModule)
                            SessionModule.LoggedFacultyID = rd("faculty_id").ToString()
                            SessionModule.LoggedFirstname = rd("first_name").ToString()
                            SessionModule.LoggedLastname = rd("last_name").ToString()

                            'Open Dashboard
                            Dim frm As New FormDashboards()
                            frm.Show()
                            Me.Hide()

                        Else
                            MessageBox.Show("Invalid username or password.", "Login Failed",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If
                    End Using

                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Database Error",
            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try



    End Sub



    'BUTTON HOVER
    'BUTTON LOGIN HOVER STUDENTS
    Private Sub btnLogin_MouseEnter(sender As Object, e As EventArgs) Handles btnLogin.MouseEnter
        btnLogin.BackColor = Color.FromArgb(8, 48, 25)

    End Sub

    Private Sub btnLogin_MouseLeave(sender As Object, e As EventArgs) Handles btnLogin.MouseLeave
        btnLogin.BackColor = Color.FromArgb(97, 144, 118)
    End Sub

    Private Sub btnLogin_MouseDown(sender As Object, e As MouseEventArgs) Handles btnLogin.MouseDown
        btnLogin.BackColor = Color.DarkSeaGreen
    End Sub

    Private Sub btnLogin_MouseUp(sender As Object, e As MouseEventArgs) Handles btnLogin.MouseUp
        btnLogin.BackColor = Color.FromArgb(8, 48, 25)
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    
End Class
