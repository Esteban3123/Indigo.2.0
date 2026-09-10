'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 09-04-2014
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
    Function GetEarningsType(code As String, validityId As Integer, type As Integer, audit As AuditMessage) As RevenueType Implements IBudgetServiceEarningsType.GetEarningsType
        Using service As IEarningsTypeAdminService = Container.Current.Resolve(Of IEarningsTypeAdminService)()
            Return service.GetEarningsType(code.Trim(), validityId, type, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un tipo de ingreso by validity
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetEarningsTypeByValidity(code As String, ValidityId As String, audit As AuditMessage) As RevenueType Implements IBudgetServiceEarningsType.GetEarningsTypeByValidity
        Using service As IEarningsTypeAdminService = Container.Current.Resolve(Of IEarningsTypeAdminService)()
            Return service.GetEarningsTypeByValidity(code.Trim(), ValidityId, audit)
        End Using
    End Function

    ''' <summary>
    ''' Lista los tipos de ingreso por vigencia
    ''' </summary>
    ''' <param name="ValidityId">The validity identifier.</param>
    ''' <returns></returns>
    Public Function ListEarningsTypeByValidity(ValidityId As Integer) As List(Of RevenueType) Implements IBudgetServiceEarningsType.ListEarningsTypeByValidity
        Using service As IEarningsTypeAdminService = Container.Current.Resolve(Of IEarningsTypeAdminService)()
            Return service.ListEarningsTypeByValidity(ValidityId)
        End Using
    End Function

    ''' <summary>
    ''' Elimina un tipo de ingreso
    ''' </summary>
    ''' <param name="earningsType">La entidad</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    Function DeleteEarningsType(earningsType As RevenueType, audit As AuditMessage) As ActionResult Implements IBudgetServiceEarningsType.DeleteEarningsType
        Using service As IEarningsTypeAdminService = Container.Current.Resolve(Of IEarningsTypeAdminService)()
            Return service.DeleteEarningsType(earningsType, audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o Actualiza un tipo de ingreso
    ''' </summary>
    ''' <param name="earningsType">la entidad</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Function SaveEarningsType(earningsType As RevenueType, idSequense As Int64, audit As AuditMessage) As ActionResult(Of RevenueType) Implements IBudgetServiceEarningsType.SaveEarningsType
        Using service As IEarningsTypeAdminService = Container.Current.Resolve(Of IEarningsTypeAdminService)()
            Return service.SaveEarningsType(earningsType, audit, idSequense)
        End Using
    End Function

    ''' <summary>
    ''' metodo para cambiar el estado a la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Public Function ChangeStateEarningsType(code As String, validityId As Integer, type As Integer, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.RevenueType) Implements IBudgetServiceEarningsType.ChangeStateEarningsType
        Using service As IEarningsTypeAdminService = Container.Current.Resolve(Of IEarningsTypeAdminService)()
            Return service.ChangeStateEarningsType(code, validityId, type, state, audit)
        End Using
    End Function

End Class