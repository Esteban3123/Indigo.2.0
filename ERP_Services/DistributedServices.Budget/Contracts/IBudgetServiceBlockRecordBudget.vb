'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Jhossept Kevin Garay
' Created          : 04-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

<ServiceContract()> _
Public Interface IBudgetServiceBlockRecordBudget

    ''' <summary>
    ''' Lista Todos los registros bloqueados
    ''' </summary>
    ''' <param name="session">Objeto session</param>
    ''' <returns>Registros bloqueados</returns>
    <OperationContract()> _
    Function ListAllBlockRecord() As List(Of BlockRecordBudget)

    ''' <summary>
    ''' Obtiene una registro de bloqueo según parametros
    ''' </summary>
    ''' <param name="IdForm">Id del formulario</param>
    ''' <param name="IdRecord">Id del registro</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>Registro bloqueado</returns>
    <OperationContract()> _
    Function GetBlockRecordByIdformAndIdRecord(ByVal IdForm As String, ByVal IdRecord As String) As BlockRecordBudget

    ''' <summary>
    ''' Almacena o Actualiza registro bloqueado
    ''' </summary>
    ''' <param name="blockRecord">Registro bloqueado</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>ActionResult</returns>
    <OperationContract()> _
    Function SaveBlockRecord(ByVal blockRecord As BlockRecordBudget) As ActionResult(Of BlockRecordBudget)

    ''' <summary>
    ''' Elimina una registro bloqueado
    ''' </summary>
    ''' <param name="blockRecord">Registro bloqueado</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>ActionResult</returns>
    <OperationContract()> _
    Function DeleteBlockRecord(ByVal blockRecord As BlockRecordBudget) As ActionResult



End Interface

