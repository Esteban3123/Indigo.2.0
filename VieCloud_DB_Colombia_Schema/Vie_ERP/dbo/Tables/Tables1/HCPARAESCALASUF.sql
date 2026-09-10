CREATE TABLE [dbo].[HCPARAESCALASUF] (
    [ID]              INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCPARAESCALAS] INT       NOT NULL,
    [UFUCODIGO]       CHAR (10) NOT NULL,
    [ESCALOBLIG]      BIT       CONSTRAINT [DF_HCPARAESCALASUF_ESCALOBLIG] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_HCPARAESCALASUF] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPARAESCALASUF_HCPARAESCALAS] FOREIGN KEY ([IDHCPARAESCALAS]) REFERENCES [dbo].[HCPARAESCALAS] ([ID]),
    CONSTRAINT [FK_HCPARAESCALASUF_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de escala obligatoria en unidad funcional (BIT: 1=Sí/obligatoria, 0=No/opcional). Determina si la escala de valoración debe ser completada obligatoriamente en evaluaciones de pacientes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALASUF', @level2type = N'COLUMN', @level2name = N'ESCALOBLIG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Escala obligatoria:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALASUF', @level2type = N'COLUMN', @level2name = N'ESCALOBLIG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALASUF', @level2type = N'COLUMN', @level2name = N'ESCALOBLIG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad funcional (FK a INUNIFUNC). Identifica el centro de atención, servicio o departamento donde aplica la escala de valoración en historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALASUF', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALASUF', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALASUF', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de relación con tabla cabecera HCPARAESCALAS (FK). Vincula a parámetros maestros de escalas de valoración clínica, diagnóstico o funcionales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALASUF', @level2type = N'COLUMN', @level2name = N'IDHCPARAESCALAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla cabecera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALASUF', @level2type = N'COLUMN', @level2name = N'IDHCPARAESCALAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALASUF', @level2type = N'COLUMN', @level2name = N'IDHCPARAESCALAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY). Clave primaria y consecutivo interno de la relación entre escalas de valoración e unidades funcionales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALASUF', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALASUF', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALASUF', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración de escalas clínicas obligatorias por unidad funcional. Indica qué escalas de valoración (dolor, caídas, úlceras, etc.) son requeridas en cada unidad de hospitalización o servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALASUF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALASUF';
