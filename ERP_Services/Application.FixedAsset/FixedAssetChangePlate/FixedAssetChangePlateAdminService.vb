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

Public Class FixedAssetChangePlateAdminService
    Implements IFixedAssetChangePlateAdminService

#Region "Variables"

    'Repositorio de la aseguradora
    Private _fixedAssetChangePlateRepository As IFixedAssetChangePlateRepository

    'Repositorio de la secuencia
    Private _sequenceRepository As IFixedAssetSequenceDetailRepository

    'Repositorio para los activos
    Private _fixedAssetPhysicalAssetRepository As IFixedAssetPhysicalAssetRepository

#End Region

#Region "Builder"

    ''' <summary>
    ''' inicia el repositorio de bancos
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(fixedAssetChangePlateRepository As IFixedAssetChangePlateRepository, sequenceRepository As IFixedAssetSequenceDetailRepository,
                   fixedAssetPhysicalAssetRepository As IFixedAssetPhysicalAssetRepository)
        If sequenceRepository Is Nothing Then
            Throw New ArgumentNullException("sequenceRepository")
        End If
        If fixedAssetChangePlateRepository Is Nothing Then
            Throw New ArgumentNullException("fixedAssetChangePlateRepository")
        End If
        If fixedAssetPhysicalAssetRepository Is Nothing Then
            Throw New ArgumentNullException("fixedAssetPhysicalAssetRepository")
        End If
        _sequenceRepository = sequenceRepository
        _fixedAssetChangePlateRepository = fixedAssetChangePlateRepository
        _fixedAssetPhysicalAssetRepository = fixedAssetPhysicalAssetRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Confirma el registro
    ''' </summary>
    ''' <param name="FixedAssetActiveOutput"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConfirmFixedAssetChangePlate(FixedAssetChangePlate As FixedAssetChangePlate, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of FixedAssetChangePlate) Implements IFixedAssetChangePlateAdminService.ConfirmFixedAssetChangePlate
        If FixedAssetChangePlate Is Nothing Then
            Throw New ArgumentNullException("FixedAssetChangePlate")
        End If

        Dim unitOfWorkPhysical As IUnitWork = Me._fixedAssetPhysicalAssetRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                'Se guarda o actualiza el cambio de placa en sus respectivas tablas
                Dim resultSave As ActionResult(Of FixedAssetChangePlate) = SaveFixedAssetChangePlate(FixedAssetChangePlate, audit, idSequense)
                If resultSave.StateResult = False Then
                    transaction.Dispose()
                    If resultSave.StatusCode = eStatusResult.EXCEPTION Then
                        Return New ActionResult(Of FixedAssetChangePlate) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = resultSave.Message}
                    ElseIf resultSave.StatusCode = eStatusResult.WARNING Then
                        Return New ActionResult(Of FixedAssetChangePlate) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = resultSave.Message}
                    End If
                End If

                ''Listado de errores
                'Dim ListErrors As New StringBuilder

                ''Se recorren los detalles y se actualiza el valor segun corresponda
                'For Each changePlateDetail In resultSave.ObjectEmbbeded.FixedAssetChangePlateDetail
                '    'Se valida que la nueva placa no exista
                '    Dim physical = _fixedAssetChangePlateRepository.GetPhysicalByPlate(changePlateDetail.NewPlate)
                '    If physical IsNot Nothing Then
                '        ListErrors.AppendLine("La nueva placa " + changePlateDetail.NewPlate + " ya existe en la BD")
                '        Continue For
                '    End If
                '    'Se consulta el physical para actualizar la nueva placa
                '    physical = _fixedAssetChangePlateRepository.GetPhysicalById(changePlateDetail.FixedAssetPhysicalAssetId)
                '    If physical IsNot Nothing Then
                '        physical.Plate = changePlateDetail.NewPlate
                '        physical.MarkAsModified()
                '        _fixedAssetPhysicalAssetRepository.SaveEntity(physical)
                '        unitOfWorkPhysical.Commit()
                '    End If
                'Next

                'Listado de errores
                Dim ListErrors As New StringBuilder

                'Se convierte la entidad a xml
                Dim xmlObject As String = ConvertToXml(resultSave.ObjectEmbbeded.FixedAssetChangePlateDetail.ToList())

                'Se consume el sp creado
                Dim resultSpChangePlate = _fixedAssetChangePlateRepository.SP_FixedAssetChangePlate(xmlObject, audit.CodeUser)
                If resultSpChangePlate IsNot Nothing AndAlso resultSpChangePlate.Count > 0 AndAlso (From x In resultSpChangePlate Where x.CodeMessage = 999 Select x).Count > 0 Then
                    resultSpChangePlate.ForEach(Sub(x) IIf(x.CodeMessage = 999, ListErrors.AppendLine(x.MessageReturn), ""))
                    unitOfWorkPhysical.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of FixedAssetChangePlate) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ListErrors.ToString}
                End If

                'If ListErrors.Length > 0 Then
                '    unitOfWorkPhysical.RollbackChanges()
                '    transaction.Dispose()
                '    Return New ActionResult(Of FixedAssetChangePlate) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ListErrors.ToString}
                'End If

                transaction.Complete()
                Return New ActionResult(Of FixedAssetChangePlate) With {.StateResult = True, .ObjectEmbbeded = FixedAssetChangePlate, .StatusCode = eStatusResult.SUCCESS}
            Catch ex As OptimisticConcurrencyException
                unitOfWorkPhysical.RollbackChanges()
                transaction.Dispose()
                Return New ActionResult(Of FixedAssetChangePlate) With {.StateResult = False, .Message = "Error de concurrencia", .StatusCode = eStatusResult.EXCEPTION}
            Catch ex As Exception
                unitOfWorkPhysical.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of FixedAssetChangePlate) With {.StateResult = False, .Message = ex.Message, .StatusCode = eStatusResult.EXCEPTION}
            End Try
        End Using
    End Function

    Private Function ConvertToXml(ListDetail As List(Of FixedAssetChangePlateDetail))
        Dim builder As StringBuilder = New StringBuilder()

        For Each item In ListDetail
            builder.Append("<TableXml>")

            builder.Append("<FixedAssetPhysicalAssetId>" & item.FixedAssetPhysicalAssetId & "</FixedAssetPhysicalAssetId>")
            builder.Append("<OldPlate>" & item.OldPlate & "</OldPlate>")
            builder.Append("<NewPlate>" & item.NewPlate & "</NewPlate>")

            builder.Append("</TableXml>")
        Next

        Return builder.ToString
    End Function

    ''' <summary>
    ''' Obtiene un registro por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetChangePlate(code As String, audit As AuditMessage) As ActionResult(Of FixedAssetChangePlate) Implements IFixedAssetChangePlateAdminService.GetFixedAssetChangePlate
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim FixedAssetChangePlate As FixedAssetChangePlate = Me._fixedAssetChangePlateRepository.GetFixedAssetChangePlate(code.Trim())
            If FixedAssetChangePlate IsNot Nothing AndAlso FixedAssetChangePlate.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of FixedAssetChangePlate)(FixedAssetChangePlate, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of FixedAssetChangePlate) With {.StateResult = True, .ObjectEmbbeded = FixedAssetChangePlate}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FixedAssetChangePlate) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un registro por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetChangePlateById(Id As Integer, audit As AuditMessage) As ActionResult(Of FixedAssetChangePlate) Implements IFixedAssetChangePlateAdminService.GetFixedAssetChangePlateById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim FixedAssetChangePlate As FixedAssetChangePlate = Me._fixedAssetChangePlateRepository.GetFixedAssetChangePlateById(Id)
            If FixedAssetChangePlate IsNot Nothing AndAlso FixedAssetChangePlate.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of FixedAssetChangePlate)(FixedAssetChangePlate, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of FixedAssetChangePlate) With {.StateResult = True, .ObjectEmbbeded = FixedAssetChangePlate}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FixedAssetChangePlate) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda un registro
    ''' </summary>
    ''' <param name="FixedAssetTransfer"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveFixedAssetChangePlate(FixedAssetChangePlate As FixedAssetChangePlate, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of FixedAssetChangePlate) Implements IFixedAssetChangePlateAdminService.SaveFixedAssetChangePlate
        If FixedAssetChangePlate Is Nothing Then
            Throw New ArgumentNullException("FixedAssetChangePlate")
        End If
        Dim unitOfWork As IUnitWork = Me._fixedAssetChangePlateRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenceRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim seq As FixedAssetSequenceDetail = Nothing
                If FixedAssetChangePlate.Code Is Nothing OrElse FixedAssetChangePlate.Code.Trim().Equals(String.Empty) Then
                    seq = Me._sequenceRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.FixedAssetSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            FixedAssetChangePlate.Code = res
                            seq.Next += 1
                            Me._sequenceRepository.SaveEntity(seq)
                        Else
                            transaction.Dispose()
                            Return New ActionResult(Of FixedAssetChangePlate) With {.StateResult = False, .Message = "El rango de la secuencia numérica ya se excedió", .StatusCode = eStatusResult.WARNING}
                        End If
                    Else
                        transaction.Dispose()
                        Return New ActionResult(Of FixedAssetChangePlate) With {.StateResult = False, .Message = "El formulario no tiene parametrizada la secuencia numérica", .StatusCode = eStatusResult.WARNING}
                    End If
                End If

                Dim auxFixedAssetChangePlate As FixedAssetChangePlate = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of FixedAssetChangePlate)
                Dim status As Integer

                If FixedAssetChangePlate.ChangeTracker.State = ObjectState.Added Then
                    FixedAssetChangePlate.CreationUser = audit.CodeUser
                    FixedAssetChangePlate.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                ElseIf FixedAssetChangePlate.ChangeTracker.State = ObjectState.Modified Then
                    auxFixedAssetChangePlate = FixedAssetChangePlate.OriginalValue
                    If FixedAssetChangePlate.Status = 1 Then
                        FixedAssetChangePlate.ModificationUser = audit.CodeUser
                        FixedAssetChangePlate.ModificationDate = DateTime.Now
                        status = Infrastructure.CrossCutting.Audit.Actions.Update
                    End If
                    If FixedAssetChangePlate.Status = 2 Then
                        FixedAssetChangePlate.ModificationUser = audit.CodeUser
                        FixedAssetChangePlate.ModificationDate = DateTime.Now
                        FixedAssetChangePlate.ConfirmationUser = audit.CodeUser
                        FixedAssetChangePlate.ConfirmationDate = DateTime.Now
                        status = Infrastructure.CrossCutting.Audit.Actions.Confirm
                    End If
                    If FixedAssetChangePlate.Status = 3 Then
                        FixedAssetChangePlate.ModificationUser = audit.CodeUser
                        FixedAssetChangePlate.ModificationDate = DateTime.Now
                        FixedAssetChangePlate.AnnulmentUser = audit.CodeUser
                        FixedAssetChangePlate.AnnulmentDate = DateTime.Now
                        status = Infrastructure.CrossCutting.Audit.Actions.Annular
                    End If
                End If

                Me._fixedAssetChangePlateRepository.SaveEntity(FixedAssetChangePlate)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of FixedAssetChangePlate)(FixedAssetChangePlate, audit, status, auxFixedAssetChangePlate)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                FixedAssetChangePlate.MarkAsUnchanged()

                transaction.Complete()
                Return New ActionResult(Of FixedAssetChangePlate) With {.StateResult = True, .ObjectEmbbeded = FixedAssetChangePlate, .StatusCode = eStatusResult.SUCCESS}
            Catch ex As OptimisticConcurrencyException
                transaction.Dispose()
                unitOfWork.RollbackChanges()
                Return New ActionResult(Of FixedAssetChangePlate) With {.StateResult = False, .Message = "Error de concurrencia", .StatusCode = eStatusResult.EXCEPTION}
            Catch ex As Exception
                transaction.Dispose()
                unitOfWork.RollbackChanges()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of FixedAssetChangePlate) With {.StateResult = False, .Message = ex.Message, .StatusCode = eStatusResult.EXCEPTION}
            End Try
        End Using
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
            _fixedAssetChangePlateRepository = Nothing
            _fixedAssetPhysicalAssetRepository = Nothing
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
