'***********************************************************************
' Assembly         : Application.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 01/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core

Public Class SuppliersDistributionLinesAdminService
    Implements ISuppliersDistributionLinesAdminService


    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _suppliersDistributionLinesRepository As ISuppliersDistributionLinesRepository

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal supplierDistributionLinesRepository As ISuppliersDistributionLinesRepository)
        If supplierDistributionLinesRepository Is Nothing Then
            Throw New ArgumentNullException("supplierDistributionLinesRepository Vacio")
        End If
        _suppliersDistributionLinesRepository = supplierDistributionLinesRepository
    End Sub

    ''' <summary>
    ''' Elimina una linea de distribucion del proveedor
    ''' </summary>
    ''' <param name="supplierDistributionLine"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteSuppliersDistributionLines(supplierDistributionLine As SuppliersDistributionLines, audit As AuditMessage) As ActionResult Implements ISuppliersDistributionLinesAdminService.DeleteSuppliersDistributionLines
        If supplierDistributionLine Is Nothing Then
            Throw New ArgumentNullException("supplierDistributionLine")
        End If
        Dim unitOfWork As IUnitWork = Me._suppliersDistributionLinesRepository.UnitWork
        Try
            If supplierDistributionLine.ChangeTracker.State = ObjectState.Deleted Then
                Me._suppliersDistributionLinesRepository.DeleteEntity(supplierDistributionLine)
                unitOfWork.Commit()
                'Auditoria básica
                IndigoAuditBasic.Execute(GetType(SuppliersDistributionLines).Name, audit.Functional, supplierDistributionLine.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, Infrastructure.CrossCutting.Base.ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
                'Ausitoria avanzada
                Dim auditObject As New IndigoAuditSimpleEntity(Of SuppliersDistributionLines)(supplierDistributionLine, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
                auditObject.Execute()
                Return New ActionResult With {.StateResult = True}
            Else
                Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
            End If
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene las lineas de distribucion del proveedor
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSuppliersDistributionLinesByIdSupplier(id As Integer, audit As AuditMessage) As List(Of SuppliersDistributionLines) Implements ISuppliersDistributionLinesAdminService.GetSuppliersDistributionLinesByIdSupplier
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim ListSuppliersDistributionLines As List(Of SuppliersDistributionLines) = Me._suppliersDistributionLinesRepository.GetSuppliersDistributionLinesByIdSupplierSimple(id)
            Return ListSuppliersDistributionLines
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza las lineas de distribucion del proveedor
    ''' </summary>
    ''' <param name="supplierDistributionLine"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveSuppliersDistributionLines(supplierDistributionLine As SuppliersDistributionLines, audit As AuditMessage) As ActionResult(Of SuppliersDistributionLines) Implements ISuppliersDistributionLinesAdminService.SaveSuppliersDistributionLines
        If supplierDistributionLine Is Nothing Then
            Throw New ArgumentNullException("supplierDistributionLine")
        End If
        Dim unitOfWork As IUnitWork = Me._suppliersDistributionLinesRepository.UnitWork
        Dim auxSuppliersDistributionLines As SuppliersDistributionLines = supplierDistributionLine.OriginalValue
        Try
            'If supplierDistributionLine.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added OrElse supplierDistributionLine.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
            Me._suppliersDistributionLinesRepository.SaveEntity(supplierDistributionLine)
            ' End If
            unitOfWork.Commit()
            If supplierDistributionLine.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(GetType(SuppliersDistributionLines).Name, audit.Functional, supplierDistributionLine.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
                '/***** Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of SuppliersDistributionLines)(supplierDistributionLine, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                auditObject.Execute()
            ElseIf supplierDistributionLine.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(GetType(SuppliersDistributionLines).Name, audit.Functional, supplierDistributionLine.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
                '/***** Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of SuppliersDistributionLines)(supplierDistributionLine, audit, Infrastructure.CrossCutting.Audit.Actions.Update, auxSuppliersDistributionLines)
                auditObject.Execute()
            End If

            'Se marca la entidad como sin cambios
            supplierDistributionLine.MarkAsUnchanged()

            Return New ActionResult(Of SuppliersDistributionLines) With {.StateResult = True, .ObjectEmbbeded = supplierDistributionLine}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of SuppliersDistributionLines) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SuppliersDistributionLines) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la linea de distribucion por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSuppliersDistributionLinesById(id As Integer, audit As AuditMessage) As SuppliersDistributionLines Implements ISuppliersDistributionLinesAdminService.GetSuppliersDistributionLinesById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim supplierDistributionLines As SuppliersDistributionLines = Me._suppliersDistributionLinesRepository.GetSuppliersDistributionLinesById(id)
            If supplierDistributionLines IsNot Nothing AndAlso supplierDistributionLines.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of SuppliersDistributionLines)(supplierDistributionLines, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return supplierDistributionLines
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New SuppliersDistributionLines
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el porcentaje de ica de que maneja la linea de distribucion por unidad operativa
    ''' </summary>
    ''' <param name="idSupplierDistributionLine"></param>
    ''' <param name="OperatingUnitId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetICARetentionConceptBySupplierDistributionLine(idSupplierDistributionLine As Integer, OperatingUnitId As Integer) As RetentionConcepts Implements ISuppliersDistributionLinesAdminService.GetICARetentionConceptBySupplierDistributionLine
        If idSupplierDistributionLine = 0 OrElse OperatingUnitId = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Try
            Return _suppliersDistributionLinesRepository.GetICARetentionConceptBySupplierDistributionLine(idSupplierDistributionLine, OperatingUnitId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New RetentionConcepts
        End Try
    End Function

    Public Function GetAccountPayableICARetentionByDistributionLineIdAndOperatingUnitId(idSupplierDistributionLine As Integer, OperatingUnitId As Integer) As AccountPayableConcepts Implements ISuppliersDistributionLinesAdminService.GetAccountPayableICARetentionByDistributionLineIdAndOperatingUnitId
        If idSupplierDistributionLine = 0 OrElse OperatingUnitId = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Try
            Return _suppliersDistributionLinesRepository.GetAccountPayableICARetentionByDistributionLineIdAndOperatingUnitId(idSupplierDistributionLine, OperatingUnitId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene de un proveedor, la lista de lineas de distribución por Concepto Acreencia
    ''' </summary>
    ''' <param name="idSupplier"></param>
    ''' <param name="accusationConcept"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function GetDistributionLinesByIdSupplierAndAccusationConcept(idSupplier As Integer, accusationConcept As Integer, audit As AuditMessage) As List(Of SuppliersDistributionLines) Implements ISuppliersDistributionLinesAdminService.GetDistributionLinesByIdSupplierAndAccusationConcept
        If idSupplier = 0 Then
            Throw New ArgumentNullException("idSupplier")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim ListSuppliersDistributionLines As List(Of SuppliersDistributionLines) = Me._suppliersDistributionLinesRepository.GetDistributionLinesByIdSupplierAndAccusationConcept(idSupplier, accusationConcept)
            Return ListSuppliersDistributionLines
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
            _suppliersDistributionLinesRepository = Nothing
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
