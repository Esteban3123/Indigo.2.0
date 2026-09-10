'***********************************************************************
' Assembly         : Application.Budget
' Author           : Jeisson Herrera Peña
' Created          : 25/08/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

Public Interface IBudgetAdminService
    Inherits IDisposable

#Region "Methods"

    ''' <summary>
    ''' Obtiene un presupuesto por id
    ''' </summary>
    '''<param name="Id">Id del presupuesto</param>
    ''' <returns></returns>
    Function GetBudgetById(Id As Integer, audit As AuditMessage) As ActionResult(Of Domain.Entities.Budget)
    ''' <summary>
    ''' Obtiene el saldo de un presupuesto por id del rubro y el tipo
    ''' </summary>
    ''' <param name="categoryId"></param>
    ''' <param name="revenueTypeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBalanceBudgetByCategoryIdAndRevenueTypeId(categoryId As Integer, revenueTypeId As Integer) As Decimal

#End Region

End Interface
