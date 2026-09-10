'***********************************************************************
' Assembly         : Application.FixedAsset
' Author           : Juan F. Tamayo
' Created          : 2014-01-15
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
''' con las secuencias numericas del modulo de contabilidad
''' </summary>
Public Class FixedAssetSequenseAdminService
    Implements IFixedAssetSequenseAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de la entidad FixedAssetSequence
    ''' </summary>
    Private _repositoryC As IFixedAssetSequenceRepository
    ''' <summary>
    ''' Repositorio de la entidad FixedAssetSequenceDetail
    ''' </summary>
    Private _repositoryD As IFixedAssetSequenceDetailRepository

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="repositoryC">Repositorio de la entidad FixedAssetSequence</param>
    ''' <param name="repositoryD">Repositorio de la entidad FixedAssetSequenceDetail</param>
    Public Sub New(ByVal repositoryC As IFixedAssetSequenceRepository, ByVal repositoryD As IFixedAssetSequenceDetailRepository)
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

    Public Function SaveSequence(seq As FixedAssetSequence) As ActionResult Implements IFixedAssetSequenseAdminService.SaveSequence
        Dim UnitOfWorkC As IUnitWork = Me._repositoryC.UnitWork
        Dim UnitOfWorkD As IUnitWork = Me._repositoryD.UnitWork
        Try
            If seq.ChangeTracker.State = ObjectState.Unchanged AndAlso seq.ChangeTracker.ObjectsRemovedFromCollectionProperties IsNot Nothing AndAlso seq.ChangeTracker.ObjectsRemovedFromCollectionProperties.Count > 0 Then
                While seq.ChangeTracker.ObjectsRemovedFromCollectionProperties("FixedAssetSequenceDetail").Count > 0
                    Me._repositoryD.DeleteEntity(seq.ChangeTracker.ObjectsRemovedFromCollectionProperties("FixedAssetSequenceDetail")(0))
                    seq.ChangeTracker.ObjectsRemovedFromCollectionProperties("FixedAssetSequenceDetail").RemoveAt(0)
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
    ''' Obtiene la configuración de secuencia numerica asignada al frontal
    ''' </summary>
    ''' <param name="idForm">Id del frontal a consultar</param>
    ''' <returns>Secuencia numerica asignada al frontal</returns>
    Public Function GetSequenseByIdForm(idForm As String) As Domain.Entities.FixedAssetSequence Implements IFixedAssetSequenseAdminService.GetSequenseByIdForm
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

    ''' <summary>
    ''' Obtiene un grupo de secuencias numericas por su id de configuración
    ''' </summary>
    ''' <param name="id">Id de la configuración de la secuencia</param>
    ''' <returns>Grupo de secuencias numericas</returns>
    Public Function GetNumericSequenseGroupById(id As Integer) As List(Of String) Implements IFixedAssetSequenseAdminService.GetNumericSequenseGroupById
        Try
            Dim unitOfWork As IUnitWork = Me._repositoryD.UnitWork
            Dim seq As FixedAssetSequenceDetail = Me._repositoryD.GetSequenseDById(id)
            If seq.Id > 0 Then
                If Not seq.FixedAssetSequence.Sequential Then
                    Dim list As New List(Of String)()
                    Dim last As Int64 = (seq.Next + seq.FixedAssetSequence.Rate) - 1
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
