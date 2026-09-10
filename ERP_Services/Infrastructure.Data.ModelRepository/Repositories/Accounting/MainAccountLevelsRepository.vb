'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Pablo Alexander Salazar sanchez
' Created          : 15-12-2022
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Text

Public Class MainAccountLevelsRepository
    Inherits GenericRepository(Of MainAccountLevels)
    Implements IMainAccountLevelsRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

#Region "Constructor"
    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub
#End Region

#Region "Functions"
    ''' <summary>
    ''' Obtiene todos los Niveles de cuntas contables
    ''' </summary>
    ''' <returns>Lista de los entidades</returns>
    ''' <remarks></remarks>
    Public Function GetAllMainAccountLevels() As List(Of MainAccountLevels) Implements IMainAccountLevelsRepository.GetAllMainAccountLevels
        Dim MainAccountLevels = From e In _context.MainAccountLevels
                                Select e
        Return MainAccountLevels.ToList()
    End Function

    ''' <summary>
    ''' obtiene un Nivel de cuntas contables por codigo
    ''' </summary>
    Public Function GetMainAccountLevelsByCode(code As String) As MainAccountLevels Implements IMainAccountLevelsRepository.GetMainAccountLevelsByCode
        Dim _MainAccountLevels = (From e In _context.MainAccountLevels Where e.Code = code Select e)
        If (_MainAccountLevels.Count > 0) Then
            _MainAccountLevels.Single().OriginalValue = (From e In _context.MainAccountLevels.AsNoTracking()
                                                         Where e.Code = code
                                                         Select e).FirstOrDefault()
            Return _MainAccountLevels.Single()
        Else
            Return New MainAccountLevels()
        End If
    End Function

    ''' <summary>
    ''' obtiene un Nivele de cuntas contables por id
    ''' </summary>
    Public Function GetMainAccountLevelsById(id As Integer, Optional tracking As Boolean = True) As MainAccountLevels Implements IMainAccountLevelsRepository.GetMainAccountLevelsById
        If tracking Then
            Dim _MainAccountLevels = From e In _context.MainAccountLevels
                                     Where e.Id = id
                                     Select e

            If _MainAccountLevels.Count > 0 Then
                Return _MainAccountLevels.Single
            Else
                Return New MainAccountLevels
            End If
        Else
            Dim _MainAccountLevels = From e In _context.MainAccountLevels.AsNoTracking
                                     Where e.Id = id
                                     Select e

            If _MainAccountLevels.Count > 0 Then
                Return _MainAccountLevels.Single
            Else
                Return New MainAccountLevels()
            End If
        End If
    End Function

#End Region
End Class
