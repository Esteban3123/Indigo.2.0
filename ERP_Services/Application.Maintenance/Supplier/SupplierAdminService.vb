

#Region "Imports"
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Audit
Imports Application.Common
Imports System.Transactions
Imports System.Data.Entity.Infrastructure
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Queue

#End Region

Public Class SupplierAdminService
    Implements ISupplierAdminService

    Private Const FORM_NAME As String = "Proveedores"
    'Repositorio de tipo de fabricante
    Private _SupplierRepository As ISupplierRepository
    'Repositorio de tipo de fabricante
    Private _SupplierCommitRepository As ISupplierRepository
    'Repositorio de tipo de fabricante

    Private _thirdPartyRepository As IThirdPartyRepository
    ''' <summary>
    ''' Aplicacion de lineas de distribucion
    ''' </summary>
    ''' <remarks></remarks>
    Private _supplierDistributionLine As ISuppliersDistributionLinesAdminService
    ''' <summary>
    ''' Aplicacion de lineas de distribucion
    ''' </summary>
    ''' <remarks></remarks>
    Private _supplierDistributionLineRepository As ISuppliersDistributionLinesRepository

    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As ISequensePaymentsDRepository
    ''' <summary>
    ''' Repositorio de tipo de proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Private _supplierDetailTypeRepository As ISuppliersDetailTypeRepository


    '''' <summary>
    '''' Repositorio de cuentas bancarias
    '''' </summary>
    '''' <remarks></remarks>
    Private _supplierBankAccountRepository As ISupplierBankAccountRepository

    ''' <summary>
    ''' Fabrica de Indiigo Queue
    ''' </summary>
    Private _factoryQueue As IFactoryQueue

    ''' <summary>
    ''' inicia el repositorio de fabricante
    ''' </summary>
    ''' <param name="SupplierRepository">Repositorio de fabricante</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal SupplierRepository As ISupplierRepository, ByVal secuenseDRepository As ISequensePaymentsDRepository,
                   supplierCommitRepository As ISupplierRepository, supplierDistributionLine As ISuppliersDistributionLinesAdminService,
                   thirdPartyRepository As IThirdPartyRepository, supplierDistributionLineRepository As ISuppliersDistributionLinesRepository,
                   supplierDetailTypeRepository As ISuppliersDetailTypeRepository, SupplierBankAccountRepository As ISupplierBankAccountRepository,
                   FactoryQueue As IFactoryQueue)
        If (SupplierRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de fabricante")
        End If
        _SupplierRepository = SupplierRepository
        _secuenseDRepository = secuenseDRepository
        _SupplierCommitRepository = supplierCommitRepository
        _supplierDistributionLine = supplierDistributionLine
        _thirdPartyRepository = thirdPartyRepository
        _supplierDistributionLineRepository = supplierDistributionLineRepository
        _supplierDetailTypeRepository = supplierDetailTypeRepository
        _supplierBankAccountRepository = SupplierBankAccountRepository
        _factoryQueue = FactoryQueue
    End Sub

    Public Function DeleteSupplier(Supplier As Domain.Entities.Supplier, listSupplierDistributionLines As List(Of Domain.Entities.SuppliersDistributionLines), listSupplierDetailType As List(Of Domain.Entities.SupplierDetailType), audit As AuditMessage) As ActionResult Implements ISupplierAdminService.DeleteSupplier
        If Supplier Is Nothing Then
            Throw New ArgumentNullException("Supplier")
        End If
        Dim unitOfWork As IUnitWork = Me._SupplierRepository.UnitWork
        Dim unitOfWorkSDL As IUnitWork = Me._supplierDistributionLineRepository.UnitWork
        Dim unitOfWorkST As IUnitWork = Me._supplierDetailTypeRepository.UnitWork

        Dim unitOfWorkSBA As IUnitWork = Me._supplierBankAccountRepository.UnitWork

        Using Transaction As New TransactionScope
            Try

                For Each item As SupplierDetailType In listSupplierDetailType
                    _supplierDetailTypeRepository.DeleteEntity(item)
                    unitOfWorkST.Commit()
                Next

                For Each item As SuppliersDistributionLines In listSupplierDistributionLines
                    _supplierDistributionLineRepository.DeleteEntity(item)
                    unitOfWorkSDL.Commit()
                Next

                For Each item In Supplier.SupplierBankAccount
                    _supplierBankAccountRepository.DeleteEntity(item)
                    unitOfWorkSBA.Commit()
                Next

                Dim auditProcess As IndigoAuditSimpleEntity(Of Domain.Entities.Supplier)
                auditProcess = New IndigoAuditSimpleEntity(Of Domain.Entities.Supplier)(Supplier, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
                Me._SupplierRepository.DeleteEntity(Supplier)
                unitOfWork.Commit()
                auditProcess.Execute()
                Transaction.Complete()

                Supplier.MarkAsDeleted()
                TriggerEvent(Supplier, audit)

                Return New ActionResult With {.StateResult = True}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                unitOfWorkST.RollbackChanges()
                unitOfWorkSDL.RollbackChanges()
                Transaction.Dispose()
                Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
            Catch ex As System.Data.Entity.Infrastructure.DbUpdateException
                unitOfWork.RollbackChanges()
                unitOfWorkST.RollbackChanges()
                unitOfWorkSDL.RollbackChanges()
                Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                unitOfWorkST.RollbackChanges()
                unitOfWorkSDL.RollbackChanges()
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult With {.StateResult = False}
            End Try
        End Using
    End Function

    Public Function GetSupplierByIdThirdParty(Id As Integer) As Domain.Entities.Supplier Implements ISupplierAdminService.GetSupplierByIdThirdParty
        If Id = 0 Then
            Throw New ArgumentNullException("IdThird")
        End If
        Try

            Return _SupplierRepository.GetSupplierByIdThirdParty(Id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return New Domain.Entities.Supplier()
        End Try
    End Function

    Public Function GetSupplierByIdThirdPartyWithThirdAdded(Id As Integer) As Domain.Entities.Supplier Implements ISupplierAdminService.GetSupplierByIdThirdPartyWithThirdAdded
        If Id = 0 Then
            Throw New ArgumentNullException("IdThird")
        End If
        Try

            Return _SupplierRepository.GetSupplierByIdThirdPartyWithThirdAdded(Id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return New Domain.Entities.Supplier()
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un proveedor por id del tercero e id de la cuenta contable
    ''' </summary>
    ''' <param name="IdThird">The identifier third.</param>
    Public Function GetSupplierByIdThirdPartyAndIdAccountAccounting(IdThird As Integer, IdAccountAccounting As Integer) As Domain.Entities.Supplier Implements ISupplierAdminService.GetSupplierByIdThirdPartyAndIdAccountAccounting
        If IdThird = 0 Then
            Throw New ArgumentNullException("IdThird")
        End If
        If IdAccountAccounting = 0 Then
            Throw New ArgumentNullException("IdAccountAccounting")
        End If
        Try

            Return _SupplierRepository.GetSupplierByIdThirdPartyAndIdAccountAccounting(IdThird, IdAccountAccounting)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return New Domain.Entities.Supplier()
        End Try
    End Function

    Public Function GetSupplier(codeSupplier As String) As Domain.Entities.Supplier Implements ISupplierAdminService.GetSupplier
        If String.IsNullOrEmpty(codeSupplier) Then
            Throw New ArgumentNullException("Codigo de tipo de inventario vacio")
        End If
        Try

            Return _SupplierRepository.GetSupplier(codeSupplier)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return New Domain.Entities.Supplier()
        End Try
    End Function

    Public Function ListAllSupplier() As List(Of Domain.Entities.Supplier) Implements ISupplierAdminService.ListAllSupplier
        Try
            Return _SupplierRepository.ListAllSupplier
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function GetSupplierByIdThirdPartyEntity(Id As Integer) As Domain.Entities.ThirdParty
        If Id = 0 Then
            Throw New ArgumentNullException("IdThird")
        End If
        Try

            Return _SupplierRepository.GetThirdPartyById(Id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return New Domain.Entities.ThirdParty()
        End Try
    End Function

    Public Function SaveSupplier(Supplier As Domain.Entities.Supplier, listSupplierDistributionLines As List(Of Domain.Entities.SuppliersDistributionLines), listSupplierDetailType As List(Of Domain.Entities.SupplierDetailType), mode As Boolean, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of Domain.Entities.Supplier) Implements ISupplierAdminService.SaveSupplier
        If Supplier Is Nothing Then
            Throw New ArgumentNullException("Supplier")
        End If
        Dim unitOfWork As IUnitWork = Me._SupplierCommitRepository.UnitWork
        Dim unitOfWorkSupplierDetailType As IUnitWork = Me._supplierDetailTypeRepository.UnitWork
        Dim unitOfWorkSupplierDistributionLine As IUnitWork = Me._supplierDistributionLineRepository.UnitWork

        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If Supplier.ThirdParty Is Nothing Then
                    Dim supplierEntity As Domain.Entities.ThirdParty = GetSupplierByIdThirdPartyEntity(Supplier.IdThirdParty)
                    Supplier.ThirdParty = supplierEntity
                End If

                If mode Then
                    Dim supplierValidate As Domain.Entities.Supplier = GetSupplierByIdThirdParty(Supplier.IdThirdParty)
                    If supplierValidate IsNot Nothing AndAlso supplierValidate.Id > 0 Then
                        unitOfWork.RollbackChanges()
                        scope.Dispose()
                        Return New ActionResult(Of Domain.Entities.Supplier) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"El tercero seleccionado ya existe como proveedor."}.ToList, .Message = "El tercero seleccionado ya existe como proveedor."}
                    End If
                End If

                If Supplier.Code Is Nothing OrElse Supplier.Code.Trim().Equals(String.Empty) Then
                    Dim seq As PaymentsSecuenceDetail = Me._secuenseDRepository.GetSequenseDetailUpdatedById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PaymentsSecuence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            Supplier.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of Domain.Entities.Supplier) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.PaymentsSecuence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), Supplier.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of Domain.Entities.Supplier) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If


                Dim auxSupplier As Domain.Entities.Supplier = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of Domain.Entities.Supplier)
                Dim status As Integer

                If Supplier.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Supplier.CreationUser = audit.CodeUser
                    Supplier.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxSupplier = _SupplierRepository.GetSupplier(Supplier.Code, False)
                    Supplier.ModificationUser = audit.CodeUser
                    Supplier.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Dim listSupplierBankAccount = (From BankAccount In Supplier.SupplierBankAccount Select BankAccount).ToList()
                Dim listSupplierThirdPartyEmail = New List(Of Email)
                Dim listSupplierThirdPartyPhone = New List(Of Phone)
                Dim listSupplierThirdPartyAddressDelete As New List(Of Address)

                If Supplier.ThirdParty IsNot Nothing Then
                    If Supplier.ThirdParty.Person.Email.Count > 0 Then
                        listSupplierThirdPartyEmail = (From Email In Supplier.ThirdParty.Person.Email Select Email).ToList()
                    End If
                    If Supplier.ThirdParty.Person.Phone.Count > 0 Then
                        listSupplierThirdPartyPhone = (From Phone In Supplier.ThirdParty.Person.Phone Select Phone).ToList()
                    End If
                    If Supplier.ThirdParty.Person.Address.Count > 0 Then
                        For Each item In Supplier.ThirdParty.Person.Address
                            If item.ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted Then
                                Dim entityAddress As New Address
                                With entityAddress
                                    entityAddress.CityId = item.CityId
                                    entityAddress.Addresss = item.Addresss
                                    entityAddress.ChangeTracker = item.ChangeTracker
                                End With
                                listSupplierThirdPartyAddressDelete.Add(entityAddress)
                            End If
                        Next
                    End If
                End If

                Dim listSupplierDistributionLinesEntity = New List(Of SuppliersDistributionLines)
                If listSupplierDistributionLines IsNot Nothing Then
                    listSupplierDistributionLinesEntity = (From Distribution In listSupplierDistributionLines Select Distribution).ToList()
                End If
                Dim listSupplierDetailTypeEntity = New List(Of SupplierDetailType)
                If listSupplierDetailType IsNot Nothing Then
                    listSupplierDetailTypeEntity = (From DetailType In listSupplierDetailType Select DetailType).ToList()
                End If


                If Supplier.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("SupplierBankAccount") Then
                    For Each item As SupplierBankAccount In Supplier.ChangeTracker.ObjectsRemovedFromCollectionProperties.Item("SupplierBankAccount")
                        listSupplierBankAccount.Add(item)
                    Next
                End If

                Me._SupplierCommitRepository.SaveEntity(Supplier)
                unitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of Domain.Entities.Supplier)(Supplier, audit, status, auxSupplier)
                auditProcess.Execute()

                If listSupplierDistributionLines IsNot Nothing Then
                    For Each item As SuppliersDistributionLines In listSupplierDistributionLines
                        If item.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added OrElse item.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                            item.IdSupplier = Supplier.Id
                            _supplierDistributionLineRepository.SaveEntity(item)
                            unitOfWorkSupplierDistributionLine.Commit()
                        ElseIf item.ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted Then
                            _supplierDistributionLineRepository.DeleteEntity(item)
                            unitOfWorkSupplierDistributionLine.Commit()
                        End If
                    Next
                End If

                If listSupplierDetailType IsNot Nothing Then
                    For Each item As SupplierDetailType In listSupplierDetailType
                        If item.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                            item.SupplierId = Supplier.Id
                            _supplierDetailTypeRepository.SaveEntity(item)
                            unitOfWorkSupplierDetailType.Commit()
                        ElseIf item.ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted Then
                            _supplierDetailTypeRepository.DeleteEntity(item)
                            unitOfWorkSupplierDetailType.Commit()
                        End If
                    Next
                End If

                If listSupplierBankAccount.Count > 0 Then
                    For Each item In listSupplierBankAccount
                        Supplier.SupplierBankAccount.Add(item)
                    Next
                End If

                If listSupplierThirdPartyEmail.Count > 0 Then
                    For Each item In listSupplierThirdPartyEmail
                        Supplier.ThirdParty.Person.Email.Add(item)
                    Next
                End If

                If listSupplierThirdPartyAddressDelete.Count > 0 Then
                    For Each item In listSupplierThirdPartyAddressDelete
                        Supplier.ThirdParty.Person.Address.Add(item)
                    Next
                End If

                If listSupplierThirdPartyPhone.Count > 0 Then
                    For Each item In listSupplierThirdPartyPhone
                        Supplier.ThirdParty.Person.Phone.Add(item)
                    Next
                End If

                If listSupplierDistributionLinesEntity.Count > 0 Then
                    For Each item In listSupplierDistributionLinesEntity
                        Supplier.SuppliersDistributionLines.Add(item)
                    Next
                End If

                If listSupplierDetailTypeEntity.Count > 0 Then
                    For Each item In listSupplierDetailTypeEntity
                        Supplier.SupplierDetailType.Add(item)
                    Next
                End If


                'se asegura que se hace commit y se se dispara el evento
                TriggerEvent(Supplier, audit)
                auditProcess = New IndigoAuditSimpleEntity(Of Domain.Entities.Supplier)(Supplier, audit, status, auxSupplier)
                auditProcess.Execute()
                Supplier.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of Domain.Entities.Supplier) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = Supplier, .Message = MessageResult}
            End Using
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            unitOfWorkSupplierDistributionLine.RollbackChanges()
            unitOfWorkSupplierDetailType.RollbackChanges()
            Return New ActionResult(Of Domain.Entities.Supplier) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {DirectCast(DirectCast(ex.InnerException, System.Data.Entity.Core.UpdateException).InnerException, System.Data.SqlClient.SqlException).Message}.ToList(), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As System.Data.Entity.Validation.DbEntityValidationException
            unitOfWork.RollbackChanges()
            unitOfWorkSupplierDistributionLine.RollbackChanges()
            unitOfWorkSupplierDetailType.RollbackChanges()
            Return New ActionResult(Of Domain.Entities.Supplier) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            unitOfWorkSupplierDistributionLine.RollbackChanges()
            unitOfWorkSupplierDetailType.RollbackChanges()
            Return New ActionResult(Of Domain.Entities.Supplier) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            unitOfWorkSupplierDistributionLine.RollbackChanges()
            unitOfWorkSupplierDetailType.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Domain.Entities.Supplier) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeState(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of Domain.Entities.Supplier) Implements ISupplierAdminService.ChangeState
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
            Dim supplier As Domain.Entities.Supplier = Me._SupplierRepository.GetSupplier(code.Trim())
            If supplier IsNot Nothing AndAlso supplier.Id > 0 Then
                supplier.Status = state
                supplier.MarkAsModified()
            End If
            Dim result = Me.SaveSupplier(supplier, Nothing, Nothing, False, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Domain.Entities.Supplier) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
        'Dim supplier As Domain.Entities.Supplier = _SupplierRepository.GetSupplier(code)
        'supplier.Status = state
        ''supplier.ThirdParty = Nothing
        ''supplier.MarkAsModified()
        'Return SaveSupplier(supplier, Nothing, Nothing, False, audit)
    End Function

    Public Function GetThirdPartyById(id As Integer) As Domain.Entities.ThirdParty Implements ISupplierAdminService.GetThirdPartyById
        If id = 0 Then
            Throw New ArgumentNullException("Id de tipo de inventario vacio")
        End If
        Try

            Return _SupplierRepository.GetThirdPartyById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return New Domain.Entities.ThirdParty()
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un proveedor por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSupplierById(id As Integer) As Domain.Entities.Supplier Implements ISupplierAdminService.GetSupplierById
        If id = 0 Then
            Throw New ArgumentNullException("id de proveedor vacio")
        End If
        Try
            Return _SupplierRepository.GetSupplierById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return New Domain.Entities.Supplier()
        End Try
    End Function

    Public Function GetThirdPartyByIdSupplier(Id As Integer) As Domain.Entities.ThirdParty Implements ISupplierAdminService.GetThirdPartyByIdSupplier
        If Id = 0 Then
            Throw New ArgumentNullException("id de proveedor vacio")
        End If
        Try
            Return _SupplierRepository.GetThirdPartyByIdSupplier(Id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return New Domain.Entities.ThirdParty
        End Try
    End Function

    Public Function GetSupplierByNitThirdParty(Nit As String) As Domain.Entities.Supplier Implements ISupplierAdminService.GetSupplierByNitThirdParty
        If String.IsNullOrEmpty(Nit) Then
            Throw New ArgumentNullException("Nit de proveedor vacio")
        End If
        Try
            Dim supplier As Domain.Entities.Supplier = _SupplierRepository.GetSupplierByNitThirdParty(Nit.Trim())
            Return supplier
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

#Region "Events"

    Public Sub TriggerEvent(Supplier As Supplier, audit As AuditMessage)
        Dim wrapperEvent As New Events.Serializers.Wrapper
        Dim ChangeTracker As String = Supplier.ChangeTracker.State.ToString().ToLower()

        If {Domain.Base.Entities.ObjectState.Unchanged, Domain.Base.Entities.ObjectState.Modified}.Contains(Supplier.ChangeTracker.State) Then
            ChangeTracker = "modified"

        ElseIf Supplier.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
            ChangeTracker = "added"
        End If

        Dim eventData As EventData = wrapperEvent.GenerateWrapperEventData(Supplier, audit.CodeUser, ChangeTracker, DittoSourceType.supplier)
        Dim Queue As IIndigoQueue = _factoryQueue.CreateQueue()
        queue.Publish(eventData)
    End Sub

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _SupplierRepository = Nothing
            _secuenseDRepository = Nothing
            _SupplierCommitRepository = Nothing
            _supplierDistributionLine = Nothing
            _thirdPartyRepository = Nothing
            _supplierDistributionLineRepository = Nothing
            _supplierDetailTypeRepository = Nothing
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
