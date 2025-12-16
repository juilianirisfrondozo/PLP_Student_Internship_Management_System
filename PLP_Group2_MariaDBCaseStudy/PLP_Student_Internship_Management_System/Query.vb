Imports MySql.Data.MySqlClient
Imports Windows.Win32.System

Module Query
    Public Function loadTable(query As String) As DataTable
        Dim dataTable As New DataTable()

        Try

            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand(query, con)
                    con.Open()
                    Using dA As New MySqlDataAdapter(cmd)
                        dA.Fill(dataTable)
                    End Using
                    con.Close()
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return dataTable
    End Function

    Public Function LoadCombo(query As String) As DataTable
        Dim dt As New DataTable()

        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand(query, con)
                    con.Open()

                    Using dA As New MySqlDataAdapter(cmd)
                        dA.Fill(dt)
                    End Using

                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return dt
    End Function

    Public Function searchStudentTable(searchItem As String) As DataTable
        Dim dataTable As New DataTable()

        Dim query As String = "SELECT 
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
                              INNER JOIN course c ON s.course_id = c.course_id
                              WHERE s.first_name LIKE @search OR s.last_name LIKE @search OR
                              s.student_id LIKE @search;"

        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand(query, con)
                    con.Open()
                    cmd.Parameters.AddWithValue("@search", "%" & searchItem & "%")
                    Using dA As New MySqlDataAdapter(cmd)
                        dA.Fill(dataTable)
                    End Using
                    con.Close()
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return dataTable
    End Function
    Public Function GetStudentInfo(studentID As String) As DataTable
        Dim dataTable As New DataTable()

        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand("
                SELECT *
                FROM student
                WHERE student_id = @id", con)
                    cmd.Parameters.AddWithValue("@id", studentID)
                    con.Open()

                    Using dA As New MySqlDataAdapter(cmd)
                        dA.Fill(dataTable)
                    End Using
                    con.Close()
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return dataTable
    End Function

    Public Function addStudentRecord(stdID As String, fname As String, lname As String, gen As String, sec As String,
                                 contact As String, email As String, depID As String, crsID As String) As Boolean
        Try
            Using con As MySqlConnection = GetConnection()
                con.Open()
                Using trans As MySqlTransaction = con.BeginTransaction()

                    ' Insert student
                    Using studentCmd As New MySqlCommand("
                INSERT INTO student 
                    (student_id, first_name, last_name, gender, section_name, contact_no, email, department_id, course_id) 
                VALUES 
                    (@studentID, @first_name, @last_name, @gender, @section, @contact, @email, @departmentID, @courseID)", con, trans)

                        studentCmd.Parameters.AddWithValue("@studentID", stdID)
                        studentCmd.Parameters.AddWithValue("@first_name", fname)
                        studentCmd.Parameters.AddWithValue("@last_name", lname)
                        studentCmd.Parameters.AddWithValue("@gender", gen)
                        studentCmd.Parameters.AddWithValue("@section", sec)
                        studentCmd.Parameters.AddWithValue("@contact", contact)
                        studentCmd.Parameters.AddWithValue("@email", email)
                        studentCmd.Parameters.AddWithValue("@departmentID", depID)
                        studentCmd.Parameters.AddWithValue("@courseID", crsID)

                        studentCmd.ExecuteNonQuery()
                    End Using

                    ' Insert internship for the same student (default status = "Pending")
                    Using internCmd As New MySqlCommand("
                INSERT INTO internship 
                    (internship_id, student_id, status) 
                VALUES 
                    (@internID, @studentID, 'Pending')", con, trans)

                        internCmd.Parameters.AddWithValue("@internID", GenerateInternID())
                        internCmd.Parameters.AddWithValue("@studentID", stdID)

                        internCmd.ExecuteNonQuery()
                    End Using

                    ' Commit transaction
                    trans.Commit()
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try

        Return True
    End Function


    Public Function updateStudentRecord(stdID As String, fname As String, lname As String, gen As String, sec As String,
                                     contact As String, email As String, depID As String, crsID As String) As Boolean


        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand("UPDATE student SET first_name = @fname, last_name = @lname, gender = @gen,
                                                section_name = @sec, contact_no = @contact, email = @email, 
                                                department_id = @depID, course_id = @crsID WHERE student_id = @stdID", con)
                    con.Open()

                    cmd.Parameters.AddWithValue("@stdID", stdID)
                    cmd.Parameters.AddWithValue("@fname", fname)
                    cmd.Parameters.AddWithValue("@lname", lname)
                    cmd.Parameters.AddWithValue("@gen", gen)
                    cmd.Parameters.AddWithValue("@sec", sec)
                    cmd.Parameters.AddWithValue("@contact", contact)
                    cmd.Parameters.AddWithValue("@email", email)
                    cmd.Parameters.AddWithValue("@depID", depID)
                    cmd.Parameters.AddWithValue("@crsID", crsID)

                    cmd.ExecuteNonQuery()
                    con.Close()
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try

        Return True
    End Function
    Public Function countStudentRecord() As String
        Dim result As Integer
        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand("SELECT COUNT(student_id) 
                                               FROM student;", con)
                    con.Open()

                    result = CInt(cmd.ExecuteScalar())
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return result.ToString()
    End Function

    Public Function countPendingIntern() As String
        Dim result As Integer
        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand("SELECT COUNT(status) 
                                               FROM internship WHERE status = 'Pending' GROUP BY Status;", con)
                    con.Open()

                    result = CInt(cmd.ExecuteScalar())
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return result.ToString()
    End Function

    Public Function countOngoingIntern() As String
        Dim result As Integer
        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand("SELECT COUNT(status) 
                                               FROM internship WHERE status = 'Ongoing' GROUP BY Status;", con)
                    con.Open()

                    result = CInt(cmd.ExecuteScalar())
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return result.ToString()
    End Function

    Public Function countCompletedIntern() As String
        Dim result As Integer
        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand("SELECT COUNT(status) 
                                               FROM internship WHERE status = 'Completed' GROUP BY Status;", con)
                    con.Open()

                    result = CInt(cmd.ExecuteScalar())
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return result.ToString()
    End Function

    Public Function countIntern() As String
        Dim result As Integer
        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand("SELECT COUNT(*) 
                                               FROM internship;", con)
                    con.Open()

                    result = CInt(cmd.ExecuteScalar())
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return result.ToString()
    End Function



    Public Function searchInterTable(searchItem As String) As DataTable
        Dim dataTable As New DataTable()

        Dim query As String = " SELECT i.internship_id AS 'Internship ID',
                                s.first_name AS 'First Name',
                                s.last_name AS 'Last Name',
                                c.company_name AS 'Company Name',
                                Cc.contact_last_name AS 'Supervisor Last Name',
                                Cc.contact_contact_no AS 'Supervisor Contact',
                                i.start_date AS 'Start_Date',
                                i.end_date AS 'End_Date',
                                i.status AS 'Status'
                                FROM internship i
                                LEFT JOIN student s  ON i.student_id = s.student_id
                                LEFT JOIN company c ON i.company_id = c.company_id
                                LEFT JOIN company_contact Cc ON i.contact_id = Cc.contact_id
                                WHERE LOWER(s.first_name) LIKE LOWER(@search) 
                                OR LOWER(s.last_name) LIKE LOWER(@search)
                                OR LOWER(i.internship_id) LIKE LOWER(@search)
                                "

        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand(query, con)
                    con.Open()
                    cmd.Parameters.AddWithValue("@search", "%" & searchItem.ToLower() & "%")

                    Using dA As New MySqlDataAdapter(cmd)
                        dA.Fill(dataTable)
                    End Using
                    con.Close()
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return dataTable


    End Function





    'CODE FOR UPDATING INTERNSHIP RECORD - IRIS 


    Public Function updateInternshipRecord(internshipID As String,
                                       companyName As String,
                                       contactName As String,
                                       startDate As Date?,
                                       endDate As Date?,
                                       status As String) As Boolean

        Dim query As String =
    "UPDATE internship i
     JOIN company c ON i.company_id = c.company_id
     JOIN company_contact cc ON i.contact_id = cc.contact_id
     SET 
        i.start_date = @startDate,
        i.end_date = @endDate,
        i.status = @status,
        c.company_name = @compName,
        cc.contact_last_name = @contactName
     WHERE i.internship_id = @internshipID"

        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand(query, con)
                    con.Open()

                    cmd.Parameters.AddWithValue("@internshipID", internshipID)
                    cmd.Parameters.AddWithValue("@compName", companyName)
                    cmd.Parameters.AddWithValue("@contactName", contactName)
                    cmd.Parameters.AddWithValue("@startDate", If(startDate.HasValue, startDate.Value, DBNull.Value))
                    cmd.Parameters.AddWithValue("@endDate", If(endDate.HasValue, endDate.Value, DBNull.Value))
                    cmd.Parameters.AddWithValue("@status", status)

                    cmd.ExecuteNonQuery()
                End Using
            End Using

            Return True

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try

    End Function


    Public Function GenerateEvaluationID() As String
        Dim newID As String = "E001" ' default fallback
        Try
            Using con As MySqlConnection = GetConnection()
                con.Open()
                Dim cmd As New MySqlCommand("SELECT evaluation_id FROM internship_evaluation ORDER BY evaluation_id DESC LIMIT 1", con)
                Dim lastID As Object = cmd.ExecuteScalar()
                If lastID IsNot Nothing Then
                    Dim numericPart As Integer = Integer.Parse(lastID.ToString().Substring(1))
                    numericPart += 1
                    newID = "E" & numericPart.ToString("D3")  ' E001, E002...
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return newID
    End Function

    Public Function LoadEvaluationTable() As DataTable
        Dim dt As New DataTable()
        Dim query As String = "
        SELECT 
            ie.evaluation_id AS 'Evaluation ID',
            i.internship_id AS 'Internship ID',
            s.first_name AS 'First Name',
            s.last_name AS 'Last Name',
            c.company_name AS 'Company Name',
            ie.grade AS 'Grade',
            ie.evaluation_report AS 'Evaluation Report',
            ie.faculty_id AS 'Faculty ID'
        FROM internship_evaluation ie
        LEFT JOIN internship i ON ie.internship_id = i.internship_id
        LEFT JOIN student s ON i.student_id = s.student_id
        LEFT JOIN company c ON i.company_id = c.company_id
        ORDER BY ie.evaluation_id ASC
    "

        Using con As MySqlConnection = GetConnection()
            Using cmd As New MySqlCommand(query, con)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using

        Return dt

    End Function

    Public Function searchEvaluationTable(searchItem As String) As DataTable
        Dim dataTable As New DataTable()

        Dim query As String = "
        SELECT 
            ie.evaluation_id AS 'Evaluation ID',
            i.internship_id AS 'Internship ID',
            s.first_name AS 'First Name',
            s.last_name AS 'Last Name',
            c.company_name AS 'Company Name',
            ie.grade AS 'Grade',
            ie.evaluation_report AS 'Evaluation Report',
            ie.faculty_id AS 'Faculty ID'
        FROM internship_evaluation ie
        LEFT JOIN internship i ON ie.internship_id = i.internship_id
        LEFT JOIN student s ON i.student_id = s.student_id
        LEFT JOIN company c ON i.company_id = c.company_id
        WHERE LOWER(ie.evaluation_id) LIKE LOWER(@search)
           OR LOWER(i.internship_id) LIKE LOWER(@search)
           OR LOWER(ie.faculty_id) LIKE LOWER(@search)
           OR LOWER(s.first_name) LIKE LOWER(@search)
           OR LOWER(s.last_name) LIKE LOWER(@search)
    "

        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@search", "%" & searchItem.ToLower() & "%")

                    Using dA As New MySqlDataAdapter(cmd)
                        dA.Fill(dataTable)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return dataTable
    End Function

    Public Function GetEvaluationInfo(evaluationID As String) As DataTable
        Dim dataTable As New DataTable()

        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand("
                SELECT *
                FROM internship_evaluation
                WHERE evaluation_id = @id", con)

                    cmd.Parameters.AddWithValue("@id", evaluationID)
                    con.Open()

                    Using dA As New MySqlDataAdapter(cmd)
                        dA.Fill(dataTable)
                    End Using

                    con.Close()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return dataTable
    End Function


    Public Function AddEvaluationRecord(internshipID As String, report As String, status As String,
                                    Optional grade As Decimal? = Nothing,
                                    Optional facultyID As String = "",
                                    Optional evaluationID As String = "") As Boolean
        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand("
                INSERT INTO internship_evaluation 
                    (evaluation_id, internship_id, evaluation_report, evaluation_status, grade, faculty_id)
                VALUES 
                    (@evaluationID, @internshipID, @report, @status, @grade, @facultyID)
            ", con)
                    con.Open()
                    cmd.Parameters.AddWithValue("@evaluationID", evaluationID)
                    cmd.Parameters.AddWithValue("@internshipID", internshipID)
                    cmd.Parameters.AddWithValue("@report", report)
                    cmd.Parameters.AddWithValue("@status", status)
                    cmd.Parameters.AddWithValue("@facultyID", facultyID)
                    If grade.HasValue Then
                        cmd.Parameters.AddWithValue("@grade", grade.Value)
                    Else
                        cmd.Parameters.AddWithValue("@grade", DBNull.Value)
                    End If
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
        Return True
    End Function


    'IF FACULTY ID IS EXISITING
    Public Function FacultyExists(facultyID As String) As Boolean
        Dim exists As Boolean = False

        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand("
                SELECT COUNT(*) 
                FROM faculty 
                WHERE faculty_id = @id", con)

                    cmd.Parameters.AddWithValue("@id", facultyID)
                    con.Open()

                    Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                    exists = (count > 0)
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return exists
    End Function

    Public Function EditEvaluationRecord(evaluationID As String,
                                       grade As Decimal,
                                       report As String,
                                       status As String) As Boolean

        If String.IsNullOrWhiteSpace(evaluationID) Then Return False

        Try
            Using con As MySqlConnection = Connectivity.GetConnection()
                Using cmd As New MySqlCommand("
                UPDATE internship_evaluation 
                SET 
                    grade = @grade,
                    evaluation_report = @report,
                    evaluation_status = @status
                WHERE evaluation_id = @evaluationID", con)

                    cmd.Parameters.AddWithValue("@evaluationID", evaluationID.Trim())
                    cmd.Parameters.AddWithValue("@grade", grade)
                    cmd.Parameters.AddWithValue("@report", report.Trim())
                    cmd.Parameters.AddWithValue("@status", status.Trim())

                    con.Open()
                    Dim rows As Integer = cmd.ExecuteNonQuery()
                    con.Close()

                    Return rows > 0
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error editing evaluation: " & ex.Message,
                            "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try

    End Function


    'CODE DUNGO FOR FACULTY
    Public Function searchFacultyTable(searchItem As String) As DataTable
        Dim dataTable As New DataTable()

        Dim query As String = "SELECT
                                f.faculty_id AS 'Faculty ID',
                                f.faculty_first_name AS 'First Name',
                                f.faculty_last_name AS 'Last Name',
                                f.faculty_position AS 'position',
                                d.department_name AS 'Department',
                                f.faculty_contact_no AS 'Faculty Contact'
                                FROM faculty f
                                INNER JOIN department d ON f.department_id = d.department_id
                                WHERE f.faculty_first_name LIKE @search OR f.faculty_last_name LIKE @search OR
                                f.faculty_id LIKE @search;
                                "

        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand(query, con)
                    con.Open()
                    cmd.Parameters.AddWithValue("@search", "%" & searchItem & "%")
                    Using dA As New MySqlDataAdapter(cmd)
                        dA.Fill(dataTable)
                    End Using
                    con.Close()
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return dataTable
    End Function

    Public Function countFaculty() As String
        Dim result As Integer
        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand("SELECT COUNT(*) 
                                               FROM faculty;", con)
                    con.Open()

                    result = CInt(cmd.ExecuteScalar())
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return result.ToString()
    End Function

    Public Function countEvaluation() As String
        Dim result As Integer
        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand("SELECT COUNT(*) 
                                               FROM internship_evaluation;", con)
                    con.Open()

                    result = CInt(cmd.ExecuteScalar())
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return result.ToString()
    End Function

    'IRIS CODE FOR COMPANY
    Public Function LoadCompanyTable() As DataTable
        Dim dt As New DataTable()
        Dim query As String = "
     SELECT 
         company_id AS 'Company ID',
         company_name AS 'Company Name',
         company_address AS 'Company Address',
         industry_type AS 'Industry Type',
         company_contact_no AS 'Contact Number',
         company_email AS 'Email'
     FROM company
     ORDER BY company_id ASC
 "

        Using con As MySqlConnection = GetConnection()
            Using cmd As New MySqlCommand(query, con)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using

        Return dt
    End Function

    Public Function SearchCompany(searchText As String) As DataTable
        Dim dt As New DataTable()

        Dim query As String = "
     SELECT 
         c.company_id AS 'Company ID',
         c.company_name AS 'Company Name',
         c.company_address AS 'Company Address',
         c.industry_type AS 'Industry Type',
         c.company_contact_no AS 'Contact No',
         c.company_email AS 'Email'
     FROM company c
     WHERE UPPER(c.company_id) = UPPER(@idSearch)
        OR UPPER(c.company_name) LIKE CONCAT('%', UPPER(@nameSearch), '%')
 "

        Using con As MySqlConnection = GetConnection()
            Using cmd As New MySqlCommand(query, con)

                cmd.Parameters.AddWithValue("@idSearch", searchText)
                cmd.Parameters.AddWithValue("@nameSearch", searchText)

                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using

        Return dt
    End Function

    Public Function countCompany() As String
        Dim result As Integer
        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand("SELECT COUNT(*) 
                                               FROM company;", con)
                    con.Open()

                    result = CInt(cmd.ExecuteScalar())
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return result.ToString()
    End Function

    Public Function LoadCompanyContacts() As DataTable
        Dim dt As New DataTable()

        Dim query As String = "
        SELECT
            cc.contact_id AS 'Contact ID',
            cc.company_id AS 'Company ID',
            c.company_name AS 'Company Name',
            cc.contact_first_name AS 'Contact First Name',
            cc.contact_last_name AS 'Contact Last Name',
            cc.contact_position AS 'Position',
            cc.contact_contact_no AS 'Contact No',
            cc.email AS 'Email',
            s.first_name AS 'Student First Name',
            s.last_name AS 'Student Last Name',
            cc.grade_student AS 'Grade'
        FROM company_contact cc
        LEFT JOIN company c ON cc.company_id = c.company_id
        LEFT JOIN student s ON cc.student_id = s.student_id
        ORDER BY cc.contact_id ASC
    "

        Using con As MySqlConnection = GetConnection()
            Using cmd As New MySqlCommand(query, con)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using

        Return dt
    End Function

    Public Function SearchCompanyContacts(searchText As String) As DataTable
        Dim dt As New DataTable()

        Dim query As String = "
        SELECT
            cc.contact_id AS 'Contact ID',
            cc.company_id AS 'Company ID',
            c.company_name AS 'Company Name',
            cc.contact_first_name AS 'Contact First Name',
            cc.contact_last_name AS 'Contact Last Name',
            cc.contact_position AS 'Position',
            cc.contact_contact_no AS 'Contact No',
            cc.email AS 'Email',
            s.first_name AS 'Student First Name',
            s.last_name AS 'Student Last Name',
            cc.grade_student AS 'Grade'
        FROM company_contact cc
        LEFT JOIN company c ON cc.company_id = c.company_id
        LEFT JOIN student s ON cc.student_id = s.student_id
        WHERE 
            UPPER(cc.contact_id) = UPPER(@exactSearch)
            OR UPPER(c.company_name) LIKE CONCAT('%', UPPER(@likeSearch), '%')
            OR UPPER(cc.contact_first_name) LIKE CONCAT('%', UPPER(@likeSearch), '%')
            OR UPPER(cc.contact_last_name) LIKE CONCAT('%', UPPER(@likeSearch), '%')
            OR UPPER(s.first_name) LIKE CONCAT('%', UPPER(@likeSearch), '%')
            OR UPPER(s.last_name) LIKE CONCAT('%', UPPER(@likeSearch), '%')
            OR UPPER(cc.grade_student) LIKE CONCAT('%', UPPER(@likeSearch), '%')
        ORDER BY cc.contact_id ASC
    "

        Using con As MySqlConnection = GetConnection()
            Using cmd As New MySqlCommand(query, con)

                cmd.Parameters.AddWithValue("@exactSearch", searchText)
                cmd.Parameters.AddWithValue("@likeSearch", searchText)

                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using

        Return dt
    End Function
    Public Function countCompanyContact() As String
        Dim result As Integer
        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand("SELECT COUNT(*) 
                                               FROM company_contact;", con)
                    con.Open()

                    result = CInt(cmd.ExecuteScalar())
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return result.ToString()
    End Function
    Public Function LoadVisitLogs() As DataTable
        Dim dt As New DataTable()

        Dim query As String = "
        SELECT
            v.visit_id AS 'Visit ID',
            v.internship_id AS 'Internship ID',
            v.faculty_id AS 'Faculty ID',
            f.faculty_first_name AS 'Faculty First Name',
            f.faculty_last_name AS 'Faculty Last Name',
            v.visit_date AS 'Visit Date',
            IFNULL(v.remarks, 'No Remarks') AS 'Remarks',
            v.score AS 'Score'
        FROM visitlog v
        LEFT JOIN faculty f ON v.faculty_id = f.faculty_id
        ORDER BY v.visit_id ASC
    "

        Using con As MySqlConnection = GetConnection()
            Using cmd As New MySqlCommand(query, con)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using

        Return dt
    End Function

    Public Function SearchVisitLogs(searchText As String) As DataTable
        Dim dt As New DataTable()

        Dim query As String = "
        SELECT
            v.visit_id AS 'Visit ID',
            v.internship_id AS 'Internship ID',
            v.faculty_id AS 'Faculty ID',
            f.faculty_first_name AS 'Faculty First Name',
            f.faculty_last_name AS 'Faculty Last Name',
            v.visit_date AS 'Visit Date',
            IFNULL(v.remarks, 'No Remarks') AS 'Remarks',
            v.score AS 'Score'
        FROM visitlog v
        LEFT JOIN faculty f ON v.faculty_id = f.faculty_id
        WHERE 
            UPPER(v.visit_id) = UPPER(@exactSearch)
            OR UPPER(v.internship_id) LIKE CONCAT('%', UPPER(@likeSearch), '%')
            OR UPPER(f.faculty_first_name) LIKE CONCAT('%', UPPER(@likeSearch), '%')
            OR UPPER(f.faculty_last_name) LIKE CONCAT('%', UPPER(@likeSearch), '%')
            OR UPPER(v.remarks) LIKE CONCAT('%', UPPER(@likeSearch), '%')
        ORDER BY v.visit_id ASC
    "

        Using con As MySqlConnection = GetConnection()
            Using cmd As New MySqlCommand(query, con)

                cmd.Parameters.AddWithValue("@exactSearch", searchText)
                cmd.Parameters.AddWithValue("@likeSearch", searchText)

                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using

        Return dt
    End Function

    Public Function countVisitlog() As String
        Dim result As Integer
        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand("SELECT COUNT(*) 
                                               FROM visitlog;", con)
                    con.Open()

                    result = CInt(cmd.ExecuteScalar())
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return result.ToString()
    End Function


    Public Function LoadStudentInternshipSummary() As DataTable

        Dim dt As New DataTable()

        Dim query As String = "
        SELECT
            s.student_id AS 'Student ID',
            CONCAT(s.first_name, ' ', s.last_name) AS 'Student Name',
            i.status AS 'Internship Status',
            i.start_date AS 'Start Date',
            i.end_date AS 'End Date',

            c.company_name AS 'Company Name',
            CONCAT(cc.contact_first_name, ' ', cc.contact_last_name) AS 'Company Contact',

            cc.grade_student AS 'Company Contact Grade',

            CONCAT(f.faculty_first_name, ' ', f.faculty_last_name) AS 'Faculty Name',

            MAX(v.visit_date) AS 'Last Visit Date',

            FORMAT(IFNULL(AVG(v.score),0), 2) AS 'Avg Visit Score',
            FORMAT(IFNULL(AVG(ie.grade),0), 2) AS 'Avg Evaluation Grade',
            FORMAT(IFNULL(AVG(cc.grade_student),0), 2) AS 'Avg Company Grade',

            CASE 
                WHEN AVG(ie.grade) IS NULL THEN 'Incomplete'
                WHEN AVG(ie.grade) >= 75 THEN 'Passed'
                ELSE 'Failed'
            END AS 'Evaluation Status',

            FORMAT(
                ( IFNULL(AVG(v.score),0) 
                 + IFNULL(AVG(ie.grade),0) 
                 + IFNULL(AVG(cc.grade_student),0) ) / 3
            , 2) AS 'Overall Summary Score'

        FROM student s
        LEFT JOIN internship i ON s.student_id = i.student_id
        LEFT JOIN company c ON i.company_id = c.company_id
        LEFT JOIN company_contact cc ON cc.company_id = c.company_id
        LEFT JOIN visitlog v ON v.internship_id = i.internship_id
        LEFT JOIN faculty f ON v.faculty_id = f.faculty_id
        LEFT JOIN internship_evaluation ie ON ie.internship_id = i.internship_id

        GROUP BY s.student_id, i.internship_id
        ORDER BY s.student_id ASC
    "

        Using con As MySqlConnection = GetConnection()
            con.Open()
            Using cmd As New MySqlCommand(query, con)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using

        Return dt
    End Function

    'STUDENT SUMMARY 
    Public Function SearchStudentInternshipSummary(searchText As String) As DataTable
        Dim dt As New DataTable()

        Dim query As String = "
        SELECT
            s.student_id AS 'Student ID',
            CONCAT(s.first_name, ' ', s.last_name) AS 'Student Name',
            i.status AS 'Internship Status',
            i.start_date AS 'Start Date',
            i.end_date AS 'End Date',
            c.company_name AS 'Company Name',
            CONCAT(cc.contact_first_name, ' ', cc.contact_last_name) AS 'Company Contact',
            cc.grade_student AS 'Company Contact Grade',
            CONCAT(f.faculty_first_name, ' ', f.faculty_last_name) AS 'Faculty Name',
            MAX(v.visit_date) AS 'Last Visit Date',
            FORMAT(IFNULL(AVG(v.score),0), 2) AS 'Avg Visit Score',
            FORMAT(IFNULL(AVG(ie.grade),0), 2) AS 'Avg Evaluation Grade',
            FORMAT(IFNULL(AVG(cc.grade_student),0), 2) AS 'Avg Company Grade',
            CASE 
                WHEN AVG(ie.grade) IS NULL THEN 'Incomplete'
                WHEN AVG(ie.grade) >= 75 THEN 'Passed'
                ELSE 'Failed'
            END AS 'Evaluation Status',
            FORMAT(
                ( IFNULL(AVG(v.score),0) 
                 + IFNULL(AVG(ie.grade),0) 
                 + IFNULL(AVG(cc.grade_student),0) ) / 3
            , 2) AS 'Overall Summary Score'
        FROM student s
        LEFT JOIN internship i ON s.student_id = i.student_id
        LEFT JOIN company c ON i.company_id = c.company_id
        LEFT JOIN company_contact cc ON cc.company_id = c.company_id
        LEFT JOIN visitlog v ON v.internship_id = i.internship_id
        LEFT JOIN faculty f ON v.faculty_id = f.faculty_id
        LEFT JOIN internship_evaluation ie ON ie.internship_id = i.internship_id
        WHERE 
            UPPER(s.student_id) = UPPER(@exactSearch)
            OR (
                UPPER(s.first_name) LIKE CONCAT('%', UPPER(@likeSearch), '%')
                OR UPPER(s.last_name) LIKE CONCAT('%', UPPER(@likeSearch), '%')
                OR UPPER(c.company_name) LIKE CONCAT('%', UPPER(@likeSearch), '%')
                OR UPPER(cc.contact_first_name) LIKE CONCAT('%', UPPER(@likeSearch), '%')
                OR UPPER(cc.contact_last_name) LIKE CONCAT('%', UPPER(@likeSearch), '%')
                OR UPPER(f.faculty_first_name) LIKE CONCAT('%', UPPER(@likeSearch), '%')
                OR UPPER(f.faculty_last_name) LIKE CONCAT('%', UPPER(@likeSearch), '%')
            )
        GROUP BY s.student_id, i.internship_id
        ORDER BY s.student_id ASC;
    "

        Using con As MySqlConnection = GetConnection()
            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@exactSearch", searchText)
                cmd.Parameters.AddWithValue("@likeSearch", searchText)

                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using

        Return dt
    End Function


    Public Function GetCompanyByID(companyID As String) As DataTable
        Dim dt As New DataTable()

        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand("
                SELECT * FROM company 
                WHERE company_id = @companyID
            ", con)

                    cmd.Parameters.AddWithValue("@companyID", companyID)

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

    'EDIT
    Public Function GetFacultyRecord(facultyID As String) As DataTable
        Dim dt As New DataTable()

        Dim query As String = "
        SELECT 
            f.faculty_id,
            f.faculty_first_name,
            f.faculty_last_name,
            f.faculty_position,
            d.department_id,
            f.faculty_contact_no
        FROM faculty f
        INNER JOIN department d ON f.department_id = d.department_id
        WHERE f.faculty_id = @id
        LIMIT 1;
    "

        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@id", facultyID)

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

    Public Function SearchFacultyRecords(searchText As String) As DataTable
        Dim dt As New DataTable()

        Dim query As String = "
        SELECT 
            f.faculty_id AS 'Faculty ID',
            f.faculty_first_name AS 'First Name',
            f.faculty_last_name AS 'Last Name',
            f.faculty_position AS 'Position',
            d.department_id AS 'Department ID',
            d.department_name AS 'Department',
            f.faculty_contact_no AS 'Contact'
        FROM faculty f
        INNER JOIN department d ON f.department_id = d.department_id
        WHERE LOWER(f.faculty_id) LIKE LOWER(@search)
           OR LOWER(f.faculty_first_name) LIKE LOWER(@search)
           OR LOWER(f.faculty_last_name) LIKE LOWER(@search)
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

    Public Function GenerateContactID() As String
        Dim newID As String = "CC001"

        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand("SELECT contact_id FROM company_contact ORDER BY contact_id DESC LIMIT 1", con)
                    con.Open()

                    Dim lastID As Object = cmd.ExecuteScalar()

                    If lastID IsNot Nothing Then
                        Dim number As Integer = Integer.Parse(lastID.ToString().Substring(2)) + 1
                        newID = "CC" & number.ToString("000")
                    End If
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error generating Contact ID: " & ex.Message)
        End Try

        Return newID
    End Function

    Public Function AddCompanyContact(contactID As String,
                                  companyID As String,
                                  contactPosition As String,
                                  firstName As String,
                                  lastName As String,
                                  contactNumber As String,
                                  email As String) As Boolean
        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand("
                INSERT INTO company_contact
                    (contact_id, company_id, contact_position, contact_first_name,
                     contact_last_name, contact_contact_no, email)
                VALUES
                    (@id, @companyID, @pos, @fname, @lname, @cnumber, @mail)
            ", con)

                    cmd.Parameters.AddWithValue("@id", contactID)
                    cmd.Parameters.AddWithValue("@companyID", companyID)
                    cmd.Parameters.AddWithValue("@pos", contactPosition)
                    cmd.Parameters.AddWithValue("@fname", firstName)
                    cmd.Parameters.AddWithValue("@lname", lastName)
                    cmd.Parameters.AddWithValue("@cnumber", contactNumber)
                    cmd.Parameters.AddWithValue("@mail", email)

                    con.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            Return True

        Catch ex As Exception
            MessageBox.Show("Error adding company contact: " & ex.Message)
            Return False
        End Try
    End Function


    Public Function LoadAllCompanyContacts() As DataTable
        Dim dt As New DataTable()
        Try
            Using con As MySqlConnection = GetConnection()
                Using da As New MySqlDataAdapter("
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
                ORDER BY cc.contact_id ASC
            ", con)
                    da.Fill(dt)
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt
    End Function

    Public Function GenerateVisitID() As String
        Dim newID As String = "V001"

        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand("SELECT visit_id FROM visitlog ORDER BY visit_id DESC LIMIT 1", con)
                    con.Open()
                    Dim lastID As Object = cmd.ExecuteScalar()
                    If lastID IsNot Nothing Then
                        Dim number As Integer = Integer.Parse(lastID.ToString().Substring(1)) + 1
                        newID = "V" & number.ToString("000")
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error generating Visit ID: " & ex.Message)
        End Try

        Return newID


    End Function

    Public Function AddVisitLogRecord(visitID As String,
                                  internshipID As String,
                                  facultyID As String,
                                  visitDate As Date,
                                  score As Decimal,
                                  remarks As String) As Boolean
        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand("
                INSERT INTO visitlog
                    (visit_id, internship_id, faculty_id, visit_date, score, remarks)
                VALUES
                    (@visitID, @internshipID, @facultyID, @visitDate, @score, @remarks)
            ", con)
                    cmd.Parameters.AddWithValue("@visitID", visitID)
                    cmd.Parameters.AddWithValue("@internshipID", internshipID)
                    cmd.Parameters.AddWithValue("@facultyID", facultyID)
                    cmd.Parameters.AddWithValue("@visitDate", visitDate)
                    cmd.Parameters.AddWithValue("@score", Math.Round(score, 2)) ' 2 decimal places
                    cmd.Parameters.AddWithValue("@remarks", remarks)

                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()
                End Using
            End Using

            Return True
        Catch ex As Exception
            MessageBox.Show("Error adding Visit Log: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function SearchVisitLogRecords(searchText As String) As DataTable
        Dim dt As New DataTable()

        Dim query As String = "
        SELECT 
            visit_id AS 'Visit ID',
            internship_id AS 'Internship ID',
            faculty_id AS 'Faculty ID',
            visit_date AS 'Visit Date',
            FORMAT(score, 2) AS 'Score',
            remarks AS 'Remarks'
        FROM visitlog
        WHERE LOWER(visit_id) LIKE LOWER(@search)
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
    Public Function LoadVisitLogs(Optional searchText As String = "") As DataTable
        Dim dt As New DataTable()

        Try
            Using con As MySqlConnection = GetConnection()
                Dim query As String

                If String.IsNullOrWhiteSpace(searchText) Then
                    ' Load all records
                    query = "
                    SELECT 
                        visit_id AS 'Visit ID',
                        internship_id AS 'Internship ID',
                        faculty_id AS 'Faculty ID',
                        visit_date AS 'Visit Date',
                        FORMAT(score, 2) AS 'Score',
                        remarks AS 'Remarks'
                    FROM visitlog
                    ORDER BY visit_id ASC
                "
                Else
                    ' Load filtered by search text
                    query = "
                    SELECT 
                        visit_id AS 'Visit ID',
                        internship_id AS 'Internship ID',
                        faculty_id AS 'Faculty ID',
                        visit_date AS 'Visit Date',
                        FORMAT(score, 2) AS 'Score',
                        remarks AS 'Remarks'
                    FROM visitlog
                    WHERE LOWER(visit_id) LIKE LOWER(@search)
                    ORDER BY visit_id ASC
                "
                End If

                Using cmd As New MySqlCommand(query, con)
                    If Not String.IsNullOrWhiteSpace(searchText) Then
                        cmd.Parameters.AddWithValue("@search", "%" & searchText.ToLower() & "%")
                    End If

                    Using da As New MySqlDataAdapter(cmd)
                        da.Fill(dt)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return dt
    End Function







End Module
