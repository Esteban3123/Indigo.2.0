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

Public Class HCPROCTERRepository
    Inherits GenericRepository(Of HCPROCTER)
    Implements IHCPROCTERRepository

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

    Public Function GetHCPROCTERByAuto(auto As Integer) As HCPROCTER Implements IHCPROCTERRepository.GetHCPROCTERByAuto
        Dim res = (From b In _crystalContext.HCPROCTER Where b.CODCONSEC = auto Select b).FirstOrDefault()
        If res IsNot Nothing AndAlso res.CODCONSEC > 0 Then
            Return res
        Else
            Return New HCPROCTER()
        End If
    End Function

End Class