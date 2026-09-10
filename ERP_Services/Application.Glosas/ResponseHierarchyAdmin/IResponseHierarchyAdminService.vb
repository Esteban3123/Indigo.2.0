'***********************************************************************
' Assembly         : Application.Glosas
' Author           : Rafael Eduardo PAtiño
' Created          : 10-06-2015
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region
Public Interface IResponseHierarchyAdminService
    Inherits IDisposable
    ''' <summary>
    ''' Lists the GlosasResponseHierarchy
    ''' </summary>
    ''' <returns></returns>
    Function ListResponseHierarchy(audit As AuditMessage) As List(Of GlosasResponseHierarchy)
    ''' <summary>
    ''' consulta un Responsible especifico
    ''' </summary>
    ''' <param name="Code">el codigo del Responsible</param>
    ''' <returns></returns>
    Function GetResponseHierarchy(ByVal Code As String, audit As AuditMessage) As GlosasResponseHierarchy
    ''' <summary>
    ''' consulta una jerarquía de respuesta especifico
    ''' </summary>
    ''' <param name="id">Id</param>
    ''' <returns></returns>
    Function GetResponseHierarchyById(ByVal id As Integer, audit As AuditMessage) As GlosasResponseHierarchy
    ''' <summary>
    ''' Elimina una jerarquía
    ''' </summary>
    ''' <param name="GlosasResponseHierarchy">el Grupo</param>
    ''' <returns></returns>
    Function DeleteGlosasResponseHierarchy(ByVal GlosasResponseHierarchy As GlosasResponseHierarchy, ByVal audit As AuditMessage) As ActionResult
    ''' <summary>
    ''' graba una Jerarquía
    ''' </summary>
    ''' <param name="GlosasResponseHierarchy">el Responsiblee</param>
    ''' <returns></returns>
    Function SaveGlosasResponseHierarchy(ByVal GlosasResponseHierarchy As GlosasResponseHierarchy, ByVal audit As AuditMessage) As ActionResult(Of GlosasResponseHierarchy)
End Interface
