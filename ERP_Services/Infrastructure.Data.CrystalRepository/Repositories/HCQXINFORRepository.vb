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

Public Class HCQXINFORRepository
    Inherits GenericRepository(Of HCQXINFOR)
    Implements IHCQXINFORRepository

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

    Public Function GetHCQXINFORByAuto(auto As Integer, tracking As Boolean) As HCQXINFOR Implements IHCQXINFORRepository.GetHCQXINFORByAuto
        Dim res As HCQXINFOR
        If tracking Then
            res = (From b In _crystalContext.HCQXINFOR Where b.AUTO = auto Select b).FirstOrDefault()
        Else
            res = (From b In _crystalContext.HCQXINFOR.AsNoTracking() Where b.AUTO = auto Select b).FirstOrDefault()
        End If
        If res IsNot Nothing AndAlso res.AUTO > 0 Then
            Return res
        Else
            Return New HCQXINFOR()
        End If
    End Function

End Class