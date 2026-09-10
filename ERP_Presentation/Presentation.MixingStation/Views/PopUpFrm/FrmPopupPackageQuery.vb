'***********************************************************************
' Assembly         : Presentation.MixingStation
' Author           : Yoe Andres Cardenas
' Created          : 13/06/2018
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.MixingStation.MVP

#End Region

Public Class FrmPopupPackageQuery
#Region "EVENTS"
	''' <summary>
	''' evento para agregar un producto
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	''' <remarks></remarks>
	Public Event AddPackageDetail(sender As Object, e As AddProductPackageDetailEventArgs)
#End Region

#Region "Variables"
	''' <summary>
	''' constante con el nombre del modulo
	''' </summary>
	Private Const MODULE_NAME = "MixingStation"

    '''' <summary>
    '''' Obtiene o establece el producto seleccionado
    '''' </summary>
    'Dim _productEntity As InventoryProduct

    '''' <summary>
    '''' Objeto que establece el producto a agregar
    '''' </summary>
    '''' <remarks></remarks>
    'Dim _packageDetailEntity As PackageDetail

    ''' <summary>
    ''' Representa la entidad de Package
    ''' </summary>
    Dim _packageEntity As Package

    '''' <summary>
    '''' Obtiene o establece el listado de los paquetes creados
    '''' </summary>
    '''' <remarks></remarks>
    'Private ListPackage As Domain.Entities.TrackableCollection(Of Package) = New Domain.Entities.TrackableCollection(Of Package)

    ''' <summary>
    ''' Obtiene o establece el listado de los paquetes creados
    ''' </summary>
    ''' <remarks></remarks>
    Private _ListPackage As List(Of PackageDto) '= New Domain.Entities.TrackableCollection(Of Package)

    ''' <summary>
    ''' Define si el form es solo para visualizar
    ''' </summary>
    ''' <remarks></remarks>
    Dim _onlyRead As Boolean

    '''' <summary>
    '''' Variable que inicia el id del producto en 0
    '''' </summary>
    'Dim _productId As Integer = 0

    ''' <summary>
    ''' id de la unidad operativa
    ''' </summary>
    ''' <remarks></remarks>
    Dim _operatingUnitId As Integer

    '''' <summary>
    '''' listado del detalle del paquete para validar que los productos no se repitan con la misma fuente
    '''' </summary>
    '''' <remarks></remarks>
    'Dim _listPackageDetailValidation As List(Of PackageDetail)

#End Region

#Region "Properties"
    ''' <summary>
    ''' Propiedad para establecer el id de la unidad operativa
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property operatingUnitId As Integer
		Set(value As Integer)
			_operatingUnitId = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece el paquete seleccionado
	''' </summary>
	Public Property PackageId As String
		Get
			Return IIf(INDslePackage.EditValue Is Nothing, Nothing, INDslePackage.EditValue)
		End Get
		Set(value As String)
			INDslePackage.EditValue = value
		End Set
	End Property

    '''' <summary>
    '''' propiedad para para pasar el listado del detalle del paquete
    '''' </summary>
    '''' <value></value>
    '''' <remarks></remarks>
    'Public WriteOnly Property ListPackageDetailValidation As List(Of PackageDetail)
    '	Set(value As List(Of PackageDetail))
    '		If value IsNot Nothing Then
    '			_listPackageDetailValidation = New List(Of PackageDetail)(value.ToArray())
    '		End If
    '	End Set
    'End Property

    '''' <summary>
    '''' propiedad publica para pasar el registro que se va a editar
    '''' </summary>
    '''' <value></value>
    '''' <remarks></remarks>
    'Public WriteOnly Property PackageDetailEdit As PackageDetail
    '	Set(value As PackageDetail)
    '		_packageDetailEntity = value
    '	End Set
    'End Property

    ''' <summary>
    ''' propiedad para para pasar el listado del detalle del paquete
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ListPackage As List(Of PackageDto)
        Set(value As List(Of PackageDto))
            If value IsNot Nothing Then
                _ListPackage = value
            End If
        End Set
    End Property
#End Region

#Region "Methods"

    ''' <summary>
    ''' Carga los datos del producto en el formulario
    ''' </summary>
    Private Function LoadControls(Optional packageDetailTmp As List(Of PackageDetail) = Nothing) As Task
        'If _listPackageDetailValidation IsNot Nothing Then
        '	'' Buscar lista de paquetes que cumplen con las condiciones de duplicidad de componentes y cantidades
        '	Using model As New MPackage(Me.Tag)
        '		INDslePackage.Datasource = model.ListDuplicatePackage(_listPackageDetailValidation)
        '	End Using
        If _ListPackage IsNot Nothing Then
            INDslePackage.Datasource = _ListPackage
        Else
            Exit Function
		End If
	End Function

	''' <summary>
	''' Limpia los controles para agregar un producto nuevo
	''' </summary>
	''' <remarks></remarks>
	Private Sub CleanControls()
        '_productEntity = Nothing
        INDslePackage.EditValue = String.Empty
		INDGvPackageDetail = Nothing

		INDslePackage.Focus()
	End Sub

	''' <summary>
	''' Propiedad para enviar mensajes al visor de eventos
	''' </summary>
	''' <param name="Icono"></param>
	Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
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

#Region "Events"

	''' <summary>
	''' Carga el popup al iniciar
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	''' <remarks></remarks>
	Private Async Sub FrmPopupPackageQuery_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		BarraBotones.OperatingUnitVisible = False
		BarraBotones.PrepareToolbar(eAction.OnlyFind)
		BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
		BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
		BarraBotones.StatusRecordVisible = True

		LoadControls()
	End Sub

#End Region

#Region "Click"
	''' <summary>
	''' Evento que se dispara al dar click para mostrar el detalle del paquete seleccionado
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub INDBtnDetail_Click(sender As Object, e As EventArgs) Handles INDBtnDetail.Click
		Using Model As New MPackage(CStr(Me.Tag))
			AsyncLoader(True)
			Dim resultOperation = Model.GetPackage(PackageId)
			_packageEntity = resultOperation.ObjectEmbbeded
			If _packageEntity IsNot Nothing AndAlso _packageEntity.Id > 0 Then
				INDGcPackageDetail.DataSource = _packageEntity.PackageDetail
			End If
			AsyncLoader(False)
		End Using
	End Sub

    '''' <summary>
    '''' Evento que se dispara al dar click para cerrar el formulario
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    'Private Sub FrmPopupPPackageQuery_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    '    If _productEntity IsNot Nothing Then
    '        If Not MessageIndigo.Show("Al cerrar el formulario se perderá la información que habia registrado", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
    '            e.Cancel = True
    '        End If
    '    End If
    'End Sub

    ''' <summary>
    ''' Evento que se dispara al dar click en Deshacer de la barra de botones
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
    End Sub

#End Region

#Region "KwyDown"

    ''' <summary>
    ''' Evento que se dispara al oprimir escape para cerrar el popup del listado de productos
    ''' </summary>
    Private Sub FrmPopupPackageQuery_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub





#End Region

End Class