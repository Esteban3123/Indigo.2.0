CREATE TABLE [Common].[PersonFreeTimeUse] (
    [Id]            INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PersonId]      INT NOT NULL,
    [FreeTimeUseId] INT NOT NULL,
    CONSTRAINT [PK_PersonFreeTimeUse__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PersonFreeTimeUse_FreeTimeUse] FOREIGN KEY ([FreeTimeUseId]) REFERENCES [Payroll].[FreeTimeUse] ([Id]),
    CONSTRAINT [FK_PersonFreeTimeUse_Person] FOREIGN KEY ([PersonId]) REFERENCES [Common].[Person] ([Id]),
    CONSTRAINT [UQ_PersonFreeTimeUse__PersonId] UNIQUE NONCLUSTERED ([PersonId] ASC)
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de referencia (FK) a la tabla Payroll.FreeTimeUse; representa el tipo o categoría de uso del tiempo libre (licencia, permiso, descanso) asignado al profesional de la salud o empleado.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonFreeTimeUse', @level2type = N'COLUMN', @level2name = N'FreeTimeUseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Llave foranea a tabla uso del tiempo libre', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonFreeTimeUse', @level2type = N'COLUMN', @level2name = N'FreeTimeUseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonFreeTimeUse', @level2type = N'COLUMN', @level2name = N'FreeTimeUseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de referencia (FK) a la tabla Common.Person; vincula el registro de uso de tiempo libre a la persona, profesional de la salud o empleado específico del sistema.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonFreeTimeUse', @level2type = N'COLUMN', @level2name = N'PersonId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Llave foranea a Persona', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonFreeTimeUse', @level2type = N'COLUMN', @level2name = N'PersonId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonFreeTimeUse', @level2type = N'COLUMN', @level2name = N'PersonId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementado (INT IDENTITY) de la relación persona-uso de tiempo libre; clave primaria que identifica cada asignación de uso de tiempo libre a un individuo.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonFreeTimeUse', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla de usos del tiempo libre de la persona', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonFreeTimeUse', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonFreeTimeUse', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona personas con sus actividades o usos del tiempo libre registrados. Permite asociar múltiples actividades de tiempo libre a una misma persona.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonFreeTimeUse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonFreeTimeUse';
