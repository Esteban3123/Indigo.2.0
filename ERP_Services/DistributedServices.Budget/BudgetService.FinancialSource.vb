'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 02-04-2014
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
    ''' Obtiene una fuente de financiacion
    ''' </summary>
    ''' <returns></returns>
    Public Function GetFinancialSource(ByVal code As String, validityId As Integer, audit As AuditMessage) As FinancialSource Implements IBudgetServiceFinancialSource.GetFinancialSource
        Using service As IFinancialSourceAdminService = Container.Current.Resolve(Of IFinancialSourceAdminService)()
            Return service.GetFinancialSource(code.Trim(), validityId, audit)
        End Using
    End Function

    ''' <summary>
    ''' Elimina una fuente de financiación
    ''' </summary>
    ''' <param name="financialSource">La entidad</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    Public Function DeleteFinancialSource(financialSource As FinancialSource, audit As AuditMessage) As ActionResult Implements IBudgetServiceFinancialSource.DeleteFinancialSource
        Using service As IFinancialSourceAdminService = Container.Current.Resolve(Of IFinancialSourceAdminService)()
            Return service.DeleteFinancialSource(financialSource, audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o Actualiza una fuente de financiacion
    ''' </summary>
    ''' <param name="financialSource">la entidad</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Public Function SaveFinancialSource(financialSource As FinancialSource, idSequense As Int64, audit As AuditMessage) As ActionResult(Of FinancialSource) Implements IBudgetServiceFinancialSource.SaveFinancialSource
        Using service As IFinancialSourceAdminService = Container.Current.Resolve(Of IFinancialSourceAdminService)()
            Return service.SaveFinancialSource(financialSource, audit, idSequense)
        End Using
    End Function

    ''' <summary>
    ''' metodo para cambiar estado a la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Public Function ChangeStateFinancialSource(code As String, validityId As Integer, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FinancialSource) Implements IBudgetServiceFinancialSource.ChangeStateFinancialSource
        Using service As IFinancialSourceAdminService = Container.Current.Resolve(Of IFinancialSourceAdminService)()
            Return service.ChangeStateFinancialSource(code, validityId, state, audit)
        End Using
    End Function

End Class