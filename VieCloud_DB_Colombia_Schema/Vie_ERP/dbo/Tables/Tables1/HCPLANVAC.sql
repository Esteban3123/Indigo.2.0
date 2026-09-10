CREATE TABLE [dbo].[HCPLANVAC] (
    [IPCODPACI]    VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [IDPLAVACU]    CHAR (4)                                                                         NOT NULL,
    [FECAPLVAC]    DATETIME                                                                         NULL,
    [OBSVACUNA]    CHAR (200)                                                                       NULL,
    [INDAUDFOR]    NUMERIC (18)                                                                     NOT NULL,
    [ID]           INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MTVNOAPL]     TINYINT                                                                          NULL,
    [CODCENATE]    CHAR (10)                                                                        NULL,
    [CODENTIDA]    CHAR (9)                                                                         NULL,
    [GENCONENTITY] INT                                                                              NULL,
    [LOTEDOSIS]    VARCHAR (20)                                                                     NULL,
    [LOTEJERINGA]  VARCHAR (20)                                                                     NULL,
    [NUMFOLIO]     NCHAR (10)                                                                       NULL,
    [USUREGISTRA]  VARCHAR (80)                                                                     NULL,
    [ADMOTRINS]    BIT                                                                              NULL,
    [APLICADA]     BIT                                                                              NULL,
    [NUMINGRES]    CHAR (10)                                                                        NULL,
    CONSTRAINT [PK_HCPLANVAC] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPLANVAC_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCPLANVAC_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCPLANVAC_INENTADM] FOREIGN KEY ([CODENTIDA]) REFERENCES [dbo].[INENTADM] ([CODENTADM]),
    CONSTRAINT [FK_HCPLANVAC_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCPLANVAC_INPLANVAC] FOREIGN KEY ([IDPLAVACU]) REFERENCES [dbo].[INPLANVAC] ([IDPLAVACU])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCPLANVAC].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO
CREATE NONCLUSTERED INDEX [IX_HCPLANVAC__IPCODPACI]
    ON [dbo].[HCPLANVAC]([IPCODPACI] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso o atención clínica asociado al registro de vacunación. Se completa solo cuando el registro se realiza desde Historia Clínica (HC). FK a ADINGRESO. CHAR(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ingreso que se guarda solo cuando el registro se hace  dede una Historia clinica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador binario: 1=Vacuna aplicada/administrada, 0=No aplicada. BIT. Booleano de estado de aplicación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'APLICADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que indica si la vacuna fue aplicada 1=Si 0=NO ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'APLICADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'APLICADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador binario: 1=Vacuna administrada por otra institución/tercero, 0=No. BIT. Registro de administración externa o tercerizada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'ADMOTRINS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que indica si la vacuna fue administrada por otra institución 1=Si 0=NO ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'ADMOTRINS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'ADMOTRINS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del usuario/profesional que registró la aplicación de vacuna. Se completa solo en evoluciones del control de Antecedentes. Vacío si se registró desde Formulario de Vacunas. VARCHAR(80).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'USUREGISTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que registro la vacuna  Se llena unicamente cuando  se Aplica la vacuna Mediante una evolucion en el control de Antecedentes.  Cuando queda el campo Vacio es porque lo hicieron esde el Formulario de Vacunas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'USUREGISTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'USUREGISTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio o referencia de la aplicación de vacuna. Se genera solo en evoluciones de Antecedentes. Vacío si origen es Formulario de Vacunas. NCHAR(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'NUMFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de Folio Con el que se guardo la aplicacion de la vacuna  Se llena unicamente cuando  se Aplica la vacuna Mediante una evolucion en el control de Antecedentes.  Cuando queda el campo Vacio es porque lo hicieron esde el Formulario de Vacunas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'NUMFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'NUMFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de lote identificativo de la jeringa/dispositivo de inyección usado en la vacunación. VARCHAR(20). Trazabilidad del material.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'LOTEJERINGA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Lote de la jeringa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'LOTEJERINGA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'LOTEJERINGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de lote del biológico/dosis vacunal administrado. VARCHAR(20). Trazabilidad y control de vacuna.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'LOTEDOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Lote de Dosis de la vacuna.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'LOTEDOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'LOTEDOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la entidad/empresa administradora nativa del plan de vacunación. INT. FK a entidad gestora.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Empresa administradora nativa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la entidad aseguradora, prestadora o administradora de la vacunación. CHAR(9). FK a INENTADM. Identificación de EPS, ARS o afiliadora.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro o punto de atención donde se aplicó la vacuna. CHAR(10). FK a ADCENATEN. IPS, clínica, consultorio o puesto de vacunación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del centro de atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo o razón de no aplicación de vacuna: 0=Aplicada, 1=Tradición, 2=Condición de salud, 3=Negación usuario, 4=Datos contacto desactualizados, 5=Otras razones. TINYINT. Categorización de rechazo o postergación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'MTVNOAPL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo no Aplicación  0- Aplicada  1- No se Administra por una Tradición,   2- No se Administra por una Condición de Salud,  3- No se Administra por Negación del Usuario,  4- No se Administra por tener datos del contacto del usuario no actualizado,   5- No se administra por otras razones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'MTVNOAPL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'MTVNOAPL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (clave primaria) del registro de vacunación en plan. INT IDENTITY. PK_HCPLANVAC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador o índice de auditoría y conformidad. NUMERIC(18). Campo de control y trazabilidad interna.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'CAmpo Auditoria  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones, notas clínicas o comentarios adicionales sobre la vacuna aplicada, reacciones, incidentes. CHAR(200). Campo narrativo complementario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'OBSVACUNA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion de Vacunas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'OBSVACUNA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'OBSVACUNA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se realizó la aplicación/administración de la vacuna. Opcional. DATETIME NULL. Registro temporal de vacunación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'FECAPLVAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en la que se realizo la aplicacion Opcional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'FECAPLVAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'FECAPLVAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del plan, esquema o protocolo de vacunación asociado. CHAR(4). FK a INPLANVAC. Referencia al programa o plan sanitario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'IDPLAVACU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del Plan de Vacunas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'IDPLAVACU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'IDPLAVACU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del paciente, equivalente a cédula, documento de identidad o identificación personal. VARCHAR(25) MASKED PII. FK a INPACIENT. Identificador del beneficiario vacunado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro del plan de vacunación de los pacientes: qué vacunas se les aplicaron o están programadas, cuándo, en qué centro, con qué lote y jeringa, y las observaciones clínicas relacionadas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC';
