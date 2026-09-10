'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 23-05-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ICancellationCheckAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Saves the cancellation check.
    ''' </summary>
    ''' <param name="cancellationCheck">The cancellation check.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveCancellationCheck(ByVal cancellationCheck As CancellationChecks, ByVal audit As AuditMessage, Optional ByVal withCommint As Boolean = True) As ActionResult(Of CancellationChecks)

    ''' <summary>
    ''' Obtener un registro de cheque cancelado
    ''' </summary>
    ''' <param name="IdEntityAccount">The identifier entity account.</param>
    ''' <param name="CheckNumber">The check number.</param>
    ''' <returns></returns>
    Function GetCancellationCheckByEntityAccountAndCheckNumber(ByVal IdEntityAccount As Integer, ByVal CheckNumber As String, ByVal audit As AuditMessage) As ActionResult(Of CancellationChecks)

    ''' <summary>
    ''' Obtener un registro de cheque cancelado por id de chequera y numero de cheque
    ''' </summary>
    ''' <returns></returns>
    Function GetCancellationCheckByCheckBookIdAndCheckNumber(ByVal checkBookId As Integer, ByVal CheckNumber As Long) As CancellationChecks

    ''' <summary>
    ''' Obtener un registro de cheque cancelado por id 
    ''' </summary>
    ''' <returns></returns>
    Function GetCancellationCheckById(ByVal id As Integer) As CancellationChecks

End Interface
