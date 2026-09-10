'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Rafael Eduardo Patiño
' Created          : 03-01-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IKindsAgreementsAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Funcion para cargar una clase de convenios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetKindsAgreements(ByVal code As String, ByVal audit As AuditMessage) As KindsAgreements

    ''' <summary>
    ''' Funcion para guardar una clase de convenios
    ''' </summary>
    ''' <param name="KindsAgreements">Objeto clase de convenio</param>
    ''' <param name="audit">Objeto inf. auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveKindsAgreements(ByVal KindsAgreements As KindsAgreements, ByVal audit As AuditMessage) As ActionResult(Of KindsAgreements)

    ''' <summary>
    ''' Funcion para eliminar una clase de convenios
    ''' </summary>
    ''' <param name="KindsAgreements">Obj. clase de convenio a eliminar</param>
    ''' <param name="audit">Objeto inf. auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteKindsAgreements(ByVal KindsAgreements As KindsAgreements, ByVal audit As AuditMessage) As ActionMessageResult(Of KindsAgreements)

    ''' <summary>
    ''' Lista las clases de convenios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListKindsAgreements(ByVal audit As AuditMessage) As List(Of KindsAgreements)

End Interface
