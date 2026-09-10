'***********************************************************************
' Assembly         : Infrastructure.Data.CrystalRepository
' Author           : Hector Rodriguez Rubiano
' Created          : 23-07-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Crystal
Imports Domain.Crystal.Entities

Public Class HCORHEMSERRepository
    Inherits GenericRepository(Of HCORHEMSER)
    Implements IHCORHEMSERRepository

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

    Public Function GetHCORHEMSERByID(id As Integer) As HCORHEMSER Implements IHCORHEMSERRepository.GetHCORHEMSERByID
        Dim res = (From b In _crystalContext.HCORHEMSER Where b.ID = id Select b).FirstOrDefault()
        If res IsNot Nothing AndAlso res.ID > 0 Then
            Return res
        Else
            Return New HCORHEMSER()
        End If
    End Function

End Class
