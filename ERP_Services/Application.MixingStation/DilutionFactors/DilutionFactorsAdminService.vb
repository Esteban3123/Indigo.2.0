'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Giovanny Plazas L
' Created          : 2022-05-24
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Transactions
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources

#End Region

Public Class DilutionFactorsAdminService
    Implements IDilutionFactorsAdminService, Inject

#Region "Properties"
    Private Const FORM_NAME As String = "FrmDilutionFactors"
    ''' <summary>
    ''' repository
    ''' </summary>
    Private ReadOnly _DilutionFactorsRepository As IDilutionFactorsRepository

    Private ReadOnly _secuenseDetailRepository As IMixingStationSequenceDetailRepository
#End Region

#Region "Methods"
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="DilutionFactorsRepository"></param>
    Public Sub New(DilutionFactorsRepository As IDilutionFactorsRepository, ByVal secuenseDetailRepository As IMixingStationSequenceDetailRepository)
        _DilutionFactorsRepository = DilutionFactorsRepository
        Me._secuenseDetailRepository = secuenseDetailRepository
    End Sub

    ''' <summary>
    ''' Guarda o actualiza
    ''' </summary>
    ''' <param name="ListDilutionFactors"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Async Function SaveDilutionFactorsRepositoryAsync(ListDilutionFactors As List(Of DilutionFactors), audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As Task(Of ActionResult) Implements IDilutionFactorsAdminService.SaveDilutionFactorsRepositoryAsync

        If Not ListDilutionFactors?.Any() Then
            Throw New Exception("El objeto a guardar viene vacio")
        End If

        Dim uow As IUnitWork = Me._DilutionFactorsRepository.UnitWork

        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted}, TransactionScopeAsyncFlowOption.Enabled)
                Dim ListOfMessage = New List(Of String)

                'Se valida si los componentes de un paquete estan dentro de otro paquete siempre y cuando sea un paquete maestro
                Dim validateDuplicates As String = Await _DilutionFactorsRepository.ValidateDuplicateDilutionFactorsAsync(ListDilutionFactors(0))
                If Not String.IsNullOrEmpty(validateDuplicates) Then
                    Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {validateDuplicates}.ToList, .Message = validateDuplicates}
                End If

                For Each x In ListDilutionFactors

                    If x.DilutionFactorsDetail.Any(Function(d) d.Volume <= 0) Then
                        ListOfMessage.Add("El volumen parametrizado debe ser mayor a 0")
                        Continue For
                    End If

                    If String.IsNullOrEmpty(x.Code) Then
                        Dim seq As MixingStationSequenceDetail = Me._secuenseDetailRepository.GetSequenceDById(idSequence)
                        If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MixingStationSequence.Sequential Then
                            Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                            If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                                x.Code = res
                                seq.Next += 1
                                Me._secuenseDetailRepository.SaveEntity(seq)
                            Else
                                scope.Dispose()
                                Return New ActionResult With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                            End If
                        Else
                            scope.Dispose()
                            Return New ActionResult With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If

                    End If

                    Dim auxObjEntity As DilutionFactors = Nothing
                    Dim auditProcess As IndigoAuditSimpleEntity(Of DilutionFactors)
                    Dim status As Integer

                    If x.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        x.CreationUser = audit.CodeUser
                        x.CreationDate = DateTime.Now
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert
                    Else
                        auxObjEntity = x.OriginalValue
                        x.ModificationUser = audit.CodeUser
                        x.ModificationDate = DateTime.Now
                        status = Infrastructure.CrossCutting.Audit.Actions.Update
                    End If

                    _DilutionFactorsRepository.SaveEntity(x)
                    auditProcess = New IndigoAuditSimpleEntity(Of DilutionFactors)(x, audit, status, auxObjEntity)
                    auditProcess.Execute()

                    If status = Infrastructure.CrossCutting.Audit.Actions.Insert Then
                        ListOfMessage.Add($"{ResourceManager.GetString("SaveMessage")} con código: {x.Code}")
                    Else
                        ListOfMessage.Add($"Se ha actualizado correctamente factor de dilución con Codigo {x.Code} para el medicamento {x.ATC.Name}")
                    End If

                Next

                Await uow.CommitAsync()
                scope.Complete()
                Return New ActionResult With {.StateResult = True, .MessageResult = ListOfMessage, .Message = "Proceso exitoso"}
            End Using
        Catch ex As OptimisticConcurrencyException
            uow.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As IndigoValidationException
            uow.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ex.Message}
        Catch ex As Exception
            uow.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' obtiene el registro por codigo
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    Public Function GetDilutionFactorsByCode(Code As String) As ActionResult(Of DilutionFactors) Implements IDilutionFactorsAdminService.GetDilutionFactorsByCode
        Try
            Dim Query = Me._DilutionFactorsRepository.GetByFilter(Function(x) x.Code = Code, True, {"DilutionFactorsDetail.ATC", "DilutionFactorsDetail.InventoryMeasurementUnit", "ATC.InventoryMeasurementUnit2", "InventoryMeasurementUnit", "PharmaceuticalForm"}).FirstOrDefault

            If Query Is Nothing Then
                Query = New DilutionFactors
            Else
                Query.MeasurementUnitCodeName = $"{Query?.InventoryMeasurementUnit?.Code} - {Query?.InventoryMeasurementUnit?.Name}"
                Query.PreMedic = $"{Query.ATC.Weight} {Query.ATC.InventoryMeasurementUnit2.Abbreviation}"
                Query.MeasurementUnitAbbreviation = $"{Query.InventoryMeasurementUnit.Abbreviation}"
                For Each item In Query.DilutionFactorsDetail.ToList()
                    item.RequiredVolumeCodeName = $"{item.RequiredVolume}  {item.InventoryMeasurementUnit.Abbreviation}"
                    item.DisplacementVolumeCodeName = $"{item.DisplacementVolume}  {item.InventoryMeasurementUnit.Abbreviation}"
                    item.VolumenCodeName = $"{item.Volume} {item.InventoryMeasurementUnit.Abbreviation}"
                    item.TimeUnitCodeName = $"{item.AmountTime} {If(item.TimeUnit = 1, "HORA(S)", "DIA(S)")}"
                    item.ATCCodeName = $"{item.ATC.Code} {item.ATC.Name}"
                    item.ConcentrationCodeName = $"{Utils.SetPartDecimalToValue(item.Concentration)}  {Query.MeasurementUnitAbbreviation}/{item.InventoryMeasurementUnit.Abbreviation}"
                Next
            End If

            Return New ActionResult(Of DilutionFactors) With {.StateResult = True, .ObjectEmbbeded = If(Query Is Nothing, New DilutionFactors, Query), .Message = "Consulta exitosa"}
        Catch ex As Exception
            Return New ActionResult(Of DilutionFactors) With {.StateResult = False, .Message = ex.Message}
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
