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


Public Class DashboardPharmacyDetailDevolutionRepository
    Inherits GenericRepository(Of ViewDashboardPharmacyDetailDevolution)
    Implements IDashboardPharmacyDetailDevolutionRepository

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
    Public Function ListDashboardPharmacyDetailDevolution(consecutive As Decimal, patientCode As String, admission As String) As List(Of ViewDashboardPharmacyDetailDevolution) Implements IDashboardPharmacyDetailDevolutionRepository.ListDashboardPharmacyDetailDevolution
        Return (From vdd In _crystalContext.ViewDashboardPharmacyDetailDevolution.AsNoTracking() Where vdd.Consecutivo = consecutive And vdd.CodigoPacienteDevolucion = patientCode.Trim() And vdd.Ingreso = admission).ToList()
    End Function
End Class
