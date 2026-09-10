'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 25-09-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ICrossingAccountAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene un registro de cruce de cuentas por ir
    ''' </summary>
    Function GetCrossingAccountById(ByVal Id As Integer, Optional tracking As Boolean = False) As CrossingAccount

    ''' <summary>
    ''' Obtiene un registro de cruce de cuentas por codigo
    ''' </summary>
    Function GetCrossingAccount(ByVal code As String, ByVal audit As AuditMessage, Optional tracking As Boolean = False) As ActionResult(Of CrossingAccount)

    ''' <summary>
    ''' Guardar Cruce de Cuentas
    ''' </summary>
    Function SaveCrossingAccount(ByVal crossingAccount As CrossingAccount, ByVal audit As AuditMessage, ByVal withConfirm As Boolean, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of CrossingAccount)

    ''' <summary>
    ''' Confirma el cruce de cuentas CxP y CxC
    ''' </summary>
    Function ConfirmCrossingAccount(ByVal crossingAccountId As Integer, ByVal audit As AuditMessage, Optional ByVal crossingAccount As CrossingAccount = Nothing) As ActionResult(Of String)

    ''' <summary>
    ''' Elimina un cruce de cuentas
    ''' </summary>
    Function DeleteCrossingAccount(ByVal crossingAccount As CrossingAccount, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    '''  establece las cuentas por cobrar del copiar y pegar
    ''' </summary>
    ''' <param name="data">Listado que se va a procesar</param>
    ''' <param name="idThirdPaty"></param>
    ''' <param name="crossingType">1-Mismo Tercero, 2-Diferente Tercero</param>
    ''' <param name="processType">1 - CxP, 2 - CxC </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetDocumentsCrossingCopyPaste(data As List(Of List(Of String)), idThirdPaty As Integer, crossingType As Integer, processType As Integer) As ActionResult(Of List(Of CrossingAccountDetailCxP), List(Of CrossingAccountDetailCxC))


    Function SetDocumentsCrossingImportFile(data As List(Of ImportFileRow), idThirdPaty As Integer, crossingType As Integer, processType As Integer) As ActionResult(Of List(Of CrossingAccountDetailCxP), List(Of CrossingAccountDetailCxC))
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="treasuryNote"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function ReverseCrossingAccount(treasuryNote As TreasuryNote, audit As AuditMessage) As ActionResult(Of String)
End Interface
