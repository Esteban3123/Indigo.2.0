'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Rafael Eduardo Patiño Cabrera
' Created          : 07-05-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface ILoanMerchandiseDevolutionRepository
    Inherits IRepository(Of LoanMerchandiseDevolution)
    Inherits IRepositoryRollbackStrategy

    Function ListLoanMerchandiseDevolutionMassiveConfirm(listDocuments As List(Of String)) As List(Of LoanMerchandiseDevolution)

    ''' <summary>
    ''' obtiene una remision por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetLoanMerchandiseDevolutionByCode(code As String) As LoanMerchandiseDevolution
    ''' <summary>
    ''' obtiene una remision por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetLoanMerchandiseDevolutionById(id As Integer) As LoanMerchandiseDevolution
End Interface
