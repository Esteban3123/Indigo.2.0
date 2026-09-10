'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Judy Andrea Díaz Reyes
' Created          : 30-04-2019
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports System.Text
Imports DevExpress.Xpo
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.CommonRepository
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base
Imports Presentation.Controls.MVP
Imports Presentation.Payroll.MVP
#End Region

''' <summary>
''' Este presentador captura toda la logica aplicada en el frontal 
''' </summary>
Public Class PCMConfig

#Region "Variables and constructor"

    ''' <summary>
    ''' Variable para instanciar la interfaz
    ''' </summary>
    Dim _view As ICMConfig

    ''' <summary>
    ''' Se utiliza para instanciar la clase singleton
    ''' </summary>
    Dim _sessionValues As SessionValues

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz.
    ''' </summary>
    ''' <param name="iview">The iview.</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByVal iview As ICMConfig)
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

#Region "Metodos"
    Public Async Sub GetSequence()
        Using model As New MBlockRecordAndSequenceMixingStation(Me._view.MyTag)
            Me._view.Sequence = Await model.GetSequence()
        End Using
    End Sub


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetCenterAttentionByCode(code As String) As CrystalRepository.CentersXpo
        Dim filter As String = "CODCENATE = '" & code & "'"
        Return XpoServiceEx.Instance(_sessionValues.HisContainer).CrystalService.GetCollection(Of CrystalRepository.CentersXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene un Almacén 
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetWareHouseByCode(code As String) As List(Of WarehouseXpo)
        Dim filter As String = "Code = '" & code & "'"
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).InventoryService.GetCollection(Of WarehouseXpo)(Nothing, filter)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetCityByCode(code As String) As CommonCityXpo
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).CommonService.GetCityByCode(code)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetProductionLineById(id As Integer) As MixingStationProductionLineXpo
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetProductionLineById(id)
    End Function

    ''' <summary>
    ''' Inicia los tipos de dosis unitarias
    ''' </summary>
    Public Sub InitializeProductionLine()
        Using Model As New MBusqueda
            Me._view.ProductionLineDatasource = Model.ConsultarEntidades(eDataSource.ListProductionLine)
        End Using
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="ids"></param>
    ''' <returns></returns>
    Public Function GetProductionLineByIds(ids As String) As List(Of MixingStationProductionLineXpo)
        Dim filter As String = "Id in (" & ids & ")"
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of MixingStationProductionLineXpo)(Nothing, filter)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="mixingStationId"></param>
    ''' <param name="codeCenterAttention"></param>
    ''' <returns></returns>
    Public Function GetCMCAPLByCenterAttention(mixingStationId As Integer, codeCenterAttention As String) As Integer
        Dim filter As String = "IdMixingStation <> " & mixingStationId & " AND CodeCenterAttention = '" & codeCenterAttention & "' AND StateCA = 1"
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of CMCenterAttentionXpo)(Nothing, filter).Count
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="mixingStationId"></param>
    ''' <param name="codeCenterAttention"></param>
    ''' <returns></returns>
    Public Function ValidateCareCenterInOtherCM(mixingStationId As Integer, codeCenterAttention As String, productionLineId As Integer) As CMCenterAttentionXpo
        Dim filter As String = "IdMixingStation.Id <> " & mixingStationId & " AND CodeCenterAttention = '" & codeCenterAttention & "' AND StateCA = 1 AND IdProductionLine = " & productionLineId
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of CMCenterAttentionXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="mixingStationId"></param>
    ''' <param name="codeCenterAttention"></param>
    ''' <returns></returns>
    Public Function ValidateCareCenterInOtherCMByUnitDoseType(mixingStationId As Integer, codeCenterAttention As String, unitDoseTypeIds As List(Of Integer)) As CMCenterAttentionXpo
        Dim filterUnitDose As String = String.Empty

        If unitDoseTypeIds IsNot Nothing AndAlso unitDoseTypeIds.Any() Then
            filterUnitDose = String.Join(" OR ", unitDoseTypeIds.Select(Function(unitDoseTypeId) $"ProductionLine.ProductionLineUnitDoseTypeXpo[Id_UnitDoseType={unitDoseTypeId}]").ToArray())
        End If

        Dim filter As String = $"IdMixingStation.Id <> {mixingStationId} AND CodeCenterAttention = '{codeCenterAttention}' AND StateCA = 1 AND ({filterUnitDose})"
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of CMCenterAttentionXpo)(Nothing, filter).FirstOrDefault()
    End Function

    Public Function InitializeUsers() As DevExpress.Data.Linq.LinqInstantFeedbackSource
        Using model As New MFunctionalUnit("")
            Return model.ListAllUser(_sessionValues.SecurityContainer)
        End Using
    End Function

    ''' <summary>
    ''' Initializes the cargos.
    ''' </summary>
    Public Function InitializeCustomer() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).CommonService.ListCustomerByStatus(True)
    End Function

    ''' <summary>
    ''' Initializes the Customers With ExternalCareCenters.
    ''' </summary>
    Public Function InitializeCustomersWithExternalCareCenters() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.ListCustomersWithExternalCareCenters()
    End Function

    ''' <summary>
    ''' Cargar los centros de atención activos por cliente de manera asíncrona.
    ''' </summary>
    Public Function InitializeExternalCareCenterByCustomer(customerId As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.ListExternalCareCenterByCustomer(customerId)
    End Function

    ''' <summary>
    ''' Cargar los centros de atención activos por cliente XPO
    ''' </summary>
    ''' <returns></returns>
    Public Function InitializeExternalCareCenterByCustomerXpo(customerId As Integer) As List(Of ExternalCareCenterXpo)
        Dim filter As String = ("CustomerId = " & customerId & " AND Status = 1")
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of ExternalCareCenterXpo)(Nothing, filter)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Function ValidateExternalCareCenter(cmConfigId As Integer, externalCareCenterId As String, productionLineId As Integer) As CMExternalCareCenterXpo
        Dim filter As String = "CMConfigurationId.Id <> " & cmConfigId & " AND ExternalCareCenterId = " & externalCareCenterId & " AND ProductionLineId = " & productionLineId
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of CMExternalCareCenterXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Function ValidateExternalCareCenterByUnitDoseId(cmConfigId As Integer, externalCareCenterId As String, unitDoseTypeIds As List(Of Integer)) As CMExternalCareCenterXpo
        Dim filterUnitDose As String = String.Empty

        If unitDoseTypeIds IsNot Nothing AndAlso unitDoseTypeIds.Any() Then
            filterUnitDose = String.Join(" OR ", unitDoseTypeIds.Select(Function(productionLineId) $"MixingStationProductionLineXpo.ProductionLineUnitDoseTypeXpo[Id_UnitDoseType={productionLineId}]").ToArray())
        End If

        Dim filter As String = $"CMConfigurationId.Id <> {cmConfigId} AND ExternalCareCenterId = {externalCareCenterId} AND ({filterUnitDose})"
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of CMExternalCareCenterXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Funcion para traer los usuarios de autorización del formulario central de mezclas
    ''' </summary>
    ''' <returns></returns>
    Public Function ViewListUserByContainer() As List(Of ViewListUserByContainerCM)
        Dim _ContainerId As Integer = Convert.ToInt32(_sessionValues.IndigoContainerId)
        Dim _TenantId As Short = 0
        'recupera el tenant de la compañia
        Dim _Company As Company = UnifiedConfiguration.Instance.ListCompanies.Where(Function(c) c.Id = _ContainerId).FirstOrDefault
        If _Company IsNot Nothing Then
            _TenantId = _Company.TenantId
        End If
        Dim filter = "(TenantId = 0 OR TenantId =" & _TenantId & ") AND (IdContainer=0 OR IdContainer =" & _ContainerId & ")"
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of ViewListUserByContainerCM)(Nothing, filter)
    End Function

#End Region

End Class
