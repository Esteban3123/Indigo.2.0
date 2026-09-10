'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 25/09/2014
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
Imports Infrastructure.CrossCutting.Queue
Imports System.Security

Public Class CupsEntityAdminService
    Implements ICupsEntityAdminService

#Region "Variables"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _cupsEntityRepository As ICupsEntityRepository


    ''' <summary>
    ''' Variable tipo repositorio para dependencia CupsHomologation
    ''' </summary>
    ''' <remarks></remarks>
    Private _cupsCupsHomologationRepository As ICupsHomologationRepository

    ''' <summary>
    ''' Variable tipo repositorio para dependencia CupsEntityContractDescriptions
    ''' </summary>
    ''' <remarks></remarks>
    Private _cupsEntityContractDescriptionsRepository As ICupsEntityContractDescriptionsRepository

    ''' <summary>
    ''' Variable tipo repositorio para dependencia ContractPackageService
    ''' </summary>
    ''' <remarks></remarks>
    Private _cupsContractPackageServiceRepository As IContractPackageServiceRepository

    ''' <summary>
    ''' Fabrica de Indiigo Queue
    ''' </summary>
    Private _factoryQueue As IFactoryQueue

#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal cupsEntityRepository As ICupsEntityRepository, ByVal cupsCupsHomologationRepository As ICupsHomologationRepository, ByVal cupsEntityContractDescriptionsRepository As ICupsEntityContractDescriptionsRepository,
                   ByVal cupsContractPackageServiceRepository As IContractPackageServiceRepository, ByVal FactoryQueue As IFactoryQueue)
        If cupsEntityRepository Is Nothing Then
            Throw New ArgumentNullException("cupsEntityRepository Vacio")
        End If
        _cupsEntityRepository = cupsEntityRepository
        _cupsCupsHomologationRepository = cupsCupsHomologationRepository
        _cupsEntityContractDescriptionsRepository = cupsEntityContractDescriptionsRepository
        _cupsContractPackageServiceRepository = cupsContractPackageServiceRepository
        _factoryQueue = FactoryQueue
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
    Public Function ChangeStateCupsEntity(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of CUPSEntity) Implements ICupsEntityAdminService.ChangeStateCupsEntity
        Dim CupsEntity As CUPSEntity = _cupsEntityRepository.GetCupsEntity(code)
        CupsEntity.Status = state
        Return SaveCupsEntity(CupsEntity, audit)
    End Function

    ''' <summary>
    ''' Elimina una entidad cups
    ''' </summary>
    ''' <param name="CupsEntity"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteCupsEntity(CupsEntity As CUPSEntity, audit As AuditMessage) As ActionResult Implements ICupsEntityAdminService.DeleteCupsEntity
        If CupsEntity Is Nothing Then
            Throw New ArgumentNullException("CupsEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._cupsEntityRepository.UnitWork
        Dim unitOfWorkCupsHomologation As IUnitWork = Me._cupsCupsHomologationRepository.UnitWork
        Dim unitOfWorkCUPSEntityContractDescriptions As IUnitWork = Me._cupsEntityContractDescriptionsRepository.UnitWork
        Dim unitOfWorkContractPackageServiceRepository As IUnitWork = Me._cupsContractPackageServiceRepository.UnitWork

        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of CUPSEntity)
            auditProcess = New IndigoAuditSimpleEntity(Of CUPSEntity)(CupsEntity, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)

            For Each item In CupsEntity.CupsHomologation
                _cupsCupsHomologationRepository.DeleteEntity(item)
                unitOfWorkCupsHomologation.Commit()
            Next

            For Each item In CupsEntity.CUPSEntityContractDescriptions
                _cupsEntityContractDescriptionsRepository.DeleteEntity(item)
                unitOfWorkCUPSEntityContractDescriptions.Commit()
            Next

            For Each item In CupsEntity.ContractPackageService
                _cupsContractPackageServiceRepository.DeleteEntity(item)
                unitOfWorkContractPackageServiceRepository.Commit()
            Next

            Me._cupsEntityRepository.DeleteEntity(CupsEntity)
            unitOfWork.Commit()

            CupsEntity.MarkAsDeleted()
            TriggerEvent(CupsEntity, audit)

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
    Public Function GetCupsEntity(code As String, audit As AuditMessage) As ActionResult(Of CUPSEntity) Implements ICupsEntityAdminService.GetCupsEntity
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim CupsEntity As CUPSEntity = Me._cupsEntityRepository.GetCupsEntity(code.Trim())
            If CupsEntity IsNot Nothing AndAlso CupsEntity.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of CUPSEntity)(CupsEntity, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of CUPSEntity) With {.StateResult = True, .ObjectEmbbeded = CupsEntity}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CUPSEntity) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una entidad cups por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCupsEntityById(id As Integer, audit As AuditMessage) As ActionResult(Of CUPSEntity) Implements ICupsEntityAdminService.GetCupsEntityById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim CupsEntity As CUPSEntity = Me._cupsEntityRepository.GetCupsEntityById(id)
            If CupsEntity IsNot Nothing AndAlso CupsEntity.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of CUPSEntity)(CupsEntity, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of CUPSEntity) With {.StateResult = True, .ObjectEmbbeded = CupsEntity}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CUPSEntity) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza una entidad cups
    ''' </summary>
    ''' <param name="CupsEntity"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveCupsEntity(CupsEntity As CUPSEntity, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of CUPSEntity) Implements ICupsEntityAdminService.SaveCupsEntity
        If CupsEntity Is Nothing Then
            Throw New ArgumentNullException("CupsEntity")
        End If

        Dim unitOfWork As IUnitWork = Me._cupsEntityRepository.UnitWork
        Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                Dim auxCupsEntity As CUPSEntity = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of CUPSEntity)
                Dim status As Integer

                If CupsEntity.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    auxCupsEntity = CupsEntity.OriginalValue
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                If CupsEntity.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    CupsEntity.CreationUser = audit.CodeUser
                    CupsEntity.CreationDate = DateTime.Now
                    CupsEntity.ModificationUser = audit.CodeUser
                    CupsEntity.ModificationDate = DateTime.Now
                Else
                    CupsEntity.ModificationUser = audit.CodeUser
                    CupsEntity.ModificationDate = DateTime.Now
                End If

                TriggerEvent(CupsEntity, audit)

                'Se marca la entidad como sin cambios
                CupsEntity.MarkAsUnchanged()

                'Se convierte la entidad a xml
                Dim xml = ConvertEntityToXml(CupsEntity)

                'Se consume el sp creado para guardar
                Dim result = _cupsEntityRepository.SP_SaveCupsEntity(xml, audit.CodeUser)
                If result.CodeMessage <> 0 Then
                    scope.Dispose()
                    Return New ActionResult(Of CUPSEntity) With {.StateResult = False, .Message = result.Message, .ObjectEmbbeded = CupsEntity}
                End If

                auditProcess = New IndigoAuditSimpleEntity(Of CUPSEntity)(CupsEntity, audit, status, auxCupsEntity)
                auditProcess.Execute()

                scope.Complete()
                Return New ActionResult(Of CUPSEntity) With {.StateResult = True, .ObjectEmbbeded = CupsEntity}
            Catch ex As OptimisticConcurrencyException
                scope.Dispose()
                unitOfWork.RollbackChanges()
                Return New ActionResult(Of CUPSEntity) With {.StateResult = False, .Message = "-999"}
            Catch ex As DbUpdateException
                scope.Dispose()
                unitOfWork.RollbackChanges()
                Dim message As String = ""
                If ex.InnerException IsNot Nothing Then
                    message = message = ex.InnerException.InnerException.Message
                End If
                Return New ActionResult(Of CUPSEntity) With {.StateResult = False, .Message = "Revisar el Trigger de la Tabla. Error del Sistema: " + message}
            Catch ex As Exception
                scope.Dispose()
                unitOfWork.RollbackChanges()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of CUPSEntity) With {.StateResult = False, .Message = ex.Message}
            End Try
        End Using
    End Function
    ''' <summary>
    ''' Función para devolver una cadena escapada para XML en caso de que vengan caracteres especiales no validos.
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Private Function EscapeXml(value As String) As String
        If value Is Nothing Then Return ""
        Return SecurityElement.Escape(value)
    End Function

    ''' <summary>
    ''' Metodo que convierte la entidad a xml
    ''' </summary>
    ''' <returns></returns>
    Private Function ConvertEntityToXml(CupsEntity As CUPSEntity) As String
        Dim builder As New StringBuilder
        builder.Append("<CupsEntity>")

        With CupsEntity
            builder.Append("<Id>" & .Id & "</Id>")
            builder.Append("<CUPSSubGroupId>" & .CUPSSubGroupId & "</CUPSSubGroupId>")
            builder.Append("<Code>" & .Code & "</Code>")
            builder.Append("<Description>" & EscapeXml(.Description) & "</Description>")
            builder.Append("<RIPSCode>" & .RIPSCode & "</RIPSCode>")
            builder.Append("<RIPSDescription>" & EscapeXml(.RIPSDescription) & "</RIPSDescription>")
            builder.Append("<RIPSConcept>" & .RIPSConcept & "</RIPSConcept>")
            builder.Append("<RIPSService>" & .RIPSServiceId & "</RIPSService>")
            builder.Append("<BillingConceptId>" & .BillingConceptId & "</BillingConceptId>")
            builder.Append("<BillingGroupId>" & .BillingGroupId & "</BillingGroupId>")
            builder.Append("<ServiceType>" & .ServiceType & "</ServiceType>")
            builder.Append("<Status>" & .Status & "</Status>")
            builder.Append("<MinimunAgeUnit>" & .MinimunAgeUnit & "</MinimunAgeUnit>")
            builder.Append("<MinimunAge>" & .MinimunAge & "</MinimunAge>")
            builder.Append("<MaximumAgeUnit>" & .MaximumAgeUnit & "</MaximumAgeUnit>")
            builder.Append("<MaximumAge>" & .MaximumAge & "</MaximumAge>")
            builder.Append("<Sex>" & .Sex & "</Sex>")
            builder.Append("<ShowServiceMedicalOrder>" & .ShowServiceMedicalOrder & "</ShowServiceMedicalOrder>")
            builder.Append("<ShowDashboardOf>" & .ShowDashboardOf & "</ShowDashboardOf>")
            builder.Append("<ShowDashboardOfAmbulatory>" & .ShowDashboardOfAmbulatory & "</ShowDashboardOfAmbulatory>")
            builder.Append("<TherapyProcedure>" & .TherapyProcedure & "</TherapyProcedure>")
            builder.Append("<AllowDiligenceInPlace>" & .AllowDiligenceInPlace & "</AllowDiligenceInPlace>")
            builder.Append("<AllowDiligenceReportRealizationQx>" & .AllowDiligenceReportRealizationQx & "</AllowDiligenceReportRealizationQx>")
            builder.Append("<SerialService>" & .SerialService & "</SerialService>")
            builder.Append("<RequiresInterpretation>" & .RequiresInterpretation & "</RequiresInterpretation>")
            builder.Append("<RequiresConfirmationRealization>" & .RequiresConfirmationRealization & "</RequiresConfirmationRealization>")
            builder.Append("<NutritionConsultation>" & .NutritionConsultation & "</NutritionConsultation>")
            builder.Append("<PsychologyConsultation>" & .PsychologyConsultation & "</PsychologyConsultation>")
            builder.Append("<YoungFirstTimeConsultation>" & .YoungFirstTimeConsultation & "</YoungFirstTimeConsultation>")
            builder.Append("<AdultFirstTimeConsultation>" & .AdultFirstTimeConsultation & "</AdultFirstTimeConsultation>")
            builder.Append("<AdvisoryPreTestElsaVIH>" & .AdvisoryPreTestElsaVIH & "</AdvisoryPreTestElsaVIH>")
            builder.Append("<AdvisoryPosTestElsaVIH>" & .AdvisoryPosTestElsaVIH & "</AdvisoryPosTestElsaVIH>")
            builder.Append("<NeonatalTSH>" & .NeonatalTSH & "</NeonatalTSH>")
            builder.Append("<SurfaceAntigen>" & .SurfaceAntigen & "</SurfaceAntigen>")
            builder.Append("<SerologySyphilis>" & .SerologySyphilis & "</SerologySyphilis>")
            builder.Append("<ElisaVIH>" & .ElisaVIH & "</ElisaVIH>")
            builder.Append("<Hemoglobin>" & .Hemoglobin & "</Hemoglobin>")
            builder.Append("<Creatine>" & .Creatine & "</Creatine>")
            builder.Append("<GlycosylatedHemoglobin>" & .GlycosylatedHemoglobin & "</GlycosylatedHemoglobin>")
            builder.Append("<Microalbuminuria>" & .Microalbuminuria & "</Microalbuminuria>")
            builder.Append("<HDL>" & .HDL & "</HDL>")
            builder.Append("<DiagnosticSmearMicroscopy>" & .DiagnosticSmearMicroscopy & "</DiagnosticSmearMicroscopy>")
            builder.Append("<PrenatalControlFirstTime>" & .PrenatalControlFirstTime & "</PrenatalControlFirstTime>")
            builder.Append("<PrenatalControl>" & .PrenatalControl & "</PrenatalControl>")
            builder.Append("<VisualAcuityAssessment>" & .VisualAcuityAssessment & "</VisualAcuityAssessment>")
            builder.Append("<OphthalmologyConsultation>" & .OphthalmologyConsultation & "</OphthalmologyConsultation>")
            builder.Append("<GrowthDevelopmentFirstTimeConsultation>" & .GrowthDevelopmentFirstTimeConsultation & "</GrowthDevelopmentFirstTimeConsultation>")
            builder.Append("<FamilyPlanningFirstTime>" & .FamilyPlanningFirstTime & "</FamilyPlanningFirstTime>")
            builder.Append("<Mammography>" & .Mammography & "</Mammography>")
            builder.Append("<CervicalBiopsy>" & .CervicalBiopsy & "</CervicalBiopsy>")
            builder.Append("<BreastBiopsyBacaf>" & .BreastBiopsyBacaf & "</BreastBiopsyBacaf>")
            builder.Append("<BasalGlycaemia>" & .BasalGlycaemia & "</BasalGlycaemia>")
            builder.Append("<Creatinuria>" & .Creatinuria & "</Creatinuria>")
            builder.Append("<TotalCholesterol>" & .TotalCholesterol & "</TotalCholesterol>")
            builder.Append("<LDL>" & .LDL & "</LDL>")
            builder.Append("<PTH>" & .PTH & "</PTH>")
            builder.Append("<SerineAlbumin>" & .SerineAlbumin & "</SerineAlbumin>")
            builder.Append("<PhosphorusAlbumin>" & .PhosphorusAlbumin & "</PhosphorusAlbumin>")
            builder.Append("<ApplyRIAS>" & .ApplyRIAS & "</ApplyRIAS>")
            builder.Append("<OxigenService>" & .OxigenService & "</OxigenService>")
            builder.Append("<FinancedResourceUPC>" & .FinancedResourceUPC & "</FinancedResourceUPC>")
            builder.Append("<RequestRoomAutomatically>" & .RequestRoomAutomatically & "</RequestRoomAutomatically>")
            builder.Append("<SurgicalReport>" & .SurgicalReport & "</SurgicalReport>")
            If .ApplyRIAS Then
                builder.Append("<RIASBillingConceptId>" & .RIASBillingConceptId & "</RIASBillingConceptId>")
                builder.Append("<RIASBillingGroupId>" & .RIASBillingGroupId & "</RIASBillingGroupId>")
            End If
            builder.Append($"<IsPanel>{ .IsPanel}</IsPanel>")
            builder.Append($"<RequiresLaterality>{ .RequiresLaterality}</RequiresLaterality>")
            builder.Append($"<ImageGuidanceProcedure>{ .ImageGuidanceProcedure}</ImageGuidanceProcedure>")

            'Id temporal para manejar los detalles
            Dim TempId As Integer = 1

            'Se arma el xml con los detalles de RIAS
            If .ListCupsEntityRIAS IsNot Nothing AndAlso .ListCupsEntityRIAS.Count > 0 Then
                For Each itemRIAS In .ListCupsEntityRIAS
                    builder.Append("<CupsEntityRIAS>")

                    builder.Append("<TempId>" & TempId & "</TempId>")
                    builder.Append("<Id>" & itemRIAS.Id & "</Id>")
                    builder.Append("<CupsCode>" & .Code & "</CupsCode>")
                    builder.Append("<RiasId>" & itemRIAS.RiasId & "</RiasId>")
                    builder.Append("<RiasDescription>" & EscapeXml(itemRIAS.RiasDescription) & "</RiasDescription>")
                    builder.Append("<ConceptRIPS>" & itemRIAS.ConceptRIPS & "</ConceptRIPS>")
                    builder.Append("<IsDelete>" & itemRIAS.IsDelete & "</IsDelete>")

                    'Si el RIAS tiene reglas
                    If itemRIAS.ListCupsEntityRIASDetail IsNot Nothing AndAlso itemRIAS.ListCupsEntityRIASDetail.Count > 0 Then
                        For Each itemRules In itemRIAS.ListCupsEntityRIASDetail
                            builder.Append("<CupsEntityRIASDetail>")

                            builder.Append("<TempId>" & TempId & "</TempId>")
                            builder.Append("<Id>" & itemRules.Id & "</Id>")
                            builder.Append("<CupsEntityRIASId>" & itemRules.CupsEntityRIASId & "</CupsEntityRIASId>")
                            builder.Append("<Rule>" & itemRules.Rule & "</Rule>")
                            builder.Append("<MinimunAge>" & itemRules.MinimunAge & "</MinimunAge>")
                            builder.Append("<MaximunAge>" & itemRules.MaximunAge & "</MaximunAge>")
                            builder.Append("<Unit>" & itemRules.Unit & "</Unit>")
                            builder.Append("<Frequency>" & itemRules.Frequency & "</Frequency>")
                            builder.Append("<FrequencyUnit>" & itemRules.FrequencyUnit & "</FrequencyUnit>")
                            builder.Append("<RequireMedicalOrder>" & itemRules.RequireMedicalOrder & "</RequireMedicalOrder>")
                            builder.Append("<IsDelete>" & itemRules.IsDelete & "</IsDelete>")
                            builder.Append("<PeriodQuantity>" & itemRules.PeriodQuantity & "</PeriodQuantity>")
                            builder.Append("<Sex>" & itemRules.Sex & "</Sex>")

                            builder.Append("<Status>" & itemRules.Status & "</Status>")
                            builder.Append("<OxigenService>" & .OxigenService & "</OxigenService>")
                            builder.Append("</CupsEntityRIASDetail>")
                        Next
                    End If

                    TempId += 1
                    builder.Append("</CupsEntityRIAS>")
                Next
            End If

            If .CUPSEntityContractDescriptions IsNot Nothing AndAlso .CUPSEntityContractDescriptions.Count > 0 Then
                For Each item In .CUPSEntityContractDescriptions
                    builder.Append("<CUPSEntityContractDescriptions>")
                    builder.Append("<Id>" & item.Id & "</Id>")
                    builder.Append("<CUPSEntityId>" & item.CUPSEntityId & "</CUPSEntityId>")
                    builder.Append("<ContractDescriptionId>" & EscapeXml(item.ContractDescriptionId) & "</ContractDescriptionId>")
                    builder.Append("<CupsSubgroupId>" & item.CupsSubgroupId & "</CupsSubgroupId>")
                    builder.Append("<BillingGroupId>" & item.BillingGroupId & "</BillingGroupId>")
                    builder.Append("<BillingConceptId>" & item.BillingConceptId & "</BillingConceptId>")
                    builder.Append("<IsDelete>" & item.IsDelete & "</IsDelete>")
                    If item.ChangeTracker.State = ObjectState.Deleted AndAlso item.IsDelete = 0 Then
                        builder.Append("<ItemDelete>" & 1 & "</ItemDelete>")
                    Else
                        builder.Append("<ItemDelete>" & 0 & "</ItemDelete>")
                    End If
                    builder.Append("</CUPSEntityContractDescriptions>")
                Next
            End If

            If .CUPSEntityPanelDetail IsNot Nothing AndAlso .CUPSEntityPanelDetail.Any() Then
                For Each item In .CUPSEntityPanelDetail
                    builder.Append($"<CUPSEntityPanelDetail>")
                    builder.Append($"<Id>{item.Id}</Id>")
                    builder.Append($"<CUPSEntityPanelId>{item.CUPSEntityPanelId}</CUPSEntityPanelId>")
                    builder.Append($"<CUPSEntityId>{item.CUPSEntityId}</CUPSEntityId>")
                    builder.Append($"<IsDelete>{item.ChangeTracker.State = ObjectState.Deleted}</IsDelete>")
                    builder.Append($"</CUPSEntityPanelDetail>")
                Next
            End If
        End With

        builder.Append("</CupsEntity>")
        Return builder.ToString()
    End Function

    ''' <summary>
    ''' Valida la descripción antes de eliminarse
    ''' </summary>
    ''' <param name="CUPSEntityContractDescriptionId"></param>
    ''' <returns></returns>
    Public Function SP_ValidateDescriptionsInCrystal(CUPSEntityContractDescriptionId As Integer) As ActionResult(Of SP_ValidateDescriptionsInCrystal_Result) Implements ICupsEntityAdminService.SP_ValidateDescriptionsInCrystal
        If CUPSEntityContractDescriptionId = Nothing Then
            Throw New ArgumentNullException("CUPSEntityContractDescriptionId")
        End If
        Try
            Dim result = _cupsEntityRepository.SP_ValidateDescriptionsInCrystal(CUPSEntityContractDescriptionId)
            If result.CodeValidation = 999 Then
                Return New ActionResult(Of SP_ValidateDescriptionsInCrystal_Result) With {.StateResult = False, .Message = result.MessageValidation}
            End If

            Return New ActionResult(Of SP_ValidateDescriptionsInCrystal_Result) With {.StateResult = True, .ObjectEmbbeded = result, .Message = result.MessageValidation}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SP_ValidateDescriptionsInCrystal_Result) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Valida el cups cuando se agrega una descripción
    ''' </summary>
    ''' <param name="CUPSEntityCode"></param>
    ''' <returns></returns>
    Public Function SP_ValidateCUPSInCrystal(CUPSEntityCode As String) As ActionResult(Of SP_ValidateCUPSInCrystal_Result) Implements ICupsEntityAdminService.SP_ValidateCUPSInCrystal
        If String.IsNullOrEmpty(CUPSEntityCode) Then
            Throw New ArgumentNullException("CUPSEntityCode")
        End If
        Try
            Dim result = _cupsEntityRepository.SP_ValidateCUPSInCrystal(CUPSEntityCode)
            If result.CodeValidation <> 0 Then
                Return New ActionResult(Of SP_ValidateCUPSInCrystal_Result) With {.StateResult = False, .Message = result.MessageValidation}
            End If

            Return New ActionResult(Of SP_ValidateCUPSInCrystal_Result) With {.StateResult = True, .ObjectEmbbeded = result, .Message = result.MessageValidation}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SP_ValidateCUPSInCrystal_Result) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

#Region "Events"

    Public Sub TriggerEvent(CupsEntity As CUPSEntity, audit As AuditMessage)
        Dim wrapperEvent As New Events.Serializers.Wrapper
        Dim ChangeTracker As String = CupsEntity.ChangeTracker.State.ToString().ToLower()

        Dim eventData As EventData = wrapperEvent.GenerateWrapperEventData(CupsEntity, audit.CodeUser, ChangeTracker, DittoSourceType.cUPSEntity)
        Dim Queue As IIndigoQueue = _factoryQueue.CreateQueue()
        Queue.Publish(eventData)
    End Sub

#End Region

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _cupsEntityRepository = Nothing
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
