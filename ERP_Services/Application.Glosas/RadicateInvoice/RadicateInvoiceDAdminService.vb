'***********************************************************************
' Assembly         : Application.Glosas
' Author           : RafaelPatiño
' Created          : 11-04-2014
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
Imports Domain.Common.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Base
Imports Domain.InterfaceERPGlosa
Imports System.Text


Public Class RadicateInvoiceDAdminService
    Implements IRadicateInvoiceDAdminService



    Dim _RadicateInvoiceDRepository As IRadicateInvoiceDRepository
    Private _InterfacePublicFOX As IInterfacePublicFOX
    Private _IInterfaceParametersRepository As IInterfaceParametersRepository
    ''' <summary>
    ''' Repositorio de Cuentas por cobrar
    ''' </summary>
    ''' <remarks></remarks>
    Private _accountReceivableRepository As IAccountReceivableRepository

    ''' <summary>
    ''' Initializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="RadicateInvoiceDRepository"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal RadicateInvoiceDRepository As IRadicateInvoiceDRepository, InterfacePublicFOX As IInterfacePublicFOX, IInterfaceParametersRepository As IInterfaceParametersRepository,
                    accountReceivableRepository As IAccountReceivableRepository)
        If RadicateInvoiceDRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de Radicacion Detalle de facturas Vacio")
        End If
        If accountReceivableRepository Is Nothing Then
            Throw New ArgumentNullException("accountReceivableRepository vacio", "repositorio de cuentas por cobrar vacio")
        End If
        _RadicateInvoiceDRepository = RadicateInvoiceDRepository
        _IInterfaceParametersRepository = IInterfaceParametersRepository
        _InterfacePublicFOX = InterfacePublicFOX
        _accountReceivableRepository = accountReceivableRepository
    End Sub

    ''' <summary>
    ''' Funcion para eliminacion masiva
    ''' </summary>
    ''' <param name="tmpList">lista de item a eliminar</param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteListInvoiceD(ByVal tmpList As List(Of String), Session As SessionValues) As ActionResult Implements IRadicateInvoiceDAdminService.DeleteListInvoiceD
        Dim unitOfWork As IUnitWork = _RadicateInvoiceDRepository.UnitWork
        Dim AccountReceivableunitOfWork As IUnitWork = _accountReceivableRepository.UnitWork
        Try
            '  Dim ListError As New List(Of String)
            Dim ListDeleteRadicateD As List(Of RadicateInvoiceD) = _RadicateInvoiceDRepository.GetListDeleteRadicateD(tmpList)
            'configuro la transaccion
            Dim txSettings As New TransactionOptions()
            txSettings.Timeout = TransactionManager.MaximumTimeout
            txSettings.IsolationLevel = IsolationLevel.ReadCommitted
            'inicio la transaccion
            Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
                If ListDeleteRadicateD IsNot Nothing AndAlso ListDeleteRadicateD.Count > 0 Then
                    If Session.IndigoGlossesIntegration = EGlossesIntegration.Native Or Session.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNet Or Session.IndigoGlossesIntegration = EGlossesIntegration.IntegractionFox Or Session.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNATIVE_MIGRATIONS Then
                        For Each itemD As RadicateInvoiceD In ListDeleteRadicateD
                            Dim _AccountReceivable As AccountReceivable = _accountReceivableRepository.GetAccountReceivableByInvoiceNumberGloss(itemD.InvoiceNumber) 'consultamos factura en cartera
                            'Actualizamos estado en cartera
                            With _AccountReceivable
                                .PortfolioStatus = 1 ' Sin radicar
                            End With
                            _accountReceivableRepository.SaveEntity(_AccountReceivable)
                            itemD.MarkAsDeleted()
                            _RadicateInvoiceDRepository.DeleteEntity(itemD)
                        Next
                        AccountReceivableunitOfWork.Commit()
                        unitOfWork.Commit()
                    ElseIf Session.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
                        Dim mensaje As List(Of String) = New List(Of String)
                        Dim OblParameters As GlosasParametersInterface = _IInterfaceParametersRepository.GetInterfacesParametersById(ListDeleteRadicateD(0).GlosasParametersInterfaceId)
                        If OblParameters IsNot Nothing Then
                            'para el metodo fox publico debemos actualizar estado de cartera para la reiteracion
                            If OblParameters.AccountingMethod = eTypeInterface.FoxPublic Or OblParameters.AccountingMethod = eTypeInterface.FoxPrivate Then
                                'Creamos la conexion
                                Using cnx As New System.Data.SqlClient.SqlConnection(Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, Session.TransactionalContainer, False))
                                    cnx.Open()
                                    Dim command As New System.Data.SqlClient.SqlCommand("", cnx)
                                    command.CommandTimeout = 30000
                                    command.CommandType = CommandType.Text
                                    Dim IntResult As Integer
                                    Dim builder As New StringBuilder
                                    '  builder = String.Join(";", (From e In ListDeleteRadicateD Select "UPDATE " & OblParameters.ContainerName & "..crcarter SET cemestado = '1' WHERE cemnumfac = '" & e.InvoiceNumber & "'").ToList())
                                    For Each itemD As RadicateInvoiceD In ListDeleteRadicateD
                                        builder.AppendLine("UPDATE " & OblParameters.ContainerName & "..crcarter SET cemestado = '1' WHERE cemnumfac = '" & itemD.InvoiceNumber & "';")
                                    Next
                                    Dim ListDelete = _RadicateInvoiceDRepository.DeleteMasivo(ListDeleteRadicateD)
                                    command.CommandText = builder.ToString()
                                    IntResult = command.ExecuteNonQuery()
                                    If IntResult <= 0 Then
                                        mensaje.Add("Error Actualizando Estado de Cartera ERP factura")
                                        scope.Dispose()
                                        Return New ActionResult With {.StateResult = False, .MessageResult = mensaje}
                                    End If
                                    cnx.Close()
                                End Using
                            Else
                                For Each itemD As RadicateInvoiceD In ListDeleteRadicateD
                                    itemD.MarkAsDeleted()
                                    _RadicateInvoiceDRepository.DeleteEntity(itemD)
                                Next
                            End If
                        End If
                        unitOfWork.Commit()
                    End If
                End If
                scope.Complete()
            End Using
            Return New ActionResult With {.StateResult = True, .MessageResult = New List(Of String)({"OK"})}
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
    ''' Elimina  un factura del oficio de radicacion de cuentas
    ''' </summary>
    ''' <param name="RadicateInvoiceD">objeto factura de radicacion de cuentas</param>
    ''' <returns></returns>
    Public Function DeleteInvoiveD(RadicateInvoiceD As RadicateInvoiceD, SessionValues As SessionValues) As ActionResult Implements IRadicateInvoiceDAdminService.DeleteInvoiveD
        If RadicateInvoiceD Is Nothing Then
            Throw New ArgumentNullException("Oficio Radicación Vacio")
        End If
        Dim unitOfWork As IUnitWork = _RadicateInvoiceDRepository.UnitWork
        Dim AccountReceivableunitOfWork As IUnitWork = _accountReceivableRepository.UnitWork
        Try
            If SessionValues.IndigoGlossesIntegration = EGlossesIntegration.Native Or SessionValues.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNet Or SessionValues.IndigoGlossesIntegration = EGlossesIntegration.IntegractionFox Or SessionValues.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNATIVE_MIGRATIONS Then
                Dim _AccountReceivable As AccountReceivable = _accountReceivableRepository.GetAccountReceivableByInvoiceNumberGloss(RadicateInvoiceD.InvoiceNumber) 'consultamos factura en cartera
                'Actualizamos estado en cartera
                With _AccountReceivable
                    .PortfolioStatus = 1 ' Sin radicar
                End With
                _accountReceivableRepository.SaveEntity(_AccountReceivable)
                AccountReceivableunitOfWork.Commit()
            ElseIf SessionValues.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
                'retorno estado al eliminar factura
                Dim OblParameters As GlosasParametersInterface = _IInterfaceParametersRepository.GetInterfacesParametersById(RadicateInvoiceD.GlosasParametersInterfaceId)
                'para el metodo fox publico debemos actualizar estado de cartera para la reiteracion
                If OblParameters.AccountingMethod = eTypeInterface.FoxPublic Or OblParameters.AccountingMethod = eTypeInterface.FoxPrivate Then
                    If OblParameters IsNot Nothing Then
                        Dim UpdateState = _InterfacePublicFOX.UpdtaeStateReiteration(RadicateInvoiceD.InvoiceNumber, OblParameters.ContainerName, "1") 'estado de careta 1
                        Dim mensaje As List(Of String) = New List(Of String)
                        Dim _actionresult As New ActionResult
                        If UpdateState = False Then
                            mensaje.Add("Error Actualizando Estado de Cartera ERP")
                            Return New ActionResult With {.StateResult = False, .MessageResult = mensaje}
                            Return _actionresult
                        End If
                    End If
                End If
            End If
            'Elimino la factura
            RadicateInvoiceD.MarkAsDeleted()
            _RadicateInvoiceDRepository.DeleteEntity(RadicateInvoiceD)
            unitOfWork.Commit()
            '/***** Auditoria Basica ********/
            IndigoAuditBasic.Execute("RadicateInvoiceD", SessionValues.AuditMessageWcf.Functional, RadicateInvoiceD.Id, SessionValues.AuditMessageWcf.NameUser, SessionValues.AuditMessageWcf.CodeUser, SessionValues.AuditMessageWcf.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, SessionValues.AuditMessageWcf.Company, SessionValues.AuditMessageWcf.ContainerSecurity)
            '/*****Auditoria Avanzada ******/
            Dim auditObject As New IndigoAuditSimpleEntity(Of RadicateInvoiceD)(RadicateInvoiceD, SessionValues.AuditMessageWcf, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", SessionValues)
            Return New ActionResult With {.StateResult = False}
        End Try
    End Function
    ''' <summary>
    ''' Lista de factura de radicacion con oficio
    ''' </summary>
    ''' <param name="consecutive">numero de radicado del oficio</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListRadicateD(consecutive As String) As List(Of RadicateInvoiceD) Implements IRadicateInvoiceDAdminService.GetListRadicateD
        If consecutive Is Nothing Then
            Throw New ArgumentNullException("Oficio Radicación Vacio")
        End If
        Dim list As List(Of RadicateInvoiceD)
        Try
            list = _RadicateInvoiceDRepository.GetListRadicateD(consecutive)
            Return list
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
                _InterfacePublicFOX.Dispose()
            End If
            _RadicateInvoiceDRepository = Nothing
            _IInterfaceParametersRepository = Nothing
            _InterfacePublicFOX = Nothing
            _accountReceivableRepository = Nothing
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
