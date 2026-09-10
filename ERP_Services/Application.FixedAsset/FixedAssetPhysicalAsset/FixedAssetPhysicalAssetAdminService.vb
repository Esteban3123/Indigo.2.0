#Region "Imports"
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports System.Data.Entity.Core
Imports System.Transactions
Imports System.Text
Imports Domain.Entities.Service
Imports Application.Payments
Imports Application.Accounting
Imports Application.Portfolio
Imports Application.FixedAsset
Imports System.Data.SqlClient
Imports System.Data.Entity.Infrastructure

#End Region

Public Class FixedAssetPhysicalAssetAdminService
    Implements IFixedAssetPhysicalAssetAdminService

#Region "Variables"

    'Repositorio de la aseguradora
    Private _fixedAssetPhysicalAssetRepository As IFixedAssetPhysicalAssetRepository

#End Region

#Region "Builder"

    ''' <summary>
    ''' inicia el repositorio de bancos
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(fixedAssetPhysicalAssetRepository As IFixedAssetPhysicalAssetRepository)
        If fixedAssetPhysicalAssetRepository Is Nothing Then
            Throw New ArgumentNullException("fixedAssetPhysicalAssetRepository")
        End If
        _fixedAssetPhysicalAssetRepository = fixedAssetPhysicalAssetRepository
    End Sub

    ''' <summary>
    ''' Envia los parametros al store [FixedAsset].[SP_ReportFixedAssetBalance] y obtiene el balance de activos fijos
    ''' </summary>
    ''' <param name="Year"></param>
    ''' <param name="Month"></param>
    ''' <param name="InitialPlate"></param>
    ''' <param name="FinalPlate"></param>
    ''' <param name="InitialCatalog"></param>
    ''' <param name="FinalCatalog"></param>
    ''' <param name="InitialGroup"></param>
    ''' <param name="FinalGroup"></param>
    ''' <param name="InitialLocation"></param>
    ''' <param name="FinalLocation"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function GetFixedAssetBalance(Year As Integer, Month As Integer, InitialPlate As String, FinalPlate As String, InitialCatalog As String, FinalCatalog As String, InitialGroup As String, FinalGroup As String, InitialLocation As String, FinalLocation As String, session As SessionValues) As DataSet Implements IFixedAssetPhysicalAssetAdminService.GetFixedAssetBalance
        Try
            Dim ds As New DataSet
            Dim query1 As String = String.Empty

            If InitialPlate Is Nothing Or FinalPlate Is Nothing Then
                InitialPlate = "0"
                FinalPlate = "Z"
            End If
            If InitialCatalog Is Nothing Or FinalCatalog Is Nothing Then
                InitialCatalog = "0"
                FinalCatalog = "Z"
            End If
            If InitialGroup Is Nothing Or FinalGroup Is Nothing Then
                InitialGroup = "0"
                FinalGroup = "Z"
            End If
            If InitialLocation Is Nothing Or FinalLocation Is Nothing Then
                InitialLocation = "0"
                FinalLocation = "Z"
            End If
            query1 = "exec [FixedAsset].[SP_ReportFixedAssetBalance] '" & Year & "','" & Month & "','" & InitialPlate & "','" & FinalPlate & "','" & InitialCatalog & "','" & FinalCatalog & "','" & InitialGroup & "','" & FinalGroup & "','" & InitialLocation & "','" & FinalLocation & "'"
            Dim dt1 = Me.GetDatatable(query1, session, "ReportFixedAssetBalance")
            ds.Tables.Add(dt1.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Envia los parametros al store [FixedAsset].[SP_ReportFixedAssetKardex] y obtiene el kardex de activos fijos
    ''' </summary>
    ''' <param name="Year"></param>
    ''' <param name="Month"></param>
    ''' <param name="InitialPlate"></param>
    ''' <param name="FinalPlate"></param>
    ''' <param name="InitialCatalog"></param>
    ''' <param name="FinalCatalog"></param>
    ''' <param name="InitialGroup"></param>
    ''' <param name="FinalGroup"></param>
    ''' <param name="InitialLocation"></param>
    ''' <param name="FinalLocation"></param>
    ''' <param name="InitialResponsible"></param>
    ''' <param name="FinalResponsible"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function GetFixedAssetKardex(Year As Integer, Month As Integer, InitialPlate As String, FinalPlate As String, InitialCatalog As String, FinalCatalog As String, InitialGroup As String, FinalGroup As String, InitialLocation As String, FinalLocation As String, InitialResponsible As String, FinalResponsible As String, session As SessionValues) As DataSet Implements IFixedAssetPhysicalAssetAdminService.GetFixedAssetKardex
        Try
            Dim ds As New DataSet
            Dim query1 As String = String.Empty

            If InitialPlate Is Nothing Or FinalPlate Is Nothing Then
                InitialPlate = "0"
                FinalPlate = "Z"
            End If
            If InitialCatalog Is Nothing Or FinalCatalog Is Nothing Then
                InitialCatalog = "0"
                FinalCatalog = "Z"
            End If
            If InitialGroup Is Nothing Or FinalGroup Is Nothing Then
                InitialGroup = "0"
                FinalGroup = "Z"
            End If
            If InitialLocation Is Nothing Or FinalLocation Is Nothing Then
                InitialLocation = "0"
                FinalLocation = "Z"
            End If
            If InitialResponsible Is Nothing Or FinalResponsible Is Nothing Then
                InitialResponsible = "0"
                FinalResponsible = "Z"
            End If
            query1 = "exec [FixedAsset].[SP_ReportFixedAssetKardex] '" & Year & "','" & Month & "','" & InitialPlate & "','" & FinalPlate & "','" & InitialCatalog & "','" & FinalCatalog & "','" & InitialGroup & "','" & FinalGroup & "','" & InitialLocation & "','" & FinalLocation & "','" & InitialResponsible & "','" & FinalResponsible & "'"
            Dim dt1 = Me.GetDatatable(query1, session, "ReportFixedAssetKardex")
            ds.Tables.Add(dt1.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
            Return Nothing
        End Try
    End Function

    Public Function GetDatatable(ByVal Comando As String, session As SessionValues, nameDt As String) As System.Data.DataTable
        Dim connectionString = String.Empty
        connectionString = Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, session.TransactionalContainer, False)
        Using conexion As New SqlConnection(connectionString)
            Try
                If conexion.State = ConnectionState.Closed Then
                    conexion.Open()
                End If
                Dim da As SqlDataAdapter = New SqlDataAdapter(Comando, conexion)
                da.SelectCommand.CommandTimeout = 30000
                Dim ds As New DataSet
                da.Fill(ds, nameDt)
                GetDatatable = ds.Tables(nameDt)
                da = Nothing
                ds = Nothing
                conexion.Close()
                Return GetDatatable
            Catch ex As Exception
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
                Return Nothing
            Finally
                conexion.Close()
            End Try
        End Using
    End Function

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene un registro por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetPhysicalAssetById(Id As Integer) As FixedAssetPhysicalAsset Implements IFixedAssetPhysicalAssetAdminService.GetFixedAssetPhysicalAssetById
        Try
            Return Me._fixedAssetPhysicalAssetRepository.GetPhysicalAssetById(Id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New FixedAssetPhysicalAsset
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un registro por placa
    ''' </summary>
    ''' <param name="Plate"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetPhysicalAssetByPlate(Plate As String, audit As AuditMessage) As ActionResult(Of FixedAssetPhysicalAsset) Implements IFixedAssetPhysicalAssetAdminService.GetFixedAssetPhysicalAssetByPlate
        If String.IsNullOrEmpty(Plate) Then
            Throw New ArgumentNullException("Plate")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim FixedAssetPhysicalAsset As FixedAssetPhysicalAsset = Me._fixedAssetPhysicalAssetRepository.GetFixedAssetPhysicalAssetByPlate(Plate.Trim())
            If FixedAssetPhysicalAsset IsNot Nothing AndAlso FixedAssetPhysicalAsset.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of FixedAssetPhysicalAsset)(FixedAssetPhysicalAsset, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of FixedAssetPhysicalAsset) With {.StateResult = True, .ObjectEmbbeded = FixedAssetPhysicalAsset}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FixedAssetPhysicalAsset) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda un registro
    ''' </summary>
    ''' <param name="FixedAssetTransfer"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveFixedAssetPhysicalAsset(FixedAssetPhysicalAsset As FixedAssetPhysicalAsset, audit As AuditMessage) As ActionResult(Of FixedAssetPhysicalAsset) Implements IFixedAssetPhysicalAssetAdminService.SaveFixedAssetPhysicalAsset
        If FixedAssetPhysicalAsset Is Nothing Then
            Throw New ArgumentNullException("FixedAssetPhysicalAsset")
        End If
        Dim unitOfWork As IUnitWork = Me._fixedAssetPhysicalAssetRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim auxFixedAssetPhysicalAsset As FixedAssetPhysicalAsset = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of FixedAssetPhysicalAsset)
                Dim status As Integer

                If FixedAssetPhysicalAsset.ChangeTracker.State = ObjectState.Added Then
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                ElseIf FixedAssetPhysicalAsset.ChangeTracker.State = ObjectState.Modified Then
                    auxFixedAssetPhysicalAsset = FixedAssetPhysicalAsset.OriginalValue
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._fixedAssetPhysicalAssetRepository.SaveEntity(FixedAssetPhysicalAsset)
                unitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of FixedAssetPhysicalAsset)(FixedAssetPhysicalAsset, audit, status, auxFixedAssetPhysicalAsset)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                FixedAssetPhysicalAsset.MarkAsUnchanged()

                transaction.Complete()
                Return New ActionResult(Of FixedAssetPhysicalAsset) With {.StateResult = True, .ObjectEmbbeded = FixedAssetPhysicalAsset, .StatusCode = eStatusResult.SUCCESS}
            Catch ex As DbUpdateException
                transaction.Dispose()
                unitOfWork.RollbackChanges()
                Return New ActionResult(Of FixedAssetPhysicalAsset) With {.StateResult = False, .Message = DirectCast(DirectCast(DirectCast(ex, System.Data.Entity.Infrastructure.DbUpdateException).InnerException, System.Data.Entity.Core.UpdateException).InnerException, System.Data.SqlClient.SqlException).Message, .StatusCode = eStatusResult.WARNING}
            Catch ex As OptimisticConcurrencyException
                transaction.Dispose()
                unitOfWork.RollbackChanges()
                Return New ActionResult(Of FixedAssetPhysicalAsset) With {.StateResult = False, .Message = "Error de concurrencia", .StatusCode = eStatusResult.EXCEPTION}
            Catch ex As Exception
                transaction.Dispose()
                unitOfWork.RollbackChanges()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of FixedAssetPhysicalAsset) With {.StateResult = False, .Message = ex.Message, .StatusCode = eStatusResult.EXCEPTION}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Confirma la finalización de contratos leasing
    ''' </summary>
    ''' <param name="OperatingUnitId"></param>
    ''' <param name="Year"></param>
    ''' <param name="Month"></param>
    ''' <param name="CompanyNit"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ConfirmLeasingContractsFinalization(ListIds As List(Of Integer), OperatingUnitId As Integer, Year As Integer, Month As Integer, CompanyNit As String, audit As AuditMessage) As ActionResult Implements IFixedAssetPhysicalAssetAdminService.ConfirmLeasingContractsFinalization
        If ListIds Is Nothing OrElse ListIds.Count = 0 Then
            Throw New ArgumentNullException("ListIds")
        End If

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim XmlObjectSave = ConvertToXmlSave(ListIds)
                Dim resultStoreSave = _fixedAssetPhysicalAssetRepository.SP_ConfirmLeasingContractsFinalization(XmlObjectSave, OperatingUnitId, Year, Month, CompanyNit, audit.CodeUser)
                If resultStoreSave.CodeMessage <> 0 Then
                    transaction.Dispose()
                    Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = resultStoreSave.Message}
                End If

                transaction.Complete()
                Return New ActionResult With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = resultStoreSave.Message}
            Catch ex As Exception
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = ex.Message}
            End Try
        End Using
    End Function

    Private Function ConvertToXmlSave(ListIds As List(Of Integer)) As String
        Dim builder As StringBuilder = New StringBuilder()
        For Each item In ListIds
            builder.Append("<TableIds>")
            builder.Append("<PhysicalAssetId>" & item & "</PhysicalAssetId>")
            builder.Append("</TableIds>")
        Next
        Return builder.ToString()
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _fixedAssetPhysicalAssetRepository = Nothing
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
