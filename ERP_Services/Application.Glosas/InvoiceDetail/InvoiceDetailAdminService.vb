'***********************************************************************
' Assembly         : Application.Glosas
' Author           : RafaelPatiño
' Created          : 30-05-2013
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports System.Transactions

Imports Domain.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Common.Entities
Imports Application.Base
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Base

Public Class InvoiceDetailAdminService
    Implements IInvoiceDetailAdminService


    Dim _InvoiceDetailRepository As IInvoiceDetailRepository
    Dim _InvoiceDetailRepositoryQX As IInvoiceDetailQxRepository

    ''' <summary>
    ''' Initializa una nueva instancia de la clase <see cref="ObjectionsReceptionCAdminService" />.
    ''' </summary>
    ''' <param name="InvoiceDetailRepository">el repositorio para el manejo de detalle de facturas.</param>
    Public Sub New(ByVal InvoiceDetailRepository As IInvoiceDetailRepository, ByVal InvoiceDetailRepositoryQX As IInvoiceDetailQxRepository)
        If InvoiceDetailRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de Detalle de Factura Vacío")
        End If
        If InvoiceDetailRepositoryQX Is Nothing Then
            Throw New ArgumentNullException("Repositorio de Detalle de Factura QX Vacío")
        End If
        _InvoiceDetailRepository = InvoiceDetailRepository
        _InvoiceDetailRepositoryQX = InvoiceDetailRepositoryQX
    End Sub


    ''' <summary>
    ''' Funcion para cargar los detalle de cada factura por medio del numero de factura
    ''' </summary>
    ''' <param name="InvoiceNumber">numero de factura</param>
    ''' <returns>lista de detalles de factura</returns>
    ''' <remarks></remarks>
    Public Function ListGlosaInvoiceDetailByInvoiceNumber(InvoiceNumber As String, ByVal Modulo As String, conciliationId As Integer) As List(Of Domain.Entities.GlosaInvoiceDetail) Implements IInvoiceDetailAdminService.ListGlosaInvoiceDetailByInvoiceNumber
        If String.IsNullOrEmpty(InvoiceNumber) = True Then
            Throw New ArgumentNullException("Numero de Factura Vacio")
        End If
        Try
            Return _InvoiceDetailRepository.ListGlosaInvoiceDetailByInvoiceNumber(InvoiceNumber, Modulo, conciliationId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Funcion para cargar los detalle quirurgicos
    ''' </summary>
    ''' <param name="InvoiceDetailId">Id del Detalle de Factura</param>
    ''' <returns>una lista de detalles de factura</returns>
    Public Function ListGlosaInvoiceDetailQX(InvoiceDetailId As String) As List(Of Domain.Entities.GlosaInvoiceDetailQX) Implements IInvoiceDetailAdminService.ListGlosaInvoiceDetailQX
        If String.IsNullOrEmpty(InvoiceDetailId) = True Then
            Throw New ArgumentNullException("Id del detalle de factura vacío")
        End If
        Try
            Return _InvoiceDetailRepositoryQX.ListGlosaInvoiceDetailQX(InvoiceDetailId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try

    End Function

    ''' <summary>
    ''' Guarda una lista de Detalles de Factura
    ''' </summary>
    ''' <param name="ListInvoiceDetail">Lista de Detalles de Factura</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>ActionResult</returns>
    Public Function SaveGlosaInvoiceDetail(ListInvoiceDetail As List(Of GlosaInvoiceDetail), audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionResult Implements IInvoiceDetailAdminService.SaveGlosaInvoiceDetail
        Dim unitOfWork As IUnitWork = _InvoiceDetailRepository.UnitWork
        Try
            Dim validate = _InvoiceDetailRepository.ValidateInvoices(ListInvoiceDetail(0).InvoiceNumber)
            If validate Then
                If ListInvoiceDetail(0).GlosaObjectionsReceptionD.DocumentType = "1" Then
                    ListInvoiceDetail(0).GlosaObjectionsReceptionD.GlosaPortfolioGlosada.TempState = ListInvoiceDetail(0).GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State
                    ListInvoiceDetail(0).GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "3"
                End If
                If ListInvoiceDetail(0).GlosaObjectionsReceptionD.DocumentType = "2" Then
                    ListInvoiceDetail(0).GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = ListInvoiceDetail(0).GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State
                    ListInvoiceDetail(0).GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "6"
                End If
            End If
            For Each item As GlosaInvoiceDetail In ListInvoiceDetail
                _InvoiceDetailRepository.SaveEntity(item)
            Next
            unitOfWork.Commit()
            For Each item As GlosaInvoiceDetail In ListInvoiceDetail
                '/***** Auditoria Basica ********/
                If item.ChangeTracker.State = ObjectState.Added Then
                    IndigoAuditBasic.Execute("GlosaInvoiceDetail", audit.Functional, item.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, Infrastructure.CrossCutting.Base.ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
                ElseIf item.ChangeTracker.State = ObjectState.Modified Then
                    IndigoAuditBasic.Execute("GlosaInvoiceDetail", audit.Functional, item.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, Infrastructure.CrossCutting.Base.ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
                End If
            Next
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False}
        End Try
    End Function


    ''' <summary>
    ''' Funcion para cargar los detalle quirurgicos
    ''' </summary>
    ''' <param name="Id">Id del Detalle de Factura</param>
    ''' <returns>una lista de detalles de factura</returns>
    Public Function GetGlosaInvoiceDetail(Id As String) As GlosaInvoiceDetail Implements IInvoiceDetailAdminService.GetGlosaInvoiceDetail
        If String.IsNullOrEmpty(Id) = True Then
            Throw New ArgumentNullException("Id del detalle de factura vacío")
        End If
        Try
            Return _InvoiceDetailRepository.GetGlosaInvoiceDetail(Id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Funcion que retorna un objeto detalle de factura tipo qx
    ''' </summary>
    ''' <param name="InvoiceDetailQXId">Codigo del detalle qx</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetGlosaInvoiceDetailQX(InvoiceDetailQXId As String) As GlosaInvoiceDetailQX Implements IInvoiceDetailAdminService.GetGlosaInvoiceDetailQX
        If String.IsNullOrEmpty(InvoiceDetailQXId) = True Then
            Throw New ArgumentNullException("Id del detalle de factura qx vacío")
        End If
        Try
            Return _InvoiceDetailRepositoryQX.GetGlosaInvoiceDetailQX(InvoiceDetailQXId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Funcion para cargar lista de detalle de factura a reiterar
    ''' </summary>
    ''' <param name="InvoiceNumber"></param>
    ''' <param name="Modulo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListGlosaInvoiceDetailByInvoiceNumberReiteration(InvoiceNumber As String, Modulo As String) As ActionResult(Of List(Of GlosaInvoiceDetail)) Implements IInvoiceDetailAdminService.ListGlosaInvoiceDetailByInvoiceNumberReiteration
        If String.IsNullOrEmpty(InvoiceNumber) = True Then
            Throw New ArgumentNullException("Numero de Factura Vacio")
        End If
        Dim _actionResult As New ActionResult(Of List(Of GlosaInvoiceDetail))
        Try
            _actionResult.ObjectEmbbeded = _InvoiceDetailRepository.ListGlosaInvoiceDetailByInvoiceNumberReiteration(InvoiceNumber, Modulo)
            _actionResult.MessageResult = New List(Of String)
            _actionResult.StateResult = True
            Return _actionResult
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            _actionResult.StateResult = False
            Return _actionResult
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="InvoiceNumber"></param>
    ''' <param name="Modulo"></param>
    ''' <returns></returns>
    Public Function ListGlosaInvoiceDetailByInvoiceNumberReiterationWithOutMovements(InvoiceNumber As String, Modulo As String) As ActionResult(Of List(Of GlosaInvoiceDetail)) Implements IInvoiceDetailAdminService.ListGlosaInvoiceDetailByInvoiceNumberReiterationWithOutMovements
        If String.IsNullOrEmpty(InvoiceNumber) = True Then
            Throw New ArgumentNullException("Numero de Factura Vacio")
        End If
        Dim _actionResult As New ActionResult(Of List(Of GlosaInvoiceDetail))
        Try
            _actionResult.ObjectEmbbeded = _InvoiceDetailRepository.ListGlosaInvoiceDetailByInvoiceNumberReiterationWithOutMovements(InvoiceNumber, Modulo)
            _actionResult.MessageResult = New List(Of String)
            _actionResult.StateResult = True
            Return _actionResult
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            _actionResult.StateResult = False
            Return _actionResult
        End Try
    End Function

    ''' <summary>
    ''' lista de detalle de facturas
    ''' </summary>
    ''' <param name="listInvoice"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListsStructureDetailInvocie(listInvoice As List(Of String)) As List(Of GlosaInvoiceDetail) Implements IInvoiceDetailAdminService.ListsStructureDetailInvocie
        If listInvoice.Count = 0 Then
            Throw New ArgumentNullException("lista de Factura Vacio")
        End If
        Try
            Return _InvoiceDetailRepository.ListsStructureDetailInvocie(listInvoice)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Funcion para cargar lista de detalle no normativos de factura a reiterar
    ''' </summary>
    ''' <param name="InvoiceNumber"></param>
    ''' <param name="Modulo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListGlosaInvoiceDetailByInvoiceNumberReiterationNoNormative(InvoiceNumber As String, Modulo As String) As ActionResult(Of List(Of GlosaInvoiceDetail)) Implements IInvoiceDetailAdminService.ListGlosaInvoiceDetailByInvoiceNumberReiterationNoNormative
        If String.IsNullOrEmpty(InvoiceNumber) = True Then
            Throw New ArgumentNullException("Numero de Factura Vacio")
        End If
        Dim _actionResult As New ActionResult(Of List(Of GlosaInvoiceDetail))
        Try
            _actionResult.ObjectEmbbeded = _InvoiceDetailRepository.ListGlosaInvoiceDetailByInvoiceNumberReiterationNoNormative(InvoiceNumber, Modulo)
            _actionResult.MessageResult = New List(Of String)
            _actionResult.StateResult = True
            Return _actionResult
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            _actionResult.StateResult = False
            Return _actionResult
        End Try
    End Function

    ''' <summary>
    ''' Función para traer una lista de detalles que tiene cada factura 
    ''' </summary>
    ''' <param name="InvoiceNumber">Numero de Factura</param>
    ''' <returns>Lista de Detalles Factura</returns>
    Public Function ListGlosaInvoiceDetailForGeneralConciliation(InvoiceNumber As String) As List(Of GlosaInvoiceDetail) Implements IInvoiceDetailAdminService.ListGlosaInvoiceDetailForGeneralConciliation
        If String.IsNullOrEmpty(InvoiceNumber) = True Then
            Throw New ArgumentNullException("Numero de Factura Vacio")
        End If
        Try
            Return _InvoiceDetailRepository.ListGlosaInvoiceDetailForGeneralConciliation(InvoiceNumber)
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
            _InvoiceDetailRepository = Nothing
            _InvoiceDetailRepositoryQX = Nothing
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
