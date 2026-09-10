'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/02/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base
Imports Presentation.Controls.MVP
#End Region

''' <summary>
''' Este presentador captura toda la logica aplicada en el frontal 
''' </summary>
Public Class PRequestUnitDoseInventory

#Region "Variables"
    ''' <summary>
    ''' Variable para instanciar la interfaz
    ''' </summary>
    Dim _view As IRequestUnitDoseInventory

    ''' <summary>
    ''' Se utiliza para instanciar la clase singleton
    ''' </summary>
    Dim _sessionValues As SessionValues
#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz.
    ''' </summary>
    ''' <param name="iview">The iview.</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByVal iview As IRequestUnitDoseInventory)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me._view = iview
            Me._sessionValues = SessionValues.Instance
        End If
    End Sub

    Public Sub New()
        Me._sessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    Public Async Sub GetSequence()
        Using model As New MBlockRecordAndSequenceMixingStation(Me._view.MyTag)
            Me._view.Sequense = Await model.GetSequence()
        End Using
    End Sub

    Public Async Sub LoadDefinitionLayout()
        Await Me._view.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' Initializes the ATC.
    ''' </summary>
    Public Function InitializeATC() As XPInstantFeedbackSource
        Using Model As New MBusqueda
            Return Model.ConsultarEntidades(eDataSource.ListATC)
        End Using
    End Function

    ''' <summary>
    ''' Inicia los tipos de dosis unitarias
    ''' </summary>
    Public Function InitializeUnitDoseType() As XPInstantFeedbackSource
        Using Model As New MBusqueda
            Return Model.ConsultarEntidades(eDataSource.ListUnitDoseTypeByStatus)
        End Using
    End Function

    ''' <summary>
    ''' Datasource de los paquetes: Por tipo de Dosis unitaria 
    ''' </summary>
    ''' <returns></returns>
    Public Function InitializePackageByUnitDoseTypeId(UnitDoseTypeId) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.ListPackageByDoseType(UnitDoseTypeId)
    End Function

    ''' <summary>
    ''' Datasource de los paquetes
    ''' </summary>
    ''' <returns></returns>
    Public Function InitializePackage() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.ListPackageByStatus(True)
    End Function

    ''' <summary>
    ''' Datasource de los paquetes
    ''' </summary>
    ''' <returns></returns>
    Public Function InitializeCMConfiguration() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.ListCMConfigurationByStatus(True, New List(Of Integer)({2, 1}))
    End Function

    ''' <summary>
    ''' Datasource de los paquetes
    ''' </summary>
    ''' <returns></returns>
    Public Function InitializeWarehouse(careCenterCode As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).InventoryService.ListWarehouseByUserCodeAndCareCenterCode(True, _sessionValues.UserIndigo, careCenterCode)
    End Function

    ''' <summary>
    ''' Obtiene el paquete por id
    ''' </summary>
    ''' <returns></returns>
    Public Function GetUnitDoseTypeById(id As Integer) As MixinStationUnitDoseTypeXpo
        Dim filter As String = "Id = " & id
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of MixinStationUnitDoseTypeXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene los detalles de la tabla de estabilidad
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetListDetails(Id As Integer) As List(Of RequestUnitDoseInventoryDetailXpo)
        Dim filter As String = "RequestUnitDoseInventoryId.Id = " & Id
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of RequestUnitDoseInventoryDetailXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Trae los centro de atencion filtrados por central de mezclas
    ''' </summary>
    ''' <param name="MixingStationId"></param>
    ''' <returns></returns>
    Public Function InitializeCareCenter(MixingStationId As Integer) As List(Of CentersXpo)
        Dim filter As String = $"CMCenterAttentionXpo[IdMixingStation.Id = {MixingStationId}]"
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of CentersXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Initializes the route of administration
    ''' </summary>s
    Public Function InitializeAdministrationRoute() As List(Of MixingStationAdministrationRouteXpo)
        Dim filter As String = "Status = true"
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollectionAsList(Of MixingStationAdministrationRouteXpo)(Nothing, filter)
    End Function

    ''' <summary>
    ''' Obtiene un registro de tipo MixingStationPackage
    ''' </summary>s
    Public Async Function GetPackageXpoByIdAsync(_Id As Integer) As Task(Of MixinStationPackageXpo)
        Dim filter As String = $"Id = {_Id}"

        Return Await Task.Run(Function()
                                  Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer) _
                                  .MixingStationService.GetXPOObject(Of MixinStationPackageXpo)(filter)
                              End Function)
    End Function

#End Region

End Class
