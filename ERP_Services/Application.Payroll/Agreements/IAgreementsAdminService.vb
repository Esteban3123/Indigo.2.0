'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Rafael Eduardo Patiño
' Created          : 13-01-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IAgreementsAdminService
    Inherits IDisposable
    ''' <summary>
    ''' Lista de empleados
    ''' </summary>
    ''' <param name="audit">Objeto Inf. auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListEmployee(ByVal audit As AuditMessage) As List(Of Employee)

    ''' <summary>
    ''' Obtiene un convenio por el consecutivo
    ''' </summary>
    ''' <param name="consecutive">consecutivo</param>
    ''' <param name="audit">Objeto Inf. Auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAgreementsC(ByVal consecutive As String, ByVal audit As AuditMessage) As AgreementsC

    ''' <summary>
    ''' Lista de convenios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAgreementsC(ByVal audit As AuditMessage) As List(Of AgreementsC)

    ''' <summary>
    ''' Funcion para guardar un convenio
    ''' </summary>
    ''' <param name="AgreementsC">Objeto convenio</param>
    ''' <param name="audit">Objeto inf. auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveAgreementsC(ByVal AgreementsC As AgreementsC, ByVal audit As AuditMessage) As ActionResult(Of AgreementsC)

    ''' <summary>
    ''' Funcion para eliminar una clase de convenios
    ''' </summary>
    ''' <param name="AgreementsC">Obj. convenio a eliminar</param>
    ''' <param name="audit">Objeto inf. auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteAgreementsC(ByVal AgreementsC As AgreementsC, ByVal audit As AuditMessage) As ActionResult
End Interface
