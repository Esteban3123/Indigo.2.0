'***********************************************************************
' Assembly         : Presentacion.Payments.Utils
' Author           : Carlos Mario Arias Rubiano
' Created          : 28/05/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls
Imports Presentation.Payments.MVP
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.Text
Imports Presentation.Controls.MVP
Imports Presentation.Accounting
Imports Presentation.Common
Imports Domain.Base.Entities

#End Region

Public Class FrmParameters
    Implements IParameters

#Region "Const"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Payments"

    Dim filter() As Object = {5, True}

#End Region

#Region "Variables"

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _record As BlockRecordPayments

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PParameters

    ''' <summary>
    ''' Representa la entidad de parametros de pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property _settingPayments As SettingPayments

    ''' <summary>
    ''' Contiene el listado de edades
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListAges As List(Of AgesPayments)

    ''' <summary>
    ''' Contiene el listado de eliminados de edades
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteAges As List(Of AgesPayments)

    ''' <summary>
    ''' Representa la entidad de edades de pagos
    ''' </summary>
    ''' <remarks></remarks>
    Dim ages As New AgesPayments

    ''' <summary>
    ''' bandera para saber si se esta editando
    ''' </summary>
    ''' <remarks></remarks>
    Dim editPopup As Boolean

#End Region

#Region "Tuples"

    Dim _listGetPostulateBudgetInterfaceBy As List(Of Tuple(Of Byte, String))
    ReadOnly Property ListGetPostulateBudgetInterfaceBy As List(Of Tuple(Of Byte, String))
        Get
            If _listGetPostulateBudgetInterfaceBy Is Nothing Then
                _listGetPostulateBudgetInterfaceBy = New List(Of Tuple(Of Byte, String))
                _listGetPostulateBudgetInterfaceBy.Add(New Tuple(Of Byte, String)(1, "Por Factura"))
                '_listGetPostulateBudgetInterfaceBy.Add(New Tuple(Of Byte, String)(2, "Por Cuenta por Pagar"))
            End If
            Return _listGetPostulateBudgetInterfaceBy
        End Get
    End Property

#End Region

#Region "Properties"

    ''' <summary>
    ''' Contiene el listado de cuentas de aprovechamiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountAchievementXpo As XPInstantFeedbackSource Implements IParameters.AccountAchievementXpo
        Get
            Return CType(INDsleVoucherAmortization.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleVoucherAmortization.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Contiene el listado de comprobante de notas credito
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property VoucherCreditNotesXpo As XPInstantFeedbackSource Implements IParameters.VoucherCreditNotesXpo
        Get
            Return CType(INDsleVoucherCreditNotes.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleVoucherCreditNotes.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Contiene el listado de comprobante de notas debito
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property VoucherDebitNotesXpo As XPInstantFeedbackSource Implements IParameters.VoucherDebitNotesXpo
        Get
            Return CType(INDsleVoucherDebitNotes.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleVoucherDebitNotes.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Contiene el listado de comprobante de traslado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property VoucherTransfersXpo As XPInstantFeedbackSource Implements IParameters.VoucherTransfersXpo
        Get
            Return CType(INDsleVoucherTransfer.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleVoucherTransfer.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Contiene el listado de comprobante de tipo de cuentas por pagar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property VoucherTypeCxpXpo As XPInstantFeedbackSource Implements IParameters.VoucherTypeCxpXpo
        Get
            Return CType(INDsleVoucherCxP.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleVoucherCxP.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta de aprovechamiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdAccountAchievement As Long Implements IParameters.IdAccountAchievement
        Get
            Return CLng(INDsleVoucherAmortization.EditValue)
        End Get
        Set(value As Long)
            INDsleVoucherAmortization.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del comprobante de notas credito
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdVoucherCreditNotes As Integer? Implements IParameters.IdVoucherCreditNotes
        Get
            Return CInt(INDsleVoucherCreditNotes.EditValue)
        End Get
        Set(value As Integer?)
            INDsleVoucherCreditNotes.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del comprobante de la cuenta por pagar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdVoucherCxp As Integer? Implements IParameters.IdVoucherCxp
        Get
            Return CInt(INDsleVoucherCxP.EditValue)
        End Get
        Set(value As Integer?)
            INDsleVoucherCxP.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del comprobante de notas debito
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdVoucherDebitNotes As Integer? Implements IParameters.IdVoucherDebitNotes
        Get
            Return CInt(INDsleVoucherDebitNotes.EditValue)
        End Get
        Set(value As Integer?)
            INDsleVoucherDebitNotes.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del comprobante de traslado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdVoucherTransfers As Integer? Implements IParameters.IdVoucherTransfers
        Get
            Return CInt(INDsleVoucherTransfer.EditValue)
        End Get
        Set(value As Integer?)
            INDsleVoucherTransfer.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del comprobante de amortizacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdVoucherAmortization As Integer? Implements IParameters.IdVoucherAmortization
        Get
            Return INDsleVoucherAmortization.EditValue
        End Get
        Set(value As Integer?)
            INDsleVoucherAmortization.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del comprobante de amortizacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property VoucherAmortizationXpo As XPInstantFeedbackSource Implements IParameters.VoucherAmortizationXpo
        Get
            Return INDsleVoucherAmortization.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleVoucherAmortization.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Especifica si se encuentra habilitada la interfaz con presupuesto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetInterface As Boolean Implements IParameters.BudgetInterface
        Get
            Return CBool(INDsleBudgetInterface.EditValue)
        End Get
        Set(value As Boolean)
            INDsleBudgetInterface.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Especifica si es obligatoria la asociación del compromiso / obligación en la cuenta por pagar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ObligationBudgetInterface As Boolean Implements IParameters.ObligationBudgetInterface
        Get
            Return CBool(INDsleObligationBudgetInterface.EditValue)
        End Get
        Set(value As Boolean)
            INDsleObligationBudgetInterface.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Indica si la obligacion se crea usando solo los valores debitos de la cuenta por pagar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ObligationDebitValue As Boolean Implements IParameters.ObligationDebitValue
        Get
            Return CBool(INDsleObligationDebitValue.EditValue)
        End Get
        Set(value As Boolean)
            INDsleObligationDebitValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Especifica si se crea una sola obligacion por el grupo de facturas asociada a la cuenta por pagar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OnlyObligation As Boolean Implements IParameters.OnlyObligation
        Get
            Return CBool(INDsleOnlyObligation.EditValue)
        End Get
        Set(value As Boolean)
            INDsleOnlyObligation.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Especifica si la interfaz presupuestal se postula en:
    '''     1 - La Factura
    '''     2 - La Cuenta por Pagar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PostulateBudgetInterfaceBy As Byte Implements IParameters.PostulateBudgetInterfaceBy
        Get
            Return CByte(INDslePostulateBudgetInterfaceBy.EditValue)
        End Get
        Set(value As Byte)
            INDslePostulateBudgetInterfaceBy.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si el registro esta activo o inactivo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Boolean Implements IParameters.Status
        Get
            Return CBool(BarraBotones.StatusRecord)
        End Get
        Set(value As Boolean)
            If value = True Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            End If
        End Set
    End Property

    Public Property NameMaximumAgeRange As String Implements IParameters.NameMaximumAgeRange
        Get
            Return INDTxtNameMaximumAgeRange.Text
        End Get
        Set(value As String)
            INDTxtNameMaximumAgeRange.Text = value
        End Set
    End Property

    Public Property NameMinimumAgeRange As String Implements IParameters.NameMinimumAgeRange
        Get
            Return INDTxtNameMinimumAgeRange.Text
        End Get
        Set(value As String)
            INDTxtNameMinimumAgeRange.Text = value
        End Set
    End Property

#End Region

#Region "ICrud"

    ''' <summary>
    ''' Método: Deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
        DeleteBlockedRecord()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    Public Sub Buscar() Implements IcrudBase.Buscar

    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' Método: Guardar
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub
        Else
            If ValidateFields() = False Then
                Exit Sub
            End If
        End If
        AssigningValues()
        Using model As New MParameters(Me.Tag.ToString())
            AsyncLoader(True)
            Dim Result = Await model.SaveSettingPayments(_settingPayments)
            AsyncLoader(False)
            If Result.StateResult = True Then
                If _settingPayments.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                ElseIf _settingPayments.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                End If
                Me._settingPayments = Result.ObjectEmbbeded
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                AsyncLoader(False)

                DeleteBlockedRecord()
                CleanControls()
                LoadControls()
                INDsleVoucherCxP.Focus()

                Me.BarraBotones.CleanAuditBasic()
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), Result.ObjectEmbbeded.CreationUser)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), Result.ObjectEmbbeded.CreationDate)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), Result.ObjectEmbbeded.ModificationUser)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), Result.ObjectEmbbeded.ModificationDate)
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
            Else
                If Result.MessageResult(0) = ErrorConcurrencia Then
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                Else
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                End If
            End If
        End Using
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    Public Sub Nuevo() Implements IcrudBase.Nuevo

    End Sub

    Public Sub OpenSearch() Implements IcrudBase.OpenSearch

    End Sub

#End Region

#Region "Events"

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _record = Nothing
        _idOperativeUnit = Nothing
        Presenter = Nothing
        _settingPayments = Nothing
        ListAges = Nothing
        ListDeleteAges = Nothing
        ages = Nothing
        editPopup = Nothing
    End Sub
    ''' <summary>
    ''' Evento load del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmParameters_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyParameters, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue


        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        ListActions.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(viewAges, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In viewAges.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
        IndigoGridControl1.RefreshGrid(INDgcAges)

        INDslePostulateBudgetInterfaceBy.Properties.DataSource = ListGetPostulateBudgetInterfaceBy

        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        Presenter = New PParameters(Me)
        LoadStatus()
        Deshacer()
        LoadControls()
        CleanControlsPopup()
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento cerrar del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmParameters_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' Cuando se activa el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmParameters_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If IdVoucherCxp = 0 Then
            INDsleVoucherCxP.Focus()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Abre el frontal de tipo de documento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleVoucherCxP_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleVoucherCxP.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using pop As New FrmTransparent(New FrmDocumentType With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent}, False)
                pop.Show()
            End Using
            Presenter.Initialize()
        End If
    End Sub

    ''' <summary>
    ''' Abre el frontal de tipo de documento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleVoucherTransfer_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleVoucherTransfer.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using pop As New FrmTransparent(New FrmDocumentType With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent}, False)
                pop.Show()
            End Using
            Presenter.Initialize()
        End If
    End Sub

    ''' <summary>
    ''' Abre el frontal de tipo de documento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleVoucherCreditNotes_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleVoucherCreditNotes.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using pop As New FrmTransparent(New FrmDocumentType With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent}, False)
                pop.Show()
            End Using
            Presenter.Initialize()
        End If
    End Sub

    ''' <summary>
    ''' Abre el frontal de tipo de documento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleVoucherDebitNotes_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleVoucherDebitNotes.ButtonClick, INDsleVoucherAmortization.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using pop As New FrmTransparent(New FrmDocumentType With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent}, False)
                pop.Show()
            End Using
            Presenter.Initialize()
        End If
    End Sub

    ''' <summary>
    ''' Abre el frontal de Puc
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleAccountAchievement_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using pop As New FrmTransparent(New FrmPopupPUC With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent}, False)
                pop.Show()
            End Using
            INDsleVoucherAmortization.Properties.DataSource = Nothing
        End If
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDsleVoucherCxP_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleVoucherCxP.QueryPopUp
        If INDsleVoucherCxP.Properties.DataSource Is Nothing Then
            Presenter.InitializeVoucherCxp()
        End If
    End Sub

    Private Sub INDsleVoucherTransfer_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleVoucherTransfer.QueryPopUp
        If INDsleVoucherTransfer.Properties.DataSource Is Nothing Then
            Presenter.InitializeVoucherTransfer()
        End If
    End Sub

    Private Sub INDsleVoucherCreditNotes_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleVoucherCreditNotes.QueryPopUp
        If INDsleVoucherCreditNotes.Properties.DataSource Is Nothing Then
            Presenter.InitializeVoucherCreditNotes()
        End If
    End Sub

    Private Sub INDsleVoucherDebitNotes_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleVoucherDebitNotes.QueryPopUp
        If INDsleVoucherDebitNotes.Properties.DataSource Is Nothing Then
            Presenter.InitializeVoucherDebitNotes()
        End If
    End Sub

    Private Sub INDsleVoucherAmortization_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleVoucherAmortization.QueryPopUp
        If INDsleVoucherAmortization.Properties.DataSource Is Nothing Then
            Presenter.InitializeVoucherAmortization()
        End If
    End Sub

    Private Sub INDpceAges_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDpceAges.QueryPopUp
        If editPopup = False Then
            If ListAges IsNot Nothing AndAlso ListAges.Count > 1 Then
                Dim age = ListAges.ElementAt(ListAges.Count - 1)
                If age.EndRange = 999 Then 'se deja quemado este valor de edad maxima
                    e.Cancel = True
                    Mensaje(EeventViewerImages.Advertencia) = "El valor maximo para las edades ya esta en uso, no se pueden agregar mas edades"
                End If
            End If
        End If
    End Sub
#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar las teclas f4 o enter en el control de nombre del popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceAges_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDpceAges.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter OrElse e.KeyCode = System.Windows.Forms.Keys.F4 Then
            INDpceAges.ShowPopup()
            INDtxtName.Focus()
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton del popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddAges_Click(sender As Object, e As EventArgs) Handles INDbtnAddAges.Click
        AddRange()
    End Sub

#End Region

#Region "MenuContextual"

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim button As DevExpress.XtraEditors.SimpleButton = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        ages = DirectCast(viewAges.GetFocusedRow(), AgesPayments)
        Select Case button.Tag.ToString
            Case "Edit"
                editPopup = True
                With ages
                    INDtxtName.Text = .Name
                    INDseInitialRange.EditValue = .InitialRange
                    INDseEndRange.EditValue = .EndRange
                    If .EndRange = 999 Then
                        INDseEndRange.Properties.MaxValue = 999
                    Else
                        INDseEndRange.Properties.MinValue = .InitialRange + 1
                    End If
                    INDcpeColor.EditValue = .Color
                End With
                INDbtnAddAges.Text = ResourceManager.GetString("Edit")
                INDpceAges.ShowPopup()
                INDtxtName.Focus()
            Case "Remove"
                DeleteRange(ages)
        End Select
    End Sub

#End Region

#Region "CloseUp"

    Private Sub INDpceAges_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDpceAges.CloseUp
        If editPopup = True Then
            CleanControlsPopup()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDsleBudgetInterface_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleBudgetInterface.EditValueChanged
		INDlyItemObligationBudgetInterface.AllowHide = Not BudgetInterface
        INDlyItemObligationBudgetInterface.ShowInCustomizationForm = Not BudgetInterface
        INDlyItemObligationDebitValue.AllowHide = Not BudgetInterface
        INDlyItemObligationDebitValue.ShowInCustomizationForm = Not BudgetInterface
		
        INDlyItemOnlyObligation.AllowHide = Not BudgetInterface
        INDlyItemOnlyObligation.ShowInCustomizationForm = Not BudgetInterface
        INDlyItemPostulateBudgetInterfaceBy.AllowHide = Not BudgetInterface
        INDlyItemPostulateBudgetInterfaceBy.ShowInCustomizationForm = Not BudgetInterface
		
		INDlyItemOnlyObligation.Enabled = False
		INDlyItemPostulateBudgetInterfaceBy.Enabled = False		
        
        INDlygBudget.Visibility = If(BudgetInterface, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
    End Sub

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Establece el rango de las edades
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetRanges()
        If ListAges IsNot Nothing AndAlso ListAges.Count > 0 Then
            INDseInitialRange.EditValue = ListAges.ElementAt(ListAges.Count - 1).EndRange + 1
            INDseEndRange.EditValue = INDseInitialRange.EditValue + 1
            If INDseEndRange.EditValue >= 999 Then
                INDseEndRange.EditValue = 999
                INDseEndRange.Properties.MaxValue = 999
                INDLciNameMaximumAgeRange.Text = String.Format(INDLciNameMaximumAgeRange.Tag, "999")
            Else
                INDseEndRange.Properties.MinValue = INDseInitialRange.EditValue + 1
                INDLciNameMaximumAgeRange.Text = String.Format(INDLciNameMaximumAgeRange.Tag, ListAges.ElementAt(ListAges.Count - 1).EndRange.ToString())
            End If
        Else
            INDseInitialRange.EditValue = 1
            INDseEndRange.EditValue = 2
            INDseEndRange.Properties.MinValue = 2
            INDLciNameMaximumAgeRange.Text = String.Format(INDLciNameMaximumAgeRange.Tag, "1")
        End If
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles del popup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsPopup()
        INDbtnAddAges.Text = ResourceManager.GetString("Add")
        INDtxtName.Text = String.Empty
        INDcpeColor.EditValue = Nothing
        editPopup = False
        ages = Nothing
        INDseEndRange.Properties.ReadOnly = False
        SetRanges()
    End Sub

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        Dim state As Boolean
        Select Case Status
            Case CBool(eActionsStatusRecords.Active)
                state = True
            Case CBool(eActionsStatusRecords.Inactive)
                state = False
        End Select
        Using model As New MParameters(Me.Tag.ToString())
            AsyncLoader(True)
            Dim Result = Await model.ChangeState(_settingPayments.IdOperatingUnit, state)
            AsyncLoader(False)
            If Result.StateResult = True Then
                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
            Else
                If Result.MessageResult(0) = ErrorConcurrencia Then
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                Else
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                End If
            End If
        End Using
    End Function

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Metodo para limpiar los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        IdVoucherCxp = Nothing
        IdVoucherTransfers = Nothing
        IdVoucherCreditNotes = Nothing
        IdVoucherDebitNotes = Nothing
        IdVoucherAmortization = Nothing

        INDsleBudgetInterface.EditValue = Nothing
        INDsleObligationDebitValue.EditValue = Nothing
        INDsleOnlyObligation.EditValue = Nothing
        INDslePostulateBudgetInterfaceBy.EditValue = Nothing
        INDsleObligationBudgetInterface.EditValue = Nothing

        INDsleVoucherCxP.Properties.NullText = String.Empty
        INDsleVoucherTransfer.Properties.NullText = String.Empty
        INDsleVoucherCreditNotes.Properties.NullText = String.Empty
        INDsleVoucherDebitNotes.Properties.NullText = String.Empty
        INDsleVoucherAmortization.Properties.NullText = String.Empty

        ListAges = Nothing
        ListDeleteAges = Nothing
        CleanControlsPopup()
        INDgcAges.DataSource = Nothing
    End Sub

    ''' <summary>
    ''' Deletes the blocked record.
    ''' </summary>
    Public Async Sub DeleteBlockedRecord()
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MBlockRecordAndSequensePayments(CStr(Me.Tag))
                Await Model.DeleteBlockRecord(_record)
                _record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.IcrudBase.Mensaje
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
    ''' Loads the controls.
    ''' </summary>
    Private Async Sub LoadControls()
        Me.BarraBotones.StatusRecordVisible = True
        Using Model As New MParameters(CStr(Me.Tag))
            AsyncLoader(True)
            Dim resulOperation = Await Model.GetSettingPaymentsByIdOperatingUnit(_idOperativeUnit)
            AsyncLoader(False)
            _settingPayments = resulOperation.ObjectEmbbeded
            If _settingPayments IsNot Nothing AndAlso _settingPayments.Id > 0 Then
                Using ModelRecord As New MBlockRecordAndSequensePayments(CStr(Me.Tag))
                    Dim result = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_settingPayments.Id))
                    With _settingPayments
                        LayoutControls.SetCustomFieldsValue(.CustomProperties)

                        Me.BarraBotones.CleanAuditBasic()
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                        IdVoucherCxp = .IdJournalVoucherAccountPayable
                        INDsleVoucherCxP.Properties.NullText = .VoucherCxpDescription

                        IdVoucherTransfers = .IdJournalVoucherTranslation
                        INDsleVoucherTransfer.Properties.NullText = .VoucherTransferDescription

                        IdVoucherCreditNotes = .IdJournalVoucherCreditNotes
                        INDsleVoucherCreditNotes.Properties.NullText = .VoucherCreditNotesDescription

                        IdVoucherDebitNotes = .IdJournalVoucherDebitNotes
                        INDsleVoucherDebitNotes.Properties.NullText = .VoucherDebitNotesDescription

                        IdVoucherAmortization = .IdJournalVocuherAmortization
                        INDsleVoucherAmortization.Properties.NullText = .VoucherAmortizationDescription

                        BudgetInterface = .BudgetInterface
                        ObligationBudgetInterface = .ObligationBudgetInterface
                        ObligationDebitValue = .ObligationDebitValue
                        OnlyObligation = .OnlyObligation
                        PostulateBudgetInterfaceBy = If({1, 2}.Contains(.PostulateBudgetInterfaceBy), .PostulateBudgetInterfaceBy, 1)

                        NameMaximumAgeRange = .NameMaximumAgeRange
                        NameMinimumAgeRange = .NameMinimumAgeRange
                        Status = .State

                        ListAges = .AgesPayments.ToList
                        INDgcAges.DataSource = Nothing
                        INDgcAges.DataSource = ListAges
                        SetRanges()
                    End With
                    Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._settingPayments.Id)
                    If result.Id = 0 Then
                        Dim state = New Domain.Base.Entities.ObjectChangeTracker
                        state.State = Domain.Base.Entities.ObjectState.Added
                        _record = New BlockRecordPayments With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = CInt(Me.Tag), .CodUser = Me.indigo.UserIndigo, .IdRecord = _settingPayments.Id}
                        Dim operation = Await ModelRecord.SaveBlockRecord(_record)
                        _record = operation.ObjectEmbbeded
                    Else
                        Dim xtraMessage As String = String.Format(ResourceManager.GetString("RecordLocked"), result.CodUser, result.NameUser, result.BlockDate)
                        _record = result
                        Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                    End If
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
                    Me.BarraBotones.SetDocuments(_settingPayments.Id)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
                End Using
            Else
                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                BarraBotones.PrepareToolbar(eAction.OnlySave)
                CleanControls()
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
            End If
        End Using
        INDsleVoucherCxP.Focus()
    End Sub

    ''' <summary>
    ''' Validates the controls.
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateFields() As Boolean
        If ListAges Is Nothing OrElse ListAges.Count = 0 Then
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CreateAgesPayments", NAME_MODULE)
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With _settingPayments
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .IdOperatingUnit = _idOperativeUnit
            .IdJournalVoucherAccountPayable = IdVoucherCxp
            .IdJournalVoucherTranslation = IdVoucherTransfers
            .IdJournalVoucherCreditNotes = IdVoucherCreditNotes
            .IdJournalVoucherDebitNotes = IdVoucherDebitNotes
            .IdJournalVocuherAmortization = IdVoucherAmortization

            .BudgetInterface = BudgetInterface
            .ObligationBudgetInterface = ObligationBudgetInterface
            .ObligationDebitValue = ObligationDebitValue
            .OnlyObligation = OnlyObligation
            .PostulateBudgetInterfaceBy = PostulateBudgetInterfaceBy

            .NameMaximumAgeRange = NameMaximumAgeRange
            .NameMinimumAgeRange = NameMinimumAgeRange
            .MaximunAgeRange = ListAges.ElementAt(ListAges.Count - 1).EndRange
            Select Case Status
                Case CBool(eActionsStatusRecords.Active)
                    .State = True
                Case CBool(eActionsStatusRecords.Inactive)
                    .State = False
            End Select

            For Each item In ListAges
                .AgesPayments.Add(item)
            Next
            If ListDeleteAges IsNot Nothing AndAlso ListDeleteAges.Count > 0 Then
                For Each item In ListDeleteAges
                    .AgesPayments.Add(item.MarkAsDeleted())
                Next
            End If
            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' Metodo que agrega un rango a la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddRange()
        Dim errors = ValidateControlsPopup()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If
        If editPopup = False Then
            ages = New AgesPayments
        End If
        With ages
            .Name = INDtxtName.Text
            .InitialRange = INDseInitialRange.EditValue
            .EndRange = INDseEndRange.EditValue
            .Color = INDcpeColor.Color.ToArgb()
        End With
        If ListAges Is Nothing Then
            ListAges = New List(Of AgesPayments)
        End If
        If editPopup = False Then
            ListAges.Add(ages)
        Else
            Dim index = ListAges.IndexOf(ages)
            For i = index + 1 To ListAges.Count - 1 Step 1
                Dim diference = ListAges.ElementAt(i).EndRange - ListAges.ElementAt(i).InitialRange
                If i = index + 1 Then
                    ListAges.ElementAt(i).InitialRange = ages.EndRange + 1
                Else
                    ListAges.ElementAt(i).InitialRange = ListAges.ElementAt(i - 1).EndRange + 1
                End If
                ListAges.ElementAt(i).EndRange = ListAges.ElementAt(i).InitialRange + diference
            Next
        End If
        INDgcAges.DataSource = Nothing
        INDgcAges.DataSource = ListAges
        INDLciNameMaximumAgeRange.Text = String.Format(INDLciNameMaximumAgeRange.Tag, ages.EndRange.ToString())
        If ages.EndRange = 999 Then
            INDpceAges.ClosePopup()
        Else
            CleanControlsPopup()
            INDtxtName.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que elimina un rango
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteRange(agesP As AgesPayments)
        If agesP.Id > 0 Then
            If ListDeleteAges Is Nothing Then
                ListDeleteAges = New List(Of AgesPayments)
            End If
            ListDeleteAges.Add(agesP)
        End If
        Dim index = ListAges.IndexOf(agesP)
        ListAges.Remove(agesP)
        If ListAges.Count > 1 Then
            For i = index To ListAges.Count - 1 Step 1
                If i = index Then
                    ListAges.ElementAt(i).InitialRange = agesP.InitialRange
                Else
                    Dim diference = ListAges.ElementAt(i).EndRange - ListAges.ElementAt(i).InitialRange
                    ListAges.ElementAt(i).InitialRange = ListAges.ElementAt(i - 1).EndRange + 1
                    ListAges.ElementAt(i).EndRange = ListAges.ElementAt(i).InitialRange + diference
                End If
            Next
        ElseIf ListAges.Count = 1 Then
            ListAges.ElementAt(0).InitialRange = 1
        End If
        INDgcAges.DataSource = Nothing
        INDgcAges.DataSource = ListAges
        CleanControlsPopup()
    End Sub

    ''' <summary>
    ''' Valida los controles del popup
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopup() As String
        Dim errors As New StringBuilder
        If INDtxtName.Text = String.Empty Then
            errors.AppendLine(INDlyItemName.CustomizationFormText + ResourceManager.GetString("Empty"))
        End If
        If INDseEndRange.EditValue = 0 Then
            errors.AppendLine(INDlyItemEndRange.CustomizationFormText + ResourceManager.GetString("Empty"))
        End If
        If INDcpeColor.Color.ToArgb() = 0 Then
            errors.AppendLine(INDlyItemColor.CustomizationFormText + ResourceManager.GetString("Empty"))
        End If
        If INDseInitialRange.EditValue > INDseEndRange.EditValue Then
            errors.AppendLine(String.Format(ResourceManager.GetString("Range", "Portfolio"), INDlyItemInitialRange.CustomizationFormText, INDlyItemEndRange.CustomizationFormText))
        End If
        Return errors.ToString()
    End Function

#End Region

#Region "Barra Botones"

    ''' <summary>
    ''' Barra Botones: Activa o desactiva el estado
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Await ChangeState()
    End Sub

    ''' <summary>
    ''' Barra botones: cambia la unidad operativa
    ''' </summary>
    ''' <param name="operatingUnit"></param>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            BarraBotones.StatusRecordVisible = False
            DeleteBlockedRecord()
            CleanControls()
            LoadControls()
            If _settingPayments IsNot Nothing AndAlso _settingPayments.Id > 0 Then
                _settingPayments.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
            End If
        End If
    End Sub

    ''' <summary>
    ''' Barra botones: Guardar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barra botones: Actualizar
    ''' </summary>
    ''' <param name="operatingUnit"></param>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Load de la barra de botones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

#End Region

End Class