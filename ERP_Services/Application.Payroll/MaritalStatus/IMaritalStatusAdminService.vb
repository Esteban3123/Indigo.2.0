'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Antony F. Córdoba P.
' Created          : 20-12-2023
'S
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IMaritalStatusAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista todos los tipos de estado civil.
    ''' </summary>
    ''' <returns></returns>
    Function ListAllMaritalStatus() As List(Of MaritalStatus)

    ''' <summary>
    ''' Elimina un tipo de estado civil
    ''' </summary>
    ''' <param name="MaritalStatus">el tipo de estado civil</param>
    ''' <returns></returns>
    Function DeleteMaritalStatus(ByVal maritalStatus As MaritalStatus, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Graba un tipo de estado civil
    ''' </summary>
    ''' <param name="MaritalStatus">el tipo de estado civil</param>
    ''' <returns></returns>
    Function SaveMaritalStatus(ByVal maritalStatus As MaritalStatus, ByVal audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of MaritalStatus)

    ''' <summary>
    ''' Consulta un tipo de estado civil por código
    ''' </summary>
    ''' <param name="code">Code</param>
    ''' <returns></returns>
    Function GetMaritalStatusByCode(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of MaritalStatus)

    ''' <summary>
    ''' Consulta un tipo de estado civil por id
    ''' </summary>
    ''' <param name="id">id</param>
    ''' <returns></returns>
    Function GetMaritalStatusById(ByVal id As Integer) As ActionResult(Of MaritalStatus)

    ''' <summary>
    ''' Consulta segun el tipo de cultura el estado civil
    ''' </summary>
    ''' <param name="CultureStatus">Estado civil según la cultura</param>
    ''' <returns></returns>
    Function GetCultureMaritalStatus(ByVal CultureStatus As String)

End Interface