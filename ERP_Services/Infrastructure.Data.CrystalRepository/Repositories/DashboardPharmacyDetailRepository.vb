'***********************************************************************
' Assembly         : Infrastructure.Data.CrystalRepository
' Author           : Juan F. Tamayo
' Created          : 2015-01-24
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Crystal
Imports Domain.Crystal.Entities
Imports System.Dynamic
Imports System.Data.Entity.Infrastructure

Public Class DashboardPharmacyDetailRepository
    Inherits GenericRepository(Of ViewDashboardPharmacyDetail)
    Implements IDashboardPharmacyDetailRepository

    'Contexto del repositorio de Indigo Vie Cloud Platform
    Private _crystalContext As ICrystalModelUnitOfWork

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="crystalContext">Contexto</param>
    Public Sub New(ByVal crystalContext As ICrystalModelUnitOfWork)
        MyBase.New(crystalContext)
        Me._crystalContext = crystalContext
    End Sub


    ''' <summary>
    ''' lista los detalle de farmacia
    ''' </summary>
    ''' <param name="consecutive"></param>
    ''' <param name="patientCode"></param>
    ''' <param name="admission"></param>
    ''' <returns></returns>
    Public Function ListDashboardPharmacyDetail(consecutive As Decimal, patientCode As String, admission As String) As List(Of ViewDashboardPharmacyDetail) Implements IDashboardPharmacyDetailRepository.ListDashboardPharmacyDetail
        Dim state = 1
        Dim res = (From vdd In _crystalContext.ViewDashboardPharmacyDetail.AsNoTracking() Where vdd.Consecutivo = consecutive And vdd.CodigoPaciente = patientCode.Trim() And vdd.Ingreso = admission.Trim() And vdd.Estado = state).ToList()

        If res.Count > 0 Then
            Return res
        Else
            Return New List(Of ViewDashboardPharmacyDetail)
        End If
    End Function


    Public Function GetStayType(codePatient As String, admissionNumber As String) As String Implements IDashboardPharmacyDetailRepository.GetStayType
        Return (From rs In _crystalContext.CHREGESTA Join tp In _crystalContext.CHTIPESTA On rs.CHTIPESTA.CODTIPEST Equals tp.CODTIPEST Where rs.INPACIENT.IPCODPACI = codePatient And rs.ADINGRESO.NUMINGRES = admissionNumber And rs.REGESTADO = 1 Select tp.DESTIPEST).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Metodo que valida si se puede agregar la rias
    ''' </summary>
    ''' <param name="Identification"></param>
    ''' <param name="RiasCupsId"></param>
    ''' <param name="CupsCode"></param>
    ''' <param name="RequestQuantity"></param>
    ''' <returns></returns>
    Public Function SP_RIAS_ValidacionCUPSRIAS(Identification As String, RiasCupsId As Integer, CupsCode As String, RequestQuantity As Integer, RealizationDate As DateTime) As SP_RIAS_ValidacionCUPSRIAS_Result Implements IDashboardPharmacyDetailRepository.SP_RIAS_ValidacionCUPSRIAS
        DirectCast(_crystalContext, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _crystalContext.SP_RIAS_ValidacionCUPSRIAS(Identification, RiasCupsId, CupsCode, RequestQuantity, 1, RealizationDate).SingleOrDefault()
    End Function

    ''' <summary>
    ''' Lista un detalle de farmacia
    ''' </summary>
    ''' <param name="entityId"></param>
    ''' <returns></returns>
    Public Function DashboardPharmacyDetail(entityId As Integer) As ViewDashboardPharmacyDetail Implements IDashboardPharmacyDetailRepository.DashboardPharmacyDetail
        Dim res = (From vdd In _crystalContext.ViewDashboardPharmacyDetail.AsNoTracking() Where vdd.EntityId = entityId And vdd.Estado = 1).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        Else
            Return New ViewDashboardPharmacyDetail
        End If

    End Function

End Class
