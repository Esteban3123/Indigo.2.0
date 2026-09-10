Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IPayrollCostCenter

    ''' <summary>
    ''' Lista todos los centros de costos
    ''' </summary>
    ''' <returns>Lista de centros de costos</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ListAllCostCenter(session As SessionValues) As List(Of CostCenter)

    ''' <summary>
    ''' Obtiene un centro de costo especifico
    ''' </summary>
    ''' <param name="code">Codigo del centro de costo</param>
    ''' <returns>Centro de costo</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetCostCenter(ByVal code As String, session As SessionValues) As CostCenter

    ''' <summary>
    ''' Graba o actualiza un centro de costo
    ''' </summary>
    ''' <param name="CostCenter">Centro de costo a guardar</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveCostCenter(ByVal costCenter As CostCenter, session As SessionValues, idSequense As Int64) As ActionResult(Of Domain.Payroll.Entities.CostCenter)

    ''' <summary>
    ''' Elimina un centro de costo
    ''' </summary>
    ''' <param name="CostCenter">Centro de costo</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function DeleteCostCenter(ByVal costCenter As CostCenter, session As SessionValues) As ActionMessageResult(Of CostCenter)

    ''' <summary>
    ''' Funcion para obtener la cuenta por id
    ''' </summary>
    ''' <param name="code">code.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCostCenterById(ByVal id As Integer, ByVal tracking As Boolean, ByVal Session As SessionValues) As CostCenter

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateCostCenter(ByVal code As String, ByVal state As Boolean, session As SessionValues) As ActionResult(Of Domain.Payroll.Entities.CostCenter)

End Interface
