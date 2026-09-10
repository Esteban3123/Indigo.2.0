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

Public Class MaintenanceContractAdminService

    Implements IMaintenanceContractAdminService


    Private Const FORM_NAME As String = "FrmMaintenanceContract"

    'Repositorio de tipo de ubicacion
    Private _maintenanceContractRespository As IMaintenanceContractRepository

    ''' <summary>
    ''' repositorio de las secuencias
    ''' </summary>
    Private _secuenseDRepository As IMaintenanceSequenceDetailRepository

    ''' <summary>
    ''' inicia el repositorio de ubicacion
    ''' </summary>
    ''' <param name="MaintenanceContractRespository">Repositorio de Responsable</param>
    ''' <remarks></remarks>
    Public Sub New(secuenceDRepository As IMaintenanceSequenceDetailRepository, ByVal MaintenanceContractRespository As IMaintenanceContractRepository)

        If (MaintenanceContractRespository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de dciRepository vacio")
        End If

        _maintenanceContractRespository = MaintenanceContractRespository
        _secuenseDRepository = secuenceDRepository

    End Sub


    ''' <summary>
    ''' Obtiene un contrato por Codigo
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    Public Function GetMaintenanceContractByCode(Code As String) As MaintenanceContract Implements IMaintenanceContractAdminService.GetMaintenanceContractByCode
        If String.IsNullOrEmpty(Code) Then
            Throw New ArgumentNullException("Codigo del contrato vacio")
        End If
        Try
            Return _maintenanceContractRespository.GetMaintenanceContractByCode(Code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return New MaintenanceContract()
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una lista de todo los contratos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllMaintenanceContract() As List(Of MaintenanceContract) Implements IMaintenanceContractAdminService.ListAllMaintenanceContract
        Try
            Return _maintenanceContractRespository.ListAllMaintenanceContract()
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza el registro
    ''' </summary>
    ''' <param name="MaintenanceContract"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    Public Function SaveMaintenanceContract(MaintenanceContract As MaintenanceContract, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of MaintenanceContract) Implements IMaintenanceContractAdminService.SaveMaintenanceContract

        If MaintenanceContract Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If

        Dim unitOfWork As IUnitWork = Me._maintenanceContractRespository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork

        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(MaintenanceContract.Code) Then
                    Dim seq As MaintenanceSequenceDetail = Me._secuenseDRepository.GetSequenseDetailUpdatedById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MaintenanceSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            MaintenanceContract.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of MaintenanceContract) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.MaintenanceSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), MaintenanceContract.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of MaintenanceContract) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As MaintenanceContract = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of MaintenanceContract)
                Dim status As Integer

                If MaintenanceContract.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    MaintenanceContract.CreationUser = audit.CodeUser
                    MaintenanceContract.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = MaintenanceContract.OriginalValue
                    MaintenanceContract.ModificationUser = audit.CodeUser
                    MaintenanceContract.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._maintenanceContractRespository.SaveEntity(MaintenanceContract)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of MaintenanceContract)(MaintenanceContract, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                MaintenanceContract.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of MaintenanceContract) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = MaintenanceContract, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of MaintenanceContract) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of MaintenanceContract) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' obtiene un activo por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetPhysicalAssetById(Id As Integer) As FixedAssetPhysicalAsset Implements IMaintenanceContractAdminService.GetPhysicalAssetById
        If String.IsNullOrEmpty(Id) Then
            Throw New ArgumentNullException("Codigo de Responsable vacio")
        End If
        Try
            Return _maintenanceContractRespository.GetPhysicalAssetById(Id)
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
            _maintenanceContractRespository = Nothing
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
