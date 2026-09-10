'***********************************************************************
' Assembly         : Infrastructure.Data.ModelRepository
' Author           : Juan Carlos Bermudez
' Created          : 27-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Public Class AnnualizedCashFlowTransferRepository
    Inherits GenericRepository(Of AnnualizedCashFlowTransfer)
    Implements IAnnualizedCashFlowTransferRepository

#Region "Properties"

    'Contexto de Budget
    Private _context As IGlobalModelUnitOfWork

#End Region

#Region "Buider"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene un traslado del pac por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAnnualizedCashFlowTransferById(id As Integer) As AnnualizedCashFlowTransfer Implements IAnnualizedCashFlowTransferRepository.GetAnnualizedCashFlowTransferById
        Dim result = (From bm In _context.AnnualizedCashFlowTransfer.AsNoTracking.Include("AnnualizedCashFlowTransferDetail").AsNoTracking Where bm.Id = id Select bm).FirstOrDefault
        If result IsNot Nothing Then
            If result.AnnualizedCashFlowTransferDetail IsNot Nothing AndAlso result.AnnualizedCashFlowTransferDetail.Count > 0 Then
                For Each item In result.AnnualizedCashFlowTransferDetail
                    Dim annualizedCashFlow = (From acf In _context.AnnualizedCashFlow.AsNoTracking() Where acf.Id = item.AnnualizedCashFlowId Select acf).FirstOrDefault()

                    Dim category = (From c In _context.Category.AsNoTracking() Where c.Id = annualizedCashFlow.CategoryId Select c).FirstOrDefault
                    item.CodeNameCategory = category.Code + " - " + category.Name

                    item.Month = annualizedCashFlow.Month
                    item.Balance = annualizedCashFlow.Balance

                    If category.FinancialSourceId IsNot Nothing Then
                        Dim financialSource = (From fs In _context.FinancialSource.AsNoTracking() Where fs.Id = category.FinancialSourceId Select fs).FirstOrDefault
                        item.CodeNameFinancialSource = financialSource.Code + " - " + financialSource.Name
                    End If
                Next

            End If
            result.OriginalValue = (From bm In _context.AnnualizedCashFlowTransfer.AsNoTracking Where bm.Id = id Select bm).FirstOrDefault
            Return result
        Else
            Return New AnnualizedCashFlowTransfer
        End If
    End Function

    ''' <summary>
    ''' obtiene un traslado del pac por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAnnualizedCashFlowTrasnferByCode(code As String, type As Integer) As AnnualizedCashFlowTransfer Implements IAnnualizedCashFlowTransferRepository.GetAnnualizedCashFlowTrasnferByCode
        Dim result = (From e In _context.AnnualizedCashFlowTransfer.Include("AnnualizedCashFlowTransferDetail")
                      Where e.Code = code And e.DocumentSource = type Select e).FirstOrDefault
        If result IsNot Nothing Then
            If result.AnnualizedCashFlowTransferDetail IsNot Nothing AndAlso result.AnnualizedCashFlowTransferDetail.Count > 0 Then
                For Each item In result.AnnualizedCashFlowTransferDetail
                    Dim annualizedCashFlow = (From acf In _context.AnnualizedCashFlow.AsNoTracking() Where acf.Id = item.AnnualizedCashFlowId Select acf).FirstOrDefault()

                    Dim category = (From c In _context.Category.AsNoTracking() Where c.Id = annualizedCashFlow.CategoryId Select c).FirstOrDefault
                    item.CodeNameCategory = category.Code + " - " + category.Name

                    item.Month = annualizedCashFlow.Month
                    item.Balance = annualizedCashFlow.Balance

                    If category.FinancialSourceId IsNot Nothing Then
                        Dim financialSource = (From fs In _context.FinancialSource.AsNoTracking() Where fs.Id = category.FinancialSourceId Select fs).FirstOrDefault
                        item.CodeNameFinancialSource = financialSource.Code + " - " + financialSource.Name
                    End If
                Next

            End If
            result.OriginalValue = (From e In _context.AnnualizedCashFlowTransfer.AsNoTracking() Where e.Code = code And e.DocumentSource = type Select e).FirstOrDefault()
            Return result
        Else
            Return New AnnualizedCashFlowTransfer
        End If
    End Function

#End Region
    
End Class
