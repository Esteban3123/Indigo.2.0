'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Judy Andrea Díaz Reyes
' Created          : 21-05-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports System.Data.Entity.Infrastructure
Imports System.Transactions
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources

#End Region
Public Class UnitDoseTypeAdminService
	Implements IUnitDoseTypeAdminService, Inject

#Region "Properties"
	Private Const FORM_NAME As String = "FrmUnitDoseType"

	''' <summary>
	''' Variable tipo repositorio para dependencia
	''' </summary>
	''' <remarks></remarks>
	Private _unitDoseTypeRepository As IUnitDoseTypeRepository

	''' <summary>
	''' Repositorio de secuencias numéricas
	''' </summary>
	Private _secuenseDetailRepository As IMixingStationSequenceDetailRepository

#End Region

#Region "Methods"

	''' <summary>
	''' Constructor de la clase
	''' </summary>
	''' <remarks></remarks>
	Public Sub New(ByVal unitDoseTypeRepository As IUnitDoseTypeRepository, ByVal secuenseDetailRepository As IMixingStationSequenceDetailRepository)
		If unitDoseTypeRepository Is Nothing Then
			Throw New ArgumentNullException("workCenterRepository Vacio")
		End If
		If secuenseDetailRepository Is Nothing Then
			Throw New ArgumentNullException("secuenseDetailRepository")
		End If
		Me._unitDoseTypeRepository = unitDoseTypeRepository
		Me._secuenseDetailRepository = secuenseDetailRepository
	End Sub

    ''' <summary>
    ''' Trae todos los tipos de dosis unitaria
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllUnitDoseType(audit As AuditMessage) As List(Of UnitDoseType) Implements IUnitDoseTypeAdminService.ListAllUnitDoseType
        Try
            Dim unitDoseType = Me._unitDoseTypeRepository.GetAll()
            For Each item As UnitDoseType In unitDoseType
                Dim auditObject As New IndigoAuditSimpleEntity(Of UnitDoseType)(item, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            Next
            Return unitDoseType
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Elimina un tipo de dosis unitaria por id
    ''' </summary>
    ''' <param name="unitDoseType">The identifier.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Function DeleteUnitDoseType(unitDoseType As UnitDoseType, audit As AuditMessage) As ActionResult Implements IUnitDoseTypeAdminService.DeleteUnitDoseType
		If unitDoseType Is Nothing Then
			Throw New ArgumentNullException("UnitDoseType")
		End If
		Dim unitOfWork As IUnitWork = Me._unitDoseTypeRepository.UnitWork
		Try
			Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
				unitDoseType.ModificationUser = audit.CodeUser
				unitDoseType.ModificationDate = Date.Now
				Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
				Dim auditProcess As New IndigoAuditSimpleEntity(Of UnitDoseType)(unitDoseType, audit, status)
				unitDoseType.MarkAsDeleted()
				Me._unitDoseTypeRepository.SaveEntity(unitDoseType)
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
	''' Obtiene el tipo de dosis unitaria por codigo
	''' </summary>
	''' <param name="code"></param>
	''' <param name="audit"></param>
	''' <returns></returns>
	''' <exception cref="System.ArgumentNullException">Id</exception>
	Public Function GetUnitDoseType(code As String, audit As AuditMessage) As ActionResult(Of UnitDoseType) Implements IUnitDoseTypeAdminService.GetUnitDoseType
		If String.IsNullOrEmpty(code) Then
			Throw New ArgumentNullException("code")
		End If
		If audit Is Nothing Then
			Throw New ArgumentNullException("audit")
		End If
		Try
			Dim unitDoseType As UnitDoseType = Me._unitDoseTypeRepository.GetUnitDoseType(code)
			If unitDoseType IsNot Nothing AndAlso unitDoseType.Id > 0 Then
				Dim auditObject As New IndigoAuditSimpleEntity(Of UnitDoseType)(unitDoseType, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
				auditObject.Execute()
			End If
			Return New ActionResult(Of UnitDoseType) With {.StateResult = True, .ObjectEmbbeded = unitDoseType}
		Catch ex As Exception
			IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
			Return New ActionResult(Of UnitDoseType) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
		End Try
	End Function

	''' <summary>
	''' Obtiene un tipo de dosis unitaria por id
	''' </summary>
	''' <param name="id">The identifier.</param>
	''' <param name="audit">The identifier.</param>
	''' <returns></returns>
	''' <exception cref="System.ArgumentNullException">Id</exception>
	Public Function GetUnitDoseTypeById(id As Integer, audit As AuditMessage) As ActionResult(Of UnitDoseType) Implements IUnitDoseTypeAdminService.GetUnitDoseTypeById
		If id = 0 Then
			Throw New ArgumentNullException("id")
		End If
		If audit Is Nothing Then
			Throw New ArgumentNullException("audit")
		End If
		Try
			Dim unitDoseType As UnitDoseType = Me._unitDoseTypeRepository.GetUnitDoseTypeById(id)
			If unitDoseType IsNot Nothing AndAlso unitDoseType.Id > 0 Then
				Dim auditObject As New IndigoAuditSimpleEntity(Of UnitDoseType)(unitDoseType, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
				auditObject.Execute()
			End If
			Return New ActionResult(Of UnitDoseType) With {.StateResult = True, .ObjectEmbbeded = unitDoseType}
		Catch ex As Exception
			IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
			Return New ActionResult(Of UnitDoseType) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
		End Try
	End Function

	''' <summary>
	''' Guarda o actualiza un tipo de dosis unitaria
	''' </summary>
	''' <param name="unitDoseType"></param>
	''' <param name="audit"></param>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Function SaveUnitDoseType(unitDoseType As UnitDoseType, audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of UnitDoseType) Implements IUnitDoseTypeAdminService.SaveUnitDoseType
		If unitDoseType Is Nothing Then
			Throw New ArgumentNullException("ObjEntity")
		End If
		Dim unitOfWork As IUnitWork = Me._unitDoseTypeRepository.UnitWork
		Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDetailRepository.UnitWork
		Try
			Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
				Dim MessageResult As String = String.Empty

				If String.IsNullOrEmpty(unitDoseType.Code) Then
					Dim seq As MixingStationSequenceDetail = Me._secuenseDetailRepository.GetSequenceDById(idSequence)
					If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MixingStationSequence.Sequential Then
						Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
						If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
							unitDoseType.Code = res
							seq.Next += 1
							Me._secuenseDetailRepository.SaveEntity(seq)
						Else
							scope.Dispose()
							Return New ActionResult(Of UnitDoseType) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
						End If
						MessageResult = If(seq.MixingStationSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), unitDoseType.Code), ResourceManager.GetString("SaveMessage"))
					Else
						scope.Dispose()
						Return New ActionResult(Of UnitDoseType) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
					End If
				Else
					MessageResult = ResourceManager.GetString("SaveMessage")
				End If

				Dim auxObjEntity As UnitDoseType = Nothing
				Dim auditProcess As IndigoAuditSimpleEntity(Of UnitDoseType)
				Dim status As Integer

				If unitDoseType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
					unitDoseType.CreationUser = audit.CodeUser
					unitDoseType.CreationDate = DateTime.Now
					status = Infrastructure.CrossCutting.Audit.Actions.Insert
				Else
					MessageResult = ResourceManager.GetString("UpdateMessage")
					auxObjEntity = unitDoseType.OriginalValue
					unitDoseType.ModificationUser = audit.CodeUser
					unitDoseType.ModificationDate = DateTime.Now
					status = Infrastructure.CrossCutting.Audit.Actions.Update
				End If

				Me._unitDoseTypeRepository.SaveEntity(unitDoseType)
				unitOfWork.Commit()
				sequenseUnitOfWork.Commit()
				auditProcess = New IndigoAuditSimpleEntity(Of UnitDoseType)(unitDoseType, audit, status, auxObjEntity)
				auditProcess.Execute()

				'Se marca la entidad como sin cambios
				unitDoseType.MarkAsUnchanged()
				scope.Complete()
				Return New ActionResult(Of UnitDoseType) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = unitDoseType, .Message = MessageResult}
			End Using
		Catch ex As OptimisticConcurrencyException
			unitOfWork.RollbackChanges()
			Return New ActionResult(Of UnitDoseType) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
		Catch ex As Exception
			unitOfWork.RollbackChanges()
			IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
			Return New ActionResult(Of UnitDoseType) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
		End Try

	End Function

	''' <summary>
	''' Actualiza el estado de un tipo de dosis unitaria
	''' </summary>
	''' <param name="code"></param>
	''' <param name="state"></param>
	''' <param name="audit"></param>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Function UpdateStateUnitDoseType(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of UnitDoseType) Implements IUnitDoseTypeAdminService.UpdateStateUnitDoseType
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
			Dim unitDoseType As UnitDoseType = Me._unitDoseTypeRepository.GetUnitDoseType(code)
			If unitDoseType IsNot Nothing AndAlso unitDoseType.Id > 0 Then
				unitDoseType.State = state
			End If
			Return Me.SaveUnitDoseType(unitDoseType, audit)
		Catch ex As Exception
			IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
			Return New ActionResult(Of UnitDoseType) With {.StatusCode = eStatusResult.EXCEPTION, .Message = ResourceManager.GetString("ErrorUnknown")}
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

			_unitDoseTypeRepository = Nothing
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
