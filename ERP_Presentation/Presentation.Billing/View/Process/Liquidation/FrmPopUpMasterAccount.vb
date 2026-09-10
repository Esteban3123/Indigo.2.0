Imports System.Text
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Billing.MVP
Imports Infrastructure.Data.Xpo

Public Class FrmPopUpMasterAccount

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property CurrencyId As Integer?
        Get
            Return INDSleCurrency.EditValue
        End Get
        Set(value As Integer?)
            INDSleCurrency.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property DateTRM As DateTime?
        Get
            Return INDSleDateTRM.EditValue
        End Get
        Set(value As DateTime?)
            INDSleDateTRM.EditValue = value
        End Set
    End Property

    Private Property _suggestedDateTRM As DateTime?

    ''' <summary>
    ''' propiedad de escritura que establece  la fecha del trm sugerido
    ''' </summary>
    Public WriteOnly Property SuggestedDateTRM As DateTime?
        Set(value As DateTime?)
            _suggestedDateTRM = value
        End Set
    End Property

    Private Sub FrmPopUpMasterAccount_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DateTRM = Nothing

        If _suggestedDateTRM IsNot Nothing Then
            DateTRM = _suggestedDateTRM
        End If
        Dim indigo = SessionValues.Instance
        CurrencyId = indigo.OfficialCurrencyId
        INDSleCurrency.Properties.NullText = indigo.CurrencyISO4217
    End Sub

    Private Sub FrmPopUpMasterAccount_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.INDSleCurrency.Focus()
    End Sub

    Private Sub GenReport_Click(sender As Object, e As EventArgs) Handles INDSbGenReport.Click
        If ValidateFields() Then
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
        End If
    End Sub

    Private Sub INDSbExportExcel_Click(sender As Object, e As EventArgs) Handles INDSbExportExcel.Click
        If ValidateFields() Then
            Me.DialogResult = System.Windows.Forms.DialogResult.Yes
        End If
    End Sub

    Private Sub FrmPopUpMasterAccount_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Public WriteOnly Property ShowMessage(ByVal Icon As Base.EeventViewerImages) As String
        Set(value As String)
            If Icon = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf Icon = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf Icon = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    Private Function ValidateFields() As Boolean
        Dim errorList As New StringBuilder()
        If DateTRM Is Nothing Then
            errorList.AppendLine(INDLciDateTRM.Text)
        End If
        If CurrencyId Is Nothing Then
            errorList.AppendLine(INDLciCurrency.Text)
        End If
        If errorList.Length > 0 Then
            Me.ShowMessage(EeventViewerImages.Advertencia) = String.Format("Existen campos sin diligenciar: " & vbCrLf & "{0}", errorList.ToString())
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCurrency_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCurrency.QueryPopUp
        If INDSleCurrency.Properties.DataSource Is Nothing Then
            INDSleCurrency.Properties.DataSource = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).CommonService.GetCurrency()
        End If
    End Sub


End Class