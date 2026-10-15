CREATE TABLE [dbo].[HCFICHA440] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
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
    [DIAGCONF]            INT           NULL,
    [FIEBRE]              BIT           NULL,
    [HEPATO]              BIT           NULL,
    [ESPLENO]             BIT           NULL,
    [ANEMIA]              BIT           NULL,
    [LEUCOCI]             BIT           NULL,
    [PLAQUET]             BIT           NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA440] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA440_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA440_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA440] NOCHECK CONSTRAINT [CK_HCFICHA440_JSON];


GO
ALTER TABLE [dbo].[HCFICHA440] NOCHECK CONSTRAINT [FK_HCFICHA440_HCFICHANOTIFICACION];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si el tipo de signos y sintomas  es seleccionada PLAQUET   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'PLAQUET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si el tipo de signos y sintomas  es seleccionada  LEUCOCI   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'LEUCOCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si el tipo de signos y sintomas  es seleccionada ANEMIA   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'ANEMIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si el tipo de signos y sintomas  es seleccionada  ESPLENO   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'ESPLENO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si el tipo de signos y sintomas  es seleccionada HEPATO   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'HEPATO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si el tipo de signos y sintomas  es seleccionada FIEBRE    true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'FIEBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  ¿Tiene diagnóstico VIH confirmado?   1 = si     2 = no    3 = Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'DIAGCONF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el valor (datos de laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'VALOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la Fecha de resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'FECHARESUL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el resultado 1 = Positivo  2 = Negativo  3=Compatible    4 = No Compatible', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el agente  1= Leishmania', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'AGENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Guarda la Prueba  1 = Hematocrito   2 = Hemoglobina  3=Plaquetas   4= Estudio Directo  5= Titulo IFI  6 = Aspirado Bazo 7 = Aspirado Médula  8= Pruebas Montenegro  9 =  Albumina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'PRUEBA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la Muestra  1 =sangre total   2 = Tejido  3 = Linfa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'MUESTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Fecha de recepción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'FECHARECE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la  Fecha toma de examen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'FECHATOMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el Total de cápsulas o ampollas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'TOTACAPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda Días de tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'DIASTRAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el  Número de capsulas o volumen diario a aplicar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'NUMCAPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el Otro ¿Cúal?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'OTROCUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medicamento Formulado Actualmente :    1 = N- Metil Glucamina (Glucantime)   2 = Estibogluconato de Sodio   3 = Isotianato de Pentamidina   4 = Anfotericina B   5 = Otro   6 = Miltefosina   7 = Pentamidina   8 = Sin tratamiento ---> item nuevo desde version ''V01_2020-03-06'' ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'MEDIFORM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el Peso actual del paciente (Kgs)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'PESOACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el Tratamiento local   1 = Crioterapia     2 = Termoterapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'TRATLOCAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda ¿Recibió tratamiento anterior?    1 = si    0 = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'RECITRAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el codigo diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA440', @level2type = N'COLUMN', @level2name = N'ID';

