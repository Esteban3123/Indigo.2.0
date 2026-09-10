CREATE TABLE [dbo].[HCACTENFE] (
    [CODACTENF]           CHAR (3)   NOT NULL,
    [DESACTENF]           CHAR (100) NOT NULL,
    [EXIREGINS]           BIT        NOT NULL,
    [EXIRESULT]           BIT        NOT NULL,
    [ACTFACTUR]           BIT        NOT NULL,
    [CODSERIPS]           CHAR (20)  NULL,
    [ESTADO]              INT        CONSTRAINT [DF_HCACTENFE_ESTADO] DEFAULT ((1)) NOT NULL,
    [RequiresNursingPack] BIT        CONSTRAINT [DF_HCACTENFE_requiresNursingPack] DEFAULT ((0)) NOT NULL,
    [IDAGPAQUETES]        INT        NULL,
    CONSTRAINT [PK_HCACTENFE] PRIMARY KEY CLUSTERED ([CODACTENF] ASC),
    CONSTRAINT [FK_HCACTENFE_AGPAQUETES] FOREIGN KEY ([IDAGPAQUETES]) REFERENCES [dbo].[AGPAQUETES] ([ID]),
    CONSTRAINT [FK_HCACTENFE_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del paquete de enfermería asociado a la actividad. Referencia FK a tabla AGPAQUETES. Tipo INT, permite NULL. Define el conjunto de servicios de enfermería vinculados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTENFE', @level2type = N'COLUMN', @level2name = N'IDAGPAQUETES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del paquete de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTENFE', @level2type = N'COLUMN', @level2name = N'IDAGPAQUETES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTENFE', @level2type = N'COLUMN', @level2name = N'IDAGPAQUETES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que especifica si la actividad de enfermería requiere un paquete de enfermería para su ejecución. Valor por defecto: 0 (no requiere).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTENFE', @level2type = N'COLUMN', @level2name = N'RequiresNursingPack';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Requiere paquete de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTENFE', @level2type = N'COLUMN', @level2name = N'RequiresNursingPack';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTENFE', @level2type = N'COLUMN', @level2name = N'RequiresNursingPack';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la actividad de enfermería: 1=Activo, 2=Inactivo. INT con valor por defecto 1. Define la disponibilidad de la actividad en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTENFE', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado 1-> Activo 2->Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTENFE', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTENFE', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de procedimiento o servicio (CUPS/RIPS) para la actividad de enfermería. CHAR(20), nullable. Referencia FK a INCUPSIPS. OBSOLETO: En desuso, migrar datos a tabla dbo.BillableNursingActivityServices (PBI 16608).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTENFE', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unico de Procedimientos y Servicios 

Este campo se dejará de usar y pasara hacer obsoleto para guardar los datos en la tabla dbo.BillableNursingActivityServices 

(PBI 16608 - Creación de descripcion relacionada en actividades de agendamiento)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTENFE', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTENFE', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que determina si la actividad de enfermería es facturable. Habilita la generación de cargos en facturación y RIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTENFE', @level2type = N'COLUMN', @level2name = N'ACTFACTUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina si la actividad de enfermeria es facturable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTENFE', @level2type = N'COLUMN', @level2name = N'ACTFACTUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTENFE', @level2type = N'COLUMN', @level2name = N'ACTFACTUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que especifica si la actividad de enfermería exige registro obligatorio de resultado o conclusión del procedimiento realizado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTENFE', @level2type = N'COLUMN', @level2name = N'EXIRESULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Exige Resultado en procedimientos de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTENFE', @level2type = N'COLUMN', @level2name = N'EXIRESULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTENFE', @level2type = N'COLUMN', @level2name = N'EXIRESULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que especifica si la actividad de enfermería exige registro obligatorio de insumos, materiales o suministros utilizados en el procedimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTENFE', @level2type = N'COLUMN', @level2name = N'EXIREGINS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Exige registro de insumos en procedimientos de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTENFE', @level2type = N'COLUMN', @level2name = N'EXIREGINS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTENFE', @level2type = N'COLUMN', @level2name = N'EXIREGINS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual de la actividad de enfermería. CHAR(100). Campo legible que detalla el nombre y propósito de la actividad (cuidados, procedimiento, atención especializada).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTENFE', @level2type = N'COLUMN', @level2name = N'DESACTENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la Actividad de Enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTENFE', @level2type = N'COLUMN', @level2name = N'DESACTENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTENFE', @level2type = N'COLUMN', @level2name = N'DESACTENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de la actividad de enfermería. CHAR(3), clave primaria. Identificador corto alfanumérico que clasifica la actividad en el sistema de atención en salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTENFE', @level2type = N'COLUMN', @level2name = N'CODACTENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Actividad de Enfermeria ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTENFE', @level2type = N'COLUMN', @level2name = N'CODACTENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTENFE', @level2type = N'COLUMN', @level2name = N'CODACTENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de actividades de enfermería registradas en la historia clínica. Define los tipos de intervenciones o procedimientos que el personal de enfermería puede ejecutar y documentar, incluyendo si requieren registro de insumos, resultados, facturación o paquetes de enfermería.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTENFE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTENFE';
