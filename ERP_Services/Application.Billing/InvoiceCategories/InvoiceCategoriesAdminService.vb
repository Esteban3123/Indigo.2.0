'***********************************************************************
' Assembly         : Application.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 26-10-2015
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
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Entities.Service
Imports System.Transactions
Imports Domain.Security
Imports System.Data.SqlClient
Imports System.Text

Public Class InvoiceCategoriesAdminService
    Implements IInvoiceCategoriesAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de categoria de facturas
    ''' </summary>
    Private _invoiceCategories As IBillingInvoiceCategories
    Private _billingSequenceD As IBillingSequenceDetailRepository
    Private _billingSequence As IBillingSequenceRepository
    Private _userRepository As IUserRepository
    Private Const FORM_NAME As String = "Categorías de Facturas"
    Private Const TAG_FORM As String = "1676"

#End Region

#Region "Methods"
    Public Sub New(ByVal invoiceCategories As IBillingInvoiceCategories, billingSequenceD As IBillingSequenceDetailRepository, billingSequence As IBillingSequenceRepository,
                   userRepository As IUserRepository)
        If invoiceCategories Is Nothing Then
            Throw New ArgumentNullException("invoiceRepository")
        End If
        _invoiceCategories = invoiceCategories
        _billingSequenceD = billingSequenceD
        _billingSequence = billingSequence
        _userRepository = userRepository
    End Sub

    ''' <summary>
    ''' Deletes the invoice category.
    ''' </summary>
    ''' <param name="invoiceCategory">The invoice category.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">invoiceCategory</exception>
    Public Function DeleteInvoiceCategory(invoiceCategory As Domain.Entities.InvoiceCategories, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult Implements IInvoiceCategoriesAdminService.DeleteInvoiceCategory
        If invoiceCategory Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._invoiceCategories.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                invoiceCategory.ModificationUser = audit.CodeUser
                invoiceCategory.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of InvoiceCategories)(invoiceCategory, audit, status)

                While invoiceCategory.InvoiceCategoriesUser.Count > 0
                    invoiceCategory.InvoiceCategoriesUser(invoiceCategory.InvoiceCategoriesUser.Count - 1).MarkAsDeleted()
                End While
                invoiceCategory.MarkAsDeleted()
                Me._invoiceCategories.SaveEntity(invoiceCategory)
                unitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Gets the invoice category.
    ''' </summary>
    Public Function GetInvoiceCategory(code As String, audit As AuditMessage) As Domain.Entities.InvoiceCategories Implements IInvoiceCategoriesAdminService.GetInvoiceCategory
        Try
            Dim invoiceCategories As InvoiceCategories = Me._invoiceCategories.GetInvoiceCategory(code.Trim())
            If invoiceCategories IsNot Nothing AndAlso invoiceCategories.Id > 0 Then
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Print
                'Dim auditProcess As New IndigoAuditSimpleEntity(Of InvoiceCategories)(invoiceCategories, audit, status)
                'auditProcess.Execute()
                Dim user As Domain.Security.Entities.User

                If invoiceCategories.InvoiceCategoriesUser IsNot Nothing AndAlso invoiceCategories.InvoiceCategoriesUser.Count > 0 Then
                    For Each iu In invoiceCategories.InvoiceCategoriesUser
                        'Se consulta primero por id
                        user = _userRepository.GetUserById(iu.UserId)
                        If Not (user IsNot Nothing AndAlso user.Id > 0) Then
                            'Si se consulta el usuario por id y no existe, entonces consulto por código
                            user = _userRepository.GetUserByCodeSimple(iu.UserCode)
                        End If
                        If user IsNot Nothing AndAlso user.Id > 0 Then 'Se establece el nombre de la persona a la entidad
                            iu.UserFullName = String.Concat(user.UserCode, " - ", user.Person.Fullname)
                        End If
                    Next
                End If

            End If
            Return invoiceCategories
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Gets the invoice category by identifier.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetInvoiceCategoryById(id As Integer) As Domain.Entities.InvoiceCategories Implements IInvoiceCategoriesAdminService.GetInvoiceCategoryById
        Try
            Return Me._invoiceCategories.GetInvoiceCategoryById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Saves the invoice category.
    ''' </summary>
    ''' <param name="invoiceCategory">The invoice category.</param>
    ''' <param name="audit">The audit.</param>
    ''' <param name="idSequence">The identifier sequence.</param>
    ''' <returns></returns>
    Public Function SaveInvoiceCategory(invoiceCategory As Domain.Entities.InvoiceCategories, audit As Infrastructure.CrossCutting.Base.AuditMessage, Optional idSequence As Long = 0) As Domain.Base.Entities.ActionResult(Of Domain.Entities.InvoiceCategories) Implements IInvoiceCategoriesAdminService.SaveInvoiceCategory
        Dim unitOfWork As IUnitWork = Me._invoiceCategories.UnitWork
        Dim unitWorkSequence As IUnitWork = Me._billingSequenceD.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty
                If String.IsNullOrEmpty(invoiceCategory.Code) Then
                    Dim seq As BillingSequenceDetail = _billingSequenceD.GetSequenseDById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.BillingSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            invoiceCategory.Code = res
                            seq.Next += 1
                            Me._billingSequenceD.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of InvoiceCategories) With {.StatusCode = eStatusResult.WARNING, .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.BillingSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), invoiceCategory.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of InvoiceCategories) With {.StatusCode = eStatusResult.WARNING, .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                End If
                Dim auxInvoiceCategory As InvoiceCategories = Nothing
                Dim state As Integer
                If invoiceCategory.ChangeTracker.State = ObjectState.Added Then
                    invoiceCategory.CreationDate = Date.Now
                    invoiceCategory.CreationUser = audit.CodeUser
                    state = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    invoiceCategory.ModificationDate = Date.Now
                    invoiceCategory.ModificationUser = audit.CodeUser
                    state = Infrastructure.CrossCutting.Audit.Actions.Update
                    auxInvoiceCategory = invoiceCategory.OriginalValue
                End If
                Me._invoiceCategories.SaveEntity(invoiceCategory)
                unitOfWork.Commit()
                unitWorkSequence.Commit()
                Dim auditProcess As New IndigoAuditSimpleEntity(Of InvoiceCategories)(invoiceCategory, audit, state, auxInvoiceCategory)
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult(Of InvoiceCategories) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = invoiceCategory, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of InvoiceCategories) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of InvoiceCategories) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Updates the state invoice category.
    ''' </summary>
    Public Function UpdateStateInvoiceCategory(code As String, state As Boolean, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.InvoiceCategories) Implements IInvoiceCategoriesAdminService.UpdateStateInvoiceCategory
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
            Dim invoiceCategories As InvoiceCategories = Me._invoiceCategories.GetInvoiceCategory(code.Trim())
            If invoiceCategories IsNot Nothing AndAlso invoiceCategories.Id > 0 Then
                invoiceCategories.Status = state
            End If
            Dim result = Me.SaveInvoiceCategory(invoiceCategories, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of InvoiceCategories) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Valida el copyPaste de categorias
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CopyAndPasteCategories(data As List(Of List(Of String))) As ActionResult(Of List(Of InvoiceCategoriesUser), List(Of Tuple(Of String, Integer))) Implements IInvoiceCategoriesAdminService.CopyAndPasteCategories
        If data Is Nothing OrElse data.Count = 0 Then
            Throw New ArgumentNullException("data")
        End If
        'Listado que se devuelve para pegar a la rejilla del form
        Dim ListInvoiceCategoriesUser As New List(Of InvoiceCategoriesUser)
        'Listado de errores
        Dim listErrors As New List(Of Tuple(Of String, Integer))
        Try
            'Objeto xml
            Dim xmlObject = ConvertToXml(data)
            'Se consume el procedimiento almacenado
            Dim resultStore = _invoiceCategories.SP_CopyAndPasteCategories(xmlObject)

            'Se crean los objetos para devolver y pegar en la rejilla
            If resultStore IsNot Nothing AndAlso resultStore.Count > 0 Then
                For Each itemXml In resultStore
                    If itemXml.StatusField = 1 Then 'Si el estado del item es True y pasó todas las validaciones
                        If ListInvoiceCategoriesUser IsNot Nothing AndAlso ListInvoiceCategoriesUser.Count > 0 Then
                            Dim itemAddeed = ListInvoiceCategoriesUser.Find(Function(x) x.UserId = itemXml.UserId)
                            If itemAddeed IsNot Nothing Then
                                Continue For
                            End If
                        End If
                        Dim InvoiceCategoriesUser As New InvoiceCategoriesUser
                        With InvoiceCategoriesUser
                            .UserId = itemXml.UserId
                            .UserCode = itemXml.UserCode
                            .UserFullName = itemXml.UserDescription
                        End With
                        ListInvoiceCategoriesUser.Add(InvoiceCategoriesUser)
                    Else 'Si el estado del item es False y no pasó alguna validación
                        listErrors.Add(New Tuple(Of String, Integer)(itemXml.MessageField, 2))
                    End If
                Next
            End If


            'Se devuelve el mensaje
            Return New ActionResult(Of List(Of InvoiceCategoriesUser), List(Of Tuple(Of String, Integer))) With {.ObjectEmbbeded = ListInvoiceCategoriesUser, .ObjectEmbbededAux = listErrors, .StateResult = True}
        Catch ex As SqlException
            If ex.ErrorCode = -2146232060 Then
                Return New ActionResult(Of List(Of InvoiceCategoriesUser), List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = "Los valores contienen decimales con un formato no valido, por favor corrija para poder continuar"}
            Else
                Return New ActionResult(Of List(Of InvoiceCategoriesUser), List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = ex.ToString}
            End If
        Catch ex As Exception
            Return New ActionResult(Of List(Of InvoiceCategoriesUser), List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = ex.ToString}
        End Try
    End Function

    ''' <summary>
    ''' Convierte el listado de datos a xml
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConvertToXml(data As List(Of List(Of String)))
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<Data>")

        For Each item In data
            builder.Append("<Row>")

            builder.Append("<CountFields>" & item.Count & "</CountFields>")
            builder.Append("<StatusField>" & 0 & "</StatusField>")
            builder.Append("<MessageField>" & "Ok" & "</MessageField>")
            builder.Append("<UserCode>" & item(0) & "</UserCode>")
            builder.Append("<UserDescription>" & "---" & "</UserDescription>")
            builder.Append("<UserId>" & 0 & "</UserId>")

            builder.Append("</Row>")
        Next

        builder.Append("</Data>")
        Return builder.ToString
    End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: elimine el estado administrado (objetos administrados).
            End If
            _invoiceCategories = Nothing
            _billingSequenceD = Nothing
            _billingSequence = Nothing
            _userRepository = Nothing
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
