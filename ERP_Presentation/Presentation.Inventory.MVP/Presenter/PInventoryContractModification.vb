#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.Base

#End Region

Public Class PInventoryContractModification

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IInventoryContractModification

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
    Public Sub New(ByRef iview As IInventoryContractModification)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

#End Region

#Region "Methods"

    Public Async Function GetSequense() As Task
        Using model As New MBlockRecordAndSequense(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Function

    Public Function GetSettingsPaymentsByOperatingUnitId(OperatingUnitId As Integer) As PaymentsRepository.PaymentsSettingPaymentsXpo
        Dim filter As String = "IdOperatingUnit = " & OperatingUnitId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.GetCollection(Of PaymentsRepository.PaymentsSettingPaymentsXpo)(Nothing, filter).FirstOrDefault()
    End Function

    Public Sub LoadContractInventory()
        Using Model As New MPurchaseOrder("")
            Me.View.ListContractInventory = Model.ListContractInventoryXPO()
        End Using
    End Sub

    Public Function GetContractById(Id As Integer) As InventoryRepository.InventoryContractXpo
        Dim filter As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetCollection(Of InventoryRepository.InventoryContractXpo)(Nothing, filter).FirstOrDefault()
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

#End Region

#End Region

End Class
