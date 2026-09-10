'***********************************************************************
' Assembly         : Application.Contract
' Author           : Giovanny Plazas L
' Created          : 24/08/2021
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
Imports System.Text
Imports System.Transactions
Imports Application.Contract
Imports Infrastructure.CrossCutting.Resources

Public Class ContractPackageAdminService
    Implements IContractPackageAdminService

#Region "Variables"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _ContractPackageRepository As IContractPackageRepository
    Private _contractSequenseDRepository As ISequenseContractDRepository
    Private _inventoryProductRepository As IInventoryProductRepository
    Private _cupsEntityRepository As ICupsEntityRepository
    Private _contractDescriptionRepository As IContractDescriptionsRepository

#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal ContractPackageRepository As IContractPackageRepository, ByVal ContractSequenseDRepository As ISequenseContractDRepository, ByVal InventoryProductRepository As IInventoryProductRepository,
                   ByVal CupsEntityRepository As ICupsEntityRepository, ByVal ContractDescriptionRepository As IContractDescriptionsRepository)
        If ContractPackageRepository Is Nothing Then
            Throw New ArgumentNullException("ContractPackageRepository Vacio")
        End If
        If ContractSequenseDRepository Is Nothing Then
            Throw New ArgumentNullException("ContractSequenseDRepository Vacio")
        End If
        If InventoryProductRepository Is Nothing Then
            Throw New ArgumentNullException("InventoryProductRepository Vacio")
        End If
        If CupsEntityRepository Is Nothing Then
            Throw New ArgumentNullException("CupsEntityRepository Vacio")
        End If
        If ContractDescriptionRepository Is Nothing Then
            Throw New ArgumentNullException("ContractDescriptionRepository Vacio")
        End If
        _contractSequenseDRepository = ContractSequenseDRepository
        _ContractPackageRepository = ContractPackageRepository
        _inventoryProductRepository = InventoryProductRepository
        _cupsEntityRepository = CupsEntityRepository
        _contractDescriptionRepository = ContractDescriptionRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeStateContractPackage(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of ContractPackage) Implements IContractPackageAdminService.ChangeStateContractPackage
        Dim ContractPackage As ContractPackage = _ContractPackageRepository.GetContractPackage(code)
        ContractPackage.Status = state
        Return SaveContractPackage(ContractPackage, audit)
    End Function

    ''' <summary>
    ''' Elimina una entidad cups
    ''' </summary>
    ''' <param name="ContractPackage"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteContractPackage(ContractPackage As ContractPackage, audit As AuditMessage) As ActionResult Implements IContractPackageAdminService.DeleteContractPackage
        If ContractPackage Is Nothing Then
            Throw New ArgumentNullException("ContractPackage")
        End If
        Dim unitOfWork As IUnitWork = Me._ContractPackageRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of ContractPackage)
            auditProcess = New IndigoAuditSimpleEntity(Of ContractPackage)(ContractPackage, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            If ContractPackage.ContractPackageService.Count > 0 Then
                ContractPackage.ContractPackageService.ToList().ForEach(Sub(i As ContractPackageService)
                                                                            i.MarkAsDeleted()
                                                                        End Sub)
            End If

            If ContractPackage.ContractPackageProduct.Count > 0 Then
                ContractPackage.ContractPackageProduct.ToList().ForEach(Sub(i As ContractPackageProduct)
                                                                            i.MarkAsDeleted()
                                                                        End Sub)
            End If
            ContractPackage.MarkAsDeleted()

            Me._ContractPackageRepository.SaveEntity(ContractPackage)
            unitOfWork.Commit()
            auditProcess.Execute()
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-001"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una entidad cups por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetContractPackage(code As String, audit As AuditMessage) As ActionResult(Of ContractPackage) Implements IContractPackageAdminService.GetContractPackage
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim ContractPackage As ContractPackage = Me._ContractPackageRepository.GetContractPackage(code.Trim())
            If ContractPackage IsNot Nothing AndAlso ContractPackage.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of ContractPackage)(ContractPackage, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of ContractPackage) With {.StateResult = True, .ObjectEmbbeded = ContractPackage}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ContractPackage) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una entidad cups por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetContractPackageById(id As Integer, audit As AuditMessage) As ActionResult(Of ContractPackage) Implements IContractPackageAdminService.GetContractPackageById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim ContractPackage As ContractPackage = Me._ContractPackageRepository.GetContractPackageById(id)
            If ContractPackage IsNot Nothing AndAlso ContractPackage.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of ContractPackage)(ContractPackage, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of ContractPackage) With {.StateResult = True, .ObjectEmbbeded = ContractPackage}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ContractPackage) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza una entidad cups
    ''' </summary>
    ''' <param name="ContractPackage"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveContractPackage(ContractPackage As ContractPackage, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of ContractPackage) Implements IContractPackageAdminService.SaveContractPackage
        If ContractPackage Is Nothing Then
            Throw New ArgumentNullException("ContractPackage")
        End If
        Dim unitOfWork As IUnitWork = Me._ContractPackageRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._contractSequenseDRepository.UnitWork

        Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try

                Dim seq As ContractSequenceDetail = Nothing
                If ContractPackage.Code Is Nothing OrElse ContractPackage.Code.Trim().Equals(String.Empty) Then
                    seq = Me._contractSequenseDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.ContractSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            ContractPackage.Code = res
                            seq.Next += 1
                            Me._contractSequenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of ContractPackage) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = ResourceManager.GetString("SequenceNotFound")}
                        End If
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of ContractPackage) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = ResourceManager.GetString("SequenceNotFound")}
                    End If
                End If

                Dim auxContractPackage As ContractPackage = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of ContractPackage)
                Dim status As Integer

                If ContractPackage.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    auxContractPackage = ContractPackage.OriginalValue
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                If ContractPackage.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    ContractPackage.CreationUser = audit.CodeUser
                    ContractPackage.CreationDate = DateTime.Now
                    ContractPackage.ModificationUser = audit.CodeUser
                    ContractPackage.ModificationDate = DateTime.Now
                Else
                    ContractPackage.ModificationUser = audit.CodeUser
                    ContractPackage.ModificationDate = DateTime.Now
                End If

                If ContractPackage.ContractPackageService.Count > 0 Then
                    ContractPackage.ContractPackageService.ToList().ForEach(Sub(i As ContractPackageService)
                                                                                If i.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                                                                                    i.CreationUser = audit.CodeUser
                                                                                    i.CreationDate = DateTime.Now
                                                                                Else
                                                                                    i.ModificationUser = audit.CodeUser
                                                                                    i.ModificationDate = DateTime.Now
                                                                                End If
                                                                            End Sub)
                End If

                If ContractPackage.ContractPackageProduct.Count > 0 Then
                    ContractPackage.ContractPackageProduct.ToList().ForEach(Sub(i As ContractPackageProduct)
                                                                                If i.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                                                                                    i.CreationUser = audit.CodeUser
                                                                                    i.CreationDate = DateTime.Now
                                                                                Else
                                                                                    i.ModificationUser = audit.CodeUser
                                                                                    i.ModificationDate = DateTime.Now
                                                                                End If
                                                                            End Sub)
                End If



                Me._ContractPackageRepository.SaveEntity(ContractPackage)

                auditProcess = New IndigoAuditSimpleEntity(Of ContractPackage)(ContractPackage, audit, status, auxContractPackage)
                auditProcess.Execute()
                unitOfWork.Commit()

                scope.Complete()
                Return New ActionResult(Of ContractPackage) With {.StateResult = True, .ObjectEmbbeded = ContractPackage}
            Catch ex As OptimisticConcurrencyException
                scope.Dispose()
                unitOfWork.RollbackChanges()
                Return New ActionResult(Of ContractPackage) With {.StateResult = False, .Message = "-999"}
            Catch ex As DbUpdateException
                scope.Dispose()
                unitOfWork.RollbackChanges()
                Return New ActionResult(Of ContractPackage) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            Catch ex As Exception
                scope.Dispose()
                unitOfWork.RollbackChanges()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of ContractPackage) With {.StateResult = False, .Message = ex.Message}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Funcion para ejecutar el Copy and Paste
    ''' </summary>
    ''' <param name="dataCopyPaste"></param>
    ''' <param name="Name"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SetCopyPasteOrImportFile(dataCopyPaste As List(Of List(Of String)), Name As String, audit As AuditMessage) As ActionResult(Of ContractPackage) Implements IContractPackageAdminService.SetCopyPasteOrImportFile
        If dataCopyPaste Is Nothing Then
            Throw New ArgumentNullException("dataCopyPaste")
        End If
        Try
            Dim ContractPackage = New ContractPackage
            Dim ListCode = New List(Of String)
            Dim repeated = New List(Of String)

            'Se verifica que el nombre No venga vacio
            If Name Is Nothing OrElse Name = String.Empty Then
                Return New ActionResult(Of ContractPackage) With {.StateResult = False, .Message = "El nombre de la rejilla a donde se van a pegar los elementos esta vacio"}
            End If
            'se verifica que hayan elementos para copiar
            If dataCopyPaste.Count = 0 Then
                Return New ActionResult(Of ContractPackage) With {.StateResult = False, .Message = "No hay ningun elemento para copiar"}
            End If

            'Busca el nombre de la rejilla a donde se va a importar los registros
            If Name.Contains("Inventory") Then
                ListCode = (From x In dataCopyPaste Select x(0)).ToList()
                repeated = ListCode.GroupBy(Function(x) x).Where(Function(m) m.Count() > 1)? _
                    .Select(Function(y) $"El Codigo de Producto {y(0)} esta repetido {y.Count} veces").ToList()

                Dim ListInventoryProduct = New List(Of InventoryProduct)
                ListInventoryProduct = Me._inventoryProductRepository.GetInventoryProductByCodeList(ListCode)
                If ListInventoryProduct.Count = 0 Then
                    Return New ActionResult(Of ContractPackage) With {.StateResult = False, .MessageResult = (From x In ListCode Select $"El Codigo de Producto {x} No existe").ToList()}
                End If
                Dim SelectedCode = (From i In ListInventoryProduct Select i.Code).ToList()
                ListCode = (From x In ListCode Where Not SelectedCode.Contains(x) Select $"El Codigo de Producto {x} No existe").ToList()
                dataCopyPaste = (From p In dataCopyPaste Where SelectedCode.Contains(p(0)) Select p).ToList()

                For Each Item In dataCopyPaste
                    Dim ContractPackageProduct = New ContractPackageProduct
                    With ContractPackageProduct
                        .ProductId = (From i In ListInventoryProduct Where i.Code = Item(0) Select i.Id).FirstOrDefault
                        .ProductCodName = (From i In ListInventoryProduct Where i.Code = Item(0) Select String.Join(" - ", i.Code, i.Name)).FirstOrDefault
                        .Description = (From i In ListInventoryProduct Where i.Code = Item(0) Select i.Description).FirstOrDefault
                        .Quantity = Item(1)
                        .ApplyCondition = Item(2)
                        .UnitValue = Utils.correctDecimalFormat(Item(3))
                        .UnitValueTotal = .UnitValue * .Quantity
                    End With
                    ContractPackage.ContractPackageProduct.Add(ContractPackageProduct)
                Next

            ElseIf Name.Contains("Services") Then
                ListCode = (From x In dataCopyPaste Select x(0)).ToList()
                Dim ListCUPSEntity = New List(Of CUPSEntity)
                Dim ListContractDescription = New List(Of ContractDescriptions)
                repeated = ListCode.GroupBy(Function(x) x).Where(Function(m) m.Count() > 1)? _
                    .Select(Function(y) $"El CUPS {y(0)} esta repetido {y.Count} veces").ToList()

                If repeated.Count > 0 Then
                    Return New ActionResult(Of ContractPackage) With {.StateResult = False, .Message = "No se puedieron copiar los elementos", .MessageResult = repeated}
                End If

                ListCode = ListCode.Distinct().ToList()
                ListCUPSEntity = _cupsEntityRepository.GetListCupsEntityBycodes(ListCode)
                ListCode = New List(Of String)
                dataCopyPaste.ForEach(Sub(x As Object)
                                          Dim ContractPackageService = New ContractPackageService
                                          With ContractPackageService
                                              .CUPSEntityId = (From i In ListCUPSEntity Where i.Code = x(0) Select i.Id).FirstOrDefault
                                              If .CUPSEntityId = 0 Then
                                                  Dim ListC = $"El Codigo Cups {x(0)} No existe"
                                                  ListCode.Add(ListC)
                                                  Exit Sub
                                              End If
                                              .CupsCodeName = (From i In ListCUPSEntity Where i.Code = x(0) Select String.Join(" - ", i.Code, i.Description)).FirstOrDefault
                                              .Quantity = x(2)
                                              .ApplyCondition = x(3)
                                              .UnitValue = Utils.correctDecimalFormat(x(4))
                                              .UnitValueTotal = .UnitValue * .Quantity
                                              If Not String.IsNullOrEmpty(x(1)) Then
                                                  Dim CupsEntityDescription = _cupsEntityRepository.GetListCupsEntityContractDescription(x(0), x(1))
                                                  If CupsEntityDescription Is Nothing OrElse CupsEntityDescription.Count = 0 Then
                                                      Dim ListC = $"No hay Relacion del Codigo Cups {x(0)} con la descripción {x(1)}"
                                                      ListCode.Add(ListC)
                                                      Exit Sub
                                                  End If
                                                  .ContractDescriptionId = (From p In CupsEntityDescription Where p.ContractDescriptions.Code = x(1) And p.CUPSEntity.Code = x(0) Select p.ContractDescriptionId).FirstOrDefault
                                                  .ContractDescriptionName = (From p In CupsEntityDescription Where p.ContractDescriptions.Code = x(1) And p.CUPSEntity.Code = x(0) Select String.Join(" - ", p.ContractDescriptions.Code, p.ContractDescriptions.Name)).FirstOrDefault
                                              Else
                                                  .ContractDescriptionId = Nothing
                                                  .ContractDescriptionName = String.Empty
                                              End If
                                          End With
                                          ContractPackage.ContractPackageService.Add(ContractPackageService)
                                      End Sub)
            Else
                Return New ActionResult(Of ContractPackage) With {.StateResult = False, .Message = "Los elementos no se pudieron Copiar"}
            End If
            If repeated.Count > 0 Then
                repeated.ForEach(Sub(x As Object)
                                     ListCode.Add(x)
                                 End Sub)
            End If
            Return New ActionResult(Of ContractPackage) With {.StateResult = True, .ObjectEmbbeded = ContractPackage, .MessageResult = ListCode}
        Catch ex As Exception
            Return New ActionResult(Of ContractPackage) With {.StateResult = True, .Message = ex.Message}
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
            _ContractPackageRepository = Nothing
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
