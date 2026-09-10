'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Duván Albeiro Mejia Cortes 
' Created          : 2021-01-13
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

<ServiceContract()>
Public Interface IBudgetServicePublicPolicy
    ''' <summary>
    ''' Obtiene una política pública
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetPublicPolicy(ByVal code As String, audit As AuditMessage) As ActionResult(Of PublicPolicy)

    ''' <summary>
    ''' Guarda o Actualiza una política pública
    ''' </summary>
    ''' <param name="publicPolicy"></param>
    ''' <param name="idSequense"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SavePublicPolicy(publicPolicy As PublicPolicy, idSequense As Int64, audit As AuditMessage) As ActionResult(Of PublicPolicy)

    ''' <summary>
    ''' metodo para cambiar estado a la entidad
    ''' </summary>
    ''' <param name="PublicPolicyId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ChangeStatePublicPolicy(publicPolicyId As Integer, audit As AuditMessage) As ActionResult
End Interface

