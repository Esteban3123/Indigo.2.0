'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Jhossept Kevin Garay
' Created          : 09-04-2014
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
Public Interface IBudgetServiceEarningsType
    ''' <summary>
    ''' Obtiene un tipo de ingreso
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetEarningsType(code As String, validityId As Integer, type As Integer, audit As AuditMessage) As RevenueType

    ''' <summary>
    ''' Obtiene un tipo de ingreso by validity
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetEarningsTypeByValidity(code As String, ValidityId As String, audit As AuditMessage) As RevenueType

    ''' <summary>
    ''' Lista los tipos de ingreso por vigencia
    ''' </summary>
    ''' <param name="ValidityId">The validity identifier.</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function ListEarningsTypeByValidity(ValidityId As Integer) As List(Of RevenueType)

    ''' <summary>
    ''' Elimina un tipo de ingreso
    ''' </summary>
    ''' <param name="earningsType">La entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    <OperationContract()>
    Function DeleteEarningsType(earningsType As RevenueType, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Guarda o Actualiza un tipo de ingreso
    ''' </summary>
    ''' <param name="earningsType">la entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    <OperationContract()>
    Function SaveEarningsType(earningsType As RevenueType, idSequense As Int64, audit As AuditMessage) As ActionResult(Of RevenueType)

    ''' <summary>
    ''' metodo para cambiar el estado a la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    <OperationContract()>
    Function ChangeStateEarningsType(code As String, validityId As Integer, type As Integer, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.RevenueType)

End Interface

