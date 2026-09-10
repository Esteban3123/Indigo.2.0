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



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la Descripción Relacionada (tabla Contract.ContractDescriptions)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENPRONOQXD', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CUPS del servicio o procedimiento no quirúrgico (Qx); identificador estándar de facturación y RIPS en Colombia; referencia a tabla INCUPSIPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENPRONOQXD', @level2type = N'COLUMN', @level2name = N'CODIGOSERVICIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código CUPS del Servicio Procedimiento No Qx', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENPRONOQXD', @level2type = N'COLUMN', @level2name = N'CODIGOSERVICIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENPRONOQXD', @level2type = N'COLUMN', @level2name = N'CODIGOSERVICIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cabecera de orden de procedimiento/servicio no quirúrgico; clave foránea a tabla HCPAQORDENESC (registro maestro de la orden)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENPRONOQXD', @level2type = N'COLUMN', @level2name = N'IDHCPAQORDENESC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla cabecera HCPAQORDENESC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENPRONOQXD', @level2type = N'COLUMN', @level2name = N'IDHCPAQORDENESC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENPRONOQXD', @level2type = N'COLUMN', @level2name = N'IDHCPAQORDENESC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico (identity) de la línea/detalle de orden; clave primaria de la tabla de detalle de procedimientos no Qx', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENPRONOQXD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumérico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENPRONOQXD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENPRONOQXD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de servicios o procedimientos quirúrgicos incluidos en una orden de pronóstico de cirugía (pronostico QX). Cada registro asocia un servicio o procedimiento específico a una orden quirúrgica preoperatoria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENPRONOQXD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENPRONOQXD';
