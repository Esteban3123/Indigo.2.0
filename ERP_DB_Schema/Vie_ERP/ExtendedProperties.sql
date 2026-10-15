EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado (1 - Activo, 0 - Inactivo)';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifica el Usuario';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifica la Unidad Operativa';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifica el Contenedor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifica la tabla del usurio de la Unidad Operativa';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de usuario';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de unidad funcional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de control de notificación, 1:Alert windows, 2:Toast notification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Perfil. Depende de dashboard por defecto = 0, Especialista o Hemocomponente = 1, Académico = 10';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si muestra el tema seleccionado o la configuración de tema por defecto';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de rol';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si carga o no el layout del reporte personalizado ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de unidad operativa';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de centro de atención';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si carga o no los videos en el login de la aplicación, aplica para el login normal, no aplica para login B2C.';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lenguaje y cultura. es-CO, en-US';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la tabla';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de grupo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de unidad funcional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de grupo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración predeterminada';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de compañia, compañia por defecto.';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dashboard por defecto. Ninguno = 0, DashBoard_Medico = 1, DashBoard_Enfermeria = 2, DashBoard_Interconsultas = 3, DashBoard_Terapias = 4, DashBoard_Laboratorio = 5, DashBoard_Imagenologia = 6, DashBoard_Patologias = 7, DashBoard_ServiciosApoyo = 8, DashBoard_MedicoInternos = 9';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ruta de reportes personalizados';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de centro de atención';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ciudad actual, corresponde a un identificador de una lista de ciudades embebidos en el archivo VieWoeidCities.xml. Acacías:368250,Aguachica:368157,Agustín Codazzi:350564,Apartadó:368635,Arauca:368458,Arjona:368263,Armenia:368158,Baranoa:368265,Barrancabermeja:368159,Barranquilla:368151,Bello:368160,Bogotá:368148,Bucaramanga:368152,Buenaventura:368161,Buga:368182,Cajicá:368471,Calarcá:368185,Caldas:368186,Cali:368149,Candelaria:358206,Carepa:26797690,Cartagena:368153,Cartago:352579,Caucasia:368187,Cereté:368188,Chía:352956,Chigorodó:368781,Chinchiná:368191,Chiquinquirá:368285,Ciénaga:368162,Ciénaga de Oro:368194,Copacabana:368293,Corozal:353342,Cúcuta:368154,Dosquebradas:353700,Duitama:368196,El Banco:368197,El Carmen de Bolívar:368198,El Cerrito:368199,El Espinal:356391,Envigado:356359,Facatativá:368200,Florencia:368201,Floridablanca:368934,Fundación:368206,Funza:356558,Fusagasugá:368207,Garzón:368208,Girardot:368209,Girardota:368501,Girón:356678,Granada:368951,Ibagué:368155,Ipiales:368211,Itagüí:368212,Jamundí:368213,La Ceja:368322,La Dorada:368323,La Estrella:358787,La Plata:368214,Los Patios:361758,Madrid:361938,Magangué:368217,Maicao:368338,Malambo:368528,Manaure:369112,Manizales:368156,Marinilla:368531,Medellín:368150,Montelíbano:369150,Montería:368164,Necoclí:369174,Neiva:368165,Ocaña:368220,Orito:369191,Palmira:368166,Pamplona:368223,Pasto:368167,Pereira:368168,Piedecuesta:368225,Pitalito:368226,Planeta Rica:368228,Plato:368229,Popayán:368169,Pradera:368364,Puerto Asís:369266,Puerto Boyacá:368366,Quibdó:368554,Riohacha:368559,Rionegro:368232,Riosucio:368233,Sabanalarga:368563,Sabaneta:364997,Sahagún:365024,San Andrés:365117,San José del Guaviare:368571,San Marcos:368398,San Vicente del Caguán:369418,Santa Cruz de Lorica:368216,Santa Marta:368578,Santa Rosa de Cabal:366619,Santander de Quilichao:366683,Sincelejo:368171,Soacha:366928,Sogamoso:368238,Soledad:368239,Tame:369477,Tierralta:368424,Tuluá:368241,Tumaco:369509,Tunja:368172,Turbaco:368433,Turbo:368604,Uribia:368435,Valledupar:368173,Villa del Rosario:367929,Villamaría:367937,Villavicencio:368174,Yopal:368245,Yumbo:368246,Zipaquirá:368248,Zona Bananera:56125843';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ver formulario (1 - Si, 0 - No)';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de usuario:  0:StandardUser - 1: Administrador Empresa - 2: Administrador Tenant - 3: Administrador Global';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de usuarioLync';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo de usuario';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de inquilino';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado (1 - Activo, 0 - Inactivo)';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Rol';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Actualizar token';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define el Tipo de Perfil: 1= Administrativo 2= Asistencial';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Posición';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nota personal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ContraseñaLync';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contraseña';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación antiguo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Está bloqueado (1 - Si, 0 - No)';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifica la Persona';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la tabla Usuario';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'El codigo del grupo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Error en el recuento de contraseñas';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correo electronico, debe ser unico por usuario';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días a Cambiar Contraseña';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del Último Cambio de Contraseña';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha Expiración de la Cuenta';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la Interfaz';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cambiar la contraseña (1 - Si, 0 - No)';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DirecciónSingInLync';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de usuario: 1: Administrador Empresa - 2: Administrador Tenant - 3: Administrador Global';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de usuario';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Para controlar acceso concurrente';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de tenant al que pertenece el usuario';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del usuario en el tenant: 1:Activo, 2:Inactivo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de rol';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cargo del usuario';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se usa para el usuario de tipo "Administrador Empresa", para filtrar entre los tenant del usuario en cuales administra alguna compañia.';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el usuario esta bloqueado en el tenant';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la tabla';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de grupo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CódigoInterfaz';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de tenant';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de rol';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la tabla TenantRoll';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de inquilino';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del grupo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion  de inquilino';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificació de la tabla';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de dominio';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación  del  inquilino';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado (1 - Activo, 0 - Inactivo)';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Principal (1 - Si, 0 - No)';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla  Contenedor de inquilinos';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del contenedor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado: 1|Aprobacion jurídica, 2|Aprobación financiera, 3|Aprobación de operaciones';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Teléfono';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado: 1|Solicitud de aprovisionamiento, 2|Tenant activo, 3|Tenant suspendido, 4|Tenant inactivo ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'RepresentaciónLegal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de tenant';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la tabla';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Pais del tenant';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de nube, 1- Nube Publica, 2 - Nube Dedicada, 3 - Nube Privada, 4 - No Aplica';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'EmpresaNit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de nube';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ciudad';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de autenticación: 1|Federada O365, 2|Federada Gmail, 3|Federada Amazon, 4|Federada Live.com, 5|Federada Linkedin, 6|B2C AD Indigo ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de licenciamiento: 1|Por Usuario, 2|Por Maquina, 3|Por BTIu V1, 4|Por BTIu V2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion  de inquilino';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion  de suscripción';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion  del catálogo de productos';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de usuarios';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de dispositivos';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BTIuV2Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BTIuV1Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL del Servicio de XPO del ERP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Protocolo del Servicio de XPO del ERP. 0:basicHttp, 2:netTcp, 3:Ninguno';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL del servicio de notificación';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL del servicio de indexación del ERP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Protocolo del servicio de indexación del ERP. 0:basicHttp, 1:wsHttp, 2:netTcp, 3:Ninguno';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del configuracion de servicios';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'No se usa, disponible';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'No se usa, disponible';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave de la azure function GetSuscriptions';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave de la azure function LoginUserCompany';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave de la azure function GetApplicationSettingsByContainerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave de la azure function SaveUserConfiguration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave de la azure function GetProfesional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave de la azure function GetPerfilUbicacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL del servicio de entidades del ERP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Protocolo del servicio de entidades del ERP. 0:basicHttp, 1:wsHttp, 2:netTcp, 3:Ninguno';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Protocolo de los servicios web del EHR. 0:basicHttp, 2:netTcp, 3:Ninguno';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Protocolo de servicio web EHR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL del servicio de entidades del EHR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Protocolo del servicio de entidades del EHR. 0:basicHttp, 2:netTcp, 3:Ninguno';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL del servicio de sistema documental del ERP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Protocolo del servicio de sistema documental del ERP. 0:basicHttp, 1:wsHttp, 2:netTcp, 3:Ninguno';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL de aplicación de funciones';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de rol';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de rol';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la suite';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del producto';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de plataforma';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado (1 - Activo, 0 - Inactivo)';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado (1 - Activo, 0 - Inactivo)';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Teléfono';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de teléfono de identificación';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación Persona';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado 1.Registrado 2.Confimar';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo nombre';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo apellido';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de identificación';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Género';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de pila';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer apellido';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Huella dactilar';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cumpleaños';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de inquilino';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de Usuario';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Formulario de Identificación';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de acción (1 - Si, 0 - No)';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Acción';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'identificación Roll';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Formulario de Identificación';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de acción (1 - Si, 0 - No)';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Acción';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permiso (1 - Si, 0 - No)';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación Usuario';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación Unidad Operativa Por defecto';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'contenedor de identificación';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Administrador';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion del usuario de inquilino';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion del suscripción';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de suscripción';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del dispositivo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DispositivoMAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DispositivoIPV4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'sincronizado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado (1 - Activo, 0 - Inactivo)';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de grupo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ZEFLicenseName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave de licencia ZEF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de tenant para autenticación B2C';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del flujo de usuario de autenticación B2C';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL de cierre de sesión';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URI de redirección aceptada al devolver la respuesta de autenticación';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del flujo de usuario de autenticación B2C para restablecimiento de contraseña';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la tabla';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del ejecutable del aplicativo crystal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave de la azure function GetUserConfigurationByUserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave de la azure function GetUserConfigurationByUserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de aplicación registrada en Azure';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URLFunciónAplicación';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifica su formulario';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Formulario Identificación del ERP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado (1 - Activo, 0 - Inactivo)';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación Persona';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correo electrónico';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del pais segun INE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la tabla';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de bandera';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del pais segun INE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contenedor Virtual';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VerificaciónDígitoNit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cola de URL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contenedor transaccional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Teléfono';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de suscripción';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado (1 - Activo, 0 - Inactivo)';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de configuración del servicio';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'RepresentaciónLegal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Publicar evento';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define si la empresa es de produccion = True, o ambiente de pruebas = False';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Integración de Nómina';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cola de nombres';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'multiinquilino';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Integración con Lync';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Está sincronizado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contenedor de costos de interoperabilidad';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Talento Humano Integración';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Integración HIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contenedor HIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Glosas Integración';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contenedor fundamental';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Imagen De Pie';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contenedor Documental';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dispensación Integración';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Separador decimal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de compañía';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'EmpresaNit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de nube';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del cliente';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ciudad';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sucursales';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ruta de recursos compartidos de archivos de Azure';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de arquitectura';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cola de arquitectura';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'dirección';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL del servicio de dispensación. - Funcional especifico para los frm de Dispensación automatica y manual realizados para FarmaQx';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del servidor o ip del servidor mongo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la cache para EHR - Obsoleto';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si se crea o no una nueva instancia de cache para XPO en ERP. True:Si, False:No';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la tabla';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración en minutos para la vigencia del dato en la cache - Obsoleto';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración en minutos para la vigencia del dato en la cache - Obsoleto';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración en minutos para la vigencia del dato en la cache - Obsoleto';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de container';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si se visualizan los mensajes del EHR en la parte inferior derecha como en ERP. 0:Normal, 1:Como en ERP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del servidor o ip donde se almacena la cache para EHR - Obsoleto';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si se habilita o no cache para XPO en ERP. True:Si, False:No ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Habilitar en caché';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la base de datos en el servidor mongo';

GO