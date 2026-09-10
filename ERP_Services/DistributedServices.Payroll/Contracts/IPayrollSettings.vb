#Region "Imports"
Imports System.ServiceModel
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

<ServiceContract()> _
Public Interface IPayrollSettings

    ''' <summary>
    ''' Elimina los Parámetros de Nómina
    ''' </summary>
    ''' <param name="PayrollSettings">PayrollSettings</param>
    ''' <param name="session"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function DeleteSettingsPayroll(ByVal PayrollSettings As PayrollSettings, session As SessionValues) As Boolean

    ''' <summary>
    ''' Almacena los Parámetros de Nómina
    ''' </summary>
    ''' <param name="PayrollSettings">PayrollSettings</param>
    ''' <param name="session"></param>
    ''' <returns>ActionResult(Of PayrollSettings)</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function SaveSettingsPayroll(ByVal PayrollSettings As PayrollSettings, session As SessionValues) As ActionResult(Of PayrollSettings)

    ''' <summary>
    ''' Obtiene los Parámetros de Nómina
    ''' </summary>
    ''' <param name="session">session</param>
    ''' <returns>PayrollSettings</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetSettingsPayroll(ByVal session As SessionValues) As PayrollSettings
End Interface
