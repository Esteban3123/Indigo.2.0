'***********************************************************************
' Assembly         : Application.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 31-03-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IMoneyAdvanceAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza un anticipo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveMoneyAdvance(ByVal moenyAdvance As AdvancePayments, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0, Optional ByVal withCommit As Boolean = True) As ActionResult(Of AdvancePayments)

    ''' <summary>
    ''' Elimina un anticipo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteMoneyAdvance(ByVal moneyAdvance As AdvancePayments, ByVal audit As AuditMessage, Optional ByVal withCommit As Boolean = True) As ActionResult

    ''' <summary>
    ''' Obtiene una determinada dependencia
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetMoneyAdvance(ByVal code As String, ByVal audit As AuditMessage) As AdvancePayments

    ''' <summary>
    ''' Obtiene una determinada dependencia
    ''' </summary>
    ''' <param name="id">The code.</param>
    ''' <returns></returns>
    Function GetMoneyAdvanceById(ByVal id As Integer, ByVal audit As AuditMessage) As AdvancePayments

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeState(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of AdvancePayments)

    ''' <summary>
    ''' Lista todos los avances por tercero
    ''' </summary>
    ''' <param name="ThirdId">The third identifier.</param>
    ''' <returns></returns>
    Function ListAdvancePaymentByThirdId(ByVal ThirdId As Integer) As ActionResult(Of List(Of AdvancePayments))

    ''' <summary>
    ''' Obtiene un anticipo por código
    ''' </summary>
    ''' <param name="id">The code.</param>
    ''' <returns></returns>
    Function GetAdvanceByCode(ByVal code As String) As AdvancePayments

End Interface
