Public Class CtrBudgetSettings

#Region "Properties and variables"

    ''' <summary>
    ''' valor del codigo del rubro
    ''' </summary>
    Private valueCodeRubro As String

#End Region

#Region "Methods and Functions"

    ''' <summary>
    ''' Cleans the control budget setting.
    ''' </summary>
    Public Sub CleanControlBudgetSetting()
        INDpopRubroSelect.Enabled = False
        INDTxtBudgetEntity.Text = String.Empty
        INDtxtEntryType.Text = String.Empty
        INDtxtResource.Text = String.Empty
        INDtxtRubro.Text = String.Empty
        INDtxtValidity.Text = String.Empty
    End Sub

    ''' <summary>
    ''' Returns the value rubro.
    ''' </summary>
    Public Function ReturnValueRubro() As String
        Return valueCodeRubro
    End Function

    ''' <summary>
    ''' Actions the control budget setting.
    ''' </summary>
    Public Sub ActionControlBudgetSetting()
        INDpopRubroSelect.Enabled = True
    End Sub

#End Region

#Region "Events"

    ''' <summary>
    ''' Handles the Load event of the CtrBudgetSettings control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub CtrBudgetSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CleanControlBudgetSetting()
    End Sub

    ''' <summary>
    ''' CTRs the rubro select1_ return value.
    ''' </summary>
    Private Sub CtrRubroSelect1_ReturnValue() Handles CtrRubroSelect1.ReturnValue
        Dim value As String = CtrRubroSelect1.ReturnValueRubro()
        INDTxtBudgetEntity.Text = value
        valueCodeRubro = value
    End Sub

#End Region

End Class
