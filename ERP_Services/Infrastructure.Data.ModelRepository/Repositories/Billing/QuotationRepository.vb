'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 27/01/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class QuotationRepository
    Inherits GenericRepository(Of Quotation)
    Implements IQuotationRepository

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
    ''' Obtiene un registro por código
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    Public Function GetQuotation(Code As String) As Quotation Implements IQuotationRepository.GetQuotation
        Dim res = (From bg In _context.Quotation Where bg.Code = Code Select bg).FirstOrDefault()
        If res IsNot Nothing Then

            If res.ThirdPartyId IsNot Nothing Then
                res.ThirdPartyDescription = (From t In _context.ThirdParty.AsNoTracking() Where t.Id = res.ThirdPartyId Select String.Concat(t.Nit, " - ", t.Name)).FirstOrDefault()
            End If

            res.OriginalValue = (From bg In _context.Quotation.AsNoTracking() Where bg.Code = Code Select bg).FirstOrDefault()
            Return res
        Else
            Return New Quotation
        End If
    End Function

    ''' <summary>
    ''' Obtiene un registro por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetQuotationById(Id As Integer) As Quotation Implements IQuotationRepository.GetQuotationById
        Dim res = (From bg In _context.Quotation Where bg.Id = Id Select bg).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        Else
            Return New Quotation
        End If
    End Function

    ''' <summary>
    ''' Proceso de cotización
    ''' </summary>
    ''' <param name="xmlData"></param>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    Public Function SP_SaveQuotation(xmlData As String, codeUser As String) As SP_SaveQuotation_Result Implements IQuotationRepository.SP_SaveQuotation
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveQuotation(xmlData, codeUser).SingleOrDefault
    End Function

End Class
