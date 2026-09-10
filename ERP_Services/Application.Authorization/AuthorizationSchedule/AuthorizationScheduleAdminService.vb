'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/10/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Text
Imports System.Transactions
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions

Public Class AuthorizationScheduleAdminService
    Implements IAuthorizationScheduleAdminService

    Private _authorizationScheduleRepository As IAuthorizationScheduleRepository

    Public Sub New(authorizationScheduleRepository As IAuthorizationScheduleRepository)
        If authorizationScheduleRepository Is Nothing Then
            Throw New ArgumentNullException("authorizationScheduleRepository")
        End If
        _authorizationScheduleRepository = authorizationScheduleRepository
    End Sub

    Public Function SaveAuthorizationSchedule(AuthorizationSchedule As AuthorizationSchedule, ListDays As List(Of Integer), audit As AuditMessage) As ActionResult(Of AuthorizationSchedule) Implements IAuthorizationScheduleAdminService.SaveAuthorizationSchedule
        If AuthorizationSchedule Is Nothing Then
            Throw New ArgumentNullException("AuthorizationSchedule")
        End If
        Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                Dim xml = ConvertEntityToXml(AuthorizationSchedule)

                Dim result = _authorizationScheduleRepository.SP_SaveAuthorizationSchedule(xml)
                If result.CodeResult <> 0 Then
                    scope.Dispose()
                    Return New ActionResult(Of AuthorizationSchedule) With {.StateResult = False, .Message = result.MessageResult}
                End If

                scope.Complete()
                Return New ActionResult(Of AuthorizationSchedule) With {.StateResult = True, .ObjectEmbbeded = AuthorizationSchedule, .Message = result.MessageResult}
            Catch ex As Exception
                scope.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of AuthorizationSchedule) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    Private Function ConvertEntityToXml(AuthorizationSchedule As AuthorizationSchedule) As String
        Dim builder As New StringBuilder

        builder.Append("<AuthorizationSchedule>")

        With AuthorizationSchedule
            builder.Append("<Id>" & .Id & "</Id>")
            builder.Append("<AuthorizationScheduleTemplateId>" & .AuthorizationScheduleTemplateId & "</AuthorizationScheduleTemplateId>")
            builder.Append("<Month>" & .Month & "</Month>")
            builder.Append("<Year>" & .Year & "</Year>")
            builder.Append("<UserId>" & .UserId & "</UserId>")
            builder.Append("<UserCode>" & .UserCode & "</UserCode>")
            builder.Append("<Status>" & .Status & "</Status>")

            Dim rowId As Integer = 1

            If .AuthorizationScheduleDetail IsNot Nothing AndAlso .AuthorizationScheduleDetail.Count > 0 Then
                For Each item In .AuthorizationScheduleDetail
                    builder.Append("<AuthorizationScheduleDetail>")
                    builder.Append("<RowId>" & rowId & "</RowId>")
                    builder.Append("<Id>" & item.Id & "</Id>")
                    builder.Append("<AuthorizationScheduleId>" & item.AuthorizationScheduleId & "</AuthorizationScheduleId>")
                    builder.Append("<Schedule>" & item.Schedule & "</Schedule>")
                    builder.Append("<Day>" & item.Day & "</Day>")
                    builder.Append("<NumberHour>" & item.NumberHour & "</NumberHour>")
                    builder.Append("<Status>" & item.Status & "</Status>")

                    If item.AuthorizationScheduleDetailHour IsNot Nothing AndAlso item.AuthorizationScheduleDetailHour.Count > 0 Then
                        For Each itemHour In item.AuthorizationScheduleDetailHour
                            builder.Append("<AuthorizationScheduleDetailHour>")
                            builder.Append("<RowId>" & rowId & "</RowId>")
                            builder.Append("<Id>" & itemHour.Id & "</Id>")
                            builder.Append("<AuthorizationScheduleDetailId>" & itemHour.AuthorizationScheduleDetailId & "</AuthorizationScheduleDetailId>")
                            builder.Append("<NextDay>" & itemHour.NextDay & "</NextDay>")
                            builder.Append("<InitialTime>" & itemHour.InitialTime.ToString() & "</InitialTime>")
                            builder.Append("<EndingTime>" & itemHour.EndingTime.ToString() & "</EndingTime>")
                            builder.Append("<NumberHour>" & itemHour.NumberHour & "</NumberHour>")
                            builder.Append("<Type>" & itemHour.Type & "</Type>")
                            builder.Append("<NoveltyType>" & itemHour.NoveltyType & "</NoveltyType>")
                            builder.Append("<Status>" & itemHour.Status & "</Status>")
                            builder.Append("</AuthorizationScheduleDetailHour>")
                        Next
                    End If

                    builder.Append("</AuthorizationScheduleDetail>")

                    rowId += 1
                Next
            End If
        End With

        builder.Append("</AuthorizationSchedule>")

        Return builder.ToString()
    End Function

    Public Function DeleteAuthorizationSchedule(AuthorizationSchedule As AuthorizationSchedule, TransactionalContainer As String, audit As AuditMessage) As ActionResult Implements IAuthorizationScheduleAdminService.DeleteAuthorizationSchedule
        If AuthorizationSchedule Is Nothing Then
            Throw New ArgumentNullException("AuthorizationSchedule")
        End If
        Using cnx As New System.Data.SqlClient.SqlConnection(Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, TransactionalContainer, False))
            cnx.Open()
            Dim tx As System.Data.SqlClient.SqlTransaction = cnx.BeginTransaction()
            Dim command As New System.Data.SqlClient.SqlCommand("", cnx, tx)
            command.CommandTimeout = 30000
                    command.CommandType = CommandType.Text

            Try
                'Se obtiene el id del día porque este método solo elimina el día con sus horas
                Dim AuthorizationScheduleDetailId As Integer = AuthorizationSchedule.AuthorizationScheduleDetail(0).Id

                'Se eliminan las horas que tenga asociado el día
                command.CommandText = "delete from [Authorization].AuthorizationScheduleDetailHour where AuthorizationScheduleDetailId = " + AuthorizationScheduleDetailId.ToString()
                command.ExecuteNonQuery()

                'Se elimina el día
                command.CommandText = "delete from [Authorization].AuthorizationScheduleDetail where Id = " + AuthorizationScheduleDetailId.ToString()
                command.ExecuteNonQuery()

                tx.Commit()
                Return New ActionResult With {.StateResult = True}
            Catch ex As OptimisticConcurrencyException
                tx.Rollback()
                Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
            Catch ex As UpdateException
                tx.Rollback()
                Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
            Catch ex As Exception
                tx.Rollback()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            Finally
                cnx.Close()
            End Try
        End Using
    End Function

    Public Function GetAuthorizationScheduleById(id As Integer) As ActionResult(Of AuthorizationSchedule) Implements IAuthorizationScheduleAdminService.GetAuthorizationScheduleById
        Try
            Dim authorization = _authorizationScheduleRepository.GetAuthorizationScheduleById(id)
            Return New ActionResult(Of AuthorizationSchedule) With {.StateResult = True, .ObjectEmbbeded = authorization}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of AuthorizationSchedule) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: elimine el estado administrado (objetos administrados).
            End If
            _authorizationScheduleRepository = Nothing
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
