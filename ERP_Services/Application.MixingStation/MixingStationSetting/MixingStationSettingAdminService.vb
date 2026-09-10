'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Diego A. Roldan
' Created          : 2021-09-16
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Crystal
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources

Public Class MixingStationSettingAdminService
    Implements IMixingStationSettingAdminService, Inject

    ''' <summary>
    ''' repository
    ''' </summary>
    Private ReadOnly _mixingStationSettingRepository As IMixingStationSettingRepository
    Private ReadOnly _mixingStationSettingAttentionCenterRepository As IMixingStationSettingAttentionCenterRepository
    Private ReadOnly _aDCENATENRepository As IADCENATENRepository
    Private ReadOnly _iNUNIFUNCRepository As IINUNIFUNCRepository
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="mixingStationSettingRepository"></param>
    Public Sub New(mixingStationSettingRepository As IMixingStationSettingRepository,
                   mixingStationSettingAttentionCenterRepository As IMixingStationSettingAttentionCenterRepository,
                   aDCENATENRepository As IADCENATENRepository,
                   iNUNIFUNCRepository As IINUNIFUNCRepository)
        _aDCENATENRepository = aDCENATENRepository
        _iNUNIFUNCRepository = iNUNIFUNCRepository
        _mixingStationSettingRepository = mixingStationSettingRepository
        _mixingStationSettingAttentionCenterRepository = mixingStationSettingAttentionCenterRepository
    End Sub

    ''' <summary>
    ''' Obtiene los parámetros de mezclas por unidad operativa
    ''' </summary>
    ''' <param name="operativeUnitId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function GetMixingStationSettingByOperativeUnitId(operativeUnitId As Integer, audit As AuditMessage) As ActionResult(Of MixingStationSetting) Implements IMixingStationSettingAdminService.GetMixingStationSettingByOperativeUnitId
        Try
            If operativeUnitId = 0 Then
                Throw New ArgumentNullException("operativeUnitId")
            End If
            If audit Is Nothing Then
                Throw New ArgumentNullException("audit")
            End If

            Dim mixingStationSetting As MixingStationSetting = _mixingStationSettingRepository.GetMixingStationSettingByOperativeUnitId(operativeUnitId, False)

            If mixingStationSetting IsNot Nothing AndAlso mixingStationSetting.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of MixingStationSetting)(mixingStationSetting, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If

            Return New ActionResult(Of MixingStationSetting) With {.StateResult = True, .ObjectEmbbeded = mixingStationSetting}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of MixingStationSetting) With {.StateResult = False, .Message = {ex.Message}.ToString}
        End Try
    End Function

    ''' <summary>
    ''' Lista los centros de atencion
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAttentionCenters() As List(Of MixingStationSettingAttentionCenter) Implements IMixingStationSettingAdminService.ListAttentionCenters
        Dim attentionCenters = _mixingStationSettingAttentionCenterRepository.GetAll().ToList()

        If attentionCenters IsNot Nothing AndAlso attentionCenters.Any() Then
            For Each item In attentionCenters
                Dim adcenate = _aDCENATENRepository.GetADCENATENByCode(item.CODCENATE, False)
                If adcenate IsNot Nothing Then
                    item.AttentionCenterCodeName = $"{adcenate.CODCENATE.Trim()} - {adcenate.NOMCENATE.Trim()}"
                End If

                Dim unifun = _iNUNIFUNCRepository.FirstOrDefault(Function(m) m.UFUCODIGO = item.UFUCODIGO, tracking:=False)
                If unifun IsNot Nothing Then
                    item.FunctionalUnitCodeName = $"{unifun.UFUCODIGO.Trim()} - {unifun.UFUDESCRI.Trim()}"
                End If
            Next
        End If

        Return attentionCenters
    End Function

    ''' <summary>
    ''' Guarda un parámetro de mezclas
    ''' </summary>
    ''' <param name="mixingStationSetting"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveMixingStationSetting(mixingStationSetting As MixingStationSetting, attentionCenters As List(Of MixingStationSettingAttentionCenter), audit As AuditMessage) As ActionResult(Of MixingStationSetting) Implements IMixingStationSettingAdminService.SaveMixingStationSetting
        If mixingStationSetting Is Nothing Then
            Throw New ArgumentNullException("mixingStationSetting")
        End If

        Dim uow As IUnitWork = Me._mixingStationSettingRepository.UnitWork

        Try
            Dim auxObjEntity As MixingStationSetting = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of MixingStationSetting)
            Dim status As Integer

            If mixingStationSetting.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                mixingStationSetting.CreationUser = audit.CodeUser
                mixingStationSetting.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                auxObjEntity = mixingStationSetting.OriginalValue
                mixingStationSetting.ModificationUser = audit.CodeUser
                mixingStationSetting.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            If attentionCenters IsNot Nothing Then
                For Each ac In attentionCenters
                    _mixingStationSettingAttentionCenterRepository.SaveEntity(ac)
                Next
            End If

            _mixingStationSettingRepository.SaveEntity(mixingStationSetting)
            uow.Commit()

            auditProcess = New IndigoAuditSimpleEntity(Of MixingStationSetting)(mixingStationSetting, audit, status, auxObjEntity)
            auditProcess.Execute()
            mixingStationSetting.MarkAsUnchanged()

            Return New ActionResult(Of MixingStationSetting) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("SaveMessage"), .ObjectEmbbeded = mixingStationSetting}
        Catch ex As OptimisticConcurrencyException
            uow.RollbackChanges()
            Return New ActionResult(Of MixingStationSetting) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As IndigoValidationException
            uow.RollbackChanges()
            Return New ActionResult(Of MixingStationSetting) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ex.Message}
        Catch ex As Exception
            uow.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of MixingStationSetting) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

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
