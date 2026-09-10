'************************************************************
' Assembly         : Domain.Entities.Service
' Author           : Juan F. Tamayo
' Created          : 2014-03-17
'
' Copyright        : (c) . All rights reserved.
'************************************************************
'************************************************************
' Assembly         : Domain.Entities.Service
' Author           : Jeisson Herrera Peña
' Created          : 2015-09-19
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Domain.Base

#End Region

Public Interface ISettingBudgetRepository
    Inherits IRepository(Of SettingsBudget)

#Region "Methods"

    ''' <summary>
    ''' Obtiene la configuracion de presupuesto
    ''' </summary>
    ''' <returns></returns>
    Function GetSettingBudgetByOperatingUnit(id As Integer, Optional tracking As Boolean = True) As SettingsBudget

#End Region

End Interface
