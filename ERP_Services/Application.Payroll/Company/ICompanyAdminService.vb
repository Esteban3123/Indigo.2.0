'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 25-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ICompanyAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista todas las Compañias
    ''' </summary>
    ''' <returns>Listado de Compañias</returns>
    ''' <remarks></remarks>
    Function ListAllCompany() As List(Of Company)

    ''' <summary>
    ''' Obtiene una Compañia en Específico
    ''' </summary>
    ''' <param name="nit">nit de la Compañia</param>
    ''' <returns>Compañia</returns>
    Function GetCompany(ByVal nit As String) As Company

    ''' <summary>
    ''' Graba o Actualiza una Compañía
    ''' </summary>
    ''' <param name="company">Compañia</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveCompany(ByVal company As Company, ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' Elimina una Compañía
    ''' </summary>
    ''' <param name="company">Compañia</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    Function DeleteCompany(ByVal company As Company, ByVal audit As AuditMessage) As ActionMessageResult(Of Company)

End Interface
