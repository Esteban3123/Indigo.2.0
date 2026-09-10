'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 16/02/2021
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
#End Region

''' <summary>
''' Este presentador captura toda la logica aplicada en el frontal 
''' </summary>
Public Class PDashboardRequestMixingStation

#Region "Variables"

    ''' <summary>
    ''' Se utiliza para instanciar la clase singleton
    ''' </summary>
    Dim _sessionValues As SessionValues

#End Region

#Region "Builder"

    Public Sub New()
        Me._sessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene el listado de paquetes sin medicamentos
    ''' </summary>
    ''' <param name="cmConfigurationId"></param>
    ''' <returns></returns>
    Public Function ListViewListDashboardRequestMixingStation(cmConfigurationId As Integer) As List(Of ViewListDashboardRequestMixingStationXpo)
        Dim filter As String = "CMConfigurationId = " & cmConfigurationId
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of ViewListDashboardRequestMixingStationXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Obtiene el listado de medicamentos
    ''' </summary>
    ''' <param name="RequestMixingStationId"></param>
    ''' <returns></returns>
    Public Function ListViewListDashboardRequestMixingStationDetail(RequestMixingStationId As String) As List(Of ViewListDashboardRequestMixingStationDetailXpo)
        Dim filter As String = "RequestMixingStationDetailId in (" & RequestMixingStationId & ")"
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of ViewListDashboardRequestMixingStationDetailXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Obtiene las centrales de mezcla a las cuales el usuario tiene permiso
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCMByUser() As List(Of MixinStationCMConfigXpo)
        Dim filter As String = "CMConfigurationUsersXpo[UserCode = '" & _sessionValues.UserIndigo & "']"
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of MixinStationCMConfigXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Datasource de los paquetes
    ''' </summary>
    ''' <returns></returns>
    Public Function InitializePackage(atcId As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.ListPackageByATCId(atcId)
    End Function

    ''' <summary>
    ''' Obtiene los detalles del paquete
    ''' </summary>
    ''' <returns></returns>
    Public Function ListPackageDetailByPackageId(packageId As Integer) As List(Of MixinStationPackageDetailXpo)
        Dim filter As String = "PackageId.Id = " & packageId
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of MixinStationPackageDetailXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Obtiene el paquete por id
    ''' </summary>
    ''' <returns></returns>
    Public Function GetPackageAssociatedById(packageId As Integer) As MixinStationPackageXpo
        Dim filter As String = "Id = " & packageId
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of MixinStationPackageXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene los detalles del paquete
    ''' </summary>
    ''' <returns></returns>
    Public Function ListApplicationDetail(careCenterCode As String, productCode As String, dosage As Decimal) As List(Of ViewListApplicationDetailXpo)
        Dim filter As String = "CareCenterCode = '" & careCenterCode & "' and ProductCode = '" & productCode & "' and Dosage = " & dosage.ToString("F", CultureInfo.InvariantCulture)
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of ViewListApplicationDetailXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Obtiene los horarios de aplicación de un medicamento
    ''' </summary>
    ''' <returns></returns>
    Public Function GetSchedules(careCenterCode As String, productCode As String, dosage As Decimal, patientCode As String) As List(Of HCHOJAMEDXpo)
        Dim filter As String = "CODCENATE = '" & careCenterCode & "' and CODPRODUC = '" & productCode & "' and DOSISPROD = " & dosage.ToString("F", CultureInfo.InvariantCulture) & " and IPCODPACI = '" & patientCode & "'"
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of HCHOJAMEDXpo)(Nothing, filter).ToList()
    End Function

    Public Function InitializeProductionLine(MixingStationId As Integer, requestMixingStationDetailId As Integer) As XPInstantFeedbackSource
        Dim filter As String = $"CMMixingProducitonLineXpo[Id_CMConfiguration = {MixingStationId}] And ProductionLineUnitDoseTypeXpo[Id_UnitDoseType.RequestMixingStationDetailXpos[Id={requestMixingStationDetailId}]]"
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.ListXPInstantFeedbackSource(Of MixingStationProductionLineXpo)(filter)
    End Function

    Public Function MixingStationProductionLineXpo(MixingStationId As Integer, requestMixingStationDetailId As Integer) As List(Of MixingStationProductionLineXpo)
        Dim filter As String = $"CMMixingProducitonLineXpo[Id_CMConfiguration = {MixingStationId}] And ProductionLineUnitDoseTypeXpo[Id_UnitDoseType.RequestMixingStationDetailXpos[Id={requestMixingStationDetailId}]]"
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of MixingStationProductionLineXpo)(Nothing, filter).ToList()
    End Function

#End Region

End Class
