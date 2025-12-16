Public Class DGVHelper
    ' Para sa tracking ng hover row per DGV
    Private Shared hoverRows As New Dictionary(Of DataGridView, Integer)
    Public Shared Sub BindAndStyleFilesDGV(dgv As DataGridView, dt As DataTable, Optional columnWidths As Dictionary(Of String, Integer) = Nothing)
        ' Bind DataTable
        dgv.DataSource = dt

        ' Column widths
        If columnWidths IsNot Nothing Then
            For Each colName In columnWidths.Keys
                If dgv.Columns.Contains(colName) Then
                    dgv.Columns(colName).Width = columnWidths(colName)
                End If
            Next
        End If

        ' General settings
        dgv.AllowUserToAddRows = False
        dgv.AllowUserToDeleteRows = False
        dgv.BackgroundColor = Color.MintCream
        dgv.BorderStyle = BorderStyle.None
        dgv.EnableHeadersVisualStyles = False
        dgv.GridColor = Color.MintCream
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv.MultiSelect = False
        dgv.ReadOnly = True
        dgv.AllowUserToAddRows = False

        ' Column Headers
        With dgv.ColumnHeadersDefaultCellStyle
            .BackColor = Color.FromArgb(97, 144, 118)
            .ForeColor = Color.White
            .Font = New Font("Microsoft New Tai Lue", 15, FontStyle.Bold)
            .SelectionBackColor = Color.FromArgb(97, 144, 118)
            .SelectionForeColor = SystemColors.Info
            .Alignment = DataGridViewContentAlignment.MiddleCenter
        End With
        dgv.ColumnHeadersHeight = 80

        ' Default Cell Style
        With dgv.DefaultCellStyle
            .BackColor = Color.MintCream
            .ForeColor = Color.FromArgb(0, 64, 0)
            .Font = New Font("Microsoft New Tai Lue", 12, FontStyle.Bold)
            .SelectionBackColor = Color.LightYellow
            .SelectionForeColor = Color.FromArgb(0, 64, 0)
        End With

        ' Row Headers
        With dgv.RowHeadersDefaultCellStyle
            .BackColor = Color.MintCream
            .ForeColor = Color.FromArgb(0, 64, 0)
            .Font = New Font("Microsoft New Tai Lue", 12, FontStyle.Bold)
            .SelectionBackColor = Color.LightYellow
            .SelectionForeColor = Color.FromArgb(0, 64, 0)
        End With
        dgv.RowHeadersWidth = 20

        ' Clear selection
        dgv.ClearSelection()
        dgv.CurrentCell = Nothing

        ' Initialize hover tracking
        If Not hoverRows.ContainsKey(dgv) Then
            hoverRows.Add(dgv, -1)
            AddHandler dgv.CellMouseEnter, AddressOf CellMouseEnter
            AddHandler dgv.CellMouseLeave, AddressOf CellMouseLeave
        End If
    End Sub

    ' Hover events (reuse existing)
    Private Shared Sub CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs)
        Dim dgv As DataGridView = DirectCast(sender, DataGridView)
        If e.RowIndex >= 0 Then
            Dim lastRow As Integer = hoverRows(dgv)
            If lastRow >= 0 AndAlso lastRow <> e.RowIndex Then
                dgv.Rows(lastRow).DefaultCellStyle.BackColor = Color.MintCream
            End If
            dgv.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.Honeydew
            hoverRows(dgv) = e.RowIndex
        End If
    End Sub

    Private Shared Sub CellMouseLeave(sender As Object, e As DataGridViewCellEventArgs)
        Dim dgv As DataGridView = DirectCast(sender, DataGridView)
        Dim lastRow As Integer = hoverRows(dgv)
        If lastRow >= 0 Then
            dgv.Rows(lastRow).DefaultCellStyle.BackColor = Color.MintCream
            hoverRows(dgv) = -1
        End If
    End Sub



    Public Shared Sub HideSelectedRows(dgv As DataGridView)
        If dgv.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select row(s) to hide.", "DGV", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        For Each row As DataGridViewRow In dgv.SelectedRows
            row.Visible = False
        Next
    End Sub

    Public Shared Sub ShowAllRows(dgv As DataGridView)
        For Each row As DataGridViewRow In dgv.Rows
            row.Visible = True
        Next
    End Sub


End Class
