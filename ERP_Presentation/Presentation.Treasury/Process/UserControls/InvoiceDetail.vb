'***********************************************************************
' Assembly         : Presentacion.Treasury
' Author           : Diego Andr�s Rold�n
' Created          : 21-08-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports DevExpress.XtraGrid.Views.Grid
Imports Presentation.Controls
Imports DevExpress.XtraGrid
Imports Presentation.Treasury.MVP
Imports Domain.Entities
Imports System.ComponentModel
Imports Presentation.Payments.MVP
Imports DevExpress.XtraPivotGrid
Imports Presentation.Controls.MVP
Imports DevExpress.Data.Linq
Imports Presentation.Portfolio.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports System.Text
Imports DevExpress.Utils.Menu
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Entities.Service
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.TreasuryRepository

#End Region

Public Class InvoiceDetail
    Implements ICustomizableForm

    Public Sub New()
        ' Llamada necesaria para el dise�ador.
        InitializeComponent()
        ' Agregue cualquier inicializaci�n despu�s de la llamada a InitializeComponent().
        _ctrValues = New CtrValues()
        _ctrValues.Dock = DockStyle.None
        AdditionalControlPanel.Controls.Add(_ctrValues)
    End Sub

    ''' <summary>
    ''' Evento que se activa al dar aceptar
    ''' </summary>
    Public Event UpdateDatasourceInvoice(sender As Object, e As UpdateDatasourceEventArgs)

    ''' <summary>
    ''' Listado que llega para cargar el datasource de la rejilla
    ''' </summary>
    Dim _listDatasource As List(Of SP_SchedulePayment_Result)

    ''' <summary>
    ''' Tupla que acumula los ids de las facturas cuando se les haya pedido consentimiento de cambiar valor descuento para que no lo vuelva a pedir
    ''' </summary>
    Dim flagTuple As New List(Of Tuple(Of String))

    ''' <summary>
    ''' Obtiene o establece el datasource del detalle de las facturas
    ''' </summary>
    ''' <value>
    ''' The detail invoice datasource.
    ''' </value>
    Property DetailInvoiceDatasource As DevExpress.XtraPivotGrid.PivotDrillDownDataSource

    ''' <summary>
    ''' variable que contiene el valor de los anticipos hechos al tercero
    ''' </summary>
    Private _advanceValue As Decimal

    ''' <summary>
    ''' Obtiene o establece si se muestra el menu contextual o no
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [show menu context]; otherwise, <c>false</c>.
    ''' </value>
    WriteOnly Property ShowMenuContext As Boolean
        Set(value As Boolean)
            If value Then
                AddHandler INDgvInvoiceDetail.PopupMenuShowing, AddressOf INDgvInvoiceDetail_PopupMenuShowing
            End If
        End Set
    End Property

    ''' <summary>
    ''' Establece si los datos son de solo visualizaci�n o no (para mostrar solo datos pagados  en dispersion de fondos)
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [only view]; otherwise, <c>false</c>.
    ''' </value>
    Property OnlyView As Boolean

    ''' <summary>
    ''' bandera que permite modificar la dispersi�n de fondos
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [allow modify]; otherwise, <c>false</c>.
    ''' </value>
    Property AllowModify As Boolean

    ''' <summary>
    ''' Bandera para saber si el que abre el popup es desde dispersion de fondo
    ''' </summary>
    ''' <returns></returns>
    Property FlagDispersion As Boolean = False

    ''' <summary>
    ''' Contador interno para conocer la posicion en la que se agrega la factura para tener su orden
    ''' </summary>
    ''' <returns></returns>
    Property CountAddInvoice As Integer

#Region "Properties and Variables"

    ''' <summary>
    ''' Control de usuario que muestra el anticipo que se le ha hecho al tercero
    ''' </summary>
    Dim _ctrValues As CtrValues

    Public Property PaymentsSettingPaymentsXpo As PaymentsSettingPaymentsXpo

    ''' <summary>
    ''' Obtiene o establece el GridView del detalle de las facturas
    ''' </summary>
    ''' <value>
    ''' The grid view invoice.
    ''' </value>
    Public Property GridControlInvoice As GridControl
        Get
            Return INDgcInvoiceDetail
        End Get
        Set(value As GridControl)
            INDgcInvoiceDetail = value
        End Set
    End Property

    Public ReadOnly Property BudgetInterface As Boolean
        Get
            Return If(PaymentsSettingPaymentsXpo Is Nothing, False, PaymentsSettingPaymentsXpo.BudgetInterface)
        End Get
    End Property

    ''' <summary>
    ''' propiedad privada que obtiene o establece el nombre y nit del proveedor
    ''' </summary>
    ''' <returns></returns>
    Private Property SupplierNitName As String
        Get
            Return INDLbSupplierNitName.Text
        End Get
        Set(value As String)
            INDLbSupplierNitName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' variable que almacena el Id del proveedor cuando todas las facturas
    ''' son del mismo proveedor
    ''' </summary>
    Private _supplierId As Integer?
    Private Property SupplierId As Integer?
        Get
            Return _supplierId
        End Get
        Set(value As Integer?)
            _supplierId = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establece el Id de la cuenta bancaria del proveedor
    ''' </summary>
    ''' <returns></returns>
    Private Property SupplierBankAccountId As Integer?
        Get
            Return INDsleSupplierBankAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleSupplierBankAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establece el datasource de las cuentas bancarias por proveedor
    ''' </summary>
    ''' <returns></returns>
    Private Property SupplierAccountXpo As List(Of SupplierBankAccountXpo)
        Get
            Return TryCast(INDsleSupplierBankAccount.Properties.DataSource, List(Of SupplierBankAccountXpo))
        End Get
        Set(value As List(Of SupplierBankAccountXpo))
            INDsleSupplierBankAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' lista de informacion de pago por proveedor
    ''' </summary>
    Private _listSchedulePaymentBankAccount As TrackableCollection(Of SchedulePaymentBankAccount)
    Public Property ListSchedulePaymentBankAccount As TrackableCollection(Of SchedulePaymentBankAccount)
        Get
            Return _listSchedulePaymentBankAccount
        End Get
        Set(value As TrackableCollection(Of SchedulePaymentBankAccount))
            _listSchedulePaymentBankAccount = value
        End Set
    End Property
#End Region

#Region "Events"

#Region "Load"
    ''' <summary>
    ''' Handles the Load event of the InvoiceDetail control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub InvoiceDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'layout
        IndigoGridControl1.RefreshGrid(INDgcInvoiceDetail)
        Me.BarraBotones.PrepareToolbar(eAction.None)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Copiar) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Cortar) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Pegar) = True
        'Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.) = False
        BarraBotones.StatusRecordVisible = True
        BarraBotones.OperatingUnitVisible = False
        colBudgetInterface.Visible = BudgetInterface
        colBudgetInterface.OptionsColumn.ShowInCustomizationForm = BudgetInterface

        If Not DetailInvoiceDatasource.RowCount > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Error, el valor a pagar debe ser menor o igual al saldo"
            Me.Close()
            Exit Sub
        End If

        LoadInvoiceDetail()
        LoadPaymentConcept()

        If Not AllowModify OrElse FlagDispersion Then
            INDcolPaymentValue.OptionsColumn.AllowEdit = False
            INDcolPaymentValue.OptionsColumn.AllowFocus = False
            INDcolPaymentValue.OptionsColumn.AllowMove = False
            INDcolPaymentConcept.OptionsColumn.AllowEdit = False
            INDcolPaymentConcept.OptionsColumn.AllowFocus = False
            INDcolPaymentConcept.OptionsColumn.AllowMove = False
            INDColDiscountRate.OptionsColumn.AllowEdit = False
            INDColDiscountRate.OptionsColumn.AllowFocus = False
            INDColDiscountRate.OptionsColumn.AllowMove = False
        End If
        INDsleSupplierBankAccount.ReadOnly = OnlyView
    End Sub
#End Region

#Region "Leave"
    ''' <summary>
    ''' Handles the Leave event of the RepositoryItemTextPayValue control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub RepositoryItemTextPayValue_Leave(sender As Object, e As EventArgs) Handles RepositoryItemTextPayValue.Leave
        Dim invoice As SP_SchedulePayment_Result = CType(INDgvInvoiceDetail.GetFocusedRow(), SP_SchedulePayment_Result)
        If invoice IsNot Nothing Then
            If (invoice.BalanceShare - invoice.DiscountValue) <= invoice.PayValue Then
                invoice.PayValue = invoice.BalanceShare - invoice.DiscountValue
                invoice.PaymentPercent = 100.0F
                invoice.ApplyDiscountValue = invoice.DiscountValue
                INDgvInvoiceDetail.RefreshData()
            Else
                If invoice.BalanceShare <> 0 Then
                    Dim ValueBalance As Decimal = invoice.BalanceShare
                    invoice.PaymentPercent = Math.Round(invoice.PayValue * 100 / ValueBalance, 2)
                    invoice.ApplyDiscountValue = IIf(invoice.PaymentPercent = 100, invoice.DiscountValue, CType(0, Decimal))
                    If invoice.DiscountValue <> 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = "Si no se paga el 100% de la CxP, No aplica descuento"
                    End If
                Else
                    invoice.PaymentPercent = 0
                End If
            End If

            If BudgetInterface Then
                If invoice.SchedulePaymentDetailBudget IsNot Nothing AndAlso invoice.SchedulePaymentDetailBudget.Any Then
                    For Each detail In invoice.SchedulePaymentDetailBudget
                        Dim value As Decimal = Math.Round(detail.Balance * invoice.PaymentPercent / 100, 0)
                        detail.Value = IIf(value > detail.Balance, detail.Balance, value)
                    Next
                End If
            End If
        End If
        LoadDataSupplier()
    End Sub

#End Region

#Region "DatasourceChange"
    ''' <summary>
    ''' Evento datasourceChange que muestra u oculta el footer
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDgcInvoiceDetail_DataSourceChanged(sender As Object, e As EventArgs) Handles INDgcInvoiceDetail.DataSourceChanged
        If CType(INDgcInvoiceDetail.DataSource, IList).Count > 0 Then
            INDgvInvoiceDetail.OptionsView.ShowFooter = True
        Else
            INDgvInvoiceDetail.OptionsView.ShowFooter = False
        End If
    End Sub
#End Region

#Region "KeyDown"
    ''' <summary>
    ''' Handles the KeyDown event of the InvoiceDetail control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Sub InvoiceDetail_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' Evento que se dispara al desplegarse el popup de presupuesto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrptPceBudgetInterface_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDrptPceBudgetInterface.QueryPopUp
        INDrptPceBudgetInterface.PopupControl = INDPccBudgetInterface
        If BudgetInterface Then
            Dim SP_SchedulePayment_Result = CType(INDgvInvoiceDetail.GetFocusedRow, SP_SchedulePayment_Result)
            INDgcBudgetInterface.DataSource = SP_SchedulePayment_Result.SchedulePaymentDetailBudget
        End If
    End Sub

    ''' <summary>
    ''' evento que consulta las cuentas bancarias por proveedor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDsleSupplierBankAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleSupplierBankAccount.QueryPopUp
        Await GetSupplierBankAccountDataSource(Me.SupplierId)
    End Sub

    ''' <summary>
    ''' metodo encargador de listar el datasource del combo y si no tiene guardado una cuenta establecer la por defecto
    ''' </summary>
    ''' <param name="supplierId"></param>
    ''' <returns></returns>
    Private Async Function GetSupplierBankAccountDataSource(supplierId As Integer?) As Task
        If supplierId Is Nothing Then
            Me.SupplierAccountXpo = Nothing
            Return
        End If

        If SupplierAccountXpo Is Nothing Then
            Using Model As New MSchedulePayment(Me.Tag)
                Dim allAccounts = Await Model.SupplierBankAccountDataSource(supplierId)
                Me.SupplierAccountXpo = allAccounts.Where(Function(x) x.State = 1).ToList()
            End Using
        End If

        If Me.SupplierBankAccountId Is Nothing AndAlso Me.SupplierAccountXpo.Any() Then
            Dim defaultAccount = Me.SupplierAccountXpo.Find(Function(x) x.PaymentDefault)
            If defaultAccount IsNot Nothing Then
                SupplierBankAccountId = defaultAccount.Id
            End If
        End If
    End Function

#End Region

#Region "EditValueChanging"

    Private Sub INDrptTxtValue_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrptTxtValue.EditValueChanging
        If e.NewValue = String.Empty Then
            Exit Sub
        End If

        Dim detail = DirectCast(INDgvBudgetInterface.GetFocusedRow, SchedulePaymentDetailBudget)
        If e.NewValue < 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "El valor no puede ser mayor al saldo"
            e.Cancel = True
        ElseIf e.NewValue > detail.Balance Then
            Mensaje(EeventViewerImages.Advertencia) = "El valor no puede ser mayor al saldo"
            e.Cancel = True
        End If
    End Sub

    ''' <summary>
    ''' evento del spinedit para recalcular el valor del descuento y si tiene pago el valor del mismo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRISEDiscountRate_EditValueChanged(sender As Object, e As EventArgs) Handles INDRISEDiscountRate.EditValueChanged
        Try
            Dim invoice As SP_SchedulePayment_Result = CType(INDgvInvoiceDetail.GetFocusedRow(), SP_SchedulePayment_Result)
            Dim NewValue = CType(sender, DevExpress.XtraEditors.SpinEdit)

            If NewValue.EditValue = invoice.DiscountRate Then
                NewValue.EditValue = invoice.DiscountRate
                Exit Sub
            End If

            ''validacion en caso de que del valor base sea cero o nothing
            If invoice.BaseValue Is Nothing Or invoice.BaseValue = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No se puede editar el porcentaje de descuento"
                NewValue.EditValue = invoice.DiscountRate
                Exit Sub
            End If

            '' validacion para que solo pregunte 1 vez la verificacion de cambio de valor
            Dim invoiceItemTuple As Tuple(Of String) = New Tuple(Of String)(invoice.Invoice)
            If Not flagTuple.Contains(invoiceItemTuple) Then
                If MessageIndigo.Show("�Est� seguro que desea cambiar el porcentaje del descuento (solo se aplica para porcentajes de pago del 100%)?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                    NewValue.EditValue = invoice.DiscountRate
                    Exit Sub
                Else
                    flagTuple.Add(New Tuple(Of String)(invoice.Invoice))
                End If
            End If
            Dim NewDiscountValue = Math.Round(CType((invoice.BaseValue * NewValue.EditValue) / 100, Decimal), 2, MidpointRounding.AwayFromZero)

            If invoice.PayValue = 0 Then
                invoice.DiscountValue = NewDiscountValue
                invoice.DiscountRate = CType(NewValue.EditValue, Decimal)
                INDgcInvoiceDetail.RefreshDataSource()
                Exit Sub
            End If
            If invoice.PaymentPercent = 100 Then
                invoice.DiscountValue = NewDiscountValue
                invoice.ApplyDiscountValue = NewDiscountValue
                invoice.PayValue = invoice.BalanceShare - NewDiscountValue
                invoice.DiscountRate = CType(NewValue.EditValue, Decimal)
                INDgcInvoiceDetail.RefreshDataSource()
            Else
                Mensaje(EeventViewerImages.Advertencia) = $"El porcentaje del pago es menor al 100%"
                NewValue.EditValue = invoice.DiscountRate
            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = ex.Message
        Finally
            LoadDataSupplier()
        End Try

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleSupplierBankAccount_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleSupplierBankAccount.EditValueChanged
        If Me.SupplierBankAccountId Is Nothing Then
            Exit Sub
        End If
        Dim schedulePaymentBankAccount = New SchedulePaymentBankAccount
        If Me.ListSchedulePaymentBankAccount?.Any(Function(x) x.SupplierId = Me.SupplierId) Then
            schedulePaymentBankAccount = Me.ListSchedulePaymentBankAccount?.FirstOrDefault(Function(x) x.SupplierId = Me.SupplierId)
            schedulePaymentBankAccount.SupplierBankAccountId = Me.SupplierBankAccountId
        Else
            With schedulePaymentBankAccount
                .SupplierId = Me.SupplierId
                .SupplierBankAccountId = Me.SupplierBankAccountId
            End With
            ListSchedulePaymentBankAccount.Add(schedulePaymentBankAccount)
        End If
    End Sub


    ''' <summary>
    ''' evento del valor del descuento cuando es cambiado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub RepositoryItemTextDiscountValue_EditValueChanged(sender As Object, e As EventArgs) Handles RepositoryItemTextDiscountValue.EditValueChanged
        Try
            Dim invoice As SP_SchedulePayment_Result = CType(INDgvInvoiceDetail.GetFocusedRow(), SP_SchedulePayment_Result)
            Dim NewValue = CType(sender, DevExpress.XtraEditors.TextEdit)

            If NewValue.EditValue = invoice.DiscountValue Then
                NewValue.EditValue = invoice.DiscountValue
                Exit Sub
            End If

            ''validacion en caso de que del valor base sea cero o nothing
            If invoice.BaseValue Is Nothing Or invoice.BaseValue = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No se puede editar el valor de descuento"
                NewValue.EditValue = invoice.DiscountValue
                Exit Sub
            End If

            If NewValue.EditValue > invoice.BaseValue Then
                Mensaje(EeventViewerImages.Advertencia) = "El valor del descuento no puede ser mayor al valor base"
                NewValue.EditValue = invoice.DiscountValue
                Exit Sub
            End If

            Dim invoiceItemTuple As Tuple(Of String) = New Tuple(Of String)(invoice.Invoice)
            If Not flagTuple.Contains(invoiceItemTuple) Then
                If MessageIndigo.Show("�Est� seguro que desea cambiar el valor del descuento (solo se aplica para porcentajes de pago del 100%)?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                    NewValue.EditValue = invoice.DiscountValue
                    Exit Sub
                Else
                    flagTuple.Add(New Tuple(Of String)(invoice.Invoice))
                End If
            End If
            Dim NewDiscountRate = CType((100 * NewValue.EditValue) / invoice.BaseValue, Decimal)

            If invoice.PayValue = 0 Then
                invoice.DiscountValue = CType(NewValue.EditValue, Decimal)
                invoice.DiscountRate = NewDiscountRate
                INDgcInvoiceDetail.RefreshDataSource()
                Exit Sub
            End If
            If invoice.PaymentPercent = 100 Then
                invoice.DiscountValue = CType(NewValue.EditValue, Decimal)
                invoice.ApplyDiscountValue = CType(NewValue.EditValue, Decimal)
                invoice.PayValue = invoice.BalanceShare - CType(NewValue.EditValue, Decimal)
                invoice.DiscountRate = NewDiscountRate
                INDgcInvoiceDetail.RefreshDataSource()
            Else
                Mensaje(EeventViewerImages.Advertencia) = $"El porcentaje del pago es menor al 100%"
                NewValue.EditValue = invoice.DiscountRate
            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = ex.Message
        Finally
            LoadDataSupplier()
        End Try
    End Sub
#End Region

#Region "Click"
    ''' <summary>
    ''' Handles the Click event of the INDsbAccept control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsbAccept_Click(sender As Object, e As EventArgs) Handles INDsbAccept.Click
        If ValidateGridFields() Then
            Dim updateEvent As New UpdateDatasourceEventArgs()
            updateEvent.NewInvoiceShareDatasource = CType(INDgcInvoiceDetail.DataSource, List(Of SP_SchedulePayment_Result))
            updateEvent.CurrentInvoiceShareDatasource = _listDatasource
            RaiseEvent UpdateDatasourceInvoice(Me, updateEvent)
            ForceClose = True
            Me.Close()
        End If
    End Sub
#End Region

    Private ForceClose As Boolean = False

#Region "Closing"
    ''' <summary>
    ''' Handles the FormClosing event of the InvoiceDetail control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub InvoiceDetail_FormClosing(sender As Object, e As FormClosingEventArgs, Optional Force As Boolean = False) Handles MyBase.FormClosing
        If Not ForceClose AndAlso Not OnlyView Then
            If Not MessageIndigo.Show(ResourceManager.GetString("CloseForm", "Payments"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                e.Cancel = True
            End If
        End If
    End Sub
#End Region

#End Region

#Region "BarButtonEvents"

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer

    End Sub

#End Region

#Region "Methods and Functions"

    ''' <summary>
    ''' Lee los datos del proveedor
    ''' </summary>
    Private Sub LoadDataSupplier()
        If _listDatasource IsNot Nothing AndAlso _listDatasource.Any Then
            Dim obj = _listDatasource.FirstOrDefault()
            Dim allSupplier As Boolean = _listDatasource.All(Function(x) x.SupplierName = obj.SupplierName)
            _ctrValues.BeginInvoke(Sub()
                                       _ctrValues.SetTotalValues(AddressOf ReturnValues)
                                       _ctrValues.INDLbTitle.Text = obj.SupplierName
                                       _ctrValues.INDLciTitle.HideControl(Not allSupplier)
                                       _ctrValues.RefreshTotalValues()
                                   End Sub)
            Me.INDLcgGeneralData.HideControl(Not allSupplier)
            Me.SupplierNitName = $"{obj.SupplierNit} - {obj.SupplierName}"

            If allSupplier Then
                Me.SupplierId = obj.SupplierId
                Me.SupplierBankAccountId = Me.ListSchedulePaymentBankAccount?.FirstOrDefault(Function(x) x.SupplierId = Me.SupplierId)?.SupplierBankAccountId
            Else
                Me.SupplierId = Nothing
                Me.SupplierBankAccountId = Nothing
            End If

            GetSupplierBankAccountDataSource(Me.SupplierId)
        End If
    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
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
    ''' carga los detalles de las facturas en la rejilla
    ''' </summary>
    Private Sub LoadInvoiceDetail()
        INDgvInvoiceDetail.ShowLoadingPanel()
        System.Threading.Tasks.Task.Factory.StartNew(Sub()
                                                         Try
                                                             _listDatasource = New List(Of SP_SchedulePayment_Result)()
                                                             For i As Integer = 0 To DetailInvoiceDatasource.RowCount - 1 Step 1
                                                                 If Convert.ToInt32(DetailInvoiceDatasource(i)("PayValue") <> 0 OrElse Not OnlyView) Then
                                                                     Dim obj As New SP_SchedulePayment_Result()
                                                                     With obj
                                                                         obj.SupplierId = DetailInvoiceDatasource(i)("SupplierId")
                                                                         obj.ThirdId = DetailInvoiceDatasource(i)("ThirdId")
                                                                         obj.SupplierName = DetailInvoiceDatasource(i)("SupplierName")
                                                                         obj.SupplierCode = DetailInvoiceDatasource(i)("SupplierCode")
                                                                         obj.SupplierNit = DetailInvoiceDatasource(i)("SupplierNit")
                                                                         obj.DescriptionLine = DetailInvoiceDatasource(i)("DescriptionLine")
                                                                         obj.DistributionLineId = DetailInvoiceDatasource(i)("DistributionLineId")
                                                                         obj.ExpenseConceptIdDistributionLine = DetailInvoiceDatasource(i)("ExpenseConceptIdDistributionLine")
                                                                         obj.MainAccountIdDistributionLine = DetailInvoiceDatasource(i)("MainAccountIdDistributionLine")
                                                                         obj.NatureExpenseConcept = DetailInvoiceDatasource(i)("NatureExpenseConcept")
                                                                         obj.Invoice = DetailInvoiceDatasource(i)("Invoice")
                                                                         obj.ExpirationDate = DetailInvoiceDatasource(i)("ExpirationDate")
                                                                         obj.AgePayment = DetailInvoiceDatasource(i)("AgePayment")
                                                                         obj.InvoiceBalance = DetailInvoiceDatasource(i)("InvoiceBalance")
                                                                         obj.Share = DetailInvoiceDatasource(i)("Share")
                                                                         obj.ShareExpirationDate = DetailInvoiceDatasource(i)("ShareExpirationDate")
                                                                         obj.BalanceShare = DetailInvoiceDatasource(i)("BalanceShare")
                                                                         obj.PayValue = DetailInvoiceDatasource(i)("PayValue")
                                                                         obj.PaymentConceptId = DetailInvoiceDatasource(i)("PaymentConceptId")
                                                                         obj.AccountPayableId = DetailInvoiceDatasource(i)("AccountPayableId")
                                                                         obj.SupplierTypeId = DetailInvoiceDatasource(i)("SupplierTypeId")
                                                                         obj.AccountPayableShareId = DetailInvoiceDatasource(i)("AccountPayableShareId")
                                                                         obj.SchedulePaymentDetailId = DetailInvoiceDatasource(i)("SchedulePaymentDetailId")
                                                                         obj.PaymentPercent = DetailInvoiceDatasource(i)("PaymentPercent")
                                                                         obj.SchedulePaymentDetailBudget = DetailInvoiceDatasource(i)("SchedulePaymentDetailBudget")
                                                                         obj.CXPValue = DetailInvoiceDatasource(i)("CXPValue")
                                                                         obj.DiscountValue = DetailInvoiceDatasource(i)("DiscountValue")
                                                                         obj.BaseValue = DetailInvoiceDatasource(i)("BaseValue")
                                                                         obj.DiscountRate = DetailInvoiceDatasource(i)("DiscountRate")
                                                                         obj.CxPRadicateDate = DetailInvoiceDatasource(i)("CxPRadicateDate")
                                                                         obj.PaymentDate = DetailInvoiceDatasource(i)("PaymentDate")
                                                                         obj.ApplyDiscountValue = DetailInvoiceDatasource(i)("ApplyDiscountValue")
                                                                         obj.ValueNote = DetailInvoiceDatasource(i)("ValueNote")
                                                                         obj.AdjusmentValueToAffectBase = DetailInvoiceDatasource(i)("AdjusmentValueToAffectBase")
                                                                         obj.CurrencyAbbreviation = DetailInvoiceDatasource(i)("CurrencyAbbreviation")

                                                                     End With

                                                                     If BudgetInterface Then
                                                                         'Debo consultar los detalles que ya tenga relacionado
                                                                         GetObligationDetailsBySchedulePaymentId(obj)

                                                                         'Obtenemos los detalles presupuestales asociados a la cuenta por pagar
                                                                         GetObligationDetailsBySchedulePayment(obj)
                                                                     End If

                                                                     _listDatasource.Add(obj)
                                                                 End If
                                                             Next

                                                             INDgcInvoiceDetail.BeginInvoke(Sub()
                                                                                                INDgcInvoiceDetail.DataSource = _listDatasource.Select(Function(x) x.Clone()).Cast(Of SP_SchedulePayment_Result).ToList()
                                                                                                INDgvInvoiceDetail.HideLoadingPanel()
                                                                                                LoadDataSupplier()
                                                                                                ''se asigna el formato de la moneda
                                                                                                If _listDatasource.Count > 0 Then
                                                                                                    Dim currencyAbbr = _listDatasource.FirstOrDefault().CurrencyAbbreviation
                                                                                                    Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
                                                                                                    _culture.NumberFormat = currencyAbbr.GetNumberFormat
                                                                                                    _ctrValues._culture = _culture
                                                                                                    _ctrValues.RefreshTotalValues()
                                                                                                    INDLyBillDetail.Text = "Detalle Facturas (" + currencyAbbr + ")"
                                                                                                    RepositoryItemTextPayValue.Mask.Culture = _culture
                                                                                                    Me.INDcolBalanceShare = Window.Utils.FormatGrid(Me.INDcolBalanceShare, currencyAbbr)
                                                                                                    Me.INDcolPaymentValue = Window.Utils.FormatGrid(Me.INDcolPaymentValue, currencyAbbr)
                                                                                                    Me.ColCxPValue = Window.Utils.FormatGrid(Me.ColCxPValue, currencyAbbr)
                                                                                                    Me.INDcolDiscountValue = Window.Utils.FormatGrid(Me.INDcolDiscountValue, currencyAbbr)
                                                                                                    Me.INDcolDiscount = Window.Utils.FormatGrid(Me.INDcolDiscount, currencyAbbr)
                                                                                                End If

                                                                                            End Sub)

                                                         Catch ex As Exception
                                                             INDgcInvoiceDetail.SafeInvoke(Sub()
                                                                                               INDgvInvoiceDetail.HideLoadingPanel()
                                                                                               Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
                                                                                           End Sub)
                                                         End Try
                                                     End Sub)
    End Sub

    ''' <summary>
    ''' Consulta las obligaciones presupuestales asociadas a una cuenta por pagar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetObligationDetailsBySchedulePaymentId(SchedulePayment As SP_SchedulePayment_Result)
        If SchedulePayment.SchedulePaymentDetailBudget Is Nothing OrElse Not SchedulePayment.SchedulePaymentDetailBudget.Any() Then
            Using model As New MSchedulePayment(Me.Tag)
                Dim collectionSchedulePaymentDetailBudget = model.ListSchedulePaymentDetailBudgetBySchedulePaymentDetailId(SchedulePayment.SchedulePaymentDetailId)
                If collectionSchedulePaymentDetailBudget IsNot Nothing Then
                    For Each schedulePaymentDetailBudget In collectionSchedulePaymentDetailBudget
                        SchedulePayment.SchedulePaymentDetailBudget.Add(New Domain.Entities.SchedulePaymentDetailBudget With {
                        .Id = schedulePaymentDetailBudget.Id,
                        .ObligationDetailId = schedulePaymentDetailBudget.ObligationDetailId,
                        .Value = schedulePaymentDetailBudget.Value
                    })
                    Next
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Consulta las obligaciones presupuestales asociadas a una cuenta por pagar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetObligationDetailsBySchedulePayment(SchedulePayment As SP_SchedulePayment_Result)
        Using model As New MVoucherTransaction(Me.Tag)
            Dim collectionObligation = model.GetObligationDetails(SchedulePayment.AccountPayableId, If(AllowModify, 1, 2))
            If collectionObligation IsNot Nothing Then
                For Each detail In collectionObligation
                    Dim schedulePaymentDetailBudget = SchedulePayment.SchedulePaymentDetailBudget.FirstOrDefault(Function(d) d.ObligationDetailId = detail.Id)
                    If schedulePaymentDetailBudget Is Nothing Then
                        If Not AllowModify Then
                            Continue For
                        End If

                        schedulePaymentDetailBudget = New SchedulePaymentDetailBudget With {
                            .ObligationDetailId = detail.Id,
                            .Value = 0
                        }
                        SchedulePayment.SchedulePaymentDetailBudget.Add(schedulePaymentDetailBudget)
                    End If

                    schedulePaymentDetailBudget.CommitmentCode = detail.CommitmentCode
                    schedulePaymentDetailBudget.CommitmentDocument = detail.CommitmentDocument
                    schedulePaymentDetailBudget.CategoryCodeName = detail.CategoryCodeName
                    schedulePaymentDetailBudget.FinancialSourceCodeName = detail.FinancialSourceCodeName
                    schedulePaymentDetailBudget.RevenueTypeCodeName = detail.RevenueTypeCodeName
                    schedulePaymentDetailBudget.Balance = detail.Balance
                Next
            End If
        End Using
    End Sub

#End Region

    ''' <summary>
    ''' carga los conceptos de pago
    ''' </summary>
    Private Sub LoadPaymentConcept()
        Using ModelXpo As New MBusqueda
            Dim filter As Object() = {True}
            Dim _listPaymentConcept As LinqInstantFeedbackSource = ModelXpo.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAllPaymentConceptByState, filter)
            RepositoryItemSearchLookUpPaymentConcept.DataSource = _listPaymentConcept
        End Using
    End Sub

    ''' <summary>
    ''' valida los campos de la rejilla
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateGridFields() As Boolean
        Dim _errorList As New StringBuilder()
        If INDgcInvoiceDetail.DataSource IsNot Nothing Then
            Parallel.ForEach(CType(INDgcInvoiceDetail.DataSource, List(Of SP_SchedulePayment_Result)), Sub(Sp As SP_SchedulePayment_Result)
                                                                                                           If Sp.PaymentConceptId <> 0 AndAlso Sp.PayValue < 0 Then
                                                                                                               _errorList.AppendLine(String.Format("seleccione un valor a pagar para la factura {0} cuota {1}", Sp.Invoice, Sp.Share))
                                                                                                           ElseIf (Sp.PaymentConceptId = 0 AndAlso Sp.PayValue <> 0) Then
                                                                                                               _errorList.AppendLine(String.Format("seleccione un concepto de pago para la factura {0} cuota {1}", Sp.Invoice, Sp.Share))
                                                                                                           End If
                                                                                                       End Sub)
        End If
        If _errorList.Length <> 0 Then
            Mensaje(EeventViewerImages.Advertencia) = _errorList.ToString()
            Return False
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' evento que se ejecuta al dar clicl derecho sobre la rejilla
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Views.Grid.PopupMenuShowingEventArgs"/> instance containing the event data.</param>
    Private Sub INDgvInvoiceDetail_PopupMenuShowing(sender As Object, e As Views.Grid.PopupMenuShowingEventArgs)
        Dim view As GridView = CType(sender, GridView)
        If e.MenuType = DevExpress.XtraGrid.Views.Grid.GridMenuType.Row Then
            e.Menu.Items.Clear()
            Dim ItemMenuAllPay As DXMenuItem = New DXMenuItem("Pagar Todo", AddressOf ContexMenuActions_Click)
            Dim ItemMenuPercentPay As DXMenuItem = New DXMenuItem("Pagar Porcentaje", AddressOf ContexMenuActions_Click)
            ItemMenuAllPay.Tag = 1
            ItemMenuPercentPay.Tag = 2
            e.Menu.Items.Add(ItemMenuAllPay)
            e.Menu.Items.Add(ItemMenuPercentPay)
        End If
    End Sub

    ''' <summary>
    ''' Evento ejecutado al seleccionar una opci�n del menu contextual
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub ContexMenuActions_Click(sender As Object, e As EventArgs)
        Dim optionPay As DXMenuItem = CType(sender, DXMenuItem)
        ' Unir las dos opciones para generar un solo Using frmPercent

        Using frmPercent As New FrmPercentPayment
            frmPercent.StartPosition = FormStartPosition.CenterScreen
            AddHandler frmPercent.PaymentPercentEvent, AddressOf Handler_PayPercent
            If optionPay.Tag = 1 Then
                frmPercent.Title = "Pago Total"
                frmPercent.Type = 1 'Pago total

                ''cuando se seleccionan varias facturas utilizando click + control, se utiliza esta forma para recorrer cada una y asi poder
                ''asignarle el consecutivo de posicion de agregado para el reporte
                For Each rowHandle As Integer In INDgvInvoiceDetail.GetSelectedRows()

                    Dim data = TryCast(INDgvInvoiceDetail.GetRow(rowHandle), SP_SchedulePayment_Result)

                    data.PositionDetailsAdd = CountAddInvoice
                    frmPercent.PaymentConceptId = data.PaymentConceptId
                    CountAddInvoice += 1
                Next
            Else
                frmPercent.Title = "Porcentaje de Pago"
                frmPercent.Type = 2 'Pago porcentaje

                ''cuando se seleccionan varias facturas utilizando click + control, se utiliza esta forma para recorrer cada una y asi poder
                ''asignarle el consecutivo de posicion de agregado para el reporte
                For Each rowHandle As Integer In INDgvInvoiceDetail.GetSelectedRows()

                    Dim data = TryCast(INDgvInvoiceDetail.GetRow(rowHandle), SP_SchedulePayment_Result)

                    frmPercent.PaymentConceptId = data.PaymentConceptId
                    data.PositionDetailsAdd = CountAddInvoice

                    If data.BalanceShare <> 0 Then
                        Dim value As Decimal = data.BalanceShare
                        frmPercent.PaymentPercent = TreasuryStaticServices.ConvertInvoiceValueToPercent(data.PayValue, value)
                    Else
                        frmPercent.PaymentPercent = 0
                    End If

                    CountAddInvoice += 1
                Next

            End If
            Dim frmTransparent As New FrmTransparent(frmPercent, False)
            frmTransparent.ShowDialog()
        End Using
        INDgvInvoiceDetail.RefreshData()
        LoadDataSupplier()
    End Sub

    ''' <summary>
    ''' Controlador del evento para pagar porcentaje a las facturas
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="PayPercentEventArgs"/> instance containing the event data.</param>
    Private Sub Handler_PayPercent(sender As Object, e As PayPercentEventArgs)
        Parallel.For(0, INDgvInvoiceDetail.SelectedRowsCount(), Sub(i)
                                                                    If Not (INDgvInvoiceDetail.GetSelectedRows()(i) >= 0) Then
                                                                        Exit Sub
                                                                    End If

                                                                    'If (INDgvInvoiceDetail.GetSelectedRows()(i) >= 0) Then
                                                                    Dim _invoice As SP_SchedulePayment_Result = CType(INDgvInvoiceDetail.GetRow(INDgvInvoiceDetail.GetSelectedRows(i)), SP_SchedulePayment_Result)

                                                                    If e.Type = 1 OrElse (e.percentValue = 100.0F) Then
                                                                        _invoice.PayValue = (_invoice.BalanceShare - _invoice.DiscountValue)
                                                                        _invoice.PaymentPercent = 100.0F
                                                                        _invoice.ApplyDiscountValue = _invoice.DiscountValue
                                                                        _invoice.PaymentConceptId = e.PaymentConceptId
                                                                        Exit Sub
                                                                    End If

                                                                    _invoice.PayValue = _invoice.BalanceShare * (e.percentValue / 100)
                                                                    If _invoice.DiscountValue <> 0 Then
                                                                        Mensaje(EeventViewerImages.Advertencia) = "Si no se paga el 100% de la CxP, No aplica descuento"
                                                                    End If
                                                                    _invoice.PaymentPercent = e.percentValue
                                                                    _invoice.ApplyDiscountValue = IIf(e.percentValue = 100, _invoice.DiscountValue, CType(0, Decimal))
                                                                    _invoice.PaymentConceptId = e.PaymentConceptId
                                                                    ' End If
                                                                End Sub)
    End Sub

    ''' <summary>
    ''' funcion que actualiza los valores de los totales del control de la barra de botones
    ''' </summary>
    ''' <returns></returns>
    Private Function ReturnValues() As Tuple(Of Decimal, Decimal)
        Dim ValueSchedule As Decimal = 0
        Dim ValueDiscount As Decimal = 0
        If TryCast(INDgvInvoiceDetail.DataSource, List(Of SP_SchedulePayment_Result)) IsNot Nothing AndAlso TryCast(INDgvInvoiceDetail.DataSource, List(Of SP_SchedulePayment_Result)).ToList().Count > 0 Then

            Dim SumValueBalance = TryCast(INDgvInvoiceDetail.DataSource, List(Of SP_SchedulePayment_Result)).ToList().Where(Function(t) t.PayValue <> 0 AndAlso (t.CXPValue - t.ApplyDiscountValue - t.ValueNote) = (t.PayValue)).Select(Function(j) j.BalanceShare).Sum()
            Dim SumPayValue = TryCast(INDgvInvoiceDetail.DataSource, List(Of SP_SchedulePayment_Result)).ToList().Where(Function(t) t.PayValue <> 0 AndAlso (t.CXPValue - t.ApplyDiscountValue - t.ValueNote) <> (t.PayValue)).Select(Function(j) j.PayValue).Sum()

            ValueSchedule = SumValueBalance + SumPayValue
            ValueDiscount = TryCast(INDgvInvoiceDetail.DataSource, List(Of SP_SchedulePayment_Result)).ToList().Where(Function(t) t.ApplyDiscountValue <> 0 And (t.PayValue) = (t.CXPValue - t.ApplyDiscountValue - t.ValueNote)).Select(Function(j) j.ApplyDiscountValue).Sum()
        End If
        Return New Tuple(Of Decimal, Decimal)(ValueSchedule, ValueDiscount)
    End Function


End Class