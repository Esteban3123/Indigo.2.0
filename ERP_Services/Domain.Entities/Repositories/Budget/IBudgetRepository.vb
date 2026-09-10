'***********************************************************************
' Assembly         : Domain.Budget
' Author           : Jeisson Herrera Peña
' Created          : 25/082015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "imports"
Imports Domain.Base
Imports Domain.Entities
#End Region

Public Interface IBudgetRepository
    Inherits IRepository(Of Budget)

    ''' <summary>
    ''' Obtiene un presupuesto por id
    ''' </summary>
    '''<param name="Id">Id del presupuesto</param>
    ''' <returns></returns>
    Function GetBudgetById(Id As Integer) As Budget
    ''' <summary>
    ''' obtiene un presupuesto por id del rubro y el tipo de ingreso
    ''' </summary>
    ''' <param name="categoryId"></param>
    ''' <param name="revenueTypeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBudgetByCategoryIdAndRevenueTypeId(categoryId As Integer, revenueTypeId As Integer) As Budget
    ''' <summary>
    ''' Obtiene el saldo de un presupuesto por id del rubro y del tipo
    ''' </summary>
    ''' <param name="categoryId"></param>
    ''' <param name="revenueTypeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBalanceBudgetByCategoryIdAndRevenueTypeId(categoryId As Integer, revenueTypeId As Integer) As Decimal
    ''' <summary>
    ''' Obtiene un detalle de presupuesto por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBudgetByIdAsNotTracking(id As Integer) As Budget

    Function GetThirdPartyByNit(nit As String) As String

End Interface
