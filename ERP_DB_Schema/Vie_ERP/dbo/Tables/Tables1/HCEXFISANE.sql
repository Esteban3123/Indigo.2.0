CREATE TABLE [dbo].[HCEXFISANE] (
    [ID]             INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDETIPHIS]      CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]      CHAR (10)                                                                        NULL,
    [IPCODPACI]      VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]      CHAR (10)                                                                        NULL,
    [CODCENATE]      CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]      CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]      CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECREGSIS]      DATETIME                                                                         CONSTRAINT [DF_HCEXFISANE_FECREGSIS] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ANESOSTEO]      BIT                                                                              NULL,
    [ANESCOSTEO]     VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')     NULL,
    [ANESMUSCU]      BIT                                                                              NULL,
    [ANESCMUSCU]     VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')     NOT NULL,
    [ANESURINA]      BIT                                                                              NULL,
    [ANESCURINA]     VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')     NULL,
    [ANESLINFATICO]  BIT                                                                              NULL,
    [ANESCLINFATICO] VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')     NULL,
    [ANESCARDIO]     BIT                                                                              NULL,
    [ANESCCARDIO]    VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')     NULL,
    [ANESOTORRI]     BIT                                                                              NULL,
    [ANESCOTORRI]    VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')     NULL,
    [ANESRESPIRA]    BIT                                                                              NULL,
    [ANESCRESPIRA]   VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')     NULL,
    [ANESDIGEST]     BIT                                                                              NULL,
    [ANESCDIGEST]    VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')     NULL,
    [ANESPSIQ]       BIT                                                                              NULL,
    [ANESCPSIQ]      VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')     NULL,
    [ANESDERMA]      BIT                                                                              NULL,
    [ANESCDERMA]     VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')     NULL,
    [ANESENDOCRI]    BIT                                                                              NULL,
    [ANESCENDOCRI]   VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')     NULL,
    [ANESNEUROLO]    BIT                                                                              NULL,
    [ANESCNEUROLO]   VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')     NULL,
    [ANESGINECO]     BIT                                                                              NULL,
    [ANESCGINECO]    VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')     NULL,
    [ANESINMUNO]     BIT                                                                              NULL,
    [ANESCINMUNO]    VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')     NULL,
    [ANESNERVI]      BIT                                                                              NULL,
    [ANESCNERVI]     VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')     NULL,
    [ANESOJO]        BIT                                                                              NULL,
    [ANESCOJO]       VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')     NULL,
    [ANESCABEZA]     BIT                                                                              NULL,
    [ANESCCABEZA]    VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')     NULL,
    [ANESDENTAL]     BIT                                                                              NULL,
    [ANESCDENTAL]    VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')     NULL,
    [ANESCUELLO]     BIT                                                                              NULL,
    [ANESCCUELLO]    VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')     NULL,
    [ANESABDOM]      BIT                                                                              NULL,
    [ANESCABDOM]     VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')     NULL,
    [ANESEXTRE]      BIT                                                                              NULL,
    [ANESCEXTRE]     VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')     NULL,
    [ANESTORAX]      BIT                                                                              NULL,
    [ANESCTORAX]     VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')     NULL,
    CONSTRAINT [PK_HCEXFISANE] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_FK_HCEXFISANE_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCEXFISANE_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCEXFISANE_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCEXFISANE_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCEXFISANE_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCEXFISANE] NOCHECK CONSTRAINT [FK_FK_HCEXFISANE_ADINGRESO];


GO
ALTER TABLE [dbo].[HCEXFISANE] NOCHECK CONSTRAINT [FK_HCEXFISANE_ADcenaten];


GO
ALTER TABLE [dbo].[HCEXFISANE] NOCHECK CONSTRAINT [FK_HCEXFISANE_INPacient];


GO
ALTER TABLE [dbo].[HCEXFISANE] NOCHECK CONSTRAINT [FK_HCEXFISANE_INPROFSAL];


GO
ALTER TABLE [dbo].[HCEXFISANE] NOCHECK CONSTRAINT [FK_HCEXFISANE_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Analisis Torax', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCTORAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si tiene valoracion Torax', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESTORAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Analisis Extremidades', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCEXTRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si tiene valoracion Extremidades', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESEXTRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Analisis Abdomen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCABDOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si tiene valoracion Abdomen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESABDOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Analisis Cuello', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCCUELLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si tiene valoracion Cuello', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCUELLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Analisis Estado Dental', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCDENTAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si tiene valoracion Estado Dental', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESDENTAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Analisis cabeza', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCCABEZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si tiene valoracion Cabeza', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCABEZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Analisis Ojos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCOJO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si tiene valoracion Ojos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESOJO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Analisis Nervioso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCNERVI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si tiene valoracion Nervioso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESNERVI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Analisis Inmunologico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCINMUNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si tiene valoracion Inmunologico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESINMUNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Analisis GinecoObstetrico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCGINECO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si tiene valoracion GinecoObstetrico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESGINECO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Analisis Neurologico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCNEUROLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si tiene valoracion Neurologico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESNEUROLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Analisis Endocrino', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCENDOCRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si tiene valoracion Endocrino', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESENDOCRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Analisis Dermatológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCDERMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si tiene valoracion Dermatológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESDERMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Analisis Psiquiatrico/Psicologico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCPSIQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si tiene valoracion Psiquiatrico/Psicologico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESPSIQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Analisis Digestivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCDIGEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si tiene valoracion Digestivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESDIGEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Analisis Respiratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCRESPIRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si tiene valoracion Respiratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESRESPIRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Analisis Otorrinolaringologico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCOTORRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si tiene valoracion Otorrinolaringologico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESOTORRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Analisis Cardiovascular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCCARDIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si tiene valoracion Cardiovascular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCARDIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Analisis Linfatico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCLINFATICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si tiene valoracion Linfatico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESLINFATICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Analisis Muscular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCURINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si tiene valoracion Urinario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESURINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Analisis Muscular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCMUSCU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si tiene valoracion Muscular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESMUSCU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Analisis Osteoarticular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCOSTEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si tiene valoracion Osteoarticular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESOSTEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ID';

