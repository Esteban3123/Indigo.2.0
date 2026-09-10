'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 12-09-2018
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities


Public Interface IForeclousureAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene un convenio por el consecutivo
    ''' </summary>
    ''' <param name="consecutive">consecutivo</param>
    ''' <param name="audit">Objeto Inf. Auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetForeclousure(ByVal consecutive As String, ByVal audit As AuditMessage) As Foreclousure

    ''' <summary>
    ''' Lista de convenios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListForeclousure(ByVal audit As AuditMessage) As List(Of Foreclousure)

    ''' <summary>
    ''' Funcion para guardar un convenio
    ''' </summary>
    ''' <param name="AgreementsC">Objeto convenio</param>
    ''' <param name="audit">Objeto inf. auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveForeclousure(ByVal Foreclousure As Foreclousure, ByVal audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of Foreclousure)

    ''' <summary>
    ''' Funcion para eliminar una clase de convenios
    ''' </summary>
    ''' <param name="AgreementsC">Obj. convenio a eliminar</param>
    ''' <param name="audit">Objeto inf. auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteForeclousure(ByVal Foreclousure As Foreclousure, ByVal audit As AuditMessage) As ActionResult
End Interface
