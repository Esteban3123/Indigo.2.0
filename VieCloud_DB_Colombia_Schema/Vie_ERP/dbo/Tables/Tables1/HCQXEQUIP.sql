CREATE TABLE [dbo].[HCQXEQUIP] (
    [CONSEEQQX] NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NUMEFOLIO] CHAR (10)                                                                        NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [QXPRINCIP] BIT                                                                              NOT NULL,
    [TIPOEQUIP] CHAR (1)                                                                         NOT NULL,
    [PERFILPRO] CHAR (100)                                                                       NULL,
    CONSTRAINT [PK_HCQXEQUIP] PRIMARY KEY CLUSTERED ([CONSEEQQX] ASC),
    CONSTRAINT [FK_HCQXEQUIP_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCQXEQUIP_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCQXEQUIP_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCQXEQUIP_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCQXEQUIP_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCQXEQUIP] NOCHECK CONSTRAINT [FK_HCQXEQUIP_INPROFSAL];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCQXEQUIP].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCQXEQUIP].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');



GO
CREATE NONCLUSTERED INDEX [IX_HCQXEQUIP]
    ON [dbo].[HCQXEQUIP]([IPCODPACI] ASC, [NUMINGRES] ASC, [NUMEFOLIO] ASC);


GO
ALTER INDEX [IX_HCQXEQUIP]
    ON [dbo].[HCQXEQUIP] DISABLE;




GO
CREATE NONCLUSTERED INDEX [IX_HCQXEQUIP_NUMINGRES]
    ON [dbo].[HCQXEQUIP]([NUMINGRES] ASC)
    INCLUDE([CODCENATE], [CODPROSAL], [CONSEEQQX], [IPCODPACI], [NUMEFOLIO], [PERFILPRO], [QXPRINCIP], [TIPOEQUIP], [UFUCODIGO]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de la especialidad cuando profesión es ''''6. Otro''''. Campo de texto libre que especifica la profesión del equipo quirúrgico no listado. VARCHAR(100), opcional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXEQUIP', @level2type = N'COLUMN', @level2name = N'PERFILPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuando la Profesion es 6. Otro se especifica cual..', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXEQUIP', @level2type = N'COLUMN', @level2name = N'PERFILPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXEQUIP', @level2type = N'COLUMN', @level2name = N'PERFILPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de profesional en equipo quirúrgico: 1=Cirujano, 2=Ayudante, 3=Anestesiólogo, 4=Instrumentadora, 5=Circulante, 6=Otro. CHAR(1), requerido.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXEQUIP', @level2type = N'COLUMN', @level2name = N'TIPOEQUIP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesion:  1. Cirujano  2. Ayudante  3. Anestesiologo  4. Instrumentadora  5. Circulante  6.Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXEQUIP', @level2type = N'COLUMN', @level2name = N'TIPOEQUIP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXEQUIP', @level2type = N'COLUMN', @level2name = N'TIPOEQUIP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que marca si el profesional es el Cirujano Principal de la intervención quirúrgica. TRUE=Principal, FALSE=No principal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXEQUIP', @level2type = N'COLUMN', @level2name = N'QXPRINCIP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cirujano Principal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXEQUIP', @level2type = N'COLUMN', @level2name = N'QXPRINCIP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXEQUIP', @level2type = N'COLUMN', @level2name = N'QXPRINCIP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del Profesional de la Salud (médico, cirujano, anestesiólogo). PII ofuscado. FK a INPROFSAL. VARCHAR(25) MASKED.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXEQUIP', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXEQUIP', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXEQUIP', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la Unidad Funcional donde se realiza la intervención quirúrgica (quirófano, sala de cirugía, unidad de procedimientos). FK a INUNIFUNC. CHAR(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXEQUIP', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXEQUIP', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXEQUIP', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Centro de Atención o sede donde ocurre la intervención quirúrgica. FK a ADCENATEN. CHAR(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXEQUIP', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXEQUIP', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXEQUIP', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de Ingreso del paciente a la institución. Vincula el registro con la admisión del paciente. FK a ADINGRESO. CHAR(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXEQUIP', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXEQUIP', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXEQUIP', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Paciente; identificación única (cédula, documento de identidad). PII ofuscado. FK a INPACIENT. VARCHAR(25) MASKED.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXEQUIP', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXEQUIP', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXEQUIP', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial de folio de la historia clínica quirúrgica. Referencia documental de la intervención. CHAR(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXEQUIP', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXEQUIP', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXEQUIP', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo único auto-incrementado (identity) de cada registro en tabla de equipos quirúrgicos. Clave primaria. NUMERIC(18).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXEQUIP', @level2type = N'COLUMN', @level2name = N'CONSEEQQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXEQUIP', @level2type = N'COLUMN', @level2name = N'CONSEEQQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXEQUIP', @level2type = N'COLUMN', @level2name = N'CONSEEQQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de equipos quirúrgicos asignados a una cirugía u procedimiento quirúrgico. Vincula al paciente, su ingreso y unidad funcional con el profesional de salud participante, indicando si es cirujano principal y el tipo de equipo o rol dentro del equipo quirúrgico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXEQUIP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXEQUIP';
