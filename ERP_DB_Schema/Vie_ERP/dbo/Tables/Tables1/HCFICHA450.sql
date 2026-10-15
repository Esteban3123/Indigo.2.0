CREATE TABLE [dbo].[HCFICHA450] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [TIPOINGRESO]         INT           NULL,
    [CLASIFCASO]          INT           NULL,
    [NUMLESIONES]         VARCHAR (50)  NULL,
    [BACILOS]             BIT           NULL,
    [RESULTADO]           VARCHAR (50)  NULL,
    [BIOPSIA]             BIT           NULL,
    [RESULHISPA]          INT           NULL,
    [MAXIGRADO]           INT           NULL,
    [PRESEREACCION]       INT           NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA450] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA450_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA450_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA450] NOCHECK CONSTRAINT [CK_HCFICHA450_JSON];


GO
ALTER TABLE [dbo].[HCFICHA450] NOCHECK CONSTRAINT [FK_HCFICHA450_HCFICHANOTIFICACION];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nuevas columnas en formato JSON', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Versión de la ficha de notificación ---> Si es valor nulo = primer versión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Presenta reacción?   1 = tipo uno    2 =  tipo dos  3= ninguno ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'PRESEREACCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Máximo grado de discapacidad evaluado  1 = grado cero   2 = grado uno   3 =  grado 3  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'MAXIGRADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el  Resultado de la histopatología  1 =  Indeterminada    2 = Tuberculoide    3 =Dimorfa (Borderline)     4 =Lepromatosa    5 =Neural     6 =  Otro Diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'RESULHISPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Biopsia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'BIOPSIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  el  Resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Baciloscopia   1 = si   0=  No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'BACILOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el  Número de lesiones identificadas, examen clínico inicial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'NUMLESIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Clasificación clínica del caso    1 = Paucibacilar    2 = Multibacilar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'CLASIFCASO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarada el tipo de ingreso 1 = Nuevo      2 = Recidiva    3 =  Retratamiento despues de la pérdida al seguimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'TIPOINGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda codigo diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA450', @level2type = N'COLUMN', @level2name = N'ID';

