'***********************************************************************
' Assembly         : Application.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 09-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IEarningsTypeAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene un tipo de ingreso
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetEarningsType(code As String, validityId As Integer, type As Integer, audit As AuditMessage) As RevenueType

    ''' <summary>
    ''' Obtiene un tipo de ingreso by validity
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetEarningsTypeByValidity(code As String, ValidityId As String, audit As AuditMessage) As RevenueType

    ''' <summary>
    ''' Lista los tipos de ingreso por vigencia
    ''' </summary>
    ''' <param name="ValidityId">The validity identifier.</param>
    ''' <returns></returns>
    Function ListEarningsTypeByValidity(ValidityId As Integer) As List(Of RevenueType)

    ''' <summary>
    ''' Elimina un tipo de ingreso
    ''' </summary>
    ''' <param name="earningsType">La entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    Function DeleteEarningsType(earningsType As RevenueType, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Guarda o Actualiza un tipo de ingreso
    ''' </summary>
    ''' <param name="earningsType">la entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Function SaveEarningsType(earningsType As RevenueType, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of RevenueType)

    ''' <summary>
    ''' metodo para cambiar el estado a la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function ChangeStateEarningsType(ByVal code As String, validityId As Integer, type As Integer, ByVal state As Boolean, audit As AuditMessage) As ActionResult(Of RevenueType)
End Interface
