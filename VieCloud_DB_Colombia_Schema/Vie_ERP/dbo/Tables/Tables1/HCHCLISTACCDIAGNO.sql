CREATE TABLE [dbo].[HCHCLISTACCDIAGNO] (
    [ID]          INT      IDENTITY (1, 1) NOT NULL,
    [IDHCLISTACC] INT      NOT NULL,
    [DIAGCODIGO]  CHAR (4) NOT NULL,
    CONSTRAINT [PK_HCHCLISTACCDIAGNO] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCHCLISTACCDIAGNO_HCLISTACC] FOREIGN KEY ([IDHCLISTACC]) REFERENCES [dbo].[HCLISTACC] ([CODCONSEC]),
    CONSTRAINT [FK_HCHCLISTACCDIAGNO_INDIAGNOS] FOREIGN KEY ([DIAGCODIGO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnóstico (4 caracteres, FK a INDIAGNOS). Identifica la enfermedad, condición clínica o problema de salud registrado en la historia clínica. Equivalente a CIE-10 u otro estándar diagnóstico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHCLISTACCDIAGNO', @level2type = N'COLUMN', @level2name = N'DIAGCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación con el código de los diagnósticos - tabla INDIAGNOS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHCLISTACCDIAGNO', @level2type = N'COLUMN', @level2name = N'DIAGCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHCLISTACCDIAGNO', @level2type = N'COLUMN', @level2name = N'DIAGCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la historia clínica de atención/consulta (FK a HCLISTACC). Vincula el diagnóstico a la cabecera de la consulta, ingreso o atención del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHCLISTACCDIAGNO', @level2type = N'COLUMN', @level2name = N'IDHCLISTACC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación con la tabla cabecera - HCLISTACC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHCLISTACCDIAGNO', @level2type = N'COLUMN', @level2name = N'IDHCLISTACC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHCLISTACCDIAGNO', @level2type = N'COLUMN', @level2name = N'IDHCLISTACC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (clave primaria, INT IDENTITY). Consecutivo interno que garantiza unicidad en la relación de diagnósticos por atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHCLISTACCDIAGNO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHCLISTACCDIAGNO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHCLISTACCDIAGNO', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diagnósticos (códigos CIE-10) asociados a cada lista de acceso o restricción de historia clínica. Permite vincular condiciones diagnósticas específicas a los controles de acceso definidos en la historia clínica del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHCLISTACCDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHCLISTACCDIAGNO';
