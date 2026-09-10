'***********************************************************************
' Assembly         : Infrastructure.Data.ModelRepository
' Author           : Carlos Ernesto Cordoba
' Created          : 19-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Public Class AnnualizedCashFlowModificationDetailRepository
    Inherits GenericRepository(Of AnnualizedCashFlowModificationDetail)
    Implements IAnnualizedCashFlowModificationDetailRepository

    'Contexto de Budget
    Private _context As IGlobalModelUnitOfWork

#Region "Buider"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
#End Region

    ''' <summary>
    ''' obtiene le detalle de la modificacion por id de la cabecera
    ''' </summary>
    ''' <param name="pacModificationId"></param>
    ''' <returns></returns>
    Public Function GetAnnualizedCashFlowModificationDetailByPACModificationId(pacModificationId As Integer) As List(Of AnnualizedCashFlowModificationDetail) Implements IAnnualizedCashFlowModificationDetailRepository.GetAnnualizedCashFlowModificationDetailByPACModificationId
        Dim res = (From acfmd In _context.AnnualizedCashFlowModificationDetail Where acfmd.PACModificationId = pacModificationId Select acfmd).ToList()
        For Each item In res
            Dim pac = (From acf In _context.AnnualizedCashFlow.AsNoTracking() Where acf.Id = item.AnnualizedCashFlowId Select acf).FirstOrDefault()
            Dim category = (From c In _context.Category.AsNoTracking() Where c.Id = pac.CategoryId Select c).FirstOrDefault()
            If category.FinancialSourceId IsNot Nothing Then
                item.CategoryResource = (From fs In _context.FinancialSource.AsNoTracking() Where fs.Id = category.FinancialSourceId Select String.Concat(fs.Code, " - ", fs.Name)).FirstOrDefault()
            End If
            item.CodeNameCategory = String.Concat(category.Code, " - ", category.Name)
            item.Month = pac.Month
            item.Balance = pac.Balance
        Next
        Return res
    End Function
End Class
