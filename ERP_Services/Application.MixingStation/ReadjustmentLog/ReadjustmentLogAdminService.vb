'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Diego A. Roldan
' Created          : 2023-01-13
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions

Public Class ReadjustmentLogAdminService
    Implements IReadjustmentLogAdminService, Inject

    Private ReadOnly _readjustmentLogRepository As IReadjustmentLogRepository ' IRepository(Of ReadjustmentLog)

    Public Sub New(readjustmentLogRepository As IReadjustmentLogRepository)
        _readjustmentLogRepository = readjustmentLogRepository
    End Sub

    Public Function AddAdjustmentLog(requestMixingStationDetailId As Integer, requestPackageDetailStatusId As Integer, batchCode As String, audit As AuditMessage) As ActionResult Implements IReadjustmentLogAdminService.AddAdjustmentLog

        Try
            Dim newReadjustmentLog As New ReadjustmentLog With {
                .RequestMixingStationDetailId = requestMixingStationDetailId,
                .RequestPackageDetailStatusId = requestPackageDetailStatusId,
                .BatchCode = batchCode,
                .CreationDate = Date.Now,
                .CreationUser = "" 'audit.CodeUser
            }

            _readjustmentLogRepository.SaveEntity(newReadjustmentLog)
            _readjustmentLogRepository.UnitWork.Commit()

            Return New ActionResult With {.StateResult = True}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = ex.ToDetailString()}
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
