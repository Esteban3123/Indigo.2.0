'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Duván Albeiro Mejia Cortes
' Created          : 2022-01-13
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
    ''' Obtiene una política pública
    ''' </summary>
    ''' <returns></returns>
    Public Function GetPublicPolicy(ByVal code As String, audit As AuditMessage) As ActionResult(Of PublicPolicy) Implements IBudgetServicePublicPolicy.GetPublicPolicy
        Using service As IPublicPolicyAdminService = Container.Current.Resolve(Of IPublicPolicyAdminService)()
            Return service.GetPublicPolicy(code.Trim(), audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o Actualiza una política pública
    ''' </summary>
    ''' <param name="publicPolicy"></param>
    ''' <param name="idSequense"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SavePublicPolicy(publicPolicy As PublicPolicy, idSequense As Int64, audit As AuditMessage) As ActionResult(Of PublicPolicy) Implements IBudgetServicePublicPolicy.SavePublicPolicy
        Using service As IPublicPolicyAdminService = Container.Current.Resolve(Of IPublicPolicyAdminService)()
            Return service.SavePublicPolicy(publicPolicy, audit, idSequense)
        End Using
    End Function

    ''' <summary>
    ''' Cambia el estado de una política pública
    ''' </summary>
    ''' <param name="publicPolicyId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ChangeStatePublicPolicy(publicPolicyId As Integer, audit As AuditMessage) As ActionResult Implements IBudgetServicePublicPolicy.ChangeStatePublicPolicy
        Using service As IPublicPolicyAdminService = Container.Current.Resolve(Of IPublicPolicyAdminService)()
            Return service.ChangeStatePublicPolicy(publicPolicyId, audit)
        End Using
    End Function
End Class