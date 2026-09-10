CREATE TABLE [dbo].[HCEXFISICGROWTHCURVE] (
    [ID]                       INT             IDENTITY (1, 1) NOT NULL,
    [IDhcfisic]                INT             NOT NULL,
    [AgeGestationalDiagram]    VARBINARY (MAX) NULL,
    [WeightForLengthDiagram]   VARBINARY (MAX) NULL,
    [BodyMassIndexDiagram]     VARBINARY (MAX) NULL,
    [WeightForAgeDiagram]      VARBINARY (MAX) NULL,
    [HeadCircumferenceDiagram] VARBINARY (MAX) NULL,
    [LengthForAgeDiagram]      VARBINARY (MAX) NULL,
    [UterineHeightDiagram]     VARBINARY (MAX) NULL,
    CONSTRAINT [PK__HCEXFISI__3214EC277BAB0038] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCEXFISICGROWTHCURVE_HCEXFISIC] FOREIGN KEY ([IDhcfisic]) REFERENCES [dbo].[HCEXFISIC] ([NUMCONSEC])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Gráfico/diagrama de altura uterina en embarazo, almacenado como imagen binaria VARBINARY para seguimiento gestacional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISICGROWTHCURVE', @level2type = N'COLUMN', @level2name = N'UterineHeightDiagram';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda altura Uterina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISICGROWTHCURVE', @level2type = N'COLUMN', @level2name = N'UterineHeightDiagram';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISICGROWTHCURVE', @level2type = N'COLUMN', @level2name = N'UterineHeightDiagram';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Gráfico/diagrama de longitud o talla según edad del paciente, almacenado como imagen binaria para evaluación de crecimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISICGROWTHCURVE', @level2type = N'COLUMN', @level2name = N'LengthForAgeDiagram';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda longitud ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISICGROWTHCURVE', @level2type = N'COLUMN', @level2name = N'LengthForAgeDiagram';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISICGROWTHCURVE', @level2type = N'COLUMN', @level2name = N'LengthForAgeDiagram';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Gráfico/diagrama de perímetro cefálico o circunferencia de cabeza según edad, almacenado como imagen binaria para control antropométrico pediátrico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISICGROWTHCURVE', @level2type = N'COLUMN', @level2name = N'HeadCircumferenceDiagram';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda circuferencia de la cabeza', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISICGROWTHCURVE', @level2type = N'COLUMN', @level2name = N'HeadCircumferenceDiagram';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISICGROWTHCURVE', @level2type = N'COLUMN', @level2name = N'HeadCircumferenceDiagram';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Gráfico/diagrama de peso según edad del paciente, almacenado como imagen binaria para monitoreo del crecimiento y desarrollo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISICGROWTHCURVE', @level2type = N'COLUMN', @level2name = N'WeightForAgeDiagram';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda peso para la edad ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISICGROWTHCURVE', @level2type = N'COLUMN', @level2name = N'WeightForAgeDiagram';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISICGROWTHCURVE', @level2type = N'COLUMN', @level2name = N'WeightForAgeDiagram';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Gráfico/diagrama de índice de masa corporal (IMC), almacenado como imagen binaria para evaluación del estado nutricional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISICGROWTHCURVE', @level2type = N'COLUMN', @level2name = N'BodyMassIndexDiagram';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda masa de indice corporal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISICGROWTHCURVE', @level2type = N'COLUMN', @level2name = N'BodyMassIndexDiagram';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISICGROWTHCURVE', @level2type = N'COLUMN', @level2name = N'BodyMassIndexDiagram';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Gráfico/diagrama de peso según longitud/talla del paciente, almacenado como imagen binaria para screening nutricional en menores', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISICGROWTHCURVE', @level2type = N'COLUMN', @level2name = N'WeightForLengthDiagram';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el  peso de Longitud ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISICGROWTHCURVE', @level2type = N'COLUMN', @level2name = N'WeightForLengthDiagram';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISICGROWTHCURVE', @level2type = N'COLUMN', @level2name = N'WeightForLengthDiagram';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Gráfico/diagrama de edad gestacional en semanas, almacenado como imagen binaria para seguimiento obstétrico y evaluación prenatal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISICGROWTHCURVE', @level2type = N'COLUMN', @level2name = N'AgeGestationalDiagram';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda código edad gestacional diagrama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISICGROWTHCURVE', @level2type = N'COLUMN', @level2name = N'AgeGestationalDiagram';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISICGROWTHCURVE', @level2type = N'COLUMN', @level2name = N'AgeGestationalDiagram';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea que referencia el ID del examen físico (historia clínica examen físico) en tabla HCEXFISIC, vinculando curvas de crecimiento al paciente/atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISICGROWTHCURVE', @level2type = N'COLUMN', @level2name = N'IDhcfisic';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el id historia examen fisico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISICGROWTHCURVE', @level2type = N'COLUMN', @level2name = N'IDhcfisic';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISICGROWTHCURVE', @level2type = N'COLUMN', @level2name = N'IDhcfisic';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (IDENTITY) de la tabla de curvas de crecimiento, clave primaria clustered', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISICGROWTHCURVE', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISICGROWTHCURVE', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISICGROWTHCURVE', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena las curvas de crecimiento en formato de imagen binaria asociadas al examen físico de un paciente, incluyendo gráficas de peso, talla, perímetro cefálico, índice de masa corporal y edad gestacional utilizadas en el seguimiento pediátrico y obstétrico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISICGROWTHCURVE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISICGROWTHCURVE';
