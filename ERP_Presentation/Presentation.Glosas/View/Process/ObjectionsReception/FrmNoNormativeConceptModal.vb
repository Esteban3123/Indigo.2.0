Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Glosas.MVP
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraEditors

Public Class FrmNoNormativeConceptModal

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
    ''' foco sobre la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Dim _indexFocus As Integer = 0
    ''' <summary>
    ''' Variable que se utiliza para instanciar el modelo-
    ''' </summary>
    Dim Model As MObjectionsReception
    ''' <summary>
    ''' variable para almacenar el id del concepto cuando cambia en el repositorio de la rejilla de detalle de factura
    ''' </summary>
    ''' <remarks></remarks>
    Dim SpecificConceptId As Integer
    Public Property Permissions As Dictionary(Of Integer, String)
    Public Property GlosaObjectionsReceptionD As GlosaObjectionsReceptionD
    Public Event OnGlosasInvoiceDetailAcepted(e As List(Of GlosaInvoiceDetail))

    Private Sub FrmNoNormativeConceptModal_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Close()
        End If
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Model = Nothing
    End Sub

    Private Async Sub FrmNoNormativeConceptModal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Model = New MObjectionsReception(Me.Tag)
        ToolBar.Hide()
        LoadInformation()
        INDgleConcept.DataSource = Await Model.ListConceptsGlosa("4")
        BarraBotones.PermissionsForm = Permissions
        BarraBotones.PrepareToolbar(Presentation.Controls.eAction.OnlySave)
    End Sub

    Private Async Sub LoadInformation()
        Using model As New MObjectionsReception("")
            INDgcvInvoiceDetailGrid.ShowLoadingPanel()
            Dim resultlist = Await model.ListGlosaInvoiceDetailByInvoiceNumberReiterationWithOutMovementsAsync(GlosaObjectionsReceptionD.InvoiceNumber, "RE")
            Dim invoices As List(Of GlosaInvoiceDetail) = resultlist.ObjectEmbbeded
            INDgleResponsible.DataSource = Await model.ListResponsiblesAll()
            INDgcInvoiceDetailGrid.DataSource = invoices
            INDgcvInvoiceDetailGrid.HideLoadingPanel()

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
        End Using
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
    End Sub

    ''' <summary>
    '''  Establece la posicion del foco en la rejilla de detalles de factura qx
    ''' </summary>
    ''' <param name="Inicial"></param>
    ''' <param name="tmpColumn"></param>
    ''' <remarks></remarks>
    Private Sub PositionFocusQX(ByVal Inicial As Integer, ByVal tmpColumn As DevExpress.XtraGrid.Columns.GridColumn)
        Dim mainViewDetail As GridView = INDgcvInvoiceDetailGrid
        Dim detailViewqx As GridView = INDgcInvoiceDetailGrid.FocusedView
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
            detailViewqx.FocusedColumn = detailViewqx.Columns(tmpColumn.FieldName)
            detailViewqx.ShowEditor()
        End If
    End Sub

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

    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        If MessageIndigo.Show("¿Está seguro que desea conservar los cambios?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            RaiseEvent OnGlosasInvoiceDetailAcepted(CType(INDgcInvoiceDetailGrid.DataSource, List(Of GlosaInvoiceDetail)) _
                                                    .FindAll(Function(m) m.MovimientoAux.ValueGlosado > 0 AndAlso m.MovimientoAux.CodeGlosaId > 0 AndAlso m.MovimientoAux.ResponsibleId > 0))
            Guardar()
            DialogResult = DialogResult.OK
        End If
    End Sub

    ''' <summary>
    ''' Registra un mensaje en el visor de eventos
    ''' </summary>
    ''' <param name="Icono">Icono segun el tipo de mensaje</param>
    ''' <value>Mensaje a registrar</value>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
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

    Private Sub BtnSaveMovementGlosa_Click(sender As Object, e As EventArgs) Handles BtnSaveMovementGlosa.Click
        Dim mainView2 As GridView = INDgcvInvoiceDetailGrid
        Dim detailView3 As GridView = INDgcInvoiceDetailGrid.FocusedView 'TryCast(mainView2.GetDetailView(mainView2.FocusedRowHandle, mainView2.GetRelationIndex(mainView2.FocusedRowHandle, "GlosaInvoiceDetailQX")), GridView)
        If detailView3 IsNot Nothing Then
            If detailView3.Name = INDgcvInvoiceDetailQXGrid.Name Then
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

    ''' <summary>
    ''' Guarda el registro de objecion desde la rejilla
    ''' </summary>
    Public Async Sub GuardarGlosaMovement(GlosaQx As Boolean)
        Try
            If GlosaQx Then
                '   Dim mainViewDetail As GridView = INDgcvInvoiceDetailGrid
                Dim detailViewQx As GridView = INDgcInvoiceDetailGrid.FocusedView 'TryCast(mainViewDetail.GetDetailView(mainViewDetail.FocusedRowHandle, mainViewDetail.GetRelationIndex(mainViewDetail.FocusedRowHandle, "GlosaInvoiceDetailQX")), GridView)
                Me._indexFocus = detailViewQx.FocusedRowHandle
            Else
                Me._indexFocus = INDgcvInvoiceDetailGrid.FocusedRowHandle
            End If

            Dim ListMovement As New List(Of GlosaMovementGlosa)
            Dim objGlosa As GlosaMovementGlosa = AssigningValidMovementGlosa(GlosaQx)

            If objGlosa.CodeGlosa > 0 Then
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
                        End If

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

                    'CalcularValueGlosado()
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
    ''' Sumatoria de todos los movimientos principales de la factura
    ''' </summary>
    ''' <remarks></remarks>
    Private sumValueGLosadoTotal As Decimal

    ''' <summary>
    ''' saldo
    ''' </summary>
    ''' <remarks></remarks>
    Public Property Balance As Decimal

    ''' <summary>
    ''' Acumula el valor glosado total
    ''' </summary>
    ''' <remarks></remarks>
    Dim AcumulativoValorGlosado As Decimal

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
            Dim detailView3 As GridView = INDgcInvoiceDetailGrid.FocusedView
            If detailView3 IsNot Nothing Then
                If detailView3.FocusedRowHandle > -1 Then
                    AuxInvoiceDetailQX = TryCast(detailView3.GetRow(detailView3.FocusedRowHandle), GlosaInvoiceDetailQX)

                    If AuxInvoiceDetailQX IsNot Nothing AndAlso AuxInvoiceDetailQX.Id > 0 Then
                        If AuxInvoiceDetailQX.MovimientoAux.ValueGlosado > AuxInvoiceDetailQX.InvoicedValue Then
                            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(ValorGlosaMayorValorFactura, Presentation.Base.Eform.RegisterObjection), ManageDecimalsFun(AuxInvoiceDetailQX.InvoicedValue))
                            Return New GlosaMovementGlosa
                        Else
                            Dim listMensaje As New List(Of String)

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
                                If AcumulativoValorGlosado <= AuxInvoiceDetailQX.InvoicedValue Then
                                    Glosa = CreateMovementGlosa(AuxInvoiceDetailQX.InvoiceDetailId, AuxInvoiceDetailQX.Id, AuxInvoiceDetailQX.MovimientoAux.CodeGlosaId, AuxInvoiceDetailQX.MovimientoAux.ResponsibleId, AuxInvoiceDetailQX.MovimientoAux.ValueGlosado, AuxInvoiceDetailQX.MovimientoAux.RationaleGlosa, True)
                                Else
                                    Glosa = CreateMovementGlosa(AuxInvoiceDetailQX.InvoiceDetailId, AuxInvoiceDetailQX.Id, AuxInvoiceDetailQX.MovimientoAux.CodeGlosaId, AuxInvoiceDetailQX.MovimientoAux.ResponsibleId, AuxInvoiceDetailQX.MovimientoAux.ValueGlosado, AuxInvoiceDetailQX.MovimientoAux.RationaleGlosa, False)
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
                        If AcumulativoValorGlosado <= AuxInvoiceDetail.ValorEntidad Then
                            Glosa = CreateMovementGlosa(AuxInvoiceDetail.Id, String.Empty, AuxInvoiceDetail.MovimientoAux.CodeGlosaId, AuxInvoiceDetail.MovimientoAux.ResponsibleId, AuxInvoiceDetail.MovimientoAux.ValueGlosado, AuxInvoiceDetail.MovimientoAux.RationaleGlosa, True) 'si no supera saldo de factura van como principal
                        Else
                            Glosa = CreateMovementGlosa(AuxInvoiceDetail.Id, String.Empty, AuxInvoiceDetail.MovimientoAux.CodeGlosaId, AuxInvoiceDetail.MovimientoAux.ResponsibleId, AuxInvoiceDetail.MovimientoAux.ValueGlosado, AuxInvoiceDetail.MovimientoAux.RationaleGlosa, False) ' si supera saldo de factura la marcamos como no principal
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
    ''' Funcion para cada vez que asignen un valor a los label's (Manjo de decimales)
    ''' </summary>
    ''' <param name="Value"></param>
    ''' <returns></returns>
    Private Function ManageDecimalsFun(Value As Decimal)
        'If _parameterGlosas.ManageDecimals = False Then
        '    Return Value.MoneyFormat(numberDecimal:=0)
        'Else
        Return Value.MoneyFormat()
        'End If
    End Function

    ''' <summary>
    ''' Handle cuando se selecciona un concepto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgleConcept_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleConcept.EditValueChanged
        Dim gridLookUpConcept As GridLookUpEdit = CType(sender, GridLookUpEdit)
        Dim ObjConcept As Domain.Entities.ConceptGlosas = gridLookUpConcept.GetSelectedDataRow()
        SpecificConceptId = ObjConcept.Id
    End Sub

    ''' <summary>
    ''' Crea Un Objeto Movimieno glosa general
    ''' </summary>
    ''' <returns>retorna un objeto movimineto glosa</returns>
    ''' <remarks></remarks>
    Private Function CreateMovementGlosa(_InvoiceDetailId As String, _InvoiceDetailIdQx As String, _codeGlosa As String, _ResponsibleId As String, _valueGlosado As Decimal, RationaleGlosa As String, MainGlosa As Boolean) As GlosaMovementGlosa
        Dim _ObjRegisterObjection As New GlosaMovementGlosa
        With _ObjRegisterObjection
            .ValueGlosado = _valueGlosado
            .InvoiceNumber = Me._InvoiceNumbertmp
            .InvoiceDetailId = _InvoiceDetailId
            .MainGlosa = MainGlosa
            .RationaleDateGlosa = Date.Now
            If _InvoiceDetailIdQx <> String.Empty Then
                .InvoiceDetailIdQX = _InvoiceDetailIdQx
            End If
            .ConceptGlosas = New Domain.Entities.ConceptGlosas()
            With .ConceptGlosas
                .Id = SpecificConceptId
            End With
            .CodeGlosa = _codeGlosa
            .RationaleGlosa = RationaleGlosa
            .State = 1 'la envio como estado 1 Pendiente Evaluar Glosa
            .TempState = 1 'la envio como estado 1 Pendiente Evaluar Glosa
            .Responsible1 = New Domain.Entities.Responsible()
            With .Responsible1
                .Id = _ResponsibleId
            End With
            .TypeConcept = 1
        End With
        Return _ObjRegisterObjection
    End Function

    Private Async Sub Guardar()
        Try
            Using Model As New MRegisterObjection
                Dim result As New ActionResult

                If INDgcInvoiceDetailGrid.DataSource.count > 0 Then
                    'If GeneralGlosa = False Then
                    AsyncLoader(True)
                    'If _Reiteration = True Then
                    result = Await Model.SaveReiterationMovementGlosa(INDgcInvoiceDetailGrid.DataSource)
                    'Else
                    '    If fnValidateMainRegister() = True Then
                    '        result = Await Model.SaveMovementGlosa(INDgcInvoiceDetailGrid.DataSource)
                    '    End If
                    'End If
                    AsyncLoader(False)
                    'Else
                    '    If GlosaSelectionqx = True Then
                    '        ObjectionGeneralqx()
                    '    Else
                    '        ObjectionGeneral()
                    '    End If
                    '    AsyncLoader(True)
                    '    result = Await Model.SaveMovementGlosa(ListRegisterObjetionsGeneral)
                    '    AsyncLoader(False)
                    'End If
                    If result IsNot Nothing Then
                        'ListRegisterObjetionsGeneral = New List(Of GlosaMovementGlosa)
                        'If result.StateResult = True Then

                        '    If GeneralGlosa = False Then
                        '        If Me.InvoiceDetaildId AndAlso Me.InvoiceDetaildQXId > 0 Then
                        '            AsyncLoader(True)
                        '            Me.MovementGlosaSource = Await Model.ListMovementGlosaQx(Me.InvoiceDetaildId, Me.InvoiceDetaildQXId)
                        '            AsyncLoader(False)
                        '        Else
                        '            AsyncLoader(True)
                        '            Me.MovementGlosaSource = Await Model.ListMovementGlosa(Me.InvoiceDetaildId)
                        '            AsyncLoader(False)
                        '        End If
                        '    Else
                        '        Me.MovementGlosaSource = New List(Of GlosaMovementGlosa)
                        '    End If
                        '    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
                        'Else
                        '    If result.MessageResult IsNot Nothing AndAlso result.MessageResult(0) = "-999" Then
                        '        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(Eresources.ComunesErrorConcurrencia)
                        '    Else
                        '        If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                        '            Dim strMensaje As String = String.Empty
                        '            For Each itemMensaje As String In result.MessageResult
                        '                strMensaje += itemMensaje + Environment.NewLine
                        '            Next
                        '            Mensaje(EeventViewerImages.Informacion) = strMensaje
                        '            MovementGlosaSource = New List(Of GlosaMovementGlosa)
                        '        End If
                        '    End If
                        'End If
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Informacion) = ex.Message.ToString
            Throw ex
        End Try
    End Sub
End Class