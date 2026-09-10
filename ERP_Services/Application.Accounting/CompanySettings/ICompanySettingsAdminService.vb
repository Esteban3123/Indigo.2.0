'***********************************************************************
' Assembly         : Application.Accounting.PUCAdminService
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 10-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Globalization
#End Region
Public Interface ICompanySettingsAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Saves the company settings.
    ''' </summary>
    ''' <param name="settings">The settings.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveCompanySettings(ByVal settings As CompanySettings, ByVal audit As AuditMessage) As ActionResult(Of CompanySettings)

    ''' <summary>
    ''' Funcion para obtenetr los parametros de la empresa
    ''' </summary>
    ''' <returns></returns>
    Function GetCompanySettings() As CompanySettings

    ''' <summary>
    ''' retorna la momenda oficial del sistema, guardada en company settings
    ''' </summary>
    ''' <returns></returns>
    Function GetOfficialCurrency() As Currency
End Interface
