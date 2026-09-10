'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 27-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IPayrollWorkCenter

    ''' <summary>
    ''' Lista Todos los Centros de Trabajo
    ''' </summary>
    ''' <returns>Listado de Centros de Trabajo</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ListAllWorkCenter(session As SessionValues) As List(Of WorkCenter)

    ''' <summary>
    ''' Obtiene un Centro de Trabajo
    ''' </summary>
    ''' <param name="code">Código del Centro de Trabajo</param>
    ''' <returns>Centro de Trabajo</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetWorkCenter(ByVal code As String, session As SessionValues) As WorkCenter

    ''' <summary>
    ''' Almacena o Actualiza un Centro de Trabajo
    ''' </summary>
    ''' <param name="workCenter">Centro de Trabajo</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function SaveWorkCenter(ByVal workCenter As WorkCenter, session As SessionValues) As Boolean

    ''' <summary>
    ''' Elimina un Centro de Trabajo
    ''' </summary>
    ''' <param name="workCenter">Centro de Trabajo</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function DeleteWorkCenter(ByVal workCenter As WorkCenter, session As SessionValues) As ActionMessageResult(Of WorkCenter)
End Interface
