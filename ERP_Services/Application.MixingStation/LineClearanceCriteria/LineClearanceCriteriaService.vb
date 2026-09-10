'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Andres Alarcon
' Created          : 21-02-2025
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Transactions
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources

#End Region

Public Class LineClearanceCriteriaService
    Implements ILineClearanceCriteriaService, Inject

#Region "Properties"
    Private Const FORM_NAME As String = "FrmLineClearanceCriteria"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    Private _lineClearanceCriteriaRepository As ILineClearanceCriteriaRepository

    ''' <summary>
    ''' Repositorio de secuencias numéricas
    ''' </summary>
    Private _secuenseDetailRepository As IMixingStationSequenceDetailRepository
#End Region

#Region "Methods"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal lineClearanceCriteriaRepository As ILineClearanceCriteriaRepository,
                   ByVal secuenseDetailRepository As IMixingStationSequenceDetailRepository)

        If lineClearanceCriteriaRepository Is Nothing Then
            Throw New ArgumentNullException("lineClearanceCriteriaRepository Vacio")
        End If

        If secuenseDetailRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDetailRepository")
        End If

        Me._lineClearanceCriteriaRepository = lineClearanceCriteriaRepository
        Me._secuenseDetailRepository = secuenseDetailRepository
    End Sub

    ''' <summary>
    ''' Elimina un criterio de despeje de linea
    ''' </summary>
    ''' <param name="_lineClearanceCriteria">The identifier.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Function DeleteLineClearanceCriteria(_lineClearanceCriteria As LineClearanceCriteria, audit As AuditMessage) As ActionResult Implements ILineClearanceCriteriaService.DeleteLineClearanceCriteria
        If _lineClearanceCriteria Is Nothing Then
            Throw New ArgumentNullException("ProductionLine")
        End If

        Dim unitOfWork As IUnitWork = Me._lineClearanceCriteriaRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of LineClearanceCriteria)(_lineClearanceCriteria, audit, status)

                If _lineClearanceCriteria.LineClearanceCriteriaUnitDoseType IsNot Nothing Then
                    _lineClearanceCriteria.LineClearanceCriteriaUnitDoseType.ToList().ForEach(Sub(x)
                                                                                                  x.MarkAsDeleted()
                                                                                              End Sub)
                End If

                _lineClearanceCriteria.MarkAsDeleted()
                Me._lineClearanceCriteriaRepository.SaveEntity(_lineClearanceCriteria)
                unitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using

        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un criterio de despeje de linea mediante el codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Function GetLineClearanceCriteriaByCode(code As String, audit As AuditMessage) As ActionResult(Of LineClearanceCriteria) Implements ILineClearanceCriteriaService.GetLineClearanceCriteriaByCode
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If

        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If

        Try
            Dim _lineClearanceCriteria As LineClearanceCriteria = Me._lineClearanceCriteriaRepository.GetLineClearanceCriteriaByCode(code)
            If _lineClearanceCriteria?.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of LineClearanceCriteria)(_lineClearanceCriteria, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If

            Return New ActionResult(Of LineClearanceCriteria) With {.StateResult = True, .ObjectEmbbeded = _lineClearanceCriteria}

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of LineClearanceCriteria) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza un criterio de despeje de linea
    ''' </summary>
    ''' <param name="_lineClearanceCriteria"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveLineClearanceCriteria(_lineClearanceCriteria As LineClearanceCriteria, audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of LineClearanceCriteria) Implements ILineClearanceCriteriaService.SaveLineClearanceCriteria
        If _lineClearanceCriteria Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._lineClearanceCriteriaRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDetailRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(_lineClearanceCriteria.Code) Then
                    Dim seq As MixingStationSequenceDetail = Me._secuenseDetailRepository.GetSequenceDById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MixingStationSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            _lineClearanceCriteria.Code = res
                            seq.Next += 1
                            Me._secuenseDetailRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of LineClearanceCriteria) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.MixingStationSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), _lineClearanceCriteria.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of LineClearanceCriteria) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As LineClearanceCriteria = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of LineClearanceCriteria)
                Dim status As Integer

                If _lineClearanceCriteria.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    _lineClearanceCriteria.Creationuser = audit.CodeUser
                    _lineClearanceCriteria.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = _lineClearanceCriteria.OriginalValue
                    _lineClearanceCriteria.ModificationUser = audit.CodeUser
                    _lineClearanceCriteria.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._lineClearanceCriteriaRepository.SaveEntity(_lineClearanceCriteria)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of LineClearanceCriteria)(_lineClearanceCriteria, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                _lineClearanceCriteria.MarkAsUnchanged()
                scope.Complete()

                Return New ActionResult(Of LineClearanceCriteria) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = _lineClearanceCriteria, .Message = MessageResult}
            End Using
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of LineClearanceCriteria) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Actualiza el estado de un criterio de despeje de linea
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateStateLineClearanceCriteria(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of LineClearanceCriteria) Implements ILineClearanceCriteriaService.UpdateStateLineClearanceCriteria
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If String.IsNullOrEmpty(state) Then
            Throw New ArgumentNullException("state")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If

        Try
            Dim _lineClearanceCriteria As LineClearanceCriteria = Me._lineClearanceCriteriaRepository.GetLineClearanceCriteriaByCode(code)
            If _lineClearanceCriteria?.Id > 0 Then
                _lineClearanceCriteria.State = state
            End If
            Return Me.SaveLineClearanceCriteria(_lineClearanceCriteria, audit)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of LineClearanceCriteria) With {.StatusCode = eStatusResult.EXCEPTION, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
