Imports System.IO
Imports System.Reflection
Imports MySql.Data.MySqlClient
Imports Mysqlx.Notice

Public Class FormDashboards
    Dim summaryTable As DataTable
    Private lastRowStudent As Integer = -1
    Private lastRowInternship As Integer = -1
    ' Private Sub PictureBox3_Click_1(sender As Object, e As EventArgs)
    '    OpenFileDialog1.Filter = "images files(*.bmp; *.jpg; *.png) | *.bmp; *.jpg; *.png"
    '   If OpenFileDialog1.ShowDialog = DialogResult.OK Then
    '      pctBoxIcon.Image = Image.FromFile(OpenFileDialog1.FileName)
    'End If
    'End Sub
    Public Property UserEmail As String
    Private PictureChanged As Boolean = False
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Student Icon for Button
        Dim picStudentIcon As New PictureBox()

        picStudentIcon.Name = "picStudentIcon"
        picStudentIcon.Size = New Size(32, 32)
        picStudentIcon.Location = New Point(5, 5)
        picStudentIcon.SizeMode = PictureBoxSizeMode.Zoom
        picStudentIcon.BackColor = Color.Transparent

        'Nilagay ko
        btnStudents.Controls.Add(picStudentIcon)
        picStudentIcon.BringToFront()

        'Button for Hover
        ' Button style
        btnStudents.FlatStyle = FlatStyle.Flat
        btnStudents.UseVisualStyleBackColor = False
        btnStudents.FlatAppearance.BorderSize = 0
        btnStudents.FlatAppearance.MouseOverBackColor = Color.DarkSeaGreen
        btnStudents.FlatAppearance.MouseDownBackColor = Color.Honeydew
        btnStudents.BackColor = Color.FromArgb(188, 221, 203)

        'Panel add Edit Location - STUDENT
        'Add
        pnlAddNewStudentRecord.Location = New Point(90, 78)
        pnlAddNewStudentRecord.Size = New Size(1100, 750)
        'Edit
        pnlEditStudentRecord.Location = New Point(90, 78)
        pnlEditStudentRecord.Size = New Size(1100, 750)

        'Panel add Edit Location - Internship
        'Update
        pnlUpdateInternshipRecord.Location = New Point(104, 101)
        pnlUpdateInternshipRecord.Size = New Size(1083, 700)


        'Panel add Edit Location - Evaluation
        'Add
        pnlAddNewInternshipEvaluationRecord.Location = New Point(90, 78)
        pnlAddNewInternshipEvaluationRecord.Size = New Size(1100, 750)
        'Edit
        pnlEditInternshipEvaluationRecord.Location = New Point(90, 78)
        pnlEditInternshipEvaluationRecord.Size = New Size(1100, 750)

        'Panel add Edit Location - Company
        'Add
        pnlAddNewCompanyRecord.Location = New Point(90, 78)
        pnlAddNewCompanyRecord.Size = New Size(1100, 750)
        'Edit
        pnlEditCompanyRecord.Location = New Point(90, 78)
        pnlEditCompanyRecord.Size = New Size(1100, 750)


        'Panel add Edit Location - Sub Buttons
        'Add
        pnlAddNewCompanyandCompanyContact.Location = New Point(317, 256)
        pnlAddNewCompanyandCompanyContact.Size = New Size(565, 340)
        'Edit
        pnlEditCompanyCompanyContact.Location = New Point(317, 256)
        pnlEditCompanyCompanyContact.Size = New Size(565, 340)

        'Panel add Edit Location - Company Contact
        'Add
        pnlAddNewCompanyContactRecord.Location = New Point(90, 78)
        pnlAddNewCompanyContactRecord.Size = New Size(1100, 750)
        'Edit
        pnlEditCompanyContactRecord.Location = New Point(90, 78)
        pnlEditCompanyContactRecord.Size = New Size(1100, 750)

        'Panel add Edit Location - Faculty
        'Add
        pnlAddNewFacultyRecord.Location = New Point(90, 78)
        pnlAddNewFacultyRecord.Size = New Size(1100, 750)
        'Edit
        pnlEditFacultyRecord.Location = New Point(90, 78)
        pnlEditFacultyRecord.Size = New Size(1100, 750)

        'Panel add Edit Location - Visit
        'Add
        pnlAddNewVisitRecord.Location = New Point(90, 78)
        pnlAddNewVisitRecord.Size = New Size(1100, 750)
        'Edit
        pnlEditVisitRecord.Location = New Point(90, 78)
        pnlEditVisitRecord.Size = New Size(1100, 750)

        'For Rounding Home Button
        Dim path As New Drawing2D.GraphicsPath()
        path.AddEllipse(0, 0, btnHome.Width, btnHome.Height)
        btnHome.Region = New Region(path)

        'Home Load
        pnlHome.Show()
        displayCount()

        'For buttons
        hidePanel()


        ' Set icon sa form at taskbar
        'Me.Icon = New System.Drawing.Icon("C:\Users\acer\Downloads\buttons or icons\internship_icon.ico")
        Me.ShowIcon = True




        'FOR COLUMNS STYLES INTERNSHIPS
        ' 1. LOAD DATA FROM DATABASE
        Dim dt As DataTable = searchInterTable("")   ' or full load query mo

        ' 2. SET DATASOURCE OF DGV
        dgvInternshipFiles4.DataSource = dt

        ' 3. APPLY STYLE (COLUMNS EXIST NA)
        dgvInternshipFiles4Styles(dgvInternshipFiles4)


        'FOR CMB HOVER

        For Each cmb As ComboBox In {cmbGender2, cmbGender3,
                                     cmbSection2, cmbSection3,
                                     cmbDepartment2, cmbDepartment3,
                                     cmbCourse2, cmbCourse3,
                                     cmbCompanyInternship, cmbCompanyContactInternship,
                                     cmbStatusUpdateInternship, cmbStatus6}
            cmb.DrawMode = DrawMode.OwnerDrawFixed
            AddHandler cmb.DrawItem, AddressOf DrawComboItem


            cmb.DrawMode = DrawMode.OwnerDrawFixed
            cmb.DropDownStyle = ComboBoxStyle.DropDownList
            AddHandler cmb.DrawItem, AddressOf DrawComboItem
        Next
        cmbCompanyInternship.DropDownStyle = ComboBoxStyle.DropDown
        cmbCompanyContactInternship.DropDownStyle = ComboBoxStyle.DropDown

        'LOAD OF DGV FOR COMPANY
        LoadCompanyDataToDGV()

        LoadCompanyContactsTable()

        LoadVisitLogTable()

        LoadInternshipSummaryTable()

        LoadCompanyData()

        LoadCompanyContactsData()

        LoadFacultyData()

        LoadVisitLogsData()

        LoadStudentSummaryData()

        dgvCompanyContactFiles.DataSource = LoadAllCompanyContacts()


        'load for total
        lblSummaryFiles.Text = countStudentRecord()

        'For loading name faculty
        lblHomeFName.Text = SessionModule.LoggedFirstname

        dgvCompanyContactFiles.DataSource = LoadAllCompanyContacts()





    End Sub


    Private Sub hidePanel()
        pnlStudentInformation.Hide()
        pnlAddNewStudentRecord.Hide()
        pnlEditStudentRecord.Hide()
        pnlInternshipInformation.Hide()
        pnlUpdateInternshipRecord.Hide()
        pnlEvaluationInformation.Hide()
        pnlAddNewInternshipEvaluationRecord.Hide()
        pnlEditInternshipEvaluationRecord.Hide()
        pnlCompanyInformation.Hide()
        pnlAddNewCompanyandCompanyContact.Hide()
        pnlEditCompanyCompanyContact.Hide()
        pnlAddNewCompanyRecord.Hide()
        pnlEditCompanyRecord.Hide()
        pnlCompanyContactInformation.Hide()
        pnlAddNewCompanyContactRecord.Hide()
        pnlEditCompanyContactRecord.Hide()
        pnlFacultyInformation.Hide()
        pnlAddNewFacultyRecord.Hide()
        pnlEditFacultyRecord.Hide()
        pnlVisitInformation.Hide()
        pnlAddNewVisitRecord.Hide()
        pnlEditVisitRecord.Hide()
        pnlSummaryReport.Hide()
    End Sub

    Private Sub displayCount()
        lblHomePending.Text = countPendingIntern()
        lblHomeOngoing.Text = countOngoingIntern()
        lblHomeCompleted.Text = countCompletedIntern()
        lblHomeTotalInterns.Text = countIntern()
    End Sub

    Private Sub clearBox(ParamArray boxes() As TextBox)
        For Each txt In boxes
            txt.Clear()

        Next

    End Sub

    Private Sub loadStudentRecord()
        Dim dt As DataTable = loadTable("SELECT 
                                        s.student_id AS 'Student ID',
                                        s.first_name AS 'First Name',
                                        s.last_name AS 'Last Name',
                                        s.gender AS 'Gender',
                                        s.section_name AS 'Section',
                                        s.contact_no AS 'Contact Number',
                                        s.email AS 'Email',
                                        d.department_name AS 'Department',
                                        c.course_name AS 'Course'
                                      FROM student s
                                      INNER JOIN department d ON s.department_id = d.department_id
                                      INNER JOIN course c ON s.course_id = c.course_id;")

        ' Add Hidden column if not exists
        If Not dt.Columns.Contains("Hidden") Then dt.Columns.Add("Hidden", GetType(Boolean))
        For Each row As DataRow In dt.Rows
            row("Hidden") = False
        Next

        ' Column widths dictionary
        Dim widths As New Dictionary(Of String, Integer) From {
        {"Student ID", 120},
        {"First Name", 200},
        {"Last Name", 200},
        {"Gender", 150},
        {"Section", 200},
        {"Contact Number", 150},
        {"Email", 350},
        {"Department", 350},
        {"Course", 350}
    }

        ' Apply styling & hover
        DGVHelper.BindAndStyleFilesDGV(dgvStudentFiles, dt, widths)


    End Sub

    Private Sub loadInternRecord()
        'Dim dt As DataTable = loadTable("SELECT * FROM vinternship_record") use this to hide the pending
        Dim dt As DataTable = loadTable("SELECT * FROM vall_internship")
        dgvInternshipFiles4.DataSource = dt
    End Sub


    Private Sub loadFacultyRecord()
        Dim dt As DataTable = loadTable("SELECT * FROM vfaculty_table")
        dgvFacultyFiles.DataSource = dt
    End Sub

    'FOR COLUMNS STYLES INTERNSHIPS
    Private Sub dgvInternshipFiles4Styles(dgv As DataGridView)

        If dgv.Columns.Count = 0 Then Exit Sub  ' ← Prevent crash

        dgv.Columns("Internship ID").Width = 170
        dgv.Columns("First Name").Width = 200
        dgv.Columns("Last Name").Width = 200
        dgv.Columns("Company Name").Width = 300
        dgv.Columns("Supervisor Last Name").Width = 200
        dgv.Columns("Supervisor Contact").Width = 150
        dgv.Columns("Start_Date").Width = 170
        dgv.Columns("End_Date").Width = 170
        dgv.Columns("Status").Width = 150

        dgv.EnableHeadersVisualStyles = False
        dgv.AdvancedColumnHeadersBorderStyle.All = DataGridViewAdvancedCellBorderStyle.None

        With dgvInternshipFiles4
            .EnableHeadersVisualStyles = False
            .RowHeadersDefaultCellStyle.BackColor = Color.MintCream   ' Palitan ng gusto mong kulay
            .RowHeadersDefaultCellStyle.SelectionBackColor = Color.LightYellow
            .RowHeadersWidth = 20
        End With


    End Sub

    'FOR HOVER EFFECT DGVINTERNSHIP 


    Private Sub dgvInternshipFiles4_CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgvInternshipFiles4.CellMouseEnter
        If e.RowIndex >= 0 Then

            ' Ibabalik yung last row sa original color
            If lastRowInternship >= 0 Then
                dgvInternshipFiles4.Rows(lastRowInternship).DefaultCellStyle.BackColor = Color.White

            End If

            ' Highlight current hovered row
            dgvInternshipFiles4.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.Honeydew

            lastRowInternship = e.RowIndex
        End If
    End Sub

    Private Sub dgvInternshipFiles_CellMouseLeave(sender As Object, e As DataGridViewCellEventArgs) Handles dgvInternshipFiles4.CellMouseLeave
        If lastRowInternship >= 0 Then
            dgvInternshipFiles4.Rows(lastRowInternship).DefaultCellStyle.BackColor = Color.White
        End If
    End Sub

    Private Sub dgvInternshipFiles4_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvStudentFiles.DataBindingComplete

        dgvInternshipFiles4.ClearSelection()
        dgvInternshipFiles4.CurrentCell = Nothing
        Me.ActiveControl = Nothing


    End Sub


    Private Function validInputbox(ParamArray boxes() As TextBox) As Boolean
        For Each txt As TextBox In boxes
            If txt.Text.Trim() = "" Then
                MessageBox.Show("Please Fill Out The Boxes", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        Next


        Return True
    End Function


    Private Function validStudentInputBx(email As TextBox, ParamArray txtBoxes() As TextBox) As Boolean

        For Each tb In txtBoxes
            If tb.Text = "" Or email.Text = "" Then
                MessageBox.Show("Please Fill Out The Boxes", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        Next

        If Not email.Text.Contains("@plpasig.edu.ph") Then
            MessageBox.Show("Email must be a School email", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtEmail2.Clear()
            Return False
        End If

        Return True
    End Function

    Private Sub loadGenderComboBx(cmb As ComboBox)
        Dim dt As New DataTable()
        dt.Columns.Add("value")
        dt.Rows.Add("Male")
        dt.Rows.Add("Female")
        dt.Rows.Add("Other")

        cmb.DataSource = dt
        cmb.DisplayMember = "value"
        cmb.ValueMember = "value"
    End Sub

    Private Sub loadDepartmentComboBx(cmb As ComboBox)
        Dim dt As DataTable = LoadCombo("SELECT department_id, department_name FROM department")

        cmb.DataSource = dt
        cmb.DisplayMember = "department_name" 'what user sees
        cmb.ValueMember = "department_id"     'actual value you use in DB
    End Sub

    Private Sub loadSectionComboBx(cmb As ComboBox)
        Dim dt As DataTable = LoadCombo("SELECT section_name FROM section")

        cmb.DataSource = dt
        cmb.DisplayMember = "section_name" 'what user sees
        cmb.ValueMember = "section_name"   'actual value you use in DB
    End Sub

    Private Sub loadCourseComboBx(cmb As ComboBox)
        Dim dt As DataTable = LoadCombo("SELECT course_id, course_name FROM course")

        cmb.DataSource = dt
        cmb.DisplayMember = "course_name" 'what user sees
        cmb.ValueMember = "course_id"   'actual value you use in DB

    End Sub


    'Home

    Private Sub pctBoxExit1_Click(sender As Object, e As EventArgs) Handles pctBoxExit1.Click
        Dim result = MessageBox.Show("Are you sure you want to exit the program?",
                                     "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub

    Private Sub pctBoxHide1_Click(sender As Object, e As EventArgs) Handles pctBoxHide1.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        Dim result = MessageBox.Show(
        "Are you sure you want to log out?",
        "Confirm Logout",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question
    )

        If result = DialogResult.Yes Then
            ' Show login form
            FormLogin.Show()
            Hide()
        End If
    End Sub

    'BUTTON HOVER
    'BUTTON LOGOUT  HOVER STUDENTS
    Private Sub btnLogout_MouseEnter(sender As Object, e As EventArgs) Handles btnLogout.MouseEnter
        btnLogout.BackColor = Color.FromArgb(97, 144, 118)

    End Sub

    Private Sub btnLogout_MouseLeave(sender As Object, e As EventArgs) Handles btnLogout.MouseLeave
        btnLogout.BackColor = Color.FromArgb(8, 48, 25)
    End Sub

    Private Sub btnLogout_MouseDown(sender As Object, e As MouseEventArgs) Handles btnLogout.MouseDown
        btnLogout.BackColor = Color.DarkSeaGreen
    End Sub

    Private Sub btnLogout_MouseUp(sender As Object, e As MouseEventArgs) Handles btnLogout.MouseUp
        btnLogout.BackColor = Color.FromArgb(97, 144, 118)
    End Sub


    Private Sub lblName_Click(sender As Object, e As EventArgs) Handles lblHomeFName.Click

    End Sub

    Private Sub lblSignInAs_Click(sender As Object, e As EventArgs)

    End Sub

    'BUTTON HOVER

    'HOME BUTTON HOVER
    Private Sub btnHome_MouseEnter(sender As Object, e As EventArgs) Handles btnHome.MouseEnter
        btnHome.BackColor = Color.FromArgb(192, 255, 192)
        picHome.BackColor = Color.FromArgb(192, 255, 192)
    End Sub

    Private Sub btnHome_MouseLeave(sender As Object, e As EventArgs) Handles btnHome.MouseLeave
        btnHome.BackColor = Color.FromArgb(218, 239, 228)
        picHome.BackColor = Color.FromArgb(218, 239, 228)

    End Sub

    Private Sub btnHome_MouseDown(sender As Object, e As MouseEventArgs) Handles btnHome.MouseDown
        btnHome.BackColor = Color.Honeydew
        picHome.BackColor = Color.Honeydew '
    End Sub

    Private Sub btnHome_MouseUp(sender As Object, e As MouseEventArgs) Handles btnHome.MouseUp
        btnHome.BackColor = Color.FromArgb(192, 255, 192)
        picHome.BackColor = Color.FromArgb(192, 255, 192)
    End Sub
    Private Sub btnHome_Click(sender As Object, e As EventArgs) Handles btnHome.Click
        hidePanel()
        pnlHome.Show()
        displayCount()
    End Sub
    Private Sub lblHomeDashboard_Click(sender As Object, e As EventArgs) Handles lblHomeDashboard.Click
        hidePanel()
        pnlHome.Show()
        displayCount()


    End Sub

    Private Sub btnStudents_Click(sender As Object, e As EventArgs) Handles btnStudents.Click
        hidePanel()
        pnlStudentInformation.Show()
        lblTotalRecords1.Text = countStudentRecord()
        loadStudentRecord()
        pnlHome.Hide()

        ' Clear DataGridView initially
        dgvStudentSearch.DataSource = Nothing
    End Sub
    'BUTTON HOVER

    'STUDENT BUTTON HOVER
    Private Sub btnStudents_MouseEnter(sender As Object, e As EventArgs) Handles btnStudents.MouseEnter
        btnStudents.BackColor = Color.FromArgb(192, 255, 192)
        picStudentIcon.BackColor = Color.FromArgb(192, 255, 192)
    End Sub

    Private Sub btnStudents_MouseLeave(sender As Object, e As EventArgs) Handles btnStudents.MouseLeave
        btnStudents.BackColor = Color.FromArgb(218, 239, 228)
        picStudentIcon.BackColor = Color.FromArgb(218, 239, 228)

    End Sub

    Private Sub btnStudents_MouseDown(sender As Object, e As MouseEventArgs) Handles btnStudents.MouseDown
        btnStudents.BackColor = Color.Honeydew
        picStudentIcon.BackColor = Color.Honeydew '
    End Sub

    Private Sub btnStudents_MouseUp(sender As Object, e As MouseEventArgs) Handles btnStudents.MouseUp
        btnStudents.BackColor = Color.FromArgb(192, 255, 192)
        picStudentIcon.BackColor = Color.FromArgb(192, 255, 192)
    End Sub


    Private Sub btnInternships_Click(sender As Object, e As EventArgs) Handles btnInternships.Click
        hidePanel()
        pnlHome.Hide()
        lblTotalRecords4.Text = countIntern()
        pnlInternshipInformation.Show()
        loadInternRecord()
    End Sub

    'BUTTON HOVER

    'INTERNSHIPS BUTTON HOVER 
    Private Sub btnInternships_MouseEnter(sender As Object, e As EventArgs) Handles btnInternships.MouseEnter
        btnInternships.BackColor = Color.FromArgb(192, 255, 192)
        picInternshipIcon.BackColor = Color.FromArgb(192, 255, 192)
    End Sub

    Private Sub btnInternships_MouseLeave(sender As Object, e As EventArgs) Handles btnInternships.MouseLeave
        btnInternships.BackColor = Color.FromArgb(218, 239, 228)
        picInternshipIcon.BackColor = Color.FromArgb(218, 239, 228)

    End Sub

    Private Sub btnInternships_MouseDown(sender As Object, e As MouseEventArgs) Handles btnInternships.MouseDown
        btnInternships.BackColor = Color.Honeydew
        picInternshipIcon.BackColor = Color.Honeydew '
    End Sub

    Private Sub btnInternships_MouseUp(sender As Object, e As MouseEventArgs) Handles btnInternships.MouseUp
        btnInternships.BackColor = Color.FromArgb(192, 255, 192)
        picInternshipIcon.BackColor = Color.FromArgb(192, 255, 192)
    End Sub


    Private Sub btnEvaluation_Click(sender As Object, e As EventArgs) Handles btnEvaluation.Click
        hidePanel()
        pnlEvaluationInformation.Show()
        lblTotalRecords5.Text = countEvaluation()
        pnlHome.Hide()
        LoadEvaluationDGV()
    End Sub

    'BUTTON HOVER

    'EVALUATION BUTTON HOVER 
    Private Sub btnEvaluation_MouseEnter(sender As Object, e As EventArgs) Handles btnEvaluation.MouseEnter
        btnEvaluation.BackColor = Color.FromArgb(192, 255, 192)
        picEvaluationIcon.BackColor = Color.FromArgb(192, 255, 192)
    End Sub

    Private Sub btnEvaluation_MouseLeave(sender As Object, e As EventArgs) Handles btnEvaluation.MouseLeave
        btnEvaluation.BackColor = Color.FromArgb(218, 239, 228)
        picEvaluationIcon.BackColor = Color.FromArgb(218, 239, 228)

    End Sub

    Private Sub btnEvaluation_MouseDown(sender As Object, e As MouseEventArgs) Handles btnEvaluation.MouseDown
        btnEvaluation.BackColor = Color.Honeydew
        picEvaluationIcon.BackColor = Color.Honeydew '
    End Sub

    Private Sub btnEvaluation_MouseUp(sender As Object, e As MouseEventArgs) Handles btnEvaluation.MouseUp
        btnEvaluation.BackColor = Color.FromArgb(192, 255, 192)
        picEvaluationIcon.BackColor = Color.FromArgb(192, 255, 192)
    End Sub


    Private Sub btnCompany_Click(sender As Object, e As EventArgs) Handles btnCompany.Click
        hidePanel()
        pnlCompanyInformation.Show()
        lblTotalRecords8.Text = countCompany()
        lblTotalRecords11.Text = countCompanyContact()
        pnlHome.Hide()
    End Sub

    'BUTTON HOVER

    'COMPANY BUTTON HOVER 
    Private Sub btnCompany_MouseEnter(sender As Object, e As EventArgs) Handles btnCompany.MouseEnter
        btnCompany.BackColor = Color.FromArgb(192, 255, 192)
        picCompanyIcon.BackColor = Color.FromArgb(192, 255, 192)
    End Sub

    Private Sub btnCompany_MouseLeave(sender As Object, e As EventArgs) Handles btnCompany.MouseLeave
        btnCompany.BackColor = Color.FromArgb(218, 239, 228)
        picCompanyIcon.BackColor = Color.FromArgb(218, 239, 228)

    End Sub

    Private Sub btnCompany_MouseDown(sender As Object, e As MouseEventArgs) Handles btnCompany.MouseDown
        btnCompany.BackColor = Color.Honeydew
        picCompanyIcon.BackColor = Color.Honeydew '
    End Sub

    Private Sub btnCompany_MouseUp(sender As Object, e As MouseEventArgs) Handles btnCompany.MouseUp
        btnCompany.BackColor = Color.FromArgb(192, 255, 192)
        picCompanyIcon.BackColor = Color.FromArgb(192, 255, 192)
    End Sub

    Private Sub btnFaculty_Click(sender As Object, e As EventArgs) Handles btnFaculty.Click
        hidePanel()
        pnlFacultyInformation.Show()
        lblTotalRecords14.Text = countFaculty()
        pnlHome.Hide()
        loadFacultyRecord()
    End Sub

    'BUTTON HOVER

    'FACULTY BUTTON HOVER 
    Private Sub btnFaculty_MouseEnter(sender As Object, e As EventArgs) Handles btnFaculty.MouseEnter
        btnFaculty.BackColor = Color.FromArgb(192, 255, 192)
        picFacultyIcon.BackColor = Color.FromArgb(192, 255, 192)
    End Sub

    Private Sub btnFaculty_MouseLeave(sender As Object, e As EventArgs) Handles btnFaculty.MouseLeave
        btnFaculty.BackColor = Color.FromArgb(218, 239, 228)
        picFacultyIcon.BackColor = Color.FromArgb(218, 239, 228)

    End Sub

    Private Sub btnFaculty_MouseDown(sender As Object, e As MouseEventArgs) Handles btnFaculty.MouseDown
        btnFaculty.BackColor = Color.Honeydew
        picFacultyIcon.BackColor = Color.Honeydew '
    End Sub

    Private Sub btnFaculty_MouseUp(sender As Object, e As MouseEventArgs) Handles btnFaculty.MouseUp
        btnFaculty.BackColor = Color.FromArgb(192, 255, 192)
        picFacultyIcon.BackColor = Color.FromArgb(192, 255, 192)
    End Sub


    Private Sub btnVisitLog_Click(sender As Object, e As EventArgs) Handles btnVisitLog.Click
        hidePanel()
        pnlVisitInformation.Show()
        lblTotalRecords17.Text = countVisitlog()
        pnlHome.Hide()
    End Sub

    'BUTTON HOVER

    'VISIT BUTTON HOVER 
    Private Sub btnVisitLog_MouseEnter(sender As Object, e As EventArgs) Handles btnVisitLog.MouseEnter
        btnVisitLog.BackColor = Color.FromArgb(192, 255, 192)
        picVisitIcon.BackColor = Color.FromArgb(192, 255, 192)
    End Sub

    Private Sub btnVisitLog_MouseLeave(sender As Object, e As EventArgs) Handles btnVisitLog.MouseLeave
        btnVisitLog.BackColor = Color.FromArgb(218, 239, 228)
        picVisitIcon.BackColor = Color.FromArgb(218, 239, 228)

    End Sub

    Private Sub btnVisitLog_MouseDown(sender As Object, e As MouseEventArgs) Handles btnVisitLog.MouseDown
        btnVisitLog.BackColor = Color.Honeydew
        picVisitIcon.BackColor = Color.Honeydew '
    End Sub

    Private Sub btnVisitLog_MouseUp(sender As Object, e As MouseEventArgs) Handles btnVisitLog.MouseUp
        btnVisitLog.BackColor = Color.FromArgb(192, 255, 192)
        picVisitIcon.BackColor = Color.FromArgb(192, 255, 192)
    End Sub

    Private Sub btnSummaryReport_Click(sender As Object, e As EventArgs) Handles btnSummaryReport.Click
        hidePanel()
        pnlSummaryReport.Show()
        pnlHome.Hide()
    End Sub


    'BUTTON HOVER

    'SUMMARY BUTTON HOVER 
    Private Sub btnSummaryReport_MouseEnter(sender As Object, e As EventArgs) Handles btnSummaryReport.MouseEnter
        btnSummaryReport.BackColor = Color.FromArgb(192, 255, 192)
        picSummary.BackColor = Color.FromArgb(192, 255, 192)
    End Sub

    Private Sub btnSummaryReport_MouseLeave(sender As Object, e As EventArgs) Handles btnSummaryReport.MouseLeave
        btnSummaryReport.BackColor = Color.FromArgb(218, 239, 228)
        picSummary.BackColor = Color.FromArgb(218, 239, 228)

    End Sub

    Private Sub btnSummaryReport_MouseDown(sender As Object, e As MouseEventArgs) Handles btnSummaryReport.MouseDown
        btnSummaryReport.BackColor = Color.Honeydew
        picSummary.BackColor = Color.Honeydew '
    End Sub

    Private Sub btnSummaryReport_MouseUp(sender As Object, e As MouseEventArgs) Handles btnSummaryReport.MouseUp
        btnSummaryReport.BackColor = Color.FromArgb(192, 255, 192)
        picSummary.BackColor = Color.FromArgb(192, 255, 192)
    End Sub

    Private Sub pnlTtotalInterns_Paint(sender As Object, e As PaintEventArgs) Handles pnlTtotalInterns.Paint

    End Sub

    Private Sub pnlCompleted_Paint(sender As Object, e As PaintEventArgs) Handles pnlCompleted.Paint

    End Sub

    Private Sub pnlOnGoing_Paint(sender As Object, e As PaintEventArgs) Handles pnlOnGoing.Paint

    End Sub

    Private Sub pnlDataModel1_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub pnlDataModel2_Paint(sender As Object, e As PaintEventArgs)

    End Sub


    'Student Logs
    '   Private Sub pnlStudentLogs_Paint(sender As Object, e As PaintEventArgs) Handles pnlStudentLogs.Paint
    ' End Sub


    Private Sub btnSearch1_Click(sender As Object, e As EventArgs) Handles btnSearch1.Click
        If txtSearchStudentID1.Text = "" Then
            MessageBox.Show("Please Enter Student ID", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim dt As DataTable = searchStudentTable(txtSearchStudentID1.Text.Trim())

        ' Bind & style only when search button is pressed
        Dim widths As New Dictionary(Of String, Integer) From {
            {"Student ID", 120},
            {"First Name", 200},
            {"Last Name", 200},
            {"Gender", 150},
            {"Section", 200},
            {"Contact Number", 150},
            {"Email", 350},
            {"Department", 350},
            {"Course", 350}
        }

        DGVHelper.BindAndStyleFilesDGV(dgvStudentSearch, dt, widths)
    End Sub


    'BUTTON HOVER
    'BUTTON SEARCH 1 HOVER STUDENTS
    Private Sub btnSearch1_MouseEnter(sender As Object, e As EventArgs) Handles btnSearch1.MouseEnter
        btnSearch1.BackColor = Color.FromArgb(8, 48, 25)

    End Sub

    Private Sub btnSearch1_MouseLeave(sender As Object, e As EventArgs) Handles btnSearch1.MouseLeave
        btnSearch1.BackColor = Color.FromArgb(97, 144, 118)
    End Sub

    Private Sub btnSearch1_MouseDown(sender As Object, e As MouseEventArgs) Handles btnSearch1.MouseDown
        btnSearch1.BackColor = Color.DarkSeaGreen
    End Sub

    Private Sub btnSearch1_MouseUp(sender As Object, e As MouseEventArgs) Handles btnSearch1.MouseUp
        btnSearch1.BackColor = Color.FromArgb(8, 48, 25)
    End Sub

    Private Sub btnImport1_Click(sender As Object, e As EventArgs) Handles btnImport1.Click
        Dim ofd As New OpenFileDialog
        Dim csvAdded As Boolean
        ofd.Filter = "CSV Files (*.csv)|*.csv"

        If ofd.ShowDialog() = DialogResult.OK Then
            Dim dt As DataTable = LoadCSV(ofd.FileName)
            csvAdded = addStudentsFromCsv(dt)
        End If

        If Not csvAdded Then
            MessageBox.Show("Importing Failed, Check The Format Properly", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        MessageBox.Show("Import Success", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        loadStudentRecord()
        lblTotalRecords1.Text = countStudentRecord()

    End Sub

    Private Sub btnAdd1_Click(sender As Object, e As EventArgs) Handles btnAdd1.Click
        pnlAddNewStudentRecord.Show()
        txtStudentID2.Text = GenerateStudentID()
        loadCourseComboBx(cmbCourse2)
        loadDepartmentComboBx(cmbDepartment2)
        loadSectionComboBx(cmbSection2)
        loadGenderComboBx(cmbGender2)

    End Sub

    'BUTTON HOVER
    'BUTTON ADD 1 HOVER STUDENTS
    Private Sub btnAdd1_MouseEnter(sender As Object, e As EventArgs) Handles btnAdd1.MouseEnter
        btnAdd1.BackColor = Color.FromArgb(97, 144, 118)

    End Sub

    Private Sub btnAdd1_MouseLeave(sender As Object, e As EventArgs) Handles btnAdd1.MouseLeave
        btnAdd1.BackColor = Color.FromArgb(8, 48, 25)
    End Sub

    Private Sub btnAdd1_MouseDown(sender As Object, e As MouseEventArgs) Handles btnAdd1.MouseDown
        btnAdd1.BackColor = Color.DarkSeaGreen
    End Sub

    Private Sub btnAdd1_MouseUp(sender As Object, e As MouseEventArgs) Handles btnAdd1.MouseUp
        btnAdd1.BackColor = Color.FromArgb(97, 144, 118)
    End Sub

    Private Sub btnEdit1_Click(sender As Object, e As EventArgs) Handles btnEdit1.Click
        pnlEditStudentRecord.Show()
        loadCourseComboBx(cmbCourse3)
        loadDepartmentComboBx(cmbDepartment3)
        loadSectionComboBx(cmbSection3)
        loadGenderComboBx(cmbGender3)
    End Sub

    'BUTTON HOVER
    'BUTTON EDIT 1 HOVER STUDENTS
    Private Sub btnEdit1_MouseEnter(sender As Object, e As EventArgs) Handles btnEdit1.MouseEnter
        btnEdit1.BackColor = Color.FromArgb(97, 144, 118)

    End Sub

    Private Sub btnEdit1_MouseLeave(sender As Object, e As EventArgs) Handles btnEdit1.MouseLeave
        btnEdit1.BackColor = Color.FromArgb(8, 48, 25)
    End Sub

    Private Sub btnEdit1_MouseDown(sender As Object, e As MouseEventArgs) Handles btnEdit1.MouseDown
        btnEdit1.BackColor = Color.DarkSeaGreen
    End Sub

    Private Sub btnEdit1_MouseUp(sender As Object, e As MouseEventArgs) Handles btnEdit1.MouseUp
        btnEdit1.BackColor = Color.FromArgb(97, 144, 118)
    End Sub

    Private Sub btnHide1_Click(sender As Object, e As EventArgs) Handles btnHide1.Click
        If dgvStudentFiles.SelectedRows.Count = 0 Then
            MessageBox.Show(
            "Please Select Row to Hide", "DGV", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            Exit Sub
        End If

        Dim dv As DataView = CType(dgvStudentFiles.DataSource, DataView)
        Dim dt As DataTable = dv.Table

        For Each row As DataGridViewRow In dgvStudentFiles.SelectedRows
            Dim dr As DataRow = CType(row.DataBoundItem, DataRowView).Row
            dr("Hidden") = True
        Next

        dv.RowFilter = "Hidden = False"

    End Sub
    Private Sub btnShow1_Click_1(sender As Object, e As EventArgs) Handles btnShow1.Click
        Dim dv As DataView = CType(dgvStudentFiles.DataSource, DataView)
        dv.RowFilter = ""
    End Sub


    'Add New Student Record

    ' Private Sub pnlAddNewStudentRecord_Paint(sender As Object, e As PaintEventArgs) Handles pnlAddNewStudentRecord.Paint

    ' End Sub

    Private Sub btnCancel2_Click(sender As Object, e As EventArgs) Handles btnCancel2.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to cancel?", "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If result = DialogResult.Yes Then
            pnlStudentInformation.Show()
            pnlAddNewStudentRecord.Hide()
        End If
    End Sub

    'BUTTON HOVER
    'BUTTON CANCEL 2 HOVER STUDENTS
    Private Sub btnCancel2_MouseEnter(sender As Object, e As EventArgs) Handles btnCancel2.MouseEnter
        btnCancel2.BackColor = Color.FromArgb(8, 48, 25)

    End Sub

    Private Sub btnCancel2_MouseLeave(sender As Object, e As EventArgs) Handles btnCancel2.MouseLeave
        btnCancel2.BackColor = Color.FromArgb(97, 144, 118)
    End Sub

    Private Sub btnCancel2_MouseDown(sender As Object, e As MouseEventArgs) Handles btnCancel2.MouseDown
        btnCancel2.BackColor = Color.DarkSeaGreen
    End Sub

    Private Sub btnCancel2_MouseUp(sender As Object, e As MouseEventArgs) Handles btnCancel2.MouseUp
        btnCancel2.BackColor = Color.FromArgb(8, 48, 25)
    End Sub

    Private Sub btnAdd2_Click(sender As Object, e As EventArgs) Handles btnAdd2.Click
        If Not validStudentInputBx(txtEmail2, txtFName2, txtLName2, txtContactNumber2) Then Exit Sub

        Dim result As DialogResult = MessageBox.Show("Do you want to add this record?", "Confirm Add", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.No Then
            pnlStudentInformation.Show()
            pnlAddNewStudentRecord.Hide()
            Exit Sub
        End If

        Dim isAdded As Boolean = addStudentRecord(txtStudentID2.Text, txtFName2.Text, txtLName2.Text, cmbGender2.SelectedValue,
                                                  cmbSection2.SelectedValue, txtContactNumber2.Text, txtEmail2.Text,
                                                  cmbDepartment2.SelectedValue, cmbCourse2.SelectedValue)

        If Not isAdded Then
            MessageBox.Show("Adding Failed", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        clearBox(txtStudentID2, txtFName2, txtLName2, txtContactNumber2, txtEmail2)
        MessageBox.Show("Successfully Added", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        loadStudentRecord()

    End Sub

    'BUTTON HOVER
    'BUTTON ADD 2 HOVER STUDENTS
    Private Sub btnAdd2_MouseEnter(sender As Object, e As EventArgs) Handles btnAdd2.MouseEnter
        btnAdd2.BackColor = Color.FromArgb(97, 144, 118)

    End Sub

    Private Sub btnAdd2_MouseLeave(sender As Object, e As EventArgs) Handles btnAdd2.MouseLeave
        btnAdd2.BackColor = Color.FromArgb(8, 48, 25)
    End Sub

    Private Sub btnAdd2_MouseDown(sender As Object, e As MouseEventArgs) Handles btnAdd2.MouseDown
        btnAdd2.BackColor = Color.DarkSeaGreen
    End Sub

    Private Sub btnAdd2_MouseUp(sender As Object, e As MouseEventArgs) Handles btnAdd2.MouseUp
        btnAdd2.BackColor = Color.FromArgb(97, 144, 118)
    End Sub


    'Edit STudent Record

    ' Private Sub pnlEditStudentRecord_Paint(sender As Object, e As PaintEventArgs) Handles pnlEditStudentRecord.Paint

    ' End Sub



    Private Sub btnSearch3_Click(sender As Object, e As EventArgs) Handles btnSearch3.Click
        Dim dT As DataTable = GetStudentInfo(txtSearchID3.Text)

        If dT.Rows.Count = 0 Then
            MessageBox.Show("Student not found", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim row As DataRow = dT.Rows(0)

        txtStudentID3.Text = row("student_id").ToString
        txtFName3.Text = row("first_name").ToString
        txtLName3.Text = row("last_name").ToString
        cmbGender3.SelectedValue = row("gender").ToString
        cmbSection3.SelectedValue = row("section_name").ToString
        txtContactNumber3.Text = row("contact_no").ToString
        txtEmail3.Text = row("email").ToString
        cmbDepartment3.SelectedValue = row("department_id").ToString
        cmbCourse3.SelectedValue = row("course_id").ToString



    End Sub


    'BUTTON HOVER
    'BUTTON SEARCH 3 HOVER STUDENTS
    Private Sub btnSearch3_MouseEnter(sender As Object, e As EventArgs) Handles btnSearch3.MouseEnter
        btnSearch3.BackColor = Color.FromArgb(8, 48, 25)

    End Sub

    Private Sub btnSearch3_MouseLeave(sender As Object, e As EventArgs) Handles btnSearch3.MouseLeave
        btnSearch3.BackColor = Color.FromArgb(97, 144, 118)
    End Sub

    Private Sub btnSearch3_MouseDown(sender As Object, e As MouseEventArgs) Handles btnSearch3.MouseDown
        btnSearch3.BackColor = Color.DarkSeaGreen
    End Sub

    Private Sub btnSearch3_MouseUp(sender As Object, e As MouseEventArgs) Handles btnSearch3.MouseUp
        btnSearch3.BackColor = Color.FromArgb(8, 48, 25)
    End Sub

    Private Sub txtStudentID3_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub txtFName3_TextChanged(sender As Object, e As EventArgs) Handles txtFName3.TextChanged

    End Sub

    Private Sub txtLName3_TextChanged(sender As Object, e As EventArgs) Handles txtLName3.TextChanged

    End Sub

    Private Sub cmbGender3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbGender3.SelectedIndexChanged

    End Sub


    Private Sub txtContactNumber3_TextChanged(sender As Object, e As EventArgs) Handles txtContactNumber3.TextChanged

    End Sub

    Private Sub txtEmail3_TextChanged(sender As Object, e As EventArgs) Handles txtEmail3.TextChanged

    End Sub

    Private Sub cmbDepartment3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDepartment3.SelectedIndexChanged

    End Sub

    Private Sub cmbProgram3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCourse3.SelectedIndexChanged

    End Sub

    Private Sub btnCancel3_Click(sender As Object, e As EventArgs) Handles btnCancel3.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to cancel?", "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If result = DialogResult.Yes Then
            pnlStudentInformation.Show()
            pnlEditStudentRecord.Hide()
        End If
    End Sub

    'BUTTON HOVER
    'BUTTON CANCEL 3 HOVER STUDENTS
    Private Sub btnCancel3_MouseEnter(sender As Object, e As EventArgs) Handles btnCancel3.MouseEnter
        btnCancel3.BackColor = Color.FromArgb(8, 48, 25)

    End Sub

    Private Sub btnCancel3_MouseLeave(sender As Object, e As EventArgs) Handles btnCancel3.MouseLeave
        btnCancel3.BackColor = Color.FromArgb(97, 144, 118)
    End Sub

    Private Sub btnCancel3_MouseDown(sender As Object, e As MouseEventArgs) Handles btnCancel3.MouseDown
        btnCancel3.BackColor = Color.DarkSeaGreen
    End Sub

    Private Sub btnCancel3_MouseUp(sender As Object, e As MouseEventArgs) Handles btnCancel3.MouseUp
        btnCancel3.BackColor = Color.FromArgb(8, 48, 25)
    End Sub
    Private Sub btnEdit3_Click(sender As Object, e As EventArgs) Handles btnEdit3.Click
        If Not validStudentInputBx(txtEmail3, txtFName3, txtLName3, txtContactNumber3) Then Exit Sub

        Dim result As DialogResult = MessageBox.Show("Do you want to edit this record?", "Confirm Edit", MessageBoxButtons.YesNo, MessageBoxIcon.Information)

        If result = DialogResult.No Then
            pnlStudentInformation.Show()
            pnlEditStudentRecord.Hide()
            Exit Sub
        End If

        Dim isEdit As Boolean = updateStudentRecord(txtStudentID3.Text, txtFName3.Text, txtLName3.Text, cmbGender3.SelectedValue,
                                                   cmbSection3.SelectedValue, txtContactNumber3.Text, txtEmail3.Text,
                                                   cmbDepartment3.SelectedValue, cmbCourse3.SelectedValue)

        If Not isEdit Then
            MessageBox.Show("Editing Failed", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        clearBox(txtStudentID3, txtFName3, txtLName3, txtContactNumber3, txtEmail3)
        MessageBox.Show("Successfully Edited", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        loadStudentRecord()

    End Sub

    'BUTTON HOVER
    'BUTTON EDIT 3 HOVER STUDENTS
    Private Sub btnEdit3_MouseEnter(sender As Object, e As EventArgs) Handles btnEdit3.MouseEnter
        btnEdit3.BackColor = Color.FromArgb(97, 144, 118)

    End Sub

    Private Sub btnEdit3_MouseLeave(sender As Object, e As EventArgs) Handles btnEdit3.MouseLeave
        btnEdit3.BackColor = Color.FromArgb(8, 48, 25)
    End Sub

    Private Sub btnEdit3_MouseDown(sender As Object, e As MouseEventArgs) Handles btnEdit3.MouseDown
        btnEdit3.BackColor = Color.DarkSeaGreen
    End Sub

    Private Sub btnEdit3_MouseUp(sender As Object, e As MouseEventArgs) Handles btnEdit3.MouseUp
        btnEdit3.BackColor = Color.FromArgb(97, 144, 118)
    End Sub


    'Internship Logs

    ' Private Sub pnlInternshipLogs_Paint(sender As Object, e As PaintEventArgs) Handles pnlInternshipLogs.Paint

    ' End Sub

    Private Sub txtSearchID4_TextChanged(sender As Object, e As EventArgs) Handles txtSearchID4.TextChanged

    End Sub

    Private Sub btnSearch4_Click(sender As Object, e As EventArgs) Handles btnSearch4.Click
        Dim searchText As String = txtSearchID4.Text.Trim()

        If String.IsNullOrWhiteSpace(searchText) Then
            MessageBox.Show("Please Enter Student ID or Name",
                        "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtSearchID4.Focus()
            Exit Sub
        End If

        ' 1️⃣ Kunin lahat ng matching rows
        Dim dt As DataTable = searchInterTable(searchText)

        ' 2️⃣ Check kung may resulta
        If dt.Rows.Count = 0 Then
            dgvInternshipLogs4.DataSource = Nothing
            MessageBox.Show("No record found!", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        ' 3️⃣ Ipakita lang ang FIRST ROW
        Dim oneRow As DataTable = dt.Clone()    ' same structure
        oneRow.ImportRow(dt.Rows(0))            ' add first row only

        ' 4️⃣ I-bind at i-apply ang DGVHelper + column widths
        Dim columnWidths As New Dictionary(Of String, Integer) From {
        {"Internship ID", 170},
        {"First Name", 200},
        {"Last Name", 200},
        {"Company Name", 200},
        {"Supervisor Last Name", 200},
        {"Supervisor Contact", 150},
        {"Start_Date", 120},
        {"End_Date", 120},
        {"Status", 100}
    }

        DGVHelper.BindAndStyleFilesDGV(dgvInternshipLogs4, oneRow, columnWidths)
    End Sub


    'BUTTON HOVER
    'BUTTON SEARCH 4 HOVER INTERNSHIPS
    Private Sub btnSearch4_MouseEnter(sender As Object, e As EventArgs) Handles btnSearch4.MouseEnter
        btnSearch4.BackColor = Color.FromArgb(8, 48, 25)

    End Sub

    Private Sub btnSearch4_MouseLeave(sender As Object, e As EventArgs) Handles btnSearch4.MouseLeave
        btnSearch4.BackColor = Color.FromArgb(97, 144, 118)
    End Sub

    Private Sub btnSearch4_MouseDown(sender As Object, e As MouseEventArgs) Handles btnSearch4.MouseDown
        btnSearch4.BackColor = Color.DarkSeaGreen
    End Sub

    Private Sub btnSearch4_MouseUp(sender As Object, e As MouseEventArgs) Handles btnSearch4.MouseUp
        btnSearch4.BackColor = Color.FromArgb(8, 48, 25)
    End Sub

    Private Sub dgvInternshipLogs4_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvInternshipLogs4.CellContentClick

    End Sub

    Private Sub dgvInternshipFiles4_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvInternshipFiles4.CellContentClick

    End Sub

    Private Sub lblTotalRecords4_Click(sender As Object, e As EventArgs) Handles lblTotalRecords4.Click

    End Sub


    'Evaluation Logs

    ' Private Sub pnlEvaluationLogs_Paint(sender As Object, e As PaintEventArgs) Handles pnlEvaluationLogs.Paint

    ' End Sub


    Private Sub btnSearch5_Click(sender As Object, e As EventArgs) Handles btnSearch5.Click
        Dim searchText As String = txtSearchID5.Text.Trim()

        If String.IsNullOrWhiteSpace(searchText) Then
            MessageBox.Show("Please enter a search term.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' 1️⃣ Kunin ang result ng search
        Dim dt As DataTable = searchEvaluationTable(searchText)

        ' 2️⃣ Optional: I-define ang column widths
        Dim columnWidths As New Dictionary(Of String, Integer) From {
        {"Evaluation ID", 170},
        {"Internship ID", 170},
        {"First Name", 250},
        {"Last Name", 250},
        {"Company Name", 300},
        {"Grade", 120},
        {"Evaluation Report", 300},
        {"Faculty ID", 120}
    }

        ' 3️⃣ I-bind at i-apply ang style sa DataGridView
        DGVHelper.BindAndStyleFilesDGV(dgvEvaluationLogs5, dt, columnWidths)
    End Sub

    Private Sub dgvEvaluationLogs5_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvEvaluationLogs5.CellContentClick

    End Sub

    Private Sub dgvEvaluationFiles5_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvEvaluationFiles5.CellContentClick

    End Sub

    Private Sub lblTotalRecords5_Click(sender As Object, e As EventArgs) Handles lblTotalRecords5.Click

    End Sub

    Private Sub btnAdd5_Click(sender As Object, e As EventArgs) Handles btnAdd5.Click
        pnlAddNewInternshipEvaluationRecord.Show()
    End Sub

    Private Sub btnEdit5_Click(sender As Object, e As EventArgs) Handles btnEdit5.Click
        pnlEditInternshipEvaluationRecord.Visible = True
        pnlEditInternshipEvaluationRecord.Enabled = True

        ' Enable lang ang editable fields
        nudEvaluationGrade7.Enabled = True
        txtEvaluationReport7.ReadOnly = False
        cmbStatus7.Enabled = True

        ' Lock ang mga non-editable fields
        txtEvaluationID7.ReadOnly = True
        txtEvaluationID7.Enabled = False

        txtInternshipID7.ReadOnly = True
        txtInternshipID7.Enabled = False

        cmbFaculty7.Enabled = False   ' para hindi mapindot

        ClearAllControls(Me)
    End Sub

    Private Sub btnDelete5_Click(sender As Object, e As EventArgs)
        Dim result = MessageBox.Show(
       "Are you sure you want to delete this?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Stop)

        If result = DialogResult.Yes Then
            pnlEvaluationInformation.Show()
        End If
    End Sub

    'Add new intern record

    '  Private Sub pnlAddNewInternshipRecord_Paint(sender As Object, e As PaintEventArgs) Handles pnlAddNewInternshipEvaluationRecord.Paint

    '  End Sub
    Private Sub txtEvaluationID6_TextChanged(sender As Object, e As EventArgs) Handles txtEvaluationID6.TextChanged

    End Sub

    Private Sub txtInternshipID6_TextChanged(sender As Object, e As EventArgs) Handles txtInternshipID6.TextChanged

    End Sub

    Private Sub txtFacultyID6_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub txtEvaluationReport6_TextChanged(sender As Object, e As EventArgs) Handles txtEvaluationReport6.TextChanged

    End Sub

    Private Sub cmbStatus6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbStatus6.SelectedIndexChanged

    End Sub

    Private Sub btnCancel6_Click(sender As Object, e As EventArgs) Handles btnCancel6.Click
        Dim result = MessageBox.Show("Are you sure you want to cancel?", "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If result = DialogResult.Yes Then
            pnlInternshipInformation.Show()
            pnlAddNewInternshipEvaluationRecord.Hide()
        End If
    End Sub

    Private Sub btnAdd6_Click(sender As Object, e As EventArgs) Handles btnAdd6.Click

        ' ----------------------
        ' Validate required fields
        ' ----------------------
        If String.IsNullOrWhiteSpace(txtInternshipID6.Text) OrElse
       String.IsNullOrWhiteSpace(txtEvaluationReport6.Text) OrElse
       cmbStatus6.SelectedIndex = -1 OrElse
       cmbFaculty6.SelectedIndex = -1 Then

            MessageBox.Show("Please complete all required fields.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' ----------------------
        ' Confirm Add
        ' ----------------------
        Dim result = MessageBox.Show("Do you want to add this record?", "Confirm Add", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.No Then Exit Sub

        ' ----------------------
        ' Generate evaluation_id
        ' ----------------------
        Dim newEvaluationID As String = GenerateEvaluationID()
        txtEvaluationID6.Text = newEvaluationID  ' show in textbox (read-only)

        ' ----------------------
        ' Get optional grade
        ' ----------------------
        Dim gradeValue As Decimal? = Nothing
        If nudEvaluationGrade.Value > 0 Then
            gradeValue = nudEvaluationGrade.Value
        End If

        ' ----------------------
        ' Get selected Faculty ID
        ' ----------------------
        Dim selectedFaculty As String = cmbFaculty6.SelectedItem.ToString()

        ' ----------------------
        ' Insert into database
        ' ----------------------
        Dim success As Boolean = AddEvaluationRecord(
                                txtInternshipID6.Text.Trim(),
                                txtEvaluationReport6.Text.Trim(),
                                cmbStatus6.Text,
                                gradeValue,
                                selectedFaculty,
                                newEvaluationID) ' pass generated ID if your function supports it

        If success Then
            MessageBox.Show("Evaluation added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' ----------------------
            ' Reload DataGridView and select last row
            ' ----------------------
            LoadEvaluationDGV()
            dgvEvaluationFiles5.AllowUserToAddRows = False

            If dgvEvaluationFiles5.Rows.Count > 0 Then
                Dim lastIndex As Integer = dgvEvaluationFiles5.Rows.Count - 1
                dgvEvaluationFiles5.ClearSelection()
                dgvEvaluationFiles5.Rows(lastIndex).Selected = True
                dgvEvaluationFiles5.FirstDisplayedScrollingRowIndex = lastIndex
            End If

            ' ----------------------
            ' Clear form for next entry
            ' ----------------------
            txtEvaluationID6.Text = GenerateEvaluationID() ' generate next ID
            txtInternshipID6.Clear()
            txtEvaluationReport6.Clear()
            cmbStatus6.SelectedIndex = -1
            nudEvaluationGrade.Value = 0
            cmbFaculty6.SelectedIndex = -1

            ' ----------------------
            ' Show main panel, hide add panel
            ' ----------------------
            pnlInternshipInformation.Show()
            pnlAddNewInternshipEvaluationRecord.Hide()
        End If

    End Sub

    'Edit intern eval record
    '   Private Sub pnlEditInternshipEvaluationRecord_Paint(sender As Object, e As PaintEventArgs) Handles pnlEditInternshipEvaluationRecord.Paint

    '   End Sub
    Private Sub txtSearchID7_TextChanged(sender As Object, e As EventArgs) Handles txtSearchID7.TextChanged

    End Sub

    Private Sub btnSearch7_Click(sender As Object, e As EventArgs) Handles btnSearch7.Click
        Dim searchID As String = txtSearchID7.Text.Trim()

        If searchID = "" Then
            MessageBox.Show("Please enter an Evaluation ID.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' --- Setup status combo (Dropdown only) ---
        cmbStatus7.Items.Clear()
        cmbStatus7.DropDownStyle = ComboBoxStyle.DropDownList
        cmbStatus7.Items.Add("Passed")
        cmbStatus7.Items.Add("Failed")
        cmbStatus7.Items.Add("Incomplete")
        cmbStatus7.SelectedIndex = -1  ' Default walang selection
        LoadFacultyCombo()

        Dim dt As New DataTable()
        Try
            Using con As MySqlConnection = Connectivity.GetConnection()
                Using cmd As New MySqlCommand("SELECT * FROM internship_evaluation WHERE LOWER(evaluation_id) = LOWER(@evalID)", con)
                    cmd.Parameters.AddWithValue("@evalID", searchID.Trim())

                    Using adapter As New MySqlDataAdapter(cmd)
                        adapter.Fill(dt)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error retrieving evaluation: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        ' Load data into controls if found
        If dt.Rows.Count > 0 Then
            Dim row As DataRow = dt.Rows(0)
            txtEvaluationID7.Text = row("evaluation_id").ToString()
            txtInternshipID7.Text = row("internship_id").ToString()
            cmbFaculty7.SelectedValue = row("faculty_id").ToString()
            nudEvaluationGrade7.Value = If(IsDBNull(row("grade")), 0, Convert.ToDecimal(row("grade")))
            txtEvaluationReport7.Text = row("evaluation_report").ToString()

            ' Set status combo selection based on DB value
            Dim statusVal As String = row("evaluation_status").ToString()
            If cmbStatus7.Items.Contains(statusVal) Then
                cmbStatus7.SelectedItem = statusVal
            Else
                cmbStatus7.SelectedIndex = -1
            End If
        Else
            MessageBox.Show("Evaluation not found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ' Clear controls
            txtEvaluationID7.Clear()
            txtInternshipID7.Clear()
            cmbFaculty7.SelectedIndex = -1
            nudEvaluationGrade7.Value = 0
            txtEvaluationReport7.Clear()
            cmbStatus7.SelectedIndex = -1
        End If

    End Sub
    Private Sub txtEvaluationID7_TextChanged(sender As Object, e As EventArgs) Handles txtEvaluationID7.TextChanged

    End Sub

    Private Sub txtInternshipID7_TextChanged(sender As Object, e As EventArgs) Handles txtInternshipID7.TextChanged

    End Sub

    Private Sub txtFacultyID7_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub txtEvaluationReport7_TextChanged(sender As Object, e As EventArgs) Handles txtEvaluationReport7.TextChanged

    End Sub

    Private Sub cmbStatus7_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbStatus7.SelectedIndexChanged

    End Sub

    Private Sub btnCancel7_Click(sender As Object, e As EventArgs) Handles btnCancel7.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to cancel?", "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If result = DialogResult.Yes Then
            pnlEvaluationInformation.Show()
            pnlEditInternshipEvaluationRecord.Hide()
        End If

    End Sub

    Private Sub btnEdit7_Click(sender As Object, e As EventArgs) Handles btnEdit7.Click

        ' Collect values from controls
        Dim evalID As String = txtEvaluationID7.Text.Trim()
        Dim grade As Decimal = nudEvaluationGrade7.Value
        Dim report As String = txtEvaluationReport7.Text.Trim()
        Dim status As String = cmbStatus7.Text.Trim()

        ' Validation – optional pero recommended
        If evalID = "" Then
            MessageBox.Show("Missing Evaluation ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        ' Call the update function in ModuleQuery
        Dim success As Boolean = EditEvaluationRecord(evalID, grade, report, status)

        If success Then
            MessageBox.Show("Record has been successfully edited.", "Edit Successful",
                        MessageBoxButtons.OK, MessageBoxIcon.Information)
            ClearAllControls(Me)
            ' After update, go back to view panel
            pnlEvaluationInformation.Show()
            pnlEditInternshipEvaluationRecord.Hide()
        Else
            MessageBox.Show("Failed to update record.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

    'CompanyLogs

    '  Private Sub pnlCompanyLogs_Paint(sender As Object, e As PaintEventArgs) Handles pnlCompanyLogs.Paint

    '  End Sub
    Private Sub txtSearchID8_TextChanged(sender As Object, e As EventArgs) Handles txtSearchID8.TextChanged

    End Sub
    Private Sub btnSearc8_TextChanged(sender As Object, e As EventArgs) Handles btnSearch8.TextChanged

    End Sub

    Private Sub dgvCompanyLogs8_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCompanyLogs8.CellContentClick

    End Sub

    Private Sub dgvCompanyFiles8_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCompanyFiles8.CellContentClick

    End Sub

    Private Sub lblTotalRecords8_Click(sender As Object, e As EventArgs) Handles lblTotalRecords8.Click

    End Sub
    Private Sub btnViewContacts8_Click(sender As Object, e As EventArgs) Handles btnViewContacts8.Click
        hidePanel()
        pnlCompanyContactInformation.Show()
        pnlHome.Hide()
    End Sub
    Private Sub btnAdd8_Click(sender As Object, e As EventArgs) Handles btnAdd8.Click
        pnlAddNewCompanyandCompanyContact.Show()
        pnlHome.Hide()
    End Sub

    Private Sub btnEdit8_Click(sender As Object, e As EventArgs) Handles btnEdit8.Click
        pnlEditCompanyCompanyContact.Show()
        pnlHome.Hide()
    End Sub

    Private Sub btnDelete8_Click(sender As Object, e As EventArgs)
        Dim result = MessageBox.Show(
       "Are you sure you want to delete this?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Stop)

        If result = DialogResult.Yes Then
            ' Your delete code here
            ' Example: DeleteRecord()
        End If
    End Sub

    'Add new record company or contact

    '  Private Sub pnlAddNewCompanyandCompanyContact_Paint(sender As Object, e As PaintEventArgs) Handles pnlAddNewCompanyandCompanyContact.Paint

    '  End Sub

    Private Sub btnAddCompany8_Click(sender As Object, e As EventArgs) Handles btnAddCompany8.Click
        pnlAddNewCompanyRecord.Show()
        pnlAddNewCompanyandCompanyContact.Hide()
        txtCompanyID9.ReadOnly = True
        txtCompanyID9.Text = GetNextCompanyID() ' dapat lumabas sa textbox
    End Sub

    Private Sub btnAddCompanyContact8_Click(sender As Object, e As EventArgs) Handles btnAddCompanyContact8.Click

        ' 1. Show parent panel
        pnlCompanyContactInformation.Show()
        pnlCompanyContactInformation.BringToFront()

        ' 2. Show the inside panel
        pnlAddNewCompanyContactRecord.Show()
        pnlAddNewCompanyContactRecord.BringToFront()

        ' 3. Hide the old panel
        pnlCompanyInformation.Hide()

        txtCompanyContactID12.Text = GenerateContactID()
        txtCompanyContactID12.ReadOnly = True

    End Sub

    'Edit company contact or sompany
    Private Sub pnlEditCompanyCompanyContact_Paint(sender As Object, e As PaintEventArgs) Handles pnlEditCompanyCompanyContact.Paint

    End Sub

    Private Sub btnEditCompany8_Click(sender As Object, e As EventArgs) Handles btnEditCompany8.Click
        pnlEditCompanyRecord.Show()
        pnlEditCompanyCompanyContact.Hide()
    End Sub

    Private Sub btnEditCompanyContact8_Click(sender As Object, e As EventArgs) Handles btnEditCompanyContact8.Click
        pnlEditCompanyContactRecord.Hide()
        pnlEditCompanyRecord.Show()

        ' 1. Show parent panel
        pnlCompanyContactInformation.Show()
        pnlCompanyContactInformation.BringToFront()

        ' 2. Show the inside panel
        pnlEditCompanyContactRecord.Show()
        pnlEditCompanyContactRecord.BringToFront()

        ' 3. Hide the old panel
        pnlCompanyInformation.Hide()


    End Sub

    'Add company record
    ' Private Sub pnlAddNewCompanyRecord_Paint(sender As Object, e As PaintEventArgs) Handles pnlAddNewCompanyRecord.Paint

    ' End Sub
    Private Sub txtCompanyID9_TextChanged(sender As Object, e As EventArgs) Handles txtCompanyID9.TextChanged

    End Sub

    Private Sub txtCompanyName9_TextChanged(sender As Object, e As EventArgs) Handles txtCompanyName9.TextChanged

    End Sub

    Private Sub txtAddress9_TextChanged(sender As Object, e As EventArgs) Handles txtAddress9.TextChanged

    End Sub

    Private Sub txtIndustryType9_TextChanged(sender As Object, e As EventArgs) Handles txtIndustryType9.TextChanged

    End Sub

    Private Sub txtContactNumber9_TextChanged(sender As Object, e As EventArgs) Handles txtContactNumber9.TextChanged

    End Sub

    Private Sub txtEmail9_TextChanged(sender As Object, e As EventArgs) Handles txtEmail9.TextChanged

    End Sub

    Private Sub btnCancel9_Click(sender As Object, e As EventArgs) Handles btnCancel9.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to cancel?", "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If result = DialogResult.Yes Then
            pnlAddNewCompanyandCompanyContact.Hide()
            pnlCompanyInformation.Show()
            pnlAddNewCompanyRecord.Hide()
        End If

    End Sub

    Private Sub btnAdd9_Click(sender As Object, e As EventArgs) Handles btnAdd9.Click
        ' Ask user confirmation
        Dim result As DialogResult = MessageBox.Show("Do you want to add this record?", "Confirm Add", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            ' Show/hide panels
            pnlAddNewCompanyandCompanyContact.Hide()
            pnlCompanyInformation.Show()
            pnlAddNewCompanyRecord.Show()

            ' Generate next Company ID (C001, C002, etc.)
            Dim newID As String = GetNextCompanyID()
            txtCompanyID9.Text = newID ' display sa textbox (read-only)

            ' Insert into database
            If AddCompanyRecord(newID, txtCompanyName9.Text, txtAddress9.Text, txtIndustryType9.Text, txtContactNumber9.Text, txtEmail9.Text) Then
                MessageBox.Show("Company record added successfully!")

                ' Refresh DataGridView
                LoadCompanyData()

                ' Clear input fields
                txtCompanyName9.Clear()
                txtAddress9.Clear()
                txtIndustryType9.Clear()
                txtContactNumber9.Clear()
                txtEmail9.Clear()
            End If
        End If
    End Sub


    'Edit COmpany Record

    ' Private Sub pnlEditCompanyRecord_Paint(sender As Object, e As PaintEventArgs) Handles pnlEditCompanyRecord.Paint

    ' End Sub


    Private Sub txtSearchID10_TextChanged(sender As Object, e As EventArgs) Handles txtSearchID10.TextChanged

    End Sub

    Private Sub btnSearch10_Click(sender As Object, e As EventArgs) Handles btnSearch10.Click

        If txtSearchID10.Text.Trim = "" Then
            MessageBox.Show("Please enter Company ID to search.", "Missing Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim dt As DataTable = GetCompanyByID(txtSearchID10.Text.Trim)

        If dt.Rows.Count = 0 Then
            MessageBox.Show("No record found for this Company ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim row As DataRow = dt.Rows(0)

        ' Populate textboxes
        txtCompanyID10.Text = row("company_id").ToString()
        txtCompanyName10.Text = row("company_name").ToString()
        txtIndustryType10.Text = row("industry_type").ToString()
        txtContactNumber10.Text = row("company_contact_no").ToString()
        txtAddress10.Text = row("company_address").ToString()
        txtEmail10.Text = row("company_email").ToString()

        ' Make ID non-editable
        txtCompanyID10.ReadOnly = True
        txtCompanyID10.BackColor = Color.LightGray

    End Sub

    Private Sub txtCompanyID10_TextChanged(sender As Object, e As EventArgs) Handles txtCompanyID10.TextChanged

    End Sub

    Private Sub txtCompanyName10_TextChanged(sender As Object, e As EventArgs) Handles txtCompanyName10.TextChanged

    End Sub

    Private Sub txtAddress10_TextChanged(sender As Object, e As EventArgs) Handles txtAddress10.TextChanged

    End Sub

    Private Sub txtIndustryType10_TextChanged(sender As Object, e As EventArgs) Handles txtIndustryType10.TextChanged

    End Sub

    Private Sub txtContactNumber10_TextChanged(sender As Object, e As EventArgs) Handles txtContactNumber10.TextChanged

    End Sub

    Private Sub txtEmail10_TextChanged(sender As Object, e As EventArgs) Handles txtEmail10.TextChanged

    End Sub

    Private Sub btnCancel10_Click(sender As Object, e As EventArgs) Handles btnCancel10.Click
        Dim result = MessageBox.Show("Are you sure you want to cancel?", "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If result = DialogResult.Yes Then
            pnlAddNewCompanyandCompanyContact.Hide()
            pnlCompanyInformation.Show()
            pnlEditCompanyRecord.Hide()

        End If
    End Sub


    'company contact logs
    'Private Sub pnlCompanyContactLogs_Paint(sender As Object, e As PaintEventArgs) Handles pnlCompanyContactLogs.Paint

    'End Sub

    Private Sub txtSearchID11_TextChanged(sender As Object, e As EventArgs) Handles txtSearchID11.TextChanged

    End Sub

    Private Sub btnSearch11_Click(sender As Object, e As EventArgs) Handles btnSearch11.Click
        Dim searchText As String = txtSearchID11.Text.Trim()

        ' Check if search box is empty
        If String.IsNullOrWhiteSpace(searchText) Then
            MessageBox.Show("Please enter a value to search.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtSearchID11.Focus()
            Exit Sub
        End If

        ' Perform search
        Dim dt As DataTable = SearchCompanyContacts(searchText)

        ' Handle no results
        If dt.Rows.Count = 0 Then
            dgvCompanyContactLogs11.DataSource = Nothing
            MessageBox.Show("No record found!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        ' Column widths
        Dim columnWidths As New Dictionary(Of String, Integer) From {
         {"Contact ID", 170},
        {"Company ID", 170},
        {"Company Name", 250},
        {"Contact First Name", 200},
        {"Contact Last Name", 200},
        {"Position", 150},
        {"Contact No", 120},
        {"Email", 200},
        {"Student First Name", 200},
        {"Student Last Name", 200},
        {"Grade", 150}
    }

        ' Bind and apply DGVHelper styling
        DGVHelper.BindAndStyleFilesDGV(dgvCompanyContactLogs11, dt, columnWidths)

        ' Clear selection
        dgvCompanyContactLogs11.ClearSelection()
    End Sub


    Private Sub dgvCompanyContactLogs11_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCompanyContactLogs11.CellContentClick

    End Sub

    Private Sub dgvCompanyContactFiles_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCompanyContactFiles.CellContentClick

    End Sub

    Private Sub lblTotalRecords11_Click(sender As Object, e As EventArgs) Handles lblTotalRecords11.Click

    End Sub

    Private Sub btnAdd11_Click(sender As Object, e As EventArgs) Handles btnAdd11.Click
        pnlCompanyInformation.Show()

        pnlCompanyContactInformation.Hide() ' ← hide the other parent panel

        pnlAddNewCompanyandCompanyContact.Show()
        pnlAddNewCompanyandCompanyContact.BringToFront()


    End Sub

    Private Sub btnEdit11_Click(sender As Object, e As EventArgs) Handles btnEdit11.Click
        ' Hide everything — safest reset
        hidePanel()

        ' Now show ONLY the Company Information (the master container)
        pnlCompanyInformation.Show()

        ' Then show ONLY the EDIT panel
        pnlEditCompanyCompanyContact.Show()
        pnlEditCompanyCompanyContact.BringToFront()


    End Sub

    Private Sub btnDelete11_Click(sender As Object, e As EventArgs)
        Dim result = MessageBox.Show(
       "Are you sure you want to delete this?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Stop)

        If result = DialogResult.Yes Then
            ' Your delete code here
            ' Example: DeleteRecord()
        End If
    End Sub

    'Add new company contacyt
    'Private Sub pnlAddNewCompanyContactRecord_Paint(sender As Object, e As PaintEventArgs) Handles pnlAddNewCompanyContactRecord.Paint

    'End Sub

    Private Sub txtCompanyContactID12_TextChanged(sender As Object, e As EventArgs) Handles txtCompanyContactID12.TextChanged

    End Sub

    Private Sub txtCompany12_TextChanged(sender As Object, e As EventArgs) Handles txtCompany12.TextChanged

    End Sub

    Private Sub txtPosition12_TextChanged(sender As Object, e As EventArgs) Handles txtPosition12.TextChanged

    End Sub

    Private Sub txtFName12_TextChanged(sender As Object, e As EventArgs) Handles txtFCName12.TextChanged

    End Sub

    Private Sub txtLName12_TextChanged(sender As Object, e As EventArgs) Handles txtLCName12.TextChanged

    End Sub

    Private Sub txtContactNumber12_TextChanged(sender As Object, e As EventArgs) Handles txtContactNumber12.TextChanged

    End Sub

    Private Sub txtEmail12_TextChanged(sender As Object, e As EventArgs) Handles txtEmail12.TextChanged

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to cancel?", "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If result = DialogResult.Yes Then
            pnlCompanyInformation.Show()
            pnlAddNewCompanyContactRecord.Hide()
            pnlAddNewCompanyandCompanyContact.Hide()
        End If

    End Sub

    Private Sub btnAdd12_Click(sender As Object, e As EventArgs) Handles btnAdd12.Click

        Dim addSuccess As Boolean = AddCompanyContact(
        txtCompanyContactID12.Text,
        txtCompany12.Text,
        txtPosition12.Text,
        txtFCName12.Text,
        txtLCName12.Text,
        txtContactNumber12.Text,
        txtEmail12.Text
    )

        If addSuccess Then
            MessageBox.Show("Company Contact successfully added!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' REFRESH DGV
            dgvCompanyContactFiles.DataSource = LoadAllCompanyContacts()

            ' GENERATE NEW ID
            txtCompanyContactID12.Text = GenerateContactID()

        End If

        Dim result As DialogResult = MessageBox.Show("Do you want to add this record?", "Confirm Add", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            pnlCompanyInformation.Show()
            pnlAddNewCompanyContactRecord.Hide()
            pnlAddNewCompanyandCompanyContact.Hide()
        End If

        txtCompanyContactID12.Text = GenerateContactID()
        txtCompanyContactID12.ReadOnly = True
    End Sub

    'Edit company contact rec
    'Private Sub pnlEditCompanyContactRecord_Paint(sender As Object, e As PaintEventArgs) Handles pnlEditCompanyContactRecord.Paint

    'End Sub

    Private Sub txtSearchID13_TextChanged(sender As Object, e As EventArgs) Handles txtSearchID13.TextChanged

    End Sub

    Private Sub btnSearch13_Click(sender As Object, e As EventArgs) Handles btnSearch13.Click
        Dim dt As DataTable = SearchCompanyContacts(txtSearchID13.Text)

        dgvCompanyContactFiles.DataSource = dt ' show results in DGV

        If dt.Rows.Count = 0 Then
            MessageBox.Show("No contact found.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        ' Load first result into textboxes
        Dim row As DataRow = dt.Rows(0)
        txtCompanyContactID13.Text = row("Contact ID").ToString()
        txtCompany13.Text = row("Company ID").ToString()
        txtFName.Text = row("First Name").ToString()
        txtLName.Text = row("Last Name").ToString()
        txtPosition13.Text = row("Position").ToString()
        txtContactNumber.Text = row("Contact Number").ToString()
        txtEmail13.Text = row("Email").ToString()

        ' Make Contact ID non-editable
        txtCompanyContactID13.ReadOnly = True

    End Sub

    Public Function SearchCompanyContacts(searchText As String) As DataTable
        Dim dt As New DataTable()

        Dim query As String = "
        SELECT 
            cc.contact_id AS 'Contact ID',
            cc.contact_first_name AS 'First Name',
            cc.contact_last_name AS 'Last Name',
            cc.contact_position AS 'Position',
            cc.company_id AS 'Company ID',
            c.company_name AS 'Company Name',
            cc.contact_contact_no AS 'Contact Number',
            cc.email AS 'Email'
        FROM company_contact cc
        INNER JOIN company c ON cc.company_id = c.company_id
        WHERE LOWER(cc.contact_id) LIKE LOWER(@search)
           OR LOWER(cc.contact_first_name) LIKE LOWER(@search)
           OR LOWER(cc.contact_last_name) LIKE LOWER(@search)
    "

        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@search", "%" & searchText.ToLower() & "%")

                    Using da As New MySqlDataAdapter(cmd)
                        da.Fill(dt)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return dt
    End Function

    Private Sub txtCompanyContactID13_TextChanged(sender As Object, e As EventArgs) Handles txtCompanyContactID13.TextChanged

    End Sub

    Private Sub txtCompany13_TextChanged(sender As Object, e As EventArgs) Handles txtCompany13.TextChanged

    End Sub

    Private Sub txtPosition13_TextChanged(sender As Object, e As EventArgs) Handles txtPosition13.TextChanged

    End Sub
    Private Sub txtFName_TextChanged(sender As Object, e As EventArgs) Handles txtFName.TextChanged

    End Sub
    Private Sub txtLName_TextChanged(sender As Object, e As EventArgs) Handles txtLName.TextChanged

    End Sub

    Private Sub txtContactNumber_TextChanged(sender As Object, e As EventArgs) Handles txtContactNumber.TextChanged

    End Sub

    Private Sub txtEmail13_TextChanged(sender As Object, e As EventArgs) Handles txtEmail13.TextChanged

    End Sub

    Private Sub btnCancel13_Click(sender As Object, e As EventArgs) Handles btnCancel13.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to cancel?", "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If result = DialogResult.Yes Then
            pnlEditCompanyContactRecord.Hide()
            pnlCompanyContactInformation.Show()
        End If
    End Sub

    Private Sub btnEdit13_Click(sender As Object, e As EventArgs) Handles btnEdit13.Click
        Dim query As String = "
        UPDATE company_contact SET
            contact_first_name=@fname,
            contact_last_name=@lname,
            contact_position=@pos,
            company_id=@company,
            contact_contact_no=@contact,
            email=@mail
        WHERE contact_id=@id
    "

        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@fname", txtFName.Text)
                    cmd.Parameters.AddWithValue("@lname", txtLName.Text)
                    cmd.Parameters.AddWithValue("@pos", txtPosition13.Text)
                    cmd.Parameters.AddWithValue("@company", txtCompany13.Text)
                    cmd.Parameters.AddWithValue("@contact", txtContactNumber.Text)
                    cmd.Parameters.AddWithValue("@mail", txtEmail13.Text)
                    cmd.Parameters.AddWithValue("@id", txtCompanyContactID13.Text)

                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()
                End Using
            End Using

            MessageBox.Show("Contact record updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' REFRESH DGV with ALL records (not just the edited one)
            dgvCompanyContactFiles.DataSource = LoadAllCompanyContacts()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    'Faculty Logs

    '  Private Sub pnlFacultyLogs_Paint(sender As Object, e As PaintEventArgs) Handles pnlFacultyLogs.Paint

    '  End Sub

    Private Sub txtSearchID14_TextChanged(sender As Object, e As EventArgs) Handles txtSearchFacultyID14.TextChanged

    End Sub

    Private Sub btnSearch14_Click(sender As Object, e As EventArgs) Handles btnSearch14.Click
        Dim searchText As String = txtSearchFacultyID14.Text.Trim()

        If String.IsNullOrWhiteSpace(searchText) Then
            MessageBox.Show("Please Enter Faculty ID", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtSearchFacultyID14.Focus()
            Exit Sub
        End If

        ' Perform search
        Dim dt As DataTable = searchFacultyTable(searchText)

        ' Handle no results
        If dt.Rows.Count = 0 Then
            dgvFacultyLogs14.DataSource = Nothing
            MessageBox.Show("No record found!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        ' Column widths
        Dim columnWidths As New Dictionary(Of String, Integer) From {
         {"Faculty ID", 170},
        {"First Name", 200},
        {"Last Name", 200},
        {"Position", 170},
        {"Department", 300},
        {"Faculty Contact", 150}
    }

        ' Bind and apply DGVHelper styling
        DGVHelper.BindAndStyleFilesDGV(dgvFacultyLogs14, dt, columnWidths)

        ' Clear selection
        dgvFacultyLogs14.ClearSelection()
    End Sub

    Private Sub dgvFacultyLogs14_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvFacultyLogs14.CellContentClick

    End Sub

    Private Sub dgvFacultyFiles_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvFacultyFiles.CellContentClick

    End Sub

    Private Sub lblTotalRecords14_Click(sender As Object, e As EventArgs) Handles lblTotalRecords14.Click

    End Sub

    Private Sub btnAdd14_Click(sender As Object, e As EventArgs) Handles btnAdd14.Click
        pnlAddNewFacultyRecord.Show()
        loadDepartmentComboBx(cmbDepartment15)
        txtFacultyID15.Text = GenerateFacultyID()
    End Sub

    Private Sub btnEdit14_Click(sender As Object, e As EventArgs) Handles btnEdit14.Click
        pnlEditFacultyRecord.Show()
    End Sub

    Private Sub btnDelete14_Click(sender As Object, e As EventArgs) Handles btnDelete14.Click
        Dim result As DialogResult = MessageBox.Show(
       "Are you sure you want to delete this?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Stop)

        If result = DialogResult.Yes Then
            ' Your delete code here
            ' Example: DeleteRecord()
        End If
    End Sub

    'Add new faculty record

    '  Private Sub pnlAddNewFacultyRecord_Paint(sender As Object, e As PaintEventArgs) Handles pnlAddNewFacultyRecord.Paint

    '  End Sub

    Private Sub btnCancel15_Click(sender As Object, e As EventArgs) Handles btnCancel15.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to cancel?", "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If result = DialogResult.Yes Then
            pnlFacultyInformation.Show()
            pnlAddNewFacultyRecord.Hide()

        End If

    End Sub

    Private Sub btnAdd15_Click(sender As Object, e As EventArgs) Handles btnAdd15.Click
        If Not validInputbox(txtFName15, txtLName15, txtContactNumber15, txtPosition15) Then
            Exit Sub
        End If

        Dim result As DialogResult = MessageBox.Show("Do you want to add this record?", "Confirm Add", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            pnlFacultyInformation.Show()
            pnlAddNewFacultyRecord.Hide()
        End If



    End Sub

    'Edit faculty Record

    Private Sub pnlEditFacultyRecord_Paint(sender As Object, e As PaintEventArgs) Handles pnlEditFacultyRecord.Paint

    End Sub

    Private Sub btnSearch16_Click(sender As Object, e As EventArgs) Handles btnSearch16.Click

        Dim dt As DataTable = GetFacultyRecord(txtSearchID16.Text)

        If dt.Rows.Count = 0 Then
            MessageBox.Show("Faculty not found.")
            Exit Sub
        End If

        Dim row As DataRow = dt.Rows(0)

        txtFacultyID16.Text = row("faculty_id").ToString()
        txtFName16.Text = row("faculty_first_name").ToString()
        txtLName16.Text = row("faculty_last_name").ToString()
        txtPosition16.Text = row("faculty_position").ToString()
        txtDepartmentID16.Text = row("department_id").ToString()
        txtContactNumber16.Text = row("faculty_contact_no").ToString()

        'Make ID non-editable
        txtFacultyID16.ReadOnly = True
        'or  txtFacultyID16.Enabled = False

    End Sub

    Private Sub txtFacultyID16_TextChanged(sender As Object, e As EventArgs) Handles txtFacultyID16.TextChanged

    End Sub

    Private Sub txtFName16_TextChanged(sender As Object, e As EventArgs) Handles txtFName16.TextChanged

    End Sub

    Private Sub txtLName16_TextChanged(sender As Object, e As EventArgs) Handles txtLName16.TextChanged

    End Sub

    Private Sub txtPosition16_TextChanged(sender As Object, e As EventArgs) Handles txtPosition16.TextChanged

    End Sub

    Private Sub txtDepartmentID16_TextChanged(sender As Object, e As EventArgs) Handles txtDepartmentID16.TextChanged

    End Sub

    Private Sub txtContactNumber16_TextChanged(sender As Object, e As EventArgs) Handles txtContactNumber16.TextChanged

    End Sub

    Private Sub btnCancel16_Click(sender As Object, e As EventArgs) Handles btnCancel16.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to cancel?", "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If result = DialogResult.Yes Then
            pnlFacultyInformation.Show()
            pnlEditFacultyRecord.Hide()
        End If

    End Sub

    Private Sub btnEdit16_Click(sender As Object, e As EventArgs) Handles btnEdit16.Click
        Dim query As String = "
        UPDATE faculty SET
            faculty_first_name=@fname,
            faculty_last_name=@lname,
            faculty_position=@pos,
            department_id=@dept,
            faculty_contact_no=@contact
        WHERE faculty_id=@id;
    "

        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand(query, con)
                    con.Open()

                    cmd.Parameters.AddWithValue("@fname", txtFName16.Text)
                    cmd.Parameters.AddWithValue("@lname", txtLName16.Text)
                    cmd.Parameters.AddWithValue("@pos", txtPosition16.Text)
                    cmd.Parameters.AddWithValue("@dept", txtDepartmentID16.Text)
                    cmd.Parameters.AddWithValue("@contact", txtContactNumber16.Text)
                    cmd.Parameters.AddWithValue("@id", txtFacultyID16.Text)

                    cmd.ExecuteNonQuery()

                    MessageBox.Show("Faculty record updated successfully!")
                End Using
            End Using

            ' REFRESH DGV
            dgvFacultyFiles.DataSource = SearchFacultyRecords("")

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub


    'visit logs

    Private Sub pnlVisitLogs_Paint(sender As Object, e As PaintEventArgs) Handles pnlVisitInformation.Paint

    End Sub

    Private Sub txtSearchID17_TextChanged(sender As Object, e As EventArgs) Handles txtSearchID17.TextChanged

    End Sub

    Private Sub btnSearch17_Click(sender As Object, e As EventArgs) Handles btnSearch17.Click
        Dim searchText As String = txtSearchID17.Text.Trim()

        If String.IsNullOrWhiteSpace(searchText) Then
            MessageBox.Show("Please enter a value to search.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtSearchID17.Focus()
            Exit Sub
        End If

        ' Perform search
        Dim dt As DataTable = SearchVisitLogs(searchText)

        ' Handle no results
        If dt.Rows.Count = 0 Then
            dgvVisitLogs17.DataSource = Nothing
            MessageBox.Show("No record found!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        ' Column widths
        Dim columnWidths As New Dictionary(Of String, Integer) From {
        {"Visit ID", 80},
        {"Internship ID", 100},
        {"Faculty ID", 80},
        {"Faculty First Name", 150},
        {"Faculty Last Name", 150},
        {"Visit Date", 120},
        {"Remarks", 200},
        {"Score", 80}
    }

        ' Bind and apply DGVHelper styling
        DGVHelper.BindAndStyleFilesDGV(dgvVisitLogs17, dt, columnWidths)

        ' Clear selection
        dgvVisitLogs17.ClearSelection()
    End Sub

    Private Sub dgvVisitLogs17_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvVisitLogs17.CellContentClick

    End Sub

    Private Sub dgvVisitLogFiles17_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvVisitLogFiles17.CellContentClick

    End Sub

    Private Sub lblTotalRecords17_Click(sender As Object, e As EventArgs) Handles lblTotalRecords17.Click

    End Sub

    Private Sub btnAdd17_Click(sender As Object, e As EventArgs) Handles btnAdd17.Click
        txtVisitID18.Text = GenerateVisitID()
        txtVisitID18.ReadOnly = True

        ' Load existing Visit Logs into DGV
        dgvVisitLogFiles17.DataSource = LoadVisitLogs()

        pnlAddNewVisitRecord.Show()
    End Sub

    Private Sub btnEdit17_Click(sender As Object, e As EventArgs) Handles btnEdit17.Click
        pnlEditVisitRecord.Show()
    End Sub

    Private Sub btnDelete17_Click(sender As Object, e As EventArgs)
        Dim result = MessageBox.Show(
    "Are you sure you want to delete this?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Stop)

        If result = DialogResult.Yes Then
            ' Your delete code here
            ' Example: DeleteRecord()
        End If
    End Sub

    'Add new visit logs

    'Private Sub pnlAddNewInternship_Paint(sender As Object, e As PaintEventArgs) Handles pnlAddNewVisitRecord.Paint

    ' End Sub

    Private Sub txtVisitID18_TextChanged(sender As Object, e As EventArgs) Handles txtVisitID18.TextChanged

    End Sub

    Private Sub txtInternshipID18_TextChanged(sender As Object, e As EventArgs) Handles txtInternshipID18.TextChanged

    End Sub

    Private Sub txtFacultyID18_TextChanged(sender As Object, e As EventArgs) Handles txtFacultyID18.TextChanged

    End Sub

    Private Sub dtpVisitDate18_ValueChanged(sender As Object, e As EventArgs) Handles dtpVisitDate18.ValueChanged

    End Sub

    Private Sub txtScore18_TextChanged(sender As Object, e As EventArgs) Handles txtScore18.TextChanged

    End Sub

    Private Sub txtRemarks18_TextChanged(sender As Object, e As EventArgs) Handles txtRemarks18.TextChanged

    End Sub
    Private Sub btnCancel18_Click(sender As Object, e As EventArgs) Handles btnCancel18.Click
        Dim result = MessageBox.Show("Are you sure you want to cancel?", "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If result = DialogResult.Yes Then
            pnlVisitInformation.Show()
            pnlAddNewVisitRecord.Hide()
        End If

    End Sub
    Private Sub btnAdd18_Click(sender As Object, e As EventArgs) Handles btnAdd18.Click
        ' Generate Visit ID if empty
        If String.IsNullOrWhiteSpace(txtVisitID18.Text) Then
            txtVisitID18.Text = GenerateVisitID()
            txtVisitID18.ReadOnly = True
        End If

        ' Validate score input
        Dim scoreValue As Decimal
        If Not Decimal.TryParse(txtScore18.Text, scoreValue) Then
            MessageBox.Show("Please enter a valid numeric score.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' Add record
        Dim success As Boolean = AddVisitLogRecord(
        txtVisitID18.Text,
        txtInternshipID18.Text,
        txtFacultyID18.Text,
        dtpVisitDate18.Value,
        scoreValue,
        txtRemarks18.Text
    )

        If success Then
            MessageBox.Show("Visit Log added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            dgvVisitLogFiles17.DataSource = LoadVisitLogs() ' Refresh DGV
            ' Clear inputs for next entry
            txtVisitID18.Text = ""
            txtInternshipID18.Text = ""
            txtFacultyID18.Text = ""
            txtScore18.Text = ""
            txtRemarks18.Text = ""
            dtpVisitDate18.Value = Date.Today
        End If


        Dim result = MessageBox.Show("Do you want to add this record?", "Confirm Add", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            pnlVisitInformation.Show()
            pnlAddNewVisitRecord.Hide()
        End If
    End Sub


    'Edit visit record
    ' Private Sub pnlEditVisitRecord_Paint(sender As Object, e As PaintEventArgs) Handles pnlEditVisitRecord.Paint

    ' End Sub

    Private Sub txtSearchID19_TextChanged(sender As Object, e As EventArgs) Handles txtSearchID19.TextChanged

    End Sub

    Private Sub btnSearch19_Click(sender As Object, e As EventArgs) Handles btnSearch19.Click
        Dim dt As DataTable = SearchVisitLogRecords(txtSearchID19.Text)
        dgvVisitLogFiles17.DataSource = dt ' show results in DGV

        If dt.Rows.Count = 0 Then
            MessageBox.Show("Visit Log not found.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        ' Load first row into textboxes
        Dim row As DataRow = dt.Rows(0)
        txtVisitID19.Text = row("Visit ID").ToString()
        txtInternshipID19.Text = row("Internship ID").ToString()
        txtFacultyID19.Text = row("Faculty ID").ToString()
        dtpVisitDate19.Value = Convert.ToDateTime(row("Visit Date"))
        txtScore19.Text = Convert.ToDecimal(row("Score")).ToString("F2") ' 2 decimal places
        txtRemarks19.Text = row("Remarks").ToString()

        ' Make Visit ID non-editable
        txtVisitID19.ReadOnly = True
    End Sub

    Private Sub txtVisitID19_TextChanged(sender As Object, e As EventArgs) Handles txtVisitID19.TextChanged

    End Sub

    Private Sub txtInternshipID19_TextChanged(sender As Object, e As EventArgs) Handles txtInternshipID19.TextChanged

    End Sub

    Private Sub txtFacultyID19_TextChanged(sender As Object, e As EventArgs) Handles txtFacultyID19.TextChanged

    End Sub

    Private Sub dtpVisitDate19_ValueChanged(sender As Object, e As EventArgs) Handles dtpVisitDate19.ValueChanged

    End Sub

    Private Sub txtScore19_TextChanged(sender As Object, e As EventArgs) Handles txtScore19.TextChanged

    End Sub

    Private Sub txtRemarks19_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnCancel19_Click(sender As Object, e As EventArgs) Handles btnCancel19.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to cancel?", "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If result = DialogResult.Yes Then
            pnlVisitInformation.Show()
            pnlEditVisitRecord.Hide()
        End If

    End Sub
    Private Sub btnEdit19_Click(sender As Object, e As EventArgs) Handles btnEdit19.Click
        ' Validate score
        Dim scoreValue As Decimal
        If Not Decimal.TryParse(txtScore19.Text, scoreValue) Then
            MessageBox.Show("Please enter a valid numeric score.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim query As String = "
        UPDATE visitlog SET
            internship_id=@internshipID,
            faculty_id=@facultyID,
            visit_date=@visitDate,
            score=@score,
            remarks=@remarks
        WHERE visit_id=@visitID
    "

        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@visitID", txtVisitID19.Text)
                    cmd.Parameters.AddWithValue("@internshipID", txtInternshipID19.Text)
                    cmd.Parameters.AddWithValue("@facultyID", txtFacultyID19.Text)
                    cmd.Parameters.AddWithValue("@visitDate", dtpVisitDate19.Value)
                    cmd.Parameters.AddWithValue("@score", Math.Round(scoreValue, 2))
                    cmd.Parameters.AddWithValue("@remarks", txtRemarks19.Text)

                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()
                End Using
            End Using

            MessageBox.Show("Visit Log record updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Refresh DGV with all records
            dgvVisitLogFiles17.DataSource = LoadVisitLogs()



        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Dim result As DialogResult = MessageBox.Show("Record has been successfully edited.", "Edit Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)

        If result = DialogResult.OK Then
            pnlVisitInformation.Show()
            pnlEditVisitRecord.Hide()
        End If

    End Sub

    'summary report

    Private Sub pnlSummaryReport_Paint(sender As Object, e As PaintEventArgs) Handles pnlSummaryReport.Paint

    End Sub

    Private Sub txtSearchID20_TextChanged(sender As Object, e As EventArgs) Handles txtSearchID20.TextChanged
        Dim searchText = txtSearchID20.Text.Trim

        If searchText = "" Then
            MessageBox.Show("Please enter a value to search.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim dv As New DataView(summaryTable)

        dv.RowFilter =
            $"[Student ID] LIKE '%{searchText}%' OR " &
            $"[Student Name] LIKE '%{searchText}%' OR " &
            $"[Company Name] LIKE '%{searchText}%' OR " &
            $"[Company Contact] LIKE '%{searchText}%' OR " &
            $"[Faculty Name] LIKE '%{searchText}%'"

        dgvSummarySearch.DataSource = dv
    End Sub

    Private Sub btnSearch20_Click(sender As Object, e As EventArgs) Handles btnSearch20.Click
        Dim searchText As String = txtSearchID20.Text.Trim()

        If String.IsNullOrWhiteSpace(searchText) Then
            MessageBox.Show("Please enter Student ID or Name to search.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtSearchID20.Focus()
            Exit Sub
        End If

        ' Perform search
        Dim dt As DataTable = SearchStudentInternshipSummary(searchText)

        ' Handle no results
        If dt.Rows.Count = 0 Then
            dgvSummarySearch.DataSource = Nothing
            MessageBox.Show("No record found!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        ' Column widths
        Dim columnWidths As New Dictionary(Of String, Integer) From {
        {"Student ID", 300},
        {"Student Name", 200},
        {"Internship Status", 350},
        {"Start Date", 300},
        {"End Date", 300},
        {"Company Name", 400},
        {"Company Contact", 500},
        {"Company Contact Grade", 500},
        {"Faculty Name", 500},
        {"Last Visit Date", 500},
        {"Avg Visit Score", 500},
        {"Avg Evaluation Grade", 500},
        {"Avg Company Grade", 500},
        {"Evaluation Status", 500},
        {"Overall Summary Score", 500}
    }

        ' Bind and apply DGVHelper styling
        DGVHelper.BindAndStyleFilesDGV(dgvSummarySearch, dt, columnWidths)

        ' Clear selection
        dgvSummarySearch.ClearSelection()
    End Sub


    Private Sub flpSummaryReport20_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub lblStudentName_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub lblStudentID20_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub lblOverallSummary20_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub pnlSummaryReport20_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub picStudentIcon_Click(sender As Object, e As EventArgs) Handles picStudentIcon.Click
        hidePanel()
        pnlStudentInformation.Show()
        lblTotalRecords1.Text = countStudentRecord()
        loadStudentRecord()
        pnlHome.Hide()
    End Sub

    'Update Internship Record
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        pnlUpdateInternshipRecord.Show()

    End Sub

    'BUTTON HOVER
    'BUTTON UPDATE HOVER INTERNSHIPS
    Private Sub btnUpdate_MouseEnter(sender As Object, e As EventArgs) Handles btnUpdate.MouseEnter
        btnUpdate.BackColor = Color.FromArgb(97, 144, 118)

    End Sub

    Private Sub btnUpdate_MouseLeave(sender As Object, e As EventArgs) Handles btnUpdate.MouseLeave
        btnUpdate.BackColor = Color.FromArgb(8, 48, 25)
    End Sub

    Private Sub btnUpdate_MouseDown(sender As Object, e As MouseEventArgs) Handles btnUpdate.MouseDown
        btnUpdate.BackColor = Color.DarkSeaGreen
    End Sub

    Private Sub btnUpdate_MouseUp(sender As Object, e As MouseEventArgs) Handles btnUpdate.MouseUp
        btnUpdate.BackColor = Color.FromArgb(97, 144, 118)
    End Sub

    Private Sub btnCancelUpdate_Click(sender As Object, e As EventArgs) Handles btnCancelUpdate.Click
        Dim result = MessageBox.Show("Are you sure you want to cancel?", "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If result = DialogResult.Yes Then
            ClearUpdateForm()
            pnlUpdateInternshipRecord.Hide()
            pnlInternshipInformation.Show()
        End If
    End Sub


    Private Sub picHome_Click(sender As Object, e As EventArgs) Handles picHome.Click
        hidePanel()
        pnlHome.Show()
    End Sub

    Private Sub picInternshipIcon_Click(sender As Object, e As EventArgs) Handles picInternshipIcon.Click
        hidePanel()
        pnlInternshipInformation.Show()
        pnlHome.Hide()
    End Sub

    Private Sub picEvaluationIcon_Click(sender As Object, e As EventArgs) Handles picEvaluationIcon.Click
        hidePanel()
        pnlEvaluationInformation.Show()
        pnlHome.Hide()
    End Sub

    Private Sub picCompanyIcon_Click(sender As Object, e As EventArgs) Handles picCompanyIcon.Click
        hidePanel()
        pnlCompanyInformation.Show()
        pnlHome.Hide()
    End Sub

    Private Sub picFacultyIcon_Click(sender As Object, e As EventArgs) Handles picFacultyIcon.Click
        hidePanel()
        pnlFacultyInformation.Show()
        pnlHome.Hide()
    End Sub

    Private Sub picVisitIcon_Click(sender As Object, e As EventArgs) Handles picVisitIcon.Click
        hidePanel()
        pnlVisitInformation.Show()
        pnlHome.Hide()
    End Sub

    Private Sub picSummary_Click(sender As Object, e As EventArgs) Handles picSummary.Click
        hidePanel()
        pnlSummaryReport.Show()
        pnlHome.Hide()
    End Sub

    Private Sub picImport1_Click(sender As Object, e As EventArgs) Handles picImport1.Click
        Dim ofd As New OpenFileDialog
        Dim csvAdded As Boolean
        ofd.Filter = "CSV Files (*.csv)|*.csv"

        If ofd.ShowDialog = DialogResult.OK Then
            Dim dt = LoadCSV(ofd.FileName)
            csvAdded = addStudentsFromCsv(dt)
        End If

        If Not csvAdded Then
            MessageBox.Show("Importing Failed, Check The Format Properly", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        MessageBox.Show("Import Success", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        loadStudentRecord()
        lblTotalRecords1.Text = countStudentRecord()

    End Sub

    Private Sub picShow1_Click(sender As Object, e As EventArgs) Handles picShow1.Click
        Dim dv = CType(dgvStudentFiles.DataSource, DataView)
        dv.RowFilter = ""
    End Sub

    Private Sub picHide1_Click(sender As Object, e As EventArgs) Handles picHide1.Click
        If dgvStudentFiles.SelectedRows.Count = 0 Then
            MessageBox.Show(
        "Please Select Row to Hide", "DGV", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            Exit Sub
        End If

        Dim dt As DataTable = CType(dgvStudentFiles.DataSource, DataTable)
        Dim dv As New DataView(dt)

        For Each row As DataGridViewRow In dgvStudentFiles.SelectedRows
            Dim dr As DataRow = CType(row.DataBoundItem, DataRowView).Row
            dr("Hidden") = True
        Next

        dv.RowFilter = "Hidden = False"
        dgvStudentFiles.DataSource = dv
    End Sub

    Private Sub cmbSection3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSection3.SelectedIndexChanged

    End Sub

    Public Sub DrawComboItem(sender As Object, e As DrawItemEventArgs)
        If e.Index < 0 Then Exit Sub

        Dim combo As ComboBox = DirectCast(sender, ComboBox)
        e.DrawBackground()

        Dim bgColor As Color
        Dim textColor As Color

        If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
            bgColor = Color.FromArgb(218, 239, 228)  ' highlight
            textColor = Color.FromArgb(8, 48, 25)
        Else
            bgColor = Color.White                    ' normal
            textColor = Color.FromArgb(8, 48, 25)
        End If

        Using b As New SolidBrush(bgColor)
            e.Graphics.FillRectangle(b, e.Bounds)
        End Using

        ' CORRECT way to get item text
        Dim text As String = combo.GetItemText(combo.Items(e.Index))

        Using b As New SolidBrush(textColor)
            e.Graphics.DrawString(text, e.Font, b, e.Bounds)
        End Using

        e.DrawFocusRectangle()
    End Sub



    'BUTTON HOVER
    'BUTTON SEARCH INTERNSHIPS HOVER UPDATE
    Private Sub btnSearchInternship_MouseEnter(sender As Object, e As EventArgs) Handles btnSearchInternship.MouseEnter
        btnSearchInternship.BackColor = Color.FromArgb(8, 48, 25)

    End Sub

    Private Sub btnSearchInternship_MouseLeave(sender As Object, e As EventArgs) Handles btnSearchInternship.MouseLeave
        btnSearch1.BackColor = Color.FromArgb(97, 144, 118)
    End Sub

    Private Sub btnSearchInternship_MouseDown(sender As Object, e As MouseEventArgs) Handles btnSearchInternship.MouseDown
        btnSearch1.BackColor = Color.DarkSeaGreen
    End Sub

    Private Sub btnSearchInternship_MouseUp(sender As Object, e As MouseEventArgs) Handles btnSearch1.MouseUp
        btnSearch1.BackColor = Color.FromArgb(8, 48, 25)
    End Sub

    'BUTTON HOVER
    'BUTTON  CANCEL UPDATE HOVER INTERNSHIPS
    Private Sub btnCancelUpdate_MouseEnter(sender As Object, e As EventArgs) Handles btnCancelUpdate.MouseEnter
        btnCancelUpdate.BackColor = Color.FromArgb(8, 48, 25)

    End Sub

    Private Sub btnCancelUpdate_MouseLeave(sender As Object, e As EventArgs) Handles btnCancelUpdate.MouseLeave
        btnCancelUpdate.BackColor = Color.FromArgb(97, 144, 118)
    End Sub

    Private Sub btnCancelUpdate_MouseDown(sender As Object, e As MouseEventArgs) Handles btnCancelUpdate.MouseDown
        btnCancelUpdate.BackColor = Color.DarkSeaGreen
    End Sub

    Private Sub btnCancelUpdate_MouseUp(sender As Object, e As MouseEventArgs) Handles btnCancelUpdate.MouseUp
        btnCancelUpdate.BackColor = Color.FromArgb(8, 48, 25)
    End Sub

    'BUTTON HOVER
    'BUTTON UPDATE RECORD HOVER INTERNSHIPS
    Private Sub btnUpdateRecord_MouseEnter(sender As Object, e As EventArgs) Handles btnUpdateRecord.MouseEnter
        btnUpdateRecord.BackColor = Color.FromArgb(97, 144, 118)

    End Sub

    Private Sub btnUpdateRecord_MouseLeave(sender As Object, e As EventArgs) Handles btnUpdateRecord.MouseLeave
        btnUpdateRecord.BackColor = Color.FromArgb(8, 48, 25)
    End Sub

    Private Sub btnUpdateRecord_MouseDown(sender As Object, e As MouseEventArgs) Handles btnUpdateRecord.MouseDown
        btnUpdateRecord.BackColor = Color.DarkSeaGreen
    End Sub

    Private Sub btnUpdateRecord_MouseUp(sender As Object, e As MouseEventArgs) Handles btnUpdateRecord.MouseUp
        btnUpdateRecord.BackColor = Color.FromArgb(97, 144, 118)
    End Sub





    'CODE FOR UPDATE INTERNSHIP RECORD - IRIS

    ' ----- Module-level variable -----
    Private loadedInternshipID As String = ""
    ' ----- Search Internship -----
    Private Sub btnSearchInternship_Click(sender As Object, e As EventArgs) Handles btnSearchInternship.Click
        If txtboxInternshipID.Text.Trim() = "" Then
            MessageBox.Show("Please enter Internship ID!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim dt As DataTable = searchInterTable(txtboxInternshipID.Text.Trim())
        If dt.Rows.Count = 0 Then
            MessageBox.Show("No internship record found!", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim row As DataRow = dt.Rows(0)

        ' Fill textboxes
        txtFNameUpdateInternship.Text = row("First Name").ToString()
        txtLNameUpdateInternship.Text = row("Last Name").ToString()
        txtboxInternshipID.Text = row("Internship ID").ToString()

        ' Track loaded ID
        loadedInternshipID = txtboxInternshipID.Text.Trim()

        ' Load ComboBoxes and auto-select
        LoadCompanies(row("Company Name").ToString())
        LoadSupervisors(row("Supervisor Last Name").ToString())

        ' Set dates
        SetDatePicker(dtpStartDate, If(IsDBNull(row("Start_Date")), Nothing, CDate(row("Start_Date"))))
        SetDatePicker(dtpEndDate, If(IsDBNull(row("End_Date")), Nothing, CDate(row("End_Date"))))

        ' Set status
        cmbStatusUpdateInternship.Text = row("Status").ToString()
        cmbStatusUpdateInternship_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    ' ----- Load Companies ComboBox -----
    Private Sub LoadCompanies(selectedCompany As String)
        cmbCompanyInternship.Items.Clear()
        cmbCompanyInternship.DropDownStyle = ComboBoxStyle.DropDownList ' <-- non-editable

        Dim companies As New HashSet(Of String)

        Using con As MySqlConnection = GetConnection()
            con.Open()
            Dim cmd As New MySqlCommand("SELECT company_name FROM company", con)
            Dim reader = cmd.ExecuteReader()
            While reader.Read()
                Dim compName As String = reader("company_name").ToString()
                If Not companies.Contains(compName) Then
                    companies.Add(compName)
                    cmbCompanyInternship.Items.Add(compName)
                End If
            End While
        End Using

        ' Auto-select
        If selectedCompany <> "" AndAlso cmbCompanyInternship.Items.Contains(selectedCompany) Then
            cmbCompanyInternship.Text = selectedCompany
        End If
    End Sub

    ' ----- Load Supervisors ComboBox -----
    Private Sub LoadSupervisors(selectedSupervisor As String)
        cmbCompanyContactInternship.Items.Clear()
        cmbCompanyContactInternship.DropDownStyle = ComboBoxStyle.DropDownList ' <-- non-editable

        Dim dict As New Dictionary(Of String, List(Of String)) ' key = last name, value = list of first names

        Using con As MySqlConnection = GetConnection()
            con.Open()
            Dim cmd As New MySqlCommand("SELECT contact_last_name, contact_first_name FROM company_contact", con)
            Dim reader = cmd.ExecuteReader()
            While reader.Read()
                Dim lastName As String = reader("contact_last_name").ToString()
                Dim firstName As String = reader("contact_first_name").ToString()

                If dict.ContainsKey(lastName) Then
                    dict(lastName).Add(firstName)
                Else
                    dict.Add(lastName, New List(Of String) From {firstName})
                End If
            End While
        End Using

        ' Populate ComboBox
        For Each kvp In dict
            If kvp.Value.Count = 1 Then
                cmbCompanyContactInternship.Items.Add(kvp.Key)
            Else
                For Each fName In kvp.Value
                    cmbCompanyContactInternship.Items.Add($"{kvp.Key}, {fName}")
                Next
            End If
        Next

        ' Auto-select
        If selectedSupervisor <> "" AndAlso cmbCompanyContactInternship.Items.Contains(selectedSupervisor) Then
            cmbCompanyContactInternship.Text = selectedSupervisor
        End If
    End Sub


    ' ----- Status Logic -----
    Private Sub cmbStatusUpdateInternship_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbStatusUpdateInternship.SelectedIndexChanged
        Select Case cmbStatusUpdateInternship.Text
            Case "Pending"
                ' Start date: only if empty
                If dtpStartDate.CustomFormat = " " Then
                    SetDatePickerEmpty(dtpStartDate)
                End If
                dtpStartDate.Enabled = True

                ' End date
                If dtpEndDate.CustomFormat = " " Then
                    SetDatePickerEmpty(dtpEndDate)
                End If
                dtpEndDate.Enabled = False

            Case "Ongoing"
                dtpStartDate.Enabled = True
                If dtpEndDate.CustomFormat <> " " Then ClearDatePicker(dtpEndDate)
                dtpEndDate.Enabled = False

            Case "Completed"
                dtpStartDate.Enabled = False
                dtpEndDate.Enabled = True
                If dtpEndDate.CustomFormat = " " Then
                    SetDatePicker(dtpEndDate, Date.Today) ' <- here
                End If
        End Select
    End Sub

    ' ----- Clear / Set DateTimePickers -----
    Private Sub ClearDatePicker(dtp As DateTimePicker)
        dtp.CustomFormat = " "
        dtp.Format = DateTimePickerFormat.Custom
    End Sub

    Private Sub SetDatePicker(dtp As DateTimePicker, dtValue As Date?)
        If dtValue.HasValue Then
            ' Make sure value is within MinDate and MaxDate
            If dtValue.Value < dtp.MinDate Then dtValue = dtp.MinDate
            If dtValue.Value > dtp.MaxDate Then dtValue = dtp.MaxDate

            dtp.Format = DateTimePickerFormat.Custom
            dtp.CustomFormat = "yyyy-MM-dd"
            dtp.Value = dtValue.Value
        Else
            ' Empty date
            dtp.Format = DateTimePickerFormat.Custom
            dtp.CustomFormat = " "
        End If
    End Sub

    ' ----- Set DateTimePicker Empty (helper for Pending) -----
    Private Sub SetDatePickerEmpty(dtp As DateTimePicker)
        dtp.Format = DateTimePickerFormat.Custom
        dtp.CustomFormat = " "
    End Sub

    ' ----- Get nullable DateTime from DateTimePicker -----
    Private Function GetDatePickerValue(dtp As DateTimePicker) As Date?
        If dtp.Format = DateTimePickerFormat.Custom AndAlso dtp.CustomFormat = " " Then
            Return Nothing
        Else
            Return dtp.Value
        End If
    End Function



    ' ----- Update Record Button -----
    Private Sub btnUpdateRecord_Click(sender As Object, e As EventArgs) Handles btnUpdateRecord.Click
        ' Validate Internship ID
        If txtboxInternshipID.Text.Trim().ToUpper() <> loadedInternshipID.Trim().ToUpper() Then
            MessageBox.Show("Error: You can only update the originally loaded Internship ID.",
                    "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        ' Get nullable dates
        Dim startDate As Date? = GetDatePickerValue(dtpStartDate)
        Dim endDate As Date? = GetDatePickerValue(dtpEndDate)

        ' Call update function
        Dim success As Boolean = updateInternshipRecord(
                             txtboxInternshipID.Text,
                             cmbCompanyInternship.Text,
                             cmbCompanyContactInternship.Text,
                             startDate,
                             endDate,
                             cmbStatusUpdateInternship.Text
                          )

        If success Then
            MessageBox.Show("Internship record updated successfully!", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Refresh DataGridView
            dgvInternshipFiles4.DataSource = searchInterTable("")

            ' Clear form
            ClearUpdateForm()
        End If
    End Sub


    ' ----- Cancel / Clear Form -----
    Private Sub ClearUpdateForm()
        txtboxInternshipID.Clear()
        txtFNameUpdateInternship.Clear()
        txtLNameUpdateInternship.Clear()
        cmbCompanyInternship.SelectedIndex = -1
        cmbCompanyContactInternship.SelectedIndex = -1
        dtpStartDate.CustomFormat = " "
        dtpEndDate.CustomFormat = " "
        cmbStatusUpdateInternship.SelectedIndex = -1
        loadedInternshipID = ""
    End Sub


    'EVALUATION DGV

    Private Sub LoadEvaluationData()
        ' 1️⃣ Kunin ang DataTable
        Dim dt As DataTable = LoadEvaluationTable()

        ' 2️⃣ I-bind sa DataGridView
        dgvEvaluationFiles5.DataSource = dt

        ' 3️⃣ I-disable ang AutoSizeColumnsMode
        dgvEvaluationFiles5.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None

        ' 4️⃣ I-define ang column widths
        Dim columnWidths As New Dictionary(Of String, Integer) From {
        {"Evaluation ID", 170},
        {"Internship ID", 170},
        {"First Name", 250},
        {"Last Name", 250},
        {"Company Name", 300},
        {"Grade", 120},
        {"Evaluation Report", 300},
        {"Faculty ID", 120}
    }

        ' 5️⃣ I-apply ang DGVHelper styling + column widths
        DGVHelper.BindAndStyleFilesDGV(dgvEvaluationFiles5, dt, columnWidths)
    End Sub


    Private Sub LoadEvaluationDGV(Optional searchItem As String = "")
        dgvEvaluationFiles5.DataSource = LoadEvaluationTable()
        dgvEvaluationFiles5.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvEvaluationFiles5.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvEvaluationFiles5.MultiSelect = False
        dgvEvaluationFiles5.ReadOnly = True
        dgvEvaluationFiles5.AllowUserToAddRows = False  ' <- dito mawawala yung asterisk
        txtEvaluationID6.Text = GenerateEvaluationID()
    End Sub



    'EVALUATION ADD
    Private Sub LoadEvaluationInfo(evaluationID As String)
        Dim dt As DataTable = GetEvaluationInfo(evaluationID)
        If dt.Rows.Count > 0 Then
            txtEvaluationID6.Text = dt.Rows(0)("evaluation_id").ToString()
            txtInternshipID6.Text = dt.Rows(0)("internship_id").ToString()
            txtEvaluationReport6.Text = dt.Rows(0)("evaluation_report").ToString()
            cmbStatus6.Text = dt.Rows(0)("evaluation_status").ToString()

            If Not IsDBNull(dt.Rows(0)("grade")) Then
                nudEvaluationGrade.Value = Convert.ToDecimal(dt.Rows(0)("grade"))
            Else
                nudEvaluationGrade.Value = 0
            End If
        End If
    End Sub

    Private Sub pnlAddNewInternshipEvaluationRecord_VisibleChanged(sender As Object, e As EventArgs) Handles pnlAddNewInternshipEvaluationRecord.VisibleChanged
        If pnlAddNewInternshipEvaluationRecord.Visible Then
            LoadStatusOptions()
            LoadFacultyCombo()
            cmbStatus6.SelectedIndex = -1 ' no preselected
            txtEvaluationID6.Text = GenerateEvaluationID()
            txtEvaluationID6.ReadOnly = True
            txtInternshipID6.Clear()
            txtEvaluationReport6.Clear()
            nudEvaluationGrade.Value = 0
        End If
    End Sub

    Private Sub LoadStatusOptions()
        cmbStatus6.Items.Clear()
        cmbStatus6.DropDownStyle = ComboBoxStyle.DropDownList
        cmbStatus6.Items.Add("Passed")
        cmbStatus6.Items.Add("Failed")
        cmbStatus6.Items.Add("Incomplete")
        cmbStatus6.SelectedIndex = -1

    End Sub
    Private Sub LoadFacultyCombo()
        cmbFaculty7.DropDownStyle = ComboBoxStyle.DropDownList

        ' Example: Load from database (faculty_id, faculty_name)
        Dim dt As New DataTable()
        Try
            Using con As MySqlConnection = Connectivity.GetConnection()
                Using cmd As New MySqlCommand("SELECT faculty_id FROM faculty", con)
                    Using adapter As New MySqlDataAdapter(cmd)
                        adapter.Fill(dt)
                    End Using
                End Using
            End Using

            cmbFaculty7.DataSource = dt
            cmbFaculty7.ValueMember = "faculty_id"
            cmbFaculty7.SelectedIndex = -1  ' Default walang selection
        Catch ex As Exception
            MessageBox.Show("Error loading faculty list: " & ex.Message)
        End Try
    End Sub

    'CLEAR HELPER

    Private Sub ClearAllControls(container As Control)
        For Each ctrl As Control In container.Controls

            If TypeOf ctrl Is TextBox Then
                CType(ctrl, TextBox).Clear()

            ElseIf TypeOf ctrl Is ComboBox Then
                CType(ctrl, ComboBox).SelectedIndex = -1

            ElseIf TypeOf ctrl Is DateTimePicker Then
                CType(ctrl, DateTimePicker).Value = DateTime.Now
            ElseIf TypeOf ctrl Is NumericUpDown Then
                CType(ctrl, NumericUpDown).Value = 0
            ElseIf ctrl.HasChildren Then
                ClearAllControls(ctrl)

            End If
        Next
    End Sub



    'FOR COLUMNS STYLES INTERNSHIPS
    Private Sub dgvEvaluationFiles5tyles(dgv As DataGridView)

        If dgv.Columns.Count = 0 Then Exit Sub  ' ← Prevent crash

        dgv.Columns("Evaluation ID").Width = 170
        dgv.Columns("Internship ID").Width = 170
        dgv.Columns("Grade").Width = 200
        dgv.Columns("Evaluation Report").Width = 300
        dgv.Columns("Evaluation Status").Width = 200
        dgv.Columns("Faculty ID").Width = 170


        dgv.EnableHeadersVisualStyles = False
        dgv.AdvancedColumnHeadersBorderStyle.All = DataGridViewAdvancedCellBorderStyle.None

        With dgvEvaluationFiles5
            .EnableHeadersVisualStyles = False
            .RowHeadersDefaultCellStyle.BackColor = Color.MintCream   ' Palitan ng gusto mong kulay
            .RowHeadersDefaultCellStyle.SelectionBackColor = Color.LightYellow
            .RowHeadersWidth = 20
        End With


    End Sub

    'FOR HOVER EFFECT DGVINTERNSHIP 


    Private Sub dgvEvaluationFiles5_CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgvEvaluationFiles5.CellMouseEnter
        If e.RowIndex >= 0 Then

            ' Ibabalik yung last row sa original color
            If lastRowInternship >= 0 Then
                dgvEvaluationFiles5.Rows(lastRowInternship).DefaultCellStyle.BackColor = Color.White

            End If

            ' Highlight current hovered row
            dgvEvaluationFiles5.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.Honeydew

            lastRowInternship = e.RowIndex
        End If
    End Sub

    Private Sub dgvEvaluationFiles5_CellMouseLeave(sender As Object, e As DataGridViewCellEventArgs) Handles dgvEvaluationFiles5.CellMouseLeave
        If lastRowInternship >= 0 Then
            dgvEvaluationFiles5.Rows(lastRowInternship).DefaultCellStyle.BackColor = Color.White
        End If
    End Sub

    Private Sub dgvEvaluationFiles5_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvEvaluationFiles5.DataBindingComplete

        dgvEvaluationFiles5.ClearSelection()
        dgvEvaluationFiles5.CurrentCell = Nothing
        Me.ActiveControl = Nothing


    End Sub



    Private Sub lblTotalRecords1_Click(sender As Object, e As EventArgs) Handles lblTotalRecords1.Click

    End Sub

    Private Sub dgvStudentFiles_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvStudentFiles.CellContentClick

    End Sub

    Private Sub dgvStudentSearch_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvStudentSearch.CellContentClick

    End Sub

    Private Sub Panel17_Paint(sender As Object, e As PaintEventArgs)

    End Sub


    Private Sub LinkLabel1_LinkClicked_2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1_LinkClicked_2.LinkClicked
        Dim filePath = "C:\PLP_Group2_MariaDBCaseStudy\Evaluation_Summary_Form.docx"

        If IO.File.Exists(filePath) Then
            Process.Start(New ProcessStartInfo() With {
                .FileName = filePath,
                .UseShellExecute = True
            })
        Else
            MessageBox.Show("Word file not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub LinkLabel2_LinkClicked_3(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim filePath = "C:\PLP_Group2_MariaDBCaseStudy\Evaluation_Summary_Form.pdf"

        If Not IO.File.Exists(filePath) Then
            MessageBox.Show("PDF file not found!" & vbCrLf & "Please check the path: " & filePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Try
            ' Buksan ang PDF gamit ang default viewer
            Dim psi As New ProcessStartInfo() With {
            .FileName = filePath,
            .UseShellExecute = True
        }
            Process.Start(psi)
        Catch ex As Exception
            ' Kung may error sa pagbukas ng PDF
            MessageBox.Show("Failed to open PDF!" & vbCrLf & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadCompanyDataToDGV()
        Try
            dgvCompanyFiles8.DataSource = LoadCompanyTable()
            dgvCompanyFiles8.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Catch ex As Exception
            MessageBox.Show("Error loading company data: " & ex.Message)
        End Try
    End Sub

    Private Sub btnSearch8_Click(sender As Object, e As EventArgs) Handles btnSearch8.Click
        Dim searchText As String = txtSearchID8.Text.Trim()

        If searchText = "" Then
            MessageBox.Show("Please enter Company ID or Company Name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim dt As DataTable = SearchCompany(searchText)

        If dt.Rows.Count = 0 Then
            MessageBox.Show("No results found for: " & searchText, "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information)
            dgvCompanyLogs8.DataSource = Nothing
            Exit Sub
        End If

        dgvCompanyLogs8.DataSource = dt
        dgvCompanyLogs8.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        LoadCompanyDataWithSearch(txtSearchID8.Text)
    End Sub

    Private Sub LoadCompanyContactsTable()
        Dim dt As DataTable = LoadCompanyContacts()

        If dt.Rows.Count = 0 Then
            dgvCompanyContactFiles.DataSource = Nothing
            Exit Sub
        End If

        dgvCompanyContactFiles.DataSource = dt

        dgvCompanyContactFiles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvCompanyContactFiles.RowHeadersVisible = False
    End Sub

    Private Sub LoadVisitLogTable()
        Dim dt As DataTable = LoadVisitLogs()
        dgvVisitLogFiles17.DataSource = dt

        ' OPTIONAL formatting
        dgvVisitLogFiles17.Columns("Visit ID").HeaderText = "Visit ID"
        dgvVisitLogFiles17.Columns("Internship ID").HeaderText = "Internship ID"
        dgvVisitLogFiles17.Columns("Faculty ID").HeaderText = "Faculty ID"
        dgvVisitLogFiles17.Columns("Visit Date").HeaderText = "Visit Date"
        dgvVisitLogFiles17.Columns("Remarks").HeaderText = "Remarks"
        dgvVisitLogFiles17.Columns("Score").HeaderText = "Score"

        dgvVisitLogFiles17.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvVisitLogFiles17.RowHeadersVisible = False
    End Sub

    Private Sub LoadInternshipSummaryTable()
        Dim dt As DataTable = LoadStudentInternshipSummary()

        dgvSummaryFiles.DataSource = dt

        dgvSummaryFiles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvSummaryFiles.RowHeadersVisible = False
    End Sub

    Private Sub LoadInternshipData(Optional searchText As String = "")
        Dim dt As DataTable = searchInterTable(searchText)

        dgvInternshipLogs4.DataSource = dt
        dgvInternshipLogs4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None

        ' Column widths
        Dim columnWidths As New Dictionary(Of String, Integer) From {
            {"Internship ID", 170},
        {"First Name", 200},
        {"Last Name", 200},
        {"Company Name", 200},
        {"Supervisor Last Name", 200},
        {"Supervisor Contact", 150},
        {"Start_Date", 120},
        {"End_Date", 120},
        {"Status", 100}
    }

        DGVHelper.BindAndStyleFilesDGV(dgvInternshipLogs4, dt, columnWidths)
        dgvInternshipLogs4.ClearSelection()
    End Sub

    Private Sub LoadCompanyData()
        ' Load data from database
        Dim dt As DataTable = LoadCompanyTable()

        ' Set as DataSource
        dgvCompanyFiles8.DataSource = dt
        dgvCompanyFiles8.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None

        ' Define your desired column widths
        Dim columnWidths As New Dictionary(Of String, Integer) From {
            {"Company ID", 170},
            {"Company Name", 400},
            {"Company Address", 500},
            {"Industry Type", 200},
            {"Contact Number", 150},
            {"Email", 200}
        }

        ' Apply the styling using your DGVHelper module
        DGVHelper.BindAndStyleFilesDGV(dgvCompanyFiles8, dt, columnWidths)

        ' Clear selection
        dgvCompanyFiles8.ClearSelection()
    End Sub

    Private Sub LoadCompanyDataWithSearch(searchText As String)
        Dim dt As DataTable

        If String.IsNullOrEmpty(searchText) Then
            ' If search text is empty, load all data
            dt = LoadCompanyTable()
        Else
            ' If there's search text, use the search function
            dt = SearchCompany(searchText)
        End If

        ' Set as DataSource
        dgvCompanyLogs8.DataSource = dt
        dgvCompanyLogs8.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None

        ' Define your desired column widths
        Dim columnWidths As New Dictionary(Of String, Integer) From {
           {"Company ID", 170},
            {"Company Name", 400},
            {"Company Address", 500},
            {"Industry Type", 200},
            {"Contact Number", 150},
            {"Email", 200}
        }

        ' Apply the styling using your DGVHelper module
        DGVHelper.BindAndStyleFilesDGV(dgvCompanyLogs8, dt, columnWidths)

        ' Clear selection
        dgvCompanyLogs8.ClearSelection()

        ' Optional: Show message if no results found
        If dt.Rows.Count = 0 AndAlso Not String.IsNullOrEmpty(searchText) Then
            MessageBox.Show("No companies found matching your search criteria.", "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub LoadCompanyContactsData()
        ' 1️⃣ Kunin ang DataTable mula sa database
        Dim dt As DataTable = LoadCompanyContacts()

        ' 2️⃣ I-bind sa DataGridView
        dgvCompanyContactFiles.DataSource = dt

        ' 3️⃣ I-disable AutoSizeColumnsMode para mag-work ang manual widths
        dgvCompanyContactFiles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None

        ' 4️⃣ I-define ang column widths (adjust based on your layout)
        Dim columnWidths As New Dictionary(Of String, Integer) From {
        {"Contact ID", 170},
        {"Company ID", 170},
        {"Company Name", 250},
        {"Contact First Name", 200},
        {"Contact Last Name", 200},
        {"Position", 150},
        {"Contact No", 120},
        {"Email", 200},
        {"Student First Name", 200},
        {"Student Last Name", 200},
        {"Grade", 150}
    }

        ' 5️⃣ I-apply ang DGVHelper styling + column widths
        DGVHelper.BindAndStyleFilesDGV(dgvCompanyContactFiles, dt, columnWidths)

        ' 6️⃣ Clear selection para walang naka-highlight
        dgvCompanyContactFiles.ClearSelection()
    End Sub

    Private Sub LoadFacultyData()
        ' 1️⃣ Kunin ang buong table
        Dim dt As DataTable = loadTable("SELECT 
                                        f.faculty_id AS 'Faculty ID',
                                        f.faculty_first_name AS 'First Name',
                                        f.faculty_last_name AS 'Last Name',
                                        f.faculty_position AS 'Position',
                                        d.department_name AS 'Department',
                                        f.faculty_contact_no AS 'Faculty Contact'
                                     FROM faculty f
                                     INNER JOIN department d ON f.department_id = d.department_id
                                     ORDER BY f.faculty_id ASC")

        ' 2️⃣ Bind to DataGridView
        dgvFacultyFiles.DataSource = dt
        dgvFacultyFiles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None

        ' 3️⃣ Column widths
        Dim columnWidths As New Dictionary(Of String, Integer) From {
        {"Faculty ID", 170},
        {"First Name", 200},
        {"Last Name", 200},
        {"Position", 170},
        {"Department", 300},
        {"Faculty Contact", 150}
    }

        ' 4️⃣ Apply DGVHelper styling
        DGVHelper.BindAndStyleFilesDGV(dgvFacultyFiles, dt, columnWidths)

        ' 5️⃣ Clear selection
        dgvFacultyFiles.ClearSelection()
    End Sub

    Private Sub LoadVisitLogsData()
        ' 1️⃣ Kunin ang DataTable mula sa database
        Dim dt As DataTable = LoadVisitLogs()

        ' 2️⃣ Bind sa DataGridView
        dgvVisitLogFiles17.DataSource = dt
        dgvVisitLogFiles17.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None

        ' 3️⃣ Column widths
        Dim columnWidths As New Dictionary(Of String, Integer) From {
        {"Visit ID", 170},
        {"Internship ID", 170},
        {"Faculty ID", 170},
        {"Faculty First Name", 200},
        {"Faculty Last Name", 200},
        {"Visit Date", 150},
        {"Remarks", 350},
        {"Score", 120}
    }

        ' 4️⃣ Apply DGVHelper styling
        DGVHelper.BindAndStyleFilesDGV(dgvVisitLogFiles17, dt, columnWidths)

        ' 5️⃣ Clear selection
        dgvVisitLogFiles17.ClearSelection()
    End Sub


    Private Sub LoadStudentSummaryData()
        ' 1️⃣ Kunin ang buong summary
        Dim dt As DataTable = LoadStudentInternshipSummary()

        ' 2️⃣ Bind sa DataGridView
        dgvSummaryFiles.DataSource = dt
        dgvSummaryFiles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None

        ' 3️⃣ Column widths (adjustable ayon sa laki ng content)
        Dim columnWidths As New Dictionary(Of String, Integer) From {
        {"Student ID", 200},
        {"Student Name", 300},
        {"Internship Status", 300},
        {"Start Date", 200},
        {"End Date", 200},
        {"Company Name", 300},
        {"Company Contact", 200},
        {"Company Contact Grade", 200},
        {"Faculty Name", 300},
        {"Last Visit Date", 200},
        {"Avg Visit Score", 200},
        {"Avg Evaluation Grade", 200},
        {"Avg Company Grade", 200},
        {"Evaluation Status", 200},
        {"Overall Summary Score", 200}
    }

        ' 4️⃣ Apply DGVHelper styling
        DGVHelper.BindAndStyleFilesDGV(dgvSummaryFiles, dt, columnWidths)

        ' 5️⃣ Clear selection
        dgvSummaryFiles.ClearSelection()
    End Sub

    Private Sub picHide3_Click(sender As Object, e As EventArgs)

    End Sub

    ' Function to get the next Company ID like C001, C002, etc.
    Private Function GetNextCompanyID() As String
        Dim nextID As String = "C001" ' default if table is empty
        Try
            Using con As MySqlConnection = GetConnection()
                Dim query As String = "SELECT company_id FROM company ORDER BY company_id DESC LIMIT 1"
                Using cmd As New MySqlCommand(query, con)
                    con.Open()
                    Dim lastID As Object = cmd.ExecuteScalar()
                    con.Close()

                    If lastID IsNot Nothing Then
                        ' Extract number part
                        Dim num As Integer = Integer.Parse(lastID.ToString().Substring(1))
                        num += 1
                        nextID = "C" & num.ToString("D3") ' format: C001, C002, etc.
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return nextID
    End Function

    Public Function AddCompanyRecord(Optional companyID As String = "",
                                 Optional companyName As String = "",
                                 Optional companyAddress As String = "",
                                 Optional industryType As String = "",
                                 Optional companyContact As String = "",
                                 Optional companyEmail As String = "") As Boolean
        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand("
            INSERT INTO company 
                (company_id, company_name, company_address, industry_type, company_contact_no, company_email)
            VALUES 
                (@companyID, @companyName, @companyAddress, @industryType, @companyContact, @company_email)
        ", con)

                    con.Open()
                    cmd.Parameters.AddWithValue("@companyID", companyID)
                    cmd.Parameters.AddWithValue("@companyName", companyName)
                    cmd.Parameters.AddWithValue("@companyAddress", companyAddress)
                    cmd.Parameters.AddWithValue("@industryType", industryType)
                    cmd.Parameters.AddWithValue("@companyContact", companyContact)
                    cmd.Parameters.AddWithValue("@company_email", companyEmail)

                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
        Return True
    End Function

    Public Function EditCompanyRecord(companyID As String,
                                  companyName As String,
                                  companyAddress As String,
                                  industryType As String,
                                  companyContact As String,
                                  companyEmail As String) As Boolean
        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand("
                UPDATE company SET 
                    company_name = @companyName,
                    company_address = @companyAddress,
                    industry_type = @industryType,
                    company_contact_no = @companyContact,
                    company_email = @companyEmail
                WHERE company_id = @companyID
            ", con)

                    con.Open()

                    cmd.Parameters.AddWithValue("@companyID", companyID)
                    cmd.Parameters.AddWithValue("@companyName", companyName)
                    cmd.Parameters.AddWithValue("@companyAddress", companyAddress)
                    cmd.Parameters.AddWithValue("@industryType", industryType)
                    cmd.Parameters.AddWithValue("@companyContact", companyContact)
                    cmd.Parameters.AddWithValue("@companyEmail", companyEmail)

                    cmd.ExecuteNonQuery()
                End Using
            End Using

            Return True

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function



    Private Sub RoundedButton9_Click(sender As Object, e As EventArgs)
        If txtCompanyID10.Text.Trim = "" Then
            MessageBox.Show("Please search a Company ID first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim confirm = MessageBox.Show("Do you want to save changes to this record?",
                                                      "Confirm Edit",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Question)
        If confirm = DialogResult.No Then Exit Sub

        Dim success = EditCompanyRecord(
            txtCompanyID10.Text,
            txtCompanyName10.Text,
            txtAddress10.Text,
            txtIndustryType10.Text,
            txtContactNumber10.Text,
            txtEmail10.Text
        )

        If success Then
            MessageBox.Show("Record has been successfully edited.", "Edit Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadCompanyTable()  ' Refresh DGV
            pnlAddNewCompanyandCompanyContact.Hide()
            pnlCompanyInformation.Show()
            pnlEditCompanyRecord.Hide()
        End If
    End Sub

    Private Sub RoundedButton10_Click(sender As Object, e As EventArgs) Handles RoundedButton10.Click
        If txtCompanyID10.Text.Trim = "" Then
            MessageBox.Show("Please search a Company ID first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim confirm As DialogResult = MessageBox.Show("Do you want to save changes to this record?",
                                                      "Confirm Edit",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Question)
        If confirm = DialogResult.No Then Exit Sub

        Dim success As Boolean = EditCompanyRecord(
            txtCompanyID10.Text,
            txtCompanyName10.Text,
            txtAddress10.Text,
            txtIndustryType10.Text,
            txtContactNumber10.Text,
            txtEmail10.Text
        )

        If success Then
            MessageBox.Show("Record has been successfully edited.", "Edit Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadCompanyTable()  ' Refresh DGV
            pnlAddNewCompanyandCompanyContact.Hide()
            pnlCompanyInformation.Show()
            pnlEditCompanyRecord.Hide()
        End If
    End Sub
    Public Function LoadVisitLogs() As DataTable
        Dim dt As New DataTable()

        Try
            Using con As MySqlConnection = GetConnection()
                Using da As New MySqlDataAdapter("
                SELECT 
                    v.visit_id AS 'Visit ID',
                    v.internship_id AS 'Internship ID',
                    v.faculty_id AS 'Faculty ID',
                    v.visit_date AS 'Visit Date',
                    FORMAT(v.score, 2) AS 'Score',
                    v.remarks AS 'Remarks'
                FROM visitlog v
                ORDER BY v.visit_id ASC
            ", con)
                    da.Fill(dt)
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return dt
    End Function

    Private Sub lnklblPython_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        Process.Start("explorer.exe", "C:\Users\acer\PycharmProjects\PythonProject\BSIT-2D\DraftFinal.ipynb")
    End Sub

    Private Sub pctBoxIcon_Click(sender As Object, e As EventArgs) Handles pctBoxIcon.Click
        Dim ofd As New OpenFileDialog()
        ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif"
        ofd.Title = "Select an Image"

        If ofd.ShowDialog() = DialogResult.OK Then
            ' Load image safely
            Dim img As Image = Image.FromFile(ofd.FileName)
            Dim imgCopy As New Bitmap(img)
            img.Dispose()

            ' Set PictureBox (memory only, not DB yet)
            pctBoxIcon.Image = imgCopy
            pctBoxIcon.SizeMode = PictureBoxSizeMode.StretchImage

            ' Optional: mark picture changed
            PictureChanged = True
        End If
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Dim ofd As New OpenFileDialog()
        ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif"
        ofd.Title = "Select an Image"

        If ofd.ShowDialog() = DialogResult.OK Then
            ' Load image safely
            Dim img As Image = Image.FromFile(ofd.FileName)
            Dim imgCopy As New Bitmap(img)
            img.Dispose()

            ' Set PictureBox (memory only, not DB yet)
            pctBoxIcon.Image = imgCopy
            pctBoxIcon.SizeMode = PictureBoxSizeMode.StretchImage

            ' Optional: mark picture changed
            PictureChanged = True
        End If
    End Sub



    Private Sub btnOpenAnalytics_Click(sender As Object, e As EventArgs) Handles btnOpenAnalytics.Click
        Dim f As New FormAnalytics()
        f.ShowDialog()
    End Sub

    Private Sub lnklblPython_LinkClicked_1(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnklblPython.LinkClicked
        Process.Start("explorer.exe", "C:\Users\acer\PycharmProjects\PythonProject\BSIT-2D\OJT_Data_Machine_Learning.ipynb")

    End Sub




    'FINAL BACKUP CODE 
    'ETO IYON

    '1529, 902
End Class
