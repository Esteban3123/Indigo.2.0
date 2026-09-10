CREATE TABLE [dbo].[HCGRUPINVD] (
    [ID]                       INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCGRUPINVC]             INT       NOT NULL,
    [CODSERIPS]                CHAR (20) NOT NULL,
    [IDDESCRIPCIONRELACIONADA] INT       NULL,
    CONSTRAINT [PK_HCGRUPINVD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCGRUPINVD_HCGRUPINVC] FOREIGN KEY ([IDHCGRUPINVC]) REFERENCES [dbo].[HCGRUPINVC] ([ID]),
    CONSTRAINT [FK_HCGRUPINVD_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [IX_HCGRUPINVD] UNIQUE NONCLUSTERED ([IDHCGRUPINVC] ASC, [CODSERIPS] ASC, [IDDESCRIPCIONRELACIONADA] ASC)
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de descripción relacionada; referencia a tabla de descripciones de contratos en VIE ERP (contract.CUPSEntityContractDescriptions); vinculación de servicios de salud con condiciones contractuales específicas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPINVD', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions) ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPINVD', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPINVD', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CUPS (Clasificación Única de Procedimientos en Salud); código estándar del servicio, procedimiento o intervención registrada en RIPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPINVD', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del CUPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPINVD', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPINVD', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de grupo de gestión de procedimientos invasivos; clave foránea hacia tabla HCGRUPINVC que agrupa procedimientos invasivos por categoría clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPINVD', @level2type = N'COLUMN', @level2name = N'IDHCGRUPINVC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla GRUGESPROINC (grupos gestion procedimientos invasivos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPINVD', @level2type = N'COLUMN', @level2name = N'IDHCGRUPINVC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPINVD', @level2type = N'COLUMN', @level2name = N'IDHCGRUPINVC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (identity) de la tabla; clave primaria clustered para registro de detalles de grupos de procedimientos invasivos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPINVD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPINVD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPINVD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los servicios o procedimientos (CUPS) asociados a cada grupo de investigación o agrupación de historia clínica. Registra qué códigos de servicio pertenecen a un grupo de exámenes, procedimientos o intervenciones definidos en la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPINVD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPINVD';
