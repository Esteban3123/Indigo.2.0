'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 11/02/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent.IndigoReference.MixingStation
Imports Presentation.Controls
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.MixingStationRepository
#End Region

''' <summary>
''' Este presentador captura toda la logica aplicada en el frontal 
''' </summary>
Public Class PRequestUnitDoseExternalCareCenter

#Region "Variables"
    ''' <summary>
    ''' Variable para instanciar la interfaz
    ''' </summary>
    Dim _view As IRequestUnitDoseExternalCareCenter

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
    Public Sub New(ByVal iview As IRequestUnitDoseExternalCareCenter)
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
    ''' Retorna las nutriciones parenterales parametrizadas desde el EHR
    ''' </summary>
    ''' <returns></returns>
    Public Function InitializeNPT() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.ListNPT
    End Function

    ''' <summary>
    ''' Datasource de los paquetes
    ''' </summary>
    ''' <returns></returns>
    Public Function InitializePackage() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.ListPackageByStatus(True)
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
    Public Function InitializeCMConfiguration() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.ListCMConfigurationByStatus(True, New List(Of Integer)({2, 3}))
    End Function

    ''' <summary>
    ''' Datasource de los paquetes
    ''' </summary>
    ''' <returns></returns>
    Public Function InitializeExternalCareCenter(MixingStationId As Integer) As List(Of ExternalCareCenterXpo)
        Dim filter As String = $"Status = 1 and ExternalCareCenterUsersXpo[UserCode = '{_sessionValues.UserIndigo}'] AND CMExternalCareCenterXpo[CMConfigurationId.Id = {MixingStationId}]"
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of ExternalCareCenterXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Datasource de los contratos para el centro de atención externo seleccionado
    ''' </summary>
    ''' <returns></returns>
    Public Function InitializeContractExternalClients(ExternalCareCenterId As Integer) As List(Of MixingStationRepository.ContractExternalClientsXpo)
        Dim filter As String = $"Status = 1 AND ExternalCareCenterXpo[Id = {ExternalCareCenterId}]"
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of MixingStationRepository.ContractExternalClientsXpo)(Nothing, filter).ToList
    End Function

    ''' <summary>
    ''' Datasource de los paquetes: Dosis Unitarias
    ''' </summary>
    ''' <returns></returns>
    Public Function ListUnitDoseType(MixingStationId As Integer, ExternalCareCenterId As Integer) As XPInstantFeedbackSource
        Dim filter As String = $"State = 1 And Not [MSClass] In (5,6,7) And ProductionLineUnitDoseTypeXpo[Id_ProductionLine.CMExternalCareCenterXpo[CMConfigurationId = {MixingStationId} And ExternalCareCenterId= {ExternalCareCenterId}]]"
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.ListXPInstantFeedbackSource(Of MixinStationUnitDoseTypeXpo)(filter)
    End Function

    ''' <summary>
    ''' Datasource de los pacientes externos
    ''' </summary>
    ''' <returns></returns>
    Public Function InitializePatientExternalCareCenter() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.ListPatientExternalCareCenterByStatus(True)
    End Function

    ''' <summary>
    ''' Obtiene el tipo de nutricion por medio de Id
    ''' </summary>
    ''' <returns></returns>
    Public Function GetNutritionTypeById(id As Integer) As HCPARNUTCXpo
        Dim filter As String = "ID = " & id
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetXPOObject(Of HCPARNUTCXpo)(filter)
    End Function

    ''' <summary>
    ''' Obtiene el paquete por id
    ''' </summary>
    ''' <returns></returns>
    Public Function GetUnitDoseTypeById(id As Integer) As MixinStationUnitDoseTypeXpo
        Dim filter As String = "Id = " & id
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetXPOObject(Of MixinStationUnitDoseTypeXpo)(filter)
    End Function

    ''' <summary>
    ''' Obtiene el contrato por Id
    ''' </summary>
    ''' <returns></returns>
    Public Function GetContractExternalClientsById(id As Integer) As MixingStationRepository.ContractExternalClientsXpo
        Dim filter As String = "Id = " & id
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of MixingStationRepository.ContractExternalClientsXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene los detalles de la tabla de estabilidad
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetListDetailsMaquila(Id As Integer) As List(Of RequestUnitDoseExternalCareCenterMaquilaXpo)
        Dim filter As String = "RequestUnitDoseExternalCareCenterId.Id = " & Id
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of RequestUnitDoseExternalCareCenterMaquilaXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Obtiene los detalles de la tabla de estabilidad
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetListDetailsPatient(Id As Integer) As List(Of RequestUnitDoseExternalCareCenterPatientXpo)
        Dim filter As String = "RequestUnitDoseExternalCareCenterId.Id = " & Id
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of RequestUnitDoseExternalCareCenterPatientXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Datasource de las vías de administración
    ''' </summary>
    ''' <param name="atcId"></param>
    ''' <returns></returns>
    Public Function InitializeAdministrationRoute(atcId As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).InventoryService.ListAdministrationRouteByAtcId(atcId)
    End Function

    ''' <summary>
    ''' Datasource de Dosis unitarias para Medicamentos 
    ''' </summary>
    ''' <returns></returns>
    Public Function InitializeUnitDoseTypeMed() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.ListUnitDoseTypeByMSClass()
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="atcId"></param>
    ''' <returns></returns>
    Public Function GetATCXpoById(atcId As Integer) As InventoryRepository.ATCXpo
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).InventoryService.GetATCXpo(atcId)
    End Function

    ''' <summary>
    ''' Obtiene una unidad de medida por el id
    ''' </summary>
    ''' <param name="measureUnitId"></param>
    ''' <returns></returns>
    Public Function MeasureUnitById(measureUnitId As Integer) As InventoryRepository.MeasureUnitXpo
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).InventoryService.MeasureUnitById(measureUnitId)
    End Function

    ''' <summary>
    ''' Lista unidades de medida por tipo de unidad
    ''' </summary>
    ''' <returns></returns>
    Public Function ListMeasureUnitByType(unitType As Byte) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).InventoryService.ListMeasureUnitByType(unitType)
    End Function

    ''' <summary>
    ''' Obtiene el género por Id
    ''' </summary>
    ''' <returns></returns>
    Public Function GetGenderTypeById(id As Integer) As MixingStationRepository.GenderTypesXpo
        Dim filter As String = "Id = " & id
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of MixingStationRepository.GenderTypesXpo)(Nothing, filter).FirstOrDefault()
    End Function

#End Region

End Class
