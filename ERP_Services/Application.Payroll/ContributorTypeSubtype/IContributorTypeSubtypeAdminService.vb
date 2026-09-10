'***********************************************************************
' Assembly         : Application.Payroll
' Author           :
' Created          : 05-06-2026
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities

Public Interface IContributorTypeSubtypeAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista todas las combinaciones Tipo+Subtipo de cotizante con nombres desnormalizados
    ''' </summary>
    Function ListAllContributorTypeSubtype() As List(Of ContributorTypeSubtype)

    ''' <summary>
    ''' Lista los subtipos válidos para un Tipo de cotizante
    ''' </summary>
    Function ListByContributorTypeId(ByVal contributorTypeId As Integer) As List(Of ContributorTypeSubtype)

    ''' <summary>
    ''' Obtiene el Id del pivote dado tipo + subtipo
    ''' </summary>
    Function GetIdByTypeAndSubtype(ByVal contributorTypeId As Integer, ByVal contributorSubtypeId As Integer) As Integer

    ''' <summary>
    ''' Guarda una nueva combinacion Tipo+Subtipo de cotizante
    ''' </summary>
    Function SaveContributorTypeSubtype(ByVal contributorTypeSubtype As ContributorTypeSubtype) As Boolean

    ''' <summary>
    ''' Elimina una combinacion Tipo+Subtipo de cotizante por Id
    ''' </summary>
    Function DeleteContributorTypeSubtype(ByVal id As Integer) As Boolean

End Interface
