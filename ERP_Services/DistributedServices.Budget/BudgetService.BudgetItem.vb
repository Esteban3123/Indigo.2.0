'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 28-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Application.Budget
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

#End Region

Partial Class BudgetService

    ''' <summary>
    ''' Obtiene un rubro por código y vigencia
    ''' </summary>
    '''<param name="Code">Código del Rubro</param>
    ''' <param name="BudgetaryValidityId">Id de la vigencia</param>
    ''' <returns></returns>
    Function GetBudgetItemByFinancialSourceIdAndCodeAndBudgetaryValidityIdAndItemType(FinancialSourceId As Integer?, Code As String, BudgetaryValidityId As Integer, ItemType As Byte, audit As AuditMessage) As ActionResult(Of Category) Implements IBudgetServiceBudgetItem.GetBudgetItemByFinancialSourceIdAndCodeAndBudgetaryValidityIdAndItemType
        Using service As IBudgetItemAdminService = Container.Current.Resolve(Of IBudgetItemAdminService)()
            Return service.GetBudgetItemByFinancialSourceIdAndCodeAndBudgetaryValidityIdAndItemType(FinancialSourceId, Code, BudgetaryValidityId, ItemType, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un rubro por código y vigencia
    ''' </summary>
    '''<param name="Code">Código del Rubro</param>
    ''' <param name="BudgetaryValidityId">Id de la vigencia</param>
    ''' <returns></returns>
    Function GetBudgetItemByCodeAndBudgetaryValidityIdAndItemType(Code As String, BudgetaryValidityId As Integer, ItemType As Byte, audit As AuditMessage) As ActionResult(Of Category) Implements IBudgetServiceBudgetItem.GetBudgetItemByCodeAndBudgetaryValidityIdAndItemType
        Using service As IBudgetItemAdminService = Container.Current.Resolve(Of IBudgetItemAdminService)()
            Return service.GetBudgetItemByCodeAndBudgetaryValidityIdAndItemType(Code, BudgetaryValidityId, ItemType, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un rubro por código y vigencia
    ''' </summary>
    '''<param name="Code">Código del Rubro</param>
    ''' <param name="BudgetaryValidityId">Id de la vigencia</param>
    ''' <returns></returns>
    Function GetBudgetAndFinancialSourceByItemByCodeAndBudgetaryValidityIdAndItemType(Code As String, BudgetaryValidityId As Integer, ItemType As Byte, audit As AuditMessage) As ActionResult(Of Category) Implements IBudgetServiceBudgetItem.GetBudgetAndFinancialSourceByItemByCodeAndBudgetaryValidityIdAndItemType
        Using service As IBudgetItemAdminService = Container.Current.Resolve(Of IBudgetItemAdminService)()
            Return service.GetBudgetAndFinancialSourceByItemByCodeAndBudgetaryValidityIdAndItemType(Code, BudgetaryValidityId, ItemType, audit)
        End Using
    End Function

    ''' <summary>
    ''' Listado de rubros por vigencia
    ''' </summary>
    ''' <param name="ValidityId">Id de la vigencia</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListBudgetItemsByValidity(ValidityId As String, ItemType As Byte, audit As AuditMessage) As List(Of Category) Implements IBudgetServiceBudgetItem.ListBudgetItemsByValidity
        Using service As IBudgetItemAdminService = Container.Current.Resolve(Of IBudgetItemAdminService)()
            Return service.ListBudgetItemsByValidity(ValidityId, ItemType, audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o Actualiza un rubro
    ''' </summary>
    ''' <param name="Category">la entidad</param>
    ''' <param name="audit">The session.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Function SaveBudgetItem(Category As Category, audit As AuditMessage) As ActionResult(Of Category) Implements IBudgetServiceBudgetItem.SaveBudgetItem
        Using service As IBudgetItemAdminService = Container.Current.Resolve(Of IBudgetItemAdminService)()
            Return service.SaveBudgetItem(Category, audit)
        End Using
    End Function

    ''' <summary>
    ''' Elimina un rubro
    ''' </summary>
    ''' <param name="Category">La entidad</param>
    ''' <param name="audit">The session.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    Function DeleteBudgetItem(Category As Category, audit As AuditMessage) As ActionResult Implements IBudgetServiceBudgetItem.DeleteBudgetItem
        Using service As IBudgetItemAdminService = Container.Current.Resolve(Of IBudgetItemAdminService)()
            Return service.DeleteBudgetItem(Category, audit)
        End Using
    End Function

    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="Category">The category.</param>
    ''' <returns></returns>
    Public Function ChangeStatusBudgetItem(Category As Category, status As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Category) Implements IBudgetServiceBudgetItem.ChangeStatusBudgetItem
        Using service As IBudgetItemAdminService = Container.Current.Resolve(Of IBudgetItemAdminService)()
            Return service.ChangeStatusBudgetItem(Category, status, audit)
        End Using
    End Function

    ''' <summary>
    ''' funcion para obtener el listado de
    ''' </summary>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Public Function GetAllBudgetItemsByState(state As Boolean, audit As AuditMessage) As List(Of Domain.Entities.Category) Implements IBudgetServiceBudgetItem.GetAllBudgetItemsByState
        Using service As IBudgetItemAdminService = Container.Current.Resolve(Of IBudgetItemAdminService)()
            Return service.GetAllBudgetItemsByState(state)
        End Using
    End Function

    Public Function CountCategories(audit As AuditMessage) As Integer Implements IBudgetServiceBudgetItem.CountCategories
        Using service As IBudgetItemAdminService = Container.Current.Resolve(Of IBudgetItemAdminService)()
            Return service.CountCategories()
        End Using
    End Function

    ''' <summary>
    ''' Obtiene el listado de disponibilidades para cargar el datasource del reporte de ejecución presupuestal por rubro de gastos
    ''' </summary>
    ''' <param name="ValidityId"></param>
    ''' <param name="BudgetId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListReportBudgetExecutionByCategoryExpense(ValidityId As Integer, BudgetId As Integer, session As SessionValues) As DataSet Implements IBudgetServiceBudgetItem.GetListReportBudgetExecutionByCategoryExpense
        Using service As IBudgetItemAdminService = Container.Current.Resolve(Of IBudgetItemAdminService)()
            Return service.GetListReportBudgetExecutionByCategoryExpense(ValidityId, BudgetId, session)
        End Using
    End Function

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
    Public Function GetListReportExpenditureBudgetSituation(validityId As Integer, month As Integer, financialSourceStart As String, financialSourceEnd As String, categoryStart As String, categoryEnd As String, session As SessionValues) As DataSet Implements IBudgetServiceBudgetItem.GetListReportExpenditureBudgetSituation
        Using service As IBudgetItemAdminService = Container.Current.Resolve(Of IBudgetItemAdminService)()
            Return service.GetListReportExpenditureBudgetSituation(validityId, month, financialSourceStart, financialSourceEnd, categoryStart, categoryEnd, session)
        End Using
    End Function

End Class