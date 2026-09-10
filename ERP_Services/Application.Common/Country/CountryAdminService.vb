Imports Domain.Common
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Transactions
Imports Infrastructure.CrossCutting.Resources
Imports System.Data.Entity.Core

Public Class CountryAdminService
    Implements ICountryAdminService

    Private _CountryRepository As ICountryRepository
    Private _secuenseDRepository As ISequenseAccountingDRepository
    Public Const FORM_NAME As String = "Paises"

    ''' <summary>
    ''' Initializa una nueva instancia de la clase <see cref="DetailedConceptAdminService" />.
    ''' </summary>
    ''' <param name="countryRepository">el repositorio para el manejo de los Países.</param>
    Public Sub New(ByVal countryRepository As ICountryRepository, secuenseDRepository As ISequenseAccountingDRepository)
        If countryRepository Is Nothing Then
            Throw New ArgumentNullException("countryRepository Vacio")
        End If
        _CountryRepository = countryRepository
        _secuenseDRepository = secuenseDRepository
    End Sub

    Public Function DeleteCountry(country As Domain.Entities.Country, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionMessageResult(Of Country) Implements ICountryAdminService.DeleteCountry
        Dim result As New ActionMessageResult(Of Country)
        result.StateResult = True
        If _CountryRepository Is Nothing Then
            Throw New ArgumentNullException("countryRepository Vacio")
        End If
        Dim UnitOfWork As IUnitWork = _CountryRepository.UnitWork
        Try
            '' elimino el pais 
            '_CountryRepository.DeleteEntity(country)
            'UnitOfWork.Commit()
            ''/***** Auditoria Basica ********/
            'IndigoAuditBasic.Execute(country.GetType.Name, audit.Functional, country.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            ''/*****Auditoria Avanzada ******/
            'Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Entities.Country)(country, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            'Return result
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                country.ModificationUser = audit.CodeUser
                country.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of Country)(country, audit, status)

                country.MarkAsDeleted()
                Me._CountryRepository.SaveEntity(country)
                UnitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()

                result.StatusCode = eStatusResult.SUCCESS
                result.Message = ResourceManager.GetString("RecordDeleted")

                Return result
            End Using
        Catch ex As OptimisticConcurrencyException
            result.StatusCode = eStatusResult.WARNING
            result.Message = ResourceManager.GetString("ErrorConcurrence")
            result.StateResult = False
            UnitOfWork.RollbackChanges()
            Return result
        Catch ex As UpdateException
            result.StatusCode = eStatusResult.WARNING
            result.Message = ResourceManager.GetString("ErrorDependence")
            result.StateResult = False
            result.MessageResult.Add(New MessageResult("c-0000", country.Code))
            UnitOfWork.RollbackChanges()
            Return result
        Catch ex As DbUpdateException
            result.StatusCode = eStatusResult.WARNING
            result.Message = ResourceManager.GetString("ErrorDependence")
            result.StateResult = False
            result.MessageResult.Add(New MessageResult("c-0000", country.Code))
            UnitOfWork.RollbackChanges()
            Return result
        Catch ex As Exception
            result.StatusCode = eStatusResult.EXCEPTION
            result.Message = IndigoManagementExceptions.GetExceptionDetails(ex)
            result.StateResult = False

            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return result
        End Try
    End Function

    Public Function GetCountry(code As String) As Domain.Entities.Country Implements ICountryAdminService.GetCountry
        If String.IsNullOrEmpty(code) = True Then
            Throw New ArgumentNullException("codeCountry vacio")
        End If
        Try
            Return _CountryRepository.GetCountry(code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Devuelve un país por ID
    ''' </summary>
    ''' <param name="idCountry">Id del pais</param>
    ''' <returns>El pais</returns>
    ''' <remarks></remarks>
    Public Function GetCountryById(ByVal idCountry As Integer, ByVal audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Entities.Country Implements ICountryAdminService.GetCountryById
        If Not (idCountry > 0) Then
            Throw New ArgumentNullException("idCountry Vacio")
        End If
        Try
            Return _CountryRepository.GetCountryById(idCountry)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New Domain.Entities.Country
        End Try
    End Function

    Public Function ListAllCountry() As List(Of Domain.Entities.Country) Implements ICountryAdminService.ListAllCountry
        Try
            Return _CountryRepository.ListAllCountry()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveCountry(country As Domain.Entities.Country, audit As Infrastructure.CrossCutting.Base.AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of Country) Implements ICountryAdminService.SaveCountry
        If country Is Nothing Then
            Throw New ArgumentNullException("Country Vacio")
        End If
        Dim UnitOfWork As IUnitWork = _CountryRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty
                Dim seq As GeneralLedgerSequenceDetail = Nothing
                If country.Code Is Nothing OrElse country.Code.Trim().Equals(String.Empty) Then
                    seq = Me._secuenseDRepository.GetSequenseDetailUpdatedById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.GeneralLedgerSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            country.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of Country) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.GeneralLedgerSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), country.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of Country) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxCommon As Country = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of Country)
                Dim status As Integer

                If country.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    country.CreationUser = audit.CodeUser
                    country.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxCommon = country.OriginalValue
                    country.ModificationUser = audit.CodeUser
                    country.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._CountryRepository.SaveEntity(country)
                UnitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of Country)(country, audit, status, auxCommon)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                country.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of Country) With {.StatusCode = eStatusResult.SUCCESS, .StateResult = True, .ObjectEmbbeded = country, .Message = MessageResult}
            End Using




            'Dim auditProcess As IndigoAuditSimpleEntity(Of Country)
            'Dim auxCountry As Domain.Entities.Country = Nothing
            'Dim status As Integer

            'If country.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
            '    country.ModificationUser = audit.CodeUser
            '    country.ModificationDate = Date.Now()
            '    status = Infrastructure.CrossCutting.Audit.Actions.Update
            '    auxCountry = _CountryRepository.GetCountryById(country.Id, False)
            'Else
            '    country.CreationUser = audit.CodeUser
            '    country.CreationDate = Date.Now()
            '    status = Infrastructure.CrossCutting.Audit.Actions.Insert
            'End If

            ''Valido si se va a guardar o a eliminar
            '_CountryRepository.SaveEntity(country)
            'UnitOfWork.Commit()
            'auditProcess = New IndigoAuditSimpleEntity(Of Country)(country, audit, status, auxCountry)
            'auditProcess.Execute()
            'Return True

        Catch ex As OptimisticConcurrencyException
            UnitOfWork.RollbackChanges()
            Return New ActionResult(Of Country) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Country) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _CountryRepository = Nothing
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
