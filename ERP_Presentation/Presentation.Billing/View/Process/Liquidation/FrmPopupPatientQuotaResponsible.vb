Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.Base

Public Class FrmPopupPatientQuotaResponsible

#Region "Private Fields"

    Private _suggestedThirdPartyId As Integer?
    Private _suggestedThirdPartyName As String
    Private _suggestedThirdPartyNit As String

#End Region

    Public Sub New()
        InitializeComponent()
        INDSleThirdParty.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "Limpiar selección (Supr)")})
    End Sub

    ''' <summary>
    ''' Constructor con parámetros para validación de mayoría de edad
    ''' </summary>
    ''' <param name="formTitle">Título del formulario</param>
    ''' <param name="suggestedId">Id del tercero sugerido</param>
    ''' <param name="suggestedName">Nombre del tercero sugerido</param>
    ''' <param name="suggestedNit">NIT del tercero sugerido</param>
    Public Sub New(formTitle As String, suggestedId As Integer?, suggestedName As String, suggestedNit As String)
        Me.New()
        If Not String.IsNullOrEmpty(formTitle) Then
            Me.Text = formTitle
        End If
        _suggestedThirdPartyId = suggestedId
        _suggestedThirdPartyName = suggestedName
        _suggestedThirdPartyNit = suggestedNit
    End Sub

    ''' <summary>
    ''' Sets the show message.
    ''' </summary>
    ''' <value>
    ''' The show message.
    ''' </value>
    Public WriteOnly Property ShowMessage(StatusCode As eStatusResult) As String
        Set(value As String)
            If StatusCode = eStatusResult.SUCCESS Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf StatusCode = eStatusResult.WARNING Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf StatusCode = eStatusResult.EXCEPTION Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    ''' <summary>
    ''' responsable de cuota moderadora
    ''' </summary>
    ''' <returns></returns>
    Public Property PatientQuotaResponsible As Integer?
        Get
            Return INDSleThirdParty.EditValue
        End Get
        Set(value As Integer?)
            INDSleThirdParty.EditValue = value
        End Set
    End Property

    Public Property PatientQuotaResponsibleText As String
        Get
            Return INDSleThirdParty.Text
        End Get
        Set(value As String)
            INDSleThirdParty.Properties.NullText = value
        End Set
    End Property

    ''' <summary>
    ''' Id del tercero responsable sugerido desde la información del ingreso
    ''' </summary>
    Public Property SuggestedThirdPartyId As Integer?
        Get
            Return _suggestedThirdPartyId
        End Get
        Set(value As Integer?)
            _suggestedThirdPartyId = value
        End Set
    End Property

    ''' <summary>
    ''' Nombre del tercero responsable sugerido
    ''' </summary>
    Public Property SuggestedThirdPartyName As String
        Get
            Return _suggestedThirdPartyName
        End Get
        Set(value As String)
            _suggestedThirdPartyName = value
        End Set
    End Property

    ''' <summary>
    ''' NIT del tercero responsable sugerido
    ''' </summary>
    Public Property SuggestedThirdPartyNit As String
        Get
            Return _suggestedThirdPartyNit
        End Get
        Set(value As String)
            _suggestedThirdPartyNit = value
        End Set
    End Property

    ''' <summary>
    ''' Postula automáticamente el tercero sugerido si existe
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmPopupPatientQuotaResponsible_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If _suggestedThirdPartyId.HasValue AndAlso _suggestedThirdPartyId.Value > 0 Then
            INDSleThirdParty.EditValue = _suggestedThirdPartyId.Value
            If Not String.IsNullOrEmpty(_suggestedThirdPartyName) Then
                Dim displayText = _suggestedThirdPartyName
                If Not String.IsNullOrEmpty(_suggestedThirdPartyNit) Then
                    displayText = $"{_suggestedThirdPartyNit} - {_suggestedThirdPartyName}"
                End If
                INDSleThirdParty.Properties.NullText = displayText
            End If
        End If
        INDSleThirdParty.Focus()
    End Sub

    ''' <summary>
    ''' Keydown
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmPopupPatientQuotaResponsible_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Close()
        End If
    End Sub

    ''' <summary>
    ''' Query popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleThirdParty_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleThirdParty.QueryPopUp
        If INDSleThirdParty.Properties.DataSource Is Nothing Then
            INDSleThirdParty.Properties.DataSource = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).CommonService.GetThirdParty()
        End If
    End Sub

    ''' <summary>
    ''' Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSbAccept_Click(sender As Object, e As EventArgs) Handles INDSbAccept.Click
        DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub INDSleThirdParty_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleThirdParty.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
            INDSleThirdParty.EditValue = Nothing
            INDSleThirdParty.Properties.NullText = String.Empty
        End If
    End Sub
End Class