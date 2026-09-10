'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 18/11/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Globalization
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Infrastructure.Data.Xpo.MixingStationRepository
#End Region

''' <summary>
''' Este presentador captura toda la logica aplicada en el frontal 
''' </summary>
Public Class PDashboardConfirmationUnitDose

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
    ''' Obtiene el listado de medicamentos
    ''' </summary>
    ''' <param name="cmConfigurationId"></param>
    ''' <returns></returns>
    Public Function ListViewListDashboardConfirmationUnitDose(cmConfigurationId As Integer) As List(Of ViewListDashboardConfirmationUnitDoseXpo)
        Dim filter As String = "CMConfigurationId = " & cmConfigurationId
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of ViewListDashboardConfirmationUnitDoseXpo)(Nothing, filter).ToList()
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
    ''' Obtiene las centrales de mezcla a las cuales el usuario tiene permiso
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCMByUserInstant() As XPInstantFeedbackSource
        Dim filter As String = "CMConfigurationUsersXpo[UserCode = '" & _sessionValues.UserIndigo & "']"
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.ListXPInstantFeedbackSource(Of MixinStationCMConfigXpo)(filter)
    End Function

    ''' <summary>
    ''' Datasource de las lineas de produccion
    ''' </summary>
    ''' <returns></returns>
    Public Function InitializeProductionLine(cmConfigurationId As Integer, careCenterCode As String, UnitDoseTypeId As Integer, SourceType As Integer) As List(Of ViewListProductionLineXpo)
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.ListViewListProductionLine(cmConfigurationId, careCenterCode, UnitDoseTypeId, SourceType)
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
    ''' Obtiene la linea de produccion por id
    ''' </summary>
    ''' <returns></returns>
    Public Function GetProductionLineById(Id As Integer) As ViewListProductionLineXpo
        Dim filter As String = "ProductionLineId = " & Id
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of ViewListProductionLineXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene los detalles del paquete
    ''' </summary>
    ''' <returns></returns>
    Public Function ListApplicationDetail(stringIds As String, sourceType As Integer) As List(Of ViewListApplicationDetailXpo)
        Dim filter As String = "Id in (" & stringIds & ") and SourceType = " & sourceType
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

    ''' <summary>
    ''' funcion que retorna la consulta de la vista
    ''' </summary>
    ''' <param name="AGRUPAQUETE"></param>
    ''' <returns></returns>
    Public Function GetHCFARMEPDbyAGRUPAQUETE(AGRUPAQUETE As String) As List(Of ViewListHCFARMEPDtoConfirmationUnitDoseXpo)
        Dim filter As String = String.Format("AGRUPAQUETE = '{0}'", AGRUPAQUETE)
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of ViewListHCFARMEPDtoConfirmationUnitDoseXpo)(Nothing, filter)
    End Function

    ''' <summary>
    ''' Obtiene el Volumen del diluyente mediante el medicamento cabecera del factor de dilucion
    ''' </summary>
    Public Function GetDilutionFactorByAtc(MainATCId As Integer, ThinnerATCId As Integer) As DilutionFactorsDetailXpo
        Dim filter As String = String.Format("DilutionFactors.ATCId = {0} AND AtcId = {1} AND ByDefault = 1", MainATCId, ThinnerATCId)
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetXPOObject(Of DilutionFactorsDetailXpo)(filter)
    End Function

#End Region

End Class
