'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Rafael Eduardo Patiño Cabrera
' Created          : 24-04-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface ILoanMerchandiseDetailRepository
    Inherits IRepository(Of LoanMerchandiseDetail)

    ''' <summary>
    ''' lista el detalla de solicitud de prestamo
    ''' </summary>
    ''' <param name="IdLoanMerchandise"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListLoanMerchandiseDetailByIdLoanMerchandise(IdLoanMerchandise As Integer, ByVal isDevolution As Boolean) As List(Of LoanMerchandiseDetail)
    ''' <summary>
    ''' obtiene un detalle de la remision por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetLoanMerchandiseDetailById(Id As Integer) As LoanMerchandiseDetail
    ''' <summary>
    ''' obtiene un item de detalle de una solicitud de prestamo con include de la cabecera
    ''' </summary>
    ''' <param name="Id">Id del item detalle de prestamo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetLoanMerchandiseDetailByIdWithIncludeLoan(Id As Integer) As LoanMerchandiseDetail
End Interface
