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

Public Class ConfigurationServicesAmbulatoryAdminService
    Implements IConfigurationServicesAmbulatoryAdminService

    Private _configurationServicesAmbulatoryRepository As IConfigurationServicesAmbulatoryRepository

    Public Sub New(configurationServicesAmbulatoryRepository As IConfigurationServicesAmbulatoryRepository)
        If configurationServicesAmbulatoryRepository Is Nothing Then
            Throw New ArgumentNullException("configurationServicesAmbulatoryRepository")
        End If
        _configurationServicesAmbulatoryRepository = configurationServicesAmbulatoryRepository
    End Sub

    Public Function SaveConfigurationServicesAmbulatory(ConfigurationServicesAmbulatory As ConfigurationServicesAmbulatory, ListPortfolioCUPSEntityIds As List(Of Integer), ListPortfolioInventoryProductIds As List(Of Integer), audit As AuditMessage) As ActionResult(Of ConfigurationServicesAmbulatory) Implements IConfigurationServicesAmbulatoryAdminService.SaveConfigurationServicesAmbulatory
        If ConfigurationServicesAmbulatory Is Nothing Then
            Throw New ArgumentNullException("ConfigurationServicesAmbulatory")
        End If
        Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                Dim xml = ConvertEntityToXml(ConfigurationServicesAmbulatory, ListPortfolioCUPSEntityIds, ListPortfolioInventoryProductIds)

                Dim result = _configurationServicesAmbulatoryRepository.SP_SaveConfigurationServicesAmbulatory(xml, audit.CodeUser)
                If result.CodeResult <> 0 Then
                    scope.Dispose()
                    Return New ActionResult(Of ConfigurationServicesAmbulatory) With {.StateResult = False, .Message = result.MessageResult}
                End If

                scope.Complete()
                Return New ActionResult(Of ConfigurationServicesAmbulatory) With {.StateResult = True, .ObjectEmbbeded = ConfigurationServicesAmbulatory, .Message = result.MessageResult}
            Catch ex As Exception
                scope.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of ConfigurationServicesAmbulatory) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    Private Function ConvertEntityToXml(ConfigurationServicesAmbulatory As ConfigurationServicesAmbulatory, ListPortfolioCUPSEntityIds As List(Of Integer), ListPortfolioInventoryProductIds As List(Of Integer)) As String
        Dim builder As New StringBuilder

        builder.Append("<ConfigurationServicesAmbulatory>")

        With ConfigurationServicesAmbulatory
            builder.Append("<Id>" & .Id & "</Id>")
            builder.Append("<Assignment>" & .Assignment & "</Assignment>")
            builder.Append("<AssignmentUnit>" & .AssignmentUnit & "</AssignmentUnit>")
            builder.Append("<Request>" & .Request & "</Request>")
            builder.Append("<RequestUnit>" & .RequestUnit & "</RequestUnit>")
            builder.Append("<Radicated>" & .Radicated & "</Radicated>")
            builder.Append("<RadicatedUnit>" & .RadicatedUnit & "</RadicatedUnit>")
            builder.Append("<DeliveryService>" & .DeliveryService & "</DeliveryService>")
            builder.Append("<DeliveryServiceUnit>" & .DeliveryServiceUnit & "</DeliveryServiceUnit>")

            If .ConfigurationServicesAmbulatoryExceptions IsNot Nothing AndAlso .ConfigurationServicesAmbulatoryExceptions.Count > 0 Then
                For Each item In .ConfigurationServicesAmbulatoryExceptions
                    builder.Append("<ConfigurationServicesAmbulatoryExceptions>")
                    builder.Append("<Id>" & item.Id & "</Id>")
                    builder.Append("<CareGroupId>" & item.CareGroupId & "</CareGroupId>")
                    builder.Append("<SusceptibleAuthorization>" & item.SusceptibleAuthorization & "</SusceptibleAuthorization>")
                    builder.Append("<Assignment>" & item.Assignment & "</Assignment>")
                    builder.Append("<AssignmentUnit>" & item.AssignmentUnit & "</AssignmentUnit>")
                    builder.Append("<Request>" & item.Request & "</Request>")
                    builder.Append("<RequestUnit>" & item.RequestUnit & "</RequestUnit>")
                    builder.Append("<Radicated>" & item.Radicated & "</Radicated>")
                    builder.Append("<RadicatedUnit>" & item.RadicatedUnit & "</RadicatedUnit>")
                    builder.Append("<DeliveryService>" & item.DeliveryService & "</DeliveryService>")
                    builder.Append("<DeliveryServiceUnit>" & item.DeliveryServiceUnit & "</DeliveryServiceUnit>")
                    builder.Append("<IsDelete>" & If(item.ChangeTracker.State = ObjectState.Deleted, 1, 0) & "</IsDelete>")
                    builder.Append("</ConfigurationServicesAmbulatoryExceptions>")
                Next
            End If

            Dim rowId As Integer = 1

            If ListPortfolioCUPSEntityIds IsNot Nothing AndAlso ListPortfolioCUPSEntityIds.Count > 0 Then
                For Each item In ListPortfolioCUPSEntityIds
                    builder.Append("<TableIds>")
                    builder.Append("<RowId>" & rowId & "</RowId>")
                    builder.Append("<AuthorizationPortfolioCUPSEntityId>" & item & "</AuthorizationPortfolioCUPSEntityId>")
                    builder.Append("<AuthorizationPortfolioInventoryProductId>" & "" & "</AuthorizationPortfolioInventoryProductId>")
                    builder.Append("</TableIds>")

                    rowId += 1
                Next
            End If

            rowId = 1

            If ListPortfolioInventoryProductIds IsNot Nothing AndAlso ListPortfolioInventoryProductIds.Count > 0 Then
                For Each item In ListPortfolioInventoryProductIds
                    builder.Append("<TableIds>")
                    builder.Append("<RowId>" & rowId & "</RowId>")
                    builder.Append("<AuthorizationPortfolioCUPSEntityId>" & "" & "</AuthorizationPortfolioCUPSEntityId>")
                    builder.Append("<AuthorizationPortfolioInventoryProductId>" & item & "</AuthorizationPortfolioInventoryProductId>")
                    builder.Append("</TableIds>")

                    rowId += 1
                Next
            End If
        End With

        builder.Append("</ConfigurationServicesAmbulatory>")

        Return builder.ToString()
    End Function

    Public Function DeleteConfigurationServicesAmbulatory(ListIds As List(Of Integer), TransactionalContainer As String, audit As AuditMessage) As ActionResult Implements IConfigurationServicesAmbulatoryAdminService.DeleteConfigurationServicesAmbulatory
        If ListIds Is Nothing Then
            Throw New ArgumentNullException("ListIds")
        End If
        Using cnx As New System.Data.SqlClient.SqlConnection(Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, TransactionalContainer, False))
            cnx.Open()
            Dim tx As System.Data.SqlClient.SqlTransaction = cnx.BeginTransaction()
            Dim command As New System.Data.SqlClient.SqlCommand("", cnx, tx)
            command.CommandTimeout = 30000
                    command.CommandType = CommandType.Text

            Try
                Dim stringIds As String = String.Join(",", ListIds.ToArray())

                command.CommandText = "delete from [Authorization].ConfigurationServicesAmbulatoryExceptions where ConfigurationServicesAmbulatoryId in (" + stringIds + ")"
                command.ExecuteNonQuery()

                command.CommandText = "delete from [Authorization].ConfigurationServicesAmbulatory where Id in (" + stringIds + ")"
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

    Public Function GetConfigurationServicesAmbulatoryById(id As Integer) As ActionResult(Of ConfigurationServicesAmbulatory) Implements IConfigurationServicesAmbulatoryAdminService.GetConfigurationServicesAmbulatoryById
        Try
            Dim ConfigurationServicesAmbulatory = _configurationServicesAmbulatoryRepository.GetConfigurationServicesAmbulatoryById(id)
            Return New ActionResult(Of ConfigurationServicesAmbulatory) With {.StateResult = True, .ObjectEmbbeded = ConfigurationServicesAmbulatory}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ConfigurationServicesAmbulatory) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
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
            _configurationServicesAmbulatoryRepository = Nothing
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
