'***********************************************************************
' Assembly         : Application.Budget
' Author           : Juan F. Tamayo
' Created          : 2014-01-15
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Base.Extentions
Imports Domain.Base.Entities
Imports Application.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base
Imports System.Data.Entity.Core
Imports System.Configuration

#End Region

Public Class BillingSequenseAdminService
    Implements IBillingSequenseAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de la entidad BillingSequence
    ''' </summary>
    Private _repositoryC As IBillingSequenceRepository
    ''' <summary>
    ''' Repositorio de la entidad BudgetSequenceDetail
    ''' </summary>
    Private _repositoryD As IBillingSequenceDetailRepository

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="repositoryC">Repositorio de la entidad BillingSequence</param>
    ''' <param name="repositoryD">Repositorio de la entidad BudgetSequenceDetail</param>
    Public Sub New(ByVal repositoryC As IBillingSequenceRepository, ByVal repositoryD As IBillingSequenceDetailRepository)
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
    Public Function GetSequenseByIdForm(idForm As String) As Domain.Entities.BillingSequence Implements IBillingSequenseAdminService.GetSequenseByIdForm
        Dim transactionContainer As String = ServerSessionValues.Current.CurrentContainer

        If idForm Is Nothing OrElse idForm.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("idForm")
        End If
        Try

            'Dim sql As New Text.StringBuilder()

            'sql.AppendLine(" SELECT * FROM Billing.BillingSequence BS													 ")
            'sql.AppendLine(" INNER JOIN Billing.BillingSequenceDetail BSD WITH(NOLOCK) ON BS.Id = BSD.IdSequenseBillingC ")
            'sql.AppendLine(" INNER JOIN Common.Sequense SQ WITH(NOLOCK) ON BSD.IdSequense = SQ.Id						 ")
            'sql.AppendLine(" INNER JOIN Common.OperatingUnit OU WITH(NOLOCK) ON BSD.IdOperatingUnit = OU.Id				 ")
            'sql.AppendLine(" WHERE IdForm = '" & idForm & "'    																	 ")

            'Dim dtDatos As New DataTable("BillingSequence")
            'Dim conx = String.Format(ConfigurationManager.ConnectionStrings(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS).ConnectionString, "", transactionContainer)
            'Dim conexion As New SqlClient.SqlConnection(conx)
            'Dim da As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter(sql.ToString(), conexion)
            'da.SelectCommand.CommandTimeout = 90

            'Dim ds As New DataSet
            'da.Fill(ds, "BillingSequence")
            'dtDatos = ds.Tables("BillingSequence")
            'conexion.Close()

            'If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
            '    Dim row As DataRow = ds.Tables(0).Rows(0)
            '    Dim bs As New BillingSequence() With {
            '        .Id = row("Id").ToString().AsInt(),
            '        .IdForm = row("IdForm").ToString(),
            '        .IsManual = row("IsManual").ToString().AsBoolean(),
            '        .Scope = row("Scope").ToString().AsString(),
            '        .Sequential = row("Sequential").ToString().AsBoolean(),
            '        .Rate = row("Rate").ToString().AsByte()
            '    }

            '    For Each i As DataRow In ds.Tables(0).Rows
            '        Dim bsd As New BillingSequenceDetail()

            '        bsd.Sequense = New Domain.Entities.Sequense()
            '        With bsd.Sequense
            '            .Id = i("Id2").ToString().AsInt()
            '            .Name = i("Name").ToString().AsString()
            '            .Pattern = i("Pattern").ToString().AsString()
            '        End With

            '        bsd.OperatingUnit = New OperatingUnit()
            '        With bsd.OperatingUnit
            '            .Id = i("Id3").ToString().AsInt()
            '            .IdUnit = If(i("IdUnit").Equals(DBNull.Value), CType(Nothing, Integer?), i("IdUnit").ToString().AsInt())
            '            .UnitCode = i("UnitCode").ToString().AsString()
            '            .UnitName = i("UnitName").ToString().AsString()
            '            .IPSCode = i("IPSCode").ToString().AsString()
            '            .Address = i("Address").ToString().AsString()
            '            .Phone = i("Phone").ToString().AsString()
            '            .Email = i("Email").ToString().AsString()
            '            .EmailAudit = If(i("EmailAudit").Equals(DBNull.Value), Nothing, i("EmailAudit").ToString().AsString())
            '            .IdCity = i("IdCity").ToString().AsInt()
            '        End With

            '        With bsd
            '            .Id = i("Id1").ToString().AsInt()
            '            .IdSequenseBillingC = i("IdSequenseBillingC").ToString().AsInt()
            '            .IdSequense = i("IdSequense").ToString().AsInt()
            '            .IdOperatingUnit = i("IdOperatingUnit").ToString().AsInt()
            '            .Next = i("Next").ToString().AsInt64()
            '        End With

            '        bs.BillingSequenceDetail.Add(bsd)
            '    Next

            '    Return bs
            'Else
            '    Return Nothing
            'End If

            Return Me._repositoryC.GetSequenseByIdForm(idForm.Trim())
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveSequence(seq As BillingSequence) As ActionResult Implements IBillingSequenseAdminService.SaveSequence
        Dim UnitOfWorkC As IUnitWork = Me._repositoryC.UnitWork
        Dim UnitOfWorkD As IUnitWork = Me._repositoryD.UnitWork
        Try
            If seq.ChangeTracker.State = ObjectState.Unchanged AndAlso seq.ChangeTracker.ObjectsRemovedFromCollectionProperties IsNot Nothing AndAlso seq.ChangeTracker.ObjectsRemovedFromCollectionProperties.Count > 0 Then
                While seq.ChangeTracker.ObjectsRemovedFromCollectionProperties("BillingSequenceDetail").Count > 0
                    Me._repositoryD.DeleteEntity(seq.ChangeTracker.ObjectsRemovedFromCollectionProperties("BillingSequenceDetail")(0))
                    seq.ChangeTracker.ObjectsRemovedFromCollectionProperties("BillingSequenceDetail").RemoveAt(0)
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
    ''' <returns>Grupo de secuencias numericas</returns>
    Public Function GetNumericSequenseGroupById(id As Integer) As List(Of String) Implements IBillingSequenseAdminService.GetNumericSequenseGroupById
        Try
            Dim unitOfWork As IUnitWork = Me._repositoryD.UnitWork
            Dim seq As BillingSequenceDetail = Me._repositoryD.GetSequenseDById(id)
            If seq.Id > 0 Then
                If Not seq.BillingSequence.Sequential Then
                    Dim list As New List(Of String)()
                    Dim last As Int64 = (seq.Next + seq.BillingSequence.Rate) - 1
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
                ' TODO: elimine el estado administrado (objetos administrados).
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
