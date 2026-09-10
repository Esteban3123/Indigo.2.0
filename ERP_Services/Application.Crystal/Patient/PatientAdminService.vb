'***********************************************************************
' Assembly         : Application.Crystal
' Author           : J. Kevin Garay 
' Created          : 26-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Data.Entity.Core
Imports System.Data.Entity.Infrastructure
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

Public Class PatientAdminService
    Implements IPatientAdminService

#Region "Fields"

    ''' <summary>
    ''' Nombre del modulo
    ''' </summary>
    ''' <remarks></remarks>
    Private Const NAMEMODULE As String = "Crystal"

    ''' <summary>
    ''' Repositorio de estancias
    ''' </summary>
    Private _patientRepository As IPatientRepository

    ''' <summary>
    ''' Aplicación terceros
    ''' </summary>
    ''' <remarks></remarks>
    Private _thirdPartyAdminService As IThirdPartyAdminService

    ''' <summary>
    ''' Repositorio de Grupos de atencion
    ''' </summary>
    ''' <remarks></remarks>
    Private _careGroupRepositoryRepository As Domain.Entities.ICareGroupRepository

    ''' <summary>
    ''' Repositorio de consecutivos de paciente
    ''' </summary>
    ''' <remarks></remarks>
    Private _patientConsecutiveRepository As IPatientConsecutiveRepository

    ''' <summary>
    ''' repositorio de niveles
    ''' </summary>
    ''' <remarks></remarks>
    Private _ILevelPatientRepository As ILevelPatientRepository

    ''' <summary>
    ''' repositorio de ciudades
    ''' </summary>
    ''' <remarks></remarks>
    Private _CityRepository As ICityRepository
    ''' <summary>
    ''' Repositorio de cupsentity
    ''' </summary>
    ''' <remarks></remarks>
    Private _healthAdministratorRepository As Domain.Entities.IHealthAdministratorRepository

    Private _crystalEntityRepository As ICrystalEntityRepository

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="patientRepository">Repositorio de estancias</param>
    Public Sub New(patientRepository As IPatientRepository, thirdPartyAdminService As IThirdPartyAdminService, CareGroupRepositoryRepository As Domain.Entities.ICareGroupRepository,
                   PatientConsecutiveRepository As IPatientConsecutiveRepository, LevelPatientRepository As ILevelPatientRepository, CityRepository As ICityRepository,
                   healthAdministratorRepository As Domain.Entities.IHealthAdministratorRepository, crystalEntityRepository As ICrystalEntityRepository)
        Me._patientRepository = patientRepository
        Me._thirdPartyAdminService = thirdPartyAdminService
        Me._careGroupRepositoryRepository = CareGroupRepositoryRepository
        Me._patientConsecutiveRepository = PatientConsecutiveRepository
        Me._ILevelPatientRepository = LevelPatientRepository
        Me._CityRepository = CityRepository
        _healthAdministratorRepository = healthAdministratorRepository
        _crystalEntityRepository = crystalEntityRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene un Paciente por identificación
    ''' </summary>
    ''' <param name="Identification">Identificación de paciente</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPatientByIdentification(Identification As String) As ActionResult(Of INPACIENT) Implements IPatientAdminService.GetPatientByIdentification
        Try
            Dim patient = Me._patientRepository.GetPatientByIdentification(Identification)

            If patient IsNot Nothing AndAlso patient.IPCODPACI IsNot String.Empty Then
                If patient.GENCAREGROUP IsNot Nothing AndAlso patient.GENCAREGROUP > 0 Then
                    Dim careGroup = Me._careGroupRepositoryRepository.GetCareGroupById(patient.GENCAREGROUP)
                    If careGroup IsNot Nothing AndAlso careGroup.Id > 0 Then
                        patient.CareGroupDesc = careGroup.Name
                    End If
                End If
                If patient.GENEXPEDITIONCITY IsNot Nothing Then
                    Dim citys = Me._CityRepository.GetCityById(patient.GENEXPEDITIONCITY, False)
                    If citys IsNot Nothing AndAlso citys.Id > 0 Then
                        patient.ExpeditionCityDescription = citys.Name
                    End If
                End If
            End If

            Return New ActionResult(Of INPACIENT) With {.StateResult = True, .ObjectEmbbeded = Me._patientRepository.GetPatientByIdentification(Identification)}
        Catch ex As OptimisticConcurrencyException
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of INPACIENT) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of INPACIENT) With {.StateResult = False, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Eliminar el paciente
    ''' </summary>
    ''' <param name="Identification"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeletePatient(Identification As String, audit As AuditMessage) As ActionResult Implements IPatientAdminService.DeletePatient
        Dim result As New ActionResult
        result.StateResult = True
        If Identification Is String.Empty Then
            Throw New ArgumentNullException("Identification")
        End If
        Dim UnitOfWork As IUnitWork = Me._patientRepository.UnitWork

        Try
            Dim entityDelete As INPACIENT = Me._patientRepository.GetPatientByIdentification(Identification)
            If entityDelete.IPCODPACI IsNot String.Empty Then
                Me._patientRepository.DeleteEntity(entityDelete)
                UnitOfWork.Commit()
            Else
                result.StateResult = False
                result.Message = String.Format(ResourceManager.GetString("DontExistPatient", NAMEMODULE), Identification)
                Return result
            End If

            ''/***** Auditoria Basica ********/
            'IndigoAuditBasic.Execute("Company", audit.Functional, company.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            ''/*****Auditoria Avanzada ******/
            'IndigoAuditSimpleEntity(Of Company).Execute(company, audit, Infrastructure.CrossCutting.Audit.Actions.Delete, audit.Company)

            'IndigoAuditSimpleEntity(Of Company).Execute(company, audit, Infrastructure.CrossCutting.Audit.Actions.Delete, company)
            Return result
        Catch ex As DbUpdateException
            UnitOfWork.RollbackChanges()
            result.StateResult = False
            result.Message = ResourceManager.GetString("ErrorDependence")
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            UnitOfWork.RollbackChanges()
            result.StateResult = False
            result.Message = ResourceManager.GetString("ErrorUnknown")
            Return result
        End Try
    End Function

    ''' <summary>
    ''' Guarda el paciente
    ''' </summary>
    ''' <param name="patient"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SavePatient(patient As INPACIENT, audit As AuditMessage) As ActionResult(Of INPACIENT) Implements IPatientAdminService.SavePatient
        If patient Is Nothing Then
            Throw New ArgumentNullException("Patient")
        End If
        Dim unitOfWork As IUnitWork = Me._patientRepository.UnitWork
        Dim unitWorkpatientConsecutive As IUnitWork = Me._patientConsecutiveRepository.UnitWork

        'configuro la transaccion
        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.DefaultTimeout
        txSettings.IsolationLevel = IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim _document As String = ""
                If patient.AnonimusAdult = True Then 'cuando no es recien nacido
                    _document = Mid(patient.CodAnonimusAdult, 1, 5).ToString
                End If
                If patient.AnonimusRegBaby = True Then 'cuando es recien nacido
                    _document = patient.IPCODPACI.ToString.Trim
                End If

                If _document IsNot String.Empty Then
                    Dim _consecutive = Me._patientConsecutiveRepository.GetConsecutiveByPoblationTypeAndDocument(patient.AnonymousType, _document)
                    _consecutive.CONSECUTI += 1
                    If _consecutive.ID = 0 Then
                        _consecutive.IDPOBLACI = patient.AnonymousType
                        _consecutive.IDDOCUMEN = _document
                    Else
                        _consecutive.ChangeTracker.State = ObjectState.Modified
                    End If
                    If patient.CodAnonimusAdult IsNot String.Empty Then 'no es recien nacido
                        patient.StopTracking()
                        patient.IPCODPACI = patient.CodAnonimusAdult.ToString.Trim & _consecutive.CONSECUTI
                        patient.StartTracking()
                    Else 'es recien nacido
                        patient.StopTracking()
                        patient.IPCODPACI = patient.IPCODPACI.ToString.Trim & _consecutive.CONSECUTI
                        patient.StartTracking()
                    End If
                    Me._patientConsecutiveRepository.SaveEntity(_consecutive)
                    unitWorkpatientConsecutive.Commit()
                End If

                patient.CODIGONIT = patient.IPCODPACI
                patient.IPNOMCOMP = patient.IPPRINOMB.ToString.Trim & " " & patient.IPSEGNOMB.ToString.Trim & " " &
                    patient.IPPRIAPEL.ToString.Trim & " " & patient.IPSEGAPEL.ToString.Trim
                If patient.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    patient.CODUSUCRE = audit.CodeUser
                    patient.FECREGCRE = DateTime.Now
                Else
                    patient.CODUSUMOD = audit.CodeUser
                    patient.FECREGMOD = DateTime.Now
                End If


                Dim ThirdParty As ThirdParty = Me._thirdPartyAdminService.GetThirdPartyByNitWithAgregates(patient.IPCODPACI.ToString.Trim)
                If ThirdParty.Person Is Nothing Then
                    ThirdParty.Person = New Person
                End If
                With ThirdParty.Person
                    .BirthDate = patient.IPFECNACI
                    .FirstLastName = patient.IPPRIAPEL
                    .FirstName = patient.IPPRINOMB
                    .Gender = patient.IPSEXOPAC
                    .IdentificationNumber = patient.IPCODPACI
                    .SecondLastName = patient.IPSEGAPEL
                    .SecondName = patient.IPSEGNOMB
                    .State = True
                    .IdentificationType = CInt(patient.IPTIPODOC) - 1
                End With
                With ThirdParty
                    .Nit = patient.IPCODPACI
                    .Name = patient.IPNOMCOMP
                    .State = True
                    If .ChangeTracker.State = ObjectState.Added Then
                        .CreationDate = DateTime.Now()
                        .UserId = audit.IdUser
                    End If
                End With

                If Not String.IsNullOrEmpty(patient.IPDIRECCI) Then
                    Dim addres As Address = Nothing

                    If ThirdParty.Person.Address IsNot Nothing AndAlso ThirdParty.Person.Address.Count > 0 Then
                        addres = (From x In ThirdParty.Person.Address Where x.Addresss = patient.IPDIRECCI.ToString().Trim() Select x).FirstOrDefault()
                    End If

                    If addres Is Nothing Then
                        addres = New Address()
                        With addres
                            .IdPerson = ThirdParty.Person.Id
                            .Addresss = patient.IPDIRECCI.ToString.Trim
                            .State = 1
                            .Synchronized = 1
                        End With

                        ThirdParty.Person.Address.Add(addres)
                    End If
                End If

                If Not String.IsNullOrEmpty(patient.IPTELEFON) Then
                    Dim phone As Phone = Nothing

                    If ThirdParty.Person.Phone IsNot Nothing AndAlso ThirdParty.Person.Phone.Count > 0 Then
                        phone = (From x In ThirdParty.Person.Phone Where x.Phone1 = patient.IPTELEFON.ToString().Trim() Select x).FirstOrDefault()
                    End If

                    If phone Is Nothing Then
                        phone = New Phone()
                        With phone
                            .IdPerson = ThirdParty.Person.Id
                            .Phone1 = patient.IPTELEFON.ToString.Trim
                            .State = 1
                            .Synchronized = 1
                            .IdPhoneType = _thirdPartyAdminService.GetPhoneType().Id
                        End With

                        ThirdParty.Person.Phone.Add(phone)
                    End If
                End If

                If Not String.IsNullOrEmpty(patient.CORELEPAC) Then
                    Dim email As Email = Nothing

                    If ThirdParty.Person.Email IsNot Nothing AndAlso ThirdParty.Person.Email.Count > 0 Then
                        email = (From x In ThirdParty.Person.Email Where x.Email1 = patient.CORELEPAC.ToString().Trim() Select x).FirstOrDefault()
                    End If

                    If email Is Nothing Then
                        email = New Email()
                        With email
                            .IdPerson = ThirdParty.Person.Id
                            .Email1 = patient.IPDIRECCI.ToString.Trim
                            .State = 1
                            .Synchronized = 1
                        End With

                        ThirdParty.Person.Email.Add(email)
                    End If
                End If

                If patient.GENCONENTITY IsNot Nothing Then
                    Dim healthAdministrator = _healthAdministratorRepository.GetHealthAdministratorById(patient.GENCONENTITY)
                    If healthAdministrator.Id = 0 Then
                        Return New ActionResult(Of INPACIENT) With {.StateResult = False, .Message = "La entidad administradora no existe en Indigo VIE"}
                    End If
                    Dim crystalEntity = _crystalEntityRepository.GetEntityByNit(healthAdministrator.ThirdPartyDescription.Split("-").ElementAt(0))
                    If crystalEntity.CODENTIDA Is String.Empty Then
                        Return New ActionResult(Of INPACIENT) With {.StateResult = False, .Message = "La entidad administradora " + healthAdministrator.Code + " - " + healthAdministrator.Name + " no esta homologada Indigo Vie Cloud Platform"}
                    End If
                    patient.CODENTIDA = crystalEntity.CODENTIDA
                Else
                    Dim healthAdministrator = _healthAdministratorRepository.GetHealthAdministrator("999")
                    If healthAdministrator.Id = 0 Then
                        Return New ActionResult(Of INPACIENT) With {.StateResult = False, .Message = "La entidad administradora 999 - Particulares no existe en Indigo VIE"}
                    End If
                    Dim crystalEntity = _crystalEntityRepository.GetEntityByCode("999")
                    If crystalEntity.CODENTIDA Is String.Empty Then
                        Return New ActionResult(Of INPACIENT) With {.StateResult = False, .Message = "La entidad administradora 999 - Particulares no esta homologada Indigo Vie Cloud Platform"}
                    End If
                    patient.CODENTIDA = crystalEntity.CODENTIDA
                End If

                If Me._thirdPartyAdminService.SaveThirdParty(ThirdParty, audit) Then
                    patient.INPACIENTTOPANU.Clear()
                    Me._patientRepository.SaveEntity(patient)
                    unitOfWork.Commit()
                    ' _ILevelPatientRepository.UnitWork.Detach(patient.ADNIVELES)
                Else
                    Return New ActionResult(Of INPACIENT) With {.StateResult = False, .ObjectEmbbeded = patient, .Message = ResourceManager.GetString("ErrorSaveThirdParty", NAMEMODULE)}
                End If

                'Se marca la entidad como sin cambios
                'patient.MarkAsUnchanged()
                transaction.Complete()
                Return New ActionResult(Of INPACIENT) With {.StateResult = True, .ObjectEmbbeded = patient}
            Catch ex As System.Data.Entity.Validation.DbEntityValidationException
                unitOfWork.RollbackChanges()
                unitWorkpatientConsecutive.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of INPACIENT) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                unitWorkpatientConsecutive.RollbackChanges()
                transaction.Dispose()
                Return New ActionResult(Of INPACIENT) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorConcurrence")}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                unitWorkpatientConsecutive.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of INPACIENT) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Obtener nivel por codigo 
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLevelByCode(Code As String) As ADNIVELES Implements IPatientAdminService.GetLevelByCode
        Try
            Return Me._patientRepository.GetLevelByCode(Code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda el peso de un paciente
    ''' </summary>
    ''' <param name="weight"></param>
    ''' <param name="codePatient"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveWeightPatient(weight As Integer, codePatient As String) As Boolean Implements IPatientAdminService.SaveWeightPatient
        Dim patient As String
        patient = Me._patientRepository _
           .ExecuteQuery(Of String)("UPDATE dbo.INPACIENT SET PESO = {0} OUTPUT inserted.IPCODPACI WHERE IPCODPACI = {1}", weight, codePatient)?.FirstOrDefault()
        Return True
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _thirdPartyAdminService.Dispose()
            End If
            _patientRepository = Nothing
            _thirdPartyAdminService = Nothing
            _careGroupRepositoryRepository = Nothing
            _patientConsecutiveRepository = Nothing
            _ILevelPatientRepository = Nothing
            _CityRepository = Nothing
            _healthAdministratorRepository = Nothing
            _crystalEntityRepository = Nothing
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
