CREATE TABLE [dbo].[HCNOSERFOTROMED] (
    [ID]                  INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDETIPHIS]           CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]           NCHAR (10)                                                                       NOT NULL,
    [IPCODPACI]           VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]           CHAR (10)                                                                        NOT NULL,
    [CODCENATE]           CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]           CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]           CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECHAORDE]           DATETIME                                                                         NOT NULL,
    [CODPRODUC]           CHAR (20)                                                                        NOT NULL,
    [ADMINISTRACION]      VARCHAR (200)                                                                    NOT NULL,
    [TIPODURACION]        VARCHAR (30)                                                                     NOT NULL,
    [CANTIDAD]            INT                                                                              NOT NULL,
    [OBSERVACIONES]       VARCHAR (200)                                                                    NOT NULL,
    [PREESTADO]           INT                                                                              NOT NULL,
    [FECHACREACION]       DATETIME                                                                         NOT NULL,
    [USUARIOCREACION]     CHAR (20)                                                                        NOT NULL,
    [FECHAMODIFICACION]   DATETIME                                                                         NULL,
    [USUARIOMODIFICACION] CHAR (20)                                                                        NULL,
    [RejectionDate]       DATETIME                                                                         NULL,
    [RejectedUser]        CHAR (20)                                                                        NULL,
    [Justification]       VARCHAR (200)                                                                    NULL,
    [FolioPrescription]   VARCHAR (10)                                                                     NULL,
    CONSTRAINT [PK_HCNOSERFOTROMED] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCNOSERFOTROMED_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCNOSERFOTROMED_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCNOSERFOTROMED_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC]),
    CONSTRAINT [FK_HCNOSERFOTROMED_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCNOSERFOTROMED_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCNOSERFOTROMED_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCNOSERFOTROMED] NOCHECK CONSTRAINT [FK_HCNOSERFOTROMED_IHLISTPRO];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCNOSERFOTROMED].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCNOSERFOTROMED].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO
ALTER TABLE [dbo].[HCNOSERFOTROMED] NOCHECK CONSTRAINT [FK_HCNOSERFOTROMED_IHLISTPRO];


GO
ALTER TABLE [dbo].[HCNOSERFOTROMED] NOCHECK CONSTRAINT [FK_HCNOSERFOTROMED_IHLISTPRO];


GO
ALTER TABLE [dbo].[HCNOSERFOTROMED] NOCHECK CONSTRAINT [FK_HCNOSERFOTROMED_IHLISTPRO];


GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Folio/número de referencia en el que fue prescrito o emitido el medicamento sugerido, VARCHAR(10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'FolioPrescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Folio en el que fue prescrito el medicamento sugerido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'FolioPrescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'FolioPrescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación o motivo del rechazo del medicamento por el profesional de salud, VARCHAR(200)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'Justification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificacion del rechazo del medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'Justification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'Justification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario/profesional de salud que rechazó o desaprobó el medicamento, CHAR(20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'RejectedUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que rechazo el medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'RejectedUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'RejectedUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que fue rechazado o desaprobado el medicamento por el médico, DATETIME', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'RejectionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en que fue rechazado por el medico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'RejectionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'RejectionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario/administrador que realizó la última modificación del registro, CHAR(20) nullable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de Modificación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro, DATETIME nullable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Modificación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario/administrador que creó o registró inicialmente el medicamento en la orden, CHAR(20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación o registro inicial del medicamento en la orden, DATETIME', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la prescripción: 1=nuevo/solicitado, 2=eliminado/suspendido, INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'PREESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado -->   1: nuevo o solicitado   2 : eliminado o suspendido ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'PREESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'PREESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas, comentarios o observaciones adicionales sobre la administración del medicamento, VARCHAR(200)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'OBSERVACIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'OBSERVACIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'OBSERVACIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de dosis o unidades del medicamento por día, INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'CANTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad / Dia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'CANTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'CANTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo o duración del tratamiento (ej: días, semanas, continuo), VARCHAR(30)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'TIPODURACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo Duración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'TIPODURACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'TIPODURACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vía o forma de administración del medicamento (ej: oral, IV, intramuscular), VARCHAR(200)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'ADMINISTRACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Administración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'ADMINISTRACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'ADMINISTRACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del producto/medicamento, FK a IHLISTPRO, CHAR(20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que fue ordenado o prescrito el medicamento, DATETIME', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'FECHAORDE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la Orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'FECHAORDE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'FECHAORDE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del profesional de salud/médico que prescribe, PII ofuscado, FK a INPROFSAL, CHAR(20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional/servicio donde se prescribe el medicamento, FK a INUNIFUNC, CHAR(10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención/hospital donde se registra la prescripción, FK a ADCENATEN, CHAR(10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del Centro de Atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso o admisión del paciente asociado a la prescripción, FK a ADINGRESO, CHAR(10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (cédula, identificación, documento), PII ofuscado, FK a INPACIENT, VARCHAR(25)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número único de folio o expediente de la historia clínica, NCHAR(10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno o tipo de historia clínica, CHAR(9)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental de la orden de medicamento, INT IDENTITY', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID o Autonumérico de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra las órdenes médicas de medicamentos que NO están incluidos en el servicio (folio de órdenes fuera de cobertura o no trazadas en formulario estándar) dentro de la historia clínica. Contiene la prescripción del profesional de salud, el producto indicado, dosis, duración y estado de aprobación o rechazo de cada orden.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED';
