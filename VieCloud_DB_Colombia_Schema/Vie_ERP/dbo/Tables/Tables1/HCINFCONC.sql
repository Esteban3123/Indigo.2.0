CREATE TABLE [dbo].[HCINFCONC] (
    [CODCONCEC]            NUMERIC (18)                                                                     NOT NULL,
    [CODPRODUC]            CHAR (20)                                                                        NOT NULL,
    [ABRPROMEZ]            CHAR (200)                                                                       NULL,
    [CONMEDMEZ]            NUMERIC (18, 2)                                                                  NOT NULL,
    [UNIMEDMED]            VARCHAR (20)                                                                     NOT NULL,
    [CANPROCAL]            INT                                                                              NOT NULL,
    [UNIMEDVOL]            CHAR (3)                                                                         NOT NULL,
    [UNIMEDPES]            CHAR (3)                                                                         NOT NULL,
    [NUMEFOLIO]            NCHAR (10)                                                                       NOT NULL,
    [IPCODPACI]            VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]            CHAR (10)                                                                        NOT NULL,
    [CODCENATE]            CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]            CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]            CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [CONMEDMFN]            NUMERIC (18, 2)                                                                  NULL,
    [UNIMEDMFN]            CHAR (3)                                                                         NULL,
    [JUSTIFICACIONPBS]     VARCHAR (2000)                                                                   NULL,
    [CANTIDADMEZCLASMAGIS] INT                                                                              NULL,
    [NUMEROAPLICACIONES]   INT                                                                              NULL,
    CONSTRAINT [PK_HCINFCONC_1] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC, [CODPRODUC] ASC),
    CONSTRAINT [FK_HCINFCONC_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCINFCONC_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCINFCONC_HCINFLIQC] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[HCINFLIQC] ([CODCONCEC]),
    CONSTRAINT [FK_HCINFCONC_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC]),
    CONSTRAINT [FK_HCINFCONC_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCINFCONC_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCINFCONC_INUNIMEDI] FOREIGN KEY ([UNIMEDMED]) REFERENCES [dbo].[INUNIMEDI] ([CODUNIMED])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCINFCONC].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCINFCONC].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');

GO
CREATE NONCLUSTERED INDEX [Productos_Pacientes_Ix]
    ON [dbo].[HCINFCONC]([CODPRODUC] ASC)
    INCLUDE([CODCONCEC]);


GO
CREATE NONCLUSTERED INDEX [IX_HCINFCONC]
    ON [dbo].[HCINFCONC]([IPCODPACI] ASC, [NUMEFOLIO] ASC);


GO
ALTER INDEX [IX_HCINFCONC]
    ON [dbo].[HCINFCONC] DISABLE;




GO
CREATE NONCLUSTERED INDEX [_dta_index_HCINFCONC_8_1474104292__K10_K11_K1_K2]
    ON [dbo].[HCINFCONC]([IPCODPACI] ASC, [NUMINGRES] ASC, [CODCONCEC] ASC, [CODPRODUC] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de aplicaciones o dosis del medicamento/producto administradas al paciente durante el tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'NUMEROAPLICACIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Aplicaciones  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'NUMEROAPLICACIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'NUMEROAPLICACIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de mezclas magistrales a preparar; se multiplica por la cantidad unitaria de cada componente para obtener el total de medicamento requerido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'CANTIDADMEZCLASMAGIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que aplica solo para la Mezcla Magistral y hace referencia a la cantidad de mezclas que van a preparar    Ejemplo:    Cantidad de Acetaminofen: 1 para una mezcla  Cantidad de Diclofenaco:  1 para una mezcla  Cantidad de Mezclas: 3     Total:    Cantidad de Acetaminofen: 3   Cantidad de Diclofenaco: 3     Se multiplica la cantidad para una mezcla POR el valor de la cantidad de mezclas para dar el total para cada medicamento.      ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'CANTIDADMEZCLASMAGIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'CANTIDADMEZCLASMAGIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación clínica PBS (Programa de Beneficios en Salud); argumento médico para autorizar cobertura de medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'JUSTIFICACIONPBS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificación clínica PBS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'JUSTIFICACIONPBS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'JUSTIFICACIONPBS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida (mg, ml, g, etc.) para la concentración del medicamento en la mezcla magistral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'UNIMEDMFN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de Medida concentracion Mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'UNIMEDMFN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'UNIMEDMFN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concentración o dosis del medicamento en la mezcla magistral, expresada en unidad de medida específica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'CONMEDMFN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concentracion medicamento para la mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'CONMEDMFN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'CONMEDMFN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud (médico, farmacéutico) que prescribe o autoriza el medicamento; dato sensible PII', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional (servicio, área clínica, farmacia) donde se registra el medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (clínica, hospital, IPS) donde se atiende el paciente; FK a ADCENATEN', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso o admisión del paciente al centro de atención; FK a ADINGRESO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (cédula, identificación, documento); dato sensible PII ofuscado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio o receta para identificar el documento de medicación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de peso (kg, g, mg) para expresar cantidad de medicamento en la mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'UNIMEDPES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de Peso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'UNIMEDPES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'UNIMEDPES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de volumen (ml, L, cc) para expresar cantidad de medicamento en la mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'UNIMEDVOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de Volumen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'UNIMEDVOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'UNIMEDVOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad inicial de producto calculada para la mezcla magistral antes de multiplicarse', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'CANPROCAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Inicial de producto calculada para la mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'CANPROCAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'CANPROCAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida estándar (mg, ml, g, etc.) para la concentración del medicamento; FK a INUNIMEDI', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'UNIMEDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de Medida concentracion Mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'UNIMEDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'UNIMEDMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concentración o dosis del medicamento en la mezcla, expresada en unidad de medida específica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'CONMEDMEZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concentracion medicamento para la mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'CONMEDMEZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'CONMEDMEZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Abreviatura o nombre corto del producto/medicamento para visualización en mezclas magistrales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'ABRPROMEZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Abreviatura del Producto para mostrar en mezclas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'ABRPROMEZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'ABRPROMEZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto o medicamento; FK a IHLISTPRO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del consecutivo de la cabecera de concentimiento; PK con CODPRODUC, FK a HCINFLIQC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Consecutivo de la cabecera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros de concentraciones e información de mezclas o preparaciones magistrales de medicamentos en historia clínica, asociadas a un paciente, ingreso y profesional de salud. Permite documentar la composición, cantidades, unidades y justificaciones de mezclas farmacéuticas preparadas para un paciente hospitalizado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC';
