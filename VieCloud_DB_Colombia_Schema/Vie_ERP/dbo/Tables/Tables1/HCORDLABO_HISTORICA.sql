CREATE TABLE [dbo].[HCORDLABO_HISTORICA] (
    [AUTO] INT NOT NULL,
    [IDETIPHIS] CHAR (9) NOT NULL,
    [NUMEFOLIO] NCHAR (10) NOT NULL,
    [IPCODPACI] VARCHAR (25) NOT NULL,
    [NUMINGRES] CHAR (10) NOT NULL,
    [CODCENATE] CHAR (10) NOT NULL,
    [UFUCODIGO] CHAR (10) NOT NULL,
    [CODPROSAL] CHAR (20) NOT NULL,
    [FECORDMED] DATETIME NOT NULL,
    [CODSERIPS] CHAR (20) NOT NULL,
    [CANSERIPS] INT NOT NULL,
    [OBSSERIPS] VARCHAR (2000) NULL,
    [PRISERIPS] CHAR (1) NOT NULL,
    [ESTSERIPS] CHAR (1) NULL,
    [MANEXTPRO] BIT NOT NULL,
    [CODDIAGNO] CHAR (4) NULL,
    [INTERPRET] VARCHAR (MAX) NULL,
    [CODPROINT] CHAR (20) NULL,
    [NUMFOLINT] NCHAR (10) NULL,
    [SERREAINT] BIT NOT NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    [ESTALELAB] BIT NOT NULL,
    [FECRECMUE] DATETIME NULL,
    [NOMARCLAB] CHAR (250) NULL,
    [CONCURRE] TIMESTAMP NULL,
    [USURECMUE] CHAR (20) NULL,
    [EXMREASIT] BIT NOT NULL,
    [GENSERVICEORDER] INT NULL,
    [RESANTSUPH] TINYINT NULL,
    [FECRESANTSUPH] DATETIME NULL,
    [RESSERSIF] TINYINT NULL,
    [FECRESSERSIF] DATETIME NULL,
    [RESELIVIH] TINYINT NULL,
    [FECRESELIVIH] DATETIME NULL,
    [RESTSHNEO] TINYINT NULL,
    [FECRESTSHNEO] DATETIME NULL,
    [RESHEMOGLO] DECIMAL (18, 2) NULL,
    [FECRESHEMOGLO] DATETIME NULL,
    [FECGLISBASAL] DATETIME NULL,
    [RESCREATININA] DECIMAL (18, 2) NULL,
    [FECRESCREATININA] DATETIME NULL,
    [RESHEMGLO] DECIMAL (18, 2) NULL,
    [FECRESHEMGLO] DATETIME NULL,
    [FECMICROALBU] DATETIME NULL,
    [FECHDL] DATETIME NULL,
    [RESBASDIAG] TINYINT NULL,
    [FECRESBASDIAG] DATETIME NULL,
    [RESHDL] DECIMAL (18, 2) NULL,
    [FECCREATINUR] DATETIME NULL,
    [RESCREATINUR] DECIMAL (18, 2) NULL,
    [FECCOLESTOTAL] DATETIME NULL,
    [RESCOLESTOTAL] DECIMAL (18, 2) NULL,
    [FECLDL] DATETIME NULL,
    [RESLDL] DECIMAL (18, 2) NULL,
    [FECPTH] DATETIME NULL,
    [RESPTH] DECIMAL (18, 2) NULL,
    [FECALBUSERICA] DATETIME NULL,
    [RESALBUSERICA] DECIMAL (18, 2) NULL,
    [FECFOSFOR] DATETIME NULL,
    [RESFOSFORO] DECIMAL (18, 2) NULL,
    [RESMICROALBU] DECIMAL (18, 2) NULL,
    [PROFRESULT] NCHAR (20) NULL,
    [ESPERESULT] CHAR (3) NULL,
    [FECHARESULT] DATETIME NULL,
    [CODMOTIVOMUESTRANOCONFORME] CHAR (4) NULL,
    [OBSERMUESTRANOCONFORME] VARCHAR (MAX) NULL,
    [IDRIASCUPS] INT NULL,
    [TIPOFACTURACION] INT NULL,
    [CORRELACION] TINYINT NOT NULL,
    [OBSERVACIONCORRELA] VARCHAR (4000) NULL,
    [IDDESCRIPCIONRELACIONADA] INT NULL,
    [FECHASUGE] DATETIME NULL,
    [TraceabilityPaperworkEventsId] INT NULL,
    [TraceabilityPaperworkId] INT NULL,
    [MICROBIOLOGIA] BIT NULL,
    [SYNCMIRTH] BIT NULL,
    [CUPSEntityPanelId] INT NULL,
    [IdServerOrderDetail] INT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Histórico de órdenes de laboratorio clínico ordenadas a pacientes. Registra cada examen solicitado (CUPS), sus resultados específicos por prueba (hemoglobina, creatinina, colesterol, VIH, TSH neonatal, etc.), el estado del proceso de toma de muestra, interpretaciones y trazabilidad con el laboratorio externo o interno.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental del registro de orden de laboratorio histórica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de identificación del paciente (ej: cédula, tarjeta de identidad, pasaporte).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio o consecutivo de la orden de laboratorio en la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cédula o código del paciente, identificación, documento del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso o admisión del paciente al centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención o sede donde se generó la orden.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional (servicio, consultorio o área) que solicitó el examen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud que ordenó el examen (médico solicitante).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que el médico generó la orden médica de laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECORDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECORDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del servicio o examen de laboratorio solicitado (código CUPS).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades del examen solicitadas en la orden.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'CANSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'CANSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones o indicaciones clínicas asociadas al examen solicitado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'OBSSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'OBSSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prioridad del examen (ej: urgente, rutina).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'PRISERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'PRISERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual del servicio o examen dentro de la orden (ej: pendiente, procesado, entregado).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el examen fue enviado a un laboratorio externo (manejo externo del proceso).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnóstico CIE-10 relacionado con la solicitud del examen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Interpretación clínica o comentario del laboratorio sobre el resultado del examen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'INTERPRET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'INTERPRET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional que realizó la interpretación del resultado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'CODPROINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'CODPROINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio del resultado o informe de interpretación del laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'NUMFOLINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'NUMFOLINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el examen requiere una reinterpretación o revisión adicional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'SERREAINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'SERREAINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de auditoría forense o control de calidad del registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de alerta del laboratorio asociado a este examen (ej: valor crítico).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'ESTALELAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'ESTALELAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de recepción de la muestra biológica en el laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECRECMUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECRECMUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o código de la marcación o etiqueta asignada a la muestra en el laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'NOMARCLAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'NOMARCLAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Control de concurrencia para manejo de actualizaciones simultáneas del registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'CONCURRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'CONCURRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario o auxiliar que recibió la muestra en el laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'USURECMUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'USURECMUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el examen fue reasignado o reubicado a otro laboratorio o proceso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'EXMREASIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'EXMREASIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la orden de servicio generada en el sistema de laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado del examen de antígeno de superficie de Hepatitis B (HBsAg).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESANTSUPH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESANTSUPH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del resultado del antígeno de superficie de Hepatitis B.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECRESANTSUPH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECRESANTSUPH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado del examen de serología para sífilis (VDRL/RPR).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESSERSIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESSERSIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del resultado de la serología para sífilis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECRESSERSIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECRESSERSIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado del examen de ELISA para VIH (detección de anticuerpos VIH).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESELIVIH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESELIVIH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del resultado del examen de VIH.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECRESELIVIH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECRESELIVIH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado del tamizaje neonatal de TSH (hormona estimulante de tiroides en recién nacido).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESTSHNEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESTSHNEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del resultado del TSH neonatal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECRESTSHNEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECRESTSHNEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de hemoglobina glicosilada (HbA1c), control glucémico en diabetes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESHEMOGLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESHEMOGLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del resultado de hemoglobina glicosilada (HbA1c).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECRESHEMOGLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECRESHEMOGLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del resultado de glicemia basal (glucosa en ayunas).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECGLISBASAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECGLISBASAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de creatinina sérica, indicador de función renal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESCREATININA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESCREATININA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del resultado de creatinina sérica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECRESCREATININA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECRESCREATININA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de hemoglobina en sangre (hemograma).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESHEMGLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESHEMGLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del resultado de hemoglobina en sangre.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECRESHEMGLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECRESHEMGLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del resultado de microalbuminuria (proteína en orina, control renal).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECMICROALBU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECMICROALBU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del resultado de HDL colesterol (colesterol bueno).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECHDL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECHDL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado del diagnóstico basal o tamizaje diagnóstico inicial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESBASDIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESBASDIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del resultado del diagnóstico basal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECRESBASDIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECRESBASDIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de HDL colesterol (colesterol de alta densidad, colesterol bueno).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESHDL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESHDL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del resultado de creatinina en orina.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECCREATINUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECCREATINUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de creatinina en orina, usado para relación albúmina/creatinina.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESCREATINUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESCREATINUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del resultado de colesterol total.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECCOLESTOTAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECCOLESTOTAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de colesterol total en sangre.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESCOLESTOTAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESCOLESTOTAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del resultado de LDL colesterol (colesterol malo, de baja densidad).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECLDL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECLDL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de LDL colesterol (colesterol de baja densidad, colesterol malo).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESLDL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESLDL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del resultado de PTH (hormona paratiroidea, paratohormona).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECPTH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECPTH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de PTH (hormona paratiroidea), control en enfermedad renal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESPTH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESPTH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del resultado de albúmina sérica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECALBUSERICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECALBUSERICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de albúmina sérica (proteína en sangre).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESALBUSERICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESALBUSERICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del resultado de fósforo en sangre.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECFOSFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECFOSFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de fósforo sérico, control metabólico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESFOSFORO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESFOSFORO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado numérico de microalbuminuria (mg/g o mg/L), detección temprana de daño renal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESMICROALBU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESMICROALBU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional que registró o validó el resultado del examen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'PROFRESULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'PROFRESULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especialidad del profesional que interpretó o entregó el resultado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'ESPERESULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'ESPERESULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se registró o entregó el resultado del examen de laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECHARESULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECHARESULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del motivo por el cual la muestra fue rechazada o no conforme.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'CODMOTIVOMUESTRANOCONFORME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'CODMOTIVOMUESTRANOCONFORME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción u observación del motivo de rechazo o no conformidad de la muestra.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'OBSERMUESTRANOCONFORME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'OBSERMUESTRANOCONFORME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del código CUPS en el sistema RIPS para facturación y reporte.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'IDRIASCUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'IDRIASCUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de facturación asociado al examen (ej: EPS, particular, SOAT).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'TIPOFACTURACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'TIPOFACTURACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el examen está correlacionado con otro examen o resultado previo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'CORRELACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'CORRELACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación o justificación de la correlación entre exámenes relacionados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'OBSERVACIONCORRELA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'OBSERVACIONCORRELA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la descripción o examen relacionado en la correlación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha sugerida para la toma o realización del examen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECHASUGE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECHASUGE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del evento de trazabilidad documental asociado a la orden.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del trámite o documento de trazabilidad de la orden de laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el examen corresponde al área de microbiología (cultivos, antibiogramas).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'MICROBIOLOGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'MICROBIOLOGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el registro fue sincronizado con el motor de integración Mirth Connect (HL7/interoperabilidad).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'SYNCMIRTH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'SYNCMIRTH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del panel o paquete de CUPS configurado en la entidad para este examen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'CUPSEntityPanelId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'CUPSEntityPanelId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de orden en el servidor externo o sistema de laboratorio integrado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'IdServerOrderDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO_HISTORICA', @level2type = N'COLUMN', @level2name = N'IdServerOrderDetail';
