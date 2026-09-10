'***********************************************************************
' Assembly         : Domain.Glosas
' Author           : RafaelPatiño
' Created          : 11-04-2014
'
' Last Modified By : 
' Last Modified On :
'
' Copyright        : (c) . All rights reserved.
'**********************************************************************
Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class RadicateInvoiceDRepository
    Inherits GenericRepository(Of RadicateInvoiceD)
    Implements IRadicateInvoiceDRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''inicializa la nueva instancia de <see cref="ObjectionsReceptionCRepository" /> clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    ''' <summary>
    ''' Obtiene factura del detalle de un radicado 
    ''' </summary>
    ''' <param name="invoiceNumber">numero de factura</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRadicateD(invoiceNumber As String) As RadicateInvoiceD Implements IRadicateInvoiceDRepository.GetRadicateD
        Dim RadicateD = From e In _context.RadicateInvoiceD
                        Where e.InvoiceNumber = invoiceNumber And e.State = 2
                        Select e
        If RadicateD.Count > 0 Then
            Dim RadicateDData = RadicateD.SingleOrDefault
            RadicateDData.OriginalValue = (From e In _context.RadicateInvoiceD.AsNoTracking
                                           Where e.InvoiceNumber = invoiceNumber And e.State = 2
                                           Select e).SingleOrDefault
            Return RadicateDData
        End If
        Return New RadicateInvoiceD
    End Function
    ''' <summary>
    ''' Lista de factura de radicacion con oficio
    ''' </summary>
    ''' <param name="consecutive">numero de radicado del oficio</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListRadicateD(consecutive As String) As List(Of RadicateInvoiceD) Implements IRadicateInvoiceDRepository.GetListRadicateD
        Dim tmpRadicateInvoiceC = From e In _context.RadicateInvoiceC
                                  Where e.RadicatedConsecutive = consecutive
                                  Select e
        Dim RadicateD As List(Of RadicateInvoiceD)
        If tmpRadicateInvoiceC.SingleOrDefault.State = 4 Then
            RadicateD = (From e In _context.RadicateInvoiceD.Include("RadicateInvoiceC").Include("RadicateInvoiceC.Customer")
                         Where e.RadicateInvoiceC.RadicatedConsecutive = consecutive).ToList()
        Else
            RadicateD = (From e In _context.RadicateInvoiceD.Include("RadicateInvoiceC").Include("RadicateInvoiceC.Customer")
                         Where e.RadicateInvoiceC.RadicatedConsecutive = consecutive And e.State <> 4).ToList()
        End If
        Return RadicateD
    End Function
    ''' <summary>
    ''' Lista de RadicateD a eliminar
    ''' </summary>
    ''' <param name="ListD"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListDeleteRadicateD(ListD As List(Of String)) As List(Of RadicateInvoiceD) Implements IRadicateInvoiceDRepository.GetListDeleteRadicateD
        Dim ListRadicateD = (From e In _context.RadicateInvoiceD
                             Where ListD.Contains(e.InvoiceNumber) And e.State = 1
                             Select e).ToList()
        Return ListRadicateD
    End Function
    ''' <summary>
    ''' Lista de RadicateD para actualizar estado.
    ''' </summary>
    ''' <param name="RadicateCId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListRadicateDByRadicateCId(RadicateCId As Integer) As List(Of RadicateInvoiceD) Implements IRadicateInvoiceDRepository.GetListRadicateDByRadicateCId
        Dim ListRadicateD = (From e In _context.RadicateInvoiceD.Include("RadicateInvoiceC")
                             Where e.RadicateInvoiceCId = RadicateCId
                             Select e).ToList()
        Return ListRadicateD
    End Function
    ''' <summary>
    ''' Realiza eliminacion masiva de RadicateD 
    ''' </summary>
    ''' <param name="list"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteMasivo(list As List(Of RadicateInvoiceD)) As List(Of RadicateInvoiceD) Implements IRadicateInvoiceDRepository.DeleteMasivo
        Return _context.RadicateInvoiceD.RemoveRange(list).ToList()
    End Function
    ''' <summary>
    ''' Funcion para validar que las facturas que estan radicando NO esten ya ingresadas a otro oficio de radicacion
    ''' </summary>
    ''' <param name="ListStrInvoice"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListValidateRadicateD(ListStrInvoice As List(Of String)) As List(Of String) Implements IRadicateInvoiceDRepository.ListValidateRadicateD
        Dim ListRadicateD = (From e In _context.RadicateInvoiceD
                             Where ListStrInvoice.Contains(e.InvoiceNumber) And e.State = 1 Select e.InvoiceNumber).ToList()
        Return ListRadicateD
    End Function

    Public Function GetRadidicateDetailInvoiceCapited(radicateId As Integer) As List(Of ViewRadicateDetailInvoiceCapitated) Implements IRadicateInvoiceDRepository.GetRadidicateDetailInvoiceCapited
        Return (From e In _context.ViewRadicateDetailInvoiceCapitated Where e.RadicateInvoiceCId = radicateId Select e).ToList()
    End Function

    Public Function ValidateInvoiceCapitated(radicateId As Integer) As Integer Implements IRadicateInvoiceDRepository.ValidateInvoiceCapitated
        Return (From e In _context.SP_ValidateRadicateDetailInvoiceCapitated(radicateId)).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene los registros para generar el archivo plano MegaRIPS
    ''' </summary>
    ''' <param name="radicateInvoiceId">Id del radicado de la factura</param>
    ''' <returns>Lista de registros</returns>
    Public Function GetMegaRIPSByRadicateInvoiceId(radicateInvoiceId As Integer, companyCode As String) As List(Of SP_GenerateMegaRIPS_Result) Implements IRadicateInvoiceDRepository.GetMegaRIPSByRadicateInvoiceId
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_GenerateMegaRIPS(radicateInvoiceId, companyCode).ToList()
    End Function

End Class
