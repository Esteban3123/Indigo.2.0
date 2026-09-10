'************************************************************
' Assembly         : Infraestructure.Data.DocumentalSystemRepository
' Author           : Juan Diego Diaz
' Created          : 21-04-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************
#Region "Imports"

Imports Domain.Base
Imports Domain.DocumentalSystem.Entities
Imports Domain.DocumentalSystem
Imports Infrastructure.Data.Base
Imports System.Transactions
Imports System.Data.SqlTypes
Imports System.Text.RegularExpressions

#End Region

''' <summary>
''' Repositorio de Archivadores formularios
''' </summary>
Public Class FileContainersFormRepository
    Inherits GenericRepository(Of FileContainersForm)
    Implements IFileContainersFormRepository


    'Devuelve el contexto en este repositorio 
    Private _context As IDocumentalSystemUnitOfWork

    ''' <summary>
    '''Inicializa la nueva instancia de clase.
    ''' </summary>
    ''' <param name="contex">El contexto.</param>
    Public Sub New(ByVal contex As IDocumentalSystemUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    ''' <summary>
    ''' Función que obtiene una lista de archivadores formularios.
    ''' </summary>
    ''' <returns>Lista de FileContainersForm</returns>
    Function ListAllFileContainersForm() As List(Of FileContainersForm) Implements IFileContainersFormRepository.ListAllFileContainersForm

        Dim Busqueda = (From e In _context.FileContainersForm
                       Select e).ToList

        Return Busqueda

    End Function

    ''' <summary>
    ''' Función que obtiene una lista de archivadores formularios según Id.
    ''' </summary>
    ''' <param name="Id">Id del formulario por contenedor</param>
    ''' <returns>Lista de FileContainersForm</returns>
    Function ListFileContainersFormById(Id As String, Optional tracking As Boolean = True) As FileContainersForm Implements IFileContainersFormRepository.ListFileContainersFormById
        If tracking Then
            Dim FileContainerForm = From e In _context.FileContainersForm
               Where e.Id = Id
               Select e
            If FileContainerForm.Count > 0 Then
                Return FileContainerForm.SingleOrDefault
            Else
                Return New FileContainersForm
            End If
        Else
            Dim FileContainerForm = (From e In _context.FileContainersForm.AsNoTracking
            Where e.Id = Id
            Select e).SingleOrDefault
            If FileContainerForm IsNot Nothing Then
                Return FileContainerForm
            Else
                Return New FileContainersForm
            End If
        End If

    End Function

    ''' <summary>
    ''' Funcion para obtener una lista de archivadores por formulario segun Id del Archivador
    ''' </summary>
    ''' <param name="IdFileContainer">Id del Archivador</param>
    ''' <returns>Lista de FileContainersForm</returns>
    Function ListFileContainersFormByIdFileContainer(ByVal IdFileContainer As Integer) As List(Of FileContainersForm) Implements IFileContainersFormRepository.ListFileContainersFormByIdFileContainer
        Dim Busqueda = From e In _context.FileContainersForm.Include("FileContainer")
                       Where e.IdFileContainer = IdFileContainer
                              Select e

        Return Busqueda.ToList
    End Function

    ''' <summary>
    ''' Funcion para obtener una lista de archivadores por formulario segun Id del Formulario
    ''' </summary>
    ''' <param name="IdForm">Id del Formulario</param>
    ''' <returns>Lista de FileContainersForm</returns>
    Function ListFileContainersFormByIdForm(ByVal IdForm As Integer) As List(Of FileContainersForm) Implements IFileContainersFormRepository.ListFileContainersFormByIdForm
        Dim Busqueda = From e In _context.FileContainersForm.Include("FileContainer")
               Where e.IdForm = IdForm
                      Select e

        Return Busqueda.ToList
    End Function

End Class



