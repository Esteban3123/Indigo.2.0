CREATE TABLE [dbo].[HCACIBASE] (
    [IDACIDBASE] INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]  VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]  CHAR (10)                                                                        NOT NULL,
    [FECHREGIS]  DATETIME                                                                         NOT NULL,
    [FEHORAINI]  CHAR (5)                                                                         NOT NULL,
    [CODCENATE]  CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]  CHAR (10)                                                                        NOT NULL,
    [ORDEREGIST] INT                                                                              NOT NULL,
    [CODPROSAL]  CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [MODVENTIL]  CHAR (50)                                                                        NULL,
    [VOLCORRIE]  CHAR (50)                                                                        NULL,
    [VOLMINUTO]  CHAR (50)                                                                        NULL,
    [VOLMINESP]  CHAR (50)                                                                        NULL,
    [PRESISOPO]  CHAR (50)                                                                        NULL,
    [BALANPEEP]  CHAR (50)                                                                        NULL,
    [BALANCPIP]  CHAR (50)                                                                        NULL,
    [FLUJOPICO]  CHAR (50)                                                                        NULL,
    [SENSIBILID] CHAR (50)                                                                        NULL,
    [DISTENSIBI] CHAR (50)                                                                        NULL,
    [BAPMESETA]  CHAR (50)                                                                        NULL,
    [ESTATICA]   CHAR (50)                                                                        NULL,
    [RELACIONIE] CHAR (50)                                                                        NULL,
    [ESFMAZINSP] CHAR (50)                                                                        NULL,
    [FRECPROGR]  CHAR (50)                                                                        NULL,
    [FRECESPON]  CHAR (50)                                                                        NULL,
    [TEMCASCAD]  CHAR (50)                                                                        NULL,
    [BALANFLO2]  CHAR (50)                                                                        NULL,
    [NUMEDETOT]  CHAR (50)                                                                        NULL,
    [COMIDETOT]  CHAR (50)                                                                        NULL,
    [PSSURFACT]  CHAR (50)                                                                        NULL,
    [VIBRACION]  CHAR (50)                                                                        NULL,
    [PHARTERIA]  CHAR (50)                                                                        NULL,
    [PHVENOSO]   CHAR (50)                                                                        NULL,
    [PCO2ARTE]   CHAR (50)                                                                        NULL,
    [PCO2VENOS]  CHAR (50)                                                                        NULL,
    [PO2ARTERI]  CHAR (50)                                                                        NULL,
    [PO2VENOSO]  CHAR (50)                                                                        NULL,
    [HCO3ARTER]  CHAR (50)                                                                        NULL,
    [HCO3VENOS]  CHAR (50)                                                                        NULL,
    [SATO2ARTE]  CHAR (50)                                                                        NULL,
    [SATO2VENO]  CHAR (50)                                                                        NULL,
    [PAO2FLO2]   CHAR (50)                                                                        NULL,
    [BALACCO2]   CHAR (50)                                                                        NULL,
    [BALACAO2]   CHAR (50)                                                                        NULL,
    [BALACVO2]   CHAR (50)                                                                        NULL,
    [BALADAV]    CHAR (50)                                                                        NULL,
    [BALAQSQT]   CHAR (50)                                                                        NULL,
    [SATURACO2]  CHAR (50)                                                                        NULL,
    [TOMCULTIV]  CHAR (50)                                                                        NULL,
    [CTRXTORAX]  CHAR (50)                                                                        NULL,
    [EXTUBACION] CHAR (50)                                                                        NULL,
    [FECHAREGIS] DATETIME                                                                         NULL,
    [OBSERVACIO] CHAR (200)                                                                       NULL,
    [RESITENCI]  CHAR (50)                                                                        NULL,
    [BALANCPH]   CHAR (50)                                                                        NULL,
    [BALANCPL]   CHAR (50)                                                                        NULL,
    [PESIBALON]  CHAR (50)                                                                        NULL,
    [BALANCBE]   CHAR (50)                                                                        NULL,
    [BALANCPIS]  CHAR (50)                                                                        NULL,
    [GRADIEXTR]  CHAR (50)                                                                        NULL,
    [INDICOXIG]  CHAR (50)                                                                        NULL,
    [DELTACO2]   CHAR (50)                                                                        NULL,
    [DELTAHIDR]  CHAR (50)                                                                        NULL,
    [TIEMPINSPI] CHAR (50)                                                                        NULL,
    [TIEMESPIR]  CHAR (50)                                                                        NULL,
    CONSTRAINT [PK_HCACIBASE_1] PRIMARY KEY CLUSTERED ([IDACIDBASE] ASC),
    CONSTRAINT [FK_HCACIBASE_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCACIBASE_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCACIBASE_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCACIBASE_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCACIBASE_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCACIBASE].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCACIBASE].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO



GO



GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_HCACIBASE_IPCODPACI]
    ON [dbo].[HCACIBASE]([IPCODPACI] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_HCACIBASE_IPCODPACI_NUMINGRES_FECHREGIS_CODCENATE_UFUCODIGO]
    ON [dbo].[HCACIBASE]([IPCODPACI] ASC, [NUMINGRES] ASC, [FECHREGIS] ASC, [CODCENATE] ASC, [UFUCODIGO] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_HCACIBASE_IPCODPACI_NUMINGRES_FECHREGIS_FEHORAINI_CODCENATE_UFUCODIGO]
    ON [dbo].[HCACIBASE]([IPCODPACI] ASC, [NUMINGRES] ASC, [FECHREGIS] ASC, [FEHORAINI] ASC, [CODCENATE] ASC, [UFUCODIGO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo de espiración, duración del ciclo espiratorio en ventilación mecánica (sin documentación disponible)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'TIEMESPIR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'(No se encontró documentación de este campo en la solución de crystal ni información por parte de los desarrolladores.)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'TIEMESPIR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'TIEMESPIR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo de inspiración, duración del ciclo inspiratorio en ventilación mecánica (sin documentación disponible)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'TIEMPINSPI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'(No se encontró documentación de este campo en la solución de crystal ni información por parte de los desarrolladores.)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'TIEMPINSPI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'TIEMPINSPI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Delta de hidrogeniones, diferencia de pH/protones entre compartimentos arterial y venoso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'DELTAHIDR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' DELTA DE HIDRIOGENONES', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'DELTAHIDR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'DELTAHIDR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Delta de CO2, diferencia de dióxido de carbono arteriovenoso, indicador de gasto cardíaco y metabolismo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'DELTACO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Delta de CO2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'DELTACO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'DELTACO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice de oxigenación, relación PAO2/FiO2 para evaluar función pulmonar en ventilación mecánica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'INDICOXIG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indice Oxigenacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'INDICOXIG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'INDICOXIG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Gradiente de extracción, diferencia arteriovenosa de oxígeno (A-V O2) para evaluación de oxigenación tisular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'GRADIEXTR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Gradiante de Extraccion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'GRADIEXTR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'GRADIEXTR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CPIS (Clinical Pulmonary Infection Score), puntuación clínica de infección pulmonar en ventilados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALANCPIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'CPIS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALANCPIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALANCPIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BE (Exceso de Base), parámetro de equilibrio ácido-base arterial y venoso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALANCBE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'BE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALANCBE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALANCBE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presión del balón, presión neumática del tubo endotraqueal para asegurar sello en vía aérea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'PESIBALON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Presion del Balon', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'PESIBALON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'PESIBALON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'PL (Presión Pleural), componente de presión del sistema respiratorio en ventilación mecánica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALANCPL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'PL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALANCPL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALANCPL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'pH, concentración de hidrogeniones en sangre arterial y venosa, parámetro ácido-base fundamental', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALANCPH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'PH', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALANCPH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALANCPH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resistencia, resistencia de vías aéreas y tejidos respiratorios durante ventilación mecánica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'RESITENCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resistencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'RESITENCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'RESITENCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones, anotaciones clínicas adicionales del registro de monitoreo de acido-base y ventilación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'OBSERVACIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'OBSERVACIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'OBSERVACIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de registro, timestamp completo de cuando se documenta el evento acido-base, DATETIME', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'FECHAREGIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'FECHAREGIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'FECHAREGIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Extubación, procedimiento de retiro del tubo endotraqueal y estado de desconexión de ventilador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'EXTUBACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Extubacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'EXTUBACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'EXTUBACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Control RX tórax, radiografía de control torácico para verificar posición tubo y complicaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'CTRXTORAX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Control RX ToraX', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'CTRXTORAX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'CTRXTORAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Toma de cultivo, muestra microbiológica de secreciones para diagnóstico de infección respiratoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'TOMCULTIV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Toma del Cultivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'TOMCULTIV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'TOMCULTIV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saturación de O2, SaO2 arterial, porcentaje de oxígeno unido a hemoglobina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'SATURACO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Saturacion de O2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'SATURACO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'SATURACO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'QS/QT (Shunt Pulmonar), cociente de flujo de sangre no oxigenada respecto gasto cardíaco total', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALAQSQT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'QS/QT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALAQSQT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALAQSQT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'D(A-V) o DAO2, diferencia arteriovenosa de oxígeno para valorar extracción tisular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALADAV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'D(A-V)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALADAV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALADAV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CVO2, contenido venoso de oxígeno, cantidad total de O2 en sangre venosa mixta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALACVO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'CVO2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALACVO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALACVO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CAO2, contenido arterial de oxígeno, cantidad total de O2 disuelto y en hemoglobina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALACAO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'CAO2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALACAO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALACAO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CCO2, contenido de dióxido de carbono, cantidad de CO2 en sangre arterial y venosa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALACCO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'CCO2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALACCO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALACCO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'PAO2/FiO2 o índice de oxigenación, cociente presión parcial O2 alveolar respecto fracción inspirada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'PAO2FLO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'PAO2/FLO2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'PAO2FLO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'PAO2FLO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'SaO2 venoso, saturación de oxígeno en sangre venosa mixta, indicador de extracción periférica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'SATO2VENO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'SATO2 Venoso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'SATO2VENO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'SATO2VENO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'SaO2 arterial, saturación de oxígeno en sangre arterial, indicador de oxigenación pulmonar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'SATO2ARTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'SATO2 Arterial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'SATO2ARTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'SATO2ARTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'HCO3 venoso, bicarbonato en sangre venosa para evaluación ácido-base sistémica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'HCO3VENOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'HCO3 Venoso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'HCO3VENOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'HCO3VENOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'HCO3 arterial, bicarbonato en sangre arterial, buffer principal del equilibrio ácido-base', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'HCO3ARTER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'HCO3 Arterial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'HCO3ARTER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'HCO3ARTER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'PO2 venoso, presión parcial de oxígeno en sangre venosa mixta, refleja oxigenación tisular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'PO2VENOSO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'PO2 Venoso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'PO2VENOSO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'PO2VENOSO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'PO2 arterial, presión parcial de oxígeno en sangre arterial, evaluación de hipoxemia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'PO2ARTERI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'PO2 Arterial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'PO2ARTERI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'PO2ARTERI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'PCO2 venoso, presión parcial de dióxido de carbono en sangre venosa para valoración ventilación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'PCO2VENOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'PCO2 Venoso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'PCO2VENOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'PCO2VENOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'PCO2 arterial, presión parcial de CO2 en sangre arterial, indicador de ventilación alveolar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'PCO2ARTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'PCO2 Arterial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'PCO2ARTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'PCO2ARTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'pH venoso, concentración de hidrogeniones en sangre venosa para equilibrio ácido-base', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'PHVENOSO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'PH Venoso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'PHVENOSO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'PHVENOSO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'pH arterial, concentración de hidrogeniones en sangre arterial, parámetro crítico ácido-base', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'PHARTERIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'PH Arterial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'PHARTERIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'PHARTERIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vibración, vibración de alta frecuencia en ventilación oscilatoria para facilitar drenaje secreto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'VIBRACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vibracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'VIBRACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'VIBRACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Paso de surfactante, instilación de agente tensoactivo pulmonar en vía aérea para mejorar ventilación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'PSSURFACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Paso Surfactante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'PSSURFACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'PSSURFACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comisura de TOT, marca de profundidad del tubo endotraqueal en comisura dental', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'COMIDETOT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Comisura de TOT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'COMIDETOT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'COMIDETOT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de TOT, calibre/diámetro del tubo endotraqueal utilizado en intubación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'NUMEDETOT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de TOT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'NUMEDETOT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'NUMEDETOT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FiO2 (Fracción Inspirada de Oxígeno), porcentaje de oxígeno suministrado en ventilación mecánica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALANFLO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FIO2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALANFLO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALANFLO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Temperatura en cascada, temperatura del gas calentado y humidificado en circuito ventilador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'TEMCASCAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Temperatura Cascada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'TEMCASCAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'TEMCASCAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Frecuencia espontánea o autónoma, ciclos respiratorios iniciados por paciente por minuto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'FRECESPON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Frecuencia Expontanea /Am', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'FRECESPON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'FRECESPON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Frecuencia programada, ciclos respiratorios mandatarios ajustados en ventilador en Hz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'FRECPROGR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Frecuencia Programada / HZ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'FRECPROGR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'FRECPROGR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'EMI (Effort de Muscular de Inspiración), esfuerzo muscular requerido para respiración espontánea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'ESFMAZINSP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'E.M.I', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'ESFMAZINSP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'ESFMAZINSP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación I:E, cociente tiempo inspiratorio respecto espiratorio en ventilación controlada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'RELACIONIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion IE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'RELACIONIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'RELACIONIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Elastancia estática, resistencia elástica pulmonar y torácica en ventilación mecánica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'ESTATICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estatica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'ESTATICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'ESTATICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'P meseta (Presión Meseta), presión alveolar máxima alcanzada durante inspiración controlada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BAPMESETA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'P Meseta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BAPMESETA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BAPMESETA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distensibilidad o compliance, capacidad de pulmón para expandirse ante cambio de presión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'DISTENSIBI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Distensibilidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'DISTENSIBI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'DISTENSIBI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sensibilidad, nivel de presión negativa requerida para disparar inspiración asistida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'SENSIBILID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sensibilidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'SENSIBILID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'SENSIBILID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flujo pico, velocidad máxima del flujo de gas entregado en ventilación mecánica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'FLUJOPICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Flujo Pico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'FLUJOPICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'FLUJOPICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Balance PIP o PIP, presión inspiratoria positiva máxima generada en vía aérea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALANCPIP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Balance PIP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALANCPIP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALANCPIP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Balance PEEP, presión espiratoria final positiva residual al final de espiración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALANPEEP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Balance PEEP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALANPEEP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALANPEEP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presión de soporte, asistencia de presión en modos espontáneos de ventilación mecánica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'PRESISOPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Presion Soporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'PRESISOPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'PRESISOPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen espontáneo, volumen tidal generado por esfuerzo inspiratorio propio del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'VOLMINESP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Volumen Expontaneo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'VOLMINESP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'VOLMINESP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen por minuto, ventilación minuto total (volumen tidal × frecuencia respiratoria)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'VOLMINUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Volumen Por Minuto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'VOLMINUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'VOLMINUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen corriente, volumen tidal programado o actual en cada ciclo respiratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'VOLCORRIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Volumen Corriente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'VOLCORRIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'VOLCORRIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modo de ventilación, patrón de soporte respiratorio (CMV, SIMV, PSV, CPAP, etc.)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'MODVENTIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modo de Ventilacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'MODVENTIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'MODVENTIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de profesional de salud, identificador PII del médico o profesional registrador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Profesional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Orden de registro, número secuencial que organiza múltiples registros de acido-base por ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'ORDEREGIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'HCO3 Venoso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'ORDEREGIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'ORDEREGIST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad funcional, identificador de área clínica (UCI, URG, etc.) donde se registra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención, identificador de la institución sanitaria que atiende', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de inicio, hora de inicio del monitoreo de parámetros acido-base, formato CHAR(5)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'FEHORAINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de Inicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'FEHORAINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'FEHORAINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del registro, fecha inicial del evento de acido-base registrado, DATETIME', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'FECHREGIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'FECHREGIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'FECHREGIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso, identificador del episodio de atención/internación del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente, identificador PII de paciente (cédula/documento/identificación ofuscado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo, clave primaria INT IDENTITY de registro acido-base único', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'IDACIDBASE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'IDACIDBASE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'IDACIDBASE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de parámetros de ventilación mecánica y gases arteriales/venosos de pacientes en unidad de cuidados intensivos o área crítica. Contiene los valores de configuración del ventilador, mecánica pulmonar, gasometría y balance de oxígeno registrados por turno o sesión de monitoreo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE';
