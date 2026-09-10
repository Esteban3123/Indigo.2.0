'***********************************************************************
' Assembly         : Presentacion.InteropCost
' Author           : Diego Andrés Roldán
' Created          : 26-12-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports Presentation.Base.BaseClass
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.Text
Imports Domain.Entities
Imports DevExpress.Xpo
Imports System.ComponentModel
Imports DevExpress.XtraGrid.Columns
Imports System.Windows.Forms
Imports Presentation.Cost.MVP
Imports Infrastructure.Data.Xpo.CostRepository
Imports Presentation.InteropCost
Imports Presentation.Accounting.MVP
Imports Infrastructure.Data.Xpo.CommonRepository
Imports Presentation.Common.MVP
Imports DevExpress.XtraSpreadsheet
Imports DevExpress.Spreadsheet

#End Region

Public Class FrmCostDistributionDirectExpenses
    Implements ICostDistributionDirectCost

#Region "Builder"

    ''' <summary>
    ''' Initializes a new instance of the <see cref="FrmCostDistributionDirectExpenses"/> class.
    ''' </summary>
    Public Sub New()
        InitializeComponent()
        _tmpCurrentPeriod = New CtrCostMainAccountValueDate()
        _tmpCurrentPeriod.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(_tmpCurrentPeriod)
    End Sub

#End Region

#Region "Globals"

    ''' <summary>
    ''' Constante que contiene  el nombre del modulo
    ''' </summary>
    Public Const MODULE_NAME As String = "Cost"

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Variable que contiene la cabecera de la secuencia
    ''' </summary>
    Private _sequence As Domain.Entities.CostSecuence

    ''' <summary>
    ''' Variable que contiene el id de la secuencia detalle
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' variable que contiene el presentador
    ''' </summary>
    Private _presenter As PCostDirectCost

    ''' <summary>
    ''' Variable privada del parametro de costo
    ''' </summary>
    Private _settingsCost As CostSetting

    ''' <summary>
    ''' entidad que almacena el registro bloqueado
    ''' </summary>
    Private _record As BlockRecordCost

    ''' <summary>
    ''' Columnas del gridview
    ''' </summary>
    Private _colActions As GridColumn

    ''' <summary>
    ''' Columnas del gridview
    ''' </summary>
    Private _colProvisionActions As GridColumn

    ''' <summary>
    ''' Control de Fecha
    ''' </summary>
    Private _tmpCurrentPeriod As CtrCostMainAccountValueDate

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListRoundService As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Valor que me sirve para poder validar al momento de realizar el lostFocus del control de valor
    ''' y calcular las distribuciones para ponerlas en la rejilla
    ''' </summary>
    Dim ValueTemp As Decimal = 0

    ''' <summary>
    ''' Identifica si se esta cargando un registro
    ''' </summary>
    Private _isLoadingControls As Boolean

    ''' <summary>
    ''' valor a distribuir previo por si la validacion sobre el valor contable sale falso
    ''' </summary>
    Private _previusValue As Decimal

    ''' <summary>
    ''' Identifica si el control se encuentra activa
    ''' </summary>
    Private INDtxtValue_Active As Boolean

    ''' <summary>
    ''' entidad de gastos generales
    ''' </summary>
    Private _directCost As CostDistributionDirectCost

    ''' <summary>
    ''' Permite saber si esta en modo de edición para el popup
    ''' (True=Edita, False=Guarda)
    ''' </summary>
    Dim FlagEditIvaDetail As Boolean

    ''' <summary>
    ''' constante que designara la lotificacion para el cargue masivo
    ''' </summary>
    Private Const DefaultBatchSize As Integer = 500

    ''' <summary>
    ''' Listado de detalle de gastos directos
    ''' </summary>
    Property ListDistributionDirectCostDetail As List(Of CostDistributionDirectCostDetail)
        Get
            Return CType(INDgcDirectCostDetail.DataSource, List(Of CostDistributionDirectCostDetail))
        End Get
        Set(value As List(Of CostDistributionDirectCostDetail))
            INDgcDirectCostDetail.SafeInvoke(Sub()
                                                 INDgcDirectCostDetail.DataSource = value
                                             End Sub)
        End Set
    End Property

    ''' <summary>
    ''' Listado de eliminados de los detalles
    ''' </summary>
    Dim ListDelete As List(Of CostDistributionDirectCostDetail)

#End Region

#Region "Fields"

    ''' <summary>
    ''' Obtiene el tag del frontal
    ''' </summary>
    Public ReadOnly Property MyTag As Object Implements ICostDistributionDirectCost.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el layout Control
    ''' </summary>
    Public ReadOnly Property MyLayoutControl As Controls.IndigoLayoutControl Implements ICostDistributionDirectCost.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements ICostDistributionDirectCost.ActionsOnControls
        Set(value As Boolean)
            INDlcRoot.BeginUpdate()

            INDbeCode.Enabled = Not value
            INDRgProvisionDocument.Enabled = value
            INDsleGeneralExpense.Enabled = value
            INDsleSuppliersDistributionLines.Enabled = value
            INDtxtDistributionLine.Enabled = value
            INDsleAccountPayable.Enabled = value
            INDtxtValue.Enabled = value
            INDmeObservation.Enabled = value

            INDtxtBillNumber.Enabled = value
            INDdeBillDate.Enabled = value
            INDseTerm.Enabled = value
            INDseHours.Enabled = value
            INDsleMainAccount.Enabled = value
            INDsleCostCenter.Enabled = value
            INDdeServicePeriodDate.Enabled = value
            INDsleFilingUnit.Enabled = value
            INDsleSupplierType.Enabled = value
            INDtxtPosition.Enabled = value
            INDSleCurrency.Enabled = value
            INDRgDeductibleIva.Enabled = value
            DdbActions.Enabled = value
            INDgcDirectCostDetail.Enabled = value
            INDGcProvisionDocuments.Enabled = value
            INDRgAccountPayableSameSupplier.Enabled = value
            INDdeDocumentDate.Enabled = value

            BarraBotones.StatusRecordVisible = value

            INDlcRoot.EndUpdate()
            If value Then
                INDsleGeneralExpense.Focus()
            Else
                INDbeCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the sequence.
    ''' </summary>
    Public Property Sequence As Domain.Entities.CostSecuence Implements ICostDistributionDirectCost.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As Domain.Entities.CostSecuence)
            _sequence = value
            Me.DicSequense.Clear()
            For Each seq As Domain.Entities.CostSecuenceDetail In Me._sequence.CostSecuenceDetail
                Me.DicSequense.Add(seq.Id, New List(Of String))
            Next
        End Set
    End Property

    ''' <summary>
    ''' Parámetros de costos
    ''' </summary>
    Public WriteOnly Property SettingsCost As CostSetting Implements ICostDistributionDirectCost.SettingsCost
        Set(value As CostSetting)
            If value Is Nothing OrElse value.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SettingCostNotFound", "Cost")
            Else
                _tmpCurrentPeriod.Year = value.Year
                _tmpCurrentPeriod.Month = value.Month
                _tmpCurrentPeriod.LoadDate()
                _settingsCost = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Fecha del Documento
    ''' </summary>
    ''' <returns></returns>
    Public Property DocumentDate As DateTime?
        Get
            Return INDdeDocumentDate.EditValue
        End Get
        Set(value As DateTime?)
            INDdeDocumentDate.EditValue = value
        End Set
    End Property

#End Region

#Region "Properties"

#Region "Main Data"

    ''' <summary>
    ''' Obtiene o establece el codigo del gasto directo
    ''' </summary>
    Public Property Code As String Implements ICostDistributionDirectCost.Code
        Get
            If INDbeCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
                Return String.Empty
            Else
                Return INDbeCode.Text
            End If
        End Get
        Set(value As String)
            INDbeCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que deduce si es documento de provision
    ''' </summary>
    Public Property ProvisionDocument As Boolean?
        Get
            Return INDRgProvisionDocument.EditValue
        End Get
        Set(value As Boolean?)
            INDRgProvisionDocument.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del gasto directo
    ''' </summary>
    Public Property GeneralExpenseId As Integer Implements ICostDistributionDirectCost.GeneralExpenseId
        Get
            Return INDsleGeneralExpense.EditValue
        End Get
        Set(value As Integer)
            INDsleGeneralExpense.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id del Proveedor con su linea de distribución
    ''' </summary>
    Public Property SuppliersDistributionLinesId As Integer? Implements ICostDistributionDirectCost.SuppliersDistributionLinesId
        Get
            Return INDsleSuppliersDistributionLines.EditValue
        End Get
        Set(value As Integer?)
            INDsleSuppliersDistributionLines.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Codigo y Nombre de la Linea de distribucion
    ''' </summary>
    Public Property DistributionLineCodeName As String Implements ICostDistributionDirectCost.DistributionLineCodeName
        Get
            Return INDtxtDistributionLine.EditValue
        End Get
        Set(value As String)
            INDtxtDistributionLine.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Codigo y Nombre del cargo
    ''' </summary>
    Public Property PositionCodeName As String Implements ICostDistributionDirectCost.PositionCodeName
        Get
            Return INDtxtPosition.EditValue
        End Get
        Set(value As String)
            INDtxtPosition.EditValue = value
            INDlciPosition.Visibility = If(value = String.Empty, DevExpress.XtraLayout.Utils.LayoutVisibility.Never, DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
        End Set
    End Property

    ''' <summary>
    ''' Id del Proveedor
    ''' </summary>
    Private Property SupplierId As Integer? Implements ICostDistributionDirectCost.SupplierId

    ''' <summary>
    ''' Id del cargo
    ''' </summary>
    Private Property PositionId As Integer? Implements ICostDistributionDirectCost.PositionId

    ''' <summary>
    ''' Id del parámetro del iva parametrizado
    ''' </summary>
    ''' <value></value>
    Private Property TaxRegistration As Integer?

    ''' <summary>
    ''' Numero de factura
    ''' </summary>
    Public Property AccountPayableId As Integer? Implements ICostDistributionDirectCost.AccountPayableId
        Get
            Return INDsleAccountPayable.EditValue
        End Get
        Set(value As Integer?)
            INDsleAccountPayable.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Valor de la factura
    ''' </summary>
    Private Property Value As Decimal Implements ICostDistributionDirectCost.Value
        Get
            Return CType(INDtxtValue.EditValue, Decimal)
        End Get
        Set(value As Decimal)
            INDtxtValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Indica el parametro de IVA descontable
    ''' </summary>
    Private Property DeductibleIVA As Boolean Implements ICostDistributionDirectCost.DeductibleIVA
        Get
            Return CType(INDRgDeductibleIva.EditValue, Boolean)
        End Get
        Set(value As Boolean)
            INDRgDeductibleIva.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Observaciones
    ''' </summary>
    ''' <returns></returns>
    Public Property Observation As String Implements ICostDistributionDirectCost.Observation
        Get
            Return INDmeObservation.EditValue
        End Get
        Set(value As String)
            INDmeObservation.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establece el id de la moneda y establece opcionalmente la abbreviacion 
    ''' </summary>
    ''' <param name="CurrencyAbbreviation"></param>
    ''' <returns></returns>
    Public Property CurrencyId(Optional currencyAbbreviation As String = Nothing) As Integer Implements ICostDistributionDirectCost.CurrencyId
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
    ''' Establece el Datasource del Tipo de Comprobante Contable
    ''' </summary>
    ''' <returns></returns>
    Public Property ThirdPartyXpo As XPInstantFeedbackSource
        Get
            Return INDsleThirdParty.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleThirdParty.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Id del tercero cuando NO se genera CxP
    ''' </summary>
    ''' <returns></returns>
    Private Property ThirdPartyId As Integer Implements ICostDistributionDirectCost.ThirdPartyId
        Get
            Return INDsleThirdParty.EditValue
        End Get
        Set(value As Integer)
            INDsleThirdParty.EditValue = value
        End Set
    End Property

#End Region

#Region "Bill Data"

    ''' <summary>
    ''' Obtiene o establece el numero de la factura
    ''' </summary>
    Public Property BillNumber As String Implements ICostDistributionDirectCost.BillNumber
        Get
            Return INDtxtBillNumber.Text
        End Get
        Set(value As String)
            INDtxtBillNumber.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Fecha de la Factura
    ''' </summary>
    Public Property BillDate As Date? Implements ICostDistributionDirectCost.BillDate
        Get
            Return INDdeBillDate.EditValue
        End Get
        Set(value As Date?)
            INDdeBillDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Plazo
    ''' </summary>
    Public Property Term As Integer? Implements ICostDistributionDirectCost.Term
        Get
            Return If(INDseTerm.EditValue Is Nothing, Nothing, CType(INDseTerm.EditValue, Integer))
        End Get
        Set(value As Integer?)
            INDseTerm.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Plazo
    ''' </summary>
    Public Property Hours As Integer? Implements ICostDistributionDirectCost.Hours
        Get
            Return If(INDseHours.EditValue Is Nothing, Nothing, CType(INDseHours.EditValue, Integer))
        End Get
        Set(value As Integer?)
            INDseHours.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id del Proveedor
    ''' </summary>
    Public Property MainAccountId As Integer? Implements ICostDistributionDirectCost.MainAccountId
        Get
            Return INDsleMainAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleMainAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id del Proveedor
    ''' </summary>
    Public Property CostCenterId As Integer? Implements ICostDistributionDirectCost.CostCenterId
        Get
            Return INDsleCostCenter.EditValue
        End Get
        Set(value As Integer?)
            INDsleCostCenter.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Fecha de la Radicación
    ''' </summary>
    Public Property ServicePeriodDate As Date? Implements ICostDistributionDirectCost.ServicePeriodDate
        Get
            Return INDdeServicePeriodDate.EditValue
        End Get
        Set(value As Date?)
            INDdeServicePeriodDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id de la unidad de radicacion Proveedor
    ''' </summary>
    Public Property FilingUnitId As Integer? Implements ICostDistributionDirectCost.FilingUnitId
        Get
            Return INDsleFilingUnit.EditValue
        End Get
        Set(value As Integer?)
            INDsleFilingUnit.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id del tipo de Proveedor
    ''' </summary>
    Public Property SupplierTypeId As Integer? Implements ICostDistributionDirectCost.SupplierTypeId
        Get
            Return INDsleSupplierType.EditValue
        End Get
        Set(value As Integer?)
            INDsleSupplierType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el si el cxp va al mimso proveedor o no
    ''' </summary>
    Public Property AccountPayableSameSupplier As Boolean? Implements ICostDistributionDirectCost.AccountPayableSameSupplier
        Get
            Return INDRgAccountPayableSameSupplier.EditValue
        End Get
        Set(value As Boolean?)
            INDRgAccountPayableSameSupplier.EditValue = value
        End Set
    End Property

#End Region

#Region "Other Information"

    ''' <summary>
    ''' Año de la distribución del gasto Directo
    ''' </summary>
    Public Property Year As Integer Implements ICostDistributionDirectCost.Year
        Get
            Return _tmpCurrentPeriod.Year
        End Get
        Set(value As Integer)
            _tmpCurrentPeriod.Year = value
        End Set
    End Property

    ''' <summary>
    ''' Mes de la distribución del gasto Directo
    ''' </summary>
    Public Property Month As Integer Implements ICostDistributionDirectCost.Month
        Get
            Return _tmpCurrentPeriod.Month
        End Get
        Set(value As Integer)
            _tmpCurrentPeriod.Month = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado
    ''' </summary>
    Public Property Status As String Implements ICostDistributionDirectCost.Status
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As String)
            BarraBotones.StatusRecord = value
        End Set
    End Property

#End Region

#Region "Properties ImportFile"

    ''' <summary>
    ''' ruta del archivo de excel
    ''' </summary>
    ''' <remarks></remarks>
    Dim myStream As String = Nothing

    ''' <summary>
    ''' listado de errores que se presentaron validando el archivo
    ''' </summary>
    ''' <remarks></remarks>
    Dim listErrorsImportFile As List(Of String)

    ''' <summary>
    ''' Colección de filas que se van a procesar
    ''' </summary>
    ''' <remarks></remarks>
    Dim rows As RowCollection

    ''' <summary>
    ''' Colección de Columnas a validar por elemento de costo elegido
    ''' </summary>
    ''' <remarks></remarks>
    Dim columns As ColumnCollection

    ''' <summary>
    ''' listado de las filas que se van a procesar y a validar
    ''' </summary>
    ''' <remarks></remarks>
    Dim listRows As New Concurrent.ConcurrentBag(Of ImportFileRow)()

#End Region

#End Region

#Region "XPO"

#Region "Individual XPO"

    Private Property GeneralExpense As CostGeneralExpenseXpo Implements ICostDistributionDirectCost.GeneralExpense

#End Region

    ''' <summary>
    ''' Obtiene los gastos generales
    ''' </summary>
    Public Property GeneralExpenseXpo As XPInstantFeedbackSource Implements ICostDistributionDirectCost.GeneralExpenseXpo
        Get
            Return INDsleGeneralExpense.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleGeneralExpense.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource del tercero
    ''' </summary>
    ''' <value></value>
    Public Property SuppliersDistributionLinesXpo As XPInstantFeedbackSource Implements ICostDistributionDirectCost.SuppliersDistributionLinesXpo
        Get
            Return INDsleSuppliersDistributionLines.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleSuppliersDistributionLines.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource de la cuenta por pagar
    ''' </summary>
    ''' <value></value>
    Public Property AccountPayableXpo As XPInstantFeedbackSource Implements ICostDistributionDirectCost.AccountPayableXpo
        Get
            Return INDsleAccountPayable.Datasource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleAccountPayable.Datasource = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource de los terceros para el tipo de distribución mano de obra y gastos generales
    ''' </summary>
    ''' <value></value>
    Public Property ThirdPartyRepositoryXpo As XPInstantFeedbackSource Implements ICostDistributionDirectCost.ThirdPartyRepositoryXpo
        Get
            Return INDRpSleThirParty.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDRpSleThirParty.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource de los cargos para la distribución de la mano de obra
    ''' </summary>
    ''' <value></value>
    Public Property PositionRepositoryXpo As XPInstantFeedbackSource Implements ICostDistributionDirectCost.PositionRepositoryXpo
        Get
            Return INDRpSlePosition.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDRpSlePosition.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource de los conceptos de retención
    ''' </summary>
    ''' <returns></returns>
    Public Property RetentionConceptXpo As XPInstantFeedbackSource Implements ICostDistributionDirectCost.RetentionConceptXpo
        Get
            Return INDRpSleRetention.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDRpSleRetention.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource de los productos para el tipo de distribución por producto
    ''' </summary>
    ''' <value></value>
    Public Property InventoryProductRepositoryXpo As XPInstantFeedbackSource Implements ICostDistributionDirectCost.InventoryProductRepositoryXpo
        Get
            Return INDRpSleProduct.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDRpSleProduct.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource del centro de costo
    ''' </summary>
    ''' <value></value>
    Public Property CostCenterXpo As XPInstantFeedbackSource Implements ICostDistributionDirectCost.CostCenterXpo
        Get
            Return INDsleCostCenter.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCostCenter.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource de la unidad de radicacion
    ''' </summary>
    ''' <value></value>
    Public Property FilingUnitXpo As List(Of FilingUnit) Implements ICostDistributionDirectCost.FilingUnitXpo
        Get
            Return INDsleFilingUnit.Properties.DataSource
        End Get
        Set(value As List(Of FilingUnit))
            INDsleFilingUnit.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the production center data source.
    ''' </summary>
    Public Property CostDistributionBaseDetailsXpo As List(Of CostDistributionBaseDetailXpo) Implements ICostDistributionDirectCost.CostDistributionBaseDetailsXpo
        Get
            Return INDsleProductionCenter.Properties.DataSource
        End Get
        Set(value As List(Of CostDistributionBaseDetailXpo))
            INDsleProductionCenter.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource unidades de medida
    ''' </summary>
    ''' <value></value>
    Public Property MeasurementUnitXpo As XPInstantFeedbackSource Implements ICostDistributionDirectCost.MeasurementUnitXpo
        Get
            Return INDsleMeasurementUnit.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleMeasurementUnit.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource de centros de produccion para el repositorio
    ''' </summary>
    Public Property MeasureUnitDataSourceRerpository As XPInstantFeedbackSource Implements ICostDistributionDirectCost.MeasureUnitDataSourceRerpository
        Get
            Return CType(INDrpsleMeasurementUnit.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDrpsleMeasurementUnit.DataSource = value
        End Set
    End Property

    Public Property RateIvaXpo As XPInstantFeedbackSource Implements ICostDistributionDirectCost.RateIvaXpo
        Get
            Return INDspIVA.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDspIVA.Properties.DataSource = value
        End Set
    End Property

    Public Property CurrencyXpo As XPInstantFeedbackSource Implements ICostDistributionDirectCost.CurrencyXpo
        Get
            Return INDSleCurrency.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleCurrency.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "ICrud"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.ICrudBase.OpenSearch
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue

        Dim ListItems As New List(Of Tuple(Of String, Integer))
        ListItems.Add(New Tuple(Of String, Integer)("Sin Confirmar", 1))
        ListItems.Add(New Tuple(Of String, Integer)("Confirmado", 2))
        ListItems.Add(New Tuple(Of String, Integer)("Anulado", 3))
        ListItems.Add(New Tuple(Of String, Integer)("Reversado", 4))
        ListItems.Add(New Tuple(Of String, Integer)("Confirmado Sin Legalizar", 5))
        ListItems.Add(New Tuple(Of String, Integer)("Confirmado Legalizado", 6))

        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Description", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3},
                              New ColumnInfo() With {.Caption = "Elemento Costo", .FieldName = "GeneralExpenseId.CodeName", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2},
                              New ColumnInfo() With {.Caption = "Año", .FieldName = "Year", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1},
                              New ColumnInfo() With {.Caption = "Mes", .FieldName = "Month", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "Status", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1), .ColumnEdit = True, .ListItemsDatasourceColumEdit = ListItems}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListCostDistributionDirectCost
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements Base.ICrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Me.NewDirectCost()
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Sub Guardar() Implements Base.ICrudBase.Guardar
        SaveAndConfirm(1)
    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.ICrudBase.LogicaBotonActualizar
    End Sub

    ''' <summary>
    ''' Guarda o actualiza el registro
    ''' </summary>
    ''' <param name="statusRecord">Estado del registro</param>
    Private Async Sub SaveAndConfirm(ByVal statusRecord As Byte)
        Me.BarraBotones.Focus()
        If Not ValidateFormControls() Then
            Exit Sub
        End If
        AssigningValues()
        _directCost.Status = statusRecord
        Try
            AsyncLoader(True)
            Using model = New MCostDistributionDirectCost(Me.Tag)
                Dim result = Await model.SaveDistributionDirectCost(Me._directCost, Me._idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If _directCost.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._sequence.CostSecuenceDetail(0).Id).RemoveAt(0)
                        End If
                    End If
                    Me._directCost = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Mensaje(EeventViewerImages.Informacion) = result.Message
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDbeCode.Enabled = False
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbeCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Eliminars this instance.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.ICrudBase.Eliminar
        If Me._directCost IsNot Nothing AndAlso Me._directCost.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using model = New MCostDistributionDirectCost(Me.Tag)
                        AsyncLoader(True)
                        Dim result = Await model.DeleteDistributionDirectCost(Me._directCost)
                        If result.StatusCode = eStatusResult.SUCCESS Then
                            Mensaje(EeventViewerImages.Informacion) = result.Message
                            Await Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = result.Message
                            AsyncLoader(False)
                            INDbeCode.Enabled = False
                        End If
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDbeCode.Enabled = False
                    Throw ex
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
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
    ''' Devuelve el valor del OpenSearch.
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDbeCode.Text = ReturnValue
        If INDbeCode.Text <> String.Empty Then
            LoadControls()
            If INDbeCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbeCode.Enabled = False
        End If
    End Sub

#End Region

#Region "BarButton Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
        End If
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
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbeCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        SaveAndConfirm(2)
    End Sub

    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        SaveAndConfirm(2)
    End Sub

    Private Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        SaveAndConfirm(2)
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el botón desconfirmar de la barra
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_Desconfirmar() Handles BarraBotones.Click_Desconfirmar
        Try
            AsyncLoader(True)
            Using model As New MCostDistributionDirectCost(Me.Tag.ToString())
                Dim result = Await model.DisconfirmDistributionDirectCost(Me._directCost)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    Me._directCost = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Mensaje(EeventViewerImages.Informacion) = result.Message
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDbeCode.Enabled = False
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try

    End Sub

    Private Async Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If _directCost.ProvisionDocument And _directCost.Status = 5 Then
            If MessageIndigo.Show("Esta seguro de reversar el documento?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using model As New MCostDistributionDirectCost(Me.Tag)
                        AsyncLoader(True)
                        Dim result = Await model.ReverseProvisionDocument(Me._directCost.Id)
                        If result.StatusCode = eStatusResult.SUCCESS Then
                            Mensaje(EeventViewerImages.Informacion) = result.MessageResult(0)
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = result.Message
                            AsyncLoader(False)
                            INDbeCode.Enabled = False
                        End If
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDbeCode.Enabled = False
                    Throw ex
                End Try
            End If
        ElseIf _directCost.Status = 2 And Not GeneralExpense.GenerateAccountPayable Then
            If MessageIndigo.Show("Esta seguro de reversar el documento?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                SaveAndConfirm(4)
            End If
        Else
            SaveAndConfirm(3)
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' Handles the Load event of the FrmDistributionDirectExpenses control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub FrmDistributionDirectExpenses_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlcRoot, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue

        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        _presenter = New PCostDirectCost(Me)
        _presenter.LoadDefinitionLayout()
        INDlciThirdParty.HideControl
        INDlciDocumentDate.HideControl

        AsyncLoader(True)
        Await _presenter.GetSequence()
        Await _presenter.LoadSettingCost()
        AsyncLoader(False)
        SetFormatCurrencyUI(_presenter.GetOfficialCurrencyFromCompanySettings().OfficialCurrency.Abbreviation)


        INDtxtMaximumAmount.Properties.MaxValue = Decimal.MaxValue

        IndigoGridView2.SetListAcction(INDGvIVADetail, New List(Of eAcciones)() From {{eAcciones.Edit}, {eAcciones.Remove}})
        IndigoGridView1.MoreInfoColunmns(INDgvNoGenerateCxP)
        Using Model As New MDocumentAccount(Me.Tag)
            Dim periods As List(Of ClosedMonth) = Model.GetOpenPeriod()
            Dim earliestPeriod As ClosedMonth = periods _
                                .OrderBy(Function(p) p.Year) _
                                .ThenBy(Function(p) p.Month) _
                                .FirstOrDefault()
            ' Verificar si se encontró un periodo
            If earliestPeriod IsNot Nothing Then
                Dim minDate As Date = New Date(earliestPeriod.Year, earliestPeriod.Month, 1)
                INDdeDocumentDate.Properties.MinValue = minDate
            End If
        End Using

        Deshacer()
        InitializeTuples()
        LoadStatus()
        SetActionsGrid()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _idOperativeUnit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _presenter = Nothing
        _settingsCost = Nothing
        _record = Nothing
        _colActions = Nothing
        _colProvisionActions = Nothing
        _tmpCurrentPeriod = Nothing
        ListRoundService = Nothing
        ValueTemp = Nothing
        _isLoadingControls = Nothing
        _previusValue = Nothing
        INDtxtValue_Active = Nothing
        _directCost = Nothing
        ListDistributionDirectCostDetail = Nothing
        ListDelete = Nothing
        GeneralExpense = Nothing

        myStream = Nothing
        listRows = Nothing
        rows = Nothing
        columns = Nothing
        listErrorsImportFile = Nothing
    End Sub

#End Region

#Region "Shown"

    Private Sub FrmDistributionDirectExpense_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDbeCode.Focus()
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Handles the FormClosing event of the FrmDistributionDirectExpenses control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmDistributionDirectExpenses_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Handles the KeyDown event of the INDbteCode control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDbteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbeCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(INDbeCode.Text) Then
                    LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(INDbeCode.Text) Then
                    Me.NewDirectCost()
                Else
                    LoadControls()
                End If
            End If
        ElseIf e.KeyCode = Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the INDpceAddDetail control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDpceAddDetail_KeyDown(sender As Object, e As KeyEventArgs) Handles INDpceAddDetail.KeyDown
        If e.KeyCode = Keys.Enter Then
            INDpceAddDetail.ShowPopup()
        End If
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de elementos del costo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleGeneralExpense_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleGeneralExpense.QueryPopUp
        If GeneralExpenseXpo Is Nothing Then
            _presenter.InitializeGeneralExpenseXpo()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de proveedores y sus lineas de distribucion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSuppliersDistributionLines_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleSuppliersDistributionLines.QueryPopUp
        If SuppliersDistributionLinesXpo Is Nothing Then
            _presenter.InitializeSuppliersDistributionLinesXpo()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de cuentas por pagar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleAccountPayable_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleAccountPayable.QueryPopUp
        If AccountPayableXpo Is Nothing AndAlso ThirdPartyId > 0 Then
            Dim CostDistributionDirectCostId = IIf(_directCost IsNot Nothing AndAlso _directCost.Id > 0, _directCost.Id, 0)
            _presenter.GetXpoAccountPayable(ThirdPartyId, CostDistributionDirectCostId)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de centros del costo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCostCenter_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCostCenter.QueryPopUp
        If CostCenterXpo Is Nothing Then
            _presenter.InitializeCostCenterXpo()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de unidades de radicacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleFilingUnit_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleFilingUnit.QueryPopUp
        If FilingUnitXpo Is Nothing Then
            _presenter.InitializeFilingUnitXpo()
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleProductionCenter control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleProductionCenter_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleProductionCenter.QueryPopUp
        If CostDistributionBaseDetailsXpo Is Nothing Then
            _presenter.InitializeCostDistributionBaseDetails()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de unidad de medida
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleMeasurementUnit_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleMeasurementUnit.QueryPopUp
        If MeasurementUnitXpo Is Nothing Then
            _presenter.InitializeMeasurimentUnit(INDsleGeneralExpense.EditValue)
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDpceAddDetail control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDpceAddDetail_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDpceAddDetail.QueryPopUp
        INDsleProductionCenter.Focus()
    End Sub

    ''' <summary>
    ''' Datasource de la moneda
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCurrency_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleCurrency.QueryPopUp
        If CurrencyXpo Is Nothing Then
            _presenter.InitializateCurrency()
        End If
    End Sub

    ''' <summary>
    ''' Evento para cargar el Datasource de los terceros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleThirdParty_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleThirdParty.QueryPopUp
        If ThirdPartyXpo Is Nothing Then
            ThirdPartyXpo = _presenter.GetAllThirdParty()
        End If
    End Sub

#End Region

#Region "EditValueChanging"

    Private Sub INDrpspnCount_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrpspnCount.EditValueChanging
        If String.IsNullOrEmpty(e.NewValue) OrElse e.NewValue <= 0 Then
            e.Cancel = True
            Exit Sub
        End If

        If Not IsDirectDistribution(GeneralExpense) Then
            e.Cancel = True
            Exit Sub
        End If

        Dim costDistributionDirectCostDetail As CostDistributionDirectCostDetail = CType(INDgvDirectCostDetail.GetFocusedRow, CostDistributionDirectCostDetail)
        If costDistributionDirectCostDetail Is Nothing Then
            e.Cancel = True
            Exit Sub
        End If

        costDistributionDirectCostDetail.Count = e.NewValue
        costDistributionDirectCostDetail.Value = costDistributionDirectCostDetail.Count * costDistributionDirectCostDetail.CostValue

        CalculateBalance()
        CalculatePercentage()
    End Sub

    Private Sub INDrpspnValue_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrpspnValue.EditValueChanging
        If String.IsNullOrEmpty(e.NewValue) OrElse e.NewValue <= 0 Then
            e.Cancel = True
            Exit Sub
        End If

        If Not IsDirectDistribution(GeneralExpense) Then
            e.Cancel = True
            Exit Sub
        End If

        Dim costDistributionDirectCostDetail As CostDistributionDirectCostDetail = CType(INDgvDirectCostDetail.GetFocusedRow, CostDistributionDirectCostDetail)
        If costDistributionDirectCostDetail Is Nothing Then
            e.Cancel = True
            Exit Sub
        End If

        Dim CostValue As Decimal = 0
        If Not Decimal.TryParse(e.NewValue.ToString().Replace(indigo.Culture.NumberFormat.CurrencyGroupSeparator, indigo.Culture.NumberFormat.NumberDecimalSeparator), Globalization.NumberStyles.AllowDecimalPoint, indigo.Culture, CostValue) Then
            e.Cancel = True
            Exit Sub
        End If

        costDistributionDirectCostDetail.CostValue = CostValue
        costDistributionDirectCostDetail.Value = costDistributionDirectCostDetail.Count * costDistributionDirectCostDetail.CostValue

        CalculateBalance()
        CalculatePercentage()
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDsleGeneralExpense control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsleGeneralExpense_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleGeneralExpense.EditValueChanged
        HideGeneralDetailColumns()

        If _directCost IsNot Nothing AndAlso Not _isLoadingControls Then
            _directCost.CostDistributionDirectCostDetail.Clear()
            ListDistributionDirectCostDetail = _directCost.CostDistributionDirectCostDetail.ToList()
            INDgcDirectCostDetail.RefreshDataSource()
            CleanControlsDetail()
        End If

        If GeneralExpenseId > 0 AndAlso GeneralExpense IsNot Nothing Then
            If GeneralExpense?.GenerateAccountPayable Is Nothing OrElse GeneralExpense.GenerateAccountPayable Then
                GenerateAccountPayableFields()
                Select Case GeneralExpense.DistributionType
                    Case 1
                        StandardDistributionColumns()

                        INDebtDistribution.AddExcelSheets(New ExcelSheet With {
                        .Columns = New List(Of ExcelColumn) From {
                            New ExcelColumn With {.Name = "Centro Produccion"},
                            New ExcelColumn With {.Name = "Cuenta Contable"},
                            New ExcelColumn With {.Name = "Centro de Costo"},
                            New ExcelColumn With {.Name = "Unidad de medida"},
                            New ExcelColumn With {.Name = "Cantidad"},
                            New ExcelColumn With {.Name = "Puntos de valor"}
                        }
                    })
                    Case 2
                        ManpowerDistributionColumns()
                        INDLyAccountPayableSameSupplier.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        INDebtDistribution.AddExcelSheets(New ExcelSheet With {
                        .Columns = New List(Of ExcelColumn) From {
                            New ExcelColumn With {.Name = "Centro Produccion"},
                            New ExcelColumn With {.Name = "Cuenta Contable"},
                            New ExcelColumn With {.Name = "Centro de Costo"},
                            New ExcelColumn With {.Name = "Tercero"},
                            New ExcelColumn With {.Name = "Cargo", .Type = ExcelColumnType.Text},
                            New ExcelColumn With {.Name = "Horas Laboradas", .Comment = "Campo númerico que solo se permite usar la coma (,) como separador decimal"},
                            New ExcelColumn With {.Name = "Horas Contratadas", .Comment = "Campo númerico que solo se permite usar la coma (,) como separador decimal"},
                            New ExcelColumn With {.Name = "Total", .Comment = "Campo númerico que solo se permite usar la coma (,) como separador decimal"}
                        }
                    })
                    Case 3
                        OverheadDistributionColumns()

                        INDebtDistribution.AddExcelSheets(New ExcelSheet With {
                        .Columns = New List(Of ExcelColumn) From {
                            New ExcelColumn With {.Name = "Centro Produccion"},
                            New ExcelColumn With {.Name = "Cuenta Contable"},
                            New ExcelColumn With {.Name = "Centro de Costo"},
                            New ExcelColumn With {.Name = "Tercero"},
                            New ExcelColumn With {.Name = "Total", .Comment = "Campo númerico que solo se permite usar la coma (,) como separador decimal"}
                        }
                    })
                    Case 4
                        ProductsDistributionColumns()

                        INDebtDistribution.AddExcelSheets(New ExcelSheet With {
                        .Columns = New List(Of ExcelColumn) From {
                            New ExcelColumn With {.Name = "Centro Produccion"},
                            New ExcelColumn With {.Name = "Cuenta Contable"},
                            New ExcelColumn With {.Name = "Centro de Costo"},
                            New ExcelColumn With {.Name = "Producto"},
                            New ExcelColumn With {.Name = "Total", .Comment = "Campo númerico que solo se permite usar la coma (,) como separador decimal"}
                        }
                    })
                    Case Else
                        HideGeneralDetailColumns()
                        INDebtDistribution.AddRangeColumns("Para obtener la estructura del archivo elija un elemento del costo")
                End Select
            Else ' En caso de No Generar CxP, se genera su propia estructura
                NoGenerateAccountPayableFields()

                INDebtDistribution.AddExcelSheets(New ExcelSheet With {
                .Columns = New List(Of ExcelColumn) From {
                            New ExcelColumn With {.Name = "Cuenta Contable", .Comment = "Digite el código exacto de la Cuenta Contable", .Type = ExcelColumnType.Text},
                            New ExcelColumn With {.Name = "Naturaleza Cuenta Contable", .Comment = "1: Débito - 2: Crédito", .Type = ExcelColumnType.Text},
                            New ExcelColumn With {.Name = "Tercero", .Comment = "Digite el Nit del terctero", .Type = ExcelColumnType.Text},
                            New ExcelColumn With {.Name = "Centro de Costo", .Comment = "Digite el código exacto del Centro de Costo", .Type = ExcelColumnType.Text},
                            New ExcelColumn With {.Name = "Observaciones"},
                            New ExcelColumn With {.Name = "Valor", .Comment = "Campo númerico que solo se permite usar la coma (,) como separador decimal"},
                            New ExcelColumn With {.Name = "Retención", .Comment = "Digite el código exacto del Concepto de Retención", .Type = ExcelColumnType.Text},
                            New ExcelColumn With {.Name = "Valor Facturado", .Comment = "Campo númerico que solo se permite usar la coma (,) como separador decimal"},
                            New ExcelColumn With {.Name = "Base Retención", .Comment = "Campo númerico que solo se permite usar la coma (,) como separador decimal"},
                            New ExcelColumn With {.Name = "Cédula del empleado"},
                            New ExcelColumn With {.Name = "Cargo", .Comment = "Digite el código exacto del Cargo", .Type = ExcelColumnType.Text},
                            New ExcelColumn With {.Name = "Horas contratadas"},
                            New ExcelColumn With {.Name = "Horas laboradas"},
                            New ExcelColumn With {.Name = "Concepto de nómina"},
                            New ExcelColumn With {.Name = "Fecha del proceso", .Comment = "Formato DDMMAA", .Type = ExcelColumnType.Text}
                        }
                    })
            End If
        End If

    End Sub

    Private Sub INDRgProvisionDocument_EditValueChanged(sender As Object, e As EventArgs) Handles INDRgProvisionDocument.EditValueChanged
        If GeneralExpense?.GenerateAccountPayable Is Nothing Or GeneralExpense?.GenerateAccountPayable Then
            If ProvisionDocument IsNot Nothing Then
                If ProvisionDocument Then
                    INDLyDeductibleIva.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlcgIVADetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlciFilingUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlciTerm.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlciBillDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlciBillNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    LyGroupProvisionDocuments.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    BillNumber = String.Empty
                    BillDate = Nothing
                    Term = 0
                    ServicePeriodDate = Nothing
                    FilingUnitId = Nothing
                Else
                    INDLyDeductibleIva.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlcgIVADetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlciFilingUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlciTerm.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlciBillDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlciBillNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    LyGroupProvisionDocuments.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                End If
            End If
        End If
    End Sub

    Private Async Sub INDsleSuppliersDistributionLines_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleSuppliersDistributionLines.EditValueChanged
		If Not _isLoadingControls Then
			DistributionLineCodeName = String.Empty
			PositionCodeName = String.Empty
			SupplierId = Nothing
			PositionId = Nothing
			ThirdPartyId = Nothing
			AccountPayableXpo = Nothing
			AccountPayableId = Nothing
			INDsleAccountPayable.DisplayNullText = String.Empty
			INDlciAccountPayable.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
			Value = Nothing
			Term = Nothing
			INDlciHours.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
			Hours = Nothing

			BillNumber = String.Empty
			MainAccountId = Nothing
			INDsleMainAccount.Properties.NullText = String.Empty
			CostCenterId = Nothing
			INDsleCostCenter.Properties.NullText = String.Empty
			INDlciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
			SupplierTypeId = Nothing
			INDsleSupplierType.SupplierId = Nothing
			INDsleSupplierType.DisplayNullText = String.Empty

			If SuppliersDistributionLinesId > 0 Then
				Dim suppplierMainAccount = _presenter.GetSupplierDistributionLineById(SuppliersDistributionLinesId)
				DistributionLineCodeName = suppplierMainAccount.IdDistributionLine.CodeName

				If ProvisionDocument Then
					MainAccountId = suppplierMainAccount.IdDistributionLine.MainAccountCostProvisionId?.Id
					INDsleMainAccount.Properties.NullText = suppplierMainAccount.IdDistributionLine.MainAccountCostProvisionId?.NumberName
					INDsleMainAccount.ReadOnly = True
					Term = Nothing
				Else
					MainAccountId = suppplierMainAccount.IdDistributionLine.IdMainAccount.Id
					INDsleMainAccount.Properties.NullText = suppplierMainAccount.IdDistributionLine.IdMainAccount.NumberName
					INDsleMainAccount.ReadOnly = False
					Term = suppplierMainAccount.IdSupplier.TimeLimitDays
				End If

				If suppplierMainAccount.IdDistributionLine.IdMainAccount.HandlesCostCenter Then
					INDlciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
				End If

				SupplierId = suppplierMainAccount.IdSupplier.Id
				INDsleSupplierType.SupplierId = SupplierId
				If suppplierMainAccount.PositionId IsNot Nothing Then
					PositionId = suppplierMainAccount.PositionId.Id
					PositionCodeName = suppplierMainAccount.PositionId.CodeName
					INDlciHours.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
				End If

				ThirdPartyId = suppplierMainAccount.IdSupplier.IdThirdParty.Id
				INDlciAccountPayable.Visibility = If(Me._settingsCost.AccountingCosts, DevExpress.XtraLayout.Utils.LayoutVisibility.Never, DevExpress.XtraLayout.Utils.LayoutVisibility.Always)

				Dim result = Await VerifyTaxRegistration()

				If suppplierMainAccount?.IdSupplier Is Nothing Then
					Exit Sub
				End If

				If Not ProvisionDocument Then
					INDLyDeductibleIva.HideControl(Not result OrElse (suppplierMainAccount?.IdSupplier?.IdThirdParty?.PersonType = 1 AndAlso suppplierMainAccount?.IdSupplier?.IdThirdParty?.ContributionType = 0))
					INDlcgIVADetails.HideControl(suppplierMainAccount?.IdSupplier?.IdThirdParty?.PersonType = 1 AndAlso suppplierMainAccount?.IdSupplier?.IdThirdParty?.ContributionType = 0)

					If GeneralExpenseId <> 0 Then
						Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
					End If
				End If
			End If
		End If
	End Sub

    Private Sub INDsleAccountPayable_EditValueChanged(sender As Object, e As EditValueChangedEventArgs) Handles INDsleAccountPayable.EditValueChanged
        If INDsleAccountPayable.Datasource IsNot Nothing AndAlso INDsleAccountPayable.EditValue IsNot Nothing Then
            Dim obj = DirectCast(DirectCast(viewAccountPayables.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.PaymentsRepository.VAccountPayablesWithoutDistribuitXpo)
            Year = obj.BillDate.Year
            Month = obj.BillDate.Month
            If Year < _settingsCost.Year OrElse (Year = _settingsCost.Year AndAlso Month < _settingsCost.Month) Then
                Mensaje(EeventViewerImages.Advertencia) = "La factura corresponde a una fecha anterior al periodo actual del módulo de costos"
                INDsleAccountPayable.EditValue = Nothing
                Value = 0
                Exit Sub
            End If

            Value = obj.InvoiceValue
            _tmpCurrentPeriod.LoadDate()
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDTxtValue_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtValue.EditValueChanged
        If Not INDtxtValue_Active Then
            CalculateValues()
        End If
        Me.EnableOrDisableCurrencyControl()
    End Sub

    Private Sub INDsleMeasurementUnit_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleMeasurementUnit.EditValueChanged
        If INDsleMeasurementUnit.Properties.DataSource IsNot Nothing AndAlso INDsleMeasurementUnit.EditValue IsNot Nothing Then
            Dim obj = DirectCast(DirectCast(viewSearchMeasurementUnit.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.InventoryRepository.MeasureUnitXpo)
            Me.INDtxtCostValue.EditValue = obj.CostValue
            Me.INDtxtCostValue.Properties.ReadOnly = Not obj.AllowEditCostValue
        End If
    End Sub

    ''' <summary>
    ''' evento cuando se cambia la moneda
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCurrency_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleCurrency.EditValueChanged
        Dim currencyAbbreviation = TryCast(TryCast(INDGvCurrency?.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)?.OriginalRow, CommonCurrencyXpo)?.Abbreviation

        If INDSleCurrency.EditValue Is Nothing OrElse _isLoadingControls OrElse String.IsNullOrEmpty(currencyAbbreviation) Then
            Exit Sub
        End If
        Me.SetFormatCurrencyUI(currencyAbbreviation)
    End Sub

    Private Sub INDspIVA_EditValueChanged(sender As Object, e As EventArgs) Handles INDspIVA.EditValueChanged
        If INDspIVA.EditValue IsNot Nothing AndAlso INDspIVA.EditValue > 0 Then
            Using modelIva As New MGeneralLedgerIVA(Me.Tag)
                Dim iva = modelIva.GetGeneralLedgerIVAById(INDspIVA.EditValue)
                INDtxtIvaValue.EditValue = Math.Round((INDtxtBaseIva.EditValue * iva.ObjectEmbbeded.Percentage) / 100, 2, MidpointRounding.AwayFromZero)
            End Using
        End If
    End Sub

    Private Sub INDRgDeductibleIva_SelectedIndexChanged(sender As Object, e As EventArgs) Handles INDRgDeductibleIva.EditValueChanged
        ' TODO: ignorar eliminar detalles al cargar, validar como debe quedar
        If _isLoadingControls Then
            Exit Sub
        End If

        If INDtxtValue.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Digite un valor primero"
            Exit Sub
        End If


        If DeductibleIVA Then
            TaxRegistration = 2
            If _directCost.CostDistributionDirectCostDetail.Count > 0 Then
                For Each item In (From x In _directCost.CostDistributionDirectCostDetail).ToList()
                    item.IvaValue = Nothing
                    item.BaseValue = Nothing
                Next
            End If
        Else
            TaxRegistration = 1
        End If
        ShowGeneralDetailColumns()
        CalculateValues()
        INDgcDirectCostDetail.RefreshDataSource()

    End Sub

    Private Sub INDtxtBaseIva_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtBaseIva.EditValueChanged
        If INDspIVA.EditValue IsNot Nothing AndAlso INDspIVA.EditValue > 0 Then
            Using modelIva As New MGeneralLedgerIVA(Me.Tag)
                Dim iva = modelIva.GetGeneralLedgerIVAById(INDspIVA.EditValue)
                INDtxtIvaValue.EditValue = Math.Round((INDtxtBaseIva.EditValue * iva.ObjectEmbbeded.Percentage) / 100, 2, MidpointRounding.AwayFromZero)
            End Using
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleGeneralExpense control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleGeneralExpense_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleGeneralExpense.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Me.OpenForm(1738, Nothing, True)
            _presenter.InitializeGeneralExpenseXpo()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al dar click en el boton del control de proveedor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSuppliersDistributionLines_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleSuppliersDistributionLines.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(558, Nothing, True)
            _presenter.InitializeSuppliersDistributionLinesXpo()
        End If
    End Sub

    Private Sub INDsleFilingUnit_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleFilingUnit.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("723", "", True)
            _presenter.InitializeFilingUnitXpo()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario correspondiente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSupplierType_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleSupplierType.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("726", "", True)
            If SupplierId > 0 Then
                INDsleSupplierType.SupplierId = SupplierId
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleProductionCenter control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleProductionCenter_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleProductionCenter.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Me.OpenForm("1201", Nothing, True)
            _presenter.InitializeCostDistributionBaseDetails()
        End If
    End Sub

    Private Sub INDsleMeasurementUnit_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleMeasurementUnit.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(IndigoSearchLookUpControl1.GetTagForm(INDsleMeasurementUnit), Nothing, True)
            _presenter.InitializeMeasurimentUnit(INDsleGeneralExpense.EditValue)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        Dim errors As New StringBuilder
        If INDtxtBaseIva.EditValue Is Nothing Then
            errors.AppendLine("Base de Iva Vacio")
        End If
        If INDspIVA.EditValue Is Nothing Then
            errors.AppendLine("Tipo Vacio")
        End If
        If INDtxtIvaValue.EditValue Is Nothing Then
            errors.AppendLine("Numero Vacio")
        End If

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Exit Sub
        End If
        Dim CostDistributionDirectCostIva As CostDistributionDirectCostIva
        If FlagEditIvaDetail Then
            CostDistributionDirectCostIva = TryCast(INDGvIVADetail.GetFocusedRow, CostDistributionDirectCostIva)
        Else
            CostDistributionDirectCostIva = New Domain.Entities.CostDistributionDirectCostIva
            _directCost.CostDistributionDirectCostIva.Add(CostDistributionDirectCostIva)
        End If

        With CostDistributionDirectCostIva
            .BaseIva = INDtxtBaseIva.EditValue
            .GeneralLedgerIvaId = INDspIVA.EditValue
            .IvaValue = INDtxtIvaValue.EditValue
            .Percentage = INDspIVA.Text
        End With

        INDGcIVADetail.DataSource = Nothing
        INDGcIVADetail.DataSource = _directCost.CostDistributionDirectCostIva
        INDGcIVADetail.RefreshDataSource()
        CleanControlsIvaDetails()
        CalculateValues()
        INDtxtBaseIva.Focus()
        Me.EnableOrDisableCurrencyControl()
    End Sub

    ''' <summary>
    ''' Evento para abrir el formulario de Personas y Terceros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleThirdParty_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleThirdParty.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(532, Nothing, True)
            ThirdPartyXpo = _presenter.GetAllThirdParty()
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Handles the Click event of the INDsbAddProductionCenter control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsbAddProductionCenter_Click(sender As Object, e As EventArgs) Handles INDsbAddProductionCenter.Click
        If ValidateControlsProductionCenter() Then
            Dim costCenterId As Integer? = Nothing
            Dim costCenterCodeName As String = Nothing
            Dim costDistributionBaseDetail = _presenter.GetCostDistributionBaseDetailById(INDsleProductionCenter.EditValue)
            Dim measureUnitId As Integer? = INDsleMeasurementUnit.EditValue
            Dim costValue As Decimal = CType(INDtxtCostValue.EditValue, Decimal)
            If costDistributionBaseDetail.CostCenterId IsNot Nothing Then
                costCenterId = costDistributionBaseDetail.CostCenterId.Id
                costCenterCodeName = costDistributionBaseDetail.CostCenterId.CodeName
            End If

            If Not ValidateProductionCenterInList(costDistributionBaseDetail.ProductionCenterId.Id, costDistributionBaseDetail.MainAccountId.Id, costCenterId, measureUnitId, costValue) Then
                Dim costCenterMessage As String = String.Empty
                Dim measureUnitMessage As String = String.Empty

                If costCenterId IsNot Nothing Then
                    costCenterMessage = " y el centro de costo " + costDistributionBaseDetail.CostCenterId.CodeName
                End If

                If measureUnitId IsNot Nothing Then
                    measureUnitMessage = " y la unidad de medida " + INDsleMeasurementUnit.Text
                End If

                Mensaje(EeventViewerImages.Advertencia) = "El centro de producción " + costDistributionBaseDetail.ProductionCenterId.CodeName + " con la cuenta contable " + costDistributionBaseDetail.MainAccountId.NumberName + costCenterMessage + measureUnitMessage + " ya existe con el mismo punto de valor en la lista"


                Exit Sub
            End If

            Dim _directExpenseDetail As New CostDistributionDirectCostDetail()
            With _directExpenseDetail
                .ProductionCenterId = costDistributionBaseDetail.ProductionCenterId.Id
                .ProductionCenterCodeName = costDistributionBaseDetail.ProductionCenterId.CodeName
                .MainAccountId = costDistributionBaseDetail.MainAccountId.Id
                .MainAccountCodeName = costDistributionBaseDetail.MainAccountId.NumberName
                .HandlesThirdParty = costDistributionBaseDetail.MainAccountId.HandlesThirdParty
                .CostCenterId = costCenterId
                .CostCenterCodeName = costCenterCodeName
                .MeasurementUnitId = CType(INDsleMeasurementUnit.EditValue, Integer)
                .MeasurementUnitCodeName = INDsleMeasurementUnit.Text
                .Count = CType(INDtxtMaximumAmount.EditValue, Decimal)
                .CostValue = CType(INDtxtCostValue.EditValue, Decimal)
                .Value = Utils.RoundValue((If(.Count, 0) * If(.CostValue, 0)), CType(INDSleRound.EditValue, Integer))
            End With

            AddDirectExpensesDetail(_directExpenseDetail)
        End If
    End Sub

#End Region

#Region "ItemClick"

    Private Sub MBtnAddCenter_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MBtnAddCenter.ItemClick
        If IsDirectDistribution(GeneralExpense) Then
            If MessageIndigo.Show(ResourceManager.GetString("LoadProductionCenter", MODULE_NAME), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                If _directCost.CostDistributionDirectCostDetail Is Nothing Then
                    _directCost.CostDistributionDirectCostDetail = New Domain.Entities.TrackableCollection(Of CostDistributionDirectCostDetail)
                End If

                For Each detail In GeneralExpense.CostDistributionBaseXpo(0).CostDistributionBaseDetailsXpo
                    Dim costCenterId As Integer? = Nothing
                    Dim costCenterCodeName As String = Nothing
                    If detail.CostCenterId IsNot Nothing Then
                        costCenterId = detail.CostCenterId.Id
                        costCenterCodeName = detail.CostCenterId.CodeName
                    End If

                    If Not ValidateProductionCenterInList(detail.ProductionCenterId.Id, detail.MainAccountId.Id, costCenterId, Nothing, 0) Then
                        Continue For
                    End If

                    Dim _directExpenseDetail As New CostDistributionDirectCostDetail()
                    With _directExpenseDetail
                        .ProductionCenterId = detail.ProductionCenterId.Id
                        .ProductionCenterCodeName = detail.ProductionCenterId.CodeName
                        .MainAccountId = detail.MainAccountId.Id
                        .MainAccountCodeName = detail.MainAccountId.NumberName
                        .HandlesThirdParty = detail.MainAccountId.HandlesThirdParty
                        .CostCenterId = costCenterId
                        .CostCenterCodeName = costCenterCodeName
                    End With

                    AddDirectExpensesDetail(_directExpenseDetail)
                Next
            End If
        End If
    End Sub

    Private Sub MBtnRemoveCenter_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MBtnRemoveCenter.ItemClick
        If MessageIndigo.Show(ResourceManager.GetString("DeleteProductionCenter", MODULE_NAME), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            If _directCost.CostDistributionDirectCostDetail IsNot Nothing AndAlso _directCost.CostDistributionDirectCostDetail.Count > 0 Then
                While _directCost.CostDistributionDirectCostDetail.Count > 0
                    _directCost.CostDistributionDirectCostDetail(_directCost.CostDistributionDirectCostDetail.Count - 1).MarkAsDeleted()
                End While
                ListDistributionDirectCostDetail = _directCost.CostDistributionDirectCostDetail.ToList()
                INDgcDirectCostDetail.RefreshDataSource()
                listErrorsImportFile = Nothing
            End If
        End If
    End Sub

#End Region

#Region "Actions"

    ''' <summary>
    ''' Handles the ButtonAction event of the IndigoGridView1_Click control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim _directCostDetail As CostDistributionDirectCostDetail
            If GeneralExpense?.GenerateAccountPayable Is Nothing OrElse GeneralExpense.GenerateAccountPayable Then
                _directCostDetail = CType(INDgvDirectCostDetail.GetFocusedRow(), CostDistributionDirectCostDetail)
            Else
                _directCostDetail = CType(INDgvNoGenerateCxP.GetFocusedRow(), CostDistributionDirectCostDetail)
            End If
            _directCostDetail.MarkAsDeleted()
            If _directCost.ChangeTracker.State <> ObjectState.Added Then
                _directCost.MarkAsModified()
            End If

            ListDistributionDirectCostDetail = _directCost.CostDistributionDirectCostDetail.ToList()
            INDgcDirectCostDetail.RefreshDataSource()

            CalculateBalance()
            CalculatePercentage()
        End If
    End Sub

    ''' <summary>
    ''' menu contextual y col acciones de rejilla detalle de iva
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView2_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView2.ContexMenuActions, IndigoGridView2.Click_ButtonAction
        Dim senderTag = sender.Tag.ToString
        If TypeOf sender Is DevExpress.XtraEditors.SimpleButton Then
            senderTag = DirectCast(sender, DevExpress.XtraEditors.SimpleButton).Tag.ToString
        End If

        Select Case (senderTag)
            Case "Edit"
                EditIvaDetail()
            Case "Remove"
                RemoveIvaDetailt()
        End Select
    End Sub

    ''' <summary>
    ''' menu contextual y columna acciones de rejilla provisiones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView3_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView3.ContexMenuActions, IndigoGridView3.Click_ButtonAction
        RemoveProvisionDocument()
    End Sub

#End Region

#Region "IdEntity"

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me._directCost IsNot Nothing AndAlso Me._directCost.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDbeCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbeCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

#End Region

#Region "DatasourceChanged"

    ''' <summary>
    ''' Handles the DataSourceChanged event of the INDgcDirectCostDetail control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDgcDirectCostDetail_DataSourceChanged(sender As Object, e As EventArgs) Handles INDgcDirectCostDetail.DataSourceChanged
        Dim status = ListDistributionDirectCostDetail IsNot Nothing AndAlso ListDistributionDirectCostDetail.Count > 0
        INDsleGeneralExpense.Properties.ReadOnly = status
    End Sub

#End Region

#Region "PasteToGrid"

    ''' <summary>
    ''' Evento que permite pegar en la rejilla de l segmento Información detallada
    ''' </summary>
    Private Async Sub IndigoGridControl1_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        If INDsleGeneralExpense.EditValue Is Nothing OrElse INDsleGeneralExpense.EditValue = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un elemento del costo"
            Exit Sub
        End If

        If GeneralExpense.GenerateAccountPayable IsNot Nothing AndAlso GeneralExpense.GenerateAccountPayable Then
            If Not IsDirectDistribution(GeneralExpense) Then
                Me.Mensaje(EeventViewerImages.Advertencia) = "El elemento del costo seleccionado no tiene una distribución directa"
                Exit Sub
            End If
        End If

        If e.Rows Is Nothing OrElse e.Rows.Count = 0 Then
            Me.Mensaje(EeventViewerImages.Advertencia) = "El formato de la estructura usada para importar los datos NO ES CORRECTO!!"
            Exit Sub
        End If

        Try
            AsyncLoader(True)
            INDgvDirectCostDetail.ShowLoadingPanel()
            INDgvNoGenerateCxP.ShowLoadingPanel()
            Me.Cursor = BaseClass.ChangeCursorIndigo()
            listErrorsImportFile = New List(Of String)

            Await ImportDetailsDistributionCosts(e.Rows)

            'si existen errores informamos al usurio y el proceso no continua
            If listErrorsImportFile.Any() Then
                Using formulario As New FrmListErrors(listErrorsImportFile)
                    formulario.StartPosition = FormStartPosition.CenterParent
                    Dim transparent As New FrmTransparent(formulario, False)
                    transparent.SafeInvoke(Sub(f) f.ShowDialog())
                End Using
            End If

        Catch ex As Exception
            Me.Mensaje(EeventViewerImages.Advertencia) = ex.Message
        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default
            INDgvDirectCostDetail.HideLoadingPanel()
            INDgvNoGenerateCxP.HideLoadingPanel()
            AsyncLoader(False)
        End Try
    End Sub

#End Region

#Region "Enter And Leave"

    Private Sub INDTxtValue_Enter(sender As Object, e As EventArgs) Handles INDtxtValue.Enter
        INDtxtValue_Active = True
    End Sub

    Private Sub INDTxtValue_Leave(sender As Object, e As EventArgs) Handles INDtxtValue.Leave
        If INDtxtValue_Active Then
            INDtxtValue_Active = False
            If Value <> ValueTemp Then
                CalculateValues()
            End If
        End If
    End Sub

#End Region

#End Region

#Region "Methods"

#Region "Importar Provisiones"

    ''' <summary>
    ''' Evento que dispara el formulario de importar documentos de provision
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ImportarInformacion() Handles BarraBotones.Click_ImportarInformacion
        Using formulario As New FrmImportProvisionDocument
            AddHandler formulario.GetListProvisionDocument, AddressOf ReturnGetListProvisionDocument
            Me.Cursor = ChangeCursorIndigo()
            formulario.Size = New System.Drawing.Size(800, 700)
            formulario.StartPosition = FormStartPosition.CenterParent
            formulario.SupplierId = SupplierId
            formulario.GeneralExpenseId = GeneralExpenseId
            formulario.ListProvisionDocumentValidation = _directCost.CostDistributionDirectCostLegalizedDocuments.ToList()
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Método que retorna los registros importados al formulario de agregar provisiones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnGetListProvisionDocument(sender As Object, e As GetListProvisionDocumentEventArgs)

        For Each pd In e.ListProvisionDocument
            _directCost.CostDistributionDirectCostLegalizedDocuments.Add(pd)
        Next

        INDGcProvisionDocuments.RefreshDataSource()
        INDGcProvisionDocuments.DataSource = _directCost.CostDistributionDirectCostLegalizedDocuments.ToList()
    End Sub

#End Region

    Private Sub InitializeTuples()
        'Redondeo
        ListRoundService = New List(Of Tuple(Of Integer, String))
        ListRoundService.Add(New Tuple(Of Integer, String)(0, "Ninguno"))
        ListRoundService.Add(New Tuple(Of Integer, String)(10, "A la Décima"))
        ListRoundService.Add(New Tuple(Of Integer, String)(100, "A la Centésima"))
        ListRoundService.Add(New Tuple(Of Integer, String)(1000, "A la Milésima"))
        INDSleRound.Properties.DataSource = ListRoundService.ToList
    End Sub

    ''' <summary>
    ''' Cargamos los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "4", .StatusName = ResourceManager.GetString("StatusReverse"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "5", .StatusName = ResourceManager.GetString("ConfirmedNotlegalized"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(0, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "6", .StatusName = ResourceManager.GetString("ConfirmedLegalized"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(193, Byte), Integer), CType(CType(79, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    Private Sub SetActionsGrid()
        Dim _listActions As New List(Of eAcciones)()
        _listActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDgvDirectCostDetail, _listActions)
        For Each col As GridColumn In INDgvDirectCostDetail.Columns
            If col.Name = "colActions" Then
                _colActions = col
                col.Width = 80
                col.OptionsColumn.FixedWidth = True
            End If
        Next

        IndigoGridView3.SetListAcction(INDGvProvision, _listActions)

        For Each col As GridColumn In INDGvProvision.Columns
            If col.Name = "colActions" Then
                _colProvisionActions = col
                col.Width = 80
                col.OptionsColumn.FixedWidth = True
            End If
        Next

        IndigoGridView1.SetListAcction(INDgvNoGenerateCxP, _listActions)
        For Each col As GridColumn In INDgvNoGenerateCxP.Columns
            If col.Name = "colActions" Then
                _colProvisionActions = col
                col.Width = 80
                col.OptionsColumn.FixedWidth = True
            End If
        Next
    End Sub

    ''' <summary>
    ''' Lanza el calculo de distribución
    ''' </summary>
    Private Async Sub RaiseCalcDistribution()
        If GeneralExpense IsNot Nothing AndAlso (GeneralExpense.GenerateAccountPayable Is Nothing OrElse GeneralExpense.GenerateAccountPayable) Then
            If Not IsDirectDistribution(GeneralExpense) OrElse GeneralExpense.DistributionType <> 1 Then
                If (Me.Value > 0 AndAlso Me.Value <> ValueTemp) OrElse Me.TaxRegistration = 2 Then
                    Try
                        Using model As New MCostDistributionDirectCost(Me.MyTag)
                            AsyncLoader(True)
                            Dim valueValidation = Me.Value
                            If TaxRegistration = 2 Then
                                If _directCost.CostDistributionDirectCostIva.Count > 0 Then
                                    Dim ivaTotal = _directCost.CostDistributionDirectCostIva.Sum(Function(x) x.IvaValue)
                                    valueValidation -= ivaTotal
                                End If
                            End If
                            Dim res = Await model.CalcDistribution(GeneralExpenseId, valueValidation, Year, Month, indigo.InteropCostContainer)
                            If Not res.StateResult Then 'Error
                                AsyncLoader(False)
                                Me.Mensaje(EeventViewerImages.Advertencia) = res.Message
                                Exit Sub
                            Else
                                ' Antes de cada ejecución del evento, se limpia el objeto para solo mantener los últimos cálculos
                                If res.ObjectEmbbeded.Count > 0 Then ' Solamente si se encuentran resultados del Cálculo de Distribución
                                    _directCost.CostDistributionDirectCostDetail.Clear()
                                End If
                                For Each item As CostDistributionDirectCostDetail In res.ObjectEmbbeded
                                    _directCost.CostDistributionDirectCostDetail.Add(item)
                                Next

                                ListDistributionDirectCostDetail = _directCost.CostDistributionDirectCostDetail.ToList()
                                If TaxRegistration <> 2 Then
                                    Dim IvaTotal = _directCost.CostDistributionDirectCostIva.Sum(Function(x) x.IvaValue)
                                    For Each item In (From x In _directCost.CostDistributionDirectCostDetail).ToList()
                                        item.IvaValue = Math.Round((IvaTotal * If(item.Percentage, 0)) / 100, 2, MidpointRounding.AwayFromZero)
                                        item.BaseValue = item.Value - item.IvaValue
                                    Next
                                End If

                                INDgcDirectCostDetail.RefreshDataSource()
                                _tmpCurrentPeriod.Value = Me.Value
                                ValueTemp = Value

                                If TaxRegistration = 2 Then
                                    If _directCost.CostDistributionDirectCostIva.Count > 0 Then
                                        Dim ivaTotal = _directCost.CostDistributionDirectCostIva.Sum(Function(x) x.IvaValue)
                                        _tmpCurrentPeriod.Value = valueValidation - ivaTotal
                                        ValueTemp = Value - ivaTotal
                                    End If
                                End If

                                CalculateBalance()
                            End If
                            AsyncLoader(False)
                        End Using
                    Catch ex As Exception
                        AsyncLoader(False)
                        Mensaje(EeventViewerImages.Advertencia) = ex.Message
                    End Try
                ElseIf Me.Value = 0 Then
                    _tmpCurrentPeriod.Value = 0
                    ValueTemp = 0
                    CalculateBalance()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Valida si el elemento del costo es de tipo distribución directa
    ''' </summary>
    ''' <param name="generalExpense">Elemento del costo</param>
    ''' <returns>Valor que indica si es de tipo directo</returns>
    Private Function IsDirectDistribution(ByVal generalExpense As CostGeneralExpenseXpo) As Boolean
        If generalExpense IsNot Nothing AndAlso generalExpense.CostDistributionBaseXpo.Count = 1 AndAlso generalExpense.CostDistributionBaseXpo(0).DistributionType = 1 Then
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' Nuevo Distribucion de gastos directos
    ''' </summary>
    Private Async Sub NewDirectCost()
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If _settingsCost Is Nothing OrElse _settingsCost.Id = 0 Then
            Exit Sub
        End If
        Me._directCost = New CostDistributionDirectCost() With {.Status = 1}
        If Me._sequence.IsManual Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.CostSecuenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me.Sequence.CostSecuenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
                    Me._idCurrentSequence = Me._sequence.CostSecuenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).FirstOrDefault().Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Sub
                End If
            End If
            If Not Me._sequence.Sequential Then
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense.ContainsKey(CInt(Me._idCurrentSequence)) = True AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(Me._idCurrentSequence)(0)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        Using model As New MCommonCost(Me.Tag)
                            Me.DicSequense(Me._idCurrentSequence) = Await model.GetNumericSequenseGroup(Me._idCurrentSequence)
                        End Using
                        If Me.DicSequense(Me._idCurrentSequence) IsNot Nothing AndAlso Me.DicSequense(Me._idCurrentSequence).Count > 0 Then
                            Me.Code = Me.DicSequense(Me._idCurrentSequence)(0)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                            Exit Sub
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            Else
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            End If
        End If

        Me.ActionsOnControls = True
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    Public Async Sub DeleteBlockedRecord()
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using ModelCommonInteropCost As New MCommonCost(Me.Tag)
                Await ModelCommonInteropCost.DeleteBlockRecordCost(_record)
                _record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' valida los controles de centros de producción
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControlsProductionCenter() As Boolean
        Dim errorList As New StringBuilder()

        If CType(INDsleProductionCenter.EditValue, Integer) = 0 Then
            errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), ResourceManager.GetString("ProductionCenter", MODULE_NAME)))
        End If
        If CType(INDsleMeasurementUnit.EditValue, Decimal) = 0 Then
            errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Unidad de Medida"))
        End If
        If INDtxtMaximumAmount.EditValue Is Nothing Then
            errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Cantidad"))
        End If
        If INDtxtCostValue.EditValue Is Nothing Then
            errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Punto de Valor"))
        End If

        If errorList.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errorList.ToString()
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Valida que el centro de produccion a agregar no esté ya agregado
    ''' </summary>
    Private Function ValidateProductionCenterInList(ProductionCenterId As Integer, MainAccountId As Integer, CostCenterId As Integer?, MeasureUnitId As Integer?, CostValue As Decimal) As Boolean
        If ListDistributionDirectCostDetail IsNot Nothing AndAlso ListDistributionDirectCostDetail.Count > 0 Then
            Return Not ListDistributionDirectCostDetail.Any(Function(x) x.ProductionCenterId = ProductionCenterId AndAlso x.MainAccountId = MainAccountId AndAlso x.CostCenterId.Equals(CostCenterId) AndAlso x.MeasurementUnitId.Equals(MeasureUnitId) AndAlso x.CostValue = CostValue)
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' Limpia los controles del detalle del gasto directo
    ''' </summary>
    Private Sub CleanControlsDetail()
        INDpceAddDetail.SafeInvoke(Sub()
                                       INDsleProductionCenter.EditValue = Nothing
                                       INDtxtMaximumAmount.EditValue = 0
                                       INDsleMeasurementUnit.EditValue = Nothing
                                       Me.INDtxtCostValue.EditValue = Nothing
                                       INDsleProductionCenter.Focus()
                                   End Sub)
    End Sub

    ''' <summary>
    ''' Generates the document.
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._directCost.Code, Me._directCost.Description),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me._directCost.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._directCost.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._directCost.Code, Me._directCost.Description)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._directCost.Code)
        End If
        Return Me._doc
    End Function

    ''' <summary>
    ''' Asigna los valores a los campos de la entidad
    ''' </summary>
    Public Sub AssigningValues()
        With _directCost
            .OperatingUnitId = Me._idOperativeUnit
            .Year = Year
            .Month = Month
            .Code = Code
            .ProvisionDocument = ProvisionDocument
            .GeneralExpenseId = GeneralExpenseId
            .SuppliersDistributionLinesId = SuppliersDistributionLinesId
            .SupplierId = SupplierId
            .PositionId = PositionId
            .ThirdPartyId = ThirdPartyId
            .AccountPayableId = AccountPayableId
            .Value = Math.Round(Value, 2, MidpointRounding.AwayFromZero)
            .Description = String.Format("{0} - {1}", GeneralExpense.Code, GeneralExpense.Name)
            .Observation = Observation
            .DeductibleIva = DeductibleIVA
            .TaxRegistration = TaxRegistration
            .CurrencyId = Me.CurrencyId
            .AccountPayableSameSupplier = AccountPayableSameSupplier
            .DocumentDate = DocumentDate
            .TaxRegistration = TaxRegistration
            If Me._settingsCost.AccountingCosts Then
                .BillNumber = BillNumber
                .BillDate = BillDate
                .Term = Term
                .Hours = Hours
                .MainAccountId = MainAccountId
                .CostCenterId = CostCenterId
                .ServicePeriodDate = ServicePeriodDate
                .FilingUnitId = FilingUnitId
                .SupplierTypeId = SupplierTypeId
            Else
                .BillNumber = INDsleAccountPayable.TextEditValue
            End If

            If ListDelete IsNot Nothing AndAlso ListDelete.Count > 0 Then
                ListDelete.ForEach(Sub(x) .CostDistributionDirectCostDetail.Add(x.MarkAsDeleted()))
            End If

            While .CostDistributionDirectCostDetail.Where(Function(d) d.Value = 0).Count > 0
                Dim _detail = .CostDistributionDirectCostDetail.First(Function(d) d.Value = 0)
                _detail.MarkAsDeleted()
                .CostDistributionDirectCostDetail.Remove(_detail)
            End While

            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Public Sub CleanControls()
        INDlcRoot.BeginUpdate()

        Me._doc = Nothing
        DeleteBlockedRecord()
        ReadOnlyControls(False)
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True

        Year = If(_settingsCost IsNot Nothing, _settingsCost.Year, 0)
        Month = If(_settingsCost IsNot Nothing, _settingsCost.Month, 0)
        Code = String.Empty
        ProvisionDocument = Nothing

        GeneralExpenseId = Nothing
        INDsleGeneralExpense.Properties.NullText = String.Empty
        SuppliersDistributionLinesId = Nothing
        INDsleSuppliersDistributionLines.Properties.NullText = String.Empty
        Observation = String.Empty
        SupplierTypeId = Nothing
        INDsleSupplierType.SupplierId = Nothing
        INDsleSupplierType.DisplayNullText = String.Empty

        BillDate = Nothing
        Term = Nothing
        Hours = Nothing
        ServicePeriodDate = Nothing
        FilingUnitId = Nothing
        INDsleFilingUnit.Properties.NullText = String.Empty

        Status = Nothing
        CostDistributionBaseDetailsXpo = Nothing
        MeasurementUnitXpo = Nothing
        AccountPayableXpo = Nothing
        RateIvaXpo = Nothing

        INDpceAddDetail.Enabled = False
        INDebtDistribution.Enabled = False
        INDBtnImportFile.Enabled = False

        _directCost = Nothing
        ListDistributionDirectCostDetail = Nothing
        ListDelete = Nothing

        INDGcIVADetail.DataSource = Nothing
        INDGcIVADetail.RefreshDataSource()
        INDGcProvisionDocuments.DataSource = Nothing
        INDGcProvisionDocuments.RefreshDataSource()
        DeductibleIVA = False
        TaxRegistration = Nothing
        INDLyDeductibleIva.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlcgIVADetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        DocumentDate = Nothing
        ThirdPartyId = Nothing
        Value = Nothing

        AccountPayableSameSupplier = False
        INDLyAccountPayableSameSupplier.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        LyGroupProvisionDocuments.HideControl

        Me.CurrencyId(indigo.CurrencyISO4217) = indigo.OfficialCurrencyId
        Me.EnableOrDisableCurrencyControl()

        ActionsOnControls = False
        INDlcgBillData.Visibility = If(Me._settingsCost.AccountingCosts, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.Anular, "Anular")

        ValueTemp = 0D
        _tmpCurrentPeriod.Value = 0D
        _tmpCurrentPeriod.Balance = 0D
        _tmpCurrentPeriod.LoadDate()
        IndigoGridView1.RaiseMenuPopUp = True

        If FormSearchObjects Is Nothing OrElse FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        End If
        GenerateAccountPayableFields()
        INDlcRoot.EndUpdate()
    End Sub

    ''' <summary>
    ''' Carga los datos en los controles
    ''' </summary>
    Public Async Sub LoadControls()
        Try
            If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
                If Me.BarraBotones.PermiteConsultar = False Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                    Exit Sub
                End If

                Using model = New MCostDistributionDirectCost(Me.Tag)
                    AsyncLoader(True)
                    Dim res = Await model.GetDistributionDirectCost(Me.Code)
                    If res.StatusCode <> eStatusResult.SUCCESS Then
                        Mensaje(EeventViewerImages.Advertencia) = res.Message
                        Exit Sub
                    End If
                    _directCost = res.ObjectEmbbeded
                End Using

                If _directCost IsNot Nothing AndAlso _directCost.Id > 0 Then
                    INDlcRoot.BeginUpdate()
                    Try
                        Using ModelCommonTreasury As New MCommonCost(Me.Tag)
                            With _directCost
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)

                                _isLoadingControls = True
                                Code = .Code
                                Year = .Year
                                Month = .Month
                                GeneralExpenseId = .GeneralExpenseId
                                ProvisionDocument = .ProvisionDocument
                                INDsleGeneralExpense.Properties.NullText = _directCost.FullNameGeneralExpense
                                SuppliersDistributionLinesId = .SuppliersDistributionLinesId
                                INDsleSuppliersDistributionLines.Properties.NullText = .ThirdPartyDescription
                                DistributionLineCodeName = .DistributionLineCodeName
                                PositionCodeName = .PositionCodeName
                                SupplierId = .SupplierId
                                PositionId = .PositionId
                                ThirdPartyId = .ThirdPartyId
                                AccountPayableId = .AccountPayableId
                                INDsleAccountPayable.DisplayNullText = .BillNumber
                                Me.CurrencyId(.CurrencyAbbreviation) = .CurrencyId
                                If Me._settingsCost.AccountingCosts Then
                                    BillNumber = .BillNumber
                                    BillDate = .BillDate
                                    Term = .Term
                                    Hours = .Hours
                                    MainAccountId = .MainAccountId
                                    INDsleMainAccount.Properties.NullText = .MainAccountCodeName
                                    If .CostCenterId IsNot Nothing Then
                                        CostCenterId = .CostCenterId
                                        INDsleCostCenter.Properties.NullText = .CostCenterCodeName
                                    End If
                                    ServicePeriodDate = .ServicePeriodDate
                                    FilingUnitId = .FilingUnitId
                                    INDsleFilingUnit.Properties.NullValuePrompt = .FilingUnitCodeName

                                    If .AccountPayableId IsNot Nothing Then
                                        INDsleSuppliersDistributionLines.Properties.ReadOnly = True
                                        INDtxtBillNumber.Properties.ReadOnly = True
                                        INDdeBillDate.Properties.ReadOnly = True
                                        INDseTerm.Properties.ReadOnly = True
                                        INDseHours.Properties.ReadOnly = True
                                        INDsleMainAccount.Properties.ReadOnly = True
                                        INDsleCostCenter.Properties.ReadOnly = True
                                        INDdeServicePeriodDate.Properties.ReadOnly = True
                                        INDsleFilingUnit.Properties.ReadOnly = True
                                        INDsleSupplierType.Properties.ReadOnly = True
                                        INDRgAccountPayableSameSupplier.ReadOnly = True
                                    End If
                                End If

                                Value = .Value
                                ValueTemp = .Value
                                Observation = .Observation
                                INDdeServicePeriodDate.EditValue = .ServicePeriodDate
                                FilingUnitId = .FilingUnitId
                                INDsleFilingUnit.Properties.NullValuePrompt = .FilingUnitCodeName
                                SupplierTypeId = .SupplierTypeId
                                INDsleSupplierType.SupplierId = SupplierId
                                INDsleSupplierType.DisplayNullText = .SupplierTypeCodeName
                                AccountPayableSameSupplier = .AccountPayableSameSupplier
                                ListDistributionDirectCostDetail = _directCost.CostDistributionDirectCostDetail.ToList()
                                INDGcProvisionDocuments.DataSource = _directCost.CostDistributionDirectCostLegalizedDocuments.ToList()
                                TaxRegistration = .TaxRegistration
                                DocumentDate = .DocumentDate

                                Dim resultTaxRegistration = Await VerifyTaxRegistration()

                                If {2, 3}.Contains(.Status) AndAlso .DeductibleIva IsNot Nothing Then
                                    DeductibleIVA = .DeductibleIva
                                    INDlcgIVADetails.HideControl(_directCost?.CostDistributionDirectCostIva Is Nothing OrElse Not _directCost?.CostDistributionDirectCostIva?.Any())
                                    CalculateValueWithIva()

                                ElseIf .SupplierId > 0 Then
                                    If Not ProvisionDocument Then
                                        ''Validacion para identificar si tiene iva descontable
                                        Using modelThird As New MCostDistributionDirectCost(Tag)
                                            Dim Third = modelThird.GetThirdPartyByIdSupplier(Convert.ToInt16(.SupplierId))
                                            If Third.PersonType = 1 AndAlso Third.ContributionType = 0 Then
                                                INDLyDeductibleIva.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                                                INDlcgIVADetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                                            Else
                                                INDlcgIVADetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                                                If resultTaxRegistration Then
                                                    INDLyDeductibleIva.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                                                    DeductibleIVA = .DeductibleIva
                                                End If
                                            End If
                                        End Using
                                    End If
                                End If

                                INDGcIVADetail.DataSource = _directCost.CostDistributionDirectCostIva.ToList()
                                Status = .Status.ToString()
                                _isLoadingControls = False
                            End With

                            _tmpCurrentPeriod.LoadDate()

                            Dim result = Await ModelCommonTreasury.GetBlockRecordCostByIdformAndIdRecord(Me.Tag, _directCost.Id)
                            If result.Id = 0 Then
                                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                                state.State = Domain.Base.Entities.ObjectState.Added
                                _record = New BlockRecordCost With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _directCost.Id}
                                Dim operation = Await ModelCommonTreasury.SaveBlockRecordCost(_record)
                                _record = operation.ObjectEmbbeded
                            Else
                                _record = result
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                            End If

                            Me.GetDocumentIndexed(Me.Tag & "_" & Me._directCost.Code)
                            Me.BarraBotones.SetDocuments(_directCost.Id, MyTag, Nothing, GetType(DistributionDirectCost).Name)

                            If _directCost.Status = 1 Then
                                IndigoGridView1.RaiseMenuPopUp = True
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
								BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = False
								If Not ProvisionDocument AndAlso GeneralExpenseId <> 0 Then
									Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
								End If
							ElseIf (_directCost.Status = 5 And ProvisionDocument) Or (_directCost.Status = 2 And Not GeneralExpense.GenerateAccountPayable) Then
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = False
                                BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.Anular, "Reversar")
                            Else
                                IndigoGridView1.RaiseMenuPopUp = False
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyDisconfirm)
                                INDsleSuppliersDistributionLines.Enabled = False
                                INDsleAccountPayable.Enabled = False

                                ReadOnlyControls(True)
                            End If

                            If Year < _settingsCost.Year OrElse (Year = _settingsCost.Year AndAlso Month < _settingsCost.Month) Then
                                Dim dtfi = indigo.Culture.DateTimeFormat
                                Dim xtraMessage As String = String.Format(ResourceManager.GetString("RegisterNotInPeriod", MODULE_NAME), Microsoft.VisualBasic.Strings.StrConv(dtfi.GetMonthName(_settingsCost.Month), Microsoft.VisualBasic.VbStrConv.ProperCase), _settingsCost.Year)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, "")
                                ReadOnlyControls(True)
                            End If

                            ActionsOnControls = True
                            Me.EnableOrDisableCurrencyControl()
                            INDbeCode.Enabled = False

                            If {2, 6}.Contains(_directCost.Status) Then
                                INDsleSuppliersDistributionLines.Enabled = False
                                INDsleAccountPayable.Enabled = False
                                INDRgDeductibleIva.Enabled = False
                                INDRgProvisionDocument.Enabled = False
                            End If
                        End Using

                        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                        BarraBotones.PrintReport(PrintReportAction.None, _directCost.Id, 0, _directCost.Id, _idOperativeUnit)
                        INDlciAccountPayable.Visibility = If(Me._settingsCost.AccountingCosts, DevExpress.XtraLayout.Utils.LayoutVisibility.Never, DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                        INDlciPosition.Visibility = If(PositionId Is Nothing, DevExpress.XtraLayout.Utils.LayoutVisibility.Never, DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                        INDlciHours.Visibility = If(Me._settingsCost.AccountingCosts AndAlso PositionId IsNot Nothing, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
                        INDlciCostCenter.Visibility = If(Me._settingsCost.AccountingCosts AndAlso CostCenterId IsNot Nothing, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
                    Catch ex As Exception
                        Me.Mensaje(EeventViewerImages.Advertencia) = ex.Message
                    End Try
                    INDlcRoot.EndUpdate()
                Else
                    If Me._sequence.IsManual Then
                        Me.NewDirectCost()
                    Else
                        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                        Deshacer()
                    End If
                End If
            End If
        Catch ex As Exception
            Me.Mensaje(EeventViewerImages.Advertencia) = ex.Message
        Finally
            AsyncLoader(False)
        End Try
    End Sub

    Private Function ValidateFormControls() As Boolean
        Dim errorList As New StringBuilder()

        If GeneralExpenseId = 0 Then
            errorList.AppendLine("Elemento del Costo")
        End If

        If GeneralExpense.GenerateAccountPayable Is Nothing OrElse GeneralExpense.GenerateAccountPayable Then
            If SuppliersDistributionLinesId = 0 OrElse SupplierId = 0 OrElse ThirdPartyId = 0 Then
                errorList.AppendLine("Proveedor")
            End If
        Else
            If ThirdPartyId = 0 Then
                errorList.AppendLine("Tercero")
            End If
            If DocumentDate Is Nothing Then
                errorList.AppendLine("Fecha del documento")
            End If
            Dim _balance = _directCost.CostDistributionDirectCostDetail _
            .Sum(Function(o) If(o.Nature = 1, o.Value, If(o.Nature = 2, -o.Value, 0)))
            If _balance <> 0 Then
                errorList.AppendLine("Los detalles Débito/Crédito balanceados")
            End If
        End If

        If Not Me._settingsCost.AccountingCosts Then
            If AccountPayableId Is Nothing Then
                errorList.AppendLine("Factura")
            End If
        End If

        If Value = 0 Then
            errorList.AppendLine("Valor")
        End If
        If String.IsNullOrEmpty(Observation) Then
            errorList.AppendLine("Observación")
        End If

        If Me._settingsCost.AccountingCosts Then

            If GeneralExpense.GenerateAccountPayable Is Nothing Or GeneralExpense.GenerateAccountPayable Then
                If MainAccountId Is Nothing Then
                    errorList.AppendLine("Cuenta Contable")
                End If

                If SupplierTypeId Is Nothing Then
                    errorList.AppendLine("Tipo de Proveedor")
                End If
            End If

            If GeneralExpense.GenerateAccountPayable Is Nothing Or GeneralExpense.GenerateAccountPayable Then
                If Not ProvisionDocument Then
                    If String.IsNullOrEmpty(BillNumber) Then
                        errorList.AppendLine("Factura")
                    End If

                    If BillDate Is Nothing Then
                        errorList.AppendLine("Fecha Factura")
                    End If

                    If Term Is Nothing Then
                        errorList.AppendLine("Plazo")
                    End If

                    If PositionId IsNot Nothing AndAlso Hours <= 0 Then
                        errorList.AppendLine("Horas")
                    End If

                    If INDlciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always AndAlso CostCenterId Is Nothing Then
                        errorList.AppendLine("Centro de Costo")
                    End If

                    If ServicePeriodDate Is Nothing Then
                        errorList.AppendLine("Fecha Radicación")
                    End If

                    If FilingUnitId Is Nothing Then
                        errorList.AppendLine("Unidad Radicación")
                    End If
                End If
            End If

        End If

        If INDgvDirectCostDetail.RowCount = 0 And INDgvNoGenerateCxP.RowCount = 0 Then
            errorList.AppendLine("Se debe agregar mínimo una información detallada")
        Else
            If GeneralExpense.DistributionType = 2 OrElse GeneralExpense.DistributionType = 3 Then
                For Each item In (From x In _directCost.CostDistributionDirectCostDetail Where x.HandlesThirdParty AndAlso Not x.ThirdPartyId.HasValue).ToList()
                    errorList.AppendLine(String.Format("El detalle con cuenta contable {0} debe tener asignado un tercero", item.MainAccountCodeName))
                Next
            End If
        End If

        If errorList.Length > 0 Then
            errorList.Insert(0, "Se requiere lo siguiente: " & Environment.NewLine)
            Mensaje(EeventViewerImages.Advertencia) = errorList.ToString()
            Return False
        End If

        Return True
    End Function

    Private Function LoadReport() As Task
        Return Task.Factory.StartNew(Sub()
                                         Me.BarraBotones.SafeInvoke(Sub(x) x.PrintReport(PrintReportAction.None, _directCost.Id, 0, _directCost.Id, _idOperativeUnit))
                                     End Sub)
    End Function

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir del abarra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, _directCost.Id, 0, _directCost.Id, _idOperativeUnit)
    End Sub

    ''' <summary>
    ''' Adds the direct expenses detail.
    ''' </summary>
    Private Sub AddDirectExpensesDetail(ByVal _directExpenseDetail As CostDistributionDirectCostDetail)

        _directCost.CostDistributionDirectCostDetail.Add(_directExpenseDetail)
        ListDistributionDirectCostDetail = _directCost.CostDistributionDirectCostDetail.ToList()
        INDgcDirectCostDetail.RefreshDataSource()

        CalculateBalance()
        CalculatePercentage()
        CleanControlsDetail()
        RaiseCalcDistribution()
    End Sub

    ''' <summary>
    ''' Calcula el total de la distribucion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CalculateBalance()
        Dim sum As Decimal = 0D 'Variable que guarda la suma de los detalles
        If _directCost?.CostDistributionDirectCostDetail?.Any() Then
            sum = _directCost.CostDistributionDirectCostDetail.Sum(Function(o) o.Value)
        End If

        If GeneralExpense?.GenerateAccountPayable Is Nothing Or GeneralExpense?.GenerateAccountPayable Then
            Dim sumBalance As Decimal = 0D 'Variable que guarda el saldo
            If _tmpCurrentPeriod.Value > 0 AndAlso sum <= _tmpCurrentPeriod.Value Then
                sumBalance = _tmpCurrentPeriod.Value - sum
                If TaxRegistration = 2 And sumBalance > 0 Then
                    If _directCost.CostDistributionDirectCostIva.Any() Then
                        sumBalance -= _directCost.CostDistributionDirectCostIva.Sum(Function(x) x.IvaValue)
                    End If
                End If
            End If

            _tmpCurrentPeriod.Balance = sumBalance
        Else
            INDtxtValue.SafeInvoke(Sub(x)
                                       Value = sum
                                       _tmpCurrentPeriod.Value = sum
                                       _tmpCurrentPeriod.Balance = 0D
                                   End Sub)
        End If
    End Sub

    ''' <summary>
    ''' Calcula los porcentajes de cada item
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CalculatePercentage()
        If GeneralExpense.GenerateAccountPayable Is Nothing OrElse GeneralExpense.GenerateAccountPayable Then
            If ListDistributionDirectCostDetail IsNot Nothing AndAlso ListDistributionDirectCostDetail.Count > 0 Then
                Dim totalValue As Decimal = Value
                Dim sumTotalValue As Decimal = (From x In ListDistributionDirectCostDetail Select x.Value).Sum()
                Dim percentageTmp As Decimal = 0.00
                Dim valueTmp As Decimal = 0.00

                For Each item In ListDistributionDirectCostDetail
                    If totalValue > 0 Then
                        If item.Percentage Is Nothing Or item.Percentage = 0 Then
                            ' Calcular porcentaje y truncar a 2 decimales
                            item.Percentage = BaseClass.TruncateDecimal(((item.Value * 100) / totalValue), 2)
                        End If
                        ' Acumular totales
                        percentageTmp += item.Percentage
                        valueTmp += item.Value
                    Else
                        ' Si el valor es 0, el porcentaje es 0
                        item.Percentage = 0
                    End If
                Next

                If percentageTmp > 100 AndAlso sumTotalValue >= totalValue Then
                    ListDistributionDirectCostDetail.Item(ListDistributionDirectCostDetail.Count - 1).Percentage = ListDistributionDirectCostDetail.Item(ListDistributionDirectCostDetail.Count - 1).Percentage - (percentageTmp - 100)
                    percentageTmp -= (percentageTmp - 100)
                ElseIf percentageTmp < 100 AndAlso sumTotalValue >= totalValue Then
                    ListDistributionDirectCostDetail.Item(ListDistributionDirectCostDetail.Count - 1).Percentage = ListDistributionDirectCostDetail.Item(ListDistributionDirectCostDetail.Count - 1).Percentage + (100 - percentageTmp)
                    percentageTmp += (100 - percentageTmp)
                End If
                If percentageTmp <> 100 AndAlso sumTotalValue > totalValue Then 'Se debe corregir también el valor asi como el porcentaje por petición de Christian Salazar
                    'Se organiza el listado de mayor a menor según el valor
                    ListDistributionDirectCostDetail = (From x In ListDistributionDirectCostDetail Order By x.Value Descending Select x).ToList()
                    'Se saca el primer valor y se le resta la diferencia
                    ListDistributionDirectCostDetail(0).Value -= (sumTotalValue - totalValue)
                End If

                If Not IsDirectDistribution(GeneralExpense) Then
                    If Math.Round(_tmpCurrentPeriod.Balance, 0) = 0 AndAlso valueTmp <> Value AndAlso percentageTmp <> 100 AndAlso valueTmp < Value Then
                        ListDistributionDirectCostDetail.Item(ListDistributionDirectCostDetail.Count - 1).Value = ListDistributionDirectCostDetail.Item(ListDistributionDirectCostDetail.Count - 1).Value + (Value - valueTmp)
                        ListDistributionDirectCostDetail.Item(ListDistributionDirectCostDetail.Count - 1).Percentage = ListDistributionDirectCostDetail.Item(ListDistributionDirectCostDetail.Count - 1).Percentage + (100 - percentageTmp)
                    End If
                End If

                INDgcDirectCostDetail.RefreshDataSource()
            End If
        End If
    End Sub
    ''' <summary>
    ''' Método para cargar los detalles que se van a distribuir
    ''' </summary>
    Private Sub CalculateValues()
        If GeneralExpense?.GenerateAccountPayable Is Nothing OrElse GeneralExpense.GenerateAccountPayable Then
            If Value <> ValueTemp AndAlso INDpceAddDetail.Enabled = False Then
                If Not _isLoadingControls Then
                    If _directCost IsNot Nothing AndAlso _directCost.Id > 0 AndAlso _directCost.CostDistributionDirectCostDetail IsNot Nothing AndAlso _directCost.CostDistributionDirectCostDetail.Count > 0 Then
                        If ListDelete Is Nothing Then
                            ListDelete = New List(Of CostDistributionDirectCostDetail)
                        End If
                        For Each item In (From x In _directCost.CostDistributionDirectCostDetail Where x.Id > 0).ToList()
                            ListDelete.Add(item)
                        Next
                    End If
                    If _directCost IsNot Nothing Then
                        ListDistributionDirectCostDetail = _directCost.CostDistributionDirectCostDetail.ToList()
                        INDgcDirectCostDetail.RefreshDataSource()
                        RaiseCalcDistribution()
                    End If
                End If

                _tmpCurrentPeriod.Value = Me.Value
                CalculateBalance()

                If IsDirectDistribution(GeneralExpense) Then
                    CalculatePercentage()
                End If

            Else
                If TaxRegistration <> 2 Then
                    If _directCost IsNot Nothing Then
                        Dim IvaTotal = Math.Round(_directCost.CostDistributionDirectCostIva.Sum(Function(x) x.IvaValue), 2, MidpointRounding.AwayFromZero)
                        For Each item In (From x In _directCost.CostDistributionDirectCostDetail).ToList()
                            item.IvaValue = Math.Round(IvaTotal * (If(item.Percentage, 0)) / 100, 2, MidpointRounding.AwayFromZero)
                            item.BaseValue = item.Value - item.IvaValue
                        Next

                        INDgcDirectCostDetail.RefreshDataSource()
                        _tmpCurrentPeriod.Value = Me.Value
                        ValueTemp = Value
                        CalculateBalance()
                    End If
                Else
                    If _directCost IsNot Nothing Then
                        ListDistributionDirectCostDetail = _directCost.CostDistributionDirectCostDetail.ToList()
                        INDgcDirectCostDetail.RefreshDataSource()
                        RaiseCalcDistribution()
                    End If
                End If
            End If
        End If
    End Sub

    Private Function CalculateValueWithIva()
        If TaxRegistration <> 2 Then
            Dim IvaTotal = _directCost.CostDistributionDirectCostIva.Sum(Function(x) x.IvaValue)
            For Each item In (From x In _directCost.CostDistributionDirectCostDetail).ToList()
                item.IvaValue = Math.Round((IvaTotal * If(item.Percentage, 0)) / 100, 2, MidpointRounding.AwayFromZero)
                item.BaseValue = item.Value - item.IvaValue
            Next
            INDgcDirectCostDetail.RefreshDataSource()
        End If
    End Function

    Private Sub INDspIVA_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDspIVA.QueryPopUp
        If INDspIVA.Properties.DataSource Is Nothing Then
            _presenter.InitializeRateIva()
        End If
    End Sub

    Private Sub CleanControlsIvaDetails()
        INDtxtBaseIva.EditValue = 0
        INDtxtIvaValue.EditValue = Nothing
        INDspIVA.EditValue = Nothing
        FlagEditIvaDetail = False
    End Sub

    ''' <summary>
    ''' funcion para editar el registro de la rejilla de detalle del iva
    ''' </summary>
    Private Sub EditIvaDetail()
        FlagEditIvaDetail = True
        Dim _itemSelected = TryCast(INDGvIVADetail.GetFocusedRow, CostDistributionDirectCostIva)
        With _itemSelected
            INDtxtBaseIva.EditValue = .BaseIva
            INDspIVA.EditValue = .GeneralLedgerIvaId
            INDtxtIvaValue.EditValue = .IvaValue
        End With
        INDPceBanckAccount.ShowPopup()
    End Sub

    ''' <summary>
    ''' funcion para eliminar el registro de la rejilla de detalle del iva
    ''' </summary>
    Private Sub RemoveIvaDetailt()
        Dim _itemSelected = TryCast(INDGvIVADetail.GetFocusedRow, CostDistributionDirectCostIva)
        If _itemSelected.Id > 0 Then
            _itemSelected.MarkAsDeleted()
        End If
        _directCost.CostDistributionDirectCostIva.Remove(_itemSelected)
        INDGcIVADetail.DataSource = _directCost.CostDistributionDirectCostIva
        INDGcIVADetail.RefreshDataSource()
        CalculateValues()
        INDgcDirectCostDetail.RefreshDataSource()
        Me.EnableOrDisableCurrencyControl()
    End Sub

    ''' <summary>
    ''' funcion para eliminar el registro de la rejilla de detalle del iva
    ''' </summary>
    Private Sub RemoveDetail()
        Dim _itemSelected = TryCast(INDgvNoGenerateCxP.GetFocusedRow, CostDistributionDirectCostDetail)
        If _itemSelected.Id > 0 Then
            _itemSelected.MarkAsDeleted()
        End If
        _directCost.CostDistributionDirectCostDetail.Remove(_itemSelected)
        INDgcDirectCostDetail.DataSource = _directCost.CostDistributionDirectCostDetail
        INDgcDirectCostDetail.RefreshDataSource()
        CalculateValues()
        INDgcDirectCostDetail.RefreshDataSource()
        'Me.EnableOrDisableCurrencyControl()
    End Sub

    ''' <summary>
    ''' funcion para eliminar documentos de provision
    ''' </summary>
    Private Sub RemoveProvisionDocument()
        Dim _itemSelected = INDGvProvision.GetFocusedRow()
        _directCost.CostDistributionDirectCostLegalizedDocuments.Remove(_itemSelected)
        INDGcProvisionDocuments.DataSource = _directCost.CostDistributionDirectCostLegalizedDocuments
        INDGcProvisionDocuments.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' establece el formato de la moneda en el formulario
    ''' </summary>
    ''' <param name="currencyAbbreviation"></param>
    Private Sub SetFormatCurrencyUI(Optional currencyAbbreviation As String = Nothing)
        If String.IsNullOrEmpty(currencyAbbreviation) Then
            Mensaje(EeventViewerImages.Advertencia) = "La abreviación de la moneda está vacía."
            Exit Sub
        End If
        Me._tmpCurrentPeriod.CurrencyAbbreviation = currencyAbbreviation
        Me.changeNumericFormatByCurrency(currencyAbbreviation.GetNumberFormat)
        Me.changeNumericFormatByCurrency(currencyAbbreviation.GetNumberFormat, INDpccAddProductionCenter.Controls)
        Me.changeNumericFormatByCurrency(currencyAbbreviation.GetNumberFormat, IVADetailPopupContainer.Controls)
        Me.BaseValueCol = Window.Utils.FormatGrid(Me.BaseValueCol, currencyAbbreviation)
        Me.IvaTotalCol = Window.Utils.FormatGrid(Me.IvaTotalCol, currencyAbbreviation)
        Me.ColTotal = Window.Utils.FormatGrid(Me.ColTotal, currencyAbbreviation)
        Me.INDColIva = Window.Utils.FormatGrid(Me.INDColIva, currencyAbbreviation)
        Me.GridColumn202 = Window.Utils.FormatGrid(Me.GridColumn202, currencyAbbreviation)
    End Sub

    ''' <summary>
    ''' deshabilita o habilita el control de la moneda
    ''' </summary>
    Private Sub EnableOrDisableCurrencyControl()
        Me.INDSleCurrency.Enabled = Not (Value > 0 _
                                        OrElse
                                        (_directCost?.CostDistributionDirectCostIva IsNot Nothing _
                                            AndAlso
                                          _directCost?.CostDistributionDirectCostIva?.ToList()?.Any()))
    End Sub

    ''' <summary>
    ''' Funcion para verificar el Registro IVA(TaxRe) de CompanySettings
    ''' </summary>
    Private Async Function VerifyTaxRegistration() As Task(Of Boolean)
        If Not ProvisionDocument Then
            If TaxRegistration Is Nothing Then
                Using model As New MCompanySettings(Me.MyTag)
                    Dim companySettings = Await model.GetCompanySettings
                    If companySettings IsNot Nothing Then
                        TaxRegistration = companySettings.TaxRegistration
                    End If
                End Using
            End If
            Select Case TaxRegistration
                Case 1, 4     '1-IVA al costo - IVA al costo control fiscal
                    INDLyDeductibleIva.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    DeductibleIVA = False
                    Mensaje(EeventViewerImages.Informacion) = "Se actualizó el valor Iva Descontable de acuerdo al parámetro general Registra IVA de Distribución de Elementos del Costo"
                    Return False
                Case 2      '2-IVA descontable
                    INDLyDeductibleIva.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    DeductibleIVA = True
                    Mensaje(EeventViewerImages.Informacion) = "Se actualizó el valor Iva Descontable de acuerdo al parámetro general Registra IVA de Distribución de Elementos del Costo"
                    Return False
                Case 3      '3-IVA mixto
                    INDLyDeductibleIva.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    '' debido a que se habilita el combo y este tiene por defecto iva al costo se actualiza a 1 el taxregistration
                    TaxRegistration = 1
                    DeductibleIVA = False
            End Select
        End If
        Return True
    End Function
#End Region

#Region "Show columns in CostDistributionBaseDetails"
    Private Sub HideGeneralDetailColumns()
        CostDistributionBaseDetailsXpo = Nothing
        MeasurementUnitXpo = Nothing

        INDpceAddDetail.Enabled = False
        INDebtDistribution.Enabled = False
        INDBtnImportFile.Enabled = False
        DdbActions.Enabled = False
        IndigoGridView1.RaiseMenuPopUp = False

        Me.ColProductionCenter.Visible = True
        Me.ColProductionCenter.VisibleIndex = 0
        Me.ColMainAccount.VisibleIndex = 1
        Me.ColCenterCost.VisibleIndex = 2
        Me.BaseValueCol.Visible = False
        Me.IvaTotalCol.Visible = False
        Me.ColMeasurementUnit.Visible = False
        Me.ColCount.Visible = False
        Me.ColCostValue.Visible = False
        Me.ColTotal.Visible = False
        Me.ColPercentage.Visible = False
        Me.ColThirdParty.Visible = False
        Me.ColHoursManpower.Visible = False
        Me.ColManpowerHoursContracted.Visible = False
        Me.ColJobTitle.Visible = False
        Me.ColPosition.Visible = False
        Me.ColProduct.Visible = False

        _tmpCurrentPeriod.Value = 0D
        _tmpCurrentPeriod.Balance = 0D
        _presenter.GetGeneralExpenseById(GeneralExpenseId)
    End Sub

    Private Sub ShowGeneralDetailColumns()
        If IsDirectDistribution(GeneralExpense) Then
            INDpceAddDetail.Enabled = True
            INDebtDistribution.Enabled = True
            INDBtnImportFile.Enabled = True
            DdbActions.Enabled = True
            IndigoGridView1.RaiseMenuPopUp = True
        End If

        If TaxRegistration = 2 Then
            Me.BaseValueCol.Visible = False
            Me.IvaTotalCol.Visible = False
            Me.ColPercentage.Visible = False
        Else
            Me.BaseValueCol.Visible = True
            Me.BaseValueCol.VisibleIndex = 3
            Me.IvaTotalCol.Visible = True
            Me.IvaTotalCol.VisibleIndex = 4
            Me.ColPercentage.Visible = True
            Me.ColPercentage.VisibleIndex = 5
        End If

        Me._presenter.InitializeMeasurimentUnit(GeneralExpenseId, True)
    End Sub

    Private Sub StandardDistributionColumns()
        If IsDirectDistribution(GeneralExpense) Then
            INDpceAddDetail.Enabled = True
            INDebtDistribution.Enabled = True
            INDBtnImportFile.Enabled = True
            DdbActions.Enabled = True
            IndigoGridView1.RaiseMenuPopUp = True

            Me.ColMeasurementUnit.VisibleIndex = 2
            Me.ColCount.VisibleIndex = 3
            Me.ColCostValue.VisibleIndex = 4
            If TaxRegistration <> 2 Then
                Me.BaseValueCol.Visible = True
                Me.IvaTotalCol.Visible = True
                Me.BaseValueCol.VisibleIndex = 2
                Me.IvaTotalCol.VisibleIndex = 3
            End If

            Me.ColTotal.VisibleIndex = 7
            Me._colActions.VisibleIndex = 8
            Me._presenter.InitializeMeasurimentUnit(GeneralExpenseId, True)
        Else
            If Not DeductibleIVA Then
                Me.BaseValueCol.Visible = True
                Me.IvaTotalCol.Visible = True
                Me.BaseValueCol.VisibleIndex = 2
                Me.IvaTotalCol.VisibleIndex = 3
            End If
            Me.ColPercentage.VisibleIndex = 4
            Me.ColTotal.VisibleIndex = 5
            Me._colActions.Visible = False

            RaiseCalcDistribution()
        End If
    End Sub

    Private Sub ManpowerDistributionColumns()
        ShowGeneralDetailColumns()

        Me.ColThirdParty.Visible = True
        Me.ColThirdParty.VisibleIndex = 6
        Me.ColPosition.Visible = True
        Me.ColPosition.VisibleIndex = 7
        Me.ColHoursManpower.Visible = True
        Me.ColHoursManpower.VisibleIndex = 8
        Me.ColManpowerHoursContracted.Visible = True
        Me.ColManpowerHoursContracted.VisibleIndex = 9

        Me.ColTotal.VisibleIndex = 9
        Me._colActions.VisibleIndex = 10

        If ThirdPartyRepositoryXpo Is Nothing Then
            _presenter.InitializeThirdParty()
        End If

        If PositionRepositoryXpo Is Nothing Then
            _presenter.InitializePosition()
        End If

    End Sub

    Private Sub OverheadDistributionColumns()
        ShowGeneralDetailColumns()

        Me.ColThirdParty.Visible = True
        Me.ColThirdParty.VisibleIndex = 6

        Me.ColTotal.VisibleIndex = 7
        Me._colActions.VisibleIndex = 8

        If ThirdPartyRepositoryXpo Is Nothing Then
            _presenter.InitializeThirdParty()
        End If
    End Sub

    Private Sub ProductsDistributionColumns()
        ShowGeneralDetailColumns()

        Me.ColProduct.Visible = True
        Me.ColProduct.VisibleIndex = 6

        Me.ColTotal.VisibleIndex = 7
        Me._colActions.VisibleIndex = 8

        If InventoryProductRepositoryXpo Is Nothing Then
            _presenter.InitializeProducts()
        End If
    End Sub

    ''' <summary>
    ''' Método para manejar la visibilidad de campos y columnas cuando no se genera CxP
    ''' </summary>
    Private Sub NoGenerateAccountPayableFields()
        'Se habilitan botes de importar y exportar
        INDebtDistribution.Enabled = True
        INDBtnImportFile.Enabled = True
        'Ocultar y mostrar campos necesarios
        INDlcgBillData.HideControl
        INDlcgIVADetails.HideControl
        LyGroupProvisionDocuments.HideControl
        INDlciDocumentDate.HideControl(False)
        INDlciThirdParty.HideControl(False)
        INDlciSupplier.HideControl
        INDlciDistributionLine.HideControl
        INDlciPosition.HideControl
        INDlciAccountPayable.HideControl
        INDlciInvoiceValue.HideControl
        INDLyDeductibleIva.HideControl
        'Se asigna la vista para cuando no se genera CxP
        INDgcDirectCostDetail.MainView = INDgvNoGenerateCxP
        IndigoGridControl1.UpdateNewMainView(INDgcDirectCostDetail.MainView)

        If ThirdPartyRepositoryXpo Is Nothing Then
            _presenter.InitializeThirdParty()
        End If

        If PositionRepositoryXpo Is Nothing Then
            _presenter.InitializePosition()
        End If

        If ThirdPartyXpo Is Nothing Then
            ThirdPartyXpo = _presenter.GetAllThirdParty()
        End If

        If RetentionConceptXpo Is Nothing Then
            _presenter.InitializeRetentionConcept()
        End If

        If _directCost?.CostDistributionDirectCostDetail?.Any() Then
            _tmpCurrentPeriod.Value = _directCost.CostDistributionDirectCostDetail.Sum(Function(o) o.Value)
        End If
    End Sub

    ''' <summary>
    ''' Método para devolver visibilidad 
    ''' </summary>
    Private Sub GenerateAccountPayableFields()
        INDlcgBillData.HideControl(False)
        INDlciDocumentDate.HideControl
        INDlciThirdParty.HideControl
        INDlciSupplier.HideControl(False)
        INDlciDistributionLine.HideControl(False)
        INDlciPosition.HideControl(False)
        INDlciAccountPayable.HideControl(False)
        INDlciInvoiceValue.HideControl(False)
        INDgcDirectCostDetail.MainView = INDgvDirectCostDetail
    End Sub
#End Region

#Region "Import Files"

    ''' <summary>
    ''' Carga y valida el archivo Excel seleccionado usando DevExpress SpreadsheetControl.
    ''' </summary>
    ''' <param name="sender">Objeto que lanza el evento importar.</param>
    ''' <param name="e">Argumentos del evento.</param>
    Private Async Sub INDBtnImportFile_Click(sender As Object, e As EventArgs) Handles INDBtnImportFile.Click
        If Status IsNot Nothing And Status <> 1 Then
            Mensaje(EeventViewerImages.Advertencia) = "El documento no se encuentra en estado registrado"
            Exit Sub
        End If

        Dim openFileDialog1 As New OpenFileDialog()
        openFileDialog1.InitialDirectory = "c:\"
        openFileDialog1.Filter = "Microsoft Excel 2003 (*.xls)|*.xls|Microsoft Excel 2007 (*.xlsx)|*.xlsx"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True
        openFileDialog1.Title = "Importar Archivo"

        'Si el usuario cancela la operación
        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        AsyncLoader(True)
        Try
            'obtengo la ruta del archivo
            myStream = openFileDialog1.FileName

            If String.IsNullOrEmpty(myStream) Then 'Si la ruta es vacía
                Mensaje(EeventViewerImages.Advertencia) = "La ruta del archivo se encuentra vacía"
                AsyncLoader(False)
                Exit Sub
            End If

            If MessageIndigo.Show("Desea continuar con el proceso de importación", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                listErrorsImportFile = New List(Of String)
                Await LoadDataImportFile(myStream)
                CalculatePercentage()

                'si existen errores informamos al usuario y el proceso no continua
                If listErrorsImportFile.Any() Then
                    Using formulario As New FrmListErrors(listErrorsImportFile)
                        formulario.StartPosition = FormStartPosition.CenterParent
                        Dim transparent As New FrmTransparent(formulario, False)
                        transparent.SafeInvoke(Sub(f) f.ShowDialog())
                    End Using
                End If
            End If

            AsyncLoader(False)
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = "No se pudo leer el archivo por " + Environment.NewLine + ex.Message
        Finally
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' Permite leer el archivo Excel seleccionado usando DevExpress SpreadsheetControl.
    ''' Verifica estructura, cantidad de hojas y columnas requeridas, y si es válida continúa con la importación.
    ''' </summary>
    ''' <param name="myStream">Ruta completa del archivo Excel a importar.</param>
    Private Async Function LoadDataImportFile(ByVal myStream As String) As Task
        Dim ssc = New DevExpress.XtraSpreadsheet.SpreadsheetControl()
        ssc.AllowDrop = False
        ssc.LoadDocument(myStream)
        Dim workBook As IWorkbook = ssc.Document

        If workBook.Worksheets.Count <> 1 Then
            listErrorsImportFile.Add("El archivo debe tener solo una hoja con los datos correspondientes")
            Exit Function
        End If

        rows = workBook.Worksheets(0).Rows
        If rows.LastUsedIndex <= 0 Then
            listErrorsImportFile.Add("No se encontraron registros en el archivo")
            Exit Function
        End If

        'Se establecen la cantidad de columnas por distribución a trabajar
        columns = workBook.Worksheets(0).Columns
        Dim expectedNumberColumns As Integer = 0
        If GeneralExpenseId > 0 AndAlso GeneralExpense IsNot Nothing Then
            Select Case GeneralExpense.DistributionType
                Case 1
                    expectedNumberColumns = 6 'StandardDistribution
                Case 2
                    If GeneralExpense.GenerateAccountPayable Is Nothing Or GeneralExpense.GenerateAccountPayable Then
                        expectedNumberColumns = 8 'ManpowerDistribution
                    Else
                        expectedNumberColumns = 15 'NoGenerateCxP
                    End If
                Case 3, 4
                    expectedNumberColumns = 5 'OverheadDistribution And ProductsDistribution
                Case Else
                    listErrorsImportFile.Add("Para obtener la estructura del archivo elija un elemento del costo")
                    Exit Function
            End Select
        End If

        If Not ValidateColumnsImportFile(columns, expectedNumberColumns) Then
            Exit Function
        End If

        Await ImportFileCostDistributionDirectCostDetailAsync(expectedNumberColumns)
    End Function

    Private Function ValidateColumnsImportFile(fileColumns As ColumnCollection, expectedColumns As Integer)
        If fileColumns.LastUsedIndex <> expectedColumns - 1 Then
            listErrorsImportFile.Add("El archivo importado posee una cantidad de columnas diferente a la esperada")
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Procesa las filas del archivo importado.
    ''' </summary>
    Private Async Function ImportFileCostDistributionDirectCostDetailAsync(expectedCols As Integer) As Task
        listRows = New Concurrent.ConcurrentBag(Of ImportFileRow)()

        SetRow(1, rows.LastUsedIndex + 1)

        Dim data As New List(Of List(Of String))
        Parallel.ForEach(listRows,
        Sub(row)
            Dim dataRow As List(Of String) =
                row.Row.Select(Function(item) If(item Is Nothing, String.Empty, CStr(item))).ToList()

            If dataRow.Count < expectedCols Then
                dataRow.AddRange(Enumerable.Repeat(String.Empty, expectedCols - dataRow.Count))
            ElseIf dataRow.Count > expectedCols Then
                dataRow = dataRow.Take(expectedCols).ToList()
            End If

            If dataRow.Any(Function(v) v <> String.Empty) Then
                SyncLock data
                    data.Add(dataRow)
                End SyncLock
            End If
        End Sub)

        Await ImportDetailsDistributionCosts(data)
    End Function

    ''' <summary>
    ''' Esta función parece realizar un procesamiento paralelo en una colección de filas de datos provenientes de una hoja de cálculo.
    ''' </summary>
    ''' <param name="indexSend"></param>
    ''' <param name="indexEnd"></param>
    Private Sub SetRow(indexSend As Integer, indexEnd As Integer)
        Dim objLock As New Object()
        If GeneralExpense.GenerateAccountPayable Is Nothing Or GeneralExpense.GenerateAccountPayable Then
            Parallel.For(indexSend, indexEnd, Sub(x)
                                                  SyncLock objLock
                                                      listRows.Add(New ImportFileRow With {.IndexRow = x + 1, .Row = rows.Item(x).SpreadsheetRowToList(10)})
                                                  End SyncLock
                                              End Sub)
        Else
            Parallel.For(indexSend, indexEnd, Sub(x)
                                                  SyncLock objLock
                                                      listRows.Add(New ImportFileRow With {.IndexRow = x + 1, .Row = rows.Item(x).SpreadsheetRowToList(15)})
                                                  End SyncLock
                                              End Sub)
        End If
    End Sub

    ''' <summary>
    ''' Importa los detalles de la distribución de costos directos a la base de datos usando el modelo correspondiente.
    ''' Se realiza por lotes para optimizar el rendimiento y evitar timeouts.
    ''' </summary>
    ''' <param name="data">Lista de listas que representa las filas y columnas del archivo a importar.</param>
    Private Async Function ImportDetailsDistributionCosts(data As List(Of List(Of String))) As Task
        Try
            Using model As New MCostDistributionDirectCost(MyTag)
                'se establece la cantidad de filas a procesas
                Dim totalRows = data.Count

                For batchStart = 0 To totalRows - 1 Step DefaultBatchSize
                    Dim batchEnd As Integer = Math.Min(batchStart + DefaultBatchSize, totalRows)
                    'se toman la cantidad de registros a procesar por lotes (para evitar timeout)
                    Dim dataBatch = data.Skip(batchStart).Take(batchEnd - batchStart).ToList()

                    'Dim result = Await Task.Run(Function() model.CopyAndPasteCostDistributionDirectCostDetail(GeneralExpenseId, dataBatch))

                    Dim result = Await model.CopyAndPasteCostDistributionDirectCostDetailAsync(GeneralExpenseId, dataBatch)
                    If Not result.StateResult Then
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                        Continue For
                    End If

                    'se añaden los mensajes de error a la lista de mensaje para mostrar el modal
                    If result.MessageResult?.Any() Then
                        listErrorsImportFile.AddRange(result.MessageResult)
                    End If

                    'si se retornan items se añaden a la rejilla
                    If result.ObjectEmbbeded?.Any() Then
                        For Each CostDistributionBaseDetail In result.ObjectEmbbeded
                            Dim _directExpenseDetail As New CostDistributionDirectCostDetail()
                            If ListDistributionDirectCostDetail Is Nothing Then
                                ListDistributionDirectCostDetail = New List(Of CostDistributionDirectCostDetail)
                            End If

                            If GeneralExpense?.GenerateAccountPayable Is Nothing OrElse GeneralExpense.GenerateAccountPayable Then
                                If ValidateProductionCenterInList(CostDistributionBaseDetail.ProductionCenterId, CostDistributionBaseDetail.MainAccountId, CostDistributionBaseDetail.CostCenterId, CostDistributionBaseDetail.MeasurementUnitId, CostDistributionBaseDetail.CostValue) Then
                                    With _directExpenseDetail
                                        .ProductionCenterId = CostDistributionBaseDetail.ProductionCenterId
                                        .ProductionCenterCodeName = CostDistributionBaseDetail.ProductionCenterCodeName
                                        .MainAccountId = CostDistributionBaseDetail.MainAccountId
                                        .MainAccountCodeName = CostDistributionBaseDetail.MainAccountCodeName
                                        .HandlesThirdParty = CostDistributionBaseDetail.HandlesThirdParty
                                        .CostCenterId = CostDistributionBaseDetail.CostCenterId
                                        .CostCenterCodeName = CostDistributionBaseDetail.CostCenterCodeName
                                        .MeasurementUnitId = CostDistributionBaseDetail.MeasurementUnitId
                                        .MeasurementUnitCodeName = CostDistributionBaseDetail.MeasurementUnitCodeName
                                        .Count = CostDistributionBaseDetail.Count
                                        .CostValue = CostDistributionBaseDetail.CostValue
                                        .Value = Utils.RoundValue((If(.Count, 0) * If(.CostValue, 0)), 6)
                                        .Percentage = CostDistributionBaseDetail.Percentage
                                        .ThirdPartyId = CostDistributionBaseDetail.ThirdPartyId
                                        .HoursManpower = CostDistributionBaseDetail.HoursManpower
                                        .ManpowerHoursContracted = CostDistributionBaseDetail.ManpowerHoursContracted
                                        .JobTitle = CostDistributionBaseDetail.JobTitle
                                        .PositionId = CostDistributionBaseDetail.PositionId
                                        .ProductId = CostDistributionBaseDetail.ProductId
                                    End With

                                    AddDirectExpensesDetail(_directExpenseDetail)
                                Else
                                    listErrorsImportFile.Add("El registro con centro de producción " & CostDistributionBaseDetail.ProductionCenterCodeName & " cuenta contable " & CostDistributionBaseDetail.MainAccountCodeName & " y centro de costo " & CostDistributionBaseDetail.CostCenterCodeName & " ya se encuentra en la rejilla")
                                End If
                            Else
                                With _directExpenseDetail
                                    .MainAccountId = CostDistributionBaseDetail.MainAccountId
                                    .MainAccountCodeName = CostDistributionBaseDetail.MainAccountCodeName
                                    .Nature = CostDistributionBaseDetail.Nature
                                    .NatureText = CostDistributionBaseDetail.NatureText
                                    .CostCenterId = CostDistributionBaseDetail.CostCenterId
                                    .CostCenterCodeName = CostDistributionBaseDetail.CostCenterCodeName
                                    .ThirdPartyId = CostDistributionBaseDetail.ThirdPartyId
                                    .ThirdPartyNitName = CostDistributionBaseDetail.ThirdPartyNitName
                                    .EmployeeId = CostDistributionBaseDetail.EmployeeId
                                    .PositionId = CostDistributionBaseDetail.PositionId
                                    .PositionCodeName = CostDistributionBaseDetail.PositionCodeName
                                    .HoursManpower = CostDistributionBaseDetail.HoursManpower
                                    .ManpowerHoursContracted = CostDistributionBaseDetail.ManpowerHoursContracted
                                    .Value = CostDistributionBaseDetail.Value
                                    .Observations = CostDistributionBaseDetail.Observations
                                    .RetentionId = CostDistributionBaseDetail.RetentionId
                                    .BaseRetention = CostDistributionBaseDetail.BaseRetention
                                    .InvoicedValue = CostDistributionBaseDetail.InvoicedValue
                                    .PayrollConcept = CostDistributionBaseDetail.PayrollConcept
                                    .ProcessDate = CostDistributionBaseDetail.ProcessDate
                                End With

                                AddDirectExpensesDetail(_directExpenseDetail)
                            End If
                        Next
                    End If
                Next
            End Using
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = $"No se pudo leer el archivo por {Environment.NewLine} {ex.Message}"
        End Try
    End Function

#End Region

End Class

Public Class GetListProvisionDocumentEventArgs
    Inherits EventArgs

    Property ListProvisionDocument As List(Of CostDistributionDirectCostLegalizedDocuments)

End Class