CREATE TABLE [dbo].[HCFICHA535] (
    [ID]                  INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT          NOT NULL,
    [CODDIAGNO]           CHAR (4)     NULL,
    [TIPOAGENTE]          INT          NULL,
    [VACUANTIHIB]         INT          NULL,
    [VACUANTIMENINGO]     INT          NULL,
    [VACUANTINEUMO]       INT          NULL,
    [DOSIS1]              INT          NULL,
    [DOSIS2]              INT          NULL,
    [DOSIS3]              INT          NULL,
    [FECHAULTIDO1]        DATE         NULL,
    [FECHAULTIDO2]        DATE         NULL,
    [FECHAULTIDO3]        DATE         NULL,
    [FECHAULTIDO4]        DATE         NULL,
    [USOANTIBIO]          BIT          NULL,
    [CLASIFCASO]          INT          NULL,
    [FECHATOMA]           DATE         NULL,
    [FECHARECE]           DATE         NULL,
    [MUESTRA]             INT          NULL,
    [PRUEBA]              INT          NULL,
    [AGENTE]              INT          NULL,
    [RESULTADO]           INT          NULL,
    [FECHARESUL]          DATE         NULL,
    [VALOR]               VARCHAR (50) NULL,
    [VERSION]             VARCHAR (20) NULL,
    CONSTRAINT [PK_HCFICHA535] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCFICHA535_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA535] NOCHECK CONSTRAINT [FK_HCFICHA535_HCFICHANOTIFICACION];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el valor  (datos de laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'VALOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fecha del  resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'FECHARESUL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda resultado  1 =Positivo    2 = Negativo  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda agente  1 =Otro   2 =Haemophilus influenzae  3 = Neisseria meningitidis 4 =Streptococus pneumonie  5 =Staphylococcus aereus 6 =Listeria monocytogenes 7 =E. coli 8 =Enterobacter clocae  9 =Enterobacter clocae   10 =Staphylococcus epidermidis  11= Streptococcus beta hemiolitico 12 =Streptococcus agalactiae  13 =Streptococcus pyogenes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'AGENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda prueba   1 =Aislamiento   2 =Cultivo  3 = Coloración de gram    4 = Antigenemia  5 = RT/PCR', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'PRUEBA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda muestra 1 =sangre total    2 = LCR', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'MUESTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Fecha de recepción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'FECHARECE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Fecha toma de examen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'FECHATOMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la clasificación de caso enfermedad meningocócica    1 = Meningitis    2 = Meningitis con meningoccocemia    3 =  Meningoccocemia sin meningitis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'CLASIFCASO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda  si uso antibioticos en la ultima semana    1 =  Si    0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'USOANTIBIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la fecha4  de ultima dosis  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'FECHAULTIDO4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la fecha3  de ultima dosis  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'FECHAULTIDO3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la fecha2  de ultima dosis  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'FECHAULTIDO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la fecha1  de ultima dosis  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'FECHAULTIDO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda Dosis3   1 =1     2 =2      3 =3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'DOSIS3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda Dosis2   1 =1     2 =2      3 =3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'DOSIS2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda Dosis1   1 =1     2 =2      3 =3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'DOSIS1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda  Vacuna anti neumococo   1 = Si   2 =No   3 = Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'VACUANTINEUMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda  Vacuna anti meningococo   1 = Si   2 =No   3 = Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'VACUANTIMENINGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Gurada  vacuna  anti - Hib   1  = Si  2 = No  3 = Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'VACUANTIHIB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el tipo de agente bacteriano  1  =Haemophilus Influenzae (Hi)     2 =Neisseria meningitidis (Meningococo)      3 =Streptoccoccus pneumoniae (Neumococo)   4 =Otros agentes bacterianos  5 = Agente sin determinar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'TIPOAGENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda  codigo diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo  de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA535', @level2type = N'COLUMN', @level2name = N'ID';

