CREATE TABLE [Security].[UserConfiguration] (
    [Id]                    BIGINT        IDENTITY (1, 1) NOT NULL,
    [UserId]                INT           NOT NULL,
    [ShowThemeSkinSelector] BIT           CONSTRAINT [DF_UserConfiguration_ShowThemeSkinSelector] DEFAULT ((1)) NOT NULL,
    [ReporteadorActivo]     BIT           CONSTRAINT [DF_UserConfiguration_ReporteadorActivo] DEFAULT ((1)) NOT NULL,
    [LightweightVersion]    BIT           CONSTRAINT [DF_UserConfiguration_LightweightVersion] DEFAULT ((0)) NOT NULL,
    [CustomReportPath]      VARCHAR (200) CONSTRAINT [DF_UserConfiguration_CustomReportPath] DEFAULT ('C:\Reportes\') NOT NULL,
    [ActualCity]            VARCHAR (50)  CONSTRAINT [DF_UserConfiguration_ActualCity] DEFAULT ('368165') NOT NULL,
    [LanguageCulture]       VARCHAR (20)  CONSTRAINT [DF_UserConfiguration_LanguageCulture] DEFAULT ('es-CO') NOT NULL,
    [DefaultCompany]        INT           NULL,
    [Dashboard]             TINYINT       NULL,
    [SideFace]              TINYINT       NULL,
    [CenterAttention]       VARCHAR (10)  NULL,
    [FunctionalUnit]        VARCHAR (10)  NULL,
    [NameCareCenter]        VARCHAR (100) NULL,
    [FunctionalUnitName]    VARCHAR (60)  NULL,
    [TypeFunctionalUnit]    INT           NULL,
    [RoleCode]              VARCHAR (3)   NULL,
    [GroupCode]             VARCHAR (3)   NULL,
    [DefaultConfiguration]  BIT           CONSTRAINT [DF_UserConfiguration_DefaultConfiguration] DEFAULT ((0)) NULL,
    [OperatingUnitId]       INT           NULL,
    [TypeAlertControl]      TINYINT       NULL,
    [IdTimezone]            INT           CONSTRAINT [DF__UserConfi__IdTim__65AC084E] DEFAULT ((1)) NOT NULL,
    [DateFormat]            INT           CONSTRAINT [DF__UserConfi__DateF__6D4D2A16] DEFAULT ((0)) NOT NULL,
    [TimeFormat]            INT           CONSTRAINT [DF__UserConfi__TimeF__6E414E4F] DEFAULT ((0)) NOT NULL,
    [ReportPathType]        TINYINT       DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_UserConfiguration] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserConfiguration_Containers] FOREIGN KEY ([DefaultCompany]) REFERENCES [Security].[Containers] ([Id]),
    CONSTRAINT [FK_UserConfiguration_Timezone] FOREIGN KEY ([IdTimezone]) REFERENCES [Security].[Timezone] ([Id]),
    CONSTRAINT [FK_UserConfiguration_User] FOREIGN KEY ([UserId]) REFERENCES [Security].[User] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_UserConfiguration_UserId]
    ON [Security].[UserConfiguration]([UserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_UserConfiguration_DefaultCompany]
    ON [Security].[UserConfiguration]([DefaultCompany] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena la configuración de usuario', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserConfiguration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la tabla', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserConfiguration', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de usuario', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserConfiguration', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si muestra el tema seleccionado o la configuración de tema por defecto', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserConfiguration', @level2type = N'COLUMN', @level2name = N'ShowThemeSkinSelector';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si carga o no el layout del reporte personalizado ', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserConfiguration', @level2type = N'COLUMN', @level2name = N'ReporteadorActivo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si carga o no los videos en el login de la aplicación, aplica para el login normal, no aplica para login B2C.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserConfiguration', @level2type = N'COLUMN', @level2name = N'LightweightVersion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ruta de reportes personalizados', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserConfiguration', @level2type = N'COLUMN', @level2name = N'CustomReportPath';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ciudad actual, corresponde a un identificador de una lista de ciudades embebidos en el archivo VieWoeidCities.xml. Acacías:368250,Aguachica:368157,Agustín Codazzi:350564,Apartadó:368635,Arauca:368458,Arjona:368263,Armenia:368158,Baranoa:368265,Barrancabermeja:368159,Barranquilla:368151,Bello:368160,Bogotá:368148,Bucaramanga:368152,Buenaventura:368161,Buga:368182,Cajicá:368471,Calarcá:368185,Caldas:368186,Cali:368149,Candelaria:358206,Carepa:26797690,Cartagena:368153,Cartago:352579,Caucasia:368187,Cereté:368188,Chía:352956,Chigorodó:368781,Chinchiná:368191,Chiquinquirá:368285,Ciénaga:368162,Ciénaga de Oro:368194,Copacabana:368293,Corozal:353342,Cúcuta:368154,Dosquebradas:353700,Duitama:368196,El Banco:368197,El Carmen de Bolívar:368198,El Cerrito:368199,El Espinal:356391,Envigado:356359,Facatativá:368200,Florencia:368201,Floridablanca:368934,Fundación:368206,Funza:356558,Fusagasugá:368207,Garzón:368208,Girardot:368209,Girardota:368501,Girón:356678,Granada:368951,Ibagué:368155,Ipiales:368211,Itagüí:368212,Jamundí:368213,La Ceja:368322,La Dorada:368323,La Estrella:358787,La Plata:368214,Los Patios:361758,Madrid:361938,Magangué:368217,Maicao:368338,Malambo:368528,Manaure:369112,Manizales:368156,Marinilla:368531,Medellín:368150,Montelíbano:369150,Montería:368164,Necoclí:369174,Neiva:368165,Ocaña:368220,Orito:369191,Palmira:368166,Pamplona:368223,Pasto:368167,Pereira:368168,Piedecuesta:368225,Pitalito:368226,Planeta Rica:368228,Plato:368229,Popayán:368169,Pradera:368364,Puerto Asís:369266,Puerto Boyacá:368366,Quibdó:368554,Riohacha:368559,Rionegro:368232,Riosucio:368233,Sabanalarga:368563,Sabaneta:364997,Sahagún:365024,San Andrés:365117,San José del Guaviare:368571,San Marcos:368398,San Vicente del Caguán:369418,Santa Cruz de Lorica:368216,Santa Marta:368578,Santa Rosa de Cabal:366619,Santander de Quilichao:366683,Sincelejo:368171,Soacha:366928,Sogamoso:368238,Soledad:368239,Tame:369477,Tierralta:368424,Tuluá:368241,Tumaco:369509,Tunja:368172,Turbaco:368433,Turbo:368604,Uribia:368435,Valledupar:368173,Villa del Rosario:367929,Villamaría:367937,Villavicencio:368174,Yopal:368245,Yumbo:368246,Zipaquirá:368248,Zona Bananera:56125843', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserConfiguration', @level2type = N'COLUMN', @level2name = N'ActualCity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lenguaje y cultura. es-CO, en-US', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserConfiguration', @level2type = N'COLUMN', @level2name = N'LanguageCulture';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de compañia, compañia por defecto.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserConfiguration', @level2type = N'COLUMN', @level2name = N'DefaultCompany';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dashboard por defecto. Ninguno = 0, DashBoard_Medico = 1, DashBoard_Enfermeria = 2, DashBoard_Interconsultas = 3, DashBoard_Terapias = 4, DashBoard_Laboratorio = 5, DashBoard_Imagenologia = 6, DashBoard_Patologias = 7, DashBoard_ServiciosApoyo = 8, DashBoard_MedicoInternos = 9', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserConfiguration', @level2type = N'COLUMN', @level2name = N'Dashboard';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Perfil. Depende de dashboard por defecto = 0, Especialista o Hemocomponente = 1, Académico = 10', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserConfiguration', @level2type = N'COLUMN', @level2name = N'SideFace';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de centro de atención', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserConfiguration', @level2type = N'COLUMN', @level2name = N'CenterAttention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad funcional', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserConfiguration', @level2type = N'COLUMN', @level2name = N'FunctionalUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de centro de atención', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserConfiguration', @level2type = N'COLUMN', @level2name = N'NameCareCenter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de unidad funcional', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserConfiguration', @level2type = N'COLUMN', @level2name = N'FunctionalUnitName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de unidad funcional', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserConfiguration', @level2type = N'COLUMN', @level2name = N'TypeFunctionalUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de rol', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserConfiguration', @level2type = N'COLUMN', @level2name = N'RoleCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de grupo', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserConfiguration', @level2type = N'COLUMN', @level2name = N'GroupCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si es configuración por defecto, al hacer login toma o no la configuración almacenada', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserConfiguration', @level2type = N'COLUMN', @level2name = N'DefaultConfiguration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de unidad operativa', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserConfiguration', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de control de notificación, 1:Alert windows, 2:Toast notification', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserConfiguration', @level2type = N'COLUMN', @level2name = N'TypeAlertControl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la zona horaria', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserConfiguration', @level2type = N'COLUMN', @level2name = N'IdTimezone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Formato fecha
0 = dd/MM/yyyy
1 = MM/dd/yyyy
2 = yyy/MM/dd
3 = dd/MMM/yyyy
4 = dd \de MMMM \de yyyy', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserConfiguration', @level2type = N'COLUMN', @level2name = N'DateFormat';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Formato hora =0:Militar(H-i) 1:Normal(g:i AM - PM) ', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserConfiguration', @level2type = N'COLUMN', @level2name = N'TimeFormat';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de ruta  para reporte: Usuario =1 , Empresa= 2', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserConfiguration', @level2type = N'COLUMN', @level2name = N'ReportPathType';

