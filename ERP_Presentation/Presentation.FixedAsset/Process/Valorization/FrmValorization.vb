'***********************************************************************
' Assembly         : Presentacion.FixedAsset
' Author           : Carlos Mario Arias Rubiano
' Created          : 31/03/2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Text
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports Presentation.Accounting.MVP
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.FixedAsset.MVP
Imports Presentation.Inventory
Imports Presentation.Maintenance.MVP
Imports Presentation.Payments.MVP

#End Region

Public Class FrmValorization
    Implements IFixedAssetValorization, ICustomizableForm

#Region "Builder"

    Public Sub New()
        InitializeComponent()
        ctrTmp = New CtrTotalInvoiceEntranceVoucher()
        ctrTmp.SetInfoFunction(AddressOf getValuesRetention)
        ctrTmp.PrintInfo()
        ctrTmp.PopupContainerControlTotalValue = PopUpSummarySettlement
        ctrTmp.Dock = System.Windows.Forms.DockStyle.Fill
    End Sub

    ''' <summary>
    ''' Devuelve el listado para mostrar el Total del contrato y sus derivados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getValuesRetention() As Tuple(Of String, String, String, String)
        Return New Tuple(Of String, String, String, String)(NetoValue, DiscountValue, IvaValue, TotalValue)
    End Function

#End Region

#Region "Properties"

    ''' <summary>
    ''' Tag del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IFixedAssetValorization.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Layout del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IFixedAssetValorization.MyLayoutControl
        Get
            Return LayoutControls
        End Get
    End Property

    Public Property Sequense As Domain.Entities.FixedAssetSequence Implements IFixedAssetValorization.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As Domain.Entities.FixedAssetSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.FixedAssetSequenceDetail In Me._sequence.FixedAssetSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor del parametro de inventario para la unidad operativa seleccionada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SettingFixedAsset As SettingFixedAsset Implements IFixedAssetValorization.SettingFixedAsset

    ''' <summary>
    ''' Obtiene o establece el código
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IFixedAssetValorization.Code
        Get
            If (INDBteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDBteCode.Text
            End If
        End Get
        Set(value As String)
            INDBteCode.Text = value
        End Set
    End Property

    Public Property DocumentDate As Date Implements IFixedAssetValorization.DocumentDate
        Get
            Return INDdteDocumentDate.EditValue
        End Get
        Set(value As Date)
            INDdteDocumentDate.EditValue = value
        End Set
    End Property

    Public Property GenerateAccountPayable As Boolean Implements IFixedAssetValorization.GenerateAccountPayable
        Get
            Return INDsleAccountPayable.EditValue
        End Get
        Set(value As Boolean)
            INDsleAccountPayable.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ThirdPartyId As Integer Implements IFixedAssetValorization.ThirdPartyId
        Get
            Return INDsleThirdParty.EditValue
        End Get
        Set(value As Integer)
            INDsleThirdParty.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ThirdPartyXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IFixedAssetValorization.ThirdPartyXpo
        Get
            Return INDsleThirdParty.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleThirdParty.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece los comentarios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Detail As String Implements IFixedAssetValorization.Detail
        Get
            Return INDmemoDetail.EditValue
        End Get
        Set(value As String)
            INDmemoDetail.EditValue = value
        End Set
    End Property

    Public Property CreditMainAccountId As Integer Implements IFixedAssetValorization.CreditMainAccountId
        Get
            Return INDSlCreditAccount.EditValue
        End Get
        Set(value As Integer)
            INDSlCreditAccount.EditValue = value
        End Set
    End Property

    Public Property MainAccountXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IFixedAssetValorization.MainAccountXpo
        Get
            Return INDSlCreditAccount.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSlCreditAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del centro de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CostCenterId As Integer? Implements IFixedAssetValorization.CostCenterId
        Get
            Return INDsleCostCenter.EditValue
        End Get
        Set(value As Integer?)
            INDsleCostCenter.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Estabablece el datasource del centro de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CostCenterXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IFixedAssetValorization.CostCenterXpo
        Get
            Return INDsleCostCenter.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleCostCenter.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Estabablece el datasource del centro de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListCurrencyXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IFixedAssetValorization.ListCurrencyXpo
        Get
            Return INDsleCurrency.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleCurrency.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id del proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SupplierId As Integer? Implements IFixedAssetValorization.SupplierId

    Public Property SupplierDistributionLineId As Integer? Implements IFixedAssetValorization.SupplierDistributionLineId
        Get
            Return INDSlDistributionLines.EditValue
        End Get
        Set(value As Integer?)
            INDSlDistributionLines.EditValue = value
        End Set
    End Property

    Public Property SupplierDistributionLineXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IFixedAssetValorization.SupplierDistributionLineXpo
        Get
            Return INDSlDistributionLines.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSlDistributionLines.Properties.DataSource = value
        End Set
    End Property

    Public Property SupplierTypeId As Integer? Implements IFixedAssetValorization.SupplierTypeId
        Get
            Return INDSlSupplierType.EditValue
        End Get
        Set(value As Integer?)
            INDSlSupplierType.EditValue = value
        End Set
    End Property

    Public Property SupplierTypeXpo As List(Of SupplierType) Implements IFixedAssetValorization.SupplierTypeXpo
        Get
            Return INDSlSupplierType.Properties.DataSource
        End Get
        Set(value As List(Of SupplierType))
            INDSlSupplierType.Properties.DataSource = value
        End Set
    End Property

    Public Property DayPeriod As Integer Implements IFixedAssetValorization.DayPeriod
        Get
            Return INDSpPeriodDays.EditValue
        End Get
        Set(value As Integer)
            INDSpPeriodDays.EditValue = value
        End Set
    End Property

    Public Property InvoiceNumber As String Implements IFixedAssetValorization.InvoiceNumber
        Get
            Return INDTxtInvoiceNumber.EditValue
        End Get
        Set(value As String)
            INDTxtInvoiceNumber.EditValue = value
        End Set
    End Property

    Public Property InvoiceDate As Date? Implements IFixedAssetValorization.InvoiceDate
        Get
            Return INDDtInvoiceDate.EditValue
        End Get
        Set(value As Date?)
            INDDtInvoiceDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad de Moneda Oficial
    ''' </summary>
    ''' <returns></returns>
    Private Property CurrencyId(Optional _currencyAbbreviation As String = Nothing) As Integer?
        Get
            Return INDsleCurrency.EditValue
        End Get
        Set(value As Integer?)
            INDsleCurrency.EditValue = value
            INDsleCurrency.Properties.NullText = _currencyAbbreviation
            SetCurrencyUI(_currencyAbbreviation)
        End Set
    End Property

    ''' <summary>
    ''' propiedad para tomar la abreviacion de la moneda y guardarla temp para cuando se necesite editar
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property CurrencyAbbreviation As String
        Get
            Return INDsleCurrency.Text
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
    ''' Tercero de la empresa actualmente seleccionada
    ''' </summary>
    Private _currentCompany As ThirdParty

    ''' <summary>
    ''' Linea de distribucion seleccionada del proveedor
    ''' </summary>
    Private _suppliersDistibutionLine As Infrastructure.Data.Xpo.CommonRepository.CommonSuppliersDistibutionLineXpo

    ''' <summary>
    ''' entidad de proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Private _supplier As New Supplier

    ''' <summary>
    ''' Registro  IVA
    ''' </summary>
    ''' <returns></returns>
    Public Property TaxRegistration As Byte Implements IFixedAssetValorization.TaxRegistration
        Get
            Return INDSleTaxRegistration.EditValue
        End Get
        Set(value As Byte)
            INDSleTaxRegistration.EditValue = value
        End Set
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "FixedAssets"

    ''' <summary>
    ''' Representa al presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PFixedAssetValorization

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Integer

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.FixedAssetSequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Tupla para el tipo de regla
    ''' </summary>
    Dim ListDocumentType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Lista los valores de la retencion
    ''' ListTuple(0) = el valor del iva,
    ''' ListTuple(1) = el valor del ica,
    ''' ListTuple(2) = el valor del retefuente
    ''' ListTuple(3) = el listado con los valores de rtfValue y rtfPercentage de cada item
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListTuple As List(Of Tuple(Of Decimal, List(Of FixedAssetTransactionDetail)))

    ''' <summary>
    ''' Control para establecer informacion del ingreso
    ''' </summary>
    Private ctrTmp As CtrTotalInvoiceEntranceVoucher

    ''' <summary>
    ''' Represa el Objeto Valorización/Desvalorización
    ''' </summary>
    ''' <remarks></remarks>
    Dim FixedAssetTransaction As FixedAssetTransaction

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordFixedAsset

    ''' <summary>
    ''' Variable para la entidad que se va a editar en la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Dim FixedAssetTransactionDetail As FixedAssetTransactionDetail

    ''' <summary>
    ''' indice del registro que se esta editando para luego insertarlo en la misma posicion que estaba
    ''' </summary>
    ''' <remarks></remarks>
    Dim IndexEditRecord As Integer

    Dim ListFixedAssetTransactionDetail As List(Of FixedAssetTransactionDetail)

    ''' <summary>
    ''' Listado de libros oficiales
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteFixedAssetTransactionDetail As List(Of FixedAssetTransactionDetail)

    Dim ListDeleteFixedAssetTransactionDetailBook As List(Of FixedAssetTransactionDetailBook)

    ''' <summary>
    ''' Sumatoria del subtotal de los articulos
    ''' </summary>
    ''' <remarks></remarks>
    Dim SubTotal As Decimal

    ''' <summary>
    ''' Sumatoria de los descuentos de los articulos
    ''' </summary>
    ''' <remarks></remarks>
    Dim DiscountValue As Decimal

    ''' <summary>
    ''' Sumatoria del valor neto de los articulos
    ''' </summary>
    ''' <remarks></remarks>
    Dim NetoValue As Decimal

    ''' <summary>
    ''' Sumatoria del valor del iva de los articulos
    ''' </summary>
    ''' <remarks></remarks>
    Dim IvaValue As Decimal

    ''' <summary>
    ''' Total a pagar
    ''' </summary>
    ''' <remarks></remarks>
    Dim TotalValue As Decimal

    ''' <summary>
    ''' Tasa de cambio
    ''' </summary>
    Private _tRM As TRM
    ''' <summary>
    ''' tasa de cambio
    ''' </summary>
    Private _roundingType As Decimal = 1
    ''' <summary>
    ''' Permite saber si se puede cambiar el valor del search
    ''' </summary>
    ''' <remarks></remarks>
    Private FlagLoad As Boolean = True
    ''' <summary>
    ''' Contiene los valores de Organización y Definición del Tenant
    ''' </summary>
    Dim CompanySettings As CompanySettings
    ''' <summary>
    ''' Variable que contiene la lista de registro de tipo de IVA
    ''' </summary>
    Dim ListTaxRegistration As New List(Of Tuple(Of Byte, String))


#End Region

#Region "ICrud"

    Public Sub Buscar() Implements ICrudBase.Buscar

    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    Public Async Sub Guardar() Implements ICrudBase.Guardar
        Try
            If ValidateControls() Then
                If FixedAssetTransaction.Status <> 3 Then
                    If Not ListFixedAssetTransactionDetail?.Any() Then
                        Mensaje(EeventViewerImages.Advertencia) = "Debe ingresar al menos un ítem."
                        Exit Sub
                    End If

                    AssigningValues()
                End If

                Using model As New MFixedAssetValorization(MyTag)
                    AsyncLoader(True)
                    Dim Result = Await model.SaveTransaction(FixedAssetTransaction, ListDeleteFixedAssetTransactionDetailBook, ListDeleteFixedAssetTransactionDetail, _idCurrentSequence)
                    If Result.StateResult = True Then
                        If FixedAssetTransaction.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                            'Se descarta la secuencia numerica usada
                            If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                                Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                            End If
                            If FixedAssetTransaction.Status = 1 Then
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                            ElseIf FixedAssetTransaction.Status = 2 Then
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SaveConfirm"), Result.ObjectEmbbeded.Code)
                            End If
                        ElseIf FixedAssetTransaction.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                            If FixedAssetTransaction.Status = 3 Then
                                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                            ElseIf FixedAssetTransaction.Status = 2 Then
                                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateConfirm")
                            ElseIf FixedAssetTransaction.Status = 1 Then
                                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                            End If
                        End If
                        Me.FixedAssetTransaction = Result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                        AsyncLoader(False)
                        Me.Deshacer()
                    Else
                        AsyncLoader(False)
                        INDBteCode.Enabled = False
                        If Result.MessageResult(0) = ErrorConcurrencia Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                        ElseIf Result.MessageResult(0) IsNot Nothing Then
                            Mensaje(EeventViewerImages.Advertencia) = Result.MessageResult(0).ToString()
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                        End If
                    End If
                End Using
            End If
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewValorization()
        End If
    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Fecha Documento", .FieldName = "DocumentDate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Número Factura", .FieldName = "InvoiceNumber", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Valor", .FieldName = "Value", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Moneda", .FieldName = "CurrencyAbbreviation", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)}}.ToList
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetTransaction
            .ValorSolicitado = "Code"
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "-1", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.White})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Metodo que inicializa el datasource del control de tipo de proveedor
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function InitializeSupplierType() As Task
        If SupplierId IsNot Nothing Then
            Using model As New MSupplierType(Tag)
                Dim x As ActionResult(Of List(Of SupplierType)) = Await model.GetSupplierTypeBySupplierId(SupplierId)
                Dim listSupplierType As List(Of SupplierType) = x.ObjectEmbbeded
                SupplierTypeXpo = listSupplierType
            End Using
        End If
    End Function

    ''' <summary>
    ''' Activa e inactiva los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IFixedAssetValorization.ActionsOnControls
        Set(value As Boolean)
            INDlyValorization.BeginUpdate()
            INDBteCode.Enabled = Not value
            INDdteDocumentDate.Enabled = value
            INDsleAccountPayable.Enabled = value
            INDsleThirdParty.Enabled = value
            INDmemoDetail.Enabled = value
            INDSlCreditAccount.Enabled = value
            INDsleCostCenter.Enabled = value
            INDSlDistributionLines.Enabled = value
            INDSlSupplierType.Enabled = value
            INDSpPeriodDays.Enabled = value
            INDTxtInvoiceNumber.Enabled = value
            INDDtInvoiceDate.Enabled = value
            INDBtnAdd.Enabled = True
            If value Then
                INDdteDocumentDate.Focus()
            Else
                INDBteCode.Focus()
            End If
            INDlyValorization.EndUpdate()
        End Set
    End Property

    ''' <summary>
    ''' Limpia los controles del popup de retenciones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlValues(FlagClean As Boolean)
        INDPopTxtValue.EditValue = 0
        INDPopTxtValueTax.EditValue = 0
        If FlagClean Then
            INDPopTxtFreightValue.EditValue = 0
            INDPopSpnFreightIVA.EditValue = 0
            INDPopTxtTotalCxp.EditValue = 0
        Else
            INDPopTxtTotalCxp.EditValue = INDPopTxtFreightValue.EditValue + INDPopSpnFreightIVA.EditValue
        End If
        INDPopTxtDiscountValue.EditValue = 0
        INDPopTxtWithholdingTax.EditValue = 0
        INDPopTxtWithholdingICA.EditValue = 0
        INDPopTxtRetentionSource.EditValue = 0
        INDPopTxtRetentionOther.EditValue = 0
        INDPopTxtDeductionOther.EditValue = 0
        INDPopTxtDistricTaxes.EditValue = 0
        SubTotal = 0
        DiscountValue = 0
        NetoValue = 0
        IvaValue = 0
        TotalValue = 0
        ctrTmp.PrintInfo()
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub CleanControls()
        INDlyValorization.BeginUpdate()
        ReadOnlyControls(False)
        ActionsOnControls = False
        Code = String.Empty
        DocumentDate = Date.Now()
        GenerateAccountPayable = False
        ThirdPartyId = Nothing
        INDsleThirdParty.Properties.NullText = String.Empty
        Detail = Nothing
        CreditMainAccountId = 0
        INDSlCreditAccount.Properties.NullText = String.Empty
        CostCenterId = Nothing
        INDsleCostCenter.Properties.NullText = String.Empty
        SupplierId = Nothing
        SupplierDistributionLineId = Nothing
        INDSlDistributionLines.Properties.NullText = String.Empty
        SupplierTypeId = Nothing
        INDSlSupplierType.Properties.NullText = String.Empty
        DayPeriod = 0
        InvoiceDate = Nothing
        InvoiceNumber = String.Empty
        ListFixedAssetTransactionDetail = Nothing
        FixedAssetTransaction = Nothing
        FixedAssetTransactionDetail = Nothing
        INDGcTransactionDetail.DataSource = Nothing
        INDBtnAdd.Enabled = False
        FlagLoad = False
        INDsleAccountPayable_EditValueChanged(Nothing, Nothing)

        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        CleanControlValues(True)
        INDlyValorization.EndUpdate()
        TaxRegistration = 4
        Me.BarraBotones.StatusRecordVisible = False
        Await DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
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

    ''' <summary>
    ''' Calcula el total del valor de la cxp
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CalculateTotalValue()
        INDPopTxtTotalCxp.EditValue = INDPopTxtValue.EditValue + INDPopTxtValueTax.EditValue + INDPopTxtFreightValue.EditValue + INDPopSpnFreightIVA.EditValue - INDPopTxtDiscountValue.EditValue - INDPopTxtWithholdingTax.EditValue - INDPopTxtWithholdingICA.EditValue - INDPopTxtRetentionSource.EditValue
    End Sub

    ''' <summary>
    ''' carga los controles con la informacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function LoadControls() As Task
        If Me._currentCompany Is Nothing Then 'Si la empresa actual no existe como tercero, se debe crear
            Me.Mensaje(EeventViewerImages.Advertencia) = "No se ha podido cargar el tercero de la empresa (" & Me.indigo.IndigoCompanyNit & ") actualmente seleccionada. Verifique que haya sido creada e intente de nuevo."
            Exit Function
        End If

        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If

            Try
                Using Model As New MFixedAssetValorization(CStr(Me.Tag))
                    AsyncLoader(True)
                    Dim tmpFixedAssetTransaction = (Await Model.GetValorizationDevaluationByCode(INDBteCode.Text.Trim)).ObjectEmbbeded

                    INDLcTransaction.BeginUpdate()
                    If tmpFixedAssetTransaction IsNot Nothing AndAlso tmpFixedAssetTransaction.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        'Cargar unidad operativa
                        BarraBotones_ChangueOperatingUnit(New OperatingUnit With {.Id = tmpFixedAssetTransaction.OperatingUnitId})
                        FixedAssetTransaction = tmpFixedAssetTransaction
                        BarraBotones_ChangueOperatingUnit(New OperatingUnit With {.Id = Me.BarraBotones.OperatingUnit.Id})

                        Using ModelRecord As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(FixedAssetTransaction.Id))

                            FlagLoad = True
                            With FixedAssetTransaction
                                Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideUser"), .AnnulmentUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideDate"), .AnnulmentDate)

                                Code = .Code
                                DocumentDate = .DocumentDate
                                GenerateAccountPayable = .GenerateAccountPayable
                                ThirdPartyId = .ThirdPartyId
                                INDsleThirdParty.Properties.NullText = .NameThirdParty
                                Detail = .Observation

                                CreditMainAccountId = .CreditMainAccountId
                                INDSlCreditAccount.Properties.NullText = .NameCreditMainAccount
                                CostCenterId = .CostCenterId
                                INDsleCostCenter.Properties.NullText = .NameCostCenter
                                CurrencyId(.Currency.Abbreviation) = .CurrencyId

                                SupplierId = .SupplierId
                                SupplierDistributionLineId = .SupplierDistributionLineId
                                INDSlDistributionLines.Properties.NullText = .NameSupplier
                                If SupplierId IsNot Nothing Then
                                    Await InitializeSupplierType()
                                End If
                                INDSlSupplierType.EditValue = .SupplierTypeId
                                DayPeriod = .DayPeriod
                                InvoiceNumber = .InvoiceNumber
                                InvoiceDate = .InvoiceDate

                                BarraBotones.StatusRecord = .Status.ToString()

                                ListFixedAssetTransactionDetail = .FixedAssetTransactionDetail.ToList()
                                INDGcTransactionDetail.DataSource = Nothing
                                INDGcTransactionDetail.DataSource = ListFixedAssetTransactionDetail

                                INDPopTxtValue.EditValue = .Value
                                INDPopTxtDiscountValue.EditValue = .ValueDiscount
                                INDPopTxtValueTax.EditValue = .ValueTax
                                INDPopTxtWithholdingTax.EditValue = .WithholdingTax
                                INDPopTxtWithholdingICA.EditValue = .WithholdingICA
                                INDPopTxtRetentionSource.EditValue = .RetentionSource
                                INDPopTxtRetentionOther.EditValue = .RetentionOther
                                INDPopTxtDeductionOther.EditValue = .DeductionOther
                                TaxRegistration = .TaxRegistration
                                SubTotal = ListFixedAssetTransactionDetail.Sum(Function(item) item.Value)
                                DiscountValue = ListFixedAssetTransactionDetail.Sum(Function(item) item.DiscountValue)
                                NetoValue = SubTotal - DiscountValue
                                IvaValue = ListFixedAssetTransactionDetail.Sum(Function(item) item.IvaValue)
                                TotalValue = ListFixedAssetTransactionDetail.Sum(Function(item) item.TotalValue)
                                CalculateTotalValue()
                                ctrTmp.PrintInfo()

                            End With

                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.FixedAssetTransaction.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordFixedAsset With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = FixedAssetTransaction.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            Me.BarraBotones.SetDocuments(FixedAssetTransaction.Id, Me.Tag.ToString(), Nothing, GetType(FixedAssetTransaction).Name)
                            Me.GetDocumentIndexed(MyTag & "_" & FixedAssetTransaction.Code)
                            AsyncLoader(False)
                            ActionsOnControls = True

                            Select Case FixedAssetTransaction.Status
                                Case 1
                                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
                                Case Else
                                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                    ReadOnlyControls(True)
                            End Select

                            FlagLoad = False
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, FixedAssetTransaction.Id, 0, FixedAssetTransaction.Id)
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewValorization()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDBteCode.Focus()
                        End If
                    End If
                    INDLcTransaction.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBteCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    Private Async Function NewValorization() As Task
        If Me._currentCompany Is Nothing Then 'Si la empresa actual no existe como tercero, se debe crear
            Me.Mensaje(EeventViewerImages.Advertencia) = "No se ha podido cargar el tercero de la empresa (" & Me.indigo.IndigoCompanyNit & ") actualmente seleccionada. Verifique que haya sido creada e intente de nuevo."
            Exit Function
        End If

        If Me._idOperativeUnit = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una unidad operativa"
            Exit Function
        End If

        Me.FixedAssetTransaction = New FixedAssetTransaction()
        BarraBotones.StatusRecordVisible = True
        BarraBotones.StatusRecord = "1"
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.FixedAssetSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.FixedAssetSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.FixedAssetSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
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
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
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
    End Function

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Confirmar()
        Try
            If ValidateControls() Then
                If FixedAssetTransaction.Status <> 3 Then
                    If ListFixedAssetTransactionDetail Is Nothing OrElse ListFixedAssetTransactionDetail.Count = 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = "Debe ingresar al menos un ítem al Detalle."
                        Exit Sub
                    End If

                    AssigningValues()
                End If

                Using model As New MFixedAssetValorization(MyTag)
                    AsyncLoader(True)
                    Dim Result = Await model.ConfirmValorizationDevaluation(FixedAssetTransaction, ListDeleteFixedAssetTransactionDetailBook, ListDeleteFixedAssetTransactionDetail, _idCurrentSequence)
                    If Result.StatusCode = eStatusResult.SUCCESS Then
                        Mensaje(EeventViewerImages.Informacion) = Result.Message
                        AsyncLoader(False)
                        Me.FixedAssetTransaction = Result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)

                        AsyncLoader(False)
                        Me.Deshacer()
                    Else
                        AsyncLoader(False)
                        INDBteCode.Enabled = False
                        If Result.StatusCode = eStatusResult.WARNING Then
                            Mensaje(EeventViewerImages.Advertencia) = Result.Message
                        ElseIf Result.StatusCode = eStatusResult.EXCEPTION Then
                            Mensaje(EeventViewerImages.MensajeError) = Result.Message
                        End If
                    End If
                End Using
            End If
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With FixedAssetTransaction
            .CustomProperties = LayoutControls.GetCustomFieldsValue()

            .OperatingUnitId = _idOperativeUnit
            .Code = Code
            .DocumentDate = DocumentDate
            .GenerateAccountPayable = GenerateAccountPayable
            .ThirdPartyId = ThirdPartyId
            .Observation = Detail
            .CurrencyId = CurrencyId

            .CreditMainAccountId = CreditMainAccountId
            .CostCenterId = CostCenterId

            'Si genera Cuentas por Pagar
            If INDlygAssets.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .SupplierId = SupplierId
                .SupplierDistributionLineId = SupplierDistributionLineId
                .SupplierTypeId = SupplierTypeId
                .DayPeriod = DayPeriod
                .InvoiceNumber = InvoiceNumber
                .InvoiceDate = InvoiceDate
            Else
                .SupplierId = Nothing
                .SupplierDistributionLineId = Nothing
                .SupplierTypeId = Nothing
                .DayPeriod = 0
                .InvoiceNumber = Nothing
                .InvoiceDate = Nothing
            End If

            .Value = INDPopTxtValue.EditValue
            .ValueDiscount = INDPopTxtDiscountValue.EditValue
            .ValueTax = INDPopTxtValueTax.EditValue
            .WithholdingTax = INDPopTxtWithholdingTax.EditValue
            .WithholdingICA = INDPopTxtWithholdingICA.EditValue
            .RetentionSource = INDPopTxtRetentionSource.EditValue
            .RetentionOther = 0
            .DeductionOther = 0
            .TotalValue = TotalValue
            .TaxRegistration = TaxRegistration
            If ListFixedAssetTransactionDetail?.Any() Then
                .FixedAssetTransactionDetail.Clear()
                ListFixedAssetTransactionDetail.ForEach(Sub(item)
                                                            .FixedAssetTransactionDetail.Add(item)
                                                        End Sub)
            End If

            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedRecord() As Task
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Function

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.FixedAssetTransaction.Code, Me.FixedAssetTransaction.DocumentDate),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.FixedAssetTransaction.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.FixedAssetTransaction.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.FixedAssetTransaction.Code, Me.FixedAssetTransaction.DocumentDate)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.FixedAssetTransaction.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Metodo que abre el form de saldo inicial articulo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OpenFormValorizationDetail(EditMode As Boolean)
        Dim errors As New StringBuilder

        If GenerateAccountPayable Then
            If SupplierDistributionLineId Is Nothing Then
                errors.AppendLine("Debe seleccionar un " + LayoutControlItem1.Text)
            End If
        Else
            If ThirdPartyId = 0 Then
                errors.Append("Debe seleccionar un " + INDlyItemThirdParty.Text)
            End If
        End If
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString
            Exit Sub
        End If

        Using formulario As New FrmValorizationDetail(_currency:=New Currency With {.Id = Me.CurrencyId, .Abbreviation = Me.CurrencyAbbreviation},
                                                              _tRMValue:=If(Me.TRM Is Nothing, 1, Me.TRM.Value),
                                                            _roundType:=_roundingType, _documentDate:=DocumentDate
                                                            )
            AddHandler formulario.AddFixedAssetEntryItemPartEventArgs, AddressOf ReturnAddEventArgs
            formulario.FlagAccountPayable = GenerateAccountPayable
            formulario.EditMode = EditMode
            formulario.SettingFixedAsset = SettingFixedAsset
            formulario.FixedAssetTransactionDetail = FixedAssetTransactionDetail
            formulario.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.8
            formulario.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.8
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    Private Sub ReturnAddEventArgs(sender As Object, e As AddValorizationDetailEventArgs)
        If Not e.EditMode Then 'Se esta ingresando un articulo
            If ListFixedAssetTransactionDetail Is Nothing Then
                ListFixedAssetTransactionDetail = New List(Of FixedAssetTransactionDetail)
            End If

            ListFixedAssetTransactionDetail.Add(e.FixedAssetTransactionDetail)
            Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente."
        Else 'Se esta modificando un articulo
            ListFixedAssetTransactionDetail.Remove(FixedAssetTransactionDetail)
            ListFixedAssetTransactionDetail.Insert(IndexEditRecord, e.FixedAssetTransactionDetail)
            Mensaje(EeventViewerImages.Informacion) = "Detalle modificado correctamente."
        End If

        Me.CalculateRetentions()
        INDGcTransactionDetail.DataSource = Nothing
        INDGcTransactionDetail.DataSource = ListFixedAssetTransactionDetail
    End Sub

    ''' <summary>
    ''' Elimina el detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RemoveDetail()
        FixedAssetTransactionDetail = CType(INDviewItem.GetFocusedRow, FixedAssetTransactionDetail)
        ListFixedAssetTransactionDetail.Remove(FixedAssetTransactionDetail)

        If FixedAssetTransactionDetail.Id > 0 Then
            If ListDeleteFixedAssetTransactionDetail Is Nothing Then
                ListDeleteFixedAssetTransactionDetail = New List(Of FixedAssetTransactionDetail)
            End If
            ListDeleteFixedAssetTransactionDetail.Add(FixedAssetTransactionDetail)
            If FixedAssetTransactionDetail.FixedAssetTransactionDetailBook IsNot Nothing AndAlso FixedAssetTransactionDetail.FixedAssetTransactionDetailBook.Count > 0 Then

                For Each itemDetail In (From l In FixedAssetTransactionDetail.FixedAssetTransactionDetailBook Where l.Id > 0 Select l).ToList
                    If ListDeleteFixedAssetTransactionDetailBook Is Nothing Then
                        ListDeleteFixedAssetTransactionDetailBook = New List(Of FixedAssetTransactionDetailBook)
                    End If
                    If ListDeleteFixedAssetTransactionDetailBook.Contains(itemDetail) Then
                        Continue For
                    End If
                    ListDeleteFixedAssetTransactionDetailBook.Add(itemDetail)
                Next
            End If
        End If

        If ListFixedAssetTransactionDetail Is Nothing OrElse ListFixedAssetTransactionDetail.Count = 0 Then 'Si no hay nada en el listado puede volver a cambiar Ubicacion/Responsable y proveedor
            INDsleThirdParty.Properties.ReadOnly = False
            INDSlDistributionLines.Properties.ReadOnly = False
            CleanControlValues(False)
        End If

        Me.CalculateRetentions()
        INDGcTransactionDetail.DataSource = Nothing
        INDGcTransactionDetail.DataSource = ListFixedAssetTransactionDetail
    End Sub

    ''' <summary>
    ''' Edita el detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditDetail()
        FixedAssetTransactionDetail = DirectCast(INDviewItem.GetFocusedRow(), FixedAssetTransactionDetail)
        IndexEditRecord = ListFixedAssetTransactionDetail.IndexOf(FixedAssetTransactionDetail)
        OpenFormValorizationDetail(True)
    End Sub

    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
        Set(ByVal value As String)

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
    ''' Carga el tercero de la empresa actualmente seleccionada
    ''' </summary>
    ''' <returns>Valor que indica si la empresa se cargo</returns>
    Private Async Function LoadCurrentCompany() As Task
        Using model As New MFixedAssetEntry(MyTag)
            Me._currentCompany = Await model.GetThirdPartyAsync(Me.indigo.IndigoCompanyNit)
        End Using
    End Function

    ''' <summary>
    ''' Calcular las retenciones
    ''' </summary>
    Private Sub CalculateRetentions()
        SubTotal = 0
        DiscountValue = 0
        NetoValue = 0
        IvaValue = 0
        TotalValue = 0

        INDPopTxtValue.EditValue = SubTotal
        INDPopTxtDiscountValue.EditValue = DiscountValue
        INDPopTxtValueTax.EditValue = IvaValue
        INDPopTxtRetentionSource.EditValue = 0
        INDPopTxtWithholdingICA.EditValue = 0
        INDPopTxtWithholdingTax.EditValue = 0

        ' Si no tiene items agregados
        If ListFixedAssetTransactionDetail Is Nothing Then
            Exit Sub
        End If

        'IVA
        For Each item In ListFixedAssetTransactionDetail
            item.IvaValue = 0
            Dim totalValue = item.Value - item.DiscountValue
            If Not _supplier?.NotIva And item.IvaPercentage > 0 Then
                item.IvaValue = Utils.RoundValue((totalValue * item.IvaPercentage / 100), 8)
            End If
            item.TotalValue = totalValue + item.IvaValue
        Next

        SubTotal = ListFixedAssetTransactionDetail.Sum(Function(item) item.Value)
        DiscountValue = ListFixedAssetTransactionDetail.Sum(Function(item) item.DiscountValue)
        NetoValue = ListFixedAssetTransactionDetail.Sum(Function(item) item.Value - item.DiscountValue)
        IvaValue = Utils.RoundValue(ListFixedAssetTransactionDetail.Sum(Function(item) item.IvaValue), 6)
        TotalValue = ListFixedAssetTransactionDetail.Sum(Function(item) item.TotalValue)

        INDPopTxtValue.EditValue = SubTotal
        INDPopTxtDiscountValue.EditValue = DiscountValue
        INDPopTxtValueTax.EditValue = IvaValue

        ' Si genera una cuenta por pagar
        If GenerateAccountPayable Then
            If _supplier IsNot Nothing AndAlso _supplier.ThirdParty IsNot Nothing Then
                'Si el tercero del proveedor maneja retenciones
                If _supplier.ThirdParty.RetentionType = 2 Then
                    Dim errors As New StringBuilder()

                    'Dictionaries
                    Dim dictionaryAccountPayableConcepts As New Dictionary(Of Integer, Integer)()
                    Dim dictionaryRetentionConcepts As New Dictionary(Of Integer, GeneralLedgerRetentionConceptsReportXpo)()
                    'Items Individuals
                    Dim retentionConceptId As Integer = Nothing
                    Dim retentionConceptXpo As GeneralLedgerRetentionConceptsReportXpo = Nothing

                    'Retefuente
                    If Not _supplier.SelfWithholding Then
                        For Each item In ListFixedAssetTransactionDetail
                            Dim fixedAssedPhysicalAssetXpo As FixedAssetPhysicalAssetXpo = Presenter.GetFixedAssetPhysicalAssetByTransactionClass(item.TransactionClass, item.PhysicalAssetId, item.PhysicalAssetPartsId)

                            Dim accountPayableConceptId = If(_supplier.Declarant, fixedAssedPhysicalAssetXpo.ItemId.ItemCatalogId.DeclarantRetentionAccountPayableConceptId, fixedAssedPhysicalAssetXpo.ItemId.ItemCatalogId.NotDeclarantRetentionAccountPayableConceptId)

                            'Obtener el id del concepto de retención
                            If Not dictionaryAccountPayableConcepts.ContainsKey(accountPayableConceptId) Then
                                Dim accountPayableConceptXpo = Presenter.GetAccountPayableConceptById(accountPayableConceptId)
                                retentionConceptId = accountPayableConceptXpo.RetentionConceptId
                                dictionaryAccountPayableConcepts.Add(accountPayableConceptId, retentionConceptId)
                            Else
                                retentionConceptId = dictionaryAccountPayableConcepts(accountPayableConceptId)
                            End If

                            'Obtener el concepto de retención
                            If Not dictionaryRetentionConcepts.ContainsKey(retentionConceptId) Then
                                retentionConceptXpo = Presenter.GetRetentionConceptById(retentionConceptId)
                                dictionaryRetentionConcepts.Add(retentionConceptId, retentionConceptXpo)
                            Else
                                retentionConceptXpo = dictionaryRetentionConcepts(retentionConceptId)
                            End If

                            If retentionConceptXpo Is Nothing Then
                                errors.AppendLine(String.Format("El Artículo {0} no tiene parametrizado un concepto de retención", fixedAssedPhysicalAssetXpo.ItemId.Code))
                                Continue For
                            End If

                            item.RTFPercentage = 0
                            item.RTFValue = 0
                            If _supplier.PermanentRetention OrElse (NetoValue >= retentionConceptXpo.MinBase) Then
                                item.RTFPercentage = retentionConceptXpo.Rate
                                item.RTFValue = Utils.RoundValue(((item.Value - item.DiscountValue) * retentionConceptXpo.Rate / 100), retentionConceptXpo.TypeRounding)
                                INDPopTxtRetentionSource.EditValue = INDPopTxtRetentionSource.EditValue + item.RTFValue
                            End If
                        Next
                    End If

                    'ReteIca
                    If _supplier.ThirdParty.Ica AndAlso Not _supplier.SelfWithholdingICA Then
                        Dim icaPercentage As Decimal = _supplier.ThirdParty.IcaPercentage
                        Dim minBase As Decimal = If(_supplier.ThirdParty.IcaTop, _supplier.ThirdParty.IcaTopValue, 0)
                        Dim typeRounding As Byte = 1

                        If _suppliersDistibutionLine IsNot Nothing AndAlso _suppliersDistibutionLine.IdDistributionLine.CommonDistributionLinesICARetentionXpo IsNot Nothing Then
                            Dim distributionLineICAXpo = _suppliersDistibutionLine.IdDistributionLine.CommonDistributionLinesICARetentionXpo.Where(Function(d) d.OperatingUnitId = _idOperativeUnit).FirstOrDefault()
                            If distributionLineICAXpo IsNot Nothing Then
                                Dim retentionConceptICAXpo = distributionLineICAXpo.AccountPayableConceptId.RetentionConceptId
                                If retentionConceptICAXpo IsNot Nothing Then
                                    icaPercentage = retentionConceptICAXpo.Rate
                                    minBase = If(_supplier.ThirdParty.IcaTop, retentionConceptICAXpo.MinBase, minBase)
                                    typeRounding = retentionConceptICAXpo.TypeRounding
                                End If
                            End If
                        End If

                        If (NetoValue >= minBase) Then
                            INDPopTxtWithholdingICA.EditValue = Utils.RoundValue((NetoValue * icaPercentage / 100), typeRounding)
                        End If
                    End If

                    'ReteIva
                    If IvaValue > 0 Then
                        If _supplier.ThirdParty.ContributionType > 0 AndAlso _currentCompany.ContributionType > _supplier.ThirdParty.ContributionType Then
                            If SettingFixedAsset.IVARetention = 1 Then
                                'La retención se saca del tercero
                                If _supplier.ThirdParty.IVARetentionConceptId Is Nothing Then
                                    errors.AppendLine("El tercero no tiene parametrizado un concepto de retención de IVA")
                                Else
                                    retentionConceptId = _supplier.ThirdParty.IVARetentionConceptId
                                End If
                            Else
                                'La retención se saca del concepto de los parámetros
                                If Not dictionaryAccountPayableConcepts.ContainsKey(SettingFixedAsset.IVARetentionAccountPayableConceptId) Then
                                    Dim accountPayableConceptXpo = Presenter.GetAccountPayableConceptById(SettingFixedAsset.IVARetentionAccountPayableConceptId)
                                    retentionConceptId = accountPayableConceptXpo.RetentionConceptId
                                    dictionaryAccountPayableConcepts.Add(SettingFixedAsset.IVARetentionAccountPayableConceptId, retentionConceptId)
                                Else
                                    retentionConceptId = dictionaryAccountPayableConcepts(SettingFixedAsset.IVARetentionAccountPayableConceptId)
                                End If
                            End If

                            'Obtener el concepto de retención
                            If Not dictionaryRetentionConcepts.ContainsKey(retentionConceptId) Then
                                retentionConceptXpo = Presenter.GetRetentionConceptById(retentionConceptId)
                                dictionaryRetentionConcepts.Add(retentionConceptId, retentionConceptXpo)
                            Else
                                retentionConceptXpo = dictionaryRetentionConcepts(retentionConceptId)
                            End If

                            If retentionConceptXpo Is Nothing Then
                                errors.AppendLine("No se encuentra parametrizado un concepto de retención de IVA")
                            Else
                                If (IvaValue >= retentionConceptXpo.MinBase) Then
                                    INDPopTxtWithholdingTax.EditValue = Utils.RoundValue((IvaValue * retentionConceptXpo.Rate / 100), retentionConceptXpo.TypeRounding)
                                End If
                            End If
                        End If
                    End If

                    If errors.Length > 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
                    End If
                End If
            End If
        End If

        CalculateTotalValue()
        ctrTmp.PrintInfo()
    End Sub

    ''' <summary>
    ''' Carga los parametros del Tenant
    ''' </summary>
    Private Async Sub LoadCompanySettings()
        Using modelCompanySettings As New MCompanySettings(MyTag)
            CompanySettings = Await modelCompanySettings.GetCompanySettings()
        End Using
    End Sub

    ''' <summary>
    ''' Establece la visibilidad del control de TaxRegistration dependiendo del Registro IVA en CompanySettings, 
    ''' siempre y cuando el tipo de IVA sea mixto
    ''' </summary>
    Private Sub ToggleTaxRegistrationFieldVisibility()
        If CompanySettings IsNot Nothing Then
            If CompanySettings.TaxRegistration = 3 Then
                INDLciTaxRegistration.ShowLayout()
            Else
                TaxRegistration = CompanySettings.TaxRegistration
                INDLciTaxRegistration.HideLayout()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Inicializa la tupla  de registro IVA
    ''' </summary>
    Private Sub InitializeTuple()
        'Registro IVA
        ListTaxRegistration = New List(Of Tuple(Of Byte, String))
        ListTaxRegistration.Add(New Tuple(Of Byte, String)(1, "IVA al costo control fiscal"))
        ListTaxRegistration.Add(New Tuple(Of Byte, String)(2, "IVA descontable"))
        ListTaxRegistration.Add(New Tuple(Of Byte, String)(4, "IVA al costo"))
        INDSleTaxRegistration.Properties.DataSource = ListTaxRegistration.ToList
    End Sub

#End Region

#Region "Handlers"

#Region "Bar Button Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        FixedAssetTransaction.Status = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDBteCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        FixedAssetTransaction.Status = 1
        Guardar()
    End Sub

    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        FixedAssetTransaction.Status = 2
        Confirmar()
    End Sub

    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        FixedAssetTransaction.Status = 2
        Confirmar()
    End Sub

    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        FixedAssetTransaction.Status = 3
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Me.Nuevo()
    End Sub
    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir del abarra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.None, FixedAssetTransaction.Id, 0, FixedAssetTransaction.Id)
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Async Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If FixedAssetTransaction Is Nothing OrElse FixedAssetTransaction.Id = 0 Then
            If operatingUnit IsNot Nothing AndAlso _idOperativeUnit <> operatingUnit.Id Then
                _idOperativeUnit = operatingUnit.Id
                Await Presenter.InitializeFixedAssetSetting(_idOperativeUnit)
                If Not GenerateAccountPayable Then
                    INDSlCreditAccount.EditValue = SettingFixedAsset.ServiceMainAccountId
                End If

                If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.FixedAssetSequenceDetail IsNot Nothing Then
                    If Not Me._sequence.FixedAssetSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    End If
                End If
            End If
        Else
            If operatingUnit.Id <> _idOperativeUnit Then
                Mensaje(EeventViewerImages.Advertencia) = "La unidad operativa no tiene ningún efecto sobre el registro ya guardado"
            End If
        End If

    End Sub

    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBteCode.Enabled Then
            INDBteCode.Focus()
        End If
    End Sub
#End Region


#Region "MenuContext"

    ''' <summary>
    ''' Despliega los botones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.SimpleButton
        btn = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case btn.Tag.ToString
            Case "Edit"
                EditDetail()
            Case "Remove"
                RemoveDetail()
        End Select
    End Sub

    ''' <summary>
    ''' Accion de click derecho del mouse
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Edit"
                EditDetail()
            Case "Remove"
                RemoveDetail()
        End Select
    End Sub

#End Region

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        _idOperativeUnit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        ListDocumentType = Nothing
        ListTuple = Nothing
        ctrTmp = Nothing
        FixedAssetTransaction = Nothing
        record = Nothing
        FixedAssetTransactionDetail = Nothing
        IndexEditRecord = Nothing
        ListFixedAssetTransactionDetail = Nothing
        ListDeleteFixedAssetTransactionDetail = Nothing
        ListDeleteFixedAssetTransactionDetailBook = Nothing
        SubTotal = Nothing
        DiscountValue = Nothing
        NetoValue = Nothing
        IvaValue = Nothing
        TotalValue = Nothing
        _currentCompany = Nothing
        _suppliersDistibutionLine = Nothing
        _supplier = Nothing
    End Sub


    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmValorization_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        IndigoGridView1.SetListAcction(INDviewItem, {eAcciones.Remove, eAcciones.Edit}.ToList())

        Me.LayoutControls.SetIsCustomizable(Me.INDlyValorization, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue

        '****Inicializar variables*****'

        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PFixedAssetValorization(Me)

        Try
            AsyncLoader(True)
            Await Presenter.GetSequense()
            Await LoadCurrentCompany()
            Await Presenter.InitializeFixedAssetSetting(_idOperativeUnit)
        Catch ex As Exception
            Me.Mensaje(EeventViewerImages.Advertencia) = ex.Message
        Finally
            AsyncLoader(False)
        End Try

        CurrencyId(SettingFixedAsset?.Currency?.Abbreviation) = SettingFixedAsset?.CurrencyId
        LoadStatus()
        Deshacer()
        INDdteDocumentDate.EditValue = Date.Now
        InitializeTuple()
        LoadCompanySettings()
        ToggleTaxRegistrationFieldVisibility()
    End Sub



    ''' <summary>
    ''' Establece el formato moneda en los controles del formulario
    ''' </summary>
    ''' <param name="_currencyAbbreviation"></param>
    Private Sub SetCurrencyUI(_currencyAbbreviation As String)

        If String.IsNullOrEmpty(_currencyAbbreviation) Then
            Mensaje(EeventViewerImages.Advertencia) = "Está llegando vacia la abreviación de la moneda"
            Exit Sub
        End If

        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = _currencyAbbreviation.GetNumberFormat

        GridColumn13 = Window.Utils.FormatGrid(GridColumn13, _currencyAbbreviation)
        Me.ctrTmp.CodeISO4217 = _currencyAbbreviation
        ctrTmp.PrintInfo()
    End Sub

    ''' <summary>
    ''' funcion Valida y establece el evento cuando la moneda cambia 
    ''' </summary>
    ''' <param name="IsLoadControl"></param>
    ''' <returns></returns>
    Private Async Function ValidateCurrencyEditValue(IsLoadControl As Boolean, _currencyId As Integer?) As Task(Of ActionResult)
        If _currencyId Is Nothing OrElse _currencyId = 0 OrElse IsLoadControl OrElse {2, 3}.Contains(Me.BarraBotones.StatusRecord) Then
            Return New ActionResult With {.StateResult = True}
        End If

        If Me.CurrencyId IsNot Nothing Then
            Me.SetCurrencyUI(Me.CurrencyAbbreviation)
        End If

        If _currencyId = SettingFixedAsset?.CurrencyId Then
            Me.TRM = New TRM With {.CurrencyId = _currencyId, .OfficialCurrencyId = SettingFixedAsset?.CurrencyId, .Value = 1}
            Return New ActionResult With {.StateResult = True}
        End If

        Dim _stateResult = Await GetTRM(SettingFixedAsset?.CurrencyId, _currencyId)

        If _stateResult Then
            Return New ActionResult With {.StateResult = True}
        End If

        If Not FlagLoad Then
            Me.CurrencyId(SettingFixedAsset?.Currency?.ISO4217Id) = SettingFixedAsset?.CurrencyId
        End If

        Return New ActionResult With {.StateResult = False}
    End Function

    ''' <summary>
    ''' Funcion que se encarga de consultar el TRM
    ''' </summary>
    ''' <param name="_currencyId"></param>
    ''' <param name="ToCurrencyId"></param>
    ''' <returns></returns>
    Private Async Function GetTRM(_currencyId As Integer, ToCurrencyId As Integer) As Task(Of Boolean)
        Using Model As New Inventory.MVP.MInventoryContract("")
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

    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me.FixedAssetTransaction IsNot Nothing AndAlso Me.FixedAssetTransaction.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                Await DeleteBlockedRecord()
                Me.INDBteCode.Text = Me.IdEntity.Trim()
                Await LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDBteCode.Text = Me.IdEntity.Trim()
            Await LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    Private Async Sub Frm_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Await DeleteBlockedRecord()
    End Sub
#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de tercero
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleThirdParty_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleThirdParty.QueryPopUp
        If ThirdPartyXpo Is Nothing Then
            Presenter.InitializeThirdParty()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de cuenta credito
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSlCreditAccount_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlCreditAccount.QueryPopUp
        If MainAccountXpo Is Nothing Then
            Presenter.InitializeMainAccount()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de centro de costo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCostCenter_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleCostCenter.QueryPopUp
        If CostCenterXpo Is Nothing Then
            Presenter.InitializeCostCenter()
        End If
    End Sub

    Private Sub INDSlDistributionLines_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlDistributionLines.QueryPopUp
        If SupplierDistributionLineXpo Is Nothing Then
            Presenter.InitializeSupplierDistribution()
        End If
    End Sub

    Private Async Sub INDSlSupplierType_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlSupplierType.QueryPopUp
        If SupplierTypeXpo Is Nothing Then
            Await InitializeSupplierType()
        End If
    End Sub

    ''' <summary>
    ''' Evento para consultar el maestro de moneda
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCurrency_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleCurrency.QueryPopUp
        If INDsleCurrency.Properties.DataSource Is Nothing Then
            Presenter.GetListCurrency()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara para abrir el form de tercero
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleThirdParty_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleThirdParty.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(532, Nothing, True)
            Presenter.InitializeThirdParty()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el form de cuenta contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleMainAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            Presenter.InitializeMainAccount()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el form de centro de costo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCostCenter_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCostCenter.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(517, Nothing, True)
            Presenter.InitializeCostCenter()
        End If
    End Sub

    Private Async Sub INDSlSupplierType_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSlSupplierType.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(726, Nothing, True)
            If SupplierId > 0 Then
                Await InitializeSupplierType()
            End If
        End If
    End Sub

#End Region

#Region "KeyDown"
    Private Async Sub INDbtnCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBteCode.KeyDown
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
                    Await Me.NewValorization()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub
#End Region

#Region "EditValueChanged"
    Private Sub INDsleAccountPayable_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleAccountPayable.EditValueChanged
        If GenerateAccountPayable Then
            INDlyItemThirdParty.HideLayout()
            INDsleThirdParty.EditValue = Nothing
            INDSlCreditAccount.ReadOnly = False

            INDLciCurrency.ShowLayout()
            INDDtInvoiceDate.EditValue = Date.Now()
            AdditionalControlPanel.Controls.Add(ctrTmp)
            INDlygAssets.HideControl(False)
            ToggleTaxRegistrationFieldVisibility()
        Else
            INDlygAssets.HideControl()
            INDLciCurrency.HideLayout()
            AdditionalControlPanel.Controls.Remove(ctrTmp)

            INDSlCreditAccount.ReadOnly = True
            INDSlCreditAccount.EditValue = SettingFixedAsset.ServiceMainAccountId
            INDSlCreditAccount.Properties.NullText = SettingFixedAsset.ServiceMainAccountNumberName
            INDlyItemThirdParty.ShowLayout()
        End If
    End Sub

    Private Sub INDSlDistributionLines_EditValueChanged(sender As Object, e As EventArgs) Handles INDSlDistributionLines.EditValueChanged
        _supplier = Nothing
        SupplierId = Nothing
        SupplierTypeXpo = Nothing
        _suppliersDistibutionLine = Nothing

        If SupplierDistributionLineId IsNot Nothing Then
            If SupplierDistributionLineXpo Is Nothing Then
                _suppliersDistibutionLine = Presenter.GetSupplierDistributionLineById(SupplierDistributionLineId)
            Else
                _suppliersDistibutionLine = DirectCast(DirectCast(INDGvSupplier.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.CommonRepository.CommonSuppliersDistibutionLineXpo)
            End If

            SupplierId = _suppliersDistibutionLine.IdSupplier.Id
            INDsleThirdParty.EditValue = _suppliersDistibutionLine.IdSupplier.IdThirdParty.Id
            INDSlCreditAccount.EditValue = _suppliersDistibutionLine.IdDistributionLine.IdMainAccount.Id
            INDSlCreditAccount.Properties.NullText = _suppliersDistibutionLine.IdDistributionLine.IdMainAccount.NumberName

            Using modelSupplier As New MSupplier(MyTag)
                _supplier = modelSupplier.GetSupplierById(SupplierId)
                DayPeriod = _supplier.TimeLimitDays
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento para cuando se cambia la moneda 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDsleCurrency_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCurrency.EditValueChanged
        If INDsleCurrency.EditValue Is Nothing OrElse Me.CurrencyId = 0 Then
            Exit Sub
        End If

        Await Me.ValidateCurrencyEditValue(Me.FlagLoad, Me.CurrencyId)
    End Sub

#End Region

#Region "Click"
    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        OpenFormValorizationDetail(False)
    End Sub
#End Region

#Region "CustomColumnDisplayText"

    Private Sub INDGvTransactionDetail_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles INDviewItem.CustomColumnDisplayText

        If e.Value Is Nothing OrElse e.Value.GetType.ToString = "DevExpress.Data.NotedObject" Then
            Exit Sub
        End If
        If e.Column.Name = INDColTransactionClass.Name Then
            Select Case e.Value
                Case 1
                    e.DisplayText = "Activo"
                Case 2
                    e.DisplayText = "Parte de Activo"
                Case Else

            End Select
        End If

        If e.Column.Name = INDColTransactionType.Name Then
            Select Case e.Value
                Case 1
                    e.DisplayText = "Valorización"
                Case 2
                    e.DisplayText = "Desvalorización"
                Case Else

            End Select
        End If

        If e.Column.Name = INDColUnitLifeTime.Name Then
            Select Case e.Value
                Case 1
                    e.DisplayText = "Año"
                Case 2
                    e.DisplayText = "Mes"
                Case 3
                    e.DisplayText = "Día"
                Case Else

            End Select
        End If
    End Sub


#End Region

#Region "DataSourceChanged"

    Private Sub INDGcTransactionDetail_DataSourceChanged(sender As Object, e As EventArgs) Handles INDGcTransactionDetail.DataSourceChanged
        'Permitir editar si no existen datos en la rejilla
        If ListFixedAssetTransactionDetail Is Nothing OrElse ListFixedAssetTransactionDetail.Count = 0 Then
            INDsleAccountPayable.Properties.ReadOnly = False
            INDsleThirdParty.Properties.ReadOnly = False
            INDSlDistributionLines.Properties.ReadOnly = False
        Else
            INDsleAccountPayable.Properties.ReadOnly = True
            INDsleThirdParty.Properties.ReadOnly = True
            INDSlDistributionLines.Properties.ReadOnly = True
        End If
    End Sub

#End Region

#End Region

End Class