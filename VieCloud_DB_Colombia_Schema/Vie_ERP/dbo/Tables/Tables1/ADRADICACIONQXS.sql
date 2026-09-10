CREATE TABLE [dbo].[ADRADICACIONQXS] (
    [ID]                       INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDRADICACIONQX]           INT       NOT NULL,
    [CODSERIPS]                CHAR (20) NOT NULL,
    [IDDESCRIPCIONRELACIONADA] INT       NULL,
    CONSTRAINT [PK_ADRADICACIONQXS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ADRADICACIONQXS_ADRADICACIONQX] FOREIGN KEY ([IDRADICACIONQX]) REFERENCES [dbo].[ADRADICACIONQX] ([ID]),
    CONSTRAINT [FK_ADRADICACIONQXS_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de descripción relacionada con contrato; vinculación a VIE ERP (contract.CUPSEntityContractDescriptions); permite asociar detalles adicionales del servicio quirúrgico con acuerdos comerciales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXS', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions) ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXS', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXS', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de Procedimiento y Servicio (CUPS/RIPS); identificador del procedimiento quirúrgico en el registro de facturación y auditoría de salud; referencia a catálogo de servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de radicación de procedimiento quirúrgico; clave foránea que vincula cada registro con el acto quirúrgico principal (tabla ADRADICACIONQX); seguimiento de ingreso/atención hospitalaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXS', @level2type = N'COLUMN', @level2name = N'IDRADICACIONQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la Radicacion procedimiento quirúrgico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXS', @level2type = N'COLUMN', @level2name = N'IDRADICACIONQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXS', @level2type = N'COLUMN', @level2name = N'IDRADICACIONQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria de auto-incremento (IDENTITY); autonumérico que identifica unívocamente cada relación entre radicación quirúrgica y servicio CUPS en el sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXS', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de servicios (procedimientos quirúrgicos CUPS) asociados a cada radicación de cirugía. Registra los códigos de servicio vinculados a una solicitud o radicación quirúrgica, permitiendo relacionar procedimientos adicionales o descripciones complementarias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXS';
