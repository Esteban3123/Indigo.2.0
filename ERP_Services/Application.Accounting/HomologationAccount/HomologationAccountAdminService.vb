'***********************************************************************
' Assembly         : Application.Accounting
' Author           : Carlos Mario Arias Rubiano
' Created          : 01/12/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Base
Imports Application.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports System.Data.Entity.Core
Imports System.Data.Entity.Infrastructure
Imports System.Transactions

#End Region

Public Class HomologationAccountAdminService
    Implements IHomologationAccountAdminService

#Region "Variables"

    ''' <summary>
    ''' Repositorio de homologación de cuentas
    ''' </summary>
    ''' <remarks></remarks>
    Private _homologationAccountRepository As IHomologationAccountRepository

#End Region

#Region "Builder"
    Public Sub New(homologationAccountRepository As IHomologationAccountRepository)
        If homologationAccountRepository Is Nothing Then
            Throw New ArgumentNullException("homologationAccountRepository vacío")
        End If
        _homologationAccountRepository = homologationAccountRepository
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Guarda una homologación de cuentas
    ''' </summary>
    ''' <param name="ListHomologationAccount"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveHomologationAccount(ListHomologationAccount As List(Of HomologationAccount), audit As AuditMessage) As ActionResult Implements IHomologationAccountAdminService.SaveHomologationAccount
        If ListHomologationAccount Is Nothing OrElse ListHomologationAccount.Count = 0 Then
            Throw New ArgumentNullException("ListHomologationAccount")
        End If
        Using Transaction As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                'Se asigna la auditoria
                ListHomologationAccount.ForEach(Sub(item)
                                                    If item.Id = 0 Then 'Si se esta agregando
                                                        item.CreationUser = audit.CodeUser
                                                        item.CreationDate = DateTime.Now
                                                    Else 'Si se esta modificando
                                                        item.ModificationUser = audit.CodeUser
                                                        item.ModificationDate = DateTime.Now
                                                    End If
                                                End Sub)

                'Se crea el xml con el listado
                Dim xmlObject = New XElement("HomologationAccounts", _
                                            From item In ListHomologationAccount
                                            Select New XElement("HomologationAccount", New XElement("Id", item.Id),
                                                                New XElement("OfficialMainAccountId", item.OfficialMainAccountId),
                                                                New XElement("MainAccountId", item.MainAccountId),
                                                                New XElement("CreationUser", item.CreationUser),
                                                                New XElement("CreationDate", item.CreationDate)))


                'Se envia a guardar
                Dim resultStore = _homologationAccountRepository.SaveHomologationAccount(xmlObject.ToString)
                If resultStore.CodeMessage <> 0 Then
                    Transaction.Dispose()
                    Return New ActionResult With {.StateResult = False, .Message = resultStore.Message}
                End If

                Transaction.Complete()
                Return New ActionResult With {.StateResult = True}
            Catch ex As Exception
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult With {.StateResult = False, .Message = ex.Message}
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
            Me._homologationAccountRepository = Nothing
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
