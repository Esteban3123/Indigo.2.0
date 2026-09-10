'************************************************************
' Assembly         : Infraestructure.Data.DocumentalSystemRepository
' Author           : Juan Diego Diaz
' Created          : 20-04-2013
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
''' Repositorio de Archivadores
''' </summary>
Public Class FileContainerRepository
    Inherits GenericRepository(Of FileContainer)
    Implements IFileContainerRepository


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
    ''' Funcion para obtener un archivador
    ''' </summary>
    ''' <param name="Id">Id del Archivador</param>
    ''' <returns>Objeto Archivador</returns>
    Public Function GetFileContainer(Id As Integer, Optional tracking As Boolean = True) As FileContainer Implements IFileContainerRepository.GetFileContainer
        If tracking Then
            Dim FileContainer = From e In _context.FileContainer.Include("Metadata").Include("FileContainersForm")
               Where e.Id = Id
               Select e
            If FileContainer.Count > 0 Then
                Return FileContainer.SingleOrDefault
            Else
                Return New FileContainer
            End If
        Else
            Dim FileContainer = (From e In _context.FileContainer.AsNoTracking
            Where e.Id = Id
            Select e).SingleOrDefault
            If FileContainer IsNot Nothing Then
                Return FileContainer
            Else
                Return New FileContainer
            End If
        End If
    End Function

    ''' <summary>
    ''' Función que obtiene una lista de archivadores.
    ''' </summary>
    ''' <returns>Lista de Archivadores</returns>
    Public Function ListAllFileContainers() As List(Of FileContainer) Implements IFileContainerRepository.ListAllFileContainers
        Dim Busqueda = From e In _context.FileContainer.Include("FileContainersForm")
                              Select e

        Return Busqueda.ToList
    End Function
End Class

