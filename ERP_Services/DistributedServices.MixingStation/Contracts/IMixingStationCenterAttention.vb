'***********************************************************************
' Assembly         : DistributedServices.MixinStation
' Author           : Ruben Dario Castañeda Giraldo
' Created          : 06/05/2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
<ServiceContract()>
Public Interface IMixingStationCenterAttention
    ''' <summary>
    ''' Lista todos los paràmetros de central de mezclas
    ''' </summary>
    ''' <returns>Lista de turnos</returns>
    <OperationContract()>
    Function ListAllCMCenterAttention(audit As AuditMessage) As List(Of CMCenterAttention)

    ''' <summary>
    ''' Guarda cel centro de mezclas
    ''' </summary>
    ''' <param name="cmCenterAttention">The identifier.</param>
    <OperationContract()>
    Function SaveCMCenterAttention(ByVal cmCenterAttention As CMCenterAttention) As ActionResult(Of CMCenterAttention)

    ''' <summary>
    ''' Actualiza los parámetros de central de mezclas
    ''' </summary>
    ''' <param name="state">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    <OperationContract()>
    Function UpdateStateCMCenterAttention(ByVal code As String, ByVal state As Boolean, audit As AuditMessage) As ActionResult(Of CMCenterAttention)

    ''' <summary>
    ''' Obtiene los parámetros de central de mezclas por id
    ''' </summary>
    <OperationContract()>
    Function GetCMCenterAttention(ByVal id As String) As ActionResult(Of CMCenterAttention)
    ''' <summary>
    ''' Obtiene un tipo de dosis unitaria por id
    ''' </summary>
    ''' <param name="Id_MIxingStation">The identifier.</param>
    <OperationContract()>
    Function ListAllCMMixingProducitonLine(Id_MIxingStation As Integer, audit As AuditMessage) As List(Of Tuple(Of Integer, String))
End Interface
