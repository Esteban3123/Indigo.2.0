'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Yoe Andres Cardenas
' Created          : 06-06-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Data.Entity.Infrastructure
Imports System.Text
Imports System.Transactions
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources
Imports System.Linq
#End Region

Public Class PackageAdminService
    Implements IPackageAdminService, Inject

#Region "Properties"
    Private Const FORM_NAME As String = "FrmPackage"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _packageRepository As IPackageRepository

    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDetailRepository As IMixingStationSequenceDetailRepository

#End Region

#Region "Methods"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal packageRepository As IPackageRepository, ByVal secuenseDetailRepository As IMixingStationSequenceDetailRepository)
        If packageRepository Is Nothing Then
            Throw New ArgumentNullException("workCenterRepository Vacio")
        End If
        If secuenseDetailRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDetailRepository")
        End If
        Me._packageRepository = packageRepository
        Me._secuenseDetailRepository = secuenseDetailRepository
    End Sub

    ''' <summary>
    ''' Trae todos los paquetes
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllPackage(audit As AuditMessage) As List(Of Package) Implements IPackageAdminService.ListAllPackage
        Try
            Dim package = Me._packageRepository.GetAll()
            For Each item As Package In package
                Dim auditObject As New IndigoAuditSimpleEntity(Of Package)(item, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            Next
            Return package
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista todos los paquetes duplicados
    ''' </summary>
    ''' <param name="packageId"></param>
    ''' <param name="packageDetailTmp"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ListDuplicatePackage(packageId As Integer, packageDetailTmp As List(Of Tuple(Of Byte, Integer)), audit As AuditMessage) As List(Of PackageDto) Implements IPackageAdminService.ListDuplicatePackage
        Try
            Dim package = Me._packageRepository.ListDuplicatePackage(packageId, packageDetailTmp)
            For Each item As PackageDto In package
                Dim auditObject As New IndigoAuditSimpleEntity(Of PackageDto)(item, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            Next
            Return package
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of PackageDto)
        End Try
    End Function

    ''' <summary>
    ''' Elimina un paquete por id
    ''' </summary>
    ''' <param name="package">The identifier.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Function DeletePackage(package As Package, audit As AuditMessage) As ActionResult Implements IPackageAdminService.DeletePackage
        If package Is Nothing Then
            Throw New ArgumentNullException("Package")
        End If
        Dim unitOfWork As IUnitWork = Me._packageRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                package.ModificationUser = audit.CodeUser
                package.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of Package)(package, audit, status)
                package.MarkAsDeleted()
                Me._packageRepository.SaveEntity(package)
                unitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using

        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-999"}), .Message = ResourceManager.GetString("ErrorConcurrence")}

        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}

        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}

        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try

    End Function

    ''' <summary>
    ''' Obtiene el paquete por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Function GetPackage(code As String, audit As AuditMessage) As ActionResult(Of Package) Implements IPackageAdminService.GetPackage
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim package As Package = Me._packageRepository.GetPackage(code)
            If package IsNot Nothing AndAlso package.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of Package)(package, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of Package) With {.StateResult = True, .ObjectEmbbeded = package}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Package) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un paquete por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Function GetPackageById(id As Integer, audit As AuditMessage) As ActionResult(Of Package) Implements IPackageAdminService.GetPackageById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim package As Package = Me._packageRepository.GetPackageById(id)
            If package IsNot Nothing AndAlso package.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of Package)(package, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of Package) With {.StateResult = True, .ObjectEmbbeded = package}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Package) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza un paquete
    ''' </summary>
    ''' <param name="package"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SavePackage(package As Package, audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of Package) Implements IPackageAdminService.SavePackage
        If package Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._packageRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDetailRepository.UnitWork

        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                Dim xml = ConvertEntityToXml(package)

                Dim result = _packageRepository.SP_SavePackage(xml, audit.CodeUser)
                If result.CodeMessage <> 0 Then
                    scope.Dispose()
                    Return New ActionResult(Of Package) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = result.Message}
                End If

                package.Id = result.Id
                package.Code = result.Code

                'Se marca la entidad como sin cambios
                package.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of Package) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = package, .Message = result.Message}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of Package) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Package) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Convierte la entidad a xml
    ''' </summary>
    Private Function ConvertEntityToXml(Package As Package) As Object
        Dim builder As New StringBuilder

        builder.Append("<Data>")

        With Package
            builder.Append("<Id>" & .Id & "</Id>")
            builder.Append("<Code>" & .Code & "</Code>")
            builder.Append("<Name>" & .Name & "</Name>")
            builder.Append("<Description>" & .Description & "</Description>")
            builder.Append("<RiskLevelId>" & .RiskLevelId & "</RiskLevelId>")
            builder.Append("<Storage>" & .Storage & "</Storage>")
            builder.Append("<CodeAlternative>" & .CodeAlternative & "</CodeAlternative>")
            builder.Append("<CodeAlternativeTwo>" & .CodeAlternativeTwo & "</CodeAlternativeTwo>")
            builder.Append("<ProductGroupId>" & .ProductGroupId & "</ProductGroupId>")
            builder.Append("<ProductSubGroupId>" & .ProductSubGroupId & "</ProductSubGroupId>")
            builder.Append("<ManufacturerId>" & .ManufacturerId & "</ManufacturerId>")
            builder.Append("<CodeSICE>" & .CodeSICE & "</CodeSICE>")
            builder.Append("<POSProduct>" & .POSProduct & "</POSProduct>")
            builder.Append("<BillingGroupId>" & .BillingGroupId & "</BillingGroupId>")
            builder.Append("<ProductControl>" & .ProductControl & "</ProductControl>")
            builder.Append("<ProductWithPriceControl>" & .ProductWithPriceControl & "</ProductWithPriceControl>")
            builder.Append("<AuthorizationByOrderNumber>" & .AuthorizationByOrderNumber & "</AuthorizationByOrderNumber>")
            builder.Append("<MaximumControlPeriod>" & .MaximumControlPeriod & "</MaximumControlPeriod>")
            builder.Append("<OsmolarityTotal>" & .OsmolarityTotal & "</OsmolarityTotal>")
            builder.Append("<VolumeTotalOrder>" & .VolumeTotalOrder & "</VolumeTotalOrder>")
            builder.Append("<VolumeTotalOrderMeasurementUnitId>" & .VolumeTotalOrderMeasurementUnitId & "</VolumeTotalOrderMeasurementUnitId>")
            builder.Append("<VolumeTotalOrderPurga>" & .VolumeTotalOrderPurga & "</VolumeTotalOrderPurga>")
            builder.Append("<WeightTotalSolution>" & .WeightTotalSolution & "</WeightTotalSolution>")
            builder.Append("<State>" & .State & "</State>")
            builder.Append("<MeasurementUnitId>" & .MeasurementUnitId & "</MeasurementUnitId>")

            If .TypeStability IsNot Nothing Then
                builder.Append("<TypeStability>" & .TypeStability & "</TypeStability>")
                builder.Append("<StabilityHour>" & .StabilityHour.ToString & "</StabilityHour>")
                builder.Append("<StabilityDays>" & .StabilityDays & "</StabilityDays>")
            End If

            builder.Append("<EnvironmentalTemperatureTerm>" & .EnvironmentalTemperatureTerm & "</EnvironmentalTemperatureTerm>")
            builder.Append("<Purge>" & .Purge & "</Purge>")
            builder.Append("<InfusionSpeed>" & .InfusionSpeed & "</InfusionSpeed>")
            builder.Append("<PreparationInstructions>" & .PreparationInstructions & "</PreparationInstructions>")
            builder.Append("<SpecialConsiderations>" & .SpecialConsiderations & "</SpecialConsiderations>")
            builder.Append("<UnitDoseTypeId>" & .UnitDoseTypeId & "</UnitDoseTypeId>")
            builder.Append("<Concentration>" & .Concentration & "</Concentration>")
            builder.Append("<ConcentrationMeasurementUnitId>" & .ConcentrationMeasurementUnitId & "</ConcentrationMeasurementUnitId>")
            builder.Append("<ATCId>" & .ATCId & "</ATCId>")
            builder.Append("<ProductId>" & .ProductId & "</ProductId>")
            builder.Append("<Justification>" & .Justification & "</Justification>")
            builder.Append("<StandardMix>" & .StandardMix & "</StandardMix>")
            builder.Append("<PhotoProtection>" & .PhotoProtection & "</PhotoProtection>")
            builder.Append("<PreparationType>" & .PreparationType & "</PreparationType>")
            builder.Append("<PersonalizedMasterPreparation>" & .PersonalizedMasterPreparation & "</PersonalizedMasterPreparation>")
            builder.Append("<AssociatedPackageId>" & .AssociatedPackageId & "</AssociatedPackageId>")
            builder.Append("<LabelType>" & .LabelType & "</LabelType>")
            builder.Append("<OperatingUnitId>" & .OperatingUnitId & "</OperatingUnitId>")
            builder.Append("<IsPackagePersonalized>" & .IsPackagePersonalized & "</IsPackagePersonalized>")
            builder.Append("<VehicleOptimization>" & .VehicleOptimization & "</VehicleOptimization>")
            builder.Append("<Readjustments>" & .Readjustments & "</Readjustments>")
            builder.Append("<ConcentrationAntibiotic>" & .ConcentrationAntibiotic & "</ConcentrationAntibiotic>")
            builder.Append("<VolumeTotalPrepared>" & .VolumeTotalPrepared & "</VolumeTotalPrepared>")
            builder.Append("<MeasurementPreparedId>" & .MeasurementPreparedId & "</MeasurementPreparedId>")
            builder.Append("<NptId>" & .NptId & "</NptId>")
            If .NptId IsNot Nothing Then
                builder.Append("<MainDrugId>" & .MainDrugId & "</MainDrugId>")
            End If

            If .PackageDetail IsNot Nothing AndAlso .PackageDetail.Count > 0 Then
                For Each item In .PackageDetail
                    builder.Append("<Details>")
                    builder.Append("<Id>" & item.Id & "</Id>")
                    builder.Append("<PackageId>" & item.PackageId & "</PackageId>")
                    builder.Append("<ProductId>" & item.ProductId & "</ProductId>")
                    builder.Append("<Quantity>" & item.Quantity & "</Quantity>")
                    builder.Append("<MeasurementUnitId>" & item.MeasurementUnitId & "</MeasurementUnitId>")
                    builder.Append("<Volume>" & item.Volume & "</Volume>")
                    builder.Append("<VolumeMeasureUnit>" & item.VolumeMeasureUnit & "</VolumeMeasureUnit>")
                    builder.Append("<Thinner>" & item.Thinner & "</Thinner>")
                    builder.Append("<Vehicle>" & item.Vehicle & "</Vehicle>")
                    builder.Append("<Osmolarity>" & item.Osmolarity & "</Osmolarity>")
                    builder.Append("<Density>" & item.Density & "</Density>")
                    builder.Append("<AtcId>" & item.AtcId & "</AtcId>")
                    builder.Append("<SupplieId>" & item.SupplieId & "</SupplieId>")
                    builder.Append("<ComponentType>" & item.ComponentType & "</ComponentType>")
                    builder.Append("<MainMedicine>" & item.MainMedicine & "</MainMedicine>")
                    builder.Append("<PreparationType>" & item.PreparationType & "</PreparationType>")
                    builder.Append("<Dilution>" & item.Dilution & "</Dilution>")
                    builder.Append("<Concentration>" & item.Concentration & "</Concentration>")
                    builder.Append("<AmountTime>" & item.AmountTime & "</AmountTime>")
                    builder.Append("<TimeUnit>" & item.TimeUnit & "</TimeUnit>")
                    builder.Append("<VolumeTotal>" & item.VolumeTotal & "</VolumeTotal>")
                    builder.Append("<NPTItemOrder>" & item.NPTItemOrder & "</NPTItemOrder>")
                    builder.Append("<ComplementaryMedicine>" & item.ComplementaryMedicine & "</ComplementaryMedicine>")
                    builder.Append("<IsDelete>" & If(item.ChangeTracker.State = ObjectState.Deleted, 1, 0) & "</IsDelete>")
                    builder.Append("</Details>")

                Next
            End If

            If .ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("PackageDetail") Then
                For Each item As PackageDetail In .ChangeTracker.ObjectsRemovedFromCollectionProperties.Item("PackageDetail")
                    builder.Append("<Details>")
                    builder.Append("<Id>" & item.Id & "</Id>")
                    builder.Append("<PackageId>" & item.PackageId & "</PackageId>")
                    builder.Append("<ProductId>" & item.ProductId & "</ProductId>")
                    builder.Append("<Quantity>" & item.Quantity & "</Quantity>")
                    builder.Append("<MeasurementUnitId>" & item.MeasurementUnitId & "</MeasurementUnitId>")
                    builder.Append("<Volume>" & item.Volume & "</Volume>")
                    builder.Append("<VolumeMeasureUnit>" & item.VolumeMeasureUnit & "</VolumeMeasureUnit>")
                    builder.Append("<Thinner>" & item.Thinner & "</Thinner>")
                    builder.Append("<Vehicle>" & item.Vehicle & "</Vehicle>")
                    builder.Append("<Osmolarity>" & item.Osmolarity & "</Osmolarity>")
                    builder.Append("<Density>" & item.Density & "</Density>")
                    builder.Append("<AtcId>" & item.AtcId & "</AtcId>")
                    builder.Append("<SupplieId>" & item.SupplieId & "</SupplieId>")
                    builder.Append("<ComponentType>" & item.ComponentType & "</ComponentType>")
                    builder.Append("<MainMedicine>" & item.MainMedicine & "</MainMedicine>")
                    builder.Append("<PreparationType>" & item.PreparationType & "</PreparationType>")
                    builder.Append("<Dilution>" & item.Dilution & "</Dilution>")
                    builder.Append("<Concentration>" & item.Concentration & "</Concentration>")
                    builder.Append("<AmountTime>" & item.AmountTime & "</AmountTime>")
                    builder.Append("<TimeUnit>" & item.TimeUnit & "</TimeUnit>")
                    builder.Append("<VolumeTotal>" & item.VolumeTotal & "</VolumeTotal>")
                    builder.Append("<NPTItemOrder>" & item.NPTItemOrder & "</NPTItemOrder>")
                    builder.Append("<ComplementaryMedicine>" & item.ComplementaryMedicine & "</ComplementaryMedicine>")
                    builder.Append("<IsDelete>" & 1 & "</IsDelete>")
                    builder.Append("</Details>")
                Next
            End If
        End With

        builder.Append("</Data>")

        Return builder.ToString()
    End Function

    ''' <summary>
    ''' Actualiza el estado de un paquete
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateStatePackage(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of Package) Implements IPackageAdminService.UpdateStatePackage
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
            Dim package As Package = Me._packageRepository.GetPackage(code)
            If package IsNot Nothing AndAlso package.Id > 0 Then
                package.State = state
            End If
            Return Me.SavePackage(package, audit)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Package) With {.StatusCode = eStatusResult.EXCEPTION, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un paquete asociado a un proceso de producción
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Function GetProductionPackage(code As String, audit As AuditMessage) As Boolean Implements IPackageAdminService.GetProductionPackage
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim package = Me._packageRepository.GetProductionPackage(code)
            Return package
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
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

            _packageRepository = Nothing
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

#Region "Properties"

#End Region
End Class
