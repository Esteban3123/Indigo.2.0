CREATE TABLE [dbo].[CHCAMATRAZA] (
    [ID]            INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODICAMAS]     INT                                                                              NOT NULL,
    [CODCENATE]     CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]     CHAR (10)                                                                        NOT NULL,
    [ESTADOCAMA]    INT                                                                              NOT NULL,
    [IPCODPACI]     VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NULL,
    [NUMINGRES]     CHAR (10)                                                                        NULL,
    [IDCHREGEBLO]   INT                                                                              NULL,
    [FECHAINICIAL]  DATETIME                                                                         NOT NULL,
    [FECHAFINAL]    DATETIME                                                                         NULL,
    [FECHACREACION] DATETIME                                                                         NOT NULL,
    [CODUSUARI]     CHAR (20)                                                                        NOT NULL,
    [CODDIAGNO]     CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NULL,
    CONSTRAINT [PK_CHCAMATRAZA] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CHCAMATRAZA_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_CHCAMATRAZA_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_CHCAMATRAZA_CHCAMASHO] FOREIGN KEY ([CODICAMAS]) REFERENCES [dbo].[CHCAMASHO] ([CODICAMAS]),
    CONSTRAINT [FK_CHCAMATRAZA_CHREGEBLO] FOREIGN KEY ([IDCHREGEBLO]) REFERENCES [dbo].[CHREGEBLO] ([ID]),
    CONSTRAINT [FK_CHCAMATRAZA_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_CHCAMATRAZA_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_CHCAMATRAZA_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[CHCAMATRAZA] NOCHECK CONSTRAINT [FK_CHCAMATRAZA_INDIAGNOS];


GO
ALTER TABLE [dbo].[CHCAMATRAZA] NOCHECK CONSTRAINT [FK_CHCAMATRAZA_INPACIENT];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[CHCAMATRAZA].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[CHCAMATRAZA].[CODDIAGNO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');




GO
CREATE NONCLUSTERED INDEX [IX_CHCAMATRAZA_CODICAMAS]
    ON [dbo].[CHCAMATRAZA]([CODICAMAS] ASC)
    INCLUDE([CODCENATE], [CODDIAGNO], [CODUSUARI], [ESTADOCAMA], [FECHACREACION], [FECHAFINAL], [FECHAINICIAL], [IDCHREGEBLO], [IPCODPACI], [NUMINGRES], [UFUCODIGO]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico del paciente (CIE-10), clasificación clínica del cuadro médico. PII ofuscado, referencia a INDIAGNOS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMATRAZA', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Diagnostico del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMATRAZA', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMATRAZA', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario operador que crea o registra el cambio de estado de la cama en el sistema. Identificador del profesional de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMATRAZA', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que crea el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMATRAZA', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMATRAZA', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de trazabilidad en la tabla, auditoría del cambio de estado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMATRAZA', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creacion del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMATRAZA', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMATRAZA', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de término o cierre del estado actual de la cama, fin de ocupación o bloqueo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMATRAZA', @level2type = N'COLUMN', @level2name = N'FECHAFINAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Final del Estado de la Cama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMATRAZA', @level2type = N'COLUMN', @level2name = N'FECHAFINAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMATRAZA', @level2type = N'COLUMN', @level2name = N'FECHAFINAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de inicio del estado actual de la cama, momento en que comienza la ocupación, asignación o bloqueo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMATRAZA', @level2type = N'COLUMN', @level2name = N'FECHAINICIAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicial del Estado de la Cama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMATRAZA', @level2type = N'COLUMN', @level2name = N'FECHAINICIAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMATRAZA', @level2type = N'COLUMN', @level2name = N'FECHAINICIAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de código de bloqueo asociado (tabla CHREGEBLO), motivo de bloqueo de la cama.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMATRAZA', @level2type = N'COLUMN', @level2name = N'IDCHREGEBLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Bloqueo = id de tabla CHREGEBLO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMATRAZA', @level2type = N'COLUMN', @level2name = N'IDCHREGEBLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMATRAZA', @level2type = N'COLUMN', @level2name = N'IDCHREGEBLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso o admisión del paciente en el centro de atención, referencia a episodio de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMATRAZA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMATRAZA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMATRAZA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de identificación del paciente (cédula, documento, identificación PII). Referencia a INPACIENT, ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMATRAZA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMATRAZA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMATRAZA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado operacional de la cama: -1=Libre, -2=Asignada, -3=Inactiva, -4=Bloqueada. Tipo INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMATRAZA', @level2type = N'COLUMN', @level2name = N'ESTADOCAMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la Cama:    -1 = Libre     -  2 = Asignada   -3 = Inactiva  -4 = Bloqueada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMATRAZA', @level2type = N'COLUMN', @level2name = N'ESTADOCAMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMATRAZA', @level2type = N'COLUMN', @level2name = N'ESTADOCAMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad funcional donde se localiza la cama, departamento o servicio clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMATRAZA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMATRAZA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMATRAZA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención u hospital, sede o institución de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMATRAZA', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMATRAZA', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMATRAZA', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador único de la cama física, número de habitación/cama.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMATRAZA', @level2type = N'COLUMN', @level2name = N'CODICAMAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Cama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMATRAZA', @level2type = N'COLUMN', @level2name = N'CODICAMAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMATRAZA', @level2type = N'COLUMN', @level2name = N'CODICAMAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de registro de trazabilidad de estado de cama, auditoria histórica de cambios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMATRAZA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la trazabilidad del estado de la cama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMATRAZA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMATRAZA', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro histórico (trazabilidad) de los estados y ocupaciones de las camas hospitalarias: qué cama estuvo asignada a qué paciente, en qué ingreso, durante qué período, y cuál era el estado de la cama en cada momento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMATRAZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMATRAZA';
