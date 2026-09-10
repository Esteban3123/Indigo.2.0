'***********************************************************************
' Assembly         : Infrastructure.Data.PortfolioRepositiry
' Author           : Carlos Eernesto Cordoba
' Created          : 04-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class BlockRecordPortfolioRepository
    Inherits GenericRepository(Of BlockRecordPortfolio)
    Implements IBlockRecordPortfolioRepository

    'Contexto de cartera
    Private _context As IGlobalModelUnitOfWork

#Region "Builder"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
#End Region
    

#Region "Methods"
    ''' <summary>
    ''' Gets the block record portfolio by idform and identifier record.
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <param name="IdRecord">The identifier record.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Public Function GetBlockRecordPortfolioByIdformAndIdRecord(IdForm As String, IdRecord As String, Optional tracking As Boolean = True) As BlockRecordPortfolio Implements IBlockRecordPortfolioRepository.GetBlockRecordPortfolioByIdformAndIdRecord
        If tracking Then
            Dim blockRecordPortfolio = From e In _context.BlockRecordPortfolio
                    Where e.IdForm = IdForm AndAlso e.IdRecord = IdRecord
                    Select e
            If blockRecordPortfolio.Count() > 0 Then
                Return blockRecordPortfolio.FirstOrDefault
            Else
                Return New BlockRecordPortfolio()
            End If
        Else
            Dim blockRecordPortfolio = (From e In _context.BlockRecordPortfolio.AsNoTracking
                                Where e.IdForm = IdForm AndAlso e.IdRecord = IdRecord
                                Select e).FirstOrDefault

            If blockRecordPortfolio IsNot Nothing > 0 Then
                Return blockRecordPortfolio
            Else
                Return New BlockRecordPortfolio()
            End If
        End If
    End Function

    ''' <summary>
    ''' Lists all block record portfolio.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllBlockRecordPortfolio() As List(Of BlockRecordPortfolio) Implements IBlockRecordPortfolioRepository.ListAllBlockRecordPortfolio
        Dim blockRecordPortfolio = From e In _context.BlockRecordPortfolio
               Select e
        Return blockRecordPortfolio.ToList()
    End Function

    ''' <summary>
    ''' Lists the block record portfolio by identifier form.
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <returns></returns>
    Public Function ListBlockRecordPortfolioByIdForm(IdForm As String) As List(Of BlockRecordPortfolio) Implements IBlockRecordPortfolioRepository.ListBlockRecordPortfolioByIdForm
        Dim blockRecordPortfolio = From e In _context.BlockRecordPortfolio
                          Where e.IdForm = IdForm
                          Select e
        Return blockRecordPortfolio.ToList()
    End Function
#End Region

    
End Class
