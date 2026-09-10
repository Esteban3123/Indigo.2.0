'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 17-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IRetirementReasonAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista Todos las Razones de Retiro
    ''' </summary>
    ''' <returns>Lista de Razones de Retiro</returns>
    Function ListAllRetirementReason() As List(Of RetirementReason)

    ''' <summary>
    ''' Obtiene una Razón de Retiro en específico
    ''' </summary>
    ''' <param name="code">Código de Razón de Retiro</param>
    ''' <returns>Razón de Retiro</returns>
    Function GetRetirementReason(ByVal code As String) As RetirementReason

    ''' <summary>
    ''' Graba o Actualiza la Razón de Retiro
    ''' </summary>
    ''' <param name="RetirementReason">Razón de Retiro</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    Function SaveRetirementReason(ByVal RetirementReason As RetirementReason, ByVal audit As AuditMessage, Optional idSequence As Int64 = 0) As ActionResult(Of RetirementReason)

    ''' <summary>
    ''' Elimina una Razón de Retiro
    ''' </summary>
    ''' <param name="RetirementReason">Razón de Retiro</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    Function DeleteRetirementReason(ByVal RetirementReason As RetirementReason, ByVal audit As AuditMessage) As ActionResult

    Function ChangeStateRetirementReason(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of RetirementReason)

End Interface
