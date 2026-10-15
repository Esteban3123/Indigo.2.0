CREATE TABLE [dbo].[INTERCTRL] (
    [AUTO]          INT                                                                               IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ORDEN_INDIGO]  VARCHAR (20)                                                                      NOT NULL,
    [NUMUESTRA]     TINYINT                                                                           NOT NULL,
    [ESTADOINT]     BIT                                                                               NOT NULL,
    [INTERPRET]     VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "Interpretation_Ofuscado", 0)') NULL,
    [AUTOLABOR]     INT                                                                               NOT NULL,
    [FECGENERA]     VARCHAR (20)                                                                      NULL,
    [FECREGIST]     DATETIME                                                                          NULL,
    [FECSERIPS]     VARCHAR (20)                                                                      NULL,
    [CODPROSAL]     CHAR (70) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')     NULL,
    [MICROBIOLOGIA] BIT                                                                               NULL,
    [TIPORESULTADO] BIT                                                                               NULL,
    CONSTRAINT [PK_INTERCTRL] PRIMARY KEY CLUSTERED ([AUTO] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_PARACLINICOS]
    ON [dbo].[INTERCTRL]([AUTOLABOR] ASC)
    INCLUDE([INTERPRET]);


GO
CREATE NONCLUSTERED INDEX [_dta_index_INTERCTRL_7_825822054__K6_K1_K2]
    ON [dbo].[INTERCTRL]([AUTOLABOR] ASC, [AUTO] ASC, [ORDEN_INDIGO] ASC);


GO
CREATE NONCLUSTERED INDEX [_dta_index_INTERCTRL_6_825822054__K4_K6_K8_3_10]
    ON [dbo].[INTERCTRL]([ESTADOINT] ASC, [AUTOLABOR] ASC, [FECREGIST] ASC)
    INCLUDE([CODPROSAL], [NUMUESTRA]);


GO
CREATE NONCLUSTERED INDEX [IX_INTERCTRL_ORDEN_INDIGO_AUTOLABOR_FECREGIST]
    ON [dbo].[INTERCTRL]([ORDEN_INDIGO] ASC, [AUTOLABOR] ASC)
    INCLUDE([FECREGIST]);


GO
CREATE NONCLUSTERED INDEX [_dta_index_INTERCTRL_7_825822054__K2_6]
    ON [dbo].[INTERCTRL]([ORDEN_INDIGO] ASC)
    INCLUDE([AUTOLABOR]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor  "0": Indica que el resultado enviado en el XML es preliminar  Valor "1": Indica que el resultado enviado en el XML es final', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'TIPORESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '''Valor "0": Indica que el estudio no es de microbiología, es decir que no maneja resultados preliminares     ''Valor "1": Indica que el estudio es de microbiología, es decir que maneja resultados preliminares', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'MICROBIOLOGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del Profesional del Resultado de Laboratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'FECSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Registro del Resultado de la interfaz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'FECREGIST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Generacion del Resultado de la Interfaz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'FECGENERA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla de laboratorios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'AUTOLABOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Interpretacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'INTERPRET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta Interpretado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'ESTADOINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de la Muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'NUMUESTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Orden Indigo  Paciente concatenado con el numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'ORDEN_INDIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico interno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'AUTO';

