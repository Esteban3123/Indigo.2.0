'***********************************************************************
' Assembly         : Infrastructure.Data.BatchSerialSequenceRepository
' Author           : Giovanny Plazas
' Created          : 25-08-2022
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base

#End Region

''' <summary>
''' Repositorio de la entidad Readecuaciones
''' </summary>
Public Class ReadjustmentsRepository
    Inherits GenericRepository(Of Readjustments)
    Implements IReadjustmentsRepository, Inject

#Region "Fields"

    ''' <summary>
    ''' Contexto de contabilidad
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="acountingContext">Contexto de cintabilidad</param>
    Public Sub New(ByVal acountingContext As IGlobalModelUnitOfWork)
        MyBase.New(acountingContext)
        Me._context = acountingContext
    End Sub

#End Region

#Region "IReadjustmentsRepository"



#End Region

#Region "Functions"
    Public Function GetReadjustmentsByRequestPackageStatus(RequestPackageDetailStatusId As Integer, Optional tracking As Boolean = False) As List(Of Readjustments) Implements IReadjustmentsRepository.GetReadjustmentsByRequestPackageStatus
        Dim res As Object
        If Not tracking Then
            res = (From r In _context.Readjustments
                   Where r.RequestPackageDetailStatusId = RequestPackageDetailStatusId
                   Select r).ToList()
        Else
            res = (From r In _context.Readjustments.Include("RequestPackageDetailStatus.PackagePersonalized.Package")
                   Where r.RequestPackageDetailStatusId = RequestPackageDetailStatusId
                   Select r).ToList()
        End If

        If res IsNot Nothing OrElse res.Count > 1 Then
            Return res
        Else
            Return New List(Of Readjustments)
        End If
    End Function

    Public Function GetReadjustmentsById(Id As Integer) As Readjustments Implements IReadjustmentsRepository.GetReadjustmentsById
        Dim res = (From r In _context.Readjustments
                   Where r.Id = Id
                   Select r).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        Else
            Return New Readjustments
        End If
    End Function
#End Region

End Class
