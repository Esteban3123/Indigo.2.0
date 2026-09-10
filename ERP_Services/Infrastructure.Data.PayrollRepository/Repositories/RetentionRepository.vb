'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 06-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports Infrastructure.Data.Base


Public Class RetentionRepository

    Inherits GenericRepository(Of Retention)
    Implements IRetentionRepository

    ' contexto de payroll
    Private _context As IPayrollUnitOfWork


    ''' <summary>
    ''' Inicia la instancia del contexto
    ''' </summary>
    ''' <param name="context">Contexto de payroll</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IPayrollUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene una Retención
    ''' </summary>
    ''' <param name="code">Código de la Retención</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRetention(code As String, Optional tracking As Boolean = True) As Retention Implements IRetentionRepository.GetRetention
        Dim retention As IQueryable(Of Retention)
        If tracking = True Then
            retention = From e In _context.Retention.Include("Consecutive1")
                        Where e.Code = code
                        Select e
        Else
            retention = From e In _context.Retention.AsNoTracking
                        Where e.Code = code
                        Select e
        End If
        If (retention.Count > 0) Then
            Dim objRetention = Nothing
            objRetention = retention.SingleOrDefault
            Return objRetention
        Else
            Return New Retention()
        End If
    End Function

    ''' <summary>
    ''' Lista Todas las Retenciones
    ''' </summary>
    ''' <returns>Retenciones</returns>
    ''' <remarks></remarks>
    Public Function ListAllRetention() As List(Of Retention) Implements IRetentionRepository.ListAllRetention
        Dim retention = From e In _context.Retention
                        Select e
        Return retention.ToList()
    End Function

    ''' <summary>
    ''' Obtiene una Retención por Año
    ''' </summary>
    ''' <param name="year">Año</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRetentionByYear(year As String) As List(Of Retention) Implements IRetentionRepository.GetRetentionByYear
        Dim retention = From e In _context.Retention.Include("Consecutive1")
                        Where e.Year = year
                        Select e
        If (retention.Count > 0) Then
            Return retention.ToList()
        Else
            Return Nothing
        End If
    End Function



    ''' <summary>
    ''' Obtiene un listado de rangos de retenciones para 383 y 384
    ''' </summary>
    ''' <param name="RetentionConceptId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListRetentionConceptById(RetentionConceptId As Integer) As RetentionConcepts Implements IRetentionRepository.GetListRetentionConceptById
        If RetentionConceptId = 0 Then
            Throw New ArgumentNullException("RetentionConceptId")
        End If
        Dim res = (From rcr In _context.RetentionConcepts.AsNoTracking() Where rcr.Id = RetentionConceptId Select rcr).FirstOrDefault
        If res IsNot Nothing Then
            Return res
        Else
            Return Nothing
        End If
    End Function


    ''' <summary>
    ''' Obtiene un listado de rangos de retenciones para 383 y 384
    ''' </summary>
    ''' <param name="RetentionConceptId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListRetentionRangeByRetentionConceptId(RetentionConceptId As Integer) As List(Of RetentionConceptRanges) Implements IRetentionRepository.GetListRetentionRangeByRetentionConceptId
        If RetentionConceptId = 0 Then
            Throw New ArgumentNullException("RetentionConceptId")
        End If
        Dim res = (From rcr In _context.RetentionConceptRanges.AsNoTracking().Include("RetentionConcepts").AsNoTracking() Where rcr.RetentionId = RetentionConceptId Select rcr).ToList
        If res IsNot Nothing AndAlso res.Count > 0 Then
            Return res
        Else
            Return Nothing
        End If
    End Function


    ''' <summary>
    ''' Obtiene un listado de rangos de retenciones para 383 y 384
    ''' </summary>
    ''' <param name="Number"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListRetentionRangeRetentionConceptIdByNumber(ByVal Number As String) As List(Of RetentionConceptRanges) Implements IRetentionRepository.GetListRetentionRangeRetentionConceptIdByNumber
        If Number = String.Empty Then
            Throw New ArgumentNullException("Number")
        End If
        Dim res = (From rcr In _context.RetentionConceptRanges.AsNoTracking Where rcr.RetentionConcepts.Name.Contains(Number) Select rcr).ToList()

        If res IsNot Nothing AndAlso res.Count > 0 Then
            Return res
        Else
            Return Nothing
        End If
    End Function



End Class
