Imports SpiceMaster.Core

Public Class frmDocumentChanges

#Region "Document Changes"
    Dim m_DocChanges As Long = 0, m_DocChangesPrev As Long = 0

    Friend Sub ElementChange(Optional evnt As String = "<None passed>", Optional caller As Object = Nothing)
        Return

        Dim s As String

        m_DocChanges += 1
        lbChanges.Text = "Document Changes: " & m_DocChanges & " (+" & (m_DocChanges - m_DocChangesPrev) & ")"
        If cbShowEvents.Checked Then
            If caller Is Nothing Then
                s = "!"
            Else
                s = caller.ToString & ": "
            End If
            If evnt Is Nothing Then
                s &= "<None passed>"
            Else
                s &= evnt
            End If
            lbEvents.Items.Add(s)
        End If
    End Sub

    Public Sub DocumentChange()
        Return

        lbChanges.Text = "Document Changes: " & m_DocChanges & " (+" & (m_DocChanges - m_DocChangesPrev) & ")"
        m_DocChangesPrev = m_DocChanges
    End Sub
#End Region

    Private Sub tsDC_Click() Handles lbChanges.Click
        DocumentChange()
    End Sub

    Private Sub lbEvents_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles lbEvents.SelectedIndexChanged
        If lbEvents.SelectedIndex < 0 Then
            tbEvent.Text = ""
        Else
            tbEvent.Text = lbEvents.SelectedItem
        End If
    End Sub

    Private Sub butClear_Click(sender As Object, e As System.EventArgs) Handles butClear.Click
        lbEvents.Items.Clear()
        lbEvents_SelectedIndexChanged(Nothing, Nothing)
        tsDC_Click()
    End Sub
End Class