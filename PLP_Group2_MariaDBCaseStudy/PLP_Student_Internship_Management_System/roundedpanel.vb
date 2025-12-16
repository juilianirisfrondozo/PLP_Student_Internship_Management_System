Public Class roundedpanel
    Inherits Panel

    Private _cornerRadius As Integer = 10 ' Default corner radius

    Public Property CornerRadius As Integer
        Get
            Return _cornerRadius
        End Get
        Set(value As Integer)
            _cornerRadius = value
            Me.Invalidate() ' Redraw the panel when the radius changes
        End Set
    End Property

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        ' Create a GraphicsPath for the rounded rectangle
        Using path As New Drawing2D.GraphicsPath()
            Dim rect As Rectangle = Me.ClientRectangle
            Dim diameter As Integer = _cornerRadius * 2

            ' Adjust rectangle for drawing the arcs
            rect.Width -= 1 ' To avoid clipping issues with the border
            rect.Height -= 1

            ' Top-left corner
            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90)
            ' Top-right corner
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90)
            ' Bottom-right corner
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90)
            ' Bottom-left corner
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90)
            path.CloseFigure()

            ' Set the Region of the panel to the rounded rectangle
            Me.Region = New Region(path)

            ' Optionally, draw a border
            'Using pen As New Pen(Me.ForeColor, 1) ' Use ForeColor for border color
            'e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            'e.Graphics.DrawPath(pen, path)
            'End Using
        End Using
    End Sub
End Class