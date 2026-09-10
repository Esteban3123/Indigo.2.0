'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 10-04-2014
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
    ''' Obtiene un tipo de ingreso
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetExpenseType(code As String, validityId As Integer, type As Integer, audit As AuditMessage) As RevenueType Implements IBudgetServiceExpenseType.GetExpenseType
        Using service As IExpenseTypeAdminService = Container.Current.Resolve(Of IExpenseTypeAdminService)()
            Return service.GetExpenseType(code.Trim(), validityId, type, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un tipo de ingreso by validity
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetExpenseTypeByValidity(code As String, ValidityId As String, audit As AuditMessage) As RevenueType Implements IBudgetServiceExpenseType.GetExpenseTypeByValidity
        Using service As IExpenseTypeAdminService = Container.Current.Resolve(Of IExpenseTypeAdminService)()
            Return service.GetExpenseTypeByValidity(code.Trim(), ValidityId, audit)
        End Using
    End Function

    ''' <summary>
    ''' Elimina un tipo de ingreso
    ''' </summary>
    ''' <param name="RevenueType">La entidad</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    Function DeleteExpenseType(RevenueType As RevenueType, audit As AuditMessage) As ActionResult Implements IBudgetServiceExpenseType.DeleteExpenseType
        Using service As IExpenseTypeAdminService = Container.Current.Resolve(Of IExpenseTypeAdminService)()
            Return service.DeleteExpenseType(RevenueType, audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o Actualiza un tipo de ingreso
    ''' </summary>
    ''' <param name="RevenueType">la entidad</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Function SaveExpenseType(RevenueType As RevenueType, idSequense As Int64, audit As AuditMessage) As ActionResult(Of RevenueType) Implements IBudgetServiceExpenseType.SaveExpenseType
        Using service As IExpenseTypeAdminService = Container.Current.Resolve(Of IExpenseTypeAdminService)()
            Return service.SaveExpenseType(RevenueType, audit, idSequense)
        End Using
    End Function

    ''' <summary>
    ''' Changes the type of the state expense.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Public Function ChangeStateExpenseType(code As String, validityId As Integer, type As Integer, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.RevenueType) Implements IBudgetServiceExpenseType.ChangeStateExpenseType
        Using service As IExpenseTypeAdminService = Container.Current.Resolve(Of IExpenseTypeAdminService)()
            Return service.ChangeStateExpenseType(code, validityId, type, state, audit)
        End Using
    End Function

End Class