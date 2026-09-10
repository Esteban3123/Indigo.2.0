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
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCEXFISANE].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCEXFISANE].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCEXFISANE].[ANESCOSTEO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCEXFISANE].[ANESCMUSCU]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCEXFISANE].[ANESCURINA]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCEXFISANE].[ANESCLINFATICO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCEXFISANE].[ANESCCARDIO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCEXFISANE].[ANESCOTORRI]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCEXFISANE].[ANESCRESPIRA]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCEXFISANE].[ANESCDIGEST]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCEXFISANE].[ANESCPSIQ]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCEXFISANE].[ANESCDERMA]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCEXFISANE].[ANESCENDOCRI]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCEXFISANE].[ANESCNEUROLO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCEXFISANE].[ANESCGINECO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCEXFISANE].[ANESCINMUNO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCEXFISANE].[ANESCNERVI]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCEXFISANE].[ANESCOJO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCEXFISANE].[ANESCCABEZA]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCEXFISANE].[ANESCDENTAL]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCEXFISANE].[ANESCCUELLO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCEXFISANE].[ANESCABDOM]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCEXFISANE].[ANESCEXTRE]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCEXFISANE].[ANESCTORAX]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Análisis clínico del tórax, hallazgos de examen físico de pared torácica, pulmones y estructuras intratórax (VARCHAR 2000, Analysis_Ofuscado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCTORAX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Analisis Torax', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCTORAX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCTORAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: ¿incluye valoración del tórax en examen físico? (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESTORAX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si tiene valoracion Torax', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESTORAX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESTORAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Análisis clínico de extremidades, hallazgos de examen físico de miembros superiores e inferiores (VARCHAR 2000, Analysis_Ofuscado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCEXTRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Analisis Extremidades', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCEXTRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCEXTRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: ¿incluye valoración de extremidades en examen físico? (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESEXTRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si tiene valoracion Extremidades', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESEXTRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESEXTRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Análisis clínico del abdomen, hallazgos de examen físico abdominal, órganos y estructuras (VARCHAR 2000, Analysis_Ofuscado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCABDOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Analisis Abdomen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCABDOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCABDOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: ¿incluye valoración abdominal en examen físico? (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESABDOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si tiene valoracion Abdomen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESABDOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESABDOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Análisis clínico del cuello, hallazgos de tiroides, ganglios, estructuras cervicales (VARCHAR 2000, Analysis_Ofuscado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCCUELLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Analisis Cuello', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCCUELLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCCUELLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: ¿incluye valoración del cuello en examen físico? (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCUELLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si tiene valoracion Cuello', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCUELLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCUELLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Análisis del estado dental, hallazgos de piezas dentarias, oclusión, caries, prótesis (VARCHAR 2000, Analysis_Ofuscado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCDENTAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Analisis Estado Dental', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCDENTAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCDENTAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: ¿incluye valoración del estado dental en examen físico? (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESDENTAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si tiene valoracion Estado Dental', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESDENTAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESDENTAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Análisis clínico de cabeza, hallazgos de cuero cabelludo, cara, senos paranasales (VARCHAR 2000, Analysis_Ofuscado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCCABEZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Analisis cabeza', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCCABEZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCCABEZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: ¿incluye valoración de cabeza en examen físico? (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCABEZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si tiene valoracion Cabeza', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCABEZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCABEZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Análisis oftalmológico, hallazgos visuales, agudeza, motilidad, fondo de ojo (VARCHAR 2000, Analysis_Ofuscado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCOJO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Analisis Ojos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCOJO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCOJO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: ¿incluye valoración oftalmológica/visual en examen? (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESOJO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si tiene valoracion Ojos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESOJO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESOJO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Análisis del sistema nervioso periférico, hallazgos de pares craneales, reflejos, sensibilidad (VARCHAR 2000, Analysis_Ofuscado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCNERVI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Analisis Nervioso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCNERVI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCNERVI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: ¿incluye valoración del sistema nervioso en examen físico? (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESNERVI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si tiene valoracion Nervioso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESNERVI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESNERVI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Análisis inmunológico, hallazgos de sistema inmune, linfocitos, respuesta inmunológica (VARCHAR 2000, Analysis_Ofuscado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCINMUNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Analisis Inmunologico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCINMUNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCINMUNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: ¿incluye valoración inmunológica en examen? (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESINMUNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si tiene valoracion Inmunologico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESINMUNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESINMUNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Análisis ginecobstétrico, hallazgos de órganos reproductivos, mamas, examen pelviano (VARCHAR 2000, Analysis_Ofuscado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCGINECO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Analisis GinecoObstetrico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCGINECO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCGINECO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: ¿incluye valoración ginecobstétrica en examen físico? (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESGINECO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si tiene valoracion GinecoObstetrico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESGINECO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESGINECO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Análisis neurológico, hallazgos de función cognitiva, motora, sensitiva, coordinación (VARCHAR 2000, Analysis_Ofuscado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCNEUROLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Analisis Neurologico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCNEUROLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCNEUROLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: ¿incluye valoración neurológica en examen físico? (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESNEUROLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si tiene valoracion Neurologico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESNEUROLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESNEUROLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Análisis endocrinológico, hallazgos de glándulas endocrinas, tiroides, metabolismo (VARCHAR 2000, Analysis_Ofuscado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCENDOCRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Analisis Endocrino', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCENDOCRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCENDOCRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: ¿incluye valoración endocrinológica en examen? (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESENDOCRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si tiene valoracion Endocrino', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESENDOCRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESENDOCRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Análisis dermatológico, hallazgos de piel, lesiones, pigmentación, anexos cutáneos (VARCHAR 2000, Analysis_Ofuscado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCDERMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Analisis Dermatológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCDERMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCDERMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: ¿incluye valoración dermatológica en examen físico? (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESDERMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si tiene valoracion Dermatológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESDERMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESDERMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Análisis psiquiátrico/psicológico, hallazgos de estado mental, afecto, cognición, comportamiento (VARCHAR 2000, Analysis_Ofuscado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCPSIQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Analisis Psiquiatrico/Psicologico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCPSIQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCPSIQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: ¿incluye valoración psiquiátrica/psicológica en examen? (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESPSIQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si tiene valoracion Psiquiatrico/Psicologico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESPSIQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESPSIQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Análisis del sistema digestivo, hallazgos de boca, esófago, estómago, intestinos, hígado (VARCHAR 2000, Analysis_Ofuscado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCDIGEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Analisis Digestivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCDIGEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCDIGEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: ¿incluye valoración del sistema digestivo en examen? (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESDIGEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si tiene valoracion Digestivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESDIGEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESDIGEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Análisis del sistema respiratorio, hallazgos de vías aéreas, pulmones, ventilación (VARCHAR 2000, Analysis_Ofuscado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCRESPIRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Analisis Respiratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCRESPIRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCRESPIRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: ¿incluye valoración respiratoria en examen físico? (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESRESPIRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si tiene valoracion Respiratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESRESPIRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESRESPIRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Análisis otorrinolaringológico, hallazgos de oído, nariz, garganta, laringe (VARCHAR 2000, Analysis_Ofuscado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCOTORRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Analisis Otorrinolaringologico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCOTORRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCOTORRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: ¿incluye valoración otorrinolaringológica en examen? (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESOTORRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si tiene valoracion Otorrinolaringologico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESOTORRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESOTORRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Análisis cardiovascular, hallazgos de corazón, pulsos, presión arterial, circulación (VARCHAR 2000, Analysis_Ofuscado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCCARDIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Analisis Cardiovascular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCCARDIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCCARDIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: ¿incluye valoración cardiovascular en examen físico? (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCARDIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si tiene valoracion Cardiovascular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCARDIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCARDIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Análisis del sistema linfático, hallazgos de ganglios, drenaje, edema (VARCHAR 2000, Analysis_Ofuscado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCLINFATICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Analisis Linfatico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCLINFATICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCLINFATICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: ¿incluye valoración del sistema linfático en examen? (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESLINFATICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si tiene valoracion Linfatico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESLINFATICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESLINFATICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Análisis del sistema urinario, hallazgos de riñones, vejiga, vías urinarias (VARCHAR 2000, Analysis_Ofuscado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCURINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Analisis Muscular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCURINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCURINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: ¿incluye valoración del sistema urinario en examen físico? (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESURINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si tiene valoracion Urinario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESURINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESURINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Análisis musculoesquelético, hallazgos de músculos, tono, fuerza, movilidad articular (VARCHAR 2000, Analysis_Ofuscado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCMUSCU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Analisis Muscular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCMUSCU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCMUSCU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: ¿incluye valoración musculoesquelética en examen físico? (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESMUSCU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si tiene valoracion Muscular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESMUSCU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESMUSCU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Análisis osteoarticular, hallazgos de huesos, articulaciones, cartílagos, alineación (VARCHAR 2000, Analysis_Ofuscado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCOSTEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Analisis Osteoarticular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCOSTEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESCOSTEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: ¿incluye valoración osteoarticular en examen físico? (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESOSTEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si tiene valoracion Osteoarticular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESOSTEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ANESOSTEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del registro de este examen físico en el sistema (DATETIME, DEFAULT getdate())', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud que realiza/firma el examen (FK→INPROFSAL, VARCHAR 20, Identification_Ofuscado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad funcional (servicio/especialidad) donde se realiza el examen (FK→INUNIFUNC, CHAR 10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención/institución donde se realiza el examen (FK→ADCENATEN, CHAR 10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso/admission vinculado al examen físico (FK→ADINGRESO, CHAR 10, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente, equivalente a cédula/documento identificación (FK→INPACIENT, VARCHAR 25, Identification_Ofuscado, PII)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número único de folio/consecutivo del registro de examen (CHAR 10, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre/tipo interno de historia clínica, clasificación de documento (CHAR 9)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico de este registro de examen físico (INT IDENTITY)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Examen físico por sistemas y regiones anatómicas registrado durante la valoración preanestésica del paciente. Cada fila corresponde a una evaluación anestésica vinculada a un ingreso, donde se indica si hay hallazgos (bit) y se describe el detalle clínico para cada sistema u órgano revisado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISANE';
