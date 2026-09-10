'***********************************************************************
' Assembly         : Infrastructure.Data.PaymentsRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class SuppliersDistributionLinesRepository
    Inherits GenericRepository(Of SuppliersDistributionLines)
    Implements ISuppliersDistributionLinesRepository

    ''' <summary>
    ''' Contexto de payments
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payments
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene las lineas de distribucion por id del proveedor
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSuppliersDistributionLinesByIdSupplier(id As Integer, Optional tracking As Boolean = True) As List(Of SuppliersDistributionLines) Implements ISuppliersDistributionLinesRepository.GetSuppliersDistributionLinesByIdSupplier
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim list = (From dl In Me._context.SuppliersDistributionLines.Include("DistributionLines").Include("DistributionLines.MainAccounts").Include("Position") Where dl.IdSupplier = id Select dl).ToList
        Return list
    End Function

    ''' <summary>
    ''' Obtiene las lineas de distribucion por id del proveedor
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSuppliersDistributionLinesByIdSupplierSimple(id As Integer) As List(Of SuppliersDistributionLines) Implements ISuppliersDistributionLinesRepository.GetSuppliersDistributionLinesByIdSupplierSimple
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim list = (From dl In Me._context.SuppliersDistributionLines Where dl.IdSupplier = id Select dl).ToList
        If list IsNot Nothing AndAlso list.Count > 0 Then
            For Each item In list
                Dim line = (From x In _context.DistributionLines.AsNoTracking().Include("MainAccounts").AsNoTracking() Where x.Id = item.IdDistributionLine Select x).FirstOrDefault()
                item.DistributionLineDescription = line.Code + " - " + line.Name
                item.MainAccountDescription = line.MainAccounts.Number + " - " + line.MainAccounts.Name
                If item.PositionId IsNot Nothing Then
                    item.PositionCodeName = (From x In _context.Position.AsNoTracking() Where x.Id = item.PositionId Select x.Code + " - " + x.Name).FirstOrDefault()
                End If
            Next
        End If
        Return list
    End Function

    ''' <summary>
    ''' Obtiene una linea de distribucion por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSuppliersDistributionLinesById(id As Integer, Optional tracking As Boolean = True) As SuppliersDistributionLines Implements ISuppliersDistributionLinesRepository.GetSuppliersDistributionLinesById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If tracking Then
            Dim res = (From d In Me._context.SuppliersDistributionLines.Include("Supplier.PromptPaymentDiscount").Include("DistributionLines").Include("DistributionLines.MainAccounts") Where d.Id = id Select d)
            If res.Count > 0 Then
                res.SingleOrDefault.OriginalValue = (From d In Me._context.SuppliersDistributionLines.Include("DistributionLines").Include("DistributionLines.MainAccounts") Where d.Id = id Select d).SingleOrDefault()
                Return res.SingleOrDefault
            Else
                Return New SuppliersDistributionLines()
            End If
        Else
            Dim res = (From d In Me._context.SuppliersDistributionLines.AsNoTracking.Include("Supplier.PromptPaymentDiscount").AsNoTracking.Include("DistributionLines").AsNoTracking.Include("DistributionLines.MainAccounts") Where d.Id = id Select d)
            If res.Count > 0 Then
                Return res.SingleOrDefault
            Else
                Return New SuppliersDistributionLines()
            End If
        End If
        
    End Function

    ''' <summary>
    ''' Obtiene el porcentaje de ica de que maneja la linea de distribucion por unidad operativa
    ''' </summary>
    ''' <param name="idSupplierDistributionLine"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetICARetentionConceptBySupplierDistributionLine(idSupplierDistributionLine As Integer, OperatingUnitId As Integer) As RetentionConcepts Implements ISuppliersDistributionLinesRepository.GetICARetentionConceptBySupplierDistributionLine
        If idSupplierDistributionLine = 0 OrElse OperatingUnitId = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim idDistributtionLines = (From sdl In Me._context.SuppliersDistributionLines Where sdl.Id = idSupplierDistributionLine Select sdl.IdDistributionLine).FirstOrDefault
        If idDistributtionLines <> Nothing Then
            Dim res = (From d In Me._context.DistributionLinesICARetention.Include("AccountPayableConcepts").Include("AccountPayableConcepts.RetentionConcepts") Where d.DistributionLineId = idDistributtionLines And d.OperatingUnitId = OperatingUnitId Select d).FirstOrDefault()
            If res IsNot Nothing AndAlso res.AccountPayableConcepts IsNot Nothing AndAlso res.AccountPayableConcepts.RetentionConcepts IsNot Nothing Then
                Return res.AccountPayableConcepts.RetentionConcepts
            Else
                Return New RetentionConcepts
            End If
        Else
            Return New RetentionConcepts
        End If
    End Function

    Public Function GetAccountPayableICARetentionByDistributionLineIdAndOperatingUnitId(idSupplierDistributionLine As Integer, OperatingUnitId As Integer) As AccountPayableConcepts Implements ISuppliersDistributionLinesRepository.GetAccountPayableICARetentionByDistributionLineIdAndOperatingUnitId
        If idSupplierDistributionLine = 0 OrElse OperatingUnitId = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim idDistributtionLines = (From sdl In Me._context.SuppliersDistributionLines Where sdl.Id = idSupplierDistributionLine Select sdl.IdDistributionLine).FirstOrDefault
        If idDistributtionLines <> Nothing Then

            Dim res = (From apc In Me._context.AccountPayableConcepts
                       Join d In _context.DistributionLinesICARetention On apc.Id Equals d.AccountPayableConceptId
                       Where d.DistributionLineId = idDistributtionLines And d.OperatingUnitId = OperatingUnitId Select apc).FirstOrDefault()

            If res IsNot Nothing AndAlso res.Id > 0 Then
                res.MainAccounts = (From ma In Me._context.MainAccounts Where ma.Id = res.IdAccount Select ma).FirstOrDefault()
                res.RetentionConcepts = (From rc In Me._context.RetentionConcepts Where rc.Id = res.RetentionConceptId Select rc).FirstOrDefault()
            End If

            Return res

            'Return (From apc In Me._context.AccountPayableConcepts.Include("MainAccounts").Include("RetentionConcepts")
            '   Join d In _context.DistributionLinesICARetention On apc.Id Equals d.AccountPayableConceptId
            '   Where d.DistributionLineId = idDistributtionLines And d.OperatingUnitId = OperatingUnitId Select apc).FirstOrDefault()

            'Return (From d In Me._context.DistributionLinesICARetention.Include("AccountPayableConcepts")
            '    Join apc In _context.AccountPayableConcepts.Include("MainAccounts").Include("MainAccounts1").Include("MainAccounts2").Include("RetentionConcepts").Include("RetentionConcepts1").Include("RetentionConcepts2") On apc.Id Equals d.AccountPayableConceptId
            '    Where d.DistributionLineId = idDistributtionLines And d.OperatingUnitId = OperatingUnitId Select apc).FirstOrDefault()
        Else
            Return New AccountPayableConcepts
        End If
    End Function

    ''' <summary>
    ''' Obtiene de un proveedor, la lista de lineas de distribución por Concepto Acreencia
    ''' </summary>
    ''' <param name="idSupplier"></param>
    ''' <param name="accusationConcept"></param>
    ''' <returns></returns>
    Public Function GetDistributionLinesByIdSupplierAndAccusationConcept(idSupplier As Integer, accusationConcept As Integer) As List(Of SuppliersDistributionLines) Implements ISuppliersDistributionLinesRepository.GetDistributionLinesByIdSupplierAndAccusationConcept
        If idSupplier = 0 Then
            Throw New ArgumentNullException("idSupplier")
        End If
        Dim list = (From dl In Me._context.SuppliersDistributionLines.Include("DistributionLines") Where dl.IdSupplier = idSupplier AndAlso dl.DistributionLines.AccusationConcept = accusationConcept Select dl).ToList
        Return list
    End Function

End Class
