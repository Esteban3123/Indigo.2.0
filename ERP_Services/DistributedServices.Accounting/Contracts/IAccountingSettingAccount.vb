#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
#End Region

<ServiceContract()>
Public Interface IAccountingSettingAccount

#Region "Methods"

    ''' <summary>
    ''' Funmcion para obtener el parametro contable
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetSettingAccount(ByVal idOperationUnit As Integer) As GeneralLedgerSettings

    ''' <summary>
    ''' Funcion para guardar el parametro contable
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveSettingAccount(ByVal settingAccount As GeneralLedgerSettings) As ActionResult(Of GeneralLedgerSettings)
#End Region

End Interface
