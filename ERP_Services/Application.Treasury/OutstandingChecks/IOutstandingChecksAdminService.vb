'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IOutstandingChecksAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Saves the outstanding checks.
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <param name="idSequence">The identifier sequence.</param>
    ''' <returns></returns>
    Function SaveOutstandingChecks(ByVal outstandingChecks As OutstandingChecks, ByVal audit As AuditMessage) As ActionResult(Of OutstandingChecks)

    ''' <summary>
    ''' Deletes the outstanding checks.
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteOutstandingChecks(ByVal outstandingChecks As OutstandingChecks, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un cheque pendiente por id
    ''' </summary>
    ''' <returns></returns>
    Function GetOutstandingChecksById(ByVal Id As Integer) As OutstandingChecks

    ''' <summary>
    ''' Obtiene un cheque pendiente por id de la chequera y numero del cheque
    ''' </summary>
    ''' <returns></returns>
    Function GetOutstandingCheckByCheckBookIdAndCheckNumber(ByVal checkBookId As Integer, ByVal checkNumber As Long) As OutstandingChecks

    ''' <summary>
    ''' Obtiene el primer cheque que esta en espera por id de chequera
    ''' </summary>
    ''' <returns></returns>
    Function GetFirstOutstandingChecks(ByVal IdCheckBook As Integer) As OutstandingChecks

    ''' <summary>
    ''' Lista todos los cheques pendientes por id de la chequera
    ''' </summary>
    ''' <param name="IdCheckBook">The identifier check book.</param>
    ''' <returns></returns>
    Function ListOutstandingChecksByIdCheckBook(ByVal IdCheckBook As Integer) As List(Of OutstandingChecks)

End Interface
