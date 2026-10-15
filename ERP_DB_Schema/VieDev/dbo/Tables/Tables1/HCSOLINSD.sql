CREATE TABLE [dbo].[HCSOLINSD] (
    [CODCONCEC]        CHAR (10)                                                                        NOT NULL,
    [IPCODPACI]        VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]        CHAR (10)                                                                        NOT NULL,
    [CODCENATE]        CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]        CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]        CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [CODPRODUC]        CHAR (20)                                                                        NOT NULL,
    [CANPEDPRO]        INT                                                                              NOT NULL,
    [PREFECANT]        BIT                                                                              NOT NULL,
    [MOTPREANT]        CHAR (250)                                                                       NULL,
    [PREESTADO]        INT                                                                              NULL,
    [INDAUDFOR]        NUMERIC (18)                                                                     NOT NULL,
    [JUSTIINSU]        VARCHAR (MAX)                                                                    NULL,
    [MANEJOEXTRA]      BIT                                                                              NULL,
    [IDHCSOLINSC]      INT                                                                              NULL,
    [ID]               INT                                                                              IDENTITY (1, 1) NOT NULL,
    [TipoPrescripcion] INT                                                                              NULL,
    [IdNursingPackage] INT                                                                              NULL,
    CONSTRAINT [PK_HCSOLINSD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCSOLINSD_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCSOLINSD_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCSOLINSD_AGPAQUETES] FOREIGN KEY ([IdNursingPackage]) REFERENCES [dbo].[AGPAQUETES] ([ID]),
    CONSTRAINT [FK_HCSOLINSD_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCSOLINSD_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCSOLINSD_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCSOLINSD_IPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_IDHCSOLINSC_ID] FOREIGN KEY ([IDHCSOLINSC]) REFERENCES [dbo].[HCSOLINSC] ([ID])
);


GO
ALTER TABLE [dbo].[HCSOLINSD] NOCHECK CONSTRAINT [FK_HCSOLINSD_ADcenaten];


GO
ALTER TABLE [dbo].[HCSOLINSD] NOCHECK CONSTRAINT [FK_HCSOLINSD_ADINGRESO];


GO
ALTER TABLE [dbo].[HCSOLINSD] NOCHECK CONSTRAINT [FK_HCSOLINSD_AGPAQUETES];


GO
ALTER TABLE [dbo].[HCSOLINSD] NOCHECK CONSTRAINT [FK_HCSOLINSD_INPacient];


GO
ALTER TABLE [dbo].[HCSOLINSD] NOCHECK CONSTRAINT [FK_HCSOLINSD_INPROFSAL];


GO
ALTER TABLE [dbo].[HCSOLINSD] NOCHECK CONSTRAINT [FK_HCSOLINSD_INUNIFUNC];


GO
ALTER TABLE [dbo].[HCSOLINSD] NOCHECK CONSTRAINT [FK_HCSOLINSD_IPROFSAL];


GO
ALTER TABLE [dbo].[HCSOLINSD] NOCHECK CONSTRAINT [FK_IDHCSOLINSC_ID];


GO
CREATE NONCLUSTERED INDEX [IX_HCSOLINSD_IPCODPACI_NUMINGRES]
    ON [dbo].[HCSOLINSD]([IPCODPACI] ASC, [NUMINGRES] ASC)
    INCLUDE([CANPEDPRO], [CODCONCEC], [CODPRODUC]);


GO
CREATE NONCLUSTERED INDEX [IX_HCSOLINSD_CODCONCEC_MANEJOEXTRA]
    ON [dbo].[HCSOLINSD]([CODCONCEC] ASC, [MANEJOEXTRA] ASC)
    INCLUDE([CANPEDPRO], [CODPRODUC], [JUSTIINSU]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda el Id del paquete de enfermería al que pertenece el producto si este fue guardado como paquete.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'IdNursingPackage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el tipo de Prescripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'TipoPrescripcion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manejo extramural:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'MANEJOEXTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Medicamento  1: Realiza Interfase con Inventarios  2: No Realiza Interfase con Inventarios x existencia del producto en el Kardex del Paciente correspondiente a la Centro de Atencion y Unidad Funcional Actual.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'PREESTADO';

