'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Henry Alejandro Vargas Polania 
' Created          : 24/14/2014
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
Imports Infrastructure.Data.Xpo.BudgetRepository
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports Presentation.Base
Imports Presentation.Controls.MVP
#End Region

Public Class PInventoryContract

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IInventoryContract

    ''' <summary>
    ''' Variable que se usa para tratar la corporacion como un objeto
    ''' </summary>
    Dim Corporation As Object

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IInventoryContract)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

#End Region

#Region "Methods"

    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequense(Me.View.MyTag)
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
    Public Function GetSettingsInventoryByOperatingUnitId(OperatingUnitId As Integer) As SettingInventoryXpo
        Dim filter As String = "OperatingUnitId = " & OperatingUnitId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetCollection(Of SettingInventoryXpo)(Nothing, filter).FirstOrDefault()
    End Function

    Public Sub LoadSuppliers()
        Using Model As New MInventoryContract("")
            Me.View.ListSupplier = Model.ListSupplierMaintenanceXPO()
        End Using
    End Sub

    Public Sub LoadContractTypesByStatus()
        Using Model As New MInventoryContract("")
            Me.View.ListContractType = Model.ListContracTypeXPO(True)
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las ciudades
    ''' </summary>
    Public Sub InitializeSupplier()
        Dim model As New MBusqueda
        Me.View.SuppliersDistributionLinesXpo = CType(model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListSuppliersDistributionLines), DevExpress.Xpo.XPInstantFeedbackSource)
    End Sub

    ''' <summary>
    ''' inicializa el combo de moneda
    ''' </summary>
    Public Sub InitializeCurrency()
        Using ModelXpo As New MBusqueda
            View.CurrencyDatasource = ModelXpo.ConsultarEntidades(eDataSource.Currency)
        End Using
    End Sub
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

#End Region

#End Region

End Class
