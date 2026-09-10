'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Carlos Cordoba
' Created          : 2015-01-24
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IDashboardPharmacyDetailRepository
    Inherits IRepository(Of ViewDashboardPharmacyDetail)
    ''' <summary>
    ''' lista los detalle de farmacia
    ''' </summary>
    ''' <param name="consecutive"></param>
    ''' <param name="patientCode"></param>
    ''' <param name="admission"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListDashboardPharmacyDetail(consecutive As Decimal, patientCode As String, admission As String) As List(Of ViewDashboardPharmacyDetail)

    ''' <summary>
    ''' metodo para obtener el nombre del tipo de estancia
    ''' </summary>
    ''' <param name="codePatient"></param>
    ''' <param name="admissionNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetStayType(codePatient As String, admissionNumber As String) As String

    ''' <summary>
    ''' Metodo que valida si la rias se puede agregar
    ''' </summary>
    ''' <returns></returns>
    Function SP_RIAS_ValidacionCUPSRIAS(Identification As String, RiasCupsId As Integer, CupsCode As String, RequestQuantity As Integer, RealizationDate As DateTime) As SP_RIAS_ValidacionCUPSRIAS_Result

    ''' <summary>
    ''' Lista un detalle de farmacia
    ''' </summary>
    ''' <param name="entityId"></param>
    ''' <returns></returns>
    Function DashboardPharmacyDetail(entityId As Integer) As ViewDashboardPharmacyDetail

End Interface
