#Region "Imports"

Imports System.Data.Entity.Core
Imports System.Transactions
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources

#End Region

Public Class GlosaMedicalFeesAdminService

    Implements IGlosaMedicalFeesAdminService

    Private Const FORM_NAME As String = "FrmGlosaMedicalFees"

    'Repositorio de tipo de ubicacion
    Private _GlosaMedicalFeesRespository As IGlosaMedicalFeesRepository

    ''' <summary>
    ''' repositorio de las secuencias
    ''' </summary>
    Private _secuenseDRepository As IMedicalFeesSecuenceDetailRepository

    ''' <summary>
    ''' inicia el repositorio de ubicacion
    ''' </summary>
    ''' <param name="GlosaMedicalFeesRespository">Repositorio de Responsable</param>
    ''' <remarks></remarks>
    Public Sub New(secuenceDRepository As IMedicalFeesSecuenceDetailRepository, ByVal GlosaMedicalFeesRespository As IGlosaMedicalFeesRepository)

        If (GlosaMedicalFeesRespository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de dciRepository vacio")
        End If

        _GlosaMedicalFeesRespository = GlosaMedicalFeesRespository
        _secuenseDRepository = secuenceDRepository

    End Sub

    ''' <summary>
    ''' Obtiene un registro por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetGlosaMedicalFeesByCode(Code As String) As GlosaMedicalFees Implements IGlosaMedicalFeesAdminService.GetGlosaMedicalFeesByCode
        If Code Is Nothing OrElse Code Is String.Empty Then
            Throw New ArgumentNullException("Codigo vacio")
        End If
        Try
            Return _GlosaMedicalFeesRespository.GetGlosaMedicalFeesByCode(Code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return New GlosaMedicalFees()
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una lista de todos lode registro de la tabla
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllGlosaMedicalFees() As List(Of GlosaMedicalFees) Implements IGlosaMedicalFeesAdminService.ListAllGlosaMedicalFees
        Try
            Return _GlosaMedicalFeesRespository.ListAllGlosaMedicalFees()
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza el registro
    ''' </summary>
    ''' <param name="GlosaMedicalFees"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    Public Function SaveGlosaMedicalFees(GlosaMedicalFees As GlosaMedicalFees, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of GlosaMedicalFees) Implements IGlosaMedicalFeesAdminService.SaveGlosaMedicalFees

        If GlosaMedicalFees Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If

        Dim unitOfWork As IUnitWork = Me._GlosaMedicalFeesRespository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork

        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(GlosaMedicalFees.Code) Then
                    Dim seq As MedicalFeesSecuenceDetail = Me._secuenseDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MedicalFeesSecuence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            GlosaMedicalFees.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of GlosaMedicalFees) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.MedicalFeesSecuence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), GlosaMedicalFees.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of GlosaMedicalFees) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As GlosaMedicalFees = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of GlosaMedicalFees)
                Dim status As Integer

                If GlosaMedicalFees.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    GlosaMedicalFees.CreationUser = audit.CodeUser
                    GlosaMedicalFees.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = GlosaMedicalFees.OriginalValue
                    GlosaMedicalFees.ModificationUser = audit.CodeUser
                    GlosaMedicalFees.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._GlosaMedicalFeesRespository.SaveEntity(GlosaMedicalFees)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of GlosaMedicalFees)(GlosaMedicalFees, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                GlosaMedicalFees.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of GlosaMedicalFees) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = GlosaMedicalFees, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of GlosaMedicalFees) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of GlosaMedicalFees) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' obtiene una cuenta por pagar por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetAccountPayableById(Id As Integer) As AccountPayable Implements IGlosaMedicalFeesAdminService.GetAccountPayableById
        If String.IsNullOrEmpty(Id) Then
            Throw New ArgumentNullException("Codigo de Responsable vacio")
        End If
        Try
            Return _GlosaMedicalFeesRespository.GetAccountPayableById(Id)
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
            _GlosaMedicalFeesRespository = Nothing
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