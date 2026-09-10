'***********************************************************************
' Assembly         : Application.Accounting
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/06/2017
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports Application.Accounting
Imports System.Transactions
Imports System.Data.SqlClient
Imports System.Resources

Public Class MassiveReplicationAdminService
    Implements IMassiveReplicationAdminService

#Region "Variables"

    ''' <summary>
    ''' Variable tipo repositorio para replicacion masiva
    ''' </summary>
    ''' <remarks></remarks>
    Private _massiveReplicationRepository As IMassiveReplicationRepository

#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(massiveReplicationRepository As IMassiveReplicationRepository)
        If massiveReplicationRepository Is Nothing Then
            Throw New ArgumentNullException("massiveReplicationRepository Vacio")
        End If
        _massiveReplicationRepository = massiveReplicationRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Replicación masiva
    ''' </summary>
    ''' <param name="InitialDate"></param>
    ''' <param name="EndDate"></param>
    ''' <param name="BookOriginId"></param>
    ''' <param name="BookDestinationId"></param>
    ''' <param name="CodeInitialJournalVoucherType"></param>
    ''' <param name="CodeEndJournalVoucherType"></param>
    ''' <param name="Audit"></param>
    ''' <returns></returns>
    Public Function SP_MassiveReplication(InitialDate As Date?, EndDate As Date?, BookOriginId As Integer, BookDestinationId As Integer, CodeInitialJournalVoucherType As String, CodeEndJournalVoucherType As String, Audit As AuditMessage) As ActionResult(Of SP_MassiveReplication_Result) Implements IMassiveReplicationAdminService.SP_MassiveReplication
        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using Transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try

                'Se envia la info al sp
                Dim resultStore = _massiveReplicationRepository.SP_MassiveReplication(InitialDate, EndDate, BookOriginId, BookDestinationId, CodeInitialJournalVoucherType, CodeEndJournalVoucherType, Audit.CodeUser)

                If resultStore.CodeMessage <> 0 Then 'Si retorno errores
                    Transaction.Dispose()
                    Return New ActionResult(Of SP_MassiveReplication_Result) With {.StateResult = False, .Message = resultStore.Message}
                End If

                Transaction.Complete()
                Return New ActionResult(Of SP_MassiveReplication_Result) With {.StateResult = True, .Message = resultStore.Message}
            Catch ex As OptimisticConcurrencyException
                Transaction.Dispose()
                Return New ActionResult(Of SP_MassiveReplication_Result) With {.StateResult = False, .Message = ex.Message}
            Catch ex As Exception
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of SP_MassiveReplication_Result) With {.StateResult = False, .Message = ex.Message}
            End Try
        End Using
    End Function
    ''' <summary>
    ''' Metodo para realizar replicacion masiva 2.0, la otra replicacion la realiza dentro de un ciclo.
    ''' </summary>
    ''' <param name="InitialDate"></param>
    ''' <param name="EndDate"></param>
    ''' <param name="BookOriginId"></param>
    ''' <param name="BookDestinationId"></param>
    ''' <returns></returns>
    Public Function SP_MassiveReplication2(InitialDate As Date?, EndDate As Date?, BookOriginId As Integer, BookDestinationId As Integer, session As SessionValues) As ActionResult Implements IMassiveReplicationAdminService.SP_MassiveReplication2

        Dim conx As String = Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, ServerSessionValues.Current.CurrentContainer, False)

        Using connection As SqlConnection = New SqlConnection(conx)
            connection.Open()
            Dim command = New SqlCommand("GeneralLedger.SP_MassiveReplication2")
            Dim transaction As SqlTransaction
            transaction = connection.BeginTransaction()

            Try
                command.Connection = connection
                command.Transaction = transaction
                command.CommandType = CommandType.StoredProcedure
                command.Parameters.Add(New SqlParameter("@InitialDate", InitialDate))
                command.Parameters.Add(New SqlParameter("@EndDate", EndDate))
                command.Parameters.Add(New SqlParameter("@BookOriginId", BookOriginId))
                command.Parameters.Add(New SqlParameter("@BookDestinationId", BookDestinationId))
                command.Parameters.Add(New SqlParameter("@CodeUser", session.UserIndigo))
                Dim dt = New DataTable()

                Using adapter = New SqlDataAdapter(command)
                    adapter.SelectCommand.CommandTimeout = 0
                    adapter.Fill(dt)
                End Using

                If dt.Rows(0).Field(Of Integer)("CodeMessage") = 999 Then
                    transaction.Rollback()
                    transaction.Dispose()
                    Return New ActionResult With {
                    .StateResult = False,
                    .Message = dt.Rows(0).Field(Of String)("Message")
                    }
                End If
                transaction.Commit()

                Return New ActionResult With {
                .StateResult = True,
                .Message = dt.Rows(0).Field(Of String)("Message")
                }

            Catch ex As Exception
                transaction.Rollback()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
                Return New ActionResult With {.StateResult = False, .Message = ex.Message}
            End Try
        End Using
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            Me._massiveReplicationRepository = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
