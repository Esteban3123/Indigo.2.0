Public Class PopUpChangeLabel

#Region "Properties"
    ''' <summary>
    ''' Event accept
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Public Event AcceptLabelType(sender As Object, e As LabelTypeEventArgs)
#End Region

#Region "Methods"
    ''' <summary>
    ''' Load label types
    ''' </summary>
    Public Sub LoadLabelTypes()
        INDGleLabelType.Properties.DataSource = {
            New Tuple(Of Byte, String)(1, "Bolsa"),
            New Tuple(Of Byte, String)(2, "Mediana"),
            New Tuple(Of Byte, String)(3, "Jeringa")
        }.ToList()
        INDGleLabelType.EditValue = CByte(1)
    End Sub
#End Region

#Region "Events"
    ''' <summary>
    ''' Close form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub PopUpChangeLabel_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' Load form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub PopUpChangeLabel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadLabelTypes()
    End Sub

    ''' <summary>
    ''' Acept
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSbAccept_Click(sender As Object, e As EventArgs) Handles INDSbAccept.Click
        RaiseEvent AcceptLabelType(Me, New LabelTypeEventArgs() With {.LabelType = INDGleLabelType.EditValue})
    End Sub
#End Region

    Public Class LabelTypeEventArgs
        Inherits EventArgs

        Public Property LabelType As Byte
    End Class
End Class