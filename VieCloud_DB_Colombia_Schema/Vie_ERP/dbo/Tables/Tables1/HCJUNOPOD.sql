CREATE TABLE [dbo].[HCJUNOPOD] (
    [CODTIPPLAN] CHAR (5)   NOT NULL,
    [CODPRODUC]  CHAR (20)  NOT NULL,
    [CODPRODUH]  CHAR (20)  NOT NULL,
    [DIASTRATA]  INT        NOT NULL,
    [MEDNOMEJO]  BIT        NOT NULL,
    [MEDREADVE]  BIT        NOT NULL,
    [MEDINTOLE]  BIT        NOT NULL,
    [MEDNOINDI]  BIT        NOT NULL,
    [MEDOBSERV]  CHAR (200) NULL,
    CONSTRAINT [PK_HCJUNOPOD_1] PRIMARY KEY CLUSTERED ([CODTIPPLAN] ASC, [CODPRODUC] ASC, [CODPRODUH] ASC),
    CONSTRAINT [FK_HCJUNOPOD_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC]),
    CONSTRAINT [FK_HCJUNOPOD_IHLISTPRO1] FOREIGN KEY ([CODPRODUH]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones adicionales sobre medicamento; notas clínicas libres (CHAR 200, nullable). Registra comentarios de tolerancia, eficacia o incidencias del fármaco.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOD', @level2type = N'COLUMN', @level2name = N'MEDOBSERV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones Adicionales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOD', @level2type = N'COLUMN', @level2name = N'MEDOBSERV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOD', @level2type = N'COLUMN', @level2name = N'MEDOBSERV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de medicamento no indicado (BIT). Bandera que marca si el fármaco NO fue prescrito según protocolos clínicos o guías de práctica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOD', @level2type = N'COLUMN', @level2name = N'MEDNOINDI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Medicamento No Indicado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOD', @level2type = N'COLUMN', @level2name = N'MEDNOINDI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOD', @level2type = N'COLUMN', @level2name = N'MEDNOINDI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de intolerancia al medicamento (BIT). Marca si el paciente presentó intolerancia, reacción alérgica o incompatibilidad con el fármaco.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOD', @level2type = N'COLUMN', @level2name = N'MEDINTOLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Intolerancia al Medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOD', @level2type = N'COLUMN', @level2name = N'MEDINTOLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOD', @level2type = N'COLUMN', @level2name = N'MEDINTOLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de reacción adversa del medicamento (BIT). Bandera que registra si el fármaco produjo reacción adversa, efecto secundario o complicación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOD', @level2type = N'COLUMN', @level2name = N'MEDREADVE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Medicamento Produjo Reaccion Adversa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOD', @level2type = N'COLUMN', @level2name = N'MEDREADVE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOD', @level2type = N'COLUMN', @level2name = N'MEDREADVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de falta de mejoría con medicamento (BIT). Marca si el paciente NO presentó mejoría clínica esperada tras administración del fármaco.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOD', @level2type = N'COLUMN', @level2name = N'MEDNOMEJO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Con el Medicamento No Mejora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOD', @level2type = N'COLUMN', @level2name = N'MEDNOMEJO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOD', @level2type = N'COLUMN', @level2name = N'MEDNOMEJO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días de tratamiento (INT). Cantidad de días de duración del esquema terapéutico o dispensación del medicamento en el plan.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOD', @level2type = N'COLUMN', @level2name = N'DIASTRATA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dias de Tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOD', @level2type = N'COLUMN', @level2name = N'DIASTRATA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOD', @level2type = N'COLUMN', @level2name = N'DIASTRATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de medicamento NO POS homólogo (CHAR 20, FK IHLISTPRO). Identificador del fármaco sustituto o equivalente terapéutico fuera del plan obligatorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOD', @level2type = N'COLUMN', @level2name = N'CODPRODUH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Medicamento NO POS Homologo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOD', @level2type = N'COLUMN', @level2name = N'CODPRODUH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOD', @level2type = N'COLUMN', @level2name = N'CODPRODUH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de medicamento NO POS (CHAR 20, FK IHLISTPRO). Identificador del fármaco solicitado o prescrito no cubierto por el plan obligatorio de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Medicamento NO POS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo de plantillas cabecera (CHAR 5, PK). Referencia numérica única del documento o solicitud matriz que agrupa medicamentos del plan.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOD', @level2type = N'COLUMN', @level2name = N'CODTIPPLAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de Plantillas Cabecera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOD', @level2type = N'COLUMN', @level2name = N'CODTIPPLAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOD', @level2type = N'COLUMN', @level2name = N'CODTIPPLAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de justificaciones de no poder prescribir o continuar un medicamento dentro de un plan de salud. Guarda la razón clínica por la que un medicamento no se usa (no mejora, reacción adversa, intolerancia, no indicado) junto con observaciones adicionales, relacionando el producto original con el producto alternativo dentro de un plan terapéutico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOD';
