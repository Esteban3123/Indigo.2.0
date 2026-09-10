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

Public Class AGASICITARepository
    Inherits GenericRepository(Of AGASICITA)
    Implements IAGASICITARepository

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

    Public Function GetAGASICITAByAuto(auto As Integer) As AGASICITA Implements IAGASICITARepository.GetAGASICITAByAuto
        Dim res = (From b In _crystalContext.AGASICITA Where b.CODAUTONU = auto Select b).FirstOrDefault()
        If res IsNot Nothing AndAlso res.CODAUTONU > 0 Then
            Return res
        Else
            Return New AGASICITA()
        End If
    End Function

End Class
