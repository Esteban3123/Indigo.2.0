'***********************************************************************
' Assembly         : Domain.Payroll
' Author           :
' Created          : 05-06-2026
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface IContributorTypeSubtypeRepository
    Inherits IRepository(Of ContributorTypeSubtype)

    ''' <summary>
    ''' Lista todas las combinaciones de Tipo y Subtipo de cotizante
    ''' con los nombres desnormalizados del Tipo y Subtipo
    ''' </summary>
    Function ListAllContributorTypeSubtype() As List(Of ContributorTypeSubtype)

    ''' <summary>
    ''' Lista los subtipos de cotizante validos para un Tipo de cotizante dado
    ''' </summary>
    ''' <param name="contributorTypeId">Id del Tipo de cotizante</param>
    Function ListByContributorTypeId(ByVal contributorTypeId As Integer) As List(Of ContributorTypeSubtype)

    ''' <summary>
    ''' Obtiene el Id de la tabla pivote dado un par (ContributorTypeId, ContributorSubtypeId)
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
