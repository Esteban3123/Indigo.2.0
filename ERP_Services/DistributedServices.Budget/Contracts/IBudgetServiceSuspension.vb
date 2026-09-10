'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Juan Carlos Bermudez  
' Created          : 18-09-2015
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
Public Interface IBudgetServiceSuspension

    ''' <summary>
    ''' obtiene una suspencion presupuestal por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetSuspensionByCode(code As String, audit As AuditMessage) As Domain.Entities.Suspension
    ''' <summary>
    ''' obtiene una suspencion presupuestal por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetSuspensionById(id As Integer) As Suspension

    ''' <summary>
    ''' Guarda una suspencion presupuestal
    ''' </summary>
    ''' <param name="suspension"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveSuspension(suspension As Suspension, idSequense As Int64, audit As AuditMessage) As ActionResult(Of Suspension)

End Interface
