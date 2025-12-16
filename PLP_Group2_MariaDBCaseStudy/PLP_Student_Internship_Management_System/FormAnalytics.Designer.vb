<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormAnalytics
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormAnalytics))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        btnImport = New Button()
        btnBack = New Button()
        pctBoxAnalytics = New PictureBox()
        pctBoxMachineLearning = New PictureBox()
        cmbAnalytics = New ComboBox()
        lblTitleGraphs = New Label()
        lblMachineLearning = New Label()
        Label1 = New Label()
        dgvImportFile = New DataGridView()
        lblScore = New Label()
        btnAdd = New Button()
        btnRefresh = New Button()
        Label2 = New Label()
        btnMachineLearning = New RoundedButton()
        btnSave = New RoundedButton()
        CType(pctBoxAnalytics, ComponentModel.ISupportInitialize).BeginInit()
        CType(pctBoxMachineLearning, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvImportFile, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' btnImport
        ' 
        btnImport.BackgroundImage = CType(resources.GetObject("btnImport.BackgroundImage"), Image)
        btnImport.FlatStyle = FlatStyle.Flat
        btnImport.ForeColor = Color.White
        btnImport.Location = New Point(1485, 18)
        btnImport.Name = "btnImport"
        btnImport.Size = New Size(86, 70)
        btnImport.TabIndex = 0
        btnImport.UseVisualStyleBackColor = True
        ' 
        ' btnBack
        ' 
        btnBack.BackgroundImage = CType(resources.GetObject("btnBack.BackgroundImage"), Image)
        btnBack.FlatStyle = FlatStyle.Flat
        btnBack.ForeColor = Color.White
        btnBack.Location = New Point(1760, 18)
        btnBack.Name = "btnBack"
        btnBack.Size = New Size(86, 70)
        btnBack.TabIndex = 6
        btnBack.UseVisualStyleBackColor = True
        ' 
        ' pctBoxAnalytics
        ' 
        pctBoxAnalytics.Location = New Point(163, 422)
        pctBoxAnalytics.Name = "pctBoxAnalytics"
        pctBoxAnalytics.Size = New Size(750, 561)
        pctBoxAnalytics.TabIndex = 276
        pctBoxAnalytics.TabStop = False
        ' 
        ' pctBoxMachineLearning
        ' 
        pctBoxMachineLearning.Location = New Point(1081, 361)
        pctBoxMachineLearning.Name = "pctBoxMachineLearning"
        pctBoxMachineLearning.Size = New Size(743, 651)
        pctBoxMachineLearning.TabIndex = 277
        pctBoxMachineLearning.TabStop = False
        ' 
        ' cmbAnalytics
        ' 
        cmbAnalytics.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        cmbAnalytics.FormattingEnabled = True
        cmbAnalytics.Location = New Point(441, 346)
        cmbAnalytics.Name = "cmbAnalytics"
        cmbAnalytics.Size = New Size(366, 36)
        cmbAnalytics.TabIndex = 278
        ' 
        ' lblTitleGraphs
        ' 
        lblTitleGraphs.AutoSize = True
        lblTitleGraphs.BackColor = Color.Transparent
        lblTitleGraphs.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblTitleGraphs.ForeColor = Color.FromArgb(CByte(8), CByte(48), CByte(25))
        lblTitleGraphs.Location = New Point(100, 1008)
        lblTitleGraphs.Name = "lblTitleGraphs"
        lblTitleGraphs.Size = New Size(32, 31)
        lblTitleGraphs.TabIndex = 279
        lblTitleGraphs.Text = "--"
        ' 
        ' lblMachineLearning
        ' 
        lblMachineLearning.AutoSize = True
        lblMachineLearning.BackColor = Color.Transparent
        lblMachineLearning.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblMachineLearning.ForeColor = Color.FromArgb(CByte(8), CByte(48), CByte(25))
        lblMachineLearning.Location = New Point(1081, 1028)
        lblMachineLearning.Name = "lblMachineLearning"
        lblMachineLearning.Size = New Size(32, 31)
        lblMachineLearning.TabIndex = 280
        lblMachineLearning.Text = "--"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.BackColor = Color.Transparent
        Label1.Font = New Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.FromArgb(CByte(8), CByte(48), CByte(25))
        Label1.Location = New Point(99, 346)
        Label1.Name = "Label1"
        Label1.Size = New Size(267, 38)
        Label1.TabIndex = 281
        Label1.Text = "Data Visualizations"
        ' 
        ' dgvImportFile
        ' 
        dgvImportFile.BackgroundColor = Color.MintCream
        dgvImportFile.BorderStyle = BorderStyle.None
        dgvImportFile.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvImportFile.Location = New Point(72, 109)
        dgvImportFile.Name = "dgvImportFile"
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = Color.MintCream
        DataGridViewCellStyle1.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle1.ForeColor = SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = DataGridViewTriState.True
        dgvImportFile.RowHeadersDefaultCellStyle = DataGridViewCellStyle1
        dgvImportFile.RowHeadersWidth = 20
        dgvImportFile.Size = New Size(1776, 202)
        dgvImportFile.TabIndex = 282
        ' 
        ' lblScore
        ' 
        lblScore.AutoSize = True
        lblScore.BackColor = Color.Transparent
        lblScore.Font = New Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblScore.ForeColor = Color.FromArgb(CByte(8), CByte(48), CByte(25))
        lblScore.Location = New Point(1516, 1034)
        lblScore.Name = "lblScore"
        lblScore.Size = New Size(24, 23)
        lblScore.TabIndex = 283
        lblScore.Text = "--"
        ' 
        ' btnAdd
        ' 
        btnAdd.BackgroundImage = CType(resources.GetObject("btnAdd.BackgroundImage"), Image)
        btnAdd.FlatStyle = FlatStyle.Flat
        btnAdd.ForeColor = Color.White
        btnAdd.Location = New Point(1669, 18)
        btnAdd.Name = "btnAdd"
        btnAdd.Size = New Size(86, 70)
        btnAdd.TabIndex = 284
        btnAdd.UseVisualStyleBackColor = True
        ' 
        ' btnRefresh
        ' 
        btnRefresh.BackgroundImage = CType(resources.GetObject("btnRefresh.BackgroundImage"), Image)
        btnRefresh.FlatStyle = FlatStyle.Flat
        btnRefresh.ForeColor = Color.White
        btnRefresh.Location = New Point(1577, 18)
        btnRefresh.Name = "btnRefresh"
        btnRefresh.Size = New Size(86, 70)
        btnRefresh.TabIndex = 288
        btnRefresh.UseVisualStyleBackColor = True
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.BackColor = Color.Transparent
        Label2.Font = New Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.ForeColor = Color.FromArgb(CByte(8), CByte(48), CByte(25))
        Label2.Location = New Point(69, 28)
        Label2.Name = "Label2"
        Label2.Size = New Size(606, 54)
        Label2.TabIndex = 289
        Label2.Text = "Student Performance Analytics"
        ' 
        ' btnMachineLearning
        ' 
        btnMachineLearning.BackColor = Color.FromArgb(CByte(8), CByte(48), CByte(25))
        btnMachineLearning.FlatAppearance.BorderSize = 0
        btnMachineLearning.FlatStyle = FlatStyle.Flat
        btnMachineLearning.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnMachineLearning.ForeColor = Color.White
        btnMachineLearning.Location = New Point(1226, 24)
        btnMachineLearning.MinimumSize = New Size(50, 25)
        btnMachineLearning.Name = "btnMachineLearning"
        btnMachineLearning.Size = New Size(236, 64)
        btnMachineLearning.TabIndex = 290
        btnMachineLearning.Text = "Machine Learning"
        btnMachineLearning.UseVisualStyleBackColor = False
        ' 
        ' btnSave
        ' 
        btnSave.BackColor = Color.FromArgb(CByte(8), CByte(48), CByte(25))
        btnSave.FlatAppearance.BorderSize = 0
        btnSave.FlatStyle = FlatStyle.Flat
        btnSave.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnSave.ForeColor = Color.White
        btnSave.Location = New Point(972, 24)
        btnSave.MinimumSize = New Size(50, 25)
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(236, 64)
        btnSave.TabIndex = 291
        btnSave.Text = "Save to Database"
        btnSave.UseVisualStyleBackColor = False
        ' 
        ' FormAnalytics
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(1920, 1080)
        Controls.Add(btnSave)
        Controls.Add(btnMachineLearning)
        Controls.Add(Label2)
        Controls.Add(btnRefresh)
        Controls.Add(btnAdd)
        Controls.Add(lblScore)
        Controls.Add(dgvImportFile)
        Controls.Add(Label1)
        Controls.Add(lblMachineLearning)
        Controls.Add(lblTitleGraphs)
        Controls.Add(cmbAnalytics)
        Controls.Add(pctBoxMachineLearning)
        Controls.Add(pctBoxAnalytics)
        Controls.Add(btnBack)
        Controls.Add(btnImport)
        FormBorderStyle = FormBorderStyle.None
        Name = "FormAnalytics"
        StartPosition = FormStartPosition.CenterScreen
        Text = "FormAnalytics"
        CType(pctBoxAnalytics, ComponentModel.ISupportInitialize).EndInit()
        CType(pctBoxMachineLearning, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvImportFile, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents btnImport As Button
    Friend WithEvents btnBack As Button
    Friend WithEvents pctBoxAnalytics As PictureBox
    Friend WithEvents pctBoxMachineLearning As PictureBox
    Friend WithEvents cmbAnalytics As ComboBox
    Friend WithEvents lblTitleGraphs As Label
    Friend WithEvents lblMachineLearning As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents dgvImportFile As DataGridView
    Friend WithEvents lblScore As Label
    Friend WithEvents btnAdd As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents btnMachineLearning As RoundedButton
    Friend WithEvents btnSave As RoundedButton
End Class
