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
Public Class NPTConfigurationAdminService
    Implements INPTConfigurationAdminService, Inject

#Region "Properties"
    Private Const FORM_NAME As String = "FrmUnitDoseType"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _nPTConfigurationRepository As INPTConfigurationRepository

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
    Public Sub New(ByVal NPTConfigurationRepository As INPTConfigurationRepository, ByVal secuenseDetailRepository As IMixingStationSequenceDetailRepository)
        If NPTConfigurationRepository Is Nothing Then
            Throw New ArgumentNullException("workCenterRepository Vacio")
        End If
        If secuenseDetailRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDetailRepository")
        End If
        Me._nPTConfigurationRepository = NPTConfigurationRepository
        Me._secuenseDetailRepository = secuenseDetailRepository
    End Sub

    ''' <summary>
    ''' Trae todo
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllNPTConfiguration(audit As AuditMessage) As List(Of NPTConfiguration) Implements INPTConfigurationAdminService.ListAllNPTConfiguration
        Try

            Dim NPTConfiguration = Me._nPTConfigurationRepository.ListAllNPTConfiguration()
            If NPTConfiguration Is Nothing Then
                Return New List(Of NPTConfiguration)
            End If

            Return NPTConfiguration
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Elimina detalles
    ''' </summary>
    ''' <param name="ListNPTConfiguration"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function DeleteNPTConfiguration(ListNPTConfiguration As List(Of NPTConfiguration), audit As AuditMessage) As ActionResult(Of List(Of NPTConfiguration)) Implements INPTConfigurationAdminService.DeleteNPTConfiguration
        If ListNPTConfiguration Is Nothing Or ListNPTConfiguration.Count = 0 Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._nPTConfigurationRepository.UnitWork

        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty
                ListNPTConfiguration.ForEach(Sub(x)
                                                 x.MarkAsDeleted()
                                                 Me._nPTConfigurationRepository.SaveEntity(x)

                                             End Sub)

                unitOfWork.Commit()
                Dim ListNPTConfigurationRefresh = ListAllNPTConfiguration(audit)

                scope.Complete()

                Return New ActionResult(Of List(Of NPTConfiguration)) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = ListNPTConfigurationRefresh, .Message = MessageResult}
            End Using


        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of NPTConfiguration)) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}

        End Try
    End Function


    ''' <summary>
    ''' Guarda,actualiza o elimina
    ''' </summary>
    ''' <param name="NPTConfiguration"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveNPTConfiguration(NPTConfiguration As NPTConfiguration, audit As AuditMessage) As ActionResult(Of List(Of NPTConfiguration)) Implements INPTConfigurationAdminService.SaveNPTConfiguration
        If NPTConfiguration Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._nPTConfigurationRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty
                If Validations(NPTConfiguration) Then
                    Me._nPTConfigurationRepository.SaveEntity(NPTConfiguration)
                    unitOfWork.Commit()
                Else
                    Return New ActionResult(Of List(Of NPTConfiguration)) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = "El rango de pesos ya existe "}
                End If

                Dim ListNPTConfiguration = ListAllNPTConfiguration(audit)

                scope.Complete()
                Return New ActionResult(Of List(Of NPTConfiguration)) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = ListNPTConfiguration, .Message = MessageResult}
            End Using
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of NPTConfiguration)) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try

    End Function

    ''' <summary>
    ''' valida que no se solapen los pesos
    ''' </summary>
    ''' <param name="NPTConfiguration"></param>
    ''' <returns></returns>
    Function Validations(NPTConfiguration As NPTConfiguration) As Boolean

        If NPTConfiguration Is Nothing Then
            Return False
        End If
        Dim ListToValidate = _nPTConfigurationRepository.GetAll()
        If ListToValidate.Any(Function(x) (x.InitialWeight <= NPTConfiguration.EndWeight And x.EndWeight >= NPTConfiguration.InitialWeight)) Then
            Return False
        Else
            Return True
        End If
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If

            _nPTConfigurationRepository = Nothing
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
