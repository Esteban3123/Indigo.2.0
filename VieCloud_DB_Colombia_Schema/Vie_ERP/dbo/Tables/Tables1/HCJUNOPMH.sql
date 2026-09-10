CREATE TABLE [dbo].[HCJUNOPMH] (
    [CODCONCEC]            CHAR (10)                                                                        NOT NULL,
    [CODPRONOP]            CHAR (20)                                                                        NOT NULL,
    [CODPRODUC]            CHAR (20)                                                                        NOT NULL,
    [IDETIPHIS]            CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]            NCHAR (10)                                                                       NOT NULL,
    [IPCODPACI]            VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]            CHAR (10)                                                                        NOT NULL,
    [CODCENATE]            CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]            CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]            CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECORDMED]            DATETIME                                                                         NOT NULL,
    [CODVIAADM]            CHAR (3)                                                                         NOT NULL,
    [CODFORMED]            VARCHAR (20)                                                                     NULL,
    [DOSISPROD]            NUMERIC (18, 2)                                                                  NULL,
    [CODUNIMED]            CHAR (3)                                                                         NULL,
    [FRECUENCI]            INT                                                                              NULL,
    [UNIFRECUE]            CHAR (1)                                                                         NULL,
    [TIPFORMED]            CHAR (3)                                                                         NULL,
    [DURACIDOS]            CHAR (20)                                                                        NOT NULL,
    [VALDURFIJ]            INT                                                                              NULL,
    [UNIDURFIJ]            CHAR (1)                                                                         NULL,
    [CANPEDPRO]            INT                                                                              NOT NULL,
    [CODDIAGNO]            CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NOT NULL,
    [DIASTRATA]            INT                                                                              NULL,
    [MEDNOMEJO]            BIT                                                                              NULL,
    [MEDREADVE]            BIT                                                                              NULL,
    [MEDINTOLE]            BIT                                                                              NULL,
    [MEDNOINDI]            BIT                                                                              NULL,
    [MEDOBSERV]            VARCHAR (500)                                                                    NULL,
    [INDAUDFOR]            NUMERIC (18)                                                                     NOT NULL,
    [IDESQUEMAONC]         INT                                                                              NULL,
    [IDHCORDQUIMIO]        INT                                                                              NULL,
    [IDHCORMEDICAMESQUEMA] INT                                                                              NULL,
    CONSTRAINT [PK_HCJUNOPMH] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC, [CODPRONOP] ASC, [CODPRODUC] ASC),
    CONSTRAINT [FK_HCJUNOPMH_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCJUNOPMH_HCORDQUIMIO] FOREIGN KEY ([IDHCORDQUIMIO]) REFERENCES [EHR].[HCORDQUIMIO] ([ID]),
    CONSTRAINT [FK_HCJUNOPMH_IDHCORMEDICAMESQUEMA] FOREIGN KEY ([IDHCORMEDICAMESQUEMA]) REFERENCES [EHR].[HCORMEDICAMESQUEMA] ([ID]),
    CONSTRAINT [FK_HCJUNOPMH_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCJUNOPMH_Schemes] FOREIGN KEY ([IDESQUEMAONC]) REFERENCES [EHR].[Schemes] ([Id])
);


GO
ALTER TABLE [dbo].[HCJUNOPMH] NOCHECK CONSTRAINT [FK_HCJUNOPMH_HCORDQUIMIO];


GO
ALTER TABLE [dbo].[HCJUNOPMH] NOCHECK CONSTRAINT [FK_HCJUNOPMH_IDHCORMEDICAMESQUEMA];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCJUNOPMH].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCJUNOPMH].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCJUNOPMH].[CODDIAGNO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');




GO
ALTER TABLE [dbo].[HCJUNOPMH] NOCHECK CONSTRAINT [FK_HCJUNOPMH_HCORDQUIMIO];


GO
ALTER TABLE [dbo].[HCJUNOPMH] NOCHECK CONSTRAINT [FK_HCJUNOPMH_IDHCORMEDICAMESQUEMA];


GO
ALTER TABLE [dbo].[HCJUNOPMH] NOCHECK CONSTRAINT [FK_HCJUNOPMH_HCORDQUIMIO];


GO
ALTER TABLE [dbo].[HCJUNOPMH] NOCHECK CONSTRAINT [FK_HCJUNOPMH_IDHCORMEDICAMESQUEMA];


GO

ALTER TABLE [dbo].[HCJUNOPMH] NOCHECK CONSTRAINT [FK_HCJUNOPMH_HCORDQUIMIO];


GO
ALTER TABLE [dbo].[HCJUNOPMH] NOCHECK CONSTRAINT [FK_HCJUNOPMH_IDHCORMEDICAMESQUEMA];


GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_HCJUNOPMH_IDHCORDQUIMIO]
    ON [dbo].[HCJUNOPMH]([IDHCORDQUIMIO] ASC);


GO
ALTER INDEX [IX_HCJUNOPMH_IDHCORDQUIMIO]
    ON [dbo].[HCJUNOPMH] DISABLE;




GO
CREATE NONCLUSTERED INDEX [IX_HCJUNOPMH_IDESQUEMAONC]
    ON [dbo].[HCJUNOPMH]([IDESQUEMAONC] ASC);


GO
ALTER INDEX [IX_HCJUNOPMH_IDESQUEMAONC]
    ON [dbo].[HCJUNOPMH] DISABLE;




GO
CREATE NONCLUSTERED INDEX [IX_HCJUNOPMH_HCORMEDICAMESQUEMA]
    ON [dbo].[HCJUNOPMH]([IDHCORMEDICAMESQUEMA] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de la orden de medicamento autorizada en esquema terapéutico; referencia FK a HCORMEDICAMESQUEMA para rastrear autorizaciones de fármacos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'IDHCORMEDICAMESQUEMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de la orden de medicamento autorizada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'IDHCORMEDICAMESQUEMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'IDHCORMEDICAMESQUEMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de la orden de quimioterapia oncológica; referencia FK a HCORDQUIMIO para órdenes de tratamiento antineoplásico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de la orden de quimioterapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de esquema oncológico autorizado; referencia FK a Schemes para protocolos de tratamiento del cáncer.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'IDESQUEMAONC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de esquema oncologico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'IDESQUEMAONC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'IDESQUEMAONC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicación de destino del paciente post-atención (NUMERIC): 1=Hospitalización, 2=Urgencias, 3=Observación, 4=Cirugía, 5=Remisión externa, 6=Morgue, 7=Consulta externa, 8=Salida; estado final de la atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicacion Paciente:  1: Orden de Hospitalizacion  2: Urgencias  3: Dejar en Observacion  4: Cirugia  5: Remitir  6: Morgue  7: Remitir a Consulta Externa  8: Salida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones clínicas (VARCHAR 500) sobre respuesta medicamentosa, efectos secundarios o notas del profesional de salud durante tratamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'MEDOBSERV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'MEDOBSERV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'MEDOBSERV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de medicamento no indicado para el diagnóstico o paciente; marca contraindicación o prescripción inapropiada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'MEDNOINDI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Medicamento No Indicado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'MEDNOINDI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'MEDNOINDI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de intolerancia al medicamento; marca reacciones alérgicas, intolerancia gástrica o hipersensibilidad confirmada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'MEDINTOLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Intolerancia al Medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'MEDINTOLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'MEDINTOLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de medicamento que produjo reacción adversa; registra efectos secundarios graves o reacciones farmacológicas negativas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'MEDREADVE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Medicamento Produjo Reaccion Adversa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'MEDREADVE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'MEDREADVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de falta de mejoría clínica con medicamento; marca ineficacia terapéutica observada durante tratamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'MEDNOMEJO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Con el Medicamento No Mejora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'MEDNOMEJO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'MEDNOMEJO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días de tratamiento (INT) para medicamento No-POS; período de cobertura especial o justificación de gasto fuera del plan.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'DIASTRATA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dias de tratamiento del medicamento No POS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'DIASTRATA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'DIASTRATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico principal (CHAR 4, enmascarado PII); referencia CIE-10 o clasificación diagnóstica que justifica orden medicamentosa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Diagnostico Principal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad pedida (INT) de medicamento en unidades; volumen prescrito de dosis, ampollas, tabletas o presentaciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'CANPEDPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Pedida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'CANPEDPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'CANPEDPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de duración fija (CHAR 1): 1=Minutos, 2=Horas, 3=Días; escala temporal de la duración del tratamiento establecida.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'UNIDURFIJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad del Valor de la duracion fija:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'UNIDURFIJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'UNIDURFIJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico (INT) de la duración fija; cantidad de unidades (minutos/horas/días) que dura el esquema medicamentoso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'VALDURFIJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la duracion fija', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'VALDURFIJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'VALDURFIJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración del tratamiento (CHAR 20); expresión en texto de período total de administración del medicamento (ej: ''''7 días'''', ''''2 semanas'''').', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'DURACIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Duracion del Tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'DURACIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'DURACIDOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de formulación (CHAR 3): 1=Peso, 2=Volumen, 3=Peso-Volumen, 4=Unidad de administración; clasificación de presentación farmacéutica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'TIPFORMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Formulacion del medicamento:  1 Peso  2 Volumen  3 Peso-Volumen  4 Unidad de Administracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'TIPFORMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'TIPFORMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de frecuencia (CHAR 1): 1=Minutos, 2=Horas, 3=Días; escala temporal entre dosis del medicamento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'UNIFRECUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad del Valor de la Frecuencia:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'UNIFRECUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'UNIFRECUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Frecuencia (INT) de administración; número de intervalos en la unidad especificada (ej: cada 8 horas = 3 veces/día).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'FRECUENCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Frecuencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'FRECUENCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'FRECUENCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida (CHAR 3); escala de dosis (mg, ml, UI, mmol, etc.) según presentación farmacéutica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'CODUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de Medida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'CODUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'CODUNIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis (NUMERIC 18,2) del medicamento por administración; cantidad en unidades de medida indicadas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'DOSISPROD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'DOSISPROD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'DOSISPROD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de forma de presentación (VARCHAR 20); identificador de presentación: solución, comprimido, cápsula, inyectable, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'CODFORMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Forma de presentacion del medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'CODFORMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'CODFORMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de vía de administración (CHAR 3); ruta de medicamento: oral, IV, IM, subcutánea, tópica, rectal, inhalatoria, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'CODVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Via de Administracion Comun', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'CODVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'CODVIAADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de solicitud de la orden (DATETIME); marca registral de cuándo el profesional de salud prescribió el medicamento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'FECORDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Solicitud de la Orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'FECORDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'FECORDMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (VARCHAR 25, enmascarado PII); referencia FK a proveedor que autoriza la orden medicamentosa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad funcional (CHAR 10); departamento, servicio o área clínica donde se ordena el medicamento (ej: oncología, urgencias).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (CHAR 10); institución hospitalaria o clínica donde se origina la orden medicamentosa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso (CHAR 10); referencia FK a ADINGRESO que vincula la orden al episodio de atención del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (VARCHAR 25, enmascarado PII); identificación única, cédula o documento que vincula orden al paciente en INPACIENT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio (NCHAR 10); identificador secuencial del formato No-POS para trazabilidad administrativa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre interno tipo de historia (CHAR 9); clasificador de tipo de registro clínico (ambulatorio, internación, urgencia, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código medicamento homólogo (CHAR 20); referencia al equivalente genérico o marca autorizada en el formulario institucional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Medicamento Homologo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código medicamento No-POS (CHAR 20); identificador único de fármaco fuera del plan obligatorio de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'CODPRONOP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Medicamento No POS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'CODPRONOP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'CODPRONOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del consecutivo del formato No-POS (CHAR 10); secuencial de orden medicamentosa especial, ej: ''''00000017''''.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Concecutivo del Formato No POS    Nota: Codigo 00000017', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Órdenes médicas de medicamentos en historia clínica (prescripciones farmacológicas). Registra cada medicamento ordenado a un paciente durante un ingreso, incluyendo dosis, vía de administración, frecuencia, duración del tratamiento y diagnóstico asociado. También captura alertas clínicas como intolerancia, reacción adversa o contraindicación, y vincula la orden a esquemas de quimioterapia cuando aplica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH';
