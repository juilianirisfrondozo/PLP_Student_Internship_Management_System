<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormLogin
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormLogin))
        Label1 = New Label()
        Label5 = New Label()
        Label6 = New Label()
        pctBoxHide1 = New PictureBox()
        pctBoxExit1 = New PictureBox()
        lnklblChangePass = New LinkLabel()
        lnklblSignUp = New LinkLabel()
        Label4 = New Label()
        Panel1 = New Panel()
        Label9 = New Label()
        Label3 = New Label()
        btnLogin = New RoundedButton()
        PictureBox1 = New PictureBox()
        lblPasswordLogIn = New Label()
        Label12 = New Label()
        txtPasswordLogIn = New TextBox()
        txtEmailLogIn = New TextBox()
        Label10 = New Label()
        Label11 = New Label()
        Panel2 = New Panel()
        pctBox2 = New PictureBox()
        txtOldPassword = New TextBox()
        txtNewPassword = New TextBox()
        txtResetEmail = New TextBox()
        Label2 = New Label()
        Label7 = New Label()
        pctBoxExit = New PictureBox()
        pctBoxHide = New PictureBox()
        btnConfirm = New RoundedButton()
        Label8 = New Label()
        lnklblLogIn = New LinkLabel()
        lblEmailResetPassword = New Label()
        Label13 = New Label()
        Label14 = New Label()
        pnlForgotPass = New Panel()
        CType(pctBoxHide1, ComponentModel.ISupportInitialize).BeginInit()
        CType(pctBoxExit1, ComponentModel.ISupportInitialize).BeginInit()
        Panel1.SuspendLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        Panel2.SuspendLayout()
        CType(pctBox2, ComponentModel.ISupportInitialize).BeginInit()
        CType(pctBoxExit, ComponentModel.ISupportInitialize).BeginInit()
        CType(pctBoxHide, ComponentModel.ISupportInitialize).BeginInit()
        pnlForgotPass.SuspendLayout()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.FromArgb(CByte(8), CByte(48), CByte(25))
        Label1.Location = New Point(649, 284)
        Label1.Name = "Label1"
        Label1.Size = New Size(203, 38)
        Label1.TabIndex = 35
        Label1.Text = "Faculty LOGIN"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label5.ForeColor = Color.DimGray
        Label5.Location = New Point(576, 252)
        Label5.Name = "Label5"
        Label5.Size = New Size(360, 23)
        Label5.TabIndex = 34
        Label5.Text = "Student Internship (OJT) Management System"
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Font = New Font("Century", 16F, FontStyle.Bold)
        Label6.ForeColor = Color.FromArgb(CByte(8), CByte(48), CByte(25))
        Label6.Location = New Point(511, 219)
        Label6.Name = "Label6"
        Label6.Size = New Size(481, 33)
        Label6.TabIndex = 33
        Label6.Text = "Pamantasan ng Lungsod ng Pasig"
        ' 
        ' pctBoxHide1
        ' 
        pctBoxHide1.BackColor = Color.Transparent
        pctBoxHide1.BackgroundImage = CType(resources.GetObject("pctBoxHide1.BackgroundImage"), Image)
        pctBoxHide1.BackgroundImageLayout = ImageLayout.Stretch
        pctBoxHide1.Cursor = Cursors.Hand
        pctBoxHide1.Location = New Point(933, 17)
        pctBoxHide1.Name = "pctBoxHide1"
        pctBoxHide1.Size = New Size(25, 25)
        pctBoxHide1.TabIndex = 32
        pctBoxHide1.TabStop = False
        ' 
        ' pctBoxExit1
        ' 
        pctBoxExit1.BackColor = Color.Transparent
        pctBoxExit1.BackgroundImage = CType(resources.GetObject("pctBoxExit1.BackgroundImage"), Image)
        pctBoxExit1.BackgroundImageLayout = ImageLayout.Stretch
        pctBoxExit1.Cursor = Cursors.Hand
        pctBoxExit1.Location = New Point(964, 17)
        pctBoxExit1.Name = "pctBoxExit1"
        pctBoxExit1.Size = New Size(27, 25)
        pctBoxExit1.TabIndex = 31
        pctBoxExit1.TabStop = False
        ' 
        ' lnklblChangePass
        ' 
        lnklblChangePass.ActiveLinkColor = Color.FromArgb(CByte(97), CByte(144), CByte(118))
        lnklblChangePass.AutoSize = True
        lnklblChangePass.Cursor = Cursors.Help
        lnklblChangePass.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lnklblChangePass.LinkColor = Color.FromArgb(CByte(0), CByte(64), CByte(0))
        lnklblChangePass.Location = New Point(539, 470)
        lnklblChangePass.Name = "lnklblChangePass"
        lnklblChangePass.Size = New Size(139, 20)
        lnklblChangePass.TabIndex = 30
        lnklblChangePass.TabStop = True
        lnklblChangePass.Text = "Change Password?"
        ' 
        ' lnklblSignUp
        ' 
        lnklblSignUp.ActiveLinkColor = Color.FromArgb(CByte(97), CByte(144), CByte(118))
        lnklblSignUp.AutoSize = True
        lnklblSignUp.BackColor = Color.Transparent
        lnklblSignUp.CausesValidation = False
        lnklblSignUp.Font = New Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lnklblSignUp.LinkColor = Color.FromArgb(CByte(0), CByte(64), CByte(0))
        lnklblSignUp.Location = New Point(808, 575)
        lnklblSignUp.Name = "lnklblSignUp"
        lnklblSignUp.Size = New Size(72, 23)
        lnklblSignUp.TabIndex = 44
        lnklblSignUp.TabStop = True
        lnklblSignUp.Text = "Sign up"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.CausesValidation = False
        Label4.Font = New Font("Segoe UI", 10F)
        Label4.ForeColor = Color.Black
        Label4.Location = New Point(623, 576)
        Label4.Name = "Label4"
        Label4.Size = New Size(191, 23)
        Label4.TabIndex = 43
        Label4.Text = "Don't have an account?"
        ' 
        ' Panel1
        ' 
        Panel1.BackgroundImage = CType(resources.GetObject("Panel1.BackgroundImage"), Image)
        Panel1.Controls.Add(Label9)
        Panel1.Controls.Add(Label3)
        Panel1.Location = New Point(2, -2)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(487, 636)
        Panel1.TabIndex = 80
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.BackColor = Color.Transparent
        Label9.CausesValidation = False
        Label9.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label9.ForeColor = Color.White
        Label9.Location = New Point(77, 318)
        Label9.Name = "Label9"
        Label9.Size = New Size(325, 56)
        Label9.TabIndex = 75
        Label9.Text = " Ensure your password is strong and" & vbCrLf & "               easy to remember"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.BackColor = Color.Transparent
        Label3.CausesValidation = False
        Label3.Font = New Font("Segoe UI", 25.8000011F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label3.ForeColor = Color.White
        Label3.Location = New Point(-1, 256)
        Label3.Name = "Label3"
        Label3.Size = New Size(495, 60)
        Label3.TabIndex = 71
        Label3.Text = "Change your Password"
        ' 
        ' btnLogin
        ' 
        btnLogin.BackColor = Color.FromArgb(CByte(97), CByte(144), CByte(118))
        btnLogin.FlatAppearance.BorderSize = 0
        btnLogin.FlatStyle = FlatStyle.Flat
        btnLogin.Font = New Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnLogin.ForeColor = Color.White
        btnLogin.Location = New Point(668, 513)
        btnLogin.MinimumSize = New Size(50, 25)
        btnLogin.Name = "btnLogin"
        btnLogin.Size = New Size(169, 55)
        btnLogin.TabIndex = 46
        btnLogin.Text = "LOG IN"
        btnLogin.UseVisualStyleBackColor = False
        ' 
        ' PictureBox1
        ' 
        PictureBox1.BackColor = Color.Transparent
        PictureBox1.BackgroundImage = CType(resources.GetObject("PictureBox1.BackgroundImage"), Image)
        PictureBox1.BackgroundImageLayout = ImageLayout.Stretch
        PictureBox1.Location = New Point(671, 66)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(154, 147)
        PictureBox1.TabIndex = 82
        PictureBox1.TabStop = False
        ' 
        ' lblPasswordLogIn
        ' 
        lblPasswordLogIn.AutoSize = True
        lblPasswordLogIn.Font = New Font("Segoe UI", 10.8F, FontStyle.Bold Or FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        lblPasswordLogIn.ForeColor = Color.FromArgb(CByte(8), CByte(48), CByte(25))
        lblPasswordLogIn.Location = New Point(542, 400)
        lblPasswordLogIn.Name = "lblPasswordLogIn"
        lblPasswordLogIn.Size = New Size(97, 25)
        lblPasswordLogIn.TabIndex = 88
        lblPasswordLogIn.Text = "Password:"
        ' 
        ' Label12
        ' 
        Label12.AutoSize = True
        Label12.Font = New Font("Segoe UI", 10.8F, FontStyle.Bold Or FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        Label12.ForeColor = Color.FromArgb(CByte(8), CByte(48), CByte(25))
        Label12.Location = New Point(542, 317)
        Label12.Name = "Label12"
        Label12.Size = New Size(66, 25)
        Label12.TabIndex = 87
        Label12.Text = "Email:"
        ' 
        ' txtPasswordLogIn
        ' 
        txtPasswordLogIn.Cursor = Cursors.IBeam
        txtPasswordLogIn.Font = New Font("Segoe UI Semibold", 13.8F, FontStyle.Bold)
        txtPasswordLogIn.ForeColor = Color.FromArgb(CByte(8), CByte(48), CByte(25))
        txtPasswordLogIn.Location = New Point(539, 429)
        txtPasswordLogIn.MaxLength = 30
        txtPasswordLogIn.Name = "txtPasswordLogIn"
        txtPasswordLogIn.PasswordChar = "*"c
        txtPasswordLogIn.Size = New Size(432, 38)
        txtPasswordLogIn.TabIndex = 86
        txtPasswordLogIn.UseSystemPasswordChar = True
        ' 
        ' txtEmailLogIn
        ' 
        txtEmailLogIn.Cursor = Cursors.IBeam
        txtEmailLogIn.Font = New Font("Segoe UI Semibold", 13.8F, FontStyle.Bold)
        txtEmailLogIn.ForeColor = Color.FromArgb(CByte(8), CByte(48), CByte(25))
        txtEmailLogIn.Location = New Point(542, 343)
        txtEmailLogIn.MaxLength = 60
        txtEmailLogIn.Multiline = True
        txtEmailLogIn.Name = "txtEmailLogIn"
        txtEmailLogIn.Size = New Size(432, 38)
        txtEmailLogIn.TabIndex = 85
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.BackColor = Color.Transparent
        Label10.CausesValidation = False
        Label10.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label10.ForeColor = Color.White
        Label10.Location = New Point(38, 325)
        Label10.Name = "Label10"
        Label10.Size = New Size(377, 56)
        Label10.TabIndex = 75
        Label10.Text = "       Empowering student success through " & vbCrLf & "           effective internship management"
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.BackColor = Color.Transparent
        Label11.CausesValidation = False
        Label11.Font = New Font("Segoe UI", 25.2F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label11.ForeColor = Color.White
        Label11.Location = New Point(-44, 199)
        Label11.Name = "Label11"
        Label11.Size = New Size(532, 114)
        Label11.TabIndex = 71
        Label11.Text = "           Faculty Portal: " & vbCrLf & "    Guiding Future Leaders"
        ' 
        ' Panel2
        ' 
        Panel2.BackgroundImage = CType(resources.GetObject("Panel2.BackgroundImage"), Image)
        Panel2.Controls.Add(Label10)
        Panel2.Controls.Add(Label11)
        Panel2.Location = New Point(0, 0)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(487, 636)
        Panel2.TabIndex = 89
        ' 
        ' pctBox2
        ' 
        pctBox2.BackColor = Color.Transparent
        pctBox2.BackgroundImage = CType(resources.GetObject("pctBox2.BackgroundImage"), Image)
        pctBox2.BackgroundImageLayout = ImageLayout.Stretch
        pctBox2.Location = New Point(677, 62)
        pctBox2.Name = "pctBox2"
        pctBox2.Size = New Size(154, 147)
        pctBox2.TabIndex = 57
        pctBox2.TabStop = False
        ' 
        ' txtOldPassword
        ' 
        txtOldPassword.Cursor = Cursors.IBeam
        txtOldPassword.Font = New Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        txtOldPassword.ForeColor = Color.FromArgb(CByte(8), CByte(48), CByte(25))
        txtOldPassword.Location = New Point(538, 390)
        txtOldPassword.MaxLength = 30
        txtOldPassword.Name = "txtOldPassword"
        txtOldPassword.PasswordChar = "*"c
        txtOldPassword.Size = New Size(432, 38)
        txtOldPassword.TabIndex = 61
        txtOldPassword.UseSystemPasswordChar = True
        ' 
        ' txtNewPassword
        ' 
        txtNewPassword.Cursor = Cursors.IBeam
        txtNewPassword.Font = New Font("Segoe UI Semibold", 13.8F, FontStyle.Bold)
        txtNewPassword.ForeColor = Color.FromArgb(CByte(8), CByte(48), CByte(25))
        txtNewPassword.Location = New Point(538, 461)
        txtNewPassword.MaxLength = 30
        txtNewPassword.Name = "txtNewPassword"
        txtNewPassword.PasswordChar = "*"c
        txtNewPassword.Size = New Size(432, 38)
        txtNewPassword.TabIndex = 62
        txtNewPassword.UseSystemPasswordChar = True
        ' 
        ' txtResetEmail
        ' 
        txtResetEmail.Cursor = Cursors.IBeam
        txtResetEmail.Font = New Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        txtResetEmail.ForeColor = Color.FromArgb(CByte(8), CByte(48), CByte(25))
        txtResetEmail.Location = New Point(538, 316)
        txtResetEmail.MaxLength = 60
        txtResetEmail.Multiline = True
        txtResetEmail.Name = "txtResetEmail"
        txtResetEmail.Size = New Size(432, 38)
        txtResetEmail.TabIndex = 64
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Century", 16F, FontStyle.Bold)
        Label2.ForeColor = Color.FromArgb(CByte(8), CByte(48), CByte(25))
        Label2.Location = New Point(621, 214)
        Label2.Name = "Label2"
        Label2.Size = New Size(271, 33)
        Label2.TabIndex = 65
        Label2.Text = "Change Password?"
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.BackColor = Color.Transparent
        Label7.CausesValidation = False
        Label7.Font = New Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label7.ForeColor = Color.DimGray
        Label7.Location = New Point(566, 247)
        Label7.Name = "Label7"
        Label7.Size = New Size(380, 23)
        Label7.TabIndex = 66
        Label7.Text = "Enter your email address to reset your password."
        ' 
        ' pctBoxExit
        ' 
        pctBoxExit.BackColor = Color.Transparent
        pctBoxExit.BackgroundImage = CType(resources.GetObject("pctBoxExit.BackgroundImage"), Image)
        pctBoxExit.BackgroundImageLayout = ImageLayout.Stretch
        pctBoxExit.Cursor = Cursors.Hand
        pctBoxExit.Location = New Point(968, 20)
        pctBoxExit.Name = "pctBoxExit"
        pctBoxExit.Size = New Size(27, 25)
        pctBoxExit.TabIndex = 67
        pctBoxExit.TabStop = False
        ' 
        ' pctBoxHide
        ' 
        pctBoxHide.BackColor = Color.Transparent
        pctBoxHide.BackgroundImage = CType(resources.GetObject("pctBoxHide.BackgroundImage"), Image)
        pctBoxHide.BackgroundImageLayout = ImageLayout.Stretch
        pctBoxHide.Cursor = Cursors.Hand
        pctBoxHide.Location = New Point(937, 20)
        pctBoxHide.Name = "pctBoxHide"
        pctBoxHide.Size = New Size(25, 25)
        pctBoxHide.TabIndex = 68
        pctBoxHide.TabStop = False
        ' 
        ' btnConfirm
        ' 
        btnConfirm.BackColor = Color.FromArgb(CByte(97), CByte(144), CByte(118))
        btnConfirm.FlatAppearance.BorderSize = 0
        btnConfirm.FlatStyle = FlatStyle.Flat
        btnConfirm.Font = New Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnConfirm.ForeColor = Color.White
        btnConfirm.Location = New Point(673, 521)
        btnConfirm.MinimumSize = New Size(50, 25)
        btnConfirm.Name = "btnConfirm"
        btnConfirm.Size = New Size(169, 55)
        btnConfirm.TabIndex = 69
        btnConfirm.Text = "CONFIRM"
        btnConfirm.UseVisualStyleBackColor = False
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.BackColor = Color.Transparent
        Label8.CausesValidation = False
        Label8.Font = New Font("Segoe UI", 10.2F)
        Label8.ForeColor = Color.Black
        Label8.Location = New Point(695, 588)
        Label8.Name = "Label8"
        Label8.Size = New Size(66, 23)
        Label8.TabIndex = 73
        Label8.Text = "Back to"
        ' 
        ' lnklblLogIn
        ' 
        lnklblLogIn.ActiveLinkColor = Color.FromArgb(CByte(97), CByte(144), CByte(118))
        lnklblLogIn.AutoSize = True
        lnklblLogIn.BackColor = Color.Transparent
        lnklblLogIn.CausesValidation = False
        lnklblLogIn.Font = New Font("Segoe UI Semibold", 10.2F, FontStyle.Bold)
        lnklblLogIn.LinkColor = Color.FromArgb(CByte(0), CByte(64), CByte(0))
        lnklblLogIn.Location = New Point(758, 587)
        lnklblLogIn.Name = "lnklblLogIn"
        lnklblLogIn.Size = New Size(57, 23)
        lnklblLogIn.TabIndex = 74
        lnklblLogIn.TabStop = True
        lnklblLogIn.Text = "Log in"
        ' 
        ' lblEmailResetPassword
        ' 
        lblEmailResetPassword.AutoSize = True
        lblEmailResetPassword.Font = New Font("Segoe UI", 10.8F, FontStyle.Bold Or FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        lblEmailResetPassword.ForeColor = Color.FromArgb(CByte(8), CByte(48), CByte(25))
        lblEmailResetPassword.Location = New Point(540, 289)
        lblEmailResetPassword.Name = "lblEmailResetPassword"
        lblEmailResetPassword.Size = New Size(66, 25)
        lblEmailResetPassword.TabIndex = 81
        lblEmailResetPassword.Text = "Email:"
        ' 
        ' Label13
        ' 
        Label13.AutoSize = True
        Label13.Font = New Font("Segoe UI", 10.8F, FontStyle.Bold Or FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        Label13.ForeColor = Color.FromArgb(CByte(8), CByte(48), CByte(25))
        Label13.Location = New Point(541, 362)
        Label13.Name = "Label13"
        Label13.Size = New Size(133, 25)
        Label13.TabIndex = 82
        Label13.Text = "Old Password:"
        ' 
        ' Label14
        ' 
        Label14.AutoSize = True
        Label14.Font = New Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label14.ForeColor = Color.FromArgb(CByte(8), CByte(48), CByte(25))
        Label14.Location = New Point(540, 435)
        Label14.Name = "Label14"
        Label14.Size = New Size(140, 25)
        Label14.TabIndex = 83
        Label14.Text = "New Password:"
        ' 
        ' pnlForgotPass
        ' 
        pnlForgotPass.Controls.Add(Label14)
        pnlForgotPass.Controls.Add(Label13)
        pnlForgotPass.Controls.Add(Panel1)
        pnlForgotPass.Controls.Add(lblEmailResetPassword)
        pnlForgotPass.Controls.Add(lnklblLogIn)
        pnlForgotPass.Controls.Add(Label8)
        pnlForgotPass.Controls.Add(btnConfirm)
        pnlForgotPass.Controls.Add(pctBoxHide)
        pnlForgotPass.Controls.Add(pctBoxExit)
        pnlForgotPass.Controls.Add(Label7)
        pnlForgotPass.Controls.Add(Label2)
        pnlForgotPass.Controls.Add(txtResetEmail)
        pnlForgotPass.Controls.Add(txtNewPassword)
        pnlForgotPass.Controls.Add(txtOldPassword)
        pnlForgotPass.Controls.Add(pctBox2)
        pnlForgotPass.Location = New Point(-2, 0)
        pnlForgotPass.Name = "pnlForgotPass"
        pnlForgotPass.Size = New Size(1019, 634)
        pnlForgotPass.TabIndex = 45
        ' 
        ' FormLogin
        ' 
        AcceptButton = btnLogin
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(1019, 633)
        Controls.Add(pnlForgotPass)
        Controls.Add(lblPasswordLogIn)
        Controls.Add(Label12)
        Controls.Add(txtPasswordLogIn)
        Controls.Add(txtEmailLogIn)
        Controls.Add(lnklblSignUp)
        Controls.Add(Label4)
        Controls.Add(Label1)
        Controls.Add(Label5)
        Controls.Add(Label6)
        Controls.Add(pctBoxHide1)
        Controls.Add(pctBoxExit1)
        Controls.Add(lnklblChangePass)
        Controls.Add(Panel2)
        Controls.Add(btnLogin)
        Controls.Add(PictureBox1)
        FormBorderStyle = FormBorderStyle.None
        Name = "FormLogin"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Student Internship Management System - PLP"
        CType(pctBoxHide1, ComponentModel.ISupportInitialize).EndInit()
        CType(pctBoxExit1, ComponentModel.ISupportInitialize).EndInit()
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        CType(pctBox2, ComponentModel.ISupportInitialize).EndInit()
        CType(pctBoxExit, ComponentModel.ISupportInitialize).EndInit()
        CType(pctBoxHide, ComponentModel.ISupportInitialize).EndInit()
        pnlForgotPass.ResumeLayout(False)
        pnlForgotPass.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents pctBoxHide1 As PictureBox
    Friend WithEvents pctBoxExit1 As PictureBox
    Friend WithEvents lnklblChangePass As LinkLabel
    Friend WithEvents lnklblSignUp As LinkLabel
    Friend WithEvents Label4 As Label
    Friend WithEvents btnLogin As RoundedButton
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label9 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents lblPasswordLogIn As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents txtPasswordLogIn As TextBox
    Friend WithEvents txtEmailLogIn As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents pnlForgotPass As Panel
    Friend WithEvents Label14 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents lblEmailResetPassword As Label
    Friend WithEvents lnklblLogIn As LinkLabel
    Friend WithEvents Label8 As Label
    Friend WithEvents btnConfirm As RoundedButton
    Friend WithEvents pctBoxHide As PictureBox
    Friend WithEvents pctBoxExit As PictureBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txtResetEmail As TextBox
    Friend WithEvents txtNewPassword As TextBox
    Friend WithEvents txtOldPassword As TextBox
    Friend WithEvents pctBox2 As PictureBox

End Class
