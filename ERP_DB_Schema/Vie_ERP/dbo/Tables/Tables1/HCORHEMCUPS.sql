CREATE TABLE [dbo].[HCORHEMCUPS] (
    [ID]           INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCORHEMCOID]  INT       NULL,
    [HCORHEMBOLID] INT       NULL,
    [IDHCCOMSAN]   INT       NOT NULL,
    [CODSERIPS]    CHAR (20) NOT NULL,
    CONSTRAINT [PK_HCORHEMCUPS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCORHEMCUPS_HCORHEMCO] FOREIGN KEY ([HCORHEMCOID]) REFERENCES [dbo].[HCORHEMCO] ([ID]),
    CONSTRAINT [FK_HCORHEMCUPS_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS])
);


GO
ALTER TABLE [dbo].[HCORHEMCUPS] NOCHECK CONSTRAINT [FK_HCORHEMCUPS_HCORHEMCO];


GO
ALTER TABLE [dbo].[HCORHEMCUPS] NOCHECK CONSTRAINT [FK_HCORHEMCUPS_INCUPSIPS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del CUPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCUPS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del componente sanguineo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCUPS', @level2type = N'COLUMN', @level2name = N'IDHCCOMSAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Bolsa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCUPS', @level2type = N'COLUMN', @level2name = N'HCORHEMBOLID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id cabecera de la solicitud de hemocomponentes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCUPS', @level2type = N'COLUMN', @level2name = N'HCORHEMCOID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCUPS', @level2type = N'COLUMN', @level2name = N'ID';

