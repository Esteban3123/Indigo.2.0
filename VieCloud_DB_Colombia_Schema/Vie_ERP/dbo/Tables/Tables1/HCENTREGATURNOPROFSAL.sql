CREATE TABLE [dbo].[HCENTREGATURNOPROFSAL] (
    [ID]                INT                                                                           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCENTREGATURNOC] INT                                                                           NOT NULL,
    [CODPROSAL]         CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODESPECI]         CHAR (3)                                                                      NOT NULL,
    [ROLPROFESIONAL]    INT                                                                           NOT NULL,
    CONSTRAINT [PK_HCENTREGATURNOPROSAL] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCENTREGATURNOPROFSAL_HCENTREGATURNOC] FOREIGN KEY ([IDHCENTREGATURNOC]) REFERENCES [dbo].[HCENTREGATURNOC] ([ID]),
    CONSTRAINT [FK_HCENTREGATURNOPROFSAL_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCENTREGATURNOPROFSAL_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCENTREGATURNOPROFSAL].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de rol del profesional de salud en la entrega de turno: 0=Entrega turno, 1=Recibe turno. Indica si el profesional es quien transfiere o asume la responsabilidad de la atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOPROFSAL', @level2type = N'COLUMN', @level2name = N'ROLPROFESIONAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Roles   0 :  Entrega Turno  1 :  Recibe Turno  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOPROFSAL', @level2type = N'COLUMN', @level2name = N'ROLPROFESIONAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOPROFSAL', @level2type = N'COLUMN', @level2name = N'ROLPROFESIONAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la especialidad médica del profesional (ej: Medicina General, Cirugía, Pediatría). FK a tabla INESPECIA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOPROFSAL', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Especialidad ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOPROFSAL', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOPROFSAL', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (identificación PII ofuscada). FK a tabla INPROFSAL. Médico, enfermero, especialista u otro profesional de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOPROFSAL', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOPROFSAL', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOPROFSAL', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico de la entrega de turno clínico. FK a tabla HCENTREGATURNOC que agrupa profesionales en un evento de traspaso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOPROFSAL', @level2type = N'COLUMN', @level2name = N'IDHCENTREGATURNOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo IdAutonumerico de la Entrega Turno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOPROFSAL', @level2type = N'COLUMN', @level2name = N'IDHCENTREGATURNOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOPROFSAL', @level2type = N'COLUMN', @level2name = N'IDHCENTREGATURNOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico de la relación profesional-turno. Clave primaria de la tabla.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOPROFSAL', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autonumerico ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOPROFSAL', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOPROFSAL', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Profesionales de la salud asignados a un turno de entrega en historia clínica. Registra qué médico o profesional (con su especialidad y rol) participa en la entrega de turno de una atención clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOPROFSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOPROFSAL';
