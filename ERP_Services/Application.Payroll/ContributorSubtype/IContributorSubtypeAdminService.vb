'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Cristian Camilo Bahamon Castaño
' Created          : 05-02-2023
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities



#End Region


Public Interface IContributorSubtypeAdminService
    Inherits IDisposable


    ''' <summary>
    ''' Lista todos los subtipos de cotizantes.
    ''' </summary>
    ''' <returns></returns>
    Function ListAllContributorSubtype() As List(Of ContributorSubtype)

    ''' <summary>
    ''' Elimina un subtipo de cotizante
    ''' </summary>
    ''' <param name="ContributorSubtype">el subtipo de cotizante</param>
    ''' <returns></returns>
    Function DeleteContributorSubtype(ByVal contributorSubtype As ContributorSubtype, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' graba un subtipo de cotizante
    ''' </summary>
    ''' <param name="ContributorSubtype">el subtipo de cotizante</param>
    ''' <returns></returns>
    Function SaveContributorSubtype(ByVal contributorSubtype As ContributorSubtype, ByVal audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of ContributorSubtype)

    ''' <summary>
    ''' Consulta un subtipo de cotizante por codigo
    ''' </summary>
    ''' <param name="code">Code</param>
    ''' <returns></returns>
    Function GetContributorSubtypeByCode(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of ContributorSubtype)

    ''' <summary>
    ''' Consulta un subtipo de cotizante por id
    ''' </summary>
    ''' <param name="id">id</param>
    ''' <returns></returns>
    Function GetContributorSubtypeById(ByVal id As Integer) As ActionResult(Of ContributorSubtype)
End Interface
