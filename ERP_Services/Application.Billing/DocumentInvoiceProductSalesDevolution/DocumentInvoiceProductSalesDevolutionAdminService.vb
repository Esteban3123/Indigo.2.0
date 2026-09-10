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
Imports Application.Billing
Imports System.Text

Public Class DocumentInvoiceProductSalesDevolutionAdminService
    Implements IDocumentInvoiceProductSalesDevolutionAdminService

    ''' <summary>
    ''' repositorio de las secuencias
    ''' </summary>
    Private _secuenseDRepository As IBillingSequenceDetailRepository
    Private _documentInvoiceProductSalesDevolutionRepository As IDocumentInvoiceProductSalesDevolutionRepository

    Public Sub New(secuenceDRepository As IBillingSequenceDetailRepository, documentInvoiceProductSalesDevolutionRepository As IDocumentInvoiceProductSalesDevolutionRepository)
        If secuenceDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenceDRepository")
        End If
        If documentInvoiceProductSalesDevolutionRepository Is Nothing Then
            Throw New ArgumentNullException("documentInvoiceProductSalesDevolutionRepository")
        End If
        _secuenseDRepository = secuenceDRepository
        _documentInvoiceProductSalesDevolutionRepository = documentInvoiceProductSalesDevolutionRepository
    End Sub

    ''' <summary>
    ''' Guarda o actualiza el registro
    ''' </summary>
    ''' <param name="DocumentInvoiceProductSalesDevolution"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    Public Function SaveDocumentInvoiceProductSalesDevolution(DocumentInvoiceProductSalesDevolution As DocumentInvoiceProductSalesDevolution, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of DocumentInvoiceProductSalesDevolution) Implements IDocumentInvoiceProductSalesDevolutionAdminService.SaveDocumentInvoiceProductSalesDevolution
        If DocumentInvoiceProductSalesDevolution Is Nothing Then
            Throw New ArgumentNullException("DocumentInvoiceProductSalesDevolution")
        End If
        Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                Dim xml = ConvertEntityToXml(DocumentInvoiceProductSalesDevolution, audit)

                Dim result = _documentInvoiceProductSalesDevolutionRepository.SP_SaveDocumentInvoiceProductSalesDevolution(xml, audit.CodeUser)
                If result.CodeMessage <> 0 Then
                    scope.Dispose()
                    Return New ActionResult(Of DocumentInvoiceProductSalesDevolution) With {.StateResult = False, .Message = result.Message}
                End If

                DocumentInvoiceProductSalesDevolution.Id = result.DevolutionId
                DocumentInvoiceProductSalesDevolution.Code = result.DevolutionCode

                scope.Complete()
                Return New ActionResult(Of DocumentInvoiceProductSalesDevolution) With {.StateResult = True, .ObjectEmbbeded = DocumentInvoiceProductSalesDevolution, .Message = result.Message}
            Catch ex As Exception
                scope.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of DocumentInvoiceProductSalesDevolution) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Convierte la entidad de devolución en xml
    ''' </summary>
    ''' <param name="documentInvoiceProductSalesDevolution"></param>
    ''' <returns></returns>
    Private Function ConvertEntityToXml(documentInvoiceProductSalesDevolution As DocumentInvoiceProductSalesDevolution, audit As AuditMessage) As String
        Dim builder As New StringBuilder

        builder.Append("<DocumentInvoiceProductSalesDevolution>")

        With documentInvoiceProductSalesDevolution
            builder.Append("<Id>" & .Id & "</Id>")
            builder.Append("<Code>" & .Code & "</Code>")
            builder.Append("<DocumentDate>" & .DocumentDate.ToString("dd/MM/yyyy HH:mm:ss") & "</DocumentDate>")
            builder.Append("<WarehouseId>" & .WarehouseId & "</WarehouseId>")
            builder.Append("<Detail>" & .Detail & "</Detail>")
            builder.Append("<DocumentInvoiceProductSalesId>" & .DocumentInvoiceProductSalesId & "</DocumentInvoiceProductSalesId>")
            builder.Append("<FreightValue>" & .FreightValue & "</FreightValue>")
            builder.Append("<FreightIVAPercentage>" & .FreightIVAPercentage & "</FreightIVAPercentage>")
            builder.Append("<FreightIVAValue>" & .FreightIVAValue & "</FreightIVAValue>")
            builder.Append("<Value>" & .Value & "</Value>")
            builder.Append("<ValueDiscount>" & .ValueDiscount & "</ValueDiscount>")
            builder.Append("<ValueTax>" & .ValueTax & "</ValueTax>")
            builder.Append("<WithholdingTax>" & .WithholdingTax & "</WithholdingTax>")
            builder.Append("<WithholdingICA>" & .WithholdingICA & "</WithholdingICA>")
            builder.Append("<RetentionSource>" & .RetentionSource & "</RetentionSource>")
            builder.Append("<RetentionOther>" & .RetentionOther & "</RetentionOther>")
            builder.Append("<DeductionOther>" & .DeductionOther & "</DeductionOther>")
            builder.Append("<DistrictTax>" & .DistrictTax & "</DistrictTax>")
            builder.Append("<TotalValue>" & .TotalValue & "</TotalValue>")
            builder.Append("<Status>" & .Status & "</Status>")
            builder.Append("<OperatingUnitId>" & .OperatingUnitId & "</OperatingUnitId>")
            builder.Append("<CompanyType>" & CByte(audit.CompanyType) & "</CompanyType>")

            If .DocumentInvoiceProductSalesDevolutionDetail IsNot Nothing AndAlso .DocumentInvoiceProductSalesDevolutionDetail.Count > 0 Then
                For Each itemDetail In .DocumentInvoiceProductSalesDevolutionDetail
                    builder.Append("<DocumentInvoiceProductSalesDevolutionDetail>")
                    builder.Append("<Id>" & itemDetail.Id & "</Id>")
                    builder.Append("<DocumentInvoiceProductSalesDevolutionId>" & itemDetail.DocumentInvoiceProductSalesDevolutionId & "</DocumentInvoiceProductSalesDevolutionId>")
                    builder.Append("<DocumentInvoiceProductSalesDetailBatchSerialId>" & itemDetail.DocumentInvoiceProductSalesDetailBatchSerialId & "</DocumentInvoiceProductSalesDetailBatchSerialId>")
                    builder.Append("<Quantity>" & itemDetail.QuantityDevolution & "</Quantity>")
                    If itemDetail.ChangeTracker.State <> ObjectState.Deleted Then
                        builder.Append("<IsDelete>" & 0 & "</IsDelete>")
                    Else
                        builder.Append("<IsDelete>" & 1 & "</IsDelete>")
                    End If
                    builder.Append("<SubTotalValue>" & itemDetail.SubTotalValue & "</SubTotalValue>")
                    builder.Append("<DiscountValue>" & itemDetail.DiscountValue & "</DiscountValue>")
                    builder.Append("<RTFValue>" & itemDetail.RTFValue & "</RTFValue>")
                    builder.Append("<RTFPercentage>" & itemDetail.RTFPercentage & "</RTFPercentage>")
                    builder.Append("</DocumentInvoiceProductSalesDevolutionDetail>")
                Next
            End If
        End With

        builder.Append("</DocumentInvoiceProductSalesDevolution>")

        Return builder.ToString()
    End Function

    ''' <summary>
    ''' Obtiene el registro por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function GetDocumentInvoiceProductSalesDevolution(code As String, audit As AuditMessage) As ActionResult(Of DocumentInvoiceProductSalesDevolution) Implements IDocumentInvoiceProductSalesDevolutionAdminService.GetDocumentInvoiceProductSalesDevolution
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim entity As DocumentInvoiceProductSalesDevolution = Me._documentInvoiceProductSalesDevolutionRepository.GetDocumentInvoiceProductSalesDevolutionByCode(code.Trim())
            If entity IsNot Nothing AndAlso entity.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of DocumentInvoiceProductSalesDevolution)(entity, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of DocumentInvoiceProductSalesDevolution) With {.StateResult = True, .ObjectEmbbeded = entity}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DocumentInvoiceProductSalesDevolution) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el registro por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetDocumentInvoiceProductSalesDevolutionById(id As Integer) As ActionResult(Of DocumentInvoiceProductSalesDevolution) Implements IDocumentInvoiceProductSalesDevolutionAdminService.GetDocumentInvoiceProductSalesDevolutionById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Try
            Dim entity As DocumentInvoiceProductSalesDevolution = Me._documentInvoiceProductSalesDevolutionRepository.GetDocumentInvoiceProductSalesDevolutionById(id)
            Return New ActionResult(Of DocumentInvoiceProductSalesDevolution) With {.StateResult = True, .ObjectEmbbeded = entity}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DocumentInvoiceProductSalesDevolution) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
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
            _secuenseDRepository = Nothing
            _documentInvoiceProductSalesDevolutionRepository = Nothing
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
