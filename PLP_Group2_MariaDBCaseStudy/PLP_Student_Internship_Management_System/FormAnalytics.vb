Imports Newtonsoft.Json
Imports MySql.Data.MySqlClient
Public Class FormAnalytics

    Private csvLoaded As Boolean = False
    Private lastCSVPath As String = ""

    Private Async Sub FormAnalytics_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cmbAnalytics.Items.Add("Bar Chart")
        cmbAnalytics.Items.Add("Histogram")
        cmbAnalytics.Items.Add("Scatter Plot")
        cmbAnalytics.Items.Add("Pie Chart")
        cmbAnalytics.SelectedIndex = -1   ' ▶ NO DEFAULT SELECTED

        For Each cmb As ComboBox In {cmbAnalytics}
            cmb.DrawMode = DrawMode.OwnerDrawFixed
            AddHandler cmb.DrawItem, AddressOf DrawComboItem


            cmb.DrawMode = DrawMode.OwnerDrawFixed
            cmb.DropDownStyle = ComboBoxStyle.DropDownList
            AddHandler cmb.DrawItem, AddressOf DrawComboItem
        Next

        dgvImportFile.AllowUserToAddRows = True
        dgvImportFile.ReadOnly = False

        If csvLoaded Then
            Await RefreshAll()
        End If

    End Sub

    'ComboBox custom draw
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


    '===============================
    '   IMPORT CSV FROM BUTTON
    '===============================

    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Dim ofd As New OpenFileDialog
        ofd.Filter = "CSV Files (*.csv)|*.csv"

        If ofd.ShowDialog = DialogResult.OK Then
            Dim dt As New DataTable
            Dim lines = IO.File.ReadAllLines(ofd.FileName)

            ' Load CSV
            If lines.Length > 0 Then
                Dim headers = lines(0).Split(","c)
                For Each header In headers
                    dt.Columns.Add(header)
                Next
                For i = 1 To lines.Length - 1
                    dt.Rows.Add(lines(i).Split(","c))
                Next
            End If

            dgvImportFile.DataSource = dt
            csvLoaded = True
            lastCSVPath = ofd.FileName

            ' Column widths
            Dim widths As New Dictionary(Of String, Integer)
            For Each col As DataColumn In dt.Columns
                widths.Add(col.ColumnName, 150)
            Next

            ' STYLE + BIND
            DGVHelper.BindAndStyleFilesDGV(dgvImportFile, dt, widths)

            ' HEADER HEIGHT
            dgvImportFile.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing
            dgvImportFile.ColumnHeadersHeight = 50


        End If
    End Sub


    '===============================
    '   EXPORT CSV TEMP FILE
    '===============================

    Private Function ExportToCSV(dgv As DataGridView, filePath As String) As Boolean
        Try
            Dim lines As New List(Of String)()

            Dim headers = dgv.Columns.Cast(Of DataGridViewColumn)().Select(Function(c) c.HeaderText)
            lines.Add(String.Join(",", headers))

            For Each row As DataGridViewRow In dgv.Rows
                If Not row.IsNewRow Then
                    Dim cells = row.Cells.Cast(Of DataGridViewCell)().Select(Function(c) c.Value?.ToString())
                    lines.Add(String.Join(",", cells))
                End If
            Next

            IO.File.WriteAllLines(filePath, lines)
            Return True

        Catch ex As Exception
            MessageBox.Show("Error exporting CSV: " & ex.Message)
            Return False
        End Try
    End Function


    '===============================
    '   COMBO ANALYTICS SELECTION
    '===============================

    Private Async Sub cmbAnalytics_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAnalytics.SelectedIndexChanged

        If Not csvLoaded Then
            MessageBox.Show("⚠ Please import a CSV file first!", "No CSV Loaded", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbAnalytics.SelectedIndex = -1
            Exit Sub
        End If

        ' Export current DataGridView to temporary CSV
        Dim tempCSV As String = "C:\CSV\temp_data.csv"
        ExportToCSV(dgvImportFile, tempCSV)

        ' Determine plot type based on selection
        Dim plotType As String = ""
        Select Case cmbAnalytics.SelectedItem.ToString()
            Case "Bar Chart"
                lblTitleGraphs.Text = "Average Scores for Q1–Q20"
                plotType = "bar"
            Case "Histogram"
                lblTitleGraphs.Text = "Histogram of Ratings"
                plotType = "hist"
            Case "Scatter Plot"
                lblTitleGraphs.Text = "Total Earned Points vs Rating"
                plotType = "scatter"
            Case "Pie Chart"
                lblTitleGraphs.Text = "Gender Distribution"
                plotType = "pie"
        End Select

        If String.IsNullOrEmpty(plotType) Then Exit Sub

        ' Flask URL
        Dim url As String = $"http://127.0.0.1:5000/analytics?plot={plotType}"

        Try
            ' Fetch image asynchronously
            Dim img As Image = Await Task.Run(Function()
                                                  Dim req = Net.WebRequest.Create(url)
                                                  Using resp = req.GetResponse()
                                                      Using stream = resp.GetResponseStream()
                                                          Return Image.FromStream(stream)
                                                      End Using
                                                  End Using
                                              End Function)

            ' Resize image according to type
            Dim finalImg As Image = Nothing
            Select Case cmbAnalytics.SelectedItem.ToString()
                Case "Bar Chart"
                    finalImg = New Bitmap(img, 909, 537)
                    pctBoxAnalytics.Size = New Size(909, 537)
                    pctBoxAnalytics.Location = New Point(100, 422)
                Case Else
                    finalImg = New Bitmap(img, 750, 561)
                    pctBoxAnalytics.Size = New Size(750, 561)
                    pctBoxAnalytics.Location = New Point(163, 422)
            End Select

            ' Display image in PictureBox
            pctBoxAnalytics.Image = finalImg

        Catch ex As Exception
            MessageBox.Show("Error loading plot: " & ex.Message, "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub



    '===============================
    '   MACHINE LEARNING BUTTON
    '===============================

    Private Async Sub btnMachineLearning_Click(sender As Object, e As EventArgs) Handles btnMachineLearning.Click
        If Not csvLoaded Then
            MessageBox.Show("⚠ Import CSV before running Machine Learning.", "Missing CSV", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim url As String = "http://127.0.0.1:5000/ml"

        Try
            ' Load ML image
            Dim img As Image = Await Task.Run(Function()
                                                  Dim req = Net.WebRequest.Create(url)
                                                  Using resp = req.GetResponse()
                                                      Using stream = resp.GetResponseStream()
                                                          Return Image.FromStream(stream)
                                                      End Using
                                                  End Using
                                              End Function)

            Dim resized As New Bitmap(img, 780, 650)
            pctBoxMachineLearning.Size = New Size(780, 650)
            pctBoxMachineLearning.Image = resized

            ' Set ML title
            lblMachineLearning.Text = "Actual vs Predicted Ratings"

            ' Fetch MSE and R² from Flask
            Dim metricsUrl As String = "http://127.0.0.1:5000/ml_metrics"
            Using client As New Net.WebClient()
                Dim jsonText As String = client.DownloadString(metricsUrl)
                Dim metrics = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Dictionary(Of String, Double))(jsonText)
                lblScore.Text = $"Mean Squared Error: {metrics("mse"):0.00}, R² Score: {metrics("r2"):0.00}"
            End Using

        Catch ex As Exception
            MessageBox.Show("Error loading ML plot or metrics: " & ex.Message)
        End Try
        Await RefreshAll()
    End Sub


    '===============================
    '   BACK BUTTON CONFIRMATION
    '===============================

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Dim result = MessageBox.Show("Are you sure you want to go back?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Me.Close()   ' ← this destroys the form completely
        End If

    End Sub

    Private Sub LoadFilesDGV()
        ' Gumawa ng sample DataTable gamit ang column names mo
        Dim dt As New DataTable()
        'dt.Columns.Add("StudentID")
        dt.Columns.Add("Student_Last_Name")
        dt.Columns.Add("Student_First_Name")
        dt.Columns.Add("Gender")
        dt.Columns.Add("Course")
        dt.Columns.Add("Department")
        dt.Columns.Add("Section")
        dt.Columns.Add("Student Email")
        dt.Columns.Add("StudentContactNo")
        dt.Columns.Add("Q1: Attends regularly.")
        dt.Columns.Add("Q2: Starts the work promply.")
        dt.Columns.Add("Q3: Wear clothes suitable to his / her work.")
        dt.Columns.Add("Q4: Courteous and considerate.")
        dt.Columns.Add("Q5: Express his / her ideas well.")
        dt.Columns.Add("Q6: Listens attentively to trainer.")
        dt.Columns.Add("Q7: Display interest in the field of ICT.")
        dt.Columns.Add("Q8: Careful in handling tools and equipment")
        dt.Columns.Add("Q9: Works to develop a variety of skills.")
        dt.Columns.Add("Q10: Generally a potential leader")
        dt.Columns.Add("Q11: Accepts responsibility.")
        dt.Columns.Add("Q12: Volunteers for an assignment.")
        dt.Columns.Add("Q13: Makes worthwhile suggestios.")
        dt.Columns.Add("Q14: Exhibits orderly / safe work station.")
        dt.Columns.Add("Q15: Applies principle to actual work station.")
        dt.Columns.Add("Q16: Prepares report accurately.")
        dt.Columns.Add("Q17: Submits report punctually.")
        dt.Columns.Add("Q18: Works well under pressure.")
        dt.Columns.Add("Q19: Knows the function requirements and responsibilities.")
        dt.Columns.Add("Q20: Is generally open for growth & development.")
        dt.Columns.Add("Total Earned Points")
        dt.Columns.Add("Rating")

        ' Column widths - lahat 150
        Dim widths As New Dictionary(Of String, Integer)
        For Each col As DataColumn In dt.Columns
            widths.Add(col.ColumnName, 150)
        Next

        ' ⭐⭐ TAWAG SA MODULE DITO ⭐⭐
        ' DGVHelper.BindAndStyleFilesDGV(dgvImportFile, dt, widths)
    End Sub

    'DGV CODES
    ' -------------------- Convert Q1-Q20 columns to ComboBox --------------------
    Private Sub SetupQColumns()
        If dgvImportFile.DataSource Is Nothing Then Exit Sub
        For i As Integer = 1 To 20
            Dim colHeader = $"Q{i}"
            Dim found = dgvImportFile.Columns.Cast(Of DataGridViewColumn)().
                        FirstOrDefault(Function(c) c.HeaderText.StartsWith($"Q{i}"))
            If found IsNot Nothing AndAlso Not TypeOf found Is DataGridViewComboBoxColumn Then
                Dim cbo As New DataGridViewComboBoxColumn With {
                    .HeaderText = found.HeaderText,
                    .Name = found.Name,
                    .DataPropertyName = found.DataPropertyName,
                    .FlatStyle = FlatStyle.Flat
                }
                cbo.Items.AddRange({"1", "2", "3", "4", "5"})
                Dim index = found.Index
                dgvImportFile.Columns.Remove(found)
                dgvImportFile.Columns.Insert(index, cbo)
            End If
        Next
    End Sub

    ' -------------------- Add new row --------------------

    '===============================
    ' BTN ADD - PROMPT + AUTO CALC
    '===============================
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If dgvImportFile.DataSource Is Nothing Then
            MessageBox.Show("Import a CSV first.")
            Exit Sub
        End If

        Dim dt As DataTable = CType(dgvImportFile.DataSource, DataTable)
        Dim newRow As DataRow = dt.NewRow()

        ' Mandatory fields
        Dim columnsToPrompt As String() = {"Student_Last_Name", "Student_First_Name", "Gender", "Student Email"}
        For Each col In columnsToPrompt
            Dim value As String = ""
            Do
                value = InputBox($"Enter {col} (Cancel to stop):", "Add New Record")
                If value = "" Then
                    ' User pressed Cancel -> exit the Add process
                    MessageBox.Show("Add New Record cancelled.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
                If String.IsNullOrWhiteSpace(value) Then
                    MessageBox.Show($"{col} cannot be empty!", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            Loop While String.IsNullOrWhiteSpace(value)
            newRow(col) = value
        Next

        ' Contact Number - 13 characters format (0943-408-2998)
        Dim contact As String = ""
        Do
            contact = InputBox("Enter Contact Number (format: 0943-408-2998, Cancel to stop):", "Add New Record")
            If contact = "" Then
                MessageBox.Show("Add New Record cancelled.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            If Not System.Text.RegularExpressions.Regex.IsMatch(contact, "^\d{4}-\d{3}-\d{4}$") Then
                MessageBox.Show("Invalid format! Must be like 0943-408-2998.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                contact = ""
            End If
        Loop While String.IsNullOrWhiteSpace(contact)
        newRow("StudentContactNo") = contact

        ' Default fields
        newRow("Department") = "College of Computer Studies"
        newRow("Course") = "BS Information Technology"
        newRow("Section") = "BSIT - 4D"

        ' Q1-Q20 fields (1-5) and compute total
        Dim totalPoints As Integer = 0
        For q = 1 To 20
            Dim col = dgvImportFile.Columns.Cast(Of DataGridViewColumn)().FirstOrDefault(Function(c) c.HeaderText.StartsWith($"Q{q}:"))
            If col IsNot Nothing Then
                Dim value As String = ""
                Dim num As Integer = 0
                Do
                    value = InputBox($"Enter {col.HeaderText} (1-5, Cancel to stop):", "Add New Record")
                    If value = "" Then
                        MessageBox.Show("Add New Record cancelled.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                    If Not Integer.TryParse(value, num) OrElse num < 1 OrElse num > 5 Then
                        MessageBox.Show($"Invalid input! {col.HeaderText} must be a number between 1 and 5.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        value = ""
                    End If
                Loop While String.IsNullOrWhiteSpace(value)
                newRow(col.Name) = num
                totalPoints += num
            End If
        Next

        ' Auto-calculate Total Earned Points
        newRow("Total Earned Points") = totalPoints

        ' Auto-calculate Rating (max 5)
        Dim rating As Double = Math.Round((totalPoints / 100) * 5, 2)
        If rating > 5 Then rating = 5
        newRow("Rating") = rating

        ' Add row to DataTable
        dt.Rows.Add(newRow)
        dgvImportFile.ClearSelection()
        dgvImportFile.CurrentCell = dgvImportFile.Rows(dgvImportFile.Rows.Count - 2).Cells(0)

        ' Auto-save CSV
        Dim filePath As String = "C:\Users\acer\PycharmProjects\PythonProject\BSIT-2D\python_ojt.csv"
        'Dim filePath As String = "C:\T\python_ojt.csv"
        ExportToCSV(dgvImportFile, filePath)

        RefreshAll()
        MessageBox.Show("New record added successfully. Total Points & Rating calculated automatically.", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub



    '========================================
    ' COMPUTE TOTAL POINTS & RATING
    '========================================
    Private Sub ComputeTotalAndRating(row As DataGridViewRow)
        If row Is Nothing Then Exit Sub
        Dim total As Double = 0
        Dim complete As Boolean = True

        For q = 1 To 20
            Dim col = dgvImportFile.Columns.Cast(Of DataGridViewColumn)().FirstOrDefault(Function(c) c.HeaderText.StartsWith($"Q{q}:"))
            If col IsNot Nothing Then
                Dim value = row.Cells(col.Index).Value
                Dim num As Double
                If value Is Nothing OrElse Not Double.TryParse(value.ToString(), num) Then
                    complete = False
                    Exit For
                End If
                total += num
            End If
        Next

        ' Temporarily remove CellValueChanged handler
        RemoveHandler dgvImportFile.CellValueChanged, AddressOf dgvImportFile_CellValueChanged

        If complete Then
            row.Cells("Total Earned Points").Value = total
            Dim rating As Double = Math.Round((total / 100) * 5, 2)
            If rating > 5 Then rating = 5
            row.Cells("Rating").Value = rating
        Else
            row.Cells("Total Earned Points").Value = ""
            row.Cells("Rating").Value = ""
        End If

        ' Reattach handler
        AddHandler dgvImportFile.CellValueChanged, AddressOf dgvImportFile_CellValueChanged
    End Sub


    Private Sub dgvImportFile_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvImportFile.CellValueChanged
        If e.RowIndex < 0 Then Exit Sub
        Dim row = dgvImportFile.Rows(e.RowIndex)
        ComputeTotalAndRating(row)

        ' Auto-save CSV on edit
        Dim filePath As String = "C:\Users\acer\PycharmProjects\PythonProject\BSIT-2D\python_ojt.csv"
        'Dim filePath As String = "C:\T\python_ojt.csv"
        ExportToCSV(dgvImportFile, filePath)
    End Sub

    ' -------------------- Cell validation --------------------
    Private Sub dgv_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs)
        Dim colName = dgvImportFile.Columns(e.ColumnIndex).Name
        If colName = "Total Earned Points" Or colName = "Rating" Then Exit Sub

        ' Required check
        If String.IsNullOrWhiteSpace(e.FormattedValue.ToString()) Then
            MessageBox.Show($"{colName} is required!")
            e.Cancel = True
            Exit Sub
        End If

        ' Q1-Q20 only 1-5
        If colName.StartsWith("Q") Then
            Dim val As Integer
            If Not Integer.TryParse(e.FormattedValue.ToString(), val) OrElse val < 1 OrElse val > 5 Then
                MessageBox.Show($"{colName} must be 1-5 only!")
                e.Cancel = True
            End If
        End If
    End Sub

    ' -------------------- Auto compute Total & Rating --------------------
    Private Sub dgv_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs)
        ComputeTotalAndRating(e.RowIndex)
    End Sub

    Private Sub dgv_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs)
        ComputeTotalAndRating(e.RowIndex)

    End Sub

    Private Sub ComputeTotalAndRating(rowIndex As Integer)
        If rowIndex < 0 Then Exit Sub
        Dim row = dgvImportFile.Rows(rowIndex)
        Dim total As Integer = 0
        For i As Integer = 1 To 20
            Dim colName = $"Q{i}"
            Dim val = row.Cells(colName).Value
            If val Is Nothing OrElse val.ToString() = "" Then
                row.Cells("Total Earned Points").Value = ""
                row.Cells("Rating").Value = ""
                Exit Sub
            End If
            total += Convert.ToInt32(val)
        Next
        row.Cells("Total Earned Points").Value = total

        ' Rating (5 max)
        Dim rating As Double = Math.Round((total / 100) * 5, 2)
        If rating > 5 Then rating = 5
        row.Cells("Rating").Value = rating
    End Sub


    Private Async Function RefreshAll() As Task
        If Not csvLoaded Then Return

        ' Export latest data to CSV
        Dim tempCSV As String = "C:\Users\acer\PycharmProjects\PythonProject\BSIT-2D\python_ojt.csv"
        'Dim tempCSV As String = "C:\T\python_ojt.csv"
        ExportToCSV(dgvImportFile, tempCSV)

        ' ------------------ Refresh Analytics ------------------
        Dim plotTypes As Dictionary(Of String, String) = New Dictionary(Of String, String) From {
        {"Bar Chart", "bar"},
        {"Histogram", "hist"},
        {"Scatter Plot", "scatter"},
        {"Pie Chart", "pie"}
    }

        ' Kung may selected item sa ComboBox, gamitin ito
        If cmbAnalytics.SelectedItem IsNot Nothing AndAlso plotTypes.ContainsKey(cmbAnalytics.SelectedItem.ToString()) Then
            Await LoadAnalytics(plotTypes(cmbAnalytics.SelectedItem.ToString()))
        End If

        ' ------------------ Refresh ML Plot ------------------
        Await LoadMLPlot()
    End Function



    Private Async Function LoadAnalytics(plotType As String) As Task
        Dim url As String = $"http://127.0.0.1:5000/analytics?plot={plotType}"

        Try
            Dim img As Image = Await Task.Run(Function()
                                                  Dim req = Net.WebRequest.Create(url)
                                                  Using resp = req.GetResponse()
                                                      Using stream = resp.GetResponseStream()
                                                          Return Image.FromStream(stream)
                                                      End Using
                                                  End Using
                                              End Function)

            ' Resize depending on type
            Dim finalImg As Image = Nothing
            If plotType = "bar" Then
                finalImg = New Bitmap(img, 909, 537)
                pctBoxAnalytics.Size = New Size(909, 537)
                pctBoxAnalytics.Location = New Point(100, 422)
            Else
                finalImg = New Bitmap(img, 750, 561)
                pctBoxAnalytics.Size = New Size(750, 561)
                pctBoxAnalytics.Location = New Point(163, 422)
            End If

            pctBoxAnalytics.Image = finalImg

        Catch ex As Exception
            MessageBox.Show("Error refreshing Analytics: " & ex.Message)
        End Try
    End Function


    Private Async Function LoadMLPlot() As Task
        Dim url As String = "http://127.0.0.1:5000/ml"

        Try
            ' Load ML image
            Dim img As Image = Await Task.Run(Function()
                                                  Dim req = Net.WebRequest.Create(url)
                                                  Using resp = req.GetResponse()
                                                      Using stream = resp.GetResponseStream()
                                                          Return Image.FromStream(stream)
                                                      End Using
                                                  End Using
                                              End Function)

            Dim resized As New Bitmap(img, 780, 650)
            pctBoxMachineLearning.Size = New Size(780, 650)
            pctBoxMachineLearning.Image = resized

            ' Set ML title
            lblMachineLearning.Text = "Actual vs Predicted Ratings"

            ' Fetch MSE and R² metrics
            Dim metricsUrl As String = "http://127.0.0.1:5000/ml_metrics"
            Using client As New Net.WebClient()
                Dim jsonText As String = client.DownloadString(metricsUrl)
                Dim metrics = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Dictionary(Of String, Double))(jsonText)
                lblScore.Text = $"Mean Squared Error: {metrics("mse"):0.00}, R² Score: {metrics("r2"):0.00}"
            End Using

        Catch ex As Exception
            MessageBox.Show("Error refreshing ML Plot: " & ex.Message)
        End Try
    End Function


    Private Async Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        If csvLoaded Then
            Await RefreshAll()
        End If
    End Sub


    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If dgvImportFile.DataSource Is Nothing Then
            MessageBox.Show("No data to save!")
            Exit Sub
        End If

        Dim dt As DataTable = CType(dgvImportFile.DataSource, DataTable)
        'Dim conn As New MySqlConnection("server=127.0.0.1;port=3306;user=root;password=;database=ojtdb_plp")
        Dim conn As New MySqlConnection("server=localhost;user=root;password=;database=ojtdb_plp")
        conn.Open()

        Dim newRecordsCount As Integer = 0
        Dim duplicateCount As Integer = 0

        For Each row As DataGridViewRow In dgvImportFile.Rows
            If row.IsNewRow Then Continue For

            Dim email As String = row.Cells(6).Value.ToString()

            ' Check if email already exists
            Dim checkCmd As New MySqlCommand("SELECT COUNT(*) FROM analytics WHERE StudentEmail = @Email", conn)
            checkCmd.Parameters.AddWithValue("@Email", email)
            Dim count As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())

            If count = 0 Then
                ' Insert new record
                Dim cmd As New MySqlCommand("
                INSERT INTO analytics (
                    Student_Last_Name, Student_First_Name, Gender, Course, Department, Section, 
                    StudentEmail, StudentContactNo,
                    Q1, Q2, Q3, Q4, Q5, Q6, Q7, Q8, Q9, Q10,
                    Q11, Q12, Q13, Q14, Q15, Q16, Q17, Q18, Q19, Q20,
                    Total_Earned_Points, Equivalent_Rating
                ) VALUES (
                    @ln, @fn, @gender, @course, @dept, @section,
                    @email, @contact,
                    @q1, @q2, @q3, @q4, @q5, @q6, @q7, @q8, @q9, @q10,
                    @q11, @q12, @q13, @q14, @q15, @q16, @q17, @q18, @q19, @q20,
                    @points, @rating
                )", conn)

                cmd.Parameters.AddWithValue("@ln", row.Cells(0).Value)
                cmd.Parameters.AddWithValue("@fn", row.Cells(1).Value)
                cmd.Parameters.AddWithValue("@gender", row.Cells(2).Value)
                cmd.Parameters.AddWithValue("@course", row.Cells(3).Value)
                cmd.Parameters.AddWithValue("@dept", row.Cells(4).Value)
                cmd.Parameters.AddWithValue("@section", row.Cells(5).Value)
                cmd.Parameters.AddWithValue("@email", email)
                cmd.Parameters.AddWithValue("@contact", row.Cells(7).Value)

                ' Questions Q1–Q20
                For i As Integer = 1 To 20
                    cmd.Parameters.AddWithValue("@q" & i, row.Cells(7 + i).Value)
                Next

                cmd.Parameters.AddWithValue("@points", row.Cells(28).Value)
                cmd.Parameters.AddWithValue("@rating", row.Cells(29).Value)

                cmd.ExecuteNonQuery()
                newRecordsCount += 1
            Else
                ' Already exists in DB, count as duplicate
                duplicateCount += 1
            End If
        Next

        conn.Close()

        ' Show summary message
        Dim message As String = ""
        If newRecordsCount > 0 Then
            message &= newRecordsCount & " new record(s) added successfully!" & vbCrLf
        End If
        If duplicateCount > 0 Then
            message &= duplicateCount & " duplicate record(s) skipped."
        End If
        If message = "" Then message = "No records to add."

        MessageBox.Show(message)
    End Sub


    Private Sub SaveNewRecordsToDatabase()
        If dgvImportFile.DataSource Is Nothing Then
            MessageBox.Show("No data to save!")
            Exit Sub
        End If

        Dim dt As DataTable = CType(dgvImportFile.DataSource, DataTable)

        ' Mapping from DataGridView column names → Database column names
        Dim dgvToDbCols As New Dictionary(Of String, String) From {
            {"Q1: Attends regularly.", "Q1"},
            {"Q2: Starts the work promply.", "Q2"},
            {"Q3: Wear clothes suitable to his / her work.", "Q3"},
            {"Q4: Courteous and considerate.", "Q4"},
            {"Q5: Express his / her ideas well.", "Q5"},
            {"Q6: Listens attentively to trainer.", "Q6"},
            {"Q7: Display interest in the field of ICT.", "Q7"},
            {"Q8: Careful in handling tools and equipment", "Q8"},
            {"Q9: Works to develop a variety of skills.", "Q9"},
            {"Q10: Generally a potential leader", "Q10"},
            {"Q11: Accepts responsibility.", "Q11"},
            {"Q12: Volunteers for an assignment.", "Q12"},
            {"Q13: Makes worthwhile suggestios.", "Q13"},
            {"Q14: Exhibits orderly / safe work station.", "Q14"},
            {"Q15: Applies principle to actual work station.", "Q15"},
            {"Q16: Prepares report accurately.", "Q16"},
            {"Q17: Submits report punctually.", "Q17"},
            {"Q18: Works well under pressure.", "Q18"},
            {"Q19: Knows the function requirements and responsibilities.", "Q19"},
            {"Q20: Is generally open for growth & development.", "Q20"}
        }

        Try
            Using conn As MySqlConnection = Connectivity.GetConnection()
                conn.Open()

                For Each row As DataRow In dt.Rows

                    Dim email As String = row("Student Email").ToString()

                    Dim cmdCheck As New MySqlCommand("SELECT COUNT(*) FROM analytics WHERE student_email=@em", conn)
                    cmdCheck.Parameters.AddWithValue("@em", email)

                    Dim exists As Integer = Convert.ToInt32(cmdCheck.ExecuteScalar())
                    If exists > 0 Then Continue For  ' Skip duplicates

                    Dim query As String =
                        "INSERT INTO analytics 
                    (student_last_name, student_first_name, gender, course, department, section, student_email, student_contact_no,
                     q1,q2,q3,q4,q5,q6,q7,q8,q9,q10,q11,q12,q13,q14,q15,q16,q17,q18,q19,q20,total_earned_points,rating)
                     VALUES
                    (@ln,@fn,@gender,@course,@dept,@sec,@em,@contact,
                     @q1,@q2,@q3,@q4,@q5,@q6,@q7,@q8,@q9,@q10,@q11,@q12,@q13,@q14,@q15,@q16,@q17,@q18,@q19,@q20,@total,@rating)"

                    Dim cmd As New MySqlCommand(query, conn)

                    ' BASIC FIELDS
                    cmd.Parameters.AddWithValue("@ln", row("Student_Last_Name"))
                    cmd.Parameters.AddWithValue("@fn", row("Student_First_Name"))
                    cmd.Parameters.AddWithValue("@gender", row("Gender"))
                    cmd.Parameters.AddWithValue("@course", row("Course"))
                    cmd.Parameters.AddWithValue("@dept", row("Department"))
                    cmd.Parameters.AddWithValue("@sec", row("Section"))
                    cmd.Parameters.AddWithValue("@em", row("Student Email"))
                    cmd.Parameters.AddWithValue("@contact", row("StudentContactNo"))

                    ' Q1–Q20 LOOP USING MAPPING
                    For Each kvp In dgvToDbCols
                        ' kvp.Key = DGV column name
                        ' kvp.Value = database column name (q1, q2, …)
                        cmd.Parameters.AddWithValue("@" & kvp.Value, row(kvp.Key))
                    Next

                    ' TOTAL + RATING
                    cmd.Parameters.AddWithValue("@total", row("Total Earned Points"))
                    cmd.Parameters.AddWithValue("@rating", row("Rating"))

                    cmd.ExecuteNonQuery()
                Next

                MessageBox.Show("New records saved successfully!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Using

        Catch ex As Exception
            MessageBox.Show("Error saving to database: " & ex.Message)
        End Try
    End Sub



End Class