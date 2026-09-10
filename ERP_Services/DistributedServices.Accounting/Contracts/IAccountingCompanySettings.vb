#Region "Imports"
Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports System.Globalization
#End Region

<ServiceContract()>
Public Interface IAccountingCompanySettings
    ''' <summary>
    ''' Saves the company settings.
    ''' </summary>
    ''' <param name="settings">The settings.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveCompanySettings(ByVal settings As CompanySettings) As ActionResult(Of CompanySettings)

    ''' <summary>
    ''' Funcion para obtenetr los parametros de la empresa
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCompanySettings() As CompanySettings

    ''' <summary>
    ''' retorna la momenda oficial del sistema, guardada en company settings
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetOfficialCurrency() As Currency
End Interface
