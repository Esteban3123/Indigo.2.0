'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Judy Andrea Díaz Reyes
' Created          : 06-05-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Base
Imports Application.Security
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Payroll
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources
Imports System.Data.Entity.Core
Imports System.Data.Entity.Infrastructure
Imports System.Text
Imports System.Transactions

Public Class CMConfigAdminService
    Implements ICMConfigAdminService, Inject

#Region "Properties"

    Private Const FORM_NAME As String = "FrmCMConfigure"

    Private _cmConfigRepository As ICMConfigRepository

    Private _cmConfigurationUserRepository As ICMConfigurationUserRepository

    Private _medicinesProductionRepository As IMedicinesProductionRepository

    ''' <summary>
    ''' Repositorio de secuencias numéricas
    ''' </summary>
    Private _secuenseDetailRepository As IMixingStationSequenceDetailRepository

    Private _IUserAdminService As IUserAdminService
    Private _employeeRepository As IEmployeeRepository

#End Region

#Region "Methods"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal cmConfigRepository As ICMConfigRepository, ByVal secuenseDetailRepository As IMixingStationSequenceDetailRepository,
                   IUserAdminService As IUserAdminService, medicinesProductionRepository As IMedicinesProductionRepository,
                   cmConfigurationUserRepository As ICMConfigurationUserRepository, EmployeeRepository As IEmployeeRepository)
        If cmConfigRepository Is Nothing Then
            Throw New ArgumentNullException("workCenterRepository Vacio")
        End If
        If secuenseDetailRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDetailRepository")
        End If
        If medicinesProductionRepository Is Nothing Then
            Throw New ArgumentNullException("medicinesProductionRepository")
        End If
        Me._cmConfigRepository = cmConfigRepository
        Me._secuenseDetailRepository = secuenseDetailRepository
        Me._IUserAdminService = IUserAdminService
        _medicinesProductionRepository = medicinesProductionRepository
        _cmConfigurationUserRepository = cmConfigurationUserRepository
        Me._employeeRepository = EmployeeRepository
    End Sub

    ''' <summary>
    ''' Elimina los medicamentos para producción
    ''' </summary>
    ''' <param name="listIds"></param>
    ''' <param name="TransactionalContainer"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function DeleteMedicinesProduction(listIds As List(Of Integer), TransactionalContainer As String, audit As AuditMessage) As ActionResult Implements ICMConfigAdminService.DeleteMedicinesProduction
        If listIds Is Nothing OrElse listIds.Count = 0 Then
            Throw New ArgumentNullException("listIds")
        End If
        Using cnx As New System.Data.SqlClient.SqlConnection(Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, TransactionalContainer, False))
            cnx.Open()
            Dim tx As System.Data.SqlClient.SqlTransaction = cnx.BeginTransaction()
            Dim command As New System.Data.SqlClient.SqlCommand("", cnx, tx)
            command.CommandTimeout = 30000
            command.CommandType = CommandType.Text

            Try
                'Se obtienen los ids en un string
                Dim stringIds As String = String.Join(",", listIds.ToArray())

                'Se eliminan los registros
                command.CommandText = "delete from MixingStation.MedicinesProduction where Id in (" + stringIds + ")"
                command.ExecuteNonQuery()

                tx.Commit()
                Return New ActionResult With {.StateResult = True}
            Catch ex As Exception
                tx.Rollback()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            Finally
                cnx.Close()
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Permite la importación de medicamentos para producción
    ''' </summary>
    ''' <param name="data"></param>
    ''' <param name="CMConfigurationId"></param>
    ''' <returns></returns>
    Public Function ImportMedicinesProduction(data As List(Of List(Of String)), CMConfigurationId As Integer) As ActionResult(Of List(Of SP_ImportMedicinesProduction_Result)) Implements ICMConfigAdminService.ImportMedicinesProduction
        If data Is Nothing OrElse data.Count = 0 Then
            Throw New ArgumentNullException("data")
        End If
        Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                'Objeto xml
                Dim xmlObject = ConvertToXml(data)

                'Se consume el procedimiento almacenado
                Dim resultStore = _cmConfigRepository.SP_ImportMedicinesProduction(xmlObject, CMConfigurationId)
                If resultStore IsNot Nothing AndAlso resultStore.Count > 0 AndAlso (From x In resultStore Where x.StatusField = 2).Count > 0 Then
                    Dim message = (From x In resultStore Where x.StatusField = 2 Select x.MessageField).FirstOrDefault()
                    scope.Dispose()
                    Return New ActionResult(Of List(Of SP_ImportMedicinesProduction_Result)) With {.StateResult = False, .Message = message}
                End If

                'Se devuelve el mensaje
                scope.Complete()
                Return New ActionResult(Of List(Of SP_ImportMedicinesProduction_Result)) With {.ObjectEmbbeded = resultStore, .StateResult = True}
            Catch ex As Exception
                scope.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of List(Of SP_ImportMedicinesProduction_Result)) With {.StateResult = False, .Message = ex.ToString}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Convierte el listado de datos a xml
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConvertToXml(data As List(Of List(Of String))) As String
        Dim builder As New StringBuilder()
        builder.Append("<Data>")

        For Each item In data
            builder.Append("<Row>")
            builder.Append($"<CountFields>{item.Count}</CountFields>")
            builder.Append("<StatusField>0</StatusField>")
            builder.Append("<MessageField></MessageField>")

            builder.Append($"<ATCCode>{If(item.ElementAtOrDefault(0), String.Empty)}</ATCCode>")
            builder.Append($"<UnitDoseTypeCode>{If(item.ElementAtOrDefault(1), String.Empty)}</UnitDoseTypeCode>")
            builder.Append($"<CodeCenterAttention>{If(item.ElementAtOrDefault(2), String.Empty)}</CodeCenterAttention>")
            builder.Append($"<AllowsRemnant>{If(item.ElementAtOrDefault(3), String.Empty)}</AllowsRemnant>")

            builder.Append("</Row>")
        Next

        builder.Append("</Data>")
        Return builder.ToString()
    End Function

    Public Function GetMedicineProductionById(id As Integer) As MedicinesProduction Implements ICMConfigAdminService.GetMedicineProductionByID
        Try
            Dim MedicineProduction As MedicinesProduction = Me._medicinesProductionRepository.GetMedicinesProductionById(id)

            Return MedicineProduction
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda una entidad
    ''' </summary>
    ''' <param name="MedicinesProduction"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveMedicinesProduction(MedicinesProduction As MedicinesProduction, audit As AuditMessage) As ActionResult(Of MedicinesProduction) Implements ICMConfigAdminService.SaveMedicinesProduction
        If MedicinesProduction Is Nothing Then
            Throw New ArgumentNullException("MedicinesProduction")
        End If
        Dim unitOfWork As IUnitWork = Me._medicinesProductionRepository.UnitWork
        Try
            Dim validation = _cmConfigRepository.ValidateMedicineProduction(MedicinesProduction)
            If Not String.IsNullOrEmpty(validation) Then
                Return New ActionResult(Of MedicinesProduction) With {.StateResult = False, .Message = validation}
            End If

            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim auditProcess As IndigoAuditSimpleEntity(Of MedicinesProduction)
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Insert

                Me._medicinesProductionRepository.SaveEntity(MedicinesProduction)
                unitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of MedicinesProduction)(MedicinesProduction, audit, status, Nothing)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                MedicinesProduction.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of MedicinesProduction) With {.StateResult = True, .ObjectEmbbeded = MedicinesProduction}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of MedicinesProduction) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of MedicinesProduction) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Trae todos los parametros de central de mezclas
    ''' </summary>
    ''' <returns></returns>
    ''' 
    Public Function ListAllCMConfig(audit As AuditMessage) As List(Of CMConfiguration) Implements ICMConfigAdminService.ListAllCMConfig
        Try
            Dim cmConfigure = Me._cmConfigRepository.GetAll()
            For Each item As CMConfiguration In cmConfigure
                Dim auditObject As New IndigoAuditSimpleEntity(Of CMConfiguration)(item, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            Next
            Return cmConfigure
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try

    End Function

    ''' <summary>
    ''' Obtiene un Central de mezclas por codigo
    ''' </summary>
    ''' <param name="audit">The identifier.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Async Function GetCMConfigAsync(code As String, audit As AuditMessage) As Task(Of ActionResult(Of CMConfiguration)) Implements ICMConfigAdminService.GetCMConfigAsync
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim cmConfig As CMConfiguration = Me._cmConfigRepository.GetCMConfig(code)
            If cmConfig IsNot Nothing AndAlso cmConfig.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of CMConfiguration)(cmConfig, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If

            'Se saca el listado de ids de usuario para enviar
            'Se consulta los usuarios por id para agregarles la descripcion
            If cmConfig IsNot Nothing AndAlso cmConfig.Id > 0 AndAlso cmConfig.CMConfigurationUsers IsNot Nothing AndAlso cmConfig.CMConfigurationUsers.Count > 0 Then
                'Se recorre el listado de usuarios que trae la autorizacion para sacar los ids
                Dim listUserIds = cmConfig.CMConfigurationUsers.Select(Function(m) m.UserId).ToList()
                'Se obtiene el listado de usuarios
                Dim listUsers = _IUserAdminService.ListUsersByIds(listUserIds)

                If listUsers IsNot Nothing AndAlso listUsers.Any() Then
                    'Se asigna el nombre completo del usuario al listado que se va a asignar al datasource de la rejilla
                    For Each bu In cmConfig.CMConfigurationUsers
                        Dim user = listUsers.FirstOrDefault(Function(m) m.Id = bu.UserId)

                        If user IsNot Nothing AndAlso user.Id > 0 AndAlso user.Person IsNot Nothing Then
                            bu.FullNameUser = user.Person.Fullname
                            bu.CodeNameUser = $"{bu.UserCode} - {bu.FullNameUser}"

                            Dim employeeByThirdPartyId As Entities.Employee = Await _employeeRepository.GetEmployeeAsync(user.Person.Identification)
                            If employeeByThirdPartyId Is Nothing OrElse Not employeeByThirdPartyId.Contract.Any() Then
                                Continue For
                            End If

                            Dim contract = employeeByThirdPartyId.Contract.FirstOrDefault(Function(x) x.Valid AndAlso x.Status)
                            If contract Is Nothing OrElse contract.Id = 0 OrElse contract.Position.Id = 0 Then
                                Continue For
                            End If

                            bu.PositionName = contract.Position.Name
                        End If
                    Next
                End If
            End If

            Dim director = cmConfig.CMConfigurationUsers.FirstOrDefault(Function(m) m.UserId = cmConfig.IdDirector)
            cmConfig.DirectorUserCodeName = director?.CodeNameUser

            If cmConfig.IdDirectorSp.HasValue Then
                Dim directorSp = cmConfig.CMConfigurationUsers.FirstOrDefault(Function(m) m.UserId = cmConfig.IdDirectorSp)
                cmConfig.DirectorSpUserCodeName = directorSp?.CodeNameUser
            End If

            Return New ActionResult(Of CMConfiguration) With {.StateResult = True, .ObjectEmbbeded = cmConfig}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CMConfiguration) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Actualiza parámetros de central de mezclas
    ''' </summary>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateStateCMConfig(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of CMConfiguration) Implements ICMConfigAdminService.UpdateStateCMConfig
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
            Dim cmConfig As CMConfiguration = Me._cmConfigRepository.GetCMConfig(code)
            If cmConfig IsNot Nothing AndAlso cmConfig.Id > 0 Then
                cmConfig.State = state
            End If
            Return Me.SaveCMConfig(cmConfig, audit)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CMConfiguration) With {.StatusCode = eStatusResult.EXCEPTION, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try


        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("state")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim cmConfig As CMConfiguration = Me._cmConfigRepository.GetCMConfig(1)
            Return Me.SaveCMConfig(cmConfig, audit)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CMConfiguration) With {.StatusCode = eStatusResult.EXCEPTION, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza una central de mezclas
    ''' </summary>
    ''' <param name="cmConfig"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveCMConfig(cmConfig As CMConfiguration, audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of CMConfiguration) Implements ICMConfigAdminService.SaveCMConfig
        If cmConfig Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._cmConfigRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDetailRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(cmConfig.Code) Then
                    Dim seq As MixingStationSequenceDetail = Me._secuenseDetailRepository.GetSequenceDById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MixingStationSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            cmConfig.Code = res
                            seq.Next += 1
                            Me._secuenseDetailRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of CMConfiguration) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.MixingStationSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), cmConfig.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of CMConfiguration) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As CMConfiguration = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of CMConfiguration)
                Dim status As Integer

                If cmConfig.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    cmConfig.CreationUser = audit.CodeUser
                    cmConfig.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = cmConfig.OriginalValue
                    cmConfig.ModificationUser = audit.CodeUser
                    cmConfig.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                _cmConfigRepository.SaveEntity(cmConfig)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of CMConfiguration)(cmConfig, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                cmConfig.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of CMConfiguration) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = cmConfig, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of CMConfiguration) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of CMConfiguration) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of CMConfiguration) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CMConfiguration) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try

    End Function

    ''' <summary>
    ''' Consulta unidades funcionales de centros de atencion de una central de mezcla
    ''' </summary>
    ''' <param name="mixingStationId"></param>
    ''' <param name="listCenterLine"></param>
    ''' <returns></returns>
    Public Function ListCMCenterLineUnit(ByVal mixingStationId As Integer, ByVal listCenterLine As List(Of Tuple(Of String, Integer, Boolean))) As ActionResult(Of List(Of SP_CMCenterLineUnit_Result)) Implements ICMConfigAdminService.ListCMCenterLineUnit
        Try
            Dim xml As New StringBuilder

            xml.Append("<Parametros>")
            xml.Append(String.Format("<{0}>{1}</{0}>", "MixingStationId", mixingStationId))
            For Each item In listCenterLine
                xml.Append("<CL>")
                xml.Append(String.Format("<{0}>{1}</{0}>", "Center", item.Item1))
                xml.Append(String.Format("<{0}>{1}</{0}>", "Line", item.Item2))
                xml.Append(String.Format("<{0}>{1}</{0}>", "StatusCL", item.Item3))
                xml.Append("</CL>")
            Next
            xml.Append("</Parametros>")

            Dim _list As List(Of SP_CMCenterLineUnit_Result) = Me._cmConfigRepository.ListCMCenterLineUnit(xml.ToString)
            Return New ActionResult(Of List(Of SP_CMCenterLineUnit_Result)) With {.StateResult = True, .ObjectEmbbeded = _list}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of SP_CMCenterLineUnit_Result)) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
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

            _cmConfigRepository = Nothing
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
