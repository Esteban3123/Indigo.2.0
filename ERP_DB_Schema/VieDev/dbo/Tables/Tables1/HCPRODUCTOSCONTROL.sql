CREATE TABLE [dbo].[HCPRODUCTOSCONTROL] (
    [ID]               INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [INCONSECUCONTROL] INT                                                                              NOT NULL,
    [FECHAREGISTRO]    DATETIME                                                                         NOT NULL,
    [CODCENATE]        CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]        CHAR (10)                                                                        NOT NULL,
    [IPCODPACI]        VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]        CHAR (10)                                                                        NOT NULL,
    [NUMEFOLIO]        NCHAR (10)                                                                       NULL,
    [CODPRODUC]        CHAR (20)                                                                        NOT NULL,
    [CODPROSAL]        CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [TIPOSOLICITUD]    INT                                                                              NOT NULL,
    [USUARIOENFERMRIA] CHAR (20)                                                                        NULL,
    [DESADMINI]        VARCHAR (MAX)                                                                    NULL,
    [CANTIDAD]         INT                                                                              NULL,
    [DURACIDOS]        CHAR (20)                                                                        NULL,
    [CODCONCECENF]     CHAR (10)                                                                        NULL,
    [CODCONCECMED]     NUMERIC (18)                                                                     NULL,
    [CODCONCECDEVO]    INT                                                                              NULL,
    [MANEXTPRO]        BIT                                                                              CONSTRAINT [DF_MANEXTPRO] DEFAULT ((0)) NOT NULL,
    [ORIGENPRODUCTO]   TINYINT                                                                          NULL,
    CONSTRAINT [PK_HCPRODUCTOSCONTROL] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPRODUCTOSCONTROL_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCPRODUCTOSCONTROL_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCPRODUCTOSCONTROL_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC]),
    CONSTRAINT [FK_HCPRODUCTOSCONTROL_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCPRODUCTOSCONTROL_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCPRODUCTOSCONTROL] NOCHECK CONSTRAINT [FK_HCPRODUCTOSCONTROL_ADCENATEN];


GO
ALTER TABLE [dbo].[HCPRODUCTOSCONTROL] NOCHECK CONSTRAINT [FK_HCPRODUCTOSCONTROL_ADINGRESO];


GO
ALTER TABLE [dbo].[HCPRODUCTOSCONTROL] NOCHECK CONSTRAINT [FK_HCPRODUCTOSCONTROL_IHLISTPRO];


GO
ALTER TABLE [dbo].[HCPRODUCTOSCONTROL] NOCHECK CONSTRAINT [FK_HCPRODUCTOSCONTROL_INPACIENT];


GO
ALTER TABLE [dbo].[HCPRODUCTOSCONTROL] NOCHECK CONSTRAINT [FK_HCPRODUCTOSCONTROL_INUNIFUNC];


GO
CREATE NONCLUSTERED INDEX [IX_HCPRODUCTOSCONTROL_NUMEFOLIO_NUMINGRES_CODPRODUC]
    ON [dbo].[HCPRODUCTOSCONTROL]([NUMEFOLIO] ASC, [NUMINGRES] ASC, [CODPRODUC] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Columna para determinar el origen de un producto.  1. Preescripción de Medicamentos.  2. Preescripción de Mezclas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'ORIGENPRODUCTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Este Producto Corresponde a un plan de Manejo Externo?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Duracion  Tratamiento continuo  fijo  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'DURACIDOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad devuleve  cantidad ordena  cantidad ordena enfermera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'CANTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Orden medica  2 - Enfermeria   3 - Devolutivos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'TIPOSOLICITUD';

