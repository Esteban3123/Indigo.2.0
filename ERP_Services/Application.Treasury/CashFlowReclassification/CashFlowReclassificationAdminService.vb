Imports System.Data.SqlClient
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources


Public Class CashFlowReclassificationAdminService
    Implements ICashFlowReclassificationAdminService

    Private _CashFlowReclassificationRepository As ICashFlowReclassificationRepository

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="CashFlowReclassificationRepository"></param>
    Public Sub New(ByVal CashFlowReclassificationRepository As ICashFlowReclassificationRepository)
        If CashFlowReclassificationRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de CashFlowReclassificationRepository vacio")
        End If
        _CashFlowReclassificationRepository = CashFlowReclassificationRepository

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="oCashFlowReclassification"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveCashFlowReclassification(ByVal oCashFlowReclassification As CashFlowReclassification, ByVal audit As AuditMessage) As ActionResult(Of CashFlowReclassification) Implements ICashFlowReclassificationAdminService.SaveCashFlowReclassification
        If oCashFlowReclassification Is Nothing Then
            Throw New ArgumentNullException("CashFlowReclassification")
        End If
        Dim parameters As String = String.Empty
        Dim item As String = String.Empty

        For Each d In oCashFlowReclassification.CashFlowReclassificationDetail
            item = String.Format("<{0}>{1}</{0}>", "DI", d.DocumentId)
            If d.PreviousCashFlowConcept IsNot Nothing Then
                item = String.Format("{0}<{1}>{2}</{1}>", item, "PCFC", d.PreviousCashFlowConcept)
            End If
            If d.CurrentCashFlowConcept IsNot Nothing Then
                item = String.Format("{0}<{1}>{2}</{1}>", item, "CCFC", d.CurrentCashFlowConcept)
            End If
            item = String.Format("<{0}>{1}</{0}>", "CFRD", item)
            parameters = String.Format("{0}{1}", parameters, item)
        Next
        item = String.Format("<{0}>{1}</{0}>", "DT", oCashFlowReclassification.DocumentType)
        parameters = String.Format("{0}{1}", parameters, item)
        parameters = String.Format("<{0}>{1}</{0}>", "CFR", parameters)
        Dim unitOfWork As IUnitWork = _CashFlowReclassificationRepository.UnitWork
        Dim conx As String = Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, ServerSessionValues.Current.CurrentContainer, False)

        Using connection As SqlConnection = New SqlConnection(conx)
            connection.Open()
            Dim command = New SqlCommand("Treasury.SP_CashFlowReclassification")
            Dim transaction As SqlTransaction
            transaction = connection.BeginTransaction()

            Try
                Dim auxCashFlowReclassification As Domain.Entities.CashFlowReclassification = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of Domain.Entities.CashFlowReclassification)
                Dim status As Infrastructure.CrossCutting.Audit.Actions = Infrastructure.CrossCutting.Audit.Actions.Insert

                If oCashFlowReclassification.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    oCashFlowReclassification.CreationUser = audit.CodeUser
                    oCashFlowReclassification.CreationDate = DateTime.Now
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
                    Return New ActionResult(Of Domain.Entities.CashFlowReclassification) With {
                                .StateResult = False,
                                .Message = dt.Rows(0).Field(Of String)("Message")
                            }
                End If

                _CashFlowReclassificationRepository.SaveEntity(oCashFlowReclassification)

                transaction.Commit()
                unitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of Domain.Entities.CashFlowReclassification)(oCashFlowReclassification, audit, status, auxCashFlowReclassification)
                auditProcess.Execute()
                Return New ActionResult(Of Domain.Entities.CashFlowReclassification) With {
                    .StateResult = True,
                    .ObjectEmbbeded = oCashFlowReclassification
                }
            Catch ex As System.Data.Entity.Core.OptimisticConcurrencyException
                transaction.Rollback()
                transaction.Dispose()
                unitOfWork.RollbackChangesUnitOfWork()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of Domain.Entities.CashFlowReclassification) With {
                    .StateResult = False,
                    .Message = ResourceManager.GetString("ErrorConcurrence")
                }
            Catch ex As System.Data.Entity.Core.UpdateException
                transaction.Rollback()
                transaction.Dispose()
                unitOfWork.RollbackChangesUnitOfWork()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of Domain.Entities.CashFlowReclassification) With {
                    .StateResult = False,
                    .Message = ResourceManager.GetString("ErrorUnknown")
                }
            Catch ex As Exception
                transaction.Rollback()
                transaction.Dispose()
                unitOfWork.RollbackChangesUnitOfWork()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of Domain.Entities.CashFlowReclassification) With {.StateResult = False, .Message = ex.Message}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetCashFlowReclassificationById(ByVal id As Integer) As Domain.Entities.CashFlowReclassification Implements ICashFlowReclassificationAdminService.GetCashFlowReclassificationById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If

        Try
            Return _CashFlowReclassificationRepository.GetCashFlowReclassificationById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
            End If
            _CashFlowReclassificationRepository = Nothing

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

