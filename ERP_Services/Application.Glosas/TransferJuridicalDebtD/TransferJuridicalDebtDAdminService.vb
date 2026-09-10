'***********************************************************************
' Assembly         : Application.Glosas
' Author           : Juan Diego Diaz
' Created          : 15-10-2013
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
Imports Domain.Common.Entities
Imports Application.Base
Imports System.Data.Entity.Core

#End Region

''' <summary>
''' Servicio de Traslado Cobro Jurídico Detalle.
''' </summary>
''' <remarks></remarks>
Public Class TransferJuridicalDebtDAdminService
    Implements ITransferJuridicalDebtDAdminService

    Private _JuridicalDReceptionDRepository As ITransferJuridicalDebtDRepository
    Private _PortFolioRepository As IPortfolioGlosadaRepository
    ''' <summary>
    ''' 
    ''' </summary>
    Private _SettingPortfolioRepository As ISettingPortfolioRepository

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase <see cref="TransferJuridicalDebtDAdminService" />.
    ''' </summary>
    ''' <param name="JuridicalDReceptionDRepository">El repositorio para el manejo de los detalles de traslado cobro jurídico.</param>
    '''  <param name="PortFolioRepository">El repositorio para el manejo de la cartera.</param>
    Public Sub New(ByVal JuridicalDReceptionDRepository As ITransferJuridicalDebtDRepository, ByVal PortFolioRepository As IPortfolioGlosadaRepository, settingPortfolioRepository As ISettingPortfolioRepository)
        If JuridicalDReceptionDRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de detalle traslado cobro jurídico vacío")
        End If
        If PortFolioRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de cartera vacío")
        End If
        If settingPortfolioRepository Is Nothing Then
            Throw New ArgumentException("Repositorio de parámetros de cuentas por cobrar vacio")
        End If
        _JuridicalDReceptionDRepository = JuridicalDReceptionDRepository
        _PortFolioRepository = PortFolioRepository
        _SettingPortfolioRepository = settingPortfolioRepository
    End Sub

    ''' <summary>
    ''' Borrar Traslado Cobro Jurídico Detalle.
    ''' </summary>
    ''' <param name="JuridicalD">Objeto Traslado Cobro Jurídico Detalle</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Action Result</returns>
    Public Function DeleteJuridicalD(JuridicalD As List(Of TransferJuridicalDebtCollectionD), audit As AuditMessage) As ActionResult Implements ITransferJuridicalDebtDAdminService.DeleteJuridicalD
        If JuridicalD Is Nothing Then
            Throw New ArgumentNullException("Detalle Traslado Cobro Jurídico Vacío")
        End If
        Dim unitOfWork As IUnitWork = _JuridicalDReceptionDRepository.UnitWork
        Try
            Dim count As Integer = JuridicalD.Count - 1
            For i As Integer = 0 To count
                'Elimino el detalle de traslado cobro jurídico.
                _JuridicalDReceptionDRepository.DeleteEntity(JuridicalD(0))
            Next
            unitOfWork.Commit()
            For i As Integer = 0 To count
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute("TransferJuridicalDebtReceptionD", audit.Functional, JuridicalD(0).Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, Infrastructure.CrossCutting.Base.ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            Next
            Return New ActionResult With {.StateResult = True}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList()}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una traslado cobro jurídico detalle según código.
    ''' </summary>
    ''' <param name="Id">Id Traslado Cobro Jurídico detalle</param>
    ''' <returns>Objeto Traslado Cobro Jurídico Detalle</returns>
    Public Function GetJuridicalDByIdJuridicalD(Id As String) As TransferJuridicalDebtCollectionD Implements ITransferJuridicalDebtDAdminService.GetJuridicalDByIdJuridicalD
        If String.IsNullOrEmpty(Id) = True Then
            Throw New ArgumentNullException("Id vacío")
        End If
        Try
            Return _JuridicalDReceptionDRepository.GetTransferJuridicalDebtD(Id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene todos los registros de traslado cobro jurídico detalle.
    ''' </summary>
    ''' <returns>Lista Traslado Cobro Jurídico Detalle</returns>
    Public Function ListAllJuridicalD() As List(Of TransferJuridicalDebtCollectionD) Implements ITransferJuridicalDebtDAdminService.ListAllJuridicalD
        Try
            Return _JuridicalDReceptionDRepository.ListAllTransferJuridicalDebtD()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una traslado cobro jurídico detalle según código.
    ''' </summary>
    ''' <param name="Id">Id Traslado Cobro Jurídico Cabecera</param>
    ''' <returns>Objeto Traslado Cobro Jurídico Detalle</returns>
    Public Function ListJuridicalDDByIdJuridicalC(Id As String) As List(Of TransferJuridicalDebtCollectionD) Implements ITransferJuridicalDebtDAdminService.ListJuridicalDDByIdJuridicalC
        If String.IsNullOrEmpty(Id) = True Then
            Throw New ArgumentNullException("Id vacío")
        End If
        Try
            Return _JuridicalDReceptionDRepository.ListTransferJuridicalDByIdTransferJuridicalC(Id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guardar Traslado Cobro Jurídico Detalle
    ''' </summary>
    ''' <param name="JuridicalD">Objeto Traslado Cobro Jurídico Detalle</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Action Result</returns>
    Public Function SaveJuridicalD(JuridicalD As List(Of TransferJuridicalDebtCollectionD), audit As AuditMessage) As ActionResult Implements ITransferJuridicalDebtDAdminService.SaveJuridicalD
        If JuridicalD Is Nothing Then
            Throw New ArgumentNullException("Detalle Traslado Cobro Jurídico Vacío")
        End If
        Dim unitOfWork As IUnitWork = _JuridicalDReceptionDRepository.UnitWork
        Try
            Dim j As Integer = 0
            Do
                If JuridicalD(j).ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    _JuridicalDReceptionDRepository.UpdateEntity(JuridicalD(j))
                ElseIf JuridicalD(j).ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    _JuridicalDReceptionDRepository.AddEntity(JuridicalD(j))
                ElseIf JuridicalD(j).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted Then
                    _JuridicalDReceptionDRepository.DeleteEntity(JuridicalD(j))
                    j = j - 1
                End If
                j = j + 1
            Loop While j < JuridicalD.Count - 1
            'Confirmo la unidad de trabajo
            unitOfWork.Commit()

            j = 0
            Do
                If JuridicalD(j).ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    '/***** Auditoria Basica ********/
                    IndigoAuditBasic.Execute("TransferJuridicalDebtReceptionD", audit.Functional, JuridicalD(0).Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, Infrastructure.CrossCutting.Base.ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
                ElseIf JuridicalD(j).ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    '/***** Auditoria Basica ********/
                    IndigoAuditBasic.Execute("TransferJuridicalDebtReceptionD", audit.Functional, JuridicalD(0).Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, Infrastructure.CrossCutting.Base.ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
                ElseIf JuridicalD(j).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted Then
                    '/***** Auditoria Basica ********/
                    IndigoAuditBasic.Execute("TransferJuridicalDebtReceptionD", audit.Functional, JuridicalD(0).Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, Infrastructure.CrossCutting.Base.ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
                    j = j - 1
                End If
                j = j + 1
            Loop While j < JuridicalD.Count - 1

            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = True, .MessageResult = {ex.Message}.ToList()}
        End Try
    End Function

    ''' <summary>
    ''' Función para validar y agregar facturas
    ''' </summary>
    ''' <param name="ListInvoices">Lista de facturas</param>
    ''' <param name="Nit">Numero de Nit</param>
    ''' <param name="_IdUnitoperating">Unidad operativa</param>
    ''' <returns>Lista de Conciliacion Detalle</returns>
    Public Function ValidateListInvoiceJuridical(ListInvoices As List(Of String), Nit As String, ByVal _IdUnitoperating As Integer) As List(Of TransferJuridicalDebtCollectionD) Implements ITransferJuridicalDebtDAdminService.ValidateListInvoiceJuridical
        If ListInvoices.Count = 0 Then
            Throw New ArgumentNullException("Lista de facturas vacía")
        End If
        If Nit Is String.Empty Then
            Throw New ArgumentNullException("Nit vacío")
        End If
        Dim ListJuridicalD As New List(Of TransferJuridicalDebtCollectionD)
        Dim listStatus As New List(Of String)
        Try
            Dim _unReconcileInvoice As Boolean = False
            Dim parametro As SettingPortfolio = _SettingPortfolioRepository.GetSettingPortfolioByIdOperatingUnit(_IdUnitoperating)
            If parametro IsNot Nothing Then
                _unReconcileInvoice = IIf(parametro.UnReconciledInvoice.HasValue, parametro.UnReconciledInvoice.GetValueOrDefault, False)
                If _unReconcileInvoice Then
                    listStatus.Add("10")
                    listStatus.Add("13")
                End If
            End If
            For Each item As String In ListInvoices
                Dim portfolio = _PortFolioRepository.ListInvoicesByNumber(item, Nit, listStatus)
                If portfolio.Id > 0 Then
                    Dim conciliation = New TransferJuridicalDebtCollectionD With {.GlosaPortfolioGlosada = portfolio, .PortfolioGlosaId = portfolio.Id, .InvoiceNumber = portfolio.InvoiceNumber}
                    ListJuridicalD.Add(conciliation)
                End If
            Next
            Return ListJuridicalD
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
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
            _JuridicalDReceptionDRepository = Nothing
            _PortFolioRepository = Nothing
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
