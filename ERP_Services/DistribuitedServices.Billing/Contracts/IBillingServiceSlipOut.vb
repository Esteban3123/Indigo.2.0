'***********************************************************************
' Assembly         : DistributedService.Billing
' Author           : Carlos Ernesto Cordoba
' Created          : 13-11-2014
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities

#End Region

<ServiceContract()>
Public Interface IBillingServiceSlipOut
    ''' <summary>
    ''' Obtiene una boleta de salida por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetSlipOutByCode(code As String, audit As AuditMessage) As Domain.Entities.SlipOut
    ''' <summary>
    ''' Obtiene una boleta de salida por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetSlipOutById(Id As Integer) As SlipOut
    ''' <summary>
    ''' Guarda una boleta de salida
    ''' </summary>
    ''' <param name="SlipOut"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveSlipOut(SlipOut As Domain.Entities.SlipOut, idSequence As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.SlipOut)
    ''' <summary>
    ''' Obtiene una boleta de salida por numero de admisión
    ''' </summary>
    ''' <param name="numberAdmission"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetSlipOutByNumberAdmission(numberAdmission As String, audit As AuditMessage) As Domain.Entities.SlipOut
End Interface
