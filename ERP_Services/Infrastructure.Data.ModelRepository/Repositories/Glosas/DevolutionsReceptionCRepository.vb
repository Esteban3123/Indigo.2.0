'************************************************************
' Assembly         : Infraestructure.Data.GlosasRepository
' Author           : Juan Diego Diaz
' Created          : 12-06-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities

#End Region

''' <summary>
''' Repositorio Cabeceras Devolución
''' </summary>
Public Class DevolutionsReceptionCRepository
    Inherits GenericRepository(Of GlosaDevolutionsReceptionC)
    Implements IDevolutionsReceptionCRepository

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
    ''' Función que obtiene una cabecera de devolución segun código.
    ''' </summary>
    ''' <param name="Id">Id Devolución Cabecera</param>
    ''' <returns>Objeto Cabecera Devolución</returns>
    Public Function GetDevolutionC(Id As String) As GlosaDevolutionsReceptionC Implements IDevolutionsReceptionCRepository.GetDevolutionC
        Dim Devolution = From e In _context.GlosaDevolutionsReceptionC.Include("Customer")
           Where e.Id = CInt(Id)
           Select e
        If Devolution.Count > 0 Then
            Dim DevolutionData = Devolution.SingleOrDefault
            DevolutionData.OriginalValue = (From e In _context.GlosaDevolutionsReceptionC.AsNoTracking.Include("GlosaDevolutionsReceptionD").AsNoTracking.Include("Customer").AsNoTracking
                                                           Where e.Id = CInt(Id)
                                                           Select e).SingleOrDefault
            Return DevolutionData
        End If
        Return New GlosaDevolutionsReceptionC
    End Function

    ''' <summary>
    ''' Función que obtiene una cabecera de devolución segun consecutivo.
    ''' </summary>
    ''' <param name="Consecutive">Consecutivo Devolución Cabecera</param>
    ''' <returns>Objeto Cabecera Devolución</returns>
    Public Function GetDevolutionCByConsecutive(Consecutive As String) As GlosaDevolutionsReceptionC Implements IDevolutionsReceptionCRepository.GetDevolutionCByConsecutive
        Dim Devolution = From e In _context.GlosaDevolutionsReceptionC.Include("Customer")
        Where e.RadicatedConsecutive = Consecutive
        Select e
        If Devolution.Count > 0 Then
            Dim DevolutionData = Devolution.SingleOrDefault
            DevolutionData.OriginalValue = (From e In _context.GlosaDevolutionsReceptionC.AsNoTracking.Include("GlosaDevolutionsReceptionD").AsNoTracking.Include("Customer").AsNoTracking
                                                           Where e.RadicatedConsecutive = Consecutive
                                                           Select e).SingleOrDefault
            Return DevolutionData
        Else
            Return New GlosaDevolutionsReceptionC
        End If
    End Function

    ''' <summary>
    ''' Funcion que obtiene todas las cabeceras de devolución.
    ''' </summary>
    ''' <returns>Lista Devolución Cabecera</returns>
    Public Function ListAllDevolutionC() As List(Of GlosaDevolutionsReceptionC) Implements IDevolutionsReceptionCRepository.ListAllDevolutionC
        Dim Busqueda = From e In _context.GlosaDevolutionsReceptionC.Include("Customer")
                              Select e

        Return Busqueda.ToList
    End Function
End Class
