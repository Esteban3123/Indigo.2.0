CREATE TABLE [Admissions].[CareEnvironment] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [Code]             VARCHAR (10)  NOT NULL,
    [Name]             VARCHAR (150) NOT NULL,
    [Status]           BIT           CONSTRAINT [DF_CareEnvironment_Status] DEFAULT ((1)) NOT NULL,
    [UserCreation]     VARCHAR (50)  NOT NULL,
    [DateCreation]     DATETIME2 (0) CONSTRAINT [DF_CareEnvironment_DateCreation] DEFAULT (sysdatetime()) NOT NULL,
    [UserModification] VARCHAR (50)  NULL,
    [DateModification] DATETIME2 (0) NULL,
    CONSTRAINT [PK_CareEnvironment] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_CareEnvironment_Code]
    ON [Admissions].[CareEnvironment]([Code] ASC);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Catálogo de ambientes o entornos de atención clínica utilizados en el módulo de admisiones. Cada registro define un tipo de entorno mediante un código único y un nombre descriptivo, con estado activo/inactivo por defecto activo. Registra auditoría de creación y modificación por usuario y fecha.', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'TABLE', @level1name=N'CareEnvironment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'TABLE', @level1name=N'CareEnvironment';
GO
