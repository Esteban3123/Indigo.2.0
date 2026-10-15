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
ALTER TABLE [dbo].[HCCTRACTP] NOCHECK CONSTRAINT [FK_HCCTRACTP_ADCENATEN];


GO
ALTER TABLE [dbo].[HCCTRACTP] NOCHECK CONSTRAINT [FK_HCCTRACTP_ADINGRESO];


GO
ALTER TABLE [dbo].[HCCTRACTP] NOCHECK CONSTRAINT [FK_HCCTRACTP_HCMOANULB];


GO
ALTER TABLE [dbo].[HCCTRACTP] NOCHECK CONSTRAINT [FK_HCCTRACTP_HCNIVIMPN];


GO
ALTER TABLE [dbo].[HCCTRACTP] NOCHECK CONSTRAINT [FK_HCCTRACTP_INPACIENT];


GO
ALTER TABLE [dbo].[HCCTRACTP] NOCHECK CONSTRAINT [FK_HCCTRACTP_INPROFSAL];


GO
ALTER TABLE [dbo].[HCCTRACTP] NOCHECK CONSTRAINT [FK_HCCTRACTP_INPROFSAL1];


GO
ALTER TABLE [dbo].[HCCTRACTP] NOCHECK CONSTRAINT [FK_HCCTRACTP_INUNIFUNC];


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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el identificador de un agrupador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'Identifier';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Se guarda el id del control del plan de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'IdHCPCECONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiene relación con HCCTRNOTE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'IdHCCTRNOTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cod usuario que dejó el registro en 3 o 7', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'CODPRORENORE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha en la que el registro se colocó en estado 3 o 7', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'FECRENORE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Justificacion de la no realizacion de la actividad de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'JUSTINOREAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de motivo de no realizacion de la actividad de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'CODMOTANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el IDINTACT de la tabla HCPCEINTACT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'IDINTACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si la actividad asignada fue aceptada o rechazada  True: Aceptada  False: Rechazada    Nota: Si la actividad o tarea es rechazada solo se muestra al usuario que la genero y este la puede volver a asignar o anular.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'ACEACTASI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud al que se le asigno la tarea o Actividad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'CODPROASI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si la actividad es general o esta asignada a un usuario especifico  True: Se muestra a todos los usuarios que ingresen  False: Se muestra solo al usuario especificado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'ACTGENPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Comentario General sobre la Actividad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'COMGENACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje de Completado Actividad    Nota: Valor numerico de 0 a 100, expresado en porcentaje. (Obsoleto por reactory PBI6651)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'PORCOMACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prioridad de la Actividad  1: Alta  2: Media  3: Baja (Obsoleto por reactory PBI6651)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'PRIACTPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Actual de la Actividad o Tarea  1: No comenzada  2: En Curso  3: Completada  4: A la Espera de otra Persona  5: Aplazada  6: Anulada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'ESTACTACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nivel de Importancia de la Actividad (obsoleto por refactory PBI6651)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'CODNIVIMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Asunto de la Actividad o Tarea Pendiente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'ASUACTPEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Vencimiento de la tarea o actividad (obsoleto por refactory PBI6651)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'FECVENREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y Hora del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'FECREGIST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud (Obsoleto por reactory PBI6651)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRACTP', @level2type = N'COLUMN', @level2name = N'IPCODPACI';

