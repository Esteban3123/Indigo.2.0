'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Ruben Dario Castañeda Giraldo
' Created          : 06-05-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources
Imports System.Data.Entity.Core
Imports System.Data.Entity.Infrastructure
Imports System.Transactions
#End Region
Public Class CMCenterAdminAttention
    Implements ICMCenterAttention, Inject

#Region "Properties"

    Private Const FORM_NAME As String = "FrmCMConfigure"

    Private _cmCenterAttention As ICMCenterAttentionRepository
    ''' <summary>
    ''' Repositorio de secuencias numéricas
    ''' </summary>
    Private _secuenseDetailRepository As IMixingStationSequenceDetailRepository

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    Private _productionLineRepository As ICMMixingProducitonLineRepository


#End Region

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal cmCenterAttentionRepository As ICMCenterAttentionRepository, ByVal productionLineRepository As ICMMixingProducitonLineRepository, ByVal secuenseDetailRepository As IMixingStationSequenceDetailRepository)
        If cmCenterAttentionRepository Is Nothing Then
            Throw New ArgumentNullException("workCenterRepository Vacio")
        End If
        If productionLineRepository Is Nothing Then
            Throw New ArgumentNullException("workCenterRepository Vacio")
        End If
        If secuenseDetailRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDetailRepository")
        End If
        Me._cmCenterAttention = cmCenterAttentionRepository
        Me._secuenseDetailRepository = secuenseDetailRepository
        Me._productionLineRepository = productionLineRepository
    End Sub

    ''' <summary>
    ''' Obtiene los tipos de dosis unitarias
    ''' </summary>
    ''' <param name="Id_MixingStation">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    ''' <returns></returns>
    Public Function ListAllCMMixingProducitonLine(Id_MixingStation As Integer, audit As AuditMessage) As List(Of Tuple(Of Integer, String)) Implements ICMCenterAttention.ListAllCMMixingProducitonLine
        If Id_MixingStation = 0 Then
            Throw New ArgumentNullException("Id_MixingStation")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim productionLineUnit = Me._productionLineRepository.ListAllCMMixingProducitonLine(Id_MixingStation)

            Return productionLineUnit
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Trae todos los parametros de centros de atencion
    ''' </summary>
    ''' <returns></returns>
    ''' 
    Public Function ListAllCMCenterAttention(audit As AuditMessage) As List(Of CMCenterAttention) Implements ICMCenterAttention.ListAllCMCenterAttention
        Try
            Dim cmCenterAttention = Me._cmCenterAttention.GetAll()
            For Each item As CMCenterAttention In cmCenterAttention
                Dim auditObject As New IndigoAuditSimpleEntity(Of CMCenterAttention)(item, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            Next
            Return cmCenterAttention
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try

    End Function

    ''' <summary>
    '''Guarda la asociacion entre el centro de atencion y la central de mezclas
    ''' </summary>
    ''' <param name="cmCenterAttention"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveCMCenterAttention(cmCenterAttention As CMCenterAttention) As ActionResult(Of CMCenterAttention) Implements ICMCenterAttention.SaveCMCenterAttention

        If cmCenterAttention Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._cmCenterAttention.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty
                If String.IsNullOrEmpty(cmCenterAttention.Id) Then
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If
                Me._cmCenterAttention.SaveEntity(cmCenterAttention)
                unitOfWork.Commit()
                cmCenterAttention.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of CMCenterAttention) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = cmCenterAttention, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of CMCenterAttention) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CMCenterAttention) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try

    End Function

    ''' <summary>
    ''' Obtiene un Central de mezclas por codigo
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Function GetCMCenterAttention(id As String) As ActionResult(Of CMCenterAttention) Implements ICMCenterAttention.GetCMCenterAttention
        If String.IsNullOrEmpty(id) Then
            Throw New ArgumentNullException("id")
        End If
        Try
            Dim cmCenterAttention As CMCenterAttention = Me._cmCenterAttention.GetCMCenterAttention(id)
            Return New ActionResult(Of CMCenterAttention) With {.StateResult = True, .ObjectEmbbeded = cmCenterAttention}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CMCenterAttention) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    Public Function UpdateStateCMCenterAttention(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of CMCenterAttention) Implements ICMCenterAttention.UpdateStateCMCenterAttention
        Throw New NotImplementedException()
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If

            _cmCenterAttention = Nothing
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
