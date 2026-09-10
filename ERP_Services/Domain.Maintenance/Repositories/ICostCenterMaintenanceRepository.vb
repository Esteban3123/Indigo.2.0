Imports Domain.Base
Imports Domain.Maintenance.Entities

Public Interface ICostCenterMaintenanceRepository
    Inherits IRepository(Of CostCenter)

    ''' <summary>
    ''' Lista todos los centros de costos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllCostCenter() As List(Of CostCenter)

    ''' <summary>
    ''' Obtiene un centro de costo especifico
    ''' </summary>
    ''' <param name="code">Codigo del centro de costo</param>
    ''' <returns>Centro de costo</returns>
    ''' <remarks></remarks>
    Function GetCostCenter(ByVal code As String, Optional desatach As Boolean = True) As CostCenter

    ''' <summary>
    ''' Funcion para obtener la cuenta por id
    ''' </summary>
    ''' <param name="code">code.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Function GetCostCenterById(ByVal id As Integer, ByVal tracking As Boolean) As CostCenter
End Interface
