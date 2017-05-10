Imports System.Threading

Public Class Form1

        Dim F2 As New Form2 'Create New Form2 Object
        Dim strText As String = "Thread Is Running!" 'Text To Display
        Dim lbList As New ListBox 'Create New ListBox, Used By Form 2

        Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            'Create Thread, and Specify Delegate
            Dim tThread1 As New Thread(AddressOf ThreadProcedure)
            'Start A Thread
            tThread1.Start()
            'Show Form 2
            F2.Show()
            F2.TopMost = True
        End Sub

        'What Thread 1 Must Do
        Private Sub ThreadProcedure()
            'Specify Properties For ListBox
            lbList.Location = New Point(0, 0)
            lbList.Width = 300
            lbList.Height = 300
            lbList.Items.Add(strText) 'Add Text
            'Call Delegate To Add ListBox To A Different Thread
            AddControlToForm(lbList)
        End Sub

        'Delegate
        Private Delegate Sub AddListBox(ByVal ctrToAdd As Control)

        Private Sub AddControlToForm(ByVal ctrl As Control)
            'InvokeRequired Informs Us If We Are In Correct Thread
            If Me.InvokeRequired Then 'Not
                'Create Another Delegate
                Dim TempDelegate As New AddListBox(AddressOf AddControlToForm)
                'Store Parameters - OPTIONAL
                Dim parameters(0) As Object
                parameters(0) = ctrl
                'Invoke TempDelegate
                Me.Invoke(TempDelegate, parameters)
            Else 'Yes
                F2.Controls.Add(ctrl)
            End If
        End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ThreadProcedure() 'Run Separate Thread
    End Sub
End Class
