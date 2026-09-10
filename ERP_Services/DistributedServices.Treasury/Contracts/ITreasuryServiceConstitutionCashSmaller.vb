'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Carlos Mario Arias Rubiano
' Created          : 17/01/2017
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface ITreasuryServiceConstitutionCashSmaller

    ''' <summary>
    ''' Guarda un registro
    ''' </summary>
    <OperationContract()>
    Function SaveConstitutionCashSmaller(ConstitutionCashSmaller As ConstitutionCashSmaller, idSequence As Int64, audit As AuditMessage) As ActionResult(Of ConstitutionCashSmaller)

    ''' <summary>
    ''' Confirma un registro
    ''' </summary>
    <OperationContract()>
    Function ConfirmConstitutionCashSmaller(ConstitutionCashSmaller As ConstitutionCashSmaller, idSequence As Int64, audit As AuditMessage) As ActionResult(Of ConstitutionCashSmaller)

    ''' <summary>
    ''' Obtiene un registro por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetConstitutionCashSmaller(code As String, audit As AuditMessage) As ActionResult(Of ConstitutionCashSmaller)

    ''' <summary>
    ''' Obtiene un registro por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetConstitutionCashSmallerById(id As Integer, tracking As Boolean, audit As AuditMessage) As ActionResult(Of ConstitutionCashSmaller)

End Interface