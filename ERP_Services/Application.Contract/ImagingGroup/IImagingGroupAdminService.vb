'***********************************************************************
' Assembly         : Application.Contract
' Author           : Hector Rodriguez Rubiano
' Created          : 14/03/2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Crystal.Entities
Public Interface IImagingGroupAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene un determinado grupo de imagenologia por id
    ''' </summary>
    ''' <returns></returns>
    Function GetImagingGroupById(id As Integer, audit As AuditMessage) As ActionResult(Of RISGRIMAGE)
    
    ''' <summary>
    ''' Obtiene grupos de imagenologia activos
    ''' </summary>
    ''' <returns></returns>
    Function GetImagingGroupActive() As ActionResult(Of List(Of RISGRIMAGE))
End Interface
