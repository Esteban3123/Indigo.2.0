'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepositiry
' Author           : Andrés Steven Rojas Rodríguez
' Created          : 29-02-2024
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure
#End Region
Public Class UPRUnitsRepository
    Inherits GenericRepository(Of UPRUnits)
    Implements IUPRUnitsRepository

    ''' <summary>
    ''' The _context
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork
    ''' <summary>
    ''' Inicia el contexto de UPRUnits
    ''' </summary>
    ''' <param name="context"></param>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#Region "Methods"
    ''' <summary>
    ''' Obtiene la Unidad UPR por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Function getUPRUnits(code As String, Optional tracking As Boolean = True) As UPRUnits Implements IUPRUnitsRepository.getUPRUnits
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("Code")
        End If
        Dim query = (From UPRUnits In _context.UPRUnits Where UPRUnits.Code = code Select UPRUnits).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From UPRUnits In _context.UPRUnits.AsNoTracking() Where UPRUnits.Code = code Select UPRUnits).FirstOrDefault()
            Return query
        Else
            Return New UPRUnits()
        End If
    End Function
    ''' <summary>
    ''' Obtiene la Unidad UPR por Id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function getUPRUnitsById(id As Integer) As UPRUnits Implements IUPRUnitsRepository.getUPRUnitsById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query = (From UPRUnits In _context.UPRUnits Where UPRUnits.Id = id Select UPRUnits).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From UPRUnits In _context.UPRUnits.AsNoTracking() Where UPRUnits.Id = id Select UPRUnits).FirstOrDefault()
            Return query
        Else
            Return New UPRUnits()
        End If
    End Function

#End Region


End Class
