'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 16/03/2021
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
Imports System.Globalization
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Presentation.Billing.MVP
Imports DevExpress.Data.PLinq
Imports Domain.Entities
Imports Infrastructure.Data.Xpo.InventoryRepository

#End Region

''' <summary>
''' Este presentador captura toda la logica aplicada en el frontal 
''' </summary>
Public Class PCampaigns

#Region "Variables"

    ''' <summary>
    ''' Se utiliza para instanciar la clase singleton
    ''' </summary>
    Public _sessionValues As SessionValues

    ''' <summary>
    ''' Referencia a la vista del frontal
    ''' </summary>
    Private _view As ICampaign
#End Region

#Region "Builder"

    Public Sub New()
        Me._sessionValues = SessionValues.Instance
    End Sub

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="iView">Referencia a la vista del frontal</param>
    Public Sub New(ByRef iView As ICampaign)
        If iView Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        _view = iView
        Me._sessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Carga los permisos asignados al usuario en éste formulario
    ''' </summary>
    Public Sub LoadPermissionsForm(tag As String)
        Using model As New MLiquidation()
            Dim permissions = model.GetPermissions(tag)
            Me._view.PermissionsForm = (From a In permissions Select Action = a.TagButton, Name = [Enum].GetName(GetType(PermissionsActionsForm), a.TagButton)).ToDictionary(Function(x) x.Action, Function(y) y.Name)
        End Using
    End Sub

    ''' <summary>
    ''' Carga la información de las solicitudes
    ''' </summary>
    ''' <returns></returns>
    Public Function ListViewListRequestsByCampaign(cmConfigurationId As Integer) As List(Of ViewListRequestsByCampaignXpo)
        Dim filter As String = "CMConfigurationId = " & cmConfigurationId
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of ViewListRequestsByCampaignXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Carga la materia prima de las solicitudes
    ''' </summary>
    ''' <returns></returns>
    Public Function ListViewListRawMaterial(campaignDetailId As Integer) As List(Of ViewListRawMaterialXpo)
        Dim filter As String = "CampaignDetailId = " & campaignDetailId
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of ViewListRawMaterialXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Carga las campañas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCampaignDetailXPInstantFeedbackSource(campaignId As Integer, Optional FilterStatus As String = Nothing) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.ListCampaignDetailXPInstantFeedbackSource(campaignId, FilterStatus)
    End Function

    ''' <summary>
    ''' Carga las campañas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCampaignDetailXPInstantFeedbackSourceAndCount(campaignId As Integer, Optional FilterStatus As String = Nothing) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.ListCampaignDetailXPInstantFeedbackSourceAndCount(campaignId, FilterStatus)
    End Function

    ''' <summary>
    ''' Obtiene la información principal de la campaña para control resumen.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCampaignMainInfoXPInstantFeedbackSourceAndCount(CampaignId As Integer, Optional FilterStatus As String = Nothing) As (XPInstantFeedbackSource, Integer)
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.ListCampaignMainInfoXPInstantFeedbackSource(CampaignId, FilterStatus)
    End Function

    ''' <summary>
    ''' Obtiene la campaña por id
    ''' </summary>
    ''' <returns></returns>
    Public Function GetCampaignByCMConfigurationId(cmConfigurationId As Integer) As CampaignXpo
        Dim filter As String = "CMConfigurationId = " & cmConfigurationId
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of CampaignXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene los detalles de los pacientes asociados a la solicitud
    ''' </summary>
    ''' <returns></returns>
    Public Function ListViewListDetailPatients(RequestMixingStationDetailId As Integer, Optional status As String = Nothing) As List(Of ViewListDetailPatientsXpo)
        Dim filter As String = String.Format("RequestMixingStationDetailId = {0} {1}", RequestMixingStationDetailId, IIf(String.IsNullOrEmpty(status), "", $"AND Status IN ({status})"))
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of ViewListDetailPatientsXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Obtengo el almacén por el tipo de paramterizacion de la Central de mezclas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListViewMixingStationWarehouse(cmConfigurationId As Integer, type As Integer) As ViewListWarehouseTypeXpo
        Dim filter As String = String.Format("IdMixingStation = {0} and WarehouseType = {1}", cmConfigurationId, type)
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of ViewListWarehouseTypeXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Carga el Almacen Maquila establecido en el contrato externo
    ''' </summary>
    ''' <returns></returns>
    Public Function ViewWarehouseMaquilaId(CampaignDetailId As Integer) As ViewWarehouseMaquilaXpo
        Dim filter As String = "CampaignDetailId = " & CampaignDetailId
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of ViewWarehouseMaquilaXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Traer el la solicitud de forma individual
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function SingleCampaingDetailWithRequests(Id As String) As ViewListCampaignDetailWithRequestsXpo
        Dim filter As String = "Id = '" & Id & "'"
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of ViewListCampaignDetailWithRequestsXpo)(Nothing, filter).FirstOrDefault
    End Function

    ''' <summary>
    ''' Obtiene los Productos por Ids
    ''' </summary>
    ''' <returns></returns>
    Public Function ListRequestPackageDetailStatus(Ids As String) As List(Of RequestPackageDetailStatusXpo)
        Dim filter As String = String.Format("Id IN ({0})", Ids)
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of RequestPackageDetailStatusXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Obtiene el los registros del Kardex para llenar la rejilla
    ''' </summary>
    ''' <returns></returns>
    Public Function ListViewCampaignKardex(CampaignDetailId As Integer, codeId As Integer, Optional batchSerialId As Integer = 0) As List(Of ViewCampaignKardexXpo)
        Dim filter As String = String.Empty
        Dim FilterValue = String.Format(" AND CodeId {0}", IIf(codeId = 0, " IS NULL ", $" ={codeId}"))
        filter = String.Format("CampaignDetailId = {0}{1}", CampaignDetailId, FilterValue)
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of ViewCampaignKardexXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Obtiene el los registros del Kardex para llenar la rejilla de procesar ajuste
    ''' </summary>
    ''' <returns></returns>
    Public Function ListViewProcessSetting(CampaignDetailId As Integer, codeId As Integer, Optional batchSerialId As Integer = 0) As List(Of ViewProcessSettingXpo)
        Dim filter As String = String.Empty
        Dim FilterValue = String.Format(" AND CodeId {0}", IIf(codeId = 0, " IS NULL ", $" ={codeId} "))
        If codeId = 0 Then
            filter = String.Format("CampaignDetailId = {0}{1}", CampaignDetailId, FilterValue)
        Else
            filter = String.Format("CampaignDetailId = {0} AND BatchSerialId = {1}{2}", CampaignDetailId, batchSerialId, FilterValue)
        End If
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of ViewProcessSettingXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Obtengo los almacén de paramterizacion de la Central de mezclas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListViewMSWarehouse(cmConfigurationId As Integer) As List(Of ViewListWarehouseTypeXpo)
        Dim filter As String = String.Format("IdMixingStation = {0} ", cmConfigurationId)
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of ViewListWarehouseTypeXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Obtengo la estabilidad y concentración del paquete por el tipo de preparación
    ''' </summary>
    ''' <returns></returns>
    Public Function ListViewPackageConcentrationByPreparationType(campaignDetailId As Integer, preparationType As Integer, productId As Integer) As ViewPackageConcentrationByPreparationTypeXpo
        Dim filter As String = String.Format("CampaignDetailId = {0} and PreparationType = {1} and ProductId = {2}", campaignDetailId, preparationType, productId)
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of ViewPackageConcentrationByPreparationTypeXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtengo los detalles del QuantityRemaining (Remanentes ingresados) en la campaña por producto.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListViewQuantityRemaining(campaignDetailId As Integer, ProductId As Integer, BatchSerialId As Integer) As ViewQuantityRemainingXpo
        Dim filter As String = String.Format("CampaignDetailId = {0} and ProductId = {1} and BatchSerialId = {2}", campaignDetailId, ProductId, BatchSerialId)
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of ViewQuantityRemainingXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    '''  Obtiene informacion de producto por Id
    ''' </summary>
    ''' <param name="ProductId"></param>
    ''' <returns></returns>
    Public Function ListProductsATC(ProductId As Integer) As InventoryProductXpo
        Dim filter As String = String.Format("Id = {0} and Status = 1", ProductId)
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).InventoryService.GetCollection(Of InventoryProductXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene los detalles de la estabilidad filtrados por un campo
    ''' </summary>
    Private Function GetStabilityDetails(Of T)(fieldName As String, fieldValue As Integer) As List(Of T)
        Dim filter As String = $"{fieldName} = {fieldValue}"
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of T)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Obtiene la estabilidad por producto
    ''' </summary>
    Public Function GetStabilityDetailByProduct(ProductId As Integer) As List(Of ViewStabilityTableDetailXpo)
        Return GetStabilityDetails(Of ViewStabilityTableDetailXpo)("ProductIdMainMedicine", ProductId)
    End Function

    ''' <summary>
    ''' Obtiene la estabilidad por ATC
    ''' </summary>
    Public Function GetStabilityDetailByATC(ATCId As Integer) As List(Of ViewStabilityTableDetailXpo)
        Return GetStabilityDetails(Of ViewStabilityTableDetailXpo)("AtcIdMainMedicine", ATCId)
    End Function

    ''' <summary>
    '''  Obtiene ATC por Id
    ''' </summary>
    Public Function GetAtcById(ATCId As Integer) As ATCXpo
        Dim filter As String = $"Id = {ATCId} AND Status = 1"
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of ATCXpo)(Nothing, filter).FirstOrDefault
    End Function

    ''' <summary>
    ''' Obtiene la cantidad de los productos en cada estado de la campaña
    ''' </summary>
    ''' <returns></returns>
    Public Function GetSumQuantityByPreparationStatus(CampaignDetailId As Integer) As ViewSumQuantityByPreparationStatusXpo
        Dim Filter As String = "Id = " & CampaignDetailId
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of ViewSumQuantityByPreparationStatusXpo)(Nothing, Filter).FirstOrDefault()
    End Function

#End Region

End Class
