'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 03-04-2014
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
Imports System.Transactions
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Payroll
Imports System.Text.RegularExpressions
Imports Application.Treasury

Public Class NoteConceptAdminService
    Implements INoteConceptAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de conceptos de nota
    ''' </summary>
    Private _noteConceptRepository As INoteConceptRepository

    ''' <summary>
    ''' repositorio de las secuencias numericas
    ''' </summary>
    Private _sequenceDRepository As ISequenseTreasuryDRepository

    Private Const FORM_NAME As String = "Conceptos de Nota"
    ''' <summary>
    ''' repositorio de terceros
    ''' </summary>
    Private _thirdPartyRepository As IThirdPartyRepository

    Private _costCenterRepository As ICostCenterRepository
    Private _pucRepository As IPUCRepository

#End Region

#Region "Methods"

    Public Sub New(ByVal noteConceptRepository As INoteConceptRepository, ByVal sequenceDRepository As ISequenseTreasuryDRepository,
                   ByVal thirdPartyRepository As IThirdPartyRepository, ByVal costCenter As ICostCenterRepository, ByVal puc As IPUCRepository)
        If noteConceptRepository Is Nothing Then
            Throw New ArgumentNullException("noteConceptRepository")
        End If
        If sequenceDRepository Is Nothing Then
            Throw New ArgumentNullException("sequenceDRepository")
        End If
        _noteConceptRepository = noteConceptRepository
        _sequenceDRepository = sequenceDRepository
        _thirdPartyRepository = thirdPartyRepository
        _costCenterRepository = costCenter
        _pucRepository = puc
    End Sub

    ''' <summary>
    ''' Deletes the note concept.
    ''' </summary>
    ''' <param name="noteConcept">The note concept.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">expenseConcept</exception>
    Public Function DeleteNoteConcept(noteConcept As NoteConcepts, audit As AuditMessage) As ActionResult Implements INoteConceptAdminService.DeleteNoteConcept
        If noteConcept Is Nothing Then
            Throw New ArgumentNullException("expenseConcept")
        End If
        Dim unitOfWork As IUnitWork = Me._noteConceptRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                noteConcept.ModificationUser = audit.CodeUser
                noteConcept.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of NoteConcepts)(noteConcept, audit, status)

                noteConcept.MarkAsDeleted()
                Me._noteConceptRepository.SaveEntity(noteConcept)
                unitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-999"}), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' actualiza el estado del registro
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function UpdateStateNoteConcept(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of NoteConcepts) Implements INoteConceptAdminService.UpdateStateNoteConcept
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
            Dim noteConcepts As NoteConcepts = Me._noteConceptRepository.GetNoteConcept(code.Trim())
            If noteConcepts IsNot Nothing AndAlso noteConcepts.Id > 0 Then
                noteConcepts.Status = state
            End If
            Dim result = Me.SaveNoteConcept(noteConcepts, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of NoteConcepts) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un concepto de nota
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">
    ''' code
    ''' or
    ''' audit
    ''' </exception>
    Public Function GetNoteConcept(code As String, audit As AuditMessage) As ActionResult(Of NoteConcepts) Implements INoteConceptAdminService.GetNoteConcept
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim noteConceptConcept As NoteConcepts = Me._noteConceptRepository.GetNoteConcept(code.Trim())
            If noteConceptConcept IsNot Nothing AndAlso noteConceptConcept.Id > 0 Then
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Print
                Dim auditProcess As New IndigoAuditSimpleEntity(Of NoteConcepts)(noteConceptConcept, audit, status)
                auditProcess.Execute()
            End If
            Return New ActionResult(Of NoteConcepts) With {.StateResult = True, .ObjectEmbbeded = noteConceptConcept}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of NoteConcepts) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un concepto de nota por id
    ''' </summary>
    Public Function GetNoteConceptById(Id As Integer, audit As AuditMessage) As NoteConcepts Implements INoteConceptAdminService.GetNoteConceptById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim noteConcept As NoteConcepts = Me._noteConceptRepository.GetNoteConceptById(Id)
            Return noteConcept
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Saves the note concept.
    ''' </summary>
    ''' <param name="noteConcept">The note concept.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">expenseConcept</exception>
    Public Function SaveNoteConcept(noteConcept As NoteConcepts, audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of NoteConcepts) Implements INoteConceptAdminService.SaveNoteConcept
        If noteConcept Is Nothing Then
            Throw New ArgumentNullException("expenseConcept")
        End If
        Dim unitOfWork As IUnitWork = Me._noteConceptRepository.UnitWork
        Dim sequenceUnitOfWork As IUnitWork = Me._sequenceDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(noteConcept.Code) Then
                    Dim seq As TreasurySequenceDetail = Me._sequenceDRepository.GetSequenseDById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.TreasurySequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            noteConcept.Code = res
                            seq.Next += 1
                            Me._sequenceDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of NoteConcepts) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.TreasurySequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), noteConcept.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of NoteConcepts) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxNoteConcept As NoteConcepts = Nothing
                Dim status As Integer
                If noteConcept.ChangeTracker.State = ObjectState.Added Then
                    noteConcept.CreationDate = Date.Now
                    noteConcept.CreationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    noteConcept.ModificationDate = Date.Now
                    noteConcept.ModificationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                    auxNoteConcept = noteConcept.OriginalValue
                End If

                Me._noteConceptRepository.SaveEntity(noteConcept)
                unitOfWork.Commit()
                sequenceUnitOfWork.Commit()
                Dim auditProcess As New IndigoAuditSimpleEntity(Of NoteConcepts)(noteConcept, audit, status, auxNoteConcept)
                auditProcess.Execute()
                'Se marca la entidad como sin cambios
                noteConcept.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of NoteConcepts) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = noteConcept, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of NoteConcepts) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of NoteConcepts) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function
    ''' <summary>
    ''' Validamos los campos
    ''' </summary>
    ''' <param name="DataImport"></param>
    ''' <returns></returns>
    Public Function ValidateNoteConcept(DataImport As List(Of ImportFileRow)) As ActionResult(Of List(Of TreasuryNoteDetail)) Implements INoteConceptAdminService.ValidateNoteConcept
        Dim errorList As New List(Of String)
        Dim treasuryNoteDetailList As New List(Of TreasuryNoteDetail)
        Try
            Dim listFilterErrors As New List(Of String)
            'Se valida que todas las celdas tengan el mismo numero que en la exportacion
            listFilterErrors = DataImport.Where(Function(d) d.Row.Count <> 5).Select(Function(d) String.Format("El registro {0} no tiene una estructura válida", (d.IndexRow).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                DataImport.RemoveAll(Function(d) d.Row.Count <= 5)
                errorList.AddRange(listFilterErrors)
            End If
            'Se valida que  exista conceptos
            Dim listConceptNoteCode = DataImport.Select(Function(d) d.Row.Item(0).ToString()).Distinct().ToList()
            Dim listConcepts = _noteConceptRepository.GetNoteConceptList(listConceptNoteCode)
            listFilterErrors = DataImport.Where(Function(d) Not listConcepts.Any(Function(c) c.Code = d.Row.Item(0))).Select(Function(d) String.Format(ResourceManager.GetString("ConceptNoteNotExist", "Treasury"), d.Row.Item(0), (d.IndexRow).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                DataImport.RemoveAll(Function(d) Not listConcepts.Any(Function(c) c.Code = d.Row.Item(0)))
                errorList.AddRange(listFilterErrors)
            End If
            'Cuentas contable
            Dim listMainAccount = _pucRepository.GetAllAcounts()

            Dim listConceptMainAccountIDs = listConcepts.Select(Function(c) c.IdMainAccount).Distinct().ToList()

            Dim validMaincAcounts = listMainAccount.Where(Function(a) listConceptMainAccountIDs.Contains(a.Id)).ToList()


            'Se valida que exista el tercero 
            Dim listNit = DataImport.Select(Function(d) d.Row.Item(1).ToString()).Distinct().ToList()
            Dim listThirdPartyNit = _thirdPartyRepository.ListAllThirdParty(listNit)
            listFilterErrors = DataImport.Where(Function(d) Not listThirdPartyNit.Any(Function(c) c.Nit = d.Row.Item(1))).Select(Function(d) String.Format(ResourceManager.GetString("ThirdPartyNotExist", "Treasury"), (d.Row.Item(1)).ToString(), (d.IndexRow).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                DataImport.RemoveAll(Function(d) Not listThirdPartyNit.Any(Function(c) c.Nit = d.Row.Item(1)))
                errorList.AddRange(listFilterErrors)
            End If

            'Validacion centro de costo
            Dim listCostCenter = DataImport.Select(Function(d) d.Row.Item(2).ToString()).Distinct.ToList
            Dim listCostCenterCode As New List(Of Domain.Payroll.Entities.CostCenter)
            If (listCostCenter.Any() AndAlso listCostCenter.Count() > 0) Then

                listCostCenterCode = _costCenterRepository.ListAllCostCenter()
                listCostCenterCode = listCostCenterCode.FindAll(Function(d) d.State).ToList()

                listFilterErrors = DataImport.Where(Function(d) Not listCostCenterCode.Any(Function(c) c.Code = d.Row.Item(2))).Select(Function(d) String.Format(ResourceManager.GetString("CostCenterCodeNotExist", "Treasury"), (d.IndexRow).ToString())).ToList()
                If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                    DataImport.RemoveAll(Function(d) Not listCostCenterCode.Any(Function(c) c.Code = d.Row.Item(2)))
                    errorList.AddRange(listFilterErrors)
                End If
            End If
            'se valida que la naturaleza sea numero y no este vacia
            listFilterErrors = DataImport.Where(Function(d) Not IsNumeric(d.Row.Item(3))).Select(Function(d) String.Format("La naturaleza del item {0} no es numerica", (d.IndexRow).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                DataImport.RemoveAll(Function(d) Not IsNumeric(d.Row.Item(3)))
                errorList.AddRange(listFilterErrors)
            End If
            'Se valida que el valor ingresado sea numerico 

            listFilterErrors = DataImport.Where(Function(d) Not Regex.IsMatch(d.Row.Item(4).ToString(), "^-?\d{1,16}(\.\d{1,2})?$")
                                                ).Select(Function(d) String.Format(ResourceManager.GetString("ValueNotNumeric", "Portfolio"), d.IndexRow.ToString())).ToList()

            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                DataImport.RemoveAll(Function(d) Not IsNumeric(d.Row.Item(4)))
                errorList.AddRange(listFilterErrors)
            End If


            If DataImport.Any() Then

                For Each row In DataImport
                    Dim _treasuryNoteDetail As New TreasuryNoteDetail
                    '------------------------------------------------
                    Dim indexRow As Integer = row.IndexRow
                    Dim costCenterCodeValue As String = Nothing
                    ' Obtiener el código del concepto
                    Dim conceptCode As String = row.Row.Item(0).ToString()



                    'Verificar si existe el concepto (ya validado antes)
                    Dim concept = listConcepts.FirstOrDefault(Function(d) d.Code = conceptCode)
                    If concept.Nature <> row.Row.Item(3) Then
                        Dim natureError = $"La naturaleza ingresada no coincide con el concepto. Registro: {indexRow}"
                        listFilterErrors.Add(natureError)
                    End If

                    If concept IsNot Nothing Then

                        'Verificar la cuenta contable asociada al concepto
                        Dim mainAcct = validMaincAcounts.FirstOrDefault(Function(a) a.Id = concept.IdMainAccount)

                        If mainAcct IsNot Nothing Then
                            'Si la cuenta maneja centro de costo
                            If mainAcct.HandlesCostCenter And row.Row.Item(2) IsNot Nothing Then

                                'Obtén el centro de costo desde la fila
                                costCenterCodeValue = row.Row.Item(2).ToString()

                            Else
                                If Not mainAcct.HandlesCostCenter Then
                                    ' Error si la cuenta contable no maneja centro de costos
                                    Dim costCenterError As String = $"La cuenta contable {mainAcct.Number} no maneja centro de costos. Registro: {indexRow}"
                                    listFilterErrors.Add(costCenterError)
                                ElseIf row.Row.Item(2) Is Nothing Then
                                    ' Error si el centro de costo esta vacio y maneja centro de costo
                                    Dim cosCenterError As String = $"La cuenta contable {mainAcct.Number} maneja centro de costo y esta vacio. Registro: {indexRow}"
                                    listFilterErrors.Add(cosCenterError)
                                End If

                            End If

                        End If
                        If listFilterErrors.Any() Then
                            errorList.AddRange(listFilterErrors)
                        End If

                    End If

                    '------------------------------------------------
                    With _treasuryNoteDetail
                        .NoteConceptId = listConcepts.FirstOrDefault(Function(d) d.Code = row.Row.Item(0)).Id
                        .MainAccountId = listConcepts.FirstOrDefault(Function(d) d.Code = row.Row.Item(0)).IdMainAccount
                        .NoteConceptCode = row.Row.Item(0)
                        .NoteConceptName = listConcepts.FirstOrDefault(Function(d) d.Code = row.Row.Item(0)).Description
                        .ThirdPartyId = listThirdPartyNit.FirstOrDefault(Function(d) d.Nit = row.Row.Item(1)).Id
                        .CostCenterId = If(row.Row.Item(2) = Nothing, costCenterCodeValue, listCostCenterCode.FirstOrDefault(Function(d) d.Code = costCenterCodeValue).Id)
                        .Nature = listConcepts.FirstOrDefault(Function(d) d.Code = row.Row.Item(0)).Nature
                        .FullNameCostCenter = If(row.Row.Item(2) = Nothing, "", listCostCenterCode.FirstOrDefault(Function(d) d.Code = row.Row.Item(2)).Name)
                        .FullNameMainAccount = listMainAccount.FirstOrDefault(Function(d) d.Id = .MainAccountId).Name
                        .FullNameNature = If(listConcepts.FirstOrDefault(Function(d) d.Code = row.Row.Item(0)).Nature = 1, "Débito", "Crédito")
                        .Value = row.Row.Item(4)
                    End With
                    treasuryNoteDetailList.Add(_treasuryNoteDetail)
                Next
                'validacion
                Return New ActionResult(Of List(Of TreasuryNoteDetail)) With {.ObjectEmbbeded = treasuryNoteDetailList, .MessageResult = errorList}
            Else
                Return New ActionResult(Of List(Of TreasuryNoteDetail)) With {.ObjectEmbbeded = treasuryNoteDetailList, .MessageResult = errorList}
            End If
        Catch ex As Exception
            errorList.Add(Utils.GetInnerExceptionMessageToString(ex))
            Return New ActionResult(Of List(Of TreasuryNoteDetail)) With {.ObjectEmbbeded = treasuryNoteDetailList, .MessageResult = errorList}

            Throw ex
        End Try
    End Function

    Public Function CopyPasteNoteConceptDetail(Data As List(Of List(Of String))) As ActionResult(Of List(Of TreasuryNoteDetail)) Implements INoteConceptAdminService.CopyPasteNoteConceptDetail
        Dim errorList As New List(Of String)
        Dim treasuryNoteDetailList As New List(Of TreasuryNoteDetail)
        Try
            For i As Integer = 0 To Data.Count - 1 Step 1
                'Validar que el concepto Exista
                Dim concept = _noteConceptRepository.GetNoteConcept(Data.Item(i).Item(0))
                If concept.Id = 0 Then
                    errorList.Add(String.Format(ResourceManager.GetString("NotExistConceptCopyPaste", "Treasury"), Data.Item(i).Item(0).ToString(), (i + 1).ToString()))
                    Continue For
                End If
                'Validar que el tercero exista 
                Dim thirdParthy = _thirdPartyRepository.GetThirdPartyByNit(Data.Item(i).Item(1))
                If thirdParthy.Id = 0 Then
                    errorList.Add(String.Format(ResourceManager.GetString("ThirdPartyNotExist", "Treasury"), Data.Item(i).Item(1).ToString(), (i + 1).ToString()))
                    Continue For
                End If
                'Validar el centro de costo

                Dim mainAccount = _pucRepository.GetAccountById(concept.IdMainAccount, False)
                Dim costCenterId = Nothing
                Dim costCenterName As String = Nothing
                Dim costCenterError As String
                If Not String.IsNullOrEmpty(Data.Item(i).Item(2).ToString()) Then
                    If mainAccount.HandlesCostCenter Then
                        Dim costCenter = _costCenterRepository.GetCostCenter(Data.Item(i).Item(2).ToString())
                        If costCenter IsNot Nothing Then
                            costCenterId = costCenter.Id
                            costCenterName = costCenter.Name
                        Else
                            costCenterError = String.Format("La cuenta contable registrada al concepto no existe. Registro: ", (i + 1).ToString())
                            errorList.Add(costCenterError)
                            Continue For
                        End If
                    Else
                        costCenterError = String.Format("La cuenta contable no maneja centro de costo", (i + 1).ToString())
                        errorList.Add(costCenterError)
                        Continue For
                    End If

                End If

                'Validar la naturaleza 
                If Not IsNumeric(Data.Item(i).Item(3)) Then
                    errorList.Add(String.Format(ResourceManager.GetString("ValueNotNumeric", "Portfolio"), (i + 1).ToString()))
                    Continue For
                End If
                If concept.Nature <> Data.Item(i).Item(3) Then
                    Dim natureError = String.Format("La naturaleza no coincide con el concepto registrado", (i + 1).ToString())
                    errorList.Add(natureError)
                    Continue For
                End If
                'Validar el valor 
                If Not IsNumeric(Data.Item(i).Item(4)) OrElse Not Regex.IsMatch(Data.Item(i).Item(4).ToString(), "^-?\d{1,16}(\,\d{1,2})?$") Then
                    errorList.Add("El valor " + (i + 1).ToString() + " no es un número válido")
                    Continue For
                End If
                Dim _treasuryNoteDetail As New TreasuryNoteDetail
                With _treasuryNoteDetail
                    .NoteConceptId = concept.Id
                    .MainAccountId = mainAccount.Id
                    .NoteConceptCode = Data.Item(i).Item(0).ToString()
                    .NoteConceptName = concept.Description
                    .ThirdPartyId = thirdParthy.Id
                    .CostCenterId = If(Data.Item(i).Item(2).ToString() = Nothing, Nothing, costCenterId)
                    .Nature = Data.Item(i).Item(3).ToString()
                    .FullNameCostCenter = If(Data.Item(i).Item(2) = Nothing, "", costCenterName)
                    .FullNameMainAccount = mainAccount?.Name
                    .FullNameNature = If(concept.Nature = 1, "Débito", "Crédito")
                    .Value = Data.Item(i).Item(4).ToString()

                End With
                treasuryNoteDetailList.Add(_treasuryNoteDetail)
            Next
            Return New ActionResult(Of List(Of TreasuryNoteDetail)) With {.ObjectEmbbeded = treasuryNoteDetailList, .MessageResult = errorList}

        Catch ex As Exception
            errorList.Add(Utils.GetInnerExceptionMessageToString(ex))
            Return New ActionResult(Of List(Of TreasuryNoteDetail)) With {.ObjectEmbbeded = treasuryNoteDetailList, .MessageResult = errorList}
        End Try

    End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _noteConceptRepository = Nothing
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
