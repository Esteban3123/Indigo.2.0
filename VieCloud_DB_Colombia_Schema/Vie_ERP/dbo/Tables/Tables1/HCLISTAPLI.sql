CREATE TABLE [dbo].[HCLISTAPLI] (
    [ID]                       INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDLISTACHEQUEO]           INT       NOT NULL,
    [CODSERIPS]                CHAR (20) NOT NULL,
    [IDDESCRIPCIONRELACIONADA] INT       NULL,
    CONSTRAINT [PK_HCLISTAPLI] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCLISTAPLI_HCLISTACC] FOREIGN KEY ([IDLISTACHEQUEO]) REFERENCES [dbo].[HCLISTACC] ([CODCONSEC])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de descripción relacionada con contrato; clave foránea hacia VIE ERP (contract.CUPSEntityContractDescriptions); vincula procedimientos/servicios a descripciones de contrato; permite consultar detalles de cobertura y autorización', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTAPLI', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions) ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTAPLI', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTAPLI', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de servicio RIPS (Registro Individual de Prestaciones de Salud); identifica procedimiento, examen, consulta, atención o servicio de salud reportable; clave para auditoría, facturación y regulación MINSALUD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTAPLI', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de servicio RIPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTAPLI', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTAPLI', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico de lista de chequeo (checklist); referencia a tabla HCLISTACC; vincula items de validación, control de procesos o verificación clínica en historias de salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTAPLI', @level2type = N'COLUMN', @level2name = N'IDLISTACHEQUEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID autonumerico lista de Chequeo ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTAPLI', @level2type = N'COLUMN', @level2name = N'IDLISTACHEQUEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTAPLI', @level2type = N'COLUMN', @level2name = N'IDLISTACHEQUEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico principal (INT IDENTITY); clave primaria única de relación entre lista de chequeo y códigos RIPS; generado automáticamente por SQL Server', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTAPLI', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTAPLI', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTAPLI', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista de procedimientos o servicios (CUPS) asociados a una lista de chequeo clínica. Permite vincular cada ítem de verificación con un servicio o procedimiento específico de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTAPLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTAPLI';
