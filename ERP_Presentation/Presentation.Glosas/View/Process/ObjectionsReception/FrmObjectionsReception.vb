'***********************************************************************
' Assembly         : Presentacion.Glosas
' Author           : Jorge Leonardo Vernaza
' Created          : 09-04-2011
'
' Last Modified By : Rafael Patiño
' Last Modified On :  20-07-2013
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports Presentation.Glosas.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Presentation.Controls
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Entities
Imports Domain.Base.Entities
Imports DevExpress.Utils.Menu
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports System.Drawing
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.GlosasRepository
Imports System.Text
Imports DevExpress.Utils
Imports Presentation.Base.Extension
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports DevExpress.XtraGrid

#End Region

''' <summary>
''' Clase que contiene todo el comportamiento de la vista
''' </summary>
''' 

Public Class FrmObjectionsReception
    Implements IObjectionsReception

#Region "Consts"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Glosas"

#End Region

#Region "Variable Globales, Propiedades y Load"

    Friend WithEvents CtrTraceabilityControl As Presentation.Controls.CtrTraceabilityControl

    ''' <summary>
    ''' Propiedad que contiene el tipo del documento
    ''' </summary>
    Public Property DocumentType As String Implements IObjectionsReception.DocumentType

    ''' <summary>
    ''' Propiedad que obtiene y establece el numero de la factura
    ''' </summary>
    Private _InvoiceNumbertmp As String
    Public Property InvoiceNumbertmp As String
        Get
            Return _InvoiceNumbertmp
        End Get
        Set(value As String)
            _InvoiceNumbertmp = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que obtiene y establece el estado de persistencia de un registro
    ''' </summary>
    Private _CompletedPersist As Boolean
    Public Property CompletedPersist As Boolean Implements IObjectionsReception.CompletedPersist
        Get
            Return _CompletedPersist
        End Get
        Set(value As Boolean)
            _CompletedPersist = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que tiene el nombre del contenedor
    ''' </summary>
    Public Property NameContainer As String Implements IObjectionsReception.NameContainer
        Get
            Return CType(INDglCompany.GetSelectedDataRow, Domain.Entities.GlosasParametersInterface).ContainerName.ToString.Trim
        End Get
        Set(value As String)
            INDglCompany.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que tiene el objeto completo de eliminados del Detalle
    ''' </summary>
    Public Property DeleteObjectionsReceptionD As List(Of GlosaObjectionsReceptionD) Implements IObjectionsReception.DeleteObjectionsReceptionD
        Get
            Return DeleteObjRecD
        End Get
        Set(value As List(Of GlosaObjectionsReceptionD))
            DeleteObjRecD = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que tiene el objeto completo
    ''' </summary>
    Public Property ObjectionsReception As Object Implements IObjectionsReception.ObjectionsReception
        Get
            Return Objeto
        End Get
        Set(value As Object)
            Objeto = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el comentario del oficio
    ''' </summary>
    Public Property Comment As String Implements IObjectionsReception.Comment
        Get
            Return INDmeComment.Text
        End Get
        Set(value As String)
            INDmeComment.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el listado de Sedes o sucursales de parametros interfaz glosas
    ''' </summary>
    Public Property DataSourceBranch As List(Of Domain.Entities.GlosasParametersInterface) Implements IObjectionsReception.DataSourceBranch
        Get
            Return INDglCompany.Properties.DataSource
        End Get
        Set(value As List(Of Domain.Entities.GlosasParametersInterface))
            INDglCompany.Properties.DataSource = value
            If value IsNot Nothing AndAlso value.Count = 1 Then
                INDglCompany.EditValue = value(0).ContainerName
            End If
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que contiene el listado de Facturas
    ''' </summary>
    Public Property DataSourceInvoices As List(Of SP_invoiceList_Result) Implements IObjectionsReception.DataSourceInvoices
        Get
            Return INDgcInvoices.DataSource
        End Get
        Set(value As List(Of SP_invoiceList_Result))
            INDgcInvoices.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el listado de Facturas Objetadas
    ''' </summary>
    Public Property DataSourceObjectsInvoices As List(Of GlosaObjectionsReceptionD) Implements IObjectionsReception.DataSourceObjectsInvoices
        Get
            Return INDgcObjetions.DataSource
        End Get
        Set(value As List(Of GlosaObjectionsReceptionD))
            INDgcObjetions.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene la fecha de oficio
    ''' </summary>
    Public Property DocumentDate As Date Implements IObjectionsReception.DocumentDate
        Get
            Return INDdeDocumentDate.EditValue
        End Get
        Set(value As Date)
            INDdeDocumentDate.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que contiene el nuero de oficio
    ''' </summary>
    Public Property DocumentNumber As String Implements IObjectionsReception.DocumentNumber
        Get
            Return INDtxtDocument.Text
        End Get
        Set(value As String)
            INDtxtDocument.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el id del tercero
    ''' </summary>
    Public Property IdCustomer As String Implements IObjectionsReception.IdCustomer
        Get
            Return INDbteNit.EditValue
        End Get
        Set(value As String)
            INDbteNit.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene la fecha del radicado
    ''' </summary>
    Public Property RadicatedDate As Date Implements IObjectionsReception.RadicatedDate
        Get
            Return INDdeFilingDate.Tag
        End Get
        Set(value As Date)
            INDdeFilingDate.Text = value.ToString("D", indigo.Culture)
            INDdeFilingDate.Tag = value
        End Set
    End Property
    ''' <summary>
    ''' propiedad que contiene el estado del oficio
    ''' </summary>
    Public Property StatusDocument As String Implements IObjectionsReception.StatusDocument
        Get
            Return Me.BarraBotones.StatusRecord
        End Get
        Set(value As String)
            Me.BarraBotones.StatusRecord = value

            If value = 1 Then 'sin confirmar
                Me.BarraBotones.PrepareToolbar(eAction.UpdateAndProcess)
                Me.BarraBotones.SetDocuments(Me.Objeto.Id)
            ElseIf value = 2 Then 'confirmado
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndAudit)
                Me.INDbtnEliminar.Enabled = False
            ElseIf value = 3 Then
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                Me.INDbtnEliminar.Enabled = False
                Me.BarraBotones.SetDocuments(Me.Objeto.Id)
            ElseIf value = 4 Then 'anulada
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndAudit)
                Me.INDbtnEliminar.Enabled = False
            End If
            Me.INDbteNit.Enabled = False
        End Set
    End Property

    ''' <summary>
    ''' Porpiedad para asignar el numero de consecutivo cuando guarda
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Consecutive() As String Implements IObjectionsReception.Consecutive
        Set(value As String)
            Me.INDbteConsecutive.Text = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para cambio de estado de cada registro de la rejilla, cuando se a completado el guardar
    ''' </summary>
    ''' <param name="_InvoiceNumber">numero de factura</param>
    ''' <param name="ObjD">objeto detalle</param>
    Public WriteOnly Property ActiveRecord(_InvoiceNumber As String, ObjD As GlosaObjectionsReceptionD) As String Implements IObjectionsReception.ActiveRecord
        Set(value As String)
            'Obtengo la vista de la  rejilla
            Dim view As ColumnView = Me.INDgcObjetions.MainView
            view.BeginUpdate()
            Try
                Dim rowHandle As Integer = 0
                Dim col2 As DevExpress.XtraGrid.Columns.GridColumn = view.Columns("StateRecord")
                'busco la factura en la rejilla 
                Dim found As GlosaObjectionsReceptionD = Me.DataSourceObjectsInvoices.Find(Function(c As GlosaObjectionsReceptionD) c.GlosaPortfolioGlosada.InvoiceNumber = ObjD.InvoiceNumber)
                Dim index = DataSourceObjectsInvoices.IndexOf(found)
                rowHandle = view.GetRowHandle(index)
                'procedo a eliminar la factura
                Me.DataSourceObjectsInvoices.Remove(found)
                'agrego la factura nuevamente pero ya con el id y el detalle de la misma agregado
                Me.DataSourceObjectsInvoices.Insert(index, ObjD)
                'cambio la columna estado para visulaizar apariencia de completado
                view.SetRowCellValue(rowHandle, col2, True)
            Finally
                'actualizo la vista
                view.EndUpdate()
            End Try
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que contiene el numero de factura 
    ''' </summary>
    Public Property InvoiceNumberInfo As String Implements IObjectionsReception.InvoiceNumber
        Get
            Return INDInvoiceNumberLbl.Text
        End Get
        Set(value As String)
            INDInvoiceNumberLbl.Text = value
        End Set
    End Property
    ''' <summary>
    ''' Asigna la secuencia numerica de cartera para la realizacion de documentos de reclasificacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sequense As Domain.Entities.PortfolioSequence Implements IObjectionsReception.Sequense
        Get
            Return _sequense
        End Get
        Set(value As Domain.Entities.PortfolioSequence)
            _sequense = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As PortfolioSequenceDetail In Me._sequense.PortfolioSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que contiene la unidad operativa.
    ''' </summary>
    ''' <returns></returns>
    Public Property OperatingUnitId As Integer
        Get
            Return Me._operativeUnitId
        End Get
        Set(value As Integer)
            Me._operativeUnitId = value
        End Set
    End Property

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _operativeUnitId As Int32
    ''' <summary>
    ''' Id del cliente.
    ''' </summary>
    Dim CustomerId As String
    ''' <summary>
    ''' Variable que contiene los items a eliminar del detalle
    ''' </summary>
    Dim DeleteObjRecD As List(Of GlosaObjectionsReceptionD)
    ''' <summary>
    ''' objeto recepcion de objeciones
    ''' </summary>
    Dim Objeto As GlosaObjectionsReceptionC
    ''' <summary>
    ''' Objeto que contiene el tercero
    ''' </summary>
    Dim CustomerTmp As Domain.Entities.Customer
    ''' <summary>
    ''' Variable que se utiliza para instanciar el modelo-
    ''' </summary>
    Dim Model As MObjectionsReception
    ''' <summary>
    ''' Variable que se utiliza para instanciar el presentador
    ''' </summary>
    Dim Presenter As PObjectionsReception
    ''' <summary>
    ''' Almacena el nombre del control que hizo el llamado al metodo buscar
    ''' </summary>
    Private _openFindSenser As String
    ''' <summary>
    ''' Variable Para almacenar la factura Seleccionada a glosar o reiterar
    ''' </summary>
    ''' <remarks></remarks>
    Dim ObjetoD As GlosaObjectionsReceptionD
    ''' <summary>
    ''' variable para almacenar el valor de la seleccion o glosa general
    ''' </summary>
    ''' <remarks></remarks>
    Dim SumValorEntidad As Decimal
    ''' <summary>
    ''' variable para almacenar el id del concepto cuando cambia en el repositorio de la rejilla de detalle de factura
    ''' </summary>
    ''' <remarks></remarks>
    Dim SpecificConceptId As Integer
    ''' <summary>
    ''' variable para almacenar el codigo del concepto cuando cambia en el repositorio de la rejilla de detalle de factura
    ''' </summary>
    ''' <remarks></remarks>
    Dim SpecificConceptCode As String
    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As Domain.Entities.BlockRecord
    ''' <summary>
    ''' Bandera para saber si el registro esta bloqueado
    ''' </summary>
    ''' <remarks></remarks>
    Private FlagBlockRecord As Boolean
    ''' <summary>
    ''' Sumatoria de todos los movimientos principales de la factura
    ''' </summary>
    ''' <remarks></remarks>
    Private sumValueGLosadoTotal As Decimal
    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequense As Domain.Entities.PortfolioSequence
    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequense As Int64
    ''' <summary>
    ''' lista de mensajes de validacion
    ''' </summary>
    ''' <remarks></remarks>
    Dim LIstMessage As List(Of String) = New List(Of String)
    ''' <summary>
    ''' datasource de xpo de clientes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DatasourceCustomers As XPInstantFeedbackSource
    ''' <summary>
    ''' Acumula el valor glosado total
    ''' </summary>
    ''' <remarks></remarks>
    Dim AcumulativoValorGlosado As Decimal
    ''' <summary>
    ''' foco sobre la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Dim _indexFocus As Integer = 0
    ''' <summary>
    ''' bandera si reitera qx
    ''' </summary>
    ''' <remarks></remarks>
    Private _reiterationQx As Boolean
    ''' <summary>
    ''' saldo
    ''' </summary>
    ''' <remarks></remarks>
    Dim balance As Decimal
    ''' <summary>
    ''' Variable para saber si el frontal abre por modo busqueda
    ''' </summary>
    Dim SearchMode As Boolean
    ''' <summary>
    ''' Parametros de Glosas
    ''' </summary>
    Private _parameterGlosas As TimeParameters

    ''' <summary>
    ''' entidad de la respuesta de la radicación
    ''' </summary>
    Private objradicateResponse As RadicateResponse

#End Region

#Region "CRUD base"
    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        Try
            If ValidateControls() = False Then
                Exit Sub
            End If
            AssigningValues()
            If DataSourceObjectsInvoices.Count = 0 Then
                Exit Sub
            End If
            If Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
                If ValidateInvoiceStateERP() = False Then
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NoPuedeGuardarPorEstadoERP, RecepcionObjeciones)
                    Exit Sub
                End If
            End If
            Me.BarraBotones.Enabled = False
            Dim result As ActionResult = Await Presenter.saveObjectionsReception(Objeto, Me.DataSourceObjectsInvoices)
            Me.BarraBotones.Enabled = True
            If result.StateResult = True Then
                Objeto.Id = result.MessageResult(0).ToString
                INDbteConsecutive.Text = result.MessageResult(1).ToString
                If Objeto.State = 1 Then
                    'actualizamos el objeto
                    If INDbteConsecutive.Text IsNot Nothing Then
                        AsyncLoader(True)
                        Objeto = Await Model.GetObjectionReception(INDbteConsecutive.Text)
                        AsyncLoader(False)
                    End If
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
                    LogicaBotonActualizar(True)
                    Me.BarraBotones.PrepareToolbar(eAction.UpdateAndProcess)
                ElseIf Objeto.State = 2 Then
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesConfirmadoCorrectamente)
                End If
                'Se indexa la información
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
            Else
                If result.MessageResult IsNot Nothing AndAlso result.MessageResult(0) = "-999" Then
                    Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(Eresources.ComunesErrorConcurrencia)
                Else
                    For Each itemMensaje As String In result.MessageResult
                        Mensaje(EeventViewerImages.Advertencia) = itemMensaje
                    Next
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesContacteAdministrador)
                End If
            End If
        Catch ex As Exception
            Me.BarraBotones.Enabled = True
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
            Deshacer()
        End Try
    End Sub

    ''' <summary>
    ''' Metodo para anular
    ''' </summary>
    Private Sub InvalidateOrConfirm(ByVal Confirm As Boolean)
        If Objeto IsNot Nothing Then
            If Objeto.State = 2 Or Objeto.State = 4 Then
                Exit Sub
            End If
        Else
            Exit Sub
        End If
        Dim msj As String = String.Empty
        'si se confirma
        If Confirm = True Then
            If ValidateAllInvoiceConfirm() = False Then
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(FacturaSinConfirmarEnLista, RecepcionObjeciones)
                Exit Sub
            End If
            msj = obtenerRecurso(ComunesPreguntaConfirmar)
        Else
            msj = obtenerRecurso(ComunesPreguntaAnular)
        End If
        If MessageIndigo.Show(msj, MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            If Confirm = True Then
                ConfirmObjc()
            Else
                InvalidateObjc()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo para anular
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub InvalidateObjc()
        Dim StateTmp As String = Objeto.State
        Objeto.State = 4 'Anulo
        Dim result As ActionResult
        AsyncLoader(True)

        If INDgcvObjetions.RowCount > 1 Then
            INDgcvObjetions.SelectAll()
            DeleteMasiveDetail()
        End If

        result = Await Model.InvalidateObjectionsReception(Objeto)
        AsyncLoader(False)
        If result.StateResult = True Then
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesAnuladoCorrectamente)
            SearchMode = False
            Deshacer()
        Else
            If result.MessageResult IsNot Nothing AndAlso result.MessageResult(0) = "-999" Then
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(Eresources.ComunesErrorConcurrencia)
            Else
                Objeto.State = StateTmp
                If result.Message IsNot Nothing And result.Message.Length > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                Else
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NoPuedeAnularHayMovimiento, RecepcionObjeciones)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo Para Confirmar
    ''' </summary>
    Private Async Sub ConfirmObjc()
        AssigningValues()
        Dim StateTmp As String = Objeto.State
        Objeto.State = 2 ' Confirmo
        Objeto.ConfirmUser = indigo.UserIndigoId
        Dim result As ActionResult
        AsyncLoader(True)
        Objeto.ConfirmDate = Date.Now
        result = Await Model.ConfirmObjectionsReceptionC(Objeto)
        AsyncLoader(False)
        If result.StateResult = True Then
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesConfirmadoCorrectamente)
            SearchMode = False
            Deshacer()
        Else
            If result.MessageResult IsNot Nothing AndAlso result.MessageResult(0) = "-999" Then
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(Eresources.ComunesErrorConcurrencia)
            ElseIf result?.MessageResult?.Any() Then
                Mensaje(EeventViewerImages.MensajeError) = result?.MessageResult?.FirstOrDefault
            Else
                Objeto.State = StateTmp
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Funcion que valida que en la lista de factura vaya por lo menos una factura con estado ERp correcto
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateInvoiceStateERP() As Boolean
        Dim Validate As Boolean = True
        Dim conteo As Integer = 0
        For Each item As GlosaObjectionsReceptionD In Me.DataSourceObjectsInvoices
            If item.ObservationInvoiceCode <> "2" And item.ObservationInvoiceCode <> "3" And item.ObservationInvoiceCode <> "4" Then
                conteo = conteo + 1
            End If
        Next
        If conteo = Me.DataSourceObjectsInvoices.Count Then
            Validate = False
        End If
        Return Validate
    End Function

    ''' <summary>
    ''' Valida que Todas las facturas esten confirmada
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateAllInvoiceConfirm() As Boolean
        For Each item As GlosaObjectionsReceptionD In DataSourceObjectsInvoices
            If item.State = 1 Then
                Return False
            End If
        Next
        Return True
    End Function

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements ICrudBase.Eliminar
        Try
            If Objeto IsNot Nothing Then
                If Objeto.Id > 0 Then
                    If Objeto.State = 1 Then
                        If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                            AsyncLoader(True)
                            If Await Model.DeleteObjectionsReceptionD(Objeto) = True Then
                                Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                                SearchMode = False
                                Deshacer()
                            Else
                                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                            End If
                            AsyncLoader(False)
                        End If
                    End If
                Else
                    Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesEliminarConfirmado)
                End If
            Else
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesSeleccioneRegistroEliminar)
            End If
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub


    ''' <summary>
    ''' METODO: Eliminar De la Columna de Acciones
    ''' </summary>
    Public Async Sub DeleteItemDetail(ByVal _ObjectionsReceptionD As GlosaObjectionsReceptionD) Implements IObjectionsReception.DeleteItemDetail
        Try
            If _ObjectionsReceptionD IsNot Nothing Then
                If _ObjectionsReceptionD.Id > 0 Then
                    If Objeto.State = 4 Then
                        Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NoPuedeRealizarAccionAnulado, RecepcionObjeciones)
                        Exit Sub
                    End If
                    If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                        Dim result As Boolean = False
                        AsyncLoader(True)
                        result = Await Model.DeleteObjectionsReceptionD(_ObjectionsReceptionD)
                        AsyncLoader(False)
                        If result Then
                            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                            Me.DataSourceObjectsInvoices.Remove(_ObjectionsReceptionD)
                            Me.INDgcObjetions.RefreshDataSource()
                            '  ListObjectionsInvoice.Remove(_ObjectionsReceptionD)
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NoPuedeEliminarPorMovimiento, Eform.RecepcionObjeciones)
                        End If
                    Else
                    End If
                Else
                    Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(NoPuedeEliminarPorMovimiento)
                End If
            Else
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesSeleccioneRegistroEliminar)
            End If
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements ICrudBase.Nuevo
        CleanControls()
    End Sub


    ''' <summary>
    ''' Metodo para Abrir PopUp de busqueda de acuerdo al foco donde se encuentra.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub AbrirBusqueda() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Sub
        End If
        Select Case Me._openFindSenser.ToUpper
            Case Me.INDbteConsecutive.Name.ToUpper() 'Abre el frontal para buscar consecutivo
                AbrirBusquedaConsecutive()
                'Case Me.INDbteNit.Name.ToUpper() 'Abre el frontal para buscar nits
                '    AbrirBusquedaCustomers()
            Case Else 'Si esta sobre un control en donde no aplica el metodo buscar
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.ComunesNoAplicaBuscar, Eform.Comunes)
        End Select
    End Sub
    ''' <summary>
    ''' Abrirs the busqueda consecutive.
    ''' </summary>
    Public Sub AbrirBusquedaConsecutive()
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValueObjection
        With FormSearchObjects
            .ValorSolicitado = "RadicatedConsecutive"
            .ListaColumnas = {New ColumnInfo With {.Caption = "Entidad", .FieldName = "NitName"}, New ColumnInfo With {.Caption = "N° Factura", .FieldName = "InvoiceNumber"}, New ColumnInfo With {.Caption = "Fecha", .FieldName = "DocumentDate"}, New ColumnInfo With {.Caption = "N° Consecutivo", .FieldName = "RadicatedConsecutive"}, New ColumnInfo With {.Caption = "Estado", .FieldName = "State"}}.ToList()
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ObjectionsReceptionD
            .FormParent = Me
            .ShowSearch(False)
        End With
        SearchMode = True
    End Sub

    ''' <summary>
    ''' Abrirs the busqueda de terceros
    ''' </summary>
    'Public Sub AbrirBusquedaCustomers()
    '    FormSearchObjects = New FrmBusqueda
    '    AddHandler FormSearchObjects.ReturnValueWithList, AddressOf ReturnValueCustomers
    '    'lista de string de solicitados
    '    Dim listSolicitado As List(Of String) = New List(Of String)
    '    listSolicitado.Add("Nit")
    '    listSolicitado.Add("Name")
    '    listSolicitado.Add("Id")
    '    With FormSearchObjects
    '        .ListaColumnas = {New ColumnInfo With {.Caption = "Nit", .FieldName = "Nit"}, New ColumnInfo With {.Caption = "Nombre", .FieldName = "Name"}}.ToList()
    '        .ListadoSolicitud = listSolicitado
    '        .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.Customers
    '        .FormParent = Me
    '        .ShowSearch(False)
    '    End With
    'End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Sub ReturnValueObjection(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        INDbteConsecutive.Text = ReturnValue
        BlockedRecord()
        BarraBotones.RibbonPagEform.Visible = True
        LoadControls()
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    'Private Sub ReturnValueCustomers(ByVal ReturnValue As String, ByVal ReturnObject As Object, ByVal ReturnList As List(Of String))
    '    'variable de la lista de string devueltos
    '    Dim listDevuelto As List(Of String)
    '    listDevuelto = ReturnList
    '    Dim ctomerTmp = CType(ReturnObject, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)
    '    Dim ctomer = CType(ctomerTmp.OriginalRow, Infrastructure.Data.Xpo.GlosasRepository.GlosasCustomerXpo)
    '    Me.CustomerTmp = New Domain.Entities.Customer With {.Id = ctomer.Id, .Nit = ctomer.Nit.ToString().Trim(), .Name = ctomer.Name.ToString().Trim(), .State = ctomer.State}
    '    If (FormSearchObjects.ListadoDevolucion.Count > 0) Then
    '        INDbteNit.Text = FormSearchObjects.ListadoDevolucion.Item(0)
    '        CustomerId = FormSearchObjects.ListadoDevolucion.Item(2)
    '        Me.INDgcInvoices.DataSource = Nothing
    '    End If
    'End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
        If SearchMode Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
        BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property
#End Region

#Region "Metodos Funciones Propiedades"

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer

        Dim detailString As String = " [ "
        For Each d As Domain.Entities.GlosaObjectionsReceptionD In Me.DataSourceObjectsInvoices
            If detailString.Length = 3 Then
                detailString &= String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContentDetail", NAME_MODULE), d.InvoiceNumber.Trim(), d.GlosaPortfolioGlosada.PatientCode, d.GlosaPortfolioGlosada.PatientName)
            Else
                detailString &= " - " & String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContentDetail", NAME_MODULE), d.InvoiceNumber.Trim(), d.GlosaPortfolioGlosada.PatientCode, d.GlosaPortfolioGlosada.PatientName)
            End If
        Next
        detailString &= " ]"

        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.Objeto.Customer.Nit.Trim(), Me.Objeto.Customer.Name.Trim().ToLower(), Me.Objeto.DocumentNumber.Trim(), detailString), .CreationDate = dateServer, .CreationUser = SessionValues.Instance.UserIndigo & "-" & SessionValues.Instance.UserIndigoName, .DocumentType = IndexedDocumentType.File, .IdEntity = "$#" & Me.Tag & "_" & Me.Objeto.RadicatedConsecutive & "#$", .IdForm = Me.Tag, .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.Objeto.RadicatedConsecutive), .Update = dateServer, .UpdateUser = SessionValues.Instance.UserIndigo & "-" & SessionValues.Instance.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = SessionValues.Instance.UserIndigo & "-" & SessionValues.Instance.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.Objeto.Customer.Nit.Trim(), Me.Objeto.Customer.Name.Trim().ToLower(), Me.Objeto.DocumentNumber.Trim(), detailString)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.Objeto.RadicatedConsecutive)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Crea y carga el control de tiempo en la barra
    ''' </summary>
    Private Sub LoadXtraTrackControl()
        Me.CtrTraceabilityControl = New CtrTraceabilityControl()
        Me.CtrTraceabilityControl.Process = GlosasProcess.Objection
        Me.CtrTraceabilityControl.Dock = DockStyle.Fill
        Me.AdditionalControlPanel.Controls.Add(Me.CtrTraceabilityControl)
    End Sub

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StateResponded"), .StatusColor = System.Drawing.Color.OrangeRed})
        listStates.Add(New StatusRecord With {.StatusValue = "4", .StatusName = ResourceManager.GetString("StateInvalidate"), .StatusColor = System.Drawing.Color.OrangeRed})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Evento edit value changing para verificar que la factura seleccionada se pueda agregar al listado de facturas a glosar.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ChangingEventArgs"/> instance containing the event data.</param>
    Private Sub INDrchSeleccion_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrchSeleccion.EditValueChanging
        If Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
            Dim Objsp_invoice As SP_invoiceList_Result = CType(INDgcvInvoices.GetRow(INDgcvInvoices.FocusedRowHandle), SP_invoiceList_Result)
            If ValidateStateinvoice(Objsp_invoice) = False Then
                e.Cancel = True
            End If
        End If
    End Sub


    ''' <summary>
    ''' Funcion para validar si las facturas se pueden agregar
    ''' </summary>
    ''' <param name="Objsp_invoice"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateStateinvoice(ByVal Objsp_invoice As SP_invoiceList_Result) As Boolean
        Dim validate As Boolean = True
        Dim ComplementoMensaje As String
        If Objsp_invoice Is Nothing Then
            Return False
        End If

        'valido los tipos de estado que viene desde el ERP
        If Objsp_invoice.StateCurrentInvoice <> "2" And Objsp_invoice.StateCurrentInvoice <> "3" Then
            ComplementoMensaje = StateErp(Objsp_invoice.StateCurrentInvoice)
            MessageIndigo.Show(String.Format(obtenerRecurso(NoSePuedeAgregarFacturaEstado, RecepcionObjeciones), ComplementoMensaje), MessageType.Warning, Me.Text)
            validate = False
        End If

        If Objsp_invoice.RadicatedConsecutive IsNot Nothing Then

            'no se permiten 2 reiteraciones a la misma factura
            If Objsp_invoice.Reiterated >= 2 Then
                MessageIndigo.Show(String.Format(obtenerRecurso(NosePuedeAgregaFacturaReiterada, RecepcionObjeciones), Objsp_invoice.RadicatedConsecutive.ToString), MessageType.Warning, Me.Text)
                validate = False
                Return False
            End If

            If Objsp_invoice.StateObjectionReceptionC <> 2 And Objsp_invoice.StateObjectionReceptionC <> 4 Then
                ComplementoMensaje = Objsp_invoice.RadicatedConsecutive
                MessageIndigo.Show(String.Format(obtenerRecurso(NosePuedeAgregaFacturaRadicada, RecepcionObjeciones), ComplementoMensaje), MessageType.Warning, Me.Text)
                validate = False
            End If

            If Objsp_invoice.StatePortfolioGlosada <> 11 And Objsp_invoice.StateObjectionReceptionC <> 4 And Objsp_invoice.StatePortfolioGlosada <> 14 Then
                ComplementoMensaje = Objsp_invoice.RadicatedConsecutive
                MessageIndigo.Show(String.Format(obtenerRecurso(NosePuedeAgregaFacturaEnProceso, RecepcionObjeciones), ComplementoMensaje), MessageType.Warning, Me.Text)
                validate = False
            End If
        End If
        Return validate
    End Function

    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IObjectionsReception.ActionsOnControls
        Set(ByVal value As Boolean)
            INDbteConsecutive.Enabled = Not value
            INDbteNit.Enabled = value
            INDdeDocumentDate.Enabled = value
            INDdeFilingDate.Enabled = value
            INDtxtDocument.Enabled = value
            INDglCompany.Enabled = value
            INDpceBuscaFactura.Enabled = value
            INDmeComment.Enabled = value
            INDgcObjetions.Visible = value
            INDpceMore.Enabled = value
            If value = True Then
                INDbteNit.Focus()
                'INDlyiFacturas.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                INDbteConsecutive.Focus()
                '  INDlyiFacturas.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        End Set
    End Property

    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        INDpceBuscaFactura.Text = String.Empty
        INDpceBuscaFactura.Hide()
        Me.BarraBotones.StatusRecord = "1" 'Sin confirmar
        Objeto = Nothing
        Me.INDbtnEliminar.Enabled = True
        CustomerTmp = Nothing
        INDbteConsecutive.Text = String.Empty
        IdCustomer = Nothing
        INDbteNit.EditValue = Nothing
        INDbteNit.DisplayNullText = String.Empty
        INDbteNit.Enabled = True
        INDdeDocumentDate.EditValue = Date.Now
        INDdeFilingDate.Text = String.Empty
        INDtxtDocument.Text = String.Empty
        INDmeComment.Text = String.Empty
        INDglCompany.EditValue = Nothing
        LogicaBotonActualizar(False)
        DataSourceInvoices = New List(Of SP_invoiceList_Result)
        INDlyiInvoiceDetailGroupNavigate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyiInoices.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDlyiDatosGenerales.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDlyiFacturas.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        objradicateResponse = Nothing
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.RadicateResponse) = True
        BarraBotones.RibbonPagEform.Visible = True
        'Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        BarraBotones.RibbonPageProcesos.Visible = False
        INDgcObjetions.DataSource = Nothing
        INDGcNoNormative.DataSource = Nothing
        BlockedRecord()
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.DisableBarDocument()
        CtrTraceabilityControl.Visible = False
        Me.AdditionalControlPanel.Visible = False
        Me.BarraBotones.Enabled = True
        Me.IndigoGridControl1.RefreshGrid(Me.INDgcObjetions)
        Me.BarraBotones.CleanAuditBasic()
        Me.DataSourceObjectsInvoices = New List(Of GlosaObjectionsReceptionD)
        ActionsOnControls = False
        'Me.INDbteConsecutive.Focus()        
    End Sub

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControls() As Boolean
        ValidateControls = True
        If INDbteConsecutive.Text = String.Empty Then
            ValidateControls = False
        End If
        If INDtxtDocument.Text = String.Empty Then
            ValidateControls = False
        End If
        If INDdeDocumentDate.Text = String.Empty Then
            ValidateControls = False
        End If
        If INDdeFilingDate.Text = String.Empty Then
            ValidateControls = False
        End If
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With Objeto
            .Comment = INDmeComment.Text
            .DocumentDate = INDdeDocumentDate.EditValue
            .DocumentNumber = INDtxtDocument.Text
            .RadicatedDate = INDdeFilingDate.Tag
            .RadicatedUser = indigo.UserIndigoId
            .CustomerId = CustomerTmp.Id
            .State = 1
            If objradicateResponse IsNot Nothing Then
                .RadicateResponse = objradicateResponse
            End If
        End With
    End Sub

    ''' <summary>
    ''' Elimina el Registro Bloqueado
    ''' </summary>
    ''' <remarks></remarks>
    Async Sub BlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 Then
            Await Model.DeleteBlockRecord(record)
            record = Nothing
        End If
        Me.BarraBotones.EnableBarItems()
        Me.FlagBlockRecord = False
    End Sub


    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Sub LoadControls()
        Me.BarraBotones.StatusRecordVisible = True
        Dim d As DateTime = GetServerDate()
        INDdeFilingDate.Tag = d
        INDdeFilingDate.Text = d.ToString("D", indigo.Culture)
        ActionsOnControls = True
        INDbteConsecutive.Enabled = False
        If INDbteConsecutive.Text <> String.Empty Then
            AsyncLoader(True)
            Objeto = Await Model.GetObjectionReception(INDbteConsecutive.Text)
            If Objeto IsNot Nothing Then
                Me.DataSourceObjectsInvoices = Await Model.ListReceptionsObjectionD(Objeto.Id)
                If DataSourceObjectsInvoices.Count > 0 Then
                    If Objeto.RadicateResponse IsNot Nothing OrElse (Await ValidateIsNoNormativeInvoices(DataSourceObjectsInvoices)) Then

                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.RadicateResponse) = False
                    End If
                    Me.CompletedPersist = True
                End If
            End If
            AsyncLoader(False)
        End If
        If Not Objeto Is Nothing Then
            If Objeto.Id > 0 Then
                Dim result As New Domain.Entities.BlockRecord
                result = Await Model.GetBlockRecord(Me.Tag, Objeto.Id)
                With Objeto
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                    INDmeComment.Text = .Comment
                    INDbteNit.EditValue = .CustomerId
                    INDbteNit.DisplayNullText = .Customer.Nit & " - " & .Customer.Name
                    INDdeFilingDate.Tag = .RadicatedDate
                    INDdeFilingDate.Text = .RadicatedDate.ToString("D", indigo.Culture)
                    INDdeDocumentDate.EditValue = .DocumentDate
                    INDtxtDocument.Text = .DocumentNumber
                    INDglCompany.Enabled = True
                    Me.StatusDocument = .State
                    Me.CustomerTmp = .Customer
                End With
                INDtxtDocument.Focus()
                INDbteNit.Enabled = False

                'envio a bloquear el registro
                If result IsNot Nothing Then
                    If result.Id = 0 Then
                        Dim state = New Domain.Base.Entities.ObjectChangeTracker
                        state.State = Domain.Base.Entities.ObjectState.Added
                        record = New Domain.Entities.BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = Objeto.Id}
                        Dim operation = Await Model.SaveBlockRecord(record)
                        record = operation.ObjectEmbbeded
                        FlagBlockRecord = False
                        If Me.StatusDocument = "2" Then
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GenerateFile) = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, Me.Objeto.Id, 0, {Me.Objeto.Id, False, Me.BarraBotones.OperatingUnit, False})
                        End If
                    Else
                        If result.CodUser.Trim().Equals(Me.indigo.UserIndigo.Trim()) Then
                            Me.FlagBlockRecord = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, Me.Objeto.Id, 0, {Me.Objeto.Id, False, Me.BarraBotones.OperatingUnit, False})
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GenerateFile) = False
                        Else
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndAudit)
                            record = result
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                            Me.FlagBlockRecord = True
                        End If
                    End If
                End If
                Me.GetDocumentIndexed(Me.Tag & "_" & Me.Objeto.RadicatedConsecutive)
            Else
                If INDbteConsecutive.Text <> String.Empty Then
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ConsecutivoObjecionNoExiste, RegisterObjection)
                    INDbteConsecutive.Text = String.Empty
                    ActionsOnControls = False
                    INDbteConsecutive.Enabled = True
                    INDbteConsecutive.Focus()
                Else
                    INDbteConsecutive.Text = obtenerRecurso(LabelNuevo, RecepcionObjeciones)
                    Objeto = New GlosaObjectionsReceptionC
                    'LogicaBotonActualizar(False)
                    Me.BarraBotones.PrepareToolbar(eAction.Save)
                    INDlyiFacturas.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                End If
            End If
        Else
            INDbteConsecutive.Text = obtenerRecurso(LabelNuevo, RecepcionObjeciones)
            Objeto = New GlosaObjectionsReceptionC
            Me.BarraBotones.PrepareToolbar(eAction.Save)
            INDlyiFacturas.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
        If DataSourceBranch IsNot Nothing AndAlso DataSourceBranch.Count = 1 Then
            INDglCompany.EditValue = DataSourceBranch(0).ContainerName
        End If
    End Sub

    ''' <summary>
    ''' Imprimir Reporte
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, Me.Objeto.Id, 0, {Me.Objeto.Id, False, Me.BarraBotones.OperatingUnit, False})
    End Sub

    ''' <summary>
    ''' Metodo para cargar las facturas
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub seleccionarFacturas()
        Try
            INDpceBuscaFactura.Enabled = False
            If Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
                If INDglCompany.EditValue IsNot Nothing And INDbteNit.EditValue IsNot Nothing Then
                    Dim ObjCompany As Domain.Entities.GlosasParametersInterface = CType(INDglCompany.GetSelectedDataRow, Domain.Entities.GlosasParametersInterface)
                    If ObjCompany IsNot Nothing Then
                        AsyncLoader(True)
                        Dim _actionResult As ActionResult(Of List(Of SP_invoiceList_Result)) = Await Model.GetInvoicesAll(ObjCompany.ContainerName, INDbteNit.EditValue, String.Empty, String.Empty, "50", "0")
                        AsyncLoader(False)
                        If _actionResult.StateResult = True Then
                            Me.DataSourceInvoices = _actionResult.ObjectEmbbeded
                        End If
                    End If
                End If
            Else
                If INDbteNit.EditValue IsNot Nothing Then
                    AsyncLoader(True)
                    Dim _actionResult As ActionResult(Of List(Of SP_invoiceList_Result)) = Await Model.GetInvoicesAll(String.Empty, INDbteNit.EditValue, String.Empty, String.Empty, "50", "0")
                    AsyncLoader(False)
                    If _actionResult.StateResult = True Then
                        Me.DataSourceInvoices = _actionResult.ObjectEmbbeded
                    End If
                End If
            End If
            INDpceBuscaFactura.Enabled = True
            'ciclo para eliminar del Popup de las facturas las que ya han sido guardadas en la BD
            For i As Integer = 0 To Objeto.GlosaObjectionsReceptionD.Count - 1
                Dim j As Integer = i
                Dim found As SP_invoiceList_Result = Nothing
                'busqueda de la factura
                found = Me.DataSourceInvoices.Find(Function(value As SP_invoiceList_Result) value.InvoiceNumber = Objeto.GlosaObjectionsReceptionD(j).InvoiceNumber)
                'procedo a eliminar la factura de la Rejilla del PopUp de Facturas
                If found IsNot Nothing Then
                    Me.DataSourceInvoices.Remove(found)
                End If
            Next
            'refresco la rejilla de facturas
            Me.INDgcInvoices.RefreshDataSource()
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub


    ''' <summary>
    ''' Metodo para cargar Datos al PopUpConatinaer de los detalles de factura
    ''' </summary>
    Private Async Sub ShowPopUpInvoiceDatail()
        Dim idObjecionReceptioD As Integer
        If Objeto IsNot Nothing Then
            If Objeto.State = 4 Then
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NoPuedeRealizarAccionAnulado, RecepcionObjeciones)
                Exit Sub
            End If
        End If

        If INDgcvObjetions.FocusedRowHandle >= 0 Then
            ObjetoD = TryCast(INDgcvObjetions.GetRow(INDgcvObjetions.FocusedRowHandle), GlosaObjectionsReceptionD)
            If ObjetoD.Id > 0 Then

                idObjecionReceptioD = ObjetoD.Id.ToString()
                INDMemoGeneralComment.Text = ObjetoD.Comment
                InvoiceNumbertmp = ObjetoD.InvoiceNumber.ToString()

                If InvoiceNumber IsNot Nothing Then
                    AsyncLoader(True)
                    balance = Await Model.LoadBalanceInvoice(ObjetoD)
                    INDLblBalanceInvoice.Text = ManageDecimalsFun(balance)
                    Dim invoices

                    Dim resultlist As ActionResult(Of List(Of GlosaInvoiceDetail))
                    If ObjetoD.DocumentType = "2" Then
                        resultlist = Await Model.ListGlosaInvoiceDetailByInvoiceNumberReiteration(InvoiceNumbertmp, "RE")
                        invoices = resultlist.ObjectEmbbeded
                        INDLcgNoNormative.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    Else
                        invoices = Await Model.ListGlosaInvoiceDetailByInvoiceNumber(InvoiceNumbertmp, "RA")
                        INDLcgNoNormative.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    End If

                    INDgcInvoiceDetailGrid.DataSource = invoices
                    INDGcNoNormative.DataSource = (Await Model.ListGlosaInvoiceDetailByInvoiceNumberReiterationNoNormative(InvoiceNumbertmp, "RE")).ObjectEmbbeded
                    INDgleResponsible.DataSource = Await Model.ListResponsiblesAll()

                    Dim concepts = Await Model.ListConceptsGlosa("1")
                    Dim conceptsNonNormaive = Await Model.ListConceptsGlosa("4")
                    INDgleConcept.DataSource = concepts
                    INDRptGleConceptNoNormative.DataSource = conceptsNonNormaive
                    INDRptGleResponsibleNoNormative.DataSource = Await Model.ListResponsiblesAll()
                    TabbedControlGroup1.SelectedTabPageIndex = 0
                    AsyncLoader(False)

                    EnabledProcess(False)

                    ' Factura Confirmada se bloque controles tan para glosa como reiteracion
                    If ObjetoD.State = "2" Then
                        Me.BarraBotones.RibbonPageProcesos.Visible = False
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Actualizar) = True
                        Me.BarraBotones.StatusRecord = "2"
                        INDBtnGuardarComment.Enabled = False

                        ClValueAcceptedFirtsInstance.VisibleIndex = -1
                        ClValueReiterated.VisibleIndex = -1
                        ClValueGlosa.VisibleIndex = -1
                        ClConcept.VisibleIndex = -1
                        ClResponsible.VisibleIndex = -1
                        ClComment.VisibleIndex = -1
                        ClsaveMovementGlosa.VisibleIndex = -1
                        ClValorEntidad.VisibleIndex = 5
                        ValueGlosado.VisibleIndex = 6
                        ClseeDetail.VisibleIndex = 7
                        ClMoreColumn.VisibleIndex = 8
                        Reg.VisibleIndex = 9

                        ClqxServiceCodeName.VisibleIndex = 0
                        ClqxAmmount.VisibleIndex = 1
                        ClqxInvoicedValue.VisibleIndex = 2
                        ClqxValorGlosado.VisibleIndex = 3

                        If ObjetoD.DocumentType = "2" Then
                            ClqxUnitValue.VisibleIndex = -1
                            ClqxValorGlosado.VisibleIndex = 3
                            ClqxValueAcceptedFirtsInstance.VisibleIndex = 4
                            ClqxValueReiterated.VisibleIndex = 5
                            ClqxseeDetail.VisibleIndex = 7
                            ClqxValueGlosa.VisibleIndex = -1
                            ClqxConcept.VisibleIndex = -1
                            ClqxResponsible.VisibleIndex = -1
                            ClqxComment.VisibleIndex = -1
                            ClqxsaveMovementGlosa.VisibleIndex = -1
                            ClqxMoreColumn.VisibleIndex = 6
                            ClqxAcciones.VisibleIndex = 7

                            'columnas del itemreposity de visualizacion de mas informacion 
                            INDClMainGlosa.VisibleIndex = 0
                            INDClConcepto.VisibleIndex = 1
                            INDClValueGlosa.VisibleIndex = 2
                            INDclValueAcceptedFirstInstance.VisibleIndex = 3
                            INDClValueReiteration.VisibleIndex = 4
                            INdclValueAcceptedSecondInstance.VisibleIndex = 5
                        Else
                            ClqxUnitValue.VisibleIndex = 3
                            ClqxValorGlosado.VisibleIndex = 4
                            ClqxValueAcceptedFirtsInstance.VisibleIndex = -1
                            ClqxValueReiterated.VisibleIndex = -1
                            ClqxseeDetail.VisibleIndex = 5
                            ClqxValueGlosa.VisibleIndex = -1
                            ClqxConcept.VisibleIndex = -1
                            ClqxResponsible.VisibleIndex = -1
                            ClqxComment.VisibleIndex = -1
                            ClqxsaveMovementGlosa.VisibleIndex = -1
                            ClqxMoreColumn.VisibleIndex = 6
                            ClqxAcciones.VisibleIndex = 7

                            'columnas del itemreposity de visualizacion de mas informacion 
                            INDClMainGlosa.VisibleIndex = 0
                            INDClConcepto.VisibleIndex = 1
                            INDClValueGlosa.VisibleIndex = 2
                            INDclValueAcceptedFirstInstance.VisibleIndex = 3
                            INDClValueReiteration.VisibleIndex = -1
                            INdclValueAcceptedSecondInstance.VisibleIndex = -1
                        End If

                    ElseIf ObjetoD.State = "1" Then
                        Me.BarraBotones.StatusRecord = "1"
                        If ObjetoD.DocumentType = "1" Then
                            Me.BarraBotones.RibbonPageProcesos.Visible = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Actualizar) = True
                            INDBtnGuardarComment.Enabled = True
                            ClValueAcceptedFirtsInstance.VisibleIndex = -1
                            ClValueReiterated.VisibleIndex = -1
                            ClValorEntidad.VisibleIndex = 5
                            ValueGlosado.VisibleIndex = 6
                            ClseeDetail.VisibleIndex = 7
                            ClValueGlosa.VisibleIndex = 8
                            ClConcept.VisibleIndex = 9
                            ClResponsible.VisibleIndex = 10
                            ClComment.VisibleIndex = 11
                            ClsaveMovementGlosa.VisibleIndex = 12
                            ClMoreColumn.VisibleIndex = 13
                            Reg.VisibleIndex = 14

                            ClqxServiceCodeName.VisibleIndex = 0
                            ClqxAmmount.VisibleIndex = 1
                            ClqxInvoicedValue.VisibleIndex = 2
                            ClqxUnitValue.VisibleIndex = 3
                            ClqxValorGlosado.VisibleIndex = 4
                            ClqxValueAcceptedFirtsInstance.VisibleIndex = -1
                            ClqxValueReiterated.VisibleIndex = -1
                            ClqxseeDetail.VisibleIndex = 5
                            ClqxValueGlosa.VisibleIndex = 6
                            ClqxConcept.VisibleIndex = 7
                            ClqxResponsible.VisibleIndex = 8
                            ClqxComment.VisibleIndex = 9
                            ClqxsaveMovementGlosa.VisibleIndex = 10
                            ClqxMoreColumn.VisibleIndex = 11
                            ClqxAcciones.VisibleIndex = 12

                            'columnas del itemreposity de visualizacion de mas informacion 
                            INDClMainGlosa.VisibleIndex = 0
                            INDClConcepto.VisibleIndex = 1
                            INDClValueGlosa.VisibleIndex = 2
                            INDclValueAcceptedFirstInstance.VisibleIndex = 3
                            INDClValueReiteration.VisibleIndex = -1
                            INdclValueAcceptedSecondInstance.VisibleIndex = -1

                        ElseIf ObjetoD.DocumentType = "2" Then
                            ClValueAcceptedFirtsInstance.VisibleIndex = -1
                            ClValueReiterated.VisibleIndex = -1
                            ClValorEntidad.VisibleIndex = 5
                            ValueGlosado.VisibleIndex = 6
                            ClseeDetail.VisibleIndex = 7
                            ClValueGlosa.VisibleIndex = -1
                            ClConcept.VisibleIndex = -1
                            ClResponsible.VisibleIndex = -1
                            ClComment.VisibleIndex = -1
                            ClsaveMovementGlosa.VisibleIndex = -1
                            ClMoreColumn.VisibleIndex = 8
                            Reg.VisibleIndex = 9

                            Me.BarraBotones.RibbonPageProcesos.Visible = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Actualizar) = True

                            ClqxServiceCodeName.VisibleIndex = 0
                            ClqxAmmount.VisibleIndex = 1
                            ClqxInvoicedValue.VisibleIndex = 2
                            ClqxUnitValue.VisibleIndex = -1
                            ClqxValorGlosado.VisibleIndex = 3
                            ClqxValueAcceptedFirtsInstance.VisibleIndex = 4
                            ClqxValueReiterated.VisibleIndex = 5
                            ClqxseeDetail.VisibleIndex = 6
                            ClqxValueGlosa.VisibleIndex = -1
                            ClqxConcept.VisibleIndex = -1
                            ClqxResponsible.VisibleIndex = -1
                            ClqxComment.VisibleIndex = -1
                            ClqxsaveMovementGlosa.VisibleIndex = -1

                            'columnas del itemreposity de visualizacion de mas informacion 
                            INDClMainGlosa.VisibleIndex = 0
                            INDClConcepto.VisibleIndex = 1
                            INDClValueGlosa.VisibleIndex = 2
                            INDclValueAcceptedFirstInstance.VisibleIndex = 3
                            INDClValueReiteration.VisibleIndex = 4
                            INdclValueAcceptedSecondInstance.VisibleIndex = 5

                        End If
                    End If
                    INDlyiInvoiceDetailGroupNavigate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyiInoices.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyiDatosGenerales.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyiFacturas.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    CtrTraceabilityControl.Visible = True
                    Me.AdditionalControlPanel.Visible = True
                    CtrTraceabilityControl.Invoice = ObjetoD.GlosaPortfolioGlosada.InvoiceNumber
                    If ObjetoD.DocumentType.ToString() = "1" Then
                        CtrTraceabilityControl.Process = GlosasProcess.Objection
                        INDGeneralObjectionBtn.Text = "Objeción general"
                        INDtxtTotalCobradoEPSorReiteration.Text = "Total Cobrado EPS"
                    ElseIf ObjetoD.DocumentType.ToString() = "2" Then
                        CtrTraceabilityControl.Process = GlosasProcess.Reiteration
                        INDGeneralObjectionBtn.Text = "Reiteración general"
                        INDtxtTotalCobradoEPSorReiteration.Text = "Vr. reiterado"
                    End If
                    CtrTraceabilityControl.Entity = Objeto.CustomerId
                    CtrTraceabilityControl.CargarControl()
                    INDInvoiceNumberLbl.Text = ObjetoD.GlosaPortfolioGlosada.InvoiceNumber
                    INDIngressNumberLbl.Text = ObjetoD.GlosaPortfolioGlosada.IngressNumber
                    INDPacientLbl.Text = ObjetoD.GlosaPortfolioGlosada.PatientName
                    LblCodeContract.Text = ObjetoD.GlosaPortfolioGlosada.ContractCode

                    Dim valor As Decimal
                    valor = ObjetoD.GlosaPortfolioGlosada.InvoiceValueEntity
                    INDTotalCobradoEPSLbl.Text = ManageDecimalsFun(valor)
                    INDtxtValuePacient.Text = ManageDecimalsFun(ObjetoD.GlosaPortfolioGlosada.InvoiceValuePacient)
                    CalcularValueGlosado()

                    If Haveqx() Then
                        Me.INDgcvInvoiceDetailGrid.ExpandMasterRow(0)
                        PositionFocusQX(0, Me.ClqxValueGlosa)
                    Else
                        Me.INDgcvInvoiceDetailGrid.Focus()
                        Me.INDgcvInvoiceDetailGrid.FocusedRowHandle = 0
                        Me.INDgcvInvoiceDetailGrid.FocusedColumn = Me.ClValueGlosa
                        Me.INDgcvInvoiceDetailGrid.ShowEditor()
                    End If
                End If

            End If
        End If
        ExpandDetailQx()
    End Sub

    Private Sub ExpandDetailQx()
        If Me.INDgcvInvoiceDetailGrid.DataSource IsNot Nothing Then
            For i As Integer = 0 To Me.INDgcvInvoiceDetailGrid.DataSource.count - 1
                Me.INDgcvInvoiceDetailGrid.ExpandMasterRow(i, "GlosaInvoiceDetailQX")
            Next
        End If
    End Sub

    ''' <summary>
    ''' Metodo para habilitar solo procesos
    ''' </summary>
    ''' <param name="Value">if set to <c>true</c> [value].</param>
    Private Sub EnabledProcess(ByVal Value As Boolean)
        BarraBotones.RibbonPagEform.Visible = Value
        If Value = True Then
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = False
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = False
        Else
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        End If
    End Sub

    ''' <summary>
    ''' Funcion que agrega un item a la rejilla de detalle de recepcion
    ''' </summary>
    ''' <param name="itemInvoice"></param>
    ''' <remarks></remarks>
    Private Async Sub ItemAddRejilla(ByVal itemInvoice As SP_invoiceList_Result)
        'lista temporal que guardar las facturas agregadas para despues eliminarlas de la rejilla de facturas del PopUp
        Dim tmpListaSP_invoiceList_ResultBorrar As List(Of SP_invoiceList_Result) = New List(Of SP_invoiceList_Result)
        Dim AccountValidate As Boolean
        Dim _interfaceId? As Integer
        If ValidateStateinvoice(itemInvoice) = False Then
            Exit Sub
        End If
        If Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
            If INDglCompany.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(SeleccioneUnaEmpresa, RecepcionObjeciones)
            End If

            'validamos que la cuenta este configurada en los parametros de interfaz
            Dim ObjInterfaceParameter As GlosasParametersInterface = CType(INDglCompany.GetSelectedDataRow, GlosasParametersInterface)
            If ObjInterfaceParameter.AccountingMethod = eTypeInterface.FoxPrivate Then
                AccountValidate = Model.ValidateAccountTableFOxPrivate(itemInvoice.AccountantAccountCustomers, True)
            ElseIf ObjInterfaceParameter.AccountingMethod = eTypeInterface.NETPrivate Then
                AccountValidate = Model.ValidateAccountTableNEtPrivate(itemInvoice.AccountantAccountCustomers, True)
            Else
                AccountValidate = True ' para FOX no aplica esta validacion 
            End If
            _interfaceId = ObjInterfaceParameter.Id

            'validacion de facturas a traslado a cobro juridico. aplica solo para el version DGH NET sectro publico 
            If ObjInterfaceParameter.AccountingMethod = eTypeInterface.NETPublic Then
                If itemInvoice.TraslateJuridical IsNot Nothing AndAlso itemInvoice.TraslateJuridical > 0 Then
                    Mensaje(EeventViewerImages.Informacion) = "La factura " & itemInvoice.InvoiceNumber & " ya esta en proceso juridico"
                    Exit Sub
                End If
            End If
        Else
            _interfaceId = Nothing
            AccountValidate = True
        End If
        'declaro variable  tipo detalle factura
        Dim _TmpObjectionsReceptionD As New GlosaObjectionsReceptionD

        'si no existe la cuenta en configuracion 
        If AccountValidate = False Then
            Dim str As String = obtenerRecurso(NoExisteCuentaFacturaRadicadaConfigurada, Eform.ParametersInterfaz) & " " & itemInvoice.InvoiceNumber & " - " & itemInvoice.AccountantAccountCustomers
            LIstMessage.Add(str)
        Else
            'Se valida el estado de la GlosaPortfolioGlosada por el bug de Pitalito 2589 y se deja igual a como se valida en los servicios, 
            'siempre y cuando el registro que ya exista tenga estado diferente a anulado
            If itemInvoice.StatePortfolioGlosada <> 11 AndAlso itemInvoice.StatePortfolioGlosada <> 12 AndAlso itemInvoice.StateObjectionReceptionC <> 4 Then
                LIstMessage.Add(itemInvoice.InvoiceNumber.ToString + " Factura Radicada  en el oficio " + itemInvoice.RadicatedConsecutive.ToString + " Se Encuentra en otro proceso")
                Exit Sub
            End If

            If Me.CustomerTmp.Id > 0 AndAlso Me.INDbteNit.EditValue IsNot Nothing Then
                CustomerTmp = Await Model.GetCustomerById(Me.INDbteNit.EditValue)
            End If
            With _TmpObjectionsReceptionD
                .InvoiceNumber = itemInvoice.InvoiceNumber.Trim
                .ObservationInvoiceCode = itemInvoice.StateCurrentInvoice.Trim
                .GlosasParametersInterfaceId = _interfaceId
                .GlosaPortfolioGlosada = New GlosaPortfolioGlosada()
                With .GlosaPortfolioGlosada
                    .InvoiceNumber = itemInvoice.InvoiceNumber.Trim
                    .BalanceInvoice = itemInvoice.BalanceInvoice.ToString.Trim
                    .InvoiceValueEntity = itemInvoice.InvoiceValueEntity.ToString.Trim
                    .InvoiceValuePacient = itemInvoice.InvoiceValuePacient.ToString.Trim
                    .AccountantAccountCustomers = itemInvoice.AccountantAccountCustomers.Trim
                    .State = 1 'se envia como pendiente confirmado
                    .PortfolioAge = itemInvoice.PortfolioAge
                    .InvoiceDate = itemInvoice.InvoiceDate
                    If itemInvoice.RadicatedNumber IsNot Nothing Then
                        .RadicatedNumber = itemInvoice.RadicatedNumber.Trim
                    End If
                    .RadicatedDate = itemInvoice.RadicatedDate
                    .PatientCode = itemInvoice.PatientCode
                    .PatientName = itemInvoice.PatientName
                    .IngressNumber = itemInvoice.IngressNumber
                    .IngressDate = itemInvoice.IngressDate
                    .UserNameInvoice = itemInvoice.UserNameInvoice
                    .ContractCode = itemInvoice.ContractCode
                    .ContractName = itemInvoice.ContractName
                    .Nit = CustomerTmp.Nit.Replace(" ", String.Empty)
                    .PlanCode = itemInvoice.CodePlan
                    .ValueGlosado = 0
                    .ValueAcceptedFirstInstance = 0
                    .ValueReiterated = 0
                    .ValueReiterationBalance = 0
                    .ValueAcceptedSecondInstance = 0
                    .ValueAcceptedIPSconciliation = 0
                    .ValueAcceptedEAPBconciliation = 0
                    .ValuePayments = 0
                    .BalanceGlosa = 0
                    .LegalTransferValue = 0
                    .BalanceLegal = 0
                    If itemInvoice.OpeningBalance Is Nothing Then
                        .OpeningBalance = False
                    Else
                        .OpeningBalance = itemInvoice.OpeningBalance
                    End If
                End With
                ' si la factura esta dentro de un oficio ya confirmado procedo agregarla como reiteracion
                If itemInvoice.StateObjectionReceptionC = 2 Then
                    .DocumentType = 2  'reiterado
                    .GlosaPortfolioGlosada.State = 4 '  4 -pendiente confirmar reiteracion
                    .Invalidate = False
                ElseIf itemInvoice.StateObjectionReceptionC = 4 Then
                    .DocumentType = 1 'glosado
                    .Invalidate = True
                Else
                    .DocumentType = 1 'glosado
                    .Invalidate = False
                End If
                .State = 1 'glosa anulada
            End With
            'valido que la factura agregar no se encuentre ya en la lista
            Dim found As GlosaObjectionsReceptionD = Nothing
            found = Me.DataSourceObjectsInvoices.Find(Function(value As GlosaObjectionsReceptionD) value.InvoiceNumber = itemInvoice.InvoiceNumber)
            'si no se encuentra la factura procedo agregarla a la lista temporal, la agrego al objeto y a la lista
            If found Is Nothing Then
                tmpListaSP_invoiceList_ResultBorrar.Add(itemInvoice)
                DataSourceObjectsInvoices.Add(_TmpObjectionsReceptionD)
            End If
            Me.INDbteNit.Enabled = False
        End If
        Me.INDgcObjetions.RefreshDataSource()
    End Sub


    ''' <summary>
    ''' Funcion que retorna una lista de detalle de factura seleccionada
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InvoiceDetailSelection() As List(Of GlosaInvoiceDetail)
        SumValorEntidad = 0
        'facturas seleccionadas
        Dim ListInvoiceDetailSelection As New List(Of GlosaInvoiceDetail)
        For Each c In Me.INDgcvInvoiceDetailGrid.GetSelectedRows
            If c > -1 Then
                Dim InvoiceDetail As GlosaInvoiceDetail = TryCast(INDgcvInvoiceDetailGrid.GetRow(c), GlosaInvoiceDetail)
                If InvoiceDetail IsNot Nothing Then
                    SumValorEntidad += InvoiceDetail.InvoicedValue
                    ListInvoiceDetailSelection.Add(InvoiceDetail)
                End If
            End If
        Next
        Return ListInvoiceDetailSelection
    End Function

    ''' <summary>
    ''' Funcion que retorna una lista de detalle qx de factura seleccionada
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InvoiceDetailqxSelection() As List(Of GlosaInvoiceDetailQX)
        SumValorEntidad = 0
        Dim mainView1 As GridView = INDgcvInvoiceDetailGrid
        Dim detailView2 As GridView = INDgcInvoiceDetailGrid.FocusedView ' TryCast(mainView1.GetDetailView(mainView1.FocusedRowHandle, mainView1.GetRelationIndex(mainView1.FocusedRowHandle, "GlosaInvoiceDetailQX")), GridView)
        Dim AuxInvoiceDetail As GlosaInvoiceDetail = INDgcvInvoiceDetailGrid.GetFocusedRow
        'facturas seleccionadas
        Dim ListInvoiceDetailqxSelection As New List(Of GlosaInvoiceDetailQX)
        If detailView2 IsNot Nothing Then
            For Each c In detailView2.GetSelectedRows
                If c > -1 Then
                    Dim InvoiceDetailqx As GlosaInvoiceDetailQX = TryCast(detailView2.GetRow(c), GlosaInvoiceDetailQX)
                    If InvoiceDetailqx IsNot Nothing Then
                        SumValorEntidad += InvoiceDetailqx.InvoicedValue
                        ListInvoiceDetailqxSelection.Add(InvoiceDetailqx)
                    End If
                End If
            Next
        End If
        Return ListInvoiceDetailqxSelection
    End Function

    ''' <summary>
    ''' Retorna el mensaje de estado de una factura
    ''' </summary>
    ''' <param name="ObservationInvoiceCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function StateErp(ObservationInvoiceCode) As String
        Dim StrMensaje As String = String.Empty
        Select Case ObservationInvoiceCode
            Case "1"
                StrMensaje = obtenerRecurso(FacturaSinRadicar, RecepcionObjeciones)
            Case "T"
                StrMensaje = obtenerRecurso(FacturaRadicadaSinConfirmar, RecepcionObjeciones)
            Case "6"
                StrMensaje = obtenerRecurso(FacturaAnulada, RecepcionObjeciones)
            Case Else
                StrMensaje = String.Format(obtenerRecurso(EstadoInvalido, RecepcionObjeciones), ObservationInvoiceCode)
        End Select
        Return StrMensaje
    End Function


    ''' <summary>
    ''' Crea Un Nuevo Frontal de registro de glosa
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OpenFrmRegisterObjection(ByVal GeneralGlosa As Boolean, ByVal Glosaqx As Boolean)
        Using _frmRegisterObjection As New FrmRegisterObjection
            _frmRegisterObjection._ManageDecimals = _parameterGlosas.ManageDecimals
            _frmRegisterObjection.StatusObjectionD = ObjetoD.State.AsByte
            'si es una reiteracion
            If ObjetoD.DocumentType = "2" Then
                Dim ObjDetail As GlosaInvoiceDetail = CType(INDgcvInvoiceDetailGrid.GetRow(INDgcvInvoiceDetailGrid.FocusedRowHandle), GlosaInvoiceDetail)
                Dim BalanceReiterated As Decimal = 0
                Dim balanceAceptedFirsStancia As Decimal
                For Each itemMov As GlosaMovementGlosa In ObjDetail.GlosaMovementGlosa
                    balanceAceptedFirsStancia += IIf(itemMov.ValueAcceptedFirstInstance IsNot Nothing AndAlso itemMov.ValueAcceptedFirstInstance > 0, itemMov.ValueAcceptedFirstInstance, 0)
                Next
                _frmRegisterObjection.Reiteration = True
            Else
                _frmRegisterObjection.Reiteration = False
            End If
            'control de bloqueo
            If Me.FlagBlockRecord = True Then
                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                _frmRegisterObjection.Actionstaskbar = False
            Else
                If ObjetoD.State = "1" Then
                    _frmRegisterObjection.Actionstaskbar = True
                Else
                    _frmRegisterObjection.Actionstaskbar = False
                End If
            End If

            'restamoes el valor ya pagado por el paciente
            Dim ObjD As GlosaObjectionsReceptionD = CType(INDgcvObjetions.GetRow(INDgcvObjetions.FocusedRowHandle), GlosaObjectionsReceptionD)
            If ObjD IsNot Nothing AndAlso ObjD.GlosaPortfolioGlosada IsNot Nothing AndAlso ObjD.GlosaPortfolioGlosada.InvoiceValuePacient > 0 Then
                SumValorEntidad = SumValorEntidad - ObjD.GlosaPortfolioGlosada.InvoiceValuePacient
            End If


            'glosa general
            If GeneralGlosa = True And Glosaqx = False Then
                _frmRegisterObjection.GeneralGlosa = True
                _frmRegisterObjection.GeneralGlosaSelection = False
                _frmRegisterObjection.InvoiceValue = FormatCurrency(balance, 2)
                _frmRegisterObjection.BalanceInvoice = FormatCurrency(balance, 2)
                _frmRegisterObjection.SumValueGlosadoTotal = sumValueGLosadoTotal
                _frmRegisterObjection.LytCValor.Text = "Saldo Disponible"
                _frmRegisterObjection.InvoiceNumber = INDgcvObjetions.GetRowCellValue(INDgcvObjetions.FocusedRowHandle, "GlosaPortfolioGlosada.InvoiceNumber").ToString
                _frmRegisterObjection.DescripTionService = "Glosa General"
                Dim frmTrans As New FrmTransparent(_frmRegisterObjection, False)
                frmTrans.ShowDialog(Me)
                'glosa detalle
            ElseIf GeneralGlosa = False And Glosaqx = False Then
                Dim ListInvoiceDetailSelection As List(Of GlosaInvoiceDetail) = InvoiceDetailSelection()
                If ListInvoiceDetailSelection.Count > 1 Then
                    _frmRegisterObjection.GeneralGlosa = True
                    _frmRegisterObjection.GeneralGlosaSelection = True
                    _frmRegisterObjection.ListInvoiceDetail = ListInvoiceDetailSelection
                    '_frmRegisterObjection.InvoiceValue = SumValorEntidad
                    If SumValorEntidad > FormatCurrency(balance, 2) Then 'si la sumatoria es mayor al saldo, debo tomar el valor del saldo de factura, si no, tomamos la sumatoria del recorrido
                        _frmRegisterObjection.InvoiceValue = FormatCurrency(balance, 2)
                        _frmRegisterObjection.LytCValor.Text = "Saldo Disponible"
                    Else
                        _frmRegisterObjection.InvoiceValue = SumValorEntidad
                        _frmRegisterObjection.LytCValor.Text = "Valor Seleccionado"
                    End If
                    _frmRegisterObjection.BalanceInvoice = FormatCurrency(balance, 2)
                    _frmRegisterObjection.SumValueGlosadoTotal = sumValueGLosadoTotal
                    _frmRegisterObjection.InvoiceDetaildId = INDgcvInvoiceDetailGrid.GetRowCellValue(INDgcvInvoiceDetailGrid.FocusedRowHandle, "Id").ToString()
                    _frmRegisterObjection.InvoiceNumber = INDgcvObjetions.GetRowCellValue(INDgcvObjetions.FocusedRowHandle, "GlosaPortfolioGlosada.InvoiceNumber").ToString
                    _frmRegisterObjection.DescripTionService = "Glosa por selección multiple"
                    Dim frmTrans As New FrmTransparent(_frmRegisterObjection, False)
                    frmTrans.ShowDialog(Me)
                Else
                    _frmRegisterObjection.GeneralGlosa = False
                    _frmRegisterObjection.GeneralGlosaSelection = False
                    _frmRegisterObjection.InvoiceDetaildId = INDgcvInvoiceDetailGrid.GetRowCellValue(INDgcvInvoiceDetailGrid.FocusedRowHandle, "Id").ToString()
                    _frmRegisterObjection.InvoiceNumber = INDgcvInvoiceDetailGrid.GetRowCellValue(INDgcvInvoiceDetailGrid.FocusedRowHandle, "InvoiceNumber").ToString()
                    '_frmRegisterObjection.InvoiceValue = INDgcvInvoiceDetailGrid.GetRowCellValue(INDgcvInvoiceDetailGrid.FocusedRowHandle, "InvoicedValue").ToString()
                    Dim valor As Decimal = INDgcvInvoiceDetailGrid.GetRowCellValue(INDgcvInvoiceDetailGrid.FocusedRowHandle, "ValorEntidad").ToString()
                    If valor = 0 Then valor = INDgcvInvoiceDetailGrid.GetRowCellValue(INDgcvInvoiceDetailGrid.FocusedRowHandle, "InvoicedValue").ToString()
                    _frmRegisterObjection.InvoiceValue = valor
                    _frmRegisterObjection.BalanceInvoice = FormatCurrency(balance, 2)
                    _frmRegisterObjection.SumValueGlosadoTotal = sumValueGLosadoTotal
                    _frmRegisterObjection.LytCValor.Text = "Valor Detalle"
                    _frmRegisterObjection.DescripTionService = INDgcvInvoiceDetailGrid.GetRowCellValue(INDgcvInvoiceDetailGrid.FocusedRowHandle, "ServiceCodeName").ToString
                    Dim frmTrans As New FrmTransparent(_frmRegisterObjection, False)
                    frmTrans.ShowDialog(Me)
                End If

                'glosa detalle qx
            ElseIf GeneralGlosa = False And Glosaqx = True Then
                Dim ListInvoiceDetailSelectionqx As List(Of GlosaInvoiceDetailQX) = InvoiceDetailqxSelection()
                If ListInvoiceDetailSelectionqx.Count > 1 Then
                    _frmRegisterObjection.GeneralGlosa = True
                    _frmRegisterObjection.GeneralGlosaSelection = True
                    _frmRegisterObjection.GlosaSelectionqx = True
                    _frmRegisterObjection.ListInvoiceDetailqx = ListInvoiceDetailSelectionqx
                    If SumValorEntidad > FormatCurrency(balance, 2) Then 'si la sumatoria es mayor al saldo, debo tomar el valor del saldo de factura, si no, tomamos la sumatoria del recorrido
                        _frmRegisterObjection.InvoiceValue = FormatCurrency(balance, 2)
                        _frmRegisterObjection.LytCValor.Text = "Saldo Disponible"
                    Else
                        _frmRegisterObjection.InvoiceValue = SumValorEntidad
                        _frmRegisterObjection.LytCValor.Text = "Valor Seleccionado"
                    End If
                    _frmRegisterObjection.BalanceInvoice = FormatCurrency(balance, 2)
                    _frmRegisterObjection.SumValueGlosadoTotal = sumValueGLosadoTotal
                    _frmRegisterObjection.InvoiceNumber = INDgcvObjetions.GetRowCellValue(INDgcvObjetions.FocusedRowHandle, "GlosaPortfolioGlosada.InvoiceNumber").ToString

                    '   Dim mainView2 As GridView = INDgcvInvoiceDetailGrid
                    Dim detailViewQx As GridView = INDgcInvoiceDetailGrid.FocusedView  'TryCast(mainView2.GetDetailView(mainView2.FocusedRowHandle, mainView2.GetRelationIndex(mainView2.FocusedRowHandle, "GlosaInvoiceDetailQX")), GridView)
                    If detailViewQx IsNot Nothing Then
                        If detailViewQx.FocusedRowHandle >= 0 Then
                            Dim Objqx As GlosaInvoiceDetailQX = CType(detailViewQx.GetRow(detailViewQx.FocusedRowHandle), GlosaInvoiceDetailQX)
                            _frmRegisterObjection.InvoiceDetaildId = Objqx.InvoiceDetailId
                            _frmRegisterObjection.InvoiceDetaildQXId = detailViewQx.GetRowCellValue(detailViewQx.FocusedRowHandle, "Id").ToString()
                            _frmRegisterObjection.DescripTionService = "Glosa por selección multiple"
                            Dim frmTrans As New FrmTransparent(_frmRegisterObjection, False)
                            frmTrans.ShowDialog(Me)
                        End If
                    End If
                Else
                    _frmRegisterObjection.GeneralGlosa = False
                    _frmRegisterObjection.GeneralGlosaSelection = False
                    _frmRegisterObjection.GlosaSelectionqx = False
                    'Dim mainView2 As GridView = INDgcvInvoiceDetailGrid
                    Dim detailViewQx As GridView = INDgcInvoiceDetailGrid.FocusedView  ' TryCast(mainView2.GetDetailView(mainView2.FocusedRowHandle, mainView2.GetRelationIndex(mainView2.FocusedRowHandle, "GlosaInvoiceDetailQX")), GridView)
                    If detailViewQx IsNot Nothing Then
                        If detailViewQx.FocusedRowHandle >= 0 Then
                            Dim Objqx As GlosaInvoiceDetailQX = Nothing
                            If detailViewQx.IsMasterRow(detailViewQx.FocusedRowHandle) = True Then
                                MessageIndigo.Show(obtenerRecurso(GloseDetalleQX, RecepcionObjeciones), MessageType.Warning, Me.Text)
                                Exit Sub
                            Else
                                Objqx = CType(detailViewQx.GetRow(detailViewQx.FocusedRowHandle), GlosaInvoiceDetailQX)
                            End If
                            If Objqx IsNot Nothing Then
                                _frmRegisterObjection.InvoiceDetaildId = Objqx.InvoiceDetailId
                                _frmRegisterObjection.InvoiceDetaildQXId = detailViewQx.GetRowCellValue(detailViewQx.FocusedRowHandle, "Id").ToString()
                                _frmRegisterObjection.InvoiceNumber = INDgcvObjetions.GetRowCellValue(INDgcvObjetions.FocusedRowHandle, "GlosaPortfolioGlosada.InvoiceNumber").ToString
                                _frmRegisterObjection.InvoiceValue = detailViewQx.GetRowCellValue(detailViewQx.FocusedRowHandle, "InvoicedValue").ToString()
                                _frmRegisterObjection.BalanceInvoice = FormatCurrency(balance, 2)
                                _frmRegisterObjection.SumValueGlosadoTotal = sumValueGLosadoTotal
                                _frmRegisterObjection.LytCValor.Text = "Valor Detalle "
                                _frmRegisterObjection.DescripTionService = detailViewQx.GetRowCellValue(detailViewQx.FocusedRowHandle, "ServiceCodeName").ToString
                                Dim frmTrans As New FrmTransparent(_frmRegisterObjection, False)
                                frmTrans.ShowDialog(Me)
                            End If
                        End If
                    End If
                End If
            End If

            'Dim frmTrans As New FrmTransparent(_frmRegisterObjection, False)
            'frmTrans.ShowDialog()
        End Using
        UpdateInvoiceDetail(ObjetoD)
        ' Me.INDgcvInvoiceDetailGrid.ExpandMasterRow(0)
    End Sub


    ''' <summary>
    '''metodo asincrono para Actualizar el detalle de la factura 
    ''' </summary>
    Private Async Sub UpdateInvoiceDetail(ByVal ObjetoD As GlosaObjectionsReceptionD)
        Try
            If ObjetoD.DocumentType = "2" Then
                AsyncLoader(True)
                Dim resultlist As ActionResult(Of List(Of GlosaInvoiceDetail))
                resultlist = Await Model.ListGlosaInvoiceDetailByInvoiceNumberReiteration(InvoiceNumbertmp, "RE")

                Dim resultlistNoNormative As List(Of GlosaInvoiceDetail)
                resultlistNoNormative = (Await Model.ListGlosaInvoiceDetailByInvoiceNumberReiterationNoNormative(InvoiceNumbertmp, "RE")).ObjectEmbbeded

                INDGcNoNormative.DataSource = resultlistNoNormative
                INDgcInvoiceDetailGrid.DataSource = resultlist.ObjectEmbbeded

                If Me.HasPermission(140) AndAlso (resultlistNoNormative IsNot Nothing OrElse resultlistNoNormative.Any()) Then
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.RadicateResponse) = False
                End If

                AsyncLoader(False)
            Else
                AsyncLoader(True)
                INDgcInvoiceDetailGrid.DataSource = Await Model.ListGlosaInvoiceDetailByInvoiceNumber(InvoiceNumbertmp, "RA")
                AsyncLoader(False)
            End If
            If ObjetoD.State = "2" Then
                INDBtnGuardarComment.Enabled = False
            Else
                INDBtnGuardarComment.Enabled = True
            End If
            CalcularValueGlosado()
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' metodo Calcular el valor glosado
    ''' </summary>
    Private Sub CalcularValueGlosado()
        Dim sumValueGlosado As Decimal
        Dim sumValueReiterate As Decimal
        sumValueGLosadoTotal = 0
        For Each item As GlosaInvoiceDetail In INDgcInvoiceDetailGrid.DataSource
            'sumo el total glosado
            For Each itemMovementGlosa As GlosaMovementGlosa In item.GlosaMovementGlosa
                If itemMovementGlosa.MainGlosa = True Then
                    sumValueGlosado = sumValueGlosado + itemMovementGlosa.ValueGlosado
                    sumValueReiterate = sumValueReiterate + IIf(itemMovementGlosa.ValueReiterated Is Nothing, 0, itemMovementGlosa.ValueReiterated)
                End If
            Next
        Next
        INDTotalGlosadoLbl.Text = ManageDecimalsFun(sumValueGlosado)
        sumValueGLosadoTotal = sumValueGLosadoTotal + sumValueGlosado
        If ObjetoD.DocumentType = "2" Then
            INDTotalCobradoEPSLbl.Text = ManageDecimalsFun(sumValueReiterate)
        End If
        Me.INDgcvInvoiceDetailGrid.FocusedRowHandle = 0
        Me.INDgcvInvoiceDetailGrid.ExpandMasterRow(0)
    End Sub

    ''' <summary>
    ''' Metodo para cambiar el icono de la primer columna de la rejilla para mostrar el icono de cargando cuando esta guardadon cada registro
    ''' </summary>
    ''' <param name="Stade">The stade.</param>
    ''' <param name="Row">The row.</param>
    Public Sub ChangeStade(Stade As Stades, Row As Integer) Implements IObjectionsReception.ChangeStade
        If DataSourceObjectsInvoices IsNot Nothing Then
            If Row <= DataSourceObjectsInvoices.Count - 1 Then
                Select Case Stade
                    Case Stades.Procesando
                        INDgcvObjetions.SetRowCellValue(Row, "Image", My.Resources.Cargando20x20)
                    Case Else
                        INDgcvObjetions.SetRowCellValue(Row, "Image", Nothing)
                End Select
            End If
        End If
    End Sub

    ''' <summary>
    ''' Confirma una Factura
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ConfirmInvoice(isConfirmMassive As Boolean) As Task(Of ActionResult)
        Try
            Me.DataSourceObjectsInvoices = Await Model.ListReceptionsObjectionD(Objeto.Id)

            If ObjetoD.DocumentType = "2" Then
                Dim resultlist As ActionResult(Of List(Of GlosaInvoiceDetail))
                resultlist = Await Model.ListGlosaInvoiceDetailByInvoiceNumberReiteration(InvoiceNumbertmp, "RE")
                INDgcInvoiceDetailGrid.DataSource = resultlist.ObjectEmbbeded
            Else
                INDgcInvoiceDetailGrid.DataSource = Await Model.ListGlosaInvoiceDetailByInvoiceNumber(InvoiceNumbertmp, "RA") 'actualizo lista
            End If
            Dim result As ActionResult

            If Me._sequense.PortfolioSequenceDetail.Count = 0 Then
                Return New ActionResult With {.StateResult = False, .MessageResult = {"No ha sido parametrizada la secuencia de radicación de cuentas"}.ToList()}
            End If

            If Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.Native Or Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegractionFox Or Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNet Or Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNATIVE_MIGRATIONS Then
                _idCurrentSequense = Me._sequense.PortfolioSequenceDetail(0).Id
            Else
                _idCurrentSequense = 0
            End If
            If ObjetoD.DocumentType = "1" Then
                Dim controlValueGlosado As Boolean = False
                Dim sumValueGlosado As Decimal
                For Each item As GlosaInvoiceDetail In INDgcInvoiceDetailGrid.DataSource
                    'sumo el total glosado
                    For Each itemMovementGlosa As GlosaMovementGlosa In item.GlosaMovementGlosa
                        If itemMovementGlosa.MainGlosa = True Then
                            sumValueGlosado = sumValueGlosado + itemMovementGlosa.ValueGlosado
                        End If
                    Next
                Next
                If sumValueGlosado > FormatCurrency(balance, 2) Then
                    Return New ActionResult With {.StateResult = False, .MessageResult = {String.Format(obtenerRecurso(ValorGlosadoMayorSaldo, RecepcionObjeciones), sumValueGlosado.ToString("C2", indigo.Culture), balance.ToString("C2", indigo.Culture))}.ToList()}
                End If
                sumValueGlosado = FormatCurrency(sumValueGlosado, 2)
                If sumValueGlosado > 0 Then
                    controlValueGlosado = True
                Else
                    controlValueGlosado = False
                End If

                Dim numberInvoice = DataSourceObjectsInvoices.FirstOrDefault(Function(x) x.InvoiceNumber = ObjetoD.InvoiceNumber)

                If numberInvoice.GlosaPortfolioGlosada.ValueGlosado <> sumValueGlosado Then
                    Return New ActionResult With {.StateResult = False, .MessageResult = {"La suma de los detalles no conincide con el valor glosado"}.ToList()}
                End If

                If controlValueGlosado = True Then
                    If isConfirmMassive OrElse MessageIndigo.Show(String.Format(obtenerRecurso(ComunesConfirmarFactura), InvoiceNumbertmp, sumValueGlosado.ToString("C2", indigo.Culture)), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                        Dim objD As GlosaObjectionsReceptionD = Nothing
                        objD = Me.DataSourceObjectsInvoices.Find(Function(value As GlosaObjectionsReceptionD) value.InvoiceNumber = InvoiceNumbertmp)
                        result = Await Model.ConfirmObjectionReceptionD(objD.Id, _idCurrentSequense, objD.GlosaObjectionsReceptionC.Customer.Nit, objD.GlosaObjectionsReceptionC.RadicatedConsecutive, sumValueGlosado)
                        ClosePopUpInvoiceDetail()
                        Return result
                    End If
                Else
                    Return New ActionResult With {.StateResult = False, .MessageResult = {obtenerRecurso(ConfirmarFacturaSinValorGlosado, RecepcionObjeciones)}.ToList()}
                End If
            ElseIf ObjetoD.DocumentType = "2" Then
                Dim controlValueReiterado As Boolean = False
                Dim sumValueReiterado As Decimal
                For Each item As GlosaInvoiceDetail In INDgcInvoiceDetailGrid.DataSource
                    'sumo el total reiterado
                    For Each itemMovementGlosa As GlosaMovementGlosa In item.GlosaMovementGlosa
                        If itemMovementGlosa.MainGlosa = True Then
                            If itemMovementGlosa.ValueReiterated IsNot Nothing Then
                                sumValueReiterado = sumValueReiterado + itemMovementGlosa.ValueReiterated
                            End If
                        End If
                    Next
                Next

                If sumValueReiterado > FormatCurrency(balance, 2) Then
                    Return New ActionResult With {.StateResult = False, .MessageResult = {String.Format(obtenerRecurso(ValorGlosadoMayorSaldo, RecepcionObjeciones), sumValueReiterado.ToString("C2", indigo.Culture), balance.ToString("C2", indigo.Culture))}.ToList()}
                End If
                sumValueReiterado = FormatCurrency(sumValueReiterado, 2)
                If sumValueReiterado > 0 Then
                    controlValueReiterado = True
                Else
                    controlValueReiterado = False
                End If
                If controlValueReiterado = True Then
                    If isConfirmMassive OrElse MessageIndigo.Show(String.Format(ResourceManager.GetString("FrmReceptionObjections_ConfirmInvoiceReiterated", "Glosas"), InvoiceNumbertmp, sumValueReiterado.ToString("C2", indigo.Culture)), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                        Dim objD As GlosaObjectionsReceptionD = Nothing
                        objD = Me.DataSourceObjectsInvoices.Find(Function(value As GlosaObjectionsReceptionD) value.InvoiceNumber = InvoiceNumbertmp)
                        result = Await Model.ConfirmObjectionReceptionD(objD.Id, _idCurrentSequense, objD.GlosaObjectionsReceptionC.Customer.Nit, objD.GlosaObjectionsReceptionC.RadicatedConsecutive, sumValueReiterado)
                        ClosePopUpInvoiceDetail()
                        Return result
                    End If
                Else
                    Return New ActionResult With {.StateResult = False, .MessageResult = {ResourceManager.GetString("FrmReceptionObjections_ValueReiteratedCero", "Glosas")}.ToList()}
                End If
            End If

            Return New ActionResult With {.StateResult = False, .MessageResult = {"No se realizó ningún proceso"}.ToList()}
        Catch ex As Exception
            Return New ActionResult With {.StateResult = False, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList()}
        End Try
    End Function

    ''' <summary>
    ''' Metodo para glosar un item
    ''' </summary>
    Private Sub GlosarItem()
        OpenFrmRegisterObjection(False, False)
    End Sub

    Private Sub GlosarItemqx()
        OpenFrmRegisterObjection(False, True)
    End Sub

    ''' <summary>
    ''' Funcion para cargar Formulario de ingreso de comentario de reiteracion multiples
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReiterationMultiple()
        If MessageIndigo.Show(obtenerRecurso(ConfirmarReitereacionMasiva, RecepcionObjeciones), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim frmPopUp As New FrmCommentReiteration()
            Using tras As New FrmTransparent(frmPopUp, False)
                If tras.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                    If _reiterationQx = True Then
                        ReiterarSeleccionqx(frmPopUp.CommentReiteration)
                    Else
                        ReiterarSeleccion(frmPopUp.CommentReiteration)
                    End If
                End If
            End Using
        End If
    End Sub
    ''' <summary>
    ''' Guarda una reiteración a nivel General
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub GeneralReiteration()
        Dim frmPopUp As New FrmCommentReiteration()
        Using tras As New FrmTransparent(frmPopUp, False)
            If tras.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                'por cada detalle repartimos valores
                Dim ListMov = ListGlosaMovementGlosa(Me.INDgcInvoiceDetailGrid.DataSource, frmPopUp.CommentReiteration)
                AsyncLoader(False)
                Dim Result = Await SaveReiterationMovementGlosa(ListMov)
                AsyncLoader(False)

                If Result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
                    UpdateInvoiceDetail(ObjetoD) 'actualizo lista
                Else
                    If Result.MessageResult IsNot Nothing AndAlso Result.MessageResult(0) = "-999" Then
                        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(Eresources.ComunesErrorConcurrencia)
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                    End If
                End If
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Retorna una lista de GlosaMovementGLosa apartir de una lista de GlosaInvoiceDetail
    ''' </summary>
    ''' <param name="_listGlosaInvoiceDetail"></param>
    ''' <param name="CommentReiteration"></param>
    ''' <returns></returns>
    Private Function ListGlosaMovementGlosa(_listGlosaInvoiceDetail As List(Of GlosaInvoiceDetail), CommentReiteration As String) As List(Of GlosaMovementGlosa)
        Dim ListMov As New List(Of GlosaMovementGlosa)

        Dim lockMe As Object = New Object()
        Parallel.ForEach(_listGlosaInvoiceDetail, Sub(item)
                                                      If item.GlosaMovementGlosa.Count > 0 Then
                                                          Dim saldo As Decimal = item.GlosaMovementGlosa.Sum(Function(cc As GlosaMovementGlosa) cc.ValuePendingConciliation)
                                                          For Each itemMov As GlosaMovementGlosa In item.GlosaMovementGlosa
                                                              If itemMov.MainGlosa = True Then
                                                                  If itemMov.ValuePendingConciliation <> 0 Then ' si hay saldo pendiente
                                                                      itemMov.ValueReiterated = itemMov.ValuePendingConciliation
                                                                      itemMov.ResponsibleReiterationId = itemMov.ResponsibleId
                                                                      itemMov.RationaleDateReiteration = Date.Now
                                                                      itemMov.State = 3 'Pendiente Evaluar Reiteracion  
                                                                      itemMov.RationaleReiteration = CommentReiteration
                                                                      SyncLock lockMe
                                                                          ListMov.Add(itemMov)
                                                                      End SyncLock
                                                                  End If
                                                              End If
                                                          Next
                                                      End If
                                                  End Sub)
        For Each item As GlosaInvoiceDetail In _listGlosaInvoiceDetail

        Next
        Return ListMov
    End Function

    ''' <summary>
    ''' Guardar una lista de GlosaMovementGlosa en la base de datos
    ''' </summary>
    ''' <param name="ListGlosaMovementGlosa"></param>
    ''' <returns></returns>
    Private Async Function SaveReiterationMovementGlosa(ListGlosaMovementGlosa As List(Of GlosaMovementGlosa)) As Task(Of ActionResult)
        Dim result As New ActionResult
        Try
            If ListGlosaMovementGlosa Is Nothing OrElse ListGlosaMovementGlosa.Count = 0 Then
                With result
                    .StateResult = False
                    .Message = "La entidad a guardar esta Vacia"
                End With
                Return result
            End If
            result = Await Model.SaveReiterationMovementGlosa(ListGlosaMovementGlosa)
            Return result
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Function


    ''' <summary>
    ''' Metodo para la realización de Reiteraciones Masiva sobre detalle de factura
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub ReiterarSeleccion(ByVal _comment As String)
        If Me.FlagBlockRecord = True Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
            Exit Sub
        End If
        Dim ListMov As New List(Of GlosaMovementGlosa)
        Dim ListInvoiceDetailSelection As List(Of GlosaInvoiceDetail) = InvoiceDetailSelection()
        For Each item In ListInvoiceDetailSelection
            If item.GlosaMovementGlosa.Count > 0 Then
                'validamos el valor a reiterar
                'para controlar los valores que ya han sido aceptado por la IPS, ya que en el tramite se pueden aceptar en una glosa NO principal
                'Dim valueAcceptFirtsInstance As Decimal = item.GlosaMovementGlosa.Sum(Function(c As GlosaMovementGlosa) c.ValueAcceptedFirstInstance)
                'Dim valueGlosado As Decimal = item.GlosaMovementGlosa.Where(Function(c As GlosaMovementGlosa) c.MainGlosa = True).Sum(Function(c As GlosaMovementGlosa) c.ValueGlosado)
                'Dim saldo As Decimal = valueGlosado - valueAcceptFirtsInstance
                Dim saldo As Decimal = item.GlosaMovementGlosa.Sum(Function(cc As GlosaMovementGlosa) cc.ValuePendingConciliation)

                For Each itemMov As GlosaMovementGlosa In item.GlosaMovementGlosa
                    If itemMov.MainGlosa = True Then
                        If itemMov.ValuePendingConciliation <> 0 Then ' si hay saldo pendiente
                            'If saldo > itemMov.ValuePendingConciliation Then 'para controlar los valores que ya han sido aceptado por la IPS, ya que en el tramite se pueden aceptar en una glosa NO principal
                            '    itemMov.ValueReiterated = itemMov.ValuePendingConciliation
                            '    saldo = saldo - itemMov.ValuePendingConciliation
                            'Else
                            '    itemMov.ValueReiterated = saldo
                            '    saldo = 0
                            'End If
                            itemMov.ValueReiterated = itemMov.ValuePendingConciliation
                            '  itemMov.ValueReiterated = saldo - itemMov.ValueGlosado - IIf(itemMov.ValueAcceptedFirstInstance Is Nothing, 0, itemMov.ValueAcceptedFirstInstance)
                            itemMov.ResponsibleReiterationId = itemMov.ResponsibleId
                            itemMov.RationaleDateReiteration = Date.Now
                            itemMov.State = 3 'Pendiente Evaluar Reiteracion  
                            itemMov.RationaleReiteration = _comment
                            ListMov.Add(itemMov)
                        End If
                    End If
                Next
            End If
        Next
        Dim result As New ActionResult
        Try
            If ListMov.Count > 0 Then
                AsyncLoader(True)
                result = Await Model.SaveReiterationMovementGlosa(ListMov)
                AsyncLoader(False)
                If result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
                    UpdateInvoiceDetail(ObjetoD) 'actualizo lista
                Else
                    If result.MessageResult IsNot Nothing AndAlso result.MessageResult(0) = "-999" Then
                        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(Eresources.ComunesErrorConcurrencia)
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                    End If
                End If
            End If
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Metodo para la realización de Reiteraciones Masiva sobre detalle qx
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub ReiterarSeleccionqx(ByVal _comment As String)
        If Me.FlagBlockRecord = True Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
            Exit Sub
        End If
        Dim ListMov As New List(Of GlosaMovementGlosa)
        Dim ListInvoiceDetailSelection As List(Of GlosaInvoiceDetailQX) = InvoiceDetailqxSelection()
        For Each item In ListInvoiceDetailSelection
            If item.GlosaMovementGlosa.Count > 0 Then
                'validamos el valor a reiterar
                'para controlar los valores que ya han sido aceptado por la IPS, ya que en el tramite se pueden aceptar en una glosa NO principal
                '  Dim valueAcceptFirtsInstance As Decimal = item.GlosaMovementGlosa.Sum(Function(c As GlosaMovementGlosa) c.ValueAcceptedFirstInstance)
                '  Dim valueGlosado As Decimal = item.GlosaMovementGlosa.Where(Function(c As GlosaMovementGlosa) c.MainGlosa = True).Sum(Function(c As GlosaMovementGlosa) c.ValueGlosado)
                ' Dim saldo As Decimal = valueGlosado - valueAcceptFirtsInstance
                Dim saldo As Decimal = item.GlosaMovementGlosa.Sum(Function(cc As GlosaMovementGlosa) cc.ValuePendingConciliation)
                For Each itemMov As GlosaMovementGlosa In item.GlosaMovementGlosa
                    If itemMov.MainGlosa = True Then
                        If itemMov.ValuePendingConciliation <> 0 Then ' si hay saldo pendiente
                            ' itemMov.ValueReiterated = itemMov.ValueGlosado - IIf(itemMov.ValueAcceptedFirstInstance Is Nothing, 0, itemMov.ValueAcceptedFirstInstance)
                            'If saldo > itemMov.ValuePendingConciliation Then 'para controlar los valores que ya han sido aceptado por la IPS, ya que en el tramite se pueden aceptar en una glosa NO principal
                            '    itemMov.ValueReiterated = itemMov.ValuePendingConciliation
                            '    saldo = saldo - itemMov.ValuePendingConciliation
                            'Else
                            '    itemMov.ValueReiterated = saldo
                            '    saldo = 0
                            'End If
                            itemMov.ValueReiterated = itemMov.ValuePendingConciliation
                            itemMov.ResponsibleReiterationId = itemMov.ResponsibleId
                            itemMov.RationaleDateReiteration = Date.Now
                            itemMov.State = 3 'Pendiente Evaluar Reiteracion  
                            itemMov.RationaleReiteration = _comment
                            ListMov.Add(itemMov)
                        End If
                    End If
                Next
            End If
        Next

        Dim result As New ActionResult
        Try
            If ListMov.Count > 0 Then
                AsyncLoader(True)
                result = Await Model.SaveReiterationMovementGlosa(ListMov)
                AsyncLoader(False)
                If result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
                    UpdateInvoiceDetail(ObjetoD) 'actualizo lista
                Else
                    If result.MessageResult IsNot Nothing AndAlso result.MessageResult(0) = "-999" Then
                        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(Eresources.ComunesErrorConcurrencia)
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                    End If
                End If
            End If
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Metodo para hacacer visible la rejilla con el detalle de la factura
    ''' </summary>
    Private Sub VerDetalle()
        ShowPopUpInvoiceDatail()
    End Sub

    ''' <summary>
    ''' Pega en la rejilla de facturas los datos de la clipboard
    ''' </summary>
    Private Async Sub PasteToGridInvoices()
        Try
            If Me.INDbteNit.EditValue IsNot Nothing AndAlso Objeto IsNot Nothing Then
                If Objeto.State = 1 Or Objeto.State Is Nothing Then
                    Dim list As List(Of String) = Me.GetInvoiceFromClipboard()
                    If list.Count > 0 Then
                        Dim _InterfaceId? As Integer
                        Dim _result As New ActionResult(Of List(Of GlosaObjectionsReceptionD))
                        If Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
                            If INDglCompany.EditValue IsNot Nothing Then
                                Dim ObjCompany As Domain.Entities.GlosasParametersInterface = CType(INDglCompany.GetSelectedDataRow, Domain.Entities.GlosasParametersInterface)
                                If ObjCompany IsNot Nothing Then
                                    _InterfaceId = ObjCompany.Id
                                    Me.AsyncLoader(True)
                                    _result = Await Model.ValidateListInvoiceSp(list, Me.INDbteNit.EditValue, ObjCompany.ContainerName)
                                    Me.AsyncLoader(False)
                                End If
                            Else
                                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(SeleccioneUnaEmpresa, RecepcionObjeciones)
                            End If
                        Else
                            _InterfaceId = Nothing
                            Me.AsyncLoader(True)
                            _result = Await Model.ValidateListInvoiceSp(list, Me.INDbteNit.EditValue, String.Empty)
                            Me.AsyncLoader(False)
                        End If
                        Dim strMensaje As String = String.Empty
                        If _result.MessageResult IsNot Nothing AndAlso _result.MessageResult.Count > 0 Then
                            For Each itemMensaje As String In _result.MessageResult
                                strMensaje += itemMensaje + Environment.NewLine
                            Next
                            Mensaje(EeventViewerImages.Informacion) = strMensaje
                        End If
                        Dim listInvoice = _result.ObjectEmbbeded
                        If listInvoice IsNot Nothing AndAlso listInvoice.Count > 0 Then
                            For Each item In listInvoice
                                item.GlosasParametersInterfaceId = _InterfaceId
                                Me.DataSourceObjectsInvoices.Add(item)
                            Next
                        End If
                        Me.INDgcObjetions.RefreshDataSource()
                        If Me.DataSourceObjectsInvoices.Count > 0 Then
                            Me.INDbteNit.Enabled = False
                        End If
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NoExisteInformacionAPegar, RecepcionObjeciones)
                    End If
                End If
            Else
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(FaltaNitTercero, RecepcionObjeciones)
            End If
        Catch ex As Exception
            Me.AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Valida una cadena de caracteres descartando que tenga caracteres especiales
    ''' </summary>
    ''' <param name="str">Cadena a validar</param>
    ''' <returns>Valor que indica si la cadena es valida</returns>
    Private Function IsValidString(ByVal str As String) As Boolean
        Dim pattern As String = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ0123456789-_ "
        For Each c As Char In str
            If Not pattern.Contains(c) Then
                Return False
            End If
        Next
        Return True
    End Function

    ''' <summary>
    ''' Obtiene una lista de numeros de facturas
    ''' almacenada en la Clipboard
    ''' </summary>
    ''' <returns>Lista de numeros de facturas</returns>
    Private Function GetInvoiceFromClipboard() As List(Of String)
        Dim list As New List(Of String)()
        If indigo.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
            Dim ObjCompany As Domain.Entities.GlosasParametersInterface = CType(INDglCompany.GetSelectedDataRow, Domain.Entities.GlosasParametersInterface)
            If ObjCompany Is Nothing Then
                Return list
            End If
            If Clipboard.GetText IsNot Nothing AndAlso Not Clipboard.GetText.Trim().Equals(String.Empty) Then
                For Each line As String In Clipboard.GetText.Split(vbNewLine)
                    Dim item() As String = line.Trim.Split(vbTab)
                    If item.Length >= 1 Then
                        If Me.DataSourceObjectsInvoices Is Nothing Then
                            Me.DataSourceObjectsInvoices = New List(Of GlosaObjectionsReceptionD)
                        End If
                        If Me.IsValidString(item(0).Trim()) Then
                            If Not item(0).Trim().Equals(String.Empty) AndAlso Not (From i As String In list Where i.Trim() = item(0).Trim() Select i).Any AndAlso Not (From f As Domain.Entities.GlosaObjectionsReceptionD In Me.DataSourceObjectsInvoices Where f.InvoiceNumber.Contains(item(0).Trim()) Select f).Any Then
                                Dim invoiceNumberFixed As String = Utils.FixInvoiceNumber(item(0).Trim(), ObjCompany.AccountingMethod)
                                list.Add(invoiceNumberFixed)
                            End If
                        End If
                    Else
                        Exit For
                    End If
                Next
            End If
        Else
            If Clipboard.GetText IsNot Nothing AndAlso Not Clipboard.GetText.Trim().Equals(String.Empty) Then
                For Each line As String In Clipboard.GetText.Split(vbNewLine)
                    Dim item() As String = line.Trim.Split(vbTab)
                    If item.Length >= 1 Then
                        If Me.DataSourceObjectsInvoices Is Nothing Then
                            Me.DataSourceObjectsInvoices = New List(Of GlosaObjectionsReceptionD)
                        End If
                        If Me.IsValidString(item(0).Trim()) Then
                            If Not item(0).Trim().Equals(String.Empty) AndAlso Not (From i As String In list Where i.Trim() = item(0).Trim() Select i).Any AndAlso Not (From f As Domain.Entities.GlosaObjectionsReceptionD In Me.DataSourceObjectsInvoices Where f.InvoiceNumber.Contains(item(0).Trim()) Select f).Any Then
                                list.Add(item(0).Trim())
                            End If
                        End If
                    Else
                        Exit For
                    End If
                Next
            End If
        End If
        Return list
    End Function

    ''' <summary>
    ''' metodo para confirmar una factura sin necesidad ver el detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub ConfirmarDetalle()
        Try
            AsyncLoader(True)
            Dim messages As New StringBuilder()
            Dim errors As New StringBuilder()

            For Each item As Integer In INDgcvObjetions.GetSelectedRows()
                If item > -1 Then
                    ObjetoD = TryCast(INDgcvObjetions.GetRow(item), GlosaObjectionsReceptionD)
                    If ObjetoD IsNot Nothing AndAlso ObjetoD.Id > 0 Then
                        If ObjetoD.State <> "1" Then
                            errors.AppendLine(String.Format("La recepción de la factura {0} ya fue confirmada.", InvoiceNumbertmp))
                            Continue For
                        End If

                        INDMemoGeneralComment.Text = ObjetoD.Comment
                        InvoiceNumbertmp = ObjetoD.InvoiceNumber.ToString()

                        If InvoiceNumber IsNot Nothing Then
                            balance = Await Model.LoadBalanceInvoice(ObjetoD)
                            Dim result = Await ConfirmInvoice(True)
                            If result.StateResult = True Then
                                messages.AppendLine(String.Format("La recepción de la factura {0} fue confirmada.", InvoiceNumbertmp))
                                If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                                    For Each itemResult As String In result.MessageResult
                                        messages.AppendLine("    " & itemResult)
                                    Next
                                End If
                            Else
                                errors.AppendLine(String.Format("La recepción de la factura {0} no fue confirmada.", InvoiceNumbertmp))
                                If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                                    If result.MessageResult(0) = "-999" Then
                                        errors.AppendLine("    " & obtenerRecurso(Eresources.ComunesErrorConcurrencia))
                                    Else
                                        For Each itemResult As String In result.MessageResult
                                            errors.AppendLine("    " & itemResult)
                                        Next
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            Next

            If messages.Length > 0 Then
                Mensaje(EeventViewerImages.Informacion) = messages.ToString()
            End If
            If errors.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        Finally
            AsyncLoader(False)
        End Try
    End Sub
#End Region

#Region "Eventos Barra Botones"
    ''' <summary>
    ''' Barras the botones_ click confirmar.
    ''' </summary>
    Private Async Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        'si esta visible el detalle de la fatura entonces confirmo solo factura
        If INDlyiInvoiceDetailGroupNavigate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            AsyncLoader(True)

            Dim messages As New StringBuilder()
            Dim errors As New StringBuilder()

            Dim result = Await ConfirmInvoice(False)
            If result.StateResult = True Then
                messages.AppendLine(String.Format("La recepción de la factura {0} fue confirmada.", InvoiceNumbertmp))
                If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                    For Each itemResult As String In result.MessageResult
                        messages.AppendLine("    " & itemResult)
                    Next
                End If
            Else
                errors.AppendLine(String.Format("La recepción de la factura {0} no fue confirmada.", InvoiceNumbertmp))
                If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                    If result.MessageResult(0) = "-999" Then
                        errors.AppendLine("    " & obtenerRecurso(Eresources.ComunesErrorConcurrencia))
                    Else
                        For Each itemResult As String In result.MessageResult
                            errors.AppendLine("    " & itemResult)
                        Next
                    End If
                End If
            End If

            If messages.Length > 0 Then
                Mensaje(EeventViewerImages.Informacion) = messages.ToString()
            End If
            If errors.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            End If

            AsyncLoader(False)
        Else
            InvalidateOrConfirm(True)
            Me.BarraBotones.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click anular.
    ''' </summary>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        InvalidateOrConfirm(False)
    End Sub

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        BarraBotones.RibbonPageProcesos.Visible = False
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        Buscar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        SearchMode = False
        Deshacer()
        'Me.CleanControls ()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        'Eliminar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        CleanControls()
        LoadControls()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click customizar.
    ''' </summary>
    Private Sub BarraBotones_ClickCustomizar() Handles BarraBotones.ClickCustomizar
        CustomizationOpen()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ clic restablecer layout.
    ''' </summary>
    Private Sub BarraBotones_ClicRestablecerLayout() Handles BarraBotones.ClicRestablecerLayout
        ResetLayout()
    End Sub
    ''' <summary>
    ''' Barras the botones_ click radicacion respuesta.
    ''' </summary>
    Private Sub BarraBotones_ClickRadicateResponse() Handles BarraBotones.Click_RadicateResponse
        RadicateResponse()
    End Sub

    ''' <summary>
    ''' Exportar datos a Excel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_GenerarArchivo() Handles BarraBotones.Click_GenerateFile
        Try
            AsyncLoader(True)
            Dim INDdtExportFull As DataTable = Model.ReceptionExcelExportFull(Objeto.Id)
            Me.INDgcExportExcelFull.DataSource = INDdtExportFull
            If Me.INDgcExportExcelFull.DataSource IsNot Nothing Then
                If INDgvExportExcelFull IsNot Nothing AndAlso INDgvExportExcelFull.VisibleColumns.Count > 0 Then
                    Dim fileName As String = System.IO.Path.GetTempFileName() & ".xlsx"
                    Dim param As New DevExpress.XtraPrinting.XlsxExportOptions(DevExpress.XtraPrinting.TextExportMode.Value, True, False)
                    INDgvExportExcelFull.HorzScrollVisibility = ScrollVisibility.Always
                    INDgvExportExcelFull.OptionsView.ColumnAutoWidth = False
                    INDgvExportExcelFull.BestFitColumns()
                    INDgvExportExcelFull.ExportToXlsx(fileName, param)
                    If System.IO.File.Exists(fileName) Then
                        System.Diagnostics.Process.Start(fileName)
                    End If
                End If
            End If
            AsyncLoader(False)
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = ex.Message.ToString()
        End Try
    End Sub
#End Region

#Region "Customizar"
    ''' <summary>
    ''' Metodo para abrir el formulario de customizar el frontal
    ''' </summary>
    Private Sub CustomizationOpen()
        INDlyOBjectionsReception.ShowCustomizationForm()
    End Sub

    ''' <summary>
    ''' Metodo para restablecer las definiciones del formulario gridLookUpEdit y Regillas
    ''' </summary>
    Private Sub ResetLayout()
        If My.Computer.FileSystem.DirectoryExists(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
            My.Computer.FileSystem.DeleteDirectory(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name), Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
            INDlyOBjectionsReception.RestoreDefaultLayout()
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesLayoutRestablecido)
        End If
    End Sub

#End Region

#Region "Metodos y Funciones Logica Guardar"

    ''' <summary>
    ''' Radicación respuesta
    ''' </summary>
    Public Sub RadicateResponse()
        Using formulario As New FrmRadicateResponse()

            formulario.DateNow = Date.Now()
            formulario.DateDocument = Objeto.DocumentDate
            formulario.State = Objeto.State
            If Objeto?.RadicateResponse IsNot Nothing OrElse objradicateResponse IsNot Nothing Then
                formulario.radicateResponse = If(Objeto?.RadicateResponse Is Nothing, objradicateResponse, Objeto?.RadicateResponse)
            Else
                formulario.DateConfirm = Date.Now()
            End If
            formulario.ToolBar.Visible = False
            formulario.Size = New Size(480, 379)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim result = formulario.ShowDialog(Me)
            If result = System.Windows.Forms.DialogResult.OK Then
                objradicateResponse = formulario.radicateResponse
            End If
        End Using
    End Sub

    Public Async Function ValidateIsNoNormativeInvoices(ObjGlosaObjectionsReceptionD As List(Of GlosaObjectionsReceptionD)) As Task(Of Boolean)
        Dim ListInvoices As List(Of String)
        ListInvoices = ObjGlosaObjectionsReceptionD.Select(Function(x) x.InvoiceNumber).ToList()
        For Each item In ListInvoices
            Dim InvoiceIsNotNormative = (Await Model.ListGlosaInvoiceDetailByInvoiceNumberReiterationNoNormative(item, "RE")).ObjectEmbbeded
            If InvoiceIsNotNormative IsNot Nothing Then
                Return True
            End If
        Next
        Return False
    End Function


    ''' <summary>
    ''' Guarda el registro de objecion desde la rejilla
    ''' </summary>
    Public Async Sub GuardarGlosaMovement(GlosaQx As Boolean)
        If Me.FlagBlockRecord = True Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
            Exit Sub
        End If
        Try
            If GlosaQx Then
                '   Dim mainViewDetail As GridView = INDgcvInvoiceDetailGrid
                Dim detailViewQx As GridView = INDgcInvoiceDetailGrid.FocusedView 'TryCast(mainViewDetail.GetDetailView(mainViewDetail.FocusedRowHandle, mainViewDetail.GetRelationIndex(mainViewDetail.FocusedRowHandle, "GlosaInvoiceDetailQX")), GridView)
                Me._indexFocus = detailViewQx.FocusedRowHandle
            Else
                Me._indexFocus = INDgcvInvoiceDetailGrid.FocusedRowHandle
            End If

            Dim ListMovement As New List(Of GlosaMovementGlosa)
            Dim objGlosa As GlosaMovementGlosa = Nothing
            'reiteracion
            If INDgcvObjetions.GetRowCellValue(INDgcvObjetions.FocusedRowHandle, "DocumentType") = "1" Then
                objGlosa = AssigningValidMovementGlosa(GlosaQx)
            End If

            If Len(Trim(objGlosa.CodeGlosa)) > 0 Then
                ListMovement.Add(objGlosa)
            End If

            If ListMovement.Count > 0 Then
                Dim result As New ActionResult
                If ListMovement.Count > 0 Then
                    AsyncLoader(True)
                    result = Await Model.SaveMovementGlosa(ListMovement)
                    AsyncLoader(False)
                End If
                If result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
                    ' UpdateInvoiceDetail(ObjetoD)
                    If GlosaQx Then

                        'actualizo detalle factura para sumatoria tenga encuenta los moviminetos regstrados a los qx
                        Dim InvoiceDetailUpdate As GlosaInvoiceDetail = Await Model.GetGlosaInvoiceDetail(ListMovement(0).InvoiceDetailId)
                        If InvoiceDetailUpdate.Id > 0 Then
                            'actualizo el objeto detalle de factura
                            Dim listInvoice As List(Of GlosaInvoiceDetail) = Me.INDgcvInvoiceDetailGrid.DataSource
                            'Busco el objeto a eliminar
                            Dim InvoiceDatilRemove = listInvoice.Find(Function(invoice As GlosaInvoiceDetail) invoice.Id = ListMovement(0).InvoiceDetailId)
                            If InvoiceDatilRemove.Id > 0 Then
                                Dim indexRecord = INDgcInvoiceDetailGrid.DataSource.IndexOf(InvoiceDatilRemove)
                                Dim view As ColumnView = Me.INDgcInvoiceDetailGrid.MainView
                                view.BeginUpdate()
                                'remuevo objeto desactualizado
                                listInvoice.Remove(InvoiceDatilRemove)
                                listInvoice.Insert(indexRecord, InvoiceDetailUpdate)
                                view.EndUpdate()
                            End If
                            'Dim mainViewDetail As GridView = INDgcvInvoiceDetailGrid
                            'Dim detailViewqx As GridView = TryCast(mainViewDetail.GetDetailView(mainViewDetail.FocusedRowHandle, mainViewDetail.GetRelationIndex(mainViewDetail.FocusedRowHandle, "GlosaInvoiceDetailQX")), GridView)
                            'detailViewqx.FocusedRowHandle = Me._indexFocus
                            'detailViewqx.FocusedColumn = detailViewqx.Columns(Me.ClValueGlosa.FieldName)
                            'detailViewqx.ShowEditor()
                        End If

                        '  Me.INDgcInvoiceDetailGrid.RefreshDataSource()
                        'actualizo qx para ver los moviminetos registrado a este sobre el popup de mas informacion
                        Dim InvoiceDetailQXUpdate As GlosaInvoiceDetailQX = Await Model.GetGlosaInvoiceDetailQX(ListMovement(0).InvoiceDetailIdQX)
                        If InvoiceDetailQXUpdate.Id > 0 Then
                            'actualizo el objeto detalle de factura
                            'Dim mainViewDetail As GridView = INDgcvInvoiceDetailGrid
                            Dim detailViewqx As GridView = INDgcInvoiceDetailGrid.FocusedView 'TryCast(mainViewDetail.GetDetailView(mainViewDetail.FocusedRowHandle, mainViewDetail.GetRelationIndex(mainViewDetail.FocusedRowHandle, "GlosaInvoiceDetailQX")), GridView)
                            Dim listInvoiceQx As New List(Of GlosaInvoiceDetailQX)

                            If detailViewqx IsNot Nothing Then
                                For Each item As GlosaInvoiceDetailQX In detailViewqx.DataSource
                                    listInvoiceQx.Add(item)
                                Next
                            End If

                            'Busco el objeto a eliminar
                            Dim InvoiceDetailQXRemove = listInvoiceQx.Find(Function(invoiceDetailqx As GlosaInvoiceDetailQX) invoiceDetailqx.Id = ListMovement(0).InvoiceDetailIdQX)
                            ' Dim view As ColumnView = Me.INDgcInvoiceDetailGrid.MainView
                            If InvoiceDetailQXRemove.Id > 0 Then

                                Dim indexRecord = detailViewqx.DataSource.IndexOf(InvoiceDetailQXRemove)
                                Dim view As ColumnView = Me.INDgcInvoiceDetailGrid.MainView
                                view.BeginUpdate()
                                detailViewqx.DataSource.Remove(InvoiceDetailQXRemove)
                                detailViewqx.DataSource.Insert(indexRecord, InvoiceDetailQXUpdate)
                                view.EndUpdate()

                            End If
                            detailViewqx.FocusedRowHandle = Me._indexFocus
                            detailViewqx.FocusedColumn = detailViewqx.Columns(Me.ClValueGlosa.FieldName)
                            detailViewqx.ShowEditor()
                        End If


                    Else
                        'actualizo detalle factura
                        Dim InvoiceDetailUpdate As GlosaInvoiceDetail = Await Model.GetGlosaInvoiceDetail(ListMovement(0).InvoiceDetailId)
                        If InvoiceDetailUpdate.Id > 0 Then

                            'actualizo el objeto detalle de factura
                            Dim listInvoice As List(Of GlosaInvoiceDetail) = Me.INDgcvInvoiceDetailGrid.DataSource
                            'Dim IndexFoco = INDgcvInvoiceDetailGrid.FocusedRowHandle
                            'Busco el objeto a eliminar
                            Dim InvoiceDatilRemove = listInvoice.Find(Function(invoice As GlosaInvoiceDetail) invoice.Id = ListMovement(0).InvoiceDetailId)
                            If InvoiceDatilRemove.Id > 0 Then
                                Dim indexRecord = INDgcInvoiceDetailGrid.DataSource.IndexOf(InvoiceDatilRemove)
                                Dim view As ColumnView = Me.INDgcInvoiceDetailGrid.MainView
                                view.BeginUpdate()
                                'remuevo objeto desactualizado
                                listInvoice.Remove(InvoiceDatilRemove)
                                listInvoice.Insert(indexRecord, InvoiceDetailUpdate)
                                view.EndUpdate()
                            End If
                            Me.INDgcvInvoiceDetailGrid.FocusedRowHandle = _indexFocus
                            Me.INDgcvInvoiceDetailGrid.FocusedColumn = Me.ClValueGlosa
                            Me.INDgcvInvoiceDetailGrid.ShowEditor()

                        End If
                    End If

                    '  UpdateInvoiceDetail(ObjetoD)
                    CalcularValueGlosado()
                Else
                    If result.MessageResult IsNot Nothing AndAlso result.MessageResult(0) = "-999" Then
                        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(Eresources.ComunesErrorConcurrencia)
                    Else
                        For Each itemMensaje As String In result.MessageResult
                            Mensaje(EeventViewerImages.Advertencia) = itemMensaje
                        Next
                        If GlosaQx Then
                            PositionFocusQX(2, ClqxValueGlosa)
                        Else
                            PositionFocus(2, ClValueGlosa)
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = ex.Message.ToString
        End Try
    End Sub

    ''' <summary>
    ''' Establece la posicion del foco en la rejilla de detalles de factura
    ''' </summary>
    ''' <param name="Inicial"></param>
    ''' <param name="tmpColumn"></param>
    ''' <remarks></remarks>
    Private Sub PositionFocus(ByVal Inicial As Integer, ByVal tmpColumn As DevExpress.XtraGrid.Columns.GridColumn)
        If Inicial = 0 Then
            Me.INDgcvInvoiceDetailGrid.FocusedRowHandle = 0
        ElseIf Inicial = 1 Then
            Me.INDgcvInvoiceDetailGrid.FocusedRowHandle = INDgcvInvoiceDetailGrid.FocusedRowHandle + 1
        ElseIf Inicial = 2 Then
            Me.INDgcvInvoiceDetailGrid.FocusedRowHandle = INDgcvInvoiceDetailGrid.FocusedRowHandle
        End If

        Me.INDgcvInvoiceDetailGrid.FocusedColumn = tmpColumn
        Me.INDgcvInvoiceDetailGrid.ShowEditor()
        'End If
    End Sub

    ''' <summary>
    '''  Establece la posicion del foco en la rejilla de detalles de factura qx
    ''' </summary>
    ''' <param name="Inicial"></param>
    ''' <param name="tmpColumn"></param>
    ''' <remarks></remarks>
    Private Sub PositionFocusQX(ByVal Inicial As Integer, ByVal tmpColumn As DevExpress.XtraGrid.Columns.GridColumn)
        Dim mainViewDetail As GridView = INDgcvInvoiceDetailGrid
        Dim detailViewqx As GridView = INDgcInvoiceDetailGrid.FocusedView 'TryCast(mainViewDetail.GetDetailView(mainViewDetail.FocusedRowHandle, mainViewDetail.GetRelationIndex(mainViewDetail.FocusedRowHandle, "GlosaInvoiceDetailQX")), GridView)
        If detailViewqx IsNot Nothing Then
            Dim tmpFocus As Integer = detailViewqx.FocusedRowHandle
            detailViewqx.Focus()
            If Inicial = 0 Then
                detailViewqx.FocusedRowHandle = 0
            ElseIf Inicial = 1 Then
                detailViewqx.FocusedRowHandle = tmpFocus + 1
            ElseIf Inicial = 2 Then
                detailViewqx.FocusedRowHandle = tmpFocus
            End If
            detailViewqx.FocusedColumn = detailViewqx.Columns(tmpColumn.FieldName) 'detailViewqx.Columns(Me.ClqxValueGlosa.FieldName) 'tmpColumn 'Me.ClqxValueGlosa
            detailViewqx.ShowEditor()
        End If
    End Sub

    ''' <summary>
    ''' Metodo para realizar foco sobre el proximo detalle de factura y posicionarlo en el detalle qx
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GoAnotherExpandMasterQx()
        INDgcvInvoiceDetailGrid.CollapseMasterRow(INDgcvInvoiceDetailGrid.FocusedRowHandle)
        INDgcvInvoiceDetailGrid.FocusedRowHandle = INDgcvInvoiceDetailGrid.FocusedRowHandle + 1
        INDgcvInvoiceDetailGrid.ExpandMasterRow(INDgcvInvoiceDetailGrid.FocusedRowHandle)
        PositionFocusQX(0, Me.ClqxValueGlosa)
    End Sub

    ''' <summary>
    ''' Funcion para Validar el ingreso de un movimineto glosa por la rejilla de detalle de factura 
    ''' </summary>
    ''' <param name="Glosaqx"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AssigningValidMovementGlosa(Glosaqx As Boolean) As GlosaMovementGlosa
        Dim Glosa As GlosaMovementGlosa = Nothing
        If Glosaqx = True Then
            Dim AuxInvoiceDetailQX As GlosaInvoiceDetailQX = Nothing
            '    Dim mainView2 As GridView = INDgcvInvoiceDetailGrid
            Dim detailView3 As GridView = INDgcInvoiceDetailGrid.FocusedView ' TryCast(mainView2.GetDetailView(mainView2.FocusedRowHandle, mainView2.GetRelationIndex(mainView2.FocusedRowHandle, "GlosaInvoiceDetailQX")), GridView)
            If detailView3 IsNot Nothing Then
                If detailView3.FocusedRowHandle > -1 Then
                    AuxInvoiceDetailQX = TryCast(detailView3.GetRow(detailView3.FocusedRowHandle), GlosaInvoiceDetailQX)

                    If AuxInvoiceDetailQX IsNot Nothing AndAlso AuxInvoiceDetailQX.Id > 0 Then
                        If AuxInvoiceDetailQX.MovimientoAux.ValueGlosado > AuxInvoiceDetailQX.InvoicedValue Then
                            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(ValorGlosaMayorValorFactura, Presentation.Base.Eform.RegisterObjection), ManageDecimalsFun(AuxInvoiceDetailQX.InvoicedValue))
                            Return New GlosaMovementGlosa
                        Else
                            Dim listMensaje As New List(Of String)
                            CalcularValueGlosado()

                            'validamos que no supere el valor del saldo 
                            sumValueGLosadoTotal += AuxInvoiceDetailQX.MovimientoAux.ValueGlosado
                            If sumValueGLosadoTotal > balance Then
                                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(ValorGlosadoMayorSaldo, RecepcionObjeciones), ManageDecimalsFun(sumValueGLosadoTotal), ManageDecimalsFun(balance))
                                Return New GlosaMovementGlosa
                            End If

                            AcumulativoValorGlosado = 0
                            'para validar que los movimineto glosa del item, no supere el valor del item
                            If AuxInvoiceDetailQX.GlosaMovementGlosa.Count > 0 Then
                                For Each Item As GlosaMovementGlosa In AuxInvoiceDetailQX.GlosaMovementGlosa
                                    If Item.MainGlosa = True Then
                                        AcumulativoValorGlosado = AcumulativoValorGlosado + Item.ValueGlosado
                                    End If
                                Next
                            Else
                                AcumulativoValorGlosado = 0
                            End If
                            AcumulativoValorGlosado = AcumulativoValorGlosado + AuxInvoiceDetailQX.MovimientoAux.ValueGlosado

                            If AuxInvoiceDetailQX.MovimientoAux.CodeGlosaId > 0 AndAlso AuxInvoiceDetailQX.MovimientoAux.ResponsibleId > 0 AndAlso AuxInvoiceDetailQX.MovimientoAux.ValueGlosado > 0 Then
                                'si la sumatoria de movimienstos supera el valor del item, marcamos la glosa como pricipal= false --- sino como principal = true para que sume como valor glosado
                                'Aqui se debe hacer la lógica para insertar la bandera si es o no normativo, por ahora enviamos en true
                                If AcumulativoValorGlosado <= AuxInvoiceDetailQX.InvoicedValue Then
                                    Glosa = CreateMovementGlosa(AuxInvoiceDetailQX.InvoiceDetailId, AuxInvoiceDetailQX.Id, AuxInvoiceDetailQX.MovimientoAux.ResponsibleId, AuxInvoiceDetailQX.MovimientoAux.ValueGlosado, AuxInvoiceDetailQX.MovimientoAux.RationaleGlosa, True, True)
                                Else
                                    Glosa = CreateMovementGlosa(AuxInvoiceDetailQX.InvoiceDetailId, AuxInvoiceDetailQX.Id, AuxInvoiceDetailQX.MovimientoAux.ResponsibleId, AuxInvoiceDetailQX.MovimientoAux.ValueGlosado, AuxInvoiceDetailQX.MovimientoAux.RationaleGlosa, False, True)
                                End If

                            Else
                                Return New GlosaMovementGlosa
                            End If

                        End If
                    End If
                End If
            End If
            AuxInvoiceDetailQX.MovimientoAux = New GlosaMovementGlosa()
            INDgcvInvoiceDetailQXGrid.RefreshData()
        Else


            Dim AuxInvoiceDetail As GlosaInvoiceDetail = Nothing
            If INDgcvInvoiceDetailGrid.FocusedRowHandle > -1 Then
                AuxInvoiceDetail = TryCast(INDgcvInvoiceDetailGrid.GetRow(INDgcvInvoiceDetailGrid.FocusedRowHandle), GlosaInvoiceDetail)

                CalcularValueGlosado()

                'validamos que no supere el valor del saldo de la factura
                sumValueGLosadoTotal += AuxInvoiceDetail.MovimientoAux.ValueGlosado
                If sumValueGLosadoTotal > balance Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(ValorGlosadoMayorSaldo, RecepcionObjeciones), ManageDecimalsFun(sumValueGLosadoTotal), ManageDecimalsFun(balance))
                    Return New GlosaMovementGlosa
                End If

                AcumulativoValorGlosado = 0
                'para validar que los movimineto glosa del item, no supere el valor del item
                If AuxInvoiceDetail.GlosaMovementGlosa.Count > 0 Then
                    For Each Item As GlosaMovementGlosa In AuxInvoiceDetail.GlosaMovementGlosa
                        If Item.MainGlosa = True Then
                            AcumulativoValorGlosado = AcumulativoValorGlosado + Item.ValueGlosado
                        End If
                    Next
                Else
                    AcumulativoValorGlosado = 0
                End If
                AcumulativoValorGlosado = AcumulativoValorGlosado + AuxInvoiceDetail.MovimientoAux.ValueGlosado

                If AuxInvoiceDetail.ValorEntidad = 0 Then AuxInvoiceDetail.ValorEntidad = AuxInvoiceDetail.InvoicedValue
                'If AuxInvoiceDetail.MovimientoAux.ValueGlosado > AuxInvoiceDetail.InvoicedValue Then
                If AuxInvoiceDetail.MovimientoAux.ValueGlosado > AuxInvoiceDetail.ValorEntidad Then
                    'Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(ValorGlosaMayorValorFactura, Presentation.Base.Eform.RegisterObjection), IIf(AuxInvoiceDetail.InvoicedValue Is Nothing, 0, AuxInvoiceDetail.InvoicedValue.Value.MoneyFormat))
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(ValorGlosaMayorValorFactura, Presentation.Base.Eform.RegisterObjection), ManageDecimalsFun(AuxInvoiceDetail.ValorEntidad))
                    PositionFocus(2, ClValueGlosa)
                    Return New GlosaMovementGlosa
                Else
                    If AuxInvoiceDetail.MovimientoAux.CodeGlosaId > 0 AndAlso AuxInvoiceDetail.MovimientoAux.ResponsibleId > 0 AndAlso AuxInvoiceDetail.MovimientoAux.ValueGlosado > 0 Then

                        'si la sumatoria de movimienstos supera el valor del item, marcamos la glosa como pricipal= false --- sino como principal = true para que sume como valor glosado
                        'If AcumulativoValorGlosado <= AuxInvoiceDetail.InvoicedValue Then
                        'Aqui se debe hacer la lógica para insertar la bandera si es o no normativo, por ahora enviamos en true
                        If AcumulativoValorGlosado <= AuxInvoiceDetail.ValorEntidad Then
                            Glosa = CreateMovementGlosa(AuxInvoiceDetail.Id, String.Empty, AuxInvoiceDetail.MovimientoAux.ResponsibleId, AuxInvoiceDetail.MovimientoAux.ValueGlosado, AuxInvoiceDetail.MovimientoAux.RationaleGlosa, True, True) 'si no supera saldo de factura van como principal
                        Else
                            Glosa = CreateMovementGlosa(AuxInvoiceDetail.Id, String.Empty, AuxInvoiceDetail.MovimientoAux.ResponsibleId, AuxInvoiceDetail.MovimientoAux.ValueGlosado, AuxInvoiceDetail.MovimientoAux.RationaleGlosa, False, True) ' si supera saldo de factura la marcamos como no principal
                        End If

                    Else
                        Return New GlosaMovementGlosa
                    End If

                End If
            End If
            AuxInvoiceDetail.MovimientoAux = New GlosaMovementGlosa()
            INDgcvInvoiceDetailGrid.RefreshData()
        End If
        Return Glosa
    End Function

    ''' <summary>
    ''' Crea Un Objeto Movimieno glosa general
    ''' </summary>
    ''' <returns>retorna un objeto movimineto glosa</returns>
    ''' <remarks></remarks>
    Private Function CreateMovementGlosa(_InvoiceDetailId As String, _InvoiceDetailIdQx As String, _ResponsibleId As String, _valueGlosado As Decimal, RationaleGlosa As String, MainGlosa As Boolean, Isnormative As Boolean) As GlosaMovementGlosa
        Dim _ObjRegisterObjection As New GlosaMovementGlosa
        With _ObjRegisterObjection
            .ValueGlosado = _valueGlosado
            .InvoiceNumber = Me._InvoiceNumbertmp
            .InvoiceDetailId = _InvoiceDetailId
            .MainGlosa = MainGlosa
            .RationaleDateGlosa = Date.Now
            .IsNormative = Isnormative
            If _InvoiceDetailIdQx <> String.Empty Then
                .InvoiceDetailIdQX = _InvoiceDetailIdQx
            End If
            .CodeGlosaId = SpecificConceptId
            .CodeGlosa = SpecificConceptCode
            .RationaleGlosa = RationaleGlosa
            .State = 1 'la envio como estado 1 Pendiente Evaluar Glosa
            .TempState = 1 'la envio como estado 1 Pendiente Evaluar Glosa
            .ResponsibleId = _ResponsibleId
            .TypeConcept = 1
        End With
        Return _ObjRegisterObjection
    End Function

    ''' <summary>
    ''' Funcion para validar si un Detalle de Factura Tiene Qx
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Haveqx() As Boolean
        Dim AuxInvoiceDetauil As GlosaInvoiceDetail = INDgcvInvoiceDetailGrid.GetFocusedRow
        If AuxInvoiceDetauil IsNot Nothing Then
            If AuxInvoiceDetauil.GlosaInvoiceDetailQX.Count > 0 Then
                Return True
            End If
        End If
        Return False
    End Function

    ''' <summary>
    ''' Funcion para validar si un Detalle de Factura Tiene Qx
    ''' </summary>
    ''' <returns></returns> 
    ''' <remarks></remarks>
    Private Function NextHaveqx() As Boolean
        Dim AuxInvoiceDetauil As GlosaInvoiceDetail = INDgcvInvoiceDetailGrid.GetRow(INDgcvInvoiceDetailGrid.FocusedRowHandle + 1)
        If AuxInvoiceDetauil IsNot Nothing Then
            If AuxInvoiceDetauil.GlosaInvoiceDetailQX.Count > 0 Then
                Return True
            End If
        End If
        Return False
    End Function

    ''' <summary>
    ''' Metodo para cargar los campos del advanced Filter
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadAdvancedFilter(Optional ByVal ObjCompany As GlosasParametersInterface = Nothing)
        Dim ListObjectField As New List(Of ObjectField)

        If Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.Integration Then

            If ObjCompany.AccountingMethod = eTypeInterface.FoxPublic Or ObjCompany.AccountingMethod = eTypeInterface.FoxPrivate Then
                ListObjectField.Add(New ObjectField("N° Factura", "car.cemnumfac", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))

                ListObjectField.Add(New ObjectField("Codigo Contrato", "sal.GECCODIGO", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))

                ListObjectField.Add(New ObjectField("Fecha Radicado", "crc.ccrfecrad", eTypes.E_Date, New List(Of CriteriaAdvancedFilter)))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Between, "Entre"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.GreaterThan, "Mayor que"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.GreaterOrEquals, "Mayor o igual"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.LessThan, "Menor que"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.LessOrEquals, "Menor o igual"))

                ListObjectField.Add(New ObjectField("Identificación", "com.gpacodigo", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))

                ListObjectField.Add(New ObjectField("Nombre Paciente", "com.gpanombre", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))

                ListObjectField.Add(New ObjectField("Apellido Paciente", "com.gpaapelli", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))


                ListObjectField.Add(New ObjectField("N° de Radicado", "RC.RadicatedConsecutive", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))


            ElseIf ObjCompany.AccountingMethod = eTypeInterface.NETPrivate Or ObjCompany.AccountingMethod = eTypeInterface.NETPublic Then


                ListObjectField.Add(New ObjectField("N° Factura", "car.cxcdocume", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))

                ListObjectField.Add(New ObjectField("Codigo Contrato", "CON.GDECODIGO", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))

                ListObjectField.Add(New ObjectField("Fecha Radicado", "crm.CRFFECRAD", eTypes.E_Date, New List(Of CriteriaAdvancedFilter)))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Between, "Entre"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.GreaterThan, "Mayor que"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.GreaterOrEquals, "Mayor o igual"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.LessThan, "Menor que"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.LessOrEquals, "Menor o igual"))

                ListObjectField.Add(New ObjectField("Identificación", "pac.pacnumdoc", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))

                ListObjectField.Add(New ObjectField("Nombre Paciente", "pac.pacprinom", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))

                ListObjectField.Add(New ObjectField("Apellido Paciente", "pac.pacpriape", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))


                ListObjectField.Add(New ObjectField("N° de Radicado", "DOC.CDCONSEC", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))
            End If
        ElseIf Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.Native Or Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegractionFox Or Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNet Or Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNATIVE_MIGRATIONS Then

            ListObjectField.Add(New ObjectField("N° Factura", "car.invoicenumber", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))

            ListObjectField.Add(New ObjectField("Codigo Contrato", "contra.code", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))

            ListObjectField.Add(New ObjectField("Fecha Factura", "car.accountreceivabledate", eTypes.E_Date, New List(Of CriteriaAdvancedFilter)))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Between, "Entre"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.GreaterThan, "Mayor que"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.GreaterOrEquals, "Mayor o igual"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.LessThan, "Menor que"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.LessOrEquals, "Menor o igual"))

            ListObjectField.Add(New ObjectField("Identificación", "sal.PatientCode", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))

            ListObjectField.Add(New ObjectField("Nombre Paciente", "pac.IPPRINOMB", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))

            ListObjectField.Add(New ObjectField("Apellido Paciente", "pac.IPPRIAPEL", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))

            ListObjectField.Add(New ObjectField("N° de Radicado", "RC.RadicatedConsecutive", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))

        End If



        CtrAdvancedFilter.ListTopResults.Clear()
        CtrAdvancedFilter.ListTopResults.Add(50)
        CtrAdvancedFilter.ListTopResults.Add(100)
        CtrAdvancedFilter.ListTopResults.Add(150)
        CtrAdvancedFilter.ListTopResults.Add(200)
        CtrAdvancedFilter.ListTopResults.Add("Todos")
        Me.CtrAdvancedFilter.ListFields = ListObjectField
        'removemos en caso de que ya este agregado el escuchador
        RemoveHandler CtrAdvancedFilter.RunSearch, AddressOf RunShearch

        AddHandler CtrAdvancedFilter.RunSearch, AddressOf RunShearch
        AddHandler CtrAdvancedFilter.InvalidValues, AddressOf InvalidFilter
        INDLytRejillaFacturas.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyiSelectAll.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLytAddInvoice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLytFiltrosAvanzada.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDBtndisplayAdvancedSearch.Text = obtenerRecurso(Eresources.OcultarFiltros)
    End Sub

    ''' <summary>
    ''' Metodo que ejecuta la busqueda avanzada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub RunShearch(sender As Object, e As RunSearchEventArgs)
        If Me.INDbteNit.EditValue IsNot Nothing Then
            Dim _actionResult As New ActionResult(Of List(Of SP_invoiceList_Result))
            If Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
                Dim ObjCompany As Domain.Entities.GlosasParametersInterface = CType(INDglCompany.GetSelectedDataRow, Domain.Entities.GlosasParametersInterface)
                If ObjCompany IsNot Nothing Then
                    AsyncLoader(True)
                    _actionResult = Await Model.GetInvoicesAll(ObjCompany.ContainerName, INDbteNit.EditValue, String.Empty, e.QueryString, e.TopResult, "0")
                    AsyncLoader(False)
                Else
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(SeleccioneUnaEmpresa, RecepcionObjeciones)
                End If
            Else
                AsyncLoader(True)
                _actionResult = Await Model.GetInvoicesAll(String.Empty, INDbteNit.EditValue, String.Empty, e.QueryString, e.TopResult, "0")
                AsyncLoader(False)
            End If
            If _actionResult.StateResult = True Then
                If _actionResult.ObjectEmbbeded.Count > 0 Then
                    Me.DataSourceInvoices = _actionResult.ObjectEmbbeded
                    INDLytRejillaFacturas.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyiSelectAll.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDLytAddInvoice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDLytFiltrosAvanzada.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDBtndisplayAdvancedSearch.Text = obtenerRecurso(Eresources.OcultarListaFacturas)
                Else
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NoHayRegistroCriteriosBusqueda, RecepcionObjeciones)
                End If
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(FaltaNitTercero, RecepcionObjeciones)
        End If
    End Sub

    ''' <summary>
    ''' Cick Sobre el boton para oculatr filtros o rejilla en la busqueda avanzada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles INDBtndisplayAdvancedSearch.Click
        If INDLytRejillaFacturas.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            INDLytRejillaFacturas.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyiSelectAll.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLytAddInvoice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLytFiltrosAvanzada.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDBtndisplayAdvancedSearch.Text = obtenerRecurso(Eresources.OcultarFiltros)
        Else
            INDLytRejillaFacturas.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyiSelectAll.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLytAddInvoice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLytFiltrosAvanzada.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDBtndisplayAdvancedSearch.Text = obtenerRecurso(Eresources.OcultarListaFacturas)
        End If
    End Sub
    ''' <summary>
    ''' Metodo En caso de error al crear filtro
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InvalidFilter()
        Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(Eresources.SeleccionesCamposParaFiltros)
    End Sub


    Private Sub BtnSaveMovementGlosa_Click(sender As Object, e As EventArgs) Handles BtnSaveMovementGlosa.Click
        Dim mainView2 As GridView = INDgcvInvoiceDetailGrid
        Dim detailView3 As GridView = INDgcInvoiceDetailGrid.FocusedView 'TryCast(mainView2.GetDetailView(mainView2.FocusedRowHandle, mainView2.GetRelationIndex(mainView2.FocusedRowHandle, "GlosaInvoiceDetailQX")), GridView)
        If detailView3 IsNot Nothing Then
            If detailView3.Name = "INDgcvInvoiceDetailQXGrid" Then
                ' Dim OBJGlosaInvoiceDetail As GlosaInvoiceDetailQX = detailView3.GetFocusedRow
                GuardarGlosaMovement(True)
            Else
                ' Dim OBJGlosaInvoiceDetail As GlosaInvoiceDetail = detailView3.GetFocusedRow
                GuardarGlosaMovement(False)
            End If
        Else
            MessageIndigo.Show(obtenerRecurso(GloseDetalleQX, RecepcionObjeciones), MessageType.Warning, Me.Text)
            'limpiamos datos registrados
            If INDgcvInvoiceDetailGrid.FocusedRowHandle > -1 Then
                Dim AuxInvoiceDetail = TryCast(INDgcvInvoiceDetailGrid.GetRow(INDgcvInvoiceDetailGrid.FocusedRowHandle), GlosaInvoiceDetail)
                AuxInvoiceDetail.MovimientoAux = New GlosaMovementGlosa()
                INDgcvInvoiceDetailGrid.RefreshRow(INDgcvInvoiceDetailGrid.FocusedRowHandle)
            Else
                INDgcvInvoiceDetailGrid.RefreshData()
            End If
            Exit Sub
        End If
    End Sub

    Private Sub INDchkSelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles INDchkSelectAll.CheckedChanged
        If DataSourceInvoices IsNot Nothing AndAlso DataSourceInvoices.Count > 0 Then
            If INDchkSelectAll.EditValue = True Then
                For Each item As SP_invoiceList_Result In DataSourceInvoices
                    item.Selection = True
                Next
            Else
                For Each item As SP_invoiceList_Result In DataSourceInvoices
                    item.Selection = False
                Next
            End If
            INDgcInvoices.RefreshDataSource()
        End If
    End Sub

    ''' <summary>
    ''' Cierra el Popup de carga de detalles de factura
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClosePopUpInvoiceDetail()
        CtrTraceabilityControl.Visible = False
        Me.AdditionalControlPanel.Visible = False
        Dim objectD As GlosaObjectionsReceptionD = TryCast(INDgcvObjetions.GetRow(INDgcvObjetions.FocusedRowHandle), GlosaObjectionsReceptionD) ' GetRowCellValue(INDgcvObjetions.FocusedRowHandle, "GlosaObjectionsReceptionC.State").ToString
        Dim state As String = objectD.GlosaObjectionsReceptionC.State
        EnabledProcess(True)
        INDlyiInvoiceDetailGroupNavigate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyiInoices.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDlyiDatosGenerales.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        If Objeto.Id > 0 Then
            If Objeto.State = 1 Then
                INDlyiFacturas.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
                Me.BarraBotones.PrepareToolbar(eAction.UpdateAndProcess)
                Me.BarraBotones.StatusRecord = "1"
            Else
                INDlyiFacturas.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                Me.BarraBotones.StatusRecord = "2"
            End If
        Else
            INDlyiFacturas.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
        If CompletedPersist = True Then
            UpdateListObjectionReception()
        End If
    End Sub

    ''' <summary>
    ''' Actualiza la lsita de detalle de oficio
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub UpdateListObjectionReception()
        AsyncLoader(True)
        Me.DataSourceObjectsInvoices = Await Model.ListReceptionsObjectionD(Objeto.Id)
        AsyncLoader(False)
    End Sub

    ''' <summary>
    ''' Recargar La Facturas del ERP
    ''' </summary>
    Private Sub CtrUpdateLoad_LoadDataSource()
        If Objeto IsNot Nothing Then
            seleccionarFacturas()
        End If
    End Sub

    Private Async Sub DeleteMasiveDetail()
        Dim lista = New List(Of GlosaObjectionsReceptionD)
        Dim listaEliminar = New List(Of GlosaObjectionsReceptionD)
        Dim listErrors As New StringBuilder

        For Each item As Integer In INDgcvObjetions.GetSelectedRows()
            If item > -1 Then
                Dim data As GlosaObjectionsReceptionD = TryCast(INDgcvObjetions.GetRow(item), Domain.Entities.GlosaObjectionsReceptionD)
                If data.Id > 0 AndAlso data.State = 2 Then 'Si el item ya está guardado con estado confirmado se valida
                    listErrors.AppendLine("La factura " + data.InvoiceNumber + " no se puede eliminar porque está confirmada")
                    Continue For
                End If
                If data IsNot Nothing Then
                    lista.Add(data)
                    listaEliminar.Add(data)
                End If
            End If
        Next

        If listErrors.ToString().Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = listErrors.ToString()
            Exit Sub
        End If

        For Each Data As GlosaObjectionsReceptionD In lista 'limpiamos los iteam que aun no han sidio guardados
            If Data.Id = 0 Then
                If listaEliminar.Contains(Data) Then
                    Me.DataSourceObjectsInvoices.Remove(Data)
                    'ListObjectionsInvoice.Remove(Data)
                    Objeto.GlosaObjectionsReceptionD.Remove(Data)
                    listaEliminar.Remove(Data)
                End If
            End If
        Next
        Me.INDgcObjetions.RefreshDataSource()

        Dim result As ActionResult
        AsyncLoader(True)
        result = Await Model.DeleteListObjectionReceptionD(listaEliminar)
        AsyncLoader(False)
        Me.INDgcObjetions.RefreshDataSource()
        If result.StateResult = True Then
            For Each item As GlosaObjectionsReceptionD In listaEliminar
                Me.DataSourceObjectsInvoices.Remove(item)
                Objeto.GlosaObjectionsReceptionD.Remove(item)
                Me.INDgcObjetions.RefreshDataSource()
            Next
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
        Else
            If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                Dim strmessage As New StringBuilder
                For Each item In result.MessageResult
                    strmessage.AppendLine(item.ToString)
                Next
                Mensaje(EeventViewerImages.Advertencia) = strmessage.ToString
            Else
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesContacteAdministrador, Eform.Comunes)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Elimina Item de la rejilla de detalle de objeciones
    ''' </summary>
    Private Async Sub DeleteItem()
        If Me.FlagBlockRecord = True Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
            Exit Sub
        End If
        If Me.INDgcvObjetions.SelectedRowsCount > 1 Then
            If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistros), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                DeleteMasiveDetail()
            End If
        Else

            Dim obj As GlosaObjectionsReceptionD = Me.INDgcvObjetions.GetRow(Me.INDgcvObjetions.FocusedRowHandle)
            If (obj.Id = 0) Then
                If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    Dim found = Me.DataSourceObjectsInvoices.Find(Function(value As GlosaObjectionsReceptionD) value.InvoiceNumber = obj.InvoiceNumber)
                    Me.DataSourceObjectsInvoices.Remove(found)
                End If
            Else
                If obj.State = 2 Then 'Si la factura ya está confirmada no se puede eliminar
                    Mensaje(EeventViewerImages.Advertencia) = "El item seleccionado no se puede eliminar porque ya está confirmado"
                    Exit Sub
                End If
                DeleteItemDetail(CType(obj, GlosaObjectionsReceptionD))
            End If
            Me.INDgcObjetions.RefreshDataSource()
            If Me.DataSourceObjectsInvoices.Count = 0 Or Me.DataSourceObjectsInvoices.TrueForAll(Function(x) x.ChangeTracker.State = ObjectState.Deleted) Then
                Me.INDbteNit.Enabled = True
            End If
        End If
    End Sub


    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleAccountStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCustomers()
        Using msearch As New MBusqueda
            DatasourceCustomers = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.Customers)
            INDbteNit.Datasource = DatasourceCustomers
        End Using
    End Sub

    Private Async Function INDbteNit_KeyDown(ByVal nit As String) As Task(Of Domain.Entities.Customer)
        CustomerTmp = Await Model.GetCustomerByNit(nit)
        If CustomerTmp IsNot Nothing AndAlso CustomerTmp.Id > 0 Then
            INDdeDocumentDate.Focus()
        Else
            Me.INDbteNit.DisplayNullText = String.Empty
            Me.INDbteNit.EditValue = Nothing
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.ClienteNoExite, Eform.Conciliation)
            INDbteNit.Focus()
        End If
        Return CustomerTmp
    End Function
    ''' <summary>
    ''' Evento que se activa cuando se cambia la unidad operativa.
    ''' </summary>
    ''' <param name="operatingUnit"></param>
    Private Async Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing AndAlso operatingUnit.Id <> Me.OperatingUnitId Then
            Me.OperatingUnitId = operatingUnit.Id
            Await Me.LoadParameters()
        End If
    End Sub

    ''' <summary>
    ''' Logica para cargar los controles dependiendo si maneja o no decimales.
    ''' </summary>
    ''' <returns></returns>
    Private Async Function LoadParameters() As Task
        Using model As New MTimeParameters(Me.Tag)
            Me._parameterGlosas = Await model.GetTimeParameters("0", Me.OperatingUnitId)
            If Me._parameterGlosas Is Nothing OrElse Me._parameterGlosas.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No se encontró parámetros de glosas para la unidad operativa seleccionada"
                Exit Function
            ElseIf Me._parameterGlosas.ManageDecimals = False Then 'No maneja decimales.
                'Grid cabecera
                For Each item In INDgcvObjetions.Columns
                    If item.DisplayFormat.FormatString = "C2" OrElse item.DisplayFormat.FormatString = "c2" Then
                        item.DisplayFormat.FormatString = "C0"
                        item.SummaryItem.DisplayFormat = "Total: {0:c0}"
                    End If
                Next
                'Grid Detalle 
                For Each item In INDgcvInvoiceDetailGrid.Columns
                    If item.DisplayFormat.FormatString = "C2" OrElse item.DisplayFormat.FormatString = "c2" Then
                        item.DisplayFormat.FormatString = "C0"
                        item.SummaryItem.DisplayFormat = "Total: {0:c0}"
                    End If
                Next
                For Each item1 As GridSummaryItem In INDgcvInvoiceDetailGrid.GroupSummary
                    If item1.DisplayFormat.Contains("C2") OrElse item1.DisplayFormat.Contains("c2") Then
                        item1.DisplayFormat = item1.DisplayFormat.Replace("2", "0")
                    End If
                Next
                INDRepSpinEValor.DisplayFormat.FormatString = "c0"
                INDRepSpinEValor.IsFloatValue = False
            End If
        End Using
    End Function
    ''' <summary>
    ''' Funcion para cada vez que asignen un valor a los label's (Manjo de decimales)
    ''' </summary>
    ''' <param name="Value"></param>
    ''' <returns></returns>
    Private Function ManageDecimalsFun(Value As Decimal)
        If _parameterGlosas.ManageDecimals = False Then
            Return Value.MoneyFormat(numberDecimal:=0)
        Else
            Return Value.MoneyFormat()
        End If
    End Function

    ''' <summary>
    ''' Método que abre el form para reasignar usuario
    ''' </summary>
    Private Async Sub OpenFormAssignRadicateResponsible()
        Dim listObjectionReceptionDetail As New List(Of GlosaObjectionsReceptionD)

        For Each item As Integer In INDgcvObjetions.GetSelectedRows()
            If item > -1 Then
                ObjetoD = TryCast(INDgcvObjetions.GetRow(item), GlosaObjectionsReceptionD)
                If ObjetoD IsNot Nothing AndAlso ObjetoD.Id > 0 Then
                    If ObjetoD.GlosaPortfolioGlosada.State = 3 Then
                        listObjectionReceptionDetail.Add(ObjetoD)
                    End If
                End If
            End If
        Next

        If listObjectionReceptionDetail.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar detalles pendientes envío de oficio"
            Exit Sub
        End If

        Using formulario As New FrmAssignRadicateResponsible()
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.Width = 450
            formulario.Height = 250
            formulario.ToolBar.Visible = False
            formulario.ListObjectionReceptionDetail = listObjectionReceptionDetail
            Dim result = formulario.ShowDialog(Me)
            If result = System.Windows.Forms.DialogResult.OK Then
                AsyncLoader(True)
                Me.DataSourceObjectsInvoices = Await Model.ListReceptionsObjectionD(Objeto.Id)
                Me.INDgcObjetions.RefreshDataSource()
                AsyncLoader(False)
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Método que abre el form para reasignar usuario
    ''' </summary>
    Private Async Sub OpenFormObjectionReceptionDetailRadicate()
        Dim listObjectionReceptionDetail As New List(Of GlosaObjectionsReceptionD)

        For Each item As Integer In INDgcvObjetions.GetSelectedRows()
            If item > -1 Then
                ObjetoD = TryCast(INDgcvObjetions.GetRow(item), GlosaObjectionsReceptionD)
                If ObjetoD IsNot Nothing AndAlso ObjetoD.Id > 0 Then
                    If ObjetoD.GlosaPortfolioGlosada.State = 3 Then
                        listObjectionReceptionDetail.Add(ObjetoD)
                    End If
                End If
            End If
        Next

        If listObjectionReceptionDetail.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar detalles pendientes envío de oficio"
            Exit Sub
        End If

        Using formulario As New FrmObjectionReceptionDetailRadicate()
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.Width = 450
            formulario.Height = 480
            formulario.ToolBar.Visible = False
            formulario.ListObjectionReceptionDetail = listObjectionReceptionDetail
            Dim result = formulario.ShowDialog(Me)
            If result = System.Windows.Forms.DialogResult.OK Then
                AsyncLoader(True)
                Me.DataSourceObjectsInvoices = Await Model.ListReceptionsObjectionD(Objeto.Id)
                Me.INDgcObjetions.RefreshDataSource()
                AsyncLoader(False)
            End If
        End Using
    End Sub

#Region "Subir glosa masivas desde excel"

    ''' <summary>
    ''' Exportar Datos a excel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CtrExportGlosa1_Load(sender As Object, e As EventArgs) Handles CtrExportGlosa1.ClickExport
        If Me.INDgcInvoices.DataSource IsNot Nothing Then
            Try
                Dim ListGlosasObjectD = TryCast(INDgcvObjetions.DataSource, List(Of GlosaObjectionsReceptionD))
                Dim PreviosObjectBalances As Boolean = False
                If ListGlosasObjectD.Count > 0 AndAlso ListGlosasObjectD.Any(Function(x) x.DocumentType IsNot Nothing AndAlso x.DocumentType = "2") Then
                    If MessageIndigo.Show("¿Desea Incluir saldos de Objeción anterior?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                        PreviosObjectBalances = True
                    End If
                End If
                AsyncLoader(True)
                Dim listDetail As DataSet = Model.ReceptionExcelExport(Objeto.Id)

                Dim LockMe As New Object()
                If PreviosObjectBalances Then
                    Parallel.ForEach(listDetail.Tables("ReceptionExcelExport").Rows.OfType(Of DataRow), Sub(item As DataRow)
                                                                                                            SyncLock LockMe

                                                                                                                If item.Item(19) = "2" And item.Item(17) = 0 Then
                                                                                                                    item.Delete()
                                                                                                                    Return
                                                                                                                End If
                                                                                                                If item.Item(19) = "2" Then
                                                                                                                    item.Item(16) = item.Item(17)
                                                                                                                    item.Item(20) = item.Item(18)
                                                                                                                End If
                                                                                                            End SyncLock
                                                                                                        End Sub)
                End If
                Me.CtrExportGlosa1.INDgCExport.DataSource = listDetail.Tables("ReceptionExcelExport")
                If Me.CtrExportGlosa1.INDgCExport.DataSource IsNot Nothing Then

                    Dim _gridView = Me.CtrExportGlosa1.INDgvExport
                    If _gridView IsNot Nothing AndAlso _gridView.VisibleColumns.Count > 0 Then
                        Dim fileName As String = System.IO.Path.GetTempFileName() & ".xlsx"
                        Dim param As New DevExpress.XtraPrinting.XlsxExportOptions(DevExpress.XtraPrinting.TextExportMode.Value, True, False)
                        _gridView.ExportToXlsx(fileName, param)
                        If System.IO.File.Exists(fileName) Then
                            System.Diagnostics.Process.Start(fileName)
                        End If
                    End If
                End If
                AsyncLoader(False)
            Catch ex As Exception
                AsyncLoader(False)
                Mensaje(EeventViewerImages.Advertencia) = ex.Message.ToString()
            End Try
            'End If
        End If
    End Sub

    ''' <summary>
    ''' Abre formulario de Carga de Datos de excel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnLoadExcel_Click(sender As Object, e As EventArgs) Handles INDbtnLoadExcel.Click
        Me.GetOpenFileDialog()
    End Sub

    ''' <summary>
    ''' Objeto openFileDialog
    ''' </summary>
    Private fileOpener As New OpenFileDialog
    ''' <summary>
    ''' ruta excel a cargar
    ''' </summary>
    ''' <remarks></remarks>
    Private rutaExcel As String


    ''' <summary>
    ''' Función para crear el objeto openFileDialog
    ''' </summary>
    Private Sub GetOpenFileDialog()
        Try
            fileOpener.CheckPathExists = True
            fileOpener.CheckFileExists = True
            fileOpener.Filter = "Excel Files(.xlsx)|*.xlsx| Excel Files(.xls)|*.xls| " &
                                 "Excel Files(*.xlsm)|*.xlsm"
            fileOpener.Multiselect = False
            fileOpener.AddExtension = True
            fileOpener.ValidateNames = True
            'fileOpener.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            fileOpener.InitialDirectory = Infrastructure.CrossCutting.Base.Window.Utils.DeskTopFolder()
            If (fileOpener.ShowDialog(Me) = DialogResult.OK) Then
                If fileOpener.FileName <> String.Empty Then
                    Dim fileInfo = New IO.FileInfo(fileOpener.FileName)
                    rutaExcel = fileOpener.FileName
                    Using FrmLoad As New FrmLoadexcel
                        FrmLoad.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                        FrmLoad.PathFile = rutaExcel
                        FrmLoad.ListInvoice = Me.DataSourceObjectsInvoices
                        FrmLoad.Size = New System.Drawing.Size(800, 600)
                        FrmLoad.ModuleName = "REC"
                        Dim transparent As New FrmTransparent(FrmLoad, False)
                        transparent.ShowDialog(Me)
                    End Using
                    UpdateListObjD()
                End If
            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = ex.Message.ToString
        End Try
    End Sub

    Private Async Sub UpdateListObjD()
        Me.DataSourceObjectsInvoices = Await Model.ListReceptionsObjectionD(Objeto.Id)
        INDgcObjetions.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Realiza una Reiteracion Masiva desde la rejilla del detalle
    ''' </summary>
    Private Async Sub MassiveGeneralReiteration()
        If Me.INDgcvObjetions.SelectedRowsCount = 0 Then
            Exit Sub
        End If
        Dim _messages = New List(Of Tuple(Of String, Integer))
        Dim frmPopUp As New FrmCommentReiteration()
        Using tras As New FrmTransparent(frmPopUp, False)
            If tras.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                AsyncLoader(True)
                For Each item As Integer In INDgcvObjetions.GetSelectedRows()
                    If item > -1 Then
                        Dim GORDetail = TryCast(INDgcvObjetions.GetRow(item), GlosaObjectionsReceptionD)
                        If GORDetail Is Nothing OrElse GORDetail.Id = 0 Then
                            _messages.Add(New Tuple(Of String, Integer)($"Tiene que Guardar y Actualizar el documento para poder reiterar la factura :{GORDetail.InvoiceNumber}", 2))
                            Continue For
                        End If
                        Dim _result = Await Model.ListGlosaInvoiceDetailByInvoiceNumberReiteration(GORDetail.InvoiceNumber, "RE")

                        If _result Is Nothing OrElse _result.StateResult = False OrElse _result.ObjectEmbbeded Is Nothing OrElse _result.ObjectEmbbeded.Count = 0 Then
                            _messages.Add(New Tuple(Of String, Integer)($"No se encontro detalle para la factura {GORDetail.InvoiceNumber}", 2))
                            Continue For
                        End If

                        Dim ListMov = ListGlosaMovementGlosa(_result.ObjectEmbbeded.ToList(), frmPopUp.CommentReiteration)
                        If ListMov.Count = 0 Then
                            Continue For
                        End If
                        Dim Result = Await SaveReiterationMovementGlosa(ListMov)
                        If Result.StateResult = True Then
                            _messages.Add(New Tuple(Of String, Integer)(String.Format("{0}; Factura: {1}", obtenerRecurso(ComunesActualizado), GORDetail.InvoiceNumber), 1))

                        Else
                            If Result.MessageResult IsNot Nothing AndAlso Result.MessageResult(0) = "-999" Then
                                _messages.Add(New Tuple(Of String, Integer)(String.Format("{0}, {1}", obtenerRecurso(Eresources.ComunesErrorConcurrencia), GORDetail.InvoiceNumber), 2))
                            Else
                                _messages.Add(New Tuple(Of String, Integer)(String.Format("{0}, {1}", obtenerRecurso(ComunesContacteAdministrador), GORDetail.InvoiceNumber), 2))
                            End If
                        End If
                    End If
                Next
                AsyncLoader(False)
                If _messages.Count > 0 Then
                    Using formulario As New FrmListErrors(_messages)
                        formulario.Title = "Lista de Mensajes"
                        formulario.StartPosition = FormStartPosition.CenterParent
                        Dim transparent As New FrmTransparent(formulario, False)
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        transparent.ShowDialog(Me)
                    End Using
                End If
                LoadControls()
            End If
        End Using
    End Sub

#End Region

#End Region

#Region "Handlers"

#Region "FormClosing"

    ''' <summary>
    ''' Evento al cerrar el formulario
    ''' </summary>
    Private Sub FrmObjectionReception_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        BlockedRecord()
    End Sub
#End Region

#Region "Constructor"
    ''' <summary>
    ''' Initialize
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        InitializeComponent()
        _openFindSenser = "INDbteConsecutive".ToUpper()
    End Sub
#End Region

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        CustomerId = Nothing
        DeleteObjRecD = Nothing
        Objeto = Nothing
        CustomerTmp = Nothing
        Model = Nothing
        Presenter = Nothing
        _openFindSenser = Nothing
        ObjetoD = Nothing
        SumValorEntidad = Nothing
        SpecificConceptId = Nothing
        SpecificConceptCode = Nothing
        record = Nothing
        FlagBlockRecord = Nothing
        sumValueGLosadoTotal = Nothing
        _sequense = Nothing
        _idCurrentSequense = Nothing
        LIstMessage = Nothing
        DatasourceCustomers = Nothing
        AcumulativoValorGlosado = Nothing
        _indexFocus = Nothing
        _reiterationQx = Nothing
        balance = Nothing
        SearchMode = Nothing
    End Sub

    ''' <summary>
    ''' Evento load del formulario donde istanciamos el precentador y cargamos los gridlookupedit en asincrono
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub FrmObjectionsReception_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Aqui se carga el control de tiempo
        Me.LoadXtraTrackControl()
        Me._funct = AddressOf GenerateDoc
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.Model = New MObjectionsReception(Me.Tag)
        Me.indigo = SessionValues.Instance
        '******************************'
        Me.INDbteNit.FuncQueryOnKeyEnterPressed = AddressOf Me.INDbteNit_KeyDown
        Me.INDbteNit.View.OptionsView.ShowGroupPanel = False
        Me.LoadStatus()
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecordEnabled = False
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcObjetions, True)
        Me.IndigoGridControl1.RefreshGrid(Me.INDgcObjetions)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcInvoiceDetailGrid, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcInvoices, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcSeeMovement, True)
        TabbedControlGroup1.SelectedTabPageIndex = 0
        BarraBotones.BarBtnImportar.Caption = "Nuevo"
        Presenter = New PObjectionsReception(Me)
        Presenter.Initializes()
        If Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.Native Or Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegractionFox Or Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNet Or Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNATIVE_MIGRATIONS Then
            INDlyiContainer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me.Presenter.GetSequense()
            '_idCurrentSequense = Me._sequense.PortfolioSequenceDetail(0).Id
        Else
            INDlyiContainer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            '_idCurrentSequense = 0
        End If
        CtrNavigation.Group = LayoutControlGroup8
        CtrTraceabilityControl.Visible = False
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Permisos) = True
        BarraBotones.RibbonPageProcesos.Visible = False
        Deshacer()
        SearchMode = False
        Me.OperatingUnitId = Me.BarraBotones.OperatingUnitValue
        Await LoadParameters()
    End Sub
#End Region

#Region "EditValueChanged"
    ''' <summary>
    ''' Handle cuando se selecciona un concepto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgleConcept_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleConcept.EditValueChanged
        Dim gridLookUpConcept As GridLookUpEdit = CType(sender, GridLookUpEdit)
        Dim ObjConcept As Domain.Entities.ConceptGlosas = gridLookUpConcept.GetSelectedDataRow()
        'Dim a = INDgleConcept.GetRowByKeyValue(grid.EditValue)
        SpecificConceptId = ObjConcept.Id
        SpecificConceptCode = ObjConcept.Code
    End Sub
    ''' <summary>
    ''' Aplicar Filtro Cada Ves Que se Escriba en el Txt de Buscar Factura
    ''' </summary>
    Private Sub INDpceBuscaFactura_EditValueChanged(sender As Object, e As EventArgs) Handles INDpceBuscaFactura.EditValueChanged
        INDgcvInvoices.ApplyFindFilter(INDpceBuscaFactura.Text)
    End Sub

    ''' <summary>
    ''' Metodo para limpiar la rejilla del detalle cuando cambian parametro de busqueda por compañias
    ''' </summary>
    Private Sub INDglBranch_EditValueChanged(sender As Object, e As EventArgs) Handles INDglCompany.EditValueChanged
        If Me.indigo IsNot Nothing Then
            If Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
                Dim ObjCompany As Domain.Entities.GlosasParametersInterface = CType(INDglCompany.GetSelectedDataRow, Domain.Entities.GlosasParametersInterface)
                If ObjCompany IsNot Nothing Then
                    LoadAdvancedFilter(ObjCompany)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento datechanged donde preguntamos si la fecha ingresada es mayor a la fecha actual y mostramos un mensaje
    ''' y colocamos la fecha actual.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDdeDocumentDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDdeDocumentDate.EditValueChanged
        If CDate(INDdeDocumentDate.EditValue) > Date.Now Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(seleccioneFechafininvalida, RecepcionObjeciones)
            INDdeDocumentDate.EditValue = Date.Now
        End If
    End Sub


    Private Async Sub INDbteNit_EditValueChanged(sender As Object, e As EditValueChangedEventArgs) Handles INDbteNit.EditValueChanged
        If Me.INDbteNit.EditValue IsNot Nothing AndAlso Me.INDbteNit.EditValue > 0 Then
            Me.CustomerTmp = Await Model.GetCustomerById(Me.INDbteNit.EditValue)
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Establecer Foco en la Busqueda De Facturas ERP
    ''' </summary>
    Private Sub INDpceBuscaFactura_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDpceBuscaFactura.QueryPopUp
        If Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
            If INDglCompany.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(SeleccioneUnaEmpresa, RecepcionObjeciones)
            End If
        Else
            LoadAdvancedFilter()
        End If
    End Sub

    ''' <summary>
    ''' Handles Mostrar Detalle columnas 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDMoreColumnPopCon_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDMoreColumnPopCon.QueryPopUp
        Dim AuxInvoiceDetail As GlosaInvoiceDetail
        If INDgcvInvoiceDetailGrid.FocusedRowHandle > -1 Then
            If Haveqx() Then
                Dim mainView1 As GridView = INDgcvInvoiceDetailGrid
                Dim detailView2 As GridView = INDgcInvoiceDetailGrid.FocusedView ' TryCast(mainView1.GetDetailView(mainView1.FocusedRowHandle, mainView1.GetRelationIndex(mainView1.FocusedRowHandle, "GlosaInvoiceDetailQX")), GridView)
                AuxInvoiceDetail = INDgcvInvoiceDetailGrid.GetFocusedRow
                If AuxInvoiceDetail.GlosaInvoiceDetailQX.Count > 0 Then
                    If detailView2 IsNot Nothing Then
                        If detailView2.Name = "INDgcvInvoiceDetailGrid" Then
                            AuxInvoiceDetail = TryCast(INDgcvInvoiceDetailGrid.GetRow(INDgcvInvoiceDetailGrid.FocusedRowHandle), GlosaInvoiceDetail)
                            CtrXtraInfoInvoiceDetail.ListProperties.Clear()
                            CtrXtraInfoInvoiceDetail.WidthTextLabel = 150
                            CtrXtraInfoInvoiceDetail.ListProperties.Add(New XtraInfoProperty("Area de Servicio", AuxInvoiceDetail.ServiceAreaCodeName))
                            CtrXtraInfoInvoiceDetail.ListProperties.Add(New XtraInfoProperty("Centro de Costo", AuxInvoiceDetail.CostCenterCodeName))
                            CtrXtraInfoInvoiceDetail.ListProperties.Add(New XtraInfoProperty("Medico", AuxInvoiceDetail.MedicalCodeName))
                            CtrXtraInfoInvoiceDetail.ListProperties.Add(New XtraInfoProperty("Fecha Servicio", AuxInvoiceDetail.ServiceDate))
                            CtrXtraInfoInvoiceDetail.ListProperties.Add(New XtraInfoProperty("Valor Servicio Manual", IIf(AuxInvoiceDetail.ValueServiceManual Is Nothing, 0, ManageDecimalsFun(AuxInvoiceDetail.ValueServiceManual.Value))))
                        Else
                            'qx
                            Dim AuxDetailqx As GlosaInvoiceDetailQX = detailView2.GetFocusedRow
                            Dim ObjD As GlosaObjectionsReceptionD = TryCast(INDgcvObjetions.GetRow(INDgcvObjetions.FocusedRowHandle), GlosaObjectionsReceptionD)
                            CtrXtraInfoInvoiceDetail.ListProperties.Clear()
                            CtrXtraInfoInvoiceDetail.ListProperties.Add(New XtraInfoProperty("Area de Servicio", AuxDetailqx.ServiceAreaCodeName))
                            CtrXtraInfoInvoiceDetail.ListProperties.Add(New XtraInfoProperty("Centro de Costo", AuxDetailqx.CostCenterCodeName))
                            CtrXtraInfoInvoiceDetail.ListProperties.Add(New XtraInfoProperty("Servicio- Area", AuxDetailqx.DescriptionServiceArea))
                            CtrXtraInfoInvoiceDetail.ListProperties.Add(New XtraInfoProperty("Medico", AuxDetailqx.MedicalCodeName))
                            CtrXtraInfoInvoiceDetail.ListProperties.Add(New XtraInfoProperty("Valor Servicio Manual", ManageDecimalsFun(AuxDetailqx.ValueServiceManual)))
                        End If
                    End If
                End If
            Else
                AuxInvoiceDetail = TryCast(INDgcvInvoiceDetailGrid.GetRow(INDgcvInvoiceDetailGrid.FocusedRowHandle), GlosaInvoiceDetail)
                CtrXtraInfoInvoiceDetail.ListProperties.Clear()
                CtrXtraInfoInvoiceDetail.WidthTextLabel = 150
                CtrXtraInfoInvoiceDetail.ListProperties.Add(New XtraInfoProperty("Area de Servicio", AuxInvoiceDetail.ServiceAreaCodeName))
                CtrXtraInfoInvoiceDetail.ListProperties.Add(New XtraInfoProperty("Centro de Costo", AuxInvoiceDetail.CostCenterCodeName))
                CtrXtraInfoInvoiceDetail.ListProperties.Add(New XtraInfoProperty("Medico", AuxInvoiceDetail.MedicalCodeName))
                CtrXtraInfoInvoiceDetail.ListProperties.Add(New XtraInfoProperty("Fecha Servicio", AuxInvoiceDetail.ServiceDate))
                CtrXtraInfoInvoiceDetail.ListProperties.Add(New XtraInfoProperty("Valor Servicio Manual", IIf(AuxInvoiceDetail.ValueServiceManual Is Nothing, 0, ManageDecimalsFun(AuxInvoiceDetail.ValueServiceManual.Value))))
            End If
        End If

    End Sub

    ''' <summary>
    ''' Handles Mostrar Detalle columnas OBjD
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDMoreColumnObjD_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDMoreColumnObjD.QueryPopUp
        If INDgcvObjetions.FocusedRowHandle > -1 Then
            Dim ObjD As GlosaObjectionsReceptionD = TryCast(INDgcvObjetions.GetRow(INDgcvObjetions.FocusedRowHandle), GlosaObjectionsReceptionD)
            CtrXtraInfoInvoice.ListProperties.Clear()
            CtrXtraInfoInvoice.ListProperties.Add(New XtraInfoProperty("Codigo Contrato", ObjD.GlosaPortfolioGlosada.ContractCode))
            CtrXtraInfoInvoice.ListProperties.Add(New XtraInfoProperty("Contrato", ObjD.GlosaPortfolioGlosada.ContractName))
            CtrXtraInfoInvoice.ListProperties.Add(New XtraInfoProperty("Identificacion", ObjD.GlosaPortfolioGlosada.PatientCode))
            CtrXtraInfoInvoice.ListProperties.Add(New XtraInfoProperty("Paciente", ObjD.GlosaPortfolioGlosada.PatientName))
            CtrXtraInfoInvoice.ListProperties.Add(New XtraInfoProperty("Usuario", ObjD.GlosaPortfolioGlosada.UserNameInvoice))
            CtrXtraInfoInvoice.ListProperties.Add(New XtraInfoProperty("Comentario", ObjD.Comment, 0))
        End If
    End Sub
    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleAccountStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbteNit_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDbteNit.QueryPopUp
        If INDbteNit.Datasource Is Nothing Then
            LoadXpoCustomers()
        End If
    End Sub
#End Region

#Region "GotFocus"
    ''' <summary>
    ''' Pone la variable _openFindSender en vacio para evitar
    ''' abrir el formulario de busqueda en un control que no aplique la funcionalidad
    ''' </summary>
    Private Sub OpenFindSender_LostFocus(sender As Object, e As EventArgs) Handles INDbteConsecutive.LostFocus
        Me._openFindSenser = String.Empty
    End Sub

    ''' <summary>
    ''' Asigna el nombre del sender a la variable _openFindSender para habilitar
    ''' la funcionalidad de buscar en el control que corresponda
    ''' </summary>
    Private Sub OpenFindSender_GotFocus(sender As Object, e As EventArgs) Handles INDbteConsecutive.GotFocus
        Me._openFindSenser = CType(sender, System.Windows.Forms.Control).Name.ToUpper()
    End Sub
#End Region

#Region "KeyDown"
    ''' <summary>
    ''' Evento keydown del control de consecutivo
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDbteConsecutive_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbteConsecutive.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
                ' Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ConsecutivoObjecionNoExiste, RecepcionObjeciones)
                Exit Sub
            End If
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = True
            Me.BarraBotones.StatusRecord = "1" 'Sin confirmar
            LoadControls()
            INDbteConsecutive.Enabled = False
        End If
    End Sub


    ''' <summary>
    ''' Buscar Una Factura del ERP
    ''' </summary>
    Private Async Sub INDTxtBuscaFactura_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDpceBuscaFactura.KeyDown
        Try
            If e.KeyCode.Equals(Keys.Enter) Then
                Dim Invoice As SP_invoiceList_Result = Nothing
                If INDpceBuscaFactura.Text <> String.Empty Then
                    If INDbteNit.EditValue IsNot Nothing Then
                        Dim found As GlosaObjectionsReceptionD = Nothing
                        If Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
                            If INDglCompany.EditValue IsNot Nothing Then
                                Dim ObjCompany As Domain.Entities.GlosasParametersInterface = CType(INDglCompany.GetSelectedDataRow, Domain.Entities.GlosasParametersInterface)
                                If ObjCompany IsNot Nothing Then
                                    Dim invoiceNumberFixed As String = Utils.FixInvoiceNumber(INDpceBuscaFactura.Text.Trim(), ObjCompany.AccountingMethod)
                                    found = Me.DataSourceObjectsInvoices.Find(Function(value As GlosaObjectionsReceptionD) value.InvoiceNumber = invoiceNumberFixed)
                                    If found Is Nothing Then 'si la factura no esta en la lista, lanzamos servicio de carge de datos
                                        AsyncLoader(True)
                                        Invoice = Await Model.GetInvoice(ObjCompany.ContainerName, INDbteNit.EditValue, invoiceNumberFixed, String.Empty, 0)
                                        AsyncLoader(False)
                                        'valido que la factura agregar no se encuentre ya en la lista
                                        If Invoice IsNot Nothing AndAlso Invoice.InvoiceNumber IsNot Nothing Then
                                            If ObjCompany.AccountingMethod = eTypeInterface.FoxPrivate Or ObjCompany.AccountingMethod = eTypeInterface.NETPrivate Then
                                                If Invoice.StateCurrentInvoice <> "2" Then
                                                    Dim ComplementoMensaje As String = StateErp(Invoice.StateCurrentInvoice)
                                                    INDpceBuscaFactura.Focus()
                                                    MessageIndigo.Show(String.Format(obtenerRecurso(NoSePuedeAgregarFacturaEstado, RecepcionObjeciones), ComplementoMensaje), MessageType.Warning, Me.Text)
                                                    Exit Sub
                                                End If
                                            End If
                                            LIstMessage = New List(Of String)
                                            ItemAddRejilla(Invoice)
                                            If LIstMessage.Count > 0 Then
                                                Dim stringBuilder As New System.Text.StringBuilder()
                                                For Each item As String In LIstMessage
                                                    stringBuilder.AppendLine(item.ToString)
                                                Next
                                                Mensaje(EeventViewerImages.Advertencia) = stringBuilder.ToString
                                            End If
                                            LIstMessage = New List(Of String)
                                            INDpceBuscaFactura.EditValue = Nothing
                                        Else
                                            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NoseEncuentraFactura, RecepcionObjeciones)
                                        End If
                                    Else
                                        Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FacturaExisteLista, RecepcionObjeciones), invoiceNumberFixed)
                                    End If
                                End If
                            Else
                                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(SeleccioneUnaEmpresa, RecepcionObjeciones)
                            End If
                        ElseIf Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.Native Or Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegractionFox Or Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNet Or Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNATIVE_MIGRATIONS Then
                            found = Me.DataSourceObjectsInvoices.Find(Function(value As GlosaObjectionsReceptionD) value.InvoiceNumber = INDpceBuscaFactura.Text.Trim())
                            If found Is Nothing Then 'si la factura no esta en la lista, lanzamos servicio de carge de datos
                                AsyncLoader(True)
                                Invoice = Await Model.GetInvoice(String.Empty, INDbteNit.EditValue, INDpceBuscaFactura.Text.Trim(), String.Empty, 0)
                                AsyncLoader(False)
                                'valido que la factura agregar no se encuentre ya en la lista
                                If Invoice IsNot Nothing AndAlso Invoice.InvoiceNumber IsNot Nothing Then
                                    LIstMessage = New List(Of String)
                                    ItemAddRejilla(Invoice)
                                    If LIstMessage.Count > 0 Then
                                        Dim stringBuilder As New System.Text.StringBuilder()
                                        For Each item As String In LIstMessage
                                            stringBuilder.AppendLine(item.ToString)
                                        Next
                                        Mensaje(EeventViewerImages.Advertencia) = stringBuilder.ToString
                                    End If
                                    LIstMessage = New List(Of String)
                                    INDpceBuscaFactura.EditValue = Nothing
                                Else
                                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NoseEncuentraFactura, RecepcionObjeciones)
                                End If
                            Else
                                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FacturaExisteLista, RecepcionObjeciones), INDpceBuscaFactura.Text.Trim())
                            End If
                        End If
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(FaltaNitTercero, RecepcionObjeciones)
                    End If
                End If
            End If
        Catch ex As Exception
            Me.AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Aqui se implementa la funcionalidad de pegar facturas desde la clipboard
    ''' </summary>
    Private Sub INDgcvObjetions_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDgcvObjetions.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.V Then
            Me.PasteToGridInvoices()
        End If
    End Sub

    ''' <summary>
    ''' Guarda una Glosa o reiteracion desde la rejilla de detalle de factura
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BtnSaveMovementGlosa_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles BtnSaveMovementGlosa.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            'Dim mainView2 As GridView = INDgcvInvoiceDetailGrid
            Dim detailView3 As GridView = INDgcInvoiceDetailGrid.FocusedView 'TryCast(mainView2.GetDetailView(mainView2.FocusedRowHandle, mainView2.GetRelationIndex(mainView2.FocusedRowHandle, "GlosaInvoiceDetailQX")), GridView)
            If detailView3 IsNot Nothing Then
                If detailView3.Name = "INDgcvInvoiceDetailQXGrid" Then
                    GuardarGlosaMovement(True)
                Else
                    GuardarGlosaMovement(False)
                End If
            Else
                MessageIndigo.Show(obtenerRecurso(GloseDetalleQX, RecepcionObjeciones), MessageType.Warning, Me.Text)
                'limpiamos datos registrados
                If INDgcvInvoiceDetailGrid.FocusedRowHandle > -1 Then
                    Dim AuxInvoiceDetail = TryCast(INDgcvInvoiceDetailGrid.GetRow(INDgcvInvoiceDetailGrid.FocusedRowHandle), GlosaInvoiceDetail)
                    AuxInvoiceDetail.MovimientoAux = New GlosaMovementGlosa()
                    INDgcvInvoiceDetailGrid.RefreshRow(INDgcvInvoiceDetailGrid.FocusedRowHandle)
                Else
                    INDgcvInvoiceDetailGrid.RefreshData()
                End If
                Exit Sub
            End If
        End If
    End Sub
#End Region

#Region "Popup"

#End Region

#Region "Click"



    Private Sub INDbteActionSeeGlosa_Click(sender As Object, e As EventArgs) Handles INDbteActionSeeGlosa.Click
        Dim mainView2 As GridView = INDgcvInvoiceDetailGrid
        Dim detailView3 As GridView = INDgcInvoiceDetailGrid.FocusedView
        Dim OBJGlosaInvoiceDetail As GlosaInvoiceDetail = INDgcvInvoiceDetailGrid.GetFocusedRow
        If OBJGlosaInvoiceDetail.GlosaInvoiceDetailQX.Count > 0 Then
            If detailView3 IsNot Nothing Then
                If detailView3.IsFocusedView = True Then
                    GlosarItemqx()
                Else
                    MessageIndigo.Show(obtenerRecurso(GloseDetalleQX, RecepcionObjeciones), MessageType.Warning, Me.Text)
                    Exit Sub
                End If
            Else
                MessageIndigo.Show(obtenerRecurso(GloseDetalleQX, RecepcionObjeciones), MessageType.Warning, Me.Text)
                Exit Sub
            End If
        Else
            GlosarItem()
        End If
    End Sub

    ''' <summary>
    ''' Handle Mostrar Vista de los movimineto sobre rejilla de detalle de factura
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDMoreSeeDetailGlosaPopCon_Click(sender As Object, e As EventArgs) Handles INDMoreSeeDetailGlosaPopCon.Click
        Me.INDgcSeeMovement.DataSource = Nothing
        Dim mainView1 As GridView = INDgcvInvoiceDetailGrid
        Dim detailView2 As GridView = INDgcInvoiceDetailGrid.FocusedView ' TryCast(mainView1.GetDetailView(mainView1.FocusedRowHandle, mainView1.GetRelationIndex(mainView1.FocusedRowHandle, "GlosaInvoiceDetailQX")), GridView)
        Dim AuxInvoiceDetail As GlosaInvoiceDetail = INDgcvInvoiceDetailGrid.GetFocusedRow
        If AuxInvoiceDetail.GlosaInvoiceDetailQX.Count > 0 Then
            If detailView2 IsNot Nothing Then
                'qx
                Dim AuxDetailqx As GlosaInvoiceDetailQX = detailView2.GetFocusedRow
                If AuxDetailqx IsNot Nothing Then
                    Me.INDgcSeeMovement.DataSource = AuxDetailqx.GlosaMovementGlosa  'AuxInvoiceDetail.GlosaMovementGlosa.Where(Function(c) c.InvoiceDetailIdQX = AuxDetailqx.Id)
                End If
            Else
                MessageIndigo.Show(obtenerRecurso(GloseDetalleQX, RecepcionObjeciones), MessageType.Warning, Me.Text)
                Exit Sub
            End If
        Else
            Me.INDgcSeeMovement.DataSource = AuxInvoiceDetail.GlosaMovementGlosa
        End If
    End Sub
    ''' <summary>
    ''' Abre el Fronta de registro de glosa
    ''' </summary>
    Private Sub INDGlosarBtn_Click(sender As Object, e As EventArgs) Handles INDGlosarBtn.Click
        Dim mainView2 As GridView = INDgcvInvoiceDetailGrid
        Dim detailView3 As GridView = INDgcInvoiceDetailGrid.FocusedView ' TryCast(mainView2.GetDetailView(mainView2.FocusedRowHandle, mainView2.GetRelationIndex(mainView2.FocusedRowHandle, "GlosaInvoiceDetailQX")), GridView)
        Dim OBJGlosaInvoiceDetail As GlosaInvoiceDetail = INDgcvInvoiceDetailGrid.GetFocusedRow
        If OBJGlosaInvoiceDetail.GlosaInvoiceDetailQX.Count > 0 Then
            If detailView3 IsNot Nothing Then
                If detailView3.IsFocusedView = True Then
                    GlosarItemqx()
                Else
                    MessageIndigo.Show(obtenerRecurso(GloseDetalleQX, RecepcionObjeciones), MessageType.Warning, Me.Text)
                    Exit Sub
                End If
            Else
                MessageIndigo.Show(obtenerRecurso(GloseDetalleQX, RecepcionObjeciones), MessageType.Warning, Me.Text)
                Exit Sub
            End If
        Else
            GlosarItem()
        End If
    End Sub

    ''' <summary>
    ''' Elimina Item de la rejilla de detalle de objeciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnEliminar_Click(sender As Object, e As EventArgs) Handles INDbtnEliminar.Click
        If FlagBlockRecord = True Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
        Else
            DeleteItem()
        End If
    End Sub

    ''' <summary>
    ''' Metodo para Agregar una Factura a la rejilla de detalle
    ''' </summary>
    Private Sub INDbtnAgregarFactura_Click(sender As Object, e As EventArgs) Handles INDbtnAgregarFactura.Click
        'lista temporal que guardar las facturas agregadas para despues eliminarlas de la rejilla de facturas del PopUp
        Dim tmpListaSP_invoiceList_ResultBorrar As List(Of SP_invoiceList_Result) = New List(Of SP_invoiceList_Result)
        LIstMessage = New List(Of String)
        'recorro las facturas
        If Me.DataSourceInvoices IsNot Nothing Then
            For i As Integer = 0 To Me.DataSourceInvoices.Count - 1
                Dim itemInvoice As SP_invoiceList_Result = Me.DataSourceInvoices(i)
                'verifico que la factura este seleccionada
                If (itemInvoice.Selection = True) Then
                    ItemAddRejilla(itemInvoice)
                End If
            Next
        End If

        'mensaje de validacion
        If LIstMessage.Count > 0 Then
            Dim stringBuilder As New System.Text.StringBuilder()
            For Each item As String In LIstMessage
                stringBuilder.AppendLine(item.ToString)
            Next
            Mensaje(EeventViewerImages.Advertencia) = stringBuilder.ToString
        End If
        LIstMessage = New List(Of String)
        Parallel.For(0, Me.DataSourceInvoices.Count, Sub(i)
                                                         Me.DataSourceInvoices(i).Selection = False
                                                     End Sub)
        'Actualizo la el datasource del PopPup de facturas
        For Each item As SP_invoiceList_Result In tmpListaSP_invoiceList_ResultBorrar
            Me.DataSourceInvoices.Remove(item)
        Next
        Me.INDgcInvoices.RefreshDataSource()
    End Sub
    ''' <summary>
    ''' Abrir Forntal de registerObjection para registro de glosa Modo general
    ''' </summary>
    Private Sub INDGeneralObjectionBtn_Click(sender As Object, e As EventArgs) Handles INDGeneralObjectionBtn.Click
        If ObjetoD.DocumentType.ToString() = "1" Then
            SumValorEntidad = 0
            If Me.INDgcvInvoiceDetailGrid.DataSource IsNot Nothing AndAlso Me.INDgcvInvoiceDetailGrid.DataRowCount > 0 Then
                For i As Integer = 0 To Me.INDgcvInvoiceDetailGrid.DataSource.count - 1
                    SumValorEntidad += TryCast(Me.INDgcvInvoiceDetailGrid.DataSource(i), GlosaInvoiceDetail).InvoicedValue
                Next
            End If
            OpenFrmRegisterObjection(True, False)
        ElseIf ObjetoD.DocumentType.ToString() = "2" Then
            GeneralReiteration()
        End If
    End Sub


    ''' <summary>
    ''' Guarda el Comentario General de Una Factura
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDBtnGuardarComment_Click(sender As Object, e As EventArgs) Handles INDBtnGuardarComment.Click
        If Me.FlagBlockRecord = True Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
            Exit Sub
        End If

        Dim ObjD As GlosaObjectionsReceptionD = TryCast(INDgcvObjetions.GetRow(INDgcvObjetions.FocusedRowHandle), GlosaObjectionsReceptionD)
        If INDMemoGeneralComment.Text <> String.Empty Then
            ObjD.Comment = INDMemoGeneralComment.Text
            AsyncLoader(True)
            Dim Result = Await Model.SaveObjectionReceptionD(ObjD)
            AsyncLoader(False)
            If Result.StateResult = True Then
                Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
            Else
                If Result.MessageResult IsNot Nothing AndAlso Result.MessageResult(0) = "-999" Then
                    Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(Eresources.ComunesErrorConcurrencia)
                Else
                    Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                End If
            End If
        End If
    End Sub


    ''' <summary>
    ''' Handles the Click event of the INDbtnViewDetail control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDbtnViewDetail_Click(sender As Object, e As EventArgs) Handles INDbtnViewDetail.Click
        VerDetalle()
    End Sub
#End Region

#Region "ButtonClick"
    ''' <summary>
    ''' Abrir Busqueda de Consecutivos
    ''' </summary>
    Private Sub INDbteConsecutive_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbteConsecutive.ButtonClick
        AbrirBusquedaConsecutive()
    End Sub
#End Region

#Region "ShowingEditor"

#End Region

#Region "PopupMenuShowing"
    ''' <summary>
    ''' Muestra el menu de opciones
    ''' </summary>
    Private Sub INDgcvObjetions_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles INDgcvObjetions.PopupMenuShowing
        INDBtnPasteInvoice.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        INDBtnShowDetail.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        INDBtnConfirm.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        INDBtnDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        INDBtnAssignResponsible.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        INDBtOnbjectionReceptionDetailRadicate.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        INDBbiGeneralReiteration.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

        If e.HitInfo.RowHandle < 0 Then
            Exit Sub
        End If

        Dim listObjectionReceptionDetail As New List(Of GlosaObjectionsReceptionD)

        For Each item As Integer In INDgcvObjetions.GetSelectedRows()
            If item > -1 Then
                ObjetoD = TryCast(INDgcvObjetions.GetRow(item), GlosaObjectionsReceptionD)
                If ObjetoD IsNot Nothing AndAlso ObjetoD.Id > 0 Then
                    listObjectionReceptionDetail.Add(ObjetoD)
                End If
            End If
        Next

        If listObjectionReceptionDetail.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos un detalle o guardarlos para ejercer una acción"
            Exit Sub
        End If

        If Objeto.State = 1 Then
            If Me.INDgcvObjetions.SelectedRowsCount > 1 Then
                INDBtnConfirm.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                INDBtnDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            Else
                INDBtnPasteInvoice.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                INDBtnShowDetail.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                INDBtnConfirm.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                INDBtnDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            End If
            'Valida que todos las facturas seleccionadas sean para reiteracion general
            If INDgcvObjetions.GetSelectedRows().Count = listObjectionReceptionDetail.Where(Function(x) x.DocumentType = "2").Count Then
                INDBbiGeneralReiteration.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            End If
        ElseIf Objeto.State Is Nothing Then
            INDBtnShowDetail.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            INDBtnDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        Else
            INDBtnShowDetail.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If

        If listObjectionReceptionDetail.Where(Function(d) d.GlosaPortfolioGlosada.State = 3).Any Then
            INDBtnAssignResponsible.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            INDBtOnbjectionReceptionDetailRadicate.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If

        Dim View = CType(sender, GridView)
        INDPopMenuActions.Manager = BarManager1
        INDPopMenuActions.ShowPopup(View.GridControl.PointToScreen(e.Point))
    End Sub

    ''' <summary>
    ''' Muestra el menu de opciones rejilla detalle de factura
    ''' </summary>
    Private Sub INDgcvInvoiceDetailGrid_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles INDgcvInvoiceDetailGrid.PopupMenuShowing
        If e.Menu Is Nothing Then
            Exit Sub
        End If
        If e.HitInfo.RowHandle < 0 Then
            Exit Sub
        End If
        e.Menu.Items.Clear()

        Dim ListInvoiceDetailSelection As List(Of GlosaInvoiceDetail) = InvoiceDetailSelection()
        If ListInvoiceDetailSelection.Count > 1 Then
            If ObjetoD.DocumentType = "1" And ObjetoD.State = "1" Then
                e.Menu.Items.Add(New DXMenuItem(obtenerRecurso(MenuGlosarSeleccion, RecepcionObjeciones), AddressOf GlosarItem, My.Resources.GlosaBorde))
            ElseIf ObjetoD.DocumentType = "2" And ObjetoD.GlosaObjectionsReceptionC.State = "1" Then
                _reiterationQx = False
                e.Menu.Items.Add(New DXMenuItem(obtenerRecurso(MenuReiterar, RecepcionObjeciones), AddressOf ReiterationMultiple, My.Resources.GlosaBorde))
            Else
                e.Menu.Items.Add(New DXMenuItem(obtenerRecurso(MenuVerDetalle, RecepcionObjeciones), AddressOf GlosarItem, My.Resources.OpenRecord))
            End If
        Else
            If Haveqx() = True Then
                Exit Sub
            End If
            e.Menu.Items.Add(New DXMenuItem(obtenerRecurso(MenuVerDetalle, RecepcionObjeciones), AddressOf GlosarItem, My.Resources.OpenRecord))
        End If
    End Sub

    ''' <summary>
    ''' Muestra el menu de opciones
    ''' </summary>
    Private Sub INDgcvInvoiceDetailQXGrid_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles INDgcvInvoiceDetailQXGrid.PopupMenuShowing
        If e.Menu Is Nothing Then
            Exit Sub
        End If
        If e.HitInfo.RowHandle < 0 Then
            Exit Sub
        End If
        e.Menu.Items.Clear()

        Dim ListInvoiceDetailSelectionqx As List(Of GlosaInvoiceDetailQX) = InvoiceDetailqxSelection()
        If ListInvoiceDetailSelectionqx.Count > 1 Then
            If ObjetoD.DocumentType = "1" And ObjetoD.State = "1" Then
                e.Menu.Items.Add(New DXMenuItem(obtenerRecurso(MenuGlosarSeleccion, RecepcionObjeciones), AddressOf GlosarItemqx, My.Resources.GlosaBorde))
            ElseIf ObjetoD.DocumentType = "2" And ObjetoD.GlosaObjectionsReceptionC.State = "1" Then
                _reiterationQx = True
                e.Menu.Items.Add(New DXMenuItem(obtenerRecurso(MenuReiterar, RecepcionObjeciones), AddressOf ReiterationMultiple, My.Resources.GlosaBorde))
            Else
                e.Menu.Items.Add(New DXMenuItem(obtenerRecurso(MenuVerDetalle, RecepcionObjeciones), AddressOf GlosarItemqx, My.Resources.OpenRecord))
            End If
        Else
            e.Menu.Items.Add(New DXMenuItem(obtenerRecurso(MenuVerDetalle, RecepcionObjeciones), AddressOf GlosarItemqx, My.Resources.OpenRecord))
        End If
    End Sub
#End Region

#Region "ItemClick"

    Private Sub INDBtnPasteInvoice_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBtnPasteInvoice.ItemClick
        PasteToGridInvoices()
    End Sub

    Private Sub INDBtnShowDetail_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBtnShowDetail.ItemClick
        VerDetalle()
    End Sub

    Private Sub INDBtnConfirm_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBtnConfirm.ItemClick
        ConfirmarDetalle()
    End Sub

    Private Sub INDBtnDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBtnDelete.ItemClick
        DeleteItem()
    End Sub

    Private Sub INDBtnAssignResponsible_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBtnAssignResponsible.ItemClick
        OpenFormAssignRadicateResponsible()
    End Sub

    Private Sub INDBtnObjectionReceptionDetailRadicate_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBtOnbjectionReceptionDetailRadicate.ItemClick
        OpenFormObjectionReceptionDetailRadicate()
    End Sub

    ''' <summary>
    ''' Evento click para cuando Accionan la Retireacion general de forma masiva
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBbiGeneralReiteration_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiGeneralReiteration.ItemClick
        MassiveGeneralReiteration()
    End Sub

#End Region

#Region "DoubleClick"


    ''' <summary>
    ''' Abre el PopUpContainer de los detalles de factura
    ''' </summary>
    Private Sub INDgcvObjetions_DoubleClick(sender As Object, e As EventArgs) Handles INDgcvObjetions.DoubleClick
        Dim view As GridView = CType(sender, GridView)
        Dim pt As Point = view.GridControl.PointToClient(Control.MousePosition)
        Dim info As GridHitInfo = view.CalcHitInfo(pt)
        ' se valida que este sobre una fila
        If info.InRow OrElse info.InRowCell Then
            If info.Column IsNot Nothing Then
                ShowPopUpInvoiceDatail()
            End If
        End If
    End Sub


    ''' <summary>
    ''' Abre el PopUpContainer de los detalles de factura
    ''' </summary>
    Private Sub INDgcvInvoiceDetailGrid_DoubleClick(sender As Object, e As EventArgs) Handles INDgcvInvoiceDetailGrid.DoubleClick
        Dim view As GridView = CType(sender, GridView)
        Dim pt As Point = view.GridControl.PointToClient(Control.MousePosition)
        Dim info As GridHitInfo = view.CalcHitInfo(pt)
        ' se valida que este sobre una fila
        If info.InRow OrElse info.InRowCell Then
            If info.Column IsNot Nothing Then
                Dim mainView2 As GridView = INDgcvInvoiceDetailGrid
                Dim detailView3 As GridView = INDgcInvoiceDetailGrid.FocusedView ' TryCast(mainView2.GetDetailView(mainView2.FocusedRowHandle, mainView2.GetRelationIndex(mainView2.FocusedRowHandle, "GlosaInvoiceDetailQX")), GridView)
                Dim OBJGlosaInvoiceDetail As GlosaInvoiceDetail = INDgcvInvoiceDetailGrid.GetFocusedRow
                If OBJGlosaInvoiceDetail.GlosaInvoiceDetailQX.Count > 0 Then
                    If detailView3 IsNot Nothing Then
                        If detailView3.IsFocusedView = True Then
                            GlosarItemqx()
                        Else
                            MessageIndigo.Show(obtenerRecurso(GloseDetalleQX, RecepcionObjeciones), MessageType.Warning, Me.Text)
                            Exit Sub
                        End If
                    Else
                        MessageIndigo.Show(obtenerRecurso(GloseDetalleQX, RecepcionObjeciones), MessageType.Warning, Me.Text)
                        Exit Sub
                    End If
                Else
                    GlosarItem()
                End If
            End If
        End If
    End Sub
#End Region

#Region "ClickBack"
    ''' <summary>
    ''' Ocualtar el PopUpContainer del detalle de facturas
    ''' </summary>
    Private Sub CtrNavigation_ClickBack() Handles CtrNavigation.ClickBack
        ClosePopUpInvoiceDetail()
    End Sub

#End Region

#Region "IdEntityLoaded"

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.Objeto IsNot Nothing AndAlso Me.Objeto.Id > 0 Then
            If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                If Me.record IsNot Nothing AndAlso Me.INDbteConsecutive.Text = Me.IdEntity.Trim Then
                    Return
                End If
                Me.INDbteConsecutive.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbteConsecutive.Text = Me.IdEntity.Trim()
            Me.LoadControls()
        End If
        Me.IdEntity = String.Empty
    End Sub
#End Region

#Region "Activated"

    Private Sub FrmObjectionsReception_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        INDbteConsecutive.Focus()
    End Sub
#End Region

#Region "OpenFormButtonClick"
    Private Sub INDbteNit_OpenFormButtonClick(sender As Object, e As EventArgs) Handles INDbteNit.OpenFormButtonClick
        OpenForm(503, Nothing, True)
        LoadXpoCustomers()
    End Sub
#End Region

#Region "Closed"

    Private Sub INDMoreSeeDetailGlosaPopCon_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDMoreSeeDetailGlosaPopCon.Closed
        Me.INDgcSeeMovement.DataSource = New List(Of GlosaMovementGlosa)
    End Sub
#End Region

#Region "CustomColumnDisplayText"

    ''' <summary>
    ''' Metodo para mostrar la descripcion de la agrupacion por la columna document type
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="CustomColumnDisplayTextEventArgs"/> instance containing the event data.</param>
    Private Sub INDgcvObjetions_CustomColumnDisplayText(sender As Object, e As CustomColumnDisplayTextEventArgs) Handles INDgcvObjetions.CustomColumnDisplayText
        If e.Column.FieldName = "DocumentType" Then
            If e.Value IsNot Nothing Then
                Select Case e.Value.ToString.Trim()
                    Case "1"
                        e.DisplayText = "Glosa"
                    Case "2"
                        e.DisplayText = obtenerRecurso(EncabezadoReiteracion, RecepcionObjeciones)
                    Case Else
                        e.DisplayText = obtenerRecurso(TiponoReconocido, RecepcionObjeciones)
                End Select
            End If
        End If
    End Sub
#End Region

#Region "SelectionChanged"
    ''' <summary>
    ''' Evento cuando se seleccionan celdas de la rejilla de detalle de factura donde hacemos la sumatioria de las celdas seleccionada
    ''' siempre y la columna sea numerica
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.Data.SelectionChangedEventArgs"/> instance containing the event data.</param>
    Private Sub INDgcvInvoiceDetailGrid_SelectionChanged(sender As Object, e As DevExpress.Data.SelectionChangedEventArgs) Handles INDgcvInvoiceDetailGrid.SelectionChanged
        Dim Currency As Boolean = True
        Dim sum As Decimal = 0
        For Each c As GridCell In Me.INDgcvInvoiceDetailGrid.GetSelectedCells()
            If c.Column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric Then
                sum += Convert.ToDecimal(Me.INDgcvInvoiceDetailGrid.GetRowCellValue(c.RowHandle, c.Column))
                If c.Column.DisplayFormat.FormatString = String.Empty Then
                    Currency = False
                End If
            End If
        Next
        If Currency = True Then
            Me.INDtxtTotalSelection.Text = ManageDecimalsFun(sum)
        Else
            Me.INDtxtTotalSelection.Text = sum.MoneyFormat(0)
        End If
    End Sub

    ''' <summary>
    ''' Evento cuando se seleccionan celdas de la rejilla de detalle de factura qx donde hacemos la sumatioria de las celdas seleccionada
    ''' siempre y la columna sea numerica
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.Data.SelectionChangedEventArgs"/> instance containing the event data.</param>
    Private Sub INDgcvInvoiceDetailQXGrid_SelectionChanged(sender As Object, e As DevExpress.Data.SelectionChangedEventArgs) Handles INDgcvInvoiceDetailQXGrid.SelectionChanged
        Dim Currency As Boolean = True
        Dim Sum As Decimal
        Dim detailview2 As GridView = INDgcvInvoiceDetailGrid
        Dim detailView3 As GridView = INDgcInvoiceDetailGrid.FocusedView ' TryCast(detailview2.GetDetailView(detailview2.FocusedRowHandle, detailview2.GetRelationIndex(detailview2.FocusedRowHandle, "GlosaInvoiceDetailQX")), GridView)
        If detailView3 IsNot Nothing Then
            For Each c As GridCell In detailView3.GetSelectedCells()
                If c.Column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric Then
                    Sum += Convert.ToDecimal(detailView3.GetRowCellValue(c.RowHandle, c.Column))
                    If c.Column.DisplayFormat.FormatString = String.Empty Then
                        Currency = False
                    End If
                End If
            Next
        End If
        If Currency = True Then
            Me.INDtxtTotalSelection.Text = ManageDecimalsFun(Sum)
        Else
            Me.INDtxtTotalSelection.Text = ManageDecimalsFun(Sum)
        End If
    End Sub
#End Region

#Region "MasterRowGetRelationCount"
    ''' <summary>
    ''' Controla el numero de relaciones de lsub-rejilla de informacion de datos de reiteracion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgcvGlosaPortfolioGlosada_MasterRowGetRelationCount(sender As Object, e As MasterRowGetRelationCountEventArgs) Handles INDgcvGlosaPortfolioGlosada.MasterRowGetRelationCount
        e.RelationCount = 0
    End Sub
#End Region

#Region "RowCellStyle"

    ''' <summary>
    ''' Handle para aplicar color sobre la celdas de agregar datos de la rejilla de detalle de factura 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgcvInvoiceDetailGrid_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles INDgcvInvoiceDetailGrid.RowCellStyle
        If e.Column.FieldName = "MovimientoAux.ValueGlosado" Or e.Column.FieldName = "MovimientoAux.CodeGlosaId" Or e.Column.FieldName = "MovimientoAux.ResponsibleId" Or e.Column.FieldName = "MovimientoAux.RationaleGlosa" Then
            e.Appearance.BackColor = System.Drawing.Color.LightSkyBlue
            e.Appearance.BackColor2 = System.Drawing.Color.LightSkyBlue
            e.Appearance.ForeColor = System.Drawing.Color.DarkSlateGray
        End If
    End Sub

    ''' <summary>
    ''' Handle para aplicar color sobre la celdas de agregar datos de la rejilla de qx 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgcvInvoiceDetailQXGrid_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles INDgcvInvoiceDetailQXGrid.RowCellStyle
        If e.Column.FieldName = "MovimientoAux.ValueGlosado" Or e.Column.FieldName = "MovimientoAux.CodeGlosaId" Or e.Column.FieldName = "MovimientoAux.ResponsibleId" Or e.Column.FieldName = "MovimientoAux.RationaleGlosa" Then
            e.Appearance.BackColor = System.Drawing.Color.LightSkyBlue
            e.Appearance.BackColor2 = System.Drawing.Color.LightSkyBlue
            e.Appearance.ForeColor = System.Drawing.Color.DarkSlateGray
        End If
    End Sub
#End Region

#Region "KeyUp"
    ''' <summary>
    ''' Controla la posicion del foco Sobre la rejilla de detalles de factura
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgcvInvoiceDetailGrid_KeyUp(sender As Object, e As KeyEventArgs) Handles INDgcvInvoiceDetailGrid.KeyUp
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then

            Dim view As GridView = sender
            Dim Column = view.FocusedColumn()
            If Column.Name = "ClValueGlosa" Then
                Dim val = Me.INDgcvInvoiceDetailGrid.GetFocusedValue
                If val = 0 And val IsNot Nothing Then
                    If NextHaveqx() = True Then
                        INDgcvInvoiceDetailGrid.FocusedRowHandle = INDgcvInvoiceDetailGrid.FocusedRowHandle + 1
                        INDgcvInvoiceDetailGrid.ExpandMasterRow(INDgcvInvoiceDetailGrid.FocusedRowHandle)
                        PositionFocusQX(0, Me.ClqxValueGlosa)
                    Else
                        PositionFocus(1, Me.ClValueGlosa)
                    End If
                    'PositionFocus(1, Me.ClValueGlosa)
                Else
                    PositionFocus(2, Me.ClConcept)
                End If
            ElseIf Column.Name = "ClConcept" Then
                PositionFocus(2, Me.ClResponsible)
            ElseIf Column.Name = "ClResponsible" Then
                PositionFocus(2, Me.ClComment)
            ElseIf Column.Name = "ClComment" Then
                PositionFocus(2, Me.ClsaveMovementGlosa)
            Else
                PositionFocus(2, ClValueGlosa)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Controla la posicion del foco Sobre la rejilla de detalles de factura qx
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgcvInvoiceDetailQXGrid_KeyUp(sender As Object, e As KeyEventArgs) Handles INDgcvInvoiceDetailQXGrid.KeyUp
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            Dim view As GridView = sender
            Dim mainViewDetail As GridView = INDgcvInvoiceDetailGrid
            Dim detailViewqx As GridView = INDgcInvoiceDetailGrid.FocusedView ' TryCast(mainViewDetail.GetDetailView(mainViewDetail.FocusedRowHandle, mainViewDetail.GetRelationIndex(mainViewDetail.FocusedRowHandle, "GlosaInvoiceDetailQX")), GridView)
            If detailViewqx IsNot Nothing Then
                Dim Column = detailViewqx.FocusedColumn()
                If Column IsNot Nothing Then
                    If Column.Name = "ClqxValueGlosa" Then
                        Dim val = view.GetFocusedValue
                        If val = 0 And val IsNot Nothing Then
                            Dim tmpFocus As Integer = detailViewqx.FocusedRowHandle
                            tmpFocus = tmpFocus + 1
                            Dim RegisterNext = detailViewqx.GetRow(tmpFocus)
                            If RegisterNext Is Nothing Then
                                If NextHaveqx() Then
                                    GoAnotherExpandMasterQx()
                                Else
                                    INDgcvInvoiceDetailGrid.CollapseMasterRow(INDgcvInvoiceDetailGrid.FocusedRowHandle)
                                    PositionFocus(1, Me.ClValueGlosa)
                                End If
                            Else
                                PositionFocusQX(1, Me.ClqxValueGlosa)
                            End If
                            '                            PositionFocusQX(1, Me.ClqxValueGlosa)
                        Else
                            PositionFocusQX(2, Me.ClqxConcept)
                        End If
                    ElseIf Column.Name = "ClqxConcept" Then
                        PositionFocusQX(2, Me.ClqxResponsible)
                    ElseIf Column.Name = "ClqxResponsible" Then
                        PositionFocusQX(2, Me.ClqxComment)
                    ElseIf Column.Name = "ClqxComment" Then
                        PositionFocusQX(2, Me.ClqxsaveMovementGlosa)
                    Else
                        PositionFocusQX(2, ClqxValueGlosa)
                    End If
                End If
            End If
        End If
    End Sub
#End Region

#Region "CustomRowCellEdit"

    ''' <summary>
    ''' evento sobre la rejilla de detalle que modific los repositorios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgcvInvoiceDetailGrid_CustomRowCellEdit(sender As Object, e As CustomRowCellEditEventArgs) Handles INDgcvInvoiceDetailGrid.CustomRowCellEdit
        Dim view = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
        Dim data = CType(view.GetRow(e.RowHandle), GlosaInvoiceDetail)
        If data IsNot Nothing Then
            If data.GlosaInvoiceDetailQX IsNot Nothing AndAlso data.GlosaInvoiceDetailQX.Count > 0 Then
                If e.Column Is Me.ClseeDetail OrElse e.Column Is Me.ClsaveMovementGlosa Then
                    Dim rep As New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
                    rep.Buttons.Clear()
                    rep.Buttons.Add(New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph))
                    rep.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
                    e.RepositoryItem = rep
                End If
            End If
        End If
    End Sub

    Private Sub TabbedControlGroup1_SelectedPageChanged(sender As Object, e As DevExpress.XtraLayout.LayoutTabPageChangedEventArgs) Handles TabbedControlGroup1.SelectedPageChanged
        If e.Page.Name = INDLcgNoNormative.Name Then
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
        Else
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
        End If
    End Sub

    Private Sub INDgdvObjections_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles INDgcvSeeMovement.CustomColumnDisplayText
        If e.Column.Name = INDColNormative.Name Then
            e.DisplayText = IIf(e.Value, "Normativo", "No normativo")
        End If
    End Sub

    Private Sub BarraBotones_Click_ImportarInformacion() Handles BarraBotones.Click_ImportarInformacion
        Importar()
    End Sub

    Private Async Sub Importar()
        Using frm As New FrmNoNormativeConceptModal()
            frm.GlosaObjectionsReceptionD = TryCast(INDgcvObjetions.GetRow(INDgcvObjetions.FocusedRowHandle), GlosaObjectionsReceptionD)
            frm.Permissions = BarraBotones.PermissionsForm
            frm.InvoiceNumbertmp = InvoiceNumbertmp
            frm.Balance = balance
            AddHandler frm.OnGlosasInvoiceDetailAcepted, Sub(e)
                                                             INDGcNoNormative.DataSource = e
                                                             INDGcNoNormative.RefreshDataSource()
                                                         End Sub
            Dim trasparent As New FrmTransparent(frm, False)
            trasparent.ShowDialog(Me)
            INDGcNoNormative.DataSource = (Await Model.ListGlosaInvoiceDetailByInvoiceNumberReiterationNoNormative(InvoiceNumbertmp, "RE")).ObjectEmbbeded
        End Using
    End Sub
#End Region




#End Region

End Class