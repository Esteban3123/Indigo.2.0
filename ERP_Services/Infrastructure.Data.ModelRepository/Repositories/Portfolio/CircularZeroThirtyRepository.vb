'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Faiber Julian Mora D.
' Created          : 07-10-2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

#End Region

Public Class CircularZeroThirtyRepository
    Inherits GenericRepository(Of Circular030)
    Implements ICircularZeroThirtyRepository

    Private _contexto As IGlobalModelUnitOfWork

    Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _contexto = context
    End Sub

    Public Function GenerateDocument030(year As Integer, trimester As Integer, user As String) As List(Of GenerateDocument030_Result) Implements ICircularZeroThirtyRepository.GenerateDocument030
        CType(_contexto, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Dim result = _contexto.GenerateDocument030(year, trimester, DateTime.Now.Date, user)
        Return result.ToList()
    End Function

    ''' <summary>
    ''' Obtiene una lista de los años y la cantidad de trimestres procesados hasta el momento
    ''' </summary>
    ''' <returns>Lista de años</returns>
    Public Function ListTrimesters() As Dictionary(Of Integer, String) Implements ICircularZeroThirtyRepository.ListTrimesters
        Dim dicResult = New Dictionary(Of Int32, String)()
        Dim result = _contexto.Circular030.OrderBy(Function(c) c.Year).ThenBy(Function(c) c.Trimester).ToList()

        If result.Any() Then
            For Each group In result
                If Not dicResult.ContainsKey(group.Year) Then
                    dicResult.Add(group.Year, group.Trimester)
                Else
                    dicResult(group.Year) &= "," & group.Trimester
                End If
            Next
        Else
            Dim setting = _contexto.CompanySettings.FirstOrDefault()
            If setting IsNot Nothing Then
                dicResult.Add(setting.LastClosingDate.Year - 1, 4)
            End If
        End If

        Return dicResult
    End Function

End Class
