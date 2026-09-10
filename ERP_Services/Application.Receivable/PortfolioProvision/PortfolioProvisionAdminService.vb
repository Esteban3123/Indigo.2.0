'***********************************************************************
' Assembly         : Application.Portfolio
' Author           : Carlos Mario Arias Rubiano
' Created          : 21/10/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "imports"
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports System.Transactions
Imports System.Data.Entity.Validation
Imports Infrastructure.CrossCutting.Resources
Imports System.Text
Imports System.Data.SqlClient
Imports Domain.Portfolio.Model

#End Region

Public Class PortfolioProvisionAdminService
    Implements IPortfolioProvisionAdminService

#Region "Fields"

    Dim _portfolioProvisionRepository As IPortfolioProvisionRepository

    Dim _sequenceDRepository As ISequensePortfolioDRepository

#End Region

#Region "Builder"

    Public Sub New(ByVal portfolioProvisionRepository As IPortfolioProvisionRepository, sequenceRepository As ISequensePortfolioDRepository)
        If portfolioProvisionRepository Is Nothing Then
            Throw New ArgumentNullException("portfolioProvisionRepository")
        End If
        If sequenceRepository Is Nothing Then
            Throw New ArgumentNullException("sequenceRepository")
        End If
        _portfolioProvisionRepository = portfolioProvisionRepository
        _sequenceDRepository = sequenceRepository

    End Sub

#End Region

#Region "Methods"

    Public Function CopyAndPastePortfolioProvision(Data As List(Of List(Of String)), CourtDate As Date, Process As Integer, OperatingUnitId As Integer, applyDeterioration As Byte, Percentage As Decimal, Expectative As Integer) As ActionResult(Of List(Of PortfolioProvisionDetail), List(Of Tuple(Of String, Integer))) Implements IPortfolioProvisionAdminService.CopyAndPastePortfolioProvision
        If Data Is Nothing OrElse Data.Count = 0 Then
            Throw New ArgumentNullException("Data")
        End If
        'Listado que se devuelve para pegar a la rejilla del form
        Dim ListPortfolioProvisionDetail As New List(Of PortfolioProvisionDetail)
        'Listado de errores
        Dim listErrors As New List(Of Tuple(Of String, Integer))
        Try
            'Objeto xml
            Dim xmlObject = ConvertToXmlCopyPaste(Data)
            'Se consume el procedimiento almacenado
            Dim resultStore = _portfolioProvisionRepository.SP_CopyAndPastePortfolioProvision(xmlObject, CourtDate, Process, OperatingUnitId, applyDeterioration)

            'Se crean los objetos para devolver y pegar en la rejilla
            If resultStore IsNot Nothing AndAlso resultStore.Count > 0 Then
                For Each itemXml In resultStore
                    If itemXml.StatusField = 1 Then 'Si el estado del item es True y pasó todas las validaciones
                        If ListPortfolioProvisionDetail IsNot Nothing AndAlso ListPortfolioProvisionDetail.Count > 0 Then
                            Dim itemAddeed = ListPortfolioProvisionDetail.Find(Function(x) x.InvoiceNumber = itemXml.InvoiceNumber)
                            If itemAddeed IsNot Nothing Then
                                Continue For
                            End If
                        End If

                        'Se crea el nuevo objeto para agregarlo al listado
                        Dim PortfolioProvisionDetail As New PortfolioProvisionDetail
                        With PortfolioProvisionDetail
                            .SelectOption = True
                            .ConfirmDateAccountReceivable = itemXml.ConfirmDate
                            .AccountReceivableId = itemXml.AccountReceivableId
                            .InvoiceNumber = itemXml.InvoiceNumber
                            .Days = itemXml.Days
                            .AgesId = itemXml.AgesId
                            .AgesDescription = itemXml.AgesDescription
                            .FacturerValue = itemXml.InvoiceValue
                            .BalanceAccountReceivable = itemXml.Balance

                            If Process = 2 AndAlso (applyDeterioration = 1 OrElse applyDeterioration = 2) Then
                                .ValueGlosado = itemXml.ValueGlosado
                                .BalanceWithGlosa = .BalanceAccountReceivable - .ValueGlosado
                                .Expectative = IIf(Expectative = 0, itemXml.Expectative, Expectative)
                                .Percentage = IIf(Percentage = 0, itemXml.Percentage, Percentage)
                                .NetPresentValue = .BalanceAccountReceivable / Math.Pow((1 + .Percentage / 100), .Expectative)
                                .Value = .BalanceAccountReceivable - .NetPresentValue
                                .AccumulatedDeterioration = itemXml.DeteriorationBalance
                                .DeteriorationCalculated = .Value - .AccumulatedDeterioration
                            Else
                                .Percentage = itemXml.Percentage
                                .NetPresentValue = 0
                                .Expectative = 0
                                .Value = Math.Round(.BalanceAccountReceivable * .Percentage / 100, 2)
                            End If

                            .RegimenName = itemXml.RegimenName
                            .ThirdPartyNitName = itemXml.ThirdPartyNitName
                        End With

                        ListPortfolioProvisionDetail.Add(PortfolioProvisionDetail)
                    Else 'Si el estado del item es False y no pasó alguna validación
                        listErrors.Add(New Tuple(Of String, Integer)(itemXml.MessageField, 2))
                    End If
                Next
            End If


            'Se devuelve el mensaje
            Return New ActionResult(Of List(Of PortfolioProvisionDetail), List(Of Tuple(Of String, Integer))) With {.ObjectEmbbeded = ListPortfolioProvisionDetail, .ObjectEmbbededAux = listErrors, .StateResult = True}
        Catch ex As SqlException
            If ex.ErrorCode = -2146232060 Then
                Return New ActionResult(Of List(Of PortfolioProvisionDetail), List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = "Los valores contienen decimales con un formato no valido, por favor corrija para poder continuar"}
            Else
                Return New ActionResult(Of List(Of PortfolioProvisionDetail), List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = ex.ToString}
            End If
        Catch ex As Exception
            Return New ActionResult(Of List(Of PortfolioProvisionDetail), List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = ex.ToString}
        End Try
    End Function

    Public Function GetPortfolioProvision(code As String, audit As AuditMessage) As ActionResult(Of PortfolioProvision) Implements IPortfolioProvisionAdminService.GetPortfolioProvision
        If code Is Nothing Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim PortfolioProvision As PortfolioProvision = Me._portfolioProvisionRepository.GetPortfolioProvision(code.Trim())
            If PortfolioProvision IsNot Nothing AndAlso PortfolioProvision.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of PortfolioProvision)(PortfolioProvision, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of PortfolioProvision) With {.StateResult = True, .ObjectEmbbeded = PortfolioProvision}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of PortfolioProvision) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    Public Function GetPortfolioProvisionById(Id As Integer, audit As AuditMessage) As ActionResult(Of PortfolioProvision) Implements IPortfolioProvisionAdminService.GetPortfolioProvisionById
        If Id = Nothing Then
            Throw New ArgumentNullException("Id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim PortfolioProvision As PortfolioProvision = Me._portfolioProvisionRepository.GetPortfolioProvisionById(Id)
            If PortfolioProvision IsNot Nothing AndAlso PortfolioProvision.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of PortfolioProvision)(PortfolioProvision, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of PortfolioProvision) With {.StateResult = True, .ObjectEmbbeded = PortfolioProvision}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of PortfolioProvision) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    Public Function SavePortfolioProvision(PortfolioProvision As PortfolioProvision, listPortfolioProvisionDetailDelete As List(Of Integer), audit As AuditMessage) As ActionResult(Of PortfolioProvision) Implements IPortfolioProvisionAdminService.SavePortfolioProvision
        If PortfolioProvision Is Nothing Then
            Throw New ArgumentNullException("PortfolioProvision")
        End If

        Dim unitOfWork As IUnitWork = Me._portfolioProvisionRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim EntityXml As String = ConvertToXmlPortfolioProvision(PortfolioProvision)
                Dim ListDeleteXml As String = ConvertToXmlListDelete(listPortfolioProvisionDetailDelete)
                Dim auditStatus As Infrastructure.CrossCutting.Audit.Actions = Utils.GetAuditStatus(PortfolioProvision.Id, PortfolioProvision.Status)

                Dim resultStore = _portfolioProvisionRepository.SP_SaveProvisionAndDeterioration(EntityXml, ListDeleteXml, audit.CodeUser)
                If resultStore.CodeMessage <> 0 Then
                    unitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of PortfolioProvision) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = resultStore.Message}
                End If

                PortfolioProvision.Id = resultStore.Id
                PortfolioProvision.Code = resultStore.Code

                Dim auditProcess = New IndigoAuditSimpleEntity(Of PortfolioProvision)(PortfolioProvision, audit, auditStatus, PortfolioProvision.OriginalValue)
                auditProcess.Execute()

                PortfolioProvision.MarkAsUnchanged()
                transaction.Complete()
                Return New ActionResult(Of PortfolioProvision) With {.StateResult = True, .ObjectEmbbeded = PortfolioProvision}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                Return New ActionResult(Of PortfolioProvision) With {.StateResult = False, .Message = "-999"}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of PortfolioProvision) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    Public Function AnnularPortfolioProvision(PortfolioProvision As PortfolioProvision, audit As AuditMessage) As ActionResult Implements IPortfolioProvisionAdminService.AnnularPortfolioProvision

    End Function

    Public Function ConfirmPortfolioProvision(PortfolioProvision As PortfolioProvision, audit As AuditMessage, Optional operativeUnitId As Integer? = Nothing) As ActionResult(Of PortfolioProvision) Implements IPortfolioProvisionAdminService.ConfirmPortfolioProvision
        If PortfolioProvision Is Nothing Then
            Throw New ArgumentNullException("PortfolioProvision")
        End If

        Dim unitOfWork As IUnitWork = Me._portfolioProvisionRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim auditStatus As Infrastructure.CrossCutting.Audit.Actions = Utils.GetAuditStatus(PortfolioProvision.Id, 2)

                Dim resultStoreConfirm = _portfolioProvisionRepository.SP_ConfirmPortfolioProvision(PortfolioProvision.Id, audit.CodeUser, operativeUnitId)
                If resultStoreConfirm.CodeMessage <> 0 Then
                    transaction.Dispose()
                    Return New ActionResult(Of PortfolioProvision) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = resultStoreConfirm.Message}
                End If

                Dim auditProcess = New IndigoAuditSimpleEntity(Of PortfolioProvision)(PortfolioProvision, audit, auditStatus, PortfolioProvision.OriginalValue)
                auditProcess.Execute()

                transaction.Complete()
                Return New ActionResult(Of PortfolioProvision) With {.StateResult = True, .Message = resultStoreConfirm.Message}
            Catch ex As Exception
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of PortfolioProvision) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    Public Function SaveAndConfirmPortfolioProvision(PortfolioProvision As PortfolioProvision, listPortfolioProvisionDetailDelete As List(Of Integer), audit As AuditMessage, Optional operativeUnitId As Integer? = Nothing) As ActionResult(Of PortfolioProvision) Implements IPortfolioProvisionAdminService.SaveAndConfirmPortfolioProvision
        If PortfolioProvision Is Nothing Then
            Throw New ArgumentNullException("PortfolioProvision")
        End If

        Dim result = SavePortfolioProvision(PortfolioProvision, listPortfolioProvisionDetailDelete, audit)
        If result.StateResult Then
            Dim resultConfirm = ConfirmPortfolioProvision(result.ObjectEmbbeded, audit, operativeUnitId)
            Return New ActionResult(Of PortfolioProvision) With {.StateResult = True, .StateResultAux = resultConfirm.StateResult, .Message = String.Format("Se guardó correctamente el registro con código {0} {1} {2}", PortfolioProvision.Code, vbCrLf, IIf(resultConfirm.StateResult, "", "No se pudo confirmar por:") & vbCrLf & resultConfirm.Message)}
        Else
            Return New ActionResult(Of PortfolioProvision) With {.StateResult = False, .StateResultAux = False, .Message = result.Message}
        End If
    End Function

    ''' <summary>
    ''' Obtiene la información del Deterioro de Cartera de acuerdo a la Clasificación
    ''' </summary>
    ''' <param name="closingDate"></param>
    ''' <param name="operativeUnitId"></param>
    ''' <returns></returns>
    Public Function GetPortfolioDeteriorationByClassification(closingDate As Date, operativeUnitId As Integer) As ActionResult(Of List(Of PortfolioDeteriorationByClassificationDTO)) Implements IPortfolioProvisionAdminService.GetPortfolioDeteriorationByClassification
        Try
            Dim result = _portfolioProvisionRepository.SP_GetPortfolioDeteriorationByClassification(closingDate, operativeUnitId)
            Return New ActionResult(Of List(Of PortfolioDeteriorationByClassificationDTO)) With {.ObjectEmbbeded = result, .StateResult = True, .StatusCode = eStatusResult.SUCCESS}
        Catch ex As Exception
            Return New ActionResult(Of List(Of PortfolioDeteriorationByClassificationDTO)) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = ex.Message}
        End Try
    End Function

#End Region

#Region "Private Methods"

    Private Function ConvertToXmlCopyPaste(data As List(Of List(Of String)))
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<Data>")

        For Each item In data
            builder.Append("<Row>")

            builder.Append("<CountFields>" & item.Count & "</CountFields>")
            builder.Append("<StatusField>" & 0 & "</StatusField>")
            builder.Append("<MessageField>" & "Ok" & "</MessageField>")
            builder.Append("<ConfirmDate>" & DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") & "</ConfirmDate>")
            builder.Append("<InvoiceNumber>" & item(0) & "</InvoiceNumber>")
            builder.Append("<Expectative>" & If(item.Count > 1, item(1), 0) & "</Expectative>")
            builder.Append("<Percentage>" & If(item.Count > 2, item(2), 0) & "</Percentage>")

            builder.Append("</Row>")
        Next

        builder.Append("</Data>")
        Return builder.ToString
    End Function

    Private Function ConvertToXmlPortfolioProvision(PortfolioProvision As PortfolioProvision) As String
        Dim builder As StringBuilder = New StringBuilder()

        builder.Append("<PortfolioProvision>")

        'Se arma la cabecera
        builder.Append("<Id>" & PortfolioProvision.Id & "</Id>")
        builder.Append("<Code>" & PortfolioProvision.Code & "</Code>")
        builder.Append("<DocumentDate>" & PortfolioProvision.DocumentDate.ToString("dd/MM/yyyy") & "</DocumentDate>")
        builder.Append("<CourtDate>" & PortfolioProvision.CourtDate.ToString("dd/MM/yyyy") & "</CourtDate>")
        builder.Append("<DocumentType>" & PortfolioProvision.DocumentType & "</DocumentType>")
        builder.Append("<Description>" & PortfolioProvision.Description & "</Description>")
        builder.Append("<OperatingUnitId>" & PortfolioProvision.OperatingUnitId & "</OperatingUnitId>")
        builder.Append("<Status>" & PortfolioProvision.Status & "</Status>")

        If PortfolioProvision.ApplyDeterioration IsNot Nothing Then
            builder.Append("<ApplyDeterioration>" & PortfolioProvision.ApplyDeterioration & "</ApplyDeterioration>")
        Else
            builder.Append("<ApplyDeterioration>" & 0 & "</ApplyDeterioration>")
        End If

        'Se arma el detalle
        If PortfolioProvision.PortfolioProvisionDetail IsNot Nothing AndAlso PortfolioProvision.PortfolioProvisionDetail.Count > 0 Then
            For Each detail In PortfolioProvision.PortfolioProvisionDetail
                builder.Append("<PortfolioProvisionDetail>")

                builder.Append("<Id>" & detail.Id & "</Id>")
                builder.Append("<PortfolioProvisionId>" & detail.PortfolioProvisionId & "</PortfolioProvisionId>")
                builder.Append("<ConfirmDateAccountReceivable>" & detail.ConfirmDateAccountReceivable.ToString("dd/MM/yyyy HH:mm:ss") & "</ConfirmDateAccountReceivable>")
                builder.Append("<AccountReceivableId>" & detail.AccountReceivableId & "</AccountReceivableId>")
                builder.Append("<InvoiceNumber>" & detail.InvoiceNumber & "</InvoiceNumber>")
                builder.Append("<Days>" & detail.Days & "</Days>")
                If detail.AgesId IsNot Nothing Then
                    builder.Append("<AgesId>" & detail.AgesId & "</AgesId>")
                End If
                builder.Append("<FacturerValue>" & detail.FacturerValue.ToString.Replace(",", ".") & "</FacturerValue>")
                builder.Append("<BalanceAccountReceivable>" & detail.BalanceAccountReceivable.ToString.Replace(",", ".") & "</BalanceAccountReceivable>")
                builder.Append("<ValueGlosado>" & detail.ValueGlosado.ToString.Replace(",", ".") & "</ValueGlosado>")
                builder.Append("<Expectative>" & detail.Expectative & "</Expectative>")
                builder.Append("<Percentage>" & detail.Percentage.ToString.Replace(",", ".") & "</Percentage>")
                builder.Append("<NetPresentValue>" & detail.NetPresentValue.ToString.Replace(",", ".") & "</NetPresentValue>")
                builder.Append("<Value>" & detail.Value.ToString.Replace(",", ".") & "</Value>")
                builder.Append("<AccumulatedDeterioration>" & detail.AccumulatedDeterioration.ToString.Replace(",", ".") & "</AccumulatedDeterioration>")
                If detail.LegalBookId IsNot Nothing Then
                    builder.Append("<LegalBookId>" & detail.LegalBookId & "</LegalBookId>")
                End If
                If detail.PortfolioDeteriorationClassificationId IsNot Nothing Then
                    builder.Append("<PortfolioDeteriorationClassificationId>" & detail.PortfolioDeteriorationClassificationId & "</PortfolioDeteriorationClassificationId>")
                End If

                builder.Append("</PortfolioProvisionDetail>")
            Next
        End If

        builder.Append("</PortfolioProvision>")

        Return builder.ToString()
    End Function

    Private Function ConvertToXmlListDelete(listPortfolioProvisionDetailDelete As List(Of Integer)) As String
        Dim builder As StringBuilder = New StringBuilder()

        If listPortfolioProvisionDetailDelete IsNot Nothing AndAlso listPortfolioProvisionDetailDelete.Count > 0 Then
            For Each detail In listPortfolioProvisionDetailDelete
                builder.Append("<PortfolioProvisionDetail>")
                builder.Append("<Id>" & detail & "</Id>")
                builder.Append("</PortfolioProvisionDetail>")
            Next
        End If

        Return builder.ToString()
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _portfolioProvisionRepository = Nothing
            _sequenceDRepository = Nothing
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
