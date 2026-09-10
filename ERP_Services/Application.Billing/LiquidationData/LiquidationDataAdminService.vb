'***********************************************************************
' Assembly         : Application.Billing
' Author           : Cristian Camilo Bahamon
' Created          : 2023-03-01
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Application.Base
Imports Domain.Base
Imports System.Transactions
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports System.Data.Entity.Core
'Imports System.Resources
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Payroll.Entities
Imports Newtonsoft.Json
Imports Application.Security
Imports System.Reflection


#End Region

Public Class LiquidationDataAdminService
    Implements ILiquidationDataAdminService

    Private _auditDataRepository As IAuditDataRepository
    'Private _auditData As AuditData
    Private _liquidationDataRepository As ILiquidationDataRepository
    Private _liquidationDataDetailRepository As ILiquidationDataDetailRepository
    Private _cupsGroupRepository As ICupsGroupRepository
    Private _cupsSubGroupRepository As ICupsSubGroupRepository
    Private _cupsEntityRepository As ICupsEntityRepository
    Private _secuenseDRepository As IBillingSequenceDetailRepository
    Private _inventoryProductRepository As IInventoryProductRepository
    Public Const FORM_NAME As String = "FrmLiquidationData"



    ''' <summary>
    ''' Initializa una nueva instancia de la clase <see cref="DetailedConceptAdminService" />.
    ''' </summary>
    Public Sub New(ByVal liquidationDataRepository As ILiquidationDataRepository,
                   secuenseDRepository As IBillingSequenceDetailRepository,
                   auditDataRepository As IAuditDataRepository,
                   liquidationDataDetail As ILiquidationDataDetailRepository,
                   cupsGroupRepository As ICupsGroupRepository,
                   cupsSubGroupRepository As ICupsSubGroupRepository,
                   cupsEntityRepository As ICupsEntityRepository,
                   InventoryProductRepository As IInventoryProductRepository)

        If liquidationDataRepository Is Nothing Then
            Throw New ArgumentNullException("liquidationDataRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository vacío")
        End If
        If auditDataRepository Is Nothing Then
            Throw New ArgumentNullException("auditDataRepository vacío")
        End If
        If liquidationDataDetail Is Nothing Then
            Throw New ArgumentNullException("liquidationDataDetail vacío")
        End If
        If cupsGroupRepository Is Nothing Then
            Throw New ArgumentNullException("cupsGroupRepository vacío")
        End If
        If cupsSubGroupRepository Is Nothing Then
            Throw New ArgumentNullException("cupsSubGroupRepository vacío")
        End If
        If cupsEntityRepository Is Nothing Then
            Throw New ArgumentNullException("cupsEntityRepository vacío")
        End If

        _liquidationDataRepository = liquidationDataRepository
        _secuenseDRepository = secuenseDRepository
        _auditDataRepository = auditDataRepository
        _liquidationDataDetailRepository = liquidationDataDetail
        _cupsGroupRepository = cupsGroupRepository
        _cupsSubGroupRepository = cupsSubGroupRepository
        _cupsEntityRepository = cupsEntityRepository
        _inventoryProductRepository = InventoryProductRepository
    End Sub

    Public Function SaveLiquidationData(_liquidationData As LiquidationData, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of LiquidationData) Implements ILiquidationDataAdminService.SaveLiquidationData
        If _liquidationData Is Nothing Then
            Throw New ArgumentNullException("Justificacion Control Vacio")
        End If
        Dim UnitOfWork As IUnitWork = _liquidationDataRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                Dim MessageResult As String = String.Empty
                Dim auxCommon As LiquidationData = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of LiquidationData)
                Dim status As Integer

                If _liquidationData.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    _liquidationData.CreationUser = audit.CodeUser
                    _liquidationData.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                    MessageResult = ResourceManager.GetString("SaveMessage")
                Else

                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxCommon = _liquidationData.OriginalValue
                    _liquidationData.ModificationUser = audit.CodeUser
                    _liquidationData.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Dim _auditData = New AuditData()


                'Entidad para la auditoria de los registros, siempre se crea un registro nuevo en auditoria ya sea que se actualize o se guarde por primera vez
                _auditData.EntityName = "LiquidationData"
                _auditData.EntityId = _liquidationData.Id
                _auditData.JSONData = JsonConvert.SerializeObject(_liquidationData)
                _auditData.CreationUser = audit.CodeUser
                _auditData.CreationDate = DateTime.Now
                _auditData.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added
                _auditData.CreationUser = audit.CodeUser
                _auditData.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert

                Me._liquidationDataRepository.SaveEntity(_liquidationData)
                Me._auditDataRepository.SaveEntity(_auditData)
                UnitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of LiquidationData)(_liquidationData, audit, status, auxCommon)
                auditProcess.Execute()

                scope.Complete()
                Return New ActionResult(Of LiquidationData) With {.StatusCode = eStatusResult.SUCCESS, .StateResult = True, .ObjectEmbbeded = _liquidationData, .Message = MessageResult}
            End Using

        Catch ex As OptimisticConcurrencyException
            UnitOfWork.RollbackChanges()
            Return New ActionResult(Of LiquidationData) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of LiquidationData) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function GetLiquidationDataByAdmissionNumber(_admissionNumber As String, audit As AuditMessage) As ActionResult(Of LiquidationData) Implements ILiquidationDataAdminService.GetLiquidationDataByAdmissionNumber
        If String.IsNullOrEmpty(_admissionNumber) = True Then
            Throw New ArgumentNullException("Numero de Admision vacio")
        End If
        Try
            Dim liquidacionData = _liquidationDataRepository.GetLiquidationDataByAdmissionNumber(_admissionNumber)

            Dim i = 0
            Dim listNameItems As New List(Of String)
            Dim NameOfCondition As String

            'desglosamos los detalles para mostrar cada item por separado
            For Each items In liquidacionData?.LiquidationDataDetail

                Dim listItems As List(Of Integer) = items.ConditionsItemsIds.Split(","c).Select(Function(s) Integer.Parse(s)).ToList()
                For Each item In listItems
                    Select Case items.ConditionType
                        Case 1
                            'ContractRepository.CupsGroupXpo
                            Dim res = _cupsGroupRepository.GetCupsGroupById(item)
                            listNameItems.Add(res.Name)
                            NameOfCondition = "Grupo cabys"

                        Case 2
                            'ContractRepository.CupsSubGroupXpo
                            Dim res = _cupsSubGroupRepository.GetCupsSubgroupById(item)
                            listNameItems.Add(res.Name)
                            NameOfCondition = "Subgrupo cabys"
                        Case 3
                            'ContractRepository.CupsEntityXpo
                            Dim res = _cupsEntityRepository.GetCupsEntityById(item)
                            listNameItems.Add(res.Description)
                            NameOfCondition = "Catálogo de servicios"
                        Case 4
                            'Inventory.InventoryProduct
                            Dim res = _inventoryProductRepository.FirstOrDefault(Function(x) x.Id = item)
                            listNameItems.Add(res.Description)
                            NameOfCondition = "Productos"
                    End Select
                    i += 1
                Next
                items.NameCondition = NameOfCondition
                items.NameConditionsItems = String.Join(",", listNameItems)
                listNameItems.Clear()
                listItems.Clear()
            Next

            Return New ActionResult(Of LiquidationData) With {.StateResult = True, .ObjectEmbbeded = liquidacionData}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
    End Sub


#End Region
End Class
