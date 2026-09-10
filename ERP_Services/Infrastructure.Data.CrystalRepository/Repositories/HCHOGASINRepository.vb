'***********************************************************************
' Assembly         : Infrastructure.Data.CrystalRepository
' Author           : Diego Andrés Roldán
' Created          : 13-07-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Crystal
Imports Domain.Crystal.Entities

Public Class HCHOGASINRepository
    Inherits GenericRepository(Of HCHOGASIN)
    Implements IHCHOGASINRepository

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

    Public Function GetHCHOGASINByAuto(auto As Integer) As HCHOGASIN Implements IHCHOGASINRepository.GetHCHOGASINByAuto
        Dim res = (From b In _crystalContext.HCHOGASIN Where b.CONSECUTI = auto Select b).FirstOrDefault()
        If res IsNot Nothing AndAlso res.CONSECUTI > 0 Then
            Return res
        Else
            Return New HCHOGASIN()
        End If
    End Function

End Class