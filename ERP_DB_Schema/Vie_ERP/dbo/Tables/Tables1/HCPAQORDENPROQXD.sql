CREATE TABLE [dbo].[HCPAQORDENPROQXD] (
    [ID]                       INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCPAQORDENESC]          INT       NOT NULL,
    [CODIGOSERVICIO]           CHAR (20) NOT NULL,
    [IDDESCRIPCIONRELACIONADA] INT       NULL,
    CONSTRAINT [PK_HCPAQORDENPROQXD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPAQORDENPROQXD_CUPSEntityContractDescriptions] FOREIGN KEY ([IDDESCRIPCIONRELACIONADA]) REFERENCES [Contract].[CUPSEntityContractDescriptions] ([Id]),
    CONSTRAINT [FK_HCPAQORDENPROQXD_HCPAQORDENESC] FOREIGN KEY ([IDHCPAQORDENESC]) REFERENCES [dbo].[HCPAQORDENESC] ([ID]),
    CONSTRAINT [FK_HCPAQORDENPROQXD_INCUPSIPS] FOREIGN KEY ([CODIGOSERVICIO]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS])
);


GO
ALTER TABLE [dbo].[HCPAQORDENPROQXD] NOCHECK CONSTRAINT [FK_HCPAQORDENPROQXD_CUPSEntityContractDescriptions];


GO
ALTER TABLE [dbo].[HCPAQORDENPROQXD] NOCHECK CONSTRAINT [FK_HCPAQORDENPROQXD_HCPAQORDENESC];


GO
ALTER TABLE [dbo].[HCPAQORDENPROQXD] NOCHECK CONSTRAINT [FK_HCPAQORDENPROQXD_INCUPSIPS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la Descripción Relacionada (tabla Contract.ContractDescriptions)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENPROQXD', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código CUPS del Servicio Procedimiento Qx', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENPROQXD', @level2type = N'COLUMN', @level2name = N'CODIGOSERVICIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla cabecera HCPAQORDENESC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENPROQXD', @level2type = N'COLUMN', @level2name = N'IDHCPAQORDENESC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumérico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENPROQXD', @level2type = N'COLUMN', @level2name = N'ID';

