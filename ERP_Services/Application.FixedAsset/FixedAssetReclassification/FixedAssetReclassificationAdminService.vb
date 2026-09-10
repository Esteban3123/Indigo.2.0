#Region "Imports"
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports System.Data.Entity.Core
Imports System.Transactions
Imports System.Text
Imports Domain.Entities.Service
Imports Application.Payments
Imports Application.Accounting
Imports Application.Portfolio

#End Region

Public Class FixedAssetReclassificationAdminService
    Implements IFixedAssetReclassificationAdminService

#Region "Variables"

    'Repositorio de la secuencia
    Private _sequenceRepository As IFixedAssetSequenceDetailRepository

    'Repositorio de la reclasificación
    Private _fixedAssetReclassificationRepository As IFixedAssetReclassificationRepository

#End Region

#Region "Builder"

    ''' <summary>
    ''' inicia los repositorio
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(fixedAssetReclassificationRepository As IFixedAssetReclassificationRepository, sequenceRepository As IFixedAssetSequenceDetailRepository)
        If sequenceRepository Is Nothing Then
            Throw New ArgumentNullException("sequenceRepository")
        End If
        If fixedAssetReclassificationRepository Is Nothing Then
            Throw New ArgumentNullException("fixedAssetReclassificationRepository")
        End If
        _sequenceRepository = sequenceRepository
        _fixedAssetReclassificationRepository = fixedAssetReclassificationRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene un registro por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetReclassificationById(Id As Integer, audit As AuditMessage) As ActionResult(Of FixedAssetReclassification) Implements IFixedAssetReclassificationAdminService.GetFixedAssetReclassificationById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim FixedAssetReclassification As FixedAssetReclassification = Me._fixedAssetReclassificationRepository.GetFixedAssetReclassificationById(Id)
            If FixedAssetReclassification IsNot Nothing AndAlso FixedAssetReclassification.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of FixedAssetReclassification)(FixedAssetReclassification, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of FixedAssetReclassification) With {.StateResult = True, .ObjectEmbbeded = FixedAssetReclassification}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FixedAssetReclassification) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un registro por código
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetReclassificationByCode(Code As String, audit As AuditMessage) As ActionResult(Of FixedAssetReclassification) Implements IFixedAssetReclassificationAdminService.GetFixedAssetReclassificationByCode
        If String.IsNullOrEmpty(Code) Then
            Throw New ArgumentNullException("Code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim FixedAssetReclassification As FixedAssetReclassification = Me._fixedAssetReclassificationRepository.GetFixedAssetReclassificationByCode(Code.Trim())
            If FixedAssetReclassification IsNot Nothing AndAlso FixedAssetReclassification.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of FixedAssetReclassification)(FixedAssetReclassification, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of FixedAssetReclassification) With {.StateResult = True, .ObjectEmbbeded = FixedAssetReclassification}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FixedAssetReclassification) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda un registro
    ''' </summary>
    ''' <param name="FixedAssetReclassification"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveFixedAssetReclassification(FixedAssetReclassification As FixedAssetReclassification, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of FixedAssetReclassification) Implements IFixedAssetReclassificationAdminService.SaveFixedAssetReclassification
        If FixedAssetReclassification Is Nothing Then
            Throw New ArgumentNullException("FixedAssetReclassification")
        End If

        If FixedAssetReclassification.FixedAssetReclassificationDetail Is Nothing OrElse FixedAssetReclassification.FixedAssetReclassificationDetail.Where(Function(d) d.ChangeTracker.State <> ObjectState.Deleted).Count() = 0 Then
            Return New ActionResult(Of FixedAssetReclassification) With {.StateResult = False, .Message = "No se han agregado activos fijos a reclasificar ", .StatusCode = eStatusResult.WARNING}
        End If

        If FixedAssetReclassification.FixedAssetReclassificationDetail.Where(Function(d) d.ChangeTracker.State <> ObjectState.Deleted AndAlso (d.FixedAssetReclassificationDetailBook Is Nothing OrElse d.FixedAssetReclassificationDetailBook.Where(Function(b) b.ChangeTracker.State <> ObjectState.Deleted).Count() = 0)).Count() > 0 Then
            Return New ActionResult(Of FixedAssetReclassification) With {.StateResult = False, .Message = "Existen activos a los que no se le han agregado detalles a reclasificar", .StatusCode = eStatusResult.WARNING}
        End If

        Dim sequenseUnitOfWork As IUnitWork = Me._sequenceRepository.UnitWork
        Dim unitOfWork As IUnitWork = Me._fixedAssetReclassificationRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim seq As FixedAssetSequenceDetail = Nothing
                If FixedAssetReclassification.Code Is Nothing OrElse FixedAssetReclassification.Code.Trim().Equals(String.Empty) Then
                    seq = Me._sequenceRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.FixedAssetSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            FixedAssetReclassification.Code = res
                            seq.Next += 1
                            Me._sequenceRepository.SaveEntity(seq)
                        Else
                            transaction.Dispose()
                            Return New ActionResult(Of FixedAssetReclassification) With {.StateResult = False, .Message = "El rango de la secuencia numérica ya se excedió", .StatusCode = eStatusResult.WARNING}
                        End If
                    Else
                        transaction.Dispose()
                        Return New ActionResult(Of FixedAssetReclassification) With {.StateResult = False, .Message = "El formulario no tiene parametrizada la secuencia numérica", .StatusCode = eStatusResult.WARNING}
                    End If
                End If

                Dim auxFixedAssetReclassification As FixedAssetReclassification = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of FixedAssetReclassification)
                Dim status As Integer

                If FixedAssetReclassification.ChangeTracker.State = ObjectState.Added Then
                    FixedAssetReclassification.CreationUser = audit.CodeUser
                    FixedAssetReclassification.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                ElseIf FixedAssetReclassification.ChangeTracker.State = ObjectState.Modified Then
                    auxFixedAssetReclassification = FixedAssetReclassification.OriginalValue
                    If FixedAssetReclassification.Status = 1 Then
                        FixedAssetReclassification.ModificationUser = audit.CodeUser
                        FixedAssetReclassification.ModificationDate = DateTime.Now
                        status = Infrastructure.CrossCutting.Audit.Actions.Update
                    End If
                    If FixedAssetReclassification.Status = 2 Then
                        FixedAssetReclassification.ModificationUser = audit.CodeUser
                        FixedAssetReclassification.ModificationDate = DateTime.Now
                        FixedAssetReclassification.ConfirmationUser = audit.CodeUser
                        FixedAssetReclassification.ConfirmationDate = DateTime.Now
                        status = Infrastructure.CrossCutting.Audit.Actions.Confirm
                    End If
                    If FixedAssetReclassification.Status = 3 Then
                        FixedAssetReclassification.ModificationUser = audit.CodeUser
                        FixedAssetReclassification.ModificationDate = DateTime.Now
                        FixedAssetReclassification.AnnulmentUser = audit.CodeUser
                        FixedAssetReclassification.AnnulmentDate = DateTime.Now
                        status = Infrastructure.CrossCutting.Audit.Actions.Annular
                    End If
                End If

                Me._fixedAssetReclassificationRepository.SaveEntity(FixedAssetReclassification)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of FixedAssetReclassification)(FixedAssetReclassification, audit, status, auxFixedAssetReclassification)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                FixedAssetReclassification.MarkAsUnchanged()

                transaction.Complete()
                Return New ActionResult(Of FixedAssetReclassification) With {.StateResult = True, .ObjectEmbbeded = FixedAssetReclassification, .StatusCode = eStatusResult.SUCCESS}
            Catch ex As OptimisticConcurrencyException
                transaction.Dispose()
                unitOfWork.RollbackChanges()
                Return New ActionResult(Of FixedAssetReclassification) With {.StateResult = False, .Message = "Error de concurrencia", .StatusCode = eStatusResult.EXCEPTION}
            Catch ex As Exception
                transaction.Dispose()
                unitOfWork.RollbackChanges()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")

                Dim message = ex.Message
                If ex.InnerException IsNot Nothing AndAlso Not String.IsNullOrEmpty(ex.InnerException.Message) Then
                    message = ex.InnerException.Message
                    If ex.InnerException.InnerException IsNot Nothing AndAlso Not String.IsNullOrEmpty(ex.InnerException.InnerException.Message) Then
                        message = ex.InnerException.InnerException.Message
                    End If
                End If

                Return New ActionResult(Of FixedAssetReclassification) With {.StateResult = False, .Message = message, .StatusCode = eStatusResult.EXCEPTION}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Confirma un registro
    ''' </summary>
    ''' <param name="FixedAssetReclassification"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConfirmFixedAssetReclassification(FixedAssetReclassification As FixedAssetReclassification, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of FixedAssetReclassification) Implements IFixedAssetReclassificationAdminService.ConfirmFixedAssetReclassification
        If FixedAssetReclassification Is Nothing Then
            Throw New ArgumentNullException("FixedAssetReclassification")
        End If

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                'Se guarda o actualiza el cambio de placa en sus respectivas tablas
                Dim resultSave As ActionResult(Of FixedAssetReclassification) = SaveFixedAssetReclassification(FixedAssetReclassification, audit, idSequense)
                If resultSave.StateResult = False Then
                    transaction.Dispose()
                    If resultSave.StatusCode = eStatusResult.EXCEPTION Then
                        Return New ActionResult(Of FixedAssetReclassification) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = resultSave.Message}
                    ElseIf resultSave.StatusCode = eStatusResult.WARNING Then
                        Return New ActionResult(Of FixedAssetReclassification) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = resultSave.Message}
                    End If
                End If

                FixedAssetReclassification = resultSave.ObjectEmbbeded

                'Listado de errores
                Dim ListErrors As New StringBuilder
                'Se consume el sp creado
                Dim resultSpReclassification = _fixedAssetReclassificationRepository.SP_GenerateJournalVoucherByFixedAssetReclassification(FixedAssetReclassification.Id, audit.CodeUser)
                If resultSpReclassification IsNot Nothing AndAlso resultSpReclassification.Count > 0 AndAlso (From x In resultSpReclassification Where x.CodeMessage = 999 Select x).Count > 0 Then
                    resultSpReclassification.ForEach(Sub(x) IIf(x.CodeMessage = 999, ListErrors.AppendLine(x.Message), ""))
                    transaction.Dispose()
                    Return New ActionResult(Of FixedAssetReclassification) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ListErrors.ToString}
                End If

                transaction.Complete()
                Return New ActionResult(Of FixedAssetReclassification) With {.StateResult = True, .ObjectEmbbeded = FixedAssetReclassification, .StatusCode = eStatusResult.SUCCESS, .Message = resultSpReclassification.FirstOrDefault().Message}
            Catch ex As OptimisticConcurrencyException
                transaction.Dispose()
                Return New ActionResult(Of FixedAssetReclassification) With {.StateResult = False, .Message = "Error de concurrencia", .StatusCode = eStatusResult.EXCEPTION}
            Catch ex As Exception
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of FixedAssetReclassification) With {.StateResult = False, .Message = ex.Message, .StatusCode = eStatusResult.EXCEPTION}
            End Try
        End Using
    End Function

    Private Function ConvertToXml(FixedAssetReclassification As FixedAssetReclassification)
        Dim builder As StringBuilder = New StringBuilder()

        builder.Append("<FixedAssetReclassification>")

        builder.Append("<Id>" & FixedAssetReclassification.Id & "</Id>")
        builder.Append("<Code>" & FixedAssetReclassification.Code & "</Code>")
        builder.Append("<DocumentDate>" & FixedAssetReclassification.DocumentDate.ToString("dd/MM/yyyy HH:mm") & "</DocumentDate>")
        builder.Append("<ReclassificationType>" & FixedAssetReclassification.ReclassificationType & "</ReclassificationType>")
        builder.Append("<Detail>" & FixedAssetReclassification.Detail & "</Detail>")
        builder.Append("<Status>" & FixedAssetReclassification.Status & "</Status>")
        builder.Append("<ItemIdPrevious>" & FixedAssetReclassification.ItemIdPrevious & "</ItemIdPrevious>")
        builder.Append("<ItemCatalogIdPrevious>" & FixedAssetReclassification.ItemCatalogIdPrevious & "</ItemCatalogIdPrevious>")
        builder.Append("<ItemId>" & FixedAssetReclassification.ItemId & "</ItemId>")
        builder.Append("<ItemCatalogId>" & FixedAssetReclassification.ItemCatalogId & "</ItemCatalogId>")

        For Each FixedAssetReclassificationDetail In FixedAssetReclassification.FixedAssetReclassificationDetail
            builder.Append("<FixedAssetReclassificationDetail>")

            builder.Append("<Id>" & FixedAssetReclassificationDetail.Id & "</Id>")
            builder.Append("<FixedAssetReclassificationId>" & FixedAssetReclassificationDetail.FixedAssetReclassificationId & "</FixedAssetReclassificationId>")
            builder.Append("<PhysicalAssetId>" & FixedAssetReclassificationDetail.PhysicalAssetId & "</PhysicalAssetId>")
            builder.Append("<Status>" & FixedAssetReclassificationDetail.Status & "</Status>")
            builder.Append("<AdquisitionType>" & FixedAssetReclassificationDetail.AdquisitionType & "</AdquisitionType>")

            For Each FixedAssetReclassificationDetailBook In FixedAssetReclassificationDetail.FixedAssetReclassificationDetailBook
                builder.Append("<FixedAssetReclassificationDetailBook>")

                builder.Append("<Id>" & FixedAssetReclassificationDetailBook.Id & "</Id>")
                builder.Append("<FixedAssetReclassificationDetailId>" & FixedAssetReclassificationDetailBook.FixedAssetReclassificationDetailId & "</FixedAssetReclassificationDetailId>")
                builder.Append("<LegalBookId>" & FixedAssetReclassificationDetailBook.LegalBookId & "</LegalBookId>")
                builder.Append("<DepreciatedValue>" & FixedAssetReclassificationDetailBook.DepreciatedValue & "</DepreciatedValue>")
                builder.Append("<ResidualValue>" & FixedAssetReclassificationDetailBook.ResidualValue & "</ResidualValue>")
                builder.Append("<HistoricalValue>" & FixedAssetReclassificationDetailBook.HistoricalValue & "</HistoricalValue>")

                builder.Append("</FixedAssetReclassificationDetailBook>")
            Next

            builder.Append("</FixedAssetReclassificationDetail>")
        Next

        builder.Append("</FixedAssetReclassification>")

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
            _sequenceRepository = Nothing
            _fixedAssetReclassificationRepository = Nothing
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
