'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Carlos Ernesto Cordoba
' Created          : 04-07-2014
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
Imports Application.Accounting
Imports System.Data.Entity.Validation
Imports Domain.Entities.Service
Imports Infrastructure.CrossCutting.Resources
Imports Application.Portfolio
Imports Application.Payments

Imports System.Text

#End Region

Public Class PortfolioTransfersAdminService
    Implements IPortfolioTransfersAdminService

#Region "Fileds"

    Private _portfolioTransfersRepository As IPortfolioTransferRepository
    Private _accountReceivableRepository As IAccountReceivableRepository
    Private _RepositoryMainAccounts As IPUCRepository

#End Region

#Region "Builder"

    Public Sub New(portfolioTransfersRepository As IPortfolioTransferRepository,
                   accountReceivableRepository As IAccountReceivableRepository,
                   repositoryMainAccounts As IPUCRepository)

        If portfolioTransfersRepository Is Nothing Then
            Throw New ArgumentNullException("portfolioTransfersRepository")
        End If
        If accountReceivableRepository Is Nothing Then
            Throw New ArgumentNullException("accountReceivableRepository")
        End If
        If repositoryMainAccounts Is Nothing Then
            Throw New ArgumentNullException("repositoryMainAccounts")
        End If

        _portfolioTransfersRepository = portfolioTransfersRepository
        _accountReceivableRepository = accountReceivableRepository
        _RepositoryMainAccounts = repositoryMainAccounts
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' metodo para pegar en la rejilla de traslado
    ''' </summary>
    ''' <param name="data"></param>
    ''' <param name="PortfolioTransfer"></param>
    ''' <returns></returns>
    Public Function SetBillsTransfersCopyPaste(data As List(Of List(Of String)), PortfolioTransfer As PortfolioTransfer, CompanyType As Integer, OperatingUnitId As Integer) As ActionResult(Of List(Of PortfolioTransferDetail)) Implements IPortfolioTransfersAdminService.SetBillsTransfersCopyPaste
        If data Is Nothing OrElse data.Count = 0 Then
            Throw New ArgumentNullException("Data")
        End If

        'Listado de errores
        Dim listErrors As New List(Of String)

        'Listado que se devuelve para pegar a la rejilla del form
        Dim listPortfolioTransferDetail As New List(Of PortfolioTransferDetail)

        Try
            Dim xmlObject = ConvertBillsToXml(OperatingUnitId, PortfolioTransfer, data)

            'Se consume el procedimiento almacenado
            Dim resultStore = _portfolioTransfersRepository.SP_CopyAndPasteTransfer(xmlObject, PortfolioTransfer.TransferType, CompanyType)

            'Se crean los objetos para devolver y pegar en la rejilla
            If resultStore IsNot Nothing AndAlso resultStore.Count > 0 Then
                For Each itemXml In resultStore
                    If itemXml.StatusField = 1 Then 'Si el estado del item es True y pasó todas las validaciones
                        'Se crea el nuevo objeto para agregarlo al listado
                        Dim accountPayable As PortfolioTransferDetail = listPortfolioTransferDetail.Where(Function(x) x.AccountReceivableId = itemXml.AccountReceivableId).FirstOrDefault()
                        If accountPayable Is Nothing OrElse resultStore.Count > 0 Then
                            accountPayable = New PortfolioTransferDetail() With
                            {
                                .AccountReceivableId = itemXml.AccountReceivableId,
                                .InvoiceNumber = itemXml.InvoiceNumber,
                                .MainAccountId = itemXml.MainAccountId,
                                .CodeNameMainAccount = itemXml.MainAccountNumberName,
                                .CostCenterId = itemXml.CostCenterId,
                                .ValueBill = itemXml.InvoiceValue,
                                .Balance = itemXml.Balance,
                                .Value = itemXml.Value,
                                .PortfolioStatusName = itemXml.PortfolioStatusName,
                                .TRMValue = itemXml.TRMValue
                            }

                            listPortfolioTransferDetail.Add(accountPayable)
                        End If
                    Else 'Si el estado del item es False y no pasó alguna validación
                        listErrors.Add(itemXml.MessageField)
                    End If
                Next
            End If

            Return New ActionResult(Of List(Of PortfolioTransferDetail)) With {.StateResult = True, .ObjectEmbbeded = listPortfolioTransferDetail, .MessageResult = listErrors}
        Catch ex As SqlClient.SqlException
            If ex.ErrorCode = -2146232060 Then
                Return New ActionResult(Of List(Of PortfolioTransferDetail)) With {.StateResult = False, .MessageResult = {"Los valores contienen decimales con un formato no valido, por favor corrija para poder continuar"}.ToList()}
            Else
                Return New ActionResult(Of List(Of PortfolioTransferDetail)) With {.StateResult = False, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList()}
            End If
        Catch ex As Exception
            Return New ActionResult(Of List(Of PortfolioTransferDetail)) With {.StateResult = False, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList()}
        End Try
    End Function

    ''' <summary>
    ''' obtiene un traslado por id
    ''' </summary>
    ''' <param name="idPortfolioTransfer"></param>
    ''' <returns></returns>
    Public Function GetPortfolioTransferById(idPortfolioTransfer As Integer) As PortfolioTransfer Implements IPortfolioTransfersAdminService.GetPortfolioTransferById
        Try
            Return _portfolioTransfersRepository.GetPortfolioTransferById(idPortfolioTransfer)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New PortfolioTransfer()
        End Try
    End Function

    ''' <summary>
    ''' obtiene un traslado por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function GetPortfolioTransfersByCode(code As String, audit As AuditMessage) As PortfolioTransfer Implements IPortfolioTransfersAdminService.GetPortfolioTransfersByCode
        Try
            Dim portfolioTransfers = _portfolioTransfersRepository.GetPortfolioTransfersByCode(code)
            If portfolioTransfers IsNot Nothing AndAlso portfolioTransfers.Id > 0 Then
                Dim auditProcess As New IndigoAuditSimpleEntity(Of PortfolioTransfer)(portfolioTransfers, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditProcess.Execute()
            End If
            Return portfolioTransfers
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New PortfolioTransfer
        End Try
    End Function

    ''' <summary>
    ''' obtiene los detalles del traslado
    ''' </summary>
    ''' <param name="idPortfolioTransfer"></param>
    ''' <returns></returns>
    Public Function GatPortfolioTransferDetailByIdPortfolioTransfer(idPortfolioTransfer As Integer) As List(Of PortfolioTransferDetail) Implements IPortfolioTransfersAdminService.GatPortfolioTransferDetailByIdPortfolioTransfer
        Try
            Return _portfolioTransfersRepository.GatPortfolioTransferDetailByIdPortfolioTransfer(idPortfolioTransfer)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of PortfolioTransferDetail)
        End Try
    End Function

    ''' <summary>
    ''' lista los otros conceptos de traslados
    ''' </summary>
    ''' <param name="idPortfolioTransfer"></param>
    ''' <returns></returns>
    Public Function GetPortfolioTransferOtherConceptByIdPortfolioTransfer(idPortfolioTransfer As Integer) As List(Of PortfolioTransferOtherConcept) Implements IPortfolioTransfersAdminService.GetPortfolioTransferOtherConceptByIdPortfolioTransfer
        Try
            Return _portfolioTransfersRepository.GetPortfolioTransferOtherConceptByIdPortfolioTransfer(idPortfolioTransfer)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of PortfolioTransferOtherConcept)
        End Try
    End Function

    ''' <summary>
    ''' guardar un traslado
    ''' </summary>
    ''' <param name="PortfolioTransfer"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SavePortfolioTransfer(PortfolioTransfer As PortfolioTransfer, audit As AuditMessage) As ActionResult(Of PortfolioTransfer) Implements IPortfolioTransfersAdminService.SavePortfolioTransfer
        If PortfolioTransfer Is Nothing Then
            Throw New ArgumentNullException("PortfolioTransfer")
        End If

        Dim unitOfWork As IUnitWork = Me._portfolioTransfersRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim EntityXml As String = ConvertToXmlPortfolioTransfer(PortfolioTransfer)
                Dim auditStatus As Infrastructure.CrossCutting.Audit.Actions = Utils.GetAuditStatus(PortfolioTransfer.Id, PortfolioTransfer.Status)
                Dim companyType As Byte = CByte(audit.CompanyType)

                Dim resultStore = Me._portfolioTransfersRepository.SP_SavePortfolioTransfer(EntityXml, audit.CodeUser, companyType)
                If resultStore.CodeResult <> 0 Then
                    unitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of PortfolioTransfer) With {.StateResult = False, .MessageResult = {resultStore.MessageResult}.ToList, .Message = resultStore.MessageResult}
                End If

                PortfolioTransfer.Id = resultStore.Id
                PortfolioTransfer.Code = resultStore.Code

                Dim auditProcess = New IndigoAuditSimpleEntity(Of PortfolioTransfer)(PortfolioTransfer, audit, auditStatus, PortfolioTransfer.OriginalValue)
                auditProcess.Execute()

                PortfolioTransfer.MarkAsUnchanged()
                transaction.Complete()
                Return New ActionResult(Of PortfolioTransfer) With {.StateResult = True, .ObjectEmbbeded = PortfolioTransfer, .Message = resultStore.MessageResult}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                Return New ActionResult(Of PortfolioTransfer) With {.StateResult = False, .MessageResult = {"-999"}.ToList(), .Message = ex.Message}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of PortfolioTransfer) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

#End Region

#Region "Private Methods"

    Private Function ConvertBillsToXml(OperatingUnitId As Integer, PortfolioTransfer As PortfolioTransfer, data As List(Of List(Of String)))
        Dim builder As StringBuilder = New StringBuilder()
        Dim fields = If(PortfolioTransfer.TransferType = 1, 3, 4)

        builder.Append("<Header>")
        builder.Append($"<PortfolioAdvanceId>{PortfolioTransfer.PortfolioAdvanceId}</PortfolioAdvanceId>")
        builder.Append($"<CurrencyId>{PortfolioTransfer.CurrencyId}</CurrencyId>")
        builder.Append("</Header>")

        For Each item In data
            builder.Append("<Data>")
            builder.Append($"<CountFields>{item.Count}</CountFields>")
            builder.Append($"<StatusField>{1}</StatusField>")
            builder.Append("<MessageField>Ok</MessageField>")
            builder.Append($"<OperatingUnitId>{OperatingUnitId}</OperatingUnitId>")
            builder.Append($"<ThirdPartyId>{If(PortfolioTransfer.TransferType = 1, PortfolioTransfer.ThirdPartyId, 0)}</ThirdPartyId>")
            builder.Append($"<ThirdPartyNit>{If(PortfolioTransfer.TransferType = 1, String.Empty, Utils.ValidateItem(item, 3, fields))}</ThirdPartyNit>")
            builder.Append($"<BillNumber>{Utils.ValidateItem(item, 0, fields)}</BillNumber>")
            builder.Append($"<CodeAccount>{Utils.ValidateItem(item, 1, fields)}</CodeAccount>")
            builder.Append($"<Value>{Replace(Utils.ValidateItem(item, 2, fields), ",", ".")}</Value>")
            builder.Append("</Data>")
        Next
        builder.Append("")
        Return builder.ToString
    End Function

    ''' <summary>
    ''' Convertir el objeto en xml
    ''' </summary>
    ''' <param name="PortfolioTransfer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConvertToXmlPortfolioTransfer(PortfolioTransfer As PortfolioTransfer) As String
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<PortfolioTransfer>")

        builder.Append("<Id>" & PortfolioTransfer.Id & "</Id>")
        builder.Append("<Code>" & PortfolioTransfer.Code & "</Code>")
        builder.Append("<OperatingUnitId>" & PortfolioTransfer.OperatingUnitId & "</OperatingUnitId>")
        builder.Append("<DocumentDate>" & PortfolioTransfer.DocumentDate.ToString("dd/MM/yyyy hh:mm:ss") & "</DocumentDate>")
        builder.Append("<CustomerId>" & PortfolioTransfer.CustomerId & "</CustomerId>")
        builder.Append("<ThirdPartyId>" & PortfolioTransfer.ThirdPartyId & "</ThirdPartyId>")
        builder.Append("<PortfolioAdvanceId>" & PortfolioTransfer.PortfolioAdvanceId & "</PortfolioAdvanceId>")
        builder.Append("<TransferType>" & PortfolioTransfer.TransferType & "</TransferType>")
        builder.Append("<MainAccountId>" & PortfolioTransfer.MainAccountId & "</MainAccountId>")
        builder.Append("<CostCenterId>" & PortfolioTransfer.CostCenterId & "</CostCenterId>")
        builder.Append("<Observations>" & PortfolioTransfer.Observations & "</Observations>")
        builder.Append("<Status>" & PortfolioTransfer.Status & "</Status>")
        builder.Append($"<CurrencyId>{PortfolioTransfer?.CurrencyId}</CurrencyId>")

        If PortfolioTransfer.PortfolioTransferDetail IsNot Nothing AndAlso PortfolioTransfer.PortfolioTransferDetail.Count > 0 Then
            For Each detail As PortfolioTransferDetail In PortfolioTransfer.PortfolioTransferDetail
                builder.Append("<PortfolioTransferDetail>")

                builder.Append("<Id>" & detail.Id & "</Id>")
                builder.Append("<PortfolioTrasferId>" & detail.PortfolioTrasferId & "</PortfolioTrasferId>")
                builder.Append("<AccountReceivableId>" & detail.AccountReceivableId & "</AccountReceivableId>")
                builder.Append("<MainAccountId>" & detail.MainAccountId & "</MainAccountId>")
                builder.Append("<CostCenterId>" & detail.CostCenterId & "</CostCenterId>")
                builder.Append("<Value>" & detail.Value.ToString().Replace(",", ".") & "</Value>")
                builder.Append("<ChangeTracker>" & If(detail.ChangeTracker.State = ObjectState.Deleted, 1, 0) & "</ChangeTracker>")
                builder.Append("<TRMValue>" & detail.TRMValue.ToString().Replace(",", ".") & "</TRMValue>")

                builder.Append("</PortfolioTransferDetail>")
            Next
        End If

        If PortfolioTransfer.PortfolioTransferOtherConcept IsNot Nothing AndAlso PortfolioTransfer.PortfolioTransferOtherConcept.Count > 0 Then
            For Each detail As PortfolioTransferOtherConcept In PortfolioTransfer.PortfolioTransferOtherConcept
                builder.Append("<PortfolioTransferOtherConcept>")

                builder.Append("<Id>" & detail.Id & "</Id>")
                builder.Append("<PortfolioTransferId>" & detail.PortfolioTransferId & "</PortfolioTransferId>")
                builder.Append("<PortfolioNoteConceptId>" & detail.PortfolioNoteConceptId & "</PortfolioNoteConceptId>")
                builder.Append("<MainAccountId>" & detail.MainAccountId & "</MainAccountId>")
                builder.Append("<ThirdPartyId>" & detail.ThirdPartyId & "</ThirdPartyId>")
                builder.Append("<CostCenterId>" & detail.CostCenterId & "</CostCenterId>")
                builder.Append("<Nature>" & detail.Nature & "</Nature>")
                builder.Append("<Value>" & detail.Value.ToString().Replace(",", ".") & "</Value>")
                builder.Append("<ChangeTracker>" & If(detail.ChangeTracker.State = ObjectState.Deleted, 1, 0) & "</ChangeTracker>")

                builder.Append("</PortfolioTransferOtherConcept>")
            Next
        End If

        builder.Append("</PortfolioTransfer>")
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

            _portfolioTransfersRepository = Nothing
            _accountReceivableRepository = Nothing
            _RepositoryMainAccounts = Nothing
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
