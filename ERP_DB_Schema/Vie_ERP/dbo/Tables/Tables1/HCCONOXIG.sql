CREATE TABLE [dbo].[HCCONOXIG] (
    [IDCONOXIG]       INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]       VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [FECHREGIS]       DATETIME                                                                         NOT NULL,
    [NUMINGRES]       CHAR (10)                                                                        NOT NULL,
    [HORAINICIA]      DATETIME                                                                         NOT NULL,
    [UFUCODIGO]       CHAR (10)                                                                        NOT NULL,
    [CODCENATE]       CHAR (10)                                                                        NOT NULL,
    [CODVIAADM]       CHAR (3)                                                                         NOT NULL,
    [LITRXMINUT]      DECIMAL (18, 1)                                                                  NOT NULL,
    [HORAFINAL]       DATETIME                                                                         NOT NULL,
    [TOTHORAS]        DECIMAL (18, 1)                                                                  NOT NULL,
    [TOTLITADM]       DECIMAL (18, 1)                                                                  NOT NULL,
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
ALTER TABLE [dbo].[HCCONOXIG] NOCHECK CONSTRAINT [FK_HCCONOXIG_ADCENATEN];


GO
ALTER TABLE [dbo].[HCCONOXIG] NOCHECK CONSTRAINT [FK_HCCONOXIG_ADINGRESO];


GO
ALTER TABLE [dbo].[HCCONOXIG] NOCHECK CONSTRAINT [FK_HCCONOXIG_INPACIENT];


GO
ALTER TABLE [dbo].[HCCONOXIG] NOCHECK CONSTRAINT [FK_HCCONOXIG_INPROFSAL];


GO
ALTER TABLE [dbo].[HCCONOXIG] NOCHECK CONSTRAINT [FK_HCCONOXIG_INUNIFUNC];


GO
CREATE NONCLUSTERED INDEX [IX_HCCONOXIG_GENSERVICEORDER]
    ON [dbo].[HCCONOXIG]([GENSERVICEORDER] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_HCCONOXIG_NUMINGRES]
    ON [dbo].[HCCONOXIG]([NUMINGRES] ASC)
    INCLUDE([CODCENATE], [CODPROSAL], [CODVIAADM], [FECHREGIS], [GENSERVICEORDER], [HORAFINAL], [HORAINICIA], [IDCONOXIG], [IPCODPACI], [LITRXMINUT], [TOTHORAS], [TOTLITADM], [UFUCODIGO]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo de Lote que se llena diligenciando la Hoja de óxido nítrico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'BATCHCODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Presion final hoja de gases al diligenciar óxido nítrico.   Unidad de Medida libras por pulgada cuadrada (PSI) ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'FINALPRESSURE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Presion inicial hoja de gases al diligenciar óxido nítrico.  Unidad de medidad libras por pulgada cuadrada(PSI)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'INITIALPRESSURE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'volumen final hoja de gases al diligenciar óxido nítrico.   Unidad de medida Litros(L)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'FINALVOLUME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'volumen inicial hoja de gases al diligenciar óxido nítrico.   Unidad de medida Litros(L)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'INITIALVOLUME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que se registra desde Hoja de gases     1 - Consumo de Oxigeno.  2 - Consumo de Oxido nitrico.  3 - Consumo de Oxihelio.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'GASTYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id de la orden de servicio en la cual quedo incluido el servicio de Oxigeno, Este campo se llena cuando se genera la orden de servicio desde el formulario de Control de Cuenta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'OBSERVACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del registro del dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'FECHAREGI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'profesional responsable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'total litros admnistrados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'TOTLITADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'numero total de horas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'TOTHORAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'hora final del consumo de oxigeno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'HORAFINAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'litros por minuto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'LITRXMINUT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1-Canula Nasal  2- Ventury  3-Ventilacion Mecanica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'CODVIAADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo del centro de atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora inciaul del consumo de oxigeno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'HORAINICIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de ingreso del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del registro del consumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'FECHREGIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificacion del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONOXIG', @level2type = N'COLUMN', @level2name = N'IDCONOXIG';

