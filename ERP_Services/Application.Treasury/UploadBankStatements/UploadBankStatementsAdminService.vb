'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Johan Sebastian Cuellar Esquivel
' Created          : 13-04-2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Libraries"
Imports System.Data.Entity.Core
Imports System.Data.Entity.Validation
Imports System.Text
Imports System.Transactions
Imports Application.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Entities.Service
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions

#End Region

Public Class UploadBankStatementsAdminService
    Implements IUploadBankStatementsAdminService


    Private _UploadBankStatementsRepository As IUploadBankStatementsRepository
    Private _TreasuryService As ITreasuryServices

    Public Sub New(UploadBankStatementsRepository As IUploadBankStatementsRepository, TreasuryService As ITreasuryServices)
        _UploadBankStatementsRepository = UploadBankStatementsRepository
        _TreasuryService = TreasuryService
    End Sub

    Public Function GetUploadBankStatementsByCode(code As String, audit As AuditMessage) As UploadBankStatements Implements IUploadBankStatementsAdminService.GetUploadBankStatementsByCode
        Try
            Dim UploadBankStatements = _UploadBankStatementsRepository.GetUploadBankStatementsByCode(code)
            If UploadBankStatements IsNot Nothing AndAlso UploadBankStatements.Id > 0 Then
                Dim auditProcess As New IndigoAuditSimpleEntity(Of UploadBankStatements)(UploadBankStatements, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditProcess.Execute()
            End If
            Return UploadBankStatements
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New UploadBankStatements
        End Try
    End Function

    ''' <summary>
    ''' Función que se encarga de validar si se ha asignado algún Tipo documento en los detalles 
    ''' En caso de no haber ninguno asignado los asigna acorde a los parámetros establecidos
    ''' </summary>
    ''' <param name="uploadBankStatements"></param>
    ''' <returns></returns>
    Public Function ValidateDocumentTypeToAssign(ByVal uploadBankStatements As UploadBankStatements) As ActionResult(Of List(Of UploadBankStatementsDetail))

        ' Obtenemos los conceptos de conciliación bancarios de la entidad bancaria
        Dim conciliationConcepts = GetBankConciliationConceptsByEntityBankAccountsId(uploadBankStatements.EntityBankAccountId)

        ' Obtenemos los Detalles con los Tipos de Documentos Asignados

        Return MatchConciliationConceptsWithDescriptionTransaction(conciliationConcepts.ObjectEmbbeded, uploadBankStatements.UploadBankStatementsDetail.ToList())
    End Function

    ''' <summary>
    ''' Función que se encarga de Guardar, Actualizar, Confirmar o Anular los Extractos de conciliación bancaria
    ''' </summary>
    ''' <param name="UploadBankStatements"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveUploadBankStatements(UploadBankStatements As UploadBankStatements, audit As AuditMessage) As ActionResult(Of UploadBankStatements) Implements IUploadBankStatementsAdminService.SaveUploadBankStatements

        Using Transaction As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                ' Al guardar o actualizar asignamos el Tipo de Documento a los detalles que no se han asignado
                If UploadBankStatements.Status = 1 Then
                    Dim newDetails = ValidateDocumentTypeToAssign(UploadBankStatements)
                    If newDetails.StateResult Then
                        UploadBankStatements.UploadBankStatementsDetail.Clear()
                        newDetails.ObjectEmbbeded.ForEach(Sub(x)
                                                              UploadBankStatements.UploadBankStatementsDetail.Add(x)
                                                          End Sub)
                    End If
                End If

                Dim xml = UploadBankStatements.ToXML()

                Dim result = _UploadBankStatementsRepository.GenerateUploadBankStatementsSP(xml, audit.CodeUser).ToList().ElementAt(0)
                If result.CodeMessage = "999" Then
                    Return New ActionResult(Of UploadBankStatements) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = result.Message}
                Else
                    Dim UploadBankStatementsReturn = _UploadBankStatementsRepository.GetUploadBankStatementsById(result.UploadBankStatementsId)
                    Transaction.Complete()
                    If result.Message.Length = 0 Then
                        Return New ActionResult(Of UploadBankStatements) With {.StateResult = True, .ObjectEmbbeded = UploadBankStatementsReturn}
                    Else
                        Return New ActionResult(Of UploadBankStatements) With {.StateResult = True, .Message = result.Message, .ObjectEmbbeded = UploadBankStatementsReturn}
                    End If

                End If

                Return New ActionResult(Of UploadBankStatements) With {.StatusCode = eStatusResult.SUCCESS, .Message = "Proceso Finalizado Correctamente"}
            Catch ex As OptimisticConcurrencyException
                Transaction.Dispose()
                Return New ActionResult(Of UploadBankStatements) With {.StatusCode = eStatusResult.EXCEPTION, .StateResult = False, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
            Catch ex As DbEntityValidationException
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of UploadBankStatements) With {.StatusCode = eStatusResult.EXCEPTION, .StateResult = False, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
            Catch ex As Exception
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of UploadBankStatements) With {.StatusCode = eStatusResult.EXCEPTION, .StateResult = False, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
            End Try
        End Using
    End Function

    Public Function GetUploadBankStatementsDetailByUploadBankStatementsId(UploadBankStatementsId As Integer) As List(Of UploadBankStatementsDetail) Implements IUploadBankStatementsAdminService.GetUploadBankStatementsDetailByUploadBankStatementsId
        Try
            Return _UploadBankStatementsRepository.GetUploadBankStatementsDetailByUploadBankStatementsId(UploadBankStatementsId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of UploadBankStatementsDetail)
        End Try
    End Function

    Public Function SetCopyPasteOrImportFileUploadBankStatements(dataImportFile As List(Of ImportFileRow), dataCopyPaste As List(Of List(Of String))) As ActionResult(Of List(Of UploadBankStatementsDetail)) Implements IUploadBankStatementsAdminService.SetCopyPasteOrImportFileSetBankStatementsDetail
        If dataImportFile IsNot Nothing Then
            Return SetImportFileUploadBankStatementsDetail(dataImportFile)
        Else
            Return SetCopyPasteUploadBankStatementsDetail(dataCopyPaste)
        End If
    End Function
    ''' <summary>
    ''' Función que retorno los Conceptos de Conciliación relacionados con el banco
    ''' </summary>
    ''' <param name="EntityBankAccountId"></param>
    ''' <returns></returns>
    Public Function GetBankConciliationConceptsByEntityBankAccountsId(EntityBankAccountId As Integer) As ActionResult(Of List(Of BankConciliationConcepts)) Implements IUploadBankStatementsAdminService.GetBankConciliationConceptsByEntityBankAccountId
        Try
            Dim res = _UploadBankStatementsRepository.GetBankConciliationConceptsByEntityBankAccounts(EntityBankAccountId)
            Return New ActionResult(Of List(Of BankConciliationConcepts)) With {.StatusCode = eStatusResult.SUCCESS, .StateResult = True, .ObjectEmbbeded = res}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of BankConciliationConcepts)) With {.StatusCode = eStatusResult.EXCEPTION, .StateResult = False, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function
    ''' <summary>
    ''' Función que se encarga de asignar el DocumentType de acuerdo con la Descripción de la transacción
    ''' </summary>
    ''' <param name="ConciliationConcepts"></param>
    ''' <param name="UploadBankStatementDetails"></param>
    ''' <returns></returns>
    Public Function MatchConciliationConceptsWithDescriptionTransaction(ConciliationConcepts As List(Of BankConciliationConcepts), UploadBankStatementDetails As List(Of UploadBankStatementsDetail)) As ActionResult(Of List(Of UploadBankStatementsDetail)) Implements IUploadBankStatementsAdminService.MatchConciliationConceptsWithDescriptionTransaction
        Try
            For Each detail In UploadBankStatementDetails
                ' Solo se asignan los que se encuentran vacíos para no modificar los seleccionados por el usuario
                If detail.DocumentType Is Nothing AndAlso Not String.IsNullOrWhiteSpace(detail.DescriptionTransaction) Then

                    ' Normalizamos strings
                    Dim description As String = detail.DescriptionTransaction.Trim()

                    ' Buscamos coincidencia exacta o si la descripción comienza con el concepto
                    Dim matchingConcept = ConciliationConcepts.FirstOrDefault(Function(concept)
                                                                                  Dim conceptName = concept.Name.Trim()
                                                                                  Return conceptName.Equals(description, StringComparison.OrdinalIgnoreCase) OrElse
                                                                                      description.StartsWith(conceptName, StringComparison.OrdinalIgnoreCase)
                                                                              End Function)

                    ' Si se encuentra una coincidencia, asignamos el DocumentType
                    If matchingConcept IsNot Nothing Then
                        Select Case matchingConcept.DocumentType
                            Case 1 ' Comprobante de Egreso (solo para créditos)
                                If detail.ValueCredit <> 0 Then
                                    detail.DocumentType = 2
                                End If
                            Case 2 ' Consignaciones (solo para débitos)
                                If detail.ValueDebit <> 0 Then
                                    detail.DocumentType = 4
                                End If
                            Case 3 ' Recibo de Caja (solo para débitos)
                                If detail.ValueDebit <> 0 Then
                                    detail.DocumentType = 1
                                End If
                            Case 4 ' Notas (aplica para ambos, crédito o débito)
                                detail.DocumentType = 3
                        End Select
                    End If
                End If
            Next
            Dim UpdatedDetails = SetConsecutiveBank(UploadBankStatementDetails)
            'Se retornan los detalles actualizados con el DocumentType
            Return New ActionResult(Of List(Of UploadBankStatementsDetail)) With {.StatusCode = eStatusResult.SUCCESS, .StateResult = True, .ObjectEmbbeded = UpdatedDetails}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of UploadBankStatementsDetail)) With {.StatusCode = eStatusResult.EXCEPTION, .StateResult = False, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function
    ''' <summary>
    ''' Asigna los consecutivos del banco que vienen vacios
    ''' </summary>
    ''' <param name="UploadBankStatementDetails"></param>
    ''' <returns></returns>
    Private Function SetConsecutiveBank(ByVal UploadBankStatementDetails As List(Of UploadBankStatementsDetail)) As List(Of UploadBankStatementsDetail)
        ' Ordenamos los detalles por la fecha de la transacción, suponiendo que haya una propiedad DateTransaction
        Dim orderedDetails = UploadBankStatementDetails.OrderBy(Function(d) d.TransactionDate).ToList()

        ' Inicializamos el contador del consecutivo
        Dim consecutive As Integer = 1

        ' Iteramos sobre cada detalle y asignamos un consecutivo si está vacío
        For Each detail In orderedDetails
            If String.IsNullOrEmpty(detail.ConsecutiveBank) Then
                detail.ConsecutiveBank = consecutive.ToString()
                consecutive += 1
            End If
        Next

        ' Devolvemos la lista actualizada
        Return UploadBankStatementDetails
    End Function

    Private Function SetCopyPasteUploadBankStatementsDetail(dataCopyPaste As List(Of List(Of String))) As ActionResult(Of List(Of UploadBankStatementsDetail))
        Dim listErros As New List(Of String)
        Try
            Dim UploadBankStatementsDetailXml As New StringBuilder

            For i As Integer = 0 To dataCopyPaste.Count - 1 Step 1
                UploadBankStatementsDetailXml.Append("<UploadBankStatementsDetail>")
                UploadBankStatementsDetailXml.Append(String.Format("<{0}>{1}</{0}>", "TransactionDate", dataCopyPaste.Item(i).Item(0)))
                UploadBankStatementsDetailXml.Append(String.Format("<{0}>{1}</{0}>", "ConsecutiveBank", dataCopyPaste.Item(i).Item(1)))
                UploadBankStatementsDetailXml.Append(String.Format("<{0}>{1}</{0}>", "TransactionCode", dataCopyPaste.Item(i).Item(2)))
                UploadBankStatementsDetailXml.Append(String.Format("<{0}>{1}</{0}>", "DescriptionTransaction", dataCopyPaste.Item(i).Item(3)))
                UploadBankStatementsDetailXml.Append(String.Format("<{0}>{1}</{0}>", "ValueDebit", dataCopyPaste.Item(i).Item(4)))
                UploadBankStatementsDetailXml.Append(String.Format("<{0}>{1}</{0}>", "ValueCredit", dataCopyPaste.Item(i).Item(5)))
                UploadBankStatementsDetailXml.Append(String.Format("<{0}>{1}</{0}>", "BankCheck", dataCopyPaste.Item(i).Item(6)))
                UploadBankStatementsDetailXml.Append(String.Format("<{0}>{1}</{0}>", "PaymentReferenceOne", dataCopyPaste.Item(i).Item(7)))
                UploadBankStatementsDetailXml.Append(String.Format("<{0}>{1}</{0}>", "PaymentReferenceTwo", dataCopyPaste.Item(i).Item(8)))
                UploadBankStatementsDetailXml.Append("</UploadBankStatementsDetail>")
            Next

            Dim result = _UploadBankStatementsRepository.SetUploadBankStatementsDetail(UploadBankStatementsDetailXml.ToString()).ToList()
            Dim listDetailsResult As New List(Of UploadBankStatementsDetail)
            listErros.AddRange(From e In result Where e.Status = 2 Select e.Message)
            For Each item In result
                Dim UploadBankStatementsDetailDetail = New UploadBankStatementsDetail
                With UploadBankStatementsDetailDetail
                    .TransactionDate = item.TransactionDate
                    .ConsecutiveBank = item.ConsecutiveBank
                    .TransactionCode = item.TransactionCode
                    .DescriptionTransaction = item.DescriptionTransaction
                    .ValueDebit = item.ValueDebit
                    .ValueCredit = item.ValueCredit
                    .BankCheck = item.BankCheck
                    .PaymentReferenceOne = item.PaymentReferenceOne
                    .PaymentReferenceTwo = item.PaymentReferenceTwo
                End With
                listDetailsResult.Add(UploadBankStatementsDetailDetail)
            Next
            Return New ActionResult(Of List(Of UploadBankStatementsDetail)) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = listDetailsResult, .MessageResult = listErros}
        Catch ex As Exception

            Return New ActionResult(Of List(Of UploadBankStatementsDetail)) With {.StatusCode = eStatusResult.EXCEPTION, .MessageResult = listErros}
        End Try
    End Function

    Private Function SetImportFileUploadBankStatementsDetail(data As List(Of ImportFileRow)) As ActionResult(Of List(Of UploadBankStatementsDetail))
        Dim listErros As New List(Of String)
        Try
            Dim UploadBankStatementsDetailXml As New StringBuilder

            For Each row In data
                UploadBankStatementsDetailXml.Append("<UploadBankStatementsDetail>")
                UploadBankStatementsDetailXml.Append(String.Format("<{0}>{1}</{0}>", "TransactionDate", CDate(row.Row.Item(0)).ToString("yyyy-MM-dd HH:mm:ss")))
                UploadBankStatementsDetailXml.Append(String.Format("<{0}>{1}</{0}>", "ConsecutiveBank", row.Row.Item(1)))
                UploadBankStatementsDetailXml.Append(String.Format("<{0}>{1}</{0}>", "TransactionCode", row.Row.Item(2)))
                UploadBankStatementsDetailXml.Append(String.Format("<{0}>{1}</{0}>", "DescriptionTransaction", row.Row.Item(3)))
                UploadBankStatementsDetailXml.Append(String.Format("<{0}>{1}</{0}>", "ValueDebit", row.Row.Item(4)))
                UploadBankStatementsDetailXml.Append(String.Format("<{0}>{1}</{0}>", "ValueCredit", row.Row.Item(5)))
                UploadBankStatementsDetailXml.Append(String.Format("<{0}>{1}</{0}>", "BankCheck", row.Row.Item(6)))
                UploadBankStatementsDetailXml.Append(String.Format("<{0}>{1}</{0}>", "PaymentReferenceOne", row.Row.Item(7)))
                UploadBankStatementsDetailXml.Append(String.Format("<{0}>{1}</{0}>", "PaymentReferenceTwo", row.Row.Item(8)))
                UploadBankStatementsDetailXml.Append("</UploadBankStatementsDetail>")
            Next
            Dim result = _UploadBankStatementsRepository.SetUploadBankStatementsDetail(UploadBankStatementsDetailXml.ToString()).ToList()
            Dim listDetailsResult As New List(Of UploadBankStatementsDetail)
            listErros.AddRange(From e In result Where e.Status = 2 Select e.Message)
            For Each item In result.FindAll(Function(x) x.Status = 1)
                Dim UploadBankStatementsDetailDetail = New UploadBankStatementsDetail
                With UploadBankStatementsDetailDetail
                    .TransactionDate = item.TransactionDate
                    .ConsecutiveBank = item.ConsecutiveBank
                    .TransactionCode = item.TransactionCode
                    .DescriptionTransaction = item.DescriptionTransaction
                    .ValueDebit = item.ValueDebit
                    .ValueCredit = item.ValueCredit
                    .BankCheck = item.BankCheck
                    .PaymentReferenceOne = item.PaymentReferenceOne
                    .PaymentReferenceTwo = item.PaymentReferenceTwo
                End With
                listDetailsResult.Add(UploadBankStatementsDetailDetail)
            Next
            Return New ActionResult(Of List(Of UploadBankStatementsDetail)) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = listDetailsResult, .MessageResult = listErros}
        Catch ex As Exception
            Return New ActionResult(Of List(Of UploadBankStatementsDetail)) With {.StatusCode = eStatusResult.EXCEPTION, .MessageResult = listErros}
        End Try
    End Function


#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _TreasuryService.Dispose()
            End If
            _UploadBankStatementsRepository = Nothing
            _TreasuryService = Nothing
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
