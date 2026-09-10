'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/10/2014
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
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions
Imports Application.MixingStation
Imports System.Text
Imports System.Security

Public Class StabilityTableAdminService
    Implements IStabilityTableAdminService, Inject

    Private _stabilityTableRepository As IStabilityTableRepository

    Public Sub New(stabilityTableRepository As IStabilityTableRepository)
        If stabilityTableRepository Is Nothing Then
            Throw New ArgumentNullException("stabilityTableRepository vacío")
        End If
        _stabilityTableRepository = stabilityTableRepository
    End Sub

    Public Function SaveStabilityTable(StabilityTable As StabilityTable, audit As AuditMessage, operatingUnitId As Integer, Optional idSequense As Long = 0) As ActionResult(Of StabilityTable) Implements IStabilityTableAdminService.SaveStabilityTable
        If StabilityTable Is Nothing Then
            Throw New ArgumentNullException("StabilityTable")
        End If
        Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                Dim xml = ConvertEntityToXml(StabilityTable, operatingUnitId)

                Dim result = _stabilityTableRepository.SP_SaveStabilityTable(xml, audit.CodeUser)
                If result.CodeMessage <> 0 Then
                    scope.Dispose()
                    Return New ActionResult(Of StabilityTable) With {.StateResult = False, .Message = result.Message}
                End If

                StabilityTable.Id = result.Id
                StabilityTable.Code = result.Code

                scope.Complete()
                Return New ActionResult(Of StabilityTable) With {.StateResult = True, .ObjectEmbbeded = StabilityTable, .Message = result.Message}
            Catch ex As Exception
                scope.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of StabilityTable) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    Private Function ConvertEntityToXml(stabilityTable As StabilityTable, operatingUnitId As Integer) As Object
        Dim builder As New StringBuilder
        Dim RowId As Integer = 1

        builder.Append("<StabilityTable>")

        With stabilityTable
            builder.Append("<Id>" & .Id & "</Id>")
            builder.Append("<Code>" & .Code & "</Code>")
            builder.Append("<Name>" & SecurityElement.Escape(.Name) & "</Name>")
            builder.Append("<StabilityDate>" & .StabilityDate.ToString("dd/MM/yyyy HH:mm") & "</StabilityDate>")
            builder.Append("<Observations>" & SecurityElement.Escape(.Observations) & "</Observations>")
            builder.Append("<Status>" & .Status & "</Status>")
            builder.Append("<OperatingUnitId>" & operatingUnitId & "</OperatingUnitId>")

            If .StabilityTableDetail?.Any() Then
                For Each item In .StabilityTableDetail
                    builder.Append("<StabilityTableDetail>")
                    builder.Append("<RowId>" & RowId & "</RowId>")
                    builder.Append("<Id>" & item.Id & "</Id>")
                    builder.Append("<StabilityTableId>" & item.StabilityTableId & "</StabilityTableId>")
                    builder.Append("<ATCId>" & item.ATCId & "</ATCId>")
                    builder.Append("<ProductId>" & item.ProductId & "</ProductId>")
                    builder.Append("<HourStabilityProduct>" & item.HourStabilityProduct & "</HourStabilityProduct>")
                    builder.Append("<UnitDoseTypeId>" & item.UnitDoseTypeId & "</UnitDoseTypeId>")
                    builder.Append("<AllowableDoses>" & item.AllowableDoses & "</AllowableDoses>")
                    builder.Append("<Observations>" & SecurityElement.Escape(.Observations) & "</Observations>")
                    builder.Append("<IsDelete>" & If(item.ChangeTracker.State = ObjectState.Deleted, 1, 0) & "</IsDelete>")

                    If item.StabilityTableDetailDilution?.Any() Then
                        For Each itemDilution In item.StabilityTableDetailDilution
                            builder.Append("<StabilityTableDetailDilution>")
                            builder.Append("<RowId>" & RowId & "</RowId>")
                            builder.Append("<Id>" & itemDilution.Id & "</Id>")
                            builder.Append("<StabilityTableDetailId>" & itemDilution.StabilityTableDetailId & "</StabilityTableDetailId>")
                            builder.Append("<ATCId>" & itemDilution.ATCId & "</ATCId>")
                            builder.Append("<ConcentrationMaximum>" & itemDilution.ConcentrationMaximum & "</ConcentrationMaximum>")
                            builder.Append("<ConcentrationMinimum>" & itemDilution.ConcentrationMinimum & "</ConcentrationMinimum>")
                            builder.Append("<PhotoProtection>" & itemDilution.PhotoProtection & "</PhotoProtection>")
                            builder.Append("<InfusionTime>" & itemDilution.InfusionTime & "</InfusionTime>")
                            builder.Append("<BibliographicReference>" & itemDilution.BibliographicReference & "</BibliographicReference>")
                            builder.Append("<IsDelete>" & If(itemDilution.ChangeTracker.State = ObjectState.Deleted, 1, 0) & "</IsDelete>")
                            builder.Append("<Container>" & itemDilution.Container & "</Container>")
                            builder.Append("<HourStability>" & itemDilution.HourStability & "</HourStability>")
                            builder.Append("<StorageTemperatureId>" & itemDilution.StorageTemperatureId & "</StorageTemperatureId>")
                            builder.Append("</StabilityTableDetailDilution>")
                        Next
                    End If

                    If item.StabilityTableDetailReconstitution?.Any() Then
                        For Each itemReconstitution In item.StabilityTableDetailReconstitution
                            builder.Append("<StabilityTableDetailReconstitution>")
                            builder.Append("<RowId>" & RowId & "</RowId>")
                            builder.Append("<Id>" & itemReconstitution.Id & "</Id>")
                            builder.Append("<StabilityTableDetailId>" & itemReconstitution.StabilityTableDetailId & "</StabilityTableDetailId>")
                            builder.Append("<ATCId>" & itemReconstitution.ATCId & "</ATCId>")
                            builder.Append("<Volume>" & itemReconstitution.Volume & "</Volume>")
                            builder.Append("<HourStability>" & itemReconstitution.HourStability & "</HourStability>")
                            builder.Append("<Observations>" & itemReconstitution.Observations & "</Observations>")
                            builder.Append("<StorageTemperatureId>" & itemReconstitution.StorageTemperatureId & "</StorageTemperatureId>")
                            builder.Append("<IsDelete>" & If(itemReconstitution.ChangeTracker.State = ObjectState.Deleted, 1, 0) & "</IsDelete>")
                            builder.Append("</StabilityTableDetailReconstitution>")
                        Next
                    End If

                    If item.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("StabilityTableDetailDilution") Then
                        For Each itemDilution As StabilityTableDetailDilution In item.ChangeTracker.ObjectsRemovedFromCollectionProperties.Item("StabilityTableDetailDilution")
                            builder.Append("<StabilityTableDetailDilution>")
                            builder.Append("<RowId>" & RowId & "</RowId>")
                            builder.Append("<Id>" & itemDilution.Id & "</Id>")
                            builder.Append("<StabilityTableDetailId>" & itemDilution.StabilityTableDetailId & "</StabilityTableDetailId>")
                            builder.Append("<ATCId>" & itemDilution.ATCId & "</ATCId>")
                            builder.Append("<ConcentrationMaximum>" & itemDilution.ConcentrationMaximum & "</ConcentrationMaximum>")
                            builder.Append("<ConcentrationMinimum>" & itemDilution.ConcentrationMinimum & "</ConcentrationMinimum>")
                            builder.Append("<PhotoProtection>" & itemDilution.PhotoProtection & "</PhotoProtection>")
                            builder.Append("<InfusionTime>" & itemDilution.InfusionTime & "</InfusionTime>")
                            builder.Append("<BibliographicReference>" & itemDilution.BibliographicReference & "</BibliographicReference>")
                            builder.Append("<IsDelete>" & 1 & "</IsDelete>")
                            builder.Append("</StabilityTableDetailDilution>")
                        Next
                    End If

                    If item.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("StabilityTableDetailReconstitution") Then
                        For Each itemReconstitution As StabilityTableDetailReconstitution In item.ChangeTracker.ObjectsRemovedFromCollectionProperties.Item("StabilityTableDetailReconstitution")
                            builder.Append("<StabilityTableDetailReconstitution>")
                            builder.Append("<RowId>" & RowId & "</RowId>")
                            builder.Append("<Id>" & itemReconstitution.Id & "</Id>")
                            builder.Append("<StabilityTableDetailId>" & itemReconstitution.StabilityTableDetailId & "</StabilityTableDetailId>")
                            builder.Append("<ATCId>" & itemReconstitution.ATCId & "</ATCId>")
                            builder.Append("<Volume>" & itemReconstitution.Volume & "</Volume>")
                            builder.Append("<HourStability>" & itemReconstitution.HourStability & "</HourStability>")
                            builder.Append("<IsDelete>" & 1 & "</IsDelete>")
                            builder.Append("</StabilityTableDetailReconstitution>")
                        Next
                    End If

                    builder.Append("</StabilityTableDetail>")

                    RowId += 1
                Next
            End If
        End With

        builder.Append("</StabilityTable>")

        Return builder.ToString()
    End Function

    Public Function DeleteStabilityTable(StabilityTable As StabilityTable, audit As AuditMessage, TransactionalContainer As String) As ActionResult Implements IStabilityTableAdminService.DeleteStabilityTable
        If StabilityTable Is Nothing Then
            Throw New ArgumentNullException("StabilityTable")
        End If
        Using cnx As New System.Data.SqlClient.SqlConnection(Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, TransactionalContainer, False))
            cnx.Open()
            Dim tx As System.Data.SqlClient.SqlTransaction = cnx.BeginTransaction()
            Dim command As New System.Data.SqlClient.SqlCommand("", cnx, tx)
            command.CommandTimeout = 30000
            command.CommandType = CommandType.Text

            Try
                command.CommandText = "delete from MixingStation.StabilityTableDetailDilution where StabilityTableDetailId in (select Id from MixingStation.StabilityTableDetail where StabilityTableId = " + StabilityTable.Id.ToString() + ")"
                command.ExecuteNonQuery()

                command.CommandText = "delete from MixingStation.StabilityTableDetailReconstitution where StabilityTableDetailId in (select Id from MixingStation.StabilityTableDetail where StabilityTableId = " + StabilityTable.Id.ToString() + ")"
                command.ExecuteNonQuery()

                command.CommandText = "delete from MixingStation.StabilityTableDetail where StabilityTableId = " + StabilityTable.Id.ToString()
                command.ExecuteNonQuery()

                command.CommandText = "delete from MixingStation.StabilityTable where Id = " + StabilityTable.Id.ToString()
                command.ExecuteNonQuery()

                tx.Commit()
                Return New ActionResult With {.StateResult = True}
            Catch ex As OptimisticConcurrencyException
                tx.Rollback()
                Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            Catch ex As UpdateException
                tx.Rollback()
                Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            Catch ex As Exception
                tx.Rollback()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            Finally
                cnx.Close()
            End Try
        End Using
    End Function

    Public Function GetStabilityTable(code As String, audit As AuditMessage) As ActionResult(Of StabilityTable) Implements IStabilityTableAdminService.GetStabilityTable
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim StabilityTable As StabilityTable = Me._stabilityTableRepository.GetStabilityTable(code.Trim())
            If StabilityTable IsNot Nothing AndAlso StabilityTable.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of StabilityTable)(StabilityTable, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If

            Return New ActionResult(Of StabilityTable) With {.StateResult = True, .ObjectEmbbeded = StabilityTable}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of StabilityTable) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function GetStabilityTableById(id As Integer) As ActionResult(Of StabilityTable) Implements IStabilityTableAdminService.GetStabilityTableById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Try
            Dim StabilityTable As StabilityTable = Me._stabilityTableRepository.GetStabilityTableById(id)
            Return New ActionResult(Of StabilityTable) With {.StateResult = True, .ObjectEmbbeded = StabilityTable}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of StabilityTable) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function ChangeStateStabilityTable(code As String, state As Boolean, audit As AuditMessage, operatingUnitId As Integer) As ActionResult(Of StabilityTable) Implements IStabilityTableAdminService.ChangeStateStabilityTable
        Dim StabilityTable As StabilityTable = _stabilityTableRepository.GetStabilityTable(code)
        StabilityTable.Status = state
        Return SaveStabilityTable(StabilityTable, audit, operatingUnitId)
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: elimine el estado administrado (objetos administrados).
            End If
            _stabilityTableRepository = Nothing
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
