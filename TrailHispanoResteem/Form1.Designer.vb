<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.ListBox2 = New System.Windows.Forms.ListBox()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.ListBox3 = New System.Windows.Forms.ListBox()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(173, 435)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(155, 46)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "Refresh"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'ListBox2
        '
        Me.ListBox2.FormattingEnabled = True
        Me.ListBox2.Location = New System.Drawing.Point(12, 12)
        Me.ListBox2.Name = "ListBox2"
        Me.ListBox2.Size = New System.Drawing.Size(319, 368)
        Me.ListBox2.TabIndex = 4
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(337, 434)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(319, 46)
        Me.Button4.TabIndex = 5
        Me.Button4.Text = "Remove"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(173, 382)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(155, 46)
        Me.Button5.TabIndex = 6
        Me.Button5.Text = "Don't Vote"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(12, 382)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(155, 46)
        Me.Button6.TabIndex = 7
        Me.Button6.Text = "Add To List"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'ListBox3
        '
        Me.ListBox3.FormattingEnabled = True
        Me.ListBox3.Location = New System.Drawing.Point(337, 12)
        Me.ListBox3.Name = "ListBox3"
        Me.ListBox3.Size = New System.Drawing.Size(319, 368)
        Me.ListBox3.TabIndex = 8
        '
        'Button7
        '
        Me.Button7.Location = New System.Drawing.Point(337, 382)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(319, 46)
        Me.Button7.TabIndex = 9
        Me.Button7.Text = "Batch Vote!"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'Button8
        '
        Me.Button8.Location = New System.Drawing.Point(12, 434)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(155, 46)
        Me.Button8.TabIndex = 10
        Me.Button8.Text = "Batch Open"
        Me.Button8.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(679, 493)
        Me.Controls.Add(Me.Button8)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.ListBox3)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.ListBox2)
        Me.Controls.Add(Me.Button2)
        Me.Name = "Form1"
        Me.Text = "TrailHispano Resteem"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Button2 As Button
    Friend WithEvents ListBox2 As ListBox
    Friend WithEvents Button4 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents ListBox3 As ListBox
    Friend WithEvents Button7 As Button
    Friend WithEvents Button8 As Button
End Class
