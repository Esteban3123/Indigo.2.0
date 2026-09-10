'***********************************************************************
' Assembly         : Application.Glosas
' Author           : JulianCardozo
' Created          : 11-03-2011
'
' Last Modified By : JulianCardozo
' Last Modified On : 2013-04-06
'
' Last Modified By : Juan Diego Diaz
' Last Modified On : 2013-04-25
' Description      : Se agregó la auditoria
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Transactions
Imports Domain.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Entities
Imports Application.Base
Imports Domain.Common.Entities
Imports Domain.Security
Imports System.Data.Entity.Core
Imports System.Data.Entity.Infrastructure

#End Region
Public Class ResponsibleAdminService
    Implements IResponsibleAdminService

    Private _ResponsibleRepository As IResponsibleRepository
    Private _MovementGlosaRepository As IMovementGlosaRepository
    Private _PortFolioRepository As IPortfolioGlosadaRepository
    Private _GlosasServiceResponsible As GlosasResponsiblesService
    Private _BlockRecordRepository As IBlockRecordRepository

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase <see cref="ResponsibleAdminService" />.
    ''' </summary>
    ''' <param name="ResponsibleRepository">el repositorio para el manejo de los Responsibles.</param>
    ''' <param name="MovementGlosaRepository">el repositorio para el manejo de los Movimientos de Glosa.</param>
    Public Sub New(ByVal ResponsibleRepository As IResponsibleRepository, ByVal MovementGlosaRepository As IMovementGlosaRepository, ByVal PortFolioRepository As IPortfolioGlosadaRepository, ByVal BlockRecordRepository As IBlockRecordRepository, ByVal GlosasServiceResponsible As IGlosasResponsiblesService)
        If ResponsibleRepository Is Nothing Then
            Throw New ArgumentNullException("ResponsibleRepository Vacío")
        End If
        If MovementGlosaRepository Is Nothing Then
            Throw New ArgumentNullException("MovementGlosaRepository Vacío")
        End If
        If PortFolioRepository Is Nothing Then
            Throw New ArgumentNullException("PortFolioRepository Vacío")
        End If
        If GlosasServiceResponsible Is Nothing Then
            Throw New ArgumentNullException("GlosasServiceResponsible Vacío")
        End If

        _ResponsibleRepository = ResponsibleRepository
        _MovementGlosaRepository = MovementGlosaRepository
        _PortFolioRepository = PortFolioRepository
        _BlockRecordRepository = BlockRecordRepository
        _GlosasServiceResponsible = GlosasServiceResponsible

        'Me._GlosasServiceResponsible = New GlosasResponsiblesService(Me._MovementGlosaRepository, Me._PortFolioRepository, Me._BlockRecordRepository)

    End Sub

    Public Function DeleteResponsible(Responsible As Responsible, audit As AuditMessage) As ActionResult Implements IResponsibleAdminService.DeleteResponsible
        If Responsible Is Nothing Then
            Throw New ArgumentNullException("Responsible Vacio")
        End If
        Dim unitOfWork As IUnitWork = _ResponsibleRepository.UnitWork
        Try
            'Elimino el responsable
            Dim auditProcess As IndigoAuditSimpleEntity(Of Responsible)
            Responsible.ModificationUser = audit.CodeUser
            Responsible.ModificationDate = Date.Now()
            _ResponsibleRepository.SaveEntity(Responsible)
            unitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of Responsible)(Responsible, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditProcess.Execute()

            '/***** Auditoria Basica ********/
            '   IndigoAuditBasic.Execute("Responsible", audit.Functional, Responsible.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            '/*****Auditoria Avanzada ******/
            '  IndigoAuditSimpleEntity(Of Responsible).Execute(Responsible, audit, Infrastructure.CrossCutting.Audit.Actions.Delete, audit.Company)
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False}
        End Try
    End Function

    Public Function GetResponsible(codeResponsible As String, audit As AuditMessage) As Responsible Implements IResponsibleAdminService.GetResponsible
        If String.IsNullOrEmpty(codeResponsible) = True Then
            Throw New ArgumentNullException("codeResponsible Vacio")
        End If
        Try
            Dim Responsible = _ResponsibleRepository.GetResponsible(codeResponsible)
            If Responsible.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of Responsible)(Responsible, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return Responsible
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function ListResponsibleedConceptAll(audit As AuditMessage) As List(Of ResponsibleAll) Implements IResponsibleAdminService.ListResponsibleAll
        Try
            Dim Responsible = _ResponsibleRepository.ListResponsibleAll
            If Responsible.Count > 0 Then
                For Each item As ResponsibleAll In Responsible
                    Dim auditObject As New IndigoAuditSimpleEntity(Of ResponsibleAll)(item, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                    auditObject.Execute()
                Next
            End If
            Return Responsible
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveResponsible(Responsible As Responsible, audit As AuditMessage) As ActionResult(Of Responsible) Implements IResponsibleAdminService.SaveResponsible
        If Responsible Is Nothing Then
            Throw New ArgumentNullException("Responsible Vacio")
        End If
        Dim unitOfWork As IUnitWork = _ResponsibleRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of Responsible)
            Dim AuxResponsible As Responsible = Nothing
            Dim status As Integer
            If Responsible.ChangeTracker.State = ObjectState.Added Then
                Dim ListResponsible As List(Of Responsible) = _ResponsibleRepository.GetListValidateResponsibleByCodeUSer(Responsible.CodeUser)
                If ListResponsible.Count > 0 Then
                    Return New ActionResult(Of Responsible) With {.StateResult = False, .MessageResult = New List(Of String)({"El codigo " & Responsible.CodeUser & " de Login de Usuario ya existe para otro responsable: " & ListResponsible(0).Name})}
                End If
            End If

            If Responsible.ChangeTracker.State = ObjectState.Modified Then
                AuxResponsible = Responsible.OriginalValue
                Responsible.ModificationUser = audit.CodeUser
                Responsible.ModificationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            ElseIf Responsible.ChangeTracker.State = ObjectState.Added Then
                Responsible.CreationUser = audit.CodeUser
                Responsible.CreationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            End If
            _ResponsibleRepository.SaveEntity(Responsible)
            'Confirmo la unidad de trabajo
            unitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of Responsible)(Responsible, audit, status, AuxResponsible)
            auditProcess.Execute()

            'If (Responsible.ChangeTracker.State = ObjectState.Added) Then
            '    '/***** Auditoria Basica ********/
            '    IndigoAuditBasic.Execute("Responsible", audit.Functional, Responsible.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
            '    '/*****Auditoria Avanzada ******/
            '    IndigoAuditSimpleEntity(Of Responsible).Execute(Responsible, audit, Infrastructure.CrossCutting.Audit.Actions.Insert, audit.Company)
            'ElseIf Responsible.ChangeTracker.State = ObjectState.Modified Then
            '    '/***** Auditoria Basica ********/
            '    IndigoAuditBasic.Execute("Responsible", audit.Functional, Responsible.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
            '    '/*****Auditoria Avanzada ******/
            '    IndigoAuditSimpleEntity(Of Responsible).Execute(Responsible, audit, Infrastructure.CrossCutting.Audit.Actions.Update, audit.Company, AuxResponsible)
            'End If
            Return New ActionResult(Of Responsible) With {.StateResult = True, .ObjectEmbbeded = Responsible}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of Responsible) With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Responsible) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' consulta un Responsible especifico
    ''' </summary>
    ''' <param name="codeResponsible">el codigo ERP del Responsible</param>
    ''' <returns></returns>
    Public Function GetResponsibleByCodeERP(codeResponsible As String, audit As AuditMessage) As Responsible Implements IResponsibleAdminService.GetResponsibleByCodeERP
        If String.IsNullOrEmpty(codeResponsible) = True Then
            Throw New ArgumentNullException("codeERPResponsible Vacio")
        End If
        Try
            Dim Responsible = _ResponsibleRepository.GetResponsibleByCodeERP(codeResponsible)
            If Responsible.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of Responsible)(Responsible, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return Responsible
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Función para listar todos los responsables que 
    ''' puede ser reasignados
    ''' </summary>
    ''' <param name="IdResponsible">Id del Responsable</param>
    ''' <returns>Lista de objetos ResponsibleMovements</returns>
    Public Function listAllResponsiblesTransfer(IdResponsible As String) As List(Of ResponsibleMovements) Implements IResponsibleAdminService.listAllResponsiblesTransfer
        If String.IsNullOrEmpty(IdResponsible) = True Then
            Throw New ArgumentNullException("IdResponsible Vacío")
        End If
        Return Me._GlosasServiceResponsible.listAllResponsiblesTransfer(IdResponsible)
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _ResponsibleRepository = Nothing
            _MovementGlosaRepository = Nothing
            _PortFolioRepository = Nothing
            _BlockRecordRepository = Nothing
            _GlosasServiceResponsible = Nothing
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
