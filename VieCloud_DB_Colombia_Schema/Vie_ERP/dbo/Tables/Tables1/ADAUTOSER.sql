-- =============================================================================
-- DEPRECATED (2026-06-15): Tabla legacy de control/trazabilidad de autorizaciones.
-- Controla solo por CODSERIPS (servicio/medicamento); NO soporta urgencias ni estancias.
-- Reemplazada por el nuevo modelo integral de control/trazabilidad.
-- Ver docs/architecture/MODELO-SUSCEPTIBILIDAD-Y-CONTROL.md.
-- No usar para nuevos desarrollos. DROP fisico pendiente del DBA tras migrar y validar
-- equivalencia (docs/architecture/PLAN-PRUEBAS-REGRESION.md).
-- =============================================================================
CREATE TABLE [dbo].[ADAUTOSER] (
    [CODCONCEC]                INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODSERIPS]                CHAR (20)                                                                        NOT NULL,
    [CANSERIPS]                INT                                                                              NOT NULL,
    [CANSERAUT]                TINYINT                                                                          NULL,
    [IPCODPACI]                VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODENTIDA]                CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]                NCHAR (10)                                                                       NULL,
    [NUMINGRES]                CHAR (10)                                                                        NOT NULL,
    [CODCENATE]                CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]                CHAR (10)                                                                        NOT NULL,
    [JUSCLISER]                VARCHAR (MAX)                                                                    NULL,
    [MEDIOSERV]                CHAR (1)                                                                         NOT NULL,
    [PROSERIPS]                BIT                                                                              NOT NULL,
    [PROESTADO]                CHAR (1)                                                                         NOT NULL,
    [JUSANULA]                 VARCHAR (250)                                                                    NULL,
    [INDAUDFOR]                INT                                                                              NOT NULL,
    [SERSUSCEP]                BIT                                                                              NULL,
    [CODUSUARI]                CHAR (20)                                                                        NULL,
    [CODUSUANU]                CHAR (20)                                                                        NULL,
    [TIPOSERIPS]               INT                                                                              NULL,
    [SOLINFOQX]                BIT                                                                              NULL,
    [SOLEXTRAM]                BIT                                                                              NULL,
    [FECREGIST]                DATETIME                                                                         NULL,
    [IDCareGroup]              INT                                                                              NULL,
    [IDDESCRIPCIONRELACIONADA] INT                                                                              NULL,
    CONSTRAINT [PK_ADAUTOSER] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_ADAUTOSER_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_ADAUTOSER_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_ADAUTOSER_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_ADAUTOSER_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADAUTOSER].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
CREATE NONCLUSTERED INDEX [_dta_index_ADAUTOSER_9_1814401633__K5_K8_K2_1_3_4_6_7_9_10_11_12_13_14_15_16_17_18_19]
    ON [dbo].[ADAUTOSER]([IPCODPACI] ASC, [NUMINGRES] ASC, [CODSERIPS] ASC)
    INCLUDE([CANSERAUT], [CANSERIPS], [CODCENATE], [CODCONCEC], [CODENTIDA], [CODUSUANU], [CODUSUARI], [INDAUDFOR], [JUSANULA], [JUSCLISER], [MEDIOSERV], [NUMEFOLIO], [PROESTADO], [PROSERIPS], [SERSUSCEP], [UFUCODIGO]);


GO
CREATE NONCLUSTERED INDEX [IX_ADAUTOSER_CODSERIPS_IPCODPACI_NUMINGRES_NUMEFOLIO]
    ON [dbo].[ADAUTOSER]([CODSERIPS] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC, [NUMEFOLIO] ASC)
    INCLUDE([CANSERAUT], [CANSERIPS], [CODCENATE], [CODENTIDA], [CODUSUANU], [CODUSUARI], [INDAUDFOR], [JUSANULA], [JUSCLISER], [MEDIOSERV], [PROESTADO], [PROSERIPS], [SERSUSCEP], [UFUCODIGO]);


GO
CREATE NONCLUSTERED INDEX [IX_ADAUTOSER_IPCODPACI_NUMINGRES_SERSUSCEP]
    ON [dbo].[ADAUTOSER]([IPCODPACI] ASC, [NUMINGRES] ASC, [SERSUSCEP] ASC)
    INCLUDE([CODSERIPS], [NUMEFOLIO], [TIPOSERIPS]);


GO


/* select * from Admission.AdmissionType */
CREATE TRIGGER [dbo].[tgg_ActualizarCampo_CANSERIPS] 
   ON  [dbo].[ADAUTOSER]
   AFTER INSERT, UPDATE
AS 
BEGIN

	SET NOCOUNT ON;

	update ADAUTOSER set CANSERIPS = 1 from ADAUTOSER i inner join inserted t on i.CODCONCEC = t.CODCONCEC where t.CANSERIPS = 0
END

---select * from ADAUTOSER WHERE CANSERIPS = 0 
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la descripción clínica relacionada al servicio autorizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la Descripción relacionada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de atención o centro de salud asociado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'IDCareGroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Grupo Atencion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'IDCareGroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'IDCareGroup';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro o solicitud del servicio en el sistema clínico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'FECREGIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de registro o solicitud del servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'FECREGIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'FECREGIST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador binario: servicio solicitado en modalidad extramural (domicilio, telemedicina). 1=Sí, 0=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'SOLEXTRAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Este campo identifica cuando el servicio es solicitado de manera extramural  1: true  0: false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'SOLEXTRAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'SOLEXTRAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador binario: servicio solicitado desde informe o nota quirúrgica en historia clínica. 1=Sí, 0=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'SOLINFOQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Este campo identifica cuando el servicio es solicitado desde una historia de informe quirúrgico.  1: true  0: false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'SOLINFOQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'SOLINFOQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del servicio: 1=Laboratorio, 2=Patología, 3=Imagen diagnóstica, 4=Procedimiento no quirúrgico, 5=Procedimiento quirúrgico, 6=Informe quirúrgico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'TIPOSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de servicio solicitado  1: Laboratorios  2: Patologias  3: Imagenes Diagnosticas  4: Procedimeintos no Qx  5: Procedimientos Qx  6: Informe QX    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'TIPOSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'TIPOSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario profesional que anuló la autorización del servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'CODUSUANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo usuario Anulacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'CODUSUANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'CODUSUANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario profesional que solicitó o registró la autorización', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador binario: servicio requiere autorización previa. 1=Sí, susceptible; 0=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'SERSUSCEP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Servicios Susceptibles de autorizacion 1-True 0-False.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'SERSUSCEP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'SERSUSCEP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de auditoría y control para trazabilidad de autorizaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para la auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación clínica o administrativa para anular la autorización del servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'JUSANULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificacion Anulacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'JUSANULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'JUSANULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la autorización: 1=Pendiente, 2=Solicitado, 3=Autorizado, 4=Anulado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'PROESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado Autorizacion:    1-Pendiente Solicitud    2-Solicitad    3-Autorizado     4-Anulado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'PROESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'PROESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador: registro originado en Historia Clínica Electrónica (HCE). 1=Sí, 0=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'PROSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el registro viene de la Historia Clinica Electronica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'PROSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'PROSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de prestación: 1=Servicio (procedimiento, examen), 2=Medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'MEDIOSERV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si es Un Medicamento o Un Servicio 1: Servicio   2: Medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'MEDIOSERV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'MEDIOSERV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación clínica para la solicitud del servicio o procedimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'JUSCLISER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificación clinica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'JUSCLISER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'JUSCLISER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la Unidad Funcional que solicita o autoriza el servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Centro de Atención donde se presta el servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso o admisión del paciente vinculado al servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio de remisión o referencia del servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la Entidad Aseguradora (EPS, ARP) del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Entidad a la que pertenece el paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (cédula, identificación). Campo PII ofuscado: Identification_Ofuscado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de servicios autorizados para prestar al paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'CANSERAUT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Cantidad de Servicio Autorizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'CANSERAUT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'CANSERAUT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de servicios registrados en RIPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'CANSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad de Servicio Autorizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'CANSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'CANSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del procedimiento o servicio en catálogo CUPS/RIPS; incluye medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unico de Procedimientos y Servicios - Ademas de Medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo único identificador del registro de autorización de servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo tabla autorizacion Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'[DEPRECATED 2026-06-15] Registro de servicios autorizados o solicitados para un ingreso de paciente. Controla qué procedimientos o servicios (CUPS) fueron pedidos, cuántos se solicitaron, cuántos fueron autorizados, y el estado de cada autorización frente a la entidad aseguradora. Reemplazada por el modelo integral de control/trazabilidad (ver MODELO-SUSCEPTIBILIDAD-Y-CONTROL.md).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER';
GO
EXECUTE sp_addextendedproperty @name = N'Deprecated', @value = N'2026-06-15: Reemplazada por el nuevo modelo integral de control/trazabilidad de autorizaciones (servicios/medicamentos de HC + urgencias ADATEINIU + estancias). Ver docs/architecture/MODELO-SUSCEPTIBILIDAD-Y-CONTROL.md. No usar para nuevos desarrollos. DROP fisico pendiente del DBA tras migracion y validacion de equivalencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTOSER';
