CREATE TABLE [Common].[ConceptGlosasUser] (
    [Id]             INT          IDENTITY (1, 1) NOT NULL,
    [ConceptGlosaId] INT          NOT NULL,
    [UserId]         INT          NOT NULL,
    [UserCode]       VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_ConceptGlosasUser] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ConceptGlosasUser_ConceptGlosas] FOREIGN KEY ([ConceptGlosaId]) REFERENCES [Common].[ConceptGlosas] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del usuario o profesional de salud vinculado a la glosa. VARCHAR(50), identificador alfanumérico para búsqueda de auditor, facturador o revisor que gestiona la glosa.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosasUser', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del usuario', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosasUser', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosasUser', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico (INT) del usuario o profesional de salud asignado a la glosa. Referencia interna al catálogo de usuarios del sistema para auditoría y trazabilidad.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosasUser', @level2type = N'COLUMN', @level2name = N'UserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del usuario', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosasUser', @level2type = N'COLUMN', @level2name = N'UserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosasUser', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del concepto de glosa asociado. Clave foránea a ConceptGlosas; vincula a motivos de rechazo, observación o ajuste de factura en RIPS, contrato o atención.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosasUser', @level2type = N'COLUMN', @level2name = N'ConceptGlosaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de glosa', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosasUser', @level2type = N'COLUMN', @level2name = N'ConceptGlosaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosasUser', @level2type = N'COLUMN', @level2name = N'ConceptGlosaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria (INT IDENTITY). Identificador único secuencial de la relación usuario-concepto-glosa. Permite rastrear qué profesional o auditor aplicó cada tipo de glosa o rechazo.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosasUser', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador único del concepto de glosas usuario.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosasUser', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosasUser', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de usuarios asociados a conceptos de glosas; indica qué usuarios tienen asignado o son responsables de un concepto de glosa específico en el proceso de auditoría y facturación.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosasUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosasUser';
