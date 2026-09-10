'************************************************************
' Assembly         : Domain.DocumentalRepository
' Author           : Juan Diego Diaz
' Created          : 21-09-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.DocumentalSystem.Entities
Imports Domain.Base

#End Region

''' <summary>
''' Interfaz del repositorio de metadata.
''' </summary>
Public Interface IMetaDataRepository
    Inherits IRepository(Of Metadata)

    ''' <summary>
    ''' Función que obtiene un objeto metadata según Id.
    ''' </summary>
    ''' <param name="Id">Id Metadata</param>
    ''' <returns>Objeto Metadata</returns>
    Function getMetadataFormById(Id As String, Optional tracking As Boolean = True) As Metadata

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
    Function GetMetadataByFileContainer(ByVal IdFileContainer As Integer) As List(Of Metadata)
    ''' <summary>
    ''' Funcion para obtener Lista de metadata según Contenedor
    ''' </summary>
    ''' <param name="IdFileContainer">Id del Archivador</param>
    ''' <returns>Lista de Metadata</returns>
    Function ListMetadataByFileContainer(ByVal IdFileContainer As String) As List(Of Metadata)

End Interface
