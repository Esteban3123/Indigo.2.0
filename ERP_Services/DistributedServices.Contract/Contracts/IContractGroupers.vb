'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 11/07/2017
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface IContractGroupers

    ''' <summary>
    ''' Guarda o Actualiza un rango uvr
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveGroupers(ByVal Groupers As Groupers, idSequense As Int64, audit As AuditMessage) As ActionResult(Of Groupers)

    ''' <summary>
    ''' Elimina un rango uvr
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteGroupers(ByVal Groupers As Groupers, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un rango uvr por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetGroupers(ByVal code As String, audit As AuditMessage) As ActionResult(Of Groupers)

    ''' <summary>
    ''' Obtiene un rango uvr por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetGroupersById(ByVal id As Integer, audit As AuditMessage) As ActionResult(Of Groupers)

    ''' <summary>
    ''' metodo para pegar cups en la rejilla del form de agrupadores
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function CopyAndPasteGroupersCups(data As List(Of List(Of String))) As ActionResult(Of List(Of GroupersCups))

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateGroupers(ByVal code As String, ByVal state As Boolean, audit As AuditMessage) As ActionResult(Of Groupers)

    ''' <summary>
    ''' Importa registros de los agrupadores
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function ImportGroupers(ByVal workSheetType As eGrouperWorkSheetType, data As List(Of ImportFileRow), ByVal audit As AuditMessage) As ActionResult(Of List(Of String))

End Interface
