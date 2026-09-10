'***********************************************************************
' Assembly         : Application.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 28-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IBudgetItemAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene un rubro por código y vigencia
    ''' </summary>
    '''<param name="Code">Código del Rubro</param>
    ''' <param name="BudgetaryValidityId">Id de la vigencia</param>
    ''' <returns></returns>
    Function GetBudgetItemByFinancialSourceIdAndCodeAndBudgetaryValidityIdAndItemType(FinancialSourceId As Integer?, Code As String, BudgetaryValidityId As Integer, ItemType As Byte, audit As AuditMessage) As ActionResult(Of Category)

    ''' <summary>
    ''' Obtiene un rubro por código y vigencia
    ''' </summary>
    '''<param name="Code">Código del Rubro</param>
    ''' <param name="BudgetaryValidityId">Id de la vigencia</param>
    ''' <returns></returns>
    Function GetBudgetItemByCodeAndBudgetaryValidityIdAndItemType(Code As String, BudgetaryValidityId As Integer, ItemType As Byte, audit As AuditMessage) As ActionResult(Of Category)

    ''' <summary>
    ''' Obtiene un rubro por código y vigencia
    ''' </summary>
    '''<param name="Code">Código del Rubro</param>
    ''' <param name="BudgetaryValidityId">Id de la vigencia</param>
    ''' <returns></returns>
    Function GetBudgetAndFinancialSourceByItemByCodeAndBudgetaryValidityIdAndItemType(Code As String, BudgetaryValidityId As Integer, ItemType As Byte, audit As AuditMessage) As ActionResult(Of Category)

    ''' <summary>
    ''' Listado de rubros por vigencia
    ''' </summary>
    ''' <param name="ValidityId">Id de la vigencia</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListBudgetItemsByValidity(ValidityId As String, ItemType As Byte, audit As AuditMessage) As List(Of Category)

    ''' <summary>
    ''' Guarda o Actualiza un rubro
    ''' </summary>
    ''' <param name="Category">la entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Function SaveBudgetItem(Category As Category, audit As AuditMessage) As ActionResult(Of Category)

    ''' <summary>
    ''' Elimina un rubro
    ''' </summary>
    ''' <param name="Category">La entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    Function DeleteBudgetItem(Category As Category, audit As AuditMessage) As ActionResult


    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad.
    ''' </summary>
    ''' <param name="Category">The category.</param>
    ''' <returns></returns>
    Function ChangeStatusBudgetItem(Category As Category, status As Boolean, audit As AuditMessage) As ActionResult(Of Category)

    ''' <summary>
    ''' funcion para obtener el listado de 
    ''' </summary>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Function GetAllBudgetItemsByState(ByVal state As Boolean) As List(Of Category)

    ''' <summary>
    ''' Devuelve cuantos rubros hay
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CountCategories() As Integer

    ''' <summary>
    ''' Obtiene el listado de disponibilidades para cargar el datasource del reporte de ejecución presupuestal por rubro de gastos
    ''' </summary>
    ''' <param name="ValidityId"></param>
    ''' <param name="BudgetId"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListReportBudgetExecutionByCategoryExpense(ValidityId As Integer, BudgetId As Integer, session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que ejecuta el storeProcedure "Budget.SP_ReportExpenditureBudgetSituation"
    ''' </summary>
    ''' <param name="validityId"></param>
    ''' <param name="month"></param>
    ''' <param name="financialSourceStart"></param>
    ''' <param name="financialSourceEnd"></param>
    ''' <param name="categoryStart"></param>
    ''' <param name="categoryEnd"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListReportExpenditureBudgetSituation(validityId As Integer, month As Integer, financialSourceStart As String, financialSourceEnd As String, categoryStart As String, categoryEnd As String, session As SessionValues) As DataSet

End Interface
