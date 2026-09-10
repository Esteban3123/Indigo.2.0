'***********************************************************************
' Assembly         : Application.Accounting
' Author           : Carlos Mario Arias Rubiano
' Created          : 15/12/2015
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
Imports System.Transactions

Public Class VieBotAdminService
    Implements IVieBotAdminService

#Region "Variables"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _vieBotRepository As IVieBotRepository

#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal vieBotRepository As IVieBotRepository)
        If vieBotRepository Is Nothing Then
            Throw New ArgumentNullException("vieBotRepository Vacio")
        End If
        Me._vieBotRepository = vieBotRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene los VieBot
    ''' </summary>
    ''' <param name="Form"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetVieBotByForm(Form As String) As ActionResult(Of List(Of VieBot)) Implements IVieBotAdminService.GetVieBotByForm
        If String.IsNullOrEmpty(Form) Then
            Throw New ArgumentNullException("Form")
        End If
        Try
            Dim ListVieBot As List(Of VieBot) = Me._vieBotRepository.GetVieBotByForm(Form.Trim())
            Return New ActionResult(Of List(Of VieBot)) With {.StateResult = True, .ObjectEmbbeded = ListVieBot}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of VieBot)) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda los VieBot
    ''' </summary>
    ''' <param name="ListVieBot"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveVieBot(ListVieBot As List(Of VieBot), audit As AuditMessage) As ActionResult(Of List(Of VieBot)) Implements IVieBotAdminService.SaveVieBot
        If ListVieBot Is Nothing OrElse ListVieBot.Count = 0 Then
            Throw New ArgumentNullException("ListVieBot")
        End If
        Dim unitOfWork As IUnitWork = Me._vieBotRepository.UnitWork
        Using Transaction As New TransactionScope
            Try
                'Se saca el listado que se envia a guardar
                Dim ListSave As List(Of VieBot) = ListVieBot.FindAll(Function(item) item.Allow = True AndAlso item.Id = 0).ToList
                If ListSave IsNot Nothing AndAlso ListSave.Count > 0 Then
                    'Se le asignan los campos de auditoria
                    ListSave.ForEach(Sub(item)
                                         item.CreationDate = DateTime.Now
                                         item.CreationUser = audit.CodeUser
                                     End Sub)
                    'Se envia a guardar
                    For Each item In ListSave
                        Me._vieBotRepository.SaveEntity(item)
                        unitOfWork.Commit()
                    Next
                End If

                Dim ListDelete As List(Of VieBot) = ListVieBot.FindAll(Function(item) item.Allow = False AndAlso item.Id > 0).ToList
                If ListDelete IsNot Nothing AndAlso ListDelete.Count > 0 Then
                    For Each item In ListDelete
                        Me._vieBotRepository.DeleteEntity(item)
                        unitOfWork.Commit()
                    Next
                End If

                Transaction.Complete()
                Return New ActionResult(Of List(Of VieBot)) With {.StateResult = True, .ObjectEmbbeded = ListVieBot}
            Catch ex As OptimisticConcurrencyException
                Transaction.Dispose()
                unitOfWork.RollbackChanges()
                Return New ActionResult(Of List(Of VieBot)) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
            Catch ex As Exception
                Transaction.Dispose()
                unitOfWork.RollbackChanges()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of List(Of VieBot)) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
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
            _vieBotRepository = Nothing
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
