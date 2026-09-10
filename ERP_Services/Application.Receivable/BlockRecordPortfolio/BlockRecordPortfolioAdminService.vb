'***********************************************************************
' Assembly         : Application.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 04-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
#End Region

Public Class BlockRecordPortfolioAdminService
    Implements IBlockRecordPortfolioAdminService

#Region "Fields"
    ''' <summary>
    ''' Repositorio de la entidad ciudades bancarias
    ''' </summary>
    Private _Repository As IBlockRecordPortfolioRepository

#End Region

#Region "Builder"
    Public Sub New(ByVal Repository As IBlockRecordPortfolioRepository)
        If Repository Is Nothing Then
            Throw New ArgumentNullException("repository")
        End If
        _Repository = Repository
    End Sub
#End Region
    
#Region "Methods"
    ''' <summary>
    ''' Elimina una registro bloqueado
    ''' </summary>
    ''' <param name="blockRecordPortfolio"></param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>
    ''' ActionResult
    ''' </returns>
    ''' <exception cref="System.ArgumentNullException">blockRecordTreasury</exception>
    Public Function DeleteBlockRecordPortfolio(blockRecordPortfolio As BlockRecordPortfolio) As ActionResult Implements IBlockRecordPortfolioAdminService.DeleteBlockRecordPortfolio
        If blockRecordPortfolio Is Nothing Then
            Throw New ArgumentNullException("blockRecordTreasury")
        End If
        Dim unitOfWork As IUnitWork = Me._Repository.UnitWork
        Try

            Me._Repository.DeleteEntity(blockRecordPortfolio)
            unitOfWork.Commit()

            Return New ActionResult With {.StateResult = True}

        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Gets the block record portfolio by idform and identifier record.
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <param name="IdRecord">The identifier record.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">IdForm IdRecord</exception>
    Public Function GetBlockRecordPortfolioByIdformAndIdRecord(IdForm As String, IdRecord As String) As BlockRecordPortfolio Implements IBlockRecordPortfolioAdminService.GetBlockRecordPortfolioByIdformAndIdRecord
        If String.IsNullOrEmpty(IdForm) Or String.IsNullOrEmpty(IdRecord) Then
            Throw New ArgumentNullException("IdForm IdRecord")
        End If
        Try
            Dim blockRecordPortfolio As BlockRecordPortfolio = Me._Repository.GetBlockRecordPortfolioByIdformAndIdRecord(IdForm, IdRecord)
            Return blockRecordPortfolio
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Almacena o Actualiza registro bloqueado
    ''' </summary>
    ''' <param name="blockRecordPortfolio"></param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>
    ''' ActionResult
    ''' </returns>
    ''' <exception cref="System.ArgumentNullException">blockRecordTreasury Vacio</exception>
    Public Function SaveBlockRecordPortfolio(blockRecordPortfolio As BlockRecordPortfolio) As ActionResult(Of BlockRecordPortfolio) Implements IBlockRecordPortfolioAdminService.SaveBlockRecordPortfolio
        If blockRecordPortfolio Is Nothing Then
            Throw New ArgumentNullException("blockRecordTreasury Vacio")
        End If

        Dim UnitOfWork As IUnitWork = _Repository.UnitWork
        Try

            _Repository.SaveEntity(blockRecordPortfolio)
            UnitOfWork.Commit()

            Return New ActionResult(Of BlockRecordPortfolio) With {.StateResult = True, .ObjectEmbbeded = blockRecordPortfolio}
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return New ActionResult(Of BlockRecordPortfolio) With {.StateResult = False}
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
            _Repository = Nothing
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
