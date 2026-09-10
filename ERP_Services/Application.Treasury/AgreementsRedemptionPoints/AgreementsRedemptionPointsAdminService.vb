#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base
Imports Application.Base
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions
Imports System.Data.Entity.Infrastructure

#End Region
Public Class AgreementsRedemptionPointsAdminService

    Implements IAgreementsRedemptionPointsAdminService
    Private Const FORM_NAME As String = "FrmAgreementsRedemptionPoints"

    'Repositorio de tipo de ubicacion
    Private _AgreementsRedemptionPointsRespository As IAgreementsRedemptionPointsRepository

    ''' <summary>
    ''' repositorio de las secuencias
    ''' </summary>
    Private _secuenseDRepository As ISequenseTreasuryDRepository

    ''' <summary>
    ''' inicia el repositorio de ubicacion
    ''' </summary>
    ''' <param name="AgreementsRedemptionPointsRespository">Repositorio de  convenios de redención de productos</param>
    ''' <remarks></remarks>
    Public Sub New(secuenceDRepository As ISequenseTreasuryDRepository, ByVal AgreementsRedemptionPointsRespository As IAgreementsRedemptionPointsRepository)
        If (AgreementsRedemptionPointsRespository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de AgreementsRedemptionPointsRespository")
        End If
        _AgreementsRedemptionPointsRespository = AgreementsRedemptionPointsRespository
        _secuenseDRepository = secuenceDRepository
    End Sub

    ''' <summary>
    ''' Función que obtiene un convenio de redención de puntos por Código
    ''' </summary>
    ''' <param name="Code">Código de los convenios de redención de productos</param>
    ''' <returns>AgreementsRedemptionPoints</returns>
    ''' <remarks></remarks>
    Public Function GetAgreementsRedemptionPointsByCode(Code As String) As AgreementsRedemptionPoints Implements IAgreementsRedemptionPointsAdminService.GetAgreementsRedemptionPointsByCode
        If String.IsNullOrEmpty(Code) Then
            Throw New ArgumentNullException("Codigo de  convenios de redención de productos vacio")
        End If
        Try
            Return _AgreementsRedemptionPointsRespository.GetAgreementsRedemptionPointsRepositoryByCode(Code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return New AgreementsRedemptionPoints()
        End Try
    End Function

    ''' <summary>
    ''' Función que obtiene todos los convenios de redención de productos
    ''' </summary>
    ''' <returns>Lista de  convenios de redención de productos</returns>
    ''' <remarks></remarks>
    Public Function ListAllAgreementsRedemptionPoints() As List(Of AgreementsRedemptionPoints) Implements IAgreementsRedemptionPointsAdminService.ListAllAgreementsRedemptionPoints
        Try
            Return _AgreementsRedemptionPointsRespository.ListAllAgreementsRedemptionPointsRepository()
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Función para Eliminar el convenio de redención de puntos
    ''' </summary>
    ''' <param name="AgreementsRedemptionPoints">Objeto AgreementsRedemptionPoints</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteAgreementsRedemptionPoints(AgreementsRedemptionPoints As AgreementsRedemptionPoints, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IAgreementsRedemptionPointsAdminService.DeleteAgreementsRedemptionPoints
        If AgreementsRedemptionPoints Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._AgreementsRedemptionPointsRespository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                AgreementsRedemptionPoints.ModificationUser = audit.CodeUser
                AgreementsRedemptionPoints.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of AgreementsRedemptionPoints)(AgreementsRedemptionPoints, audit, status)

                AgreementsRedemptionPoints.MarkAsDeleted()
                Me._AgreementsRedemptionPointsRespository.SaveEntity(AgreementsRedemptionPoints)
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
    ''' Función para Almacenar convenios de redención de productos
    ''' </summary>
    ''' <param name="AgreementsRedemptionPoints">Objeto AgreementsRedemptionPoints</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveAgreementsRedemptionPoints(AgreementsRedemptionPoints As AgreementsRedemptionPoints, audit As AuditMessage, Optional idSequense As Long = 0) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AgreementsRedemptionPoints) Implements IAgreementsRedemptionPointsAdminService.SaveAgreementsRedemptionPoints
        If AgreementsRedemptionPoints Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._AgreementsRedemptionPointsRespository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(AgreementsRedemptionPoints.Code) Then
                    Dim seq As TreasurySequenceDetail = Me._secuenseDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.TreasurySequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            AgreementsRedemptionPoints.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of AgreementsRedemptionPoints) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.TreasurySequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), AgreementsRedemptionPoints.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of AgreementsRedemptionPoints) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As AgreementsRedemptionPoints = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of AgreementsRedemptionPoints)
                Dim status As Integer

                If AgreementsRedemptionPoints.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    AgreementsRedemptionPoints.CreationUser = audit.CodeUser
                    AgreementsRedemptionPoints.CreationDate = DateTime.Now
                    AgreementsRedemptionPoints.State = 1
                    For Each item In AgreementsRedemptionPoints.AgreementsRedemptionPointsDetail
                        item.CreationDate = DateTime.Now
                        item.CreationUser = audit.CodeUser
                    Next
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = AgreementsRedemptionPoints.OriginalValue
                    AgreementsRedemptionPoints.ModificationUser = audit.CodeUser
                    AgreementsRedemptionPoints.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                    For Each item In AgreementsRedemptionPoints.AgreementsRedemptionPointsDetail
                        If item.ChangeTracker.State = ObjectState.Modified Then
                            item.ModificationDate = Date.Now
                            item.ModificationUser = audit.CodeUser
                        ElseIf item.ChangeTracker.State = ObjectState.Added Then
                            item.CreationDate = Date.Now
                            item.CreationUser = audit.CodeUser
                        End If
                    Next
                End If

                Me._AgreementsRedemptionPointsRespository.SaveEntity(AgreementsRedemptionPoints)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of AgreementsRedemptionPoints)(AgreementsRedemptionPoints, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se  convenios de redención de productos la entidad como sin cambios
                AgreementsRedemptionPoints.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of AgreementsRedemptionPoints) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = AgreementsRedemptionPoints, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of AgreementsRedemptionPoints) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of AgreementsRedemptionPoints) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Función para cambiar el estado del convenio
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <remarks></remarks>
    Function ChangeAgreementsRedemptionPointsStatus(code As String, state As Boolean, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AgreementsRedemptionPoints) Implements IAgreementsRedemptionPointsAdminService.ChangeAgreementsRedemptionPointsStatus
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
            Dim AgreementsRedemptionPoints As AgreementsRedemptionPoints = GetAgreementsRedemptionPointsByCode(code.Trim())
            If AgreementsRedemptionPoints IsNot Nothing AndAlso AgreementsRedemptionPoints.Id > 0 Then
                AgreementsRedemptionPoints.State = state
            End If
            Dim result = Me.SaveAgreementsRedemptionPoints(AgreementsRedemptionPoints, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of AgreementsRedemptionPoints) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Función para consutar los detalles del convenio de redención de puntos
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetAgreementsRedemptionPointsDetails(Id As Integer) As List(Of AgreementsRedemptionPointsDetail) Implements IAgreementsRedemptionPointsAdminService.GetAgreementsRedemptionPointsDetails
        Try
            Return _AgreementsRedemptionPointsRespository.GetAgreementsRedemptionPointsDetails(Id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _AgreementsRedemptionPointsRespository = Nothing
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
