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
''' Repositorio de Metadata
''' </summary>
Public Class MetadataRepository
    Inherits GenericRepository(Of Metadata)
    Implements IMetaDataRepository


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
    ''' Función que obtiene un objeto metadata según Id.
    ''' </summary>
    ''' <param name="Id">Id Metadata</param>
    ''' <returns>Objeto Metadata</returns>
    Function getMetadataFormById(Id As String, Optional tracking As Boolean = True) As Metadata Implements IMetaDataRepository.getMetadataFormById
        If tracking Then
            Dim ObjMetadata = From e In _context.Metadata
               Where e.Id = Id
               Select e
            If ObjMetadata.Count > 0 Then
                Return ObjMetadata.SingleOrDefault
            Else
                Return New Metadata
            End If
        Else
            Dim ObjMetadata = (From e In _context.Metadata.AsNoTracking
            Where e.Id = Id
            Select e).SingleOrDefault
            If ObjMetadata IsNot Nothing Then
                Return ObjMetadata
            Else
                Return New Metadata
            End If
        End If
    End Function

    ''' <summary>
    ''' Función que obtiene una lista de metadata.
    ''' </summary>
    ''' <returns>Lista de Metadata</returns>
    Function ListAllMetadata() As List(Of Metadata) Implements IMetaDataRepository.ListAllMetadata

        Dim Busqueda = (From e In _context.Metadata
                       Select e).ToList

        Return Busqueda

    End Function

    ''' <summary>
    ''' Funcion para obtener metadata según archivador
    ''' </summary>
    ''' <param name="IdFileContainer">Id del Archivador</param>
    ''' <returns>Lista de Metadata</returns>
    Function GetMetadataByFileContainer(ByVal IdFileContainer As Integer) As List(Of Metadata) Implements IMetaDataRepository.GetMetadataByFileContainer
        Dim Busqueda = From e In _context.Metadata
                       Where e.IdFileContainer = IdFileContainer
                              Select e

        Return Busqueda.ToList
    End Function


    ''' <summary>
    ''' Funcion para obtener Lista de metadata según Contenedor
    ''' </summary>
    ''' <param name="IdFileContainer">Id del Archivador</param>
    ''' <returns>Lista de Metadata</returns>
    Function ListMetadataByFileContainer(ByVal IdFileContainer As String) As List(Of Metadata) Implements IMetaDataRepository.ListMetadataByFileContainer
        Dim Busqueda = From e In _context.Metadata
                       Where e.IdFileContainer = IdFileContainer
                              Select e

        Return Busqueda.ToList
    End Function
End Class


