<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormSignup
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormSignup))
        lnklblLogIn = New LinkLabel()
        Label4 = New Label()
        Label1 = New Label()
        Label5 = New Label()
        Label6 = New Label()
        pctBoxHide = New PictureBox()
        pctBoxExit = New PictureBox()
        PictureBox4 = New PictureBox()
        txtEmailSignUp = New TextBox()
        txtFirstName = New TextBox()
        btnSIgnUp = New RoundedButton()
        txtPasswordSignUp = New TextBox()
        cbTermsAndConditions = New CheckBox()
        lblTermsAndCondition = New LinkLabel()
        pnlTermsAndCondition = New Panel()
        btnBack = New RoundedButton()
        Label10 = New Label()
        Label11 = New Label()
        txtLastName = New TextBox()
        cmbDepartmentSignUp = New ComboBox()
        Label2 = New Label()
        Label7 = New Label()
        Label8 = New Label()
        Label12 = New Label()
        Label13 = New Label()
        Label3 = New Label()
        Label9 = New Label()
        Panel1 = New Panel()
        CType(pctBoxHide, ComponentModel.ISupportInitialize).BeginInit()
        CType(pctBoxExit, ComponentModel.ISupportInitialize).BeginInit()
        CType(PictureBox4, ComponentModel.ISupportInitialize).BeginInit()
        pnlTermsAndCondition.SuspendLayout()
        Panel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' lnklblLogIn
        ' 
        lnklblLogIn.ActiveLinkColor = Color.FromArgb(CByte(97), CByte(144), CByte(118))
        lnklblLogIn.AutoSize = True
        lnklblLogIn.BackColor = Color.Transparent
        lnklblLogIn.CausesValidation = False
        lnklblLogIn.Font = New Font("Segoe UI Semibold", 10.2F, FontStyle.Bold)
        lnklblLogIn.LinkColor = Color.FromArgb(CByte(0), CByte(64), CByte(0))
        lnklblLogIn.Location = New Point(831, 604)
        lnklblLogIn.Name = "lnklblLogIn"
        lnklblLogIn.Size = New Size(57, 23)
        lnklblLogIn.TabIndex = 72
        lnklblLogIn.TabStop = True
        lnklblLogIn.Text = "Log in"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.BackColor = Color.Transparent
        Label4.CausesValidation = False
        Label4.Font = New Font("Segoe UI", 10.2F)
        Label4.ForeColor = Color.FromArgb(CByte(8), CByte(48), CByte(25))
        Label4.Location = New Point(629, 604)
        Label4.Name = "Label4"
        Label4.Size = New Size(206, 23)
        Label4.TabIndex = 71
        Label4.Text = "Already have an account?"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.FromArgb(CByte(8), CByte(48), CByte(25))
        Label1.Location = New Point(675, 223)
        Label1.Name = "Label1"
        Label1.Size = New Size(147, 38)
        Label1.TabIndex = 70
        Label1.Text = "Welcome!"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Segoe UI", 10F)
        Label5.ForeColor = Color.DimGray
        Label5.Location = New Point(568, 200)
        Label5.Name = "Label5"
        Label5.Size = New Size(360, 23)
        Label5.TabIndex = 69
        Label5.Text = "Student Internship (OJT) Management System"
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Font = New Font("Century", 16F, FontStyle.Bold)
        Label6.ForeColor = Color.FromArgb(CByte(8), CByte(48), CByte(25))
        Label6.Location = New Point(633, 168)
        Label6.Name = "Label6"
        Label6.Size = New Size(226, 33)
        Label6.TabIndex = 68
        Label6.Text = "Create Account"
        ' 
        ' pctBoxHide
        ' 
        pctBoxHide.BackColor = Color.Transparent
        pctBoxHide.BackgroundImage = CType(resources.GetObject("pctBoxHide.BackgroundImage"), Image)
        pctBoxHide.BackgroundImageLayout = ImageLayout.Stretch
        pctBoxHide.Cursor = Cursors.Hand
        pctBoxHide.Location = New Point(951, 14)
        pctBoxHide.Name = "pctBoxHide"
        pctBoxHide.Size = New Size(25, 25)
        pctBoxHide.TabIndex = 67
        pctBoxHide.TabStop = False
        ' 
        ' pctBoxExit
        ' 
        pctBoxExit.BackColor = Color.Transparent
        pctBoxExit.BackgroundImage = CType(resources.GetObject("pctBoxExit.BackgroundImage"), Image)
        pctBoxExit.BackgroundImageLayout = ImageLayout.Stretch
        pctBoxExit.Cursor = Cursors.Hand
        pctBoxExit.Location = New Point(982, 14)
        pctBoxExit.Name = "pctBoxExit"
        pctBoxExit.Size = New Size(27, 25)
        pctBoxExit.TabIndex = 66
        pctBoxExit.TabStop = False
        ' 
        ' PictureBox4
        ' 
        PictureBox4.BackColor = Color.Transparent
        PictureBox4.BackgroundImage = CType(resources.GetObject("PictureBox4.BackgroundImage"), Image)
        PictureBox4.BackgroundImageLayout = ImageLayout.Stretch
        PictureBox4.Location = New Point(665, 13)
        PictureBox4.Name = "PictureBox4"
        PictureBox4.Size = New Size(154, 147)
        PictureBox4.TabIndex = 62
        PictureBox4.TabStop = False
        ' 
        ' txtEmailSignUp
        ' 
        txtEmailSignUp.Cursor = Cursors.IBeam
        txtEmailSignUp.Font = New Font("Segoe UI Semibold", 13.8F, FontStyle.Bold)
        txtEmailSignUp.ForeColor = Color.FromArgb(CByte(8), CByte(48), CByte(25))
        txtEmailSignUp.Location = New Point(609, 422)
        txtEmailSignUp.MaxLength = 60
        txtEmailSignUp.Multiline = True
        txtEmailSignUp.Name = "txtEmailSignUp"
        txtEmailSignUp.Size = New Size(374, 38)
        txtEmailSignUp.TabIndex = 64
        txtEmailSignUp.UseSystemPasswordChar = True
        ' 
        ' txtFirstName
        ' 
        txtFirstName.Cursor = Cursors.IBeam
        txtFirstName.Font = New Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        txtFirstName.ForeColor = Color.FromArgb(CByte(8), CByte(48), CByte(25))
        txtFirstName.Location = New Point(609, 268)
        txtFirstName.MaxLength = 120
        txtFirstName.Multiline = True
        txtFirstName.Name = "txtFirstName"
        txtFirstName.Size = New Size(374, 38)
        txtFirstName.TabIndex = 63
        ' 
        ' btnSIgnUp
        ' 
        btnSIgnUp.BackColor = Color.FromArgb(CByte(97), CByte(144), CByte(118))
        btnSIgnUp.FlatAppearance.BorderSize = 0
        btnSIgnUp.FlatStyle = FlatStyle.Flat
        btnSIgnUp.Font = New Font("Segoe UI", 16.2F, FontStyle.Bold)
        btnSIgnUp.ForeColor = Color.White
        btnSIgnUp.Location = New Point(677, 556)
        btnSIgnUp.MinimumSize = New Size(50, 25)
        btnSIgnUp.Name = "btnSIgnUp"
        btnSIgnUp.Size = New Size(164, 43)
        btnSIgnUp.TabIndex = 74
        btnSIgnUp.Text = "SIGN UP"
        btnSIgnUp.UseVisualStyleBackColor = False
        ' 
        ' txtPasswordSignUp
        ' 
        txtPasswordSignUp.Cursor = Cursors.IBeam
        txtPasswordSignUp.Font = New Font("Segoe UI Semibold", 13.8F, FontStyle.Bold)
        txtPasswordSignUp.ForeColor = Color.FromArgb(CByte(8), CByte(48), CByte(25))
        txtPasswordSignUp.Location = New Point(609, 480)
        txtPasswordSignUp.MaxLength = 30
        txtPasswordSignUp.Multiline = True
        txtPasswordSignUp.Name = "txtPasswordSignUp"
        txtPasswordSignUp.Size = New Size(374, 38)
        txtPasswordSignUp.TabIndex = 75
        txtPasswordSignUp.UseSystemPasswordChar = True
        ' 
        ' cbTermsAndConditions
        ' 
        cbTermsAndConditions.AutoSize = True
        cbTermsAndConditions.BackColor = Color.Transparent
        cbTermsAndConditions.Font = New Font("Segoe UI", 10.2F)
        cbTermsAndConditions.ForeColor = Color.FromArgb(CByte(8), CByte(48), CByte(25))
        cbTermsAndConditions.Location = New Point(620, 525)
        cbTermsAndConditions.Name = "cbTermsAndConditions"
        cbTermsAndConditions.Size = New Size(136, 27)
        cbTermsAndConditions.TabIndex = 76
        cbTermsAndConditions.Text = "I agree to the"
        cbTermsAndConditions.UseVisualStyleBackColor = False
        ' 
        ' lblTermsAndCondition
        ' 
        lblTermsAndCondition.ActiveLinkColor = Color.FromArgb(CByte(97), CByte(144), CByte(118))
        lblTermsAndCondition.AutoSize = True
        lblTermsAndCondition.BackColor = Color.Transparent
        lblTermsAndCondition.CausesValidation = False
        lblTermsAndCondition.Font = New Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblTermsAndCondition.LinkColor = Color.FromArgb(CByte(0), CByte(64), CByte(0))
        lblTermsAndCondition.Location = New Point(740, 526)
        lblTermsAndCondition.Name = "lblTermsAndCondition"
        lblTermsAndCondition.Size = New Size(175, 23)
        lblTermsAndCondition.TabIndex = 77
        lblTermsAndCondition.TabStop = True
        lblTermsAndCondition.Text = "Terms and Conditions"
        ' 
        ' pnlTermsAndCondition
        ' 
        pnlTermsAndCondition.Controls.Add(btnBack)
        pnlTermsAndCondition.Controls.Add(Label10)
        pnlTermsAndCondition.Controls.Add(Label11)
        pnlTermsAndCondition.Location = New Point(34, 38)
        pnlTermsAndCondition.Name = "pnlTermsAndCondition"
        pnlTermsAndCondition.Size = New Size(911, 447)
        pnlTermsAndCondition.TabIndex = 82
        ' 
        ' btnBack
        ' 
        btnBack.BackColor = Color.FromArgb(CByte(97), CByte(144), CByte(118))
        btnBack.FlatAppearance.BorderSize = 0
        btnBack.FlatStyle = FlatStyle.Flat
        btnBack.Font = New Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnBack.ForeColor = Color.White
        btnBack.Location = New Point(728, 469)
        btnBack.MinimumSize = New Size(50, 25)
        btnBack.Name = "btnBack"
        btnBack.Size = New Size(142, 45)
        btnBack.TabIndex = 69
        btnBack.Text = "Done"
        btnBack.UseVisualStyleBackColor = False
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.BackColor = Color.Transparent
        Label10.CausesValidation = False
        Label10.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label10.ForeColor = Color.FromArgb(CByte(8), CByte(48), CByte(25))
        Label10.Location = New Point(46, 56)
        Label10.Name = "Label10"
        Label10.Size = New Size(834, 420)
        Label10.TabIndex = 66
        Label10.Text = resources.GetString("Label10.Text")
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.Font = New Font("Century", 18F, FontStyle.Bold)
        Label11.ForeColor = Color.FromArgb(CByte(8), CByte(48), CByte(25))
        Label11.Location = New Point(22, 17)
        Label11.Name = "Label11"
        Label11.Size = New Size(341, 35)
        Label11.TabIndex = 65
        Label11.Text = "Terms and Conditions"
        ' 
        ' txtLastName
        ' 
        txtLastName.Cursor = Cursors.IBeam
        txtLastName.Font = New Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        txtLastName.ForeColor = Color.FromArgb(CByte(8), CByte(48), CByte(25))
        txtLastName.Location = New Point(609, 317)
        txtLastName.MaxLength = 120
        txtLastName.Multiline = True
        txtLastName.Name = "txtLastName"
        txtLastName.Size = New Size(374, 38)
        txtLastName.TabIndex = 83
        ' 
        ' cmbDepartmentSignUp
        ' 
        cmbDepartmentSignUp.DropDownStyle = ComboBoxStyle.DropDownList
        cmbDepartmentSignUp.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        cmbDepartmentSignUp.ForeColor = Color.FromArgb(CByte(8), CByte(48), CByte(25))
        cmbDepartmentSignUp.FormattingEnabled = True
        cmbDepartmentSignUp.Items.AddRange(New Object() {"College of Arts and Sciences", "College of Business and Accountancy", "College of Education", "College of Engineering", "College of Computer Studies", "College of Hospitality Management", "College of Nursing"})
        cmbDepartmentSignUp.Location = New Point(611, 371)
        cmbDepartmentSignUp.Name = "cmbDepartmentSignUp"
        cmbDepartmentSignUp.Size = New Size(370, 36)
        cmbDepartmentSignUp.TabIndex = 84
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 10.8F, FontStyle.Bold Or FontStyle.Italic)
        Label2.ForeColor = Color.FromArgb(CByte(8), CByte(48), CByte(25))
        Label2.Location = New Point(478, 276)
        Label2.Name = "Label2"
        Label2.Size = New Size(109, 25)
        Label2.TabIndex = 85
        Label2.Text = "First Name:"
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Font = New Font("Segoe UI", 10.8F, FontStyle.Bold Or FontStyle.Italic)
        Label7.ForeColor = Color.FromArgb(CByte(8), CByte(48), CByte(25))
        Label7.Location = New Point(478, 327)
        Label7.Name = "Label7"
        Label7.Size = New Size(107, 25)
        Label7.TabIndex = 86
        Label7.Text = "Last Name:"
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Font = New Font("Segoe UI", 10.8F, FontStyle.Bold Or FontStyle.Italic)
        Label8.ForeColor = Color.FromArgb(CByte(8), CByte(48), CByte(25))
        Label8.Location = New Point(478, 377)
        Label8.Name = "Label8"
        Label8.Size = New Size(119, 25)
        Label8.TabIndex = 87
        Label8.Text = "Department:"
        ' 
        ' Label12
        ' 
        Label12.AutoSize = True
        Label12.Font = New Font("Segoe UI", 10.8F, FontStyle.Bold Or FontStyle.Italic)
        Label12.ForeColor = Color.FromArgb(CByte(8), CByte(48), CByte(25))
        Label12.Location = New Point(478, 430)
        Label12.Name = "Label12"
        Label12.Size = New Size(66, 25)
        Label12.TabIndex = 88
        Label12.Text = "Email:"
        ' 
        ' Label13
        ' 
        Label13.AutoSize = True
        Label13.Font = New Font("Segoe UI", 10.8F, FontStyle.Bold Or FontStyle.Italic)
        Label13.ForeColor = Color.FromArgb(CByte(8), CByte(48), CByte(25))
        Label13.Location = New Point(478, 488)
        Label13.Name = "Label13"
        Label13.Size = New Size(97, 25)
        Label13.TabIndex = 89
        Label13.Text = "Password:"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.BackColor = Color.Transparent
        Label3.CausesValidation = False
        Label3.Font = New Font("Segoe UI", 36F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label3.ForeColor = Color.White
        Label3.Location = New Point(21, 226)
        Label3.Name = "Label3"
        Label3.Size = New Size(425, 81)
        Label3.TabIndex = 71
        Label3.Text = "Sign Up Now!"
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.BackColor = Color.Transparent
        Label9.CausesValidation = False
        Label9.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label9.ForeColor = Color.White
        Label9.Location = New Point(66, 318)
        Label9.Name = "Label9"
        Label9.Size = New Size(345, 56)
        Label9.TabIndex = 75
        Label9.Text = "Join our community to facilitate more " & vbCrLf & "    impactful internship experiences."
        ' 
        ' Panel1
        ' 
        Panel1.BackgroundImage = CType(resources.GetObject("Panel1.BackgroundImage"), Image)
        Panel1.Controls.Add(Label3)
        Panel1.Controls.Add(Label9)
        Panel1.Location = New Point(0, -3)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(465, 642)
        Panel1.TabIndex = 90
        ' 
        ' FormSignup
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(1020, 634)
        Controls.Add(pnlTermsAndCondition)
        Controls.Add(Panel1)
        Controls.Add(lblTermsAndCondition)
        Controls.Add(Label13)
        Controls.Add(Label8)
        Controls.Add(Label7)
        Controls.Add(Label2)
        Controls.Add(Label12)
        Controls.Add(txtLastName)
        Controls.Add(cmbDepartmentSignUp)
        Controls.Add(cbTermsAndConditions)
        Controls.Add(txtPasswordSignUp)
        Controls.Add(lnklblLogIn)
        Controls.Add(Label4)
        Controls.Add(Label1)
        Controls.Add(Label5)
        Controls.Add(Label6)
        Controls.Add(pctBoxHide)
        Controls.Add(pctBoxExit)
        Controls.Add(PictureBox4)
        Controls.Add(txtEmailSignUp)
        Controls.Add(txtFirstName)
        Controls.Add(btnSIgnUp)
        ForeColor = SystemColors.ControlDarkDark
        FormBorderStyle = FormBorderStyle.None
        Name = "FormSignup"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Student Internship Management System Signup - PLP"
        CType(pctBoxHide, ComponentModel.ISupportInitialize).EndInit()
        CType(pctBoxExit, ComponentModel.ISupportInitialize).EndInit()
        CType(PictureBox4, ComponentModel.ISupportInitialize).EndInit()
        pnlTermsAndCondition.ResumeLayout(False)
        pnlTermsAndCondition.PerformLayout()
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lnklblLogIn As LinkLabel
    Friend WithEvents Label4 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents pctBoxHide As PictureBox
    Friend WithEvents pctBoxExit As PictureBox
    Friend WithEvents PictureBox4 As PictureBox
    Friend WithEvents txtEmailSignUp As TextBox
    Friend WithEvents txtFirstName As TextBox
    Friend WithEvents btnSIgnUp As RoundedButton
    Friend WithEvents txtPasswordSignUp As TextBox
    Friend WithEvents cbTermsAndConditions As CheckBox
    Friend WithEvents lblTermsAndCondition As LinkLabel
    Friend WithEvents pnlTermsAndCondition As Panel
    Friend WithEvents btnBack As RoundedButton
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents txtLastName As TextBox
    Friend WithEvents cmbDepartmentSignUp As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Panel1 As Panel
End Class
