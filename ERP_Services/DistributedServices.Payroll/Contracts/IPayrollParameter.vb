'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 21-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
<ServiceContract()> _
Public Interface IPayrollParameter

    ''' <summary>
    ''' Lista todos las Parámetrizaciones de Nómina
    ''' </summary>
    ''' <returns>Parámetrizaciones de Nómina</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ListAllPayrollParameter(session As SessionValues) As List(Of PayrollParameter)

    ''' <summary>
    ''' Elimina una Parametrización de Nómina
    ''' </summary>
    ''' <param name="payrollParameter">Parametrización de Nómina</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function DeletePayrollParameter(ByVal payrollParameter As PayrollParameter, session As SessionValues) As Boolean

    ''' <summary>
    ''' Almacena una Parametrización de Nómina
    ''' </summary>
    ''' <param name="payrollParameter">Parametrización de Nómina</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function SavePayrollParameter(ByVal payrollParameter As PayrollParameter, session As SessionValues) As Boolean

    ''' <summary>
    ''' Obtiene una Parametrización deNómina
    ''' </summary>
    ''' <param name="groupId">Group ID</param>
    ''' <returns>Parametrización de Nómina</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetPayrollParameter(ByVal groupId As String, session As SessionValues) As PayrollParameter

End Interface
