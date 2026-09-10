'************************************************************
' Assembly         : Infraestructure.Data.GlosasRepository
' Author           : Juan Diego Diaz
' Created          : 13-06-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities

#End Region

''' <summary>
''' Repositorio Detalles Devolución
''' </summary>
Public Class DevolutionsReceptionDRepository
    Inherits GenericRepository(Of GlosaDevolutionsReceptionD)
    Implements IDevolutionsReceptionDRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''Inicializa la nueva instancia de clase.
    ''' </summary>
    ''' <param name="contex">El contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    ''' <summary>
    ''' Función que obtiene un detalle de devolución segun código.
    ''' </summary>
    ''' <param name="Id">Id Devolución Detalle</param>
    ''' <returns>Objeto Cabecera Devolución</returns>
    Public Function GetDevolutionDByIdDevolutionD(Id As String, Optional tracking As Boolean = True) As GlosaDevolutionsReceptionD Implements IDevolutionsReceptionDRepository.GetDevolutionDByIdDevolutionD
        If tracking Then
            Dim Devolution = From e In _context.GlosaDevolutionsReceptionD.Include("GlosaMovementDevolutions").Include("GlosaDevolutionsReceptionC").Include("GlosaDevolutionsReceptionC.Customer")
                Where e.Id = CInt(Id)
                Select e
            If Devolution.Count > 0 Then
                Dim DevolutionData = Devolution.SingleOrDefault
                DevolutionData.OriginalValue = (From e In _context.GlosaDevolutionsReceptionD.AsNoTracking
                Where e.Id = CInt(Id)
                Select e).SingleOrDefault
                Return DevolutionData
            End If
        Else
            Dim Devolution = (From e In _context.GlosaDevolutionsReceptionD.AsNoTracking
               Where e.Id = CInt(Id)
               Select e).SingleOrDefault
            If Devolution IsNot Nothing Then
                Return Devolution
            End If
        End If
        Return New GlosaDevolutionsReceptionD
    End Function

    ''' <summary>
    ''' Función que obtiene detalles de devolución segun código.
    ''' </summary>
    ''' <param name="Id">Id Devolución Cabecera</param>
    ''' <returns>Lista Devolución Cabecera</returns>
    Public Function ListDevolutionDByIdDevolutionC(Id As String) As List(Of GlosaDevolutionsReceptionD) Implements IDevolutionsReceptionDRepository.ListDevolutionDByIdDevolutionnC
        Dim Devolution = From e In _context.GlosaDevolutionsReceptionD.Include("GlosaMovementDevolutions").Include("GlosaDevolutionsReceptionC.Customer").Include("GlosasParametersInterface")
                         Where e.GlosaDevolutionsReceptionCId = CInt(Id)
                         Select e

        For Each item In Devolution
            item.EntityName = item?.GlosaDevolutionsReceptionC?.Customer?.Name
        Next
        Return Devolution.ToList()

    End Function

    ''' <summary>
    ''' Funcion que obtiene todas los detalles de devolución.
    ''' </summary>
    ''' <returns>Lista Devolución Cabecera</returns>
    Public Function ListAllDevolutionD() As List(Of GlosaDevolutionsReceptionD) Implements IDevolutionsReceptionDRepository.ListAllDevolutionD
        Dim Busqueda = From e In _context.GlosaDevolutionsReceptionD.Include("GlosaMovementDevolutions").Include("GlosaDevolutionsReceptionC").Include("GlosaDevolutionsReceptionC.Customer")
                                 Select e

        Return Busqueda.ToList
    End Function


    ''' <summary>
    ''' Función que obtiene un detalle de devolución segun código.
    ''' </summary>
    ''' <param name="Id">Id Devolución Detalle</param>
    ''' <returns>Objeto Cabecera Devolución</returns>
    Public Function GetDevolutionDById(Id As String) As GlosaDevolutionsReceptionD Implements IDevolutionsReceptionDRepository.GetDevolutionDById
        Dim Devolution = From e In _context.GlosaDevolutionsReceptionD.Include("GlosaMovementDevolutions")
            Where e.Id = CInt(Id)
            Select e
        If Devolution.Count > 0 Then
            Dim DevolutionData = Devolution.SingleOrDefault
            Return DevolutionData
        End If
        Return New GlosaDevolutionsReceptionD
    End Function
End Class
