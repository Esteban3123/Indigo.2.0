CREATE TABLE [dbo].[HCEVENADVPAC] (
    [ID]              INT        IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDVACUNAPLICADA] INT        NULL,
    [IDEVENADVERSO]   INT        NULL,
    [OBSEVENADVER]    CHAR (200) NULL,
    [FECHACREACION]   DATETIME   NULL,
    [USUCREACION]     CHAR (20)  NULL,
    CONSTRAINT [PK_HCEVENADVPAC] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCEVENADVPAC_HCEVENADV] FOREIGN KEY ([IDEVENADVERSO]) REFERENCES [dbo].[HCEVENADV] ([ID]),
    CONSTRAINT [FK_HCEVENADVPAC_HCVACADIPAC] FOREIGN KEY ([IDVACUNAPLICADA]) REFERENCES [dbo].[HCVACADIPAC] ([ID])
);


GO
ALTER TABLE [dbo].[HCEVENADVPAC] NOCHECK CONSTRAINT [FK_HCEVENADVPAC_HCEVENADV];


GO
ALTER TABLE [dbo].[HCEVENADVPAC] NOCHECK CONSTRAINT [FK_HCEVENADVPAC_HCVACADIPAC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de Creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEVENADVPAC', @level2type = N'COLUMN', @level2name = N'USUCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEVENADVPAC', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observación Evento Adverso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEVENADVPAC', @level2type = N'COLUMN', @level2name = N'OBSEVENADVER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Evento Adverso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEVENADVPAC', @level2type = N'COLUMN', @level2name = N'IDEVENADVERSO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Vacuna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEVENADVPAC', @level2type = N'COLUMN', @level2name = N'IDVACUNAPLICADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEVENADVPAC', @level2type = N'COLUMN', @level2name = N'ID';

