'***********************************************************************
' Assembly         : Application.MedicalFees
' Author           : Carlos Mario Arias Rubiano
' Created          : 28/01/2015
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
Imports System.Transactions
Imports Domain.Entities.Service
Imports Application.Payments
Imports Domain.Crystal
Imports Domain.Crystal.Entities
Imports System.Text
Imports System.IO

Public Class MedicalFeesLiquidationAdminService
    Implements IMedicalFeesLiquidationAdminService

#Region "Variables"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _medicalFeesLiquidationRepository As IMedicalFeesLiquidationRepository

    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As IMedicalFeesSecuenceDetailRepository

    ''' <summary>
    ''' Repositorio de aplicacion para cuenta por pagar
    ''' </summary>
    ''' <remarks></remarks>
    Private _accountPayableAdminService As IAccountPayableAdminService

    ''' <summary>
    ''' Repositorio para proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Private _supplierRepository As Domain.Maintenance.ISupplierRepository

    ''' <summary>
    ''' Repositorio para contratos
    ''' </summary>
    ''' <remarks></remarks>
    Private _medicalFeesContractRepository As IMedicalFeesContractRepository

    ''' <summary>
    ''' Repositorio para el medico
    ''' </summary>
    ''' <remarks></remarks>
    Private _healthProfessionalRepository As IHealthProfessionalRepository

    ''' <summary>
    ''' Repositorio para el paciente
    ''' </summary>
    ''' <remarks></remarks>
    Private _patientRepository As IPatientRepository

    ''' <summary>
    ''' Repositorio para cuenta por pagar
    ''' </summary>
    ''' <remarks></remarks>
    Private _accountPayableRepository As IAccountPayableRepository

    ''' <summary>
    ''' Repositorio para proveedor lineas de distribucion
    ''' </summary>
    ''' <remarks></remarks>
    Private _supplierDistributionLineRepository As ISuppliersDistributionLinesRepository

    ''' <summary>
    ''' Repositorio para lineas de distribucion
    ''' </summary>
    ''' <remarks></remarks>
    Private _distributionLineRepository As IDistributionLinesRepository

    ''' <summary>
    ''' Repositorio para los parametros de honorarios medicos
    ''' </summary>
    ''' <remarks></remarks>
    Private _settingsMedicalFeesRepository As IMedicalFeesSettingsRepository

    ''' <summary>
    ''' Repositorio para la secuencia numerica de cxp
    ''' </summary>
    ''' <remarks></remarks>
    Private _sequenseCPaymentsRepository As ISequensePaymentsCRepository

    ''' <summary>
    ''' Repositorio para causaciones de honorarios medicos
    ''' </summary>
    ''' <remarks></remarks>
    Private _medicalFeesCausationRepository As IMedicalFeesCausationRepository

#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal medicalFeesLiquidationRepository As IMedicalFeesLiquidationRepository, ByVal secuenseDRepository As IMedicalFeesSecuenceDetailRepository, ByVal accountPayableAdminService As IAccountPayableAdminService,
                   ByVal supplierRepository As Domain.Maintenance.ISupplierRepository, ByVal medicalFeesContractRepository As IMedicalFeesContractRepository, ByVal healthProfessionalRepository As IHealthProfessionalRepository,
                   ByVal accountPayableRepository As IAccountPayableRepository, ByVal supplierDistributionLineRepository As ISuppliersDistributionLinesRepository, ByVal distributionLineRepository As IDistributionLinesRepository,
                   settingsMedicalFeesRepository As IMedicalFeesSettingsRepository, sequenseCPaymentsRepository As ISequensePaymentsCRepository, medicalFeesCausationRepository As IMedicalFeesCausationRepository,
                   patientRepository As IPatientRepository)
        If medicalFeesLiquidationRepository Is Nothing Then
            Throw New ArgumentNullException("medicalFeesLiquidationRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        If accountPayableAdminService Is Nothing Then
            Throw New ArgumentNullException("accountPayableAdminService")
        End If
        If supplierRepository Is Nothing Then
            Throw New ArgumentNullException("supplierRepository")
        End If
        If medicalFeesContractRepository Is Nothing Then
            Throw New ArgumentNullException("medicalFeesContractRepository")
        End If
        If healthProfessionalRepository Is Nothing Then
            Throw New ArgumentNullException("healthProfessionalRepository")
        End If
        If accountPayableRepository Is Nothing Then
            Throw New ArgumentNullException("accountPayableRepository")
        End If
        If supplierDistributionLineRepository Is Nothing Then
            Throw New ArgumentNullException("supplierDistributionLineRepository")
        End If
        If distributionLineRepository Is Nothing Then
            Throw New ArgumentNullException("distributionLineRepository")
        End If
        If settingsMedicalFeesRepository Is Nothing Then
            Throw New ArgumentNullException("settingsMedicalFeesRepository")
        End If
        If sequenseCPaymentsRepository Is Nothing Then
            Throw New ArgumentNullException("sequenseCPaymentsRepository")
        End If
        If medicalFeesCausationRepository Is Nothing Then
            Throw New ArgumentNullException("medicalFeesCausationRepository")
        End If
        If patientRepository Is Nothing Then
            Throw New ArgumentNullException("patientRepository")
        End If
        _medicalFeesLiquidationRepository = medicalFeesLiquidationRepository
        _secuenseDRepository = secuenseDRepository
        _accountPayableAdminService = accountPayableAdminService
        _supplierRepository = supplierRepository
        _medicalFeesContractRepository = medicalFeesContractRepository
        _healthProfessionalRepository = healthProfessionalRepository
        _patientRepository = patientRepository
        _accountPayableRepository = accountPayableRepository
        _supplierDistributionLineRepository = supplierDistributionLineRepository
        _distributionLineRepository = distributionLineRepository
        _settingsMedicalFeesRepository = settingsMedicalFeesRepository
        _sequenseCPaymentsRepository = sequenseCPaymentsRepository
        _medicalFeesCausationRepository = medicalFeesCausationRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Confirma la liquidacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AnnularMedicalFeesLiquidation(ByVal MedicalFeesLiquidation As MedicalFeesLiquidation, ByVal audit As AuditMessage) As ActionResult(Of MedicalFeesLiquidation) Implements IMedicalFeesLiquidationAdminService.AnnularMedicalFeesLiquidation
        If MedicalFeesLiquidation Is Nothing Then
            Throw New ArgumentNullException("MedicalFeesLiquidation")
        End If


        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using Transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            'Dim unitOfWorkCausation As IUnitWork = Me._medicalFeesCausationRepository.UnitWork
            Try
                'If MedicalFeesLiquidation.MedicalFeesLiquidationDetail Is Nothing OrElse MedicalFeesLiquidation.MedicalFeesLiquidationDetail.Count = 0 Then
                '    unitOfWorkCausation.RollbackChanges()
                '    Transaction.Dispose()
                '    Return New ActionResult(Of MedicalFeesLiquidation) With {.StateResult = False, .MessageResult = {"No hay detalles de liquidación."}.ToList}
                'End If

                'For Each itemDetail As MedicalFeesLiquidationDetail In MedicalFeesLiquidation.MedicalFeesLiquidationDetail
                '    If itemDetail.LiquidationType = 1 Then
                '        Dim medicalFeesCausation = _medicalFeesCausationRepository.GetMedicalFeesCausationById(itemDetail.MedicalFeesCausationId)

                '        If medicalFeesCausation.InvoiceReversal = True OrElse medicalFeesCausation.ObjectionAccepted = True Then
                '            medicalFeesCausation.Status = 4
                '        Else
                '            medicalFeesCausation.Status = 1
                '        End If

                '        medicalFeesCausation.MarkAsModified()
                '        _medicalFeesCausationRepository.SaveEntity(medicalFeesCausation)
                '        unitOfWorkCausation.Commit()
                '    End If
                'Next

                'MedicalFeesLiquidation.Status = 3
                'Dim resultMedicalFeesLiquidation As ActionResult(Of MedicalFeesLiquidation) = SaveMedicalFeesLiquidation(MedicalFeesLiquidation, audit)
                'If resultMedicalFeesLiquidation.StateResult = False Then
                '    unitOfWorkCausation.RollbackChanges()
                '    Transaction.Dispose()
                '    Return New ActionResult(Of MedicalFeesLiquidation) With {.StateResult = False, .MessageResult = resultMedicalFeesLiquidation.MessageResult}
                'End If

                Dim resultStore = _medicalFeesLiquidationRepository.SP_AnnularMedicalFeesLiquidation(MedicalFeesLiquidation.Id, audit.CodeUser)
                If resultStore.CodeMessage <> 0 Then
                    Transaction.Dispose()
                    Return New ActionResult(Of MedicalFeesLiquidation) With {.StateResult = False, .MessageResult = {resultStore.Message}.ToList}
                End If

                Transaction.Complete()
                Return New ActionResult(Of MedicalFeesLiquidation) With {.StateResult = True, .ObjectEmbbeded = MedicalFeesLiquidation}
            Catch ex As OptimisticConcurrencyException
                'unitOfWorkCausation.RollbackChanges()
                Transaction.Dispose()
                Return New ActionResult(Of MedicalFeesLiquidation) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
            Catch ex As Exception
                'unitOfWorkCausation.RollbackChanges()
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of MedicalFeesLiquidation) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Elimina la liquidacion
    ''' </summary>
    ''' <param name="MedicalFeesLiquidation"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteMedicalFeesLiquidation(MedicalFeesLiquidation As MedicalFeesLiquidation, audit As AuditMessage) As ActionResult Implements IMedicalFeesLiquidationAdminService.DeleteMedicalFeesLiquidation
        If MedicalFeesLiquidation Is Nothing Then
            Throw New ArgumentNullException("MedicalFeesLiquidation")
        End If
        Dim unitOfWork As IUnitWork = Me._medicalFeesLiquidationRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of MedicalFeesLiquidation)
            auditProcess = New IndigoAuditSimpleEntity(Of MedicalFeesLiquidation)(MedicalFeesLiquidation, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._medicalFeesLiquidationRepository.DeleteEntity(MedicalFeesLiquidation)
            unitOfWork.Commit()
            auditProcess.Execute()
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
    ''' Obtiene la liquidacion por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMedicalFeesLiquidation(code As String, medicalFeesContractId As Integer, optionConsult As Integer, audit As AuditMessage, Optional ByVal healthProfeesionalCode As String = Nothing) As ActionResult(Of MedicalFeesLiquidation) Implements IMedicalFeesLiquidationAdminService.GetMedicalFeesLiquidation
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim MedicalFeesLiquidation As MedicalFeesLiquidation = Me._medicalFeesLiquidationRepository.GetMedicalFeesLiquidation(code.Trim(), medicalFeesContractId, optionConsult, healthProfeesionalCode)
            If MedicalFeesLiquidation IsNot Nothing AndAlso MedicalFeesLiquidation.Id > 0 Then

                'Se consulta el medico para el nullText de la cabecera
                If MedicalFeesLiquidation.HealthProfessionalCode IsNot Nothing Then
                    Dim healthProfessional = _healthProfessionalRepository.GetHealthProfessionalByCode(MedicalFeesLiquidation.HealthProfessionalCode)
                    MedicalFeesLiquidation.HealthProfessionalDescription = healthProfessional.CODPROSAL.Trim + " - " + healthProfessional.NOMMEDICO.Trim
                End If

                Dim auditObject As New IndigoAuditSimpleEntity(Of MedicalFeesLiquidation)(MedicalFeesLiquidation, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of MedicalFeesLiquidation) With {.StateResult = True, .ObjectEmbbeded = MedicalFeesLiquidation}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of MedicalFeesLiquidation) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una liquidacion por id del contrato profesional de la salud
    ''' </summary>
    ''' <param name="MedicalFeesContractId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMedicalFeesLiquidationByMedicalFeesContractId(MedicalFeesContractId As Integer, audit As AuditMessage) As ActionResult(Of MedicalFeesLiquidation) Implements IMedicalFeesLiquidationAdminService.GetMedicalFeesLiquidationByMedicalFeesContractId
        If MedicalFeesContractId = 0 Then
            Throw New ArgumentNullException("MedicalFeesContractId")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim MedicalFeesLiquidation As MedicalFeesLiquidation = Me._medicalFeesLiquidationRepository.GetMedicalFeesLiquidationByMedicalFeesContractId(MedicalFeesContractId)
            If MedicalFeesLiquidation IsNot Nothing AndAlso MedicalFeesLiquidation.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of MedicalFeesLiquidation)(MedicalFeesLiquidation, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of MedicalFeesLiquidation) With {.StateResult = True, .ObjectEmbbeded = MedicalFeesLiquidation}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of MedicalFeesLiquidation) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la liquidacion por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMedicalFeesLiquidationById(id As Integer, audit As AuditMessage) As ActionResult(Of MedicalFeesLiquidation) Implements IMedicalFeesLiquidationAdminService.GetMedicalFeesLiquidationById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim MedicalFeesLiquidation As MedicalFeesLiquidation = Me._medicalFeesLiquidationRepository.GetMedicalFeesLiquidationById(id)
            If MedicalFeesLiquidation IsNot Nothing AndAlso MedicalFeesLiquidation.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of MedicalFeesLiquidation)(MedicalFeesLiquidation, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of MedicalFeesLiquidation) With {.StateResult = True, .ObjectEmbbeded = MedicalFeesLiquidation}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of MedicalFeesLiquidation) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda y confirma la liquidacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveAndConfirmMedicalFeesLiquidation(medicalFeesLiquidation As MedicalFeesLiquidation, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of MedicalFeesLiquidation) Implements IMedicalFeesLiquidationAdminService.SaveAndConfirmMedicalFeesLiquidation
        If medicalFeesLiquidation Is Nothing Then
            Throw New ArgumentNullException("medicalFeesLiquidation")
        End If

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                ''Se instancia el dominio de honorarios medicos
                'Dim medicalFeesService As New MedicalFeesServices(_supplierRepository, _medicalFeesContractRepository, _healthProfessionalRepository, _accountPayableRepository,
                '                                                  _supplierDistributionLineRepository, _distributionLineRepository, _settingsMedicalFeesRepository, _medicalFeesLiquidationRepository)

                ''Se crea el objeto de cuenta por pagar con el objeto de liquidacion
                'Dim resultCreateAccountpayable As ActionResult(Of AccountPayable) = medicalFeesService.CreateAccountPayable(medicalFeesLiquidation)
                'If resultCreateAccountpayable.StateResult = False Then
                '    transaction.Dispose()
                '    Return New ActionResult(Of MedicalFeesLiquidation) With {.StateResult = False, .MessageResult = resultCreateAccountpayable.MessageResult}
                'End If
                'Agrego el objeto de cuenta x pagar a un listado porque el metodo construido lo recibe
                'Dim ListAccountPayable As New List(Of AccountPayable)
                'ListAccountPayable.Add(resultCreateAccountpayable.ObjectEmbbeded)

                'Se consulta el id de la secuencia para el form de cxp para poder sacar el consecutivo de la misma
                'Dim sequensePayments = _sequenseCPaymentsRepository.GetSequenseByIdForm("730")
                'If sequensePayments.Id = 0 Then
                '    transaction.Dispose()
                '    Return New ActionResult(Of MedicalFeesLiquidation) With {.StateResult = False, .MessageResult = {"No se puede generar la cuenta por pagar porque no existe secuencia numérica para generar el consecutivo"}.ToList}
                'End If

                'Se realiza el cambio porque Jose dijo, cuando se confirma la liquidación de honorarios médicos no se debe crear confirmada la cxp asi le escriban el No. factura
                'Se debe crear la cxp sin confirmar
                'Se guarda la cxp y se confirma segun correponda
                'Dim banSaveAndConfirm As Boolean = False
                'Dim banSaveAndConfirm As Boolean 'False = solo guarda cxp, True = guarda cxp y la confirma
                'If medicalFeesLiquidation.BillNumber = String.Empty Then
                '    banSaveAndConfirm = False
                'Else
                '    banSaveAndConfirm = True
                'End If

                'Dim resultSaveAndConfirmAccountPayable As ActionResult(Of List(Of String))
                'resultSaveAndConfirmAccountPayable = _accountPayableAdminService.SaveListAccountPayable(ListAccountPayable, Nothing, banSaveAndConfirm, audit, sequensePayments.PaymentsSecuenceDetail(0).Id)
                'If resultSaveAndConfirmAccountPayable.StateResult = False Then
                '    transaction.Dispose()
                '    Return New ActionResult(Of MedicalFeesLiquidation) With {.StateResult = False, .MessageResult = resultSaveAndConfirmAccountPayable.MessageResult}
                'End If

                Dim XmlObject = ConvertToXml(medicalFeesLiquidation)
                Dim resultStore = _medicalFeesLiquidationRepository.SP_ConfirmMedicalFeesLiquidation(XmlObject, audit.CodeUser)
                If resultStore.CodeMessage <> 0 Then
                    transaction.Dispose()
                    Return New ActionResult(Of MedicalFeesLiquidation) With {.StateResult = False, .MessageResult = {resultStore.Message}.ToList}
                End If

                'Se guarda o actualiza la liquidacion de honorarios medicos en las tablas de MedicalFeesLiquidation y MedicalFeesLiquidationDetail
                medicalFeesLiquidation.AccountPayableId = resultStore.AccountPayableId
                medicalFeesLiquidation.Status = 2
                Dim resultMedicalFeesLiquidation = SaveMedicalFeesLiquidation(medicalFeesLiquidation, audit, idSequense)
                If resultMedicalFeesLiquidation.StateResult = False Then
                    transaction.Dispose()
                    Return New ActionResult(Of MedicalFeesLiquidation) With {.StateResult = False, .MessageResult = resultMedicalFeesLiquidation.MessageResult}
                End If
                medicalFeesLiquidation = resultMedicalFeesLiquidation.ObjectEmbbeded

                transaction.Complete()
                Return New ActionResult(Of MedicalFeesLiquidation) With {.StateResult = True, .ObjectEmbbeded = medicalFeesLiquidation, .MessageResult = {resultStore.CodeAccountPayable}.ToList}
            Catch ex As Exception
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of MedicalFeesLiquidation) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Guarda o actualiza la liquidacion
    ''' </summary>
    ''' <param name="MedicalFeesLiquidation"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveMedicalFeesLiquidation(MedicalFeesLiquidation As MedicalFeesLiquidation, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of MedicalFeesLiquidation) Implements IMedicalFeesLiquidationAdminService.SaveMedicalFeesLiquidation
        If MedicalFeesLiquidation Is Nothing Then
            Throw New ArgumentNullException("MedicalFeesLiquidation")
        End If
        'Dim unitOfWork As IUnitWork = Me._medicalFeesLiquidationRepository.UnitWork
        'Dim unitOfWorkCausation As IUnitWork = Me._medicalFeesCausationRepository.UnitWork
        'Dim unitOfWorkContract As IUnitWork = Me._medicalFeesContractRepository.UnitWork
        'Dim unitOfWorkHealthProfessional As IUnitWork = Me._healthProfessionalRepository.UnitWork
        'Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                'Dim seq As MedicalFeesSecuenceDetail = Nothing
                'If MedicalFeesLiquidation.Code Is Nothing OrElse MedicalFeesLiquidation.Code.Trim().Equals(String.Empty) Then
                '    seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                '    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MedicalFeesSecuence.Sequential Then
                '        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                '        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                '            MedicalFeesLiquidation.Code = res
                '            seq.Next += 1
                '            Me._secuenseDRepository.SaveEntity(seq)
                '        Else
                '            Return New ActionResult(Of MedicalFeesLiquidation) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                '        End If
                '    Else
                '        Return New ActionResult(Of MedicalFeesLiquidation) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                '    End If
                'End If

                Dim auxMedicalFeesLiquidation As MedicalFeesLiquidation = Nothing
                'Dim auditProcess As IndigoAuditSimpleEntity(Of MedicalFeesLiquidation)
                Dim status As Integer

                If MedicalFeesLiquidation.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    'MedicalFeesLiquidation.CreationUser = audit.CodeUser
                    'MedicalFeesLiquidation.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                ElseIf MedicalFeesLiquidation.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    auxMedicalFeesLiquidation = MedicalFeesLiquidation.OriginalValue
                    Select Case MedicalFeesLiquidation.Status
                        Case 1
                            'MedicalFeesLiquidation.ModificationUser = audit.CodeUser
                            'MedicalFeesLiquidation.ModificationDate = DateTime.Now
                            status = Infrastructure.CrossCutting.Audit.Actions.Update
                        Case 2
                            'MedicalFeesLiquidation.ModificationUser = audit.CodeUser
                            'MedicalFeesLiquidation.ModificationDate = DateTime.Now
                            'MedicalFeesLiquidation.ConfirmationUser = audit.CodeUser
                            'MedicalFeesLiquidation.ConfirmationDate = DateTime.Now
                            status = Infrastructure.CrossCutting.Audit.Actions.Confirm
                        Case 3
                            'MedicalFeesLiquidation.ModificationUser = audit.CodeUser
                            'MedicalFeesLiquidation.ModificationDate = DateTime.Now
                            'MedicalFeesLiquidation.AnnulmentUser = audit.CodeUser
                            'MedicalFeesLiquidation.AnnulmentDate = DateTime.Now
                            status = Infrastructure.CrossCutting.Audit.Actions.Annular
                    End Select
                End If

                ''Actualizo las variables correspondientes en la tabla de medicalFeesCausation
                'If (MedicalFeesLiquidation.Id = 0 AndAlso MedicalFeesLiquidation.Status = 1) OrElse MedicalFeesLiquidation.Status = 2 Then
                '    For Each itemDetail As MedicalFeesLiquidationDetail In MedicalFeesLiquidation.MedicalFeesLiquidationDetail
                '        Dim medicalFeesCausation = _medicalFeesCausationRepository.GetMedicalFeesCausationById(itemDetail.MedicalFeesCausationId)
                '        If itemDetail.ChangeTracker.State <> ObjectState.Deleted Then

                '            If MedicalFeesLiquidation.Status = 2 AndAlso medicalFeesCausation.Status = 3 AndAlso medicalFeesCausation.InvoiceReversal = True Then 'Cambia el estado de la variable para la factura
                '                medicalFeesCausation.ReassessmentForReversal = True
                '            ElseIf MedicalFeesLiquidation.Status = 2 AndAlso medicalFeesCausation.Status = 3 AndAlso medicalFeesCausation.ObjectionAccepted = True Then 'Cambia el estado de la variable para glosas
                '                medicalFeesCausation.ReassessmentForObjection = True
                '            End If

                '            'Se modifican los estados en la causacion
                '            If medicalFeesCausation.Status <> 3 Then
                '                If MedicalFeesLiquidation.Status = 1 Then
                '                    medicalFeesCausation.Status = 2
                '                Else
                '                    medicalFeesCausation.Status = 3
                '                End If
                '            End If
                '        Else
                '            medicalFeesCausation.Status = 4
                '        End If
                '        medicalFeesCausation.MarkAsModified()
                '        _medicalFeesCausationRepository.SaveEntity(medicalFeesCausation)
                '        unitOfWorkCausation.Commit()
                '    Next
                'End If


                ''Se consulta el contrato o el medico para para cambiarle la fecha de ultima liquidacion y el temporal de la fecha de ultima liquidacion
                'If MedicalFeesLiquidation.ChangeTracker.State = ObjectState.Added OrElse MedicalFeesLiquidation.Status = 2 OrElse MedicalFeesLiquidation.Status = 3 Then
                '    If MedicalFeesLiquidation.HealthProfessionalCode IsNot Nothing Then 'Si el código del médico no viene vací se hace los cambios en la tabla INPROFSAL
                '        'Se consulta el medico con el codigo que viene de la tabla MedicalFeesLiquidation
                '        Dim healthProfessional As INPROFSAL = _healthProfessionalRepository.GetHealthProfessionalByCode(MedicalFeesLiquidation.HealthProfessionalCode)
                '        Select Case MedicalFeesLiquidation.Status
                '            Case 1
                '                healthProfessional.FECULTLIQTMP = MedicalFeesLiquidation.EndDate
                '            Case 2
                '                healthProfessional.FECULTLIQTMP = Nothing
                '                healthProfessional.FECULTLIQ = MedicalFeesLiquidation.EndDate
                '            Case 3
                '                healthProfessional.FECULTLIQTMP = Nothing
                '        End Select
                '        healthProfessional.MarkAsModified()
                '        _healthProfessionalRepository.SaveEntity(healthProfessional)
                '        unitOfWorkHealthProfessional.Commit()
                '    Else 'Si el código del médico viene vacíio se hace los cambios en la tabla MedicalFeesContract
                '        Dim medicalFeesContract As MedicalFeesContract = _medicalFeesContractRepository.GetMedicalFeesContractById(MedicalFeesLiquidation.MedicalFeesContractId)
                '        medicalFeesContract.StartTracking()
                '        Select Case MedicalFeesLiquidation.Status
                '            Case 1
                '                medicalFeesContract.LastLiquidationDateTmp = MedicalFeesLiquidation.EndDate
                '            Case 2
                '                medicalFeesContract.LastLiquidationDateTmp = Nothing
                '                medicalFeesContract.LastLiquidationDate = MedicalFeesLiquidation.EndDate
                '            Case 3
                '                medicalFeesContract.LastLiquidationDateTmp = Nothing
                '        End Select
                '        medicalFeesContract.MarkAsModified()
                '        _medicalFeesContractRepository.SaveEntity(medicalFeesContract)
                '        unitOfWorkContract.Commit()
                '    End If

                'End If


                'Me._medicalFeesLiquidationRepository.SaveEntity(MedicalFeesLiquidation)
                'unitOfWork.Commit()
                'sequenseUnitOfWork.Commit()

                Dim XmlObject = ConvertToXml(MedicalFeesLiquidation)
                Dim resultStore = _medicalFeesLiquidationRepository.SP_SaveMedicalFeesLiquidation(XmlObject, audit.CodeUser)
                If resultStore.CodeMessage <> 0 Then
                    transaction.Dispose()
                    Return New ActionResult(Of MedicalFeesLiquidation) With {.StateResult = False, .Message = resultStore.Message}
                End If

                MedicalFeesLiquidation.Id = resultStore.Id
                MedicalFeesLiquidation.Code = resultStore.CodeMedicalFeesLiquidation
                'auditProcess = New IndigoAuditSimpleEntity(Of MedicalFeesLiquidation)(MedicalFeesLiquidation, audit, status, auxMedicalFeesLiquidation)
                'auditProcess.Execute()

                'Se marca la entidad como sin cambios
                MedicalFeesLiquidation.MarkAsUnchanged()

                transaction.Complete()
                Return New ActionResult(Of MedicalFeesLiquidation) With {.StateResult = True, .ObjectEmbbeded = MedicalFeesLiquidation}
            Catch ex As OptimisticConcurrencyException
                'unitOfWorkCausation.RollbackChanges()
                'unitOfWorkContract.RollbackChanges()
                'unitOfWorkHealthProfessional.RollbackChanges()
                'unitOfWork.RollbackChanges()
                transaction.Dispose()
                Return New ActionResult(Of MedicalFeesLiquidation) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
            Catch ex As Exception
                'unitOfWorkCausation.RollbackChanges()
                'unitOfWorkContract.RollbackChanges()
                'unitOfWorkHealthProfessional.RollbackChanges()
                'unitOfWork.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of MedicalFeesLiquidation) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Convertir el objeto en xml
    ''' </summary>
    ''' <param name="MedicalFeesLiquidation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConvertToXml(MedicalFeesLiquidation As MedicalFeesLiquidation) As String
        Dim builder As StringBuilder = New StringBuilder()

        builder.Append("<MedicalFeesLiquidation>")

        'Se arma la cabecera
        builder.Append("<Id>" & MedicalFeesLiquidation.Id & "</Id>")
        builder.Append("<Code>" & MedicalFeesLiquidation.Code & "</Code>")
        builder.Append("<OperatingUnitId>" & MedicalFeesLiquidation.OperatingUnitId & "</OperatingUnitId>")
        builder.Append("<LiquidationType>" & MedicalFeesLiquidation.LiquidationType & "</LiquidationType>")
        If MedicalFeesLiquidation.MedicalFeesContractId IsNot Nothing Then
            builder.Append("<MedicalFeesContractId>" & MedicalFeesLiquidation.MedicalFeesContractId & "</MedicalFeesContractId>")
        Else
            builder.Append("<MedicalFeesContractId>" & 0 & "</MedicalFeesContractId>")
        End If
        If MedicalFeesLiquidation.HealthProfessionalCode IsNot Nothing Then
            builder.Append("<HealthProfessionalCode>" & MedicalFeesLiquidation.HealthProfessionalCode & "</HealthProfessionalCode>")
        Else
            builder.Append("<HealthProfessionalCode>" & 0 & "</HealthProfessionalCode>")
        End If
        builder.Append("<SupplierId>" & MedicalFeesLiquidation.SupplierId & "</SupplierId>")
        builder.Append("<SuppliersDistributionLineId>" & MedicalFeesLiquidation.SuppliersDistributionLineId & "</SuppliersDistributionLineId>")
        If MedicalFeesLiquidation.CostCenterId IsNot Nothing Then
            builder.Append("<CostCenterId>" & MedicalFeesLiquidation.CostCenterId & "</CostCenterId>")
        Else
            builder.Append("<CostCenterId>" & 0 & "</CostCenterId>")
        End If
        builder.Append("<InitialDate>" & MedicalFeesLiquidation.InitialDate.ToString("dd/MM/yyyy HH:mm:ss") & "</InitialDate>")
        builder.Append("<EndDate>" & MedicalFeesLiquidation.EndDate.ToString("dd/MM/yyyy HH:mm:ss") & "</EndDate>")
        builder.Append("<BillNumber>" & MedicalFeesLiquidation.BillNumber & "</BillNumber>")
        builder.Append("<DocumentDate>" & MedicalFeesLiquidation.DocumentDate.ToString("dd/MM/yyyy HH:mm:ss") & "</DocumentDate>")
        builder.Append("<FilingUnitId>" & MedicalFeesLiquidation.FilingUnitId & "</FilingUnitId>")
        builder.Append("<SupplierTypeId>" & MedicalFeesLiquidation.SupplierTypeId & "</SupplierTypeId>")
        If MedicalFeesLiquidation.AccountPayableId IsNot Nothing Then
            builder.Append("<AccountPayableId>" & MedicalFeesLiquidation.AccountPayableId & "</AccountPayableId>")
        Else
            builder.Append("<AccountPayableId>" & 0 & "</AccountPayableId>")
        End If
        builder.Append("<Status>" & MedicalFeesLiquidation.Status & "</Status>")

        'Se arma el detalle
        If MedicalFeesLiquidation.MedicalFeesLiquidationDetail IsNot Nothing AndAlso MedicalFeesLiquidation.MedicalFeesLiquidationDetail.Count > 0 Then
            For Each detail In MedicalFeesLiquidation.MedicalFeesLiquidationDetail
                builder.Append("<MedicalFeesLiquidationDetail>")
                builder.Append("<Id>" & detail.Id & "</Id>")
                builder.Append("<LiquidationType>" & detail.LiquidationType & "</LiquidationType>")
                builder.Append("<MedicalFeesLiquidacionId>" & detail.MedicalFeesLiquidacionId & "</MedicalFeesLiquidacionId>")
                builder.Append("<MedicalFeesCausationId>" & detail.MedicalFeesCausationId & "</MedicalFeesCausationId>")
                If detail.ChangeTracker.State = ObjectState.Added OrElse detail.ChangeTracker.State = ObjectState.Unchanged Then
                    builder.Append("<ChangeTracker>" & 1 & "</ChangeTracker>")
                ElseIf detail.ChangeTracker.State = ObjectState.Deleted Then
                    builder.Append("<ChangeTracker>" & 3 & "</ChangeTracker>")
                End If
                If detail.ChangeStatusMedicalFeesCausation = True AndAlso detail.ChangeTracker.State = ObjectState.Deleted Then
                    builder.Append("<ChangeStatusMedicalFeesCausation>" & 1 & "</ChangeStatusMedicalFeesCausation>")
                Else
                    builder.Append("<ChangeStatusMedicalFeesCausation>" & 0 & "</ChangeStatusMedicalFeesCausation>")
                End If
                builder.Append("</MedicalFeesLiquidationDetail>")
            Next
        End If

        builder.Append("</MedicalFeesLiquidation>")

        Return builder.ToString()
    End Function

    ''' <summary>
    ''' Valida que la cuenta contable que viene amrrada a la linea de distribucion maneje centro costo
    ''' </summary>
    ''' <param name="supplierDistributionLineId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateCostCenterBySupplierDistributionLineId(supplierDistributionLineId As Integer) As ActionResult(Of MedicalFeesLiquidation) Implements IMedicalFeesLiquidationAdminService.ValidateCostCenterBySupplierDistributionLineId
        If supplierDistributionLineId = 0 Then
            Throw New ArgumentNullException("supplierDistributionLineId")
        End If
        Try
            Dim handlesCostCenter As Boolean = Me._medicalFeesLiquidationRepository.ValidateCostCenterBySupplierDistributionLineId(supplierDistributionLineId)
            Return New ActionResult(Of MedicalFeesLiquidation) With {.StateResult = True, .StateResultAux = handlesCostCenter}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of MedicalFeesLiquidation) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _accountPayableAdminService.Dispose()
            End If
            _medicalFeesLiquidationRepository = Nothing
            _secuenseDRepository = Nothing
            _accountPayableAdminService = Nothing
            _supplierRepository = Nothing
            _medicalFeesContractRepository = Nothing
            _healthProfessionalRepository = Nothing
            _patientRepository = Nothing
            _accountPayableRepository = Nothing
            _supplierDistributionLineRepository = Nothing
            _distributionLineRepository = Nothing
            _settingsMedicalFeesRepository = Nothing
            _sequenseCPaymentsRepository = Nothing
            _medicalFeesCausationRepository = Nothing
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
