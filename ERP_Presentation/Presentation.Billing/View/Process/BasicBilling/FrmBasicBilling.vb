'***********************************************************************
' Assembly         : Presentacion.Billing
' Author           : Miguel Angel Fonseca Castro
' Created          : 2018-11-13
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Text
Imports DevExpress.Spreadsheet
Imports DevExpress.Xpo
Imports DevExpress.XtraSpreadsheet
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Infrastructure.Data.Xpo.CommonRepository
Imports Infrastructure.Data.Xpo.PortfolioRepository
Imports Presentation.Base
Imports Presentation.Billing.MVP
Imports Presentation.Controls
Imports Presentation.Treasury

#End Region

Public Class FrmBasicBilling
    Implements IBasicBilling, ICustomizableForm

#Region "Builder"

    ''' <summary>
    ''' Se inicializa una isntancia de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        InitializeComponent()

        Me._ctrTotal = New CtrTotalInvoice()
        Me._ctrTotal.SetInfoFunction(AddressOf Me.GetInvoiceInfo)
        Me._ctrTotal.PrintInfo()
        Me._ctrTotal.Dock = System.Windows.Forms.DockStyle.Fill
        AdditionalControlPanel.Controls.Add(Me._ctrTotal)
        INDEsbDetails.AddRangeColumns("Tipo Detalle", "Código", "Lote", "Cantidad", "Precio Unitario", "% Descuento")
    End Sub

#End Region

#Region "Globals"

    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Billing"

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _operativeUnitId As Int32

    ''' <summary>
    ''' Tercero relacionado con la empresa
    ''' </summary>
    Private _currentCompany As ThirdParty

    ''' <summary>
    ''' Parametros de Facturación
    ''' </summary>
    Private _parameterBilling As SettingsBilling

    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Private _indigoSession As SessionValues

    ''' <summary>
    ''' variable que guarda el numero de decimales que se van a usar
    ''' </summary>
    Private _roundDecimalLevel As Integer = 2

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As BillingSequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _currentSequenceId As Int64

    ''' <summary>
    ''' presenter del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private _presenter As PBasicBilling

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _record As BlockRecordBilling

    ''' <summary>
    ''' bandera para identificarcuando se consulta un registro
    ''' </summary>
    ''' <remarks></remarks>
    Private _isLoad As Boolean

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Private _varImp As Integer

    ''' <summary>
    ''' indice del registro que se esta editando
    ''' </summary>
    ''' <remarks></remarks>
    Dim _indexEditRecord As Integer

    ''' <summary>
    ''' Control para establecer el total de la Factura
    ''' </summary>
    Private _ctrTotal As CtrTotalInvoice

    ''' <summary>
    ''' Tercero relacionado con el cliente
    ''' </summary>
    Private _currentCustomer As Infrastructure.Data.Xpo.CommonRepository.CommonThirdPartyXpo

    ''' <summary>
    ''' representa la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private _basicBilling As BasicBilling

    ''' <summary>
    ''' listado de detalle a eliminar de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private _listBasicBillingDetailDelete As List(Of BasicBillingDetail)

    ''' <summary>
    ''' listado de Obsequios a eliminar de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private _listBasicBillingGiftsDelete As List(Of BasicBillingGifts)

    ''' <summary>
    ''' variable privada que guarda los datos de la moneda oficial para no reconsultar
    ''' </summary>
    Private _currencyDataOfficial As Currency

    Private FieldMask As String

    ''' <summary>
    ''' tasa de cambio
    ''' </summary>
    Private _tRM As TRM

    ''' <summary>
    ''' variable que almacena la abreviacion de la moneda
    ''' </summary>
    Private _currencyASelected As String

    ''' <summary>
    ''' variable que almacena el tipo de contribuyente del tercero relacionado con el ciente
    ''' </summary>
    Private _ContributionType As Byte

    ''' <summary>
    ''' bandera para identificar cuando el registro ha sido importado
    ''' </summary>
    ''' <remarks></remarks>
    Private _isImported As Boolean

    ''' <summary>
    ''' Indica si en los parametros de la compañia se maneja la actividad economica
    ''' </summary>
    Private TransactionEconomicActivity As Boolean = False

    ''' <summary>
    ''' Indica si el tercero relacionado al cliente es facturador electronico
    ''' </summary>
    Private IsElectronicBillerThirdParty As Boolean = False
#End Region

#Region "Fields"

    Public ReadOnly Property MyTag As Object Implements IBasicBilling.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IBasicBilling.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    Public WriteOnly Property ActionsOnControls As Boolean Implements IBasicBilling.ActionsOnControls
        Set(value As Boolean)
            INDLcBasicBilling.BeginUpdate()

            INDBteCode.Enabled = Not value
            INDSleBillingAuthorization.Enabled = value
            INDMeDetail.Enabled = value
            INDSleCustomer.Enabled = value
            INDSleAddress.Enabled = value
            INDGleSaleModality.Enabled = value
            INDSleWarehouse.Enabled = value
            INDSleConditionSales.Enabled = value
            INDSleEconomicActivity.Enabled = value

            INDSleBudgetaryEntityId.Enabled = value
            INDSleBudgetaryValidityId.Enabled = value
            INDSleBudgetId.Enabled = value

            INDBtnAddDetails.Enabled = False
            INDBtnAddGifts.Enabled = False

            If value AndAlso Me._basicBilling.Status = 1 Then
                INDBtnAddDetails.Enabled = value
                INDBtnAddGifts.Enabled = value
            End If

            INDGcDetails.Enabled = value
            INDGcGifts.Enabled = value
            INDLcBasicBilling.EndUpdate()
            INDSleCurrency.Enabled = value

            If value Then
                INDSleBillingAuthorization.Focus()
            Else
                INDBteCode.Focus()
            End If

            'ActionsOnControl de fecha según cultura Colombia
            If indigo.LanguageCulture = "es-CO" Then
                INDDteDocumentDate.Enabled = False
            Else
                INDDteDocumentDate.Enabled = value
            End If

        End Set
    End Property

    Public Property Sequence As BillingSequence Implements IBasicBilling.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As BillingSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As BillingSequenceDetail In Me._sequence.BillingSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

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

#Region "Properties"

    Public Property Code As String Implements IBasicBilling.Code
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

    Public Property DocumentDate As Date Implements IBasicBilling.DocumentDate
        Get
            Return INDDteDocumentDate.EditValue
        End Get
        Set(value As Date)
            INDDteDocumentDate.EditValue = value
        End Set
    End Property

    Public Property Description As String Implements IBasicBilling.Description
        Get
            Return INDMeDetail.EditValue
        End Get
        Set(value As String)
            INDMeDetail.EditValue = value
        End Set
    End Property

    Public Property SaleModality As Byte Implements IBasicBilling.SaleModality
        Get
            Return INDGleSaleModality.EditValue
        End Get
        Set(value As Byte)
            INDGleSaleModality.EditValue = value
        End Set
    End Property

    Public Property OperatingUnitId As Integer Implements IBasicBilling.OperatingUnitId
        Get
            Return Me._operativeUnitId
        End Get
        Set(value As Integer)
            Me._operativeUnitId = value
        End Set
    End Property

    Public Property BillingAuthorizationId As Integer Implements IBasicBilling.BillingAuthorizationId
        Get
            Return INDSleBillingAuthorization.EditValue
        End Get
        Set(value As Integer)
            INDSleBillingAuthorization.EditValue = value
        End Set
    End Property

    Public Property ThirdPartyCustomerId As String Implements IBasicBilling.ThirdPartyCustomerId
        Get
            Return INDSleCustomer.EditValue
        End Get
        Set(value As String)
            INDSleCustomer.EditValue = value
        End Set
    End Property

    Property MainAccountId As Integer? Implements IBasicBilling.MainAccountId

    Public Property AddressId As Integer Implements IBasicBilling.AddressId
        Get
            Return INDSleAddress.EditValue
        End Get
        Set(value As Integer)
            INDSleAddress.EditValue = value
        End Set
    End Property

    Public Property WarehouseId As Integer? Implements IBasicBilling.WarehouseId
        Get
            Return INDSleWarehouse.EditValue
        End Get
        Set(value As Integer?)
            INDSleWarehouse.EditValue = value
        End Set
    End Property

    Public Property ConditionSalesId As Integer? Implements IBasicBilling.ConditionSalesId
        Get
            Return INDSleConditionSales.EditValue
        End Get
        Set(value As Integer?)
            INDSleConditionSales.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que obtiene o establece la actividad economica
    ''' </summary>
    ''' <returns></returns>
    Public Property EconomicActivityId As Integer?
        Get
            Return INDSleEconomicActivity.EditValue
        End Get
        Set(value As Integer?)
            INDSleEconomicActivity.EditValue = value
        End Set
    End Property

    Public Property Value As Decimal Implements IBasicBilling.Value
        Get
            Return Utils.RoundValue(INDTxtValue.EditValue, Me.RoundLevel)
        End Get
        Set(value As Decimal)
            INDTxtValue.EditValue = value
        End Set
    End Property

    Public Property ValueDiscount As Decimal Implements IBasicBilling.ValueDiscount
        Get
            Return Utils.RoundValue(INDTxtValueDiscount.EditValue, Me.RoundLevel)
        End Get
        Set(value As Decimal)
            INDTxtValueDiscount.EditValue = value
        End Set
    End Property

    Public Property ValueIVA As Decimal Implements IBasicBilling.ValueIVA
        Get
            Return Utils.RoundValue(INDTxtValueIVA.EditValue, Me.RoundLevel)
        End Get
        Set(value As Decimal)
            INDTxtValueIVA.EditValue = value
        End Set
    End Property

    Public Property WithholdingTax As Decimal Implements IBasicBilling.WithholdingTax
        Get
            Return Utils.RoundValue(INDTxtWithholdingTax.EditValue, Me.RoundLevel)
        End Get
        Set(value As Decimal)
            INDTxtWithholdingTax.EditValue = value
        End Set
    End Property

    Public ReadOnly Property ApplyRetention As Boolean Implements IBasicBilling.ApplyRetention
        Get
            If Me._currentCompany IsNot Nothing AndAlso Me._currentCompany.RetentionType = 2 Then
                Return True
            End If
            Return False
        End Get
    End Property

    Public ReadOnly Property RetentionIdIVA As Integer? Implements IBasicBilling.RetentionIdIVA
        Get
            If Me._parameterBilling IsNot Nothing Then
                Return Me._parameterBilling.ReteIVAConceptId
            End If
            Return Nothing
        End Get
    End Property

    Public ReadOnly Property RetentionPercentageIVA As Decimal Implements IBasicBilling.RetentionPercentageIVA
        Get
            If Me.ApplyRetention AndAlso Me._currentCustomer IsNot Nothing AndAlso Me._parameterBilling IsNot Nothing Then
                ' Un agente de retención (mayor ContributionType) retiene a contribuyentes menores
                If Me._currentCompany.ContributionType > 0 AndAlso Me._currentCompany.ContributionType < Me._currentCustomer.ContributionType Then
                    Return Me._parameterBilling.RetentionPercentageIVA
                End If
            End If
            Return 0
        End Get
    End Property

    Public Property WithholdingIVA As Decimal Implements IBasicBilling.WithholdingIVA
        Get
            Return Utils.RoundValue(INDTxtWithholdingIVA.EditValue, Me.RoundLevel)
        End Get
        Set(value As Decimal)
            INDTxtWithholdingIVA.EditValue = value
        End Set
    End Property

    Public Property WithholdingICA As Decimal Implements IBasicBilling.WithholdingICA
        Get
            Return Utils.RoundValue(INDTxtWithholdingICA.EditValue, Me.RoundLevel)
        End Get
        Set(value As Decimal)
            INDTxtWithholdingICA.EditValue = value
        End Set
    End Property

    Public Property TotalValue As Decimal Implements IBasicBilling.TotalValue
        Get
            Return Utils.RoundValue(INDTxtTotalValue.EditValue, Me.RoundLevel)
        End Get
        Set(value As Decimal)
            INDTxtTotalValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establece el id de la moneda del documento
    ''' establece opcionalmente la abreviacion
    ''' </summary>
    ''' <param name="currencyAbbreviation"></param>
    ''' <returns></returns>
    Public Property CurrencyId(Optional currencyAbbreviation As String = Nothing) As Integer Implements IBasicBilling.CurrencyId
        Get
            Return INDSleCurrency.EditValue
        End Get
        Set(value As Integer)
            If (value <> indigo?.OfficialCurrencyId) AndAlso String.IsNullOrEmpty(currencyAbbreviation) Then
                value = indigo?.OfficialCurrencyId
                currencyAbbreviation = indigo?.CurrencyISO4217
            End If
            INDSleCurrency.EditValue = value
            INDSleCurrency.Properties.NullText = currencyAbbreviation
            SetFormatCurrencyUI(currencyAbbreviation)
        End Set
    End Property

    ''' <summary>
    ''' propiedad de lectura que lee la abbreviacion de la moneda
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property CurrencyASelected As String
        Get
            Return _currencyASelected
        End Get
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establece el trm
    ''' </summary>
    ''' <returns></returns>
    Private Property TRM As TRM
        Get
            Return _tRM
        End Get
        Set(value As TRM)
            _tRM = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que homologa los niveles de redondeo para usar correctamente el Utils RoundLevel
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property RoundLevel As Integer
        Get
            Select Case Me._roundDecimalLevel
                Case 2
                    Return 0
                Case 1
                    Return 5
                Case 0
                    Return 1
                Case -1
                    Return 2
                Case -2
                    Return 3
                Case -3
                    Return 4
                Case Else
                    Return 0
            End Select
        End Get
    End Property

    Public ReadOnly Property RequiresConditionsSale As Boolean
        Get
            Dim _requiresConditionsSale As Boolean

            If _parameterBilling IsNot Nothing Then
                _requiresConditionsSale = _parameterBilling.requiresConditionsSale
            End If

            Return _requiresConditionsSale
        End Get
    End Property




#Region "Budget Interface"

    Public ReadOnly Property BudgetInterface As Boolean
        Get
            Dim _budgetInterface As Boolean

            If _parameterBilling IsNot Nothing Then
                _budgetInterface = _parameterBilling.BudgetInterface
            End If

            Return _budgetInterface
        End Get
    End Property

    Public Property BudgetaryEntityId As Integer? Implements IBasicBilling.BudgetaryEntityId
        Get
            Return INDSleBudgetaryEntityId.EditValue
        End Get
        Set(value As Integer?)
            INDSleBudgetaryEntityId.EditValue = value
        End Set
    End Property

    Public Property BudgetaryEntityXpo As XPCollection Implements IBasicBilling.BudgetaryEntityXpo
        Get
            Return INDSleBudgetaryEntityId.Properties.DataSource
        End Get
        Set(value As XPCollection)
            INDSleBudgetaryEntityId.Properties.DataSource = value
        End Set
    End Property

    Public Property BudgetaryValidityId As Integer? Implements IBasicBilling.BudgetaryValidityId
        Get
            Return INDSleBudgetaryValidityId.EditValue
        End Get
        Set(value As Integer?)
            INDSleBudgetaryValidityId.EditValue = value
        End Set
    End Property

    Public Property BudgetaryValidityXpo As XPCollection Implements IBasicBilling.BudgetaryValidityXpo
        Get
            Return INDSleBudgetaryValidityId.Properties.DataSource
        End Get
        Set(value As XPCollection)
            INDSleBudgetaryValidityId.Properties.DataSource = value
        End Set
    End Property

    Public Property BudgetId As Integer? Implements IBasicBilling.BudgetId
        Get
            Return INDSleBudgetId.EditValue
        End Get
        Set(value As Integer?)
            INDSleBudgetId.EditValue = value
        End Set
    End Property

    Public Property BudgetXpo As XPCollection Implements IBasicBilling.BudgetXpo
        Get
            Return INDSleBudgetId.Properties.DataSource
        End Get
        Set(value As XPCollection)
            INDSleBudgetId.Properties.DataSource = value
        End Set
    End Property

#End Region

#End Region

#Region "XPO"

    Public Property BillingAuthorizationXpo As XPInstantFeedbackSource Implements IBasicBilling.BillingAuthorizationXpo
        Get
            Return CType(INDSleBillingAuthorization.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleBillingAuthorization.Properties.DataSource = value
        End Set
    End Property

    Public Property CustomerXpo As XPInstantFeedbackSource Implements IBasicBilling.CustomerXpo
        Get
            Return CType(INDSleCustomer.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleCustomer.Properties.DataSource = value
        End Set
    End Property

    Public Property AddressXpo As XPInstantFeedbackSource Implements IBasicBilling.AddressXpo
        Get
            Return CType(INDSleAddress.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleAddress.Properties.DataSource = value
        End Set
    End Property

    Public Property WarehouseXpo As XPInstantFeedbackSource Implements IBasicBilling.WarehouseXpo
        Get
            Return CType(INDSleWarehouse.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleWarehouse.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' datasource del combo de moneda
    ''' </summary>
    ''' <returns></returns>
    Public Property CurrencyXpo As XPInstantFeedbackSource Implements IBasicBilling.CurrencyXpo
        Get
            Return TryCast(INDSleCurrency.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleCurrency.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "Crud"

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
                              New ColumnInfo With {.Caption = "Fecha", .FieldName = "DocumentDate", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Cliente", .FieldName = "CustomerId.NitName", .ColumnWidth = 300},
                              New ColumnInfo With {.Caption = "Factura", .FieldName = "InvoiceId.InvoiceNumber", .ColumnWidth = 100, .ColumnAligment = DevExpress.Utils.HorzAlignment.Center},
                              New ColumnInfo With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = 200}}.ToList()
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListBasicBillings
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
    End Sub

    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If Me._sequence Is Nothing OrElse Me._sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewBasicBilling()
        End If
    End Sub

    Public Async Sub Guardar() Implements ICrudBase.Guardar
        Try
            If Me._basicBilling Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "No se ha instanciado un objeto a guardar"
                Exit Sub
            End If

            If Me._basicBilling.Status = 2 Then
                Mensaje(EeventViewerImages.Advertencia) = "Accion Equivocada"
                Exit Sub
            End If

            If Me._basicBilling.Status < 3 Then
                If Me.ValidateControls() = True Then
                    If INDGvDetails.RowCount = 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = "Se debe agregar un detalle"
                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If
                Dim errors = ValidateControlsForms()
                If errors.Length > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = errors
                    Exit Sub
                End If
                Me.AssigningValues()
            End If

            AsyncLoader(True)
            Using model As New MBasicBilling(MyTag)
                Dim result = Await model.SaveBasicBilling(Me._basicBilling)

                If result.StateResult Then
                    If Me._basicBilling.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._currentSequenceId).RemoveAt(0)
                        End If
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), result.ObjectEmbbeded.Code)
                    Else
                        If Me._basicBilling.Status = 3 Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        End If
                    End If

                    Me._basicBilling = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)

                    Select Case Me._varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, Me._basicBilling.Id, 0, Me._basicBilling.Id, Me.OperatingUnitId)
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, Me._basicBilling.Id, 0, Me._basicBilling.Id, Me.OperatingUnitId)
                        Case 3
                            Me.BarraBotones.PrintReport(PrintReportAction.Confirm, Me._basicBilling.Id, 0, Me._basicBilling.Id, Me.OperatingUnitId)
                        Case 4
                            Me.BarraBotones.PrintReport(PrintReportAction.Cancel, Me._basicBilling.Id, 0, Me._basicBilling.Id, Me.OperatingUnitId)
                    End Select

                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    If result.StateResult = False And result.StateResultAux = False Then
                        If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 AndAlso result.MessageResult.First().ToString() <> "" Then
                            Mensaje(EeventViewerImages.Advertencia) = String.Join(vbCrLf, result.MessageResult)
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = result.Message
                        End If
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If

                    AsyncLoader(False)
                    INDBteCode.Enabled = False
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar
    End Sub

    Private Async Sub SaveAndConfirmBasicBilling()
        Try
            If Me.ValidateControls() = True Then
                If INDGvDetails.RowCount = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "Se debe agregar un detalle"
                    Exit Sub
                End If

                If INDLciEconomicActivity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    If EconomicActivityId Is Nothing And IsElectronicBillerThirdParty Then
                        Mensaje(EeventViewerImages.Advertencia) = "Por favor asocie una Actividad Económica al Cliente"
                        Exit Sub
                    End If
                End If
            Else
                Exit Sub
            End If

            AssigningValues()
            AsyncLoader(True)

            Dim cashReceipts As CashReceipts = Nothing
            Dim requiredCashReceip As Boolean = True
            Dim thirdParty As Integer = Integer.Parse(ThirdPartyCustomerId.Split("-")(0))

            Dim resultFindAdvances = String.Empty

            'Verificamos si la modalidad de venta es contado o crédito para saber si se debe pedir el recibo de caja
            If CByte(INDGleSaleModality.EditValue) = 2 Then
                If MessageIndigo.Show("¿Desea registrar Anticipo?", MessageType.Question, Me.Text, Botones.SiNo) <> System.Windows.Forms.DialogResult.Yes Then
                    requiredCashReceip = False
                End If
            Else
                resultFindAdvances = Await GetIfExistsPortfolioAdvances(thirdParty)
            End If

            If requiredCashReceip Then
                Dim currentBalance As Decimal = Me._basicBilling.TotalValue

                If Not String.IsNullOrEmpty(resultFindAdvances) Then
                    'Show de Crossing Popup
                    Using formAdvance As New FrmBasicBillingAdvanceCrossing(eSourceDocument.BasicBilling,
                                                                            New Currency With {.Id = Me.CurrencyId, .Abbreviation = Me._currencyASelected},
                                                                            thirdParty, Me._basicBilling.TotalValue, MainAccountId)
                        AddHandler formAdvance.RunLiquidateBasicBilling, AddressOf RunLiquidateBasicBilling
                        Dim transparent As New FrmTransparent(formAdvance, False)
                        If transparent.ShowDialog(Me) <> System.Windows.Forms.DialogResult.OK Then
                            Mensaje(EeventViewerImages.Advertencia) = "Es necesario realizar un cruce de anticipo para continuar."
                            AsyncLoader(False)
                            Exit Sub
                        End If
                        currentBalance = formAdvance.GetCurrentBalance
                    End Using
                End If

                If currentBalance > 0 Then
                    'abro el popup de recibos de caja para que se cree 
                    Using frm As New FrmCashReceipts
                        frm.subCashReceipts = True
                        frm.IdThirdParty = thirdParty
                        frm.SourceDocument = eSourceDocument.BasicBilling
                        frm.IdMainAccount = Me.MainAccountId
                        frm.IdThirdPartyMainAccount = Me.MainAccountId
                        frm.CashDefaultValue = currentBalance
                        frm.ValueProductInvoice = currentBalance
                        frm.ParentCurrencyId(Me.CurrencyASelected) = Me.CurrencyId
                        Dim transparent As New FrmTransparent(frm, False)
                        If transparent.ShowDialog(Me) <> System.Windows.Forms.DialogResult.OK Then
                            Mensaje(EeventViewerImages.Advertencia) = "Se debe crear un recibo de caja para continuar con el proceso"
                            AsyncLoader(False)
                            Exit Sub
                        End If
                        cashReceipts = frm.CashReceiptProductInvoice
                        cashReceipts.EntityName = NameOf(BasicBilling)
                    End Using
                End If
            End If

            Using model As New MBasicBilling(MyTag)
                Dim result = Await model.SaveAndConfirmBasicBilling(Me._basicBilling, cashReceipts)
                If result.StateResult = True And result.StateResultAux = True Then
                    If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                        If DicSequense.Count > 0 Then
                            Me.DicSequense.Remove(Me._currentSequenceId)
                        End If
                    End If

                    Mensaje(EeventViewerImages.Informacion) = result.Message

                    Me._basicBilling = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Me.BarraBotones.PrintReport(PrintReportAction.Confirm, Me._basicBilling.Id, 0, Me._basicBilling.Id)

                    AsyncLoader(False)
                    Me.Deshacer()
                ElseIf result.StateResult = True And result.StateResultAux = False Then
                    If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                        If DicSequense.Count > 0 Then
                            Me.DicSequense.Remove(Me._currentSequenceId)
                        End If
                    End If

                    Mensaje(EeventViewerImages.Advertencia) = result.Message

                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Me.BarraBotones.PrintReport(PrintReportAction.Confirm, Me._basicBilling.Id, 0, Me._basicBilling.Id)

                    AsyncLoader(False)
                    Me.Deshacer()
                ElseIf result.StateResult = False And result.StateResultAux = False Then
                    If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 AndAlso result.MessageResult.First().ToString() <> "" Then
                        Mensaje(EeventViewerImages.Advertencia) = String.Join(vbCrLf, result.MessageResult)
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If

                    If Me._basicBilling.Id > 0 Then
                        Me._basicBilling = Await model.GetBasicBillingByCode(Code)
                    Else
                        Me._basicBilling = New BasicBilling With {.Status = 1}
                    End If

                    AsyncLoader(False)
                    INDBteCode.Enabled = False
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Reversar o Anular una facturacion basica
    ''' </summary>
    ''' <param name="_basicBilling"></param>
    Public Async Sub Reversar(_basicBilling As BasicBilling)

        'Verifico que no se encuentre ya con el estado en 3 (anulado)
        If _basicBilling.Status = 3 Then
            Mensaje(EeventViewerImages.Advertencia) = "Ya se encuentra anulado"
            Exit Sub
        End If

        'Abrimos el modal que pide la razon de anulacion
        Try
            Dim reversalReasonId As Integer = 0
            Dim reversalDescription As String = String.Empty
            AsyncLoader(True)

            'Llamar el pop-up Motivos de Anulación
            Using PopUpAnnulmentReason As New PopUpAnnulmentReason()
                Dim transparent As New FrmTransparent(PopUpAnnulmentReason, False)
                If transparent.ShowDialog(Me) <> System.Windows.Forms.DialogResult.OK Then
                    AsyncLoader(False)
                    Exit Sub
                Else
                    reversalReasonId = PopUpAnnulmentReason.ReversalReasonId
                    reversalDescription = PopUpAnnulmentReason.ReversalDescription

                    'Validacion adicional de campos de Motivos de Anulacion
                    If reversalReasonId = Nothing OrElse reversalDescription Is Nothing Then
                        Mensaje(EeventViewerImages.Advertencia) = "Campos si llenar"
                        Exit Sub
                    ElseIf reversalDescription.Length > 200 Then
                        Mensaje(EeventViewerImages.Advertencia) = "La descripción de motivos de anulación no puede ser superior a 200 caracteres"
                        Exit Sub
                    End If

                    Using model As New MBasicBilling(MyTag)
                        AsyncLoader(True)

                        'Metodo para reversar y agregar los datos de anulacion en facturacion basica
                        Dim result = Await model.ReverseBasicBilling(_basicBilling, reversalReasonId, reversalDescription)

                        If result?.StateResult Then
                            MessageIndigo.Show(result.Message, MessageType.Information, Me.Text)
                            Me.Deshacer()
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = result?.Message
                        End If
                        AsyncLoader(False)
                    End Using
                End If
            End Using

        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try
    End Sub

#End Region

#Region "Bar Buttons"

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    Private Async Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing AndAlso operatingUnit.Id <> Me.OperatingUnitId Then
            Me.OperatingUnitId = operatingUnit.Id
            Await Me.LoadParameters()
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.BillingSequenceDetail IsNot Nothing Then
                If Not Me._sequence.BillingSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Nuevo()
    End Sub

    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDBteCode.ButtonClick
        Buscar()
    End Sub

    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Me._varImp = 1
        Guardar()
    End Sub

    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Me._varImp = 2
        Guardar()
    End Sub

    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Me.SaveAndConfirmBasicBilling()
        End If
    End Sub

    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Me.SaveAndConfirmBasicBilling()
        End If
    End Sub

    Private Async Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular


        If Me._basicBilling IsNot Nothing AndAlso Me._basicBilling.Status = 2 Then
            Try
                AsyncLoader(True)
                Using model As New MBasicBilling(MyTag)

                    'Objetener la lista de recibos de caja asociados
                    Dim getCashReceipt = model.GetCashReceiptsByBasicBillingInvoice(Me._basicBilling.InvoiceId)
                    Dim resultGettransfer = Await model.GetPortfolioTransferByBasicBilling(Me._basicBilling.InvoiceId)
                    Dim resultGetCashReceipt As List(Of CashReceipts)
                    Dim messageBuilder = New StringBuilder
                    Dim message As String = "Esta factura no tiene asociados recibos de caja"

                    If resultGettransfer?.Any() Then
                        Dim codesMessage = String.Join(", ", resultGettransfer.Select(Function(x) x.Code).ToList())
                        messageBuilder.AppendLine($"Cruce de anticipo vs CxC ({codesMessage})")
                    Else
                        Task.WaitAll(getCashReceipt)
                        resultGetCashReceipt = getCashReceipt.Result?.Where(Function(receipt) receipt.Status = 2).ToList()
                        If resultGetCashReceipt?.Any() Then
                            messageBuilder.AppendLine($"Recibos de caja ({String.Join(", ", resultGetCashReceipt.Select(Function(x) x.Code))})")
                        End If
                    End If

                    If messageBuilder.Length > 0 Then
                        message = $"Esta factura tiene asociados los siguientes documentos: {messageBuilder.ToString()}"
                    End If

                    Dim CashMessage As StringBuilder = New StringBuilder
                    CashMessage.AppendLine(message)
                    CashMessage.AppendLine("¿Está seguro que desea reversar el documento?")
                    If MessageIndigo.Show(CashMessage.ToString(), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                        Me.Reversar(_basicBilling)
                    End If

                End Using
            Catch ex As Exception
                Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
            Finally
                AsyncLoader(False)
            End Try

        Else
            If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Me._basicBilling.Status = 3
                Me._varImp = 4
                Guardar()
            End If
        End If
    End Sub

    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, Me._basicBilling.Id, 0, Me._basicBilling.Id)
    End Sub

#End Region

#Region "Handles"

#Region "Load And Disposed"

    Private Async Sub FrmBasicBilling_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDLcBasicBilling, True)
        Me.HideFieldsByCurrentCulture()
        Me._doc = Nothing
        Me._indigoSession = SessionValues.Instance
        Me._presenter = New PBasicBilling(Me)
        Me._presenter.LoadDefinitionLayout()
        Me._presenter.GetSequence()
        Me.OperatingUnitId = BarraBotones.OperatingUnitValue

        Await Me.LoadCurrentCompany()
        Await Me.LoadParameters()
        Await Me.GetRoundingType()
        Await Me.LoadCompanySettings()
        Await SetDefaultValuesAndDatasource()

        Me.AddActionsColumns()
        Me.InitTuples()
        Me.Deshacer()
        Me.LoadStatus()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Me._operativeUnitId = Nothing
        Me._currentCompany = Nothing
        Me._parameterBilling = Nothing
        Me._indigoSession = Nothing
        Me._sequence = Nothing
        Me._currentSequenceId = Nothing
        Me._presenter = Nothing
        Me._record = Nothing
        Me._isLoad = Nothing
        Me._varImp = Nothing
        Me._indexEditRecord = Nothing
        Me._ctrTotal = Nothing
        Me._currentCustomer = Nothing
        Me._basicBilling = Nothing
        Me._listBasicBillingDetailDelete = Nothing
        Me._listBasicBillingGiftsDelete = Nothing
    End Sub

#End Region

#Region "ImportarInformacion"

    ''' <summary>
    ''' Evento que dispara el formulario de importar documentos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ImportarInformacion() Handles BarraBotones.Click_ImportarInformacion
        Using formulario As New FrmImportDocument
            AddHandler formulario.GetListBasicBillingDetails, AddressOf ReturnGetDocumentDetails
            formulario.Size = New System.Drawing.Size(800, 700)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent

            formulario.SaleModality = INDGleSaleModality.EditValue
            formulario.CustomerId = _currentCustomer?.Customer_ThirdParty.FirstOrDefault.Id
            formulario.CurrencyId = Me.CurrencyId

            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Método que retorna los registros importados desde el formulario de importacion de documentos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnGetDocumentDetails(sender As Object, e As AddDocumentsEventArgs)
        Dim Errors As New StringBuilder

        _isImported = True

        INDSleCustomer.Properties.NullText = e.ItemBasicBilling.CustomerName
        ThirdPartyCustomerId = e.ItemBasicBilling.ThirdPartyCustomerId
        SaleModality = e.ItemBasicBilling.SaleModality
        CurrencyId(e.ItemBasicBilling.CurrencyAbbreviation) = e.ItemBasicBilling.CurrencyId

        If e.ItemBasicBilling.ConditionSalesId IsNot Nothing Then
            INDLciConditionSales.HideControl(Not RequiresConditionsSale)
            INDLciConditionSales.ShowInCustomizationForm = Not RequiresConditionsSale
            ConditionSalesId = e.ItemBasicBilling.ConditionSalesId
        End If

        Dim DictionaryItemsDuplicated As New Dictionary(Of Byte?, String)
        For Each item In e.ListBasicbillingDetail
            If DictionaryItemsDuplicated.ContainsKey(item.DetailType) AndAlso DictionaryItemsDuplicated.ContainsValue(item.CodeName) Then
                Continue For
            End If

            If Me._basicBilling.BasicBillingDetail.Any(
            Function(x) x.DetailType = item.DetailType AndAlso
                        (
                            (x.DetailType = 1 AndAlso x.ProductId = item.ProductId) OrElse
                            (x.DetailType = 2 AndAlso IIf(item.ServicesProvidedId IsNot Nothing, x.ServicesProvidedId = item.ServicesProvidedId, x.BillingConceptId = item.BillingConceptId)) OrElse
                            (x.DetailType = 3 AndAlso x.PhysicalAssetId = item.PhysicalAssetId) OrElse
                            (x.DetailType = 4 AndAlso x.PhysicalAssetPartId = item.PhysicalAssetPartId)
                            )
            ) Then

                Errors.AppendLine("El Item " + item.CodeName + " ya esta agregado")
                DictionaryItemsDuplicated.Add(item.DetailType, item.CodeName)

                Continue For
            End If

            _basicBilling.BasicBillingDetail.Add(item)
        Next

        Dim DictionaryGiftsDuplicated As New List(Of Integer)
        For Each item In e.ListGiftsDetail
            If DictionaryGiftsDuplicated.Contains(item.ProductId) Then
                Continue For
            End If

            If Me._basicBilling.BasicBillingGifts.Any(Function(x) x.ProductId = item.ProductId) Then

                Errors.AppendLine("El Obsequio " + item.ProductName + " ya esta agregado")
                DictionaryGiftsDuplicated.Add(item.ProductId)

                Continue For
            End If

            _basicBilling.BasicBillingGifts.Add(item)
        Next

        INDGcDetails.DataSource = Nothing
        INDGcDetails.DataSource = Me._basicBilling.BasicBillingDetail.ToList()

        INDGcGifts.DataSource = Nothing
        INDGcGifts.DataSource = _basicBilling.BasicBillingGifts.Where(Function(x) x.ChangeTracker.State <> ObjectState.Deleted).ToList()
        RefreshTotals()

        If Errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = Errors.ToString()
        End If
    End Sub
#End Region

#Region "Shown"
    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBteCode.Enabled Then
            INDBteCode.Focus()
        End If
    End Sub
#End Region

#Region "IdEntityLoaded"

    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me._basicBilling IsNot Nothing AndAlso Me._basicBilling.Id > 0 Then
            If (MessageIndigo.Show(BaseClass.obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, BaseClass.obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDBteCode.Text = Me.IdEntity.Trim()
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

#Region "FormClosing"
    Private Sub FrmBasicBilling_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub
#End Region

#Region "KeyDown"

    Private Async Sub INDBteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBteCode.KeyDown
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
                    Await Me.NewBasicBilling()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

#End Region

#Region "QueryPopUp"

    Private Sub INDSleBillingAuthorization_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleBillingAuthorization.QueryPopUp
        If BillingAuthorizationXpo Is Nothing Then
            Me._presenter.InitializeBillingAuthorizationXPO()
        End If
    End Sub

    Private Sub INDSleCustomer_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCustomer.QueryPopUp
        If CustomerXpo Is Nothing Then
            INDSleCustomer.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).CommonService.ListXPInstantFeedbackSource(Of ViewThirdPartyCustomerXpo)()
        End If
    End Sub

    Private Sub INDSleWarehouse_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleWarehouse.QueryPopUp
        If WarehouseXpo Is Nothing Then
            Me._presenter.InitializeWarehouseXPO()
        End If
    End Sub

    Private Sub INDsleBudgetaryEntityId_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleBudgetaryEntityId.QueryPopUp
        If BudgetaryEntityXpo Is Nothing Then
            Me._presenter.InitializeBudgetaryEntity()
        End If
    End Sub

    Private Sub INDsleBudgetaryValidityId_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleBudgetaryValidityId.QueryPopUp
        If BudgetaryValidityXpo Is Nothing AndAlso BudgetaryEntityId IsNot Nothing Then
            Me._presenter.InitializeBudgetaryValidity(BudgetaryEntityId)
        End If
    End Sub

    Private Sub INDSleBudgetId_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleBudgetId.QueryPopUp
        If BudgetXpo Is Nothing AndAlso BudgetaryValidityId IsNot Nothing Then
            Me._presenter.InitializeBasicBillingBudget(BudgetaryValidityId)
        End If
    End Sub

    ''' <summary>
    ''' evento que carga el datasource del combo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCurrency_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCurrency.QueryPopUp
        If CurrencyXpo Is Nothing Then
            Me._presenter.InitializateCurrency()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    Private Sub INDSleBillingAuthorization_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleBillingAuthorization.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("1517", Nothing, True)
        End If
    End Sub

    Private Sub INDSleCustomer_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleCustomer.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("503", Nothing, True)
        End If
    End Sub

    Private Sub INDSleAddress_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleAddress.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("532", Nothing, True)
        End If
    End Sub

    Private Sub INDSleWarehouse_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleWarehouse.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("302", Nothing, True)
        End If
    End Sub


#End Region

#Region "Click"

    Private Sub INDBtnAddDetails_Click(sender As Object, e As EventArgs) Handles INDBtnAddDetails.Click
        Using formulario As New FrmPopupBasicBillingDetail(_currency:=New Currency With {.Id = Me.CurrencyId, .Abbreviation = Me.CurrencyASelected},
                                                             _tRMValue:=If(Me.TRM Is Nothing, 1, Me.TRM.Value))
            Me.Cursor = BaseClass.ChangeCursorIndigo()
            formulario.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.7
            formulario.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.7
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.customerThirdParty = Me._currentCustomer
            formulario.ContributionType = _ContributionType
            formulario.IdOperativeUnit = Me._operativeUnitId
            formulario.AddressId = Me.AddressId
            formulario.RoundLevel = Me.RoundLevel
            formulario.MaximizeBox = True
            formulario.SetFieldMask = Me.FieldMask
            formulario.RoundDecimal = Me.RoundLevel
            AddHandler formulario.AddBasicBillingDetail, AddressOf ReturnAddBillingBasicDetail
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    Private Sub INDBtnAddGifts_Click(sender As Object, e As EventArgs) Handles INDBtnAddGifts.Click

        If _basicBilling.BasicBillingDetail.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No se pueden agregar Obsequios. Por favor agregar un producto de venta en el segmento 'Detalles'"
            Exit Sub
        End If

        Using formulario As New FrmPopupBasicBillingGifts
            Me.Cursor = BaseClass.ChangeCursorIndigo()
            formulario.Size = New System.Drawing.Size(800, 730)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            AddHandler formulario.AddBasicBillingDetail, AddressOf ReturnAddBillingBasicGifts
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

#End Region

#Region "EditValueChanged"

    Private Async Sub INDSleCustomer_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleCustomer.EditValueChanged
        If INDSleCustomer.EditValue IsNot Nothing AndAlso Not String.IsNullOrEmpty(ThirdPartyCustomerId) Then
            Await GetCustomer(Not Me._isLoad)
        End If
    End Sub

    Private Sub INDSleAddress_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleAddress.EditValueChanged
        INDBtnAddDetails.Enabled = False

        If AddressId > 0 AndAlso Me._basicBilling.Status = 1 Then
            INDBtnAddDetails.Enabled = True
        End If
    End Sub

    Private Sub INDsleBudgetaryEntityId_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleBudgetaryEntityId.EditValueChanged
        CleanBudgetInterface(1)

        If BudgetaryEntityId IsNot Nothing Then
            _presenter.InitializeBudgetaryValidity(BudgetaryEntityId)
            SetFirstOrDefaultValidity()
        End If
    End Sub

    Private Sub INDsleBudgetaryValidityId_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleBudgetaryValidityId.EditValueChanged
        CleanBudgetInterface(2)

        If BudgetaryValidityId IsNot Nothing Then
            _presenter.InitializeBasicBillingBudget(BudgetaryValidityId)
            SetDefaultBudget()
        End If
    End Sub

    ''' <summary>
    ''' evento al cambiar la moneda
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSleCurrency_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleCurrency.EditValueChanged
        Dim currencyAbbreviation = TryCast(TryCast(INDGvCurrency?.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)?.OriginalRow, CommonRepository.CommonCurrencyXpo)?.Abbreviation

        If INDSleCurrency.EditValue Is Nothing OrElse _isLoad OrElse String.IsNullOrEmpty(currencyAbbreviation) Then
            Exit Sub
        End If

        Await Me.ValidateCurrencyEditValue(Me.CurrencyId, currencyAbbreviation)
    End Sub

#End Region

#Region "EditValueChanging"

    Private Sub INDSleWarehouse_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDSleWarehouse.EditValueChanging
        If INDSleWarehouse.Properties.ReadOnly Then
            e.Cancel = True
        End If
    End Sub

#End Region

#Region "Click_ButtonAction"

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        If Me._basicBilling.Status > 1 Then
            If Me._basicBilling.Status = 2 Then
                Mensaje(EeventViewerImages.Advertencia) = "No se puede modificar el registro porque la factura esta confirmada"
            Else
                Mensaje(EeventViewerImages.Advertencia) = "No se puede modificar el registro porque la factura esta anulada"
            End If
            Exit Sub
        End If

        Dim basicBillingDetail = DirectCast(INDGvDetails.GetFocusedRow(), BasicBillingDetail)
        Select Case (sender.Tag)

            Case "Edit", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("Edit")
                Me._indexEditRecord = Me._basicBilling.BasicBillingDetail.IndexOf(basicBillingDetail)
                Using formulario As New FrmPopupBasicBillingDetail(_currency:=New Currency With {.Id = Me.CurrencyId, .Abbreviation = Me.CurrencyASelected},
                                                             _tRMValue:=If(Me.TRM Is Nothing, 1, Me.TRM.Value))
                    Me.Cursor = BaseClass.ChangeCursorIndigo()
                    formulario.Size = New System.Drawing.Size(1300, 730)
                    formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                    formulario.customerThirdParty = Me._currentCustomer
                    formulario.ListBasicBillingDetail = _basicBilling.BasicBillingDetail.ToList()
                    formulario.AddressId = Me.AddressId
                    formulario.IdOperativeUnit = Me._operativeUnitId
                    formulario.EditMode = True
                    formulario.RoundLevel = Me.RoundLevel
                    formulario.MaximizeBox = True
                    formulario.SetFieldMask = Me.FieldMask
                    formulario.BasicBillingDetail = basicBillingDetail
                    AddHandler formulario.AddBasicBillingDetail, AddressOf ReturnAddBillingBasicDetail
                    Dim transparent = New Base.FrmTransparent(formulario, False)
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    transparent.ShowDialog(Me)
                End Using

            Case "Remove", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("Remove")
                If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then

                    If Me._listBasicBillingDetailDelete Is Nothing Then
                        Me._listBasicBillingDetailDelete = New List(Of BasicBillingDetail)
                    End If

                    basicBillingDetail.MarkAsDeleted
                    _listBasicBillingDetailDelete.Add(basicBillingDetail)

                    INDGcDetails.DataSource = Nothing
                    INDGcDetails.DataSource = _basicBilling.BasicBillingDetail.Where(Function(x) x.ChangeTracker.State <> ObjectState.Deleted).ToList()
                    RefreshTotals()
                End If
        End Select
    End Sub

    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction, IndigoGridView2.ContexMenuActions
        If Me._basicBilling.Status > 1 Then
            If Me._basicBilling.Status = 2 Then
                Mensaje(EeventViewerImages.Advertencia) = "No se puede modificar el registro porque la factura esta confirmada"
            Else
                Mensaje(EeventViewerImages.Advertencia) = "No se puede modificar el registro porque la factura esta anulada"
            End If
            Exit Sub
        End If

        Dim BasicBillingGifts = INDGvGifts.GetFocusedRow()
        Select Case (sender.Tag)
            Case "Edit"
                EditGift(BasicBillingGifts)
            Case "Remove"
                DeleteGift(BasicBillingGifts)
        End Select
    End Sub

    Private Sub DeleteGift(Gift As BasicBillingGifts)
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            If _listBasicBillingGiftsDelete Is Nothing Then
                _listBasicBillingGiftsDelete = New List(Of BasicBillingGifts)
            End If

            Gift.MarkAsDeleted
            _listBasicBillingGiftsDelete.Add(Gift)

            INDGcGifts.DataSource = Nothing
            INDGcGifts.DataSource = _basicBilling.BasicBillingGifts.Where(Function(x) x.ChangeTracker.State <> ObjectState.Deleted).ToList()
        End If
        Me.EnableOrDisableCurrencyControl()
    End Sub

    Private Sub EditGift(Gift As BasicBillingGifts)
        Me._indexEditRecord = Me._basicBilling.BasicBillingGifts.IndexOf(Gift)
        Using formulario As New FrmPopupBasicBillingGifts
            Me.Cursor = BaseClass.ChangeCursorIndigo()
            formulario._BasicBillingGifts = Gift
            formulario._editMode = True
            formulario.Size = New System.Drawing.Size(800, 730)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            AddHandler formulario.AddBasicBillingDetail, AddressOf ReturnAddBillingBasicGifts
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

#End Region

#Region "PasteToGrid"

    Private Async Sub IndigoGridControl1_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        If Me._basicBilling.Status = 2 Then
            Mensaje(EeventViewerImages.Advertencia) = "El documento esta confirmado"
            Exit Sub
        End If

        'Validaciones
        If INDSleCustomer.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione un Cliente"
            Exit Sub
        End If
        If INDSleAddress.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione una Dirección"
            Exit Sub
        End If
        If INDSleWarehouse.EditValue Is Nothing Then
            If MessageIndigo.Show("Debe seleccionar un Almacen si va a agregar Productos. ¿Desea Continuar?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        End If

        Try
            AsyncLoader(True)

            Await PasteToGrid(e.Rows)

            Me.RefreshTotals()
            AsyncLoader(False)
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = ex.Message
            INDGvDetails.HideLoadingPanel()
            AsyncLoader(False)
        End Try
    End Sub

#End Region

#Region "ShowingEditor"
    Private Sub INDGvDetails_ShowingEditor(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDGvDetails.ShowingEditor
        Dim detail = DirectCast(INDGvDetails.GetFocusedRow, BasicBillingDetail)
        If detail.BasicBillingDetailItem Is Nothing OrElse detail.BasicBillingDetailItem.Count = 0 Then
            INDRpPceBatch.ReadOnly = True
        Else
            INDRpPceBatch.ReadOnly = False
        End If
    End Sub
#End Region

#Region "Popup"
    Private Sub INDRpPceBatch_Popup(sender As Object, e As EventArgs) Handles INDRpPceBatch.Popup
        Dim detail = DirectCast(INDGvDetails.GetFocusedRow, BasicBillingDetail)
        INDGcBatch.DataSource = Nothing
        INDGcBatch.DataSource = detail.BasicBillingDetailItem
    End Sub

#End Region

#End Region

#Region "Methods"
    ''' <summary>
    ''' Funcion para tener en cuenta los campos obligatorios
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControlsForms() As String
        Dim errorList As New StringBuilder()
        If (INDSleAddress.EditValue Is Nothing Or INDSleAddress.EditValue Is String.Empty) Then
            errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Dirección"))
        End If

        If INDLciEconomicActivity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If EconomicActivityId Is Nothing And IsElectronicBillerThirdParty Then
                errorList.AppendLine("Por favor asocie una Actividad Económica al Cliente")
            End If
        End If

        Return errorList.ToString()
    End Function
    Private Sub AddActionsColumns()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        ListActions.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(INDGvDetails, ListActions)
        IndigoGridView2.SetListAcction(INDGvGifts, ListActions)

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvDetails.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvGifts.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
    End Sub

    Private Sub InitTuples()
        Dim modalidades As New List(Of Tuple(Of Byte, String))()
        modalidades.Add(New Tuple(Of Byte, String)(1, "Contado"))
        modalidades.Add(New Tuple(Of Byte, String)(2, "Credito"))
        INDGleSaleModality.Properties.DataSource = modalidades
    End Sub

    Private Sub CleanControls()
        INDLcBasicBilling.BeginUpdate()

        Me.ReadOnlyControls(False)
        Me.DeleteBlockedRecord()
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()

        Me._doc = Nothing
        Me.Code = String.Empty
        Me.DocumentDate = GetDateServer()
        INDSleBillingAuthorization.EditValue = Nothing
        INDSleBillingAuthorization.Properties.NullText = String.Empty
        Me.Description = String.Empty
        INDSleCustomer.EditValue = Nothing
        INDSleCustomer.Properties.NullText = String.Empty
        AddressId = Nothing
        INDGleSaleModality.EditValue = Nothing
        INDSleWarehouse.EditValue = Nothing
        INDSleWarehouse.Properties.NullText = String.Empty
        _isImported = False

        ConditionSalesId = Nothing
        INDSleConditionSales.Properties.NullText = String.Empty
        EconomicActivityId = Nothing
        INDSleEconomicActivity.Properties.NullText = String.Empty
        VisibilityLabels()

        BudgetaryEntityId = Nothing
        INDSleBudgetaryEntityId.Properties.NullText = String.Empty
        BudgetaryValidityId = Nothing
        INDSleBudgetaryValidityId.Properties.NullText = String.Empty
        BudgetId = Nothing
        INDSleBudgetId.Properties.NullText = String.Empty

        Me._currentCustomer = Nothing
        INDBtnAddDetails.Enabled = False
        Me._listBasicBillingDetailDelete = Nothing
        INDGcDetails.DataSource = Nothing
        INDGcGifts.DataSource = Nothing
        Me._basicBilling = Nothing

        Me.Value = 0
        Me.ValueDiscount = 0
        Me.ValueIVA = 0
        Me.WithholdingTax = 0
        Me.WithholdingIVA = 0
        Me.WithholdingICA = 0
        Me.TotalValue = 0
        Me.CurrencyId(indigo.CurrencyISO4217) = indigo.OfficialCurrencyId
        Dim taskRounding = Me.GetRoundingType()
        Me.ReadOnlyMonetaryInfo()
        Me._ctrTotal.PrintInfo()

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDLcBasicBilling.EndUpdate()
        ActionsOnControls = False
        Task.WaitAll(taskRounding)
    End Sub

    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateRegistered"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    Private Async Function LoadCurrentCompany() As Task(Of Boolean)
        Try
            AsyncLoader(True)
            Using model As New Common.MVP.MThirdParty(MyTag)
                Me._currentCompany = Await model.GetThirdPartyAsync(Me.indigo.IndigoCompanyNit)
            End Using
            Return True
        Catch ex As Exception
            Me.Mensaje(EeventViewerImages.Advertencia) = ex.Message
            Return False
        Finally
            AsyncLoader(False)
        End Try
    End Function

    ''' <summary>
    ''' funcion encargada de establecer la mascara de los controles
    ''' </summary>
    ''' <param name="currencyId"></param>
    ''' <returns></returns>
    Private Async Function GetRoundingType(Optional currencyId As Integer? = Nothing) As Task
        Using model As New Presentation.Common.MVP.MCurrency(MyTag)

            Dim currencyData As Currency = Nothing
            Dim Num_Decimal As Integer

            If Me._currencyDataOfficial Is Nothing Then
                Me._currencyDataOfficial = Await model.GetCurrencyById(_indigoSession.OfficialCurrencyId)
            End If

            If currencyId Is Nothing OrElse currencyId = _indigoSession.OfficialCurrencyId Then
                currencyData = Me._currencyDataOfficial.Clone()
            Else
                currencyData = Await model.GetCurrencyById(currencyId)
            End If

            Select Case currencyData.RoundingType
                Case 1
                    Me.FieldMask = "C2"
                    Num_Decimal = 2
                Case 2
                    Me.FieldMask = "C1"
                    Num_Decimal = 1
                Case 3
                    Me.FieldMask = "C0"
                    Num_Decimal = 0
                Case 4
                    Me.FieldMask = "C0"
                    Num_Decimal = -1
                Case 5
                    Me.FieldMask = "C0"
                    Num_Decimal = -2
                Case 6
                    Me.FieldMask = "C0"
                    Num_Decimal = -3
            End Select
            Me._roundDecimalLevel = Num_Decimal

            'Mascara para los campos segun su redondeo
            INDColValue.DisplayFormat.FormatString = Me.FieldMask
            INDColTotalValue.DisplayFormat.FormatString = Me.FieldMask
            INDColValueDiscount.DisplayFormat.FormatString = Me.FieldMask

            INDTxtValueIVA.Properties.Mask.EditMask = Me.FieldMask
            INDTxtValueDiscount.Properties.Mask.EditMask = Me.FieldMask
            INDTxtValue.Properties.Mask.EditMask = Me.FieldMask
            INDTxtWithholdingICA.Properties.Mask.EditMask = Me.FieldMask
            INDTxtWithholdingIVA.Properties.Mask.EditMask = Me.FieldMask
            INDTxtWithholdingTax.Properties.Mask.EditMask = Me.FieldMask
            INDTxtTotalValue.Properties.Mask.EditMask = Me.FieldMask
        End Using
        Return
    End Function

    Private Async Function LoadParameters() As Task
        Using model As New MBillingSetting(MyTag)
            Me._parameterBilling = Await model.GetSettingsBillingByIdUnitOperative(Me.OperatingUnitId, False)
            If Me._parameterBilling Is Nothing OrElse Me._parameterBilling.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No se encontró parámetros de Facturación para la unidad operativa seleccionada"
                Exit Function
            End If

            If Me._parameterBilling.ReteIVAConceptId Is Nothing OrElse Me._parameterBilling.ReteIVAMainAccountId Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "No se ha parametrizado correctamente la facturación básica"
                Me._parameterBilling = Nothing
                Exit Function
            End If

            If Not BudgetInterface Then
                CleanBudgetInterface(0)
            End If
        End Using
    End Function

    ''' <summary>
    ''' Carga los parametros de la compañia
    ''' </summary>
    ''' <returns></returns>
    Private Async Function LoadCompanySettings() As Task
        Using model As New Accounting.MVP.MCompanySettings(Me.Tag)
            Dim setting = Await model.GetCompanySettings
            If setting IsNot Nothing Then
                TransactionEconomicActivity = setting.TransactionEconomicActivity
            End If
        End Using
    End Function

    ''' <summary>
    ''' Funcion que se encarga de setear informacion por defecto cargando previamente los Datasources
    ''' </summary>
    Private Async Function SetDefaultValuesAndDatasource() As Task
        Try
            VisibilityLabels()

            If RequiresConditionsSale Then
                Using model As New MLiquidation()
                    INDSleConditionSales.Properties.DataSource = Await model.ListConditionSales

                    Dim ConditionSales = TryCast(INDSleConditionSales.Properties.DataSource, List(Of ConditionSalesXpo))
                    If ConditionSales.Count = 1 Then
                        ConditionSalesId = ConditionSales.FirstOrDefault.Id
                    End If
                End Using
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Funcion que se encarga de setear la visibilidad de campos dependiendo de la parametrizacion
    ''' </summary>
    Private Sub VisibilityLabels()
        INDLciBudgetaryEntityId.AllowHide = Not BudgetInterface
        INDLciBudgetaryEntityId.ShowInCustomizationForm = Not BudgetInterface
        INDLciBudgetaryValidityId.AllowHide = Not BudgetInterface
        INDLciBudgetaryValidityId.ShowInCustomizationForm = Not BudgetInterface
        INDLciBudgetId.AllowHide = Not BudgetInterface
        INDLciBudgetId.ShowInCustomizationForm = Not BudgetInterface
        INDlygBudgetInterface.Visibility = If(BudgetInterface, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)

        INDLciConditionSales.HideControl(Not RequiresConditionsSale)
        INDLciConditionSales.ShowInCustomizationForm = Not RequiresConditionsSale

        INDLciEconomicActivity.Visibility = If(TransactionEconomicActivity, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
    End Sub

    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            Try
                If Me.BarraBotones.PermiteConsultar = False Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                    Exit Function
                End If
                Using model As New MBasicBilling(MyTag)
                    AsyncLoader(True)
                    INDLcBasicBilling.BeginUpdate()

                    Me._basicBilling = Await model.GetBasicBillingByCode(Code)
                    If Me._basicBilling IsNot Nothing AndAlso Me._basicBilling.Id > 0 Then

                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            Me._record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(Me._basicBilling.Id))

                            With Me._basicBilling
                                Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)

                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationUser"), .ConfirmationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationDate"), .ConfirmationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideUser"), .AnnulmentUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideDate"), .AnnulmentDate)

                                Me.BarraBotones.StatusRecordVisible = True
                                Me.BarraBotones.StatusRecord = .Status.ToString()
                                Select Case .Status
                                    Case 1
                                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
                                    Case 2
                                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAnnular)
                                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
                                        ReadOnlyControls(True)
                                    Case Else
                                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
                                        ReadOnlyControls(True)
                                End Select

                                Me._isLoad = True
                                Code = .Code
                                Description = .Description
                                SaleModality = .SaleModality
                                BarraBotones.OperatingUnitValue = .OperatingUnitId
                                BillingAuthorizationId = .BillingAuthorizationId
                                INDSleBillingAuthorization.Properties.NullText = .BillingAuthorizationName

                                ThirdPartyCustomerId = .ThirdPartyCustomerId
                                Await GetCustomer(Me._isLoad)
                                INDSleCustomer.Properties.NullText = .CustomerName
                                AddressId = .AddressId
                                INDSleAddress.Properties.NullText = .AddressName
                                WarehouseId = .WarehouseId
                                INDSleWarehouse.Properties.NullText = .WarehouseName

                                If .ConditionSalesId IsNot Nothing Then
                                    ConditionSalesId = .ConditionSalesId
                                    INDSleConditionSales.Properties.NullText = .CodeNameConditionSales

                                    If .Status <> 1 Then
                                        INDLciConditionSales.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                                    End If
                                End If

                                If .EconomicActivityId IsNot Nothing Then
                                    EconomicActivityId = .EconomicActivityId
                                    INDSleEconomicActivity.Properties.NullText = .CodeNameEconomicActivity

                                    If .Status <> 1 Then
                                        INDLciEconomicActivity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                                    End If
                                End If

                                Me.CurrencyId(.CurrencyAbbreviation) = .CurrencyId
                                Await Me.ValidateCurrencyEditValue(.CurrencyId, .CurrencyAbbreviation, _isLoad)
                                Me._isLoad = False

                                If BudgetInterface Then
                                    BudgetaryEntityId = .BudgetaryEntityId
                                    INDSleBudgetaryEntityId.Properties.NullText = .BudgetaryEntityDescription
                                    BudgetaryValidityId = .BudgetaryValidityId
                                    INDSleBudgetaryValidityId.Properties.NullText = .BudgetaryValidityDescription
                                    BudgetId = .BudgetId
                                    INDSleBudgetId.Properties.NullText = .BudgetDescription
                                End If

                                INDGcDetails.DataSource = Nothing
                                INDGcDetails.DataSource = _basicBilling.BasicBillingDetail.ToList()

                                INDGcGifts.DataSource = Nothing
                                INDGcGifts.DataSource = _basicBilling.BasicBillingGifts.ToList()

                                Me.RefreshTotals()

                                Me.DocumentDate = .DocumentDate
                                If _basicBilling.Status = 1 AndAlso indigo.LanguageCulture = "es-CO" Then
                                    DocumentDate = DateTime.Today
                                End If

                            End With

                            If _basicBilling.Status = 2 Then
                                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = _basicBilling.ThirdPartyEntityCopayId.HasValue
                            End If

                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._basicBilling.Code)

                            If Me._record.Id = 0 Then
                                Me._record = (Await ModelRecord.SaveBlockRecord(
                                        New BlockRecordBilling With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                            .CodUser = Me.indigo.UserIndigo, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .IdRecord = Me._basicBilling.Id})
                                        ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(BaseClass.obtenerRecurso(Eresources.RegistroBloqueado, Eform.Comunes), Me._record.CodUser, Me._record.NameUser, Me._record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, Me._record.CodUser)
                            End If

                            Me.BarraBotones.SetDocuments(Me._basicBilling.Id, Me.Tag.ToString(), Nothing, GetType(BasicBilling).Name)

                            AsyncLoader(False)
                            ActionsOnControls = True
                            Me.EnableOrDisableCurrencyControl()
                            INDSleBillingAuthorization.Focus()
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, Me._basicBilling.Id, 0, Me._basicBilling.Id)
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewBasicBilling()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                            Code = String.Empty
                            INDBteCode.Focus()
                        End If
                    End If

                    INDLcBasicBilling.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBteCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    Private Async Function NewBasicBilling() As Task
        If Me._currentCompany Is Nothing OrElse Me._currentCompany.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No se ha podido cargar el tercero de la empresa (" & Me.indigo.IndigoCompanyNit & ") actualmente seleccionada. Verifique que haya sido creada e intente de nuevo."
            Exit Function
        End If
        If Me._parameterBilling Is Nothing OrElse Me._parameterBilling.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No se encontró parámetros de Facturación para la unidad operativa seleccionada"
            Exit Function
        End If

        VisibilityLabels()
        Me._basicBilling = New BasicBilling() With {.Status = 1}
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            If Me.BarraBotones.PermissionsForm.ContainsKey(147) Then
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
            End If
        Else
            If Me._sequence.BillingSequenceDetail Is Nothing OrElse Me._sequence.BillingSequenceDetail.Count = 0 Then
                Me.Mensaje(EeventViewerImages.Advertencia) = "No se ha podido cargar el detalle de la Secuencia Numerica."
                Exit Function
            End If

            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._currentSequenceId = Me._sequence.BillingSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.BillingSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me.OperatingUnitId) Then
                    Me._currentSequenceId = Me._sequence.BillingSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me.OperatingUnitId).Id
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
                If Me.BarraBotones.PermissionsForm.ContainsKey(147) Then
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
                End If
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._currentSequenceId)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._currentSequenceId))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        If Me.BarraBotones.PermissionsForm.ContainsKey(147) Then
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
                        End If
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._currentSequenceId)) = Await model.GetNumericSequenseGroup(CInt(Me._currentSequenceId))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._currentSequenceId)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._currentSequenceId)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._currentSequenceId))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                            If Me.BarraBotones.PermissionsForm.ContainsKey(147) Then
                                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
                            End If
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
                    If Me.BarraBotones.PermissionsForm.ContainsKey(147) Then
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
                    End If
                End If
            End If
            BarraBotones.StatusRecordVisible = True
            BarraBotones.StatusRecord = "1"
        End If

        If BudgetInterface Then
            _presenter.InitializeBudgetaryEntity()
            Me.SetFirstOrDefaultEntity()
        End If
    End Function

    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        Code = ReturnValue
        If Code <> String.Empty Then
            Await LoadControls()
            If INDBteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBteCode.Enabled = False
        End If
    End Sub

    Private Sub ReturnAddBillingBasicDetail(sender As Object, e As AddBasicBillingDetailEventArgs)

        If e.EditMode Then
            Me._basicBilling.BasicBillingDetail.RemoveAt(Me._indexEditRecord)
            Me._basicBilling.BasicBillingDetail.Insert(Me._indexEditRecord, e.BasicBillingDetail)
        Else
            If Me._basicBilling.BasicBillingDetail.Any(
                Function(x) x.DetailType = e.BasicBillingDetail.DetailType AndAlso
                            (
                                (x.DetailType = 1 AndAlso x.ProductId = e.BasicBillingDetail.ProductId) OrElse
                                (x.DetailType = 2 AndAlso IIf(e.BasicBillingDetail.ServicesProvidedId IsNot Nothing, x.ServicesProvidedId = e.BasicBillingDetail.ServicesProvidedId, x.BillingConceptId = e.BasicBillingDetail.BillingConceptId)) OrElse
                                (x.DetailType = 3 AndAlso x.PhysicalAssetId = e.BasicBillingDetail.PhysicalAssetId) OrElse
                                (x.DetailType = 4 AndAlso x.PhysicalAssetPartId = e.BasicBillingDetail.PhysicalAssetPartId)
                            )
            ) Then
                Mensaje(EeventViewerImages.Advertencia) = "El Item " + e.BasicBillingDetail.CodeName + " ya esta agregado"
                Exit Sub
            End If

            Me._basicBilling.BasicBillingDetail.Add(e.BasicBillingDetail)
        End If

        INDGcDetails.DataSource = Nothing
        INDGcDetails.DataSource = Me._basicBilling.BasicBillingDetail.ToList()
        RefreshTotals()
    End Sub

    Private Sub ReturnAddBillingBasicGifts(sender As Object, e As AddBasicBillingGiftsEventArgs)

        If e.EditMode Then
            Me._basicBilling.BasicBillingGifts.RemoveAt(Me._indexEditRecord)
            Me._basicBilling.BasicBillingGifts.Insert(Me._indexEditRecord, e.BasicBillingGifts)
        Else
            If Me._basicBilling.BasicBillingGifts.Any(Function(x) x.ProductId = e.BasicBillingGifts.ProductId) Then
                Mensaje(EeventViewerImages.Advertencia) = "El Obsequio " + e.BasicBillingGifts.ProductName + " ya esta agregado"
                Exit Sub
            End If
            Me._basicBilling.BasicBillingGifts.Add(e.BasicBillingGifts)
        End If

        INDGcGifts.DataSource = Nothing
        INDGcGifts.DataSource = _basicBilling.BasicBillingGifts.Where(Function(x) x.ChangeTracker.State <> ObjectState.Deleted).ToList()
        Me.EnableOrDisableCurrencyControl()
    End Sub

    Private Async Sub DeleteBlockedRecord()
        If Me._record IsNot Nothing AndAlso Me._record.Id > 0 AndAlso Me._record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                Await model.DeleteBlockRecord(Me._record)
                Me._record = Nothing
            End Using
        Else
            Me.BarraBotones.EnableBarItems()
        End If
    End Sub

    Private Function GenerateDoc() As IndexedDocument2
        Dim content = String.Format(ResourceManager.GetString("FrmBasicBilling_IndexContent", MODULE_NAME), Me._basicBilling.Code, If(INDSleCustomer.Text = String.Empty, INDSleCustomer.Properties.NullText, INDSleCustomer.Text), DocumentDate)
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = content,
                .CreationDate = dateServer,
                .CreationUser = Me._indigoSession.UserIndigo & "-" & Me._indigoSession.UserIndigoName,
                .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me._basicBilling.Code & "#$",
                .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._basicBilling.Code),
                .Update = dateServer,
                .UpdateUser = Me._indigoSession.UserIndigo & "-" & Me._indigoSession.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me._indigoSession.UserIndigo & "-" & Me._indigoSession.UserIndigoName
            Me._doc.Content = content
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._basicBilling.Code)
            Return Me._doc
        End If
    End Function

    Private Sub ChangePropertyReadOnly(value As Boolean, Optional IsImported As Boolean = False)
        INDSleCustomer.Properties.ReadOnly = value
        INDSleWarehouse.Properties.ReadOnly = value

        If Not IsImported AndAlso INDSleAddress.EditValue IsNot Nothing Then
            INDSleAddress.Properties.ReadOnly = value
        End If
    End Sub

    Private Sub ReadOnlyMonetaryInfo()
        INDTxtValue.Properties.ReadOnly = True
        INDTxtValueDiscount.Properties.ReadOnly = True
        INDTxtValueIVA.Properties.ReadOnly = True
        INDTxtWithholdingTax.Properties.ReadOnly = True
        INDTxtWithholdingIVA.Properties.ReadOnly = True
        INDTxtWithholdingICA.Properties.ReadOnly = True
        INDTxtTotalValue.Properties.ReadOnly = True
    End Sub

    Private Function GetInvoiceInfo() As Tuple(Of String, String, String, String, Integer)
        Return New Tuple(Of String, String, String, String, Integer)(Me.Value, Me.ValueDiscount, Me.ValueIVA, (Me.Value - Me.ValueDiscount + Me.ValueIVA), If(Me._roundDecimalLevel < 0, 0, Me._roundDecimalLevel))
    End Function

    Private Sub RefreshTotals()
        Me.Value = 0
        Me.ValueDiscount = 0
        Me.ValueIVA = 0
        Me.WithholdingTax = 0
        Me.WithholdingIVA = 0
        Me.WithholdingICA = 0
        Me.TotalValue = 0
        Dim flag As Boolean = Me._basicBilling?.BasicBillingDetail IsNot Nothing AndAlso Me._basicBilling?.BasicBillingDetail?.Any()

        Me.ChangePropertyReadOnly(flag, _isImported)
        If flag Then
            For Each detail In Me._basicBilling.BasicBillingDetail
                detail.RoundLevel = Me.RoundLevel
                detail.CalculateValueDiscount()
            Next

            Me.Value = Me._basicBilling.BasicBillingDetail.Sum(Function(d) d.Value)
            Me.ValueDiscount = Me._basicBilling.BasicBillingDetail.Sum(Function(d) d.ValueDiscount)

            ' Calcular IVA usando Utils.RoundValue para ser coherente con el popup
            ' Se calcula el IVA de cada detalle sin redondear, se suma, y se aplica Utils.RoundValue al total
            Dim ivaTotal As Decimal = Me._basicBilling.BasicBillingDetail.Sum(Function(d) (d.Value - d.ValueDiscount) * d.PercentageIVA / 100)
            Me.ValueIVA = Utils.RoundValue(ivaTotal, Me.RoundLevel)

            If Me.ApplyRetention Then
                If Me.RetentionPercentageIVA > 0 AndAlso Me.Value >= Me._parameterBilling.RetentionBaseIVA Then
                    Me.WithholdingIVA = Math.Round(Me.ValueIVA * Me.RetentionPercentageIVA / 100, 2, MidpointRounding.AwayFromZero)
                End If

                For Each detail In Me._basicBilling.BasicBillingDetail
                    detail.WithholdingTax = 0
                    If Me.Value >= detail.RetentionBaseTax Then
                        detail.CalculateWithholdingTax()
                    End If

                    detail.WithholdingICA = 0
                    If Me.Value >= detail.RetentionBaseICA Then
                        detail.CalculateWithholdingICA()
                    End If
                Next
            End If

            Me.WithholdingTax = Me._basicBilling.BasicBillingDetail.Sum(Function(d) d.WithholdingTax)
            Me.WithholdingICA = Me._basicBilling.BasicBillingDetail.Sum(Function(d) d.WithholdingICA)

            ' Calcular TotalValue usando Utils.RoundValue para ser coherente con el popup
            ' Se suma (Value - ValueDiscount + IVA sin redondear) para cada detalle y se aplica redondeo al total
            Dim totalBruto As Decimal = Me._basicBilling.BasicBillingDetail.Sum(Function(d) d.Value - d.ValueDiscount + (d.Value - d.ValueDiscount) * d.PercentageIVA / 100 - d.WithholdingTax - d.WithholdingICA)
            Me.TotalValue = Utils.RoundValue(totalBruto, Me.RoundLevel) - Me.WithholdingIVA

        End If

        Me._ctrTotal.PrintInfo()
        Me.EnableOrDisableCurrencyControl()
    End Sub

    Private Sub AssigningValues()
        With Me._basicBilling
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .RoundLevel = Me.RoundLevel

            .Code = Me.Code
            .DocumentDate = Me.DocumentDate
            .Description = Me.Description
            .SaleModality = CByte(INDGleSaleModality.EditValue)
            .OperatingUnitId = Me.OperatingUnitId
            .BillingAuthorizationId = Me.BillingAuthorizationId
            .CustomerId = Integer.Parse(ThirdPartyCustomerId.Split("-")(1))
            .ThirdPartyCustomerId = ThirdPartyCustomerId
            .AddressId = Me.AddressId
            .WarehouseId = Me.WarehouseId
            .CurrencyId = Me.CurrencyId
            .CurrencyAbbreviation = Me.CurrencyASelected
            .Value = Me.Value
            .IsImported = _isImported

            If INDLciConditionSales.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .ConditionSalesId = ConditionSalesId
            Else
                .ConditionSalesId = Nothing
            End If

            If INDLciEconomicActivity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .EconomicActivityId = EconomicActivityId
            Else
                .EconomicActivityId = Nothing
            End If

            .ValueDiscount = Me.ValueDiscount
            .ValueIVA = Me.ValueIVA
            .WithholdingTax = Me.WithholdingTax
            .RetentionIdIVA = Me.RetentionIdIVA
            .RetentionPercentageIVA = Me.RetentionPercentageIVA
            .WithholdingIVA = Me.WithholdingIVA
            .WithholdingICA = Me.WithholdingICA

            .TotalValue = Me.TotalValue

            If BudgetInterface Then
                .BudgetId = BudgetId
            End If

            If _listBasicBillingDetailDelete IsNot Nothing AndAlso _listBasicBillingDetailDelete.Count > 0 Then
                For Each detail In Me._listBasicBillingDetailDelete
                    .BasicBillingDetail.Add(detail)
                Next
            End If

            If _listBasicBillingGiftsDelete IsNot Nothing AndAlso _listBasicBillingGiftsDelete.Count > 0 Then
                For Each detail In _listBasicBillingGiftsDelete
                    .BasicBillingGifts.Add(detail)
                Next
            End If
        End With
    End Sub

    Private Async Function PasteToGrid(data As List(Of List(Of String))) As Task
        INDGvDetails.ShowLoadingPanel()

        Using model As New MBasicBilling(MyTag)
            Dim result = Await model.SetBasicBillingDetailFromFile(AddressId, IIf(WarehouseId Is Nothing, 0, WarehouseId), data)
            If result.StateResult = False Then
                Mensaje(EeventViewerImages.Advertencia) = result.Message
                Me.Cursor = System.Windows.Forms.Cursors.Default
                INDGvDetails.HideLoadingPanel()
                Exit Function
            End If
            If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then

                _basicBilling.BasicBillingDetail.ToList().AddRange(result.ObjectEmbbeded.Where(Function(x) (From y In Me._basicBilling.BasicBillingDetail Where y.DetailType = x.DetailType AndAlso (
                                    (x.DetailType = 1 AndAlso x.ProductId = y.ProductId) OrElse
                                    (x.DetailType = 2 AndAlso x.BillingConceptId = y.BillingConceptId) OrElse
                                    (x.DetailType = 3 AndAlso x.PhysicalAssetId = y.PhysicalAssetId) OrElse
                                    (x.DetailType = 4 AndAlso x.PhysicalAssetPartId = y.PhysicalAssetPartId)
                                )).Count = 0))
            End If
            If result.ObjectEmbbededAux IsNot Nothing AndAlso result.ObjectEmbbededAux.Count > 0 Then
                Using formulario As New FrmListErrors(result.ObjectEmbbededAux)
                    formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                    Dim transparent As New FrmTransparent(formulario, False)
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    transparent.ShowDialog(Me)
                End Using
            End If
        End Using

        INDGcDetails.DataSource = Nothing
        INDGcDetails.DataSource = Me._basicBilling.BasicBillingDetail.ToList()

        Me.Cursor = System.Windows.Forms.Cursors.Default
        INDGvDetails.HideLoadingPanel()
    End Function

    Private Sub CleanBudgetInterface(level As Integer)
        If level < 1 Then
            BudgetaryEntityId = Nothing
            INDSleBudgetaryEntityId.Properties.NullText = String.Empty
        End If

        If level < 2 Then
            BudgetaryValidityId = Nothing
            INDSleBudgetaryValidityId.Properties.NullText = String.Empty
            BudgetaryValidityXpo = Nothing
        End If

        If level < 3 Then
            BudgetId = Nothing
            INDSleBudgetId.Properties.NullText = String.Empty
            BudgetXpo = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Metodo para seleccionar por defecto el primer registro si solo hay uno en entidades presupuestales
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetFirstOrDefaultEntity()
        If BudgetaryEntityXpo IsNot Nothing AndAlso BudgetaryEntityXpo.Count > 0 Then
            Dim item = (From l In BudgetaryEntityXpo Where l.Status = 1 Select l).FirstOrDefault
            If item IsNot Nothing Then
                BudgetaryEntityId = item.Id
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo para seleccionar por defecto el primer registro si solo hay uno en vigencias
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetFirstOrDefaultValidity()
        If BudgetaryValidityXpo IsNot Nothing AndAlso BudgetaryValidityXpo.Count > 0 Then
            Dim item = (From l In BudgetaryValidityXpo Where l.Status = 2 Select l).FirstOrDefault
            If item IsNot Nothing Then
                BudgetaryValidityId = item.Id
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo para seleccionar por defecto el primer registro si solo hay uno en vigencias
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetDefaultBudget()
        If BudgetXpo IsNot Nothing AndAlso BudgetXpo.Count > 0 Then
            If _parameterBilling IsNot Nothing AndAlso _parameterBilling.BasicBillingBudgetId IsNot Nothing Then
                BudgetId = _parameterBilling.BasicBillingBudgetId
            End If
        End If
    End Sub

    ''' <summary>
    ''' funcion que centraliza el formato de la moneda dentro del formulario
    ''' </summary>
    ''' <param name="currencyAbbreviation"></param>
    Private Sub SetFormatCurrencyUI(currencyAbbreviation As String)
        If String.IsNullOrEmpty(currencyAbbreviation) Then
            currencyAbbreviation = _indigoSession.CurrencyISO4217
            Mensaje(EeventViewerImages.Advertencia) = "La moneda esta llegando vacia"
        End If
        Me._currencyASelected = currencyAbbreviation
        Dim NumberFormat = currencyAbbreviation.GetNumberFormat
        NumberFormat.CurrencyDecimalDigits = If(Me._roundDecimalLevel < 0, 0, Me._roundDecimalLevel)
        Me.changeNumericFormatByCurrency(NumberFormat)
        Me._ctrTotal.CurrencyAbbreviation = currencyAbbreviation
        Me.RefreshTotals()
    End Sub

    ''' <summary>
    ''' habilita o deshabilita el control de moneda
    ''' </summary>
    Private Sub EnableOrDisableCurrencyControl()
        Dim flag = Not ((Me._basicBilling?.BasicBillingDetail IsNot Nothing AndAlso Me._basicBilling?.BasicBillingDetail?.ToList()?.Any()) _
                            OrElse
                            (Me._basicBilling?.BasicBillingGifts IsNot Nothing AndAlso Me._basicBilling?.BasicBillingGifts?.Any()))

        Me.INDSleCurrency.Enabled = flag
    End Sub

    ''' <summary>
    ''' funcion Valida y establece el evento cuando la moneda cambia 
    ''' </summary>
    ''' <returns></returns>
    Private Async Function ValidateCurrencyEditValue(_currencyId As Integer?,
                                                     currencyAbbreviation As String, Optional loadControl As Boolean = False) As Task

        If _currencyId Is Nothing OrElse String.IsNullOrEmpty(currencyAbbreviation) Then
            Return
        End If

        Await GetRoundingType(_currencyId)

        If Not loadControl Then
            Me.SetFormatCurrencyUI(currencyAbbreviation)
        End If

        If _currencyId = Me._indigoSession.OfficialCurrencyId Then
            Me.TRM = New TRM With {.CurrencyId = _currencyId, .OfficialCurrencyId = Me.indigo.OfficialCurrencyId, .Value = 1}
            Return
        End If

        Dim _stateResult = Await GetTRM(Me.indigo.OfficialCurrencyId, _currencyId)
        Return
    End Function

    ''' <summary>
    ''' Funcion que se encarga de consultar el TRM
    ''' </summary>
    ''' <param name="_currencyId"></param>
    ''' <param name="ToCurrencyId"></param>
    ''' <returns></returns>
    Private Async Function GetTRM(_currencyId As Integer, ToCurrencyId As Integer) As Task(Of Boolean)
        Using Model As New MBasicBilling(Me.Tag)
            Dim Result = Await Model.GetTRMbyCurrencyIdAsync(ToCurrencyId, _currencyId)
            If Result Is Nothing OrElse Not Result?.StateResult Then
                Me.Mensaje(EeventViewerImages.Advertencia) = Result?.Message
                Return False
            End If
            Me.TRM = Result.ObjectEmbbeded
            Me.Mensaje(EeventViewerImages.Informacion) = Result?.Message
            Return True
        End Using
    End Function

    ''' <summary>
    ''' metodo para ocultar ciertos campos que para la version internacional o diferente a Colombia
    ''' </summary>
    Private Sub HideFieldsByCurrentCulture()
        If indigo Is Nothing Then
            indigo = SessionValues.Instance
        End If

        If indigo.LanguageCulture <> "es-CO" Then
            INDLcBasicBilling.BeginUpdate()
            INDLciWithholdingTax.HideControl()
            INDLciWithholdingIVA.HideControl()
            INDLciWithholdingICA.HideControl()
            INDLcBasicBilling.EndUpdate()
        End If
    End Sub

    ''' <summary>
    ''' funcion que obtiene el cliente y su parametrizacion
    ''' </summary>
    ''' <param name="flagIsLoad"></param>
    ''' <returns></returns>
    Private Async Function GetCustomer(flagIsLoad As Boolean) As Task
        Try
            If Not flagIsLoad Then
                Return
            End If

            AsyncLoader(True)
            Me.AddressXpo = Nothing
            EconomicActivityId = Nothing
            INDSleEconomicActivity.Properties.DataSource = Nothing
            INDSleEconomicActivity.Properties.NullText = String.Empty
            Me._currentCustomer = Nothing
            INDSleAddress.EditValue = Nothing
            INDSleAddress.Properties.NullText = String.Empty
            Me.MainAccountId = Nothing
            Dim tpcustomer As ViewThirdPartyCustomerXpo = Nothing

            If Not String.IsNullOrWhiteSpace(ThirdPartyCustomerId) Then
                Dim customerThirdParty = $"{ThirdPartyCustomerId}"
                tpcustomer = Await Task.Run(Function() As ViewThirdPartyCustomerXpo
                                                Return XpoServiceEx.Instance(indigo.TransactionalContainer).CommonService.GetXPOObject(Of ViewThirdPartyCustomerXpo)($"Id='{customerThirdParty}'")
                                            End Function)
            End If

            If tpcustomer IsNot Nothing Then
                If tpcustomer.CustomerId > 0 Then
                    Me.MainAccountId = tpcustomer.MainAccountReceivableId
                    Me._currentCustomer = Await Me._presenter.GetThirdPartyByCustomerId(tpcustomer.ThirdPartyId)
                Else
                    MainAccountId = _parameterBilling.ClientMainAccountId
                End If

                _ContributionType = tpcustomer.ContributionType

                If TransactionEconomicActivity Then
                    Await LoadDatasourceEconomicActivities(tpcustomer.ThirdPartyId)
                End If

                If MainAccountId Is Nothing Then
                    INDSleCustomer.EditValue = Nothing
                    Mensaje(EeventViewerImages.Advertencia) = "El cliente no tiene cuenta contable parametrizada"
                    Return
                End If

                If Not Me._isLoad Then
                    Me._presenter.InitializeAddressXPO(tpcustomer.PersonId)
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            AsyncLoader(False)
        End Try
    End Function

    ''' <summary>
    ''' Carga el datasource de las actividades economicas del tercero relacionado al cliente
    ''' </summary>
    ''' <param name="thirdPartyId"></param>
    Private Async Function LoadDatasourceEconomicActivities(thirdPartyId As Integer) As Task
        Dim thirdParty = Await _presenter.GetThirdPartyByCustomerId(thirdPartyId)

        If thirdParty Is Nothing Then
            ShowMessage(EeventViewerImages.Advertencia) = "No se encontró el tercero relacionado al cliente"
            Return
        End If

        IsElectronicBillerThirdParty = thirdParty.ElectronicBiller

        'variable central de economicActivities
        Dim economicActivities = thirdParty.CommonThirdPartyEconomicActivitiesXpo?.ToList()

        If thirdParty.ElectronicBiller AndAlso (economicActivities Is Nothing OrElse Not economicActivities.Any()) Then
            ShowMessage(EeventViewerImages.Advertencia) = "Por favor asocie una Actividad Económica al Cliente"
            Return
        End If

        ' Asignar al datasource
        INDSleEconomicActivity.Properties.DataSource = economicActivities

        If Not _isLoad AndAlso economicActivities IsNot Nothing AndAlso economicActivities.Any() Then
            Dim economicActivityIdDefault = economicActivities.
            FirstOrDefault(Function(x) x.Defect)?.
            EconomicActivityId?.
            Id

            EconomicActivityId = economicActivityIdDefault
        End If
    End Function

    ''' <summary>
    ''' this method is to validate if exists portfolioAdvance to current thirdParty
    ''' </summary>
    ''' <param name="thirdParty"></param>
    ''' <returns></returns>
    Private Async Function GetIfExistsPortfolioAdvances(thirdParty As Integer) As Task(Of String)
        Dim message As String = String.Empty
        Using model As New MBasicBilling(MyTag)

            Dim result = Await model.GetPortfolioAdvanceByThirdParty(thirdParty)

            If result Is Nothing OrElse result.Count = 0 Then
                Return message
            End If
			message = String.Join(", ", result _
						.ToEntityList(Of Portfolio_PortfolioAdvance) _
						.Where(Function(x) x.CashReceiptId IsNot Nothing AndAlso x.CashReceiptId.Status = 2) _
						.Select(Function(x) x.Code) _
						.ToList())
			Return message
        End Using
    End Function

    ''' <summary>
    ''' event to make a related entity to make a crossing
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub RunLiquidateBasicBilling(sender As Object, e As RunCrossingProcessEventArgs)
        Me._basicBilling.PortfolioAdvanceInvoicePayment = e?.ListPortfolioAdvanceInvoicePaymentAs
    End Sub
#End Region

End Class

Public Class AddBasicBillingGiftsEventArgs
    Inherits EventArgs

    Property BasicBillingGifts As BasicBillingGifts

    Property EditMode As Boolean

End Class

Public Class AddDocumentsEventArgs
    Inherits EventArgs

    Property ListBasicbillingDetail As List(Of BasicBillingDetail)

    Property ListGiftsDetail As List(Of BasicBillingGifts)

    Property ItemBasicBilling As BasicBilling

End Class