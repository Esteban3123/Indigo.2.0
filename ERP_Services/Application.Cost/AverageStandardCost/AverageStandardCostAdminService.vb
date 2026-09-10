Imports System.Transactions
Imports Application.Base
Imports Domain.Base.Entities
Imports Domain.Base.Entities.Enums.ElectronicDocuments
Imports Domain.Entities
Imports Domain.Entities.Service
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources

Public Class AverageStandardCostAdminService
    Implements IAverageStandardCostAdminService

    Public Sub New(averageStandardCostReposotory As IAverageStandardCostRepository, sequenceRepository As ICostSequenceDetailRepository,
                   averageStandardCostDetailRepository As IAverageStandardCostDetailsRepository, costServices As ICostServices)
        Me._costServices = costServices
        Me._averageStandardCostReposotory = averageStandardCostReposotory
        Me._sequenceRepository = sequenceRepository
        Me._averageStandardCostDetailRepository = averageStandardCostDetailRepository
    End Sub

#Region "Fields"
    Private _costServices As ICostServices
    Private _averageStandardCostReposotory As IAverageStandardCostRepository
    Private _averageStandardCostDetailRepository As IAverageStandardCostDetailsRepository
    Private _sequenceRepository As ICostSequenceDetailRepository
#End Region

    ''' <summary>
    ''' Guarda la entidad
    ''' </summary>
    ''' <param name="standarCost"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveAverageStandardCost(standarCost As StandarCost, audit As AuditMessage) As ActionResult(Of StandarCost) Implements IAverageStandardCostAdminService.SaveAverageStandardCost
        If standarCost Is Nothing Then Throw New ArgumentNullException("standarCost")

        'UnitWork
        Dim standarCostUnitWork = _averageStandardCostReposotory.UnitWork
        Dim standarCostDetailUnitWork = _averageStandardCostDetailRepository.UnitWork

        Try
            Dim txSettings = New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted}

            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, txSettings)
                Dim MessageResult = String.Empty
                If String.IsNullOrEmpty(standarCost.Code) Then

                    Dim sequenceDetail As CostSecuenceDetail = Me._sequenceRepository.GetSequenseDById(standarCost.IdSequence)
                    If sequenceDetail?.Id > 0 Then
                        Dim Sequence = Infrastructure.CrossCutting.Base.Sequense.GetSequense(sequenceDetail.Sequense.Pattern, sequenceDetail.[Next])
                        If Sequence IsNot Nothing AndAlso Not Sequence.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            standarCost.Code = Sequence
                            sequenceDetail.[Next] += 1
                            _sequenceRepository.SaveEntity(sequenceDetail)
                            _sequenceRepository.UnitWork.Commit()
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of StandarCost) With {.StateResult = False, .Message = "La secuencia numérica alcanzo el valor máximo"}
                        End If
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of StandarCost) With {.StateResult = False, .Message = ResourceManager.GetString("SequenceNotFound")}
                    End If

                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As StandarCost = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of StandarCost)
                Dim status As Infrastructure.CrossCutting.Audit.Actions

                If standarCost.ChangeTracker.State = ObjectState.Added Then
                    MessageResult = ResourceManager.GetString("SaveMessage")
                    standarCost.CreationUser = audit.CodeUser
                    standarCost.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    standarCost.ModificationUser = audit.CodeUser
                    standarCost.ModificationDate = DateTime.Now
                    auxObjEntity = standarCost.OriginalValue
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                If standarCost.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("StandarCostDetails") Then
                    standarCost.ChangeTracker.ObjectsRemovedFromCollectionProperties("StandarCostDetails").Clear()
                End If
                If standarCost.Id > 0 Then
                    Dim ids = standarCost.StandarCostDetails.Where(Function(w) w.Id > 0).Select(Function(s) s.Id).ToList()
                    Dim toDelete = _averageStandardCostDetailRepository.GetByFilter(Function(m) m.StandarCostId = standarCost.Id AndAlso Not ids.Contains(m.Id))
                    For Each itemDelete In toDelete
                        _averageStandardCostDetailRepository.DeleteEntity(itemDelete)
                    Next
                End If
                For Each item In standarCost.StandarCostDetails
                    If item.Id = 0 Then
                        item.CreationUser = audit.CodeUser
                        item.CreationDate = DateTime.Now
                    Else
                        item.ModificationUser = audit.CodeUser
                        item.ModificationUser = DateTime.Now
                    End If
                Next

                standarCostDetailUnitWork.Commit()
                _averageStandardCostReposotory.SaveEntity(standarCost)
                standarCostUnitWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of StandarCost)(standarCost, audit, status, auxObjEntity)
                auditProcess.Execute()
                standarCost.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of StandarCost) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = MessageResult, .ObjectEmbbeded = standarCost}
            End Using
        Catch ex As Exception
            standarCostUnitWork.RollbackChanges()
            Return New ActionResult(Of StandarCost) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene al entidad por código
    ''' </summary>
    ''' <param name="standarCostCode"></param>
    ''' <returns></returns>
    Public Function GetAverageStandardCostByCode(standarCostCode As String) As ActionResult(Of StandarCost) Implements IAverageStandardCostAdminService.GetAverageStandardCostByCode
        Try
            Dim record = Me._averageStandardCostReposotory.GetAverageStandardCostByCode(standarCostCode)
            Return New ActionResult(Of StandarCost) With {.StateResult = True, .ObjectEmbbeded = record}
        Catch ex As Exception
            Return New ActionResult(Of StandarCost) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene al entidad por Id
    ''' </summary>
    ''' <param name="standarCostId"></param>
    ''' <returns></returns>
    Public Function GetAverageStandardCostById(standarCostId As Integer) As ActionResult(Of StandarCost) Implements IAverageStandardCostAdminService.GetAverageStandardCostById
        Try
            Dim record = Me._averageStandardCostReposotory.GetAverageStandardCostById(standarCostId)
            Return New ActionResult(Of StandarCost) With {.StateResult = True, .ObjectEmbbeded = record}
        Catch ex As Exception
            Return New ActionResult(Of StandarCost) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' carga datos desde un excel
    ''' </summary>
    ''' <param name="dataImportFile"></param>
    ''' <param name="dataCopyPaste"></param>
    ''' <returns></returns>
    Public Function ImportOrCopyAndPasteDetails(dataImportFile As List(Of ImportFileRow), dataCopyPaste As List(Of List(Of String))) As ActionResult(Of List(Of StandarCostDetails)) Implements IAverageStandardCostAdminService.ImportOrCopyAndPasteDetails
        Try
            Return _costServices.ImportOrCopyAndPasteStandarCostDetails(dataImportFile, dataCopyPaste)
        Catch ex As Exception
            Return New ActionResult(Of List(Of StandarCostDetails)) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
            End If
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
