CREATE TABLE [dbo].[PADCONTROL] (
    [ID]              INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCHISPACA]     INT                                                                              NOT NULL,
    [CODCENATE]       CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]       CHAR (10)                                                                        NOT NULL,
    [NUMINGRES]       CHAR (10)                                                                        NOT NULL,
    [IPCODPACI]       VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMEFOLIO]       CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]       CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [ESTADO]          TINYINT                                                                          NOT NULL,
    [FECHAREGISTRO]   DATETIME                                                                         NOT NULL,
    [CODUSUARIO]      CHAR (20)                                                                        NOT NULL,
    [MOTIVOSUSP]      VARCHAR (MAX)                                                                    NULL,
    [FECHASUSP]       DATETIME                                                                         NULL,
    [CODUSUARIOSUSP]  CHAR (20)                                                                        NULL,
    [TIPOATENCION]    INT                                                                              NULL,
    [IDREGISTROPADRE] INT                                                                              NULL,
    CONSTRAINT [PK_PHDCONTROL] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PADCONTROL_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_PHDCONTROL_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_PHDCONTROL_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_PHDCONTROL_HCHISPACA] FOREIGN KEY ([IDHCHISPACA]) REFERENCES [dbo].[HCHISPACA] ([ID]),
    CONSTRAINT [FK_PHDCONTROL_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_PHDCONTROL_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[PADCONTROL] NOCHECK CONSTRAINT [FK_PHDCONTROL_INPROFSAL];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[PADCONTROL].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[PADCONTROL].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
CREATE NONCLUSTERED INDEX [IX_INDICE_PADCONTROL]
    ON [dbo].[PADCONTROL]([NUMEFOLIO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del registro PAD padre que fue modificado; permite rastrear la cadena de cambios y auditoría de versiones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'IDREGISTROPADRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guardamos el id del registro que se modificó.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'IDREGISTROPADRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'IDREGISTROPADRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de atención domiciliaria: 1=Atención domiciliaria, 2=Hospitalización en casa (programa de cuidado en domicilio).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'TIPOATENCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Opcion de atencion domiciliaria   1: Atención domiciliaria   2: Hospitalización en casa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'TIPOATENCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'TIPOATENCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario que realizó la suspensión de la orden PAD; identificación del profesional o administrador que suspendió.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'CODUSUARIOSUSP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo usuario de suspensión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'CODUSUARIOSUSP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'CODUSUARIOSUSP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se suspendió la orden PAD (programa de atención domiciliaria).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'FECHASUSP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de suspensión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'FECHASUSP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'FECHASUSP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo o justificación de la suspensión de la orden de PAD; descripción de la causa de la interrupción del servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'MOTIVOSUSP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo de suspension de orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'MOTIVOSUSP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'MOTIVOSUSP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario creador del registro PAD; profesional o administrativo que inició la orden de atención domiciliaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'CODUSUARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'CODUSUARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'CODUSUARIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de control PAD; timestamp de entrada al sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha creacion de registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la orden PAD: 1=Registrada, 2=Suspendida, 3=Suspendida por modificación del plan de manejo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la Orden:  1 - Registrado  2 - Suspendida  3 - Suspendida a raiz de una modificación del PAD en el plan de manejo.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud responsable de la orden PAD (médico, enfermero, coordinador); PII ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de profesional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio o referencia de la orden PAD para control administrativo y trazabilidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del paciente (cédula, documento, código de paciente); PII ofuscado con mascara parcial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'identificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso hospitalario o administrativo asociado al control PAD; referencia a atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional (servicio, departamento) responsable de la atención domiciliaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención o institución que proporciona el servicio PAD.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo centro atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la historia clínica del paciente; referencia a registro médico completo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable de cada registro de control PAD en la tabla.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de control y seguimiento del PAD (Programa de Atención Domiciliaria), donde se guarda el estado de cada episodio de atención domiciliaria por paciente, incluyendo quién lo registró, si fue suspendido y el motivo, vinculando el ingreso, el profesional y la historia clínica correspondiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL';
