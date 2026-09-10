'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 19/01/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports System.Collections.Generic
Imports System.Text
Imports System.Transactions
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Crystal
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources

#End Region
Public Class ConfirmationUnitDoseAdminService
    Implements IConfirmationUnitDoseAdminService, Inject

#Region "Properties"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private ReadOnly _confirmationUnitDoseRepository As IConfirmationUnitDoseRepository
    Private ReadOnly _packageAdminService As IPackageAdminService
    Private ReadOnly _pharmaDoseRepository As IPharmaDoseRepository
    Private ReadOnly _hCFARMEPDRepository As IHCFARMEPDRepository
    Private ReadOnly _ConfirmationUnitDoseValidationsRepository As IConfirmationUnitDoseValidationsRepository

#End Region

#Region "Methods"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ConfirmationUnitDoseRepository As IConfirmationUnitDoseRepository, packageAdminService As IPackageAdminService,
                   pharmaDoseRepository As IPharmaDoseRepository,
                   hCFARMEPDRepository As IHCFARMEPDRepository,
                   confirmationUnitDoseValidationsRepositoryRepository As IConfirmationUnitDoseValidationsRepository)

        If ConfirmationUnitDoseRepository Is Nothing Then
            Throw New ArgumentNullException("ConfirmationUnitDoseRepository Vacio")
        End If

        _hCFARMEPDRepository = hCFARMEPDRepository
        _pharmaDoseRepository = pharmaDoseRepository
        _packageAdminService = packageAdminService
        _confirmationUnitDoseRepository = ConfirmationUnitDoseRepository
        _ConfirmationUnitDoseValidationsRepository = confirmationUnitDoseValidationsRepositoryRepository
    End Sub

    ''' <summary>
    ''' Verifica una solicitud de tipo NPT
    ''' </summary>
    Public Function VerifyRequestNPT(_ItemTmp As ConfirmationUnitDoseValidations) As ActionResult Implements IConfirmationUnitDoseAdminService.VerifyRequestNPT
        Try

            Using scope As New TransactionScope(TransactionScopeOption.Required,
                                                    New TransactionOptions() With {
                                                        .Timeout = TransactionManager.MaximumTimeout,
                                                        .IsolationLevel = IsolationLevel.ReadCommitted})

                Dim confirmationUnitDoseValidations = _ConfirmationUnitDoseValidationsRepository.FirstOrDefault(Function(x) x.Id.Equals(_ItemTmp.Id), False)

                If confirmationUnitDoseValidations IsNot Nothing Then
                    _ItemTmp.SafeStatus = confirmationUnitDoseValidations.SafeStatus
                    _ItemTmp.ModificationDateSafeStatus = confirmationUnitDoseValidations.ModificationDateSafeStatus
                    _ItemTmp.ModificationUserSafeStatus = confirmationUnitDoseValidations.ModificationUserSafeStatus
                    _ItemTmp.MarkAsModified()
                End If

                _ConfirmationUnitDoseValidationsRepository.SaveEntity(_ItemTmp)
                _ConfirmationUnitDoseValidationsRepository.UnitWork.Commit()

                scope.Complete()
                Return New ActionResult With {.StateResult = True}
            End Using

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function SaveConfirmationUnitDoseAndPackageList(confirmationUnitDoses As List(Of ConfirmationUnitDose), package As Package, audit As AuditMessage) As ActionResult(Of ConfirmationUnitDose) Implements IConfirmationUnitDoseAdminService.SaveConfirmationUnitDoseAndPackageList
        Try
            Using scope As New TransactionScope(
                TransactionScopeOption.Required,
                New TransactionOptions() With {
                    .Timeout = TransactionManager.MaximumTimeout,
                    .IsolationLevel = IsolationLevel.ReadCommitted
                }
            )

                For Each item In confirmationUnitDoses
                    Dim routingCheck = ValidateHospitalRowStillRoutedToMixingStation(item)
                    If Not routingCheck.StateResult Then
                        scope.Dispose()
                        Return New ActionResult(Of ConfirmationUnitDose) With {.StateResult = False, .Message = routingCheck.Message}
                    End If

                    Dim res = If(package Is Nothing,
                                 SaveConfirmationUnitDose(item, audit),
                                 SaveConfirmationUnitDoseAndPackage(item, package.Clone(), audit))

                    If Not res.StateResult Then
                        scope.Dispose()
                        Throw New IndigoValidationException(res.Message)
                    End If
                Next

                scope.Complete()
                Return New ActionResult(Of ConfirmationUnitDose) With {.StateResult = True}
            End Using
        Catch ex As IndigoValidationException
            Return New ActionResult(Of ConfirmationUnitDose) With {.StateResult = False, .Message = ex.Message}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ConfirmationUnitDose) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function SaveConfirmationUnitDoseAndPackage(confirmationUnitDose As ConfirmationUnitDose, package As Package, audit As AuditMessage) As ActionResult(Of ConfirmationUnitDose) Implements IConfirmationUnitDoseAdminService.SaveConfirmationUnitDoseAndPackage
        Try
            Dim routingCheck = ValidateHospitalRowStillRoutedToMixingStation(confirmationUnitDose)
            If Not routingCheck.StateResult Then
                Return New ActionResult(Of ConfirmationUnitDose) With {.StateResult = False, .Message = routingCheck.Message}
            End If

            If package Is Nothing Then
                Return SaveConfirmationUnitDose(confirmationUnitDose, audit)
            End If

            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim resPackage = _packageAdminService.SavePackage(package, audit, 0)

                If Not resPackage.StateResult Then
                    Throw New IndigoValidationException(resPackage.Message)
                End If

                If package.IsPackagePersonalized Then
                    confirmationUnitDose.PersonalizedMasterPreparationPackageId = package.Id
                End If

                Dim res = SaveConfirmationUnitDose(confirmationUnitDose, audit)

                scope.Complete()
                Return res
            End Using
        Catch ex As IndigoValidationException
            Return New ActionResult(Of ConfirmationUnitDose) With {.StateResult = False, .Message = ex.Message}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ConfirmationUnitDose) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function SaveConfirmationUnitDose(ConfirmationUnitDose As ConfirmationUnitDose, audit As AuditMessage) As ActionResult(Of ConfirmationUnitDose) Implements IConfirmationUnitDoseAdminService.SaveConfirmationUnitDose
        If ConfirmationUnitDose Is Nothing Then
            Throw New ArgumentNullException("ConfirmationUnitDose")
        End If
        Dim routingCheck = ValidateHospitalRowStillRoutedToMixingStation(ConfirmationUnitDose)
        If Not routingCheck.StateResult Then
            Return New ActionResult(Of ConfirmationUnitDose) With {.StateResult = False, .Message = routingCheck.Message}
        End If
        Dim unitOfWork As IUnitWork = Me._confirmationUnitDoseRepository.UnitWork
        Try

            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim auxConfirmationUnitDose As ConfirmationUnitDose = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of ConfirmationUnitDose)
                Dim status As Integer

                If ConfirmationUnitDose.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    ConfirmationUnitDose.CreationUser = audit.CodeUser
                    ConfirmationUnitDose.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    auxConfirmationUnitDose = ConfirmationUnitDose.OriginalValue
                    ConfirmationUnitDose.ModificationUser = audit.CodeUser
                    ConfirmationUnitDose.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._confirmationUnitDoseRepository.SaveEntity(ConfirmationUnitDose)
                unitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of ConfirmationUnitDose)(ConfirmationUnitDose, audit, status, auxConfirmationUnitDose)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                ConfirmationUnitDose.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of ConfirmationUnitDose) With {.StateResult = True, .ObjectEmbbeded = ConfirmationUnitDose}
            End Using
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ConfirmationUnitDose) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function DeleteConfirmationUnitDose(listIds As List(Of Integer), audit As AuditMessage, TransactionalContainer As String) As ActionResult Implements IConfirmationUnitDoseAdminService.DeleteConfirmationUnitDose
        If listIds Is Nothing OrElse Not listIds.Any() Then
            Throw New ArgumentNullException("listIds")
        End If

        Using cnx As New SqlClient.SqlConnection(Utils.GetEntityConnectionString(ConfigurationFile.CONX_GENESIS, String.Empty, TransactionalContainer, False))
            cnx.Open()
            Dim tx As SqlClient.SqlTransaction = cnx.BeginTransaction()
            Dim command As New SqlClient.SqlCommand("", cnx, tx)
            command.CommandTimeout = 30000
            command.CommandType = CommandType.Text

            Try
                Dim personalizedMasterPreparationPackageIds = _confirmationUnitDoseRepository.Query(Function(m) listIds.Contains(m.Id) AndAlso m.PersonalizedMasterPreparationPackageId.HasValue) _
                .Select(Function(m) m.PersonalizedMasterPreparationPackageId.Value)?.ToList()

                Dim stringsIds = String.Join(",", listIds.ToArray())
                command.CommandText = $"delete from MixingStation.ConfirmationUnitDose where Id in ({stringsIds})"
                command.ExecuteNonQuery()

                If personalizedMasterPreparationPackageIds IsNot Nothing AndAlso personalizedMasterPreparationPackageIds.Any() Then
                    Dim packageIdsUsedInAnotherConfirmation = _confirmationUnitDoseRepository.Query(Function(m) Not listIds.Contains(m.Id) AndAlso m.PersonalizedMasterPreparationPackageId.HasValue AndAlso personalizedMasterPreparationPackageIds.Contains(m.PersonalizedMasterPreparationPackageId)) _
                        .Select(Function(m) m.PersonalizedMasterPreparationPackageId.Value)?.ToList()

                    If packageIdsUsedInAnotherConfirmation Is Nothing OrElse Not packageIdsUsedInAnotherConfirmation.Any() Then
                        command.CommandText = $"delete from MixingStation.PackagePersonalizedDetail where PackagePersonalizedId in ({String.Join(", ", personalizedMasterPreparationPackageIds)})"
                        command.ExecuteNonQuery()

                        command.CommandText = $"delete from MixingStation.PackagePersonalized where Id in ({String.Join(", ", personalizedMasterPreparationPackageIds)})"
                        command.ExecuteNonQuery()
                    End If
                End If

                tx.Commit()
                Return New ActionResult With {.StateResult = True}
            Catch ex As OptimisticConcurrencyException
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                tx.Rollback()
                Return New ActionResult With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
            Catch ex As UpdateException
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                tx.Rollback()
                Return New ActionResult With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
            Catch ex As Exception
                tx.Rollback()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            Finally
                cnx.Close()
            End Try
        End Using
    End Function

    Public Function GetConfirmationUnitDoseById(id As Integer) As ActionResult(Of ConfirmationUnitDose) Implements IConfirmationUnitDoseAdminService.GetConfirmationUnitDoseById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Try
            Dim ConfirmationUnitDose As ConfirmationUnitDose = Me._confirmationUnitDoseRepository.GetConfirmationUnitDoseById(id)
            Return New ActionResult(Of ConfirmationUnitDose) With {.StateResult = True, .ObjectEmbbeded = ConfirmationUnitDose}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ConfirmationUnitDose) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function AnnulateUnitDoses(items As List(Of AnnulateUnitDoseModel), audit As AuditMessage) As ActionResult Implements IConfirmationUnitDoseAdminService.AnnulateUnitDoses
        Try
            Dim errors As New StringBuilder()

            Using scope As New TransactionScope(
                TransactionScopeOption.Required,
                New TransactionOptions() With {
                    .Timeout = TransactionManager.MaximumTimeout,
                    .IsolationLevel = IsolationLevel.ReadCommitted
                })

                For Each item In items
                    Dim res = AnnulateItemDose(item, audit)

                    If Not res.StateResult Then
                        errors.AppendLine(res.Message)
                    End If
                Next

                If errors.Length > 0 Then
                    scope.Dispose()
                    Return New ActionResult With {.StateResult = False, .Message = errors.ToString()}
                End If

                scope.Complete()
            End Using

            Return New ActionResult With {.StateResult = True, .Message = "Los items fueron anulados correctamente"}
        Catch ex As IndigoValidationException
            Return New ActionResult With {.StateResult = False, .Message = ex.Message}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = ex.ToDetailString()}
        End Try
    End Function


    Private Function AnnulateItemDose(item As AnnulateUnitDoseModel, audit As AuditMessage) As ActionResult
        Try
            If item.Quantity = 0 Then
                Throw New IndigoValidationException("La cantidad no debe ser cero")
            End If

            Dim itemsPharmaDose = _pharmaDoseRepository _
            .Query(Function(m) m.CodeSusceptibleMixingStation = item.CodeSusceptibleMixingStation) _
            .ToList()

            Dim cantidadTotalDosis = itemsPharmaDose _
                .GroupBy(Function(m) m.GroupingCodeDose) _
                .Count()

            Dim cantidadAnulada = itemsPharmaDose _
                .Where(Function(m) m.DeliveryStatus = 3) _
                .GroupBy(Function(m) m.GroupingCodeDose) _
                .Count()

            Dim porcentajeAnulacion = (cantidadAnulada + item.Quantity) / cantidadTotalDosis ' 1 es la cantidad nueva a anular
            Dim hcFarmaPd = _hCFARMEPDRepository.Query(Function(m) m.CodeSusceptibleMixingStation = item.CodeSusceptibleMixingStation).ToList()
            Dim phd = itemsPharmaDose _
                .FindAll(Function(m) m.GroupingCodeDose = item.GroupingCodeDose)

            If phd.Any(Function(m) m.DeliveryStatus = 3) Then
                Throw New IndigoValidationException("Existen items seleccionados que ya se encuentran anulados")
            End If

            For Each dose In phd
                dose.DeliveryStatus = 3
                _pharmaDoseRepository.SaveEntity(dose)
            Next

            _pharmaDoseRepository.UnitWork.Commit()

            ' Anular los PharmaDose
            For Each ph In hcFarmaPd
                ph.CanceledQuantity = CInt(Math.Floor(ph.CANPEDPRO * porcentajeAnulacion))
                ph.CANPENPRO = ph.CANPEDPRO - ph.CanceledQuantity

                If ph.CANPENPRO < 0 Then
                    Throw New IndigoValidationException("Existen cantidades irregulares en los items enviados")
                End If

                If ph.CANPENPRO = 0 Then
                    ph.PROESTADO = 3
                End If

                _hCFARMEPDRepository.SaveEntity(ph)
            Next

            _hCFARMEPDRepository.UnitWork.Commit()

            Return New ActionResult With {.StateResult = True}
        Catch ex As Exception
            Return New ActionResult With {.StateResult = False, .Message = ex.ToDetailString()}
        End Try
    End Function

    Public Function UpdateMedicalOrderCM(objParams As String, audit As AuditMessage) As ActionResult(Of SP_UpdateMedicalOrder_Result) Implements IConfirmationUnitDoseAdminService.UpdateMedicalOrderCM
        Dim args As Object = Utils.DeserializeJsonToObject(objParams)
        Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                Dim xml = ConvertEntityToXml(args)

                Dim result = _confirmationUnitDoseRepository.SP_UpdateMedicalOrder(xml, audit.CodeUser)
                If result.CodeMessage <> 0 Then
                    scope.Dispose()
                    Return New ActionResult(Of SP_UpdateMedicalOrder_Result) With {.StateResult = False, .Message = result.Message}
                End If

                scope.Complete()
                Return New ActionResult(Of SP_UpdateMedicalOrder_Result) With {.StateResult = True, .ObjectEmbbeded = result, .Message = result.Message}
            Catch ex As Exception
                scope.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of SP_UpdateMedicalOrder_Result) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using

    End Function
    ''' <summary>
    ''' Establece el SafeStatus
    ''' </summary>
    Public Function SetSafeStatus(items As List(Of ConfirmationUnitDoseValidations), audit As AuditMessage) As ActionResult Implements IConfirmationUnitDoseAdminService.SetSafeStatus
        Try

            Using scope As New TransactionScope(TransactionScopeOption.Required,
                                                    New TransactionOptions() With {
                                                        .Timeout = TransactionManager.MaximumTimeout,
                                                        .IsolationLevel = IsolationLevel.ReadCommitted})

                For Each item In items
                    Dim confirmationUnitDoseValidations = _ConfirmationUnitDoseValidationsRepository.FirstOrDefault(Function(x) x.Id.Equals(item.Id), False)
                    If confirmationUnitDoseValidations IsNot Nothing Then
                        item.NPTVerified = confirmationUnitDoseValidations.NPTVerified
                        item.MarkAsModified()
                    End If

                    item.SafeStatus = item.SafeStatus
                    item.ModificationDateSafeStatus = DateTime.Now
                    item.ModificationUserSafeStatus = audit.CodeUser

                    _ConfirmationUnitDoseValidationsRepository.SaveEntity(item)
                Next

                _ConfirmationUnitDoseValidationsRepository.UnitWork.Commit()
                scope.Complete()
                Return New ActionResult With {.StateResult = True}
            End Using

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Origen hospitalario (Source = 1): exige que HCFARMEPD siga en SENDTO = 2 para el vínculo PharmaDose / producto (evita carrera con enrutamiento a farmacia).
    ''' </summary>
    Private Function ValidateHospitalRowStillRoutedToMixingStation(cud As ConfirmationUnitDose) As ActionResult
        Const msg As String = "El enrutamiento cambió o la solicitud ya no está en central de mezclas. Actualice el listado e intente de nuevo."
        If cud.Source <> 1 Then
            Return New ActionResult With {.StateResult = True}
        End If

        If cud.GroupingCodeDose = Guid.Empty Then
            Return New ActionResult With {.StateResult = True}
        End If

        If _confirmationUnitDoseRepository.Query(Function(c) c.GroupingCodeDose = cud.GroupingCodeDose AndAlso c.Id <> cud.Id).Any() Then
            Return New ActionResult With {.StateResult = False, .Message = "Esta dosis ya fue procesada. Actualice el listado e intente de nuevo."}
        End If

        Dim doses = _pharmaDoseRepository.Query(Function(p) p.GroupingCodeDose = cud.GroupingCodeDose).ToList()
        If Not doses.Any Then
            Return New ActionResult With {.StateResult = False, .Message = msg}
        End If

        Dim productTrim = If(cud.ServiceCode, String.Empty).Trim()
        If Not String.IsNullOrEmpty(productTrim) Then
            Dim matchByProduct = doses.Where(Function(p) String.Equals(p.ProductCode.Trim(), productTrim, StringComparison.OrdinalIgnoreCase)).ToList()
            If matchByProduct.Any Then
                doses = matchByProduct
            End If
        End If

        Dim processedKeys As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        For Each pd In doses
            Dim key = pd.CodeSusceptibleMixingStation.ToString() & "|" & pd.ProductCode.Trim()
            If processedKeys.Contains(key) Then Continue For
            processedKeys.Add(key)

            Dim pdProductUpper = pd.ProductCode.Trim().ToUpper()
            Dim rows = _hCFARMEPDRepository.Query(
                Function(h) h.CodeSusceptibleMixingStation.HasValue AndAlso
                    h.CodeSusceptibleMixingStation.Value = pd.CodeSusceptibleMixingStation AndAlso
                    h.CODPRODUC.Trim().ToUpper() = pdProductUpper, tracking:=False).ToList()

            If Not rows.Any OrElse rows.Any(Function(h) Not h.SENDTO.HasValue OrElse h.SENDTO.Value <> 2) Then
                Return New ActionResult With {.StateResult = False, .Message = msg}
            End If
        Next

        Return New ActionResult With {.StateResult = True}
    End Function

    Private Function ConvertEntityToXml(args As Object) As Object
        Dim builder As New StringBuilder

        builder.Append("<Header>")
        builder.Append("<CMConfigurationId>" & args.CMConfigurationId & "</CMConfigurationId>")
        builder.Append("</Header>")

        If CType(args, IDictionary(Of String, Object)).ContainsKey("SendComplete") Then 'Cuando se envian todos los registros principales para obtener los detalles de cada registro
            For Each item In CType(args.SendComplete, List(Of Object)).ToList()
                builder.Append("<SendComplete>")
                builder.Append("<SendTo>" & item.SendTo & "</SendTo>")
                builder.Append("<SourceType>" & item.SourceType & "</SourceType>")
                builder.Append("<CodeSusceptibleMixingStation>" & item.CodeSusceptibleMixingStation & "</CodeSusceptibleMixingStation>")
                builder.Append($"<PatientCode>{item.PatientCode}</PatientCode>")
                builder.Append("<ConfirmationUnitDoseId>" & item.ConfirmationUnitDoseId & "</ConfirmationUnitDoseId>")
                builder.Append("<Bed>" & item.Bed & "</Bed>")
                builder.Append("<EntityId>" & item.EntityId & "</EntityId>")
                builder.Append("<EntityName>" & item.EntityName & "</EntityName>")
                builder.Append("</SendComplete>")
            Next
        End If

        If CType(args, IDictionary(Of String, Object)).ContainsKey("SendOneToOne") Then 'Cuando se envian los detalles de solo un registro principal - Desde Detalles de Dosis -°Antes° ahora es solo SendComplete
            For Each item In CType(args.SendOneToOne, List(Of Object)).ToList()
                builder.Append("<SendOneToOne>")
                builder.Append("<Id>" & item.Id & "</Id>")
                builder.Append("<Observations>" & item.Observations & "</Observations>")
                builder.Append("<SendTo>" & item.SendTo & "</SendTo>")
                builder.Append($"<PatientCode>{item.PatientCode}</PatientCode>")
                builder.Append("<SourceType>" & item.SourceType & "</SourceType>")
                builder.Append("<ConfirmationUnitDoseId>" & item.ConfirmationUnitDoseId & "</ConfirmationUnitDoseId>")
                builder.Append("</SendOneToOne>")
            Next
        End If

        Return builder.ToString()
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If

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
