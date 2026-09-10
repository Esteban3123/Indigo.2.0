Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports System.Data.SqlClient
Imports System.Text
Imports Domain.Base
Imports System.Transactions

Public Class LoadMassiveAdminService
    Implements ILoadMassiveAdminService

#Region "Properties"

    Private _loadMassiveRepository As ILoadMassiveRepository

#End Region

#Region "Builder"

    Public Sub New(ByVal loadMassiveRepository As ILoadMassiveRepository)
        If loadMassiveRepository Is Nothing Then
            Throw New ArgumentNullException("loadMassiveRepository Vacio")
        End If

        Me._loadMassiveRepository = loadMassiveRepository
    End Sub

#End Region

#Region "Methods"

    Public Function GetLoadMassive(code As String, audit As AuditMessage) As LoadMassive Implements ILoadMassiveAdminService.GetLoadMassive
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Return Me._loadMassiveRepository.GetLoadMassive(code.Trim())
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveLoadMassive(ByVal loadMassive As LoadMassive, dataBills As List(Of Domain.Base.Entities.ImportFileRow), dataDetails As List(Of LoadMassiveAccountPayableDetail), ByVal audit As AuditMessage, Optional ByVal withCommit As Boolean = False) As ActionResult(Of LoadMassive) Implements ILoadMassiveAdminService.SaveLoadMassive
        If loadMassive Is Nothing Then
            Throw New ArgumentNullException("loadMassive")
        End If
        If dataBills Is Nothing OrElse dataBills.Count = 0 Then
            Throw New ArgumentNullException("dataBills")
        End If
        If dataDetails Is Nothing OrElse dataDetails.Count = 0 Then
            'Throw New ArgumentNullException("dataDetails")
            dataDetails = New List(Of LoadMassiveAccountPayableDetail)
        End If

        Dim UnitOfWork As IUnitWork = _loadMassiveRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            'Listado de errores
            Dim listErrors As New List(Of String)

            Try
                loadMassive.DocumentDate = IIf(loadMassive.Id = 0, DateTime.Now, loadMassive.DocumentDate)
                loadMassive.ModificationUser = audit.CodeUser
                loadMassive.Status = If(withCommit, 2, 1)

                Dim xmlLoadMassive = Me.ConvertLoadMassiveToXml(loadMassive)
                Dim xmlBills = Me.ConvertBillsToXml(dataBills)
                Dim xmlDetails = Me.ConvertDetailsToXml(dataDetails)

                'Se consume el procedimiento almacenado
                Dim resultStore = _loadMassiveRepository.SP_SaveLoadMassive(xmlLoadMassive, xmlBills, xmlDetails)

                'Se crean los objetos para devolver y pegar en la rejilla
                If resultStore IsNot Nothing AndAlso resultStore.Count > 0 Then
                    For Each itemResult In resultStore
                        If itemResult.StatusField = 1 Then 'Si el estado del item es True y pasó todas las validaciones
                            'Se crea el nuevo objeto para agregarlo al listado
                            Dim accountPayable As LoadMassiveAccountPayable = loadMassive.LoadMassiveAccountPayable.Where(Function(x) x.Id = itemResult.HeadId).FirstOrDefault()
                            If accountPayable Is Nothing Then
                                accountPayable = New LoadMassiveAccountPayable() With
                                {
                                    .Id = itemResult.HeadId,
                                    .SupplierNit = itemResult.SupplierNit,
                                    .DistributionLineCode = itemResult.DistributionLineCode,
                                    .MainAccountNumber = itemResult.AccountPayableMainAccountNumber,
                                    .CostCenterCode = itemResult.CostCenterCode,
                                    .BillNumber = itemResult.BillNumber,
                                    .DocumentDate = itemResult.DocumentDate,
                                    .BillDate = itemResult.BillDate,
                                    .FilingUnitCode = itemResult.FilingUnitCode,
                                    .SupplierTypeCode = itemResult.SupplierTypeCode,
                                    .Term = itemResult.Term,
                                    .InvoiceValue = itemResult.InvoiceValue,
                                    .Value = itemResult.AccountPayableValue,
                                    .Coments = itemResult.Coments,
                                    .AccountPayableCode = itemResult.AccountPayableCode
                                }

                                loadMassive.LoadMassiveAccountPayable.Add(accountPayable)
                            End If

                            If itemResult.AccountPayableConceptId IsNot Nothing Then
                                Dim accountPayableDetailConcept As New LoadMassiveAccountPayableDetail() With
                                {
                                    .AccountPayableConceptCode = itemResult.AccountPayableCostCenterCode,
                                    .MainAccountNumber = itemResult.MainAccountNumber,
                                    .ThirdPartyNit = itemResult.ThirdPartyNit,
                                    .CostCenterCode = itemResult.CostCenterCode,
                                    .Detail = itemResult.Detail,
                                    .Nature = itemResult.Nature,
                                    .Value = itemResult.Value,
                                    .RetentionConceptCode = itemResult.RetentionConceptCode,
                                    .BaseValue = itemResult.BaseValue,
                                    .Percentage = itemResult.Percentage
                                }

                                accountPayable.LoadMassiveAccountPayableDetail.Add(accountPayableDetailConcept)
                            End If
                        Else 'Si el estado del item es False y no pasó alguna validación
                            listErrors.Add(itemResult.MessageField)
                        End If
                    Next

                    If resultStore.Any(Function(d) d.StatusField = 1) Then
                        Dim itemResult = resultStore.FirstOrDefault(Function(d) d.StatusField = 1)
                        loadMassive.Id = itemResult.LoadMassiveId
                        loadMassive.Code = itemResult.LoadMassiveCode
                        transaction.Complete()
                    Else
                        UnitOfWork.RollbackChanges()
                        transaction.Dispose()
                    End If
                End If

                Return New ActionResult(Of LoadMassive) With {.StateResult = True, .ObjectEmbbeded = loadMassive, .MessageResult = listErrors}
            Catch ex As SqlException
                If ex.ErrorCode = -2146232060 Then
                    Return New ActionResult(Of LoadMassive) With {.StateResult = False, .MessageResult = {"Los valores contienen decimales con un formato no valido, por favor corrija para poder continuar"}.ToList()}
                Else
                    Return New ActionResult(Of LoadMassive) With {.StateResult = False, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList()}
                End If
            Catch ex As Exception
                Return New ActionResult(Of LoadMassive) With {.StateResult = False, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList()}
            End Try
        End Using
    End Function

#End Region

#Region "Private Methods"

    Private Function ConvertLoadMassiveToXml(loadMassive As LoadMassive) As Object
        Dim builder As StringBuilder = New StringBuilder()

        builder.Append("<Data>")

        builder.Append("<Id>" & loadMassive.Id & "</Id>")
        builder.Append("<OperatingUnitId>" & loadMassive.OperatingUnitId & "</OperatingUnitId>")
        builder.Append("<Code>" & loadMassive.Code & "</Code>")
        builder.Append("<DocumentDate>" & loadMassive.DocumentDate.ToString("dd/MM/yyyy HH:mm") & "</DocumentDate>")
        builder.Append("<Observations>" & loadMassive.Observations & "</Observations>")
        builder.Append("<Status>" & loadMassive.Status & "</Status>")
        builder.Append("<ModificationUser>" & loadMassive.ModificationUser & "</ModificationUser>")

        builder.Append("</Data>")

        Return builder.ToString()
    End Function

    Private Function ConvertBillsToXml(dataBills As List(Of Domain.Base.Entities.ImportFileRow)) As Object
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<Data>")

        'Variable para saber si se obtiene valor de la colección
        Dim ColumnsQuantity As Integer = 0

        For Each row In dataBills
            Dim headerId As Integer = 0
            If Not Integer.TryParse(row.Row.Item(0), headerId) Then
                builder.Append("<Row>")

                builder.Append("<StatusField>" & 0 & "</StatusField>")
                builder.Append("<MessageField>" & String.Format("CABECERA: El registro {0} de la cabecera debe tener un identificador numerico valido", row.IndexRow) & "</MessageField>")

                builder.Append("</Row>")
                Continue For
            End If

            builder.Append("<Row>")

            builder.Append("<StatusField>" & 1 & "</StatusField>")
            builder.Append("<MessageField>" & "Ok" & "</MessageField>")

            builder.Append("<Id>" & row.IndexRow & "</Id>")
            builder.Append("<HeadId>" & headerId & "</HeadId>")
            builder.Append("<SupplierNit>" & row.Row.Item(1) & "</SupplierNit>")
            builder.Append("<DistributionLineCode>" & row.Row.Item(2) & "</DistributionLineCode>")
            builder.Append("<AccountPayableCostCenterCode>" & row.Row.Item(3) & "</AccountPayableCostCenterCode>")
            builder.Append("<BillNumber>" & row.Row.Item(4) & "</BillNumber>")
            builder.Append("<HandlesDocumentSupport>" & If(row.Row.Item(5).ToString() = "SI", True, False) & "</HandlesDocumentSupport>")
            builder.Append("<BillDate>" & CDate(row.Row.Item(6)).ToString("dd/MM/yyyy") & "</BillDate>")
            builder.Append("<DocumentDate>" & CDate(row.Row.Item(7)).ToString("dd/MM/yyyy") & "</DocumentDate>")
            builder.Append("<FilingUnitCode>" & row.Row.Item(8) & "</FilingUnitCode>")
            builder.Append("<SupplierTypeCode>" & row.Row.Item(9) & "</SupplierTypeCode>")
            builder.Append("<Term>" & row.Row.Item(10) & "</Term>")
            builder.Append("<InvoiceValue>" & row.Row.Item(11).ToString().Replace(",", ".") & "</InvoiceValue>")
            builder.Append("<Coments>" & row.Row.Item(12) & "</Coments>")

            builder.Append("</Row>")
        Next

        builder.Append("</Data>")
        Return builder.ToString
    End Function

    Private Function ConvertDetailsToXml(dataDetails As List(Of LoadMassiveAccountPayableDetail)) As Object
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<Data>")

        For Each item In dataDetails
            builder.Append("<Row>")

            builder.Append("<Id>" & item.Id & "</Id>")
            builder.Append("<HeadId>" & item.HeaderId & "</HeadId>")
            builder.Append("<AccountPayableConceptCode>" & item.AccountPayableConceptCode & "</AccountPayableConceptCode>")
            builder.Append("<ThirdPartyNit>" & item.ThirdPartyNit & "</ThirdPartyNit>")
            builder.Append("<CostCenterCode>" & item.CostCenterCode & "</CostCenterCode>")
            builder.Append("<Detail>" & item.Detail & "</Detail>")
            builder.Append("<Nature>" & item.Nature & "</Nature>")
            builder.Append("<Value>" & item.Value & "</Value>")
            builder.Append("<RetentionConceptCode>" & item.RetentionConceptCode & "</RetentionConceptCode>")
            builder.Append("<BaseValue>" & item.BaseValue & "</BaseValue>")

            builder.Append("</Row>")
        Next

        builder.Append("</Data>")
        Return builder.ToString
    End Function

#End Region

#Region "IDisposable Support"

    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
            End If
            Me._loadMassiveRepository = Nothing
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