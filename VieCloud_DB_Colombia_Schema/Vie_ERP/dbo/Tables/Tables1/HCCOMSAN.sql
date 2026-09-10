CREATE TABLE [dbo].[HCCOMSAN] (
    [ID]                          INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCOMSAM]                   VARCHAR (20)    NOT NULL,
    [DESCOMSAM]                   VARCHAR (200)   NOT NULL,
    [VALIDAHEMOCLA]               INT             NULL,
    [MinimumContentHemocomponent] NUMERIC (18, 1) NULL,
    [MaximumContentHemocomponent] NUMERIC (18, 1) NULL,
    [State]                       BIT             NULL,
    [FrozenFreshPlasma]           INT             DEFAULT ((2)) NULL,
    CONSTRAINT [PK__HCCOMSAN__DE084E6E6079D22F] PRIMARY KEY CLUSTERED ([ID] ASC)
);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del componente sanguíneo (BIT): 1=Activo/habilitado para transfusión, 0=Inactivo/deshabilitado en el catálogo de hemocomponentes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAN', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del componente Sanguineo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAN', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAN', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contenido máximo de la unidad (NUMERIC 18.1 ml), volumen máximo permitido del hemocomponente en bolsa (ej: 350ml máximo para glóbulos rojos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAN', @level2type = N'COLUMN', @level2name = N'MaximumContentHemocomponent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contenido máximo de la unidad (ml)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAN', @level2type = N'COLUMN', @level2name = N'MaximumContentHemocomponent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAN', @level2type = N'COLUMN', @level2name = N'MaximumContentHemocomponent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contenido mínimo de la unidad (NUMERIC 18.1 ml), volumen menor permitido del hemocomponente en bolsa (ej: 150ml mínimo para glóbulos rojos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAN', @level2type = N'COLUMN', @level2name = N'MinimumContentHemocomponent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contenido mínimo de la unidad (ml)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAN', @level2type = N'COLUMN', @level2name = N'MinimumContentHemocomponent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAN', @level2type = N'COLUMN', @level2name = N'MinimumContentHemocomponent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Validación de hemoclasificación (INT): 1=Sí requiere compatibilidad ABO-RhD entre bolsa y paciente, 2=No; aplicable a transfusiones y banco de sangre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAN', @level2type = N'COLUMN', @level2name = N'VALIDAHEMOCLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Validar Hemoclasificación de bolsa y paciente 1->Si, 2->No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAN', @level2type = N'COLUMN', @level2name = N'VALIDAHEMOCLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAN', @level2type = N'COLUMN', @level2name = N'VALIDAHEMOCLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del componente sanguíneo (VARCHAR 200), nombre completo: glóbulos rojos concentrados, plasma fresco congelado, concentrado plaquetario, crioprecipitado, sangre total, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAN', @level2type = N'COLUMN', @level2name = N'DESCOMSAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del Componete Sanguineo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAN', @level2type = N'COLUMN', @level2name = N'DESCOMSAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAN', @level2type = N'COLUMN', @level2name = N'DESCOMSAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del componente sanguíneo (VARCHAR 20), identificador funcional de glóbulos rojos, plaquetas, plasma, crioprecipitado u otros hemocomponentes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAN', @level2type = N'COLUMN', @level2name = N'CODCOMSAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Componete Sanguineo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAN', @level2type = N'COLUMN', @level2name = N'CODCOMSAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAN', @level2type = N'COLUMN', @level2name = N'CODCOMSAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) del componente sanguíneo, clave primaria de la tabla HCCOMSAN', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAN', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Componente Sanguineo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAN', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAN', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Validación de Plasma Fresco Congelado (INT, default=2): 1=Es plasma fresco congelado (PFC), 2=No es PFC; identifica componentes con requerimientos especiales de congelación y descongelación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAN', @level2type = N'COLUMN', @level2name = N'FrozenFreshPlasma';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Validar si Corresponde a Plasma Fresco Congelado 1->Si,   2->No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAN', @level2type = N'COLUMN', @level2name = N'FrozenFreshPlasma';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAN', @level2type = N'COLUMN', @level2name = N'FrozenFreshPlasma';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de componentes sanguíneos (hemocomponentes) utilizados en transfusiones y procedimientos de hemoterapia. Registra cada tipo de hemocomponente con sus rangos de contenido permitidos y configuraciones especiales como plasma fresco congelado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAN';
