'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 25-09-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface ITreasuryServiceCrossingAccount

    ''' <summary>
    ''' Obtiene un registro de cruce de cuentas por ir
    ''' </summary>
    <OperationContract()>
    Function GetCrossingAccountById(ByVal Id As Integer, Optional tracking As Boolean = False) As CrossingAccount

    ''' <summary>
    ''' Obtiene un registro de cruce de cuentas por codigo
    ''' </summary>
    <OperationContract()>
    Function GetCrossingAccount(code As String, audit As AuditMessage, Optional tracking As Boolean = False) As ActionResult(Of CrossingAccount)

    ''' <summary>
    ''' Guardar Cruce de Cuentas
    ''' </summary>
    <OperationContract()>
    Function SaveCrossingAccount(crossingAccount As CrossingAccount, withConfirm As Boolean, audit As AuditMessage, idSequence As Int64) As ActionResult(Of CrossingAccount)

    ''' <summary>
    ''' Comfirma el cruce de cuentas
    ''' </summary>
    <OperationContract()>
    Function ConfirmCrossingAccount(crossingAccountId As Integer, audit As AuditMessage) As ActionResult(Of String)

    ''' <summary>
    ''' Elimina un cruce de cuentas
    ''' </summary>
    <OperationContract()>
    Function DeleteCrossingAccount(crossingAccount As CrossingAccount, audit As AuditMessage) As ActionResult

    ''' <summary>
    '''  establece las cuentas por cobrar del copiar y pegar
    ''' </summary>
    ''' <param name="data">Listado que se va a procesar</param>
    ''' <param name="idThirdPaty"></param>
    ''' <param name="crossingType">1-Mismo Tercero, 2-Diferente Tercero</param>
    ''' <param name="processType">1 - CxP, 2 - CxC </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SetDocumentsCrossingCopyPaste(data As List(Of List(Of String)), idThirdPaty As Integer, crossingType As Integer, processType As Integer) As ActionResult(Of List(Of CrossingAccountDetailCxP), List(Of CrossingAccountDetailCxC))

    <OperationContract()>
    Function SetDocumentsCrossingImportFile(data As List(Of ImportFileRow), idThirdPaty As Integer, crossingType As Integer, processType As Integer) As ActionResult(Of List(Of CrossingAccountDetailCxP), List(Of CrossingAccountDetailCxC))
End Interface