'***********************************************************************
' Assembly         : Application.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 28-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "imports"
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Transactions
Imports System.Data.Entity.Infrastructure
Imports System.Data.SqlClient

#End Region

Public Class BudgetItemAdminService
    Implements IBudgetItemAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de los rubros presupuestales
    ''' </summary>
    Private _BudgetItemRepository As IBudgetItemRepository

#End Region

#Region "Constructor"
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal BudgetItemRepository As IBudgetItemRepository)
        If BudgetItemRepository Is Nothing Then
            Throw New ArgumentNullException("repositoryBudgetItemRepository")
        End If
        Me._BudgetItemRepository = BudgetItemRepository
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene un rubro por código y vigencia
    ''' </summary>
    '''<param name="Code">Código del Rubro</param>
    ''' <param name="BudgetaryValidityId">Id de la vigencia</param>
    ''' <param name="ItemType">TIPO DE RUBRO (NINGUNO = 0,INGRESO = 1,GASTO = 2)</param>
    ''' <returns></returns>
    Function GetBudgetItemByFinancialSourceIdAndCodeAndBudgetaryValidityIdAndItemType(FinancialSourceId As Integer?, Code As String, BudgetaryValidityId As Integer, ItemType As Byte, audit As AuditMessage) As ActionResult(Of Category) Implements IBudgetItemAdminService.GetBudgetItemByFinancialSourceIdAndCodeAndBudgetaryValidityIdAndItemType
        If String.IsNullOrEmpty(Code) Then
            Throw New ArgumentNullException("code")
        End If
        If BudgetaryValidityId = 0 Then
            Throw New ArgumentNullException("ValidityId")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim budgetItem As Category = Me._BudgetItemRepository.GetBudgetItemByFinancialSourceIdAndCodeAndBudgetaryValidityIdAndItemType(FinancialSourceId, Code.Trim(), BudgetaryValidityId, ItemType)
            Return New ActionResult(Of Category) With {.StateResult = True, .ObjectEmbbeded = budgetItem}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Category) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un rubro por código y vigencia
    ''' </summary>
    '''<param name="Code">Código del Rubro</param>
    ''' <param name="BudgetaryValidityId">Id de la vigencia</param>
    ''' <param name="ItemType">TIPO DE RUBRO (NINGUNO = 0,INGRESO = 1,GASTO = 2)</param>
    ''' <returns></returns>
    Function GetBudgetItemByCodeAndBudgetaryValidityIdAndItemType(Code As String, BudgetaryValidityId As Integer, ItemType As Byte, audit As AuditMessage) As ActionResult(Of Category) Implements IBudgetItemAdminService.GetBudgetItemByCodeAndBudgetaryValidityIdAndItemType
        If String.IsNullOrEmpty(Code) Then
            Throw New ArgumentNullException("code")
        End If
        If BudgetaryValidityId = 0 Then
            Throw New ArgumentNullException("ValidityId")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim budgetItem As Category = Me._BudgetItemRepository.GetBudgetItemByCodeAndBudgetaryValidityIdAndItemType(Code.Trim(), BudgetaryValidityId, ItemType)
            Return New ActionResult(Of Category) With {.StateResult = True, .ObjectEmbbeded = budgetItem}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Category) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un rubro por código y vigencia
    ''' </summary>
    '''<param name="Code">Código del Rubro</param>
    ''' <param name="BudgetaryValidityId">Id de la vigencia</param>
    ''' <param name="ItemType">TIPO DE RUBRO (NINGUNO = 0,INGRESO = 1,GASTO = 2)</param>
    ''' <returns></returns>
    Function GetBudgetAndFinancialSourceByItemByCodeAndBudgetaryValidityIdAndItemType(Code As String, BudgetaryValidityId As Integer, ItemType As Byte, audit As AuditMessage) As ActionResult(Of Category) Implements IBudgetItemAdminService.GetBudgetAndFinancialSourceByItemByCodeAndBudgetaryValidityIdAndItemType
        If String.IsNullOrEmpty(Code) Then
            Throw New ArgumentNullException("code")
        End If
        If BudgetaryValidityId = 0 Then
            Throw New ArgumentNullException("ValidityId")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim budgetItem As Category = Me._BudgetItemRepository.GetBudgetAndFinancialSourceByItemByCodeAndBudgetaryValidityIdAndItemType(Code.Trim(), BudgetaryValidityId, ItemType)
            Return New ActionResult(Of Category) With {.StateResult = True, .ObjectEmbbeded = budgetItem}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Category) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Listado de rubros por vigencia
    ''' </summary>
    ''' <param name="ValidityId">Id de la vigencia</param>
    ''' <param name="ItemType">TIPO DE RUBRO (NINGUNO = 0,INGRESO = 1,GASTO = 2)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListBudgetItemsByValidity(ValidityId As String, ItemType As Byte, audit As AuditMessage) As List(Of Category) Implements IBudgetItemAdminService.ListBudgetItemsByValidity
        If String.IsNullOrEmpty(ValidityId) Then
            Throw New ArgumentNullException("ValidityId")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim budgetItem As List(Of Category) = Me._BudgetItemRepository.ListBudgetItemsByValidity(ValidityId.Trim(), ItemType)
            If budgetItem IsNot Nothing AndAlso budgetItem.Count > 0 Then
                For Each _item In budgetItem
                    'IndigoAuditSimpleEntity(Of Category).Execute(_item, audit, Infrastructure.CrossCutting.Audit.Actions.Print, audit.Company)
                Next
            End If
            Return budgetItem
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda o Actualiza un rubro
    ''' </summary>
    ''' <param name="Category">la entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Function SaveBudgetItem(Category As Category, audit As AuditMessage) As ActionResult(Of Category) Implements IBudgetItemAdminService.SaveBudgetItem
        If Category Is Nothing Then
            Throw New ArgumentNullException("Category")
        End If
        Dim unitOfWork As IUnitWork = Me._BudgetItemRepository.UnitWork
        Try
            Dim auxCategory As Category = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of Category)
            Dim status As Integer

            If Category.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                Category.CreationUser = audit.CodeUser
                Category.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                auxCategory = Category.OriginalValue
                Category.ModificationUser = audit.CodeUser
                Category.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            Me._BudgetItemRepository.SaveEntity(Category)
            unitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of Category)(Category, audit, status, auxCategory)
            auditProcess.Execute()

            'Se marca la entidad como sin cambios
            Category.MarkAsUnchanged()

            Return New ActionResult(Of Category) With {.StateResult = True, .ObjectEmbbeded = Category}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of Category) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Category) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Elimina un rubro
    ''' </summary>
    ''' <param name="Category">La entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    Function DeleteBudgetItem(Category As Category, audit As AuditMessage) As ActionResult Implements IBudgetItemAdminService.DeleteBudgetItem
        If Category Is Nothing Then
            Throw New ArgumentNullException("Category")
        End If
        Dim unitOfWork As IUnitWork = Me._BudgetItemRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of Category)
            auditProcess = New IndigoAuditSimpleEntity(Of Category)(Category, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._BudgetItemRepository.DeleteEntity(Category)
            unitOfWork.Commit()
            auditProcess.Execute()
            Return New ActionResult With {.StateResult = True}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-111"})}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad.
    ''' </summary>
    ''' <param name="Category">The category.</param>
    ''' <param name="status">if set to <c>true</c> [status].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function ChangeStatusBudgetItem(Category As Category, status As Boolean, audit As AuditMessage) As ActionResult(Of Category) Implements IBudgetItemAdminService.ChangeStatusBudgetItem
        'Dim budgetItem As Category = GetBudgetItemByCodeAndBudgetaryValidityIdAndItemType(code, ValidityId, ItemType, audit)
        'budgetItem.Status = status
        'budgetItem.MarkAsModified()
        'Return SaveBudgetItem(budgetItem, audit)
    End Function

    ''' <summary>
    ''' funcion para obtener el listado de
    ''' </summary>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Public Function GetAllBudgetItemsByState(state As Boolean) As List(Of Category) Implements IBudgetItemAdminService.GetAllBudgetItemsByState
        Return _BudgetItemRepository.GetAllBudgetItemsByState(state)
    End Function

    Public Function CountCategories() As Integer Implements IBudgetItemAdminService.CountCategories
        Try
            Return Me._BudgetItemRepository.CountCategories
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return -1
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el listado de disponibilidades para cargar el datasource del reporte de ejecución presupuestal por rubro de gastos
    ''' </summary>
    ''' <param name="ValidityId"></param>
    ''' <param name="BudgetId"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListReportBudgetExecutionByCategoryExpense(ValidityId As Integer, BudgetId As Integer, session As SessionValues) As DataSet Implements IBudgetItemAdminService.GetListReportBudgetExecutionByCategoryExpense
        If ValidityId = 0 Then
            Throw New ArgumentNullException("ValidityId")
        End If
        If BudgetId = 0 Then
            Throw New ArgumentNullException("BudgetId")
        End If
        Try
            Dim ds As New DataSet
            Dim query1 As String
            query1 = "exec [Budget].[SP_ReportBudgetExcutionByCategoryExpense] " & ValidityId & "," & BudgetId

            Dim dt1 = Me.GetDatatable(query1, session, "ReportBudgetExcutionByCategoryExpense")
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

    ''' <summary>
    ''' Metodo que ejecuta el storeProcedure "Budget.SP_ReportExpenditureBudgetSituation"
    ''' </summary>
    ''' <param name="validityId"></param>
    ''' <param name="month"></param>
    ''' <param name="financialSourceStart"></param>
    ''' <param name="financialSourceEnd"></param>
    ''' <param name="categoryStart"></param>
    ''' <param name="categoryEnd"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListReportExpenditureBudgetSituation(validityId As Integer, month As Integer, financialSourceStart As String, financialSourceEnd As String, categoryStart As String, categoryEnd As String, session As SessionValues) As DataSet Implements IBudgetItemAdminService.GetListReportExpenditureBudgetSituation
        If validityId = 0 Then
            Throw New ArgumentNullException("ValidityId")
        End If
        If month = 0 Then
            Throw New ArgumentNullException("month")
        End If
        Try
            Dim ds As New DataSet
            Dim query1 As String = String.Empty
            If financialSourceStart Is Nothing AndAlso financialSourceEnd Is Nothing AndAlso categoryStart Is Nothing AndAlso categoryEnd Is Nothing Then
                query1 = "exec Budget.SP_ReportExpenditureBudgetSituation " & validityId & "," & month & ", null, null, null, null"
            ElseIf financialSourceStart IsNot Nothing AndAlso financialSourceEnd IsNot Nothing AndAlso categoryStart Is Nothing AndAlso categoryEnd Is Nothing Then
                query1 = "exec Budget.SP_ReportExpenditureBudgetSituation " & validityId & "," & month & ",'" & financialSourceStart & "','" & financialSourceEnd & "', null, null"
            ElseIf financialSourceStart Is Nothing AndAlso financialSourceEnd Is Nothing AndAlso categoryStart IsNot Nothing AndAlso categoryEnd IsNot Nothing Then
                query1 = "exec Budget.SP_ReportExpenditureBudgetSituation " & validityId & "," & month & ", null, null, '" & categoryStart & "','" & categoryEnd & "'"
            ElseIf financialSourceStart IsNot Nothing AndAlso financialSourceEnd IsNot Nothing AndAlso categoryStart IsNot Nothing AndAlso categoryEnd IsNot Nothing Then
                query1 = "exec Budget.SP_ReportExpenditureBudgetSituation " & validityId & "," & month & ",'" & financialSourceStart & "','" & financialSourceEnd & "','" & categoryStart & "','" & categoryEnd & "'"
            End If


            Dim dt1 = Me.GetDatatable(query1, session, "ReportExpenditureBudgetSituation")
            ds.Tables.Add(dt1.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
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
            Me._BudgetItemRepository = Nothing
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
