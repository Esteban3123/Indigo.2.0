'***********************************************************************
' Assembly         : DistributedServices.Common
' Author           : Juan Diego Diaz Mosquera
' Created          : 03-10-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************


Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface ICommonERPBlockRecord

        ''' <summary>
    ''' Lista Todos los registros bloqueados
    ''' </summary>
    ''' <param name="session">Objeto session</param>
    ''' <returns>Registros bloqueados</returns>
    <OperationContract()> _
    Function ListAllBlockRecord(session As SessionValues) As List(Of BlockRecord)

    ''' <summary>
    ''' Obtiene una registro de bloqueo según parametros
    ''' </summary>
    ''' <param name="IdForm">Id del formulario</param>
    ''' <param name="IdRecord">Id del registro</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>Registro bloqueado</returns>
    <OperationContract()> _
    Function GetBlockRecordByIdformAndIdRecord(ByVal IdForm As String, ByVal IdRecord As String, session As SessionValues) As BlockRecord

    ''' <summary>
    ''' Almacena o Actualiza registro bloqueado
    ''' </summary>
    ''' <param name="blockRecord">Registro bloqueado</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>ActionResult</returns>
    <OperationContract()> _
    Function SaveBlockRecord(ByVal blockRecord As BlockRecord, ByVal session As SessionValues) As ActionResult(Of BlockRecord)

    ''' <summary>
    ''' Elimina una registro bloqueado
    ''' </summary>
    ''' <param name="blockRecord">Registro bloqueado</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>ActionResult</returns>
    <OperationContract()> _
    Function DeleteBlockRecord(ByVal blockRecord As BlockRecord, ByVal session As SessionValues) As ActionResult

    ''' <summary>
    ''' Limpiar regsitro de bloqueo
    ''' </summary>
    ''' <param name="CodUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function SP_UnlockBlockRecord(ByVal CodUser As String, session As Infrastructure.CrossCutting.Base.SessionValues) As Boolean

End Interface

