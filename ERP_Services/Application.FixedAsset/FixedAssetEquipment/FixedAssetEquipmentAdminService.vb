#Region "Imports"
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions
Imports System.Data.Entity.Infrastructure

#End Region
Public Class FixedAssetItemAdminService
    Implements IFixedAssetItemAdminService

    Private Const FORM_NAME As String = "FrmFixedAssetEquipment"
    'Repositorio de el tipo de equipo
    Private _EquipmentRepository As IFixedAssetItemAllRepository

    'Repositorio de activos fijos
    Private _physicalAssetRepository As IFixedAssetPhysicalAssetRepository

    'repositorio de la secuencia
    Private _sequenceRepository As IFixedAssetSequenceDetailRepository

    'Repositorio de FixedAssetEntryItem
    Private _entryItemRepository As IFixedAssetEntryItemRepository

    Private _currencyRepository As ICurrencyRepository

    Private _entryRepository As IFixedAssetEntryRepository

    ''' <summary>
    ''' inicia el repositorio de tipo de equipo
    ''' </summary>
    ''' <param name="EquipmentRepository">Repositorio de tipo de equipo</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal EquipmentRepository As IFixedAssetItemAllRepository, sequenceRepository As IFixedAssetSequenceDetailRepository,
                   entryItemRepository As IFixedAssetEntryItemRepository, currencyRepository As ICurrencyRepository, entryRepository As IFixedAssetEntryRepository,
                   physicalAssetRepository As IFixedAssetPhysicalAssetRepository)
        If (EquipmentRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio del tipo de equipo")
        End If
        _sequenceRepository = sequenceRepository
        _EquipmentRepository = EquipmentRepository
        _currencyRepository = currencyRepository
        _entryItemRepository = entryItemRepository
        _entryRepository = entryRepository
        _physicalAssetRepository = physicalAssetRepository
    End Sub

    Public Function DeleteEquipment(Equipment As FixedAssetItem, audit As AuditMessage) As ActionResult Implements IFixedAssetItemAdminService.DeleteEquipment
        'If Equipment Is Nothing Then
        '    Throw New ArgumentNullException("Equipment")
        'End If
        'Dim unitOfWork As IUnitWork = Me._EquipmentRepository.UnitWork
        'Try
        '    While Equipment.FixedAssetItemDetail.Count > 0
        '        Equipment.FixedAssetItemDetail.Item(Equipment.FixedAssetItemDetail.Count() - 1).MarkAsDeleted()
        '    End While
        '    Equipment.MarkAsDeleted()
        '    Me._EquipmentRepository.SaveEntity(Equipment)
        '    unitOfWork.Commit()
        '    Dim auditProcess As IndigoAuditSimpleEntity(Of FixedAssetItem)
        '    auditProcess = New IndigoAuditSimpleEntity(Of FixedAssetItem)(Equipment, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
        '    auditProcess.Execute()
        '    Return New ActionResult With {.StateResult = True}
        'Catch ex As OptimisticConcurrencyException
        '    unitOfWork.RollbackChanges()
        '    Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        'Catch ex As UpdateException
        '    unitOfWork.RollbackChanges()
        '    Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        'Catch ex As Exception
        '    unitOfWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        'End Try


        If Equipment Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._EquipmentRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Equipment.ModificationUser = audit.CodeUser
                Equipment.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of FixedAssetItem)(Equipment, audit, status)

                While Equipment.FixedAssetItemDetail.Count > 0
                    Equipment.FixedAssetItemDetail(Equipment.FixedAssetItemDetail.Count - 1).MarkAsDeleted()
                End While
                Equipment.MarkAsDeleted()
                Me._EquipmentRepository.SaveEntity(Equipment)
                unitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-999"}), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function GetEquipment(codeEquipment As String) As ActionResult(Of FixedAssetItem) Implements IFixedAssetItemAdminService.GetEquipment
        If String.IsNullOrEmpty(codeEquipment) Then
            Throw New ArgumentNullException("codeEquipment")
        End If
        Try
            Dim equipment As FixedAssetItem = Me._EquipmentRepository.GetFixedAssetItem(codeEquipment.Trim())
            Return New ActionResult(Of FixedAssetItem) With {.StateResult = True, .ObjectEmbbeded = equipment}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FixedAssetItem) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el ultimo costo de un artículo por la moneda de los libros contables oficiales
    ''' del ultimo ingreso de activo.
    ''' </summary>
    ''' <param name="itemId">ID del artículo</param>
    ''' <returns>Una lista de una tupla de FixedAssetEntryItem y de Currency</returns>
    Public Function GetItemCostsPerCurrency(itemId As Integer) As ActionResult(Of List(Of Tuple(Of Decimal, Currency))) Implements IFixedAssetItemAdminService.GetItemCostsPerCurrency
        Try
            Dim physicalAsset = Me._physicalAssetRepository.GetLastFixedAssetPhysicalAssetByItem(itemId)

            If physicalAsset Is Nothing Then
                Return New ActionResult(Of List(Of Tuple(Of Decimal, Currency))) With {.StateResult = True, .ObjectEmbbeded = New List(Of Tuple(Of Decimal, Currency)), .MessageResult = {"No existe ingreso de activos para el artículo con ID: " & itemId}.ToList}
            End If

            If physicalAsset.FixedAssetPhysicalAssetDetailBook?.Any() Then
                Dim activeCurrencies = Me._currencyRepository.ListAllCurrency().Where(Function(x) x.State = True).ToList()

                Dim itemCosts As New List(Of Tuple(Of Decimal, Currency))
                For Each book In physicalAsset.FixedAssetPhysicalAssetDetailBook
                    Dim currency = activeCurrencies.FirstOrDefault(Function(c) c.Id = book.LegalBook.OfficialCurrencyId)
                    If currency IsNot Nothing AndAlso Not itemCosts.Any(Function(c) c.Item2?.Id = currency.Id) Then
                        itemCosts.Add(New Tuple(Of Decimal, Currency)(book.HistoricalValue, currency))
                    End If
                Next

                If itemCosts.Any() Then
                    Return New ActionResult(Of List(Of Tuple(Of Decimal, Currency))) With {.StateResult = True, .ObjectEmbbeded = itemCosts}
                Else
                    Return New ActionResult(Of List(Of Tuple(Of Decimal, Currency))) With {.StateResult = True, .ObjectEmbbeded = New List(Of Tuple(Of Decimal, Currency)), .MessageResult = {"No existen ingreso de activos para las monedas activas"}.ToList}
                End If
            End If
            Return New ActionResult(Of List(Of Tuple(Of Decimal, Currency))) With {.StateResult = True, .ObjectEmbbeded = New List(Of Tuple(Of Decimal, Currency)) From {New Tuple(Of Decimal, Currency)(physicalAsset.HistoricalValue, Nothing)}}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of Tuple(Of Decimal, Currency))) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    Public Function ListAllEquipment() As List(Of FixedAssetItem) Implements IFixedAssetItemAdminService.ListAllEquipment
        Try
            Return _EquipmentRepository.ListAllFixedAssetItem
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveEquipment(Equipment As FixedAssetItem, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of FixedAssetItem) Implements IFixedAssetItemAdminService.SaveEquipment
        'If Equipment Is Nothing Then
        '    Throw New ArgumentNullException("Equipment")
        'End If
        'Dim unitOfWork As IUnitWork = Me._EquipmentRepository.UnitWork
        'Dim sequenseUnitOfWork As IUnitWork = Me._sequenceRepository.UnitWork
        'Try
        '    'Se valida que la cantidad de libros oficiales que se agregaron en el form coincidan con la cantidad de libros activos que hay en la BD,
        '    'siempre y cuando se permita depreciar el articulo
        '    If Equipment.AllowDepreciate Then 'Si permite depreciar
        '        Dim count = _EquipmentRepository.CountQuantityLegalBook() 'Cantidad de libros oficiales activos de la BD
        '        If count <> (From l In Equipment.FixedAssetItemDetail Where l.ChangeTracker.State <> ObjectState.Deleted Select l).Count Then
        '            unitOfWork.RollbackChanges()
        '            Return New ActionResult(Of FixedAssetItem) With {.StateResult = False, .MessageResult = {"-333"}.ToList()}
        '        End If
        '    Else 'No permite depreciar
        '        'Se eliminan todos los detalles que tenga
        '        If Equipment.FixedAssetItemDetail IsNot Nothing AndAlso Equipment.FixedAssetItemDetail.Count > 0 AndAlso (From x In Equipment.FixedAssetItemDetail Where x.Id > 0 Select x).Count > 0 Then
        '            While (From x In Equipment.FixedAssetItemDetail Where x.Id > 0).Count > 0
        '                Equipment.FixedAssetItemDetail.Item(0).MarkAsDeleted()
        '            End While
        '        End If
        '    End If


        '    Dim seq As FixedAssetSequenceDetail = Nothing
        '    If Equipment.Code Is Nothing OrElse Equipment.Code.Trim().Equals(String.Empty) Then
        '        seq = Me._sequenceRepository.GetSequenseDById(idSequense)
        '        If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.FixedAssetSequence.Sequential Then
        '            Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
        '            If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
        '                Equipment.Code = res
        '                seq.Next += 1
        '                Me._sequenceRepository.SaveEntity(seq)
        '            Else
        '                Return New ActionResult(Of FixedAssetItem) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
        '            End If
        '        Else
        '            Return New ActionResult(Of FixedAssetItem) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
        '        End If
        '    End If

        '    Dim auxEquipment As FixedAssetItem = Nothing
        '    Dim auditProcess As IndigoAuditSimpleEntity(Of FixedAssetItem)
        '    Dim status As Integer

        '    If Equipment.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
        '        Equipment.CreationUser = audit.CodeUser
        '        Equipment.CreationDate = DateTime.Now
        '        status = Infrastructure.CrossCutting.Audit.Actions.Insert
        '    Else
        '        auxEquipment = Equipment.OriginalValue
        '        Equipment.ModificationUser = audit.CodeUser
        '        Equipment.ModificationDate = DateTime.Now
        '        status = Infrastructure.CrossCutting.Audit.Actions.Update
        '    End If

        '    Me._EquipmentRepository.SaveEntity(Equipment)
        '    unitOfWork.Commit()
        '    sequenseUnitOfWork.Commit()
        '    auditProcess = New IndigoAuditSimpleEntity(Of FixedAssetItem)(Equipment, audit, status, auxEquipment)
        '    auditProcess.Execute()

        '    'Se marca la entidad como sin cambios
        '    Equipment.MarkAsUnchanged()

        '    Return New ActionResult(Of FixedAssetItem) With {.StateResult = True, .ObjectEmbbeded = Equipment}
        'Catch ex As OptimisticConcurrencyException
        '    unitOfWork.RollbackChanges()
        '    Return New ActionResult(Of FixedAssetItem) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        'Catch ex As Exception
        '    unitOfWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    Return New ActionResult(Of FixedAssetItem) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        'End Try




        If Equipment Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._EquipmentRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenceRepository.UnitWork
        Try

            'Se valida que la cantidad de libros oficiales que se agregaron en el form coincidan con la cantidad de libros activos que hay en la BD,
            'siempre y cuando se permita depreciar el articulo
            If Equipment.AllowDepreciate Then 'Si permite depreciar
                Dim count = _EquipmentRepository.CountQuantityLegalBook() 'Cantidad de libros oficiales activos de la BD
                If count <> (From l In Equipment.FixedAssetItemDetail Where l.ChangeTracker.State <> ObjectState.Deleted Select l).Count Then
                    unitOfWork.RollbackChanges()
                    Return New ActionResult(Of FixedAssetItem) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-333"}.ToList(), .Message = "Debe ingresar todos los libros contables al listado."}
                End If
            Else 'No permite depreciar
                'Se eliminan todos los detalles que tenga
                If Equipment.FixedAssetItemDetail IsNot Nothing AndAlso Equipment.FixedAssetItemDetail.Count > 0 AndAlso (From x In Equipment.FixedAssetItemDetail Where x.Id > 0 Select x).Count > 0 Then
                    While (From x In Equipment.FixedAssetItemDetail Where x.Id > 0).Count > 0
                        Equipment.FixedAssetItemDetail.Item(0).MarkAsDeleted()
                    End While
                End If
            End If
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(Equipment.Code) Then
                    Dim seq As FixedAssetSequenceDetail = Me._sequenceRepository.GetSequenseDetailUpdatedById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.FixedAssetSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            Equipment.Code = res
                            seq.Next += 1
                            Me._sequenceRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of FixedAssetItem) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.FixedAssetSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), Equipment.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of FixedAssetItem) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As FixedAssetItem = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of FixedAssetItem)
                Dim status As Integer

                If Equipment.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Equipment.CreationUser = audit.CodeUser
                    Equipment.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = Equipment.OriginalValue
                    Equipment.ModificationUser = audit.CodeUser
                    Equipment.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._EquipmentRepository.SaveEntity(Equipment)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of FixedAssetItem)(Equipment, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                Equipment.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of FixedAssetItem) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = Equipment, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of FixedAssetItem) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FixedAssetItem) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function ChangeState(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of FixedAssetItem) Implements IFixedAssetItemAdminService.ChangeState
        'Dim Equipment As FixedAssetItem = _EquipmentRepository.GetFixedAssetItem(code)
        'Equipment.Status = state
        'Equipment.MarkAsModified()
        'Return SaveEquipment(Equipment, audit)


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
            Dim Equipment As FixedAssetItem = Me._EquipmentRepository.GetFixedAssetItem(code.Trim())
            If Equipment IsNot Nothing AndAlso Equipment.Id > 0 Then
                Equipment.Status = state
            End If
            Dim result = Me.SaveEquipment(Equipment, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FixedAssetItem) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _sequenceRepository = Nothing
            _EquipmentRepository = Nothing
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
