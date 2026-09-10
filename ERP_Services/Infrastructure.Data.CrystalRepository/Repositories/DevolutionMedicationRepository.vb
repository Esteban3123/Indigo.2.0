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

Public Class DevolutionMedicationRepository
    Inherits GenericRepository(Of HCDEVMEDC)
    Implements IDevolutionMedicationRepository

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


    Public Function GetDevolutionMedicationByConsecutive(consecutive As Integer) As HCDEVMEDC Implements IDevolutionMedicationRepository.GetDevolutionMedicationByConsecutive
        Dim res = (From p In _crystalContext.HCDEVMEDC Where p.CODCONCEC = consecutive Select p).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        Else
            Return New HCDEVMEDC
        End If
    End Function

    Public Function GetViewDashBoardPharmacyDevolutionByConsecutive(consecutive As Integer) As ViewDashBoardPharmacyDevolution Implements IDevolutionMedicationRepository.GetViewDashBoardPharmacyDevolutionByConsecutive
        Dim res = (From pdv In _crystalContext.ViewDashBoardPharmacyDevolution Where pdv.ConsecutivoDevoluciones = consecutive Select pdv).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        Else
            Return New ViewDashBoardPharmacyDevolution
        End If
    End Function
End Class
