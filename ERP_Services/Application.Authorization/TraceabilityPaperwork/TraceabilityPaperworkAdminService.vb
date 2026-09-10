'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 02/06/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Text
Imports System.Transactions
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions

Public Class TraceabilityPaperworkAdminService
    Implements ITraceabilityPaperworkAdminService

#Region "Builder"

    Private _traceabilityPaperworkRepository As ITraceabilityPaperworkRepository

    Public Sub New(traceabilityPaperworkRepository As ITraceabilityPaperworkRepository)
        If traceabilityPaperworkRepository Is Nothing Then
            Throw New ArgumentNullException("traceabilityPaperworkRepository")
        End If
        _traceabilityPaperworkRepository = traceabilityPaperworkRepository
    End Sub

#End Region

#Region "Methods"

    Public Function GetTraceabilityPaperworkById(id As Integer) As ActionResult(Of TraceabilityPaperwork) Implements ITraceabilityPaperworkAdminService.GetTraceabilityPaperworkById
        Try
            Dim TraceabilityPaperwork = _traceabilityPaperworkRepository.GetTraceabilityPaperworkById(id)
            Return New ActionResult(Of TraceabilityPaperwork) With {.StateResult = True, .ObjectEmbbeded = TraceabilityPaperwork}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of TraceabilityPaperwork) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function AssignTraceabilityPaperwork(ListTraceabilityPaperwork As List(Of TraceabilityPaperwork), audit As AuditMessage) As ActionResult(Of List(Of TraceabilityPaperwork)) Implements ITraceabilityPaperworkAdminService.AssignTraceabilityPaperwork
        If ListTraceabilityPaperwork Is Nothing OrElse ListTraceabilityPaperwork.Count = 0 Then
            Throw New ArgumentNullException("ListTraceabilityPaperwork")
        End If
        Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                Dim xml = ConvertEntityToXml(ListTraceabilityPaperwork, audit)

                Dim result = _traceabilityPaperworkRepository.SP_AssignTraceabilityPaperwork(xml)
                If result.CodeResult <> 0 Then
                    scope.Dispose()
                    Return New ActionResult(Of List(Of TraceabilityPaperwork)) With {.StateResult = False, .Message = result.MessageResult}
                End If

                scope.Complete()
                Return New ActionResult(Of List(Of TraceabilityPaperwork)) With {.StateResult = result.FullAllocation, .ObjectEmbbeded = ListTraceabilityPaperwork, .Message = result.MessageResult}
            Catch ex As Exception
                scope.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of List(Of TraceabilityPaperwork)) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    Public Function SaveTraceabilityPaperwork(ListTraceabilityPaperwork As List(Of TraceabilityPaperwork), audit As AuditMessage) As ActionResult(Of List(Of TraceabilityPaperwork)) Implements ITraceabilityPaperworkAdminService.SaveTraceabilityPaperwork
        If ListTraceabilityPaperwork Is Nothing OrElse ListTraceabilityPaperwork.Count = 0 Then
            Throw New ArgumentNullException("ListTraceabilityPaperwork")
        End If
        Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                Dim xml = ConvertEntityToXml(ListTraceabilityPaperwork, audit)

                Dim result = _traceabilityPaperworkRepository.SP_SaveTraceabilityPaperwork(xml)
                If result.CodeResult <> 0 Then
                    scope.Dispose()
                    Return New ActionResult(Of List(Of TraceabilityPaperwork)) With {.StateResult = False, .Message = result.MessageResult}
                End If

                ListTraceabilityPaperwork(0).AnnexId = result.AnnexId
                ListTraceabilityPaperwork(0).AnnexesConsecutives = result.AnnexesConsecutives

                scope.Complete()
                Return New ActionResult(Of List(Of TraceabilityPaperwork)) With {.StateResult = True, .ObjectEmbbeded = ListTraceabilityPaperwork, .Message = result.MessageResult}
            Catch ex As Exception
                scope.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of List(Of TraceabilityPaperwork)) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    Public Function SP_SaveAcceptanceAuthorization(listTuple As List(Of Tuple(Of Integer, Integer, String, Integer)), audit As AuditMessage) As ActionResult Implements ITraceabilityPaperworkAdminService.SP_SaveAcceptanceAuthorization
        If listTuple Is Nothing OrElse listTuple.Count = 0 Then
            Throw New ArgumentNullException("listTuple")
        End If
        Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                Dim xml = ConvertAcceptanceToXml(listTuple, audit)

                Dim result = _traceabilityPaperworkRepository.SP_SaveAcceptanceAuthorization(xml)
                If result.CodeResult <> 0 Then
                    scope.Dispose()
                    Return New ActionResult With {.StateResult = False, .Message = result.MessageResult}
                End If

                scope.Complete()
                Return New ActionResult With {.StateResult = True, .Message = result.MessageResult}
            Catch ex As Exception
                scope.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    Public Function DeleteTraceabilityPaperwork(TraceabilityPaperwork As TraceabilityPaperwork, TransactionalContainer As String, audit As AuditMessage) As ActionResult Implements ITraceabilityPaperworkAdminService.DeleteTraceabilityPaperwork
        If TraceabilityPaperwork Is Nothing Then
            Throw New ArgumentNullException("TraceabilityPaperwork")
        End If
        Using cnx As New System.Data.SqlClient.SqlConnection(Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, TransactionalContainer, False))
            cnx.Open()
            Dim tx As System.Data.SqlClient.SqlTransaction = cnx.BeginTransaction()
            Dim command As New System.Data.SqlClient.SqlCommand("", cnx, tx)
                command.CommandTimeout = 30000
                    command.CommandType = CommandType.Text

            Try


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

#End Region

#Region "Private Methods"

    Private Function ConvertEntityToXml(ListTraceabilityPaperwork As List(Of TraceabilityPaperwork), audit As AuditMessage) As String
        Dim builder As New StringBuilder

        Dim rowHeaderId As Integer = 1
        Dim rowId As Integer = 1
        Dim rowId2 As Integer = 1

        For Each TraceabilityPaperwork In ListTraceabilityPaperwork
            builder.Append("<TraceabilityPaperwork>")

            With TraceabilityPaperwork
                builder.Append("<RowHeaderId>" & rowHeaderId & "</RowHeaderId>")
                builder.Append("<Id>" & .Id & "</Id>")
                builder.Append("<AdmissionNumber>" & .AdmissionNumber & "</AdmissionNumber>")
                builder.Append("<Folio>" & .Folio & "</Folio>")
                builder.Append("<ServiceCode>" & .ServiceCode & "</ServiceCode>")
                builder.Append("<Type>" & .Type & "</Type>")
                builder.Append("<PatientCode>" & .PatientCode & "</PatientCode>")
                builder.Append("<CareCenterCode>" & .CareCenterCode & "</CareCenterCode>")
                builder.Append("<RequestDate>" & .RequestDate.ToString("dd/MM/yyyy HH:mm") & "</RequestDate>")
                builder.Append("<RequestQuantity>" & .RequestQuantity & "</RequestQuantity>")
                builder.Append("<FunctionalUnitCode>" & .FunctionalUnitCode & "</FunctionalUnitCode>")
                builder.Append("<EntityId>" & .EntityId & "</EntityId>")
                builder.Append("<EntityName>" & .EntityName & "</EntityName>")
                builder.Append("<PreviousStatus>" & .PreviousStatus & "</PreviousStatus>")
                builder.Append("<Status>" & .Status & "</Status>")
                builder.Append("<CancellationReasonsId>" & .CancellationReasonsId & "</CancellationReasonsId>")
                builder.Append("<CancellationReasonsObservations>" & .CancellationReasonsObservations & "</CancellationReasonsObservations>")
                builder.Append("<CancellationUserCode>" & .CancellationUserCode & "</CancellationUserCode>")
                builder.Append("<AssignUserCode>" & .AssignUserCode & "</AssignUserCode>")
                builder.Append("<AuthorizationSourceId>" & .AuthorizationSourceId & "</AuthorizationSourceId>")
                builder.Append("<IsManual>" & .IsManual & "</IsManual>")
                builder.Append("<CareGroupId>" & .CareGroupId & "</CareGroupId>")
                builder.Append("<HealthAdministratorId>" & .HealthAdministratorId & "</HealthAdministratorId>")
                builder.Append("<ProfessionalCode>" & .ProfessionalCode & "</ProfessionalCode>")
                builder.Append("<CareCenterTargetCode>" & .CareCenterTargetCode & "</CareCenterTargetCode>")
                builder.Append("<FunctionalUnitTargetId>" & .FunctionalUnitTargetId & "</FunctionalUnitTargetId>")
                builder.Append("<ServiceId>" & .ServiceId & "</ServiceId>")
                If .ContractDescriptionId IsNot Nothing Then
                    builder.Append("<ContractDescriptionId>" & .ContractDescriptionId & "</ContractDescriptionId>")
                End If
                builder.Append("<Order>" & .Order & "</Order>")
                builder.Append("<TraceabilityPaperworkPostponementReasonsId>" & .TraceabilityPaperworkPostponementReasonsId & "</TraceabilityPaperworkPostponementReasonsId>")

                If .TraceabilityPaperworkEvents IsNot Nothing AndAlso .TraceabilityPaperworkEvents.Count > 0 Then
                    For Each item In .TraceabilityPaperworkEvents
                        builder.Append("<TraceabilityPaperworkEvents>")
                        builder.Append("<RowHeaderId>" & rowHeaderId & "</RowHeaderId>")
                        builder.Append("<RowId>" & rowId & "</RowId>")
                        builder.Append("<Id>" & item.Id & "</Id>")
                        builder.Append("<TraceabilityPaperworkId>" & item.TraceabilityPaperworkId & "</TraceabilityPaperworkId>")
                        builder.Append("<TraceabilityPaperworkAnnexesId>" & item.TraceabilityPaperworkAnnexesId & "</TraceabilityPaperworkAnnexesId>")
                        builder.Append("<HealthAdministratorId>" & item.HealthAdministratorId & "</HealthAdministratorId>")
                        builder.Append("<ReportType>" & item.ReportType & "</ReportType>")
                        builder.Append("<Instructions>" & item.Instructions & "</Instructions>")
                        builder.Append("<Status>" & item.Status & "</Status>")
                        builder.Append("<AuthorizationNumber>" & item.AuthorizationNumber & "</AuthorizationNumber>")
                        builder.Append("<AuthorizedQuantity>" & item.AuthorizedQuantity & "</AuthorizedQuantity>")
                        builder.Append("<Observations>" & item.Observations & "</Observations>")
                        builder.Append("<PatientNotificated>" & item.PatientNotificated & "</PatientNotificated>")
                        builder.Append("<InformationPatient>" & item.InformationPatient & "</InformationPatient>")
                        builder.Append("<PhoneNumber>" & item.PhoneNumber & "</PhoneNumber>")
                        builder.Append("<Extension>" & item.Extension & "</Extension>")
                        builder.Append("<InitialTime>" & If(item.InitialTime IsNot Nothing, item.InitialTime.ToString(), "") & "</InitialTime>")
                        builder.Append("<EndTime>" & If(item.InitialTime IsNot Nothing, item.EndTime.ToString(), "") & "</EndTime>")
                        builder.Append("<ContactPerson>" & item.ContactPerson & "</ContactPerson>")
                        builder.Append("<Charge>" & item.Charge & "</Charge>")
                        builder.Append("<RadicateNumber>" & item.RadicateNumber & "</RadicateNumber>")
                        builder.Append("<SendType>" & item.SendType & "</SendType>")
                        builder.Append("<ReceivedDate>" & If(item.ReceivedDate IsNot Nothing, item.ReceivedDate.Value.ToString("dd/MM/yyyy HH:mm"), "") & "</ReceivedDate>")
                        builder.Append("<ReceivePerson>" & item.ReceivePerson & "</ReceivePerson>")
                        builder.Append("<URL>" & item.URL & "</URL>")
                        builder.Append("<RegistrationDate>" & If(item.RegistrationDate IsNot Nothing, item.RegistrationDate.Value.ToString("dd/MM/yyyy HH:mm"), "") & "</RegistrationDate>")
                        builder.Append("<Email>" & item.Email & "</Email>")
                        builder.Append("<SendDate>" & If(item.SendDate IsNot Nothing, item.SendDate.Value.ToString("dd/MM/yyyy HH:mm"), "") & "</SendDate>")
                        builder.Append("<CreationUser>" & audit.CodeUser & "</CreationUser>")
                        builder.Append("<AuthorizedBy>" & item.AuthorizedBy & "</AuthorizedBy>")
                        builder.Append("<AuthorizationDate>" & If(item.AuthorizationDate IsNot Nothing, item.AuthorizationDate.Value.ToString("dd/MM/yyyy HH:mm"), "") & "</AuthorizationDate>")
                        builder.Append("<AuthorizationExpiredDate>" & If(item.AuthorizationExpiredDate IsNot Nothing, item.AuthorizationExpiredDate.Value.ToString("dd/MM/yyyy HH:mm"), "") & "</AuthorizationExpiredDate>")

                        If item.ListAttachment IsNot Nothing AndAlso item.ListAttachment.Count > 0 Then
                            For Each itemAttachment In item.ListAttachment
                                builder.Append("<Attachment>")
                                builder.Append("<RowId>" & rowId & "</RowId>")
                                builder.Append("<Name>" & itemAttachment.Name & "</Name>")
                                builder.Append("<Extension>" & itemAttachment.Extension & "</Extension>")
                                builder.Append("<Description>" & itemAttachment.Description & "</Description>")
                                builder.Append("<FileAttached>" & Convert.ToBase64String(itemAttachment.FileAttached) & "</FileAttached>")
                                builder.Append("<CreationUser>" & audit.CodeUser & "</CreationUser>")
                                builder.Append("</Attachment>")
                            Next
                        End If

                        builder.Append("</TraceabilityPaperworkEvents>")

                        rowId += 1
                    Next
                End If

                If .TraceabilityPaperworkAnnexes IsNot Nothing AndAlso .TraceabilityPaperworkAnnexes.Count > 0 Then
                    For Each item In .TraceabilityPaperworkAnnexes
                        builder.Append("<TraceabilityPaperworkAnnexes>")
                        builder.Append("<RowHeaderId>" & rowHeaderId & "</RowHeaderId>")
                        builder.Append("<RowId>" & rowId2 & "</RowId>")
                        builder.Append("<Id>" & item.Id & "</Id>")
                        builder.Append("<TraceabilityPaperworkId>" & item.TraceabilityPaperworkId & "</TraceabilityPaperworkId>")
                        builder.Append("<HealthAdministratorId>" & item.HealthAdministratorId & "</HealthAdministratorId>")
                        builder.Append("<TypeRequestServices>" & item.TypeRequestServices & "</TypeRequestServices>")
                        builder.Append("<PriorityAttention>" & item.PriorityAttention & "</PriorityAttention>")
                        builder.Append("<Justification>" & item.Justification & "</Justification>")
                        builder.Append("<Folio>" & item.Folio & "</Folio>")
                        builder.Append("<CreationUser>" & audit.CodeUser & "</CreationUser>")
                        builder.Append("<GenerateConsecutiveWithMultipleService>" & item.GenerateConsecutiveWithMultipleService & "</GenerateConsecutiveWithMultipleService>")
                        builder.Append("<DiagnosticCode>" & item.DiagnosticCode & "</DiagnosticCode>")
                        builder.Append("</TraceabilityPaperworkAnnexes>")

                        rowId2 += 1
                    Next
                End If

                If .TraceabilityPaperworkAlert IsNot Nothing AndAlso .TraceabilityPaperworkAlert.Count > 0 Then
                    For Each item In .TraceabilityPaperworkAlert
                        builder.Append("<TraceabilityPaperworkAlert>")
                        builder.Append("<RowHeaderId>" & rowHeaderId & "</RowHeaderId>")
                        builder.Append("<Id>" & item.Id & "</Id>")
                        builder.Append("<TraceabilityPaperworkId>" & item.TraceabilityPaperworkId & "</TraceabilityPaperworkId>")
                        builder.Append("<Comments>" & item.Comments & "</Comments>")
                        builder.Append("<Status>" & item.Status & "</Status>")
                        builder.Append("<CreationUser>" & audit.CodeUser & "</CreationUser>")
                        builder.Append("<ModificationUser>" & audit.CodeUser & "</ModificationUser>")
                        builder.Append("</TraceabilityPaperworkAlert>")
                    Next
                End If

                If .TraceabilityPaperworkPostponementReasons IsNot Nothing AndAlso .TraceabilityPaperworkPostponementReasons.Count > 0 Then
                    For Each item In .TraceabilityPaperworkPostponementReasons
                        builder.Append("<TraceabilityPaperworkPostponementReasons>")
                        builder.Append("<RowHeaderId>" & rowHeaderId & "</RowHeaderId>")
                        builder.Append("<Id>" & item.Id & "</Id>")
                        builder.Append("<TraceabilityPaperworkId>" & item.TraceabilityPaperworkId & "</TraceabilityPaperworkId>")
                        builder.Append("<PostponementReasonsId>" & item.PostponementReasonsId & "</PostponementReasonsId>")
                        builder.Append("<PostponementDate>" & item.PostponementDate.ToString("dd/MM/yyyy HH:mm") & "</PostponementDate>")
                        builder.Append("<PostponementObservations>" & item.PostponementObservations & "</PostponementObservations>")
                        builder.Append("<StatusPrevious>" & item.StatusPrevious & "</StatusPrevious>")
                        builder.Append("<Status>" & item.Status & "</Status>")
                        builder.Append("<CreationUser>" & item.CreationUser & "</CreationUser>")
                        builder.Append("<ModificationUser>" & item.ModificationUser & "</ModificationUser>")
                        builder.Append("</TraceabilityPaperworkPostponementReasons>")
                    Next
                End If
            End With

            builder.Append("</TraceabilityPaperwork>")

            rowHeaderId += 1
        Next

        Return builder.ToString()
    End Function



    Private Function ConvertAcceptanceToXml(listTuple As List(Of Tuple(Of Integer, Integer, String, Integer)), audit As AuditMessage) As String
        Dim builder As New StringBuilder

        For Each item In listTuple
            builder.Append("<TableAcceptance>")
            builder.Append("<TraceabilityPaperworkId>" & item.Item1 & "</TraceabilityPaperworkId>")
            builder.Append("<AcceptanceStatus>" & item.Item2 & "</AcceptanceStatus>")
            builder.Append("<RejectionObservations>" & item.Item3 & "</RejectionObservations>")
            builder.Append("<AuthorizationRejectionId>" & item.Item4 & "</AuthorizationRejectionId>")
            builder.Append("<RejectionUserCode>" & audit.CodeUser & "</RejectionUserCode>")
            builder.Append("</TableAcceptance>")
        Next

        Return builder.ToString()
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: elimine el estado administrado (objetos administrados).
            End If
            _traceabilityPaperworkRepository = Nothing
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
