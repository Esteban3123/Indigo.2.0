Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Transactions
Imports Infrastructure.CrossCutting.Resources
Imports System.Text
Imports Application.Payroll

Public Class BankAdminService
    Implements IBankAdminService

    'Repositorio de bancos
    Private _BankRepository As IBankRepository
    Private _secuenseDRepository As Domain.Entities.ISequenseTreasuryDRepository
    Private Const FORM_NAME As String = "Banco"

    ''' <summary>
    ''' inicia el repositorio de bancos
    ''' </summary>
    ''' <param name="bankRepository">Repositorio de bancos</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal bankRepository As IBankRepository, secuenseDRepository As Domain.Entities.ISequenseTreasuryDRepository)
        If (bankRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de bancos vacio")
        End If
        _BankRepository = bankRepository
        _secuenseDRepository = secuenseDRepository
    End Sub

    ''' <summary>
    ''' Elimina un banco
    ''' </summary>
    ''' <param name="bank">Banco</param>
    ''' <returns></returns>
    Public Function DeleteBank(bank As Bank, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionMessageResult(Of Bank) Implements IBankAdminService.DeleteBank
        Dim result As New ActionMessageResult(Of Bank)
        result.StateResult = True
        If bank Is Nothing Then
            Throw New ArgumentNullException("Banco vacio")
        End If
        Dim unitWork As IUnitWork = _BankRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                bank.ModificationUser = audit.CodeUser
                bank.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of Bank)(bank, audit, status)

                bank.MarkAsDeleted()
                Me._BankRepository.SaveEntity(bank)
                unitWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionMessageResult(Of Bank) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}

            End Using
        Catch ex As DbUpdateException
            unitWork.RollbackChanges()
            result.StateResult = False
            result.StatusCode = eStatusResult.WARNING
            result.Message = ResourceManager.GetString("ErrorDependence")
            result.MessageResult.Add(New MessageResult("c-0000", bank.Code))
            Return result
        Catch ex As Exception
            unitWork.RollbackChanges()
            result.StatusCode = eStatusResult.EXCEPTION
            result.StateResult = False
            result.Message = IndigoManagementExceptions.GetExceptionDetails(ex)
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return result
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un Banco especifico
    ''' </summary>
    ''' <param name="code">Código de el banco</param>
    ''' <returns> Banco</returns>
    Public Function GetBank(code As String, ByVal audit As AuditMessage) As Bank Implements IBankAdminService.GetBank
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Codigo vacio")
        End If
        Try
            Dim Bank As Bank = _BankRepository.GetBank(code)
            If Bank IsNot Nothing AndAlso Bank.Id > 0 Then
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Print
                Dim auditProcess As New IndigoAuditSimpleEntity(Of Bank)(Bank, audit, status)
                auditProcess.Execute()
            End If
            Return Bank
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New Bank()
        End Try
    End Function

    ''' <summary>
    ''' Lista todos los bancos
    ''' </summary>
    ''' <returns>Lista de bancos</returns>
    Public Function ListAllBank() As List(Of Bank) Implements IBankAdminService.ListAllBank
        Try
            Return _BankRepository.ListAllBank()

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda o edita un banco
    ''' </summary>
    ''' <param name="bank">Banco</param>
    ''' <returns></returns>
    Public Function SaveBank(bank As Bank, audit As Infrastructure.CrossCutting.Base.AuditMessage, Optional idSequence As Long = 0) As ActionResult(Of Bank) Implements IBankAdminService.SaveBank
        If bank Is Nothing Then
            Throw New ArgumentNullException("Banco vacio")
        End If
        Dim unitWork As IUnitWork = _BankRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If bank.Code Is Nothing OrElse bank.Code.Trim().Equals(String.Empty) Then
                    Dim seq As Domain.Entities.TreasurySequenceDetail = Me._secuenseDRepository.GetSequenseDById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.TreasurySequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            bank.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of Bank) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.TreasurySequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), bank.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of Bank) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As Bank = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of Bank)
                Dim status As Integer

                If bank.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    bank.CreationUser = audit.CodeUser
                    bank.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = _BankRepository.GetBank(bank.Code, False)
                    bank.ModificationUser = audit.CodeUser
                    bank.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                _BankRepository.SaveEntity(bank)
                unitWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of Bank)(bank, audit, status, auxObjEntity)
                auditProcess.Execute()
                bank.MarkAsUnchanged()
                scope.Complete()

                Return New ActionResult(Of Bank) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = bank, .Message = MessageResult}
            End Using
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Bank) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function UpdateStateBank(code As String, state As Boolean, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of Bank) Implements IBankAdminService.UpdateStateBank
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If String.IsNullOrEmpty(state) Then
            Throw New ArgumentNullException("state")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim bank As Bank = Me._BankRepository.GetBank(code.Trim())
            If bank IsNot Nothing AndAlso bank.Id > 0 Then
                bank.State = state
            End If
            Dim result = Me.SaveBank(bank, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Bank) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un banco por Id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Codigo vacio</exception>
    Public Function GetBankById(Id As Integer) As Bank Implements IBankAdminService.GetBankById
        If String.IsNullOrEmpty(Id) Then
            Throw New ArgumentNullException("Codigo vacio")
        End If
        Try
            Return _BankRepository.GetBankById(Id)

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New Bank()
        End Try
    End Function

    Public Function SetCopyPasteOrImportFileBankDetail(dataImportFile As List(Of ImportFileRow), dataCopyPaste As List(Of List(Of String))) As ActionResult(Of List(Of BankDetail)) Implements IBankAdminService.SetCopyPasteOrImportFileBankDetail
        If dataImportFile IsNot Nothing Then
            Return SetImportFileBankDetail(dataImportFile)
        Else
            Return SetCopyPasteBankDetail(dataCopyPaste)
        End If
    End Function

    Private Function SetCopyPasteBankDetail(dataCopyPaste As List(Of List(Of String))) As ActionResult(Of List(Of BankDetail))
        Try
            Dim invoiceXml As New StringBuilder

            For i As Integer = 0 To dataCopyPaste.Count - 1 Step 1
                invoiceXml.Append("<BankDetail>")
                invoiceXml.Append(String.Format("<{0}>{1}</{0}>", "Code", dataCopyPaste.Item(i).Item(0)))
                invoiceXml.Append(String.Format("<{0}>{1}</{0}>", "ExtractCode", dataCopyPaste.Item(i).Item(1)))
                invoiceXml.Append(String.Format("<{0}>{1}</{0}>", "Detail", dataCopyPaste.Item(i).Item(2)))
                invoiceXml.Append("</BankDetail>")
            Next
            Dim result = _BankRepository.SetBankDetail(invoiceXml.ToString()).ToList()
            Dim listDetailsResult As New List(Of BankDetail)
            Dim listErros As New List(Of String)
            For Each item In result
                Dim BankDetailDetail = New BankDetail
                With BankDetailDetail
                    .BankConciliationConceptsId = item.BankConciliationConceptsId
                    .Code = item.Code
                    .Name = item.Name
                    .ExtractCode = item.ExtractCode
                    .Detail = item.Detail
                End With
                listDetailsResult.Add(BankDetailDetail)
            Next
            Return New ActionResult(Of List(Of BankDetail)) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = listDetailsResult, .MessageResult = listErros}
        Catch ex As Exception
            Return New ActionResult(Of List(Of BankDetail)) With {.StatusCode = eStatusResult.EXCEPTION}
        End Try
    End Function

    Private Function SetImportFileBankDetail(data As List(Of ImportFileRow)) As ActionResult(Of List(Of BankDetail))
        Try
            Dim invoiceXml As New StringBuilder

            For Each row In data
                invoiceXml.Append("<BankDetail>")
                invoiceXml.Append(String.Format("<{0}>{1}</{0}>", "Code", row.Row.Item(0)))
                invoiceXml.Append(String.Format("<{0}>{1}</{0}>", "ExtractCode", row.Row.Item(1)))
                invoiceXml.Append(String.Format("<{0}>{1}</{0}>", "Detail", row.Row.Item(1)))
                invoiceXml.Append("</BankDetail>")
            Next

            Dim result = _BankRepository.SetBankDetail(invoiceXml.ToString()).ToList()
            Dim listDetailsResult As New List(Of BankDetail)
            Dim listErros As New List(Of String)
            For Each item In result
                Dim BankDetailDetail = New BankDetail
                With BankDetailDetail
                    .BankConciliationConceptsId = item.BankConciliationConceptsId
                    .Code = item.Code
                    .Name = item.Name
                    .ExtractCode = item.ExtractCode
                    .Detail = item.Detail
                End With
                listDetailsResult.Add(BankDetailDetail)
            Next

            Return New ActionResult(Of List(Of BankDetail)) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = listDetailsResult, .MessageResult = listErros}
        Catch ex As Exception
            Return New ActionResult(Of List(Of BankDetail)) With {.StatusCode = eStatusResult.EXCEPTION}
        End Try
    End Function

    ''' <summary>
    ''' Importa la información del File o cuando es CopyPaste
    ''' </summary>
    ''' <param name="dataimport">Banco</param>    ''' 
    ''' <param name="data">Banco</param>
    ''' <returns></returns>
    Public Function SetBankAutomaticRecognitionRulesFromFIle(dataimport As List(Of ImportFileRow), data As List(Of List(Of String))) As ActionResult(Of List(Of BankAutomaticRecognitionRules)) Implements IBankAdminService.SetBankAutomaticRecognitionRulesFromFIle
        Try
            'Objeto xml
            Dim xmlObject As String = String.Empty

            'listado de datos copiados o importados que devuelven ala rejilla de productos
            Dim ListBankAutomaticRecognitionRules As New List(Of BankAutomaticRecognitionRules)

            'listado de registro con errores
            Dim ListRecordsErrors As New List(Of String())


            'Listado de errores
            Dim listErrors As New List(Of String)

            If dataimport IsNot Nothing Then
                xmlObject = ConvertToXmlImportFile(dataimport)
            Else
                xmlObject = ConvertToXmlCopyPaste(data)
            End If

            Dim resultStore = Me._BankRepository.SetBankAutomaticRecognitionRules(xmlObject)

            'se crea los objectos para devolver y pegar en la rejilla
            If resultStore IsNot Nothing AndAlso resultStore.Count > 0 Then
                For Each itemXml In resultStore
                    If itemXml.StatusField = 0 Then

                        'Se crea el nuevo objecto para agregarlo al listado
                        ListBankAutomaticRecognitionRules.Add(New BankAutomaticRecognitionRules With
                 {
                .Code = itemXml.CodeBankAutomaticRecognitionRules,
                .DescriptionTransaction = itemXml.DescriptionTransaction,
                .NoteConceptsId = itemXml.NoteConceptId,
                .NoteConceptCodeName = itemXml.NoteConceptCode,
                .MainAccountsId = itemXml.MainAccountId,
                .AccountingAccountNumberName = itemXml.MainAccountNumberName,
                .CostCenterId = itemXml.CostCenterId,
                .CostCenterCodeName = itemXml.CostCenterCode
                })

                    Else 'Si el estado del item es false y no paso alguna validacion

                        If itemXml.CodeBankAutomaticRecognitionRules IsNot Nothing Then
                            If itemXml.CodeBankAutomaticRecognitionRules.Contains("-") Then
                                Dim _Code = itemXml.CodeBankAutomaticRecognitionRules.Split("-")
                                itemXml.CodeBankAutomaticRecognitionRules = _Code(0)
                            End If
                        End If

                        Dim datos As String() = {itemXml.CodeBankAutomaticRecognitionRules, itemXml.NoteConceptCode, itemXml.CostCenterCode,
                                              itemXml.MessageField}

                        ListRecordsErrors.Add(datos)
                        listErrors.Add(itemXml.MessageField)
                    End If
                Next

            End If


            'Se devuelve el mensaje
            Return New ActionResult(Of List(Of BankAutomaticRecognitionRules)) With {.StateResult = True, .ObjectEmbbeded = ListBankAutomaticRecognitionRules, .MessageResult = listErrors, .ListMessageResult = ListRecordsErrors}

        Catch ex As SqlClient.SqlException
            Return New ActionResult(Of List(Of BankAutomaticRecognitionRules)) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        Catch ex As Exception
            Return New ActionResult(Of List(Of BankAutomaticRecognitionRules)) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Construye el XML con la informacion del File
    ''' </summary>
    ''' <param name="data">Banco</param>
    ''' <returns></returns>
    Private Function ConvertToXmlImportFile(data As List(Of ImportFileRow))
        Dim builder As StringBuilder = New StringBuilder()

        builder.Append("<Data>")

        For Each item In data

            Dim indexRow = item.IndexRow
            Dim Columns = item.Row.Count

            builder.Append("<Row>")

            builder.Append("<RowIndex>" & indexRow & "</RowIndex>")
            builder.Append("<RowColumns>" & Columns & "</RowColumns>")
            builder.Append("<CodeBankAutomaticRecognitionRules>" & If(Columns > 0, item.Row.Item(0), String.Empty) & "</CodeBankAutomaticRecognitionRules>")
            builder.Append("<DescriptionTransaction>" & If(Columns > 1, item.Row.Item(1), String.Empty) & "</DescriptionTransaction>")
            builder.Append("<NoteConceptCode>" & If(Columns > 2, item.Row.Item(2), String.Empty) & "</NoteConceptCode>")
            builder.Append("<CostCenterCode>" & If(Columns > 3, item.Row.Item(3), String.Empty) & "</CostCenterCode>")

            builder.Append("</Row>")

        Next
        builder.Append("</Data>")
        Return builder.ToString

    End Function

    ''' <summary>
    ''' Construye el XML con la informacion del CopyPaste
    ''' </summary>
    ''' <param name="data">Banco</param>
    ''' <returns></returns>
    Private Function ConvertToXmlCopyPaste(data As List(Of List(Of String)))
        Dim builder As StringBuilder = New StringBuilder()

        builder.Append("<Data>")

        Dim indexRow As Integer = 0
        For Each item In data

            indexRow = indexRow + 1
            Dim Columns = item.Count

            builder.Append("<Row>")

            builder.Append("<RowIndex>" & indexRow & "</RowIndex>")
            builder.Append("<RowColumns>" & Columns & "</RowColumns>")
            builder.Append("<CodeBankAutomaticRecognitionRules>" & If(Columns > 0, item(0), String.Empty) & "</CodeBankAutomaticRecognitionRules>")
            builder.Append("<DescriptionTransaction>" & If(Columns > 1, item(1), String.Empty) & "</DescriptionTransaction>")
            builder.Append("<NoteConceptCode>" & If(Columns > 2, item(2), String.Empty) & "</NoteConceptCode>")
            builder.Append("<CostCenterCode>" & If(Columns > 3, item(3), String.Empty) & "</CostCenterCode>")

            builder.Append("</Row>")

        Next
        builder.Append("</Data>")

        Return builder.ToString
    End Function


#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _BankRepository = Nothing
            _secuenseDRepository = Nothing
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
