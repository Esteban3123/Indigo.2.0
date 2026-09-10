'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Ernesto Cordoba
' Created          : 08-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class RemissionOutputRepository
    Inherits GenericRepository(Of RemissionOutput)
    Implements IRemissionOutputRepository

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
    ''' obtiene una remision por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetRemissionOutputByCode(code As String) As RemissionOutput Implements IRemissionOutputRepository.GetRemissionOutputByCode
        Dim res = (From ro In _context.RemissionOutput Where ro.Code = code Select ro).FirstOrDefault()
        If res IsNot Nothing Then
            res.CodeNameCustomer = (From c In _context.Customer.AsNoTracking() Where c.Id = res.CustomerId Select String.Concat(c.Nit, " - ", c.Name)).FirstOrDefault()
            Dim Warehouse = (From w In Me._context.Warehouse.AsNoTracking() Where w.Id = res.WarehouseId Select w).FirstOrDefault
            res.CodeNameWareHouse = Warehouse.Code + " - " + Warehouse.Name
            res.Prefix = Warehouse.Prefix
            res.OriginalValue = (From ro In _context.RemissionOutput.AsNoTracking() Where ro.Code = code Select ro).FirstOrDefault()
            Return res
        Else
            Return New RemissionOutput
        End If
    End Function

    ''' <summary>
    ''' obtiene una remision por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetRemissionOutputById(id As Integer) As RemissionOutput Implements IRemissionOutputRepository.GetRemissionOutputById
        Dim res = (From ro In _context.RemissionOutput.AsNoTracking() Where ro.Id = id Select ro).FirstOrDefault()
        If res IsNot Nothing Then
            res.CodeNameCustomer = (From c In _context.Customer.AsNoTracking() Where c.Id = res.CustomerId Select String.Concat(c.Nit, " - ", c.Name)).FirstOrDefault()
            res.CodeNameWareHouse = (From w In _context.Warehouse.AsNoTracking() Where w.Id = res.WarehouseId Select String.Concat(w.Code, " - ", w.Name)).FirstOrDefault()
            Return res
        Else
            Return New RemissionOutput
        End If
    End Function

    ''' <summary>
    ''' Genera el comprobante contable de la remisión de salida
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Public Function SP_GenerateJournalVoucherByRemissionOutput(Id As Integer, CodeUser As String) As SP_GenerateJournalVoucherByRemissionOutput_Result Implements IRemissionOutputRepository.SP_GenerateJournalVoucherByRemissionOutput
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_GenerateJournalVoucherByRemissionOutput(Id, CodeUser).SingleOrDefault
    End Function

End Class
