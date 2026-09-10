CREATE TABLE [dbo].[HCPCEPLANVAL] (
    [IDPLANVAL]    INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDPCECONTROL] INT NOT NULL,
    [CODVALORAPCE] INT NOT NULL,
    CONSTRAINT [PK_HCPCEPLANVAL] PRIMARY KEY CLUSTERED ([IDPLANVAL] ASC),
    CONSTRAINT [FK_HCPCEPLANVAL_HCPCECONTROL] FOREIGN KEY ([IDPCECONTROL]) REFERENCES [dbo].[HCPCECONTROL] ([ID]),
    CONSTRAINT [FK_HCPCEPLANVAL_HCVALORAPCE] FOREIGN KEY ([CODVALORAPCE]) REFERENCES [dbo].[HCVALORAPCE] ([CODVALORAPCE])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la valoración clínica del paciente; referencia a catálogo de valoraciones (HCVALORAPCE); identifica el tipo de evaluación médica registrada en el plan de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEPLANVAL', @level2type = N'COLUMN', @level2name = N'CODVALORAPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la valoración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEPLANVAL', @level2type = N'COLUMN', @level2name = N'CODVALORAPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEPLANVAL', @level2type = N'COLUMN', @level2name = N'CODVALORAPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del registro de control de paciente; clave foránea a HCPCECONTROL; vincula la valoración al seguimiento y monitoreo clínico del caso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEPLANVAL', @level2type = N'COLUMN', @level2name = N'IDPCECONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro de control', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEPLANVAL', @level2type = N'COLUMN', @level2name = N'IDPCECONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEPLANVAL', @level2type = N'COLUMN', @level2name = N'IDPCECONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de plan de valoración; clave primaria (IDENTITY); agrega cada valoración al historial clínico del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEPLANVAL', @level2type = N'COLUMN', @level2name = N'IDPLANVAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEPLANVAL', @level2type = N'COLUMN', @level2name = N'IDPLANVAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEPLANVAL', @level2type = N'COLUMN', @level2name = N'IDPLANVAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de valores o ítems del plan de cuidado de enfermería asignados a cada control o evaluación de enfermería. Vincula cada control de cuidado (HCPCECONTROL) con los valores o parámetros valorados durante ese plan.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEPLANVAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEPLANVAL';
