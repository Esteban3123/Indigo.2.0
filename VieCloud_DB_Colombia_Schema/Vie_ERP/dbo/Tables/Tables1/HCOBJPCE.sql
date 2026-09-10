CREATE TABLE [dbo].[HCOBJPCE] (
    [CODOBJPCE] INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NOMOBJPCE] VARCHAR (1000) NOT NULL,
    [ESTADO]    INT            NULL,
    CONSTRAINT [PK_HCOBJPCE] PRIMARY KEY CLUSTERED ([CODOBJPCE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del objeto de plan de cuidado de enfermería: 1=Activo, 2=Inactivo. Indicador booleano que controla la disponibilidad del registro para uso clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCOBJPCE', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardo el estado   1 = Activo  2 = Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCOBJPCE', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCOBJPCE', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción del objeto de plan de cuidado de enfermería. Texto de hasta 1000 caracteres que identifica intervenciones, diagnósticos o actividades enfermeras en protocolos de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCOBJPCE', @level2type = N'COLUMN', @level2name = N'NOMOBJPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardo el nombre objetos o descripcion de planes de cuidado de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCOBJPCE', @level2type = N'COLUMN', @level2name = N'NOMOBJPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCOBJPCE', @level2type = N'COLUMN', @level2name = N'NOMOBJPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único (PK) del objeto de plan de cuidado de enfermería. Identificador numérico autoincrementado que referencia elementos de planes de cuidado, protocolos de enfermería e intervenciones clínicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCOBJPCE', @level2type = N'COLUMN', @level2name = N'CODOBJPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el código objetos de planes  de cuidado de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCOBJPCE', @level2type = N'COLUMN', @level2name = N'CODOBJPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCOBJPCE', @level2type = N'COLUMN', @level2name = N'CODOBJPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de objetivos o propósitos de atención del paciente en historia clínica. Registra los diferentes tipos de objetivos clínicos que pueden asignarse durante el proceso de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCOBJPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCOBJPCE';
