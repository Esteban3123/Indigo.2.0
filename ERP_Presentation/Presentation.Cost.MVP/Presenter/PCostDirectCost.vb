'***********************************************************************
' Assembly         : Presentacion.InteropCost.MVP
' Author           : Diego Andrés Roldán
' Created          : 11-12-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.CostRepository
Imports Infrastructure.Data.Xpo.CommonRepository
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports Infrastructure.Data.Xpo.AccountingRepository

#End Region

Public Class PCostDirectCost

    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As ICostDistributionDirectCost

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As ICostDistributionDirectCost)
        If iView Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        View = iView
        Indigo = SessionValues.Instance
    End Sub

    ''' <summary>
    ''' Función para listar todos los terceros del formulario Personas y Terceros
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllThirdParty() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetThirdParty()
    End Function

#Region "Methods"

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' Obtiene la secuencia
    ''' </summary>
    Public Async Function GetSequence() As Task
        Using Model As New MCommonCost(View.MyTag)
            Me.View.Sequence = Await Model.GetSequense()
        End Using
    End Function

    ''' <summary>
    ''' Carga los parámetros de costos
    ''' </summary>
    Public Async Function LoadSettingCost() As Task
        Using Model As New MCostSetting(Me.View.MyTag)
            Me.View.SettingsCost = Await Model.GetCostSettingAsync()
        End Using
    End Function

    ''' <summary>
    ''' Datasource de elementos del costo
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeGeneralExpenseXpo()
        Me.View.GeneralExpenseXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).CostService.ListGeneralExpensesByStatus(True)
    End Sub

    ''' <summary>
    ''' Obtiene el elemento del costo por id
    ''' </summary>
    ''' <param name="GeneralExpenseId"></param>
    ''' <remarks></remarks>
    Public Sub GetGeneralExpenseById(GeneralExpenseId As Integer)
        If View.GeneralExpenseId = 0 Then
            View.GeneralExpense = Nothing
        Else
            Dim filtroConsulta As String = "Id = " & GeneralExpenseId
            View.GeneralExpense = XpoServiceEx.Instance(Indigo.TransactionalContainer).CostService.GetCollection(Of CostGeneralExpenseXpo)(Nothing, filtroConsulta).FirstOrDefault()
        End If
    End Sub

    ''' <summary>
    ''' Datasource de elementos del costo
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeSuppliersDistributionLinesXpo()
        Using model As New MBusqueda
            Me.View.SuppliersDistributionLinesXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListSuppliersDistributionLines)
        End Using
    End Sub

    ''' <summary>
    ''' Metodo para cargar de informacion los combos
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeRateIva()
        Using msearch As New MBusqueda
            Me.View.RateIvaXpo = CType(msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListGeneralLedgerIva), XPInstantFeedbackSource)
        End Using
    End Sub

    Public Function GetSupplierDistributionLineById(id As String) As Infrastructure.Data.Xpo.CommonRepository.CommonSuppliersDistibutionLineXpo
        Dim filtroConsulta As String = "Id = " & id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetCollection(Of CommonSuppliersDistibutionLineXpo)(Nothing, filtroConsulta)(0)
    End Function

    ''' <summary>
    ''' Datasource cuentas por pagar
    ''' </summary>
    ''' <param name="ThirdPartyId"></param>
    ''' <param name="CostDistributionDirectCostId"></param>
    Public Sub GetXpoAccountPayable(ByVal ThirdPartyId As Integer, ByVal CostDistributionDirectCostId As Integer)
        View.AccountPayableXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.ListAccountPayablesWithoutDistribuitByIdThirdParty(ThirdPartyId, CostDistributionDirectCostId)
    End Sub

    ''' <summary>
    ''' Datasource de elementos del costo
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeCostCenterXpo()
        Me.View.CostCenterXpo = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).PayrollService.GetCostCenterByState(True)
    End Sub

    ''' <summary>
    ''' Datasource de elementos del costo
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeFilingUnitXpo()
        Using model As New MCommonCost(View.MyTag)
            Me.View.FilingUnitXpo = model.GetFilingUnitByUser(Indigo.UserIndigo).ObjectEmbbeded
        End Using
    End Sub

    ''' <summary>
    ''' Initializes the production center.
    ''' </summary>
    Public Sub InitializeCostDistributionBaseDetails()
        View.CostDistributionBaseDetailsXpo = View.GeneralExpense.CostDistributionBaseXpo(0).CostDistributionBaseDetailsXpo.Where(Function(x) x.ProductionCenterId.Status = True And IIf(x.CostCenterId Is Nothing, True, x.CostCenterId?.State = True)).ToList()
    End Sub

    ''' <summary>
    ''' Datasource unidades de medida
    ''' </summary>
    ''' <param name="GeneralExpenseId"></param>
    ''' <remarks></remarks>
    Public Sub InitializeMeasurimentUnit(GeneralExpenseId As Integer, Optional isRepository As Boolean = False)
        If View.GeneralExpense Is Nothing Then
            Exit Sub
        End If
        Dim ListMeasurementUnitIds = (From x In View.GeneralExpense.CostDistributionBaseXpo(0).CostDistributionBaseMeasurementUnitXpo Select x.MeasurementUnitId.Id).ToList
        If isRepository Then
            View.MeasureUnitDataSourceRerpository = XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListMeasurementUnitByIds(ListMeasurementUnitIds)
        Else
            View.MeasurementUnitXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListMeasurementUnitByIds(ListMeasurementUnitIds)
        End If
    End Sub

    Public Function GetCostDistributionBaseDetailById(Id As Integer) As CostRepository.CostDistributionBaseDetailXpo
        Dim filter As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CostService.GetCollection(Of CostRepository.CostDistributionBaseDetailXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' se incializa el datasource de la moneda
    ''' </summary>
    Public Sub InitializateCurrency()
        View.CurrencyXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetCurrency()
    End Sub

    ''' <summary>
    ''' Inicializacción del datasource de los terceros
    ''' </summary>
    Public Sub InitializeThirdParty()
        Me.View.ThirdPartyRepositoryXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetThirdParty()
    End Sub

    ''' <summary>
    ''' Inicializacción del datasource de los cargos
    ''' </summary>
    Public Sub InitializePosition()
        Me.View.PositionRepositoryXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetPosition()
    End Sub

    ''' <summary>
    ''' Inicializacción del datasource de los productos
    ''' </summary>
    Public Sub InitializeProducts()
        Me.View.InventoryProductRepositoryXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListInventoryProduct(True)
    End Sub

    ''' <summary>
    ''' Inicialización de los conceptos de retención
    ''' </summary>
    Public Sub InitializeRetentionConcept()
        Me.View.RetentionConceptXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.ListRetentionConcept()
    End Sub

    ''' <summary>
    ''' Obtiene la configuracion de la moneda oficial
    ''' </summary>
    ''' <returns></returns>
    Public Function GetOfficialCurrencyFromCompanySettings() As GeneralLedgerCompanySettingsXpo
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.GetXPOObject(Of GeneralLedgerCompanySettingsXpo)(Nothing)
    End Function
#End Region

End Class