'***********************************************************************
' Assembly         : Presentacion.FixedAsset
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 20-01-2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Presentation.Base
Imports Presentation.Controls.MVP
Imports Infrastructure.Data.Xpo.CommonRepository

#End Region

Public Class PFixedAssetPurchaseOrder

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IFixedAssetPurchaseOrder

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IFixedAssetPurchaseOrder)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        Me.View = iview
        Indigo = SessionValues.Instance
    End Sub

    Public Sub New()
        Indigo = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequenceFixedAsset(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Obtiene la linea de distribución
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetSettingsPaymentsByOperatingUnitId(OperatingUnitId As Integer) As PaymentsSettingPaymentsXpo
        Dim filter As String = "IdOperatingUnit = " & OperatingUnitId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.GetCollection(Of PaymentsSettingPaymentsXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene la linea de distribución
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetSettingsFixedAssetByOperatingUnitId(OperatingUnitId As Integer) As SettingFixedAssetXpo
        Dim filter As String = "OperatingUnitId = " & OperatingUnitId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.GetCollection(Of SettingFixedAssetXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Lista las remisiones de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetBranchOfficeByIdXpo(Id As Integer) As PayrollBranchOffice
        Dim filtroConsulta As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetCollection(Of PayrollBranchOffice)(Nothing, filtroConsulta).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene el item
    ''' </summary>
    ''' <returns></returns>
    Public Function GetItemById(Id As Integer) As FixedAssetEquipmentXpo
        Dim filtroConsulta As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.GetCollection(Of FixedAssetEquipmentXpo)(Nothing, filtroConsulta).FirstOrDefault()
    End Function

#Region "Budget Interface"

    ''' <summary>
    ''' Inicializa el datasource de las entidades presupuestales
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeBudgetaryEntity()
        If View.BudgetaryEntityXpo Is Nothing Then
            View.BudgetaryEntityXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.ListCollectionBudgetEntityByStatus(True)
        End If
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las vigencias
    ''' </summary>
    ''' <param name="budgetEntityId"></param>
    ''' <remarks></remarks>
    Public Sub InitializeBudgetaryValidity(budgetEntityId As Integer)
        If View.BudgetaryValidityXpo Is Nothing Then
            View.BudgetaryValidityXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.ListCollectionValidityByBudgetByBudgetEntityIdAndStatus(budgetEntityId, 2)
        End If
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las disponibilidades
    ''' </summary>
    Public Sub InitializeAvailabilityDetails(BudgetaryValidityId As Integer)
        If View.AvailabilityXpo Is Nothing Then
            Me.View.AvailabilityXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.ListAvailabilityDetails(BudgetaryValidityId)
        End If
    End Sub

    ''' <summary>
    ''' Obtiene el detalle de la disponibilidad por el Id
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetAvailabilityDetailById(Id As Integer) As BudgetRepository.ViewListAvailabilityDetailXpo
        Dim filter As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.GetCollection(Of BudgetRepository.ViewListAvailabilityDetailXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Lista las unidades funcionales activas
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeFunctionalUnitXPO()
        View.RequestedFunctionalUnitXPO = XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.ListFunctionalUnit(True)
    End Sub

    ''' <summary>
    ''' Obtiene el proveedor por id de la relacion entre proveedor y linea de distribucion
    ''' </summary>
    ''' <param name="supplierDistributionLineId"></param>
    ''' <returns></returns>
    Public Function GetSupplierBySupplierDistributionLineId(supplierDistributionLineId As Integer) As CommonSuppliersDistibutionLineXpo
        Dim filter As String = "Id = " & supplierDistributionLineId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetCollection(Of CommonSuppliersDistibutionLineXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' inicializa el combo de moneda
    ''' </summary>
    Public Sub InitializeCurrency()
        Using ModelXpo As New MBusqueda
            View.CurrencyDatasource = ModelXpo.ConsultarEntidades(eDataSource.Currency)
        End Using
    End Sub

#End Region

#End Region

End Class
