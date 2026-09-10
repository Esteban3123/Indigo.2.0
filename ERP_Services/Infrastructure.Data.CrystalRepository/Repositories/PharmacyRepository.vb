'***********************************************************************
' Assembly         : Infrastructure.Data.CrystalRepository
' Author           : Carlos Ernesto Cordoba
' Created          : 2015-03-25
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Crystal
Imports Domain.Crystal.Entities

Public Class PharmacyRepository
    Inherits GenericRepository(Of HCFARMEPC)
    Implements IPharmacyRepository


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


    Public Function GetPharmacyByConsecutive(consecutive As Decimal) As HCFARMEPC Implements IPharmacyRepository.GetPharmacyByConsecutive
        Dim res = (From p In _crystalContext.HCFARMEPC Where p.CODCONCEC = consecutive Select p).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        Else
            Return New HCFARMEPC
        End If
    End Function

    Public Function GetPharmacyByConsecutiveWithDetail(consecutive As Decimal) As HCFARMEPC Implements IPharmacyRepository.GetPharmacyByConsecutiveWithDetail
        Dim res = (From p In _crystalContext.HCFARMEPC.Include("HCFARMEPD") Where p.CODCONCEC = consecutive Select p).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        Else
            Return New HCFARMEPC
        End If
    End Function

    ''' <summary>
    ''' valida que el paciente no tenga egresos por el numero de ingreso
    ''' </summary>
    ''' <param name="admissionNumber "></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPatientEgress(admissionNumber As String) As Boolean Implements IPharmacyRepository.GetPatientEgress
        Dim result = (From e In _crystalContext.CHREGEGRE Where e.NUMINGRES = admissionNumber Select e).FirstOrDefault()
        If result IsNot Nothing Then
            Return True
        End If
        Return False
    End Function


    ''' <summary>
    ''' Obtener una solicitud de paquete QX por consecutivo
    ''' </summary>
    ''' <param name="consecutive"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SurgicalPackageByConsecutive(consecutive As Decimal) As ViewDashBoardPharmacy_SurgicalPackage Implements IPharmacyRepository.SurgicalPackageByConsecutive
        Dim res = (From p In _crystalContext.ViewDashBoardPharmacy_SurgicalPackage Where p.ConsecutivoFarmacia = consecutive Select p).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        Else
            Return New ViewDashBoardPharmacy_SurgicalPackage
        End If
    End Function
End Class
