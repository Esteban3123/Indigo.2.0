CREATE TABLE [dbo].[AMBORDLAB_HISTORICA] (
    [AUTO] INT NOT NULL,
    [IPCODPACI] VARCHAR (25) NOT NULL,
    [NUMINGRES] CHAR (10) NOT NULL,
    [CODCENATE] CHAR (10) NOT NULL,
    [UFUCODIGO] CHAR (10) NULL,
    [CODPROSAL] CHAR (20) NULL,
    [FECORDMED] DATETIME NOT NULL,
    [CODSERIPS] CHAR (20) NOT NULL,
    [CANSERIPS] INT NOT NULL,
    [ESTSERIPS] CHAR (1) NOT NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    [ESTALELAB] BIT NOT NULL,
    [FECRECMUE] DATETIME NULL,
    [NOMARCLAB] CHAR (250) NULL,
    [CONCURRE] TIMESTAMP NULL,
    [USURECMUE] CHAR (20) NULL,
    [CODENTIDA] CHAR (9) NULL,
    [CODCONTRA] CHAR (6) NULL,
    [CODPANATE] CHAR (2) NULL,
    [IPFECHACO] DATETIME NOT NULL,
    [NUMCONCIT] CHAR (20) NULL,
    [OBSERVACI] VARCHAR (2000) NULL,
    [GENCAREGROUP] INT NULL,
    [GENCONENTITY] INT NULL,
    [GENINVOICE] VARCHAR (20) NULL,
    [GENINVOICEID] INT NULL,
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
    [RESMICROALBU] DECIMAL (18, 2) NULL,
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
    [INTERPRET] VARCHAR (4000) NULL,
    [CODPROINT] CHAR (20) NULL,
    [NUMFOLINT] CHAR (10) NULL,
    [PROFRESULT] NCHAR (20) NULL,
    [ESPERESULT] CHAR (3) NULL,
    [FECHARESULT] DATETIME NULL,
    [CODMOTIVOMUESTRANOCONFORME] CHAR (4) NULL,
    [OBSERMUESTRANOCONFORME] VARCHAR (MAX) NULL,
    [IDDESCRIPCIONRELACIONADA] INT NULL,
    [MICROBIOLOGIA] BIT NULL,
    [SYNCMIRTH] BIT NULL,
    [IdServerOrderDetail] INT NULL,
    [GENSERVICEORDER] INT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Historial de órdenes de laboratorio ambulatorio: registra las solicitudes de exámenes de laboratorio ordenados a pacientes en consulta externa, incluyendo el estado de la orden, recepción de muestras, resultados de exámenes clínicos específicos (hemoglobina, creatinina, colesterol, microalbuminuria, entre otros) y datos de facturación asociados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de la orden de laboratorio (clave primaria autoincremental).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o cédula del paciente, identificación, documento de identidad del paciente al que se le ordenó el examen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso o de atención del paciente asociado a la orden de laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención donde se generó la orden de laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional (servicio o área) que solicitó el examen de laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud que ordenó el examen de laboratorio (médico tratante).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que el médico generó la orden médica de laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECORDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECORDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del servicio o examen solicitado (código CUPS/RIPS del procedimiento de laboratorio).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades o repeticiones del examen de laboratorio solicitado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'CANSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'CANSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual del servicio de laboratorio en la orden (por ejemplo: pendiente, procesado, entregado, anulado).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de auditoría formal de la orden; número que identifica el proceso de auditoría o autorización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de alerta de laboratorio: señala si existe alguna alerta o novedad sobre el resultado del examen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'ESTALELAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'ESTALELAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de recepción de la muestra biológica en el laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECRECMUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECRECMUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o marcación del laboratorio que procesó la muestra (nombre del laboratorio externo o interno).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'NOMARCLAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'NOMARCLAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Control de concurrencia del registro; marca de tiempo para evitar conflictos de actualización simultánea.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'CONCURRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'CONCURRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que registró la recepción de la muestra en el sistema (responsable de la toma o recepción).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'USURECMUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'USURECMUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la entidad aseguradora o pagadora (EPS, ARL, aseguradora) relacionada con la atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del contrato con la entidad aseguradora bajo el cual se factura el servicio de laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'CODCONTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'CODCONTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del plan de atención o tipo de plan del paciente (POS, particular, complementario, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'CODPANATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'CODPANATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de consulta o creación del registro del paciente en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'IPFECHACO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'IPFECHACO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de la consulta o cita médica asociada a la orden de laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'NUMCONCIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'NUMCONCIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones generales sobre la orden de laboratorio o instrucciones especiales para el examen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de atención o episodio clínico al que pertenece la orden (referencia a agrupador de cuidado).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'GENCAREGROUP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'GENCAREGROUP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno de la entidad de contrato o convenio en el módulo de facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de factura generada para el cobro del servicio de laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'GENINVOICE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'GENINVOICE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno de la factura en el sistema de facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'GENINVOICEID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'GENINVOICEID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado del examen de anticuerpos anti-Toxoplasma (antígeno de superficie): valor numérico codificado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESANTSUPH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESANTSUPH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del resultado del examen de anticuerpos anti-Toxoplasma (RESANTSUPH).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECRESANTSUPH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECRESANTSUPH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de la serología para sífilis (VDRL/RPR u otro método serológico): valor numérico codificado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESSERSIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESSERSIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del resultado de la serología para sífilis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECRESSERSIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECRESSERSIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado del examen de ELISA para VIH (detección de anticuerpos/antígenos del VIH): valor numérico codificado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESELIVIH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESELIVIH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del resultado del examen de ELISA para VIH.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECRESELIVIH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECRESELIVIH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado del tamizaje neonatal (TSH neonatal u otro examen de tamiz en recién nacido): valor numérico codificado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESTSHNEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESTSHNEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del resultado del tamizaje neonatal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECRESTSHNEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECRESTSHNEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de hemoglobina glicosilada (HbA1c), indicador de control glucémico en pacientes con diabetes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESHEMOGLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESHEMOGLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del resultado de hemoglobina glicosilada (RESHEMOGLO).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECRESHEMOGLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECRESHEMOGLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del resultado de glucosa en ayunas o glicemia basal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECGLISBASAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECGLISBASAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de creatinina sérica, indicador de función renal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESCREATININA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESCREATININA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del resultado de creatinina sérica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECRESCREATININA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECRESCREATININA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de hemoglobina (hemograma), valor de concentración de hemoglobina en sangre.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESHEMGLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESHEMGLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del resultado de hemoglobina (RESHEMGLO).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECRESHEMGLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECRESHEMGLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del resultado de microalbuminuria en orina, indicador de daño renal temprano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECMICROALBU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECMICROALBU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del resultado de HDL colesterol (colesterol bueno).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECHDL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECHDL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de la glucosa basal diagnóstica o prueba diagnóstica de base: valor numérico codificado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESBASDIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESBASDIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del resultado de la glucosa basal diagnóstica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECRESBASDIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECRESBASDIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de microalbuminuria en orina (valor numérico en mg/g o mg/L).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESMICROALBU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESMICROALBU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de HDL colesterol (colesterol de alta densidad, colesterol bueno) en mg/dL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESHDL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESHDL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del resultado de creatinuria (creatinina en orina).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECCREATINUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECCREATINUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de creatinina en orina (creatinuria), utilizado para calcular el índice albúmina-creatinina.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESCREATINUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESCREATINUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del resultado de colesterol total.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECCOLESTOTAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECCOLESTOTAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de colesterol total en sangre (mg/dL).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESCOLESTOTAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESCOLESTOTAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del resultado de LDL colesterol (colesterol malo).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECLDL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECLDL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de LDL colesterol (colesterol de baja densidad, colesterol malo) en mg/dL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESLDL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESLDL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del resultado de PTH (parathormona o hormona paratiroidea).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECPTH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECPTH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de PTH (parathormona), indicador del metabolismo del calcio y del fósforo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESPTH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESPTH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del resultado de albúmina sérica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECALBUSERICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECALBUSERICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de albúmina sérica (proteína plasmática), indicador nutricional y de función hepática.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESALBUSERICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESALBUSERICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del resultado de fósforo sérico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECFOSFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECFOSFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de fósforo sérico (fosfatemia), indicador del metabolismo mineral.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESFOSFORO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESFOSFORO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Interpretación clínica del resultado del laboratorio emitida por el profesional responsable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'INTERPRET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'INTERPRET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud que realizó la interpretación del resultado de laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'CODPROINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'CODPROINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio o consecutivo de la interpretación del resultado de laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'NUMFOLINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'NUMFOLINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o nombre del profesional que entregó o validó el resultado del laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'PROFRESULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'PROFRESULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de espera o disponibilidad del resultado del laboratorio (por ejemplo: pendiente, disponible, parcial).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'ESPERESULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'ESPERESULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que el resultado del laboratorio fue registrado o entregado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECHARESULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECHARESULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del motivo por el cual la muestra fue considerada no conforme o rechazada (muestra inadecuada).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'CODMOTIVOMUESTRANOCONFORME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'CODMOTIVOMUESTRANOCONFORME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones detalladas sobre el rechazo o no conformidad de la muestra biológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'OBSERMUESTRANOCONFORME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'OBSERMUESTRANOCONFORME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de una descripción o registro relacionado con la orden de laboratorio (referencia cruzada).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de si la orden corresponde a un examen de microbiología (cultivo, antibiograma, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'MICROBIOLOGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'MICROBIOLOGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de sincronización con el motor de integración Mirth Connect (intercambio de mensajes HL7 u otros estándares).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'SYNCMIRTH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'SYNCMIRTH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de la orden en el servidor de laboratorio externo o LIS (Laboratory Information System).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'IdServerOrderDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'IdServerOrderDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno de la orden de servicio generada en el módulo de facturación o gestión de servicios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB_HISTORICA', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';
