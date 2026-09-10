'***********************************************************************
' Assembly         : Application.Maintenance
' Author           : Daniel Eduardo Arévalo
' Created          : 15-09-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Domain.Maintenance
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Domain.Maintenance.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports System.Transactions
Imports Infrastructure.CrossCutting.Resources
Imports System.Data.Entity.Infrastructure

#End Region
Public Class TrademarkAdminService

    Implements ITrademarkAdminService

    Private Const FORM_NAME As String = "Marcas"
    'Repositorio de tipo de ubicacion
    Private _trademarkRespository As ITrademarkRepository
    Private _secuenseDRepository As IMaintenanceSequenceDetailRepository

    ''' <summary>
    ''' inicia el repositorio de ubicacion
    ''' </summary>
    ''' <param name="trademarkRespository">Repositorio de Marcas</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal trademarkRespository As ITrademarkRepository,
                   secuenseDRepository As IMaintenanceSequenceDetailRepository)
        If (trademarkRespository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de trademarkRespository")
        End If
        _trademarkRespository = trademarkRespository
        _secuenseDRepository = secuenseDRepository
    End Sub

    ''' <summary>
    ''' Función que obtiene una marca por Código
    ''' </summary>
    ''' <param name="Code">Código de la Marca</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    Public Function GetTrademarkByCode(Code As String) As Trademark Implements ITrademarkAdminService.GetTrademarkByCode
        If String.IsNullOrEmpty(Code) Then
            Throw New ArgumentNullException("Codigo de Marca vacio")
        End If
        Try

            Return _trademarkRespository.GetTrademarkByCode(Code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
            Return New Trademark()
        End Try
    End Function

    ''' <summary>
    ''' Función que obtiene todas las Marcas par alos Equipos
    ''' </summary>
    ''' <returns>Lista de Marcas</returns>
    ''' <remarks></remarks>
    Public Function ListAllTrademark() As List(Of Trademark) Implements ITrademarkAdminService.ListAllTrademark
        Try
            Return _trademarkRespository.ListAllTrademark()
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Función para Eliminar las Marcas
    ''' </summary>
    ''' <param name="Trademark">Objeto Trademark</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteTrademark(Trademark As Trademark, audit As AuditMessage) As ActionResult Implements ITrademarkAdminService.DeleteTrademark
        If Trademark Is Nothing Then
            Throw New ArgumentNullException("Trademark")
        End If
        Dim unitOfWork As IUnitWork = Me._trademarkRespository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Trademark.ModificationUser = audit.CodeUser
                Trademark.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of Trademark)(Trademark, audit, status)
                Trademark.MarkAsDeleted()
                Me._trademarkRespository.SaveEntity(Trademark)
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

        'If Trademark Is Nothing Then
        '    Throw New ArgumentNullException("Trademark vacio")
        'End If
        'Dim unitWork As IUnitWork = _trademarkRespository.UnitWork
        'Try
        '    _trademarkRespository.DeleteEntity(Trademark)
        '    unitWork.Commit()
        '    Dim auditObject As New IndigoAuditSimpleEntity(Of Trademark)(Trademark, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
        '    auditObject.Execute()
        '    Return True
        'Catch ex As Exception
        '    unitWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
        '    Return False
        'End Try
    End Function

    ''' <summary>
    ''' Función para Almacenar una Marca
    ''' </summary>
    ''' <param name="Trademark">Objeto Trademark</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveTrademark(Trademark As Trademark, audit As AuditMessage, Optional idSequence As Long = 0) As Domain.Base.Entities.ActionResult(Of Trademark) Implements ITrademarkAdminService.SaveTrademark
        If Trademark Is Nothing Then
            Throw New ArgumentNullException("Trademark")
        End If
        Dim unitOfWork As IUnitWork = Me._trademarkRespository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(Trademark.Code) Then
                    Dim seq As Domain.Entities.MaintenanceSequenceDetail = Me._secuenseDRepository.GetSequenseDById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MaintenanceSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            Trademark.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of Trademark) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.MaintenanceSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), Trademark.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of Trademark) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As Trademark = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of Trademark)
                Dim status As Integer

                If Trademark.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Trademark.CreationUser = audit.CodeUser
                    Trademark.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = _trademarkRespository.GetTrademarkByCode(Trademark.Code, False)
                    Trademark.ModificationUser = audit.CodeUser
                    Trademark.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._trademarkRespository.SaveEntity(Trademark)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of Trademark)(Trademark, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                Trademark.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of Trademark) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = Trademark, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of Trademark) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Trademark) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try




        'If Trademark Is Nothing Then
        '    Throw New ArgumentNullException("Trademark Vacio")
        'End If
        'Dim UnitOfWork As IUnitWork = _trademarkRespository.UnitWork
        'Try
        '    Dim auditProcess As IndigoAuditSimpleEntity(Of Trademark)
        '    Dim auxTrademark As Trademark = Nothing
        '    Dim status As Integer
        '    If Trademark.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
        '        Trademark.ModificationUser = audit.CodeUser
        '        Trademark.ModificationDate = Date.Now()
        '        status = Infrastructure.CrossCutting.Audit.Actions.Update
        '        auxTrademark = _trademarkRespository.GetTrademarkByCode(Trademark.Code, False)
        '    Else
        '        Trademark.CreationUser = audit.CodeUser
        '        Trademark.CreationDate = Date.Now()
        '        status = Infrastructure.CrossCutting.Audit.Actions.Insert
        '    End If
        '    'Valido si se va a guardar o a eliminar
        '    _trademarkRespository.SaveEntity(Trademark)
        '    UnitOfWork.Commit()
        '    auditProcess = New IndigoAuditSimpleEntity(Of Trademark)(Trademark, audit, status, auxTrademark)
        '    auditProcess.Execute()
        '    Return True
        'Catch ex As Exception
        '    UnitOfWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    Return False
        'End Try
    End Function

    Public Function UpdateStateTrademark(code As String, state As Boolean, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of Trademark) Implements ITrademarkAdminService.UpdateStateTrademark
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
            Dim trademark As Trademark = Me._trademarkRespository.GetTrademarkByCode(code.Trim())
            If trademark IsNot Nothing AndAlso trademark.Id > 0 Then
                trademark.State = state
            End If
            Dim result = Me.SaveTrademark(trademark, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Trademark) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _trademarkRespository = Nothing
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
