CREATE TABLE [dbo].[HCPAQORDENPRONOQXD] (
    [ID]                       INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCPAQORDENESC]          INT       NOT NULL,
    [CODIGOSERVICIO]           CHAR (20) NOT NULL,
    [IDDESCRIPCIONRELACIONADA] INT       NULL,
    CONSTRAINT [PK_HCPAQORDENPRONOQXD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPAQORDENPRONOQXD_CUPSEntityContractDescriptions] FOREIGN KEY ([IDDESCRIPCIONRELACIONADA]) REFERENCES [Contract].[CUPSEntityContractDescriptions] ([Id]),
    CONSTRAINT [FK_HCPAQORDENPRONOQXD_HCPAQORDENESC] FOREIGN KEY ([IDHCPAQORDENESC]) REFERENCES [dbo].[HCPAQORDENESC] ([ID]),
    CONSTRAINT [FK_HCPAQORDENPRONOQXD_INCUPSIPS] FOREIGN KEY ([CODIGOSERVICIO]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS])
);


GO
ALTER TABLE [dbo].[HCPAQORDENPRONOQXD] NOCHECK CONSTRAINT [FK_HCPAQORDENPRONOQXD_CUPSEntityContractDescriptions];


GO
ALTER TABLE [dbo].[HCPAQORDENPRONOQXD] NOCHECK CONSTRAINT [FK_HCPAQORDENPRONOQXD_HCPAQORDENESC];


GO
ALTER TABLE [dbo].[HCPAQORDENPRONOQXD] NOCHECK CONSTRAINT [FK_HCPAQORDENPRONOQXD_INCUPSIPS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la Descripción Relacionada (tabla Contract.ContractDescriptions)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENPRONOQXD', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código CUPS del Servicio Procedimiento No Qx', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENPRONOQXD', @level2type = N'COLUMN', @level2name = N'CODIGOSERVICIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla cabecera HCPAQORDENESC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENPRONOQXD', @level2type = N'COLUMN', @level2name = N'IDHCPAQORDENESC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumérico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENPRONOQXD', @level2type = N'COLUMN', @level2name = N'ID';

