'************************************************************
' Assembly         : Domain.DocumentalRepository
' Author           : Juan Diego Diaz
' Created          : 20-09-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.DocumentalSystem.Entities
Imports Domain.Base

#End Region

''' <summary>
''' Interfaz del repositorio de archivadores.
''' </summary>
Public Interface IFileContainerRepository
    Inherits IRepository(Of FileContainer)

    ''' <summary>
    ''' Función que obtiene una lista de archivadores.
    ''' </summary>
    ''' <returns>Lista de Archivadores</returns>
    Function ListAllFileContainers() As List(Of FileContainer)
    ''' <summary>
    ''' Funcion para obtener un archivador
    ''' </summary>
    ''' <param name="Id">Id del Archivador</param>
    ''' <returns>Objeto Archivador</returns>
    Function GetFileContainer(ByVal Id As Integer, Optional tracking As Boolean = True) As FileContainer

End Interface
