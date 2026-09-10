CREATE TABLE [dbo].[PRMODELOHC] (
    [ID]                  INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODIGO]              VARCHAR (5)     NOT NULL,
    [NOMBRE]              VARCHAR (100)   NOT NULL,
    [DESCRIPCION]         VARCHAR (500)   NULL,
    [ESTADO]              BIT             NOT NULL,
    [FECHACREA]           DATETIME        NOT NULL,
    [USUACREA]            CHAR (20)       NOT NULL,
    [IMAGEN]              VARBINARY (MAX) NOT NULL,
    [MOSTRARCURVAS]       BIT             CONSTRAINT [DF_PRMODELOHC_MOSTRARCURVAS] DEFAULT ((0)) NOT NULL,
    [MOSTRARVACUNACION]   BIT             NULL,
    [MOSTRARTFG]          BIT             CONSTRAINT [DF_PRMODELOHC_MOSTRARTFG] DEFAULT ((0)) NOT NULL,
    [MOSTRARJUNTAMEDICA]  BIT             CONSTRAINT [DF__PRMODELOH__MOSTR__3188B38A] DEFAULT ((0)) NOT NULL,
    [MOSTRARFAMILIOGRAMA] BIT             NULL,
    [MOSTRARECOMAPA]      BIT             NULL,
    [SpecialtyType]       INT             NULL,
    [Gender]              INT             NULL,
    CONSTRAINT [PK_PRMODELOHC] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Género aplicable al modelo HC: 1=Femenino, 2=Masculino, 3=Ambos. Filtro demográfico para plantilla de historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'Gender';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 Femenino   2 Masculino    3 Ambos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'Gender';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'Gender';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de especialidad médica asociada (0=Ninguno, 1=Salud visual, 2=Nutrición, 3=Psicología, 4=Anestesiología, 5=Mastología, 6=Oncología, 7=Hematología, 8=Ortopedia, 9=Cirugía plástica, 10=Urología, 11=Dermatología, 12=Cuidados paliativos, 13=Fisioterapia, 14=Radioterapia, 15=Cirugía, 16=Especialidades clínicas, 17=Ginecología, 18=Radiología, 19=Trabajo social).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'SpecialtyType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'0 - Ninguno   1 Salud visual   2 Nutrición   3 Psicología    4 Anestesiología   5 Mastología   6 Oncología   7 Hematología   8 Ortopedia   9 Cirugía plástica   10 Urología   11 Dermatología   12 Cuidados paliativos   13 Fisioterapia   14 Radioterapia   15 Cirugía   16 Especialidades clínicas   17 Ginecología   18 Radiología   19 Trabajo social ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'SpecialtyType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'SpecialtyType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera booleana (1=Sí, 0=No) para mostrar u ocultar el ecomapa en la historia clínica. Define visibilidad de mapa relacional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'MOSTRARECOMAPA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'mostrar ecomapa 1=  true  0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'MOSTRARECOMAPA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'MOSTRARECOMAPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (1=Sí, 0=No) para mostrar u ocultar el familiograma en el modelo de HC. Controla visualización del árbol genealógico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'MOSTRARFAMILIOGRAMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda mostrar  familiograma   1 = Si   0 = No ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'MOSTRARFAMILIOGRAMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'MOSTRARFAMILIOGRAMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera booleana que define si se debe mostrar o habilitar el control/seguimiento de junta médica en la plantilla HC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'MOSTRARJUNTAMEDICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Define si se debe mostrar el control de junta medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'MOSTRARJUNTAMEDICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'MOSTRARJUNTAMEDICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (1=Verdadero, 0=Falso) para mostrar u ocultar la sección de Test de Funcionalidad Geriátrica (TFG) en la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'MOSTRARTFG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'mostrar TFG  1 = True    0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'MOSTRARTFG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'MOSTRARTFG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera booleana (1=Sí, 0=No) para mostrar u ocultar el historial y plan de vacunación en la plantilla de HC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'MOSTRARVACUNACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda mostrar vacunacion  1 = si   0 = No ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'MOSTRARVACUNACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'MOSTRARVACUNACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (1=Sí, 0=No) para mostrar u ocultar las curvas de crecimiento y desarrollo en la historia clínica pediátrica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'MOSTRARCURVAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda mostrar curvas de crecimiento  1 = Si  0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'MOSTRARCURVAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'MOSTRARCURVAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacenamiento binario (VARBINARY) de la imagen/logo del modelo de historia clínica para visualización en interfaz.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'IMAGEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la imagen de modelo HC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'IMAGEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'IMAGEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del usuario que creó el registro del modelo HC. Campo de auditoría (20 caracteres). PII sensible.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'USUACREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Usuario de  Creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'USUACREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'USUACREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Timestamp de creación del modelo de historia clínica. Registro de auditoría DATETIME.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'FECHACREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la Fecha Creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'FECHACREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'FECHACREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado operativo del modelo HC: 1=Activo (disponible para uso), 0=Inactivo (archivado/deshabilitado).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el estado   1 = Activo    0 =Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual extendida del modelo HC. Detalle de propósito, alcance y características específicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion Modelo HC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo del modelo de historia clínica. Identificador legible para profesionales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'NOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Modelo HC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'NOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'NOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único corto (5 caracteres) del modelo HC. Identificador de negocio para referencias rápidas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'CODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Modelo HC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'CODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'CODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único automático (INT IDENTITY). Clave primaria secuencial de la tabla PRMODELOHC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de modelos o plantillas de historia clínica electrónica. Define los tipos de formato de HC disponibles en el sistema, indicando qué secciones o módulos clínicos se habilitan en cada modelo (curvas de crecimiento, vacunación, tasa de filtración glomerular, junta médica, familiograma, ecomapa), así como la especialidad y el género al que aplica cada plantilla.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC';
