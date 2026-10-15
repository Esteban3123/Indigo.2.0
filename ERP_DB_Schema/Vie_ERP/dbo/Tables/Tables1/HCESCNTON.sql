CREATE TABLE [dbo].[HCESCNTON] (
    [CODCONCEC]  NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]  VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]  CHAR (10)                                                                        NOT NULL,
    [CODCENATE]  CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]  CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]  CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECREGSIS]  DATETIME                                                                         NOT NULL,
    [ESTFISGEN]  TINYINT                                                                          NOT NULL,
    [ESTMENTAL]  TINYINT                                                                          NOT NULL,
    [MOVILIDAD]  TINYINT                                                                          NOT NULL,
    [ACTIVIDAD]  TINYINT                                                                          NOT NULL,
    [INCTINENCI] TINYINT                                                                          NOT NULL,
    [HCESCALAID] INT                                                                              NULL,
    [CODESPECI]  CHAR (3)                                                                         NULL,
    CONSTRAINT [PK_HCESCNTON] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_HCESCNTON_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCESCNTON_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCESCNTON_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCESCNTON] NOCHECK CONSTRAINT [FK_HCESCNTON_ADINGRESO];


GO
ALTER TABLE [dbo].[HCESCNTON] NOCHECK CONSTRAINT [FK_HCESCNTON_INESPECIA];


GO
ALTER TABLE [dbo].[HCESCNTON] NOCHECK CONSTRAINT [FK_HCESCNTON_INPACIENT];


GO
CREATE NONCLUSTERED INDEX [_dta_index_HCESCNTON_7_1642593040__K2_K3_K5_8_9_10_11_12]
    ON [dbo].[HCESCNTON]([IPCODPACI] ASC, [NUMINGRES] ASC, [UFUCODIGO] ASC)
    INCLUDE([ACTIVIDAD], [ESTFISGEN], [ESTMENTAL], [INCTINENCI], [MOVILIDAD]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda la especialidad del medico o enfermera que Diligencia la escala. ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con la tabla cabecera llamada HCESCALAS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'HCESCALAID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1: Urinaria y Fecal 2: Urinaria o Fecal 3: Ocasional 4: Ninguna.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'INCTINENCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1: Encamado 2: Sentado 3: Camina con ayuda 4: Ambulante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'ACTIVIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1: Inmovil 2: Muy Limitada 3: Disminuida 4: Total', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'MOVILIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1: Estuporoso y Comatoso 2: Confuso 3: Apatico 4: Alerta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'ESTMENTAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1: Muy Malo 2: Malo 3: Regular 4: Bueno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'ESTFISGEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha de registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'código del profesional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'código de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'código del centro de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'numero de ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'código del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'CODCONCEC';

