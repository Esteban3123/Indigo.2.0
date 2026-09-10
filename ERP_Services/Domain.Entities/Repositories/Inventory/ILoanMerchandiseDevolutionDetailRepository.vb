'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Rafael Eduardo Patiño Cabrera
' Created          : 07-05-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface ILoanMerchandiseDevolutionDetailRepository
    Inherits IRepository(Of LoanMerchandiseDevolutionDetail)

    ''' <summary>
    ''' lista el detalla de la devolucion  de prestamo
    ''' </summary>
    ''' <param name="IdLoanMerchandiseDevolution"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListLoanMerchandiseDevolutionDetailByIdLoanMerchandise(IdLoanMerchandiseDevolution As Integer) As List(Of LoanMerchandiseDevolutionDetail)


    ''' <summary>
    ''' obtiene un detalle de la devolcuion de prestamo
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetLoanMerchandiseDevolutionDetailById(Id As Integer) As LoanMerchandiseDevolutionDetail
End Interface
