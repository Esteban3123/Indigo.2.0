#Region "Imports"

Imports System.ComponentModel
Imports System.Text
Imports DevExpress.Spreadsheet
Imports DevExpress.Xpo
Imports DevExpress.XtraSpreadsheet
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.PortfolioRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Common
Imports Presentation.Common.MVP
Imports Presentation.Controls
Imports Presentation.Glosas
Imports Presentation.Glosas.MVP
Imports Presentation.Portfolio.MVP

#End Region

Public Class FrmPortfolioTransfers
    Implements IPortfolioTransfers, ICustomizableForm

#Region "Builder"

    Dim ctrAdvance As CtrAdvanceTreasury

    Public Sub New()
        InitializeComponent()
        ctrAdvance = New CtrAdvanceTreasury()
        ctrAdvance.Title = "Saldo Anticipo"
        ctrAdvance.WithEvent = False
        ctrAdvance.SetAdvance(AddressOf getAdvance)
        ctrAdvance.PrintAdvance()
        ctrAdvance.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrAdvance)
    End Sub

#End Region

#Region "Consts"

    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Portfolio"

#End Region

#Region "Variables"

    ''' <summary>
    ''' Presentador del frontal
    ''' </summary>
    Private _presenter As PPortfolioTransfers

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _operativeUnitId As Int32

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _currentSequenceId As Int64

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.PortfolioSequence

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _record As BlockRecordPortfolio

    ''' <summary>
    ''' representa la entidad de traslados
    ''' </summary>
    ''' <remarks></remarks>
    Private _portfolioTransfer As PortfolioTransfer

    ''' <summary>
    ''' representa la entidad del detalle del traslado
    ''' </summary>
    ''' <remarks></remarks>
    Private _portfolioTransferDetail As PortfolioTransferDetail

    ''' <summary>
    ''' listado del detalla del traslado
    ''' </summary>
    ''' <remarks></remarks>
    Private _listPortfolioTransferDetail As List(Of PortfolioTransferDetail)

    ''' <summary>
    ''' listado del detalla del traslado para eliminar
    ''' </summary>
    ''' <remarks></remarks>
    Private _listPortfolioTransferDetailDelete As List(Of PortfolioTransferDetail)

    ''' <summary>
    ''' representa la entidad de otros conceptos
    ''' </summary>
    ''' <remarks></remarks>
    Private _portfolioTransferOtherConcept As PortfolioTransferOtherConcept

    ''' <summary>
    ''' listado de otros conceptos
    ''' </summary>
    ''' <remarks></remarks>
    Private _listPortfolioTransferOtherConcept As List(Of PortfolioTransferOtherConcept)

    ''' <summary>
    ''' listado de otros conceptos para eliminar
    ''' </summary>
    ''' <remarks></remarks>
    Private _listPortfolioTransferOtherConceptDelete As List(Of PortfolioTransferOtherConcept)

    ''' <summary>
    ''' variable que contiene la entidad
    ''' </summary>
    Dim settingPortfolio As SettingPortfolio

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim _varImp As Integer

    ''' <summary>
    ''' ruta del archivo de excel
    ''' </summary>
    ''' <remarks></remarks>
    Dim _myStream As String = Nothing

    ''' <summary>
    ''' standart ISO4217
    ''' </summary>
    Private _currencyAbbreviation As String

    ''' <summary>
    ''' id de la moneda que viene en la factura
    ''' </summary>
    Private _currencyInvoiceId As Integer?

    ''' <summary>
    ''' Lista de la tasa de cambio
    ''' </summary>
    Private _listTRM As List(Of TRM)


#End Region

#Region "Properties"

    Public ReadOnly Property MyTag As Object Implements IPortfolioTransfers.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IPortfolioTransfers.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    Public WriteOnly Property ActionsOnControls As Boolean Implements IPortfolioTransfers.ActionsOnControls
        Set(value As Boolean)
            INDLcTransfers.BeginUpdate()

            INDBteCode.Enabled = Not value
            INDDteDate.Enabled = value
            INDsleClient.Enabled = value
            INDGleTransferType.Enabled = value
            INDSleAdvance.Enabled = value
            INDSleMainAccount.Enabled = value
            INDMeObservations.Enabled = value

            INDEsbBills.Enabled = False
            INDBtnImportFile.Enabled = False
            INDGcBills.Enabled = value

            INDPceOtherConcept.Enabled = value
            INDGcOtherConcept.Enabled = value

            INDLcTransfers.EndUpdate()
            If INDBteCode.Enabled = True Then
                INDBteCode.Focus()
            Else
                INDDteDate.Focus()
            End If
        End Set
    End Property

    Public Property Sequense As PortfolioSequence Implements IPortfolioTransfers.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As PortfolioSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.PortfolioSequenceDetail In Me._sequence.PortfolioSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' codigo de la entidad
    ''' </summary>
    Public Property Code As String Implements IPortfolioTransfers.Code
        Get
            If INDBteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
                Return String.Empty
            Else
                Return INDBteCode.Text
            End If
        End Get
        Set(value As String)
            INDBteCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' fecha del documento
    ''' </summary>
    Public Property DocumentDate As Date? Implements IPortfolioTransfers.DocumentDate
        Get
            Return INDDteDate.EditValue
        End Get
        Set(value As Date?)
            INDDteDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' id del cliente
    ''' </summary>
    Public Property CustomerId As Integer? Implements IPortfolioTransfers.CustomerId
        Get
            Return INDsleClient.EditValue
        End Get
        Set(value As Integer?)
            INDsleClient.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' id del anticipo
    ''' </summary>
    Public Property PortfolioAdvanceId As Integer? Implements IPortfolioTransfers.PortfolioAdvanceId
        Get
            Return INDSleAdvance.EditValue
        End Get
        Set(value As Integer?)
            INDSleAdvance.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' observaciones
    ''' </summary>
    Public Property Observations As String Implements IPortfolioTransfers.Observations
        Get
            Return INDMeObservations.Text
        End Get
        Set(value As String)
            INDMeObservations.Text = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad de la moneda del anticipo seleccionado
    ''' </summary>
    ''' <returns></returns>
    Public Property CurrencyId(Optional _currencyAbbreviation As String = Nothing) As Integer? Implements IPortfolioTransfers.CurrencyId
        Get
            Return INDSleCurrency.EditValue
        End Get
        Set(value As Integer?)
            INDSleCurrency.EditValue = value
            Me._setFormatGeneralControls(_currencyAbbreviation)
        End Set
    End Property

    ''' <summary>
    ''' codigo standart de la moneda ISO4217
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property CurrencyAbbreviation As String Implements IPortfolioTransfers.CurrencyAbbreviation
        Get
            Return _currencyAbbreviation
        End Get
    End Property

#Region "Popup Other Concepts"

    ''' <summary>
    ''' id de la cuenta contable
    ''' </summary>
    Public Property MainAccountId As Integer? Implements IPortfolioTransfers.MainAccountId
        Get
            Return INDSleMainAccount.EditValue
        End Get
        Set(value As Integer?)
            INDSleMainAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ThirdPartyId As Integer? Implements IPortfolioTransfers.ThirdPartyId
        Get
            Return INDsleThirdParty.EditValue
        End Get
        Set(value As Integer?)
            INDsleThirdParty.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' id del centro de costo
    ''' </summary>
    Public Property CostCenterId As Integer? Implements IPortfolioTransfers.CostCenterId
        Get
            Return INDSleCostCenter.EditValue
        End Get
        Set(value As Integer?)
            INDSleCostCenter.EditValue = value
        End Set
    End Property

#End Region

#Region "External Call"

    Public CallNote As Boolean = False

    Public ReadOnly Property GetCustomer As Integer?
        Get
            Return IIf(_portfolioTransfer IsNot Nothing, _portfolioTransfer.CustomerIdTemp, Nothing)
        End Get
    End Property

    Private _billValue As Decimal
    Private _advanceValue As Decimal
    Private _advanceBalance As Decimal
    Public Function getAdvance() As Decimal
        _billValue = 0
        _debitValue = 0
        _creditValue = 0

        If _listPortfolioTransferDetail IsNot Nothing AndAlso _listPortfolioTransferDetail.Count > 0 Then
            _billValue = _listPortfolioTransferDetail.Sum(Function(x) x.Value)
        End If

        If _listPortfolioTransferOtherConcept IsNot Nothing AndAlso _listPortfolioTransferOtherConcept.Count > 0 Then
            _debitValue = _listPortfolioTransferOtherConcept.Where(Function(x) x.Nature = 1).Sum(Function(z) z.Value)
            _creditValue = _listPortfolioTransferOtherConcept.Where(Function(x) x.Nature = 2).Sum(Function(z) z.Value)
        End If

        _advanceBalance = _advanceValue - _billValue - _creditValue + _debitValue
        _advanceBalance = If(_portfolioTransfer Is Nothing OrElse _portfolioTransfer.Status = 1, _advanceBalance, 0)

        If CallNote Then
            _creditValue = _billValue + _creditValue
            _debitValue = _creditValue
        End If

        If _advanceBalance < 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ZeroBalance", MODULE_NAME)
        End If

        Return _advanceBalance
    End Function

    Private _debitValue As Decimal
    Public ReadOnly Property GetDebit As Decimal
        Get
            Return _debitValue
        End Get
    End Property

    Private _creditValue As Decimal
    Public ReadOnly Property GetCredit As Decimal
        Get
            Return _creditValue
        End Get
    End Property

#End Region

#End Region

#Region "Datasources"

    Dim _listTransferType As List(Of Tuple(Of Byte, String))
    ReadOnly Property ListTransferType As List(Of Tuple(Of Byte, String))
        Get
            If _listTransferType Is Nothing Then
                _listTransferType = New List(Of Tuple(Of Byte, String))
                _listTransferType.Add(New Tuple(Of Byte, String)(1, "Mismo Cliente"))
                _listTransferType.Add(New Tuple(Of Byte, String)(2, "Diferente Cliente"))
            End If
            Return _listTransferType
        End Get
    End Property

    Public Property CustomerXPO As XPInstantFeedbackSource Implements IPortfolioTransfers.CustomerXPO
        Get
            Return CType(INDsleClient.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleClient.Properties.DataSource = value
        End Set
    End Property

    Public Property AdvanceXPO As XPInstantFeedbackSource Implements IPortfolioTransfers.AdvanceXPO
        Get
            Return CType(INDSleAdvance.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleAdvance.Properties.DataSource = value
        End Set
    End Property

    Public Property CostCenterXPO As XPInstantFeedbackSource Implements IPortfolioTransfers.CostCenterXPO
        Get
            Return CType(INDSleCostCenter.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleCostCenter.Properties.DataSource = value
        End Set
    End Property

    Public Property BillsXPO As XPInstantFeedbackSource Implements IPortfolioTransfers.BillsXPO
        Get
            Return CType(INDSleBill.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleBill.Properties.DataSource = value
        End Set
    End Property

#Region "Popup Other Concepts"

    Public Property NoteConceptXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IPortfolioTransfers.NoteConceptXpo
        Get
            Return CType(INDSleNoteConcept.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleNoteConcept.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el listado de cuentas xpo
    ''' </summary>
    Public Property AccountsXPO As XPInstantFeedbackSource Implements IPortfolioTransfers.AccountsXPO
        Get
            Return CType(INDSleMainAccountConcept.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleMainAccountConcept.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ThirdPartyXpo As XPInstantFeedbackSource Implements IPortfolioTransfers.ThirdPartyXpo
        Get
            Return INDsleThirdParty.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleThirdParty.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del centro de costo
    ''' </summary>
    ''' <returns></returns>
    Public Property CostCenterConceptXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements IPortfolioTransfers.CostCenterConceptXPO
        Get
            Return CType(INDSLeCostCenterConcept.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSLeCostCenterConcept.Properties.DataSource = value
        End Set
    End Property

#End Region

#End Region

#Region "ICrud Base"

    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = 100},
                              New ColumnInfo With {.Caption = "Tercero", .FieldName = "ThirdPartyId.NitName", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Fecha", .FieldName = "DocumentDate", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Código anticipo", .FieldName = "PortfolioAdvanceId.Code", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Moneda", .FieldName = "PortfolioAdvanceId.Abbreviation", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Valor", .FieldName = "ValueTotal", .ColumnWidth = 200, .ColumnFormatType = DevExpress.Utils.FormatType.Custom, .ColumnFormat = "N2"},
                              New ColumnInfo With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = 200}}.ToList()
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListPortfolioTransfers
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        Await DeleteBlockedRecord()
        Code = ReturnValue
        If Code <> String.Empty Then
            Await LoadControls()
            If INDBteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBteCode.Enabled = False
        End If
    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewPortfolioTransfers()
        End If
    End Sub

    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If Me._portfolioTransfer.Status <> 3 Then
            If ValidateControls() = False Then
                Exit Sub
            End If
            If INDGvBills.RowCount = 0 AndAlso INDGvOtherConcept.RowCount = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe agregar una factura o un concepto."
                Exit Sub
            End If
            If _advanceBalance < 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "El valor del documento debe ser mayor a $ 0"
                Exit Sub
            End If
            If _listPortfolioTransferOtherConcept IsNot Nothing AndAlso _listPortfolioTransferOtherConcept.Count > 0 AndAlso _listPortfolioTransferDetail IsNot Nothing AndAlso _listPortfolioTransferDetail.Count > 0 Then
                Dim debitCreditResult = _debitValue - _creditValue
                If debitCreditResult > 0 Then
                    If debitCreditResult > _billValue Then
                        Mensaje(EeventViewerImages.Advertencia) = "No se puede guardar porque el valor de otros conceptos es mayor al total de las facturas"
                        Exit Sub
                    End If
                End If
            End If
            AssigningValues()
        End If
        Try
            Await SavePortfolioTransfer()
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
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

#Region "Methods"

    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "4", .StatusName = ResourceManager.GetString("StatusReverse"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "-1", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.White})
        Me.BarraBotones.States = listStates
    End Sub

    Private Sub AddActionsColumns()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDGvBills, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvBills.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
        IndigoGridView2.SetListAcction(INDGvOtherConcept, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvOtherConcept.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
    End Sub

    Private Sub InitializeTuples()
        INDGleTransferType.Properties.DataSource = ListTransferType
    End Sub

    Private Async Sub CleanControls()
        INDLcTransfers.BeginUpdate()
        Await DeleteBlockedRecord()

        INDBteCode.Text = String.Empty
        INDDteDate.EditValue = Me.GetDateServer()
        INDsleClient.EditValue = Nothing
        INDsleClient.Properties.NullText = String.Empty
        INDGleTransferType.EditValue = 1
        INDSleAdvance.EditValue = Nothing
        INDSleAdvance.Properties.NullText = String.Empty
        INDSleMainAccount.EditValue = Nothing
        INDSleMainAccount.Properties.NullText = String.Empty
        INDSleCostCenter.EditValue = Nothing
        INDSleCostCenter.Properties.NullText = String.Empty
        INDLciCostCenter.HideControl(True)
        INDMeObservations.Text = Nothing
        Me.PortfolioAdvanceId = Nothing
        Me._currencyInvoiceId = Nothing
        Me._listTRM = Nothing
        Me.LoadDefaultCurrency()

        CleanControlsPopup()
        INDColBalance.Visible = True
        INDGcBills.DataSource = Nothing
        IndigoGridControl1.RefreshGrid(INDGcBills)

        INDPceBills.Enabled = False
        CleanControlsPopupOtherConcept()
        INDGcOtherConcept.DataSource = Nothing
        IndigoGridControl1.RefreshGrid(INDGcOtherConcept)

        Me._doc = Nothing
        Me._billValue = 0
        Me._advanceValue = 0
        Me._advanceBalance = 0
        Me._portfolioTransfer = Nothing
        Me._portfolioTransferDetail = Nothing
        Me._listPortfolioTransferDetail = Nothing
        Me._listPortfolioTransferDetailDelete = Nothing
        Me._portfolioTransferOtherConcept = Nothing
        Me._listPortfolioTransferOtherConcept = Nothing
        Me._listPortfolioTransferOtherConceptDelete = Nothing

        ReadOnlyControls(False)
        ActionsOnControls = False
        ctrAdvance.PrintAdvance()

        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me.BarraBotones.StatusRecordVisible = False

        INDLcTransfers.EndUpdate()

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    Private Sub CleanControlsPopup()
        _portfolioTransferDetail = Nothing
        INDSleBill.EditValue = Nothing
        INDSleBill.Properties.NullText = String.Empty
        INDDteBillDate.EditValue = Nothing
        INDDteExpiredDate.EditValue = Nothing
        INDTxtValue.EditValue = 0F
        INDTxtBalance.EditValue = 0F
        INDTxtTransferValue.EditValue = 0F
        INDTxtTransferValue.Enabled = False
        INDBtnAdd.Enabled = False
        Me.CurrencyExchangeActions(False)
    End Sub

    Private Sub CleanControlsPopupOtherConcept()
        _portfolioTransferOtherConcept = Nothing

        INDSleNoteConcept.EditValue = Nothing
        INDSleNoteConcept.Properties.NullText = String.Empty
        INDSleMainAccountConcept.EditValue = Nothing
        INDSleMainAccountConcept.Properties.NullText = String.Empty
        INDsleThirdParty.EditValue = Nothing
        INDsleThirdParty.Properties.NullText = String.Empty
        INDlyItemThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDSLeCostCenterConcept.EditValue = Nothing
        INDSLeCostCenterConcept.Properties.NullText = String.Empty
        INDLciCostCenterConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDGLeNatureConcept.EditValue = Nothing
        INDTxtValueOtherConcept.EditValue = 0
    End Sub

    Private Async Function NewPortfolioTransfers() As Task
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._currentSequenceId = Me._sequence.PortfolioSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.PortfolioSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._operativeUnitId) Then
                    Me._currentSequenceId = Me._sequence.PortfolioSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._operativeUnitId).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._currentSequenceId)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._currentSequenceId))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New Presentation.Portfolio.MVP.MBlockRecordAndSequense(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._currentSequenceId)) = Await model.GetNumericSequenseGroup(CInt(Me._currentSequenceId))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._currentSequenceId)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._currentSequenceId)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._currentSequenceId))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If

        BarraBotones.StatusRecord = "1"
        BarraBotones.StatusRecordVisible = True
        Me._portfolioTransfer = New PortfolioTransfer() With {.Status = 1}
    End Function

    Public Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MPortfolioTransfers(CStr(Me.Tag))
                    AsyncLoader(True)
                    INDLcTransfers.BeginUpdate()
                    _portfolioTransfer = Await Model.GetPortfolioTransfersByCode(INDBteCode.Text.Trim)
                    If _portfolioTransfer IsNot Nothing AndAlso _portfolioTransfer.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True
                        Using ModelRecord As New Presentation.Portfolio.MVP.MBlockRecordAndSequense(CStr(Me.Tag))
                            _record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_portfolioTransfer.Id))
                            With _portfolioTransfer
                                Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationUser"), .ConfirmationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationDate"), .ConfirmationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideUser"), .AnnulmentUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideDate"), .AnnulmentDate)

                                BarraBotones.OperatingUnitValue = .OperatingUnitId
                                Code = .Code
                                DocumentDate = .DocumentDate
                                CustomerId = .CustomerId
                                INDsleClient.Properties.NullText = .CodeNameCustomer
                                INDsleClient.Properties.ReadOnly = True
                                INDGleTransferType.EditValue = .TransferType
                                INDGleTransferType.Properties.ReadOnly = True
                                PortfolioAdvanceId = .PortfolioAdvanceId
                                INDSleAdvance.Properties.NullText = .CodeNameAdvance
                                INDSleAdvance.Properties.ReadOnly = True
                                MainAccountId = .MainAccountId
                                INDSleMainAccount.Properties.NullText = .CodeNameMainAccount
                                CostCenterId = .CostCenterId
                                INDSleCostCenter.Properties.NullText = .CodeNameCostCenter
                                Observations = .Observations
                                Me.BarraBotones.StatusRecord = .Status.ToString()
                                Me.BarraBotones.StatusRecordVisible = True

                                'consulto los detalles
                                LoadDetails(_portfolioTransfer.Id)

                                'consulto los otros conceptos
                                _listPortfolioTransferOtherConcept = Model.GetPortfolioTransferOtherConceptByIdPortfolioTransfer(_portfolioTransfer.Id)
                                INDGcOtherConcept.DataSource = Nothing
                                INDGcOtherConcept.DataSource = _listPortfolioTransferOtherConcept
                            End With
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._portfolioTransfer.Code)
                            If _record.Id = 0 Then
                                _record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordPortfolio With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _portfolioTransfer.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), _record.CodUser, _record.NameUser, _record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _record.CodUser)
                            End If

                            If _portfolioTransfer.Status = 1 Then
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                            Else
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                ReadOnlyControls(True)
                                INDColBalance.Visible = False
                            End If

                            Me.BarraBotones.SetDocuments(_portfolioTransfer.Id, Me.Tag.ToString(), Nothing, GetType(PortfolioTransfer).Name)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, _portfolioTransfer.Id, 0, _portfolioTransfer.Id)


                            ActionsOnControls = True
                            AsyncLoader(False)

                            If CallNote Then
                                INDBteCode.Enabled = False
                            End If
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewPortfolioTransfers()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                            Code = String.Empty
                            Deshacer()
                            INDBteCode.Focus()
                        End If
                    End If
                    INDLcTransfers.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBteCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    Private Sub LoadDetails(PortfolioTransferId As Integer)
        _listPortfolioTransferDetail = New List(Of PortfolioTransferDetail)

        Dim ListXpo = _presenter.ListPortfolioTransferDetailByPortfolioTransferId(PortfolioTransferId)
        If ListXpo IsNot Nothing AndAlso ListXpo.Count > 0 Then
            For Each itemXpo As ViewListPortfolioTransferDetailXpo In ListXpo
                Dim ptd As New PortfolioTransferDetail
                With ptd
                    .StartTracking()
                    .Id = itemXpo.PortfolioTransferDetailId
                    .PortfolioTrasferId = PortfolioTransferId
                    .AccountReceivableId = itemXpo.AccountReceivableId
                    .InvoiceNumber = itemXpo.InvoiceNumber
                    .MainAccountId = itemXpo.MainAccountId
                    .CodeNameMainAccount = itemXpo.CodeNameMainAccount
                    .Patient = itemXpo.Patient
                    If itemXpo.CostCenterId > 0 Then
                        .CostCenterId = itemXpo.CostCenterId
                    Else
                        .CostCenterId = Nothing
                    End If
                    .ValueBill = itemXpo.ValueBill
                    .Balance = itemXpo.Balance
                    .Value = itemXpo.Value
                    .PortfolioStatusName = itemXpo.PortfolioStatusName
                    .CurrencyAbbreviation = itemXpo.CurrencyAbbreviation
                    .MarkAsUnchanged()
                End With
                _listPortfolioTransferDetail.Add(ptd)
            Next
        End If

        INDGcBills.DataSource = Nothing
        INDGcBills.DataSource = _listPortfolioTransferDetail
    End Sub

    Private Function GenerateDoc() As IndexedDocument2
        Dim bills = String.Join("-", (From a In _portfolioTransfer.PortfolioTransferDetail Select a.InvoiceNumber).ToList.Distinct.ToList())
        Dim content As String = String.Format(ResourceManager.GetString("FrmPortfolioTransfers_IndexContent", MODULE_NAME), _portfolioTransfer.Code, INDsleClient.Text, INDSleAdvance.Text, bills)
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = content,
                .CreationDate = dateServer,
                .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName,
                .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me._portfolioTransfer.Code & "#$",
                .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._portfolioTransfer.Code),
                .Update = dateServer,
                .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = content
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._portfolioTransfer.Code)
            Return Me._doc
        End If
    End Function

    Private Async Sub GenerateBlockRecord()
        Using model As New Presentation.Portfolio.MVP.MBlockRecordAndSequense(MyTag)
            Dim result = Await model.GetBlockRecord(Me.Tag, Me._portfolioTransfer.Id)
            If result IsNot Nothing AndAlso result.Id = 0 Then
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                state.State = Domain.Base.Entities.ObjectState.Added
                _record = New BlockRecordPortfolio With {.BlockDate = DateTime.Now, .ChangeTracker = state, .NameUser = SessionValues.Instance.UserIndigoName, .IdForm = Me.Tag, .CodUser = SessionValues.Instance.UserIndigo, .IdRecord = Me._portfolioTransfer.Id}
                Dim operation = Await model.SaveBlockRecord(_record)
                _record = operation.ObjectEmbbeded
            Else
                Dim xtraMessage As String = String.Format(ResourceManager.GetString("RecordLocked"), result.CodUser, result.NameUser, result.BlockDate)
                _record = result
                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
            End If
        End Using
    End Sub

    Private Async Function DeleteBlockedRecord() As Task
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New Presentation.Portfolio.MVP.MBlockRecordAndSequense(Me.Tag.ToString())
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                Await model.DeleteBlockRecord(_record)
            End Using
            _record = Nothing
        End If
    End Function

    Private Function ValidatePopup() As String
        Dim errors As New StringBuilder
        If PortfolioAdvanceId Is Nothing Then
            errors.AppendLine("Debe seleccionar un anticipo")
        End If
        If INDTxtTransferValue.EditValue <= 0 Then
            errors.AppendLine(INDLciValue.CustomizationFormText + ResourceManager.GetString("Empty"))
        End If

        If INDTxtTransferValue.EditValue > INDTxtBalance.EditValue Then
            errors.AppendLine(String.Format(ResourceManager.GetString("BalanceBill", MODULE_NAME), INDSleBill.Text))
        End If

        If _listPortfolioTransferDetail IsNot Nothing AndAlso _listPortfolioTransferDetail.Count > 0 Then
            If _listPortfolioTransferDetail.Any(Function(x) x.InvoiceNumber = _portfolioTransferDetail.InvoiceNumber And x.MainAccountId = _portfolioTransferDetail.MainAccountId) Then
                errors.AppendLine(String.Format(ResourceManager.GetString("AddedBill", MODULE_NAME), INDSleBill.Text))
            End If
        End If
        Return errors.ToString()
    End Function

    Private Function ValidateControlsPopupOtherConcept() As String
        Dim errors As New StringBuilder
        If PortfolioAdvanceId Is Nothing Then
            errors.AppendLine("Debe seleccionar un anticipo")
        End If
        If INDSleNoteConcept.EditValue = Nothing Then
            errors.AppendLine("Concepto Vacio")
        End If
        If INDSleMainAccountConcept.EditValue = Nothing Then
            errors.AppendLine("Cuenta Contable Vacia")
        End If
        If INDlyItemThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDsleThirdParty.EditValue = Nothing Then
                errors.AppendLine("Tercero Vacio")
            End If
        End If
        If INDLciCostCenterConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDSLeCostCenterConcept.EditValue = Nothing Then
                errors.AppendLine("Centro de Costo Vacio")
            End If
        End If
        If INDGLeNatureConcept.EditValue = Nothing Then
            errors.AppendLine("Naturaleza Vacia")
        End If
        If INDTxtValueOtherConcept.EditValue Is Nothing OrElse INDTxtValueOtherConcept.EditValue = 0 Then
            errors.AppendLine("El valor debe ser superior a 0")
        End If
        Return errors.ToString()
    End Function

    Private Sub AssigningValues()
        With _portfolioTransfer
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .OperatingUnitId = _operativeUnitId
            .Code = Code
            .DocumentDate = DocumentDate
            .CustomerId = CustomerId
            .TransferType = INDGleTransferType.EditValue
            .PortfolioAdvanceId = PortfolioAdvanceId
            .MainAccountId = MainAccountId
            .CostCenterId = CostCenterId
            .Observations = Observations
            .CurrencyId = Me.CurrencyId
            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' carga por defecto la moneda oficial en los controles
    ''' </summary>
    Private Sub LoadDefaultCurrency()
        Me.CurrencyId(indigo.CurrencyISO4217) = indigo.OfficialCurrencyId
        Me.CurrencyExchangeActions(False)
    End Sub

    ''' <summary>
    ''' setea el formato moneda de los campos y columnas del formulario
    ''' </summary>
    ''' <param name="Abbreviation"></param>
    Private Sub _setFormatGeneralControls(Abbreviation As String)
        If String.IsNullOrEmpty(Abbreviation) Then
            Exit Sub
        End If
        Me._currencyAbbreviation = Abbreviation
        Me.INDSleCurrency.Properties.NullText = $"{Abbreviation}"
        Me.INDColTransferValue = Window.Utils.FormatGrid(INDColTransferValue, Abbreviation)
        Me.INDColValue = Window.Utils.FormatGrid(INDColValue, Abbreviation)
        '---------------------------------------------------------------------------'
        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = Abbreviation.GetNumberFormat
        Me.INDTxtValueOtherConcept.Properties.Mask.Culture = _culture
        Me.INDTxtValue.Properties.Mask.Culture = _culture
        Me.INDTxtBalance.Properties.Mask.Culture = _culture
        Me.INDTxtTransferValue.Properties.Mask.Culture = _culture
        ctrAdvance.CodeISO4217 = Abbreviation
    End Sub


    ''' <summary>
    ''' Funcion para ocultar/mostrar el control de tasa de cambio, establece el valor del trm y el texto del control
    ''' </summary>
    ''' <param name="value">True- Activa validaciones y acciones del control del TRM.; False - Oculta el control</param>
    ''' <param name="_currencyId"></param>
    ''' <param name="_currencyAbbreviation"></param>
    ''' <param name="ToCurrencyId"></param>
    ''' <returns></returns>
    Private Async Function CurrencyExchangeActions(value As Boolean, Optional _currencyId As Integer? = Nothing, Optional _currencyAbbreviation As String = Nothing, Optional ToCurrencyId As Integer? = Nothing) As Task(Of Boolean)
        'Si el valor es esta en False se oculta o si no pasa alguna de las otras condiciones
        If Not value OrElse CurrencyId Is Nothing OrElse String.IsNullOrEmpty(_currencyAbbreviation) OrElse ToCurrencyId Is Nothing OrElse (_currencyId = ToCurrencyId) Then
            Me.ShowOrHideExchangeControl(False)
            Return (Not value OrElse _currencyId = ToCurrencyId)
        End If

        'si pasa la validacion se muestra el control
        Me.ShowOrHideExchangeControl(True)

        'si ya existe el TRM se consulta el guardado para no perder tiempo en ir a consultarlo de nuevo debido a que el trm es diario
        If _listTRM IsNot Nothing AndAlso _listTRM.Any(Function(x) x.CurrencyId = _currencyId AndAlso x.OfficialCurrencyId = ToCurrencyId AndAlso x.MeasurementDate.Date = GetDateServer().Date) Then
            INDTxtExchange.Text = $"{_currencyAbbreviation} - TRM:{String.Format("{0:n2}", Utils.VisibleTRM(_listTRM.FirstOrDefault(Function(x) x.CurrencyId = _currencyId AndAlso
                                                                                                          x.OfficialCurrencyId = ToCurrencyId AndAlso
                                                                                                          x.MeasurementDate.Date = GetDateServer().Date).Value))}"
            Return True
        End If

        'si no se aha consultado previamente, se manda a consultar
        Using Model As New MPortfolioTransfers("")
            Dim Result = Await Model.GetTRMbyCurrencyId(_currencyId, ToCurrencyId)

            If Result Is Nothing OrElse Not Result?.StateResult Then
                Me.ShowOrHideExchangeControl(False)
                Me.Mensaje(EeventViewerImages.Advertencia) = Result?.Message
                Return False
            End If

            _listTRM = If(_listTRM Is Nothing, New List(Of TRM), _listTRM)
            _listTRM.Add(Result.ObjectEmbbeded)
            Me.Mensaje(EeventViewerImages.Informacion) = Result?.Message
            INDTxtExchange.Text = $"{_currencyAbbreviation} - TRM: {String.Format("{0:n2}", Utils.VisibleTRM(Result.ObjectEmbbeded.Value))}"
            Return True
        End Using
    End Function

    ''' <summary>
    ''' oculta o muestra el control de tasa de cambio
    ''' </summary>
    ''' <param name="Value"></param>
    Private Sub ShowOrHideExchangeControl(Value As Boolean)
        INDLciExchange.Visibility = If(Not Value, DevExpress.XtraLayout.Utils.LayoutVisibility.Never, DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
        INDTxtExchange.Text = String.Empty
    End Sub

#Region "Import File & Copy - Paste"

    ''' <summary>
    ''' Metodo que importa los items del excel a la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ImportFile() As Task
        If INDsleClient.EditValue Is Nothing Then 'Si no han seleccionado el cliente
            Mensaje(EeventViewerImages.Advertencia) = "Se debe seleccionar un cliente antes de importar la información"
            Exit Function
        End If
        If PortfolioAdvanceId Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Se debe seleccionar un anticipo antes de pegar la información"
            Exit Function
        End If

        If _listPortfolioTransferDetail IsNot Nothing AndAlso _listPortfolioTransferDetail.Count > 0 Then 'Si ya existen datos en la rejilla de facturas
            If MessageIndigo.Show("Se perderan los datos que estan en la rejilla, desea continuar", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                Exit Function
            End If
            _listPortfolioTransferDetail = Nothing
            INDGcBills.DataSource = Nothing
        End If

        'Configuramos el cuadro de dialogo para importar el archivo
        Dim openFileDialog1 As New OpenFileDialog()
        openFileDialog1.InitialDirectory = "c:\"
            openFileDialog1.Filter = "Microsoft Excel 2003 (*.xls)|*.xls|Microsoft Excel 2007 (*.xlsx)|*.xlsx"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True
        openFileDialog1.Title = "Importar Archivo"
        AsyncLoader(True)

        'Si el ususario cancela la operación
        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.Cancel Then
            AsyncLoader(False)
            Exit Function
        End If

        Try
            'Obtengo la ruta del archivo
            _myStream = openFileDialog1.FileName
            If (_myStream Is Nothing OrElse _myStream.Trim().Equals(String.Empty)) Then 'Si la ruta es vacía
                Mensaje(EeventViewerImages.Advertencia) = "Ruta de archivo vacía"
                AsyncLoader(False)
                Exit Function
            End If

            'Validamos los datos de excel y armamos el listado que se envia para poder pegar en la rejilla de facturas
            Dim result = LoadImportFile(_myStream)
            If result.StateResult = False Then
                AsyncLoader(False)
                Mensaje(EeventViewerImages.Advertencia) = result.Message
                Exit Function
            End If

            'Se llama el metodo que utiliza el copyPaste
            Await PasteToGrid(INDGcBills, result.ObjectEmbbeded)

            AsyncLoader(False)
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' Metodo que se encarga de validar el archivo de excel
    ''' </summary>
    ''' <param name="fileName"></param>
    ''' <remarks></remarks>
    Private Function LoadImportFile(ByVal fileName As String) As ActionResult(Of List(Of List(Of String)))
        Dim ssc = New SpreadsheetControl()
        ssc.AllowDrop = False
        ssc.LoadDocument(_myStream)
        Dim workBook As IWorkbook = ssc.Document

        Dim rows As RowCollection = workBook.Worksheets(0).Rows
        If rows.LastUsedIndex = 0 Then
            Return New ActionResult(Of List(Of List(Of String))) With {.StateResult = False, .Message = "No se encontraron registros en el archivo"}
        End If

        Dim ListFile As New List(Of List(Of String))
        If INDGleTransferType.EditValue = 1 Then 'Mismo cliente
            For i As Integer = 1 To rows.LastUsedIndex Step 1
                Dim item = rows.Item(i).SpreadsheetRowToList(3)
                If item(0) IsNot Nothing AndAlso item(0).ToString() <> "" AndAlso item(1) IsNot Nothing AndAlso item(1).ToString() <> "" AndAlso item(2) IsNot Nothing AndAlso item(2).ToString() <> "" Then
                    Dim ListItem As New List(Of String)
                    If item(0) IsNot Nothing Then
                        ListItem.Add(item(0).ToString)
                    End If
                    If item(1) IsNot Nothing Then
                        ListItem.Add(item(1).ToString)
                    Else
                        ListItem.Add(String.Empty)
                    End If
                    If item(2) IsNot Nothing Then
                        ListItem.Add(item(2).ToString)
                    End If
                    ListFile.Add(ListItem)
                End If
            Next
        Else 'Diferente cliente
            For i As Integer = 1 To rows.LastUsedIndex Step 1
                Dim item = rows.Item(i).SpreadsheetRowToList(4)
                If item(0) IsNot Nothing AndAlso item(0).ToString() <> "" AndAlso item(1) IsNot Nothing AndAlso item(1).ToString() <> "" AndAlso item(2) IsNot Nothing AndAlso item(2).ToString() <> "" AndAlso item(3) IsNot Nothing AndAlso item(3).ToString <> "" Then
                    Dim ListItem As New List(Of String)
                    If item(0) IsNot Nothing Then
                        ListItem.Add(item(0).ToString)
                    End If
                    If item(1) IsNot Nothing Then
                        ListItem.Add(item(1).ToString)
                    Else
                        ListItem.Add(String.Empty)
                    End If
                    If item(2) IsNot Nothing Then
                        ListItem.Add(item(2).ToString)
                    End If
                    If item(3) IsNot Nothing Then
                        ListItem.Add(item(3).ToString)
                    End If
                    ListFile.Add(ListItem)
                End If
            Next
        End If

        Return New ActionResult(Of List(Of List(Of String))) With {.StateResult = True, .ObjectEmbbeded = ListFile}
    End Function

    ''' <summary>
    ''' Metodo que copia y pega los items a la rejilla de facturas
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function PasteToGrid(sender As DevExpress.XtraGrid.GridControl, ListInfo As List(Of List(Of String))) As Task
        If sender.Name = INDGcBills.Name Then
            If INDsleClient.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Se debe seleccionar un cliente antes de pegar la información"
                Exit Function
            End If
            If PortfolioAdvanceId Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Se debe seleccionar un anticipo antes de pegar la información"
                Exit Function
            End If

            INDGvBills.ShowLoadingPanel()
            Me.Cursor = ChangeCursorIndigo()
            Using model As New MPortfolioTransfers(MyTag)
                _portfolioTransfer.TransferType = INDGleTransferType.EditValue
                Dim result = Await model.SetBillsTransfersCopyPaste(ListInfo, _portfolioTransfer, indigo.IndigoCompanyType, Me._operativeUnitId)
                If result.StateResult = False Then
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    INDGvBills.HideLoadingPanel()
                    Exit Function
                End If

                Dim listErrors As New List(Of String)
                If result.MessageResult IsNot Nothing Then
                    listErrors = result.MessageResult
                End If

                If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                    If _listPortfolioTransferDetail IsNot Nothing AndAlso _listPortfolioTransferDetail.Count > 0 Then
                        For Each item In result.ObjectEmbbeded
                            Dim billAdded = _listPortfolioTransferDetail.Where(Function(x) x.AccountReceivableId = item.AccountReceivableId And x.MainAccountId = item.MainAccountId).FirstOrDefault()
                            If billAdded IsNot Nothing Then
                                listErrors.Add("La Factura " & item.InvoiceNumber & " ya esta agregada con la cuenta contable " & item.CodeNameMainAccount)
                                Continue For
                            End If
                            _listPortfolioTransferDetail.Add(item)
                        Next
                    Else
                        _listPortfolioTransferDetail = result.ObjectEmbbeded
                    End If
                End If

                If listErrors.Count > 0 Then
                    Using formulario As New FrmListErrors(listErrors)
                        formulario.StartPosition = FormStartPosition.CenterParent
                        Dim transparent As New FrmTransparent(formulario, False)
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        transparent.ShowDialog(Me)
                    End Using
                End If
            End Using

            Me.Cursor = System.Windows.Forms.Cursors.Default
            INDGvBills.HideLoadingPanel()

            INDGcBills.DataSource = Nothing
            INDGcBills.DataSource = _listPortfolioTransferDetail
            If _listPortfolioTransferDetail IsNot Nothing AndAlso _listPortfolioTransferDetail.Count > 0 Then
                INDsleClient.Properties.ReadOnly = True
                INDGleTransferType.Enabled = False
                INDSleAdvance.Properties.ReadOnly = True
            End If

            ctrAdvance.PrintAdvance()
        End If
    End Function

#End Region

#Region "Save For Batches"

    ''' <summary>
    ''' control donde se muestra el progreso de la operacion de importar archivos
    ''' </summary>
    ''' <remarks></remarks>
    Dim progress As CtrProgress

    ''' <summary>
    ''' total de los items a procesar
    ''' </summary>
    ''' <remarks></remarks>
    Dim totalItems As Integer

    ''' <summary>
    ''' items procesados
    ''' </summary>
    ''' <remarks></remarks>
    Dim totalProcessedItems As Integer = 0

    ''' <summary>
    ''' items que se van a enviar en cada proceso
    ''' </summary>
    ''' <remarks></remarks>
    Dim itemsSend As Integer = 100

    ''' <summary>
    ''' metodo para mostrar en el control cuantos items se han procesado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getInfo() As Tuple(Of String, String)
        Return New Tuple(Of String, String)(totalProcessedItems.ToString(), totalItems.ToString())
    End Function

    Private Async Function SavePortfolioTransfer() As Task
        Try
            AsyncLoader(True)

            Dim errors As New StringBuilder()
            Dim Status = _portfolioTransfer.Status

            If {1, 2}.Contains(Status) Then
                progress = New CtrProgress
                progress.SetInfoFunction(AddressOf getInfo)
                progress.PrintInfo()
                progress.Dock = DockStyle.Fill
                AdditionalControlPanel.SafeInvoke(Sub(x) x.Controls.Clear())
                AdditionalControlPanel.SafeInvoke(Sub(x) x.Controls.Add(progress))

                Me._portfolioTransfer.Status = 1

                'Facturas
                Dim ListPortfolioProvisionDetailToSend = New List(Of PortfolioTransferDetail)
                If _listPortfolioTransferDetail IsNot Nothing AndAlso _listPortfolioTransferDetail.Any(Function(o) o.ChangeTracker.State = ObjectState.Added OrElse o.ChangeTracker.State = ObjectState.Modified) Then
                    For Each item In _listPortfolioTransferDetail.Where(Function(o) o.ChangeTracker.State = ObjectState.Added OrElse o.ChangeTracker.State = ObjectState.Modified)
                        ListPortfolioProvisionDetailToSend.Add(item)
                    Next
                End If
                If _listPortfolioTransferDetailDelete IsNot Nothing AndAlso _listPortfolioTransferDetailDelete.Count > 0 Then
                    For Each item In _listPortfolioTransferDetailDelete
                        ListPortfolioProvisionDetailToSend.Add(item.MarkAsDeleted())
                    Next
                End If

                'Otros Conceptos
                Dim ListPortfolioTransferOtherConcept = New List(Of PortfolioTransferOtherConcept)
                If _listPortfolioTransferOtherConcept IsNot Nothing AndAlso _listPortfolioTransferOtherConcept.Any(Function(o) o.ChangeTracker.State = ObjectState.Added OrElse o.ChangeTracker.State = ObjectState.Modified) Then
                    For Each item In _listPortfolioTransferOtherConcept.Where(Function(o) o.ChangeTracker.State = ObjectState.Added OrElse o.ChangeTracker.State = ObjectState.Modified)
                        ListPortfolioTransferOtherConcept.Add(item)
                    Next
                End If
                If _listPortfolioTransferOtherConceptDelete IsNot Nothing AndAlso _listPortfolioTransferOtherConceptDelete.Count > 0 Then
                    For Each item In _listPortfolioTransferOtherConceptDelete
                        ListPortfolioTransferOtherConcept.Add(item.MarkAsDeleted())
                    Next
                End If

                totalProcessedItems = 0
                totalItems = 0
                If (ListPortfolioProvisionDetailToSend.Count > 0) OrElse (ListPortfolioTransferOtherConcept.Count > 0) Then
                    Dim indexSend = 0
                    totalItems = ListPortfolioProvisionDetailToSend.Count + ListPortfolioTransferOtherConcept.Count
                    progress.SafeInvoke(Sub(x)
                                            x.SetTitle = "Registros Guardados"
                                            x.PrintInfo()
                                        End Sub)

                    While ListPortfolioProvisionDetailToSend.Count > 0 OrElse ListPortfolioTransferOtherConcept.Count > 0
                        Dim objLock As New Object()
                        'agregamos los detalles a la cabecera
                        Dim listToSend = ListPortfolioProvisionDetailToSend.Take(itemsSend).ToList()
                        Me._portfolioTransfer.PortfolioTransferDetail.Clear()
                        Parallel.ForEach(listToSend, Sub(x)
                                                         SyncLock objLock
                                                             _portfolioTransfer.PortfolioTransferDetail.Add(x)
                                                         End SyncLock
                                                     End Sub)
                        'Si es el primer ciclo agregamos los otros conceptos a la cabecera
                        Dim listOtherConceptToSend = ListPortfolioTransferOtherConcept.Take(itemsSend).ToList()
                        Me._portfolioTransfer.PortfolioTransferOtherConcept.Clear()
                        Parallel.ForEach(listOtherConceptToSend, Sub(x)
                                                                     SyncLock objLock
                                                                         _portfolioTransfer.PortfolioTransferOtherConcept.Add(x)
                                                                     End SyncLock
                                                                 End Sub)

                        Dim quantityDetailsToProcess = If(ListPortfolioProvisionDetailToSend.Count < itemsSend, ListPortfolioProvisionDetailToSend.Count, itemsSend)
                        Dim quantityOtherConceptsToProcess = If(ListPortfolioTransferOtherConcept.Count < itemsSend, ListPortfolioTransferOtherConcept.Count, itemsSend)
                        indexSend = totalProcessedItems + 1
                        totalProcessedItems += quantityDetailsToProcess + quantityOtherConceptsToProcess

                        Using model As New MPortfolioTransfers(Me.Tag.ToString())
                            Dim Result = Await model.SavePortfolioTransfer(Me._portfolioTransfer)
                            If Result.StateResult Then
                                Me.Code = Result.ObjectEmbbeded.Code
                                Me._portfolioTransfer = Result.ObjectEmbbeded
                            Else
                                errors.AppendLine("Los items del " + (indexSend).ToString() + " hasta " + (totalProcessedItems).ToString() + " no se pudieron guardar porque:" + vbNewLine + Result.Message)
                            End If
                        End Using

                        If quantityDetailsToProcess > 0 Then
                            ListPortfolioProvisionDetailToSend.RemoveRange(0, quantityDetailsToProcess)
                        End If

                        If quantityOtherConceptsToProcess > 0 Then
                            ListPortfolioTransferOtherConcept.RemoveRange(0, quantityOtherConceptsToProcess)
                        End If

                        progress.PrintInfo()
                    End While
                End If

                AdditionalControlPanel.Controls.Clear()
                AdditionalControlPanel.Controls.Add(ctrAdvance)
            End If

            If errors.Length = 0 Then
                Me._portfolioTransfer.PortfolioTransferDetail.Clear()
                Me._portfolioTransfer.PortfolioTransferOtherConcept.Clear()

                If Status = 1 Then
                    If totalProcessedItems = 0 Then
                        Using model As New MPortfolioTransfers(Me.Tag.ToString())
                            Dim Result = Await model.SavePortfolioTransfer(Me._portfolioTransfer)
                            AsyncLoader(False)
                            If Result.StateResult = True Then
                                Mensaje(EeventViewerImages.Informacion) = Result.Message
                                _portfolioTransfer = Result.ObjectEmbbeded
                            Else
                                INDBteCode.Enabled = False
                                Mensaje(EeventViewerImages.Advertencia) = Result.Message
                                Exit Function
                            End If
                        End Using
                    Else
                        AsyncLoader(False)
                        Mensaje(EeventViewerImages.Informacion) = String.Format("Se guardó el Cruce de Anticipo vs CxC con código {0}", Me._portfolioTransfer.Code)
                    End If
                Else
                    Me._portfolioTransfer.Status = Status
                    Using model As New MPortfolioTransfers(Me.Tag.ToString())
                        Dim Result = Await model.SavePortfolioTransfer(Me._portfolioTransfer)
                        AsyncLoader(False)
                        If Result.StateResult = True Then
                            Mensaje(EeventViewerImages.Informacion) = Result.Message
                            _portfolioTransfer = Result.ObjectEmbbeded
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = String.Concat(String.Format("No se {0} el Cruce de Anticipo vs CxC con código {1} porque:", If(Status = 2, "Confirmó", "Anuló"), Me._portfolioTransfer.Code), vbNewLine, Result.Message)
                            Me.Deshacer()
                            Exit Function
                        End If
                    End Using
                End If

                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                Select Case _varImp
                    Case 1
                        Me.BarraBotones.PrintReport(PrintReportAction.Create, _portfolioTransfer.Id, 0, _portfolioTransfer.Id)
                    Case 2
                        Me.BarraBotones.PrintReport(PrintReportAction.Update, _portfolioTransfer.Id, 0, _portfolioTransfer.Id)
                    Case 3
                        Me.BarraBotones.PrintReport(PrintReportAction.Cancel, _portfolioTransfer.Id, 0, _portfolioTransfer.Id)
                    Case 4
                        Me.BarraBotones.PrintReport(PrintReportAction.Confirm, _portfolioTransfer.Id, 0, _portfolioTransfer.Id)
                End Select
                Me.Deshacer()
            Else
                AsyncLoader(False)
                INDBteCode.Enabled = False
                Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            End If
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Mensaje(EeventViewerImages.Informacion) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Function

#End Region

#End Region

#Region "Events"

#Region "Custom"

    Public Event LoadControlsFinish()

#End Region

#Region "Load"

    Private Async Sub FrmPortfolioTransfers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDLcTransfers, True)
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.Funct = AddressOf GenerateDoc
        _operativeUnitId = BarraBotones.OperatingUnitValue
        _presenter = New PPortfolioTransfers(Me)
        _presenter.LoadDefinitionLayout()
        _presenter.GetSequense()
        '******************************

        AddActionsColumns()
        InitializeTuples()
        Deshacer()
        LoadStatus()
        Using model As New MSettingPortfolio(MyTag)
            settingPortfolio = Await model.GetSettingPortfolioByIdOperatingUnitAsync(_operativeUnitId)
        End Using
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _presenter = Nothing
        _operativeUnitId = Nothing
        _currentSequenceId = Nothing
        _sequence = Nothing
        _record = Nothing
        _portfolioTransfer = Nothing
        _portfolioTransferDetail = Nothing
        _listPortfolioTransferDetail = Nothing
        _listPortfolioTransferDetailDelete = Nothing
        _portfolioTransferOtherConcept = Nothing
        _listPortfolioTransferOtherConcept = Nothing
        _listPortfolioTransferOtherConceptDelete = Nothing
        _varImp = Nothing
        _myStream = Nothing
        settingPortfolio = Nothing
    End Sub

#End Region

#Region "Activated"

    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBteCode.Enabled Then
            INDBteCode.Focus()
        End If
    End Sub

#End Region

#Region "FormClosing"

    Private Async Sub FrmPortfolioTransfers_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Await DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    Private Async Sub INDBteCode_KeyDown(sender As Object, e As KeyEventArgs) Handles INDBteCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(Code.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(Code) Then
                    Await Me.NewPortfolioTransfers()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    Private Sub INDMeObservations_KeyDown(sender As Object, e As KeyEventArgs) Handles INDMeObservations.KeyDown
        If e.KeyCode = Keys.Enter Then
            INDPceBills.Focus()
            INDPceBills.ShowPopup()
            INDSleBill.Focus()
        End If
    End Sub

#End Region

#Region "QueryPopUp"

    Private Sub INDsleClient_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleClient.QueryPopUp
        If INDsleClient.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If CustomerXPO Is Nothing Then
            _presenter.InitializeCustomerXPO()
        End If
    End Sub

    Private Sub INDSleAdvance_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleAdvance.QueryPopUp
        If INDSleAdvance.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If AdvanceXPO Is Nothing Then
            _presenter.InitializeAdvanceXPO(_portfolioTransfer.ThirdPartyId)
        End If
    End Sub

    Private Sub INDSleCostCenter_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleCostCenter.QueryPopUp
        If CostCenterXPO Is Nothing Then
            _presenter.InitializeCostCenterXPO()
        End If
    End Sub

    Private Sub INDSleBill_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleBill.QueryPopUp
        If BillsXPO Is Nothing Then
            If INDGleTransferType.EditValue = 1 Then
                _presenter.ViewInitializeBillsXPO(_portfolioTransfer.ThirdPartyId, settingPortfolio.Transfers)
            Else
                _presenter.ViewInitializeBillsXPO(0, settingPortfolio.Transfers)
            End If
        End If
    End Sub

    Private Sub INDSleNoteConcept_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleNoteConcept.QueryPopUp
        If NoteConceptXpo Is Nothing Then
            If INDSleNoteConcept.Properties.ReadOnly = False Then
                _presenter.InitializeNoteConceptXPO()
            End If
        End If
    End Sub

    Private Sub INDSleMainAccountConcept_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleMainAccountConcept.QueryPopUp
        If AccountsXPO Is Nothing Then
            _presenter.InitializeAccountXPO()
        End If
    End Sub

    Private Sub INDsleThirdParty_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleThirdParty.QueryPopUp
        If ThirdPartyXpo Is Nothing Then
            _presenter.InitializeThirdPartyXpo()
        End If
    End Sub

    Private Sub INDSLeCostCenterConcept_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSLeCostCenterConcept.QueryPopUp
        If CostCenterConceptXPO Is Nothing Then
            _presenter.InitializeCostCenterConceptXPO()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    Private Sub INDsleClient_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleClient.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmCustomers
                formulario.ViewModeEditHold = True
                formulario.Size = New System.Drawing.Size(800, 700)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(formulario, False)
                transparent.ShowDialog(Me)
                _presenter.InitializeCustomerXPO()
                INDsleClient.Focus()
            End Using
        End If
    End Sub

    Private Sub INDSleMainAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleMainAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmPopupPUC
                formulario.ViewModeEditHold = True
                Dim size As New System.Drawing.Size(800, 700)
                formulario.Size = size
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(formulario, False)
                transparent.ShowDialog(Me)
                Dim value = MainAccountId
                MainAccountId = Nothing
                MainAccountId = value
                INDSleMainAccount.Focus()
            End Using
        End If
    End Sub

    Private Sub INDSleCostCenter_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleCostCenter.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmCostCenter
                formulario.ViewModeEditHold = True
                Dim size As New System.Drawing.Size(800, 700)
                formulario.Size = size
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(formulario, False)
                transparent.ShowDialog(Me)
                _presenter.InitializeCostCenterXPO()
                INDSleCostCenter.Focus()
            End Using
        End If
    End Sub

    Private Sub INDSleNoteConcept_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleNoteConcept.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmPortfolioNoteConcepts
                formulario.ViewModeEditHold = True
                formulario.Size = New System.Drawing.Size(800, 700)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent = New FrmTransparent(formulario, False)
                transparent.ShowDialog(Me)
                _presenter.InitializeNoteConceptXPO()
                Dim value = INDSleNoteConcept.EditValue
                INDSleNoteConcept.EditValue = Nothing
                INDSleNoteConcept.EditValue = value
                INDPceOtherConcept.ShowPopup()
                INDSleNoteConcept.Focus()
            End Using
        End If
    End Sub

    Private Sub INDSleMainAccountConcept_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleMainAccountConcept.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmPopupPUC
                formulario.ViewModeEditHold = True
                formulario.Size = New System.Drawing.Size(800, 700)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(formulario, False)
                transparent.ShowDialog(Me)
                INDPceOtherConcept.ShowPopup()
                INDSleMainAccountConcept.Focus()
                Dim value = INDSleMainAccountConcept.EditValue
                INDSleMainAccountConcept.EditValue = Nothing
                INDSleMainAccountConcept.EditValue = value
            End Using
        End If
    End Sub

    Private Sub INDSLeCostCenterConcept_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSLeCostCenterConcept.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmCostCenter
                formulario.ViewModeEditHold = True
                formulario.Size = New System.Drawing.Size(800, 700)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(formulario, False)
                transparent.ShowDialog(Me)
                _presenter.InitializeCostCenterConceptXPO()
                INDPceOtherConcept.ShowPopup()
                INDSLeCostCenterConcept.Focus()
            End Using
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDsleClient_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleClient.EditValueChanged
        AdvanceXPO = Nothing
        INDSleAdvance.EditValue = Nothing
        INDSleAdvance.Enabled = False

        INDEsbBills.Enabled = False
        INDBtnImportFile.Enabled = False
        BillsXPO = Nothing

        INDPceBills.Enabled = False

        If CustomerId IsNot Nothing Then
            INDSleAdvance.Enabled = True
            INDEsbBills.Enabled = True
            INDBtnImportFile.Enabled = True
            INDPceBills.Enabled = True

            Using modelCustomer As New MCustomers(MyTag)
                Dim customer = modelCustomer.GetCustomerById(INDsleClient.EditValue)
                _portfolioTransfer.ThirdPartyId = customer.ThirdPartyId
            End Using
        End If
    End Sub

    Private Sub INDGleTransferType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleTransferType.EditValueChanged
        BillsXPO = Nothing
        If INDGleTransferType.EditValue IsNot Nothing Then
            'Se establece el formato de exportación de excel para CopyPaste e ImportFile
            If INDGleTransferType.EditValue = 1 Then 'Mismo cliente
                INDEsbBills.AddExcelSheets(New ExcelSheet With {
                    .Columns = New List(Of ExcelColumn) From {
                        New ExcelColumn With {.Name = "Factura"},
                        New ExcelColumn With {.Name = "Estado Cartera", .Comment = "1 - Sin Radicar" & vbCrLf & "2 -  Radicada sin Confirmar" & vbCrLf & "3 - Radicada Entidad" & vbCrLf & "4 - Glosada sin Conciliar" & vbCrLf & "12 - Glosada Conciliada" & vbCrLf & "15 - Cuenta de Dificil Recaudo" & vbCrLf & "16 - Cobro Jurídico"},
                        New ExcelColumn With {.Name = "Valor", .Comment = "El valor del cruce se debe escribir en la moneda de la cuenta por cobrar"}
                    }
                })
            Else 'Diferente cliente
                INDEsbBills.AddRangeColumns("Factura", "Estado Cartera", "Valor", "Tercero")
            End If
        End If
    End Sub

    Private Async Sub INDSleAdvance_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleAdvance.EditValueChanged
        _advanceValue = 0
        If PortfolioAdvanceId IsNot Nothing Then
            Using model As New MPortfolioAdvance(MyTag)

                Dim advance = Await model.GetPortfolioAdvanceByIdAsync(PortfolioAdvanceId)
                Me.MainAccountId = advance.ObjectEmbbeded.MainAccountId
                Me._advanceValue = advance.ObjectEmbbeded.Balance
                Me.CurrencyId(If(advance?.ObjectEmbbeded?.Currency Is Nothing, indigo.CurrencyISO4217, advance?.ObjectEmbbeded?.Currency?.Abbreviation)) = If(advance?.ObjectEmbbeded?.CurrencyId Is Nothing, indigo.OfficialCurrencyId, advance?.ObjectEmbbeded?.CurrencyId)

                If _portfolioTransfer Is Nothing Then
                    _portfolioTransfer = New PortfolioTransfer With {.CurrencyId = Me.CurrencyId, .PortfolioAdvanceId = Me.PortfolioAdvanceId}
                Else
                    With _portfolioTransfer
                        .CurrencyId = Me.CurrencyId
                        .PortfolioAdvanceId = Me.PortfolioAdvanceId
                    End With
                End If

                ctrAdvance.PrintAdvance()
                If CallNote Then
                    RaiseEvent LoadControlsFinish()
                End If
            End Using
        End If
    End Sub

    Private Sub INDSleMainAccount_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleMainAccount.EditValueChanged
        INDLciCostCenter.HideControl(True)
        INDSleCostCenter.EditValue = Nothing
        INDSleCostCenter.Properties.NullText = String.Empty

        If MainAccountId IsNot Nothing Then
            Using model As New MPUC(MyTag)
                Dim account = model.GetAccountByIdSimple(MainAccountId, False)
                INDSleMainAccount.Properties.NullText = account.Number + " - " + account.Name
                If account.HandlesCostCenter Then
                    INDLciCostCenter.HideControl(False)
                End If
            End Using
        End If
    End Sub

    Private Async Sub INDSleBill_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleBill.EditValueChanged
        If INDSleBill.EditValue IsNot Nothing Then
            Using model As New MNotesDebitCreditPortfolio(MyTag)
                Dim accountReceivableAccounting = model.GetPortfolioAccountReceivableAccountingById(INDSleBill.EditValue)
                Dim TRMValue As Decimal = 1
                Dim patient = DirectCast(INDGvSleBill.GetFocusedRow, DevExpress.Data.[Async].Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow
                If accountReceivableAccounting Is Nothing Then
                    CleanControlsPopup()
                    Exit Sub
                End If

                If Not Await Me.CurrencyExchangeActions(True, Me.CurrencyId, Me.CurrencyAbbreviation, accountReceivableAccounting?.CurrencyId) Then
                    CleanControlsPopup()
                    Exit Sub
                End If

                If Me.CurrencyId <> accountReceivableAccounting?.CurrencyId Then
                    TRMValue = _listTRM.FirstOrDefault(Function(x) x.CurrencyId = Me.CurrencyId AndAlso x.OfficialCurrencyId = accountReceivableAccounting?.CurrencyId)?.Value
                End If

                INDDteBillDate.EditValue = accountReceivableAccounting.AccountReceivableId.AccountReceivableDate
                INDDteExpiredDate.EditValue = accountReceivableAccounting.AccountReceivableId.ExpiredDate
                INDTxtValue.EditValue = Math.Round(accountReceivableAccounting.Value / TRMValue, 2)
                INDTxtBalance.EditValue = Math.Round(accountReceivableAccounting.Balance / TRMValue, 2)

                INDTxtTransferValue.Enabled = True
                INDBtnAdd.Enabled = True

                _portfolioTransferDetail = New PortfolioTransferDetail
                With _portfolioTransferDetail
                    .MainAccountId = accountReceivableAccounting.MainAccountId.Id
                    .CodeNameMainAccount = accountReceivableAccounting.MainAccountId.NumberName
                    .AccountReceivableId = accountReceivableAccounting.AccountReceivableId.Id
                    .InvoiceNumber = accountReceivableAccounting.AccountReceivableId.InvoiceNumber
                    .ValueBill = accountReceivableAccounting.Value
                    .Balance = accountReceivableAccounting.Balance
                    .TRMValue = TRMValue
                    .CurrencyAbbreviation = accountReceivableAccounting.CurrencyAbbreviation
                    .Patient = DirectCast(patient, Infrastructure.Data.Xpo.PortfolioRepository.ViewPortfolioListBillsTransfersXpo)?.Patient

                    Select Case accountReceivableAccounting.MainAccountId.Id

                        Case accountReceivableAccounting.AccountReceivableId.MainAccountWithoutFilingId
                            .PortfolioStatusName = "1-Sin Radicar"

                        Case accountReceivableAccounting.AccountReceivableId.AccountRadicateId
                            .PortfolioStatusName = "3-Radicada Entidad"

                        Case accountReceivableAccounting.AccountReceivableId.AccountObjectionRemediedId
                            .PortfolioStatusName = "4-Glosada sin Conciliar"

                        Case accountReceivableAccounting.AccountReceivableId.AccountConciliationId
                            .PortfolioStatusName = "12-Glosada Conciliada"

                        Case accountReceivableAccounting.AccountReceivableId.AccountHardCollectionId
                            .PortfolioStatusName = "15-Cuenta de Dificil Recaudo"

                        Case accountReceivableAccounting.AccountReceivableId.AccountLegalCollectionId
                            .PortfolioStatusName = "16-Cobro Jurídico"
                    End Select
                End With
            End Using
        End If
    End Sub

    Private Sub INDSleNoteConcept_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleNoteConcept.EditValueChanged
        If INDSleNoteConcept.EditValue IsNot Nothing Then
            Using modelConcept As New MPortfolioNoteConcept(MyTag)
                Dim concept = modelConcept.GetPortfolioNoteConceptById(INDSleNoteConcept.EditValue)
                If concept.IdAccount IsNot Nothing Then
                    INDSleMainAccountConcept.EditValue = concept.IdAccount
                Else
                    INDSleMainAccountConcept.EditValue = Nothing
                    INDSleMainAccountConcept.Properties.NullText = String.Empty
                    INDGLeNatureConcept.EditValue = Nothing
                End If
            End Using
        End If
    End Sub

    Private Sub INDSleMainAccountConcept_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleMainAccountConcept.EditValueChanged
        INDlyItemThirdParty.HideControl(True)
        INDsleThirdParty.EditValue = Nothing
        INDsleThirdParty.Properties.NullText = String.Empty

        INDLciCostCenterConcept.HideControl(True)
        INDSLeCostCenterConcept.EditValue = Nothing
        INDSLeCostCenterConcept.Properties.NullText = String.Empty

        If INDSleMainAccountConcept.EditValue IsNot Nothing Then
            Using modelPUC As New MPUC(MyTag)
                Dim account = modelPUC.GetAccountByIdSimple(INDSleMainAccountConcept.EditValue)
                INDSleMainAccountConcept.Properties.NullText = account.Number + " - " + account.Name
                If account.HandlesThirdParty Then 'Si maneja tercero
                    INDlyItemThirdParty.HideControl(False)
                    INDsleThirdParty.EditValue = Me._portfolioTransfer.ThirdPartyId
                    INDsleThirdParty.Properties.NullText = INDsleClient.Text
                End If
                If account.HandlesCostCenter Then
                    INDLciCostCenterConcept.HideControl(False)
                End If
                INDGLeNatureConcept.EditValue = account.MainAccountClasses.Nature
            End Using
        End If
    End Sub

    ''' <summary>
    ''' evento que se dispara al cambiar la moneda
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCurrency_Properties_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleCurrency.Properties.EditValueChanged
        CleanControlsPopup()
        If Me.CurrencyId Is Nothing Then
            Me.LoadDefaultCurrency()
            Exit Sub
        End If
    End Sub


#End Region

#Region "Click"

    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        Dim errors = ValidatePopup()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If

        If _listPortfolioTransferDetail Is Nothing Then
            _listPortfolioTransferDetail = New List(Of PortfolioTransferDetail)
        End If
        INDsleClient.Properties.ReadOnly = True
        INDGleTransferType.Enabled = False
        INDSleAdvance.Properties.ReadOnly = True
        _portfolioTransferDetail.Value = INDTxtTransferValue.EditValue

        _listPortfolioTransferDetail.Add(_portfolioTransferDetail)
        INDGcBills.DataSource = Nothing
        INDGcBills.DataSource = _listPortfolioTransferDetail

        ctrAdvance.PrintAdvance()
        CleanControlsPopup()
        INDSleBill.Focus()
    End Sub

    Private Sub INDBtnAddConcept_Click(sender As Object, e As EventArgs) Handles INDBtnAddConcept.Click
        Dim errors = ValidateControlsPopupOtherConcept()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If

        If _listPortfolioTransferOtherConcept Is Nothing Then
            _listPortfolioTransferOtherConcept = New List(Of PortfolioTransferOtherConcept)
        End If
        _portfolioTransferOtherConcept = New PortfolioTransferOtherConcept
        With _portfolioTransferOtherConcept
            .PortfolioNoteConceptId = INDSleNoteConcept.EditValue
            .CodeNameNoteConcept = INDSleNoteConcept.Text
            .MainAccountId = INDSleMainAccountConcept.EditValue
            .NumberNameMainAccount = If(INDSleMainAccountConcept.Text IsNot String.Empty, INDSleMainAccountConcept.Text, INDSleMainAccountConcept.Properties.NullText)
            .ThirdPartyId = INDsleThirdParty.EditValue
            .CodeNameThirdParty = INDsleThirdParty.Text
            .CostCenterId = INDSLeCostCenterConcept.EditValue
            .CodeNameCostCenter = INDSLeCostCenterConcept.Text
            .Nature = INDGLeNatureConcept.EditValue
            .Value = INDTxtValueOtherConcept.EditValue
        End With

        _listPortfolioTransferOtherConcept.Add(_portfolioTransferOtherConcept)
        INDGcOtherConcept.DataSource = Nothing
        INDGcOtherConcept.DataSource = _listPortfolioTransferOtherConcept

        ctrAdvance.PrintAdvance()
        CleanControlsPopupOtherConcept()
        INDSleNoteConcept.Focus()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de importar archivo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDBtnImportFile_Click(sender As Object, e As EventArgs) Handles INDBtnImportFile.Click
        Await ImportFile()
    End Sub

#End Region

#Region "Popup"

    Private Sub INDPceBills_Popup(sender As Object, e As EventArgs) Handles INDPceBills.Popup
        INDSleBill.Focus()
    End Sub

#End Region

#Region "CloseUp"

    Private Sub INDPceBills_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDPceBills.CloseUp
        INDPceOtherConcept.Focus()
        'INDPceOtherConcept.ShowPopup()
        INDSleNoteConcept.Focus()
    End Sub

#End Region

#Region "Click_ButtonAction"

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _portfolioTransferDetail = DirectCast(INDGvBills.GetFocusedRow, PortfolioTransferDetail)
            If _portfolioTransferDetail.Id > 0 Then
                If _listPortfolioTransferDetailDelete Is Nothing Then
                    _listPortfolioTransferDetailDelete = New List(Of PortfolioTransferDetail)
                End If
                _listPortfolioTransferDetailDelete.Add(_portfolioTransferDetail)
            End If

            _listPortfolioTransferDetail.Remove(_portfolioTransferDetail)
            INDGcBills.DataSource = Nothing
            INDGcBills.DataSource = _listPortfolioTransferDetail

            ctrAdvance.PrintAdvance()

            If _listPortfolioTransferDetail Is Nothing OrElse _listPortfolioTransferDetail.Count = 0 Then
                INDsleClient.Properties.ReadOnly = False
                INDGleTransferType.Enabled = True
                INDSleAdvance.Properties.ReadOnly = False
            End If
        End If
    End Sub

    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction, IndigoGridView2.ContexMenuActions
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Me._portfolioTransferOtherConcept = DirectCast(INDGvOtherConcept.GetFocusedRow, PortfolioTransferOtherConcept)
            If Me._portfolioTransferOtherConcept.Id > 0 Then
                If _listPortfolioTransferOtherConceptDelete Is Nothing Then
                    Me._listPortfolioTransferOtherConceptDelete = New List(Of PortfolioTransferOtherConcept)
                End If
                _listPortfolioTransferOtherConceptDelete.Add(_portfolioTransferOtherConcept)
            End If

            _listPortfolioTransferOtherConcept.Remove(_portfolioTransferOtherConcept)
            INDGcOtherConcept.DataSource = Nothing
            INDGcOtherConcept.DataSource = _listPortfolioTransferOtherConcept

            ctrAdvance.PrintAdvance()
        End If
    End Sub

#End Region

#Region "IdEntityLoaded"

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    ''' 
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me._portfolioTransfer IsNot Nothing AndAlso Me._portfolioTransfer.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("OpenFromVituelContent"), MessageType.Question, ResourceManager.GetString("OpenFromVituelTitle"), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Await DeleteBlockedRecord()
                Code = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDBteCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

#End Region

#Region "PasteToGrid"

    Private Async Sub IndigoGridControl1_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        Await PasteToGrid(sender, e.Rows)
    End Sub

#End Region

#End Region

#Region "Buttons Bar"

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDBteCode.ButtonClick
        Buscar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        _portfolioTransfer.Status = 1
        _varImp = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        _portfolioTransfer.Status = 1
        _varImp = 2
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click anular.
    ''' </summary>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _portfolioTransfer.Status = 3
            _varImp = 3
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click confirmar.
    ''' </summary>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Me._portfolioTransfer.Status = 2
            _varImp = 4
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar y confirmar.
    ''' </summary>
    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Me._portfolioTransfer.Status = 2
            _varImp = 4
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir de la barra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, _portfolioTransfer.Id, 0, _portfolioTransfer.Id)
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._operativeUnitId = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.PortfolioSequenceDetail IsNot Nothing Then
                If Not Me._sequence.PortfolioSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub
#End Region

End Class