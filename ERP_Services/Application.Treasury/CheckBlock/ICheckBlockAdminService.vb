'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 27-06-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ICheckBlockAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Bloquea un cheque
    ''' </summary>
    ''' <param name="checkBlock">The check block.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveCheckBlock(ByVal checkBlock As CheckBlock, ByVal audit As AuditMessage) As ActionResult(Of CheckBlock)

    ''' <summary>
    ''' Elimina un cheque bloqueado
    ''' </summary>
    ''' <param name="checkBlock">The check block.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteCheckBlock(ByVal checkBlock As CheckBlock, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un cheque bloqueado por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function GetCheckBlockById(ByVal Id As Integer, ByVal audit As AuditMessage) As CheckBlock

    ''' <summary>
    ''' Obtiene un cheque bloqueado por el id de la chequera y numero del cheque
    ''' </summary>
    ''' <returns></returns>
    Function GetCheckBlockByIdCheckBookAndNumber(ByVal IdCheckBook As Integer, ByVal checkNumber As Long, ByVal audit As AuditMessage) As ActionResult(Of CheckBlock)

End Interface
