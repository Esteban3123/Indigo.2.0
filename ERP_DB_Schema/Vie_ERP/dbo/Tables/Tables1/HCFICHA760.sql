CREATE TABLE [dbo].[HCFICHA760] (
    [ID]                  INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT          NOT NULL,
    [CODDIAGNO]           CHAR (4)     NULL,
    [DOLORCUELLO]         INT          NULL,
    [DOLORGARGANTA]       INT          NULL,
    [IMPOABRIR]           INT          NULL,
    [DISFAGIA]            INT          NULL,
    [CONVULSIONES]        INT          NULL,
    [CONTRAMUSC]          INT          NULL,
    [RIGIDEZMUSC]         INT          NULL,
    [ESPASGENER]          INT          NULL,
    [RIGIDEZNUCA]         INT          NULL,
    [AFECTNERVIOS]        INT          NULL,
    [TRISMUS]             INT          NULL,
    [OPISTOTONOS]         INT          NULL,
    [FIEBRE]              INT          NULL,
    [OTRO]                VARCHAR (50) NULL,
    [VERSION]             VARCHAR (20) NULL,
    CONSTRAINT [PK_HCFICHA760] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCFICHA760_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA760] NOCHECK CONSTRAINT [FK_HCFICHA760_HCFICHANOTIFICACION];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Otro ¿ Cuál? (DATOS CLÍNICOS)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'OTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Fiebre? (DATOS CLÍNICOS, (1. Sí, 2. No, 3. Desconocido))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'FIEBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Opistótonos? (DATOS CLÍNICOS, (1. Sí, 2. No, 3. Desconocido))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'OPISTOTONOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Trismus? (DATOS CLÍNICOS, (1. Sí, 2. No, 3. Desconocido))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'TRISMUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Afectación de nervios craneales? (DATOS CLÍNICOS, (1. Sí, 2. No, 3. Desconocido))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'AFECTNERVIOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Rigidez de nuca? (DATOS CLÍNICOS, (1. Sí, 2. No, 3. Desconocido))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'RIGIDEZNUCA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Espasmos generalizados? (DATOS CLÍNICOS, (1. Sí, 2. No, 3. Desconocido))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'ESPASGENER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Rigidez en músculos abdominales? (DATOS CLÍNICOS, (1. Sí, 2. No, 3. Desconocido))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'RIGIDEZMUSC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Contracciones musculares? (DATOS CLÍNICOS,(1. Sí, 2. No, 3. Desconocido))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'CONTRAMUSC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Convulsiones? (DATOS CLÍNICOS, (1.Sí, 2. No, 3. Desconocido))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'CONVULSIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Disfagia? (DATOS CLÍNICOS, (1. Sí, 2. No, 3. Desconocido))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'DISFAGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Imposibilidad para abrir la boca / hablar? (DATOS CLÍNICOS, (1. Sí, 2. No, 3. Desconocido))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'IMPOABRIR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Dolor de garganta? (DATOS CLÍNICOS, (1. Sí, 2. No, 3. Desconocido))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'DOLORGARGANTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Dolor del cuello? (DATOS CLÍNICOS, (1. Sí, 2. No, 3. Desconcodido))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'DOLORCUELLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiene relación con la tabla HCFICHANOTIFICACION', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA760', @level2type = N'COLUMN', @level2name = N'ID';

