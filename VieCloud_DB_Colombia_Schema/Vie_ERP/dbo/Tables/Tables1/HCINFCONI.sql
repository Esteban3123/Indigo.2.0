CREATE TABLE [dbo].[HCINFCONI] (
    [CODCONCEC]        NUMERIC (18)                                                                     NOT NULL,
    [CODPRODUC]        CHAR (20)                                                                        NOT NULL,
    [ABRPROMEZ]        CHAR (200)                                                                       NULL,
    [CONMEDMEZ]        NUMERIC (18, 2)                                                                  NOT NULL,
    [UNIMEDMED]        VARCHAR (20)                                                                     NOT NULL,
    [CANPROCAL]        INT                                                                              NOT NULL,
    [UNIMEDVOL]        CHAR (3)                                                                         NOT NULL,
    [UNIMEDPES]        CHAR (3)                                                                         NOT NULL,
    [NUMEFOLIO]        NCHAR (10)                                                                       NOT NULL,
    [IPCODPACI]        VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]        CHAR (10)                                                                        NOT NULL,
    [CODCENATE]        CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]        CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]        CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [CONMEDMFN]        NUMERIC (18, 2)                                                                  NULL,
    [UNIMEDMFN]        CHAR (3)                                                                         NULL,
    [JUSTIFICACIONPBS] VARCHAR (2000)                                                                   NULL,
    CONSTRAINT [PK_HCINFCONI_1] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC, [CODPRODUC] ASC),
    CONSTRAINT [FK_HCINFCONI_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCINFCONI_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCINFCONI_HCINFLIQC] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[HCINFLICI] ([CODCONCEC]),
    CONSTRAINT [FK_HCINFCONI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCINFCONI_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCINFCONI_INUNIMEDI] FOREIGN KEY ([UNIMEDMED]) REFERENCES [dbo].[INUNIMEDI] ([CODUNIMED])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCINFCONI].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCINFCONI].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación clínica PBS (Pos/formulario), soporte regulatorio para medicamento en mezcla, VARCHAR(2000), documentación de necesidad terapéutica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'JUSTIFICACIONPBS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificación clínica PBS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'JUSTIFICACIONPBS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'JUSTIFICACIONPBS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida concentración mezcla final, CHAR(3), referencia INUNIMEDI, volumen/peso/mg/ml', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'UNIMEDMFN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de Medida concentracion Mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'UNIMEDMFN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'UNIMEDMFN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concentración medicamento para la mezcla final, NUMERIC(18,2), valor numérico dosificación preparada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'CONMEDMFN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concentracion medicamento para la mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'CONMEDMFN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'CONMEDMFN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código profesional de la salud (médico/enfermero), VARCHAR(25) PII Ofuscado, FK INPROFES, identificación del prescriptor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código unidad funcional (piso/área/servicio), CHAR(10), FK INUNIFUNC, ubicación donde se prepara mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código centro de atención (hospital/clínica/ESE), CHAR(10), FK ADCENATEN, institución prestadora de salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número ingreso/atención (episodio hospitalario), CHAR(10), FK ADINGRESO, identificador de consulta/internación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código paciente (cédula/documento/identificación), VARCHAR(25) PII Ofuscado, FK INPACIENT, titular de la atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número folio/página, NCHAR(10), referencia documento para mezcla medicamentosa, trazabilidad RIPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad peso (gramo/kilogramo/mg), CHAR(3), medida ponderal para componentes en mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'UNIMEDPES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de Peso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'UNIMEDPES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'UNIMEDPES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad volumen (litro/mililitro/cc), CHAR(3), medida de capacidad para componentes en mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'UNIMEDVOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de Volumen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'UNIMEDVOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'UNIMEDVOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad inicial producto calculada para mezcla, INT, número de dosis/unidades dispensadas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'CANPROCAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Inicial de producto calculada para la mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'CANPROCAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'CANPROCAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad medida concentración mezcla, VARCHAR(20), FK INUNIMEDI, normalización de métrica dosificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'UNIMEDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de Medida concentracion Mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'UNIMEDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'UNIMEDMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concentración medicamento para mezcla, NUMERIC(18,2), dosis/mg/porcentaje del fármaco en preparación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'CONMEDMEZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concentracion medicamento para la mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'CONMEDMEZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'CONMEDMEZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Abreviatura producto mostrar en mezcla, VARCHAR(200), denominación corta para factura/receta/documentación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'ABRPROMEZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Abreviatura del Producto para mostrar en mezclas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'ABRPROMEZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'ABRPROMEZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código producto (medicamento/insumo), CHAR(20), identificador del fármaco/material en formulación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código consecutivo cabecera concepto mezcla, NUMERIC(18), PK referencia HCINFLICI, secuencial ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Consecutivo de la cabecera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Información de concentración y mezcla de medicamentos o productos farmacéuticos en historia clínica. Registra los componentes, cantidades, unidades y concentraciones utilizadas en la preparación de mezclas o fórmulas magistrales para un paciente en un ingreso específico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI';
