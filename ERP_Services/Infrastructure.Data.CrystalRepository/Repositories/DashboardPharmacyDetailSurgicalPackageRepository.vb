'***********************************************************************
' Assembly         : Infrastructure.Data.CrystalRepository
' Author           : Rafael Patiño
' Created          : 2018-08-31
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Crystal
Imports Domain.Crystal.Entities
Imports System.Dynamic


Public Class DashboardPharmacyDetailSurgicalPackageRepository
    Inherits GenericRepository(Of ViewDashBoardPharmacy_SurgicalPackageDeatils)
    Implements IDashboardPharmacyDetailSurgicalPackageRepository


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
    ''' listar paquetes QX
    ''' </summary>
    ''' <param name="ConsecutivoProgramacion"></param>
    ''' <param name="patientCode"></param>
    ''' <returns></returns>
    Public Function ListDashboardPharmacyDetailSurgicalPackage(ConsecutivoProgramacion As Decimal, patientCode As String) As List(Of ViewDashBoardPharmacy_SurgicalPackageDeatils) Implements IDashboardPharmacyDetailSurgicalPackageRepository.ListDashboardPharmacyDetailSurgicalPackage
        Dim state = 1
        Dim res = (From vdd In _crystalContext.ViewDashBoardPharmacy_SurgicalPackageDeatils.AsNoTracking() Where vdd.Consecutivo = ConsecutivoProgramacion And vdd.CodigoPaciente = patientCode.Trim() And vdd.Estado = state).ToList()
        If res.Count > 0 Then
            Return res
        Else
            Return New List(Of ViewDashBoardPharmacy_SurgicalPackageDeatils)
        End If
    End Function

    ''' <summary>
    ''' Devuelve un detalle de la solicitud del paquete QX
    ''' </summary>
    ''' <param name="consecutive"></param>
    ''' <param name="patientCode"></param>
    ''' <param name="productCode"></param>
    ''' <returns></returns>
    Public Function DashboardPharmacyDetailSurgicalPackage(consecutive As Decimal, patientCode As String, productCode As String) As ViewDashBoardPharmacy_SurgicalPackageDeatils Implements IDashboardPharmacyDetailSurgicalPackageRepository.DashboardPharmacyDetailSurgicalPackage
        Dim state = 1
        Dim res = (From vdd In _crystalContext.ViewDashBoardPharmacy_SurgicalPackageDeatils.AsNoTracking() Where vdd.Consecutivo = consecutive And vdd.CodigoPaciente = patientCode.Trim() And vdd.CodProducto = productCode And vdd.Estado = state).ToList()
        If res.Count > 0 Then
            Return res.FirstOrDefault
        Else
            Return New ViewDashBoardPharmacy_SurgicalPackageDeatils
        End If
    End Function
End Class
