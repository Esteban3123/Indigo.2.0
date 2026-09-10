'***********************************************************************
' Assembly         : Infrastructure.Data.CrystalRepository
' Author           : Hector Rodriguez
' Created          : 18/03/2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Crystal
Imports Domain.Crystal.Entities
Imports System.Linq
Public Class RISGRIMAGERepository
    Inherits GenericRepository(Of RISGRIMAGE)
    Implements IRISGRIMAGERepository

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

    Public Function GetImagingGroups() As List(Of RISGRIMAGE) Implements IRISGRIMAGERepository.GetImagingGroups
        Dim res As List(Of RISGRIMAGE)
        res = New List(Of RISGRIMAGE)
        For Each b In _crystalContext.RISGRIMAGE
            res.Add(b)
        Next b
        Return res
    End Function

    Public Function GetImagingGroupById(id As Integer) As RISGRIMAGE Implements IRISGRIMAGERepository.GetImagingGroupById
        Dim res
        res = (From b In _crystalContext.RISGRIMAGE Where b.ID = id Select b).FirstOrDefault()
        If res IsNot Nothing AndAlso res.Id > 0 Then
            Return res
        Else
            Return New RISGRIMAGE()
        End If
    End Function

End Class
