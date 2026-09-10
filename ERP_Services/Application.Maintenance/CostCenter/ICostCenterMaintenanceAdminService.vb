'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 20-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Maintenance.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ICostCenterMaintenanceAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista todos los centros de costos
    ''' </summary>
    ''' <returns>Lista de centros de costos</returns>
    ''' <remarks></remarks>
    Function ListAllCostCenter() As List(Of CostCenter)

    ''' <summary>
    ''' Obtiene un centro de costo especifico
    ''' </summary>
    ''' <param name="code">Codigo del centro de costo</param>
    ''' <returns>Centro de costo</returns>
    ''' <remarks></remarks>
    Function GetCostCenter(ByVal code As String) As CostCenter

    ''' <summary>
    ''' Graba o actualiza un centro de costo
    ''' </summary>
    ''' <param name="CostCenter">Centro de costo a guardar</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    Function SaveCostCenter(ByVal costCenter As CostCenter, ByVal audit As AuditMessage, Optional idSequence As Long = 0) As ActionResult(Of CostCenter)

    ''' <summary>
    ''' Elimina un centro de costo
    ''' </summary>
    ''' <param name="CostCenter">Centro de costo</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    Function DeleteCostCenter(ByVal costCenter As CostCenter, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Funcion para obtener la cuenta por id
    ''' </summary>
    ''' <param name="code">code.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Function GetCostCenterById(ByVal id As Integer, ByVal tracking As Boolean) As CostCenter

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeStateCostCenter(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of CostCenter)
End Interface
