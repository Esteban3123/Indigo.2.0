'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Juan Carlos Bermudez Gutierrez 
' Created          : 13/05/2015
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
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Presentation.Base
Imports Presentation.Controls.MVP

#End Region

Public Class PTransferOrder

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As ITransferOrder

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
    Public Sub New(ByRef iview As ITransferOrder)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequense(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' cargar el datasource de las unidades funcionales
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub TargetFunctionalUnit()
        Using model As New MBusqueda
            Me.View.TargetFunctionalUnitXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFunctionalUnit, True)
        End Using
    End Sub

    ''' <summary>
    ''' carga el datasource de los almacenes de origen
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SourceWarehouse()
        Me.View.SourceWarehouseXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListWareHouseByOrderType(View.OrderType, True, Indigo.UserIndigo)
        'If Me.View.OrderType = 2 Then
        '    Me.View.SourceWarehouseXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListOwnAndControlWarehouseByStatusAndUser(True, Indigo.UserIndigo)
        'Else
        '    Me.View.SourceWarehouseXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListOwnWarehouseByStatusAndUser(True, Indigo.UserIndigo)
        'End If
    End Sub

    ''' <summary>
    ''' carga el datasource de los almacenes de origen
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub TransitWarehouse()
        Me.View.TransitWarehouseXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListTransitWarehouseByStatusAndUser(True, Indigo.UserIndigo)
    End Sub

    ''' <summary>
    ''' carga el datasource de los almacenes de destino
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub TargetWarehouse()
        Using Model As New MInventoryAdjustments("")
            Me.View.TargetWarehouseXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListOwnWarehouseByStatusAndUser(True, Nothing)
        End Using
    End Sub

    ''' <summary>
    ''' Concepto de ajuste de inventario por clase de movimiento
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeAdjustmentConcept(Optional CostCenterId As Integer? = Nothing)
        Dim filter() As Object = {2, 2, True}
        Me.View.AdjustmentConceptXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListAdjustmentConceptByUsersAndCostCenter(2, 2, True, Indigo.UserIndigo, CostCenterId)
    End Sub

    ''' <summary>
    ''' carga el datasource de los terceros
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeThirdParty()
        Using Model As New MInventoryAdjustments("")
            Me.View.ThirdPartyXpo = Model.ListAllThirdPartyXpo()
        End Using
    End Sub

    ''' <summary>
    ''' Lista las remisiones de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetWarehouseByIdXpo(Id As Integer) As WarehouseXpo
        Dim filter As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetCollection(Of WarehouseXpo)(Nothing, filter).FirstOrDefault()
    End Function

#End Region

End Class
