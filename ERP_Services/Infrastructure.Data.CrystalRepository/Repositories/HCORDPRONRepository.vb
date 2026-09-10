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

Public Class HCORDPRONRepository
    Inherits GenericRepository(Of HCORDPRON)
    Implements IHCORDPRONRepository

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

    Public Function GetHCORDPRONByAuto(auto As Integer) As HCORDPRON Implements IHCORDPRONRepository.GetHCORDPRONByAuto
        Dim res = (From b In _crystalContext.HCORDPRON Where b.AUTO = auto Select b).FirstOrDefault()
        If res IsNot Nothing AndAlso res.AUTO > 0 Then
            Return res
        Else
            Return New HCORDPRON()
        End If
    End Function

End Class
