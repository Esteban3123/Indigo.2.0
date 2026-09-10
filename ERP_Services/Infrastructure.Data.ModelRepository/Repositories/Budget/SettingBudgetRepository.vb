'***********************************************************************
' Assembly         : Infrastructure.Data.BudgetRepository
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 10-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
'***********************************************************************
' Assembly         : Infrastructure.Data.BudgetRepository
' Author           : Jeisson Herrera Peña
' Created          : 19-09-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.Data.Base
Imports Domain.Entities
#End Region

Public Class SettingBudgetRepository
    Inherits GenericRepository(Of SettingsBudget)
    Implements ISettingBudgetRepository

#Region "Fields"
    ''' <summary>
    ''' Contexto de Presupuesto
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork
#End Region

#Region "Builder"
    ''' <summary>
    ''' Inicia el contexto de Presupuesto
    ''' </summary>
    ''' <param name="context"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' Obtiene la configuracion de presupuesto
    ''' </summary>
    ''' <returns></returns>
    Public Function GetSettingBudgetByOperatingUnit(id As Integer, Optional tracking As Boolean = True) As SettingsBudget Implements ISettingBudgetRepository.GetSettingBudgetByOperatingUnit
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        'Dim setting = From e In _context.SettingsBudget Where e.OperatingUnitId = id Select e
        'If setting.Count() > 0 Then
        '    setting.SingleOrDefault().OriginalValue = (From e In _context.SettingsBudget.AsNoTracking() Where e.OperatingUnitId = id Select e).SingleOrDefault
        '    Return setting.SingleOrDefault()
        'Else
        '    Return New SettingsBudget()
        'End If
        Dim res = (From e In _context.SettingsBudget.AsNoTracking Where e.OperatingUnitId = id Select e).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From e In _context.SettingsBudget.AsNoTracking Where e.OperatingUnitId = id Select e).FirstOrDefault
            Return res
        Else
            Return New SettingsBudget()
        End If
    End Function
#End Region
    
End Class
