CREATE TABLE [dbo].[HCFICHA650] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [NOMBEVENTO]          VARCHAR (500) NULL,
    [CODEVENTO]           VARCHAR (10)  NULL,
    [FECHANOTIFICA]       DATETIME      NULL,
    [RAZONSOCIAL]         VARCHAR (100) NULL,
    [UPGD]                VARCHAR (50)  NULL,
    [CLASIFICACION]       TINYINT       NULL,
    [PROPIETARIO]         VARCHAR (100) NULL,
    [RESIDENCIA]          VARCHAR (100) NULL,
    [TELEFONO]            VARCHAR (50)  NULL,
    [DEPMUNCOD]           CHAR (5)      NULL,
    [ESPECIE]             TINYINT       NULL,
    [RAZA]                VARCHAR (100) NULL,
    [COLOR]               VARCHAR (100) NULL,
    [EDAD]                TINYINT       NULL,
    [UNIDAD]              TINYINT       NULL,
    [ANTECEDENTE]         TINYINT       NULL,
    [FECHAVACUNA]         DATETIME      NULL,
    [AREA]                TINYINT       NULL,
    [AGRESIVIDAD]         BIT           NULL,
    [PARALISIS]           BIT           NULL,
    [SALIVACION]          BIT           NULL,
    [APETITO]             BIT           NULL,
    [VORACIDAD]           BIT           NULL,
    [DEGLUCION]           BIT           NULL,
    [LADRIDO]             BIT           NULL,
    [MANDIBULA]           BIT           NULL,
    [ANISOCORIA]          BIT           NULL,
    [OTROSIGNO]           BIT           NULL,
    [CUALSIGNO]           VARCHAR (100) NULL,
    [FECHASINTOMA]        DATETIME      NULL,
    [TIPOMUERTE]          TINYINT       NULL,
    [FECHAMUERTE]         DATETIME      NULL,
    [INFOLABORATORIO]     BIT           NULL,
    [FECHATOMA]           DATETIME      NULL,
    [FECHAREMISION]       DATETIME      NULL,
    [PRUEBADX]            TINYINT       NULL,
    [RESULTADO]           TINYINT       NULL,
    [IDENTIFICACION]      BIT           NULL,
    [VARIANTE]            TINYINT       NULL,
    [CUALVARIANTE]        VARCHAR (100) NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA650] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA650_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA650_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA650] NOCHECK CONSTRAINT [CK_HCFICHA650_JSON];


GO
ALTER TABLE [dbo].[HCFICHA650] NOCHECK CONSTRAINT [FK_HCFICHA650_HCFICHANOTIFICACION];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nuevas columnas en formato JSON', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Versión de la ficha de notificación --->primer versión : V01_2020-03-06', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuál otra variante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'CUALVARIANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Variante identificada:   1 - 1  3 - 3  4 - 4  5 - 5  8 - 8  0 - 0. Otra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'VARIANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificación variante:   True: Sí  False: No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'IDENTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado:   1- Positivo   2- Negativo   3- Inadecuado   4- Pendiente   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prueba diagnóstica:   1- IFD   2- Prueba biológica   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'PRUEBADX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha remisión de muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'FECHAREMISION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha toma de muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'FECHATOMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Información de Laboratorio:  True: Sí hay información de labotorio  False: No hay información de laboratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'INFOLABORATORIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de muerte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'FECHAMUERTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de muerte:   1- Espontánea   2- Sacrificio   3- Accidente   4- Desconocida    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'TIPOMUERTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de inicio de síntomas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'FECHASINTOMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cual Otro (Signo y sintoma)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'CUALSIGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Otro (signo y sintoma)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'OTROSIGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Anisocoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'ANISOCORIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mandibula trabada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'MANDIBULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ladrido ronco', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'LADRIDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Deglución dificultosa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'DEGLUCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Voracidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'VORACIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Apetito alterado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'APETITO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Salivación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'SALIVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Parálisis miembros posteriores', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'PARALISIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Agresividad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'AGRESIVIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Área de procedencia del animal:   1- Cabecera municipal   2- Centro poblado   3- Rural disperso   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'AREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de vacunación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'FECHAVACUNA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antecedente de vacunación:   1- Sí  2- No  3- Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'ANTECEDENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de medida de edad:   1- Años   2- Meses   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'UNIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'EDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Color de la cabeza del animal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'COLOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Raza', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'RAZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especie:   1- Perro   2- Gato   3- Zorro   4- Murciélago   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'ESPECIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del departamento y municipio de residencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Télefono', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'TELEFONO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dirección de residencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'RESIDENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombres y apellidos del propietario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'PROPIETARIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clasificación inicial del caso:   1 - Probable   2- Confirmado por laboratorio   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'CLASIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la UPGD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'UPGD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Razón social de la Unidad primaria generadora de datos UPGD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'RAZONSOCIAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'FECHANOTIFICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del Evento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'CODEVENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del Evento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'NOMBEVENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'ID';

