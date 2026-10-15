CREATE TABLE [dbo].[HCPPYPPAC] (
    [CODCONCEC] NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [CODPROPYP] CHAR (4)                                                                         NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [JUSNOREPP] VARCHAR (4000)                                                                   NULL,
    [FECREGSIS] DATETIME                                                                         NOT NULL,
    [IDRIAS]    INT                                                                              NULL,
    CONSTRAINT [PK_HCPPYPPAC] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_HCPPYPPAC_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCPPYPPAC_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCPPYPPAC_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCPPYPPAC_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCPPYPPAC_RIAS] FOREIGN KEY ([IDRIAS]) REFERENCES [dbo].[RIAS] ([ID])
);


GO
ALTER TABLE [dbo].[HCPPYPPAC] NOCHECK CONSTRAINT [FK_HCPPYPPAC_ADCENATEN];


GO
ALTER TABLE [dbo].[HCPPYPPAC] NOCHECK CONSTRAINT [FK_HCPPYPPAC_ADINGRESO];


GO
ALTER TABLE [dbo].[HCPPYPPAC] NOCHECK CONSTRAINT [FK_HCPPYPPAC_INPACIENT];


GO
ALTER TABLE [dbo].[HCPPYPPAC] NOCHECK CONSTRAINT [FK_HCPPYPPAC_INUNIFUNC];


GO
ALTER TABLE [dbo].[HCPPYPPAC] NOCHECK CONSTRAINT [FK_HCPPYPPAC_RIAS];


GO
CREATE NONCLUSTERED INDEX [_dta_index_HCPPYPPAC_6_1248111587__K2_K4_K9_K5_3_6]
    ON [dbo].[HCPPYPPAC]([IPCODPACI] ASC, [CODCENATE] ASC, [FECREGSIS] ASC, [UFUCODIGO] ASC)
    INCLUDE([CODPROPYP], [NUMINGRES]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con la tabla de RIAS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPPYPPAC', @level2type = N'COLUMN', @level2name = N'IDRIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha registro en el sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPPYPPAC', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Justificacion porque no se recomienda un Programa de PyP al Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPPYPPAC', @level2type = N'COLUMN', @level2name = N'JUSNOREPP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPPYPPAC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Programa de promocion y prevencion  0: DETECCION TEMPRANA DE ALTERACIONES DE CRECIMIENTO Y DESARROLLO EN EL MENOR DE 10 AÑOS  1:DETECCION TEMPRANA DE ALTERACIONES DEL DESARROLLO DEL JOVEN 10 A 29 AÑOS  2:DETECCION TEMPRANA DE ALTERACIONES DEL EMBARAZO  3:DETECCIÓN TEMPRANA DE LAS ALTERACIONES DEL ADULTO  4:DETECCIÓN TEMPRANA DEL CANCER DE CUELLO UTERINO  5:DETECCIÓN TEMPRANA DEL CANCER DE SENO  6:DETECCIÓN TEMPRANA DE LAS ALTERACIONES DE LA AGUDEZA VISUAL  9:NINGUNO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPPYPPAC', @level2type = N'COLUMN', @level2name = N'CODPROPYP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPPYPPAC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPPYPPAC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPPYPPAC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPPYPPAC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo interno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPPYPPAC', @level2type = N'COLUMN', @level2name = N'CODCONCEC';

