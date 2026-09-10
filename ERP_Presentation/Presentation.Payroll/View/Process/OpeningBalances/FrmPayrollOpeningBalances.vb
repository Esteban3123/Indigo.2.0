Imports Presentation.Controls
Imports Presentation.Base
Imports System.Windows.Forms
Imports Domain.Payroll.Entities
Imports Presentation.Payroll.MVP
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Infrastructure.Data.Xpo

Public Class FrmPayrollOpeningBalances
    Implements IPayrollOpeningBalances

    Dim ListLiquidation As New List(Of Liquidation)

    ''' <summary>
    ''' Indica la moneda oficial
    ''' </summary>
    Private CurrencyAbbreviation As String

#Region "Bar buttons events"

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        AsyncLoader(True)
        Me.BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))

        AsyncLoader(False)

        Me.BarraBotones.PrepareToolbar(eAction.OnlyProcess)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = False

        'Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = True
        Me.BarraBotones.RibbonPageRejillas.Visible = False
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        'Buscar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        'CleanControls()
        'Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
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
        'Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        'CleanControls()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click customizar.
    ''' </summary>
    Private Sub BarraBotones_ClickCustomizar() Handles BarraBotones.ClickCustomizar
        'CustomizationOpen()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        'Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ clic restablecer layout.
    ''' </summary>
    Private Sub BarraBotones_ClicRestablecerLayout() Handles BarraBotones.ClicRestablecerLayout
        'ResetLayout()
    End Sub

    ''' <summary>
    ''' Barra botones boton Confirmar
    ''' </summary>
    Private Async Sub BarraBotones_ClicConfirmar() Handles BarraBotones.ClickConfirmar
        If INDGcEmployeeLiquidation.DataSource IsNot Nothing Then
            Await SaveOpeningBalances()
        End If
    End Sub
#End Region

    Private Sub FrmPayrollOpeningBalances_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        CurrencyAbbreviation = LoadPayrollSettings.CurrencyId.Abbreviation
        SetCurrencyFormat(CurrencyAbbreviation)
    End Sub

    ''' <summary>
    ''' Obtiene los datos de la moneda oficial
    ''' </summary>
    ''' <returns></returns>
    Public Function LoadPayrollSettings() As PayrollSettingsXpo
        Return XpoServiceEx.Instance(indigo.TransactionalContainer).PayrollService.GetCollection(Of PayrollSettingsXpo).FirstOrDefault()
    End Function


    Private Sub INDBtnAddOpenBalance_Click(sender As Object, e As EventArgs) Handles INDBtnAddOpenBalance.Click


        Using formulario As New PopUpPayrollOpeningBalances()

            formulario.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            formulario.ViewModeEditHold = True
            formulario.StartPosition = FormStartPosition.CenterParent
            formulario.CurrencyAbbreviation = CurrencyAbbreviation
            Dim size As System.Drawing.Size
            size.Width = 1000
            size.Height = 750
            formulario.Size = size
            Dim transparent = New FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)

            'If formulario.LiquidationAdd IsNot Nothing Then

            '    If formulario.LiquidationAdd.ContractId > 0 Then

            '        If ListLiquidation.Where(Function(x) x.PayrollDateLiquidated = formulario.LiquidationAdd.PayrollDateLiquidated And x.EmployeeId = formulario.LiquidationAdd.EmployeeId).Count > 0 Then
            '            Mensaje(EeventViewerImages.Advertencia) = "Ya existe agregada con esta Fecha y el mismo empleado"
            '            Return
            '        End If

            '        ListLiquidation.Add(formulario.LiquidationAdd)
            '        INDGcEmployeeLiquidation.DataSource = Nothing
            '        INDGcEmployeeLiquidation.DataSource = ListLiquidation
            '    End If
            '    End If


        End Using
    End Sub

    Private Async Function SaveOpeningBalances() As Task
        Using m As New MPayrollOpeningBalances(MPayrollOpeningBalances.TAG)
            Dim ListLiquidation As List(Of Liquidation) = INDGcEmployeeLiquidation.DataSource
            AsyncLoader(True)
            Dim OpeningResult = Await m.SaveLiquidationOpenBalancesAsync(ListLiquidation)
            AsyncLoader(False)
            If OpeningResult = True Then
                Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesConfirmadoCorrectamente)
                INDGcEmployeeLiquidation.DataSource = Nothing
                ListLiquidation = New List(Of Liquidation)
            Else
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesError)
            End If
        End Using
    End Function


    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.ICrudBase.Mensaje
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

    Public Sub Buscar() Implements ICrudBase.Buscar

    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer

    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    Public Sub Guardar() Implements ICrudBase.Guardar

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    Public Sub Nuevo() Implements ICrudBase.Nuevo

    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch

    End Sub

    Public Property EmployeeXpo As DevExpress.Data.Linq.LinqInstantFeedbackSource Implements IPayrollOpeningBalances.EmployeeXpo

    Public Property EmployeeId As Integer Implements IPayrollOpeningBalances.EmployeeId
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Integer)
            Throw New NotImplementedException()
        End Set
    End Property

    ''' <summary>
    ''' Establece a  los controles la moneda parametrizada
    ''' </summary>
    ''' <param name="_currencyAbbreviation"></param>
    Private Sub SetCurrencyFormat(_currencyAbbreviation As String)
        If String.IsNullOrEmpty(_currencyAbbreviation) Then
            Mensaje(EeventViewerImages.Advertencia) = "La abreviación de la moneda está vacía."
            Exit Sub
        End If
        Dim numberFormat = _currencyAbbreviation.GetNumberFormat()
        changeNumericFormatByCurrency(numberFormat)
        INDColTotalAccrued = Window.Utils.FormatGrid(INDColTotalAccrued, _currencyAbbreviation)
        INDColTotalDeducted = Window.Utils.FormatGrid(INDColTotalAccrued, _currencyAbbreviation)
        INDColTotalPaid = Window.Utils.FormatGrid(INDColTotalAccrued, _currencyAbbreviation)
    End Sub

End Class