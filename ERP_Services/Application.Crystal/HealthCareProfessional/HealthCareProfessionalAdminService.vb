'***********************************************************************
' Assembly         : Application.Crystal
' Author           : J. Kevin Garay 
' Created          : 11-02-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Data.Entity.Core
Imports System.Data.Entity.Infrastructure
Imports System.Dynamic
Imports System.Transactions
Imports Application.Common
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Crystal
Imports Domain.Crystal.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources
#End Region
Public Class HealthCareProfessionalAdminService
    Implements IHealthCareProfessionalAdminService

#Region "Fields"

    ''' <summary>
    ''' Nombre del modulo
    ''' </summary>
    ''' <remarks></remarks>
    Private Const NAMEMODULE As String = "Crystal"

    ''' <summary>
    ''' Repositorio de estancias
    ''' </summary>
    Private _healthCareProfessional As IHealthCareProfessionalRepository

    ''' <summary>
    ''' Repositorio de contrato de profesional
    ''' </summary>
    ''' <remarks></remarks>
    Private _medicalFeesContractRepository As IMedicalFeesContractRepository

    ''' <summary>
    ''' Repositorio de lineas de distribución
    ''' </summary>
    ''' <remarks></remarks>
    Private _distributionLinesRepository As ISuppliersDistributionLinesRepository

    ''' <summary>
    ''' Repositorio de detalles de contratos del medico
    ''' </summary>
    ''' <remarks></remarks>
    Private _healthProfessionalContractRepository As IHealthProfessionalContractRepository

    ''' <summary>
    ''' Repositrorio Profesionales de la Salud ERP (Interno y Externo)
    ''' </summary>
    Private _healthProfessionalRepository As ICommonHealthProfessionalRepository

    ''' <summary>
    ''' Repositorio de Persona
    ''' </summary>
    Private _personRepository As IPersonRepository

    ''' <summary>
    ''' servicios de terceros
    ''' </summary>
    Private _thirdPartyAdminService As IThirdPartyAdminService
#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(healthCareProfessional As IHealthCareProfessionalRepository, medicalFeesContractRepository As IMedicalFeesContractRepository,
                   distributionLinesRepository As ISuppliersDistributionLinesRepository, healthProfessionalContractRepository As IHealthProfessionalContractRepository,
                   commonHealthProfessionalRepository As ICommonHealthProfessionalRepository, personRepository As IPersonRepository,
                   thirdPartyAdminService As IThirdPartyAdminService)
        Me._healthCareProfessional = healthCareProfessional
        Me._medicalFeesContractRepository = medicalFeesContractRepository
        Me._distributionLinesRepository = distributionLinesRepository
        Me._healthProfessionalContractRepository = healthProfessionalContractRepository
        Me._healthProfessionalRepository = commonHealthProfessionalRepository
        Me._personRepository = personRepository
        Me._thirdPartyAdminService = thirdPartyAdminService
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtener Profesional Por Código
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetHealthProfessionalByCode(Code As String) As ActionResult(Of HealthProfessionalModel) Implements IHealthCareProfessionalAdminService.GetHealthProfessionalByCode
        Try
            Dim professionalERP As HealthProfessional
            Dim resultProfesionalEHR = Me.GetProfessionalByCode(Code)

            If resultProfesionalEHR Is Nothing OrElse Not resultProfesionalEHR.StateResult Then
                Throw New ArgumentNullException(NameOf(resultProfesionalEHR), resultProfesionalEHR?.Message)
            End If

            Dim ProfesionalEHR As INPROFSAL = resultProfesionalEHR?.ObjectEmbbeded
            Dim identificationNumber As String = If(String.IsNullOrEmpty(ProfesionalEHR?.CODIGONIT), Code, ProfesionalEHR?.CODIGONIT)
            professionalERP = Me._healthProfessionalRepository.GetHealthProfessionalByCode(identificationNumber)

            If professionalERP.Id = 0 AndAlso Not String.IsNullOrEmpty(ProfesionalEHR?.CODPROSAL) Then

                Dim person = Me._personRepository.FirstOrDefault(Function(x) x.IdentificationNumber = ProfesionalEHR.CODIGONIT, False, {"ADTIPOIDENTIFICA"})
                If person Is Nothing Then
                    Return New ActionResult(Of HealthProfessionalModel) With {.StateResult = False, .Message = $"No se encontró la persona con el numero de identificación: {ProfesionalEHR.CODIGONIT}"}
                End If

                With professionalERP
                    .IdentificationNumber = ProfesionalEHR.CODIGONIT
                    .IdentificationTypeId = person.IdentificationTypeId
                    .IdentificationTypeName = person.ADTIPOIDENTIFICA.NOMBRE
                    .FirstName = ProfesionalEHR.MEDPRINOM
                    .SecondName = ProfesionalEHR.MEDSEGNOM
                    .FirstLastName = ProfesionalEHR.MEDPRIAPEL
                    .SecondLastName = ProfesionalEHR.MEDSEGAPEL
                    .ProfessionalSpecialty = ProfesionalEHR.CODESPEC1
                    .ProfessionalSpecialtyCodeName = ProfesionalEHR.Specialty1Description
                    .ExternalProfessional = False
                    .ProfessionalLicenseNumber = ProfesionalEHR.TARJETAPR
                    .CreationDate = DateTime.Now
                    .CreationUser = ProfesionalEHR.CODUSUARI
                    .Status = If(ProfesionalEHR.ESTADOMED = 1, True, False)
                End With

                Return New ActionResult(Of HealthProfessionalModel) With {.StateResult = True, .ObjectEmbbeded = New HealthProfessionalModel With {.HealthProfessional = professionalERP, .INPROFSAL = ProfesionalEHR}}
            End If

            Dim inespecia = _healthCareProfessional.GetSpecialityByCode(professionalERP.ProfessionalSpecialty)
            professionalERP.ProfessionalSpecialtyCodeName = $"{inespecia?.CODESPECI} - {inespecia?.DESESPECI}"

            Return New ActionResult(Of HealthProfessionalModel) With {.StateResult = True, .ObjectEmbbeded = New HealthProfessionalModel With {.HealthProfessional = professionalERP, .INPROFSAL = If(professionalERP.ExternalProfessional, Nothing, ProfesionalEHR)}}
        Catch ex As OptimisticConcurrencyException
            Return New ActionResult(Of HealthProfessionalModel) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorConcurrence")}
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of HealthProfessionalModel) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function


    ''' <summary>
    ''' Obtener Profesional Por Código
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetProfessionalByCode(Code As String) As ActionResult(Of INPROFSAL) Implements IHealthCareProfessionalAdminService.GetProfessionalByCode
        Try
            Dim profesional = Me._healthCareProfessional.GetProfessionalByCode(Code)
            If profesional IsNot Nothing AndAlso profesional.CODPROSAL IsNot Nothing Then
                If profesional.GENLINDIST Then
                    Dim _DistributionLine = Me._distributionLinesRepository.GetSuppliersDistributionLinesById(profesional.GENLINDIST, False)
                    If _DistributionLine IsNot Nothing AndAlso _DistributionLine.Id > 0 Then
                        profesional.SupplierDistributionLineDesc = _DistributionLine.Supplier.Code & " - " & _DistributionLine.Supplier.Name & " - " & _DistributionLine.DistributionLines.Code & " - " & _DistributionLine.DistributionLines.Name
                    End If
                End If
            End If
            Return New ActionResult(Of INPROFSAL) With {.StateResult = True, .ObjectEmbbeded = profesional}
        Catch ex As OptimisticConcurrencyException
            Return New ActionResult(Of INPROFSAL) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorConcurrence")}
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of INPROFSAL) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Obtener Especialidad Por Código
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSpecialityByCode(Code As String) As ActionResult(Of INESPECIA) Implements IHealthCareProfessionalAdminService.GetSpecialityByCode
        Try
            Return New ActionResult(Of INESPECIA) With {.StateResult = True, .ObjectEmbbeded = Me._healthCareProfessional.GetSpecialityByCode(Code)}
        Catch ex As OptimisticConcurrencyException
            Return New ActionResult(Of INESPECIA) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorConcurrence")}
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of INESPECIA) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Elimina Profesionales
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteProfessional(Code As String) As ActionResult Implements IHealthCareProfessionalAdminService.DeleteProfessional
        Dim result As New ActionResult
        result.StateResult = True
        If Code Is String.Empty Then
            Throw New ArgumentNullException("Code")
        End If

        Dim UnitOfWork As IUnitWork = Me._healthCareProfessional.UnitWork
        Dim unitOfWorkHealthProfessionalContract As IUnitWork = Me._healthProfessionalContractRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try

                Dim entityDelete As INPROFSAL = Me._healthCareProfessional.GetProfessionalByCode(Code)
                Dim IdentificationNumber = If(String.IsNullOrEmpty(entityDelete?.CODIGONIT), Code, entityDelete?.CODIGONIT)
                Dim professionalERP = Me._healthProfessionalRepository.FirstOrDefault(Function(x) x.IdentificationNumber = IdentificationNumber)

                If professionalERP Is Nothing AndAlso String.IsNullOrEmpty(entityDelete?.CODPROSAL?.ToString?.Trim()) Then
                    transaction.Dispose()
                    result.StateResult = False
                    result.Message = String.Format(ResourceManager.GetString("DontExistProfessional", NAMEMODULE), Code)
                    Return result
                End If

                'Se consulta el listado de contratos que tiene asociado el medico
                Dim ListHealthProfessionalContract As List(Of HealthProfessionalContract) = _healthProfessionalContractRepository.GetListHealthProfessionalContractByHealthProfessionalCode(Code)
                If ListHealthProfessionalContract IsNot Nothing AndAlso ListHealthProfessionalContract.Count > 0 Then
                    For Each itemDelete As HealthProfessionalContract In ListHealthProfessionalContract
                        itemDelete.MarkAsDeleted()
                        _healthProfessionalContractRepository.DeleteEntity(itemDelete)
                        unitOfWorkHealthProfessionalContract.Commit()
                    Next
                End If

                If Not String.IsNullOrEmpty(entityDelete?.CODPROSAL?.ToString?.Trim()) AndAlso (professionalERP Is Nothing OrElse Not professionalERP.ExternalProfessional) Then
                    Me._healthCareProfessional.DeleteEntity(entityDelete)
                    UnitOfWork.Commit()
                End If

                If professionalERP IsNot Nothing Then
                    Me._healthProfessionalRepository.DeleteEntity(professionalERP)
                    Me._healthProfessionalRepository.UnitWork.Commit()
                End If

                transaction.Complete()
                Return result
            Catch ex As DbUpdateException
                UnitOfWork.RollbackChanges()
                unitOfWorkHealthProfessionalContract.RollbackChanges()
                unitOfWorkHealthProfessionalContract.RollbackChanges()
                result.StateResult = False
                result.Message = ResourceManager.GetString("ErrorDependence")
                Return result
            Catch ex As Exception
                UnitOfWork.RollbackChanges()
                unitOfWorkHealthProfessionalContract.RollbackChanges()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                result.StateResult = False
                result.Message = ResourceManager.GetString("ErrorUnknown")
                Return result
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Servicio que guarda y actualiza un profesional int o ext
    ''' </summary>
    ''' <param name="professional"></param>
    ''' <param name="ListHealthProfessionalContract"></param>
    ''' <param name="ListDeleteHealthProfessionalContract"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveHealthProfessional(professional As HealthProfessionalModel,
                                            ListHealthProfessionalContract As List(Of Domain.Entities.HealthProfessionalContract),
                                            ListDeleteHealthProfessionalContract As List(Of Domain.Entities.HealthProfessionalContract),
                                            audit As AuditMessage) As ActionResult(Of HealthProfessionalModel) Implements IHealthCareProfessionalAdminService.SaveHealthProfessional

        If professional Is Nothing Then
            Throw New ArgumentNullException(NameOf(professional))
        End If

        Dim unitOfWork As IUnitWork = Me._healthProfessionalRepository.UnitWork
        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted

        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim professionalERP = professional?.HealthProfessional
                Dim identificationTypeId As Integer? = Nothing

                If Not professionalERP.ExternalProfessional Then
                    Dim professionalEHR = professional.INPROFSAL
                    Dim resultInprof = Me.SaveProfessional(professionalEHR, ListHealthProfessionalContract, ListDeleteHealthProfessionalContract, audit)
                    If resultInprof Is Nothing OrElse Not resultInprof.StateResult Then
                        unitOfWork.RollbackChanges()
                        transaction.Dispose()
                        Return New ActionResult(Of HealthProfessionalModel) With {.StateResult = False, .Message = resultInprof?.Message}
                    End If

                    identificationTypeId = Me._personRepository.FirstOrDefault(Function(x) x.IdentificationNumber = professionalEHR.CODIGONIT)?.IdentificationTypeId
                    If identificationTypeId Is Nothing Then
                        Return New ActionResult(Of HealthProfessionalModel) With {.StateResult = False, .Message = $"No se encontró la persona con el numero de identificación: {professionalEHR.CODIGONIT}"}
                    End If
                    professionalERP.IdentificationTypeId = identificationTypeId
                    professionalERP.Status = If(professionalEHR.ESTADOMED = 1, True, False)
                End If

                If professionalERP.ChangeTracker.State = ObjectState.Added Then
                    professionalERP.CreationUser = audit.CodeUser
                    professionalERP.CreationDate = DateTime.Now
                    professionalERP.Status = If(professionalERP.ExternalProfessional, True, professionalERP.Status)
                Else
                    professionalERP.ModificationUser = audit?.CodeUser
                    professionalERP.ModificationDate = DateTime.Now
                End If

                Me._healthProfessionalRepository.SaveEntity(professionalERP)
                unitOfWork.Commit()

                If professionalERP.ExternalProfessional Then
                    Dim result = Me.CreateThirdPartyObject(professionalERP, audit)
                    If Not result.StateResult Then
                        Return New ActionResult(Of HealthProfessionalModel) With {.StateResult = result.StateResult, .Message = result.Message}
                    End If
                End If

                transaction.Complete()
                Return New ActionResult(Of HealthProfessionalModel) With {.StateResult = True, .Message = professionalERP.IdentificationNumber, .ObjectEmbbeded = New HealthProfessionalModel With {.HealthProfessional = professionalERP}}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                Throw ex
            End Try
        End Using
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function CreateThirdPartyObject(obj As HealthProfessional, audit As AuditMessage) As ActionResult(Of ThirdParty)

        If obj Is Nothing Then
            Throw New ArgumentNullException(NameOf(HealthProfessional), "No se logra crear el tercero ya que el objeto viene vacio")
        End If

        Dim thirdParty As ThirdParty
        thirdParty = _thirdPartyAdminService.GetThirdPartyByNit(obj.IdentificationNumber)
        Dim person As Person = _personRepository.FirstOrDefault(Function(x) x.IdentificationNumber = obj.IdentificationNumber)

        If person Is Nothing OrElse person.Id = 0 Then
            person = New Person()
            With person
                .IdentificationNumber = obj.IdentificationNumber
                .IdentificationType = 0
                .IdentificationTypeId = obj.IdentificationTypeId
                .FirstName = obj.FirstName
                .FirstLastName = obj.FirstLastName
                .SecondName = obj.SecondName
                .SecondLastName = obj.SecondLastName
                .State = True
            End With
        Else
            person.IdentificationTypeId = obj.IdentificationTypeId
        End If

        If thirdParty Is Nothing OrElse thirdParty.Id = 0 Then
            thirdParty = New ThirdParty()
            With thirdParty
                .Nit = obj.IdentificationNumber
                .PersonId = person.Id
                .Person = person
                .Name = $"{obj.FirstName} {obj.FirstName}"
                .PersonType = 1
                .RetentionType = 0
                .ContributionType = 0
                .StateEnterpriseType = 0
                .Ica = False
                .IcaTop = False
                .CreationDate = DateTime.Now
                .CreationUser = audit.CodeUser
                .UserId = audit.IdUser
                .HandlesBranchOffice = False
                .ElectronicBiller = False
                .State = True
            End With
        Else
            thirdParty.Person = person
        End If

        Dim result = _thirdPartyAdminService.SaveThirdParty(thirdParty, audit)
        Return New ActionResult(Of ThirdParty) With {.StateResult = result, .ObjectEmbbeded = thirdParty, .Message = $"{If(result, "Se ", "No se ")} logro guardar/actualizar el Tercero {obj.IdentificationNumber}"}
    End Function

    ''' <summary>
    ''' Guarda Profesionales
    ''' </summary>
    ''' <param name="professional"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveProfessional(professional As INPROFSAL, ListHealthProfessionalContract As List(Of Domain.Entities.HealthProfessionalContract), ListDeleteHealthProfessionalContract As List(Of Domain.Entities.HealthProfessionalContract),
                                     audit As AuditMessage) As ActionResult(Of INPROFSAL) Implements IHealthCareProfessionalAdminService.SaveProfessional
        If professional Is Nothing Then
            Throw New ArgumentNullException("professional")
        End If
        Dim unitOfWork As IUnitWork = Me._healthCareProfessional.UnitWork
        Dim unitOfWorkHealthProfessionalContract As IUnitWork = Me._healthProfessionalContractRepository.UnitWork
        Using Transaction As New TransactionScope
            Try
                'Se valida que el codigo del medico exista como usuario en Crystal
                If professional.ChangeTracker.State = ObjectState.Added Then
                    professional.CODUSUARI = professional.CODPROSAL
                    Dim _user = Me._healthCareProfessional.GetUserHIS(professional.CODPROSAL.ToString.Trim)
                    If _user Is Nothing OrElse _user.CODUSUARI Is Nothing OrElse _user.CODUSUARI.ToString.Trim Is String.Empty Then
                        unitOfWork.RollbackChanges()
                        unitOfWorkHealthProfessionalContract.RollbackChanges()
                        Transaction.Dispose()
                        Return New ActionResult(Of INPROFSAL) With {.StateResult = False, .Message = String.Format(ResourceManager.GetString("UserDontExist", NAMEMODULE), professional.CODUSUARI)}
                    End If
                End If

                'Se guardan los detalles de contratos del medico con los listados
                If ListHealthProfessionalContract IsNot Nothing AndAlso ListHealthProfessionalContract.Count > 0 Then
                    For Each item As HealthProfessionalContract In ListHealthProfessionalContract
                        _healthProfessionalContractRepository.SaveEntity(item)
                        unitOfWorkHealthProfessionalContract.Commit()
                    Next
                End If
                'Se eliminan los detalles de contratos del medico con los listados
                If ListDeleteHealthProfessionalContract IsNot Nothing AndAlso ListDeleteHealthProfessionalContract.Count > 0 Then
                    For Each item As HealthProfessionalContract In ListDeleteHealthProfessionalContract
                        _healthProfessionalContractRepository.SaveEntity(item)
                        unitOfWorkHealthProfessionalContract.Commit()
                    Next
                End If

                Me._healthCareProfessional.SaveEntity(professional)
                unitOfWork.Commit()
                Transaction.Complete()
                Return New ActionResult(Of INPROFSAL) With {.StateResult = True, .Message = professional.CODUSUARI, .ObjectEmbbeded = professional}
            Catch ex As System.Data.Entity.Validation.DbEntityValidationException
                unitOfWork.RollbackChanges()
                unitOfWorkHealthProfessionalContract.RollbackChanges()
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of INPROFSAL) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                unitOfWorkHealthProfessionalContract.RollbackChanges()
                Transaction.Dispose()
                Return New ActionResult(Of INPROFSAL) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorConcurrence")}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                unitOfWorkHealthProfessionalContract.RollbackChanges()
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of INPROFSAL) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Cambia el estado de un profesional por código
    ''' </summary>
    ''' <param name="Code">Código del profesional</param>
    ''' <param name="Status">nuevo estado del profesional</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateStatusProfessional(Code As String, Status As Boolean) As ActionResult(Of INPROFSAL) Implements IHealthCareProfessionalAdminService.UpdateStatusProfessional
        Try
            Dim professional As INPROFSAL = Me._healthCareProfessional.GetProfessionalByCode(Code)
            professional.ESTADOMED = If(Status = True, 1, 2)
            professional.MarkAsModified()
            Dim result = Me.SaveProfessional(professional, Nothing, Nothing, Nothing)
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of INPROFSAL) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' cambia de estado un profesional int o ext
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <param name="Status"></param>
    ''' <returns></returns>
    Public Function UpdateStatusProfessionalHealth(Code As String, Status As Boolean) As ActionResult(Of HealthProfessionalModel) Implements IHealthCareProfessionalAdminService.UpdateStatusProfessionalHealth
        Try
            Dim professional As INPROFSAL = Me._healthCareProfessional.GetProfessionalByCode(Code)
            Dim IdentificationNumber = If(String.IsNullOrEmpty(professional?.CODIGONIT), Code, professional?.CODIGONIT)
            Dim professionalERP = Me._healthProfessionalRepository.FirstOrDefault(Function(x) x.IdentificationNumber = IdentificationNumber)

            If professionalERP Is Nothing Then
                Dim person = Me._personRepository.FirstOrDefault(Function(x) x.IdentificationNumber = professional.CODIGONIT)

                If person Is Nothing Then
                    Return New ActionResult(Of HealthProfessionalModel) With {.StateResult = False, .Message = $"No se encontró la persona con el numero de identificación: {professional.CODIGONIT}"}
                End If

                professionalERP = New HealthProfessional
                With professionalERP
                    .IdentificationNumber = professional.CODIGONIT
                    .IdentificationTypeId = person.IdentificationTypeId
                    .FirstName = professional.MEDPRINOM
                    .SecondName = professional.MEDSEGNOM
                    .FirstLastName = professional.MEDPRIAPEL
                    .SecondLastName = professional.MEDSEGAPEL
                    .ProfessionalSpecialty = professional.CODESPEC1
                    .ExternalProfessional = False
                    .ProfessionalLicenseNumber = professional.TARJETAPR
                    .CreationDate = DateTime.Now
                    .CreationUser = professional.CODUSUARI
                    .Status = Status
                End With
            End If

            professional.ESTADOMED = If(Status = True, 1, 2)
            professional.MarkAsModified()

            If professionalERP.Id = 0 Then
                professionalERP.MarkAsAdded()
            Else
                professionalERP.MarkAsModified()
            End If

            Dim ObjProfessional = New HealthProfessionalModel With {.HealthProfessional = professionalERP, .INPROFSAL = If(professionalERP.ExternalProfessional, Nothing, professional)}

            Dim result = Me.SaveHealthProfessional(ObjProfessional, Nothing, Nothing, Nothing)
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of HealthProfessionalModel) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un listado de detalles de contratos que tiene asociado un medico
    ''' </summary>
    ''' <param name="healthProfessionalCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListHealthProfessionalContractByHealthProfessionalCode(healthProfessionalCode As String) As ActionResult(Of List(Of HealthProfessionalContract)) Implements IHealthCareProfessionalAdminService.GetListHealthProfessionalContractByHealthProfessionalCode
        If healthProfessionalCode Is String.Empty Then
            Throw New ArgumentNullException("healthProfessionalCode")
        End If
        Try
            Dim ListHealthProfessionalContract As List(Of HealthProfessionalContract) = Me._healthProfessionalContractRepository.GetListHealthProfessionalContractByHealthProfessionalCode(healthProfessionalCode)
            Return New ActionResult(Of List(Of HealthProfessionalContract)) With {.StateResult = True, .ObjectEmbbeded = ListHealthProfessionalContract}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of HealthProfessionalContract)) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el usuario del HIS
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUserHIS(Code As String) As ActionResult(Of SEGusuaru) Implements IHealthCareProfessionalAdminService.GetUserHIS
        Try
            Return New ActionResult(Of SEGusuaru) With {.StateResult = True, .ObjectEmbbeded = Me._healthCareProfessional.GetUserHIS(Code)}
        Catch ex As OptimisticConcurrencyException
            Return New ActionResult(Of SEGusuaru) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorConcurrence")}
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SEGusuaru) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
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
            _healthCareProfessional = Nothing
            _medicalFeesContractRepository = Nothing
            _distributionLinesRepository = Nothing
            _healthProfessionalContractRepository = Nothing
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
