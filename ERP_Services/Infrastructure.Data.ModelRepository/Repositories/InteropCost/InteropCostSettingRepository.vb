'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 29-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources

Public Class InteropCostSettingRepository
    Inherits GenericRepository(Of InteropCostSetting)
    Implements IInteropCostSettingRepository

    ''' <summary>
    ''' The _context
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene el parámetro actual de costos
    ''' </summary>
    Public Function GetInteropCostSetting() As InteropCostSetting Implements IInteropCostSettingRepository.GetInteropCostSetting
        Dim query = (From s In _context.InteropCostSetting Select s).FirstOrDefault()
        If query IsNot Nothing Then
            query.OriginalValue = (From s In _context.InteropCostSetting.AsNoTracking() Select s).FirstOrDefault()
            Return query
        Else
            Return New InteropCostSetting()
        End If
    End Function

    ''' <summary>
    ''' Obtiene el parámetro actual de costos por id
    ''' </summary>
    Public Function GetInteropCostSettingById(id As Integer) As InteropCostSetting Implements IInteropCostSettingRepository.GetInteropCostSettingById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query = (From s In _context.InteropCostSetting Where s.Id = id Select s).FirstOrDefault()
        If query IsNot Nothing Then
            query.OriginalValue = (From s In _context.InteropCostSetting.AsNoTracking() Where s.Id = id Select s).FirstOrDefault()
            Return query
        Else
            Return New InteropCostSetting()
        End If
    End Function

End Class