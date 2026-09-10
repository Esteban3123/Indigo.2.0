
'***********************************************************************
' Assembly         : Domain.FixedAsset
' Author           : Daniel Eduardo Arévalo
' Created          : 23/05/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Payroll.Entities

#End Region

Public Interface IPayrollSettingsRepository
    Inherits IRepository(Of PayrollSettings)

    ''' <summary>
    ''' Obtengo los Parámetros de Nómina
    ''' </summary>
    ''' <param name="tracking"></param>
    ''' <returns>PayrollSettings</returns>
    ''' <remarks></remarks>
    Function GetSettingPayroll(Optional tracking As Boolean = True) As PayrollSettings
End Interface
