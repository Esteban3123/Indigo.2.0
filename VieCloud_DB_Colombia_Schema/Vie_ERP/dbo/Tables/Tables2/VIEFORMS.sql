CREATE TABLE [dbo].[VIEFORMS] (
    [Id]                    VARCHAR (10)  NULL,
    [Nombre]                VARCHAR (100) NULL,
    [Tipo]                  VARCHAR (10)  NULL,
    [IdModuleSource]        VARCHAR (10)  NULL,
    [PrintEvents]           VARCHAR (10)  NULL,
    [GroupForms]            VARCHAR (50)  NULL,
    [HasSequence]           VARCHAR (10)  NULL,
    [IsNativeForm]          VARCHAR (10)  NULL,
    [HasForm]               VARCHAR (10)  NULL,
    [IsFoundational]        VARCHAR (10)  NULL,
    [ClassName]             VARCHAR (100) NULL,
    [AssemblyName]          VARCHAR (100) NULL,
    [HandlesMassiveConfirm] VARCHAR (10)  NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Catálogo de formularios del sistema, donde cada registro define un formulario identificado por un código, su nombre, tipo y módulo de origen. Almacena metadatos de comportamiento como si el formulario imprime eventos, pertenece a un grupo, maneja secuencias, es nativo, foundacional o soporta confirmación masiva. También registra el nombre de clase y ensamblado .NET asociado, lo que indica integración con componentes de software para renderizar o procesar cada formulario.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'VIEFORMS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'VIEFORMS';
GO
