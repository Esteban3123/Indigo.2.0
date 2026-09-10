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
ALTER TABLE [dbo].[HCORHEMCUPS] NOCHECK CONSTRAINT [FK_HCORHEMCUPS_HCORHEMCO];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CUPS del servicio de hemocomponente; identificador estandarizado RIPS que vincula la prestación de sangre, plasma o derivados según catálogo nacional de procedimientos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCUPS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del CUPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCUPS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCUPS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del componente sanguíneo (sangre total, glóbulos rojos, plaquetas, plasma, crioprecipitado); tipo de hemocomponente transfundido en la solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCUPS', @level2type = N'COLUMN', @level2name = N'IDHCCOMSAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del componente sanguineo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCUPS', @level2type = N'COLUMN', @level2name = N'IDHCCOMSAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCUPS', @level2type = N'COLUMN', @level2name = N'IDHCCOMSAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la bolsa física del hemocomponente; número único de trazabilidad y lote del componente sanguíneo administrado al paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCUPS', @level2type = N'COLUMN', @level2name = N'HCORHEMBOLID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Bolsa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCUPS', @level2type = N'COLUMN', @level2name = N'HCORHEMBOLID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCUPS', @level2type = N'COLUMN', @level2name = N'HCORHEMBOLID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de cabecera de solicitud de hemocomponentes; referencia a la orden o requisición principal de transfusión sanguínea del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCUPS', @level2type = N'COLUMN', @level2name = N'HCORHEMCOID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id cabecera de la solicitud de hemocomponentes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCUPS', @level2type = N'COLUMN', @level2name = N'HCORHEMCOID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCUPS', @level2type = N'COLUMN', @level2name = N'HCORHEMCOID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo único de la tabla HCORHEMCUPS; identificador de transaccional que vincula bolsa, componente y código de servicio CUPS en la solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCUPS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCUPS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCUPS', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalla los códigos de servicios (CUPS) asociados a órdenes o registros de hemocultivos en la historia clínica, vinculando cada muestra o componente de hemocultivo con su respectivo código de procedimiento o servicio de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCUPS';
