CREATE TABLE [dbo].[IHLISTPRO] (
    [CODPRODUC]       CHAR (20)       NOT NULL,
    [CODDCIMED]       CHAR (20)       NULL,
    [DESPRODUC]       CHAR (255)      NOT NULL,
    [NOPOSPROD]       BIT             NOT NULL,
    [TIPPRODUC]       CHAR (1)        NOT NULL,
    [MANCONPRO]       BIT             NOT NULL,
    [REGINVACT]       BIT             NOT NULL,
    [REGINVIMA]       CHAR (30)       NULL,
    [CODGRUFAR]       CHAR (20)       NULL,
    [CODVIAADM]       CHAR (20)       NULL,
    [CONCENMED]       CHAR (50)       NULL,
    [PRESENMED]       CHAR (160)      NULL,
    [CODFORMED]       CHAR (20)       NULL,
    [TIEESTMED]       INT             NULL,
    [TIPFORMED]       CHAR (1)        NULL,
    [PESTOTMED]       NUMERIC (18, 2) NULL,
    [CODUNIPES]       CHAR (20)       NULL,
    [VOLTOTMED]       NUMERIC (18, 2) NULL,
    [CODUNIVOL]       CHAR (20)       NULL,
    [CODUNIADM]       CHAR (20)       NULL,
    [CALCANAUT]       BIT             NULL,
    [ESPDILPRO]       BIT             NULL,
    [ABRPROMEZ]       CHAR (200)      NULL,
    [PROESTADO]       BIT             NOT NULL,
    [PROCONTRO]       BIT             NOT NULL,
    [TODASPATO]       BIT             NOT NULL,
    [MANLOCALI]       BIT             NOT NULL,
    [RETRASOGE]       BIT             NOT NULL,
    [CODUSUCRE]       CHAR (20)       NULL,
    [FECUSUCRE]       DATETIME        NULL,
    [CODUSUMOD]       CHAR (20)       NULL,
    [FECUSUMOD]       DATETIME        NULL,
    [JUSINMEDI]       BIT             NOT NULL,
    [CODNIVRIE]       VARCHAR (20)    NULL,
    [CODJUMEES]       BIT             NOT NULL,
    [ADVERTENC]       VARCHAR (MAX)   NULL,
    [POSOLOGIA]       VARCHAR (MAX)   NULL,
    [MEDTRAZA]        BIT             NULL,
    [MATOSTOSIN]      BIT             NULL,
    [MEDICAMENTONPT]  BIT             CONSTRAINT [DF_IHLISTPRO_MEDICAMENTONOT] DEFAULT ((0)) NOT NULL,
    [OSMOLARIDAD]     DECIMAL (18, 2) NULL,
    [DENSIDAD]        DECIMAL (6, 4)  NULL,
    [UNIDADMANEJO]    INT             NULL,
    [CONSUMPTION]     BIT             NULL,
    [OPTOMETRYDEVICE] BIT             CONSTRAINT [DF__IHLISTPRO__OPTOM__706FE89B] DEFAULT ((0)) NOT NULL,
    [AddedAutomatic]  BIT             CONSTRAINT [DF_IHLISTPRO_AddedAutomatic] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_CODPRODUC] PRIMARY KEY CLUSTERED ([CODPRODUC] ASC),
    CONSTRAINT [FK_IHLISTPRO_INIVERIES] FOREIGN KEY ([CODNIVRIE]) REFERENCES [dbo].[INIVERIES] ([CODNIVRIE])
);




GO



GO
CREATE NONCLUSTERED INDEX [IX_IHLISTPRO_PROESTADO_TIPPRODUC_CONSUMPTION]
    ON [dbo].[IHLISTPRO]([PROESTADO] ASC, [TIPPRODUC] ASC, [CONSUMPTION] ASC)
    INCLUDE([DESPRODUC], [JUSINMEDI]);


GO
CREATE NONCLUSTERED INDEX [IX_ESTATIPO]
    ON [dbo].[IHLISTPRO]([PROESTADO] ASC, [TIPPRODUC] ASC)
    INCLUDE([CODPRODUC], [DESPRODUC]);


GO
CREATE NONCLUSTERED INDEX [IX_MedPrescripciones]
    ON [dbo].[IHLISTPRO]([CODDCIMED] ASC, [ESPDILPRO] ASC, [PROESTADO] ASC, [TIPPRODUC] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de creación automática (BIT): marca si el producto fue creado automáticamente desde la historia clínica por el médico. Desde 05-07-2023.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'AddedAutomatic';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'05-07-2023 se cra columna en la cual me va identificar si el producto fue creado de forma automatica desde la historia clinica por el medico. ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'AddedAutomatic';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'AddedAutomatic';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de dispositivo optométrico (BIT): identifica si el producto es un dispositivo para oftalmología/óptica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'OPTOMETRYDEVICE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si Dispositivo Optometría   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'OPTOMETRYDEVICE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'OPTOMETRYDEVICE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de consumo (BIT): señala si el producto es un insumo de consumo. Se completa cuando se clasifica como item insumo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CONSUMPTION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite saber si es de consumo, este campo se llena cuando es de item insumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CONSUMPTION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CONSUMPTION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de manejo (INT): cantidad mínima de descarga para gestionar el producto. Define la menor unidad operativa de solicitud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'UNIDADMANEJO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de manejo, Campo que contiene la cantidad de descarga mínima a la que pretendemos manejar el producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'UNIDADMANEJO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'UNIDADMANEJO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Densidad del medicamento (DECIMAL 6,4): propiedad física que almacena la densidad específica del fármaco.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'DENSIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene la densidad del medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'DENSIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'DENSIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Osmolaridad del medicamento (DECIMAL 18,2): propiedad coligativa que contiene la osmolaridad en mOsm/L del medicamento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'OSMOLARIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene la osmolaridad del medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'OSMOLARIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'OSMOLARIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medicamento NPT (BIT): identifica si el producto es un medicamento para Nutrición Parenteral Total.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'MEDICAMENTONPT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica si es un medicamento NPT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'MEDICAMENTONPT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'MEDICAMENTONPT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Material de osteosíntesis (BIT): marca si el producto es material óseo/de fijación ósea. Solo aplica cuando tipo de producto = suministro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'MATOSTOSIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica si el producto es material de ostosintesis.  solo puede ser "si", cuando el tipo de producto sea suministro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'MATOSTOSIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'MATOSTOSIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medicamento trazador (BIT): indica si el medicamento requiere seguimiento trazable en farmacovigilancia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'MEDTRAZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determinia si el medicamento es trazador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'MEDTRAZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'MEDTRAZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Posología (VARCHAR MAX): instrucción completa de dosis, frecuencia y duración según ficha técnica del medicamento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'POSOLOGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Posologia que correspondiente a la ficha tecnica del producto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'POSOLOGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'POSOLOGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Advertencias (VARCHAR MAX): contraindicaciones, precauciones e interacciones del medicamento según ficha técnica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'ADVERTENC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Adverntencias correspondiente a la ficha tecnica del producto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'ADVERTENC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'ADVERTENC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código justificación medicamentos especiales (BIT): identificador que vincula a normativa de medicamentos con restricción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODJUMEES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la justificacion de medicamentos especiales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODJUMEES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODJUMEES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código nivel de riesgo (VARCHAR 20): FK a INIVERIES. Clasifica el riesgo farmacológico del medicamento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODNIVRIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Nivel Riesgo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODNIVRIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODNIVRIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación requerida (BIT): especifica si el medicamento/insumo exige justificación médica para su dispensación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'JUSINMEDI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si exige justificacion de insumos / medicamentos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'JUSINMEDI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'JUSINMEDI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha última modificación (DATETIME): timestamp del último cambio del registro del medicamento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'FECUSUMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del Modificacion del registro ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'FECUSUMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'FECUSUMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código usuario modificador (CHAR 20): ID del usuario que realizó la última modificación del registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Ultimo Usuario que modifica el registro ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha creación (DATETIME): timestamp de creación inicial del registro del medicamento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'FECUSUCRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creacion del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'FECUSUCRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'FECUSUCRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código usuario creador (CHAR 20): ID del usuario que creó el registro del medicamento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODUSUCRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Usuario que crea el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODUSUCRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODUSUCRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Traslado de sobrantes (BIT): 1=Sí realiza traslado de medicamento sobrante; 0=No realiza.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'RETRASOGE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Realiza traslado de Sobrantes Si=1;No=3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'RETRASOGE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'RETRASOGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Manejo de localización (BIT): 1=Producto requiere seguimiento de ubicación física; 0=No requiere.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'MANLOCALI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Producto Maneja Localizacion 1: Si; 0:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'MANLOCALI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'MANLOCALI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Aplica a todas patologías POS (BIT): especifica si el medicamento POS cubre todas las patologías del paciente o es restringido.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'TODASPATO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el producto Pos aplica para todos las patologias del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'TODASPATO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'TODASPATO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Producto de control (BIT): marca si el medicamento está bajo regulación de control especial (drogas controladas, estupefacientes).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'PROCONTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Producto de Control', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'PROCONTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'PROCONTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del producto (BIT): 1=Activo, dispensable; 0=Inactivo, no disponible para prescripción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'PROESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del medicamento:  True: Activo  False: Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'PROESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'PROESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Abreviatura para mezclas (CHAR 200): código corto del medicamento para mostrarlo en fórmulas compuestas/mezclas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'ABRPROMEZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Abreviatura del Producto para mostrar en mezclas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'ABRPROMEZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'ABRPROMEZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diluyente (BIT): especifica si el producto funciona como agente diluyente en preparaciones farmacéuticas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'ESPDILPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el producto funciona como Diluyente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'ESPDILPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'ESPDILPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cálculo automático de cantidades (BIT): 1=Cálculo básico automático (duración, dosis, frecuencia); 0=No calcula. El sistema realiza cálculo simple.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CALCANAUT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el sistema realiza un calculo basico automatico para determinar el numero de producto a solicitar segun la orden medica.    True: Calculo Basico  False: No Calcula    Nota: El sistema realiza dos tipos de calculo automatico de cantidades:    1. Complejo: Tomando como base, la duracion, dosis, frecuencia, presentacion (peso total o volumen total, o unidad de administracion) y la estabilidad del producto.    2. Basico: Tomando como base la duracion, dosis y frecuencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CALCANAUT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CALCANAUT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código unidad de administración (CHAR 20): FK. Unidad de dosificación (sobres, tabletas compuestas, cremas, etc).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODUNIADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unidad de Administracion  Nota: Opcion disponible para Cremas, Sobres, Tabletas Compuestas, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODUNIADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODUNIADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código unidad de volumen (CHAR 20): FK. Unidad de medida para medicamentos líquidos (ml, L).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODUNIVOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad de Medida del Volumen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODUNIVOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODUNIVOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen total medicamento (NUMERIC 18,2): volumen de presentación. Ej: 20ml en presentación de 10mg/2ml. NULL para tabletas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'VOLTOTMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Volumen Total del Medicamento  Presentacion    Ejemplo:   Concentracion: 10mg / 2ml  Presentacion: 20 ml    Volumen Total =  20 ml     Nota: Cuando no aplique volumen ej: Tabletas, se deja Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'VOLTOTMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'VOLTOTMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código unidad de peso (CHAR 20): FK. Unidad de medida para medicamentos sólidos (mg, g).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODUNIPES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad de Medida del Peso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODUNIPES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODUNIPES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso total medicamento (NUMERIC 18,2): cantidad total de principio activo en presentación. Ej: 100mg. NULL para tabletas compuestas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'PESTOTMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Peso Total del Medicamento  Concentracion X Presentacion    Ejemplo:   Concentracion: 10mg / 2ml  Presentacion: 20 ml    Peso Total =  (10 * 20 / 2)    Nota: Cuando no aplique peso ej: Tabletas Compuestas, se deja Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'PESTOTMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'PESTOTMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de formulación (CHAR 1): 1=Peso(mg), 2=Volumen(ml), 3=Peso-Volumen, 4=Unidad de administración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'TIPFORMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Formulacion del medicamento:  1 Peso  2 Volumen  3 Peso-Volumen  4 Unidad de Administracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'TIPFORMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'TIPFORMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estabilidad temperatura ambiente (INT): horas de estabilidad a 25°C. Variables adicionales de enfermería se cargan por el personal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'TIEESTMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Horas de estabilidad del medicamento a temperatura ambiente. En el caso de que el medicamento se encuentre expuesto a otras condiciones (Clima, Mezclado, etc) estas variables se almacenran desde el modulo de enfermeria y dichos valores se cargaran por el personal de enfermeria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'TIEESTMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'TIEESTMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código forma farmacéutica (CHAR 20): FK. Clasificación: tableta, cápsula, solución inyectable, crema, jarabe, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODFORMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Forma Farmaceutica del medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODFORMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODFORMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presentación del medicamento (CHAR 160): formato comercial/dispensable. Ej: caja x10 tabletas, frasco x100ml.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'PRESENMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Presentacion del Medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'PRESENMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'PRESENMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concentración del medicamento (CHAR 50): proporción del principio activo. Ej: 50mg/5ml, 500mg/Tableta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CONCENMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concentracion del Medicamento    Ej: 50mg / 5 ml - 500mg / Tab', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CONCENMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CONCENMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código vía de administración (CHAR 20): FK. Ruta farmacéutica: oral, IV, IM, tópica, rectal, inhalada, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Via de Administracion Comun', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODVIAADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código grupo farmacológico (CHAR 20): FK. Clasificación terapéutica: antibiótico, analgésico, antihipertensivo, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODGRUFAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Grupo Farmacologico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODGRUFAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODGRUFAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro INVIMA (CHAR 30): número de aprobación regulatoria de la autoridad sanitaria colombiana.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'REGINVIMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Registro INVIMA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'REGINVIMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'REGINVIMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiene registro INVIMA (BIT): 1=Medicamento registrado en INVIMA; 0=No registrado o eximido.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'REGINVACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Define si el producto tiene registro INVIMA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'REGINVACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'REGINVACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Producto de control (BIT): marca si el medicamento es sustancia controlada bajo regulaciones especiales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'MANCONPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Es Producto de Control', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'MANCONPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'MANCONPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de producto (CHAR 1): 1=Medicamento, 2=Materiales/insumos, 3=Medicamento disponible como insumo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'TIPPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Producto  
1: Medicamentos  
2: Materiales e Insumos  
3: Medicamento disponible como Insumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'TIPPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'TIPPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medicamento NO POS (BIT): 1=NO POS (no cubierto por plan obligatorio); 0=POS (cubierto).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'NOPOSPROD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si el medicamento es NO POS:  True: NO POS  False: POS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'NOPOSPROD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'NOPOSPROD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del producto (CHAR 255): nombre comercial o genérico corto del medicamento/insumo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'DESPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion Corta del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'DESPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'DESPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código Denominación Común Internacional (CHAR 20): DCI, nombre genérico internacional del fármaco.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODDCIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Denominacion Internacion del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODDCIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODDCIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código interno del producto (CHAR 20, PK): identificador único del medicamento/insumo en el sistema Indigo Vie.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Interno del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo maestro de productos farmacéuticos y dispositivos médicos. Contiene la información de cada medicamento, insumo o producto de salud disponible en el sistema, incluyendo sus características técnicas, de dosificación, presentación, control y administración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO';
