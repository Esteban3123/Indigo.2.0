'***********************************************************************
' Assembly         : Infrastructure.Data.CrystalRepository
' Author           : Diego Andrés Roldán
' Created          : 24-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Crystal
Imports Domain.Crystal.Entities

Public Class HCUNITHISRepository
    Inherits GenericRepository(Of HCUNITHIS)
    Implements IHCUNITHISRepository

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

    Public Function GetHCUNITHISByUFUCODIGO(ufucodigo As String) As List(Of HCUNITHIS) Implements IHCUNITHISRepository.GetHCUNITHISByUFUCODIGO
        Return (From b In _crystalContext.HCUNITHIS Where b.UFUCODIGO = ufucodigo Select b).ToList()
    End Function

    Public Function GetHCUNITHISByUFUCODIGOWithFACMECONINS(ufucodigo As String) As HCUNITHIS Implements IHCUNITHISRepository.GetHCUNITHISByUFUCODIGOWithFACMECONINS
        Dim res = (From b In _crystalContext.HCUNITHIS Where b.UFUCODIGO = ufucodigo AndAlso b.FACMECONINS IsNot Nothing Select b).FirstOrDefault()
        If res IsNot Nothing AndAlso Not String.IsNullOrEmpty(res.UFUCODIGO) Then
            Return res
        Else
            Return New HCUNITHIS()
        End If
    End Function
End Class
