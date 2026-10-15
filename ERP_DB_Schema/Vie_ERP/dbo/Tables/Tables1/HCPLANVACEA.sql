CREATE TABLE [dbo].[HCPLANVACEA] (
    [ID]          INT        IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCEVENADVID] INT        NULL,
    [IDHCPLANVAC] INT        NOT NULL,
    [OBSVACEVEAD] CHAR (200) NULL,
    [FECCREACION] DATETIME   NOT NULL,
    [USUCREACION] CHAR (20)  NULL,
    CONSTRAINT [PK_HCPLANVACEA] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPLANVACEA_HCEVENADVID] FOREIGN KEY ([HCEVENADVID]) REFERENCES [dbo].[HCEVENADV] ([ID])
);


GO
ALTER TABLE [dbo].[HCPLANVACEA] NOCHECK CONSTRAINT [FK_HCPLANVACEA_HCEVENADVID];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVACEA', @level2type = N'COLUMN', @level2name = N'USUCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVACEA', @level2type = N'COLUMN', @level2name = N'FECCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVACEA', @level2type = N'COLUMN', @level2name = N'OBSVACEVEAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del evento adverso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVACEA', @level2type = N'COLUMN', @level2name = N'IDHCPLANVAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla plan de vacunas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVACEA', @level2type = N'COLUMN', @level2name = N'HCEVENADVID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVACEA', @level2type = N'COLUMN', @level2name = N'ID';

