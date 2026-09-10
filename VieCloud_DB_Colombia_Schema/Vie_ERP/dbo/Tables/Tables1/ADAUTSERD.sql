CREATE TABLE [dbo].[ADAUTSERD] (
    [CODCONCEC]                NUMERIC (18)                                                                     NOT NULL,
    [CODSERIPS]                CHAR (20)                                                                        NOT NULL,
    [CANSERIPS]                TINYINT                                                                          NOT NULL,
    [CANSERAUT]                TINYINT                                                                          NOT NULL,
    [IPCODPACI]                VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]                CHAR (10)                                                                        NOT NULL,
    [NUMEFOLIO]                CHAR (10)                                                                        NULL,
    [CODENTIDA]                CHAR (9)                                                                         NULL,
    [JUSCLISER]                VARCHAR (MAX)                                                                    NULL,
    [UFUCODIGO]                CHAR (10)                                                                        NOT NULL,
    [UFUDESCRI]                CHAR (30)                                                                        NOT NULL,
    [MEDIOSERV]                CHAR (1)                                                                         NOT NULL,
    [PROSERIPS]                BIT                                                                              NOT NULL,
    [ESTATUSER]                CHAR (1)                                                                         NOT NULL,
    [CODDOCALM]                NUMERIC (18)                                                                     NULL,
    [TIPOPCSEL]                CHAR (1)                                                                         NOT NULL,
    [INDAUDFOR]                NUMERIC (18)                                                                     NOT NULL,
    [CHREGESTAID]              INT                                                                              NULL,
    [SOLEXTRAM]                BIT                                                                              NULL,
    [IDDESCRIPCIONRELACIONADA] INT                                                                              NULL,
    [ID]                       INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    CONSTRAINT [PK__ADAUTSER__3214EC2703E00E18] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ADAUTSERD_ADAUTSERC] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[ADAUTSERC] ([CODCONCEC]),
    CONSTRAINT [FK_ADAUTSERD_ADDOCADIC] FOREIGN KEY ([CODDOCALM]) REFERENCES [dbo].[ADDOCADIC] ([CODDOCALM]),
    CONSTRAINT [FK_ADAUTSERD_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_ADAUTSERD_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADAUTSERD].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO
CREATE NONCLUSTERED INDEX [IX_ADAUTSERD_CODCONCEC_CODSERIPS]
    ON [dbo].[ADAUTSERD]([CODCONCEC] ASC, [CODSERIPS] ASC)
    INCLUDE([CANSERIPS]);


GO
CREATE NONCLUSTERED INDEX [ADAUTSERD_CODSERIPS]
    ON [dbo].[ADAUTSERD]([CODSERIPS] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC, [NUMEFOLIO] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ADAUTSERD_NUMINGRES]
    ON [dbo].[ADAUTSERD]([NUMINGRES] ASC);


GO
ALTER INDEX [IX_ADAUTSERD_NUMINGRES]
    ON [dbo].[ADAUTSERD] DISABLE;




GO
CREATE NONCLUSTERED INDEX [IDX_ADAUTSERD_IPCODPACI_NUMINGRES]
    ON [dbo].[ADAUTSERD]([IPCODPACI] ASC, [NUMINGRES] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) que guarda el consecutivo autoincrementable de la tabla ADAUTSERD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de la descripción relacionada con el detalle de autorización de servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Descripción Relacion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que distingue origen: 0=Autorización de pacientes hospitalizados (dashboard), 1=Solicitudes extramurales desde pestaña pacientes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'SOLEXTRAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Desde dashboar autorización solicitud es desde pestaña       pacentes hospitalarios = 0 = False        Solicitudes extramurales = 1= True', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'SOLEXTRAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'SOLEXTRAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) que relaciona registros de estancia (cabecera) con servicios agregados (detalle) en historia clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'CHREGESTAID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el id que relaciona los registros de estancia (cabecera) con los servicios agregados (detalle)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'CHREGESTAID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'CHREGESTAID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de auditoría (NUMERIC 18) para trazabilidad de autorización de servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Opción seleccionada en autorización (CHAR 1): 1=Servicios, 2=Medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'TIPOPCSEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Opcion seleccionada desde la autorizacion del servicio 1- Servicios 2- Medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'TIPOPCSEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'TIPOPCSEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del documento almacenado (NUMERIC 18, FK ADDOCADIC): nombre archivo físico de autorización del servicio, PII sensible', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'CODDOCALM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del documento almacenado - Representa el nombre del archivo fisico: Esta campo se diligencia cuando se ha clasificado un archivo que corresponde a una autorizacion. Permite identificar con que documento quedo autorizado el servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'CODDOCALM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'CODDOCALM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de autorización (CHAR 1): 1=Autorizado, 2=No Autorizado, 3=Pendiente de Autorización', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'ESTATUSER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la Autorizacion del Servicio  1: Autorizado  2: No Autorizado  3: Pendiente de Autorizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'ESTATUSER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'ESTATUSER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) si el registro proviene de Historia Clínica Electrónica (HCE) o sistema RIPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'PROSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el registro viene de la Historia Clinica Electronica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'PROSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'PROSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de prestación (CHAR 1): 1=Servicio/Procedimiento, 2=Medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'MEDIOSERV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si es Un Medicamento o Un Servicio  1: Servicio  2: Medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'MEDIOSERV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'MEDIOSERV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de Unidad Funcional (CHAR 30, primeros caracteres) donde se autoriza el servicio/procedimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'UFUDESCRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la Unidad Funcional - Extrae los 30 Caracteres Iniciales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'UFUDESCRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'UFUDESCRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de Unidad Funcional (CHAR 10) identificador de centro de atención o unidad prestadora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación clínica (VARCHAR MAX) documentada para solicitud de servicio/procedimiento/medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'JUSCLISER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificacion Clinica para la solicitud del Servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'JUSCLISER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'JUSCLISER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de Entidad (CHAR 9, FK) aseguradora o entidad responsable de autorización', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio (CHAR 10) en historia clínica del paciente, referencia documental', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio de Historia Clinica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso (CHAR 10, FK ADINGRESO) que identifica atención/hospitalización del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Ingreso    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (VARCHAR 25, PII Identification_Ofuscado): cédula/documento/identificación enmascarado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de servicio/medicamento autorizado (TINYINT) para dispensación o procedimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'CANSERAUT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad de Servicio Autorizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'CANSERAUT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'CANSERAUT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de servicio/medicamento solicitado (TINYINT) en solicitud RIPS o prescripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'CANSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad de Servicio Solicitado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'CANSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'CANSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único RIPS de procedimiento/servicio (CHAR 20) o código de medicamento (tabla IHLISTPRO)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unico de Procedimientos y Servicios - Ademas Puede ser El codigo de Medicamentos (Tabla IHLISTPRO)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo interno (NUMERIC 18, PK FK ADAUTSERC) de tabla de autorización cabecera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo Interno de Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de servicios autorizados por concepto de autorización para un ingreso o atención del paciente. Controla qué servicios (procedimientos, exámenes, medicamentos según códigos CUPS/RIPS) fueron solicitados, cuántos se autorizaron y el estado de cada autorización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD';
