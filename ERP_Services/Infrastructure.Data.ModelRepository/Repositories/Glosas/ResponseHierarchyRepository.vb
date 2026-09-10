'***********************************************************************
' Assembly         : Infraestructure.Data.GlosasRepository
' Author           : Juan Diego Diaz Mosquera
' Created          : 12-08-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

''' <summary>
''' 
''' </summary>
Public Class ResponseHierarchyRepository
    Inherits GenericRepository(Of GlosasResponseHierarchy)
    Implements IResponseHierarchyRepository


    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub
    ''' <summary>
    ''' Obtiene jerarquia por codigo
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetResponseHierarchy(Code As String) As GlosasResponseHierarchy Implements IResponseHierarchyRepository.GetResponseHierarchy
        Dim GlosasResponseHierarchy = From e In _context.GlosasResponseHierarchy
             Where e.Code = Code
             Select e
        If GlosasResponseHierarchy.Count > 0 Then
            Dim ResponseHierarchy = GlosasResponseHierarchy.SingleOrDefault
            ResponseHierarchy.OriginalValue = (From e In _context.GlosasResponseHierarchy.AsNoTracking
         Where e.Code = Code
         Select e).SingleOrDefault
            Return ResponseHierarchy
        End If
        Return New GlosasResponseHierarchy
    End Function
    ''' <summary>
    ''' Obtiene jerarquia por Id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetResponseHierarchyById(id As Integer) As GlosasResponseHierarchy Implements IResponseHierarchyRepository.GetResponseHierarchyById
        Dim GlosasResponseHierarchy = From e In _context.GlosasResponseHierarchy
        Where e.Id = id
        Select e
        If GlosasResponseHierarchy.Count > 0 Then
            Dim ResponseHierarchy = GlosasResponseHierarchy.SingleOrDefault
            ResponseHierarchy.OriginalValue = (From e In _context.GlosasResponseHierarchy.AsNoTracking
         Where e.Id = id
         Select e).SingleOrDefault
            Return ResponseHierarchy
        End If
        Return New GlosasResponseHierarchy
    End Function
    ''' <summary>
    ''' Lista de jerarquia de respuesta
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListResponseHierarchy() As List(Of GlosasResponseHierarchy) Implements IResponseHierarchyRepository.ListResponseHierarchy
        Dim Busqueda = From e In _context.GlosasResponseHierarchy
                      Where e.State = True
                           Select e
        Return Busqueda.ToList
    End Function

End Class
