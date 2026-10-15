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
ALTER TABLE [dbo].[HCACIBASE] NOCHECK CONSTRAINT [FK_HCACIBASE_ADCENATEN];


GO
ALTER TABLE [dbo].[HCACIBASE] NOCHECK CONSTRAINT [FK_HCACIBASE_ADINGRESO];


GO
ALTER TABLE [dbo].[HCACIBASE] NOCHECK CONSTRAINT [FK_HCACIBASE_INPACIENT];


GO
ALTER TABLE [dbo].[HCACIBASE] NOCHECK CONSTRAINT [FK_HCACIBASE_INPROFSAL];


GO
ALTER TABLE [dbo].[HCACIBASE] NOCHECK CONSTRAINT [FK_HCACIBASE_INUNIFUNC];


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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(No se encontró documentación de este campo en la solución de crystal ni información por parte de los desarrolladores.)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'TIEMESPIR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(No se encontró documentación de este campo en la solución de crystal ni información por parte de los desarrolladores.)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'TIEMPINSPI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' DELTA DE HIDRIOGENONES', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'DELTAHIDR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Delta de CO2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'DELTACO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indice Oxigenacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'INDICOXIG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Gradiante de Extraccion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'GRADIEXTR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CPIS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALANCPIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'BE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALANCBE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Presion del Balon', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'PESIBALON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALANCPL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PH', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALANCPH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resistencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'RESITENCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'OBSERVACIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'FECHAREGIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Extubacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'EXTUBACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Control RX ToraX', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'CTRXTORAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Toma del Cultivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'TOMCULTIV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Saturacion de O2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'SATURACO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'QS/QT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALAQSQT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'D(A-V)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALADAV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CVO2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALACVO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CAO2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALACAO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CCO2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALACCO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PAO2/FLO2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'PAO2FLO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'SATO2 Venoso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'SATO2VENO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'SATO2 Arterial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'SATO2ARTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'HCO3 Venoso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'HCO3VENOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'HCO3 Arterial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'HCO3ARTER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PO2 Venoso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'PO2VENOSO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PO2 Arterial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'PO2ARTERI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PCO2 Venoso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'PCO2VENOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PCO2 Arterial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'PCO2ARTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PH Venoso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'PHVENOSO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PH Arterial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'PHARTERIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vibracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'VIBRACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Paso Surfactante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'PSSURFACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Comisura de TOT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'COMIDETOT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de TOT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'NUMEDETOT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FIO2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALANFLO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Temperatura Cascada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'TEMCASCAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Frecuencia Expontanea /Am', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'FRECESPON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Frecuencia Programada / HZ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'FRECPROGR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'E.M.I', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'ESFMAZINSP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion IE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'RELACIONIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estatica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'ESTATICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'P Meseta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BAPMESETA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Distensibilidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'DISTENSIBI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sensibilidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'SENSIBILID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Flujo Pico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'FLUJOPICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Balance PIP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALANCPIP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Balance PEEP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'BALANPEEP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Presion Soporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'PRESISOPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Volumen Expontaneo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'VOLMINESP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Volumen Por Minuto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'VOLMINUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Volumen Corriente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'VOLCORRIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Modo de Ventilacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'MODVENTIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Profesional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'HCO3 Venoso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'ORDEREGIST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora de Inicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'FEHORAINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'FECHREGIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'IDACIDBASE';

