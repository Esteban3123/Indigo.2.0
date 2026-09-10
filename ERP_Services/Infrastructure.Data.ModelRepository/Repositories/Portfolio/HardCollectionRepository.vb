'***********************************************************************
' Assembly         : Infrastructure.Data.PortfolioRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 30-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Core.Objects

Public Class HardCollectionRepository
    Inherits GenericRepository(Of HardCollection)
    Implements IHardCollectionRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Public Function GetHardCollectionByCode(code As String) As HardCollection Implements IHardCollectionRepository.GetHardCollectionByCode
        Dim res = (From hc In _context.HardCollection Where hc.Code = code Select hc).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From hc In _context.HardCollection.AsNoTracking() Where hc.Code = code Select hc).FirstOrDefault()
            Return res
        End If
        Return New HardCollection
    End Function

    Public Function GetHardCollectionById(id As Integer) As HardCollection Implements IHardCollectionRepository.GetHardCollectionById
        Dim res = (From hc In _context.HardCollection Where hc.Id = id Select hc).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        End If
        Return New HardCollection
    End Function

    Public Function GenerateHardCollectionSP(xml As String, UserCode As String) As ObjectResult(Of SP_HardCollection_Result) Implements IHardCollectionRepository.GenerateHardCollectionSP
        Return _context.SP_HardCollection(xml, UserCode)
    End Function

    Public Function GetHardCollectionDetailByHardCollectionId(hardCollectionId As Integer) As List(Of HardCollectionDetail) Implements IHardCollectionRepository.GetHardCollectionDetailByHardCollectionId
        Dim res = (From hcd In _context.HardCollectionDetail Where hcd.HardCollectionId = hardCollectionId Select hcd).ToList()
        If res.Count > 0 Then
            For Each item In res
                item.InvoiceNumber = (From ar In _context.AccountReceivable.AsNoTracking() Where ar.Id = item.AccountReceivableId Select ar.InvoiceNumber).FirstOrDefault()
            Next
        End If
        Return res
    End Function

    Public Function SetInvoicesHardCollection(xml As String) As ObjectResult(Of SP_SetInvoicesHardCollection_Result) Implements IHardCollectionRepository.SetInvoicesHardCollection
        Return _context.SP_SetInvoicesHardCollection(xml)
    End Function
End Class
