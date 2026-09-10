CREATE TABLE [dbo].[HCJUNOPOP] (
    [CODPRODUC]  CHAR (20)                                                                    NOT NULL,
    [CODTIPPLAN] CHAR (5)                                                                     NOT NULL,
    [DESTIPPLAN] CHAR (200)                                                                   NOT NULL,
    [INDTERAPR]  VARCHAR (MAX)                                                                NULL,
    [RESJUSMED]  VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "SummaryHC_Ofuscado", 0)') NULL,
    [EFEADVMED]  VARCHAR (MAX)                                                                NULL,
    [RAZVENMED]  VARCHAR (MAX)                                                                NULL,
    [RIEINMPAC]  BIT                                                                          NULL,
    [AGOPOSTER]  BIT                                                                          NULL,
    [MEDUTIPAI]  BIT                                                                          NULL,
    [MEDEXPERI]  BIT                                                                          NULL,
    [USOCORINV]  BIT                                                                          NULL,
    [INDAUDFOR]  NUMERIC (18)                                                                 NOT NULL,
    CONSTRAINT [PK_HCJUNOPOP] PRIMARY KEY CLUSTERED ([CODPRODUC] ASC, [CODTIPPLAN] ASC),
    CONSTRAINT [FK_HCJUNOPOP_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCJUNOPOP].[RESJUSMED]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicación de atención del paciente: 1=Orden de hospitalización, 2=Urgencias, 3=Observación, 4=Cirugía, 5=Remisión, 6=Morgue, 7=Consulta Externa, 8=Salida. Tipo NUMERIC(18), clave de destino clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicacion Paciente:  1: Orden de Hospitalizacion  2: Urgencias  3: Dejar en Observacion  4: Cirugia  5: Remitir  6: Morgue  7: Remitir a Consulta Externa  8: Salida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: uso del medicamento se corresponde con la indicación registrada ante INVIMA (autorización regulatoria). Conformidad normativa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'USOCORINV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'El uso corresponde a la indicación registrada en el Invima ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'USOCORINV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'USOCORINV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: medicamento de experimentación, fase clínica o ensayo. Uso no convencional, requiere justificación de investigación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'MEDEXPERI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Es un medicamento de experimentación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'MEDEXPERI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'MEDEXPERI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: medicamento utilizado/comercializado en el país. Disponibilidad local confirmada, registro sanitario vigente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'MEDUTIPAI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Es un medicamento utilizado en el pais', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'MEDUTIPAI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'MEDUTIPAI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: agotadas posibilidades terapéuticas existentes. Última opción de tratamiento, criterio de excepcionalidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'AGOPOSTER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se han agotado las posibilidades terapéuticas existentes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'AGOPOSTER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'AGOPOSTER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: existe riesgo inminente para la vida del paciente. Situación crítica, urgencia vital. Justificante de uso exceptuado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'RIEINMPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Existe Riesgo Inminente para la Vida del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'RIEINMPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'RIEINMPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Explicación narrativa de razones clínicas y ventajas terapéuticas del medicamento. Beneficio esperado versus alternativas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'RAZVENMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Explicacion de Razones y Ventajas del uso de este Medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'RAZVENMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'RAZVENMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de posibles efectos adversos, reacciones indeseadas y riesgos derivados del uso del medicamento. Perfil de seguridad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'EFEADVMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Posibles Efectos Adversos que se deriven del uso de este medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'EFEADVMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'EFEADVMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resumen clínico de historia médica del paciente y justificación farmacológica del medicamento No-POS (Identificación_Ofuscado). Fundamentación médica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'RESJUSMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resumen de la Historia Clinica y Justificacion del Medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'RESJUSMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'RESJUSMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicaciones terapéuticas, patologías o diagnósticos para los cuales se justifica el uso del medicamento. Propósito clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'INDTERAPR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicaciones Terapeuticas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'INDTERAPR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'INDTERAPR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada de la plantilla de justificación para medicamentos NO-POS (no incluidos en Plan Obligatorio de Salud). Documento normativo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'DESTIPPLAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la Plantilla Justificacion Medicamento No POS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'DESTIPPLAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'DESTIPPLAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código/consecutivo de plantilla No-POS por producto (interno, ej: 00000021). CHAR(5), identificador de formato justificatorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'CODTIPPLAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de Plantillas NO POS por Producto  Consecutivo Interno {00000021}', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'CODTIPPLAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'CODTIPPLAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del medicamento No-POS. CHAR(20), clave de referencia de producto farmacéutico excepcional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Medicamento NO POS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de planes terapéuticos de medicamentos en la historia clínica, que documenta la justificación médica, efectos adversos, razones de venta y condiciones especiales de uso de cada producto farmacológico prescrito al paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP';
