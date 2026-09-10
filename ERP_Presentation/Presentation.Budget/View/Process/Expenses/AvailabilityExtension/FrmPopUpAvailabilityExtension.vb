'***********************************************************************
' Assembly         : Presentacion.Budget
' Author           : Juan Carlos Bermudez
' Created          : 21-09-2015
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Budget.MVP
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.BudgetRepository
Imports DevExpress.XtraGrid.Views.Grid
Imports Domain.Entities.RecognitionDetail
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eresources
Imports Domain.Base.Entities
Imports Presentation.Controls
Imports System.Drawing
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports System.Text

#End Region

Public Class FrmPopUpAvailabilityExtension

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el id de la vigencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _ValidityId As Integer
    Public Property ValidityId As Integer
        Get
            Return _ValidityId
        End Get
        Set(value As Integer)
            _ValidityId = value
        End Set
    End Property

    ''' <summary>
    ''' Listado de disponibilidades
    ''' </summary>
    ''' <remarks></remarks>
    Public Property AvailabilityXpo As XPInstantFeedbackSource
        Get
            Return INDsleAvailability.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleAvailability.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para establecer si se va a editar un registro 
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property EditMode As Boolean
        Set(value As Boolean)
            _editMode = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad publica para pasar el registro que se va a editar
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ItemAvailabilityExtensionDetail As AvailabilityExtensionDetail
        Set(value As AvailabilityExtensionDetail)
            _itemAvailability = value
        End Set
    End Property

    ''' <summary>
    ''' id de la disponibilidad seleccionada
    ''' </summary>
    ''' <remarks></remarks>
    Public Property AvailabilityId As Integer
        Get
            Return INDsleAvailability.EditValue
        End Get
        Set(value As Integer)
            INDsleAvailability.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para activar o desactivar controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Private WriteOnly Property ActionsControls As Boolean
        Set(value As Boolean)
            INDspnDaysExpired.Enabled = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para para pasar el listado del detalle de la prorroga de disponibilidades
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ListAvailabilityExtensionDetailValidation As List(Of AvailabilityExtensionDetail)
        Set(value As List(Of AvailabilityExtensionDetail))
            If value IsNot Nothing Then
                _listAvailabilityExtensionDetailValidation = New List(Of AvailabilityExtensionDetail)(value.ToArray())
            End If
        End Set
    End Property

    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    ''' <remarks></remarks>
    Private Const MODULE_NAME = "Budget"

    ''' <summary>
    ''' item a agregar o editar
    ''' </summary>
    ''' <remarks></remarks>
    Dim _itemAvailability As Domain.Entities.AvailabilityExtensionDetail

    ''' <summary>
    ''' bandera para saber que se esta editando
    ''' </summary>
    ''' <remarks></remarks>
    Dim _editMode As Boolean

    ''' <summary>
    ''' entidad de disponibilidad
    ''' </summary>
    ''' <remarks></remarks>
    Dim availability As Domain.Entities.Availability

    ''' <summary>
    ''' listado del detalle de la prorroga de disponibilidades para validar que no se repita la misma disponibilidad
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listAvailabilityExtensionDetailValidation As List(Of Domain.Entities.AvailabilityExtensionDetail)

    ''' <summary>
    ''' Evento publico para agregar una disponibilidad en
    ''' la rejilla del form principal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddAvailabilityToGridFormPrinipal(sender As Object, e As AddAvailabilityToGridAvailabilityExtension)

#End Region

#Region "METHODS"

    ''' <summary>
    ''' Carga el datasource del search de disponibilidades
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub LoadDataSourceAvailability()
        AvailabilityXpo = XpoServiceEx.Instance(indigo.TransactionalContainer).BudgetService.ListViewAvailabilityBalance(ValidityId, 2, Me.GetDateServer())
    End Sub

    ''' <summary>
    ''' metodo para limpiar los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        _itemAvailability = Nothing
        availability = Nothing
        INDsleAvailability.EditValue = Nothing
        INDsleAvailability.Properties.ReadOnly = False
        INDdeDateExpiredInitial.EditValue = Nothing
        INDdeDateExpiredEnd.EditValue = Nothing
        INDspnDaysExpired.EditValue = Nothing
        ActionsControls = False
        BarraBotones.FilterDataSource = Nothing
        _editMode = False
    End Sub

    ''' <summary>
    ''' metodo para cargar los controles con la informacion requerida
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadControls()
        If _editMode = True Then
            INDsbAccept.Text = ResourceManager.GetString("Edit")
        End If
        With _itemAvailability
            Using model As New MAvailability(Me.Tag)
                availability = model.GetAvailabilityById(.AvailabilityId)
                INDsleAvailability.Properties.NullText = availability.Code
                INDsleAvailability.EditValue = availability.Id
                INDspnDaysExpired.EditValue = .ExtensionDay
                INDdeDateExpiredInitial.EditValue = .AvailabilityDate
                INDdeDateExpiredEnd.EditValue = .ExpirationDate
            End Using
            ActionsControls = True
            INDsleAvailability.Focus()
        End With
    End Sub

    ''' <summary>
    ''' valida los controles del formualario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopup() As String
        Dim errors As New StringBuilder
        If _editMode = False AndAlso availability IsNot Nothing Then
            If _listAvailabilityExtensionDetailValidation IsNot Nothing AndAlso _listAvailabilityExtensionDetailValidation.Count > 0 Then
                Dim availabilityExtensionDetailTmp = _listAvailabilityExtensionDetailValidation.Find(Function(x) x.AvailabilityId = availability.Id)
                If availabilityExtensionDetailTmp IsNot Nothing Then
                    errors.AppendLine("La disponibilidad con Código " + availability.Code + " Ya se encuentra en el formulario Principal")
                End If
            End If
        End If
        If INDsleAvailability.EditValue Is Nothing Then
            errors.AppendLine(INDlciAvailability.Text + ResourceManager.GetString("Empty"))
        End If
        If INDspnDaysExpired.EditValue Is Nothing OrElse INDspnDaysExpired.EditValue = 0 Then
            errors.AppendLine(INDlciDaysExpired.Text + ResourceManager.GetString("Empty"))
        End If
        Return errors.ToString()
    End Function

    ''' <summary>
    ''' asigna valores
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetValues()
        If _editMode = False Then
            _itemAvailability = New AvailabilityExtensionDetail
        End If
        With _itemAvailability
            .AvailabilityId = availability.Id
            .ExtensionDay = INDspnDaysExpired.EditValue
            .AvailabilityDate = INDdeDateExpiredInitial.EditValue
            .ExpirationDate = INDdeDateExpiredEnd.EditValue
            .CodeAvailability = availability.Code
            Select Case availability.Status
                Case 1
                    .StatusAvailability = "Registrado"
                Case 2
                    .StatusAvailability = "Confirmado"
                Case 3
                    .StatusAvailability = "Anulado"
            End Select
            .BalanceAvailability = availability.AvailabilityDetail.ToList().Sum(Function(item) As Decimal
                                                                                    Return item.Balance
                                                                                End Function)
        End With
    End Sub

    ''' <summary>
    ''' Añadimos la disponibilidad al grid del formulario principal
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub AddAvailability()
        Try
            INDsbAccept.Enabled = False
            Dim errors = ValidateControlsPopup()
            If errors.Length > 0 Then
                INDsbAccept.Enabled = True
                Mensaje(EeventViewerImages.Advertencia) = errors
                Exit Sub
            End If
            Dim args As New AddAvailabilityToGridAvailabilityExtension
            SetValues()
            If _editMode = True Then
                args.itemAvailability = _itemAvailability
                args.EditMode = True
            Else
                args.itemAvailability = _itemAvailability
                _listAvailabilityExtensionDetailValidation.Add(_itemAvailability)
            End If
            RaiseEvent AddAvailabilityToGridFormPrinipal(Nothing, args)
            INDsbAccept.Enabled = True
            If _editMode = True Then
                availability = Nothing
                Me.Close()
            Else
                CleanControls()
            End If
        Catch ex As Exception
            INDsbAccept.Enabled = True
            Throw ex
        End Try
    End Sub

#End Region

#Region "Handles"

#Region "Load"
    ''' <summary>
    ''' Libera la memoria al cerrar el Frm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _itemAvailability = Nothing
        _editMode = Nothing
        availability = Nothing
        _listAvailabilityExtensionDetailValidation = Nothing
    End Sub


    ''' <summary>
    ''' se dispara al cargar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPopUpAvailabilityExtension_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        BarraBotones.StatusRecordVisible = False
        If _editMode = True Then
            LoadControls()
        Else
            CleanControls()
        End If
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' se dispara al activarse el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPopUpAvailabilityExtension_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If availability Is Nothing Then
            INDsleAvailability.Focus()
        End If
        If _editMode = True Then
            INDdeDateExpiredInitial.Focus()
        End If
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' se dispara al cerrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPopUpAvailabilityExtension_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If availability IsNot Nothing Then
            If Not MessageIndigo.Show(ResourceManager.GetString("CloseForm", "Payments"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                e.Cancel = True
            End If
        End If
    End Sub

#End Region

#Region "ButtonClick"
    ''' <summary>
    ''' evento al dar click en control Availability
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleAvailability_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleAvailability.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("228", Nothing, True)
            LoadDataSourceAvailability()
        End If
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' carga el datasource de la disponibilidades
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleAvailability_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleAvailability.QueryPopUp
        If AvailabilityXpo Is Nothing Then
            LoadDataSourceAvailability()
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' se agrega el item a la rejilla principal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsbAccept_Click(sender As Object, e As EventArgs) Handles INDsbAccept.Click
        AddAvailability()
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' se dispara al cambiar la disponibilidad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleAvailability_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleAvailability.EditValueChanged
        If AvailabilityId <> Nothing OrElse AvailabilityId <> 0 Then
            Using model As New MAvailability(Me.Tag)
                availability = model.GetAvailabilityById(AvailabilityId)
                INDdeDateExpiredInitial.EditValue = availability.ExpirationDate
                INDspnDaysExpired.Focus()
                ActionsControls = True
            End Using
        End If
    End Sub

    ''' <summary>
    ''' se dispara al cambiar los dias de expiración
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDspnDaysExpired_EditValueChanged(sender As Object, e As EventArgs) Handles INDspnDaysExpired.EditValueChanged
        If INDspnDaysExpired.EditValue IsNot Nothing Then
            INDdeDateExpiredEnd.EditValue = DateAdd(DateInterval.Day, INDspnDaysExpired.EditValue, availability.ExpirationDate)
        End If
    End Sub

#End Region

#End Region

#Region "BAR BUTTONS"
    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
    End Sub
#End Region

End Class


Public Class AddAvailabilityToGridAvailabilityExtension
    Inherits EventArgs

    ''' <summary>
    ''' item a agregar o editar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property itemAvailability As Domain.Entities.AvailabilityExtensionDetail

    ''' <summary>
    ''' Bandera para saber si el dato se va a editar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property EditMode As Boolean

End Class