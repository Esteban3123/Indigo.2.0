'***********************************************************************
' Assembly         : Presentation.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/02/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ComponentModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base
Imports Presentation.MixingStation.MVP

#End Region

Public Class FrmRequestUnitDoseInventoryDetail

#Region "Event"

    ''' <summary>
    ''' Evento para agregar un detalle
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddRequestUnitDoseInventoryDetailArgs(sender As Object, e As AddRequestUnitDoseInventoryDetail)

#End Region

#Region "Variables"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Dim Presenter As PRequestUnitDoseInventory

    ''' <summary>
    ''' Permite saber si se esta editando el detalle
    ''' </summary>
    Public EditModeDetail As Boolean

    ''' <summary>
    ''' Detalle
    ''' </summary>
    Public RequestUnitDoseInventoryDetail As RequestUnitDoseInventoryDetail

    ''' <summary>
    ''' Se utiliza para validar
    ''' </summary>
    Public ListRequestUnitDoseInventoryDetailCompare As List(Of RequestUnitDoseInventoryDetail)

    ''' <summary>
    ''' Identifica la clase del tipo de dosis unitaria
    ''' </summary>
    Private ItemType As Integer

    ''' <summary>
    ''' Identifica el tipo de dosis unitaria
    ''' </summary>
    Private unitDoseTypeXpo As MixinStationUnitDoseTypeXpo

#End Region

#Region "Properties"

    ''' <summary>
    ''' Slide de mensajes
    ''' </summary>
    ''' <param name="Icono"></param>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Carga los datos del producto en el formulario
    ''' </summary>
    Private Sub LoadControls()
        With RequestUnitDoseInventoryDetail
            INDsleUnitDoseType.EditValue = .UnitDoseTypeId
            INDsleUnitDoseType.Properties.NullText = .UnitDoseTypeCodeName

            If .ATCId IsNot Nothing Then
                INDsleATC.EditValue = .ATCId
                INDsleATC.Properties.NullText = .ItemDescription
            End If

            If .PackageId IsNot Nothing Then
                INDslePackage.EditValue = .PackageId
                INDslePackage.Properties.NullText = .ItemDescription
            End If

            INDseQuantity.EditValue = .Quantity

            If .AdministrationRouteId IsNot Nothing Then
                INDsleAdministrationRoute.EditValue = .AdministrationRouteId
                INDsleAdministrationRoute.Properties.NullText = .AdministrationRouteCodeName
            End If
        End With
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Carga el popup al iniciar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmRequestUnitDoseInventoryDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ToolBar.Visible = False
        Presenter = New PRequestUnitDoseInventory()

        If EditModeDetail Then
            LoadControls()
        End If
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Define el foco cuando el control esta activo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmRequestUnitDoseInventoryDetail_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleUnitDoseType.Focus()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Agrega el producto al listado del paquete
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDbtnAdd.Click
        If Not ValidateControls() Then
            Exit Sub
        End If

        'Se valida que el item que se va a agregar no exista en el formulario principal
        If ListRequestUnitDoseInventoryDetailCompare IsNot Nothing AndAlso ListRequestUnitDoseInventoryDetailCompare.Count > 0 Then
            If ItemType = 1 Then 'Medicamento
                If (From x In ListRequestUnitDoseInventoryDetailCompare Where x.ATCId = INDsleATC.EditValue Select x).Count > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "El item seleccionado ya existe en la rejilla principal"
                    INDsleATC.Focus()
                    Exit Sub
                End If
            ElseIf ItemType = 2 Then 'Paquete
                If (From x In ListRequestUnitDoseInventoryDetailCompare Where x.PackageId = INDslePackage.EditValue Select x).Count > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "El item seleccionado ya existe en la rejilla principal"
                    INDsleUnitDoseType.Focus()
                    Exit Sub
                End If
            End If
        End If

        If EditModeDetail = False Then
            RequestUnitDoseInventoryDetail = New RequestUnitDoseInventoryDetail
        End If
        With RequestUnitDoseInventoryDetail
            .UnitDoseTypeId = INDsleUnitDoseType.EditValue
            .UnitDoseTypeCodeName = INDsleUnitDoseType.Text

            .ItemType = ItemType
            .ItemTypeName = If(ItemType = 1, "Medicamento", "Paquete")

            .ATCId = Nothing
            If INDlyItemATC.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .ATCId = INDsleATC.EditValue
                .ItemDescription = INDsleATC.Text
            End If

            .PackageId = Nothing
            If INDlyItemPackage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .PackageId = INDslePackage.EditValue
                .ItemDescription = INDslePackage.Text
            End If

            .Quantity = INDseQuantity.EditValue

            If INDlyItemAdministrationRoute.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .AdministrationRouteId = INDsleAdministrationRoute.EditValue
                .AdministrationRouteCodeName = INDsleAdministrationRoute.Text
            End If
        End With

        Dim args As New AddRequestUnitDoseInventoryDetail
        args.RequestUnitDoseInventoryDetail = RequestUnitDoseInventoryDetail
        args.EditMode = EditModeDetail
        RaiseEvent AddRequestUnitDoseInventoryDetailArgs(Nothing, args)
        Me.Close()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al oprimir escape para cerrar el popup del listado de productos
    ''' </summary>
    Private Sub FrmRequestUnitDoseInventoryDetail_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleManufacturer control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
    ''' INDSleMeasurementUnit
    Private Sub INDslePackage_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDslePackage.QueryPopUp
        If INDslePackage.Properties.DataSource Is Nothing Then
            INDslePackage.Properties.DataSource = Presenter.InitializePackageByUnitDoseTypeId(INDsleUnitDoseType.EditValue)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de medicamentos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleATC_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleATC.QueryPopUp
        If INDsleATC.Properties.DataSource Is Nothing Then
            INDsleATC.Properties.DataSource = Presenter.InitializeATC()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de insumos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleUnitDoseType_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleUnitDoseType.QueryPopUp
        If INDsleUnitDoseType.Properties.DataSource Is Nothing Then
            INDsleUnitDoseType.Properties.DataSource = Presenter.InitializeUnitDoseType()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Abre el form de tipo dosis unitarias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleUnitDoseType_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleUnitDoseType.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(2067, Nothing, True)
            INDsleUnitDoseType.Properties.DataSource = Presenter.InitializeUnitDoseType()
        End If
    End Sub

    ''' <summary>
    ''' Abre el form de atc
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleATC_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleATC.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(343, Nothing, True)
            INDsleATC.Properties.DataSource = Presenter.InitializeATC()
        End If
    End Sub

    ''' <summary>
    ''' Abre el form de paquetes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDslePackage_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDslePackage.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(2065, Nothing, True)
            INDslePackage.Properties.DataSource = Presenter.InitializePackage()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Cuando cambia el valor del control de tipo dosis unitaria
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleUnitDoseType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleUnitDoseType.EditValueChanged
        If INDsleUnitDoseType.EditValue IsNot Nothing Then
            INDslePackage.Properties.DataSource = Nothing
            INDsleAdministrationRoute.Properties.DataSource = Nothing
            INDsleAdministrationRoute.EditValue = Nothing
            unitDoseTypeXpo = Presenter.GetUnitDoseTypeById(INDsleUnitDoseType.EditValue)

            If unitDoseTypeXpo IsNot Nothing Then
                If {EUnitDoseTypeClass.Repackaging, EUnitDoseTypeClass.Refilling}.Contains(unitDoseTypeXpo.MSClass) Then 'Reempaque, Reenvase
                    ItemType = 1
                    INDlyItemATC.ShowLayout()
                    INDlyItemPackage.HideLayout()
                    INDlyItemAdministrationRoute.HideLayout()

                Else 'NPT, Antibioticoterapia, Citostatico, Magistral, Otros estériles
                    ItemType = 2
                    INDlyItemATC.HideLayout()
                    INDlyItemPackage.ShowLayout()
                    INDlyItemAdministrationRoute.HideLayout()

                    If {EUnitDoseTypeClass.Antibiotics, EUnitDoseTypeClass.Cytostatic, EUnitDoseTypeClass.Magistral, EUnitDoseTypeClass.OtherSterile}.Contains(unitDoseTypeXpo.MSClass) Then
                        INDlyItemAdministrationRoute.ShowLayout()
                    End If
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Cuando cambia el valor del control de paquete
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDslePackage_EditValueChanged(sender As Object, e As EventArgs) Handles INDslePackage.EditValueChanged
        If CInt(INDslePackage.EditValue) <= 0 Then Exit Sub

        Dim package = Await Presenter.GetPackageXpoByIdAsync(INDslePackage.EditValue)
        If package Is Nothing Then Exit Sub

        Dim isMagistral = (unitDoseTypeXpo.MSClass = EUnitDoseTypeClass.Magistral)
        Dim routes = GetAdministrationRoutes(package, isMagistral)

        INDsleAdministrationRoute.Properties.DataSource = routes

        If Not EditModeDetail AndAlso routes?.Count = 1 Then
            INDsleAdministrationRoute.EditValue = routes(0)
        End If
    End Sub

    ''' <summary>
    ''' Identifica el tipo de dato que tomara el campo 'Vias de administracion'
    ''' </summary>
    ''' <param name="package"></param>
    ''' <param name="isMagistral"></param>
    ''' <returns></returns>
    Private Function GetAdministrationRoutes(package As MixinStationPackageXpo, isMagistral As Boolean) As IList
        If package Is Nothing Then Return Nothing

        If isMagistral Then
            Return Presenter.InitializeAdministrationRoute()
        Else
            ' Evitar acceso tardío que cause error de sesión
            Dim mainMed = package.MixinStationPackageDetailXpo.FirstOrDefault(Function(x) x.MainMedicine)
            Return mainMed?.AtcId?.MixingStationATCAdministrationRouteXpo?.ToList()
        End If
    End Function

#End Region

#End Region

End Class

Public Class AddRequestUnitDoseInventoryDetail
    Inherits EventArgs

    Property RequestUnitDoseInventoryDetail As RequestUnitDoseInventoryDetail

    Property EditMode As Boolean

End Class