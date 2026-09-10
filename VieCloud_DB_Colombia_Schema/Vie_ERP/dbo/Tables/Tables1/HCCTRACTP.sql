CREATE TABLE [dbo].[HCCTRACTP] (
    [IPCODPACI]      VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]      CHAR (10)                                                                        NOT NULL,
    [CODCENATE]      CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]      CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]      CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [FECREGIST]      DATETIME                                                                         NOT NULL,
    [FECVENREG]      DATETIME                                                                         NULL,
    [ASUACTPEN]      CHAR (500)                                                                       NOT NULL,
    [CODNIVIMP]      CHAR (2)                                                                         NULL,
    [ESTACTACT]      CHAR (1)                                                                         NOT NULL,
    [PRIACTPAC]      CHAR (1)                                                                         NULL,
    [PORCOMACT]      INT                                                                              NULL,
    [COMGENACT]      CHAR (500)                                                                       NOT NULL,
    [ACTGENPAC]      BIT                                                                              NOT NULL,
    [CODPROASI]      CHAR (20)                                                                        NULL,
    [ACEACTASI]      BIT                                                                              NULL,
    [IDINTACT]       INT                                                                              NULL,
    [CODMOTANU]      CHAR (4)                                                                         NULL,
    [JUSTINOREAL]    VARCHAR (500)                                                                    NULL,
    [FECRENORE]      DATETIME                                                                         NULL,
    [CODPRORENORE]   CHAR (20)                                                                        NULL,
    [ID]             INT                                                                              IDENTITY (1, 1) NOT NULL,
    [IdHCCTRNOTE]    INT                                                                              NULL,
    [IdHCPCECONTROL] INT                                                                              NULL,
    [Identifier]     UNIQUEIDENTIFIER                                                                 NULL,
    CONSTRAINT [PK_HCCTRACTP_1] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCCTRACTP_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCCTRACTP_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCCTRACTP_HCMOANULB] FOREIGN KEY ([CODMOTANU]) REFERENCES [dbo].[HCMOANULB] ([CODMOTANU]),
    CONSTRAINT [FK_HCCTRACTP_HCNIVIMPN] FOREIGN KEY ([CODNIVIMP]) REFERENCES [dbo].[HCNIVIMPN] ([CODNIVIMP]),
    CONSTRAINT [FK_HCCTRACTP_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCCTRACTP_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCCTRACTP_INPROFSAL1] FOREIGN KEY ([CODPROASI]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCCTRACTP_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCCTRACTP].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCCTRACTP].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
CREATE NONCLUSTERED INDEX [IX_HCCTRACTP_NUMINGRES_IPCODPACI_ESTACTACT_ACEACTASI_ACTGENPAC_ASUACTPEN_CODCENATE_CODMOTANU_CODNIVIMP_CODPROASI_CODPRORENORE]
    ON [dbo].[HCCTRACTP]([NUMINGRES] ASC, [IPCODPACI] ASC, [ESTACTACT] ASC)
    INCLUDE([ACEACTASI], [ACTGENPAC], [ASUACTPEN], [CODCENATE], [CODMOTANU], [CODNIVIMP], [CODPROASI], [CODPRORENORE], [CODPROSAL], [COMGENACT], [FECREGIST], [FECVENREG], [Identifier], [IdHCCTRNOTE], [IdHCPCECONTROL], [IDINTACT], [JUSTINOREAL], [PORCOMACT], [PRIACTPAC], [UFUCODIGO]);


GO
CREATE NONCLUSTERED INDEX [IX_HCCTRACTP_IPCODPACI_FECREGIST_NUMINGRES_ESTACTACT]
    ON [dbo].[HCCTRACTP]([IPCODPACI] ASC, [FECREGIST] ASC, [NUMINGRES] ASC, [ESTACTACT] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_HCCTRACTP_dHCCTRNOTE_ASUACTPEN_ESTACTACT]
    ON [dbo].[HCCTRACTP]([IdHCCTRNOTE] ASC)
    INCLUDE([ASUACTPEN], [ESTACTACT]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (GUID) del agrupador de actividades de enfermería; clave de integración semántica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'Identifier';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el identificador de un agrupador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'Identifier';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'Identifier';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del control del plan de enfermería; referencia a seguimiento y vigilancia clínica del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'IdHCPCECONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el id del control del plan de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'IdHCPCECONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'IdHCPCECONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con tabla HCCTRNOTE; vincula notas clínicas y observaciones a la actividad de enfermería', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'IdHCCTRNOTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiene relación con HCCTRNOTE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'IdHCCTRNOTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'IdHCCTRNOTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) consecutivo de la actividad de enfermería registrada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud que cambió estado a 3 (Completada) o 7 (Anulada); mascarado PII', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'CODPRORENORE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cod usuario que dejó el registro en 3 o 7', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'CODPRORENORE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'CODPRORENORE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del cambio de estado a 3 o 7 de la actividad de enfermería; audit trail', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'FECRENORE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha en la que el registro se colocó en estado 3 o 7', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'FECRENORE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'FECRENORE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación textual de por qué no se realizó la actividad o tarea de enfermería asignada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'JUSTINOREAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificacion de la no realizacion de la actividad de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'JUSTINOREAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'JUSTINOREAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del motivo de no realización o anulación de la actividad de enfermería (FK: HCMOANULB)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'CODMOTANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de motivo de no realizacion de la actividad de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'CODMOTANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'CODMOTANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la intervención de enfermería (HCPCEINTACT); vincula intervención clínica específica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'IDINTACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el IDINTACT de la tabla HCPCEINTACT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'IDINTACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'IDINTACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera: aceptación (True) o rechazo (False) de la actividad asignada; visibilidad solo al autor si rechaza', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'ACEACTASI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si la actividad asignada fue aceptada o rechazada  True: Aceptada  False: Rechazada    Nota: Si la actividad o tarea es rechazada solo se muestra al usuario que la genero y este la puede volver a asignar o anular.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'ACEACTASI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'ACEACTASI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud asignado para ejecutar la actividad o tarea de enfermería; mascarado PII', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'CODPROASI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud al que se le asigno la tarea o Actividad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'CODPROASI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'CODPROASI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera: True=visible a todos los usuarios; False=solo al profesional asignado (CODPROASI)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'ACTGENPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si la actividad es general o esta asignada a un usuario especifico  True: Se muestra a todos los usuarios que ingresen  False: Se muestra solo al usuario especificado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'ACTGENPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'ACTGENPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comentario general o notas sobre la actividad de enfermería; observaciones clínicas relevantes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'COMGENACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Comentario General sobre la Actividad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'COMGENACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'COMGENACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de avance de la actividad (0-100); INT; obsoleto por refactorización PBI6651', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'PORCOMACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de Completado Actividad    Nota: Valor numerico de 0 a 100, expresado en porcentaje. (Obsoleto por reactory PBI6651)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'PORCOMACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'PORCOMACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prioridad de la actividad (1=Alta, 2=Media, 3=Baja); CHAR(1); obsoleto por refactorización PBI6651', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'PRIACTPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prioridad de la Actividad  1: Alta  2: Media  3: Baja (Obsoleto por reactory PBI6651)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'PRIACTPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'PRIACTPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual (1=No comenzada, 2=En Curso, 3=Completada, 4=Espera, 5=Aplazada, 6=Anulada); tarea enfermería', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'ESTACTACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado Actual de la Actividad o Tarea  1: No comenzada  2: En Curso  3: Completada  4: A la Espera de otra Persona  5: Aplazada  6: Anulada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'ESTACTACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'ESTACTACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de importancia clínica de la actividad; CHAR(2); FK a HCNIVIMPN; obsoleto por PBI6651', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'CODNIVIMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nivel de Importancia de la Actividad (obsoleto por refactory PBI6651)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'CODNIVIMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'CODNIVIMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Asunto o descripción resumida de la tarea o actividad de enfermería pendiente; hasta 500 caracteres', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'ASUACTPEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Asunto de la Actividad o Tarea Pendiente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'ASUACTPEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'ASUACTPEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de vencimiento esperado de la actividad o tarea; deadline; obsoleto por refactorización PBI6651', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'FECVENREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Vencimiento de la tarea o actividad (obsoleto por refactory PBI6651)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'FECVENREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'FECVENREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación/registro de la actividad de enfermería; timestamp de auditoría', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'FECREGIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y Hora del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'FECREGIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'FECREGIST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud responsable inicial; mascarado PII; obsoleto por refactorización PBI6651', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud (Obsoleto por reactory PBI6651)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional donde se registra la actividad de enfermería; FK a INUNIFUNC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro o punto de atención clínica donde ocurre la actividad; FK a ADCENATEN', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del ingreso/admisión del paciente asociado a la actividad de enfermería; FK a ADINGRESO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente; identificación enmascarada (PII); equivale a cédula/documento; FK a INPACIENT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de actividades y acciones terapéuticas del plan de cuidado del paciente durante su ingreso. Guarda el seguimiento clínico de intervenciones, asuntos pendientes, compromisos generados y estado de cada actividad realizada o programada por el profesional de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP';
