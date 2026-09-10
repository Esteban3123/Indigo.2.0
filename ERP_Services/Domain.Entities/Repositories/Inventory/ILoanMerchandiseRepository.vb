'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Rafael Eduardo Patiño Cabrera
' Created          : 24-04-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface ILoanMerchandiseRepository
    Inherits IRepository(Of LoanMerchandise)
    Inherits IRepositoryRollbackStrategy
    ''' <summary>
    ''' obtiene una remision por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetLoanMerchandiseByCode(code As String) As LoanMerchandise
    ''' <summary>
    ''' obtiene una remision por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetLoanMerchandiseById(id As Integer, Optional tracking As Boolean = True) As LoanMerchandise
End Interface
