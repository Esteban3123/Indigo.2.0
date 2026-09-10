#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports System.Transactions
Imports Application.Maintenance

#End Region
Public Class EquipmentRegistrationAdminService
    Implements IEquipmentRegistrationAdminService


    'Repositorio de la aseguradora
    Private _EquipmentRegistrationRepository As IEquipmentRegistration

    'repositorio de la secuencia
    Private _sequenceDRepository As IMaintenanceSequenceDetailRepository

    Private _TechnicalLogDetailRepository As ITechnicalLogDetailRepository

    Private _EquipmentReceptionAccesoryRepository As IEquipmentReceptionAccesoryRepository

    Private _TechnicalEquipmentSheetRepository As ITechnicalEquipmentSheetRepository

    Private _ManualDetailRepository As IManualDetailRepository

    Private _EquipmentReceptionConsumableRepository As IEquipmentReceptionConsumableRepository

    Private _DrawingsDetailRepository As IDrawingsDetailRepository

    Private _fixedAssetPhysicalAssetRepository As IFixedAssetPhysicalAssetRepository

    'Private _PartsAccesoriesConsumablesRepository As IEquipmentReceptionPACRepository

    ''' <summary>
    ''' inicia el repositorio de registro de equipos
    ''' </summary>
    ''' <param name="EquipmentRegistrationaRepository">Repositorio de registro de equipos</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal EquipmentRegistrationaRepository As IEquipmentRegistration, sequenceRepository As IMaintenanceSequenceDetailRepository,
                   TechnicalLogDetailRepository As ITechnicalLogDetailRepository,
                   EquipmentReceptionAccesoryRepository As IEquipmentReceptionAccesoryRepository,
                   TechnicalEquipmentSheetRepository As ITechnicalEquipmentSheetRepository,
                   ManualDetailRepository As IManualDetailRepository,
                   EquipmentReceptionConsumableRepository As IEquipmentReceptionConsumableRepository,
                   DrawingsDetailRepository As IDrawingsDetailRepository,
                   fixedAssetPhysicalAssetRepository As IFixedAssetPhysicalAssetRepository)
        If (EquipmentRegistrationaRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de resgitro de equipo")
        End If
        If (sequenceRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de secuencia vacio")
        End If
        _sequenceDRepository = sequenceRepository
        _EquipmentRegistrationRepository = EquipmentRegistrationaRepository
        _TechnicalLogDetailRepository = TechnicalLogDetailRepository
        _EquipmentReceptionAccesoryRepository = EquipmentReceptionAccesoryRepository
        _TechnicalEquipmentSheetRepository = TechnicalEquipmentSheetRepository
        _ManualDetailRepository = ManualDetailRepository
        _EquipmentReceptionConsumableRepository = EquipmentReceptionConsumableRepository
        _DrawingsDetailRepository = DrawingsDetailRepository
        _fixedAssetPhysicalAssetRepository = fixedAssetPhysicalAssetRepository
        '_PartsAccesoriesConsumablesRepository = EquipmentReceptionPACRepository
    End Sub

    Public Function DeleteEquipmentRegistration(EquipmentRegistration As EquipmentRegistration, audit As AuditMessage) As Boolean Implements IEquipmentRegistrationAdminService.DeleteEquipmentRegistration

        If EquipmentRegistration Is Nothing Then
            Throw New ArgumentNullException("registro de equipo vacio")
        End If
        Dim unitWork As IUnitWork = _EquipmentRegistrationRepository.UnitWork
        Dim unitWorkTechnicalLogDetail As IUnitWork = _TechnicalLogDetailRepository.UnitWork
        Dim unitWorkEquipmentReceptionAccesory As IUnitWork = _EquipmentReceptionAccesoryRepository.UnitWork
        Dim unitWorkTechnicalEquipmentSheet As IUnitWork = _TechnicalEquipmentSheetRepository.UnitWork
        Dim unitWorkManualDetail As IUnitWork = _ManualDetailRepository.UnitWork
        'Dim unitWorkEquipmentReceptionParte As IUnitWork = _EquipmentReceptionParteRepository.UnitWork
        Dim unitWorkEquipmentReceptionConsumable As IUnitWork = _EquipmentReceptionConsumableRepository.UnitWork
        Dim unitWorkDrawingsDetail As IUnitWork = _DrawingsDetailRepository.UnitWork
        'Dim unitWorkPartsAccesoriesConsumables As IUnitWork = _PartsAccesoriesConsumablesRepository.UnitWork
        Try



            While EquipmentRegistration.EquipmentReceptionAccesory.Count > 0 'Elimino las autorizacion de conceptos que tenga
                Dim index = EquipmentRegistration.EquipmentReceptionAccesory.Count - 1
                _EquipmentReceptionAccesoryRepository.DeleteEntity(EquipmentRegistration.EquipmentReceptionAccesory(index))
            End While
            unitWorkEquipmentReceptionAccesory.Commit()


            While EquipmentRegistration.TechnicalLogDetail.Count > 0 'Elimino las autorizacion de conceptos que tenga
                Dim index = EquipmentRegistration.TechnicalLogDetail.Count - 1
                _TechnicalLogDetailRepository.DeleteEntity(EquipmentRegistration.TechnicalLogDetail(index))
            End While
            unitWorkTechnicalLogDetail.Commit()

            While EquipmentRegistration.TechnicalEquipmentSheet.Count > 0 'Elimino las autorizacion de conceptos que tenga
                Dim index = EquipmentRegistration.TechnicalEquipmentSheet.Count - 1
                _TechnicalEquipmentSheetRepository.DeleteEntity(EquipmentRegistration.TechnicalEquipmentSheet(index))
            End While
            unitWorkTechnicalEquipmentSheet.Commit()

            While EquipmentRegistration.ManualDetail.Count > 0 'Elimino las autorizacion de conceptos que tenga
                Dim index = EquipmentRegistration.ManualDetail.Count - 1
                _ManualDetailRepository.DeleteEntity(EquipmentRegistration.ManualDetail(index))
            End While
            unitWorkManualDetail.Commit()

            'While EquipmentRegistration.EquipmentReceptionParte.Count > 0 'Elimino las autorizacion de conceptos que tenga
            '    Dim index = EquipmentRegistration.EquipmentReceptionParte.Count - 1
            '    _EquipmentReceptionParteRepository.DeleteEntity(EquipmentRegistration.EquipmentReceptionParte(index))
            'End While
            'unitWorkEquipmentReceptionParte.Commit()

            While EquipmentRegistration.EquipmentReceptionConsumable.Count > 0 'Elimino las autorizacion de conceptos que tenga
                Dim index = EquipmentRegistration.EquipmentReceptionConsumable.Count - 1
                _EquipmentReceptionConsumableRepository.DeleteEntity(EquipmentRegistration.EquipmentReceptionConsumable(index))
            End While
            unitWorkEquipmentReceptionConsumable.Commit()

            While EquipmentRegistration.DrawingsDetail.Count > 0 'Elimino las autorizacion de conceptos que tenga
                Dim index = EquipmentRegistration.DrawingsDetail.Count - 1
                _DrawingsDetailRepository.DeleteEntity(EquipmentRegistration.DrawingsDetail(index))
            End While
            unitWorkDrawingsDetail.Commit()

            'While EquipmentRegistration.EquipmentReceptionPartsAccesoriesConsumables.Count > 0
            '    Dim index = EquipmentRegistration.EquipmentReceptionPartsAccesoriesConsumables.Count - 1
            '    _PartsAccesoriesConsumablesRepository.DeleteEntity(EquipmentRegistration.EquipmentReceptionPartsAccesoriesConsumables(index))
            'End While
            'unitWorkPartsAccesoriesConsumables.Commit()


            _EquipmentRegistrationRepository.DeleteEntity(EquipmentRegistration)

            unitWork.Commit()
            'IndigoAuditSimpleEntity(Of EquipmentRegistration).Execute(EquipmentRegistration, audit, Infrastructure.CrossCutting.Audit.Actions.Delete, audit.Company)
            Return True
        Catch ex As Exception
            unitWorkEquipmentReceptionAccesory.RollbackChanges()
            unitWorkTechnicalLogDetail.RollbackChanges()
            unitWorkTechnicalEquipmentSheet.RollbackChanges()
            unitWorkManualDetail.RollbackChanges()
            'unitWorkEquipmentReceptionParte.RollbackChanges()
            unitWorkEquipmentReceptionConsumable.RollbackChanges()
            'unitWorkPartsAccesoriesConsumables.RollbackChanges()
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    Public Function GetEquipmentRegistration(plate As String) As EquipmentRegistration Implements IEquipmentRegistrationAdminService.GetEquipmentRegistration
        If String.IsNullOrEmpty(plate) Then
            Throw New ArgumentNullException("Codigo vacio")
        End If
        Try

            Return _EquipmentRegistrationRepository.GetEquipmentRegistration(plate)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New EquipmentRegistration()
        End Try
    End Function

    Public Function ListAllEquipmentRegistration() As List(Of EquipmentRegistration) Implements IEquipmentRegistrationAdminService.ListAllEquipmentRegistration
        Try
            Return _EquipmentRegistrationRepository.ListAllEquipmentRegistration
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Funcion para registra un equipo
    ''' </summary>
    ''' <param name="EquipmentRegistration">oobj equipo</param>
    ''' <param name="idSequense">id de la secuencia numerica que maneja el frontal de equipos</param>
    ''' <param name="audit">auditoria</param>
    ''' <returns>actionresult</returns>
    ''' <remarks></remarks>
    Public Function SaveEquipmentRegistration(EquipmentRegistration As EquipmentRegistration, idSequense As Long, audit As AuditMessage) As ActionResult(Of EquipmentRegistration) Implements IEquipmentRegistrationAdminService.SaveEquipmentRegistration
        If EquipmentRegistration Is Nothing Then
            Throw New ArgumentNullException("Aseguradora vacio")
        End If
        Dim unitWork As IUnitWork = _EquipmentRegistrationRepository.UnitWork
        Dim sequenceUnitOfWork As IUnitWork = Me._sequenceDRepository.UnitWork
        Dim result As New ActionResult(Of EquipmentRegistration)
        Try
            'configuro la transaccion
            Dim txSettings As New TransactionOptions()
            txSettings.Timeout = TransactionManager.DefaultTimeout
            txSettings.IsolationLevel = IsolationLevel.ReadCommitted
            Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
                _EquipmentRegistrationRepository.SaveEntity(EquipmentRegistration)

                Dim physicalAsset As FixedAssetPhysicalAsset = _fixedAssetPhysicalAssetRepository.GetFixedAssetPhysicalAssetByPlate(EquipmentRegistration.Plate)
                physicalAsset.HasHighTech = True
                physicalAsset.MarkAsModified()

                _fixedAssetPhysicalAssetRepository.SaveEntity(physicalAsset)
                _fixedAssetPhysicalAssetRepository.UnitWork.Commit()
                unitWork.Commit()
                transaction.Complete()
            End Using
            Return New ActionResult(Of EquipmentRegistration) With {.StateResult = True, .ObjectEmbbeded = EquipmentRegistration}
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of EquipmentRegistration) With {.StateResult = False, .ObjectEmbbeded = EquipmentRegistration, .MessageResult = {ex.Message.ToString()}.ToList(), .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function ListAccesoryEquipmentType(Type As Integer) As List(Of AccesoryDetail) Implements IEquipmentRegistrationAdminService.ListAccesoryEquipmentType
        Try
            Return _EquipmentRegistrationRepository.ListAccesoryEquipmentType(Type)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function


    Public Function ListConsumableEquipmentType(Type As Integer) As List(Of ConsumableDetail) Implements IEquipmentRegistrationAdminService.ListConsumableEquipmentType
        Try
            Return _EquipmentRegistrationRepository.ListConsumableEquipmentType(Type)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function ListTechnicalLogDetailByFixedAssetPhysicalIdAndEquipmentRegistration(fixedAssetPhysicalAssetId As Integer, equipmentRegistration As Integer) As List(Of TechnicalLogDetail) Implements IEquipmentRegistrationAdminService.ListTechnicalLogDetailByFixedAssetPhysicalIdAndEquipmentRegistration
        Try
            Return _EquipmentRegistrationRepository.ListTechnicalLogDetailByFixedAssetPhysicalIdAndEquipmentRegistration(fixedAssetPhysicalAssetId, equipmentRegistration)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _sequenceDRepository = Nothing
            _EquipmentRegistrationRepository = Nothing
            _TechnicalLogDetailRepository = Nothing
            _EquipmentReceptionAccesoryRepository = Nothing
            _TechnicalEquipmentSheetRepository = Nothing
            _ManualDetailRepository = Nothing
            _EquipmentReceptionConsumableRepository = Nothing
            _DrawingsDetailRepository = Nothing
            '_PartsAccesoriesConsumablesRepository = Nothing
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
