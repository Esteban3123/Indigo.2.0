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

    ''' <summary>
    ''' Indica si el empleador maneja nómina electrónica en alguna de sus unidades operativas.
    ''' La nómina electrónica se parametriza por empleador (mismo NIT, mismo software ante la
    ''' DIAN), aunque el parámetro viva en una fila por unidad operativa. Se filtra por IdDian
    ''' para que en bases con varios empleadores no se mezclen sus configuraciones.
    ''' </summary>
    ''' <param name="idDian">Tercero empleador que reporta ante la DIAN.</param>
    Function EmployerHandlesElectronicPayroll(ByVal idDian As Integer) As Boolean
End Interface
