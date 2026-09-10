'***********************************************************************
' Assembly         : DistributedServices.Accounting
' Author           : Carlos Ernesto Cordoba
' Created          : 04-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.ServiceModel
Imports Domain.Base.Entities

<ServiceContract()>
Public Interface IAccountingMassiveConfirm

    ''' <summary>
    ''' Metodo para confirmar un documentos del módulo de Contabilidad
    ''' </summary>
    ''' <param name="code"></param>
    <OperationContract()>
    Function ConfirmAccountingDocument(code As String) As ActionResult(Of Tuple(Of String, Integer))

    ''' <summary>
    ''' metodo para confirmar documentos de tesoreria masivamente
    ''' </summary>
    ''' <param name="listDocuments"></param>
    <OperationContract()>
    Function ConfirmAccountingDocuments(listDocuments As List(Of String)) As ActionResult(Of List(Of Tuple(Of String, Integer)))

End Interface