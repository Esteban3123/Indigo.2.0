'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 04-04-2014
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
    ''' Obtiene las validaciones de una entidad presupuestal
    ''' </summary>
    ''' <param name="BudgetEntityId">Id de la entidad presupuestal.</param>
    ''' <returns></returns>
    Function GetValidityByBudgetEntity(BudgetEntityId As String, audit As AuditMessage) As Object Implements IBudgetServiceValidity.GetValidityByBudgetEntity
        Using service As IValidityAdminService = Container.Current.Resolve(Of IValidityAdminService)()
            Return service.GetValidityByBudgetEntity(BudgetEntityId.Trim(), audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una validacion por Id
    ''' </summary>
    ''' <param name="Id">Id de la validacion.</param>
    ''' <returns></returns>
    Function GetValidity(Id As String, audit As AuditMessage) As BudgetaryValidity Implements IBudgetServiceValidity.GetValidity
        Using service As IValidityAdminService = Container.Current.Resolve(Of IValidityAdminService)()
            Return service.GetValidity(Id.Trim(), audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o Actualiza una Vigencia
    ''' </summary>
    ''' <param name="BudgetaryValidity">la entidad</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Function SaveValidity(BudgetaryValidity As BudgetaryValidity, audit As AuditMessage) As ActionResult(Of BudgetaryValidity) Implements IBudgetServiceValidity.SaveValidity
        Using service As IValidityAdminService = Container.Current.Resolve(Of IValidityAdminService)()
            Return service.SaveValidity(BudgetaryValidity, audit)
        End Using
    End Function

    ''' <summary>
    ''' Cierra una Vigencia
    ''' </summary>
    ''' <param name="BudgetaryValidity">la entidad</param>
    ''' <param name="type">Tipo 1. Ingresos 2. Gastos</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Function CloseValidity(BudgetaryValidity As BudgetaryValidity, type As Integer, audit As AuditMessage) As ActionResult(Of BudgetaryValidity) Implements IBudgetServiceValidity.CloseValidity
        Using service As IValidityAdminService = Container.Current.Resolve(Of IValidityAdminService)()
            Return service.CloseValidity(BudgetaryValidity, type, audit)
        End Using
    End Function

    ''' <summary>
    ''' Recalcula los valores de una Vigencia
    ''' </summary>
    ''' <param name="BudgetaryValidity">la entidad</param>
    ''' <param name="type">Tipo 1. Ingresos 2. Gastos</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Function RecalculateBalances(BudgetaryValidity As BudgetaryValidity, type As Integer, audit As AuditMessage) As ActionResult(Of BudgetaryValidity) Implements IBudgetServiceValidity.RecalculateBalances
        Using service As IValidityAdminService = Container.Current.Resolve(Of IValidityAdminService)()
            Return service.RecalculateBalances(BudgetaryValidity, type, audit)
        End Using
    End Function

    ''' <summary>
    ''' Elimina una vigencia
    ''' </summary>
    ''' <param name="BudgetaryValidity">La entidad</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    Function DeleteValidity(BudgetaryValidity As BudgetaryValidity, audit As AuditMessage) As ActionResult Implements IBudgetServiceValidity.DeleteValidity
        Using service As IValidityAdminService = Container.Current.Resolve(Of IValidityAdminService)()
            Return service.DeleteValidity(BudgetaryValidity, audit)
        End Using
    End Function

End Class