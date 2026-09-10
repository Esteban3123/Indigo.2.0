'***********************************************************************
' Assembly         : Application.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 11-12-2014
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
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions
Imports Domain.Payroll
Imports System.Data.SqlClient

Public Class CostProductionCenterAdminService
    Implements ICostProductionCenterAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de centros de produccion
    ''' </summary>
    Private _productionCenterRepository As ICostProductionCenterRepository

    ''' <summary>
    ''' repositorio de secuencias numericas
    ''' </summary>
    Private _sequenceDRepository As ICostSequenceDetailRepository


#End Region

#Region "Methods"

    Public Sub New(ByVal productionCenterRepository As ICostProductionCenterRepository, ByVal sequenceDRepository As ICostSequenceDetailRepository)
        If productionCenterRepository Is Nothing Then
            Throw New ArgumentNullException("productionCenterRepository")
        End If
        If sequenceDRepository Is Nothing Then
            Throw New ArgumentNullException("sequenceDRepository")
        End If
        _productionCenterRepository = productionCenterRepository
        _sequenceDRepository = sequenceDRepository
    End Sub

    Public Function ListReportOperatingResult(InitialMonth As Integer, EndMonth As Integer, Year As Integer, CodePCenterIni As String, CodePCenterFin As String, StructureOfCostId As Integer) As List(Of SP_CostReportOperatingResult_Result) Implements ICostProductionCenterAdminService.ListReportOperatingResult
        Try
            Return _productionCenterRepository.ListReportOperatingResult(InitialMonth, EndMonth, Year, CodePCenterIni, CodePCenterFin, StructureOfCostId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of SP_CostReportOperatingResult_Result)()
        End Try
    End Function

    Public Function ListReportOperatingResultsByOrganizationalStructure(InitialMonth As Integer, EndMonth As Integer, Year As Integer, Container As String, CodePCenterIni As String, CodePCenterFin As String, StructureOfCostId As Integer) As List(Of SP_CostReportOperatingResultsByOrganizationalStructure_Result) Implements ICostProductionCenterAdminService.ListReportOperatingResultsByOrganizationalStructure
        Try
            Return _productionCenterRepository.ListReportOperatingResultsByOrganizationalStructure(InitialMonth, EndMonth, Year, Container, CodePCenterIni, CodePCenterFin, StructureOfCostId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of SP_CostReportOperatingResultsByOrganizationalStructure_Result)()
        End Try
    End Function

    Public Function ListReportOperatingResultsByOrganizationalStatisticalGraphics(InitialMonth As Integer, EndMonth As Integer, Year As Integer, Container As String, CodePCenterIni As String, CodePCenterFin As String, StructureOfCostId As Integer) As List(Of SP_CostReportOperatingResultsByOrganizationalStatisticalGraphics_Result) Implements ICostProductionCenterAdminService.ListReportOperatingResultsByOrganizationalStatisticalGraphics
        Try
            Return _productionCenterRepository.ListReportOperatingResultsByOrganizationalStatisticalGraphics(InitialMonth, EndMonth, Year, Container, CodePCenterIni, CodePCenterFin, StructureOfCostId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of SP_CostReportOperatingResultsByOrganizationalStatisticalGraphics_Result)()
        End Try
    End Function

    ''' <summary>
    ''' Lista la informacion de costo por Unidad de Medida
    ''' </summary>
    ''' <param name="initialMonth"></param>
    ''' <param name="initialYear"></param>
    ''' <param name="endMonth"></param>
    ''' <param name="endYear"></param>
    ''' <param name="initialMeasurementUnitCode"></param>
    ''' <param name="endMeasurementUnitCode"></param>
    ''' <param name="ProductionCenterId"></param>
    ''' <returns></returns>
    Public Function ListReportCostCostMeasurementUnit(initialMonth As Integer, initialYear As Integer, endMonth As Integer, endYear As Integer, initialMeasurementUnitCode As String, endMeasurementUnitCode As String, ByVal ProductionCenterId As Integer) As List(Of spCostReportCostMeasurementUnit_Result) Implements ICostProductionCenterAdminService.ListReportCostCostMeasurementUnit
        Try
            Return _productionCenterRepository.ListReportCostCostMeasurementUnit(initialMonth, initialYear, endMonth, endYear, initialMeasurementUnitCode, endMeasurementUnitCode, ProductionCenterId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of spCostReportCostMeasurementUnit_Result)()
        End Try
    End Function

#End Region

    Public Function DeleteCostProductionCenter(productionCenter As CostProductionCenter, audit As AuditMessage) As ActionResult Implements ICostProductionCenterAdminService.DeleteCostProductionCenter
        If productionCenter Is Nothing Then
            Throw New ArgumentNullException("productionCenter")
        End If
        Dim unitOfWork As IUnitWork = Me._productionCenterRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                While productionCenter.CostProductionCenterCostCenter.Count > 0
                    productionCenter.CostProductionCenterCostCenter.Item(productionCenter.CostProductionCenterCostCenter.Count() - 1).MarkAsDeleted()
                End While
                While productionCenter.CostProductionCenterServiceArea.Count > 0
                    productionCenter.CostProductionCenterServiceArea.Item(productionCenter.CostProductionCenterServiceArea.Count() - 1).MarkAsDeleted()
                End While
                While productionCenter.CostProductionCenterHomologation.Count > 0
                    productionCenter.CostProductionCenterHomologation.Item(productionCenter.CostProductionCenterHomologation.Count() - 1).MarkAsDeleted()
                End While

                productionCenter.MarkAsDeleted()
                productionCenter.ModificationUser = audit.CodeUser
                productionCenter.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CostProductionCenter)(productionCenter, audit, status)

                Me._productionCenterRepository.SaveEntity(productionCenter)
                unitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult With {.StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function GetCostProductionCenter(code As String, audit As AuditMessage) As ActionResult(Of CostProductionCenter) Implements ICostProductionCenterAdminService.GetCostProductionCenter
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim productionCenter As CostProductionCenter = Me._productionCenterRepository.GetCostProductionCenter(code.Trim())
            If productionCenter IsNot Nothing AndAlso productionCenter.Id > 0 Then
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Print
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CostProductionCenter)(productionCenter, audit, status)
                auditProcess.Execute()
            End If
            Return New ActionResult(Of CostProductionCenter) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = productionCenter}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostProductionCenter) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function GetCostProductionCenterById(id As Integer) As CostProductionCenter Implements ICostProductionCenterAdminService.GetCostProductionCenterById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Try
            Dim productionCenter As CostProductionCenter = Me._productionCenterRepository.GetCostProductionCenterById(id)
            Return productionCenter
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function ListCostProductionCenter() As List(Of CostProductionCenter) Implements ICostProductionCenterAdminService.ListCostProductionCenter
        Try
            Return _productionCenterRepository.ListCostProductionCenter()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveCostProductionCenter(productionCenter As CostProductionCenter, audit As AuditMessage, Optional idSequence As Long = 0) As ActionResult(Of CostProductionCenter) Implements ICostProductionCenterAdminService.SaveCostProductionCenter
        If productionCenter Is Nothing Then
            Throw New ArgumentNullException("productionCenter")
        End If
        Dim unitOfWork As IUnitWork = Me._productionCenterRepository.UnitWork
        Dim sequenceUnitOfWork As IUnitWork = Me._sequenceDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty
                If String.IsNullOrEmpty(productionCenter.Code) Then
                    Dim seq As CostSecuenceDetail = _sequenceDRepository.GetSequenseDById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.CostSecuence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            productionCenter.Code = res
                            seq.Next += 1
                            Me._sequenceDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of CostProductionCenter) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("SequenceNotFound")}
                        End If
                        MessageResult = If(seq.CostSecuence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), productionCenter.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of CostProductionCenter) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("SequenceNotFound")}
                    End If
                End If

                'Se valida que los centros de costo que vienen en la entidad no existan en otro centro de producción
                Dim validate = _productionCenterRepository.ValidateCostCenterIds(productionCenter.CostProductionCenterCostCenter.ToList, productionCenter.Id)
                If validate.Length > 0 Then
                    scope.Dispose()
                    Return New ActionResult(Of CostProductionCenter) With {.StatusCode = eStatusResult.WARNING, .Message = validate}
                End If

                Dim auxExpenseConcept As CostProductionCenter = Nothing
                Dim status As Integer
                If productionCenter.ChangeTracker.State = ObjectState.Added Then
                    If String.IsNullOrEmpty(MessageResult) Then
                        MessageResult = String.Format(ResourceManager.GetString("SavedWithCode"), productionCenter.Code)
                    End If
                    productionCenter.CreationDate = Date.Now
                    productionCenter.CreationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    productionCenter.ModificationDate = Date.Now
                    productionCenter.ModificationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                    auxExpenseConcept = productionCenter.OriginalValue
                End If

                Me._productionCenterRepository.SaveEntity(productionCenter)
                unitOfWork.Commit()
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CostProductionCenter)(productionCenter, audit, status, auxExpenseConcept)
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult(Of CostProductionCenter) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = productionCenter, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of CostProductionCenter) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            'Valida el mensaje a mostrar
            If ex.HResult = -2146233087 Then
                Return New ActionResult(Of CostProductionCenter) With {.StatusCode = eStatusResult.WARNING, .Message = "Estos datos ya existen actualmente en el centro de produccion, cargue de nuevo el centro de producción."}
            Else
                Return New ActionResult(Of CostProductionCenter) With {.StatusCode = eStatusResult.WARNING, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
            End If
        End Try
    End Function

    Public Function UpdateStateCostProductionCenter(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of CostProductionCenter) Implements ICostProductionCenterAdminService.UpdateStateCostProductionCenter
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If String.IsNullOrEmpty(state) Then
            Throw New ArgumentNullException("state")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If

        Try
            Dim productionCenter As CostProductionCenter = Me._productionCenterRepository.GetCostProductionCenter(code.Trim())
            If productionCenter IsNot Nothing AndAlso productionCenter.Id > 0 Then
                productionCenter.Status = state
            End If
            Return Me.SaveCostProductionCenter(productionCenter, audit)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostProductionCenter) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    Public Function GetProductionCenterByCostCenterId(costCenterId As Integer) As CostProductionCenter Implements ICostProductionCenterAdminService.GetProductionCenterByCostCenterId
        If costCenterId = 0 Then
            Throw New ArgumentNullException("costCenterId")
        End If
        Try
            Dim CostProductionCenter As CostProductionCenter = Me._productionCenterRepository.GetProductionCenterByCostCenterId(costCenterId)
            Return CostProductionCenter
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure SP_CostReportOperatingResultProductionCenter realizado para cargar los datos del reporte de Estructura Organizacional ProductionCenter en costo nativo
    ''' </summary>
    ''' <param name="InitialMonth"></param>
    ''' <param name="EndMonth"></param>
    ''' <param name="Year"></param>
    ''' <param name="CodePCenterIni"></param>
    ''' <param name="CodePCenterFin"></param>
    ''' <param name="OrderBy"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetCostListReportOperatingProductionCenter(InitialMonth As Integer, EndMonth As Integer, Year As Integer, CodePCenterIni As String, CodePCenterFin As String, OrderBy As Integer, Session As SessionValues) As DataSet Implements ICostProductionCenterAdminService.GetCostListReportOperatingProductionCenter
        If InitialMonth = Nothing OrElse InitialMonth = 0 Then
            Throw New ArgumentNullException("InitialMonth")
        End If
        If Year = Nothing OrElse Year = 0 Then
            Throw New ArgumentNullException("Year")
        End If
        If EndMonth = Nothing OrElse EndMonth = 0 Then
            Throw New ArgumentNullException("EndMonth")
        End If
        Try
            Dim ds As New DataSet
            Dim query1 As String = String.Empty
            query1 = "exec [Cost].[SP_CostReportOperatingResultProductionCenter] " & InitialMonth & "," & EndMonth & "," & Year & ",'" & CodePCenterIni & "','" & CodePCenterFin & "'," & OrderBy
            Dim dt1 = Me.GetDatatable(query1, Session, "OperatingProductionCenter")
            ds.Tables.Add(dt1.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Metodo usado para ejecutar la consulta obtener el datatable con los datos retornados
    ''' </summary>
    ''' <param name="Comando"></param>
    ''' <param name="session"></param>
    ''' <param name="nameDt"></param>
    ''' <returns></returns>
    Public Function GetDatatable(ByVal Comando As String, session As SessionValues, nameDt As String) As System.Data.DataTable
        Dim connectionString = String.Empty
        connectionString = Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, session.TransactionalContainer, False)
        Using conexion As New SqlConnection
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

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _productionCenterRepository = Nothing
            _sequenceDRepository = Nothing
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