'***********************************************************************
' Assembly         : Application.Common
' Author           : Juan Diego Diaz
' Created          : 09-08-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Domain.Base.Entities

Public Interface IBlockRecordAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista Todos los registros bloqueados
    ''' </summary>
    ''' <returns>Registros bloqueados</returns>
    Function ListAllBlockRecord() As List(Of BlockRecord)

    ''' <summary>
    ''' Obtiene una registro de bloqueo según parametros
    ''' </summary>
    ''' <param name="IdForm">Id del formulario</param>
    ''' <param name="IdRecord">Id del registro</param>
    ''' <returns>Registro bloqueado</returns>
    Function GetBlockRecordByIdformAndIdRecord(ByVal IdForm As String, ByVal IdRecord As String) As BlockRecord

    ''' <summary>
    ''' Almacena o Actualiza registro bloqueado
    ''' </summary>
    ''' <param name="blockRecord">Registro bloqueado</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>ActionResult</returns>
    Function SaveBlockRecord(ByVal blockRecord As BlockRecord, ByVal audit As AuditMessage) As ActionResult(Of BlockRecord)

    ''' <summary>
    ''' Elimina una registro bloqueado
    ''' </summary>
    ''' <param name="blockRecord">Registro bloqueado</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>ActionResult</returns>
    Function DeleteBlockRecord(ByVal blockRecord As BlockRecord, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Limpiar regsitro de bloqueo
    ''' </summary>
    ''' <param name="CodUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_UnlockBlockRecord(ByVal CodUser As String, session As Infrastructure.CrossCutting.Base.SessionValues) As Boolean

End Interface
