'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 30-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IPayrollParameterAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista todos los Parámetros de Nómina
    ''' </summary>
    ''' <returns>Lista de Parámetros de Nómina</returns>
    ''' <remarks></remarks>
    Function ListAllPayrollParameter() As List(Of PayrollParameter)

    ''' <summary>
    ''' Elimina un Parámetro de Nómina
    ''' </summary>
    ''' <param name="payrollParameter">Objeto Parámetro de Nómina</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function DeletePayrollParameter(ByVal payrollParameter As PayrollParameter, ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' Almacena un Parámetro de Nómina
    ''' </summary>
    ''' <param name="payrollParameter">Objeto Parámetro de Nómina</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SavePayrollParameter(ByVal payrollParameter As PayrollParameter, ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' Obtiene un Parámetro de Nómina
    ''' </summary>
    ''' <param name="groupId">Id del Grupo</param>
    ''' <returns>Parámetro de Nómina</returns>
    ''' <remarks></remarks>
    Function GetPayrollParameter(ByVal groupId As String) As PayrollParameter

End Interface
