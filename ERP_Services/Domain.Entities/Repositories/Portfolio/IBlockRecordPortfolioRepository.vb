'***********************************************************************
' Assembly         : Domain.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 04-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface IBlockRecordPortfolioRepository
    Inherits IRepository(Of BlockRecordPortfolio)

    ''' <summary>
    ''' Lists all block record portfolio.
    ''' </summary>
    ''' <returns></returns>
    Function ListAllBlockRecordPortfolio() As List(Of BlockRecordPortfolio)

    ''' <summary>
    ''' Gets the block record portfolio by idform and identifier record.
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <param name="IdRecord">The identifier record.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Function GetBlockRecordPortfolioByIdformAndIdRecord(ByVal IdForm As String, ByVal IdRecord As String, Optional tracking As Boolean = True) As BlockRecordPortfolio

    ''' <summary>
    ''' Lists the block record portfolio by identifier form.
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <returns></returns>
    Function ListBlockRecordPortfolioByIdForm(IdForm As String) As List(Of BlockRecordPortfolio)
End Interface
