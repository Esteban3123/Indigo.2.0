CREATE TABLE [dbo].[HCDIAGAIEPI] (
    [ID]              INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCAIEPIID]       INT           NOT NULL,
    [DIAGNOSTICO]     VARCHAR (200) NULL,
    [COLOR]           VARCHAR (100) NULL,
    [RECOMENDACIONES] VARCHAR (800) NULL,
    CONSTRAINT [PK_HCDIAGAIEPI] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCDIAGAIEPI_HCAIEPI] FOREIGN KEY ([HCAIEPIID]) REFERENCES [dbo].[HCAIEPI] ([ID])
);


GO
ALTER TABLE [dbo].[HCDIAGAIEPI] NOCHECK CONSTRAINT [FK_HCDIAGAIEPI_HCAIEPI];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Recomendaciones relacionadas al diagnotico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGAIEPI', @level2type = N'COLUMN', @level2name = N'RECOMENDACIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Color correspondiente al diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGAIEPI', @level2type = N'COLUMN', @level2name = N'COLOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGAIEPI', @level2type = N'COLUMN', @level2name = N'DIAGNOSTICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que se relaciona con el ID de la tabla HCAIEPI', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGAIEPI', @level2type = N'COLUMN', @level2name = N'HCAIEPIID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGAIEPI', @level2type = N'COLUMN', @level2name = N'ID';

