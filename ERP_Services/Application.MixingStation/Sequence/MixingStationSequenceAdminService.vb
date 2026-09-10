'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Yoe Andres Cardenas
' Created          : 25/04/2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base

#End Region

Public Class MixingStationSequenceAdminService
    Implements IMixingStationSequenceAdminService, Inject

#Region "Fields"

    ''' <summary>
    ''' Repositorio de la entidad SequencePaymentsC
    ''' </summary>
    Private _repositoryC As IMixingStationSequenceRepository
    ''' <summary>
    ''' Repositorio de la entidad SequencePaymentsD
    ''' </summary>
    Private _repositoryD As IMixingStationSequenceDetailRepository

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="repositoryC">Repositorio de la entidad SequencePaymentsC</param>
    ''' <param name="repositoryD">Repositorio de la entidad SequencePaymentsD</param>
    Public Sub New(ByVal repositoryC As IMixingStationSequenceRepository, ByVal repositoryD As IMixingStationSequenceDetailRepository)
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

    ''' <summary>
    ''' Obtiene la configuración de secuencia numerica asignada al frontal
    ''' </summary>
    ''' <param name="idForm">Id del frontal a consultar</param>
    ''' <returns>Secuencia numerica asignada al frontal</returns>
    Public Function GetSequenceByIdForm(idForm As String) As MixingStationSequence Implements IMixingStationSequenceAdminService.GetSequenceByIdForm
        If idForm Is Nothing OrElse idForm.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("idForm")
        End If
        Try
            Return Me._repositoryC.GetSequenceByIdForm(idForm.Trim())
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
    Public Function GetNumericSequenceGroupById(id As Integer) As List(Of String) Implements IMixingStationSequenceAdminService.GetNumericSequenceGroupById
        Try
            Dim unitOfWork As IUnitWork = Me._repositoryD.UnitWork
            Dim seq As MixingStationSequenceDetail = Me._repositoryD.GetSequenceDById(id)
            If seq.Id > 0 Then
                If Not seq.MixingStationSequence.Sequential Then
                    Dim list As New List(Of String)()
                    Dim last As Int64 = (seq.Next + seq.MixingStationSequence.Rate) - 1
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

    Public Function SaveSequence(seq As MixingStationSequence) As ActionResult Implements IMixingStationSequenceAdminService.SaveSequence
        Dim UnitOfWorkC As IUnitWork = Me._repositoryC.UnitWork
        Dim UnitOfWorkD As IUnitWork = Me._repositoryD.UnitWork
        Try
            If seq.ChangeTracker.State = ObjectState.Unchanged AndAlso seq.ChangeTracker.ObjectsRemovedFromCollectionProperties IsNot Nothing AndAlso seq.ChangeTracker.ObjectsRemovedFromCollectionProperties.Count > 0 Then
                While seq.ChangeTracker.ObjectsRemovedFromCollectionProperties("MixingStationSequenceDetail").Count > 0
                    Me._repositoryD.DeleteEntity(seq.ChangeTracker.ObjectsRemovedFromCollectionProperties("MixingStationSequenceDetail")(0))
                    seq.ChangeTracker.ObjectsRemovedFromCollectionProperties("MixingStationSequenceDetail").RemoveAt(0)
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

    Public Function GetCurrentSequenceByIdForm(idForm As String, operativeUnitId As Integer) As Long Implements IMixingStationSequenceAdminService.GetCurrentSequenceByIdForm
        Dim _currentSequence As Long = 0
        Dim sequence = GetSequenceByIdForm(idForm)

        If (sequence.Scope.Equals("O")) Then
            _currentSequence = sequence.MixingStationSequenceDetail(0).Id
        ElseIf (sequence.Scope.Equals("OU") AndAlso sequence.MixingStationSequenceDetail.Any(Function(m) m.IdOperatingUnit = operativeUnitId)) Then
            _currentSequence = sequence.MixingStationSequenceDetail.FirstOrDefault(Function(m) m.IdOperatingUnit = operativeUnitId).Id
        End If

        Return _currentSequence
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
