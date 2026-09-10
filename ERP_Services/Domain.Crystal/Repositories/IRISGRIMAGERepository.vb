'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Hector Rodriguez Rubiano
' Created          : 14/03/2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities
Public Interface IRISGRIMAGERepository
    Inherits IRepository(Of RISGRIMAGE)

    Function GetImagingGroupById(id As Integer) As RISGRIMAGE
    
    ''' <summary>
    ''' Obtiene grupos de imagenologia
    ''' </summary>
    ''' <returns></returns>
    Function GetImagingGroups() As List(Of RISGRIMAGE)
End Interface
