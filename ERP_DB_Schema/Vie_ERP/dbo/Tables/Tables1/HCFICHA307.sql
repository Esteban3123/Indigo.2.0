CREATE TABLE [dbo].[HCFICHA307] (
    [ID]                     INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION]    INT           NOT NULL,
    [TIPOAGRESION]           INT           NULL,
    [AGRESIONPROVOCADA]      BIT           NULL,
    [TIPOLESION]             INT           NULL,
    [PROFUNDIDAD]            INT           NULL,
    [CABEZA]                 BIT           NULL,
    [MANOS]                  BIT           NULL,
    [TRONCO]                 BIT           NULL,
    [MIEMBROSSUPERI]         BIT           NULL,
    [MIEMBROSINFERI]         BIT           NULL,
    [PIES]                   BIT           NULL,
    [GENITALESEXTERN]        BIT           NULL,
    [FECHAAGRESION]          DATE          NULL,
    [ESPECIEAGRESORA]        INT           NULL,
    [VACUNADO]               INT           NULL,
    [FECHAVACUNACION]        DATE          NULL,
    [PRESENTOCARNE]          INT           NULL,
    [NOMBREPROPIETARIO]      VARCHAR (200) NULL,
    [DIRECCIONPROPIETAR]     VARCHAR (200) NULL,
    [TELEFPROPIETARIO]       VARCHAR (15)  NULL,
    [ESTADOANIMAL]           INT           NULL,
    [UBICACION]              INT           NULL,
    [CLASIEXPOSICION]        INT           NULL,
    [SUEROANTIRRABICO]       INT           NULL,
    [FECHAAPLICACION]        DATE          NULL,
    [VACUNAANTIRRA]          INT           NULL,
    [NUMERODOSIS]            INT           NULL,
    [FECHAULTIMADOSIS]       DATE          NULL,
    [LAVADOHERIDAD]          BIT           NULL,
    [ORDENOSUERO]            BIT           NULL,
    [ORDENOAPLICACION]       BIT           NULL,
    [FIEBRE]                 BIT           NULL,
    [HIPOREXIA]              BIT           NULL,
    [CEFALEA]                BIT           NULL,
    [VOMITO]                 BIT           NULL,
    [PARESIAS]               BIT           NULL,
    [PARESTESIAS]            BIT           NULL,
    [DISFAGIA]               BIT           NULL,
    [ODINOFAGIA]             BIT           NULL,
    [ARREFLEXIA]             BIT           NULL,
    [ALUCINACIONES]          BIT           NULL,
    [EXPRETERROR]            BIT           NULL,
    [SIALORREA]              BIT           NULL,
    [AEROFOBIA]              BIT           NULL,
    [HIDROFOBIA]             BIT           NULL,
    [TRANQUILIDAD]           BIT           NULL,
    [DEPRESION]              BIT           NULL,
    [HIPEREXITABILIDAD]      BIT           NULL,
    [AGRESIVIDAD]            BIT           NULL,
    [ESPASMOSMUSCUL]         BIT           NULL,
    [CONVULSIONES]           BIT           NULL,
    [PARALISIS]              BIT           NULL,
    [CRISISRESPIRATORIA]     BIT           NULL,
    [COMA]                   BIT           NULL,
    [PAROCARDIORESPIRATO]    BIT           NULL,
    [PRUEDIAGNOSTICA]        INT           NULL,
    [RESULTADO]              INT           NULL,
    [IDENTIFICACIONVARIANTE] INT           NULL,
    [VARIANTEIDENTIFICADA]   INT           NULL,
    [OTRA]                   VARCHAR (200) NULL,
    [FECHARESULTADOLABOR]    DATE          NULL,
    [CODDIAGNO]              CHAR (4)      NULL,
    [INFORAMCIONLABORATORIO] INT           NULL,
    [AREAMORDEDURA]          INT           NULL,
    [VERSION]                VARCHAR (20)  NULL,
    [JSON]                   VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA307] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA307_JSON] CHECK (isjson([JSON])=(1))
);


GO
ALTER TABLE [dbo].[HCFICHA307] NOCHECK CONSTRAINT [CK_HCFICHA307_JSON];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nuevas columnas en formato JSON  -->  (versión antigua) = nulo   /    (versiones nuevas, desde ''V01_2020-03-06'' ) =    ESTADO_ANIMAL_CONSULTA = numero (Estado Animal al momento de la Consulta  --> 1= Vivo, 2= Muerto, 3= Desconocido) ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Area de la mordedura:   1=En área cubierta del cuerpo   2=En área descubierta del cuerpo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'AREAMORDEDURA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Infromación del laboratorio:   1=Sí hay información de laboratorio   2=No hay información de laboratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'INFORAMCIONLABORATORIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha resultado laboratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'FECHARESULTADOLABOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ingreso de Otra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'OTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Variante identificada:  1=Uno   2=Tres   3=Cuatro   4=Cinco   5=Ocho   6=Atípica   7=Otra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'VARIANTEIDENTIFICADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificación variante:   1=Si   2=No   3=Pendiente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'IDENTIFICACIONVARIANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado:   1=Positivo   2=Negativo   3=Inadecuado   --> (Item eliminado desde versión V01_2020-03-06)  4=Pendiente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prueba diagnostica confrimatoria:    1= IFD    2= Prueba biológica    3= Histopatología    4= Inmunohistoquímica    5= Titulación anticuerpos antirrábicos -->  (Item nuevo desde versión V01_2020-03-06)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'PRUEDIAGNOSTICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(Antes) Paro cardio respiratorio / (Ahora, V01_2020-03-06 ) Paro  respiratorio :   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'PAROCARDIORESPIRATO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Coma:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'COMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Crisis respiratoria:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'CRISISRESPIRATORIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Paralisis:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'PARALISIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Convulsiones:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'CONVULSIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(Antes) Espasmos musculares / (Ahora, V01_2020-03-06 ) Espasmos :   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'ESPASMOSMUSCUL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Agresividad:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'AGRESIVIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hiperexitabilidad:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'HIPEREXITABILIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Depresión:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'DEPRESION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(Antes) Tranquilidad alterna con exitación / (Ahora, V01_2020-03-06 )  Tranquilidad - excitación :   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'TRANQUILIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hidrofobia:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'HIDROFOBIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Aerofobia:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'AEROFOBIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sialorrea:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'SIALORREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(Antes) Expresión de terror / (Ahora, V01_2020-03-06 )  Fascies :    True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'EXPRETERROR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(Antes) Alucinaciones o delirio de persecución / (Ahora, V01_2020-03-06 )   Alucinaciones :   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'ALUCINACIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(Antes) Arreflexia - hiporreflexia / (Ahora, V01_2020-03-06 )  Arreflexia :   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'ARREFLEXIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Odinofagia:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'ODINOFAGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Disfagia:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'DISFAGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Parestesias:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'PARESTESIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(Antes) Paresias -  debilidad muscular / (Ahora, V01_2020-03-06 ) Paresias :   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'PARESIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vomito:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'VOMITO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cefalea:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'CEFALEA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(Antes) Hiporexia - inapetencia  / (Ahora, V01_2020-03-06 ) Hiperoxia :  True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'HIPOREXIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fiebre:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'FIEBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ordeno Aplicación:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'ORDENOAPLICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ordeno suero:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'ORDENOSUERO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Lavado Agua Jabon:  True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'LAVADOHERIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de última dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'FECHAULTIMADOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'NUMERODOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vacuna antirrábica:  1=Si   2=No   3=No sabe', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'VACUNAANTIRRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la aplicación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'FECHAAPLICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Suero Antirrabico:  1=Si   2=No   3=No sabe', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'SUEROANTIRRABICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clasificación exposición: ---> ( Campo obsoleto desde la versión V01_2020-03-06  )  1=Vivo   2=Muerto   3=Desconicido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'CLASIEXPOSICION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ubicacion:   1=Observable   2=Perdido   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'UBICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del animal al momento de la agresión o contacto:   1= Con signos de rabia   2= Sin signos de rabia   3= Desconocido ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'ESTADOANIMAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Telefono del propietario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'TELEFPROPIETARIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Direccion del propietario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'DIRECCIONPROPIETAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del propietario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'NOMBREPROPIETARIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Presento carnet:   1=Si   2=No   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'PRESENTOCARNE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la vacunación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'FECHAVACUNACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(Antes) Vacunado / (Ahora, V01_2020-03-06 ) Animal vacunado:   1=Si   2=No   3=Desconocido ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'VACUNADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especie agresora  1= Perro   2= Gato   3= (Antes) Bovino  / (Ahora, V01_2020-03-06) Bovino - Bufalino  4= (Antes) Equino /  (Ahora, V01_2020-03-06) Equidos  5= Porcino (cerdo)   6= Murcielago   7= Zorro   8= Mico   9= Humano   10= Otros domésticos --> (Item eliminado desde versión V01_2020-03-06)  11= Otros silvestres   12= Ovino - Caprino   13= Grandes roedores   14= Pequeños roedores  --> (Item eliminado desde versión V01_2020-03-06)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'ESPECIEAGRESORA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la agresión o contacto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'FECHAAGRESION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Localización anatómica  Genitales externos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'GENITALESEXTERN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Localización anatómica  Pies, dedos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'PIES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Localización anatómica  Miembros inferiores', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'MIEMBROSINFERI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Localización anatómica  Miembros superiores', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'MIEMBROSSUPERI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Localización anatómica  Tronco', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'TRONCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Localización anatómica  Manos, dedos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'MANOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Localización anatómica  Cabeza, cara, cuello', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'CABEZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Profundidad  1=superficial  2=profunda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'PROFUNDIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de lesión  1= unica  2= multiple', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'TIPOLESION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Agresión provocada  True = si   false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'AGRESIONPROVOCADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de agresión o contacto  1=Mordedura  2=Arañazo o rasguño  3= (Antes) Contacto de mucosa o piel lesionada con saliva del agresor / (Ahora, version V01_2020-03-06 ) Contacto de mucosa o piel lesionada con saliva o baba infectada con virus rábico   4=Contacto de mucosa o piel lesionada, con tejido nervioso, material biológico o secreciones infectadas con virus rábico  5=Inhalación en ambientes cargados o virus rábico (aerosoles)  6=Trasplante de órganos o tejidos infectados con virus rábico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'TIPOAGRESION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'ID';

