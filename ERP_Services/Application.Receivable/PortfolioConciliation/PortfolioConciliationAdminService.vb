#Region "Imports"

Imports System.Data.Entity.Core
Imports System.Data.SqlClient
Imports System.Transactions
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources

#End Region

Public Class PortfolioConciliationAdminService
    Implements IPortfolioConciliationAdminService

    Private Const FORM_NAME As String = "FrmPortfolioConciliation"

    'Repositorio de tipo de ubicacion
    Private _portfolioConciliationRepository As IPortfolioConciliationRepository

    ''' <summary>
    ''' repositorio de las secuencias
    ''' </summary>
    Private secuenseRepository As ISequensePortfolioDRepository

    ''' <summary>
    ''' inicia el repositorio de ubicacion
    ''' </summary>
    ''' <param name="PortfolioConciliationRespository">Repositorio de Responsable</param>
    ''' <remarks></remarks>
    Public Sub New(secuenceDRepository As ISequensePortfolioDRepository, ByVal PortfolioConciliationRespository As IPortfolioConciliationRepository)

        If (PortfolioConciliationRespository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de dciRepository vacio")
        End If

        _portfolioConciliationRepository = PortfolioConciliationRespository
        secuenseRepository = secuenceDRepository

    End Sub

    ''' <summary>
    ''' obtiene una recepcion de objeciones
    ''' </summary>
    ''' <param name="Consecutive">codigo de la recepcion </param>
    ''' <returns>una recepcion de objecion </returns>
    Public Function GetConciliationByConsecutive(Consecutive As String) As Domain.Entities.PortfolioConciliation Implements IPortfolioConciliationAdminService.GetConciliationByConsecutive
        If String.IsNullOrEmpty(Consecutive) = True Then
            Throw New ArgumentNullException("Codigo de objecion Vacio")
        End If
        Try
            Return _portfolioConciliationRepository.GetConciliationByConsecutive(Consecutive)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' lista de todas las conciliaciones
    ''' </summary>
    ''' <returns>lista de recpcion de objeciones</returns>
    ''' <remarks></remarks>
    Public Function ListAllPortfolioConciliation() As List(Of Domain.Entities.PortfolioConciliation) Implements IPortfolioConciliationAdminService.ListAllPortfolioConciliation
        Try
            Return _portfolioConciliationRepository.ListAllPortfolioConciliation()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' guarda una objecion con su detalle y se persiste  el detalle de la factura
    ''' </summary>
    ''' <param name="PortfolioConciliation">la objecion de recepcion</param>
    ''' <param name="audit">mensaje de auditoria</param>
    ''' <returns>valor de si guardo o no</returns>
    Public Function SavePortfolioConciliation(ByVal PortfolioConciliation As PortfolioConciliation, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of PortfolioConciliation) Implements IPortfolioConciliationAdminService.SavePortfolioConciliation

        If PortfolioConciliation Is Nothing Then
            Throw New ArgumentNullException("Recepción Vacio")
        End If

        Dim unitOfWork As IUnitWork = Me._portfolioConciliationRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me.secuenseRepository.UnitWork

        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(PortfolioConciliation.ConciliationConsecutive) Then
                    Dim seq As PortfolioSequenceDetail = Me.secuenseRepository.GetSequenseDetailUpdatedById(idSequense)

                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PortfolioSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            PortfolioConciliation.ConciliationConsecutive = res
                            seq.Next += 1
                            Me.secuenseRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of PortfolioConciliation) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.PortfolioSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), PortfolioConciliation.ConciliationConsecutive), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of PortfolioConciliation) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As PortfolioConciliation = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of PortfolioConciliation)
                Dim status As Integer

                If PortfolioConciliation.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    PortfolioConciliation.CreationUser = audit.CodeUser
                    PortfolioConciliation.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = PortfolioConciliation.OriginalValue
                    PortfolioConciliation.ModificationUser = audit.CodeUser
                    PortfolioConciliation.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._portfolioConciliationRepository.SaveEntity(PortfolioConciliation)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of PortfolioConciliation)(PortfolioConciliation, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                PortfolioConciliation.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of PortfolioConciliation) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = PortfolioConciliation, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of PortfolioConciliation) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of PortfolioConciliation) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Lista las facturas por contenedor y nit de la entidad ademas del total de registro
    ''' </summary>
    ''' <param name="nameContainer">recibe el nombre del contenedor</param>
    ''' <param name="nit">nit del tercro a buscar facturas</param>
    ''' <param name="InvoiceNumber">Numero de factura</param>
    ''' <param name="IndigoCompany">numero de contenedor</param>
    '''  <param name="stringSQl">cadena Sql de Filtro avanzados</param>
    ''' <param name="TopQuery">Cantidad de Registro a retornar</param>
    ''' <returns>Un objeto result que tiene una lista de facturas y el conteo de las misma</returns>
    Public Function ListAllInvoice(nameContainer As String, nit As String, InvoiceNumber As String, IndigoCompany As String, stringSQl As String, ByVal TopQuery As String, ByVal FlagNotConfirmInvoice As String, session As SessionValues) As ActionResult(Of List(Of SP_invoiceList_Result)) Implements IPortfolioConciliationAdminService.ListAllInvoice
        If session.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
            If String.IsNullOrEmpty(nameContainer) = True Then
                Throw New ArgumentNullException("Nombre del contenedor Vacio")
            End If
        End If
        If String.IsNullOrEmpty(nit) = True Then
            Throw New ArgumentNullException("Nit Vacio")
        End If
        If String.IsNullOrEmpty(IndigoCompany) = True Then
            Throw New ArgumentNullException("Codigo contenedor Indigo Vacio ")
        End If
        Dim _actionResult As New ActionResult(Of List(Of SP_invoiceList_Result))
        Try
            _actionResult.ObjectEmbbeded = _portfolioConciliationRepository.ListAllInvoce(nameContainer, nit, InvoiceNumber, IndigoCompany, session.HisContainer, stringSQl, TopQuery, FlagNotConfirmInvoice)
            _actionResult.MessageResult = New List(Of String)
            _actionResult.StateResult = True
            Return _actionResult
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
            _actionResult.StateResult = False
            Return _actionResult
        End Try
    End Function

    ''' <summary>
    ''' carga una factura
    ''' </summary>
    ''' <param name="nameContainer">recibe el nombre del contenedor</param>
    ''' <param name="nit">nit del tercro a buscar facturas</param>
    ''' <param name="InvoiceNumber">numero de factura</param>
    ''' <returns>una factura</returns>
    Public Function GetInvoice(nameContainer As String, nit As String, InvoiceNumber As String, IndigoCompany As String, ByVal stringSQl As String, ByVal FlagNotConfirmInvoice As String, session As SessionValues) As SP_invoiceList_Result Implements IPortfolioConciliationAdminService.GetInvoice
        If session.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
            If String.IsNullOrEmpty(nameContainer) = True Then
                Throw New ArgumentNullException("Nombre del contenedor Vacio")
            End If
        End If
        If String.IsNullOrEmpty(nit) = True Then
            Throw New ArgumentNullException("Nit Vacio")
        End If
        If String.IsNullOrEmpty(InvoiceNumber) = True Then
            Throw New ArgumentNullException("Numero de Factura Vacio")
        End If
        If String.IsNullOrEmpty(IndigoCompany) = True Then
            Throw New ArgumentNullException("Codigo contenedor Indigo Vacio ")
        End If
        Try
            Return _portfolioConciliationRepository.GetInvoice(nameContainer, nit, InvoiceNumber, IndigoCompany, session.HisContainer, stringSQl, FlagNotConfirmInvoice)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' lista del detalle de una factura mediante un SP
    ''' </summary>
    ''' <param name="container">nombre del contenedor o BD a cargar Detalles de facturas</param>
    ''' <param name="invoiceNumber">numero de factura</param>
    ''' <param name="ingressNumber">numero consecutivo</param>
    ''' <returns>una lista del detalle de una factura</returns>
    Public Function ListInvoiceDetails(ByVal container As String, invoiceNumber As String, ingressNumber As String, session As SessionValues) As List(Of SP_invoiceDetailList_Result) Implements IPortfolioConciliationAdminService.ListInvoiceDetails
        If session.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
            If String.IsNullOrEmpty(container) = True Then
                Throw New ArgumentNullException("Nombre del contenedor Vacio")
            End If
        End If
        If String.IsNullOrEmpty(invoiceNumber) = True Then
            Throw New ArgumentNullException("Numero de Factura vacio")
        End If
        If String.IsNullOrEmpty(ingressNumber) = True Then
            Throw New ArgumentNullException("Numero de ingreso o consecutivo vacio")
        End If
        Try
            Return _portfolioConciliationRepository.ListInvoiceDetail(container, session.HisContainer, session.SecurityContainer, invoiceNumber, ingressNumber)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Función para validar y agregar facturas desde el sp
    ''' </summary>
    ''' <param name="ListInvoices">Lista de facturas</param>
    ''' <param name="Nit">Numero de Nit</param>
    ''' <param name="container">nombre Contenedor</param>
    ''' <returns>Lista de Factura </returns>
    Public Function ValidateListInvoiceSp(ListInvoices As List(Of String), Nit As String, container As String, ByVal Session As SessionValues) As ActionResult(Of List(Of PortfolioConciliationDetail)) Implements IPortfolioConciliationAdminService.ValidateListInvoiceSp
        If ListInvoices.Count = 0 Then
            Throw New ArgumentNullException("Lista de facturas vacía")
        End If
        If Nit Is String.Empty Then
            Throw New ArgumentNullException("Nit vacío")
        End If
        If Session.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
            If container Is String.Empty Then
                Throw New ArgumentNullException("Nombre Contenedor vacío")
            End If
        End If
        Dim _Result As New ActionResult(Of List(Of PortfolioConciliationDetail))
        Dim _listError As New List(Of String)
        Dim ListPortfolioConciliationDetail As New List(Of PortfolioConciliationDetail)
        Try
            If Session.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
                '_Result = ExcelIntegration(ListInvoices, Nit, container, Session.TransactionalContainer, Session.HisContainer)
            Else 'If Session.IndigoGlossesIntegration = EGlossesIntegration.Native Then
                '_Result = ExcelNative(ListInvoices, Nit, Session.TransactionalContainer, Session.HisContainer)
            End If
            Return _Result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' obtiene una lista de detalles de una recepcion
    ''' </summary>
    ''' <param name="ConciliationId">codigo de la recpcion</param>
    ''' <returns>una lista detalle de oficio</returns>
    Public Function GetListConciliationDetail(ByVal ConciliationId As Integer) As List(Of PortfolioConciliationDetail) Implements IPortfolioConciliationAdminService.GetListConciliationDetail
        If String.IsNullOrEmpty(ConciliationId) Then
            Throw New ArgumentNullException("Id Recepcion vacio")
        End If
        Try
            Return _portfolioConciliationRepository.GetListConciliationDetail(ConciliationId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function ValidateExcelData(ByVal dtSet As DataSet, ByVal Session As SessionValues) As ActionResult Implements IPortfolioConciliationAdminService.ValidateExcelData
        If dtSet Is Nothing AndAlso dtSet.Tables("Datos") Is Nothing Then
            Throw New ArgumentNullException("Dataset de datos glosa excel esta vacio")
        End If

        Dim unitOfWork As IUnitWork = Me._portfolioConciliationRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim xmlObject As String = dtSet.GetXml()

                Dim resultStore = Me._portfolioConciliationRepository.SP_ImportExcelConciliation(xmlObject)
                If resultStore Is Nothing OrElse resultStore.Count = 0 OrElse resultStore.Any(Function(d) d.StatusField <> 0) Then
                    unitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult With {.StateResult = False, .Message = "Proceso Finalizado con errores", .MessageResult = resultStore.Where(Function(d) d.StatusField <> 0).Select(Function(d) d.MessageField).ToList()}
                End If

                transaction.Complete()
                Return New ActionResult With {.StateResult = True, .Message = resultStore.FirstOrDefault().MessageField, .MessageResult = resultStore.Select(Function(d) d.MessageField).ToList()}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
                Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex), .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList()}
            End Try
        End Using
    End Function

    Public Function GetSP_PortfolioConciliation(InvoiceNumber As String, ClosingDate As Date, Session As SessionValues) As SP_PortfolioConciliation_Result Implements IPortfolioConciliationAdminService.GetSP_PortfolioConciliation
        Try
            Return _portfolioConciliationRepository.GetSP_PortfolioConciliation(InvoiceNumber, ClosingDate)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

#Region "Methods Privates"

    Private Function GetDatatable(ByVal Comando As String, session As SessionValues, nameDt As String) As System.Data.DataTable
        Dim connectionString = String.Empty
        connectionString = Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, session.TransactionalContainer, False)
        Using conexion As New SqlConnection
            Try
                If conexion.State = ConnectionState.Closed Then
                    conexion.Open()
                End If
                Dim da As SqlDataAdapter = New SqlDataAdapter(Comando, conexion)
                da.SelectCommand.CommandTimeout = 30000
                Dim ds As New DataSet
                da.Fill(ds, nameDt)
                GetDatatable = ds.Tables(nameDt)
                da = Nothing
                ds = Nothing
                conexion.Close()
                Return GetDatatable
            Catch ex As Exception
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
                Return Nothing
            Finally
                conexion.Close()
            End Try
        End Using
    End Function

#End Region

#Region "IDisposable Support"

    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
            End If
            _portfolioConciliationRepository = Nothing
            secuenseRepository = Nothing
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