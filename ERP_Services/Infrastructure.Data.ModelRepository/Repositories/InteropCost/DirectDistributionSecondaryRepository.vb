'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 11-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure


Public Class DirectDistributionSecondaryRepository
    Inherits GenericRepository(Of DirectDistributionSecondary)
    Implements IDirectDistributionSecondaryRepository

    ''' <summary>
    ''' The _context
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub



    Public Function GetDirectDistributionSecondary(code As String) As DirectDistributionSecondary Implements IDirectDistributionSecondaryRepository.GetDirectDistributionSecondary
        Dim res = (From e In _context.DirectDistributionSecondary.Include("DirectDistributionSecondaryDetail") Where e.Code = code Select e).FirstOrDefault
        If res IsNot Nothing Then
            For Each item In res.DirectDistributionSecondaryDetail
                item.CodeNameMeasureUnit = (From i In _context.InventoryMeasurementUnit.AsNoTracking() Where i.Id = item.MeasurementUnitId Select String.Concat(i.Code, " - ", i.Name)).FirstOrDefault()
                item.CodeNameProductionCenter = (From e In _context.ProductionCenter.AsNoTracking() Where e.Id = item.ProductionCenterId Select String.Concat(e.Code, " - ", e.Name)).FirstOrDefault()
            Next
            res.OriginalValue = (From e In _context.DirectDistributionSecondary.AsNoTracking() Where e.Code = code Select e).FirstOrDefault
            Return res
        Else
            Return New DirectDistributionSecondary
        End If
    End Function

    Public Function GetDirectDistributionSecondaryById(id As Integer) As DirectDistributionSecondary Implements IDirectDistributionSecondaryRepository.GetDirectDistributionSecondaryById
        Dim res = (From e In _context.DirectDistributionSecondary Where e.Id = id Select e).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From e In _context.DirectDistributionSecondary.AsNoTracking() Where e.Id = id Select e).FirstOrDefault
            Return res
        Else
            Return New DirectDistributionSecondary
        End If
    End Function

    ''' <summary>
    ''' Obtiene la cantidad de veces que esta el elemento de distribucion secundaria 
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCountByDistributionSecondaryId(id As Integer, month As Integer, year As Integer) As Integer Implements IDirectDistributionSecondaryRepository.GetCountByDistributionSecondaryId
        Dim result = (From e In _context.DirectDistributionSecondary.AsNoTracking() Where e.DistributionSecondaryId = id And e.Month = month And e.Year = year Select e.Code).Count()
        Return result
    End Function

    ''' <summary>
    ''' Actualiza el campo import en la tabla LogisticProductionCenterRecordDetail
    ''' </summary>
    ''' <param name="ObjectXml"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_UpdateFieldImport(ObjectXml As String, Status As Integer) As SP_UpdateFieldImport_Result Implements IDirectDistributionSecondaryRepository.SP_UpdateFieldImport
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_UpdateFieldImport(ObjectXml, Status).SingleOrDefault
    End Function

End Class
