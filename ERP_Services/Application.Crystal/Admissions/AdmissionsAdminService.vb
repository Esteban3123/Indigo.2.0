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
Imports System.Transactions
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Crystal
Imports Domain.Crystal.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources
#End Region
Public Class AdmissionsAdminService
    Implements IAdmissionsAdminService

#Region "Fields"

    ''' <summary>
    ''' Nombre del modulo
    ''' </summary>
    ''' <remarks></remarks>
    Private Const NAMEMODULE As String = "Crystal"

    ''' <summary>
    ''' Repositorio de ADMISIONES
    ''' </summary>
    ''' <remarks></remarks>
    Private _admissionRepository As IAdmissionRepository

    ''' <summary>
    ''' Repositorio de las estancias
    ''' </summary>
    ''' <remarks></remarks>
    Private _stayRepository As IStayRepository

    ''' <summary>
    ''' Repositorio de grupos de atencion
    ''' </summary>
    ''' <remarks></remarks>
    Private _careGroupRepositoryRepository As ICareGroupRepository

    Private _patientRepository As IPatientRepository

    ''' <summary>
    ''' repositorio de consecutivos
    ''' </summary>
    ''' <remarks></remarks>
    Private _consecutiveRepository As Domain.Crystal.IConsecutiveRepository

    ''' <summary>
    ''' Repositorio de cupsentity
    ''' </summary>
    ''' <remarks></remarks>
    Private _healthAdministratorRepository As IHealthAdministratorRepository

    ''' <summary>
    ''' Repositorio de usuarios de crystal
    ''' </summary>
    ''' <remarks></remarks>
    Private _CrystalRepository As ISEGusuaruRepository

    Private _crystalEntityRepository As ICrystalEntityRepository

    Private _hCREGEGRERepository As IHCREGEGRERepository
#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="admissionRepository">Repositorio de estancias</param>
    Public Sub New(admissionRepository As IAdmissionRepository, stayRepository As IStayRepository, careGroupRepositoryRepository As ICareGroupRepository,
                   consecutiveRepository As Domain.Crystal.IConsecutiveRepository, healthAdministratorRepository As IHealthAdministratorRepository, CrystalRepository As ISEGusuaruRepository,
                   crystalEntityRepository As ICrystalEntityRepository, patientRepository As IPatientRepository, hCREGEGRERepository As IHCREGEGRERepository)
        Me._admissionRepository = admissionRepository
        Me._stayRepository = stayRepository
        Me._careGroupRepositoryRepository = careGroupRepositoryRepository
        Me._consecutiveRepository = consecutiveRepository
        Me._healthAdministratorRepository = healthAdministratorRepository
        Me._CrystalRepository = CrystalRepository
        _crystalEntityRepository = crystalEntityRepository
        _patientRepository = patientRepository
        _hCREGEGRERepository = hCREGEGRERepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtener Ingreso Por Código
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAdmissionByCode(Code As String) As ActionResult(Of ADINGRESO) Implements IAdmissionsAdminService.GetAdmissionByCode
        Try
            Dim admission = Me._admissionRepository.GetAdmissionByCode(Code)
            If admission IsNot Nothing AndAlso admission.NUMINGRES IsNot Nothing AndAlso admission.NUMINGRES IsNot String.Empty Then
                If admission.GENCAREGROUP IsNot Nothing AndAlso admission.GENCAREGROUP > 0 Then
                    Dim careGroup = Me._careGroupRepositoryRepository.GetCareGroupById(admission.GENCAREGROUP)
                    If careGroup IsNot Nothing AndAlso careGroup.Id > 0 Then
                        admission.ExtCareGroup = careGroup.Code & " - " & careGroup.Name
                    End If
                End If
                If admission.GENCONENTITY IsNot Nothing AndAlso admission.GENCONENTITY > 0 Then
                    Dim cupsEntity = Me._healthAdministratorRepository.GetHealthAdministratorById(admission.GENCONENTITY)
                    If cupsEntity IsNot Nothing AndAlso cupsEntity.Id > 0 Then
                        admission.ExtCupsEntity = cupsEntity.Code & " - " & cupsEntity.Name
                    End If
                End If
            End If
            Return New ActionResult(Of ADINGRESO) With {.StateResult = True, .ObjectEmbbeded = admission}
        Catch ex As OptimisticConcurrencyException
            Return New ActionResult(Of ADINGRESO) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorConcurrence")}
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ADINGRESO) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Obtener Especialidad Por Código
    ''' </summary>
    ''' <param name="Identification"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAdmissionsByPatient(Identification As String) As ActionResult(Of List(Of ADINGRESO)) Implements IAdmissionsAdminService.GetAdmissionsByPatient
        Try
            Return New ActionResult(Of List(Of ADINGRESO)) With {.StateResult = True, .ObjectEmbbeded = Me._admissionRepository.GetAdmissionByPatient(Identification)}
        Catch ex As OptimisticConcurrencyException
            Return New ActionResult(Of List(Of ADINGRESO)) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorConcurrence")}
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of ADINGRESO)) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    '''valida el ingreso que no este en estado fadturado,anulado, cerrado, si tiene egreso de cama y si tiene alta medica
    ''' </summary>
    ''' <param name="AdmissionCode"></param>
    ''' <returns></returns>
    Public Function AdmisionValidations(AdmissionCode As String) As ActionResult Implements IAdmissionsAdminService.AdmisionValidations
        Dim result As New ActionResult

        If AdmissionCode Is String.Empty Then
            Throw New ArgumentNullException("AdmissionCode")
        End If

        Try
            Dim Ingress = Me._admissionRepository.GetAdmissionByCode(AdmissionCode)

            If {"F", "A", "C"}.Contains(Ingress.IESTADOIN) Then
                Return New ActionResult With {.StateResult = False, .Message = $"El ingreso esta en estado {Utils.DicAdmisionStatus.Item(Ingress.IESTADOIN)}, No se puede hacer la solicitud!"}
            End If

            'si el ingreso tiene egreso de cama 
            If Ingress.FECHEGRESO.HasValue Then
                Return New ActionResult With {.StateResult = False, .Message = "El Ingreso ya tiene egreso, No se puede hacer la solicitud!"}
                'al ingreso ya se le dio de alta
            End If

            Dim parmas As List(Of (String, Object)) = New List(Of (String, Object)) From {("@NUMINGRES", AdmissionCode)}
            Dim IngressDeAlta = _admissionRepository.ExecuteQueryDR(Of CHREGEGRE)("select  * from CHREGEGRE where NUMINGRES = @NUMINGRES", parmas).FirstOrDefault

            If IngressDeAlta IsNot Nothing Then
                Return New ActionResult With {.StateResult = False, .Message = "Al Ingreso ya se le dio de alta, No se puede hacer la solicitud!"}
            End If

            Return New ActionResult With {.StateResult = True}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Elimina Profesionales
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteAdmission(Code As String) As ActionResult Implements IAdmissionsAdminService.DeleteAdmission
        Dim result As New ActionResult
        result.StateResult = True
        If Code Is String.Empty Then
            Throw New ArgumentNullException("Code")
        End If
        Dim UnitOfWork As IUnitWork = Me._admissionRepository.UnitWork
        Try
            Dim entityDelete As ADINGRESO = Me._admissionRepository.GetAdmissionByCode(Code)
            If entityDelete IsNot Nothing AndAlso entityDelete.NUMINGRES IsNot Nothing AndAlso entityDelete.NUMINGRES.ToString.Trim IsNot String.Empty Then
                Me._admissionRepository.DeleteEntity(entityDelete)
                UnitOfWork.Commit()
            Else
                result.StateResult = False
                result.Message = String.Format(ResourceManager.GetString("DontExistAdmission", NAMEMODULE), Code)
                Return result
            End If
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
    ''' Guarda Profesionales
    ''' </summary>
    ''' <param name="admission"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveAdmission(admission As ADINGRESO, audit As AuditMessage) As ActionResult(Of ADINGRESO) Implements IAdmissionsAdminService.SaveAdmission
        If admission Is Nothing Then
            Throw New ArgumentNullException("professional")
        End If
        Dim unitOfWork As IUnitWork = Me._admissionRepository.UnitWork

        'configuro la transaccion
        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.DefaultTimeout
        txSettings.IsolationLevel = IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                If admission.ChangeTracker.State = ObjectState.Added Then
                    admission.FECREGCRE = DateTime.Now()
                    Dim NUMINGRES As Integer
                    Dim _consecutive = Me._consecutiveRepository.GetConsecutive("00000001")
                    If _consecutive IsNot Nothing Then
                        NUMINGRES = fncConcatenar("0", _consecutive.CONNUMACT, 10, Direction.Left)
                        admission.NUMINGRES = NUMINGRES
                        _consecutive.CONNUMACT = _consecutive.CONNUMACT + CDec(1)
                        _consecutive.MarkAsModified()
                        Me._consecutiveRepository.SaveEntity(_consecutive)
                        Me._consecutiveRepository.UnitWork.Commit()
                    End If
                    If admission.TIPOINGRE = 2 Then
                        Dim statusCama As Boolean = Me._admissionRepository.GetBedStatus(admission.CODICAMHO)
                        If statusCama = False Then
                            Return New ActionResult(Of ADINGRESO) With {.StateResult = False, .Message = "La cama no se encuentra disponible!"}
                        End If
                    End If
                Else
                    admission.FECREGMOD = DateTime.Now
                End If

                If admission.GENCONENTITY IsNot Nothing Then
                    Dim healthAdministrator = _healthAdministratorRepository.GetHealthAdministratorById(admission.GENCONENTITY)
                    If healthAdministrator.Id = 0 Then
                        Return New ActionResult(Of ADINGRESO) With {.StateResult = False, .Message = "La entidad administradora no existe en Indigo VIE"}
                    End If
                    Dim crystalEntity = _crystalEntityRepository.GetEntityByNit(healthAdministrator.ThirdPartyDescription.Split("-").ElementAt(0))
                    If crystalEntity.CODENTIDA Is String.Empty Then
                        Return New ActionResult(Of ADINGRESO) With {.StateResult = False, .Message = "La entidad administradora " + healthAdministrator.Code + " - " + healthAdministrator.Name + " no esta homologada Indigo Vie Cloud Platform"}
                    End If
                    admission.CODENTIDA = crystalEntity.CODENTIDA
                Else
                    Dim careGroup As CareGroup = _careGroupRepositoryRepository.GetCareGroupById(admission.GENCAREGROUP)
                    If careGroup.CareGroupType <> 3 Then
                        Dim healthAdministrator = _healthAdministratorRepository.GetHealthAdministrator("999")
                        If healthAdministrator.Id = 0 Then
                            Return New ActionResult(Of ADINGRESO) With {.StateResult = False, .Message = "La entidad administradora 999 - Particulares no existe en Indigo VIE"}
                        End If
                        Dim crystalEntity = _crystalEntityRepository.GetEntityByCode("999")
                        If crystalEntity.CODENTIDA Is String.Empty Then
                            Return New ActionResult(Of ADINGRESO) With {.StateResult = False, .Message = "La entidad administradora 999 - Particulares no esta homologada Indigo Vie Cloud Platform"}
                        End If
                        admission.CODENTIDA = crystalEntity.CODENTIDA
                    End If
                End If


                'admission.INPACIENT = Nothing
                Me._admissionRepository.SaveEntity(admission)
                unitOfWork.Commit()
                transaction.Complete()
                Return New ActionResult(Of ADINGRESO) With {.StateResult = True, .ObjectEmbbeded = admission, .StateResultAux = True}
            Catch ex As System.Data.Entity.Validation.DbEntityValidationException
                unitOfWork.RollbackChanges()
                Me._consecutiveRepository.UnitWork.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of ADINGRESO) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                Me._consecutiveRepository.UnitWork.RollbackChanges()
                transaction.Dispose()
                Return New ActionResult(Of ADINGRESO) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorConcurrence")}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                Me._consecutiveRepository.UnitWork.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of ADINGRESO) With {.StateResult = False, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' funcion para concatenar string
    ''' </summary>
    ''' <param name="strCaracter"></param>
    ''' <param name="strCadena"></param>
    ''' <param name="intNumero"></param>
    ''' <param name="Dir"></param>
    ''' <param name="strPrefijo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function fncConcatenar(ByVal strCaracter As String, ByVal strCadena As String, ByVal intNumero As Integer, ByVal Dir As Direction, Optional ByVal strPrefijo As String = "") As String
        Dim intCant As Integer
        intCant = strCadena.Length
        For i = intCant To intNumero - 1
            If Dir = Direction.Left Then
                strCadena = strCaracter & strCadena
            Else
                strCadena = strCadena & strCaracter
            End If
        Next
        fncConcatenar = strPrefijo & strCadena
        Return fncConcatenar
    End Function

    ''' <summary>
    ''' Enumeracion con la dirección
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum Direction
        Right
        Left
    End Enum

    ''' <summary>
    ''' Cambia el estado de un profesional por código
    ''' </summary>
    ''' <param name="Code">Código del profesional</param>
    ''' <param name="Status">nuevo estado del profesional</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateStatusAdmission(Code As String, Status As String, Justification As String, audit As AuditMessage) As ActionResult(Of ADINGRESO) Implements IAdmissionsAdminService.UpdateStatusAdmission
        Try
            Dim admission As ADINGRESO = Me._admissionRepository.GetAdmissionByCode(Code)
            ' validar si se puede anular
            If Status = "A" Then
                Dim ValidationState = _admissionRepository.AdmissionAnulateValidation(admission.NUMINGRES, admission.IPCODPACI)
                If ValidationState.ESTADO_VALIDACION = 0 Then
                    Return New ActionResult(Of ADINGRESO) With {.StateResult = True, .Message = ValidationState.MENSAJE, .StateResultAux = False}
                End If
            End If
            admission.IESTADOIN = Status
            admission.IJUSTIFIC = Justification
            admission.CODUSUANU = audit.CodeUser
            admission.FECREGANU = DateTime.Now
            admission.MarkAsModified()
            Dim result = Me.SaveAdmission(admission, Nothing)
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ADINGRESO) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene los parametros de un centro de atención
    ''' </summary>
    ''' <param name="_codcenate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCentersParameters(_codcenate As String) As ActionResult(Of ADPARAMET) Implements IAdmissionsAdminService.GetCentersParameters
        Try
            Return New ActionResult(Of ADPARAMET) With {.StateResult = True, .ObjectEmbbeded = Me._admissionRepository.GetCentersParameters(_codcenate)}
        Catch ex As OptimisticConcurrencyException
            Return New ActionResult(Of ADPARAMET) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorConcurrence")}
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ADPARAMET) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene fecha de triage para validar creación de ingreo
    ''' </summary>
    ''' <param name="paciente"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTriageDate(paciente As String) As DateTime Implements IAdmissionsAdminService.GetTriageDate
        Try
            Return Me._admissionRepository.GetTriageDate(paciente)
        Catch ex As OptimisticConcurrencyException
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un usuardio de crystal
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetusuarioCrystal(code As String) As SEGusuaru Implements IAdmissionsAdminService.GetusuarioCrystal
        Try
            Return Me._CrystalRepository.GetusuarioCrystal(code)
        Catch ex As OptimisticConcurrencyException
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function GetAdmissionStatusByNumIngres(admissionNumber As String) As String Implements IAdmissionsAdminService.GetAdmissionStatusByNumIngres
        Try
            Return Me._admissionRepository.GetAdmissionStatusByNumIngres(admissionNumber)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function
    Public Function ModifyAuthorizationAdmission(admissionNumber As String, authorizationNumber As String) As ActionResult Implements IAdmissionsAdminService.ModifyAuthorizationAdmission
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim adingreso As ADINGRESO = Me._admissionRepository.GetAdmissionByCode(admissionNumber, True)
                If adingreso IsNot Nothing AndAlso Not String.IsNullOrEmpty(adingreso.NUMINGRES) Then
                    adingreso.IAUTORIZA = authorizationNumber
                    _admissionRepository.SaveEntity(adingreso)
                    _admissionRepository.UnitWork.Commit()
                    scope.Complete()
                    Return New ActionResult With {.StateResult = True}
                Else
                    scope.Dispose()
                    Return New ActionResult With {.StateResult = False, .Message = String.Format("No se encontró el ingreso ({0})", admissionNumber)}
                End If
            End Using
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function GetINDIAGNOPByAdmissionNumber(adminssionNumber As String) As INDIAGNOP Implements IAdmissionsAdminService.GetINDIAGNOPByAdmissionNumber
        Try
            Return _admissionRepository.GetINDIAGNOPByAdmissionNumber(adminssionNumber)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function ListarHemocomponentesPorIngreso(ingreso As String) As List(Of SP_ListarHemocomponentesPorIngreso_Result) Implements IAdmissionsAdminService.ListarHemocomponentesPorIngreso
        Try
            Return _patientRepository.ListarHemocomponentesPorIngreso(ingreso)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of SP_ListarHemocomponentesPorIngreso_Result)()
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
            _admissionRepository = Nothing
            _stayRepository = Nothing
            _careGroupRepositoryRepository = Nothing
            _consecutiveRepository = Nothing
            _healthAdministratorRepository = Nothing
            _CrystalRepository = Nothing
            _crystalEntityRepository = Nothing
            _patientRepository = Nothing
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
