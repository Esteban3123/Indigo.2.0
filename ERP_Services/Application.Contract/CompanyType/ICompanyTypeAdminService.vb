'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 23/09/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities

Public Interface ICompanyTypeAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista todos defectos
    ''' </summary>
    ''' <returns>Lista de tipos de dosis unitaria</returns>
    Function GetAllCompanyType(ByVal audit As AuditMessage) As List(Of CompanyType)

    ''' <summary>
    ''' Guarda o Actualiza un grupo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveCompanyType(ByVal CompanyType As CompanyType, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of CompanyType)

    ''' <summary>
    ''' Elimina un grupo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteCompanyType(ByVal CompanyType As CompanyType, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un grupo por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetCompanyTypeByCode(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of CompanyType)

    ''' <summary>
    ''' Obtiene un grupo por id
    ''' </summary>
    ''' <returns></returns>
    Function GetCompanyTypeById(ByVal id As Integer, ByVal audit As AuditMessage) As ActionResult(Of CompanyType)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeStateCompanyType(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of CompanyType)
End Interface
