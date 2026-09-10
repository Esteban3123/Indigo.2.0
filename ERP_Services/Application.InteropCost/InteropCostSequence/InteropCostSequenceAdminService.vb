'***********************************************************************
' Assembly         : Application.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 11-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Application.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base
Imports System.Data.Entity.Core

#End Region

''' <summary>
''' Gestiona los servicios disponibles para todas las operaciones
''' con las secuencias numericas del modulo de costos
''' </summary>
Public Class InteropCostSequenceAdminService
    Implements IInteropCostSequenceAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de la entidad SequenseC
    ''' </summary>
    Private _repositoryC As IInteropCostSequenceRepository
    ''' <summary>
    ''' Repositorio de la entidad SequenseD
    ''' </summary>
    Private _repositoryD As IInteropCostSequenceDetailRepository

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="repositoryC">Repositorio de la entidad SequenseTreasuryC</param>
    ''' <param name="repositoryD">Repositorio de la entidad SequenseTreasuryD</param>
    Public Sub New(ByVal repositoryC As IInteropCostSequenceRepository, ByVal repositoryD As IInteropCostSequenceDetailRepository)
        If repositoryC Is Nothing Then
            Throw New ArgumentNullException("repositoryC")
        End If
        If repositoryC Is Nothing Then
            Throw New ArgumentNullException("repositoryD")
        End If
        Me._repositoryC = repositoryC
        Me._repositoryD = repositoryD
    End Sub

#End Region

#Region "Methods"

    Public Function SaveSequence(seq As InteropCostSecuence) As ActionResult Implements IInteropCostSequenceAdminService.SaveSequence
        Dim UnitOfWorkC As IUnitWork = Me._repositoryC.UnitWork
        Dim UnitOfWorkD As IUnitWork = Me._repositoryD.UnitWork
        Try
            If seq.ChangeTracker.State = ObjectState.Unchanged AndAlso seq.ChangeTracker.ObjectsRemovedFromCollectionProperties IsNot Nothing AndAlso seq.ChangeTracker.ObjectsRemovedFromCollectionProperties.Count > 0 Then
                While seq.ChangeTracker.ObjectsRemovedFromCollectionProperties("InteropCostSecuenceDetail").Count > 0
                    Me._repositoryD.DeleteEntity(seq.ChangeTracker.ObjectsRemovedFromCollectionProperties("InteropCostSecuenceDetail")(0))
                    seq.ChangeTracker.ObjectsRemovedFromCollectionProperties("InteropCostSecuenceDetail").RemoveAt(0)
                End While
                seq.MarkAsModified()
            End If
            _repositoryC.SaveEntity(seq)
            UnitOfWorkD.Commit()
            UnitOfWorkC.Commit()
            Return New ActionResult() With {.StateResult = True}
        Catch ex As Exception
            UnitOfWorkC.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult() With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un grupo de secuencias numericas por su id de configuración
    ''' </summary>
    ''' <param name="id">Id de la configuración de la secuencia</param>
    ''' <returns>
    ''' Grupo de secuencias numericas
    ''' </returns>
    Public Function GetNumericSequenseGroupById(id As Integer) As List(Of String) Implements IInteropCostSequenceAdminService.GetNumericSequenseGroupById
        Try
            Dim unitOfWork As IUnitWork = Me._repositoryD.UnitWork
            Dim seq As InteropCostSecuenceDetail = Me._repositoryD.GetSequenseDById(id)
            If seq.Id > 0 Then
                If Not seq.InteropCostSecuence.Sequential Then
                    Dim list As New List(Of String)()
                    Dim last As Int64 = (seq.Next + seq.InteropCostSecuence.Rate) - 1
                    For i As Int64 = seq.Next To last
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, i)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            list.Add(res)
                        Else
                            Exit For
                        End If
                    Next
                    If list.Count > 0 Then
                        seq.Next = (seq.Next + list.Count)

                        Me._repositoryD.SaveEntity(seq)

                        unitOfWork.Commit()
                    End If

                    Return list
                Else
                    Return New List(Of String)()
                End If
            Else
                Return New List(Of String)()
            End If
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la cabecera de la secuencia por id del frontal
    ''' </summary>
    ''' <param name="idForm"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">idForm</exception>
    Public Function GetSequenseByIdForm(idForm As String) As InteropCostSecuence Implements IInteropCostSequenceAdminService.GetSequenseByIdForm
        If idForm Is Nothing OrElse idForm.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("idForm")
        End If
        Try
            Return Me._repositoryC.GetSequenseByIdForm(idForm.Trim())
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
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
            Me._repositoryC = Nothing
            Me._repositoryD = Nothing
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