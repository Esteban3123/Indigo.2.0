'***********************************************************************
' Assembly         : Application.Accounting
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/06/2017
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IMassiveReplicationAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Consume el sp de replicación masiva
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_MassiveReplication(InitialDate As Date?, EndDate As Date?, BookOriginId As Integer, BookDestinationId As Integer, CodeInitialJournalVoucherType As String, CodeEndJournalVoucherType As String, Audit As AuditMessage) As ActionResult(Of SP_MassiveReplication_Result)

    ''' <summary>
    ''' Metodo para realizar replicacion masiva 2.0, la otra replicacion la realiza dentro de un ciclo.
    ''' </summary>
    ''' <param name="InitialDate"></param>
    ''' <param name="EndDate"></param>
    ''' <param name="BookOriginId"></param>
    ''' <param name="BookDestinationId"></param>
    ''' <returns></returns>
    Function SP_MassiveReplication2(InitialDate As Date?, EndDate As Date?, BookOriginId As Integer, BookDestinationId As Integer, session As SessionValues) As ActionResult

End Interface
