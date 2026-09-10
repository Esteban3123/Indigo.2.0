'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 03-06-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ICheckAdminService
    Inherits IDisposable

    ''' <summary>
    ''' guarda una chequera
    ''' </summary>
    ''' <param name="check">The check.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveCheck(ByVal check As Checkbooks, ByVal audit As AuditMessage, Optional ByVal withCommit As Boolean = True) As ActionResult(Of Checkbooks)

    ''' <summary>
    ''' Elimina una chequera
    ''' </summary>
    ''' <param name="checks">The checks.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteCheck(ByVal checks As Checkbooks, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene una chequera por el id de la cuenta bancaria
    ''' </summary>
    ''' <param name="IdEntity">The identifier entity.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function GetCheckByIdEntityBankAccountAndStatus(ByVal IdEntity As Integer, ByVal status As Short, ByVal audit As AuditMessage) As ActionResult(Of Checkbooks)

End Interface
