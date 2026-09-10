CREATE TABLE [dbo].[HCCONOXIG] (
    [IDCONOXIG]       INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]       VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [FECHREGIS]       DATETIME                                                                         NOT NULL,
    [NUMINGRES]       CHAR (10)                                                                        NOT NULL,
    [HORAINICIA]      DATETIME                                                                         NOT NULL,
    [UFUCODIGO]       CHAR (10)                                                                        NOT NULL,
    [CODCENATE]       CHAR (10)                                                                        NOT NULL,
    [CODVIAADM]       CHAR (3)                                                                         NOT NULL,
    [LITRXMINUT]      DECIMAL (18, 2)                                                                  NOT NULL,
    [HORAFINAL]       DATETIME                                                                         NOT NULL,
    [TOTHORAS]        DECIMAL (18, 2)                                                                  NOT NULL,
    [TOTLITADM]       DECIMAL (18, 2)                                                                  NOT NULL,
    [CODPROSAL]       CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECHAREGI]       DATETIME                                                                         NOT NULL,
    [OBSERVACI]       VARCHAR (250)                                                                    NULL,
    [GENSERVICEORDER] INT                                                                              NULL,
    [GASTYPE]         TINYINT                                                                          NULL,
    [INITIALVOLUME]   INT                                                                              NULL,
    [FINALVOLUME]     INT                                                                              NULL,
    [INITIALPRESSURE] INT                                                                              NULL,
    [FINALPRESSURE]   INT                                                                              NULL,
    [BATCHCODE]       VARCHAR (20)                                                                     NULL,
    CONSTRAINT [PK_HCCONOXIG_1] PRIMARY KEY CLUSTERED ([IDCONOXIG] ASC, [IPCODPACI] ASC, [CODCENATE] ASC, [UFUCODIGO] ASC, [FECHREGIS] ASC, [HORAINICIA] ASC, [NUMINGRES] ASC),
    CONSTRAINT [FK_HCCONOXIG_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCCONOXIG_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCCONOXIG_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCCONOXIG_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCCONOXIG_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCCONOXIG].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCCONOXIG].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');



GO
CREATE NONCLUSTERED INDEX [IX_HCCONOXIG_GENSERVICEORDER]
    ON [dbo].[HCCONOXIG]([GENSERVICEORDER] ASC);


GO
ALTER INDEX [IX_HCCONOXIG_GENSERVICEORDER]
    ON [dbo].[HCCONOXIG] DISABLE;




GO
CREATE NONCLUSTERED INDEX [IX_HCCONOXIG_NUMINGRES]
    ON [dbo].[HCCONOXIG]([NUMINGRES] ASC)
    INCLUDE([CODCENATE], [CODPROSAL], [CODVIAADM], [FECHREGIS], [GENSERVICEORDER], [HORAFINAL], [HORAINICIA], [IDCONOXIG], [IPCODPACI], [LITRXMINUT], [TOTHORAS], [TOTLITADM], [UFUCODIGO]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de lote del cilindro de gas (oxígeno, óxido nítrico u oxihelio) registrado en la hoja de control de gases. VARCHAR(20), opcional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'BATCHCODE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo de Lote que se llena diligenciando la Hoja de óxido nítrico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'BATCHCODE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'BATCHCODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presión final del cilindro en libras por pulgada cuadrada (PSI) al terminar la administración de gas. INT, requerido para auditoría de consumo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'FINALPRESSURE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Presion final hoja de gases al diligenciar óxido nítrico.   Unidad de Medida libras por pulgada cuadrada (PSI) ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'FINALPRESSURE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'FINALPRESSURE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presión inicial del cilindro en libras por pulgada cuadrada (PSI) al iniciar la administración de gas. INT, requerido para cálculo de consumo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'INITIALPRESSURE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Presion inicial hoja de gases al diligenciar óxido nítrico.  Unidad de medidad libras por pulgada cuadrada(PSI)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'INITIALPRESSURE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'INITIALPRESSURE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen final en litros (L) del gas administrado al paciente. INT, registrado en la hoja de control de gases.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'FINALVOLUME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'volumen final hoja de gases al diligenciar óxido nítrico.   Unidad de medida Litros(L)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'FINALVOLUME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'FINALVOLUME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen inicial en litros (L) del gas disponible para administración. INT, medida de línea base en hoja de gases.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'INITIALVOLUME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'volumen inicial hoja de gases al diligenciar óxido nítrico.   Unidad de medida Litros(L)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'INITIALVOLUME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'INITIALVOLUME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de gas consumido: 1=Oxígeno, 2=Óxido nítrico, 3=Oxihelio. TINYINT, clasificación de gas terapéutico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'GASTYPE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que se registra desde Hoja de gases     1 - Consumo de Oxigeno.  2 - Consumo de Oxido nitrico.  3 - Consumo de Oxihelio.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'GASTYPE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'GASTYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la orden de servicio generada desde Control de Cuenta cuando se factura el consumo de gas. INT, FK a orden de servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id de la orden de servicio en la cual quedo incluido el servicio de Oxigeno, Este campo se llena cuando se genera la orden de servicio desde el formulario de Control de Cuenta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas y observaciones clínicas adicionales sobre el consumo o administración del gas al paciente. VARCHAR(250), opcional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'OBSERVACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del registro del consumo de gas en el sistema. DATETIME, auditoría de ingreso de dato.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'FECHAREGI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del registro del dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'FECHAREGI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'FECHAREGI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud (médico, enfermero) responsable de la administración del gas. VARCHAR(20) PII ofuscado, FK a INPROFSAL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'profesional responsable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total de litros administrados al paciente durante todo el período de consumo. DECIMAL(18,1), suma de volumen terapéutico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'TOTLITADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'total litros admnistrados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'TOTLITADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'TOTLITADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número total de horas de administración del gas. DECIMAL(18,1), duración del tratamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'TOTHORAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'numero total de horas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'TOTHORAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'TOTHORAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora y minuto final del consumo de oxígeno o gas. DATETIME, cierre del registro de administración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'HORAFINAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'hora final del consumo de oxigeno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'HORAFINAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'HORAFINAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flujo de gas en litros por minuto (L/min) administrados al paciente. DECIMAL(18,1), tasa de flujo terapéutico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'LITRXMINUT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'litros por minuto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'LITRXMINUT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'LITRXMINUT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vía de administración del gas: 1=Cánula nasal, 2=Ventury, 3=Ventilación mecánica. CHAR(3), ruta de ingreso de gas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'CODVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1-Canula Nasal  2- Ventury  3-Ventilacion Mecanica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'CODVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'CODVIAADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención donde se realizó la administración del gas. CHAR(10), FK a ADCENATEN.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo del centro de atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional (UCI, piso, urgencias) donde se administró el gas. CHAR(10), FK a INUNIFUNC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora y minuto inicial del consumo de oxígeno o gas. DATETIME, apertura del registro de administración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'HORAINICIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora inciaul del consumo de oxigeno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'HORAINICIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'HORAINICIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso del paciente a la institución. CHAR(10), FK a ADINGRESO, vinculación con atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de ingreso del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del registro del consumo de gas en la historia clínica. DATETIME, trazabilidad de ingreso de dato.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'FECHREGIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del registro del consumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'FECHREGIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'FECHREGIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del paciente (cédula, documento, equivalente a código de paciente). VARCHAR(25) PII ofuscado, FK a INPACIENT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (secuencial) del registro de consumo de oxígeno o gas. INT IDENTITY, clave primaria de tabla HCCONOXIG.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'IDCONOXIG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'IDCONOXIG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'IDCONOXIG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de administración de oxigenoterapia a pacientes hospitalizados. Guarda cada sesión de suministro de oxígeno u otros gases medicinales, incluyendo duración, volumen administrado, presión y litros por minuto entregados al paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG';
