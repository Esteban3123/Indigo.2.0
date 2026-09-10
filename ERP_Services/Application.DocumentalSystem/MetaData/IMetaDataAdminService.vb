'***********************************************************************
' Assembly         : Application.DocumentalSystem
' Author           : Juan Diego Diaz
' Created          : 21-09-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.DocumentalSystem.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

''' <summary>
''' Interfaz para el servicio de metadata.
''' </summary>
Public Interface IMetaDataAdminService
    ''' <summary>
    ''' Función que obtiene una lista de metadata.
    ''' </summary>
    ''' <returns>Lista de Metadata</returns>
    Function ListAllMetadata() As List(Of Metadata)
    ''' <summary>
    ''' Funcion para obtener metadata según archivador
    ''' </summary>
    ''' <param name="IdFileContainer">Id del Archivador</param>
    ''' <returns>Lista de Metadata</returns>
    Function GetMetadataByFileContainer(ByVal IdFileContainer As String) As List(Of Metadata)
    ''' <summary>
    ''' Funcion para guardar metadata
    ''' </summary>
    ''' <param name="_listMetadata"></param>
    Function saveListMetata(ByVal _listMetadata As List(Of Metadata)) As ActionResult
    ''' <summary>
    ''' Funcion para eliminar metadata
    ''' </summary>
    ''' <param name="_objMetadata"></param>
    ''' <returns></returns>
    Function DeleteMetadata(ByVal _objMetadata As Metadata) As ActionResult

End Interface
