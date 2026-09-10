'***********************************************************************
' Assembly         : Application.Glosas
' Author           : Juan Diego Diaz
' Created          : 12-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Data.Entity.Core
Imports System.Data.SqlClient
Imports System.Text
Imports System.Transactions
Imports Application.Base
Imports Application.Portfolio
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Entities.Service
Imports Domain.InterfaceERPGlosa
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources
#End Region

''' <summary>
''' Servicio de Devolución Cabecera.
''' </summary>
''' <remarks></remarks>
Public Class TransferJuridicalDebtCAdminService
    Implements ITransferJuridicalDebtCAdminService

#Region "Fields"

    Private _JuridicalCRepository As ITransferJuridicalDebtCRepository
    Private _JuridicalDRepository As ITransferJuridicalDebtDRepository
    Private _ConsecutiveRepository As IConsecutiveRepository
    Private _CustomerRepository As ICustomerRepository
    Private _PortFolioRepository As IPortfolioGlosadaRepository
    Private _IInterfaceParametersRepository As IInterfaceParametersRepository
    Private _InterfaceFox As IInterfaceFOX
    Private _InterfaceNet As IInterfaceNET
    Private _InterfacePublicFOX As IInterfacePublicFOX
    Private _MovementGlosaRepository As IMovementGlosaRepository
    ''' <summary>
    ''' Servicio de secuencias numericas
    ''' </summary>
    ''' <remarks></remarks>
    Private _PortfolioSequenseAdminService As IPortfolioSequenseAdminService
    ''' <summary>
    ''' Servicio de interface en modo nativo
    ''' </summary>
    ''' <remarks></remarks>
    Private _InterfaceNativeAdminservice As IInterfaceNativeAdminService
    ''' <summary>
    ''' Repositorio de Cuentas por cobrar
    ''' </summary>
    ''' <remarks></remarks>
    Private _accountReceivableRepository As IAccountReceivableRepository
    ''' <summary>
    ''' repositorio Grupo de Atencion
    ''' </summary>
    ''' <remarks></remarks>
    Private _careGroupRepository As ICareGroupRepository
    ''' <summary>
    ''' Repositorio de Estructrura cuentas por cobrar
    ''' </summary>
    ''' <remarks></remarks>
    Private _AccountReceivableAccountingRepository As IAccountReceivableAccountingRepository
    ''' <summary>
    ''' Repositorio de Parametros de Glosas
    ''' </summary>
    ''' <remarks></remarks>
    Private _ITimeGlossParametersRepository As ITimeParametersRepository

    Private _glosasServices As IGlosasServices
    ''' <summary>
    ''' 
    ''' </summary>
    Private _SettingPortfolioRepository As ISettingPortfolioRepository
    ''' <summary>
    ''' 
    ''' </summary>
    Private _DemandTransferJuridicalRepository As IDemandTransferJuridicalRepository

#End Region

#Region "builds"
    ''' <summary>
    ''' Inicializa una nueva instancia de la clase <see cref="TransferJuridicalDebtCAdminService" />.
    ''' </summary>
    ''' <param name="JuridicalCRepository">El repositorio para el manejo de las cabeceras de transferencia cobro jurídico.</param>
    ''' <param name="JuridicalDRepository">El repositorio para el manejo de los detalles de transferencia cobro jurídico.</param>
    ''' <param name="ConsecutiveRepository">El repositorio para el manejo de los consecutivos.</param>
    ''' <param name="CustomerRepository">El repositorio para el manejo de los terceros.</param>
    ''' <param name="PortFolioRepository">El repositorio para el manejo de la cartera.</param>
    Public Sub New(ByVal JuridicalCRepository As ITransferJuridicalDebtCRepository, ByVal JuridicalDRepository As ITransferJuridicalDebtDRepository, ByVal ConsecutiveRepository As IConsecutiveRepository,
                   ByVal CustomerRepository As ICustomerRepository, ByVal PortFolioRepository As IPortfolioGlosadaRepository, InterfaceFox As IInterfaceFOX, InterfaceNET As IInterfaceNET,
                   IInterfaceParametersRepository As IInterfaceParametersRepository, InterfacePublicFOX As InterfacePublicFOX, ByVal InterfaceNativeAdminservice As IInterfaceNativeAdminService,
                   ByVal PortfolioSequenseAdminService As IPortfolioSequenseAdminService, accountReceivableRepository As IAccountReceivableRepository,
                   careGroupRepository As ICareGroupRepository, AccountReceivableAccountingRepository As IAccountReceivableAccountingRepository,
                    ITimeGlossParametersRepository As ITimeParametersRepository, MovementGlosaRepository As IMovementGlosaRepository, glosasServices As IGlosasServices, settingPortfolioRepository As ISettingPortfolioRepository,
                   demandTransferJuridicalRepository As IDemandTransferJuridicalRepository)
        If JuridicalCRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio Cabecera Transferencia Cobro Jurídico Vacío")
        End If
        If JuridicalDRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio Transferencia Cobro Jurídico Detalle Vacío")
        End If
        If ConsecutiveRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio Consecutivo Vacío")
        End If
        If CustomerRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio Customer Vacío")
        End If
        If PortFolioRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio Cartera Vacío")
        End If
        If PortfolioSequenseAdminService Is Nothing Then
            Throw New ArgumentNullException("PortfolioSequenseAdminService vacio", "Servicio de secuencias vacio")
        End If
        If InterfaceNativeAdminservice Is Nothing Then
            Throw New ArgumentNullException("InterfaceNativeAdminservice vacio", "Repositorio de interface nativo es vacio")
        End If
        If accountReceivableRepository Is Nothing Then
            Throw New ArgumentNullException("accountReceivableRepository vacio", "repositorio de cuentas por cobrar vacio")
        End If
        If careGroupRepository Is Nothing Then
            Throw New ArgumentNullException("careGroupRepository vacio", "repositorio de centro de atencion vacio")
        End If
        If AccountReceivableAccountingRepository Is Nothing Then
            Throw New ArgumentNullException("AccountReceivableAccountingRepository vacio", "Repositorio de estructura de cuenta de cobro vacio")
        End If
        If ITimeGlossParametersRepository Is Nothing Then
            Throw New ArgumentNullException("ITimeGlossParametersRepository vacio")
        End If
        If MovementGlosaRepository Is Nothing Then
            Throw New ArgumentException("_MovementGlosaRepository")
        End If
        If settingPortfolioRepository Is Nothing Then
            Throw New ArgumentException("Repositorio de parámetros de cuentas por cobrar vacio")
        End If
        If demandTransferJuridicalRepository Is Nothing Then
            Throw New ArgumentException("Repositorio de demanda de trasferencia juridica vacio")
        End If

        _JuridicalCRepository = JuridicalCRepository
        _JuridicalDRepository = JuridicalDRepository
        _ConsecutiveRepository = ConsecutiveRepository
        _CustomerRepository = CustomerRepository
        _PortFolioRepository = PortFolioRepository
        _IInterfaceParametersRepository = IInterfaceParametersRepository
        _InterfaceFox = InterfaceFox
        _InterfaceNet = InterfaceNET
        _InterfacePublicFOX = InterfacePublicFOX
        _PortfolioSequenseAdminService = PortfolioSequenseAdminService
        _accountReceivableRepository = accountReceivableRepository
        _careGroupRepository = careGroupRepository
        _AccountReceivableAccountingRepository = AccountReceivableAccountingRepository
        _InterfaceNativeAdminservice = InterfaceNativeAdminservice
        _ITimeGlossParametersRepository = ITimeGlossParametersRepository
        _MovementGlosaRepository = MovementGlosaRepository
        _glosasServices = glosasServices
        _SettingPortfolioRepository = settingPortfolioRepository
        _DemandTransferJuridicalRepository = demandTransferJuridicalRepository
    End Sub
#End Region

#Region "Methods"

    Public Function CopyAndPasteTransferJuridicalDebtCollectionDetail(operatingUnitId As Integer, customerId As Integer, transferJuridicalDebtCollectionCId As Integer, copyType As Integer, dataImportFile As List(Of ImportFileRow), dataCopyPaste As List(Of List(Of String)), session As SessionValues) As ActionResult(Of List(Of TransferJuridicalDebtCollectionD)) Implements ITransferJuridicalDebtCAdminService.CopyAndPasteTransferJuridicalDebtCollectionDetail
        Try
            Dim xmlParameter As String = ConvertToXmlParameters(operatingUnitId, customerId, transferJuridicalDebtCollectionCId, copyType)
            Dim xmlObject As String = String.Empty
            If dataImportFile IsNot Nothing Then
                xmlObject = ConvertToXmlImportFile(dataImportFile)
            Else
                xmlObject = ConvertToXmlCopyPaste(dataCopyPaste)
            End If

            Dim resultStore = Me._JuridicalCRepository.SP_CopyAndPasteTransferJuridicalDebtCollectionDetail(xmlParameter, xmlObject)

            Dim listErrors As New List(Of String)
            Dim ListDetails As New List(Of TransferJuridicalDebtCollectionD)
            If resultStore IsNot Nothing AndAlso resultStore.Count > 0 Then
                For Each itemXml In resultStore
                    If itemXml.StatusField = 0 Then
                        ListDetails.Add(New TransferJuridicalDebtCollectionD With
                        {
                            .TransferJuridicalDebtCollectionCId = transferJuridicalDebtCollectionCId,
                            .PortfolioGlosaId = itemXml.PortfolioGlosaId,
                            .AccountReceivableId = itemXml.AccountReceivableId,
                            .LegalTransferValue = itemXml.LegalTransferValue,
                            .InvoiceNumber = itemXml.InvoiceNumber,
                            .AccountReceivableDate = itemXml.AccountReceivableDate
                        })
                    Else
                        listErrors.Add(itemXml.MessageField)
                    End If
                Next
            End If

            Return New ActionResult(Of List(Of TransferJuridicalDebtCollectionD)) With {.StateResult = True, .ObjectEmbbeded = ListDetails, .MessageResult = listErrors}
        Catch ex As SqlClient.SqlException
            If ex.ErrorCode = -2146232060 Then
                Return New ActionResult(Of List(Of TransferJuridicalDebtCollectionD)) With {.StateResult = False, .Message = "Los valores contienen decimales con un formato no valido, por favor corrija para poder continuar"}
            Else
                Return New ActionResult(Of List(Of TransferJuridicalDebtCollectionD)) With {.StateResult = False, .Message = ex.ToString}
            End If
        Catch ex As Exception
            Return New ActionResult(Of List(Of TransferJuridicalDebtCollectionD)) With {.StateResult = False, .Message = ex.ToString}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una devolución cabecera según código.
    ''' </summary>
    ''' <param name="Id">Id Devolución Cabecera</param>
    ''' <returns>Objeto Devolución Cabecera</returns>
    Public Function GetTransferJuridicalDebtC(Id As String) As TransferJuridicalDebtCollectionC Implements ITransferJuridicalDebtCAdminService.GetTransferJuridicalDebtC
        If String.IsNullOrEmpty(Id) = True Then
            Throw New ArgumentNullException("Id vacío")
        End If
        Try
            Return _JuridicalCRepository.GetTransferJuridicalDebtC(Id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una devolución cabecera según consecutivo.
    ''' </summary>
    ''' <param name="Consecutive">Consecutivo Devolución Cabecera</param>
    ''' <returns>Objeto Devolución Cabecera</returns>
    Public Function GetTransferJuridicalDebtCByConsecutive(Consecutive As String) As TransferJuridicalDebtCollectionC Implements ITransferJuridicalDebtCAdminService.GetTransferJuridicalDebtCByConsecutive
        If String.IsNullOrEmpty(Consecutive) = True Then
            Throw New ArgumentNullException("Consecutivo vacío")
        End If
        Try
            Return _JuridicalCRepository.GetTransferJuridicalDebtCByConsecutive(Consecutive)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Listar todas las devoluciones cabecera.
    ''' </summary>
    ''' <returns>Lista de Devoluciones Cabeceras</returns>
    Public Function ListAllTransferJuridicalDebtC() As List(Of TransferJuridicalDebtCollectionC) Implements ITransferJuridicalDebtCAdminService.ListAllTransferJuridicalDebtC
        Try
            Return _JuridicalCRepository.ListAllTransferJuridicalDebtC
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guardar Traslado Cobro Jurídico Cabecera
    ''' </summary>
    ''' <param name="JuridicalC"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function SaveTransferJuridicalDebtC(JuridicalC As TransferJuridicalDebtCollectionC, ByVal session As SessionValues) As ActionResult(Of TransferJuridicalDebtCollectionC) Implements ITransferJuridicalDebtCAdminService.SaveTransferJuridicalDebtC
        If JuridicalC Is Nothing Then
            Throw New ArgumentNullException("TransferJuridicalDebtCollectionC")
        End If

        Dim unitOfWork As IUnitWork = Me._JuridicalCRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try

                'validamos que si  el tipo de compañia es 1 dejeme el estado en 1
                'debido a que cuando en tipo 2 la compañia el sp se encarga de confirmarla.
                If (session.IndigoCompanyType = 1 And JuridicalC.State <> 3) Then
                    JuridicalC.State = 1
                End If

                Dim EntityXml As String = JuridicalC.ToXML()
                Dim auditStatus As Infrastructure.CrossCutting.Audit.Actions = Utils.GetAuditStatus(JuridicalC.Id, JuridicalC.State)

                Dim resultStore = Me._JuridicalCRepository.SP_SaveTransferJuridicalDebtCollection(EntityXml, session.AuditMessageWcf.CodeUser, session.IndigoGlossesIntegration, session.IndigoCompanyType)
                If resultStore.CodeResult <> 0 Then
                    unitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of TransferJuridicalDebtCollectionC) With {.StateResult = False, .MessageResult = {resultStore.MessageResult}.ToList, .Message = resultStore.MessageResult}
                End If

                JuridicalC.Id = resultStore.Id
                JuridicalC.JuridicalTransferConsecutive = resultStore.Code

                Dim auditProcess = New IndigoAuditSimpleEntity(Of TransferJuridicalDebtCollectionC)(JuridicalC, session.AuditMessageWcf, auditStatus, JuridicalC.OriginalValue)
                auditProcess.Execute()

                JuridicalC.MarkAsUnchanged()
                transaction.Complete()
                Return New ActionResult(Of TransferJuridicalDebtCollectionC) With {.StateResult = True, .ObjectEmbbeded = JuridicalC, .Message = resultStore.MessageResult}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                Return New ActionResult(Of TransferJuridicalDebtCollectionC) With {.StateResult = False, .MessageResult = {"-999"}.ToList(), .Message = ex.Message}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
                Return New ActionResult(Of TransferJuridicalDebtCollectionC) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Confirma la Transferencia Cobro Jurídico
    ''' </summary>
    ''' <param name="JuridicalC">Objeto Transferencia Cobro Jurídico Cabecera</param>
    ''' <param name="ListJuridical">Lista Transferencia Cobro Jurídico Detalles</param>
    ''' <param name="IndigoSessionValues">Objeto Auditoria</param>
    ''' <returns>Action Result</returns>
    Public Function ConfirmTransferJuridicalDebt(JuridicalC As TransferJuridicalDebtCollectionC, ByVal IndigoSessionValues As SessionValues) As ActionResult Implements ITransferJuridicalDebtCAdminService.ConfirmTransferJuridicalDebt
        If JuridicalC Is Nothing Then
            Throw New ArgumentNullException("Cabecera transferencia cobro jurídico vacío")
        End If

        Dim unitOfWork As IUnitWork = _JuridicalCRepository.UnitWork
        Dim unitOfWorkAccountReceivableAccounting As IUnitWork = _AccountReceivableAccountingRepository.UnitWork
        Dim unitOfWorkMovements As IUnitWork = _MovementGlosaRepository.UnitWork
        Dim unitOfWorkJuridicalD As IUnitWork = _JuridicalDRepository.UnitWork
        Dim unitOfWorkDemand As IUnitWork = _DemandTransferJuridicalRepository.UnitWork
        Dim unitOfWorkAccountReceivable As IUnitWork = _accountReceivableRepository.UnitWork
        Dim unitOfPortFolio As IUnitWork = _PortFolioRepository.UnitWork

        Try
            Dim operatingUnitId = JuridicalC.OperatingUnitId
            JuridicalC = _JuridicalCRepository.GetTransferJuridicalDebtC(JuridicalC.Id)
            JuridicalC.OperatingUnitId = operatingUnitId

            'configuro la transaccion
            Dim txSettings As New TransactionOptions()
            txSettings.Timeout = TransactionManager.MaximumTimeout
            txSettings.IsolationLevel = IsolationLevel.ReadCommitted
            'inicio la transaccion
            Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
                Dim ListJuridical = _JuridicalDRepository.ListTransferJuridicalDByIdTransferJuridicalC(JuridicalC.Id, True)
                If ListJuridical.Count = 0 Then
                    Return New ActionResult With {.StateResult = False, .Message = "Detalle de transferencia cobro jurídico vacío"}
                End If

                Dim records As New List(Of List(Of String))
                For Each detail In ListJuridical
                    records.Add(New List(Of String) From {detail.InvoiceNumber})
                Next

                Dim resultValidation = Me.CopyAndPasteTransferJuridicalDebtCollectionDetail(JuridicalC.OperatingUnitId, JuridicalC.CustomerId, JuridicalC.Id, 4, Nothing, records, IndigoSessionValues)

                If resultValidation.StateResult AndAlso Not resultValidation.MessageResult.Any() Then
                    Dim strmesagge As New StringBuilder
                    If JuridicalC.Reclassified Then
                        Dim _idCurrentSequense As Integer
                        If IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.Native OrElse IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNet OrElse IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.IntegractionFox OrElse IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNATIVE_MIGRATIONS Then
                            If IndigoSessionValues.IndigoCompanyType = eCompanyType.PrivateCompany Then
                                Dim _sequense As Domain.Entities.PortfolioSequence = Me._PortfolioSequenseAdminService.GetSequenseByIdForm("1529") 'tag del formulario de reclasificacion de documentos de cartera
                                If _sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
                                    _idCurrentSequense = _sequense.PortfolioSequenceDetail(0).Id
                                ElseIf _sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                                    If _sequense.PortfolioSequenceDetail.Any(Function(S) S.IdOperatingUnit = JuridicalC.OperatingUnitId) Then
                                        _idCurrentSequense = _sequense.PortfolioSequenceDetail.Where(Function(s) s.IdOperatingUnit = JuridicalC.OperatingUnitId).SingleOrDefault().Id
                                    Else
                                        Return New ActionResult With {.StateResult = False, .Message = ResourceManager.GetString("OperatingUnitUnassigned")}
                                    End If
                                End If
                            End If
                        End If

                        Dim listAccount As New List(Of Tuple(Of Integer, Decimal))
                        For i As Integer = 0 To ListJuridical.Count - 1
                            listAccount.Clear()
                            Dim AccountReceivable As AccountReceivable = _accountReceivableRepository.GetAccountByInvoiceNumberAndAccountReceivableType(ListJuridical(i).InvoiceNumber, {1, 2})
                            Dim ListAccountReceivableAccounting As List(Of AccountReceivableAccounting) = _AccountReceivableAccountingRepository.ListAccountReceivableAccounting(AccountReceivable.Id)
                            Dim _tmpbalance As Decimal = 0
                            For Each item As AccountReceivableAccounting In ListAccountReceivableAccounting
                                If item.Balance > 0 Then
                                    listAccount.Add(New Tuple(Of Integer, Decimal)(item.MainAccountId, item.Balance))
                                    _tmpbalance += item.Balance
                                    item.Balance = 0
                                    If (IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.Native OrElse
                                    IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNet OrElse
                                    IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.IntegractionFox OrElse
                                    IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNATIVE_MIGRATIONS) AndAlso
                                    IndigoSessionValues.IndigoCompanyType = eCompanyType.PrivateCompany Then
                                        _AccountReceivableAccountingRepository.SaveEntity(item)
                                        unitOfWorkAccountReceivableAccounting.Commit()
                                    End If
                                End If
                            Next

                            If IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.Native OrElse IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNet OrElse IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.IntegractionFox OrElse IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNATIVE_MIGRATIONS Then
                                If IndigoSessionValues.IndigoCompanyType = eCompanyType.PrivateCompany Then
                                    Dim result = Me.ExecuteNativePrivate(AccountReceivable, listAccount, AccountReceivable.OperatingUnitId, _idCurrentSequense, IndigoSessionValues)
                                    If result.StateResult = False Then
                                        Return result
                                    Else
                                        strmesagge.AppendLine(result.Message)
                                    End If
                                End If
                            End If

                            Dim portfolioGlosa = _PortFolioRepository.GetPortfolioGlosadaWithAggregates(ListJuridical(i).InvoiceNumber)
                            If portfolioGlosa IsNot Nothing AndAlso portfolioGlosa.Id > 0 Then
                                portfolioGlosa.LegalTransferValue = portfolioGlosa.BalanceGlosa  'saldo factura por aceptar por parte de la EAPB
                                portfolioGlosa.BalanceGlosa = 0 'anulamos el saldo ya que se paso por tranferencia cobro juridico
                                portfolioGlosa.TempState = portfolioGlosa.State
                                portfolioGlosa.State = 15 'cobro juridico
                                _PortFolioRepository.SaveEntity(portfolioGlosa)
                                unitOfPortFolio.Commit()

                                'Actualizamos movimientos de glosas
                                Dim listMovementUpdate As List(Of GlosaMovementGlosa) = _MovementGlosaRepository.ListAllMovementGlosaByInvoiceNumber(ListJuridical(i).InvoiceNumber)
                                For Each item As GlosaMovementGlosa In listMovementUpdate
                                    item.LegalTransferValue = item.ValuePendingConciliation
                                    item.ValuePendingConciliation = 0
                                    _MovementGlosaRepository.SaveEntity(item)
                                    unitOfWorkMovements.Commit()
                                Next
                            End If

                            'Actualizo la tabla de cuentas por cobrar
                            AccountReceivable.PortfolioStatus = 16 ' TRASLADO COBRO JURÍDICO CONFIRMADO 
                            AccountReceivable.MarkAsModified()
                            _accountReceivableRepository.SaveEntity(AccountReceivable)
                            unitOfWorkAccountReceivable.Commit()

                            'actualizo saldo de traslado
                            ListJuridical(i).LegalTransferValue = _tmpbalance
                            _JuridicalDRepository.SaveEntity(ListJuridical(i))
                            unitOfWorkJuridicalD.Commit()
                        Next
                    End If

                    Dim _DemandTransferJuridical = New DemandTransferJuridical()
                    _DemandTransferJuridical.TransferJuridicalDebtCollectionCId = JuridicalC.Id
                    _DemandTransferJuridical.FilingUnitSourceId = JuridicalC.FilingUnitSourceId
                    _DemandTransferJuridical.FilingUnitTargetId = JuridicalC.FilingUnitTargetId
                    _DemandTransferJuridical.LawyerId = JuridicalC.LawyerId
                    _DemandTransferJuridical.DemandStatusId = JuridicalC.DemandStatusId
                    _DemandTransferJuridical.CreationDate = Date.Now
                    _DemandTransferJuridical.CreationUser = IndigoSessionValues.AuditMessageWcf.CodeUser
                    _DemandTransferJuridical.StartTracking()
                    _DemandTransferJuridicalRepository.SaveEntity(_DemandTransferJuridical)
                    unitOfWorkDemand.Commit()

                    JuridicalC.State = 2
                    _JuridicalCRepository.SaveEntity(JuridicalC)
                    unitOfWork.Commit()

                    'confirmo la transaccion
                    scope.Complete()
                    Return New ActionResult With {.StateResult = True, .Message = strmesagge.ToString}
                Else
                    scope.Dispose()
                    Return New ActionResult With {.StateResult = False, .Message = String.Join(vbNewLine, resultValidation.MessageResult)}
                End If
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .Message = "-999"}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", IndigoSessionValues)
            Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Reversa un traslado juridico
    ''' </summary>
    ''' <param name="idTransferJuridical"></param>
    ''' <param name="_IdUnitoperating"></param>
    ''' <param name="IndigoSessionValues"></param>
    ''' <returns></returns>
    Public Function ReverseTransferJuridical(ByVal idTransferJuridical As Integer, ByVal _IdUnitoperating As Integer, ByVal IndigoSessionValues As SessionValues) As ActionResult(Of String) Implements ITransferJuridicalDebtCAdminService.ReverseTransferJuridical
        Dim conx As String = Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, ServerSessionValues.Current.CurrentContainer, False)

        Using connection As SqlConnection = New SqlConnection(conx)
            connection.Open()
            Dim command = New SqlCommand("Glosas.SP_ReverseJuridicalTransfer")


            Try
                Dim _GenerateReclassification As Boolean
                Dim dt = New DataTable()
                Dim resultado As String = String.Empty

                _GenerateReclassification = (IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.Native OrElse
                                        IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNet OrElse
                                        IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.IntegractionFox OrElse
                                        IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNATIVE_MIGRATIONS) AndAlso
                                        IndigoSessionValues.IndigoCompanyType = eCompanyType.PrivateCompany

                command.Connection = connection

                command.CommandType = CommandType.StoredProcedure
                command.Parameters.Add(New SqlParameter("@Id", idTransferJuridical))
                command.Parameters.Add(New SqlParameter("@IdOperatingUnit", _IdUnitoperating))
                command.Parameters.Add(New SqlParameter("@CodeUser", IndigoSessionValues.AuditMessageWcf.CodeUser))
                command.Parameters.Add(New SqlParameter("@GenerateReclassification", _GenerateReclassification))

                Using adapter = New SqlDataAdapter(command)
                    adapter.SelectCommand.CommandTimeout = 0
                    adapter.Fill(dt)
                End Using

                If dt.Rows.Count > 0 Then
                    For Each r As DataRow In dt.Rows
                        resultado = String.Format("{0}{1}{2}", resultado, Environment.NewLine, r.Field(Of String)("MessageResult"))
                    Next

                Else
                    resultado = "No se encontro información"
                End If

                Return New ActionResult(Of String) With {
                                .StateResult = True,
                                .Message = resultado
                            }
            Catch ex As System.Data.Entity.Core.OptimisticConcurrencyException

                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", IndigoSessionValues)
                Return New ActionResult(Of String) With {
                    .StateResult = False,
                    .Message = ResourceManager.GetString("ErrorConcurrence")
                }
            Catch ex As System.Data.Entity.Core.UpdateException

                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", IndigoSessionValues)
                Return New ActionResult(Of String) With {
                    .StateResult = False,
                    .Message = ResourceManager.GetString("ErrorUnknown")
                }
            Catch ex As Exception

                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of String) With {.StateResult = False, .Message = ex.Message}
            End Try
        End Using

    End Function

    ''' <summary>
    ''' Creamos Documento de reclasificacion 
    ''' </summary>
    ''' <param name="_AccountReceivable"></param>
    ''' <param name="listAccount"></param>
    ''' <param name="_IdUnitoperating"></param>
    ''' <param name="IndigoSessionValues"></param>
    ''' <returns></returns>
    Public Function ExecuteNativePrivate(_AccountReceivable As AccountReceivable, ByVal listAccount As List(Of Tuple(Of Integer, Decimal)), _IdUnitoperating As Integer, _idCurrentSequense As Integer, IndigoSessionValues As SessionValues) As ActionResult
        Dim _ConfirmResult As New ActionResult
        Dim StrMessage As New StringBuilder
        Dim listStrValidateMessage As New StringBuilder
        Try
            Dim GlossParameter As TimeParameters = _ITimeGlossParametersRepository.GetTimeParametersDefault(_IdUnitoperating)
            If GlossParameter.Id = 0 Then
                Return New ActionResult With {.StateResult = False, .Message = "No se encontrarón parametros de glosas"}
            End If
            Dim AccountReceivableAccountingunitOfWork As IUnitWork = _AccountReceivableAccountingRepository.UnitWork
            'reclasificacion
            If _AccountReceivable.AccountLegalCollectionId Is Nothing Then
                listStrValidateMessage.AppendLine("La cuenta de traslado a cobro juridico esta vacio para la factura: " & _AccountReceivable.InvoiceNumber)
                Return New ActionResult With {.StateResult = False, .Message = listStrValidateMessage.ToString}
            End If
			If GlossParameter.TransferLegalJournalVoucherTypeId Is Nothing Then
				listStrValidateMessage.AppendLine("El tipo de documento contable para traslado a cobro juridico esta vacio para la factura: " & _AccountReceivable.InvoiceNumber)
				Return New ActionResult With {.StateResult = False, .Message = listStrValidateMessage.ToString}
			End If

			Dim _AccountReceivableAccountingJuridical As New AccountReceivableAccounting
			'generamos nueva estructura de cuenta de cobro de traslado a cobro juridico
			With _AccountReceivableAccountingJuridical
				.AccountReceivableId = _AccountReceivable.Id
				.MainAccountId = _AccountReceivable.AccountLegalCollectionId  'Id Cuenta traslado cobro juridico
				.ThirdPartyId = _AccountReceivable.ThirdPartyId
				.CostCenterId = _AccountReceivable.CostCenterId
				.Value = listAccount.Sum(Function(d) d.Item2)
				.Balance = .Value
			End With
			Dim ExistRegister = _AccountReceivableAccountingRepository.GetAccountReceivableAccounting(_AccountReceivableAccountingJuridical.AccountReceivableId, _AccountReceivableAccountingJuridical.MainAccountId)
			If ExistRegister IsNot Nothing AndAlso ExistRegister.Id > 0 Then
				ExistRegister.ThirdPartyId = _AccountReceivableAccountingJuridical.ThirdPartyId
				ExistRegister.CostCenterId = _AccountReceivableAccountingJuridical.CostCenterId
				ExistRegister.Value = _AccountReceivableAccountingJuridical.Value
				ExistRegister.Balance = _AccountReceivableAccountingJuridical.Balance
				ExistRegister.ChangeTracker.State = ObjectState.Modified
				_AccountReceivableAccountingRepository.SaveEntity(ExistRegister)
			Else
				_AccountReceivableAccountingRepository.SaveEntity(_AccountReceivableAccountingJuridical)
			End If
			AccountReceivableAccountingunitOfWork.Commit()
            'creamos documento de reclasificacion
            Dim tmpclasificationResul = _InterfaceNativeAdminservice.CreatePortfolioReclassificationLegalTransfer(TypePortfolioReclassification.TransFerJuridical, _idCurrentSequense, _AccountReceivable, listAccount, _AccountReceivable.ThirdPartyId, GlossParameter, IndigoSessionValues.AuditMessageWcf)
            If tmpclasificationResul.StateResult = True Then
                For Each Item As String In tmpclasificationResul.MessageResult
                    StrMessage.AppendLine(Item)
                Next
            Else
                Return tmpclasificationResul
            End If
            _ConfirmResult.StateResult = True
            _ConfirmResult.Message = StrMessage.ToString
            Return _ConfirmResult
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", IndigoSessionValues)
            Return New ActionResult With {.StateResult = False, .Message = ex.ToString}
        End Try
    End Function

    ''' <summary>
    ''' Borrar Traslado Cobro Jurídico Cabecera.
    ''' </summary>
    ''' <param name="JuridicalC">Objeto Traslado Cobro Jurídico Cabecera</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Action Result</returns>
    Public Function DeleteTransferJuridicalDebtC(JuridicalC As TransferJuridicalDebtCollectionC, audit As AuditMessage) As ActionResult Implements ITransferJuridicalDebtCAdminService.DeleteTransferJuridicalDebtC
        If JuridicalC Is Nothing Then
            Throw New ArgumentNullException("Cabecera Traslado Cobro Jurídico Vacia")
        End If
        Dim unitOfWork As IUnitWork = _JuridicalCRepository.UnitWork
        Try
            'Elimino la cabecera de traslado cobro jurídico.
            _JuridicalCRepository.DeleteEntity(JuridicalC)
            unitOfWork.Commit()
            '/***** Auditoria Basica *******/
            IndigoAuditBasic.Execute("TransferJuridicalDebtReceptionC", audit.Functional, JuridicalC.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, Infrastructure.CrossCutting.Base.ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            Dim auditObject As New IndigoAuditSimpleEntity(Of TransferJuridicalDebtCollectionC)(JuridicalC, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return New ActionResult With {.StateResult = True}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList()}
        End Try
    End Function

#End Region

#Region "Private Methods"

    Private Function ConvertToXmlParameters(operatingUnitId As Integer, customerId As Integer, transferJuridicalDebtCollectionCId As Integer, type As Integer)
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<Data>")
        builder.Append("<Row>")

        builder.Append("<OperatingUnitId>" & operatingUnitId & "</OperatingUnitId>")
        builder.Append("<CustomerId>" & customerId & "</CustomerId>")
        builder.Append("<TransferJuridicalDebtCollectionCId>" & transferJuridicalDebtCollectionCId & "</TransferJuridicalDebtCollectionCId>")
        builder.Append("<Type>" & type & "</Type>")

        builder.Append("</Row>")
        builder.Append("</Data>")
        Return builder.ToString
    End Function

    Private Function ConvertToXmlImportFile(dataImportFile As List(Of ImportFileRow))
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<Data>")

        For Each importFile In dataImportFile
            Dim indexRow = importFile.IndexRow
            Dim columns = importFile.Row.Count

            builder.Append("<Row>")
            builder.Append("<RowIndex>" & indexRow & "</RowIndex>")
            builder.Append("<RowColumns>" & columns & "</RowColumns>")

            If (columns >= 1) Then
                builder.Append("<InvoiceNumber>" & importFile.Row.Item(0) & "</InvoiceNumber>")
            End If

            builder.Append("</Row>")
        Next

        builder.Append("</Data>")
        Return builder.ToString
    End Function

    Private Function ConvertToXmlCopyPaste(data As List(Of List(Of String)))
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<Data>")

        Dim indexRow As Integer = 0
        For Each item In data
            indexRow = indexRow + 1
            Dim columns = item.Count

            builder.Append("<Row>")
            builder.Append("<RowIndex>" & indexRow & "</RowIndex>")
            builder.Append("<RowColumns>" & columns & "</RowColumns>")

            If (columns >= 1) Then
                builder.Append("<InvoiceNumber>" & item(0) & "</InvoiceNumber>")
            End If

            builder.Append("</Row>")
        Next

        builder.Append("</Data>")
        Return builder.ToString
    End Function

    ''' <summary>
    ''' Funcion para validar las facturas que se van a confirmar para cobreo juridico
    ''' </summary>
    ''' <param name="listInvoiceTransferDetailD"></param>
    ''' <param name="isConfirm"></param>
    ''' <param name="_unReconcileInvoice"></param>
    ''' <returns></returns>
    Public Function validateInvoice(ByVal listInvoiceTransferDetailD As List(Of String), ByVal isConfirm As Boolean, ByVal _unReconcileInvoice As Boolean) As ActionResult Implements ITransferJuridicalDebtCAdminService.validateInvoice
        Dim result As New StringBuilder

        For Each item As String In listInvoiceTransferDetailD
            Dim AccountReceivable As AccountReceivable = _accountReceivableRepository.GetAccountReceivableByInvoiceNumber(item)
            If AccountReceivable IsNot Nothing AndAlso AccountReceivable.Id > 0 Then
                If AccountReceivable.Balance = 0 Then
                    result.AppendLine(item & " - Factura sin saldo")
                End If
                'valido estado en cartera 
                Select Case AccountReceivable.PortfolioStatus
                    Case 1
                        result.AppendLine(item & " - Factura esta sin radicar")
                    Case 2
                        result.AppendLine(item & " - Factura esta radicada sin confirmar")
                    Case 4
                        result.AppendLine(item & " - Factura esta radicada sin confirmar")
                    Case 16
                        result.AppendLine(item & " - Factura ya esta en un traslado a cobro jurídico Confirmado")
                End Select
            End If
            'valido estado en cartera de glosas '1', '4','8', '9', '10','13'
            Dim portfolioGlosa = _PortFolioRepository.GetPortfolioGlosadaWithAggregates(item)
            If portfolioGlosa IsNot Nothing AndAlso portfolioGlosa.Id > 0 Then
                If _unReconcileInvoice Then
                    'permite factura glosada con, sin conciliar
                    Select Case portfolioGlosa.State
                        Case 13
                            result.AppendLine(item & " - Factura pendiente confirmar pago parcial")
                    End Select
                Else
                    'valido estado en cartera 
                    Select Case portfolioGlosa.State
                        Case 1
                            result.AppendLine(item & " - Factura pendiente de confirmar glosas")
                        Case 4
                            result.AppendLine(item & " - Factura pendiente de confirmar Reiteración")
                        Case 7
                            result.AppendLine(item & " - Factura pendiente confirmar conciliacón")
                        Case 8
                            result.AppendLine(item & " - Factura Conciliada")
                        Case 9
                            result.AppendLine(item & " - Factura Conciliación parcial")
                        Case 13
                            result.AppendLine(item & " - Factura pendiente confirmar pago parcial")
                    End Select
                End If

            End If
            If isConfirm = False Then
                Dim TransferD As TransferJuridicalDebtCollectionD = _JuridicalDRepository.GetTransferJuridicalDebtDByInvoice(item)
                If TransferD IsNot Nothing AndAlso TransferD.Id > 0 Then
                    result.AppendLine(item & " - Factura ya esta en el traslado a cobro jurídico N° " & TransferD.TransferJuridicalDebtCollectionC.JuridicalTransferConsecutive)
                End If
            End If
        Next
        If result.Length = 0 Then
            Return New ActionResult With {.StateResult = True}
        Else
            Return New ActionResult With {.StateResult = False, .Message = result.ToString()}
        End If
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _InterfaceFox.Dispose()
                _InterfaceNet.Dispose()
                _InterfacePublicFOX.Dispose()
                _PortfolioSequenseAdminService.Dispose()
                _InterfaceNativeAdminservice.Dispose()
                _glosasServices.Dispose()
            End If
            _JuridicalCRepository = Nothing
            _JuridicalDRepository = Nothing
            _ConsecutiveRepository = Nothing
            _CustomerRepository = Nothing
            _PortFolioRepository = Nothing
            _IInterfaceParametersRepository = Nothing
            _InterfaceFox = Nothing
            _InterfaceNet = Nothing
            _InterfacePublicFOX = Nothing
            _PortfolioSequenseAdminService = Nothing
            _accountReceivableRepository = Nothing
            _careGroupRepository = Nothing
            _AccountReceivableAccountingRepository = Nothing
            _InterfaceNativeAdminservice = Nothing
            _ITimeGlossParametersRepository = Nothing
            _MovementGlosaRepository = Nothing
            _glosasServices = Nothing
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
