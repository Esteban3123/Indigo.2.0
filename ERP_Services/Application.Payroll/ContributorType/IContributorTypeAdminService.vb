'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 04-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IContributorTypeAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista todos los tipos de contribuyentes
    ''' </summary>
    ''' <returns>Lista de tipos de contribuyentes</returns>
    Function ListAllContributorType() As List(Of ContributorType)

    ''' <summary>
    ''' Elimina un tipo de contribuyente
    ''' </summary>
    ''' <param name="contributorType">Tipo contribuyente</param>
    ''' <returns></returns>
    Function DeleteContributorType(ByVal contributorType As ContributorType, ByVal audit As AuditMessage) As ActionMessageResult(Of ContributorType)

    ''' <summary>
    ''' Guarda o edita un tipo contribuyente
    ''' </summary>
    ''' <param name="contributorType">Tipo Contribuyente</param>
    ''' <returns></returns>
    Function SaveContributorType(ByVal contributorType As ContributorType, ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' Obtiene un tipo contribuyente
    ''' </summary>
    ''' <param name="code">Código del tipo contribuyente</param>
    ''' <returns>Tipo Contribuyente</returns>
    Function GetContributorType(ByVal code As String) As ContributorType

End Interface
