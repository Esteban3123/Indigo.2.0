'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Juan Carlos Bermudez Gutierrez 
' Created          : 04/05/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository.View
Imports Presentation.Base
Imports Presentation.Controls.MVP
#End Region

Public Class PInventoryRequest

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IInventoryRequest

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
    Public Sub New(ByRef iview As IInventoryRequest)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    Public Sub New()
        Me.Indigo = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequense(Me.View.MyTag)
            Dim seq = Await model.GetSequense()
            Me.View.Sequense = IIf(seq Is Nothing OrElse seq.Id = 0, Nothing, seq)
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
        Me.View.SourceWarehouseXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListOwnWarehouseByStatusAndUser(True, Nothing)
    End Sub

    ''' <summary>
    ''' carga el datasource de los almacenes de destino
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub TargetWarehouse()
        Me.View.TargetWarehouseXpo = Infrastructure.Data.Xpo.XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListOwnWarehouseByStatusAndUser(True, Indigo.UserIndigo)
    End Sub

    ''' <summary>
    ''' Retorna los productos o Insumos por unidad funcional según parámetros de solicitudes
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetListRequestParamByFunctionalUnitId(FunctionalUnitId As Integer) As List(Of ViewRequestParamXpo)
        Dim filter As String = String.Format("FunctionalUnitId = {0}", FunctionalUnitId)
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.GetCollection(Of ViewRequestParamXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Retorna los productos o Insumos por almacen según parámetros de solicitudes
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetListRequestParamByWarehouseId(WarehouseId As Integer) As List(Of ViewRequestParamXpo)
        Dim filter As String = String.Format("WarehouseId = {0}", WarehouseId)
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.GetCollection(Of ViewRequestParamXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Retorna los Insumos por unidad funcional según parámetros de solicitudes
    ''' </summary>
    ''' <remarks></remarks>
    Public Function DatasourceSuppliesByRequestParams(ListViewRequestParamXpo As List(Of ViewRequestParamXpo)) As XPInstantFeedbackSource
        If Not ListViewRequestParamXpo.Any() Then
            Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListInventorySupplie()
        End If

        Dim ids = ListViewRequestParamXpo.Where(Function(d) d.Type = 1).Select(Function(d) d.SupplieId.Value).ToList()
        If Not ids.Any() Then
            Return New XPInstantFeedbackSource
        End If

        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListInventorySupplieByIds(ids)
    End Function

    ''' <summary>
    ''' Retorna los productos por unidad funcional según parámetros de solicitudes
    ''' </summary>
    ''' <remarks></remarks>
    Public Function DatasourceProductsByRequestParams(ListViewRequestParamXpo As List(Of ViewRequestParamXpo)) As XPInstantFeedbackSource
        If Not ListViewRequestParamXpo.Any() Then
            Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListInventoryProductByProductType("4")
        End If

        Dim ids = ListViewRequestParamXpo.Where(Function(d) d.Type = 2).Select(Function(d) d.ProductId.Value).ToList()
        If Not ids.Any() Then
            Return New XPInstantFeedbackSource
        End If

        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListInventoryProductByIds(ids)
    End Function

#End Region

End Class
