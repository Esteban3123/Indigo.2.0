Imports System.IO

Public Class FrmDashboardDesigner

    Public Sub New()
        InitializeComponent()
        AddHandler CtrDashBoardDesign1.INDDashboardDesigner.DashboardSaving, AddressOf INDDashboardDesigner_DashboardSaving
    End Sub

    Private Sub FrmDashboardDesigner_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub INDDashboardDesigner_DashboardSaving(sender As Object, e As DevExpress.DashboardWin.DashboardSavingEventArgs)
        ' Determines whether the user has called the Save command.
        If e.Command = DevExpress.DashboardWin.DashboardSaveCommand.Save Then
            ' Saves the dashboard to the specified XML file.

            Dim ms As New MemoryStream, m_Buffer() As Byte, m_XML As String = ""
            CtrDashBoardDesign1.INDDashboardDesigner.Dashboard.SaveToXml(ms)
            m_Buffer = ms.ToArray()
            ms.Flush()
            ms.Close()
            m_XML = System.Text.Encoding.UTF8.GetString(m_Buffer)
            ms.Dispose()

            'CtrDashBoardDesign1.INDDashboardDesigner.Dashboard.SaveToXml(filePath)
            ' Specifies that the dashboard has been saved and no default actions are required.
            e.Handled = True
        End If
        ' Determines whether the user has called the Save As command.
        If e.Command = DevExpress.DashboardWin.DashboardSaveCommand.SaveAs Then
            'Dim result As DialogResult = InvokeMessageBox()
            'If result = System.Windows.Forms.DialogResult.OK Then
            '    CtrDashBoardDesign1.INDDashboardDesigner.Dashboard.SaveToXml(filePath)
            '    e.Handled = True

            '    ' Specifies that the dashboard has been saved.
            '    e.Saved = True
            'End If
            'If result = System.Windows.Forms.DialogResult.Cancel Then
            '    e.Handled = True
            '    e.Saved = False
            'End If
        End If
    End Sub

    Private Function InvokeMessageBox() As DialogResult
        Return SaveFileDialog1.ShowDialog(Me)
    End Function
End Class