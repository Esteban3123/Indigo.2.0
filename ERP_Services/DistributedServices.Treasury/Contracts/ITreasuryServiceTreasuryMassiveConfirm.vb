'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Carlos Ernesto Cordoba
' Created          : 04-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()>
Public Interface ITreasuryServiceTreasuryMassiveConfirm

    ''' <summary>
    ''' Metodo para confirmar un documentos del módulo de tesoreria
    ''' </summary>
    ''' <param name="processId"></param>
    ''' <param name="code"></param>
    ''' <param name="session"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ConfirmTreasuryDocument(processId As Integer, code As String, session As SessionValues, audit As AuditMessage) As ActionResult(Of Tuple(Of String, Integer))

    ''' <summary>
    ''' metodo para confirmar documentos de tesoreria masivamente
    ''' </summary>
    ''' <param name="processId"></param>
    ''' <param name="listDocuments"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ConfirmTreasuryDocuments(processId As Integer, listDocuments As List(Of String), IndigoSessionValues As SessionValues, audit As AuditMessage) As ActionResult(Of List(Of Tuple(Of String, Integer)))

End Interface