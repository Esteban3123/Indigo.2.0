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

Public Class INCONSECURepository
    Inherits GenericRepository(Of INCONSECU)
    Implements IINCONSECURepository

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

    Public Function GetINCONSECUByID(id As String) As INCONSECU Implements IINCONSECURepository.GetINCONSECUByID
        Dim res = (From b In _crystalContext.INCONSECU Where b.IDCONSECU.Equals(id) Select b).FirstOrDefault()
        If res IsNot Nothing AndAlso Not String.IsNullOrEmpty(res.IDCONSECU) Then
            Return res
        Else
            Return New INCONSECU()
        End If
    End Function

End Class