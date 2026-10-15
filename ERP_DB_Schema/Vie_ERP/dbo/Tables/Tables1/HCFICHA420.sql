CREATE TABLE [dbo].[HCFICHA420] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [CARA]                BIT           NULL,
    [TRONCO]              BIT           NULL,
    [MIEMSUP]             BIT           NULL,
    [MIEMINF]             BIT           NULL,
    [RECITRAT]            BIT           NULL,
    [TRATLOCAL]           BIT           NULL,
    [PESOACT]             VARCHAR (50)  NULL,
    [MEDIFORM]            INT           NULL,
    [OTROCUAL]            VARCHAR (50)  NULL,
    [NUMCAPS]             VARCHAR (50)  NULL,
    [DIASTRAT]            VARCHAR (50)  NULL,
    [TOTACAPS]            VARCHAR (50)  NULL,
    [FECHATOMA]           DATE          NULL,
    [FECHARECE]           DATE          NULL,
    [MUESTRA]             INT           NULL,
    [PRUEBA]              INT           NULL,
    [AGENTE]              INT           NULL,
    [RESULTADO]           INT           NULL,
    [FECHARESUL]          DATE          NULL,
    [VALOR]               VARCHAR (50)  NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA420] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA420_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA420_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA420] NOCHECK CONSTRAINT [CK_HCFICHA420_JSON];


GO
ALTER TABLE [dbo].[HCFICHA420] NOCHECK CONSTRAINT [FK_HCFICHA420_HCFICHANOTIFICACION];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nuevas columnas en formato JSON', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA420', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Versión de la ficha de notificación ---> Si es valor nulo = primer versión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA420', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el valor (datos de laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA420', @level2type = N'COLUMN', @level2name = N'VALOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la Fecha de resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA420', @level2type = N'COLUMN', @level2name = N'FECHARESUL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el resultado 1 = Positivo  2 = Negativo  3=Compatible    4 = No Compatible', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA420', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el agente  1= Leishmania', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA420', @level2type = N'COLUMN', @level2name = N'AGENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la Prueba  1 = Estudio Directo   2 = Aspirado Bazo   3= Aspirado Médula   4= Prueba Montenegro   5= Biopsia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA420', @level2type = N'COLUMN', @level2name = N'PRUEBA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la Muestra  1 =sangre total   2 = Tejido  3 = Linfa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA420', @level2type = N'COLUMN', @level2name = N'MUESTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la Fecha de recepción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA420', @level2type = N'COLUMN', @level2name = N'FECHARECE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la  Fecha toma de examen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA420', @level2type = N'COLUMN', @level2name = N'FECHATOMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el Total de cápsulas o ampollas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA420', @level2type = N'COLUMN', @level2name = N'TOTACAPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda Días de tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA420', @level2type = N'COLUMN', @level2name = N'DIASTRAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el  Número de capsulas o volumen diario a aplicar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA420', @level2type = N'COLUMN', @level2name = N'NUMCAPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el Otro ¿Cúal?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA420', @level2type = N'COLUMN', @level2name = N'OTROCUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Medicamento formulado actualmente  1= N- Metil Glucamina  2= Estibogluconato de Sodio  3 = Isotianato de Pentamidina  4= Anfotericina B  5 = Otro  6 = Miltefosina   7 = Pentamidina  8 = Sin tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA420', @level2type = N'COLUMN', @level2name = N'MEDIFORM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el Peso actual del paciente (Kgs)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA420', @level2type = N'COLUMN', @level2name = N'PESOACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el Tratamiento local   1 = Crioterapia     2 = Termoterapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA420', @level2type = N'COLUMN', @level2name = N'TRATLOCAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda ¿Recibió tratamiento anterior?    1 = si    0 = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA420', @level2type = N'COLUMN', @level2name = N'RECITRAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si el tipo de localizacion es seleccionada MIEMINF    true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA420', @level2type = N'COLUMN', @level2name = N'MIEMINF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si el tipo de localizacion es seleccionada  MIEMSUP  true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA420', @level2type = N'COLUMN', @level2name = N'MIEMSUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si el tipo de localizacion es seleccionada tronco   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA420', @level2type = N'COLUMN', @level2name = N'TRONCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si el tipo de localizacion es seleccionada CARA   true = si    false = no  1 =Cara   2= Tronco   3 Miembros superiores    4= Miembros Inferiores', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA420', @level2type = N'COLUMN', @level2name = N'CARA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el codigo diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA420', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA420', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA420', @level2type = N'COLUMN', @level2name = N'ID';

