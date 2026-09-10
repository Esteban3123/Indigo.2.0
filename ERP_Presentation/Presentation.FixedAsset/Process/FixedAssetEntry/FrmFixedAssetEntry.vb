'***********************************************************************
' Assembly         : Presentacion.FixedAsset
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 19-01-2016
'
' Last Modified By : Carlos Mario Arias Rubiano
' Last Modified On : 15/04/2016
' Description      : Se refactoriza todo el form por los nuevos cambios
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.FixedAsset.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Domain.Entities
Imports Domain.Base.Entities
Imports DevExpress.Xpo
Imports Presentation.Base.BaseClass
Imports System.Text
Imports Presentation.Payments.MVP
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports Presentation.Inventory
Imports System.ComponentModel
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports Infrastructure.Data.Xpo.BudgetRepository
Imports Presentation.Maintenance.MVP
Imports Presentation.Accounting.MVP
Imports Presentation.Inventory.MVP

#End Region

Public Class FrmFixedAssetEntry
    Implements IFixedAssetEntry, ICustomizableForm

#Region "Builder"

    Public Sub New()
        InitializeComponent()
        ctrTmp = New CtrTotalInvoiceEntranceVoucher()
        ctrTmp.SetInfoFunction(AddressOf getValuesRetention)
        ctrTmp.PrintInfo()
        ctrTmp.PopupContainerControlTotalValue = PopUpSummarySettlement
        ctrTmp.Dock = System.Windows.Forms.DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrTmp)
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
    ''' Datasource compromiso
    ''' </summary>
    ''' <returns></returns>
    Public Property CommitmentDetailXpo As XPCollection Implements IFixedAssetEntry.CommitmentDetailXpo
        Get
            Return INDsleCommitmentDetail.Properties.DataSource
        End Get
        Set(value As XPCollection)
            INDsleCommitmentDetail.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datsource entidad presupuestal
    ''' </summary>
    ''' <returns></returns>
    Public Property BudgetaryEntityXpo As XPCollection Implements IFixedAssetEntry.BudgetaryEntityXpo
        Get
            Return INDsleBudgetaryEntity.Properties.DataSource
        End Get
        Set(value As XPCollection)
            INDsleBudgetaryEntity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource vigencia
    ''' </summary>
    ''' <returns></returns>
    Public Property BudgetaryValidityXpo As XPCollection Implements IFixedAssetEntry.BudgetaryValidityXpo
        Get
            Return INDsleBudgetaryValidity.Properties.DataSource
        End Get
        Set(value As XPCollection)
            INDsleBudgetaryValidity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Registro tipo IVA
    ''' </summary>
    ''' <returns></returns>
    Public Property TaxRegistration As Integer? Implements IFixedAssetEntry.TaxRegistration
        Get
            Return INDsleTaxRegistration.EditValue
        End Get
        Set(value As Integer?)
            INDsleTaxRegistration.EditValue = value
        End Set
    End Property

    Public Property NumberContractLeasing As String Implements IFixedAssetEntry.NumberContractLeasing
        Get
            Return INDtxtNumberContractLeasing.EditValue
        End Get
        Set(value As String)
            INDtxtNumberContractLeasing.EditValue = value
        End Set
    End Property

    Public Property InitialDateLeasing As Date? Implements IFixedAssetEntry.InitialDateLeasing
        Get
            Return INDdteInitialDateLeasing.EditValue
        End Get
        Set(value As Date?)
            INDdteInitialDateLeasing.EditValue = value
        End Set
    End Property

    Public Property EndDateLeasing As Date? Implements IFixedAssetEntry.EndDateLeasing
        Get
            Return INDdteEndDateLeasing.EditValue
        End Get
        Set(value As Date?)
            INDdteEndDateLeasing.EditValue = value
        End Set
    End Property

    Public Property CostCenterId As Integer? Implements IFixedAssetEntry.CostCenterId
        Get
            Return INDsleCostCenter.EditValue
        End Get
        Set(value As Integer?)
            INDsleCostCenter.EditValue = value
        End Set
    End Property

    Public Property CostCenterXpo As XPInstantFeedbackSource Implements IFixedAssetEntry.CostCenterXpo
        Get
            Return INDsleCostCenter.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCostCenter.Properties.DataSource = value
        End Set
    End Property

    Private _SettingsFixedAsset As SettingFixedAsset
    Public Property SettingsFixedAsset As SettingFixedAsset Implements IFixedAssetEntry.SettingsFixedAsset
        Get
            Return _SettingsFixedAsset
        End Get
        Set(value As SettingFixedAsset)
            _SettingsFixedAsset = value
        End Set
    End Property

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IFixedAssetEntry.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    Public ReadOnly Property MyTag As Object Implements IFixedAssetEntry.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    Public Property Sequense As FixedAssetSequence Implements IFixedAssetEntry.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As FixedAssetSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.FixedAssetSequenceDetail In Me._sequence.FixedAssetSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    Public Property Code As String Implements IFixedAssetEntry.Code
        Get
            If (INDbtnCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDbtnCode.Text
            End If
        End Get
        Set(value As String)
            INDbtnCode.Text = value
        End Set
    End Property

    Public Property AdquisitionType As Integer? Implements IFixedAssetEntry.AdquisitionType
        Get
            Return INDsleAdquisitionType.EditValue
        End Get
        Set(value As Integer?)
            INDsleAdquisitionType.EditValue = value
        End Set
    End Property

    Public Property DayPeriod As Integer Implements IFixedAssetEntry.DayPeriod
        Get
            Return INDseDayPeriod.EditValue
        End Get
        Set(value As Integer)
            INDseDayPeriod.EditValue = value
        End Set
    End Property

    Public Property EntryDate As Date? Implements IFixedAssetEntry.EntryDate
        Get
            Return INDdteEntryDate.EditValue
        End Get
        Set(value As Date?)
            INDdteEntryDate.EditValue = value
        End Set
    End Property

    Public Property EntryNumber As String Implements IFixedAssetEntry.EntryNumber
        Get
            Return INDtxtEntryNumber.EditValue
        End Get
        Set(value As String)
            INDtxtEntryNumber.EditValue = value
        End Set
    End Property

    Public Property FreightIVAValue As Decimal Implements IFixedAssetEntry.FreightIVAValue
        Get
            Return INDtxtFreightIVAValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtFreightIVAValue.EditValue = value
        End Set
    End Property

    Public Property FreightValue As Decimal Implements IFixedAssetEntry.FreightValue
        Get
            Return INDtxtFreightValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtFreightValue.EditValue = value
        End Set
    End Property

    Public Property GetLocationResponsible As Integer? Implements IFixedAssetEntry.GetLocationResponsible
        Get
            Return INDsleGetLocationResponsible.EditValue
        End Get
        Set(value As Integer?)
            INDsleGetLocationResponsible.EditValue = value
        End Set
    End Property

    Public Property IcaPercentage As Decimal Implements IFixedAssetEntry.IcaPercentage
        Get
            Return INDseIcaPercentage.EditValue
        End Get
        Set(value As Decimal)
            INDseIcaPercentage.EditValue = value
        End Set
    End Property

    Public Property InvoiceDate As Date? Implements IFixedAssetEntry.InvoiceDate
        Get
            Return INDdteInvoiceDate.EditValue
        End Get
        Set(value As Date?)
            INDdteInvoiceDate.EditValue = value
        End Set
    End Property

    Public Property InvoiceNumber As String Implements IFixedAssetEntry.InvoiceNumber
        Get
            Return INDtxtInvoiceNumber.EditValue
        End Get
        Set(value As String)
            INDtxtInvoiceNumber.EditValue = value
        End Set
    End Property

    Public Property LocationId As Integer? Implements IFixedAssetEntry.LocationId
        Get
            Return INDsleLocation.EditValue
        End Get
        Set(value As Integer?)
            INDsleLocation.EditValue = value
        End Set
    End Property

    Public Property LocationXpo As XPCollection Implements IFixedAssetEntry.LocationXpo
        Get
            Return INDsleLocation.Properties.DataSource
        End Get
        Set(value As XPCollection)
            INDsleLocation.Properties.DataSource = value
        End Set
    End Property

    Public Property ResponsibleId As Integer? Implements IFixedAssetEntry.ResponsibleId
        Get
            Return INDsleResponsible.EditValue
        End Get
        Set(value As Integer?)
            INDsleResponsible.EditValue = value
        End Set
    End Property

    Public Property ResponsibleXpo As XPInstantFeedbackSource Implements IFixedAssetEntry.ResponsibleXpo
        Get
            Return INDsleResponsible.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleResponsible.Properties.DataSource = value
        End Set
    End Property

    Public Property RoundService As Integer? Implements IFixedAssetEntry.RoundService
        Get
            Return INDsleRoundService.EditValue
        End Get
        Set(value As Integer?)
            INDsleRoundService.EditValue = value
        End Set
    End Property

    Public Property SupplierDistributionLineId As Integer? Implements IFixedAssetEntry.SupplierDistributionLineId
        Get
            Return INDsleSupplierDistributionLine.EditValue
        End Get
        Set(value As Integer?)
            INDsleSupplierDistributionLine.EditValue = value
        End Set
    End Property

    Public Property SupplierDistributionLineXpo As XPInstantFeedbackSource Implements IFixedAssetEntry.SupplierDistributionLineXpo
        Get
            Return INDsleSupplierDistributionLine.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleSupplierDistributionLine.Properties.DataSource = value
        End Set
    End Property

    Public Property SupplierTypeId As Integer? Implements IFixedAssetEntry.SupplierTypeId
        Get
            Return INDsleSupplierType.EditValue
        End Get
        Set(value As Integer?)
            INDsleSupplierType.EditValue = value
        End Set
    End Property

    Public Property SupplierTypeXpo As List(Of SupplierType) Implements IFixedAssetEntry.SupplierTypeXpo
        Get
            Return INDsleSupplierType.Properties.DataSource
        End Get
        Set(value As List(Of SupplierType))
            INDsleSupplierType.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece la actividad economica
    ''' </summary>
    ''' <returns></returns>
    Public Property EconomicActivity As Integer?
        Get
            Return INDsleEconomicActivity.EditValue
        End Get
        Set(value As Integer?)
            INDsleEconomicActivity.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el datasource de la actividad economica
    ''' </summary>
    ''' <returns></returns>
    Public Property EconomicActivityDatasource As XPInstantFeedbackSource Implements IFixedAssetEntry.EconomicActivityDatasource
        Get
            Return CType(INDsleEconomicActivity.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleEconomicActivity.Properties.DataSource = value
        End Set
    End Property

    Public Property Description As String Implements IFixedAssetEntry.Description
        Get
            Return INDmemoDescription.EditValue
        End Get
        Set(value As String)
            INDmemoDescription.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Porpiedad que contiene el listado de centros de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentSupportXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IFixedAssetEntry.DocumentSupportXpo
        Get
            Return CType(INDSleDocumentSupportId.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleDocumentSupportId.Properties.DataSource = value
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
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Byte
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As Byte)
            Me.BarraBotones.StatusRecord = value.ToString()
        End Set
    End Property


    Public Property SupplierId As Integer? Implements IFixedAssetEntry.SupplierId

    Public Property FreightIVAPercentage As Decimal Implements IFixedAssetEntry.FreightIVAPercentage

    Private ReadOnly Property FillingHandleDocumentSupport As List(Of Tuple(Of Boolean, String))
        Get
            If _FillingHandleDocumentSupport Is Nothing Then
                _FillingHandleDocumentSupport = New List(Of Tuple(Of Boolean, String))
                _FillingHandleDocumentSupport.Add(New Tuple(Of Boolean, String)(True, "Si"))
                _FillingHandleDocumentSupport.Add(New Tuple(Of Boolean, String)(False, "No"))
            End If
            Return _FillingHandleDocumentSupport
        End Get
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' Permite saber si el grupo de presupuesto es visible
    ''' </summary>
    Private IsVisibleGroupBudget As Boolean = False

    ''' <summary>
    ''' Listado de compromisos
    ''' </summary>
    Private ListFixedAssetEntryCommitment As List(Of FixedAssetEntryCommitment)

    ''' <summary>
    ''' Listado de eliminados de compromisos
    ''' </summary>
    Private ListDeleteFixedAssetEntryCommitment As List(Of FixedAssetEntryCommitment)

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
    ''' Control para establecer informacion del ingreso
    ''' </summary>
    Private ctrTmp As CtrTotalInvoiceEntranceVoucher

    ''' <summary>
    ''' Variable que contiene el objeto torre
    ''' </summary>
    Dim FixedAssetEntry As FixedAssetEntry

    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PFixedAssetEntry

    ''' <summary>
    ''' Representa a la entidad de parámetros de cxp
    ''' </summary>
    Dim PaymentsSettingPaymentsXpo As PaymentsSettingPaymentsXpo

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordFixedAsset

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "FixedAssets"

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.FixedAssetSequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListAdquisitionType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListRoundService As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que contiene la lista de registro de tipo de IVA
    ''' </summary>
    Dim ListTaxRegistration As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListGetLocationResponsible As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Listado de detalles del ingreso de activo
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListFixedAssetEntryItem As List(Of FixedAssetEntryItem)

    ''' <summary>
    ''' Variable para la entidad que se va a editar en la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Dim FixedAssetEntryItem As FixedAssetEntryItem

    ''' <summary>
    ''' indice del registro que se esta editando para luego insertarlo en la misma posicion que estaba
    ''' </summary>
    ''' <remarks></remarks>
    Dim IndexEditRecord As Integer

    ''' <summary>
    ''' Listado de libros oficiales
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteFixedAssetEntryItemDetailPartBook As List(Of FixedAssetEntryItemDetailPartBook)

    ''' <summary>
    ''' Listado de libros oficiales
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteFixedAssetEntryItemDetailPart As List(Of FixedAssetEntryItemDetailPart)

    ''' <summary>
    ''' Listado de libros oficiales
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteFixedAssetEntryItemDetailBook As List(Of FixedAssetEntryItemDetailBook)

    ''' <summary>
    ''' Listado de libros oficiales
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteFixedAssetEntryItemDetail As List(Of FixedAssetEntryItemDetail)

    ''' <summary>
    ''' Listado de libros oficiales
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteFixedAssetEntryItem As List(Of FixedAssetEntryItem)

    ''' <summary>
    ''' Permite saber si se puede cambiar el valor del search
    ''' </summary>
    ''' <remarks></remarks>
    Dim FlagLoad As Boolean = True

    ''' <summary>
    ''' Lista los valores de la retencion
    ''' ListTuple(0) = el valor del iva,
    ''' ListTuple(1) = el valor del ica,
    ''' ListTuple(2) = el valor del retefuente
    ''' ListTuple(3) = el listado con los valores de rtfValue y rtfPercentage de cada item
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListTuple As List(Of Tuple(Of Decimal, List(Of FixedAssetEntryItem)))

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim varImp As Integer

    ''' <summary>
    ''' Tercero de la empresa actualmente seleccionada
    ''' </summary>
    Private _currentCompany As ThirdParty

    ''' <summary>
    ''' Linea de distribucion seleccionada del proveedor
    ''' </summary>
    Dim _suppliersDistibutionLine As Infrastructure.Data.Xpo.CommonRepository.CommonSuppliersDistibutionLineXpo

    ''' <summary>
    ''' entidad de proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Dim _supplier As New Domain.Entities.Supplier

    ''' <summary>
    ''' Concepto de retención de ICA
    ''' </summary>
    Dim _icaRetentionConcept As RetentionConcepts

    ''' <summary>
    ''' Tasa de cambio
    ''' </summary>
    Dim _tRM As TRM
    ''' <summary>
    ''' Contiene los valores de Organización y Definición del Tenant
    ''' </summary>
    Dim companySettings As CompanySettings

    Private _FillingHandleDocumentSupport As List(Of Tuple(Of Boolean, String))


#End Region

#Region "ICrud"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.ICrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
        Try
            If ValidateControls() = True Then
                If FixedAssetEntry.Status <> 3 Then
                    If ListFixedAssetEntryItem Is Nothing OrElse ListFixedAssetEntryItem.Count = 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = "Debe ingresar al menos un articulo."
                        Exit Sub
                    End If
                    AssigningValues()
                End If
                Using model As New MFixedAssetEntry(MyTag)
                    AsyncLoader(True)
                    Dim Result = Await model.SaveFixedAssetEntry(FixedAssetEntry, ListDeleteFixedAssetEntryItemDetailPartBook, ListDeleteFixedAssetEntryItemDetailPart, ListDeleteFixedAssetEntryItemDetailBook, ListDeleteFixedAssetEntryItemDetail, ListDeleteFixedAssetEntryItem, _idCurrentSequence)
                    If Result.StateResult = True Then
                        If FixedAssetEntry.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                            'Se descarta la secuencia numerica usada
                            If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                                Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                            End If
                            If FixedAssetEntry.Status = 1 Then
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                            ElseIf FixedAssetEntry.Status = 2 Then
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SaveConfirm"), Result.ObjectEmbbeded.Code)
                            End If
                        ElseIf FixedAssetEntry.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                            If FixedAssetEntry.Status = 3 Then
                                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                            ElseIf FixedAssetEntry.Status = 2 Then
                                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateConfirm")
                            ElseIf FixedAssetEntry.Status = 1 Then
                                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                            End If
                        End If

                        Me.FixedAssetEntry = Result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)

                        Select Case varImp
                            Case 1
                                Me.BarraBotones.PrintReport(PrintReportAction.Create, FixedAssetEntry.Id, 0, FixedAssetEntry.Id, _idOperativeUnit)
                            Case 2
                                Me.BarraBotones.PrintReport(PrintReportAction.Update, FixedAssetEntry.Id, 0, FixedAssetEntry.Id, _idOperativeUnit)
                            Case 3
                                Me.BarraBotones.PrintReport(PrintReportAction.Confirm, FixedAssetEntry.Id, 0, FixedAssetEntry.Id, _idOperativeUnit)
                            Case 4
                                Me.BarraBotones.PrintReport(PrintReportAction.Cancel, FixedAssetEntry.Id, 0, FixedAssetEntry.Id, _idOperativeUnit)
                        End Select

                        AsyncLoader(False)
                        Me.Deshacer()
                    Else
                        AsyncLoader(False)
                        INDbtnCode.Enabled = False
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
            INDbtnCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements Base.ICrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewEntry()
        End If
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Fecha Ingreso", .FieldName = "EntryDate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "No. Documento", .FieldName = "EntryNumber", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Tipo Adquisición", .FieldName = "AdquisitionTypeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetEntry
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        Await DeleteBlockedRecord()
        INDbtnCode.Text = ReturnValue
        If INDbtnCode.Text <> String.Empty Then
            Await LoadControls()
            If INDbtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbtnCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Confirmar()
        Try
            If ValidateControls() = True Then
                If FixedAssetEntry.Status <> 3 Then
                    If ListFixedAssetEntryItem Is Nothing OrElse ListFixedAssetEntryItem.Count = 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = "Debe ingresar al menos un articulo."
                        Exit Sub
                    End If
                    AssigningValues()
                End If
                If companySettings.TaxRegistration = 3 AndAlso (FixedAssetEntry?.TaxRegistration = 0 Or FixedAssetEntry?.TaxRegistration = 3) AndAlso FixedAssetEntry.Status = 2 Then
                    Mensaje(EeventViewerImages.Advertencia) = "Se debe ingresar Registro Iva"
                    Exit Sub
                End If
                Using model As New MFixedAssetEntry(MyTag)
                    AsyncLoader(True)
                    Dim Result = Await model.ConfirmFixedAssetEntry(FixedAssetEntry, ListDeleteFixedAssetEntryItemDetailPartBook, ListDeleteFixedAssetEntryItemDetailPart, ListDeleteFixedAssetEntryItemDetailBook, ListDeleteFixedAssetEntryItemDetail, ListDeleteFixedAssetEntryItem, _idCurrentSequence)
                    If Result.StatusCode = eStatusResult.SUCCESS Then
                        Mensaje(EeventViewerImages.Informacion) = Result.Message
                        AsyncLoader(False)
                        Me.FixedAssetEntry = Result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)

                        Select Case varImp
                            Case 1
                                Me.BarraBotones.PrintReport(PrintReportAction.Create, FixedAssetEntry.Id, 0, FixedAssetEntry.Id, _idOperativeUnit)
                            Case 2
                                Me.BarraBotones.PrintReport(PrintReportAction.Update, FixedAssetEntry.Id, 0, FixedAssetEntry.Id, _idOperativeUnit)
                            Case 3
                                Me.BarraBotones.PrintReport(PrintReportAction.Confirm, FixedAssetEntry.Id, 0, FixedAssetEntry.Id, _idOperativeUnit)
                            Case 4
                                Me.BarraBotones.PrintReport(PrintReportAction.Cancel, FixedAssetEntry.Id, 0, FixedAssetEntry.Id, _idOperativeUnit)
                        End Select

                        AsyncLoader(False)
                        Me.Deshacer()
                    Else
                        AsyncLoader(False)
                        FixedAssetEntry.Status = 1 'Cambia el estado si falla al confirmar
                        INDbtnCode.Enabled = False
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
            INDbtnCode.Enabled = False
            Throw ex
        End Try
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Método que elimina un compromiso de la rejilla
    ''' </summary>
    Private Sub DeleteCommitment()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Dim entity As FixedAssetEntryCommitment = INDviewGridCommitment.GetFocusedRow()
        ListFixedAssetEntryCommitment.Remove(entity)

        If entity.Id > 0 Then
            If ListDeleteFixedAssetEntryCommitment Is Nothing Then
                ListDeleteFixedAssetEntryCommitment = New List(Of FixedAssetEntryCommitment)
            End If
            entity.MarkAsDeleted()
            ListDeleteFixedAssetEntryCommitment.Add(entity)
        End If

        INDgcCommitment.DataSource = Nothing
        INDgcCommitment.DataSource = ListFixedAssetEntryCommitment
    End Sub

    ''' <summary>
    ''' Valida los controles del popup
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControlsPopup() As Boolean
        Dim errors As New StringBuilder

        If INDsleBudgetaryEntity.EditValue Is Nothing Then
            errors.AppendLine("Debe seleccionar una entidad presupuestal")
        End If

        If INDsleBudgetaryValidity.EditValue Is Nothing Then
            errors.AppendLine("Debe seleccionar una vigencia")
        End If

        If INDsleCommitmentDetail.EditValue Is Nothing Then
            errors.AppendLine("Debe seleccionar un compromiso")
        End If

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Return False
        End If

        Return True
    End Function

    Private WriteOnly Property ActionsOnControlsPopupDetails As Boolean
        Set(value As Boolean)
            INDpopupDetails.Enabled = value
            INDlyDetails.Enabled = value
            LayoutControlGroup3.Enabled = value
            INDgcDetails.Enabled = value
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
    ''' Calcula el total del valor de la cxp
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CalculateTotalValue()
        INDPopTxtTotalCxp.EditValue = INDPopTxtValue.EditValue + INDPopTxtValueTax.EditValue + INDPopTxtFreightValue.EditValue + INDPopSpnFreightIVA.EditValue - INDPopTxtDiscountValue.EditValue - INDPopTxtWithholdingTax.EditValue - INDPopTxtWithholdingICA.EditValue - INDPopTxtRetentionSource.EditValue
        ctrTmp.PrintInfo()
    End Sub

    ''' <summary>
    ''' Muestra o esconde el control de TaxRegistration dependiendo del Registro IVA en CompanySettings
    ''' </summary>
    Private Sub PrintTaxRegistrationField()
        If companySettings IsNot Nothing Then
            If companySettings.TaxRegistration = 3 Then
                INDLciTaxRegistration.ShowLayout()
                TaxRegistration = 1

            Else
                TaxRegistration = companySettings.TaxRegistration
                INDLciTaxRegistration.HideLayout()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Inicializa los search que van quemados
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeTuple()
        'Tipo de adquisición
        ListAdquisitionType = New List(Of Tuple(Of Integer, String))
        ListAdquisitionType.Add(New Tuple(Of Integer, String)(1, "Compra Directa"))
        ListAdquisitionType.Add(New Tuple(Of Integer, String)(3, "Comodato"))
        ListAdquisitionType.Add(New Tuple(Of Integer, String)(8, "Comodato Tercerizado"))
        ListAdquisitionType.Add(New Tuple(Of Integer, String)(4, "Donación"))
        ListAdquisitionType.Add(New Tuple(Of Integer, String)(5, "Traspaso de Bienes"))
        ListAdquisitionType.Add(New Tuple(Of Integer, String)(6, "Otro Concepto"))
        ListAdquisitionType.Add(New Tuple(Of Integer, String)(7, "Leasing Financiero"))
        ListAdquisitionType.Add(New Tuple(Of Integer, String)(9, "Renting Financiero"))
        ListAdquisitionType.Add(New Tuple(Of Integer, String)(10, "Renting Operativo"))
        INDsleAdquisitionType.Properties.DataSource = ListAdquisitionType.ToList

        'Redondeo
        ListRoundService = New List(Of Tuple(Of Integer, String))
        ListRoundService.Add(New Tuple(Of Integer, String)(0, "Ninguno"))
        ListRoundService.Add(New Tuple(Of Integer, String)(10, "A la Décima"))
        ListRoundService.Add(New Tuple(Of Integer, String)(100, "A la Centésima"))
        ListRoundService.Add(New Tuple(Of Integer, String)(1000, "A la Milésima"))
        INDsleRoundService.Properties.DataSource = ListRoundService.ToList

        'Ubicación y responsable
        ListGetLocationResponsible = New List(Of Tuple(Of Integer, String))
        ListGetLocationResponsible.Add(New Tuple(Of Integer, String)(1, "Responsable y Ubicación General"))
        ListGetLocationResponsible.Add(New Tuple(Of Integer, String)(2, "Responsable y Ubicación Específico"))
        INDsleGetLocationResponsible.Properties.DataSource = ListGetLocationResponsible.ToList

        'Registro IVA
        ListTaxRegistration = New List(Of Tuple(Of Integer, String))
        ListTaxRegistration.Add(New Tuple(Of Integer, String)(1, "IVA al costo control fiscal"))
        ListTaxRegistration.Add(New Tuple(Of Integer, String)(2, "IVA descontable"))
        ListTaxRegistration.Add(New Tuple(Of Integer, String)(4, "IVA al costo"))
        INDsleTaxRegistration.Properties.DataSource = ListTaxRegistration.ToList

        'Maneja Documento Soporte
        INDSleHandleDocumentSupport.Properties.DataSource = FillingHandleDocumentSupport
        INDSleHandleDocumentSupport.EditValue = False
    End Sub

    ''' <summary>
    ''' Metodo que inicializa el datasource del control de tipo de proveedor
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function InitializeSupplierType() As Task
        Using model As New MSupplierType(Tag)
            Dim x As ActionResult(Of List(Of SupplierType)) = Await model.GetSupplierTypeBySupplierId(SupplierId)
            Dim listSupplierType As List(Of SupplierType) = x.ObjectEmbbeded
            SupplierTypeXpo = listSupplierType
        End Using
    End Function

    ''' <summary>
    ''' Elimina el detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RemoveDetail()
        FixedAssetEntryItem = CType(INDviewItem.GetFocusedRow, FixedAssetEntryItem)
        ListFixedAssetEntryItem.Remove(FixedAssetEntryItem)

        If FixedAssetEntryItem.Id > 0 Then
            If ListDeleteFixedAssetEntryItem Is Nothing Then
                ListDeleteFixedAssetEntryItem = New List(Of FixedAssetEntryItem)
            End If
            ListDeleteFixedAssetEntryItem.Add(FixedAssetEntryItem)
            If FixedAssetEntryItem.FixedAssetEntryItemDetail IsNot Nothing AndAlso FixedAssetEntryItem.FixedAssetEntryItemDetail.Count > 0 Then
                For Each itemDetail In (From l In FixedAssetEntryItem.FixedAssetEntryItemDetail Where l.Id > 0 Select l).ToList
                    If ListDeleteFixedAssetEntryItemDetail Is Nothing Then
                        ListDeleteFixedAssetEntryItemDetail = New List(Of FixedAssetEntryItemDetail)
                    End If
                    If ListDeleteFixedAssetEntryItemDetail.Contains(itemDetail) Then
                        Continue For
                    End If
                    ListDeleteFixedAssetEntryItemDetail.Add(itemDetail)
                    'Se recorren los libros del articulo, si hay para eliminarlos
                    If itemDetail.FixedAssetEntryItemDetailBook IsNot Nothing AndAlso itemDetail.FixedAssetEntryItemDetailBook.Count > 0 Then
                        For Each item In (From l In itemDetail.FixedAssetEntryItemDetailBook Where l.Id > 0 Select l).ToList
                            If ListDeleteFixedAssetEntryItemDetailBook Is Nothing Then
                                ListDeleteFixedAssetEntryItemDetailBook = New List(Of FixedAssetEntryItemDetailBook)
                            End If
                            If ListDeleteFixedAssetEntryItemDetailBook.Contains(item) Then
                                Continue For
                            End If
                            ListDeleteFixedAssetEntryItemDetailBook.Add(item)
                        Next
                    End If
                    'Se recorren las partes y los libros de las partes, si hay para eliminarlos
                    If itemDetail.FixedAssetEntryItemDetailPart IsNot Nothing AndAlso itemDetail.FixedAssetEntryItemDetailPart.Count > 0 Then
                        For Each itemPart In (From l In itemDetail.FixedAssetEntryItemDetailPart Where l.Id > 0 Select l).ToList 'Se recorre las partes
                            If itemPart.FixedAssetEntryItemDetailPartBook IsNot Nothing AndAlso itemPart.FixedAssetEntryItemDetailPartBook.Count > 0 Then
                                For Each itemPDB In (From l In itemPart.FixedAssetEntryItemDetailPartBook Where l.Id > 0 Select l).ToList 'Se recorren los libros de las partes
                                    If ListDeleteFixedAssetEntryItemDetailPartBook Is Nothing Then
                                        ListDeleteFixedAssetEntryItemDetailPartBook = New List(Of FixedAssetEntryItemDetailPartBook)
                                    End If
                                    If ListDeleteFixedAssetEntryItemDetailPartBook.Contains(itemPDB) Then
                                        Continue For
                                    End If
                                    ListDeleteFixedAssetEntryItemDetailPartBook.Add(itemPDB)
                                Next
                            End If
                            If ListDeleteFixedAssetEntryItemDetailPart Is Nothing Then
                                ListDeleteFixedAssetEntryItemDetailPart = New List(Of FixedAssetEntryItemDetailPart)
                            End If
                            If ListDeleteFixedAssetEntryItemDetailPart.Contains(itemPart) Then
                                Continue For
                            End If
                            ListDeleteFixedAssetEntryItemDetailPart.Add(itemPart)
                        Next
                    End If
                Next
            End If
        End If

        If ListFixedAssetEntryItem Is Nothing OrElse ListFixedAssetEntryItem.Count = 0 Then 'Si no hay nada en el listado puede volver a cambiar Ubicacion/Responsable y proveedor
            INDsleGetLocationResponsible.Properties.ReadOnly = False
            INDsleSupplierDistributionLine.Properties.ReadOnly = False
            CleanControlValues(False)
        End If

        Me.CalculateRetentions()
        INDgcItem.DataSource = Nothing
        INDgcItem.DataSource = ListFixedAssetEntryItem
    End Sub

    ''' <summary>
    ''' Edita el detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditDetail()
        FixedAssetEntryItem = DirectCast(INDviewItem.GetFocusedRow(), FixedAssetEntryItem)
        IndexEditRecord = ListFixedAssetEntryItem.IndexOf(FixedAssetEntryItem)
        OpenFormFixedAssetEntryItem(True)
    End Sub

    ''' <summary>
    ''' Metodo que abre el form de saldo inicial articulo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OpenFormFixedAssetEntryItem(EditMode As Boolean)
        Dim errors As New StringBuilder
        If SupplierDistributionLineId Is Nothing Then
            errors.AppendLine("Debe seleccionar " + INDlyItemSupplierDistributionLine.Text)
        End If
        If GetLocationResponsible Is Nothing Then
            errors.AppendLine("Debe seleccionar " + INDlyItemGetLocationResponsible.Text)
        End If
        If EntryDate Is Nothing Then
            errors.AppendLine("Debe ingresar " + INDlyItemEntryDate.Text)
        End If
        If AdquisitionType Is Nothing Then
            errors.AppendLine("Debe ingresar " + INDlyItemAdquisitionType.Text)
        End If
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString
            Exit Sub
        End If
        Using formulario As New FrmFixedAssetEntryItem(RoundService,
                                                             _currency:=New Currency With {.Id = Me.CurrencyId, .Abbreviation = Me.CurrencyAbbreviation},
                                                             _tRMValue:=If(Me.TRM Is Nothing, 1, Me.TRM.Value))
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddFixedAssetEntryItemEventArgs, AddressOf ReturnAddEventArgs
            formulario.SettingsFixedAsset = Me.SettingsFixedAsset
            formulario.EditMode = EditMode
            formulario.ListCompare = ListFixedAssetEntryItem
            formulario.FixedAssetEntryItem = FixedAssetEntryItem
            formulario.GetLocationResponsible = GetLocationResponsible
            formulario.EntryDate = EntryDate
            formulario.ImportData = False
            formulario.AdquisitionType = AdquisitionType
            formulario.Declarant = _supplier.Declarant
            formulario.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.8
            formulario.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.9
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Metodo que agrega el detalle a la entidad principal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnAddEventArgs(sender As Object, e As AddFixedAssetEntryItem)
        If e.EditMode = False Then 'Se esta ingresando un articulo
            If ListFixedAssetEntryItem Is Nothing Then
                ListFixedAssetEntryItem = New List(Of FixedAssetEntryItem)
            End If
            ListFixedAssetEntryItem.Add(e.FixedAssetEntryItem)
            Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente."
        Else 'Se esta modificando un articulo
            ListFixedAssetEntryItem.Remove(FixedAssetEntryItem)
            ListFixedAssetEntryItem.Insert(IndexEditRecord, e.FixedAssetEntryItem)
            Mensaje(EeventViewerImages.Informacion) = "Detalle modificado correctamente."
        End If

        If e.ListDeleteFixedAssetEntryItemDetailPartBook IsNot Nothing Then
            If ListDeleteFixedAssetEntryItemDetailPartBook Is Nothing Then
                ListDeleteFixedAssetEntryItemDetailPartBook = New List(Of FixedAssetEntryItemDetailPartBook)
            End If
            ListDeleteFixedAssetEntryItemDetailPartBook.AddRange(e.ListDeleteFixedAssetEntryItemDetailPartBook)
        End If

        If e.ListDeleteFixedAssetEntryItemDetailPart IsNot Nothing Then
            If ListDeleteFixedAssetEntryItemDetailPart Is Nothing Then
                ListDeleteFixedAssetEntryItemDetailPart = New List(Of FixedAssetEntryItemDetailPart)
            End If
            ListDeleteFixedAssetEntryItemDetailPart.AddRange(e.ListDeleteFixedAssetEntryItemDetailPart)
        End If

        If e.ListDeleteFixedAssetEntryItemDetailBook IsNot Nothing Then
            If ListDeleteFixedAssetEntryItemDetailBook Is Nothing Then
                ListDeleteFixedAssetEntryItemDetailBook = New List(Of FixedAssetEntryItemDetailBook)
            End If
            ListDeleteFixedAssetEntryItemDetailBook.AddRange(e.ListDeleteFixedAssetEntryItemDetailBook)
        End If

        If e.ListDeleteFixedAssetEntryItemDetail IsNot Nothing Then
            If ListDeleteFixedAssetEntryItemDetail Is Nothing Then
                ListDeleteFixedAssetEntryItemDetail = New List(Of FixedAssetEntryItemDetail)
            End If
            ListDeleteFixedAssetEntryItemDetail.AddRange(e.ListDeleteFixedAssetEntryItemDetail)
        End If

        Me.CalculateRetentions()
        INDgcItem.DataSource = Nothing
        INDgcItem.DataSource = ListFixedAssetEntryItem
    End Sub

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.FixedAssetEntry IsNot Nothing AndAlso Me.FixedAssetEntry.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                Await DeleteBlockedRecord()
                Me.INDbtnCode.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbtnCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If

        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Esta propiedad establece si los controles estan o no habilitados
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IFixedAssetEntry.ActionsOnControls
        Set(value As Boolean)
            INDlyEntry.BeginUpdate()
            INDbtnCode.Enabled = Not value
            INDsleSupplierDistributionLine.Enabled = value
            INDsleCostCenter.Enabled = value
            INDsleSupplierType.Enabled = value
            INDdteEntryDate.Enabled = value
            INDtxtEntryNumber.Enabled = value
            INDsleCurrency.Enabled = value
            INDsleTaxRegistration.Enabled = value
            INDmemoDescription.Enabled = value
            INDtxtInvoiceNumber.Enabled = value
            INDdteInvoiceDate.Enabled = value
            INDSleHandleDocumentSupport.Enabled = value
            INDSleDocumentSupportId.Enabled = value
            INDtxtFreightValue.Enabled = value
            INDsleAdquisitionType.Enabled = value
            INDtxtNumberContractLeasing.Enabled = value
            INDdteInitialDateLeasing.Enabled = value
            INDdteEndDateLeasing.Enabled = value
            INDsleRoundService.Enabled = value
            INDsleGetLocationResponsible.Enabled = value
            INDsleLocation.Enabled = value
            INDsleResponsible.Enabled = value
            INDbtnAddItem.Enabled = value
            INDgcItem.Enabled = value
            INDsleEconomicActivity.Enabled = value

            INDpceCommitment.Enabled = value
            INDgcCommitment.Enabled = value

            INDlyEntry.EndUpdate()
            If value Then
                INDsleSupplierDistributionLine.Focus()
            Else
                INDbtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.ICrudBase.Mensaje
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
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.FixedAssetEntry.Code, INDsleSupplierDistributionLine.Text),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.FixedAssetEntry.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.FixedAssetEntry.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.FixedAssetEntry.Code, INDsleSupplierDistributionLine.Text)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.FixedAssetEntry.Code)
            Return Me._doc
        End If
    End Function

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
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDlyEntry.BeginUpdate()

        ReadOnlyControls(False)
        ActionsOnControls = False
        Code = String.Empty
        SupplierDistributionLineId = Nothing
        INDsleSupplierDistributionLine.Properties.NullText = String.Empty
        INDsleSupplierDistributionLine.Properties.ReadOnly = False
        SupplierTypeId = Nothing
        INDsleSupplierType.Properties.NullText = String.Empty
        CostCenterId = Nothing
        INDsleCostCenter.Properties.NullText = String.Empty
        INDlyItemCostCenter.HideLayout()
        SupplierTypeXpo = Nothing
        EconomicActivity = Nothing
        EntryDate = Nothing
        EntryNumber = Nothing
        Me.CurrencyId(SettingsFixedAsset?.Currency?.Abbreviation) = SettingsFixedAsset?.CurrencyId
        Description = Nothing
        InvoiceNumber = Nothing
        InvoiceDate = Nothing
        INDLyItemHandleDocumentSupport.HideControl(True)
        INDSleHandleDocumentSupport.EditValue = False
        INDSleDocumentSupportId.EditValue = Nothing
        INDSleDocumentSupportId.Properties.NullText = String.Empty
        DayPeriod = Nothing
        FlagLoad = False
        FreightValue = Nothing
        FreightIVAValue = Nothing
        IcaPercentage = Nothing
        AdquisitionType = Nothing
        NumberContractLeasing = Nothing
        InitialDateLeasing = Nothing
        EndDateLeasing = Nothing
        RoundService = Nothing
        GetLocationResponsible = Nothing
        INDsleGetLocationResponsible.Properties.ReadOnly = False
        LocationId = Nothing
        INDsleLocation.Properties.NullText = String.Empty
        ResponsibleId = Nothing
        INDsleResponsible.Properties.NullText = String.Empty
        ListFixedAssetEntryItem = Nothing
        ListDeleteFixedAssetEntryItemDetailPartBook = Nothing
        ListDeleteFixedAssetEntryItemDetailPart = Nothing
        ListDeleteFixedAssetEntryItemDetailBook = Nothing
        ListDeleteFixedAssetEntryItemDetail = Nothing
        ListDeleteFixedAssetEntryItem = Nothing
        INDgcItem.DataSource = Nothing

        ListFixedAssetEntryCommitment = Nothing
        ListDeleteFixedAssetEntryCommitment = Nothing
        INDgcCommitment.DataSource = Nothing
        INDsleCommitmentDetail.EditValue = Nothing
        INDsleCommitmentDetail.Properties.DataSource = Nothing
        INDlygBudget.HideControl()

        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        INDlyItemNumberContractLeasing.HideLayout()
        INDlyItemInitialDateLeasing.HideLayout()
        INDlyItemEndDateLeasing.HideLayout()
        INDlyItemLocation.HideLayout()
        INDlyItemResponsible.HideLayout()
        CleanControlValues(True)

        INDlyEntry.EndUpdate()

        Me.BarraBotones.StatusRecordVisible = False
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True

        Me.FixedAssetEntry = New FixedAssetEntry()

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With FixedAssetEntry
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .EntryDate = EntryDate
            .EntryNumber = EntryNumber
            .CurrencyId = CurrencyId
            If TaxRegistration = 0 Then
                If companySettings IsNot Nothing Then
                    .TaxRegistration = companySettings.TaxRegistration
                End If
            Else
                .TaxRegistration = TaxRegistration
            End If
            .AdquisitionType = AdquisitionType
            If INDlyItemNumberContractLeasing.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .NumberContractLeasing = NumberContractLeasing
            Else
                .NumberContractLeasing = Nothing
            End If
            If INDlyItemInitialDateLeasing.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .InitialDateLeasing = InitialDateLeasing
            Else
                .InitialDateLeasing = Nothing
            End If
            If INDlyItemEndDateLeasing.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .EndDateLeasing = EndDateLeasing
            Else
                .EndDateLeasing = Nothing
            End If
            .SupplierId = SupplierId
            .SupplierDistributionLineId = SupplierDistributionLineId
            If INDlyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .CostCenterId = CostCenterId
            Else
                .CostCenterId = Nothing
            End If
            .SupplierTypeId = SupplierTypeId
            .Description = Description
            .GetLocationResponsible = GetLocationResponsible
            If INDlyItemLocation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .LocationId = LocationId
                .ResponsibleId = ResponsibleId

                ListFixedAssetEntryItem.ForEach(Sub(entryItem)
                                                    If entryItem.FixedAssetEntryItemDetail IsNot Nothing AndAlso entryItem.FixedAssetEntryItemDetail.Count > 0 Then
                                                        entryItem.FixedAssetEntryItemDetail.ToList.ForEach(Sub(entryItemDetail)
                                                                                                               entryItemDetail.ReponsibleId = ResponsibleId
                                                                                                               entryItemDetail.LocationId = LocationId
                                                                                                           End Sub)
                                                    End If
                                                End Sub)

            Else
                .LocationId = Nothing
                .ResponsibleId = Nothing
            End If
            .RoundService = RoundService
            .InvoiceNumber = InvoiceNumber
            .InvoiceDate = InvoiceDate
            .DocumentSupportId = INDSleDocumentSupportId.EditValue
            .DayPeriod = DayPeriod
            .IcaPercentage = IcaPercentage
            .FreightValue = FreightValue
            .FreightIVAPercentage = FreightIVAPercentage
            .FreightIVAValue = FreightIVAValue
            .Value = CType(INDPopTxtValue.EditValue, Decimal)
            .ValueDiscount = CType(INDPopTxtDiscountValue.EditValue, Decimal)
            .ValueTax = CType(INDPopTxtValueTax.EditValue, Decimal)
            .WithholdingTax = CType(INDPopTxtWithholdingTax.EditValue, Decimal)
            .WithholdingICA = CType(INDPopTxtWithholdingICA.EditValue, Decimal)
            .RetentionSource = CType(INDPopTxtRetentionSource.EditValue, Decimal)
            .RetentionOther = 0
            .DeductionOther = 0
            .TotalValue = TotalValue
            .OperatingUnitId = Me._idOperativeUnit
            .EconomicActivityId = EconomicActivity

            If ListFixedAssetEntryItem IsNot Nothing AndAlso ListFixedAssetEntryItem.Count > 0 Then
                .FixedAssetEntryItem.Clear()
                ListFixedAssetEntryItem.FindAll(Function(o) o.ChangeTracker.State = ObjectState.Added OrElse o.ChangeTracker.State = ObjectState.Modified OrElse (o.FixedAssetEntryItemDetail IsNot Nothing AndAlso o.FixedAssetEntryItemDetail.Any(Function(i) i.ChangeTracker.State = ObjectState.Modified))) _
                    .ForEach(Sub(item)
                                 .FixedAssetEntryItem.Add(item)
                             End Sub)
            End If

            If ListDeleteFixedAssetEntryItemDetailPartBook IsNot Nothing AndAlso ListDeleteFixedAssetEntryItemDetailPartBook.Any() Then
                For Each item In ListDeleteFixedAssetEntryItemDetailPartBook
                    item.FixedAssetEntryItemDetailPart = Nothing
                    item.LegalBook = Nothing
                Next
            End If
            If ListDeleteFixedAssetEntryItemDetailPart IsNot Nothing AndAlso ListDeleteFixedAssetEntryItemDetailPart.Any() Then
                For Each item In ListDeleteFixedAssetEntryItemDetailPart
                    item.FixedAssetEntryItemDetail = Nothing
                    item.FixedAssetPartsAccesoriesConsumables = Nothing
                Next
            End If
            If ListDeleteFixedAssetEntryItemDetailBook IsNot Nothing AndAlso ListDeleteFixedAssetEntryItemDetailBook.Any() Then
                For Each item In ListDeleteFixedAssetEntryItemDetailBook
                    item.FixedAssetEntryItemDetail = Nothing
                    item.LegalBook = Nothing
                Next
            End If
            If ListDeleteFixedAssetEntryItemDetail IsNot Nothing AndAlso ListDeleteFixedAssetEntryItemDetail.Any() Then
                For Each item In ListDeleteFixedAssetEntryItemDetail
                    item.FixedAssetEntryItem = Nothing
                    item.FixedAssetLocation = Nothing
                    item.FixedAssetResponsible = Nothing
                    item.FixedAssetStatusAsset = Nothing
                    If item.FixedAssetEntryDevolutionDetail.Any() Then
                        For Each it In item.FixedAssetEntryDevolutionDetail
                            it.MarkAsDeleted()
                        Next
                    End If
                    If item.FixedAssetEntryItemDetailBook.Any() Then
                        For Each it In item.FixedAssetEntryItemDetailBook
                            it.MarkAsDeleted()
                        Next
                    End If
                    If item.FixedAssetEntryItemDetailPart.Any() Then
                        For Each it In item.FixedAssetEntryItemDetailPart
                            it.MarkAsDeleted()
                        Next
                    End If
                Next
            End If
            If ListDeleteFixedAssetEntryItem IsNot Nothing AndAlso ListDeleteFixedAssetEntryItem.Any() Then
                For Each item In ListDeleteFixedAssetEntryItem
                    item.FixedAssetEntry = Nothing
                    item.FixedAssetItem = Nothing
                    item.FixedAssetPolicy = Nothing
                    item.FixedAssetPurchaseOrderItem = Nothing
                    item.FixedAssetRemissionEntranceItem = Nothing
                    item.FixedAssetTrademark = Nothing
                    item.GeneralLedgerIVA = Nothing
                    If item.FixedAssetEntryDevolutionDetail.Any() Then
                        For Each it In item.FixedAssetEntryDevolutionDetail
                            it.MarkAsDeleted()
                        Next
                    End If
                    If item.FixedAssetEntryItemDetail.Any() Then
                        For Each it In item.FixedAssetEntryItemDetail
                            it.MarkAsDeleted()
                        Next
                    End If
                Next
            End If

            If INDlygBudget.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If ListFixedAssetEntryCommitment IsNot Nothing AndAlso ListFixedAssetEntryCommitment.Count > 0 Then
                    ListFixedAssetEntryCommitment.ForEach(Sub(item) .FixedAssetEntryCommitment.Add(item))
                End If
            Else
                If ListFixedAssetEntryCommitment IsNot Nothing AndAlso ListFixedAssetEntryCommitment.Count > 0 Then
                    ListFixedAssetEntryCommitment.ForEach(Sub(item)
                                                              If item.Id > 0 Then
                                                                  .FixedAssetEntryCommitment.Add(item.MarkAsDeleted())
                                                              End If
                                                          End Sub)
                End If
            End If

            If ListDeleteFixedAssetEntryCommitment IsNot Nothing AndAlso ListDeleteFixedAssetEntryCommitment.Count > 0 Then
                ListDeleteFixedAssetEntryCommitment.ForEach(Sub(item) .FixedAssetEntryCommitment.Add(item))
            End If

            .ChangeTracker.ObjectsRemovedFromCollectionProperties.Clear()
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
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MFixedAssetEntry(CStr(Me.Tag))
                    AsyncLoader(True)
                    Dim tmpFixedAssetEntry = (Await Model.GetFixedAssetEntry(INDbtnCode.Text.Trim)).ObjectEmbbeded

                    INDlyEntry.BeginUpdate()
                    If tmpFixedAssetEntry IsNot Nothing AndAlso tmpFixedAssetEntry.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        'Cargar unidad operativa
                        Await ChangeOperatingUnitAsync(New OperatingUnit With {.Id = tmpFixedAssetEntry.OperatingUnitId})

                        FixedAssetEntry = tmpFixedAssetEntry
                        Await ChangeOperatingUnitAsync(New OperatingUnit With {.Id = Me.BarraBotones.OperatingUnit.Id})

                        If Me.SettingsFixedAsset Is Nothing Then
                            Mensaje(EeventViewerImages.Advertencia) = "No existe parámetros de activo fijo para la unidad operativa escogida"
                            Exit Function
                        End If

                        Using ModelRecord As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(FixedAssetEntry.Id))
                            FlagLoad = True

                            With FixedAssetEntry
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                Using modelSupplier As New MSupplier(MyTag)
                                    _supplier = modelSupplier.GetSupplierById(.SupplierId)
                                End Using
                                _suppliersDistibutionLine = Presenter.GetSupplierDistributionLineById(.SupplierDistributionLineId)

                                'La fecha del ingreso tiene que estar en el mismo mes de la fecha de parametros y no puede ser mayor a la fecha actual
                                'Solo si se encuentra en estado registrado
                                Dim dateMin As DateTime = Convert.ToDateTime(SettingsFixedAsset.ProcessDate.Year.ToString + "/" + SettingsFixedAsset.ProcessDate.Month.ToString + "/01")
                                INDdteEntryDate.Properties.MinValue = If(.Status = 1, dateMin, Nothing)
                                INDdteEntryDate.Properties.MaxValue = If(.Status = 1, GetDateServer(), Nothing)

                                Code = .Code
                                SupplierId = .SupplierId
                                SupplierDistributionLineId = .SupplierDistributionLineId
                                INDsleSupplierDistributionLine.Properties.NullText = .SupplierCodeName
                                If .CostCenterId IsNot Nothing Then
                                    INDlyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                                    INDlyItemCostCenter.AllowHide = False
                                    CostCenterId = .CostCenterId
                                    INDsleCostCenter.Properties.NullText = .CostCenterCodeName
                                End If
                                If .EconomicActivityId IsNot Nothing Then
                                    EconomicActivity = .EconomicActivityId
                                End If
                                SupplierTypeId = .SupplierTypeId
                                EntryDate = .EntryDate
                                EntryNumber = .EntryNumber
                                CurrencyId(.Currency?.Abbreviation) = .CurrencyId
                                TaxRegistration = .TaxRegistration
                                Description = .Description
                                InvoiceNumber = .InvoiceNumber
                                InvoiceDate = .InvoiceDate
                                DayPeriod = .DayPeriod
                                FreightValue = .FreightValue
                                FreightIVAValue = .FreightIVAValue
                                FreightIVAPercentage = .FreightIVAPercentage
                                IcaPercentage = .IcaPercentage
                                AdquisitionType = .AdquisitionType
                                NumberContractLeasing = .NumberContractLeasing
                                InitialDateLeasing = .InitialDateLeasing
                                EndDateLeasing = .EndDateLeasing
                                RoundService = .RoundService
                                GetLocationResponsible = .GetLocationResponsible
                                LocationId = .LocationId
                                ResponsibleId = .ResponsibleId
                                INDsleResponsible.Properties.NullText = .ResponsibleCodeName
                                BarraBotones.StatusRecord = .Status.ToString

                                If FixedAssetEntry.Status = 1 Then
                                    INDSleHandleDocumentSupport.EditValue = (_supplier.ThirdParty.ContributionType = 0 AndAlso FixedAssetEntry.DocumentSupportId IsNot Nothing)
                                    INDLyItemHandleDocumentSupport.HideControl(Not (_supplier.ThirdParty.ContributionType = 0))
                                    INDSleDocumentSupportId.EditValue = If(_supplier.ThirdParty.ContributionType = 0, FixedAssetEntry.DocumentSupportId, Nothing)
                                    INDSleDocumentSupportId.Properties.NullText = If(_supplier.ThirdParty.ContributionType = 0, FixedAssetEntry.DescriptionDocumentSupport, Nothing)
                                Else
                                    INDSleHandleDocumentSupport.EditValue = If(FixedAssetEntry.DocumentSupportId Is Nothing, False, True)
                                    INDSleDocumentSupportId.EditValue = FixedAssetEntry.DocumentSupportId
                                    INDSleDocumentSupportId.Properties.NullText = FixedAssetEntry.DescriptionDocumentSupport
                                End If

                                Await LoadIcaPercentage(_supplier, .SupplierDistributionLineId, .OperatingUnitId)
                                ListFixedAssetEntryItem = .FixedAssetEntryItem.ToList
                                INDgcItem.DataSource = Nothing
                                INDgcItem.DataSource = ListFixedAssetEntryItem

                                ListFixedAssetEntryCommitment = .FixedAssetEntryCommitment.ToList()
                                INDgcCommitment.DataSource = Nothing
                                INDgcCommitment.DataSource = ListFixedAssetEntryCommitment

                                INDPopTxtValue.EditValue = .Value
                                INDPopTxtDiscountValue.EditValue = .ValueDiscount
                                INDPopTxtValueTax.EditValue = .ValueTax
                                INDPopTxtWithholdingTax.EditValue = .WithholdingTax
                                INDPopTxtWithholdingICA.EditValue = .WithholdingICA
                                INDPopTxtRetentionSource.EditValue = .RetentionSource

                                SubTotal = ListFixedAssetEntryItem.Sum(Function(item) item.SubTotalValue)
                                DiscountValue = ListFixedAssetEntryItem.Sum(Function(item) item.DiscountValue)
                                NetoValue = SubTotal - DiscountValue
                                IvaValue = ListFixedAssetEntryItem.Sum(Function(item) item.IvaValue)
                                TotalValue = .TotalValue
                                INDPopTxtValue.EditValue = SubTotal
                                INDPopTxtDiscountValue.EditValue = DiscountValue
                                INDPopTxtValueTax.EditValue = IvaValue
                                INDPopTxtFreightValue.EditValue = FreightValue
                                INDPopSpnFreightIVA.EditValue = FreightIVAValue

                                CalculateRetentions()
                            End With
                            If companySettings.TaxRegistration = 3 AndAlso (FixedAssetEntry?.TaxRegistration = 0 Or FixedAssetEntry?.TaxRegistration = 3) AndAlso FixedAssetEntry.Status = 1 Then
                                Mensaje(EeventViewerImages.Advertencia) = "El campo Registro Iva esta vacio"
                            End If
                            'Llenar NullText
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.FixedAssetEntry.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordFixedAsset With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = FixedAssetEntry.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            Me.BarraBotones.SetDocuments(FixedAssetEntry.Id, Me.Tag.ToString(), Nothing, GetType(FixedAssetEntry).Name)
                            ActionsOnControls = True
                            If FixedAssetEntry.Status = 1 Then
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
                            Else
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
                                ReadOnlyControls(True)
                                ActionsOnControlsPopupDetails = True
                                For iColumns = 0 To INDviewItem.Columns.Count - 1
                                    If INDviewItem.Columns(iColumns).Name = "INDcolDetails" Then
                                        INDviewItem.Columns(iColumns).OptionsColumn.AllowEdit = True
                                    End If
                                Next
                            End If
                            If FixedAssetEntry.Status = 2 Then 'Validamos que el ingreso del activo este confirmado para bloquear los controles
                                ActionsOnControls = False
                                INDgcItem.Enabled = True
                            End If
                            FlagLoad = False
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, FixedAssetEntry.Id, 0, FixedAssetEntry.Id, _idOperativeUnit)
                        End Using
                    Else
                        If Me._sequence.IsManual Then
                            Await Me.NewEntry()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbtnCode.Focus()
                        End If
                    End If
                    INDlyEntry.EndUpdate()
                End Using
            Catch ex As Exception
                INDbtnCode.Enabled = False
                Throw ex
            Finally
                AsyncLoader(False)
            End Try
        End If
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewEntry() As Task
        If Me._idOperativeUnit = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una unidad operativa"
            Exit Function
        End If
        If Me.SettingsFixedAsset Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "No existe parámetros de activo fijo para la unidad operativa escogida"
            Exit Function
        End If
        AsyncLoader(True)

        'La fecha del ingreso tiene que estar en el mismo mes de la fecha de parametros y no puede ser mayor a la fecha actual
        Dim dateMin As DateTime = Convert.ToDateTime(SettingsFixedAsset.ProcessDate.Year.ToString + "/" + SettingsFixedAsset.ProcessDate.Month.ToString + "/01")
        INDdteEntryDate.Properties.MinValue = dateMin
        INDdteEntryDate.Properties.MaxValue = GetDateServer()

        EntryDate = GetDateServer()
        InvoiceDate = GetDateServer()
        AsyncLoader(False)
        Me.FixedAssetEntry = New FixedAssetEntry() With {.Status = 1}
        Me.BarraBotones.StatusRecordVisible = True
        Me.BarraBotones.StatusRecord = "1"
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

    Private Function LoadReport() As Task
        Return Task.Factory.StartNew(Sub()
                                         Me.BarraBotones.SafeInvoke(Sub(x) x.PrintReport(PrintReportAction.None, FixedAssetEntry.Id, 0, FixedAssetEntry.Id, _idOperativeUnit))
                                     End Sub)
    End Function

    ''' <summary>
    ''' Importa la información a la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ImportInfo()
        If SupplierDistributionLineId Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un proveedor."
            Exit Sub
        End If
        Using formulario As New FrmImports(_currency:=New Currency With {.Id = Me.CurrencyId, .Abbreviation = Me.CurrencyAbbreviation})
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddImportsInfoEventArgs, AddressOf ReturnImportInfo
            formulario.ToolBars.Visible = False
            formulario.Size = New Drawing.Size(604, 535)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.SupplierDistributionLineId = SupplierDistributionLineId
            formulario.EntryDate = EntryDate
            formulario.GetLocationResponsible = GetLocationResponsible
            formulario.ListCompare = ListFixedAssetEntryItem
            formulario.AdquisitionType = AdquisitionType
            formulario.Declarant = _supplier.Declarant
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Retorno del evento de importar información
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnImportInfo(sender As Object, e As AddImportsInfo)
        If e IsNot Nothing Then
            If ListFixedAssetEntryItem Is Nothing Then
                ListFixedAssetEntryItem = New List(Of FixedAssetEntryItem)
            End If
            ListFixedAssetEntryItem.AddRange(e.ListFixedAssetEntryItem)

            Me.CalculateRetentions()
            INDgcItem.DataSource = Nothing
            INDgcItem.DataSource = ListFixedAssetEntryItem

            INDviewGridCommitment.ShowLoadingPanel()
            Task.Factory.StartNew(Sub() LoadDatasourceCommitments(e.ListFixedAssetEntryItem))
        End If
    End Sub

    ''' <summary>
    ''' Carga la rejilla de presupuesto con la información que viene desde el importar
    ''' </summary>
    Public Sub LoadDatasourceCommitments(list As List(Of FixedAssetEntryItem))
        CheckForIllegalCrossThreadCalls = False

        If list IsNot Nothing AndAlso list.Count > 0 Then
            Dim listXpo = Presenter.GetListCommitmentBySourceCodeAndEntityName(list)

            If listXpo IsNot Nothing AndAlso listXpo.Count > 0 Then
                If ListFixedAssetEntryCommitment Is Nothing Then
                    ListFixedAssetEntryCommitment = New List(Of FixedAssetEntryCommitment)
                End If

                For Each itemXpo In listXpo
                    If (From x In ListFixedAssetEntryCommitment Where x.CommitmentDetailId = itemXpo.Id Select x).Count > 0 Then
                        Continue For
                    End If

                    Dim entity As New FixedAssetEntryCommitment
                    entity.CommitmentDetailId = itemXpo.Id
                    entity.Value = 0
                    entity.CommitmentCode = itemXpo.CommitmentId.Code
                    entity.CommitmentDocument = itemXpo.CommitmentId.Document
                    entity.CategoryCodeName = itemXpo.CategoryId.NameCode
                    entity.FinancialSourceCodeName = ""
                    If itemXpo.CategoryId.FinancialSourceId IsNot Nothing Then
                        entity.FinancialSourceCodeName = itemXpo.CategoryId.FinancialSourceId.NameCode
                    End If
                    entity.RevenueTypeCodeName = itemXpo.RevenueTypeId.NameCode
                    entity.Balance = itemXpo.Balance

                    ListFixedAssetEntryCommitment.Add(entity)
                Next
                INDgcCommitment.DataSource = Nothing
                INDgcCommitment.DataSource = ListFixedAssetEntryCommitment
                INDgcCommitment.RefreshDataSource()
            End If
        End If

        INDviewGridCommitment.HideLoadingPanel()
    End Sub

    ''' <summary>
    ''' Metodo que oculta o muestra el boton de importar información
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub HideImportInfo()
        If SupplierDistributionLineId IsNot Nothing AndAlso EntryDate IsNot Nothing AndAlso GetLocationResponsible IsNot Nothing Then
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
        Else
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
        End If
    End Sub

    ''' <summary>
    ''' Carga el tercero de la empresa actualmente seleccionada
    ''' </summary>
    ''' <returns>Valor que indica si la empresa se cargo</returns>
    Private Async Function LoadCurrentCompany() As Task
        Using model As New MFixedAssetEntry(MyTag)
            Me._currentCompany = Await model.GetThirdPartyAsync(Me.indigo.IndigoCompanyNit)

            If Me._currentCompany Is Nothing Then 'Si la empresa actual no existe como tercero, se debe crear
                Me.Mensaje(EeventViewerImages.Advertencia) = "No se ha podido cargar el tercero de la empresa (" & Me.indigo.IndigoCompanyNit & ") actualmente seleccionada. Verifique que haya sido creada e intente de nuevo."
                Exit Function
            End If
        End Using

        Using model As New Presentation.Payments.MVP.MParameters(MyTag)
            Dim _parameter = Await model.GetSettingPaymentsByIdOperatingUnit(_idOperativeUnit)
            If _parameter IsNot Nothing AndAlso _parameter.ObjectEmbbeded IsNot Nothing AndAlso _parameter.StateResult Then
                IsVisibleGroupBudget = _parameter.ObjectEmbbeded.BudgetInterface
            End If
        End Using
    End Function

    Private Async Function LoadIcaPercentage(ByVal supplier As Supplier, ByVal supplierDistributionLineId As Integer, ByVal operatingUnitId As Integer) As Task
        If Me.FixedAssetEntry IsNot Nothing AndAlso Me.FixedAssetEntry.Status = 1 Then
            IcaPercentage = 0
            If supplier.ThirdParty.RetentionType = 2 AndAlso supplier.ThirdParty.Ica AndAlso Not supplier.SelfWithholdingICA Then
                Using Model As New MFixedAssetEntry(Me.MyTag)
                    _icaRetentionConcept = Await Model.GetICARetentionConceptBySupplierDistributionLine(supplierDistributionLineId, operatingUnitId)
                    If _icaRetentionConcept IsNot Nothing Then
                        IcaPercentage = _icaRetentionConcept.Rate
                    Else
                        IcaPercentage = supplier.ThirdParty.IcaPercentage
                    End If
                End Using
            End If
        End If
    End Function

    ''' <summary>
    ''' Método que muestra/oculta el control de compromiso
    ''' </summary>
    Private Sub ShowHideControlCommitment()
        'Se obtiene los parámetros de pagos por unidad operativa
        If BarraBotones.OperatingUnitValue <> Nothing AndAlso BarraBotones.OperatingUnitValue > 0 Then
            PaymentsSettingPaymentsXpo = Presenter.GetSettingsPaymentsByOperatingUnitId(BarraBotones.OperatingUnitValue)
        End If
    End Sub

    ''' <summary>
    ''' Calcular las retenciones
    ''' </summary>
    Private Sub CalculateRetentions()
        If Me.FixedAssetEntry Is Nothing OrElse Me.FixedAssetEntry.Status <> 1 Then
            CalculateTotalValue()
            Exit Sub
        End If

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
        If ListFixedAssetEntryItem Is Nothing Then
            Exit Sub
        End If

        'IVA
        For Each item In ListFixedAssetEntryItem
            item.IvaValue = 0
            Dim totalValue = item.SubTotalValue - item.DiscountValue
            If _supplier IsNot Nothing AndAlso Not _supplier.NotIva Then
                If (AdquisitionType = 1 OrElse AdquisitionType = 7 OrElse AdquisitionType = 9) Then
                    item.IvaValue = Math.Round((totalValue * item.IvaPercentage / 100), 2, MidpointRounding.AwayFromZero)
                End If
            End If
            item.TotalValue = totalValue + item.IvaValue
        Next

        SubTotal = ListFixedAssetEntryItem.Sum(Function(item) item.SubTotalValue)
        DiscountValue = ListFixedAssetEntryItem.Sum(Function(item) item.DiscountValue)
        NetoValue = ListFixedAssetEntryItem.Sum(Function(item) item.SubTotalValue - item.DiscountValue)
        IvaValue = Utils.RoundValue(ListFixedAssetEntryItem.Sum(Function(item) item.IvaValue), 6)
        TotalValue = ListFixedAssetEntryItem.Sum(Function(item) item.TotalValue)

        INDPopTxtValue.EditValue = SubTotal
        INDPopTxtDiscountValue.EditValue = DiscountValue
        INDPopTxtValueTax.EditValue = IvaValue

        ' Si es una compra directa, o un leasing o renting financiero
        If (AdquisitionType = 1 OrElse AdquisitionType = 7 OrElse AdquisitionType = 9) Then
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
                        For Each item In ListFixedAssetEntryItem

                            item.RTFPercentage = 0
                            item.RTFValue = 0
                            If _supplier.PermanentRetention OrElse (NetoValue >= item.MinBase) Then
                                item.RTFPercentage = item.Rate
                                item.RTFValue = Utils.RoundValue(((item.SubTotalValue - item.DiscountValue) * item.Rate / 100), item.TypeRounding)
                                INDPopTxtRetentionSource.EditValue = INDPopTxtRetentionSource.EditValue + item.RTFValue
                            End If
                        Next
                    End If

                    'ReteIca
                    If _supplier.ThirdParty.Ica AndAlso Not _supplier.SelfWithholdingICA Then
                        If IcaPercentage > 0 Then
                            Dim minBase = If(_supplier.ThirdParty.IcaTop, _supplier.ThirdParty.IcaTopValue, If(_icaRetentionConcept IsNot Nothing, _icaRetentionConcept.MinBase, 0))
                            Dim typeRounding = If(_icaRetentionConcept IsNot Nothing, _icaRetentionConcept.TypeRounding, 1)
                            If (NetoValue >= minBase) Then
                                INDPopTxtWithholdingICA.EditValue = Utils.RoundValue((NetoValue * IcaPercentage / 100), typeRounding)
                            End If
                        End If
                    End If

                    'ReteIva
                    If IvaValue > 0 Then
                        If _supplier.ThirdParty.ContributionType > 0 AndAlso _currentCompany?.ContributionType > _supplier.ThirdParty.ContributionType Then
                            If SettingsFixedAsset.IVARetention = 1 Then
                                'La retención se saca del tercero
                                If _supplier.ThirdParty.IVARetentionConceptId Is Nothing Then
                                    errors.AppendLine("El tercero no tiene parametrizado un concepto de retención de IVA")
                                Else
                                    retentionConceptId = _supplier.ThirdParty.IVARetentionConceptId
                                End If
                            Else
                                'La retención se saca del concepto de los parámetros
                                If Not dictionaryAccountPayableConcepts.ContainsKey(SettingsFixedAsset.IVARetentionAccountPayableConceptId) Then
                                    Dim accountPayableConceptXpo = Presenter.GetAccountPayableConceptById(SettingsFixedAsset.IVARetentionAccountPayableConceptId)
                                    retentionConceptId = accountPayableConceptXpo.RetentionConceptId
                                    dictionaryAccountPayableConcepts.Add(SettingsFixedAsset.IVARetentionAccountPayableConceptId, retentionConceptId)
                                Else
                                    retentionConceptId = dictionaryAccountPayableConcepts(SettingsFixedAsset.IVARetentionAccountPayableConceptId)
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
    End Sub

    ''' <summary>
    ''' Establece el formato moneda en los controles del formulario
    ''' </summary>
    ''' <param name="_currencyAbbreviation"></param>
    Private Sub SetCurrencyUI(_currencyAbbreviation As String)

        If String.IsNullOrEmpty(_currencyAbbreviation) Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CurrencyAbbreviationEmpty")
            Exit Sub
        End If

        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = _currencyAbbreviation.GetNumberFormat

        Me.INDtxtFreightValue.Properties.Mask.Culture = _culture
        Me.INDtxtFreightIVAValue.Properties.Mask.Culture = _culture

        Me.INDcolUnitValue = Window.Utils.FormatGrid(INDcolUnitValue, _currencyAbbreviation)
        Me.INDcolSubtotal = Window.Utils.FormatGrid(INDcolSubtotal, _currencyAbbreviation)
        Me.GridColumn201 = Window.Utils.FormatGrid(GridColumn201, _currencyAbbreviation)
        Me.GridColumn211 = Window.Utils.FormatGrid(GridColumn211, _currencyAbbreviation)
        Me.ctrTmp.CodeISO4217 = _currencyAbbreviation
        ctrTmp.PrintInfo()
    End Sub

    ''' <summary>
    ''' funcion Valida y establece el evento cuando la moneda cambia 
    ''' </summary>
    ''' <param name="IsLoadControl"></param>
    ''' <returns></returns>
    Private Async Function ValidateCurrencyEditValue(IsLoadControl As Boolean, _currencyId As Integer?) As Task(Of ActionResult)
        If _currencyId Is Nothing OrElse _currencyId = 0 OrElse IsLoadControl OrElse {2, 3}.Contains(Me.Status) Then
            Return New ActionResult With {.StateResult = True}
        End If

        If Me.CurrencyId IsNot Nothing Then
            Me.SetCurrencyUI(Me.CurrencyAbbreviation)
        End If

        If _currencyId = SettingsFixedAsset?.CurrencyId Then
            Me.TRM = New TRM With {.CurrencyId = _currencyId, .OfficialCurrencyId = SettingsFixedAsset?.CurrencyId, .Value = 1}
            Return New ActionResult With {.StateResult = True}
        End If

        Dim _stateResult = Await GetTRM(SettingsFixedAsset?.CurrencyId, _currencyId)

        If _stateResult Then
            Return New ActionResult With {.StateResult = True}
        End If

        If Not FlagLoad Then
            Me.CurrencyId(SettingsFixedAsset?.Currency?.Abbreviation) = SettingsFixedAsset?.CurrencyId
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
        Using Model As New MInventoryContract("")
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

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        SubTotal = Nothing
        DiscountValue = Nothing
        NetoValue = Nothing
        IvaValue = Nothing
        TotalValue = Nothing
        ctrTmp = Nothing
        FixedAssetEntry = Nothing
        Presenter = Nothing
        record = Nothing
        _idOperativeUnit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        ListAdquisitionType = Nothing
        ListRoundService = Nothing
        ListGetLocationResponsible = Nothing
        ListFixedAssetEntryItem = Nothing
        FixedAssetEntryItem = Nothing
        IndexEditRecord = Nothing
        ListDeleteFixedAssetEntryItemDetailPartBook = Nothing
        ListDeleteFixedAssetEntryItemDetailPart = Nothing
        ListDeleteFixedAssetEntryItemDetailBook = Nothing
        ListDeleteFixedAssetEntryItemDetail = Nothing
        ListDeleteFixedAssetEntryItem = Nothing
        FlagLoad = Nothing
        ListTuple = Nothing
        varImp = Nothing
        _currentCompany = Nothing
        _supplier = Nothing
        _suppliersDistibutionLine = Nothing
    End Sub

    ''' <summary>
    ''' Metodo que se encarga de setear las acciones disponibles de las rejillas
    ''' </summary>
    Private Sub SetActions()
        IndigoGridView1.SetListAcction(INDviewItem, {eAcciones.Remove, eAcciones.Edit}.ToList())
        IndigoGridControl1.RefreshGrid(INDgcItem)
        IndigoGridView2.SetListAcction(INDviewGridCommitment, {eAcciones.Remove}.ToList())
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDviewGridCommitment.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmFixedAssetEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.LayoutControls.SetIsCustomizable(Me.INDlyEntry, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue

        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PFixedAssetEntry(Me)
        Presenter.LoadDefinitionLayout()
        Await Presenter.GetSettingFixedAssetByOperatingUnitId(_idOperativeUnit)

        Try
            AsyncLoader(True)
            Await Presenter.GetSequense()
            Await LoadCurrentCompany()

            Using modelCompanySettings As New MCompanySettings(MyTag)
                companySettings = Await modelCompanySettings.GetCompanySettings()
            End Using

            CurrencyId(SettingsFixedAsset?.Currency?.Abbreviation) = SettingsFixedAsset?.CurrencyId
            If companySettings?.TransactionEconomicActivity Then
                INDliEconomicActivity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If

            AsyncLoader(False)
        Catch ex As Exception
            Me.Mensaje(EeventViewerImages.Advertencia) = ex.Message
        End Try

        Deshacer()
        If Me.indigo.Culture.Name = "es-CR" Then
            INDlyItemIcaPercentage.HideLayout
        Else
            INDlyItemIcaPercentage.ShowLayout
        End If

        SetActions()
        LoadStatus()
        InitializeTuple()
        PrintTaxRegistrationField()

        INDdteInvoiceDate.Properties.MaxValue = GetDateServer()
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmFixedAssetEntry_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se ejecuta al presionar enter o f4 sobre el control de presupuesto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDpceCommitment_Properties_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDpceCommitment.Properties.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.F4 OrElse e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDpceCommitment.ShowPopup()
        End If
    End Sub

    ''' <summary>
    ''' Evento para consultar un concepto de notas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnCode.KeyDown
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
                    Await Me.NewEntry()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmFixedAssetEntry_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDbtnCode.Focus()
        Presenter.InitializeLocation()

        'Lógica del control de compromiso
        ShowHideControlCommitment()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se ejecuta al presionar click sobre el boton de agrega presupuesto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnAddCommitment_Click(sender As Object, e As EventArgs) Handles INDbtnAddCommitment.Click
        If ValidateControlsPopup() = False Then
            Exit Sub
        End If

        If ListFixedAssetEntryCommitment Is Nothing Then
            ListFixedAssetEntryCommitment = New List(Of FixedAssetEntryCommitment)
        End If

        If (From x In ListFixedAssetEntryCommitment Where x.CommitmentDetailId = INDsleCommitmentDetail.EditValue Select x).Count > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "El compromiso seleccionado ya existe en la lista"
            Exit Sub
        End If

        Dim entityXpo As ViewListCommitmentDetailXpo = INDviewSearchCommitment.GetFocusedRow()
        Dim fixedAssetEntryCommitment As New FixedAssetEntryCommitment
        With fixedAssetEntryCommitment
            .CommitmentDetailId = entityXpo.CommitmentDetailId
            .Value = 0
            .CommitmentCode = entityXpo.CommitmentCode
            .CommitmentDocument = entityXpo.Document
            .CategoryCodeName = entityXpo.CategoryCodeName
            .FinancialSourceCodeName = entityXpo.FinancialSourceCodeName
            .RevenueTypeCodeName = entityXpo.RevenueTypeCodeName
            .Balance = entityXpo.Balance
        End With

        ListFixedAssetEntryCommitment.Add(fixedAssetEntryCommitment)
        INDgcCommitment.DataSource = Nothing
        INDgcCommitment.DataSource = ListFixedAssetEntryCommitment
        INDsleCommitmentDetail.EditValue = Nothing
        INDsleCommitmentDetail.Focus()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddItem_Click(sender As Object, e As EventArgs) Handles INDbtnAddItem.Click
        FixedAssetEntryItem = Nothing
        OpenFormFixedAssetEntryItem(False)
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

    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs)
        DeleteCommitment()
    End Sub

    Private Sub IndigoGridView2_ContexMenuActions(sender As Object, e As EventArgs)
        DeleteCommitment()
    End Sub

#End Region

#Region "ButtonClick"

    Private Sub INDsleSupplierDistributionLine_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleSupplierDistributionLine.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(558, Nothing, True)
            Presenter.InitializeSupplierDistributionLine()
        End If
    End Sub

    Private Async Sub INDsleSupplierType_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleSupplierType.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(726, Nothing, True)
            If SupplierId > 0 Then
                Await InitializeSupplierType()
            End If
        End If
    End Sub

    Private Sub INDsleResponsible_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleResponsible.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1711, Nothing, True)
            Presenter.InitializeResponsible()
        End If
    End Sub

    Private Sub INDsleCostCenter_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCostCenter.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(517, Nothing, True)
            Presenter.InitializeCostCenter()
        End If
    End Sub
    ''' <summary>
    ''' Metodo para abrir el frm de actividades economicas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleEconomicActivity_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleEconomicActivity.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1845, Nothing, True)
            Presenter.InitializeEconomicActivity()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de presupuesto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDpceCommitment_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDpceCommitment.QueryPopUp
        INDsleBudgetaryEntity.Focus()
    End Sub

    ''' <summary>
    ''' Carga el datasource de la entidad presupuestal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleBudgetaryEntity_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleBudgetaryEntity.QueryPopUp
        If BudgetaryEntityXpo Is Nothing Then
            Presenter.InitializeBudgetaryEntity()
        End If
    End Sub

    ''' <summary>
    ''' Carga el datasource de la vigencia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleBudgetaryValidity_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleBudgetaryValidity.QueryPopUp
        If BudgetaryValidityXpo Is Nothing AndAlso INDsleBudgetaryEntity.EditValue IsNot Nothing Then
            Presenter.InitializeBudgetaryValidity(INDsleBudgetaryEntity.EditValue)
        End If
    End Sub

    ''' <summary>
    ''' Carga el datasource del compromiso
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCommitmentDetail_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCommitmentDetail.QueryPopUp
        If CommitmentDetailXpo Is Nothing AndAlso _supplier.ThirdParty IsNot Nothing AndAlso _supplier.ThirdParty.Id > 0 AndAlso INDsleBudgetaryValidity.EditValue IsNot Nothing Then
            Presenter.InitializeCommitmentDetail(_supplier.ThirdParty.Id, INDsleBudgetaryValidity.EditValue)
        End If
    End Sub

    Private Sub INDrepPceDetails_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDrepPceDetails.QueryPopUp
        Dim info As FixedAssetEntryItem = CType(INDviewItem.GetFocusedRow, FixedAssetEntryItem)
        If info.FixedAssetEntryItemDetail.Count > 0 Then
            INDgcDetails.DataSource = Nothing
            INDgcDetails.DataSource = info.FixedAssetEntryItemDetail.ToList
        End If
    End Sub

    Private Sub INDsleSupplierDistributionLine_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleSupplierDistributionLine.QueryPopUp
        If SupplierDistributionLineXpo Is Nothing Then
            Presenter.InitializeSupplierDistributionLine()
        End If
    End Sub

    Private Sub INDsleResponsible_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleResponsible.QueryPopUp
        If ResponsibleXpo Is Nothing Then
            Presenter.InitializeResponsible()
        End If
    End Sub

    Private Sub INDsleCostCenter_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCostCenter.QueryPopUp
        If CostCenterXpo Is Nothing Then
            Presenter.InitializeCostCenter()
        End If
    End Sub


    ''' <summary>
    ''' Evento que se dispara al desplegar documento soporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleResolutionDocumentSupport_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleDocumentSupportId.QueryPopUp
        Presenter.InitializeDocumentSupport()
    End Sub

    ''' <summary>
    ''' Evento para consultar el maestro de moneda
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCurrency_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCurrency.QueryPopUp
        If INDsleCurrency.Properties.DataSource Is Nothing Then
            Using Model As New MCompanySettings("")
                INDsleCurrency.Properties.DataSource = Model.GetCurrencyDatasource()
            End Using
        End If
    End Sub
    ''' <summary>
    ''' evento cuando se le da click al pop up de actividad economica
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleEconomicActivity_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleEconomicActivity.QueryPopUp
        If EconomicActivityDatasource Is Nothing Then
            Presenter.InitializeEconomicActivity()
        End If
    End Sub
#End Region

#Region "EditValueChanged"

    Private Sub INDdteInitialDateLeasing_EditValueChanged(sender As Object, e As EventArgs) Handles INDdteInitialDateLeasing.EditValueChanged
        If InitialDateLeasing IsNot Nothing Then
            INDdteEndDateLeasing.Properties.MinValue = InitialDateLeasing
        End If
    End Sub

    Private Sub INDsleAdquisitionType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleAdquisitionType.EditValueChanged
        If AdquisitionType IsNot Nothing Then
            If AdquisitionType = 7 OrElse AdquisitionType = 9 Then 'Leasing Financiero o Renting Financiero
                INDlyItemNumberContractLeasing.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemNumberContractLeasing.AllowHide = False
                INDlyItemInitialDateLeasing.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemInitialDateLeasing.AllowHide = False
                INDlyItemEndDateLeasing.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemEndDateLeasing.AllowHide = False
            Else
                INDlyItemNumberContractLeasing.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemNumberContractLeasing.AllowHide = True
                INDlyItemInitialDateLeasing.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemInitialDateLeasing.AllowHide = True
                INDlyItemEndDateLeasing.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemEndDateLeasing.AllowHide = True
            End If

            'Si en el parametro de cxp esta activo presupuesto y además el tipo de adquisición es compra directa, leasing financiaero, renting financiero
            If IsVisibleGroupBudget AndAlso (AdquisitionType = 1 OrElse AdquisitionType = 7 OrElse AdquisitionType = 9) Then
                INDlygBudget.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                INDlygBudget.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        End If
    End Sub

    Private Async Sub INDsleSupplierDistributionLine_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleSupplierDistributionLine.EditValueChanged
        If SupplierDistributionLineId IsNot Nothing Then
            If Not FlagLoad Then
                If viewSupplier.DataSource Is Nothing Then
                    _suppliersDistibutionLine = Presenter.GetSupplierDistributionLineById(SupplierDistributionLineId)
                Else
                    _suppliersDistibutionLine = DirectCast(DirectCast(viewSupplier.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.CommonRepository.CommonSuppliersDistibutionLineXpo)
                End If

                SupplierId = _suppliersDistibutionLine.IdSupplier.Id
                Using modelSupplier As New MSupplier(MyTag)
                    _supplier = modelSupplier.GetSupplierById(SupplierId)
                    DayPeriod = _supplier.TimeLimitDays
                End Using

                INDSleHandleDocumentSupport.EditValue = (_supplier.ThirdParty.ContributionType = 0)
                INDLyItemHandleDocumentSupport.HideControl(Not (_supplier.ThirdParty.ContributionType = 0))

                Await LoadIcaPercentage(_supplier, SupplierDistributionLineId, Me.BarraBotones.OperatingUnit.Id)

                'Si la cuenta contable que tiene la linea de distribucion maneja centro costo se pide
                If _suppliersDistibutionLine.IdDistributionLine.IdMainAccount.HandlesCostCenter Then
                    INDlyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemCostCenter.AllowHide = False
                Else
                    INDlyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemCostCenter.AllowHide = True
                End If
                HideImportInfo()
            End If
            Await InitializeSupplierType()
        Else
            SupplierId = 0
            IcaPercentage = 0
            _suppliersDistibutionLine = Nothing
            _supplier = Nothing
        End If
    End Sub

    Private Sub INDsleGetLocationResponsible_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleGetLocationResponsible.EditValueChanged
        If GetLocationResponsible IsNot Nothing Then
            If GetLocationResponsible = 1 Then 'Responsable y Ubicación General
                INDlyItemLocation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemLocation.AllowHide = False
                INDlyItemResponsible.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemResponsible.AllowHide = False
            Else 'Responsable y Ubicación Específico
                INDlyItemLocation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemLocation.AllowHide = True
                INDlyItemResponsible.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemResponsible.AllowHide = True
            End If
            HideImportInfo()
        End If
    End Sub

    Private Sub INDsleSupplierType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleSupplierType.EditValueChanged
        If SupplierTypeId IsNot Nothing AndAlso Not FlagLoad Then
            INDsleSupplierType.ValidateSupplierType()
        End If
    End Sub

    Private Sub INDtxtFreightValue_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtFreightValue.EditValueChanged
        If Not FlagLoad Then
            If FreightValue <> Nothing Then
                INDPopTxtFreightValue.EditValue = FreightValue
                If SettingsFixedAsset IsNot Nothing Then
                    If SettingsFixedAsset.FreightIVAPercentage Is Nothing Then
                        Mensaje(EeventViewerImages.Informacion) = "No existe un porcentaje en el concepto pago IVA flete seleccionado en los parámetros de activos fijos o la cuenta no maneja retención. "
                        FreightIVAValue = 0
                        INDPopSpnFreightIVA.EditValue = 0
                        INDPopTxtFreightValue.EditValue = 0
                        Exit Sub
                    End If
                    FreightIVAValue = (FreightValue * SettingsFixedAsset.FreightIVAPercentage) / 100
                    INDPopSpnFreightIVA.EditValue = FreightIVAValue
                    FreightIVAPercentage = SettingsFixedAsset.FreightIVAPercentage
                Else
                    FreightIVAValue = 0
                    INDPopSpnFreightIVA.EditValue = 0
                End If
            ElseIf FreightValue = 0 Then
                FreightIVAValue = 0
                INDPopSpnFreightIVA.EditValue = 0
                INDPopTxtFreightValue.EditValue = 0
            End If

            CalculateTotalValue()
        End If
    End Sub

    Private Sub INDdteEntryDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDdteEntryDate.EditValueChanged
        If EntryDate IsNot Nothing Then
            HideImportInfo()
        End If
    End Sub

    Private Sub INDSleHandleDocumentSupport_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleHandleDocumentSupport.EditValueChanged
        INDSleDocumentSupportId.EditValue = Nothing
        INDSleDocumentSupportId.Properties.NullText = String.Empty

        INDLyItemDocumentSupportId.HideControl(If(INDSleHandleDocumentSupport.EditValue Is Nothing, True, Not INDSleHandleDocumentSupport.EditValue))
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

#Region "EditValueChanging"

    ''' <summary>
    ''' Evento que se ejecuta al cambiar el valor del control de la rejilla de presupuesto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrepTxtValue_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepTxtValue.EditValueChanging
        If e IsNot Nothing AndAlso e.NewValue IsNot Nothing Then
            Dim entity As FixedAssetEntryCommitment = INDviewGridCommitment.GetFocusedRow()
            If CDec(e.NewValue) > CDec(entity.Balance) Then
                Mensaje(EeventViewerImages.Advertencia) = "El valor a ejecutar no puede ser mayor al saldo del compromiso"
                e.Cancel = True
                Exit Sub
            End If
            entity.Value = e.NewValue
        End If
    End Sub

#End Region

#Region "DataSourceChanged"
    Private Sub INDgcItem_DataSourceChanged(sender As Object, e As EventArgs) Handles INDgcItem.DataSourceChanged
        'Permitir editar si no existen datos en la rejilla
        If ListFixedAssetEntryItem Is Nothing OrElse ListFixedAssetEntryItem.Count = 0 Then
            INDsleAdquisitionType.Properties.ReadOnly = False
            INDsleSupplierDistributionLine.Properties.ReadOnly = False
        Else
            INDsleAdquisitionType.Properties.ReadOnly = True
            INDsleSupplierDistributionLine.Properties.ReadOnly = True
        End If
    End Sub
#End Region

#Region "CustomColumnDisplayText"

    Private Sub INDviewItem_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles INDviewItem.CustomColumnDisplayText
        If e.Value Is Nothing OrElse e.Value.GetType.ToString = "DevExpress.Data.NotLoadedObject" Then
            Exit Sub
        End If
        If e.Column.Name = INDcolSource.Name Then
            Select Case e.Value
                Case 1
                    e.DisplayText = "Ninguna"
                Case 2
                    e.DisplayText = "Orden de compra"
                Case 3
                    e.DisplayText = "Remision de Entrada"
                Case Else

            End Select
        End If
    End Sub

#End Region

#End Region

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
        FixedAssetEntry.Status = 1
        varImp = 2
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbtnCode.ButtonClick
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
        FixedAssetEntry.Status = 1
        varImp = 1
        Guardar()
    End Sub

    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        FixedAssetEntry.Status = 2
        varImp = 3
        Confirmar()
    End Sub

    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        FixedAssetEntry.Status = 2
        varImp = 3
        Confirmar()
    End Sub

    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        FixedAssetEntry.Status = 3
        varImp = 4
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Me.Nuevo()
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir del abarra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, FixedAssetEntry.Id, 0, FixedAssetEntry.Id, _idOperativeUnit)
    End Sub
    ''' <summary>
    ''' Se abstrae la logica del evento BarraBotones_ChangueOperatingUnit
    ''' </summary>
    ''' <param name="operatingUnit"></param>
    ''' <returns></returns>
    Private Async Function ChangeOperatingUnitAsync(operatingUnit As OperatingUnit) As Task
        If FixedAssetEntry Is Nothing OrElse FixedAssetEntry.Id = 0 Then
            If operatingUnit IsNot Nothing AndAlso Me._idOperativeUnit <> operatingUnit.Id Then
                Me._idOperativeUnit = operatingUnit.Id

                Await Presenter.GetSettingFixedAssetByOperatingUnitId(_idOperativeUnit)

                If Me._sequence IsNot Nothing AndAlso
               Me._sequence.Scope.Equals("OU") AndAlso
               Me._sequence.FixedAssetSequenceDetail IsNot Nothing Then

                    If Not Me._sequence.FixedAssetSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    End If
                End If
            End If
        Else
            If operatingUnit.Id <> Me._idOperativeUnit Then
                Mensaje(EeventViewerImages.Advertencia) = "La unidad operativa no tiene ningún efecto sobre el registro ya guardado"
            End If
        End If
    End Function
    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Async Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        Await ChangeOperatingUnitAsync(operatingUnit)
    End Sub

    ''' <summary>
    ''' Barras the botones_ImportarInformacion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ImportarInformacion() Handles BarraBotones.Click_ImportarInformacion
        ImportInfo()
    End Sub

#End Region

End Class
