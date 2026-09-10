'***********************************************************************
' Assembly         : Application.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 31-03-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Data.Entity.Core
Imports System.Data.Entity.Infrastructure
Imports System.Data.SqlClient
Imports System.Text
Imports System.Transactions
Imports Application.Base
Imports Application.EventHandlers
Imports Application.EventHandlers.Enums.Enums
Imports Application.EventHandlers.Model
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources

Public Class AccountPayableAdminService
    Implements IAccountPayableAdminService

#Region "Properties"

    ''' <summary>
    ''' Variable tipo repositorio para una cuenta por pagar
    ''' </summary>
    ''' <remarks></remarks>
    Private _accountPayableRepository As IAccountPayableRepository

    ''' <summary>
    ''' Repositorio de gastos generales
    ''' </summary>
    ''' <remarks></remarks>
    Private _distributionDirectCostRepository As ICostDistributionDirectCostRepository

    ''' <summary>
    ''' Variable tipo repositorio para parametros de pago
    ''' </summary>
    ''' <remarks></remarks>
    Private _settingsPaymentsRepository As ISettingPaymentsRepository

    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As ISequensePaymentsDRepository

    ''' <summary>
    ''' Repositorio de causacion diferida
    ''' </summary>
    ''' <remarks></remarks>
    Private _deferredCausationAdminService As IDeferredCausationAdminService

    ''' <summary>
    ''' Servicios de aplicacion de control en pagos
    ''' </summary>
    ''' <remarks></remarks>
    Private _paymentControlAdminService As IPaymentControlAdminService

    ''' <summary>
    ''' Variable tipo repositorio para causacion diferida
    ''' </summary>
    ''' <remarks></remarks>
    Private _deferredCausationRepository As IDeferredCausationRepository

    ''' <summary>
    ''' Repositorio del mes cerrado
    ''' </summary>
    ''' <remarks></remarks>
    Private _repositoryCloseMont As ICloseMonthRepository

    ''' <summary>
    ''' Repositorio de cuenta contable
    ''' </summary>
    ''' <remarks></remarks>
    Private _repositoryMainAccounts As IPUCRepository

    ''' <summary>
    ''' Repositorio para los parametros de empresa
    ''' </summary>
    ''' <remarks></remarks>
    Private _companySettingsRepository As ICompanySettingsRepository

    ''' <summary>
    ''' Repositorio de proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Private _supplierRepository As ISupplierRepository

    ''' <summary>
    ''' repositorio de documento soporte electronico
    ''' </summary>
    Private _electronicSupportDocumentRepository As IElectronicSupportDocumentRepository

    ''' <summary>
    ''' Repositorio parametro cuentas contables
    ''' </summary>
    Private _settingsAccountRepository As ISettingsAccountRepository


    ''' <summary>
    ''' Repositorio parametro cuentas contables
    ''' </summary>
    Private _accountReceivableRepository As IAccountReceivableRepository

    ''' <summary>
    ''' Event Publisher
    ''' </summary>
    Private ReadOnly _eventProxy As IEventProxy


#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal accountPayableRepository As IAccountPayableRepository, ByVal DistributionDirectCostRepository As ICostDistributionDirectCostRepository, ByVal secuenseDRepository As ISequensePaymentsDRepository,
                   ByVal settingPayments As ISettingPaymentsRepository, ByVal deferredCausation As IDeferredCausationAdminService,
                   ByVal paymentControlAdminService As IPaymentControlAdminService, ByVal deferredCausationRepository As IDeferredCausationRepository,
                   ByVal repositoryCloseMont As ICloseMonthRepository, ByVal repositoryMainAccounts As IPUCRepository,
                   ByVal companySettingsRepository As ICompanySettingsRepository, supplierRepository As ISupplierRepository,
                   ByVal ElectronicSupportDocumentRepository As IElectronicSupportDocumentRepository, ByVal SettingsAccountRepository As ISettingsAccountRepository,
                   ByVal AccountReceivableRepository As IAccountReceivableRepository, eventProxy As IEventProxy)
        If accountPayableRepository Is Nothing Then
            Throw New ArgumentNullException("accountPayableRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        If settingPayments Is Nothing Then
            Throw New ArgumentNullException("settingPayments")
        End If
        If deferredCausation Is Nothing Then
            Throw New ArgumentNullException("deferredCausation")
        End If
        If paymentControlAdminService Is Nothing Then
            Throw New ArgumentNullException("paymentControlAdminService Vacio")
        End If
        If companySettingsRepository Is Nothing Then
            Throw New ArgumentNullException("companySettingsRepository Vacio")
        End If
        If supplierRepository Is Nothing Then
            Throw New ArgumentNullException("supplierRepository Vacio")
        End If
        _accountPayableRepository = accountPayableRepository
        _distributionDirectCostRepository = DistributionDirectCostRepository
        _secuenseDRepository = secuenseDRepository
        _settingsPaymentsRepository = settingPayments
        _deferredCausationAdminService = deferredCausation
        _paymentControlAdminService = paymentControlAdminService
        _deferredCausationRepository = deferredCausationRepository
        _repositoryCloseMont = repositoryCloseMont
        _repositoryMainAccounts = repositoryMainAccounts
        _companySettingsRepository = companySettingsRepository
        _supplierRepository = supplierRepository
        _electronicSupportDocumentRepository = ElectronicSupportDocumentRepository
        _settingsAccountRepository = SettingsAccountRepository
        _accountReceivableRepository = AccountReceivableRepository
        _eventProxy = eventProxy
    End Sub

#End Region

#Region "Methods"

    Public Function ImportBillsToAccountPayable(data As List(Of List(Of String)), dataImport As List(Of ImportFileRow), ParamArray parameters() As Object) As ActionResult(Of List(Of AccountPayable)) Implements IAccountPayableAdminService.ImportBillsToAccountPayable
        If data Is Nothing And dataImport Is Nothing Then
            Throw New ArgumentNullException("Data")
        End If

        'Listado de errores
        Dim listErrors As New List(Of String)

        'Listado que se devuelve para pegar a la rejilla del form
        Dim ListAccountPayables As New List(Of AccountPayable)

        Try
            Dim xmlObject As Object = New Object
            If data?.Any() AndAlso dataImport Is Nothing Then
                xmlObject = ConvertBillsToXml(data)
            End If
            If dataImport?.Any() AndAlso data Is Nothing Then
                xmlObject = ConvertBillsToXmlImport(dataImport)
            End If
            Dim xmlParameters = ConvertParametersToXml(parameters)

            'Se consume el procedimiento almacenado
            Dim resultStore = _accountPayableRepository.SP_ImportBillsToAccountPayable(xmlObject, xmlParameters)

            'Se crean los objetos para devolver y pegar en la rejilla
            If resultStore IsNot Nothing AndAlso resultStore.Count > 0 Then
                For Each itemXml In resultStore
                    If itemXml.StatusField = 1 Then 'Si el estado del item es True y pasó todas las validaciones
                        'Se crea el nuevo objeto para agregarlo al listado
                        Dim accountPayable As AccountPayable = ListAccountPayables.Where(Function(x) x.BillNumber = itemXml.BillNumber).FirstOrDefault()
                        If accountPayable Is Nothing Then
                            accountPayable = New AccountPayable() With
                            {
                                .BillNumber = itemXml.BillNumber,
                                .BillDate = itemXml.BillDate,
                                .Shares = 1,
                                .Term = itemXml.Term,
                                .ExpirationDate = itemXml.ExpirationDate,
                                .IdAccount = itemXml.IdAccount,
                                .NumberNameMainAccount = itemXml.NumberNameMainAccount,
                                .IdCostCenter = If(itemXml.IdCostCenter > 0, itemXml.IdCostCenter, Nothing),
                                .DescriptionCostCenter = itemXml.DescriptionCostCenter,
                                .Coments = itemXml.Coments,
                                .InvoiceValue = itemXml.InvoiceValue,
                                .Value = itemXml.Value,
                                .Balance = itemXml.Balance,
                                .Status = 1,
                                .CurrencyAbbreviation = itemXml.CurrencyAbbreviation,
                                .DeductibleIva = itemXml.DeductibleIva
                            }

                            'Si el registro que se esta pegando tiene conceptos se crea la cuota
                            If itemXml.DetailIdConceptAccountPayable IsNot Nothing Then
                                Dim accountPayableShare As New AccountPayableShares() With
                                {
                                    .Share = 1,
                                    .DateExpires = itemXml.ExpirationDate,
                                    .InitialValue = itemXml.Value,
                                    .Balance = itemXml.Value
                                }

                                accountPayable.AccountPayableShares.Add(accountPayableShare)
                            End If

                            ListAccountPayables.Add(accountPayable)
                        End If

                        If itemXml.DetailIdConceptAccountPayable IsNot Nothing Then
                            Dim accountPayableDetailConcept As New AccountPayableDetailConcept() With
                            {
                                .IdConceptAccountPayable = itemXml.DetailIdConceptAccountPayable,
                                .DescriptionPaymentConcept = itemXml.DetailDescriptionPaymentConcept,
                                .IdAccount = itemXml.DetailIdAccount,
                                .NumberNameMainAccount = itemXml.DetailNumberNameMainAccount,
                                .IdThirdParty = itemXml.DetailIdThirdParty,
                                .DescriptionThirdParty = itemXml.DetailDescriptionThirdParty,
                                .DeferredCausation = itemXml.DetailDeferredCausation,
                                .HandlesDeferredCausation = itemXml.DetailDeferredCausation,
                                .IdCostCenter = itemXml.DetailIdCostCenter,
                                .DescriptionCostCenter = itemXml.DetailDescriptionCostCenter,
                                .Detail = itemXml.DetailDetail,
                                .IdRetentionConcept = itemXml.DetailIdRetentionConcept,
                                .DescriptionRetentionConcept = itemXml.DetailDescriptionRetentionConcept,
                                .Percentage = itemXml.DetailPercentage,
                                .Value = itemXml.DetailValue,
                                .BillingValue = itemXml.DetailBillingValue,
                                .Nature = itemXml.DetailNature,
                                .BaseValue = itemXml.DetailBaseValue,
                                .IvaValue = itemXml.IvaValue,
                                .TotalConcept = itemXml.TotalConcept,
                                .RateIva = itemXml.RateIvaId
                            }

                            accountPayable.AccountPayableDetailConcept.Add(accountPayableDetailConcept)
                        End If
                    Else 'Si el estado del item es False y no pasó alguna validación
                        listErrors.Add(itemXml.MessageField)
                    End If
                Next
            End If

            Return New ActionResult(Of List(Of AccountPayable)) With {.StateResult = True, .ObjectEmbbeded = ListAccountPayables, .MessageResult = listErrors}
        Catch ex As SqlException
            If ex.ErrorCode = -2146232060 Then
                Return New ActionResult(Of List(Of AccountPayable)) With {.StateResult = False, .MessageResult = {"Los valores contienen decimales con un formato no valido, por favor corrija para poder continuar"}.ToList()}
            Else
                Return New ActionResult(Of List(Of AccountPayable)) With {.StateResult = False, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList()}
            End If
        Catch ex As Exception
            Return New ActionResult(Of List(Of AccountPayable)) With {.StateResult = False, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList()}
        End Try
    End Function

    ''' <summary>
    ''' Confirma la cuenta por pagar
    ''' </summary>
    ''' <param name="listAccountPayable"></param>
    ''' <param name="audit"></param>
    ''' <param name="isMassiveConfirm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConfirmAccountPayable(listAccountPayable As List(Of AccountPayable), audit As AuditMessage, Optional isMassiveConfirm As Boolean = False) As ActionResult(Of List(Of Tuple(Of String, Integer))) Implements IAccountPayableAdminService.ConfirmAccountPayable
        If listAccountPayable Is Nothing Then
            Throw New ArgumentNullException("listAccountPayable")
        End If

        Dim UnitOfWork As IUnitWork = _accountPayableRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim message As String = String.Empty
                Dim messageProvision As String = String.Empty
                Dim listCodes As New List(Of Tuple(Of String, Integer))

                Dim AccountsPayableXml As String = ConvertToXmlListAccountPayable(listAccountPayable)

                Dim resultStore = Me._accountPayableRepository.SP_ConfirmAccountsPayable(AccountsPayableXml, audit.CodeUser, isMassiveConfirm)
                If Not isMassiveConfirm Then
                    If resultStore Is Nothing OrElse resultStore.Count = 0 Or resultStore.Any(Function(r) r.CodeMessage <> 0) Then
                        UnitOfWork.RollbackChanges()
                        transaction.Dispose()
                        Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = resultStore.Where(Function(r) r.CodeMessage <> 0).FirstOrDefault.Message}
                    End If

                    For Each item In listAccountPayable
                        Dim DistributionDirectCost As CostDistributionDirectCost = _distributionDirectCostRepository.GetDistributionDirectCostByAccountPayableId(item.Id)

                        If DistributionDirectCost.CostDistributionDirectCostLegalizedDocuments.Any() Then

                            Dim ReverseProvisionDocuments = _distributionDirectCostRepository.SP_ReverseProvisionDocumentDistributionDirectCost(DistributionDirectCost.Id, audit.CodeUser, 1)
                            If ReverseProvisionDocuments Is Nothing OrElse ReverseProvisionDocuments.Count = 0 Or ReverseProvisionDocuments.Any(Function(r) r.CodeMessage <> 0) Then
                                UnitOfWork.RollbackChanges()
                                transaction.Dispose()
                                Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = ReverseProvisionDocuments.Where(Function(r) r.CodeMessage <> 0).FirstOrDefault.Message}
                            End If

                            ReverseProvisionDocuments.ForEach(Sub(r) messageProvision = messageProvision + r.Message + vbCrLf)

                        End If
                    Next

                    resultStore.ForEach(Sub(r) listCodes.Add(New Tuple(Of String, Integer)(r.Consecutive, If(r.CodeMessage <> 0, 2, 1))))
                    transaction.Complete()
                Else
                    If resultStore.Any(Function(r) r.CodeMessage <> 0) Then
                        UnitOfWork.RollbackChanges()
                        transaction.Dispose()

                        resultStore.Where(Function(r) r.CodeMessage <> 0).ToList().ForEach(Sub(r)
                                                                                               listCodes.Add(New Tuple(Of String, Integer)(r.Message, 2))
                                                                                           End Sub)
                    Else
                        resultStore.ForEach(Sub(r)
                                                listCodes.Add(New Tuple(Of String, Integer)(r.Message, 1))
                                            End Sub)

                        transaction.Complete()
                    End If
                End If

                resultStore.ForEach(Sub(r) message = message + r.Message + vbCrLf)
                message = message + messageProvision

                Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = True, .ObjectEmbbeded = listCodes, .Message = message}
            Catch ex As DbUpdateException
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex), .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList}
            Catch ex As Exception
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex), .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' funcion para generar y confirmar el documento soporte apartir de cuentas por pagar
    ''' </summary>
    ''' <param name="listAccountPayable"></param>
    ''' <param name="audit"></param>
    ''' <param name="isMassiveConfirm"></param>
    ''' <returns></returns>
    Public Function GenerateElectronicSupportDocument(listAccountPayable As List(Of AccountPayable), audit As AuditMessage, Optional isMassiveConfirm As Boolean = False) As ActionResult(Of List(Of Tuple(Of String, String)))
        If listAccountPayable Is Nothing Then
            Throw New ArgumentNullException("listAccountPayable")
        End If

        Dim UnitOfWork As IUnitWork = _electronicSupportDocumentRepository.UnitWork
        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim message As String = String.Empty
                Dim listCodes As New List(Of Tuple(Of String, String))

                Dim AccountsPayableXml As String = ConvertToXmlListAccountPayableIds(listAccountPayable, audit)

                Dim resultStore = Me._electronicSupportDocumentRepository.SP_GenerateElectronicSupportDocument(AccountsPayableXml)

                If resultStore.Any(Function(r) r.Code = "999") Then
                    UnitOfWork.RollbackChanges()
                    transaction.Dispose()

                    resultStore.ToList().ForEach(Sub(r)
                                                     listCodes.Add(New Tuple(Of String, String)(r.Code, r.Message))
                                                 End Sub)

                    Return New ActionResult(Of List(Of Tuple(Of String, String))) With {.StateResult = False, .ObjectEmbbeded = listCodes, .Message = message}

                End If

                Dim IdsElectronicDS = resultStore.FindAll(Function(s) s.Code = "002").Select(Function(d) d.IdDS).ToList()

                resultStore.ForEach(Sub(r)
                                        listCodes.Add(New Tuple(Of String, String)(r.Code, r.Message))
                                    End Sub)

                If IdsElectronicDS.Any() Then

                    Dim _electronicDocumentSupport = Me._electronicSupportDocumentRepository.GetByFilter(Function(x) IdsElectronicDS.Contains(x.Id)).ToList()

                    If Not _electronicDocumentSupport.Any() Then
                        UnitOfWork.RollbackChanges()
                        transaction.Dispose()
                        Return New ActionResult(Of List(Of Tuple(Of String, String))) With {.StateResult = False, .ObjectEmbbeded = {New Tuple(Of String, String)("999", "Error Consultando los documentos soporte electronicos")}.ToList()}
                    End If

                    For Each Item In _electronicDocumentSupport
                        Dim settingsAccount = Me._settingsAccountRepository.GetSettingAccountSimple(Item.OperativeUnitId)

                        If settingsAccount Is Nothing OrElse settingsAccount.Id = 0 OrElse String.IsNullOrEmpty(settingsAccount.SoftwarePin) Then
                            Return New ActionResult(Of List(Of Tuple(Of String, String))) With {.StateResult = False, .ObjectEmbbeded = {New Tuple(Of String, String)("999", "No hay parametros de cuentas contables confirgurado para la unidad operativa")}.ToList()}
                        End If

                        Item.SoftwarePin = settingsAccount.SoftwarePin
                        Item.Environment = IIf(settingsAccount.SupportDocumentEnvironment, 1, 2)
                        Item.CUDS = Item.GetCUDSCode()
                        Me._electronicSupportDocumentRepository.SaveEntity(Item)
                    Next
                Else
                    UnitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of List(Of Tuple(Of String, String))) With {.StateResult = False, .ObjectEmbbeded = listCodes, .Message = message}
                End If

                UnitOfWork.Commit()
                transaction.Complete()

                resultStore.ForEach(Sub(r) message = message + r.Message + vbCrLf)

                Return New ActionResult(Of List(Of Tuple(Of String, String))) With {.StateResult = True, .ObjectEmbbeded = listCodes, .Message = message}
            Catch ex As DbUpdateException
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of List(Of Tuple(Of String, String))) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex) + ex.StackTrace, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList}
            Catch ex As Exception
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of List(Of Tuple(Of String, String))) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex) + ex.StackTrace, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Funcion para publicar el documento soporte a partir de cuentas por pagar
    ''' </summary>
    ''' <param name="listAccountPayable"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Private Function PublishElectronicSupporDocumentMessages(listAccountPayable As List(Of AccountPayable), audit As AuditMessage) As ActionResult(Of List(Of String))
        If listAccountPayable Is Nothing Then
            Throw New ArgumentNullException("listAccountPayable")
        End If

        Try
            Dim data As New List(Of SupportDocumentEvent)

            data = listAccountPayable.Select(Function(a) New SupportDocumentEvent With {
                .EntityId = a.Id,
                .EntityCode = a.Code,
                .EntityName = "AccountPayable",
                .DocumentDate = a.DocumentDate}).ToList()

            _eventProxy.Publish(New EventData(data, NameOf(EventType.SupportDocument), NameOf(EventAction.added), audit.Company, audit.CodeUser, DateTime.Now().GetTimestamp))

            Return New ActionResult(Of List(Of String)) With {.StateResult = True}
        Catch ex As Exception
            Return New ActionResult(Of List(Of String)) With {.StateResult = False, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Método que convierte a xml el listado de CxP
    ''' </summary>
    ''' <param name="ListAccountPayable"></param>
    ''' <returns></returns>
    Private Function ConvertToXml(ListAccountPayable As List(Of AccountPayable), ListDeferredCausation As List(Of DeferredCausation))
        Dim builder As New StringBuilder
        Dim TempAccountPayableId As Integer = 1
        Dim TempDeferredCausationId As Integer = 1
        Dim TempAccountPayableDetailConceptId As Integer = 1
        Dim TempAccountPayableDetailConceptLiquidationId As Integer = 1

        For Each accountPayable In ListAccountPayable
            builder.Append("<AccountPayable>")

            builder.Append("<TempAccountPayableId>" & TempAccountPayableId & "</TempAccountPayableId>")
            builder.Append("<Id>" & accountPayable.Id & "</Id>")
            builder.Append("<NumberFiling>" & accountPayable.NumberFiling & "</NumberFiling>")
            builder.Append("<EntityId>" & accountPayable.EntityId & "</EntityId>")
            builder.Append("<EntityCode>" & accountPayable.EntityCode & "</EntityCode>")
            builder.Append("<EntityName>" & accountPayable.EntityName & "</EntityName>")
            builder.Append("<IdSupplier>" & accountPayable.IdSupplier & "</IdSupplier>")
            builder.Append("<IdThirdParty>" & accountPayable.IdThirdParty & "</IdThirdParty>")
            builder.Append("<IdAccount>" & accountPayable.IdAccount & "</IdAccount>")
            builder.Append("<IdCostCenter>" & accountPayable.IdCostCenter & "</IdCostCenter>")
            builder.Append("<BillNumber>" & accountPayable.BillNumber & "</BillNumber>")
            builder.Append("<BillDate>" & accountPayable.BillDate.ToString("dd/MM/yyyy HH:mm:ss") & "</BillDate>")
            builder.Append("<FilingUnitId>" & accountPayable.FilingUnitId & "</FilingUnitId>")
            builder.Append("<SupplierTypeId>" & accountPayable.SupplierTypeId & "</SupplierTypeId>")
            builder.Append("<Term>" & accountPayable.Term & "</Term>")
            builder.Append("<ExpirationDate>" & accountPayable.ExpirationDate.ToString("dd/MM/yyyy HH:mm:ss") & "</ExpirationDate>")
            builder.Append("<Coments>" & accountPayable.Coments & "</Coments>")
            builder.Append("<Status>" & accountPayable.Status & "</Status>")
            builder.Append("<InitialBalance>" & accountPayable.InitialBalance & "</InitialBalance>")
            builder.Append("<IdInitialBalance>" & accountPayable.IdInitialBalance & "</IdInitialBalance>")
            builder.Append("<PreviousBudget>" & accountPayable.PreviousBudget & "</PreviousBudget>")
            builder.Append("<Shares>" & accountPayable.Shares & "</Shares>")
            builder.Append("<InvoiceValue>" & accountPayable.InvoiceValue & "</InvoiceValue>")
            builder.Append("<Value>" & accountPayable.Value & "</Value>")
            builder.Append("<Balance>" & accountPayable.Balance & "</Balance>")
            builder.Append("<IdSuppliersDistributionLines>" & accountPayable.IdSuppliersDistributionLines & "</IdSuppliersDistributionLines>")
            builder.Append("<CostDistributionDirectCostId>" & accountPayable.CostDistributionDirectCostId & "</CostDistributionDirectCostId>")
            builder.Append("<PositionId>" & accountPayable.PositionId & "</PositionId>")
            builder.Append("<Hours>" & accountPayable.Hours & "</Hours>")
            builder.Append("<ChangeProperties>" & accountPayable.ChangeProperties & "</ChangeProperties>")
            builder.Append("<AuxEntityName>" & accountPayable.AuxEntityName & "</AuxEntityName>")
            builder.Append("<AuxEntityCode>" & accountPayable.AuxEntityCode & "</AuxEntityCode>")
            builder.Append("<AuxEntityId>" & accountPayable.AuxEntityId & "</AuxEntityId>")
            builder.Append("<Code>" & accountPayable.Code & "</Code>")
            builder.Append("<DocumentDate>" & accountPayable.DocumentDate.ToString("dd/MM/yyyy HH:mm:ss") & "</DocumentDate>")
            builder.Append("<ServicePeriodDate>" & accountPayable.ServicePeriodDate.ToString("dd/MM/yyyy HH:mm:ss") & "</ServicePeriodDate>")
            builder.Append("<OperatingUnitId>" & accountPayable.IdOperatingUnit & "</OperatingUnitId>")
            builder.Append("<IsDelete>" & IIf(accountPayable.ChangeTracker.State = ObjectState.Deleted, 1, 0) & "</IsDelete>")
            builder.Append("<CommitmentDetailId>" & accountPayable.CommitmentDetailId & "</CommitmentDetailId>")
            builder.Append("<HandlesDocumentSupport>" & accountPayable.HandlesDocumentSupport & "</HandlesDocumentSupport>")
            builder.Append("<DeductibleIva>" & accountPayable.DeductibleIva & "</DeductibleIva>")
            builder.Append("<TaxRegistration>" & accountPayable.TaxRegistration & "</TaxRegistration>")
            If accountPayable.DocumentSupportId IsNot Nothing Then
                builder.Append("<DocumentSupportId>" & accountPayable.DocumentSupportId & "</DocumentSupportId>")
            End If
            builder.Append("<CurrencyId>" & accountPayable?.CurrencyId & "</CurrencyId>")
            If accountPayable.IdEconomicActivity IsNot Nothing Then
                builder.Append("<IdEconomicActivity>" & accountPayable?.IdEconomicActivity & "</IdEconomicActivity>")
            End If

            If accountPayable.AccountPayableDetailConcept IsNot Nothing AndAlso accountPayable.AccountPayableDetailConcept.Count > 0 Then
                For Each accountPayableDetailConcept In accountPayable.AccountPayableDetailConcept
                    builder.Append("<AccountPayableDetailConcept>")

                    builder.Append("<TempAccountPayableId>" & TempAccountPayableId & "</TempAccountPayableId>")
                    builder.Append("<TempAccountPayableDetailConceptId>" & TempAccountPayableDetailConceptId & "</TempAccountPayableDetailConceptId>")
                    builder.Append("<Id>" & accountPayableDetailConcept.Id & "</Id>")
                    builder.Append("<IdAccountPayable>" & accountPayableDetailConcept.IdAccountPayable & "</IdAccountPayable>")
                    builder.Append("<IdConceptAccountPayable>" & accountPayableDetailConcept.IdConceptAccountPayable & "</IdConceptAccountPayable>")
                    builder.Append("<IdAccount>" & accountPayableDetailConcept.IdAccount & "</IdAccount>")
                    builder.Append("<IdThirdParty>" & accountPayableDetailConcept.IdThirdParty & "</IdThirdParty>")
                    builder.Append("<IdCostCenter>" & accountPayableDetailConcept.IdCostCenter & "</IdCostCenter>")
                    builder.Append("<Nature>" & accountPayableDetailConcept.Nature & "</Nature>")
                    builder.Append("<BaseValue>" & accountPayableDetailConcept.BaseValue & "</BaseValue>")
                    builder.Append("<RateIva>" & accountPayableDetailConcept.RateIva & "</RateIva>")
                    builder.Append("<IvaValue>" & accountPayableDetailConcept.IvaValue & "</IvaValue>")
                    builder.Append("<TotalConcept>" & accountPayableDetailConcept.TotalConcept & "</TotalConcept>")
                    builder.Append("<BillingValue>" & accountPayableDetailConcept.BillingValue & "</BillingValue>")
                    builder.Append("<Value>" & accountPayableDetailConcept.Value & "</Value>")
                    builder.Append("<IdRetentionConcept>" & accountPayableDetailConcept.IdRetentionConcept & "</IdRetentionConcept>")
                    builder.Append("<Percentage>" & accountPayableDetailConcept.Percentage & "</Percentage>")
                    builder.Append("<Detail>" & accountPayableDetailConcept.Detail & "</Detail>")
                    builder.Append("<DeferredCausation>" & accountPayableDetailConcept.DeferredCausation & "</DeferredCausation>")
                    builder.Append("<IsDirectCost>" & accountPayableDetailConcept.IsDirectCost & "</IsDirectCost>")
                    builder.Append("<IsDelete>" & IIf(accountPayableDetailConcept.ChangeTracker.State = ObjectState.Deleted, 1, 0) & "</IsDelete>")

                    If accountPayableDetailConcept.AccountPayableDetailConceptLiquidation IsNot Nothing AndAlso accountPayableDetailConcept.AccountPayableDetailConceptLiquidation.Count > 0 Then
                        For Each accountPayableDetailConceptLiquidation In accountPayableDetailConcept.AccountPayableDetailConceptLiquidation
                            builder.Append("<AccountPayableDetailConceptLiquidation>")

                            builder.Append("<TempAccountPayableDetailConceptId>" & TempAccountPayableDetailConceptId & "</TempAccountPayableDetailConceptId>")
                            builder.Append("<TempAccountPayableDetailConceptLiquidationId>" & TempAccountPayableDetailConceptLiquidationId & "</TempAccountPayableDetailConceptLiquidationId>")
                            builder.Append("<Id>" & accountPayableDetailConceptLiquidation.Id & "</Id>")
                            builder.Append("<AccountPayableDetailConceptId>" & accountPayableDetailConceptLiquidation.AccountPayableDetailConceptId & "</AccountPayableDetailConceptId>")
                            builder.Append("<TotalIncome>" & accountPayableDetailConceptLiquidation.TotalIncome & "</TotalIncome>")
                            builder.Append("<PensionFundContribution>" & accountPayableDetailConceptLiquidation.PensionFundContribution & "</PensionFundContribution>")
                            builder.Append("<PensionFundContributionReal>" & accountPayableDetailConceptLiquidation.PensionFundContributionReal & "</PensionFundContributionReal>")
                            builder.Append("<VoluntaryPensionFundContribution>" & accountPayableDetailConceptLiquidation.VoluntaryPensionFundContribution & "</VoluntaryPensionFundContribution>")
                            builder.Append("<VoluntaryPensionFundContributionReal>" & accountPayableDetailConceptLiquidation.VoluntaryPensionFundContributionReal & "</VoluntaryPensionFundContributionReal>")
                            builder.Append("<SolidarityPensionFund>" & accountPayableDetailConceptLiquidation.SolidarityPensionFund & "</SolidarityPensionFund>")
                            builder.Append("<SolidarityPensionFundReal>" & accountPayableDetailConceptLiquidation.SolidarityPensionFundReal & "</SolidarityPensionFundReal>")
                            builder.Append("<ContributionAccountAFC>" & accountPayableDetailConceptLiquidation.ContributionAccountAFC & "</ContributionAccountAFC>")
                            builder.Append("<ContributionAccountAFCReal>" & accountPayableDetailConceptLiquidation.ContributionAccountAFCReal & "</ContributionAccountAFCReal>")
                            builder.Append("<TotalIncomeExempt>" & accountPayableDetailConceptLiquidation.TotalIncomeExempt & "</TotalIncomeExempt>")
                            builder.Append("<TotalIncomeExemptReal>" & accountPayableDetailConceptLiquidation.TotalIncomeExemptReal & "</TotalIncomeExemptReal>")
                            builder.Append("<PaymentCompulsoryHealth>" & accountPayableDetailConceptLiquidation.PaymentCompulsoryHealth & "</PaymentCompulsoryHealth>")
                            builder.Append("<PaymentCompulsoryHealthReal>" & accountPayableDetailConceptLiquidation.PaymentCompulsoryHealthReal & "</PaymentCompulsoryHealthReal>")
                            builder.Append("<PaymentPrepaidMedical>" & accountPayableDetailConceptLiquidation.PaymentPrepaidMedical & "</PaymentPrepaidMedical>")
                            builder.Append("<PaymentPrepaidMedicalReal>" & accountPayableDetailConceptLiquidation.PaymentPrepaidMedicalReal & "</PaymentPrepaidMedicalReal>")
                            builder.Append("<PaymentForDependent>" & accountPayableDetailConceptLiquidation.PaymentForDependent & "</PaymentForDependent>")
                            builder.Append("<PaymentForDependentReal>" & accountPayableDetailConceptLiquidation.PaymentForDependentReal & "</PaymentForDependentReal>")
                            builder.Append("<HousingLoanInterest>" & accountPayableDetailConceptLiquidation.HousingLoanInterest & "</HousingLoanInterest>")
                            builder.Append("<HousingLoanInterestReal>" & accountPayableDetailConceptLiquidation.HousingLoanInterestReal & "</HousingLoanInterestReal>")
                            builder.Append("<OccupationalRiskContribution>" & accountPayableDetailConceptLiquidation.OccupationalRiskContribution & "</OccupationalRiskContribution>")
                            builder.Append("<OccupationalRiskContributionReal>" & accountPayableDetailConceptLiquidation.OccupationalRiskContributionReal & "</OccupationalRiskContributionReal>")
                            builder.Append("<TotalDeduction>" & accountPayableDetailConceptLiquidation.TotalDeduction & "</TotalDeduction>")
                            builder.Append("<TotalDeductionReal>" & accountPayableDetailConceptLiquidation.TotalDeductionReal & "</TotalDeductionReal>")
                            builder.Append("<SubTotal>" & accountPayableDetailConceptLiquidation.SubTotal & "</SubTotal>")
                            builder.Append("<ExemptIncome>" & accountPayableDetailConceptLiquidation.ExemptIncome & "</ExemptIncome>")
                            builder.Append("<TaxableBase>" & accountPayableDetailConceptLiquidation.TaxableBase & "</TaxableBase>")
                            builder.Append("<RetentionValue383>" & accountPayableDetailConceptLiquidation.RetentionValue383 & "</RetentionValue383>")
                            builder.Append("<RetentionValue384>" & accountPayableDetailConceptLiquidation.RetentionValue384 & "</RetentionValue384>")
                            builder.Append("<ApplyRetention>" & accountPayableDetailConceptLiquidation.ApplyRetention & "</ApplyRetention>")
                            builder.Append("<MaxDeductionsAndRentExents>" & accountPayableDetailConceptLiquidation.MaxDeductionsAndRentExents & "</MaxDeductionsAndRentExents>")
                            builder.Append("<PensionByIndividualSavingsRegime>" & accountPayableDetailConceptLiquidation.PensionByIndividualSavingsRegime & "</PensionByIndividualSavingsRegime>")
                            builder.Append("<PensionByIndividualSavingsRegimeReal>" & accountPayableDetailConceptLiquidation.PensionByIndividualSavingsRegimeReal & "</PensionByIndividualSavingsRegimeReal>")
                            builder.Append("<UVT>" & accountPayableDetailConceptLiquidation.UVT & "</UVT>")
                            builder.Append("<SMLV>" & accountPayableDetailConceptLiquidation.SMLV & "</SMLV>")
                            builder.Append("<PreviousDeductionsForWithholdings>" & accountPayableDetailConceptLiquidation.PreviousDeductionsForWithholdings & "</PreviousDeductionsForWithholdings>")
                            builder.Append("<AccumulatedIncome>" & accountPayableDetailConceptLiquidation.AccumulatedIncome & "</AccumulatedIncome>")
                            builder.Append("<IsDelete>" & IIf(accountPayableDetailConceptLiquidation.ChangeTracker.State = ObjectState.Deleted, 1, 0) & "</IsDelete>")

                            If accountPayableDetailConceptLiquidation.AccountPayableDetailConceptLiquidationValuesModificated IsNot Nothing AndAlso accountPayableDetailConceptLiquidation.AccountPayableDetailConceptLiquidationValuesModificated.Count > 0 Then
                                For Each valuesModificated In accountPayableDetailConceptLiquidation.AccountPayableDetailConceptLiquidationValuesModificated
                                    builder.Append("<AccountPayableDetailConceptLiquidationValuesModificated>")

                                    builder.Append("<TempAccountPayableDetailConceptLiquidationId>" & TempAccountPayableDetailConceptLiquidationId & "</TempAccountPayableDetailConceptLiquidationId>")
                                    builder.Append("<Id>" & valuesModificated.Id & "</Id>")
                                    builder.Append("<LiquidationId>" & valuesModificated.LiquidationId & "</LiquidationId>")
                                    builder.Append("<ConceptType>" & valuesModificated.ConceptType & "</ConceptType>")
                                    builder.Append("<PreviousValue>" & valuesModificated.PreviousValue & "</PreviousValue>")
                                    builder.Append("<NewValue>" & valuesModificated.NewValue & "</NewValue>")
                                    builder.Append("<Observations>" & valuesModificated.Observations & "</Observations>")
                                    builder.Append("<IsDelete>" & IIf(valuesModificated.ChangeTracker.State = ObjectState.Deleted, 1, 0) & "</IsDelete>")

                                    builder.Append("</AccountPayableDetailConceptLiquidationValuesModificated>")
                                Next
                            End If

                            If accountPayableDetailConceptLiquidation.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("AccountPayableDetailConceptLiquidationValuesModificated") Then
                                For Each valuesModificated As AccountPayableDetailConceptLiquidationValuesModificated In accountPayableDetailConceptLiquidation.ChangeTracker.ObjectsRemovedFromCollectionProperties.Item("AccountPayableDetailConceptLiquidationValuesModificated")
                                    If valuesModificated.Id = 0 Then
                                        Continue For
                                    End If

                                    builder.Append("<AccountPayableDetailConceptLiquidationValuesModificated>")

                                    builder.Append("<TempAccountPayableDetailConceptLiquidationId>" & TempAccountPayableDetailConceptLiquidationId & "</TempAccountPayableDetailConceptLiquidationId>")
                                    builder.Append("<Id>" & valuesModificated.Id & "</Id>")
                                    builder.Append("<LiquidationId>" & valuesModificated.LiquidationId & "</LiquidationId>")
                                    builder.Append("<ConceptType>" & valuesModificated.ConceptType & "</ConceptType>")
                                    builder.Append("<PreviousValue>" & valuesModificated.PreviousValue & "</PreviousValue>")
                                    builder.Append("<NewValue>" & valuesModificated.NewValue & "</NewValue>")
                                    builder.Append("<Observations>" & valuesModificated.Observations & "</Observations>")
                                    builder.Append("<IsDelete>" & 1 & "</IsDelete>")

                                    builder.Append("</AccountPayableDetailConceptLiquidationValuesModificated>")
                                Next
                            End If

                            If accountPayableDetailConceptLiquidation.AccountPayableDetailConceptLiquidationAdjusments IsNot Nothing AndAlso accountPayableDetailConceptLiquidation.AccountPayableDetailConceptLiquidationAdjusments.Count > 0 Then
                                For Each adjustment In accountPayableDetailConceptLiquidation.AccountPayableDetailConceptLiquidationAdjusments
                                    builder.Append("<AccountPayableDetailConceptLiquidationAdjusments>")

                                    builder.Append("<TempAccountPayableDetailConceptLiquidationId>" & TempAccountPayableDetailConceptLiquidationId & "</TempAccountPayableDetailConceptLiquidationId>")
                                    builder.Append("<Id>" & adjustment.Id & "</Id>")
                                    builder.Append("<LiquidationId>" & adjustment.LiquidationId & "</LiquidationId>")
                                    builder.Append("<ConceptType>" & adjustment.ConceptType & "</ConceptType>")
                                    builder.Append("<Nature>" & adjustment.Nature & "</Nature>")
                                    builder.Append("<Value>" & adjustment.Value & "</Value>")
                                    builder.Append("<Observations>" & adjustment.Observations & "</Observations>")
                                    builder.Append("<Status>" & adjustment.Status & "</Status>")
                                    builder.Append("<TotalIncome>" & adjustment.TotalIncome & "</TotalIncome>")
                                    builder.Append("<IsDelete>" & IIf(adjustment.ChangeTracker.State = ObjectState.Deleted, 1, 0) & "</IsDelete>")

                                    builder.Append("</AccountPayableDetailConceptLiquidationAdjusments>")
                                Next
                            End If

                            If accountPayableDetailConceptLiquidation.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("AccountPayableDetailConceptLiquidationAdjusments") Then
                                For Each adjustment As AccountPayableDetailConceptLiquidationAdjusments In accountPayableDetailConceptLiquidation.ChangeTracker.ObjectsRemovedFromCollectionProperties.Item("AccountPayableDetailConceptLiquidationAdjusments")
                                    If adjustment.Id = 0 Then
                                        Continue For
                                    End If

                                    builder.Append("<AccountPayableDetailConceptLiquidationAdjusments>")

                                    builder.Append("<TempAccountPayableDetailConceptLiquidationId>" & TempAccountPayableDetailConceptLiquidationId & "</TempAccountPayableDetailConceptLiquidationId>")
                                    builder.Append("<Id>" & adjustment.Id & "</Id>")
                                    builder.Append("<LiquidationId>" & adjustment.LiquidationId & "</LiquidationId>")
                                    builder.Append("<ConceptType>" & adjustment.ConceptType & "</ConceptType>")
                                    builder.Append("<Nature>" & adjustment.Nature & "</Nature>")
                                    builder.Append("<Value>" & adjustment.Value & "</Value>")
                                    builder.Append("<Observations>" & adjustment.Observations & "</Observations>")
                                    builder.Append("<Status>" & adjustment.Status & "</Status>")
                                    builder.Append("<TotalIncome>" & adjustment.TotalIncome & "</TotalIncome>")
                                    builder.Append("<IsDelete>" & 1 & "</IsDelete>")

                                    builder.Append("</AccountPayableDetailConceptLiquidationAdjusments>")
                                Next
                            End If

                            builder.Append("</AccountPayableDetailConceptLiquidation>")

                            TempAccountPayableDetailConceptLiquidationId += 1
                        Next
                    End If

                    If accountPayable.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("AccountPayableDetailConceptLiquidation") Then
                        For Each item As AccountPayableDetailConceptLiquidation In accountPayable.ChangeTracker.ObjectsRemovedFromCollectionProperties.Item("AccountPayableDetailConceptLiquidation")
                            If item.Id = 0 Then
                                Continue For
                            End If

                            builder.Append("<AccountPayableDetailConceptLiquidation>")

                            builder.Append("<TempAccountPayableDetailConceptId>" & TempAccountPayableDetailConceptId & "</TempAccountPayableDetailConceptId>")
                            builder.Append("<Id>" & item.Id & "</Id>")
                            builder.Append("<AccountPayableDetailConceptId>" & item.AccountPayableDetailConceptId & "</AccountPayableDetailConceptId>")
                            builder.Append("<TotalIncome>" & item.TotalIncome & "</TotalIncome>")
                            builder.Append("<PensionFundContribution>" & item.PensionFundContribution & "</PensionFundContribution>")
                            builder.Append("<PensionFundContributionReal>" & item.PensionFundContributionReal & "</PensionFundContributionReal>")
                            builder.Append("<VoluntaryPensionFundContribution>" & item.VoluntaryPensionFundContribution & "</VoluntaryPensionFundContribution>")
                            builder.Append("<VoluntaryPensionFundContributionReal>" & item.VoluntaryPensionFundContributionReal & "</VoluntaryPensionFundContributionReal>")
                            builder.Append("<SolidarityPensionFund>" & item.SolidarityPensionFund & "</SolidarityPensionFund>")
                            builder.Append("<SolidarityPensionFundReal>" & item.SolidarityPensionFundReal & "</SolidarityPensionFundReal>")
                            builder.Append("<ContributionAccountAFC>" & item.ContributionAccountAFC & "</ContributionAccountAFC>")
                            builder.Append("<ContributionAccountAFCReal>" & item.ContributionAccountAFCReal & "</ContributionAccountAFCReal>")
                            builder.Append("<TotalIncomeExempt>" & item.TotalIncomeExempt & "</TotalIncomeExempt>")
                            builder.Append("<TotalIncomeExemptReal>" & item.TotalIncomeExemptReal & "</TotalIncomeExemptReal>")
                            builder.Append("<PaymentCompulsoryHealth>" & item.PaymentCompulsoryHealth & "</PaymentCompulsoryHealth>")
                            builder.Append("<PaymentCompulsoryHealthReal>" & item.PaymentCompulsoryHealthReal & "</PaymentCompulsoryHealthReal>")
                            builder.Append("<PaymentPrepaidMedical>" & item.PaymentPrepaidMedical & "</PaymentPrepaidMedical>")
                            builder.Append("<PaymentPrepaidMedicalReal>" & item.PaymentPrepaidMedicalReal & "</PaymentPrepaidMedicalReal>")
                            builder.Append("<PaymentForDependent>" & item.PaymentForDependent & "</PaymentForDependent>")
                            builder.Append("<PaymentForDependentReal>" & item.PaymentForDependentReal & "</PaymentForDependentReal>")
                            builder.Append("<HousingLoanInterest>" & item.HousingLoanInterest & "</HousingLoanInterest>")
                            builder.Append("<HousingLoanInterestReal>" & item.HousingLoanInterestReal & "</HousingLoanInterestReal>")
                            builder.Append("<OccupationalRiskContribution>" & item.OccupationalRiskContribution & "</OccupationalRiskContribution>")
                            builder.Append("<OccupationalRiskContributionReal>" & item.OccupationalRiskContributionReal & "</OccupationalRiskContributionReal>")
                            builder.Append("<TotalDeduction>" & item.TotalDeduction & "</TotalDeduction>")
                            builder.Append("<TotalDeductionReal>" & item.TotalDeductionReal & "</TotalDeductionReal>")
                            builder.Append("<SubTotal>" & item.SubTotal & "</SubTotal>")
                            builder.Append("<ExemptIncome>" & item.ExemptIncome & "</ExemptIncome>")
                            builder.Append("<TaxableBase>" & item.TaxableBase & "</TaxableBase>")
                            builder.Append("<RetentionValue383>" & item.RetentionValue383 & "</RetentionValue383>")
                            builder.Append("<RetentionValue384>" & item.RetentionValue384 & "</RetentionValue384>")
                            builder.Append("<ApplyRetention>" & item.ApplyRetention & "</ApplyRetention>")
                            builder.Append("<MaxDeductionsAndRentExents>" & item.MaxDeductionsAndRentExents & "</MaxDeductionsAndRentExents>")
                            builder.Append("<PensionByIndividualSavingsRegime>" & item.PensionByIndividualSavingsRegime & "</PensionByIndividualSavingsRegime>")
                            builder.Append("<PensionByIndividualSavingsRegimeReal>" & item.PensionByIndividualSavingsRegimeReal & "</PensionByIndividualSavingsRegimeReal>")
                            builder.Append("<UVT>" & item.UVT & "</UVT>")
                            builder.Append("<SMLV>" & item.SMLV & "</SMLV>")
                            builder.Append("<IsDelete>" & 1 & "</IsDelete>")

                            builder.Append("</AccountPayableDetailConceptLiquidation>")
                        Next
                    End If

                    builder.Append("</AccountPayableDetailConcept>")

                    TempAccountPayableDetailConceptId += 1
                Next
            End If

            If accountPayable.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("AccountPayableDetailConcept") Then
                For Each item As AccountPayableDetailConcept In accountPayable.ChangeTracker.ObjectsRemovedFromCollectionProperties.Item("AccountPayableDetailConcept")
                    If item.Id = 0 Then
                        Continue For
                    End If

                    builder.Append("<AccountPayableDetailConcept>")

                    builder.Append("<TempAccountPayableId>" & TempAccountPayableId & "</TempAccountPayableId>")
                    builder.Append("<Id>" & item.Id & "</Id>")
                    builder.Append("<IdAccountPayable>" & item.IdAccountPayable & "</IdAccountPayable>")
                    builder.Append("<IdConceptAccountPayable>" & item.IdConceptAccountPayable & "</IdConceptAccountPayable>")
                    builder.Append("<IdAccount>" & item.IdAccount & "</IdAccount>")
                    builder.Append("<IdThirdParty>" & item.IdThirdParty & "</IdThirdParty>")
                    builder.Append("<IdCostCenter>" & item.IdCostCenter & "</IdCostCenter>")
                    builder.Append("<Nature>" & item.Nature & "</Nature>")
                    builder.Append("<BaseValue>" & item.BaseValue & "</BaseValue>")
                    builder.Append("<BillingValue>" & item.BillingValue & "</BillingValue>")
                    builder.Append("<Value>" & item.Value & "</Value>")
                    builder.Append("<IdRetentionConcept>" & item.IdRetentionConcept & "</IdRetentionConcept>")
                    builder.Append("<Percentage>" & item.Percentage & "</Percentage>")
                    builder.Append("<Detail>" & item.Detail & "</Detail>")
                    builder.Append("<DeferredCausation>" & item.DeferredCausation & "</DeferredCausation>")
                    builder.Append("<IsDirectCost>" & item.IsDirectCost & "</IsDirectCost>")
                    builder.Append("<IsDelete>" & 1 & "</IsDelete>")

                    builder.Append("</AccountPayableDetailConcept>")
                Next
            End If

            If accountPayable.AccountPayableShares IsNot Nothing AndAlso accountPayable.AccountPayableShares.Count > 0 Then
                For Each accountPayableShares In accountPayable.AccountPayableShares
                    builder.Append("<AccountPayableShares>")

                    builder.Append("<TempAccountPayableId>" & TempAccountPayableId & "</TempAccountPayableId>")
                    builder.Append("<Id>" & accountPayableShares.Id & "</Id>")
                    builder.Append("<IdAccountPayable>" & accountPayableShares.IdAccountPayable & "</IdAccountPayable>")
                    builder.Append("<Share>" & accountPayableShares.Share & "</Share>")
                    builder.Append("<DateExpires>" & accountPayableShares.DateExpires.ToString("dd/MM/yyyy HH:mm:ss") & "</DateExpires>")
                    builder.Append("<InitialValue>" & accountPayableShares.InitialValue & "</InitialValue>")
                    builder.Append("<DebitValue>" & accountPayableShares.DebitValue & "</DebitValue>")
                    builder.Append("<CreditValue>" & accountPayableShares.CreditValue & "</CreditValue>")
                    builder.Append("<ValueTransfers>" & accountPayableShares.ValueTransfers & "</ValueTransfers>")
                    builder.Append("<PaymentValue>" & accountPayableShares.PaymentValue & "</PaymentValue>")
                    builder.Append("<CrossingValue>" & accountPayableShares.CrossingValue & "</CrossingValue>")
                    builder.Append("<Balance>" & accountPayableShares.Balance & "</Balance>")
                    builder.Append("<IsDelete>" & IIf(accountPayableShares.ChangeTracker.State = ObjectState.Deleted, 1, 0) & "</IsDelete>")

                    builder.Append("</AccountPayableShares>")
                Next
            End If

            If accountPayable.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("AccountPayableShares") Then
                For Each item As AccountPayableShares In accountPayable.ChangeTracker.ObjectsRemovedFromCollectionProperties.Item("AccountPayableShares")
                    If item.Id = 0 Then
                        Continue For
                    End If

                    builder.Append("<AccountPayableShares>")

                    builder.Append("<TempAccountPayableId>" & TempAccountPayableId & "</TempAccountPayableId>")
                    builder.Append("<Id>" & item.Id & "</Id>")
                    builder.Append("<IdAccountPayable>" & item.IdAccountPayable & "</IdAccountPayable>")
                    builder.Append("<Share>" & item.Share & "</Share>")
                    builder.Append("<DateExpires>" & item.DateExpires.ToString("dd/MM/yyyy HH:mm:ss") & "</DateExpires>")
                    builder.Append("<InitialValue>" & item.InitialValue & "</InitialValue>")
                    builder.Append("<DebitValue>" & item.DebitValue & "</DebitValue>")
                    builder.Append("<CreditValue>" & item.CreditValue & "</CreditValue>")
                    builder.Append("<ValueTransfers>" & item.ValueTransfers & "</ValueTransfers>")
                    builder.Append("<PaymentValue>" & item.PaymentValue & "</PaymentValue>")
                    builder.Append("<CrossingValue>" & item.CrossingValue & "</CrossingValue>")
                    builder.Append("<Balance>" & item.Balance & "</Balance>")
                    builder.Append("<IsDelete>" & 1 & "</IsDelete>")

                    builder.Append("</AccountPayableShares>")
                Next
            End If

            If ListDeferredCausation IsNot Nothing AndAlso ListDeferredCausation.Count > 0 Then
                Dim listTemp = (From x In ListDeferredCausation Where x.BillNumber = accountPayable.BillNumber).ToList()
                If listTemp IsNot Nothing AndAlso listTemp.Count > 0 Then
                    For Each deferredCausation In listTemp
                        builder.Append("<DeferredCausation>")

                        builder.Append("<TempAccountPayableId>" & TempAccountPayableId & "</TempAccountPayableId>")
                        builder.Append("<TempDeferredCausationId>" & TempDeferredCausationId & "</TempDeferredCausationId>")
                        builder.Append("<Id>" & deferredCausation.Id & "</Id>")
                        builder.Append("<IdAccountPayable>" & deferredCausation.IdAccountPayable & "</IdAccountPayable>")
                        builder.Append("<BillNumber>" & deferredCausation.BillNumber & "</BillNumber>")
                        builder.Append("<IdMainAccount>" & deferredCausation.IdMainAccount & "</IdMainAccount>")
                        builder.Append("<PeriodsNumber>" & deferredCausation.PeriodsNumber & "</PeriodsNumber>")
                        builder.Append("<TypeDistribution>" & deferredCausation.TypeDistribution & "</TypeDistribution>")
                        builder.Append("<InitialDate>" & deferredCausation.InitialDate.ToString("dd/MM/yyyy HH:mm:ss") & "</InitialDate>")
                        builder.Append("<EndDate>" & deferredCausation.EndDate.ToString("dd/MM/yyyy HH:mm:ss") & "</EndDate>")
                        builder.Append("<IdThirdParty>" & deferredCausation.IdThirdParty & "</IdThirdParty>")
                        builder.Append("<IdCostCenter>" & deferredCausation.IdCostCenter & "</IdCostCenter>")
                        builder.Append("<ValueCreditPeriod>" & deferredCausation.ValueCreditPeriod & "</ValueCreditPeriod>")
                        builder.Append("<Status>" & deferredCausation.Status & "</Status>")
                        builder.Append("<IsDelete>" & IIf(deferredCausation.ChangeTracker.State = ObjectState.Deleted, 1, 0) & "</IsDelete>")

                        If deferredCausation.DeferredCausationDetails IsNot Nothing AndAlso deferredCausation.DeferredCausationDetails.Count > 0 Then
                            For Each deferredCausationDetails In deferredCausation.DeferredCausationDetails
                                builder.Append("<DeferredCausationDetails>")

                                builder.Append("<TempDeferredCausationId>" & TempDeferredCausationId & "</TempDeferredCausationId>")
                                builder.Append("<Id>" & deferredCausationDetails.Id & "</Id>")
                                builder.Append("<IdDeferredCausation>" & deferredCausationDetails.IdDeferredCausation & "</IdDeferredCausation>")
                                builder.Append("<IdMainAccount>" & deferredCausationDetails.IdMainAccount & "</IdMainAccount>")
                                builder.Append("<IdCostCenter>" & deferredCausationDetails.IdCostCenter & "</IdCostCenter>")
                                builder.Append("<Value>" & deferredCausationDetails.Value & "</Value>")
                                builder.Append("<Nature>" & deferredCausationDetails.Nature & "</Nature>")
                                builder.Append("<DateNextPeriod>" & deferredCausationDetails.DateNextPeriod.ToString("dd/MM/yyyy HH:mm:ss") & "</DateNextPeriod>")
                                builder.Append("<IsDelete>" & IIf(deferredCausationDetails.ChangeTracker.State = ObjectState.Deleted, 1, 0) & "</IsDelete>")

                                builder.Append("</DeferredCausationDetails>")
                            Next
                        End If

                        If deferredCausation.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("DeferredCausationDetails") Then
                            For Each item As DeferredCausationDetails In deferredCausation.ChangeTracker.ObjectsRemovedFromCollectionProperties.Item("DeferredCausationDetails")
                                If item.Id = 0 Then
                                    Continue For
                                End If

                                builder.Append("<DeferredCausationDetails>")

                                builder.Append("<TempDeferredCausationId>" & TempDeferredCausationId & "</TempDeferredCausationId>")
                                builder.Append("<Id>" & item.Id & "</Id>")
                                builder.Append("<IdDeferredCausation>" & item.IdDeferredCausation & "</IdDeferredCausation>")
                                builder.Append("<IdMainAccount>" & item.IdMainAccount & "</IdMainAccount>")
                                builder.Append("<IdCostCenter>" & item.IdCostCenter & "</IdCostCenter>")
                                builder.Append("<Value>" & item.Value & "</Value>")
                                builder.Append("<Nature>" & item.Nature & "</Nature>")
                                builder.Append("<DateNextPeriod>" & item.DateNextPeriod.ToString("dd/MM/yyyy HH:mm:ss") & "</DateNextPeriod>")
                                builder.Append("<IsDelete>" & 1 & "</IsDelete>")

                                builder.Append("</DeferredCausationDetails>")
                            Next
                        End If

                        If deferredCausation.DeferredCausationShare IsNot Nothing AndAlso deferredCausation.DeferredCausationShare.Count > 0 Then
                            For Each deferredCausationShare In deferredCausation.DeferredCausationShare
                                builder.Append("<DeferredCausationShare>")

                                builder.Append("<TempDeferredCausationId>" & TempDeferredCausationId & "</TempDeferredCausationId>")
                                builder.Append("<Id>" & deferredCausationShare.Id & "</Id>")
                                builder.Append("<DeferredCausationId>" & deferredCausationShare.DeferredCausationId & "</DeferredCausationId>")
                                builder.Append("<PaymentMonth>" & deferredCausationShare.PaymentMonth & "</PaymentMonth>")
                                builder.Append("<PaymentYear>" & deferredCausationShare.PaymentYear & "</PaymentYear>")
                                builder.Append("<Value>" & deferredCausationShare.Value & "</Value>")
                                builder.Append("<Amortized>" & deferredCausationShare.Amortized & "</Amortized>")
                                builder.Append("<IsDelete>" & IIf(deferredCausationShare.ChangeTracker.State = ObjectState.Deleted, 1, 0) & "</IsDelete>")

                                builder.Append("</DeferredCausationShare>")
                            Next
                        End If

                        If deferredCausation.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("DeferredCausationShare") Then
                            For Each item As DeferredCausationShare In deferredCausation.ChangeTracker.ObjectsRemovedFromCollectionProperties.Item("DeferredCausationShare")
                                If item.Id = 0 Then
                                    Continue For
                                End If

                                builder.Append("<DeferredCausationShare>")

                                builder.Append("<TempDeferredCausationId>" & TempDeferredCausationId & "</TempDeferredCausationId>")
                                builder.Append("<Id>" & item.Id & "</Id>")
                                builder.Append("<DeferredCausationId>" & item.DeferredCausationId & "</DeferredCausationId>")
                                builder.Append("<PaymentMonth>" & item.PaymentMonth & "</PaymentMonth>")
                                builder.Append("<PaymentYear>" & item.PaymentYear & "</PaymentYear>")
                                builder.Append("<Value>" & item.Value & "</Value>")
                                builder.Append("<Amortized>" & item.Amortized & "</Amortized>")
                                builder.Append("<IsDelete>" & 1 & "</IsDelete>")

                                builder.Append("</DeferredCausationShare>")
                            Next
                        End If

                        builder.Append("</DeferredCausation>")

                        TempDeferredCausationId += 1
                    Next
                End If
            End If

            If accountPayable.AccountPayableCommitments IsNot Nothing AndAlso accountPayable.AccountPayableCommitments.Count > 0 Then
                For Each AccountPayableCommitment In accountPayable.AccountPayableCommitments
                    builder.Append("<AccountPayableCommitments>")

                    builder.Append("<TempAccountPayableId>" & TempAccountPayableId & "</TempAccountPayableId>")
                    builder.Append("<Id>" & AccountPayableCommitment.Id & "</Id>")
                    builder.Append("<AccountPayableId>" & AccountPayableCommitment.AccountPayableId & "</AccountPayableId>")
                    builder.Append("<CommitmentDetailId>" & AccountPayableCommitment.CommitmentDetailId & "</CommitmentDetailId>")
                    builder.Append("<Value>" & AccountPayableCommitment.Value & "</Value>")
                    builder.Append("<IsDelete>" & IIf(AccountPayableCommitment.ChangeTracker.State = ObjectState.Deleted, 1, 0) & "</IsDelete>")

                    builder.Append("</AccountPayableCommitments>")
                Next
            End If

            If accountPayable.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("AccountPayableCommitments") Then
                For Each AccountPayableCommitment As AccountPayableCommitments In accountPayable.ChangeTracker.ObjectsRemovedFromCollectionProperties.Item("AccountPayableCommitments")
                    If AccountPayableCommitment.Id = 0 Then
                        Continue For
                    End If

                    builder.Append("<AccountPayableCommitments>")

                    builder.Append("<TempAccountPayableId>" & TempAccountPayableId & "</TempAccountPayableId>")
                    builder.Append("<Id>" & AccountPayableCommitment.Id & "</Id>")
                    builder.Append("<AccountPayableId>" & AccountPayableCommitment.AccountPayableId & "</AccountPayableId>")
                    builder.Append("<CommitmentDetailId>" & AccountPayableCommitment.CommitmentDetailId & "</CommitmentDetailId>")
                    builder.Append("<Value>" & AccountPayableCommitment.Value & "</Value>")
                    builder.Append("<IsDelete>" & 1 & "</IsDelete>")

                    builder.Append("</AccountPayableCommitments>")
                Next
            End If

            builder.Append("</AccountPayable>")

            TempAccountPayableId += 1
        Next

        Return builder.ToString()
    End Function

    ''' <summary>
    ''' Guarda las facturas de pagos asociados a la misma cuenta por pagar
    ''' </summary>
    ''' <param name="listAccountPayable"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveListAccountPayableWithReturn(listAccountPayable As List(Of AccountPayable), listDeferredCausation As List(Of DeferredCausation), modeSaveAndConfirm As Boolean, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of List(Of AccountPayable)) Implements IAccountPayableAdminService.SaveListAccountPayableWithReturn
        If listAccountPayable Is Nothing Then
            Throw New ArgumentNullException("listAccountPayable")
        End If

        Dim UnitOfWork As IUnitWork = _accountPayableRepository.UnitWork
        Dim companySettingsUnitOfWork As IUnitWork = Me._companySettingsRepository.UnitWork
        Dim deferredCausationUnitOfWork As IUnitWork = Me._deferredCausationRepository.UnitWork
        Dim code As String = ""
        Dim listCodes As New List(Of String)
        Dim result As ActionResult(Of AccountPayable) = Nothing
        Dim listError As New StringBuilder()

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                ''Validacion para actualizar iva descontable en caso de que el parámetro haya sido modificado
                Dim message As String = String.Empty
                Dim _companySettings = _companySettingsRepository.GetCompanySettings(True)
                If _companySettings IsNot Nothing Then
                    Dim taxValidation As Boolean
                    Dim parameter As Boolean = False
                    Select Case _companySettings.TaxRegistration
                        Case 1, 4
                            parameter = False
                            taxValidation = (From l In listAccountPayable Where l.DeductibleIva.HasValue AndAlso l.DeductibleIva).Any()
                        Case 2
                            parameter = True
                            taxValidation = (From l In listAccountPayable Where l.DeductibleIva.HasValue AndAlso Not l.DeductibleIva).Any()
                    End Select
                    If taxValidation Then
                        message += "Debido a una diferencia en el parámetro de Registro IVA, el iva descontable ha sido modificado"
                        Parallel.ForEach(listAccountPayable.FindAll(Function(s) s.ChangeTracker.State <> ObjectState.Deleted), Sub(x)
                                                                                                                                   x.DeductibleIva = parameter
                                                                                                                               End Sub)
                    End If
                End If

                Dim xml = ConvertToXml(listAccountPayable, listDeferredCausation)
                Dim resultStore = _accountPayableRepository.SP_SaveAccountsPayable(xml, audit.CodeUser)
                If resultStore.CodeResult = 999 Then
                    transaction.Dispose()
                    Return New ActionResult(Of List(Of AccountPayable)) With {.StateResult = False, .MessageResult = {resultStore.MessageResult}.ToList}
                End If

                listAccountPayable.ForEach(Sub(item)
                                               If item.Id = 0 Then
                                                   item.Id = resultStore.AccountPayableId
                                               End If
                                               item.NumberFiling = resultStore.ConsecutiveFiling
                                               item.Code = resultStore.AccountPayableCode
                                           End Sub)

                listCodes.Add(resultStore.AccountPayableCode)


                Dim resultConfirm As ActionResult(Of List(Of Tuple(Of String, Integer))) = Nothing
                Dim ResultGEDS As ActionResult(Of List(Of Tuple(Of String, String))) = Nothing
                Dim MessageGEDS As String = String.Empty
                'Confirmo segun corresponda
                If modeSaveAndConfirm Then

                    resultConfirm = ConfirmAccountPayable(listAccountPayable, audit)
                    If resultConfirm.StateResult = False Then
                        UnitOfWork.RollbackChanges()
                        companySettingsUnitOfWork.RollbackChanges()
                        deferredCausationUnitOfWork.RollbackChanges()
                        transaction.Dispose()
                        Return New ActionResult(Of List(Of AccountPayable)) With {.StateResult = False, .MessageResult = {resultConfirm.Message}.ToList, .Message = message}
                    End If

                    If listAccountPayable.Any(Function(x) x.HandlesDocumentSupport = True) Then
                        'genero y confirmo el documento soporte electronico
                        ResultGEDS = GenerateElectronicSupportDocument(listAccountPayable.Where(Function(y) y.HandlesDocumentSupport = True).ToList(), audit)
                        If ResultGEDS.StateResult = False Then
                            UnitOfWork.RollbackChanges()
                            companySettingsUnitOfWork.RollbackChanges()
                            deferredCausationUnitOfWork.RollbackChanges()
                            transaction.Dispose()
                            Return New ActionResult(Of List(Of AccountPayable)) With {.StateResult = False, .MessageResult = ResultGEDS.ObjectEmbbeded.Select(Function(x) x.Item2).ToList()}
                        End If

                        If ResultGEDS IsNot Nothing Then
                            For Each Itemx In ResultGEDS.ObjectEmbbeded.ToList()
                                listCodes.Add(Itemx.Item2)
                            Next
                        End If
                        MessageGEDS = $"Documento Soporte Electronico: {String.Join(" ; ", ResultGEDS.ObjectEmbbeded.Select(Function(x) x.Item2).ToList())}"

                        Dim resultPublish = PublishElectronicSupporDocumentMessages(listAccountPayable.Where(Function(y) y.HandlesDocumentSupport = True).ToList(), audit)
                        If resultPublish.StateResult = False Then
                            UnitOfWork.RollbackChanges()
                            companySettingsUnitOfWork.RollbackChanges()
                            deferredCausationUnitOfWork.RollbackChanges()
                            transaction.Dispose()
                            Return New ActionResult(Of List(Of AccountPayable)) With {.StateResult = False, .MessageResult = resultPublish.MessageResult}
                        End If
                    End If

                    For i = 0 To resultConfirm.ObjectEmbbeded.Count - 1
                        listCodes.Add(resultConfirm.ObjectEmbbeded.Item(i).Item1)
                    Next

                    message = String.Format("No. Radicado: {0}", listAccountPayable.FirstOrDefault().NumberFiling)
                    message = message + vbCrLf + resultConfirm.Message + vbCrLf + MessageGEDS
                Else
                    message = "Consecutivo CxP: " + listAccountPayable.FirstOrDefault().Code
                    message = message + vbCrLf + "No. Radicado: " + listAccountPayable.FirstOrDefault().NumberFiling.ToString
                End If

                transaction.Complete()
                Return New ActionResult(Of List(Of AccountPayable)) With {.StateResult = True, .ObjectEmbbeded = listAccountPayable, .MessageResult = listCodes, .Message = message}
            Catch ex As Exception
                UnitOfWork.RollbackChanges()
                companySettingsUnitOfWork.RollbackChanges()
                deferredCausationUnitOfWork.RollbackChanges()
                transaction.Dispose()

                Dim message = ex.Message
                If (ex.InnerException IsNot Nothing AndAlso ex.InnerException.Message IsNot Nothing) Then
                    message = ex.InnerException.Message
                    If (ex.InnerException.InnerException IsNot Nothing AndAlso ex.InnerException.InnerException.Message IsNot Nothing) Then
                        message = ex.InnerException.InnerException.Message
                    End If
                End If

                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of List(Of AccountPayable)) With {.StateResult = False, .MessageResult = {message}.ToList}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Guarda las facturas de pagos
    ''' </summary>
    ''' <param name="listAccountPayable"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveListAccountPayable(listAccountPayable As List(Of AccountPayable), listDeferredCausation As List(Of DeferredCausation), modeSaveAndConfirm As Boolean, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of List(Of String)) Implements IAccountPayableAdminService.SaveListAccountPayable
        Dim result = Me.SaveListAccountPayableWithReturn(listAccountPayable, listDeferredCausation, modeSaveAndConfirm, audit, idSequense)
        Dim actionResult = New ActionResult(Of List(Of String)) With {.StateResult = result.StateResult, .MessageResult = result.MessageResult}

        If result.StateResult Then
            actionResult.ObjectEmbbeded = result.MessageResult
            actionResult.Message = result.ObjectEmbbeded(0).NumberFiling
            actionResult.MessageResult = {result.Message}.ToList()
        End If

        Return actionResult
    End Function

    ''' <summary>
    ''' Guarda o actualiza una dependencia
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveAccountPayable(accountPayable As AccountPayable, audit As AuditMessage, Optional idSequense As Long = 0, Optional ByVal withCommit As Boolean = True) As ActionResult(Of AccountPayable) Implements IAccountPayableAdminService.SaveAccountPayable
        If accountPayable Is Nothing Then
            Throw New ArgumentNullException("accountPayable")
        End If
        Dim unitOfWork As IUnitWork = Me._accountPayableRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using Transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim seq As PaymentsSecuenceDetail = Nothing
                If accountPayable.Code Is Nothing OrElse accountPayable.Code.Trim().Equals(String.Empty) Then
                    seq = Me._secuenseDRepository.GetSequenseDByIdSequencePaymentsC(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PaymentsSecuence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            accountPayable.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                            sequenseUnitOfWork.Commit()
                        Else
                            Return New ActionResult(Of AccountPayable) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                        End If
                    Else
                        Return New ActionResult(Of AccountPayable) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                    End If
                End If

                Dim auxAccountPayable As AccountPayable = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of AccountPayable)
                Dim status As Integer

                If accountPayable.ChangeTracker.State = ObjectState.Added Then
                    accountPayable.CreationUser = audit.CodeUser
                    accountPayable.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                ElseIf accountPayable.ChangeTracker.State = ObjectState.Modified Then
                    auxAccountPayable = accountPayable.OriginalValue
                    If accountPayable.Status = 1 Then
                        accountPayable.ModificationUser = audit.CodeUser
                        accountPayable.ModificationDate = DateTime.Now
                        status = Infrastructure.CrossCutting.Audit.Actions.Update
                    End If
                    If accountPayable.Status = 2 Then
                        accountPayable.ModificationUser = audit.CodeUser
                        accountPayable.ModificationDate = DateTime.Now
                        accountPayable.ConfirmationUser = audit.CodeUser
                        accountPayable.ConfirmationDate = DateTime.Now
                        status = Infrastructure.CrossCutting.Audit.Actions.Confirm
                    End If
                    If accountPayable.Status = 3 Then
                        accountPayable.ModificationUser = audit.CodeUser
                        accountPayable.ModificationDate = DateTime.Now
                        accountPayable.AnnulmentUser = audit.CodeUser
                        accountPayable.AnnulmentDate = DateTime.Now
                        status = Infrastructure.CrossCutting.Audit.Actions.Annular
                    End If
                End If

                If accountPayable.Status = 1 Then
                    If accountPayable.ChangeTracker.State = ObjectState.Added Then
                        Dim pc = _paymentControlAdminService.GetPaymentControlByDocumentNumber(accountPayable.Code, 1)
                        If pc.Id = 0 Then
                            Dim paymentControl As New PaymentsControl
                            paymentControl.DocumentNumber = accountPayable.Code
                            paymentControl.DocumentType = 1
                            paymentControl.DocumentUser = audit.CodeUser
                            paymentControl.DocumentDate = accountPayable.DocumentDate
                            Dim resultSaveControl = _paymentControlAdminService.SavePaymentControl(paymentControl, audit)
                            If resultSaveControl.StateResult = False Then
                                unitOfWork.RollbackChanges()
                                Transaction.Dispose()
                                Return New ActionResult(Of AccountPayable) With {.StateResult = False, .Message = ResourceManager.GetString("SavePaymentControlError", "Payments")}
                            End If
                        End If
                    End If
                ElseIf (accountPayable.Status = 2 AndAlso accountPayable.Id > 0) OrElse accountPayable.Status = 3 Then
                    Dim paymentControl = _paymentControlAdminService.GetPaymentControlByDocumentNumber(accountPayable.Code, 1)
                    If paymentControl.Id > 0 Then
                        paymentControl.MarkAsDeleted()
                        Dim resultSaveControl = _paymentControlAdminService.DeletePaymentControl(paymentControl, audit)
                        If resultSaveControl.StateResult = False Then
                            unitOfWork.RollbackChanges()
                            Transaction.Dispose()
                            Return New ActionResult(Of AccountPayable) With {.StateResult = False, .Message = ResourceManager.GetString("DeletePaymentControlError", "Payments")}
                        End If
                    End If
                End If

                'Se valida que si la cxp tiene asociado un id de distribución de elementos del costo está distribución no esté en otra cxp
                If (accountPayable.Status = 1 OrElse accountPayable.Status = 2) AndAlso accountPayable.CostDistributionDirectCostId IsNot Nothing AndAlso accountPayable.CostDistributionDirectCostId > 0 Then
                    'Se consulta la cxp que tenga asociada la distribución y que no esté anulada
                    Dim apTemp = _accountPayableRepository.ValidateAccountPayableByCostDistributionDirectCostId(accountPayable.CostDistributionDirectCostId, accountPayable.Id)
                    If apTemp IsNot Nothing Then
                        unitOfWork.RollbackChanges()
                        Transaction.Dispose()
                        Return New ActionResult(Of AccountPayable) With {.StateResult = False, .MessageResult = {"No se puede guardar porque la distribución de elementos del costo asociado ya está asignado a la CxP " + apTemp.Code + " con No. factura " + apTemp.BillNumber}.ToList()}
                    End If
                End If

                Me._accountPayableRepository.SaveEntity(accountPayable)
                If withCommit Then
                    unitOfWork.Commit()
                    'sequenseUnitOfWork.Commit()
                End If
                auditProcess = New IndigoAuditSimpleEntity(Of AccountPayable)(accountPayable, audit, status, auxAccountPayable)
                auditProcess.Execute()

                Transaction.Complete()
                Return New ActionResult(Of AccountPayable) With {.StateResult = True, .ObjectEmbbeded = accountPayable}
            Catch ex As DbUpdateException
                unitOfWork.RollbackChanges()
                Transaction.Dispose()
                Return New ActionResult(Of AccountPayable) With {.StateResult = False, .MessageResult = {DirectCast(ex, System.Data.Entity.Infrastructure.DbUpdateException).InnerException.ToString()}.ToList()}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                Transaction.Dispose()
                Return New ActionResult(Of AccountPayable) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
            Catch ex As InvalidOperationException
                unitOfWork.RollbackChanges()
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of AccountPayable) With {.StateResult = False}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of AccountPayable) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Elimina una cuenta por pagar
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteAccountPayable(ListAccountPayable As List(Of AccountPayable), audit As AuditMessage) As ActionResult Implements IAccountPayableAdminService.DeleteAccountPayable
        If ListAccountPayable Is Nothing Then
            Throw New ArgumentNullException("ListAccountPayable")
        End If
        Dim unitOfWork As IUnitWork = Me._accountPayableRepository.UnitWork
        Try
            For Each item As AccountPayable In ListAccountPayable
                item.MarkAsUnchanged()
                While item.AccountPayableDetailConcept.Count > 0
                    item.AccountPayableDetailConcept.Item(0).MarkAsDeleted()
                End While
                While item.AccountPayableShares.Count > 0
                    item.AccountPayableShares.Item(0).MarkAsDeleted()
                End While
                Me._accountPayableRepository.SaveEntity(item)

                item.MarkAsDeleted()
                Me._accountPayableRepository.DeleteEntity(item)
                unitOfWork.Commit()
                'Auditoria básica
                IndigoAuditBasic.Execute(GetType(AccountPayable).Name, audit.Functional, item.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, Infrastructure.CrossCutting.Base.ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
                'Ausitoria avanzada
                Dim auditObject As New IndigoAuditSimpleEntity(Of AccountPayable)(item, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
                auditObject.Execute()
            Next
            Return New ActionResult With {.StateResult = True}
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
    ''' Obtiene una determinada cuenta por pagar
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountPayable(code As String, audit As AuditMessage) As AccountPayable Implements IAccountPayableAdminService.GetAccountPayable
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim accountPayable As AccountPayable = Me._accountPayableRepository.GetAccountPayable(code.Trim())
            Return accountPayable
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
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
    Public Function ChangeState(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of AccountPayable) Implements IAccountPayableAdminService.ChangeState
        Dim accountPayable As AccountPayable = _accountPayableRepository.GetAccountPayable(code)
        accountPayable.Status = state
        Return SaveAccountPayable(accountPayable, audit)
    End Function

    ''' <summary>
    ''' Centro de costo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCostCenterById(id As Integer, audit As AuditMessage) As CostCenter Implements IAccountPayableAdminService.GetCostCenterById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim costCenter As CostCenter = Me._accountPayableRepository.GetCostCenterById(id)
            If costCenter IsNot Nothing AndAlso costCenter.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of CostCenter)(costCenter, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return costCenter
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el listado de facturas por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountPayableByCode(code As String, audit As AuditMessage) As List(Of AccountPayable) Implements IAccountPayableAdminService.GetAccountPayableByCode
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim accountPayable As List(Of AccountPayable) = Me._accountPayableRepository.GetAccountPayableByCode(code.Trim())
            Return accountPayable
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una lista de cuentas por pagar que tenga la unidad de radicacion
    ''' </summary>
    ''' <param name="FilingUnitId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListAccountPayableByFilingUnitId(FilingUnitId As Integer) As ActionResult(Of List(Of AccountPayable)) Implements IAccountPayableAdminService.GetListAccountPayableByFilingUnitId
        If FilingUnitId = 0 Then
            Throw New ArgumentNullException("FilingUnitId")
        End If
        Try
            Dim ListAccountPayable As List(Of AccountPayable) = Me._accountPayableRepository.GetListAccountPayableByFilingUnitId(FilingUnitId)
            Return New ActionResult(Of List(Of AccountPayable)) With {.StateResult = True, .ObjectEmbbeded = ListAccountPayable}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of AccountPayable)) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el listado de facturas por id proveedor
    ''' </summary>
    ''' <param name="idSupplier"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountPayableByIdSupplier(idSupplier As Integer, audit As AuditMessage) As List(Of AccountPayable) Implements IAccountPayableAdminService.GetAccountPayableByIdSupplier
        If idSupplier = 0 Then
            Throw New ArgumentNullException("idSupplier")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim accountPayable As List(Of AccountPayable) = Me._accountPayableRepository.GetAccountPayableByIdSupplier(idSupplier, False)
            If accountPayable IsNot Nothing Then
                'IndigoAuditSimpleEntity(Of List(Of AccountPayable)).Execute(accountPayable, audit, Infrastructure.CrossCutting.Audit.Actions.Print, audit.Company)
            End If
            Return accountPayable
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una factura por id del tercero, id de la cuenta y el estado
    ''' </summary>
    ''' <param name="IdThird">The identifier third.</param>
    ''' <param name="state">The state.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function GetAccountPayableByIdThirdIdAccountAndState(IdThird As Integer, IdAccount As Integer, state As Short, audit As AuditMessage) As List(Of AccountPayable) Implements IAccountPayableAdminService.GetAccountPayableByIdThirdIdAccountAndState
        If IdThird = 0 Then
            Throw New ArgumentNullException("IdThird")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim accountPayable As List(Of AccountPayable) = Me._accountPayableRepository.GetAccountPayableByIdThirdIdAccountAndState(IdThird, IdAccount, state)
            If accountPayable IsNot Nothing Then
                'For Each ac As AccountPayable In accountPayable
                '    IndigoAuditSimpleEntity(Of AccountPayable).Execute(ac, audit, Infrastructure.CrossCutting.Audit.Actions.Print, audit.Company)
                'Next
            End If
            Return accountPayable
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' obtiene las cuotas de una factura por el id de la factura, id del tercero y el estado
    ''' </summary>
    Public Function GetAccountPayableSharesByAccountPayableIdThirdIdAccountAndState(IdThird As Integer, IdAccount As Integer, state As Short, audit As AuditMessage) As List(Of AccountPayableShares) Implements IAccountPayableAdminService.GetAccountPayableSharesByAccountPayableIdThirdIdAccountAndState
        If IdAccount = 0 Then
            Throw New ArgumentNullException("IdAccount")
        End If
        If IdThird = 0 Then
            Throw New ArgumentNullException("IdThird")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim accountPayable As List(Of AccountPayableShares) = Me._accountPayableRepository.GetAccountPayableSharesByAccountPayableIdThirdIdAccountAndState(IdThird, IdAccount, state)
            If accountPayable IsNot Nothing Then
                'For Each ac As AccountPayableShares In accountPayable
                '    IndigoAuditSimpleEntity(Of AccountPayableShares).Execute(ac, audit, Infrastructure.CrossCutting.Audit.Actions.Print, audit.Company)
                'Next
            End If
            Return accountPayable
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una cuenta por pagar por el numero de factura
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountPayableByBillNumber(code As String, idSupplier As Integer) As AccountPayable Implements IAccountPayableAdminService.GetAccountPayableByBillNumber
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        Try
            Dim accountPayable As AccountPayable = Me._accountPayableRepository.GetAccountPayableByBillNumber(code.Trim(), idSupplier)
            Return accountPayable
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function GetAccountPayableById(Id As Integer, audit As AuditMessage, Optional tracking As Boolean = True) As AccountPayable Implements IAccountPayableAdminService.GetAccountPayableById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim accountPayable As AccountPayable = Me._accountPayableRepository.GetAccountPayableById(Id, False)
            If accountPayable IsNot Nothing AndAlso accountPayable.Id > 0 Then
                'IndigoAuditSimpleEntity(Of AccountPayable).Execute(accountPayable, audit, Infrastructure.CrossCutting.Audit.Actions.Print, audit.Company)
            End If
            Return accountPayable
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la cxp por id para las notas
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="audit"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountPayableByIdForNotes(Id As Integer, audit As AuditMessage, Optional tracking As Boolean = True) As AccountPayable Implements IAccountPayableAdminService.GetAccountPayableByIdForNotes
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim accountPayable As AccountPayable = Me._accountPayableRepository.GetAccountPayableByIdForNotes(Id)
            If accountPayable IsNot Nothing AndAlso accountPayable.Id > 0 Then
                'IndigoAuditSimpleEntity(Of AccountPayable).Execute(accountPayable, audit, Infrastructure.CrossCutting.Audit.Actions.Print, audit.Company)
            End If
            Return accountPayable
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene las facturas que tiene el proveedor y que esten confirmadas
    ''' </summary>
    ''' <param name="idSupplier"></param>
    ''' <param name="status"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountPayableByIdSupplierAndState(idSupplier As Integer, status As Integer, audit As AuditMessage) As List(Of AccountPayable) Implements IAccountPayableAdminService.GetAccountPayableByIdSupplierAndState
        If idSupplier = 0 Then
            Throw New ArgumentNullException("idSupplier")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim accountPayable As List(Of AccountPayable) = Me._accountPayableRepository.GetAccountPayableByIdSupplierAndState(idSupplier, status)
            If accountPayable IsNot Nothing Then
                'IndigoAuditSimpleEntity(Of List(Of AccountPayable)).Execute(accountPayable, audit, Infrastructure.CrossCutting.Audit.Actions.Print, audit.Company)
            End If
            Return accountPayable
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una cuota de factura por el id
    ''' </summary>
    Public Function GetAccountPayableShareById(Id As Integer, audit As AuditMessage) As AccountPayableShares Implements IAccountPayableAdminService.GetAccountPayableShareById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim accountPayableShare As AccountPayableShares = Me._accountPayableRepository.GetAccountPayableShareById(Id)
            If accountPayableShare IsNot Nothing AndAlso accountPayableShare.Id > 0 Then
                'IndigoAuditSimpleEntity(Of AccountPayableShares).Execute(accountPayableShare, audit, Infrastructure.CrossCutting.Audit.Actions.Print, audit.Company)
            End If
            Return accountPayableShare
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la cantidad de cuentas asociadas a un tercero cuenta y estado
    ''' </summary>
    Public Function GetCountAccountPayableShareByAccountPayIdThirdIdAccountAndState(IdThird As Integer, IdAccount As Integer, state As Short, audit As AuditMessage) As Integer Implements IAccountPayableAdminService.GetCountAccountPayableShareByAccountPayIdThirdIdAccountAndState
        If IdAccount = 0 Then
            Throw New ArgumentNullException("IdAccount")
        End If
        If IdThird = 0 Then
            Throw New ArgumentNullException("IdThird")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim accountPayable As Integer = Me._accountPayableRepository.GetCountAccountPayableShareByAccountPayIdThirdIdAccountAndState(IdThird, IdAccount, state)
            Return accountPayable
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Consulta las cuotas de la factura
    ''' </summary>
    ''' <param name="idAccountPayable"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountPayableSharesByIdAccountPayable(idAccountPayable As Integer, audit As AuditMessage) As List(Of AccountPayableShares) Implements IAccountPayableAdminService.GetAccountPayableSharesByIdAccountPayable
        If idAccountPayable = 0 Then
            Throw New ArgumentNullException("idAccountPayable")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim accountPayableShares As List(Of AccountPayableShares) = Me._accountPayableRepository.GetAccountPayableSharesByIdAccountPayable(idAccountPayable)
            If accountPayableShares IsNot Nothing Then
                'IndigoAuditSimpleEntity(Of List(Of AccountPayableShares)).Execute(accountPayableShares, audit, Infrastructure.CrossCutting.Audit.Actions.Print, audit.Company)
            End If
            Return accountPayableShares
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Anula la cuenta por pagar
    ''' </summary>
    ''' <param name="ListAccountPayable"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AnnularAccountPayable(ListAccountPayable As List(Of AccountPayable), audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of List(Of AccountPayable)) Implements IAccountPayableAdminService.AnnularAccountPayable
        If ListAccountPayable Is Nothing Then
            Throw New ArgumentNullException("listAccountPayable")
        End If
        Dim result As ActionResult(Of AccountPayable)
        Using Transaction As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

            Dim idsAccountPayable = ListAccountPayable.Select(Function(d) d.Id).ToList()
            Dim queryListAccountPayable = _accountPayableRepository.GetByFilter(Function(x) idsAccountPayable.Contains(x.Id)).ToList()

            For Each item As AccountPayable In queryListAccountPayable
                If item.Status = 2 Then
                    Transaction.Dispose()
                    Return New ActionResult(Of List(Of AccountPayable)) With {
                        .StateResult = False,
                        .MessageResult = {"No se puede anular la CxP ya que esta confirmada, por favor vuelva y cargue el registro."}.ToList()
                    }
                End If

                item.Status = 3
                result = SaveAccountPayable(item, audit, idSequense)
                If result.StateResult = False Then
                    Transaction.Dispose()
                    Return New ActionResult(Of List(Of AccountPayable)) With {
                        .StateResult = False,
                        .MessageResult = {"Error al intentar anular la CxP."}.ToList()
                    }
                End If
            Next

            Transaction.Complete()
            Return New ActionResult(Of List(Of AccountPayable)) With {.StateResult = True, .ObjectEmbbeded = ListAccountPayable}
        End Using
    End Function

    ''' <summary>
    ''' Obtiene la validacion que si la cxp maneja causacion diferida
    ''' </summary>
    ''' <param name="accountPayableId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetValidationDeferredCausation(accountPayableId As Integer) As ActionResult(Of AccountPayable) Implements IAccountPayableAdminService.GetValidationDeferredCausation
        If accountPayableId = 0 Then
            Throw New ArgumentNullException("accountPayableId")
        End If
        Try
            Dim banAccountPayable As Boolean = _accountPayableRepository.GetValidationDeferredCausation(accountPayableId)
            If banAccountPayable Then
                Return New ActionResult(Of AccountPayable) With {.StateResult = True}
            Else
                Return New ActionResult(Of AccountPayable) With {.StateResult = False}
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Consulta si hay registros de cxp
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCheckExistAccountPayable() As ActionResult(Of AccountPayable) Implements IAccountPayableAdminService.GetCheckExistAccountPayable
        Try
            Dim banList As Boolean = _accountPayableRepository.GetCheckExistAccountPayable()
            If banList Then
                Return New ActionResult(Of AccountPayable) With {.StateResult = True}
            Else
                Return New ActionResult(Of AccountPayable) With {.StateResult = False}
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el registro de mayor valor de consecutivo de radicacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUltimateRegisterConsecutiveFiling() As ActionResult(Of AccountPayable) Implements IAccountPayableAdminService.GetUltimateRegisterConsecutiveFiling
        Try
            Dim accountPayable As AccountPayable = _accountPayableRepository.GetUltimateRegisterConsecutiveFiling()
            If accountPayable IsNot Nothing Then
                Return New ActionResult(Of AccountPayable) With {.StateResult = True, .ObjectEmbbeded = accountPayable}
            Else
                Return New ActionResult(Of AccountPayable) With {.StateResult = False}
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una cuenta por pagar por el numero de factura
    ''' </summary>
    ''' <param name="BillNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountsPayableByBillNumber(BillNumber As String) As AccountPayable Implements IAccountPayableAdminService.GetAccountsPayableByBillNumber
        If String.IsNullOrEmpty(BillNumber) Then
            Throw New ArgumentNullException("BillNumber")
        End If
        Try
            Dim accountPayable As AccountPayable = Me._accountPayableRepository.GetAccountsPayableByBillNumber(BillNumber.Trim())
            Return accountPayable
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function GetAccountPayableByBillNumberAndMainAccount(code As String, idSupplier As Integer, mainAccountId As Integer) As AccountPayable Implements IAccountPayableAdminService.GetAccountPayableByBillNumberAndMainAccount
        Try
            Return Me._accountPayableRepository.GetAccountPayableByBillNumberAndMainAccount(code.Trim(), idSupplier, mainAccountId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New AccountPayable
        End Try
    End Function

    ''' <summary>
    ''' Obtiene todas las lineas de distribución de un proveedor
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllDistributionLines() As List(Of Domain.Entities.SuppliersDistributionLines) Implements IAccountPayableAdminService.GetAllDistributionLines
        Try
            Dim distributionLines As List(Of Domain.Entities.SuppliersDistributionLines) = Me._accountPayableRepository.GetAllDistributionLines
            Return distributionLines
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Private Function ConvertToXmlListAccountPayable(listAccountPayable As List(Of AccountPayable)) As String
        Dim builder As StringBuilder = New StringBuilder()

        builder.Append("<ListAccountPayable>")

        Dim rowAccountPayable As Integer = 1

        For Each accountPayable In listAccountPayable
            builder.Append("<AccountPayable>")

            builder.Append("<TempId>" & rowAccountPayable & "</TempId>")
            builder.Append("<Code>" & accountPayable.Code & "</Code>")
            If accountPayable.JournalVoucherId IsNot Nothing Then
                'Si esta llena es porque viene desde el form de comprobante de entrada
                builder.Append("<JournalVoucherTypeId>" & accountPayable.JournalVoucherId & "</JournalVoucherTypeId>")
            End If
            If Not String.IsNullOrEmpty(accountPayable.EntityName) AndAlso accountPayable.EntityName <> GetType(AccountPayable).Name Then
                builder.Append("<OriginEntityName>" & accountPayable.EntityName & "</OriginEntityName>")
            End If

            builder.Append("<ListAccountPayableDetailConceptNotHomologated>")

            For Each otherNotHomologatedBook In accountPayable.AccountPayableDetailConceptOthersNotHomologatedBooks
                For Each detail In otherNotHomologatedBook.Value
                    builder.Append("<AccountPayableDetailConceptNotHomologated>")

                    builder.Append("<ParentId>" & rowAccountPayable & "</ParentId>")
                    builder.Append("<LegalBookId>" & otherNotHomologatedBook.Key & "</LegalBookId>")
                    builder.Append("<IdMainAccount>" & detail.IdAccount & "</IdMainAccount>")
                    builder.Append("<IdThirdParty>" & detail.IdThirdParty & "</IdThirdParty>")
                    If detail.IdCostCenter IsNot Nothing Then
                        builder.Append("<IdCostCenter>" & detail.IdCostCenter & "</IdCostCenter>")
                    End If
                    If detail.Nature = 1 Then
                        builder.Append("<DebitValue>" & detail.Value.ToString().Replace(",", ".") & "</DebitValue>")
                        builder.Append("<CreditValue>" & 0 & "</CreditValue>")
                    Else
                        builder.Append("<DebitValue>" & 0 & "</DebitValue>")
                        builder.Append("<CreditValue>" & detail.Value.ToString().Replace(",", ".") & "</CreditValue>")
                    End If
                    If detail.Detail IsNot Nothing Then
                        builder.Append("<Detail>" & detail.Detail & "</Detail>")
                    End If
                    If detail.IdRetentionConcept IsNot Nothing Then
                        builder.Append("<IdRetention>" & detail.IdRetentionConcept & "</IdRetention>")
                    End If
                    If detail.Percentage IsNot Nothing Then
                        builder.Append("<RetentionRate>" & detail.Percentage.ToString().Replace(",", ".") & "</RetentionRate>")
                    End If
                    builder.Append("<BaseValue>" & detail.BaseValue.ToString().Replace(",", ".") & "</BaseValue>")
                    If (detail.BillingValue = 0) Then
                        builder.Append("<BillingValue>" & accountPayable.InvoiceValue.ToString().Replace(",", ".") & "</BillingValue>")
                    Else
                        builder.Append("<BillingValue>" & detail.BillingValue.ToString().Replace(",", ".") & "</BillingValue>")
                    End If

                    builder.Append("</AccountPayableDetailConceptNotHomologated>")
                Next
            Next

            builder.Append("</ListAccountPayableDetailConceptNotHomologated>")

            builder.Append("</AccountPayable>")

            rowAccountPayable += 1
        Next

        builder.Append("</ListAccountPayable>")

        Return builder.ToString()
    End Function

    ''' <summary>
    ''' funcion para crear el xml para enviar al sp de generacion de documento soporte electronico
    ''' </summary>
    ''' <param name="listAccountPayable"></param>
    ''' <param name="Audit"></param>
    ''' <returns></returns>
    Private Function ConvertToXmlListAccountPayableIds(listAccountPayable As List(Of AccountPayable), Audit As AuditMessage) As String
        Dim builder As StringBuilder = New StringBuilder()
        builder.AppendLine("<Data>")
        builder.AppendLine("<CodeUser>" & Audit.CodeUser & "</CodeUser>")
        builder.AppendLine("<CompanyNit>" & listAccountPayable.FirstOrDefault().IndigoCompanyNit & "</CompanyNit>")
        For Each accountPayable In listAccountPayable
            builder.AppendLine("<AccountPayableIds>")
            builder.AppendLine("<Id>" & accountPayable.Id & "</Id>")
            builder.AppendLine("</AccountPayableIds>")
        Next
        builder.AppendLine("</Data>")
        Return builder.ToString()
    End Function

    Private Function ConvertBillsToXml(data As List(Of List(Of String)))
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<Data>")

        'Variable para saber si se obtiene valor de la colección
        Dim ColumnsQuantity As Integer = 0

        Dim position As Integer = 0
        For Each item In data
            position = position + 1

            'Se obtiene la cantidad de columnas que tiene cada registro
            ColumnsQuantity = item.Count

            'Se agrega validación por cantidad de columnas(5, 6) ya que se realiza cambio para que se pueda copiar y pegar solo con los campos de la causación
            If (item.Count <> 5 AndAlso item.Count <> 6 AndAlso item.Count < 12) OrElse item.Count > 14 Then
                builder.Append("<Row>")

                builder.Append("<StatusField>" & 0 & "</StatusField>")
                builder.Append("<MessageField>" & String.Format("El registro {0} no tiene la estructura requerida", position) & "</MessageField>")

                builder.Append("</Row>")
                Continue For
            End If

            builder.Append("<Row>")

            builder.Append("<CountFields>" & item.Count & "</CountFields>")
            builder.Append("<StatusField>" & 1 & "</StatusField>")
            builder.Append("<MessageField>" & "Ok" & "</MessageField>")

            'Se valida si la cantidad de columnas es mayor a cero para poder obtener el valor sino se envia vacio y se va restando la cantidad de columnas por cada valor asignado
            '----------------------- AccountPayable -----------------------
            builder.Append("<DeductibleIva>" & If(ColumnsQuantity > 0, item(0), "") & "</DeductibleIva>")
            ColumnsQuantity -= 1

            builder.Append("<BillNumber>" & If(ColumnsQuantity > 0, item(1), "") & "</BillNumber>")
            ColumnsQuantity -= 1

            builder.Append("<BillDate>" & If(ColumnsQuantity > 0, CDate(item(2)).ToString("dd/MM/yyyy"), "") & "</BillDate>")
            ColumnsQuantity -= 1

            builder.Append("<Currency>" & If(ColumnsQuantity > 0, item(3), "") & "</Currency>")
            ColumnsQuantity -= 1

            builder.Append("<InvoiceValue>" & If(ColumnsQuantity > 0, item(4).ToString().Replace(",", "."), "") & "</InvoiceValue>")
            ColumnsQuantity -= 1

            builder.Append("<Coments>" & If(ColumnsQuantity > 0, item(5), "") & "</Coments>")
            ColumnsQuantity -= 1


            '----------------------- AccountPayableDetailConcept -----------------------
            builder.Append("<AccountPayableConceptCode>" & If(ColumnsQuantity > 0, item(6), "") & "</AccountPayableConceptCode>")
            ColumnsQuantity -= 1

            builder.Append("<ThirdPartyNit>" & If(ColumnsQuantity > 0, item(7), "") & "</ThirdPartyNit>")
            ColumnsQuantity -= 1

            builder.Append("<CostCenterCode>" & If(ColumnsQuantity > 0, item(8), "") & "</CostCenterCode>")
            ColumnsQuantity -= 1

            builder.Append("<Detail>" & If(ColumnsQuantity > 0, item(9), "") & "</Detail>")
            ColumnsQuantity -= 1

            builder.Append("<Nature>" & If(ColumnsQuantity > 0, item(10), "") & "</Nature>")
            ColumnsQuantity -= 1

            builder.Append("<BaseValue>" & If(ColumnsQuantity > 0, item(11).ToString().Replace(",", "."), "") & "</BaseValue>")
            ColumnsQuantity -= 1

            builder.Append("<RateIva>" & If(ColumnsQuantity > 0, item(12), "") & "</RateIva>")
            ColumnsQuantity -= 1

            builder.Append("<RetentionConceptCode>" & If(ColumnsQuantity > 0, item(13), "") & "</RetentionConceptCode>")
            ColumnsQuantity -= 1

            builder.Append("</Row>")
        Next

        builder.Append("</Data>")
        Return builder.ToString
    End Function

    Private Function ConvertBillsToXmlImport(data As List(Of ImportFileRow))
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<Data>")

        'Variable para saber si se obtiene valor de la colección
        Dim ColumnsQuantity As Integer = 0


        Dim position As Integer = 0
        For Each item In data
            position = position + 1

            'Se obtiene la cantidad de columnas que tiene cada registro
            ColumnsQuantity = item.Row.Count

            'Se agrega validación por cantidad de columnas(5, 6) ya que se realiza cambio para que se pueda copiar y pegar solo con los campos de la causación
            If (item.Row.Count <> 5 AndAlso item.Row.Count <> 6 AndAlso item.Row.Count < 14) OrElse item.Row.Count > 14 Then
                builder.Append("<Row>")

                builder.Append("<StatusField>" & 0 & "</StatusField>")
                builder.Append("<MessageField>" & String.Format("El registro {0} no tiene la estructura requerida", position) & "</MessageField>")

                builder.Append("</Row>")
                Continue For
            End If

            ''se valida que no vengan rows vacias tomando en cuenta algunas de las celdas obligatorias
            If (item.Row.Item(1) Is Nothing) Then
                Continue For
            End If

            builder.Append("<Row>")

            builder.Append("<CountFields>" & item.Row.Count & "</CountFields>")
            builder.Append("<StatusField>" & 1 & "</StatusField>")
            builder.Append("<MessageField>" & "Ok" & "</MessageField>")

            'Se valida si la cantidad de columnas es mayor a cero para poder obtener el valor sino se envia vacio y se va restando la cantidad de columnas por cada valor asignado
            '----------------------- AccountPayable -----------------------
            builder.Append("<DeductibleIva>" & If(ColumnsQuantity > 0, item.Row.Item(0), "") & "</DeductibleIva>")
            ColumnsQuantity -= 1

            builder.Append("<BillNumber>" & If(ColumnsQuantity > 0, item.Row.Item(1), "") & "</BillNumber>")
            ColumnsQuantity -= 1

            builder.Append("<BillDate>" & If(ColumnsQuantity > 0, CDate(item.Row.Item(2)).ToString("dd/MM/yyyy"), "") & "</BillDate>")
            ColumnsQuantity -= 1

            builder.Append("<Currency>" & If(ColumnsQuantity > 0, item.Row.Item(3), "") & "</Currency>")
            ColumnsQuantity -= 1

            builder.Append("<InvoiceValue>" & If(ColumnsQuantity > 0, item.Row.Item(4).ToString().Replace(",", "."), "") & "</InvoiceValue>")
            ColumnsQuantity -= 1

            builder.Append("<Coments>" & If(ColumnsQuantity > 0, item.Row.Item(5), "") & "</Coments>")
            ColumnsQuantity -= 1


            '----------------------- AccountPayableDetailConcept -----------------------
            builder.Append("<AccountPayableConceptCode>" & If(ColumnsQuantity > 0, item.Row.Item(6), "") & "</AccountPayableConceptCode>")
            ColumnsQuantity -= 1

            builder.Append("<ThirdPartyNit>" & If(ColumnsQuantity > 0, item.Row.Item(7), "") & "</ThirdPartyNit>")
            ColumnsQuantity -= 1

            builder.Append("<CostCenterCode>" & If(ColumnsQuantity > 0, item.Row.Item(8), "") & "</CostCenterCode>")
            ColumnsQuantity -= 1

            builder.Append("<Detail>" & If(ColumnsQuantity > 0, item.Row.Item(9), "") & "</Detail>")
            ColumnsQuantity -= 1

            builder.Append("<Nature>" & If(ColumnsQuantity > 0, item.Row.Item(10), "") & "</Nature>")
            ColumnsQuantity -= 1

            builder.Append("<BaseValue>" & If(ColumnsQuantity > 0, item.Row.Item(11).ToString().Replace(",", "."), "") & "</BaseValue>")
            ColumnsQuantity -= 1

            builder.Append("<RateIva>" & If(ColumnsQuantity > 0, item.Row.Item(12), "") & "</RateIva>")
            ColumnsQuantity -= 1

            builder.Append("<RetentionConceptCode>" & If(ColumnsQuantity > 0, item.Row.Item(13), "") & "</RetentionConceptCode>")
            ColumnsQuantity -= 1

            builder.Append("</Row>")
        Next

        builder.Append("</Data>")
        Return builder.ToString
    End Function

    Private Function ConvertParametersToXml(ParamArray parameters() As Object)
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<Data>")

        builder.Append("<SupplierId>" & parameters.ElementAt(0) & "</SupplierId>")
        builder.Append("<IdAccount>" & parameters.ElementAt(1) & "</IdAccount>")

        builder.Append("</Data>")
        Return builder.ToString
    End Function

    ''' <summary>
    ''' La función GetSupportPaymentSuppliers es una implementación de una interfaz IAccountPayableAdminService y
    ''' esta diseñada para obtener información relacionada con Soporte de pagos proveedores.
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <returns></returns>
    Public Function GetSupportPaymentSuppliers(xml As String) As ActionResult(Of List(Of SP_SupportPaymentSuppliers_Result)) Implements IAccountPayableAdminService.GetSupportPaymentSuppliers
        Try
            If String.IsNullOrEmpty(xml) Then
                Throw New ArgumentNullException("xml")
            End If

            Dim query = _accountPayableRepository.SP_SupportPaymentSuppliers(xml)

            If query Is Nothing OrElse Not query.Any() Then
                Return New ActionResult(Of List(Of SP_SupportPaymentSuppliers_Result)) With {.StateResult = False, .Message = "No se encontraron datos"}
            End If

            Return New ActionResult(Of List(Of SP_SupportPaymentSuppliers_Result)) With {.StateResult = True, .ObjectEmbbeded = query}

        Catch ex As Exception
            Return New ActionResult(Of List(Of SP_SupportPaymentSuppliers_Result)) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function



    ''' <summary>
    ''' La función GetSupportPaymentSuppliers es una implementación de una interfaz IAccountPayableAdminService y
    ''' esta diseñada para obtener información relacionada con Soporte de pagos proveedores.
    ''' </summary>
    ''' <returns></returns>
    Public Function SetCopyPasteOrImportFileDeferredCausation(data As List(Of ImportFileRow), dataCopyPaste As List(Of List(Of String)), valueShare As Decimal) As ActionResult(Of List(Of Domain.Entities.DeferredCausationDetails)) Implements IAccountPayableAdminService.SetCopyPasteOrImportFileDeferredCausation

        If data IsNot Nothing Then
            Return SetImportFileDeferredCausation(data, valueShare)
        End If
        If dataCopyPaste IsNot Nothing Then
            Return setCopyPaste(dataCopyPaste, valueShare)
        End If
    End Function

    Private Function SetImportFileDeferredCausation(data As List(Of ImportFileRow), valueShare As Decimal) As ActionResult(Of List(Of DeferredCausationDetails))
        Dim listErrors As New List(Of String)
        Dim listResult As New List(Of DeferredCausationDetails)

        For Each row In data
            Dim indexRow = row.IndexRow
            Dim mainAccountItem = row.Row.Item(0)
            Dim costcenterItem = row.Row.Item(1)
            Dim value = CDec(row.Row.Item(2))

            'valido los campos
            If row.Row.All(Function(x) String.IsNullOrEmpty(x)) Then
                Continue For
            End If

            If mainAccountItem Is Nothing OrElse mainAccountItem Is String.Empty Then
                listErrors.Add(String.Format("La cuenta contable del item {0} está vacio", (indexRow).ToString()))
                Continue For
            End If

            If Not IsNumeric(value) OrElse value < 0 Then
                listErrors.Add(String.Format("El valor del item {0} no es valido", (indexRow).ToString()))
                Continue For
            End If

            ''valido que el valor no sea mayor al de la cuota si es el primer detalle o si ya van varios
            If listResult.Count = 0 Then
                If value > valueShare Then
                    listErrors.Add(String.Format("El valor del item {0} supera al valor de la cuota", (indexRow).ToString()))
                    Return New ActionResult(Of List(Of DeferredCausationDetails)) With {.StatusCode = eStatusResult.EXCEPTION, .ObjectEmbbeded = listResult, .MessageResult = listErrors}
                End If
            Else
                Dim sumDetails = listResult.Sum(Function(x) x.Value) + value
                If sumDetails > valueShare Then
                    listErrors.Add(String.Format("La sumatoria de los valores de los detalles agregados supera al valor de la cuota", (indexRow).ToString()))
                    Return New ActionResult(Of List(Of DeferredCausationDetails)) With {.StatusCode = eStatusResult.EXCEPTION, .ObjectEmbbeded = listResult, .MessageResult = listErrors}
                End If
            End If


            Dim MainAccount As MainAccounts = Nothing
            ''saco el id de la cuenta contable si no llega nulo
            If mainAccountItem IsNot Nothing OrElse Not mainAccountItem Is String.Empty Then
                MainAccount = _accountReceivableRepository.GetMainAccountByNumber(mainAccountItem.ToString())
            End If

            If MainAccount Is Nothing Then
                listErrors.Add(String.Format("La cuenta contable del item {0} no es valida", (indexRow).ToString()))
                Continue For
            End If

            Dim costCenter As CostCenter = Nothing
            ''saco el id del centro de costo si no llega nulo
            If costcenterItem IsNot Nothing AndAlso Not costcenterItem Is String.Empty Then
                costCenter = _accountPayableRepository.GetCostCenterByCode(costcenterItem.ToString())
            End If


            Dim DeferredCausationDetails As New DeferredCausationDetails()
            With DeferredCausationDetails
                .IdMainAccount = MainAccount?.Id
                .NumberNameMainAccount = mainAccountItem
                If costCenter Is Nothing Then
                    .IdCostCenter = Nothing
                    .DescriptionCostCenter = String.Empty
                Else
                    .IdCostCenter = costCenter?.Id
                    .DescriptionCostCenter = costcenterItem
                End If
                .Nature = 1
                .Value = value
                .DateNextPeriod = DateTime.Now
            End With
            listResult.Add(DeferredCausationDetails)
        Next
        Return New ActionResult(Of List(Of DeferredCausationDetails)) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = listResult, .MessageResult = listErrors}
    End Function

    Private Function setCopyPaste(data As List(Of List(Of String)), valueShare As Decimal) As ActionResult(Of List(Of DeferredCausationDetails))
        Dim listErrors As New List(Of String)
        Dim listResult As New List(Of DeferredCausationDetails)
        If data.Count <= 2 Then
            listErrors.Add("Faltan items por ser llenados. Verifique la información")
            Return New ActionResult(Of List(Of DeferredCausationDetails)) With {.StatusCode = eStatusResult.EXCEPTION, .ObjectEmbbeded = listResult, .MessageResult = listErrors}
        End If
        For i As Integer = 0 To data.Count - 1 Step 1

            Dim mainAccountItem = data.Item(i).Item(0)
            Dim costcenterItem = data.Item(i).Item(1)
            Dim value = CDec(data.Item(i).Item(2))

            If mainAccountItem Is Nothing OrElse mainAccountItem Is String.Empty Then
                listErrors.Add(String.Format("La cuenta contable del item {0} está vacio", (i + 1).ToString()))
                Continue For
            End If

            If Not IsNumeric(value) OrElse value < 0 Then
                listErrors.Add(String.Format("El valor del item {0} no es valido", (i + 1).ToString()))
                Continue For
            End If

            ''valido que el valor no sea mayor al de la cuota si es el primer detalle o si ya van varios
            If listResult.Count = 0 Then
                If value > valueShare Then
                    listErrors.Add(String.Format("El valor del item {0} supera al valor de la cuota", (i + 1).ToString()))
                    Return New ActionResult(Of List(Of DeferredCausationDetails)) With {.StatusCode = eStatusResult.EXCEPTION, .ObjectEmbbeded = listResult, .MessageResult = listErrors}
                End If
            Else
                Dim sumDetails = listResult.Sum(Function(x) x.Value) + value
                If sumDetails > valueShare Then
                    listErrors.Add(String.Format("La sumatoria de los valores de los detalles agregados supera al valor de la cuota", (i + 1).ToString()))
                    Return New ActionResult(Of List(Of DeferredCausationDetails)) With {.StatusCode = eStatusResult.EXCEPTION, .ObjectEmbbeded = listResult, .MessageResult = listErrors}
                End If
            End If


            Dim MainAccount As MainAccounts = Nothing
            ''saco el id de la cuenta contable si no llega nulo
            If mainAccountItem IsNot Nothing OrElse Not mainAccountItem Is String.Empty Then
                MainAccount = _accountReceivableRepository.GetMainAccountByNumber(mainAccountItem.ToString())
            End If

            If MainAccount Is Nothing Then
                listErrors.Add(String.Format("La cuenta contable del item {0} no es valida", (i + 1).ToString()))
                Continue For
            End If

            Dim costCenter As CostCenter = Nothing
            ''saco el id del centro de costo si no llega nulo
            If costcenterItem IsNot Nothing AndAlso Not costcenterItem Is String.Empty Then
                costCenter = _accountPayableRepository.GetCostCenterByCode(costcenterItem.ToString())
            End If


            Dim DeferredCausationDetails As New DeferredCausationDetails()
            With DeferredCausationDetails
                .IdMainAccount = MainAccount?.Id
                .NumberNameMainAccount = data.Item(i).Item(0)
                If costCenter Is Nothing Then
                    .IdCostCenter = Nothing
                    .DescriptionCostCenter = String.Empty
                Else
                    .IdCostCenter = costCenter?.Id
                    .DescriptionCostCenter = costcenterItem
                End If
                .Nature = 1
                .Value = data.Item(i).Item(2)
                .DateNextPeriod = DateTime.Now
            End With
            listResult.Add(DeferredCausationDetails)
        Next
        Return New ActionResult(Of List(Of DeferredCausationDetails)) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = listResult, .MessageResult = listErrors}

    End Function
#End Region

#Region "IDisposable Support"

    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _deferredCausationAdminService.Dispose()
            End If
            _accountPayableRepository = Nothing
            _secuenseDRepository = Nothing
            _settingsPaymentsRepository = Nothing
            _deferredCausationAdminService = Nothing
            _paymentControlAdminService = Nothing
            _deferredCausationRepository = Nothing
            _repositoryCloseMont = Nothing
            _repositoryMainAccounts = Nothing
            _companySettingsRepository = Nothing
            _supplierRepository = Nothing
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