'***********************************************************************
' Assembly         : Presentacion.Base
' Author           : AndresBonilla
' Created          : 
'
' Last Modified By : Sergio Fernandez
' Last Modified On : 12-10-2011
' Description      : se agregaron los mensajes de los formularios con tag de 400 a 407
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Enumeraciones"
''' <summary>
''' Enumeracion que sirve para el control de los formularios
''' </summary>
Public Enum _EStatusUserMessangin
    ''' <summary>
    ''' El usuario se encuentra en linea
    ''' </summary>
    Online
    ''' <summary>
    ''' El usuario se encuentra desconectado
    ''' </summary>
    Offline
    ''' <summary>
    ''' El usuario esta ocupado
    ''' </summary>
    Bussy
    ''' <summary>
    ''' El usuario esta ausente
    ''' </summary>
    absent
End Enum

''' <summary>
''' Enumeracion que sirve para el control de los formularioss
''' </summary>
Public Enum _Eform
    ''' <summary>
    ''' Visor de eventos(No es un frontal, pero lo necesito para consumir los recursos correctos))
    ''' </summary>
    VisorEventos = 1

    ''' <summary>
    ''' Formulario Conexiones.
    ''' </summary>
    Conexion = 2

    ''' <summary>
    ''' Recuros Comunes(No es un frontal, pero lo necesito para consumir los recursos correctos))
    ''' </summary>
    Comunes = 3

    ''' <summary>
    ''' Login(No es un frontal, pero lo necesito para consumir los recursos correctos))
    ''' </summary>
    Login = 4

    ''' <summary>
    ''' Customizar(No es un frontal, pero lo necesito para consumir los recursos correctos))
    ''' </summary>
    Customizar = 5

    ''' <summary>
    ''' Formulario Roles
    ''' </summary>
    Roles = 101

    ''' <summary>
    ''' Formulario Grupos
    ''' </summary>
    Grupos = 102

    ''' <summary>
    ''' Formulario Usuario
    ''' </summary>
    Usuario = 103

    ''' <summary>
    ''' Formulario Cambiar Contrasena
    ''' </summary>
    CambiarContrasena = 104

    ''' <summary>
    ''' Formulario Desbloquear Usuario.
    ''' </summary>
    DesbloquearUsuario = 105

    ''' <summary>
    ''' Formulario Bloquear sesion.
    ''' </summary>
    BloquearSession = 105

    ''' <summary>
    ''' Formulario Unidades Funcionales.
    ''' </summary>
    UnidadesFuncionales = 201

    ''' <summary>
    ''' Formulario Centro Atenciones.
    ''' </summary>
    CentroAtenciones = 202

    ''' <summary>
    ''' Formulario Clasificacion Codigos Cups.
    ''' </summary>
    ClasificacionCups = 203

    ''' <summary>
    ''' Formulario de portafolio de servicios.
    ''' </summary>
    PortafolioServicios = 204

    ''' <summary>
    ''' Formulario del iva.
    ''' </summary>
    Iva = 205

    ''' <summary>
    ''' Formulario de Unidades de Mercadeo 
    ''' </summary>
    UnidadesMercadeo = 206
    ''' <summary>
    ''' Formulario de Conceptos de Facturacion.
    ''' </summary>
    ConceptosFacturacion = 207

    ''' <summary>
    ''' Formulario de convenios de contratos.
    ''' </summary>
    Convenios = 208

    ''' <summary>
    ''' Formulario Tiene las Entidades Administradoras.
    ''' </summary>
    EntidadesAdministradores = 209

    ''' <summary>
    ''' Este formulario sirve para agregar actividades medicas.
    ''' </summary>
    ActividadesMedicas = 300

    ''' <summary>
    ''' Formulario que crea las especialidades.
    ''' </summary>
    NuevaEspecialidad = 301

    ''' <summary>
    ''' Formulario que contiene las especialidades que tienen relacionadas las actividades.
    ''' </summary>
    EspecialidadesActividades = 302

    ''' <summary>
    ''' Formulario que contiene los consultorios.
    ''' </summary>
    Consultorios = 305

    ''' <summary>
    ''' Formulario que contiene los difetentes tipos de causas de cancelacion de citas.
    ''' </summary>
    CausaCancelacion = 306

    ''' <summary>
    ''' Formulario que contiene el bloqueo de los medicos.
    ''' </summary>
    BloqueoMedico = 307

    ''' <summary>
    ''' Formulario de empresas
    ''' </summary>
    Empresa = 401

    ''' <summary>
    ''' Formulario de entidades
    ''' </summary>
    Entidades = 402

    ''' <summary>
    ''' Formulario que contiene los departamentos
    ''' </summary>
    Departamentos = 403

    ''' <summary>
    ''' Form que contiene los registros
    ''' </summary>
    Municipios = 404

    ''' <summary>
    ''' Form que contiene las actividades o cargos empresariales
    ''' </summary>
    ActividadesCargo = 406

    ''' <summary>
    ''' Form que contiene los niveles del sisben
    ''' </summary>
    Niveles = 407
    ''' <summary>
    ''' formulario de pacientes
    ''' </summary>
    Pacientes = 408
    ''' <summary>
    ''' Formulario profesionales
    ''' </summary>
    Profesionales = 409
    ''' <summary>
    ''' Form que contiene las Profesiones
    ''' </summary>
    Profesiones = 410
    ''' <summary>
    ''' Form que contiene los DxSindromaticos
    ''' </summary>
    DXSindromaticos = 414
    ''' <summary>
    ''' Form que contiene los Diagnosticos
    ''' </summary>
    Diagnosticos = 412
    ''' <summary>
    ''' Form que contiene los Dias Festivos
    ''' </summary>
    DiasFestivos = 413
    ''' <summary>
    ''' Form que contiene los Salarios
    ''' </summary>
    Salarios = 411
    ''' <summary>
    ''' 
    '''     ''' Form que contiene los Paramentros3047
    ''' </summary>
    Parametros3047 = 415

    ''' <summary>
    ''' Form Contine los conceptos Generales de glosas
    ''' </summary>
    ConceptosGenerales = 416

    ''' <summary>
    ''' Form Contine las compañias
    ''' </summary>
    Company = 417

    ''' <summary>
    ''' Form Contine las corporaciones
    ''' </summary>
    Corporation = 418

    ''' <summary>
    ''' Form Contine los nivels de cargo
    ''' </summary>
    PositionLevel = 419

    ''' <summary>
    ''' Form Contine los Paises
    ''' </summary>
    Country = 420

    ''' <summary>
    ''' Form Contine los Departamentos
    ''' </summary>
    Deparments = 421

    ''' <summary>
    ''' Form Contine las ciudades
    ''' </summary>
    City = 422

    ''' <summary>
    ''' Form Contine las ciudades
    ''' </summary>
    HealthCenter = 423

    ''' <summary>
    ''' Frontal de autorizacion de centros de atencion
    ''' </summary>
    AutorizationHealthCenter = 424

    ''' <summary>
    ''' Frontal de grupos
    ''' </summary>
    groups = 425

    ''' <summary>
    ''' Frontal de Recepcion de objeciones
    ''' </summary>
    RecepcionObjeciones = 426

    ''' <summary>
    ''' Control COnfirmado
    ''' </summary>
    CtrConfirmado = 427

    ''' <summary>
    ''' Frontal de Lenguaje
    ''' </summary>
    Languages = 428

    ''' <summary>
    ''' Frontal de registro de objeciones
    ''' </summary>
    RegisterObjection = 429

    ''' <summary>
    ''' Frontal de conciliacion
    ''' </summary>
    Conciliation = 430

    ''' <summary>
    ''' Frontal de devolución
    ''' </summary>
    Devolution = 431

    ''' <summary>
    ''' Frontal de tipo de pensionado
    ''' </summary>
    TipoPensionado = 432

    ''' <summary>
    ''' Frontal de centro de trabajo
    ''' </summary>
    CentroTrabajo = 433

    ''' <summary>
    ''' Frontal de centro de trabajo
    ''' </summary>
    RiesgoProfesional = 434

    ''' <summary>
    ''' Frontal de discapacidades
    ''' </summary>
    Discapacidad = 435

    ''' <summary>
    ''' Frontal de Tipo de contribuyente
    ''' </summary>
    TipoContribuyente = 436

    ''' <summary>
    ''' Frontal de Tipo de estudio
    ''' </summary>
    TipoEstudio = 437

    ''' <summary>
    ''' Frontal de grupo de contratos
    ''' </summary>
    GrupoContrato = 438

    ''' <summary>
    ''' Frontal de Terceros
    ''' </summary>
    Terceros = 439
    ''' <summary>
    ''' Frontal de cargos
    ''' </summary>
    Cargo = 440


    ''' <summary>
    ''' Frontal Tipo de Contrato
    ''' </summary>
    TipoDeContrato = 441

    ''' <summary>
    ''' Frontal Centro de estudios
    ''' </summary>
    CentrosdeEstudio = 442

    ''' <summary>
    ''' Frontal Plantilla de contratos
    ''' </summary>
    ''' <remarks></remarks>
    PlantillaContrato = 443

    ''' <summary>
    ''' Frontal Retenciones
    ''' </summary>
    ''' <remarks></remarks>
    Retenciones = 545
    ''' <summary>
    ''' Frontal Tipos de vinculacion
    ''' </summary>
    ''' <remarks></remarks>
    TipodeVinculacion = 546
    ''' <summary>
    ''' Frontal Unidad De Tiempo
    ''' </summary>
    ''' <remarks></remarks>
    UnidaddeTiempo = 548


    ''' <summary>
    ''' Control de aplicaciones abiertas
    ''' </summary>
    CtrAplicationOpen = 444

    ''' <summary>
    ''' Frontal de coordinacion
    ''' </summary>
    Coordinacion = 549
    ''' <summary>
    ''' Frontal plantilla de turno
    ''' </summary>
    ''' <remarks></remarks>
    PlantillaDeTurno = 550
    ''' <summary>
    ''' Frontal de evaluación
    ''' </summary>
    Evaluacion = 551

    ''' <summary>
    ''' Frontal de autorización de conceptos
    ''' </summary>
    AutorizacionConceptos = 556

    ''' <summary>
    ''' Frontal de incapacidades
    ''' </summary>
    ''' <remarks></remarks>
    Incapacidades = 544
    ''' <summary>
    ''' Frontal de Empleado
    ''' </summary>
    Empleado = 557
    ''' <summary>
    ''' Frontal de Aseguradoras - Mantenimiento
    ''' </summary>
    Aseguradoras = 557
    ''' <summary>
    ''' Frontal de Fabricantes Mantenimiento
    ''' </summary>
    Fabricantes = 558
    ''' <summary>
    ''' Frontal de Polizas - Mantenimiento
    ''' </summary>
    Poliza = 559
    ''' <summary>
    ''' Frontal de tipos de poliza - Mantenimiento
    ''' </summary>
    TiposPoliza = 560
    ''' <summary>
    ''' Frontal de tipos de equipos - Mantenimiento
    ''' </summary>
    TiposEquipos = 561
    ''' <summary>
    ''' Frontal de tipos de inventarios - Mantenimiento
    ''' </summary>
    TiposInventarios = 562
    ''' <summary>
    ''' Frontal de Sucursales - Mantenimiento
    ''' </summary>
    Sucursales = 563
    ''' <summary>
    ''' Frontal de accesorios - Mantenimiento
    ''' </summary>
    Accesorios = 565
    ''' <summary>
    ''' Frontal Consumibles - Mantenimiento
    ''' </summary>
    Consumibles = 566
    ''' <summary>
    ''' Frontal de Torres - Mantenimiento
    ''' </summary>
    Torres = 567
    ''' <summary>
    ''' Frontal de Pisos - Mantenimiento
    ''' </summary>
    Pisos = 568
    ''' <summary>
    ''' Frontal de Areas - Mantenimiento
    ''' </summary>
    Areas = 569
    ''' <summary>
    ''' Frontal de Habitaciones - Mantenimiento
    ''' </summary>
    Habitaciones = 570
    ''' <summary>
    ''' Frontal de resonsables - Mantenimiento
    ''' </summary>
    Responsables = 571
    ''' <summary>
    ''' Frontal de Equipos - Mantenimiento
    ''' </summary>
    Equipment = 573
    ''' <summary>
    ''' Frontal de partes - Mantenimiento
    ''' </summary>
    Part = 572
    ''' <summary>
    ''' Frontal de Ingreso de Equipos - Mantenimiento
    ''' </summary>
    EquipmentReception = 574
    ''' <summary>
    ''' Frontal de Unidad de medidas - Mantenimiento
    ''' </summary>
    UnitMeasure = 575
    ''' <summary>
    ''' Frontal de registro tecnico - Mantenimiento
    ''' </summary>
    RegistroTecnico = 576

    ''' <summary>
    ''' Frontal de parametros de glosas 
    ''' </summary>
    ''' <remarks></remarks>
    ParametersGlosas = 577

    ''' <summary>
    ''' Frontal de contrato
    ''' </summary>
    Contrato = 578
    ''' <summary>
    ''' Frontal de metadata
    ''' </summary>
    EmployeeType = 579
    ''' <summary>
    ''' Frontal de digitalización
    ''' </summary>
    Digitalizacion = 580
    ''' <summary>
    ''' Frontal de adjuntar
    ''' </summary>
    Adjuntar = 581
    ''' <summary>
    ''' Control de usuario Barra Botones
    ''' </summary>
    CtrBarraBotones = 582

    ''' <summary>
    ''' Frontal de tipo de empleados
    ''' </summary>
    TipoEmpleados = 583

    ''' <summary>
    ''' Frontal de Razon de modificacion de contratos
    ''' </summary>
    RazonModificacionContrato = 584

    ''' <summary>
    ''' Frontal de Cuadro de turnos
    ''' </summary>
    ''' <remarks></remarks>
    CuadroDeTurno = 585

    ''' <summary>
    ''' Frontal de vacaciones
    ''' </summary>
    ''' <remarks></remarks>
    Vacaciones = 587

    ''' <summary>
    ''' Frontal de Metadata
    ''' </summary>
    ''' <remarks></remarks>
    MetaData = 586

    ''' <summary>
    ''' Frontal Liquidacion Nomina
    ''' </summary>
    ''' <remarks></remarks>
    LiquidacionNomina = 588

    ''' <summary>
    ''' Liquidacion de cesantías
    ''' </summary>
    ''' <remarks></remarks>
    LiquidacionCesantias = 591

    ''' <summary>
    ''' Liquidación de Primas
    ''' </summary>
    ''' <remarks></remarks>
    LiquidacionPrimas = 597

    ''' <summary>
    ''' Fronta de Traslado Cobro Jurídico
    ''' </summary>
    TrasladoCobroJurídico

    ''' <summary>
    ''' Enumeracion para el estado de clima
    ''' </summary>
    Weather

    ''' <summary>
    ''' Enumeracion para el sistema de mensajeria
    ''' </summary>
    Messages

    ''' <summary>
    ''' Enumeración para la metadata de todos los formularios
    ''' </summary>
    InfoMetaData

    ''' <summary>
    ''' Frontal de clases de convenios
    ''' </summary>
    ''' <remarks></remarks>
    KindsAgreements = 593

    ''' <summary>
    ''' Frontal de convenios
    ''' </summary>
    ''' <remarks></remarks>
    Agreements = 595

End Enum

Public Enum _EPluginsTypes

    ''' <summary>
    ''' Tipo de Plugin redes sociales
    ''' </summary>
    RedesSociales = 1

    ''' <summary>
    ''' Tipo de Plugin redes GestionDocuemntal
    ''' </summary>
    GestionDocumental = 2

    ''' <summary>
    ''' Tipo de Plugin redes COmunicaciones Unificadas
    ''' </summary>
    ComunicacionesUnificadas = 3

    ''' <summary>
    ''' Tipo de Plugin redes Otros (Calculadoras Financieras , etc)
    ''' </summary>
    OtrosPlugins = 4


End Enum

Public Enum _EpluginsIcons

    ''' <summary>
    ''' Icono que representa una red social
    ''' </summary>
    IconoRedesSociales = 1

    ''' <summary>
    ''' Icono que representa un documento
    ''' </summary>
    IconoGestionDocumental = 2

    ''' <summary>
    ''' Icono que representa comunicaciones unificadas
    ''' </summary>
    IconoComunicacionesUnificadas = 3

End Enum

''' <summary>
''' Enumeracion que sirve para conocer que tema se va autilizar en la barra de usuario.
''' </summary>
Public Enum _EbarTheme
    ''' <summary>
    ''' 	Tema Enfermeria
    ''' </summary>
    Enfermeria = 1
    ''' <summary>
    ''' 	Tema Cuidados Intensivos
    ''' </summary>
    CuidadosIntensivos = 2
    ''' <summary>
    ''' 	Tema Medicos Generales
    ''' </summary>
    MedicosGenerales = 3
    ''' <summary>
    ''' 	Tema Cirugia
    ''' </summary>
    Cirugia = 4

    ''' <summary>
    '''     Tema Administativo
    ''' </summary>
    Administrativo = 5

End Enum

''' <summary>
''' Enumeracion que sirve para establecer el tipo de icono que se muestra en el log de eventos
''' </summary>
Public Enum _EeventViewerImages
    ''' <summary>
    ''' 	Icono de Advertencia
    ''' </summary>
    ''' <remarks></remarks>
    Advertencia = 0
    ''' <summary>
    ''' 	Icono de Informacion
    ''' </summary>
    ''' <remarks></remarks>
    Informacion = 1
    ''' <summary>
    ''' 	Icono de mensaje de error.
    ''' </summary>
    ''' <remarks></remarks>
    MensajeError = 2
    ''' <summary>
    ''' 	Icono de mensaje de error.
    ''' </summary>
    ''' <remarks></remarks>
    Pregunta = 3
End Enum

''' <summary>
''' Enumeracion que sirve para establecerle un valor a los botones que no se muestran por permisos en la barra de botones
''' </summary>
Public Enum _EbuttonsWithoutPermission
    ''' <summary>
    ''' 	Boton Nuevo de la barra de control.
    ''' </summary>
    ''' <remarks></remarks>
    Nuevo = 1
    ''' <summary>
    ''' 	Boton Copiar de la barra de control.
    ''' </summary>
    ''' <remarks></remarks>
    Copiar = 2
    ''' <summary>
    ''' 	Boton Cortar de la barra de control.
    ''' </summary>
    ''' <remarks></remarks>
    Cortar = 3
    ''' <summary>
    ''' 	Boton Pegar de la barra de control.
    ''' </summary>
    ''' <remarks></remarks>
    Pegar = 4
    ''' <summary>
    ''' 	Boton Buscar de la barra de control.
    ''' </summary>
    ''' <remarks></remarks>
    Buscar = 5
    ''' <summary>
    ''' 	Boton Deshacer de la barra de control.
    ''' </summary>
    ''' <remarks></remarks>
    Deshacer = 6
    ''' <summary>
    ''' 	Boton Permisos de la barra de control.
    ''' </summary>
    ''' <remarks></remarks>
    Permisos = 7
    ''' <summary>
    ''' 	Boton Cerrar de la barra de control.
    ''' </summary>
    ''' <remarks></remarks>
    Cerrar = 8
    ''' <summary>
    ''' 	Boton Favoritos de la barra de control.
    ''' </summary>
    ''' <remarks></remarks>
    Favoritos = 9

    ''' <summary>
    '''     Boton para el plugins Redes Sociales de la barra de control.
    ''' </summary>
    RedesSociales = 10

    ''' <summary>
    '''     Boton para el plugins gestion documental de la barra de control.
    ''' </summary>
    GestionDocumental = 11

    ''' <summary>
    '''     Boton para el plugins Comunicaciones Unificadas de la barra de control.
    ''' </summary>
    ComunicacionesUnificadas = 12

    ''' <summary>
    '''     Boton para el plugins Otros de la barra de control.
    ''' </summary>
    OtrosPlugins = 13


    ''' <summary>
    '''     Boton para el control biometrico
    ''' </summary>
    ControlBiometrico = 20

    ''' <summary>
    ''' Boton de procesos para anular
    ''' </summary>
    Anular = 21

    ''' <summary>
    ''' Botón confirmar
    ''' </summary>
    Confirmar = 22
    ''' <summary>
    ''' Botón guardar
    ''' </summary>
    Guardar = 23
    ''' <summary>
    ''' Botón actualizar
    ''' </summary>
    Actualizar = 24
    ''' <summary>
    ''' Botón eliminar
    ''' </summary>
    Eliminar = 25
    ''' <summary>
    ''' Botón imprimir
    ''' </summary>
    Imprimir = 26
    ''' <summary>
    ''' Botón Suspender
    ''' </summary>
    ''' <remarks></remarks>
    Suspender = 27
    ''' <summary>
    ''' Botón para Re-Activar
    ''' </summary>
    ''' <remarks></remarks>
    Reactivar = 28
End Enum

''' <summary>
''' Enumeracion que sirve para establecer los recursos de excepciones.
''' </summary>
Public Enum _EexceptionsResources
    ''' <summary>
    ''' Sucede cuando el constuctor del presentador no encuentra su vista correspondiente.
    ''' </summary>
    MensajeConstructorPresentador

    ''' <summary>
    ''' Sucede cuando la contraseña no se a podido cambiar debido a que el servicio retorna falso.
    ''' </summary>
    ContrasenaNosePudoCambiarContrasena

    ''' <summary>
    ''' Sucede cuando el usuario a sido validado con exito pero el usuario no tiene la suficiente informacion 
    ''' para ser mostrado o el codigo del usuario esta presente mas de una vez.
    ''' </summary>
    LoginUsuarioSinDatos
    ''' <summary>
    ''' Sucede cuando el valor solicitado por el frontal de busqueda ya a sido removido y no se pueden traer los datos
    ''' </summary>
    BusquedaValorNoEncontrado
    ''' <summary>
    ''' Este es el complemento del frontal busqueda
    ''' </summary>
    BusquedaValorNoEncontradoComplemento

    ''' <summary>
    ''' Sucede cuando el archivo a aido modificado, movido o eliminado.
    ''' </summary>
    ArchivoNoExiste
End Enum

''' <summary>
''' Enumeracion que sirve para establecer el tipo de recurso que se va consumir (localizacion)
''' </summary>
Public Enum _Eresources
    Informacion
    Advertencia
    Errores
    Pregunta
    Confirmado
    SinConfirmar
    Anulado
    OficioConRespuesta
    ComunesTodos
    ComunesAbrirEntidad
    ComunesActivo
    ComunesInactivo
    ComunesSeleccioneRegistroEliminar
    ComunesEliminarConfirmado
    ComunesConfirmadoCorrectamente
    ComunesAnuladoCorrectamente
    ComunesPreguntaAnular
    ComunesPreguntaConfirmar
    ComunesNoSeEncontroDatoERP
    ComunesClienteNoExiste
    ComunesConfirmar
    FacturasSinConfirmarComunes
    ComunesSesionLocal
    ComunesSesionRemota
    ComunesSeguroCerrarSesion
    ComunesUserNameInvalid
    ComunesSePerderanCambios
    ComunesUsuarioNoAdministrador
    ComunesBloqueoSession
    ComunesNoHayRegistros
    ComunesDias
    ComunesDebeIngresarLaRutaDeServiciosYProtocolo
    ComunesRemplazaInformacionArchivoConfiguracion
    ComunesSeleccioneAño
    EliminarRegistro
    EditarRegistro
    RegistroActivo
    RegistroInactivo
    RegistroBloqueado
    YaExisteFormulario
    RegistroEnEdicion
    ItemYaAgregado
#Region "Visor de Imagenes"
    ''' <summary>
    ''' Titulo del visor de eventos.
    ''' </summary>
    VisorTitulo
    ''' <summary>
    ''' Error en el visor de eventos.
    ''' </summary>
    VisorErrores
    ''' <summary>
    ''' Advertencia en el visor de eventos.
    ''' </summary>
    VisorAdvertencias
    ''' <summary>
    ''' Mensajes de tipo informacion en el visor de eventos.
    ''' </summary>
    VisorMensajes
#End Region
#Region "Funcional Cambiar Contrasena"

    ''' <summary>
    ''' Cuando la contraseña no cumple con el minimo de caracteres establecidos.
    ''' </summary>
    ContrasenaMinimoCaracteres
    ''' <summary>
    ''' Cuando el campo confirmar Contraseña No puede estar vacio.
    ''' </summary>
    ContrasenaConfirmacionVacia
    ''' <summary>
    ''' Cuando la contraseña anterior esta vacia.
    ''' </summary>
    ContrasenaAnteriorVacia
    ''' <summary>
    ''' Cuando la nueva contraseña esta vacia.
    ''' </summary>
    ContrasenaNuevaVacia
    ''' <summary>
    ''' Cuando la nueva contraseña NO coincide con la confirmacion de la contraseña.
    ''' </summary>
    ContrasenasNoCoinciden
    ''' <summary>
    ''' Cuando la nueva contraseña no es la correcta.
    ''' </summary>
    ContrasenaNoCorrecta
    ''' <summary>
    ''' Cuando la contraseña se a actualizado correctamente.
    ''' </summary>
    ContrasenaGuardadaCorrectamente
    ''' <summary>
    ''' Cuando se le pide al usuario que digite la contraseña.
    ''' </summary>
    ContrasenaDigiteContrasena

#End Region
#Region "Mensajes Comunes"

    ''' <summary>
    ''' Mensaje que se envia cuando hay error al realizar una operacion del crud.
    ''' </summary>
    ComunesError
    ''' <summary>
    ''' Cuando el registro se a actualizado correctamente.
    ''' </summary>
    ComunesActualizado
    ''' <summary>
    ''' Cuando el codigo esta vacio.(Cualquier codigo:Roles-grupo-unidad....)
    ''' </summary>
    ComunesCodigoVacio
    ''' <summary>
    ''' Cuando el usuario da click en guardar, eliminar o actualizar y no a digitado ningun dato.
    ''' </summary>
    ComunesDigiteDatos
    ''' <summary>
    ''' Cuando el registro se a eliminado correctamente.
    ''' </summary>
    ComunesEliminado
    ''' <summary>
    ''' Cuando el registro se a guardado correctamente.
    ''' </summary>
    ComunesGuardado
    ''' <summary>
    ''' Cuando el nombre se encuentra vacio.
    ''' </summary>
    ComunesNombreVacio
    ''' <summary>
    ''' Cuando el archivo se a creado con exito.
    ''' </summary>
    ComunesArchivoCreado
    ''' <summary>
    ''' Titulo de los xtramessages ("Indigo Crystal.NET")
    ''' </summary>
    ComunesIndigoCrystal
    ''' <summary>
    ''' Cuando se quiere pedir una direccion("Por Favor, digite la Dirección")
    ''' </summary>
    ComunesDireccion
    ''' <summary>
    ''' Cuando se pide elegir una opcion de un radiobutton o checks("Por Favor, elija una Opción.")
    ''' </summary>
    ComunesElija
    ''' <summary>
    ''' Cuando el codigo no puede ser menor a 3 caracteres.
    ''' </summary>
    ComunesCodigoCaracteres
    ''' <summary>
    ''' Cuando se quiere preguntar si se desea eliminar un registro
    ''' </summary>
    ComunesEliminarRegistro
    ''' <summary>
    ''' Cuando se quiere mostrar al usuario que contacte al administrador.
    ''' </summary>
    ComunesContacteAdministrador
    ''' <summary>
    ''' Cuando se quiere mostrar el mensaje que falta un correo personal.
    ''' </summary>
    ComunesCorreoPersonal
    ''' <summary>
    ''' Cuando se quiere mostrar el mensaje que falta un correo empresarial.
    ''' </summary>
    ComunesCorreoEmpresarial
    ''' <summary>
    ''' Cuando se quiere mostrar que el diseño del frontal a sido cambiado y se va a guardar en un XML
    ''' </summary>
    ComunesXmlCambio
    ''' <summary>
    ''' Cuando se quiere mostrar  el Por Favor mas un complemento
    ''' </summary>
    ComunesPorFavor
    ''' <summary>
    ''' Cuando se quiere mostrar  el Seleccione para los casos de GridLookUp o un lista
    ''' </summary>
    ComunesSeleccione
    ''' <summary>
    ''' Cuando se quiere mostrar  que el usuario no tiene Pemisos para consultar
    ''' </summary>
    ComunesNoTienePermisos
    ''' <summary>
    ''' Cuando no se ha guardado la definicion XML de un funcional
    ''' </summary>
    ComunesErrorGuardarDefinicion

    ''' <summary>
    ''' Se usa cuando se provoca un error al tratar de levantar una definicion que exista.
    ''' </summary>
    ComunesErrorLevantarDefinicion

    ''' <summary>
    ''' Se usa para dar el mensaje de que el layout se restablecio correctamente
    ''' </summary>
    ComunesLayoutRestablecido

    ''' <summary>
    ''' Se usa para informar que el control que tiene el foco no admite busquedas
    ''' </summary>
    ComunesNoAplicaBuscar

    ''' <summary>
    ''' se usa para preguntar si se desea realmente confirmar la factura
    ''' </summary>
    ''' <remarks></remarks>
    ComunesConfirmarFactura

    ''' <summary>
    ''' Se una cuando el formulario tiene datos por ingresar
    ''' </summary>
    ''' <remarks></remarks>
    ComunesFormIncompleto

    ''' <summary>
    ''' Se usa cuando el mensaje es que la fecha no puede ser menor a la fecha actual
    ''' </summary>
    ComunesFechaMenorActual

    ''' <summary>
    ''' The cerrar formularios cambiar empresa
    ''' </summary>
    CerrarFormulariosCambiarEmpresa

    ''' <summary>
    ''' se usa para confirmar si el usuario quiere editar el registro
    ''' </summary>
    ''' <remarks></remarks>
    ComunesEditarRegistro

    ''' <summary>
    ''' Enumeracion para mostrar el mensaje de que no tiene permisos a la empresa seleccionada
    ''' </summary>
    ComunesUsuarioSinPermisosEmpresa

    ''' <summary>
    ''' Enumeracion para mostrar el mensaje de error de concurrencia
    ''' </summary>
    ''' <remarks></remarks>
    ComunesErrorConcurrencia

    ''' <summary>
    ''' Enumeracion para mostrar el mensaje de error al eliminar registro que ya tiene dependencia
    ''' </summary>
    ''' <remarks></remarks>
    ComunesErrorDependencia

    ''' <summary>
    ''' Enumeracion para mostrar el mensaje de que el rol tiene el permiso asignado
    ''' </summary>
    ''' <remarks></remarks>
    ComunesDebeQuitarPermisoRoll
    ''' <summary>
    ''' Enumeración para mostrar el mensaje de pregunta sobre la generación de un reporte
    ''' </summary>
    ComunesGenerarReporte

#Region "Permisos"

    ''' <summary>
    ''' Se usa para establecer el permiso de eliminar.
    ''' </summary>
    PermisosEliminar
    ''' <summary>
    ''' Se usa para establecer el permiso de guardar.
    ''' </summary>
    PermisosGuardar
    ''' <summary>
    ''' Se usa para establecer el permiso de actualizar.
    ''' </summary>
    PermisosActualizar

    ''' <summary>
    ''' Se Usa para eliminar en la rejilla
    ''' </summary>
    PermisosEliminarRejilla

    ''' <summary>
    ''' Se usa para establecer el permiso de customizar el frontal.
    ''' </summary>
    PermisosCustomizar

    ''' <summary>
    ''' Se usa para establecer el permiso de utilizar redes sociales en el funcional.
    ''' </summary>
    PermisosRedesSociales
    ''' <summary>
    ''' Se usa para establecer el permiso de utilizar la Gestion Documental en el funcional.
    ''' </summary>
    PermisosDocumentos
    ''' <summary>
    ''' Se usa para establecer el permiso de utilizar las comunicaciones en el funcional.
    ''' </summary>
    PermisosComunicacion
    ''' <summary>
    ''' Se usa para establecer el permiso de utilizar los otros plugins en el funcional.
    ''' </summary>
    PermisosOtrosPlugins

    ''' <summary>
    ''' Se usa para establecer el permiso de consultar en el frontal.
    ''' </summary>
    PermisosConsultar

    ''' <summary>
    ''' Se usa para establecer el permiso de Visible en el frontal.
    ''' </summary>
    PermisosVisible


#End Region

#End Region
#Region "Funcional Rol"
    ''' <summary>
    ''' Se usa para el complemento del frontal rol ("del Rol.")
    ''' </summary>
    RolesMensajeComplemento

#End Region
#Region "Funcional Usuario"

    ''' <summary>
    ''' Se usa para el complemento del frontal usuario ("del Usuario.")
    ''' </summary>
    UsuarioMensajeComplemento
    ''' <summary>
    ''' Se usa Cuando se produce un error y se quiere mostrar que el usuario no existe.
    ''' </summary>
    UsuarioNoExiste
#End Region
#Region "Funcional Unidad Funcional"
    ''' <summary>
    ''' Se usa para el complemento del frontal unidadfuncional ("de la unidad funcional.")
    ''' </summary>
    UnidadFunMensajeComplemento
#End Region
#Region "Desbloquear Usuario"

    ''' <summary>
    ''' Se usa cuando el usuario ha sido desbloqueado con exito.
    ''' </summary>
    DesbloquearUsuarioDesbloqueado
    ''' <summary>
    ''' Se usa cuando el usuario NO a podido ser desbloqueado.
    ''' </summary>
    DesbloquearUsuarioNOdesbloqueado



#End Region
#Region "Funcional Grupos"
    ''' <summary>
    ''' Se usa para el complemento del frontal grupos ("del Grupo.")
    ''' </summary>
    GruposMensajeComplemento
    ''' <summary>
    ''' Se usa para preguntarle al usuario si desea eliminar el grupo.
    ''' </summary>
    GruposNoEliminado
#End Region
#Region "Funcional Conexion"
    ''' <summary>
    ''' Se usa para cuando se le muestra al usuario que debe reiniciar indigo.
    ''' </summary>
    ConexionReiniciarIndigo

    ''' <summary>
    ''' Se usa para mostrar el mensaje de que no puede tener vacio la URL del servidor.
    ''' </summary>
    ConexionUrlServidor

    ''' <summary>
    ''' Se usa para mostrar el mensaje de que no puede tener vacio la URL del servidor de entidades.
    ''' </summary>
    ConexionUrlServidorEntidades

    ''' <summary>
    ''' Se usa cuando se quiere mostrar que la ruta escogida no existe o no es valida.
    ''' </summary>
    ConexionRutaNoExiste

    ''' <summary>
    ''' Se usa para pedir al usuario seleccione una ruta.
    ''' </summary>
    ConexionSeleccioneRuta

#End Region
#Region "Funcional Centro atencion"
    ''' <summary>
    ''' Se usa para el complemento del frontal centro atencion ("del Centro de Atencion.")
    ''' </summary>
    CentroAtenMensajeComplemento
    ''' <summary>
    ''' Se usa para el codigo de habilitación del centro de atencion.
    ''' </summary>
    CentroCodigoHabilitacion
    ''' <summary>
    ''' Se usa para saber si la unidad funcional ya a sido seleccionada con anterioridad
    ''' </summary>
    CentroUnidadAgregada

    ''' <summary>
    ''' Se usa para preguntar al usuario si desea eliminar el centro de atencion y las unidades funcionales relacionadas.
    ''' </summary>
    CentroEliminado



#End Region
#Region "Funcional Nueva Especialidad"
    ''' <summary>
    ''' Se usa para el complemento del funcionl de Nueva especialidad
    ''' </summary>
    NuevaEspecialidadMensajeCompleto
#End Region
#Region "Funcional EspecialidadesActividades"
    ''' <summary>
    ''' Se usa para el complemento del funcionl de la especialidad
    ''' </summary>
    EspecialidadMensajeCompleto

    ''' <summary>
    ''' se usa cuando se va a agregar una actividad que ya esta relacionada
    ''' </summary>
    ActividadRelacionadaExistente

    ''' <summary>
    ''' se usa cuando se va  a eliminar una actividad de la especialiad
    ''' </summary>
    Actividadeliminada
#End Region
#Region "Funcional Actividades Medicas"
    ''' <summary>
    ''' Se sa para el complemento del funcionl de las actividades
    ''' </summary>
    ActividadesMensajeCompleto

    ''' <summary>
    ''' Se usa para indicar que debe seleccionar un color de etiqueta
    ''' </summary>
    ColorEtiqueta
    ''' <summary>
    ''' Se usa para indicarle al usuario que debe especificar el numero de pacientes
    ''' </summary>
    NumeroPaciente
    ''' <summary>
    ''' Se usa para exigir la duracion de la actividad
    ''' </summary>
    Duracion
    ''' <summary>
    ''' Se usa para exigir El numero de control mensual
    ''' </summary>
    ControlMensual
    ''' <summary>
    ''' Se usa para exigir El numero de control diario
    ''' </summary>
    ControlDiario
    ''' <summary>
    ''' Se usa para exigir El numero de intervalo entre citas
    ''' </summary>
    Intervalo
#End Region
#Region "Funcional Cups"

    ''' <summary>
    ''' Se utiliza para informar que tiene que seleccionar un subgrupo Valido
    ''' </summary>
    CupsSubGrupo

#End Region
#Region "Funcional IVA"
    IvaMensajeComplemento
#End Region
#Region "Funcional EntidadesAdmin"
    EntidadMensajeComplemento
#End Region
#Region "Customizar"

    ''' <summary>
    ''' Se utiliza para el recurso "Espacio Vacio"
    ''' </summary>
    CustomizarEspacioVacio
    ''' <summary>
    ''' Se utiliza para el recurso "Etiqueta"
    ''' </summary>
    CustomizarEtiqueta
    ''' <summary>
    ''' Se utiliza para el recurso "Separador"
    ''' </summary>
    CustomizarSeparador
    ''' <summary>
    ''' Se utiliza para el recurso "Divisor"
    ''' </summary>
    CustomizarDivisor
    ''' <summary>
    ''' Se utiliza para el recurso "Mostrar Item"
    ''' </summary>
    CustomizarMostrarItem
    ''' <summary>
    ''' Se utiliza para el recurso "Mostrar Texto"
    ''' </summary>
    CustomizarMostrarTexto
    ''' <summary>
    ''' Se utiliza para el recurso "Ocultar Item"
    ''' </summary>
    CustomizarOcultarItem
    ''' <summary>
    ''' Se utiliza para el recurso "Ocultar Texto"
    ''' </summary>
    CustomizarOcultarTexto

#End Region
#Region "Login"

    ''' <summary>
    ''' Se usa cuando el usuario debe seleccionar una unidad funcional.
    ''' </summary>
    LoginUnidadFuncional
    ''' <summary>
    ''' Se usa cuando el usuario debe seleccionar un Centro de atencion.
    ''' </summary>
    LoginCentroAtencion
    ''' <summary>
    ''' Se usa cuando se debe seleccionar una empresa a conectarse.
    ''' </summary>
    LoginSeleccioneEmpresa
    ''' <summary>
    ''' Se usa cuando se debe seleccionar el perfil asistencial del usuario si es un usuario asistencial.
    ''' </summary>
    LoginPerfilAsistencial
    ''' <summary>
    ''' Se usa cuando el usaurio ha sido validado pero el codigo del usuario se repite mas de una vez
    ''' </summary>
    LoginUsuarioBloqueado
    ''' <summary>
    ''' Se usa cuando el usuario se equivoca en la contraseña o el usuario no existe.
    ''' </summary>
    LoginIntentoContrasena
    ''' <summary>
    ''' Se usa para complementar el mensaje de Intentos de contraseña.
    ''' </summary>
    LoginIntentoContrasenaComplemento
    ''' <summary>
    ''' Se usa para mostrarle al usuario que debe cambiar la contraseña para poder iniciar la aplicacion.
    ''' </summary>
    LoginCambiarContrasena
    ''' <summary>
    ''' Se usa cuando se a guardado correctamente la configuracion del usuario.
    ''' </summary>
    LoginGuardarComo
    ''' <summary>
    ''' Se usa cuando la cuenta del usuario ya a caducado.
    ''' </summary>
    LoginCuentaCaduco

    ''' <summary>
    ''' Se usa cuando la cuenta del usuario esta inactiva
    ''' </summary>
    LoginUsuarioInactivo

#End Region
#Region "CausaCanelacion"
    ''' <summary>
    ''' Contiene el complemento del mensaje completo de la causa de cancelacion
    ''' </summary>
    CausaCancelacionMensajeCompleto
#End Region
#Region "Funcional Consultorios"
    ''' <summary>
    ''' Se usa para el complemento del funcional de consultorios
    ''' </summary>
    ConsultorioMensajeCompleto
#End Region
#Region "Funcional BloqueoMedico"
    ''' <summary>
    ''' Se usa para el complemento del funcionl de bloqueo de medic
    ''' </summary>
    BloqueoMedicoMensajeCompleto

    ''' <summary>
    ''' Se usa para enviar un mensaje que alerte que se debe selecionar una fecha de inicio
    ''' </summary>
    seleccioneFechainicio

    ''' <summary>
    ''' Se usa para enviar un mensaje que alerte que se debe selecionar una fecha de fin
    ''' </summary>
    seleccioneFechafin
    ''' <summary>
    ''' Se usa para enviar un mensaje que alerte que se la fecha e inicio seleccionada no es valida
    ''' </summary>
    seleccioneFechainicioinvalida
    ''' <summary>
    ''' Se usa para enviar un mensaje que alerte que se la fecha de fin seleccionada no es valida
    ''' </summary>
    seleccioneFechafininvalida

    ''' <summary>
    ''' Se usa para enviar un mensaje que alerte que debe seleccionar estado
    ''' </summary>
    seleccioneEstado

    ''' <summary>
    ''' Se usa para enviar un mensaje que alerte que debe seleccionar una causa
    ''' </summary>
    seleccioneCausa
#End Region
#Region "Funcional Empresas"
    ''' <summary>
    ''' Se usa para el complemento del funcional de consultorios
    ''' </summary>
    EmpresasMensajeCompleto
#End Region
#Region "Funcional Entidades"
    ''' <summary>
    ''' Se usa para el complemento del funcional de consultorios
    ''' </summary>
    EntidadesMensajeCompleto
#End Region
#Region "Funcional Departamentos"
    ''' <summary>
    ''' Se usa para el complemento del funcional de consultorios
    ''' </summary>
    DepartamentosMensajeCompleto
#End Region
#Region "Funcional Municipios"
    ''' <summary>
    ''' Se usa para el complemento del funcional de consultorios
    ''' </summary>
    MunicipiosMensajeCompleto
    ''' <summary>
    ''' Se usa para pedir que seleccione el departamento
    ''' </summary>
    SeleccioneDepartamento
#End Region
#Region "Funcional ActividadesCargo"
    ''' <summary>
    ''' Se usa para el complemento del funcional de consultorios
    ''' </summary>
    ActividadesCargoMensajeCompleto
#End Region
#Region "Funcional Niveles"
    ''' <summary>
    ''' Se usa para el complemento del funcional de consultorios
    ''' </summary>
    NivelesMensajeCompleto
#End Region
#Region "Funcional Diagnosticos"
    ''' <summary>
    ''' Se usa para el complemento del funcional de consultorios
    ''' </summary>
    DiagnosticosMensajeCompleto
    ''' <summary>
    ''' Se usa para pedir que ingrese la edad minima
    ''' </summary>
    IngreseEdadMinima
    ''' <summary>
    ''' Se usa para pedir que ingrese la edad maxima
    ''' </summary>
    IngreseEdadMaxima
    ''' <summary>
    ''' Se usa para pedir que seleccione la unidad de edad maxima
    ''' </summary>
    SeleccioneUnidadEdadMaxima
    ''' <summary>
    ''' Se usa para pedir que seleccione la unidad de edad minima
    ''' </summary>
    SeleccioneUnidadEdadMinima
    ''' <summary>
    ''' Se usa para pedir que seleccione al menos un sexo
    ''' </summary>
    SeleccioneSexo
    ''' <summary>
    ''' Se usa para pedir que ingrese el CIE10
    ''' </summary>
    IngreseCIE10

    ''' <summary>
    ''' Se usa cuando la unidad no es valida
    ''' </summary>
    UnidadNovalida
    ''' <summary>
    ''' Se usa cuando la edad ingresada no es valida
    ''' </summary>
    EdadNovalida
#End Region
#Region "Funcional Dias Festivos"
    ''' <summary>
    ''' Se usa para el complemento del funcional de DiasFestivos
    ''' </summary>
    FestivosMensajeCompleto
    ''' <summary>
    ''' Se usa para pedir que ingrese la fecha del dia festivo
    ''' </summary>
    festivosFecha
    ''' <summary>
    ''' Se usa para Pedir que ingrese el motivo del dia festivo
    ''' </summary>
    FestivosMotivo
#End Region
#Region "Funcional DX Sindromaticos"
    ''' <summary>
    ''' Se usa mostrar el label del gauget
    ''' </summary>
    SidromaticoEmergencia
    ''' <summary>
    ''' Se usa mostrar el label del gauget
    ''' </summary>
    SidromaticoUrgenciaMedica
    ''' <summary>
    ''' Se usa mostrar el label del gauget
    ''' </summary>
    SidromaticoUrgenciaDiferida
    ''' <summary>
    ''' Se usa mostrar el label del gauget
    ''' </summary>
    SidromaticoNoUrgente
    ''' <summary>
    ''' Se usa para el complemento del funcional de DxSindromatico
    ''' </summary>
    SidromaticoMensajeCompleto
    ''' <summary>
    ''' Se usa para pedir que seleccione un nivel
    ''' </summary>
    SindromaticoNivel
    ''' <summary>
    ''' Se usa para Pedir que ingrese el Tipo 
    ''' </summary>
    SindromaticoTipo
#End Region
#Region "Funcional Salarios"
    ''' <summary>
    ''' Se usa para el complemento del funcional de Salarios
    ''' </summary>
    SalariosMensajeCompleto
    ''' <summary>
    ''' Se usa para pedir que ingrese el valor del salario
    ''' </summary>
    SalariosValor

#Region "Funcional Profesiones"
    ''' <summary>
    ''' Se usa para el complemento del funcional de Profesiones
    ''' </summary>
    ProfesionesMensajeCompleto
#End Region

#End Region
#Region "Funcional Pacientes"
    ''' <summary>
    ''' Se usa para el complemento del funcional de Profesiones
    ''' </summary>
    IdentificacionMamaNOValida

    ''' <summary>
    ''' Se usa cuando no existen los datos de la madre
    ''' </summary>
    NoexisteDatosMama

    ''' <summary>
    ''' Se usa cuando no existen datos de la madre
    ''' </summary>
    NoexisteuncodigodelaMama

    ''' <summary>
    ''' Se usa cuando se tiene que digitar otra vez la identificacion
    ''' </summary>
    DebeDigitarDeNuevolaIdentificacion

    ''' <summary>
    ''' Se usa cuando los datos de identificacion no coinciden
    ''' </summary>
    LasIdentificacionNoCoincidieron

    ''' <summary>
    ''' Se usa cuando se lanza una alerta
    ''' </summary>
    Alerta

    ''' <summary>
    ''' Se usa cuando la identificaion de madre no coincide
    ''' </summary>
    IdentificacionMamaEsdeHombre

    ''' <summary>
    ''' Hijo
    ''' </summary>
    Hijo

    ''' <summary>
    ''' Hija
    ''' </summary>
    Hija

    ''' <summary>
    ''' Se usa cuando se produce un error al tratar de copiar los archivos al servidor web.
    ''' </summary>
    ErrorAlcopiarAlservidorWeb

    ''' <summary>
    ''' Numero de identificacion
    ''' </summary>
    NumeroIdentificacion

    ''' <summary>
    ''' Se usa cuando para concatenar un mensaje.
    ''' </summary>
    Mensajecomplemento

    ''' <summary>
    ''' Se usa cuando no hay parametros del paciente
    ''' </summary>
    NoExistenParametros

    ''' <summary>
    ''' Se usa cuando se da un error al adjuntar errores.
    ''' </summary>
    ErrorAdjuntararchivos
    ''' <summary>
    ''' se usa cuando se requiere concatenar antes del numero de identificacion
    ''' </summary>
    PacientesConElNumeroDeIdentificacion
    ''' <summary>
    ''' se usa cuando se requiere la identificacion de la madre
    ''' </summary>
    PacientesDebeDigitarIdMadre
    ''' <summary>
    ''' se usa cuando se debe selecconar el tipo de anonimos
    ''' </summary>
    PacientesDebeSeleccionarTipoIdAnonimos
    ''' <summary>
    ''' se usa cuando se debe digitar el codigo de la madre
    ''' </summary>
    PacientesDebeDigitarCodigoMadre
    ''' <summary>
    ''' se usa cuando se requiere que expecifique el lugar de expedicion del documento
    ''' </summary>
    PacientesEspecifiqueLugarExpedicion
    ''' <summary>
    ''' se usa cuando se require el primer apellido
    ''' </summary>
    PacientesEspecifiquePrimerApellido
    ''' <summary>
    ''' se usa cuando se require la fecha de nacimiento
    ''' </summary>
    PacientesEspecifiqueFechaNacimiento
    ''' <summary>
    ''' se usa cuando se quire mostrar que la fecha esta mal ingresada
    ''' </summary>
    PacientesLaFechaNoPuedeSerIgualoMayoraLaActual
    ''' <summary>
    ''' se usa cuando se require especificar el sexo
    ''' </summary>
    PacientesEspecifiqueSexo
    ''' <summary>
    ''' se usa cuando se requiren los datos de contacto
    ''' </summary>
    PacientesEspecifiqueDatosContacto
    ''' <summary>
    ''' se usa cuando se require el estado civil
    ''' </summary>
    PacientesEspecifiqueEstadoCivil
    ''' <summary>
    ''' se usa cuando se quire mostrar que se van a cargar los datos de la madre
    ''' </summary>
    PacientesDeseaCargarDatosMadre
    ''' <summary>
    ''' se usa cuando se informa que no exite el codigo de la madre
    ''' </summary>
    PacientesNoExisteUnCodigoMadre
    ''' <summary>
    ''' se usa cuando no se ha ingresado al descripcion de un archivo
    ''' </summary>
    PacientesEcribaDescripcionArchivo
    ''' <summary>
    ''' se usa cuando no existe la ruta de los documentos
    ''' </summary>
    PacientesNoExisteRutaDocumentos
    ''' <summary>
    ''' se usa cuando se ha definido la ruta de documentos
    ''' </summary>
    PacientesLaRutaDeDocumentoDefinidaPara
    ''' <summary>
    ''' se usa cuando los documentos no son validos
    ''' </summary>
    PacientesDocumentoValidos
    ''' <summary>
    ''' se usa cuando supera el numero de caracteres permitidos 
    ''' </summary>
    PacientesElTamañoPermitido
    ''' <summary>
    ''' se usa cuando el archivo ya se encuentra
    ''' </summary>
    PacientesArchivoYaSeEncuentra
    ''' <summary>
    ''' se usa cuando se van a editar las reglas de negocio
    ''' </summary>
    PacientesEditarReglasNegocio
    ''' <summary>
    ''' se usa cuando se van editar el flujo de trabajo
    ''' </summary>
    PacientesEditarFlujoTrabajo
    ''' <summary>
    ''' se usa cuando se require el primer nombre
    ''' </summary>
    PacientesPrimerNombre
    ''' <summary>
    ''' se usa cuando se muestra que los parametros no han sido establecidos
    ''' </summary>
    PacintesParametrosNoEstablecidos
    ''' <summary>
    ''' se usa cuando se muestra que debe seleccionar un paciente en el frontal coincidencias
    ''' </summary>
    PacintesDebeSeleccionarUnPaciente

















#End Region
#Region "Funcional Parametros Centro Atencion"
    ''' <summary>
    ''' Se usa para pedir que ingrese un email local
    ''' </summary>
    ParametrosEspecifiqueEmailLocal
    ''' <summary>
    ''' Se usa para especigique un indicativo local
    ''' </summary>
    ParametrosEspecifiqueIndicativoLocal
    ''' <summary>
    ''' Se usa para que especifique un numero de fax local
    ''' </summary>
    ParametrosEspecifiqueFaxLocal
    ''' <summary>
    ''' Se usa para que especifique una extencion local
    ''' </summary>
    ParametrosEspecifiqueExtencionLocal
    ''' <summary>
    ''' Se usa para que seleccione un centro de atencion
    ''' </summary>
    ParametrosSeleccionesCentroAtencion
    ''' <summary>
    ''' Se usa para idicar que la ruta a exedido el tamaño maximo de caracteres
    ''' </summary>
    ParametrosRutaExedeCaracteresMaximo
#End Region
#Region "Funcional Profesionales"
    ''' <summary>
    ''' Se usa para informar que la especialidad seleccionada ya esta relacionada
    ''' </summary>
    ProfesionalesMensajeEspecialidadRepetida
    ''' <summary>
    ''' Se usa para informa que hay que relacionar una especialidad
    ''' </summary>
    ProfesionalesRelacioneEspecialidad
#End Region
#Region "Funcional Conceptos Generales"
    ''' <summary>
    ''' Se usa para informar que debe seleccionar un concepto general para poder eliminar
    ''' </summary>    
    SeleccioneUnConceptoGeneral
#End Region
#Region "Funcional Empresas"
    ''' <summary>
    ''' Se usa para informar que debe seleccionar un concepto general para poder eliminar
    ''' </summary>
    SeleccioneUnaEmpresa
#End Region
#Region "Funcional Paises"
    ''' <summary>
    ''' Se usa para informar que debe seleccionar un pais para poder eliminar
    ''' </summary>
    SeleccioneUnPais
#End Region
#Region "Funcional Ciudades"
    ''' <summary> 
    ''' Se usa para informar que debe seleccionar un pais para poder eliminarla
    ''' </summary>
    SeleccioneUnaCiudad
#End Region

#Region "Funcional Empresas"
    ''' <summary>
    ''' Se usa para informar que debe seleccionar un concepto general para poder eliminar
    ''' </summary>
    SeleccioneUnCentroDeAtencion
    ''' <summary>
    ''' Texto del menu contextual de la rejilla de centros de atención
    ''' </summary>
    MenuEliminarCentroCompanies
#End Region
#Region "Funcional Empresas"
    ''' <summary>
    ''' Se usa cuando se va a agregar un centro de atencion que ya esta
    ''' </summary>
    CentroAtencionExisteEnEmpresa
#End Region
#Region "Funcional Autorizacion de centros de atencion"
    ''' <summary>
    ''' Se usa cuando se va consultar o agregar una autorizacion de centro de atencion a un usuario
    ''' </summary>
    SeleccioneUnUsuario
    ''' <summary>
    ''' Se usa cuando se va a agregar una empresa que ya existe en la autorizacion de centros de atencion
    ''' </summary>
    EmpresaYaExiste
#End Region

#Region "Funcional Empresas"
    ''' <summary>
    ''' Se usa para informar que debe seleccionar un concepto general para poder eliminar
    ''' </summary>
    seleccioneCorporacion
#End Region


#Region "Funcional Grupos"
    ''' <summary>
    ''' Se usa para informar que debe seleccionar un concepto general para poder eliminar
    ''' </summary>
    seleccioneGrupo

    ''' <summary>
    ''' Se una para informar que el concepto que se va a agregar ya esta registrado
    ''' </summary>
    ''' <remarks></remarks>
    conceptoYaAgregado

    ordinario

    feriado

    normal

    nocturno

    ''' <summary>
    ''' Se utiliza para informar que debe registrar un concepto de cada dia y jornada
    ''' </summary>
    ''' <remarks></remarks>
    GrupoEventosMinReg
#End Region

#Region "Funcional Lenguaje"
    ''' <summary>
    ''' Se usa para informar que debe seleccionar idioma para poder eliminar
    ''' </summary>
    seleccioneLenguaje
#End Region

#Region "Frontal de Recepcion de Objeciones"

    ''' <summary>
    ''' Mensaje de control para que no confirmen facturas sin valor glosado
    ''' </summary>
    ''' <remarks></remarks>
    ConfirmarFacturaSinValorGlosado

    ''' <summary>
    ''' Mensaje de control debe estar todas las facturas confirmadas para poder confirmar el oficio
    ''' </summary>
    ''' <remarks></remarks>
    FacturaSinConfirmarEnLista

    ''' <summary>
    ''' Mensaje que no permite anular porque existen movimineto glosa
    ''' </summary>
    ''' <remarks></remarks>
    NoPuedeAnularHayMovimiento

    ''' <summary>
    ''' Mensaje de Informacion cuando no se puede eliminar una factura del detalle de oficio porque ya tiene movimientos
    ''' </summary>
    ''' <remarks></remarks>
    NoPuedeEliminarPorMovimiento


    ''' <summary>
    ''' Mensaje para mostrar que el item tiene un detalle qx
    ''' </summary>
    GloseDetalleQX

    ''' <summary>
    ''' Mensaje para mostrar que No se Puede GLosar Por Estado ERP 
    ''' </summary>
    ''' <remarks></remarks>
    NoPuedeGlosarEstadoERP

    ''' <summary>
    ''' Mensaje para Mostrar que no se puede guardar por el estado erp
    ''' </summary>
    ''' <remarks></remarks>
    NoPuedeGuardarPorEstadoERP

    ''' <summary>
    ''' Mensaje para mostrar que no se puede realizar una accion por que el oficio ya esta anulado
    ''' </summary>
    ''' <remarks></remarks>
    NoPuedeRealizarAccionAnulado

    ''' <summary>
    ''' Mensaje para mostrar que no se puede agregar la factura por el estado
    ''' </summary>
    NoSePuedeAgregarFacturaEstado


    ''' <summary>
    ''' Mensaje para mostrar que no se puede agregar la factura por que ya esta en otro oficio con estado no confirmado
    ''' </summary>
    ''' <remarks></remarks>
    NosePuedeAgregaFacturaRadicada

    ''' <summary>
    ''' Mensaje para mostrar que no se puede agregar la factura porque ya esta como reiterada con otro oficio
    ''' </summary>
    ''' <remarks></remarks>
    NosePuedeAgregaFacturaReiterada

    ''' <summary>
    ''' Mensaje para mostrar al realizar una reiteracion masiva
    ''' </summary>
    ''' <remarks></remarks>
    ConfirmarReitereacionMasiva

    ''' <summary>
    ''' Mensaje para mostrar que no existe informacion o lista de factura en memoria para pegar 
    ''' </summary>
    ''' <remarks></remarks>
    NoExisteInformacionAPegar


    ''' <summary>
    ''' Mensaje para Mostrar que no se puede eliminar datos de reiteracion por que el oficio esta confirmado
    ''' </summary>
    ''' <remarks></remarks>
    NoPuedeEliminarDatoReiteracion

    ''' <summary>
    ''' Label de Nuevo Objecion
    ''' </summary>
    ''' <remarks></remarks>
    LabelNuevo

    ''' <summary>
    ''' Texto de Mensaje de Estado de factura
    ''' </summary>
    ''' <remarks></remarks>
    FacturaSinRadicar
    FacturaRadicadaSinConfirmar
    FacturaAnulada

    ''' <summary>
    ''' Texto del Encabezado de las Rejilla principal de Recepcion de Objeciones
    ''' </summary>
    ''' <remarks></remarks>
    EncabezadoReiteracion
    TiponoReconocido

    ''' <summary>
    ''' Texto Para Los menus Contextuales de las Rejillas
    ''' </summary>
    ''' <remarks></remarks>
    MenuVerDetalle
    MenuEliminar
    MenuGlosarSeleccion
    MenuReiterar

    ''' <summary>
    ''' 'Mensaje para mostrar que no se puede agregar factura como reiterada porque esta en proceso de evaluacion o cordinacion y no se a generado respuesta
    ''' </summary>
    ''' <remarks></remarks>
    NosePuedeAgregaFacturaEnProceso

    ''' <summary>
    ''' Mensaje para mostrar que no puede guardar por que no hay facturas  agregadas en la lista
    ''' </summary>
    ''' <remarks></remarks>
    NoPuedeGuardarPorDetalleOficio

    ''' <summary>
    ''' se usa en el mensaje de no permitir una reiteracion a un item si este no tiene moviminetos glosa
    ''' </summary>
    ''' <remarks></remarks>
    NoPuedeReiterarNoExisteGlosa

#End Region

#Region "Frontal registro de objeciones"

    ''' <summary>
    ''' Se usa para informar al usuario que el concepto especifico
    ''' seleccionado no tiene conceptos detallados relacionados
    ''' </summary>
    ConceptoEspecificoSinDetallados
    ''' <summary>
    ''' Se usa para el text del boton agregar objecion
    ''' </summary>
    TextBotonAgregarObjecion
    ''' <summary>
    ''' Se usa para el text del boton modificar objecion
    ''' </summary>
    TextBotonModificarObjecion

    ''' <summary>
    ''' Se usa para mostrar Mensaje cuando no existe la objecion con el consecutivo ingresado
    ''' </summary>
    ConsecutivoObjecionNoExiste

    ''' <summary>
    ''' se usa para mostrar mensaje de validacion cuando ya existe la glosa
    ''' </summary>
    ''' <remarks></remarks>
    CodigoGlosaYaExiste
    ''' <summary>
    ''' se usa para el mensaje de validacion de valores a glosar y valor factura
    ''' </summary>
    ''' <remarks></remarks>
    ValorGlosaMayorValorFactura
    ''' <summary>
    ''' se usa para mostrar mensaje de que el valor a reiterar no se ha mayor al valor glosado
    ''' </summary>
    ''' <remarks></remarks>
    ValorReiteracionMayorValorGlosado




#End Region

#Region "Frontal de conciliacion"

    ''' <summary>
    ''' Mensaje que indica que el cliente consultado no existe
    ''' </summary>
    ClienteNoExite
    ''' <summary>
    ''' No se encuentra la conciliacion buscada
    ''' </summary>
    ConciliacionNoExiste
    ''' <summary>
    ''' Texto para el boton de participantes cuando es agregar
    ''' </summary>
    TextoBotonAgregarParticipante
    ''' <summary>
    ''' Texto para el boton de participantes cuando es modificar
    ''' </summary>
    TextoBotonModificarParticipante
    ''' <summary>
    ''' La factura ya existe en la conciliacion
    ''' </summary>
    FacturaYaExiste
    ''' <summary>
    ''' La factura no existe al momento de agregarla a la conciliacion
    ''' </summary>
    FacturaNoExisteAlAgregar
    ''' <summary>
    ''' La lista de facturas en la conciliación no puede ser vacia, debe agregar al menos una
    ''' </summary>
    DetalleConciliacionVacia
    ''' <summary>
    ''' Pregunta si desea eliminar la factura de la rejilla
    ''' </summary>
    EliminarFacturaRejilla
    ''' <summary>
    ''' Se usa para el titulo del grupo de detalle de facturas
    ''' </summary>
    TituloDetalleFactura
    ''' <summary>
    ''' Se usa para el titulo del grupo de conciliación detalle factura
    ''' </summary>
    TituloConciliacionDetalleFactura
    ''' <summary>
    ''' Se usa para informar al usario que la factura que esta tratando de
    ''' conciliar no se encuentra persistida aun
    ''' </summary>
    FacturaSinGuardar
    ''' <summary>
    ''' Indica que la conciliacion no se puede guardar sin al menos tener un detalle
    ''' </summary>
    ConciliacionSinDetalles
    ''' <summary>
    ''' Mensaje del label cuando se crea una nueva conciliacion
    ''' </summary>
    NuevaConciliacionLabel
    ''' <summary>
    ''' Se le indica al usuario que no ha completado los datos para poder guardar
    ''' los movimientos del detalle que esta conciliando
    ''' </summary>
    FaltanDatosParaConciliar
    ''' <summary>
    ''' Se le indica al usuario que aun no se ha conciliado todos los detalles
    ''' y por tanto no se puede confirmar
    ''' </summary>
    FaltanDetallesPorConciliar
    ''' <summary>
    ''' Se le indica al usuario que aun no se ha conciliado todas las facturas
    ''' y por eso no se puede confirmar
    ''' </summary>
    FaltanFacturasPorConciliar
    ''' <summary>
    ''' Pregunta al usuario si desea confirmar la factura por el valor X
    ''' </summary>
    ConciliacionConfirmarFactura
    ''' <summary>
    ''' Se le indica al usuario que hay facturas que aún no se persisten
    ''' </summary>
    FaltanFacturasPorPersistir
    ''' <summary>
    ''' Se le indica al usuario que debe seleccionar un nit de tercero 
    ''' para poder realizar la accion de pegado de facturas desde la clipboard
    ''' </summary>
    DebeSeleccionarTercero
    ''' <summary>
    ''' Texto del menu contextual de la rejilla de participantes
    ''' </summary>
    MenuEditarParticipante
    ''' <summary>
    ''' Texto del menu contextual de la rejilla de participantes
    ''' </summary>
    MenuEliminarParticipante
    ''' <summary>
    ''' Texto del menu contextual de la rejilla de detalles de conciliacion
    ''' </summary>
    MenuPegarFacturaConciliacion
    ''' <summary>
    ''' Texto del menu contextual de la rejilla de detalles de conciliacion
    ''' </summary>
    MenuConciliarFactura
    ''' <summary>
    ''' Texto del menu contextual de la rejilla de detalles de conciliacion
    ''' </summary>
    MenuEliminarFacturaConciliacion
    ''' <summary>
    ''' Texto del menu contextual de la rejilla de detalles de factura
    ''' </summary>
    MenuConciliarDetalle

#End Region

#Region "Frontal de devolución"
    ''' <summary>
    ''' No se encuentra la devolución buscada
    ''' </summary>
    DevolucionNoExiste
    ''' <summary>
    ''' Debe guardar primero el radicado para poder confirmar
    ''' </summary>
    RadicadoSinGuardar
    ''' <summary>
    ''' Debe guardar la devolución para poder ir al detalle radicado
    ''' </summary>
    GuardarDevolucion
#End Region

#Region "Funcional Tipo Pensionado"
    ''' <summary>
    ''' Se usa para informar que debe seleccionar un pais para poder eliminar
    ''' </summary>
    SeleccioneUnTipoPensionado
#End Region

#Region "Funcional Centro de trabajo"
    ''' <summary>
    ''' Se usa para informar que debe seleccionar un pais para poder eliminar
    ''' </summary>
    SeleccioneUnCentroTrabajo
#End Region

#Region "Funcional Riesgos profesionales"
    ''' <summary>
    ''' Se usa para informar que debe seleccionar un riesgo profesional para poder eliminar
    ''' </summary>
    SeleccioneUnRiesgoProfesional
#End Region

#Region "Funcional Discapacidades"
    ''' <summary>
    ''' Se usa para informar que debe seleccionar una discapacidad para poder eliminar
    ''' </summary>
    SeleccioneUnaDiscapacidad
#End Region


#Region "Funcional Tipo Contribuyente"
    ''' <summary>
    ''' Se usa para informar que debe seleccionar un tipo de contribuyente para poder eliminar
    ''' </summary>
    SeleccioneUnTipocontribuyente
#End Region

#Region "Funcional Tipo Estudio"
    ''' <summary>
    ''' Se usa para informar que debe seleccionar un tipo de estudio para poder eliminar
    ''' </summary>
    SeleccioneUnTipoestudio

#Region "Jerarquia de estudios"
    Primaria
    Secundaria
    Tecnico
    Universitario
    Postgrado
#End Region

#End Region

#Region "Funcional Grupo de Contratos"
    ''' <summary>
    ''' Se usa para informar que debe seleccionar un grupo de contratos para poder eliminar
    ''' </summary>
    SeleccioneUnGrupoContrato
#End Region

#Region "Funcional Terceros"
    ''' <summary> 
    ''' Se usa para informar que debe seleccionar un tercero para poder eliminarla
    ''' </summary>
    SeleccioneUnTercero
#End Region
#Region "Funcional Tipo de Contrato"
    ''' <summary> 
    ''' Se usa para informar que debe seleccionar un tipo de contrato para poder eliminarla
    ''' </summary>
    SeleccioneUnTipodeContrato
#End Region
#Region "Funcional Tipo de Contrato"
    ''' <summary> 
    ''' Se usa para informar que debe seleccionar un centro de estudio para poder eliminarla
    ''' </summary>
    SeleccioneUnCentrodeEstudio
#End Region
#Region "Funcional Tipo de Contrato"
    ''' <summary> 
    ''' Se usa para informar que debe seleccionar una plantilla de contrato para poder eliminarla
    ''' </summary>
    SeleccionePlantilladeContrato
#End Region
#Region "Funcional Cargo"
    ''' <summary>
    ''' Se usa para informar que debe seleccionar un cargo para poder eliminar
    ''' </summary>
    SeleccioneUnCargo
#End Region
#Region "Funcional Retenciones"
    ''' <summary> 
    ''' Se usa para informar que debe seleccionar una plantilla de contrato para poder eliminarla
    ''' </summary>
    SeleccioneRetencion
#End Region
#Region "Funcional Tipo de Vinculacion"
    ''' <summary> 
    ''' Se usa para informar que debe seleccionar un tipo de vinculacion para poder eliminarla
    ''' </summary>
    SeleccioneUnTipodeVinculacion
#End Region
#Region "Funcional Unidad de Tiempo"
    ''' <summary> 
    ''' Se usa para informar que debe seleccionar una unidad de tiempo para poder eliminarla
    ''' </summary>
    SeleccioneUnaUnidaddeTiempo
#End Region

#Region "Frontal de coordinación"

    ''' <summary>
    ''' Texto del menu contextual de acciones sobre la rejilla de oficios
    ''' </summary>
    MenuDetalleDeOficio
    ''' <summary>
    ''' Texto del menu contextual de acciones sobre la rejilla de facturas
    ''' </summary>
    MenuDetalleDeFactura
    ''' <summary>
    ''' Se usa para el titulo en el grupo de detalle de oficio
    ''' </summary>
    TituloDetalleDeOficioCoordinacion
    ''' <summary>
    ''' Se usa para el grupo de detalle de factura
    ''' </summary>
    TituloDetalleDeFacturaCoordinacion

#End Region
#Region "Frontal de evaluación"

    ''' <summary>
    ''' Datos del responsable que bloquea una factura
    ''' </summary>
    ResponsableFacturaBloqueda

#End Region
#Region "Frontal de plantilla de turno"
    ''' <summary>
    ''' Se usa para informar que minimo debe agregar un concepto feriado
    ''' </summary>
    ''' <remarks></remarks>
    MinimoUnFeriado
    ''' <summary>
    ''' Se usa para informar que minimo debe agregar un concepto ordinario
    ''' </summary>
    ''' <remarks></remarks>
    MinimoUnOrdinario
    ''' <summary>
    ''' se usa para informar que el rango de tiempo debe ser con horas completas
    ''' </summary>
    ''' <remarks></remarks>
    RangoHorasCompletas
    ''' <summary>
    ''' Se usa para informar que conceptos ordinarios selecciono
    ''' </summary>
    ''' <remarks></remarks>
    ConceptoOrdinario
    ''' <summary>
    ''' Se usa para informar que conceptos feriados selecciono
    ''' </summary>
    ''' <remarks></remarks>
    ConceptoFeriado
    ''' <summary>
    ''' Se usa para informar el rango de horas debe ser mayor a 0
    ''' </summary>
    ''' <remarks></remarks>
    TiempoMayorCero
    ''' <summary>
    ''' Se usa para informar que ya existe un appointment en el rango de horas seleccionados
    ''' </summary>
    ''' <remarks></remarks>
    AppointmentExiste
#End Region
#Region "Funcional Autorizacion de conceptos"
    ''' <summary> 
    ''' Se usa para informar que se registro correctamente los conceptos autorizados
    ''' </summary>
    ProcesoRealizadoConExito

    ''' <summary>
    ''' Se usa para informar que el concepto seleccionado no se puede quitar porque esta autorizado a nivel de grupo
    ''' </summary>
    ConceptoAutorizadoPorGrupo
#End Region

#Region "Funcional Empleado"

    MinimoQuinceLaborar

#Region "Tipos de identificacion"
    CedulaCiudadania
    CedulaExtranjeria
    TarjetaIdentidad
    RegistroCivil
    Pasaporte
    AdultoSinIdentificacion
    MenorSinIdentificacion
#End Region

#Region "Tipos de cesantias"
    NoAplica
    Tradicional
    TradicionalMensual
    Consignado
    ConsignadoMensual
    Ley33
#End Region

#Region "Sindicato"
    Ninguno
    Convencionado
    Sindicalizado
    PactoColectivo
#End Region

#Region "Estado civil"
    Soltero
    Casado
    Divorciado
    Viudo
    UnionLibre
#End Region

#Region "Estado estudios"
    EstudiaActualmente
    EstudioInterrumpido
    EstudioTerminado
    Graduado
#End Region

#Region "Genero"
    Masculino
    Femenino
    Otro
#End Region

#Region "Libreta Militar"
    LibretaPrimera
    LibretaSegunda
#End Region

#Region "SI/NO"
    Si
    No
#End Region

#Region "Activo/Suspendido"
    Activo
    Suspendido
#End Region

#Region "Tipo de Retencion"
    ExentoRetencion
    HaceRetencion
    Autoretenedor
#End Region

#Region "Tipos de educacion"
    Formal
    NoFormal
#End Region

#Region "Validacion de fechas"
    FechaXMenorQueY
#End Region


#End Region

#Region "Funcional de Cuadro de turno"
    SobreescribirHorario
    Manana
    MananaTarde
    Tarde
    MananaNoche
    TardeNoche
    Noche
    Incapacidad
    Licencia
    Sancion
    EmpleadosLiquidados
    EmpleadoLiquidado
    ErrorHour
    ErrorScheduleDetailExisting
    ErrorMaximumTotalHourNumber
    ErrorWithoutContract
    ErrorIncapacidad
    ErrorSancion
    ErrorLicencia
    EventoAprobado
    FechaInicioMenor
    RangoFrecuenciaAlto
#End Region

#Region "Tipo de empleados"
    SeleccioneTipoEmpleado
#End Region

#Region "Funcional contrato"

#Region "General"
    Mas
#End Region

#Region "Duracion del contrato"
    Indefinido
    Ano
    Mes
    Dia
#End Region

#Region "Tipo de salario"
    Fijo
    Integral
    Variable
#End Region

#Region "Periodo de pago"
    Mensual
    Quincenal
#End Region

#Region "Tipo de pago"
    Cheque
    Efectivo
    Consignacion
    Desprendible
#End Region

#Region "Tipo de cuenta bancaria"
    Ahorros
    Corriente
#End Region

#Region "Tipo de empleado"
    Administrativo
    Asistencial
#End Region

#Region "Campos del contrato"
    Id
    RowType
    InitialContractNumber
    Position
    Group
    FunctionalUnit
    ContractType
    JobBondingType
    JobBondingDate
    ContractInitialDate
    ContractEndingDate
    BasicSalary
    Status
    RetirementReason
    RetirementDate
    PaymentPeriod
    SalaryType
    PaymentType
    TrialPeriod
    TrialPeriodTime
    TrialPeriodSalaryPercentage
    AutoRenew
    ContractCreationDate
    Valid
    Notes
    Bank
    BankAccountNumber
    BankAccountType
#End Region

#Region "Tipo de Registro"
    ContratoBase
    Novedad
#End Region

#Region "Clase de contrato"
    ClaseContratoOtros
    ClaseContratoAprendizaje
    ClaseContratoLaboralFijo
    ClaseContratoLaboralIndefinido
#End Region

#Region "Razon de Modificacion de contrato"
    SeleccioneRazonMoficacionContrato
#End Region

#Region "tipos de fondos"
    Salud
    Pension
    Riesgos
    Cesantias
    CajaCompensacion
#End Region


    ContratosNo2Vigentes
    ContratosEnRangoOtroContrato
    FondoArlObligatorio
    FondoSaludObligatorio
    FondoPensionesObligatorio
    ContratosFechaMinimaPermitida
    ContratosFechaMaximaContratoFijo
    ContratoRangoDeFechas
    FondoYaSeEncuentra
    ContratosSalarioVsCargo
    ContratosRenovacion
    FondoFechasEntreAfiliaciones
    FondoFechasEnRangoFondo
    ContratosSinCambio
    FondoFechaMinimaContrato
    FondoAccionesInactivos
    ContratosSalarioVsAnteriorSalario
    ContratosCrearNuevo
    ContratosConCuadroTurnos
    ContratosEliminarAdvertencia
    ContratosConNominaPagada


#End Region

#Region "Unidad Funcional"

    ''' <summary>
    ''' Se usa para informar que el usuario que desea agregar ya se encuentracomo responsable
    ''' </summary>
    ''' <remarks></remarks>
    UsuarioYaAgregado
    ''' <summary>
    ''' Se usa para informar que debe seleccionar un usuario
    ''' </summary>
    ''' <remarks></remarks>
    SeleccioneUsuario

#End Region

#Region "Vacaciones"

    ''' <summary>
    ''' Se utiliza para informar al usuario que no puede solicitar mas dias de vacaciones que los que tiene disponible
    ''' </summary>
    ''' <remarks></remarks>
    NoPuedeSolicitarDias

    ''' <summary>
    ''' Se utiliza para informar que el usuario no puede solicitar mas de X cantidad de dias
    ''' </summary>
    ''' <remarks></remarks>
    EmpleadoNoPuedeDias

    ''' <summary>
    ''' Se utiliza para informar que el empleado no tiene dias pendientes
    ''' </summary>
    ''' <remarks></remarks>
    EmpleadoNotieneDiasPendientes

    ''' <summary>
    ''' Variable para informar al usuario que no tiene empleados seleccionados
    ''' </summary>
    ''' <remarks></remarks>
    NoHayEmpleadosSeleccionados

    ''' <summary>
    ''' Variable para informar al usuario que el empleado esta en vacaciones
    ''' </summary>
    ''' <remarks></remarks>
    EmpleadoEnVacaciones

    ''' <summary>
    ''' Vaiable para el estado esperando pago
    ''' </summary>
    ''' <remarks></remarks>
    EsperandoPago

    ''' <summary>
    ''' Varible para estado pagas
    ''' </summary>
    ''' <remarks></remarks>
    Pagas

    ''' <summary>
    ''' Variable para informar que si esta seguro de cancelar vacaciones
    ''' </summary>
    ''' <remarks></remarks>
    EstaSeguroCancelarVacacion

    ''' <summary>
    ''' Variable para informar que si esta seguro de hacer reingreso
    ''' </summary>
    ''' <remarks></remarks>
    EstaSeguroReingresoVacacion

    ''' <summary>
    ''' Variable para informar la accion Cacelar Solicitud
    ''' </summary>
    ''' <remarks></remarks>
    CancelarSolicitud

    ''' <summary>
    ''' Variable para informar la accion Ingreso Forzoso
    ''' </summary>
    ''' <remarks></remarks>
    IngresoForzoso

    ''' <summary>
    ''' Se usa para informar que se ha cancelado la solicitud correctamente
    ''' </summary>
    ''' <remarks></remarks>
    SolicitudCanceladaCorrectamente

    ''' <summary>
    ''' Se usa para informar que se ya se han pagado la solicitud de vacaciones
    ''' </summary>
    ''' <remarks></remarks>
    SolicitudYaEstanPagas

#End Region

#Region "LiquidacionNomina"

    ''' <summary>
    ''' Se utiliza para informar al usuario que no se generó una Liquidación de Nómina
    ''' </summary>
    ''' <remarks></remarks>
    NoGeneraLiquidacion

    ''' <summary>
    ''' Se utiliza para informar al usuario que no existe un empleado con ese número de identificación Liquidación de Nómina
    ''' </summary>
    ''' <remarks></remarks>
    NoExisteEmpleado


#End Region

#Region "Liquidación de Cesantías"

    ''' <summary>
    ''' Se utiliza para informar que no hay nominas liquidadas
    ''' </summary>
    ''' <remarks></remarks>
    NoHayNominasLiquidadas

    ''' <summary>
    ''' Se utiliza para informar que no hay grupos seleccionados
    ''' </summary>
    ''' <remarks></remarks>
    NoHayGruposSeleccionados

    ''' <summary>
    ''' Se Utiliza para informar que el empleado no tiene nominas liquidadas
    ''' </summary>
    ''' <remarks></remarks>
    EmpleadoNoTieneNominasLiquidadas

    ''' <summary>
    ''' Se Utiliza para informar que el empleado no tiene contrato de tipo laboral, que pueda liquidar cesantías
    ''' </summary>
    ''' <remarks></remarks>
    EmpleadoNoTieneContratoLaboral

    ''' <summary>
    ''' Se Utiliza para informar que el empleado ya tiene liquidadas cesantías en un determinado periodo
    ''' </summary>
    ''' <remarks></remarks>
    EmpleadoYaTieneCesantiasLiquidadas

    ''' <summary>
    ''' Se Utiliza para informar que el empleado no tiene contrato para liquidar determinado periodo de cesantías
    ''' </summary>
    ''' <remarks></remarks>
    EmpleadoNoTieneContrato

    ''' <summary>
    ''' Informa que este empleado ha liquidado cesantía anual 
    ''' </summary>
    ''' <remarks></remarks>
    EmpleadoYaTieneCesantiaAnualLiquidada
#End Region

#Region "Funcional Incapacidades (Inability)"
    ''' <summary>
    ''' se usa para informar al usuario que la cantidad de dias debe ser mayor a 0
    ''' </summary>
    ''' <remarks></remarks>
    CantidadDiasNovedad

    ''' <summary>
    ''' se usa para informar que el empleado no existe
    ''' </summary>
    ''' <remarks></remarks>
    EmpleadoNoExiste

    ''' <summary>
    ''' Item del combo Postular valor
    ''' </summary>
    ''' <remarks></remarks>
    Postular23
    ''' <summary>
    ''' Item del combo Postular valor
    ''' </summary>
    ''' <remarks></remarks>
    PostularPatrono
    ''' <summary>
    ''' Item del combo Postular valor
    ''' </summary>
    ''' <remarks></remarks>
    PostularNomina
    ''' <summary>
    ''' Item del combo Postular valor
    ''' </summary>
    ''' <remarks></remarks>
    Postular90Dias
    ''' <summary>
    ''' Item del combo Postular valor
    ''' </summary>
    ''' <remarks></remarks>
    Postular3CienPorciento
    ''' <summary>
    ''' Item del combo Incapacidad
    ''' </summary>
    ''' <remarks></remarks>
    IncapacidadAmbulatoria
    ''' <summary>
    ''' Item del combo Incapacidad
    ''' </summary>
    ''' <remarks></remarks>
    IncapacidadHospitalaria
    ''' <summary>
    ''' Item del combo Incapacidad
    ''' </summary>
    ''' <remarks></remarks>
    IncapacidadMaternidad
    ''' <summary>
    ''' Item del combo Incapacidad
    ''' </summary>
    ''' <remarks></remarks>
    IncapacidadRiesgos
    ''' <summary>
    ''' Se usa para informar al usuario que el grupo no posee parametros de nomina
    ''' </summary>
    ''' <remarks></remarks>
    GrupoSinParametros
    ''' <summary>
    ''' Se usa para informar que aun faltan campos por digilenciar para por guardar
    ''' </summary>
    ''' <remarks></remarks>
    FaltaCampo
    ''' <summary>
    ''' Se usa para informar que se va crear novedad
    ''' </summary>
    ''' <remarks></remarks>
    CrearNovedad
    ''' <summary>
    ''' Se usa para informar que se va editar novedad
    ''' </summary>
    ''' <remarks></remarks>
    EditarNovedad
    ''' <summary>
    ''' Se usa para informar que se va dar prorroga a una novedad
    ''' </summary>
    ''' <remarks></remarks>
    ProrrogaNovedad
    ''' <summary>
    ''' Se usa para informar que es una novedad inicial
    ''' </summary>
    ''' <remarks></remarks>
    Inicial
    ''' <summary>
    ''' Se usa para informar que es una novedad de prorroga
    ''' </summary>
    ''' <remarks></remarks>
    Prorroga

    ''' <summary>
    ''' se usa para informar al usuario que no puede ni eliminar ni editar la novedad ya que solo es permitido para la ultima prorroga
    ''' </summary>
    ''' <remarks></remarks>
    NoPuedeAccionPorUltimaProrroga
    ''' <summary>
    ''' se usa para informar al usuario de que la fecha de inicio prorroga no puede ser mayor ala del contrato
    ''' </summary>
    ''' <remarks></remarks>
    NoPuedeFechaInferiorContrato
    ''' <summary>
    ''' se usa para informar al usuario de que la fecha de inicio prorroga no puede ser mayor ala del contrato
    ''' </summary>
    ''' <remarks></remarks>
    NoPuedeFechaSuperiorContrato
    ''' <summary>
    ''' Se usa para informar al usuario que la fecha fin de la novedad no puede ser mayor a la fecha fin del contrato
    ''' </summary>
    ''' <remarks></remarks>
    NoPuedeFechaFinSuperiorContrato

#End Region

#Region "Control Busqueda Avanzada"

    OcultarFiltros
    OcultarListaFacturas
    SeleccionesCamposParaFiltros
    LabelNumeroDeRegistro

#End Region

#Region "Modulo Mantenimiento"

#Region "Funcional Aseguradoras"
    SeleccioneAseguradora
#End Region
#Region "Funcional Fabricantes"
    SeleccioneFabricantes
#End Region
#Region "Funcional Tipo Equipos"
    SeleccioneTipoEquipos
#End Region
#Region "Funcional Tipos Poliza"
    SeleccioneTipoPoliza
#End Region
#Region "Funcional Poliza"
    SeleccionePoliza
#End Region
#Region "Funcional Sucursales"
    SeleccioneSucursal
#End Region
#Region "Funcional Accesorios"
    SeleccioneAccesorios
#End Region
#Region "Funcional Areas"
    SeleccioneArea
#End Region
#Region "Funcional Pisos"
    SeleccionePiso
#End Region
#Region "Funcional Torres"
    SeleccioneTorre
#End Region
#Region "Funcional Consumibles"
    SeleccioneConsumibles
#End Region
#Region "Funcional Habitaciones"
    SeleccioneHabitaciones
#End Region
#Region "Funcional Tipo Inventario"
    SeleccioneTipoInventario
#End Region
#Region "Funcional Responsable"
    SeleccioneResponsable
#End Region
#Region "Funcional Partes"
    SeleccioneParte
#End Region
#Region "Funcional Equipos"
    SeleccioneEquipo
#End Region
#Region "Funcional Unidad Medidas"
    SeleccioneUnidadMedida
#End Region
#Region "Funcional Registro Tecnico"
    SeleccioneRegistroTecnico
#End Region



#End Region

#Region "Sistema Documental"

#Region "Funcional MetaData"

    ErrorGuardarDocumento
    BotonGuardarDocumento
    RadioGrupoDocumento
    TituloMetada

#End Region

#Region "Funcional Configuración Metadata"
    NoExisteArchivador
#End Region

#Region "Funcional Digitalización"

    PaginaDigitalizacion
    DeDigitalización
    SeleccioneDispositivo
    EscaneoDuplex
    TituloDigitalizacion

#End Region

#Region "Funcional Adjuntar"

    TituloAdjuntar

#End Region

#Region "CtrBarraBotones"

    SinArchivador
    SinDocumentos
    SinPermisoAbrirDocumentos

#End Region

#End Region

#Region "Traslado Cobro Jurídico"
    NoExisteTrasladoCobroJuridico
    FacturaYaExisteTrasladoCobroJuridico
#End Region

#Region "Weather"
    Weathertornado
    Weathertropicalstorm
    Weatherhurricane
    Weatherseverethunderstorms
    Weatherthunderstorms
    Weathermixedrainandsnow
    Weathermixedrainandsleet
    Weathermixedsnowandsleet
    Weatherfreezingdrizzle
    Weatherdrizzle
    Weatherfreezingrain
    Weathershowers
    Weathersnowflurries
    Weatherlightsnowshowers
    Weatherblowingsnow
    Weathersnow
    Weatherhail
    Weathersleet
    Weatherdust
    Weatherfoggy
    Weatherhaze
    Weathersmoky
    Weatherblustery
    Weatherwindy
    Weathercold
    Weathercloudy
    Weathermostlycloudynight
    Weathermostlycloudyday
    Weatherpartlycloudynight
    Weatherpartlycloudyday
    Weatherclearnight
    Weathersunny
    Weatherfairnight
    Weatherfairday
    Weathermixedrainandhail
    Weatherhot
    Weatherisolatedthunderstorms
    Weatherscatteredthunderstorms
    Weatherscatteredshowers
    Weatherheavysnow
    Weatherscatteredsnowshowers
    Weatherpartlycloudy
    Weatherthundershowers
    Weathersnowshowers
    Weatherisolatedthundershowers

#End Region

#Region "Sitema Mensajeria"
    MessagesTitle
    MeesagesRead
    MessagesWriting
    MessagesGroupInvitation
    MessagesGroupLeave
#End Region

#Region "MetaData Frontales"

    FrmCustomersMetaData
    FrmCustomersMetaDataTitle
    DocumentsMetaDataTitle
    FrmResponsibleMetaData
    FrmResponsibleMetaDataTitle
    FrmJustificationTemplateMetaData
    FrmJustificationTemplateMetaDataTitle
    FrmHealthCenterMetaData
    FrmHealthCenterMetaDataTitle
    FrmCompanyMetaData
    FrmCompanyMetaDataTitle
    FrmBankMetaData
    FrmBankMetaDataTitle
    FrmWorkCenterMetaData
    FrmWorkCenterMetaDataTitle
    FrmCityMetaData
    FrmCityMetaDataTitle
    FrmBranchOfficeMetaData
    FrmBranchOfficeMetaDataTitle
    FrmStudyTypeMetaData
    FrmStudyTypeMetaDataTitle
    FrmStudyCenterMetaData
    FrmStudyCenterMetaDataTitle
    FrmRetirementReasonMetaData
    FrmRetirementReasonMetaDataTitle
    FrmProfessionMetaData
    FrmProfessionMetaDataTitle
    FrmProfessionalRiskMetaData
    FrmProfessionalRiskMetaDataTitle
    FrmPositionLevelMetaData
    FrmPositionLevelMetaDataTitle
    FrmPositionMetaData
    FrmPositionMetaDataTitle
    FrmPensionaryTypeMetaData
    FrmPensionaryTypeMetaDataTitle
    FrmLanguageMetaData
    FrmLanguageMetaDataTitle
    FrmKinshipMetaData
    FrmKinshipMetaDataTitle
    FrmDepartmentMetaData
    FrmDepartmentMetaDataTitle
    FrmCountryMetaData
    FrmCountryMetaDataTitle
    FrmDisabilityMetaData
    FrmDisabilityMetaDataTitle
    FrmThirdPartyMetaData
    FrmThirdPartyMetaDataTitle
    FrmJobBondingTypeMetaData
    FrmJobBondingTypeMetaDataTitle
    FrmGroupMetaData
    FrmGroupMetaDataTitle
    FrmFundMetaData
    FrmFundMetaDataTitle
    FrmFunctionalUnitMetaData
    FrmFunctionalUnitMetaDataTitle
    FrmGroupsMetaData
    FrmGroupsMetaDataTitle
    FrmCompanyPayrollMetaData
    FrmCompanyPayrollMetaDataTitle
    FrmConceptsMetaData
    FrmConceptsMetaDataTitle
    FrmTimeUnitMetaData
    FrmTimeUnitMetaDataTitle
    FrmContractGroupMetaData
    FrmContractGroupMetaDataTitle
    FrmContractModificationReasonMetaData
    FrmContractModificationReasonMetaDataTitle
    FrmContractTemplateMetaData
    FrmContractTemplateMetaDataTitle
    FrmContractTypeMetaData
    FrmContractTypeMetaDataTitle
    FrmContributorTypeMetaData
    FrmContributorTypeMetaDataTitle
    FrmCostCenterMetaData
    FrmCostCenterMetaDataTitle
    FrmEducationLevelsMetaData
    FrmEducationLevelsMetaDataTitle
    FrmEmployeeTypeMetaData
    FrmEmployeeTypeMetaDataTitle
    FrmAuthorizationConceptMetaData
    FrmAuthorizationConceptMetaDataTitle
    FrmKindsAgrementsMetaData
    FrmKindsAgrementsMetaDataTitle
    FrmDocumentTypeMetaData
    FrmDocumentTypeMetaDataTitle
    FrmFunctionalAgreementsMetaData
    FrmFunctionalAgreementsMetaDataTitle
    FrmRetentionConceptMetaData
    FrmRetentionConceptMetaDataTitle
#End Region

#Region "Funcional de Convenios"
    PaidValueExceeds
    TipoPorNomina
    TipoManual
    TipoPorArchivo
#End Region

#Region "Funcional Plantillas de Justificación"
    PlantillaExistente
    PlantillaPorDefecto
#End Region

End Enum


''' <summary>
''' Enumeracion con los procesos de la gloas
''' </summary>
Public Enum _GlosasProcess
    Objection = 1
    ObjectionEvaluation = 2
    ObjectionCoordination = 3
    Reiteration = 4
    ReiterationEvaluation = 5
    ReiterationCoordination = 6
    Conciliation = 7

End Enum

''' <summary>
''' Enumeracion con los lenguajes para los diccionarios
''' </summary>
Public Enum _LanguageDictionaries
    Spanish = 0
    English = 1
End Enum

''' <summary>
''' Enumeracion que sirve para establecer  los botones que tiene los mensajes indigo
''' </summary>
Public Enum _Botones
    Ok = 0
    SiNo = 1
    AceptarCancelar = 2
    Aceptar = 3
    Cancelar = 4
End Enum

''' <summary>
''' Enumeracion que sirve para establecer el tipo de mensaje indigo
''' </summary>
Public Enum _Icono
    Errores = 0
    Advertencia = 1
    Informacion = 2
    pregunta = 3
    Ninguno = 4
End Enum

''' <summary>
''' Enumeracion que sirve para establecer si el manseje es un show dialog o un mensaje slide
''' </summary>
Public Enum _TipoMensaje
    Slide = 0
    ShowDialog = 1
End Enum

Public Enum _Confirm
    Confirmed
    NotConfirmed
    Invalidate
    OfficeResponse
    Vacio
End Enum

Public Enum _AppBarButtons
    ''' <summary>
    ''' Opcion para mostrar los botones de desanclar del menu y el boton mas grande
    ''' </summary>
    MasGrandeDesanclar = 0

    ''' <summary>
    ''' Opcion para mostrar los botones de desanclar del menu y el boton mas pequeño
    ''' </summary>
    MasPequeñoDesanclar = 1

    ''' <summary>
    ''' Opcion para mostrar los botones de desanclar del menu y borrar seleccion
    ''' </summary>
    DesanclarBorraSeleccion = 2

    ''' <summary>
    ''' Opcion para mostrar el Boton Nombre del grupo
    ''' </summary>
    CambiarNombreGrupo = 3

    ''' <summary>
    ''' Opcion para mostrar el Boton Anclar a mi menu
    ''' </summary>
    AnclarMiMenu = 4

End Enum

''' <summary>
''' Enumeracion Para establecer que xml se quiere obtener
''' </summary>
Public Enum _eDataXml
    ''' <summary>
    ''' xml que tiene los permisos que dispone cada formulario
    ''' </summary>
    XMLPermissionForm
    ''' <summary>
    ''' xml que tiene la asociacion de los formulario con los modulos
    ''' </summary>
    XMLModuleForm
    ''' <summary>
    ''' xml que tiene los modulos 
    ''' </summary>
    XMLModule
    ''' <summary>
    ''' xml que tiene los formularios
    ''' </summary>
    XMLFormERP
    ''' <summary>
    ''' xml que tiene los grupos de modulos
    ''' </summary>
    XMLModuleGroups
    ''' <summary>
    ''' xml que tiene los WOEID de las principales ciudades de colombia
    ''' </summary>
    XMLWoeidCities
    ''' <summary>
    ''' xml que tiene los meses
    ''' </summary>
    XMLMonths
End Enum

''' <summary>
''' Enumeracion que contiene las acciones que se pueden realizar con el contrato
''' </summary>
Public Enum _ContractActions
    NewContractWithPayments
    NewContractWithoutPayments
    DeleteContract
    EditContract
    'ModifyContract
    EndingContract
    NoAction
End Enum

''' <summary>
''' Enumeración que contiene la acciones permitidas por los formularios
''' </summary>
Public Enum _PermissionsActionsForm

    Eliminar = 1
    Guardar = 2
    Actualizar = 3
    Confirmar = 7
    Anular = 8
    Customizar = 11
    RedesSociales = 12
    Documentos = 13
    Comunicación = 14
    Adjuntar = 16
    Digitalizar = 17
    Abrir = 18
    Auditoria = 19
    Consultar = 40
    Visible = 41
    EditarHorario = 42
    RegistrarEvento = 43
    AprobarEvento = 44

End Enum

#End Region