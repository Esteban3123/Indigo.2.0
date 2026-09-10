'************************************************************
' Assembly         : Domain.Entities.Reporitories
' Author           : Sergio Abraham Fernnadez Cruz
' Created          : 2014-03-17
'
' Copyright        : (c) . All rights reserved.
'************************************************************
#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region

Public Interface ISettingsAccountRepository
    Inherits IRepository(Of GeneralLedgerSettings)

    ''' <summary>
    ''' funcion para obtener el parametro contable
    ''' </summary>
    ''' <returns></returns>
    Function GetSettingAccount(ByVal idOperationUnit As Integer) As GeneralLedgerSettings

    ''' <summary>
    ''' funcion para obtener el parametro contable sin agregados
    ''' </summary>
    ''' <returns></returns>
    Function GetSettingAccountSimple(ByVal idOperationUnit As Integer) As GeneralLedgerSettings

    ''' <summary>
    ''' función para obtener los parametros de contabilidad si manejan nomima electrónica
    ''' </summary>
    ''' <returns></returns>
    Function GetSettingElectronicPayrollInformation() As GeneralLedgerSettings
End Interface
