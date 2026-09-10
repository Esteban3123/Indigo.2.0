Imports System
Imports System.Transactions
Imports Domain.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports TechTalk.SpecFlow

Namespace Application.Inventory.Test

    <Binding()>
    Public Class SaveEntranceVoucherSteps

#Region "Fields"
        Private ReadOnly _InventoryContext As InventoryContext
        Private _entranceVoucher As New Domain.Entities.EntranceVoucher
        Private _actionResult As New ActionResult(Of Domain.Entities.EntranceVoucher)
#End Region

#Region "Builder"
        Public Sub New(InventoryContext As InventoryContext)
            _InventoryContext = InventoryContext
        End Sub
#End Region


        <TechTalk.SpecFlow.Given("Consulto el comprobante de entrada (.*)")>
        Public Sub DadoConsultoElComprobanteDeEntrada(ByVal Id As Int32)
            _entranceVoucher = _InventoryContext._entranceVoucherAdminService.GetEntranceVoucherById(Id)

        End Sub

        <TechTalk.SpecFlow.Given("El comprobante de entrada existe")>
        Public Sub DadoElComprobanteDeEntradaExiste()
            If _entranceVoucher Is Nothing Then
                Assert.Fail("EntranceVoucher Vacio")
            Else
                'Assert.Fail("fallo esta joda")
            End If
        End Sub

        <TechTalk.SpecFlow.When("Yo guardo el comprobante de entrada")>
        Public Sub CuandoYoGuardoElComprobanteDeEntrada()
            Try
                Dim unitOfWork As IUnitWork = _InventoryContext._EntranceVoucherRepository.UnitWork
                'Dim unitOfWorkSequense As IUnitWork = _InventoryContext._EntranceVoucherRepository.UnitWork
                'Dim unitOfWorkControlDocuments As IUnitWork = _InventoryContext._InventoryControlDocumentRepository.UnitWork

                Dim txSettings As New TransactionOptions()
                txSettings.Timeout = TransactionManager.MaximumTimeout
                txSettings.IsolationLevel = IsolationLevel.ReadCommitted
                Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)

                    '_entranceVoucher.StartTracking()

                    _entranceVoucher.ModificationUser = _InventoryContext._audit.CodeUser
                    _entranceVoucher.ModificationDate = DateTime.Now
                    For Each detail As Domain.Entities.EntranceVoucherDetail In _entranceVoucher.EntranceVoucherDetail
                        If detail.InventoryProduct IsNot Nothing Then
                            If detail.InventoryProduct.ProductGroup IsNot Nothing Then
                                Dim groupId = detail.InventoryProduct.ProductGroup.Id
                                detail.InventoryProduct.ProductGroup = Nothing
                                detail.InventoryProduct.ProductGroupId = groupId
                            End If
                            If (detail.InventoryProduct.ProductSubGroup IsNot Nothing) Then
                                Dim subGroupId = detail.InventoryProduct.ProductSubGroup.Id
                                detail.InventoryProduct.ProductSubGroup = Nothing
                                detail.InventoryProduct.ProductSubGroupId = subGroupId
                            End If
                        End If
                    Next

                    ' _entranceVoucher.Description = "Prueba Traza 2"
                    _entranceVoucher.MarkAsModified()
                    _InventoryContext._EntranceVoucherRepository.SaveEntity(_entranceVoucher)
                    unitOfWork.Commit()
                    'unitOfWorkSequense.Commit();
                    'unitOfWorkControlDocuments.Commit();
                    ' auditProcess = New IndigoAuditSimpleEntity < Domain.Entities.EntranceVoucher > (EntranceVoucher, audit, status, auxEntranceVoucher);
                    'auditProcess.Execute();
                    _actionResult.StateResult = True
                    '_actionResult.Message = String.Join(vbCrLf, (From e In resultList Where e.Status = 2 Select e.Message).ToList())
                    _actionResult.ObjectEmbbeded = _entranceVoucher
                    scope.Complete()
                    'Return New ActionResult < Domain.Entities.EntranceVoucher > {StateResult = True, ObjectEmbbeded = EntranceVoucher};
                End Using
            Catch ex As System.Data.Entity.Core.OptimisticConcurrencyException
                _actionResult.StateResult = False
                Assert.Fail(ResourceManager.GetString("ErrorConcurrence"))
                'IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                'Return New ActionResult < Domain.Entities.EntranceVoucher > {StateResult = False, Message = ResourceManager.get_GetString("ErrorConcurrence")}
            Catch ex As System.Data.Entity.Core.UpdateException
                _actionResult.StateResult = False
                Assert.Fail(ResourceManager.GetString("ErrorUnknown"))
                'IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                'Return New ActionResult < Domain.Entities.EntranceVoucher > {StateResult = False, Message = ResourceManager.get_GetString("ErrorUnknown")};

            Catch ex As Exception
                _actionResult.StateResult = False
                Assert.Fail(ex.Message.ToString())
                'IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                'Return New ActionResult < Domain.Entities.EntranceVoucher > {StateResult = False, Message = ex.Message.ToString()};
            End Try
        End Sub

        <TechTalk.SpecFlow.Then("Este es almacenado")> _
        Public Sub EntoncesEsteEsAlmacenado()
            Assert.IsTrue(_actionResult.StateResult, "Respuesta Falsa")
        End Sub

    End Class

End Namespace
