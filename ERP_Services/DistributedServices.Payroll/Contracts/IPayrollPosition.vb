'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 06-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IPayrollPosition

    ''' <summary>
    ''' Lista todos los cargos
    ''' </summary>
    ''' <returns>Lista de cargos</returns>
    <OperationContract()> _
    Function ListAllPosition(session As SessionValues) As List(Of Position)

    ''' <summary>
    ''' Elimina un cargo
    ''' </summary>
    ''' <param name="position">Cargo</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function DeletePosition(ByVal position As Position, session As SessionValues) As ActionMessageResult(Of Position)

    ''' <summary>
    ''' Guarda o edita un cargo
    ''' </summary>
    ''' <param name="position">Cargo</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function SavePosition(ByVal position As Position, session As SessionValues) As Boolean

    ''' <summary>
    ''' Obtiene un cargo
    ''' </summary>
    ''' <param name="code">Código del cargo</param>
    ''' <returns>Cargo</returns>
    <OperationContract()> _
    Function GetPosition(ByVal code As String, session As SessionValues) As Position

    ''' <summary>
    ''' Obtiene un cargo
    ''' </summary>
    ''' <param name="code">Código del cargo</param>
    ''' <returns>Cargo</returns>
    <OperationContract()> _
    Function ChangeStatePosition(ode As String, state As Boolean, session As SessionValues) As Boolean

End Interface
