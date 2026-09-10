'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 02/12/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports System.Data.Entity.Infrastructure
Imports System.Transactions
Imports Application.Base
Imports Application.MixingStation
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources

#End Region
Public Class ProductionBasketsAdminService
    Implements IProductionBasketsAdminService, Inject

#Region "Properties"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _productionBasketsRepository As IProductionBasketsRepository

    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDetailRepository As IMixingStationSequenceDetailRepository

#End Region

#Region "Methods"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(productionBasketsRepository As IProductionBasketsRepository, ByVal secuenseDetailRepository As IMixingStationSequenceDetailRepository)
        If productionBasketsRepository Is Nothing Then
            Throw New ArgumentNullException("productionBasketsRepository Vacio")
        End If
        If secuenseDetailRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDetailRepository")
        End If
        _productionBasketsRepository = productionBasketsRepository
        _secuenseDetailRepository = secuenseDetailRepository
    End Sub

    Public Function SaveProductionBaskets(ProductionBaskets As ProductionBaskets, audit As AuditMessage, operatingUnitId As Integer, Optional idSequense As Long = 0) As ActionResult(Of ProductionBaskets) Implements IProductionBasketsAdminService.SaveProductionBaskets
        If ProductionBaskets Is Nothing Then
            Throw New ArgumentNullException("ProductionBaskets")
        End If
        Dim unitOfWork As IUnitWork = Me._productionBasketsRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDetailRepository.UnitWork
        Try

            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                Dim seq As MixingStationSequenceDetail = Nothing
                If ProductionBaskets.Code Is Nothing OrElse ProductionBaskets.Code.Trim().Equals(String.Empty) Then
                    seq = Me._secuenseDetailRepository.GetSequenceDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MixingStationSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            ProductionBaskets.Code = res
                            seq.Next += 1
                            Me._secuenseDetailRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of ProductionBaskets) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = ResourceManager.GetString("SequenceNotFound")}
                        End If
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of ProductionBaskets) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = ResourceManager.GetString("SequenceNotFound")}
                    End If
                End If

                Dim auxProductionBaskets As ProductionBaskets = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of ProductionBaskets)
                Dim status As Integer

                If ProductionBaskets.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    ProductionBaskets.CreationUser = audit.CodeUser
                    ProductionBaskets.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    auxProductionBaskets = ProductionBaskets.OriginalValue
                    ProductionBaskets.ModificationUser = audit.CodeUser
                    ProductionBaskets.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._productionBasketsRepository.SaveEntity(ProductionBaskets)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of ProductionBaskets)(ProductionBaskets, audit, status, auxProductionBaskets)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                ProductionBaskets.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of ProductionBaskets) With {.StateResult = True, .ObjectEmbbeded = ProductionBaskets}
            End Using

        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of ProductionBaskets) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ProductionBaskets) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    Public Function DeleteProductionBaskets(ProductionBaskets As ProductionBaskets, audit As AuditMessage, TransactionalContainer As String) As ActionResult Implements IProductionBasketsAdminService.DeleteProductionBaskets
        If ProductionBaskets Is Nothing Then
            Throw New ArgumentNullException("ProductionBaskets")
        End If
        Dim unitOfWork As IUnitWork = Me._productionBasketsRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of ProductionBaskets)
            auditProcess = New IndigoAuditSimpleEntity(Of ProductionBaskets)(ProductionBaskets, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)

            While ProductionBaskets.ProductionBasketsDetail.Count > 0
                ProductionBaskets.ProductionBasketsDetail(ProductionBaskets.ProductionBasketsDetail.Count - 1).MarkAsDeleted()
            End While
            ProductionBaskets.MarkAsDeleted()

            Me._productionBasketsRepository.SaveEntity(ProductionBaskets)
            unitOfWork.Commit()
            auditProcess.Execute()
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    Public Function GetProductionBasketse(code As String, audit As AuditMessage) As ActionResult(Of ProductionBaskets) Implements IProductionBasketsAdminService.GetProductionBasketse
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim ProductionBaskets As ProductionBaskets = Me._productionBasketsRepository.GetProductionBaskets(code)
            If ProductionBaskets IsNot Nothing AndAlso ProductionBaskets.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of ProductionBaskets)(ProductionBaskets, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of ProductionBaskets) With {.StateResult = True, .ObjectEmbbeded = ProductionBaskets}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ProductionBaskets) With {.StateResult = False, .MessageResult = {ex.Message}.ToList, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function GetProductionBasketsById(id As Integer) As ActionResult(Of ProductionBaskets) Implements IProductionBasketsAdminService.GetProductionBasketsById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Try
            Dim ProductionBaskets As ProductionBaskets = Me._productionBasketsRepository.GetProductionBasketsById(id)
            Return New ActionResult(Of ProductionBaskets) With {.StateResult = True, .ObjectEmbbeded = ProductionBaskets}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ProductionBaskets) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function ChangeStateProductionBaskets(code As String, state As Boolean, audit As AuditMessage, operatingUnitId As Integer) As ActionResult(Of ProductionBaskets) Implements IProductionBasketsAdminService.ChangeStateProductionBaskets
        Dim ProductionBaskets As ProductionBaskets = _productionBasketsRepository.GetProductionBaskets(code)
        ProductionBaskets.Status = state
        Return SaveProductionBaskets(ProductionBaskets, audit, operatingUnitId)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If

            _productionBasketsRepository = Nothing
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
