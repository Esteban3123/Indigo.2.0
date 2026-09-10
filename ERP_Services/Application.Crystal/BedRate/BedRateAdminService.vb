'***********************************************************************
' Assembly         : Application.Crystal
' Author           : Diego Roldán Lozano
' Created          : 22-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Crystal
Imports Domain.Crystal.Service
Imports System.Data.Entity.Infrastructure
Imports Application.Billing
Imports System.Data.Entity.Core
Imports System.Transactions
Imports System.Dynamic
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Crystal.Entities
Imports Application.Base

#End Region

Public Class BedRateAdminService
    Implements IBedRateAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de unidad funcional
    ''' </summary>
    Private _bedRateRepository As IBedRateRepository
    Private _cupsEntityRepository As ICupsEntityRepository
    Private _adcenatenRepository As IADCENATENRepository
    Private _inunifunRepository As IINUNIFUNCRepository
    Private _chtipestaRepository As ICHTIPESTARepository
    Private _bedRepository As IBedRepository

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(bedRateRepository As IBedRateRepository, cupsEntityRepository As ICupsEntityRepository, adcenatenRepository As IADCENATENRepository,
                   inunifunRepository As IINUNIFUNCRepository, chtipestaRepository As ICHTIPESTARepository, bedRepository As IBedRepository)
        _bedRateRepository = bedRateRepository
        _cupsEntityRepository = cupsEntityRepository
        _adcenatenRepository = adcenatenRepository
        _inunifunRepository = inunifunRepository
        _chtipestaRepository = chtipestaRepository
        _bedRepository = bedRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Elimina una tarifa para una cama
    ''' </summary>
    ''' <param name="bedRate">The bed rate.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">bedRate</exception>
    Public Function DeleteBedRate(bedRate As Domain.Crystal.Entities.CHGENTARI) As ActionResult Implements IBedRateAdminService.DeleteBedRate
        If bedRate Is Nothing Then
            Throw New ArgumentNullException("bedRate")
        End If
        Dim unitOfWork As IUnitWork = Me._bedRateRepository.UnitWork
        Try
            bedRate.MarkAsDeleted()
            Me._bedRateRepository.SaveEntity(bedRate)
            unitOfWork.Commit()
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .Message = ResourceManager.GetString("ErrorConcurrence"), .MessageResult = New List(Of String)({"-999"})}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .Message = ResourceManager.GetString("ErrorDependence"), .MessageResult = New List(Of String)({"-000"})}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .Message = ResourceManager.GetString("ErrorDependence"), .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' obtiene una tarifa de cama por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">code</exception>
    Public Function GetBedRatebyCode(code As Integer) As CHGENTARI Implements IBedRateAdminService.GetBedRatebyCode
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        Try
            Dim bedRate As CHGENTARI = Me._bedRateRepository.GetBedRatebyCode(code)
            If bedRate IsNot Nothing AndAlso bedRate.CODCONCEC > 0 Then
                If bedRate.GENCUPS IsNot Nothing Then
                    Dim cupsEntity As CUPSEntity = _cupsEntityRepository.GetCupsEntityById(bedRate.GENCUPS.Value)
                    bedRate.CupsEntityObservationFullName = String.Concat(cupsEntity.Code, " - ", cupsEntity.Description)
                End If
                Dim cupsEntity2 As CUPSEntity = _cupsEntityRepository.GetCupsEntityById(bedRate.GENCUPS2)
                bedRate.CupsEntityHospitalizationFullName = String.Concat(cupsEntity2.Code, " - ", cupsEntity2.Description)
            End If
            Return bedRate
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' obtiene una tariga de cama por codigo
    ''' </summary>
    Public Function GetBGetBedRatebyBedCodeedRatebyCode(code As Integer) As ActionResult(Of List(Of Domain.Crystal.Entities.CHGENTARI)) Implements IBedRateAdminService.GetBedRatebyBedCode
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        Try
            Dim bedRate As List(Of CHGENTARI) = Me._bedRateRepository.GetBedRatebyBedCode(code)
            If bedRate IsNot Nothing AndAlso bedRate.Count > 0 Then
                For Each item In bedRate
                    If item.GENCUPS IsNot Nothing Then
                        Dim cupsEntity As CUPSEntity = _cupsEntityRepository.GetCupsEntityById(item.GENCUPS.Value)
                        item.CupsEntityObservationFullName = String.Concat(cupsEntity.Code, " - ", cupsEntity.Description)
                    End If
                    Dim cupsEntity2 As CUPSEntity = _cupsEntityRepository.GetCupsEntityById(item.GENCUPS2)
                    item.CupsEntityHospitalizationFullName = String.Concat(cupsEntity2.Code, " - ", cupsEntity2.Description)
                Next
            End If
            Return New ActionResult(Of List(Of CHGENTARI)) With {.StateResult = True, .ObjectEmbbeded = bedRate}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of CHGENTARI)) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Guarda una tarifa para una cama
    ''' </summary>
    Public Function SaveBedRate(bedRate As Domain.Crystal.Entities.CHGENTARI) As ActionResult(Of Domain.Crystal.Entities.CHGENTARI) Implements IBedRateAdminService.SaveBedRate
        If bedRate Is Nothing Then
            Throw New ArgumentNullException("bedRate")
        End If
        Dim unitOfWork As IUnitWork = Me._bedRateRepository.UnitWork

        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                With bedRate
                    If .CHCAMASHO Is Nothing OrElse .CHCAMASHO.CODICAMAS <> bedRate.CODICAMASExt Then
                        .CHCAMASHO = _bedRepository.GetBedbyCode(bedRate.CODICAMASExt)
                    End If
                    If .CHTIPESTA Is Nothing OrElse .CHTIPESTA.CODTIPEST <> bedRate.CODTIPESTExt Then
                        .CHTIPESTA = _chtipestaRepository.GetCHTIPESTAByCode(bedRate.CODTIPESTExt)
                    End If
                    If .ADCENATEN Is Nothing OrElse .ADCENATEN.CODCENATE <> bedRate.CODCENATEExt Then
                        .ADCENATEN = _adcenatenRepository.GetADCENATENByCode(bedRate.CODCENATEExt, True)
                    End If
                    If .INUNIFUNC Is Nothing OrElse .INUNIFUNC.UFUCODIGO <> bedRate.UFUCODIGOExt Then
                        .INUNIFUNC = _inunifunRepository.GetINUNIFUNCByCode(bedRate.UFUCODIGOExt)
                    End If
                    '.CHCAMASHO = _bedRepository.GetBedbyCode(bedRate.CODICAMASExt)
                    '.CHTIPESTA = _chtipestaRepository.GetCHTIPESTAByCode(bedRate.CODTIPESTExt)
                    '.ADCENATEN = _adcenatenRepository.GetADCENATENByCode(bedRate.CODCENATEExt, True)
                    '.INUNIFUNC = _inunifunRepository.GetINUNIFUNCByCode(bedRate.UFUCODIGOExt)
                End With

                Me._bedRateRepository.SaveEntity(bedRate)
                unitOfWork.Commit()

                scope.Complete()
                Return New ActionResult(Of CHGENTARI) With {.StateResult = True, .ObjectEmbbeded = bedRate}
            End Using

        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChangesUnitOfWork()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CHGENTARI) With {.StateResult = False, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorUnknown")}
        Catch ex As Exception
            unitOfWork.RollbackChangesUnitOfWork()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CHGENTARI) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
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
            _bedRateRepository = Nothing
            _cupsEntityRepository = Nothing
            _adcenatenRepository = Nothing
            _inunifunRepository = Nothing
            _chtipestaRepository = Nothing
            _bedRepository = Nothing
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
