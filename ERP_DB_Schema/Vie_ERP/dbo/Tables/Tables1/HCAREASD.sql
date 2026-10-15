CREATE TABLE [dbo].[HCAREASD] (
    [ID]                       INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDAREASC]                 INT       NOT NULL,
    [CODSERIPS]                CHAR (20) NOT NULL,
    [IDDESCRIPCIONRELACIONADA] INT       NULL,
    CONSTRAINT [PK_HCAREASD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCAREASD_HCAREASC] FOREIGN KEY ([IDAREASC]) REFERENCES [dbo].[HCAREASC] ([ID]),
    CONSTRAINT [FK_HCAREASD_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS])
);


GO
ALTER TABLE [dbo].[HCAREASD] NOCHECK CONSTRAINT [FK_HCAREASD_HCAREASC];


GO
ALTER TABLE [dbo].[HCAREASD] NOCHECK CONSTRAINT [FK_HCAREASD_INCUPSIPS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions) ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAREASD', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAREASD', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del Area otros procedimientos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAREASD', @level2type = N'COLUMN', @level2name = N'IDAREASC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAREASD', @level2type = N'COLUMN', @level2name = N'ID';

