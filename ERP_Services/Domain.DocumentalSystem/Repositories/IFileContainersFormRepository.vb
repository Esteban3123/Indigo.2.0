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
''' Interfaz del repositorio de archivadores por formulario.
''' </summary>
Public Interface IFileContainersFormRepository
    Inherits IRepository(Of FileContainersForm)

    ''' <summary>
    ''' Función que obtiene una lista de archivadores formularios.
    ''' </summary>
    ''' <returns>Lista de FileContainersForm</returns>
    Function ListAllFileContainersForm() As List(Of FileContainersForm)
    ''' <summary>
    ''' Funcion para obtener una lista de archivadores por formulario segun Id del Archivador
    ''' </summary>
    ''' <param name="IdFileContainer">Id del Archivador</param>
    ''' <returns>Lista de FileContainersForm</returns>
    Function ListFileContainersFormByIdFileContainer(ByVal IdFileContainer As Integer) As List(Of FileContainersForm)
    ''' <summary>
    ''' Funcion para obtener una lista de archivadores por formulario segun Id del Formulario
    ''' </summary>
    ''' <param name="IdForm">Id del Formulario</param>
    ''' <returns>Lista de FileContainersForm</returns>
    Function ListFileContainersFormByIdForm(ByVal IdForm As Integer) As List(Of FileContainersForm)
    ''' <summary>
    ''' Función que obtiene una lista de archivadores formularios según Id.
    ''' </summary>
    ''' <param name="Id">Id del formulario por contenedor</param>
    ''' <returns>Lista de FileContainersForm</returns>
    Function ListFileContainersFormById(Id As String, Optional tracking As Boolean = True) As FileContainersForm

End Interface

