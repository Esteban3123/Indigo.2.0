'***********************************************************************
' Assembly         : Application.Accounting
' Author           : Sergio Abraham Fernande Cruz
' Created          : 15-05-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region
Public Interface ISettingAccountAdminService
    Inherits IDisposable

#Region "Methods"
    ''' <summary>
    ''' Funmcion para obtener el parametro contable
    ''' </summary>
    ''' <returns></returns>
    Function GetSettingAccount(ByVal idOperationUnit As Integer) As GeneralLedgerSettings

    ''' <summary>
    ''' Funcion para guardar el parametro contable
    ''' </summary>
    ''' <returns></returns>
    Function SaveSettingAccount(ByVal settingAccount As GeneralLedgerSettings, audit As AuditMessage) As ActionResult(Of GeneralLedgerSettings)
#End Region

End Interface