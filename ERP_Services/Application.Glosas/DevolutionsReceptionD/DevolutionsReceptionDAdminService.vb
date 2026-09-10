'***********************************************************************
' Assembly         : Application.Glosas
' Author           : Juan Diego Diaz
' Created          : 13-06-2013
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
Imports System.Data.Entity.Infrastructure

#End Region

''' <summary>
''' Servicio de Devolución Detalle.
''' </summary>
''' <remarks></remarks>
Public Class DevolutionsReceptionDAdminService
    Implements IDevolutionsReceptionDAdminService

    Private _DevolutionsReceptionDRepository As IDevolutionsReceptionDRepository
    Private _PortFolioGlosaRepository As IPortfolioGlosadaRepository
    Private _MovementGlosaRepository As IMovementGlosaRepository

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase <see cref="ConciliationDAdminService" />.
    ''' </summary>
    ''' <param name="DevolutionsReceptionDRepository">El repositorio para el manejo de los detalles de conciliacion.</param>
    Public Sub New(ByVal DevolutionsReceptionDRepository As IDevolutionsReceptionDRepository, ByVal PortFolioGlosaRepository As IPortfolioGlosadaRepository, ByVal MovementGlosaRepository As IMovementGlosaRepository)
        If DevolutionsReceptionDRepository Is Nothing Then
            Throw New ArgumentNullException("Detalle Devolución Vacío")
        End If
        If PortFolioGlosaRepository Is Nothing Then
            Throw New ArgumentNullException("Glosa Cartera Vacia")
        End If
        If MovementGlosaRepository Is Nothing Then
            Throw New ArgumentNullException("Movimiento Glosa Vacío")
        End If
        _DevolutionsReceptionDRepository = DevolutionsReceptionDRepository
        _PortFolioGlosaRepository = PortFolioGlosaRepository
        _MovementGlosaRepository = MovementGlosaRepository
    End Sub

    ''' <summary>
    ''' Borrar Devolución Detalle.
    ''' </summary>
    ''' <param name="DevolucionD">Objeto Devolución Detalle</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Action Result</returns>
    Public Function DeleteDevolutionD(DevolucionD As List(Of GlosaDevolutionsReceptionD), audit As AuditMessage) As ActionResult Implements IDevolutionsReceptionDAdminService.DeleteDevolutionD
        If DevolucionD Is Nothing Then
            Throw New ArgumentNullException("Detalle Devolución Vacío")
        End If
        Dim unitOfWork As IUnitWork = _DevolutionsReceptionDRepository.UnitWork
        Try
            Dim count As Integer = DevolucionD.Count - 1
            For i As Integer = 0 To count
                'Elimino el detalle de conciliación.
                _DevolutionsReceptionDRepository.DeleteEntity(DevolucionD(0))
            Next
            unitOfWork.Commit()

            '/***** Auditoria Basica ********/
            IndigoAuditBasic.Execute("GlosaDevolutionsReceptionD", audit.Functional, DevolucionD(0).Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            Return New ActionResult With {.StateResult = True}

        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList()}
        End Try
    End Function

    ''' <summary>
    ''' Guardar Devolución Detalle
    ''' </summary>
    ''' <param name="DevolucionD">Objeto Devolución Detalle</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Action Result</returns>
    Public Function SaveDevolutionD(DevolucionD As List(Of GlosaDevolutionsReceptionD), audit As AuditMessage) As ActionResult Implements IDevolutionsReceptionDAdminService.SaveDevolutionD
        If DevolucionD Is Nothing Then
            Throw New ArgumentNullException("Detalle devolución vacio")
        End If
        Dim unitOfWork As IUnitWork = _DevolutionsReceptionDRepository.UnitWork
        Dim unitOfWorkPortfolioGlosa As IUnitWork = _PortFolioGlosaRepository.UnitWork
        Try

            Dim j As Integer = 0
            Do
                If DevolucionD(j).ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    _DevolutionsReceptionDRepository.UpdateEntity(DevolucionD(j))
                ElseIf DevolucionD(j).ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    _DevolutionsReceptionDRepository.AddEntity(DevolucionD(j))
                ElseIf DevolucionD(j).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted Then
                    _DevolutionsReceptionDRepository.DeleteEntity(DevolucionD(j))
                    j = j - 1
                End If
                j = j + 1
            Loop While j < DevolucionD.Count - 1
            'Confirmo la unidad de trabajo
            unitOfWork.Commit()
            unitOfWorkPortfolioGlosa.Commit()

            j = 0
            Do
                If DevolucionD(j).ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    '/***** Auditoria Basica ********/
                    IndigoAuditBasic.Execute("GlosaDevolutionsReceptionD", audit.Functional, DevolucionD(0).Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
                ElseIf DevolucionD(j).ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    '/***** Auditoria Basica ********/
                    IndigoAuditBasic.Execute("GlosaDevolutionsReceptionD", audit.Functional, DevolucionD(0).Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
                ElseIf DevolucionD(j).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted Then
                    '/***** Auditoria Basica ********/
                    IndigoAuditBasic.Execute("GlosaDevolutionsReceptionD", audit.Functional, DevolucionD(0).Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
                End If
                j = j + 1

            Loop While j < DevolucionD.Count - 1

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
    ''' Obtiene una devolución detalle según código.
    ''' </summary>
    ''' <param name="Id">Id Devolución detalle</param>
    ''' <returns>Objeto Devolución Detalle</returns>
    Public Function GetDevolutionDByIdObjectionD(Id As String) As GlosaDevolutionsReceptionD Implements IDevolutionsReceptionDAdminService.GetDevolutionDByIdDevolutionD
        If String.IsNullOrEmpty(Id) = True Then
            Throw New ArgumentNullException("Id vacío")
        End If
        Try
            Return _DevolutionsReceptionDRepository.GetDevolutionDByIdDevolutionD(Id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene todos los registros de devoluciones detalle.
    ''' </summary>
    ''' <returns>Lista Devolución Detalle</returns>
    Public Function ListAllDevolutionD() As List(Of GlosaDevolutionsReceptionD) Implements IDevolutionsReceptionDAdminService.ListAllDevolutionD
        Try
            Return _DevolutionsReceptionDRepository.ListAllDevolutionD()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una devolución detalle según código.
    ''' </summary>
    ''' <param name="Id">Id Devolución Cabecera</param>
    ''' <returns>Objeto Devolución Detalle</returns>
    Public Function ListDevolutionDByIdDevolutionC(Id As String) As List(Of GlosaDevolutionsReceptionD) Implements IDevolutionsReceptionDAdminService.ListDevolutionDByIdDevolutionnC
        If String.IsNullOrEmpty(Id) = True Then
            Throw New ArgumentNullException("Id vacío")
        End If
        Try
            Return _DevolutionsReceptionDRepository.ListDevolutionDByIdDevolutionnC(Id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Funcion para eliminacion masiva
    ''' </summary>
    ''' <param name="tmpList">lista de item a eliminar</param>
    ''' <param name="SessionValues"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteListDevolution(ByVal tmpList As List(Of GlosaDevolutionsReceptionD), SessionValues As SessionValues) As ActionResult Implements IDevolutionsReceptionDAdminService.DeleteListDevolution
        Dim result As ActionResult
        Dim ListError As New List(Of String)
        If tmpList.Count > 0 Then
            For i As Integer = 0 To tmpList.Count - 1
                result = DeleteDevolution(tmpList(i), SessionValues)
                If result.StateResult = False Then
                    ListError.Add("Ocurrio un error eliminando factura: " & tmpList(i).InvoiceNumber)
                End If
            Next
        End If
        If ListError.Count = 0 Then
            Return New ActionResult With {.StateResult = True, .MessageResult = New List(Of String)({"OK"})}
        Else
            Return New ActionResult With {.StateResult = False, .MessageResult = ListError}
        End If
    End Function

    ''' <summary>
    ''' eliminar una devolucion
    ''' </summary>
    ''' <param name="GlosaDevolutionsReceptionD"></param>
    ''' <param name="SessionValues"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteDevolution(GlosaDevolutionsReceptionD As GlosaDevolutionsReceptionD, SessionValues As SessionValues) As ActionResult Implements IDevolutionsReceptionDAdminService.DeleteDevolution
        If GlosaDevolutionsReceptionD Is Nothing Then
            Throw New ArgumentNullException("Factura devolucion Vacio")
        End If
        Using cnx As New System.Data.SqlClient.SqlConnection(Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, SessionValues.TransactionalContainer, False))
            cnx.Open()
            Dim tx As System.Data.SqlClient.SqlTransaction = cnx.BeginTransaction()
            Dim command As New System.Data.SqlClient.SqlCommand("", cnx, tx)
            command.CommandTimeout = 30000
            command.CommandType = CommandType.Text
            ' Dim unitOfWork As IUnitWork = _DevolutionsReceptionDRepository.UnitWork
            Try
                'Dim objDelte As GlosaDevolutionsReceptionD = _DevolutionsReceptionDRepository.GetDevolutionDByIdDevolutionD(GlosaDevolutionsReceptionD.Id)

                'If objDelte IsNot Nothing Then
                '    Dim count As Integer = objDelte.GlosaMovementDevolutions.Count
                '    For i As Integer = 0 To count - 1
                '        objDelte.GlosaMovementDevolutions(i).MarkAsDeleted()
                '    Next

                '    objDelte.MarkAsDeleted()
                '    _DevolutionsReceptionDRepository.SaveEntity(objDelte)
                '    unitOfWork.Commit()

                'End If
                'Creamos la conexion
                command.CommandText = "DELETE FROM [Glosas].[GlosaMovementDevolutions] WHERE [IdDevolutionsReceptionD]='" & GlosaDevolutionsReceptionD.Id & "'"
                command.ExecuteNonQuery()
                'Aqui eliminamos la factura
                command.CommandText = "DELETE FROM [Glosas].[GlosaDevolutionsReceptionD] WHERE [Id]='" & GlosaDevolutionsReceptionD.Id & "'"
                command.ExecuteNonQuery()
                tx.Commit()
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute("GlosaDevolutionsReceptionD", SessionValues.AuditMessageWcf.Functional, GlosaDevolutionsReceptionD.Id, SessionValues.AuditMessageWcf.NameUser, SessionValues.AuditMessageWcf.CodeUser, SessionValues.AuditMessageWcf.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, SessionValues.AuditMessageWcf.Company, SessionValues.AuditMessageWcf.ContainerSecurity)
                '/*****Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of GlosaDevolutionsReceptionD)(GlosaDevolutionsReceptionD, SessionValues.AuditMessageWcf, Infrastructure.CrossCutting.Audit.Actions.Delete)
                auditObject.Execute()
                Return New ActionResult With {.StateResult = True}
            Catch ex As OptimisticConcurrencyException
                ' unitOfWork.RollbackChanges()
                tx.Rollback()
                Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
            Catch ex As DbUpdateException
                'unitOfWork.RollbackChanges()
                tx.Rollback()
                Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
            Catch ex As Exception
                ' unitOfWork.RollbackChanges()
                tx.Rollback()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", SessionValues)
                Return New ActionResult With {.StateResult = False}
            Finally
                cnx.Close()
            End Try
        End Using
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _DevolutionsReceptionDRepository = Nothing
            _PortFolioGlosaRepository = Nothing
            _MovementGlosaRepository = Nothing
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
