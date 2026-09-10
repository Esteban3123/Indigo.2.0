'***********************************************************************
' Assembly         : Presentacion.Base
' Author           : Julian Cardozo
' Created          : 15-04-2011
'
' Last Modified By : Julian Cardozo
' Last Modified On : 21-11-2011
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"

Imports System.Globalization
Imports System.Threading
Imports Infrastructure.CrossCutting.Base
Imports System.Xml
Imports System.IO
Imports System.Text
Imports System.Xml.Serialization
Imports System.Threading.Tasks
Imports System.Net.NetworkInformation
Imports Domain.Security.Entities
Imports System.Reflection
Imports System.Drawing
Imports Infrastructure.CrossCutting.Base.Window

#End Region

''' <summary>
'''Esta clase es la clase base que contiene todo el manejo de localizacion
''' </summary>
Public NotInheritable Class BaseClass
    Public Shared Event EventCloseApplication()
    Public Shared Event HideMDIEvent(ByVal Hide As Boolean)
    Public Shared Event HideSplashScreenEvent()
    Public Shared Event ProcessAsyncEvent(ByVal Form As String, ByVal Process As String, ByVal Status As String)
    Private Declare Auto Function SetProcessWorkingSetSize Lib "kernel32.dll" (ByVal procHandle As IntPtr, ByVal min As Int32, ByVal max As Int32) As Boolean
    Public Shared ListFormHide As New List(Of Integer)()

    ''' <summary>
    ''' Metodo para liberar la memoria no utilizada por la aplicacion
    ''' </summary>
    Shared Sub FreeMemory()
        GC.Collect()
        GC.WaitForPendingFinalizers()
        Dim Memory As Process
        Memory = Process.GetCurrentProcess()
        SetProcessWorkingSetSize(Memory.Handle, -1, -1)
    End Sub

    ''' <summary>
    ''' Obtiene la versión de la aplicación
    ''' </summary>
    ''' <returns>Versión de la aplicación</returns>
    Shared Function GetAppVersion() As Version
        Return My.Application.Info.Version
    End Function

    Public Shared Async Function LoadSessionValuesAsync() As Task(Of SessionValues)
        Return Await Task.Run(AddressOf LoadSingelton)
    End Function

    ''' <summary>
    ''' Metodo para dispara el evento de ocultar el mdi
    ''' </summary>
    ''' <param name="Hide">if set to <c>true</c> [hide].</param>
    Public Shared Sub HideMDI(ByVal Hide As Boolean)
        RaiseEvent HideMDIEvent(Hide)
    End Sub

    ''' <summary>
    ''' Metodo para ocultar el splash
    ''' </summary>
    Public Shared Sub HideSplashScreen()
        RaiseEvent HideSplashScreenEvent()
    End Sub

    ''' <summary>
    ''' Funcion para cargar la singelton
    ''' </summary>
    ''' <returns></returns>
    Private Shared Function LoadSingelton() As SessionValues
        Return SessionValues.Instance
    End Function

    ''' <summary>
    ''' Metodo que devuelve el cursor de carga de indigo
    ''' </summary>
    ''' <returns></returns>
    Public Shared Sub ActiveSplashScreen(ByVal Active As Boolean)
        If Active = True Then

        Else
            RaiseEvent HideSplashScreenEvent()
        End If
    End Sub

    Public Shared Sub CloseApplication()
        RaiseEvent EventCloseApplication()
    End Sub

    Public Shared Sub ProcessAsync(ByVal Form As String, ByVal Process As String, ByVal Status As String)
        RaiseEvent ProcessAsyncEvent(Form, Process, Status)
    End Sub

    Public Shared Sub LoadCulture()
        If LoaderConfigurationFile.ConfigurationFileExists() Then
            Thread.CurrentThread.CurrentUICulture = ConfigurationFile.Instance.Culture
            Thread.CurrentThread.CurrentCulture = ConfigurationFile.Instance.Culture
            'Se deja 2 decimales por defecto en la cultura seleccionada
            Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalDigits = 2
            Thread.CurrentThread.CurrentUICulture.NumberFormat.CurrencyDecimalDigits = 2
            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalDigits = 2
            Thread.CurrentThread.CurrentUICulture.NumberFormat.NumberDecimalDigits = 2
            SessionValues.Instance.Culture = ConfigurationFile.Instance.Culture
        Else
            Thread.CurrentThread.CurrentUICulture = Infrastructure.CrossCutting.Base.Utils.GetDefaultCulture()
            Thread.CurrentThread.CurrentCulture = Infrastructure.CrossCutting.Base.Utils.GetDefaultCulture()
            'Se deja 2 decimales por defecto en la cultura seleccionada
            Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalDigits = 2
            Thread.CurrentThread.CurrentUICulture.NumberFormat.CurrencyDecimalDigits = 2
            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalDigits = 2
            Thread.CurrentThread.CurrentUICulture.NumberFormat.NumberDecimalDigits = 2
            SessionValues.Instance.Culture = Thread.CurrentThread.CurrentUICulture
        End If
    End Sub

    ''' <summary>
    ''' Metodo que devuelve el cursor de carga de indigo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Si falla la carga del cursor personalizado (ej: "Unable to load cursor"), se usa Cursors.WaitCursor como fallback.
    ''' Esto puede ocurrir intermitentemente al cargar cursors .ani desde recursos embebidos en ciertas versiones de Windows.
    ''' </remarks>
    Public Shared Function ChangeCursorIndigo() As System.Windows.Forms.Cursor
        Try
            Dim INDcursorIndigo As New ExtCursors.ExtCursor(My.Resources.CursorVie)
            Return INDcursorIndigo.Cursor
        Catch ex As Exception
            ' Fallback al cursor de espera estándar cuando falla la carga del cursor personalizado
            Return System.Windows.Forms.Cursors.WaitCursor
        End Try
    End Function

    ''' <summary>
    ''' Metodo que devuelve el cursor de default de indigo
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function ChageCursorDefault() As System.Windows.Forms.Cursor
        'Provoca que el cursor quede oculto en el formulario donde se usa
        'Dim INDcursorIndigo As ExtCursors.ExtCursor
        'INDcursorIndigo = New ExtCursors.ExtCursor(My.Resources.CursorBlue)
        'Return INDcursorIndigo.Cursor
        Return System.Windows.Forms.Cursors.Default
    End Function

    ''' <summary>
    ''' Metodo que devuelve el cursor de mano de indigo
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function ChageCursorHand() As System.Windows.Forms.Cursor
        'Provoca que el cursor quede oculto en el formulario donde se usa
        'Dim INDcursorIndigo As ExtCursors.ExtCursor
        'INDcursorIndigo = New ExtCursors.ExtCursor(My.Resources.CursorHandBlue)
        'Return INDcursorIndigo.Cursor
        Return System.Windows.Forms.Cursors.Hand
    End Function

    ''' <summary>
    ''' Funcion Compartida que se utiliza para obtener los mensajes personalizados de
    ''' unos archivos de recurso creados, por el idioma que este disponible.
    ''' NOTA: Para los mensajes comunes NO se debe especificar el nombre del frontal.
    ''' </summary>
    Public Shared Function obtenerRecurso(ByVal recurso As Eresources, Optional ByVal frontal As Eform = Eform.Comunes) As String
        If SessionValues.Instance.Culture Is Nothing Then
            Return String.Empty
        End If
        Try
            ' Captura el lenguaje y la cultura establecido en el app.config.
            Dim Indigo As SessionValues = SessionValues.Instance
            'Dim configuracionXml As New DataTable("ConfigIdioma")
            'configuracionXml.Columns.Add("Idioma")
            'configuracionXml.ReadXml(String.Concat(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "\", "Indigo Technologies", "\", "Indigo Crystal", "\XML\ConfigIdioma.xml"))
            'Dim culturaInstalada As String = configuracionXml.Rows(0).Item("Idioma").ToString
            Dim culturaInstalada As String = Indigo.Culture.Name
            'se realiza un Select case por la cultura instalada
            Select Case culturaInstalada
            'SI LA CULTURA ES INGLES(ESTADOS UNIDOS)
                Case "en-US"
                    'se realiza un select case por tipo de frontal para no forzar el case
                    'a buscar entre todos los posibles  mensajes, esto me brinda mejorar la busqueda
                    'y mantener mi codigo mejor organizado.
                    Select Case frontal

                    'Recursos para el sistema de mensajeria
                        Case Eform.Messages
                            Select Case recurso
                                Case Eresources.MessagesTitle
                                    Return My.Resources.RecursoEN_US.MessagesTitle
                                Case Eresources.MeesagesRead
                                    Return My.Resources.RecursoEN_US.MeesagesRead
                                Case Eresources.MessagesWriting
                                    Return My.Resources.RecursoEN_US.MessagesWriting
                                Case Eresources.MessagesGroupInvitation
                                    Return My.Resources.RecursoEN_US.MessagesGroupInvitation
                                Case Eresources.MessagesGroupLeave
                                    Return My.Resources.RecursoEN_US.MessagesGroupLeave
                            End Select
                        Case Eform.LiquidacionContrato
                            Select Case recurso
                                Case Eresources.EmpleadoTieneErrores
                                    Return My.Resources.RecursoEN_US.EmpleadoTieneErrores
                                Case Eresources.AlgunEmpleadoTieneErrores
                                    Return My.Resources.RecursoEN_US.AlgunEmpleadoTieneErrores
                            End Select
                        Case Eform.Autoliquidation
                            Select Case recurso
                                Case Eresources.EmpleadoMasDeUnaEPS
                                    Return My.Resources.RecursoEN_US.EmpleadoMasDeUnaEPS
                                Case Eresources.EmpleadoNoTieneEPS
                                    Return My.Resources.RecursoEN_US.EmpleadoNoTieneEPS
                                Case Eresources.EmpleadoMasFondoPension
                                    Return My.Resources.RecursoEN_US.EmpleadoMasFondoPension
                                Case Eresources.EmpleadoNoTieneFondoPension
                                    Return My.Resources.RecursoEN_US.EmpleadoNoTieneFondoPension
                                Case Eresources.EmpleadoMasCajaCompensacion
                                    Return My.Resources.RecursoEN_US.EmpleadoMasCajaCompensacion
                                Case Eresources.EmpleadoNoTieneCajaCompensacion
                                    Return My.Resources.RecursoEN_US.EmpleadoNoTieneCajaCompensacion
                            End Select
                        'Climas de login
                        Case Eform.Weather
                            Select Case recurso
                                Case Eresources.Weatherblowingsnow
                                    Return My.Resources.RecursoEN_US.Weatherblowingsnow
                                Case Eresources.Weatherblustery
                                    Return My.Resources.RecursoEN_US.Weatherblustery
                                Case Eresources.Weatherclearnight
                                    Return My.Resources.RecursoEN_US.Weatherclearnight
                                Case Eresources.Weathercloudy
                                    Return My.Resources.RecursoEN_US.Weathercloudy
                                Case Eresources.Weathercold
                                    Return My.Resources.RecursoEN_US.Weathercold
                                Case Eresources.Weatherdrizzle
                                    Return My.Resources.RecursoEN_US.Weatherdrizzle
                                Case Eresources.Weatherdust
                                    Return My.Resources.RecursoEN_US.Weatherdust
                                Case Eresources.Weatherfairday
                                    Return My.Resources.RecursoEN_US.Weatherfairday
                                Case Eresources.Weatherfairnight
                                    Return My.Resources.RecursoEN_US.Weatherfairnight
                                Case Eresources.Weatherfoggy
                                    Return My.Resources.RecursoEN_US.Weatherfoggy
                                Case Eresources.Weatherfreezingdrizzle
                                    Return My.Resources.RecursoEN_US.Weatherfreezingdrizzle
                                Case Eresources.Weatherfreezingrain
                                    Return My.Resources.RecursoEN_US.Weatherfreezingrain
                                Case Eresources.Weatherhail
                                    Return My.Resources.RecursoEN_US.Weatherhail
                                Case Eresources.Weatherhaze
                                    Return My.Resources.RecursoEN_US.Weatherhaze
                                Case Eresources.Weatherheavysnow
                                    Return My.Resources.RecursoEN_US.Weatherheavysnow
                                Case Eresources.Weatherhot
                                    Return My.Resources.RecursoEN_US.Weatherhot
                                Case Eresources.Weatherhurricane
                                    Return My.Resources.RecursoEN_US.Weatherhurricane
                                Case Eresources.Weatherisolatedthundershowers
                                    Return My.Resources.RecursoEN_US.Weatherisolatedthundershowers
                                Case Eresources.Weatherisolatedthunderstorms
                                    Return My.Resources.RecursoEN_US.Weatherisolatedthunderstorms
                                Case Eresources.Weatherlightsnowshowers
                                    Return My.Resources.RecursoEN_US.Weatherlightsnowshowers
                                Case Eresources.Weathermixedrainandhail
                                    Return My.Resources.RecursoEN_US.Weathermixedrainandhail
                                Case Eresources.Weathermixedrainandsleet
                                    Return My.Resources.RecursoEN_US.Weathermixedrainandsleet
                                Case Eresources.Weathermixedrainandsnow
                                    Return My.Resources.RecursoEN_US.Weathermixedrainandsnow
                                Case Eresources.Weathermixedsnowandsleet
                                    Return My.Resources.RecursoEN_US.Weathermixedsnowandsleet
                                Case Eresources.Weathermostlycloudyday
                                    Return My.Resources.RecursoEN_US.Weathermostlycloudyday
                                Case Eresources.Weathermostlycloudynight
                                    Return My.Resources.RecursoEN_US.Weathermostlycloudynight
                                Case Eresources.Weatherpartlycloudy
                                    Return My.Resources.RecursoEN_US.Weatherpartlycloudy
                                Case Eresources.Weatherpartlycloudyday
                                    Return My.Resources.RecursoEN_US.Weatherpartlycloudyday
                                Case Eresources.Weatherpartlycloudynight
                                    Return My.Resources.RecursoEN_US.Weatherpartlycloudynight
                                Case Eresources.Weatherscatteredshowers
                                    Return My.Resources.RecursoEN_US.Weatherscatteredshowers
                                Case Eresources.Weatherscatteredsnowshowers
                                    Return My.Resources.RecursoEN_US.Weatherscatteredsnowshowers
                                Case Eresources.Weatherscatteredthunderstorms
                                    Return My.Resources.RecursoEN_US.Weatherscatteredthunderstorms
                                Case Eresources.Weatherseverethunderstorms
                                    Return My.Resources.RecursoEN_US.Weatherseverethunderstorms
                                Case Eresources.Weathershowers
                                    Return My.Resources.RecursoEN_US.Weathershowers
                                Case Eresources.Weathersleet
                                    Return My.Resources.RecursoEN_US.Weathersleet
                                Case Eresources.Weathersmoky
                                    Return My.Resources.RecursoEN_US.Weathersmoky
                                Case Eresources.Weathersnow
                                    Return My.Resources.RecursoEN_US.Weathersnow
                                Case Eresources.Weathersnowflurries
                                    Return My.Resources.RecursoEN_US.Weathersnowflurries
                                Case Eresources.Weathersnowshowers
                                    Return My.Resources.RecursoEN_US.Weathersnowshowers
                                Case Eresources.Weathersunny
                                    Return My.Resources.RecursoEN_US.Weathersunny
                                Case Eresources.Weatherthundershowers
                                    Return My.Resources.RecursoEN_US.Weatherthundershowers
                                Case Eresources.Weatherthunderstorms
                                    Return My.Resources.RecursoEN_US.Weatherthunderstorms
                                Case Eresources.Weathertornado
                                    Return My.Resources.RecursoEN_US.Weathertornado
                                Case Eresources.Weathertropicalstorm
                                    Return My.Resources.RecursoEN_US.Weathertropicalstorm
                                Case Eresources.Weatherwindy
                                    Return My.Resources.RecursoEN_US.Weatherwindy

                            End Select

                        'VISOR DE EVENTOS
                        Case Eform.VisorEventos

                            'se realiza un select case para buscar entre los recursos disponibles del visor de eventos.
                            Select Case recurso
                                Case Eresources.VisorTitulo
                                    Return My.Resources.RecursoEN_US.VisorTitulo
                                Case Eresources.VisorErrores
                                    Return My.Resources.RecursoEN_US.VisorErrores
                                Case Eresources.VisorAdvertencias
                                    Return My.Resources.RecursoEN_US.VisorAdvertencias
                                Case Eresources.VisorMensajes
                                    Return My.Resources.RecursoEN_US.VisorMensajes
                                Case Else
                                    Return "Add this value to the resource stack."
                            End Select

                        'form de liquidacion de cesantías
                        Case Eform.LiquidacionCesantias
                            Select Case recurso
                                Case Eresources.NoHayNominasLiquidadas
                                    Return My.Resources.RecursoEN_US.NoHayNominasLiquidadas
                                Case Eresources.NoHayGruposSeleccionados
                                    Return My.Resources.RecursoEN_US.NoHayGruposSeleccionados
                                Case Eresources.EmpleadoNoTieneNominasLiquidadas
                                    Return My.Resources.RecursoEN_US.EmpleadoNoTieneNominasLiquidadas
                                Case Eresources.EmpleadoNoTieneContratoLaboral
                                    Return My.Resources.RecursoEN_US.EmpleadoNoTieneContratoLaboral
                                Case Eresources.EmpleadoYaTieneCesantiasLiquidadas
                                    Return My.Resources.RecursoEN_US.EmpleadoYaTieneCesantiasLiquidadas
                                Case Eresources.EmpleadoNoTieneContrato
                                    Return My.Resources.RecursoEN_US.EmpleadoNoTieneContrato
                                Case Eresources.EmpleadoYaTieneCesantiaAnualLiquidada
                                    Return My.Resources.RecursoEN_US.EmpleadoYaTieneCesantiaAnualLiquidada
                                Case Eresources.NohayCesantiasLiquidadas
                                    Return My.Resources.RecursoEN_US.NohayCesantiasLiquidadas
                                Case Eresources.NoseLiquidaronCesantias
                                    Return My.Resources.RecursoEN_US.NoseLiquidaronCesantias
                                Case Eresources.EmpleadoNoTieneFondoDeCesantia
                                    Return My.Resources.RecursoEN_US.EmpleadoNoTieneFondoDeCesantia
                                Case Eresources.LiquidacionCesantiasYaConfirmada
                                    Return My.Resources.RecursoEN_US.LiquidacionCesantiasYaConfirmada
                            End Select


                        'form de liquidacion de Primas
                        Case Eform.LiquidacionPrimas
                            Select Case recurso
                                Case Eresources.NoHayGruposSeleccionadosPrimas
                                    Return My.Resources.RecursoEN_US.NoHayGruposSeleccionadosPrimas
                                Case Eresources.NoGeneroPrimas
                                    Return My.Resources.RecursoEN_US.NoGeneroPrimas
                                Case Eresources.LiquidacionPrimasConfirmada
                                    Return My.Resources.RecursoEN_US.LiquidacionPrimasConfirmada
                                Case Eresources.LiquidacionPrimasNoConfirmada
                                    Return My.Resources.RecursoEN_US.LiquidacionPrimasNoConfirmada
                                Case Eresources.LiquidacionPrimasYaConfirmada
                                    Return My.Resources.RecursoEN_US.LiquidacionPrimasYaConfirmada
                                Case Eresources.NoHayDatosAcumuladosLiquidacion
                                    Return My.Resources.RecursoEN_US.NoHayDatosAcumuladosLiquidacion
                                Case Eresources.IBCPrimasVacio
                                    Return My.Resources.RecursoEN_US.IBCPrimasVacio
                                Case Eresources.NoHayLiquidacionesGeneradas
                                    Return My.Resources.RecursoEN_US.NoHayLiquidacionesGeneradas
                                Case Eresources.NumeroPrimasDiferentes
                                    Return My.Resources.RecursoEN_US.NumeroPrimasDiferentes
                                Case Eresources.NoHaySeleccionadoSemestrePrimas
                                    Return My.Resources.RecursoEN_US.NoHaySeleccionadoSemestrePrimas
                                Case Eresources.ConfirmarPrimas
                                    Return My.Resources.RecursoEN_US.ConfirmarPrimas
                                Case Eresources.NoHaSeleccionadoTipoPagoPrimas
                                    Return My.Resources.RecursoEN_US.NoHaSeleccionadoTipoPagoPrimas
                            End Select

                        'form cuadro de turnos
                        Case Eform.CuadroDeTurno
                            Select Case recurso
                                Case Eresources.EventoAprobado
                                    Return My.Resources.RecursoEN_US.EventoAprobado

                                Case Eresources.ErrorPermisoVacaciones
                                    Return My.Resources.RecursoEN_US.ErrorPermisoVacaciones

                                Case Eresources.ErrorVacaciones
                                    Return My.Resources.RecursoEN_US.ErrorVacaciones

                                Case Eresources.Vacaciones
                                    Return My.Resources.RecursoEN_US.Vacaciones

                                Case Eresources.PermisoVacaciones
                                    Return My.Resources.RecursoEN_US.PermisoVacaciones

                                Case Eresources.SobreescribirHorario

                                Case Eresources.ErrorHour

                                Case Eresources.ErrorScheduleDetailExisting

                                Case Eresources.ErrorMaximumTotalHourNumber

                                Case Eresources.ErrorWithoutContract

                                Case Eresources.ErrorHoliday
                                    Return My.Resources.RecursoEN_US.ErrorHoliday
                                Case Eresources.EmpleadoLiquidado
                                    Return My.Resources.RecursoEN_US.EmpleadoLiquidado
                                Case Eresources.EmpleadosLiquidados
                                    Return My.Resources.RecursoEN_US.EmpleadosLiquidados
                                Case Eresources.Manana
                                    Return My.Resources.RecursoEN_US.Manana
                                Case Eresources.MananaTarde
                                    Return My.Resources.RecursoEN_US.MananaTarde
                                Case Eresources.Tarde
                                    Return My.Resources.RecursoEN_US.Tarde
                                Case Eresources.MananaNoche
                                    Return My.Resources.RecursoEN_US.MananaNoche
                                Case Eresources.TardeNoche
                                    Return My.Resources.RecursoEN_US.TardeNoche
                                Case Eresources.Noche
                                    Return My.Resources.RecursoEN_US.Noche
                                Case Eresources.Incapacidad
                                    Return My.Resources.RecursoEN_US.Incapacidad
                                Case Eresources.Licencia
                                    Return My.Resources.RecursoEN_US.Licencia
                                Case Eresources.Sancion
                                    Return My.Resources.RecursoEN_US.Sancion
                                Case Eresources.FechaInicioMenor
                                    Return My.Resources.RecursoEN_US.FechaInicioMenor
                                Case Eresources.RangoFrecuenciaAlto
                                    Return My.Resources.RecursoEN_US.RangoFrecuenciaAlto
                                Case Eresources.ErrorMaximoHoras18
                                    Return My.Resources.RecursoEN_US.ErrorMaximoHoras18

                            End Select
                        Case Eform.Vacaciones
                            Select Case recurso
                                Case Eresources.NoPuedeSolicitarDias
                                    Return My.Resources.RecursoEN_US.NoPuedeSolicitarDias
                                Case Eresources.EmpleadoNoPuedeDias
                                    Return My.Resources.RecursoEN_US.EmpleadoNoPuedeDias
                                Case Eresources.EmpleadoNotieneDiasPendientes
                                    Return My.Resources.RecursoEN_US.EmpleadoNotieneDiasPendientes
                                Case Eresources.NoHayEmpleadosSeleccionados
                                    Return My.Resources.RecursoEN_US.NoHayEmpleadosSeleccionados
                                Case Eresources.EmpleadoEnVacaciones
                                    Return My.Resources.RecursoEN_US.EmpleadoEnVacaciones
                                Case Eresources.EsperandoPago
                                    Return My.Resources.RecursoEN_US.EsperandoPago
                                Case Eresources.Pagas
                                    Return My.Resources.RecursoEN_US.Pagas
                                Case Eresources.EstaSeguroCancelarVacacion
                                    Return My.Resources.RecursoEN_US.EstaSeguroCancelarVacacion
                                Case Eresources.EstaSeguroReingresoVacacion
                                    Return My.Resources.RecursoEN_US.EstaSeguroReingresoVacacion
                                Case Eresources.CancelarSolicitud
                                    Return My.Resources.RecursoEN_US.CancelarSolicitud
                                Case Eresources.IngresoForzoso
                                    Return My.Resources.RecursoEN_US.IngresoForzoso
                                Case Eresources.SolicitudCanceladaCorrectamente
                                    Return My.Resources.RecursoEN_US.SolicitudCanceladaCorrectamente
                                Case Eresources.SolicitudYaEstanPagas
                                    Return My.Resources.RecursoEN_US.SolicitudYaEstanPagas
                                Case Eresources.PermisoCargoVacaciones
                                    Return My.Resources.RecursoEN_US.PermisoCargoVacaciones
                                Case Eresources.InterrupcionVacaciones
                                    Return My.Resources.RecursoEN_US.InterrupcionVacaciones
                            End Select

                        Case Eform.LiquidacionNomina
                            Select Case recurso
                                Case Eresources.NoGeneraLiquidacion
                                    Return My.Resources.RecursoEN_US.NoGeneraLiquidacion
                                Case Eresources.NoExisteEmpleado
                                    Return My.Resources.RecursoEN_US.NoGeneraLiquidacion
                                Case Eresources.NominaConfirmadaCorrectamente
                                    Return My.Resources.RecursoEN_US.NominaConfirmadaCorrectamente
                                Case Eresources.PrimeroNominaBorrador
                                    Return My.Resources.RecursoEN_US.PrimeroNominaBorrador
                                Case Eresources.NoHayEmpleadosLiquidar
                                    Return My.Resources.RecursoEN_US.NoHayEmpleadosLiquidar
                                Case Eresources.ErroresGravesLiquidacion
                                    Return My.Resources.RecursoEN_US.ErroresGravesLiquidacion
                                Case Eresources.NoConceptosAutorizados
                                    Return My.Resources.RecursoEN_US.NoConceptosAutorizados
                                Case Eresources.NominaConfirmarNomina
                                    Return My.Resources.RecursoEN_US.NominaConfirmarNomina
                                Case Eresources.NoHayGruposSeleccionadosNomina
                                    Return My.Resources.RecursoEN_US.NoHayGruposSeleccionadosNomina
                                Case Eresources.NoSeleccionoLiquidacionVisualizar
                                    Return My.Resources.RecursoEN_US.NoSeleccionoLiquidacionVisualizar
                                Case Eresources.LiquidacionNominaCorrecta
                                    Return My.Resources.RecursoEN_US.LiquidacionNominaCorrecta

                            End Select
                        Case Eform.PlantillaDeTurno
                            Select Case recurso
                                Case Eresources.MinimoUnOrdinario
                                    Return My.Resources.RecursoEN_US.MinimoUnOrdinario
                                Case Eresources.MinimoUnFeriado
                                    Return My.Resources.RecursoEN_US.MinimoUnFeriado
                                Case Eresources.RangoHorasCompletas
                                    Return My.Resources.RecursoEN_US.RangoHorasCompletas
                                Case Eresources.ConceptoFeriado
                                    Return My.Resources.RecursoEN_US.ConceptoFeriado
                                Case Eresources.ConceptoOrdinario
                                    Return My.Resources.RecursoEN_US.ConceptoOrdinario
                                Case Eresources.TiempoMayorCero
                                    Return My.Resources.RecursoEN_US.TiempoMayorCero
                                Case Eresources.AppointmentExiste
                                    Return My.Resources.RecursoEN_US.AppointmentExiste
                            End Select
                        Case Eform.Incapacidades
                            Select Case recurso
                                Case Eresources.NovedadReajusteVacacionesNoEditar
                                    Return My.Resources.RecursoEN_US.NovedadReajusteVacacionesNoEditar
                                Case Eresources.EmpleadoVacacionesNoPuedeNovedad
                                    Return My.Resources.RecursoEN_US.EmpleadoVacacionesNoPuedeNovedad
                                Case Eresources.EmpleadoVacacionesReajuste
                                    Return My.Resources.RecursoEN_US.EmpleadoVacacionesReajuste
                                Case Eresources.CantidadDiasNovedad
                                    Return My.Resources.RecursoEN_US.CantidadDiasNovedad
                                Case Eresources.EmpleadoNoExiste
                                    Return My.Resources.RecursoEN_US.EmpleadoNoExiste
                                Case Eresources.Postular23
                                    Return My.Resources.RecursoEN_US.Postular23
                                Case Eresources.Postular90Dias
                                    Return My.Resources.RecursoEN_US.Postular90Dias
                                Case Eresources.PostularNomina
                                    Return My.Resources.RecursoEN_US.PostularNomina
                                Case Eresources.PostularPatrono
                                    Return My.Resources.RecursoEN_US.PostularPatrono
                                Case Eresources.Postular23Todos
                                    Return My.Resources.RecursoEN_US.Postular23Todos
                                Case Eresources.LosTresPrimerosDias
                                    Return My.Resources.RecursoEN_US.LosTresPrimerosDias
                                Case Eresources.TodosLosDias
                                    Return My.Resources.RecursoEN_US.TodosLosDias
                                Case Eresources.NoLiquida
                                    Return My.Resources.RecursoEN_US.NoLiquida
                                Case Eresources.IncapacidadAmbulatoria
                                    Return My.Resources.RecursoEN_US.IncapacidadAmbulatoria
                                Case Eresources.IncapacidadGeneral
                                    Return My.Resources.RecursoEN_US.IncapacidadGeneral
                                Case Eresources.IncapacidadHospitalaria
                                    Return My.Resources.RecursoEN_US.IncapacidadHospitalaria
                                Case Eresources.IncapacidadMaternidad
                                    Return My.Resources.RecursoEN_US.IncapacidadMaternidad
                                Case Eresources.IncapacidadPaternidad
                                    Return My.Resources.RecursoEN_US.IncapacidadPaternidad
                                Case Eresources.IncapacidadRiesgos
                                    Return My.Resources.RecursoEN_US.IncapacidadRiesgos
                                Case Eresources.LicenciaLuto
                                    Return My.Resources.RecursoEN_US.LicenciaLuto
                                Case Eresources.GrupoSinParametros
                                    Return My.Resources.RecursoEN_US.GrupoSinParametros
                                Case Eresources.FaltaCampo
                                    Return My.Resources.RecursoEN_US.FaltaCampo
                                Case Eresources.CrearNovedad
                                    Return My.Resources.RecursoEN_US.CrearNovedad
                                Case Eresources.EditarNovedad
                                    Return My.Resources.RecursoEN_US.EditarNovedad
                                Case Eresources.ProrrogaNovedad
                                    Return My.Resources.RecursoEN_US.ProrrogaNovedad
                                Case Eresources.Inicial
                                    Return My.Resources.RecursoEN_US.Inicial
                                Case Eresources.Prorroga
                                    Return My.Resources.RecursoEN_US.Prorroga
                                Case Eresources.NoPuedeAccionPorUltimaProrroga
                                    Return My.Resources.RecursoEN_US.NoPuedeAccionPorUltimaProrroga
                                Case Eresources.NoPuedeFechaInferiorContrato
                                    Return My.Resources.RecursoEN_US.NoPuedeFechaInferiorContrato
                                Case Eresources.NoPuedeFechaSuperiorContrato
                                    Return My.Resources.RecursoEN_US.NoPuedeFechaSuperiorContrato
                                Case Eresources.NoPuedeFechaFinSuperiorContrato
                                    Return My.Resources.RecursoEN_US.NoPuedeFechaFinSuperiorContrato
                                Case Eresources.Liquidar
                                    Return My.Resources.RecursoEN_US.Liquidar
                                Case Eresources.NoLiquidar
                                    Return My.Resources.RecursoEN_US.NoLiquidar
                            End Select
                        Case Eform.RegisterObjection
                            Select Case recurso
                                Case Eresources.ConceptoEspecificoSinDetallados
                                    Return My.Resources.RecursoEN_US.ConceptoEspecificoSinDetallados
                                Case Eresources.TextBotonAgregarObjecion
                                    Return My.Resources.RecursoEN_US.TextBotonAgregarObjecion
                                Case Eresources.TextBotonModificarObjecion
                                    Return My.Resources.RecursoEN_US.TextBotonModificarObjecion
                                Case Eresources.ConsecutivoObjecionNoExiste
                                    Return My.Resources.RecursoEN_US.ConsecutivoObjecionNoExiste
                                Case Eresources.CodigoGlosaYaExiste
                                    Return My.Resources.RecursoEN_US.CodigoGlosaYaExiste
                                Case Eresources.ValorGlosaMayorValorFactura
                                    Return My.Resources.RecursoEN_US.ValorGlosaMayorValorFactura
                                Case Eresources.ValorReiteracionMayorValorGlosado
                                    Return My.Resources.RecursoEN_US.ValorReiteracionMayorValorGlosado
                                Case Eresources.ReiteratedSlopeValueGreaterBalance
                                    Return My.Resources.RecursoEN_US.ReiteratedSlopeValueGreaterBalance
                                Case Eresources.GlossValueWasAlreadyAccepted
                                    Return My.Resources.RecursoEN_US.GlossValueWasAlreadyAccepted
                                Case Eresources.ItemWithoutBalance
                                    Return My.Resources.RecursoEN_US.ItemWithoutBalance
                            End Select
                        Case Eform.ConceptosGenerales
                            Select Case recurso
                                Case Eresources.SeleccioneUnConceptoGeneral
                                    Return My.Resources.RecursoEN_US.SeleccioneUnConceptoGeneral
                            End Select
                        Case Eform.Coordinacion
                            Select Case recurso
                                Case Eresources.MenuDetalleDeOficio
                                    Return My.Resources.RecursoEN_US.MenuDetalleDeOficio
                                Case Eresources.MenuDetalleDeFactura
                                    Return My.Resources.RecursoEN_US.MenuDetalleDeFactura
                                Case Eresources.TituloDetalleDeFacturaCoordinacion
                                    Return My.Resources.RecursoEN_US.TituloDetalleDeFacturaCoordinacion
                                Case Eresources.TituloDetalleDeOficioCoordinacion
                                    Return My.Resources.RecursoEN_US.TituloDetalleDeOficioCoordinacion
                                Case Eresources.ValorAceptadoMayorValorGlosado
                                    Return My.Resources.RecursoEN_US.ValorAceptadoMayorValorGlosado
                            End Select
                        Case Eform.Evaluacion
                            Select Case recurso
                                Case Eresources.ResponsableFacturaBloqueda
                                    Return My.Resources.RecursoEN_US.ResponsibleBlockInvoice
                            End Select
                        Case Eform.Conciliation
                            Select Case recurso
                                Case Eresources.ClienteNoExite
                                    Return My.Resources.RecursoEN_US.ClienteNoExite
                                Case Eresources.ConciliacionNoExiste
                                    Return My.Resources.RecursoEN_US.ConciliacionNoExiste
                                Case Eresources.TextoBotonAgregarParticipante
                                    Return My.Resources.RecursoEN_US.TextoBotonAgregarParticipante
                                Case Eresources.TextoBotonModificarParticipante
                                    Return My.Resources.RecursoEN_US.TextoBotonModificarParticipante
                                Case Eresources.FacturaYaExiste
                                    Return My.Resources.RecursoEN_US.FacturaYaExiste
                                Case Eresources.FacturaNoExisteAlAgregar
                                    Return My.Resources.RecursoEN_US.FacturaNoExisteAlAgregar
                                Case Eresources.DetalleConciliacionVacia
                                    Return My.Resources.RecursoEN_US.DetalleConciliacionVacia
                                Case Eresources.EliminarFacturaRejilla
                                    Return My.Resources.RecursoEN_US.EliminarFacturaRejilla
                                Case Eresources.TituloDetalleFactura
                                    Return My.Resources.RecursoEN_US.TituloDetalleFactura
                                Case Eresources.TituloConciliacionDetalleFactura
                                    Return My.Resources.RecursoEN_US.TituloConciliacionDetalleFactura
                                Case Eresources.FacturaSinGuardar
                                    Return My.Resources.RecursoEN_US.FacturaSinGuardar
                                Case Eresources.ConciliacionSinDetalles
                                    Return My.Resources.RecursoEN_US.ConciliacionSinDetalles
                                Case Eresources.NuevaConciliacionLabel
                                    Return My.Resources.RecursoEN_US.NuevaConciliacionLabel
                                Case Eresources.FaltanDatosParaConciliar
                                    Return My.Resources.RecursoEN_US.FaltanDatosParaConciliar
                                Case Eresources.FaltanDetallesPorConciliar
                                    Return My.Resources.RecursoEN_US.FaltanDetallesPorConciliar
                                Case Eresources.FaltanFacturasPorConciliar
                                    Return My.Resources.RecursoEN_US.FaltanFacturasPorConciliar
                                Case Eresources.ConciliacionConfirmarFactura
                                    Return My.Resources.RecursoEN_US.ConciliacionConfirmarFactura
                                Case Eresources.FaltanFacturasPorPersistir
                                    Return My.Resources.RecursoEN_US.FaltanFacturasPorPersistir
                                Case Eresources.DebeSeleccionarTercero
                                    Return My.Resources.RecursoEN_US.DebeSeleccionarTercero
                                Case Eresources.MenuConciliarDetalle
                                    Return My.Resources.RecursoEN_US.MenuConciliarDetalle
                                Case Eresources.MenuConciliarFactura
                                    Return My.Resources.RecursoEN_US.MenuConciliarFactura
                                Case Eresources.MenuEditarParticipante
                                    Return My.Resources.RecursoEN_US.MenuEditarParticipante
                                Case Eresources.MenuEliminarFacturaConciliacion
                                    Return My.Resources.RecursoEN_US.MenuEliminarFacturaConciliacion
                                Case Eresources.MenuEliminarParticipante
                                    Return My.Resources.RecursoEN_US.MenuEliminarParticipante
                                Case Eresources.MenuPegarFacturaConciliacion
                                    Return My.Resources.RecursoEN_US.MenuPegarFacturaConciliacion
                                Case Eresources.ValorConciliarMayorValorSaldoAConciliar
                                    Return My.Resources.RecursoEN_US.ValorConciliarMayorValorSaldoAConciliar
                            End Select
                        Case Eform.Devolution
                            Select Case recurso
                                Case Eresources.DevolucionNoExiste
                                    Return My.Resources.RecursoEN_US.DevolucionNoExiste
                                Case Eresources.GuardarDevolucion
                                    Return My.Resources.RecursoEN_US.GuardarDevolucion
                            End Select
                        'Comunes - Paises
                        Case Eform.Country
                            Select Case recurso
                                Case Eresources.SeleccioneUnPais
                                    Return My.Resources.RecursoEN_US.SeleccioneUnPais
                            End Select

                        'Comunes - Tipo de pensionado
                        Case Eform.TipoPensionado
                            Select Case recurso
                                Case Eresources.SeleccioneUnTipoPensionado
                                    Return My.Resources.RecursoEN_US.SeleccioneUnTipoPensionado
                            End Select

                        'Comunes - Centro de trabajo
                        Case Eform.CentroTrabajo
                            Select Case recurso
                                Case Eresources.SeleccioneUnCentroTrabajo
                                    Return My.Resources.RecursoEN_US.SeleccioneUnCentroTrabajo
                            End Select
                        Case Eform.Company
                            Select Case recurso
                                Case Eresources.SeleccioneUnaEmpresa
                                    Return My.Resources.RecursoEN_US.SeleccioneUnaEmpresa
                                Case Eresources.CentroAtencionExisteEnEmpresa
                                    Return My.Resources.RecursoEN_US.CentroAtencionExisteEnEmpresa
                                Case Eresources.MenuEliminarCentroCompanies
                                    Return My.Resources.RecursoEN_US.MenuEliminarCentroCompanies
                                Case Else
                                    Return "Agrege Este Valor a la Pila de Recursos"
                            End Select
                        Case Eform.AutorizationHealthCenter
                            Select Case recurso
                                Case Eresources.SeleccioneUnUsuario
                                    Return My.Resources.RecursoEN_US.SeleccioneUnUsuario
                                Case Eresources.EmpresaYaExiste
                                    Return My.Resources.RecursoEN_US.EmpresaYaExiste
                                Case Eresources.UsuarioNoExiste
                                    Return My.Resources.RecursoEN_US.UsuarioNoExiste
                                Case Else
                                    Return "Agrege Este Valor a la Pila de Recursos"
                            End Select
                        Case Eform.RecepcionObjeciones
                            Select Case recurso
                                Case Eresources.FacturaExisteLista
                                    Return My.Resources.RecursoEN_US.FacturaExisteLista
                                Case Eresources.OficioSinDetalles
                                    Return My.Resources.RecursoEN_US.OficioSinDetalles
                                Case Eresources.ComunesNoSeEncontroDatoERP
                                    Return My.Resources.RecursoEN_US.ComunesNoSeEncontroDatoERP
                                Case Eresources.ConfirmarFacturaSinValorGlosado
                                    Return My.Resources.RecursoEN_US.ConfirmarFacturaSinValorGlosado
                                Case Eresources.ValorGlosadoMayorSaldo
                                    Return My.Resources.RecursoEN_US.ValorGlosadoMayorSaldo
                                Case Eresources.FacturaSinConfirmarEnLista
                                    Return My.Resources.RecursoEN_US.FacturaSinConfirmarEnLista
                                Case Eresources.NoPuedeEliminarPorMovimiento
                                    Return My.Resources.RecursoEN_US.NoPuedeEliminarPorMovimiento
                                Case Eresources.seleccioneFechafininvalida
                                    Return My.Resources.RecursoEN_US.SeleccioneFechaValida
                                Case Eresources.GloseDetalleQX
                                    Return My.Resources.RecursoEN_US.GloseDetalleQX
                                Case Eresources.NoPuedeAnularHayMovimiento
                                    Return My.Resources.RecursoEN_US.NoPuedeAnularHayMovimiento
                                Case Eresources.NoPuedeGlosarEstadoERP
                                    Return My.Resources.RecursoEN_US.NoPuedeGlosarEstadoERP
                                Case Eresources.NoPuedeGuardarPorEstadoERP
                                    Return My.Resources.RecursoEN_US.NoPuedeGuardarPorEstadoERP
                                Case Eresources.NoPuedeRealizarAccionAnulado
                                    Return My.Resources.RecursoEN_US.NoPuedeRealizarAccionAnulado
                                Case Eresources.NoSePuedeAgregarFacturaEstado
                                    Return My.Resources.RecursoEN_US.NoSePuedeAgregarFacturaEstado
                                Case Eresources.NosePuedeAgregaFacturaRadicada
                                    Return My.Resources.RecursoEN_US.NosePuedeAgregaFacturaRadicada
                                Case Eresources.NosePuedeAgregaFacturaReiterada
                                    Return My.Resources.RecursoEN_US.NosePuedeAgregaFacturaReiterada
                                Case Eresources.ConfirmarReitereacionMasiva
                                    Return My.Resources.RecursoEN_US.ConfirmarReitereacionMasiva
                                Case Eresources.SeleccioneUnaEmpresa
                                    Return My.Resources.RecursoEN_US.SeleccioneUnaEmpresa
                                Case Eresources.NoExisteInformacionAPegar
                                    Return My.Resources.RecursoEN_US.NoExisteInformacionAPegar
                                Case Eresources.NoPuedeEliminarDatoReiteracion
                                    Return My.Resources.RecursoEN_US.NoPuedeEliminarDatoReiteracion
                                Case Eresources.LabelNuevo
                                    Return My.Resources.RecursoEN_US.LabelNuevo
                                Case Eresources.FacturaSinRadicar
                                    Return My.Resources.RecursoEN_US.FacturaSinRadicar
                                Case Eresources.FacturaRadicadaSinConfirmar
                                    Return My.Resources.RecursoEN_US.FacturaRadicadaSinConfirmar
                                Case Eresources.FacturaAnulada
                                    Return My.Resources.RecursoEN_US.FacturaAnulada
                                Case Eresources.EstadoInvalido
                                    Return My.Resources.RecursoEN_US.EstadoInvalido
                                Case Eresources.EncabezadoReiteracion
                                    Return My.Resources.RecursoEN_US.EncabezadoReiteracion
                                Case Eresources.TiponoReconocido
                                    Return My.Resources.RecursoEN_US.TiponoReconocido
                                Case Eresources.MenuVerDetalle
                                    Return My.Resources.RecursoEN_US.MenuVerDetalle
                                Case Eresources.MenuEliminar
                                    Return My.Resources.RecursoEN_US.MenuEliminar
                                Case Eresources.MenuGlosarSeleccion
                                    Return My.Resources.RecursoEN_US.MenuGlosarSeleccion
                                Case Eresources.MenuReiterar
                                    Return My.Resources.RecursoEN_US.MenuReiterar
                                Case Eresources.NosePuedeAgregaFacturaEnProceso
                                    Return My.Resources.RecursoEN_US.NosePuedeAgregaFacturaEnProceso
                                Case Eresources.NoPuedeGuardarPorDetalleOficio
                                    Return My.Resources.RecursoEN_US.NoPuedeGuardarPorDetalleOficio
                                Case Eresources.NoPuedeReiterarNoExisteGlosa
                                    Return My.Resources.RecursoEN_US.NoPuedeReiterarNoExisteGlosa
                                Case Eresources.FaltaNitTercero
                                    Return My.Resources.RecursoEN_US.FaltaNitTercero
                                Case Eresources.NoHayRegistroCriteriosBusqueda
                                    Return My.Resources.RecursoEN_US.NoHayRegistroCriteriosBusqueda
                                Case Eresources.NoseEncuentraFactura
                                    Return My.Resources.RecursoEN_US.NoseEncuentraFactura
                            End Select
                        Case Eform.ParametersInterfaz
                            Select Case recurso
                                Case Eresources.NoExisteContenedor
                                    Return My.Resources.RecursoEN_US.NoExisteContenedor
                                Case Eresources.NoExisteDatosContables
                                    Return My.Resources.RecursoEN_US.NoExisteDatosContables
                                Case Eresources.NoExisteCuentaConfigurada
                                    Return My.Resources.RecursoEN_US.NoExisteCuentaConfigurada
                                Case Eresources.NoExisteCuentaFacturaRadicadaConfigurada
                                    Return My.Resources.RecursoEN_US.NoExisteCuentaFacturaRadicadaConfigurada
                            End Select


                        'CAMBIAR CONTRASEÑA
                        Case Eform.CambiarContrasena
                            Select Case recurso
                                Case Eresources.ContrasenaMinimoCaracteres
                                    Return My.Resources.RecursoEN_US.ContrasenaMinimoCaracteres
                                Case Eresources.ContrasenaConfirmacionVacia
                                    Return My.Resources.RecursoEN_US.ContrasenaConfirmacionVacia
                                Case Eresources.ContrasenaAnteriorVacia
                                    Return My.Resources.RecursoEN_US.ContrasenaAnteriorVacia
                                Case Eresources.ContrasenaNuevaVacia
                                    Return My.Resources.RecursoEN_US.ContrasenaNuevaVacia
                                Case Eresources.ContrasenasNoCoinciden
                                    Return My.Resources.RecursoEN_US.ContrasenasNoCoinciden
                                Case Eresources.ContrasenaNoCorrecta
                                    Return My.Resources.RecursoEN_US.ContrasenaNoCorrecta
                                Case Eresources.ContrasenaGuardadaCorrectamente
                                    Return My.Resources.RecursoEN_US.ContrasenaGuardadaCorrectamente
                                Case Eresources.ContrasenaDigiteContrasena
                                    Return My.Resources.RecursoEN_US.ContrasenaDigiteContrasena
                                Case Else
                                    Return "Add this value to the resource stack."
                            End Select

                        'Control Confirmados
                        Case Eform.CtrConfirmado
                            Select Case recurso
                                Case Eresources.Confirmado
                                    Return My.Resources.RecursoEN_US.Confirmado
                                Case Eresources.SinConfirmar
                                    Return My.Resources.RecursoEN_US.SinConfirmar
                                Case Eresources.Anulado
                                    Return My.Resources.RecursoEN_US.Anulado
                                Case Eresources.OficioConRespuesta
                                    Return My.Resources.RecursoEN_US.OficioConRespuesta
                                Case Else
                                    Return "Add this value to the resource stack."
                            End Select

                        'FORMULARIO ROLES
                        Case Eform.Roles
                            Select Case recurso
                                Case Eresources.RolesMensajeComplemento
                                    Return My.Resources.RecursoEN_US.RolesMensajeComplemento
                                Case Else
                                    Return "Add this value to the resource stack."
                            End Select
                        'FORMULARIO UNIDADES FUNCIONALES
                        Case Eform.UnidadesFuncionales
                            Select Case recurso
                                Case Eresources.UnidadFunMensajeComplemento
                                    Return My.Resources.RecursoEN_US.UnidadFunMensajeComplemento
                                Case Eresources.UsuarioYaAgregado
                                    Return My.Resources.RecursoEN_US.UsuarioYaAgregado
                                Case Eresources.SeleccioneUsuario
                                    Return My.Resources.RecursoEN_US.SeleccioneUsuario
                                Case Else
                                    Return "Add this value to the resource stack."
                            End Select
                        'FORMULARIO CENTROS DE ATENCION
                        Case Eform.CentroAtenciones
                            Select Case recurso
                                Case Eresources.CentroAtenMensajeComplemento
                                    Return My.Resources.RecursoEN_US.CentroAtenMensajeComplemento
                                Case Eresources.CentroCodigoHabilitacion
                                    Return My.Resources.RecursoEN_US.CentroCodigoHabilitacion
                                Case Eresources.CentroUnidadAgregada
                                    Return My.Resources.RecursoEN_US.CentroUnidadAgregada
                                Case Eresources.CentroEliminado
                                    Return My.Resources.RecursoEN_US.CentroEliminado
                                Case Else
                                    Return "Add this value to the resource stack."
                            End Select

                        'FORMULARIO GRUPOS
                        Case Eform.Grupos
                            Select Case recurso
                                Case Eresources.GruposMensajeComplemento
                                    Return My.Resources.RecursoEN_US.GruposMensajeComplemento
                                Case Eresources.GruposNoEliminado
                                    Return My.Resources.RecursoEN_US.GruposNoEliminado
                                Case Else
                                    Return "Add this value to the resource stack."
                            End Select
                        'FORMULARIO USUARIO
                        Case Eform.Usuario
                            Select Case recurso
                                Case Eresources.UsuarioUnidadOperativaDefault
                                    Return My.Resources.RecursoEN_US.UsuarioUnidadOperativaDefault
                                Case Eresources.UsuarioPermisoUnaEmpresa
                                    Return My.Resources.RecursoEN_US.UsuarioPermisoUnaEmpresa
                                Case Eresources.UsuarioMensajeComplemento
                                    Return My.Resources.RecursoEN_US.UsuarioMensajeComplemento
                                Case Eresources.UsuarioNoExiste
                                    Return My.Resources.RecursoEN_US.UsuarioNoExiste
                                Case Else
                                    Return "Add this value to the resource stack."
                            End Select
                        'FORMULARIO CONEXION
                        Case Eform.Conexion
                            Select Case recurso
                                Case Eresources.ConexionReiniciarIndigo
                                    Return My.Resources.RecursoEN_US.ConexionReiniciarIndigo
                                Case Eresources.ConexionUrlServidor
                                    Return My.Resources.RecursoEN_US.ConexionUrlServidor
                                Case Eresources.ConexionUrlServidorEntidades
                                    Return My.Resources.RecursoEN_US.ConexionUrlServidorEntidades
                                Case Eresources.ConexionRutaNoExiste
                                    Return My.Resources.RecursoEN_US.ConexionRutaNoExiste
                                Case Eresources.ConexionSeleccioneRuta
                                    Return My.Resources.RecursoEN_US.ConexionSeleccioneRuta
                                Case Eresources.ComunesDigiteDatos
                                    Return My.Resources.RecursoEN_US.ComunesDigiteDatos
                                Case Else
                                    Return "Add this value to the resource stack."
                            End Select
                        'FORMULARIO DESBLOQUEAR USUARIO
                        Case Eform.DesbloquearUsuario
                            Select Case recurso
                                Case Eresources.DesbloquearUsuarioDesbloqueado
                                    Return My.Resources.RecursoEN_US.DesbloquearUsuarioDesbloqueado
                                Case Eresources.DesbloquearUsuarioNOdesbloqueado
                                    Return My.Resources.RecursoEN_US.DesbloquearUsuarioNOdesbloqueado
                                Case Else
                                    Return "Add this value to the resource stack."
                            End Select

                        Case Eform.Login
                            Select Case recurso
                                Case Eresources.LoginCentroAtencion
                                    Return My.Resources.RecursoEN_US.LoginCentroAtencion
                                Case Eresources.LoginUnidadFuncional
                                    Return My.Resources.RecursoEN_US.LoginUnidadFuncional
                                Case Eresources.LoginSeleccioneEmpresa
                                    Return My.Resources.RecursoEN_US.LoginSeleccioneEmpresa
                                Case Eresources.LoginPerfilAsistencial
                                    Return My.Resources.RecursoEN_US.LoginPerfilAsistencial
                                Case Eresources.LoginIntentoContrasena
                                    Return My.Resources.RecursoEN_US.LoginIntentoContrasena
                                Case Eresources.LoginIntentoContrasenaComplemento
                                    Return My.Resources.RecursoEN_US.LoginIntentoContrasenaComplemento
                                Case Eresources.LoginCambiarContrasena
                                    Return My.Resources.RecursoEN_US.LoginCambiarContrasena
                                Case Eresources.LoginGuardarComo
                                    Return My.Resources.RecursoEN_US.LoginGuardarComo
                                Case Eresources.LoginCuentaCaduco
                                    Return My.Resources.RecursoEN_US.LoginCuentaCaduco
                                Case Eresources.LoginUsuarioInactivo
                                    Return My.Resources.RecursoEN_US.LoginUsuarioInactivo
                                Case Eresources.LoginUsuarioBloqueado
                                    Return My.Resources.RecursoEN_US.LoginUsuarioBloqueado
                                Case Else
                                    Return "Add this value to the resource stack."
                            End Select
                        'CUSTOMIZAR SE UTILIZA CUANDO SE VA A PERSONALIZAR UN FRONTAL ESPECIFICO
                        Case Eform.Customizar
                            Select Case recurso
                                Case Eresources.CustomizarEspacioVacio
                                    Return My.Resources.RecursoEN_US.CustomizarEspacioVacio
                                Case Eresources.CustomizarEtiqueta
                                    Return My.Resources.RecursoEN_US.CustomizarEtiqueta
                                Case Eresources.CustomizarSeparador
                                    Return My.Resources.RecursoEN_US.CustomizarSeparador
                                Case Eresources.CustomizarDivisor
                                    Return My.Resources.RecursoEN_US.CustomizarDivisor
                                Case Eresources.CustomizarMostrarItem
                                    Return My.Resources.RecursoEN_US.CustomizarMostrarItem
                                Case Eresources.CustomizarMostrarTexto
                                    Return My.Resources.RecursoEN_US.CustomizarMostrarTexto
                                Case Eresources.CustomizarOcultarItem
                                    Return My.Resources.RecursoEN_US.CustomizarOcultarItem
                                Case Eresources.CustomizarOcultarTexto
                                    Return My.Resources.RecursoEN_US.CustomizarOcultarTexto
                                Case Else
                                    Return "Add this value to the resource stack."
                            End Select

                        'REPORTES SE UTILIZA PARA QUE SE DEFINAN TODOS LOS MENSAJES RELACIONADOS A REPORTES
                        Case Eform.Reportes
                            Select Case recurso
                                Case Eresources.DefinitionReport
                                    Return My.Resources.RecursoEN_US.DefinitionReport
                                Case Eresources.DefinitionReport
                                    Return My.Resources.RecursoEN_US.DefaultDefinition
                            End Select

                        'COMUNES SE UTILIZA PARA QUE POR ACA ENTREN TODOS LOS MENSAJES COMUNES
                        Case Eform.Comunes
                            Select Case recurso
                                Case Eresources.SeConfirmaronTodos
                                    Return My.Resources.RecursoEN_US.SeConfirmaronTodos
                                Case Eresources.SeConfirmoRegistro
                                    Return My.Resources.RecursoEN_US.SeConfirmoRegistro
                                Case Eresources.XpoGridErrorMessage
                                    Return My.Resources.RecursoEN_US.XpoGridErrorMessage
                                Case Eresources.XpoGridErrorTitle
                                    Return My.Resources.RecursoEN_US.XpoGridErrorTitle
                                Case Eresources.AgregarComunes
                                    Return My.Resources.RecursoEN_US.AgregarComunes
                                Case Eresources.ModificarComunes
                                    Return My.Resources.RecursoEN_US.ModificarComunes
                                Case Eresources.TituloParametrosComunes
                                    Return My.Resources.RecursoEN_US.TituloParametrosComunes
                                Case Eresources.PlantillaExistente
                                    Return My.Resources.RecursoEN_US.PlantillaExistente
                                Case Eresources.PlantillaPorDefecto
                                    Return My.Resources.RecursoEN_US.PlantillaPorDefecto
                                Case Eresources.ComunesGenerarReporte
                                    Return My.Resources.RecursoEN_US.ComunesGenerarReporte
                                Case Eresources.ComunesTodos
                                    Return My.Resources.RecursoEN_US.ComunesTodos
                                Case Eresources.RegistroEnEdicion
                                    Return My.Resources.RecursoEN_US.RegistroEnEdicion
                                Case Eresources.ComunesAbrirEntidad
                                    Return My.Resources.RecursoEN_US.ComunesAbrirEntidad
                                Case Eresources.YaExisteFormulario
                                    Return My.Resources.RecursoEN_US.YaExisteFormulario
                                Case Eresources.ComunesDebeIngresarLaRutaDeServiciosYProtocolo
                                    Return My.Resources.RecursoEN_US.ComunesDebeIngresarLaRutaDeServiciosYProtocolo
                                Case Eresources.ComunesRemplazaInformacionArchivoConfiguracion
                                    Return My.Resources.RecursoEN_US.ComunesRemplazaInformacionArchivoConfiguracion
                                Case Eresources.ComunesDebeQuitarPermisoRoll
                                    Return My.Resources.RecursoEN_US.ComunesDebeQuitarPermisoRoll
                                Case Eresources.ComunesDias
                                    Return My.Resources.RecursoEN_US.ComunesDias
                                Case Eresources.ComunesNoHayRegistros
                                    Return My.Resources.RecursoEN_US.ComunesNoHayRegistros
                                Case Eresources.ComunesNoHayRegistrosParaEliminar
                                    Return My.Resources.RecursoEN_US.ComunesNoHayRegistrosParaEliminar
                                Case Eresources.ComunesActivo
                                    Return My.Resources.RecursoEN_US.ComunesActivo
                                Case Eresources.ComunesInactivo
                                    Return My.Resources.RecursoEN_US.ComunesInactivo
                                Case Eresources.ComunesBloqueoSession
                                    Return My.Resources.RecursoEN_US.ComunesBloqueoSession
                                Case Eresources.ComunesUsuarioNoAdministrador
                                    Return My.Resources.RecursoEN_US.ComunesUsuarioNoAdministrador
                                Case Eresources.ComunesSePerderanCambios
                                    Return My.Resources.RecursoEN_US.ComunesSePerderanCambios
                                Case Eresources.ComunesUserNameInvalid
                                    Return My.Resources.RecursoEN_US.ComunesUserNameInvalid
                                Case Eresources.ComunesSeguroCerrarSesion
                                    Return My.Resources.RecursoEN_US.ComunesSeguroCerrarSesion
                                Case Eresources.RegistroActivo
                                    Return My.Resources.RecursoEN_US.RegistroActivo
                                Case Eresources.RegistroInactivo
                                    Return My.Resources.RecursoEN_US.RegistroInactivo
                                Case Eresources.RegistroBloqueado
                                    Return My.Resources.RecursoEN_US.RegistroBloqueado
                                Case Eresources.Informacion
                                    Return My.Resources.RecursoEN_US.Informacion
                                Case Eresources.Advertencia
                                    Return My.Resources.RecursoEN_US.Advertencia
                                Case Eresources.Errores
                                    Return My.Resources.RecursoEN_US.Errores
                                Case Eresources.Pregunta
                                    Return My.Resources.RecursoEN_US.Pregunta
                                Case Eresources.ComunesUsuarioSinPermisosEmpresa
                                    Return My.Resources.RecursoEN_US.ComunesUsuarioSinPermisosEmpresa
                                Case Eresources.CerrarFormulariosCambiarEmpresa
                                    Return My.Resources.RecursoEN_US.CerrarFormulariosCambiarEmpresa
                                Case Eresources.ComunesFechaMenorActual
                                    Return My.Resources.RecursoEN_US.ComunesFechaMenorActual
                                Case Eresources.ComunesConfirmar
                                    Return My.Resources.RecursoEN_US.ComunesConfirmar
                                Case Eresources.ComunesImprimir
                                    Return My.Resources.RecursoEN_US.ComunesImprimir
                                Case Eresources.ComunesClienteNoExiste
                                    Return My.Resources.RecursoEN_US.ComunesClienteNoExiste
                                Case Eresources.ComunesSesionLocal
                                    Return My.Resources.RecursoEN_US.ComunesSesionLocal
                                Case Eresources.ComunesSesionRemota
                                    Return My.Resources.RecursoEN_US.ComunesSesionRemota
                                Case Eresources.ComunesSeleccioneRegistroEliminar
                                    Return My.Resources.RecursoEN_US.ComunesSeleccioneRegistroEliminar
                                Case Eresources.ComunesEliminarConfirmado
                                    Return My.Resources.RecursoEN_US.ComunesEliminarConfirmado
                                Case Eresources.ComunesConfirmadoCorrectamente
                                    Return My.Resources.RecursoEN_US.ComunesConfirmadoCorrectamente
                                Case Eresources.ComunesAnuladoCorrectamente
                                    Return My.Resources.RecursoEN_US.ComunesAnuladoCorrectamente
                                Case Eresources.ComunesNoSeEncontroDatoERP
                                    Return My.Resources.RecursoEN_US.ComunesNoSeEncontroDatoERP
                                Case Eresources.ComunesPreguntaAnular
                                    Return My.Resources.RecursoEN_US.ComunesPreguntaAnular
                                Case Eresources.ComunesPreguntaConfirmar
                                    Return My.Resources.RecursoEN_US.ComunesPreguntaConfirmar
                                Case Eresources.ComunesLayoutRestablecido
                                    Return My.Resources.RecursoEN_US.ComunesLayoutRestablecido
                                Case Eresources.ComunesActualizado
                                    Return My.Resources.RecursoEN_US.ComunesActualizado
                                Case Eresources.ComunesCodigoVacio
                                    Return My.Resources.RecursoEN_US.ComunesCodigoVacio
                                Case Eresources.ComunesDigiteDatos
                                    Return My.Resources.RecursoEN_US.ComunesDigiteDatos
                                Case Eresources.ComunesEliminado
                                    Return My.Resources.RecursoEN_US.ComunesEliminado
                                Case Eresources.ComunesGuardado
                                    Return My.Resources.RecursoEN_US.ComunesGuardado
                                Case Eresources.ComunesNombreVacio
                                    Return My.Resources.RecursoEN_US.ComunesNombreVacio
                                Case Eresources.ComunesDireccion
                                    Return My.Resources.RecursoEN_US.ComunesDireccion
                                Case Eresources.ComunesElija
                                    Return My.Resources.RecursoEN_US.ComunesElija
                                Case Eresources.ComunesCodigoCaracteres
                                    Return My.Resources.RecursoEN_US.ComunesCodigoCaracteres
                                Case Eresources.ComunesContacteAdministrador
                                    Return My.Resources.RecursoEN_US.ComunesContacteAdministrador
                                Case Eresources.PermisosEliminar
                                    Return My.Resources.RecursoEN_US.PermisosEliminar
                                Case Eresources.PermisosGuardar
                                    Return My.Resources.RecursoEN_US.PermisosGuardar
                                Case Eresources.PermisosActualizar
                                    Return My.Resources.RecursoEN_US.PermisosActualizar
                                Case Eresources.PermisosEliminarRejilla
                                    Return My.Resources.RecursoEN_US.PermisosEliminarRejilla
                                Case Eresources.PermisosCustomizar
                                    Return My.Resources.RecursoEN_US.PermisosCustomizar
                                Case Eresources.PermisosDocumentos
                                    Return My.Resources.RecursoEN_US.PermisosDocumentos
                                Case Eresources.PermisosRedesSociales
                                    Return My.Resources.RecursoEN_US.PermisosRedesSociales
                                Case Eresources.PermisosComunicacion
                                    Return My.Resources.RecursoEN_US.PermisosComunicacion
                                Case Eresources.PermisosOtrosPlugins
                                    Return My.Resources.RecursoEN_US.PermisosOtrosPlugins
                                Case Eresources.PermisosVisible
                                    Return My.Resources.RecursoEN_US.PermisosVisible
                                Case Eresources.ComunesCorreoPersonal
                                    Return My.Resources.RecursoEN_US.ComunesCorreoPersonal
                                Case Eresources.ComunesCorreoEmpresarial
                                    Return My.Resources.RecursoEN_US.ComunesCorreoEmpresarial
                                Case Eresources.ComunesXmlCambio
                                    Return My.Resources.RecursoEN_US.ComunesXmlCambio
                                Case Eresources.ComunesEliminarRegistro, Eresources.ComunesEliminarRegistros
                                    Return My.Resources.RecursoEN_US.ComunesEliminarRegistro
                                Case Eresources.ComunesEditarRegistro
                                    Return My.Resources.RecursoEN_US.ComunesEditarRegistro
                                Case Eresources.ComunesIndigoCrystal
                                    Return My.Resources.RecursoEN_US.ComunesIndigoCrystal
                                Case Eresources.ComunesArchivoCreado
                                    Return My.Resources.RecursoEN_US.ComunesArchivoCreado
                                Case Eresources.PermisosConsultar
                                    Return My.Resources.RecursoEN_US.PermisosConsultar
                                Case Eresources.ComunesErrorLevantarDefinicion
                                    Return My.Resources.RecursosES_CO.ComunesErrorLevantarDefinicion
                                Case Eresources.ComunesNoAplicaBuscar
                                    Return My.Resources.RecursoEN_US.ComunesNoAplicaBuscar
                                Case Eresources.ComunesConfirmarFactura
                                    Return My.Resources.RecursoEN_US.ComunesConfirmarFactura
                                Case Eresources.ComunesFormIncompleto
                                    Return My.Resources.RecursoEN_US.ComunesFormIncompleto
                                'Tipos de identificacion
                                Case Eresources.CedulaCiudadania
                                    Return My.Resources.RecursoEN_US.CedulaCiudadania
                                Case Eresources.CedulaExtranjeria
                                    Return My.Resources.RecursoEN_US.CedulaExtranjeria
                                Case Eresources.TarjetaIdentidad
                                    Return My.Resources.RecursoEN_US.TarjetaIdentidad
                                Case Eresources.RegistroCivil
                                    Return My.Resources.RecursoEN_US.RegistroCivil
                                Case Eresources.Pasaporte
                                    Return My.Resources.RecursoEN_US.Pasaporte
                                Case Eresources.AdultoSinIdentificacion
                                    Return My.Resources.RecursoEN_US.AdultoSinIdentificacion
                                Case Eresources.MenorSinIdentificacion
                                    Return My.Resources.RecursoEN_US.MenorSinIdentificacion
                                Case Eresources.Nit
                                    Return My.Resources.RecursoEN_US.Nit
                                Case Eresources.NumeroUnicoIdentificacionPersonal
                                    Return My.Resources.RecursoEN_US.NumeroUnicoIdentificacionPersonal
                                Case Eresources.CertificadoNacidoVivo
                                    Return My.Resources.RecursoEN_US.CertificadoNacidoVivo
                                Case Eresources.CarnetDiplomatico
                                    Return My.Resources.RecursoEN_US.CarnetDiplomatico
                                Case Eresources.Salvoconducto
                                    Return My.Resources.RecursoEN_US.Salvoconducto
                                Case Eresources.PermisoEspecialPermanencia
                                    Return My.Resources.RecursoEN_US.PermisoEspecialPermanencia
                                'Estado civil
                                Case Eresources.Soltero
                                    Return My.Resources.RecursoEN_US.Soltero
                                Case Eresources.Casado
                                    Return My.Resources.RecursoEN_US.Casado
                                Case Eresources.Divorciado
                                    Return My.Resources.RecursoEN_US.Divorciado
                                Case Eresources.Viudo
                                    Return My.Resources.RecursoEN_US.Viudo
                                Case Eresources.UnionLibre
                                    Return My.Resources.RecursoEN_US.UnionLibre
                                'Genero
                                Case Eresources.Masculino
                                    Return My.Resources.RecursoEN_US.Masculino
                                Case Eresources.Femenino
                                    Return My.Resources.RecursoEN_US.Femenino
                                Case Eresources.Otro
                                    Return My.Resources.RecursoEN_US.Otro
                                'SI/NO
                                Case Eresources.Si
                                    Return My.Resources.RecursoEN_US.Si
                                Case Eresources.No
                                    Return My.Resources.RecursoEN_US.No
                                'Activo/Suspendido
                                Case Eresources.Activo
                                    Return My.Resources.RecursoEN_US.Activo
                                Case Eresources.Suspendido
                                    Return My.Resources.RecursoEN_US.Suspendido
                                'Estado
                                Case Eresources.Ninguno
                                    Return My.Resources.RecursoEN_US.Ninguno
                                'Eliminar Registro
                                Case Eresources.EliminarRegistro
                                    Return My.Resources.RecursoEN_US.EliminarRegistro
                                Case Eresources.OcultarFiltros
                                    Return My.Resources.RecursoEN_US.OcultarFiltros
                                Case Eresources.OcultarListaFacturas
                                    Return My.Resources.RecursoEN_US.OcultarListaFacturas
                                Case Eresources.SeleccionesCamposParaFiltros
                                    Return My.Resources.RecursoEN_US.SeleccionesCamposParaFiltros
                                Case Eresources.LabelNumeroDeRegistro
                                    Return My.Resources.RecursoEN_US.LabelNumeroDeRegistro
                                Case Eresources.EditarRegistro
                                    Return My.Resources.RecursoEN_US.EditarRegistro
                                Case Eresources.ComunesErrorConcurrencia
                                    Return My.Resources.RecursoEN_US.ComunesErrorConcurrencia
                                Case Eresources.ComunesErrorDependencia
                                    Return My.Resources.RecursoEN_US.ComunesErrorDependencia
                                Case Eresources.ItemYaAgregado
                                    Return My.Resources.RecursoEN_US.ItemYaAgregado
                                Case Eresources.ComunesSeleccioneAño
                                    Return My.Resources.RecursoEN_US.ComunesSeleccioneAño
                                Case Else
                                    Return "Add this value to the resource stack."

                            End Select

                        'FACTURACIÓN
                        Case Eform.Facturacion
                            Select Case recurso
                                Case Eresources.NoExistenParametrosFacturacion
                                    Return My.Resources.RecursoEN_US.SinParametrosFacturacion
                            End Select

                        'FORMULARIO DE PAISES
                        Case Eform.Country
                            Select Case recurso
                                Case Eresources.SeleccioneUnPais
                                    Return My.Resources.RecursoEN_US.SeleccioneUnPais
                                Case Else
                                    Return "Agregue este valor a la pila de recursos"
                            End Select
                        'FORMULARIO DE CIUDADES
                        Case Eform.City
                            Select Case recurso
                                Case Eresources.SeleccioneUnaCiudad
                                    Return My.Resources.RecursoEN_US.SeleccioneUnaCiudad
                                Case Else
                                    Return "Agregue este valor a la pila de recursos"
                            End Select
                        'FORMULARIO CORPORACIONES
                        Case Eform.Corporation
                            Select Case recurso
                                Case Eresources.seleccioneCorporacion
                                    Return My.Resources.RecursoEN_US.seleccioneCorporacion
                                Case Else
                                    Return "Agregue este valor a la pila de recursos"
                            End Select
                        'FORMULARIO DE GRUPOS
                        Case Eform.groups
                            Select Case recurso
                                Case Eresources.seleccioneGrupo
                                    Return My.Resources.RecursoEN_US.seleccioneGrupo
                                Case Eresources.conceptoYaAgregado
                                    Return My.Resources.RecursoEN_US.conceptoYaAgregado
                                Case Eresources.ordinario
                                    Return My.Resources.RecursoEN_US.ordinario
                                Case Eresources.feriado
                                    Return My.Resources.RecursoEN_US.feriado
                                Case Eresources.normal
                                    Return My.Resources.RecursoEN_US.normal
                                Case Eresources.nocturno
                                    Return My.Resources.RecursoEN_US.nocturno
                                Case Eresources.GrupoEventosMinReg
                                    Return My.Resources.RecursoEN_US.GrupoEventosMinReg
                                Case Else
                                    Return "Agregue este valor a la pila de recursos"
                            End Select

                        'FORMULARIO DE LENGUAJES
                        Case Eform.Languages
                            Select Case recurso
                                Case Eresources.seleccioneLenguaje
                                    Return My.Resources.RecursoEN_US.seleccioneLenguaje
                                Case Else
                                    Return "Agregue este valor a la pila de recursos"
                            End Select

                        'Nomina - Riesgos Profesionales
                        Case Eform.RiesgoProfesional
                            Select Case recurso
                                Case Eresources.SeleccioneUnRiesgoProfesional
                                    Return My.Resources.RecursoEN_US.SeleccioneUnRiesgoProfesional
                            End Select

                        'Comunes - discapacidades
                        Case Eform.Discapacidad
                            Select Case recurso
                                Case Eresources.SeleccioneUnaDiscapacidad
                                    Return My.Resources.RecursoEN_US.SeleccioneUnaDiscapacidad
                            End Select

                        'Comunes - Tipo de contribuyente
                        Case Eform.TipoContribuyente
                            Select Case recurso
                                Case Eresources.SeleccioneUnTipocontribuyente
                                    Return My.Resources.RecursoEN_US.SeleccioneUnTipocontribuyente
                            End Select

                        'Comunes - Tipo de estudio
                        Case Eform.TipoEstudio
                            Select Case recurso
                                Case Eresources.SeleccioneUnTipoestudio
                                    Return My.Resources.RecursoEN_US.SeleccioneUnTipoestudio
                                Case Eresources.Primaria
                                    Return My.Resources.RecursoEN_US.Primaria
                                Case Eresources.Secundaria
                                    Return My.Resources.RecursoEN_US.Secundaria
                                Case Eresources.Tecnico
                                    Return My.Resources.RecursoEN_US.Tecnico
                                Case Eresources.Universitario
                                    Return My.Resources.RecursoEN_US.Universitario
                                Case Eresources.Postgrado
                                    Return My.Resources.RecursoEN_US.Postgrado
                            End Select

                        'Comunes - grupo de contratos
                        Case Eform.GrupoContrato
                            Select Case recurso
                                Case Eresources.SeleccioneUnGrupoContrato
                                    Return My.Resources.RecursoEN_US.SeleccioneUnGrupoContrato
                            End Select
                        'Formulaio de terceros
                        Case Eform.Terceros
                            Select Case recurso
                                Case Eresources.SeleccioneUnTercero
                                    Return My.Resources.RecursoEN_US.SeleccioneUnTercero
                                Case Else
                                    Return "Agregue este valor a la pila de recursos"
                            End Select
                        'Formulaio de tipo de contrato
                        Case Eform.TipoDeContrato
                            Select Case recurso
                                Case Eresources.SeleccioneUnTipodeContrato
                                    Return My.Resources.RecursoEN_US.SeleccioneUnTipodeContrato
                                Case Else
                                    Return "Agregue este valor a la pila de recursos"
                            End Select
                        'Nomina - Cargos
                        Case Eform.Cargo
                            Select Case recurso
                                Case Eresources.SeleccioneUnCargo
                                    Return My.Resources.RecursoEN_US.SeleccioneUnCargo
                            End Select


                        'Formulario de cetros de estudio
                        Case Eform.CentrosdeEstudio
                            Select Case recurso
                                Case Eresources.SeleccioneUnCentrodeEstudio
                                    Return My.Resources.RecursoEN_US.SeleccioneUnCentrodeEstudio
                                Case Else
                                    Return "Agregue este valor a la pila de recursos"
                            End Select
                        'Formulario de plantilla de contratos
                        Case Eform.PlantillaContrato
                            Select Case recurso
                                Case Eresources.SeleccionePlantilladeContrato
                                    Return My.Resources.RecursoEN_US.SeleccionePlantilladeContrato
                                Case Else
                                    Return "Agregue este valor a la pila de recursos"
                            End Select
                        Case Eform.Retenciones
                            Select Case recurso
                                Case Eresources.SeleccioneRetencion
                                    Return My.Resources.RecursoEN_US.SeleccioneRetencion
                                Case Else
                                    Return "Agregue este valor a la pila de recursos"
                            End Select
                        'Formulaio de tipo de vinculacion
                        Case Eform.TipodeVinculacion
                            Select Case recurso
                                Case Eresources.SeleccioneUnTipodeVinculacion
                                    Return My.Resources.RecursoEN_US.SeleccioneUnTipodeVinculacion
                                Case Else
                                    Return "Agregue este valor a la pila de recursos"
                            End Select
                        'Formulaio de unidad de tiempo
                        Case Eform.UnidaddeTiempo
                            Select Case recurso
                                Case Eresources.SeleccioneUnaUnidaddeTiempo
                                    Return My.Resources.RecursoEN_US.SeleccioneUnaUnidaddeTiempo
                                Case Else
                                    Return "Agregue este valor a la pila de recursos"
                            End Select
                        'Formulario de autorizacion de concepto
                        Case Eform.AutorizacionConceptos
                            Select Case recurso
                                Case Eresources.ConceptoAutorizadoPorGrupo
                                    Return My.Resources.RecursoEN_US.ConceptoAutorizadoPorGrupo
                                Case Eresources.ConceptosAutorizadoPorGrupo
                                    Return My.Resources.RecursoEN_US.ConceptosAutorizadoPorGrupo
                                Case Eresources.ProcesoRealizadoConExito
                                    Return My.Resources.RecursoEN_US.ProcesoRealizadoConExito
                                Case Eresources.ClaseConceptoNoPermiteFormula
                                    Return My.Resources.RecursoEN_US.ClaseConceptoNoPermiteFormula
                                Case Else
                                    Return "Agregue este valor a la pila de recursos"
                            End Select
                        'Formulario de Convenios
                        Case Eform.Agreements
                            Select Case recurso
                                Case Eresources.PaidValueExceeds
                                    Return My.Resources.RecursoEN_US.PaidValueExceeds
                                Case Eresources.TipoPorNomina
                                    Return My.Resources.RecursoEN_US.TipoPorNomina
                                Case Eresources.TipoPorArchivo
                                    Return My.Resources.RecursoEN_US.TipoPorArchivo
                                Case Eresources.TipoManual
                                    Return My.Resources.RecursoEN_US.TipoManual
                            End Select
                        'Formulario de Conceptos manuales
                        Case Eform.ConceptosManuales
                            Select Case recurso
                                Case Eresources.EstadoActivo
                                    Return My.Resources.RecursoEN_US.EstadoActivo
                                Case Eresources.EstadoSuspendido
                                    Return My.Resources.RecursoEN_US.EstadoSuspendido
                                Case Eresources.EstadoCompletado
                                    Return My.Resources.RecursoEN_US.EstadoCompletado
                                Case Eresources.FormPagoPrimeraQuincena
                                    Return My.Resources.RecursoEN_US.FormPagoPrimeraQuincena
                                Case Eresources.FormPagoSegundaQuincena
                                    Return My.Resources.RecursoEN_US.FormPagoSegundaQuincena
                                Case Eresources.FormPagoAmbas
                                    Return My.Resources.RecursoEN_US.FormPagoAmbas
                                Case Eresources.FormPagoMensual
                                    Return My.Resources.RecursoEN_US.FormPagoMensual

                            End Select
                        'formulario de empleado
                        Case Eform.Empleado
                            Select Case recurso
                            'Tipos de cesantias
                                Case Eresources.NoAplica
                                    Return My.Resources.RecursoEN_US.NoAplica
                                Case Eresources.Tradicional
                                    Return My.Resources.RecursoEN_US.Tradicional
                                Case Eresources.TradicionalMensual
                                    Return My.Resources.RecursoEN_US.TradicionalMensual
                                Case Eresources.Consignado
                                    Return My.Resources.RecursoEN_US.Consignado
                                Case Eresources.ConsignadoMensual
                                    Return My.Resources.RecursoEN_US.ConsignadoMensual
                                Case Eresources.Ley33
                                    Return My.Resources.RecursoEN_US.Ley33
                                'tipos de sindicato
                                Case Eresources.Convencionado
                                    Return My.Resources.RecursoEN_US.Convencionado
                                Case Eresources.Sindicalizado
                                    Return My.Resources.RecursoEN_US.Sindicalizado
                                Case Eresources.PactoColectivo
                                    Return My.Resources.RecursoEN_US.PactoColectivo
                                'Estado de estudios
                                Case Eresources.EstudiaActualmente
                                    Return My.Resources.RecursoEN_US.EstudiaActualmente
                                Case Eresources.EstudioInterrumpido
                                    Return My.Resources.RecursoEN_US.EstudioInterrumpido
                                Case Eresources.EstudioTerminado
                                    Return My.Resources.RecursoEN_US.EstudioTerminado
                                Case Eresources.Graduado
                                    Return My.Resources.RecursoEN_US.Graduado
                                'Libreta militar
                                Case Eresources.NoTieneLibreta
                                    Return My.Resources.RecursoEN_US.NoTieneLibreta
                                Case Eresources.LibretaPrimera
                                    Return My.Resources.RecursoEN_US.LibretaPrimera
                                Case Eresources.LibretaSegunda
                                    Return My.Resources.RecursoEN_US.LibretaSegunda
                                'tipos de retencion
                                Case Eresources.Autoretenedor
                                    Return My.Resources.RecursoEN_US.Autoretenedor
                                Case Eresources.HaceRetencion
                                    Return My.Resources.RecursoEN_US.HaceRetencion
                                Case Eresources.ExentoRetencion
                                    Return My.Resources.RecursoEN_US.ExentoRetencion
                                'tipo de educacion
                                Case Eresources.Formal
                                    Return My.Resources.RecursoEN_US.Formal
                                Case Eresources.NoFormal
                                    Return My.Resources.RecursoEN_US.NoFormal
                                Case Eresources.MinimoQuinceLaborar
                                    Return My.Resources.RecursoEN_US.MinimoQuinceLaborar
                                Case Eresources.FechaXMenorQueY
                                    Return My.Resources.RecursoEN_US.FechaXMenorQueY
                                Case Eresources.EmpleadoNoSeElimino
                                    Return My.Resources.RecursoEN_US.EmpleadoNoSeElimino
                                Case Eresources.EmpleadoTieneLiquidaciones
                                    Return My.Resources.RecursoEN_US.EmpleadoTieneLiquidaciones
                                Case Eresources.EmpleadoTieneLiquidacionContrato
                                    Return My.Resources.RecursoEN_US.EmpleadoTieneLiquidacionContrato
                                Case Eresources.EmpleadoTieneNovedades
                                    Return My.Resources.RecursoEN_US.EmpleadoTieneNovedades
                                Case Eresources.EmpleadoTieneTurno
                                    Return My.Resources.RecursoEN_US.EmpleadoTieneTurno
                                Case Eresources.EmpleadoTieneConvenios
                                    Return My.Resources.RecursoEN_US.EmpleadoTieneConvenios
                                Case Eresources.EmpleadoTieneConvenios
                                    Return My.Resources.RecursoEN_US.EmpleadoTieneConvenios
                                Case Else
                                    Return "Agregue este valor a la pila de recursos"
                            End Select
                        'formulario de contrato
                        Case Eform.Contrato
                            Select Case recurso
                            'Tipo de salario
                                Case Eresources.Fijo
                                    Return My.Resources.RecursoEN_US.Fijo
                                Case Eresources.Integral
                                    Return My.Resources.RecursoEN_US.Integral
                                Case Eresources.Variable
                                    Return My.Resources.RecursoEN_US.Variable
                                'Periodo de pago
                                Case Eresources.Mensual
                                    Return My.Resources.RecursoEN_US.Mensual
                                Case Eresources.Quincenal
                                    Return My.Resources.RecursoEN_US.Quincenal
                                'Tipo de pago
                                Case Eresources.Cheque
                                    Return My.Resources.RecursoEN_US.Cheque
                                Case Eresources.Efectivo
                                    Return My.Resources.RecursoEN_US.Efectivo
                                Case Eresources.Consignacion
                                    Return My.Resources.RecursoEN_US.Consignacion
                                Case Eresources.Desprendible
                                    Return My.Resources.RecursoEN_US.Desprendible
                                'Tipo de cuenta bancaria"
                                Case Eresources.Ahorros
                                    Return My.Resources.RecursoEN_US.Ahorros
                                Case Eresources.Corriente
                                    Return My.Resources.RecursoEN_US.Corriente
                                'Tipo de empleado
                                Case Eresources.Administrativo
                                    Return My.Resources.RecursoEN_US.Administrativo
                                Case Eresources.Asistencial
                                    Return My.Resources.RecursoEN_US.Asistencial
                                'Campos de contrato
                                Case Eresources.Id
                                    Return My.Resources.RecursoEN_US.Id
                                Case Eresources.RowType
                                    Return My.Resources.RecursoEN_US.RowType
                                Case Eresources.InitialContractNumber
                                    Return My.Resources.RecursoEN_US.InitialContractNumber
                                Case Eresources.Position
                                    Return My.Resources.RecursoEN_US.Position
                                Case Eresources.Group
                                    Return My.Resources.RecursoEN_US.Group
                                Case Eresources.FunctionalUnit
                                    Return My.Resources.RecursoEN_US.FunctionalUnit
                                Case Eresources.ContractType
                                    Return My.Resources.RecursoEN_US.ContractType
                                Case Eresources.JobBondingType
                                    Return My.Resources.RecursoEN_US.JobBondingType
                                Case Eresources.JobBondingDate
                                    Return My.Resources.RecursoEN_US.JobBondingDate
                                Case Eresources.ContractInitialDate
                                    Return My.Resources.RecursoEN_US.ContractInitialDate
                                Case Eresources.ContractEndingDate
                                    Return My.Resources.RecursoEN_US.ContractEndingDate
                                Case Eresources.BasicSalary
                                    Return My.Resources.RecursoEN_US.BasicSalary
                                Case Eresources.Status
                                    Return My.Resources.RecursoEN_US.Status
                                Case Eresources.RetirementReason
                                    Return My.Resources.RecursoEN_US.RetirementReason
                                Case Eresources.RetirementDate
                                    Return My.Resources.RecursoEN_US.RetirementDate
                                Case Eresources.PaymentPeriod
                                    Return My.Resources.RecursoEN_US.PaymentPeriod
                                Case Eresources.SalaryType
                                    Return My.Resources.RecursoEN_US.SalaryType
                                Case Eresources.PaymentType
                                    Return My.Resources.RecursoEN_US.PaymentType
                                Case Eresources.TrialPeriod
                                    Return My.Resources.RecursoEN_US.TrialPeriod
                                Case Eresources.TrialPeriodTime
                                    Return My.Resources.RecursoEN_US.TrialPeriodTime
                                Case Eresources.TrialPeriodSalaryPercentage
                                    Return My.Resources.RecursoEN_US.TrialPeriodSalaryPercentage
                                Case Eresources.AutoRenew
                                    Return My.Resources.RecursoEN_US.AutoRenew
                                Case Eresources.ContractCreationDate
                                    Return My.Resources.RecursoEN_US.ContractCreationDate
                                Case Eresources.Valid
                                    Return My.Resources.RecursoEN_US.Valid
                                Case Eresources.Notes
                                    Return My.Resources.RecursoEN_US.Notes
                                Case Eresources.Bank
                                    Return My.Resources.RecursoEN_US.Bank
                                Case Eresources.BankAccountNumber
                                    Return My.Resources.RecursoEN_US.BankAccountNumber
                                Case Eresources.BankAccountType
                                    Return My.Resources.RecursoEN_US.BankAccountType
                                'Tipos de registro
                                Case Eresources.ContratoBase
                                    Return My.Resources.RecursoEN_US.ContratoBase
                                Case Eresources.Novedad
                                    Return My.Resources.RecursoEN_US.Novedad
                                'Duracion del contrato
                                Case Eresources.Indefinido
                                    Return My.Resources.RecursoEN_US.Indefinido
                                Case Eresources.Ano
                                    Return My.Resources.RecursoEN_US.Ano
                                Case Eresources.Mes
                                    Return My.Resources.RecursoEN_US.Mes
                                Case Eresources.Dia
                                    Return My.Resources.RecursoEN_US.Dia
                                'General
                                Case Eresources.Mas
                                    Return My.Resources.RecursoEN_US.Mas
                                'Clase de contrato
                                Case Eresources.ClaseContratoOtros
                                    Return My.Resources.RecursoEN_US.ClaseContratoOtros
                                Case Eresources.ClaseContratoAprendizaje
                                    Return My.Resources.RecursoEN_US.ClaseContratoAprendizaje
                                Case Eresources.ClaseContratoLaboralFijo
                                    Return My.Resources.RecursoEN_US.ClaseContratoLaboralFijo
                                Case Eresources.ClaseContratoLaboralIndefinido
                                    Return My.Resources.RecursoEN_US.ClaseContratoLaboralIndefinido
                                Case Eresources.ClaseContratoPracticasoPasantiasUniversitarias
                                    Return My.Resources.RecursosES_CO.ClaseContratoPracticasoPasantiasUniversitarias
                                'Tipos de fondos
                                Case Eresources.Salud
                                    Return My.Resources.RecursoEN_US.Salud
                                Case Eresources.Pension
                                    Return My.Resources.RecursoEN_US.Pension
                                Case Eresources.Riesgos
                                    Return My.Resources.RecursoEN_US.Riesgos
                                Case Eresources.Cesantias
                                    Return My.Resources.RecursoEN_US.Cesantias
                                Case Eresources.CajaCompensacion
                                    Return My.Resources.RecursoEN_US.CajaCompensacion
                                '------------ Varios
                                Case Eresources.ContratosNo2Vigentes
                                    Return My.Resources.RecursoEN_US.ContratosNo2Vigentes
                                Case Eresources.ContratosEnRangoOtroContrato
                                    Return My.Resources.RecursoEN_US.ContratosEnRangoOtroContrato
                                Case Eresources.FondoArlObligatorio
                                    Return My.Resources.RecursoEN_US.FondoArlObligatorio
                                Case Eresources.FondoSaludObligatorio
                                    Return My.Resources.RecursoEN_US.FondoSaludObligatorio
                                Case Eresources.FondoPensionesObligatorio
                                    Return My.Resources.RecursoEN_US.FondoPensionesObligatorio
                                Case Eresources.ContratosFechaMinimaPermitida
                                    Return My.Resources.RecursoEN_US.ContratosFechaMinimaPermitida
                                Case Eresources.ContratosFechaMaximaContratoFijo
                                    Return My.Resources.RecursoEN_US.ContratosFechaMaximaContratoFijo
                                Case Eresources.ContratoRangoDeFechas
                                    Return My.Resources.RecursoEN_US.ContratoRangoDeFechas
                                Case Eresources.FondoYaSeEncuentra
                                    Return My.Resources.RecursoEN_US.FondoYaSeEncuentra
                                Case Eresources.ContratosSalarioVsCargo
                                    Return My.Resources.RecursoEN_US.ContratosSalarioVsCargo

                                Case Eresources.ContratosRenovacion
                                    Return My.Resources.RecursoEN_US.ContratosRenovacion
                                Case Eresources.FondoFechasEntreAfiliaciones
                                    Return My.Resources.RecursoEN_US.FondoFechasEntreAfiliaciones
                                Case Eresources.FondoFechasEnRangoFondo
                                    Return My.Resources.RecursoEN_US.FondoFechasEnRangoFondo
                                Case Eresources.ContratosSinCambio
                                    Return My.Resources.RecursoEN_US.ContratosSinCambio
                                Case Eresources.FondoFechaMinimaContrato
                                    Return My.Resources.RecursoEN_US.FondoFechaMinimaContrato
                                Case Eresources.FondoAccionesInactivos
                                    Return My.Resources.RecursoEN_US.FondoAccionesInactivos
                                Case Eresources.ContratosSalarioVsAnteriorSalario
                                    Return My.Resources.RecursoEN_US.ContratosSalarioVsAnteriorSalario

                                Case Eresources.ContratosCrearNuevo
                                    Return My.Resources.RecursoEN_US.ContratosCrearNuevo
                                Case Eresources.ContratosConCuadroTurnos
                                    Return My.Resources.RecursoEN_US.ContratosConCuadroTurnos
                                Case Eresources.ContratosEliminarAdvertencia
                                    Return My.Resources.RecursoEN_US.ContratosEliminarAdvertencia
                                Case Eresources.ContratosConNominaPagada
                                    Return My.Resources.RecursoEN_US.ContratosConNominaPagada
                            End Select
                        '/******** Sistem Documental **********/
                        'Frontal de MetaData
                        Case Eform.MetaData
                            Select Case recurso
                                Case Eresources.ErrorGuardarDocumento
                                    Return My.Resources.RecursoEN_US.ErrorGuardarDocumento
                                Case Eresources.BotonGuardarDocumento
                                    Return My.Resources.RecursoEN_US.BotonGuardarDocumento
                                Case Eresources.RadioGrupoDocumento
                                    Return My.Resources.RecursoEN_US.RadioGrupoDocumento
                                Case Eresources.TituloMetada
                                    Return My.Resources.RecursoEN_US.TituloMetada
                                Case Eresources.NoExisteArchivador
                                    Return My.Resources.RecursoEN_US.NoExisteArchivador
                            End Select

                        'Frontal de Digitalización
                        Case Eform.Digitalizacion
                            Select Case recurso
                                Case Eresources.TituloDigitalizacion
                                    Return My.Resources.RecursoEN_US.TituloDigitalizacion
                                Case Eresources.DeDigitalización
                                    Return My.Resources.RecursoEN_US.DeDigitalización
                                Case Eresources.PaginaDigitalizacion
                                    Return My.Resources.RecursoEN_US.PaginaDigitalizacion
                                Case Eresources.SeleccioneDispositivo
                                    Return My.Resources.RecursoEN_US.SeleccioneDispositivo
                                Case Eresources.EscaneoDuplex
                                    Return My.Resources.RecursoEN_US.EscaneoDuplex
                            End Select

                        'Frontal de adjuntar
                        Case Eform.Adjuntar
                            Select Case recurso
                                Case Eresources.TituloAdjuntar
                                    Return My.Resources.RecursoEN_US.TituloAdjuntar
                            End Select

                        'Control de usuario Barra botones
                        Case Eform.CtrBarraBotones
                            Select Case recurso
                                Case Eresources.SinArchivador
                                    Return My.Resources.RecursoEN_US.SinArchivador
                                Case Eresources.SinDocumentos
                                    Return My.Resources.RecursoEN_US.SinDocumentos
                                Case Eresources.SinPermisoAbrirDocumentos
                                    Return My.Resources.RecursoEN_US.SinPermisoAbrirDocumentos
                            End Select
                        'Frontal de Traslado Cobro Jurídico
                        Case Eform.TrasladoCobroJurídico
                            Select Case recurso
                                Case Eresources.NoExisteTrasladoCobroJuridico
                                    Return My.Resources.RecursoEN_US.NoExisteTrasladoCobroJuridico
                                Case Eresources.FacturaYaExisteTrasladoCobroJuridico
                                    Return My.Resources.RecursoEN_US.FacturaYaExisteTrasladoCobroJuridico
                            End Select
                        Case Eform.InfoMetaData
                            Select Case recurso
                                Case Eresources.FrmEvaluationMetaData
                                    Return My.Resources.RecursoEN_US.FrmEvaluationMetaData
                                Case Eresources.FrmEvaluationMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmEvaluationMetaDataTitle
                                Case Eresources.FrmCoordinationMetaData
                                    Return My.Resources.RecursoEN_US.FrmCoordinationMetaData
                                Case Eresources.FrmCoordinationMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmCoordinationMetaDataTitle
                                Case Eresources.FrmDevolutionMetaData
                                    Return My.Resources.RecursoEN_US.FrmDevolutionMetaData
                                Case Eresources.FrmDevolutionMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmDevolutionMetaDataTitle
                                Case Eresources.FrmDevolutionMetaDataDetail
                                    Return My.Resources.RecursoEN_US.FrmDevolutionMetaDataDetail
                                Case Eresources.FrmJuridicalDebtMetaData
                                    Return My.Resources.RecursoEN_US.FrmJuridicalDebtMetaData
                                Case Eresources.FrmJuridicalDebtMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmJuridicalDebtMetaDataTitle
                                Case Eresources.FrmJuridicalDebtMetaDataDetail
                                    Return My.Resources.RecursoEN_US.FrmJuridicalDebtMetaDataDetail
                                Case Eresources.FrmTimeParametersMetaData
                                    Return My.Resources.RecursoEN_US.FrmTimeParametersMetaData
                                Case Eresources.FrmTimeParametersMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmTimeParametersMetaDataTitle
                                Case Eresources.FrmCustomersMetaData
                                    Return My.Resources.RecursoEN_US.FrmCustomersMetaData
                                Case Eresources.FrmCustomersMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmCustomersMetaDataTitle
                                Case Eresources.DocumentsMetaDataTitle
                                    Return My.Resources.RecursoEN_US.DocumentsMetaDataTitle
                                Case Eresources.FrmResponsibleMetaData
                                    Return My.Resources.RecursoEN_US.FrmResponsibleMetaData
                                Case Eresources.FrmResponsibleMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmResponsibleMetaDataTitle
                                Case Eresources.FrmCompanyMetaData
                                    Return My.Resources.RecursoEN_US.FrmCompanyMetaData
                                Case Eresources.FrmCompanyMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmCompanyMetaDataTitle
                                Case Eresources.FrmJustificationTemplateMetaData
                                    Return My.Resources.RecursoEN_US.FrmJustificationTemplateMetaData
                                Case Eresources.FrmJustificationTemplateMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmJustificationTemplateMetaDataTitle
                                Case Eresources.FrmHealthCenterMetaData
                                    Return My.Resources.RecursoEN_US.FrmHealthCenterMetaData
                                Case Eresources.FrmHealthCenterMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmHealthCenterMetaDataTitle
                                Case Eresources.FrmBankMetaData
                                    Return My.Resources.RecursoEN_US.FrmBankMetaData
                                Case Eresources.FrmBankMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmBankMetaDataTitle
                                Case Eresources.FrmWorkCenterMetaData
                                    Return My.Resources.RecursoEN_US.FrmWorkCenterMetaData
                                Case Eresources.FrmWorkCenterMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmWorkCenterMetaDataTitle
                                Case Eresources.FrmCityMetaData
                                    Return My.Resources.RecursoEN_US.FrmCityMetaData
                                Case Eresources.FrmCityMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmCityMetaDataTitle
                                Case Eresources.FrmStudyCenterMetaData
                                    Return My.Resources.RecursoEN_US.FrmStudyCenterMetaData
                                Case Eresources.FrmStudyCenterMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmStudyCenterMetaDataTitle
                                Case Eresources.FrmStudyTypeMetaData
                                    Return My.Resources.RecursoEN_US.FrmStudyTypeMetaData
                                Case Eresources.FrmStudyTypeMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmStudyTypeMetaDataTitle
                                Case Eresources.FrmRetirementReasonMetaData
                                    Return My.Resources.RecursoEN_US.FrmRetirementReasonMetaData
                                Case Eresources.FrmRetirementReasonMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmRetirementReasonMetaDataTitle
                                Case Eresources.FrmProfessionMetaData
                                    Return My.Resources.RecursoEN_US.FrmProfessionMetaData
                                Case Eresources.FrmProfessionMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmProfessionMetaDataTitle
                                Case Eresources.FrmProfessionalRiskMetaData
                                    Return My.Resources.RecursoEN_US.FrmProfessionalRiskMetaData
                                Case Eresources.FrmProfessionalRiskMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmProfessionalRiskMetaDataTitle
                                Case Eresources.FrmPositionLevelMetaData
                                    Return My.Resources.RecursoEN_US.FrmPositionLevelMetaData
                                Case Eresources.FrmPositionLevelMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmPositionLevelMetaDataTitle
                                Case Eresources.FrmPositionMetaData
                                    Return My.Resources.RecursoEN_US.FrmPositionMetaData
                                Case Eresources.FrmPositionMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmPositionMetaDataTitle
                                Case Eresources.FrmPensionaryTypeMetaData
                                    Return My.Resources.RecursoEN_US.FrmPensionaryTypeMetaData
                                Case Eresources.FrmPensionaryTypeMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmPensionaryTypeMetaDataTitle
                                Case Eresources.FrmLanguageMetaData
                                    Return My.Resources.RecursoEN_US.FrmLanguageMetaData
                                Case Eresources.FrmLanguageMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmLanguageMetaDataTitle
                                Case Eresources.FrmKinshipMetaData
                                    Return My.Resources.RecursoEN_US.FrmKinshipMetaData
                                Case Eresources.FrmKinshipMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmKinshipMetaDataTitle
                                Case Eresources.FrmDepartmentMetaData
                                    Return My.Resources.RecursoEN_US.FrmDepartmentMetaData
                                Case Eresources.FrmDepartmentMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmDepartmentMetaDataTitle
                                Case Eresources.FrmCurrencyMetaData
                                    Return My.Resources.RecursoEN_US.FrmCurrencyMetaData
                                Case Eresources.FrmCurrencyMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmCurrencyMetaDataTitle
                                Case Eresources.FrmCountryMetaData
                                    Return My.Resources.RecursoEN_US.FrmCountryMetaData
                                Case Eresources.FrmCountryMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmCountryMetaDataTitle
                                Case Eresources.FrmDisabilityMetaData
                                    Return My.Resources.RecursoEN_US.FrmDisabilityMetaData
                                Case Eresources.FrmDisabilityMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmDisabilityMetaDataTitle
                                Case Eresources.FrmThirdPartyMetaData
                                    Return My.Resources.RecursoEN_US.FrmThirdPartyMetaData
                                Case Eresources.FrmThirdPartyMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmThirdPartyMetaDataTitle
                                Case Eresources.FrmJobBondingTypeMetaData
                                    Return My.Resources.RecursoEN_US.FrmJobBondingTypeMetaData
                                Case Eresources.FrmJobBondingTypeMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmJobBondingTypeMetaDataTitle
                                Case Eresources.FrmGroupMetaData
                                    Return My.Resources.RecursoEN_US.FrmGroupMetaData
                                Case Eresources.FrmGroupMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmGroupMetaDataTitle
                                Case Eresources.FrmFundMetaData
                                    Return My.Resources.RecursoEN_US.FrmFundMetaData
                                Case Eresources.FrmFundMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmFundMetaDataTitle
                                Case Eresources.FrmFunctionalUnitMetaData
                                    Return My.Resources.RecursoEN_US.FrmFunctionalUnitMetaData
                                Case Eresources.FrmFunctionalUnitMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmFunctionalUnitMetaDataTitle
                                Case Eresources.FrmGroupMetaData
                                    Return My.Resources.RecursoEN_US.FrmGroupsMetaData
                                Case Eresources.FrmGroupMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmGroupsMetaDataTitle
                                Case Eresources.FrmCompanyPayrollMetaData
                                    Return My.Resources.RecursoEN_US.FrmCompanyPayrollMetaData
                                Case Eresources.FrmCompanyPayrollMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmCompanyPayrollMetaDataTitle
                                Case Eresources.FrmConceptsMetaData
                                    Return My.Resources.RecursoEN_US.FrmConceptsMetaData
                                Case Eresources.FrmConceptsMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmConceptsMetaDataTitle
                                Case Eresources.FrmTimeUnitMetaData
                                    Return My.Resources.RecursoEN_US.FrmTimeUnitMetaData
                                Case Eresources.FrmTimeUnitMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmTimeUnitMetaDataTitle
                                Case Eresources.FrmContractGroupMetaData
                                    Return My.Resources.RecursoEN_US.FrmContractGroupMetaData
                                Case Eresources.FrmContractGroupMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmContractGroupMetaDataTitle
                                Case Eresources.FrmContractModificationReasonMetaData
                                    Return My.Resources.RecursoEN_US.FrmContractModificationReasonMetaData
                                Case Eresources.FrmContractModificationReasonMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmContractModificationReasonMetaDataTitle
                                Case Eresources.FrmContractTemplateMetaData
                                    Return My.Resources.RecursoEN_US.FrmContractTemplateMetaData
                                Case Eresources.FrmContractTemplateMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmContractTemplateMetaDataTitle
                                Case Eresources.FrmContractTypeMetaData
                                    Return My.Resources.RecursoEN_US.FrmContractTypeMetaData
                                Case Eresources.FrmContractTypeMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmContractTypeMetaDataTitle
                                Case Eresources.FrmContributorTypeMetaData
                                    Return My.Resources.RecursoEN_US.FrmContributorTypeMetaData
                                Case Eresources.FrmContributorTypeMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmContributorTypeMetaDataTitle
                                Case Eresources.FrmCostCenterMetaData
                                    Return My.Resources.RecursoEN_US.FrmCostCenterMetaData
                                Case Eresources.FrmCostCenterMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmCostCenterMetaDataTitle
                                Case Eresources.FrmEducationLevelsMetaData
                                    Return My.Resources.RecursoEN_US.FrmEducationLevelsMetaData
                                Case Eresources.FrmEducationLevelsMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmEducationLevelsMetaDataTitle
                                Case Eresources.FrmEmployeeTypeMetaData
                                    Return My.Resources.RecursoEN_US.FrmEmployeeTypeMetaData
                                Case Eresources.FrmEmployeeTypeMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmEmployeeTypeMetaDataTitle
                                Case Eresources.FrmAuthorizationConceptMetaData
                                    Return My.Resources.RecursoEN_US.FrmAuthorizationConceptMetaData
                                Case Eresources.FrmAuthorizationConceptMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmAuthorizationConceptMetaDataTitle
                                Case Eresources.FrmKindsAgrementsMetaData
                                    Return My.Resources.RecursoEN_US.FrmKindsAgrementsMetaData
                                Case Eresources.FrmKindsAgrementsMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmKindsAgrementsMetaDataTitle
                                Case Eresources.FrmFunctionalAgreementsMetaData
                                    Return My.Resources.RecursoEN_US.FrmFunctionalAgreementsMetaData
                                Case Eresources.FrmFunctionalAgreementsMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmFunctionalAgreementsMetaDataTitle
                                Case Eresources.FrmNoveltyMetaData
                                    Return My.Resources.RecursoEN_US.FrmNoveltyMetaData
                                Case Eresources.FrmNoveltyMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmNoveltyMetaDataTitle
                                Case Eresources.FrmPhoneTypeMetaData
                                    Return My.Resources.RecursoEN_US.FrmPhoneTypeMetaData
                                Case Eresources.FrmPhoneTypeMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmPhoneTypeMetaDataTitle
                                Case Eresources.FrmScheduleTemplateMetaData
                                    Return My.Resources.RecursoEN_US.FrmScheduleTemplateMetaData
                                Case Eresources.FrmScheduleTemplateMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmScheduleTemplateMetaDataTitle
                                Case Eresources.FrmEmployeeMetaData
                                    Return My.Resources.RecursoEN_US.FrmEmployeeMetaData
                                Case Eresources.FrmEmployeeMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmEmployeeMetaDataTitle
                                Case Eresources.FrmManualConceptsMetaData
                                    Return My.Resources.RecursoEN_US.FrmManualConceptsMetaData
                                Case Eresources.FrmManualConceptsMetaDataTitle
                                    Return My.Resources.RecursoEN_US.FrmManualConceptsMetaDataTitle
                            End Select

                        Case Eform.AumentoSalario
                            Select Case recurso
                                Case Eresources.PorcentajeCero
                                    Return My.Resources.RecursoEN_US.PorcentajeCero
                                Case Eresources.FechaRetroactivo
                                    Return My.Resources.RecursoEN_US.FechaRetroactivo
                                Case Eresources.ModificacionSueldosCorrecta
                                    Return My.Resources.RecursoEN_US.ModificacionSueldosCorrecta
                            End Select
                              'frontal de archivo plano para banco
                        Case Eform.ArchivoBanco
                            Select Case recurso
                                Case Eresources.GrupoNoTieneEstructuraArch
                                    Return My.Resources.RecursoEN_US.GrupoNoTieneEstructuraArch
                                Case Eresources.GuardadoDeseaAbrir
                                    Return My.Resources.RecursoEN_US.GuardadoDeseaAbrir
                                Case Eresources.ExisteAuditoriaArchivoBanco
                                    Return My.Resources.RecursoEN_US.ExisteAuditoriaArchivoBanco
                            End Select
                    End Select

                    'CARGA POR DEFECTOS LOS RECURSOS DE COLOMBIA 
                Case Else

                    'se realiza un select case por tipo de frontal para no forzar el case
                    'a buscar entre todos los posibles  mensajes, esto me brinda mejorar la busqueda
                    'y mantener mi codigo mejor organizado.
                    Select Case frontal

                    'Recursos para el sistema de mensajeria
                        Case Eform.Messages
                            Select Case recurso
                                Case Eresources.MessagesTitle
                                    Return My.Resources.RecursosES_CO.MessagesTitle
                                Case Eresources.MeesagesRead
                                    Return My.Resources.RecursosES_CO.MeesagesRead
                                Case Eresources.MessagesWriting
                                    Return My.Resources.RecursosES_CO.MessagesWriting
                                Case Eresources.MessagesGroupInvitation
                                    Return My.Resources.RecursosES_CO.MessagesGroupInvitation
                                Case Eresources.MessagesGroupLeave
                                    Return My.Resources.RecursosES_CO.MessagesGroupLeave
                            End Select
                        Case Eform.LiquidacionContrato
                            Select Case recurso
                                Case Eresources.EmpleadoTieneErrores
                                    Return My.Resources.RecursosES_CO.EmpleadoTieneErrores
                                Case Eresources.AlgunEmpleadoTieneErrores
                                    Return My.Resources.RecursosES_CO.AlgunEmpleadoTieneErrores
                            End Select
                        Case Eform.Autoliquidation
                            Select Case recurso
                                Case Eresources.EmpleadoMasDeUnaEPS
                                    Return My.Resources.RecursosES_CO.EmpleadoMasDeUnaEPS
                                Case Eresources.EmpleadoNoTieneEPS
                                    Return My.Resources.RecursosES_CO.EmpleadoNoTieneEPS
                                Case Eresources.EmpleadoMasFondoPension
                                    Return My.Resources.RecursosES_CO.EmpleadoMasFondoPension
                                Case Eresources.EmpleadoNoTieneFondoPension
                                    Return My.Resources.RecursosES_CO.EmpleadoNoTieneFondoPension
                                Case Eresources.EmpleadoMasCajaCompensacion
                                    Return My.Resources.RecursosES_CO.EmpleadoMasCajaCompensacion
                                Case Eresources.EmpleadoNoTieneCajaCompensacion
                                    Return My.Resources.RecursosES_CO.EmpleadoNoTieneCajaCompensacion
                            End Select
                        'Climas de login
                        Case Eform.Weather
                            Select Case recurso
                                Case Eresources.Weatherblowingsnow
                                    Return My.Resources.RecursosES_CO.Weatherblowingsnow
                                Case Eresources.Weatherblustery
                                    Return My.Resources.RecursosES_CO.Weatherblustery
                                Case Eresources.Weatherclearnight
                                    Return My.Resources.RecursosES_CO.Weatherclearnight
                                Case Eresources.Weathercloudy
                                    Return My.Resources.RecursosES_CO.Weathercloudy
                                Case Eresources.Weathercold
                                    Return My.Resources.RecursosES_CO.Weathercold
                                Case Eresources.Weatherdrizzle
                                    Return My.Resources.RecursosES_CO.Weatherdrizzle
                                Case Eresources.Weatherdust
                                    Return My.Resources.RecursosES_CO.Weatherdust
                                Case Eresources.Weatherfairday
                                    Return My.Resources.RecursosES_CO.Weatherfairday
                                Case Eresources.Weatherfairnight
                                    Return My.Resources.RecursosES_CO.Weatherfairnight
                                Case Eresources.Weatherfoggy
                                    Return My.Resources.RecursosES_CO.Weatherfoggy
                                Case Eresources.Weatherfreezingdrizzle
                                    Return My.Resources.RecursosES_CO.Weatherfreezingdrizzle
                                Case Eresources.Weatherfreezingrain
                                    Return My.Resources.RecursosES_CO.Weatherfreezingrain
                                Case Eresources.Weatherhail
                                    Return My.Resources.RecursosES_CO.Weatherhail
                                Case Eresources.Weatherhaze
                                    Return My.Resources.RecursosES_CO.Weatherhaze
                                Case Eresources.Weatherheavysnow
                                    Return My.Resources.RecursosES_CO.Weatherheavysnow
                                Case Eresources.Weatherhot
                                    Return My.Resources.RecursosES_CO.Weatherhot
                                Case Eresources.Weatherhurricane
                                    Return My.Resources.RecursosES_CO.Weatherhurricane
                                Case Eresources.Weatherisolatedthundershowers
                                    Return My.Resources.RecursosES_CO.Weatherisolatedthundershowers
                                Case Eresources.Weatherisolatedthunderstorms
                                    Return My.Resources.RecursosES_CO.Weatherisolatedthunderstorms
                                Case Eresources.Weatherlightsnowshowers
                                    Return My.Resources.RecursosES_CO.Weatherlightsnowshowers
                                Case Eresources.Weathermixedrainandhail
                                    Return My.Resources.RecursosES_CO.Weathermixedrainandhail
                                Case Eresources.Weathermixedrainandsleet
                                    Return My.Resources.RecursosES_CO.Weathermixedrainandsleet
                                Case Eresources.Weathermixedrainandsnow
                                    Return My.Resources.RecursosES_CO.Weathermixedrainandsnow
                                Case Eresources.Weathermixedsnowandsleet
                                    Return My.Resources.RecursosES_CO.Weathermixedsnowandsleet
                                Case Eresources.Weathermostlycloudyday
                                    Return My.Resources.RecursosES_CO.Weathermostlycloudyday
                                Case Eresources.Weathermostlycloudynight
                                    Return My.Resources.RecursosES_CO.Weathermostlycloudynight
                                Case Eresources.Weatherpartlycloudy
                                    Return My.Resources.RecursosES_CO.Weatherpartlycloudy
                                Case Eresources.Weatherpartlycloudyday
                                    Return My.Resources.RecursosES_CO.Weatherpartlycloudyday
                                Case Eresources.Weatherpartlycloudynight
                                    Return My.Resources.RecursosES_CO.Weatherpartlycloudynight
                                Case Eresources.Weatherscatteredshowers
                                    Return My.Resources.RecursosES_CO.Weatherscatteredshowers
                                Case Eresources.Weatherscatteredsnowshowers
                                    Return My.Resources.RecursosES_CO.Weatherscatteredsnowshowers
                                Case Eresources.Weatherscatteredthunderstorms
                                    Return My.Resources.RecursosES_CO.Weatherscatteredthunderstorms
                                Case Eresources.Weatherseverethunderstorms
                                    Return My.Resources.RecursosES_CO.Weatherseverethunderstorms
                                Case Eresources.Weathershowers
                                    Return My.Resources.RecursosES_CO.Weathershowers
                                Case Eresources.Weathersleet
                                    Return My.Resources.RecursosES_CO.Weathersleet
                                Case Eresources.Weathersmoky
                                    Return My.Resources.RecursosES_CO.Weathersmoky
                                Case Eresources.Weathersnow
                                    Return My.Resources.RecursosES_CO.Weathersnow
                                Case Eresources.Weathersnowflurries
                                    Return My.Resources.RecursosES_CO.Weathersnowflurries
                                Case Eresources.Weathersnowshowers
                                    Return My.Resources.RecursosES_CO.Weathersnowshowers
                                Case Eresources.Weathersunny
                                    Return My.Resources.RecursosES_CO.Weathersunny
                                Case Eresources.Weatherthundershowers
                                    Return My.Resources.RecursosES_CO.Weatherthundershowers
                                Case Eresources.Weatherthunderstorms
                                    Return My.Resources.RecursosES_CO.Weatherthunderstorms
                                Case Eresources.Weathertornado
                                    Return My.Resources.RecursosES_CO.Weathertornado
                                Case Eresources.Weathertropicalstorm
                                    Return My.Resources.RecursosES_CO.Weathertropicalstorm
                                Case Eresources.Weatherwindy
                                    Return My.Resources.RecursosES_CO.Weatherwindy

                            End Select

                        'Control Confirmados
                        Case Eform.CtrConfirmado
                            Select Case recurso
                                Case Eresources.Confirmado
                                    Return My.Resources.RecursosES_CO.Confirmado
                                Case Eresources.SinConfirmar
                                    Return My.Resources.RecursosES_CO.SinConfirmar
                                Case Eresources.Anulado
                                    Return My.Resources.RecursosES_CO.Anulado
                                Case Eresources.OficioConRespuesta
                                    Return My.Resources.RecursosES_CO.OficioConRespuesta
                                Case Else
                                    Return "Agrege Este Valor a la Pila de Recursos"
                            End Select



                        'VISOR DE EVENTOS
                        Case Eform.VisorEventos

                            'se realiza un select case para buscar entre los recursos disponibles del visor de eventos.
                            Select Case recurso
                                Case Eresources.VisorTitulo
                                    Return My.Resources.RecursosES_CO.VisorTitulo
                                Case Eresources.VisorErrores
                                    Return My.Resources.RecursosES_CO.VisorErrores
                                Case Eresources.VisorAdvertencias
                                    Return My.Resources.RecursosES_CO.VisorAdvertencias
                                Case Eresources.VisorMensajes
                                    Return My.Resources.RecursosES_CO.VisorMensajes
                                Case Else
                                    Return "Agrege Este Valor a la Pila de Recursos"
                            End Select

                        'form de liquidacion de cesantías
                        Case Eform.LiquidacionCesantias
                            Select Case recurso
                                Case Eresources.NoHayNominasLiquidadas
                                    Return My.Resources.RecursosES_CO.NoHayNominasLiquidadas
                                Case Eresources.NoHayGruposSeleccionados
                                    Return My.Resources.RecursosES_CO.NoHayGruposSeleccionados
                                Case Eresources.EmpleadoNoTieneNominasLiquidadas
                                    Return My.Resources.RecursosES_CO.EmpleadoNoTieneNominasLiquidadas
                                Case Eresources.EmpleadoNoTieneContratoLaboral
                                    Return My.Resources.RecursosES_CO.EmpleadoNoTieneContratoLaboral
                                Case Eresources.EmpleadoYaTieneCesantiasLiquidadas
                                    Return My.Resources.RecursosES_CO.EmpleadoYaTieneCesantiasLiquidadas
                                Case Eresources.EmpleadoNoTieneContrato
                                    Return My.Resources.RecursosES_CO.EmpleadoNoTieneContrato
                                Case Eresources.EmpleadoYaTieneCesantiaAnualLiquidada
                                    Return My.Resources.RecursosES_CO.EmpleadoYaTieneCesantiaAnualLiquidada
                                Case Eresources.NohayCesantiasLiquidadas
                                    Return My.Resources.RecursosES_CO.NohayCesantiasLiquidadas
                                Case Eresources.NoseLiquidaronCesantias
                                    Return My.Resources.RecursosES_CO.NoseLiquidaronCesantias
                                Case Eresources.EmpleadoNoTieneFondoDeCesantia
                                    Return My.Resources.RecursosES_CO.EmpleadoNoTieneFondoDeCesantia
                                Case Eresources.LiquidacionCesantiasYaConfirmada
                                    Return My.Resources.RecursosES_CO.LiquidacionCesantiasYaConfirmada
                                Case Eresources.ConfirmarCesantias
                                    Return My.Resources.RecursosES_CO.ConfirmarCesantias
                                Case Eresources.CesantiasConfirmadasCorrectamente
                                    Return My.Resources.RecursosES_CO.CesantiasConfirmadasCorrectamente
                                Case Eresources.CesantiasNoSePudieronConfirmar
                                    Return My.Resources.RecursosES_CO.CesantiasNoSePudieronConfirmar
                            End Select

                        'form de liquidacion de Primas
                        Case Eform.LiquidacionPrimas
                            Select Case recurso
                                Case Eresources.NoHayGruposSeleccionadosPrimas
                                    Return My.Resources.RecursosES_CO.NoHayGruposSeleccionadosPrimas
                                Case Eresources.NoGeneroPrimas
                                    Return My.Resources.RecursosES_CO.NoGeneroPrimas
                                Case Eresources.LiquidacionPrimasConfirmada
                                    Return My.Resources.RecursosES_CO.LiquidacionPrimasConfirmada
                                Case Eresources.LiquidacionPrimasNoConfirmada
                                    Return My.Resources.RecursosES_CO.LiquidacionPrimasNoConfirmada
                                Case Eresources.LiquidacionPrimasYaConfirmada
                                    Return My.Resources.RecursosES_CO.LiquidacionPrimasYaConfirmada
                                Case Eresources.NoHayDatosAcumuladosLiquidacion
                                    Return My.Resources.RecursosES_CO.NoHayDatosAcumuladosLiquidacion
                                Case Eresources.IBCPrimasVacio
                                    Return My.Resources.RecursosES_CO.IBCPrimasVacio
                                Case Eresources.NoHayLiquidacionesGeneradas
                                    Return My.Resources.RecursosES_CO.NoHayLiquidacionesGeneradas
                                Case Eresources.NumeroPrimasDiferentes
                                    Return My.Resources.RecursosES_CO.NumeroPrimasDiferentes
                                Case Eresources.NoHaySeleccionadoSemestrePrimas
                                    Return My.Resources.RecursosES_CO.NoHaySeleccionadoSemestrePrimas
                                Case Eresources.ConfirmarPrimas
                                    Return My.Resources.RecursosES_CO.ConfirmarPrimas
                                Case Eresources.NoHaSeleccionadoTipoPagoPrimas
                                    Return My.Resources.RecursosES_CO.NoHaSeleccionadoTipoPagoPrimas
                            End Select

                        'form cuadro de turnos
                        Case Eform.CuadroDeTurno
                            Select Case recurso
                                Case Eresources.EmpleadoLiquidado
                                    Return My.Resources.RecursosES_CO.EmpleadoLiquidado
                                Case Eresources.EmpleadosLiquidados
                                    Return My.Resources.RecursosES_CO.EmpleadosLiquidados
                                Case Eresources.SobreescribirHorario
                                    Return My.Resources.RecursosES_CO.SobreescribirHorario
                                Case Eresources.Manana
                                    Return My.Resources.RecursosES_CO.Manana
                                Case Eresources.MananaTarde
                                    Return My.Resources.RecursosES_CO.MananaTarde
                                Case Eresources.Tarde
                                    Return My.Resources.RecursosES_CO.Tarde
                                Case Eresources.MananaNoche
                                    Return My.Resources.RecursosES_CO.MananaNoche
                                Case Eresources.TardeNoche
                                    Return My.Resources.RecursosES_CO.TardeNoche
                                Case Eresources.Noche
                                    Return My.Resources.RecursosES_CO.Noche
                                Case Eresources.Incapacidad
                                    Return My.Resources.RecursosES_CO.Incapacidad
                                Case Eresources.Licencia
                                    Return My.Resources.RecursosES_CO.Licencia
                                Case Eresources.Sancion
                                    Return My.Resources.RecursosES_CO.Sancion
                                Case Eresources.ErrorHour
                                    Return My.Resources.RecursosES_CO.ErrorHour
                                Case Eresources.ErrorScheduleDetailExisting
                                    Return My.Resources.RecursosES_CO.ErrorScheduleDetailExisting
                                Case Eresources.ErrorMaximumTotalHourNumber
                                    Return My.Resources.RecursosES_CO.ErrorMaximumTotalHourNumber
                                Case Eresources.ErrorWithoutContract
                                    Return My.Resources.RecursosES_CO.ErrorWithoutContract
                                Case Eresources.ErrorHoliday
                                    Return My.Resources.RecursosES_CO.ErrorHoliday
                                Case Eresources.ErrorIncapacidad
                                    Return My.Resources.RecursosES_CO.ErrorIncapacidad
                                Case Eresources.ErrorLicencia
                                    Return My.Resources.RecursosES_CO.ErrorLicencia
                                Case Eresources.ErrorSancion
                                    Return My.Resources.RecursosES_CO.ErrorSancion
                                Case Eresources.EventoAprobado
                                    Return My.Resources.RecursosES_CO.EventoAprobado
                                Case Eresources.FechaInicioMenor
                                    Return My.Resources.RecursosES_CO.FechaInicioMenor
                                Case Eresources.RangoFrecuenciaAlto
                                    Return My.Resources.RecursosES_CO.RangoFrecuenciaAlto
                                Case Eresources.ErrorPermisoVacaciones
                                    Return My.Resources.RecursosES_CO.ErrorPermisoVacaciones
                                Case Eresources.ErrorVacaciones
                                    Return My.Resources.RecursosES_CO.ErrorVacaciones
                                Case Eresources.Vacaciones
                                    Return My.Resources.RecursosES_CO.Vacaciones
                                Case Eresources.PermisoVacaciones
                                    Return My.Resources.RecursosES_CO.PermisoVacaciones
                                Case Eresources.NoHayDiasSeleccionados
                                    Return My.Resources.RecursosES_CO.NoHayDiasSeleccionados
                                Case Eresources.ErrorMaximoHoras18
                                    Return My.Resources.RecursosES_CO.ErrorMaximoHoras18

                            End Select
                        Case Eform.Vacaciones
                            Select Case recurso
                                Case Eresources.NoPuedeSolicitarDias
                                    Return My.Resources.RecursosES_CO.NoPuedeSolicitarDias
                                Case Eresources.EmpleadoNoPuedeDias
                                    Return My.Resources.RecursosES_CO.EmpleadoNoPuedeDias
                                Case Eresources.EmpleadoNotieneDiasPendientes
                                    Return My.Resources.RecursosES_CO.EmpleadoNotieneDiasPendientes
                                Case Eresources.NoHayEmpleadosSeleccionados
                                    Return My.Resources.RecursosES_CO.NoHayEmpleadosSeleccionados
                                Case Eresources.EmpleadoEnVacaciones
                                    Return My.Resources.RecursosES_CO.EmpleadoEnVacaciones
                                Case Eresources.EsperandoPago
                                    Return My.Resources.RecursosES_CO.EsperandoPago
                                Case Eresources.Pagas
                                    Return My.Resources.RecursosES_CO.Pagas
                                Case Eresources.EstaSeguroCancelarVacacion
                                    Return My.Resources.RecursosES_CO.EstaSeguroCancelarVacacion
                                Case Eresources.EstaSeguroReingresoVacacion
                                    Return My.Resources.RecursosES_CO.EstaSeguroReingresoVacacion
                                Case Eresources.CancelarSolicitud
                                    Return My.Resources.RecursosES_CO.CancelarSolicitud
                                Case Eresources.IngresoForzoso
                                    Return My.Resources.RecursosES_CO.IngresoForzoso
                                Case Eresources.SolicitudCanceladaCorrectamente
                                    Return My.Resources.RecursosES_CO.SolicitudCanceladaCorrectamente
                                Case Eresources.SolicitudYaEstanPagas
                                    Return My.Resources.RecursosES_CO.SolicitudYaEstanPagas
                                Case Eresources.PermisoCargoVacaciones
                                    Return My.Resources.RecursosES_CO.PermisoCargoVacaciones
                                Case Eresources.InterrupcionVacaciones
                                    Return My.Resources.RecursosES_CO.InterrupcionVacaciones
                            End Select
                        Case Eform.LiquidacionNomina
                            Select Case recurso
                                Case Eresources.NoGeneraLiquidacion
                                    Return My.Resources.RecursosES_CO.NoGeneraLiquidacion
                                Case Eresources.NoExisteEmpleado
                                    Return My.Resources.RecursosES_CO.NoExisteEmpleado
                                Case Eresources.NominaConfirmadaCorrectamente
                                    Return My.Resources.RecursosES_CO.NominaConfirmadaCorrectamente
                                Case Eresources.PrimeroNominaBorrador
                                    Return My.Resources.RecursosES_CO.PrimeroNominaBorrador
                                Case Eresources.NoHayEmpleadosLiquidar
                                    Return My.Resources.RecursosES_CO.NoHayEmpleadosLiquidar
                                Case Eresources.ErroresGravesLiquidacion
                                    Return My.Resources.RecursosES_CO.ErroresGravesLiquidacion
                                Case Eresources.NoConceptosAutorizados
                                    Return My.Resources.RecursosES_CO.NoConceptosAutorizados
                                Case Eresources.NominaConfirmarNomina
                                    Return My.Resources.RecursosES_CO.NominaConfirmarNomina
                                Case Eresources.NoHayGruposSeleccionadosNomina
                                    Return My.Resources.RecursosES_CO.NoHayGruposSeleccionadosNomina
                                Case Eresources.NoSeleccionoLiquidacionVisualizar
                                    Return My.Resources.RecursosES_CO.NoSeleccionoLiquidacionVisualizar
                                Case Eresources.LiquidacionNominaCorrecta
                                    Return My.Resources.RecursosES_CO.LiquidacionNominaCorrecta
                            End Select
                        Case Eform.PlantillaDeTurno
                            Select Case recurso
                                Case Eresources.MinimoUnOrdinario
                                    Return My.Resources.RecursosES_CO.MinimoUnOrdinario
                                Case Eresources.MinimoUnFeriado
                                    Return My.Resources.RecursosES_CO.MinimoUnFeriado
                                Case Eresources.RangoHorasCompletas
                                    Return My.Resources.RecursosES_CO.RangoHorasCompletas
                                Case Eresources.ConceptoFeriado
                                    Return My.Resources.RecursosES_CO.ConceptoFeriado
                                Case Eresources.ConceptoOrdinario
                                    Return My.Resources.RecursosES_CO.ConceptoOrdinario
                                Case Eresources.TiempoMayorCero
                                    Return My.Resources.RecursosES_CO.TiempoMayorCero
                                Case Eresources.AppointmentExiste
                                    Return My.Resources.RecursosES_CO.AppointmentExiste
                            End Select
                        Case Eform.Incapacidades
                            Select Case recurso
                                Case Eresources.NovedadReajusteVacacionesNoEditar
                                    Return My.Resources.RecursosES_CO.NovedadReajusteVacacionesNoEditar
                                Case Eresources.EmpleadoVacacionesNoPuedeNovedad
                                    Return My.Resources.RecursosES_CO.EmpleadoVacacionesNoPuedeNovedad
                                Case Eresources.EmpleadoVacacionesReajuste
                                    Return My.Resources.RecursosES_CO.EmpleadoVacacionesReajuste
                                Case Eresources.CantidadDiasNovedad
                                    Return My.Resources.RecursosES_CO.CantidadDiasNovedad
                                Case Eresources.EmpleadoNoExiste
                                    Return My.Resources.RecursosES_CO.EmpleadoNoExiste
                                Case Eresources.Postular23
                                    Return My.Resources.RecursosES_CO.Postular23
                                Case Eresources.Postular90Dias
                                    Return My.Resources.RecursosES_CO.Postular90Dias
                                Case Eresources.PostularNomina
                                    Return My.Resources.RecursosES_CO.PostularNomina
                                Case Eresources.PostularPatrono
                                    Return My.Resources.RecursosES_CO.PostularPatrono
                                Case Eresources.Postular23Todos
                                    Return My.Resources.RecursosES_CO.Postular23Todos
                                Case Eresources.LosTresPrimerosDias
                                    Return My.Resources.RecursosES_CO.LosTresPrimerosDias
                                Case Eresources.TodosLosDias
                                    Return My.Resources.RecursosES_CO.TodosLosDias
                                Case Eresources.NoLiquida
                                    Return My.Resources.RecursosES_CO.NoLiquida
                                Case Eresources.IncapacidadAmbulatoria
                                    Return My.Resources.RecursosES_CO.IncapacidadAmbulatoria
                                Case Eresources.IncapacidadGeneral
                                    Return My.Resources.RecursosES_CO.IncapacidadGeneral
                                Case Eresources.IncapacidadHospitalaria
                                    Return My.Resources.RecursosES_CO.IncapacidadHospitalaria
                                Case Eresources.IncapacidadMaternidad
                                    Return My.Resources.RecursosES_CO.IncapacidadMaternidad
                                Case Eresources.IncapacidadPaternidad
                                    Return My.Resources.RecursosES_CO.IncapacidadPaternidad
                                Case Eresources.LicenciaLuto
                                    Return My.Resources.RecursosES_CO.LicenciaLuto
                                Case Eresources.IncapacidadRiesgos
                                    Return My.Resources.RecursosES_CO.IncapacidadRiesgos
                                Case Eresources.GrupoSinParametros
                                    Return My.Resources.RecursosES_CO.GrupoSinParametros
                                Case Eresources.FaltaCampo
                                    Return My.Resources.RecursosES_CO.FaltaCampo
                                Case Eresources.CrearNovedad
                                    Return My.Resources.RecursosES_CO.CrearNovedad
                                Case Eresources.EditarNovedad
                                    Return My.Resources.RecursosES_CO.EditarNovedad
                                Case Eresources.ProrrogaNovedad
                                    Return My.Resources.RecursosES_CO.ProrrogaNovedad
                                Case Eresources.Inicial
                                    Return My.Resources.RecursosES_CO.Inicial
                                Case Eresources.Prorroga
                                    Return My.Resources.RecursosES_CO.Prorroga
                                Case Eresources.NoPuedeAccionPorUltimaProrroga
                                    Return My.Resources.RecursosES_CO.NoPuedeAccionPorUltimaProrroga
                                Case Eresources.NoPuedeFechaInferiorContrato
                                    Return My.Resources.RecursosES_CO.NoPuedeFechaInferiorContrato
                                Case Eresources.NoPuedeFechaSuperiorContrato
                                    Return My.Resources.RecursosES_CO.NoPuedeFechaSuperiorContrato
                                Case Eresources.NoPuedeFechaFinSuperiorContrato
                                    Return My.Resources.RecursosES_CO.NoPuedeFechaFinSuperiorContrato
                                Case Eresources.Liquidar
                                    Return My.Resources.RecursosES_CO.Liquidar
                                Case Eresources.NoLiquidar
                                    Return My.Resources.RecursosES_CO.NoLiquidar
                            End Select
                        Case Eform.RegisterObjection
                            Select Case recurso
                                Case Eresources.ConceptoEspecificoSinDetallados
                                    Return My.Resources.RecursosES_CO.ConceptoEspecificoSinDetallados
                                Case Eresources.TextBotonAgregarObjecion
                                    Return My.Resources.RecursosES_CO.TextBotonAgregarObjecion
                                Case Eresources.TextBotonModificarObjecion
                                    Return My.Resources.RecursosES_CO.TextBotonModificarObjecion
                                Case Eresources.ConsecutivoObjecionNoExiste
                                    Return My.Resources.RecursosES_CO.ConsecutivoObjecionNoExiste
                                Case Eresources.CodigoGlosaYaExiste
                                    Return My.Resources.RecursosES_CO.CodigoGlosaYaExiste
                                Case Eresources.ValorGlosaMayorValorFactura
                                    Return My.Resources.RecursosES_CO.ValorGlosaMayorValorFactura
                                Case Eresources.ValorReiteracionMayorValorGlosado
                                    Return My.Resources.RecursosES_CO.ValorReiteracionMayorValorGlosado
                                Case Eresources.ReiteratedSlopeValueGreaterBalance
                                    Return My.Resources.RecursosES_CO.ReiteratedSlopeValueGreaterBalance
                                Case Eresources.GlossValueWasAlreadyAccepted
                                    Return My.Resources.RecursosES_CO.GlossValueWasAlreadyAccepted
                                Case Eresources.ItemWithoutBalance
                                    Return My.Resources.RecursosES_CO.ItemWithoutBalance
                            End Select
                        Case Eform.Coordinacion
                            Select Case recurso
                                Case Eresources.MenuDetalleDeOficio
                                    Return My.Resources.RecursosES_CO.MenuDetalleDeOficio
                                Case Eresources.MenuDetalleDeFactura
                                    Return My.Resources.RecursosES_CO.MenuDetalleDeFactura
                                Case Eresources.TituloDetalleDeFacturaCoordinacion
                                    Return My.Resources.RecursosES_CO.TituloDetalleDeFacturaCoordinacion
                                Case Eresources.TituloDetalleDeOficioCoordinacion
                                    Return My.Resources.RecursosES_CO.TituloDetalleDeOficioCoordinacion
                                Case Eresources.ValorAceptadoMayorValorGlosado
                                    Return My.Resources.RecursosES_CO.ValorAceptadoMayorValorGlosado

                            End Select
                        Case Eform.Evaluacion
                            Select Case recurso
                                Case Eresources.ResponsableFacturaBloqueda
                                    Return My.Resources.RecursosES_CO.ResponsableFacturaBloqueada
                            End Select
                        Case Eform.Conciliation
                            Select Case recurso
                                Case Eresources.ClienteNoExite
                                    Return My.Resources.RecursosES_CO.ClienteNoExite
                                Case Eresources.ConciliacionNoExiste
                                    Return My.Resources.RecursosES_CO.ConciliacionNoExiste
                                Case Eresources.TextoBotonAgregarParticipante
                                    Return My.Resources.RecursosES_CO.TextoBotonAgregarParticipante
                                Case Eresources.TextoBotonModificarParticipante
                                    Return My.Resources.RecursosES_CO.TextoBotonModificarParticipante
                                Case Eresources.FacturaYaExiste
                                    Return My.Resources.RecursosES_CO.FacturaYaExiste
                                Case Eresources.FacturaNoExisteAlAgregar
                                    Return My.Resources.RecursosES_CO.FacturaNoExisteAlAgregar
                                Case Eresources.DetalleConciliacionVacia
                                    Return My.Resources.RecursosES_CO.DetalleConciliacionVacia
                                Case Eresources.EliminarFacturaRejilla
                                    Return My.Resources.RecursosES_CO.EliminarFacturaRejilla
                                Case Eresources.TituloDetalleFactura
                                    Return My.Resources.RecursosES_CO.TituloDetalleFactura
                                Case Eresources.TituloConciliacionDetalleFactura
                                    Return My.Resources.RecursosES_CO.TituloConciliacionDetalleFactura
                                Case Eresources.FacturaSinGuardar
                                    Return My.Resources.RecursosES_CO.FacturaSinGuardar
                                Case Eresources.ConciliacionSinDetalles
                                    Return My.Resources.RecursosES_CO.ConciliacionSinDetalles
                                Case Eresources.NuevaConciliacionLabel
                                    Return My.Resources.RecursosES_CO.NuevaConciliacionLabel
                                Case Eresources.FaltanDatosParaConciliar
                                    Return My.Resources.RecursosES_CO.FaltanDatosParaConciliar
                                Case Eresources.FaltanDetallesPorConciliar
                                    Return My.Resources.RecursosES_CO.FaltanDetallesPorConciliar
                                Case Eresources.FaltanFacturasPorConciliar
                                    Return My.Resources.RecursosES_CO.FaltanFacturasPorConciliar
                                Case Eresources.ConciliacionConfirmarFactura
                                    Return My.Resources.RecursosES_CO.ConciliacionConfirmarFactura
                                Case Eresources.FaltanFacturasPorPersistir
                                    Return My.Resources.RecursosES_CO.FaltanFacturasPorPersistir
                                Case Eresources.DebeSeleccionarTercero
                                    Return My.Resources.RecursosES_CO.DebeSeleccionarTercero
                                Case Eresources.MenuConciliarDetalle
                                    Return My.Resources.RecursosES_CO.MenuConciliarDetalle
                                Case Eresources.MenuConciliarFactura
                                    Return My.Resources.RecursosES_CO.MenuConciliarFactura
                                Case Eresources.MenuEditarParticipante
                                    Return My.Resources.RecursosES_CO.MenuEditarParticipante
                                Case Eresources.MenuEliminarFacturaConciliacion
                                    Return My.Resources.RecursosES_CO.MenuEliminarFacturaConciliacion
                                Case Eresources.MenuEliminarParticipante
                                    Return My.Resources.RecursosES_CO.MenuEliminarParticipante
                                Case Eresources.MenuPegarFacturaConciliacion
                                    Return My.Resources.RecursosES_CO.MenuPegarFacturaConciliacion
                                Case Eresources.ValorConciliarMayorValorSaldoAConciliar
                                    Return My.Resources.RecursosES_CO.ValorConciliarMayorValorSaldoAConciliar
                            End Select
                        Case Eform.Devolution
                            Select Case recurso
                                Case Eresources.DevolucionNoExiste
                                    Return My.Resources.RecursosES_CO.DevolucionNoExiste
                                Case Eresources.RadicadoSinGuardar
                                    Return My.Resources.RecursosES_CO.RadicadoSinGuardar
                                Case Eresources.GuardarDevolucion
                                    Return My.Resources.RecursosES_CO.GuardarDevolucion
                            End Select
                        Case Eform.ConceptosGenerales
                            Select Case recurso
                                Case Eresources.SeleccioneUnConceptoGeneral
                                    Return My.Resources.RecursosES_CO.SeleccioneUnConceptoGeneral
                                Case Else
                                    Return "Agrege Este Valor a la Pila de Recursos"
                            End Select
                        Case Eform.Company
                            Select Case recurso
                                Case Eresources.SeleccioneUnaEmpresa
                                    Return My.Resources.RecursosES_CO.SeleccioneUnaEmpresa
                                Case Eresources.CentroAtencionExisteEnEmpresa
                                    Return My.Resources.RecursosES_CO.CentroAtencionExisteEnEmpresa
                                Case Eresources.MenuEliminarCentroCompanies
                                    Return My.Resources.RecursosES_CO.MenuEliminarCentroCompanies
                                Case Else
                                    Return "Agrege Este Valor a la Pila de Recursos"
                            End Select
                        Case Eform.AutorizationHealthCenter
                            Select Case recurso
                                Case Eresources.SeleccioneUnUsuario
                                    Return My.Resources.RecursosES_CO.SeleccioneUnUsuario
                                Case Eresources.EmpresaYaExiste
                                    Return My.Resources.RecursosES_CO.EmpresaYaExiste
                                Case Eresources.UsuarioNoExiste
                                    Return My.Resources.RecursosES_CO.UsuarioNoExiste
                                Case Else
                                    Return "Agrege Este Valor a la Pila de Recursos"
                            End Select
                        Case Eform.RecepcionObjeciones
                            Select Case recurso
                                Case Eresources.FacturaExisteLista
                                    Return My.Resources.RecursosES_CO.FacturaExisteLista
                                Case Eresources.ComunesNoSeEncontroDatoERP
                                    Return My.Resources.RecursosES_CO.ComunesNoSeEncontroDatoERP
                                Case Eresources.ConfirmarFacturaSinValorGlosado
                                    Return My.Resources.RecursosES_CO.ConfirmarFacturaSinValorGlosado
                                Case Eresources.ValorGlosadoMayorSaldo
                                    Return My.Resources.RecursosES_CO.ValorGlosadoMayorSaldo
                                Case Eresources.FacturaSinConfirmarEnLista
                                    Return My.Resources.RecursosES_CO.FacturaSinConfirmarEnLista
                                Case Eresources.NoPuedeEliminarPorMovimiento
                                    Return My.Resources.RecursosES_CO.NoPuedeEliminarPorMovimiento
                                Case Eresources.seleccioneFechafininvalida
                                    Return My.Resources.RecursosES_CO.SeleccioneFechaValida
                                Case Eresources.GloseDetalleQX
                                    Return My.Resources.RecursosES_CO.GloseDetalleQX
                                Case Eresources.NoPuedeAnularHayMovimiento
                                    Return My.Resources.RecursosES_CO.NoPuedeAnularHayMovimiento
                                Case Eresources.NoPuedeGlosarEstadoERP
                                    Return My.Resources.RecursosES_CO.NoPuedeGlosarEstadoERP
                                Case Eresources.NoPuedeGuardarPorEstadoERP
                                    Return My.Resources.RecursosES_CO.NoPuedeGuardarPorEstadoERP
                                Case Eresources.NoSePuedeAgregarFacturaEstado
                                    Return My.Resources.RecursosES_CO.NoSePuedeAgregarFacturaEstado
                                Case Eresources.NoPuedeRealizarAccionAnulado
                                    Return My.Resources.RecursosES_CO.NoPuedeRealizarAccionAnulado
                                Case Eresources.NosePuedeAgregaFacturaRadicada
                                    Return My.Resources.RecursosES_CO.NosePuedeAgregaFacturaRadicada
                                Case Eresources.NosePuedeAgregaFacturaReiterada
                                    Return My.Resources.RecursosES_CO.NosePuedeAgregaFacturaReiterada
                                Case Eresources.ConfirmarReitereacionMasiva
                                    Return My.Resources.RecursosES_CO.ConfirmarReitereacionMasiva
                                Case Eresources.SeleccioneUnaEmpresa
                                    Return My.Resources.RecursosES_CO.SeleccioneUnaEmpresa
                                Case Eresources.NoExisteInformacionAPegar
                                    Return My.Resources.RecursosES_CO.NoExisteInformacionAPegar
                                Case Eresources.NoPuedeEliminarDatoReiteracion
                                    Return My.Resources.RecursosES_CO.NoPuedeEliminarDatoReiteracion
                                Case Eresources.LabelNuevo
                                    Return My.Resources.RecursosES_CO.LabelNuevo
                                Case Eresources.FacturaSinRadicar
                                    Return My.Resources.RecursosES_CO.FacturaSinRadicar
                                Case Eresources.FacturaRadicadaSinConfirmar
                                    Return My.Resources.RecursosES_CO.FacturaRadicadaSinConfirmar
                                Case Eresources.FacturaAnulada
                                    Return My.Resources.RecursosES_CO.FacturaAnulada
                                Case Eresources.EstadoInvalido
                                    Return My.Resources.RecursosES_CO.EstadoInvalido
                                Case Eresources.EncabezadoReiteracion
                                    Return My.Resources.RecursosES_CO.EncabezadoReiteracion
                                Case Eresources.TiponoReconocido
                                    Return My.Resources.RecursosES_CO.TiponoReconocido
                                Case Eresources.MenuVerDetalle
                                    Return My.Resources.RecursosES_CO.MenuVerDetalle
                                Case Eresources.MenuEliminar
                                    Return My.Resources.RecursosES_CO.MenuEliminar
                                Case Eresources.MenuGlosarSeleccion
                                    Return My.Resources.RecursosES_CO.MenuGlosarSeleccion
                                Case Eresources.MenuReiterar
                                    Return My.Resources.RecursosES_CO.MenuReiterar
                                Case Eresources.NosePuedeAgregaFacturaEnProceso
                                    Return My.Resources.RecursosES_CO.NosePuedeAgregaFacturaEnProceso
                                Case Eresources.NoPuedeGuardarPorDetalleOficio
                                    Return My.Resources.RecursosES_CO.NoPuedeGuardarPorDetalleOficio
                                Case Eresources.NoPuedeReiterarNoExisteGlosa
                                    Return My.Resources.RecursosES_CO.NoPuedeReiterarNoExisteGlosa
                                Case Eresources.FaltaNitTercero
                                    Return My.Resources.RecursosES_CO.FaltaNitTercero
                                Case Eresources.NoHayRegistroCriteriosBusqueda
                                    Return My.Resources.RecursosES_CO.NoHayRegistroCriteriosBusqueda
                                Case Eresources.NoseEncuentraFactura
                                    Return My.Resources.RecursosES_CO.NoseEncuentraFactura
                            End Select
                        Case Eform.ParametersInterfaz
                            Select Case recurso
                                Case Eresources.NoExisteContenedor
                                    Return My.Resources.RecursosES_CO.NoExisteContenedor
                                Case Eresources.NoExisteDatosContables
                                    Return My.Resources.RecursosES_CO.NoExisteDatosContables
                                Case Eresources.NoExisteCuentaConfigurada
                                    Return My.Resources.RecursosES_CO.NoExisteCuentaConfigurada
                                Case Eresources.NoExisteCuentaFacturaRadicadaConfigurada
                                    Return My.Resources.RecursosES_CO.NoExisteCuentaFacturaRadicadaConfigurada
                            End Select
                        'CAMBIAR CONTRASEÑA
                        Case Eform.CambiarContrasena
                            Select Case recurso
                                Case Eresources.ContrasenaMinimoCaracteres
                                    Return My.Resources.RecursosES_CO.ContrasenaMinimoCaracteres
                                Case Eresources.ContrasenaConfirmacionVacia
                                    Return My.Resources.RecursosES_CO.ContrasenaConfirmacionVacia
                                Case Eresources.ContrasenaAnteriorVacia
                                    Return My.Resources.RecursosES_CO.ContrasenaAnteriorVacia
                                Case Eresources.ContrasenaNuevaVacia
                                    Return My.Resources.RecursosES_CO.ContrasenaNuevaVacia
                                Case Eresources.ContrasenasNoCoinciden
                                    Return My.Resources.RecursosES_CO.ContrasenasNoCoinciden
                                Case Eresources.ContrasenaNoCorrecta
                                    Return My.Resources.RecursosES_CO.ContrasenaNoCorrecta
                                Case Eresources.ContrasenaGuardadaCorrectamente
                                    Return My.Resources.RecursosES_CO.ContrasenaGuardadaCorrectamente
                                Case Eresources.ContrasenaDigiteContrasena
                                    Return My.Resources.RecursosES_CO.ContrasenaDigiteContrasena
                                Case Else
                                    Return "Agrege Este Valor a la Pila de Recursos"
                            End Select


                        'FORMULARIO ROLES
                        Case Eform.Roles
                            Select Case recurso
                                Case Eresources.RolesMensajeComplemento
                                    Return My.Resources.RecursosES_CO.RolesMensajeComplemento

                                Case Else
                                    Return "Agrege Este Valor a la Pila de Recursos"
                            End Select
                        'FORMULARIO UNIDADES FUNCIONALES
                        Case Eform.UnidadesFuncionales
                            Select Case recurso
                                Case Eresources.UnidadFunMensajeComplemento
                                    Return My.Resources.RecursosES_CO.UnidadFunMensajeComplemento
                                Case Eresources.UsuarioYaAgregado
                                    Return My.Resources.RecursosES_CO.UsuarioYaAgregado
                                Case Eresources.SeleccioneUsuario
                                    Return My.Resources.RecursosES_CO.SeleccioneUsuario
                                Case Else
                                    Return "Agrege Este Valor a la Pila de Recursos"
                            End Select
                        'FORMULARIO CENTROS DE ATENCION
                        Case Eform.CentroAtenciones
                            Select Case recurso
                                Case Eresources.CentroAtenMensajeComplemento
                                    Return My.Resources.RecursosES_CO.CentroAtenMensajeComplemento
                                Case Eresources.CentroCodigoHabilitacion
                                    Return My.Resources.RecursosES_CO.CentroCodigoHabilitacion
                                Case Eresources.CentroUnidadAgregada
                                    Return My.Resources.RecursosES_CO.CentroUnidadAgregada
                                Case Eresources.CentroEliminado
                                    Return My.Resources.RecursosES_CO.CentroEliminado
                                Case Else
                                    Return "Agrege Este Valor a la Pila de Recursos"
                            End Select

                        'FORMULARIO GRUPOS
                        Case Eform.Grupos
                            Select Case recurso
                                Case Eresources.GruposMensajeComplemento
                                    Return My.Resources.RecursosES_CO.GruposMensajeComplemento
                                Case Eresources.GruposNoEliminado
                                    Return My.Resources.RecursosES_CO.GruposNoEliminado

                                Case Else
                                    Return "Agrege Este Valor a la Pila de Recursos"
                            End Select
                        'FORMULARIO USUARIO
                        Case Eform.Usuario
                            Select Case recurso
                                Case Eresources.UsuarioUnidadOperativaDefault
                                    Return My.Resources.RecursosES_CO.UsuarioUnidadOperativaDefault
                                Case Eresources.UsuarioPermisoUnaEmpresa
                                    Return My.Resources.RecursosES_CO.UsuarioPermisoUnaEmpresa
                                Case Eresources.UsuarioMensajeComplemento
                                    Return My.Resources.RecursosES_CO.UsuarioMensajeComplemento
                                Case Eresources.UsuarioNoExiste
                                    Return My.Resources.RecursosES_CO.UsuarioNoExiste
                                Case Else
                                    Return "Agrege Este Valor a la Pila de Recursos"
                            End Select
                        'FORMULARIO CONEXION
                        Case Eform.Conexion
                            Select Case recurso
                                Case Eresources.ConexionReiniciarIndigo
                                    Return My.Resources.RecursosES_CO.ConexionReiniciarIndigo
                                Case Eresources.ConexionUrlServidor
                                    Return My.Resources.RecursosES_CO.ConexionUrlServidor
                                Case Eresources.ConexionUrlServidorEntidades
                                    Return My.Resources.RecursosES_CO.ConexionUrlServidorEntidades
                                Case Eresources.ConexionRutaNoExiste
                                    Return My.Resources.RecursosES_CO.ConexionRutaNoExiste
                                Case Eresources.ConexionSeleccioneRuta
                                    Return My.Resources.RecursosES_CO.ConexionSeleccioneRuta
                                Case Eresources.ComunesDigiteDatos
                                    Return My.Resources.RecursosES_CO.ComunesDigiteDatos
                                Case Else
                                    Return "Agrege Este Valor a la Pila de Recursos"
                            End Select
                        'FORMULARIO DESBLOQUEAR USUARIO
                        Case Eform.DesbloquearUsuario
                            Select Case recurso
                                Case Eresources.DesbloquearUsuarioDesbloqueado
                                    Return My.Resources.RecursosES_CO.DesbloquearUsuarioDesbloqueado
                                Case Eresources.DesbloquearUsuarioNOdesbloqueado
                                    Return My.Resources.RecursosES_CO.DesbloquearUsuarioNOdesbloqueado
                                Case Else
                                    Return "Agrege Este Valor a la Pila de Recursos"
                            End Select

                        Case Eform.Login
                            Select Case recurso
                                Case Eresources.LoginCentroAtencion
                                    Return My.Resources.RecursosES_CO.LoginCentroAtencion
                                Case Eresources.LoginUnidadFuncional
                                    Return My.Resources.RecursosES_CO.LoginUnidadFuncional
                                Case Eresources.LoginSeleccioneEmpresa
                                    Return My.Resources.RecursosES_CO.LoginSeleccioneEmpresa
                                Case Eresources.LoginPerfilAsistencial
                                    Return My.Resources.RecursosES_CO.LoginPerfilAsistencial
                                Case Eresources.LoginIntentoContrasena
                                    Return My.Resources.RecursosES_CO.LoginIntentoContrasena
                                Case Eresources.LoginIntentoContrasenaComplemento
                                    Return My.Resources.RecursosES_CO.LoginIntentoContrasenaComplemento
                                Case Eresources.LoginCambiarContrasena
                                    Return My.Resources.RecursosES_CO.LoginCambiarContrasena
                                Case Eresources.LoginGuardarComo
                                    Return My.Resources.RecursosES_CO.LoginGuardarComo
                                Case Eresources.LoginCuentaCaduco
                                    Return My.Resources.RecursosES_CO.LoginCuentaCaduco
                                Case Eresources.LoginUsuarioInactivo
                                    Return My.Resources.RecursosES_CO.LoginUsuarioInactivo
                                Case Eresources.LoginUsuarioBloqueado
                                    Return My.Resources.RecursosES_CO.LoginUsuarioBloqueado
                                Case Else
                                    Return "Agrege Este Valor a la Pila de Recursos"
                            End Select
                        'CUSTOMIZAR SE UTILIZA CUANDO SE VA A PERSONALIZAR UN FRONTAL ESPECIFICO
                        Case Eform.Customizar
                            Select Case recurso
                                Case Eresources.CustomizarEspacioVacio
                                    Return My.Resources.RecursosES_CO.CustomizarEspacioVacio
                                Case Eresources.CustomizarEtiqueta
                                    Return My.Resources.RecursosES_CO.CustomizarEtiqueta
                                Case Eresources.CustomizarSeparador
                                    Return My.Resources.RecursosES_CO.CustomizarSeparador
                                Case Eresources.CustomizarDivisor
                                    Return My.Resources.RecursosES_CO.CustomizarDivisor
                                Case Eresources.CustomizarMostrarItem
                                    Return My.Resources.RecursosES_CO.CustomizarMostrarItem
                                Case Eresources.CustomizarMostrarTexto
                                    Return My.Resources.RecursosES_CO.CustomizarMostrarTexto
                                Case Eresources.CustomizarOcultarItem
                                    Return My.Resources.RecursosES_CO.CustomizarOcultarItem
                                Case Eresources.CustomizarOcultarTexto
                                    Return My.Resources.RecursosES_CO.CustomizarOcultarTexto
                                Case Else
                                    Return "Agrege Este Valor a la Pila de Recursos"
                            End Select
                        'REPORTES SE UTILIZA PARA QUE SE DEFINAN TODOS LOS MENSAJES RELACIONADOS A REPORTES
                        Case Eform.Reportes
                            Select Case recurso
                                Case Eresources.DefinitionReport
                                    Return My.Resources.RecursosES_CO.DefinitionReport
                                Case Eresources.DefinitionReport
                                    Return My.Resources.RecursosES_CO.DefaultDefinition
                            End Select
                        'COMUNES SE UTILIZA PARA QUE POR ACA ENTREN TODOS LOS MENSAJES COMUNES
                        Case Eform.Comunes
                            Select Case recurso
                                Case Eresources.ComunesDebeQuitarPermisoRoll
                                    Return My.Resources.RecursosES_CO.ComunesDebeQuitarPermisoRoll
                                Case Eresources.SeConfirmaronTodos
                                    Return My.Resources.RecursosES_CO.SeConfirmaronTodos
                                Case Eresources.SeConfirmoRegistro
                                    Return My.Resources.RecursosES_CO.SeConfirmoRegistro
                                Case Eresources.OficioSinDetalles
                                    Return My.Resources.RecursosES_CO.OficioSinDetalles
                                Case Eresources.XpoGridErrorMessage
                                    Return My.Resources.RecursosES_CO.XpoGridErrorMessage
                                Case Eresources.XpoGridErrorTitle
                                    Return My.Resources.RecursosES_CO.XpoGridErrorTitle
                                Case Eresources.AgregarComunes
                                    Return My.Resources.RecursosES_CO.AgregarComunes
                                Case Eresources.ModificarComunes
                                    Return My.Resources.RecursosES_CO.ModificarComunes
                                Case Eresources.TituloParametrosComunes
                                    Return My.Resources.RecursosES_CO.TituloParametrosComunes
                                Case Eresources.PlantillaExistente
                                    Return My.Resources.RecursosES_CO.PlantillaExistente
                                Case Eresources.PlantillaPorDefecto
                                    Return My.Resources.RecursosES_CO.PlantillaPorDefecto
                                Case Eresources.ComunesTodos
                                    Return My.Resources.RecursosES_CO.ComunesTodos
                                Case Eresources.ComunesGenerarReporte
                                    Return My.Resources.RecursosES_CO.ComunesGenerarReporte
                                Case Eresources.RegistroEnEdicion
                                    Return My.Resources.RecursosES_CO.RegistroEnEdicion
                                Case Eresources.ComunesAbrirEntidad
                                    Return My.Resources.RecursosES_CO.ComunesAbrirEntidad
                                Case Eresources.ComunesDias
                                    Return My.Resources.RecursosES_CO.ComunesDias
                                Case Eresources.ComunesActivo
                                    Return My.Resources.RecursosES_CO.ComunesActivo
                                Case Eresources.ComunesInactivo
                                    Return My.Resources.RecursosES_CO.ComunesInactivo
                                Case Eresources.ComunesNoHayRegistros
                                    Return My.Resources.RecursosES_CO.ComunesNoHayRegistros
                                Case Eresources.ComunesNoHayRegistrosParaEliminar
                                    Return My.Resources.RecursosES_CO.ComunesNoHayRegistrosParaEliminar
                                Case Eresources.ComunesBloqueoSession
                                    Return My.Resources.RecursosES_CO.ComunesBloqueoSession
                                Case Eresources.ComunesUsuarioNoAdministrador
                                    Return My.Resources.RecursosES_CO.ComunesUsuarioNoAdministrador
                                Case Eresources.ComunesDebeIngresarLaRutaDeServiciosYProtocolo
                                    Return My.Resources.RecursosES_CO.ComunesDebeIngresarLaRutaDeServiciosYProtocolo
                                Case Eresources.ComunesSePerderanCambios
                                    Return My.Resources.RecursosES_CO.ComunesSePerderanCambios
                                Case Eresources.ComunesUserNameInvalid
                                    Return My.Resources.RecursosES_CO.ComunesUserNameInvalid
                                Case Eresources.ComunesSeguroCerrarSesion
                                    Return My.Resources.RecursosES_CO.ComunesSeguroCerrarSesion
                                Case Eresources.RegistroActivo
                                    Return My.Resources.RecursosES_CO.RegistroActivo
                                Case Eresources.RegistroInactivo
                                    Return My.Resources.RecursosES_CO.RegistroInactivo
                                Case Eresources.RegistroBloqueado
                                    Return My.Resources.RecursosES_CO.RegistroBloqueado
                                Case Eresources.Informacion
                                    Return My.Resources.RecursosES_CO.Informacion
                                Case Eresources.Advertencia
                                    Return My.Resources.RecursosES_CO.Advertencia
                                Case Eresources.Errores
                                    Return My.Resources.RecursosES_CO.Errores
                                Case Eresources.Pregunta
                                    Return My.Resources.RecursosES_CO.Pregunta
                                Case Eresources.ComunesUsuarioSinPermisosEmpresa
                                    Return My.Resources.RecursosES_CO.ComunesUsuarioSinPermisosEmpresa
                                Case Eresources.ComunesClienteNoExiste
                                    Return My.Resources.RecursosES_CO.ComunesClienteNoExiste
                                Case Eresources.CerrarFormulariosCambiarEmpresa
                                    Return My.Resources.RecursosES_CO.CerrarFormulariosCambiarEmpresa
                                Case Eresources.ComunesRemplazaInformacionArchivoConfiguracion
                                    Return My.Resources.RecursosES_CO.ComunesRemplazaInformacionArchivoConfiguracion
                                Case Eresources.ComunesFechaMenorActual
                                    Return My.Resources.RecursosES_CO.ComunesFechaMenorActual
                                Case Eresources.ComunesSesionLocal
                                    Return My.Resources.RecursosES_CO.ComunesSesionLocal
                                Case Eresources.ComunesSesionRemota
                                    Return My.Resources.RecursosES_CO.ComunesSesionRemota
                                Case Eresources.ComunesEliminarConfirmado
                                    Return My.Resources.RecursosES_CO.ComunesEliminarConfirmado
                                Case Eresources.ComunesConfirmadoCorrectamente
                                    Return My.Resources.RecursosES_CO.ComunesConfirmadoCorrectamente
                                Case Eresources.ComunesAnuladoCorrectamente
                                    Return My.Resources.RecursosES_CO.ComunesAnuladoCorrectamente
                                Case Eresources.FacturasSinConfirmarComunes
                                    Return My.Resources.RecursosES_CO.FacturasSinConfirmarComunes
                                Case Eresources.ComunesNoSeEncontroDatoERP
                                    Return My.Resources.RecursosES_CO.ComunesNoSeEncontroDatoERP
                                Case Eresources.ComunesPreguntaAnular
                                    Return My.Resources.RecursosES_CO.ComunesPreguntaAnular
                                Case Eresources.ComunesPreguntaConfirmar
                                    Return My.Resources.RecursosES_CO.ComunesPreguntaConfirmar
                                Case Eresources.ComunesPreguntaCerrarReg
                                    Return My.Resources.RecursosES_CO.ComunesPreguntaCerrarReg
                                Case Eresources.ComunesLayoutRestablecido
                                    Return My.Resources.RecursosES_CO.ComunesLayoutRestablecido
                                Case Eresources.ComunesActualizado
                                    Return My.Resources.RecursosES_CO.ComunesActualizado
                                Case Eresources.ComunesCodigoVacio
                                    Return My.Resources.RecursosES_CO.ComunesCodigoVacio
                                Case Eresources.ComunesDigiteDatos
                                    Return My.Resources.RecursosES_CO.ComunesDigiteDatos
                                Case Eresources.ComunesEliminado
                                    Return My.Resources.RecursosES_CO.ComunesEliminado
                                Case Eresources.ComunesGuardado
                                    Return My.Resources.RecursosES_CO.ComunesGuardado
                                Case Eresources.ComunesNombreVacio
                                    Return My.Resources.RecursosES_CO.ComunesNombreVacio
                                Case Eresources.ComunesDireccion
                                    Return My.Resources.RecursosES_CO.ComunesDireccion
                                Case Eresources.ComunesElija
                                    Return My.Resources.RecursosES_CO.ComunesElija
                                Case Eresources.ComunesCodigoCaracteres
                                    Return My.Resources.RecursosES_CO.ComunesCodigoCaracteres
                                Case Eresources.ComunesContacteAdministrador
                                    Return My.Resources.RecursosES_CO.ComunesContacteAdministrador
                                Case Eresources.PermisosEliminar
                                    Return My.Resources.RecursosES_CO.PermisosEliminar
                                Case Eresources.PermisosGuardar
                                    Return My.Resources.RecursosES_CO.PermisosGuardar
                                Case Eresources.PermisosActualizar
                                    Return My.Resources.RecursosES_CO.PermisosActualizar
                                Case Eresources.PermisosEliminarRejilla
                                    Return My.Resources.RecursosES_CO.PermisosEliminarRejilla
                                Case Eresources.PermisosCustomizar
                                    Return My.Resources.RecursosES_CO.PermisosCustomizar
                                Case Eresources.PermisosDocumentos
                                    Return My.Resources.RecursosES_CO.PermisosDocumentos
                                Case Eresources.PermisosRedesSociales
                                    Return My.Resources.RecursosES_CO.PermisosRedesSociales
                                Case Eresources.PermisosComunicacion
                                    Return My.Resources.RecursosES_CO.PermisosComunicacion
                                Case Eresources.PermisosOtrosPlugins
                                    Return My.Resources.RecursosES_CO.PermisosOtrosPlugins
                                Case Eresources.PermisosVisible
                                    Return My.Resources.RecursosES_CO.PermisosVisible
                                Case Eresources.ComunesCorreoPersonal
                                    Return My.Resources.RecursosES_CO.ComunesCorreoPersonal
                                Case Eresources.ComunesCorreoEmpresarial
                                    Return My.Resources.RecursosES_CO.ComunesCorreoEmpresarial
                                Case Eresources.ComunesXmlCambio
                                    Return My.Resources.RecursosES_CO.ComunesXmlCambio
                                Case Eresources.ComunesEliminarRegistro
                                    Return My.Resources.RecursosES_CO.ComunesEliminarRegistro
                                Case Eresources.ComunesEliminarRegistros
                                    Return My.Resources.RecursosES_CO.ComunesEliminarRegistros
                                Case Eresources.ComunesEditarRegistro
                                    Return My.Resources.RecursosES_CO.ComunesEditarRegistro
                                Case Eresources.ComunesIndigoCrystal
                                    Return My.Resources.RecursosES_CO.ComunesIndigoCrystal
                                Case Eresources.ComunesArchivoCreado
                                    Return My.Resources.RecursosES_CO.ComunesArchivoCreado
                                Case Eresources.PermisosConsultar
                                    Return My.Resources.RecursosES_CO.PermisosConsultar
                                Case Eresources.ComunesNoTienePermisos
                                    Return My.Resources.RecursosES_CO.ComunesNoTienePermisos
                                Case Eresources.ComunesNoTienePermisosGuardar
                                    Return My.Resources.RecursosES_CO.ComunesNoTienePermisosGuardar
                                Case Eresources.ComunesNoTienePermisosActualizar
                                    Return My.Resources.RecursosES_CO.ComunesNoTienePermisosActualizar
                                Case Eresources.ComunesNoTienePermisosEliminar
                                    Return My.Resources.RecursosES_CO.ComunesNoTienePermisosEliminar
                                Case Eresources.ComunesError
                                    Return My.Resources.RecursosES_CO.ComunesError
                                Case Eresources.ComunesErrorGuardarDefinicion
                                    Return My.Resources.RecursosES_CO.ComunesErrorGuardarDefinicion
                                Case Eresources.ComunesSeleccione
                                    Return My.Resources.RecursosES_CO.ComunesSeleccione
                                Case Eresources.ComunesSeleccioneRegistroEliminar
                                    Return My.Resources.RecursosES_CO.ComunesSeleccioneRegistroEliminar
                                Case Eresources.ComunesNoAplicaBuscar
                                    Return My.Resources.RecursosES_CO.ComunesNoAplicaBuscar
                                Case Eresources.ComunesConfirmarFactura
                                    Return My.Resources.RecursosES_CO.ComunesConfirmarFactura
                                Case Eresources.ComunesImprimir
                                    Return My.Resources.RecursosES_CO.ComunesImprimir
                                Case Eresources.ComunesFormIncompleto
                                    Return My.Resources.RecursosES_CO.ComunesFormIncompleto
                                'Tipos de identificacion
                                Case Eresources.CedulaCiudadania
                                    Return My.Resources.RecursosES_CO.CedulaCiudadania
                                Case Eresources.CedulaExtranjeria
                                    Return My.Resources.RecursosES_CO.CedulaExtranjeria
                                Case Eresources.TarjetaIdentidad
                                    Return My.Resources.RecursosES_CO.TarjetaIdentidad
                                Case Eresources.RegistroCivil
                                    Return My.Resources.RecursosES_CO.RegistroCivil
                                Case Eresources.Pasaporte
                                    Return My.Resources.RecursosES_CO.Pasaporte
                                Case Eresources.AdultoSinIdentificacion
                                    Return My.Resources.RecursosES_CO.AdultoSinIdentificacion
                                Case Eresources.MenorSinIdentificacion
                                    Return My.Resources.RecursosES_CO.MenorSinIdentificacion
                                Case Eresources.Nit
                                    Return My.Resources.RecursosES_CO.Nit
                                Case Eresources.NumeroUnicoIdentificacionPersonal
                                    Return My.Resources.RecursosES_CO.NumeroUnicoIdentificacionPersonal
                                Case Eresources.CertificadoNacidoVivo
                                    Return My.Resources.RecursosES_CO.CertificadoNacidoVivo
                                Case Eresources.CarnetDiplomatico
                                    Return My.Resources.RecursosES_CO.CarnetDiplomatico
                                Case Eresources.Salvoconducto
                                    Return My.Resources.RecursosES_CO.Salvoconducto
                                Case Eresources.PermisoEspecialPermanencia
                                    Return My.Resources.RecursosES_CO.PermisoEspecialPermanencia
                                Case Eresources.PermisoProteccionTemporal
                                    Return My.Resources.RecursosES_CO.PermisoProteccionTemporal
                                'Estado civil
                                Case Eresources.Soltero
                                    Return My.Resources.RecursosES_CO.Soltero
                                Case Eresources.Casado
                                    Return My.Resources.RecursosES_CO.Casado
                                Case Eresources.Divorciado
                                    Return My.Resources.RecursosES_CO.Divorciado
                                Case Eresources.Viudo
                                    Return My.Resources.RecursosES_CO.Viudo
                                Case Eresources.UnionLibre
                                    Return My.Resources.RecursosES_CO.UnionLibre
                                'Genero
                                Case Eresources.Masculino
                                    Return My.Resources.RecursosES_CO.Masculino
                                Case Eresources.Femenino
                                    Return My.Resources.RecursosES_CO.Femenino
                                Case Eresources.Otro
                                    Return My.Resources.RecursosES_CO.Otro
                                'SI/NO
                                Case Eresources.Si
                                    Return My.Resources.RecursosES_CO.Si
                                Case Eresources.No
                                    Return My.Resources.RecursosES_CO.No
                                'Activo/Suspendido
                                Case Eresources.Activo
                                    Return My.Resources.RecursosES_CO.Activo
                                Case Eresources.Suspendido
                                    Return My.Resources.RecursosES_CO.Suspendido
                                'Estado
                                Case Eresources.Ninguno
                                    Return My.Resources.RecursosES_CO.Ninguno
                                'Eliminar Registro
                                Case Eresources.EliminarRegistro
                                    Return My.Resources.RecursosES_CO.EliminarRegistro
                                Case Eresources.OcultarFiltros
                                    Return My.Resources.RecursosES_CO.OcultarFiltros
                                Case Eresources.OcultarListaFacturas
                                    Return My.Resources.RecursosES_CO.OcultarListaFacturas
                                Case Eresources.SeleccionesCamposParaFiltros
                                    Return My.Resources.RecursosES_CO.SeleccionesCamposParaFiltros
                                Case Eresources.LabelNumeroDeRegistro
                                    Return My.Resources.RecursosES_CO.LabelNumeroDeRegistro
                                Case Eresources.EditarRegistro
                                    Return My.Resources.RecursosES_CO.EditarRegistro
                                Case Eresources.ComunesErrorConcurrencia
                                    Return My.Resources.RecursosES_CO.ComunesErrorConcurrencia
                                Case Eresources.ComunesErrorDependencia
                                    Return My.Resources.RecursosES_CO.ComunesErrorDependencia
                                Case Eresources.ItemYaAgregado
                                    Return My.Resources.RecursosES_CO.ItemYaAgregado
                                Case Eresources.ComunesSeleccioneAño
                                    Return My.Resources.RecursosES_CO.ComunesSeleccioneAño
                                Case Eresources.ValorMinimoMaximo
                                    Return My.Resources.RecursosES_CO.ValorMinimoMaximo
                                Case Eresources.ValorMaximoMinimoHoras
                                    Return My.Resources.RecursosES_CO.ValorMinimoMaximoHoras
                                Case Eresources.SinRevaluaciones
                                    Return My.Resources.RecursosES_CO.SinRevaluaciones
                                Case Else
                                    Return "Agrege Este Valor a la Pila de Recursos"
                            End Select

                        'FACTURACIÓN
                        Case Eform.Facturacion
                            Select Case recurso
                                Case Eresources.NoExistenParametrosFacturacion
                                    Return My.Resources.RecursosES_CO.SinParametrosFacturacion
                            End Select

                        'Formulario Nueva Especialidad
                        Case Eform.NuevaEspecialidad
                            Select Case recurso
                                Case Eresources.NuevaEspecialidadMensajeCompleto
                                    Return My.Resources.RecursosES_CO.NuevaEspecialidadMesajeCompleto
                            End Select
                        'Formulario Empresas
                        Case Eform.Empresa
                            Select Case recurso
                                Case Eresources.EmpresasMensajeCompleto
                                    Return My.Resources.RecursosES_CO.EmpresaMensajeCompleto
                            End Select
                        'Formulario Entidades
                        Case Eform.Entidades
                            Select Case recurso
                                Case Eresources.EntidadesMensajeCompleto
                                    Return My.Resources.RecursosES_CO.EntidadesMensajeCompleto
                            End Select
                        'Formulario Departamentos
                        Case Eform.Departamentos
                            Select Case recurso
                                Case Eresources.DepartamentosMensajeCompleto
                                    Return My.Resources.RecursosES_CO.DepartamentosMensajeCompleto
                            End Select
                        'Formulario ActividadesCargo
                        Case Eform.ActividadesCargo
                            Select Case recurso
                                Case Eresources.ActividadesCargoMensajeCompleto
                                    Return My.Resources.RecursosES_CO.ActividadesCargoMensajeCompleto
                            End Select

                        'Formulario diagnosticos
                        Case Eform.Diagnosticos
                            Select Case recurso
                                Case Eresources.DiagnosticosMensajeCompleto
                                    Return My.Resources.RecursosES_CO.DiagnosticosMensajeCompleto
                                Case Eresources.IngreseCIE10
                                    Return My.Resources.RecursosES_CO.DiagnosticosIngreseCIE10
                                Case Eresources.IngreseEdadMaxima
                                    Return My.Resources.RecursosES_CO.DiagnosticosIngreseEdadMaxima
                                Case Eresources.IngreseEdadMinima
                                    Return My.Resources.RecursosES_CO.DiagnositicosIngreseEdadMinima
                                Case Eresources.SeleccioneUnidadEdadMinima
                                    Return My.Resources.RecursosES_CO.DiagnosticosSeleccioneUnidadEdadMinima
                                Case Eresources.SeleccioneUnidadEdadMaxima
                                    Return My.Resources.RecursosES_CO.DiagnosticosSeleccioneUnidadEdadMaxima
                                Case Eresources.SeleccioneSexo
                                    Return My.Resources.RecursosES_CO.DiagnosticosSeleccioneSexo
                                Case Eresources.EdadNovalida
                                    Return My.Resources.RecursosES_CO.EdadNovalida
                                Case Eresources.UnidadNovalida
                                    Return My.Resources.RecursosES_CO.UnidadNovalida
                            End Select

                        'Formulario diagnosticos Sindromaticos
                        Case Eform.DXSindromaticos
                            Select Case recurso
                                Case Eresources.SidromaticoMensajeCompleto
                                    Return My.Resources.RecursosES_CO.SidromaticoMensajeCompleto
                                Case Eresources.SindromaticoTipo
                                    Return My.Resources.RecursosES_CO.SindromaticoTipo
                                Case Eresources.SindromaticoNivel
                                    Return My.Resources.RecursosES_CO.SindromaticoNivel
                                Case Eresources.SidromaticoUrgenciaDiferida
                                    Return My.Resources.RecursosES_CO.SidromaticoUrgenciaDiferida
                                Case Eresources.SidromaticoEmergencia
                                    Return My.Resources.RecursosES_CO.SidromaticoEmergencia
                                Case Eresources.SidromaticoUrgenciaMedica
                                    Return My.Resources.RecursosES_CO.SidromaticoUrgenciaMedica
                                Case Eresources.SidromaticoNoUrgente
                                    Return My.Resources.RecursosES_CO.SidromaticoNoUrgente

                            End Select

                        'Formulario dias festivos
                        Case Eform.DiasFestivos
                            Select Case recurso
                                Case Eresources.FestivosMensajeCompleto
                                    Return My.Resources.RecursosES_CO.FestivosMensajeCompleto
                                Case Eresources.FestivosMotivo
                                    Return My.Resources.RecursosES_CO.FestivosMotivo
                                Case Eresources.festivosFecha
                                    Return My.Resources.RecursosES_CO.festivosFecha
                            End Select

                        'Formulario profesiones
                        Case Eform.Profesiones
                            Select Case recurso
                                Case Eresources.ProfesionesMensajeCompleto
                                    Return My.Resources.RecursosES_CO.ProfesionesMensajeCompleto
                            End Select

                        'Formulario salarios
                        Case Eform.Salarios
                            Select Case recurso
                                Case Eresources.SalariosMensajeCompleto
                                    Return My.Resources.RecursosES_CO.SalariosMensajeCompleto
                                Case Eresources.SalariosValor
                                    Return My.Resources.RecursosES_CO.SalariosValor
                            End Select

                        'Formulario municipios
                        Case Eform.Municipios
                            Select Case recurso
                                Case Eresources.SeleccioneDepartamento
                                    Return My.Resources.RecursosES_CO.SeleccioneDepartamento
                                Case Eresources.MunicipiosMensajeCompleto
                                    Return My.Resources.RecursosES_CO.MunicipiosMensajeCompleto
                            End Select

                        'Formulario Parametros Centro Atencion
                        Case Eform.Parametros3047
                            Select Case recurso
                                Case Eresources.ParametrosEspecifiqueEmailLocal
                                    Return My.Resources.RecursosES_CO.ParametrosEspecifiqueEmailLocal
                                Case Eresources.ParametrosEspecifiqueIndicativoLocal
                                    Return My.Resources.RecursosES_CO.ParametrosEspecifiqueIndicativoLocal
                                Case Eresources.ParametrosEspecifiqueFaxLocal
                                    Return My.Resources.RecursosES_CO.ParametrosEspecifiqueFaxLocal
                                Case Eresources.ParametrosEspecifiqueExtencionLocal
                                    Return My.Resources.RecursosES_CO.ParametrosEspecifiqueExtencionLocal
                                Case Eresources.ParametrosSeleccionesCentroAtencion
                                    Return My.Resources.RecursosES_CO.ParametrosSeleccionesCentroAtencion
                                Case Eresources.ParametrosRutaExedeCaracteresMaximo
                                    Return My.Resources.RecursosES_CO.ParametrosRutaExedeCaracteresMaximo
                            End Select

                        'Formulario niveles
                        Case Eform.Niveles
                            Select Case recurso
                                Case Eresources.NivelesMensajeCompleto
                                    Return My.Resources.RecursosES_CO.NivelesMensajeCompleto
                            End Select
                        Case Eform.ActividadesMedicas
                            Select Case recurso
                                Case Eresources.ActividadesMensajeCompleto
                                    Return My.Resources.RecursosES_CO.ActividadesMensajeCompleto
                                Case Eresources.ColorEtiqueta
                                    Return My.Resources.RecursosES_CO.ColorEtiqueta
                                Case Eresources.NumeroPaciente
                                    Return My.Resources.RecursosES_CO.NumeroPaciente
                                Case Eresources.Duracion
                                    Return My.Resources.RecursosES_CO.Duracion
                                Case Eresources.ControlMensual
                                    Return My.Resources.RecursosES_CO.ControlMensual
                                Case Eresources.ControlDiario
                                    Return My.Resources.RecursosES_CO.ControlDiario
                                Case Eresources.Intervalo
                                    Return My.Resources.RecursosES_CO.Intervalo

                            End Select
                        'formulario EspecialidadesActividades
                        Case Eform.EspecialidadesActividades
                            Select Case recurso
                                Case Eresources.ActividadRelacionadaExistente
                                    Return My.Resources.RecursosES_CO.ActividadRelacionadaExistente
                                Case Eresources.Actividadeliminada
                                    Return My.Resources.RecursosES_CO.ActividadEliminada
                            End Select
                        'formulario CausasCancelacion
                        Case Eform.CausaCancelacion
                            Select Case recurso
                                Case Eresources.CausaCancelacionMensajeCompleto
                                    Return My.Resources.RecursosES_CO.CausaCancelacionMensajeCompleto
                            End Select
                        'formulario clasificacion cups
                        Case Eform.ClasificacionCups
                            Select Case recurso
                                Case Eresources.CupsSubGrupo
                                    Return My.Resources.RecursosES_CO.CupsSubGrupo
                                Case Else
                                    Return "Agrege Este Valor a la Pila de Recursos"
                            End Select
                        'Formulario Iva
                        Case Eform.Iva
                            Select Case recurso
                                Case Eresources.IvaMensajeComplemento
                                    Return My.Resources.RecursosES_CO.IvaMensajeComplemento
                                Case Else
                                    Return "Agrege Este Valor a la Pila de Recursos"
                            End Select
                        'Formulario Profesionales
                        Case Eform.Profesionales
                            Select Case recurso
                                Case Eresources.ProfesionalesRelacioneEspecialidad
                                    Return My.Resources.RecursosES_CO.ProfesionalesRelacioneEspecialiad
                                Case Eresources.ProfesionalesMensajeEspecialidadRepetida
                                    Return My.Resources.RecursosES_CO.ProfesionalEspecialidadRepetida
                                Case Else
                                    Return "Agrege Este Valor a la Pila de Recursos"
                            End Select

                        Case Eform.Pacientes
                            Select Case recurso
                                Case Eresources.IdentificacionMamaNOValida
                                    Return My.Resources.RecursosES_CO.IdentificacionMamaNOValida
                                Case Eresources.NoexisteDatosMama
                                    Return My.Resources.RecursosES_CO.NoexisteDatosMama
                                Case Eresources.NoexisteuncodigodelaMama
                                    Return My.Resources.RecursosES_CO.NoexisteuncodigodelaMama
                                Case Eresources.DebeDigitarDeNuevolaIdentificacion
                                    Return My.Resources.RecursosES_CO.DebeDigitarDeNuevolaIdentificacion
                                Case Eresources.LasIdentificacionNoCoincidieron
                                    Return My.Resources.RecursosES_CO.LasIdentificacionNoCoincidieron
                                Case Eresources.Alerta
                                    Return My.Resources.RecursosES_CO.Alerta
                                Case Eresources.IdentificacionMamaEsdeHombre
                                    Return My.Resources.RecursosES_CO.IdentificacionMamaEsdeHombre
                                Case Eresources.Hijo
                                    Return My.Resources.RecursosES_CO.Hijo
                                Case Eresources.Hija
                                    Return My.Resources.RecursosES_CO.Hija
                                Case Eresources.ErrorAlcopiarAlservidorWeb
                                    Return My.Resources.RecursosES_CO.ErrorAlcopiarAlservidorWeb
                                Case Eresources.NumeroIdentificacion
                                    Return My.Resources.RecursosES_CO.NumeroIdentificacion
                                Case Eresources.Mensajecomplemento
                                    Return My.Resources.RecursosES_CO.Mensajecomplemento
                                Case Eresources.NoExistenParametros
                                    Return My.Resources.RecursosES_CO.NoExistenParametros
                                Case Eresources.ErrorAdjuntararchivos
                                    Return My.Resources.RecursosES_CO.ErrorAdjuntararchivos
                                Case Eresources.PacientesConElNumeroDeIdentificacion
                                    Return My.Resources.RecursosES_CO.PacientesConElNumeroDeIdentificacion
                                Case Eresources.PacientesDebeDigitarIdMadre
                                    Return My.Resources.RecursosES_CO.PacientesDebeDigitarIdMadre
                                Case Eresources.PacientesDebeSeleccionarTipoIdAnonimos
                                    Return My.Resources.RecursosES_CO.PacientesDebeSeleccionarTipoIdAnonimos
                                Case Eresources.PacientesDebeDigitarCodigoMadre
                                    Return My.Resources.RecursosES_CO.PacientesDebeDigitarCodigoMadre
                                Case Eresources.PacientesEspecifiqueLugarExpedicion
                                    Return My.Resources.RecursosES_CO.PacientesEspecifiqueLugarExpedicion
                                Case Eresources.PacientesEspecifiquePrimerApellido
                                    Return My.Resources.RecursosES_CO.PacientesEspecifiquePrimerApellido
                                Case Eresources.PacientesEspecifiqueFechaNacimiento
                                    Return My.Resources.RecursosES_CO.PacientesEspecifiqueFechaNacimiento
                                Case Eresources.PacientesLaFechaNoPuedeSerIgualoMayoraLaActual
                                    Return My.Resources.RecursosES_CO.PacientesLaFechaNoPuedeSerIgualoMayoraLaActual
                                Case Eresources.PacientesEspecifiqueSexo
                                    Return My.Resources.RecursosES_CO.PacientesEspecifiqueSexo
                                Case Eresources.PacientesEspecifiqueDatosContacto
                                    Return My.Resources.RecursosES_CO.PacientesEspecifiqueDatosContacto
                                Case Eresources.PacientesEspecifiqueEstadoCivil
                                    Return My.Resources.RecursosES_CO.PacientesEspecifiqueEstadoCivil
                                Case Eresources.PacientesDeseaCargarDatosMadre
                                    Return My.Resources.RecursosES_CO.PacientesDeseaCargarDatosMadre
                                Case Eresources.PacientesNoExisteUnCodigoMadre
                                    Return My.Resources.RecursosES_CO.PacientesNoExisteUnCodigoMadre
                                Case Eresources.PacientesEcribaDescripcionArchivo
                                    Return My.Resources.RecursosES_CO.PacientesEcribaDescripcionArchivo
                                Case Eresources.PacientesNoExisteRutaDocumentos
                                    Return My.Resources.RecursosES_CO.PacientesNoExisteRutaDocumentos
                                Case Eresources.PacientesLaRutaDeDocumentoDefinidaPara
                                    Return My.Resources.RecursosES_CO.PacientesLaRutaDeDocumentoDefinidaPara
                                Case Eresources.PacientesDocumentoValidos
                                    Return My.Resources.RecursosES_CO.PacientesDocumentoValidos
                                Case Eresources.PacientesElTamañoPermitido
                                    Return My.Resources.RecursosES_CO.PacientesElTamañoPermitido
                                Case Eresources.PacientesArchivoYaSeEncuentra
                                    Return My.Resources.RecursosES_CO.PacientesArchivoYaSeEncuentra
                                Case Eresources.PacientesEditarReglasNegocio
                                    Return My.Resources.RecursosES_CO.PacientesEditarReglasNegocio
                                Case Eresources.PacientesPrimerNombre
                                    Return My.Resources.RecursosES_CO.PacientesPrimerNombre
                                Case Eresources.PacientesEditarFlujoTrabajo
                                    Return My.Resources.RecursosES_CO.PacientesEditarFlujoTrabajo
                                Case Eresources.PacintesParametrosNoEstablecidos
                                    Return My.Resources.RecursosES_CO.PacintesParametrosNoEstablecidos
                                Case Eresources.PacintesDebeSeleccionarUnPaciente
                                    Return My.Resources.RecursosES_CO.PacintesDebeSeleccionarUnPaciente()
                            End Select

                        Case Eform.EntidadesAdministradores
                            Select Case recurso
                                Case Eresources.EntidadMensajeComplemento
                                    Return My.Resources.RecursosES_CO.EntidadMensajeComplemento
                                Case Else
                                    Return "Agrege Este Valor a la Pila de Recursos"
                            End Select
                        'FORMULARIO DE PAISES
                        Case Eform.Country
                            Select Case recurso
                                Case Eresources.SeleccioneUnPais
                                    Return My.Resources.RecursosES_CO.SeleccioneUnPais
                                Case Else
                                    Return "Agregue este valor a la pila de recursos"
                            End Select

                        'FORMULARIO DE TIPO DE PENSIONADO
                        Case Eform.TipoPensionado
                            Select Case recurso
                                Case Eresources.SeleccioneUnTipoPensionado
                                    Return My.Resources.RecursosES_CO.SeleccioneUnTipoPensionado
                                Case Else
                                    Return "Agregue este valor a la pila de recursos"
                            End Select

                        'Comunes - Centro de trabajo
                        Case Eform.CentroTrabajo
                            Select Case recurso
                                Case Eresources.SeleccioneUnCentroTrabajo
                                    Return My.Resources.RecursosES_CO.SeleccioneUnCentroTrabajo
                            End Select

                        'FORMULARIO DE CIUDADES
                        Case Eform.City
                            Select Case recurso
                                Case Eresources.SeleccioneUnaCiudad
                                    Return My.Resources.RecursosES_CO.SeleccioneUnaCiudad
                                Case Else
                                    Return "Agregue este valor a la pila de recursos"
                            End Select
                        'FORMULARIO CORPORACIONES
                        Case Eform.Corporation
                            Select Case recurso
                                Case Eresources.seleccioneCorporacion
                                    Return My.Resources.RecursosES_CO.seleccioneCorporacion
                                Case Else
                                    Return "Agregue este valor a la pila de recursos"
                            End Select
                        'FORMULARIO GRUPOS DE NOMINA
                        Case Eform.groups
                            Select Case recurso
                                Case Eresources.seleccioneGrupo
                                    Return My.Resources.RecursosES_CO.seleccioneGrupo
                                Case Eresources.conceptoYaAgregado
                                    Return My.Resources.RecursosES_CO.conceptoYaAgregado
                                Case Eresources.ordinario
                                    Return My.Resources.RecursosES_CO.ordinario
                                Case Eresources.feriado
                                    Return My.Resources.RecursosES_CO.feriado
                                Case Eresources.normal
                                    Return My.Resources.RecursosES_CO.normal
                                Case Eresources.nocturno
                                    Return My.Resources.RecursosES_CO.nocturno
                                Case Eresources.GrupoEventosMinReg
                                    Return My.Resources.RecursosES_CO.GrupoEventosMinReg
                                Case Else
                                    Return "Agregue este valor a la pila de recursos"
                            End Select

                        'FORMULARIO DE LENGUAJES
                        Case Eform.Languages
                            Select Case recurso
                                Case Eresources.seleccioneLenguaje
                                    Return My.Resources.RecursosES_CO.seleccioneLenguaje
                                Case Else
                                    Return "Agregue este valor a la pila de recursos"
                            End Select

                        'Nomina - Riesgos Profesionales
                        Case Eform.RiesgoProfesional
                            Select Case recurso
                                Case Eresources.SeleccioneUnRiesgoProfesional
                                    Return My.Resources.RecursosES_CO.SeleccioneUnRiesgoProfesional
                            End Select

                        'Comunes - discapacidades
                        Case Eform.Discapacidad
                            Select Case recurso
                                Case Eresources.SeleccioneUnaDiscapacidad
                                    Return My.Resources.RecursosES_CO.SeleccioneUnaDiscapacidad
                            End Select

                        'Comunes - Tipo de contribuyente
                        Case Eform.TipoContribuyente
                            Select Case recurso
                                Case Eresources.SeleccioneUnTipocontribuyente
                                    Return My.Resources.RecursosES_CO.SeleccioneUnTipocontribuyente
                            End Select

                        'Comunes - Tipo de estudio
                        Case Eform.TipoEstudio
                            Select Case recurso
                                Case Eresources.SeleccioneUnTipoestudio
                                    Return My.Resources.RecursosES_CO.SeleccioneUnTipoestudio
                                Case Eresources.Primaria
                                    Return My.Resources.RecursosES_CO.Primaria
                                Case Eresources.Secundaria
                                    Return My.Resources.RecursosES_CO.Secundaria
                                Case Eresources.Tecnico
                                    Return My.Resources.RecursosES_CO.Tecnico
                                Case Eresources.Universitario
                                    Return My.Resources.RecursosES_CO.Universitario
                                Case Eresources.Postgrado
                                    Return My.Resources.RecursosES_CO.Postgrado
                            End Select

                        'Comunes - grupo de contratos
                        Case Eform.GrupoContrato
                            Select Case recurso
                                Case Eresources.SeleccioneUnGrupoContrato
                                    Return My.Resources.RecursosES_CO.SeleccioneUnGrupoContrato
                            End Select
                        'Comunes - Terceros
                        Case Eform.Terceros
                            Select Case recurso
                                Case Eresources.SeleccioneUnTercero
                                    Return My.Resources.RecursosES_CO.SeleccioneUnTercero
                            End Select
                        'Nomina - Tipo de Contrato
                        Case Eform.TipoDeContrato
                            Select Case recurso
                                Case Eresources.SeleccioneUnTipodeContrato
                                    Return My.Resources.RecursosES_CO.SeleccioneUnTipodeContrato
                            End Select
                        'Nomina - Centro de estudios
                        Case Eform.CentrosdeEstudio
                            Select Case recurso
                                Case Eresources.SeleccioneUnCentrodeEstudio
                                    Return My.Resources.RecursosES_CO.SeleccioneUnCentrodeEstudio
                            End Select
                        'Nomina - plantilla de contratos
                        Case Eform.PlantillaContrato
                            Select Case recurso
                                Case Eresources.SeleccionePlantilladeContrato
                                    Return My.Resources.RecursosES_CO.SeleccionePlantilladeContrato
                            End Select

                        'Nomina - Cargos
                        Case Eform.Cargo
                            Select Case recurso
                                Case Eresources.SeleccioneUnCargo
                                    Return My.Resources.RecursosES_CO.SeleccioneUnCargo
                            End Select
                        'Nomina - Cargos
                        Case Eform.Retenciones
                            Select Case recurso
                                Case Eresources.SeleccioneRetencion
                                    Return My.Resources.RecursosES_CO.SeleccioneRetencion
                            End Select
                        'Nomina - Tipo de Contrato
                        Case Eform.TipodeVinculacion
                            Select Case recurso
                                Case Eresources.SeleccioneUnTipodeVinculacion
                                    Return My.Resources.RecursosES_CO.SeleccioneUnTipodeVinculacion
                            End Select
                        'Nomina - Unidad de Tiempo
                        Case Eform.UnidaddeTiempo
                            Select Case recurso
                                Case Eresources.SeleccioneUnaUnidaddeTiempo
                                    Return My.Resources.RecursosES_CO.SeleccioneUnaUnidaddeTiempo
                            End Select
                        'Nomina - Autorizacion de conceptos
                        Case Eform.AutorizacionConceptos
                            Select Case recurso
                                Case Eresources.ProcesoRealizadoConExito
                                    Return My.Resources.RecursosES_CO.ProcesoRealizadoConExito
                                Case Eresources.ConceptoAutorizadoPorGrupo
                                    Return My.Resources.RecursosES_CO.ConceptoAutorizadoPorGrupo
                                Case Eresources.ConceptosAutorizadoPorGrupo
                                    Return My.Resources.RecursosES_CO.ConceptosAutorizadoPorGrupo
                                Case Eresources.ClaseConceptoNoPermiteFormula
                                    Return My.Resources.RecursosES_CO.ClaseConceptoNoPermiteFormula
                            End Select
                        'Formulario de Convenios
                        Case Eform.Agreements
                            Select Case recurso
                                Case Eresources.PaidValueExceeds
                                    Return My.Resources.RecursosES_CO.PaidValueExceeds
                                Case Eresources.TipoPorNomina
                                    Return My.Resources.RecursosES_CO.TipoPorNomina
                                Case Eresources.TipoPorArchivo
                                    Return My.Resources.RecursosES_CO.TipoPorArchivo
                                Case Eresources.TipoManual
                                    Return My.Resources.RecursosES_CO.TipoManual
                            End Select
                        'Formulario de Conceptos manuales
                        Case Eform.ConceptosManuales
                            Select Case recurso
                                Case Eresources.EstadoActivo
                                    Return My.Resources.RecursosES_CO.EstadoActivo
                                Case Eresources.EstadoSuspendido
                                    Return My.Resources.RecursosES_CO.EstadoSuspendido
                                Case Eresources.EstadoCompletado
                                    Return My.Resources.RecursosES_CO.EstadoCompletado
                                Case Eresources.FormPagoPrimeraQuincena
                                    Return My.Resources.RecursosES_CO.FormPagoPrimeraQuincena
                                Case Eresources.FormPagoSegundaQuincena
                                    Return My.Resources.RecursosES_CO.FormPagoSegundaQuincena
                                Case Eresources.FormPagoAmbas
                                    Return My.Resources.RecursosES_CO.FormPagoAmbas
                                Case Eresources.FormPagoMensual
                                    Return My.Resources.RecursosES_CO.FormPagoMensual
                                Case Eresources.EstadoEsperandoPago
                                    Return My.Resources.RecursosES_CO.EstadoEsperandoPago
                                Case Eresources.EstadoPagado
                                    Return My.Resources.RecursosES_CO.EstadoPagado
                                Case Eresources.DeseaSuspender
                                    Return My.Resources.RecursosES_CO.DeseaSuspender
                                Case Eresources.YaTieneNominaLiquidada
                                    Return My.Resources.RecursosES_CO.YaTieneNominaLiquidada
                                Case Eresources.YaTieneConcManConEsteConcDesdeHasta
                                    Return My.Resources.RecursosES_CO.YaTieneConcManConEsteConcDesdeHasta
                                Case Eresources.YaTieneConcManConEsteConcDesdeHastaFinal
                                    Return My.Resources.RecursosES_CO.YaTieneConcManConEsteConcDesdeHastaFinal
                                Case Eresources.YaTieneEsteConceptoManualHastaFin
                                    Return My.Resources.RecursosES_CO.YaTieneEsteConceptoManualHastaFin
                                Case Eresources.NoSeRegistraronTodasCuotas
                                    Return My.Resources.RecursosES_CO.NoSeRegistraronTodasCuotas
                            End Select
                        'frontal de archivo plano para banco
                        Case Eform.ArchivoBanco
                            Select Case recurso
                                Case Eresources.GrupoNoTieneEstructuraArch
                                    Return My.Resources.RecursosES_CO.GrupoNoTieneEstructuraArch
                                Case Eresources.GuardadoDeseaAbrir
                                    Return My.Resources.RecursosES_CO.GuardadoDeseaAbrir
                                Case Eresources.ExisteAuditoriaArchivoBanco
                                    Return My.Resources.RecursosES_CO.ExisteAuditoriaArchivoBanco
                            End Select

                        'frontal de fuentes de financiacion
                        Case Eform.FinancialSource
                            Select Case recurso
                                Case Eresources.ConSituacion
                                    Return My.Resources.RecursosES_CO.ConSituacion
                                Case Eresources.SinSituacion
                                    Return My.Resources.RecursosES_CO.SinSituacion
                                Case Eresources.RecursosPropios
                                    Return My.Resources.RecursosES_CO.RecursosPropios
                            End Select

                        'frontal de Entidades PResupuestales
                        Case Eform.BudgetEntities
                            Select Case recurso
                                Case Eresources.Activa
                                    Return My.Resources.RecursosES_CO.Activa
                                Case Eresources.Cerrada
                                    Return My.Resources.RecursosES_CO.Cerrada
                                Case Eresources.Registrada
                                    Return My.Resources.RecursosES_CO.Registrada
                                Case Eresources.PresupuestoInicial
                                    Return My.Resources.RecursosES_CO.PresupuestoInicial
                                Case Eresources.ModificacionesAlPresupuesto
                                    Return My.Resources.RecursosES_CO.ModificacionesAlPresupuesto
                                Case Eresources.TrasladosLaPresupuesto
                                    Return My.Resources.RecursosES_CO.TrasladosLaPresupuesto
                                Case Eresources.ModificacionesAlPAC
                                    Return My.Resources.RecursosES_CO.TrasladosLaPresupuesto
                                Case Eresources.TrasladosAlPAC
                                    Return My.Resources.RecursosES_CO.TrasladosAlPAC
                                Case Eresources.Reconocimientos
                                    Return My.Resources.RecursosES_CO.Reconocimientos
                                Case Eresources.ModificacionesAReconocimiento
                                    Return My.Resources.RecursosES_CO.ModificacionesAReconocimiento
                                Case Eresources.Recaudos
                                    Return My.Resources.RecursosES_CO.Recaudos
                                Case Eresources.ModificacionARecaudos
                                    Return My.Resources.RecursosES_CO.ModificacionARecaudos
                                Case Eresources.CuentasPorCobrar
                                    Return My.Resources.RecursosES_CO.CuentasPorCobrar
                                Case Eresources.Disponibilidades
                                    Return My.Resources.RecursosES_CO.Disponibilidades
                                Case Eresources.ModificacionesADisponibilidades
                                    Return My.Resources.RecursosES_CO.ModificacionesADisponibilidades
                                Case Eresources.Compromisos
                                    Return My.Resources.RecursosES_CO.Compromisos
                                Case Eresources.ModificacionesACompromisos
                                    Return My.Resources.RecursosES_CO.ModificacionesACompromisos
                                Case Eresources.Obligaciones
                                    Return My.Resources.RecursosES_CO.Obligaciones
                                Case Eresources.ModificacionesAObligaciones
                                    Return My.Resources.RecursosES_CO.ModificacionesAObligaciones
                                Case Eresources.OrdenDePago
                                    Return My.Resources.RecursosES_CO.OrdenDePago
                                Case Eresources.ProrrogasDeDisponibilidades
                                    Return My.Resources.RecursosES_CO.ProrrogasDeDisponibilidades
                                Case Eresources.LiberacionDeRecursos
                                    Return My.Resources.RecursosES_CO.LiberacionDeRecursos
                                Case Eresources.ReintegroDeRecurso
                                    Return My.Resources.RecursosES_CO.ReintegroDeRecurso
                                Case Eresources.ReservasPresupuestales
                                    Return My.Resources.RecursosES_CO.ReservasPresupuestales
                                Case Eresources.CuentasPorPagarPresupuestales
                                    Return My.Resources.RecursosES_CO.CuentasPorPagarPresupuestales
                                Case Eresources.DisponibilidadesVFT
                                    Return My.Resources.RecursosES_CO.DisponibilidadesVFT
                                Case Eresources.CompromisosVFT
                                    Return My.Resources.RecursosES_CO.CompromisosVFT
                                Case Eresources.ObligacionesVFT
                                    Return My.Resources.RecursosES_CO.ObligacionesVFT
                                Case Eresources.OrdenesDePagoVFT
                                    Return My.Resources.RecursosES_CO.OrdenesDePagoVFT
                                Case Eresources.SuspencionAlPresupuestoDeGastos
                                    Return My.Resources.RecursosES_CO.SuspencionAlPresupuestoDeGastos
                                Case Eresources.LevantamientoDeSuspencionesAlPresupuestoDeGastos
                                    Return My.Resources.RecursosES_CO.LevantamientoDeSuspencionesAlPresupuestoDeGastos
                                Case Eresources.ProrrogasDeDocumentos
                                    Return My.Resources.RecursosES_CO.ProrrogasDeDocumentos

                            End Select

                        'frontal de tipos de ingreso
                        Case Eform.RevenueType
                            Select Case recurso
                                Case Eresources.AportesNacionales
                                    Return My.Resources.RecursosES_CO.AportesNacionales
                                Case Eresources.Ingresospropios
                                    Return My.Resources.RecursosES_CO.Ingresospropios
                                Case Eresources.Disponibilidadinicial
                                    Return My.Resources.RecursosES_CO.Disponibilidadinicial
                                Case Eresources.Ventadeservicios
                                    Return My.Resources.RecursosES_CO.Ventadeservicios
                                Case Eresources.Sistemageneraldeparticipaciones
                                    Return My.Resources.RecursosES_CO.Sistemageneraldeparticipaciones
                                Case Eresources.Otrosingresos
                                    Return My.Resources.RecursosES_CO.Otrosingresos
                                Case Eresources.Ingresoscapital
                                    Return My.Resources.RecursosES_CO.Ingresoscapital
                                Case Eresources.Totalingresos
                                    Return My.Resources.RecursosES_CO.Totalingresos
                            End Select

                        'frontal de tipos de gasto
                        Case Eform.RevenueType
                            Select Case recurso
                                Case Eresources.Funcionamiento
                                    Return My.Resources.RecursosES_CO.Funcionamiento
                                Case Eresources.Inversión
                                    Return My.Resources.RecursosES_CO.Inversión
                                Case Eresources.ServiciosDeuda
                                    Return My.Resources.RecursosES_CO.ServiciosDeuda
                                Case Eresources.GastosPersonal
                                    Return My.Resources.RecursosES_CO.GastosPersonal
                                Case Eresources.GastosGenerales
                                    Return My.Resources.RecursosES_CO.GastosGenerales
                                Case Eresources.MantenimientoHospitalario
                                    Return My.Resources.RecursosES_CO.MantenimientoHospitalario
                                Case Eresources.Trasferencias
                                    Return My.Resources.RecursosES_CO.Trasferencias
                                Case Eresources.GastosOperaconComercial
                                    Return My.Resources.RecursosES_CO.GastosOperaconComercial
                                Case Eresources.DisponibilidadFinal
                                    Return My.Resources.RecursosES_CO.DisponibilidadFinal
                            End Select

                        'formulario de empleado
                        Case Eform.Empleado

                            Select Case recurso
                            'Tipos de cesantias
                                Case Eresources.NoAplica
                                    Return My.Resources.RecursosES_CO.NoAplica
                                Case Eresources.Tradicional
                                    Return My.Resources.RecursosES_CO.Tradicional
                                Case Eresources.TradicionalMensual
                                    Return My.Resources.RecursosES_CO.TradicionalMensual
                                Case Eresources.Consignado
                                    Return My.Resources.RecursosES_CO.Consignado
                                Case Eresources.ConsignadoMensual
                                    Return My.Resources.RecursosES_CO.ConsignadoMensual
                                Case Eresources.Ley33
                                    Return My.Resources.RecursosES_CO.Ley33
                                'tipos de sindicato
                                Case Eresources.Convencionado
                                    Return My.Resources.RecursosES_CO.Convencionado
                                Case Eresources.Sindicalizado
                                    Return My.Resources.RecursosES_CO.Sindicalizado
                                Case Eresources.PactoColectivo
                                    Return My.Resources.RecursosES_CO.PactoColectivo
                                'Estado de estudios
                                Case Eresources.EstudiaActualmente
                                    Return My.Resources.RecursosES_CO.EstudiaActualmente
                                Case Eresources.EstudioInterrumpido
                                    Return My.Resources.RecursosES_CO.EstudioInterrumpido
                                Case Eresources.EstudioTerminado
                                    Return My.Resources.RecursosES_CO.EstudioTerminado
                                Case Eresources.Graduado
                                    Return My.Resources.RecursosES_CO.Graduado
                                'Libreta militar
                                Case Eresources.NoTieneLibreta
                                    Return My.Resources.RecursosES_CO.NoTieneLibreta
                                Case Eresources.LibretaPrimera
                                    Return My.Resources.RecursosES_CO.LibretaPrimera
                                Case Eresources.LibretaSegunda
                                    Return My.Resources.RecursosES_CO.LibretaSegunda
                                'tipos de retencion
                                Case Eresources.Autoretenedor
                                    Return My.Resources.RecursosES_CO.Autoretenedor
                                Case Eresources.HaceRetencion
                                    Return My.Resources.RecursosES_CO.HaceRetencion
                                Case Eresources.ExentoRetencion
                                    Return My.Resources.RecursosES_CO.ExentoRetencion
                                'tipo de educacion
                                Case Eresources.Formal
                                    Return My.Resources.RecursosES_CO.Formal
                                Case Eresources.NoFormal
                                    Return My.Resources.RecursosES_CO.NoFormal
                                Case Eresources.MinimoQuinceLaborar
                                    Return My.Resources.RecursosES_CO.MinimoQuinceLaborar
                                Case Eresources.FechaXMenorQueY
                                    Return My.Resources.RecursosES_CO.FechaXMenorQueY
                                Case Eresources.EmpleadoNoSeElimino
                                    Return My.Resources.RecursosES_CO.EmpleadoNoSeElimino
                                Case Eresources.EmpleadoTieneLiquidaciones
                                    Return My.Resources.RecursosES_CO.EmpleadoTieneLiquidaciones
                                Case Eresources.EmpleadoTieneLiquidacionContrato
                                    Return My.Resources.RecursosES_CO.EmpleadoTieneLiquidacionContrato
                                Case Eresources.EmpleadoTieneNovedades
                                    Return My.Resources.RecursosES_CO.EmpleadoTieneNovedades
                                Case Eresources.EmpleadoTieneTurno
                                    Return My.Resources.RecursosES_CO.EmpleadoTieneTurno
                                Case Eresources.EmpleadoTieneConvenios
                                    Return My.Resources.RecursosES_CO.EmpleadoTieneConvenios
                                Case Else
                                    Return "Agregue este valor a la pila de recursos"
                            End Select
                        Case Eform.Aseguradoras
                            Select Case recurso
                                Case Eresources.SeleccioneAseguradora
                                    Return My.Resources.RecursosES_CO.SeleccioneAseguradora
                                Case Else
                                    Return "Agregue este valor a la pila de recursos"
                            End Select
                        Case Eform.Fabricantes
                            Select Case recurso
                                Case Eresources.SeleccioneFabricantes
                                    Return My.Resources.RecursosES_CO.SeleccioneFabricante
                                Case Else
                                    Return "Agregue este valor a la pila de recursos"
                            End Select
                        Case Eform.Poliza
                            Select Case recurso
                                Case Eresources.SeleccionePoliza
                                    Return My.Resources.RecursosES_CO.SeleccionePoliza
                                Case Else
                                    Return "Agregue este valor a la pila de recursos"
                            End Select
                        Case Eform.TiposPoliza
                            Select Case recurso
                                Case Eresources.SeleccioneTipoPoliza
                                    Return My.Resources.RecursosES_CO.SeleccioneTipoPoliza
                                Case Else
                                    Return "Agregue este valor a la pila de recursos"
                            End Select
                        Case Eform.TiposEquipos
                            Select Case recurso
                                Case Eresources.SeleccioneTipoEquipos
                                    Return My.Resources.RecursosES_CO.SeleccioneTipoEquipo
                                Case Else
                                    Return "Agregue este valor a la pila de recursos"
                            End Select
                        Case Eform.TiposInventarios
                            Select Case recurso
                                Case Eresources.SeleccioneTipoInventario
                                    Return My.Resources.RecursosES_CO.SeleccioneTipoInventario
                                Case Else
                                    Return "Agregue este valor a la pila de recursos"
                            End Select
                        Case Eform.Sucursales
                            Select Case recurso
                                Case Eresources.SeleccioneSucursal
                                    Return My.Resources.RecursosES_CO.SeleccioneSucursal
                                Case Else
                                    Return "Agregue este valor a la pila de recursos"
                            End Select
                        Case Eform.Accesorios
                            Select Case recurso
                                Case Eresources.SeleccioneAccesorios
                                    Return My.Resources.RecursosES_CO.SeleccioneAccesorio
                                Case Else
                                    Return "Agregue este valor a la pila de recursos"
                            End Select
                        Case Eform.Consumibles
                            Select Case recurso
                                Case Eresources.SeleccioneConsumibles
                                    Return My.Resources.RecursosES_CO.SeleccioneConsumible
                                Case Else
                                    Return "Agregue este valor a la pila de recursos"
                                    'formulario de contrato
                            End Select
                        Case Eform.Contrato
                            Select Case recurso
                            'Tipo de salario
                                Case Eresources.Fijo
                                    Return My.Resources.RecursosES_CO.Fijo
                                Case Eresources.Integral
                                    Return My.Resources.RecursosES_CO.Integral
                                Case Eresources.Variable
                                    Return My.Resources.RecursosES_CO.Variable
                                'Periodo de pago
                                Case Eresources.Mensual
                                    Return My.Resources.RecursosES_CO.Mensual
                                Case Eresources.Quincenal
                                    Return My.Resources.RecursosES_CO.Quincenal
                                'Tipo de pago
                                Case Eresources.Cheque
                                    Return My.Resources.RecursosES_CO.Cheque
                                Case Eresources.Efectivo
                                    Return My.Resources.RecursosES_CO.Efectivo
                                Case Eresources.Consignacion
                                    Return My.Resources.RecursosES_CO.Consignacion
                                Case Eresources.Desprendible
                                    Return My.Resources.RecursosES_CO.Desprendible
                                'Tipo de cuenta bancaria"
                                Case Eresources.Ahorros
                                    Return My.Resources.RecursosES_CO.Ahorros
                                Case Eresources.Corriente
                                    Return My.Resources.RecursosES_CO.Corriente
                                'Tipo de empleado
                                Case Eresources.Administrativo
                                    Return My.Resources.RecursosES_CO.Administrativo
                                Case Eresources.Asistencial
                                    Return My.Resources.RecursosES_CO.Asistencial
                                'Campos de contrato
                                Case Eresources.Id
                                    Return My.Resources.RecursosES_CO.Id
                                Case Eresources.RowType
                                    Return My.Resources.RecursosES_CO.RowType
                                Case Eresources.InitialContractNumber
                                    Return My.Resources.RecursosES_CO.InitialContractNumber
                                Case Eresources.Position
                                    Return My.Resources.RecursosES_CO.Position
                                Case Eresources.Group
                                    Return My.Resources.RecursosES_CO.Group
                                Case Eresources.FunctionalUnit
                                    Return My.Resources.RecursosES_CO.FunctionalUnit
                                Case Eresources.ContractType
                                    Return My.Resources.RecursosES_CO.ContractType
                                Case Eresources.JobBondingType
                                    Return My.Resources.RecursosES_CO.JobBondingType
                                Case Eresources.JobBondingDate
                                    Return My.Resources.RecursosES_CO.JobBondingDate
                                Case Eresources.ContractInitialDate
                                    Return My.Resources.RecursosES_CO.ContractInitialDate
                                Case Eresources.ContractEndingDate
                                    Return My.Resources.RecursosES_CO.ContractEndingDate
                                Case Eresources.BasicSalary
                                    Return My.Resources.RecursosES_CO.BasicSalary
                                Case Eresources.Status
                                    Return My.Resources.RecursosES_CO.Status
                                Case Eresources.RetirementReason
                                    Return My.Resources.RecursosES_CO.RetirementReason
                                Case Eresources.RetirementDate
                                    Return My.Resources.RecursosES_CO.RetirementDate
                                Case Eresources.PaymentPeriod
                                    Return My.Resources.RecursosES_CO.PaymentPeriod
                                Case Eresources.SalaryType
                                    Return My.Resources.RecursosES_CO.SalaryType
                                Case Eresources.PaymentType
                                    Return My.Resources.RecursosES_CO.PaymentType
                                Case Eresources.TrialPeriod
                                    Return My.Resources.RecursosES_CO.TrialPeriod
                                Case Eresources.TrialPeriodTime
                                    Return My.Resources.RecursosES_CO.TrialPeriodTime
                                Case Eresources.TrialPeriodSalaryPercentage
                                    Return My.Resources.RecursosES_CO.TrialPeriodSalaryPercentage
                                Case Eresources.AutoRenew
                                    Return My.Resources.RecursosES_CO.AutoRenew
                                Case Eresources.ContractCreationDate
                                    Return My.Resources.RecursosES_CO.ContractCreationDate
                                Case Eresources.Valid
                                    Return My.Resources.RecursosES_CO.Valid
                                Case Eresources.Notes
                                    Return My.Resources.RecursosES_CO.Notes
                                Case Eresources.Bank
                                    Return My.Resources.RecursosES_CO.Bank
                                Case Eresources.BankAccountNumber
                                    Return My.Resources.RecursosES_CO.BankAccountNumber
                                Case Eresources.BankAccountType
                                    Return My.Resources.RecursosES_CO.BankAccountType
                                'Tipos de registro
                                Case Eresources.ContratoBase
                                    Return My.Resources.RecursosES_CO.ContratoBase
                                Case Eresources.Novedad
                                    Return My.Resources.RecursosES_CO.Novedad
                                'Duracion del contrato
                                Case Eresources.Indefinido
                                    Return My.Resources.RecursosES_CO.Indefinido
                                Case Eresources.Ano
                                    Return My.Resources.RecursosES_CO.Ano
                                Case Eresources.Mes
                                    Return My.Resources.RecursosES_CO.Mes
                                Case Eresources.Dia
                                    Return My.Resources.RecursosES_CO.Dia
                                'General
                                Case Eresources.Mas
                                    Return My.Resources.RecursosES_CO.Mas
                                'Clase de contrato
                                Case Eresources.ClaseContratoOtros
                                    Return My.Resources.RecursosES_CO.ClaseContratoOtros
                                Case Eresources.ClaseContratoAprendizaje
                                    Return My.Resources.RecursosES_CO.ClaseContratoAprendizaje
                                Case Eresources.ClaseContratoAprendizajePractica
                                    Return My.Resources.RecursosES_CO.ClaseContratoAprendizajePractica
                                Case Eresources.ClaseContratoLaboralFijo
                                    Return My.Resources.RecursosES_CO.ClaseContratoLaboralFijo
                                Case Eresources.ClaseContratoLaboralIndefinido
                                    Return My.Resources.RecursosES_CO.ClaseContratoLaboralIndefinido
                                Case Eresources.ClaseContratoPracticasoPasantiasUniversitarias
                                    Return My.Resources.RecursosES_CO.ClaseContratoPracticasoPasantiasUniversitarias
                                'Tipos de fondos
                                Case Eresources.Salud
                                    Return My.Resources.RecursosES_CO.Salud
                                Case Eresources.Pension
                                    Return My.Resources.RecursosES_CO.Pension
                                Case Eresources.Riesgos
                                    Return My.Resources.RecursosES_CO.Riesgos
                                Case Eresources.Cesantias
                                    Return My.Resources.RecursosES_CO.Cesantias
                                Case Eresources.CajaCompensacion
                                    Return My.Resources.RecursosES_CO.CajaCompensacion
                                '------------ Varios
                                Case Eresources.ContratosNo2Vigentes
                                    Return My.Resources.RecursosES_CO.ContratosNo2Vigentes
                                Case Eresources.ContratosEnRangoOtroContrato
                                    Return My.Resources.RecursosES_CO.ContratosEnRangoOtroContrato
                                Case Eresources.FondoArlObligatorio
                                    Return My.Resources.RecursosES_CO.FondoArlObligatorio
                                Case Eresources.FondoSaludObligatorio
                                    Return My.Resources.RecursosES_CO.FondoSaludObligatorio
                                Case Eresources.FondoPensionesObligatorio
                                    Return My.Resources.RecursosES_CO.FondoPensionesObligatorio
                                Case Eresources.ContratosFechaMinimaPermitida
                                    Return My.Resources.RecursosES_CO.ContratosFechaMinimaPermitida
                                Case Eresources.ContratosFechaMaximaContratoFijo
                                    Return My.Resources.RecursosES_CO.ContratosFechaMaximaContratoFijo
                                Case Eresources.ContratoRangoDeFechas
                                    Return My.Resources.RecursosES_CO.ContratoRangoDeFechas
                                Case Eresources.FondoYaSeEncuentra
                                    Return My.Resources.RecursosES_CO.FondoYaSeEncuentra
                                Case Eresources.ContratosSalarioVsCargo
                                    Return My.Resources.RecursosES_CO.ContratosSalarioVsCargo

                                Case Eresources.ContratosRenovacion
                                    Return My.Resources.RecursosES_CO.ContratosRenovacion
                                Case Eresources.FondoFechasEntreAfiliaciones
                                    Return My.Resources.RecursosES_CO.FondoFechasEntreAfiliaciones
                                Case Eresources.FondoFechasEnRangoFondo
                                    Return My.Resources.RecursosES_CO.FondoFechasEnRangoFondo
                                Case Eresources.ContratosSinCambio
                                    Return My.Resources.RecursosES_CO.ContratosSinCambio
                                Case Eresources.FondoFechaMinimaContrato
                                    Return My.Resources.RecursosES_CO.FondoFechaMinimaContrato
                                Case Eresources.FondoAccionesInactivos
                                    Return My.Resources.RecursosES_CO.FondoAccionesInactivos
                                Case Eresources.ContratosSalarioVsAnteriorSalario
                                    Return My.Resources.RecursosES_CO.ContratosSalarioVsAnteriorSalario

                                Case Eresources.ContratosCrearNuevo
                                    Return My.Resources.RecursosES_CO.ContratosCrearNuevo
                                Case Eresources.ContratosConCuadroTurnos
                                    Return My.Resources.RecursosES_CO.ContratosConCuadroTurnos
                                Case Eresources.ContratosEliminarAdvertencia
                                    Return My.Resources.RecursosES_CO.ContratosEliminarAdvertencia
                                Case Eresources.ContratosConNominaPagada
                                    Return My.Resources.RecursosES_CO.ContratosConNominaPagada
                            End Select
                        Case Eform.Torres
                            Select Case recurso
                                Case Eresources.SeleccioneTorre
                                    Return My.Resources.RecursosES_CO.SeleccioneUnaTorre
                                Case Else
                                    Return "Agregue este valor a la pila de recursos"
                            End Select
                        Case Eform.Pisos
                            Select Case recurso
                                Case Eresources.SeleccionePiso
                                    Return My.Resources.RecursosES_CO.SeleccionePiso
                                Case Else
                                    Return "Agregue este valor a la pila de recursos"
                            End Select
                        Case Eform.Areas
                            Select Case recurso
                                Case Eresources.SeleccioneArea
                                    Return My.Resources.RecursosES_CO.SeleccioneArea
                                Case Else
                                    Return "Agregue este valor a la pila de recursos"
                            End Select
                        Case Eform.Habitaciones
                            Select Case recurso
                                Case Eresources.SeleccioneHabitaciones
                                    Return My.Resources.RecursosES_CO.SeleccioneHabitacion
                                Case Else
                                    Return "Agregue este valor a la pila de recursos"
                            End Select
                        Case Eform.Responsables
                            Select Case recurso
                                Case Eresources.SeleccioneResponsable
                                    Return My.Resources.RecursosES_CO.SeleccioneResponsable
                                Case Else
                                    Return "Agregue este valor a la pila de recursos"
                            End Select
                        Case Eform.Part
                            Select Case recurso
                                Case Eresources.SeleccioneParte
                                    Return My.Resources.RecursosES_CO.SeleccioneParte
                                Case Else
                                    Return "Agregue este valor a la pila de recursos"
                            End Select
                        Case Eform.Equipment
                            Select Case recurso
                                Case Eresources.SeleccioneEquipo
                                    Return My.Resources.RecursosES_CO.SeleccioneEquipo
                                Case Else
                                    Return "Agregue este valor a la pila de recursos"
                            End Select
                        Case Eform.UnitMeasure
                            Select Case recurso
                                Case Eresources.SeleccioneUnidadMedida
                                    Return My.Resources.RecursosES_CO.SeleccioneUnidadMedida
                                Case Else
                                    Return "Agregue este valor a la pila de recursos"
                            End Select
                        Case Eform.RegistroTecnico
                            Select Case recurso
                                Case Eresources.SeleccioneRegistroTecnico
                                    Return My.Resources.RecursosES_CO.SeleccioneRegistroTecnico
                                Case Else
                                    Return "Agregue este valor a la pila de recursos"
                            End Select
                        '/******** Sistem Documental **********/
                        'Frontal de MetaData
                        Case Eform.MetaData
                            Select Case recurso
                                Case Eresources.ErrorGuardarDocumento
                                    Return My.Resources.RecursosES_CO.ErrorGuardarDocumento
                                Case Eresources.BotonGuardarDocumento
                                    Return My.Resources.RecursosES_CO.BotonGuardarDocumento
                                Case Eresources.RadioGrupoDocumento
                                    Return My.Resources.RecursosES_CO.RadioGrupoDocumento
                                Case Eresources.TituloMetada
                                    Return My.Resources.RecursosES_CO.TituloMetada
                                Case Eresources.NoExisteArchivador
                                    Return My.Resources.RecursosES_CO.NoExisteArchivador
                            End Select
                        'Frontal de Digitalización
                        Case Eform.Digitalizacion
                            Select Case recurso
                                Case Eresources.TituloDigitalizacion
                                    Return My.Resources.RecursosES_CO.TituloDigitalizacion
                                Case Eresources.DeDigitalización
                                    Return My.Resources.RecursosES_CO.DeDigitalización
                                Case Eresources.PaginaDigitalizacion
                                    Return My.Resources.RecursosES_CO.PaginaDigitalizacion
                                Case Eresources.SeleccioneDispositivo
                                    Return My.Resources.RecursosES_CO.SeleccioneDispositivo
                                Case Eresources.EscaneoDuplex
                                    Return My.Resources.RecursosES_CO.EscaneoDuplex
                            End Select

                        'Frontal de adjuntar
                        Case Eform.Adjuntar
                            Select Case recurso
                                Case Eresources.TituloAdjuntar
                                    Return My.Resources.RecursosES_CO.TituloAdjuntar
                            End Select

                        'Control de usuario Barra botones
                        Case Eform.CtrBarraBotones
                            Select Case recurso
                                Case Eresources.SinArchivador
                                    Return My.Resources.RecursosES_CO.SinArchivador
                                Case Eresources.SinDocumentos
                                    Return My.Resources.RecursosES_CO.SinDocumentos
                                Case Eresources.SinPermisoAbrirDocumentos
                                    Return My.Resources.RecursosES_CO.SinPermisoAbrirDocumentos
                            End Select

                        'Frontal de Traslado Cobro Jurídico
                        Case Eform.TrasladoCobroJurídico
                            Select Case recurso
                                Case Eresources.NoExisteTrasladoCobroJuridico
                                    Return My.Resources.RecursosES_CO.NoExisteTrasladoCobroJuridico
                                Case Eresources.FacturaYaExisteTrasladoCobroJuridico
                                    Return My.Resources.RecursosES_CO.FacturaYaExisteTrasladoCobroJuridico
                            End Select
                        'Se registran la información de metadata correspondiente a cada frontal
                        Case Eform.InfoMetaData
                            Select Case recurso
                                Case Eresources.FrmEvaluationMetaData
                                    Return My.Resources.RecursosES_CO.FrmEvaluationMetaData
                                Case Eresources.FrmEvaluationMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmEvaluationMetaDataTitle
                                Case Eresources.FrmCoordinationMetaData
                                    Return My.Resources.RecursosES_CO.FrmCoordinationMetaData
                                Case Eresources.FrmCoordinationMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmCoordinationMetaDataTitle
                                Case Eresources.FrmDevolutionMetaData
                                    Return My.Resources.RecursosES_CO.FrmDevolutionMetaData
                                Case Eresources.FrmDevolutionMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmDevolutionMetaDataTitle
                                Case Eresources.FrmDevolutionMetaDataDetail
                                    Return My.Resources.RecursosES_CO.FrmDevolutionMetaDataDetail
                                Case Eresources.FrmJuridicalDebtMetaData
                                    Return My.Resources.RecursosES_CO.FrmJuridicalDebtMetaData
                                Case Eresources.FrmJuridicalDebtMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmJuridicalDebtMetaDataTitle
                                Case Eresources.FrmJuridicalDebtMetaDataDetail
                                    Return My.Resources.RecursosES_CO.FrmJuridicalDebtMetaDataDetail
                                Case Eresources.FrmTimeParametersMetaData
                                    Return My.Resources.RecursosES_CO.FrmTimeParametersMetaData
                                Case Eresources.FrmTimeParametersMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmTimeParametersMetaDataTitle
                                Case Eresources.FrmCustomersMetaData
                                    Return My.Resources.RecursosES_CO.FrmCustomersMetaData
                                Case Eresources.FrmCustomersMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmCustomersMetaDataTitle
                                Case Eresources.DocumentsMetaDataTitle
                                    Return My.Resources.RecursosES_CO.DocumentsMetaDataTitle
                                Case Eresources.FrmResponsibleMetaData
                                    Return My.Resources.RecursosES_CO.FrmResponsibleMetaData

                                Case Eresources.FrmResponsibleMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmResponsibleMetaDataTitle
                                Case Eresources.FrmCompanyMetaData
                                    Return My.Resources.RecursosES_CO.FrmCompanyMetaData

                                Case Eresources.FrmCompanyMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmCompanyMetaDataTitle
                                Case Eresources.FrmJustificationTemplateMetaData

                                    Return My.Resources.RecursosES_CO.FrmJustificationTemplateMetaData
                                Case Eresources.FrmJustificationTemplateMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmJustificationTemplateMetaDataTitle
                                Case Eresources.FrmHealthCenterMetaData
                                    Return My.Resources.RecursosES_CO.FrmHealthCenterMetaData
                                Case Eresources.FrmHealthCenterMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmHealthCenterMetaDataTitle
                                Case Eresources.FrmBankMetaData
                                    Return My.Resources.RecursosES_CO.FrmBankMetaData
                                Case Eresources.FrmBankMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmBankMetaDataTitle
                                Case Eresources.FrmCityMetaData
                                    Return My.Resources.RecursosES_CO.FrmCityMetaData
                                Case Eresources.FrmCityMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmCityMetaDataTitle
                                Case Eresources.FrmStudyCenterMetaData
                                    Return My.Resources.RecursosES_CO.FrmStudyCenterMetaData
                                Case Eresources.FrmStudyCenterMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmStudyCenterMetaDataTitle
                                Case Eresources.FrmWorkCenterMetaData
                                    Return My.Resources.RecursosES_CO.FrmWorkCenterMetaData
                                Case Eresources.FrmWorkCenterMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmWorkCenterMetaDataTitle
                                Case Eresources.FrmStudyTypeMetaData
                                    Return My.Resources.RecursosES_CO.FrmStudyTypeMetaData
                                Case Eresources.FrmStudyTypeMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmStudyTypeMetaDataTitle
                                Case Eresources.FrmRetirementReasonMetaData
                                    Return My.Resources.RecursosES_CO.FrmRetirementReasonMetaData
                                Case Eresources.FrmRetirementReasonMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmRetirementReasonMetaDataTitle
                                Case Eresources.FrmProfessionMetaData
                                    Return My.Resources.RecursosES_CO.FrmProfessionMetaData
                                Case Eresources.FrmProfessionMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmProfessionMetaDataTitle
                                Case Eresources.FrmProfessionalRiskMetaData
                                    Return My.Resources.RecursosES_CO.FrmProfessionalRiskMetaData
                                Case Eresources.FrmProfessionalRiskMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmProfessionalRiskMetaDataTitle
                                Case Eresources.FrmPositionLevelMetaData
                                    Return My.Resources.RecursosES_CO.FrmPositionLevelMetaData
                                Case Eresources.FrmPositionLevelMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmPositionLevelMetaDataTitle
                                Case Eresources.FrmPositionMetaData
                                    Return My.Resources.RecursosES_CO.FrmPositionMetaData
                                Case Eresources.FrmPositionMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmPositionMetaDataTitle
                                Case Eresources.FrmPensionaryTypeMetaData
                                    Return My.Resources.RecursosES_CO.FrmPensionaryTypeMetaData
                                Case Eresources.FrmPensionaryTypeMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmPensionaryTypeMetaDataTitle
                                Case Eresources.FrmLanguageMetaData
                                    Return My.Resources.RecursosES_CO.FrmLanguageMetaData
                                Case Eresources.FrmLanguageMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmLanguageMetaDataTitle
                                Case Eresources.FrmKinshipMetaData
                                    Return My.Resources.RecursosES_CO.FrmKinshipMetaData
                                Case Eresources.FrmKinshipMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmKinshipMetaDataTitle
                                Case Eresources.FrmDepartmentMetaData
                                    Return My.Resources.RecursosES_CO.FrmDepartmentMetaData
                                Case Eresources.FrmDepartmentMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmDepartmentMetaDataTitle
                                Case Eresources.FrmCountryMetaData
                                    Return My.Resources.RecursosES_CO.FrmCountryMetaData
                                Case Eresources.FrmCountryMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmCountryMetaDataTitle
                                Case Eresources.FrmDisabilityMetaData
                                    Return My.Resources.RecursosES_CO.FrmDisabilityMetaData
                                Case Eresources.FrmDisabilityMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmDisabilityMetaDataTitle
                                Case Eresources.FrmThirdPartyMetaData
                                    Return My.Resources.RecursosES_CO.FrmThirdPartyMetaData
                                Case Eresources.FrmThirdPartyMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmThirdPartyMetaDataTitle
                                Case Eresources.FrmJobBondingTypeMetaData
                                    Return My.Resources.RecursosES_CO.FrmJobBondingTypeMetaData
                                Case Eresources.FrmJobBondingTypeMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmJobBondingTypeMetaDataTitle
                                Case Eresources.FrmFundMetaData
                                    Return My.Resources.RecursosES_CO.FrmFundMetaData
                                Case Eresources.FrmFundMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmFundMetaDataTitle
                                Case Eresources.FrmFunctionalUnitMetaData
                                    Return My.Resources.RecursosES_CO.FrmFunctionalUnitMetaData
                                Case Eresources.FrmFunctionalUnitMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmFunctionalUnitMetaDataTitle
                                Case Eresources.FrmGroupMetaData
                                    Return My.Resources.RecursosES_CO.FrmGroupMetaData
                                Case Eresources.FrmGroupMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmGroupMetaDataTitle
                                Case Eresources.FrmCompanyPayrollMetaData
                                    Return My.Resources.RecursosES_CO.FrmCompanyPayrollMetaData
                                Case Eresources.FrmCompanyPayrollMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmCompanyPayrollMetaDataTitle
                                Case Eresources.FrmConceptsMetaData
                                    Return My.Resources.RecursosES_CO.FrmConceptsMetaData
                                Case Eresources.FrmConceptsMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmConceptsMetaDataTitle
                                Case Eresources.FrmTimeUnitMetaData
                                    Return My.Resources.RecursosES_CO.FrmTimeUnitMetaData
                                Case Eresources.FrmTimeUnitMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmTimeUnitMetaDataTitle
                                Case Eresources.FrmContractGroupMetaData
                                    Return My.Resources.RecursosES_CO.FrmContractGroupMetaData
                                Case Eresources.FrmContractGroupMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmContractGroupMetaDataTitle
                                Case Eresources.FrmContractModificationReasonMetaData
                                    Return My.Resources.RecursosES_CO.FrmContractModificationReasonMetaData
                                Case Eresources.FrmContractModificationReasonMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmContractModificationReasonMetaDataTitle
                                Case Eresources.FrmContractTemplateMetaData
                                    Return My.Resources.RecursosES_CO.FrmContractTemplateMetaData
                                Case Eresources.FrmContractTemplateMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmContractTemplateMetaDataTitle
                                Case Eresources.FrmContractTypeMetaData
                                    Return My.Resources.RecursosES_CO.FrmContractTypeMetaData
                                Case Eresources.FrmContractTypeMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmContractTypeMetaDataTitle
                                Case Eresources.FrmContributorTypeMetaData
                                    Return My.Resources.RecursosES_CO.FrmContributorTypeMetaData
                                Case Eresources.FrmContributorTypeMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmContributorTypeMetaDataTitle
                                Case Eresources.FrmCostCenterMetaData
                                    Return My.Resources.RecursosES_CO.FrmCostCenterMetaData
                                Case Eresources.FrmCostCenterMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmCostCenterMetaDataTitle
                                Case Eresources.FrmEducationLevelsMetaData
                                    Return My.Resources.RecursosES_CO.FrmEducationLevelsMetaData
                                Case Eresources.FrmEducationLevelsMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmEducationLevelsMetaDataTitle
                                Case Eresources.FrmEmployeeTypeMetaData
                                    Return My.Resources.RecursosES_CO.FrmEmployeeTypeMetaData
                                Case Eresources.FrmEmployeeTypeMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmEmployeeTypeMetaDataTitle
                                Case Eresources.FrmAuthorizationConceptMetaData
                                    Return My.Resources.RecursosES_CO.FrmAuthorizationConceptMetaData
                                Case Eresources.FrmAuthorizationConceptMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmAuthorizationConceptMetaDataTitle
                                Case Eresources.FrmNoveltyMetaData
                                    Return My.Resources.RecursosES_CO.FrmNoveltyMetaData
                                Case Eresources.FrmNoveltyMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmNoveltyMetaDataTitle
                                Case Eresources.FrmPhoneTypeMetaData
                                    Return My.Resources.RecursosES_CO.FrmPhoneTypeMetaData
                                Case Eresources.FrmPhoneTypeMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmPhoneTypeMetaDataTitle
                                Case Eresources.FrmScheduleTemplateMetaData
                                    Return My.Resources.RecursosES_CO.FrmScheduleTemplateMetaData
                                Case Eresources.FrmScheduleTemplateMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmScheduleTemplateMetaDataTitle
                                Case Eresources.FrmKindsAgrementsMetaData
                                    Return My.Resources.RecursosES_CO.FrmKindsAgrementsMetaData
                                Case Eresources.FrmKindsAgrementsMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmKindsAgrementsMetaDataTitle
                                Case Eresources.FrmEmployeeMetaData
                                    Return My.Resources.RecursosES_CO.FrmEmployeeMetaData
                                Case Eresources.FrmEmployeeMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmEmployeeMetaDataTitle
                                Case Eresources.FrmFunctionalAgreementsMetaData
                                    Return My.Resources.RecursosES_CO.FrmFunctionalAgreementsMetaData
                                Case Eresources.FrmFunctionalAgreementsMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmFunctionalAgreementsMetaDataTitle
                                Case Eresources.FrmManualConceptsMetaData
                                    Return My.Resources.RecursosES_CO.FrmManualConceptsMetaData
                                Case Eresources.FrmManualConceptsMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmManualConceptsMetaDataTitle
                                Case Eresources.FrmFinancialSourceMetaData
                                    Return My.Resources.RecursosES_CO.FrmFinancialSourceMetaData
                                Case Eresources.FrmFinancialSourceMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmFinancialSourceMetaDataTitle
                                Case Eresources.FrmBudgetEntitiesMetaData
                                    Return My.Resources.RecursosES_CO.FrmBudgetEntitiesMetaData
                                Case Eresources.FrmBudgetEntitiesMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmBudgetEntitiesMetaDataTitle
                                Case Eresources.FrmEarningsTypeMetaData
                                    Return My.Resources.RecursosES_CO.FrmEarningsTypeMetaData
                                Case Eresources.FrmEarningsTypeMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmEarningsTypeMetaDataTitle
                                Case Eresources.FrmExpenseTypeMetaData
                                    Return My.Resources.RecursosES_CO.FrmExpenseTypeMetaData
                                Case Eresources.FrmExpenseTypeMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmExpenseTypeMetaDataTitle
                                Case Eresources.FrmBudgetDependencyMetaData
                                    Return My.Resources.RecursosES_CO.FrmBudgetDependencyMetaData
                                Case Eresources.FrmBudgetDependencyMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmBudgetDependencyMetaDataTitle
                                Case Eresources.FrmBudgetConceptMetaData
                                    Return My.Resources.RecursosES_CO.FrmBudgetConceptMetaData
                                Case Eresources.FrmBudgetConceptMetaDataTitle
                                    Return My.Resources.RecursosES_CO.FrmBudgetConceptMetaDataTitle

                            End Select
                        Case Eform.AumentoSalario
                            Select Case recurso
                                Case Eresources.PorcentajeCero
                                    Return My.Resources.RecursosES_CO.PorcentajeCero
                                Case Eresources.FechaRetroactivo
                                    Return My.Resources.RecursosES_CO.FechaRetroactivo
                                Case Eresources.ModificacionSueldosCorrecta
                                    Return My.Resources.RecursosES_CO.ModificacionSueldosCorrecta
                            End Select

                    End Select
            End Select

            Return String.Empty
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function

    ''' <summary>
    ''' Funcion Compartida que se utiliza para obtener las excepciones de
    ''' unos archivos de recurso creados, por el idioma que este disponible.
    ''' </summary>
    ''' 
    Public Shared Function obtenerExcepcion(ByVal recurso As EexceptionsResources) As String

        Dim Indigo As SessionValues = SessionValues.Instance

        'Dim configuracionXml As New DataTable("ConfigIdioma")
        'configuracionXml.Columns.Add("Idioma")
        'configuracionXml.ReadXml(String.Concat(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "\", "Indigo Technologies", "\", "Indigo Crystal", "\XML\ConfigIdioma.xml"))
        Dim culturaInstalada As String = Indigo.Culture.Name


        Select Case culturaInstalada
            Case "en-US"

                Select Case recurso

                    Case EexceptionsResources.MensajeConstructorPresentador
                        Return My.Resources.RecursoExcepcionesEN_US.MensajeConstructorPresentador
                    Case EexceptionsResources.ContrasenaNosePudoCambiarContrasena
                        Return My.Resources.RecursoExcepcionesEN_US.ContrasenaNosePudoCambiarContrasena
                    Case EexceptionsResources.LoginUsuarioSinDatos
                        Return My.Resources.RecursoExcepcionesEN_US.LoginUsuarioSinDatos
                    Case EexceptionsResources.BusquedaValorNoEncontrado
                        Return My.Resources.RecursoExcepcionesEN_US.BusquedaValorNoEncontrado
                    Case EexceptionsResources.BusquedaValorNoEncontradoComplemento
                        Return My.Resources.RecursoExcepcionesEN_US.BusquedaValorNoEncontradoComplemento
                    Case EexceptionsResources.ArchivoNoExiste
                        Return My.Resources.RecursoExcepcionesEN_US.ArchivoNoExiste
                    Case Else
                        Return "Add this value to the exception resource stack."

                End Select

            Case "es-CO"

                Select Case recurso

                    Case EexceptionsResources.MensajeConstructorPresentador
                        Return My.Resources.RecursosExcepcionesES_CO.MensajeConstructorPresentador
                    Case EexceptionsResources.ContrasenaNosePudoCambiarContrasena
                        Return My.Resources.RecursosExcepcionesES_CO.ContrasenaNosePudoCambiarContrasena
                    Case EexceptionsResources.LoginUsuarioSinDatos
                        Return My.Resources.RecursosExcepcionesES_CO.LoginUsuarioSinDatos
                    Case EexceptionsResources.BusquedaValorNoEncontrado
                        Return My.Resources.RecursosExcepcionesES_CO.BusquedaValorNoEncontrado
                    Case EexceptionsResources.BusquedaValorNoEncontradoComplemento
                        Return My.Resources.RecursosExcepcionesES_CO.BusquedaValorNoEncontradoComplemento
                    Case EexceptionsResources.ArchivoNoExiste
                        Return My.Resources.RecursosExcepcionesES_CO.ArchivoNoExiste
                    Case Else
                        Return "Agrege Este Valor a la Pila de Recursos"

                End Select
        End Select
        Return Nothing
    End Function

    ''' <summary>
    ''' Funcion para determinar si hay acceso a internet.
    ''' </summary>
    ''' <returns></returns>
    Shared Function AccessInternetAvaliable() As Boolean
        If My.Computer.Network.IsAvailable() Then
            Try
                If My.Computer.Network.Ping("www.google.com.co", 1000) Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As PingException
                Return False
            End Try
        Else
            Return False
        End If
    End Function

    ' ''' <summary>
    ' ''' metodo para cargar los formularios visibles del usuario logueado
    ' ''' </summary>
    ' ''' <param name="UserCode">Código Usuario</param>
    ' ''' <param name="UserRol">Código Rol</param>
    ' ''' <returns>FormsActive Dictionary</returns>
    'Public Shared Async Sub GetActiveForms(UserCode As String, UserRol As String)
    '    Dim permissions As List(Of PermissionsFormsActive)
    '    permissions = Await MBaseClass.GetActiveForms(UserRol, UserCode)
    '    Dim forms As List(Of VieForm) = BaseClass.GetXmlWithAggregates(Of VieForm)(Base.eDataXml.XMLForms)
    '    Dim modulesList As List(Of VieModule) = BaseClass.GetXmlWithAggregates(Of VieModule)(eDataXml.XMLModules)
    '    Dim permissionsGroup = (From a In permissions
    '                  Group a By key = a.FormTag Into Group
    '                  Select Group.FirstOrDefault).ToList

    '    Parallel.ForEach(Of PermissionsFormsActive)(permissionsGroup, Sub(item)
    '                                                                      Dim objAux = (From c In forms Where c.Id = item.FormTag Select c).FirstOrDefault
    '                                                                      If objAux IsNot Nothing Then
    '                                                                          item.NameForm = objAux.Name
    '                                                                          item.NameGroup = modulesList.Where(Function(m) m.Id = objAux.Module.Id).First().Group.Name
    '                                                                          item.NameModule = objAux.Module.Name
    '                                                                          item.PrintRunSave = False
    '                                                                          item.PrintRunConfirm = False

    '                                                                      End If
    '                                                                  End Sub)

    '    SessionValues.Instance.ActiveForms = permissionsGroup.ToDictionary(Function(x) x.FormTag, Function(y) New Tuple(Of String, String, String, List(Of String), Boolean, Boolean)(y.NameForm, y.NameModule, y.NameGroup, y.ListActions, y.PrintRunSave, y.PrintRunConfirm))
    'End Sub

#Region "Xmls"

    'Private Shared _taskXmlForms As Task(Of List(Of VieForm))
    Public Shared _listXmlForms As List(Of VieForm)

    'Private Shared _taskXmlFormsModuleUser As Task(Of List(Of VieForm))
    'Private Shared _listXmlFormsModuleUser As List(Of VieForm)

    'Private Shared _taskXmlModules As Task(Of List(Of VieModule))
    'Private Shared _listXmlModules As List(Of VieModule)

    'Private Shared _taskXmlPermissions As Task(Of List(Of ViePermission))
    'Private Shared _listXmlPermissions As List(Of ViePermission)

    'Private Shared _taskXmlGroups As Task(Of List(Of VieGroup))
    'Private Shared _listXmlGroups As List(Of VieGroup)

    'Private Shared _taskXmlFormsModule As Task(Of List(Of VieFormModule))
    'Private Shared _listXmlFormsModule As List(Of VieFormModule)

    'Private Shared _taskXmlPermissionsForm As Task(Of List(Of ViePermissionForm))
    'Private Shared _listXmlPermissionsForm As List(Of ViePermissionForm)

    'Private Shared _taskXmlModulesGroup As Task(Of List(Of VieModuleGroup))
    'Private Shared _listXmlModulesGroup As List(Of VieModuleGroup)

    'Private Shared _taskXmlReports As Task(Of List(Of VieReport))
    Private Shared _listXmlReports As List(Of VieReport)

    'Private Shared _taskXmlReportsForm As Task(Of List(Of VieReportForm))
    'Private Shared _listXmlReportsForm As List(Of VieReportForm)

    Private Shared _listXmlWoeidCities As List(Of VieWoeidCities)

    'Private Shared _listXmlMonths As List(Of VieMonths)

    Private Shared _listXmlTitles As List(Of VieTitle)

    Public Shared _listXmlFormsNuevo As List(Of VieForm)

    Private Shared _listXmlModulesNuevo As List(Of VieModule)

    Private Shared _listXmlPermissionsFormNuevo As List(Of ViePermissionForm)

    Private Shared _listXmlGroupsNuevo As List(Of VieGroup)


    ''' <summary>
    ''' Obtiene una lista de objetos IVieXml tipo T
    ''' </summary>
    ''' <typeparam name="T">Objetos IVieXml</typeparam>
    ''' <param name="Xml">Tipo de archivo Xml a usar</param>
    ''' <returns>Lista de objetos tipo T</returns>
    Public Shared Function GetXmlWithAggregates(Of T As IVieXml)(ByVal Xml As eDataXml) As List(Of T)
        Dim oAux As Object = Nothing
        Select Case Xml
            Case eDataXml.XMLForms
                'If _listXmlForms Is Nothing Then
                '    _listXmlForms = Await _taskXmlForms
                'End If
                'If ApplicationSetting.Instance.LoginAzure Then
                oAux = _listXmlFormsNuevo
                'Else
                '    oAux = _listXmlForms
                'End If

            Case eDataXml.XMLModules
                'If _listXmlModules Is Nothing Then
                '    _listXmlModules = Await _taskXmlModules
                'End If
                'If ApplicationSetting.Instance.LoginAzure Then
                oAux = _listXmlModulesNuevo
                'Else
                '    oAux = _listXmlModules
                'End If

            'Case eDataXml.XMLPermissions
            '    'If _listXmlPermissions Is Nothing Then
            '    '    _listXmlPermissions = Await _taskXmlPermissions
            '    'End If
            '    oAux = _listXmlPermissions
            'Case eDataXml.XMLGroups
            '    'If _listXmlGroups Is Nothing Then
            '    '    _listXmlGroups = Await _taskXmlGroups
            '    'End If
            '    oAux = _listXmlGroups
            'Case eDataXml.XMLFormsModule
            '    'If _listXmlFormsModule Is Nothing Then
            '    '    _listXmlFormsModule = Await _taskXmlFormsModule
            '    'End If
            '    oAux = _listXmlFormsModule
            Case eDataXml.XMLPermissionsForm
                'If _listXmlPermissionsForm Is Nothing Then
                '    _listXmlPermissionsForm = Await _taskXmlPermissionsForm
                'End If
                'If ApplicationSetting.Instance.LoginAzure Then
                oAux = _listXmlPermissionsFormNuevo
                'Else
                '    oAux = _listXmlPermissionsForm
                'End If

            'Case eDataXml.XMLModulesGroup
            '    'If _listXmlModulesGroup Is Nothing Then
            '    '    _listXmlModulesGroup = Await _taskXmlModulesGroup
            '    'End If
            '    oAux = _listXmlModulesGroup
            Case eDataXml.XMLWoeidCities
                'If _listXmlWoeidCities Is Nothing Then
                '    _listXmlWoeidCities = DeserializeXml(Of VieWoeidCities)(eDataXml.XMLWoeidCities)
                'End If
                oAux = _listXmlWoeidCities
            'Case eDataXml.XMLMonths
            '    'If _listXmlMonths Is Nothing Then
            '    '    _listXmlMonths = DeserializeXml(Of VieMonths)(eDataXml.XMLMonths)
            '    'End If
            '    oAux = _listXmlMonths
            Case eDataXml.XMLReports
                'If _listXmlReports Is Nothing Then
                '    _listXmlReports = Await _taskXmlReports
                'End If
                oAux = _listXmlReports
            'Case eDataXml.XMLReportsForm
            '    'If _listXmlReportsForm Is Nothing Then
            '    '    _listXmlReportsForm = Await _taskXmlReportsForm
            '    'End If
            '    oAux = _listXmlReportsForm
            'Case eDataXml.XMLFormsModuleUser
            '    oAux = _listXmlFormsModuleUser

            Case eDataXml.XMLTitles
                oAux = _listXmlTitles
        End Select
        Return TryCast(oAux, List(Of T))
    End Function

    ''' <summary>
    ''' Inicia la ejecución de las tareas para la consulta de los archivos Xml
    ''' </summary>
    Public Shared Async Sub RunGetXmlTasks()
        '_listXmlForms = Await Task.Factory.StartNew(Of List(Of VieForm))(Function()
        '                                                                     Dim forms As List(Of VieForm) = DeserializeXml(Of VieForm)(eDataXml.XMLForms)
        '                                                                     Dim modules As List(Of VieModule) = DeserializeXml(Of VieModule)(eDataXml.XMLModules)
        '                                                                     Dim permissions As List(Of ViePermission) = DeserializeXml(Of ViePermission)(eDataXml.XMLPermissions)
        '                                                                     Dim formsModule As List(Of VieFormModule) = DeserializeXml(Of VieFormModule)(eDataXml.XMLFormsModule)
        '                                                                     Dim permissionsForm As List(Of ViePermissionForm) = DeserializeXml(Of ViePermissionForm)(eDataXml.XMLPermissionsForm)
        '                                                                     For Each f In forms
        '                                                                         Dim res = (From m In modules
        '                                                                                    Join fm In formsModule On m.Id Equals fm.IdModule
        '                                                                                    Where fm.IdModule = f.IdModuleSource AndAlso fm.IdForm = f.Id Select m).ToList()
        '                                                                         f.Module = modules.Where(Function(m) m.Id = f.IdModuleSource).FirstOrDefault()
        '                                                                         f.Modules = (From m In modules
        '                                                                                      Join fm In formsModule On m.Id Equals fm.IdModule
        '                                                                                      Where fm.IdForm = f.Id Select m).ToList()
        '                                                                         f.Permissions = (From p In permissions
        '                                                                                          Join pf In permissionsForm On p.Id Equals pf.IdPermission
        '                                                                                          Where pf.IdForm = f.Id Select p).ToList()
        '                                                                     Next

        '                                                                     Return forms
        '                                                                 End Function)

        '_listXmlFormsModuleUser = Await Task.Factory.StartNew(Of List(Of VieForm))(Function()
        '                                                                               Dim forms As List(Of VieForm) = DeserializeXml(Of VieForm)(eDataXml.XMLForms)
        '                                                                               Dim modules As List(Of VieModule) = DeserializeXml(Of VieModule)(eDataXml.XMLModules)
        '                                                                               Dim permissions As List(Of ViePermission) = DeserializeXml(Of ViePermission)(eDataXml.XMLPermissions)
        '                                                                               Dim formsModule As List(Of VieFormModule) = DeserializeXml(Of VieFormModule)(eDataXml.XMLFormsModule)
        '                                                                               Dim permissionsForm As List(Of ViePermissionForm) = DeserializeXml(Of ViePermissionForm)(eDataXml.XMLPermissionsForm)
        '                                                                               Dim formAux As VieForm = Nothing
        '                                                                               For Each f In forms
        '                                                                                   Dim res = (From m In modules
        '                                                                                              Join fm In formsModule On m.Id Equals fm.IdModule
        '                                                                                              Where fm.IdModule = f.IdModuleSource AndAlso fm.IdForm = f.Id Select m).ToList()
        '                                                                                   f.Module = modules.Where(Function(m) m.Id = f.IdModuleSource).FirstOrDefault()
        '                                                                                   f.Modules = (From m In modules
        '                                                                                                Join fm In formsModule On m.Id Equals fm.IdModule
        '                                                                                                Where fm.IdForm = f.Id Select m).ToList()
        '                                                                                   f.Permissions = (From p In permissions
        '                                                                                                    Join pf In permissionsForm On p.Id Equals pf.IdPermission
        '                                                                                                    Where pf.IdForm = f.Id Select p).ToList()
        '                                                                               Next
        '                                                                               If forms IsNot Nothing AndAlso forms.Any(Function(f) f.Modules IsNot Nothing AndAlso f.Modules.Count > 1) Then

        '                                                                                   For Each fa In forms.Where(Function(f) f.Modules IsNot Nothing AndAlso f.Modules.Count > 1).ToList
        '                                                                                       If fa.Modules.Any(Function(fam) fam.Id <> fa.Module.Id) Then
        '                                                                                           For Each ma In fa.Modules.Where(Function(fam) fam.Id <> fa.Module.Id)
        '                                                                                               formAux = New VieForm
        '                                                                                               With formAux
        '                                                                                                   .AssemblyName = fa.AssemblyName
        '                                                                                                   .ClassName = fa.ClassName
        '                                                                                                   .GroupForms = fa.GroupForms
        '                                                                                                   .HandlesMassiveConfirm = fa.HandlesMassiveConfirm
        '                                                                                                   .HasForm = fa.HasForm
        '                                                                                                   .HasSequence = fa.HasSequence
        '                                                                                                   .Id = fa.Id
        '                                                                                                   .IdModuleSource = ma.Id
        '                                                                                                   .IsNativeForm = fa.IsNativeForm
        '                                                                                                   .Module = (From m In modules Where m.Id = ma.Id).FirstOrDefault
        '                                                                                                   .Modules = New List(Of VieModule)
        '                                                                                                   .Modules.Add(.Module)
        '                                                                                                   .Name = fa.Name
        '                                                                                                   .Permissions = (From p In permissions
        '                                                                                                                   Join pf In permissionsForm On p.Id Equals pf.IdPermission
        '                                                                                                                   Where pf.IdForm = fa.Id Select p).ToList()
        '                                                                                                   .PrintEvents = fa.PrintEvents
        '                                                                                                   .Type = fa.Type
        '                                                                                               End With
        '                                                                                               If Not forms.Any(Function(f) f.Id = fa.Id AndAlso f.IdModuleSource = ma.Id) Then
        '                                                                                                   forms.Add(formAux)
        '                                                                                               End If
        '                                                                                           Next
        '                                                                                       End If
        '                                                                                   Next
        '                                                                               End If

        '                                                                               Return forms
        '                                                                           End Function)

        '_listXmlModules = Await Task.Factory.StartNew(Of List(Of VieModule))(Function()
        '                                                                         Dim modules As List(Of VieModule) = DeserializeXml(Of VieModule)(eDataXml.XMLModules)
        '                                                                         Dim forms As List(Of VieForm) = DeserializeXml(Of VieForm)(eDataXml.XMLForms)
        '                                                                         Dim groups As List(Of VieGroup) = DeserializeXml(Of VieGroup)(eDataXml.XMLGroups)
        '                                                                         Dim modulesGroup As List(Of VieModuleGroup) = DeserializeXml(Of VieModuleGroup)(eDataXml.XMLModulesGroup)
        '                                                                         Dim formsModule As List(Of VieFormModule) = DeserializeXml(Of VieFormModule)(eDataXml.XMLFormsModule)
        '                                                                         For Each m In modules
        '                                                                             Dim res = (From g In groups
        '                                                                                        Join mg In modulesGroup On g.Id Equals mg.IdGroup
        '                                                                                        Where mg.IdModule = m.Id Select g).ToList()
        '                                                                             m.Group = If(res.Count > 0, res(0), New VieGroup())
        '                                                                             m.Forms = (From f In forms
        '                                                                                        Join fm In formsModule On f.Id Equals fm.IdForm
        '                                                                                        Where fm.IdModule = m.Id Select f).ToList()
        '                                                                         Next
        '                                                                         Return modules
        '                                                                     End Function)

        '_listXmlGroups = Await Task.Factory.StartNew(Of List(Of VieGroup))(Function()
        '                                                                       Dim groups As List(Of VieGroup) = DeserializeXml(Of VieGroup)(eDataXml.XMLGroups)
        '                                                                       Dim modules As List(Of VieModule) = DeserializeXml(Of VieModule)(eDataXml.XMLModules)
        '                                                                       Dim modulesGroup As List(Of VieModuleGroup) = DeserializeXml(Of VieModuleGroup)(eDataXml.XMLModulesGroup)
        '                                                                       For Each g In groups
        '                                                                           g.Modules = (From m In modules
        '                                                                                        Join mg In modulesGroup On m.Id Equals mg.IdModule
        '                                                                                        Where mg.IdGroup = g.Id Select m).ToList()
        '                                                                       Next
        '                                                                       Return groups
        '                                                                   End Function)

        '_listXmlPermissions = Await Task.Factory.StartNew(Of List(Of ViePermission))(Function()
        '                                                                                 Dim permissions As List(Of ViePermission) = DeserializeXml(Of ViePermission)(eDataXml.XMLPermissions)
        '                                                                                 Dim forms As List(Of VieForm) = DeserializeXml(Of VieForm)(eDataXml.XMLForms)
        '                                                                                 Dim permissionsForm As List(Of ViePermissionForm) = DeserializeXml(Of ViePermissionForm)(eDataXml.XMLPermissionsForm)
        '                                                                                 For Each p In permissions
        '                                                                                     p.Forms = (From f In forms
        '                                                                                                Join pf In permissionsForm On f.Id Equals pf.IdForm
        '                                                                                                Where pf.IdPermission = p.Id Select f).ToList()
        '                                                                                 Next
        '                                                                                 Return permissions
        '                                                                             End Function)

        '_listXmlFormsModule = Await Task.Factory.StartNew(Of List(Of VieFormModule))(Function()
        '                                                                                 Dim formsModule As List(Of VieFormModule) = DeserializeXml(Of VieFormModule)(eDataXml.XMLFormsModule)
        '                                                                                 Dim modules As List(Of VieModule) = DeserializeXml(Of VieModule)(eDataXml.XMLModules)
        '                                                                                 Dim forms As List(Of VieForm) = DeserializeXml(Of VieForm)(eDataXml.XMLForms)
        '                                                                                 For Each fm In formsModule
        '                                                                                     Dim res = (From m In modules Where m.Id = fm.IdModule Select m).ToList()
        '                                                                                     fm.Module = If(res.Count > 0, res(0), New VieModule())
        '                                                                                     Dim res1 = (From f In forms Where f.Id = fm.IdForm Select f).ToList()
        '                                                                                     fm.Form = If(res1.Count > 0, res1(0), New VieForm())
        '                                                                                 Next
        '                                                                                 Return formsModule
        '                                                                             End Function)

        '_listXmlPermissionsForm = Await Task.Factory.StartNew(Of List(Of ViePermissionForm))(Function()
        '                                                                                         Dim permissionsForm As List(Of ViePermissionForm) = DeserializeXml(Of ViePermissionForm)(eDataXml.XMLPermissionsForm)
        '                                                                                         Dim forms As List(Of VieForm) = DeserializeXml(Of VieForm)(eDataXml.XMLForms)
        '                                                                                         Dim permissions As List(Of ViePermission) = DeserializeXml(Of ViePermission)(eDataXml.XMLPermissions)
        '                                                                                         For Each pf In permissionsForm
        '                                                                                             Dim res = (From f In forms Where f.Id = pf.IdForm Select f).ToList()
        '                                                                                             pf.Form = If(res.Count > 0, res(0), New VieForm())
        '                                                                                             Dim res1 = (From p In permissions Where p.Id = pf.IdPermission Select p).ToList()
        '                                                                                             pf.Permission = If(res1.Count > 0, res1(0), New ViePermission())
        '                                                                                         Next
        '                                                                                         Return permissionsForm
        '                                                                                     End Function)

        '_listXmlModulesGroup = Await Task.Factory.StartNew(Of List(Of VieModuleGroup))(Function()
        '                                                                                   Dim modulesGroup As List(Of VieModuleGroup) = DeserializeXml(Of VieModuleGroup)(eDataXml.XMLModulesGroup)
        '                                                                                   Dim modules As List(Of VieModule) = DeserializeXml(Of VieModule)(eDataXml.XMLModules)
        '                                                                                   Dim groups As List(Of VieGroup) = DeserializeXml(Of VieGroup)(eDataXml.XMLGroups)
        '                                                                                   For Each mg In modulesGroup
        '                                                                                       Dim res = (From g In groups Where g.Id = mg.IdGroup Select g).ToList()
        '                                                                                       mg.Group = If(res.Count > 0, res(0), New VieGroup())
        '                                                                                       Dim res1 = (From m In modules Where m.Id = mg.IdModule Select m).ToList()
        '                                                                                       mg.Module = If(res1.Count > 0, res1(0), New VieModule())
        '                                                                                   Next
        '                                                                                   Return modulesGroup
        '                                                                               End Function)

        '_listXmlReports = Await Task.Factory.StartNew(Of List(Of VieReport))(Function()
        '                                                                         Dim reports As List(Of VieReport) = DeserializeXml(Of VieReport)(eDataXml.XMLReports)
        '                                                                         Dim reportForms As List(Of VieReportForm) = DeserializeXml(Of VieReportForm)(eDataXml.XMLReportsForm)
        '                                                                         Dim forms As List(Of VieForm) = DeserializeXml(Of VieForm)(eDataXml.XMLFormsNuevo)
        '                                                                         For Each r In reports
        '                                                                             r.Forms = reportForms.Where(Function(f) f.IdReport = r.Id).ToList()
        '                                                                             For Each f In r.Forms
        '                                                                                 Dim res = (From frm In forms Where frm.Id = f.IdForm Select frm).ToList()
        '                                                                                 f.Form = If(res.Count > 0, res(0), New VieForm())
        '                                                                             Next
        '                                                                             r.ClassType = Window.Utils.GetReportTypeByClassName(r.ClassName)
        '                                                                         Next
        '                                                                         Return reports
        '                                                                     End Function)
        _listXmlReports = Await Task.Factory.StartNew(Of List(Of VieReport))(Function()
                                                                                 Dim reports As List(Of VieReport) = DeserializeXml(Of VieReport)(eDataXml.XMLReports)
                                                                                 Dim reportForms As List(Of VieReportForm) = DeserializeXml(Of VieReportForm)(eDataXml.XMLReportsForm)
                                                                                 Dim forms As List(Of VieForm) = DeserializeXml(Of VieForm)(eDataXml.XMLFormsNuevo)
                                                                                 For Each r In reports
                                                                                     r.Forms = reportForms.Where(Function(f) f.IdReport = r.Id).ToList()
                                                                                     For Each f In r.Forms
                                                                                         Dim res = (From frm In forms Where frm.Id = f.IdForm Select frm).ToList()
                                                                                         f.Form = If(res.Count > 0, res(0), New VieForm())
                                                                                     Next
                                                                                     r.ClassType = Window.Utils.GetReportTypeByClassName(r.ClassName)
                                                                                 Next
                                                                                 Return reports
                                                                             End Function)

        '_listXmlReportsForm = Await Task.Factory.StartNew(Of List(Of VieReportForm))(Function()
        '                                                                                 Dim reportForms As List(Of VieReportForm) = DeserializeXml(Of VieReportForm)(eDataXml.XMLReportsForm)
        '                                                                                 Dim forms As List(Of VieForm) = DeserializeXml(Of VieForm)(eDataXml.XMLForms)
        '                                                                                 Dim reports As List(Of VieReport) = DeserializeXml(Of VieReport)(eDataXml.XMLReports)
        '                                                                                 For Each rm In reportForms
        '                                                                                     Dim res = (From frm In forms Where frm.Id = rm.IdForm Select frm).ToList()
        '                                                                                     rm.Form = If(res.Count > 0, res(0), New VieForm())
        '                                                                                     Dim res1 = (From r In reports Where r.Id = rm.IdReport Select r).ToList()
        '                                                                                     rm.Report = If(res1.Count > 0, res1(0), New VieReport())
        '                                                                                 Next
        '                                                                                 Return reportForms
        '                                                                             End Function)

        _listXmlWoeidCities = Await Task.Factory.StartNew(Of List(Of VieWoeidCities))(Function()
                                                                                          Return DeserializeXml(Of VieWoeidCities)(eDataXml.XMLWoeidCities)
                                                                                      End Function)

        _listXmlTitles = Await Task.Factory.StartNew(Of List(Of VieTitle))(Function()
                                                                               Return DeserializeXml(Of VieTitle)(eDataXml.XMLTitles)
                                                                           End Function)

        _listXmlFormsNuevo = Await Task.Factory.StartNew(Of List(Of VieForm))(Function()
                                                                                  Dim forms As List(Of VieForm) = DeserializeXml(Of VieForm)(eDataXml.XMLFormsNuevo)
                                                                                  Dim modules As List(Of VieModule) = DeserializeXml(Of VieModule)(eDataXml.XMLModulesNuevo)
                                                                                  Dim permissions As List(Of ViePermission) = DeserializeXml(Of ViePermission)(eDataXml.XMLPermissions)
                                                                                  Dim permissionsForm As List(Of ViePermissionForm) = DeserializeXml(Of ViePermissionForm)(eDataXml.XMLPermissionsForm)
                                                                                  Dim titles As List(Of VieTitle) = DeserializeXml(Of VieTitle)(eDataXml.XMLTitles)
                                                                                  Dim title As VieTitle = Nothing
                                                                                  For Each f In forms
                                                                                      f.Module = modules.Where(Function(m) m.Id = f.IdModuleSource).FirstOrDefault()
                                                                                      f.Permissions = (From p In permissions
                                                                                                       Join pf In permissionsForm On p.Id Equals pf.IdPermission
                                                                                                       Where pf.IdForm = f.Id Select p).ToList()
                                                                                      title = titles.Where(Function(t) t.Id = f.Type).FirstOrDefault()
                                                                                      If title IsNot Nothing Then
                                                                                          f.Order = title.Order
                                                                                      End If
                                                                                  Next
                                                                                  Return forms
                                                                              End Function)
        _listXmlForms = _listXmlFormsNuevo

        _listXmlModulesNuevo = Await Task.Factory.StartNew(Of List(Of VieModule))(Function()
                                                                                      Dim modules As List(Of VieModule) = DeserializeXml(Of VieModule)(eDataXml.XMLModulesNuevo)
                                                                                      Dim forms As List(Of VieForm) = DeserializeXml(Of VieForm)(eDataXml.XMLFormsNuevo)
                                                                                      'Dim groups As List(Of VieGroup) = DeserializeXml(Of VieGroup)(eDataXml.XMLGroupsNuevo)
                                                                                      'Dim modulesGroup As List(Of VieModuleGroup) = DeserializeXml(Of VieModuleGroup)(eDataXml.XMLModulesGroupNuevo)
                                                                                      Dim formsModule As List(Of VieFormModule) = DeserializeXml(Of VieFormModule)(eDataXml.XMLFormsModuleNuevo)
                                                                                      For Each m In modules
                                                                                          'Dim res = (From g In groups
                                                                                          'Join mg In modulesGroup On g.Id Equals mg.IdGroup
                                                                                          'Where mg.IdModule = m.Id Select g).ToList()
                                                                                          'm.Group = If(res.Count > 0, res(0), New VieGroup())
                                                                                          m.Group = New VieGroup()
                                                                                          m.Forms = (From f In forms
                                                                                                     Join fm In formsModule On f.Id Equals fm.IdForm
                                                                                                     Where fm.IdModule = m.Id Select f).ToList()
                                                                                      Next
                                                                                      Return modules
                                                                                  End Function)

        _listXmlPermissionsFormNuevo = Await Task.Factory.StartNew(Of List(Of ViePermissionForm))(Function()
                                                                                                      Dim permissionsForm As List(Of ViePermissionForm) = DeserializeXml(Of ViePermissionForm)(eDataXml.XMLPermissionsForm)
                                                                                                      Return permissionsForm
                                                                                                  End Function)
        'TODO:HRR REVISAR
        '_listXmlGroupsNuevo = Await Task.Factory.StartNew(Of List(Of VieGroup))(Function()
        '                                                                            Dim groups As List(Of VieGroup) = DeserializeXml(Of VieGroup)(eDataXml.XMLGroupsNuevo)
        '                                                                            Dim modules As List(Of VieModule) = DeserializeXml(Of VieModule)(eDataXml.XMLModulesNuevo)
        '                                                                            Dim modulesGroup As List(Of VieModuleGroup) = DeserializeXml(Of VieModuleGroup)(eDataXml.XMLModulesGroupNuevo)
        '                                                                            For Each g In groups
        '                                                                                g.Modules = (From m In modules
        '                                                                                             Join mg In modulesGroup On m.Id Equals mg.IdModule
        '                                                                                             Where mg.IdGroup = g.Id Select m).ToList()
        '                                                                            Next
        '                                                                            Return groups
        '                                                                        End Function)
    End Sub

    Private Shared Function DeserializeXml(Of T As IVieXml)(ByVal Xml As eDataXml) As List(Of T)
        Dim file As String = String.Empty
        Select Case Xml
            Case eDataXml.XMLForms
                file = My.Resources.VieForms
            Case eDataXml.XMLModules
                file = My.Resources.VieModules
            Case eDataXml.XMLFormsModule
                file = My.Resources.VieFormsModule
            Case eDataXml.XMLPermissionsForm
                file = My.Resources.ViePermissionsForm
            Case eDataXml.XMLModulesGroup
                file = My.Resources.VieModulesGroup
            Case eDataXml.XMLWoeidCities
                file = My.Resources.VieWoeidCities
            Case eDataXml.XMLMonths
                file = My.Resources.VieMonths
            Case eDataXml.XMLPermissions
                file = My.Resources.ViePermissions
            Case eDataXml.XMLGroups
                file = My.Resources.VieGroups
            Case eDataXml.XMLReports
                file = My.Resources.Reports.VieReports
            Case eDataXml.XMLReportsForm
                file = My.Resources.Reports.VieReportForms
            Case eDataXml.XMLTitles
                file = My.Resources.VieTitles
            Case eDataXml.XMLFormsNuevo
                file = My.Resources.VieFormsNuevo
            Case eDataXml.XMLModulesNuevo
                file = My.Resources.VieModulesNuevo
            Case eDataXml.XMLFormsModuleNuevo
                file = My.Resources.VieFormsModuleNuevo
        End Select
        Using ms As MemoryStream = New MemoryStream(Encoding.UTF8.GetBytes(file))
            Dim xs = New System.Xml.Serialization.XmlSerializer(GetType(List(Of T)))
            Return CType(xs.Deserialize(ms), List(Of T))
        End Using
    End Function

#Region "Xml Repositories"

    ''' <summary>
    ''' Obtiene un modulo por su numero de Id
    ''' </summary>
    ''' <param name="idModule">Id del modulo a consultar</param>
    ''' <returns>Modulo consultado</returns>
    Public Shared Function GetModuleById(ByVal idModule As Integer) As VieModule
        Dim res = GetXmlWithAggregates(Of VieModule)(eDataXml.XMLModules)
        If res IsNot Nothing Then
            Dim modl = res.Where(Function(m) m.Id = idModule).ToList()
            If modl.Count > 0 Then
                Return modl(0)
            Else
                Return New VieModule()
            End If
        Else
            Return New VieModule()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un formulario por su numero de Id
    ''' </summary>
    ''' <param name="idForm">Id del formulario a consultar</param>
    ''' <returns>Formulario consultado</returns>
    Public Shared Function GetFormById(ByVal idForm As Integer) As VieDBForm
        Dim result = (From C In SessionValues.Instance.ListProductCatalog
                      From M In C.ListModules
                      From F In M.ListForm
                      Where F.IdForm = idForm
                      Select F).FirstOrDefault()

        If result IsNot Nothing Then
            Return result
        Else
            Return New VieDBForm()
        End If
    End Function

    ''' <summary>
    ''' Lista los reportes de un formulario por su Id
    ''' </summary>
    ''' <param name="idForm">Id del formulario a consultar</param>
    ''' <returns>Lista de reportes</returns>
    Public Shared Function ListReportsByIdForm(ByVal idForm As Integer) As List(Of VieReport)
        Dim res = GetXmlWithAggregates(Of VieReport)(eDataXml.XMLReports)
        If res IsNot Nothing Then
            Return res.Where(Function(r) r.Forms.Any(Function(f) f.IdForm = idForm.ToString())).ToList()
        Else
            Return New List(Of VieReport)()
        End If
    End Function

#End Region

#End Region

#Region "Utilidades"

    ''' <summary>
    ''' Corta un valor decimal a una presición dada SIN REDONDEAR
    ''' </summary>
    ''' <param name="value">Valor decimal a cortar</param>
    ''' <param name="precision">Cantidad de decimales a cortar</param>
    ''' <returns>Valor decimal resultado del proceso de cortado</returns>
    Public Shared Function TruncateDecimal(value As Decimal, precision As Integer) As Decimal
        Dim stepper As Decimal = Math.Pow(10, precision)
        Dim tmp As Decimal = Math.Truncate(stepper * value)
        Return tmp / stepper
    End Function

#End Region

End Class