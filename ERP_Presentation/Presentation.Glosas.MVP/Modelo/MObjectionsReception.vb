'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Jorge Leonardo Vernaza
' Created          : 09-04-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.Data.Xpo

#End Region



''' <summary>
''' Modelo que se comunica con los servicios corresporndientes al funcional
''' </summary>
Public Class MObjectionsReception
    Implements IDisposable
    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance



#Region "Builders"


    ''' <summary>
    ''' Tago del formulario
    ''' </summary>
    Private _tagForm As String

    Sub New(Tag As String)
        _tagForm = Tag
    End Sub

#End Region

    ''' <summary>
    ''' Obtiene la secuencia numerica asignada al formulario
    ''' </summary>
    ''' <returns>Secuencia numerica</returns>
    Public Async Function GetSequense() As Task(Of Domain.Entities.PortfolioSequence)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetSequenseByIdFormAsync(Me._tagForm)
    End Function
    ''' <summary>
    ''' Funcion para validar que la cuenta este configurada en los parametros NET privado
    ''' </summary>
    ''' <param name="AccountValidate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateAccountTableNEtPrivate(ByVal AccountValidate As String, ByVal ObjectionReception As Boolean) As Boolean
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ValidateAccountTableNEtPrivate(AccountValidate, Me.Indigo, ObjectionReception)
    End Function

    ''' <summary>
    ''' Funcion para validar que la cuenta este configurada en los parametros FOX privado
    ''' </summary>
    ''' <param name="AccountValidate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateAccountTableFOxPrivate(ByVal AccountValidate As String, ByVal ObjectionReception As Boolean) As Boolean
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ValidateAccountTableFOxPrivate(AccountValidate, Me.Indigo, ObjectionReception)
    End Function


    ''' <summary>
    ''' Funcion para obtener la recepcion de la objecion
    ''' </summary>
    ''' <param name="Consecutive">El Consecutivo.</param>
    ''' <returns></returns>
    Public Async Function GetObjectionReception(ByVal Consecutive As String) As Task(Of Object)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GetObjectionAsync(Consecutive, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para obtener las facturas
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetInvoicesAll(ByVal NameContainer As String, ByVal nit As String, ByVal InvoiceNumber As String, ByVal stringSQl As String, ByVal TopQuery As String, ByVal FlagNotConfirmInvoice As String) As Task(Of ActionResult(Of List(Of SP_invoiceList_Result)))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListAllInvoceAsync(NameContainer, nit, InvoiceNumber, Me.Indigo, stringSQl, TopQuery, FlagNotConfirmInvoice)
    End Function

    ''' <summary>
    ''' Funcion para cargra una factura
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function GetInvoice(ByVal NameContainer As String, ByVal nit As String, ByVal InvoiceNumber As String, ByVal stringSQl As String, ByVal FlagNotConfirmInvoice As String) As Task(Of SP_invoiceList_Result)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GetInvoiceAsync(NameContainer, nit, InvoiceNumber, Me.Indigo, stringSQl, FlagNotConfirmInvoice)
    End Function

    ''' <summary>
    ''' Funcion para obtener las sucursales o sedes
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetBranchAll() As Task(Of List(Of Domain.Entities.GlosasParametersInterface))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListInterfacesParametersAsync(Me.Indigo)
    End Function

    ''' <summary>
    ''' Obtiene un cliente por su nit
    ''' </summary>
    ''' <param name="nit">Nit del cliente</param>
    ''' <returns>El cliente</returns>
    Public Async Function GetCustomerByNit(ByVal nit As String) As Task(Of Domain.Entities.Customer)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GetCustomerByNitAsync(nit.Trim(), Me.Indigo)
    End Function

    ''' <summary>
    ''' Obtiene un cliente por su Id
    ''' </summary>
    ''' <param name="id">Nit del cliente</param>
    ''' <returns>El cliente</returns>
    Public Async Function GetCustomerById(ByVal Id As String) As Task(Of Domain.Entities.Customer)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GetCustomerByIdAsync(Id, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para Guardar la recepcion de objeciones
    ''' </summary>
    ''' <returns></returns>
    Public Async Function SaveObjectionsReception(ByVal Record As GlosaObjectionsReceptionC) As Task(Of ActionResult)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.SaveObjectionsReceptionCAsync(Record, Me.Indigo)
    End Function


    ''' <summary>
    ''' Funcion para Guardar el detalle recepcion de objeciones
    ''' </summary>
    ''' <returns></returns>
    Public Async Function SaveObjectionsReceptionDAndPersist(ByVal RadicateConsecutive As String, ByVal ContainerName As String, ByVal GlosaObjectionsReceptionD As GlosaObjectionsReceptionD) As Task(Of ActionResult)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.SaveObjectionsReceptionDAndPersistAsync(RadicateConsecutive, ContainerName, GlosaObjectionsReceptionD, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para Guardar el detalle recepcion de objeciones
    ''' </summary>
    ''' <returns></returns>
    Public Function SaveObjectionsReceptionDAndPersistFor(ByVal RadicateConsecutive As String, ByVal ContainerName As String, ByVal GlosaObjectionsReceptionD As GlosaObjectionsReceptionD) As ActionResult
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.SaveObjectionsReceptionDAndPersist(RadicateConsecutive, ContainerName, GlosaObjectionsReceptionD, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para Eliminar la recepcion de objeciones
    ''' </summary>
    ''' <returns></returns>
    Public Async Function DeleteObjectionsReceptionD(ByVal Record As Object) As Task(Of Boolean)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.DeleteObjectionsReceptionDAsync(Record, Me.Indigo)
    End Function

    ''' <summary>
    ''' Eliminacion masiva
    ''' </summary>
    ''' <param name="listObjDDelete"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteListObjectionReceptionD(ByVal listObjDDelete As List(Of GlosaObjectionsReceptionD)) As Task(Of ActionResult)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.DeleteListObjectionReceptionDAsync(listObjDDelete, Me.Indigo)
    End Function


    ''' <summary>
    ''' Funcion para confirmar la recepcion de objeciones
    ''' </summary>
    ''' <returns></returns>
    Public Async Function ConfirmObjectionsReceptionC(ByVal Record As Object) As Task(Of ActionResult)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ConfirmObjectionsReceptionCAsync(Record, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para Anular la recepcion de objeciones
    ''' </summary>
    ''' <returns></returns>
    Public Async Function InvalidateObjectionsReception(ByVal Record As Object) As Task(Of ActionResult)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.InvalidateObjectionsReceptionCAsync(Record, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para obtener los campos que permiten nulos
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetNullFields() As Task(Of DataSet)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetFieldsNULLAsync("ObjectionsReceptionC", Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para obtener el detalle de una recepcion
    ''' </summary>
    ''' <param name="InvoiceNumber">numero de factura</param>
    ''' <param name="GlosaObjectionsReceptionCId">numero de oficio</param>
    ''' <returns>obtejo detalle recepcion</returns>
    Public Async Function GetReceptionsObjectionD(ByVal InvoiceNumber As String, ByVal GlosaObjectionsReceptionCId As String) As Task(Of GlosaObjectionsReceptionD)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.getObjectionReceptionDAsync(InvoiceNumber, GlosaObjectionsReceptionCId, Me.Indigo)
    End Function


    ''' <summary>
    ''' Funcion para listar el detalle de la recepcion
    ''' </summary>
    ''' <param name="ObjectionReceptionCcode">codigo de la recepción</param>
    ''' <returns>lista de detalles de recepcion</returns>
    Public Async Function ListReceptionsObjectionD(ByVal ObjectionReceptionCcode As String) As Task(Of List(Of GlosaObjectionsReceptionD))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListObjectionReceptionDAsync(ObjectionReceptionCcode, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion Para listar los detalles de Factura Por Numero factura
    ''' </summary>
    ''' <param name="InvoiceNUmber">Numero de Factura</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ListGlosaInvoiceDetailByInvoiceNumber(ByVal InvoiceNUmber As String, ByVal Modulo As String) As Task(Of List(Of GlosaInvoiceDetail))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListGlosaInvoiceDetailByInvoiceNumberAsync(InvoiceNUmber, Modulo, 0, Me.Indigo)
    End Function

    ''' <summary>
    ''' Carga Una Factura
    ''' </summary>
    ''' <param name="IdObjectionReceptionD">Codigo de factura a cargar</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetListReceptionsObjectionD(ByVal IdObjectionReceptionD As String) As Task(Of List(Of GlosaObjectionsReceptionD))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GetListObjectionReceptionDAsync(IdObjectionReceptionD, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion Para Confirmar una Factura
    ''' </summary>
    ''' <param name="ObjectionsReceptionDId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ConfirmObjectionReceptionD(ObjectionsReceptionDId As Integer, ByVal IdSecuense As Integer, ByVal Nit As String, ByVal RadicateConsecutive As String, ByVal valueGlosa As Decimal) As Task(Of ActionResult)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ConfirmObjectionReceptionDAsync(ObjectionsReceptionDId, IdSecuense, Nit, RadicateConsecutive, valueGlosa, Me.Indigo)
    End Function

    ''' <summary>
    ''' Función para validar y agregar facturas desde el sp
    ''' </summary>
    ''' <param name="ListInvoices">Lista de facturas</param>
    ''' <param name="Nit">Numero de Nit</param>
    ''' <param name="container">nombre Contenedor</param>
    ''' <returns>Lista de Factura </returns>
    Public Async Function ValidateListInvoiceSp(ListInvoices As List(Of String), Nit As String, container As String) As Task(Of ActionResult(Of List(Of GlosaObjectionsReceptionD)))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ValidateListInvoiceSpAsync(ListInvoices, Nit, container, Me.Indigo)
    End Function

    ''' <summary>
    ''' Lista de detalle de factura para reiteraciones
    ''' </summary>
    ''' <param name="InvoiceNumber"></param>
    ''' <param name="modulo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ListGlosaInvoiceDetailByInvoiceNumberReiteration(InvoiceNumber As String, modulo As String) As Task(Of ActionResult(Of List(Of GlosaInvoiceDetail)))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListGlosaInvoiceDetailByInvoiceNumberReiterationAsync(InvoiceNumber, modulo, Me.Indigo)
    End Function

    ''' <summary>
    ''' Lista de detalle de factura para reiteraciones
    ''' </summary>
    ''' <param name="InvoiceNumber"></param>
    ''' <param name="modulo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ListGlosaInvoiceDetailByInvoiceNumberReiterationNoNormative(InvoiceNumber As String, modulo As String) As Task(Of ActionResult(Of List(Of GlosaInvoiceDetail)))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListGlosaInvoiceDetailByInvoiceNumberReiterationNoNormativeAsync(InvoiceNumber, modulo, Me.Indigo)
    End Function

    Public Async Function ListGlosaInvoiceDetailByInvoiceNumberReiterationWithOutMovementsAsync(InvoiceNumber As String, modulo As String) As Task(Of ActionResult(Of List(Of GlosaInvoiceDetail)))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListGlosaInvoiceDetailByInvoiceNumberReiterationWithOutMovementsAsync(InvoiceNumber, modulo, Me.Indigo)
    End Function

    ''' <summary>
    ''' Lista todos los responsables
    ''' </summary>
    ''' <returns>Lista responsables</returns>
    Public Async Function ListResponsiblesAll() As Task(Of List(Of ResponsibleAll))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListResponsibleAllAsync(Me.Indigo)
    End Function

    ''' <summary>
    ''' Graba un registro de objecion
    ''' </summary>
    ''' <param name="ListMovementGlosa">Lista de Objeto registro de objecion a grabar</param>
    ''' <returns>Un resultado de la accion</returns>
    Public Async Function SaveMovementGlosa(ByVal ListMovementGlosa As List(Of GlosaMovementGlosa)) As Task(Of ActionResult)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.SaveMovementGlosaAsync(ListMovementGlosa, Me.Indigo)
    End Function

    ''' <summary>
    ''' Graba un registro de objecion
    ''' </summary>
    ''' <param name="ListMovementGlosa">Lista de Objeto registro de objecion a grabar</param>
    ''' <returns>Un resultado de la accion</returns>
    Public Async Function SaveReiterationMovementGlosa(ByVal ListMovementGlosa As List(Of GlosaMovementGlosa)) As Task(Of ActionResult)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.SaveReiterationMovementGlosaAsync(ListMovementGlosa, Me.Indigo)
    End Function

    ''' <summary>
    ''' Guardar una factura
    ''' </summary>
    ''' <param name="ObjectionsReceptionD">Objeto Factura</param>
    ''' <returns></returns>
    Public Async Function SaveObjectionReceptionD(ObjectionsReceptionD As GlosaObjectionsReceptionD) As Task(Of ActionResult)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.SaveObjectionReceptionDAsync(ObjectionsReceptionD, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para cargar un detalle de Factura
    ''' </summary>
    ''' <param name="Id">Codigo detalle de factura</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetGlosaInvoiceDetail(Id As String) As Task(Of GlosaInvoiceDetail)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GetGlosaInvoiceDetailAsync(Id, Me.Indigo)
    End Function

    ''' <summary>
    ''' Obtiene una lista de conceptos de evaluación
    ''' </summary>
    ''' <param name="Type">Tipo Concepto</param>
    ''' <returns>Lista de Conceptos</returns>
    Public Async Function ListConceptsGlosa(ByVal Type As String) As Task(Of List(Of Domain.Entities.ConceptGlosas))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ListConceptGlosaByTypeAsync(Type, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion que retorna un objeto detalle de factura tipo qx
    ''' </summary>
    ''' <param name="InvoiceDetailQXId">Codigo del detalle qx</param>
    ''' <returns></returns>
    Public Async Function GetGlosaInvoiceDetailQX(InvoiceDetailQXId As String) As Task(Of GlosaInvoiceDetailQX)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GetGlosaInvoiceDetailQXAsync(InvoiceDetailQXId, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para guardar el registro bloqueado
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function SaveBlockRecord(ByVal Record As Domain.Entities.BlockRecord) As Task(Of ActionResult(Of Domain.Entities.BlockRecord))
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.SaveBlockRecordAsync(Record, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para eliminar el registro bloqueado
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function DeleteBlockRecord(ByVal Record As Domain.Entities.BlockRecord) As Task(Of ActionResult)
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.DeleteBlockRecordAsync(Record, Me.Indigo)
    End Function

    ''' <summary>
    ''' Obtener registro bloqueado
    ''' </summary>
    ''' <param name="IdForm"></param>
    ''' <param name="IdRecord"></param>
    ''' <returns></returns>
    Public Async Function GetBlockRecord(ByVal IdForm As String, ByVal IdRecord As String) As Task(Of Domain.Entities.BlockRecord)
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetBlockRecordByIdformAndIdRecordAsync(IdForm, IdRecord, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para cargar  saldo de factura
    ''' </summary>
    ''' <param name="ObjD"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function LoadBalanceInvoice(ByVal ObjD As GlosaObjectionsReceptionD) As Task(Of Decimal)
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.LoadBalanceInvoiceAsync(ObjD, Me.Indigo)
    End Function

    ''' <summary>
    ''' funcionalidad excel
    ''' </summary>
    ''' <param name="list"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ListsStructureDetailInvocie(ByVal list As List(Of String)) As Task(Of List(Of GlosaInvoiceDetail))
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListsStructureDetailInvocieAsync(list, Me.Indigo)
    End Function

    ''' <summary>
    ''' Obtiene una lista de facturas por el nit usando las entidades XPO
    ''' </summary>
    ''' <param name="ListInvoice">Nit a consultar</param>
    ''' <returns>Un objeto <see cref="DevExpress.Xpo.XPInstantFeedbackSource" /></returns>
    Public Function ListGetInvoiceExportExcelGlosa(ByVal ListInvoice As List(Of String)) As Object
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).GlosasService.ListGetInvoiceExportExcelGlosa(ListInvoice)
    End Function

    ''' <summary>
    ''' Funcion para validar y cargar los datos del excel de glosas
    ''' </summary>
    ''' <param name="dtset"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ValidateExcelData(ByVal dtset As DataSet) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ValidateExcelDataAsync(dtset, Me.Indigo)
    End Function

    ''' <summary>
    ''' Lista todos los responsables
    ''' </summary>
    ''' <returns>Lista responsables</returns>
    Public Function ReceptionExcelExport(IdRecepcion As String) As DataSet
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ReceptionExcelExport(IdRecepcion, Me.Indigo)
    End Function
    ''' <summary>
    ''' Exportar todos los datos a excel de la recepcion de objeciones.
    ''' </summary>
    ''' <returns>Lista responsables</returns>
    Public Function ReceptionExcelExportFull(IdRecepcion As Integer) As DataTable
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ReceptionExcelExportFull(IdRecepcion, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para asignar responsable de radicar respuesta ante EAPB
    ''' </summary>
    ''' <param name="listObjetionReceptionDetail">Lista</param>
    ''' <returns></returns>
    Public Async Function GlosaObjetionReceptionDetailAssignRadicateResponsible(listObjetionReceptionDetail As List(Of GlosaObjectionsReceptionD)) As Task(Of ActionResult(Of List(Of GlosaObjectionsReceptionD)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GlosaObjetionReceptionDetailAssignRadicateResponsibleAsync(listObjetionReceptionDetail, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para asignar respuesta ante EAPB
    ''' </summary>
    ''' <param name="listObjetionReceptionDetail">Lista</param>
    ''' <returns></returns>
    Public Async Function GlosaObjetionReceptionDetailRadicate(listObjetionReceptionDetail As List(Of GlosaObjectionsReceptionD)) As Task(Of ActionResult(Of List(Of GlosaObjectionsReceptionD)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GlosaObjetionReceptionDetailRadicateAsync(listObjetionReceptionDetail, Me.Indigo)
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: eliminar estado administrado (objetos administrados).
            End If

            ' TODO: liberar recursos no administrados (objetos no administrados) e invalidar Finalize() below.
            ' TODO: Establecer campos grandes como Null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: invalidar Finalize() sólo si la instrucción Dispose(ByVal disposing As Boolean) anterior tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Ponga el código de limpieza en la instrucción Dispose(ByVal disposing As Boolean) anterior.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agregó este código para implementar correctamente el modelo descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
