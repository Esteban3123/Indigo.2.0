CREATE TABLE [dbo].[HealthTechnologyPurposes] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [Code]             VARCHAR (3)   NOT NULL,
    [Name]             VARCHAR (300) NOT NULL,
    [Status]           BIT           NOT NULL,
    [UserCreation]     CHAR (20)     NOT NULL,
    [DateCreation]     DATETIME      NOT NULL,
    [UserModification] CHAR (20)     NULL,
    [DateModification] DATETIME      NULL,
    CONSTRAINT [PK_HealthTechnologyPurposes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_HealthTechnologyPurposes_SEGusuaru_1] FOREIGN KEY ([UserCreation]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI]),
    CONSTRAINT [FK_HealthTechnologyPurposes_SEGusuaru_2] FOREIGN KEY ([UserModification]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI])
);




GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_HealthTechnologyPurposes]
    ON [dbo].[HealthTechnologyPurposes]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de la última modificación del registro de finalidad de tecnología de la salud; NULL si nunca fue editado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HealthTechnologyPurposes', @level2type = N'COLUMN', @level2name = N'DateModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la fecha de la modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HealthTechnologyPurposes', @level2type = N'COLUMN', @level2name = N'DateModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HealthTechnologyPurposes', @level2type = N'COLUMN', @level2name = N'DateModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario (CHAR 20, FK a SEGusuaru.CODUSUARI) que realizó la última modificación; NULL si el registro no ha sido editado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HealthTechnologyPurposes', @level2type = N'COLUMN', @level2name = N'UserModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el usuario quien modifico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HealthTechnologyPurposes', @level2type = N'COLUMN', @level2name = N'UserModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HealthTechnologyPurposes', @level2type = N'COLUMN', @level2name = N'UserModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación del registro de finalidad de tecnología de la salud; auditoría de origen del dato.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HealthTechnologyPurposes', @level2type = N'COLUMN', @level2name = N'DateCreation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la fecha de creación ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HealthTechnologyPurposes', @level2type = N'COLUMN', @level2name = N'DateCreation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HealthTechnologyPurposes', @level2type = N'COLUMN', @level2name = N'DateCreation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario (CHAR 20, FK a SEGusuaru.CODUSUARI) que creó el registro; trazabilidad de responsable de registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HealthTechnologyPurposes', @level2type = N'COLUMN', @level2name = N'UserCreation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el usuario creacion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HealthTechnologyPurposes', @level2type = N'COLUMN', @level2name = N'UserCreation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HealthTechnologyPurposes', @level2type = N'COLUMN', @level2name = N'UserCreation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado lógico (BIT) de la finalidad: 1=Activo (vigente) | 0=Inactivo (deshabilitado); controla disponibilidad en procesos de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HealthTechnologyPurposes', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se almacena el estado de las finalidades de tecnologia de la salud: Estado True -> Activo  False -> Inactivo ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HealthTechnologyPurposes', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HealthTechnologyPurposes', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción o nombre completo (VARCHAR 300) de la finalidad de la tecnología de la salud (ej: diagnóstico, atención clínica, investigación).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HealthTechnologyPurposes', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de las finalidades de tecnologia de la salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HealthTechnologyPurposes', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HealthTechnologyPurposes', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único alfanumérico (VARCHAR 3) identificador de la finalidad de tecnología de la salud; usado para búsquedas y referencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HealthTechnologyPurposes', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Código de la finalidades de tecnologia de la salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HealthTechnologyPurposes', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HealthTechnologyPurposes', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico único (INT IDENTITY) y consecutivo de la tabla HealthTechnologyPurposes; clave primaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HealthTechnologyPurposes', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HealthTechnologyPurposes', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HealthTechnologyPurposes', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo maestro de propósitos o finalidades de las tecnologías en salud (medicamentos, dispositivos, procedimientos). Define para qué se usa cada tecnología sanitaria, permitiendo clasificarla según su objetivo clínico o terapéutico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HealthTechnologyPurposes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HealthTechnologyPurposes';
