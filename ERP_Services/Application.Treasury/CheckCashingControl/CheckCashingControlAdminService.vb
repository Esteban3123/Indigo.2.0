Imports System.Data.SqlClient
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources


Public Class CheckCashingControlAdminService
    Implements ICheckCashingControlAdminService

    Private _CheckCashingControlRepository As ICheckCashingControlRepository

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="CheckCashingControlRepository"></param>
    Public Sub New(ByVal CheckCashingControlRepository As ICheckCashingControlRepository)
        If CheckCashingControlRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de CheckCashingControlRepository vacio")
        End If
        _CheckCashingControlRepository = CheckCashingControlRepository

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="oCheckCashingControl"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveCheckCashingControl(ByVal oCheckCashingControl As CheckCashingControl, ByVal audit As AuditMessage) As ActionResult(Of CheckCashingControl) Implements ICheckCashingControlAdminService.SaveCheckCashingControl
        If oCheckCashingControl Is Nothing Then
            Throw New ArgumentNullException("CheckCashingControl")
        End If
        Dim parameters As String = String.Empty
        Dim item As String = String.Empty
        For Each d In oCheckCashingControl.CheckCashingControlDetail
            item = String.Format("<{0}>{1}</{0}>", "IdVT", d.IdVoucherTransaction)
            item = String.Format("{0}<{1}>{2}</{1}>", item, "PCS", d.PreviousCheckStatus)
            item = String.Format("{0}<{1}>{2}</{1}>", item, "CCS", d.CurrentCheckStatus)
            item = String.Format("<{0}>{1}</{0}>", "CCCD", item)
            parameters = String.Format("{0}{1}", parameters, item)
        Next
        parameters = String.Format("<{0}>{1}</{0}>", "CCC", parameters)
        Dim unitOfWork As IUnitWork = _CheckCashingControlRepository.UnitWork
        Dim conx As String = Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, ServerSessionValues.Current.CurrentContainer, False)

        Using connection As SqlConnection = New SqlConnection(conx)
            connection.Open()
            Dim command = New SqlCommand("Treasury.SP_ChangeCheckCashingStatus")
            Dim transaction As SqlTransaction
            transaction = connection.BeginTransaction()

            Try
                Dim auxCheckCashingControl As Domain.Entities.CheckCashingControl = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of Domain.Entities.CheckCashingControl)
                Dim status As Infrastructure.CrossCutting.Audit.Actions = Infrastructure.CrossCutting.Audit.Actions.Insert

                If oCheckCashingControl.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    oCheckCashingControl.CreationUser = audit.CodeUser
                    oCheckCashingControl.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                End If

                command.Connection = connection
                command.Transaction = transaction
                command.CommandType = CommandType.StoredProcedure
                command.Parameters.Add(New SqlParameter("@Parameters", parameters))
                Dim dt = New DataTable()

                Using adapter = New SqlDataAdapter(command)
                    adapter.SelectCommand.CommandTimeout = 90
                    adapter.Fill(dt)
                End Using

                If dt.Rows(0).Field(Of Byte)("Status") = 3 Then
                    transaction.Rollback()
                    transaction.Dispose()
                    unitOfWork.RollbackChangesUnitOfWork()
                    'IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                    Return New ActionResult(Of Domain.Entities.CheckCashingControl) With {
                                .StateResult = False,
                                .Message = dt.Rows(0).Field(Of String)("Message")
                            }
                End If

                _CheckCashingControlRepository.SaveEntity(oCheckCashingControl)

                transaction.Commit()
                unitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of Domain.Entities.CheckCashingControl)(oCheckCashingControl, audit, status, auxCheckCashingControl)
                auditProcess.Execute()
                Return New ActionResult(Of Domain.Entities.CheckCashingControl) With {
                    .StateResult = True,
                    .ObjectEmbbeded = oCheckCashingControl
                }
            Catch ex As System.Data.Entity.Core.OptimisticConcurrencyException
                transaction.Rollback()
                transaction.Dispose()
                unitOfWork.RollbackChangesUnitOfWork()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of Domain.Entities.CheckCashingControl) With {
                    .StateResult = False,
                    .Message = ResourceManager.GetString("ErrorConcurrence")
                }
            Catch ex As System.Data.Entity.Core.UpdateException
                transaction.Rollback()
                transaction.Dispose()
                unitOfWork.RollbackChangesUnitOfWork()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of Domain.Entities.CheckCashingControl) With {
                    .StateResult = False,
                    .Message = ResourceManager.GetString("ErrorUnknown")
                }
            Catch ex As Exception
                transaction.Rollback()
                transaction.Dispose()
                unitOfWork.RollbackChangesUnitOfWork()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of Domain.Entities.CheckCashingControl) With {.StateResult = False, .Message = ex.Message}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetCheckCashingControlById(ByVal id As Integer) As Domain.Entities.CheckCashingControl Implements ICheckCashingControlAdminService.GetCheckCashingControlById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If

        Try
            Return _CheckCashingControlRepository.GetCheckCashingControlById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="parameters"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function ListCheckCashingControl(ByVal parameters As String, ByVal session As SessionValues) As DataSet Implements ICheckCashingControlAdminService.ListCheckCashingControl
        If String.IsNullOrEmpty(parameters) Then Throw New ArgumentNullException("parameters")

        Try
            Dim ds As DataSet
            Dim query1 As String
            query1 = String.Format("exec Treasury.SP_ChangeCheckCashingStatus '{0}'", parameters)
            ds = Me.GetDatatable(query1, session, "SP_ChangeCheckCashingStatus")
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Comando"></param>
    ''' <param name="session"></param>
    ''' <param name="nameDt"></param>
    ''' <returns></returns>
    Private Function GetDatatable(ByVal Comando As String, ByVal session As SessionValues, ByVal nameDt As String) As DataSet
        Dim connectionString = String.Empty
        connectionString = Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, session.TransactionalContainer, False)
        Using conexion As SqlConnection = New SqlConnection()
            Dim _GetDatatable As DataSet

            Try

                If conexion.State = ConnectionState.Closed Then
                    conexion.Open()
                End If

                Dim da As SqlDataAdapter = New SqlDataAdapter(Comando, conexion)
                da.SelectCommand.CommandTimeout = 30000
                Dim ds As DataSet = New DataSet()
                da.Fill(ds, nameDt)
                _GetDatatable = ds
                da = Nothing
                ds = Nothing
                conexion.Close()
                Return _GetDatatable
            Catch ex As Exception
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
                Return Nothing
            Finally
                conexion.Close()
            End Try
        End Using
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
            End If
            _CheckCashingControlRepository = Nothing

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

