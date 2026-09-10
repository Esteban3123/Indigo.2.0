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
ALTER TABLE [dbo].[HCPAQORDENPROQXD] NOCHECK CONSTRAINT [FK_HCPAQORDENPROQXD_HCPAQORDENESC];




GO



GO
ALTER TABLE [dbo].[HCPAQORDENPROQXD] NOCHECK CONSTRAINT [FK_HCPAQORDENPROQXD_HCPAQORDENESC];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la descripción relacionada del contrato CUPS (tabla Contract.CUPSEntityContractDescriptions); vincula el procedimiento quirúrgico con su descripción contractual y cobertura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENPROQXD', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la Descripción Relacionada (tabla Contract.ContractDescriptions)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENPROQXD', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENPROQXD', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CUPS del servicio o procedimiento quirúrgico (Qx); identificador de la prestación según nomenclatura nacional de servicios de salud, referencia a INCUPSIPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENPROQXD', @level2type = N'COLUMN', @level2name = N'CODIGOSERVICIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código CUPS del Servicio Procedimiento Qx', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENPROQXD', @level2type = N'COLUMN', @level2name = N'CODIGOSERVICIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENPROQXD', @level2type = N'COLUMN', @level2name = N'CODIGOSERVICIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la orden quirúrgica (tabla cabecera HCPAQORDENESC); enlace con la orden principal de procedimiento o intervención quirúrgica del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENPROQXD', @level2type = N'COLUMN', @level2name = N'IDHCPAQORDENESC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla cabecera HCPAQORDENESC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENPROQXD', @level2type = N'COLUMN', @level2name = N'IDHCPAQORDENESC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENPROQXD', @level2type = N'COLUMN', @level2name = N'IDHCPAQORDENESC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico único (INT IDENTITY) del detalle de procedimiento quirúrgico en la orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENPROQXD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumérico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENPROQXD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENPROQXD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de procedimientos quirúrgicos (cirugías) asociados a órdenes de sala de cirugía en la historia clínica. Cada registro vincula un servicio o procedimiento quirúrgico específico (código CUPS) a una orden de escenario quirúrgico del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENPROQXD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENPROQXD';
