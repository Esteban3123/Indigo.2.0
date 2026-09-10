CREATE TABLE [dbo].[HCPLAOTRPRODIAG] (
    [ID]             INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCPLAOTRPROC] INT      NOT NULL,
    [CODDIAGNO]      CHAR (4) NOT NULL,
    [PRINCIPAL]      BIT      NOT NULL,
    CONSTRAINT [PK_HCPLAOTRPRODIAG] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (bit) que marca si este es el diagnóstico principal o primario de la atención/procedimiento. Valores: 1=diagnóstico principal, 0=diagnóstico secundario o asociado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPRODIAG', @level2type = N'COLUMN', @level2name = N'PRINCIPAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para marcar el Diagnostico Principal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPRODIAG', @level2type = N'COLUMN', @level2name = N'PRINCIPAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPRODIAG', @level2type = N'COLUMN', @level2name = N'PRINCIPAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnóstico (4 caracteres, CHAR). Clave foránea a tabla INDIAGNOS. Referencia a clasificación CIE-10 u otra nomenclatura de diagnósticos clínicos, patologías, enfermedades.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPRODIAG', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPRODIAG', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPRODIAG', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de la cabecera del procedimiento/intervención en tabla HCPLAOTRPROC. Vincula este diagnóstico al procedimiento específico realizado en la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPRODIAG', @level2type = N'COLUMN', @level2name = N'IDHCPLAOTRPROC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id cabecera tabla HCPLAOTRPROC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPRODIAG', @level2type = N'COLUMN', @level2name = N'IDHCPLAOTRPROC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPRODIAG', @level2type = N'COLUMN', @level2name = N'IDHCPLAOTRPROC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY) del detalle de diagnósticos. Clave primaria de este registro de relación diagnóstico-procedimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPRODIAG', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id autonumerico detalle de Diagnosticos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPRODIAG', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPRODIAG', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diagnósticos asociados a otros procedimientos registrados en la planilla de historia clínica. Permite vincular uno o varios códigos de diagnóstico (CIE-10) a un procedimiento adicional del paciente, indicando cuál es el diagnóstico principal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPRODIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPRODIAG';
