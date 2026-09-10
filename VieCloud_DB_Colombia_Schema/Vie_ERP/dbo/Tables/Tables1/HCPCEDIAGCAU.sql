CREATE TABLE [dbo].[HCPCEDIAGCAU] (
    [IDDIAGCAU]      INT IDENTITY (1, 1) NOT NULL,
    [IDVALDIAG]      INT NOT NULL,
    [CODCAUSAPCE]    INT NOT NULL,
    [IDHCPCECONTROL] INT NOT NULL,
    CONSTRAINT [PK_HCPCEDIAGCAU] PRIMARY KEY CLUSTERED ([IDDIAGCAU] ASC),
    CONSTRAINT [FK_HCPCEDIAGCAU_HCCAUSAPCE] FOREIGN KEY ([CODCAUSAPCE]) REFERENCES [dbo].[HCCAUSAPCE] ([CODCAUSAPCE]),
    CONSTRAINT [FK_HCPCEDIAGCAU_HCPCEVALDIAG] FOREIGN KEY ([IDVALDIAG]) REFERENCES [dbo].[HCPCEVALDIAG] ([IDVALDIAG])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del control del plan de enfermería (FK a HCPCECONTROL). Vincula el diagnóstico con la causa a un registro específico de control/seguimiento del plan de cuidados de enfermería.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEDIAGCAU', @level2type = N'COLUMN', @level2name = N'IDHCPCECONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el id del control del plan de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEDIAGCAU', @level2type = N'COLUMN', @level2name = N'IDHCPCECONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEDIAGCAU', @level2type = N'COLUMN', @level2name = N'IDHCPCECONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la causa del diagnóstico (FK a HCCAUSAPCE). Clasifica la etiología, origen o factor causal asociado al diagnóstico de enfermería registrado en la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEDIAGCAU', @level2type = N'COLUMN', @level2name = N'CODCAUSAPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el  Código de la causa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEDIAGCAU', @level2type = N'COLUMN', @level2name = N'CODCAUSAPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEDIAGCAU', @level2type = N'COLUMN', @level2name = N'CODCAUSAPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del valor o evaluación del diagnóstico (FK a HCPCEVALDIAG). Referencia la evaluación clínica del diagnóstico de enfermería y su estado durante el proceso de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEDIAGCAU', @level2type = N'COLUMN', @level2name = N'IDVALDIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el id valor diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEDIAGCAU', @level2type = N'COLUMN', @level2name = N'IDVALDIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEDIAGCAU', @level2type = N'COLUMN', @level2name = N'IDVALDIAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de diagnóstico con causa (PK). Clave primaria que vincula un diagnóstico de enfermería con su causa etiológica en la historia clínica del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEDIAGCAU', @level2type = N'COLUMN', @level2name = N'IDDIAGCAU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el Id del daignostico de la causa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEDIAGCAU', @level2type = N'COLUMN', @level2name = N'IDDIAGCAU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEDIAGCAU', @level2type = N'COLUMN', @level2name = N'IDDIAGCAU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los diagnósticos validados de historia clínica con sus causas o motivos de consulta/atención, vinculando cada diagnóstico a un control de historia clínica de paciente. Permite clasificar la causa que origina cada diagnóstico registrado en la atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEDIAGCAU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEDIAGCAU';
