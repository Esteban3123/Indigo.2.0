Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports Application.Payroll
Imports System.Data.Entity.Core

Public Class BlockScheduleAdminService

    Implements IBlockScheduleAdminService

    'Repositorio de bancos
    Private _BlockScheduleRepository As IBlockScheduleRepository

    Private _BlocScheduleCRepository As IBlockScheduleCRepository

    Public Sub New(ByVal blockScheduleRepository As IBlockScheduleRepository, BlockScheduleCRepository As IBlockScheduleCRepository)
        If (blockScheduleRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de blockScheduleRepository vacio")
        End If

        If (BlockScheduleCRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de BlockScheduleCRepository vacio")
        End If
        _BlockScheduleRepository = blockScheduleRepository
        _BlocScheduleCRepository = BlockScheduleCRepository
    End Sub

    Public Function ListAllBlockSchedule() As Tuple(Of BlockScheduleC, List(Of BlockSchedule)) Implements IBlockScheduleAdminService.ListAllBlockSchedule
        Try
            Return _BlockScheduleRepository.ListAllBlockSchedule()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveBlockSchedule(ObjblockScheduleC As BlockScheduleC, ListBlockSchedule As List(Of BlockSchedule), audit As AuditMessage) As ActionResult(Of Tuple(Of BlockScheduleC, List(Of BlockSchedule))) Implements IBlockScheduleAdminService.SaveBlockSchedule

        If ListBlockSchedule Is Nothing Then
            Throw New ArgumentNullException("Banco vacio")
        End If
        Dim unitWork As IUnitWork = _BlockScheduleRepository.UnitWork
        Dim unitWorkBlockC As IUnitWork = _BlocScheduleCRepository.UnitWork
        Dim unitWorkDelete As IUnitWork = _BlockScheduleRepository.UnitWork
        Try

            Dim SaveListBlockSchedule As New List(Of BlockSchedule)

            If ListBlockSchedule IsNot Nothing And ListBlockSchedule.Count > 0 Then
                For Each ObjDeleteExistingBlockSchedule As BlockSchedule In ListBlockSchedule
                    If ObjDeleteExistingBlockSchedule.ChangeTracker.State <> ObjectState.Added Then
                        ObjDeleteExistingBlockSchedule.MarkAsDeleted()
                        _BlockScheduleRepository.SaveEntity(ObjDeleteExistingBlockSchedule)
                    Else
                        SaveListBlockSchedule.Add(ObjDeleteExistingBlockSchedule)
                    End If
                Next

                unitWorkDelete.Commit()

            End If

            For Each objBlockSchedule As BlockSchedule In SaveListBlockSchedule
                objBlockSchedule.CreationDate = Date.Now
                objBlockSchedule.CreationUser = audit.CodeUser

                _BlockScheduleRepository.SaveEntity(objBlockSchedule)
            Next

            ObjblockScheduleC.CreationDate = Date.Now
            ObjblockScheduleC.CreationUser = audit.CodeUser

            _BlocScheduleCRepository.SaveEntity(ObjblockScheduleC)

            unitWork.Commit()
            unitWorkBlockC.Commit()

            Dim ObjectResult = ListAllBlockSchedule()

            Return New ActionResult(Of Tuple(Of BlockScheduleC, List(Of BlockSchedule))) With {.StateResult = True, .ObjectEmbbeded = ObjectResult}

        Catch ex As OptimisticConcurrencyException
            unitWorkDelete.RollbackChanges()
            unitWork.RollbackChanges()
            unitWorkBlockC.RollbackChanges()
            Return New ActionResult(Of Tuple(Of BlockScheduleC, List(Of BlockSchedule))) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitWorkDelete.RollbackChanges()
            unitWork.RollbackChanges()
            unitWorkBlockC.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Tuple(Of BlockScheduleC, List(Of BlockSchedule))) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _BlockScheduleRepository = Nothing
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
