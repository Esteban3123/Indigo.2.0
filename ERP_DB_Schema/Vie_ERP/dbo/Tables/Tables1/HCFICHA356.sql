CREATE TABLE [dbo].[HCFICHA356] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [FECHAOCURRENCIA]     DATE          NULL,
    [INTENPREVIOS]        BIT           NULL,
    [NUMINTENTOS]         INT           NULL,
    [ESTADOCIVIL]         INT           NULL,
    [ESCOLARIDAD]         INT           NULL,
    [CONFLIPAREJA]        BIT           NULL,
    [PROBLEJURIDICOS]     BIT           NULL,
    [ENFERCRONICA]        BIT           NULL,
    [SUICIDIOFAMI]        BIT           NULL,
    [PROBLEECONO]         BIT           NULL,
    [MALTRAFISICO]        BIT           NULL,
    [MUERTEFAMI]          BIT           NULL,
    [PROBLELABOR]         BIT           NULL,
    [ESCOLAR]             BIT           NULL,
    [CONSUMOSPA]          BIT           NULL,
    [ANTECEFAMI]          BIT           NULL,
    [IDEASUICIDA]         BIT           NULL,
    [PLANSUICIDIO]        BIT           NULL,
    [ANTEVIOLENCIA]       BIT           NULL,
    [ABUSOALCOHOL]        BIT           NULL,
    [ANTEPSQUIATRI]       BIT           NULL,
    [AHORCAMI]            BIT           NULL,
    [ELEMCORTO]           BIT           NULL,
    [ARMAFUEGO]           BIT           NULL,
    [INMOLACION]          BIT           NULL,
    [LANZAVACIO]          BIT           NULL,
    [LANZAVEHI]           BIT           NULL,
    [LANZACUERPO]         BIT           NULL,
    [INTOXICACION]        BIT           NULL,
    [CODPRODUC]           VARCHAR (50)  NULL,
    [NOMBREPRODUC]        VARCHAR (50)  NULL,
    [VIAEXPO]             INT           NULL,
    [LUGARINTOX]          INT           NULL,
    [PSIQUIATRIA]         BIT           NULL,
    [PSICOLOGIA]          BIT           NULL,
    [TRABAJSOCIAL]        BIT           NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [CASOTRASTOR]         INT           NULL,
    [CASOINTOX]           INT           NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA356] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA356_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA356_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA356] NOCHECK CONSTRAINT [CK_HCFICHA356_JSON];


GO
ALTER TABLE [dbo].[HCFICHA356] NOCHECK CONSTRAINT [FK_HCFICHA356_HCFICHANOTIFICACION];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nuevas columnas en formato JSON  -->  (versión antigua) = nulo   /    (versiones nuevas, desde V01_2020-03-06 ) = PROBLEFAMILIAR = bit (Problema Familiar)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Versión de la ficha de notificación ---> Si es valor nulo = primer versión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Caso de la intoxicación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'CASOINTOX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Caso del trastorno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'CASOTRASTOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Servicio de trabajo social (remisión a salud mental)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'TRABAJSOCIAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Servicio de psicologia (remisión a salud mental)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'PSICOLOGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Servicio de psiquiatria (remisión a salud mental)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'PSIQUIATRIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lugar donde se produjo la intoxicación  (Mecanismo de suicidio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'LUGARINTOX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Via de la exposición (Mecanismo de suicidio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'VIAEXPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del producto (Mecanismo de suicidio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'NOMBREPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto (Mecanismo de suicidio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Intoxicación (Mecanismo de suicidio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'INTOXICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Salto a un cuerpo de agua (Mecanismo de suicidio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'LANZACUERPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Salto a un vehiculo (Mecanismo de suicidio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'LANZAVEHI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Salto al vacio (Mecanismo de suicidio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'LANZAVACIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Inmolación (Mecanismo de suicidio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'INMOLACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Arma de fuego (Mecanismo de suicidio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'ARMAFUEGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Elemento cortopunzante  (Mecanismo de suicidio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'ELEMCORTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ahorcamiento o asfixia (Mecanismo de suicidio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'AHORCAMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de trastorno psiquiatrico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'ANTEPSQUIATRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Abuso del alcohol', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'ABUSOALCOHOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes de violencia o abuso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'ANTEVIOLENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plan organizado de suicidio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'PLANSUICIDIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ideas suicidas persistente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'IDEASUICIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de conducta suicida (familiar)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'ANTECEFAMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consumo de sustancias psicoactivas (SPA)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'CONSUMOSPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel escolar alcanzado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'ESCOLAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Problemas laborales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'PROBLELABOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Muerte familiar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'MUERTEFAMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Maltrato físico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'MALTRAFISICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Problemas económicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'PROBLEECONO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Suicidio de un familiar o amigo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'SUICIDIOFAMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Enfermedad crónica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'ENFERCRONICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Problema juridico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'PROBLEJURIDICOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Conflicto con la pareja o expareja', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'CONFLIPAREJA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de escolaridad del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'ESCOLARIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estádo civil del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'ESTADOCIVIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de intentos de suicidio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'NUMINTENTOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Intento previo de suicidio (true or false)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'INTENPREVIOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la ocurrencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'FECHAOCURRENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la tabla HCFICHANOTIFICACION (Id)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'ID';

