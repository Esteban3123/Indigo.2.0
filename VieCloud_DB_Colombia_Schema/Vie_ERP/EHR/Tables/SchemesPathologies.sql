CREATE TABLE [EHR].[SchemesPathologies] (
    [Id]            INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SchemesId]     INT      NOT NULL,
    [PathologyCode] CHAR (4) NOT NULL,
    CONSTRAINT [PK_SchemeByPathologies] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SchemesPathologies_Schemes] FOREIGN KEY ([SchemesId]) REFERENCES [EHR].[Schemes] ([Id]),
    CONSTRAINT [FK_SchemesPathologies_SchemesPathologies] FOREIGN KEY ([PathologyCode]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnóstico (CIE-10 u otra clasificación) relacionado con la tabla INDIAGNOS; identifica la patología, enfermedad o condición clínica asociada al esquema de atención.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesPathologies', @level2type = N'COLUMN', @level2name = N'PathologyCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del diagnostico - relacion (INDIAGNOS)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesPathologies', @level2type = N'COLUMN', @level2name = N'PathologyCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesPathologies', @level2type = N'COLUMN', @level2name = N'PathologyCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del esquema de atención, tratamiento o protocolo clínico definido en la tabla EHR.Schemes; vincula la patología al plan o ruta de atención.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesPathologies', @level2type = N'COLUMN', @level2name = N'SchemesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del esquema (EHR.Schemes)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesPathologies', @level2type = N'COLUMN', @level2name = N'SchemesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesPathologies', @level2type = N'COLUMN', @level2name = N'SchemesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria autoincrementada (INT IDENTITY) que identifica únicamente cada relación entre esquema y patología en la tabla.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesPathologies', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesPathologies', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesPathologies', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona esquemas de tratamiento o protocolos clínicos con las patologías (diagnósticos) que les aplican. Permite saber qué enfermedades o condiciones están asociadas a cada esquema terapéutico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesPathologies';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesPathologies';
