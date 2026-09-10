CREATE TABLE [dbo].[HCFICHA346] (
    [ID]                   INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION]  INT             NOT NULL,
    [TRABADESALUD]         INT             NULL,
    [VIAJOAREASVIRUS]      INT             NULL,
    [VIAJETERNAL]          BIT             NULL,
    [LUGARVIAJENAL]        VARCHAR (50)    NULL,
    [VIAJETERINTER]        BIT             NULL,
    [LUGARVIAJEINTER]      VARCHAR (50)    NULL,
    [CONTACTO14DIAS]       INT             NULL,
    [SIN_TOS]              BIT             NULL,
    [SIN_FIEBRE]           BIT             NULL,
    [SIN_ODINOFAGIA]       BIT             NULL,
    [SIN_DIFICULTARESPIRA] BIT             NULL,
    [SIN_FATIGA]           BIT             NULL,
    [VACUINFLUENZA]        INT             NULL,
    [DOSIS]                NUMERIC (18, 1) NULL,
    [ANTEASMA]             BIT             NULL,
    [ANTEEPOC]             BIT             NULL,
    [ANTEDIABETES]         BIT             NULL,
    [ANTEVIH]              BIT             NULL,
    [ANTECARDIACA]         BIT             NULL,
    [ANTECANCER]           BIT             NULL,
    [ANTEMALNUTRI]         BIT             NULL,
    [ANTEOBESIDAD]         BIT             NULL,
    [ANTERENAL]            BIT             NULL,
    [ANTEMEDICA]           BIT             NULL,
    [ANTEFUMADOR]          BIT             NULL,
    [ANTTUBERCULOS]        BIT             NULL,
    [ANTEOTRO]             BIT             NULL,
    [OTROSANTE]            VARCHAR (1000)  NULL,
    [RADIOTORAX]           INT             NULL,
    [ANTIBIOTICOS]         BIT             NULL,
    [LABFECHATOMA1]        DATE            NULL,
    [LABFECHARECEP1]       DATE            NULL,
    [LABMUESTRA1]          INT             NULL,
    [LABPRUEBA1]           VARCHAR (3)     NULL,
    [LABAGENTE1]           VARCHAR (3)     NULL,
    [LABRESULTADO1]        INT             NULL,
    [LABFECHARECEP11]      DATETIME        NULL,
    [LABVALOREGIS1]        VARCHAR (200)   NULL,
    [LABFECHATOMA2]        DATE            NULL,
    [LABFECHARECEP2]       DATE            NULL,
    [LABMUESTRA2]          INT             NULL,
    [LABPRUEBA2]           VARCHAR (3)     NULL,
    [LABAGENTE2]           VARCHAR (3)     NULL,
    [LABRESULTADO2]        INT             NULL,
    [LABFECHARECEP22]      DATE            NULL,
    [LABVALOREGIS2]        VARCHAR (200)   NULL,
    [CODDIAGNO]            CHAR (4)        NULL,
    [CODDEPTOMUN]          VARCHAR (50)    NULL,
    [CODPAISINT]           INT             NULL,
    [SIN_RINORREA]         BIT             NULL,
    [SIN_CONJUNTIVITIS]    BIT             NULL,
    [SIN_CEFALEA]          BIT             NULL,
    [SIN_DIARREA]          BIT             NULL,
    [SIN_OLFATO]           BIT             NULL,
    [OTROSINT]             BIT             NULL,
    [OTROSINTO]            VARCHAR (50)    NULL,
    [ANTEHIPER]            BIT             NULL,
    [SERVHOS]              INT             NULL,
    [FECHINGUCI]           DATE            NULL,
    [COMPDEPLEURAL]        BIT             NULL,
    [COMPDEPERI]           BIT             NULL,
    [COMPMIOCARDITIS]      BIT             NULL,
    [COMPSEPTICEMIA]       BIT             NULL,
    [COMPRESPI]            BIT             NULL,
    [COMPOTRO]             BIT             NULL,
    [OTROCOMPLI]           VARCHAR (50)    NULL,
    [VERSION]              VARCHAR (20)    NULL,
    [JSON]                 VARCHAR (MAX)   NULL,
    CONSTRAINT [PK_HCFICHA346] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA346_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA346_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA346] NOCHECK CONSTRAINT [CK_HCFICHA346_JSON];




GO
ALTER TABLE [dbo].[HCFICHA346] NOCHECK CONSTRAINT [CK_HCFICHA346_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Datos adicionales en formato JSON (VARCHAR MAX). Versiones antiguas=nulo. Desde V01_2020-05-08: contiene CODIGOPAIS (texto, código ISO país), nuevos campos epidemiológicos, histórico de cambios en notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevas columnas en formato JSON  -->  (versión antigua) = nulo   /    (versiones nuevas, desde V01_2020-05-08 ) =    CODIGOPAIS : texto (Codigo Internacional del País en alfanumérico )', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de ficha de notificación epidemiológica. Nulo=primera versión. Formato V01_AAAA-MM-DD. Controla evolución de formulario RIPS/notificación obligatoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Texto libre para otras complicaciones no clasificadas (VARCHAR 50). Ej: fallo multiorgánico, tromboembolismo, encefalopatía', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'OTROCOMPLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otras Complicaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'OTROCOMPLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'OTROCOMPLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de complicaciones no clasificadas en categorías estándar. True=Sí, False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'COMPOTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Complicaciones Otros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'COMPOTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'COMPOTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de insuficiencia respiratoria aguda (IRA) o falla respiratoria como complicación. True=Sí, False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'COMPRESPI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Complicaciones Falla Respiratoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'COMPRESPI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'COMPRESPI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de septicemia/choque séptico como complicación infecciosa. True=Sí, False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'COMPSEPTICEMIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Complicaciones Septicemia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'COMPSEPTICEMIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'COMPSEPTICEMIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de miocarditis (inflamación del músculo cardíaco) como complicación cardiovascular. True=Sí, False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'COMPMIOCARDITIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Complicaciones Miocarditis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'COMPMIOCARDITIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'COMPMIOCARDITIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de derrame pericárdico (líquido alrededor del corazón) como complicación. True=Sí, False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'COMPDEPERI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Complicaciones Derrame Pericardico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'COMPDEPERI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'COMPDEPERI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de derrame pleural (líquido en pleura pulmonar) como complicación respiratoria. True=Sí, False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'COMPDEPLEURAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Complicaciones Derrame Pleural', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'COMPDEPLEURAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'COMPDEPLEURAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de ingreso a Unidad de Cuidados Intensivos (UCI/UCI). Tipo DATE. Nulo si paciente no requirió UCI', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'FECHINGUCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Ingreso UCI', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'FECHINGUCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'FECHINGUCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Servicio hospitalario de internación. 1=Hospitalización general, 2=UCI. Identifica nivel de complejidad de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'SERVHOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Servicio Hospitalario: 1= Hospitalizacion General 2=UCI', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'SERVHOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'SERVHOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de hipertensión arterial (HTA). True=Sí, False=No. Comorbilidad cardiovascular relevante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTEHIPER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedente Hipertension', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTEHIPER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTEHIPER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual de otros síntomas no enumerados (VARCHAR 50). Ej: cefalea atípica, síntomas neurológicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'OTROSINTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otros Sintomas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'OTROSINTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'OTROSINTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de presencia de síntomas adicionales no clasificados. True=Sí, False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'OTROSINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sintomas Otros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'OTROSINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'OTROSINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de anosmia/pérdida del olfato como síntoma. True=Sí (presente), False=No (ausente). Síntoma neurológico específico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'SIN_OLFATO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sintomas Perdida Olfato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'SIN_OLFATO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'SIN_OLFATO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de diarrea como síntoma gastrointestinal. True=Sí, False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'SIN_DIARREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sintomas Diarrea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'SIN_DIARREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'SIN_DIARREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de cefalea/dolor de cabeza como síntoma neurológico. True=Sí, False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'SIN_CEFALEA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sintomas Cefalea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'SIN_CEFALEA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'SIN_CEFALEA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de conjuntivitis (inflamación ocular) como síntoma. True=Sí, False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'SIN_CONJUNTIVITIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sintomas Conjuntivitis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'SIN_CONJUNTIVITIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'SIN_CONJUNTIVITIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de rinorrea (secreción nasal) como síntoma respiratorio. True=Sí, False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'SIN_RINORREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sintomas Rinorrea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'SIN_RINORREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'SIN_RINORREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código ISO país internacional (VARCHAR 50). OBSOLETO desde V01_2020-05-08. Reemplazado por CODIGOPAIS en JSON. Mantiene compatibilidad histórica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'CODPAISINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo pais internacional    --> campo obsoleto desde versión ''''V01_2020-05-08'''' , lo reemplaza la columna CODIGOPAIS en campo JSON ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'CODPAISINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'CODPAISINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código departamento/municipio de residencia (VARCHAR 50). Usado para geolocalización y seguimiento epidemiológico por RIPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'CODDEPTOMUN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Dpto/municipio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'CODDEPTOMUN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'CODDEPTOMUN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico ICD-10 (CHAR 4). Ej: U07.1 (COVID-19). Vincula a diagnóstico principal de notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor registrado en segundo laboratorio (VARCHAR 200). Resultado numérico, carga viral, Ct, u otro valor cuantitativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABVALOREGIS2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor registrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABVALOREGIS2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABVALOREGIS2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de recepción de segunda muestra en laboratorio (DATE). Marca procesamiento de prueba de confirmación o seguimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABFECHARECEP22';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha recepción:', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABFECHARECEP22';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABFECHARECEP22';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de segundo análisis: 1=Positivo, 2=Negativo, 3=No procesado, 4=Inadecuado, 6=Valor registrado, 12=Contaminado hongos, 13=Muestra escasa células', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABRESULTADO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado:   1= Positivo   2= Negativo   3= No procesado   4= Inadecuado   6= Valor registrado   12= Contaminado con hongos   13= Muestra escasa de celulas ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABRESULTADO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABRESULTADO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agente patógeno identificado en prueba 2: códigos como 77=Coronavirus, 2H=2019-nCov, 40=Influenza A, 59=H1N1 pdm09, etc. Permite rastreo de variantes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABAGENTE2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Agente:   8= 8-Otro   16= 16-Adenovirus   18=18-Virus sincitial respiratorio   22= 22-Haemophilus influenzae   24= 24-Streptococcus pneumoniae   40= 40-Influenza A   41= 41-Influenza   42= 42-Parainfluenza 1   43= 43-Parainfluenza 2   44= 44-Parainfluenza 3   56= 56-Enterovirus   59= 59-Influenza A(H1N1) pdm09   64= 64-Influenza A no subtipificable   76= 76-Bocavirus   77= 77-Coronavirus   78= 78-Metaneumovirus   79= 79-Rinovirus   84= 84-virus respiratorios   1Q= 1Q-Coronavirus causante del síndrome respiratorio de oriente medio (MERS-CoV)   1R= 1R-Coronavirus subtipo 229e   1S= 1S-Coronavirus subtipo HKU1   1T= 1T-Coronavirus subtipo NL63   1U= 1U-Coronavirus subtipo OC43   1V= 1V-Influenza A(H3N2)   1W= 1W-Parainfluenza tipo 4   2H= 2H. Coronavirus subtipo 2019-nCov', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABAGENTE2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABAGENTE2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de prueba de laboratorio 2: 4=PCR, 2=IgM, 3=IgG, F3=Antígenos, H9=IgG+IgM, 92=Hemocultivo, 55=Cultivo viral. Método diagnóstico confirmatorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABPRUEBA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prueba:  4= 4-PCR   E1= E1-Aislamiento viral   6= 6-Otra --> item eliminado desde version V01_2020-05-08   30= 30-Patología   31= 31-Inmunohistoquímica   46= 46-Inhibición hemaglutinación   55= 55-Cultivo   58= 58-Antigenemia --> item eliminado desde version V01_2020-05-08   76=76-IFI   92= 92-Hemocultivo     Los siguientes son items nuevos desde version V01_2020-05-08 :   2 = 2 - IgM   3 = 3 - IgG   F3 = F3 - Determinación de antígenos   H9 = H9 - IgG , IgM   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABPRUEBA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABPRUEBA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de muestra segunda prueba: 1=Sangre total, 3=Hisopado nasofaríngeo, 8=Aspirado nasofaríngeo, 11=Líquidos estériles, 22=Lavado bronquial. Define tipo de especimen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABMUESTRA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Muestra:   1= 1-Sangre total   3= 3-Hisopado nasofaríngeo   4= 4-Tejido   8= 8-Aspirado nasofaríngeo   9= 9- Lavado nasal  --> item eliminado desde version V01_2020-05-08   11=  11-Lavado broncoalveolar (antes)  / 11 - Otros líquidos estériles (ahora) -->  item modificado desde version V01_2020-05-08   22= 22-Lavado bronquial ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABMUESTRA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABMUESTRA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de recepción de segunda muestra en laboratorio (DATE). Control de tiempo entre toma y procesamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABFECHARECEP2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha recepción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABFECHARECEP2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABFECHARECEP2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de toma de segunda muestra (DATE). Segunda toma para confirmación, monitoreo viral o seguimiento post-alta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABFECHATOMA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de toma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABFECHATOMA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABFECHATOMA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor registrado en primer laboratorio (VARCHAR 200). Carga viral, Ct cycle, resultado cuantitativo o descriptivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABVALOREGIS1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor registrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABVALOREGIS1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABVALOREGIS1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha/hora de recepción de primera muestra en laboratorio (DATETIME). Incluye hora para trazabilidad del procesamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABFECHARECEP11';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha recepción:', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABFECHARECEP11';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABFECHARECEP11';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado prueba 1: 1=Positivo, 2=Negativo, 3=No procesado, 4=Inadecuado, 6=Valor registrado, 12=Contaminado, 13=Muestra escasa. Determina confirmación diagnóstica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABRESULTADO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado:   1= Positivo   2= Negativo   3= No procesado   4= Inadecuado   6= Valor registrado   12= Contaminado con hongos   13= Muestra escasa de celulas ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABRESULTADO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABRESULTADO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agente patógeno identificado prueba 1: 77=Coronavirus, 2H=nCoV-2019, 40=Influenza A, 59=H1N1, 78=Metaneumovirus, etc. Identifica virus causante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABAGENTE1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Agente:   8= 8-Otro   16= 16-Adenovirus   18=18-Virus sincitial respiratorio   22= 22-Haemophilus influenzae   24= 24-Streptococcus pneumoniae   40= 40-Influenza A   41= 41-Influenza   42= 42-Parainfluenza 1   43= 43-Parainfluenza 2   44= 44-Parainfluenza 3   56= 56-Enterovirus   59= 59-Influenza A(H1N1) pdm09   64= 64-Influenza A no subtipificable   76= 76-Bocavirus   77= 77-Coronavirus   78= 78-Metaneumovirus   79= 79-Rinovirus   84= 84-virus respiratorios   1Q= 1Q-Coronavirus causante del síndrome respiratorio de oriente medio (MERS-CoV)   1R= 1R-Coronavirus subtipo 229e   1S= 1S-Coronavirus subtipo HKU1   1T= 1T-Coronavirus subtipo NL63   1U= 1U-Coronavirus subtipo OC43   1V= 1V-Influenza A(H3N2)   1W= 1W-Parainfluenza tipo 4   2H= 2H. Coronavirus subtipo 2019-nCov', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABAGENTE1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABAGENTE1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo prueba 1: 4=PCR (oro estándar), 2=IgM, 3=IgG, F3=Antígenos, H9=serologías, 92=Hemocultivo, 55=Cultivo, 31=Inmunohistoquímica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABPRUEBA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prueba:  4= 4-PCR   E1= E1-Aislamiento viral   6= 6-Otra --> item eliminado desde version V01_2020-05-08   30= 30-Patología   31= 31-Inmunohistoquímica   46= 46-Inhibición hemaglutinación   55= 55-Cultivo   58= 58-Antigenemia --> item eliminado desde version V01_2020-05-08   76=76-IFI   92= 92-Hemocultivo     Los siguientes son items nuevos desde version V01_2020-05-08 :   2 = 2 - IgM   3 = 3 - IgG   F3 = F3 - Determinación de antígenos   H9 = H9 - IgG , IgM   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABPRUEBA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABPRUEBA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo muestra 1: 1=Sangre total, 3=Hisopado nasofaríngeo, 4=Tejido, 8=Aspirado nasofaríngeo, 11=Líquidos estériles, 22=Lavado bronquial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABMUESTRA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Muestra:   1= 1-Sangre total   3= 3-Hisopado nasofaríngeo   4= 4-Tejido   8= 8-Aspirado nasofaríngeo   9= 9- Lavado nasal --> item eliminado desde version V01_2020-05-08   11=  11-Lavado broncoalveolar (antes)  / 11 - Otros líquidos estériles (ahora) -->  item modificado desde version V01_2020-05-08   22= 22-Lavado bronquial ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABMUESTRA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABMUESTRA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha recepción primera muestra en laboratorio (DATE). Marca inicio procesamiento diagnóstico, control de turnaround time', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABFECHARECEP1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha recepción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABFECHARECEP1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABFECHARECEP1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de toma de primera muestra (DATE). Punto de partida para diagnóstico confirmatorio. Diferencia entre toma y recepción define calidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABFECHATOMA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de toma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABFECHATOMA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABFECHATOMA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usó antibióticos en última semana antes de notificación. True=Sí, False=No. Ayuda a evaluar comorbilidades, coinfecciones bacterianas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTIBIOTICOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usó antibiótico en la última semana:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTIBIOTICOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTIBIOTICOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado radiografía de tórax: 1=Infiltrado alveolar/neumonía, 2=Infiltrados intersticiales, 3=Infiltrados basales vidrio esmerilado, 4=Normal. Gravedad pulmonar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'RADIOTORAX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se tomó radiografía de tórax:   1= 1. Infiltrado alveolar o neumonía   2= 2. Infiltrados intersticiales   3= 3. Infiltrados basales en vidrio esmerilado 4= 4. Ninguno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'RADIOTORAX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'RADIOTORAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de otros antecedentes médicos significativos (VARCHAR 1000). Libre: cirugías previas, medicaciones crónicas, alergias, eventos relevantes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'OTROSANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuáles otros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'OTROSANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'OTROSANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de existencia de otros antecedentes médicos no listados. True=Sí, False=No. Requiere revisar OTROSANTE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTEOTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Anteotro:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTEOTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTEOTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de tuberculosis (TB, TBC) activa o latente. True=Sí, False=No. Comorbilidad respiratoria crónica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTTUBERCULOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antetuberculos:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTTUBERCULOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTTUBERCULOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de tabaquismo activo o pasado. True=Sí (fumador), False=No (nunca fumó). Factor de riesgo pulmonar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTEFUMADOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antefumador:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTEFUMADOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTEFUMADOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de medicaciones crónicas o tratamiento regular. True=Sí, False=No. Inmunosupresión, enfermedad crónica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTEMEDICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antemedica:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTEMEDICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTEMEDICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de enfermedad renal crónica (ERC, diálisis, trasplante). True=Sí, False=No. Comorbilidad con riesgo aumentado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTERENAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Anterenal:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTERENAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTERENAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de obesidad (IMC≥30). True=Sí, False=No. Factor de riesgo de gravedad y complicaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTEOBESIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Anteobesidad:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTEOBESIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTEOBESIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de malnutrición o desnutrición crónica. True=Sí, False=No. Factor de riesgo inmunológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTEMALNUTRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antemalnutri:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTEMALNUTRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTEMALNUTRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de cáncer activo o remisión. True=Sí, False=No. Inmunosupresión, riesgo aumentado complicaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTECANCER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecancer:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTECANCER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTECANCER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de enfermedad cardiovascular: insuficiencia cardíaca, cardiopatía isquémica, arritmia. True=Sí, False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTECARDIACA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecardiaca:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTECARDIACA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTECARDIACA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de VIH/SIDA. True=Sí (positivo), False=No (negativo/desconocido). Inmunosupresión, riesgo severo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTEVIH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'AnteVIH:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTEVIH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTEVIH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de diabetes mellitus tipo 1 o 2. True=Sí, False=No. Factor de riesgo complicaciones vasculares', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTEDIABETES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antediabetes:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTEDIABETES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTEDIABETES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de Enfermedad Pulmonar Obstructiva Crónica (EPOC, enfisema, bronquitis crónica). True=Sí, False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTEEPOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Anteepoc:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTEEPOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTEEPOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de asma bronquial. True=Sí, False=No. Comorbilidad respiratoria reactiva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTEASMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Anteasma:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTEASMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTEASMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis de vacuna recibida (NUMERIC 18,1). Número de dosis aplicadas (1, 2, 3, etc.). Esquema de inmunización', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'DOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'DOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'DOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vacuna influenza estacional: 1=Sí vacunado, 2=No vacunado, 3=Desconocido. Inmunización previa relevante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'VACUINFLUENZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Influenza estacional:   1=Si   2=No   3=Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'VACUINFLUENZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'VACUINFLUENZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de fatiga/cansancio extremo como síntoma. True=Sí (presente), False=No (ausente)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'SIN_FATIGA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sin_fatiga:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'SIN_FATIGA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'SIN_FATIGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de disnea/dificultad respiratoria como síntoma. True=Sí, False=No. Síntoma grave de insuficiencia pulmonar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'SIN_DIFICULTARESPIRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sin_Dificultarespira:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'SIN_DIFICULTARESPIRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'SIN_DIFICULTARESPIRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de odinofagia/dolor al tragar como síntoma. True=Sí, False=No. Síntoma faringo-laríngeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'SIN_ODINOFAGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sin_Odinofagia:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'SIN_ODINOFAGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'SIN_ODINOFAGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de fiebre/temperatura elevada como síntoma. True=Sí, False=No. Signo cardinal de enfermedad infecciosa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'SIN_FIEBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sin_fiebre:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'SIN_FIEBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'SIN_FIEBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de tos como síntoma respiratorio. True=Sí, False=No. Síntoma más frecuente infección respiratoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'SIN_TOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sin_tos:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'SIN_TOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'SIN_TOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contacto cercano en últimos 14 días con caso probable/confirmado infección respiratoria por virus nuevo: 1=Sí, 2=No. Criterio epidemiológico de exposición', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'CONTACTO14DIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tuvo contacto estrcho en los últimos 14 días con caso probable o confirmado con infección respiratoria por virus nuevo:   1=Si   2=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'CONTACTO14DIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'CONTACTO14DIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lugar o país de viaje internacional (VARCHAR 50). Identificación de zona de procedencia de exposición. Nulo si no viajó', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LUGARVIAJEINTER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Donde', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LUGARVIAJEINTER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LUGARVIAJEINTER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Viaje fue internacional. True=Sí (viaje al exterior), False=No. Criterio de importación de enfermedad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'VIAJETERINTER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'El viaje fue Internacional:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'VIAJETERINTER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'VIAJETERINTER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lugar/departamento de viaje nacional (VARCHAR 50). Zona dentro territorio nacional. Nulo si no viajó internamente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LUGARVIAJENAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Donde', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LUGARVIAJENAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LUGARVIAJENAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Viaje fue en territorio nacional. True=Sí (viaje interno), False=No. Criterio de dispersión nacional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'VIAJETERNAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'El viaje fue en el territorio Nacional:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'VIAJETERNAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'VIAJETERNAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Viajó a áreas con circulación activa de virus nuevo: 1=Sí, 2=No. Exposición en zona endémica o brote activo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'VIAJOAREASVIRUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Viajó a áreas de circulación del virus nuevo:   1=Si   2=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'VIAJOAREASVIRUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'VIAJOAREASVIRUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Trabajador de salud u otro personal de riesgo ocupacional: 1=Sí, 2=No. Identifica exposición laboral en centros sanitarios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'TRABADESALUD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Es trabajador de la salud u otro persona:   1=Si   2=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'TRABADESALUD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'TRABADESALUD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de ficha notificación epidemiológica relacionada. FK a HCFICHANOTIFICACION. Vincula datos clínicos al evento notificado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY 1,1). Clave primaria autonumérica de registro epidemiológico en HCFICHA346', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha epidemiológica de notificación de enfermedades respiratorias (como influenza, COVID-19 u otras de vigilancia obligatoria). Registra antecedentes de exposición, síntomas, comorbilidades, resultados de laboratorio y complicaciones del paciente notificado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346';
