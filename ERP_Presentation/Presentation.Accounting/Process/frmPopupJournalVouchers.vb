'***********************************************************************
' Assembly         : Presentacion.Accounting
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 07-07-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Accounting.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base.BaseClass
Imports Domain.Base.Entities
Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Controls
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities.ObjectChangeTracker
Imports Domain.Entities
Imports System.Text
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo

#End Region

Public Class frmPopupJournalVouchers

#Region "Builders"
    Public Sub New()
        InitializeComponent()
    End Sub
#End Region

#Region "fields"

    ''' <summary>
    ''' Constante que contiene el nombre del módulo al cual pertenece el formulario
    ''' </summary>
    Const MODULE_NAME As String = "Accounting"

    ''' <summary>
    ''' Propiedad que almacena una listado de comprobantes de pago
    ''' </summary>
    ''' <returns></returns>
    Public Property ListJournalVouchers As List(Of SP_GetJournalVouchersByStatus_Result)

    ''' <summary>
    ''' Variable que instancia la clase BindingSource
    ''' </summary>
    Dim binding As New BindingSource

    ''' <summary>
    ''' Propiedad que almacena una listado de comprobantes de pago confirmados 
    ''' </summary>
    Public ListConfirmJournalVouchers As List(Of SP_GetJournalVouchersByStatus_Result)

    ''' <summary>
    ''' Define una variable de tipo entero
    ''' </summary>
    Dim IdsUncheck As Integer

    ''' <summary>
    ''' Define una variable de tipo Boolean que va a guardar un estado de confirmacion
    ''' </summary>
    Public Confirm As Boolean

    ''' <summary>
    ''' Declaración un campo privado para almacenar el valor numérico del mes
    ''' </summary>
    Dim _month As Integer

    ''' <summary>
    ''' Declaración de un campo privado para almacenar el valor numérico del mes
    ''' </summary>
    Dim _monthName As String

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property


    ''' <summary>
    ''' Propiedad que obtiene y asigna el valor del mes
    ''' </summary>
    ''' <returns></returns>
    Property Month As Integer
        Get
            Return _month
        End Get
        Set(value As Integer)
            _month = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que obtiene y asigna el nombre del mes
    ''' </summary>
    ''' <returns></returns>
    Property MonthName As String
        Get
            Return _monthName
        End Get
        Set(value As String)
            _monthName = value
        End Set
    End Property


#End Region

#Region "Events"

    ''' <summary>
    ''' Handles the Load event of the frmPopupJournalVouchers control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub frmPopupJournalVouchers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        binding.DataSource = ListJournalVouchers
        ToolBars.Visible = False
        gridJournalVouchers.DataSource = binding
        INDGvJournalVouvher.ExpandAllGroups()
    End Sub

    ''' <summary>
    ''' Configura la visualización inicial de la grilla en el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBtnUnselect_Click(sender As Object, e As EventArgs) Handles INDBtnUnselect.Click
        For Each item As SP_GetJournalVouchersByStatus_Result In ListJournalVouchers
            If item.State = True Then
                item.State = False
            End If
        Next
        gridJournalVouchers.DataSource = Nothing
        gridJournalVouchers.DataSource = ListJournalVouchers
        INDGvJournalVouvher.ExpandAllGroups()
    End Sub

    ''' <summary>
    ''' Evento que se relaciona con la confirmación y cierre del formulario.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub frmPopupJournalVouchers_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Me.DialogResult = System.Windows.Forms.DialogResult.OK Then
            If IdsUncheck <> 0 Then
                Confirm = False
            Else
                Dim existOtherJournalVoucher = ListJournalVouchers.FindAll(Function(x) x.EntityName <> ResourceManager.GetString("AccountingEntity", MODULE_NAME))
                If existOtherJournalVoucher.Count = 0 Then
                    Confirm = True
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que controla la edición en  la vista de la grilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvJournalVouvher_ShowingEditor(sender As Object, e As CancelEventArgs) Handles INDGvJournalVouvher.ShowingEditor
        Dim row As SP_GetJournalVouchersByStatus_Result
        row = DirectCast(INDGvJournalVouvher.GetFocusedRow, SP_GetJournalVouchersByStatus_Result)
        If row.EntityName <> ResourceManager.GetString("AccountingEntity", MODULE_NAME) Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OnlyConfirm", MODULE_NAME)
            e.Cancel = True
        Else
            e.Cancel = False
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al dar click sobre el btn "Seleccionar Todos".
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDBtnSelectAll_Click(sender As Object, e As EventArgs) Handles INDBtnSelectAll.Click
        For Each item As SP_GetJournalVouchersByStatus_Result In ListJournalVouchers
            If item.EntityName = ResourceManager.GetString("AccountingEntity", MODULE_NAME) Then
                item.State = True
            End If
        Next
        gridJournalVouchers.DataSource = Nothing
        gridJournalVouchers.DataSource = ListJournalVouchers
        INDGvJournalVouvher.ExpandAllGroups()
    End Sub


    ''' <summary>
    ''' Evento que se ejecuta al dar click sobre el btn "Aceptar".
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub INDBtnOK_Click(sender As Object, e As EventArgs) Handles INDBtnOK.Click
        Dim idx As String = String.Empty
        For Each item As SP_GetJournalVouchersByStatus_Result In ListJournalVouchers
            If item.State = True Then
                If ListConfirmJournalVouchers Is Nothing Then
                    ListConfirmJournalVouchers = New List(Of SP_GetJournalVouchersByStatus_Result)
                End If
                Dim itemConfirm As New SP_GetJournalVouchersByStatus_Result
                itemConfirm.Code = item.Code
                If idx.Trim().Equals(String.Empty) Then
                    idx = item.Id.ToString()
                Else
                    idx += "," + item.Id.ToString
                End If

                ListConfirmJournalVouchers.Add(itemConfirm)
            Else
                If item.EntityName = ResourceManager.GetString("AccountingEntity", MODULE_NAME) Then
                    IdsUncheck = IdsUncheck + 1
                End If
            End If
        Next
        If ListConfirmJournalVouchers IsNot Nothing AndAlso ListConfirmJournalVouchers.Count > 0 Then
            If (MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                Using model As New MDocumentAccount("")
                    Dim result As List(Of SP_ChangeStatusJournalVouchers_Result) = Await model.SaveListJournalVouchers(CInt(1), idx, Month)
                    If result IsNot Nothing AndAlso result.Count > 0 Then
                        Dim consecutives As String = String.Empty
                        For Each item In ListConfirmJournalVouchers
                            If consecutives.Trim().Equals(String.Empty) Then
                                consecutives = item.Code
                            Else
                                consecutives += " - " + item.Code
                            End If
                        Next
                        If ListConfirmJournalVouchers.Count = 1 Then
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("DocumentConfirmed", MODULE_NAME), consecutives)
                        Else
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("ConfirmedDocuments", MODULE_NAME), consecutives)
                        End If
                    End If
                End Using
            End If
        End If
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Close()
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al presionar la tecla "ESC", el cual llama al método Close()
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub frmPopupJournalVouchers_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Close()
        End If
    End Sub
#End Region

End Class